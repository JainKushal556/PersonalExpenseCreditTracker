using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DALayer.Borrow;
using BLLayer.Common;
namespace BLLayer.Borrow
{
    public class BorrowBLL
    {
        public int userId { get; set; }
        public int lentId { get; set; }
        public int personId { get; set; }
        public int paymentId { get; set; }
        public int statusId { get; set; }
        public string amount { get; set; }
        public DateTime deadlineAt { get; set; }
        public string description { get; set; }
        private BorrowDAL borrowDal = new BorrowDAL();
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



            borrowDal.userId = userId;
            borrowDal.lentId = lentId;
            borrowDal.personId = personId;
            borrowDal.paymentId = paymentId;
            borrowDal.statusId = statusId;
            borrowDal.amount = amount;
            borrowDal.deadlineAt = deadlineAt;
            borrowDal.description = description;

            if (borrowDal.SaveLentToDb())
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
