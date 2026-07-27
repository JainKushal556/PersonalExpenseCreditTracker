using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalExpenseCreditTracker.Modules.Note
{
    public partial class NoteAddDetailsControl : Form
    {
        public NoteAddDetailsControl()
        {
            InitializeComponent();
            this.Resize += NoteAddDetailsControl_Resize;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NoteAddDetailsControl_Load(object sender, EventArgs e)
        {
            CenterPanel();
        }
        private void CenterPanel()
        {
            pnlAddNoteDetails.Left = (this.ClientSize.Width - pnlAddNoteDetails.Width) / 2;
            pnlAddNoteDetails.Top = (this.ClientSize.Height - pnlAddNoteDetails.Height) / 2;
        }


        private void NoteAddDetailsControl_Resize(object sender, EventArgs e)
        {
            CenterPanel();
        }

        private void rtxtDescription_TextChanged(object sender, EventArgs e)
        {
            lblDescriptionCount.Text = rtxtDescription.TextLength + "/1000";
        }

        private void btnCancelDialog_MouseEnter(object sender, EventArgs e)
        {
            btnCancelDialog.BackColor = Color.Red;
        }

        private void btnCancelDialog_MouseLeave(object sender, EventArgs e)
        {
            btnCancelDialog.BackColor = Color.Transparent;
        }

        private void btnCancelDialog_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveNote_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Note Added Successfully");
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNoteTitle_TextChanged(object sender, EventArgs e)
        {
            lblTitleCount.Text = txtNoteTitle.TextLength + "/100";
        }
    }
}
