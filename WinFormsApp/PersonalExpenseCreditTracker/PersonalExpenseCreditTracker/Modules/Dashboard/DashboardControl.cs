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

            this.chartExpenseCategory.Resize += (s, e) => CenterDonutLabel();
            this.pnlDonut.Resize += (s, e) => CenterDonutLabel();
        }
        private void DashboardControl_Resize(object sender, EventArgs e)
        {
            ApplyRoundCorners();
            CenterDonutLabel(); 
        }


        private void DashboardControl_Load(object sender, EventArgs e)
        {
            ApplyRoundCorners();

            int userID = LogedInUser.GetUserId();
            LoadDashboardSummary(userID);

            // Income Overview ComboBox 
            if (cmbSecondHeader.Items.Contains("This Year"))
            {
                cmbSecondHeader.SelectedItem = "This Year"; 
            }
            cmbSecondHeader.SelectedIndexChanged -= cmbSecondHeader_SelectedIndexChanged;
            cmbSecondHeader.SelectedIndexChanged += cmbSecondHeader_SelectedIndexChanged;

            // ২. Income Overview 
            LoadIncomeOverviewChart(userID, "This Year"); 

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

            if (cmbSecondHeader.Items.Contains("This Year"))
            {
                cmbSecondHeader.SelectedItem = "This Year";
            }
            cmbSecondHeader.SelectedIndexChanged -= cmbSecondHeader_SelectedIndexChanged;
            cmbSecondHeader.SelectedIndexChanged += cmbSecondHeader_SelectedIndexChanged;
            LoadIncomeOverviewChart(userID, "This Year");

            if (cmbExpenseFilter.Items.Contains("This Year"))
            {
                cmbExpenseFilter.SelectedItem = "This Year";
            }
            cmbExpenseFilter.SelectedIndexChanged -= cmbExpenseFilter_SelectedIndexChanged;
            cmbExpenseFilter.SelectedIndexChanged += cmbExpenseFilter_SelectedIndexChanged;
            LoadExpenseCategoryChart(userID, "This Year");
            this.BeginInvoke((MethodInvoker)(() => CenterDonutLabel()));
        }


        private void LoadIncomeOverviewChart(int userID, string filter = "This Year")
        {
            // =========================
            // 1. CLEAR CHART
            // =========================
            chartSecond.Series.Clear();
            chartSecond.Legends.Clear();

            // =========================
            // 2. CREATE INCOME SERIES
            // =========================
            Series seriesIncome = new Series("Income");

            // IMPORTANT:
            // Spline removed because it can overshoot below 0
            seriesIncome.ChartType = SeriesChartType.Line;
            seriesIncome.BorderWidth = 3;
            seriesIncome.Color = Color.FromArgb(16, 185, 129);
            seriesIncome.IsVisibleInLegend = false;

            // =========================
            // 3. GET CREDIT DATA
            // =========================
            DataTable dtCredit =
                CommonUiFunction.RetrieveDataForGridView(
                    "spGetAllCreditsByID",
                    userID);

            DateTime today = DateTime.Today;

            decimal maxVal = 0;

            // =========================
            // 4. THIS YEAR
            // =========================
            if (filter == "This Year")
            {
                var monthsData = new Dictionary<int, decimal>();

                for (int m = 1; m <= 12; m++)
                {
                    monthsData[m] = 0;
                }

                if (dtCredit != null &&
                    dtCredit.Rows.Count > 0 &&
                    !dtCredit.Columns.Contains("Message"))
                {
                    var creditsThisYear =
                        dtCredit.AsEnumerable()
                        .Where(r =>
                            r["CreditAt"] != DBNull.Value &&
                            Convert.ToDateTime(r["CreditAt"]).Year
                                == today.Year);

                    foreach (var group in
                        creditsThisYear.GroupBy(r =>
                            Convert.ToDateTime(r["CreditAt"]).Month))
                    {
                        if (monthsData.ContainsKey(group.Key))
                        {
                            monthsData[group.Key] =
                                group.Sum(r =>
                                    Convert.ToDecimal(r["Amount"]));
                        }
                    }
                }

                // Only January -> Current Month
                for (int m = 1; m <= today.Month; m++)
                {
                    string monthName =
                        System.Globalization.CultureInfo.CurrentCulture
                        .DateTimeFormat
                        .GetAbbreviatedMonthName(m);

                    decimal val = monthsData[m];

                    if (val > maxVal)
                    {
                        maxVal = val;
                    }

                    int pIdx =
                        seriesIncome.Points.AddXY(
                            monthName,
                            val);

                    if (val > 0)
                    {
                        seriesIncome.Points[pIdx].MarkerStyle =
                            MarkerStyle.Circle;

                        seriesIncome.Points[pIdx].MarkerSize = 7;

                        seriesIncome.Points[pIdx].MarkerColor =
                            Color.FromArgb(16, 185, 129);
                    }
                }
            }

            // =========================
            // 5. THIS MONTH
            // =========================
            else if (filter == "This Month")
            {
                int daysInMonth =
                    DateTime.DaysInMonth(
                        today.Year,
                        today.Month);

                var daysData =
                    new Dictionary<int, decimal>();

                for (int d = 1; d <= daysInMonth; d++)
                {
                    daysData[d] = 0;
                }

                if (dtCredit != null &&
                    dtCredit.Rows.Count > 0 &&
                    !dtCredit.Columns.Contains("Message"))
                {
                    var creditsThisMonth =
                        dtCredit.AsEnumerable()
                        .Where(r =>
                            r["CreditAt"] != DBNull.Value &&
                            Convert.ToDateTime(r["CreditAt"]).Month
                                == today.Month &&
                            Convert.ToDateTime(r["CreditAt"]).Year
                                == today.Year);

                    foreach (var group in
                        creditsThisMonth.GroupBy(r =>
                            Convert.ToDateTime(r["CreditAt"]).Day))
                    {
                        if (daysData.ContainsKey(group.Key))
                        {
                            daysData[group.Key] =
                                group.Sum(r =>
                                    Convert.ToDecimal(r["Amount"]));
                        }
                    }
                }

                // Only 1st -> Today
                for (int d = 1; d <= today.Day; d++)
                {
                    decimal val = daysData[d];

                    if (val > maxVal)
                    {
                        maxVal = val;
                    }

                    int pIdx =
                        seriesIncome.Points.AddXY(
                            d.ToString("D2"),
                            val);

                    if (val > 0)
                    {
                        seriesIncome.Points[pIdx].MarkerStyle =
                            MarkerStyle.Circle;

                        seriesIncome.Points[pIdx].MarkerSize = 7;

                        seriesIncome.Points[pIdx].MarkerColor =
                            Color.FromArgb(16, 185, 129);
                    }
                }
            }

            // =========================
            // 6. LAST MONTH
            // =========================
            else if (filter == "Last Month")
            {
                DateTime lastMonth =
                    today.AddMonths(-1);

                int daysInMonth =
                    DateTime.DaysInMonth(
                        lastMonth.Year,
                        lastMonth.Month);

                var daysData =
                    new Dictionary<int, decimal>();

                for (int d = 1; d <= daysInMonth; d++)
                {
                    daysData[d] = 0;
                }

                if (dtCredit != null &&
                    dtCredit.Rows.Count > 0 &&
                    !dtCredit.Columns.Contains("Message"))
                {
                    var creditsLastMonth =
                        dtCredit.AsEnumerable()
                        .Where(r =>
                            r["CreditAt"] != DBNull.Value &&
                            Convert.ToDateTime(r["CreditAt"]).Month
                                == lastMonth.Month &&
                            Convert.ToDateTime(r["CreditAt"]).Year
                                == lastMonth.Year);

                    foreach (var group in
                        creditsLastMonth.GroupBy(r =>
                            Convert.ToDateTime(r["CreditAt"]).Day))
                    {
                        if (daysData.ContainsKey(group.Key))
                        {
                            daysData[group.Key] =
                                group.Sum(r =>
                                    Convert.ToDecimal(r["Amount"]));
                        }
                    }
                }

                // Show complete previous month
                for (int d = 1; d <= daysInMonth; d++)
                {
                    decimal val = daysData[d];

                    if (val > maxVal)
                    {
                        maxVal = val;
                    }

                    int pIdx =
                        seriesIncome.Points.AddXY(
                            d.ToString("D2"),
                            val);

                    if (val > 0)
                    {
                        seriesIncome.Points[pIdx].MarkerStyle =
                            MarkerStyle.Circle;

                        seriesIncome.Points[pIdx].MarkerSize = 7;

                        seriesIncome.Points[pIdx].MarkerColor =
                            Color.FromArgb(16, 185, 129);
                    }
                }
            }

            // =========================
            // 7. ADD SERIES
            // =========================
            chartSecond.Series.Add(seriesIncome);

            // =========================
            // 8. CHART AREA
            // =========================
            chartSecond.Padding =
                new Padding(0, 0, 0, 0);

            ChartArea area =
                chartSecond.ChartAreas[0];

            area.BackColor = Color.White;
            area.BorderWidth = 0;

            area.Position =
                new ElementPosition(
                    0, 0, 100, 100);

            // Increased plot height slightly
            area.InnerPlotPosition =
                new ElementPosition(
                    14, 5, 82, 82);

            // =========================
            // 9. X AXIS
            // =========================
            area.AxisX.IsMarginVisible = true;

            area.AxisX.MajorGrid.Enabled = false;

            area.AxisX.MajorTickMark.Enabled = false;

            area.AxisX.LineColor =
                Color.FromArgb(
                    226, 232, 240);

            area.AxisX.LabelStyle.ForeColor =
                Color.FromArgb(
                    100, 116, 139);

            area.AxisX.LabelStyle.Font =
                new Font(
                    "Segoe UI",
                    6.5F,
                    FontStyle.Regular);

            area.AxisX.LabelAutoFitStyle =
                LabelAutoFitStyles.None;

            if (filter == "This Year")
            {
                // Jan, Feb, Mar...
                area.AxisX.Interval = 1;
            }
            else if (filter == "This Month")
            {
                // 01, 02, 03... আজ পর্যন্ত
                area.AxisX.Interval = 1;
            }
            else if (filter == "Last Month")
            {
                // প্রতি 5 দিন পর
                area.AxisX.Interval = 5;
            }

            // =========================
            // 10. Y AXIS
            // =========================
            area.AxisY.Minimum = 0;

            area.AxisY.IsStartedFromZero = true;

            if (maxVal > 0)
            {
                double calculatedMax =
                    (double)(maxVal * 1.25m);

                area.AxisY.Maximum =
                    Math.Ceiling(
                        calculatedMax / 10000.0)
                    * 10000.0;
            }
            else
            {
                area.AxisY.Maximum = 10000;
            }

            area.AxisY.MajorGrid.LineColor =
                Color.FromArgb(
                    241, 245, 249);

            area.AxisY.MajorGrid.LineWidth = 1;

            area.AxisY.MajorTickMark.Enabled = false;

            area.AxisY.LineColor =
                Color.Transparent;

            area.AxisY.LabelStyle.ForeColor =
                Color.FromArgb(
                    100, 116, 139);

            area.AxisY.LabelStyle.Font =
                new Font(
                    "Segoe UI",
                    7F,
                    FontStyle.Regular);

            area.AxisY.LabelStyle.Format =
                "#,##0";
        }


        private Color GetCategoryColor(string categoryName, int index)
        {
            Color[] distinctPalette = new Color[]
    {
        Color.FromArgb(239, 68, 68),   // 1. Red (লাল)
        Color.FromArgb(255, 149, 0),   // 2. Orange (কমলা)
        Color.FromArgb(0, 114, 245),   // 3. Blue (নীল)
        Color.FromArgb(16, 185, 129),  // 4. Emerald Green (সবুজ)
        Color.FromArgb(139, 92, 246),  // 5. Purple (পার্পল)
        Color.FromArgb(236, 72, 153),  // 6. Pink (গোলাপী)
        Color.FromArgb(14, 165, 233),  // 7. Sky Blue (আকাশি)
        Color.FromArgb(234, 179, 8),   // 8. Golden Yellow (হলুদ)
        Color.FromArgb(20, 184, 166),  // 9. Teal (টিয়া সবুজ)
        Color.FromArgb(244, 63, 94),   // 10. Rose Red (গোলাপী লাল)
        Color.FromArgb(132, 204, 22),  // 11. Lime Green (লেবু সবুজ)
        Color.FromArgb(168, 85, 247),  // 12. Magenta (ম্যাজেন্টা)
        Color.FromArgb(217, 119, 6),   // 13. Amber (আম্বার)
        Color.FromArgb(79, 70, 229),   // 14. Deep Indigo (গাঢ় নীল)
        Color.FromArgb(6, 182, 212),   // 15. Cyan (সায়ান)
        Color.FromArgb(162, 28, 175),  // 16. Deep Purple (গাঢ় পার্পল)
        Color.FromArgb(225, 29, 72),   // 17. Crimson (ক্রিমসন)
        Color.FromArgb(5, 150, 105),   // 18. Forest Green (গাঢ় সবুজ)
        Color.FromArgb(107, 114, 128), // 19. Slate Gray (স্লেট ধূসর)
        Color.FromArgb(30, 41, 59)     // 20. Navy Blue (নেভি ব্লু)
    };
            return distinctPalette[index % distinctPalette.Length];
        }


        private void SetExpenseCenterLabel(
    string text,
    Color textColor)
        {
            if (label3 == null)
                return;


            // ==============================
            // LABEL SETTINGS
            // ==============================
            label3.Parent = chartExpenseCategory;

            label3.BackColor = Color.Transparent;

            label3.AutoSize = false;

            label3.Size =
                new Size(130, 48);

            label3.Text = text;

            label3.Font =
                new Font(
                    "Segoe UI",
                    9.5F,
                    FontStyle.Bold);

            label3.ForeColor = textColor;

            label3.TextAlign =
                ContentAlignment.MiddleCenter;


            // ==============================
            // GET CHART AREA
            // ==============================
            ChartArea area =
                chartExpenseCategory.ChartAreas[0];


            // ==============================
            // GET CHART SIZE
            // ==============================
            float chartWidth =
                chartExpenseCategory.ClientSize.Width;

            float chartHeight =
                chartExpenseCategory.ClientSize.Height;


            // ==============================
            // CHART AREA POSITION
            // ==============================
            float areaX =
                chartWidth *
                area.Position.X / 100f;

            float areaY =
                chartHeight *
                area.Position.Y / 100f;

            float areaWidth =
                chartWidth *
                area.Position.Width / 100f;

            float areaHeight =
                chartHeight *
                area.Position.Height / 100f;


            // ==============================
            // INNER PLOT POSITION
            // ==============================
            float plotX =
                areaX +
                (areaWidth *
                area.InnerPlotPosition.X / 100f);

            float plotY =
                areaY +
                (areaHeight *
                area.InnerPlotPosition.Y / 100f);

            float plotWidth =
                areaWidth *
                area.InnerPlotPosition.Width / 100f;

            float plotHeight =
                areaHeight *
                area.InnerPlotPosition.Height / 100f;


            // ==============================
            // ACTUAL DOUGHNUT CENTER
            // ==============================
            int centerX =
                (int)(
                    plotX +
                    (plotWidth / 2f) -
                    (label3.Width / 2f));

            int centerY =
                (int)(
                    plotY +
                    (plotHeight / 2f) -
                    (label3.Height / 2f));


            // ==============================
            // SET LABEL LOCATION
            // ==============================
            label3.Location =
                new Point(
                    centerX,
                    centerY);

            label3.BringToFront();
        }

        // Top 5 table
        private void UpdateTop5ExpenseTable(dynamic categoryList, decimal totalExpense)
        {
            var rowControls = new[]
            {
                new { Panel = pnlFoodColorr, LblName = lblFood, LblAmt = lblFoodAmount, LblPct = lblFoodPercentt, Flp = flpFood },
                new { Panel = pnlShoppingColorr, LblName = lblShopping, LblAmt = lblShopingAmount, LblPct = lblShopingPercentt, Flp = flpShopping },
                new { Panel = pnlTransportColorr, LblName = lblTransport, LblAmt = lblTransportAmount, LblPct = lblTransportPercentt, Flp = flowLayoutPanel1 },
                new { Panel = pnlBillsColorr, LblName = lblBill, LblAmt = lblBillAmount, LblPct = lblBillPercentt, Flp = flowLayoutPanel2 },
                new { Panel = pnlEnterColorr, LblName = label1, LblAmt = lblEnterAmount, LblPct = lblEnterPercentt, Flp = flowLayoutPanel3 }
            };

            for (int i = 0; i < 5; i++)
            {
                var ctrl = rowControls[i];
                if (i < categoryList.Count)
                {
                    var item = categoryList[i];
                    ctrl.Panel.Visible = true;
                    ctrl.Panel.BackColor = GetCategoryColor(item.CategoryName, i);
                    ctrl.LblName.Text = item.CategoryName;
                    ctrl.LblAmt.Text = " ₹" + item.TotalAmount.ToString("#,##0");

                    int pct = totalExpense > 0 ? (int)Math.Round((item.TotalAmount / totalExpense) * 100) : 0;
                    ctrl.LblPct.Text = pct + "%";
                }
                else
                {
                    ctrl.Panel.Visible = false;
                    ctrl.LblName.Text = "-";
                    ctrl.LblAmt.Text = " ₹0";
                    ctrl.LblPct.Text = "0%";
                }
            }
        }

        private void LoadExpenseCategoryChart(int userID, string filter = "This Year")
        {
            chartExpenseCategory.Series[0].Points.Clear();
            chartExpenseCategory.Series[0].ChartType = SeriesChartType.Doughnut;
            chartExpenseCategory.Series[0]["DoughnutRadius"] = "58";
            chartExpenseCategory.Series[0]["PieDrawingStyle"] = "Default"; 
            chartExpenseCategory.Series[0].BorderColor = Color.White;
            chartExpenseCategory.Series[0].BorderWidth = 2;
            chartExpenseCategory.Legends[0].Enabled = false;
            DataTable dtExpense = CommonUiFunction.RetrieveDataForGridView("spGetAllExpensesByID", userID);
            DateTime today = DateTime.Today;
            IEnumerable<DataRow> filteredRows = Enumerable.Empty<DataRow>();
            if (dtExpense != null && dtExpense.Rows.Count > 0 && !dtExpense.Columns.Contains("Message"))
            {
                if (filter == "This Month")
                {
                    filteredRows = dtExpense.AsEnumerable()
                        .Where(r => r["ExpenseAt"] != DBNull.Value &&
                                    Convert.ToDateTime(r["ExpenseAt"]).Month == today.Month &&
                                    Convert.ToDateTime(r["ExpenseAt"]).Year == today.Year);
                }
                else if (filter == "Last Month")
                {
                    DateTime lastMonth = today.AddMonths(-1);
                    filteredRows = dtExpense.AsEnumerable()
                        .Where(r => r["ExpenseAt"] != DBNull.Value &&
                                    Convert.ToDateTime(r["ExpenseAt"]).Month == lastMonth.Month &&
                                    Convert.ToDateTime(r["ExpenseAt"]).Year == lastMonth.Year);
                }
                else // This Year
                {
                    filteredRows = dtExpense.AsEnumerable()
                        .Where(r => r["ExpenseAt"] != DBNull.Value &&
                                    Convert.ToDateTime(r["ExpenseAt"]).Year == today.Year);
                }
            }
            var categoryList = filteredRows
                .Where(r => r["CategoryName"] != DBNull.Value &&
                            !string.IsNullOrWhiteSpace(r["CategoryName"].ToString()) &&
                            r["Amount"] != DBNull.Value &&
                            Convert.ToDecimal(r["Amount"]) > 0)
                .GroupBy(r => r["CategoryName"].ToString().Trim())
                .Select(g => new
                {
                    CategoryName = g.Key,
                    TotalAmount = g.Sum(r => Convert.ToDecimal(r["Amount"]))
                })
                .Where(c => c.TotalAmount > 0)
                .OrderByDescending(c => c.TotalAmount)
                .ToList();
            decimal totalExpense = categoryList.Sum(c => c.TotalAmount);

            UpdateTop5ExpenseTable(categoryList, totalExpense);

            if (categoryList.Count > 0 && totalExpense > 0)
            {
                chartExpenseCategory.Series[0].IsValueShownAsLabel = false; 
                chartExpenseCategory.Series[0].Label = "";
                chartExpenseCategory.Series[0].Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                chartExpenseCategory.Series[0].LabelForeColor = Color.White;
                for (int i = 0; i < categoryList.Count; i++)
                {
                    var item = categoryList[i];
                    int pIdx = chartExpenseCategory.Series[0].Points.AddXY(item.CategoryName, item.TotalAmount);
                    var point = chartExpenseCategory.Series[0].Points[pIdx];
                    point.Color = GetCategoryColor(item.CategoryName, i);
                    double pct = ((double)item.TotalAmount / (double)totalExpense) * 100.0;
                    int displayPercent = (int)Math.Round(pct);
                
                    if (displayPercent >= 1)
                    {
                        point.Label = displayPercent.ToString() + "%";
                    }
                    else
                    {
                        point.Label = " "; 
                    }
                    point.ToolTip = string.Format("{0}: ₹{1:#,##0} ({2:0.0}%)", item.CategoryName, item.TotalAmount, pct);
                }
                if (label3 != null)
                {
                    label3.Parent = chartExpenseCategory;
                    label3.BackColor = Color.Transparent;
                    label3.AutoSize = false;
                    label3.Size = new Size(130, 48);
                    label3.Text = string.Format("₹{0:#,##0}\nTotal Expenses", totalExpense);
                    label3.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    label3.ForeColor = Color.FromArgb(30, 41, 59);
                    label3.TextAlign = ContentAlignment.MiddleCenter;
                    CenterDonutLabel();
                }
            }
            else
            {
                int pIdx = chartExpenseCategory.Series[0].Points.AddXY("No Expense", 1);
                chartExpenseCategory.Series[0].Points[pIdx].Color = Color.FromArgb(226, 232, 240);
                chartExpenseCategory.Series[0].Points[pIdx].Label = "";
                if (label3 != null)
                {
                    label3.Parent = chartExpenseCategory;
                    label3.BackColor = Color.Transparent;
                    label3.AutoSize = false;
                    label3.Size = new Size(130, 48);
                    label3.Text = "₹0\nTotal Expenses";
                    label3.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    label3.ForeColor = Color.FromArgb(100, 116, 139);
                    label3.TextAlign = ContentAlignment.MiddleCenter;
                    CenterDonutLabel();
                }
            }
        }



        private void CenterDonutLabel()
        {
            if (label3 != null && chartExpenseCategory != null && chartExpenseCategory.Width > 0 && chartExpenseCategory.Height > 0)
            {
                label3.Location = new Point(
                    (chartExpenseCategory.Width - label3.Width) / 2,
                    (chartExpenseCategory.Height - label3.Height) / 2
                );
                label3.BringToFront();
            }
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

            string currentFilter = "This Year";
            if (cmbSecondHeader != null && cmbSecondHeader.SelectedItem != null)
            {
                currentFilter = cmbSecondHeader.SelectedItem.ToString();
            }
            LoadIncomeOverviewChart(userID, currentFilter);


            string expenseFilter = "This Year";
            if (cmbExpenseFilter != null && cmbExpenseFilter.SelectedItem != null)
            {
                expenseFilter = cmbExpenseFilter.SelectedItem.ToString();
            }
            LoadExpenseCategoryChart(userID, expenseFilter);
        }

        private void cmbSecondHeader_SelectedIndexChanged(object sender, EventArgs e)
        {
            int userID = LogedInUser.GetUserId();
            string filter = "This Year";

           
            switch (cmbSecondHeader.SelectedIndex)
            {
                case 0:
                    filter = "This Month";
                    break;
                case 1:
                    filter = "Last Month";
                    break;
                case 2:
                    filter = "This Year";
                    break;
            }

          
            LoadIncomeOverviewChart(userID, filter);
        }

        private void cmbExpenseFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            int userID = LogedInUser.GetUserId();
            string filter = "This Year";

            switch (cmbExpenseFilter.SelectedIndex)
            {
                case 0:
                    filter = "This Month";
                    break;
                case 1:
                    filter = "Last Month";
                    break;
                case 2:
                    filter = "This Year";
                    break;
            }

            LoadExpenseCategoryChart(userID, filter);
        }




    }
}
