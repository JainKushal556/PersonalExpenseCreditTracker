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
using System.IO;
using System.Drawing.Imaging;
using PersonalExpenseCreditTracker.Common;
using PersonalExpenseCreditTracker.Session;
using BLLayer.Common;

namespace PersonalExpenseCreditTracker.Modules.Profile
{
    public partial class ProfileControls : Form
    {
        //private EditProfileControls editProfileControls;

        public ProfileControls()
        {
            InitializeComponent();
        }

        ImageCropControls imageCropControls = new ImageCropControls();

        // Import CreateRoundRectRgn
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

        //Load From
        private void ProfileControls_Load(object sender, EventArgs e)
        {
            RichTextBoxUserProfileName.SelectAll();
            RichTextBoxUserProfileName.SelectionAlignment = HorizontalAlignment.Center;
            RichTextBoxUserProfileName.DeselectAll();

            // Apply rounded corners
            SetRadius(pnlProfilePersonalInfo, 20);
            SetRadius(btnProfileEditButton, 10);
            SetRadius(pnlProfileStatus, 10);

            // Update radius whenever the panel is resized
            pnlProfilePersonalInfo.Resize += panelPersonalInfo_Resize;
            btnProfileEditButton.Resize += btnProfileEditButton_Resize;
            pnlProfileStatus.Resize += pnlProfileStatus_Resize;

            imageCropControls.MakePictureCircular(picProfileUserPhoto);
            LoadUserProfileData();

            
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

        private void panelPersonalInfo_Resize(object sender, EventArgs e)
        {
            SetRadius(pnlProfilePersonalInfo, 20);
        }
        private void btnProfileEditButton_Resize(object sender, EventArgs e)
        {
            SetRadius(btnProfileEditButton, 10);
        }
        private void pnlProfileStatus_Resize(object sender, EventArgs e)
        {
            SetRadius(pnlProfileStatus, 10);
        }
        private void btnProfileEditButton_MouseEnter(object sender, EventArgs e)
        {
            btnProfileEditButton.BackColor = Color.FromArgb(122, 197, 255);
        }
        private void btnProfileEditButton_MouseLeave(object sender, EventArgs e)
        {
            btnProfileEditButton.BackColor = Color.FromArgb(255, 255, 255);
        }
        //

        private void picProfileUserPhoto_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, picProfileUserPhoto.Width - 1, picProfileUserPhoto.Height - 1);

            e.Graphics.SetClip(path);
            e.Graphics.DrawImage(
                picProfileUserPhoto.Image,
                0,
                0,
                picProfileUserPhoto.Width,
                picProfileUserPhoto.Height);

            e.Graphics.ResetClip();

            // Optional border
            e.Graphics.DrawEllipse(
                new Pen(Color.White, 2),
                0,
                0,
                picProfileUserPhoto.Width - 1,
                picProfileUserPhoto.Height - 1);
        }

