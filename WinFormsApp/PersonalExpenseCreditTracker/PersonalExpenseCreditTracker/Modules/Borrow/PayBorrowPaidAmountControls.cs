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

            BorrowUI borrowUi = new BorrowUI();
            borrowUi.userId = Session.LogedInUser.GetUserId();
            borrowUi.borrowId = this.selectedBorrowId;
            borrowUi.paymentId = Convert.ToInt32(cmbPaymentType.SelectedValue);
            borrowUi.returnAmount = (txtAmount.Text == "Select Amount") ? "" : txtAmount.Text;
            borrowUi.description = (txtDescription.Text == "Enter Description") ? "" : txtDescription.Text;

            DateTime returnDate = DateTime.MinValue;
            if (!string.IsNullOrWhiteSpace(txtReturnDate.Text))
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
                    DialogResult confirmResult = MessageBox.Show(
                        "Are you sure you want to pay this borrow?",
                        "Confirm Pay",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (confirmResult != DialogResult.Yes)
                    {
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        return;
                    }
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
                case CommonValidator.ValidationResult.ReturnAmountDeadlineMustBeTodayOrEarlier:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtReturnDate);
                    break;

                case CommonValidator.ValidationResult.DescriptionInvalid:
                case CommonValidator.ValidationResult.DescriptionTooShort:
                case CommonValidator.ValidationResult.DescriptionTooLong:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtDescription);
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
