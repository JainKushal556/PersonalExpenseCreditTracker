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
            PersonNameInvalid,
            PersonNameEmpty,
            PaymentInvalid,
            StatusInvalid,
            PriorityInvalid,
            TaskTitleInvalid,
            TaskTitleAlreadyExists,
            NoteTitleInvalid,
            NoteTitleAlreadyExists,

            AmountEmpty,
            AmountInvalid,
            AmountTooLarge,

            DeadlineInvalid,
            ReturnAmountDeadlineMustBeTodayOrEarlier,

            DescriptionInvalid,
            DescriptionTooShort,
            DescriptionTooLong,

            EmailInvalid,
            PhoneInvalid,
            PhoneNumberEmpty,
            PhoneNumberAlreadyExists,

            DateRangeInvalid,
            MinimumAmountInvalid,
            MaximumAmountInvalid,
            AmountRangeInvalid,

            ColorInvalid,

            CategoryInvalid,
            SubCategoryInvalid,

            StoreProcedureError,
            TaskAlreadyUpdated,

            // Profile Validation
            PhotoInvalid,
            FullNameInvalid,
            AddressInvalid,
            DateOfBirthInvalid,
            GenderInvalid,

            //Category Validation
            InvalidCategoryName,
            CategoryNameEmpty,
            CategoryError,

            //Password Validation
            CurrentAndNewPasswordSame,
            CurrentPasswordEmpty,
            NewPasswordEmpty,
            ConfirmPasswordEmpty,
            NotMatchPassword,
            WeakPassword,
            MediumPassword,
            StrongPassword,
            VeryStrongPassword
        }

        // Validation Password
        public static ValidationResult ValidatePassword(string CurrentPassword, string NewPassword, string ConfirmPassword)
        {
            if (string.IsNullOrWhiteSpace(CurrentPassword))
            {
                return ValidationResult.CurrentPasswordEmpty;
            }
            else if (string.IsNullOrWhiteSpace(NewPassword))
            {
                return ValidationResult.NewPasswordEmpty;
            }
            else if (string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                return ValidationResult.ConfirmPasswordEmpty;
            }
            else if (NewPassword != ConfirmPassword)
            {
                return ValidationResult.NotMatchPassword;
            }
            else if (CurrentPassword == NewPassword)
            {
                return ValidationResult.CurrentAndNewPasswordSame;
            }
            else
            {
                return ValidationResult.Success;
            }
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

        // Person Name Validation
        public static ValidationResult ValidationPersonName(string personName)
        {
            if (string.IsNullOrWhiteSpace(personName))
            {
                return ValidationResult.PersonNameEmpty;
            }

            personName = personName.Trim();

            if (personName.Length < 3)
            {
                return ValidationResult.PersonNameInvalid;
            }

            if (!Regex.IsMatch(personName, @"^[A-Za-z]+(?:[ '-][A-Za-z]+)*$"))
            {
                return ValidationResult.PersonNameInvalid;
            }

            return ValidationResult.Success;
        }

        // Category Name Validation
        public static ValidationResult ValidationCategoryName(string CategoryName)
        {
            if (string.IsNullOrWhiteSpace(CategoryName))
            {
                return ValidationResult.CategoryNameEmpty;
            }

            CategoryName = CategoryName.Trim();

            if (CategoryName.Length < 3)
            {
                return ValidationResult.InvalidCategoryName;
            }

            if (!Regex.IsMatch(CategoryName, @"^[A-Za-z]+(?:[ &'/-][A-Za-z]+)*$"))
            {
                return ValidationResult.InvalidCategoryName;
            }

            return ValidationResult.Success;
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

        //Validate MinimumAmount
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

        // Validate Maximum Amount
        public static ValidationResult ValidateMaximumAmount(string maxAmount)
        {
            if (string.IsNullOrWhiteSpace(maxAmount))
            {
                return ValidationResult.MaximumAmountInvalid;
            }

            decimal value;

            if (!decimal.TryParse(maxAmount.Trim(), out value))
            {
                return ValidationResult.MaximumAmountInvalid;
            }

            if (value < 0)
            {
                return ValidationResult.MaximumAmountInvalid;
            }

            if (value > 999999999)
            {
                return ValidationResult.MaximumAmountInvalid;
            }

            return ValidationResult.Success;
        }

        //Validate AmountRange
        // Validate Amount Range
        public static ValidationResult ValidateAmountRange(decimal minAmount, decimal maxAmount)
        {
            if (minAmount <= maxAmount)
            {
                return ValidationResult.Success;
            }

            return ValidationResult.AmountRangeInvalid;
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

        public static ValidationResult ValidateDeadlineReturnAmount(DateTime deadline)
        {
     
            if (deadline == DateTime.MinValue)
            {
                return ValidationResult.DeadlineInvalid;
            }

         
            if (deadline.Date <= DateTime.Today)
            {
                return ValidationResult.Success;
            }

            return ValidationResult.ReturnAmountDeadlineMustBeTodayOrEarlier;
        }

        //Description Validation

        public static ValidationResult ValidateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return ValidationResult.DescriptionInvalid;
            }

            description = description.Trim();

            if (description.Length < 5)
            {
                return ValidationResult.DescriptionTooShort;
            }

            if (description.Length > 150)
            {
                return ValidationResult.DescriptionTooLong;
            }

            return ValidationResult.Success;
        }

        // Email Validation
        public static ValidationResult ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return ValidationResult.EmailInvalid;
            }

            email = email.Trim();

            // Uppercase letters are not allowed
            if (email != email.ToLower())
            {
                return ValidationResult.EmailInvalid;
            }

            if (email.Length > 100)
            {
                return ValidationResult.EmailInvalid;
            }

            string pattern = @"^[a-z0-9]+([._%+-][a-z0-9]+)*@[a-z0-9-]+(\.[a-z0-9-]+)+$";

            if (!Regex.IsMatch(email, pattern))
            {
                return ValidationResult.EmailInvalid;
            }

            return ValidationResult.Success;
        }
        // Phone Number Validation
        public static ValidationResult ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return ValidationResult.PhoneNumberEmpty;
            }

            phoneNumber = phoneNumber.Trim();

            if (phoneNumber.Length != 10)
            {
                return ValidationResult.PhoneInvalid;
            }

            if (!Regex.IsMatch(phoneNumber, @"^[6-9][0-9]{9}$"))
            {
                return ValidationResult.PhoneInvalid;
            }

            return ValidationResult.Success;
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

        // Photo Validation
        public static ValidationResult ValidatePhotoData(byte[] photoData)
        {
            if (photoData == null || photoData.Length == 0)
            {
                return ValidationResult.PhotoInvalid;
            }

            // Maximum file size: 2 MB
            if (photoData.Length > 2 * 1024 * 1024)
            {
                return ValidationResult.PhotoInvalid;
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidateFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return ValidationResult.FullNameInvalid;

            fullName = fullName.Trim();

            if (fullName.Length < 3 || fullName.Length > 100)
                return ValidationResult.FullNameInvalid;

            if (!Regex.IsMatch(fullName, @"^[A-Za-z]+(?:[ '-][A-Za-z]+)*$"))
                return ValidationResult.FullNameInvalid;

            return ValidationResult.Success;
        }
        // Address Validation
        public static ValidationResult ValidateAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return ValidationResult.AddressInvalid;
            }

            address = address.Trim();

            if (address.Length < 5 || address.Length > 200)
            {
                return ValidationResult.AddressInvalid;
            }

            return ValidationResult.Success;
        }

        // Date of Birth Validation
        public static ValidationResult ValidateDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth == DateTime.MinValue)
            {
                return ValidationResult.DateOfBirthInvalid;
            }

            if (dateOfBirth > DateTime.Today)
            {
                return ValidationResult.DateOfBirthInvalid;
            }

            int age = DateTime.Today.Year - dateOfBirth.Year;

            if (dateOfBirth.Date > DateTime.Today.AddYears(-age))
            {
                age--;
            }

            if (age < 5 || age > 100)
            {
                return ValidationResult.DateOfBirthInvalid;
            }

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

        // Note Title Validation
        public static ValidationResult ValidateNoteTitle(string noteTitle)
        {
            if (!string.IsNullOrWhiteSpace(noteTitle))
            {
                noteTitle = noteTitle.Trim();

                if (noteTitle.Length >= 5)
                {
                    if (noteTitle.Length <= 150)
                    {
                        return ValidationResult.Success;
                    }
                }
            }

            return ValidationResult.NoteTitleInvalid;
        }

      
        
    }
}
