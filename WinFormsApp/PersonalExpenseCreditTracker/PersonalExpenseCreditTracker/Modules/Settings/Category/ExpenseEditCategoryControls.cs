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
    public partial class ExpenseEditCategoryControls : Form
    {
        public int SelectedCategoryId { get; set; }
        public string SelectedCategoryName { get; set; }
        bool isSubCategory1;
        public ExpenseEditCategoryControls(int categoryId, string categoryName) : this(categoryId, categoryName, false)
        {
        }

        public ExpenseEditCategoryControls(int categoryId, string categoryName, bool isSubCategory)
        {
            InitializeComponent();
            SelectedCategoryId = categoryId;
            SelectedCategoryName = categoryName;
            isSubCategory1 = isSubCategory;

            if (isSubCategory)
            {
                label1.Text = "Edit Expense Sub Category";
                label2.Text = "Update the details of the selected expense sub category.";
                lblCategoryName.Text = "Sub Category Name";
                label4.Location = new System.Drawing.Point(162, 75);
            }
            else
            {
                label1.Text = "Edit Expense Category";
                label2.Text = "Update the details of the selected expense category.";
                lblCategoryName.Text = "Category Name";
                label4.Location = new System.Drawing.Point(130, 75);
            }
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

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            CategoryUI categoryUI = new CategoryUI();

            categoryUI.UserId = Session.LogedInUser.GetUserId();
            categoryUI.CategoryID = SelectedCategoryId;
            categoryUI.CategoryName = txtCategoryName.Text;

            categoryUI.IsActive = Convert.ToInt32(rdobtnActive.Checked);
            categoryUI.Inactive = Convert.ToInt32(rdobtnInactive.Checked);

            CommonValidator.ValidationResult result;
            string ErrorMsg;
            if (isSubCategory1)
            {
                result = categoryUI.UpdateExpenseSubCategoryDataIntoCategoryUI();
                ErrorMsg = categoryUI.GetErrorMsg("spUpdateExpenseSubCategoryByUserID", "@SubCategoryID", "@AvtiveStatus", "@SubCategoryName");
            }
            else
            {
                result = categoryUI.UpdateExpenseCategoryDataIntoCategoryUI();
                ErrorMsg = categoryUI.GetErrorMsg("spUpdateExpenseCategoryByUserID", "@CategoryID", "@AvtiveStatus", "@CategoryName");
            }

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show(
                    isSubCategory1
                        ? "Sub Category Update Successfully"
                        : "Category Update Successfully"
                        );
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;

                case CommonValidator.ValidationResult.CategoryInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtCategoryName);
                    break;

                case CommonValidator.ValidationResult.CategoryNameEmpty:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtCategoryName);
                    break;

                case CommonValidator.ValidationResult.InvalidCategoryName:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtCategoryName);
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    if (!string.IsNullOrWhiteSpace(ErrorMsg))
                        MessageBox.Show(ErrorMsg);
                    else
                        MessageBox.Show(
                    isSubCategory1
                        ? "Sub Category Not Updated."
                        : "Category Not Updated."
                        );
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        private void rdobtnInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (rdobtnInactive.Checked)
                rdobtnActive.Checked = false;
        }

        private void rdobtnActive_CheckedChanged(object sender, EventArgs e)
        {
            if (rdobtnActive.Checked)
                rdobtnInactive.Checked = false;
        }

        private void ExpenseEditCategoryControls_Load(object sender, EventArgs e)
        {
            txtCategoryName.Text = SelectedCategoryName;
            SetRadius(btnCancel, 5);
            SetRadius(btnUpdateCategory,5);
            rdobtnActive.Checked = true;
        }
    }
}
