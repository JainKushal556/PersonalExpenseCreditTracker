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
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tblTable = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTableHeader = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.dgvExpenseDataTable = new System.Windows.Forms.DataGridView();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlFooter = new System.Windows.Forms.Panel();
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblTransactionAmount = new System.Windows.Forms.Label();
            this.lblTransction = new System.Windows.Forms.Label();
            this.picTransaction = new System.Windows.Forms.PictureBox();
            this.pnlExpenseFooter = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblExpenseStartingPageNumber = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblExpenseEndingPageNumber = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblExpenseTotalPageNumber = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlContent.SuspendLayout();
            this.tblTable.SuspendLayout();
            this.pnlTableHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenseDataTable)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.pnlControl.SuspendLayout();
            this.tblSummary.SuspendLayout();
            this.pnlTotalExpense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExpense)).BeginInit();
            this.pnlTransactionCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTransaction)).BeginInit();
            this.pnlExpenseFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlContent.Controls.Add(this.tblTable);
            this.pnlContent.Controls.Add(this.pnlFooter);
            this.pnlContent.Controls.Add(this.tblSummary);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(15);
            this.pnlContent.Size = new System.Drawing.Size(1250, 753);
            this.pnlContent.TabIndex = 0;
            this.pnlContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlContent_Paint);
            // 
            // tblTable
            // 
            this.tblTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblTable.ColumnCount = 1;
            this.tblTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTable.Controls.Add(this.pnlTableHeader, 0, 0);
            this.tblTable.Controls.Add(this.dgvExpenseDataTable, 0, 1);
            this.tblTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTable.Location = new System.Drawing.Point(15, 125);
            this.tblTable.Name = "tblTable";
            this.tblTable.RowCount = 2;
            this.tblTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTable.Size = new System.Drawing.Size(1220, 562);
            this.tblTable.TabIndex = 3;
            // 
            // pnlTableHeader
            // 
            this.pnlTableHeader.BackColor = System.Drawing.Color.PaleTurquoise;
            this.pnlTableHeader.Controls.Add(this.btnExport);
            this.pnlTableHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTableHeader.Location = new System.Drawing.Point(4, 4);
            this.pnlTableHeader.Name = "pnlTableHeader";
            this.pnlTableHeader.Size = new System.Drawing.Size(1212, 44);
            this.pnlTableHeader.TabIndex = 0;
            this.pnlTableHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTableHeader_Paint);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.BackColor = System.Drawing.Color.Honeydew;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Image = global::PersonalExpenseCreditTracker.Properties.Resources.share;
            this.btnExport.Location = new System.Drawing.Point(1108, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(92, 32);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export";
            this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExport.UseVisualStyleBackColor = false;
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
            this.dgvExpenseDataTable.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvExpenseDataTable.Location = new System.Drawing.Point(4, 55);
            this.dgvExpenseDataTable.Name = "dgvExpenseDataTable";
            this.dgvExpenseDataTable.ReadOnly = true;
            this.dgvExpenseDataTable.RowHeadersVisible = false;
            this.dgvExpenseDataTable.RowTemplate.Height = 24;
            this.dgvExpenseDataTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExpenseDataTable.Size = new System.Drawing.Size(1212, 503);
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
            this.pnlFooter.Location = new System.Drawing.Point(15, 687);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1220, 51);
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
            this.pnlControl.Location = new System.Drawing.Point(970, 0);
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
            this.btnLastPage.Click += new System.EventHandler(this.btnPervious_Click);
            // 
            // btnNextpage
            // 
            this.btnNextpage.Image = global::PersonalExpenseCreditTracker.Properties.Resources.next;
            this.btnNextpage.Location = new System.Drawing.Point(152, 6);
            this.btnNextpage.Name = "btnNextpage";
            this.btnNextpage.Size = new System.Drawing.Size(40, 40);
            this.btnNextpage.TabIndex = 3;
            this.btnNextpage.UseVisualStyleBackColor = true;
            this.btnNextpage.Click += new System.EventHandler(this.btn_Click);
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
            this.btnCurrentPage.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Image = global::PersonalExpenseCreditTracker.Properties.Resources.preview;
            this.btnPreviousPage.Location = new System.Drawing.Point(59, 6);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(40, 40);
            this.btnPreviousPage.TabIndex = 1;
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            this.btnPreviousPage.Click += new System.EventHandler(this.button2_Click);
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
            // tblSummary
            // 
            this.tblSummary.BackColor = System.Drawing.Color.Transparent;
            this.tblSummary.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblSummary.ColumnCount = 2;
            this.tblSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblSummary.Controls.Add(this.pnlTotalExpense, 0, 0);
            this.tblSummary.Controls.Add(this.pnlTransactionCard, 1, 0);
            this.tblSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblSummary.Location = new System.Drawing.Point(15, 15);
            this.tblSummary.Name = "tblSummary";
            this.tblSummary.RowCount = 1;
            this.tblSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblSummary.Size = new System.Drawing.Size(1220, 110);
            this.tblSummary.TabIndex = 0;
            this.tblSummary.Paint += new System.Windows.Forms.PaintEventHandler(this.tblSummary_Paint);
            // 
            // pnlTotalExpense
            // 
            this.pnlTotalExpense.BackColor = System.Drawing.Color.White;
            this.pnlTotalExpense.Controls.Add(this.lblExpenseAmount);
            this.pnlTotalExpense.Controls.Add(this.picExpense);
            this.pnlTotalExpense.Controls.Add(this.lblTotalExpense);
            this.pnlTotalExpense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTotalExpense.Location = new System.Drawing.Point(16, 16);
            this.pnlTotalExpense.Margin = new System.Windows.Forms.Padding(15);
            this.pnlTotalExpense.Name = "pnlTotalExpense";
            this.pnlTotalExpense.Padding = new System.Windows.Forms.Padding(20);
            this.pnlTotalExpense.Size = new System.Drawing.Size(578, 78);
            this.pnlTotalExpense.TabIndex = 0;
            // 
            // lblExpenseAmount
            // 
            this.lblExpenseAmount.AutoSize = true;
            this.lblExpenseAmount.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpenseAmount.Location = new System.Drawing.Point(85, 37);
            this.lblExpenseAmount.Name = "lblExpenseAmount";
            this.lblExpenseAmount.Size = new System.Drawing.Size(106, 32);
            this.lblExpenseAmount.TabIndex = 2;
            this.lblExpenseAmount.Text = "₹25,000";
            // 
            // picExpense
            // 
            this.picExpense.Image = global::PersonalExpenseCreditTracker.Properties.Resources.spending;
            this.picExpense.Location = new System.Drawing.Point(20, 16);
            this.picExpense.Name = "picExpense";
            this.picExpense.Size = new System.Drawing.Size(48, 48);
            this.picExpense.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picExpense.TabIndex = 0;
            this.picExpense.TabStop = false;
            // 
            // lblTotalExpense
            // 
            this.lblTotalExpense.AutoSize = true;
            this.lblTotalExpense.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalExpense.ForeColor = System.Drawing.Color.Black;
            this.lblTotalExpense.Location = new System.Drawing.Point(85, 11);
            this.lblTotalExpense.Name = "lblTotalExpense";
            this.lblTotalExpense.Size = new System.Drawing.Size(126, 25);
            this.lblTotalExpense.TabIndex = 1;
            this.lblTotalExpense.Text = "Total Expense";
            // 
            // pnlTransactionCard
            // 
            this.pnlTransactionCard.BackColor = System.Drawing.Color.White;
            this.pnlTransactionCard.Controls.Add(this.btnRefresh);
            this.pnlTransactionCard.Controls.Add(this.lblTransactionAmount);
            this.pnlTransactionCard.Controls.Add(this.lblTransction);
            this.pnlTransactionCard.Controls.Add(this.picTransaction);
            this.pnlTransactionCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTransactionCard.Location = new System.Drawing.Point(625, 16);
            this.pnlTransactionCard.Margin = new System.Windows.Forms.Padding(15);
            this.pnlTransactionCard.Name = "pnlTransactionCard";
            this.pnlTransactionCard.Padding = new System.Windows.Forms.Padding(20);
            this.pnlTransactionCard.Size = new System.Drawing.Size(579, 78);
            this.pnlTransactionCard.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.AutoSize = true;
            this.btnRefresh.BackColor = System.Drawing.Color.Beige;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = global::PersonalExpenseCreditTracker.Properties.Resources.refresh;
            this.btnRefresh.Location = new System.Drawing.Point(484, 6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(94, 35);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // lblTransactionAmount
            // 
            this.lblTransactionAmount.AutoSize = true;
            this.lblTransactionAmount.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransactionAmount.Location = new System.Drawing.Point(85, 37);
            this.lblTransactionAmount.Name = "lblTransactionAmount";
            this.lblTransactionAmount.Size = new System.Drawing.Size(43, 32);
            this.lblTransactionAmount.TabIndex = 2;
            this.lblTransactionAmount.Text = "₹0";
            // 
            // lblTransction
            // 
            this.lblTransction.AutoSize = true;
            this.lblTransction.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransction.Location = new System.Drawing.Point(85, 11);
            this.lblTransction.Name = "lblTransction";
            this.lblTransction.Size = new System.Drawing.Size(161, 25);
            this.lblTransction.TabIndex = 1;
            this.lblTransction.Text = "Total Transactions";
            // 
            // picTransaction
            // 
            this.picTransaction.Image = global::PersonalExpenseCreditTracker.Properties.Resources.transaction;
            this.picTransaction.Location = new System.Drawing.Point(20, 16);
            this.picTransaction.Name = "picTransaction";
            this.picTransaction.Size = new System.Drawing.Size(48, 48);
            this.picTransaction.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTransaction.TabIndex = 0;
            this.picTransaction.TabStop = false;
            // 
            // pnlExpenseFooter
            // 
            this.pnlExpenseFooter.Controls.Add(this.label7);
            this.pnlExpenseFooter.Controls.Add(this.lblExpenseTotalPageNumber);
            this.pnlExpenseFooter.Controls.Add(this.label5);
            this.pnlExpenseFooter.Controls.Add(this.lblExpenseEndingPageNumber);
            this.pnlExpenseFooter.Controls.Add(this.label3);
            this.pnlExpenseFooter.Controls.Add(this.lblExpenseStartingPageNumber);
            this.pnlExpenseFooter.Controls.Add(this.label1);
            this.pnlExpenseFooter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlExpenseFooter.Location = new System.Drawing.Point(0, 0);
            this.pnlExpenseFooter.Name = "pnlExpenseFooter";
            this.pnlExpenseFooter.Size = new System.Drawing.Size(348, 51);
            this.pnlExpenseFooter.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Showing";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(92, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "to";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblExpenseEndingPageNumber
            // 
            this.lblExpenseEndingPageNumber.AutoSize = true;
            this.lblExpenseEndingPageNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpenseEndingPageNumber.Location = new System.Drawing.Point(114, 16);
            this.lblExpenseEndingPageNumber.Name = "lblExpenseEndingPageNumber";
            this.lblExpenseEndingPageNumber.Size = new System.Drawing.Size(28, 23);
            this.lblExpenseEndingPageNumber.TabIndex = 3;
            this.lblExpenseEndingPageNumber.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(143, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "of";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // lblExpenseTotalPageNumber
            // 
            this.lblExpenseTotalPageNumber.AutoSize = true;
            this.lblExpenseTotalPageNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpenseTotalPageNumber.Location = new System.Drawing.Point(167, 16);
            this.lblExpenseTotalPageNumber.Name = "lblExpenseTotalPageNumber";
            this.lblExpenseTotalPageNumber.Size = new System.Drawing.Size(28, 23);
            this.lblExpenseTotalPageNumber.TabIndex = 5;
            this.lblExpenseTotalPageNumber.Text = "10";
            this.lblExpenseTotalPageNumber.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(199, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 23);
            this.label7.TabIndex = 6;
            this.label7.Text = "entries";
            // 
            // ExpenseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 753);
            this.Controls.Add(this.pnlContent);
            this.Name = "ExpenseControl";
            this.Text = "ExpenseControl";
            this.Load += new System.EventHandler(this.ExpenseControl_Load);
            this.pnlContent.ResumeLayout(false);
            this.tblTable.ResumeLayout(false);
            this.pnlTableHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpenseDataTable)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlControl.ResumeLayout(false);
            this.tblSummary.ResumeLayout(false);
            this.pnlTotalExpense.ResumeLayout(false);
            this.pnlTotalExpense.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExpense)).EndInit();
            this.pnlTransactionCard.ResumeLayout(false);
            this.pnlTransactionCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTransaction)).EndInit();
            this.pnlExpenseFooter.ResumeLayout(false);
            this.pnlExpenseFooter.PerformLayout();
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblExpenseTotalPageNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblExpenseEndingPageNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblExpenseStartingPageNumber;
        private System.Windows.Forms.Label label1;
        
    }
}