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
    /// The primary window that the application is drawn on.
    /// If closed, the application will end.
    /// </summary>
    public partial class MainWindow : Form
    {
        #region PROPERTIES
        private EditWindow editWindow = new EditWindow();
        #endregion

        #region CONSTRUCTOR
        public MainWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(((ex.InnerException == null) ? ex.Message : ex.InnerException.Message) + Environment.NewLine + ex.StackTrace);
                this.Dispose(); // Close the window and end the program.
            }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Goes through every column in the DataTable and gets each header
        /// into a list to return.
        /// </summary>
        /// <returns></returns>
        public List<string> GetHeaders()
        {
            List<string> returnList = new List<string>() { };
            DataGridViewColumnCollection columns = DataTable.Columns;

            foreach (DataGridViewColumn column in columns)
            {
                returnList.Add(column.HeaderText);
            }

            return returnList;
        }
        #endregion

        #region EVENTS
        /// <summary>
        /// Called when the window first loads.  Handles populating
        /// the DataTable and setting the logged in user in the StatusBar.
        /// Will also catch errors with the provided data connection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Load(object sender, EventArgs e)
        {
            try
            {
                // Sets the current user in the status bar
                CurrentUser.Text = ApplicationHelper.GetCurrentUserName();
                // This line of code loads data into the DataTable.
                this.spPMSUploadAdmin_GetClaimTransactionsTableAdapter.Fill(this.getData.spPMSUploadAdmin_GetClaimTransactions);

                // Set the association between the MainWindow and the helper.
                Helpers.MainWindowHelper.mainWindow = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show(((ex.InnerException == null) ? ex.Message : ex.InnerException.Message) + Environment.NewLine + ex.StackTrace);
                this.Dispose(); // Close the window and end the program.
            }
        }

        /// <summary>
        /// Handles double-clicking a row / cell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Checks if the window is already open
            if (editWindow.Visible)
                editWindow.Focus();
            else
            {
                if (editWindow.IsDisposed)
                    editWindow = new EditWindow();
                editWindow.Show();
            }
        }

        /// <summary>
        /// Handles the select button being pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Select_Button_Click(object sender, EventArgs e)
        {
            // Checks if nothing is selected or if the window is already open
            if (DataTable.SelectedRows.Count <= 0)
                return;
            else if (editWindow.Visible)
                editWindow.Focus();
            else
            {
                if (editWindow.IsDisposed)
                    editWindow = new EditWindow();
                editWindow.Show();
            }
        }
        #endregion
    }
}