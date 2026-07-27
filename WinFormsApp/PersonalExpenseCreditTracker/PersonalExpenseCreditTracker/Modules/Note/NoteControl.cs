using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace PersonalExpenseCreditTracker.Modules.Note
{
    public partial class NoteControl : Form
    {
        [DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);
        public NoteControl()
        {
            InitializeComponent();
            Resize += NoteControl_Resize;
        }

        private void NoteControl_Load(object sender, EventArgs e)
        {
            btnNoteMore.ContextMenuStrip = cmsNote;
            
            foreach (Control c in flpNotes.Controls)
            {
                if (c is Panel)
                {
                    SetRadius(c, 20);
                }
            }
            ResizeNoteCards();
            SetRoundedPanel(pnlTotalNotes, 20);
            SetRoundedPanel(pnlImportant, 20);
            SetRoundedPanel(pnlThisMonth, 20);

        }


        private void lblNoteSubtitle_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void PicNoteMenu_Click(object sender, EventArgs e)
        {

        }

       
        private void pnlTotalNotes_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlTotalNotes.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void picNoteImportant_Click(object sender, EventArgs e)
        {

        }

        private void flpNotes_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            cmsNote.Show(btnNoteMore, 0, btnNoteMore.Height);

        }

        private void NoteControl_Resize(object sender, EventArgs e)
        {
            ResizeNoteCards();
            SetRoundedPanel(pnlTotalNotes, 15);
            SetRoundedPanel(pnlImportant, 15);
            SetRoundedPanel(pnlThisMonth, 15);
        }

        private void ResizeNoteCards()
        {
            int margin = 10;
            int availableWidth = flpNotes.ClientSize.Width
                               - flpNotes.Padding.Left
                               - flpNotes.Padding.Right;

            int columns;

            if (availableWidth < 500)
                columns = 1;
            else if (availableWidth < 850)
                columns = 2;
            else if (availableWidth < 1150)
                columns = 3;
            else
                columns = 4;

            int cardWidth = (availableWidth - (columns * margin * 2)) / columns;

            foreach (Control c in flpNotes.Controls)
            {
                if (c is Panel)
                {
                    c.Width = cardWidth;
                    c.Height = 120;
                    c.Margin = new Padding(margin);

                    SetRadius(c, 20);
                }
            }
            //foreach (Control c in flpNotes.Controls)
            //{
            //    if (c is Panel)
            //    {
            //        c.Width = cardWidth;
            //        c.Height = 120;
            //        c.Margin = new Padding(margin);

            //        Label lblDescription = c.Controls["lblNoteCardDescription"] as Label;

            //        if (lblDescription != null)
            //        {
            //            lblDescription.Width = c.Width - 30;      // 15 px padding on each side
            //            lblDescription.MaximumSize = new Size(c.Width - 30, 60);
            //        }

            //        SetRadius(c, 20);
            //    }
            //}
        }

        private void pnlNoteHeader_Paint(object sender, PaintEventArgs e)
        {

        }
        

        private void SetRoundedPanel(Panel panel, int radius)
         {
            GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(panel.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(panel.Width - radius, panel.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, panel.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                panel.Region = new Region(path);
           }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoteViewDetailsControl noteViewDetailsControl = new NoteViewDetailsControl();
            noteViewDetailsControl.Show();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoteEditDetailsControl noteEditDetailsControl = new NoteEditDetailsControl();
            noteEditDetailsControl.Show();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

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

        

        private void pnlNoteCard_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlImportant.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void pnlImportant_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlImportant.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void pnlThisMonth_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlImportant.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void lblNoteCardDescription_Click(object sender, EventArgs e)
        {

        }
    }
}
