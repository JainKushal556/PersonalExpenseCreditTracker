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
using System.Windows.Forms;
using WinFormsSortOrder = System.Windows.Forms.SortOrder;
using System.Runtime.InteropServices;
using PersonalExpenseCreditTracker.Common;
using BLLayer.Credit;
using BLLayer.Common;
using Excel = Microsoft.Office.Interop.Excel;

namespace PersonalExpenseCreditTracker.Modules.Credit
{
    public partial class CreditControl : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);
        private string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private DataTable AllCreditData = new DataTable();
        private DataTable masterData = new DataTable();
        private int currentPage = 1;
        private int pageSize = 0;
        private string sortedColumn = "CreditAt";
        private System.Windows.Forms.SortOrder currentSortOrder = System.Windows.Forms.SortOrder.Descending;

        private bool ignoreEvents { get; set; }
        private int lastSelectedCategoryId { get; set; }
        private int lastSelectedSubCategoryId { get; set; }

        private DateTime fromDate { get; set; }
        private DateTime toDate { get; set; }
        private bool validFromDate { get; set; }
        private bool validToDate { get; set; }
        private static readonly string[] DateFormats = { "dd-MM-yyyy", "d-M-yyyy", "dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd" };
        private ErrorProvider errorProvider1 = new ErrorProvider();
        public CreditControl() 
        {
            InitializeComponent();
            StyleCreditGrid();
            ApplyRoundCorners();
            this.Resize += CreditControl_Resize;
            txtSearch.TextChanged += txtSearch_TextChanged;

            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null,
                dgvCreditDataTable,
                new object[] { true });

        }

        private void CreditControl_Load(object sender, EventArgs e)
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

            CommonUiFunction.SetComboBoxHeightAndOwnerDraw(cmbCategory);
            CommonUiFunction.SetComboBoxHeightAndOwnerDraw(cmbCategorytxt);
            CommonUiFunction.SetComboBoxHeightAndOwnerDraw(cmbSubCategory);

            dgvCreditDataTable.CellPainting += dgvCreditDataTable_CellPainting;
            ApplyRoundCorners();
            pageSize = GetRowsPerPage();
            int userID = Session.LogedInUser.GetUserId(); 
            LoadCreditData(userID);
            HideAllFilterPanels();
            DesignContextMenu();
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

            tsmiDate.Width = cmsFilter.Width;
            tsmiCategory.Width = cmsFilter.Width;
        }

        private void StyleCreditGrid()  
        {
            colDate.DataPropertyName = "CreditAt";
            colCategory.DataPropertyName = "CategoryName";
            colSubCategory.DataPropertyName = "SubCategoryName";
            colAmount.DataPropertyName = "Amount";
            colDescription.DataPropertyName = "Description";
            colPaymentMethod.DataPropertyName = "PaymentName";
            

            //Column Style
            dgvCreditDataTable.AllowUserToOrderColumns = false;
            dgvCreditDataTable.AutoGenerateColumns = false;
            dgvCreditDataTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //Column HeaderStyle
            dgvCreditDataTable.EnableHeadersVisualStyles = false;
            dgvCreditDataTable.ColumnHeadersHeight = 45;
            dgvCreditDataTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCreditDataTable.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvCreditDataTable.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvCreditDataTable.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 245, 255);
            dgvCreditDataTable.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(80, 60, 180);
            dgvCreditDataTable.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            dgvCreditDataTable.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvCreditDataTable.DefaultCellStyle.BackColor = Color.White;
            dgvCreditDataTable.DefaultCellStyle.ForeColor = Color.Black;
            dgvCreditDataTable.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCreditDataTable.RowTemplate.Height = 40;
            dgvCreditDataTable.RowHeadersVisible = false;
            dgvCreditDataTable.MultiSelect = false;
            dgvCreditDataTable.ReadOnly = true;
            dgvCreditDataTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Border style
            dgvCreditDataTable.BorderStyle = BorderStyle.None;
            dgvCreditDataTable.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCreditDataTable.GridColor = Color.FromArgb(230, 230, 230);

            colAmount.DefaultCellStyle.ForeColor = Color.Red;
            colCategory.DefaultCellStyle.ForeColor = Color.Green;
            colPaymentMethod.DefaultCellStyle.ForeColor = Color.Blue;
            colSubCategory.DefaultCellStyle.ForeColor = Color.Purple;

            // Cell Alignment
            colDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDescription.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCategory.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSubCategory.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPaymentMethod.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void LoadCreditData(int userID)
        {
            DataTable dataTable = CommonUiFunction.RetrieveDataForGridView("spGetAllCreditsByID", userID);
            if (dataTable == null || dataTable.Columns.Contains("Message") || dataTable.Rows.Count == 0)
            {
                dgvCreditDataTable.DataSource = null;
                lblCreditStartingPageNumber.Text = "0";
                lblCreditEndingPageNumber.Text = "0";
                lblCreditTotalPageNumber.Text = "0";
                lblCreditAmount.Text = "₹ 0";
                lblTransactionAmount.Text = "0";
                return;
            }

            AllCreditData = dataTable;
            masterData = dataTable.Copy();
            sortedColumn = "CreditAt";
            currentSortOrder = System.Windows.Forms.SortOrder.Descending;
            currentPage = 1;
            ShowCurrentPage();
            UpdateCreditSummaryCards();
        }

        public Boolean LoadFilteredCreditData(string spName, string paramName,int paramValue,int filterId)
        {
            int userID = PersonalExpenseCreditTracker.Session.LogedInUser.GetUserId();
            DataTable dataTable = CommonUiFunction.RetrieveFilteredDataByStatus(spName, userID, paramName, filterId);
            if (dataTable.Columns.Contains("Message"))
            {
                MessageBox.Show(dataTable.Rows[0]["Message"].ToString(),
                                "Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }
            AllCreditData = dataTable;
            currentPage = 1;
            ShowCurrentPage();
            UpdateCreditSummaryCards();
            return true;
        }

        public Boolean LoadFilteredCreditData(string spName, int userId, string paramName1, int paramId1, string paramName2, int paramId2)
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
            AllCreditData = dataTable;
            currentPage = 1;
            ShowCurrentPage();
            UpdateCreditSummaryCards();
            return true;
        }

        public Boolean LoadFilteredCreditData(string spName, int userId, string paramName1, Decimal paramId1, string paramName2, Decimal paramId2)
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
                MessageBox.Show("No Record Found",
                                "Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }
            AllCreditData = dataTable;
            currentPage = 1;
            ShowCurrentPage();
            UpdateCreditSummaryCards();
            return true;
        }

        public Boolean LoadFilteredCreditData(string spName, int userId, string paramName1, DateTime paramId1, string paramName2, DateTime paramId2)
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
            AllCreditData = dataTable;
            currentPage = 1;
            ShowCurrentPage();
            UpdateCreditSummaryCards();
            return true;
        }
        private void dgvCreditDataTable_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex != -1)
                return;

            switch (dgvCreditDataTable.Columns[e.ColumnIndex].Name)
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

        private void DrawHeader(DataGridViewCellPaintingEventArgs e, Image icon, string text)
        {
            e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);
            int iconSize = 16;
            int spacing = 6;

            SizeF textSize = e.Graphics.MeasureString(text, e.CellStyle.Font);

            int totalWidth = iconSize + spacing + (int)textSize.Width;

            int startX = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;

            int iconY = e.CellBounds.Y + (e.CellBounds.Height - iconSize) / 2;

            e.Graphics.DrawImage(icon, startX, iconY, iconSize, iconSize);

            using (Brush brush =
                new SolidBrush(Color.FromArgb(80, 60, 180)))
            {
                float textX = startX + iconSize + spacing;

                float textY = e.CellBounds.Y + (e.CellBounds.Height - textSize.Height) / 2;

                e.Graphics.DrawString(text, e.CellStyle.Font, brush, textX, textY);
            }

            e.Handled = true;
        }

        private void dgvCreditDataTable_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.ColumnIndex < 0)
                return;

            DataGridViewColumn column = dgvCreditDataTable.Columns[e.ColumnIndex];

            string columnName = column.DataPropertyName;

            //  sortable columns
            if (columnName != "CreditAt" &&
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

            ApplyCreditSort();

            currentPage = 1;

            ShowCurrentPage();
        }

        private void ApplyCreditSort()
        {
            if (string.IsNullOrEmpty(sortedColumn) ||
                currentSortOrder == WinFormsSortOrder.None)
                return;

            if (AllCreditData == null ||
                AllCreditData.Rows.Count == 0)
                return;

            if (!AllCreditData.Columns.Contains(sortedColumn))
                return;

            DataView view = AllCreditData.DefaultView;

            string direction =
                currentSortOrder == WinFormsSortOrder.Ascending
                ? "ASC"
                : "DESC";

            view.Sort =
                "[" + sortedColumn + "] " + direction;

            AllCreditData = view.ToTable();
        }
        private void ShowCurrentPage()
        {
            DataTable pageTable = AllCreditData.Clone();
            btnCurrentPage.Text = currentPage.ToString();
            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, AllCreditData.Rows.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                pageTable.ImportRow(AllCreditData.Rows[i]);
            }

            dgvCreditDataTable.DataSource = pageTable;
            Common.CommonUiFunction.HighlightSearch(dgvCreditDataTable, txtSearch);
            int start = startIndex + 1;
            int end = endIndex;
            int total = AllCreditData.Rows.Count;

            lblCreditStartingPageNumber.Text = total == 0 ? "0" : start.ToString();
            lblCreditEndingPageNumber.Text = end.ToString();
            lblCreditTotalPageNumber.Text = total.ToString();
        }

        private int GetRowsPerPage()
        {
            Rectangle display = dgvCreditDataTable.DisplayRectangle;

            int rowHeight = dgvCreditDataTable.RowTemplate.Height;

            return Math.Max(1, display.Height / rowHeight) - 1;
        }

       
        private void CreditControl_Resize(object sender, EventArgs e)
        {
            ApplyRoundCorners();
            if (AllCreditData == null || AllCreditData.Rows.Count == 0)
                return;

            int newPageSize = GetRowsPerPage();

            if (newPageSize != pageSize)
            {
                pageSize = newPageSize;
                ShowCurrentPage();
            }
        }

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
            int totalPages = (int)Math.Ceiling((double)AllCreditData.Rows.Count / pageSize);

            if (currentPage < totalPages)
            {
                currentPage++;
                ShowCurrentPage();
            }
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)AllCreditData.Rows.Count / pageSize);
            if (currentPage != totalPages)
            {
                currentPage = totalPages;
                ShowCurrentPage();
            }
        }

        private void dgvCreditDataTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pnlTotalCredit_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
               e.Graphics,
               pnlTotalCredit.ClientRectangle,
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
            pnlTotalCredit.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pnlTotalCredit.Width, pnlTotalCredit.Height, 15, 15));

            pnlTransactionCard.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pnlTransactionCard.Width, pnlTransactionCard.Height, 15, 15));


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

            LoadCreditData(Session.LogedInUser.GetUserId());
        }

        private void btncategoryClose_Click(object sender, EventArgs e)
        {
            ResetCategoryAndSubCategoryFilters();
        }

        private void tsmiDate_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlDateFilter);
            ignoreEvents = false;
        }

        private void tsmiCategory_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlCategoryFilter);
            ignoreEvents = false;

            Common.CommonUiFunction.LoadInComboBox("spGetCreditCategoriesByUserID", Session.LogedInUser.GetUserId(), "Select Category", cmbCategory);
            CommonUiFunction.SetComboBoxHeightAndOwnerDraw(cmbCategory);
        }

        private void tsmiSubCategory_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlSubCategoryFilter);
            ignoreEvents = false;
            Common.CommonUiFunction.LoadInComboBox("spGetCreditCategoriesByUserID", Session.LogedInUser.GetUserId(), "Select Category", cmbCategorytxt);
            CommonUiFunction.SetComboBoxHeightAndOwnerDraw(cmbCategorytxt);
        }

        private void tsmiAmount_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlAmountFilter);
            ignoreEvents = false;
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

            tsmiDate.Image = Properties.Resources.calendar;
            tsmiCategory.Image = Properties.Resources.shop;
            tsmiAmount.Image = Properties.Resources.money;

            tsmiDate.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsmiCategory.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsmiAmount.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

            tsmiDate.ImageScaling = ToolStripItemImageScaling.None;
            tsmiCategory.ImageScaling = ToolStripItemImageScaling.None;
            tsmiAmount.ImageScaling = ToolStripItemImageScaling.None;

        }

       

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
                ctrl.MouseDown += CreditControls_MouseDown;

                if (ctrl.HasChildren)
                    RegisterMouseDown(ctrl);
            }
        }
        private void CreditControls_MouseDown(object sender, MouseEventArgs e)
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

        
        private void ShowCalenderToDatePanel(Panel panel)
        {
            HidePopupPanels();

            panel.Parent = this;

            Point p = txtToDate.PointToScreen(
                new Point(0, txtToDate.Height + 10));

            p = this.PointToClient(p);

            panel.Location = p;

            panel.BringToFront();
            panel.Visible = true;
        }

        
        private void btnFilter_Click(object sender, EventArgs e)
        {
            cmsFilter.Show(btnFilter, 0, btnFilter.Height);
        }

        private void picCalenderFromDate_Click_1(object sender, EventArgs e)
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

        private void picCalenderToDate_Click_1(object sender, EventArgs e)
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

        private void btnDateClose_Click_1(object sender, EventArgs e)
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
            LoadCreditData(Session.LogedInUser.GetUserId());
        }

        private void btnSubCategoryclose_Click(object sender, EventArgs e)
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

            LoadCreditData(Session.LogedInUser.GetUserId());
        }

        private void UpdateCreditSummaryCards()
        {
            if (AllCreditData == null || AllCreditData.Rows.Count == 0)
            {
                lblCreditAmount.Text = "₹ 0";
                lblTransactionAmount.Text = "0";
                return;
            }

            decimal totalCredit = 0;

            foreach (DataRow row in AllCreditData.Rows)
            {
                if (row["Amount"] != DBNull.Value)
                {
                    totalCredit += Convert.ToDecimal(row["Amount"]);
                }
            }

            lblCreditAmount.Text = "₹ " + totalCredit.ToString("#,##0");
            lblTransactionAmount.Text = AllCreditData.Rows.Count.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            AllCreditData = Common.CommonUiFunction.SearchDataInExpenseOrCredit(masterData, txtSearch);
            ShowCurrentPage();
        }

        private void pnlCategory_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCategoryApply_Click(object sender, EventArgs e)
        {

        }

        private void cmbCategory_Click(object sender, EventArgs e)
        {
            cmbCategory.DroppedDown = true;
        }

        private void cmbCategory_Enter(object sender, EventArgs e)
        {
            if (cmbCategory.Text == "Select Category")
                cmbCategory.ForeColor = Color.Black;
        }

        private void cmbCategory_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbCategory.Text) || cmbCategory.Text == "Select Category")
            {

                cmbCategory.Text = "Select Category";
                cmbCategory.ForeColor = Color.Gray;
            }
            else
            {
                cmbCategory.ForeColor = Color.Black;
            }
        }

        private void cmbCategorytxt_Enter(object sender, EventArgs e)
        {
            if (cmbCategorytxt.Text == "Select Category")
                cmbCategorytxt.ForeColor = Color.Black;
        }

        private void cmbCategorytxt_Click(object sender, EventArgs e)
        {
            if (cmbCategorytxt.Items.Count <= 1)
            {
                Common.CommonUiFunction.LoadInComboBox("spGetCreditCategoriesByUserID", Session.LogedInUser.GetUserId(), "Select Category", cmbCategorytxt);
            }
            cmbCategorytxt.DroppedDown = true;
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
            if (ignoreEvents) return;

            string minStr = txtMinAmount.Text.Trim();
            string maxStr = txtMaxAmount.Text.Trim();

            bool isMinProvided = !string.IsNullOrWhiteSpace(minStr) && minStr != "Enter Amount";
            bool isMaxProvided = !string.IsNullOrWhiteSpace(maxStr) && maxStr != "Enter Amount";

            if (!isMinProvided && !isMaxProvided)
            {
                LoadCreditData(Session.LogedInUser.GetUserId());
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

        private void ApplyAmountFilter(decimal minValue, decimal maxValue)
        {
            if (!LoadFilteredCreditData("spFilterCreditByAmountRange", Session.LogedInUser.GetUserId(), "@MinAmount", minValue, "@MaxAmount", maxValue))
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

            LoadCreditData(Session.LogedInUser.GetUserId());
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
                DateTime defaultToDate = this.fromDate > DateTime.Today ? this.fromDate : DateTime.Today;
                txtToDate.Text = defaultToDate.ToString("dd-MM-yyyy");
                ignoreEvents = false;
                this.toDate = defaultToDate;
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

            CreditBLL creditBll = new CreditBLL();
            creditBll.fromDate = this.fromDate;
            creditBll.toDate = this.toDate;

            CommonValidator.ValidationResult result = creditBll.DateValidatorIntoCreditBll();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    validFromDate = true;
                    if (!LoadFilteredCreditData(
                            "spFilterCreditByDateRange",
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

                        LoadCreditData(Session.LogedInUser.GetUserId());
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
                    LoadCreditData(Session.LogedInUser.GetUserId());
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

            CreditBLL creditBll = new CreditBLL();
            creditBll.fromDate = this.fromDate;
            creditBll.toDate = this.toDate;

            CommonValidator.ValidationResult result = creditBll.DateValidatorIntoCreditBll();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    validFromDate = true;
                    if (!LoadFilteredCreditData(
                            "spFilterCreditByDateRange",
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

                        LoadCreditData(Session.LogedInUser.GetUserId());
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
                    LoadCreditData(Session.LogedInUser.GetUserId());
                    break;
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            if (cmbCategory.SelectedIndex <= 0) return;
            if (cmbCategory.SelectedValue == null) return;
            if (cmbCategory.SelectedValue is DataRowView) return;

            int categoryId = Convert.ToInt32(cmbCategory.SelectedValue);

            if (categoryId == lastSelectedCategoryId) return;

            if (!LoadFilteredCreditData(
                    "spFilterCreditByCategory",
                    "@CategoryID",
                    categoryId,
                    categoryId))
            {
                ignoreEvents = true;
                if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
                if (cmbCategorytxt.Items.Count > 0) cmbCategorytxt.SelectedIndex = 0;
                if (cmbSubCategory.Items.Count > 0) cmbSubCategory.SelectedIndex = 0;
                lastSelectedCategoryId = -1;
                lastSelectedSubCategoryId = -1;
                ignoreEvents = false;

                pnlSubCategoryFilter.Visible = false;
                LoadCreditData(Session.LogedInUser.GetUserId());
                return;
            }

            lastSelectedCategoryId = categoryId;
            lastSelectedSubCategoryId = -1;

            CommonUiFunction.LoadInComboBox(
                "spGetCreditSubCategoryByCategoryID",
                "Select SubCategory",
                cmbSubCategory,
                "@CategoryID",
                categoryId);

            if (!(cmbSubCategory.Items.Count == 2 && cmbSubCategory.GetItemText(cmbSubCategory.Items[1]).Trim().Equals("General", StringComparison.OrdinalIgnoreCase)))
            {
                ignoreEvents = true;
                Common.CommonUiFunction.LoadInComboBox("spGetCreditCategoriesByUserID", Session.LogedInUser.GetUserId(), "Select Category", cmbCategorytxt);
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

            if (!LoadFilteredCreditData("spFilterCreditByCategoryAndSubCategory", Session.LogedInUser.GetUserId(), "@CategoryID", categoryId, "@SubCategoryID", subCategoryId))
            {
                ignoreEvents = true;
                cmbSubCategory.SelectedIndex = 0;
                lastSelectedSubCategoryId = -1;
                ignoreEvents = false;

                if (!LoadFilteredCreditData(
                        "spFilterCreditByCategory",
                        "@CategoryID",
                        categoryId,
                        categoryId))
                {
                    LoadCreditData(Session.LogedInUser.GetUserId());
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

            if (!LoadFilteredCreditData(
                    "spFilterCreditByCategory",
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

                LoadCreditData(Session.LogedInUser.GetUserId());
                return;
            }

            lastSelectedCategoryId = categoryId;
            lastSelectedSubCategoryId = -1;

            CommonUiFunction.LoadInComboBox(
                "spGetCreditSubCategoryByCategoryID",
                "Select SubCategory",
                cmbSubCategory,
                "@CategoryID",
                categoryId);

            if (cmbSubCategory.Items.Count == 2 && cmbSubCategory.GetItemText(cmbSubCategory.Items[1]).Trim().Equals("General", StringComparison.OrdinalIgnoreCase))
            {
                pnlSubCategoryFilter.Visible = false;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ignoreEvents = true;

            // Clear search
            txtSearch.Clear();

            // Clear Date Filter
            txtFromdate.Clear();
            txtToDate.Clear();

            fromDate = DateTime.MinValue;
            toDate = DateTime.MinValue;

            validFromDate = false;
            validToDate = false;

            // Reset Category
            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;

            if (cmbCategorytxt.Items.Count > 0)
                cmbCategorytxt.SelectedIndex = 0;

            // Reset Sub Category
            if (cmbSubCategory.Items.Count > 0)
                cmbSubCategory.SelectedIndex = 0;

            // Clear Amount Filter
            txtMinAmount.Text = "Enter Amount";
            txtMinAmount.ForeColor = Color.Gray;

            txtMaxAmount.Text = "Enter Amount";
            txtMaxAmount.ForeColor = Color.Gray;

            // Reset selected filter IDs
            lastSelectedCategoryId = -1;
            lastSelectedSubCategoryId = -1;

            // Hide filter panels
            HideAllFilterPanels();
            HidePopupPanels();

            // Clear validation
            errorProvider1.Clear();
            ErrorHelper.HideErrorForControl(pnlFromDate);
            ErrorHelper.HideErrorForControl(pnlToDate);

            ignoreEvents = false;

            // Load ALL original data again
            LoadCreditData(Session.LogedInUser.GetUserId());
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (AllCreditData == null || AllCreditData.Rows.Count == 0)
            {
                MessageBox.Show(
                    "There is no data to export.",
                    "Export",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "Save Credit Excel File";
                saveDialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                saveDialog.FileName =
                    "Credit_" +
                    DateTime.Now.ToString("ddMMyyyy_HHmmss") +
                    ".xlsx";

                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    ExportCreditToExcel(
                        AllCreditData,
                        saveDialog.FileName);

                    MessageBox.Show(
                        "Credit data exported successfully.",
                        "Export Successful",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Export failed.\n\n" + ex.Message,
                        "Export Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void ExportCreditToExcel(DataTable dataTable, string filePath)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                excelApp = new Excel.Application();
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                workbook = excelApp.Workbooks.Add(
                    Excel.XlWBATemplate.xlWBATWorksheet);

                worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                worksheet.Name = "Credit";

                // Column Names
                for (int col = 0;
                     col < dataTable.Columns.Count;
                     col++)
                {
                    worksheet.Cells[1, col + 1] =
                        GetCreditExportColumnName(
                            dataTable.Columns[col].ColumnName);
                }

                // Data
                for (int row = 0;
                     row < dataTable.Rows.Count;
                     row++)
                {
                    for (int col = 0;
                         col < dataTable.Columns.Count;
                         col++)
                    {
                        if (dataTable.Rows[row][col] != DBNull.Value)
                        {
                            worksheet.Cells[row + 2, col + 1] =
                                dataTable.Rows[row][col].ToString();
                        }
                    }
                }

                // Header bold
                Excel.Range headerRange =
                    worksheet.Range[
                        worksheet.Cells[1, 1],
                        worksheet.Cells[
                            1,
                            dataTable.Columns.Count]];

                headerRange.Font.Bold = true;

                // Auto fit columns
                worksheet.Columns.AutoFit();

                // SAVE EXCEL FILE
                workbook.SaveAs(
                    filePath,
                    Excel.XlFileFormat.xlOpenXMLWorkbook);

                // Close Excel without opening it
                workbook.Close(false);
                excelApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Excel export failed.\n\n" + ex.Message,
                    "Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                if (workbook != null)
                {
                    try
                    {
                        workbook.Close(false);
                    }
                    catch { }
                }

                if (excelApp != null)
                {
                    try
                    {
                        excelApp.Quit();
                    }
                    catch { }
                }
            }
            finally
            {
                // Release COM objects
                if (worksheet != null)
                    Marshal.ReleaseComObject(worksheet);

                if (workbook != null)
                    Marshal.ReleaseComObject(workbook);

                if (excelApp != null)
                    Marshal.ReleaseComObject(excelApp);

                worksheet = null;
                workbook = null;
                excelApp = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        private string GetCreditExportColumnName(string columnName)
        {
            switch (columnName)
            {
                
                case "CreditAt":
                    return "Date";

                case "Description":
                    return "Description";

                case "CategoryName":
                    return "Category";

                case "SubCategoryName":
                    return "Sub Category";

                case "Amount":
                    return "Amount";

                case "PaymentName":
                    return "Payment Method";

                default:
                    return columnName;
            }
        }


        

        
    }
}