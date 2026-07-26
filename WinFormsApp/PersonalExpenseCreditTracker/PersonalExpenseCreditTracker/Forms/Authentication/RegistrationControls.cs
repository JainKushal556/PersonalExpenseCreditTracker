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
    public partial class RegistrationControls : Form
    {
        public RegistrationControls()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            AuthUI authUi = new AuthUI();
            authUi.userName = txtUserName.Text;
            authUi.email = txtEmail.Text;
            authUi.password = txtPassword.Text;

            bool result = authUi.InsertDataIntoAuthUi(authUi);
            if (result)
            {
                MessageBox.Show("Validation Passed");
            }
            else
            {
                MessageBox.Show("Validation Failed");
            }
        }
    }
}
