using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLLayer.Common;
using System.Data;

namespace PersonalExpenseCreditTracker.Common
{
    public class CommonUiFunction
    {
        // Helper method to load a ComboBox with data (with UserID)
        public static void LoadInComboBox(string spName, int userId, string initialText, ComboBox comboBox)
        {
            DataTable dataTable = RetrieveListForComboBox(spName, userId);
            DataRow dataRow = dataTable.NewRow();
            dataRow[0] = 0;
            dataRow[1] = initialText;
            dataTable.Rows.InsertAt(dataRow, 0);

            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = dataTable.Columns[1].ColumnName;
            comboBox.ValueMember = dataTable.Columns[0].ColumnName;
            comboBox.SelectedIndex = 0;
        }

        // Helper method to load a ComboBox with data (without UserID)
        public static void LoadInComboBox(string spName, string initialText, ComboBox comboBox)
        {            
            DataTable dataTable = RetrieveListForComboBox(spName);
            DataRow dataRow = dataTable.NewRow();
            dataRow[0] = 0;
            dataRow[1] = initialText;
            dataTable.Rows.InsertAt(dataRow, 0);

            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = dataTable.Columns[1].ColumnName;
            comboBox.ValueMember = dataTable.Columns[0].ColumnName;
            comboBox.SelectedIndex = 0;
        }

        // Retrieves list data for ComboBoxes from BLL layer (with UserID)
        public static DataTable RetrieveListForComboBox(string spName, int userId)
        {
            DataTable dataTable = null;
            dataTable = CommonBllFunction.RetrieveListForComboBox(spName, userId);
            return dataTable;
        }

        // Retrieves list data for ComboBoxes from BLL layer (without UserID)
        public static DataTable RetrieveListForComboBox(string spName)
        {
            DataTable dataTable = null;
            dataTable = CommonBllFunction.RetrieveListForComboBox(spName);
            return dataTable;
        }

        // Retrieves filtered data by Status ID from BLL layer
        public static DataTable RetrieveFilteredDataByStatus(string spName, int userid, string paramName, int filterId)
        {
            DataTable dataTable = new DataTable();
            dataTable = CommonBllFunction.RetrieveFilteredDataByStatus(spName, userid, paramName, filterId);
            return dataTable;
        }

        // Retrieves table data for GridView display from BLL layer (without UserID)
        public static DataTable RetrieveDataForGridView(string spName)
        {
            DataTable dataTable = null;
            dataTable = CommonBllFunction.RetrieveDataForGridView(spName);
            return dataTable;
        }

        // Retrieves table data for GridView display from BLL layer (with UserID)
        public static DataTable RetrieveDataForGridView(string spName, int userId)
        {
            DataTable dataTable = null;
            dataTable = CommonBllFunction.RetrieveDataForGridView(spName, userId);
            return dataTable;
        }

        // Retrieves filtered data by a range of two decimal values from BLL layer
        public static DataTable RetrieveDataByUserIdAndFilterId(string spName, int userId, string paramName1, decimal paramId1, string paramName2, decimal paramId2)
        {
            DataTable dataTable = new DataTable();
            dataTable = CommonBllFunction.RetrieveDataByUserIdAndFilterId(spName, userId, paramName1, paramId1, paramName2, paramId2);
            return dataTable;
        }

        // Retrieves filtered data by a date range from BLL layer
        public static DataTable RetrieveDataByUserIdAndFilterId(string spName, int userId, string paramName1, DateTime paramId1, string paramName2, DateTime paramId2)
        {
            DataTable dataTable = new DataTable();
            dataTable = CommonBllFunction.RetrieveDataByUserIdAndFilterId(spName, userId, paramName1, paramId1, paramName2, paramId2);
            return dataTable;
        }
    }
}
