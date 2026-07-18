
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
    public partial class NoteEditDetailsControl : Form
    {
        public NoteEditDetailsControl()
        {
            InitializeComponent();
            this.Resize += NoteEditDetailsControl_Resize;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NoteEditDetailsControl_Load(object sender, EventArgs e)
        {
            CenterPanel();
        }
        private void CenterPanel()
        {
            pnlEditNoteDetails.Left = (this.ClientSize.Width - pnlEditNoteDetails.Width) / 2;
            pnlEditNoteDetails.Top = (this.ClientSize.Height - pnlEditNoteDetails.Height) / 2;
        }


        private void NoteEditDetailsControl_Resize(object sender, EventArgs e)
        {
            CenterPanel();
        }
    }
}

