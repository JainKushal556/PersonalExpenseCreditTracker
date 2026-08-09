using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinFormsSortOrder = System.Windows.Forms.SortOrder;
using PersonalExpenseCreditTracker.Common;
using BLLayer.Expense;
using BLLayer.Common;

namespace PersonalExpenseCreditTracker.Modules.Expense
{
    public partial class ExpenseControl : Form

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
        private bool ignoreEvents { get; set; }
        private int lastSelectedCategoryId { get; set; } 
        private int lastSelectedSubCategoryId { get; set; } 
        private DateTime fromDate { get; set; }
        private DateTime toDate { get; set; }
        private bool validFromDate { get; set; }
        private bool validToDate { get; set; }
        private string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private DataTable AllExpenseData = new DataTable();
        private DataTable masterData = new DataTable();
        private int currentPage = 1;
        private int pageSize = 0;
        private string sortedColumn = "ExpenseAt";
        private System.Windows.Forms.SortOrder currentSortOrder =
            System.Windows.Forms.SortOrder.Descending;

        public ExpenseControl()
        {
            InitializeComponent();
            StyleExpenseGrid();
            ApplyRoundCorners();
            //dgvExpenseDataTable.CellPainting += dgvExpenseDataTable_CellPainting;
            dgvExpenseDataTable.ColumnHeaderMouseClick +=dgvExpenseDataTable_ColumnHeaderMouseClick;
            this.Resize += ExpenseControl_Resize;

            typeof(DataGridView).InvokeMember(
                 "DoubleBuffered",
                 System.Reflection.BindingFlags.NonPublic |
                 System.Reflection.BindingFlags.Instance |
                 System.Reflection.BindingFlags.SetProperty,
                 null,
                 dgvExpenseDataTable,
                 new object[] { true });

        }


        private void ExpenseControl_Load(object sender, EventArgs e)
        {
            txtMinAmount.Text = "Enter Amount";
            txtMinAmount.ForeColor = Color.Gray;
            txtMaxAmount.Text = "Enter Amount";
            txtMaxAmount.ForeColor = Color.Gray;
            cmbCategory.Text = "Select Category";
            cmbCategory.ForeColor = Color.Gray;
            cmbCategorytxt.Text = "Enter Category";
            cmbCategorytxt.ForeColor = Color.Gray;
            cmbSubCategory.Text = "Enter SubCategory";
            cmbSubCategory.ForeColor = Color.Gray;

            dgvExpenseDataTable.CellPainting += dgvExpenseDataTable_CellPainting;
            ApplyRoundCorners();
            pageSize = GetRowsPerPage();
            HideAllFilterPanels();
            DesignContextMenu();
            
            int userID = Session.LogedInUser.GetUserId();
            LoadExpenseData(userID);
            cmsFilter.Opening += cmsFilter_Opening;
            RegisterMouseDown(this);
            txtFromdate.ReadOnly = true;
            txtToDate.ReadOnly = true;
            monthCalendarToDate.MaxDate = DateTime.Today;
            monthCalendarFromDate.MaxDate = DateTime.Today;
            ignoreEvents = false;
        }

        private void cmsFilter_Opening(object sender, CancelEventArgs e)
        {
            tsmiDate.AutoSize = false;
            tsmiCategory.AutoSize = false;
            tsmiAmount.AutoSize = false;
           // tsmiSubCategory.AutoSize = false;
            tsmiDate.Width = cmsFilter.Width;
            tsmiCategory.Width = cmsFilter.Width;
          //  tsmiSubCategory.Width = cmsFilter.Width;
            tsmiAmount.Width = cmsFilter.Width;
        }

        private void StyleExpenseGrid()
        {
            colDate.DataPropertyName = "ExpenseAt";
            colDescription.DataPropertyName = "Description";
            colCategory.DataPropertyName = "CategoryName";
            colSubCategory.DataPropertyName = "SubCategoryName";
            colAmount.DataPropertyName = "Amount";
            colPaymentMethod.DataPropertyName = "PaymentName";

            //Column Style
            dgvExpenseDataTable.AllowUserToOrderColumns = false;
            dgvExpenseDataTable.AutoGenerateColumns = false;
            dgvExpenseDataTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //Column HeaderStyle
            dgvExpenseDataTable.EnableHeadersVisualStyles = false;
            dgvExpenseDataTable.ColumnHeadersHeight = 45;
            dgvExpenseDataTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvExpenseDataTable.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvExpenseDataTable.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvExpenseDataTable.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 245, 255);
            dgvExpenseDataTable.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(80, 60, 180);
            dgvExpenseDataTable.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //Column Background Color
            colDate.DefaultCellStyle.BackColor = Color.White;
            colDescription.DefaultCellStyle.BackColor = Color.White;
            colCategory.DefaultCellStyle.BackColor = Color.White;
            colSubCategory.DefaultCellStyle.BackColor = Color.White;
            colAmount.DefaultCellStyle.BackColor = Color.White;
            colPaymentMethod.DefaultCellStyle.BackColor = Color.White;

