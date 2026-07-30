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
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        public UpdateTaskStatus(TaskControls taskcontrol)
        {
            InitializeComponent();
            taskControl = taskcontrol;
        }

        private void UpdateTaskStatus_Load(object sender, EventArgs e)
        {

            btnCancel.Region = Region.FromHrgn(CreateRoundRectRgn(
                0,
                0,
                btnCancel.Width,
                btnCancel.Height,
                5,
                5));

            btnUpdate.Region = Region.FromHrgn(CreateRoundRectRgn(
                0,
                0,
                btnUpdate.Width,
                btnUpdate.Height,
                5,
                5));

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
            
            // Clear all previous validation errors
            errorProvider1.Clear();

            // Create a new object to store the user's input
            TaskUI taskUi = new TaskUI();

            taskUi.taskId = taskControl.SelectedTaskID;
            taskUi.statusId = Convert.ToInt32(cmbStatus.SelectedValue);

            CommonValidator.ValidationResult result = taskUi.UpdateStatusIntoTaskUi();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show("Task status updated successfully!");
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

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        
       
    }
}
