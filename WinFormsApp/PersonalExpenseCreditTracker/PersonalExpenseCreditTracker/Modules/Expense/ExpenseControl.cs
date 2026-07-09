using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace PersonalExpenseCreditTracker.Modules.Expense
{
    public partial class ExpenseControl : Form
    {
        public ExpenseControl()
        {
            InitializeComponent();
            StyleExpenseGrid();
            //dgvExpenseDataTable.CellPainting += dgvExpenseDataTable_CellPainting;

            typeof(DataGridView).InvokeMember(
                 "DoubleBuffered",
                 System.Reflection.BindingFlags.NonPublic |
                 System.Reflection.BindingFlags.Instance |
                 System.Reflection.BindingFlags.SetProperty,
                 null,
                 dgvExpenseDataTable,
                 new object[] { true });
            
        }

        private void tblSummary_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTotalExpense_Click(object sender, EventArgs e)
        {

        }

        private void picExpense_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btn_Click(object sender, EventArgs e)
        {

        }

        private void btnPervious_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void pnlTableHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ExpenseControl_Load(object sender, EventArgs e)
        {
            dgvExpenseDataTable.CellPainting += dgvExpenseDataTable_CellPainting;
        }

        private void dgvExpenseDataTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void StyleExpenseGrid()
        {
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
            colDate.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            colDescription.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            colCategory.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            colSubCategory.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            colAmount.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            colPaymentMethod.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);

            //Row Style
            dgvExpenseDataTable.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvExpenseDataTable.DefaultCellStyle.BackColor = Color.White;
            dgvExpenseDataTable.DefaultCellStyle.ForeColor = Color.Black;
            dgvExpenseDataTable.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgvExpenseDataTable.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 238, 255);
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
                    DrawHeader(e, Properties.Resources.credit_card, "Payment");
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

    }
}
