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

            txtDeadline.Text = DateTime.Today.ToString("dd-MM-yyyy");
            monthCalendar1.Visible = false;

            txtTaskTitle.Text = "Enter task title";
            txtTaskTitle.ForeColor = Color.Gray;
            cmbPriority.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;

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
                txtTaskTitle.Text = "Enter task title";
                txtTaskTitle.ForeColor = Color.Gray;
            }
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
            if (cmbStatus.SelectedIndex == 0)
            {
                MessageBox.Show("Please select Status.");
                cmbPriority.Focus();
                return;
            }
            MessageBox.Show("Task Add Successfully.");
            this.Close();
        }

    }
}
