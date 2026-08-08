using System;
using DALayer.Credit;
using BLLayer.Common;

namespace BLLayer.Credit
{
    public class CreditBLL
    {
        public int userId { get; set; }
        public int creditId { get; set; }
        public int categoryId { get; set; }
        public int subCategoryId { get; set; }
        public string amount { get; set; }
        public string description { get; set; }
        public int paymentId { get; set; }

        private CreditDAL creditDal = new CreditDAL();

        CommonValidator.ValidationResult result;

        public CommonValidator.ValidationResult DataValidatorIntoCreditBll()
        {
            // Category Validation
            result = CommonValidator.ValidateCategory(categoryId);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            // SubCategory Validation
            result = CommonValidator.ValidateSubCategory(subCategoryId);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            // Amount Validation
            result = CommonValidator.ValidateAmount(amount);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            // Payment Validation
            result = CommonValidator.ValidatePayment(paymentId);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            // Description Validation
            result = CommonValidator.ValidateDescription(description);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            // DAL Property Assign
            creditDal.userId = userId;
            creditDal.creditId = creditId;
            creditDal.categoryId = categoryId;
            creditDal.subCategoryId = subCategoryId;
            creditDal.paymentId = paymentId;
            creditDal.amount = amount;
            creditDal.description = description;

            // Save
            if (creditDal.SaveCreditToDb())
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