using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using BLLayer.Settings;
using BLLayer.Common;
using PersonalExpenseCreditTracker.Common;

namespace PersonalExpenseCreditTracker.Modules.Settings
{
    public partial class ChangePasswordControls : Form
    {
        private bool _PasswordMatch;
        bool isPasswordVisible1 = false;
        bool isPasswordVisible2 = true;
        bool isPasswordVisible3 = true;

        public ChangePasswordControls()
        {
            InitializeComponent();
        }

        [DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);

        // Free GDI object
        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        private void btnChangePasswordClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Radius Corner of These Panels
        private void SetRadius(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0)
                return;

            IntPtr hrgn = CreateRoundRectRgn(
                0,
                0,
                control.Width + 1,
                control.Height + 1,
                radius,
                radius);

            Region region = Region.FromHrgn(hrgn);

            if (control.Region != null)
                control.Region.Dispose();

            control.Region = region;

            DeleteObject(hrgn);
        }

        private void ChangePasswordControls_Load(object sender, EventArgs e)
        {
            SetRadius(btnChangePasswordCancel, 5);
            SetRadius(btnChangePasswordUpdatePassword, 5);
            SetRadius(pnlWeak, 10);
            SetRadius(pnlMedium, 10);
            SetRadius(pnlStrong, 10);
            SetRadius(pnlVeryStrong, 10);
            SetRadius(pnlChangePasswordAbout, 10);

            DataTable dataTable = null;
            dataTable = CommonUiFunction.RetrieveListForComboBox("spGetUserCurrentPassword", Session.LogedInUser.GetUserId());

            if (dataTable.Rows.Count > 0)
            {
                txtCurrentPassword.Text = dataTable.Rows[0]["Password"].ToString();
            }

            txtNewPassword.Text = "Enter New Password";
            txtConfirmPassword.Text = "Confirm New Password";

            txtCurrentPassword.ForeColor = Color.FromArgb(0, 0, 0);
            txtNewPassword.ForeColor = Color.FromArgb(191, 192, 199);
            txtConfirmPassword.ForeColor = Color.FromArgb(191, 192, 199);

            lblPasswordStrengthLevel.Text = "";

            pnlWeak.BackColor = Color.FromArgb(234, 235, 239);
            pnlMedium.BackColor = Color.FromArgb(234, 235, 239);
            pnlStrong.BackColor = Color.FromArgb(234, 235, 239);
            pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);

            txtCurrentPassword.UseSystemPasswordChar = true;
            txtNewPassword.UseSystemPasswordChar = false;
            txtConfirmPassword.UseSystemPasswordChar = false;
            _PasswordMatch = false;

