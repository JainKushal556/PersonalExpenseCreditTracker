using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Common;
using DALayer.Expense;
namespace BLLayer.Expense
{
   public  class ExpenseBLL
    {

        public int userId { get; set; }
        public int expenseId { get; set; }
        public int categoryId { get; set; }
        public int subCategoryId { get; set; }
        public string amount { get; set; }
        public string description { get; set; }
        public int paymentId { get; set; }

        private ExpenseDAL expenseDal = new ExpenseDAL();

        CommonValidator.ValidationResult result;

        public CommonValidator.ValidationResult DataValidatorIntoExpenseBll()
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
            expenseDal.userId = userId;
            expenseDal.expenseId = expenseId;
            expenseDal.categoryId = categoryId;
            expenseDal.subCategoryId = subCategoryId;
            expenseDal.paymentId = paymentId;
            expenseDal.amount =amount;
            expenseDal.description = description;

            // Save
            if (expenseDal.SaveExpenseToDb())
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
