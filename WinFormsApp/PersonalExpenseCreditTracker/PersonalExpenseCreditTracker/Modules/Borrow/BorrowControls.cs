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
        public BorrowControls()
        {
            InitializeComponent();
            StyleBorrowGrid();

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
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spGetAllBorrow", con))
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

                            dgvBorrowDataTable.DataSource = null;
                            return;
                        }

                        AllBorrowData = dt;
                        masterData = dt.Copy();
                        sortedColumn = "BorrowAt";
                        currentSortOrder = System.Windows.Forms.SortOrder.Descending;
                        ApplyBorrowSort();

                        currentPage = 1;
                        ShowCurrentPage();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            AllBorrowData = dataTable;
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
                return false;
            }
            AllBorrowData = dataTable;
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

            //Column Background Color
            colDate.DefaultCellStyle.BackColor = Color.White;
            colPersonName.DefaultCellStyle.BackColor = Color.White;
            colPaymentType.DefaultCellStyle.BackColor = Color.White;
            colStatus.DefaultCellStyle.BackColor = Color.White;
            colAmount.DefaultCellStyle.BackColor = Color.White;
            colPaidAmount.DefaultCellStyle.BackColor = Color.White;
            colRemainingAmount.DefaultCellStyle.BackColor = Color.White;
            colDeadline.DefaultCellStyle.BackColor = Color.White;
            colDescription.DefaultCellStyle.BackColor = Color.White;

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
            dgvBorrowDataTable.DefaultCellStyle.BackColor = Color.White;
            dgvBorrowDataTable.DefaultCellStyle.ForeColor = Color.Black;
            dgvBorrowDataTable.RowTemplate.Height = 40;
            dgvBorrowDataTable.RowHeadersVisible = false;
            dgvBorrowDataTable.MultiSelect = false;
            dgvBorrowDataTable.ReadOnly = true;
            dgvBorrowDataTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

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
            pnlDateFilter.Visible = false;
        }

        private void btnPersonClose_Click(object sender, EventArgs e)
        {
            pnlPersonFilter.Visible = false;
        }

        private void btnStatusClose_Click(object sender, EventArgs e)
        {
            pnlStatusFilter.Visible = false;
        }

        private void btnPaymentClose_Click(object sender, EventArgs e)
        {
            pnlPaymentFilter.Visible = false;
        }
       

        private void monthCalendarToDate_DateChanged_1(object sender, DateRangeEventArgs e)
        {
            txtToDate.Text = e.Start.ToString("dd-MM-yyyy");
            pnlToDateCalenderShow.Visible = false;
        }

         private void monthCalendarFromDate_DateChanged_1(object sender, DateRangeEventArgs e)
         {
             txtFromdate.Text = e.Start.ToString("dd-MM-yyyy");
             pnlFromDateCalenderShow.Visible = false;
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

         private void cmbStatus_Click(object sender, EventArgs e)
         {
             cmbStatus.DroppedDown = true;
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

         private void cmbPerson_Click(object sender, EventArgs e)
         {
             cmbPerson.DroppedDown = true;
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
