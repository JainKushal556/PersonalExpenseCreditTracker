
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PersonalExpenseCreditTracker.Common;
using BLLayer.Common;

namespace PersonalExpenseCreditTracker.Modules.Note
{
    public partial class NoteEditDetailsControl : Form
    {
        private NoteControl noteControl;
        private string selectedPriorityName = null;
        private string selectedColorName = null;
        private int selectedPriorityId = 0;
        private int selectedColorId=0;

        public NoteEditDetailsControl()
        {
            InitializeComponent();
        }
        public NoteEditDetailsControl(NoteControl noteControl)
        {
            InitializeComponent();
            this.noteControl = noteControl;
            this.Load += NoteEditDetailsControl_Load;
            rbLow.Tag = 1;
            rbMedium.Tag = 2;
            rbHigh.Tag = 3;
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

        private void NoteEditDetailsControl_Load(object sender, EventArgs e)
        {
            SetRadius(btnCancel, 5);
            SetRadius(btnUpdateNote, 5);

            if (noteControl != null)
            {
                // ১. Title সেট করা ও কাউন্টার আপডেট
                if (!string.IsNullOrWhiteSpace(noteControl.SelectedNoteTitle))
                {
                    txtNoteTitle.Text = noteControl.SelectedNoteTitle;
                    txtNoteTitle.ForeColor = Color.Black;
                    lblTitleCount.Text = txtNoteTitle.TextLength + "/100";
                }
                else
                {
                    txtNoteTitle.Text = "Enter title";
                    txtNoteTitle.ForeColor = Color.Gray;
                    lblTitleCount.Text = "0/100";
                }

                // ২. Description সেট করা ও কাউন্টার আপডেট
                if (!string.IsNullOrWhiteSpace(noteControl.SelectedDescription))
                {
                    rtxtDescription.Text = noteControl.SelectedDescription;
                    rtxtDescription.ForeColor = Color.Black;
                    lblDescriptionCount.Text = rtxtDescription.TextLength + "/1000";
                }
                else
                {
                    rtxtDescription.Text = "Enter description";
                    rtxtDescription.ForeColor = Color.Gray;
                    lblDescriptionCount.Text = "0/1000";
                }

                // ৩. Previous Priority
                selectedPriorityName = noteControl.SelectedPriority;
                if (noteControl.SelectedPriority == "Low")
                {
                    rbLow.Checked = true;
                }
                else if (noteControl.SelectedPriority == "Medium")
                {
                    rbMedium.Checked = true;
                }
                else if (noteControl.SelectedPriority == "High")
                {
                    rbHigh.Checked = true;
                }

                // ৪. Previous Color
                selectedColorName = noteControl.SelectedColorName;
                LoadNoteColors();
                LoadSelectedColor();
                
                
            }
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

        private void btnUpdateNote_Click(object sender, EventArgs e)
        {
           
            errorProvider1.Clear();

          
            NoteUI noteUi = new NoteUI();
            noteUi.userId = Session.LogedInUser.GetUserId();
            noteUi.noteId = noteControl.SelectedNoteID;

            noteUi.noteTitle = (txtNoteTitle.Text == "Enter title") ? "" : txtNoteTitle.Text.Trim();
            noteUi.description = (rtxtDescription.Text == "Enter description") ? "" : rtxtDescription.Text.Trim();

            noteUi.priorityId = selectedPriorityId;
            noteUi.colorId = selectedColorId;


            CommonValidator.ValidationResult result = noteUi.UpdateDataIntoNoteUi();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show("Note updated successfully!");
                    this.Close();
                    break;

                case CommonValidator.ValidationResult.NoteTitleInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtNoteTitle);
                    break;

                case CommonValidator.ValidationResult.DescriptionInvalid:
                    ErrorHelper.ShowErrorBelowControl(pnlDescription, "* Description is required.");
                    break;

                case CommonValidator.ValidationResult.DescriptionTooShort:
                    ErrorHelper.ShowErrorBelowControl(pnlDescription, "* Description must contain at least 5 characters.");
                    break;

                case CommonValidator.ValidationResult.DescriptionTooLong:
                    ErrorHelper.ShowErrorBelowControl(pnlDescription, "* Description cannot exceed 1000 characters.");
                    break;

                case CommonValidator.ValidationResult.PriorityInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, lblPriority);
                    break;

                case CommonValidator.ValidationResult.ColorInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, lblNoteColor);
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    MessageBox.Show("Note update failed!");
                    break;
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtNoteTitle_TextChanged(object sender, EventArgs e)
        {
            if (txtNoteTitle.Text != "Enter title")
            {
                lblTitleCount.Text = txtNoteTitle.TextLength + "/100";
            }
            else
            {
                lblTitleCount.Text = "0/100";
            }
            if (txtNoteTitle.Text != "Enter title" && !string.IsNullOrWhiteSpace(txtNoteTitle.Text))
            {
                txtNoteTitle.ForeColor = Color.Black; // 👈 টাইপ করার সাথে সাথে কালো হবে
                ErrorHelper.HideErrorForControl(txtNoteTitle);
            }
        }


