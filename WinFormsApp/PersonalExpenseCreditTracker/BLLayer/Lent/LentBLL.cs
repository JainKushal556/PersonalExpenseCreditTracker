using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Common;
using System.Data;
using DALayer.Common;
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

        CommonValidator.ValidationResult result;

        public CommonValidator.ValidationResult InsertDataIntoLentBll()
        {
            //Validation PersonID
            result = CommonValidator.ValidatePerson(personId);

            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }


            //Payment Validation
            result = CommonValidator.ValidatePayment(paymentId);
            if (result!=CommonValidator.ValidatePayment(paymentId))
            {
                return result;
            }

            ////Status Validation
            //if (CommonValidator.ValidateStatus(statusId))
            //{
            //    return false;
            //}

            ////Amount Validation
            //if (CommonValidator.ValidateAmount(amount.ToString()))
            //{
            //    return false;
            //}

            ////Deadline Validation
            //if (CommonValidator.ValidateDeadline(deadlineAt))
            //{
            //    return false;
            //}

            ////Description Validation
            //if (CommonValidator.ValidateDescription(description))
            //{
            //    return false;
            //}

           //DLRLayer function
            return CommonValidator.ValidationResult.Success;
        }

        public static List<string> retriveListForComboBoxAtBal(string spName, string colName, int userId)
        {
            return SqlHelper.retriveListForComboBoxAtDal(spName,colName, userId);
        }

       

    }
}
