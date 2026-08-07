using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using BLLayer.Common;
using PersonalExpenseCreditTracker.Common;
using PersonalExpenseCreditTracker.Session;
using System.Data;

namespace PersonalExpenseCreditTracker.Modules.Lent
{
    public partial class AddLentControls : Form
    {
        public AddLentControls()
        {
            InitializeComponent();
            LoadFormData();
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

        private void LoadFormData()
        {
            comboBoxLentSelectPerson.Text = "Select Person";
            txtLentAddDeadlineDatePicker.Text = "DD-MM-YYYY";
            panelLentAddCalenderShow.Visible = false;

            textBoxLentAddDescription.Text ="Enter description";
            txtLentAddAmount.Text = "Select Amount";

            CommonUiFunction.LoadInComboBox("spGetAllPersons",Session.LogedInUser.GetUserId() ,"Select Person","+ Add New Person",comboBoxLentSelectPerson);
            CommonUiFunction.LoadInComboBox("spGetAllPaymentTypes","Select Payment Type", comboBoxLentPaymentType);
        }

        private void btnLentAddCalendar_Click(object sender, EventArgs e)
        {
             panelLentAddCalenderShow.Visible = !panelLentAddCalenderShow.Visible;
        }

        private void txtLentAddDeadlineDatePicker_TextChanged(object sender, EventArgs e)
        {
            panelLentAddCalenderShow.Visible = false;
        }

        private void txtLentAddDeadlineDatePicker_Enter(object sender, EventArgs e)
        {
            if (txtLentAddDeadlineDatePicker.Text == "DD-MM-YYYY")
            {
                txtLentAddDeadlineDatePicker.Text = "";
                txtLentAddDeadlineDatePicker.ForeColor = Color.Black;
            }
            //panelLentAddCalenderShow.Visible = true;
        }

        private void txtLentAddDeadlineDatePicker_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLentAddDeadlineDatePicker.Text))
            {
                txtLentAddDeadlineDatePicker.Text = "DD-MM-YYYY";
                txtLentAddDeadlineDatePicker.ForeColor = Color.Gray;
            }
            else
            {
                txtLentAddDeadlineDatePicker.ForeColor = Color.Black;
            }
        }

        private void textBoxLentAddDescription_Enter(object sender, EventArgs e)
        {
            if (textBoxLentAddDescription.Text == "Enter description")
            {
                textBoxLentAddDescription.Text = "";
                textBoxLentAddDescription.ForeColor = Color.Black;
            }

            panelLentAddCalenderShow.Visible = false;
        }

        private void textBoxLentAddDescription_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLentAddDescription.Text))
            {
                textBoxLentAddDescription.Text = "Enter description";
                textBoxLentAddDescription.ForeColor = Color.Gray;
            }
            else
            {
                textBoxLentAddDescription.ForeColor = Color.Black;
            }
        }

        private void txtLentAddAmount_Enter(object sender, EventArgs e)
        {
            if (txtLentAddAmount.Text == "Select Amount")
            {
                txtLentAddAmount.Text = "";
                txtLentAddAmount.ForeColor = Color.Black;
                
            }
            panelLentAddCalenderShow.Visible = false;
        }

        private void txtLentAddAmount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLentAddAmount.Text))
            {
                txtLentAddAmount.Text = "Select Amount";
                txtLentAddAmount.ForeColor = Color.Gray;
            }
        }

        private void comboBoxLentSelectPerson_Enter(object sender, EventArgs e)
        {
            if (comboBoxLentSelectPerson.Text == "Select Person")
                comboBoxLentSelectPerson.ForeColor = Color.Black;

            panelLentAddCalenderShow.Visible = false;
        }

        private void comboBoxLentSelectPerson_Leave(object sender, EventArgs e)
        {
          
            if (comboBoxLentSelectPerson.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(comboBoxLentSelectPerson.Text) || comboBoxLentSelectPerson.Text == "Select Person")
            {
                comboBoxLentSelectPerson.SelectedIndex = 0;
                comboBoxLentSelectPerson.Text = "Select Person";
                comboBoxLentSelectPerson.ForeColor = Color.Gray;
            }
            else
            {
                comboBoxLentSelectPerson.ForeColor = Color.Black;
            }
        }

        private void comboBoxLentPaymentType_Enter(object sender, EventArgs e)
        {
            if (comboBoxLentPaymentType.Text == "Select Payment Type")
                comboBoxLentPaymentType.ForeColor = Color.Black;

            panelLentAddCalenderShow.Visible = false;
        }

        private void comboBoxLentPaymentType_Leave(object sender, EventArgs e)
        {
            if (comboBoxLentPaymentType.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(comboBoxLentPaymentType.Text) || comboBoxLentPaymentType.Text == "Select Payment Type")
            {
                comboBoxLentPaymentType.SelectedIndex = 0;
                comboBoxLentPaymentType.Text = "Select Payment Type";
                comboBoxLentPaymentType.ForeColor = Color.Gray;
            }
            else
            {
                comboBoxLentPaymentType.ForeColor = Color.Black;
            }
        }


        private void comboBoxLentStatus_Enter(object sender, EventArgs e)
        {
            //if (comboBoxLentStatus.Text == "Select Status")
            //    comboBoxLentStatus.ForeColor = Color.Black;

            //panelLentAddCalenderShow.Visible = false;
        }

        private void comboBoxLentStatus_Leave(object sender, EventArgs e)
        {
            //if (comboBoxLentStatus.SelectedIndex == -1 || comboBoxLentStatus.Text == "Select Status")
            //{
            //    comboBoxLentStatus.Text = "Select Status";
            //    comboBoxLentStatus.ForeColor = Color.Gray;
            //}
        }

        private void AddLentControls_Load(object sender, EventArgs e)
        {
            panelLentAddCalenderShow.Visible = false;

            comboBoxLentSelectPerson.Text = "Select Person";
            comboBoxLentSelectPerson.ForeColor = Color.Gray;

            comboBoxLentPaymentType.Text = "Select Payment Type";
            comboBoxLentPaymentType.ForeColor = Color.Gray;

            txtLentAddAmount.Text = "Select Amount";
            txtLentAddAmount.ForeColor = Color.Gray;

            textBoxLentAddDescription.Text = "Enter description";
            textBoxLentAddDescription.ForeColor = Color.Gray;

            txtLentAddDeadlineDatePicker.Text = "DD-MM-YYYY";
            txtLentAddDeadlineDatePicker.ForeColor = Color.Gray;

       
            // Load ComboBoxes
            CommonUiFunction.LoadInComboBox("spGetAllPersons", Session.LogedInUser.GetUserId(), "Select Person", "+ Add New Person", comboBoxLentSelectPerson);
            CommonUiFunction.LoadInComboBox("spGetAllPaymentTypes", "Select Payment Type", comboBoxLentPaymentType);

            // Mouse click dropdown handlers
            comboBoxLentSelectPerson.MouseClick += (s, ev) => { comboBoxLentSelectPerson.DroppedDown = true; };
            comboBoxLentPaymentType.MouseClick += (s, ev) => { comboBoxLentPaymentType.DroppedDown = true; };
        }


        private void AddLentControls_Click(object sender, EventArgs e)
        {
            panelLentAddCalenderShow.Visible = false;
        }

        private void btnLentAddClear_Click(object sender, EventArgs e)
        {
            comboBoxLentSelectPerson.Text = "Select Person";
            comboBoxLentPaymentType.Text = "Select Payment Type";
            comboBoxLentSelectPerson.ForeColor = Color.Gray;
            comboBoxLentPaymentType.ForeColor = Color.Gray;
            txtLentAddAmount.Text = "Select Amount";
            txtLentAddAmount.ForeColor = Color.Gray;
            textBoxLentAddDescription.Text = "Enter description";
            textBoxLentAddDescription.ForeColor = Color.Gray;
            txtLentAddDeadlineDatePicker.Text = "DD-MM-YYYY";
            txtLentAddDeadlineDatePicker.ForeColor = Color.Gray;
            panelLentAddCalenderShow.Visible = false;
            errorProvider1.Clear();
        }

        private void btnLentAddCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLentAddSave_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            LentUi lentUi = new LentUi();

            lentUi.userId = Session.LogedInUser.GetUserId();
            lentUi.lentId = -1;
            lentUi.personId = Convert.ToInt32(comboBoxLentSelectPerson.SelectedValue);
            lentUi.paymentId = Convert.ToInt32(comboBoxLentPaymentType.SelectedValue);

            lentUi.amount = (txtLentAddAmount.Text == "Select Amount" || txtLentAddAmount.Text == "Enter Amount") ? "" : txtLentAddAmount.Text;
            lentUi.description = (textBoxLentAddDescription.Text == "Enter description" || textBoxLentAddDescription.Text == "Enter Description") ? "" : textBoxLentAddDescription.Text;
            lentUi.deadlineAt = (txtLentAddDeadlineDatePicker.Text == "DD-MM-YYYY") ? DateTime.MinValue : monthCalendarAddLent.SelectionStart;

            CommonValidator.ValidationResult result = lentUi.InsertDataIntoLentUi();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show("Lent added successfully!");
                    this.Close();
                    break;

                case CommonValidator.ValidationResult.PersonInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, comboBoxLentSelectPerson);
                    break;

                case CommonValidator.ValidationResult.PaymentInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, comboBoxLentPaymentType);
                    break;

                case CommonValidator.ValidationResult.AmountEmpty:
                case CommonValidator.ValidationResult.AmountInvalid:
                case CommonValidator.ValidationResult.AmountTooLarge:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtLentAddAmount);
                    break;

                case CommonValidator.ValidationResult.DeadlineInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtLentAddDeadlineDatePicker);
                    break;

                case CommonValidator.ValidationResult.DescriptionInvalid:
                case CommonValidator.ValidationResult.DescriptionTooShort:
                case CommonValidator.ValidationResult.DescriptionTooLong:
                    ErrorHelper.ShowValidationError(result, errorProvider1, textBoxLentAddDescription);
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    MessageBox.Show("Lent added unsuccessfully!");
                    break;
            }
        }


        private void comboBoxLentSelectPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            ErrorHelper.HideErrorForControl(comboBoxLentSelectPerson);
            comboBoxLentSelectPerson.AutoCompleteMode = AutoCompleteMode.Append;
            comboBoxLentSelectPerson.AutoCompleteSource = AutoCompleteSource.ListItems;

            if (comboBoxLentSelectPerson.SelectedValue == null)
                return;

            int personId = 0;
            DataRowView drv = comboBoxLentSelectPerson.SelectedValue as DataRowView;
            if (drv != null)
            {
                personId = Convert.ToInt32(drv[0]);
            }
            else
            {
                personId = Convert.ToInt32(comboBoxLentSelectPerson.SelectedValue);
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
                        CommonUiFunction.LoadInComboBox("spGetAllPersons", Session.LogedInUser.GetUserId(), "Select Person", "+ Add New Person", comboBoxLentSelectPerson);
                    }
                    else
                    {
                        comboBoxLentSelectPerson.SelectedIndex = 0;
                    }
                }
            }
        }


        private void comboBoxLentPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ErrorHelper.HideErrorForControl(comboBoxLentPaymentType);
            comboBoxLentPaymentType.AutoCompleteMode = AutoCompleteMode.Append;
            comboBoxLentPaymentType.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void panelMainBody_Click(object sender, EventArgs e)
        {
            panelLentAddCalenderShow.Visible = false;
        }

        private void btnAddLentClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLentAddClear_Resize(object sender, EventArgs e)
        {
            SetRadius(btnLentAddClear, 5);
        }

        private void btnLentAddCancel_Resize(object sender, EventArgs e)
        {
            SetRadius(btnLentAddCancel, 5);
        }

        private void btnLentAddSave_Resize(object sender, EventArgs e)
        {
            SetRadius(btnLentAddSave, 5);
        }


        private void monthCalendarAddLent_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtLentAddDeadlineDatePicker.Text = e.Start.ToString("dd-MM-yyyy");
            txtLentAddDeadlineDatePicker.ForeColor = Color.Black;
            panelLentAddCalenderShow.Visible = false;
            ErrorHelper.HideErrorForControl(txtLentAddDeadlineDatePicker);
        }


        private void txtLentAddAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtLentAddAmount.Text != "Select Amount" && !string.IsNullOrWhiteSpace(txtLentAddAmount.Text))
            {
                ErrorHelper.HideErrorForControl(txtLentAddAmount);
            }
        }

        private void textBoxLentAddDescription_TextChanged(object sender, EventArgs e)
        {
            if (textBoxLentAddDescription.Text != "Enter description" && !string.IsNullOrWhiteSpace(textBoxLentAddDescription.Text))
            {
                ErrorHelper.HideErrorForControl(textBoxLentAddDescription);
            }
        }

    }
}
