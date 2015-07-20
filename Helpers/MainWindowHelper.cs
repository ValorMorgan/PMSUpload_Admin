using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMSUpload_Admin.Helpers
{
    /// <summary>
    /// An access class to use functionalities in the MainWindow.
    /// </summary>
    public class MainWindowHelper
    {
        #region PROPERTIES
        /// <summary>
        /// A reference to the MainWindow to access functionalities here.
        /// </summary>
        public static MainWindow mainWindow { get; set; }
        #endregion

        #region METHODS
        /// <summary>
        /// Goes through every column in the DataTable and gets each header
        /// into a list to return.
        /// </summary>
        /// <returns></returns>
        public List<string> GetHeaders()
        {
            return mainWindow.GetHeaders();
        }
        #endregion
    }
}
