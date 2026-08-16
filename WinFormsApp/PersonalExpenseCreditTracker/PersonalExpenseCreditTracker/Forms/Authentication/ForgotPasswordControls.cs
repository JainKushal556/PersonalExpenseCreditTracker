using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using PersonalExpenseCreditTracker.Common;
using BLLayer.Authentication;
using BLLayer.Common;


namespace PersonalExpenseCreditTracker.Forms.Authentication
{
    public partial class ForgotPasswordControls : Form
    {
        private bool _PasswordMatch;
        bool isPasswordVisible2 = true;
        bool isPasswordVisible3 = true;
        private readonly string defaultInfoMessage = "Use a strong password with at least 8 characters including\nuppercase, lowercase, number and special character.";

        public ForgotPasswordControls()
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

        private void ForgotPasswordControls_Load(object sender, EventArgs e)
        {
            txtRegisteredEmail.Text = "Enter Registered Email";
            txtRegisteredPhoneNumber.Text = "Enter Registered Phone Number";
            txtNewPassword.Text = "Enter New Password";
            txtConfirmPassword.Text = "Confirm New Password";

            pnlWeak.BackColor = Color.FromArgb(234, 235, 239);
            pnlMedium.BackColor = Color.FromArgb(234, 235, 239);
            pnlStrong.BackColor = Color.FromArgb(234, 235, 239);
            pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);

            txtRegisteredEmail.ForeColor = Color.FromArgb(191, 192, 199);
            txtRegisteredPhoneNumber.ForeColor = Color.FromArgb(191, 192, 199);
            txtNewPassword.ForeColor = Color.FromArgb(191, 192, 199);
            txtConfirmPassword.ForeColor = Color.FromArgb(191, 192, 199);

            picEye2.Image = Properties.Resources.open_eye__2_;
            picEye3.Image = Properties.Resources.open_eye__2_;

            txtRegisteredEmail.TextChanged += txtRegisteredEmail_TextChanged;
            txtRegisteredPhoneNumber.TextChanged += txtRegisteredPhoneNumber_TextChanged;
            txtConfirmPassword.TextChanged += txtConfirmPassword_TextChanged;
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

        private void ShowDatabaseError(string message)
        {
            pnlForgotPasswordAbout.Height = 52;
            pnlForgotPasswordAbout.BackColor = Color.FromArgb(254, 242, 242);
            pictureBox7.Location = new Point(14, 12);
            pictureBox7.Size = new Size(28, 28);
            label7.Location = new Point(50, 16);
            label7.Text = message;
            label7.ForeColor = Color.FromArgb(220, 38, 38);
            label7.BringToFront();
            SetRadius(pnlForgotPasswordAbout, 14);
        }
      
        private void ResetInfoBox()
        {
            if (label7.Text != defaultInfoMessage)
            {
                pnlForgotPasswordAbout.Height = 84;
                pnlForgotPasswordAbout.BackColor = Color.FromArgb(241, 243, 255);
                pictureBox7.Location = new Point(14, 24);
                pictureBox7.Size = new Size(32, 32);
                label7.Location = new Point(50, 18); 
                label7.Text = defaultInfoMessage;
                label7.ForeColor = Color.Black;
                label7.BringToFront();
                SetRadius(pnlForgotPasswordAbout, 20);
            }
        }
        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            ResetInfoBox();
            if (txtNewPassword.Text != "Enter New Password" && !string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                ErrorHelper.HideErrorForControl(txtNewPassword);
            }
            AuthBLL authBll = new AuthBLL();

            if (txtNewPassword.Text == "" || txtNewPassword.Text == "Enter New Password")
            {
                lblPasswordStrengthLevel.Text = "";

                pnlWeak.BackColor = Color.FromArgb(234, 235, 239);
                pnlMedium.BackColor = Color.FromArgb(234, 235, 239);
                pnlStrong.BackColor = Color.FromArgb(234, 235, 239);
                pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);

                return;
            }

            AuthBLL.PasswordStrengthLevel strength = authBll.GetPasswordStrength(txtNewPassword.Text);

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

