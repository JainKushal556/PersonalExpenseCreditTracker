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
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);

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
       
      private void EditTaskControl_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
    
           
            txtTaskTitle.Text = taskControl.SelectedTaskTitle;
            txtTaskTitle.ForeColor = Color.Black;

         
            if (!string.IsNullOrEmpty(taskControl.selectDeadline))
            {
                txtDeadline.Text = taskControl.selectDeadline;
                txtDeadline.ForeColor = Color.Black;

               
                DateTime parsedDate;
                if (DateTime.TryParse(taskControl.selectDeadline, out parsedDate))
                {
                    monthCalendar1.SelectionStart = parsedDate;
                }
            }
            else
            {
                txtDeadline.Text = "DD-MM-YYYY";
                txtDeadline.ForeColor = Color.Gray;
            }

      
            CommonUiFunction.LoadInComboBox("spGetAllTaskPriorities", "Select the Proiority", cmbPriority);
            CommonUiFunction.LoadInComboBox("spGetAllTaskStatus", "Select Status", cmbStatus);

      
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

            btnCancel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnCancel.Width, btnCancel.Height, 6, 6));
            btnUpdateTask.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnUpdateTask.Width, btnUpdateTask.Height, 6, 6));
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
            
            // Clear all previous validation errors
            errorProvider1.Clear();

            // Create a new object to store the user's input
            TaskUI taskUi = new TaskUI();
            int currentTaskId = taskControl.SelectedTaskID;
            taskUi.taskId = currentTaskId;
            taskUi.userId = Session.LogedInUser.GetUserId();
            taskUi.taskTitle = (txtTaskTitle.Text == "Enter task title") ? "" : txtTaskTitle.Text;
            taskUi.priorityId = Convert.ToInt32(cmbPriority.SelectedValue);
            taskUi.statusId = Convert.ToInt32(cmbStatus.SelectedValue);

            // If no deadline is selected, assign DateTime.MinValue
            // Otherwise, assign the selected date from the calendar
            taskUi.deadline = (txtDeadline.Text == "DD-MM-YYYY") ? DateTime.MinValue : monthCalendar1.SelectionStart;

            CommonValidator.ValidationResult result = taskUi.UpdateDataIntoTaskUi();
            // Perform action based on the validation result
            switch (result)
            {
                // Data is valid and updated successfully
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show("Task updated successfully!");
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

            monthCalendar1.Visible = true;
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
        }


      

     

    }
}
