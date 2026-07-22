using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using System.Configuration;
using PersonalExpenseCreditTracker.Modules.Settings.Person;

namespace PersonalExpenseCreditTracker.Modules.Settings.Person
{
    public partial class EditPersons : Form
    {
        private AddPersonControls addPersonSControls;
        public EditPersons()
        {
            InitializeComponent();
        }

        public EditPersons(AddPersonControls addPerson)
        {
            InitializeComponent();
            addPersonSControls = addPerson;
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

        // All Border Cornar Radius
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

        private void EditPersons_Load(object sender, EventArgs e)
        {
            SetRadius(btnUpdatePersonDetails, 15);
            SetRadius(btnCancelEditPersonDetails, 15);
            txtEditPersonDetailsFullName.Text = addPersonSControls.txtAddPersonInputFullName.Text;
            txtEditPersonDetailsPhoneNumber.Text = addPersonSControls.txtAddPersonInputPhoneNumber.Text;
            txtEditPersonDetailsAddress.Text = addPersonSControls.txtAddPersonInputAddress.Text;
        }

        private void btnUpdatePersonDetails_Resize(object sender, System.EventArgs e)
        {
            SetRadius(btnUpdatePersonDetails, 15);
        }
        private void btnCancelEditPersonDetails_Resize(object sender, System.EventArgs e)
        {
            SetRadius(btnCancelEditPersonDetails, 15);
        }

        private void btnCloseEditPersonDetails_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        private void btnCancelEditPersonDetails_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void pnlEditPersonDetailsFullName_Leave(object sender, EventArgs e)
        {
            pnlEditPersonDetailsFullName.BorderStyle = BorderStyle.None;
        }

        private void txtEditPersonDetailsFullName_Click(object sender, EventArgs e)
        {
            pnlEditPersonDetailsFullName.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pnlEditPersonDetailsPhoneNumber_Leave(object sender, EventArgs e)
        {
            pnlEditPersonDetailsPhoneNumber.BorderStyle = BorderStyle.None;
        }

        private void txtEditPersonDetailsPhoneNumber_Click(object sender, EventArgs e)
        {
            pnlEditPersonDetailsPhoneNumber.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pnlEditPersonDetailsAddress_Leave(object sender, EventArgs e)
        {
            pnlEditPersonDetailsAddress.BorderStyle = BorderStyle.None;
        }

        private void txtEditPersonDetailsAddress_Click(object sender, EventArgs e)
        {
            pnlEditPersonDetailsAddress.BorderStyle = BorderStyle.FixedSingle;
        }

        private void btnUpdatePersonDetails_Click(object sender, EventArgs e)
        {
            addPersonSControls.txtAddPersonInputFullName.Text = txtEditPersonDetailsFullName.Text;
            addPersonSControls.txtAddPersonInputPhoneNumber.Text = txtEditPersonDetailsPhoneNumber.Text;
            addPersonSControls.txtAddPersonInputAddress.Text = txtEditPersonDetailsAddress.Text;

            MessageBox.Show("Person Details Updated Successfully");
            this.Close();
        }

        private void btnCloseEditPersonDetails_MouseHover(object sender, EventArgs e)
        {
            btnCloseEditPersonDetails.BackColor = Color.FromArgb(255, 0, 0);
        }

        private void btnCloseEditPersonDetails_MouseLeave(object sender, EventArgs e)
        {
            btnCloseEditPersonDetails.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void btnCloseEditPersonDetails_MouseEnter(object sender, EventArgs e)
        {
            btnCloseEditPersonDetails.BackColor = Color.FromArgb(255, 0, 0);
        }
    }
}
