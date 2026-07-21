using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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
            GetUserProfileDetails();

            SetRadius(pnlEditProfileMainBody, 20);
            SetRadius(btnCancelEditProfile, 20);
            SetRadius(btnUpdateProfile, 25);

            pnlEditProfileMainBody.Resize += pnlEditProfileMainBody_Resize;
            btnCancelEditProfile.Resize += btnCancelEditProfile_Resize;
            btnUpdateProfile.Resize += btnUpdateProfile_Resize;

            this.ActiveControl = pnlEditProfileMainBody;
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
        private void pnlEditProfileAddress_Leave(object sender, EventArgs e)
        {
            pnlEditProfileAddress.BorderStyle = BorderStyle.None;
        }
        private void txtEditProfileAddress_Click(object sender, EventArgs e)
        {
            pnlEditProfileAddress.BorderStyle = BorderStyle.FixedSingle;
        }
        //

        private void btnCancelEditProfile_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            SetUserProfileDetails();
            MessageBox.Show("Profile Updated Successfully");
            this.Close();
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
            txtEditProfilePhoneNumber.Text = profileControls.lblProfileInfoPersonPhoneNumber.Text;
            cmbEditProfileGender.Text = profileControls.lblProfileInfoPersonGender.Text;
            txtEditProfileAddress.Text = profileControls.lblProfileInfoPersonAddress.Text;
        }
        private void SetUserProfileDetails()
        {
            profileControls.lblProfileInfoPersonFullName.Text = txtEditProfileFullName.Text;
            profileControls.lblProfileInfoPersonDathOfBirth.Text = txtEditProfileDathOfBirth.Text;
            profileControls.lblProfileInfoPersonEmail.Text = txtEditProfileEmailAddress.Text;
            profileControls.lblProfileInfoPersonPhoneNumber.Text = txtEditProfilePhoneNumber.Text;
            profileControls.lblProfileInfoPersonGender.Text = cmbEditProfileGender.Text;
            profileControls.lblProfileInfoPersonAddress.Text = txtEditProfileAddress.Text;

            profileControls.lblProfileEmailvalue.Text = txtEditProfileEmailAddress.Text;
            profileControls.lblProfilePhoneValue.Text = txtEditProfilePhoneNumber.Text;
            profileControls.RichTextBoxUserProfileName.Text = txtEditProfileFullName.Text;

            profileControls.RichTextBoxUserProfileName.SelectAll();
            profileControls.RichTextBoxUserProfileName.SelectionAlignment = HorizontalAlignment.Center;
            profileControls.RichTextBoxUserProfileName.DeselectAll();
        }
    }
}
