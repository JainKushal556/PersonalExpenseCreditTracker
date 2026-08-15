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

        // Retrieves list data for ComboBoxes from the database using UserID
        public static DataTable RetrieveListForComboBox(string spName, string paramName,int paramValue)
        {
            DataTable dataTable = null;
            dataTable = SqlHelper.retriveDataByAndFilterIdAtDal(spName, paramName, paramValue);
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

        // Filters and retrieves data by a range of two int values (e.g., Min and Max Amount)
        public static DataTable RetrieveDataByUserIdAndFilterId(string spName, int userId, string paramName1, int paramId1, string paramName2, int paramId2)
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

        public static DataTable RetrieveErrorCategoryDataIntoCategory(string spName, int userId, int paramId1, int paramId2, string paramName, string paramName1, string paramName2, string paramName3)
        {
            DataTable dataTable = new DataTable();
            dataTable = SqlHelper.GetErrorCategoryDataIntoCategoryDB(spName, userId, paramId1, paramId2, paramName, paramName1, paramName2, paramName3);
            return dataTable;
        }

        public static DataTable RetrieveErrorCategoryDataIntoCategory(string spName, int userId, int paramId1, string paramName, string paramName1, string paramName2)
        {
            DataTable dataTable = new DataTable();
            dataTable = SqlHelper.GetErrorCategoryDataIntoCategoryDB(spName, userId, paramId1, paramName, paramName1, paramName2);
            return dataTable;
        }


        public static void UpdateOverdueStatus()
        {
            SqlHelper.UpdateOverdueStatus();
        }
    }
}
