using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PersonalExpenseCreditTracker.Common;
using System.Runtime.InteropServices;
using BLLayer.Common;

namespace PersonalExpenseCreditTracker.Modules.Task
{
    public partial class EditTaskControl : Form
    {
        private TaskControls taskControl;

        public EditTaskControl()
        {
            InitializeComponent();
        }

        public EditTaskControl(TaskControls taskcontrol)
        {
            InitializeComponent();

            taskControl = taskcontrol;
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
       
        private void EditTaskControl_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            SetRadius(pnlBody, 15);
            SetRadius(btnCancel,     6);
            SetRadius(btnUpdateTask, 6);

            txtTaskTitle.Text = taskControl.SelectedTaskTitle;
            txtTaskTitle.ForeColor = Color.Black;


            if (!string.IsNullOrEmpty(taskControl.selectDeadline))
            {
                DateTime parsedDate;
                if (DateTime.TryParse(taskControl.selectDeadline, out parsedDate))
                {
                   
                    txtDeadline.Text = parsedDate.ToString("dd-MM-yyyy");
                    monthCalendar1.SelectionStart = parsedDate;
                }
                else
                {
                    txtDeadline.Text = taskControl.selectDeadline;
                }
                txtDeadline.ForeColor = Color.Black;
            }
            else
            {
                txtDeadline.Text = "DD-MM-YYYY";
                txtDeadline.ForeColor = Color.Gray;
            }


      
            CommonUiFunction.LoadInComboBox("spGetAllTaskPriorities", "Select the Proiority", cmbPriority);
            CommonUiFunction.LoadInComboBox("spGetAllTaskStatus", "Select Status", cmbStatus);

            cmbPriority.AutoCompleteMode = AutoCompleteMode.Append;
            cmbPriority.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbStatus.AutoCompleteMode = AutoCompleteMode.Append;
            cmbStatus.AutoCompleteSource = AutoCompleteSource.ListItems;


      
            if (!string.IsNullOrEmpty(taskControl.selectPriority))
            {
                int priorityIndex = cmbPriority.FindStringExact(taskControl.selectPriority);
                if (priorityIndex != -1)
                {
                    cmbPriority.SelectedIndex = priorityIndex;
                }
            }

         
            if (!string.IsNullOrEmpty(taskControl.selectStatus))
            {
                int statusIndex = cmbStatus.FindStringExact(taskControl.selectStatus);
                if (statusIndex != -1)
                {
                    cmbStatus.SelectedIndex = statusIndex;
                }
            }

            // Regions already applied by SetRadius() above — no duplicate needed
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {

                if (cmbPriority.Focused)
                {
                    SelectComboBoxSuggestion(cmbPriority);
                    return true; 
                }

                else if (cmbStatus.Focused)
                {
                    SelectComboBoxSuggestion(cmbStatus);
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void SelectComboBoxSuggestion(ComboBox cmb)
        {
            if (!string.IsNullOrWhiteSpace(cmb.Text))
            {

                int index = cmb.FindStringExact(cmb.Text);

                if (index == -1)
                {
                    index = cmb.FindString(cmb.Text);
                }

  
                if (index != -1)
                {
                    cmb.SelectedIndex = index;
                    cmb.SelectionStart = cmb.Text.Length;
                }
            }

 
            cmb.DroppedDown = false;
        }




        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {

            monthCalendar1.Visible = !monthCalendar1.Visible;
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtDeadline.Text = e.Start.ToString("dd-MM-yyyy");
            txtDeadline.ForeColor = Color.Black;
            monthCalendar1.Visible = false;
        }
        private void btnUpdateTask_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            TaskUI taskUi = new TaskUI();
            taskUi.taskId = taskControl.SelectedTaskID;
            taskUi.userId = Session.LogedInUser.GetUserId();
            taskUi.taskTitle = (txtTaskTitle.Text == "Enter task title") ? "" : txtTaskTitle.Text;

            int priorityId = 0;
            DataRowView drvPriority = cmbPriority.SelectedValue as DataRowView;
            if (drvPriority != null)
            {
                priorityId = Convert.ToInt32(drvPriority[0]);
            }
            else if (cmbPriority.SelectedValue != null)
            {
                int.TryParse(cmbPriority.SelectedValue.ToString(), out priorityId);
            }
            taskUi.priorityId = priorityId;


            int statusId = 0;
            DataRowView drvStatus = cmbStatus.SelectedValue as DataRowView;
            if (drvStatus != null)
            {
                statusId = Convert.ToInt32(drvStatus[0]);
            }
            else if (cmbStatus.SelectedValue != null)
            {
                int.TryParse(cmbStatus.SelectedValue.ToString(), out statusId);
            }
            taskUi.statusId = statusId;


            DateTime parsedDate;
            if (!string.IsNullOrWhiteSpace(txtDeadline.Text) &&
                txtDeadline.Text != "DD-MM-YYYY" &&
                DateTime.TryParseExact(txtDeadline.Text.Trim(), "dd-MM-yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                taskUi.deadline = parsedDate;
            }
            else
            {
                taskUi.deadline = DateTime.MinValue;
            }

            if (!ErrorHelper.Validate(CommonValidator.ValidateTaskTitle(taskUi.taskTitle), errorProvider1, txtTaskTitle)) return;
            if (!ErrorHelper.Validate(CommonValidator.ValidatePriority(priorityId), errorProvider1, cmbPriority)) return;
            if (!ErrorHelper.Validate(CommonValidator.ValidateStatus(statusId), errorProvider1, cmbStatus)) return;
            if (!ErrorHelper.Validate(CommonValidator.ValidateDeadline(taskUi.deadline), errorProvider1, txtDeadline)) return;

           
            DialogResult confirmResult = MessageBox.Show(
                "Are you sure you want to update this task?",
                "Confirm Update",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            CommonValidator.ValidationResult result = taskUi.UpdateDataIntoTaskUi();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;

                case CommonValidator.ValidationResult.TaskTitleInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtTaskTitle);
                    break;

                case CommonValidator.ValidationResult.PriorityInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, cmbPriority);
                    break;

                case CommonValidator.ValidationResult.StatusInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, cmbStatus);
                    break;

                case CommonValidator.ValidationResult.DeadlineInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtDeadline);
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    MessageBox.Show("Task updated unsuccessfully!");
                    break;
            }
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        private void txtTaskTitle_Enter(object sender, EventArgs e)
        {
            if (txtTaskTitle.Text == "Enter task title")
            {
                txtTaskTitle.Text = "";
                txtTaskTitle.ForeColor = Color.Black;
            }
            monthCalendar1.Visible = false;
        }

