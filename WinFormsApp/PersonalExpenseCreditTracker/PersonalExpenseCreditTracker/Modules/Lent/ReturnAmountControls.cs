using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalExpenseCreditTracker.Modules.Lent
{
    public partial class ReturnAmountControls : Form
    {
        public ReturnAmountControls()
        {
            InitializeComponent();
            this.rtxtDescription.Enter += new System.EventHandler(this.rtxtDescription_Leave);
            this.rtxtDescription.Leave += new System.EventHandler(this.rtxtDescription_Enter);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveReturn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Saved Lent Person");
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }
        private void txtReturnAmount_Enter(object sender, EventArgs e)
        {
            if (txtReturnAmount.Text == "Enter Amount")
            {
                txtReturnAmount.Text = "";
            }
        }


        private void txtReturnAmount_Leave(object sender, EventArgs e)
        {
            if (txtReturnAmount.Text == "")
            {
                txtReturnAmount.Text = "Enter Amount";
            }
        }
        private void rtxtDescription_Enter(object sender, EventArgs e)
        {
            if (rtxtDescription.Text == "Enter Description")
            {
                rtxtDescription.Text = "";
            }
        }


        private void rtxtDescription_Leave(object sender, EventArgs e)
        {
            if (rtxtDescription.Text == "")
            {
                rtxtDescription.Text = "Enter Description";
            }
        }

        private void ReturnAmountControls_Load(object sender, EventArgs e)
        {
            txtReturnAmount.Text = "  Enter Amount";
            rtxtDescription.Text = "  Enter Description";
        }
    }
}