            CheckPasswordMatch();
        }

        private void picEye2_Click(object sender, EventArgs e)
        {
            isPasswordVisible2 = !isPasswordVisible2;

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

        private void pnlForgotPasswordAbout_Resize(object sender, EventArgs e)
        {
            SetRadius(pnlForgotPasswordAbout, 20);
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

        private void btnForgotPasswordUpdatePassword_Resize(object sender, EventArgs e)
        {
            SetRadius(btnForgotPasswordUpdatePassword, 20);
        }

        private void btnForgotPasswordCancel_Resize(object sender, EventArgs e)
        {
            SetRadius(btnForgotPasswordCancel, 20);
        }

        private void txtRegisteredEmail_Enter(object sender, EventArgs e)
        {
            if (txtRegisteredEmail.Text == "Enter Registered Email")
            {
                txtRegisteredEmail.Text = "";
                txtRegisteredEmail.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void txtRegisteredEmail_Leave(object sender, EventArgs e)
        {
            if (txtRegisteredEmail.Text == "")
            {
                txtRegisteredEmail.Text = "Enter Registered Email";
                txtRegisteredEmail.ForeColor = Color.FromArgb(191, 192, 199);
            }

        }

        private void txtRegisteredPhoneNumber_Enter(object sender, EventArgs e)
        {
            if (txtRegisteredPhoneNumber.Text == "Enter Registered Phone Number")
            {
                txtRegisteredPhoneNumber.Text = "";
                txtRegisteredPhoneNumber.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void txtRegisteredPhoneNumber_Leave(object sender, EventArgs e)
        {
            if (txtRegisteredPhoneNumber.Text == "")
            {
                txtRegisteredPhoneNumber.Text = "Enter Registered Phone Number";
                txtRegisteredPhoneNumber.ForeColor = Color.FromArgb(191, 192, 199);
            }
        }

        private void txtNewPassword_Enter(object sender, EventArgs e)
        {
            if (txtNewPassword.Text == "Enter New Password")
            {
                txtNewPassword.Text = "";
                txtNewPassword.ForeColor = Color.FromArgb(0, 0, 0);
            }
            _PasswordMatch = true;
        }

        private void txtNewPassword_Leave(object sender, EventArgs e)
        {
            if (txtNewPassword.Text == "")
            {
                txtNewPassword.Text = "Enter New Password";
                txtNewPassword.ForeColor = Color.FromArgb(191, 192, 199);
                _PasswordMatch = false;
            }

        }

        private void txtConfirmPassword_Enter(object sender, EventArgs e)
        {
            if (txtConfirmPassword.Text == "Confirm New Password")
            {
                txtConfirmPassword.Text = "";
                txtConfirmPassword.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        private void txtConfirmPassword_Leave(object sender, EventArgs e)
        {
            if (txtConfirmPassword.Text == "")
            {
                txtConfirmPassword.Text = "Confirm New Password";
                txtConfirmPassword.ForeColor = Color.FromArgb(191, 192, 199);
            }
        }

        //private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtConfirmPassword.Text != "Confirm New Password" && !string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
        //    {
        //        ErrorHelper.HideErrorForControl(txtConfirmPassword);
        //    }
        //    if (txtConfirmPassword.Text == "" || txtConfirmPassword.Text == "Confirm New Password")
        //    {
        //        lblPasswordMatch.Text = "";
        //    }
        //    else if (_PasswordMatch)
        //    {
        //        if (txtNewPassword.Text != txtConfirmPassword.Text)
        //        {
        //            lblPasswordMatch.Text = "* Password dosen't match";
        //            lblPasswordMatch.ForeColor = Color.Red;
        //        }
        //        else
        //        {
        //            lblPasswordMatch.Text = "* Password match";
        //            lblPasswordMatch.ForeColor = Color.Green;
        //        }
        //    }

        //    CheckPasswordMatch();
        //}

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            CheckPasswordMatch();
            ResetInfoBox();
        }


        private void CheckPasswordMatch()
        {
            string newPass = txtNewPassword.Text;
            string confirmPass = txtConfirmPassword.Text;

            Control targetControl = txtConfirmPassword;
            if (txtConfirmPassword.Parent != null && txtConfirmPassword.Parent.Height < 50 && !(txtConfirmPassword.Parent is Form))
            {
                targetControl = txtConfirmPassword.Parent;
            }

            Control parent = targetControl.Parent;
            if (parent == null) return;

            string labelName = "lblErr_" + txtConfirmPassword.Name;
            Label matchLabel = parent.Controls.Find(labelName, false).FirstOrDefault() as Label;

            // 1. If confirm password field is empty or placeholder
            // Keep "Confirm password is required" error visible; only hide previous match status
            if (string.IsNullOrWhiteSpace(confirmPass) || confirmPass == "Confirm New Password")
            {
                if (matchLabel != null && (matchLabel.Text == "* Password match" || matchLabel.Text == "* Password doesn't match" || matchLabel.Text == "* Password doesn't match."))
                {
                    matchLabel.Visible = false;
                }
                return;
            }

            // 2. Hide previous error as soon as user starts typing
            if (matchLabel != null && matchLabel.Text != "* Password match")
            {
                matchLabel.Visible = false;
            }

            // 3. If new password field is empty or placeholder, do not check match yet
            if (string.IsNullOrWhiteSpace(newPass) || newPass == "Enter New Password")
            {
                return;
            }

            // 4. If both passwords match, automatically display "* Password match" in green
            if (newPass == confirmPass)
            {
                if (matchLabel == null)
                {
                    matchLabel = new Label();
                    matchLabel.Name = labelName;
                    matchLabel.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular);
                    matchLabel.AutoSize = true;
                    parent.Controls.Add(matchLabel);
                }

                matchLabel.Location = new Point(targetControl.Left, targetControl.Bottom + 2);
                matchLabel.BringToFront();
                matchLabel.Text = "* Password match";
                matchLabel.ForeColor = Color.Green;
                matchLabel.Visible = true;
            }
            else
            {
                // 5. If passwords do not match while typing, hide the green match message
                if (matchLabel != null && matchLabel.Text == "* Password match")
                {
                    matchLabel.Visible = false;
                }
            }
        }





        private void btnForgotPasswordClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnForgotPasswordCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void btnForgotPasswordUpdatePassword_Click(object sender, EventArgs e)
        //{
        //    AuthUI authUI = new AuthUI();
        //    string ErroeMsg;

        //    authUI.email = (txtRegisteredEmail.Text == "Enter Registered Email") ? "" : txtRegisteredEmail.Text;
        //    authUI.phoneNumber = (txtRegisteredPhoneNumber.Text == "Enter Registered Phone Number") ? "" : txtRegisteredPhoneNumber.Text;
        //    authUI.newPassword = (txtNewPassword.Text == "Enter New Password") ? "" : txtNewPassword.Text;
        //    authUI.confirmPassword = (txtConfirmPassword.Text == "Confirm New Password") ? "" : txtConfirmPassword.Text;

        //    CommonValidator.ValidationResult result = authUI.ForgotPasswordDataIntoAuthUI();

        //    switch (result)
        //    {
        //        case CommonValidator.ValidationResult.Success:
        //            MessageBox.Show("Password Reset Successfully");
        //            this.DialogResult = DialogResult.OK;
        //            this.Close();
        //            break;

        //        case CommonValidator.ValidationResult.EmailInvalid:
        //            ErrorHelper.ShowValidationError(result, errorProvider1, txtRegisteredEmail);
        //            break;

        //        case CommonValidator.ValidationResult.PhoneNumberEmpty:
        //            ErrorHelper.ShowValidationError(result, errorProvider1, txtRegisteredPhoneNumber);
        //            break;

        //        case CommonValidator.ValidationResult.PhoneInvalid:
        //            ErrorHelper.ShowValidationError(result, errorProvider1, txtRegisteredPhoneNumber);
        //            break;

        //        case CommonValidator.ValidationResult.NewPasswordEmpty:
        //            ErrorHelper.ShowValidationError(result, errorProvider1, txtNewPassword);
        //            break;

        //        case CommonValidator.ValidationResult.ConfirmPasswordEmpty:
        //            //lblPasswordMatch.Text = "";
        //            ErrorHelper.ShowValidationError(result, errorProvider1, txtConfirmPassword);
        //            break;

        //        case CommonValidator.ValidationResult.NotMatchPassword:
        //            MessageBox.Show("Password Doesn't Match");

        //            //ErrorHelper.HideErrorForControl(txtConfirmPassword);
        //            //lblPasswordMatch.Text = "* Password doesn't match.";
        //            //lblPasswordMatch.ForeColor = Color.Red;
        //            //txtConfirmPassword.Focus();
        //            break;

        //        case CommonValidator.ValidationResult.WeakPassword:
        //            MessageBox.Show("Password is weak please enter strong hard password.");
        //            break;

        //        case CommonValidator.ValidationResult.MediumPassword:
        //            MessageBox.Show("Password is medium please enter strong hard password.");
        //            break;

        //        case CommonValidator.ValidationResult.StrongPassword:
        //            MessageBox.Show("Password is strong but not hard, please enter strong hard password.");
        //            break;

        //        case CommonValidator.ValidationResult.StoreProcedureError:
        //            ErroeMsg = authUI.GetErrorMsgForForgotPassword();
        //            MessageBox.Show(ErroeMsg);
        //            break;
        //    }
            
        //}

        private void btnForgotPasswordUpdatePassword_Click(object sender, EventArgs e)
        {
            // 1. Clear all existing validation error messages
            ErrorHelper.ClearAllErrors(panel4);

            AuthUI authUI = new AuthUI();
            string errorMsg;

            authUI.email = (txtRegisteredEmail.Text == "Enter Registered Email") ? "" : txtRegisteredEmail.Text.Trim();
            authUI.phoneNumber = (txtRegisteredPhoneNumber.Text == "Enter Registered Phone Number") ? "" : txtRegisteredPhoneNumber.Text.Trim();
            authUI.newPassword = (txtNewPassword.Text == "Enter New Password") ? "" : txtNewPassword.Text;
            authUI.confirmPassword = (txtConfirmPassword.Text == "Confirm New Password") ? "" : txtConfirmPassword.Text;

            CommonValidator.ValidationResult result = authUI.ForgotPasswordDataIntoAuthUI();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show("Password updated successfully! Please login with your new password.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;

                case CommonValidator.ValidationResult.EmailInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtRegisteredEmail);
                    break;

                case CommonValidator.ValidationResult.PhoneNumberEmpty:
                case CommonValidator.ValidationResult.PhoneInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtRegisteredPhoneNumber);
                    break;

                case CommonValidator.ValidationResult.NewPasswordEmpty:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtNewPassword);
                    break;

                case CommonValidator.ValidationResult.ConfirmPasswordEmpty:
                case CommonValidator.ValidationResult.NotMatchPassword:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtConfirmPassword);
                    break;

                case CommonValidator.ValidationResult.WeakPassword:
                case CommonValidator.ValidationResult.MediumPassword:
                case CommonValidator.ValidationResult.StrongPassword:
                    ErrorHelper.ShowErrorBelowControl(txtNewPassword, "* Password must be very strong.");
                    txtNewPassword.Focus();
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    errorMsg = authUI.GetErrorMsgForForgotPassword();
                    // Display error in the bottom info box in red instead of a popup
                    ShowDatabaseError(string.IsNullOrWhiteSpace(errorMsg) ? "Invalid email or phone number. Please try again." : errorMsg);
                    txtRegisteredEmail.Focus();
                    break;

            }
        }


        private void btnForgotPasswordClose_MouseEnter(object sender, EventArgs e)
        {
            btnForgotPasswordClose.BackColor = Color.Red;
        }

        private void btnForgotPasswordClose_MouseLeave(object sender, EventArgs e)
        {
            btnForgotPasswordClose.BackColor = Color.White;
        }

        private void txtRegisteredEmail_TextChanged(object sender, EventArgs e)
        {
            ResetInfoBox(); 
            if (txtRegisteredEmail.Text != "Enter Registered Email" && !string.IsNullOrWhiteSpace(txtRegisteredEmail.Text))
            {
                ErrorHelper.HideErrorForControl(txtRegisteredEmail);
            }
        }


        private void txtRegisteredPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            ResetInfoBox();
            if (txtRegisteredPhoneNumber.Text != "Enter Registered Phone Number" && !string.IsNullOrWhiteSpace(txtRegisteredPhoneNumber.Text))
            {
                ErrorHelper.HideErrorForControl(txtRegisteredPhoneNumber);
            }
        }




    }
}