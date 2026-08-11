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
using System.Text.RegularExpressions;
using BLLayer.Common;
using PersonalExpenseCreditTracker.Common;
namespace PersonalExpenseCreditTracker.Forms.Authentication
{
    public partial class LoginControls : Form
    {
        bool isPasswordVisible = true;
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

            //Remember Me
            //if (Properties.Settings.Default.RememberMe && Properties.Settings.Default.RememberedUserId > 0)
            //{
            //    int userId = Properties.Settings.Default.RememberedUserId;

            //    Session.LogedInUser.SetUserId(userId);

            //    //open main Form
            //    MainForm mainForm = new MainForm();
            //    mainForm.Show();
            //    //Hide Login Form
            //    this.Hide();
            //}
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           // MessageBox.Show("Button click");
            if (txtLoginEmail.Text == "Enter Email Address" || txtLoginPassword.Text == "Enter Password")
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            // Clear all previous validation errors
            errorProvider1.Clear();

            // Create a new object to store the user's input
            AuthUI authUi = new AuthUI();

            authUi.email = (txtLoginEmail.Text == "Enter Email Address") ? "" : txtLoginEmail.Text;
            authUi.password = (txtLoginPassword.Text == "Enter Password") ? "" : txtLoginPassword.Text;

            //Call UI layer
            CommonValidator.ValidationResult result = authUi.LoginUserIntoAuthUi();
           
            // Perform action based on the validation result
            switch (result)
            {
                // Login successful
                case CommonValidator.ValidationResult.Success:
                    
                    MessageBox.Show(authUi.message);

                    ////Remember me
                    //if (checkBoxRememberMe.Checked)
                    //{
                    //    Properties.Settings.Default.RememberMe = true;
                    //    Properties.Settings.Default.RememberedUserId = authUi.userId;
                    //}
                    //else
                    //{
                    //    Properties.Settings.Default.RememberMe = false;
                    //    Properties.Settings.Default.RememberedUserId = 0;
                    //}

                    //Properties.Settings.Default.Save();
                    

                    //Store logged-in UserID in Session
                    Session.LogedInUser.SetUserId(Convert.ToInt32(authUi.userId));
                   
                    //Open Main Page
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                    this.Hide();
                    break;
                //Email validation Error
                case CommonValidator.ValidationResult.LoginEmailInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtLoginEmail);
                    break;

                    //password validation
                case CommonValidator.ValidationResult.LoginPasswordInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtLoginPassword);
                    break;
                case CommonValidator.ValidationResult.StoreProcedureError:
                    string errorMsg = string.IsNullOrWhiteSpace(authUi.message) ? "Login Unsuccessful." : authUi.message;
                     MessageBox.Show(errorMsg, "Login Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   // MessageBox.Show("Login Unsuccessful.");
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
            this.Close();
        }

        private void checkBoxRememberMe_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
