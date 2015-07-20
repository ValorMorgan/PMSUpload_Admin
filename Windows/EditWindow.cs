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
                this.Close(); // Close the window.
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
            this.Close();
        }
        
        /// <summary>
        /// Events that occur when the window first loads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditWindow_Load(object sender, EventArgs e)
        {
            // Centers the window when using "Show()"
            this.CenterToParent();

            // Fill the fields with the proper data that was selected
            MainWindowHelper mainWindowHelper = new MainWindowHelper();
            List<string> headers = mainWindowHelper.GetHeaders();

            foreach (string header in headers)
            {
                if (headers.IndexOf(header) <= 0)
                {
                    ClaimForm.RowStyles[0].Height = 20F;
                }
                else
                    ClaimForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));

                #region HeaderLabel
                Label headerLabel = new Label();
                headerLabel.AutoSize = true;
                headerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                headerLabel.Size = new System.Drawing.Size(45, 20);
                headerLabel.Location = new Point(2, 0);
                headerLabel.TextAlign = ContentAlignment.MiddleRight;
                headerLabel.TabIndex = 0;
                headerLabel.Text = header + ":";
                headerLabel.Name = "headerLabel" + (headers.IndexOf(header) + 1);
                #endregion
                #region TextBox
                TextBox textBox = new TextBox();
                textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
                textBox.Name = "textBox" + (headers.IndexOf(header) + 1);
                textBox.Size = new System.Drawing.Size(289, 20);
                textBox.TabIndex = 0;
                #endregion

                ClaimForm.Controls.Add(textBox, 1, headers.IndexOf(header));
                ClaimForm.Controls.Add(headerLabel, 0, headers.IndexOf(header));
            }

            // Restrict window size (pre-set width, total height)
            Size windowSize = new Size(500, panel1.Height + 104);
            this.MaximumSize = this.MinimumSize = this.Size = windowSize;
        }
        #endregion
    }
}
