using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PersonalExpenseCreditTracker.Modules.Credit
{
    public partial class AddCreditControls : Form
    {
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

        private void btnSaveCredit_Click(object sender, EventArgs e)
        {
            if (txtAddCreditAmount.Text == "Enter Amount" || txtAddCreditDescription.Text == "Enter Description"
                || cmbAddCreditCategory.Text == "Select Category" || cmbAddCreditPaymentType.Text == "Select Payment Type"
                || cmbAddCreditSubCategory.Text == "Select Sub Category")
            {
                MessageBox.Show("Please fill all fields");
            }
            else
            {
                MessageBox.Show("Credit Details Added Successfully...");
                this.Close();
            }
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

        private void cmbAddCreditCategory_Enter(object sender, EventArgs e)
        {
            cmbAddCreditCategory.ForeColor = Color.Black;
        }

        private void cmbAddCreditCategory_Leave(object sender, EventArgs e)
        {
            if (cmbAddCreditCategory.SelectedIndex == -1 || cmbAddCreditCategory.Text == "Select Category")
                cmbAddCreditCategory.ForeColor = Color.Gray;
        }

        private void cmbAddCreditSubCategory_Enter(object sender, EventArgs e)
        {
            if (cmbAddCreditSubCategory.Text == "Select Sub Category")
                cmbAddCreditSubCategory.ForeColor = Color.Black;
        }

        private void cmbAddCreditSubCategory_Leave(object sender, EventArgs e)
        {
            if (cmbAddCreditSubCategory.SelectedIndex == -1 || cmbAddCreditSubCategory.Text == "Select Sub Category")
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
            if (cmbAddCreditPaymentType.SelectedIndex == -1 || cmbAddCreditPaymentType.Text == "Select Payment Type")
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
    }
}