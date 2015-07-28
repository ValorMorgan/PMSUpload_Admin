using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using PMSUpload_Admin.Helpers;

/*===================================================*\
 *                  PMSUpload_Admin                  *
 *              Created By: Joshua Morgan            *
 *                                                   *
 *      Graphical interface tool for administration  *
 *  to view claims data on the GuideWire database    *
 *  and edit visible fields.  The application shows  *
 *  only data that is retrieved from stored          *
 *  procedures and dynamically creates the forms.    *
 *  Assemblies only need occur for special cases     *
 *  such as certain data only needed to be seen but  *
 *  not editable.                                    *
 *                                                   * 
 *  -- Table of Contens --                           *
 *  MainWindow: The primary window the users see.    *
 *    Here, the users are given a search field and   *
 *    a table to select claims from.                 *
 *  EditWindow: A popup form for editing a claim.    *
 *    Here, the users are given a list of text boxes *
 *    with set lengths dependant on the database.    *
 *    If new data is entered and the save button is  *
 *    hit, the database will save the claim.         *
 *  ApplicationHelper: Performs database connection  *
 *    tasks.  Here, the server connection is setup   *
 *    and calls to the database procedures are made. *
 *  MainWindowHelper: Serves as a connection for     *
 *    other classes to call functions and features   *
 *    within the MainWindow.                         *
\*===================================================*/

namespace PMSUpload_Admin
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Determines if the user can actually connect to the server.
                ApplicationHelper.SetupApplication();

                Application.Run(new MainWindow());
            }
            catch (Exception ex)
            {
                string errorMsg = "The server may not be responding or you may not have the rights to run this application.  If you feel this is wrong, please send a request ticket to ISD with the below information."
                    + Environment.NewLine + "Server: " + System.Configuration.ConfigurationManager.ConnectionStrings[1].ConnectionString.Split(';')[0].Split('=')[1]
                    + Environment.NewLine + "Database: " + System.Configuration.ConfigurationManager.ConnectionStrings[1].ConnectionString.Split(';')[1].Split('=')[1]
                    + Environment.NewLine
                    + Environment.NewLine + "Error Message"
                    + Environment.NewLine + ((ex.InnerException == null) ? ex.Message : ex.InnerException.Message)
                    + Environment.NewLine
                    + Environment.NewLine + "Stack Trace"
                    + Environment.NewLine + ex.StackTrace;
                MessageBox.Show(errorMsg, "Failed to access server");
                Console.WriteLine(errorMsg);
            }
            finally
            {
                ApplicationHelper.CloseConnection();
            }
        }
    }
}
