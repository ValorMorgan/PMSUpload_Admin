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
        /// Tests if the user can connect to the server that the application points to
        /// by creating, opening and closing the connection.
        /// </summary>
        public static void TestConnect()
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Calls the stored procedure and returns the resultset
        /// as a DataTable.
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProcedureDataTable()
        {
            try
            {
                SqlCommand command = new SqlCommand("spPMSUploadAdmin_GetClaimTransactions", connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                DataTable returnTable = new DataTable();
                returnTable.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(returnTable);

                return returnTable;
            }
            catch (Exception ex)
            {
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
                SqlCommand command = new SqlCommand(commandString, connection);
                connection.Open();
                command.ExecuteNonQuery();

                // Refresh the MainWindow's master table
                MainWindowHelper.mainWindow.RefreshMainWindow();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion
    }
}
