namespace PersonalExpenseCreditTracker.Modules.Task
{
    partial class AddTaskControl
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
            this.pnlAddTask = new System.Windows.Forms.Panel();
            this.pnlDeadlinePicker = new System.Windows.Forms.Panel();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.pnlDeadline = new System.Windows.Forms.Panel();
            this.btnCalendar = new System.Windows.Forms.Button();
            this.txtDeadline = new System.Windows.Forms.TextBox();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.lvlStatus = new System.Windows.Forms.Label();
            this.pnlPriority = new System.Windows.Forms.Panel();
            this.cmbPriority = new System.Windows.Forms.ComboBox();
            this.pnlTaskTitle = new System.Windows.Forms.Panel();
            this.txtTaskTitle = new System.Windows.Forms.TextBox();
            this.lblDeadline = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPriority = new System.Windows.Forms.Label();
            this.lblTaskTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAddTask = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlAddTask.SuspendLayout();
            this.pnlDeadlinePicker.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlDeadline.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.pnlPriority.SuspendLayout();
            this.pnlTaskTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlAddTask
            // 
            this.pnlAddTask.BackColor = System.Drawing.Color.Transparent;
            this.pnlAddTask.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddTask.Controls.Add(this.pnlDeadlinePicker);
            this.pnlAddTask.Controls.Add(this.pnlBody);
            this.pnlAddTask.Controls.Add(this.panel1);
            this.pnlAddTask.Controls.Add(this.btnAddTask);
            this.pnlAddTask.Controls.Add(this.btnCancel);
            this.pnlAddTask.Controls.Add(this.btnClose);
            this.pnlAddTask.Controls.Add(this.lblTitle);
            this.pnlAddTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAddTask.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAddTask.Location = new System.Drawing.Point(0, 0);
            this.pnlAddTask.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAddTask.Name = "pnlAddTask";
            this.pnlAddTask.Size = new System.Drawing.Size(487, 390);
            this.pnlAddTask.TabIndex = 0;
            this.pnlAddTask.Click += new System.EventHandler(this.pnlAddTask_Click);
            // 
            // pnlDeadlinePicker
            // 
            this.pnlDeadlinePicker.BackColor = System.Drawing.Color.Transparent;
            this.pnlDeadlinePicker.Controls.Add(this.monthCalendar1);
            this.pnlDeadlinePicker.Location = new System.Drawing.Point(164, 185);
            this.pnlDeadlinePicker.Name = "pnlDeadlinePicker";
            this.pnlDeadlinePicker.Size = new System.Drawing.Size(301, 199);
            this.pnlDeadlinePicker.TabIndex = 22;
            this.pnlDeadlinePicker.Visible = false;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(0, 0);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 0;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Controls.Add(this.label3);
            this.pnlBody.Controls.Add(this.label1);
            this.pnlBody.Controls.Add(this.lblStart);
            this.pnlBody.Controls.Add(this.pnlDeadline);
            this.pnlBody.Controls.Add(this.pnlStatus);
            this.pnlBody.Controls.Add(this.pnlPriority);
            this.pnlBody.Controls.Add(this.pnlTaskTitle);
            this.pnlBody.Controls.Add(this.lblDeadline);
            this.pnlBody.Controls.Add(this.lblStatus);
            this.pnlBody.Controls.Add(this.lblPriority);
            this.pnlBody.Controls.Add(this.lblTaskTitle);
            this.pnlBody.Location = new System.Drawing.Point(19, 65);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(450, 248);
            this.pnlBody.TabIndex = 23;
            this.pnlBody.Click += new System.EventHandler(this.pnlBody_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(107, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 23);
            this.label3.TabIndex = 20;
            this.label3.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(94, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 23);
            this.label1.TabIndex = 18;
            this.label1.Text = "*";
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStart.ForeColor = System.Drawing.Color.Red;
            this.lblStart.Location = new System.Drawing.Point(107, 20);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(18, 23);
            this.lblStart.TabIndex = 17;
            this.lblStart.Text = "*";
            // 
            // pnlDeadline
            // 
            this.pnlDeadline.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlDeadline.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDeadline.Controls.Add(this.btnCalendar);
            this.pnlDeadline.Controls.Add(this.txtDeadline);
            this.pnlDeadline.Location = new System.Drawing.Point(139, 192);
            this.pnlDeadline.Name = "pnlDeadline";
            this.pnlDeadline.Size = new System.Drawing.Size(295, 31);
            this.pnlDeadline.TabIndex = 15;
            // 
            // btnCalendar
            // 
            this.btnCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalendar.FlatAppearance.BorderSize = 0;
            this.btnCalendar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalendar.Image = global::PersonalExpenseCreditTracker.Properties.Resources.calendar__1_;
            this.btnCalendar.Location = new System.Drawing.Point(256, 0);
            this.btnCalendar.Name = "btnCalendar";
            this.btnCalendar.Size = new System.Drawing.Size(34, 29);
            this.btnCalendar.TabIndex = 1;
            this.btnCalendar.UseVisualStyleBackColor = true;
            this.btnCalendar.Click += new System.EventHandler(this.btnCalendar_Click);
            // 
            // txtDeadline
            // 
            this.txtDeadline.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtDeadline.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeadline.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeadline.Location = new System.Drawing.Point(5, 2);
            this.txtDeadline.Name = "txtDeadline";
            this.txtDeadline.Size = new System.Drawing.Size(235, 24);
            this.txtDeadline.TabIndex = 0;
            this.txtDeadline.Click += new System.EventHandler(this.txtDeadline_Click);
            this.txtDeadline.TextChanged += new System.EventHandler(this.txtDeadline_TextChanged);
            this.txtDeadline.Enter += new System.EventHandler(this.txtDeadline_Enter);
            this.txtDeadline.Leave += new System.EventHandler(this.txtDeadline_Leave);
            // 
            // pnlStatus
            // 
            this.pnlStatus.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatus.Controls.Add(this.pictureBox6);
            this.pnlStatus.Controls.Add(this.lvlStatus);
            this.pnlStatus.Location = new System.Drawing.Point(139, 132);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(295, 31);
            this.pnlStatus.TabIndex = 14;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox6.Image = global::PersonalExpenseCreditTracker.Properties.Resources.padlock__5_;
            this.pictureBox6.Location = new System.Drawing.Point(265, 2);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(25, 26);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 35;
            this.pictureBox6.TabStop = false;
            // 
            // lvlStatus
            // 
            this.lvlStatus.AutoSize = true;
            this.lvlStatus.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvlStatus.Location = new System.Drawing.Point(4, 3);
            this.lvlStatus.Name = "lvlStatus";
            this.lvlStatus.Size = new System.Drawing.Size(72, 23);
            this.lvlStatus.TabIndex = 0;
            this.lvlStatus.Text = "Pending";
            // 
            // pnlPriority
            // 
            this.pnlPriority.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlPriority.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPriority.Controls.Add(this.cmbPriority);
            this.pnlPriority.Location = new System.Drawing.Point(139, 76);
            this.pnlPriority.Name = "pnlPriority";
            this.pnlPriority.Size = new System.Drawing.Size(295, 31);
            this.pnlPriority.TabIndex = 13;
            // 
            // cmbPriority
            // 
            this.cmbPriority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPriority.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbPriority.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPriority.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPriority.FormattingEnabled = true;
            this.cmbPriority.Location = new System.Drawing.Point(4, -1);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Size = new System.Drawing.Size(290, 31);
            this.cmbPriority.TabIndex = 0;
            this.cmbPriority.SelectedIndexChanged += new System.EventHandler(this.cmbPriority_SelectedIndexChanged);
            this.cmbPriority.TextChanged += new System.EventHandler(this.cmbPriority_TextChanged);
            this.cmbPriority.Click += new System.EventHandler(this.cmbPriority_Click);
            this.cmbPriority.Enter += new System.EventHandler(this.cmbPriority_Enter);
            this.cmbPriority.Leave += new System.EventHandler(this.cmbPriority_Leave);
            // 
            // pnlTaskTitle
            // 
            this.pnlTaskTitle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTaskTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTaskTitle.Controls.Add(this.txtTaskTitle);
            this.pnlTaskTitle.Location = new System.Drawing.Point(139, 20);
            this.pnlTaskTitle.Name = "pnlTaskTitle";
            this.pnlTaskTitle.Size = new System.Drawing.Size(295, 31);
            this.pnlTaskTitle.TabIndex = 12;
            // 
            // txtTaskTitle
            // 
            this.txtTaskTitle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTaskTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTaskTitle.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaskTitle.Location = new System.Drawing.Point(4, 3);
            this.txtTaskTitle.Name = "txtTaskTitle";
            this.txtTaskTitle.Size = new System.Drawing.Size(286, 24);
            this.txtTaskTitle.TabIndex = 3;
            this.txtTaskTitle.TextChanged += new System.EventHandler(this.txtTaskTitle_TextChanged);
            this.txtTaskTitle.Enter += new System.EventHandler(this.txtTaskTitle_Enter);
            this.txtTaskTitle.Leave += new System.EventHandler(this.txtTaskTitle_Leave);
            // 
            // lblDeadline
            // 
            this.lblDeadline.AutoSize = true;
            this.lblDeadline.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeadline.Location = new System.Drawing.Point(13, 193);
            this.lblDeadline.Name = "lblDeadline";
            this.lblDeadline.Size = new System.Drawing.Size(88, 25);
            this.lblDeadline.TabIndex = 8;
            this.lblDeadline.Text = "Deadline";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(13, 133);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(64, 25);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status";
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriority.Location = new System.Drawing.Point(13, 77);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(75, 25);
            this.lblPriority.TabIndex = 4;
            this.lblPriority.Text = "Priority";
            // 
            // lblTaskTitle
            // 
            this.lblTaskTitle.AutoSize = true;
            this.lblTaskTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaskTitle.Location = new System.Drawing.Point(13, 21);
            this.lblTaskTitle.Name = "lblTaskTitle";
            this.lblTaskTitle.Size = new System.Drawing.Size(95, 25);
            this.lblTaskTitle.TabIndex = 2;
            this.lblTaskTitle.Text = "Task Title ";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(19, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 1);
            this.panel1.TabIndex = 21;
            // 
            // btnAddTask
            // 
            this.btnAddTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnAddTask.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddTask.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnAddTask.FlatAppearance.BorderSize = 0;
            this.btnAddTask.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnAddTask.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnAddTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTask.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddTask.ForeColor = System.Drawing.Color.White;
            this.btnAddTask.Image = global::PersonalExpenseCreditTracker.Properties.Resources.add__2_;
            this.btnAddTask.Location = new System.Drawing.Point(319, 330);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.btnAddTask.Size = new System.Drawing.Size(146, 41);
            this.btnAddTask.TabIndex = 11;
            this.btnAddTask.Text = "Add Task";
            this.btnAddTask.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddTask.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddTask.UseVisualStyleBackColor = false;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(91)))));
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(187, 330);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 41);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Image = global::PersonalExpenseCreditTracker.Properties.Resources.close;
            this.btnClose.Location = new System.Drawing.Point(436, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(39, 35);
            this.btnClose.TabIndex = 1;
            this.btnClose.UseVisualStyleBackColor = false;
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
            this.lblTitle.Size = new System.Drawing.Size(146, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add New Task";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // AddTaskControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(487, 390);
            this.ControlBox = false;
            this.Controls.Add(this.pnlAddTask);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTaskControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New Task";
            this.Load += new System.EventHandler(this.AddTaskControl_Load);
            this.pnlAddTask.ResumeLayout(false);
            this.pnlAddTask.PerformLayout();
            this.pnlDeadlinePicker.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.pnlDeadline.ResumeLayout(false);
            this.pnlDeadline.PerformLayout();
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.pnlPriority.ResumeLayout(false);
            this.pnlTaskTitle.ResumeLayout(false);
            this.pnlTaskTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlAddTask;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTaskTitle;
        private System.Windows.Forms.Label lblTaskTitle;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblDeadline;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.Panel pnlTaskTitle;
        private System.Windows.Forms.Panel pnlPriority;
        private System.Windows.Forms.ComboBox cmbPriority;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Panel pnlDeadline;
        private System.Windows.Forms.Button btnCalendar;
        private System.Windows.Forms.TextBox txtDeadline;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel pnlDeadlinePicker;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Label lvlStatus;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.PictureBox pictureBox6;
    }
}