            picEye1.Image = Properties.Resources.eye;
            picEye2.Image = Properties.Resources.open_eye__2_;
            picEye3.Image = Properties.Resources.open_eye__2_;
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtNewPassword.Text != "Enter New Password" && !string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                ErrorHelper.HideErrorForControl(txtNewPassword);
            }
            SettingsBLL settingsBLL = new SettingsBLL();

            if (txtNewPassword.Text == "" || txtNewPassword.Text == "Enter New Password")
            {
                lblPasswordStrengthLevel.Text = "";

                pnlWeak.BackColor = Color.FromArgb(234, 235, 239);
                pnlMedium.BackColor = Color.FromArgb(234, 235, 239);
                pnlStrong.BackColor = Color.FromArgb(234, 235, 239);
                pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);

                return;
            }
            
            SettingsBLL.PasswordStrengthLevel strength = settingsBLL.GetPasswordStrength(txtNewPassword.Text);

            switch (strength)
            {
                case SettingsBLL.PasswordStrengthLevel.Weak:

                    lblPasswordStrengthLevel.Text = "Weak";
                    lblPasswordStrengthLevel.ForeColor = Color.Red;

                    pnlWeak.BackColor = Color.Red;
                    pnlMedium.BackColor = Color.FromArgb(234, 235, 239);
                    pnlStrong.BackColor = Color.FromArgb(234, 235, 239);
                    pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);
                    break;

                case SettingsBLL.PasswordStrengthLevel.Medium:

                    lblPasswordStrengthLevel.Text = "Medium";
                    lblPasswordStrengthLevel.ForeColor = Color.Orange;

                    pnlWeak.BackColor = Color.Orange;
                    pnlMedium.BackColor = Color.Orange;
                    pnlStrong.BackColor = Color.FromArgb(234, 235, 239);
                    pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);
                    break;

                case SettingsBLL.PasswordStrengthLevel.Strong:

                    lblPasswordStrengthLevel.Text = "Strong";
                    lblPasswordStrengthLevel.ForeColor = Color.YellowGreen;

                    pnlWeak.BackColor = Color.YellowGreen;
                    pnlMedium.BackColor = Color.YellowGreen;
                    pnlStrong.BackColor = Color.YellowGreen;
                    pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);
                    break;

                case SettingsBLL.PasswordStrengthLevel.VeryStrong:

                    lblPasswordStrengthLevel.Text = "Very Strong";
                    lblPasswordStrengthLevel.ForeColor = Color.Green;

                    pnlWeak.BackColor = Color.Green;
                    pnlMedium.BackColor = Color.Green;
                    pnlStrong.BackColor = Color.Green;
                    pnlVeryStrong.BackColor = Color.Green;
                    break;
            }

            if (txtCurrentPassword.Text == txtNewPassword.Text)
            {
                lblPasswordMatch.Text = "Your current password and new password are same..";
                lblPasswordMatch.ForeColor = Color.Red;
            }
            else
            {
                lblPasswordMatch.Text = "";
            }
        }

        private void pnlWeak_Resize(object sender, EventArgs e)
        {
            SetRadius(pnlWeak, 10);
        }

        private void pnlMedium_Resize(object sender, EventArgs e)
        {
            SetRadius(pnlMedium, 10);
        }

        private void pnlStrong_Resize(object sender, EventArgs e)
        {
            SetRadius(pnlStrong, 10);
        }

        private void pnlVeryStrong_Resize(object sender, EventArgs e)
        {
            SetRadius(pnlVeryStrong, 10);
        }

        private void btnChangePasswordCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlChangePasswordAbout_Resize(object sender, EventArgs e)
        {
            SetRadius(pnlChangePasswordAbout, 10);
        }

        private void txtCurrentPassword_Enter(object sender, EventArgs e)
        {
            
            if (txtCurrentPassword.Text == "Enter Current Password")
            {
                txtCurrentPassword.Text = "";
                txtCurrentPassword.UseSystemPasswordChar = !isPasswordVisible1;
                txtCurrentPassword.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void txtCurrentPassword_Leave(object sender, EventArgs e)
        {
            if (txtCurrentPassword.Text == "")
            {
                txtCurrentPassword.Text = "Enter Current Password";
                txtCurrentPassword.UseSystemPasswordChar = false;
                txtCurrentPassword.ForeColor = Color.FromArgb(191, 192, 199);
            }
        }

        private void txtNewPassword_Enter(object sender, EventArgs e)
        {
            if (txtNewPassword.Text == "Enter New Password")
            {
                txtNewPassword.Text = "";
                txtNewPassword.UseSystemPasswordChar = !isPasswordVisible1;
                txtNewPassword.ForeColor = Color.FromArgb(0, 0, 0);
            }
            _PasswordMatch = true;
        }

        private void txtNewPassword_Leave(object sender, EventArgs e)
        {
            if (txtNewPassword.Text == "")
            {
                txtNewPassword.Text = "Enter New Password";
                txtNewPassword.UseSystemPasswordChar = false;
                txtNewPassword.ForeColor = Color.FromArgb(191, 192, 199);
                _PasswordMatch = false;
            }
        }

        private void txtConfirmPassword_Enter(object sender, EventArgs e)
        {
            if (txtConfirmPassword.Text == "Confirm New Password")
            {
                txtConfirmPassword.Text = "";
                txtConfirmPassword.UseSystemPasswordChar = !isPasswordVisible3;
                txtConfirmPassword.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void txtConfirmPassword_Leave(object sender, EventArgs e)
        {
            if (txtConfirmPassword.Text == "")
            {
                txtConfirmPassword.Text = "Confirm New Password";
                txtConfirmPassword.UseSystemPasswordChar = false;
                txtConfirmPassword.ForeColor = Color.FromArgb(191, 192, 199);
            }
        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {

            if (txtConfirmPassword.Text != "Confirm New Password" && !string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                ErrorHelper.HideErrorForControl(txtConfirmPassword);
            }
            if (txtConfirmPassword.Text == "" || txtConfirmPassword.Text == "Confirm New Password")
            {
                lblPasswordMatch.Text = "";
            
            }
            else if (txtCurrentPassword.Text == txtNewPassword.Text)
            {
                lblPasswordMatch.Text = "Your current password and new password are same..";

            }
            else if (_PasswordMatch)
            {
                if (txtNewPassword.Text != txtConfirmPassword.Text)
                {
                    lblPasswordMatch.Text = "* Password dosen't match";
                    lblPasswordMatch.ForeColor = Color.Red;
                }
                else
                {
                    lblPasswordMatch.Text = "* Password match";
                    lblPasswordMatch.ForeColor = Color.Green;
                }
            }
        }

        private void picEye1_Click(object sender, EventArgs e)
        {
            isPasswordVisible1 = !isPasswordVisible1;

            if (txtCurrentPassword.Text != "Enter Current Password")
            txtCurrentPassword.UseSystemPasswordChar = !isPasswordVisible1;

            if (isPasswordVisible1)
            {
                picEye1.Image = Properties.Resources.open_eye__2_;
            }
            else
            {
                picEye1.Image = Properties.Resources.eye;
            }
        }

        private void picEye2_Click(object sender, EventArgs e)
        {
            isPasswordVisible2 = !isPasswordVisible2;

            if (txtNewPassword.Text != "Enter New Password")
            txtNewPassword.UseSystemPasswordChar = !isPasswordVisible2;

            if (isPasswordVisible2)
            {
                picEye2.Image = Properties.Resources.open_eye__2_;
            }
            else
            {
                picEye2.Image = Properties.Resources.eye;
            }
        }

        private void picEye3_Click(object sender, EventArgs e)
        {
            isPasswordVisible3 = !isPasswordVisible3;

            if (txtConfirmPassword.Text != "Confirm New Password")
            txtConfirmPassword.UseSystemPasswordChar = !isPasswordVisible3;

            if (isPasswordVisible3)
            {
                picEye3.Image = Properties.Resources.open_eye__2_;
            }
            else
            {
                picEye3.Image = Properties.Resources.eye;
            }
        }

        private void btnChangePasswordUpdatePassword_Click(object sender, EventArgs e)
        {
            SettingsUi settingsUi = new SettingsUi();

            settingsUi.UserId = Session.LogedInUser.GetUserId();
            settingsUi.CurrentPassword = (txtCurrentPassword.Text == "Enter Current Password") ? "" : txtCurrentPassword.Text;
            settingsUi.NewPassword = (txtNewPassword.Text == "Enter New Password") ? "" : txtNewPassword.Text;
            settingsUi.ConfirmPassword = (txtConfirmPassword.Text == "Confirm New Password") ? "" : txtConfirmPassword.Text;

            CommonValidator.ValidationResult result = settingsUi.ChangePasswordDataIntoSettingsUi();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show("Password Changed Successfully");
                    this.Close();
                    break;

                case CommonValidator.ValidationResult.CurrentPasswordEmpty:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtCurrentPassword);
                    break;

                case CommonValidator.ValidationResult.NewPasswordEmpty:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtNewPassword);
                    break;

                case CommonValidator.ValidationResult.ConfirmPasswordEmpty:
                    lblPasswordMatch.Text = ""; 
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtConfirmPassword);
                    break;

                case CommonValidator.ValidationResult.CurrentAndNewPasswordSame:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtNewPassword);
                    break;


                case CommonValidator.ValidationResult.NotMatchPassword:
                    ErrorHelper.HideErrorForControl(txtConfirmPassword);
                    lblPasswordMatch.Text = "* Password doesn't match.";
                    lblPasswordMatch.ForeColor = Color.Red;
                    txtConfirmPassword.Focus();
                    break;

                case CommonValidator.ValidationResult.WeakPassword:
                    MessageBox.Show("Password is weak please enter strong hard password.");
                    break;

                case CommonValidator.ValidationResult.MediumPassword:
                    MessageBox.Show("Password is medium please enter strong hard password.");
                    break;

                case CommonValidator.ValidationResult.StrongPassword:
                    MessageBox.Show("Password is strong but not hard, please enter strong hard password.");
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    MessageBox.Show("Password Not Updated.");
                    break;
            }
        }


    }
}
