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
            result = CommonValidator.ValidateStatus(statusId);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

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

          
            LentDAL lentDal = new LentDAL();
            lentDal.userId = userId;
            lentDal.lentId = lentId;
            lentDal.personId = personId;
            lentDal.paymentId = paymentId;
            lentDal.statusId = statusId;
            lentDal.amount = amount;
            lentDal.deadlineAt = deadlineAt;
            lentDal.description = description;

            return CommonValidator.ValidationResult.Success;
        }

        // Retrieves ComboBox data from the DAL using a stored procedure with UserId
        public static List<string> retriveListForComboBoxAtBal(string spName, string colName, int userId)
        {
            return SqlHelper.retriveListForComboBoxAtDal(spName,colName, userId);
        }

        // Retrieves ComboBox data from the DAL using a stored procedure without UserId
        public static List<string> retriveListForComboBoxAtBal(string spName, string colName)
        {
            return SqlHelper.retriveListForComboBoxAtDal(spName, colName);
        }

    }
}
