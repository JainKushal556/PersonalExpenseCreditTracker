using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PersonalExpenseCreditTracker.Modules.Expense
{
    public partial class AddExpenseControls : Form
    {
        public AddExpenseControls()
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

        private void ExpenseDetailsControl_Load(object sender, EventArgs e)
        {
            txtAddExpenseAmount.Text = "Enter Amount";
            txtAddExpenseAmount.ForeColor = Color.Gray;
            txtAddExpenseDescription.Text = "Enter Description";
            txtAddExpenseDescription.ForeColor = Color.Gray;
            cmbAddExpenseCategory.Text = "Select Category";
            cmbAddExpenseCategory.ForeColor = Color.Gray;
            cmbAddExpenseSubCategory.Text = "Select Sub Category";
            cmbAddExpenseSubCategory.ForeColor = Color.Gray;
            cmbAddExpensePaymentType.Text = "Select Payment Type";
            cmbAddExpensePaymentType.ForeColor = Color.Gray;
        }

        // All Border Cornar Radius
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCloseEditPersonDetails_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveExpense_Click(object sender, EventArgs e)
        {
            if (txtAddExpenseAmount.Text == "Enter Amount" || txtAddExpenseDescription.Text == "Enter Description"
                || cmbAddExpenseCategory.Text == "Select Category" || cmbAddExpensePaymentType.Text == "Select Payment Type"
                || cmbAddExpenseSubCategory.Text == "Select Sub Category")
            {
                MessageBox.Show("Please fill all fields");
            }
            else
            {
                MessageBox.Show("Expense Details Added Successfully...");
                this.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAddExpenseAmount.Text = "Enter Amount";
            txtAddExpenseAmount.ForeColor = Color.Gray;
            txtAddExpenseDescription.Text = "Enter Description";
            txtAddExpenseDescription.ForeColor = Color.Gray;
            cmbAddExpenseCategory.Text = "Select Category";
            cmbAddExpenseCategory.ForeColor = Color.Gray;
            cmbAddExpenseSubCategory.Text = "Select Sub Category";
            cmbAddExpenseSubCategory.ForeColor = Color.Gray;
            cmbAddExpensePaymentType.Text = "Select Payment Type";
            cmbAddExpensePaymentType.ForeColor = Color.Gray;
        }

        private void cmbAddExpenseCategory_Enter(object sender, EventArgs e)
        {
            if (cmbAddExpenseCategory.Text == "Select Category")
            cmbAddExpenseCategory.ForeColor = Color.Black;
        }

        private void cmbAddExpenseCategory_Leave(object sender, EventArgs e)
        {
            if (cmbAddExpenseCategory.SelectedIndex == -1 || cmbAddExpenseCategory.Text == "Select Category")
            {
                cmbAddExpenseCategory.Text = "Select Category";
                cmbAddExpenseCategory.ForeColor = Color.Gray;
            }
        }

        private void cmbAddExpenseSubCategory_Enter(object sender, EventArgs e)
        {
            if (cmbAddExpenseSubCategory.Text == "Select Sub Category")
                cmbAddExpenseSubCategory.ForeColor = Color.Black;
        }

        private void cmbAddExpenseSubCategory_Leave(object sender, EventArgs e)
        {
            if (cmbAddExpenseSubCategory.SelectedIndex == -1 || cmbAddExpenseSubCategory.Text == "Select Sub Category")
            {
                cmbAddExpenseSubCategory.Text = "Select Sub Category";
                cmbAddExpenseSubCategory.ForeColor = Color.Gray;
            }
        }

        private void cmbAddExpensePaymentType_Enter(object sender, EventArgs e)
        {
            if (cmbAddExpensePaymentType.Text == "Select Payment Type")
                cmbAddExpensePaymentType.ForeColor = Color.Black;
        }

        private void cmbAddExpensePaymentType_Leave(object sender, EventArgs e)
        {
            if (cmbAddExpensePaymentType.SelectedIndex == -1 || cmbAddExpensePaymentType.Text == "Select Payment Type")
            {
                cmbAddExpensePaymentType.Text = "Select Payment Type";
                cmbAddExpensePaymentType.ForeColor = Color.Gray;
            }
        }

        private void btnSaveExpense_Resize(object sender, EventArgs e)
        {
            SetRadius(btnSaveExpense, 5);
        }

        private void btnCancel_Resize(object sender, EventArgs e)
        {
            SetRadius(btnCancel, 5);
        }

        private void btnLentAddClear_Resize(object sender, EventArgs e)
        {
            SetRadius(btnClear, 5);
        }

        private void cmbAddExpenseSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
    }
}
