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
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtLoginEmail.Text == "Enter Email Address" || txtLoginPassword.Text == "Enter Password")
                MessageBox.Show("Please fill all fields");

            this.Close();
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
    }
}
