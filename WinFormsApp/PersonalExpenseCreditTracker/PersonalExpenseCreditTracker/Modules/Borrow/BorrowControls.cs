using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormsSortOrder = System.Windows.Forms.SortOrder;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using PersonalExpenseCreditTracker.Common;
using BLLayer.Common;
using BLLayer.Borrow;
using Excel = Microsoft.Office.Interop.Excel;
//using PersonalExpenseCreditTracker.Modules.Borrow.PayBorrowAmountControls;


namespace PersonalExpenseCreditTracker.Modules.Borrow
{
    public partial class BorrowControls : Form
    {
        private string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private DataTable AllBorrowData = new DataTable();
        private DataTable masterData = new DataTable();
        private int currentPage = 1;
        private int pageSize = 0;
        private int userID = Session.LogedInUser.GetUserId();
        private string sortedColumn = "BorrowAt";
        private System.Windows.Forms.SortOrder currentSortOrder = System.Windows.Forms.SortOrder.Descending;

        private bool ignoreEvents { get; set; }
        private int lastSelectedCategoryId { get; set; }
        private int lastSelectedSubCategoryId { get; set; }

        private DateTime fromDate { get; set; }
        private DateTime toDate { get; set; }
        private bool validFromDate { get; set; }
        private bool validToDate { get; set; }
        private static readonly string[] DateFormats = { "dd-MM-yyyy", "d-M-yyyy", "dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd" };
        public BorrowControls()
        {
            InitializeComponent();
            StyleBorrowGrid();

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnFilter, "Filter Borrow");
            toolTip.SetToolTip(btnRefresh, "Refresh List");
            toolTip.SetToolTip(btnExport, "Export Borrow");

            dgvBorrowDataTable.AutoGenerateColumns = false;
            dgvBorrowDataTable.CellDoubleClick += dgvBorrowDataTable_CellDoubleClick;

            ApplyRoundCorners();

            this.Resize += BorrowControls_Resize;
            this.MouseDown += BorrowControls_MouseDown;
            RegisterMouseDown(this);
            typeof(DataGridView).InvokeMember(
               "DoubleBuffered",
               System.Reflection.BindingFlags.NonPublic |
               System.Reflection.BindingFlags.Instance |
               System.Reflection.BindingFlags.SetProperty,
               null,
               dgvBorrowDataTable,
               new object[] { true });
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);


        private void BorrowControls_Resize(object sender, EventArgs e)
        {
            ApplyRoundCorners();

            if (AllBorrowData == null || AllBorrowData.Rows.Count == 0)
                return;

            int newPageSize = GetRowsPerPage();

            if (newPageSize != pageSize)
            {
                pageSize = newPageSize;

                int totalPages = (int)Math.Ceiling((double)AllBorrowData.Rows.Count / pageSize);

                if (currentPage > totalPages)
                    currentPage = totalPages;

                ShowCurrentPage();
            }
        }

