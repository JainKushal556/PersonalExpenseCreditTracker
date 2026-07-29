using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PersonalExpenseCreditTracker.Modules.Lent
{
    public partial class PayLentReturnAmountControls : Form
    {
        public PayLentReturnAmountControls()
        {
            InitializeComponent();
            //this.rtxtDescription.Enter += new System.EventHandler(this.rtxtDescription_Leave);
            //this.rtxtDescription.Leave += new System.EventHandler(this.rtxtDescription_Enter);
        }

        private void ReturnAmountControls_Load(object sender, EventArgs e)
        {
            SetRadius(pnlInputField, 15);
            SetRadius(pnlPersonDetails, 15);
            SetRadius(btnClear, 5);
            SetRadius(btnSave, 5);
            SetRadius(btnCancel, 5);

            txtReturnAmount.Text = "Enter Return Amount";
            txtReturnDate.Text = "DD-MM-YYYY";
            cmbPaymentType.Text = "Enter Payment Type";
            cmbStatus.Text = "Enetr Status";
            txtDescription.Text = "Enter Description";

            txtReturnAmount.ForeColor = Color.Gray;
            txtReturnDate.ForeColor = Color.Gray;
            cmbPaymentType.ForeColor = Color.Gray;
            cmbStatus.ForeColor = Color.Gray;
            txtDescription.ForeColor = Color.Gray;
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

        // Radius Corner of These Panels
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

        private void btnAddSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Amount Return Successfully");
            this.Close();
        }

        private void btnAddCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddClear_Click(object sender, EventArgs e)
        {
            txtReturnAmount.Text = "Enter Return Amount";
            txtReturnDate.Text = "DD-MM-YYYY";
            cmbPaymentType.Text = "Enter Payment Type";
            cmbStatus.Text = "Enetr Status";
            txtDescription.Text = "Enter Description";

            txtReturnAmount.ForeColor = Color.Gray;
            txtReturnDate.ForeColor = Color.Gray;
            txtDescription.ForeColor = Color.Gray;
            cmbPaymentType.ForeColor = Color.Gray;
            cmbStatus.ForeColor = Color.Gray;

            pnlCalenderShow.Visible = false;
        }

        private void txtReturnAmount_Enter(object sender, EventArgs e)
        {
            if (txtReturnAmount.Text == "Enter Return Amount")
            {
                txtReturnAmount.Text = "";
                txtReturnAmount.ForeColor = Color.Black;
            }
            pnlCalenderShow.Visible = false;
        }

        private void txtReturnAmount_Leave(object sender, EventArgs e)
        {
            if (txtReturnAmount.Text == "")
            {
                txtReturnAmount.Text = "Enter Return Amount";
                txtReturnAmount.ForeColor = Color.Gray;
            }
            pnlCalenderShow.Visible = false;
        }

        private void cmbStatus_Enter(object sender, EventArgs e)
        {
            if (cmbStatus.Text == "Enetr Status")
            {
                cmbStatus.Text = "";
                cmbStatus.ForeColor = Color.Black;
            }
            pnlCalenderShow.Visible = false;
        }

        private void cmbStatus_Leave(object sender, EventArgs e)
        {
            if (cmbStatus.Text == "")
            {
                cmbStatus.Text = "Enetr Status";
                cmbStatus.ForeColor = Color.Gray;
            }
        }

        private void cmbPaymentType_Enter(object sender, EventArgs e)
        {
            if (cmbPaymentType.Text == "Enter Payment Type")
            {
                cmbPaymentType.Text = "";
                cmbPaymentType.ForeColor = Color.Black;
            }
            pnlCalenderShow.Visible = false;
        }

        private void cmbPaymentType_Leave(object sender, EventArgs e)
        {
            if (cmbPaymentType.Text == "")
            {
                cmbPaymentType.Text = "Enter Payment Type";
                cmbPaymentType.ForeColor = Color.Gray;
            }
        }

        private void txtReturnDate_Enter(object sender, EventArgs e)
        {
            if (txtReturnDate.Text == "DD-MM-YYYY")
            {
                txtReturnDate.Text = "";
                txtReturnDate.ForeColor = Color.Black;
            }
            pnlCalenderShow.Visible = true;
        }

        private void txtReturnDate_Leave(object sender, EventArgs e)
        {
            if (txtReturnDate.Text == "")
            {
                txtReturnDate.Text = "DD-MM-YYYY";
                txtReturnDate.ForeColor = Color.Gray;
            }
        }
        
        private void txtDescription_Enter(object sender, EventArgs e)
        {
            if (txtDescription.Text == "Enter Description")
            {
                txtDescription.Text = "";
                txtDescription.ForeColor = Color.Black;
            }
            pnlCalenderShow.Visible = false;
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            if (txtDescription.Text == "")
            {
                txtDescription.Text = "Enter Description";
                txtDescription.ForeColor = Color.Gray;
            }
        }

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtReturnDate.Text = e.Start.ToString("dd-MM-yyyy");
            txtReturnDate.ForeColor = Color.Black;
            pnlCalenderShow.Visible = false;
        }

        private void txtReturnDate_TextChanged(object sender, EventArgs e)
        {
            pnlCalenderShow.Visible = false;
        }

        private void btnAddCalendar_Click(object sender, EventArgs e)
        {
            pnlCalenderShow.Visible = !pnlCalenderShow.Visible;
        }

        

        private void panelMainBody_Click(object sender, EventArgs e)
        {
            pnlCalenderShow.Visible = false;
        }

        private void pnlPersonDetails_Click(object sender, EventArgs e)
        {
            pnlCalenderShow.Visible = false;
        }

        private void pnlInputField_Click(object sender, EventArgs e)
        {
            pnlCalenderShow.Visible = false;
        }
    }
}
