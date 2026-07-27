using System;
using System.Data;
using DALayer.Common;

namespace BLLayer.Common
{
    public static class CommonBllFunction
    {
        // Retrieves list data for ComboBoxes from the database using UserID
        public static DataTable RetrieveListForComboBox(string spName, int userId)
        {
            DataTable dataTable = null;
            dataTable = SqlHelper.retrieveDataTableBySpNameAndUserId(spName, userId);
            return dataTable;
        }

        // Retrieves list data for ComboBoxes from the database without UserID
        public static DataTable RetrieveListForComboBox(string spName)
        {
            DataTable dataTable = null;
            dataTable = SqlHelper.retriveDataTableBySpName(spName);
            return dataTable;
        }

        // Retrieves all table data for GridView display without UserID
        public static DataTable RetrieveDataForGridView(string spName)
        {
            DataTable dataTable = null;
            dataTable = SqlHelper.retriveDataTableBySpName(spName);
            return dataTable;
        }

        // Retrieves all table data for GridView display with UserID
        public static DataTable RetrieveDataForGridView(string spName, int userId)
        {
            DataTable dataTable = null;
            dataTable = SqlHelper.retrieveDataTableBySpNameAndUserId(spName, userId);
            return dataTable;
        }

        // Filters and retrieves data by Status ID (e.g., Paid/Unpaid)
        public static DataTable RetrieveFilteredDataByStatus(string spName, int userId, string paramName, int filterId)
        {
            DataTable dataTable = new DataTable();
            dataTable = SqlHelper.retriveDataByUserIdAndFilterIdAtDal(spName, userId, paramName, filterId);
            return dataTable;
        }

        // Filters and retrieves data by a range of two decimal values (e.g., Min and Max Amount)
        public static DataTable RetrieveDataByUserIdAndFilterId(string spName, int userId, string paramName1, decimal paramId1, string paramName2, decimal paramId2)
        {
            DataTable dataTable = new DataTable();
            dataTable = SqlHelper.retriveDataByUserIdAndFilterIdAtDal(spName, userId, paramName1, paramId1, paramName2, paramId2);
            return dataTable;
        }

        // Filters and retrieves data by a date range (e.g., Start Date and End Date)
        public static DataTable RetrieveDataByUserIdAndFilterId(string spName, int userId, string paramName1, DateTime paramId1, string paramName2, DateTime paramId2)
        {
            DataTable dataTable = new DataTable();
            dataTable = SqlHelper.retriveDataByUserIdAndFilterIdAtDal(spName, userId, paramName1, paramId1, paramName2, paramId2);
            return dataTable;
        }
    }
}
