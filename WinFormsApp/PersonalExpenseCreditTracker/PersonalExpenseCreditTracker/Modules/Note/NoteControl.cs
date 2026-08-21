using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using System.Configuration;
using PersonalExpenseCreditTracker.Session;
using PersonalExpenseCreditTracker.Common;
using System.Runtime.InteropServices;
using BLLayer.Common;
using PersonalExpenseCreditTracker.Forms.Main;
using Excel = Microsoft.Office.Interop.Excel;
using PersonalExpenseCreditTracker.Helpers;

namespace PersonalExpenseCreditTracker.Modules.Note
{
    public partial class NoteControl : Form
    {
        int userID = Session.LogedInUser.GetUserId();

        public int SelectedNoteID = 0;
        public string SelectedNoteTitle = "";
        public string SelectedDescription = "";
        public string SelectedPriority = "";
        public string SelectedColorName = "";
        public string SelectedColorHexCode = "";
        public string SelectedCreatedAt = "";

        

        [DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);
        private string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private DataTable AllNoteData = new DataTable();
        private DataTable masterData = new DataTable();
        private int currentPage = 1;
        private int pageSize ;

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

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);


        public NoteControl()
        {
            InitializeComponent();
            Resize += NoteControl_Resize;

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnFilter, "Filter Notes");
            toolTip.SetToolTip(btnRefresh, "Refresh List");
            toolTip.SetToolTip(btnExport, "Export Notes");
            toolTip.SetToolTip(txtSearch,"Search by Note Title, Priority or Date");

        }

        private void NoteControl_Load(object sender, EventArgs e)
        {
            
            
            foreach (Control c in flpNotes.Controls)
            {
                if (c is Panel)
                {
                    SetRadius(c, 20);
                }
            }
            ignoreEvents = true;
            CommonUiFunction.LoadInComboBox("spGetAllTaskPriorities", "Select Priority", cmbPriority);
            CommonUiFunction.SetComboBoxHeightAndOwnerDraw(cmbPriority);
            cmbPriority.ForeColor = Color.Gray;
            cmbPriority.SelectedIndexChanged += cmbPriority_SelectedIndexChanged;
            txtSearch.Text = "Search records...";
            txtSearch.ForeColor = Color.Gray;

            DesignContextMenu();
            ResizeNoteCards();
            SetRoundedPanel(pnlTotalNotes, 20);
            SetRoundedPanel(pnlImportant, 20);
            SetRoundedPanel(pnlThisMonth, 20);
            HideAllFilterPanels();
            DesignContextMenu();
            this.MouseDown += NoteControls_MouseDown;
            RegisterMouseDown(this);

            txtFromdate.ReadOnly = true;
            txtToDate.ReadOnly = true;
            monthCalendarToDate.MaxDate = DateTime.Today;
            monthCalendarFromDate.MaxDate = DateTime.Today;
            txtFromdate.TextChanged += txtFromdate_TextChanged;
            txtToDate.TextChanged += txtToDate_TextChanged;
            ignoreEvents = false;

            LoadNoteData(userID);
            cmsFilter.Opening += cmsFilter_Opening;
            RegisterMouseDown(this);

            cmsNote.RenderMode = ToolStripRenderMode.ManagerRenderMode; // Default renderer
            cmsNote.ShowImageMargin = true; // Enable left icon margin
            cmsNote.ShowCheckMargin = false;
            cmsNote.ImageScalingSize = new Size(10, 10);

            viewToolStripMenuItem.AutoSize = false;
            viewToolStripMenuItem.Height = 30;

            editToolStripMenuItem.AutoSize = false;
            editToolStripMenuItem.Height = 30;

            deleteToolStripMenuItem.AutoSize = false;
            deleteToolStripMenuItem.Height = 30;

            cmsNote.Opening += cmsNote_Opening;




        }

        private void cmsNote_Opening(object sender, CancelEventArgs e)
        {
            viewToolStripMenuItem.Width = cmsNote.Width;
            editToolStripMenuItem.Width = cmsNote.Width;
            deleteToolStripMenuItem.Width = cmsNote.Width;
        }



        private void cmsFilter_Opening(object sender, CancelEventArgs e)
        {
            tsmiDate.AutoSize = false;
            tsmiPriority.AutoSize = false;

            tsmiDate.Width = cmsFilter.Width;
            tsmiPriority.Width = cmsFilter.Width;
        }

        

        public Boolean LoadFilteredNoteData(string spName, string paramName, int filterId)
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
            AllNoteData = dataTable;
            masterData = dataTable.Copy();
            currentPage = 1;
            ShowCurrentPage();
            return true;
        }

        public Boolean LoadFilteredNoteData(string spName, int userId, string paramName1, DateTime paramId1, string paramName2, DateTime paramId2)
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
            AllNoteData = dataTable;
            masterData = dataTable.Copy();
            currentPage = 1;
            ShowCurrentPage();
            return true;
        }
        public Boolean LoadFilteredNoteData(string spName, int userId, string paramName1, Decimal paramId1, string paramName2, Decimal paramId2)
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
            AllNoteData = dataTable;
            masterData = dataTable.Copy();
            currentPage = 1;
            ShowCurrentPage();
            return true;
        }
        public void LoadNoteData(int userID)
        {
            DataTable dataTable = CommonUiFunction.RetrieveDataForGridView("spGetAllNotes", userID);
            if (dataTable == null || dataTable.Columns.Contains("Message") || dataTable.Rows.Count == 0)
            {
                AllNoteData = new DataTable();
                masterData = new DataTable();
                flpNotes.Controls.Clear();
                lblNoteStartingPageNumber.Text = "0";
                lblNoteEndingPageNumber.Text = "0";
                lblNoteTotalPageNumber.Text = "0";
                lblNoteTotal.Text = "0";
                lblNoteImportantNumber.Text = "0";
                lblMonthNoteNumber.Text = "0";
                return;
            }

            AllNoteData = dataTable;
            masterData = dataTable.Copy();
            currentPage = 1;
            ShowCurrentPage();
        }


        public Boolean LoadFilteredNotetData(string spName, string paramName, int filterId)
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
            AllNoteData = dataTable;
            masterData = dataTable.Copy();
            currentPage = 1;
            ShowCurrentPage();
            return true;
        }

        private Color GetTextColor(Color backgroundColor)
        {
            double brightness =
                (0.299 * backgroundColor.R) +
                (0.587 * backgroundColor.G) +
                (0.114 * backgroundColor.B);

            if (brightness < 160)
            {
                return Color.White;
            }

            return Color.Black;
        }

        private Color GetSecondaryTextColor(Color backgroundColor)
        {
            double brightness =
                (0.299 * backgroundColor.R) +
                (0.587 * backgroundColor.G) +
                (0.114 * backgroundColor.B);

            if (brightness < 160)
            {
                return Color.FromArgb(250, 250, 250);
            }

            return Color.FromArgb(80, 80, 80);
        }
        private void AddNoteCard(DataRow row)
        {
            Panel card = new Panel();

            string hexCode = row["ColorHexCode"].ToString();

            if (!string.IsNullOrWhiteSpace(hexCode))
            {
                card.BackColor = ColorTranslator.FromHtml(hexCode);
            }
            else
            {
                card.BackColor = Color.White;
            }

            Color textColor = GetTextColor(card.BackColor);
            Color secondaryTextColor = GetSecondaryTextColor(card.BackColor);

            card.Size = new Size(331, 170);
            card.Margin = new Padding(10);
            card.Padding = new Padding(10);
            Label title = new Label();

            title.Text = row["NoteTitle"].ToString();
            title.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            title.ForeColor = textColor;
            title.Location = new Point(10, 10);
            title.AutoSize = true;

            Label description = new Label();

            description.Name = "lblNoteCardDescription";
            description.Text = row["Description"].ToString();
            description.Font = new Font("Segoe UI", 10f);
            description.ForeColor = secondaryTextColor;
            description.Location = new Point(15, 45);
            description.AutoEllipsis = true;
            Panel footer = new Panel();

            footer.Dock = DockStyle.Bottom;
            footer.Height = 35;
            //footer.Location = new Point(15,140);
            Label date = new Label();

            date.Text = Convert.ToDateTime(row["CreatedAt"]) .ToString("dd MMM yyyy");

            date.Font = new Font("Segoe UI", 10F);
            date.ForeColor = secondaryTextColor;
            date.AutoSize = true;
            date.Location = new Point(0, 8);
            Label priority = new Label();

            priority.Text = row["NotePriorityName"].ToString();
            priority.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            priority.ForeColor = textColor;
            priority.AutoSize = true;
            priority.Location = new Point(150, 8);
            Button btnMore = new Button();

            btnMore.Size = new Size(30, 30);
            btnMore.Dock = DockStyle.Right;
            

            btnMore.FlatStyle = FlatStyle.Flat;
            btnMore.FlatAppearance.BorderSize = 0;
            
            btnMore.Image = Properties.Resources.more2;
            btnMore.Cursor = Cursors.Hand;
            btnMore.Click += delegate(object sender, EventArgs e)
            {
                cmsNote.Show(btnMore, new Point(0, (btnMore.Height)-10));
            };
            footer.Controls.Add(priority);
            footer.Controls.Add(btnMore);
            footer.Controls.Add(date);
            card.Controls.Add(title);
            card.Controls.Add(description);
            card.Controls.Add(footer);

            MouseEventHandler onNoteDoubleClick = delegate(object sender, MouseEventArgs e)
            {
                SelectedNoteID = Convert.ToInt32(row["NoteID"]);
                SelectedNoteTitle = row["NoteTitle"].ToString();
                SelectedDescription = row["Description"].ToString();
                SelectedPriority = row["NotePriorityName"].ToString();
                SelectedColorName = row["ColorName"].ToString();
                SelectedColorHexCode = row["ColorHexCode"].ToString();
                SelectedCreatedAt = row["CreatedAt"] != DBNull.Value
                    ? Convert.ToDateTime(row["CreatedAt"]).ToString("dd MMM yyyy")
                    : "";
                NoteViewDetailsControl view = new NoteViewDetailsControl(this);
                view.ShowDialog();
            };
            // Now this event is connected to the card and all its controls
            card.MouseDoubleClick += onNoteDoubleClick;
            title.MouseDoubleClick += onNoteDoubleClick;
            description.MouseDoubleClick += onNoteDoubleClick;
            footer.MouseDoubleClick += onNoteDoubleClick;
            date.MouseDoubleClick += onNoteDoubleClick;
            priority.MouseDoubleClick += onNoteDoubleClick;

            SetRadius(card, 20);


            flpNotes.Controls.Add(card);

            btnMore.Click += delegate(object sender, EventArgs e)
            {
                SelectedNoteID = Convert.ToInt32(row["NoteID"]);
                SelectedNoteTitle = row["NoteTitle"].ToString();
                SelectedDescription = row["Description"].ToString();
                SelectedPriority = row["NotePriorityName"].ToString();
                SelectedColorName = row["ColorName"].ToString();
                SelectedColorHexCode = row["ColorHexCode"].ToString();
                SelectedCreatedAt = (row["CreatedAt"] != DBNull.Value)
                    ? Convert.ToDateTime(row["CreatedAt"]).ToString("dd MMM yyyy")
                    : "";
                cmsNote.Show(btnMore, new Point(0, (btnMore.Height) - 10));
            };
        }

        private void UpdatePageSize()
        {
            int availableWidth = flpNotes.ClientSize.Width
                               - flpNotes.Padding.Left
                               - flpNotes.Padding.Right;

            int columns;

            if (availableWidth < 500)
                columns = 1;
            else if (availableWidth < 850)
                columns = 2;
            else if (availableWidth < 1150)
                columns = 3;
            else
                columns = 4;

            // Always show 3 rows
            pageSize = columns * 3;
        }

        private void ShowCurrentPage()
        {
           
            flpNotes.SuspendLayout();
            flpNotes.Controls.Clear();

            int start = (currentPage - 1) * pageSize;
            int end = Math.Min(start + pageSize, AllNoteData.Rows.Count);

            for (int i = start; i < end; i++)
            {
                AddNoteCard(AllNoteData.Rows[i]);
            }

            ResizeNoteCards();
            flpNotes.ResumeLayout();

            lblNoteStartingPageNumber.Text = (AllNoteData.Rows.Count == 0) ? "0" : (start + 1).ToString();
            lblNoteEndingPageNumber.Text = end.ToString();
            lblNoteTotalPageNumber.Text = AllNoteData.Rows.Count.ToString();

            int totalPages = (int)Math.Ceiling(AllNoteData.Rows.Count / (double)pageSize);

            btnCurrentPage.Text = currentPage.ToString();

            btnFirstpage.Enabled = currentPage > 1;
            btnPreviousPage.Enabled = currentPage > 1;

            btnNextpage.Enabled = currentPage < totalPages;
            btnLastPage.Enabled = currentPage < totalPages;

            if (AllNoteData != null)
            {
                lblNoteTotal.Text = AllNoteData.Rows.Count.ToString();

                int importantCount = AllNoteData.AsEnumerable().Count(row =>
                {
                    string priority = Convert.ToString(row["NotePriorityName"]);
                    return priority.Equals("High", StringComparison.OrdinalIgnoreCase) || priority.Equals("Important", StringComparison.OrdinalIgnoreCase);
                });
                lblNoteImportantNumber.Text = importantCount.ToString();

                DateTime now = DateTime.Now;
                int thisMonthCount = AllNoteData.AsEnumerable().Count(row =>
                {
                    if (row["CreatedAt"] != DBNull.Value)
                    {
                        DateTime dt = Convert.ToDateTime(row["CreatedAt"]);
                        return dt.Year == now.Year && dt.Month == now.Month;
                    }
                    return false;
                });
                lblMonthNoteNumber.Text = thisMonthCount.ToString();
            }
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
                    if (!LoadFilteredNoteData("spFilterNotesByPriority", "@PriorityID", priorityId))
                    {
                        ignoreEvents = true;
                        cmbPriority.SelectedIndex = 0;
                        cmbPriority.ForeColor = Color.Gray;
                        ignoreEvents = false;
                        LoadNoteData(userID);
                    }
                }
                else
                {
                    cmbPriority.ForeColor = Color.Gray;
                    LoadNoteData(userID);
                }
            }
            else
            {
                if (cmbPriority.SelectedIndex <= 0)
                {
                    cmbPriority.ForeColor = Color.Gray;
                }
            }
        }

        private void lblNoteSubtitle_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void PicNoteMenu_Click(object sender, EventArgs e)
        {

        }

       
        private void pnlTotalNotes_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlTotalNotes.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void picNoteImportant_Click(object sender, EventArgs e)
        {

        }

        private void flpNotes_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            cmsNote.Show(btnNoteMore, 0, btnNoteMore.Height);


        }

        private void NoteControl_Resize(object sender, EventArgs e)
        {
            ResizeNoteCards();
            UpdatePageSize();

            int totalPages = (int)Math.Ceiling(AllNoteData.Rows.Count / (double)pageSize);

            if (currentPage > totalPages)
                currentPage = totalPages;

            if (currentPage < 1)
                currentPage = 1;

           
            SetRoundedPanel(pnlTotalNotes, 15);
            SetRoundedPanel(pnlImportant, 15);
            SetRoundedPanel(pnlThisMonth, 15);
        }


        private void ResizeNoteCards()
        {
            int margin = 10;

            int availableWidth = flpNotes.ClientSize.Width
                                 - flpNotes.Padding.Left
                                 - flpNotes.Padding.Right;


            int columns;

            if (availableWidth < 500)
                columns = 1;
            else if (availableWidth < 850)
                columns = 2;
            else if (availableWidth < 1150)
                columns = 3;
            else
                columns = 4;


            int cardWidth = (availableWidth - (columns * margin * 2)) / columns;


            foreach (Control c in flpNotes.Controls)
            {
                if (c is Panel)
                {
                    c.Width = cardWidth;
                    c.Height = 155;
                    c.Margin = new Padding(margin);


                    Label description = c.Controls["lblNoteCardDescription"] as Label;

                    if (description != null)
                    {
                        description.Width = c.Width - 30;


                        if (availableWidth < 500)
                        {
                            // Small screen
                            description.Height = 30;
                            description.MaximumSize = new Size(c.Width - 30, 40);
                        }
                        else if (availableWidth < 850)
                        {
                            // Medium screen
                            description.Height = 40;
                            description.MaximumSize = new Size(c.Width - 30, 60);
                        }
                        else
                        {
                            // Large screen
                            description.Height = 50;
                            description.MaximumSize = new Size(c.Width - 30, 90);
                        }


                        description.AutoEllipsis = true;
                    }


                    SetRadius(c, 20);
                }
            }
        }


        private void pnlNoteHeader_Paint(object sender, PaintEventArgs e)
        {

        }
        

        private void SetRoundedPanel(Panel panel, int radius)
         {
            GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(panel.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(panel.Width - radius, panel.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, panel.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                panel.Region = new Region(path);
           }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoteViewDetailsControl noteViewDetailsControl = new NoteViewDetailsControl(this);
            noteViewDetailsControl.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoteEditDetailsControl noteEditDetailsControl = new NoteEditDetailsControl(this);
            noteEditDetailsControl.FormClosed += delegate
            {
                LoadNoteData(userID); 
            };
            noteEditDetailsControl.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Show confirmation message to user
            DialogResult dialogResult = MessageBox.Show(
                "Are you sure you want to delete this note: \"" + SelectedNoteTitle + "\"?",
                "Delete Note",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                // 2. Pass ID to UI object
                NoteUI noteUi = new NoteUI();
                noteUi.userId = userID;
                noteUi.noteId = SelectedNoteID;

                // 3. Call delete method
                CommonValidator.ValidationResult result = noteUi.DeleteNoteIntoNoteUi();

                if (result == CommonValidator.ValidationResult.Success)
                {
                    MessageBox.Show("Note deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNoteData(userID); // ✅ Immediately refresh note grid
                }
                else
                {
                    MessageBox.Show("Note deletion failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void SetRadius(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0)
                return;

            IntPtr hrgn = CreateRoundRectRgn(
                0,
                0,
                control.Width + 1,
                control.Height + 1,
                radius,
                radius);

            Region region = Region.FromHrgn(hrgn);

            if (control.Region != null)
                control.Region.Dispose();

            control.Region = region;

            DeleteObject(hrgn);
        }


        private void DesignContextMenu()
        {
            if (cmsNote == null)
            {
                    cmsNote = new ContextMenuStrip();
                    cmsNote.Items.AddRange(new ToolStripItem[] { 
                viewToolStripMenuItem, 
                editToolStripMenuItem, 
                deleteToolStripMenuItem 
                });
            }
            if (cmsFilter == null)
            {
                        cmsFilter = new ContextMenuStrip();
                        cmsFilter.Items.AddRange(new ToolStripItem[] { 
                    tsmiDate, 
                    tsmiPriority 
                });
            }
            // Context Menu
            cmsNote.ShowImageMargin = true;
            cmsNote.ShowCheckMargin = false;
            cmsNote.ImageScalingSize = new Size(10, 10);
            cmsNote.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
         

            // Menu Item Height
            viewToolStripMenuItem.AutoSize = false;
            viewToolStripMenuItem.Height = 30;

            editToolStripMenuItem.AutoSize = false;
            editToolStripMenuItem.Height = 30;

            deleteToolStripMenuItem.AutoSize = false;
            deleteToolStripMenuItem.Height = 30;

            

            // Images
            viewToolStripMenuItem.Image = Properties.Resources.open_eye;
            editToolStripMenuItem.Image = Properties.Resources.pen;
            deleteToolStripMenuItem.Image = Properties.Resources.trash;
            

            // Display Style
            viewToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            editToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            deleteToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            

            // Image Scaling
            viewToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            editToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            deleteToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;

            //filter cms
            cmsFilter.ShowImageMargin = true;
            cmsFilter.ShowCheckMargin = false;
            cmsFilter.ImageScalingSize = new Size(10, 10);

            tsmiDate.AutoSize = false;
            tsmiDate.Height = 30;

            tsmiPriority.AutoSize = false;
            tsmiPriority.Height = 30;

            tsmiDate.Image = Properties.Resources.calendar__1_;
            tsmiPriority.Image = Properties.Resources.shop;

            tsmiDate.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsmiPriority.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

            tsmiDate.ImageScaling = ToolStripItemImageScaling.None;
            tsmiPriority.ImageScaling = ToolStripItemImageScaling.None;
           
        }

        private void pnlNoteCard_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlImportant.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void pnlImportant_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlImportant.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void pnlThisMonth_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics,
                pnlImportant.ClientRectangle,
                ColorTranslator.FromHtml("#E7ECF3"),
                ButtonBorderStyle.Solid);
        }

        private void lblNoteCardDescription_Click(object sender, EventArgs e)
        {

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
            int totalPages = (int)Math.Ceiling((double)AllNoteData.Rows.Count / pageSize);

            if (currentPage < totalPages)
            {
                currentPage++;
                ShowCurrentPage();
            }
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)AllNoteData.Rows.Count / pageSize);
            if (currentPage != totalPages)
            {
                currentPage = totalPages;
                ShowCurrentPage();
            }
        }

        private void pnlNoteMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tsmiDate_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlDateFilter);
        }

        private void tsmiPriority_Click(object sender, EventArgs e)
        {
            ShowFilterPanel(pnlPriorityFilter);
            cmbPriority.DroppedDown = true;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            cmsFilter.Show(btnFilter, 0, btnFilter.Height);
        }

        private void btnDateClose_Click(object sender, EventArgs e)
        {
            HidePopupPanels();
            pnlDateFilter.Visible = false;
        }

        private void btnPriorityClose_Click(object sender, EventArgs e)
        {
            ignoreEvents = true;
            cmbPriority.SelectedIndex = 0;
            ignoreEvents = false;
            pnlPriorityFilter.Visible = false;
            LoadNoteData(userID);
        }

        private void HideAllFilterPanels()
        {
            HidePopupPanels();
            pnlDateFilter.Visible = false;
            pnlPriorityFilter.Visible = false;

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
                p.Y+5);

            panel.BringToFront();
            panel.Visible = true;
        }





        private void ShowCalenderFromDatePanel(Panel panel)
        {
            HidePopupPanels();
            panel.Parent = this;
            Point p = txtFromdate.PointToScreen(
                      new Point(0, txtFromdate.Height + 10));
            p = this.PointToClient(p);
            panel.Location = p;
            panel.BringToFront();
            panel.Visible = true;
        }
        private void ShowCalenderToDatePanel(Panel panel)
        {
            HidePopupPanels();

            panel.Parent = this;

            Point p = txtToDate.PointToScreen(
                new Point(0, txtToDate.Height + 10));

            p = this.PointToClient(p);

            panel.Location = p;

            panel.BringToFront();
            panel.Visible = true;
        }
        private void RegisterMouseDown(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.MouseDown += NoteControls_MouseDown;

                if (ctrl.HasChildren)
                    RegisterMouseDown(ctrl);
            }
        }
        private void NoteControls_MouseDown(object sender, MouseEventArgs e)
        {
            Point mousePos = this.PointToClient(Control.MousePosition);

            // From Date Calendar
            if (pnlFromDateCalenderShow.Visible)
            {
                bool clickInsideCalendar =
                    pnlFromDateCalenderShow.Bounds.Contains(mousePos);

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
                    pnlFromDateCalenderShow.Visible = false;
                }
            }

            // To Date Calendar
            if (pnlToDateCalenderShow.Visible)
            {
                bool clickInsideCalendar =
                    pnlToDateCalenderShow.Bounds.Contains(mousePos);

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
                    pnlToDateCalenderShow.Visible = false;
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

            pnlFromDateCalenderShow.Visible = false;
            pnlToDateCalenderShow.Visible = false;
            errorProvider1.Clear();
            ErrorHelper.HideErrorForControl(pnlFromDate);
            ErrorHelper.HideErrorForControl(pnlToDate);

            pnlDateFilter.Visible = false;

            ignoreEvents = false;
            LoadNoteData(userID);
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

            BLLayer.Note.NoteBLL noteBll = new BLLayer.Note.NoteBLL();
            noteBll.fromDate = this.fromDate;
            noteBll.toDate = this.toDate;

            CommonValidator.ValidationResult result = noteBll.DateValidatorIntoNoteBll();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    validFromDate = true;
                    if (!LoadFilteredNoteData(
                            "spFilterNoteByDateRange",
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

                        LoadNoteData(userID);
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
                    LoadNoteData(userID);
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

            BLLayer.Note.NoteBLL noteBll = new BLLayer.Note.NoteBLL();
            noteBll.fromDate = this.fromDate;
            noteBll.toDate = this.toDate;

            CommonValidator.ValidationResult result = noteBll.DateValidatorIntoNoteBll();

            switch (result)
            {
                case CommonValidator.ValidationResult.Success:
                    validFromDate = true;
                    if (!LoadFilteredNoteData(
                            "spFilterNoteByDateRange",
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

                        LoadNoteData(userID);
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
                    LoadNoteData(userID);
                    break;
            }
        }

        private void picCalenderFromDate_Click_1(object sender, EventArgs e)
        {
            if (pnlFromDateCalenderShow.Visible)
            {
                pnlFromDateCalenderShow.Visible = false;
            }
            else
            {
                pnlToDateCalenderShow.Visible = false;
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
                pnlFromDateCalenderShow.Visible = false;
                ShowCalenderToDatePanel(pnlToDateCalenderShow);
            }
        }



        private void btnNoteMore_MouseHover(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn != null && btn.Parent != null)
            {
                btn.BackColor = btn.Parent.BackColor;
            }
        }

        private void btnNoteMore_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn != null)
            {
                btn.BackColor = SystemColors.Control;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Hide panels
            HideAllFilterPanels();
            HidePopupPanels();

            // Reset page
            currentPage = 1;

            // Reload note data
            LoadNoteData(userID);

            // Refresh UI
            this.Refresh();
        }

        private void lblNoteTotal_Click(object sender, EventArgs e)
        {

        }

        private void lblNoteTotal_TextChanged(object sender, EventArgs e)
        {
            lblNoteTotal.Text = lblNoteTotalPageNumber.Text;
        }

        

       

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            AllNoteData = Common.CommonUiFunction.SearchDataInNote(masterData, txtSearch);
            currentPage = 1;
            ShowCurrentPage();
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

        private void monthCalendarToDate_DateSelected(object sender, DateRangeEventArgs e)
        {
            toDate = e.Start.Date;
            txtToDate.Text = e.Start.ToString("dd-MM-yyyy");
            pnlToDateCalenderShow.Visible = false;
        }

        private void monthCalendarFromDate_DateSelected(object sender, DateRangeEventArgs e)
        {
            fromDate = e.Start.Date;
            txtFromdate.Text = e.Start.ToString("dd-MM-yyyy");
            pnlFromDateCalenderShow.Visible = false;
        }

        private void txtFromdate_Click(object sender, EventArgs e)
        {
            if (pnlFromDateCalenderShow.Visible)
            {
                pnlFromDateCalenderShow.Visible = false;
            }
            else
            {
                pnlToDateCalenderShow.Visible = false;
                ShowCalenderFromDatePanel(pnlFromDateCalenderShow);
            }
        }

        private void txtToDate_Click(object sender, EventArgs e)
        {
            if (pnlToDateCalenderShow.Visible)
            {
                pnlToDateCalenderShow.Visible = false;
            }
            else
            {
                pnlFromDateCalenderShow.Visible = false;
                ShowCalenderToDatePanel(pnlToDateCalenderShow);
            }
        }

        private void pnlDateHeader_Click(object sender, EventArgs e)
        {
            HidePopupPanels();
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            HideAllFilterPanels();
            HidePopupPanels();
            ignoreEvents = true;
            txtFromdate.Clear();
            txtToDate.Clear();
            if (cmbPriority.Items.Count > 0) cmbPriority.SelectedIndex = 0;
            txtSearch.Clear();
            txtSearch_Leave(txtSearch, EventArgs.Empty);
            ignoreEvents = false;
            currentPage = 1;
            LoadNoteData(Session.LogedInUser.GetUserId());
            this.Refresh();
        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            if (AllNoteData == null || AllNoteData.Rows.Count == 0)
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
                saveDialog.Title = "Save Note Excel File";
                saveDialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                saveDialog.FileName =
                    "Note_" +
                    DateTime.Now.ToString("ddMMyyyy_HHmmss") +
                    ".xlsx";

                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;

                var progress = new ExportProgressHelper();
                progress.Show(this, "Exporting Note Data...");

                try
                {
                    progress.SetProgress(10);

                    ExportNoteToExcel(
                        AllNoteData,
                        saveDialog.FileName);

                    progress.SetProgress(100);
                }
                catch (Exception ex)
                {
                    progress.Close();
                    MessageBox.Show(
                        "Export failed.\n\n" + ex.Message,
                        "Export Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
        private void ExportNoteToExcel(DataTable dataTable, string filePath)
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
                worksheet.Name = "Note";

                // Only export these columns
                string[] exportColumns =
                                  {
                                   "NoteTitle",
                                    "CreatedAt",
                                    "Description",
                                    "NotePriorityName",
                                     "ColorName"
                                    };
       

                string[] exportHeaders =
                                  {
                                      "Title",
                                      "Date",
                                      "Description",
                                      "Priority",
                                      "Color"
                                  };

                // Header
                for (int col = 0; col < exportColumns.Length; col++)
                {
                    worksheet.Cells[1, col + 1] = exportHeaders[col];
                }

                // Data
                for (int row = 0; row < dataTable.Rows.Count; row++)
                {
                    for (int col = 0; col < exportColumns.Length; col++)
                    {
                        string columnName = exportColumns[col];

                        if (dataTable.Columns.Contains(columnName) &&
                            dataTable.Rows[row][columnName] != DBNull.Value)
                        {
                            string value =
                                dataTable.Rows[row][columnName].ToString();

                            // Date formatting
                            if (columnName == "CreatedAt")
                            {
                                DateTime date =
                                    Convert.ToDateTime(
                                        dataTable.Rows[row][columnName]);

                                value = date.ToString("dd MMM yyyy");
                            }

                            worksheet.Cells[row + 2, col + 1] = value;
                        }
                    }
                }

                // Header bold
                Excel.Range headerRange =
                    worksheet.Range[
                        worksheet.Cells[1, 1],
                        worksheet.Cells[1, exportColumns.Length]];

                headerRange.Font.Bold = true;

                // Auto fit
                worksheet.Columns.AutoFit();

                // Save
                workbook.SaveAs(
                    filePath,
                    Excel.XlFileFormat.xlOpenXMLWorkbook);

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

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search records...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search records...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        
    }
}
