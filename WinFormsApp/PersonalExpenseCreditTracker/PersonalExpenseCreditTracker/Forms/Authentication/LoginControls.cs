using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalExpenseCreditTracker.Forms.Authentication
{
    public partial class LoginControls : Form
    {
        public LoginControls()
        {
            InitializeComponent();
        }
        private void textBox2_Event(object sender, EventArgs e)
        {
            if (txtEmail.Text == "Enter your email address")
            {
                txtEmail.Text = "";
                txtEmail.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
            {
                txtEmail.Text = "Enter your email address";
                txtEmail.ForeColor = Color.Gray;
            }
        }

        private void textBox3_Event(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Enter your password")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() == "")
            {
                txtPassword.Text = "Enter your password";
                txtPassword.ForeColor = Color.Gray;
            }
        }

        private void LoginControls_Load(object sender, EventArgs e)
        {
            txtEmail.Text = "Enter your email address";
            txtEmail.ForeColor = Color.Gray;
            txtEmail.BackColor = Color.White;
            txtEmail.SelectionStart = 0;
            txtEmail.SelectionLength = 0;

            txtPassword.Text = "Enter your password";
            txtPassword.ForeColor = Color.Gray;
            txtPassword.BackColor = Color.White;
            txtPassword.SelectionStart = 0;
            txtPassword.SelectionLength = 0;


            tableLayoutPanel1.Left = (this.ClientSize.Width - tableLayoutPanel1.Width) / 2;
            tableLayoutPanel1.Top = (this.ClientSize.Height - tableLayoutPanel1.Height) / 2;

        }

        private void LoginControls_Resize(object sender, EventArgs e)
        {

            tableLayoutPanel1.Left = (this.ClientSize.Width - tableLayoutPanel1.Width) / 2;
            tableLayoutPanel1.Top = (this.ClientSize.Height - tableLayoutPanel1.Height) / 2;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            AuthUI authUi = new AuthUI();
            authUi.email = txtEmail.Text;
            authUi.password = txtPassword.Text;

            bool result = authUi.LoginDataIntoAuthUi(authUi);
            if (result)
            {
                MessageBox.Show("Validation Success");
            }
            else
            {
                MessageBox.Show("Validation Failed");
            }
        }

        private void LblForgotPassword_Click(object sender, EventArgs e)
        {
            AuthUI authUi = new AuthUI();
            authUi.email = txtEmail.Text;

            bool result = authUi.ForgetPasswordIntoAuthUi();
            if (result)
            {
                MessageBox.Show("Validation Success");
            }
            else
            {
                MessageBox.Show("Validation Falied");
            }
        }

    }
}
