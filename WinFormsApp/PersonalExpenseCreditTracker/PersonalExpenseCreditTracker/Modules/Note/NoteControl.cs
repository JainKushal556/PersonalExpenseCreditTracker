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
using PersonalExpenseCreditTracker.Forms.Main;

namespace PersonalExpenseCreditTracker.Modules.Note
{
    public partial class NoteControl : Form
    {
        int userID = Session.LogedInUser.GetUserId();
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

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);
        public NoteControl()
        {
            InitializeComponent();
            Resize += NoteControl_Resize;
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
            DesignContextMenu();
            ResizeNoteCards();
            SetRoundedPanel(pnlTotalNotes, 20);
            SetRoundedPanel(pnlImportant, 20);
            SetRoundedPanel(pnlThisMonth, 20);
            //int userID = Session.LogedInUser.GetUserId();
            HideAllFilterPanels();
            DesignContextMenu();
            this.MouseDown += NoteControls_MouseDown;
            RegisterMouseDown(this);

            LoadNoteData(userID);
            cmsFilter.Opening += cmsFilter_Opening;

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
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllNotes", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    AllNoteData.Clear();
                    da.Fill(AllNoteData);
                }

                currentPage = 1;
                ShowCurrentPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            SetRadius(card, 20);


            flpNotes.Controls.Add(card);
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
            UpdatePageSize();
            flpNotes.SuspendLayout();
            flpNotes.Controls.Clear();

            int start = (currentPage - 1) * pageSize;
            int end = Math.Min(start + pageSize, AllNoteData.Rows.Count);

            for (int i = start; i < end; i++)
            {
                AddNoteCard(AllNoteData.Rows[i]);
            }

            flpNotes.ResumeLayout();

            ResizeNoteCards();

            lblNoteStartingPageNumber.Text = (AllNoteData.Rows.Count == 0) ? "0" : (start + 1).ToString();
            lblNoteEndingPageNumber.Text = end.ToString();
            lblNoteTotalPageNumber.Text = AllNoteData.Rows.Count.ToString();

            int totalPages = (int)Math.Ceiling(AllNoteData.Rows.Count / (double)pageSize);

            btnCurrentPage.Text = currentPage.ToString();

            btnFirstpage.Enabled = currentPage > 1;
            btnPreviousPage.Enabled = currentPage > 1;

            btnNextpage.Enabled = currentPage < totalPages;
            btnLastPage.Enabled = currentPage < totalPages;
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

            ShowCurrentPage();
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


                        // Screen size অনুযায়ী description line
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
            NoteViewDetailsControl noteViewDetailsControl = new NoteViewDetailsControl();
            noteViewDetailsControl.Show();


        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoteEditDetailsControl noteEditDetailsControl = new NoteEditDetailsControl();
            noteEditDetailsControl.Show();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            pnlPriorityFilter.Visible = false;
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
                p.Y);

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
            HidePopupPanels();
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

        private void cmsNote_Opening(object sender, CancelEventArgs e)
        {
            viewToolStripMenuItem.AutoSize = false;
            editToolStripMenuItem.AutoSize = false;
            deleteToolStripMenuItem.AutoSize = false;

            viewToolStripMenuItem.Width = cmsNote.Width;
            editToolStripMenuItem.Width = cmsNote.Width;
            deleteToolStripMenuItem.Width = cmsNote.Width;
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
            ShowCurrentPage();
        }

        private void monthCalendarToDate_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtToDate.Text = e.Start.ToString("dd-MM-yyyy");
            pnlToDateCalenderShow.Visible = false;
        }

        private void txtFromdate_Enter(object sender, EventArgs e)
        {
            pnlToDateCalenderShow.Visible = false;
            ShowCalenderFromDatePanel(pnlFromDateCalenderShow);
        }

        private void txtToDate_Enter(object sender, EventArgs e)
        {
            pnlFromDateCalenderShow.Visible = false;
            ShowCalenderToDatePanel(pnlToDateCalenderShow);
        }

        private void monthCalendarFromDate_DateSelected(object sender, DateRangeEventArgs e)
        {
            txtFromdate.Text = e.Start.ToString("dd-MM-yyyy");
            pnlFromDateCalenderShow.Visible = false;
        }

    }
}