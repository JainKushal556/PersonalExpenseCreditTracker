using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace BLLayer.Common
{
    public static class CommonValidator
    {
        // Stores all possible validation results
        public enum ValidationResult
        {
            Success,

            PersonInvalid,
            PaymentInvalid,
            StatusInvalid,
            PriorityInvalid,
            TaskTitleInvalid,

            AmountEmpty,
            AmountInvalid,
            AmountTooLarge,

            DeadlineInvalid,

            DescriptionInvalid,

            EmailInvalid,
            PhoneInvalid,

            DateRangeInvalid,
            MinimumAmountInvalid,
            MaximumAmountInvalid,
            AmountRangeInvalid,

            CategoryInvalid,
            SubCategoryInvalid,

            StoreProcedureError,
            TaskAlreadyUpdated,

            // Profile Validation
            PhotoInvalid,
            FullNameInvalid,
            AddressInvalid,
            DateOfBirthInvalid,
            GenderInvalid
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

            return ValidationResult.MinimumAmountInvalid;
        }

        //ValidateMaximumAmount
        public static ValidationResult ValidateMaximumAmount(string maxAmount)
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
                            return ValidationResult.Success;
                        }
                    }
                }
            }

            return ValidationResult.MaximumAmountInvalid;
        }

        //ValidateAmountRange
        public static ValidationResult ValidateAmountRange(decimal minAmount, decimal maxAmount)
        {
            if (minAmount <= maxAmount)
            {
                return ValidationResult.Success;
            }


            return ValidationResult.MaximumAmountInvalid;
        }

        //Status Validation
        public static ValidationResult ValidateStatus(int statusId)
        {
            if (statusId > 0)
            {
                return ValidationResult.Success;
            }

            return ValidationResult.StatusInvalid;
        }

        //Deadline Validation

        public static ValidationResult ValidateDeadline(DateTime deadline)
        {
            if (deadline.Date >= DateTime.Today)
            {
                return ValidationResult.Success;
            }

            return ValidationResult.DeadlineInvalid;
        }

        //Description Validation

        public static ValidationResult ValidateDescription(string description)
        {
            if (!string.IsNullOrWhiteSpace(description))
            {
                description = description.Trim();

                if (description.Length >= 5)
                {
                    if (description.Length <= 150)
                    {
                        return ValidationResult.Success;
                    }
                }
            }

            return ValidationResult.DescriptionInvalid;
        }

        //Email Validation
        public static ValidationResult ValidateEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                email = email.Trim();

                if (email.Length <= 100)
                {
                    string pattern = @"^[A-Za-z0-9]+([._%+-][A-Za-z0-9]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)+$";

                    if (Regex.IsMatch(email, pattern))
                    {
                        return ValidationResult.Success;
                    }
                }
            }

            return ValidationResult.EmailInvalid;
        }
        // Phone Number Validation
        public static ValidationResult ValidatePhoneNumber(string phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                phoneNumber = phoneNumber.Trim();

                if (phoneNumber.Length == 10)
                {
                    if (Regex.IsMatch(phoneNumber, @"^[6-9][0-9]{9}$"))
                    {
                        return ValidationResult.Success;
                    }
                }
            }

            return ValidationResult.PhoneInvalid;
        }

        // Date Range Validation
        public static ValidationResult ValidateDateRange(DateTime fromDate, DateTime toDate)
        {
            if (fromDate != DateTime.MinValue)
            {
                if (toDate != DateTime.MinValue)
                {
                    if (fromDate.Date <= toDate.Date)
                    {
                        return ValidationResult.Success;
                    }
                }
            }

            return ValidationResult.DateRangeInvalid;
        }

        // Category Validation
        public static ValidationResult ValidateCategory(int categoryId)
        {
            if (categoryId > 0)
            {
                return ValidationResult.Success;
            }

            return ValidationResult.CategoryInvalid;
        }

        // SubCategory Validation
        public static ValidationResult ValidateSubCategory(int subCategoryId)
        {
            if (subCategoryId > 0)
            {
                return ValidationResult.Success;
            }

            return ValidationResult.SubCategoryInvalid;
        }

        // Task Title Validation
        public static ValidationResult ValidateTaskTitle(string taskTitle)
        {
            if (!string.IsNullOrWhiteSpace(taskTitle))
            {
                taskTitle = taskTitle.Trim();

                if (taskTitle.Length >= 3)
                {
                    if (taskTitle.Length <= 150)
                    {
                        return ValidationResult.Success;
                    }
                }
            }

            return ValidationResult.TaskTitleInvalid;
        }

        // Priority Validation
        public static ValidationResult ValidatePriority(int priorityId)
        {
            if (priorityId > 0)
            {
                return ValidationResult.Success;
            }

            return ValidationResult.PriorityInvalid;
        }

        //profile

        public static ValidationResult ValidatePhotoData(byte[] photoData)
        {
            if (photoData == null)
                return ValidationResult.PhotoInvalid;

            if (photoData.Length > 2 * 1024 * 1024)
                return ValidationResult.PhotoInvalid;

            return ValidationResult.Success;
        }

        public static ValidationResult ValidateFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return ValidationResult.FullNameInvalid;

            if (fullName.Trim().Length > 100)
                return ValidationResult.FullNameInvalid;

            return ValidationResult.Success;
        }
        public static ValidationResult ValidateAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return ValidationResult.AddressInvalid;

            if (address.Trim().Length > 200)
                return ValidationResult.AddressInvalid;

            return ValidationResult.Success;
        }
       
        public static ValidationResult ValidateDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth > DateTime.Today)
                return ValidationResult.DateOfBirthInvalid;

            return ValidationResult.Success;
        }
        // Gender Validation
        public static ValidationResult ValidateGender(int genderId)
        {
            if (genderId > 0)
            {
                return ValidationResult.Success;
            }

            return ValidationResult.GenderInvalid;
        }

    }
}
