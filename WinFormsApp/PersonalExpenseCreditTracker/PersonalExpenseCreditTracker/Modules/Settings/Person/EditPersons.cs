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
using BLLayer.Common;
using BLLayer.Settings.Persons;
using PersonalExpenseCreditTracker.Common;
using PersonalExpenseCreditTracker.Session;
namespace PersonalExpenseCreditTracker.Modules.Settings.Person
{
    public partial class EditPersons : Form
    {
        private AddPersonControls addPersonSControls;

        public int personID;
        public string PersonName;
        public string PhoneNumber;
        public string Address;

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
            //SetRadius(btnUpdatePersonDetails, 15);
            //SetRadius(btnCancelEditPersonDetails, 15);
            txtEditPersonDetailsFullName.Text = PersonName;
            txtEditPersonDetailsPhoneNumber.Text = PhoneNumber;
            txtEditPersonDetailsAddress.Text = Address;
        }

        //private void btnUpdatePersonDetails_Resize(object sender, System.EventArgs e)
        //{
        //    SetRadius(btnUpdatePersonDetails, 15);
        //}
        //private void btnCancelEditPersonDetails_Resize(object sender, System.EventArgs e)
        //{
        //    SetRadius(btnCancelEditPersonDetails, 15);
        //}

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
            
            errorProvider1.Clear();
          
            string personName = (txtEditPersonDetailsFullName.Text == "Enter Full Name") ? "" : txtEditPersonDetailsFullName.Text.Trim();
            string personNumber = (txtEditPersonDetailsPhoneNumber.Text == "Enter Phone Number") ? "" : txtEditPersonDetailsPhoneNumber.Text.Trim();
            string address = (txtEditPersonDetailsAddress.Text == "Enter Address") ? "" : txtEditPersonDetailsAddress.Text.Trim();
 
            CommonValidator.ValidationResult valResult = CommonValidator.ValidationPersonName(personName);
            if (valResult != CommonValidator.ValidationResult.Success)
            {
                ErrorHelper.ShowValidationError(valResult, errorProvider1, txtEditPersonDetailsFullName);
                return; 
            }

          
            valResult = CommonValidator.ValidatePhoneNumber(personNumber);
            if (valResult != CommonValidator.ValidationResult.Success)
            {
                ErrorHelper.ShowValidationError(valResult, errorProvider1, txtEditPersonDetailsPhoneNumber);
                return;
            }

          
            DialogResult confirmResult = MessageBox.Show(
                "Are you sure you want to update this person details?",
                "Confirm Update",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close(); 
                return;
            }

            
            PersonUI personUI = new PersonUI();

            personUI.userId = Session.LogedInUser.GetUserId();
            personUI.personId = personID;
            personUI.personName = personName;
            personUI.personNumber = personNumber;
            personUI.address = address;

            CommonValidator.ValidationResult result = personUI.UpdateDataIntoPersonUi();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    //MessageBox.Show("Person Details Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (addPersonSControls != null)
                    {
                        addPersonSControls.LoadData();
                    }
                    this.Close();
                    break;

                case CommonValidator.ValidationResult.PersonInvalid:
                case CommonValidator.ValidationResult.PersonNameEmpty:
                case CommonValidator.ValidationResult.PersonNameInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtEditPersonDetailsFullName);
                    break;

                case CommonValidator.ValidationResult.PhoneNumberEmpty:
                case CommonValidator.ValidationResult.PhoneInvalid:
                case CommonValidator.ValidationResult.PhoneNumberAlreadyExists:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtEditPersonDetailsPhoneNumber);
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    MessageBox.Show("Person Not Updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    break;
            }
        }




        private void pnlEditPersonDetailsMainBody_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtEditPersonDetailsFullName_TextChanged(object sender, EventArgs e)
        {
            if (txtEditPersonDetailsFullName.Text != "Enter Full Name" && !string.IsNullOrWhiteSpace(txtEditPersonDetailsFullName.Text))
            {
                ErrorHelper.HideErrorForControl(txtEditPersonDetailsFullName);
            }
        }

        private void txtEditPersonDetailsPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtEditPersonDetailsPhoneNumber.Text != "Enter Phone Number" && !string.IsNullOrWhiteSpace(txtEditPersonDetailsPhoneNumber.Text))
            {
                ErrorHelper.HideErrorForControl(txtEditPersonDetailsPhoneNumber);
            }
        }

        private void txtEditPersonDetailsAddress_TextChanged(object sender, EventArgs e)
        {
            if (txtEditPersonDetailsAddress.Text != "Enter Address" && !string.IsNullOrWhiteSpace(txtEditPersonDetailsAddress.Text))
            {
                ErrorHelper.HideErrorForControl(txtEditPersonDetailsAddress);
            }
        }
    }
}
