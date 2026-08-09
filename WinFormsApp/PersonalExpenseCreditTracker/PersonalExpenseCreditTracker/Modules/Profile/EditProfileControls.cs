using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using BLLayer.Common;
using PersonalExpenseCreditTracker.Common;
namespace PersonalExpenseCreditTracker.Modules.Profile
{
    public partial class EditProfileControls : Form
    {
        private ProfileControls profileControls;

        public EditProfileControls()
        {
            InitializeComponent();
        }

        public EditProfileControls(ProfileControls PC)
        {
            InitializeComponent();
            profileControls = PC;
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

        private void EditProfileControls_Load(object sender, EventArgs e)
        {
           
            CommonUiFunction.LoadInComboBox("spGetGender", "Select Gender", cmbEditProfileGender);
            GetUserProfileDetails();
            monthCalendarProfile.MaxDate = DateTime.Today; 
            SetRadius(pnlEditProfileMainBody, 20);
            SetRadius(btnCancelEditProfile, 20);
            SetRadius(btnUpdateProfile, 25);

            pnlEditProfileMainBody.Resize += pnlEditProfileMainBody_Resize;
            btnCancelEditProfile.Resize += btnCancelEditProfile_Resize;
            btnUpdateProfile.Resize += btnUpdateProfile_Resize;

            this.ActiveControl = pnlEditProfileMainBody;

            panelProfileCalenderShow.Visible = false;
           
        }

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
        private void pnlEditProfileMainBody_Resize(object sender, EventArgs e)
        {
            SetRadius(pnlEditProfileMainBody, 20);
        }
        private void btnCancelEditProfile_Resize(object sender, EventArgs e)
        {
            SetRadius(btnCancelEditProfile, 20);
        }
        private void btnUpdateProfile_Resize(object sender, EventArgs e)
        {
            SetRadius(btnUpdateProfile, 25);
        }
        //

        // All Panle Border Show
        private void pnlEditProfileFullName_Leave(object sender, EventArgs e)
        {
            pnlEditProfileFullName.BorderStyle = BorderStyle.None;
        }
        private void txtEditProfileFullName_Click(object sender, EventArgs e)
        {
            pnlEditProfileFullName.BorderStyle = BorderStyle.FixedSingle;
        }
        private void pnlEditProfileDathOfBirth_Leave(object sender, EventArgs e)
        {
            pnlEditProfileDathOfBirth.BorderStyle = BorderStyle.None;
        }
        private void txtEditProfileDathOfBirth_Click(object sender, EventArgs e)
        {
            pnlEditProfileDathOfBirth.BorderStyle = BorderStyle.FixedSingle;
            ShowCalenderToDatePanel(panelProfileCalenderShow);
        }
        private void pnlEditProfileEmailAddress_Leave(object sender, EventArgs e)
        {
            pnlEditProfileEmailAddress.BorderStyle = BorderStyle.None;
        }
        private void txtEditProfileEmailAddress_Click(object sender, EventArgs e)
        {
            pnlEditProfileEmailAddress.BorderStyle = BorderStyle.FixedSingle;
        }
        private void pnlEditProfilePhoneNumber_Leave(object sender, EventArgs e)
        {
            pnlEditProfilePhoneNumber.BorderStyle = BorderStyle.None;
        }
        private void txtEditProfilePhoneNumber_Click(object sender, EventArgs e)
        {
            pnlEditProfilePhoneNumber.BorderStyle = BorderStyle.FixedSingle;
        }
        private void cmbEditProfileGender_Leave(object sender, EventArgs e)
        {
            pnlEditProfileGender.BorderStyle = BorderStyle.None;
        }
        private void cmbEditProfileGender_Enter(object sender, EventArgs e)
        {
            pnlEditProfileGender.BorderStyle = BorderStyle.FixedSingle;
        }
       
        private void txtEditProfileAddress_Click(object sender, EventArgs e)
        {
            txtEditProfileAddress.BorderStyle = BorderStyle.FixedSingle;
        }

        private void btnCancelEditProfile_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnUpdateProfile_Click(object sender, EventArgs e)
        {
           

            ProfileUI profileUi = new ProfileUI();

            profileUi.userId = Session.LogedInUser.GetUserId();
            profileUi.genderId = Convert.ToInt32(cmbEditProfileGender.SelectedValue);
            profileUi.fullName = txtEditProfileFullName.Text.Trim();
            profileUi.email = txtEditProfileEmailAddress.Text.Trim();
            profileUi.phoneNumber = txtEditProfilePhoneNumber.Text.Trim();
            profileUi.address = txtEditProfileAddress.Text.Trim();
            DateTime dob = DateTime.MinValue;
            if (!string.IsNullOrWhiteSpace(txtEditProfileDathOfBirth.Text))
            {
                string[] formats = new string[] { "dd-MM-yyyy", "d-M-yyyy", "yyyy-MM-dd", "dd/MM/yyyy", "MM/dd/yyyy", "dd MMM yyyy", "d MMM yyyy", "dd MMMM yyyy" };
                if (!DateTime.TryParseExact(txtEditProfileDathOfBirth.Text.Trim(), formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dob))
                {
                    DateTime.TryParse(txtEditProfileDathOfBirth.Text.Trim(), out dob);
                }
            }

            profileUi.dateOfBirth = dob;

            CommonValidator.ValidationResult result = profileUi.UpdateUserProfileIntoProfUi();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show("Profile updated successfully!");
                    if (profileControls != null)
                    {
                        profileControls.LoadUserProfileData();
                    }

                    MainForm mainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
                    if (mainForm != null)
                    {
                        mainForm.LoadSidebarUserProfile();
                    }

                    this.Close();
                    break;

                case CommonValidator.ValidationResult.FullNameInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtEditProfileFullName);
                    break;

                case CommonValidator.ValidationResult.EmailInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtEditProfileEmailAddress);
                    break;

