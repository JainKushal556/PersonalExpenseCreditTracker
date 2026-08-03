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
using PersonalExpenseCreditTracker.Common;
using PersonalExpenseCreditTracker.Session;

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

             int userID = LogedInUser.GetUserId();
             LoadDashboardSummary(userID);

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


        public void LoadDashboardSummary(int userID)
        {
            //Total Expense 
            DataTable dtExpense = CommonUiFunction.RetrieveDataForGridView("spGetAllExpensesByID", userID);

            decimal totalExpense = 0;
            if (dtExpense != null && dtExpense.Rows.Count > 0 && !dtExpense.Columns.Contains("Message"))
            {
                totalExpense = dtExpense.AsEnumerable().Sum(r => Convert.ToDecimal(r["Amount"]));
            }

            lblExpenseAmount.Text = "₹ " + totalExpense.ToString("#,##0");
           lblExpenseValue.Text = "₹ " + totalExpense.ToString("#,##0");

           //Total Credit (Income)
           DataTable dtCredit = CommonUiFunction.RetrieveDataForGridView("spGetAllCreditsByID", userID);
           decimal totalCredit = 0;
           decimal amt = 0;

           if (dtCredit != null &&
               dtCredit.Rows.Count > 0 &&
               !dtCredit.Columns.Contains("Message") &&
               dtCredit.Columns.Contains("Amount"))
           {
               foreach (DataRow r in dtCredit.Rows)
               {
                   if (r["Amount"] != DBNull.Value &&
                       decimal.TryParse(r["Amount"].ToString(), out amt))
                   {
                       totalCredit += amt;
                   }
               }
           }

           lblCreditAmount.Text = "₹ " + totalCredit.ToString("#,##0");
           lblIncomeValue.Text = "₹ " + totalCredit.ToString("#,##0");


            // Total Borrow 
            DataTable dtBorrow = CommonUiFunction.RetrieveDataForGridView("spGetAllBorrow", userID);
            decimal totalBorrow = 0;
            if (dtBorrow != null && dtBorrow.Rows.Count > 0 && !dtBorrow.Columns.Contains("Message"))
            {
                totalBorrow = dtBorrow.AsEnumerable().Sum(r => Convert.ToDecimal(r["Amount"]));
            }
           lblBorrowAmount.Text = "₹ " + totalBorrow.ToString("#,##0");
            lblBorrowValue.Text = "₹ " + totalBorrow.ToString("#,##0");

            // Total Lent 
            DataTable dtLent = CommonUiFunction.RetrieveDataForGridView("spGetAllLent", userID);
            decimal totalLent = 0;
            if (dtLent != null && dtLent.Rows.Count > 0 && !dtLent.Columns.Contains("Message"))
            {
                totalLent = dtLent.AsEnumerable().Sum(r => Convert.ToDecimal(r["Amount"]));
            }
           lblLentAmount.Text = "₹ " + totalLent.ToString("#,##0");
            lblLentValue.Text = "₹ " + totalLent.ToString("#,##0");

            //Net Balance (Income - Expense)
            decimal netBalance = totalCredit - totalExpense;

            if (netBalance < 0)
            {
                lblNetBalanceValue.Text = "-₹ " + Math.Abs(netBalance).ToString("#,##0");
                lblNetBalanceValue.ForeColor = Color.Red;
            }
            else if (netBalance > 0)
            {
                lblNetBalanceValue.Text = "₹ " + netBalance.ToString("#,##0");
                lblNetBalanceValue.ForeColor = Color.Green;
            }
            else
            {
                lblNetBalanceValue.Text = "₹ 0";
                lblNetBalanceValue.ForeColor = Color.Black;
            }

        }


    }
}