            //Column FontStyle
            colDate.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colDescription.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colCategory.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colSubCategory.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colAmount.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colPaymentMethod.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            //Row Style
            dgvExpenseDataTable.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvExpenseDataTable.DefaultCellStyle.BackColor = Color.White;
            dgvExpenseDataTable.DefaultCellStyle.ForeColor = Color.Black;
            //dgvExpenseDataTable.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            //dgvExpenseDataTable.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 238, 255);
            dgvExpenseDataTable.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvExpenseDataTable.RowTemplate.Height = 40;
            dgvExpenseDataTable.RowHeadersVisible = false;
            dgvExpenseDataTable.MultiSelect = false;
            dgvExpenseDataTable.ReadOnly = true;
            dgvExpenseDataTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Border style
            dgvExpenseDataTable.BorderStyle = BorderStyle.None;
            dgvExpenseDataTable.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvExpenseDataTable.GridColor = Color.FromArgb(230, 230, 230);

            // Cell Alignment
            colDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDescription.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCategory.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSubCategory.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPaymentMethod.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //row data color
            colAmount.DefaultCellStyle.ForeColor = Color.Red;
            colCategory.DefaultCellStyle.ForeColor = Color.Green;
            colPaymentMethod.DefaultCellStyle.ForeColor = Color.Blue;
            colSubCategory.DefaultCellStyle.ForeColor = Color.Purple;
        }

        private void dgvExpenseDataTable_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex != -1)
                return;

            switch (dgvExpenseDataTable.Columns[e.ColumnIndex].Name)
            {
                case "colDate":
                    DrawHeader(e, Properties.Resources.date, "Date");
                    break;

                case "colDescription":
                    DrawHeader(e, Properties.Resources.note, "Description");
                    break;

                case "colCategory":
                    DrawHeader(e, Properties.Resources.shop, "Category");
                    break;

                case "colSubCategory":
                    DrawHeader(e, Properties.Resources.folder, "Sub Category");
                    break;

                case "colAmount":
                    DrawHeader(e, Properties.Resources.money, "Amount");
                    break;

                case "colPaymentMethod":
                    DrawHeader(e, Properties.Resources.credit_card1, "Payment");
                    break;
            }

        }


        private void DrawHeader(DataGridViewCellPaintingEventArgs e,Image icon,string text)
        {
            e.Paint( e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);
            int iconSize = 16;
            int spacing = 6;

            SizeF textSize =e.Graphics.MeasureString(text, e.CellStyle.Font);

            int totalWidth =iconSize +spacing +(int)textSize.Width;

            int startX =e.CellBounds.X +(e.CellBounds.Width - totalWidth) / 2;

            int iconY = e.CellBounds.Y +(e.CellBounds.Height - iconSize) / 2;

            e.Graphics.DrawImage(icon, startX, iconY,iconSize,iconSize);

            using (Brush brush =
                new SolidBrush(Color.FromArgb(80, 60, 180)))
            {
                float textX =startX +iconSize +spacing;

                float textY = e.CellBounds.Y +(e.CellBounds.Height - textSize.Height) / 2;

                e.Graphics.DrawString(text, e.CellStyle.Font, brush, textX, textY);
            }

            e.Handled = true;
        }

        public void LoadExpenseData(int userID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spGetAllExpensesByID", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", userID);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        da.Fill(dt);


                        if (dt.Columns.Contains("Message"))
                        {
                            MessageBox.Show(dt.Rows[0]["Message"].ToString(),
                                            "Information",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);

                            dgvExpenseDataTable.DataSource = null;
                            return;
                        }

                        AllExpenseData = dt;
                        masterData = dt.Copy();
                        sortedColumn = "ExpenseAt";
                        currentSortOrder = System.Windows.Forms.SortOrder.Descending;

                        ApplyExpenseSort();
                        currentPage = 1;
                        ShowCurrentPage();
                        UpdateExpenseSummaryCards();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Boolean LoadFilteredExpenseData(string spName, int userId, string paramName1, DateTime paramId1, string paramName2, DateTime paramId2)
        {
            int userID = PersonalExpenseCreditTracker.Session.LogedInUser.GetUserId();
            DataTable dataTable = CommonUiFunction.RetrieveDataByUserIdAndFilterId(spName, userID, paramName1, paramId1, paramName2, paramId2);
            if (dataTable.Columns.Contains("Message"))
            {
                MessageBox.Show(dataTable.Rows[0]["Message"].ToString(),
                                "Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }
            if (dataTable.Rows.Count <= 0)
            {
                return false;
            }
            AllExpenseData = dataTable;
            masterData = dataTable.Copy();
            currentPage = 1;
            ShowCurrentPage();
            UpdateExpenseSummaryCards();
            return true;
        }

        public Boolean LoadFilteredExpenseData(string spName, int userId, string paramName1, Decimal paramId1, string paramName2, Decimal paramId2)
        {
            DataTable dataTable = CommonUiFunction.RetrieveDataByUserIdAndFilterId(
                spName,
                userId,
                paramName1,
                paramId1,
                paramName2,
                paramId2);

            if (dataTable.Columns.Contains("Message"))
            {
                MessageBox.Show(dataTable.Rows[0]["Message"].ToString(),
                                "Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }

            if (dataTable.Rows.Count <= 0)
            {
                return false;
            }

            AllExpenseData = dataTable;
            masterData = dataTable.Copy();
            currentPage = 1;
            ShowCurrentPage();
            UpdateExpenseSummaryCards();
            return true;
        }

        public Boolean LoadFilteredExpenseData(string spName, int userId, string paramName1, int paramId1, string paramName2, int paramId2)
        {
            DataTable dataTable = CommonUiFunction.RetrieveDataByUserIdAndFilterId(
                spName,
                userId,
                paramName1,
                paramId1,
                paramName2,
                paramId2);

            if (dataTable.Columns.Contains("Message"))
            {
                MessageBox.Show(dataTable.Rows[0]["Message"].ToString(),
                                "Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }

            if (dataTable.Rows.Count == 0)
            {
                return false;
            }

            AllExpenseData = dataTable;
            masterData = dataTable.Copy();
            currentPage = 1;
            ShowCurrentPage();
            UpdateExpenseSummaryCards();
            return true;
        }

        public Boolean LoadFilteredExpenseData(string spName, string paramName, int paramValue, int filterId)
        {
            int userID = PersonalExpenseCreditTracker.Session.LogedInUser.GetUserId();

            DataTable dataTable = CommonUiFunction.RetrieveFilteredDataByStatus(
                spName,
                userID,
                paramName,
                filterId);

            if (dataTable.Columns.Contains("Message"))
            {
                MessageBox.Show(dataTable.Rows[0]["Message"].ToString(),
                                "Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }

            if (dataTable.Rows.Count <= 0)
            {
                return false;
            }

            AllExpenseData = dataTable;
            masterData = dataTable.Copy();
            currentPage = 1;
            ShowCurrentPage();
            UpdateExpenseSummaryCards();
            return true;
        }

        private void UpdateExpenseSummaryCards()
        {
            if (AllExpenseData == null || AllExpenseData.Rows.Count == 0)
            {
                lblExpenseAmount.Text = "₹ 0";
                lblTransactionAmount.Text = "0";
                return;
            }

            decimal totalExpense = 0;

            foreach (DataRow row in AllExpenseData.Rows)
            {
                if (row["Amount"] != DBNull.Value)
                {
                    totalExpense += Convert.ToDecimal(row["Amount"]);
                }
            }

            lblExpenseAmount.Text = "₹ " + totalExpense.ToString("#,##0");
            lblTransactionAmount.Text = AllExpenseData.Rows.Count.ToString();
        }


        private void dgvExpenseDataTable_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            DataGridViewColumn column =dgvExpenseDataTable.Columns[e.ColumnIndex];

            string columnName = column.DataPropertyName;

            //  sortable columns
            if (columnName != "ExpenseAt" &&
                columnName != "Amount" &&
                columnName != "CategoryName" &&
                columnName != "SubCategoryName" &&
                columnName != "PaymentName")
            {
                return;
            }

            // Same column click ASC <-> DESC
            if (sortedColumn == columnName)
            {
                currentSortOrder =
                    currentSortOrder == WinFormsSortOrder.Ascending
                    ? WinFormsSortOrder.Descending
                    : WinFormsSortOrder.Ascending;
            }
            else
            {
                
                sortedColumn = columnName;
                currentSortOrder = WinFormsSortOrder.Ascending;
            }

            ApplyExpenseSort();

            currentPage = 1;

            ShowCurrentPage();
        }



        private void ApplyExpenseSort()
        {
            if (string.IsNullOrEmpty(sortedColumn) ||
                currentSortOrder == WinFormsSortOrder.None)
                return;

            if (AllExpenseData == null ||
                AllExpenseData.Rows.Count == 0)
                return;

            if (!AllExpenseData.Columns.Contains(sortedColumn))
                return;

            DataView view = AllExpenseData.DefaultView;

            string direction =
                currentSortOrder == WinFormsSortOrder.Ascending
                ? "ASC"
                : "DESC";

            view.Sort =
                "[" + sortedColumn + "] " + direction;

            AllExpenseData = view.ToTable();
        }




        private void ShowCurrentPage()
        {
            DataTable pageTable = AllExpenseData.Clone();

            btnCurrentPage.Text = currentPage.ToString();

            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, AllExpenseData.Rows.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                pageTable.ImportRow(AllExpenseData.Rows[i]);
            }

            dgvExpenseDataTable.DataSource = pageTable;
            Common.CommonUiFunction.HighlightSearch(dgvExpenseDataTable, txtSearch);
            int start = startIndex + 1;
            int end = endIndex;
            int total = AllExpenseData.Rows.Count;

            lblExpenseStartingPageNumber.Text = total == 0 ? "0" : start.ToString();
            lblExpenseEndingPageNumber.Text = end.ToString();
            lblExpenseTotalPageNumber.Text = total.ToString();
        }

        private int GetRowsPerPage()
        {
            Rectangle display = dgvExpenseDataTable.DisplayRectangle;

            int rowHeight = dgvExpenseDataTable.RowTemplate.Height;

            return Math.Max(1, display.Height / rowHeight) - 1;
        }


        private void ExpenseControl_Resize(object sender, EventArgs e)
        {
            ApplyRoundCorners();
            if (AllExpenseData == null || AllExpenseData.Rows.Count == 0)
                return;

            int newPageSize = GetRowsPerPage();

            if (newPageSize != pageSize)
            {
                pageSize = newPageSize;

                int totalPages = (int)Math.Ceiling((double)AllExpenseData.Rows.Count / pageSize);

                if (currentPage > totalPages)
                    currentPage = totalPages;

                ShowCurrentPage();
            }
        }

        //Page control button
        private void btnFirstpage_Click(object sender, EventArgs e)
        {
            if (currentPage != 1)
            {
                currentPage = 1;
                ShowCurrentPage();
            }
        }
        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                ShowCurrentPage();
            }
        }
        private void btnNextpage_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)AllExpenseData.Rows.Count / pageSize);

            if (currentPage < totalPages)
            {
                currentPage++;
                ShowCurrentPage();
            }
        }
        private void btnLastPage_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)AllExpenseData.Rows.Count / pageSize);
            if (currentPage != totalPages)
            {
                currentPage = totalPages;
                ShowCurrentPage();
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {


        }

        
        private void pnlTotalExpense_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlTotalExpense.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void pnlTransactionCard_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlTransactionCard.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }
        private void ApplyRoundCorners()
        {
            pnlTotalExpense.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pnlTotalExpense.Width, pnlTotalExpense.Height, 15, 15));

            pnlTransactionCard.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pnlTransactionCard.Width, pnlTransactionCard.Height, 15, 15));

            
        }
        private void pnlTotalExpense_Resize(object sender, EventArgs e)
        {
            ApplyRoundCorners();
        }

        private void pnlTransactionCard_Resize(object sender, EventArgs e)
        {
            ApplyRoundCorners();
        }

        private void dgvExpenseDataTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void btnFilter_Click(object sender, EventArgs e)
        {
            cmsFilter.Show(btnFilter, 0, btnFilter.Height);
        }
        private void HideAllFilterPanels()
        {
            pnlDateFilter.Visible = false;
            pnlCategoryFilter.Visible = false;
            pnlAmountFilter.Visible = false;
            pnlSubCategoryFilter.Visible = false;
        }
        private void HidePopupPanels()
        {
            pnlFromDateCalenderShow.Visible = false;
            pnlToDateCalenderShow.Visible = false;
        }
        private void ShowFilterPanel(Panel panel)
        {
            HideAllFilterPanels();

            Point p = pnlButtonControls.PointToScreen(Point.Empty);
            p = this.PointToClient(p);

            panel.Parent = this;

            panel.Location = new Point(
                p.X - panel.Width - 10,
                p.Y);

            panel.BringToFront();
            panel.Visible = true;
        }
        private void tsmiDate_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlDateFilter);
            ignoreEvents = false;
        }

        private void tsmiCategory_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlCategoryFilter);
            //cmbCategory.DroppedDown = true;
            ignoreEvents = false;

            Common.CommonUiFunction.LoadInComboBox("spGetExpenseCategoriesByUserID", Session.LogedInUser.GetUserId(), "Select Category", cmbCategory);


        }
        private void tsmiSubCategory_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlSubCategoryFilter);
            ignoreEvents = false;
            Common.CommonUiFunction.LoadInComboBox("spGetExpenseCategoriesByUserID", Session.LogedInUser.GetUserId(), "Select Category", cmbCategorytxt);
        }

        private void tsmiAmount_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlAmountFilter);
            ignoreEvents = false;
        }

        private void btnDateClose_Click(object sender, EventArgs e)
        {
            ignoreEvents = true;

            txtFromdate.Clear();
            txtToDate.Clear();

            this.fromDate = DateTime.MinValue;
            this.toDate = DateTime.MinValue;
            this.validFromDate = false;
            this.validToDate = false;

            pnlFromDateCalenderShow.Visible = false;
            pnlToDateCalenderShow.Visible = false;
            errorProvider1.Clear();
            ErrorHelper.HideErrorForControl(pnlFromDate);
            ErrorHelper.HideErrorForControl(pnlToDate);

            pnlDateFilter.Visible = false;

            ignoreEvents = false;
            LoadExpenseData(Session.LogedInUser.GetUserId());
        }

        private void ResetCategoryAndSubCategoryFilters()
        {
            ignoreEvents = true;
            if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
            if (cmbSubCategory.Items.Count > 0) cmbSubCategory.SelectedIndex = 0;
            if (cmbCategorytxt.Items.Count > 0) cmbCategorytxt.SelectedIndex = 0;
            lastSelectedCategoryId = -1;
            lastSelectedSubCategoryId = -1;
            ignoreEvents = false;

            pnlCategoryFilter.Visible = false;
            pnlSubCategoryFilter.Visible = false;

            LoadExpenseData(Session.LogedInUser.GetUserId());
        }

        private void btncategoryClose_Click(object sender, EventArgs e)
        {
            ResetCategoryAndSubCategoryFilters();
        }
        private void DesignContextMenu()
        {
            cmsFilter.ShowImageMargin = true;
            cmsFilter.ShowCheckMargin = false;
            cmsFilter.ImageScalingSize = new Size(10, 10);

            tsmiDate.AutoSize = false;
            tsmiDate.Height = 30;

            tsmiCategory.AutoSize = false;
            tsmiCategory.Height = 30;

            tsmiAmount.AutoSize = false;
            tsmiAmount.Height = 30;
            

            //tsmiSubCategory.AutoSize = false;
         //   tsmiSubCategory.Height = 30;


            tsmiDate.Image = Properties.Resources.calendar;
            tsmiCategory.Image = Properties.Resources.shop;
          //  tsmiSubCategory.Image = Properties.Resources.folder;
            tsmiAmount.Image = Properties.Resources.money;

            tsmiDate.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsmiCategory.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
          //  tsmiSubCategory.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsmiAmount.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

            tsmiDate.ImageScaling = ToolStripItemImageScaling.None;
            tsmiCategory.ImageScaling = ToolStripItemImageScaling.None;
         //   tsmiSubCategory.ImageScaling = ToolStripItemImageScaling.None;
            tsmiAmount.ImageScaling = ToolStripItemImageScaling.None;


        }

        private void picCalenderFromDate_Click(object sender, EventArgs e)
        {
            if (pnlFromDateCalenderShow.Visible)
            {
                pnlFromDateCalenderShow.Visible = false;
            }
            else
            {
                ShowCalenderFromDatePanel(pnlFromDateCalenderShow);
            }
        }

        //private void monthCalendarFromDate_DateChanged(object sender, DateRangeEventArgs e)
        //{
        //    txtFromdate.Text = e.Start.ToString("dd-MM-yyyy");
            
        //}
        private void ShowCalenderFromDatePanel(Panel panel)
        {
            HidePopupPanels();
            panel.Parent = this;
            Point p = txtFromdate.PointToScreen(
                      new Point(0, txtFromdate.Height + 10));
            p = this.PointToClient(p);
            panel.Location = p;
            panel.BringToFront();
            panel.Visible = true;
        }
        private void RegisterMouseDown(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.MouseDown += ExpenseControls_MouseDown;

                if (ctrl.HasChildren)
                    RegisterMouseDown(ctrl);
            }
        }
        private void ExpenseControls_MouseDown(object sender, MouseEventArgs e)
        {
            Point mousePos = this.PointToClient(Control.MousePosition);

            // From Date Calendar
            if (pnlFromDateCalenderShow.Visible)
            {
                bool clickInsideCalendar =
                    pnlFromDateCalenderShow.Bounds.Contains(mousePos);

                bool clickOnCalendarIcon =
                    picCalenderFromDate.RectangleToScreen(
                        picCalenderFromDate.ClientRectangle)
                        .Contains(Control.MousePosition);

                bool clickOnTextBox =
                    txtFromdate.RectangleToScreen(
                        txtFromdate.ClientRectangle)
                        .Contains(Control.MousePosition);

                if (!clickInsideCalendar &&
                    !clickOnCalendarIcon &&
                    !clickOnTextBox)
                {
                    pnlFromDateCalenderShow.Visible = false;
                }
            }

            // To Date Calendar
            if (pnlToDateCalenderShow.Visible)
            {
                bool clickInsideCalendar =
                    pnlToDateCalenderShow.Bounds.Contains(mousePos);

                bool clickOnCalendarIcon =
                    picCalenderToDate.RectangleToScreen(
                        picCalenderToDate.ClientRectangle)
                        .Contains(Control.MousePosition);

                bool clickOnTextBox =
                    txtToDate.RectangleToScreen(
                        txtToDate.ClientRectangle)
                        .Contains(Control.MousePosition);

                if (!clickInsideCalendar &&
                    !clickOnCalendarIcon &&
                    !clickOnTextBox)
                {
                    pnlToDateCalenderShow.Visible = false;
                }
            }
        }
        private static readonly string[] DateFormats = { "dd-MM-yyyy", "d-M-yyyy", "dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd" };

        private void txtFromdate_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            errorProvider1.Clear();
            ErrorHelper.HideErrorForControl(pnlFromDate);
            ErrorHelper.HideErrorForControl(pnlToDate);

            if (string.IsNullOrWhiteSpace(txtFromdate.Text) || txtFromdate.Text == "Select Date")
            {
                this.fromDate = DateTime.MinValue;
                return;
            }

            DateTime parsedFromDate;
            if (!DateTime.TryParseExact(
                    txtFromdate.Text.Trim(),
                    DateFormats,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out parsedFromDate))
            {
                return;
            }

            this.fromDate = parsedFromDate;

            if (string.IsNullOrWhiteSpace(txtToDate.Text) || txtToDate.Text == "Select Date")
            {
                ignoreEvents = true;
                txtToDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                ignoreEvents = false;
                this.toDate = DateTime.Today;
            }
            else
            {
                DateTime parsedToDate;
                if (DateTime.TryParseExact(
                        txtToDate.Text.Trim(),
                        DateFormats,
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
                        out parsedToDate))
                {
                    this.toDate = parsedToDate;
                }
            }

            ExpenseBLL expenseBll = new ExpenseBLL();
            expenseBll.fromDate = this.fromDate;
            expenseBll.toDate = this.toDate;

            CommonValidator.ValidationResult result = expenseBll.DateValidatorIntoExpenseBll();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    validFromDate = true;
                    if (!LoadFilteredExpenseData(
                            "spFilterExpenseByDateRange",
                            Session.LogedInUser.GetUserId(),
                            "@FromDate",
                            this.fromDate,
                            "@ToDate",
                            this.toDate))
                    {
                        ignoreEvents = true;
                        txtFromdate.Clear();
                        txtToDate.Clear();
                        ignoreEvents = false;
                        this.fromDate = DateTime.MinValue;
                        this.toDate = DateTime.MinValue;

                        LoadExpenseData(Session.LogedInUser.GetUserId());
                    }
                    break;

                case CommonValidator.ValidationResult.DateRangeInvalid:
                    validFromDate = false;
                    ErrorHelper.ShowValidationError(result, errorProvider1, pnlFromDate, pnlToDate);
                    MessageBox.Show("From Date cannot be greater than To Date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ignoreEvents = true;
                    txtFromdate.Clear();
                    txtToDate.Clear();
                    ignoreEvents = false;
                    this.fromDate = DateTime.MinValue;
                    this.toDate = DateTime.MinValue;
                    LoadExpenseData(Session.LogedInUser.GetUserId());
                    break;
            }
        }

        private void txtToDate_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            errorProvider1.Clear();
            ErrorHelper.HideErrorForControl(pnlFromDate);
            ErrorHelper.HideErrorForControl(pnlToDate);

            if (string.IsNullOrWhiteSpace(txtToDate.Text) || txtToDate.Text == "Select Date")
            {
                this.toDate = DateTime.MinValue;
                return;
            }

            DateTime parsedToDate;
            if (!DateTime.TryParseExact(
                    txtToDate.Text.Trim(),
                    DateFormats,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out parsedToDate))
            {
                return;
            }

            this.toDate = parsedToDate;

            if (string.IsNullOrWhiteSpace(txtFromdate.Text) || txtFromdate.Text == "Select Date")
            {
                return;
            }

            DateTime parsedFromDate;
            if (DateTime.TryParseExact(
                    txtFromdate.Text.Trim(),
                    DateFormats,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out parsedFromDate))
            {
                this.fromDate = parsedFromDate;
            }

            ExpenseBLL expenseBll = new ExpenseBLL();
            expenseBll.fromDate = this.fromDate;
            expenseBll.toDate = this.toDate;

            CommonValidator.ValidationResult result = expenseBll.DateValidatorIntoExpenseBll();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    validFromDate = true;
                    if (!LoadFilteredExpenseData(
                            "spFilterExpenseByDateRange",
                            Session.LogedInUser.GetUserId(),
                            "@FromDate",
                            this.fromDate,
                            "@ToDate",
                            this.toDate))
                    {
                        ignoreEvents = true;
                        txtFromdate.Clear();
                        txtToDate.Clear();
                        ignoreEvents = false;
                        this.fromDate = DateTime.MinValue;
                        this.toDate = DateTime.MinValue;

                        LoadExpenseData(Session.LogedInUser.GetUserId());
                    }
                    break;

                case CommonValidator.ValidationResult.DateRangeInvalid:
                    validFromDate = false;
                    ErrorHelper.ShowValidationError(result, errorProvider1, pnlFromDate, pnlToDate);
                    MessageBox.Show("From Date cannot be greater than To Date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ignoreEvents = true;
                    txtFromdate.Clear();
                    txtToDate.Clear();
                    ignoreEvents = false;
                    this.fromDate = DateTime.MinValue;
                    this.toDate = DateTime.MinValue;
                    LoadExpenseData(Session.LogedInUser.GetUserId());
                    break;
            }
        }
        private void ShowCalenderToDatePanel(Panel panel)
        {
            HidePopupPanels();

            panel.Parent = this;

            Point p = txtToDate.PointToScreen(
                new Point(0, txtToDate.Height +10));

            p = this.PointToClient(p);

            panel.Location = p;

            panel.BringToFront();
            panel.Visible = true;
        }

      

        private void picCalenderToDate_Click(object sender, EventArgs e)
        {
            if (pnlToDateCalenderShow.Visible)
            {
                pnlToDateCalenderShow.Visible = false;
            }
            else
            {
                ShowCalenderToDatePanel(pnlToDateCalenderShow);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetCategoryAndSubCategoryFilters();
        }

        private void btnAmountClose_Click(object sender, EventArgs e)
        {
            ignoreEvents = true;
            txtMinAmount.Text = "Enter Amount";
            txtMinAmount.ForeColor = Color.Gray;
            txtMaxAmount.Text = "Enter Amount";
            txtMaxAmount.ForeColor = Color.Gray;
            ignoreEvents = false;

            pnlAmountFilter.Visible = false;

            LoadExpenseData(Session.LogedInUser.GetUserId());
        }

        

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            AllExpenseData = Common.CommonUiFunction.SearchDataInExpenseOrCredit(masterData, txtSearch);
            ApplyExpenseSort();
            currentPage = 1;
            ShowCurrentPage();
        }

        //private void txtFromdate_TextChanged(object sender, EventArgs e)
        //{
        //    errorProvider1.Clear();
        //    ExpenseBLL expenseBll = new ExpenseBLL();
        //    expenseBll.fromDate = this.fromDate;
        //    if (this.toDate == DateTime.MinValue)
        //    {
        //        expenseBll.toDate = DateTime.Now;
        //    }
        //    else
        //    {
        //        expenseBll.toDate = this.toDate;
        //    }

        //    CommonValidator.ValidationResult result = expenseBll.DateValidatorIntoExpenseBll();

        //    switch (result)
        //    {
        //        case CommonValidator.ValidationResult.Success:
        //            validFromDate = true;
        //            break;
        //        case CommonValidator.ValidationResult.DateRangeInvalid:
        //            validFromDate = false;
        //            ErrorHelper.ShowValidationError(result, errorProvider1, pnlFromDate, pnlToDate);
        //            break;
        //    }
        //}

        private void cmbCategory_Enter(object sender, EventArgs e)
        {
            if (cmbCategory.Text == "Select Category")
                cmbCategory.ForeColor = Color.Black;
        }

        private void cmbCategory_Leave(object sender, EventArgs e)
        {
            if ( string.IsNullOrWhiteSpace(cmbCategory.Text) || cmbCategory.Text == "Select Category")
            {
                
                cmbCategory.Text = "Select Category";
                cmbCategory.ForeColor = Color.Gray;
            }
            else
            {
                cmbCategory.ForeColor = Color.Black;
            }
        }

        private void cmbCategory_Click(object sender, EventArgs e)
        {
            cmbCategory.DroppedDown = true;
        }

        private void txtMinAmount_Enter(object sender, EventArgs e)
        {
            if (txtMinAmount.Text == "Enter Amount")
            {
                txtMinAmount.Text = "";
                txtMinAmount.ForeColor = Color.Black;
            }
        }

        private void txtMinAmount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMinAmount.Text))
            {
                txtMinAmount.Text = "Enter Amount";
                txtMinAmount.ForeColor = Color.Gray;
            }
            ExecuteAmountFilter();
        }

        private void txtMaxAmount_Enter(object sender, EventArgs e)
        {
            if (txtMaxAmount.Text == "Enter Amount")
            {
                txtMaxAmount.Text = "";
                txtMaxAmount.ForeColor = Color.Black;
            }
        }

        private void txtMaxAmount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaxAmount.Text))
            {
                txtMaxAmount.Text = "Enter Amount";
                txtMaxAmount.ForeColor = Color.Gray;
            }
            ExecuteAmountFilter();
        }

        private void txtMinAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ExecuteAmountFilter();
            }
        }

        private void txtMaxAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ExecuteAmountFilter();
            }
        }

        private void ExecuteAmountFilter()
        {
            string minStr = txtMinAmount.Text.Trim();
            string maxStr = txtMaxAmount.Text.Trim();

            bool isMinProvided = minStr != "Enter Amount" && !string.IsNullOrWhiteSpace(minStr);
            bool isMaxProvided = maxStr != "Enter Amount" && !string.IsNullOrWhiteSpace(maxStr);

            if (!isMinProvided && !isMaxProvided)
            {
                txtMinAmount.Text = "Enter Amount";
                txtMinAmount.ForeColor = Color.Gray;
                txtMaxAmount.Text = "Enter Amount";
                txtMaxAmount.ForeColor = Color.Gray;
                LoadExpenseData(Session.LogedInUser.GetUserId());
                return;
            }

            decimal minValue = 0m;
            decimal maxValue = Common.CommonUiFunction.SqlAmountMax;

            if (isMinProvided)
            {
                if (!decimal.TryParse(minStr, out minValue) || minValue < 0)
                {
                    MessageBox.Show("Please enter a valid numeric Minimum Amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ResetAmountFilterInputsAndReloadData();
                    return;
                }
            }

            if (isMaxProvided)
            {
                if (!decimal.TryParse(maxStr, out maxValue) || maxValue < 0)
                {
                    MessageBox.Show("Please enter a valid numeric Maximum Amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ResetAmountFilterInputsAndReloadData();
                    return;
                }
            }

            if (minValue > maxValue)
            {
                MessageBox.Show("Minimum Amount cannot be greater than Maximum Amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetAmountFilterInputsAndReloadData();
                return;
            }

            ApplyAmountFilter(minValue, maxValue);
        }

        private void cmbCategorytxt_Click(object sender, EventArgs e)
        {
            if (cmbCategorytxt.Items.Count <= 1)
            {
                Common.CommonUiFunction.LoadInComboBox("spGetExpenseCategoriesByUserID", Session.LogedInUser.GetUserId(), "Select Category", cmbCategorytxt);
            }
            cmbCategorytxt.DroppedDown = true;
        }

        private void cmbCategorytxt_Enter(object sender, EventArgs e)
        {
            if (cmbCategorytxt.Text == "Select Category")
                cmbCategorytxt.ForeColor = Color.Black;
        }

        private void cmbCategorytxt_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbCategorytxt.Text) || cmbCategorytxt.Text == "Select Category")
            {

                cmbCategorytxt.Text = "Select Category";
                cmbCategorytxt.ForeColor = Color.Gray;
            }
            else
            {
                cmbCategorytxt.ForeColor = Color.Black;
            }
        }

        private void cmbSubCategory_Click(object sender, EventArgs e)
        {
            cmbSubCategory.DroppedDown = true;
        }

        private void cmbSubCategory_Enter(object sender, EventArgs e)
        {
            if (cmbSubCategory.Text == "Select SubCategory")
                cmbSubCategory.ForeColor = Color.Black;
        }

        private void cmbSubCategory_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbSubCategory.Text) || cmbSubCategory.Text == "Select SubCategory")
            {

                cmbSubCategory.Text = "Select SubCategory";
                cmbSubCategory.ForeColor = Color.Gray;
            }
            else
            {
                cmbSubCategory.ForeColor = Color.Black;
            }
        }

        private void txtFromdate_Click(object sender, EventArgs e)
        {

            if (pnlFromDateCalenderShow.Visible)
            {
                pnlFromDateCalenderShow.Visible = false;
            }
            else
            {
                ShowCalenderFromDatePanel(pnlFromDateCalenderShow);
            }
        }

        private void txtToDate_Click(object sender, EventArgs e)
        {
            if (pnlToDateCalenderShow.Visible)
            {
                pnlToDateCalenderShow.Visible = false;
            }
            else
            {
                ShowCalenderToDatePanel(pnlToDateCalenderShow);
            }
        }

        private void monthCalendarFromDate_DateSelected(object sender, DateRangeEventArgs e)
        {
           
            fromDate = e.Start.Date;
            txtFromdate.Text = e.Start.ToString("dd-MM-yyyy");
            
            pnlFromDateCalenderShow.Visible = false; 
        }

        private void monthCalendarToDate_DateSelected(object sender, DateRangeEventArgs e)
        {
            
            toDate = e.Start.Date;
            txtToDate.Text = e.Start.ToString("dd-MM-yyyy");
            pnlToDateCalenderShow.Visible = false;
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            if (cmbCategory.SelectedIndex <= 0) return;
            if (cmbCategory.SelectedValue == null) return;
            if (cmbCategory.SelectedValue is DataRowView) return;

            int categoryId = Convert.ToInt32(cmbCategory.SelectedValue);

            if (categoryId == lastSelectedCategoryId) return;

            if (!LoadFilteredExpenseData("spFilterExpenseByCategory", "@CategoryID", categoryId, categoryId))
            {
                ignoreEvents = true;
                if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
                if (cmbCategorytxt.Items.Count > 0) cmbCategorytxt.SelectedIndex = 0;
                if (cmbSubCategory.Items.Count > 0) cmbSubCategory.SelectedIndex = 0;
                lastSelectedCategoryId = -1;
                lastSelectedSubCategoryId = -1;
                ignoreEvents = false;

                pnlSubCategoryFilter.Visible = false;
                LoadExpenseData(Session.LogedInUser.GetUserId());
                return;
            }

            lastSelectedCategoryId = categoryId;
            lastSelectedSubCategoryId = -1;

            CommonUiFunction.LoadInComboBox(
                "spGetExpenseSubCategoryByCategoryID",
                "Select SubCategory",
                cmbSubCategory,
                "@CategoryID",
                categoryId);

            if (!(cmbSubCategory.Items.Count == 2 && cmbSubCategory.GetItemText(cmbSubCategory.Items[1]).Trim().Equals("General", StringComparison.OrdinalIgnoreCase)))
            {
                ignoreEvents = true;
                Common.CommonUiFunction.LoadInComboBox("spGetExpenseCategoriesByUserID", Session.LogedInUser.GetUserId(), "Select Category", cmbCategorytxt);
                cmbCategorytxt.SelectedValue = categoryId;
                cmbCategorytxt.ForeColor = Color.Black;
                ignoreEvents = false;

                ShowFilterPanel(pnlSubCategoryFilter);
            }
            else
            {
                pnlSubCategoryFilter.Visible = false;
            }
        }

        private void cmbSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            if (cmbSubCategory.SelectedIndex <= 0) return;
            if (cmbSubCategory.SelectedValue == null) return;
            if (cmbSubCategory.SelectedValue is DataRowView) return;
            
            int categoryId = Convert.ToInt32(cmbCategorytxt.SelectedValue);
            int subCategoryId = Convert.ToInt32(cmbSubCategory.SelectedValue);

            if (subCategoryId == lastSelectedSubCategoryId) return;

            if (!LoadFilteredExpenseData("spFilterExpenseByCategoryAndSubCategory", Session.LogedInUser.GetUserId(), "@CategoryID", categoryId, "@SubCategoryID", subCategoryId))
            {
                ignoreEvents = true;
                cmbSubCategory.SelectedIndex = 0;
                lastSelectedSubCategoryId = -1;
                ignoreEvents = false;

                if (!LoadFilteredExpenseData(
                        "spFilterExpenseByCategory",
                        "@CategoryID",
                        categoryId,
                        categoryId))
                {
                    LoadExpenseData(Session.LogedInUser.GetUserId());
                }
                return;
            }

            lastSelectedSubCategoryId = subCategoryId;
        }

        private void cmbCategorytxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            if (cmbCategorytxt.SelectedIndex <= 0) return;
            if (cmbCategorytxt.SelectedValue == null) return;
            if (cmbCategorytxt.SelectedValue is DataRowView) return;

            int categoryId = Convert.ToInt32(cmbCategorytxt.SelectedValue);
             if (categoryId == lastSelectedCategoryId) return;


            if (!LoadFilteredExpenseData(
                    "spFilterExpenseByCategory",
                    "@CategoryID",
                    categoryId,
                    categoryId))
            {
                ignoreEvents = true;
                if (cmbCategorytxt.Items.Count > 0) cmbCategorytxt.SelectedIndex = 0;
                if (cmbSubCategory.Items.Count > 0) cmbSubCategory.SelectedIndex = 0;
                lastSelectedCategoryId = -1;
                lastSelectedSubCategoryId = -1;
                ignoreEvents = false;

                LoadExpenseData(Session.LogedInUser.GetUserId());
                return;
            }

            lastSelectedCategoryId = categoryId;
            lastSelectedSubCategoryId = -1;

            CommonUiFunction.LoadInComboBox(
                "spGetExpenseSubCategoryByCategoryID",
                "Select SubCategory",
                cmbSubCategory,
                "@CategoryID",
                categoryId);
        }

        private void ApplyAmountFilter(Decimal minvalue , Decimal maxValue)
        {
            if (!LoadFilteredExpenseData("spFilterExpenseByAmountRange", Session.LogedInUser.GetUserId(), "@MinAmount", minvalue, "@MaxAmount", maxValue))
            {
                ResetAmountFilterInputsAndReloadData();
            }
        }

        private void ResetAmountFilterInputsAndReloadData()
        {
            ignoreEvents = true;
            txtMinAmount.Text = "Enter Amount";
            txtMinAmount.ForeColor = Color.Gray;
            txtMaxAmount.Text = "Enter Amount";
            txtMaxAmount.ForeColor = Color.Gray;
            ignoreEvents = false;

            LoadExpenseData(Session.LogedInUser.GetUserId());
        }

      

        

        

        

       

       
    }
}