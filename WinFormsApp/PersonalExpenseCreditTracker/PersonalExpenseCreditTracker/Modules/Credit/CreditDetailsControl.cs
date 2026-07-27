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
            //txtAddCreditDescription.Text = "Enter Description";
            txtAddCreditDescription.ForeColor = Color.Gray;
            CenterTableLayout();
            cmbAddCreditCategory.ForeColor = Color.Gray;
            cmbAddCreditCategory.Text = "Select Category";
            cmbAddCreditSubCategory.ForeColor = Color.Gray;
            cmbAddCreditSubCategory.Text = "Select SubCategory";
            cmbAddCreditPaymentType.ForeColor = Color.Gray;
            cmbAddCreditPaymentType.Text = "Select PaymentType";

            CommonUiFunction.LoadInComboBox("spGetAllCreditCategory", "Select Category", cmbAddCreditCategory);
         //CommonUiFunction.LoadInComboBox("spGetCreditSubCategoryByCategoryID", "Select SubCategory", cmbAddCreditSubCategory, "@CategoryID", Convert.ToInt32(cmbAddCreditCategory.SelectedValue));
           //MessageBox.Show(Convert.ToInt32(cmbAddCreditCategory.SelectedValue).ToString());
            CommonUiFunction.LoadInComboBox("spGetAllPaymentTypes", "Select PaymentType", cmbAddCreditPaymentType);


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
        //private void cmbAddCreditCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    cmbAddCreditCategory.ForeColor = Color.Gray;
        //}
        private void cmbAddCreditSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAddCreditSubCategory.ForeColor = Color.Gray;
        }

        private void cmbAddCreditPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAddCreditPaymentType.ForeColor = Color.Gray;
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