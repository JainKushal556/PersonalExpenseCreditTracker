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

            //cmbAddCreditSubCategory.Text = "Select Sub Category";
            //cmbAddCreditSubCategory.ForeColor = Color.Gray;

            cmbAddCreditSubCategory.DataSource = null;
            cmbAddCreditSubCategory.Items.Clear();
            cmbAddCreditSubCategory.Items.Add("Please Select a Category First");
            cmbAddCreditSubCategory.SelectedIndex = 0;
            cmbAddCreditSubCategory.ForeColor = Color.Gray;

            cmbAddCreditPaymentType.Text = "Select Payment Type";
            cmbAddCreditPaymentType.ForeColor = Color.Gray;

           // CommonUiFunction.LoadInComboBox("spGetAllCreditCategory", "Select Category", "+ Add New Category", cmbAddCreditCategory);
            Common.CommonUiFunction.LoadInComboBox("spGetCreditCategoriesByUserID", Session.LogedInUser.GetUserId(), "Select Category", "+ Add New Category", cmbAddCreditCategory);
            CommonUiFunction.LoadInComboBox("spGetAllPaymentTypes", "Select Payment Type", cmbAddCreditPaymentType);
            cmbAddCreditPaymentType.MouseClick += (s, ev) => { cmbAddCreditPaymentType.DroppedDown = true; };

            cmbAddCreditCategory.AutoCompleteMode = AutoCompleteMode.Append;
            cmbAddCreditCategory.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbAddCreditSubCategory.AutoCompleteMode = AutoCompleteMode.Append;
            cmbAddCreditSubCategory.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbAddCreditPaymentType.AutoCompleteMode = AutoCompleteMode.Append;
            cmbAddCreditPaymentType.AutoCompleteSource = AutoCompleteSource.ListItems;


            CommonUiFunction.SetComboBoxHeightAndOwnerDraw1(cmbAddCreditCategory);
            CommonUiFunction.SetComboBoxHeightAndOwnerDraw1(cmbAddCreditSubCategory);
            CommonUiFunction.SetComboBoxHeightAndOwnerDraw1(cmbAddCreditPaymentType);

            ignoreEvents = false;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (cmbAddCreditCategory.Focused)
                {
                    SelectComboBoxSuggestion(cmbAddCreditCategory);
                    return true;
                }
                else if (cmbAddCreditSubCategory.Focused)
                {
                    SelectComboBoxSuggestion(cmbAddCreditSubCategory);
                    return true;
                }
                else if (cmbAddCreditPaymentType.Focused)
                {
                    SelectComboBoxSuggestion(cmbAddCreditPaymentType);
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SelectComboBoxSuggestion(ComboBox cmb)
        {
            if (!string.IsNullOrWhiteSpace(cmb.Text))
            {
                int index = cmb.FindStringExact(cmb.Text);
                if (index == -1)
                {
                    index = cmb.FindString(cmb.Text);
                }

                if (index != -1)
                {
                    cmb.SelectedIndex = index;
                    cmb.SelectionStart = cmb.Text.Length;
                }
            }

            cmb.DroppedDown = false;
        }

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
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            ignoreEvents = true;

            txtAddCreditAmount.Text = "Enter Amount";
            txtAddCreditAmount.ForeColor = Color.Gray;
            txtAddCreditDescription.Text = "Enter Description";
            txtAddCreditDescription.ForeColor = Color.Gray;

            if (cmbAddCreditCategory.Items.Count > 0)
                cmbAddCreditCategory.SelectedIndex = 0;
            cmbAddCreditCategory.Text = "Select Category";
            cmbAddCreditCategory.ForeColor = Color.Gray;

            cmbAddCreditSubCategory.DataSource = null;
            cmbAddCreditSubCategory.Items.Clear();
            cmbAddCreditSubCategory.Text = "Select Sub Category";
            cmbAddCreditSubCategory.ForeColor = Color.Gray;

            if (cmbAddCreditPaymentType.Items.Count > 0)
                cmbAddCreditPaymentType.SelectedIndex = 0;
            cmbAddCreditPaymentType.Text = "Select Payment Type";
            cmbAddCreditPaymentType.ForeColor = Color.Gray;

            ErrorHelper.ClearAllErrors(this);

            ignoreEvents = false;
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
            if (ignoreEvents) return;

            ignoreEvents = true; // Block all events during selection

            ErrorHelper.HideErrorForControl(cmbAddCreditCategory);
            cmbAddCreditCategory.AutoCompleteMode = AutoCompleteMode.Append;
            cmbAddCreditCategory.AutoCompleteSource = AutoCompleteSource.ListItems;

            int categoryId = 0;
            if (cmbAddCreditCategory.SelectedValue != null)
            {
                DataRowView drv = cmbAddCreditCategory.SelectedValue as DataRowView;
                if (drv != null)
                {
                    categoryId = Convert.ToInt32(drv[0]);
                }
                else
                {
                    categoryId = Convert.ToInt32(cmbAddCreditCategory.SelectedValue);
                }
            }

            if (categoryId == -99)
            {
                this.Opacity = 0;
                using (var addCategoryForm = new PersonalExpenseCreditTracker.Modules.Settings.Category.CreditAddCategoryControls())
                {
                    DialogResult result = addCategoryForm.ShowDialog(this);
                    this.Opacity = 1;

                    int newSelectedCategoryId = 0;

                    if (result == DialogResult.OK)
                    {
                        Common.CommonUiFunction.LoadInComboBox(
                            "spGetCreditCategoriesByUserID",
                            Session.LogedInUser.GetUserId(),
                            "Select Category",
                            "+ Add New Cetegory",
                            cmbAddCreditCategory);

                        if (!string.IsNullOrEmpty(addCategoryForm.AddedCategoryName))
                        {
                            int index = cmbAddCreditCategory.FindStringExact(addCategoryForm.AddedCategoryName);
                            if (index != -1)
                            {
                                cmbAddCreditCategory.SelectedIndex = index;
                                cmbAddCreditCategory.ForeColor = Color.Black;

                                DataRowView newlySelectedDrv = cmbAddCreditCategory.SelectedValue as DataRowView;
                                if (newlySelectedDrv != null)
                                {
                                    newSelectedCategoryId = Convert.ToInt32(newlySelectedDrv[0]);
                                }
                                else
                                {
                                    newSelectedCategoryId = Convert.ToInt32(cmbAddCreditCategory.SelectedValue);
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
                            cmbAddCreditCategory.ForeColor = Color.Gray;
                        }
                    }
                    else
                    {
                        cmbAddCreditCategory.SelectedIndex = 0;
                        cmbAddCreditCategory.ForeColor = Color.Black;
                    }

                    if (newSelectedCategoryId > 0)
                    {
                        CommonUiFunction.LoadInComboBox(
                            "spGetCreditSubCategoryByCategoryID",
                            "Select Sub Category",
                            "+ Add New Sub Category",
                            cmbAddCreditSubCategory,
                            "@CategoryID",
                            newSelectedCategoryId);
                    }
                    else
                    {
                        cmbAddCreditSubCategory.DataSource = null;
                        cmbAddCreditSubCategory.Items.Clear();
                        cmbAddCreditSubCategory.Items.Add("Please Select a Category First");
                        cmbAddCreditSubCategory.SelectedIndex = 0;
                        cmbAddCreditSubCategory.ForeColor = Color.Gray;
                    }
                }
            }
            else if (categoryId > 0)
            {
                // Load SubCategories when Category is selected
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
                cmbAddCreditSubCategory.Items.Add("Please Select a Category First");
                cmbAddCreditSubCategory.SelectedIndex = 0;
                cmbAddCreditSubCategory.ForeColor = Color.Gray;
            }

            ignoreEvents = false; 
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


            int catId = 0, subCatId = 0, payId = 0;

            if (cmbAddCreditCategory.SelectedValue != null)
                int.TryParse(cmbAddCreditCategory.SelectedValue.ToString(), out catId);

            if (cmbAddCreditSubCategory.SelectedValue != null)
                int.TryParse(cmbAddCreditSubCategory.SelectedValue.ToString(), out subCatId);

            if (cmbAddCreditPaymentType.SelectedValue != null)
                int.TryParse(cmbAddCreditPaymentType.SelectedValue.ToString(), out payId);

            string amount = (txtAddCreditAmount.Text == "Select Amount") ? "" : txtAddCreditAmount.Text;
            string description = (txtAddCreditDescription.Text == "Enter Description") ? "" : txtAddCreditDescription.Text;

            if (!ErrorHelper.Validate(CommonValidator.ValidateCategory(catId), errorProvider1, cmbAddCreditCategory)) return;
            if (!ErrorHelper.Validate(CommonValidator.ValidateSubCategory(subCatId), errorProvider1, cmbAddCreditSubCategory)) return;
            if (!ErrorHelper.Validate(CommonValidator.ValidateAmount(amount), errorProvider1, txtAddCreditAmount)) return;
            if (!ErrorHelper.Validate(CommonValidator.ValidatePayment(payId), errorProvider1, cmbAddCreditPaymentType)) return;
            if (!ErrorHelper.Validate(CommonValidator.ValidateDescription(description), errorProvider1, txtAddCreditDescription)) return;

            DialogResult confirmResult = MessageBox.Show(
                "Are you sure you want to add this credit?",
                "Confirm Add",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }


            CreditUI creditUi = new CreditUI();

            creditUi.userId = Session.LogedInUser.GetUserId();
            creditUi.creditId = -1;
            creditUi.categoryId = catId;
            creditUi.subCategoryId = subCatId;
            creditUi.paymentId = payId;
            creditUi.amount = amount;
            creditUi.description = description;

            CommonValidator.ValidationResult result = creditUi.InsertDataIntoCreditUi();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    //MessageBox.Show("Expense added successfully!");
                    this.DialogResult = DialogResult.OK;
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
            if (ignoreEvents) return;

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

                this.Opacity = 0;
                using (CreditAddSubCategoryControls addSubCatForm = new CreditAddSubCategoryControls(currentCategoryId, currentCategoryName))
                {
                    DialogResult result = addSubCatForm.ShowDialog(this);
                    this.Opacity = 1;

                    ignoreEvents = true;

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

                    ignoreEvents = false;
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
            if (cmbAddCreditCategory.SelectedIndex > 0 || cmbAddCreditCategory.Text == "Select Category") return;
            cmbAddCreditCategory.DroppedDown = true;
        }

        private void cmbAddCreditSubCategory_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            if (cmbAddCreditSubCategory.SelectedIndex > 0 || cmbAddCreditSubCategory.Text == "Select Sub Category") return;
            cmbAddCreditSubCategory.DroppedDown = true;
        }

        private void cmbAddCreditPaymentType_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            if (cmbAddCreditPaymentType.SelectedIndex > 0 || cmbAddCreditPaymentType.Text == "Select Payment Type") return;
            cmbAddCreditPaymentType.DroppedDown = true;
        }




       


       
    }
}