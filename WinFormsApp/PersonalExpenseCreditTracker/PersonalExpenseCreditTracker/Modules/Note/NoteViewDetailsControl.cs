
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace PersonalExpenseCreditTracker.Modules.Note
{

    public partial class NoteViewDetailsControl : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        public NoteViewDetailsControl()
        {
            InitializeComponent();
            this.Resize += NoteViewDetailsControl_Resize;
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NoteViewDetailsControl_Load(object sender, EventArgs e)
        {
            CenterPanel();
            ApplyBorderRadius();
        }
        private void CenterPanel()
        {
            pnlViewNoteDetails.Left = (this.ClientSize.Width - pnlViewNoteDetails.Width) / 2;
            pnlViewNoteDetails.Top = (this.ClientSize.Height - pnlViewNoteDetails.Height) / 2;
        }


        private void NoteViewDetailsControl_Resize(object sender, EventArgs e)
        {
            CenterPanel();

            
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void lblUpdatedCaption_Click(object sender, EventArgs e)
        {

        }
        private void ApplyBorderRadius()
        {
            pnlColor.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    pnlColor.Width,
                    pnlColor.Height,
                    pnlColor.Width,
                    pnlColor.Height
                ));
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

