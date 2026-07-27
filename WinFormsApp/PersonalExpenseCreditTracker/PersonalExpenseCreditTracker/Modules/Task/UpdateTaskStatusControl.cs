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
    public partial class UpdateTaskStatus : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public UpdateTaskStatus()
        {
            InitializeComponent();

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
            if (cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select status.",
                                "Validation",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                cmbStatus.Focus();
                return;
            }

            MessageBox.Show("Task status updated successfully.",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

            this.Close();
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        
       
    }
}
