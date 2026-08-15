using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinFormsSortOrder = System.Windows.Forms.SortOrder;
using System.Runtime.InteropServices;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using PersonalExpenseCreditTracker.Common;
using BLLayer.Common;
using Excel = Microsoft.Office.Interop.Excel;
namespace PersonalExpenseCreditTracker.Modules.Task

{
    public partial class TaskControls : Form
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
        private DataTable AllTaskData = new DataTable();
        private DataTable masterData = new DataTable();

        public int SelectedTaskID = 0;
        public string SelectedTaskTitle = "";
        public string selectStatus = "";
        public string selectPriority = "";
        public string selectDeadline = "";
        private int currentPage = 1;
        private int pageSize = 0;
        private string sortedColumn = "CreatedAt";
        private System.Windows.Forms.SortOrder currentSortOrder = System.Windows.Forms.SortOrder.Descending;

        private ErrorProvider errorProvider1 = new ErrorProvider();
        private bool ignoreEvents { get; set; }
        private DateTime fromDate { get; set; }
        private DateTime toDate { get; set; }
        private bool validFromDate { get; set; }
        private bool validToDate { get; set; }
        private static readonly string[] DateFormats = new[]
        {
            "yyyy-MM-dd",
            "dd-MM-yyyy",
            "MM/dd/yyyy",
            "yyyy/MM/dd",
            "dd/MM/yyyy"
        };

