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
    public partial class DeleteTask : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);

        public DeleteTask()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

            this.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    this.Width,
                    this.Height,
                    10,
                    10));
        }

        private void DeleteTaskControl_Load(object sender, EventArgs e)
        {
           
            btnCancel.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, btnCancel.Width, btnCancel.Height, 8, 8));

            btnUpdateTask.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, btnUpdateTask.Width, btnUpdateTask.Height, 8, 8));
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
            btnClose.BackColor = Color.White;
            btnClose.ForeColor = Color.Black;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
