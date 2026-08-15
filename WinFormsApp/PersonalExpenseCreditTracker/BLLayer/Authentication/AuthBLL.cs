using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DALayer.Authentication;
using System.Text.RegularExpressions;
//using DALayer.Authentication;
using BLLayer.Common;
using System.Data;

namespace BLLayer.Authentication
{
    public class AuthBLL
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string oldPassword { get; set; }
        public string newPassword {get;set;}

        private AuthDAL authDAL = new AuthDAL();

        public enum PasswordStrengthLevel
        {
            Weak,
            Medium,
            Strong,
            VeryStrong
        }

        CommonValidator.ValidationResult result;


        public CommonValidator.ValidationResult RegistrationFormDataIntoAuthBLL()
        {
            // User Name Validation
            result = CommonValidator.ValidationPersonName(userName);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            // Email Validation
            result = CommonValidator.ValidateEmail(email);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            // Phone Number Validation
            result = CommonValidator.ValidatePhoneNumber(phoneNumber);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            // Password Validation
            result = ValidatePassword(newPassword, confirmPassword);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            if (PasswordStrengthLevel.Weak == GetPasswordStrength(newPassword))
                return CommonValidator.ValidationResult.WeakPassword;

            if (PasswordStrengthLevel.Medium == GetPasswordStrength(newPassword))
                return CommonValidator.ValidationResult.MediumPassword;

            if (PasswordStrengthLevel.Strong == GetPasswordStrength(newPassword))
                return CommonValidator.ValidationResult.StrongPassword;
            

            authDAL.userName = userName;
            authDAL.newPassword = newPassword;
            authDAL.email = email;
            authDAL.phoneNumber = phoneNumber;


            if (authDAL.RegistrationFormDataIntoAuthDAL())
                return CommonValidator.ValidationResult.Success;
            else
                return CommonValidator.ValidationResult.StoreProcedureError;
        }

        // Validation Password
        public static CommonValidator.ValidationResult ValidatePassword(string NewPassword, string ConfirmPassword)
        {
            if (string.IsNullOrWhiteSpace(NewPassword))
            {
                return CommonValidator.ValidationResult.NewPasswordEmpty;
            }
            else if (string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                return CommonValidator.ValidationResult.ConfirmPasswordEmpty;
            }
            else if (NewPassword != ConfirmPassword)
            {
                return CommonValidator.ValidationResult.NotMatchPassword;
            }
            else
            {
                return CommonValidator.ValidationResult.Success;
            }
        }

        public PasswordStrengthLevel GetPasswordStrength(string password)
        {
            int score = 0;

            if (password.Length >= 8)
                score++;

            if (Regex.IsMatch(password, "[A-Z]"))
                score++;

            if (Regex.IsMatch(password, "[a-z]"))
                score++;

            if (Regex.IsMatch(password, "[0-9]"))
                score++;

            if (Regex.IsMatch(password, "[^a-zA-Z0-9]"))
                score++;

            if (score <= 2)
                return PasswordStrengthLevel.Weak;
            else if (score == 3)
                return PasswordStrengthLevel.Medium;
            else if (score == 4)
                return PasswordStrengthLevel.Strong;
            else
                return PasswordStrengthLevel.VeryStrong;
        }

        public string GetError()
        {
            return authDAL.GetErrorMsgForRegistrationForm();
        }

















        //private bool ValidUserName()
        //{
        //    if (string.IsNullOrWhiteSpace(userName))
        //    {
        //        return false;
        //    }
        //    if (userName.Length > 50)
        //    {
        //        return false;
        //    }
        //    return Regex.IsMatch(userName, @"^[a-zA-Z0-9]+$");

        //}
        //private bool ValidEmail()
        //{
        //    if (string.IsNullOrWhiteSpace(email))
        //    {
        //        return false;
        //    }
        //    if (email.Length > 100)
        //    {
        //        return false;
        //    }

        //    return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        //}

        //private bool ValidPhoneNumber()
        //{
        //    if (string.IsNullOrWhiteSpace(phoneNumber))
        //    {
        //        return false;
        //    }

        //    return Regex.IsMatch(phoneNumber, @"^\d{10}$");
        //}

        //private bool ValidPassword()
        //{
        //    if (string.IsNullOrWhiteSpace(password))
        //    {
        //        return false;
        //    }
        //    if (password.Length < 6)
        //    {
        //        return false;
        //    }
        //    return Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).+$");
        //}

        //private bool ValidOldPassword()
        //{
        //    if(string.IsNullOrWhiteSpace(oldPassword))
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        //private bool ValidNewPassword()
        //{
        //    if(string.IsNullOrWhiteSpace(newPassword))
        //    {
        //        return false;
        //    }
        //    if(newPassword.Length<6)
        //    {
        //        return false;
        //    }
        //    if(newPassword==oldPassword)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        ////Register Page
        //public bool InsertDataIntoAuthBll()
        //{
        //    if (ValidUserName())
        //    {
        //        if (ValidEmail())
        //        {
        //            if (ValidPhoneNumber())
        //            {
        //                if (ValidPassword())
        //                {
        //                    return true;
        //                }
        //            }
        //        }
        //    }
        //    return false;
        //}

        ////Login Page
        //public bool LoginDataIntoAuthBll()
        //{
        //    if (ValidEmail())
        //    {
        //        if (ValidPassword())
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        ////Forget Password
        //public bool ForgetPasswordIntoAuthBll()
        //{
        //    if (ValidEmail())
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        ////Change Password
        //public bool ChangePasswordIntoAuthBll()
        //{
        //    if (ValidOldPassword())
        //    {
        //        if (ValidNewPassword())
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

    }
}
