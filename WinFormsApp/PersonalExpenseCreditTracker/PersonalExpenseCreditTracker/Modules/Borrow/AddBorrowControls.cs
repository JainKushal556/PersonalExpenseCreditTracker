using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalExpenseCreditTracker.Modules.Borrow
{
    public partial class AddBorrowControls : Form
    {
        public AddBorrowControls()
        {
            InitializeComponent();
        }


        private void btnBorrowAddCalendar_Click(object sender, EventArgs e)
        {
            pnlBorrowAddCalenderShow.Visible = !pnlBorrowAddCalenderShow.Visible;
        }


        private void monthCalendarAddBorrow_DateChanged(object sender, DateRangeEventArgs e)
        {
            txtBorrowAddDeadlineDatePicker.Text = e.Start.ToString("dd-MM-yyyy");
        }


        private void txtBorrowAddDeadlineDatePicker_TextChanged(object sender, EventArgs e)
        {
            pnlBorrowAddCalenderShow.Visible = true;
        }


        private void txtBorrowAddDescription_Enter(object sender, EventArgs e)
        {
            if (txtBorrowAddDescription.Text == "Enter description")
            {
                txtBorrowAddDescription.Text = "";
            }
        }


        private void txtBorrowAddDescription_Leave(object sender, EventArgs e)
        {
            if (txtBorrowAddDescription.Text == "")
            {
                txtBorrowAddDescription.Text = "Enter description";
            }
        }

        private void txtBorrowAddAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBorrowAddAmount_Enter(object sender, EventArgs e)
        {
            if (txtBorrowAddAmount.Text == "Select Amount")
            {
                txtBorrowAddAmount.Text = "";
            }
        }


        private void txtBorrowAddAmount_Leave(object sender, EventArgs e)
        {
            if (txtBorrowAddAmount.Text == "")
            {
                txtBorrowAddAmount.Text = "Select Amount";
            }
        }


        private void txtBorrowAddDeadlineDatePicker_Enter(object sender, EventArgs e)
        {
            if (txtBorrowAddDeadlineDatePicker.Text == "DD-MM-YYYY")
            {
                txtBorrowAddDeadlineDatePicker.Text = "";
            }
        }


        private void txtBorrowAddDeadlineDatePicker_Leave(object sender, EventArgs e)
        {
            if (txtBorrowAddDeadlineDatePicker.Text == "")
            {
                txtBorrowAddDeadlineDatePicker.Text = "DD-MM-YYYY";
            }
        }


        private void AddBorrowControls_Load(object sender, EventArgs e)
        {
            txtBorrowAddDeadlineDatePicker.Text = "DD-MM-YYYY";
            pnlBorrowAddCalenderShow.Visible = false;
            txtBorrowAddDescription.Text = "Enter description";
            txtBorrowAddAmount.Text = "Select Amount";
        }


        private void AddBorrowControls_Click(object sender, EventArgs e)
        {
            pnlBorrowAddCalenderShow.Visible = false;
        }


        private void btnBorrowAddClear_Click(object sender, EventArgs e)
        {
            cmbBorrowSelectPerson.Text = "Select Person";
            cmbBorrowPaymentType.Text = "Select Payment Type";
            cmbBorrowStatus.Text = "Select Status";
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
            MessageBox.Show("Lent Details Saved");
        }


        private void pnlAddBorrowMainBody_Click(object sender, EventArgs e)
        {
            pnlBorrowAddCalenderShow.Visible = false;
        }
    }
}
