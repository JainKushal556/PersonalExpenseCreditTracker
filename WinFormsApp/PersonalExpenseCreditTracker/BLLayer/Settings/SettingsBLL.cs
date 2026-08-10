using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Common;
using DALayer.Settings;
using System.Data;
using DALayer.Common;
using System.Text.RegularExpressions;

namespace BLLayer.Settings
{
    public class SettingsBLL
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        public enum PasswordStrengthLevel
        {
            Weak,
            Medium,
            Strong,
            VeryStrong
        }

        CommonValidator.ValidationResult result;

        private SettingsDAL settingsDAL = new SettingsDAL();

        public CommonValidator.ValidationResult DataValidatorIntoChangePasswordBll()
        {
            // Current Password Validation
            result = CommonValidator.ValidatePassword(CurrentPassword, NewPassword, ConfirmPassword);
            if (result != CommonValidator.ValidationResult.Success)
                return result;

            settingsDAL.UserId = UserId;
            settingsDAL.CurrentPassword = CurrentPassword;
            settingsDAL.NewPassword = NewPassword;

            if (SettingsBLL.PasswordStrengthLevel.Weak == GetPasswordStrength(NewPassword))
                return CommonValidator.ValidationResult.WeakPassword;

            if (SettingsBLL.PasswordStrengthLevel.Medium == GetPasswordStrength(NewPassword))
                return CommonValidator.ValidationResult.MediumPassword;

            if (SettingsBLL.PasswordStrengthLevel.Strong == GetPasswordStrength(NewPassword))
                return CommonValidator.ValidationResult.StrongPassword;

            if (settingsDAL.ChangePasswordDataIntoSettingsDB())
                return CommonValidator.ValidationResult.Success;
            else
                return CommonValidator.ValidationResult.StoreProcedureError;
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

        // 👉 Logout BLL Logic
        public CommonValidator.ValidationResult LogoutUserIntoSettingsBll()
        {
            if (UserId <= 0)
            {
                return CommonValidator.ValidationResult.StoreProcedureError;
            }

            settingsDAL.UserId = UserId;

            if (settingsDAL.LogoutUserFromDb())
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
