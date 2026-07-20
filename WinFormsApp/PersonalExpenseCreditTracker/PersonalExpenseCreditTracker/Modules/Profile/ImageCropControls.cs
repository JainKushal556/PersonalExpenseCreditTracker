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

namespace PersonalExpenseCreditTracker.Modules.Profile
{
    public partial class ImageCropControls : Form
    {
        private ProfileControls profileControls;

        public ImageCropControls()
        {
            InitializeComponent();
        }

        public ImageCropControls(ProfileControls frm)
        {
            InitializeComponent();
            profileControls = frm;
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

        //Load Form
        private void ImageCropControls_Load(object sender, EventArgs e)
        {
            SetRadius(pnlImageCrop, 20);
            SetRadius(btnCrop, 10);
            SetRadius(btnCropImageCancel, 10);

            ImgBoxCrop.SelectionMode = Cyotek.Windows.Forms.ImageBoxSelectionMode.Rectangle;
            ImgBoxCrop.LimitSelectionToImage = true;
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
        //Show ImageBox and load Image on this Area
        protected internal void SetImageInImgBoxCrop()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ImgBoxCrop.Image = Image.FromFile(ofd.FileName);
                ImgBoxCrop.ZoomToFit();
            }
        }
        
        //Circle Image
        protected internal void MakePictureCircular(PictureBox pic)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, pic.Width - 1, pic.Height - 1);
            pic.Region = new Region(path);
        }

        //Crop Image
        private Bitmap CropToSquare(Image image)
        {
            int size = Math.Min(image.Width, image.Height);

            int x = (image.Width - size) / 2;
            int y = (image.Height - size) / 2;

            Bitmap bmp = new Bitmap(size, size);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(image,
                    new Rectangle(0, 0, size, size),
                    new Rectangle(x, y, size, size),
                    GraphicsUnit.Pixel);
            }

            return bmp;
        }

        //Change Crop Image on Profile Photo
        private void btnCrop_Click(object sender, EventArgs e)
        {
            RectangleF selection = ImgBoxCrop.SelectionRegion;

            if (selection.Width <= 0 || selection.Height <= 0)
            {
                MessageBox.Show("Please select an area first.");
                return;
            }

            Rectangle cropRect = Rectangle.Round(selection);

            Bitmap bmp = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(
                    ImgBoxCrop.Image,
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    cropRect,
                    GraphicsUnit.Pixel);
            }

            profileControls.picProfileUserPhoto.Image = bmp;
            ImgBoxCrop.SelectionRegion = RectangleF.Empty;
            this.Close();
        }

        private void btnCropImageCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlImageCrop_Resize(object sender, EventArgs e)
        {
            SetRadius(pnlImageCrop, 20);
        }

        private void btnCropImageCancel_Resize(object sender, EventArgs e)
        {
            SetRadius(btnCropImageCancel, 10);
        }

        private void btnCrop_Resize(object sender, EventArgs e)
        {
            SetRadius(btnCrop, 10);
        }
    }
}
