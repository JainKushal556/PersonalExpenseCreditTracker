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
using System.Threading.Tasks;


namespace PersonalExpenseCreditTracker.Modules.Dashboard
{
    public partial class DashboardControl : Form
    {

     
        private static HashSet<string> readNotifications = new HashSet<string>();

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
            cmbSecondHeader.TabStop = false;
            cmbExpenseFilter.TabStop = false;
            cmbSecondHeader.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbExpenseFilter.DropDownStyle = ComboBoxStyle.DropDownList;
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
            pnlNotification.Visible = false;
            ApplyRoundCorners();

            // 1. Setup Dropdown Defaults & Events
            if (cmbSecondHeader.Items.Contains("This Year"))
            {
                cmbSecondHeader.SelectedItem = "This Year";
            }
            cmbSecondHeader.SelectedIndexChanged -= cmbSecondHeader_SelectedIndexChanged;
            cmbSecondHeader.SelectedIndexChanged += cmbSecondHeader_SelectedIndexChanged;

            if (cmbExpenseFilter.Items.Contains("This Year"))
            {
                cmbExpenseFilter.SelectedItem = "This Year";
            }
            cmbExpenseFilter.SelectedIndexChanged -= cmbExpenseFilter_SelectedIndexChanged;
            cmbExpenseFilter.SelectedIndexChanged += cmbExpenseFilter_SelectedIndexChanged;

            // 2. Load Dashboard Summary, Charts & Totals once
            int userID = LogedInUser.GetUserId();
            LoadDashboardSummary(userID);

            // 3. Load Notifications
            lblNotification.Text = "🔔  Notifications";
            lblTitle.Text = "🔔  Notifications";
            LoadNotifications(userID, pnlExtra, flpNotifications);

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
            
            // Clean up the data labels
            seriesIncome.Font = new Font("Segoe UI", 8F);
            seriesIncome.SmartLabelStyle.Enabled = true;
            seriesIncome.SmartLabelStyle.CalloutLineColor = Color.Transparent;

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

                        seriesIncome.Points[pIdx].IsValueShownAsLabel = true;
                        seriesIncome.Points[pIdx].LabelFormat = "₹ #,##0";
                        seriesIncome.Points[pIdx].LabelForeColor = Color.DimGray;
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

                var daysData = new Dictionary<int, decimal>();

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

                        seriesIncome.Points[pIdx].IsValueShownAsLabel = true;
                        seriesIncome.Points[pIdx].LabelFormat = "₹ #,##0";
                        seriesIncome.Points[pIdx].LabelForeColor = Color.DimGray;
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

