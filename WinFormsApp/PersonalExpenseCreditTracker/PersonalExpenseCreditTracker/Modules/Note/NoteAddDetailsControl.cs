using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using BLLayer.Common;
using PersonalExpenseCreditTracker.Common;
using PersonalExpenseCreditTracker.Session;
namespace PersonalExpenseCreditTracker.Modules.Note
{
    public partial class NoteAddDetailsControl : Form
    {
        private int selectedColorId = 0;
        private int selectedPriorityId = 0;
        
        public NoteAddDetailsControl()
        {
            InitializeComponent();
            rbLow.Tag = 1;
            rbMedium.Tag = 2;
            rbHigh.Tag = 3;
            LoadFormData();
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

        private void LoadFormData()
        {
            txtNoteTitle.Text = "Enter title";
            txtNoteTitle.ForeColor = Color.Gray;

            rtxtDescription.Text = "Enter description";
            rtxtDescription.ForeColor = Color.Gray;
            LoadNoteColors();
        }

        private void txtNoteTitle_Enter(object sender, EventArgs e)
        {
            if (txtNoteTitle.Text == "Enter title")
            {
                txtNoteTitle.Text = "";
                txtNoteTitle.ForeColor = Color.Black;
            }
        }
        private void txtNoteTitle_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNoteTitle.Text))
            {
                txtNoteTitle.Text = "Enter title";
                txtNoteTitle.ForeColor = Color.Gray;
            }
        }

        private void rtxtDescription_Enter(object sender, EventArgs e)
        {
            if (rtxtDescription.Text == "Enter description")
            {
                rtxtDescription.Text = "";
                rtxtDescription.ForeColor = Color.Black;
            }
        }

        private void rtxtDescription_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtxtDescription.Text))
            {
                rtxtDescription.Text = "Enter description";
                rtxtDescription.ForeColor = Color.Gray;
            }
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

            // Clear all previous validation errors
            errorProvider1.Clear();

            NoteUI noteUi = new NoteUI();

            // Assign values from the form controls to the object
            noteUi.userId = Session.LogedInUser.GetUserId();
            noteUi.noteId = -1;

            // If the placeholder text is still present, pass an empty string
            noteUi.noteTitle = (txtNoteTitle.Text == "Enter title") ? "" : txtNoteTitle.Text;
            noteUi.description = (rtxtDescription.Text == "Enter description") ? "" : rtxtDescription.Text;
            noteUi.priorityId = selectedPriorityId;
            noteUi.colorId = selectedColorId;


            CommonValidator.ValidationResult result = noteUi.InsertDataIntoNoteUi();

            // Perform action based on the validation result
            switch (result)
            {
                // Data is valid and inserted successfully
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show("Note added successfully!");
                    
                    this.Close();

                    break;
                case CommonValidator.ValidationResult.NoteTitleInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtNoteTitle);
                    break;
                case CommonValidator.ValidationResult.DescriptionInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, rtxtDescription);
                    break;

                case CommonValidator.ValidationResult.PriorityInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1,lblPriority);
                    break;
                case CommonValidator.ValidationResult.ColorInvalid:
                    ErrorHelper.ShowValidationError( result, errorProvider1,lblNoteColor);
                    break;
                  case CommonValidator.ValidationResult.StoreProcedureError:

                    MessageBox.Show("Note added unsuccessfully!");
                    break;

            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtNoteTitle_TextChanged(object sender, EventArgs e)
        {
            lblTitleCount.Text = txtNoteTitle.TextLength + "/100";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlAddNoteDetails_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SelectPriority(RadioButton selectedRadioButton)
        {
            rbLow.Checked = false;
            rbMedium.Checked = false;
            rbHigh.Checked = false;

            selectedRadioButton.Checked = true;

            selectedPriorityId = Convert.ToInt32(selectedRadioButton.Tag);

            // Remove priority validation icon
            errorProvider1.SetError(lblPriority, "");
        }

        private void rbLow_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLow.Checked)
            {
                rbMedium.Checked=false;
                rbHigh.Checked=false;
                selectedPriorityId = Convert.ToInt32(rbLow.Tag);
            }
        }

        private void rbMedium_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMedium.Checked)
            {
                rbLow.Checked = false;
                rbHigh.Checked = false;
                selectedPriorityId = Convert.ToInt32(rbMedium.Tag);
            }
        }

        private void rbHigh_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHigh.Checked)
            {
                rbLow.Checked = false;
                rbMedium.Checked = false;
                selectedPriorityId = Convert.ToInt32(rbHigh.Tag);
            }
        }



        private void pnlWhiteColor_Click(object sender, EventArgs e)
        {
            SelectColor(pnlWhiteColor);
        }

        private void pnlRedColor_Click(object sender, EventArgs e)
        {
            SelectColor(pnlRedColor);
        }

        private void pnlOrangeColor_Click(object sender, EventArgs e)
        {
            SelectColor(pnlOrangeColor);
        }

        private void pnlYellowColor_Click(object sender, EventArgs e)
        {
            SelectColor(pnlYellowColor);
        }

        private void pnlGreenColor_Click(object sender, EventArgs e)
        {
            SelectColor(pnlGreenColor);
        }

        private void pnlTealColor_Click(object sender, EventArgs e)
        {
            SelectColor(pnlTealColor);
        }

        private void pnlBlueColor_Click(object sender, EventArgs e)
        {
            SelectColor(pnlBlueColor);
        }

        private void pnlPurpleColor_Click(object sender, EventArgs e)
        {
            SelectColor(pnlPurpleColor);
        }

        private void pnlPinkColor_Click(object sender, EventArgs e)
        {
            SelectColor(pnlPinkColor);
        }

        private void pnlGrayColor_Click(object sender, EventArgs e)
        {
            SelectColor(pnlGrayColor);
        }
        private void pnlLavenderColor_Click(object sender, EventArgs e)
        {
            SelectColor(pnlLavenderColor);
        }
        private void pnlCoralColor_Click(object sender, EventArgs e)
        {
            SelectColor(pnlCoralColor);
        }
        private void pnlMintColor_Click(object sender, EventArgs e)
        {
            SelectColor(pnlMintColor);
        }
        private void pnlIndigoColor_Click(object sender, EventArgs e)
        {
            SelectColor(pnlIndigoColor);
        }
        private Panel selectedColorPanel = null;
        private void SelectColor(Panel selectedPanel)
        {

            selectedColorId = Convert.ToInt32(selectedPanel.Tag);
            selectedColorPanel = selectedPanel;
            errorProvider1.SetError(flpNoteColors, "");

            Panel[] panels =
                           {pnlWhiteColor,pnlRedColor,pnlOrangeColor,pnlYellowColor,pnlGreenColor, 
                               pnlTealColor,pnlBlueColor,pnlPurpleColor, pnlPinkColor,pnlGrayColor,
                               pnlLavenderColor,pnlCoralColor, pnlMintColor,pnlIndigoColor};

            foreach (Panel panel in panels)
            {
                panel.BorderStyle = BorderStyle.FixedSingle;
            }
       
     
            selectedPanel.BorderStyle = BorderStyle.Fixed3D;
        }
        private void LoadNoteColors()
        {
            try
            {
                DataTable dt = CommonUiFunction.RetrieveListForComboBox(
                    "spGetAllNoteColors");

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("No color data found.");
                    return;
                }

                foreach (DataRow row in dt.Rows)
                {
                    int colorId = Convert.ToInt32(row["NoteColorID"]);
                    string hexCode = row["ColorHexCode"].ToString().Trim();

                    Color color = ColorTranslator.FromHtml(hexCode);

                    Panel panel = null;

                    switch (colorId)
                    {
                        case 1:
                            panel = pnlWhiteColor;
                            break;

                        case 2:
                            panel = pnlRedColor;
                            break;

                        case 3:
                            panel = pnlOrangeColor;
                            break;

                        case 4:
                            panel = pnlYellowColor;
                            break;

                        case 5:
                            panel = pnlGreenColor;
                            break;

                        case 6:
                            panel = pnlTealColor;
                            break;

                        case 7:
                            panel = pnlBlueColor;
                            break;

                        case 8:
                            panel = pnlPurpleColor;
                            break;

                        case 9:
                            panel = pnlPinkColor;
                            break;

                        case 10:
                            panel = pnlGrayColor;
                            break;

                        case 11:
                            panel = pnlLavenderColor;
                            break;

                        case 12:
                            panel = pnlCoralColor;
                            break;

                        case 13:
                            panel = pnlMintColor;
                            break;

                        case 14:
                            panel = pnlIndigoColor;
                            break;
                    }

                    if (panel != null)
                    {
                        // Background color from database
                        panel.BackColor = color;

                        // Store Color ID
                        panel.Tag = colorId;

                        // Keep your existing border
                        panel.BorderStyle = BorderStyle.FixedSingle;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load note colors.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

       

    }
}
