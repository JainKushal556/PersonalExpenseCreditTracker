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
using PersonalExpenseCreditTracker.Modules.Settings.Category;
namespace PersonalExpenseCreditTracker.Modules.Credit
{
    
    public partial class AddCreditControls : Form
    {
        private bool ignoreEvents = true;
        public AddCreditControls()
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
            txtAddCreditDescription.Text = "Enter Description";
            txtAddCreditDescription.ForeColor = Color.Gray;
            cmbAddCreditCategory.Text = "Select Category";
            cmbAddCreditCategory.ForeColor = Color.Gray;
            cmbAddCreditSubCategory.Text = "Select Sub Category";
            cmbAddCreditSubCategory.ForeColor = Color.Gray;
            cmbAddCreditPaymentType.Text = "Select Payment Type";
            cmbAddCreditPaymentType.ForeColor = Color.Gray;

           // CommonUiFunction.LoadInComboBox("spGetAllCreditCategory", "Select Category", "+ Add New Category", cmbAddCreditCategory);
            Common.CommonUiFunction.LoadInComboBox("spGetCreditCategoriesByUserID", Session.LogedInUser.GetUserId(), "Select Category", "+ Add New Cetegory", cmbAddCreditCategory);
            CommonUiFunction.LoadInComboBox("spGetAllPaymentTypes", "Select Payment Type", cmbAddCreditPaymentType);
            cmbAddCreditPaymentType.MouseClick += (s, ev) => { cmbAddCreditPaymentType.DroppedDown = true; };
            ignoreEvents = false;
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
            ErrorHelper.ClearAllErrors(this);
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
            ErrorHelper.HideErrorForControl(cmbAddCreditCategory);
            cmbAddCreditCategory.AutoCompleteMode = AutoCompleteMode.Append;
            cmbAddCreditCategory.AutoCompleteSource = AutoCompleteSource.ListItems;

            if (cmbAddCreditCategory.SelectedValue == null)
                return;

            int categoryId = 0;
            DataRowView drv = cmbAddCreditCategory.SelectedValue as DataRowView;
            if (drv != null)
            {
                categoryId = Convert.ToInt32(drv[0]);
            }
            else
            {
                categoryId = Convert.ToInt32(cmbAddCreditCategory.SelectedValue);
            }

            if (categoryId == -99)
            {
                this.Hide();
                using (var addCategoryForm = new PersonalExpenseCreditTracker.Modules.Settings.Category.CreditAddCategoryControls())
                {
                    DialogResult result = addCategoryForm.ShowDialog();
                    this.Show();

                    if (result == DialogResult.OK)
                    {

                        CommonUiFunction.LoadInComboBox("spGetAllCreditCategory", "Select Category", "+ Add New Category", cmbAddCreditCategory);

                        if (!string.IsNullOrEmpty(addCategoryForm.AddedCategoryName))
                        {
                            int index = cmbAddCreditCategory.FindStringExact(addCategoryForm.AddedCategoryName);
                            if (index != -1)
                            {
                                cmbAddCreditCategory.SelectedIndex = index;
                                cmbAddCreditCategory.ForeColor = Color.Black;
                            }
                            else
                            {
                                cmbAddCreditCategory.SelectedIndex = 0;
                                cmbAddCreditCategory.ForeColor = Color.Gray;
                            }
                        }
                        else
                        {
                            cmbAddCreditCategory.SelectedIndex = 0;
                            cmbAddCreditCategory.ForeColor = Color.Gray;
                        }
                    }
                    else
                    {
                        cmbAddCreditCategory.SelectedIndex = 0;
                        cmbAddCreditCategory.ForeColor = Color.Black;
                    }
                }
            }
            else if (categoryId > 0)
            {
               
                CommonUiFunction.LoadInComboBox(
                    "spGetCreditSubCategoryByCategoryID",
                    "Select Sub Category",
                    "+ Add New Sub Category",
                    cmbAddCreditSubCategory,
                    "@CategoryID",
                    categoryId);
            }
            else
            {
                cmbAddCreditSubCategory.DataSource = null;
                cmbAddCreditSubCategory.Items.Clear();
                cmbAddCreditSubCategory.Text = "Select Sub Category";
                cmbAddCreditSubCategory.ForeColor = Color.Gray;
            }
        }


