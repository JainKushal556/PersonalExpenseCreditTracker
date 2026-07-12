using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalExpenseCreditTracker.Modules.Credit
{
    public partial class CreditDetailsControl : Form
    {
        public CreditDetailsControl()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.CreditDetailsControl_Load);
            pnlCreditDetailsContent.Resize += (s, e) => CenterTableLayout();
            this.txtAddCreditAmount.Enter += new System.EventHandler(this.txtAmount_Enter);
            this.txtAddCreditAmount.Leave += new System.EventHandler(this.txtAmount_Leave);
            this.txtAddCreditDescription.Enter += new System.EventHandler(this.txtDescription_Enter);
            this.txtAddCreditDescription.Leave += new System.EventHandler(this.txtDescription_Leave);

        }

        private void txtCategory_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnlDescription_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblDescription_Click(object sender, EventArgs e)
        {

        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void pnlCreditDetailsContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CreditDetailsControl_Load(object sender, EventArgs e)
        {
            txtAddCreditAmount.Text = "Enter Amount";
            txtAddCreditAmount.ForeColor = Color.Gray;
            txtAddCreditDescription.Text = "Enter Description";
            txtAddCreditDescription.ForeColor = Color.Gray;
            CenterTableLayout();
            cmbAddCreditCategory.ForeColor = Color.Gray;
            cmbAddCreditCategory.Text = "Select Category";
            cmbAddCreditSubCategory.ForeColor = Color.Gray;
            cmbAddCreditSubCategory.Text = "Select SubCategory";
            cmbAddCreditPaymentType.ForeColor = Color.Gray;
            cmbAddCreditPaymentType.Text = "Select PaymentType";



        }
        private void txtAmount_Enter(object sender, EventArgs e)
        {
            if (txtAddCreditAmount.Text == "Enter Amount")
            {
                txtAddCreditAmount.Text = "";
                txtAddCreditAmount.ForeColor = Color.Black;
            }
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddCreditAmount.Text))
            {
                txtAddCreditAmount.Text = "Enter Amount";
                txtAddCreditAmount.ForeColor = Color.Gray;
            }
        }

        private void txtDescription_Enter(object sender, EventArgs e)
        {
            if (txtAddCreditDescription.Text == "Enter Description")
            {
                txtAddCreditDescription.Text = "";
                txtAddCreditDescription.ForeColor = Color.Black;
            }
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddCreditDescription.Text))
            {
                txtAddCreditDescription.Text = "Enter Description";
                txtAddCreditDescription.ForeColor = Color.Gray;
            }
        }

        private void cmbCreditCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void CenterTableLayout()
        {
            tlpAddCredit.Location = new Point(
                (pnlCreditDetailsContent.ClientSize.Width - tlpAddCredit.Width) / 2,
                (pnlCreditDetailsContent.ClientSize.Height - tlpAddCredit.Height) / 2
            );
        }

        private void CreditDetailsControl_Resize(object sender, EventArgs e)
        {
            CenterTableLayout();
        }
        private void cmbAddCreditCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAddCreditCategory.ForeColor = Color.Gray;
        }
        private void cmbAddCreditSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAddCreditSubCategory.ForeColor = Color.Gray;
        }

        private void cmbAddCreditPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAddCreditPaymentType.ForeColor = Color.Gray;
        }
    }
}