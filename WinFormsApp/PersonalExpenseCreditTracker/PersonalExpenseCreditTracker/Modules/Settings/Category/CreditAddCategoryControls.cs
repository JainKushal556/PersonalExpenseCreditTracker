using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PersonalExpenseCreditTracker.Modules.Settings.Category
{
    public partial class CreditAddCategoryControls : Form
    {
        CreditCategoryControls creditCategoryControls;
        public CreditAddCategoryControls()
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

        public CreditAddCategoryControls(CreditCategoryControls Obj)
        {
            InitializeComponent();
            creditCategoryControls = Obj;
        }

        private void CreditAddCategoryControls_Load(object sender, EventArgs e)
        {
            txtCategory.Text = "Enter Category Name";
            txtCategory.ForeColor = Color.Gray;

            SetRadius(pnlBody, 15);
            SetRadius(btnCancel, 5);
            SetRadius(btnSave, 5);

            rdActive.Checked = true;
        }
        private void txtCategory_Enter(object sender, EventArgs e)
        {
            if (txtCategory.Text == "Enter Category Name")
            {
                txtCategory.Text = "";
                txtCategory.ForeColor = Color.Black;
            }
        }
        private void txtCategory_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategory.Text))
            {
                txtCategory.Text = "Enter Category Name";
                txtCategory.ForeColor = Color.Gray;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Saved Credit Category");
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
    }
}
