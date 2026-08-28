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
using PersonalExpenseCreditTracker.Modules.Settings.Category;

namespace PersonalExpenseCreditTracker.Modules.Expense
{
    public partial class AddExpenseControls : Form
    {
        private bool ignoreEvents = true;
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

            //cmbAddExpenseSubCategory.Text = "Select Sub Category";
            //cmbAddExpenseSubCategory.ForeColor = Color.Gray;

            cmbAddExpenseSubCategory.DataSource = null;
            cmbAddExpenseSubCategory.Items.Clear();
            cmbAddExpenseSubCategory.Items.Add("Please Select a Category First");
            cmbAddExpenseSubCategory.SelectedIndex = 0;
            cmbAddExpenseSubCategory.ForeColor = Color.Gray;

            cmbAddExpensePaymentType.Text = "Select Payment Type";
            cmbAddExpensePaymentType.ForeColor = Color.Gray;


            Common.CommonUiFunction.LoadInComboBox("spGetExpenseCategoriesByUserID", Session.LogedInUser.GetUserId(), "Select Category", "+ Add New Cetegory", cmbAddExpenseCategory);
            CommonUiFunction.LoadInComboBox("spGetAllPaymentTypes", "Select Payment Type", cmbAddExpensePaymentType);
            cmbAddExpensePaymentType.MouseClick += (s, ev) => { cmbAddExpensePaymentType.DroppedDown = true; };

            cmbAddExpenseCategory.AutoCompleteMode = AutoCompleteMode.Append;
            cmbAddExpenseCategory.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbAddExpenseSubCategory.AutoCompleteMode = AutoCompleteMode.Append;
            cmbAddExpenseSubCategory.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbAddExpensePaymentType.AutoCompleteMode = AutoCompleteMode.Append;
            cmbAddExpensePaymentType.AutoCompleteSource = AutoCompleteSource.ListItems;

