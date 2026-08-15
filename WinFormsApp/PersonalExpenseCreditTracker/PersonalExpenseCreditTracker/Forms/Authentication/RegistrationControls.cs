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
using PersonalExpenseCreditTracker.Common;
using BLLayer.Authentication;
using BLLayer.Common;

namespace PersonalExpenseCreditTracker.Forms.Authentication
{
    public partial class RegistrationControls : Form
    {
        bool isPasswordVisible1 = true;
        bool isPasswordVisible2 = true;
        public RegistrationControls()
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

        private void RegistrationControls_Load(object sender, EventArgs e)
        {
            SetRadius(pnlRegistrationDataInput, 20);
            SetRadius(pnlWeak, 10);
            SetRadius(pnlMedium, 10);
            SetRadius(pnlStrong, 10);
            SetRadius(pnlVeryStrong, 10);
            SetRadius(btnCreateAccount, 17);

            picEye1.Image = Properties.Resources.open_eye__2_;
            picEye2.Image = Properties.Resources.open_eye__2_;

            txtFullName.Text = "Enter your full name";
            txtRegistrationEmail.Text = "Enter your email address";
            txtRegistrationCreatePassword.Text = "Create a password";
            txtRegistrationConfirmPassword.Text = "Confirm password";

            txtFullName.ForeColor = Color.Gray;
            txtRegistrationEmail.ForeColor = Color.Gray;
            txtRegistrationCreatePassword.ForeColor = Color.Gray;
            txtRegistrationConfirmPassword.ForeColor = Color.Gray;

            //lblPasswordRestriction.Text = "At least 8 characters including uppercase, lowercase, number and special character";
        }
        
        private void txtRegistrationEmail_Enter(object sender, EventArgs e)
        {
            if (txtRegistrationEmail.Text == "Enter your email address")
            {
                txtRegistrationEmail.Text = "";
                txtRegistrationEmail.ForeColor = Color.Black;
            }
        }

        private void txtRegistrationEmail_Leave(object sender, EventArgs e)
        {
            if (txtRegistrationEmail.Text == "")
            {
                txtRegistrationEmail.Text = "Enter your email address";
                txtRegistrationEmail.ForeColor = Color.Gray;
            }
        }

        private void txtFullName_Enter(object sender, EventArgs e)
        {
            if (txtFullName.Text == "Enter your full name")
            {
                txtFullName.Text = "";
                txtFullName.ForeColor = Color.Black;
            }
        }

        private void txtFullName_Leave(object sender, EventArgs e)
        {
            if (txtFullName.Text == "")
            {
                txtFullName.Text = "Enter your full name";
                txtFullName.ForeColor = Color.Gray;
            }
        }

        private void txtRegistrationCreatePassword_Enter(object sender, EventArgs e)
        {
            if (txtRegistrationCreatePassword.Text == "Create a password")
            {
                txtRegistrationCreatePassword.Text = "";
                txtRegistrationCreatePassword.ForeColor = Color.Black;
            }
        }

        private void txtRegistrationCreatePassword_Leave(object sender, EventArgs e)
        {
            if (txtRegistrationCreatePassword.Text == "")
            {
                txtRegistrationCreatePassword.Text = "Create a password";
                txtRegistrationCreatePassword.ForeColor = Color.Gray;
            }
        }

        private void txtRegistrationConfirmPassword_Enter(object sender, EventArgs e)
        {
            if (txtRegistrationConfirmPassword.Text == "Confirm password")
            {
                txtRegistrationConfirmPassword.Text = "";
                txtRegistrationConfirmPassword.ForeColor = Color.Black;
            }
        }

        private void txtRegistrationConfirmPassword_Leave(object sender, EventArgs e)
        {
            if (txtRegistrationConfirmPassword.Text == "")
            {
                txtRegistrationConfirmPassword.Text = "Confirm password";
                txtRegistrationConfirmPassword.ForeColor = Color.Gray;
            }
        }

        private int CheckPasswordStrengthLevel(string password)
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

