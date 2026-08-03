using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PersonalExpenseCreditTracker.Common;
using BLLayer.Common;

namespace PersonalExpenseCreditTracker.Modules.Borrow
{
    public partial class PayBorrowPaidAmountControls : Form
    {
        private string selectedStatus = "";
        private int selectedBorrowId = 0;
        public PayBorrowPaidAmountControls()
        {
            InitializeComponent();
            //this.txtDescription.Enter += new System.EventHandler(this.txtDescription_Leave);
            //this.txtDescription.Leave += new System.EventHandler(this.txtDescription_Enter);
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

        public void SetBorrowDetails(int borrowId,string personName, string totalAmount, string remainingAmount, string status,string paidAmount)
        {
            selectedBorrowId = borrowId;
            lblPersonNameText.Text = personName;
            lblTotalAmountText.Text = totalAmount;
            lblRemainingAmountText.Text = remainingAmount;
            lblPaidAmountText.Text = paidAmount;
            selectedStatus = status;
        }


        private void btnAddSave_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

           //Pore Hobe
            errorProvider1.Clear();

            BorrowUI borrowUi = new BorrowUI();
            // Assign values from the form controls to the object
            borrowUi.userId = Session.LogedInUser.GetUserId();
            borrowUi.borrowId = this.selectedBorrowId;
            borrowUi.paymentId = Convert.ToInt32(cmbPaymentType.SelectedValue);

            //  If the placeholder text is still present, pass an empty string
            borrowUi.returnAmount = (txtAmount.Text == "Select Amount") ? "" : txtAmount.Text;
            borrowUi.description = (txtDescription.Text == "Enter Description") ? "" : txtDescription.Text;

            // If no deadline is selected, assign DateTime.MinValue
            //    // Otherwise, assign the selected date from the calendar
            borrowUi.returnDate = (txtReturnDate.Text == "DD-MM-YYYY") ? DateTime.MinValue : monthCalendar.SelectionStart;

            CommonValidator.ValidationResult result = borrowUi.InsertPayBorrowIntoBorrowUi();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show("Borrow paid successfully!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;

                case CommonValidator.ValidationResult.AmountEmpty:
                case CommonValidator.ValidationResult.AmountInvalid:
                case CommonValidator.ValidationResult.AmountTooLarge:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtAmount);
                    break;

                case CommonValidator.ValidationResult.PaymentInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, cmbPaymentType);
                    break;

                case CommonValidator.ValidationResult.DeadlineInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtReturnDate);
                    break;

                case CommonValidator.ValidationResult.DescriptionInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtDescription);
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    MessageBox.Show("Borrow payment failed!");
                    break;
            }
        }

        private void PayBorrowAmountControls_Load(object sender, EventArgs e)
        {
            SetRadius(pnlInputField, 15);
            SetRadius(pnlPersonDetails, 15);
            SetRadius(btnClear, 5);
            SetRadius(btnSave, 5);
            SetRadius(btnCancel, 5);

            txtAmount.Text = "Enter Return Amount";
            txtReturnDate.Text = "DD-MM-YYYY";
            txtDescription.Text = "Enter Description";
            cmbPaymentType.Text = "Enter Payment Type";
            cmbStatus.Text = "Enetr Status";

            txtAmount.ForeColor = Color.Gray;
            txtReturnDate.ForeColor = Color.Gray;
            txtDescription.ForeColor = Color.Gray;
            cmbPaymentType.ForeColor = Color.Gray;
            cmbStatus.ForeColor = Color.Gray;

            CommonUiFunction.LoadInComboBox("spGetAllPaymentTypes", "Select Payment Type", cmbPaymentType);
            CommonUiFunction.LoadInComboBox("spGetAllLentBorrowStatus", "Select Status", cmbStatus);
            if (!string.IsNullOrEmpty(selectedStatus))
            {
                cmbStatus.Text = selectedStatus;
                cmbStatus.ForeColor = Color.Black;
            }
             
        }

        private void btnAddCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddClear_Click(object sender, EventArgs e)
        {
            txtAmount.Text = "Enter Return Amount";
            txtReturnDate.Text = "DD-MM-YYYY";
            cmbPaymentType.Text = "Enter Payment Type";
            cmbStatus.Text = "Enetr Status";
            txtDescription.Text = "Enter Description";

            txtAmount.ForeColor = Color.Gray;
            txtReturnDate.ForeColor = Color.Gray;
            txtDescription.ForeColor = Color.Gray;
            cmbPaymentType.ForeColor = Color.Gray;
            cmbStatus.ForeColor = Color.Gray;

            pnlCalenderShow.Visible = false;
        }

        private void txtAmount_Enter(object sender, EventArgs e)
        {
            if (txtAmount.Text == "Enter Return Amount")
            {
                txtAmount.Text = "";
                txtAmount.ForeColor = Color.Black;
            }
            pnlCalenderShow.Visible = false;
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            if (txtAmount.Text == "")
            {
                txtAmount.Text = "Enter Return Amount";
                txtAmount.ForeColor = Color.Gray;
            }
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

        private void panelMainBody_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
