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
        /// <summary>
        /// The popup window where the user enters custom data and saves the claim.
        /// </summary>
        private EditWindow editWindow = new EditWindow();
        /// <summary>
        /// The original list of data to roll back to during searches.
        /// </summary>
        private DataTable masterTable = new DataTable();
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
                string errorMsg = ((ex.InnerException == null) ? ex.Message : ex.InnerException.Message) + Environment.NewLine + ex.StackTrace;
                MessageBox.Show(errorMsg, "Error");
                Console.WriteLine(errorMsg);
                this.Dispose(); // Close the window and end the program.
            }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Checks the given dataList against the DataTable selected cells
        /// and determines if there is new data.
        /// Assumes index for dataList matches index for DataTable cells.
        /// </summary>
        /// <param name="dataList">The list to compare against the table</param>
        /// <returns></returns>
        public bool IsNewData(List<string> dataList)
        {
            bool isNew = false;
            for (int i = 0; i < DataTable.SelectedCells.Count && i < dataList.Count; i++)
            {
                if (!dataList[i + 1].Equals(GetCellData(i)))
                {
                    isNew = true;
                    break;
                }
            }
            return isNew;
        }

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
                // Make sure only visible columns are returned
                if(column.Visible == true)
                    returnList.Add(column.HeaderText);
            }

            return returnList;
        }

        /// <summary>
        /// Gets the selected row's data from the column specified.
        /// If the column does not exist, returns null.
        /// </summary>
        /// <param name="header">The name of the column.</param>
        /// <returns></returns>
        public string GetCellData(string header)
        {
            if (!DataTable.Columns.Contains(header))
                return null;

            return DataTable.SelectedRows[0].Cells[header].Value.ToString().Trim();
        }
        /// <summary>
        /// Gets the selected row's data from the column specified.
        /// If the column does not exist, returns null.
        /// </summary>
        /// <param name="index">The known index of the cell (the column number).</param>
        /// <returns></returns>
        public string GetCellData(int index)
        {
            if (DataTable.Columns.Count <= index)
                return null;

            return DataTable.SelectedCells[index].Value.ToString().Trim();
        }

        /// <summary>
        /// Updates the DataTable by calling the procedure and getting the most up-to-date data.
        /// Commented out code that can be used if DataTable
        /// is filled before a search occurs.
        /// </summary>
        /// <param name="claim">The claim to get data on from the stored procedure. This is usually the search text.</param>
        public void UpdateDataTable(string claim)
        {
            // If the searchBox has nothing in it or is not the length of a claim, no data can be found.
            if (string.IsNullOrWhiteSpace(claim) || claim.Length < 15)
                throw new Exception("Claim was not found.  Did you type the claim number in correctly?");

            //try
            //{
                // Get the data and create a binding source
                masterTable = ApplicationHelper.GetProcedureDataTable(claim);
                BindingSource source = new BindingSource();

                //// Check for repopulation of the auto-complete cache
                //bool newRows = DataTable.Rows.Count != masterTable.Rows.Count;

                // Assign the data sources
                source.DataSource = masterTable;
                DataTable.DataSource = source;

                // Update the record count in the status bar
                recordCount.Text = "|  Claims Found: " + masterTable.Rows.Count;
                Console.Write("Updated Master Table: " + masterTable.Rows.Count + " claims");

                // Hide the key column
                int indexOfKey = masterTable.Columns.IndexOf("PMSPrimaryKey");
                if (indexOfKey >= 0)
                {
                    DataTable.Columns[indexOfKey].Visible = false;
                    Console.WriteLine("  Primary Key found.  Hid column.");
                }
                else
                    Console.WriteLine("  Primary Key was not found.");

                //// Get the index for the search parameter (return if not found or if nothing new to add)
                //int indexOfSearch = masterTable.Columns.IndexOf("clmClaimNumber");
                //if (indexOfSearch <= 0 || !newRows)
                //    return;

                //// Get a list of the claims in the dataTable (duplicates included)
                //List<string> claims = new List<string>() { };
                //foreach (DataGridViewRow row in DataTable.Rows)
                //{
                //    claims.Add(row.Cells[indexOfSearch].Value.ToString());
                //}

                //// Setup the auto-complete selection for the searchBox
                //worker_SearchBox.RunWorkerAsync(claims);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(((ex.InnerException == null) ? ex.Message : ex.InnerException.Message) + Environment.NewLine + ex.StackTrace);
            //    // If the window is not loaded, end the program
            //    if (!this.IsAccessible)
            //        this.Dispose();
            //}
        }

        /// <summary>
        /// Refreshes the MainWindow back to the state that it was at when first loaded.
        /// Will also get the latest data from the procedure if it changed mid-use.
        /// </summary>
        public void RefreshMainWindow()
        {
            int selectedRowIndex = DataTable.Rows.IndexOf(DataTable.SelectedRows[0]);
            try
            {
                Console.WriteLine("Refreshing the MainWindow.");
                this.Enabled = false;
                UpdateDataTable(DataTable.Rows[0].Cells["clmClaimNumber"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Enabled = true;

                // Re-select the currect active claim
                DataTable.Rows[selectedRowIndex].Selected = true;
            }
        }
        #endregion

        #region EVENTS
        /// <summary>
        /// Called when the window first loads.  Handles setting
        /// the logged in user in the StatusBar.
        /// Will also catch errors with the provided data connection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Load(object sender, EventArgs e)
        {
            try
            {
                Console.Write("MainWindow loading...");
                // Sets the current user in the status bar
                CurrentUser.Text = ApplicationHelper.GetCurrentUserName();

                Console.Write("...");
                // Set the association between the MainWindow and the helper.
                Helpers.MainWindowHelper.mainWindow = this;
                Console.WriteLine("...Done");
            }
            catch (Exception ex)
            {
                Console.WriteLine("...Failed");
                string errorMsg = ((ex.InnerException == null) ? ex.Message : ex.InnerException.Message) + Environment.NewLine + ex.StackTrace;
                MessageBox.Show(errorMsg, "Error");
                Console.WriteLine(errorMsg);
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
                try
                {
                    Console.Write("Opening claim " + GetCellData("clmClaimNumber") + "...");
                    if (editWindow.IsDisposed)
                        editWindow = new EditWindow();
                    // Update data (if 2 working on same record, data can go stale)
                    RefreshMainWindow();
                    Console.WriteLine("...Done");
                    editWindow.ShowDialog();
                    Console.Write("Closing claim " + GetCellData("clmClaimNumber") + "...");
                    editWindow.Dispose();
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
                try
                {
                    Console.Write("Opening claim " + GetCellData("clmClaimNumber") + "...");
                    if (editWindow.IsDisposed)
                        editWindow = new EditWindow();
                    // Update data (if 2 working on same record, data can go stale)
                    RefreshMainWindow();
                    Console.WriteLine("...Done");
                    editWindow.ShowDialog();
                    Console.Write("Closing claim " + GetCellData("clmClaimNumber") + "...");
                    editWindow.Dispose();
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
        }

        /// <summary>
        /// Handles all of the search operations.
        /// Commented out code that can be used if DataTable
        /// is filled before a search occurs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the data in the DataTable
                UpdateDataTable(searchBox.Text);

                refreshButton.Enabled = true;

                //bool doSearch = true;
                //DataTable searchTable;

                //// Check if the searchBox is empty and a search was never performed
                //if (string.IsNullOrEmpty(searchBox.Text)) //&& DataTable.Rows.Count >= masterTable.Rows.Count)
                //    return;
                //else if (string.IsNullOrEmpty(searchBox.Text))
                //    doSearch = false;
                
                //// Do the search if there is something to search for
                //if (doSearch)
                //{

                    //// Setup the new table for display
                    //searchTable = new DataTable();
                    //foreach (DataColumn column in masterTable.Columns)
                    //{
                    //    searchTable.Columns.Add(column.ColumnName);
                    //}

                    //// Find each row that has the claim number searched for
                    //foreach (DataRow row in masterTable.Rows)
                    //{
                    //    object[] rowCells = row.ItemArray;
                    //    foreach (object cell in rowCells)
                    //    {
                    //        if (cell.ToString().Contains(searchBox.Text))
                    //        {
                    //            searchTable.Rows.Add(rowCells);
                    //            break;
                    //        }
                    //    }
                    //}

                    //// If nothing was found
                    //if (searchTable.Rows.Count <= 0)
                    //    throw new Exception("Claim was not found.  Did you type the claim number in correctly?");
                //}
                //else // Otherwise assume to reset table
                //    searchTable = masterTable;

                //BindingSource source = new BindingSource();

                //// Assign the data sources
                //source.DataSource = searchTable;
                //DataTable.DataSource = source;

                //// Hide the key column
                //int indexOfKey = searchTable.Columns.IndexOf("PMSPrimaryKey");
                //if (indexOfKey >= 0)
                //    DataTable.Columns[indexOfKey].Visible = false;
            }
            catch (Exception ex)
            {
                string errorMsg = ((ex.InnerException == null) ? ex.Message : ex.InnerException.Message);
                MessageBox.Show(errorMsg, "Error");
                Console.WriteLine(errorMsg);
            }
        }
        
        /// <summary>
        /// Calls the RefreshMainWindow function to re-draw the MainWindow
        /// and update the DataTable.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(searchBox.Text) && searchBox.Text.Length >= 15)
                RefreshMainWindow();
        }

        /// <summary>
        /// Handles events when the user presses a key in the searchBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchBox_KeyPress(object sender, KeyEventArgs e)
        {
            // Checks if the "enter" key is pressed
            if (e.KeyCode == Keys.Enter)
            {
                searchTimer.Stop();
                this.searchButton_Click(sender, e);
            }
        }

        /// <summary>
        /// Handles calling the background worker to fill the auto-complete while
        /// the user types.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            if (worker_SearchBox.IsBusy || searchBox.Text.Length < 2 || searchBox.Text.Length >= 15)
            {
                searchTimer.Stop();
            }
            else
                searchTimer.Start();
        }

        /// <summary>
        /// Occurs if a second passes while the user is typing in the searchBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            worker_SearchBox.RunWorkerAsync(searchBox.Text);
        }
        #endregion

        #region BACKGROUND_THREAD
        /// <summary>
        /// Background thread workspace for populating the searchBox
        /// auto-complete values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_SearchBox_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Console.WriteLine("SearchTimer ticked.  Performing search...");
                Console.Write("...");
                // Setup the list with unique claims
                List<string> autoCompleteList = new List<string>() { };
                foreach (string item in ApplicationHelper.GetAutoCompleteList(e.Argument.ToString()))
                    autoCompleteList.Add(item);
                Console.Write("...");

                // Fill the list in the searchBox
                // An invoke check exists to work with the main thread
                this.Invoke(new MethodInvoker(delegate
                {
                    searchBox.Items.Clear();
                    searchBox.Items.AddRange(autoCompleteList.ToArray());
                }));
                Console.Write("...");
            }
            catch (Exception ex)
            {
                string errorMsg = ((ex.InnerException == null) ? ex.Message : ex.InnerException.Message) + Environment.NewLine + ex.StackTrace;
                MessageBox.Show(errorMsg, "Background Thread Error");
                Console.WriteLine(errorMsg);
            }
            finally
            {
                // Display the dropdown so the user can see their options
                this.Invoke(new MethodInvoker(delegate
                {
                    searchBox.Focus();
                    if (searchBox.Items.Count > 0)
                        searchBox.DroppedDown = true;
                }));
                Console.WriteLine("...Done");
            }
        }
        #endregion
    }
}