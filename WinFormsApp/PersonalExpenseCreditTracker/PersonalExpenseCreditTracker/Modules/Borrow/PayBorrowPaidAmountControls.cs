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
        private bool ignoreEvents = true;
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
            pnlCalenderShow.Visible = false;
            errorProvider1.Clear();

            // Step 1: প্রথমে field ভ্যালিডেশন করো
            string amountText = (txtAmount.Text == "Select Amount" || txtAmount.Text == "Enter Return Amount") ? "" : txtAmount.Text;
            string descriptionText = (txtDescription.Text == "Enter Description") ? "" : txtDescription.Text;

            if (string.IsNullOrWhiteSpace(amountText))
            {
                ErrorHelper.ShowValidationError(CommonValidator.ValidationResult.AmountEmpty, errorProvider1, txtAmount);
                return;
            }

            decimal parsedAmount;
            if (!decimal.TryParse(amountText, out parsedAmount) || parsedAmount <= 0)
            {
                ErrorHelper.ShowValidationError(CommonValidator.ValidationResult.AmountInvalid, errorProvider1, txtAmount);
                return;
            }

            if (cmbPaymentType.SelectedIndex <= 0)
            {
                ErrorHelper.ShowValidationError(CommonValidator.ValidationResult.PaymentInvalid, errorProvider1, cmbPaymentType);
                return;
            }

            // Step 2: ভ্যালিডেশন পাস হলে confirmation দেখাও (এখনো DB তে কিছু save হয়নি)
            DialogResult confirmResult = MessageBox.Show(
                "Are you sure you want to pay this borrow?",
                "Confirm Pay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
            {
                // "No" — কিছু save হবে না, form খোলা থাকবে
                return;
            }

            // Step 3: "Yes" হলে BLL call করো যা ভ্যালিডেশন সহ DB তে save করবে
            BorrowUI borrowUi = new BorrowUI();
            borrowUi.userId = Session.LogedInUser.GetUserId();
            borrowUi.borrowId = this.selectedBorrowId;
            borrowUi.paymentId = Convert.ToInt32(cmbPaymentType.SelectedValue);
            borrowUi.returnAmount = amountText;
            borrowUi.description = descriptionText;

            DateTime returnDate = DateTime.MinValue;
            if (!string.IsNullOrWhiteSpace(txtReturnDate.Text) && txtReturnDate.Text != "DD-MM-YYYY")
            {
                string[] formats = new string[] { "dd-MM-yyyy", "d-M-yyyy", "yyyy-MM-dd", "dd/MM/yyyy", "MM/dd/yyyy", "dd MMM yyyy", "d MMM yyyy", "dd MMMM yyyy" };
                if (!DateTime.TryParseExact(txtReturnDate.Text.Trim(), formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out returnDate))
                {
                    DateTime.TryParse(txtReturnDate.Text.Trim(), out returnDate);
                }
            }
            borrowUi.returnDate = returnDate;

            CommonValidator.ValidationResult result = borrowUi.InsertPayBorrowIntoBorrowUi();
            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    this.Close();
                    break;

                case CommonValidator.ValidationResult.AmountEmpty:
                    ErrorHelper.ShowValidationError(CommonValidator.ValidationResult.AmountEmpty, errorProvider1, txtAmount);
                    break;
                case CommonValidator.ValidationResult.AmountInvalid:
                    ErrorHelper.ShowValidationError(CommonValidator.ValidationResult.AmountInvalid, errorProvider1, txtAmount);
                    break;
                case CommonValidator.ValidationResult.AmountTooLarge:
                    ErrorHelper.ShowValidationError(CommonValidator.ValidationResult.AmountTooLarge, errorProvider1, txtAmount);
                    break;
                case CommonValidator.ValidationResult.PaymentInvalid:
                    ErrorHelper.ShowValidationError(CommonValidator.ValidationResult.PaymentInvalid, errorProvider1, cmbPaymentType);
                    break;
                case CommonValidator.ValidationResult.DeadlineInvalid:
                    ErrorHelper.ShowValidationError(CommonValidator.ValidationResult.DeadlineInvalid, errorProvider1, txtReturnDate);
                    break;
                case CommonValidator.ValidationResult.ReturnAmountDeadlineMustBeTodayOrEarlier:
                    ErrorHelper.ShowValidationError(CommonValidator.ValidationResult.ReturnAmountDeadlineMustBeTodayOrEarlier, errorProvider1, txtReturnDate);
                    break;
                case CommonValidator.ValidationResult.DescriptionInvalid:
                    ErrorHelper.ShowValidationError(CommonValidator.ValidationResult.DescriptionInvalid, errorProvider1, txtDescription);
                    break;
                case CommonValidator.ValidationResult.DescriptionTooShort:
                    ErrorHelper.ShowValidationError(CommonValidator.ValidationResult.DescriptionTooShort, errorProvider1, txtDescription);
                    break;
                case CommonValidator.ValidationResult.DescriptionTooLong:
                    ErrorHelper.ShowValidationError(CommonValidator.ValidationResult.DescriptionTooLong, errorProvider1, txtDescription);
                    break;
                case CommonValidator.ValidationResult.StoreProcedureError:
                    MessageBox.Show("Borrow payment failed!");
                    break;
            }
        }



        private void PayBorrowAmountControls_Load(object sender, EventArgs e)
        {
            ignoreEvents = true;

            monthCalendar.MaxDate = DateTime.Today; 

            SetRadius(pnlInputField, 15);
            SetRadius(pnlPersonDetails, 15);
            SetRadius(btnClear, 5);
            SetRadius(btnSave, 5);
            SetRadius(btnCancel, 5);

            txtAmount.Text = "Enter Return Amount";
            txtReturnDate.Text = "DD-MM-YYYY";
            txtDescription.Text = "Enter Description";
            cmbPaymentType.Text = "Select Payment Type";

            txtAmount.ForeColor = Color.Gray;
            txtReturnDate.ForeColor = Color.Gray;
            txtDescription.ForeColor = Color.Gray;
            cmbPaymentType.ForeColor = Color.Gray;

            CommonUiFunction.LoadInComboBox("spGetAllPaymentTypes", "Select Payment Type", cmbPaymentType);
            cmbPaymentType.MouseClick += (s, ev) => { cmbPaymentType.DroppedDown = true; };
            txtReturnDate.Click += txtReturnDate_Click;

            cmbPaymentType.AutoCompleteMode = AutoCompleteMode.Append;
            cmbPaymentType.AutoCompleteSource = AutoCompleteSource.ListItems;

            CommonUiFunction.SetComboBoxHeightAndOwnerDraw1(cmbPaymentType);

            ignoreEvents = false;
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {

                if (cmbPaymentType.Focused)
                {
                    SelectComboBoxSuggestion(cmbPaymentType);
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

        private void btnAddCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAddClear_Click(object sender, EventArgs e)
        {
            ignoreEvents = true;

            txtAmount.Text = "Enter Return Amount";
            txtReturnDate.Text = "DD-MM-YYYY";

            if (cmbPaymentType.Items.Count > 0)
                cmbPaymentType.SelectedIndex = 0;
            cmbPaymentType.Text = "Select Payment Type";
            cmbPaymentType.ForeColor = Color.Gray;

            txtDescription.Text = "Enter Description";
            txtAmount.ForeColor = Color.Gray;
            txtReturnDate.ForeColor = Color.Gray;
            txtDescription.ForeColor = Color.Gray;

            pnlCalenderShow.Visible = false;
            errorProvider1.Clear();

            ignoreEvents = false;
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
            if (cmbPaymentType.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cmbPaymentType.Text))
            {
                cmbPaymentType.SelectedIndex = 0;
                cmbPaymentType.Text = "Select Payment Type";
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
            ErrorHelper.HideErrorForControl(txtReturnDate);
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

        private void txtReturnDate_Click(object sender, EventArgs e)
        {
            pnlCalenderShow.Visible = true;
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtAmount.Text != "Enter Return Amount" && !string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                ErrorHelper.HideErrorForControl(txtAmount);
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
            if (ignoreEvents) return;

            ErrorHelper.HideErrorForControl(cmbPaymentType);
            cmbPaymentType.AutoCompleteMode = AutoCompleteMode.Append;
            cmbPaymentType.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void cmbPaymentType_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            if (cmbPaymentType.SelectedIndex > 0 || cmbPaymentType.Text == "Select Payment Type") return;
            cmbPaymentType.DroppedDown = true;
        }
    }
}