        public TaskControls()
        {
            InitializeComponent();
            StyleTaskGrid();
            this.Resize += TaskControls_Resize;

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnFilter, "Filter Tasks");
            toolTip.SetToolTip(btnRefresh, "Refresh List");
            toolTip.SetToolTip(btnExport, "Export Tasks");
        }

        private void TaskControls_Load(object sender, EventArgs e)
        {
            ignoreEvents = true;
            CommonUiFunction.LoadInComboBox("spGetAllTaskPriorities", "Select Priority", cmbPriority);
            CommonUiFunction.LoadInComboBox("spGetAllTaskStatus", "Select Status", cmbStatus);
            CommonUiFunction.SetComboBoxHeightAndOwnerDraw(cmbPriority);
            CommonUiFunction.SetComboBoxHeightAndOwnerDraw(cmbStatus);

            txtSearch.Text = "Search...";
            txtSearch.ForeColor = Color.Gray;
            cmbPriority.ForeColor = Color.Gray;
            cmbStatus.ForeColor = Color.Gray;
            cmbPriority.SelectedIndexChanged += cmbPriority_SelectedIndexChanged;
            cmbStatus.SelectedIndexChanged += cmbStatus_SelectedIndexChanged;

            pageSize = GetRowsPerPage();
            int userID = Session.LogedInUser.GetUserId();

            txtFromdate.ReadOnly = true;
            txtToDate.ReadOnly = true;
            txtFromdate.TextChanged += txtFromdate_TextChanged;
            txtToDate.TextChanged += txtToDate_TextChanged;
            ignoreEvents = false;

            LoadTaskData(userID);
            SetPanelRadius();
            this.MouseDown += TaskControls_MouseDown;
            RegisterMouseDown(this);
            HideAllFilterPanels();
            DesignContextMenu();
            HidePopupPanels();
            txtFromdate.ReadOnly = true;
            txtToDate.ReadOnly = true;
            monthCalendarToDate.MaxDate = DateTime.Today;
            monthCalendarFromDate.MaxDate = DateTime.Today;
            this.Resize += TaskControls_Resize;
            dataGridViewTask.EnableHeadersVisualStyles = false;
            dataGridViewTask.CellPainting += dataGridViewTask_CellPainting;
            dataGridViewTask.CellFormatting += dataGridViewTask_CellFormatting;
            dataGridViewTask.CellClick += dataGridViewTask_CellContentClick;
            cmsFilter.Opening += cmsFilter_Opening;

            //Padding Add 
            dataGridViewTask.Columns["colPriority"].HeaderCell.Style.Padding = new Padding(20, 0, 0, 0);

            dataGridViewTask.Columns["colStatus"].HeaderCell.Style.Padding = new Padding(20, 0, 0, 0);

            dataGridViewTask.Columns["colDeadline"].HeaderCell.Style.Padding = new Padding(17, 0, 0, 0);
            RegisterMouseDown(this);



        }
        //Applies  styling to the Task Context Menu.
        public void LoadTaskData(int userID)
        {
            DataTable dataTable = CommonUiFunction.RetrieveDataForGridView("spGetAllTasks", userID);
            if (dataTable == null || dataTable.Columns.Contains("Message") || dataTable.Rows.Count == 0)
            {
                dataGridViewTask.DataSource = null;
                lblTaskStartingPageNumber.Text = "0";
                lblTaskEndingPageNumber.Text = "0";
                lblTaskTotalPageNumber.Text = "0";
                lblTotalTaskCount.Text = "0";
                lblTaskCompleteCount.Text = "0";
                lblTaskPandingCount.Text = "0";
                lblLentAmount.Text = "0";
                return;
            }

            masterData = dataTable.Copy();
            AllTaskData = dataTable;
            sortedColumn = "CreatedAt";
            currentSortOrder = System.Windows.Forms.SortOrder.Descending;
            ApplyTaskSort();
            currentPage = 1;
            ShowCurrentPage();
        }

        public Boolean LoadFilteredTaskData(string spName, string paramName, int filterId)
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
            if (dataTable.Rows.Count <= 0)
            {
                MessageBox.Show("No Record Found.",
                                "Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }
            AllTaskData = dataTable;
            currentPage = 1;
            ShowCurrentPage();
            return true;
        }

        public Boolean LoadFilteredTaskData(string spName, int userId, string paramName1, DateTime paramId1, string paramName2, DateTime paramId2)
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
                MessageBox.Show("No Record Found.",
                                "Information",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return false;
            }
            AllTaskData = dataTable;
            masterData = dataTable.Copy();
            currentPage = 1;
            ShowCurrentPage();
            return true;
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
            tsmiEdit.AutoSize = false;
            tsmiEdit.Height = 30;


            tsmiUpdateStatus.AutoSize = false;
            tsmiUpdateStatus.Height = 30;

            tsmiDeleteTask.AutoSize = false;
            tsmiDeleteTask.Height = 30;

            tsmiCancel.AutoSize = false;
            tsmiCancel.Height = 30;

            // Delete Color
            tsmiDeleteTask.ForeColor = Color.Red;

            // Images
            tsmiEdit.Image = Properties.Resources.pen;
            tsmiUpdateStatus.Image = Properties.Resources.refresh1;
            tsmiDeleteTask.Image = Properties.Resources.trash;
            tsmiCancel.Image = Properties.Resources.delete;

            // Display Style
            tsmiEdit.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsmiUpdateStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsmiDeleteTask.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsmiCancel.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

            // Image Scaling
            tsmiEdit.ImageScaling = ToolStripItemImageScaling.None;
            tsmiUpdateStatus.ImageScaling = ToolStripItemImageScaling.None;
            tsmiDeleteTask.ImageScaling = ToolStripItemImageScaling.None;
            tsmiCancel.ImageScaling = ToolStripItemImageScaling.None;

            //filter cms
            cmsFilter.ShowImageMargin = true;
            cmsFilter.ShowCheckMargin = false;
            cmsFilter.ImageScalingSize = new Size(10, 10);

            tsmiDate.AutoSize = false;
            tsmiDate.Height = 30;
            

            tsmiPriority.AutoSize = false;
            tsmiPriority.Height = 30;

            tsmiStatus.AutoSize = false;
            tsmiStatus.Height = 30;

            tsmiDate.Image = Properties.Resources.calendar__1_;
            tsmiPriority.Image = Properties.Resources.shop;
            tsmiStatus.Image = Properties.Resources.loading;

            tsmiDate.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsmiPriority.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsmiStatus.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

            tsmiDate.ImageScaling = ToolStripItemImageScaling.None;
            tsmiPriority.ImageScaling = ToolStripItemImageScaling.None;
            tsmiStatus.ImageScaling = ToolStripItemImageScaling.None;

        }
        private void cmsFilter_Opening(object sender, CancelEventArgs e)
        {
            tsmiDate.AutoSize = false;
            tsmiPriority.AutoSize = false;
            tsmiStatus.AutoSize = false;
            tsmiDate.Width = cmsFilter.Width;
            tsmiPriority.Width = cmsFilter.Width;
            tsmiStatus.Width = cmsFilter.Width;
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

                SelectedTaskTitle = Convert.ToString(((DataRowView)dataGridViewTask.Rows[e.RowIndex].DataBoundItem)["TaskTitle"]);
                selectStatus = Convert.ToString(((DataRowView)dataGridViewTask.Rows[e.RowIndex].DataBoundItem)["TaskStatusName"]);
                selectPriority = Convert.ToString(((DataRowView)dataGridViewTask.Rows[e.RowIndex].DataBoundItem)["PriorityName"]);
                selectDeadline = Convert.ToString(((DataRowView)dataGridViewTask.Rows[e.RowIndex].DataBoundItem)["Deadline"]);
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
            tsmiEdit.AutoSize = false;
            tsmiUpdateStatus.AutoSize = false;
            tsmiDeleteTask.AutoSize = false;
            tsmiCancel.AutoSize = false;
            tsmiCancel.Width = cmsTaskAction.Width;
            tsmiDeleteTask.Width = cmsTaskAction.Width;
            tsmiUpdateStatus.Width = cmsTaskAction.Width;
            tsmiEdit.Width = cmsTaskAction.Width;
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

            LoadTaskData(Session.LogedInUser.GetUserId());
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            UpdateTaskStatus updateTaskStatus = new UpdateTaskStatus(this);
            updateTaskStatus.ShowDialog();
            LoadTaskData(Session.LogedInUser.GetUserId());
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DeleteTask deleteTask = new DeleteTask(this); 

            deleteTask.ShowDialog();

            LoadTaskData(Session.LogedInUser.GetUserId());
        }



        private void StyleTaskGrid()
        {
            colDate.DataPropertyName = "CreatedAt";
            colTask.DataPropertyName = "TaskTitle";
            colPriority.DataPropertyName = "PriorityName";
            colStatus.DataPropertyName = "TaskStatusName";
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
            colDate.DefaultCellStyle.BackColor = Color.White;
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
            dataGridViewTask.RowTemplate.Height = 40;
            dataGridViewTask.RowHeadersVisible = false;
            dataGridViewTask.MultiSelect = false;
            dataGridViewTask.ReadOnly = true;
            dataGridViewTask.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //// Zigzag

            //Normal Row
            dataGridViewTask.DefaultCellStyle.BackColor = Color.White;
            dataGridViewTask.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            //  Alternating Row (Zigzag - Soft Slate Tint)
            dataGridViewTask.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(244, 247, 250);
            dataGridViewTask.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);

            //  Selection Color (একই সিলেকশন কালার)
            dataGridViewTask.DefaultCellStyle.SelectionBackColor = Color.FromArgb(174, 205, 247); // Royal Blue
            dataGridViewTask.DefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridViewTask.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(174, 205, 247);
            dataGridViewTask.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;


            ////// Zigzag
            ////Normal Row
            //dataGridViewTask.DefaultCellStyle.BackColor = Color.White;
            //dataGridViewTask.DefaultCellStyle.ForeColor = Color.Black;

            //// Alternating Row Style 
            //dataGridViewTask.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 253, 244);
            //dataGridViewTask.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);

            //// Selected Row Style 
            //dataGridViewTask.DefaultCellStyle.SelectionBackColor = Color.FromArgb(5, 150, 105);
            //dataGridViewTask.DefaultCellStyle.SelectionForeColor = Color.White;

            //dataGridViewTask.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(5, 150, 105);
            //dataGridViewTask.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;


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
            e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);
            int iconSize = 16;
            int spacing = 6;

            SizeF textSize = e.Graphics.MeasureString(text, e.CellStyle.Font);

            int totalWidth = iconSize + spacing + (int)textSize.Width;

            int startX = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;

            int iconY = e.CellBounds.Y + (e.CellBounds.Height - iconSize) / 2;

            e.Graphics.DrawImage(icon, startX, iconY, iconSize, iconSize);

            using (Brush brush =
                new SolidBrush(Color.FromArgb(80, 60, 180)))
            {
                float textX = startX + iconSize + spacing;

                float textY = e.CellBounds.Y + (e.CellBounds.Height - textSize.Height) / 2;

                e.Graphics.DrawString(text, e.CellStyle.Font, brush, textX, textY);
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
        private void dataGridViewTask_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            DataGridViewColumn column = dataGridViewTask.Columns[e.ColumnIndex];

            string columnName = column.DataPropertyName;

            
            if (columnName != "CreatedAt" &&
                columnName != "TaskTitle" &&
                columnName != "PriorityName" &&
                columnName != "TaskStatusName" &&
                columnName != "Deadline")
                
            {
                return;
            }

            // Same column click ASC <-> DESC
            if (sortedColumn == columnName)
            {
                currentSortOrder =
                    currentSortOrder == WinFormsSortOrder.Ascending
                    ? WinFormsSortOrder.Descending
                    : WinFormsSortOrder.Ascending;
            }
            else
            {

                sortedColumn = columnName;
                currentSortOrder = WinFormsSortOrder.Ascending;
            }

            ApplyTaskSort();

            currentPage = 1;

            ShowCurrentPage();
        }



        private void ApplyTaskSort()
        {
            if (string.IsNullOrEmpty(sortedColumn) ||
                currentSortOrder == WinFormsSortOrder.None)
                return;

            if (AllTaskData == null ||
                AllTaskData.Rows.Count == 0)
                return;

            if (!AllTaskData.Columns.Contains(sortedColumn))
                return;

            DataView view = AllTaskData.DefaultView;

            string direction =
                currentSortOrder == WinFormsSortOrder.Ascending
                ? "ASC"
                : "DESC";

            view.Sort =
                "[" + sortedColumn + "] " + direction;

            AllTaskData = view.ToTable();
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
            Common.CommonUiFunction.HighlightSearch(dataGridViewTask, txtSearch);
            int start = startIndex + 1;
            int end = endIndex;
            int total = AllTaskData.Rows.Count;

            lblTaskStartingPageNumber.Text = total == 0 ? "0" : start.ToString();
            lblTaskEndingPageNumber.Text = end.ToString();
            lblTaskTotalPageNumber.Text = total.ToString();
            UpdateTaskSummaryCards();
        }

        private void UpdateTaskSummaryCards()
        {
            if (AllTaskData == null || AllTaskData.Rows.Count == 0)
            {
                lblTotalTaskCount.Text = "0";
                lblTaskCompleteCount.Text = "0";
                lblTaskPandingCount.Text = "0";
                lblLentAmount.Text = "0";
                return;
            }

            // ১. Total Task
            int totalTasks = AllTaskData.Rows.Count;
            lblTotalTaskCount.Text = totalTasks.ToString();

            // ২. Complete Task ("Complete" এবং "Completed" দুটি বানানের জন্যই সেফ-চেক)
            int completedTasks = AllTaskData.AsEnumerable()
                .Count(row =>
                {
                    string status = Convert.ToString(row["TaskStatusName"]);
                    return status.Equals("Complete", StringComparison.OrdinalIgnoreCase) ||
                           status.Equals("Completed", StringComparison.OrdinalIgnoreCase);
                });
            lblTaskCompleteCount.Text = completedTasks.ToString();

            // ৩. Pending Task ("Pending" এবং "In Progress")
            int pendingTasks = AllTaskData.AsEnumerable()
                .Count(row =>
                {
                    string status = Convert.ToString(row["TaskStatusName"]);
                    return status.Equals("Pending", StringComparison.OrdinalIgnoreCase) ||
                           status.Equals("In Progress", StringComparison.OrdinalIgnoreCase);
                });
            lblTaskPandingCount.Text = pendingTasks.ToString();

            // ৪. Due Today
            DateTime today = DateTime.Today;
            int dueTodayTasks = AllTaskData.AsEnumerable()
                .Count(row => row["Deadline"] != DBNull.Value &&
                              Convert.ToDateTime(row["Deadline"]).Date == today);
            lblLentAmount.Text = dueTodayTasks.ToString();
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
        private void tsmiStatus_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlStatusFilter);
        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            HidePopupPanels();
            cmsFilter.Show(btnFilter, 0, btnFilter.Height);
        }

        private void btnPriorityClose_Click(object sender, EventArgs e)
        {
            ignoreEvents = true;
            cmbPriority.SelectedIndex = 0;
            ignoreEvents = false;
            pnlPriorityFilter.Visible = false;
            LoadTaskData(Session.LogedInUser.GetUserId());
        }

        private void btnStatusClose_Click(object sender, EventArgs e)
        {
            ignoreEvents = true;
            cmbStatus.SelectedIndex = 0;
            ignoreEvents = false;
            pnlStatusFilter.Visible = false;
            LoadTaskData(Session.LogedInUser.GetUserId());
        }

        private void HideAllFilterPanels()
        {
            HidePopupPanels();
            pnlDateFilter.Visible = false;
            pnlPriorityFilter.Visible = false;
            pnlStatusFilter.Visible = false;

        }
        private void HidePopupPanels()
        {
            monthCalendarFromDate.Visible = false;
            monthCalendarToDate.Visible = false;
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            AllTaskData = Common.CommonUiFunction.SearchDataInTask(masterData, txtSearch);
            ShowCurrentPage();
        }

        private void ShowCalenderFromDatePanel(MonthCalendar monthCalendar)
        {
            HidePopupPanels();
            monthCalendar.Parent = this;
            Point p = txtFromdate.PointToScreen(
                      new Point(0, txtFromdate.Height + 10));
            p = this.PointToClient(p);
            monthCalendar.Location = p;
            monthCalendar.BringToFront();
            monthCalendar.Visible = true;
        }
        private void ShowCalenderToDatePanel(MonthCalendar monthCalendar)
        {
            HidePopupPanels();
            monthCalendar.Parent = this;
            Point p = txtToDate.PointToScreen(
                new Point(0, txtToDate.Height + 10));
            p = this.PointToClient(p);
            monthCalendar.Location = p;
            monthCalendar.BringToFront();
            monthCalendar.Visible = true;
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
            if (monthCalendarFromDate.Visible)
            {
                bool clickInsideCalendar =
                    monthCalendarFromDate.Bounds.Contains(mousePos);

                bool clickOnCalendarIcon =
                    picCalenderFromDate.RectangleToScreen(
                        picCalenderFromDate.ClientRectangle)
                        .Contains(Control.MousePosition);

                bool clickOnTextBox =
                    txtFromdate.RectangleToScreen(
                        txtFromdate.ClientRectangle)
                        .Contains(Control.MousePosition);

                if (!clickInsideCalendar &&
                    !clickOnCalendarIcon &&
                    !clickOnTextBox)
                {
                    monthCalendarFromDate.Visible = false;
                }
            }

            // To Date Calendar
            if (monthCalendarToDate.Visible)
            {
                bool clickInsideCalendar =
                    monthCalendarToDate.Bounds.Contains(mousePos);

                bool clickOnCalendarIcon =
                    picCalenderToDate.RectangleToScreen(
                        picCalenderToDate.ClientRectangle)
                        .Contains(Control.MousePosition);

                bool clickOnTextBox =
                    txtToDate.RectangleToScreen(
                        txtToDate.ClientRectangle)
                        .Contains(Control.MousePosition);

                if (!clickInsideCalendar &&
                    !clickOnCalendarIcon &&
                    !clickOnTextBox)
                {
                    monthCalendarToDate.Visible = false;
                }
            }
        }

        private void btnDateClose_Click_1(object sender, EventArgs e)
        {
            ignoreEvents = true;

            txtFromdate.Clear();
            txtToDate.Clear();

            this.fromDate = DateTime.MinValue;
            this.toDate = DateTime.MinValue;
            this.validFromDate = false;
            this.validToDate = false;

            monthCalendarFromDate.Visible = false;
            monthCalendarToDate.Visible = false;
            errorProvider1.Clear();
            ErrorHelper.HideErrorForControl(pnlFromDate);
            ErrorHelper.HideErrorForControl(pnlToDate);

            pnlDateFilter.Visible = false;

            ignoreEvents = false;
            LoadTaskData(Session.LogedInUser.GetUserId());
        }

        private void txtFromdate_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            errorProvider1.Clear();
            ErrorHelper.HideErrorForControl(pnlFromDate);
            ErrorHelper.HideErrorForControl(pnlToDate);

            if (string.IsNullOrWhiteSpace(txtFromdate.Text) || txtFromdate.Text == "Select Date")
            {
                this.fromDate = DateTime.MinValue;
                return;
            }

            DateTime parsedFromDate;
            if (!DateTime.TryParseExact(
                    txtFromdate.Text.Trim(),
                    DateFormats,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out parsedFromDate))
            {
                return;
            }

            this.fromDate = parsedFromDate;

            if (string.IsNullOrWhiteSpace(txtToDate.Text) || txtToDate.Text == "Select Date")
            {
                ignoreEvents = true;
                DateTime defaultToDate = this.fromDate > DateTime.Today ? this.fromDate : DateTime.Today;
                txtToDate.Text = defaultToDate.ToString("dd-MM-yyyy");
                ignoreEvents = false;
                this.toDate = defaultToDate;
            }
            else
            {
                DateTime parsedToDate;
                if (DateTime.TryParseExact(
                        txtToDate.Text.Trim(),
                        DateFormats,
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
                        out parsedToDate))
                {
                    this.toDate = parsedToDate;
                }
            }

            BLLayer.Task.TaskBLL taskBll = new BLLayer.Task.TaskBLL();
            taskBll.fromDate = this.fromDate;
            taskBll.toDate = this.toDate;

            CommonValidator.ValidationResult result = taskBll.DateValidatorIntoTaskBll();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    validFromDate = true;
                    if (!LoadFilteredTaskData(
                            "spGetTasksBetweenDates",
                            Session.LogedInUser.GetUserId(),
                            "@FromDate",
                            this.fromDate,
                            "@ToDate",
                            this.toDate))
                    {
                        ignoreEvents = true;
                        txtFromdate.Clear();
                        txtToDate.Clear();
                        ignoreEvents = false;
                        this.fromDate = DateTime.MinValue;
                        this.toDate = DateTime.MinValue;

                        LoadTaskData(Session.LogedInUser.GetUserId());
                    }
                    break;

                case CommonValidator.ValidationResult.DateRangeInvalid:
                    validFromDate = false;
                    ErrorHelper.ShowValidationError(result, errorProvider1, pnlFromDate, pnlToDate);
                    MessageBox.Show("From Date cannot be greater than To Date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ignoreEvents = true;
                    txtFromdate.Clear();
                    txtToDate.Clear();
                    ignoreEvents = false;
                    this.fromDate = DateTime.MinValue;
                    this.toDate = DateTime.MinValue;
                    LoadTaskData(Session.LogedInUser.GetUserId());
                    break;
            }
        }

        private void txtToDate_TextChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            errorProvider1.Clear();
            ErrorHelper.HideErrorForControl(pnlFromDate);
            ErrorHelper.HideErrorForControl(pnlToDate);

            if (string.IsNullOrWhiteSpace(txtToDate.Text) || txtToDate.Text == "Select Date")
            {
                this.toDate = DateTime.MinValue;
                return;
            }

            DateTime parsedToDate;
            if (!DateTime.TryParseExact(
                    txtToDate.Text.Trim(),
                    DateFormats,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out parsedToDate))
            {
                return;
            }

            this.toDate = parsedToDate;

            if (string.IsNullOrWhiteSpace(txtFromdate.Text) || txtFromdate.Text == "Select Date")
            {
                return;
            }

            DateTime parsedFromDate;
            if (DateTime.TryParseExact(
                    txtFromdate.Text.Trim(),
                    DateFormats,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out parsedFromDate))
            {
                this.fromDate = parsedFromDate;
            }

            BLLayer.Task.TaskBLL taskBll = new BLLayer.Task.TaskBLL();
            taskBll.fromDate = this.fromDate;
            taskBll.toDate = this.toDate;

            CommonValidator.ValidationResult result = taskBll.DateValidatorIntoTaskBll();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    validFromDate = true;
                    if (!LoadFilteredTaskData(
                            "spGetTasksBetweenDates",
                            Session.LogedInUser.GetUserId(),
                            "@FromDate",
                            this.fromDate,
                            "@ToDate",
                            this.toDate))
                    {
                        ignoreEvents = true;
                        txtFromdate.Clear();
                        txtToDate.Clear();
                        ignoreEvents = false;
                        this.fromDate = DateTime.MinValue;
                        this.toDate = DateTime.MinValue;

                        LoadTaskData(Session.LogedInUser.GetUserId());
                    }
                    break;

                case CommonValidator.ValidationResult.DateRangeInvalid:
                    validFromDate = false;
                    ErrorHelper.ShowValidationError(result, errorProvider1, pnlFromDate, pnlToDate);
                    MessageBox.Show("From Date cannot be greater than To Date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ignoreEvents = true;
                    txtFromdate.Clear();
                    txtToDate.Clear();
                    ignoreEvents = false;
                    this.fromDate = DateTime.MinValue;
                    this.toDate = DateTime.MinValue;
                    LoadTaskData(Session.LogedInUser.GetUserId());
                    break;
            }
        }

        private void picCalenderFromDate_Click_1(object sender, EventArgs e)
        {
            if (monthCalendarFromDate.Visible)
            {
                monthCalendarFromDate.Visible = false;
            }
            else
            {
                monthCalendarToDate.Visible = false;
                ShowCalenderFromDatePanel(monthCalendarFromDate);
            }
        }

        private void picCalenderToDate_Click_1(object sender, EventArgs e)
        {
            if (monthCalendarToDate.Visible)
            {
                monthCalendarToDate.Visible = false;
            }
            else
            {
                monthCalendarFromDate.Visible = false;
                ShowCalenderToDatePanel(monthCalendarToDate);
            }
        }

        private void monthCalendarFromDate_DateSelected(object sender, DateRangeEventArgs e)
        {
            fromDate = e.Start.Date;
            txtFromdate.Text = e.Start.ToString("dd-MM-yyyy");
            monthCalendarFromDate.Visible = false;
        }

        private void monthCalendarToDate_DateSelected(object sender, DateRangeEventArgs e)
        {
            toDate = e.Start.Date;
            txtToDate.Text = e.Start.ToString("dd-MM-yyyy");
            monthCalendarToDate.Visible = false;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void cmbPriority_Click(object sender, EventArgs e)
        {
            cmbPriority.DroppedDown = true;
        }

        private void cmbPriority_Enter(object sender, EventArgs e)
        {
            if (cmbPriority.Text != "Select Priority" && cmbPriority.SelectedIndex > 0)
                cmbPriority.ForeColor = Color.Black;
            else
                cmbPriority.ForeColor = Color.Gray;
        }

        private void cmbPriority_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbPriority.Text) || cmbPriority.Text == "Select Priority" || cmbPriority.SelectedIndex <= 0)
            {
                cmbPriority.Text = "Select Priority";
                cmbPriority.ForeColor = Color.Gray;
            }
            else
            {
                cmbPriority.ForeColor = Color.Black;
            }
        }

        private void cmbStatus_Click(object sender, EventArgs e)
        {
            cmbStatus.DroppedDown = true;
        }

        private void cmbStatus_Enter(object sender, EventArgs e)
        {
            if (cmbStatus.Text != "Select Status" && cmbStatus.SelectedIndex > 0)
                cmbStatus.ForeColor = Color.Black;
            else
                cmbStatus.ForeColor = Color.Gray;
        }

        private void cmbStatus_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbStatus.Text) || cmbStatus.Text == "Select Status" || cmbStatus.SelectedIndex <= 0)
            {
                cmbStatus.Text = "Select Status";
                cmbStatus.ForeColor = Color.Gray;
            }
            else
            {
                cmbStatus.ForeColor = Color.Black;
            }
        }

        private void txtFromdate_Click(object sender, EventArgs e)
        {
            if (monthCalendarFromDate.Visible)
            {
                monthCalendarFromDate.Visible = false;
            }
            else
            {
                monthCalendarToDate.Visible = false;
                ShowCalenderFromDatePanel(monthCalendarFromDate);
            }
        }

        private void txtToDate_Click(object sender, EventArgs e)
        {
            if (monthCalendarToDate.Visible)
            {
                monthCalendarToDate.Visible = false;
            }
            else
            {
                monthCalendarFromDate.Visible = false;
                ShowCalenderToDatePanel(monthCalendarToDate);
            }
        }

        private void pnlDateHeader_Click(object sender, EventArgs e)
        {
            HidePopupPanels();
        }

        private void pnlTableHeader_Click(object sender, EventArgs e)
        {
            HidePopupPanels();
        }

        private void tblCardContant_Click(object sender, EventArgs e)
        {
            HidePopupPanels();
        }

        private void dataGridViewTask_Click(object sender, EventArgs e)
        {
            HidePopupPanels();
        }

        private void cmbPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            int priorityId;
            if (cmbPriority.SelectedValue != null && int.TryParse(cmbPriority.SelectedValue.ToString(), out priorityId))
            {
                if (priorityId > 0)
                {
                    cmbPriority.ForeColor = Color.Black;
                    if (!LoadFilteredTaskData("spFilterTasksByPriority", "@PriorityID", priorityId))
                    {
                        ignoreEvents = true;
                        cmbPriority.SelectedIndex = 0;
                        cmbPriority.ForeColor = Color.Gray;
                        ignoreEvents = false;
                        LoadTaskData(Session.LogedInUser.GetUserId());
                    }
                }
                else
                {
                    cmbPriority.ForeColor = Color.Gray;
                    LoadTaskData(Session.LogedInUser.GetUserId());
                }
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreEvents) return;
            int statusId;
            if (cmbStatus.SelectedValue != null && int.TryParse(cmbStatus.SelectedValue.ToString(), out statusId))
            {
                if (statusId > 0)
                {
                    cmbStatus.ForeColor = Color.Black;
                    if (!LoadFilteredTaskData("spFilterTasksByStatus", "@TaskStatusID", statusId))
                    {
                        ignoreEvents = true;
                        cmbStatus.SelectedIndex = 0;
                        cmbStatus.ForeColor = Color.Gray;
                        ignoreEvents = false;
                        LoadTaskData(Session.LogedInUser.GetUserId());
                    }
                }
                else
                {
                    cmbStatus.ForeColor = Color.Gray;
                    LoadTaskData(Session.LogedInUser.GetUserId());
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            HideAllFilterPanels();
            HidePopupPanels();
            ignoreEvents = true;
            txtFromdate.Clear();
            txtToDate.Clear();
            if (cmbPriority.Items.Count > 0) cmbPriority.SelectedIndex = 0;
            if (cmbStatus.Items.Count > 0) cmbStatus.SelectedIndex = 0;
            txtSearch.Clear();
            txtSearch_Leave(txtSearch, EventArgs.Empty);
            ignoreEvents = false;
            currentPage = 1;
            LoadTaskData(Session.LogedInUser.GetUserId());
            this.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HidePopupPanels();
        }

        

        //public static DataTable SearchDataInTask(DataTable masterTable, TextBox txtBox)
        //{
        //    string search = txtBox.Text.Trim().Replace("'", "''");

        //    if (masterTable == null) return null;
        //    if (string.IsNullOrWhiteSpace(search))
        //    {
        //        masterTable.DefaultView.RowFilter = "";
        //        return masterTable.DefaultView.ToTable();
        //    }

        //    masterTable.DefaultView.RowFilter = string.Format(
        //          "Convert(Amount, 'System.String') LIKE '%{0}%' OR " +
        //          "Convert({1}, 'System.String') LIKE '%{0}%' OR " +
        //          "CategoryName LIKE '%{0}%' OR " +
        //          "PaymentName LIKE '%{0}%' OR " +
        //          "SubCategoryName LIKE '%{0}%'",
        //           search, dateColumn);


        //    DataTable filteredTable = masterTable.DefaultView.ToTable();


        //    return filteredTable;
        //}

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (AllTaskData == null || AllTaskData.Rows.Count == 0)
            {
                MessageBox.Show(
                    "There is no data to export.",
                    "Export",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "Save Task Excel File";
                saveDialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                saveDialog.FileName =
                    "Task_" +
                    DateTime.Now.ToString("ddMMyyyy_HHmmss") +
                    ".xlsx";

                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    ExportTaskToExcel(
                        AllTaskData,
                        saveDialog.FileName);

                    MessageBox.Show(
                        "Task data exported successfully.",
                        "Export Successful",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Export failed.\n\n" + ex.Message,
                        "Export Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
        private void ExportTaskToExcel(DataTable dataTable, string filePath)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                excelApp = new Excel.Application();
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                workbook = excelApp.Workbooks.Add(
                    Excel.XlWBATemplate.xlWBATWorksheet);

                worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                worksheet.Name = "Task";

                // Column Names
                for (int col = 0;
                     col < dataTable.Columns.Count;
                     col++)
                {
                    worksheet.Cells[1, col + 1] =
                        GetTaskExportColumnName(
                            dataTable.Columns[col].ColumnName);
                }

                // Data
                for (int row = 0;
                     row < dataTable.Rows.Count;
                     row++)
                {
                    for (int col = 0;
                         col < dataTable.Columns.Count;
                         col++)
                    {
                        if (dataTable.Rows[row][col] != DBNull.Value)
                        {
                            worksheet.Cells[row + 2, col + 1] =
                                dataTable.Rows[row][col].ToString();
                        }
                    }
                }

                // Header bold
                Excel.Range headerRange =
                    worksheet.Range[
                        worksheet.Cells[1, 1],
                        worksheet.Cells[
                            1,
                            dataTable.Columns.Count]];

                headerRange.Font.Bold = true;

                // Auto fit columns
                worksheet.Columns.AutoFit();

                // SAVE EXCEL FILE
                workbook.SaveAs(
                    filePath,
                    Excel.XlFileFormat.xlOpenXMLWorkbook);

                // Close Excel without opening it
                workbook.Close(false);
                excelApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Excel export failed.\n\n" + ex.Message,
                    "Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                if (workbook != null)
                {
                    try
                    {
                        workbook.Close(false);
                    }
                    catch { }
                }

                if (excelApp != null)
                {
                    try
                    {
                        excelApp.Quit();
                    }
                    catch { }
                }
            }
            finally
            {
                // Release COM objects
                if (worksheet != null)
                    Marshal.ReleaseComObject(worksheet);

                if (workbook != null)
                    Marshal.ReleaseComObject(workbook);

                if (excelApp != null)
                    Marshal.ReleaseComObject(excelApp);

                worksheet = null;
                workbook = null;
                excelApp = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        private string GetTaskExportColumnName(string columnName)
        {
            switch (columnName)
            {

            

                case "CreatedAt":
                    return "Date";

                case "TaskTitle":
                    return "Task Title";

                case "PriorityName":
                    return "Priority Name";

                case "TaskStatusName":
                    return "Status Name";

                case "StatusName":
                    return "Status";
                    

                case "Deadline":
                    return "Deadline";

                default:
                    return columnName;
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

    }
}
