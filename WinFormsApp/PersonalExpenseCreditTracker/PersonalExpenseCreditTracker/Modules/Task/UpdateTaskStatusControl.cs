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

namespace PersonalExpenseCreditTracker.Modules.Task
{
    public partial class UpdateTaskStatus : Form
    {
        private TaskControls taskControl;
        
        public UpdateTaskStatus(TaskControls taskcontrol)
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

        private void UpdateTaskStatus_Load(object sender, EventArgs e)
        {

            SetRadius(pnlBody, 15);
            SetRadius(btnCancel, 5);
            SetRadius(btnUpdate, 5);

            txtTaskTitle.Text = taskControl.SelectedTaskTitle;
            lblCurrentStatus.Text = taskControl.selectStatus;

            CommonUiFunction.LoadInComboBox("spGetAllTaskStatus", "Select Status", cmbStatus);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            int statusId = Convert.ToInt32(cmbStatus.SelectedValue);
            if (!ErrorHelper.Validate(CommonValidator.ValidateStatus(statusId), errorProvider1, cmbStatus)) return;
            DialogResult confirmResult = MessageBox.Show(
                "Are you sure you want to update the task status?",
                "Confirm Update",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            TaskUI taskUi = new TaskUI();
            taskUi.taskId = taskControl.SelectedTaskID;
            taskUi.statusId = statusId;

            CommonValidator.ValidationResult result = taskUi.UpdateStatusIntoTaskUi();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    this.Close();
                    break;

                case CommonValidator.ValidationResult.StatusInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, cmbStatus);
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    MessageBox.Show("Task status update unsuccessful!");
                    break;

                case CommonValidator.ValidationResult.TaskAlreadyUpdated:
                    MessageBox.Show("Task is already in this status.");
                    break;
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ErrorHelper.HideErrorForControl(cmbStatus);
            cmbStatus.AutoCompleteSource = AutoCompleteSource.ListItems; 
            cmbStatus.AutoCompleteMode = AutoCompleteMode.Append;        
        }

        private void cmbStatus_Click(object sender, EventArgs e)
        {
            cmbStatus.DroppedDown = true;
        }

        
       
    }
}
