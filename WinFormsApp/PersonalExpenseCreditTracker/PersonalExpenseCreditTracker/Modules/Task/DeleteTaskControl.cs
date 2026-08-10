using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using BLLayer.Common;

namespace PersonalExpenseCreditTracker.Modules.Task
{
    public partial class DeleteTask : Form
    {
        private TaskControls taskControl = null;
        public DeleteTask()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

          
        }
        public DeleteTask(TaskControls taskControl)
        {
            InitializeComponent();
            this.taskControl = taskControl;
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

        private void DeleteTaskControl_Load(object sender, EventArgs e)
        {
            SetRadius(btnCancel, 5);
            SetRadius(btnDeleteTask, 5);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
            btnClose.ForeColor = Color.White;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                "Are you sure you want to delete this task?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                TaskUI taskUi = new TaskUI();
                taskUi.taskId = taskControl.SelectedTaskID;

                CommonValidator.ValidationResult result = taskUi.DeleteTaskIntoTaskUi();

                switch (result)
                {
                    case CommonValidator.ValidationResult.Success:
                        
                        this.Close();
                        break;

                    case CommonValidator.ValidationResult.StoreProcedureError:
                        MessageBox.Show("Task could not be deleted.");
                        break;
                }
            }
        }
    }
}
