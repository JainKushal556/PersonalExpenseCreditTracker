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
            if (txtRegistrationCreatePassword.Text == "" || txtRegistrationCreatePassword.Text == "Create a password")
            {
                pnlWeak.BackColor = Color.FromArgb(234, 235, 239);
                pnlMedium.BackColor = Color.FromArgb(234, 235, 239);
                pnlStrong.BackColor = Color.FromArgb(234, 235, 239);
                pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);

                return;
            }

            int score = CheckPasswordStrengthLevel(txtRegistrationCreatePassword.Text);

            if (score <= 2)
            {
                lblPasswordStrengthLevel.Text = "Weak";
                lblPasswordStrengthLevel.ForeColor = Color.Red;
                pnlWeak.BackColor = Color.Red;
                pnlMedium.BackColor = Color.FromArgb(234, 235, 239);
                pnlStrong.BackColor = Color.FromArgb(234, 235, 239);
                pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);
            }
            else if (score == 3)
            {
                lblPasswordStrengthLevel.Text = "Medium";
                lblPasswordStrengthLevel.ForeColor = Color.Orange;
                pnlWeak.BackColor = Color.Orange;
                pnlMedium.BackColor = Color.Orange;
                pnlStrong.BackColor = Color.FromArgb(234, 235, 239);
                pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);
            }
            else if (score == 4)
            {
                lblPasswordStrengthLevel.Text = "Strong";
                lblPasswordStrengthLevel.ForeColor = Color.YellowGreen;
                pnlWeak.BackColor = Color.YellowGreen;
                pnlMedium.BackColor = Color.YellowGreen;
                pnlStrong.BackColor = Color.YellowGreen;
                pnlVeryStrong.BackColor = Color.FromArgb(234, 235, 239);
            }
            else
            {
                lblPasswordStrengthLevel.Text = "Very Strong";
                lblPasswordStrengthLevel.ForeColor = Color.Green;
                pnlWeak.BackColor = Color.Green;
                pnlMedium.BackColor = Color.Green;
                pnlStrong.BackColor = Color.Green;
                pnlVeryStrong.BackColor = Color.Green;
            }
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
            this.Close();
        }
    }
}
