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
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);

        public AddTaskControl()
        {
            InitializeComponent();

            //this.FormBorderStyle = FormBorderStyle.None;

            //this.Region = Region.FromHrgn(
            //    CreateRoundRectRgn(0, 0, this.Width, this.Height, 10, 10)
            //);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddTaskControl_Load(object sender, EventArgs e)
        {

            txtDeadline.Text = "DD-MM-YYYY";
            txtDeadline.ForeColor = Color.Gray;
            pnlDeadlinePicker.Visible = false;

            txtTaskTitle.Text = "Enter task title";
            txtTaskTitle.ForeColor = Color.Gray;
            //cmbPriority.SelectedIndex = 0;
            //cmbStatus.SelectedIndex = 0;

            pnlTaskTitle.Region = Region.FromHrgn(CreateRoundRectRgn(
                0,
                0,
                pnlTaskTitle.Width,
                pnlTaskTitle.Height,
                5,
                5));

            pnlPriority.Region = Region.FromHrgn(CreateRoundRectRgn(
                0,
                0,
                pnlPriority.Width,
                pnlPriority.Height,
                5,
                5));

            pnlStatus.Region = Region.FromHrgn(CreateRoundRectRgn(
                0,
                0,
                pnlStatus.Width,
                pnlStatus.Height,
                5,
                5));

            pnlDeadline.Region = Region.FromHrgn(CreateRoundRectRgn(
                0,
                0,
                pnlDeadline.Width,
                pnlDeadline.Height,
                5,
                5));

            btnCancel.Region = Region.FromHrgn(CreateRoundRectRgn(
                0,
                0,
                btnCancel.Width,
                btnCancel.Height,
                5,
                5));

            btnAddTask.Region = Region.FromHrgn(CreateRoundRectRgn(
                0,
                0,
                btnAddTask.Width,
                btnAddTask.Height,
                5,
                5));

            CommonUiFunction.LoadInComboBox("spGetAllTaskPriorities", "Select the Proiority", cmbPriority);

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
            if (txtTaskTitle.Text.Trim() == "")
            {
                //txtTaskTitle.Text = "Enter task title";
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

            // Clear all previous validation errors
            errorProvider1.Clear();

            // Create a new object to store the user's input
            TaskUI taskUi = new TaskUI();


            taskUi.userId = Session.LogedInUser.GetUserId(); // Logged-in UserID
            taskUi.taskTitle = (txtTaskTitle.Text == "Enter task title") ? "" : txtTaskTitle.Text;
            taskUi.priorityId = Convert.ToInt32(cmbPriority.SelectedValue);
          

            // If no deadline is selected, assign DateTime.MinValue
            // Otherwise, assign the selected date from the calendar
            taskUi.deadline = (txtDeadline.Text == "DD-MM-YYYY") ? DateTime.MinValue : monthCalendar1.SelectionStart;

            CommonValidator.ValidationResult result = taskUi.InsertDataIntoTaskUi();

            // Perform action based on the validation result
            switch (result)
            {
                // Data is valid and inserted successfully
                case CommonValidator.ValidationResult.Success:
                    MessageBox.Show("Task added successfully!");
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

        private void pnlAddTask_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbPriority_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void txtDeadline_Enter(object sender, EventArgs e)
        {
            if (txtDeadline.Text == "DD-MM-YYYY")
            {
                txtDeadline.Text = "";
                txtDeadline.ForeColor = Color.Black;
            }

            pnlDeadlinePicker.Visible = true;
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

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtDeadline.Text = e.Start.ToString("dd-MM-yyyy");
            txtDeadline.ForeColor = Color.Black;
            pnlDeadlinePicker.Visible = false;
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            pnlDeadlinePicker.Visible = !pnlDeadlinePicker.Visible;
        }

        private void txtDeadline_TextChanged(object sender, EventArgs e)
        {
            pnlDeadlinePicker.Visible = false;
        }
    }
}
