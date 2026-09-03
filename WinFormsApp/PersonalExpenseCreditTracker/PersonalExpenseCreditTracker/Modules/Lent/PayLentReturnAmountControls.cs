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
            ignoreEvents = true;

            monthCalendar.MaxDate = DateTime.Today; 

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

            cmbPaymentType.AutoCompleteMode = AutoCompleteMode.Append;
            cmbPaymentType.AutoCompleteSource = AutoCompleteSource.ListItems;

            CommonUiFunction.SetComboBoxHeightAndOwnerDraw1(cmbPaymentType);
            
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


        // Select suggestion on Enter key press
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                // If Payment Type ComboBox has focus
                if (cmbPaymentType.Focused)
                {
                    SelectComboBoxSuggestion(cmbPaymentType);
                    return true; // Enter action complete, no form submit or beep
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

        private void btnAddCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddClear_Click(object sender, EventArgs e)
        {
            ignoreEvents = true;

            txtReturnAmount.Text = "Enter Return Amount";
            txtReturnDate.Text = "DD-MM-YYYY";

            if (cmbPaymentType.Items.Count > 0)
                cmbPaymentType.SelectedIndex = 0;
            cmbPaymentType.Text = "Select Payment Type";
            cmbPaymentType.ForeColor = Color.Gray;

            txtDescription.Text = "Enter Description";
            txtReturnAmount.ForeColor = Color.Gray;
            txtReturnDate.ForeColor = Color.Gray;
            txtDescription.ForeColor = Color.Gray;

            pnlCalenderShow.Visible = false;
            errorProvider1.Clear();

            ignoreEvents = false;
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

            // Step 1: প্রথমে field ভ্যালিডেশন করো
            string returnAmountText = (txtReturnAmount.Text == "Enter Return Amount" || txtReturnAmount.Text == "Select Amount") ? "" : txtReturnAmount.Text;
            string descriptionText = (txtDescription.Text == "Enter Description") ? "" : txtDescription.Text;

            if (string.IsNullOrWhiteSpace(returnAmountText))
            {
                ErrorHelper.ShowValidationError(CommonValidator.ValidationResult.AmountEmpty, errorProvider1, txtReturnAmount);
                return;
            }

            decimal parsedAmount;
            if (!decimal.TryParse(returnAmountText, out parsedAmount) || parsedAmount <= 0)
            {
                ErrorHelper.ShowValidationError(CommonValidator.ValidationResult.AmountInvalid, errorProvider1, txtReturnAmount);
                return;
            }

            if (cmbPaymentType.SelectedIndex <= 0)
            {
                ErrorHelper.ShowValidationError(CommonValidator.ValidationResult.PaymentInvalid, errorProvider1, cmbPaymentType);
                return;
            }

            // Step 2: Validation পাস হলে confirmation দেখাও (এখনো DB তে কিছু save হয়নি)
            DialogResult confirmResult = MessageBox.Show(
                "Are you sure you want to return this lent?",
                "Confirm Return",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
            {
                // "No" — কিছু save হবে না, form খোলা থাকবে
                return;
            }

            // Step 3: "Yes" হলে BLL call করো যা DB তে save করবে
            LentUi lentUi = new LentUi();
            lentUi.userId = Session.LogedInUser.GetUserId();
            lentUi.lentId = this.selectedLentId;
            lentUi.paymentId = Convert.ToInt32(cmbPaymentType.SelectedValue);
            lentUi.returnAmount = returnAmountText;
            lentUi.description = descriptionText;

            DateTime returnDate = DateTime.MinValue;
            if (!string.IsNullOrWhiteSpace(txtReturnDate.Text) && txtReturnDate.Text != "DD-MM-YYYY")
            {
                string[] formats = new string[] { "dd-MM-yyyy", "d-M-yyyy", "yyyy-MM-dd", "dd/MM/yyyy", "MM/dd/yyyy", "dd MMM yyyy", "d MMM yyyy", "dd MMMM yyyy" };
                if (!DateTime.TryParseExact(txtReturnDate.Text.Trim(), formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out returnDate))
                {
                    DateTime.TryParse(txtReturnDate.Text.Trim(), out returnDate);
                }
            }
            lentUi.returnDate = returnDate;

            CommonValidator.ValidationResult result = lentUi.InsertReturnLentIntoLentUi();
            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    this.Close();
                    break;

                case CommonValidator.ValidationResult.AmountEmpty:
                    ErrorHelper.ShowValidationError(CommonValidator.ValidationResult.AmountEmpty, errorProvider1, txtReturnAmount);
                    break;
                case CommonValidator.ValidationResult.AmountInvalid:
                    ErrorHelper.ShowValidationError(CommonValidator.ValidationResult.AmountInvalid, errorProvider1, txtReturnAmount);
                    break;
                case CommonValidator.ValidationResult.AmountTooLarge:
                    ErrorHelper.ShowValidationError(CommonValidator.ValidationResult.AmountTooLarge, errorProvider1, txtReturnAmount);
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
