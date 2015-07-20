using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PMSUpload_Admin.Helpers
{
    /// <summary>
    /// Holds functionalities for getting and setting system information and
    /// application wide values.
    /// </summary>
    class ApplicationHelper
    {
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
            // Uses this connection with the App.config connectionstring as the connectino settings
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[1].ConnectionString))
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
        }
    }
}
