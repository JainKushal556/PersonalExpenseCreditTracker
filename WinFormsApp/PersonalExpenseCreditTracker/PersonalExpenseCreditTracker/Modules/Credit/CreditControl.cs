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
using System.Runtime.InteropServices;
using PersonalExpenseCreditTracker.Common;

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
        public CreditControl() 
        {
            InitializeComponent();
            StyleCreditGrid();
            ApplyRoundCorners();
            //dgvCreditDataTable.CellPainting += dgvCreditDataTable_CellPainting;
            this.Resize += CreditControl_Resize;

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
            dgvCreditDataTable.CellPainting += dgvCreditDataTable_CellPainting;
            ApplyRoundCorners();
            pageSize = GetRowsPerPage();
            int userID = 11; 
            LoadCreditData(userID);
            HideAllFilterPanels();
            DesignContextMenu();
            cmsFilter.Opening += cmsFilter_Opening;

        }

        private void cmsFilter_Opening(object sender, CancelEventArgs e)
        {
            tsmiDate.AutoSize = false;
            tsmiCategory.AutoSize = false;
            tsmiSubCategory.AutoSize = false;
            tsmiAmount.AutoSize = false;

            tsmiDate.Width = cmsFilter.Width;
            tsmiCategory.Width = cmsFilter.Width;
            //tsmiSubCategory= cmsFilter.Width;
            //tsmiAmount = cmsFilter.Width;
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
            //dgvCreditDataTable.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            //dgvCreditDataTable.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 238, 255);
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
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spGetAllCreditsByID", con))
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

                            dgvCreditDataTable.DataSource = null;
                            return;
                        }

                        AllCreditData = dt;
                        dgvCreditDataTable.DataSource = AllCreditData;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            masterData = dataTable.Copy();
            currentPage = 1;
            ShowCurrentPage();
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
            masterData = dataTable.Copy();
            currentPage = 1;
            ShowCurrentPage();
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
                return false;
            }
            AllCreditData = dataTable;
            masterData = dataTable.Copy();
            currentPage = 1;
            ShowCurrentPage();
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
            masterData = dataTable.Copy();
            currentPage = 1;
            ShowCurrentPage();
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
            e.Paint(e.CellBounds,
                DataGridViewPaintParts.Background |
                DataGridViewPaintParts.Border);

            int iconSize = 16;
            int spacing = 6;

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

        private void btncategoryClose_Click(object sender, EventArgs e)
        {
            pnlCategoryFilter.Visible = false;
        }

        

        private void tsmiDate_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlDateFilter);
        }

        private void tsmiCategory_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlCategoryFilter);
        }

        private void tsmiSubCategory_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlSubCategoryFilter);
        }

        private void tsmiAmount_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlAmountFilter);
        }
        private void btnSerach_Click(object sender, EventArgs e)
        {
            ShowSearchPanel(pnlSearch);
        }
        private void HideAllFilterPanels()
        {
            pnlDateFilter.Visible = false;
            pnlCategoryFilter.Visible = false;
            pnlSearch.Visible = false;
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

      

        private void ShowSearchPanel(Panel panel)
        {
            HideAllFilterPanels();

            panel.Parent = this;

            
            Point p = btnSerach.PointToScreen(Point.Empty);
            p = this.PointToClient(p);

            panel.Location = new Point(
                p.X + btnSerach.Width + 10,
                p.Y                     
            );

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

            tsmiDate.Image = Properties.Resources.calendar;
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
                p.X + pnlDateFilter.Width - panel.Width - 300,
                p.Y + 35);

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

        
        private void ShowCalenderToDatePanel(Panel panel)
        {
            HidePopupPanels();

            Point p = pnlDateFilter.PointToScreen(Point.Empty);
            p = this.PointToClient(p);

            panel.Parent = this;

            panel.Location = new Point(
                p.X + pnlDateFilter.Width - panel.Width - 70,
                p.Y + 35);

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
            pnlDateFilter.Visible = false;
        }

        private void monthCalendarFromDate_DateChanged_1(object sender, DateRangeEventArgs e)
        {
            txtFromdate.Text = e.Start.ToString("dd-MM-yyyy");
        }

        private void monthCalendarToDate_DateChanged_1(object sender, DateRangeEventArgs e)
        {
            txtToDate.Text = e.Start.ToString("dd-MM-yyyy");
        }
        private void picCredit_Click(object sender, EventArgs e)
        {

        }

        private void lblTransction_Click(object sender, EventArgs e)
        {

        }

        private void btnSubCategoryclose_Click(object sender, EventArgs e)
        {
            pnlSubCategoryFilter.Visible = false;
        }

        private void btnAmountClose_Click(object sender, EventArgs e)
        {
            pnlAmountFilter.Visible = false;
        }

        

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            AllCreditData = Common.CommonUiFunction.SearchDataInExpenseOrCredit(masterData, txtSearch);
            ShowCurrentPage();
        }

    }
}