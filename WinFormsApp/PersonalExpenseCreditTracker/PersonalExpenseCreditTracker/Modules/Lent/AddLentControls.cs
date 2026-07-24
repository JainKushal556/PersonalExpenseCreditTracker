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

            //List<string> personList =LentUi.retriveListForComboBoxAtUi("spGetAllPersons", "PersonName", 11);
            //comboBoxLentSelectPerson.Items.Add("Select Person");
            //foreach (string person in personList)
            //{
            //    comboBoxLentSelectPerson.Items.Add(person);
            //}

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
            //MessageBox.Show("Lent Details Saved");
            errorProvider1.Clear();

            LentUi lentUi = new LentUi();

            lentUi.userId = 1;
            lentUi.lentId = -1;
            lentUi.personId = Convert.ToInt32(comboBoxLentSelectPerson.SelectedIndex);
            lentUi.paymentId = Convert.ToInt32(comboBoxLentPaymentType.SelectedValue);
            lentUi.statusId = Convert.ToInt32(comboBoxLentStatus.SelectedValue);
            lentUi.amount = txtLentAddAmount.Text;
            lentUi.deadlineAt = monthCalendarAddLent.SelectionStart;
            lentUi.description = textBoxLentAddDescription.Text;
            CommonValidator.ValidationResult result =lentUi.InsertDataIntoLentUi();

            if (result == CommonValidator.ValidationResult.Success)
            {
                MessageBox.Show("Lent added Sucessfully");
            }
            else
            {
                ErrorHelper.ShowValidationError(result, errorProvider1,comboBoxLentSelectPerson);
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
