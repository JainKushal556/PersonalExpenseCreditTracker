namespace PersonalExpenseCreditTracker.Modules.Note
{
    partial class NoteControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlNoteMain = new System.Windows.Forms.Panel();
            this.flpNotes = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlNoteCard = new System.Windows.Forms.Panel();
            this.pnlNoteCardFooter = new System.Windows.Forms.Panel();
            this.btnNoteMore = new System.Windows.Forms.Button();
            this.lblNoteCardDate = new System.Windows.Forms.Label();
            this.lblNoteCardDescription = new System.Windows.Forms.Label();
            this.lblNoteCardTitle = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.btnNextpage = new System.Windows.Forms.Button();
            this.btnCurrentPage = new System.Windows.Forms.Button();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.btnFirstpage = new System.Windows.Forms.Button();
            this.pnlNoteFooter = new System.Windows.Forms.Panel();
            this.lblentries = new System.Windows.Forms.Label();
            this.lblNoteTotalPageNumber = new System.Windows.Forms.Label();
            this.lblof = new System.Windows.Forms.Label();
            this.lblNoteEndingPageNumber = new System.Windows.Forms.Label();
            this.lblto = new System.Windows.Forms.Label();
            this.lblNoteStartingPageNumber = new System.Windows.Forms.Label();
            this.lblShowing = new System.Windows.Forms.Label();
            this.tblNoteSummary = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTotalNotes = new System.Windows.Forms.Panel();
            this.lblAllNote = new System.Windows.Forms.Label();
            this.lblNoteTotal = new System.Windows.Forms.Label();
            this.lblTotalTitle = new System.Windows.Forms.Label();
            this.PicNote = new System.Windows.Forms.PictureBox();
            this.pnlImportant = new System.Windows.Forms.Panel();
            this.lblNoteImportantDescription = new System.Windows.Forms.Label();
            this.lblNoteImportantNumber = new System.Windows.Forms.Label();
            this.lblNoteImportant = new System.Windows.Forms.Label();
            this.picNoteImportant = new System.Windows.Forms.PictureBox();
            this.pnlThisMonth = new System.Windows.Forms.Panel();
            this.lblMothDescription = new System.Windows.Forms.Label();
            this.lblMonthNoteNumber = new System.Windows.Forms.Label();
            this.lblMothTitle = new System.Windows.Forms.Label();
            this.picNoteCalender = new System.Windows.Forms.PictureBox();
            this.cmsNote = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlNoteMain.SuspendLayout();
            this.flpNotes.SuspendLayout();
            this.pnlNoteCard.SuspendLayout();
            this.pnlNoteCardFooter.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlControl.SuspendLayout();
            this.pnlNoteFooter.SuspendLayout();
            this.tblNoteSummary.SuspendLayout();
            this.pnlTotalNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicNote)).BeginInit();
            this.pnlImportant.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNoteImportant)).BeginInit();
            this.pnlThisMonth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNoteCalender)).BeginInit();
            this.cmsNote.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlNoteMain
            // 
            this.pnlNoteMain.AutoScroll = true;
            this.pnlNoteMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.pnlNoteMain.Controls.Add(this.flpNotes);
            this.pnlNoteMain.Controls.Add(this.pnlFooter);
            this.pnlNoteMain.Controls.Add(this.tblNoteSummary);
            this.pnlNoteMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNoteMain.Location = new System.Drawing.Point(0, 0);
            this.pnlNoteMain.Name = "pnlNoteMain";
            this.pnlNoteMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlNoteMain.Size = new System.Drawing.Size(1203, 630);
            this.pnlNoteMain.TabIndex = 0;
            // 
            // flpNotes
            // 
            this.flpNotes.AutoScroll = true;
            this.flpNotes.AutoSize = true;
            this.flpNotes.Controls.Add(this.pnlNoteCard);
            this.flpNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpNotes.Location = new System.Drawing.Point(20, 140);
            this.flpNotes.Name = "flpNotes";
            this.flpNotes.Padding = new System.Windows.Forms.Padding(8);
            this.flpNotes.Size = new System.Drawing.Size(1163, 417);
            this.flpNotes.TabIndex = 3;
            this.flpNotes.Paint += new System.Windows.Forms.PaintEventHandler(this.flpNotes_Paint);
            // 
            // pnlNoteCard
            // 
            this.pnlNoteCard.BackColor = System.Drawing.Color.SeaShell;
            this.pnlNoteCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNoteCard.Controls.Add(this.pnlNoteCardFooter);
            this.pnlNoteCard.Controls.Add(this.lblNoteCardDescription);
            this.pnlNoteCard.Controls.Add(this.lblNoteCardTitle);
            this.pnlNoteCard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlNoteCard.Location = new System.Drawing.Point(18, 18);
            this.pnlNoteCard.Margin = new System.Windows.Forms.Padding(10);
            this.pnlNoteCard.Name = "pnlNoteCard";
            this.pnlNoteCard.Padding = new System.Windows.Forms.Padding(10);
            this.pnlNoteCard.Size = new System.Drawing.Size(320, 193);
            this.pnlNoteCard.TabIndex = 0;
            // 
            // pnlNoteCardFooter
            // 
            this.pnlNoteCardFooter.Controls.Add(this.btnNoteMore);
            this.pnlNoteCardFooter.Controls.Add(this.lblNoteCardDate);
            this.pnlNoteCardFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNoteCardFooter.Location = new System.Drawing.Point(10, 141);
            this.pnlNoteCardFooter.Name = "pnlNoteCardFooter";
            this.pnlNoteCardFooter.Size = new System.Drawing.Size(298, 40);
            this.pnlNoteCardFooter.TabIndex = 2;
            // 
            // btnNoteMore
            // 
            this.btnNoteMore.BackColor = System.Drawing.Color.Transparent;
            this.btnNoteMore.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNoteMore.FlatAppearance.BorderSize = 0;
            this.btnNoteMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNoteMore.Image = global::PersonalExpenseCreditTracker.Properties.Resources.more2;
            this.btnNoteMore.Location = new System.Drawing.Point(268, 0);
            this.btnNoteMore.Name = "btnNoteMore";
            this.btnNoteMore.Size = new System.Drawing.Size(30, 40);
            this.btnNoteMore.TabIndex = 1;
            this.btnNoteMore.UseVisualStyleBackColor = false;
            this.btnNoteMore.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // lblNoteCardDate
            // 
            this.lblNoteCardDate.AutoSize = true;
            this.lblNoteCardDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoteCardDate.Location = new System.Drawing.Point(4, 10);
            this.lblNoteCardDate.Name = "lblNoteCardDate";
            this.lblNoteCardDate.Size = new System.Drawing.Size(82, 20);
            this.lblNoteCardDate.TabIndex = 0;
            this.lblNoteCardDate.Text = "12 Jul 2026";
            // 
            // lblNoteCardDescription
            // 
            this.lblNoteCardDescription.AutoEllipsis = true;
            this.lblNoteCardDescription.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoteCardDescription.Location = new System.Drawing.Point(18, 45);
            this.lblNoteCardDescription.MaximumSize = new System.Drawing.Size(270, 75);
            this.lblNoteCardDescription.Name = "lblNoteCardDescription";
            this.lblNoteCardDescription.Size = new System.Drawing.Size(270, 75);
            this.lblNoteCardDescription.TabIndex = 1;
            this.lblNoteCardDescription.Text = "Finish the project proposal, review team updates and prepare for tomorrow\'s meeti" +
                "ng.";
            // 
            // lblNoteCardTitle
            // 
            this.lblNoteCardTitle.AutoSize = true;
            this.lblNoteCardTitle.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoteCardTitle.Location = new System.Drawing.Point(10, 10);
            this.lblNoteCardTitle.Name = "lblNoteCardTitle";
            this.lblNoteCardTitle.Size = new System.Drawing.Size(113, 25);
            this.lblNoteCardTitle.TabIndex = 0;
            this.lblNoteCardTitle.Text = "Daily Notes";
            this.lblNoteCardTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.pnlFooter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFooter.Controls.Add(this.pnlControl);
            this.pnlFooter.Controls.Add(this.pnlNoteFooter);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(20, 557);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1163, 53);
            this.pnlFooter.TabIndex = 2;
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.btnLastPage);
            this.pnlControl.Controls.Add(this.btnNextpage);
            this.pnlControl.Controls.Add(this.btnCurrentPage);
            this.pnlControl.Controls.Add(this.btnPreviousPage);
            this.pnlControl.Controls.Add(this.btnFirstpage);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControl.Location = new System.Drawing.Point(911, 0);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(250, 51);
            this.pnlControl.TabIndex = 0;
            // 
            // btnLastPage
            // 
            this.btnLastPage.Image = global::PersonalExpenseCreditTracker.Properties.Resources.right;
            this.btnLastPage.Location = new System.Drawing.Point(198, 6);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(40, 40);
            this.btnLastPage.TabIndex = 4;
            this.btnLastPage.UseVisualStyleBackColor = true;
            // 
            // btnNextpage
            // 
            this.btnNextpage.Image = global::PersonalExpenseCreditTracker.Properties.Resources.next;
            this.btnNextpage.Location = new System.Drawing.Point(152, 6);
            this.btnNextpage.Name = "btnNextpage";
            this.btnNextpage.Size = new System.Drawing.Size(40, 40);
            this.btnNextpage.TabIndex = 3;
            this.btnNextpage.UseVisualStyleBackColor = true;
            // 
            // btnCurrentPage
            // 
            this.btnCurrentPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCurrentPage.Location = new System.Drawing.Point(106, 6);
            this.btnCurrentPage.Name = "btnCurrentPage";
            this.btnCurrentPage.Size = new System.Drawing.Size(40, 40);
            this.btnCurrentPage.TabIndex = 2;
            this.btnCurrentPage.Text = "1";
            this.btnCurrentPage.UseVisualStyleBackColor = true;
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Image = global::PersonalExpenseCreditTracker.Properties.Resources.preview;
            this.btnPreviousPage.Location = new System.Drawing.Point(59, 6);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(40, 40);
            this.btnPreviousPage.TabIndex = 1;
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            // 
            // btnFirstpage
            // 
            this.btnFirstpage.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirstpage.Image = global::PersonalExpenseCreditTracker.Properties.Resources.left;
            this.btnFirstpage.Location = new System.Drawing.Point(13, 6);
            this.btnFirstpage.Name = "btnFirstpage";
            this.btnFirstpage.Size = new System.Drawing.Size(40, 40);
            this.btnFirstpage.TabIndex = 0;
            this.btnFirstpage.UseVisualStyleBackColor = true;
            // 
            // pnlNoteFooter
            // 
            this.pnlNoteFooter.Controls.Add(this.lblentries);
            this.pnlNoteFooter.Controls.Add(this.lblNoteTotalPageNumber);
            this.pnlNoteFooter.Controls.Add(this.lblof);
            this.pnlNoteFooter.Controls.Add(this.lblNoteEndingPageNumber);
            this.pnlNoteFooter.Controls.Add(this.lblto);
            this.pnlNoteFooter.Controls.Add(this.lblNoteStartingPageNumber);
            this.pnlNoteFooter.Controls.Add(this.lblShowing);
            this.pnlNoteFooter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNoteFooter.Location = new System.Drawing.Point(0, 0);
            this.pnlNoteFooter.Name = "pnlNoteFooter";
            this.pnlNoteFooter.Size = new System.Drawing.Size(348, 51);
            this.pnlNoteFooter.TabIndex = 1;
            // 
            // lblentries
            // 
            this.lblentries.AutoSize = true;
            this.lblentries.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblentries.Location = new System.Drawing.Point(225, 15);
            this.lblentries.Name = "lblentries";
            this.lblentries.Size = new System.Drawing.Size(61, 23);
            this.lblentries.TabIndex = 6;
            this.lblentries.Text = "entries";
            // 
            // lblNoteTotalPageNumber
            // 
            this.lblNoteTotalPageNumber.AutoSize = true;
            this.lblNoteTotalPageNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoteTotalPageNumber.Location = new System.Drawing.Point(191, 16);
            this.lblNoteTotalPageNumber.Name = "lblNoteTotalPageNumber";
            this.lblNoteTotalPageNumber.Size = new System.Drawing.Size(28, 23);
            this.lblNoteTotalPageNumber.TabIndex = 5;
            this.lblNoteTotalPageNumber.Text = "10";
            // 
            // lblof
            // 
            this.lblof.AutoSize = true;
            this.lblof.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblof.Location = new System.Drawing.Point(158, 15);
            this.lblof.Name = "lblof";
            this.lblof.Size = new System.Drawing.Size(25, 23);
            this.lblof.TabIndex = 4;
            this.lblof.Text = "of";
            // 
            // lblNoteEndingPageNumber
            // 
            this.lblNoteEndingPageNumber.AutoSize = true;
            this.lblNoteEndingPageNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoteEndingPageNumber.Location = new System.Drawing.Point(128, 16);
            this.lblNoteEndingPageNumber.Name = "lblNoteEndingPageNumber";
            this.lblNoteEndingPageNumber.Size = new System.Drawing.Size(28, 23);
            this.lblNoteEndingPageNumber.TabIndex = 3;
            this.lblNoteEndingPageNumber.Text = "10";
            // 
            // lblto
            // 
            this.lblto.AutoSize = true;
            this.lblto.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblto.Location = new System.Drawing.Point(103, 15);
            this.lblto.Name = "lblto";
            this.lblto.Size = new System.Drawing.Size(26, 23);
            this.lblto.TabIndex = 2;
            this.lblto.Text = "to";
            // 
            // lblNoteStartingPageNumber
            // 
            this.lblNoteStartingPageNumber.AutoSize = true;
            this.lblNoteStartingPageNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoteStartingPageNumber.Location = new System.Drawing.Point(76, 15);
            this.lblNoteStartingPageNumber.Name = "lblNoteStartingPageNumber";
            this.lblNoteStartingPageNumber.Size = new System.Drawing.Size(19, 23);
            this.lblNoteStartingPageNumber.TabIndex = 1;
            this.lblNoteStartingPageNumber.Text = "1";
            // 
            // lblShowing
            // 
            this.lblShowing.AutoSize = true;
            this.lblShowing.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowing.Location = new System.Drawing.Point(3, 13);
            this.lblShowing.Name = "lblShowing";
            this.lblShowing.Size = new System.Drawing.Size(75, 23);
            this.lblShowing.TabIndex = 0;
            this.lblShowing.Text = "Showing";
            // 
            // tblNoteSummary
            // 
            this.tblNoteSummary.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblNoteSummary.ColumnCount = 3;
            this.tblNoteSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tblNoteSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tblNoteSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tblNoteSummary.Controls.Add(this.pnlTotalNotes, 0, 0);
            this.tblNoteSummary.Controls.Add(this.pnlImportant, 1, 0);
            this.tblNoteSummary.Controls.Add(this.pnlThisMonth, 2, 0);
            this.tblNoteSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblNoteSummary.Location = new System.Drawing.Point(20, 20);
            this.tblNoteSummary.Margin = new System.Windows.Forms.Padding(0);
            this.tblNoteSummary.MinimumSize = new System.Drawing.Size(830, 120);
            this.tblNoteSummary.Name = "tblNoteSummary";
            this.tblNoteSummary.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.tblNoteSummary.RowCount = 1;
            this.tblNoteSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblNoteSummary.Size = new System.Drawing.Size(1163, 120);
            this.tblNoteSummary.TabIndex = 1;
            // 
            // pnlTotalNotes
            // 
            this.pnlTotalNotes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(212)))), ((int)(((byte)(248)))));
            this.pnlTotalNotes.Controls.Add(this.lblAllNote);
            this.pnlTotalNotes.Controls.Add(this.lblNoteTotal);
            this.pnlTotalNotes.Controls.Add(this.lblTotalTitle);
            this.pnlTotalNotes.Controls.Add(this.PicNote);
            this.pnlTotalNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTotalNotes.Location = new System.Drawing.Point(20, 10);
            this.pnlTotalNotes.Margin = new System.Windows.Forms.Padding(10);
            this.pnlTotalNotes.MinimumSize = new System.Drawing.Size(250, 100);
            this.pnlTotalNotes.Name = "pnlTotalNotes";
            this.pnlTotalNotes.Size = new System.Drawing.Size(357, 100);
            this.pnlTotalNotes.TabIndex = 0;
            this.pnlTotalNotes.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTotalNotes_Paint);
            // 
            // lblAllNote
            // 
            this.lblAllNote.AutoSize = true;
            this.lblAllNote.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAllNote.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblAllNote.Location = new System.Drawing.Point(78, 70);
            this.lblAllNote.Name = "lblAllNote";
            this.lblAllNote.Size = new System.Drawing.Size(70, 20);
            this.lblAllNote.TabIndex = 3;
            this.lblAllNote.Text = "All Notes";
            // 
            // lblNoteTotal
            // 
            this.lblNoteTotal.AutoSize = true;
            this.lblNoteTotal.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoteTotal.Location = new System.Drawing.Point(78, 35);
            this.lblNoteTotal.Name = "lblNoteTotal";
            this.lblNoteTotal.Size = new System.Drawing.Size(43, 32);
            this.lblNoteTotal.TabIndex = 2;
            this.lblNoteTotal.Text = "21";
            // 
            // lblTotalTitle
            // 
            this.lblTotalTitle.AutoSize = true;
            this.lblTotalTitle.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTitle.Location = new System.Drawing.Point(74, 12);
            this.lblTotalTitle.Name = "lblTotalTitle";
            this.lblTotalTitle.Size = new System.Drawing.Size(100, 23);
            this.lblTotalTitle.TabIndex = 1;
            this.lblTotalTitle.Text = "Total Notes";
            // 
            // PicNote
            // 
            this.PicNote.Image = global::PersonalExpenseCreditTracker.Properties.Resources.note2;
            this.PicNote.Location = new System.Drawing.Point(16, 26);
            this.PicNote.Name = "PicNote";
            this.PicNote.Size = new System.Drawing.Size(38, 38);
            this.PicNote.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicNote.TabIndex = 0;
            this.PicNote.TabStop = false;
            // 
            // pnlImportant
            // 
            this.pnlImportant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(228)))), ((int)(((byte)(198)))));
            this.pnlImportant.Controls.Add(this.lblNoteImportantDescription);
            this.pnlImportant.Controls.Add(this.lblNoteImportantNumber);
            this.pnlImportant.Controls.Add(this.lblNoteImportant);
            this.pnlImportant.Controls.Add(this.picNoteImportant);
            this.pnlImportant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImportant.Location = new System.Drawing.Point(407, 10);
            this.pnlImportant.Margin = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.pnlImportant.MinimumSize = new System.Drawing.Size(250, 100);
            this.pnlImportant.Name = "pnlImportant";
            this.pnlImportant.Size = new System.Drawing.Size(358, 100);
            this.pnlImportant.TabIndex = 1;
            // 
            // lblNoteImportantDescription
            // 
            this.lblNoteImportantDescription.AutoSize = true;
            this.lblNoteImportantDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoteImportantDescription.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblNoteImportantDescription.Location = new System.Drawing.Point(78, 70);
            this.lblNoteImportantDescription.Name = "lblNoteImportantDescription";
            this.lblNoteImportantDescription.Size = new System.Drawing.Size(97, 20);
            this.lblNoteImportantDescription.TabIndex = 3;
            this.lblNoteImportantDescription.Text = "Pinned Notes";
            // 
            // lblNoteImportantNumber
            // 
            this.lblNoteImportantNumber.AutoSize = true;
            this.lblNoteImportantNumber.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoteImportantNumber.Location = new System.Drawing.Point(78, 35);
            this.lblNoteImportantNumber.Name = "lblNoteImportantNumber";
            this.lblNoteImportantNumber.Size = new System.Drawing.Size(29, 32);
            this.lblNoteImportantNumber.TabIndex = 2;
            this.lblNoteImportantNumber.Text = "8";
            // 
            // lblNoteImportant
            // 
            this.lblNoteImportant.AutoSize = true;
            this.lblNoteImportant.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoteImportant.Location = new System.Drawing.Point(74, 12);
            this.lblNoteImportant.Name = "lblNoteImportant";
            this.lblNoteImportant.Size = new System.Drawing.Size(92, 23);
            this.lblNoteImportant.TabIndex = 1;
            this.lblNoteImportant.Text = "Important";
            // 
            // picNoteImportant
            // 
            this.picNoteImportant.Image = global::PersonalExpenseCreditTracker.Properties.Resources.star;
            this.picNoteImportant.Location = new System.Drawing.Point(13, 28);
            this.picNoteImportant.Name = "picNoteImportant";
            this.picNoteImportant.Size = new System.Drawing.Size(38, 38);
            this.picNoteImportant.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picNoteImportant.TabIndex = 0;
            this.picNoteImportant.TabStop = false;
            this.picNoteImportant.Click += new System.EventHandler(this.picNoteImportant_Click);
            // 
            // pnlThisMonth
            // 
            this.pnlThisMonth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(211)))), ((int)(((byte)(221)))));
            this.pnlThisMonth.Controls.Add(this.lblMothDescription);
            this.pnlThisMonth.Controls.Add(this.lblMonthNoteNumber);
            this.pnlThisMonth.Controls.Add(this.lblMothTitle);
            this.pnlThisMonth.Controls.Add(this.picNoteCalender);
            this.pnlThisMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlThisMonth.Location = new System.Drawing.Point(795, 10);
            this.pnlThisMonth.Margin = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.pnlThisMonth.MinimumSize = new System.Drawing.Size(250, 100);
            this.pnlThisMonth.Name = "pnlThisMonth";
            this.pnlThisMonth.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.pnlThisMonth.Size = new System.Drawing.Size(348, 100);
            this.pnlThisMonth.TabIndex = 2;
            // 
            // lblMothDescription
            // 
            this.lblMothDescription.AutoSize = true;
            this.lblMothDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMothDescription.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblMothDescription.Location = new System.Drawing.Point(78, 70);
            this.lblMothDescription.Name = "lblMothDescription";
            this.lblMothDescription.Size = new System.Drawing.Size(97, 20);
            this.lblMothDescription.TabIndex = 3;
            this.lblMothDescription.Text = "Notes Added";
            // 
            // lblMonthNoteNumber
            // 
            this.lblMonthNoteNumber.AutoSize = true;
            this.lblMonthNoteNumber.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonthNoteNumber.Location = new System.Drawing.Point(78, 35);
            this.lblMonthNoteNumber.Name = "lblMonthNoteNumber";
            this.lblMonthNoteNumber.Size = new System.Drawing.Size(43, 32);
            this.lblMonthNoteNumber.TabIndex = 2;
            this.lblMonthNoteNumber.Text = "12";
            // 
            // lblMothTitle
            // 
            this.lblMothTitle.AutoSize = true;
            this.lblMothTitle.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMothTitle.Location = new System.Drawing.Point(74, 12);
            this.lblMothTitle.Name = "lblMothTitle";
            this.lblMothTitle.Size = new System.Drawing.Size(100, 23);
            this.lblMothTitle.TabIndex = 1;
            this.lblMothTitle.Text = "This Month";
            // 
            // picNoteCalender
            // 
            this.picNoteCalender.Image = global::PersonalExpenseCreditTracker.Properties.Resources.NoteCalender;
            this.picNoteCalender.Location = new System.Drawing.Point(14, 26);
            this.picNoteCalender.Name = "picNoteCalender";
            this.picNoteCalender.Size = new System.Drawing.Size(38, 38);
            this.picNoteCalender.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picNoteCalender.TabIndex = 0;
            this.picNoteCalender.TabStop = false;
            // 
            // cmsNote
            // 
            this.cmsNote.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.cmsNote.Name = "cmsNote";
            this.cmsNote.Size = new System.Drawing.Size(123, 76);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // NoteControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 630);
            this.Controls.Add(this.pnlNoteMain);
            this.Name = "NoteControl";
            this.Text = "NoteControl";
            this.Load += new System.EventHandler(this.NoteControl_Load);
            this.pnlNoteMain.ResumeLayout(false);
            this.pnlNoteMain.PerformLayout();
            this.flpNotes.ResumeLayout(false);
            this.pnlNoteCard.ResumeLayout(false);
            this.pnlNoteCard.PerformLayout();
            this.pnlNoteCardFooter.ResumeLayout(false);
            this.pnlNoteCardFooter.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlControl.ResumeLayout(false);
            this.pnlNoteFooter.ResumeLayout(false);
            this.pnlNoteFooter.PerformLayout();
            this.tblNoteSummary.ResumeLayout(false);
            this.pnlTotalNotes.ResumeLayout(false);
            this.pnlTotalNotes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicNote)).EndInit();
            this.pnlImportant.ResumeLayout(false);
            this.pnlImportant.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNoteImportant)).EndInit();
            this.pnlThisMonth.ResumeLayout(false);
            this.pnlThisMonth.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNoteCalender)).EndInit();
            this.cmsNote.ResumeLayout(false);
            this.ResumeLayout(false);

        }

       

        #endregion

        private System.Windows.Forms.Panel pnlNoteMain;
        private System.Windows.Forms.TableLayoutPanel tblNoteSummary;
        private System.Windows.Forms.Panel pnlTotalNotes;
        private System.Windows.Forms.Panel pnlImportant;
        private System.Windows.Forms.Panel pnlThisMonth;
        private System.Windows.Forms.PictureBox PicNote;
        private System.Windows.Forms.Label lblAllNote;
        private System.Windows.Forms.Label lblNoteTotal;
        private System.Windows.Forms.Label lblTotalTitle;
        private System.Windows.Forms.PictureBox picNoteImportant;
        private System.Windows.Forms.Label lblNoteImportantDescription;
        private System.Windows.Forms.Label lblNoteImportantNumber;
        private System.Windows.Forms.Label lblNoteImportant;
        private System.Windows.Forms.PictureBox picNoteCalender;
        private System.Windows.Forms.FlowLayoutPanel flpNotes;
        private System.Windows.Forms.Label lblMothDescription;
        private System.Windows.Forms.Label lblMonthNoteNumber;
        private System.Windows.Forms.Label lblMothTitle;
        private System.Windows.Forms.Panel pnlNoteCard;
        private System.Windows.Forms.Label lblNoteCardTitle;
        private System.Windows.Forms.Panel pnlNoteCardFooter;
        private System.Windows.Forms.Button btnNoteMore;
        private System.Windows.Forms.Label lblNoteCardDate;
        private System.Windows.Forms.Label lblNoteCardDescription;
        private System.Windows.Forms.ContextMenuStrip cmsNote;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlNoteFooter;
        private System.Windows.Forms.Label lblentries;
        private System.Windows.Forms.Label lblNoteTotalPageNumber;
        private System.Windows.Forms.Label lblof;
        private System.Windows.Forms.Label lblNoteEndingPageNumber;
        private System.Windows.Forms.Label lblto;
        private System.Windows.Forms.Label lblNoteStartingPageNumber;
        private System.Windows.Forms.Label lblShowing;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Button btnNextpage;
        private System.Windows.Forms.Button btnCurrentPage;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.Button btnFirstpage;
        //private System.Windows.Forms.ContextMenuStrip cmsnote;
    }
}