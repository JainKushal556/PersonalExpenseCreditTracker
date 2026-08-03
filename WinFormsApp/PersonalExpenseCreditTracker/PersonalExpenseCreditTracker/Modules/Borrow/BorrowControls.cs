using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
            ApplyRoundCorners();
            dgvBorrowDataTable.CellPainting += dgvBorrowDataTable_CellPainting;
            pageSize = GetRowsPerPage();
            int userID = 11;
            LoadBorrowData(userID);
            HideAllFilterPanels();
            DesignContextMenu();
            cmsFilter.Opening += cmsFilter_Opening;

        }

        private void cmsFilter_Opening(object sender, CancelEventArgs e)
        {
            tsmiDate.AutoSize = false;
            tsmiCategory.AutoSize = false;

            tsmiDate.Width = cmsFilter.Width;
            tsmiCategory.Width = cmsFilter.Width;
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
            e.Paint(e.CellBounds,
                DataGridViewPaintParts.Background |
                DataGridViewPaintParts.Border);

            int iconSize = 16;
            int spacing = 4;

            SizeF textSize = e.Graphics.MeasureString(text, e.CellStyle.Font);

            int totalWidth = iconSize + spacing + (int)textSize.Width;

            int startX = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
            int iconY = e.CellBounds.Y + (e.CellBounds.Height - iconSize) / 2;

            e.Graphics.DrawImage(icon, startX, iconY, iconSize, iconSize);

            using (Brush brush = new SolidBrush(Color.FromArgb(80, 60, 180)))
            {
                e.Graphics.DrawString(
                    text,
                    e.CellStyle.Font,
                    brush,
                    startX + iconSize + spacing,
                    e.CellBounds.Y + (e.CellBounds.Height - textSize.Height) / 2);
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
                    DrawHeader(e, Properties.Resources.PersonIcon, "Person Name");
                    break;
                case "colDescription":
                    DrawHeader(e, Properties.Resources.note, "Description");
                    break;
                case "colAmount":
                    DrawHeader(e, Properties.Resources.money, "Amount");
                    break;
                case "colPaymentType":
                    DrawHeader(e, Properties.Resources.credit_card1, "Payment Type");
                    break;
                case "colPaidAmount":
                    DrawHeader(e, Properties.Resources.money, "Paid Amount");
                    break;
                case "colRemainingAmount":
                    DrawHeader(e, Properties.Resources.money, "Remaining Amount");
                    break;
                case "colDeadline":
                    DrawHeader(e, Properties.Resources.deadline, "Deadline");
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

            int personID = Convert.ToInt32(row.Cells["colPersonID"].Value);

            MessageBox.Show("PersonID = " + personID);

            PayBorrowPaidAmountControls frm = new PayBorrowPaidAmountControls();

            //frm.UserID = userID;
            //frm.PersonID = personID;

            frm.ShowDialog(this);
        }
        private void HideAllFilterPanels()
        {
            pnlDateFilter.Visible = false;
            pnlCategoryFilter.Visible = false;
            //pnlSearch.Visible = false;
        }
        private void HidePopupPanels()
        {
            pnlFromDateCalenderShow.Visible = false;
            pnlToDateCalenderShow.Visible = false;
        }



        private void btnFilter_Click(object sender, EventArgs e)
        {
            //pnlSearch.Visible = false;
            HidePopupPanels();
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



        //private void ShowSearchPanel(Panel panel)
        //{
        //    if (panel.Visible)
        //    {
        //        panel.Visible = false;
        //        return;
        //    }
        //    HideAllFilterPanels();

        //    panel.Parent = this;


        //    Point p = btnSerach.PointToScreen(Point.Empty);
        //    p = this.PointToClient(p);

        //    panel.Location = new Point(
        //        p.X + btnSerach.Width + 10,
        //        p.Y
        //    );

        //    panel.BringToFront();
        //    panel.Visible = true;
        //}
       

        private void tsmiDate_Click_1(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlDateFilter);
        }

        private void tsmiCategory_Click_1(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlCategoryFilter);
            comboBox1.DroppedDown = true;
        }

        private void btnSerach_Click(object sender, EventArgs e)
        {
            pnlFromDateCalenderShow.Visible = false;
            pnlToDateCalenderShow.Visible = false;
            //ShowSearchPanel(pnlSearch);
        }

        private void pnlCategoryFilter_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnlSearch.Visible = false;
        }

        //private void btnDateClose_Click(object sender, EventArgs e)
        //{
        //    pnlDateFilter.Visible = false;
        //}

        private void btncategoryClose_Click(object sender, EventArgs e)
        {
            HidePopupPanels();
            pnlCategoryFilter.Visible = false;
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

            tsmiDate.Image = Properties.Resources.calendar__1_;
            tsmiCategory.Image = Properties.Resources.shop;

            tsmiDate.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsmiCategory.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

            tsmiDate.ImageScaling = ToolStripItemImageScaling.None;
            tsmiCategory.ImageScaling = ToolStripItemImageScaling.None;
        }
        
        private void ShowCalenderFromDatePanel(Panel panel)
        {
            HidePopupPanels();

            Point p = pnlDateFilter.PointToScreen(Point.Empty);
            p = this.PointToClient(p);

            panel.Parent = this;

            panel.Location = new Point(
                p.X + pnlDateFilter.Width - panel.Width-300 ,
                p.Y +35);

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
                if (!pnlFromDateCalenderShow.Bounds.Contains(mousePos) &&
                    !picCalenderFromDate.RectangleToScreen(picCalenderFromDate.ClientRectangle)
                        .Contains(Control.MousePosition))
                {
                    pnlFromDateCalenderShow.Visible = false;
                }
            }

            // To Date Calendar
            if (pnlToDateCalenderShow.Visible)
            {
                if (!pnlToDateCalenderShow.Bounds.Contains(mousePos) &&
                    !picCalenderToDate.RectangleToScreen(picCalenderToDate.ClientRectangle)
                        .Contains(Control.MousePosition))
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

            Point p = pnlDateFilter.PointToScreen(Point.Empty);
            p = this.PointToClient(p);

            panel.Parent = this;

            panel.Location = new Point(
                p.X + pnlDateFilter.Width - panel.Width-70,
                p.Y +35);

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
            HidePopupPanels();
            pnlDateFilter.Visible = false;
        }

        private void pnlButtonControls_Click(object sender, EventArgs e)
        {
            HidePopupPanels();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            HidePopupPanels();
            pnlToDateCalenderShow.Visible = false;
        }

        private void pnlDateHeader_Click(object sender, EventArgs e)
        {
            HidePopupPanels();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            HidePopupPanels();
        }

        private void tableLayoutPanel1_Click(object sender, EventArgs e)
        {
            HidePopupPanels();
        }

        private void dgvBorrowDataTable_Click(object sender, EventArgs e)
        {
            HidePopupPanels();
        }



        private void monthCalendarToDate_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtToDate.Text = e.Start.ToString("dd-MM-yyyy");
            pnlToDateCalenderShow.Visible = false;
        }

        private void monthCalendarFromDate_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtFromdate.Text = e.Start.ToString("dd-MM-yyyy");
            pnlFromDateCalenderShow.Visible = false;
        }

        private void txtToDate_Enter(object sender, EventArgs e)
        {

        }

        private void txtToDate_Click(object sender, EventArgs e)
        {
            pnlFromDateCalenderShow.Visible = false;
            ShowCalenderToDatePanel(pnlToDateCalenderShow);
        }

        private void txtFromdate_Click(object sender, EventArgs e)
        {
            pnlToDateCalenderShow.Visible = false;
            ShowCalenderFromDatePanel(pnlFromDateCalenderShow);
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
           AllBorrowData = Common.CommonUiFunction.SearchDataInLentOrBorrow(masterData, txtSearch);
           ShowCurrentPage();
        }

    }
}

