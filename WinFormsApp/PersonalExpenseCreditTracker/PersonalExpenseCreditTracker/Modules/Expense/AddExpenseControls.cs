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
            cmbAddExpenseSubCategory.Text = "Select Sub Category";
            cmbAddExpenseSubCategory.ForeColor = Color.Gray;
            cmbAddExpensePaymentType.Text = "Select Payment Type";
            cmbAddExpensePaymentType.ForeColor = Color.Gray;


            Common.CommonUiFunction.LoadInComboBox("spGetExpenseCategoriesByUserID", Session.LogedInUser.GetUserId(), "Select Category", "+ Add New Cetegory", cmbAddExpenseCategory);
            CommonUiFunction.LoadInComboBox("spGetAllPaymentTypes", "Select Payment Type", cmbAddExpensePaymentType);
            cmbAddExpensePaymentType.MouseClick += (s, ev) => { cmbAddExpensePaymentType.DroppedDown = true; };
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
            this.Close();
        }

        private void btnCloseEditPersonDetails_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveExpense_Click(object sender, EventArgs e)
        {
            // Clear all previous validation errors
           // ErrorHelper.ClearCustomErrors(this);
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
                case CommonValidator.ValidationResult.DescriptionTooShort:
                case CommonValidator.ValidationResult.DescriptionTooLong:
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

            ErrorHelper.ClearAllErrors(this);
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

                this.Hide();

                using (ExpenseAddSubCategoryControls addSubCatForm = new ExpenseAddSubCategoryControls(currentCategoryId, currentCategoryName))
                {
                    DialogResult result = addSubCatForm.ShowDialog();
                    this.Show();

                    if (result == DialogResult.OK)
                    {
                        CommonUiFunction.LoadInComboBox(
                            "spGetExpenseSubCategoryByCategoryID",
                            "Select SubCategory",
                            "+ Add New Sub Category",
                            cmbAddExpenseSubCategory,
                            "@CategoryID",
                            currentCategoryId);

                        string newSubCategory = addSubCatForm.AddedSubCategoryName;
                        if (!string.IsNullOrEmpty(newSubCategory))
                        {
                            cmbAddExpenseSubCategory.Text = newSubCategory;
                        }
                    }
                    else
                    {
                        cmbAddExpenseSubCategory.SelectedIndex = 0;
                    }
                }
            }
        }


        private void cmbAddExpenseCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ErrorHelper.HideErrorForControl(cmbAddExpenseCategory);
            cmbAddExpenseCategory.AutoCompleteMode = AutoCompleteMode.Append;
            cmbAddExpenseCategory.AutoCompleteSource = AutoCompleteSource.ListItems;

            if (cmbAddExpenseCategory.SelectedValue == null)
                return;
            int categoryId = 0;
            DataRowView drv = cmbAddExpenseCategory.SelectedValue as DataRowView;
            if (drv != null)
            {
                categoryId = Convert.ToInt32(drv[0]);
            }
            else
            {
                categoryId = Convert.ToInt32(cmbAddExpenseCategory.SelectedValue);
            }
            if (categoryId == -99)
            {
                this.Hide();
                using (var addCategoryForm = new PersonalExpenseCreditTracker.Modules.Settings.Category.ExpenseAddCategoryControls())
                {
                    DialogResult result = addCategoryForm.ShowDialog();
                    this.Show();
                    if (result == DialogResult.OK)
                    {
                        CommonUiFunction.LoadInComboBox("spGetAllCreditCategory", "Select Category", "+ Add New Category", cmbAddExpenseCategory);
                        string newCategory = addCategoryForm.AddedCategoryName;
                        if (!string.IsNullOrEmpty(newCategory))
                        {
                            cmbAddExpenseCategory.Text = newCategory;
                        }
                    }
                    else
                    {
                        cmbAddExpenseCategory.SelectedIndex = 0;
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
                cmbAddExpenseSubCategory.DataSource = null;
                cmbAddExpenseSubCategory.Items.Clear();
                cmbAddExpenseSubCategory.Text = "Select Sub Category";
                cmbAddExpenseSubCategory.ForeColor = Color.Gray;
            }
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
            cmbAddExpenseCategory.DroppedDown = true;
        }

        private void cmbAddExpenseSubCategory_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;

            if (cmbAddExpenseSubCategory.Text != "Select Sub Category")
            {
                cmbAddExpenseSubCategory.DroppedDown = true;
            }

        }

        private void cmbAddExpensePaymentType_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            cmbAddExpensePaymentType.DroppedDown = true;
        }

       
    }
}
