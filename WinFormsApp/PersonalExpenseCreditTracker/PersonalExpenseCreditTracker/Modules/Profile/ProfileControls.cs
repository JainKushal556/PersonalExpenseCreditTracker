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
            crop.SetImageInImgBoxCrop();
            crop.ShowDialog();

            ProfileUI profileUi = new ProfileUI();
            profileUi.userId = 1;
            MemoryStream ms = new MemoryStream();
            picProfileUserPhoto.Image.Save(ms, ImageFormat.Jpeg);
            profileUi.photoData =ms.ToArray();

            bool result = profileUi.UpdateProfilePhotoIntoProfUi();
            if (result)
            {
                MessageBox.Show("Validation Success");
            }
            else
            {
                MessageBox.Show("Validation Failed");
            }
        }
        private void btnProfileEditButton_Click(object sender, EventArgs e)
        {
            EditProfileControls edit = new EditProfileControls(this);
            edit.ShowDialog();
            
        }
    }
}