using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PMSUpload_Admin.Helpers;

namespace PMSUpload_Admin
{
    /// <summary>
    /// Pop-up window that is shown when a row is selected from the MainWindow.
    /// It shows each cell as a text box that the user may edit then save.
    /// </summary>
    public partial class EditWindow : Form
    {
        #region PROPERTIES
        #endregion

        #region CONSTRUCTOR
        public EditWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(((ex.InnerException == null) ? ex.Message : ex.InnerException.Message) + Environment.NewLine + ex.StackTrace);
                this.Dispose(); // Close the window.
            }
        }
        #endregion

        #region METHODS
        #endregion

        #region EVENTS
        /// <summary>
        /// Closes the window without saving any changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        
        /// <summary>
        /// Events that occur when the window first loads.
        /// Checks against the header name exist and should be considered
        /// when changes occur in the stored procedure.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditWindow_Load(object sender, EventArgs e)
        {
            // Centers the window when using "Show()"
            this.CenterToParent();

            // Fill the fields with the proper data that was selected
            List<string> headers = MainWindowHelper.GetHeaders();

            // Clear out any existing controls
            ClaimForm.Controls.Clear();
            foreach (string header in headers)
            {
                // Don't add the key column
                if (header.Equals("PMSPrimaryKey"))
                    continue;

                // If the first header, don't create the extra row
                if (ClaimForm.Controls.Count <= 0)
                {
                    ClaimForm.RowStyles[0].Height = 24F;
                }
                else
                    ClaimForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));

                // Declare and setup the form elements
                #region HeaderLabel
                Label headerLabel = new Label();
                headerLabel.AutoSize = true;
                headerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                headerLabel.Size = new System.Drawing.Size(45, 20);
                headerLabel.Location = new Point(2, 0);
                headerLabel.TextAlign = ContentAlignment.MiddleRight;
                headerLabel.TabIndex = 0;
                headerLabel.Text = header + ":";
                headerLabel.Name = "headerLabel" + ClaimForm.RowStyles.Count;
                #endregion
                #region TextBox
                TextBox textBox = new TextBox();
                textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
                textBox.Name = "textBox" + ClaimForm.RowStyles.Count;
                textBox.Size = new System.Drawing.Size(200, 20);
                textBox.TabIndex = 0;
                textBox.Text = MainWindowHelper.GetRowData(header);
                // Set these columns to be greyed out
                if (header.Equals("clmClaimNumber") || header.Equals("trxAmount"))
                    textBox.Enabled = false;
                #endregion
                #region DropDown
                ComboBox comboBox = new ComboBox();
                if (header.Equals("trxAmount"))
                {
                    comboBox.Cursor = System.Windows.Forms.Cursors.Hand;
                    comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                    comboBox.FormattingEnabled = true;
                    comboBox.MaxDropDownItems = 2;
                    comboBox.MaxLength = 1;
                    comboBox.Name = "comboBox" + ClaimForm.RowStyles.Count;
                    comboBox.Size = new System.Drawing.Size(32, 21);
                    comboBox.TabIndex = 0;
                    comboBox.Items.Add("+");
                    comboBox.Items.Add("-");
                    // If there is a value, determine the sign and make seen value absolute
                    if (!string.IsNullOrEmpty(textBox.Text))
                    {
                        comboBox.SelectedItem = (float.Parse(textBox.Text) < 0) ? "-" : "+";
                        textBox.Text = Math.Abs(float.Parse(textBox.Text)).ToString();
                    }
                }
                #endregion

                // Add the elements to the display
                // The dropbox only applies to trxAmount
                ClaimForm.Controls.Add(headerLabel, 0, ClaimForm.RowStyles.Count - 1);
                if (header.Equals("trxAmount"))
                {
                    ClaimForm.Controls.Add(comboBox, 1, ClaimForm.RowStyles.Count - 1);
                }
                ClaimForm.Controls.Add(textBox, 2, ClaimForm.RowStyles.Count - 1);
            }
        }
        #endregion
    }
}
