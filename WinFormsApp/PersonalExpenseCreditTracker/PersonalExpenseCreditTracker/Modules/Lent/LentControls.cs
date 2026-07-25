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
using PersonalExpenseCreditTracker.Common;
using System.Runtime.InteropServices;
using PersonalExpenseCreditTracker.Forms.Main;
namespace PersonalExpenseCreditTracker.Modules.Lent
{
    public partial class LentControls : Form
    {
        private string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private DataTable AllLentData = new DataTable();
        private int currentPage = 1;
        private int pageSize = 0;

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
                    10,
                    10));

            panelTotalDue.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    panelTotalDue.Width,
                    panelTotalDue.Height,
                    10,
                    10));

            panelTotalTransaction.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    panelTotalTransaction.Width,
                    panelTotalTransaction.Height,
                    10,
                    10));

            panelExportReport.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    panelExportReport.Width,
                    panelExportReport.Height,
                    10,
                    10));
        }

      

        private void btnExportReport_MouseEnter(object sender, EventArgs e)
        {
            btnExportReport.BackColor = Color.FromArgb(0, 0, 240);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LentControls_Load(object sender, EventArgs e)
        {
            ApplyRoundCorners();
            dgvLentDataTable.CellPainting += dgvLentDataTable_CellPainting;
            pageSize = GetRowsPerPage();
            int userID = Session.LogedInUser.GetUserId();
            LoadLentData(userID);
           
        }


        public  void LoadLentData(int userID)
        {
            //try
            //{
            //    using (SqlConnection con = new SqlConnection(ConnectionString))
            //    {
            //        using (SqlCommand cmd = new SqlCommand("spGetAllLent", con))
            //        {
            //            cmd.CommandType = CommandType.StoredProcedure;
            //            cmd.Parameters.AddWithValue("@UserID", userID);

            //            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //            DataTable dt = new DataTable();

            //            da.Fill(dt);


            //            if (dt.Columns.Contains("Message"))
            //            {
            //                MessageBox.Show(dt.Rows[0]["Message"].ToString(),
            //                                "Information",
            //                                MessageBoxButtons.OK,
            //                                MessageBoxIcon.Information);

            //                dgvLentDataTable.DataSource = null;
            //                return;
            //            }

            //            AllLentData = dt;
            //            currentPage = 1;
            //            ShowCurrentPage();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            DataTable dataTable = LentUi.retriveDataForGridViewAtUi("spGetAllLent", userID);
            if (dataTable.Columns.Contains("Message"))
             {
                 MessageBox.Show(dataTable.Rows[0]["Message"].ToString(),
                                 "Information",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Information);
                 
                 dgvLentDataTable.DataSource = null;
                 return;
             }
            //if (dgvLentDataTable.DataSource != null)
            //{
            //    dgvLentDataTable.DataSource = null;
            //}
            AllLentData = dataTable;
            currentPage = 1;
            ShowCurrentPage();
        }

        public Boolean LoadFilteredLentData(int statusId, string paramName)
        {
            int userID = PersonalExpenseCreditTracker.Session.LogedInUser.GetUserId();
            DataTable dataTable = LentUi.retriveFilteredDataByStatusAtUi("spFilterLentByStatus", userID,paramName, statusId);
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
            //dgvBorrowDataTable.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 238, 255);
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

            int start = startIndex + 1;
            int end = endIndex;
            int total = AllLentData.Rows.Count;

            lblStartingPageNumber.Text = total == 0 ? "0" : start.ToString();
            lblEndingPageNumber.Text = end.ToString();
            lblTotalPageNumber.Text = total.ToString();
        }

        private int GetRowsPerPage()
        {
            Rectangle display = dgvLentDataTable.DisplayRectangle;

            int rowHeight = dgvLentDataTable.RowTemplate.Height;

            return Math.Max(1, display.Height / rowHeight) - 1;

           
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

      }
    }
