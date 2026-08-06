using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Expense;
using BLLayer.Common;
namespace PersonalExpenseCreditTracker.Modules.Expense
{
   public class ExpenseUI
    {
        public int userId { get; set; }
        public int expenseId { get; set; }
        public int categoryId { get; set; }
        public int subCategoryId { get; set; }
        public string amount { get; set; }
        public string description { get; set; }
        public int paymentId { get; set; }


        // Create an object of the Business Logic Layer
        private ExpenseBLL expenseBll = new ExpenseBLL();

        // Pass the data from the UI layer to the Business Logic Layer
        public CommonValidator.ValidationResult InsertDataIntoExpenseUi()
        {
            expenseBll.userId = userId;
            expenseBll.expenseId = expenseId;
            expenseBll.categoryId = categoryId;
            expenseBll.subCategoryId = subCategoryId;
            expenseBll.paymentId = paymentId;
            expenseBll.amount = amount;
            expenseBll.description = description;

            // Call the BLL method for validation
            return expenseBll.DataValidatorIntoExpenseBll();
        }

    }
}
