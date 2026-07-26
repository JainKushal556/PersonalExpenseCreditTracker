using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Lent;
using BLLayer.Common;
using System.Data;
namespace PersonalExpenseCreditTracker.Modules.Lent
{
    public class LentUi
    {
        public int userId { get; set; }
        public int lentId { get; set; }
        public int personId { get; set; }
        public int paymentId { get; set; }
        public int statusId { get; set; }
        public string amount { get; set; }
        public DateTime deadlineAt { get; set; }
        public string description { get; set; }

        // Create an object of the Business Logic Layer
        private LentBLL lentBLL = new LentBLL();

        // Pass the data from the UI layer to the Business Logic Layer
        public CommonValidator.ValidationResult  InsertDataIntoLentUi()
        {
            lentBLL.userId = userId;
            lentBLL.lentId = lentId;
            lentBLL.personId = personId;
            lentBLL.paymentId = paymentId;
            lentBLL.statusId = statusId;
            lentBLL.amount = amount;
            lentBLL.deadlineAt = deadlineAt;
            lentBLL.description = description;

            // Call the BLL method for validation
            return lentBLL.DataValidatorIntoLentBll();
        }

        // Retrieve ComboBox data from the BLL (Stored Procedure with UserId)
        public static DataTable retriveListForComboBoxAtUi(string spName, int userId)
        {
            DataTable dataTable = null;
            dataTable = LentBLL.retriveListForComboBoxAtBll(spName, userId);
            return dataTable;
        }
        // Retrieve ComboBox data from the BLL (Stored Procedure without UserId)
        public static DataTable retriveListForComboBoxAtUi(string spName)
        {
            DataTable dataTable = null;
            dataTable = LentBLL.retriveListForComboBoxAtBll(spName);
            return dataTable;
        }
        public static DataTable retriveFilteredDataByStatusAtUi(string spName, int userid,string paramName, int statusId)
        {
            DataTable dataTable = new DataTable();
            dataTable = LentBLL.retriveFilteredDataByStatusAtBll(spName, userid,paramName ,statusId);
            return dataTable;
        }
        // Retrieve GridView data from the BLL (Stored Procedure without UserId)
        public static DataTable retriveDataForGridViewAtUi(string spName)
        {
            DataTable dataTable = null;
            dataTable = LentBLL.retriveDataForGridViewAtBll(spName);
            return dataTable;
        }

        // Retrieve GridView data from the BLL (Stored Procedure with UserId)
        public static DataTable retriveDataForGridViewAtUi(string spName,int userId)
        {
            DataTable dataTable = null;
            dataTable = LentBLL.retriveDataForGridViewAtBll(spName, userId);
            return dataTable;
        }
        public static DataTable retriveDataByUserIdAndFilterIdAtUi(string spName, int userId, string paramName1, int paramId1, string paramName2, int paramId2)
        {
            DataTable dataTable = new DataTable();
            dataTable = LentBLL.retriveDataByUserIdAndFilterIdAtBll(spName,userId,paramName1,paramId1,paramName2,paramId2);
            return dataTable;
        }
    }
}
