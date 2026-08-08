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
            this.components = new System.ComponentModel.Container();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblUpdateStatus = new System.Windows.Forms.Label();
            this.pnlCurrentStatus = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblCurrentStatus = new System.Windows.Forms.Label();
            this.lblCurrentStatusTitle = new System.Windows.Forms.Label();
            this.pnlTaskTitle = new System.Windows.Forms.Panel();
            this.piclock = new System.Windows.Forms.PictureBox();
            this.txtTaskTitle = new System.Windows.Forms.TextBox();
            this.lblTaskTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlMain.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.pnlCurrentStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlTaskTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piclock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.pnlBody);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.btnUpdate);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.btnClose);
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.ForeColor = System.Drawing.Color.Black;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(548, 320);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Controls.Add(this.label2);
            this.pnlBody.Controls.Add(this.pnlStatus);
            this.pnlBody.Controls.Add(this.lblUpdateStatus);
            this.pnlBody.Controls.Add(this.pnlCurrentStatus);
            this.pnlBody.Controls.Add(this.lblCurrentStatusTitle);
            this.pnlBody.Controls.Add(this.pnlTaskTitle);
            this.pnlBody.Controls.Add(this.lblTaskTitle);
            this.pnlBody.Location = new System.Drawing.Point(19, 65);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(508, 180);
            this.pnlBody.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(171, 122);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 23);
            this.label2.TabIndex = 12;
            this.label2.Text = "*";
            // 
            // pnlStatus
            // 
            this.pnlStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatus.Controls.Add(this.cmbStatus);
            this.pnlStatus.Location = new System.Drawing.Point(195, 122);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(295, 31);
            this.pnlStatus.TabIndex = 8;
            // 
            // cmbStatus
            // 
            this.cmbStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(4, -2);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(289, 31);
            this.cmbStatus.TabIndex = 7;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // lblUpdateStatus
            // 
            this.lblUpdateStatus.AutoSize = true;
            this.lblUpdateStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateStatus.Location = new System.Drawing.Point(13, 123);
            this.lblUpdateStatus.Name = "lblUpdateStatus";
            this.lblUpdateStatus.Size = new System.Drawing.Size(155, 25);
            this.lblUpdateStatus.TabIndex = 6;
            this.lblUpdateStatus.Text = "Update Status To";
            // 
            // pnlCurrentStatus
            // 
            this.pnlCurrentStatus.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlCurrentStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCurrentStatus.Controls.Add(this.pictureBox1);
            this.pnlCurrentStatus.Controls.Add(this.lblCurrentStatus);
            this.pnlCurrentStatus.Location = new System.Drawing.Point(195, 71);
            this.pnlCurrentStatus.Name = "pnlCurrentStatus";
            this.pnlCurrentStatus.Size = new System.Drawing.Size(295, 31);
            this.pnlCurrentStatus.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PersonalExpenseCreditTracker.Properties.Resources.padlock__5_;
            this.pictureBox1.Location = new System.Drawing.Point(260, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentStatus.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentStatus.Location = new System.Drawing.Point(4, 3);
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Size = new System.Drawing.Size(72, 23);
            this.lblCurrentStatus.TabIndex = 0;
            this.lblCurrentStatus.Text = "Pending";
            // 
            // lblCurrentStatusTitle
            // 
            this.lblCurrentStatusTitle.AutoSize = true;
            this.lblCurrentStatusTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold);
            this.lblCurrentStatusTitle.Location = new System.Drawing.Point(13, 72);
            this.lblCurrentStatusTitle.Name = "lblCurrentStatusTitle";
            this.lblCurrentStatusTitle.Size = new System.Drawing.Size(134, 25);
            this.lblCurrentStatusTitle.TabIndex = 4;
            this.lblCurrentStatusTitle.Text = "Current Status";
            // 
            // pnlTaskTitle
            // 
            this.pnlTaskTitle.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlTaskTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTaskTitle.Controls.Add(this.piclock);
            this.pnlTaskTitle.Controls.Add(this.txtTaskTitle);
            this.pnlTaskTitle.Location = new System.Drawing.Point(195, 20);
            this.pnlTaskTitle.Name = "pnlTaskTitle";
            this.pnlTaskTitle.Size = new System.Drawing.Size(295, 31);
            this.pnlTaskTitle.TabIndex = 3;
            // 
            // piclock
            // 
            this.piclock.Image = global::PersonalExpenseCreditTracker.Properties.Resources.padlock__5_;
            this.piclock.Location = new System.Drawing.Point(260, 1);
            this.piclock.Name = "piclock";
            this.piclock.Size = new System.Drawing.Size(25, 26);
            this.piclock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.piclock.TabIndex = 1;
            this.piclock.TabStop = false;
            // 
            // txtTaskTitle
            // 
            this.txtTaskTitle.BackColor = System.Drawing.Color.Gainsboro;
            this.txtTaskTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTaskTitle.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaskTitle.ForeColor = System.Drawing.Color.Black;
            this.txtTaskTitle.Location = new System.Drawing.Point(5, 2);
            this.txtTaskTitle.Name = "txtTaskTitle";
            this.txtTaskTitle.ReadOnly = true;
            this.txtTaskTitle.Size = new System.Drawing.Size(249, 23);
            this.txtTaskTitle.TabIndex = 0;
            // 
            // lblTaskTitle
            // 
            this.lblTaskTitle.AutoSize = true;
            this.lblTaskTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaskTitle.Location = new System.Drawing.Point(13, 21);
            this.lblTaskTitle.Name = "lblTaskTitle";
            this.lblTaskTitle.Size = new System.Drawing.Size(90, 25);
            this.lblTaskTitle.TabIndex = 2;
            this.lblTaskTitle.Text = "Task Title";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(19, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(508, 1);
            this.panel1.TabIndex = 13;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Image = global::PersonalExpenseCreditTracker.Properties.Resources.update__4_;
            this.btnUpdate.Location = new System.Drawing.Point(390, 262);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.btnUpdate.Size = new System.Drawing.Size(137, 41);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "Update";
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
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(259, 262);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 41);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::PersonalExpenseCreditTracker.Properties.Resources.close;
            this.btnClose.Location = new System.Drawing.Point(498, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(39, 35);
            this.btnClose.TabIndex = 1;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(17, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(192, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Update Task Status";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // UpdateTaskStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(548, 320);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UpdateTaskStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.UpdateTaskStatus_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.pnlStatus.ResumeLayout(false);
            this.pnlCurrentStatus.ResumeLayout(false);
            this.pnlCurrentStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlTaskTitle.ResumeLayout(false);
            this.pnlTaskTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piclock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.PictureBox piclock;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}