        private void ApplyRoundCorners()
        {
            pnlBorrowTotalBorrowed.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    pnlBorrowTotalBorrowed.Width,
                    pnlBorrowTotalBorrowed.Height,
                    15,
                    15));

            pnlPaidAmount.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    pnlRepaidAmount.Width,
                    pnlRepaidAmount.Height,
                    15,
                    15));


            pnlActiveBorrowings.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    pnlActiveBorrowings.Width,
                    pnlActiveBorrowings.Height,
                    15,
                    15));

            pnlRepaidAmount.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    pnlRepaidAmount.Width,
                    pnlRepaidAmount.Height,
                    15,
                    15));
        }

        private void BorrowControls_Load(object sender, EventArgs e)
        {
            ignoreEvents = true;
            txtSearch.Text = "Search...";
            txtSearch.ForeColor = Color.Gray;
            txtMinAmount.Text = "Enter Amount";
            txtMinAmount.ForeColor = Color.Gray;
            txtMaxAmount.Text = "Enter Amount";
            txtMaxAmount.ForeColor = Color.Gray;

            CommonUiFunction.LoadInComboBox("spGetAllPersons", Session.LogedInUser.GetUserId(), "Select Person", cmbPerson);
            CommonUiFunction.LoadInComboBox("spGetAllPaymentTypes", "Select Payment Type", cmbPayment);
            CommonUiFunction.LoadInComboBox("spGetAllLentBorrowStatus", "Select Status", cmbStatus);

            CommonUiFunction.SetComboBoxHeightAndOwnerDraw(cmbPerson);
            CommonUiFunction.SetComboBoxHeightAndOwnerDraw(cmbPayment);
            CommonUiFunction.SetComboBoxHeightAndOwnerDraw(cmbStatus);

            cmbPerson.ForeColor = Color.Gray;
            cmbPayment.ForeColor = Color.Gray;
            cmbStatus.ForeColor = Color.Gray;

            cmbPerson.SelectedIndexChanged += cmbPerson_SelectedIndexChanged;
            cmbPayment.SelectedIndexChanged += cmbPayment_SelectedIndexChanged;
            cmbStatus.SelectedIndexChanged += cmbStatus_SelectedIndexChanged;

            btnRefresh.Click += btnRefresh_Click;

            ApplyRoundCorners();
            dgvBorrowDataTable.CellPainting += dgvBorrowDataTable_CellPainting;
            pageSize = GetRowsPerPage();
            int userID = Session.LogedInUser.GetUserId();
            LoadBorrowData(userID);
            HideAllFilterPanels();
           //ShowSearchPanel(pnlSearch);
            pnlSearch.Visible = true;
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
            tsmiAmount.AutoSize = false;
            tsmiPayment.AutoSize = false;
            tsmiPerson.AutoSize = false;
            tsmiStatus.AutoSize = false;

            tsmiDate.Width = cmsFilter.Width;
            tsmiAmount.Width = cmsFilter.Width;
            tsmiStatus.Width = cmsFilter.Width;
            tsmiPerson.Width = cmsFilter.Width;
            tsmiPayment.Width = cmsFilter.Width;
        }
        public void LoadBorrowData(int userID)
        {
            DataTable dataTable = CommonUiFunction.RetrieveDataForGridView("spGetAllBorrow", userID);
            if (dataTable == null || dataTable.Columns.Contains("Message") || dataTable.Rows.Count == 0)
            {
                dgvBorrowDataTable.DataSource = null;
                lblBorrowStartingPageNumber.Text = "0";
                lblBorrowEndingPageNumber.Text = "0";
                lblBorrowTotalPageNumber.Text = "0";
                lblBorrowTotalBorrowedAmount.Text = "₹ 0";
                lblBorrowPaidAmount.Text = "₹ 0";
                lblBorrowActiveBorrowingsAmount.Text = "₹ 0";
                lblBorrowRepaidAmount.Text = "0";
                return;
            }

            AllBorrowData = dataTable;
            masterData = dataTable.Copy();
            sortedColumn = "BorrowAt";
            currentSortOrder = System.Windows.Forms.SortOrder.Descending;
            ApplyBorrowSort();

            currentPage = 1;
            ShowCurrentPage();
        }


       
        public Boolean LoadFilteredBorrowtData(string spName, string paramName, int filterId)
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
            if (dataTable.Rows.Count <= 0)
            {
                MessageBox.Show("No Record Found",
                                "Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }
            AllBorrowData = dataTable;
            masterData = dataTable.Copy();
            currentPage = 1;
            ShowCurrentPage();
            return true;
        }

        public Boolean LoadFilteredBorrowData(string spName, int userId, string paramName1, DateTime paramId1, string paramName2, DateTime paramId2)
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
            AllBorrowData = dataTable;
            masterData = dataTable.Copy();
            currentPage = 1;
            ShowCurrentPage();
            return true;
        }
        //
        public Boolean LoadFilteredBorowData(string spName, int userId, string paramName1, Decimal paramId1, string paramName2, Decimal paramId2)
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
            AllBorrowData = dataTable;
            masterData = dataTable.Copy();
            currentPage = 1;
            ShowCurrentPage();
            return true;
        }



        private void StyleBorrowGrid()
        {

            colDate.DataPropertyName = "BorrowAt";
            colPersonID.DataPropertyName = "PersonID";
            colPersonName.DataPropertyName = "PersonName";
            colPaymentType.DataPropertyName = "PaymentName";
            colStatus.DataPropertyName = "StatusName";
            colAmount.DataPropertyName = "Amount";
            colPaidAmount.DataPropertyName = "PaidAmount";
            colRemainingAmount.DataPropertyName = "RemainingAmount";
            colDeadline.DataPropertyName = "DeadlineAt";
            colDescription.DataPropertyName = "Description";
            colPersonID.Visible = false;

            //Column Style
            dgvBorrowDataTable.AllowUserToOrderColumns = false;
            dgvBorrowDataTable.AutoGenerateColumns = false;
            dgvBorrowDataTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //Column HeaderStyle
            dgvBorrowDataTable.EnableHeadersVisualStyles = false;
            dgvBorrowDataTable.ColumnHeadersHeight = 45;
            dgvBorrowDataTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvBorrowDataTable.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvBorrowDataTable.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvBorrowDataTable.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 245, 255);
            dgvBorrowDataTable.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(80, 60, 180);
            dgvBorrowDataTable.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ////Column Background Color
            //colDate.DefaultCellStyle.BackColor = Color.White;
            //colPersonName.DefaultCellStyle.BackColor = Color.White;
            //colPaymentType.DefaultCellStyle.BackColor = Color.White;
            //colStatus.DefaultCellStyle.BackColor = Color.White;
            //colAmount.DefaultCellStyle.BackColor = Color.White;
            //colPaidAmount.DefaultCellStyle.BackColor = Color.White;
            //colRemainingAmount.DefaultCellStyle.BackColor = Color.White;
            //colDeadline.DefaultCellStyle.BackColor = Color.White;
            //colDescription.DefaultCellStyle.BackColor = Color.White;

            //Column FontStyle
            colDate.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colPersonName.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colPaymentType.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colStatus.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colAmount.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colPaidAmount.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colRemainingAmount.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colDeadline.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colDescription.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            //Row Style
            dgvBorrowDataTable.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvBorrowDataTable.RowTemplate.Height = 40;
            dgvBorrowDataTable.RowHeadersVisible = false;
            dgvBorrowDataTable.MultiSelect = false;
            dgvBorrowDataTable.ReadOnly = true;
            dgvBorrowDataTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Zigzag

            Color selectedRowColor = Color.FromArgb(174, 205, 247);
            // Normal Row
            dgvBorrowDataTable.DefaultCellStyle.BackColor = Color.White;
            dgvBorrowDataTable.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);

            // Alternating Row
            dgvBorrowDataTable.AlternatingRowsDefaultCellStyle.BackColor =  Color.FromArgb(244, 247, 250);

            dgvBorrowDataTable.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);

            // Selection
            dgvBorrowDataTable.DefaultCellStyle.SelectionBackColor = selectedRowColor;

            dgvBorrowDataTable.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvBorrowDataTable.AlternatingRowsDefaultCellStyle.SelectionBackColor =  selectedRowColor;

            dgvBorrowDataTable.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;

            // Force every column to use the same selection color
            foreach (DataGridViewColumn column in dgvBorrowDataTable.Columns)
            {
                column.DefaultCellStyle.SelectionBackColor =
                    selectedRowColor;

                column.DefaultCellStyle.SelectionForeColor =
                    Color.Black;
            }


            // Border Style
            dgvBorrowDataTable.BorderStyle = BorderStyle.None;
            dgvBorrowDataTable.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvBorrowDataTable.GridColor = Color.Gainsboro;
            dgvBorrowDataTable.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;


            colAmount.DefaultCellStyle.ForeColor = Color.Red;
            colPaidAmount.DefaultCellStyle.ForeColor = Color.Red;
            colRemainingAmount.DefaultCellStyle.ForeColor = Color.Red;
            colDeadline.DefaultCellStyle.ForeColor = Color.Purple;
            colPaymentType.DefaultCellStyle.ForeColor = Color.Blue;
            colStatus.DefaultCellStyle.ForeColor = Color.Green;


            // Cell Alignment
            colDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPersonName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPaymentType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPaidAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colRemainingAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDeadline.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDescription.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void dgvBorrowDataTable_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.ColumnIndex < 0)
                return;

            DataGridViewColumn column = dgvBorrowDataTable.Columns[e.ColumnIndex];

            string columnName = column.DataPropertyName;

            //  sortable columns
            if (columnName != "BorrowAt" &&
                columnName != "PersonID" &&
                columnName != "PersonName" &&
                columnName != "PaymentName" &&
                columnName != "StatusName" &&
                columnName != "Amount" &&
                columnName != "PaidAmount" &&
                columnName != "RemainingAmount" &&
                columnName != "DeadlineAt" &&
                columnName != "Description")
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

            ApplyBorrowSort();

            currentPage = 1;

            ShowCurrentPage();
        }



        private void ApplyBorrowSort()
        {
            if (string.IsNullOrEmpty(sortedColumn) ||
                currentSortOrder == WinFormsSortOrder.None)
                return;

            if (AllBorrowData == null ||
                AllBorrowData.Rows.Count == 0)
                return;

            if (!AllBorrowData.Columns.Contains(sortedColumn))
                return;

            DataView view = AllBorrowData.DefaultView;

            string direction =
                currentSortOrder == WinFormsSortOrder.Ascending
                ? "ASC"
                : "DESC";

            view.Sort =
                "[" + sortedColumn + "] " + direction;

            AllBorrowData = view.ToTable();
        }
        private void ShowCurrentPage()
        {
            DataTable pageTable = AllBorrowData.Clone();

            btnCurrentPage.Text = currentPage.ToString();

            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, AllBorrowData.Rows.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                pageTable.ImportRow(AllBorrowData.Rows[i]);
            }

            dgvBorrowDataTable.DataSource = pageTable;
            Common.CommonUiFunction.HighlightSearch(dgvBorrowDataTable, txtSearch);



            int start = startIndex + 1;
            int end = endIndex;
            int total = AllBorrowData.Rows.Count;

            lblBorrowStartingPageNumber.Text = total == 0 ? "0" : start.ToString();
            lblBorrowEndingPageNumber.Text = end.ToString();
            lblBorrowTotalPageNumber.Text = total.ToString();

            if (AllBorrowData != null && AllBorrowData.Rows.Count > 0)
            {
                // Total Lent Amount
                decimal totalBorrow = AllBorrowData.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));

                // Total Repaid Amount
                decimal totalPaid = AllBorrowData.AsEnumerable().Sum(row => row.Field<decimal>("PaidAmount"));

                // Total Due Amount
                decimal totalDue = AllBorrowData.AsEnumerable().Sum(row => row.Field<decimal>("RemainingAmount"));

                // Total Transactions
                int totalTransaction = AllBorrowData.Rows.Count;
                
                // Display
                 this.lblBorrowTotalBorrowedAmount.Text = "₹ " + totalBorrow.ToString("#,##0.##");
                 this.lblBorrowPaidAmount.Text = "₹ " + totalPaid.ToString("#,##0.##");
                 this.lblBorrowActiveBorrowingsAmount.Text = "₹ " + totalDue.ToString("#,##0.##");
                 this.lblBorrowRepaidAmount.Text = totalTransaction.ToString();
                 
            }
            else
            {
                lblBorrowTotalBorrowedAmount.Text = "₹ 0";
                this.lblBorrowPaidAmount.Text = "₹ 0";
                this.lblBorrowActiveBorrowingsAmount.Text = "₹ 0";
                this.lblBorrowRepaidAmount.Text = "0";
            }
        }

        private int GetRowsPerPage()
        {
            Rectangle display = dgvBorrowDataTable.DisplayRectangle;

            int rowHeight = dgvBorrowDataTable.RowTemplate.Height;

            return Math.Max(1, display.Height / rowHeight) - 1;
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
        private void btnNextPage_Click(object sender, EventArgs e)
        {
            int totalPages = Math.Max(1, (int)Math.Ceiling((double)AllBorrowData.Rows.Count / pageSize));

            if (currentPage < totalPages)
            {
                currentPage++;
                ShowCurrentPage();
            }
        }
        private void btnLastPage_Click(object sender, EventArgs e)
        {
            int totalPages = Math.Max(1, (int)Math.Ceiling((double)AllBorrowData.Rows.Count / pageSize));
            if (currentPage != totalPages)
            {
                currentPage = totalPages;
                ShowCurrentPage();
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

        private void dgvBorrowDataTable_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex != -1)
                return;

            switch (dgvBorrowDataTable.Columns[e.ColumnIndex].Name)
            {
                case "colDate":
                    DrawHeader(e, Properties.Resources.date, "Date");
                    break;

                case "colPersonName":
                    DrawHeader(e, Properties.Resources.PersonIcon, "PersonName");
                    break;
                case "colDescription":
                    DrawHeader(e, Properties.Resources.note, "Description");
                    break;
                case "colAmount":
                    DrawHeader(e, Properties.Resources.money, "Amount");
                    break;
                case "colPaymentType":
                    DrawHeader(e, Properties.Resources.credit_card1, "PaymentType");
                    break;
                case "colPaidAmount":
                    DrawHeader(e, Properties.Resources.money, "PaidAmount");
                    break;
                case "colRemainingAmount":
                    DrawHeader(e, Properties.Resources.money, "RemainingAmount");
                    break;
                case "colDeadline":
                    DrawHeader(e, Properties.Resources.deadline, "DeadlineAt");
                    break;
                case "colStatus":
                    DrawHeader(e, Properties.Resources.loading, "Status");
                    break;
            }

        }

        private void lblBorrowRepaidAmount_Click(object sender, EventArgs e)
        {

        }

        private void pnlBorrowTotalBorrowed_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlBorrowTotalBorrowed.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void pnlPaidAmount_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlPaidAmount.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void lblBorrowActiveBorrowingsAmount_Click(object sender, EventArgs e)
        {

        }

        private void pnlActiveBorrowings_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlActiveBorrowings.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void pnlRepaidAmount_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlRepaidAmount.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void dgvBorrowDataTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      

    

        private void dgvBorrowDataTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvBorrowDataTable.Rows[e.RowIndex];

            int borrowId = 0;

            DataRowView drv = row.DataBoundItem as DataRowView;
            if (drv != null && drv.Row.Table.Columns.Contains("BorrowID"))
            {
                borrowId = Convert.ToInt32(drv["BorrowID"]);
            }

            string personName = Convert.ToString(row.Cells["colPersonName"].Value);
            string totalAmount = Convert.ToString(row.Cells["colAmount"].Value);
            string remainingAmount = Convert.ToString(row.Cells["colRemainingAmount"].Value);
            string status = Convert.ToString(row.Cells["colStatus"].Value);
            string paidAmount = Convert.ToString(row.Cells["colPaidAmount"].Value);

            using (PayBorrowPaidAmountControls frm = new PayBorrowPaidAmountControls())
            {
                frm.SetBorrowDetails(
                    borrowId,
                    personName,
                    totalAmount,
                    remainingAmount,
                    status,
                    paidAmount);

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    int userID = PersonalExpenseCreditTracker.Session.LogedInUser.GetUserId();
                    LoadBorrowData(userID);
                }
            }
        }
        private void HideAllFilterPanels()
        {
            pnlDateFilter.Visible = false;
            pnlAmountFilter.Visible = false;
           
            pnlPaymentFilter.Visible = false;
            pnlPersonFilter.Visible = false;
            pnlStatusFilter.Visible = false;
        }
        private void HidePopupPanels()
        {
            pnlFromDateCalenderShow.Visible = false;
            pnlToDateCalenderShow.Visible = false;
        }



        private void btnFilter_Click(object sender, EventArgs e)
        {
            cmsFilter.Show(btnFilter, 0, btnFilter.Height);
        }


        private void ShowFilterPanel(Panel panel)
        {
            HideAllFilterPanels();

            Point p = pnlButton.PointToScreen(Point.Empty);
            p = this.PointToClient(p);

            panel.Parent = this;

            panel.Location = new Point(
                p.X - panel.Width - 10,
                p.Y);

            panel.BringToFront();
            panel.Visible = true;
        }




       

        private void tsmiDate_Click_1(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlDateFilter);
        }

        private void tsmiCategory_Click_1(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlAmountFilter);
        }

        private void tsmiPerson_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlPersonFilter);
        }

        private void tsmiStatus_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlStatusFilter);
        }

        private void tsmiPayment_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlPaymentFilter);
        }


        private void btncategoryClose_Click(object sender, EventArgs e)
        {
            pnlAmountFilter.Visible = false;
            ignoreEvents = true;
            txtMinAmount.Text = "Enter Amount";
            txtMinAmount.ForeColor = Color.Gray;
            txtMaxAmount.Text = "Enter Amount";
            txtMaxAmount.ForeColor = Color.Gray;
            ignoreEvents = false;

            pnlAmountFilter.Visible = false;

            LoadBorrowData(Session.LogedInUser.GetUserId());
        }

        private void DesignContextMenu()
        {
            cmsFilter.ShowImageMargin = true;
            cmsFilter.ShowCheckMargin = false;
            cmsFilter.ImageScalingSize = new Size(10, 10);

            tsmiDate.AutoSize = false;
            tsmiDate.Height = 30;

            tsmiAmount.AutoSize = false;
            tsmiAmount.Height = 30;

            tsmiPerson.AutoSize = false;
            tsmiPerson.Height = 30;

            tsmiPayment.AutoSize = false;
            tsmiPayment.Height = 30;

            tsmiStatus.AutoSize = false;
            tsmiStatus.Height = 30;

            tsmiDate.Image = Properties.Resources.calendar;
            tsmiAmount.Image = Properties.Resources.shop;
            tsmiPayment.Image = Properties.Resources.credit_card1;
            tsmiPerson.Image = Properties.Resources.PersonIcon;
            tsmiStatus.Image = Properties.Resources.loading;

            tsmiDate.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsmiAmount.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsmiPerson.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsmiPayment.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsmiStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;


            tsmiDate.ImageScaling = ToolStripItemImageScaling.None;
            tsmiAmount.ImageScaling = ToolStripItemImageScaling.None;
            tsmiPerson.ImageScaling = ToolStripItemImageScaling.None;
            tsmiPayment.ImageScaling = ToolStripItemImageScaling.None;
            tsmiStatus.ImageScaling = ToolStripItemImageScaling.None;


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
                ctrl.MouseDown += BorrowControls_MouseDown;

                if (ctrl.HasChildren)
                    RegisterMouseDown(ctrl);
            }
        }
        private void BorrowControls_MouseDown(object sender, MouseEventArgs e)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnlToDateCalenderShow_Paint(object sender, PaintEventArgs e)
        {

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
       
      

        private void lblToDate_Click(object sender, EventArgs e)
        {

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
            LoadBorrowData(Session.LogedInUser.GetUserId());
        }

        private void btnPersonClose_Click(object sender, EventArgs e)
        {
            ignoreEvents = true;
            cmbPerson.SelectedIndex = 0;
            cmbPerson.ForeColor = Color.Gray;
            ignoreEvents = false;
            pnlPersonFilter.Visible = false;
            LoadBorrowData(Session.LogedInUser.GetUserId());
        }

        private void btnStatusClose_Click(object sender, EventArgs e)
        {
            ignoreEvents = true;
            cmbStatus.SelectedIndex = 0;
            cmbStatus.ForeColor = Color.Gray;
            ignoreEvents = false;
            pnlStatusFilter.Visible = false;
            LoadBorrowData(Session.LogedInUser.GetUserId());
        }

        private void btnPaymentClose_Click(object sender, EventArgs e)
        {
            ignoreEvents = true;
            cmbPayment.SelectedIndex = 0;
            cmbPayment.ForeColor = Color.Gray;
            ignoreEvents = false;
            pnlPaymentFilter.Visible = false;
            LoadBorrowData(Session.LogedInUser.GetUserId());
        }

        private void cmbPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            int personId;
            if (cmbPerson.SelectedValue != null && int.TryParse(cmbPerson.SelectedValue.ToString(), out personId))
            {
                if (personId > 0)
                {
                    cmbPerson.ForeColor = Color.Black;
                    if (!LoadFilteredBorrowtData("spFilterBorrowByPerson", "@PersonID", personId))
                    {
                        ignoreEvents = true;
                        cmbPerson.SelectedIndex = 0;
                        cmbPerson.ForeColor = Color.Gray;
                        ignoreEvents = false;
                        LoadBorrowData(Session.LogedInUser.GetUserId());
                    }
                }
                else
                {
                    cmbPerson.ForeColor = Color.Gray;
                    LoadBorrowData(Session.LogedInUser.GetUserId());
                }
            }
        }

        private void cmbPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            int paymentId;
            if (cmbPayment.SelectedValue != null && int.TryParse(cmbPayment.SelectedValue.ToString(), out paymentId))
            {
                if (paymentId > 0)
                {
                    cmbPayment.ForeColor = Color.Black;
                    if (!LoadFilteredBorrowtData("spFilterBorrowByPaymentMethod", "@PaymentID", paymentId))
                    {
                        ignoreEvents = true;
                        cmbPayment.SelectedIndex = 0;
                        cmbPayment.ForeColor = Color.Gray;
                        ignoreEvents = false;
                        LoadBorrowData(Session.LogedInUser.GetUserId());
                    }
                }
                else
                {
                    cmbPayment.ForeColor = Color.Gray;
                    LoadBorrowData(Session.LogedInUser.GetUserId());
                }
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            int statusId;
            if (cmbStatus.SelectedValue != null && int.TryParse(cmbStatus.SelectedValue.ToString(), out statusId))
            {
                if (statusId > 0)
                {
                    cmbStatus.ForeColor = Color.Black;
                    if (!LoadFilteredBorrowtData("spFilterBorrowByStatus", "@StatusID", statusId))
                    {
                        ignoreEvents = true;
                        cmbStatus.SelectedIndex = 0;
                        cmbStatus.ForeColor = Color.Gray;
                        ignoreEvents = false;
                        LoadBorrowData(Session.LogedInUser.GetUserId());
                    }
                }
                else
                {
                    cmbStatus.ForeColor = Color.Gray;
                    LoadBorrowData(Session.LogedInUser.GetUserId());
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            HideAllFilterPanels();
            HidePopupPanels();
            ignoreEvents = true;
            txtFromdate.Clear();
            txtToDate.Clear();
            txtMinAmount.Text = "Enter Amount";
            txtMinAmount.ForeColor = Color.Gray;
            txtMaxAmount.Text = "Enter Amount";
            txtMaxAmount.ForeColor = Color.Gray;
            if (cmbPerson.Items.Count > 0)
            {
                cmbPerson.SelectedIndex = 0;
                cmbPerson.ForeColor = Color.Gray;
            }
            if (cmbPayment.Items.Count > 0)
            {
                cmbPayment.SelectedIndex = 0;
                cmbPayment.ForeColor = Color.Gray;
            }
            if (cmbStatus.Items.Count > 0)
            {
                cmbStatus.SelectedIndex = 0;
                cmbStatus.ForeColor = Color.Gray;
            }
            txtSearch.Clear();
            txtSearch_Leave(txtSearch, EventArgs.Empty);
            ignoreEvents = false;
            currentPage = 1;
            LoadBorrowData(Session.LogedInUser.GetUserId());
            this.Refresh();
        }

         private void txtSearch_TextChanged(object sender, EventArgs e)
         {
             AllBorrowData = Common.CommonUiFunction.SearchDataInLentOrBorrow(masterData, txtSearch);
             ShowCurrentPage();
         }

         private void cmbPayment_Click(object sender, EventArgs e)
         {
             cmbPayment.DroppedDown = true;
         }

         private void cmbPayment_Enter(object sender, EventArgs e)
         {
             if (cmbPayment.SelectedIndex <= 0)
                 cmbPayment.ForeColor = Color.Black;
         }

         private void cmbPayment_Leave(object sender, EventArgs e)
         {
             if (cmbPayment.SelectedIndex <= 0)
             {
                 cmbPayment.ForeColor = Color.Gray;
             }
             else
             {
                 cmbPayment.ForeColor = Color.Black;
             }
         }

         private void cmbStatus_Click(object sender, EventArgs e)
         {
             cmbStatus.DroppedDown = true;
         }

         private void cmbStatus_Enter(object sender, EventArgs e)
         {
             if (cmbStatus.SelectedIndex <= 0)
                 cmbStatus.ForeColor = Color.Black;
         }

         private void cmbStatus_Leave(object sender, EventArgs e)
         {
             if (cmbStatus.SelectedIndex <= 0)
             {
                 cmbStatus.ForeColor = Color.Gray;
             }
             else
             {
                 cmbStatus.ForeColor = Color.Black;
             }
         }

         private void cmbPerson_Click(object sender, EventArgs e)
         {
             cmbPerson.DroppedDown = true;
         }

         private void cmbPerson_Enter(object sender, EventArgs e)
         {
             if (cmbPerson.SelectedIndex <= 0)
                 cmbPerson.ForeColor = Color.Black;
         }

         private void cmbPerson_Leave(object sender, EventArgs e)
         {
             if (cmbPerson.SelectedIndex <= 0)
             {
                 cmbPerson.ForeColor = Color.Gray;
             }
             else
             {
                 cmbPerson.ForeColor = Color.Black;
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
         private void ExecuteAmountFilter()
         {
             if (ignoreEvents) return;

             string minStr = txtMinAmount.Text.Trim();
             string maxStr = txtMaxAmount.Text.Trim();

             bool isMinProvided = !string.IsNullOrWhiteSpace(minStr) && minStr != "Enter Amount";
             bool isMaxProvided = !string.IsNullOrWhiteSpace(maxStr) && maxStr != "Enter Amount";

             if (!isMinProvided && !isMaxProvided)
             {
                 LoadBorrowData(Session.LogedInUser.GetUserId());
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
             //if (!LoadFilteredBorrowData("spFilterCreditByAmountRange", Session.LogedInUser.GetUserId(), "@MinAmount", minValue, "@MaxAmount", maxValue))
             if(!LoadFilteredBorowData("spFilterBorrowByAmountRange",Session.LogedInUser.GetUserId(),"@MinAmount",minValue,"@MaxAmount",maxValue))
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

             LoadBorrowData(Session.LogedInUser.GetUserId());
             
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

         private void monthCalendarToDate_DateSelected(object sender, DateRangeEventArgs e)
         {
             toDate = e.Start.Date;
             txtToDate.Text = e.Start.ToString("dd-MM-yyyy");
             pnlToDateCalenderShow.Visible = false;
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

             //CreditBLL creditBll = new CreditBLL();
             //creditBll.fromDate = this.fromDate;
             //creditBll.toDate = this.toDate;

             BorrowBLL borrowBll = new BorrowBLL();
             borrowBll.fromDate = this.fromDate;
             borrowBll.toDate = this.toDate;

             CommonValidator.ValidationResult result = borrowBll.DateValidatorIntoBorrowBll();

             switch (result)
             {
                 case CommonValidator.ValidationResult.Success:
                     validFromDate = true;
                     if (!LoadFilteredBorrowData(
                             "spFilterBorrowByDateRange",
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

                         LoadBorrowData(Session.LogedInUser.GetUserId());
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
                     LoadBorrowData(Session.LogedInUser.GetUserId());
                     break;
             }
         }

         private void monthCalendarFromDate_DateSelected(object sender, DateRangeEventArgs e)
         {
             fromDate = e.Start.Date;
             txtFromdate.Text = e.Start.ToString("dd-MM-yyyy");
             pnlFromDateCalenderShow.Visible = false;
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

             BorrowBLL borrowBll = new BorrowBLL();
             borrowBll.fromDate = this.fromDate;
             borrowBll.toDate = this.toDate;

             CommonValidator.ValidationResult result = borrowBll.DateValidatorIntoBorrowBll();

             switch (result)
             {
                 case CommonValidator.ValidationResult.Success:
                     validFromDate = true;
                     if (!LoadFilteredBorrowData(
                             "spFilterBorrowByDateRange",
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

                         LoadBorrowData(Session.LogedInUser.GetUserId());
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
                     LoadBorrowData(Session.LogedInUser.GetUserId());
                     break;
             }
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

         private void btnExport_Click(object sender, EventArgs e)
         {
             if (AllBorrowData == null || AllBorrowData.Rows.Count == 0)
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
                 saveDialog.Title = "Save Borrow Excel File";
                 saveDialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                 saveDialog.FileName =
                     "Borrow_" +
                     DateTime.Now.ToString("ddMMyyyy_HHmmss") +
                     ".xlsx";

                 if (saveDialog.ShowDialog() != DialogResult.OK)
                     return;

                 try
                 {
                     ExportBorrowToExcel(
                         AllBorrowData,
                         saveDialog.FileName);

                     MessageBox.Show(
                         "Borrow data exported successfully.",
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
         private void ExportBorrowToExcel(DataTable dataTable, string filePath)
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
                 worksheet.Name = "Borrow";

                 // Column Names
                 for (int col = 0;
                      col < dataTable.Columns.Count;
                      col++)
                 {
                     worksheet.Cells[1, col + 1] =
                         GetBorrowExportColumnName(
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
         private string GetBorrowExportColumnName(string columnName)
         {
             switch (columnName)
             {

                 case "BorrowAt":
                     return "Date";

                 case "Description":
                     return "Description";

                 case "PersonName":
                     return "Person";

                 case "Amount":
                     return "Amount";

                 case "StatusName":
                     return "Status";

                 case "PaymentName":
                     return "Payment Method";

                 case "PaidAmount":
                     return "Paid Amount";

                 case "RemainingAmount":
                     return "Remaining Amount";

                 case "DeadlineAt":
                     return "Deadline";

                 default:
                     return columnName;
             }
         }

         private void txtSearch_Enter(object sender, EventArgs e)
         {
             if (txtSearch.Text == "Search...")
             {
                 txtSearch.Text = "";
                 txtSearch.ForeColor = Color.Black;
             }
         }

         private void txtSearch_Leave(object sender, EventArgs e)
         {
             if (string.IsNullOrWhiteSpace(txtSearch.Text))
             {
                 txtSearch.Text = "Search...";
                 txtSearch.ForeColor = Color.Gray;
             }
         }

    }
}
