namespace PersonalExpenseCreditTracker.Modules.Credit
{
    partial class CreditControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlCreditContent = new System.Windows.Forms.Panel();
            this.tblTable = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTableHeader = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.dgvCreditDataTable = new System.Windows.Forms.DataGridView();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pnlCreditFooter = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCreditTotalPageNumber = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCreditEndingPageNumber = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCreditStartingPageNumber = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.btnNextpage = new System.Windows.Forms.Button();
            this.btnCurrentPage = new System.Windows.Forms.Button();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.btnFirstpage = new System.Windows.Forms.Button();
            this.tblCreditSummary = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTotalCredit = new System.Windows.Forms.Panel();
            this.lblCreditAmount = new System.Windows.Forms.Label();
            this.picCredit = new System.Windows.Forms.PictureBox();
            this.lblTotalCredit = new System.Windows.Forms.Label();
            this.pnlTransactionCard = new System.Windows.Forms.Panel();
            this.lblTransactionAmount = new System.Windows.Forms.Label();
            this.lblTransction = new System.Windows.Forms.Label();
            this.picTransaction = new System.Windows.Forms.PictureBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.pnlCreditContent.SuspendLayout();
            this.tblTable.SuspendLayout();
            this.pnlTableHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCreditDataTable)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.pnlCreditFooter.SuspendLayout();
            this.pnlControl.SuspendLayout();
            this.tblCreditSummary.SuspendLayout();
            this.pnlTotalCredit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCredit)).BeginInit();
            this.pnlTransactionCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTransaction)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCreditContent
            // 
            this.pnlCreditContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(253)))));
            this.pnlCreditContent.Controls.Add(this.tblTable);
            this.pnlCreditContent.Controls.Add(this.pnlFooter);
            this.pnlCreditContent.Controls.Add(this.tblCreditSummary);
            this.pnlCreditContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCreditContent.Location = new System.Drawing.Point(0, 0);
            this.pnlCreditContent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlCreditContent.Name = "pnlCreditContent";
            this.pnlCreditContent.Padding = new System.Windows.Forms.Padding(3);
            this.pnlCreditContent.Size = new System.Drawing.Size(1250, 753);
            this.pnlCreditContent.TabIndex = 0;
            // 
            // tblTable
            // 
            this.tblTable.ColumnCount = 1;
            this.tblTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTable.Controls.Add(this.pnlTableHeader, 0, 0);
            this.tblTable.Controls.Add(this.dgvCreditDataTable, 0, 1);
            this.tblTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTable.Location = new System.Drawing.Point(3, 113);
            this.tblTable.Name = "tblTable";
            this.tblTable.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.tblTable.RowCount = 2;
            this.tblTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTable.Size = new System.Drawing.Size(1244, 586);
            this.tblTable.TabIndex = 3;
            // 
            // pnlTableHeader
            // 
            this.pnlTableHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlTableHeader.Controls.Add(this.panel1);
            this.pnlTableHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTableHeader.Location = new System.Drawing.Point(11, 8);
            this.pnlTableHeader.Name = "pnlTableHeader";
            this.pnlTableHeader.Size = new System.Drawing.Size(1222, 44);
            this.pnlTableHeader.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(881, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(341, 44);
            this.panel1.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.AutoSize = true;
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(127)))), ((int)(((byte)(242)))));
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(91)))), ((int)(((byte)(176)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(103)))), ((int)(((byte)(199)))));
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(98)))), ((int)(((byte)(180)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Image = global::PersonalExpenseCreditTracker.Properties.Resources.refresh;
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(109, 44);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(127)))), ((int)(((byte)(242)))));
            this.btnExport.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(91)))), ((int)(((byte)(176)))));
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(103)))), ((int)(((byte)(199)))));
            this.btnExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Image = global::PersonalExpenseCreditTracker.Properties.Resources.share;
            this.btnExport.Location = new System.Drawing.Point(232, 0);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(109, 44);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExport.UseVisualStyleBackColor = false;
            // 
            // dgvCreditDataTable
            // 
            this.dgvCreditDataTable.AllowUserToAddRows = false;
            this.dgvCreditDataTable.AllowUserToDeleteRows = false;
            this.dgvCreditDataTable.AllowUserToResizeColumns = false;
            this.dgvCreditDataTable.AllowUserToResizeRows = false;
            this.dgvCreditDataTable.BackgroundColor = System.Drawing.Color.White;
            this.dgvCreditDataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCreditDataTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDate,
            this.colDescription,
            this.colCategory,
            this.colSubCategory,
            this.colAmount,
            this.colPaymentMethod});
            this.dgvCreditDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCreditDataTable.EnableHeadersVisualStyles = false;
            this.dgvCreditDataTable.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvCreditDataTable.Location = new System.Drawing.Point(11, 58);
            this.dgvCreditDataTable.Name = "dgvCreditDataTable";
            this.dgvCreditDataTable.ReadOnly = true;
            this.dgvCreditDataTable.RowHeadersVisible = false;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.dgvCreditDataTable.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvCreditDataTable.RowTemplate.Height = 24;
            this.dgvCreditDataTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCreditDataTable.Size = new System.Drawing.Size(1222, 520);
            this.dgvCreditDataTable.TabIndex = 1;
            this.dgvCreditDataTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCreditDataTable_CellContentClick);
            // 
            // colDate
            // 
            this.colDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDate.FillWeight = 10F;
            this.colDate.HeaderText = "Date";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            // 
            // colDescription
            // 
            this.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDescription.FillWeight = 15F;
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            // 
            // colCategory
            // 
            this.colCategory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCategory.FillWeight = 15F;
            this.colCategory.HeaderText = "Category";
            this.colCategory.Name = "colCategory";
            this.colCategory.ReadOnly = true;
            // 
            // colSubCategory
            // 
            this.colSubCategory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSubCategory.FillWeight = 15F;
            this.colSubCategory.HeaderText = "SubCategory";
            this.colSubCategory.Name = "colSubCategory";
            this.colSubCategory.ReadOnly = true;
            // 
            // colAmount
            // 
            this.colAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAmount.FillWeight = 10F;
            this.colAmount.HeaderText = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            // 
            // colPaymentMethod
            // 
            this.colPaymentMethod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colPaymentMethod.FillWeight = 15F;
            this.colPaymentMethod.HeaderText = "Payment Method";
            this.colPaymentMethod.Name = "colPaymentMethod";
            this.colPaymentMethod.ReadOnly = true;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.pnlCreditFooter);
            this.pnlFooter.Controls.Add(this.pnlControl);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(3, 699);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1244, 51);
            this.pnlFooter.TabIndex = 2;
            // 
            // pnlCreditFooter
            // 
            this.pnlCreditFooter.Controls.Add(this.label7);
            this.pnlCreditFooter.Controls.Add(this.lblCreditTotalPageNumber);
            this.pnlCreditFooter.Controls.Add(this.label5);
            this.pnlCreditFooter.Controls.Add(this.lblCreditEndingPageNumber);
            this.pnlCreditFooter.Controls.Add(this.label3);
            this.pnlCreditFooter.Controls.Add(this.lblCreditStartingPageNumber);
            this.pnlCreditFooter.Controls.Add(this.label1);
            this.pnlCreditFooter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlCreditFooter.Location = new System.Drawing.Point(0, 0);
            this.pnlCreditFooter.Name = "pnlCreditFooter";
            this.pnlCreditFooter.Size = new System.Drawing.Size(348, 51);
            this.pnlCreditFooter.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(215, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 23);
            this.label7.TabIndex = 6;
            this.label7.Text = "entries";
            // 
            // lblCreditTotalPageNumber
            // 
            this.lblCreditTotalPageNumber.AutoSize = true;
            this.lblCreditTotalPageNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditTotalPageNumber.Location = new System.Drawing.Point(186, 12);
            this.lblCreditTotalPageNumber.Name = "lblCreditTotalPageNumber";
            this.lblCreditTotalPageNumber.Size = new System.Drawing.Size(28, 23);
            this.lblCreditTotalPageNumber.TabIndex = 5;
            this.lblCreditTotalPageNumber.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(160, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "of";
            // 
            // lblCreditEndingPageNumber
            // 
            this.lblCreditEndingPageNumber.AutoSize = true;
            this.lblCreditEndingPageNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditEndingPageNumber.Location = new System.Drawing.Point(129, 12);
            this.lblCreditEndingPageNumber.Name = "lblCreditEndingPageNumber";
            this.lblCreditEndingPageNumber.Size = new System.Drawing.Size(28, 23);
            this.lblCreditEndingPageNumber.TabIndex = 3;
            this.lblCreditEndingPageNumber.Text = "10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(101, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "to";
            // 
            // lblCreditStartingPageNumber
            // 
            this.lblCreditStartingPageNumber.AutoSize = true;
            this.lblCreditStartingPageNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditStartingPageNumber.Location = new System.Drawing.Point(76, 12);
            this.lblCreditStartingPageNumber.Name = "lblCreditStartingPageNumber";
            this.lblCreditStartingPageNumber.Size = new System.Drawing.Size(19, 23);
            this.lblCreditStartingPageNumber.TabIndex = 1;
            this.lblCreditStartingPageNumber.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Showing";
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.btnLastPage);
            this.pnlControl.Controls.Add(this.btnNextpage);
            this.pnlControl.Controls.Add(this.btnCurrentPage);
            this.pnlControl.Controls.Add(this.btnPreviousPage);
            this.pnlControl.Controls.Add(this.btnFirstpage);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControl.Location = new System.Drawing.Point(994, 0);
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
            this.btnLastPage.Click += new System.EventHandler(this.btnLastPage_Click);
            // 
            // btnNextpage
            // 
            this.btnNextpage.Image = global::PersonalExpenseCreditTracker.Properties.Resources.next;
            this.btnNextpage.Location = new System.Drawing.Point(152, 6);
            this.btnNextpage.Name = "btnNextpage";
            this.btnNextpage.Size = new System.Drawing.Size(40, 40);
            this.btnNextpage.TabIndex = 3;
            this.btnNextpage.UseVisualStyleBackColor = true;
            this.btnNextpage.Click += new System.EventHandler(this.btnNextpage_Click);
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
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
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
            this.btnFirstpage.Click += new System.EventHandler(this.btnFirstpage_Click);
            // 
            // tblCreditSummary
            // 
            this.tblCreditSummary.BackColor = System.Drawing.Color.Transparent;
            this.tblCreditSummary.ColumnCount = 2;
            this.tblCreditSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblCreditSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblCreditSummary.Controls.Add(this.pnlTotalCredit, 0, 0);
            this.tblCreditSummary.Controls.Add(this.pnlTransactionCard, 1, 0);
            this.tblCreditSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblCreditSummary.Location = new System.Drawing.Point(3, 3);
            this.tblCreditSummary.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tblCreditSummary.Name = "tblCreditSummary";
            this.tblCreditSummary.RowCount = 1;
            this.tblCreditSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblCreditSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblCreditSummary.Size = new System.Drawing.Size(1244, 110);
            this.tblCreditSummary.TabIndex = 0;
            // 
            // pnlTotalCredit
            // 
            this.pnlTotalCredit.BackColor = System.Drawing.Color.White;
            this.pnlTotalCredit.Controls.Add(this.lblCreditAmount);
            this.pnlTotalCredit.Controls.Add(this.picCredit);
            this.pnlTotalCredit.Controls.Add(this.lblTotalCredit);
            this.pnlTotalCredit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTotalCredit.Location = new System.Drawing.Point(11, 10);
            this.pnlTotalCredit.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.pnlTotalCredit.Name = "pnlTotalCredit";
            this.pnlTotalCredit.Padding = new System.Windows.Forms.Padding(20);
            this.pnlTotalCredit.Size = new System.Drawing.Size(600, 90);
            this.pnlTotalCredit.TabIndex = 0;
            this.pnlTotalCredit.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTotalCredit_Paint);
            // 
            // lblCreditAmount
            // 
            this.lblCreditAmount.AutoSize = true;
            this.lblCreditAmount.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditAmount.ForeColor = System.Drawing.Color.Black;
            this.lblCreditAmount.Location = new System.Drawing.Point(85, 42);
            this.lblCreditAmount.Name = "lblCreditAmount";
            this.lblCreditAmount.Size = new System.Drawing.Size(99, 32);
            this.lblCreditAmount.TabIndex = 2;
            this.lblCreditAmount.Text = "₹36750";
            // 
            // picCredit
            // 
            this.picCredit.Image = global::PersonalExpenseCreditTracker.Properties.Resources.spending;
            this.picCredit.Location = new System.Drawing.Point(20, 22);
            this.picCredit.Name = "picCredit";
            this.picCredit.Size = new System.Drawing.Size(45, 45);
            this.picCredit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCredit.TabIndex = 0;
            this.picCredit.TabStop = false;
            this.picCredit.Click += new System.EventHandler(this.picCredit_Click);
            // 
            // lblTotalCredit
            // 
            this.lblTotalCredit.AutoSize = true;
            this.lblTotalCredit.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCredit.ForeColor = System.Drawing.Color.Black;
            this.lblTotalCredit.Location = new System.Drawing.Point(85, 12);
            this.lblTotalCredit.Name = "lblTotalCredit";
            this.lblTotalCredit.Size = new System.Drawing.Size(104, 23);
            this.lblTotalCredit.TabIndex = 1;
            this.lblTotalCredit.Text = "Total Credit";
            // 
            // pnlTransactionCard
            // 
            this.pnlTransactionCard.BackColor = System.Drawing.Color.White;
            this.pnlTransactionCard.Controls.Add(this.lblTransactionAmount);
            this.pnlTransactionCard.Controls.Add(this.lblTransction);
            this.pnlTransactionCard.Controls.Add(this.picTransaction);
            this.pnlTransactionCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTransactionCard.Location = new System.Drawing.Point(633, 10);
            this.pnlTransactionCard.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.pnlTransactionCard.Name = "pnlTransactionCard";
            this.pnlTransactionCard.Padding = new System.Windows.Forms.Padding(20);
            this.pnlTransactionCard.Size = new System.Drawing.Size(600, 90);
            this.pnlTransactionCard.TabIndex = 1;
            this.pnlTransactionCard.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTransactionCard_Paint);
            // 
            // lblTransactionAmount
            // 
            this.lblTransactionAmount.AutoSize = true;
            this.lblTransactionAmount.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransactionAmount.ForeColor = System.Drawing.Color.Black;
            this.lblTransactionAmount.Location = new System.Drawing.Point(85, 42);
            this.lblTransactionAmount.Name = "lblTransactionAmount";
            this.lblTransactionAmount.Size = new System.Drawing.Size(57, 32);
            this.lblTransactionAmount.TabIndex = 2;
            this.lblTransactionAmount.Text = "₹28";
            // 
            // lblTransction
            // 
            this.lblTransction.AutoSize = true;
            this.lblTransction.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransction.ForeColor = System.Drawing.Color.Black;
            this.lblTransction.Location = new System.Drawing.Point(85, 12);
            this.lblTransction.Name = "lblTransction";
            this.lblTransction.Size = new System.Drawing.Size(152, 23);
            this.lblTransction.TabIndex = 1;
            this.lblTransction.Text = "Total Transactions";
            // 
            // picTransaction
            // 
            this.picTransaction.Image = global::PersonalExpenseCreditTracker.Properties.Resources.transaction;
            this.picTransaction.Location = new System.Drawing.Point(20, 22);
            this.picTransaction.Name = "picTransaction";
            this.picTransaction.Size = new System.Drawing.Size(45, 45);
            this.picTransaction.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTransaction.TabIndex = 0;
            this.picTransaction.TabStop = false;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(127)))), ((int)(((byte)(242)))));
            this.btnPrint.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(91)))), ((int)(((byte)(176)))));
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(103)))), ((int)(((byte)(199)))));
            this.btnPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Image = global::PersonalExpenseCreditTracker.Properties.Resources.share;
            this.btnPrint.Location = new System.Drawing.Point(122, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(99, 44);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "Print";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.UseVisualStyleBackColor = false;
            // 
            // CreditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 753);
            this.Controls.Add(this.pnlCreditContent);
            this.Name = "CreditControl";
            this.Text = "CreditControl";
            this.Load += new System.EventHandler(this.CreditControl_Load);
            this.pnlCreditContent.ResumeLayout(false);
            this.tblTable.ResumeLayout(false);
            this.pnlTableHeader.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCreditDataTable)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlCreditFooter.ResumeLayout(false);
            this.pnlCreditFooter.PerformLayout();
            this.pnlControl.ResumeLayout(false);
            this.tblCreditSummary.ResumeLayout(false);
            this.pnlTotalCredit.ResumeLayout(false);
            this.pnlTotalCredit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCredit)).EndInit();
            this.pnlTransactionCard.ResumeLayout(false);
            this.pnlTransactionCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTransaction)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCreditContent;
        private System.Windows.Forms.TableLayoutPanel tblCreditSummary;
        private System.Windows.Forms.Panel pnlTransactionCard;
        private System.Windows.Forms.Panel pnlTotalCredit;
        private System.Windows.Forms.Label lblCreditAmount;
        private System.Windows.Forms.PictureBox picCredit;
        private System.Windows.Forms.Label lblTotalCredit;
        private System.Windows.Forms.Label lblTransactionAmount;
        private System.Windows.Forms.Label lblTransction;
        private System.Windows.Forms.PictureBox picTransaction;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.TableLayoutPanel tblTable;
        private System.Windows.Forms.Panel pnlTableHeader;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Button btnNextpage;
        private System.Windows.Forms.Button btnCurrentPage;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.Button btnFirstpage;
        private System.Windows.Forms.DataGridView dgvCreditDataTable;
        private System.Windows.Forms.Panel pnlCreditFooter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCreditTotalPageNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCreditEndingPageNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCreditStartingPageNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentMethod;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPrint;

    }
}