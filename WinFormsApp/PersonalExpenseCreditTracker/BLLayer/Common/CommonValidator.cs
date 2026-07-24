using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace BLLayer.Common
{
    public static class CommonValidator
    {

        public enum ValidationResult
        {
            Success,

            PersonInvalid,
            PaymentInvalid,
            StatusInvalid,

            AmountEmpty,
            AmountInvalid,
            AmountTooLarge,

            DeadlineInvalid,

            DescriptionInvalid
        }

        
        //Validation PersonID
        public static ValidationResult ValidatePerson(int personId)
        {
            if (personId >0)
            {
                return ValidationResult.Success;
            }

            return ValidationResult.PersonInvalid;
        }

        //Payment Validation
        public static ValidationResult ValidatePayment(int paymentId)
        {
            if (paymentId > 0)
            {
                return ValidationResult.Success;
            }

            return ValidationResult.PaymentInvalid;
        }

        //Amount Validation
        public static ValidationResult ValidateAmount(string amount)
        {
            decimal value;

            if (string.IsNullOrWhiteSpace(amount))
            {
                return ValidationResult.AmountEmpty;
            }

            if (!decimal.TryParse(amount, out value))
            {
                return ValidationResult.AmountInvalid;
            }

            if (value > 999999999)
            {
                return ValidationResult.AmountTooLarge;
            }

            if (value <= 0)
            {
                return ValidationResult.AmountInvalid;
            }

            return ValidationResult.Success;
        }

        //ValidateMinimumAmount
        public static ValidationResult ValidateMinimumAmount(string minAmount)
        {
            decimal value;

            if (!string.IsNullOrWhiteSpace(minAmount))
            {
                if (decimal.TryParse(minAmount, out value))
                {
                    if (value >= 0)
                    {
                        if (value <= 999999999)
                        {
                            return ValidationResult.Success;
                        }
                    }
                }
            }

            return ValidationResult.PaymentInvalid;
        }

        //ValidateMaximumAmount
        public static bool ValidateMaximumAmount(string maxAmount)
        {
            decimal value;

            if (!string.IsNullOrWhiteSpace(maxAmount))
            {
                if (decimal.TryParse(maxAmount, out value))
                {
                    if (value >= 0)
                    {
                        if (value <= 999999999)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        //ValidateAmountRange
        public static bool ValidateAmountRange(decimal minAmount, decimal maxAmount)
        {
            if (minAmount <= maxAmount)
            {
                return true;
            }

            return false;
        }

        //Status Validation
        public static bool ValidateStatus(int statusId)
        {
            if (statusId > 0)
            {
                return true;
            }

            return false;
        }

        //Deadline Validation

        public static bool ValidateDeadline(DateTime deadline)
        {
            if (deadline.Date < DateTime.Today)
            {
                return false;
            }

            return true;
        }

        //Description Validation

        public static bool ValidateDescription(string description)
        {
            if (!string.IsNullOrWhiteSpace(description))
            {
                if (description.Trim().Length >= 5)
                {
                    if (description.Length <= 150)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //Email Validation
        public static bool ValidateEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                email = email.Trim();

                if (email.Length <= 100)
                {
                    string pattern = @"^[A-Za-z0-9]+([._%+-][A-Za-z0-9]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)+$";

                    if (Regex.IsMatch(email, pattern))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        // Phone Number Validation
        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                phoneNumber = phoneNumber.Trim();

                if (phoneNumber.Length == 10)
                {
                    if (Regex.IsMatch(phoneNumber, @"^[6-9][0-9]{9}$"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // Date Range Validation
        public static bool ValidateDateRange(DateTime fromDate, DateTime toDate)
        {
            if (fromDate != DateTime.MinValue)
            {
                if (toDate != DateTime.MinValue)
                {
                    if (fromDate.Date <= toDate.Date)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


    }
}
