using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalExpenseCreditTracker.Modules.Expense
{
    public partial class ExpenseDetailsControl : Form
    {
        public ExpenseDetailsControl()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.ExpenseDetailsControl_Load);
            pnlExpenseDetailsContent.Resize += (s, e) => CenterTableLayout();
            this.txtAddExpenseAmount.Enter += new System.EventHandler(this.txtAmount_Enter);
            this.txtAddExpenseAmount.Leave += new System.EventHandler(this.txtAmount_Leave);
            this.txtAddExpenseDescription.Enter += new System.EventHandler(this.txtDescription_Enter);
            this.txtAddExpenseDescription.Leave += new System.EventHandler(this.txtDescription_Leave);
           
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

        private void pnlExpenseDetailsContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ExpenseDetailsControl_Load(object sender, EventArgs e)
        {
            txtAddExpenseAmount.Text = "Enter Amount";
            txtAddExpenseAmount.ForeColor = Color.Gray;
            txtAddExpenseDescription.Text = "Enter Description";
            txtAddExpenseDescription.ForeColor = Color.Gray;
            CenterTableLayout();
            cmbAddExpenseCategory.ForeColor = Color.Gray;
            cmbAddExpenseCategory.Text = "Select Category";
            cmbAddExpenseSubCategory.ForeColor = Color.Gray;
            cmbAddExpenseSubCategory.Text = "Select SubCategory";
            cmbAddExpensePaymentType.ForeColor = Color.Gray;
            cmbAddExpensePaymentType.Text = "Select Category";

           
            
        }
        private void txtAmount_Enter(object sender, EventArgs e)
        {
            if (txtAddExpenseAmount.Text == "Enter Amount")
            {
                txtAddExpenseAmount.Text = "";
                txtAddExpenseAmount.ForeColor = Color.Black;
            }
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddExpenseAmount.Text))
            {
                txtAddExpenseAmount.Text = "Enter Amount";
                txtAddExpenseAmount.ForeColor = Color.Gray;
            }
        }

        private void txtDescription_Enter(object sender, EventArgs e)
        {
            if (txtAddExpenseDescription.Text == "Enter Description")
            {
                txtAddExpenseDescription.Text = "";
                txtAddExpenseDescription.ForeColor = Color.Black;
            }
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddExpenseDescription.Text))
            {
                txtAddExpenseDescription.Text = "Enter Description";
                txtAddExpenseDescription.ForeColor = Color.Gray;
            }
        }

        private void cmbExpenseCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void CenterTableLayout()
        {
            tlpAddExpense.Location = new Point(
                (pnlExpenseDetailsContent.ClientSize.Width - tlpAddExpense.Width) / 2,
                (pnlExpenseDetailsContent.ClientSize.Height - tlpAddExpense.Height) / 2
            );
        }
        
        private void ExpenseDetailsControl_Resize(object sender, EventArgs e)
        {
            CenterTableLayout();
        }
        private void cmbAddExpenseCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAddExpenseCategory.ForeColor = Color.Gray;
        }
        private void cmbAddExpenseSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAddExpenseSubCategory.ForeColor = Color.Gray;
        }

        private void cmbAddExpensePaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAddExpensePaymentType.ForeColor = Color.Gray;
        }

        private void pnlAddExpenseHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
