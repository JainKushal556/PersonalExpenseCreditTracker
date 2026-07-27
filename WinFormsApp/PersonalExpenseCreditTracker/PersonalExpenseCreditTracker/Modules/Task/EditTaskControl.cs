using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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
            txtTaskTitle.Text = "Enter task title";
            txtTaskTitle.ForeColor = Color.Gray;
            // Buttons
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
            monthCalendar1.Location = new Point(
                pnlDeadline.Left,
                pnlDeadline.Top - monthCalendar1.Height - 5);

            monthCalendar1.Visible = !monthCalendar1.Visible;
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtDeadline.Text = e.Start.ToString("dd-MM-yyyy");
            monthCalendar1.Visible = false;
        }

        private void btnUpdateTask_Click(object sender, EventArgs e)
        {
            if (txtTaskTitle.Text.Trim() == "")
            {
                MessageBox.Show("Please enter task title.");
                txtTaskTitle.Focus();
                return;
            }

            if (cmbPriority.SelectedIndex == 0)
            {
                MessageBox.Show("Please select priority.");
                cmbPriority.Focus();
                return;
            }

            MessageBox.Show("Task Updated Successfully.");
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


      

     

    }
}
