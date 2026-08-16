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


        public int GetUserIdFromDB()
        {
            return authDAL.GetUserIdFromDB();
        }

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

        public CommonValidator.ValidationResult LoginDataIntoAuthBLL()
        {
            // Email Validation
            result = CommonValidator.ValidateEmail(email);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            // Password Validation
            if (string.IsNullOrWhiteSpace(password))
                return CommonValidator.ValidationResult.NewPasswordEmpty;

            authDAL.password = password;
            authDAL.email = email;

            if (authDAL.LoginDataIntoAuthDAL())
                return CommonValidator.ValidationResult.Success;
            else
                return CommonValidator.ValidationResult.StoreProcedureError;
        }

        public CommonValidator.ValidationResult ForgotPasswordDataIntoAuthBLL()
        {
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


            authDAL.newPassword = newPassword;
            authDAL.email = email;
            authDAL.phoneNumber = phoneNumber;


            if (authDAL.ForgotPasswordDataIntoAuthDAL())
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

        public string GetErrorForLogin()
        {
            return authDAL.GetErrorMsgForLogin();
        }

        public string GetErrorMsgForForgotPassword()
        {
            return authDAL.GetErrorMsgForForgotPassword();
        }
    }
}
