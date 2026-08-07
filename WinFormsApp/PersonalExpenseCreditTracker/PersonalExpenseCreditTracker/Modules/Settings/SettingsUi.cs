using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLLayer.Common;
using System.Data;
using BLLayer.Settings;

namespace PersonalExpenseCreditTracker.Modules.Settings
{
    class SettingsUi
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        private SettingsBLL settingsBLL = new SettingsBLL();

        public CommonValidator.ValidationResult ChangePasswordDataIntoSettingsUi()
        {
            settingsBLL.UserId = UserId;
            settingsBLL.CurrentPassword = CurrentPassword;
            settingsBLL.NewPassword = NewPassword;
            settingsBLL.ConfirmPassword = ConfirmPassword;

            return settingsBLL.DataValidatorIntoChangePasswordBll();
        }


    }
}
