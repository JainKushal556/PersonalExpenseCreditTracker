using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalExpenseCreditTracker.Modules.Lent
{
    public partial class LentControls : Form
    {
        public LentControls()
        {
            InitializeComponent();
        }

        private void btnExportReport_Click(object sender, EventArgs e)
        {

        }

        private void btnExportReport_MouseEnter(object sender, EventArgs e)
        {
            btnExportReport.BackColor = Color.FromArgb(0, 0, 240);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        //private void btnExportReport_MouseLeave(object sender, EventArgs e)
        //{
        //    btnExportReport.BackColor = Color.FromArgb(212, 212, 255);
        //}
    }
}
