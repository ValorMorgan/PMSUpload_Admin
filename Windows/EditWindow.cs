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
                string errorMsg = ((ex.InnerException == null) ? ex.Message : ex.InnerException.Message) + Environment.NewLine + ex.StackTrace;
                MessageBox.Show(errorMsg, "Error");
                Console.WriteLine(errorMsg);
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
            Console.Write("Loading EditWindow...");
            // Centers the window when using "Show()"
            this.CenterToParent();

            // Set the window display name
            this.Text = this.Text + MainWindowHelper.GetCellData("clmClaimNumber");

            // Fill the fields with the proper data that was selected
            List<string> headers = MainWindowHelper.GetHeaders();

            try
            {
                // Create the form and its elements
                ClaimForm.Controls.Clear();

                Console.Write("...");
                foreach (string header in headers)
                {
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
                    headerLabel.Margin = new System.Windows.Forms.Padding(0, 4, 3, 0);
                    headerLabel.Location = new Point(0, 0);
                    headerLabel.TextAlign = ContentAlignment.BottomLeft;
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
                    textBox.Size = new System.Drawing.Size(10, 20);
                    textBox.TabIndex = 0;
                    // Set these columns to be greyed out
                    if (header.Equals("clmClaimNumber") || header.Equals("trxAmount"))
                        textBox.Enabled = false;
                    List<string> dataConstraints = ApplicationHelper.GetValidationData(header);
                    if (dataConstraints != null)
                        textBox.MaxLength = int.Parse(dataConstraints[0]);
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
                        // Determine the sign
                        comboBox.SelectedItem = (float.Parse(MainWindowHelper.GetCellData(header)) < 0) ? "-" : "+";
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

                    // Get the data into the textBox (done here as a rounding occurs if put in TextBox above)
                    Control claimFormTextBox = ClaimForm.Controls[ClaimForm.Controls.Count - 1];
                    claimFormTextBox.Text = MainWindowHelper.GetCellData(header);
                    if (claimFormTextBox.Text.StartsWith("-"))
                        claimFormTextBox.Text = claimFormTextBox.Text.Remove(0, 1);
                }
                Console.Write("...");

                // Resize the window and add the scroll bars as need be
                // The width is +20 to offset for the scrollbar if its added
                // If not done, a horizontal scrollbar appears
                // Note: the ClaimForm in the designer has 20px space to the right to handle this
                if (ClaimForm.Controls.Count > 16)
                    ClaimForm.AutoScroll = true;
                ClaimForm.Size = new Size(ClaimForm.Size.Width + 20, ClaimForm.Size.Height);

                Console.WriteLine("...Done");
            }
            catch (Exception ex)
            {
                Console.WriteLine("...Failed");
                string errorMsg = ((ex.InnerException == null) ? ex.Message : ex.InnerException.Message) + Environment.NewLine + ex.StackTrace;
                MessageBox.Show(errorMsg, "Error");
                Console.WriteLine(errorMsg);
            }
        }

        /// <summary>
        /// Handles a save request from the user by going to the
        /// ApplicationHelper with all the parameters and calling
        /// the save stored procedure.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveClaim_Button_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("Saving claim...");

                // Get the data into a list
                List<string> parameters = new List<string>() { ApplicationHelper.GetCurrentUserName() };
                bool raiseError = false;
                string errorMessage = "";

                // Insert the PrimaryKey if it's being used
                if (!string.IsNullOrEmpty(MainWindowHelper.GetCellData("PMSPrimaryKey")))
                    parameters.Add(MainWindowHelper.GetCellData("PMSPrimaryKey"));

                foreach (Control control in ClaimForm.Controls)
                {
                    // If we are not looking at the data, move on
                    if (ClaimForm.GetColumn(control) != 2)
                        continue;

                    // Clear any highlights
                    control.BackColor = Color.White;

                    // Parameter to add
                    string parameter = "";

                    // Add all the parameters to the list
                    // Handle the positive negative dropdown of trxAmount
                    Control previousControl = ClaimForm.Controls[ClaimForm.Controls.IndexOf(control) - 1];
                    if (ClaimForm.GetColumn(previousControl) == 1 && previousControl.Text.Equals("-"))
                        parameter = previousControl.Text + control.Text;
                    else if (ClaimForm.GetColumn(previousControl) == 1)
                    {
                        parameter = control.Text;
                        previousControl = ClaimForm.Controls[ClaimForm.Controls.IndexOf(control) - 2];
                    }
                    else
                        parameter = control.Text;
                    parameters.Add(parameter);

                    // Validate input
                    if (!string.IsNullOrWhiteSpace(parameter))
                    {
                        List<string> validationCheck = ApplicationHelper.GetValidationData(previousControl.Text.Substring(0, previousControl.Text.Length - 1));
                        if (validationCheck != null && int.Parse(validationCheck[1]) > 0)
                        {
                            if (control.Text.Length != int.Parse(validationCheck[0]))
                            {
                                // Tell to raise an error and inform the user about the error and where it is
                                raiseError = true;
                                errorMessage += Environment.NewLine + "  - " + previousControl.Text + " needs to be of length " + validationCheck[0];
                                control.BackColor = Color.LightPink;
                            }
                        }
                    }
                }

                // Confirm that the data entered is valid (above)
                Console.Write("...IsValidData: ");
                if (raiseError)
                {
                    Console.WriteLine("false");
                    throw new Exception("Errors with input were found: " + errorMessage);
                }
                Console.WriteLine("true");

                // Confirm that there is new data (if NOT new data)
                //Console.Write("...IsNewData: ");
                //if (!MainWindowHelper.IsNewData(parameters))
                //{
                //    Console.WriteLine("false");
                //    throw new Exception("No new data was entered.");
                //}
                //Console.WriteLine("true");

                // Prompt user as a double-check
                if (DialogResult.No == MessageBox.Show("Transaction will be saved and re-uploaded to the mainframe." + Environment.NewLine + "Do you wish to continue?", "Continue?", MessageBoxButtons.YesNo))
                {
                    Console.WriteLine("...Canceled by user");
                    return;
                }

                // Tell the user to wait
                this.Enabled = false;

                ApplicationHelper.SaveClaim(parameters);

                Console.WriteLine("...Done");
            }
            catch (Exception ex)
            {
                Console.WriteLine("...Failed to save");
                string errorMsg = "Claim failed to save!" + Environment.NewLine + ((ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
                MessageBox.Show(errorMsg, "Failed to save");
                Console.WriteLine(errorMsg);
            }
            finally
            {
                // Close EditWindow when done
                this.Dispose();
            }
        }
        #endregion
    }
}
