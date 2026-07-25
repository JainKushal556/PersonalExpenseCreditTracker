using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PersonalExpenseCreditTracker.Modules.Task
{
    public partial class TaskControls : Form
    {
        private EditTaskControl editTaskControl;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        public TaskControls()
        {
            InitializeComponent();
            StyleTaskGrid();
        }

        private void TaskControls_Load(object sender, EventArgs e)
        {


            LoadDummyData();
            SetPanelRadius();
            DesignContextMenu();

            this.Resize += TaskControls_Resize;
            dataGridViewTask.EnableHeadersVisualStyles = false;
            dataGridViewTask.CellPainting += dataGridViewTask_CellPainting;

            //Padding Add Specific Col
            dataGridViewTask.Columns["colPriority"].HeaderCell.Style.Padding = new Padding(20, 0, 0, 0);

            dataGridViewTask.Columns["colStatus"].HeaderCell.Style.Padding = new Padding(20, 0, 0, 0);

            dataGridViewTask.Columns["colDeadline"].HeaderCell.Style.Padding = new Padding(17, 0, 0, 0);

        }
        //Applies  styling to the Task Context Menu.

        private void DesignContextMenu()
        {
            // Context Menu
            cmsTaskAction.ShowImageMargin = true;
            cmsTaskAction.ShowCheckMargin = false;
            cmsTaskAction.ImageScalingSize = new Size(10, 10);
            cmsTaskAction.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            //cmsTaskAction.Renderer = new ToolStripProfessionalRenderer(new CustomColorTable());

            // Menu Item Height
            toolStripMenuItem1.AutoSize = false;
            toolStripMenuItem1.Height = 30;

            toolStripMenuItem2.AutoSize = false;
            toolStripMenuItem2.Height = 30;

            toolStripMenuItem3.AutoSize = false;
            toolStripMenuItem3.Height = 30;

            toolStripMenuItem4.AutoSize = false;
            toolStripMenuItem4.Height = 30;

            // Delete Color
            toolStripMenuItem3.ForeColor = Color.Red;

            // Images
            toolStripMenuItem1.Image = Properties.Resources.pen;
            toolStripMenuItem2.Image = Properties.Resources.refresh1;
            toolStripMenuItem3.Image = Properties.Resources.trash;
            toolStripMenuItem4.Image = Properties.Resources.delete;

            // Display Style
            toolStripMenuItem1.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            toolStripMenuItem2.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            toolStripMenuItem3.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            toolStripMenuItem4.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

            // Image Scaling
            toolStripMenuItem1.ImageScaling = ToolStripItemImageScaling.None;
            toolStripMenuItem2.ImageScaling = ToolStripItemImageScaling.None;
            toolStripMenuItem3.ImageScaling = ToolStripItemImageScaling.None;
            toolStripMenuItem4.ImageScaling = ToolStripItemImageScaling.None;
        }

        private void SetPanelRadius()
        {
            pnlTotalTask.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pnlTotalTask.Width, pnlTotalTask.Height, 3, 3));

            pnlTaskComplete.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pnlTaskComplete.Width, pnlTaskComplete.Height, 3, 3));

            pnlTaskPanding.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pnlTaskPanding.Width, pnlTaskPanding.Height, 3, 3));

            pnlDueToday.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pnlDueToday.Width, pnlDueToday.Height, 3, 3));
        }

        private void LoadDummyData()
        {
            dataGridViewTask.Rows.Clear();

            dataGridViewTask.Rows.Add(
                "01 Jul 2025",
                "Design Dashboard UI",
                "High",
                "Pending",
                "10 Jul 2025",
                "⋮");

            dataGridViewTask.Rows.Add(
                "02 Jul 2025",
                "API Integration",
                "Medium",
                "In Progress",
                "12 Jul 2025",
                "⋮");

            dataGridViewTask.Rows.Add(
                "03 Jul 2025",
                "Expense Module Testing",
                "High",
                "Pending",
                "15 Jul 2025",
                "⋮");

            dataGridViewTask.Rows.Add(
                "28 Jun 2025",
                "Fix Report Issues",
                "Low",
                "Completed",
                "08 Jul 2025",
                "⋮");

            dataGridViewTask.Rows.Add(
                "29 Jun 2025",
                "Database Optimization",
                "Medium",
                "In Progress",
                "18 Jul 2025",
                "⋮");
        }

        private void dataGridViewTask_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Priority Color
            if (dataGridViewTask.Columns[e.ColumnIndex].Name == "colPriority")
            {
                if (e.Value == null) return;

                switch (e.Value.ToString())
                {
                    case "High":
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.Font = new Font("Segoe UI", 10);
                        break;

                    case "Medium":
                        e.CellStyle.ForeColor = Color.DarkOrange;
                        e.CellStyle.Font = new Font("Segoe UI", 10);
                        break;

                    case "Low":
                        e.CellStyle.ForeColor = Color.Green;
                        e.CellStyle.Font = new Font("Segoe UI", 10);
                        break;
                }
            }

            // Status Color
            if (dataGridViewTask.Columns[e.ColumnIndex].Name == "colStatus")
            {
                if (e.Value == null) return;

                switch (e.Value.ToString())
                {
                    case "Pending":
                        //e.CellStyle.BackColor = Color.Moccasin;
                        e.CellStyle.ForeColor = Color.DarkGoldenrod;
                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                        break;

                    case "In Progress":
                        //e.CellStyle.BackColor = Color.LightBlue;
                        e.CellStyle.ForeColor = Color.RoyalBlue;
                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                        break;

                    case "Completed":
                        //e.CellStyle.BackColor = Color.Honeydew;
                        e.CellStyle.ForeColor = Color.Green;
                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                        break;
                }
            }

        }

        private void cmsTaskAction_Opening(object sender, CancelEventArgs e)
        {

        }

        private void TaskControls_Resize(object sender, EventArgs e)
        {
            SetPanelRadius();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EditTaskControl frm = new EditTaskControl(this);
            frm.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            UpdateTaskStatus updateTaskStatus = new UpdateTaskStatus();
            updateTaskStatus.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DeleteTask deleteTask = new DeleteTask();
            deleteTask.ShowDialog();
        }

        

        private void StyleTaskGrid()
        {
            //colDate.DataPropertyName = "ExpenseAt";
            //colDescription.DataPropertyName = "Description";
            //colCategory.DataPropertyName = "CategoryName";
            //colSubCategory.DataPropertyName = "SubCategoryName";
            //colAmount.DataPropertyName = "Amount";
            //colPaymentMethod.DataPropertyName = "PaymentName";

            //Column Style
            dataGridViewTask.AllowUserToOrderColumns = false;
            dataGridViewTask.AutoGenerateColumns = false;
            dataGridViewTask.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //Column HeaderStyle
            dataGridViewTask.EnableHeadersVisualStyles = false;
            dataGridViewTask.ColumnHeadersHeight = 45;
            dataGridViewTask.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewTask.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dataGridViewTask.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewTask.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 245, 255);
            dataGridViewTask.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(80, 60, 180);
            dataGridViewTask.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ////Column Background Color
            colDate.DefaultCellStyle.BackColor=Color.White;
            colAction.DefaultCellStyle.BackColor = Color.White;
            colDeadline.DefaultCellStyle.BackColor = Color.White;
            colTask.DefaultCellStyle.BackColor = Color.White;
            colStatus.DefaultCellStyle.BackColor = Color.White;
            colPriority.DefaultCellStyle.BackColor = Color.White;

            ////Column FontStyle
            colDate.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colAction.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colDeadline.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colTask.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colStatus.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            colPriority.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            //Row Style
            dataGridViewTask.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dataGridViewTask.DefaultCellStyle.BackColor = Color.White;
            dataGridViewTask.DefaultCellStyle.ForeColor = Color.Black;
            //dataGridViewTask.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            //dataGridViewTask.DefaultCellStyle.SelectionBackColor = Color.FromArgb(229, 238, 255);
            dataGridViewTask.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridViewTask.RowTemplate.Height = 40;
            dataGridViewTask.RowHeadersVisible = false;
            dataGridViewTask.MultiSelect = false;
            dataGridViewTask.ReadOnly = true;
            dataGridViewTask.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Border style
            dataGridViewTask.BorderStyle = BorderStyle.None;
            dataGridViewTask.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewTask.GridColor = Color.FromArgb(230, 230, 230);

            // Cell Alignment
            colDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTask.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colDeadline.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colAction.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPriority.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ////row data color
            //colAmount.DefaultCellStyle.ForeColor = Color.Red;
            //colCategory.DefaultCellStyle.ForeColor = Color.Green;
            //colPaymentMethod.DefaultCellStyle.ForeColor = Color.Blue;
            //colSubCategory.DefaultCellStyle.ForeColor = Color.Purple;
        }

        private void dataGridViewTask_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dataGridViewTask.Columns[e.ColumnIndex].Name == "colAction")
            {
                Rectangle rect = dataGridViewTask.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                cmsTaskAction.Show(
                    dataGridViewTask,
                    rect.Left,
                    rect.Bottom);
            }
        }

        private void dataGridViewTask_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex != -1)
                return;

            switch (dataGridViewTask.Columns[e.ColumnIndex].Name)
            {
                case "colDate":
                    DrawHeader(e, Properties.Resources.date, "Date");
                    break;

                case "colPriority":
                    DrawHeader(e, Properties.Resources.priority, "Priority");
                    break;
                case "colTask":
                    DrawHeader(e, Properties.Resources.note, "Task");
                    break;

                case "colStatus":
                    DrawHeader(e, Properties.Resources.loading, "Status");
                    break;

                case "colAction":
                    DrawHeader(e, Properties.Resources.Action, "Action");
                    break;

                case "colDeadline":
                    DrawHeader(e, Properties.Resources.deadline, "Deadline");
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


    }
}