            CommonUiFunction.SetComboBoxHeightAndOwnerDraw1(cmbAddExpenseCategory);
            CommonUiFunction.SetComboBoxHeightAndOwnerDraw1(cmbAddExpenseSubCategory);
            CommonUiFunction.SetComboBoxHeightAndOwnerDraw1(cmbAddExpensePaymentType);




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
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnCloseEditPersonDetails_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSaveExpense_Click(object sender, EventArgs e)
        {
           
            errorProvider1.Clear();

            
            int catId = 0, subCatId = 0, payId = 0;

            if (cmbAddExpenseCategory.SelectedValue != null)
                int.TryParse(cmbAddExpenseCategory.SelectedValue.ToString(), out catId);

            if (cmbAddExpenseSubCategory.SelectedValue != null)
                int.TryParse(cmbAddExpenseSubCategory.SelectedValue.ToString(), out subCatId);

            if (cmbAddExpensePaymentType.SelectedValue != null)
                int.TryParse(cmbAddExpensePaymentType.SelectedValue.ToString(), out payId);

            string amount = (txtAddExpenseAmount.Text == "Select Amount") ? "" : txtAddExpenseAmount.Text;
            string description = (txtAddExpenseDescription.Text == "Enter Description") ? "" : txtAddExpenseDescription.Text;

            if (!ErrorHelper.Validate(CommonValidator.ValidateCategory(catId), errorProvider1, cmbAddExpenseCategory)) return;
            if (!ErrorHelper.Validate(CommonValidator.ValidateSubCategory(subCatId), errorProvider1, cmbAddExpenseSubCategory)) return;
            if (!ErrorHelper.Validate(CommonValidator.ValidateAmount(amount), errorProvider1, txtAddExpenseAmount)) return;
            if (!ErrorHelper.Validate(CommonValidator.ValidatePayment(payId), errorProvider1, cmbAddExpensePaymentType)) return;
            if (!ErrorHelper.Validate(CommonValidator.ValidateDescription(description), errorProvider1, txtAddExpenseDescription)) return;

            DialogResult confirmResult = MessageBox.Show(
                "Are you sure you want to add this expense?",
                "Confirm Add",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            
            ExpenseUI expenseUi = new ExpenseUI();

            expenseUi.userId = Session.LogedInUser.GetUserId();
            expenseUi.expenseId = -1;
            expenseUi.categoryId = catId;
            expenseUi.subCategoryId = subCatId;
            expenseUi.paymentId = payId;
            expenseUi.amount = amount;
            expenseUi.description = description;

            CommonValidator.ValidationResult result = expenseUi.InsertDataIntoExpenseUi();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    //MessageBox.Show("Expense added successfully!");
                    this.DialogResult = DialogResult.OK;
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
                case CommonValidator.ValidationResult.DescriptionTooShort:
                case CommonValidator.ValidationResult.DescriptionTooLong:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtAddExpenseDescription);
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    MessageBox.Show("Expense added unsuccessfully!");
                    break;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                // If Category ComboBox has focus
                if (cmbAddExpenseCategory.Focused)
                {
                    SelectComboBoxSuggestion(cmbAddExpenseCategory);
                    return true; // Enter action complete, no form submit or beep
                }
                // If Sub Category ComboBox has focus
                else if (cmbAddExpenseSubCategory.Focused)
                {
                    SelectComboBoxSuggestion(cmbAddExpenseSubCategory);
                    return true;
                }
                // If Payment Type ComboBox has focus
                else if (cmbAddExpensePaymentType.Focused)
                {
                    SelectComboBoxSuggestion(cmbAddExpensePaymentType);
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Helper method to find and select item by text
        private void SelectComboBoxSuggestion(ComboBox cmb)
        {
            if (!string.IsNullOrWhiteSpace(cmb.Text))
            {
                // 1. Exact match with name
                int index = cmb.FindStringExact(cmb.Text);

                // 2. If not found, match with starting characters
                if (index == -1)
                {
                    index = cmb.FindString(cmb.Text);
                }

                // 3. Select item if found
                if (index != -1)
                {
                    cmb.SelectedIndex = index;
                    cmb.SelectionStart = cmb.Text.Length;
                }
            }

            // Close dropdown if open
            cmb.DroppedDown = false;
        }



        private void btnClear_Click(object sender, EventArgs e)
        {
            ignoreEvents = true;

            txtAddExpenseAmount.Text = "Enter Amount";
            txtAddExpenseAmount.ForeColor = Color.Gray;
            txtAddExpenseDescription.Text = "Enter Description";
            txtAddExpenseDescription.ForeColor = Color.Gray;

            if (cmbAddExpenseCategory.Items.Count > 0)
                cmbAddExpenseCategory.SelectedIndex = 0;
            cmbAddExpenseCategory.Text = "Select Category";
            cmbAddExpenseCategory.ForeColor = Color.Gray;

            cmbAddExpenseSubCategory.DataSource = null;
            cmbAddExpenseSubCategory.Items.Clear();
            cmbAddExpenseSubCategory.Text = "Select Sub Category";
            cmbAddExpenseSubCategory.ForeColor = Color.Gray;

            if (cmbAddExpensePaymentType.Items.Count > 0)
                cmbAddExpensePaymentType.SelectedIndex = 0;
            cmbAddExpensePaymentType.Text = "Select Payment Type";
            cmbAddExpensePaymentType.ForeColor = Color.Gray;

            ErrorHelper.ClearAllErrors(this);

            ignoreEvents = false;
        }

        private void cmbAddExpenseCategory_Enter(object sender, EventArgs e)
        {
            if (cmbAddExpenseCategory.Text == "Select Category")
            cmbAddExpenseCategory.ForeColor = Color.Black;
        }

        private void cmbAddExpenseCategory_Leave(object sender, EventArgs e)
        {
          
            if (cmbAddExpenseCategory.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(cmbAddExpenseCategory.Text) || cmbAddExpenseCategory.Text == "Select Category")
            {
                cmbAddExpenseCategory.SelectedIndex = 0;
                cmbAddExpenseCategory.Text = "Select Category";
                cmbAddExpenseCategory.ForeColor = Color.Gray;
            }
            else
            {
                cmbAddExpenseCategory.ForeColor = Color.Black;
            }
        }


        private void cmbAddExpenseSubCategory_Enter(object sender, EventArgs e)
        {
            if (cmbAddExpenseSubCategory.Text == "Select Sub Category")
                cmbAddExpenseSubCategory.ForeColor = Color.Black;
        }

        private void cmbAddExpenseSubCategory_Leave(object sender, EventArgs e)
        {
            if (cmbAddExpenseSubCategory.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(cmbAddExpenseSubCategory.Text) || cmbAddExpenseSubCategory.Text == "Select Sub Category" || cmbAddExpenseSubCategory.Text == "Select SubCategory")
            {
              
                if (cmbAddExpenseSubCategory.Items.Count > 0)
                {
                    cmbAddExpenseSubCategory.SelectedIndex = 0;
                }

                cmbAddExpenseSubCategory.Text = "Select Sub Category";
                cmbAddExpenseSubCategory.ForeColor = Color.Gray;
            }
            else
            {
                cmbAddExpenseSubCategory.ForeColor = Color.Black;
            }
        }



        private void cmbAddExpensePaymentType_Enter(object sender, EventArgs e)
        {
            if (cmbAddExpensePaymentType.Text == "Select Payment Type")
            {
                cmbAddExpensePaymentType.ForeColor = Color.Black;
            }
        }

        private void cmbAddExpensePaymentType_Leave(object sender, EventArgs e)
        {
            if (cmbAddExpensePaymentType.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(cmbAddExpensePaymentType.Text) || cmbAddExpensePaymentType.Text == "Select Payment Type")
            {
                cmbAddExpensePaymentType.SelectedIndex = 0;
                cmbAddExpensePaymentType.Text = "Select Payment Type";
                cmbAddExpensePaymentType.ForeColor = Color.Gray;
            }
            else
            {
                cmbAddExpensePaymentType.ForeColor = Color.Black;
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
            if (ignoreEvents) return;

            ErrorHelper.HideErrorForControl(cmbAddExpenseSubCategory);
            cmbAddExpenseSubCategory.AutoCompleteMode = AutoCompleteMode.Append;
            cmbAddExpenseSubCategory.AutoCompleteSource = AutoCompleteSource.ListItems;

            if (cmbAddExpenseSubCategory.SelectedValue == null)
            {
                return;
            }

            int subCategoryId = 0;
            DataRowView drv = cmbAddExpenseSubCategory.SelectedValue as DataRowView;
            if (drv != null)
            {
                subCategoryId = Convert.ToInt32(drv[0]);
            }
            else
            {
                subCategoryId = Convert.ToInt32(cmbAddExpenseSubCategory.SelectedValue);
            }

            if (subCategoryId == -99)
            {
                int currentCategoryId = 0;
                DataRowView drvCat = cmbAddExpenseCategory.SelectedValue as DataRowView;
                if (drvCat != null)
                {
                    currentCategoryId = Convert.ToInt32(drvCat[0]);
                }
                else
                {
                    currentCategoryId = Convert.ToInt32(cmbAddExpenseCategory.SelectedValue);
                }

                string currentCategoryName = cmbAddExpenseCategory.Text;

                this.Opacity = 0;
                using (ExpenseAddSubCategoryControls addSubCatForm = new ExpenseAddSubCategoryControls(currentCategoryId, currentCategoryName))
                {
                    DialogResult result = addSubCatForm.ShowDialog(this);
                    this.Opacity = 1;

                    ignoreEvents = true;

                    if (result == DialogResult.OK)
                    {
                        CommonUiFunction.LoadInComboBox(
                            "spGetExpenseSubCategoryByCategoryID",
                            "Select Sub Category",
                            "+ Add New Sub Category",
                            cmbAddExpenseSubCategory,
                            "@CategoryID",
                            currentCategoryId);

                        if (!string.IsNullOrEmpty(addSubCatForm.AddedSubCategoryName))
                        {
                            int index = cmbAddExpenseSubCategory.FindStringExact(addSubCatForm.AddedSubCategoryName);
                            if (index != -1)
                            {
                                cmbAddExpenseSubCategory.SelectedIndex = index;
                                cmbAddExpenseSubCategory.ForeColor = Color.Black; 
                            }
                            else
                            {
                                cmbAddExpenseSubCategory.SelectedIndex = 0;
                                cmbAddExpenseSubCategory.ForeColor = Color.Gray;
                            }
                        }
                        else
                        {
                            cmbAddExpenseSubCategory.SelectedIndex = 0;
                            cmbAddExpenseSubCategory.ForeColor = Color.Gray;
                        }
                    }
                    else
                    {
                        cmbAddExpenseSubCategory.SelectedIndex = 0;
                        cmbAddExpenseSubCategory.ForeColor = Color.Black;
                    }

                    ignoreEvents = false;
                }
            }

        }


        private void cmbAddExpenseCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;

            ignoreEvents = true; 

            ErrorHelper.HideErrorForControl(cmbAddExpenseCategory);
            cmbAddExpenseCategory.AutoCompleteMode = AutoCompleteMode.Append;
            cmbAddExpenseCategory.AutoCompleteSource = AutoCompleteSource.ListItems;

            int categoryId = 0;
            if (cmbAddExpenseCategory.SelectedValue != null)
            {
                DataRowView drv = cmbAddExpenseCategory.SelectedValue as DataRowView;
                if (drv != null)
                {
                    categoryId = Convert.ToInt32(drv[0]);
                }
                else
                {
                    categoryId = Convert.ToInt32(cmbAddExpenseCategory.SelectedValue);
                }
            }

            if (categoryId == -99)
            {
                this.Opacity = 0;
                using (var addCategoryForm = new PersonalExpenseCreditTracker.Modules.Settings.Category.ExpenseAddCategoryControls())
                {
                    DialogResult result = addCategoryForm.ShowDialog(this);
                    this.Opacity = 1;

                    int newSelectedCategoryId = 0;

                    if (result == DialogResult.OK)
                    {
                        Common.CommonUiFunction.LoadInComboBox(
                            "spGetExpenseCategoriesByUserID",
                            Session.LogedInUser.GetUserId(),
                            "Select Category",
                            "+ Add New Cetegory",
                            cmbAddExpenseCategory);

                        if (!string.IsNullOrEmpty(addCategoryForm.AddedCategoryName))
                        {
                            int index = cmbAddExpenseCategory.FindStringExact(addCategoryForm.AddedCategoryName);
                            if (index != -1)
                            {
                                cmbAddExpenseCategory.SelectedIndex = index;
                                cmbAddExpenseCategory.ForeColor = Color.Black;

                                DataRowView newlySelectedDrv = cmbAddExpenseCategory.SelectedValue as DataRowView;
                                if (newlySelectedDrv != null)
                                {
                                    newSelectedCategoryId = Convert.ToInt32(newlySelectedDrv[0]);
                                }
                                else
                                {
                                    newSelectedCategoryId = Convert.ToInt32(cmbAddExpenseCategory.SelectedValue);
                                }
                            }
                            else
                            {
                                cmbAddExpenseCategory.SelectedIndex = 0;
                                cmbAddExpenseCategory.ForeColor = Color.Gray;
                            }
                        }
                        else
                        {
                            cmbAddExpenseCategory.SelectedIndex = 0;
                            cmbAddExpenseCategory.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        cmbAddExpenseCategory.SelectedIndex = 0;
                        cmbAddExpenseCategory.ForeColor = Color.Black;
                    }

                    if (newSelectedCategoryId > 0)
                    {
                        CommonUiFunction.LoadInComboBox(
                            "spGetExpenseSubCategoryByCategoryID",
                            "Select Sub Category",
                            "+ Add New Sub Category",
                            cmbAddExpenseSubCategory,
                            "@CategoryID",
                            newSelectedCategoryId);
                    }
                    else
                    {
                        cmbAddExpenseSubCategory.DataSource = null;
                        cmbAddExpenseSubCategory.Items.Clear();
                        cmbAddExpenseSubCategory.Items.Add("Please Select a Category First");
                        cmbAddExpenseSubCategory.SelectedIndex = 0;
                        cmbAddExpenseSubCategory.ForeColor = Color.Gray;
                    }
                }
            }
            else if (categoryId > 0)
            {
               
                CommonUiFunction.LoadInComboBox(
                    "spGetExpenseSubCategoryByCategoryID",
                    "Select Sub Category",
                    "+ Add New Sub Category",
                    cmbAddExpenseSubCategory,
                    "@CategoryID",
                    categoryId);
            }
            else
            {
               
                //cmbAddExpenseSubCategory.DataSource = null;
                //cmbAddExpenseSubCategory.Items.Clear();
                //cmbAddExpenseSubCategory.Text = "Select Sub Category";
                //cmbAddExpenseSubCategory.ForeColor = Color.Gray;

                cmbAddExpenseSubCategory.DataSource = null;
                cmbAddExpenseSubCategory.Items.Clear();
                cmbAddExpenseSubCategory.Items.Add("Please Select a Category First");
                cmbAddExpenseSubCategory.SelectedIndex = 0;
                cmbAddExpenseSubCategory.ForeColor = Color.Gray;
            }

            ignoreEvents = false; 
        }

        private void cmbAddExpenseCategory_Click(object sender, EventArgs e)
        {
            cmbAddExpenseCategory.DroppedDown = true;
        }

        private void cmbAddExpenseSubCategory_Click(object sender, EventArgs e)
        {
            cmbAddExpenseSubCategory.DroppedDown = true;
        }

        private void cmbAddExpensePaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ErrorHelper.HideErrorForControl(cmbAddExpensePaymentType);
            cmbAddExpensePaymentType.AutoCompleteMode = AutoCompleteMode.Append;
            cmbAddExpensePaymentType.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void cmbAddExpensePaymentType_Click(object sender, EventArgs e)
        {
            cmbAddExpensePaymentType.DroppedDown = true;
        }

        private void txtAddExpenseAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtAddExpenseAmount.Text != "Enter Amount" && !string.IsNullOrWhiteSpace(txtAddExpenseAmount.Text))
            {
                ErrorHelper.HideErrorForControl(txtAddExpenseAmount);
            }
        }

        private void txtAddExpenseDescription_TextChanged(object sender, EventArgs e)
        {
            if (txtAddExpenseDescription.Text != "Enter Description" &&
                !string.IsNullOrWhiteSpace(txtAddExpenseDescription.Text))
            {
                ErrorHelper.HideErrorForControl(txtAddExpenseDescription);
            }
        }

        private void cmbAddExpenseCategory_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            if (cmbAddExpenseCategory.SelectedIndex > 0 || cmbAddExpenseCategory.Text == "Select Category") return;
            cmbAddExpenseCategory.DroppedDown = true;
        }

        private void cmbAddExpenseSubCategory_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            if (cmbAddExpenseSubCategory.SelectedIndex > 0 || cmbAddExpenseSubCategory.Text == "Select Sub Category") return;
            cmbAddExpenseSubCategory.DroppedDown = true;
        }

        private void cmbAddExpensePaymentType_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            if (cmbAddExpensePaymentType.SelectedIndex > 0 || cmbAddExpensePaymentType.Text == "Select Payment Type") return;
            cmbAddExpensePaymentType.DroppedDown = true;
        }

       
    }
}