        private void txtTaskTitle_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskTitle.Text))
            {
                txtTaskTitle.Text = "Enter task title";
                txtTaskTitle.ForeColor = Color.Gray;
            }
        }

        private void txtDeadline_Enter(object sender, EventArgs e)
        {
            if (txtDeadline.Text == "DD-MM-YYYY")
            {
                txtDeadline.Text = "";
                txtDeadline.ForeColor = Color.Black;
            }

        }

        private void txtDeadline_Leave(object sender, EventArgs e)
        {
            if (txtDeadline.Text == "")
            {
                txtDeadline.Text = "DD-MM-YYYY";
                txtDeadline.ForeColor = Color.Gray;
            }
        }

        private void txtDeadline_TextChanged(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
            if (txtDeadline.Text != "DD-MM-YYYY" && !string.IsNullOrWhiteSpace(txtDeadline.Text))
            {
                ErrorHelper.HideErrorForControl(txtDeadline);
            }
        }

        private void pnlBody_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        private void pnlEditTask_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        private void cmbPriority_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        private void cmbStatus_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
            cmbStatus.DroppedDown = true;
        }

        private void txtTaskTitle_TextChanged(object sender, EventArgs e)
        {
            if (txtTaskTitle.Text != "Enter task title" && !string.IsNullOrWhiteSpace(txtTaskTitle.Text))
            {
                ErrorHelper.HideErrorForControl(txtTaskTitle);
            }
        }

        private void cmbPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            ErrorHelper.HideErrorForControl(cmbPriority);
          
            cmbPriority.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbPriority.AutoCompleteMode = AutoCompleteMode.Append;
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ErrorHelper.HideErrorForControl(cmbStatus);
            cmbStatus.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbStatus.AutoCompleteMode = AutoCompleteMode.Append;

        }

        private void txtDeadline_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
        }


    }
}
