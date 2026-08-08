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

            cmbBorrowSelectPerson.MouseClick += (s, ev) => { cmbBorrowSelectPerson.DroppedDown = true; };
            cmbBorrowPaymentType.MouseClick += (s, ev) => { cmbBorrowPaymentType.DroppedDown = true; };
            txtBorrowAddDeadlineDatePicker.Click += txtBorrowAddDeadlineDatePicker_Click;
        }

        private void AddBorrowControls_Click(object sender, EventArgs e)
        {
            pnlBorrowAddCalenderShow.Visible = false;
        }


        private void btnBorrowAddClear_Click(object sender, EventArgs e)
        {
            cmbBorrowSelectPerson.Text = "Select Person";
            cmbBorrowPaymentType.Text = "Select Payment Type";
            //cmbBorrowStatus.Text = "Select Status";
            txtBorrowAddAmount.Text = "Select Amount";
            txtBorrowAddDescription.Text = "Enter description";
            txtBorrowAddDeadlineDatePicker.Text = "DD-MM-YYYY";
            pnlBorrowAddCalenderShow.Visible = false;
        }


        private void btnBorrowAddCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void btnBorrowAddSave_Click(object sender, EventArgs e)
        {
            pnlBorrowAddCalenderShow.Visible = false;
            errorProvider1.Clear();

            BorrowUI borrowUi = new BorrowUI();
            borrowUi.userId = Session.LogedInUser.GetUserId();
            borrowUi.personId = Convert.ToInt32(cmbBorrowSelectPerson.SelectedValue);
            borrowUi.paymentId = Convert.ToInt32(cmbBorrowPaymentType.SelectedValue);

            borrowUi.amount = (txtBorrowAddAmount.Text == "Select Amount" || txtBorrowAddAmount.Text == "Enter Amount") ? "" : txtBorrowAddAmount.Text;
            borrowUi.description = (txtBorrowAddDescription.Text == "Enter description" || txtBorrowAddDescription.Text == "Enter Description") ? "" : txtBorrowAddDescription.Text;

            DateTime parsedDate;
            if (txtBorrowAddDeadlineDatePicker.Text != "DD-MM-YYYY" && DateTime.TryParseExact(txtBorrowAddDeadlineDatePicker.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                borrowUi.deadlineAt = parsedDate;
            }
            else
            {
                borrowUi.deadlineAt = DateTime.MinValue;
            }

            CommonValidator.ValidationResult result = borrowUi.InsertDataIntoLentUi(); // অথবা borrowUi.InsertDataIntoBorrowUi()

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show("Borrow added successfully!");
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
            ErrorHelper.HideErrorForControl(cmbBorrowSelectPerson);
            cmbBorrowSelectPerson.AutoCompleteMode = AutoCompleteMode.Append;
            cmbBorrowSelectPerson.AutoCompleteSource = AutoCompleteSource.ListItems;

            if (cmbBorrowSelectPerson.SelectedValue == null)
                return;

            int personId = 0;
            DataRowView drv = cmbBorrowSelectPerson.SelectedValue as DataRowView;

            if (drv != null)
            {
                personId = Convert.ToInt32(drv[0]);
            }
            else
            {
                personId = Convert.ToInt32(cmbBorrowSelectPerson.SelectedValue);
            }

            if (personId == -99)
            {
                this.Hide();

                using (var addPersonForm = new PersonalExpenseCreditTracker.Modules.Settings.Person.AddPersonControls())
                {
                    DialogResult result = addPersonForm.ShowDialog();

                    this.Show();

                    if (result == DialogResult.OK)
                    {
                        CommonUiFunction.LoadInComboBox(
                            "spGetAllPersons",
                            Session.LogedInUser.GetUserId(),
                            "Select Person",
                            "+ Add New Person",
                            cmbBorrowSelectPerson);
                    }
                    else
                    {
                        cmbBorrowSelectPerson.SelectedIndex = 0;
                    }
                }
            }
        }


        private void cmbBorrowPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            cmbBorrowSelectPerson.DroppedDown = true;
        }

        private void cmbBorrowPaymentType_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            cmbBorrowPaymentType.DroppedDown = true;
        }

       


    }
}
