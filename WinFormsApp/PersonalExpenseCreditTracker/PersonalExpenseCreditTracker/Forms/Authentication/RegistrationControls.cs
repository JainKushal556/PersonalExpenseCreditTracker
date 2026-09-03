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

            txtFullName.Text = "Enter Your Full Name";
            txtRegistrationEmail.Text = "Enter Your Email Address";
            txtRegistrationPhoneNumber.Text = "Enter Your Phone Number";
            txtRegistrationCreatePassword.Text = "Create a Password";
            txtRegistrationConfirmPassword.Text = "Confirm Password";

            // Tab order fix: parent panel গুলোর TabStop বন্ধ করো
            // যাতে Tab চাপলে সরাসরি TextBox-এ focus যায়
            panel4.TabStop = false;
            panel3.TabStop = false;
            pnlPhoneNumber.TabStop = false;
            pnlNewPassword.TabStop = false;
            pnlConfirmPassword.TabStop = false;

            // প্রতিটা TextBox-এ sequential TabIndex সেট করো
            // Tab order: FullName → Email → Phone → Password → ConfirmPassword → CreateAccount
            txtFullName.TabIndex = 1;
            txtRegistrationEmail.TabIndex = 2;
            txtRegistrationPhoneNumber.TabIndex = 3;
            txtRegistrationCreatePassword.TabIndex = 4;
            txtRegistrationConfirmPassword.TabIndex = 5;
            btnCreateAccount.TabIndex = 6;

            txtFullName.ForeColor = Color.Gray;
            txtRegistrationEmail.ForeColor = Color.Gray;
            txtRegistrationPhoneNumber.ForeColor = Color.Gray;
            txtRegistrationCreatePassword.ForeColor = Color.Gray;
            txtRegistrationConfirmPassword.ForeColor = Color.Gray;

            txtFullName.TextChanged += txtFullName_TextChanged;
            txtRegistrationEmail.TextChanged += txtRegistrationEmail_TextChanged;
            txtRegistrationPhoneNumber.TextChanged += txtRegistrationPhoneNumber_TextChanged;
            txtRegistrationConfirmPassword.TextChanged += txtRegistrationConfirmPassword_TextChanged;

        }
        
        private void txtRegistrationEmail_Enter(object sender, EventArgs e)
        {
            if (txtRegistrationEmail.Text == "Enter Your Email Address")
            {
                txtRegistrationEmail.Text = "";
                txtRegistrationEmail.ForeColor = Color.Black;
            }
        }

        private void txtRegistrationEmail_Leave(object sender, EventArgs e)
        {
            if (txtRegistrationEmail.Text == "")
            {
                txtRegistrationEmail.Text = "Enter Your Email Address";
                txtRegistrationEmail.ForeColor = Color.Gray;
            }
        }

        private void txtFullName_Enter(object sender, EventArgs e)
        {
            if (txtFullName.Text == "Enter Your Full Name")
            {
                txtFullName.Text = "";
                txtFullName.ForeColor = Color.Black;
            }
        }

        private void txtFullName_Leave(object sender, EventArgs e)
        {
            if (txtFullName.Text == "")
            {
                txtFullName.Text = "Enter Your Full Name";
                txtFullName.ForeColor = Color.Gray;
            }
        }

        private void txtRegistrationPhoneNumber_Enter(object sender, EventArgs e)
        {
            if (txtRegistrationPhoneNumber.Text == "Enter Your Phone Number")
            {
                txtRegistrationPhoneNumber.Text = "";
                txtRegistrationPhoneNumber.ForeColor = Color.Black;
            }
        }

        private void txtRegistrationPhoneNumber_Leave(object sender, EventArgs e)
        {
            if (txtRegistrationPhoneNumber.Text == "")
            {
                txtRegistrationPhoneNumber.Text = "Enter Your Phone Number";
                txtRegistrationPhoneNumber.ForeColor = Color.Gray;
            }
        }

        private void txtRegistrationPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtRegistrationPhoneNumber.Text != "Enter Your Phone Number" && !string.IsNullOrWhiteSpace(txtRegistrationPhoneNumber.Text))
            {
                ErrorHelper.HideErrorForControl(txtRegistrationPhoneNumber);
            }
        }


        private void txtRegistrationCreatePassword_Enter(object sender, EventArgs e)
        {
            
            
            if (txtRegistrationCreatePassword.Text == "Create a Password")
            {
                txtRegistrationCreatePassword.Text = "";
                txtRegistrationCreatePassword.UseSystemPasswordChar = !isPasswordVisible1;
                txtRegistrationCreatePassword.ForeColor = Color.Black;
            }
        }

        private void txtRegistrationCreatePassword_Leave(object sender, EventArgs e)
        {
            if (txtRegistrationCreatePassword.Text == "")
            {
                txtRegistrationCreatePassword.Text = "Create a Password";
                txtRegistrationCreatePassword.UseSystemPasswordChar = false;
                txtRegistrationCreatePassword.ForeColor = Color.Gray;
            }
        }

        private void txtRegistrationConfirmPassword_Enter(object sender, EventArgs e)
        {
            
            
            if (txtRegistrationConfirmPassword.Text == "Confirm Password")
            {
                txtRegistrationConfirmPassword.Text = "";
                txtRegistrationConfirmPassword.UseSystemPasswordChar = !isPasswordVisible2;
                txtRegistrationConfirmPassword.ForeColor = Color.Black;
            }
        }

        private void txtRegistrationConfirmPassword_Leave(object sender, EventArgs e)
        {
            if (txtRegistrationConfirmPassword.Text == "")
            {
                txtRegistrationConfirmPassword.Text = "Confirm Password";
                txtRegistrationConfirmPassword.UseSystemPasswordChar = false;
                txtRegistrationConfirmPassword.ForeColor = Color.Gray;
            }
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtRegistrationCreatePassword.Text != "Create a Password" && !string.IsNullOrWhiteSpace(txtRegistrationCreatePassword.Text))
            {
                ErrorHelper.HideErrorForControl(txtRegistrationCreatePassword);
            }

            AuthBLL authBll = new AuthBLL();

            if (txtRegistrationCreatePassword.Text == "" || txtRegistrationCreatePassword.Text == "Create a Password")
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

            CheckPasswordMatch();

            
        }

        private void picEye1_Click(object sender, EventArgs e)
        {
            isPasswordVisible1 = !isPasswordVisible1;

            if (txtRegistrationCreatePassword.Text != "Create a Password")
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

            if (txtRegistrationConfirmPassword.Text != "Confirm Password")
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
            ErrorHelper.ClearAllErrors(pnlRegistrationDataInput);

            AuthUI authUI = new AuthUI();
            string errorMsg;

            authUI.userName = (txtFullName.Text == "Enter Your Full Name") ? "" : txtFullName.Text.Trim();
            authUI.email = (txtRegistrationEmail.Text == "Enter Your Email Address") ? "" : txtRegistrationEmail.Text.Trim();
            authUI.phoneNumber = (txtRegistrationPhoneNumber.Text == "Enter Your Phone Number") ? "" : txtRegistrationPhoneNumber.Text.Trim();
            authUI.newPassword = (txtRegistrationCreatePassword.Text == "Create a Password") ? "" : txtRegistrationCreatePassword.Text;
            authUI.confirmPassword = (txtRegistrationConfirmPassword.Text == "Confirm Password") ? "" : txtRegistrationConfirmPassword.Text;

            CommonValidator.ValidationResult result = authUI.RegistrationFormDataIntoAuthUI();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    //MessageBox.Show("Registration successful! Please login.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    break;

                case CommonValidator.ValidationResult.PersonNameEmpty:
                case CommonValidator.ValidationResult.PersonNameInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtFullName);
                    break;

                case CommonValidator.ValidationResult.EmailInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtRegistrationEmail);
                    break;

                case CommonValidator.ValidationResult.PhoneNumberEmpty:
                case CommonValidator.ValidationResult.PhoneInvalid:
                case CommonValidator.ValidationResult.PhoneNumberAlreadyExists:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtRegistrationPhoneNumber);
                    break;

                case CommonValidator.ValidationResult.NewPasswordEmpty:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtRegistrationCreatePassword);
                    break;

                case CommonValidator.ValidationResult.ConfirmPasswordEmpty:
                case CommonValidator.ValidationResult.NotMatchPassword:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtRegistrationConfirmPassword);
                    break;

                case CommonValidator.ValidationResult.WeakPassword:
                case CommonValidator.ValidationResult.MediumPassword:
                case CommonValidator.ValidationResult.StrongPassword:
                    ErrorHelper.ShowErrorBelowControl(txtRegistrationCreatePassword, "* Password must be very strong.");
                    txtRegistrationCreatePassword.Focus();
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    errorMsg = authUI.GetErrorMsg();
                    MessageBox.Show(string.IsNullOrWhiteSpace(errorMsg) ? "Registration failed. Please try again." : errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }


        private void lblLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {
            if (txtFullName.Text != "Enter Your Full Name" && !string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                ErrorHelper.HideErrorForControl(txtFullName);
            }
        }

        private void txtRegistrationEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtRegistrationEmail.Text != "Enter Your Email Address" && !string.IsNullOrWhiteSpace(txtRegistrationEmail.Text))
            {
                ErrorHelper.HideErrorForControl(txtRegistrationEmail);
            }
        }

        private void txtRegistrationConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            CheckPasswordMatch();
        }


        private void CheckPasswordMatch()
        {
            string newPass = txtRegistrationCreatePassword.Text;
            string confirmPass = txtRegistrationConfirmPassword.Text;

            Control targetControl = txtRegistrationConfirmPassword;
            if (txtRegistrationConfirmPassword.Parent != null && txtRegistrationConfirmPassword.Parent.Height < 50 && !(txtRegistrationConfirmPassword.Parent is Form))
            {
                targetControl = txtRegistrationConfirmPassword.Parent;
            }

            Control parent = targetControl.Parent;
            if (parent == null) return;

            string labelName = "lblErr_" + txtRegistrationConfirmPassword.Name;
            Label matchLabel = parent.Controls.Find(labelName, false).FirstOrDefault() as Label;

            // 1. If confirm password field is empty or placeholder
            // Keep "Confirm password is required" error visible if present; only hide previous match status
            if (string.IsNullOrWhiteSpace(confirmPass) || confirmPass == "Confirm Password")
            {
                if (matchLabel != null && (matchLabel.Text == "* Password match" || matchLabel.Text == "* Password doesn't match"))
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

            // 3. If create password field is empty or placeholder
            if (string.IsNullOrWhiteSpace(newPass) || newPass == "Create a Password")
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
        }











    }
}
