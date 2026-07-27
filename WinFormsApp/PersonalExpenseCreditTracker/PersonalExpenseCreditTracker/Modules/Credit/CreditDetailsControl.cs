using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PersonalExpenseCreditTracker.Common;
using BLLayer.Common;
using System.Runtime.InteropServices;
namespace PersonalExpenseCreditTracker.Modules.Credit
{
    public partial class CreditDetailsControl : Form
    {
        public CreditDetailsControl()
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

        private void CreditDetailsControl_Load(object sender, EventArgs e)
        {
            txtAddCreditAmount.Text = "Enter Amount";
            txtAddCreditAmount.ForeColor = Color.Gray;
            //txtAddCreditDescription.Text = "Enter Description";
            txtAddCreditDescription.ForeColor = Color.Gray;
            cmbAddCreditCategory.Text = "Select Category";
            cmbAddCreditCategory.ForeColor = Color.Gray;
            cmbAddCreditSubCategory.Text = "Select Sub Category";
            cmbAddCreditSubCategory.ForeColor = Color.Gray;
            cmbAddCreditPaymentType.Text = "Select Category";
            cmbAddCreditPaymentType.ForeColor = Color.Gray;
            cmbAddCreditPaymentType.Text = "Select PaymentType";

            CommonUiFunction.LoadInComboBox("spGetAllCreditCategory", "Select Category", cmbAddCreditCategory);
            CommonUiFunction.LoadInComboBox("spGetAllPaymentTypes", "Select PaymentType", cmbAddCreditPaymentType);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAddCreditAmount.Text = "Enter Amount";
            txtAddCreditAmount.ForeColor = Color.Gray;
            txtAddCreditDescription.Text = "Enter Description";
            txtAddCreditDescription.ForeColor = Color.Gray;
            cmbAddCreditCategory.Text = "Select Category";
            cmbAddCreditCategory.ForeColor = Color.Gray;
            cmbAddCreditSubCategory.Text = "Select Sub Category";
            cmbAddCreditSubCategory.ForeColor = Color.Gray;
            cmbAddCreditPaymentType.Text = "Select Payment Type";
            cmbAddCreditPaymentType.ForeColor = Color.Gray;
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

        private void cmbAddCreditCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAddCreditCategory.SelectedValue == null)
                return;

            if (cmbAddCreditCategory.SelectedValue is DataRowView)
                return;

            int categoryId = Convert.ToInt32(cmbAddCreditCategory.SelectedValue);

            CommonUiFunction.LoadInComboBox(
                "spGetCreditSubCategoryByCategoryID",
                "Select SubCategory",
                cmbAddCreditSubCategory,
                "@CategoryID",
                categoryId);
        }

        private void cmbAddCreditCategory_Enter(object sender, EventArgs e)
        {
            cmbAddCreditCategory.ForeColor = Color.Black;
        }

        private void cmbAddCreditCategory_Leave(object sender, EventArgs e)
        {
            if (cmbAddCreditCategory.Text == "Select Category")
                cmbAddCreditCategory.ForeColor = Color.Gray;
        }

        private void cmbAddCreditSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAddCreditSubCategory.ForeColor = Color.Gray;
        }

        private void cmbAddCreditSubCategory_Enter(object sender, EventArgs e)
        {
            if (cmbAddCreditSubCategory.Text == "Select Sub Category")
                cmbAddCreditSubCategory.ForeColor = Color.Black;
        }

        private void cmbAddCreditSubCategory_Leave(object sender, EventArgs e)
        {
            if (cmbAddCreditSubCategory.SelectedIndex == -1)
            {
                cmbAddCreditSubCategory.Text = "Select Sub Category";
                cmbAddCreditSubCategory.ForeColor = Color.Gray;
            }
        }

        private void cmbAddCreditPaymentType_Enter(object sender, EventArgs e)
        {
            if (cmbAddCreditPaymentType.Text == "Select Payment Type")
                cmbAddCreditPaymentType.ForeColor = Color.Black;
        }

        private void cmbAddCreditPaymentType_Leave(object sender, EventArgs e)
        {
            if (cmbAddCreditPaymentType.SelectedIndex == -1)
            {
                cmbAddCreditPaymentType.Text = "Select Payment Type";
                cmbAddCreditPaymentType.ForeColor = Color.Gray;
            }
        }

        private void btnSaveCredit_Resize(object sender, EventArgs e)
        {
            SetRadius(btnSaveCredit, 5);
        }

        private void btnCancel_Resize(object sender, EventArgs e)
        {
            SetRadius(btnCancel, 5);
        }

        private void btnClear_Resize(object sender, EventArgs e)
        {
            SetRadius(btnClear, 5);
        }

        private void btnSaveCredit_Click(object sender, EventArgs e)
        {
            // Clear all previous validation errors
             errorProvider1.Clear();

            // Create a new object to store the user's input
             CreditUI creditUi = new CreditUI();

            // Assign values from the form controls to the object
             creditUi.userId = Session.LogedInUser.GetUserId();
             creditUi.creditId = -1;

             creditUi.categoryId = Convert.ToInt32(cmbAddCreditCategory.SelectedValue);
             creditUi.subCategoryId = Convert.ToInt32(cmbAddCreditSubCategory.SelectedValue);
             creditUi.paymentId = Convert.ToInt32(cmbAddCreditPaymentType.SelectedValue);


            // If the placeholder text is still present, pass an empty string
             creditUi.amount = (txtAddCreditAmount.Text == "Select Amount") ? "" : txtAddCreditAmount.Text;
             creditUi.description = (txtAddCreditDescription.Text == "Enter description") ? "" : txtAddCreditDescription.Text;

             CommonValidator.ValidationResult result = creditUi.InsertDataIntoCreditUi();
             // Perform action based on the validation result
             switch (result)
             {
                 case CommonValidator.ValidationResult.Success:
                     MessageBox.Show("Credit added successfully!");
                     this.Close();
                     break;

                 case CommonValidator.ValidationResult.CategoryInvalid:
                     ErrorHelper.ShowValidationError(result, errorProvider1, cmbAddCreditCategory);
                     break;

                 case CommonValidator.ValidationResult.SubCategoryInvalid:
                     ErrorHelper.ShowValidationError(result, errorProvider1, cmbAddCreditSubCategory);
                     break;

                 case CommonValidator.ValidationResult.PaymentInvalid:
                     ErrorHelper.ShowValidationError(result, errorProvider1, cmbAddCreditPaymentType);
                     break;

                 case CommonValidator.ValidationResult.AmountEmpty:
                 case CommonValidator.ValidationResult.AmountInvalid:
                 case CommonValidator.ValidationResult.AmountTooLarge:
                     ErrorHelper.ShowValidationError(result, errorProvider1, txtAddCreditAmount);
                     break;

                 case CommonValidator.ValidationResult.DescriptionInvalid:
                     ErrorHelper.ShowValidationError(result, errorProvider1, txtAddCreditDescription);
                     break;

                 case CommonValidator.ValidationResult.StoreProcedureError:
                     MessageBox.Show("Credit added unsuccessfully!");
                     break;
             }
        }

        private void cmbAddCreditSubCategory_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}