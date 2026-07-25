using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalExpenseCreditTracker.Modules.Settings.Category
{
    public partial class ExpenseAddCategoryControls : Form
    {
        ExpenseCategoryControls expenseCategoryControls;
        public ExpenseAddCategoryControls()
        {
            InitializeComponent();
            this.Resize += ExpenseAddDetailsControl_Resize;
            CenterPanel();
            this.txtCategory.Enter += new System.EventHandler(this.txtCategory_Enter);
            this.txtCategory.Leave += new System.EventHandler(this.txtCategory_Leave);
        }

        public ExpenseAddCategoryControls(ExpenseCategoryControls Obj)
        {
            InitializeComponent();
            expenseCategoryControls = Obj;
        }

        private void pnlAddExpenseCategory_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CenterPanel()
        {
            pnlAddExpenseCategory.Location = new Point(
                (this.ClientSize.Width - pnlAddExpenseCategory.Width) / 2,
                (this.ClientSize.Height - pnlAddExpenseCategory.Height) / 2
            );
        }
        private void ExpenseAddDetailsControl_Resize(object sender, EventArgs e)
        {
            CenterPanel();
        }

        private void ExpenseAddCategoryControls_Load(object sender, EventArgs e)
        {
            txtCategory.Text = "  Enter Category name";
            txtCategory.ForeColor = Color.FromArgb(45, 45, 45);
        }
        private void txtCategory_Enter(object sender, EventArgs e)
        {
            if (txtCategory.Text == "  Enter Category name")
            {
                txtCategory.Text = "";
                txtCategory.ForeColor = Color.FromArgb(45, 45, 45);
            }
        }
        private void txtCategory_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategory.Text))
            {
                txtCategory.Text = "  Enter Category name";
                txtCategory.ForeColor = Color.FromArgb(150, 150, 150);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Saved Expense Category");
        }
    }
}
