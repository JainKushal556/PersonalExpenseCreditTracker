using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Forms.DataVisualization.Charting;

namespace PersonalExpenseCreditTracker.Modules.Dashboard
{
    public partial class DashboardControl : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);

        public DashboardControl()
        {
            InitializeComponent();
            ApplyRoundCorners();
            this.Resize += DashboardControl_Resize;
        }
        private void DashboardControl_Resize(object sender, EventArgs e)
        {
            ApplyRoundCorners();
        }
        private void DashboardControl_Load(object sender, EventArgs e)
        {
            ApplyRoundCorners();

            chartExpenseCategory.Series[0].Points.Clear();

            chartExpenseCategory.Series[0].Points.AddXY("Food & Dining", 8690);
            chartExpenseCategory.Series[0].Points.AddXY("Shopping", 6210);
            chartExpenseCategory.Series[0].Points.AddXY("Transport", 3730);
            chartExpenseCategory.Series[0].Points.AddXY("Bills & Utilities", 3730);
            chartExpenseCategory.Series[0].Points.AddXY("Entertainment", 2490);

            chartExpenseCategory.Series[0].ChartType = SeriesChartType.Doughnut;

            chartExpenseCategory.Series[0]["DoughnutRadius"] = "60";

            chartExpenseCategory.Series[0].IsValueShownAsLabel = true;
            chartExpenseCategory.Series[0].Label = "#PERCENT{P0}";

            chartExpenseCategory.Legends[0].Enabled = false;

           //Second Chart
            chartSecond.Series.Clear();

            Series s = new Series("Income");

            s.ChartType = SeriesChartType.Spline;
            s.BorderWidth = 3;
            s.MarkerStyle = MarkerStyle.Circle;
            s.MarkerSize = 7;
            s.Color = Color.FromArgb(16, 185, 129);

            s.Points.AddXY("May 1", 10);
            s.Points.AddXY("May 6", 28);
            s.Points.AddXY("May 10", 22);
            s.Points.AddXY("May 15", 45);
            s.Points.AddXY("May 20", 55);
            s.Points.AddXY("May 25", 48);
            s.Points.AddXY("May 31", 72);

            chartSecond.Series.Add(s);

            chartSecond.ChartAreas[0].BackColor = Color.Transparent;
            chartSecond.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartSecond.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gainsboro;

            chartSecond.Legends.Clear();

            chartSecond.ChartAreas[0].BackColor = Color.White;

            chartSecond.ChartAreas[0].BorderWidth = 0;

            chartSecond.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            chartSecond.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(235, 235, 235);

            chartSecond.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
            chartSecond.ChartAreas[0].AxisX.LineColor = Color.LightGray;
            chartSecond.ChartAreas[0].AxisY.LineColor = Color.LightGray;

            chartSecond.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
            chartSecond.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;

            chartSecond.ChartAreas[0].AxisX.LabelStyle.Font =
                new Font("Segoe UI", 6);

            chartSecond.ChartAreas[0].AxisY.LabelStyle.Font =
                new Font("Segoe UI", 6);

            s.BorderWidth = 3;

            s.MarkerStyle = MarkerStyle.Circle;

            s.MarkerSize = 6;

            s.IsVisibleInLegend = false;
            s.Color = Color.FromArgb(64, 192, 165);
        }

        private void ApplyRoundCorners()
        {
            pnlExpenseCard.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pnlExpenseCard.Width, pnlExpenseCard.Height, 15, 15));

            pnlCreditCard.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pnlCreditCard.Width, pnlCreditCard.Height, 15, 15));

            pnlCardBorrowCard.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pnlCardBorrowCard.Width, pnlCardBorrowCard.Height, 15, 15));

            pnlCardLentCard.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pnlCardLentCard.Width, pnlCardLentCard.Height, 15, 15));
        }

        private void pnlExpenseCard_Resize(object sender, EventArgs e)
        {
            ApplyRoundCorners();
        }

        private void pnlCreditCard_Resize(object sender, EventArgs e)
        {
            ApplyRoundCorners();
        }

        private void pnlCardBorrowCard_Resize(object sender, EventArgs e)
        {
            ApplyRoundCorners();
        }

        private void pnlCardLentCard_Resize(object sender, EventArgs e)
        {
            ApplyRoundCorners();
        }

        private void chartExpenseCategory_Click(object sender, EventArgs e)
        {

        }

        private void tblCardExpense_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblExpenseAmount_Click(object sender, EventArgs e)
        {

        }

        private void pnlExpenseCard_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlExpenseCard.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void pnlCreditCard_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlCreditCard.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void pnlCardBorrowCard_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlCardBorrowCard.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void pnlCardLentCard_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlCardLentCard.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void pnlCardExpense_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlCardExpense.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void pnlCardIncome_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlCardIncome.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void pnlThird_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlThird.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

    }
}
