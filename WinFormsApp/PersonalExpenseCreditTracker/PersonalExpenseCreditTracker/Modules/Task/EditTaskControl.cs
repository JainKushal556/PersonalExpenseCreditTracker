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
        private bool ignoreEvents = true;

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
            SetRadius(btnCancel, 5);
            SetRadius(btnUpdateTask, 5);

            txtTaskTitle.Text = taskControl.SelectedTaskTitle;
            txtTaskTitle.ForeColor = Color.Black;

            // ✅ এটি লিখুন (শুধু dd-MM-yyyy ফরম্যাট আসবে):
            if (!string.IsNullOrEmpty(taskControl.selectDeadline))
            {
                DateTime parsedDate;
                if (DateTime.TryParse(taskControl.selectDeadline, out parsedDate))
                {
                    txtDeadline.Text = parsedDate.ToString("dd-MM-yyyy"); // ✅ শুধু দিন-মাস-বছর দেখাবে
                    txtDeadline.ForeColor = Color.Black;
                    monthCalendar1.SelectionStart = parsedDate;
                }
                else
                {
                    txtDeadline.Text = "DD-MM-YYYY";
                    txtDeadline.ForeColor = Color.Gray;
                }
            }
            else
            {
                txtDeadline.Text = "DD-MM-YYYY";
                txtDeadline.ForeColor = Color.Gray;
            }


      
            CommonUiFunction.LoadInComboBox("spGetAllTaskPriorities", "Select the Proiority", cmbPriority);
            CommonUiFunction.LoadInComboBox("spGetAllTaskStatus", "Select Status", cmbStatus);

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

            btnCancel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnCancel.Width, btnCancel.Height, 6, 6));
            btnUpdateTask.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnUpdateTask.Width, btnUpdateTask.Height, 6, 6));

            ignoreEvents = false;
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
            ErrorHelper.HideErrorForControl(txtDeadline); 
        }

        private void btnUpdateTask_Click(object sender, EventArgs e)
        {

            errorProvider1.Clear();

            TaskUI taskUi = new TaskUI();
            taskUi.taskId = taskControl.SelectedTaskID;
            taskUi.userId = Session.LogedInUser.GetUserId();
            taskUi.taskTitle = (txtTaskTitle.Text == "Enter task title") ? "" : txtTaskTitle.Text.Trim();
            taskUi.priorityId = Convert.ToInt32(cmbPriority.SelectedValue);
            taskUi.statusId = Convert.ToInt32(cmbStatus.SelectedValue);

            if (!string.IsNullOrEmpty(taskControl.selectDeadline))
            {
                DateTime parsedDate;
                if (DateTime.TryParse(taskControl.selectDeadline, out parsedDate))
                {
                   
                    txtDeadline.Text = parsedDate.ToString("dd-MM-yyyy");
                    txtDeadline.ForeColor = Color.Black;
                    monthCalendar1.SelectionStart = parsedDate;
                }
                else
                {
                    txtDeadline.Text = "DD-MM-YYYY";
                    txtDeadline.ForeColor = Color.Gray;
                }
            }
            else
            {
                txtDeadline.Text = "DD-MM-YYYY";
                txtDeadline.ForeColor = Color.Gray;
            }



            CommonValidator.ValidationResult result = taskUi.UpdateDataIntoTaskUi();

            switch (result)
            {
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
                    MessageBox.Show("Task update failed!");
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
            cmbPriority.DroppedDown = true;
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
            if (cmbPriority.SelectedIndex > 0)
            {
                ErrorHelper.HideErrorForControl(cmbPriority);
            }
            //cmbPriority.AutoCompleteMode = AutoCompleteMode.Append;
            //cmbPriority.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStatus.SelectedIndex > 0)
            {
                ErrorHelper.HideErrorForControl(cmbStatus);
            }
        }


        private void cmbPriority_Enter(object sender, EventArgs e)
        {
            if (cmbPriority.Text == "Select the Proiority")
                cmbPriority.ForeColor = Color.Black;
        }

        private void cmbPriority_Leave(object sender, EventArgs e)
        {
            if (cmbPriority.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(cmbPriority.Text) || cmbPriority.Text == "Select the Proiority")
            {
                cmbPriority.SelectedIndex = 0;
                cmbPriority.Text = "Select the Proiority";
                cmbPriority.ForeColor = Color.Gray;
            }
            else
            {
                cmbPriority.ForeColor = Color.Black;
            }
        }

        private void cmbPriority_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;

            if (cmbPriority.SelectedIndex > 0 ||
               (!string.IsNullOrWhiteSpace(cmbPriority.Text) && cmbPriority.Text != "Select the Proiority"))
            {
                ErrorHelper.HideErrorForControl(cmbPriority);
            }

            
        }

        private void cmbStatus_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            if (cmbStatus.SelectedIndex > 0 ||
               (!string.IsNullOrWhiteSpace(cmbStatus.Text) && cmbStatus.Text != "Select Status"))
            {
                ErrorHelper.HideErrorForControl(cmbStatus);
            }
            //cmbStatus.DroppedDown = true;
        }

        private void cmbStatus_Enter(object sender, EventArgs e)
        {
            if (cmbStatus.Text == "Select Status")
                cmbStatus.ForeColor = Color.Black;
        }

        private void cmbStatus_Leave(object sender, EventArgs e)
        {
            if (cmbStatus.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(cmbStatus.Text) || cmbStatus.Text == "Select Status")
            {
                cmbStatus.SelectedIndex = 0;
                cmbStatus.Text = "Select Status";
                cmbStatus.ForeColor = Color.Gray;
            }
            else
            {
                cmbStatus.ForeColor = Color.Black;
            }
        }



       

    }
}