            return score;
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtRegistrationCreatePassword.Text != "Create a password" && !string.IsNullOrWhiteSpace(txtRegistrationCreatePassword.Text))
            {
                ErrorHelper.HideErrorForControl(txtRegistrationCreatePassword);
            }

            AuthBLL authBll = new AuthBLL();

            if (txtRegistrationCreatePassword.Text == "" || txtRegistrationCreatePassword.Text == "Create a password")
            {
                lblPasswordStrengthLevel.Text = "";

                pnlWeak.BackColor = Color.FromArgb(234, 235, 239);
                pnlMedium.BackColor = Color.FromArgb(234, 235, 239);
                pnlStrong.BackColor = Color.FromArgb(234, 235, 239);
                pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);

                return;
            }

            AuthBLL.PasswordStrengthLevel strength = authBll.GetPasswordStrength(txtRegistrationCreatePassword.Text);

            switch (strength)
            {
                case AuthBLL.PasswordStrengthLevel.Weak:

                    lblPasswordStrengthLevel.Text = "Weak";
                    lblPasswordStrengthLevel.ForeColor = Color.Red;

                    pnlWeak.BackColor = Color.Red;
                    pnlMedium.BackColor = Color.FromArgb(234, 235, 239);
                    pnlStrong.BackColor = Color.FromArgb(234, 235, 239);
                    pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);
                    break;

                case AuthBLL.PasswordStrengthLevel.Medium:

                    lblPasswordStrengthLevel.Text = "Medium";
                    lblPasswordStrengthLevel.ForeColor = Color.Orange;

                    pnlWeak.BackColor = Color.Orange;
                    pnlMedium.BackColor = Color.Orange;
                    pnlStrong.BackColor = Color.FromArgb(234, 235, 239);
                    pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);
                    break;

                case AuthBLL.PasswordStrengthLevel.Strong:

                    lblPasswordStrengthLevel.Text = "Strong";
                    lblPasswordStrengthLevel.ForeColor = Color.YellowGreen;

                    pnlWeak.BackColor = Color.YellowGreen;
                    pnlMedium.BackColor = Color.YellowGreen;
                    pnlStrong.BackColor = Color.YellowGreen;
                    pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);
                    break;

                case AuthBLL.PasswordStrengthLevel.VeryStrong:

                    lblPasswordStrengthLevel.Text = "Very Strong";
                    lblPasswordStrengthLevel.ForeColor = Color.Green;

                    pnlWeak.BackColor = Color.Green;
                    pnlMedium.BackColor = Color.Green;
                    pnlStrong.BackColor = Color.Green;
                    pnlVeryStrong.BackColor = Color.Green;
                    break;
            }

            //if (txtCurrentPassword.Text == txtRegistrationCreatePassword.Text)
            //{
            //    lblPasswordMatch.Text = "Your current password and new password are same..";
            //    lblPasswordMatch.ForeColor = Color.Red;
            //}
            //else
            //{
            //    lblPasswordMatch.Text = "";
            //}

            //if (txtRegistrationCreatePassword.Text == "" || txtRegistrationCreatePassword.Text == "Create a password")
            //{
            //    pnlWeak.BackColor = Color.FromArgb(234, 235, 239);
            //    pnlMedium.BackColor = Color.FromArgb(234, 235, 239);
            //    pnlStrong.BackColor = Color.FromArgb(234, 235, 239);
            //    pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);

            //    return;
            //}

            //int score = CheckPasswordStrengthLevel(txtRegistrationCreatePassword.Text);

            //if (score <= 2)
            //{
            //    lblPasswordStrengthLevel.Text = "Weak";
            //    lblPasswordStrengthLevel.ForeColor = Color.Red;
            //    pnlWeak.BackColor = Color.Red;
            //    pnlMedium.BackColor = Color.FromArgb(234, 235, 239);
            //    pnlStrong.BackColor = Color.FromArgb(234, 235, 239);
            //    pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);
            //}
            //else if (score == 3)
            //{
            //    lblPasswordStrengthLevel.Text = "Medium";
            //    lblPasswordStrengthLevel.ForeColor = Color.Orange;
            //    pnlWeak.BackColor = Color.Orange;
            //    pnlMedium.BackColor = Color.Orange;
            //    pnlStrong.BackColor = Color.FromArgb(234, 235, 239);
            //    pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);
            //}
            //else if (score == 4)
            //{
            //    lblPasswordStrengthLevel.Text = "Strong";
            //    lblPasswordStrengthLevel.ForeColor = Color.YellowGreen;
            //    pnlWeak.BackColor = Color.YellowGreen;
            //    pnlMedium.BackColor = Color.YellowGreen;
            //    pnlStrong.BackColor = Color.YellowGreen;
            //    pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);
            //}
            //else
            //{
            //    lblPasswordStrengthLevel.Text = "Very Strong";
            //    lblPasswordStrengthLevel.ForeColor = Color.Green;
            //    pnlWeak.BackColor = Color.Green;
            //    pnlMedium.BackColor = Color.Green;
            //    pnlStrong.BackColor = Color.Green;
            //    pnlVeryStrong.BackColor = Color.Green;
            //}
        }

        private void picEye1_Click(object sender, EventArgs e)
        {
            isPasswordVisible1 = !isPasswordVisible1;

            txtRegistrationCreatePassword.UseSystemPasswordChar = !isPasswordVisible1;

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

            txtRegistrationConfirmPassword.UseSystemPasswordChar = !isPasswordVisible2;

            if (isPasswordVisible2)
            {
                picEye2.Image = Properties.Resources.open_eye__2_;
            }
            else
            {
                picEye2.Image = Properties.Resources.eye;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.ActiveControl = null;
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            AuthUI authUI = new AuthUI();
            string ErroeMsg;

            authUI.userName = (txtFullName.Text == "Enter your full name") ? "" : txtFullName.Text;
            authUI.email = (txtRegistrationEmail.Text == "Enter your email address") ? "" : txtRegistrationEmail.Text;
            authUI.phoneNumber = txtRegistrationPhoneNumber.Text;
            authUI.newPassword = (txtRegistrationCreatePassword.Text == "Create a password") ? "" : txtRegistrationCreatePassword.Text;
            authUI.confirmPassword = (txtRegistrationConfirmPassword.Text == "Confirm password") ? "" : txtRegistrationConfirmPassword.Text;

            CommonValidator.ValidationResult result = authUI.RegistrationFormDataIntoAuthUI();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show("Registration Successfully");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;

                case CommonValidator.ValidationResult.PersonNameEmpty:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtFullName);
                    break;

                case CommonValidator.ValidationResult.PersonNameInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtFullName);
                    break;

                case CommonValidator.ValidationResult.EmailInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtRegistrationEmail);
                    break;

                case CommonValidator.ValidationResult.PhoneNumberEmpty:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtRegistrationPhoneNumber);
                    break;

                case CommonValidator.ValidationResult.PhoneInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtRegistrationPhoneNumber);
                    break;

                case CommonValidator.ValidationResult.NewPasswordEmpty:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtRegistrationCreatePassword);
                    break;

                case CommonValidator.ValidationResult.ConfirmPasswordEmpty:
                    //lblPasswordMatch.Text = "";
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtRegistrationConfirmPassword);
                    break;

                case CommonValidator.ValidationResult.CurrentAndNewPasswordSame:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtRegistrationCreatePassword);
                    break;

                case CommonValidator.ValidationResult.NotMatchPassword:
                    MessageBox.Show("Password Doesn't Match");

                    //ErrorHelper.HideErrorForControl(txtConfirmPassword);
                    //lblPasswordMatch.Text = "* Password doesn't match.";
                    //lblPasswordMatch.ForeColor = Color.Red;
                    //txtConfirmPassword.Focus();
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
                    ErroeMsg = authUI.GetErrorMsg();
                    MessageBox.Show(ErroeMsg);
                    break;
            }
        }

        private void txtRegistrationConfirmPassword_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
