using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLLayer.Common;
using PersonalExpenseCreditTracker.Common;
namespace PersonalExpenseCreditTracker.Modules.Lent
{
    public partial class AddLentControls : Form
    {
        public AddLentControls()
        {
            InitializeComponent();
        }


        private void AddLentControls_Load(object sender, EventArgs e)
        {
            comboBoxLentSelectPerson.Text = "Select Person";
            txtLentAddDeadlineDatePicker.Text = "DD-MM-YYYY";
            panelLentAddCalenderShow.Visible = false;

            textBoxLentAddDescription.Text ="Enter description";
            txtLentAddAmount.Text = "Select Amount";

            CommonUiFunction.LoadInComboBox("spGetAllPersons", "PersonName",11,"Select Person",comboBoxLentSelectPerson);
            CommonUiFunction.LoadInComboBox("spGetAllPaymentTypes", "PaymentName","Select Payment Type", comboBoxLentPaymentType);
            CommonUiFunction.LoadInComboBox("spGetAllLentBorrowStatus", "StatusName", "Select Status", comboBoxLentStatus);

            
        }
        
        private void btnLentAddCalendar_Click(object sender, EventArgs e)
        {
            panelLentAddCalenderShow.Visible = !panelLentAddCalenderShow.Visible;
        }


        private void monthCalendarAddLent_DateChanged(object sender, DateRangeEventArgs e)
        {
            txtLentAddDeadlineDatePicker.Text = e.Start.ToString("dd-MM-yyyy");
        }


        private void txtLentAddDeadlineDatePicker_TextChanged(object sender, EventArgs e)
        {
            panelLentAddCalenderShow.Visible = true;
        }


        private void textBoxLentAddDescription_Enter(object sender, EventArgs e)
        {
            if(textBoxLentAddDescription.Text =="Enter description")
            {
                textBoxLentAddDescription.Text = "";
            }
        }


        private void textBoxLentAddDescription_Leave(object sender, EventArgs e)
        {
            if (textBoxLentAddDescription.Text == "")
            {
                textBoxLentAddDescription.Text = "Enter description";
            }
        }

        private void txtLentAddAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLentAddAmount_Enter(object sender, EventArgs e)
        {
            if (txtLentAddAmount.Text == "Select Amount")
            {
                txtLentAddAmount.Text = "";
            }
        }


        private void txtLentAddAmount_Leave(object sender, EventArgs e)
        {
            if (txtLentAddAmount.Text == "")
            {
                txtLentAddAmount.Text = "Select Amount";
            }
        }

        private void txtLentAddAmount_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void btnLentAddClear_Click(object sender, EventArgs e)
        {
            comboBoxLentSelectPerson.Text = "Select Person";
            comboBoxLentPaymentType.Text = "Select Payment Type";
            comboBoxLentStatus.Text = "Select Status";
            txtLentAddAmount.Text = "Select Amount";
            textBoxLentAddDescription.Text = "Enter description";
            txtLentAddDeadlineDatePicker.Text = "DD-MM-YYYY";
            panelLentAddCalenderShow.Visible = false;
        }

        private void btnLentAddCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLentAddSave_Click(object sender, EventArgs e)
        {
            // Clear all previous validation errors
            errorProvider1.Clear();

            // Create a new object to store the user's input
            LentUi lentUi = new LentUi();

            // Assign values from the form controls to the object
            lentUi.userId = 11;
            lentUi.lentId = -1;
            lentUi.personId = Convert.ToInt32(comboBoxLentSelectPerson.SelectedIndex);
            lentUi.paymentId = Convert.ToInt32(comboBoxLentPaymentType.SelectedIndex);
            lentUi.statusId = Convert.ToInt32(comboBoxLentStatus.SelectedIndex);

            // If the placeholder text is still present, pass an empty string
            lentUi.amount = (txtLentAddAmount.Text == "Select Amount") ? "" : txtLentAddAmount.Text;
            lentUi.description = (textBoxLentAddDescription.Text == "Enter description") ? "" : textBoxLentAddDescription.Text;

            // If no deadline is selected, assign DateTime.MinValue
            // Otherwise, assign the selected date from the calendar
            lentUi.deadlineAt = (txtLentAddDeadlineDatePicker.Text == "DD-MM-YYYY") ? DateTime.MinValue : monthCalendarAddLent.SelectionStart;


            CommonValidator.ValidationResult result = lentUi.InsertDataIntoLentUi();
            // Perform action based on the validation result
            switch (result)
            {
                // Data is valid and inserted successfully
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show("Lent added successfully!");
                    break;
                case CommonValidator.ValidationResult.PersonInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, comboBoxLentSelectPerson);
                    break;

                case CommonValidator.ValidationResult.PaymentInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, comboBoxLentPaymentType);
                    break;

                case CommonValidator.ValidationResult.StatusInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, comboBoxLentStatus);
                    break;

                case CommonValidator.ValidationResult.AmountEmpty:
                case CommonValidator.ValidationResult.AmountInvalid:
                case CommonValidator.ValidationResult.AmountTooLarge:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtLentAddAmount);
                    break;

                case CommonValidator.ValidationResult.DescriptionInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, textBoxLentAddDescription);
                    break;

                case CommonValidator.ValidationResult.DeadlineInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtLentAddDeadlineDatePicker);
                    break;
                case CommonValidator.ValidationResult.StoreProcedureError:
                    MessageBox.Show("Lent added Unsuccessfully!");
                    break;
            }

        }



        private void comboBoxLentSelectPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (comboBoxLentSelectPerson.SelectedIndex > 0)
            {
                errorProvider1.Clear();
            }
        }
    }


}
