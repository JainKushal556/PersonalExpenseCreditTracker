using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Common;
using System.Data;
using DALayer.Common;
using DALayer.Lent;
namespace BLLayer.Lent
{
    public class LentBLL
    {
        public int userId { get; set; }
        public int lentId { get; set; }
        public int personId { get; set; }
        public int paymentId { get; set; }
        public int statusId { get; set; }
        public string amount { get; set; }
        public DateTime deadlineAt { get; set; }
        public string description { get; set; }
        private LentDAL lentDal = new LentDAL();
        // Stores the validation result
        CommonValidator.ValidationResult result;

        // Validates all user input before saving the data
        public CommonValidator.ValidationResult DataValidatorIntoLentBll()
        {
            //  Person Validation
            result = CommonValidator.ValidatePerson(personId);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            //Payment Validation
            result = CommonValidator.ValidatePayment(paymentId);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            // Status Validation
            //result = CommonValidator.ValidateStatus(statusId);
            //if (result != CommonValidator.ValidationResult.Success)
            //{
            //    return result;
            //}

            //Amount Validation
            result = CommonValidator.ValidateAmount(amount);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            //Deadline Validation
            result = CommonValidator.ValidateDeadline(deadlineAt);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            //Description Validation
            result = CommonValidator.ValidateDescription(description);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

          
            
            lentDal.userId = userId;
            lentDal.lentId = lentId;
            lentDal.personId = personId;
            lentDal.paymentId = paymentId;
            lentDal.statusId = statusId;
            lentDal.amount = amount;
            lentDal.deadlineAt = deadlineAt;
            lentDal.description = description;

            if (lentDal.SaveLentToDb())
            {
                return CommonValidator.ValidationResult.Success;
            }
            else
            {
                return CommonValidator.ValidationResult.StoreProcedureError;
            }
            
        }

        // Retrieves ComboBox data from the DAL using a stored procedure with UserId
        public static DataTable retriveListForComboBoxAtBll(string spName, int userId)
        {
            DataTable dataTable = null;
            dataTable = SqlHelper.retrieveDataTableBySpNameAndUserId(spName, userId);
            return dataTable;
        }

        // Retrieves ComboBox data from the DAL using a stored procedure without UserId
        public static DataTable retriveListForComboBoxAtBll(string spName)
        {
            DataTable dataTable = null;
            dataTable = SqlHelper.retriveDataTableBySpName(spName);
            return dataTable;
        }
        // Retrieves GirdView data from the DAL using a stored procedure without UserId
        public static DataTable retriveDataForGridViewAtBll(string spName)
        {
            DataTable dataTable = null;
            dataTable = SqlHelper.retriveDataTableBySpName(spName);
            return dataTable;
        }
        // Retrieves GirdView data from the DAL using a stored procedure with UserId
        public static DataTable retriveDataForGridViewAtBll(string spName,int userId)
        {
            DataTable dataTable = null;
            dataTable = SqlHelper.retrieveDataTableBySpNameAndUserId(spName, userId);
            return dataTable;
        }

        public static DataTable retriveFilteredDataByStatusAtBll(string spName, int userId,string paramName,int filterId)
        {
            DataTable dataTable = new DataTable();
            dataTable = SqlHelper.retriveDataByUserIdAndFilterIdAtDal(spName, userId, paramName, filterId);
            return dataTable;
        }

        public static DataTable retriveDataByUserIdAndFilterIdAtBll(string spName, int userId, string paramName1, int paramId1, string paramName2, int paramId2)
        {
            DataTable dataTable = new DataTable();
            dataTable = SqlHelper.retriveDataByUserIdAndFilterIdAtDal(spName,userId,paramName1,paramId1,paramName2,paramId2);
            return dataTable;
        }
        public static DataTable retriveDataByUserIdAndFilterIdAtBll(string spName, int userId, string paramName1, DateTime paramId1, string paramName2, DateTime paramId2)
        {
            DataTable dataTable = new DataTable();
            dataTable = SqlHelper.retriveDataByUserIdAndFilterIdAtDal(spName, userId, paramName1, paramId1, paramName2, paramId2);
            return dataTable;
        }
    }
}
