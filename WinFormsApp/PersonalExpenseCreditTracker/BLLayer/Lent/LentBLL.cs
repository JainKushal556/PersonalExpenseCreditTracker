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

        // Add these for Return Lent
        public string returnAmount { get; set; }
        public DateTime returnDate { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }

        private LentDAL lentDal = new LentDAL();

        // Stores the validation result
        CommonValidator.ValidationResult result;

        public CommonValidator.ValidationResult DateValidatorIntoLentBll()
        {
            //  Date Validation
            result = CommonValidator.ValidateDateRange(fromDate, toDate);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            return CommonValidator.ValidationResult.Success;
        }

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


        // Validates all user input before returning the lent amount
        public CommonValidator.ValidationResult DataValidatorIntoReturnLentBll()
        {
            // Return Amount Validation
            result = CommonValidator.ValidateAmount(returnAmount);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            // Payment Validation
            result = CommonValidator.ValidatePayment(paymentId);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            // Return Date Validation
            result = CommonValidator.ValidateDeadline(returnDate);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            // Description Validation
            result = CommonValidator.ValidateDescription(description);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            // Pass data to DAL
            lentDal.userId = userId;
            lentDal.lentId = lentId;
            lentDal.paymentId = paymentId;
            lentDal.returnAmount = returnAmount;
            lentDal.returnDate = returnDate;
            lentDal.description = description;

            if (lentDal.ReturnLent())
            {
                return CommonValidator.ValidationResult.Success;
            }
            else
            {
                return CommonValidator.ValidationResult.StoreProcedureError;
            }
        }
    }
}
