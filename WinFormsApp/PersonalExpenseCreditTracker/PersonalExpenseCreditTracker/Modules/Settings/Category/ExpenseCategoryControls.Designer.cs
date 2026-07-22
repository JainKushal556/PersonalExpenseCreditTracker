using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PersonalExpenseCreditTracker.Modules.Settings.Category
{
    partial class ExpenseCategoryControls
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.dgvCategory = new System.Windows.Forms.DataGridView();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.tblFooter = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTotalCategory = new System.Windows.Forms.Panel();
            this.lblTotalCategoryNumber = new System.Windows.Forms.Label();
            this.lblTotalCategory = new System.Windows.Forms.Label();
            this.picTotalCategory = new System.Windows.Forms.PictureBox();
            this.pnlActive = new System.Windows.Forms.Panel();
            this.lblActiveNumber = new System.Windows.Forms.Label();
            this.lblActive = new System.Windows.Forms.Label();
            this.pnlInactive = new System.Windows.Forms.Panel();
            this.lblInactiveNumber = new System.Windows.Forms.Label();
            this.lblInactive = new System.Windows.Forms.Label();
            this.pnlTotalSubCategory = new System.Windows.Forms.Panel();
            this.lblTotalSubCategoryNumber = new System.Windows.Forms.Label();
            this.lblTotalSubCategory = new System.Windows.Forms.Label();
            this.picTotalSubCategory = new System.Windows.Forms.PictureBox();
            this.pnlSectionHeader = new System.Windows.Forms.Panel();
            this.pnlSectionBottom = new System.Windows.Forms.Panel();
            this.lblSectionHeader = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlHeaderRight = new System.Windows.Forms.Panel();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.cmsCategoryAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddSubCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMain.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategory)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.tblFooter.SuspendLayout();
            this.pnlTotalCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTotalCategory)).BeginInit();
            this.pnlActive.SuspendLayout();
            this.pnlInactive.SuspendLayout();
            this.pnlTotalSubCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTotalSubCategory)).BeginInit();
            this.pnlSectionHeader.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlHeaderRight.SuspendLayout();
            this.cmsCategoryAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(10, 10);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMain.Size = new System.Drawing.Size(1301, 680);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvCategory);
            this.pnlContent.Controls.Add(this.pnlFooter);
            this.pnlContent.Controls.Add(this.pnlSectionHeader);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(10, 64);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(10);
            this.pnlContent.Size = new System.Drawing.Size(1279, 604);
            this.pnlContent.TabIndex = 2;
            // 
            // dgvCategory
            // 
            this.dgvCategory.AllowUserToAddRows = false;
            this.dgvCategory.AllowUserToDeleteRows = false;
            this.dgvCategory.AllowUserToResizeColumns = false;
            this.dgvCategory.AllowUserToResizeRows = false;
            this.dgvCategory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCategory.BackgroundColor = System.Drawing.Color.White;
            this.dgvCategory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCategory.EnableHeadersVisualStyles = false;
            this.dgvCategory.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.dgvCategory.Location = new System.Drawing.Point(10, 45);
            this.dgvCategory.MultiSelect = false;
            this.dgvCategory.Name = "dgvCategory";
            this.dgvCategory.ReadOnly = true;
            this.dgvCategory.RowHeadersVisible = false;
            this.dgvCategory.RowTemplate.Height = 42;
            this.dgvCategory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategory.Size = new System.Drawing.Size(1259, 499);
            this.dgvCategory.TabIndex = 2;
            this.dgvCategory.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategory_CellClick);
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.tblFooter);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(10, 544);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1259, 50);
            this.pnlFooter.TabIndex = 1;
            // 
            // tblFooter
            // 
            this.tblFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(233)))), ((int)(((byte)(255)))));
            this.tblFooter.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetPartial;
            this.tblFooter.ColumnCount = 4;
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblFooter.Controls.Add(this.pnlTotalCategory, 0, 0);
            this.tblFooter.Controls.Add(this.pnlActive, 2, 0);
            this.tblFooter.Controls.Add(this.pnlInactive, 3, 0);
            this.tblFooter.Controls.Add(this.pnlTotalSubCategory, 1, 0);
            this.tblFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFooter.Location = new System.Drawing.Point(0, 0);
            this.tblFooter.Name = "tblFooter";
            this.tblFooter.RowCount = 1;
            this.tblFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.Size = new System.Drawing.Size(1259, 50);
            this.tblFooter.TabIndex = 0;
            // 
            // pnlTotalCategory
            // 
            this.pnlTotalCategory.BackColor = System.Drawing.Color.Transparent;
            this.pnlTotalCategory.Controls.Add(this.lblTotalCategoryNumber);
            this.pnlTotalCategory.Controls.Add(this.lblTotalCategory);
            this.pnlTotalCategory.Controls.Add(this.picTotalCategory);
            this.pnlTotalCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTotalCategory.Location = new System.Drawing.Point(8, 8);
            this.pnlTotalCategory.Margin = new System.Windows.Forms.Padding(5);
            this.pnlTotalCategory.Name = "pnlTotalCategory";
            this.pnlTotalCategory.Padding = new System.Windows.Forms.Padding(10);
            this.pnlTotalCategory.Size = new System.Drawing.Size(363, 34);
            this.pnlTotalCategory.TabIndex = 0;
            // 
            // lblTotalCategoryNumber
            // 
            this.lblTotalCategoryNumber.AutoSize = true;
            this.lblTotalCategoryNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCategoryNumber.ForeColor = System.Drawing.Color.DarkViolet;
            this.lblTotalCategoryNumber.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTotalCategoryNumber.Location = new System.Drawing.Point(178, 10);
            this.lblTotalCategoryNumber.Name = "lblTotalCategoryNumber";
            this.lblTotalCategoryNumber.Size = new System.Drawing.Size(17, 19);
            this.lblTotalCategoryNumber.TabIndex = 2;
            this.lblTotalCategoryNumber.Text = "5";
            // 
            // lblTotalCategory
            // 
            this.lblTotalCategory.AutoSize = true;
            this.lblTotalCategory.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCategory.Location = new System.Drawing.Point(51, 9);
            this.lblTotalCategory.Name = "lblTotalCategory";
            this.lblTotalCategory.Size = new System.Drawing.Size(126, 19);
            this.lblTotalCategory.TabIndex = 1;
            this.lblTotalCategory.Text = "Total Categories :";
            // 
            // picTotalCategory
            // 
            this.picTotalCategory.Image = global::PersonalExpenseCreditTracker.Properties.Resources.grid;
            this.picTotalCategory.Location = new System.Drawing.Point(10, 3);
            this.picTotalCategory.Name = "picTotalCategory";
            this.picTotalCategory.Size = new System.Drawing.Size(32, 32);
            this.picTotalCategory.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTotalCategory.TabIndex = 0;
            this.picTotalCategory.TabStop = false;
            // 
            // pnlActive
            // 
            this.pnlActive.Controls.Add(this.lblActiveNumber);
            this.pnlActive.Controls.Add(this.lblActive);
            this.pnlActive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlActive.Location = new System.Drawing.Point(760, 8);
            this.pnlActive.Margin = new System.Windows.Forms.Padding(5);
            this.pnlActive.Name = "pnlActive";
            this.pnlActive.Padding = new System.Windows.Forms.Padding(10);
            this.pnlActive.Size = new System.Drawing.Size(238, 34);
            this.pnlActive.TabIndex = 1;
            // 
            // lblActiveNumber
            // 
            this.lblActiveNumber.AutoSize = true;
            this.lblActiveNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveNumber.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblActiveNumber.Location = new System.Drawing.Point(68, 10);
            this.lblActiveNumber.Name = "lblActiveNumber";
            this.lblActiveNumber.Size = new System.Drawing.Size(25, 19);
            this.lblActiveNumber.TabIndex = 2;
            this.lblActiveNumber.Text = "10";
            // 
            // lblActive
            // 
            this.lblActive.AutoSize = true;
            this.lblActive.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActive.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblActive.Location = new System.Drawing.Point(13, 9);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(59, 19);
            this.lblActive.TabIndex = 1;
            this.lblActive.Text = "Active :";
            // 
            // pnlInactive
            // 
            this.pnlInactive.Controls.Add(this.lblInactiveNumber);
            this.pnlInactive.Controls.Add(this.lblInactive);
            this.pnlInactive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInactive.Location = new System.Drawing.Point(1011, 8);
            this.pnlInactive.Margin = new System.Windows.Forms.Padding(5);
            this.pnlInactive.Name = "pnlInactive";
            this.pnlInactive.Padding = new System.Windows.Forms.Padding(10);
            this.pnlInactive.Size = new System.Drawing.Size(240, 34);
            this.pnlInactive.TabIndex = 2;
            // 
            // lblInactiveNumber
            // 
            this.lblInactiveNumber.AutoSize = true;
            this.lblInactiveNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInactiveNumber.ForeColor = System.Drawing.Color.Red;
            this.lblInactiveNumber.Location = new System.Drawing.Point(80, 10);
            this.lblInactiveNumber.Name = "lblInactiveNumber";
            this.lblInactiveNumber.Size = new System.Drawing.Size(17, 19);
            this.lblInactiveNumber.TabIndex = 1;
            this.lblInactiveNumber.Text = "5";
            // 
            // lblInactive
            // 
            this.lblInactive.AutoSize = true;
            this.lblInactive.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInactive.ForeColor = System.Drawing.Color.Red;
            this.lblInactive.Location = new System.Drawing.Point(14, 9);
            this.lblInactive.Name = "lblInactive";
            this.lblInactive.Size = new System.Drawing.Size(69, 19);
            this.lblInactive.TabIndex = 0;
            this.lblInactive.Text = "Inactive :";
            // 
            // pnlTotalSubCategory
            // 
            this.pnlTotalSubCategory.Controls.Add(this.lblTotalSubCategoryNumber);
            this.pnlTotalSubCategory.Controls.Add(this.lblTotalSubCategory);
            this.pnlTotalSubCategory.Controls.Add(this.picTotalSubCategory);
            this.pnlTotalSubCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTotalSubCategory.Location = new System.Drawing.Point(384, 8);
            this.pnlTotalSubCategory.Margin = new System.Windows.Forms.Padding(5);
            this.pnlTotalSubCategory.Name = "pnlTotalSubCategory";
            this.pnlTotalSubCategory.Padding = new System.Windows.Forms.Padding(10);
            this.pnlTotalSubCategory.Size = new System.Drawing.Size(363, 34);
            this.pnlTotalSubCategory.TabIndex = 3;
            // 
            // lblTotalSubCategoryNumber
            // 
            this.lblTotalSubCategoryNumber.AutoSize = true;
            this.lblTotalSubCategoryNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSubCategoryNumber.ForeColor = System.Drawing.Color.DarkViolet;
            this.lblTotalSubCategoryNumber.Location = new System.Drawing.Point(204, 10);
            this.lblTotalSubCategoryNumber.Name = "lblTotalSubCategoryNumber";
            this.lblTotalSubCategoryNumber.Size = new System.Drawing.Size(25, 19);
            this.lblTotalSubCategoryNumber.TabIndex = 2;
            this.lblTotalSubCategoryNumber.Text = "25";
            // 
            // lblTotalSubCategory
            // 
            this.lblTotalSubCategory.AutoSize = true;
            this.lblTotalSubCategory.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSubCategory.Location = new System.Drawing.Point(49, 9);
            this.lblTotalSubCategory.Name = "lblTotalSubCategory";
            this.lblTotalSubCategory.Size = new System.Drawing.Size(151, 19);
            this.lblTotalSubCategory.TabIndex = 1;
            this.lblTotalSubCategory.Text = "Total SubCategories :";
            // 
            // picTotalSubCategory
            // 
            this.picTotalSubCategory.Image = global::PersonalExpenseCreditTracker.Properties.Resources.categorization;
            this.picTotalSubCategory.Location = new System.Drawing.Point(10, 3);
            this.picTotalSubCategory.Name = "picTotalSubCategory";
            this.picTotalSubCategory.Size = new System.Drawing.Size(32, 32);
            this.picTotalSubCategory.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTotalSubCategory.TabIndex = 0;
            this.picTotalSubCategory.TabStop = false;
            // 
            // pnlSectionHeader
            // 
            this.pnlSectionHeader.Controls.Add(this.pnlSectionBottom);
            this.pnlSectionHeader.Controls.Add(this.lblSectionHeader);
            this.pnlSectionHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSectionHeader.Location = new System.Drawing.Point(10, 10);
            this.pnlSectionHeader.Name = "pnlSectionHeader";
            this.pnlSectionHeader.Size = new System.Drawing.Size(1259, 35);
            this.pnlSectionHeader.TabIndex = 0;
            // 
            // pnlSectionBottom
            // 
            this.pnlSectionBottom.BackColor = System.Drawing.Color.DodgerBlue;
            this.pnlSectionBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSectionBottom.Location = new System.Drawing.Point(0, 33);
            this.pnlSectionBottom.Name = "pnlSectionBottom";
            this.pnlSectionBottom.Size = new System.Drawing.Size(1259, 2);
            this.pnlSectionBottom.TabIndex = 1;
            // 
            // lblSectionHeader
            // 
            this.lblSectionHeader.AutoSize = true;
            this.lblSectionHeader.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSectionHeader.Location = new System.Drawing.Point(12, 6);
            this.lblSectionHeader.Name = "lblSectionHeader";
            this.lblSectionHeader.Size = new System.Drawing.Size(103, 19);
            this.lblSectionHeader.TabIndex = 0;
            this.lblSectionHeader.Text = "All Categories";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(233)))), ((int)(((byte)(255)))));
            this.pnlHeader.Controls.Add(this.pnlHeaderRight);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(10, 10);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(5);
            this.pnlHeader.Size = new System.Drawing.Size(1279, 54);
            this.pnlHeader.TabIndex = 1;
            // 
            // pnlHeaderRight
            // 
            this.pnlHeaderRight.Controls.Add(this.btnAddCategory);
            this.pnlHeaderRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlHeaderRight.Location = new System.Drawing.Point(1074, 5);
            this.pnlHeaderRight.Name = "pnlHeaderRight";
            this.pnlHeaderRight.Size = new System.Drawing.Size(200, 44);
            this.pnlHeaderRight.TabIndex = 2;
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.AutoSize = true;
            this.btnAddCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnAddCategory.FlatAppearance.BorderSize = 0;
            this.btnAddCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCategory.ForeColor = System.Drawing.Color.White;
            this.btnAddCategory.Location = new System.Drawing.Point(50, 2);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(145, 38);
            this.btnAddCategory.TabIndex = 2;
            this.btnAddCategory.Text = "+ Add Category";
            this.btnAddCategory.UseVisualStyleBackColor = false;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lblTitle.Location = new System.Drawing.Point(15, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(184, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Expense Categories";
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubTitle.Location = new System.Drawing.Point(17, 34);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(302, 15);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Create, manage and deactivate your expense categories.";
            // 
            // cmsCategoryAction
            // 
            this.cmsCategoryAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEdit,
            this.tsmiAddSubCategory});
            this.cmsCategoryAction.Name = "cmsCategoryAction";
            this.cmsCategoryAction.Size = new System.Drawing.Size(168, 48);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(167, 22);
            this.tsmiEdit.Text = "Edit";
            // 
            // tsmiAddSubCategory
            // 
            this.tsmiAddSubCategory.Name = "tsmiAddSubCategory";
            this.tsmiAddSubCategory.Size = new System.Drawing.Size(167, 22);
            this.tsmiAddSubCategory.Text = "Add SubCategory";
            // 
            // ExpenseCategoryControls
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1321, 700);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ExpenseCategoryControls";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "ExpenseCategoryControls";
            this.Load += new System.EventHandler(this.ExpenseCategoryControls_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategory)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.tblFooter.ResumeLayout(false);
            this.pnlTotalCategory.ResumeLayout(false);
            this.pnlTotalCategory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTotalCategory)).EndInit();
            this.pnlActive.ResumeLayout(false);
            this.pnlActive.PerformLayout();
            this.pnlInactive.ResumeLayout(false);
            this.pnlInactive.PerformLayout();
            this.pnlTotalSubCategory.ResumeLayout(false);
            this.pnlTotalSubCategory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTotalSubCategory)).EndInit();
            this.pnlSectionHeader.ResumeLayout(false);
            this.pnlSectionHeader.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlHeaderRight.ResumeLayout(false);
            this.pnlHeaderRight.PerformLayout();
            this.cmsCategoryAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private Panel pnlMain;
        private Panel pnlHeader;

        private Label lblTitle;
        private Label lblSubTitle;
        private Panel pnlContent;
        private Panel pnlSectionHeader;
        private Panel pnlSectionBottom;
        private Label lblSectionHeader;
        private Button btnAddCategory;
        private Panel pnlFooter;
        private DataGridView dgvCategory;
        private ContextMenuStrip cmsCategoryAction;
        private ToolStripMenuItem tsmiEdit;
        private Panel pnlHeaderRight;
        private TableLayoutPanel tblFooter;
        private Panel pnlTotalCategory;
        private PictureBox picTotalCategory;
        private Label lblTotalCategoryNumber;
        private Label lblTotalCategory;
        private Label lblActive;
        private Panel pnlActive;
        private Label lblActiveNumber;
        private Panel pnlInactive;
        private Label lblInactiveNumber;
        private Label lblInactive;
        private Panel pnlTotalSubCategory;
        private PictureBox picTotalSubCategory;
        private Label lblTotalSubCategoryNumber;
        private Label lblTotalSubCategory;
        private ToolStripMenuItem tsmiAddSubCategory;



      
    }
}