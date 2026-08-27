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
using PersonalExpenseCreditTracker.Modules.Settings.Person;
namespace PersonalExpenseCreditTracker.Modules.Borrow
{
    public partial class AddBorrowControls : Form
    {
        private bool ignoreEvents = true;
        public AddBorrowControls()
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

        private void btnBorrowAddCalendar_Click(object sender, EventArgs e)
        {
            pnlBorrowAddCalenderShow.Visible = !pnlBorrowAddCalenderShow.Visible;
        }

        private void txtBorrowAddDeadlineDatePicker_TextChanged(object sender, EventArgs e)
        {
            pnlBorrowAddCalenderShow.Visible = false;
            if (txtBorrowAddDeadlineDatePicker.Text != "DD-MM-YYYY" && !string.IsNullOrWhiteSpace(txtBorrowAddDeadlineDatePicker.Text))
            {
                ErrorHelper.HideErrorForControl(txtBorrowAddDeadlineDatePicker);
            }
        }

        private void txtBorrowAddDeadlineDatePicker_Enter(object sender, EventArgs e)
        {
            if (txtBorrowAddDeadlineDatePicker.Text == "DD-MM-YYYY")
            {
                txtBorrowAddDeadlineDatePicker.Text = "";
                txtBorrowAddDeadlineDatePicker.ForeColor = Color.Black;
            }
          
        }

        private void monthCalendarAddBorrow_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtBorrowAddDeadlineDatePicker.Text = e.Start.ToString("dd-MM-yyyy");
            txtBorrowAddDeadlineDatePicker.ForeColor = Color.Black;
            pnlBorrowAddCalenderShow.Visible = false;

            ErrorHelper.HideErrorForControl(txtBorrowAddDeadlineDatePicker);
        }


