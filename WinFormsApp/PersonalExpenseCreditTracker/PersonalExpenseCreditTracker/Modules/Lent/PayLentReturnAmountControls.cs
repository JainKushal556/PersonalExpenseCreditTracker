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
namespace PersonalExpenseCreditTracker.Modules.Lent
{
    public partial class PayLentReturnAmountControls : Form
    {
        private int selectedLentId = 0;
        private bool ignoreEvents = true;
        public PayLentReturnAmountControls()
        {
            InitializeComponent();
           
        }
        public void SetLentDetails(int lentId, string personName, string totalAmount, string remainingAmount, string status, string returnAmount)
        {
            this.selectedLentId = lentId;
            lblPersonNameText.Text = personName;
            lblTotalAmountText.Text = totalAmount;
            lblRemainingAmountText.Text = remainingAmount;
            lblReturedAmountText.Text = returnAmount;
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
            cmbPaymentType.Text = "Select Payment Type";
         
            txtDescription.Text = "Enter Description";

            txtReturnAmount.ForeColor = Color.Gray;
            txtReturnDate.ForeColor = Color.Gray;
            cmbPaymentType.ForeColor = Color.Gray;
            txtDescription.ForeColor = Color.Gray;

            CommonUiFunction.LoadInComboBox("spGetAllPaymentTypes", "Select Payment Type", cmbPaymentType);
            cmbPaymentType.MouseClick += (s, ev) => { cmbPaymentType.DroppedDown = true; };
            txtReturnDate.Click += txtReturnDate_Click;
            ignoreEvents = false;
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

        private void btnAddCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddClear_Click(object sender, EventArgs e)
        {
            txtReturnAmount.Text = "Enter Return Amount";
            txtReturnDate.Text = "DD-MM-YYYY";
            cmbPaymentType.Text = "Select Payment Type";

            txtDescription.Text = "Enter Description";
            txtReturnAmount.ForeColor = Color.Gray;
            txtReturnDate.ForeColor = Color.Gray;
            txtDescription.ForeColor = Color.Gray;
            cmbPaymentType.ForeColor = Color.Gray;

            pnlCalenderShow.Visible = false;
            errorProvider1.Clear();
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

       



        private void cmbPaymentType_Enter(object sender, EventArgs e)
        {
          
            if (cmbPaymentType.Text == "Select Payment Type")
            {
                cmbPaymentType.ForeColor = Color.Black;
            }
            pnlCalenderShow.Visible = false;
        }
        private void cmbPaymentType_Leave(object sender, EventArgs e)
        {
           
            if (cmbPaymentType.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(cmbPaymentType.Text) || cmbPaymentType.Text == "Select Payment Type" || cmbPaymentType.Text == "Enter Payment Type")
            {
                cmbPaymentType.SelectedIndex = 0;
                cmbPaymentType.Text = "Select Payment Type";
                cmbPaymentType.ForeColor = Color.Gray;
            }
            else
            {
                cmbPaymentType.ForeColor = Color.Black;
            }
        }


        private void txtReturnDate_Enter(object sender, EventArgs e)
        {
            if (txtReturnDate.Text == "DD-MM-YYYY")
            {
                txtReturnDate.Text = "";
                txtReturnDate.ForeColor = Color.Black;
            }
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
            if (txtReturnDate.Text != "DD-MM-YYYY" && !string.IsNullOrWhiteSpace(txtReturnDate.Text))
            {
                ErrorHelper.HideErrorForControl(txtReturnDate);
            }
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            pnlCalenderShow.Visible = false;
            errorProvider1.Clear();

            LentUi lentUi = new LentUi();

            lentUi.userId = Session.LogedInUser.GetUserId();
            lentUi.lentId = this.selectedLentId;
            lentUi.paymentId = Convert.ToInt32(cmbPaymentType.SelectedValue);

            lentUi.returnAmount = (txtReturnAmount.Text == "Enter Return Amount" || txtReturnAmount.Text == "Select Amount") ? "" : txtReturnAmount.Text;
            lentUi.description = (txtDescription.Text == "Enter Description") ? "" : txtDescription.Text;

            lentUi.returnDate = (txtReturnDate.Text == "DD-MM-YYYY")
                ? DateTime.MinValue
                : monthCalendar.SelectionStart;

            CommonValidator.ValidationResult result = lentUi.InsertReturnLentIntoLentUi();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show("Lent returned successfully!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;

                case CommonValidator.ValidationResult.AmountEmpty:
                case CommonValidator.ValidationResult.AmountInvalid:
                case CommonValidator.ValidationResult.AmountTooLarge:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtReturnAmount);
                    break;

                case CommonValidator.ValidationResult.PaymentInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, cmbPaymentType);
                    break;

                case CommonValidator.ValidationResult.DeadlineInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtReturnDate);
                    break;

                case CommonValidator.ValidationResult.DescriptionInvalid:
                case CommonValidator.ValidationResult.DescriptionTooShort:
                case CommonValidator.ValidationResult.DescriptionTooLong:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtDescription);
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    MessageBox.Show("Lent return failed!");
                    break;
            }
        }

        private void txtReturnDate_Click(object sender, EventArgs e)
        {
            pnlCalenderShow.Visible = true;
        }

        private void txtReturnAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtReturnAmount.Text != "Enter Return Amount" && !string.IsNullOrWhiteSpace(txtReturnAmount.Text))
            {
                ErrorHelper.HideErrorForControl(txtReturnAmount);
            }
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            if (txtDescription.Text != "Enter Description" && !string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                ErrorHelper.HideErrorForControl(txtDescription);
            }
        }

        private void cmbPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ErrorHelper.HideErrorForControl(cmbPaymentType);
            cmbPaymentType.AutoCompleteMode = AutoCompleteMode.Append;
            cmbPaymentType.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void cmbPaymentType_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            cmbPaymentType.DroppedDown = true;
        }
    }
}
