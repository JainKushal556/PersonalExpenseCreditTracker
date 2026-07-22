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

namespace PersonalExpenseCreditTracker.Modules.Borrow
{
    public partial class BorrowControls : Form
    {
        private string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private DataTable AllBorrowData = new DataTable();
        private int currentPage = 1;
        private int pageSize = 0;
        public BorrowControls()
        {
            InitializeComponent();
            StyleBorrowGrid();
            dgvBorrowDataTable.AutoGenerateColumns = false;

            ApplyRoundCorners();

            this.Resize += BorrowControls_Resize;

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
                    10,
                    10));

            pnlRepaidAmount.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    pnlRepaidAmount.Width,
                    pnlRepaidAmount.Height,
                    10,
                    10));

            pnlActiveBorrowings.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    pnlActiveBorrowings.Width,
                    pnlActiveBorrowings.Height,
                    10,
                    10));

            pnlRepaidAmount.Region = Region.FromHrgn(
                CreateRoundRectRgn(
                    0,
                    0,
                    pnlRepaidAmount.Width,
                    pnlRepaidAmount.Height,
                    10,
                    10));
        }

        private void BorrowControls_Load(object sender, EventArgs e)
        {
            ApplyRoundCorners();
            dgvBorrowDataTable.CellPainting += dgvBorrowDataTable_CellPainting;
           pageSize = GetRowsPerPage();
            int userID = 11;
            LoadBorrowData(userID);
        }

      
        private void LoadBorrowData(int userID)
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


        

       
        private void StyleBorrowGrid()
        {
            
            colDate.DataPropertyName = "BorrowAt";
            colPersonName.DataPropertyName = "PersonName";
            colPaymentType.DataPropertyName = "PaymentName";
            colStatus.DataPropertyName = "StatusName";
            colAmount.DataPropertyName = "Amount";
            colPaidAmount.DataPropertyName = "PaidAmount";
            colRemainingAmount.DataPropertyName = "RemainingAmount";
            colDeadline.DataPropertyName = "DeadlineAt";
            colDescription.DataPropertyName = "Description";

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
            //dgvBorrowDataTable.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            //dgvBorrowDataTable.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 238, 255);
            dgvBorrowDataTable.DefaultCellStyle.SelectionForeColor = Color.Black;
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

            

            int start = startIndex + 1;
            int end = endIndex;
            int total = AllBorrowData.Rows.Count;

            lblBorrowStartingPageNumber.Text = total == 0 ? "0" : start.ToString();
            lblBorrowEndingPageNumber.Text = end.ToString();
            lblBorrowTotalPageNumber.Text = total.ToString();
        }

        private int GetRowsPerPage()
        {
            Rectangle display = dgvBorrowDataTable.DisplayRectangle;

            int rowHeight = dgvBorrowDataTable.RowTemplate.Height;

            return Math.Max(1, display.Height / rowHeight)-1;
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
            int totalPages = Math.Max(1,(int)Math.Ceiling((double)AllBorrowData.Rows.Count / pageSize));
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
       
        
    }
}
