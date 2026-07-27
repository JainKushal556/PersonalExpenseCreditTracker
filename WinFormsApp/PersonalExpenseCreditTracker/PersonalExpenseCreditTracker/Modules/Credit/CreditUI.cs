using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Credit;
using BLLayer.Common;
namespace PersonalExpenseCreditTracker.Modules.Credit
{
    public class CreditUI
    {
        public int userId { get; set; }
        public int creditId { get; set; }
        public int categoryId { get; set; }
        public int subCategoryId { get; set; }
        public string amount { get; set; }
        public string description { get; set; }
        public int paymentId { get; set; }


        // Create an object of the Business Logic Layer
        private CreditBLL creditBll = new CreditBLL();

        // Pass the data from the UI layer to the Business Logic Layer
        public CommonValidator.ValidationResult InsertDataIntoCreditUi()
        {
            creditBll.userId = userId;
            creditBll.creditId = creditId;
            creditBll.categoryId = categoryId;
            creditBll.subCategoryId = subCategoryId;
            creditBll.paymentId = paymentId;
            creditBll.amount = amount;
            creditBll.description = description;

            // Call the BLL method for validation
             return creditBll.DataValidatorIntoCreditBll();
        }
    }
}