        private void rtxtDescription_TextChanged(object sender, EventArgs e)
        {
            if (rtxtDescription.Text != "Enter description")
            {
                lblDescriptionCount.Text = rtxtDescription.TextLength + "/1000";
            }
            else
            {
                lblDescriptionCount.Text = "0/1000";
            }
            if (rtxtDescription.Text != "Enter description" && !string.IsNullOrWhiteSpace(rtxtDescription.Text))
            {
                rtxtDescription.ForeColor = Color.Black; // 👈 টাইপ করার সাথে সাথে কালো হবে
                ErrorHelper.HideErrorForControl(pnlDescription);
            }
        }

        private void SelectColor(Panel selectedPanel)
        {
            selectedColorId = Convert.ToInt32(selectedPanel.Tag);
            Panel[] panels =
                           {pnlWhiteColor,pnlRedColor,pnlOrangeColor,pnlYellowColor,pnlGreenColor, 
                               pnlTealColor,pnlBlueColor,pnlPurpleColor, pnlPinkColor,pnlGrayColor,
                               pnlLavenderColor,pnlCoralColor, pnlMintColor,pnlIndigoColor};


            // Reset all panels
            foreach (Panel pnl in panels)
            {
                pnl.BorderStyle = BorderStyle.FixedSingle;
            }

            // Highlight selected panel
            selectedPanel.BorderStyle = BorderStyle.Fixed3D;
        }

        private void LoadSelectedColor()
        {
            if (selectedColorName == "White")
            {
                SelectColor(pnlWhiteColor);
            }
            else if (selectedColorName == "Red")
            {
                SelectColor(pnlRedColor);
            }
            else if (selectedColorName == "Orange")
            {
                SelectColor(pnlOrangeColor);
            }
            else if (selectedColorName == "Yellow")
            {
                SelectColor(pnlYellowColor);
            }
            else if (selectedColorName == "Green")
            {
                SelectColor(pnlGreenColor);
            }
            else if (selectedColorName == "Teal")
            {
                SelectColor(pnlTealColor);
            }
            else if (selectedColorName == "Blue")
            {
                SelectColor(pnlBlueColor);
            }
            else if (selectedColorName == "Purple")
            {
                SelectColor(pnlPurpleColor);
            }
            else if (selectedColorName == "Pink")
            {
                SelectColor(pnlPinkColor);
            }
            else if (selectedColorName == "Gray")
            {
                SelectColor(pnlGrayColor);
            }
            else if (selectedColorName == "Lavender")
            {
                SelectColor(pnlLavenderColor);
            }
            else if (selectedColorName == "Coral")
            {
                SelectColor(pnlCoralColor);
            }
            else if (selectedColorName == "Mint")
            {
                SelectColor(pnlMintColor);
            }
            else if (selectedColorName == "Indigo")
            {
                SelectColor(pnlIndigoColor);
            }
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
        private void pnlColorWhite_Click(object sender, EventArgs e)
        {
            SelectColor(pnlWhiteColor);
        }

        private void pnlColorRed_Click(object sender, EventArgs e)
        {
            SelectColor(pnlRedColor);
        }

        private void pnlColorOrange_Click(object sender, EventArgs e)
        {
            SelectColor(pnlOrangeColor);
        }

        private void pnlColorYellow_Click(object sender, EventArgs e)
        {
            SelectColor(pnlYellowColor);
        }

        private void pnlColorGreen_Click(object sender, EventArgs e)
        {
            SelectColor(pnlGreenColor);
        }

        private void pnlColorTeal_Click(object sender, EventArgs e)
        {
            SelectColor(pnlTealColor);
        }

        private void pnlColorBlue_Click(object sender, EventArgs e)
        {
            SelectColor(pnlBlueColor);
        }

        private void pnlColorPurple_Click(object sender, EventArgs e)
        {
            SelectColor(pnlPurpleColor);
        }

        private void pnlColorPink_Click(object sender, EventArgs e)
        {
            SelectColor(pnlPinkColor);
        }

        private void pnlColorGray_Click(object sender, EventArgs e)
        {
            SelectColor(pnlGrayColor);
        }

        private void pnlColorLavender_Click(object sender, EventArgs e)
        {
            SelectColor(pnlLavenderColor);
        }

        private void pnlColorCoral_Click(object sender, EventArgs e)
        {
            SelectColor(pnlCoralColor);
        }

        private void pnlColorMint_Click(object sender, EventArgs e)
        {
            SelectColor(pnlMintColor);
        }

        private void pnlColorIndigo_Click(object sender, EventArgs e)
        {
            SelectColor(pnlIndigoColor);
        }

        private void rbHigh_CheckedChanged(object sender, EventArgs e)
        {

            if (rbHigh.Checked)
            {
                rbMedium.Checked = false;
                rbLow.Checked = false;
                selectedPriorityId = Convert.ToInt32(rbHigh.Tag);
            }
        }

        private void rbMedium_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMedium.Checked)
            {
                rbHigh.Checked = false;
                rbLow.Checked = false;
                selectedPriorityId = Convert.ToInt32(rbMedium.Tag);
            }
        }

        private void rbLow_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLow.Checked)
            {
                rbHigh.Checked = false;
                rbMedium.Checked = false;
                selectedPriorityId = Convert.ToInt32(rbLow.Tag);
            }
        }

        private void btnUpdateNote_Resize(object sender, EventArgs e)
        {
            SetRadius(btnUpdateNote, 5);
        }

        private void btnCancel_Resize(object sender, EventArgs e)
        {
            SetRadius(btnCancel, 5);
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

        
    }
}

