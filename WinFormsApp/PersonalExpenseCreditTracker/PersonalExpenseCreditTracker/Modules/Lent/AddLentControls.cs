using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            txtLentAddDeadlineDatePicker.Text = "DD-MM-YYYY";
            panelLentAddCalenderShow.Visible = false;

            textBoxLentAddDescription.Text ="Enter description";
            txtLentAddAmount.Text = "Select Amount";

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
            MessageBox.Show("Lent Details Saved");
        }
    }
}
