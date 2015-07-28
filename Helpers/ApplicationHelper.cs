using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace PMSUpload_Admin.Helpers
{
    /// <summary>
    /// Holds functionalities for getting and setting system information and
    /// application wide values.
    /// </summary>
    class ApplicationHelper
    {
        #region PROPERTIES
        /// <summary>
        /// The SQL server connection.
        /// </summary>
        private static SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[1].ConnectionString);
        /// <summary>
        /// The validation table for confirming input data is accurate.
        /// </summary>
        private static DataTable dataValidationTable = new DataTable();
        #endregion

        #region METHODS
        /// <summary>
        /// Gets the current active user based off the Windows Login.
        /// </summary>
        /// <returns>WindowsIdentity.GetCurrent().Name</returns>
        public static string GetCurrentUserName()
        {
            return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        /// <summary>
        /// Used both to test if the user can access the server
        /// and sets up the data validation table.
        /// </summary>
        public static void SetupApplication()
        {
            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("spPMSUploadAdmin_DataValidationTable", connection);

                Console.Write("Calling spPMSUploadAdmin_DataValidationTable...");
                dataValidationTable.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(dataValidationTable);
                Console.WriteLine("...Done");

                if (dataValidationTable.Rows.Count <= 0)
                    throw new Exception("Failed to setup application!  Contact ISD for further assistance.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("...Failed");
                throw ex;
            }
        }

        /// <summary>
        /// Calls the stored procedure and returns the resultset
        /// as a DataTable.
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProcedureDataTable(string claim)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("spPMSUploadAdmin_GetClaimTransactions '" + claim + "'", connection);

                Console.Write("Calling spPMSUploadAdmin_GetClaimTransactions '" + claim + "'...");
                DataTable returnTable = new DataTable();
                returnTable.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(returnTable);
                Console.WriteLine("...Done");

                if (returnTable.Rows.Count <= 0)
                    throw new Exception("Claim was not found.  Did you type the claim number in correctly?");

                return returnTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine("...Failed");
                throw ex;
            }
        }

        /// <summary>
        /// Calls the stored procedure with the parameter list to save the claim into
        /// the database.
        /// </summary>
        /// <param name="parameters"></param>
        public static void SaveClaim(List<string> parameters)
        {
            try
            {
                // Setup the command string to call the stored procedure with the parameters
                string commandString = "spPMSUploadAdmin_SaveClaimTransaction";
                foreach (string param in parameters)
                {
                    if (string.IsNullOrWhiteSpace(param))
                        commandString += (parameters.IndexOf(param) != 0) ? ", NULL" : " NULL";
                    else
                    {
                        // Check if there are characters or symbols and add the ''
                        if (param.Any(x => char.IsLetter(x)) || param.Any(x => char.IsSymbol(x)) || param.Any(x => char.IsPunctuation(x)))
                            commandString += (parameters.IndexOf(param) != 0) ? (", '" + param + "'") : (" '" + param + "'");
                        else
                            commandString += (parameters.IndexOf(param) != 0) ? (", " + param) : (" " + param);
                    }
                }

                // Sets the actual command and executes it
                // The execute command returns the number of rows affected if desired
                Console.Write("Calling " + commandString + "...");
                SqlCommand command = new SqlCommand(commandString, connection);
                command.ExecuteNonQuery();
                Console.WriteLine("...Done");

                // Refresh the MainWindow's master table and the EditWindow if it is open
                // Refresh the EditWindow for getting the new PrimaryKey
                MainWindowHelper.mainWindow.RefreshMainWindow();
            }
            catch (Exception ex)
            {
                Console.WriteLine("...Failed");
                throw ex;
            }
        }

        /// <summary>
        /// Grabs the data row data from the validation table to confirm
        /// the input data against the setup in the database.
        /// If no data is found for the given column name, null will be returned.
        /// </summary>
        /// <param name="header">The column name to check for.</param>
        /// <returns>[fieldLength, isAbsoluteLength (0,1)]</returns>
        public static List<string> GetValidationData(string header)
        {
            if (dataValidationTable.Rows.Count <= 0)
                throw new Exception("Validation table is not setup properly!");

            List<string> returnList = new List<string>() { };
            foreach (DataRow row in dataValidationTable.Rows)
            {
                if (row.ItemArray[0].ToString().Equals(header))
                {
                    returnList.Add(row.ItemArray[1].ToString());
                    returnList.Add(row.ItemArray[2].ToString());
                    return returnList;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a list of unique claims from the PMSFinancialupload table
        /// in order by the claim number.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAutoCompleteList(string search)
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand("spPMSUploadAdmin_AllDistinctClaims '" + search.Trim() + "'", connection);

                Console.Write("Calling spPMSUploadAdmin_AllDistinctClaims '" + search.Trim() + "'...");
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                Console.Write("...");
                List<string> returnList = new List<string>() { };
                foreach (DataRow row in table.Rows)
                    returnList.Add(row.ItemArray[0].ToString());

                Console.WriteLine("...Done");
                return returnList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("...Failed");
                throw ex;
            }
        }

        /// <summary>
        /// Makes sure the connection to the database is
        /// closed.
        /// </summary>
        public static void CloseConnection()
        {
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
