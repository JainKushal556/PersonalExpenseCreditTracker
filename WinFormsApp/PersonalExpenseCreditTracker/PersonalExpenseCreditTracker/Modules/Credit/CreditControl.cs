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

        private void LoadCreditData(int userID)
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

    }
}