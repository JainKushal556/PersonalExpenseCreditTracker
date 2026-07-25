using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PersonalExpenseCreditTracker.Modules.Note;

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

        private void btnSaveNote_Click(object sender, EventArgs e)
        {
            NoteUI noteUI = new NoteUI();
            noteUI.userId = 1;
            noteUI.noteId = 1;
            noteUI.content = rtxtDescription.Text;

            // Map selection to Priority ID
            if (rbLow.Checked)
            {
                noteUI.priorityId = 1;      // Low
            }
            else if (rbMedium.Checked)
            {
                noteUI.priorityId = 2;      // Medium
            }
            else if (rbHigh.Checked)
            {
                noteUI.priorityId = 3;      // High
            }
            else
            {
                MessageBox.Show("Please select a Priority level.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop execution if nothing is selected
            }

            // ----Note Color ----
            RadioButton selectedColorRb = flpNoteColors.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked);
            if (selectedColorRb == null)
            {
                MessageBox.Show("Please select a Note Color.");
                return;
            }

            //Get the color from the selected radio button
            Color selectedColor = selectedColorRb.BackColor;

            //Apply the color as a note back color
            rtxtDescription.BackColor = selectedColor;

            bool result = noteUI.InsertDataToNoteUi(noteUI);

            if (result)
            {
                MessageBox.Show("Validation Passed");
            }
            else
            {
                MessageBox.Show("Validation Failed");
            }
        }

        
    }
}
