using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PersonalExpenseCreditTracker.Modules.Borrow
{
    public partial class AddBorrowControls : Form
    {
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

        private void monthCalendarAddBorrow_DateChanged(object sender, DateRangeEventArgs e)
        {
            txtBorrowAddDeadlineDatePicker.Text = e.Start.ToString("dd-MM-yyyy");
            txtBorrowAddDeadlineDatePicker.ForeColor = Color.Black;
            //pnlBorrowAddCalenderShow.Visible = false;
        }

        private void txtBorrowAddDeadlineDatePicker_TextChanged(object sender, EventArgs e)
        {
            pnlBorrowAddCalenderShow.Visible = false;
        }

        private void txtBorrowAddDeadlineDatePicker_Enter(object sender, EventArgs e)
        {
            if (txtBorrowAddDeadlineDatePicker.Text == "DD-MM-YYYY")
            {
                txtBorrowAddDeadlineDatePicker.Text = "";
                txtBorrowAddDeadlineDatePicker.ForeColor = Color.Black;
            }
            pnlBorrowAddCalenderShow.Visible = true;
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

            //pnlBorrowAddCalenderShow.Visible = false;
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
            if (cmbBorrowSelectPerson.SelectedIndex == -1)
            {
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
            if (cmbBorrowPaymentType.SelectedIndex == -1)
            {
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
            if (cmbBorrowStatus.Text == "Select Status")
                cmbBorrowStatus.ForeColor = Color.Black;

            pnlBorrowAddCalenderShow.Visible = false;
        }

        private void cmbBorrowStatus_Leave(object sender, EventArgs e)
        {
            if (cmbBorrowStatus.SelectedIndex == -1)
            {
                cmbBorrowStatus.Text = "Select Status";
                cmbBorrowStatus.ForeColor = Color.Gray;
            }
            else
            {
                cmbBorrowStatus.ForeColor = Color.Black;
            }
        }

        private void AddBorrowControls_Load(object sender, EventArgs e)
        {

            pnlBorrowAddCalenderShow.Visible = false;
            cmbBorrowSelectPerson.Text = "Select Person";
            cmbBorrowSelectPerson.ForeColor = Color.Gray;

            cmbBorrowPaymentType.Text = "Select Payment Type";
            cmbBorrowPaymentType.ForeColor = Color.Gray;

            cmbBorrowStatus.Text = "Select Status";
            cmbBorrowStatus.ForeColor = Color.Gray;

            txtBorrowAddAmount.Text = "Select Amount";
            txtBorrowAddAmount.ForeColor = Color.Gray;

            txtBorrowAddDescription.Text = "Enter description";
            txtBorrowAddDescription.ForeColor = Color.Gray;

            txtBorrowAddDeadlineDatePicker.Text = "DD-MM-YYYY";
            txtBorrowAddDeadlineDatePicker.ForeColor = Color.Gray;
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
            if (txtBorrowAddDeadlineDatePicker.Text == "DD-MM-YYYY" ||
                txtBorrowAddDescription.Text == "Enter description" ||
                txtBorrowAddAmount.Text == "Select Amount" ||
                cmbBorrowStatus.Text == "Select Status" ||
                cmbBorrowPaymentType.Text == "Select Payment Type" ||
                cmbBorrowSelectPerson.Text == "Select Person")
                MessageBox.Show("Please fill all fields");
            else
            MessageBox.Show("Borrow Details Saved");
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
    }
}
