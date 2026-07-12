using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace PersonalExpenseCreditTracker.Modules.Credit
{
    public partial class CreditControl : Form
    {
        public CreditControl()
        {
            InitializeComponent();
            StyleCreditGrid();
            dgvCreditDataTable.CellPainting += dgvCreditDataTable_CellPainting;

        }

        private void tblSummary_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTotalCredit_Click(object sender, EventArgs e)
        {

        }

        private void picCredit_Click(object sender, EventArgs e)
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

        private void CreditControl_Load(object sender, EventArgs e)
        {

        }

        private void dgvCreditDataTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void StyleCreditGrid()
        {
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
            ColFrom.DefaultCellStyle.BackColor = Color.White;
            colDescription.DefaultCellStyle.BackColor = Color.White;
            colCategory.DefaultCellStyle.BackColor = Color.White;
            colSubCategory.DefaultCellStyle.BackColor = Color.White;
            colAmount.DefaultCellStyle.BackColor = Color.White;
            colPaymentMethod.DefaultCellStyle.BackColor = Color.White;


            //Column FontStyle
            colDate.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            ColFrom.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            colDescription.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            colCategory.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            colSubCategory.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            colAmount.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            colPaymentMethod.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);

            //Row Style
            dgvCreditDataTable.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvCreditDataTable.DefaultCellStyle.BackColor = Color.White;
            dgvCreditDataTable.DefaultCellStyle.ForeColor = Color.Black;
            dgvCreditDataTable.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgvCreditDataTable.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 238, 255);
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
                case "ColFrom":
                    DrawHeader(e, Properties.Resources.up_and_down_arrows, "From");
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