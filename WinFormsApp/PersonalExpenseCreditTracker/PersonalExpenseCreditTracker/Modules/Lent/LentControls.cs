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
using PersonalExpenseCreditTracker.Common;
using System.Runtime.InteropServices;
using PersonalExpenseCreditTracker.Forms.Main;
namespace PersonalExpenseCreditTracker.Modules.Lent
{
    public partial class LentControls : Form
    {
        private string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private DataTable AllLentData = new DataTable();
        private DataTable masterData = new DataTable();
        private int currentPage = 1;
        private int pageSize = 0;
        private string sortedColumn = "LentAt";
        private System.Windows.Forms.SortOrder currentSortOrder = System.Windows.Forms.SortOrder.Descending;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);


        public LentControls()
        {
            InitializeComponent();

            StyleLentGrid();
            dgvLentDataTable.CellDoubleClick += dgvLentDataTable_CellDoubleClick;

            dgvLentDataTable.AutoGenerateColumns = false;

            ApplyRoundCorners();

            this.Resize += LentControls_Resize;

            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null,
                dgvLentDataTable,
                new object[] { true });
        }
        private void LentControls_Resize(object sender, EventArgs e)
        {
            ApplyRoundCorners();
            if (AllLentData == null || AllLentData.Rows.Count == 0)
                return;

            int newPageSize = GetRowsPerPage();

            if (newPageSize != pageSize)
            {
                pageSize = newPageSize;

                int totalPages = (int)Math.Ceiling((double)AllLentData.Rows.Count / pageSize);

                if (currentPage > totalPages)
                    currentPage = totalPages;

                ShowCurrentPage();
            }
        }

        private void ApplyRoundCorners()
        {
            panelTotalLent.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    panelTotalLent.Width,
                    panelTotalLent.Height,
                    10,
                    10));

            panelTotalRepaid.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    panelTotalRepaid.Width,
                    panelTotalRepaid.Height,
                    15,
                    15));

            panelTotalDue.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    panelTotalDue.Width,
                    panelTotalDue.Height,
                    15,
                    15));

            panelTotalTransaction.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    panelTotalTransaction.Width,
                    panelTotalTransaction.Height,
                    15,
                    15));

        }

      

        //private void btnExportReport_MouseEnter(object sender, EventArgs e)
        //{
        //    btnPrint.BackColor = Color.FromArgb(0, 0, 240);
        //}

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LentControls_Load(object sender, EventArgs e)
        {
            txtMinAmount.Text = "Enter Amount";
            txtMinAmount.ForeColor = Color.Gray;
            txtMaxAmount.Text = "Enter Amount";
            txtMaxAmount.ForeColor = Color.Gray;
            cmbPerson.Text = "Select Person";
            cmbPerson.ForeColor = Color.Gray;
            cmbPayment.Text = "Select Payment";
            cmbPayment.ForeColor = Color.Gray;
            cmbStatus.Text = "Select Status";
            cmbStatus.ForeColor = Color.Gray;

            ApplyRoundCorners();
            dgvLentDataTable.CellPainting += dgvLentDataTable_CellPainting;
            pageSize = GetRowsPerPage();
            int userID = Session.LogedInUser.GetUserId();
            LoadLentData(userID);
            HideAllFilterPanels();
            DesignContextMenu();
             cmsFilter.Opening += cmsFilter_Opening;
             RegisterMouseDown(this);
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
        public void LoadLentData(int userID)
        {

            DataTable dataTable = CommonUiFunction.RetrieveDataForGridView("spGetAllLent", userID);
            if (dataTable.Columns.Contains("Message"))
             {
                 MessageBox.Show(dataTable.Rows[0]["Message"].ToString(),
                                 "Information",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Information);
                 
                 dgvLentDataTable.DataSource = null;
                 return;
             }

            AllLentData = dataTable;
            masterData = dataTable.Copy();
            sortedColumn = "LentAt";
            currentSortOrder = System.Windows.Forms.SortOrder.Descending;
            ApplyLentSort();

            currentPage = 1;
            ShowCurrentPage();
            
        }

        public Boolean LoadFilteredLentData(string spName, string paramName, int filterId)
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
            AllLentData = dataTable;
            currentPage = 1;
            ShowCurrentPage();
            return true;
        }

        public Boolean LoadFilteredLentData(string spName, int userId, string paramName1, DateTime paramId1, string paramName2, DateTime paramId2)
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
            AllLentData = dataTable;
            currentPage = 1;
            ShowCurrentPage();
            return true;
        }
        //
        public Boolean LoadFilteredLentData(string spName, int userId, string paramName1, Decimal paramId1, string paramName2, Decimal paramId2)
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
            AllLentData = dataTable;
            currentPage = 1;
            ShowCurrentPage();
            return true;
        }
        private void StyleLentGrid()
        {
            
            colDate.DataPropertyName = "LentAt";
            colPersonName.DataPropertyName = "PersonName";
            colAmount.DataPropertyName = "Amount";
            colPaymentType.DataPropertyName = "PaymentName";
            colStatus.DataPropertyName = "StatusName";
            colReturnedAmount.DataPropertyName = "ReturnedAmount";
            colRemainingAmount.DataPropertyName = "RemainingAmount";
            colDeadline.DataPropertyName = "DeadlineAt";
            colDescription.DataPropertyName = "Description";

            //Column Style
            dgvLentDataTable.AllowUserToOrderColumns = false;
            dgvLentDataTable.AutoGenerateColumns = false;
            dgvLentDataTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //Column HeaderStyle
            dgvLentDataTable.EnableHeadersVisualStyles = false;
            dgvLentDataTable.ColumnHeadersHeight = 45;
            dgvLentDataTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvLentDataTable.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgvLentDataTable.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvLentDataTable.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 245, 255);
            dgvLentDataTable.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(80, 60, 180);
            dgvLentDataTable.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //Column Background Color
            colDate.DefaultCellStyle.BackColor = Color.White;
            colPersonName.DefaultCellStyle.BackColor = Color.White;
            colAmount.DefaultCellStyle.BackColor = Color.White;
            colPaymentType.DefaultCellStyle.BackColor = Color.White;
            colStatus.DefaultCellStyle.BackColor = Color.White;
            colReturnedAmount.DefaultCellStyle.BackColor = Color.White;
            colRemainingAmount.DefaultCellStyle.BackColor = Color.White;
            colDeadline.DefaultCellStyle.BackColor = Color.White;
            colDescription.DefaultCellStyle.BackColor = Color.White;

            //Column FontStyle
            colDate.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colPersonName.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colPaymentType.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colStatus.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colAmount.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colReturnedAmount.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colRemainingAmount.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colDeadline.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colDescription.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            //Row Style
            dgvLentDataTable.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvLentDataTable.DefaultCellStyle.BackColor = Color.White;
            dgvLentDataTable.DefaultCellStyle.ForeColor = Color.Black;
            //dgvBorrowDataTable.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            //dgvLentDataTable.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvLentDataTable.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvLentDataTable.RowTemplate.Height = 40;
            dgvLentDataTable.RowHeadersVisible = false;
            dgvLentDataTable.MultiSelect = false;
            dgvLentDataTable.ReadOnly = true;
            dgvLentDataTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Border style
            dgvLentDataTable.BorderStyle = BorderStyle.None;
            dgvLentDataTable.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvLentDataTable.GridColor = System.Drawing.Color.Gainsboro;
            
            dgvLentDataTable.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            colAmount.DefaultCellStyle.ForeColor = Color.Red;
            colReturnedAmount.DefaultCellStyle.ForeColor = Color.Red;
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
            colReturnedAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colRemainingAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDeadline.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDescription.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void dgvLentDataTable_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            DataGridViewColumn column = dgvLentDataTable.Columns[e.ColumnIndex];

            string columnName = column.DataPropertyName;

            

            //  sortable columns
            if (columnName != "LentAt" &&
                columnName != "PersonName" &&
                columnName != "Amount" &&
                columnName != "StatusName" &&
                columnName != "PaymentName" &&
                columnName != "ReturnedAmount" &&
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

            ApplyLentSort();

            currentPage = 1;

            ShowCurrentPage();
        }



        private void ApplyLentSort()
        {
            if (string.IsNullOrEmpty(sortedColumn) ||
                currentSortOrder == WinFormsSortOrder.None)
                return;

            if (AllLentData == null ||
                AllLentData.Rows.Count == 0)
                return;

            if (!AllLentData.Columns.Contains(sortedColumn))
                return;

            DataView view = AllLentData.DefaultView;

            string direction =
                currentSortOrder == WinFormsSortOrder.Ascending
                ? "ASC"
                : "DESC";

            view.Sort =
                "[" + sortedColumn + "] " + direction;

            AllLentData = view.ToTable();
        }

        private void ShowCurrentPage()
        {
            DataTable pageTable = AllLentData.Clone();

            btnCurrentPage.Text = currentPage.ToString();

            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, AllLentData.Rows.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                pageTable.ImportRow(AllLentData.Rows[i]);
            }

            dgvLentDataTable.DataSource = pageTable;
            Common.CommonUiFunction.HighlightSearch(dgvLentDataTable, txtSearch);

            int start = startIndex + 1;
            int end = endIndex;
            int total = AllLentData.Rows.Count;

            lblStartingPageNumber.Text = total == 0 ? "0" : start.ToString();
            lblEndingPageNumber.Text = end.ToString();
            lblTotalPageNumber.Text = total.ToString();

            if (AllLentData != null && AllLentData.Rows.Count > 0)
            {
                // Total Lent Amount
                decimal totalLent = AllLentData.AsEnumerable().Sum(row => row.Field<decimal>("Amount"));

                // Total Repaid Amount
                decimal totalRepaid = AllLentData.AsEnumerable().Sum(row => row.Field<decimal>("ReturnedAmount"));

                // Total Due Amount
                decimal totalDue = AllLentData.AsEnumerable().Sum(row => row.Field<decimal>("RemainingAmount"));

                // Total Transactions
                int totalTransaction = AllLentData.Rows.Count;

                // Display
                lblTotalLentAmount.Text = "₹ " + totalLent.ToString("#,##0.##");
                lblTotalRepaidAmount.Text = "₹ " + totalRepaid.ToString("#,##0.##");
                lblTotalDueAmount.Text = "₹ " + totalDue.ToString("#,##0.##");
                labelTotalTransactionNumber.Text = totalTransaction.ToString();
            }
            else
            {
                lblTotalLentAmount.Text = "₹ 0";
                lblTotalRepaidAmount.Text = "₹ 0";
                lblTotalDueAmount.Text = "₹ 0";
                labelTotalTransactionNumber.Text = "0";
            }

        }

        private int GetRowsPerPage()
        {
            Rectangle display = dgvLentDataTable.DisplayRectangle;

            int rowHeight = dgvLentDataTable.RowTemplate.Height;

            return Math.Max(1, display.Height / rowHeight) - 1;

           
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

        private void dgvLentDataTable_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex != -1)
                return;

            switch (dgvLentDataTable.Columns[e.ColumnIndex].Name)
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
                case "colReturnedAmount":
                    DrawHeader(e, Properties.Resources.money, "ReturnedAmount");
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

        //Page control button
        private void btnNextPage_Click_1(object sender, EventArgs e)
        {
            int totalPages = Math.Max(1, (int)Math.Ceiling((double)AllLentData.Rows.Count / pageSize));

            if (currentPage < totalPages)
            {
                currentPage++;
                ShowCurrentPage();
            }
        }
        private void btnLastPage_Click_1(object sender, EventArgs e)
        {
            int totalPages = Math.Max(1, (int)Math.Ceiling((double)AllLentData.Rows.Count / pageSize));
            if (currentPage != totalPages)
            {
                currentPage = totalPages;
                ShowCurrentPage();
            }
        }
        private void btnPreviousPage_Click_1(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                ShowCurrentPage();
            }
        }
        private void btnFirstPage_Click_1(object sender, EventArgs e)
        {
            if (currentPage != 1)
            {
                currentPage = 1;
                ShowCurrentPage();
            }
        }

        private void dgvLentDataTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panelTotalLent_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                panelTotalLent.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void panelTotalRepaid_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                panelTotalRepaid.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void panelTotalDue_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                panelTotalDue.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void panelTotalTransaction_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                panelTotalTransaction.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }


        private void dgvLentDataTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvLentDataTable.Rows[e.RowIndex];

            int lentId = 0;

            DataRowView drv = row.DataBoundItem as DataRowView;
            if (drv != null && drv.Row.Table.Columns.Contains("LentID"))
            {
                lentId = Convert.ToInt32(drv["LentID"]);
            }

            string personName = Convert.ToString(row.Cells["colPersonName"].Value);
            string totalAmount = Convert.ToString(row.Cells["colAmount"].Value);
            string remainingAmount = Convert.ToString(row.Cells["colRemainingAmount"].Value);
            string status = Convert.ToString(row.Cells["colStatus"].Value);
            string returnAmount = Convert.ToString(row.Cells["colReturnedAmount"].Value);

            using (PayLentReturnAmountControls frm = new PayLentReturnAmountControls())
            {
                frm.SetLentDetails(
                    lentId,
                    personName,
                    totalAmount,
                    remainingAmount,
                    status,
                    returnAmount);

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    int userID = PersonalExpenseCreditTracker.Session.LogedInUser.GetUserId();
                    LoadLentData(userID);
                }
            }
        }

        private void btnExportReport_Click(object sender, EventArgs e)
        {

        }

        private void tsmiDate_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlDateFilter);
        }

        private void tsmiCategory_Click(object sender, EventArgs e)
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
        }

        

        

        private void btnFilter_Click(object sender, EventArgs e)
        {
            cmsFilter.Show(btnFilter, 0, btnFilter.Height);
        }

        private void HideAllFilterPanels()
        {
            pnlDateFilter.Visible = false;
            pnlAmountFilter.Visible = false;
            pnlStatusFilter.Visible = false;
            pnlPaymentFilter.Visible = false;
            pnlPersonFilter.Visible = false;
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
                ctrl.MouseDown += LentControls_MouseDown;

                if (ctrl.HasChildren)
                    RegisterMouseDown(ctrl);
            }
        }
        private void LentControls_MouseDown(object sender, MouseEventArgs e)
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

        private void monthCalendarFromDate_DateChanged_1(object sender, DateRangeEventArgs e)
        {
            txtFromdate.Text = e.Start.ToString("dd-MM-yyyy");
        }

        private void monthCalendarToDate_DateChanged_1(object sender, DateRangeEventArgs e)
        {
            txtToDate.Text = e.Start.ToString("dd-MM-yyyy");
        }

        private void btnDateClose_Click_1(object sender, EventArgs e)
        {
            pnlDateFilter.Visible = false;
        }

        private void btnPersonClose_Click(object sender, EventArgs e)
        {
            pnlPersonFilter.Visible = false;
        }

        private void btnPaymentClose_Click(object sender, EventArgs e)
        {
            pnlPaymentFilter.Visible = false;
        }

         private void button3_Click(object sender, EventArgs e)
         {
             pnlStatusFilter.Visible = false;
         }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            AllLentData = Common.CommonUiFunction.SearchDataInLentOrBorrow(masterData, txtSearch);
            ShowCurrentPage();
        }

        private void cmbPerson_Click(object sender, EventArgs e)
        {
            cmbPerson.DroppedDown = true;
        }

        private void cmbPerson_Enter(object sender, EventArgs e)
        {
            if (cmbPerson.Text == "Select Person")
                cmbPerson.ForeColor = Color.Black;
        }

        private void cmbPerson_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbPerson.Text) || cmbPerson.Text == "Select Person")
            {

                cmbPerson.Text = "Select Person";
                cmbPerson.ForeColor = Color.Gray;
            }
            else
            {
                cmbPerson.ForeColor = Color.Black;
            }
        }

        private void cmbPayment_Click(object sender, EventArgs e)
        {
            cmbPayment.DroppedDown = true;
        }

        private void cmbPayment_Enter(object sender, EventArgs e)
        {
            if (cmbPayment.Text == "Select Payment")
                cmbPayment.ForeColor = Color.Black;
        }

        private void cmbPayment_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbPayment.Text) || cmbPayment.Text == "Select Payment")
            {

                cmbPayment.Text = "Select Payment";
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
            if (cmbStatus.Text == "Select Status")
                cmbStatus.ForeColor = Color.Black;
        }

        private void cmbStatus_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbStatus.Text) || cmbStatus.Text == "Select Status")
            {

                cmbStatus.Text = "Select Status";
                cmbStatus.ForeColor = Color.Gray;
            }
            else
            {
                cmbStatus.ForeColor = Color.Black;
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

       

        
      }
    }