                case CommonValidator.ValidationResult.PhoneInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtEditProfilePhoneNumber);
                    break;

                case CommonValidator.ValidationResult.DateOfBirthInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtEditProfileDathOfBirth);
                    break;

                case CommonValidator.ValidationResult.GenderInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, cmbEditProfileGender);
                    break;

                case CommonValidator.ValidationResult.AddressInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtEditProfileAddress);
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    MessageBox.Show("Profile updated unsuccessfully!");
                    break;
            }
        }
        private void btnCloseEditProfile_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetUserProfileDetails()
        {
            txtEditProfileFullName.Text = profileControls.lblProfileInfoPersonFullName.Text;
            txtEditProfileDathOfBirth.Text = profileControls.lblProfileInfoPersonDathOfBirth.Text;
            txtEditProfileEmailAddress.Text = profileControls.lblProfileInfoPersonEmail.Text;

            string phone = profileControls.lblProfileInfoPersonPhoneNumber.Text;
            if (!string.IsNullOrWhiteSpace(phone))
            {
                phone = phone.Replace("+91", "").Trim();
            }
            txtEditProfilePhoneNumber.Text = phone;

            cmbEditProfileGender.Text = profileControls.lblProfileInfoPersonGender.Text;
            txtEditProfileAddress.Text = profileControls.lblProfileInfoPersonAddress.Text;
        }

        private void ShowCalenderToDatePanel(Panel panel)
        {
            panel.Parent = pnlEditProfileMainBody;
            panel.BackColor = Color.White;

            Point screenPoint = pnlEditProfileDathOfBirth.PointToScreen(Point.Empty);
            Point mainBodyPoint = pnlEditProfileMainBody.PointToClient(screenPoint);

            panel.Location = new Point(
                mainBodyPoint.X + pnlEditProfileDathOfBirth.Width - panel.Width,
                mainBodyPoint.Y + pnlEditProfileDathOfBirth.Height + 2);

            panel.BringToFront();
            panel.Visible = true;
        }


        private void btnProfileCalendar_Click(object sender, EventArgs e)
        {
            if (panelProfileCalenderShow.Visible)
            {
                panelProfileCalenderShow.Visible = false;
            }
            else
            {
                ShowCalenderToDatePanel(panelProfileCalenderShow);
            }
        }

        private void monthCalendarProfile_DateSelected(object sender, DateRangeEventArgs e)
        {
           
            if (e.Start.Date > DateTime.Today)
            {
                return;
            }

            txtEditProfileDathOfBirth.Text = e.Start.ToString("dd-MM-yyyy");
            txtEditProfileDathOfBirth.ForeColor = Color.Black;
            ErrorHelper.HideErrorForControl(txtEditProfileDathOfBirth);
            panelProfileCalenderShow.Visible = false;
        }

        private void txtEditProfileDathOfBirth_Enter(object sender, EventArgs e)
        {
            pnlEditProfileDathOfBirth.BorderStyle = BorderStyle.FixedSingle;
        }
        private void txtEditProfileDathOfBirth_Leave(object sender, EventArgs e)
        {
            pnlEditProfileDathOfBirth.BorderStyle = BorderStyle.None;
        }

        private void txtEditProfileDathOfBirth_TextChanged(object sender, EventArgs e)
        {
            ErrorHelper.HideErrorForControl(txtEditProfileDathOfBirth);
            panelProfileCalenderShow.Visible = false;
        }


        private void txtEditProfileFullName_Enter(object sender, EventArgs e)
        {
            pnlEditProfileFullName.BorderStyle = BorderStyle.FixedSingle;
        }

        private void txtEditProfileFullName_Leave(object sender, EventArgs e)
        {
            pnlEditProfileFullName.BorderStyle = BorderStyle.None;
        }

        private void txtEditProfileFullName_TextChanged(object sender, EventArgs e)
        {
            ErrorHelper.HideErrorForControl(txtEditProfileFullName);
        }

        private void txtEditProfileEmailAddress_Enter(object sender, EventArgs e)
        {
            pnlEditProfileEmailAddress.BorderStyle = BorderStyle.FixedSingle;
        }

        private void txtEditProfileEmailAddress_Leave(object sender, EventArgs e)
        {
            pnlEditProfileEmailAddress.BorderStyle = BorderStyle.None;
        }

        private void txtEditProfileEmailAddress_TextChanged(object sender, EventArgs e)
        {
            ErrorHelper.HideErrorForControl(txtEditProfileEmailAddress);
        }

        private void txtEditProfileEmailAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

        private void txtEditProfilePhoneNumber_Enter(object sender, EventArgs e)
        {
            pnlEditProfilePhoneNumber.BorderStyle = BorderStyle.FixedSingle;
        }

        private void txtEditProfilePhoneNumber_Leave(object sender, EventArgs e)
        {
            pnlEditProfilePhoneNumber.BorderStyle = BorderStyle.None;
        }

        private void txtEditProfilePhoneNumber_TextChanged(object sender, EventArgs e)
        {
            ErrorHelper.HideErrorForControl(txtEditProfilePhoneNumber);
        }

        private void cmbEditProfileGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            ErrorHelper.HideErrorForControl(cmbEditProfileGender);
        }

        private void txtEditProfileAddress_Enter(object sender, EventArgs e)
        {
            txtEditProfileAddress.BorderStyle = BorderStyle.FixedSingle;
        }

        private void txtEditProfileAddress_Leave(object sender, EventArgs e)
        {
            txtEditProfileAddress.BorderStyle = BorderStyle.None;
        }

        private void txtEditProfileAddress_TextChanged(object sender, EventArgs e)
        {
            ErrorHelper.HideErrorForControl(txtEditProfileAddress);
        }

       
       
    }
}
