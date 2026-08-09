
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Drawing.Drawing2D;
using PersonalExpenseCreditTracker.Modules.Note;
using System.Runtime.InteropServices;

namespace PersonalExpenseCreditTracker.Modules.Note
{

    public partial class NoteViewDetailsControl : Form
    {
        private NoteControl noteControl;

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

        public NoteViewDetailsControl()
        {
            InitializeComponent();
        }
        public NoteViewDetailsControl(NoteControl noteControl)
        {
            InitializeComponent();
            this.noteControl = noteControl;
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NoteViewDetailsControl_Load(object sender, EventArgs e)
        {
            SetRadius(pnlBody, 15);
            SetRadius(btnClose, 5);

            if (noteControl != null)
            {
                lblNoteTitle.Text = noteControl.SelectedNoteTitle;
                lblDescription.Text = noteControl.SelectedDescription;
                lblPriority.Text = noteControl.SelectedPriority;
                lblColorName.Text = noteControl.SelectedColorName;
                lblCreatedDate.Text = noteControl.SelectedCreatedAt;

                // Priority Color
                if (noteControl.SelectedPriority == "Low")
                {
                    lblPriority.ForeColor = Color.Green;
                    pnlColor.BackColor = Color.Green;
                }
                else if (noteControl.SelectedPriority == "Medium")
                {
                    lblPriority.ForeColor = Color.Orange;
                    pnlColor.BackColor = Color.Orange;
                }
                else if (noteControl.SelectedPriority == "High")
                {
                    lblPriority.ForeColor = Color.Red;
                    pnlColor.BackColor = Color.Red;
                }

                // Note Color Preview
                // Note Color Preview
                if (!string.IsNullOrWhiteSpace(noteControl.SelectedColorHexCode))
                {
                    Color selectedColor =
                        ColorTranslator.FromHtml(noteControl.SelectedColorHexCode);

                    pnlColorPreview.BackColor = selectedColor;

                    if (selectedColor.ToArgb() == Color.White.ToArgb())
                    {
                        lblColorName.ForeColor = Color.Black;
                    }
                    else
                    {
                        lblColorName.ForeColor = selectedColor;
                    }
                }
            }
        }
        //private void CenterPanel()
        //{
        //    pnlViewNoteDetails.Left = (this.ClientSize.Width - pnlViewNoteDetails.Width) / 2;
        //    pnlViewNoteDetails.Top = (this.ClientSize.Height - pnlViewNoteDetails.Height) / 2;
        //}

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

        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            btnCancel.BackColor = Color.Red;
        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            btnCancel.BackColor = Color.Transparent;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