                        seriesIncome.Points[pIdx].IsValueShownAsLabel = true;
                        seriesIncome.Points[pIdx].LabelFormat = "₹ #,##0";
                        seriesIncome.Points[pIdx].LabelForeColor = Color.DimGray;
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
                // 01, 02, 03... Up to today
                area.AxisX.Interval = 1;
            }
            else if (filter == "Last Month")
            {
                // Every 5 days
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
        Color.FromArgb(239, 68, 68),   // 1. Red
        Color.FromArgb(255, 149, 0),   // 2. Orange
        Color.FromArgb(0, 114, 245),   // 3. Blue
        Color.FromArgb(16, 185, 129),  // 4. Emerald Green
        Color.FromArgb(139, 92, 246),  // 5. Purple
        Color.FromArgb(236, 72, 153),  // 6. Pink
        Color.FromArgb(14, 165, 233),  // 7. Sky Blue
        Color.FromArgb(234, 179, 8),   // 8. Golden Yellow
        Color.FromArgb(20, 184, 166),  // 9. Teal
        Color.FromArgb(244, 63, 94),   // 10. Rose Red
        Color.FromArgb(132, 204, 22),  // 11. Lime Green
        Color.FromArgb(168, 85, 247),  // 12. Magenta
        Color.FromArgb(217, 119, 6),   // 13. Amber
        Color.FromArgb(79, 70, 229),   // 14. Deep Indigo
        Color.FromArgb(6, 182, 212),   // 15. Cyan
        Color.FromArgb(162, 28, 175),  // 16. Deep Purple
        Color.FromArgb(225, 29, 72),   // 17. Crimson
        Color.FromArgb(5, 150, 105),   // 18. Forest Green
        Color.FromArgb(107, 114, 128), // 19. Slate Gray
        Color.FromArgb(30, 41, 59)     // 20. Navy Blue
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

                    double exactPct = totalExpense > 0 ? (double)(item.TotalAmount / totalExpense) * 100.0 : 0;
                    string pctText = "0%";
                    if (exactPct >= 1.0)
                    {
                        pctText = Math.Round(exactPct).ToString() + "%";
                    }
                    else if (exactPct > 0)
                    {
                        pctText = exactPct.ToString("0.0") + "%";
                    }
                    ctrl.LblPct.Text = pctText;
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
                bool addedAny = false;
                for (int i = 0; i < categoryList.Count; i++)
                {
                    var item = categoryList[i];
                    double pct = ((double)item.TotalAmount / (double)totalExpense) * 100.0;
                    if (pct >= 1.0)
                    {
                        string pctText = Math.Round(pct).ToString() + "%";
                        
                        int pIdx = chartExpenseCategory.Series[0].Points.AddXY(item.CategoryName, item.TotalAmount);
                        var point = chartExpenseCategory.Series[0].Points[pIdx];
                        point.Color = GetCategoryColor(item.CategoryName, i);
                        point.Label = pctText;
                        point.ToolTip = string.Format("{0}: ₹{1:#,##0} ({2})", item.CategoryName, item.TotalAmount, pctText);
                        addedAny = true;
                    }
                }

                if (!addedAny)
                {
                    int pIdx = chartExpenseCategory.Series[0].Points.AddXY("Other", 1);
                    chartExpenseCategory.Series[0].Points[pIdx].Color = Color.FromArgb(226, 232, 240);
                    chartExpenseCategory.Series[0].Points[pIdx].Label = "";
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

            // Automatically reload Notifications (Budget Alert, Credit Ratio, etc.)
            lblNotification.Text = "🔔  Notifications";
            lblTitle.Text = "🔔  Notifications";
            LoadNotifications(userID, pnlExtra, flpNotifications);
            if (pnlNotification != null && flowLayoutPanel5 != null)
            {
                LoadNotifications(userID, pnlNotification, flowLayoutPanel5);
            }
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
            //this.BeginInvoke((MethodInvoker)(() => this.ActiveControl = null;));
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
            //this.BeginInvoke((MethodInvoker)(() => this.ActiveControl = null;));
        }

        internal protected void LoadNotifications(int userID, Panel pnl, FlowLayoutPanel flpNotifications)
        {
            if (pnl == null || flpNotifications == null) return;

            
            string cleanExpense = lblExpenseAmount.Text.Replace("₹", "").Replace(",", "").Trim();
            string cleanCredit = lblCreditAmount.Text.Replace("₹", "").Replace(",", "").Trim();

            System.Threading.ThreadPool.QueueUserWorkItem(state =>
            {
                List<NotificationItem> notifList = new List<NotificationItem>();
                try
                {
                    decimal totalExpenseAmount = 0m;
                    decimal totalCreditAmount = 0m;

                    decimal.TryParse(cleanExpense, out totalExpenseAmount);
                    decimal.TryParse(cleanCredit, out totalCreditAmount);

                    if (totalCreditAmount > 0)
                    {
                        int usedPercentage = (int)Math.Round((totalExpenseAmount / totalCreditAmount) * 100m);
                        string displayPercentageText = usedPercentage > 100 ? ">100%" : usedPercentage + "%";

                        // 1. Budget Alert (if 50% or more is spent)
                        if (usedPercentage >= 50)
                        {
                            notifList.Add(new NotificationItem
                            {
                                Title = "Budget Alert",
                                Description = "You've used " + displayPercentageText + " of your monthly income/budget.",
                                TimeText = "This Month",
                                ThemeColor = Color.FromArgb(168, 85, 247),
                                BgColor = Color.FromArgb(250, 245, 255),
                                IconSymbol = "📊",
                                TargetModule = "Expense"
                            });
                        }

                        // 👉 2. Credit Expense Ratio
                        notifList.Add(new NotificationItem
                        {
                            Title = "Credit Expense Ratio",
                            Description = "Expense is " + displayPercentageText + " (₹" + totalExpenseAmount.ToString("#,##0") + ") of total credit (₹" + totalCreditAmount.ToString("#,##0") + ").",
                            TimeText = displayPercentageText + " Spent",
                        
                            ThemeColor = usedPercentage > 80 ? Color.FromArgb(239, 68, 68) : (usedPercentage > 50 ? Color.FromArgb(249, 115, 22) : Color.FromArgb(16, 185, 129)),
                            BgColor = usedPercentage > 80 ? Color.FromArgb(254, 242, 242) : (usedPercentage > 50 ? Color.FromArgb(255, 247, 237) : Color.FromArgb(236, 253, 245)),
                            IconSymbol = "💳",
                            TargetModule = "Expense"
                        });
                    }


                    
                    DataTable dtBorrow = CommonUiFunction.RetrieveDataForGridView("spGetUpcomingBorrowReminders", userID);
                    DataTable dtLent = CommonUiFunction.RetrieveDataForGridView("spGetUpcomingLentReminders", userID);
                    DataTable dtTask = CommonUiFunction.RetrieveDataForGridView("spGetUpcomingTaskReminders", userID);

                    FindOverdueAndDeadlineReminder(dtBorrow, notifList, "Borrow", false);
                    FindOverdueAndDeadlineReminder(dtLent, notifList, "Lent", false);
                    FindOverdueAndDeadlineReminder(dtTask, notifList, "Task", true);


                }
                catch { }

                notifList.RemoveAll(x => readNotifications.Contains(x.Title + "_" + x.Description));
                this.BeginInvoke((MethodInvoker)delegate
                {
                    flpNotifications.SuspendLayout();
                    try
                    {
                    
                        if (pnl == pnlNotification)
                        {
                            if (panel11 != null)
                            {
                                panel11.BringToFront();
                                if (btnClose != null)
                                {
                                    btnClose.Location = new Point(panel11.Width - 40, 5);
                                    btnClose.BringToFront();
                                }
                            }


                            flpNotifications.Padding = new Padding(6, 42, 6, 30);
                        }
                        else
                        {

                            flpNotifications.Padding = new Padding(6, 4, 6, 20);
                        }


                        flpNotifications.Controls.Clear();

                        int cardWidth = 355;

                        if (notifList.Count == 0)
                        {
                            Panel emptyPanel = new Panel
                            {
                                Size = new Size(cardWidth, 60),
                                BackColor = Color.White
                            };

                            Label lblEmpty = new Label
                            {
                                Text = "🔔  No new notifications",
                                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                                ForeColor = Color.FromArgb(100, 116, 139),
                                TextAlign = ContentAlignment.MiddleCenter,
                                Dock = DockStyle.Fill
                            };

                            emptyPanel.Controls.Add(lblEmpty);
                            flpNotifications.Controls.Add(emptyPanel);
                        }
                        else
                        {
        
                            foreach (var item in notifList)
                            {
                                Panel card = CreateNotificationCardItem(item, cardWidth);
                                flpNotifications.Controls.Add(card);
                            }

                            Panel bottomSpacer = new Panel
                            {
                                Size = new Size(cardWidth, 45),
                                BackColor = Color.Transparent
                            };
                            flpNotifications.Controls.Add(bottomSpacer);
                        }


                        int headerHeight = 44;
                        int cardHeight = 69;
                        int totalHeight = headerHeight + (notifList.Count * cardHeight) + 10;

                        if (notifList.Count == 0)
                        {
                            totalHeight = 115;
                        }

                        int maxHeight = 480;
                        if (pnl.Parent != null)
                        {
                            maxHeight = pnl.Parent.ClientSize.Height - 80;
                        }

                        pnl.Height = Math.Min(totalHeight, maxHeight);

                        MainForm mainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
                        if (mainForm != null)
                        {
                            mainForm.UpdateNotificationBadge(notifList.Count);
                        }

                    }
                    finally
                    {
                        flpNotifications.ResumeLayout(true);
                    }
                });

                   
            });
        }

        private static string GetDaysLeft(DateTime deadline)
        {
            DateTime today = DateTime.Today;

            TimeSpan difference = deadline.Date - today.Date;
            int daysLeft = difference.Days;

            if (daysLeft == 1)
            {
                return "Due tomorrow";
            }
            else if (daysLeft > 0)
            {
                return string.Format("{0} day{1} left", daysLeft, daysLeft == 1 ? "" : "s");
            }
            else if (daysLeft == 0)
            {
                return "Due today";
            }
            
            else
            {
                int daysOverdue = Math.Abs(daysLeft);
                return string.Format("{0} day{1}", daysOverdue, daysOverdue == 1 ? "" : "s");
            }
        }

        private void FindOverdueAndDeadlineReminder(DataTable dt, List<NotificationItem> notifList, string targetModule, bool check)
        {
            if (!check)
            {
                if (dt != null && dt.Rows.Count > 0 && !dt.Columns.Contains("Message"))
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        if (!dt.Columns.Contains("StatusName") || r["StatusName"] == DBNull.Value || r["StatusName"].ToString() != "Overdue")
                        {
                            continue;
                        }

                        string person = r["PersonName"] != DBNull.Value ? r["PersonName"].ToString() : "Person";
                        string amt = r["Amount"] != DBNull.Value ? Convert.ToDecimal(r["Amount"]).ToString("#,##0") : "0";
                        
                        notifList.Add(new NotificationItem
                        {
                            Title = "Payment Overdue",
                            Description = person + "'s payment of ₹" + amt + " is overdue.",
                            TimeText = "",
                            ThemeColor = Color.FromArgb(239, 68, 68),
                            BgColor = Color.FromArgb(254, 242, 242),
                            IconSymbol = "📌",
                            TargetModule = targetModule
                        });
                    }
                }

                if (dt != null && dt.Rows.Count > 0 && !dt.Columns.Contains("Message"))
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        if (!dt.Columns.Contains("StatusName") || r["StatusName"] == DBNull.Value || r["StatusName"].ToString() != "Pending" || r["StatusName"].ToString() != "Pending")
                        {
                            continue;
                        }

                        string person = r["PersonName"] != DBNull.Value ? r["PersonName"].ToString() : "Person";
                        string amt = r["Amount"] != DBNull.Value ? Convert.ToDecimal(r["Amount"]).ToString("#,##0") : "0";
                        DateTime deadlineDate = r["DeadlineAt"] != DBNull.Value ? Convert.ToDateTime(r["DeadlineAt"]) : DateTime.Today;
                        notifList.Add(new NotificationItem
                        {
                            Title = "Deadline Approaching",
                            Description = person + "'s " + targetModule.ToLower() + " payment deadline is coming.",
                            TimeText = GetDaysLeft(deadlineDate),
                            ThemeColor = Color.FromArgb(249, 115, 22),
                            BgColor = Color.FromArgb(255, 247, 237),
                            IconSymbol = "⏰",
                            TargetModule = targetModule
                        });
                    }
                }
            }
            else
            {
                if (dt != null && dt.Rows.Count > 0 && !dt.Columns.Contains("Message"))
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        if (!dt.Columns.Contains("TaskStatusName") || r["TaskStatusName"] == DBNull.Value || r["TaskStatusName"].ToString() != "Overdue")
                        {
                            continue;
                        }

                        string taskTitle = r["TaskTitle"] != DBNull.Value ? r["TaskTitle"].ToString() : "Task";
                        string priority = dt.Columns.Contains("PriorityName") && r["PriorityName"] != DBNull.Value ? r["PriorityName"].ToString() : "Normal";
                        notifList.Add(new NotificationItem
                        {
                            Title = "Task Deadline",
                            Description = "Task: '" + taskTitle + "' (" + priority + ") deadline is due.",
                            TimeText = "",
                            ThemeColor = Color.FromArgb(59, 130, 246),
                            BgColor = Color.FromArgb(239, 246, 255),
                            IconSymbol = "📝",
                            TargetModule = targetModule
                        });
                    }
                }

                if (dt != null && dt.Rows.Count > 0 && !dt.Columns.Contains("Message"))
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        if (!dt.Columns.Contains("TaskStatusName") || r["TaskStatusName"] == DBNull.Value || r["TaskStatusName"].ToString() != "Pending" || r["TaskStatusName"].ToString() != "Pending")
                        {
                            continue;
                        }

                        string taskTitle = r["TaskTitle"] != DBNull.Value ? r["TaskTitle"].ToString() : "Task";
                        DateTime deadlineDate = r["Deadline"] != DBNull.Value ? Convert.ToDateTime(r["Deadline"]) : DateTime.Today;

                        notifList.Add(new NotificationItem
                        {
                            Title = "Deadline Approaching",
                            Description = "Task: '" + taskTitle + "' deadline is coming soon.",
                            TimeText = GetDaysLeft(deadlineDate),
                            ThemeColor = Color.FromArgb(249, 115, 22),
                            BgColor = Color.FromArgb(255, 247, 237),
                            IconSymbol = "⏰",
                            TargetModule = targetModule
                        });
                    }
                }
            }
        }


        private Panel CreateNotificationCardItem(NotificationItem item, int width)
        {
           
            Panel card = new Panel
            {
                Size = new Size(width, 66),
                Margin = new Padding(0, 0, 0, 3), 
                BackColor = Color.White
            };

   
            Panel pnlLeftAccent = new Panel
            {
                Size = new Size(3, 58),
                Location = new Point(0, 4),
                BackColor = item.ThemeColor
            };
            card.Controls.Add(pnlLeftAccent);

            Label lblIconCircle = new Label
            {
                Text = item.IconSymbol,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = item.ThemeColor,
                BackColor = item.BgColor,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(34, 34),
                Location = new Point(10, 15)
            };
            try
            {
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(0, 0, lblIconCircle.Width, lblIconCircle.Height);
                lblIconCircle.Region = new Region(path);
            }
            catch { }
            card.Controls.Add(lblIconCircle);

          
            Label lblTitle = new Label
            {
                Text = item.Title,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                AutoSize = true,
                Location = new Point(52, 10)
            };
            card.Controls.Add(lblTitle);

          
            Label lblTime = new Label
            {
                Text = item.TimeText,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = item.ThemeColor,
                AutoSize = true,
                Location = new Point(width - 92, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            card.Controls.Add(lblTime);

          
            Label lblDesc = new Label
            {
                Text = item.Description,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                Size = new Size(width - 65, 30),
                Location = new Point(52, 32)
            };
            card.Controls.Add(lblDesc);

          
            Panel pnlLine = new Panel
            {
                Height = 1,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(245, 247, 250)
            };
            card.Controls.Add(pnlLine);

            card.Cursor = Cursors.Hand;
            lblIconCircle.Cursor = Cursors.Hand;
            lblTitle.Cursor = Cursors.Hand;
            lblTime.Cursor = Cursors.Hand;
            lblDesc.Cursor = Cursors.Hand;
            pnlLeftAccent.Cursor = Cursors.Hand;

            EventHandler onHover = (s, e) => card.BackColor = Color.FromArgb(248, 250, 252);
            EventHandler onLeave = (s, e) => card.BackColor = Color.White;

            card.MouseEnter += onHover;
            lblIconCircle.MouseEnter += onHover;
            lblTitle.MouseEnter += onHover;
            lblTime.MouseEnter += onHover;
            lblDesc.MouseEnter += onHover;

            card.MouseLeave += onLeave;
            lblIconCircle.MouseLeave += onLeave;
            lblTitle.MouseLeave += onLeave;
            lblTime.MouseLeave += onLeave;
            lblDesc.MouseLeave += onLeave;

            EventHandler onClick = (s, e) => OnNotificationCardClick(item, card);
            card.Click += onClick;
            lblIconCircle.Click += onClick;
            lblTitle.Click += onClick;
            lblTime.Click += onClick;
            lblDesc.Click += onClick;
            pnlLeftAccent.Click += onClick;

            return card;
        }



        private void OnNotificationCardClick(NotificationItem item, Panel card)
        {
            if (item == null) return;
        
            readNotifications.Add(item.Title + "_" + item.Description);
            MainForm mainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
            if (mainForm != null)
            {
          
                mainForm.DecrementNotificationBadge();
            }
        
            if (card != null && card.Parent != null)
            {
                card.Parent.Controls.Remove(card);
            }

            if (pnlNotification != null)
            {
                pnlNotification.Visible = false;
            }
            string target = item.TargetModule ?? "";
            if (target == "Borrow" || item.Title.Contains("Borrow"))
            {
                var method = mainForm.GetType().GetMethod("pnlBorrow_Click", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (method != null) method.Invoke(mainForm, new object[] { null, null });
            }
            else if (target == "Lent" || item.Title.Contains("Lent"))
            {
                var method = mainForm.GetType().GetMethod("pnlLent_Click", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (method != null) method.Invoke(mainForm, new object[] { null, null });
            }
            else if (target == "Task" || item.Title.Contains("Task"))
            {
                var method = mainForm.GetType().GetMethod("pnlTasks_Click", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (method != null) method.Invoke(mainForm, new object[] { null, null });
            }
            else if (target == "Credit" || item.Title.Contains("Credit") || item.Title.Contains("Received"))
            {
                var method = mainForm.GetType().GetMethod("pnlCredit_Click", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (method != null) method.Invoke(mainForm, new object[] { null, null });
            }
            else if (target == "Expense" || item.Title.Contains("Expense") || item.Title.Contains("Budget"))
            {
                var method = mainForm.GetType().GetMethod("pnlExpense_Click", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (method != null) method.Invoke(mainForm, new object[] { null, null });
            }
        }

        private class NotificationItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string TimeText { get; set; }
            public Color ThemeColor { get; set; }
            public Color BgColor { get; set; }
            public string IconSymbol { get; set; }

            public string TargetModule { get; set; }
        }

        



        private void btnClose_Click(object sender, EventArgs e)
        {
            //this.Close();

            pnlNotification.Visible = false;
        }

    }
}


