using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Common;
using BLLayer.Lent;
namespace BLLayer.Mainform
{
   public class MainformBLL
    {
        public string minAmount { get; set; }
        public string maxAmount { get; set; }

        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }

        // Stores the validation result
        CommonValidator.ValidationResult result;

        // Validates all user input before saving the data
        public CommonValidator.ValidationResult DateValidatorIntoMainBll()
        {
            //  Date Validation
            result = CommonValidator.ValidateDateRange(fromDate, toDate);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            ////  MinAmount Validation
            //result = CommonValidator.ValidateMinimumAmount(minAmount);
            //if (result != CommonValidator.ValidationResult.Success)
            //{
            //    return result;
            //}

            ////  MaxAmount Validation
            //result = CommonValidator.ValidateMaximumAmount(maxAmount);
            //if (result != CommonValidator.ValidationResult.Success)
            //{
            //    return result;
            //}
            ////  AmountRange Validation
            //result = CommonValidator.ValidateAmountRange(Convert.ToDecimal(minAmount),Convert.ToDecimal(maxAmount));
            //if (result != CommonValidator.ValidationResult.Success)
            //{
            //    return result;
            //}

            return CommonValidator.ValidationResult.Success;
        }

        // Validates all user input before saving the data
        public CommonValidator.ValidationResult AmountValidatorIntoMainBll()
        {
            
            //  MinAmount Validation
            result = CommonValidator.ValidateMinimumAmount(minAmount);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            //  MaxAmount Validation
            result = CommonValidator.ValidateMaximumAmount(maxAmount);
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }
            //  AmountRange Validation
            result = CommonValidator.ValidateAmountRange(Convert.ToDecimal(minAmount), Convert.ToDecimal(maxAmount));
            if (result != CommonValidator.ValidationResult.Success)
            {
                return result;
            }

            return CommonValidator.ValidationResult.Success;
        }
    }
}