        private void cmbAddCreditCategory_Enter(object sender, EventArgs e)
        {
            cmbAddCreditCategory.ForeColor = Color.Black;
        }
        private void cmbAddCreditCategory_Leave(object sender, EventArgs e)
        {
           
            if (cmbAddCreditCategory.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(cmbAddCreditCategory.Text) || cmbAddCreditCategory.Text == "Select Category")
            {
                cmbAddCreditCategory.SelectedIndex = 0;
                cmbAddCreditCategory.Text = "Select Category";
                cmbAddCreditCategory.ForeColor = Color.Gray;
            }
            else
            {
                cmbAddCreditCategory.ForeColor = Color.Black;
            }
        }


        //private void cmbAddCreditSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    cmbAddCreditSubCategory.ForeColor = Color.Black;
        //}

        private void cmbAddCreditSubCategory_Enter(object sender, EventArgs e)
        {
            if (cmbAddCreditSubCategory.Text == "Select Sub Category")
                cmbAddCreditSubCategory.ForeColor = Color.Black;
        }

        private void cmbAddCreditSubCategory_Leave(object sender, EventArgs e)
        {
            if (cmbAddCreditSubCategory.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(cmbAddCreditSubCategory.Text) || cmbAddCreditSubCategory.Text == "Select Sub Category" || cmbAddCreditSubCategory.Text == "Select SubCategory")
            {
                
                if (cmbAddCreditSubCategory.Items.Count > 0)
                {
                    cmbAddCreditSubCategory.SelectedIndex = 0;
                }

                cmbAddCreditSubCategory.Text = "Select Sub Category";
                cmbAddCreditSubCategory.ForeColor = Color.Gray;
            }
            else
            {
                cmbAddCreditSubCategory.ForeColor = Color.Black;
            }
        }



        private void cmbAddCreditPaymentType_Enter(object sender, EventArgs e)
        {
            if (cmbAddCreditPaymentType.Text == "Select Payment Type")
                cmbAddCreditPaymentType.ForeColor = Color.Black;
        }

        private void cmbAddCreditPaymentType_Leave(object sender, EventArgs e)
        {
            if (cmbAddCreditPaymentType.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(cmbAddCreditPaymentType.Text) || cmbAddCreditPaymentType.Text == "Select Payment Type")
            {
                cmbAddCreditPaymentType.SelectedIndex = 0;
                cmbAddCreditPaymentType.Text = "Select Payment Type";
                cmbAddCreditPaymentType.ForeColor = Color.Gray;
            }
            else
            {
                cmbAddCreditPaymentType.ForeColor = Color.Black;
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
            errorProvider1.Clear();

            CreditUI creditUi = new CreditUI();

            creditUi.userId = Session.LogedInUser.GetUserId();
            creditUi.creditId = -1;

            creditUi.categoryId = Convert.ToInt32(cmbAddCreditCategory.SelectedValue);
            creditUi.subCategoryId = Convert.ToInt32(cmbAddCreditSubCategory.SelectedValue);
            creditUi.paymentId = Convert.ToInt32(cmbAddCreditPaymentType.SelectedValue);

            creditUi.amount = (txtAddCreditAmount.Text == "Enter Amount" || txtAddCreditAmount.Text == "Select Amount") ? "" : txtAddCreditAmount.Text;
            creditUi.description = (txtAddCreditDescription.Text == "Enter Description" || txtAddCreditDescription.Text == "Enter description") ? "" : txtAddCreditDescription.Text;

            CommonValidator.ValidationResult result = creditUi.InsertDataIntoCreditUi();

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

                case CommonValidator.ValidationResult.AmountEmpty:
                case CommonValidator.ValidationResult.AmountInvalid:
                case CommonValidator.ValidationResult.AmountTooLarge:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtAddCreditAmount);
                    break;

                case CommonValidator.ValidationResult.PaymentInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, cmbAddCreditPaymentType);
                    break;

