using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Mainform;
using BLLayer.Common;
namespace PersonalExpenseCreditTracker.Forms.Main
{
   public class MainUI
    {
       public string minAmount { get; set; }
       public string maxAmount { get; set; }

       public DateTime fromDate { get; set; }
       public DateTime toDate { get; set; }

       // Create an object of the Business Logic Layer
       private MainformBLL mainFormBLL = new MainformBLL();
       // Pass the data from the UI layer to the Business Logic Layer
        public CommonValidator.ValidationResult  InsertDateDataIntoMainUi()
        {
           

            mainFormBLL.fromDate = fromDate;
            mainFormBLL.toDate = toDate;

            // Call the BLL method for validation
            return mainFormBLL.DateValidatorIntoMainBll();
        }
        public CommonValidator.ValidationResult InsertAmountDataIntoMainUi()
        {
            mainFormBLL.minAmount = minAmount;
            mainFormBLL.maxAmount = maxAmount;

            // Call the BLL method for validation
            return mainFormBLL.AmountValidatorIntoMainBll();
        }
           
    }
}
