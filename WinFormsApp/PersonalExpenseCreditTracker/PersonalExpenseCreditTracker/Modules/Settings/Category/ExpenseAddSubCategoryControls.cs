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

namespace PersonalExpenseCreditTracker.Modules.Settings.Category
{
    public partial class ExpenseAddSubCategoryControls : Form
    {
        public string AddedSubCategoryName { get; private set; }
        public int SelectedCategoryId { get; set; }
        public string SelectedCategoryName { get; set; }

        public ExpenseAddSubCategoryControls()
        {
            InitializeComponent();
        }
        public ExpenseAddSubCategoryControls(int categoryId, string categoryName)
        {
            InitializeComponent();
            SelectedCategoryId = categoryId;
            SelectedCategoryName = categoryName;
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

        private void ExpenseAddSubCategoryControls_Load(object sender, EventArgs e)
        {
            txtSubCategory.Text = "Enter Sub Category Name";
            txtSubCategory.ForeColor = Color.Gray;

            if (!string.IsNullOrEmpty(SelectedCategoryName))
            {
                lblCategoryName.Text = SelectedCategoryName;
            }
            SetRadius(pnlBody, 15);
            SetRadius(btnCancel, 5);
            SetRadius(btnSave, 5);
            rdActive.Checked = true;
        }
        private void txtSubCategory_Enter(object sender, EventArgs e)
        {
            if (txtSubCategory.Text == "Enter Sub Category Name")
            {
                txtSubCategory.Text = "";
                txtSubCategory.ForeColor = Color.Black;
            }
        }
        private void txtSubCategory_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSubCategory.Text))
            {
                txtSubCategory.Text = "Enter Sub Category Name";
                txtSubCategory.ForeColor = Color.Gray;
            }
        }

        //Save Button
        private void btnSave_Click(object sender, EventArgs e)
        {
            CategoryUI categoryUI = new CategoryUI();

            AddedSubCategoryName = txtSubCategory.Text.Trim();

            categoryUI.UserId = Session.LogedInUser.GetUserId();
            categoryUI.CategoryID = SelectedCategoryId;
            categoryUI.CategoryName = SelectedCategoryName;
            categoryUI.SubCategory = (txtSubCategory.Text == "Enter Sub Category Name") ? "" : txtSubCategory.Text.Trim();

            categoryUI.IsActive = Convert.ToInt32(rdActive.Checked);
            categoryUI.Inactive = Convert.ToInt32(rdInactive.Checked);
            string ErrorMsg;


            CommonValidator.ValidationResult result = categoryUI.AddExpenseSubCategoryDataIntoCategoryUI();
            

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show("Sub Category added successfully.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    ExpenseCategoryControls expenseCategoryControls = new ExpenseCategoryControls();
                    expenseCategoryControls.LoadSubCategories();
                    break;

                case CommonValidator.ValidationResult.CategoryInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtSubCategory);
                    break;

                case CommonValidator.ValidationResult.CategoryNameEmpty:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtSubCategory);
                    break;

                case CommonValidator.ValidationResult.InvalidCategoryName:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtSubCategory);
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    ErrorMsg = categoryUI.GetErrorMsgforSub("spInsertNewExpenseSubCategoryByUserID", "@CategoryID", "@ActiveStatus", "@SubCategoryName");
                    if (!string.IsNullOrWhiteSpace(ErrorMsg))
                        MessageBox.Show(ErrorMsg);

                    this.Close();
                    break;
            }
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

        private void rbInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (rdInactive.Checked)
                rdActive.Checked = false;
        }

        private void rbActive_CheckedChanged(object sender, EventArgs e)
        {
            if (rdActive.Checked)
                rdInactive.Checked = false;
        }

        private void txtSubCategory_TextChanged(object sender, EventArgs e)
        {
            if (txtSubCategory.Text != "Enter Sub Category Name" && !string.IsNullOrWhiteSpace(txtSubCategory.Text))
            {
                ErrorHelper.HideErrorForControl(txtSubCategory);
            }
        }
    }
}