        private void picProfileImageEditButton_Click(object sender, EventArgs e)
        {
            ImageCropControls crop = new ImageCropControls(this);

            if (crop.SetImageInImgBoxCrop())
            {
                if (crop.ShowDialog() == DialogResult.OK)
                {
                    if (picProfileUserPhoto.Image != null)
                    {
                        ProfileUI profileUi = new ProfileUI();
                        profileUi.userId = Session.LogedInUser.GetUserId();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            picProfileUserPhoto.Image.Save(ms, ImageFormat.Jpeg);
                            profileUi.photoData = ms.ToArray();
                        }

                        CommonValidator.ValidationResult result = profileUi.UpdateProfilePhotoIntoProfUi();

                        switch (result)
                        {
                            case CommonValidator.ValidationResult.Success:
                                MessageBox.Show("Profile photo updated successfully.");
                                LoadUserProfileData();

                                MainForm mainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
                                if (mainForm != null)
                                {
                                    mainForm.LoadSidebarUserProfile(); 
                                }
                                break;

                            case CommonValidator.ValidationResult.PhotoInvalid:
                                MessageBox.Show("Invalid profile photo data.");
                                break;

                            case CommonValidator.ValidationResult.StoreProcedureError:
                                MessageBox.Show("Failed to update profile photo into database.");
                                break;
                        }
                    }
                }
            }
        }



        private void btnProfileEditButton_Click(object sender, EventArgs e)
        {
            EditProfileControls edit = new EditProfileControls(this);
            edit.ShowDialog();
            
        }

        public void LoadUserProfileData()
        {
            int userID = Session.LogedInUser.GetUserId();

            DataTable dt = CommonUiFunction.RetrieveDataForGridView("spGetActiveUserDetails", userID);

            if (dt != null && dt.Rows.Count > 0 && !dt.Columns.Contains("Message"))
            {
                DataRow row = dt.Rows[0];

                // Full Name
                if (dt.Columns.Contains("FullName"))
                {
                    lblProfileInfoPersonFullName.Text = Convert.ToString(row["FullName"]);
                    RichTextBoxUserProfileName.Text = Convert.ToString(row["FullName"]);
                    RichTextBoxUserProfileName.SelectAll();
                    RichTextBoxUserProfileName.SelectionAlignment = HorizontalAlignment.Center;
                    RichTextBoxUserProfileName.DeselectAll();

                }

                // Email
                if (dt.Columns.Contains("Email"))
                {
                    lblProfileInfoPersonEmail.Text = Convert.ToString(row["Email"]);

                    if (lblProfileEmailvalue != null)
                        lblProfileEmailvalue.Text = Convert.ToString(row["Email"]);
                }

                // Phone Number
                if (dt.Columns.Contains("PhoneNumber"))
                {
                    string phoneNumber = Convert.ToString(row["PhoneNumber"]);

                    if (!string.IsNullOrWhiteSpace(phoneNumber))
                    {
                        if (!phoneNumber.StartsWith("+91"))
                            phoneNumber = "+91 " + phoneNumber;

                        lblProfileInfoPersonPhoneNumber.Text = phoneNumber;

                        if (lblProfilePhoneValue != null)
                            lblProfilePhoneValue.Text = phoneNumber;
                    }
                    else
                    {
                        lblProfileInfoPersonPhoneNumber.Text = "";

                        if (lblProfilePhoneValue != null)
                            lblProfilePhoneValue.Text = "";
                    }
                }

                // Date Of Birth
                if (dt.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
                {
                    lblProfileInfoPersonDathOfBirth.Text =
                        Convert.ToDateTime(row["DOB"]).ToString("dd-MM-yyyy");
                }
                else
                {
                    lblProfileInfoPersonDathOfBirth.Text = "";
                }

                // Gender
                if (dt.Columns.Contains("Gender") && row["Gender"] != DBNull.Value)
                {
                    lblProfileInfoPersonGender.Text = Convert.ToString(row["Gender"]);
                }
                else
                {
                    lblProfileInfoPersonGender.Text = "";
                }

                // Address
                if (dt.Columns.Contains("Address") && row["Address"] != DBNull.Value)
                {
                    lblProfileInfoPersonAddress.Text = Convert.ToString(row["Address"]);
                }
                else
                {
                    lblProfileInfoPersonAddress.Text = "";
                }

                // Member Since
                if (dt.Columns.Contains("MemberSince") && row["MemberSince"] != DBNull.Value)
                {
                    lblProfileUserSinceValue.Text =
                        Convert.ToDateTime(row["MemberSince"]).ToString("dd MMM yyyy");
                }
                else
                {
                    lblProfileUserSinceValue.Text = "";
                }

                // Account Status
                if (dt.Columns.Contains("AccountStatus") && row["AccountStatus"] != DBNull.Value)
                {
                    bool isActive = Convert.ToBoolean(row["AccountStatus"]);

                    lblProfileAccountStatusValue.Text = isActive ? "Active" : "Inactive";
                }
                else
                {
                    lblProfileAccountStatusValue.Text = "";
                }

                // Profile Photo
                // Profile Photo
                if (dt.Columns.Contains("ProfilePhoto") && row["ProfilePhoto"] != DBNull.Value)
                {
                    byte[] imgBytes = (byte[])row["ProfilePhoto"];

                    // ১. বাইট খালি বা নাল কিনা চেক
                    if (imgBytes != null && imgBytes.Length > 0)
                    {
                        try
                        {
                            using (MemoryStream ms = new MemoryStream(imgBytes))
                            {
                                using (Image tempImg = Image.FromStream(ms))
                                {
                                    // ২. সেফলি Bitmap তৈরি (যাতে স্ট্রিম লিক বা ক্র্যাশ না করে)
                                    picProfileUserPhoto.Image = new Bitmap(tempImg);
                                }
                            }
                        }
                        catch
                        {
                            // ছবি করাপ্ট থাকলে ডিফল্ট ছবি লোড হবে
                            picProfileUserPhoto.Image = Properties.Resources.people__3_1;
                        }
                    }
                    else
                    {
                        picProfileUserPhoto.Image = Properties.Resources.people__3_1;
                    }
                }
                else
                {
                    picProfileUserPhoto.Image = Properties.Resources.people__3_1;
                }

            }
            else
            {
                picProfileUserPhoto.Image = Properties.Resources.people__3_1;
            }
        }

      

    }
}