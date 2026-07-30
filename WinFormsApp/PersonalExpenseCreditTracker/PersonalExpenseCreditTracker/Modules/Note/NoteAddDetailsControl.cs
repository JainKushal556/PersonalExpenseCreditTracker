using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
<<<<<<< HEAD
using PersonalExpenseCreditTracker.Modules.Note;
=======
using System.Runtime.InteropServices;
>>>>>>> origin/main

namespace PersonalExpenseCreditTracker.Modules.Note
{
    public partial class NoteAddDetailsControl : Form
    {
        public NoteAddDetailsControl()
        {
            InitializeComponent();
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

        private void NoteAddDetailsControl_Load(object sender, EventArgs e)
        {
            SetRadius(btnCancel, 5);
            SetRadius(btnSaveNote, 5);
        }

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

        private void SelectColor(Panel selectedPanel)
        {
            Panel[] panels =
            {
                pnlColorWhite, pnlColorCream, pnlColorYellow, pnlColorOrange,
                pnlColorPink, pnlColorLavender, pnlColorBlue, pnlColorGreen,
                pnlColorMint, pnlColorBlack, pnlColorGray, pnlColorRed,
                pnlColorPurple, pnlColorBrown
            };

            foreach (Panel pnl in panels)
            {
                pnl.BorderStyle = BorderStyle.FixedSingle;
            }

            selectedPanel.BorderStyle = BorderStyle.Fixed3D;
        }

        private void txtNoteTitle_TextChanged(object sender, EventArgs e)
        {
            lblTitleCount.Text = txtNoteTitle.TextLength + "/100";
        }

        private void pnlColorWhite_Click(object sender, EventArgs e)
        {
            SelectColor(pnlColorWhite);
        }

        private void pnlColorCream_Click(object sender, EventArgs e)
        {
            SelectColor(pnlColorCream);
        }

        private void pnlColorYellow_Click(object sender, EventArgs e)
        {
            SelectColor(pnlColorYellow);
        }

        private void pnlColorOrange_Click(object sender, EventArgs e)
        {
            SelectColor(pnlColorOrange);
        }

        private void pnlColorPink_Click(object sender, EventArgs e)
        {
            SelectColor(pnlColorPink);
        }

        private void pnlColorBlue_Click(object sender, EventArgs e)
        {
            SelectColor(pnlColorBlue);
        }

        private void pnlColorGreen_Click(object sender, EventArgs e)
        {
            SelectColor(pnlColorGreen);
        }

        private void pnlColorMint_Click(object sender, EventArgs e)
        {
            SelectColor(pnlColorMint);
        }

        private void pnlColorBlack_Click(object sender, EventArgs e)
        {
            SelectColor(pnlColorBlack);
        }

        private void pnlColorGray_Click(object sender, EventArgs e)
        {
            SelectColor(pnlColorGray);
        }

        private void pnlColorRed_Click(object sender, EventArgs e)
        {
            SelectColor(pnlColorRed);
        }

        private void pnlColorBrown_Click(object sender, EventArgs e)
        {
            SelectColor(pnlColorBrown);
        }

        private void rbHigh_CheckedChanged(object sender, EventArgs e)
        {

            if (rbHigh.Checked)
            {
                rbMedium.Checked = false;
                rbLow.Checked = false;
            }
        }

        private void rbMedium_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMedium.Checked)
            {
                rbHigh.Checked = false;
                rbLow.Checked = false;
            }
        }

        private void rbLow_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLow.Checked)
            {
                rbHigh.Checked = false;
                rbMedium.Checked = false;
            }
        }

        private void pnlColorPurple_Click(object sender, EventArgs e)
        {
            SelectColor(pnlColorPurple);
        }

        private void pnlColorLavender_Click(object sender, EventArgs e)
        {
            SelectColor(pnlColorLavender);
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