        private void txtBorrowAddDeadlineDatePicker_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBorrowAddDeadlineDatePicker.Text))
            {
                txtBorrowAddDeadlineDatePicker.Text = "DD-MM-YYYY";
                txtBorrowAddDeadlineDatePicker.ForeColor = Color.Gray;
            }
            else
            {
                txtBorrowAddDeadlineDatePicker.ForeColor = Color.Black;
            }
        }

        private void txtBorrowAddDescription_Enter(object sender, EventArgs e)
        {
            if (txtBorrowAddDescription.Text == "Enter description")
            {
                txtBorrowAddDescription.Text = "";
                txtBorrowAddDescription.ForeColor = Color.Black;
            }
            pnlBorrowAddCalenderShow.Visible = false;
        }

        private void txtBorrowAddDescription_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBorrowAddDescription.Text))
            {
                txtBorrowAddDescription.Text = "Enter description";
                txtBorrowAddDescription.ForeColor = Color.Gray;
            }
            else
            {
                txtBorrowAddDescription.ForeColor = Color.Black;
            }
        }

        private void txtBorrowAddAmount_Enter(object sender, EventArgs e)
        {
            if (txtBorrowAddAmount.Text == "Select Amount")
            {
                txtBorrowAddAmount.Text = "";
                txtBorrowAddAmount.ForeColor = Color.Black;
            }
            pnlBorrowAddCalenderShow.Visible = false;
        }

        private void txtBorrowAddAmount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBorrowAddAmount.Text))
            {
                txtBorrowAddAmount.Text = "Select Amount";
                txtBorrowAddAmount.ForeColor = Color.Gray;
            }
            else
            {
                txtBorrowAddAmount.ForeColor = Color.Black;
            }
        }

        private void cmbBorrowSelectPerson_Enter(object sender, EventArgs e)
        {
            if (cmbBorrowSelectPerson.Text == "Select Person")
                cmbBorrowSelectPerson.ForeColor = Color.Black;

            pnlBorrowAddCalenderShow.Visible = false;
        }

        private void cmbBorrowSelectPerson_Leave(object sender, EventArgs e)
        {
           
            if (cmbBorrowSelectPerson.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(cmbBorrowSelectPerson.Text) || cmbBorrowSelectPerson.Text == "Select Person")
            {
                cmbBorrowSelectPerson.SelectedIndex = 0;
                cmbBorrowSelectPerson.Text = "Select Person";
                cmbBorrowSelectPerson.ForeColor = Color.Gray;
            }
            else
            {
                cmbBorrowSelectPerson.ForeColor = Color.Black;
            }
        }


        private void cmbBorrowPaymentType_Enter(object sender, EventArgs e)
        {
            if (cmbBorrowPaymentType.Text == "Select Payment Type")
                cmbBorrowPaymentType.ForeColor = Color.Black;

            pnlBorrowAddCalenderShow.Visible = false;
        }

        private void cmbBorrowPaymentType_Leave(object sender, EventArgs e)
        {
            if (cmbBorrowPaymentType.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(cmbBorrowPaymentType.Text) || cmbBorrowPaymentType.Text == "Select Payment Type")
            {
                cmbBorrowPaymentType.SelectedIndex = 0;
                cmbBorrowPaymentType.Text = "Select Payment Type";
                cmbBorrowPaymentType.ForeColor = Color.Gray;
            }
            else
            {
                cmbBorrowPaymentType.ForeColor = Color.Black;
            }
        }


        private void cmbBorrowStatus_Enter(object sender, EventArgs e)
        {
            //if (cmbBorrowStatus.Text == "Select Status")
            //    cmbBorrowStatus.ForeColor = Color.Black;

            //pnlBorrowAddCalenderShow.Visible = false;
        }

        private void cmbBorrowStatus_Leave(object sender, EventArgs e)
        {
            //if (cmbBorrowStatus.SelectedIndex == -1)
            //{
            //    cmbBorrowStatus.Text = "Select Status";
            //    cmbBorrowStatus.ForeColor = Color.Gray;
            //}
            //else
            //{
            //    cmbBorrowStatus.ForeColor = Color.Black;
            //}
        }

        private void LoadFormData()
        {
            ignoreEvents = true;

            cmbBorrowSelectPerson.Text = "Select Person";
            txtBorrowAddDeadlineDatePicker.Text = "DD-MM-YYYY";
            pnlBorrowAddCalenderShow.Visible = false;
            txtBorrowAddDescription.Text = "Enter description";
            txtBorrowAddAmount.Text = "Select Amount";

            CommonUiFunction.LoadInComboBox("spGetAllPersons", Session.LogedInUser.GetUserId(), "Select Person", "+ Add New Person", cmbBorrowSelectPerson);
            CommonUiFunction.LoadInComboBox("spGetAllPaymentTypes", "Select Payment Type", cmbBorrowPaymentType);

            ignoreEvents = false;
        }

        private void AddBorrowControls_Load(object sender, EventArgs e)
        {
            ignoreEvents = true;

            pnlBorrowAddCalenderShow.Visible = false;
            cmbBorrowSelectPerson.Text = "Select Person";
            cmbBorrowSelectPerson.ForeColor = Color.Gray;

            cmbBorrowPaymentType.Text = "Select Payment Type";
            cmbBorrowPaymentType.ForeColor = Color.Gray;

            txtBorrowAddAmount.Text = "Select Amount";
            txtBorrowAddAmount.ForeColor = Color.Gray;

            txtBorrowAddDescription.Text = "Enter description";
            txtBorrowAddDescription.ForeColor = Color.Gray;

            txtBorrowAddDeadlineDatePicker.Text = "DD-MM-YYYY";
            txtBorrowAddDeadlineDatePicker.ForeColor = Color.Gray;

            SetRadius(btnBorrowAddClear, 5);
            SetRadius(btnBorrowAddSave, 5);
            SetRadius(btnBorrowAddCancel, 5);

            LoadFormData();

            cmbBorrowSelectPerson.AutoCompleteMode = AutoCompleteMode.Append;
            cmbBorrowSelectPerson.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbBorrowPaymentType.AutoCompleteMode = AutoCompleteMode.Append;
            cmbBorrowPaymentType.AutoCompleteSource = AutoCompleteSource.ListItems;


            cmbBorrowSelectPerson.MouseClick += (s, ev) => { cmbBorrowSelectPerson.DroppedDown = true; };
            cmbBorrowPaymentType.MouseClick += (s, ev) => { cmbBorrowPaymentType.DroppedDown = true; };
            txtBorrowAddDeadlineDatePicker.Click += txtBorrowAddDeadlineDatePicker_Click;

            ignoreEvents = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {

                if (cmbBorrowSelectPerson.Focused)
                {
                    SelectComboBoxSuggestion(cmbBorrowSelectPerson);
                    return true; 
                }

                else if (cmbBorrowPaymentType.Focused)
                {
                    SelectComboBoxSuggestion(cmbBorrowPaymentType);
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

            // Close dropdown if open
            cmb.DroppedDown = false;
        }


        private void AddBorrowControls_Click(object sender, EventArgs e)
        {
            pnlBorrowAddCalenderShow.Visible = false;
        }


        private void btnBorrowAddClear_Click(object sender, EventArgs e)
        {
            ignoreEvents = true;

            if (cmbBorrowSelectPerson.Items.Count > 0)
                cmbBorrowSelectPerson.SelectedIndex = 0;
            cmbBorrowSelectPerson.Text = "Select Person";
            cmbBorrowSelectPerson.ForeColor = Color.Gray;

            if (cmbBorrowPaymentType.Items.Count > 0)
                cmbBorrowPaymentType.SelectedIndex = 0;
            cmbBorrowPaymentType.Text = "Select Payment Type";
            cmbBorrowPaymentType.ForeColor = Color.Gray;

            txtBorrowAddAmount.Text = "Select Amount";
            txtBorrowAddAmount.ForeColor = Color.Gray;
            txtBorrowAddDescription.Text = "Enter description";
            txtBorrowAddDescription.ForeColor = Color.Gray;
            txtBorrowAddDeadlineDatePicker.Text = "DD-MM-YYYY";
            txtBorrowAddDeadlineDatePicker.ForeColor = Color.Gray;
            pnlBorrowAddCalenderShow.Visible = false;
            errorProvider1.Clear();

            ignoreEvents = false;
        }


        private void btnBorrowAddCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void btnBorrowAddSave_Click(object sender, EventArgs e)
        {
            pnlBorrowAddCalenderShow.Visible = false;
            errorProvider1.Clear();

            int personId = Convert.ToInt32(cmbBorrowSelectPerson.SelectedValue);
            int paymentId = Convert.ToInt32(cmbBorrowPaymentType.SelectedValue);
            string amount = (txtBorrowAddAmount.Text == "Select Amount" || txtBorrowAddAmount.Text == "Enter Amount") ? "" : txtBorrowAddAmount.Text;
            string description = (txtBorrowAddDescription.Text == "Enter description" || txtBorrowAddDescription.Text == "Enter Description") ? "" : txtBorrowAddDescription.Text;

            DateTime parsedDate;
            DateTime deadline = DateTime.MinValue;
            if (txtBorrowAddDeadlineDatePicker.Text != "DD-MM-YYYY" && DateTime.TryParseExact(txtBorrowAddDeadlineDatePicker.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                deadline = parsedDate;
            }

            if (!ErrorHelper.Validate(CommonValidator.ValidatePerson(personId), errorProvider1, cmbBorrowSelectPerson)) return;
            if (!ErrorHelper.Validate(CommonValidator.ValidatePayment(paymentId), errorProvider1, cmbBorrowPaymentType)) return;
            if (!ErrorHelper.Validate(CommonValidator.ValidateAmount(amount), errorProvider1, txtBorrowAddAmount)) return;
            if (!ErrorHelper.Validate(CommonValidator.ValidateDeadline(deadline), errorProvider1, txtBorrowAddDeadlineDatePicker)) return;
            if (!ErrorHelper.Validate(CommonValidator.ValidateDescription(description), errorProvider1, txtBorrowAddDescription)) return;

            
            DialogResult confirmResult = MessageBox.Show(
                "Are you sure you want to add this borrow?",
                "Confirm Add",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            BorrowUI borrowUi = new BorrowUI();
            borrowUi.userId = Session.LogedInUser.GetUserId();
            borrowUi.personId = personId;
            borrowUi.paymentId = paymentId;
            borrowUi.amount = amount;
            borrowUi.description = description;
            borrowUi.deadlineAt = deadline;

            CommonValidator.ValidationResult result = borrowUi.InsertDataIntoBorrowUi();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;

                case CommonValidator.ValidationResult.PersonInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, cmbBorrowSelectPerson);
                    break;

                case CommonValidator.ValidationResult.PaymentInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, cmbBorrowPaymentType);
                    break;

                case CommonValidator.ValidationResult.AmountEmpty:
                case CommonValidator.ValidationResult.AmountInvalid:
                case CommonValidator.ValidationResult.AmountTooLarge:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtBorrowAddAmount);
                    break;

                case CommonValidator.ValidationResult.DeadlineInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtBorrowAddDeadlineDatePicker);
                    break;

                case CommonValidator.ValidationResult.DescriptionInvalid:
                case CommonValidator.ValidationResult.DescriptionTooShort:
                case CommonValidator.ValidationResult.DescriptionTooLong:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtBorrowAddDescription);
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    MessageBox.Show("Borrow added unsuccessfully!");
                    break;
            }
        }


        private void pnlAddBorrowMainBody_Click(object sender, EventArgs e)
        {
            pnlBorrowAddCalenderShow.Visible = false;
        }

        private void btnAddBorrowClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        
        private void btnBorrowAddClear_Resize(object sender, EventArgs e)
        {
            SetRadius(btnBorrowAddClear, 5);
        }

        private void btnBorrowAddCancel_Resize(object sender, EventArgs e)
        {
            SetRadius(btnBorrowAddCancel, 5);
        }

        private void btnBorrowAddSave_Resize(object sender, EventArgs e)
        {
            SetRadius(btnBorrowAddSave, 5);
        }

        private void pnlAddBorrowMainBody_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbBorrowSelectPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;

            ignoreEvents = true; 

            ErrorHelper.HideErrorForControl(cmbBorrowSelectPerson);
            cmbBorrowSelectPerson.AutoCompleteMode = AutoCompleteMode.Append;
            cmbBorrowSelectPerson.AutoCompleteSource = AutoCompleteSource.ListItems;

            int personId = 0;
            if (cmbBorrowSelectPerson.SelectedValue != null)
            {
                DataRowView drv = cmbBorrowSelectPerson.SelectedValue as DataRowView;
                if (drv != null)
                {
                    personId = Convert.ToInt32(drv[0]);
                }
                else
                {
                    personId = Convert.ToInt32(cmbBorrowSelectPerson.SelectedValue);
                }
            }

            if (personId == -99)
            {
                this.Opacity = 0;
                using (var addPersonForm = new PersonalExpenseCreditTracker.Modules.Settings.Person.AddPersonControls())
                {
                    DialogResult result = addPersonForm.ShowDialog(this);
                    this.Opacity = 1;

                    CommonUiFunction.LoadInComboBox(
                        "spGetAllPersons",
                        Session.LogedInUser.GetUserId(),
                        "Select Person",
                        "+ Add New Person",
                        cmbBorrowSelectPerson);

                    if (!string.IsNullOrEmpty(addPersonForm.LastAddedPersonName))
                    {
                        int index = cmbBorrowSelectPerson.FindStringExact(addPersonForm.LastAddedPersonName);
                        if (index != -1)
                        {
                            cmbBorrowSelectPerson.SelectedIndex = index;
                            cmbBorrowSelectPerson.ForeColor = Color.Black; 
                        }
                        else
                        {
                            cmbBorrowSelectPerson.SelectedIndex = 0;
                            cmbBorrowSelectPerson.ForeColor = Color.Gray;
                        }
                    }
                    else
                    {
                        cmbBorrowSelectPerson.SelectedIndex = 0;
                        cmbBorrowSelectPerson.ForeColor = Color.Black;
                    }
                }
            }

            ignoreEvents = false; 
        }


        private void cmbBorrowPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;

            ErrorHelper.HideErrorForControl(cmbBorrowPaymentType);
            cmbBorrowPaymentType.AutoCompleteMode = AutoCompleteMode.Append;
            cmbBorrowPaymentType.AutoCompleteSource = AutoCompleteSource.ListItems;
            
        }

        private void txtBorrowAddDeadlineDatePicker_Click(object sender, EventArgs e)
        {
            pnlBorrowAddCalenderShow.Visible = true;
        }

        private void txtBorrowAddAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtBorrowAddAmount.Text != "Select Amount" && !string.IsNullOrWhiteSpace(txtBorrowAddAmount.Text))
            {
                ErrorHelper.HideErrorForControl(txtBorrowAddAmount);
            }
        }

        private void txtBorrowAddDescription_TextChanged(object sender, EventArgs e)
        {
            if (txtBorrowAddDescription.Text != "Enter description" && !string.IsNullOrWhiteSpace(txtBorrowAddDescription.Text))
            {
                ErrorHelper.HideErrorForControl(txtBorrowAddDescription);
            }
        }

        private void cmbBorrowSelectPerson_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            if (cmbBorrowSelectPerson.SelectedIndex > 0 || cmbBorrowSelectPerson.Text == "Select Person") return;
            cmbBorrowSelectPerson.DroppedDown = true;
        }

        private void cmbBorrowPaymentType_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            if (cmbBorrowPaymentType.SelectedIndex > 0 || cmbBorrowPaymentType.Text == "Select Payment Type") return;
            cmbBorrowPaymentType.DroppedDown = true;
        }

       


    }
}