                case CommonValidator.ValidationResult.DescriptionInvalid:
                case CommonValidator.ValidationResult.DescriptionTooShort:
                case CommonValidator.ValidationResult.DescriptionTooLong:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtAddCreditDescription);
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    MessageBox.Show("Credit added unsuccessfully!");
                    break;
            }
        }

        private void cmbAddCreditSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ErrorHelper.HideErrorForControl(cmbAddCreditSubCategory);
            cmbAddCreditSubCategory.AutoCompleteMode = AutoCompleteMode.Append;
            cmbAddCreditSubCategory.AutoCompleteSource = AutoCompleteSource.ListItems;

            if (cmbAddCreditSubCategory.SelectedValue == null)
            {
                return;
            }

            int subCategoryId = 0;
            DataRowView drv = cmbAddCreditSubCategory.SelectedValue as DataRowView;
            if (drv != null)
            {
                subCategoryId = Convert.ToInt32(drv[0]);
            }
            else
            {
                subCategoryId = Convert.ToInt32(cmbAddCreditSubCategory.SelectedValue);
            }

            if (subCategoryId == -99)
            {
                int currentCategoryId = 0;
                DataRowView drvCat = cmbAddCreditCategory.SelectedValue as DataRowView;
                if (drvCat != null)
                {
                    currentCategoryId = Convert.ToInt32(drvCat[0]);
                }
                else
                {
                    currentCategoryId = Convert.ToInt32(cmbAddCreditCategory.SelectedValue);
                }

                string currentCategoryName = cmbAddCreditCategory.Text;

                this.Hide();

                using (CreditAddSubCategoryControls addSubCatForm = new CreditAddSubCategoryControls(currentCategoryId, currentCategoryName))
                {
                    DialogResult result = addSubCatForm.ShowDialog();
                    this.Show();

                    if (result == DialogResult.OK)
                    {

                        CommonUiFunction.LoadInComboBox(
                            "spGetCreditSubCategoryByCategoryID",
                            "Select Sub Category",
                            "+ Add New Sub Category",
                            cmbAddCreditSubCategory,
                            "@CategoryID",
                            currentCategoryId);


                        if (!string.IsNullOrEmpty(addSubCatForm.AddedSubCategoryName))
                        {
                            int index = cmbAddCreditSubCategory.FindStringExact(addSubCatForm.AddedSubCategoryName);
                            if (index != -1)
                            {
                                cmbAddCreditSubCategory.SelectedIndex = index;
                                cmbAddCreditSubCategory.ForeColor = Color.Black;
                            }
                            else
                            {
                                cmbAddCreditSubCategory.SelectedIndex = 0;
                                cmbAddCreditSubCategory.ForeColor = Color.Gray;
                            }
                        }
                        else
                        {
                            cmbAddCreditSubCategory.SelectedIndex = 0;
                            cmbAddCreditSubCategory.ForeColor = Color.Gray;
                        }
                    }
                    else
                    {
                        cmbAddCreditSubCategory.SelectedIndex = 0;
                        cmbAddCreditSubCategory.ForeColor = Color.Black;
                    }
                }
            }
        }


        private void txtAddCreditAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtAddCreditAmount.Text != "Enter Amount" && !string.IsNullOrWhiteSpace(txtAddCreditAmount.Text))
            {
                ErrorHelper.HideErrorForControl(txtAddCreditAmount);
            }
        }

        private void txtAddCreditDescription_TextChanged(object sender, EventArgs e)
        {
            if (txtAddCreditDescription.Text != "Enter Description" && !string.IsNullOrWhiteSpace(txtAddCreditDescription.Text))
            {
                ErrorHelper.HideErrorForControl(txtAddCreditDescription);
            }
        }

        private void cmbAddCreditPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ErrorHelper.HideErrorForControl(cmbAddCreditPaymentType);
            cmbAddCreditPaymentType.AutoCompleteMode = AutoCompleteMode.Append;
            cmbAddCreditPaymentType.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void cmbAddCreditCategory_Click(object sender, EventArgs e)
        {
            cmbAddCreditCategory.DroppedDown = true;
        }

        private void cmbAddCreditSubCategory_Click(object sender, EventArgs e)
        {
            cmbAddCreditSubCategory.DroppedDown = true;
        }

        private void cmbAddCreditPaymentType_Click(object sender, EventArgs e)
        {
            cmbAddCreditPaymentType.DroppedDown = true;
        }

        private void cmbAddCreditCategory_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;

            cmbAddCreditCategory.DroppedDown = true;
        }

        private void cmbAddCreditSubCategory_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;

            if (cmbAddCreditSubCategory.Text != "Select Sub Category")
            {
                cmbAddCreditSubCategory.DroppedDown = true;
            }
        }

        private void cmbAddCreditPaymentType_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;

            cmbAddCreditPaymentType.DroppedDown = true;
        }




       


       
    }
}