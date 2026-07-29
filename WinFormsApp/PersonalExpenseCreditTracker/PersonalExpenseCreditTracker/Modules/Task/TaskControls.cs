using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

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

        private string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private DataTable AllTaskData = new DataTable();
        private int SelectedTaskID = 0;
        private int currentPage = 1;
        private int pageSize = 0;
        public TaskControls()
        {
            InitializeComponent();
            StyleTaskGrid();
            this.Resize += TaskControls_Resize;
        }

        private void TaskControls_Load(object sender, EventArgs e)
        {

            pageSize = GetRowsPerPage();
            int userID = 11;
            LoadTaskData(userID);
            SetPanelRadius();
            HideAllFilterPanels();
            DesignContextMenu();

            this.Resize += TaskControls_Resize;
            dataGridViewTask.EnableHeadersVisualStyles = false;
            dataGridViewTask.CellPainting += dataGridViewTask_CellPainting;
            dataGridViewTask.CellFormatting += dataGridViewTask_CellFormatting;
            

            //Padding Add 
            dataGridViewTask.Columns["colPriority"].HeaderCell.Style.Padding = new Padding(20, 0, 0, 0);

            dataGridViewTask.Columns["colStatus"].HeaderCell.Style.Padding = new Padding(20, 0, 0, 0);

            dataGridViewTask.Columns["colDeadline"].HeaderCell.Style.Padding = new Padding(17, 0, 0, 0);

        }
        //Applies  styling to the Task Context Menu.
        private void LoadTaskData(int userID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("spGetAllTasks", con))
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

                            dataGridViewTask.DataSource = null;
                            return;
                        }

                        AllTaskData = dt;
                        dataGridViewTask.DataSource = AllTaskData;
                        foreach (DataGridViewRow row in dataGridViewTask.Rows)
                        {
                            row.Cells["colAction"].Value = "⋮";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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

            //filter cms
            cmsFilter.ShowImageMargin = true;
            cmsFilter.ShowCheckMargin = false;
            cmsFilter.ImageScalingSize = new Size(10, 10);

            tsmiDate.AutoSize = false;
            tsmiDate.Height = 30;

            tsmiPriority.AutoSize = false;
            tsmiPriority.Height = 30;

            tsmiDate.Image = Properties.Resources.calendar;
            tsmiPriority.Image = Properties.Resources.shop;

            tsmiDate.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsmiPriority.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

            tsmiDate.ImageScaling = ToolStripItemImageScaling.None;
            tsmiPriority.ImageScaling = ToolStripItemImageScaling.None;
        }

        private void SetPanelRadius()
        {
            pnlTotalTask.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pnlTotalTask.Width, pnlTotalTask.Height, 10, 10));

            pnlTaskComplete.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pnlTaskComplete.Width, pnlTaskComplete.Height, 10, 10));

            pnlTaskPanding.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pnlTaskPanding.Width, pnlTaskPanding.Height, 10, 10));

            pnlDueToday.Region = Region.FromHrgn(
                CreateRoundRectRgn(0, 0, pnlDueToday.Width, pnlDueToday.Height, 10, 10));
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
            if (dataGridViewTask.Columns[e.ColumnIndex].Name == "colAction")
            {
                e.Value = "⋮";
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                e.CellStyle.ForeColor = Color.FromArgb(80, 80, 80);
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
            if (AllTaskData == null || AllTaskData.Rows.Count == 0)
                return;

            int newPageSize = GetRowsPerPage();

            if (newPageSize != pageSize)
            {
                pageSize = newPageSize;
                ShowCurrentPage();
            }
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
            colDate.DataPropertyName = "Date";
            colTask.DataPropertyName = "TaskTitle";
            colPriority.DataPropertyName = "PriorityName";
            colStatus.DataPropertyName = "‎TaskStatusName";
            colDeadline.DataPropertyName = "Deadline";
            

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

          
            colAction.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colAction.DefaultCellStyle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            colAction.DefaultCellStyle.ForeColor = Color.FromArgb(90, 90, 90);
        }

      
        private void dataGridViewTask_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dataGridViewTask.Columns[e.ColumnIndex].Name == "colAction")
            {
                SelectedTaskID = Convert.ToInt32(
                    ((DataRowView)dataGridViewTask.Rows[e.RowIndex].DataBoundItem)["TaskID"]);

                Rectangle rect = dataGridViewTask.GetCellDisplayRectangle(
                    e.ColumnIndex,
                    e.RowIndex,
                    true);

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

        
        private void pnlTotalTask_Resize(object sender, EventArgs e)
        {
            SetPanelRadius();
        }
        private void pnlDueToday_Resize(object sender, EventArgs e)
        {
            SetPanelRadius();
        }
        private void pnlTaskPanding_Resize(object sender, EventArgs e)
        {
            SetPanelRadius();
        }
        private void pnlTaskComplete_Resize(object sender, EventArgs e)
        {
            SetPanelRadius();
        }

        private void pnlTotalTask_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlTotalTask.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }
        private void pnlTaskComplete_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlTaskComplete.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void pnlTaskPanding_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlTaskPanding.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void pnlDueToday_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlDueToday.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }


        private void ShowCurrentPage()
        {
            DataTable pageTable = AllTaskData.Clone();
            btnCurrentPage.Text = currentPage.ToString();
            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, AllTaskData.Rows.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                pageTable.ImportRow(AllTaskData.Rows[i]);
            }

            dataGridViewTask.DataSource = pageTable;
            int start = startIndex + 1;
            int end = endIndex;
            int total = AllTaskData.Rows.Count;

            lblTaskStartingPageNumber.Text = total == 0 ? "0" : start.ToString();
            lblTaskEndingPageNumber.Text = end.ToString();
            lblTaskTotalPageNumber.Text = total.ToString();
        }

        private int GetRowsPerPage()
        {
            Rectangle display = dataGridViewTask.DisplayRectangle;

            int rowHeight = dataGridViewTask.RowTemplate.Height;

            return Math.Max(1, display.Height / rowHeight) - 1;
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
            int totalPages = (int)Math.Ceiling((double)AllTaskData.Rows.Count / pageSize);

            if (currentPage < totalPages)
            {
                currentPage++;
                ShowCurrentPage();
            }
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)AllTaskData.Rows.Count / pageSize);
            if (currentPage != totalPages)
            {
                currentPage = totalPages;
                ShowCurrentPage();
            }
        }

        private void tsmiDate_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlDateFilter);
        }

        private void tsmiPriority_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlPriorityFilter);
        }

        private void btnSerach_Click(object sender, EventArgs e)
        {
            ShowSearchPanel(pnlSearch);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            cmsFilter.Show(btnFilter, 0, btnFilter.Height);
        }

        private void btnPriorityClose_Click(object sender, EventArgs e)
        {
            pnlPriorityFilter.Visible = false;
        }

       

       

       
        
         private void HideAllFilterPanels()
        {
            pnlDateFilter.Visible = false;
            pnlPriorityFilter.Visible = false;
            pnlSearch.Visible = false;

        }
        private void HidePopupPanels()
        {
            pnlFromDateCalenderShow.Visible = false;
            pnlToDateCalenderShow.Visible = false;
        }
        private void ShowFilterPanel(Panel panel)
        {
            HideAllFilterPanels();

            Point p = dataGridViewTask.PointToScreen(Point.Empty);
            p = this.PointToClient(p);

            panel.Parent = this;

            panel.Location = new Point(
                p.X + dataGridViewTask.Width - panel.Width - 170,
                p.Y - 55);

            panel.BringToFront();
            panel.Visible = true;
        }

        private void ShowSearchPanel(Panel panel)
        {
            HideAllFilterPanels();
            Point p = dataGridViewTask.PointToScreen(Point.Empty);
            p = this.PointToClient(p);
            panel.Location = new Point(
                p.X + dataGridViewTask.Width - panel.Width - 750,
                p.Y - 55);
            panel.BringToFront();
            panel.Visible = true;
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
        private void RegisterMouseDown(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.MouseDown += TaskControls_MouseDown;

                if (ctrl.HasChildren)
                    RegisterMouseDown(ctrl);
            }
        }
        private void TaskControls_MouseDown(object sender, MouseEventArgs e)
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

        private void btnDateClose_Click_1(object sender, EventArgs e)
        {
            pnlDateFilter.Visible = false;
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

        private void monthCalendarFromDate_DateChanged_1(object sender, DateRangeEventArgs e)
        {
            txtFromdate.Text = e.Start.ToString("dd-MM-yyyy");
        }

        private void monthCalendarToDate_DateChanged_1(object sender, DateRangeEventArgs e)
        {
            txtToDate.Text = e.Start.ToString("dd-MM-yyyy");
        }
    }
}
