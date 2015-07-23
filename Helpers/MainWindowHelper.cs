using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace PMSUpload_Admin.Helpers
{
    /// <summary>
    /// An access class to use functionalities in the MainWindow.
    /// </summary>
    public static class MainWindowHelper
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
        public static List<string> GetHeaders()
        {
            return mainWindow.GetHeaders();
        }

        /// <summary>
        /// Gets the selected row's data from the column specified.
        /// </summary>
        /// <param name="header">The name of the column.</param>
        /// <returns></returns>
        public static string GetCellData(string header)
        {
            return mainWindow.GetCellData(header);
        }

        public static bool IsNewData(List<string> dataList)
        {
            return mainWindow.IsNewData(dataList);
        }
        #endregion
    }
}
