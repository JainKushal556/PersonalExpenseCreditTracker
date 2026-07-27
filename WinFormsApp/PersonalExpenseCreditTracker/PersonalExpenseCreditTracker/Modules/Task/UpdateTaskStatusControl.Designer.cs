namespace PersonalExpenseCreditTracker.Modules.Task
{
    partial class UpdateTaskStatus
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblUpdateStatus = new System.Windows.Forms.Label();
            this.pnlCurrentStatus = new System.Windows.Forms.Panel();
            this.lblCurrentStatus = new System.Windows.Forms.Label();
            this.lblCurrentStatusTitle = new System.Windows.Forms.Label();
            this.pnlTaskTitle = new System.Windows.Forms.Panel();
            this.txtTaskTitle = new System.Windows.Forms.TextBox();
            this.lblTaskTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlMain.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.pnlCurrentStatus.SuspendLayout();
            this.pnlTaskTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.btnUpdate);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.pnlStatus);
            this.pnlMain.Controls.Add(this.lblUpdateStatus);
            this.pnlMain.Controls.Add(this.pnlCurrentStatus);
            this.pnlMain.Controls.Add(this.lblCurrentStatusTitle);
            this.pnlMain.Controls.Add(this.pnlTaskTitle);
            this.pnlMain.Controls.Add(this.lblTaskTitle);
            this.pnlMain.Controls.Add(this.btnClose);
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.ForeColor = System.Drawing.Color.Black;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(513, 524);
            this.pnlMain.TabIndex = 0;
            this.pnlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMain_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(204, 308);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 28);
            this.label2.TabIndex = 12;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(130, 84);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 28);
            this.label1.TabIndex = 11;
            this.label1.Text = "*";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Image = global::PersonalExpenseCreditTracker.Properties.Resources.update;
            this.btnUpdate.Location = new System.Drawing.Point(267, 433);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(219, 59);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "Update Status";
            this.btnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(91)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(38, 433);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(196, 59);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlStatus
            // 
            this.pnlStatus.BackColor = System.Drawing.Color.White;
            this.pnlStatus.Controls.Add(this.cmbStatus);
            this.pnlStatus.Location = new System.Drawing.Point(30, 356);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(453, 52);
            this.pnlStatus.TabIndex = 8;
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Pending",
            "In Progress",
            "Completed"});
            this.cmbStatus.Location = new System.Drawing.Point(10, 8);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(432, 36);
            this.cmbStatus.TabIndex = 7;
            // 
            // lblUpdateStatus
            // 
            this.lblUpdateStatus.AutoSize = true;
            this.lblUpdateStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateStatus.Location = new System.Drawing.Point(27, 310);
            this.lblUpdateStatus.Name = "lblUpdateStatus";
            this.lblUpdateStatus.Size = new System.Drawing.Size(166, 28);
            this.lblUpdateStatus.TabIndex = 6;
            this.lblUpdateStatus.Text = "Update Status To";
            // 
            // pnlCurrentStatus
            // 
            this.pnlCurrentStatus.BackColor = System.Drawing.Color.White;
            this.pnlCurrentStatus.Controls.Add(this.lblCurrentStatus);
            this.pnlCurrentStatus.Location = new System.Drawing.Point(30, 241);
            this.pnlCurrentStatus.Name = "pnlCurrentStatus";
            this.pnlCurrentStatus.Size = new System.Drawing.Size(197, 52);
            this.pnlCurrentStatus.TabIndex = 5;
            // 
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentStatus.Location = new System.Drawing.Point(46, 12);
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Size = new System.Drawing.Size(83, 28);
            this.lblCurrentStatus.TabIndex = 0;
            this.lblCurrentStatus.Text = "Pending";
            // 
            // lblCurrentStatusTitle
            // 
            this.lblCurrentStatusTitle.AutoSize = true;
            this.lblCurrentStatusTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentStatusTitle.Location = new System.Drawing.Point(27, 200);
            this.lblCurrentStatusTitle.Name = "lblCurrentStatusTitle";
            this.lblCurrentStatusTitle.Size = new System.Drawing.Size(141, 28);
            this.lblCurrentStatusTitle.TabIndex = 4;
            this.lblCurrentStatusTitle.Text = "Current Status";
            // 
            // pnlTaskTitle
            // 
            this.pnlTaskTitle.BackColor = System.Drawing.Color.White;
            this.pnlTaskTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTaskTitle.Controls.Add(this.txtTaskTitle);
            this.pnlTaskTitle.Location = new System.Drawing.Point(30, 129);
            this.pnlTaskTitle.Name = "pnlTaskTitle";
            this.pnlTaskTitle.Size = new System.Drawing.Size(453, 52);
            this.pnlTaskTitle.TabIndex = 3;
            // 
            // txtTaskTitle
            // 
            this.txtTaskTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTaskTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaskTitle.Location = new System.Drawing.Point(10, 13);
            this.txtTaskTitle.Name = "txtTaskTitle";
            this.txtTaskTitle.Size = new System.Drawing.Size(432, 27);
            this.txtTaskTitle.TabIndex = 0;
            // 
            // lblTaskTitle
            // 
            this.lblTaskTitle.AutoSize = true;
            this.lblTaskTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaskTitle.Location = new System.Drawing.Point(27, 87);
            this.lblTaskTitle.Name = "lblTaskTitle";
            this.lblTaskTitle.Size = new System.Drawing.Size(96, 28);
            this.lblTaskTitle.TabIndex = 2;
            this.lblTaskTitle.Text = "Task Title";
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::PersonalExpenseCreditTracker.Properties.Resources.close;
            this.btnClose.Location = new System.Drawing.Point(458, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(44, 42);
            this.btnClose.TabIndex = 1;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(231, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Update Task Status";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(28, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 1);
            this.panel1.TabIndex = 13;
            // 
            // UpdateTaskStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 524);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UpdateTaskStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.UpdateTaskStatus_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlStatus.ResumeLayout(false);
            this.pnlCurrentStatus.ResumeLayout(false);
            this.pnlCurrentStatus.PerformLayout();
            this.pnlTaskTitle.ResumeLayout(false);
            this.pnlTaskTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblUpdateStatus;
        private System.Windows.Forms.Panel pnlCurrentStatus;
        private System.Windows.Forms.Label lblCurrentStatus;
        private System.Windows.Forms.Label lblCurrentStatusTitle;
        private System.Windows.Forms.Panel pnlTaskTitle;
        private System.Windows.Forms.TextBox txtTaskTitle;
        private System.Windows.Forms.Label lblTaskTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
    }
}