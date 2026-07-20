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

        }
    }
}
