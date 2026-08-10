using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLLayer.Common;
using PersonalExpenseCreditTracker.Common;
using System.Runtime.InteropServices;

namespace PersonalExpenseCreditTracker.Modules.Task
{

    public partial class AddTaskControl : Form
    {
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

        public AddTaskControl()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddTaskControl_Load(object sender, EventArgs e)
        {
            SetRadius(pnlBody, 15);
            SetRadius(btnAddTask, 5);
            SetRadius(btnCancel, 5);
            txtDeadline.Text = "DD-MM-YYYY";
            txtDeadline.ForeColor = Color.Gray;
            pnlDeadlinePicker.Visible = false;

            txtTaskTitle.Text = "Enter task title";
            txtTaskTitle.ForeColor = Color.Gray;

            CommonUiFunction.LoadInComboBox("spGetAllTaskPriorities", "Select the Proiority", cmbPriority);
        }

        private void txtTaskTitle_Enter(object sender, EventArgs e)
        {
            if (txtTaskTitle.Text == "Enter task title")
            {
                txtTaskTitle.Text = "";
                txtTaskTitle.ForeColor = Color.Black;
            }
            pnlDeadlinePicker.Visible = false;
        }

        private void txtTaskTitle_Leave(object sender, EventArgs e)
        {
            if (txtTaskTitle.Text.Trim() == "")
            {
                txtTaskTitle.Text = "Enter task title";
                txtTaskTitle.ForeColor = Color.Gray;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
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

        private void btnAddTask_Click(object sender, EventArgs e)
        {

            errorProvider1.Clear();


            TaskUI taskUi = new TaskUI();

            taskUi.userId = Session.LogedInUser.GetUserId(); // Logged-in User ID
            taskUi.taskTitle = (txtTaskTitle.Text == "Enter task title") ? "" : txtTaskTitle.Text;


            int priorityId = 0;
            DataRowView drv = cmbPriority.SelectedValue as DataRowView;
            if (drv != null)
            {
                priorityId = Convert.ToInt32(drv[0]);
            }
            else if (cmbPriority.SelectedValue != null)
            {
                int.TryParse(cmbPriority.SelectedValue.ToString(), out priorityId);
            }
            taskUi.priorityId = priorityId;
            DateTime parsedDate;
            if (txtDeadline.Text != "DD-MM-YYYY" &&
                DateTime.TryParseExact(txtDeadline.Text, "dd-MM-yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                taskUi.deadline = parsedDate;
            }
            else
            {
                taskUi.deadline = (txtDeadline.Text == "DD-MM-YYYY") ? DateTime.MinValue : monthCalendar1.SelectionStart;
            }


            CommonValidator.ValidationResult result = taskUi.InsertDataIntoTaskUi();


            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show("Task added successfully!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;

                case CommonValidator.ValidationResult.TaskTitleInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtTaskTitle);
                    break;

                case CommonValidator.ValidationResult.PriorityInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, cmbPriority);
                    break;

                case CommonValidator.ValidationResult.DeadlineInvalid:
                    ErrorHelper.ShowValidationError(result, errorProvider1, txtDeadline);
                    break;

                case CommonValidator.ValidationResult.StoreProcedureError:
                    MessageBox.Show("Task added unsuccessfully!");
                    break;
            }
        }


        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtDeadline.Text = e.Start.ToString("dd-MM-yyyy");
            txtDeadline.ForeColor = Color.Black;
            pnlDeadlinePicker.Visible = false;
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
            if (string.IsNullOrWhiteSpace(txtDeadline.Text))
            {
                txtDeadline.Text = "DD-MM-YYYY";
                txtDeadline.ForeColor = Color.Gray;
            }
            else
            {
                txtDeadline.ForeColor = Color.Black;
            }
        }
        
        private void btnCalendar_Click(object sender, EventArgs e)
        {
            //pnlDeadlinePicker.BringToFront();
            pnlDeadlinePicker.Visible = !pnlDeadlinePicker.Visible;
        }

        private void txtDeadline_TextChanged(object sender, EventArgs e)
        {
            pnlDeadlinePicker.Visible = false;
            if (txtDeadline.Text != "DD-MM-YYYY" && !string.IsNullOrWhiteSpace(txtDeadline.Text))
            {
                ErrorHelper.HideErrorForControl(txtDeadline);
            }
        }


        private void pnlAddTask_Click(object sender, EventArgs e)
        {
            pnlDeadlinePicker.Visible = false;
        }

        private void pnlBody_Click(object sender, EventArgs e)
        {
            pnlDeadlinePicker.Visible = false;
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
            cmbPriority.AutoCompleteMode = AutoCompleteMode.Append;
            cmbPriority.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void cmbPriority_Click(object sender, EventArgs e)
        {
            pnlDeadlinePicker.Visible = false;
            cmbPriority.DroppedDown = true;
        }

        private void txtDeadline_Click(object sender, EventArgs e)
        {
            pnlDeadlinePicker.Visible = true;
        }

        


    }
}