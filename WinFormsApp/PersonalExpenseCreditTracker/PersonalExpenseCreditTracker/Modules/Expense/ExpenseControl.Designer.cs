namespace PersonalExpenseCreditTracker.Modules.Expense
{
    partial class ExpenseControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tblTable = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTableHeader = new System.Windows.Forms.Panel();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvExpenseDataTable = new System.Windows.Forms.DataGridView();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pnlExpenseFooter = new System.Windows.Forms.Panel();
            this.lblentries = new System.Windows.Forms.Label();
            this.lblExpenseTotalPageNumber = new System.Windows.Forms.Label();
            this.lblof = new System.Windows.Forms.Label();
            this.lblExpenseEndingPageNumber = new System.Windows.Forms.Label();
            this.lblto = new System.Windows.Forms.Label();
            this.lblExpenseStartingPageNumber = new System.Windows.Forms.Label();
            this.lblShowing = new System.Windows.Forms.Label();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.btnNextpage = new System.Windows.Forms.Button();
            this.btnCurrentPage = new System.Windows.Forms.Button();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.btnFirstpage = new System.Windows.Forms.Button();
            this.tblSummary = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTotalExpense = new System.Windows.Forms.Panel();
            this.lblExpenseAmount = new System.Windows.Forms.Label();
            this.picExpense = new System.Windows.Forms.PictureBox();
            this.lblTotalExpense = new System.Windows.Forms.Label();
            this.pnlTransactionCard = new System.Windows.Forms.Panel();
            this.lblTransactionAmount = new System.Windows.Forms.Label();
            this.lblTransction = new System.Windows.Forms.Label();
            this.picTransaction = new System.Windows.Forms.PictureBox();
            this.pnlContent.SuspendLayout();
            this.tblTable.SuspendLayout();
            this.pnlTableHeader.SuspendLayout();
            this.pnlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenseDataTable)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.pnlExpenseFooter.SuspendLayout();
            this.pnlControl.SuspendLayout();
            this.tblSummary.SuspendLayout();
            this.pnlTotalExpense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExpense)).BeginInit();
            this.pnlTransactionCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTransaction)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(253)))));
            this.pnlContent.Controls.Add(this.tblTable);
            this.pnlContent.Controls.Add(this.pnlFooter);
            this.pnlContent.Controls.Add(this.tblSummary);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(3);
            this.pnlContent.Size = new System.Drawing.Size(1250, 753);
            this.pnlContent.TabIndex = 0;
            // 
            // tblTable
            // 
            this.tblTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(253)))));
            this.tblTable.ColumnCount = 1;
            this.tblTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTable.Controls.Add(this.pnlTableHeader, 0, 0);
            this.tblTable.Controls.Add(this.dgvExpenseDataTable, 0, 1);
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
            this.pnlTableHeader.Controls.Add(this.pnlButton);
            this.pnlTableHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTableHeader.Location = new System.Drawing.Point(11, 8);
            this.pnlTableHeader.Name = "pnlTableHeader";
            this.pnlTableHeader.Size = new System.Drawing.Size(1222, 44);
            this.pnlTableHeader.TabIndex = 0;
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.btnPrint);
            this.pnlButton.Controls.Add(this.btnExport);
            this.pnlButton.Controls.Add(this.btnRefresh);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlButton.Location = new System.Drawing.Point(881, 0);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(341, 44);
            this.pnlButton.TabIndex = 0;
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
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.UseVisualStyleBackColor = false;
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
            // btnRefresh
            // 
            this.btnRefresh.AutoSize = true;
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(127)))), ((int)(((byte)(242)))));
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(91)))), ((int)(((byte)(176)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(103)))), ((int)(((byte)(199)))));
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
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
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvExpenseDataTable
            // 
            this.dgvExpenseDataTable.AllowUserToAddRows = false;
            this.dgvExpenseDataTable.AllowUserToDeleteRows = false;
            this.dgvExpenseDataTable.AllowUserToResizeColumns = false;
            this.dgvExpenseDataTable.AllowUserToResizeRows = false;
            this.dgvExpenseDataTable.BackgroundColor = System.Drawing.Color.White;
            this.dgvExpenseDataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExpenseDataTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDate,
            this.colDescription,
            this.colCategory,
            this.colSubCategory,
            this.colAmount,
            this.colPaymentMethod});
            this.dgvExpenseDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExpenseDataTable.EnableHeadersVisualStyles = false;
            this.dgvExpenseDataTable.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(236)))), ((int)(((byte)(242)))));
            this.dgvExpenseDataTable.Location = new System.Drawing.Point(11, 58);
            this.dgvExpenseDataTable.Name = "dgvExpenseDataTable";
            this.dgvExpenseDataTable.ReadOnly = true;
            this.dgvExpenseDataTable.RowHeadersVisible = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.dgvExpenseDataTable.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvExpenseDataTable.RowTemplate.Height = 24;
            this.dgvExpenseDataTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExpenseDataTable.Size = new System.Drawing.Size(1222, 520);
            this.dgvExpenseDataTable.TabIndex = 1;
            this.dgvExpenseDataTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExpenseDataTable_CellContentClick);
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
            this.colDescription.FillWeight = 20F;
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
            this.pnlFooter.Controls.Add(this.pnlExpenseFooter);
            this.pnlFooter.Controls.Add(this.pnlControl);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(3, 699);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1244, 51);
            this.pnlFooter.TabIndex = 2;
            // 
            // pnlExpenseFooter
            // 
            this.pnlExpenseFooter.Controls.Add(this.lblentries);
            this.pnlExpenseFooter.Controls.Add(this.lblExpenseTotalPageNumber);
            this.pnlExpenseFooter.Controls.Add(this.lblof);
            this.pnlExpenseFooter.Controls.Add(this.lblExpenseEndingPageNumber);
            this.pnlExpenseFooter.Controls.Add(this.lblto);
            this.pnlExpenseFooter.Controls.Add(this.lblExpenseStartingPageNumber);
            this.pnlExpenseFooter.Controls.Add(this.lblShowing);
            this.pnlExpenseFooter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlExpenseFooter.Location = new System.Drawing.Point(0, 0);
            this.pnlExpenseFooter.Name = "pnlExpenseFooter";
            this.pnlExpenseFooter.Size = new System.Drawing.Size(348, 51);
            this.pnlExpenseFooter.TabIndex = 1;
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
            // lblExpenseTotalPageNumber
            // 
            this.lblExpenseTotalPageNumber.AutoSize = true;
            this.lblExpenseTotalPageNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpenseTotalPageNumber.Location = new System.Drawing.Point(191, 16);
            this.lblExpenseTotalPageNumber.Name = "lblExpenseTotalPageNumber";
            this.lblExpenseTotalPageNumber.Size = new System.Drawing.Size(28, 23);
            this.lblExpenseTotalPageNumber.TabIndex = 5;
            this.lblExpenseTotalPageNumber.Text = "10";
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
            // lblExpenseEndingPageNumber
            // 
            this.lblExpenseEndingPageNumber.AutoSize = true;
            this.lblExpenseEndingPageNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpenseEndingPageNumber.Location = new System.Drawing.Point(128, 16);
            this.lblExpenseEndingPageNumber.Name = "lblExpenseEndingPageNumber";
            this.lblExpenseEndingPageNumber.Size = new System.Drawing.Size(28, 23);
            this.lblExpenseEndingPageNumber.TabIndex = 3;
            this.lblExpenseEndingPageNumber.Text = "10";
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
            // lblExpenseStartingPageNumber
            // 
            this.lblExpenseStartingPageNumber.AutoSize = true;
            this.lblExpenseStartingPageNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpenseStartingPageNumber.Location = new System.Drawing.Point(76, 15);
            this.lblExpenseStartingPageNumber.Name = "lblExpenseStartingPageNumber";
            this.lblExpenseStartingPageNumber.Size = new System.Drawing.Size(19, 23);
            this.lblExpenseStartingPageNumber.TabIndex = 1;
            this.lblExpenseStartingPageNumber.Text = "1";
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
            // tblSummary
            // 
            this.tblSummary.BackColor = System.Drawing.Color.Transparent;
            this.tblSummary.ColumnCount = 2;
            this.tblSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblSummary.Controls.Add(this.pnlTotalExpense, 0, 0);
            this.tblSummary.Controls.Add(this.pnlTransactionCard, 1, 0);
            this.tblSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblSummary.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblSummary.Location = new System.Drawing.Point(3, 3);
            this.tblSummary.Margin = new System.Windows.Forms.Padding(0);
            this.tblSummary.Name = "tblSummary";
            this.tblSummary.RowCount = 1;
            this.tblSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblSummary.Size = new System.Drawing.Size(1244, 110);
            this.tblSummary.TabIndex = 0;
            // 
            // pnlTotalExpense
            // 
            this.pnlTotalExpense.BackColor = System.Drawing.Color.White;
            this.pnlTotalExpense.Controls.Add(this.lblExpenseAmount);
            this.pnlTotalExpense.Controls.Add(this.picExpense);
            this.pnlTotalExpense.Controls.Add(this.lblTotalExpense);
            this.pnlTotalExpense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTotalExpense.Location = new System.Drawing.Point(11, 10);
            this.pnlTotalExpense.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.pnlTotalExpense.Name = "pnlTotalExpense";
            this.pnlTotalExpense.Padding = new System.Windows.Forms.Padding(20);
            this.pnlTotalExpense.Size = new System.Drawing.Size(600, 90);
            this.pnlTotalExpense.TabIndex = 0;
            this.pnlTotalExpense.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTotalExpense_Paint);
            // 
            // lblExpenseAmount
            // 
            this.lblExpenseAmount.AutoSize = true;
            this.lblExpenseAmount.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpenseAmount.ForeColor = System.Drawing.Color.Black;
            this.lblExpenseAmount.Location = new System.Drawing.Point(85, 42);
            this.lblExpenseAmount.Name = "lblExpenseAmount";
            this.lblExpenseAmount.Size = new System.Drawing.Size(106, 32);
            this.lblExpenseAmount.TabIndex = 2;
            this.lblExpenseAmount.Text = "₹25,000";
            // 
            // picExpense
            // 
            this.picExpense.Image = global::PersonalExpenseCreditTracker.Properties.Resources.spending;
            this.picExpense.Location = new System.Drawing.Point(20, 22);
            this.picExpense.Name = "picExpense";
            this.picExpense.Size = new System.Drawing.Size(48, 48);
            this.picExpense.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picExpense.TabIndex = 0;
            this.picExpense.TabStop = false;
            // 
            // lblTotalExpense
            // 
            this.lblTotalExpense.AutoSize = true;
            this.lblTotalExpense.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalExpense.ForeColor = System.Drawing.Color.Black;
            this.lblTotalExpense.Location = new System.Drawing.Point(85, 12);
            this.lblTotalExpense.Name = "lblTotalExpense";
            this.lblTotalExpense.Size = new System.Drawing.Size(118, 23);
            this.lblTotalExpense.TabIndex = 1;
            this.lblTotalExpense.Text = "Total Expense";
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
            this.lblTransactionAmount.Size = new System.Drawing.Size(43, 32);
            this.lblTransactionAmount.TabIndex = 2;
            this.lblTransactionAmount.Text = "₹0";
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
            this.picTransaction.Size = new System.Drawing.Size(48, 48);
            this.picTransaction.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTransaction.TabIndex = 0;
            this.picTransaction.TabStop = false;
            // 
            // ExpenseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1250, 753);
            this.Controls.Add(this.pnlContent);
            this.Name = "ExpenseControl";
            this.Text = "ExpenseControl";
            this.Load += new System.EventHandler(this.ExpenseControl_Load);
            this.pnlContent.ResumeLayout(false);
            this.tblTable.ResumeLayout(false);
            this.pnlTableHeader.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.pnlButton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenseDataTable)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlExpenseFooter.ResumeLayout(false);
            this.pnlExpenseFooter.PerformLayout();
            this.pnlControl.ResumeLayout(false);
            this.tblSummary.ResumeLayout(false);
            this.pnlTotalExpense.ResumeLayout(false);
            this.pnlTotalExpense.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExpense)).EndInit();
            this.pnlTransactionCard.ResumeLayout(false);
            this.pnlTransactionCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTransaction)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TableLayoutPanel tblSummary;
        private System.Windows.Forms.Panel pnlTransactionCard;
        private System.Windows.Forms.Panel pnlTotalExpense;
        private System.Windows.Forms.Label lblExpenseAmount;
        private System.Windows.Forms.PictureBox picExpense;
        private System.Windows.Forms.Label lblTotalExpense;
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
        private System.Windows.Forms.DataGridView dgvExpenseDataTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentMethod;
        private System.Windows.Forms.Panel pnlExpenseFooter;
        private System.Windows.Forms.Label lblentries;
        private System.Windows.Forms.Label lblExpenseTotalPageNumber;
        private System.Windows.Forms.Label lblof;
        private System.Windows.Forms.Label lblExpenseEndingPageNumber;
        private System.Windows.Forms.Label lblto;
        private System.Windows.Forms.Label lblExpenseStartingPageNumber;
        private System.Windows.Forms.Label lblShowing;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Button btnPrint;


     
    }
}