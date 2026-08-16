using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PersonalExpenseCreditTracker.Forms.Main;
using PersonalExpenseCreditTracker.Common;
using BLLayer.Authentication;
using BLLayer.Common;
using PersonalExpenseCreditTracker.Forms.Main;

namespace PersonalExpenseCreditTracker.Forms.Authentication
{
    public partial class LoginControls : Form
    {
        bool isPasswordVisible = true;

        internal protected int UserId { get; set; }

        public LoginControls()
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
        
        private void LoginControls_Load(object sender, EventArgs e)
        {
            SetRadius(pnlLoginDataInput, 20);
            SetRadius(btnLogin, 17);

            picEye.Image = Properties.Resources.open_eye__2_;

            txtLoginEmail.Text = "Enter Email Address";
            txtLoginPassword.Text = "Enter Password";

            txtLoginEmail.ForeColor = Color.Gray;
            txtLoginPassword.ForeColor = Color.Gray;
            this.ActiveControl = pnlLoginDataInput;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            AuthUI authUI = new AuthUI();
            AuthBLL authBLL = new AuthBLL();
            string ErroeMsg;

            authUI.email = (txtLoginEmail.Text == "Enter Email Address") ? "" : txtLoginEmail.Text;
            authUI.password = (txtLoginPassword.Text == "Enter Password") ? "" : txtLoginPassword.Text;

            CommonValidator.ValidationResult result = authUI.LoginDataIntoAuthUI();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    UserId = authBLL.GetUserIdFromDB();
                    Session.LogedInUser.SetUserId(UserId);

                    this.Close(); 
                    break;

                case CommonValidator.ValidationResult.EmailInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtLoginEmail);
                    break;

                case CommonValidator.ValidationResult.NewPasswordEmpty:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtLoginPassword);
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    ErroeMsg = authUI.GetErrorMsgForLogin();
                    MessageBox.Show(ErroeMsg);
                    break;
            }
        }

        private void picEye_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;

            txtLoginPassword.UseSystemPasswordChar = !isPasswordVisible;

            if (isPasswordVisible)
            {
                picEye.Image = Properties.Resources.open_eye__2_;
            }
            else
            {
                picEye.Image = Properties.Resources.eye;
            }
        }

        private void txtLoginEmail_Enter(object sender, EventArgs e)
        {
            if (txtLoginEmail.Text == "Enter Email Address")
            {
                txtLoginEmail.Text = "";
                txtLoginEmail.ForeColor = Color.Black;
            }
        }

        private void txtLoginEmail_Leave(object sender, EventArgs e)
        {
            if (txtLoginEmail.Text == "")
            {
                txtLoginEmail.Text = "Enter Email Address";
                txtLoginEmail.ForeColor = Color.Gray;
            }
        }

        private void txtLoginPassword_Enter(object sender, EventArgs e)
        {
            if (txtLoginPassword.Text == "Enter Password")
            {
                txtLoginPassword.Text = "";
                txtLoginPassword.ForeColor = Color.Black;
            }
        }

        private void txtLoginPassword_Leave(object sender, EventArgs e)
        {
            if (txtLoginPassword.Text == "")
            {
                txtLoginPassword.Text = "Enter Password";
                txtLoginPassword.ForeColor = Color.Gray;
            }
        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            ForgotPasswordControls forgotPasswordControls = new ForgotPasswordControls();
            forgotPasswordControls.ShowDialog();
        }

        private void lblCreateAccount_Click(object sender, EventArgs e)
        {
            this.Hide();

            RegistrationControls registrationControls = new RegistrationControls();
            registrationControls.ShowDialog();

            this.Show();
        }
    }
}
