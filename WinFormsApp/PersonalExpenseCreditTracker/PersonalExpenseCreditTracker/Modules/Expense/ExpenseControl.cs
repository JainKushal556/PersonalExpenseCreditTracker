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
        private string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private DataTable AllExpenseData = new DataTable();
        private int currentPage = 1;
        private int pageSize = 0;
        public ExpenseControl()
        {
            InitializeComponent();
            StyleExpenseGrid();
            ApplyRoundCorners();
            //dgvExpenseDataTable.CellPainting += dgvExpenseDataTable_CellPainting;
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
            dgvExpenseDataTable.CellPainting += dgvExpenseDataTable_CellPainting;
            ApplyRoundCorners();
            pageSize = GetRowsPerPage();
            int userID = 11;
            LoadExpenseData(userID);
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

        private void LoadExpenseData(int userID)
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
                Color.FromArgb(38, 50, 75),
                ButtonBorderStyle.Solid);
        }

        private void pnlTransactionCard_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlTransactionCard.ClientRectangle,
                Color.FromArgb(55, 90, 210),
                ButtonBorderStyle.Solid);
        }
        private void ApplyRoundCorners()
        {
            pnlTotalExpense.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pnlTotalExpense.Width, pnlTotalExpense.Height, 10, 10));

            pnlTransactionCard.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pnlTransactionCard.Width, pnlTransactionCard.Height, 10, 10));

            
        }
        private void pnlTotalExpense_Resize(object sender, EventArgs e)
        {
            ApplyRoundCorners();
        }

        private void pnlTransactionCard_Resize(object sender, EventArgs e)
        {
            ApplyRoundCorners();
        }
    }
}
