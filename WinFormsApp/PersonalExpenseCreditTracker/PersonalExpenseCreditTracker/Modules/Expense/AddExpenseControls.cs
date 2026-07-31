using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using BLLayer.Common;
using PersonalExpenseCreditTracker.Common;

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


            CommonUiFunction.LoadInComboBox("spGetAllCreditCategory", "Select Category", cmbAddExpenseCategory);
            CommonUiFunction.LoadInComboBox("spGetAllPaymentTypes", "Select PaymentType", cmbAddExpensePaymentType);
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
            // Clear all previous validation errors
            errorProvider1.Clear();

            // Create a new object to store the user's input
            ExpenseUI expenseUi = new ExpenseUI();

            // Assign values from the form controls to the object
            expenseUi.userId = Session.LogedInUser.GetUserId();
            expenseUi.expenseId = -1;

            expenseUi.categoryId = Convert.ToInt32(cmbAddExpenseCategory.SelectedValue);
            expenseUi.subCategoryId = Convert.ToInt32(cmbAddExpenseSubCategory.SelectedValue);
            expenseUi.paymentId = Convert.ToInt32(cmbAddExpensePaymentType.SelectedValue);


            // If the placeholder text is still present, pass an empty string
            expenseUi.amount = (txtAddExpenseAmount.Text == "Select Amount") ? "" : txtAddExpenseAmount.Text;
            expenseUi.description = (txtAddExpenseDescription.Text == "Enter Description") ? "" : txtAddExpenseDescription.Text;

            CommonValidator.ValidationResult result = expenseUi.InsertDataIntoExpenseUi();

            // Perform action based on the validation result
            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show("Expense added successfully!");
                    this.Close();
                    break;

                case CommonValidator.ValidationResult.CategoryInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, cmbAddExpenseCategory);
                    break;

                case CommonValidator.ValidationResult.SubCategoryInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, cmbAddExpenseSubCategory);
                    break;

                case CommonValidator.ValidationResult.AmountEmpty:
                case CommonValidator.ValidationResult.AmountInvalid:
                case CommonValidator.ValidationResult.AmountTooLarge:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtAddExpenseAmount);
                    break;

                case CommonValidator.ValidationResult.PaymentInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, cmbAddExpensePaymentType);
                    break;     

                case CommonValidator.ValidationResult.DescriptionInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtAddExpenseDescription);
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    MessageBox.Show("Expense added unsuccessfully!");
                    break;
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

        private void cmbAddExpenseCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAddExpenseCategory.SelectedValue == null)
                return;

            if (cmbAddExpenseCategory.SelectedValue is DataRowView)
                return;

            int categoryId = Convert.ToInt32(cmbAddExpenseCategory.SelectedValue);

            CommonUiFunction.LoadInComboBox(
                "spGetExpenseSubCategoryByCategoryID",
                "Select SubCategory",
                cmbAddExpenseSubCategory,
                "@CategoryID",
                categoryId);
        }
    }
}
