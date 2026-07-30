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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.pnlDeadline = new System.Windows.Forms.Panel();
            this.btnCalendar = new System.Windows.Forms.Button();
            this.txtDeadline = new System.Windows.Forms.TextBox();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.pnlPriority = new System.Windows.Forms.Panel();
            this.cmbPriority = new System.Windows.Forms.ComboBox();
            this.pnlTaskTitle = new System.Windows.Forms.Panel();
            this.txtTaskTitle = new System.Windows.Forms.TextBox();
            this.btnAddTask = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDeadline = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPriority = new System.Windows.Forms.Label();
            this.lblTaskTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlAddTask.SuspendLayout();
            this.pnlDeadlinePicker.SuspendLayout();
            this.pnlDeadline.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            this.pnlPriority.SuspendLayout();
            this.pnlTaskTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlAddTask
            // 
            this.pnlAddTask.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlAddTask.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddTask.Controls.Add(this.pnlDeadlinePicker);
            this.pnlAddTask.Controls.Add(this.panel1);
            this.pnlAddTask.Controls.Add(this.label3);
            this.pnlAddTask.Controls.Add(this.label2);
            this.pnlAddTask.Controls.Add(this.label1);
            this.pnlAddTask.Controls.Add(this.lblStart);
            this.pnlAddTask.Controls.Add(this.pnlDeadline);
            this.pnlAddTask.Controls.Add(this.pnlStatus);
            this.pnlAddTask.Controls.Add(this.pnlPriority);
            this.pnlAddTask.Controls.Add(this.pnlTaskTitle);
            this.pnlAddTask.Controls.Add(this.btnAddTask);
            this.pnlAddTask.Controls.Add(this.btnCancel);
            this.pnlAddTask.Controls.Add(this.lblDeadline);
            this.pnlAddTask.Controls.Add(this.lblStatus);
            this.pnlAddTask.Controls.Add(this.lblPriority);
            this.pnlAddTask.Controls.Add(this.lblTaskTitle);
            this.pnlAddTask.Controls.Add(this.btnClose);
            this.pnlAddTask.Controls.Add(this.lblTitle);
            this.pnlAddTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAddTask.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAddTask.Location = new System.Drawing.Point(0, 0);
            this.pnlAddTask.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAddTask.Name = "pnlAddTask";
            this.pnlAddTask.Size = new System.Drawing.Size(513, 580);
            this.pnlAddTask.TabIndex = 0;
            this.pnlAddTask.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlAddTask_Paint);
            // 
            // pnlDeadlinePicker
            // 
            this.pnlDeadlinePicker.BackColor = System.Drawing.Color.Transparent;
            this.pnlDeadlinePicker.Controls.Add(this.monthCalendar1);
            this.pnlDeadlinePicker.Location = new System.Drawing.Point(215, 175);
            this.pnlDeadlinePicker.Name = "pnlDeadlinePicker";
            this.pnlDeadlinePicker.Size = new System.Drawing.Size(268, 203);
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
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(25, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 1);
            this.panel1.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(123, 337);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 28);
            this.label3.TabIndex = 20;
            this.label3.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(357, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 28);
            this.label2.TabIndex = 19;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(111, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 28);
            this.label1.TabIndex = 18;
            this.label1.Text = "*";
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStart.ForeColor = System.Drawing.Color.Red;
            this.lblStart.Location = new System.Drawing.Point(129, 82);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(21, 28);
            this.lblStart.TabIndex = 17;
            this.lblStart.Text = "*";
            // 
            // pnlDeadline
            // 
            this.pnlDeadline.BackColor = System.Drawing.Color.White;
            this.pnlDeadline.Controls.Add(this.btnCalendar);
            this.pnlDeadline.Controls.Add(this.txtDeadline);
            this.pnlDeadline.Location = new System.Drawing.Point(30, 378);
            this.pnlDeadline.Name = "pnlDeadline";
            this.pnlDeadline.Size = new System.Drawing.Size(453, 52);
            this.pnlDeadline.TabIndex = 15;
            // 
            // btnCalendar
            // 
            this.btnCalendar.FlatAppearance.BorderSize = 0;
            this.btnCalendar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalendar.Image = global::PersonalExpenseCreditTracker.Properties.Resources.calendar__1_;
            this.btnCalendar.Location = new System.Drawing.Point(403, 8);
            this.btnCalendar.Name = "btnCalendar";
            this.btnCalendar.Size = new System.Drawing.Size(32, 32);
            this.btnCalendar.TabIndex = 1;
            this.btnCalendar.UseVisualStyleBackColor = true;
            this.btnCalendar.Click += new System.EventHandler(this.btnCalendar_Click);
            // 
            // txtDeadline
            // 
            this.txtDeadline.BackColor = System.Drawing.Color.White;
            this.txtDeadline.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeadline.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeadline.Location = new System.Drawing.Point(10, 13);
            this.txtDeadline.Name = "txtDeadline";
            this.txtDeadline.Size = new System.Drawing.Size(376, 27);
            this.txtDeadline.TabIndex = 0;
            this.txtDeadline.TextChanged += new System.EventHandler(this.txtDeadline_TextChanged);
            this.txtDeadline.Enter += new System.EventHandler(this.txtDeadline_Enter);
            this.txtDeadline.Leave += new System.EventHandler(this.txtDeadline_Leave);
            // 
            // pnlStatus
            // 
            this.pnlStatus.BackColor = System.Drawing.Color.White;
            this.pnlStatus.Controls.Add(this.cmbStatus);
            this.pnlStatus.Location = new System.Drawing.Point(286, 252);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(197, 52);
            this.pnlStatus.TabIndex = 14;
            // 
            // cmbStatus
            // 
            this.cmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(11, 7);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(172, 36);
            this.cmbStatus.TabIndex = 7;
            // 
            // pnlPriority
            // 
            this.pnlPriority.BackColor = System.Drawing.Color.White;
            this.pnlPriority.Controls.Add(this.cmbPriority);
            this.pnlPriority.Location = new System.Drawing.Point(30, 252);
            this.pnlPriority.Name = "pnlPriority";
            this.pnlPriority.Size = new System.Drawing.Size(197, 52);
            this.pnlPriority.TabIndex = 13;
            // 
            // cmbPriority
            // 
            this.cmbPriority.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPriority.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPriority.FormattingEnabled = true;
            this.cmbPriority.Location = new System.Drawing.Point(12, 8);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Size = new System.Drawing.Size(172, 36);
            this.cmbPriority.TabIndex = 0;
            this.cmbPriority.SelectedIndexChanged += new System.EventHandler(this.cmbPriority_SelectedIndexChanged);
            // 
            // pnlTaskTitle
            // 
            this.pnlTaskTitle.BackColor = System.Drawing.Color.White;
            this.pnlTaskTitle.Controls.Add(this.txtTaskTitle);
            this.pnlTaskTitle.Location = new System.Drawing.Point(30, 129);
            this.pnlTaskTitle.Name = "pnlTaskTitle";
            this.pnlTaskTitle.Size = new System.Drawing.Size(453, 52);
            this.pnlTaskTitle.TabIndex = 12;
            // 
            // txtTaskTitle
            // 
            this.txtTaskTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTaskTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaskTitle.Location = new System.Drawing.Point(12, 13);
            this.txtTaskTitle.Name = "txtTaskTitle";
            this.txtTaskTitle.Size = new System.Drawing.Size(413, 27);
            this.txtTaskTitle.TabIndex = 3;
            this.txtTaskTitle.Enter += new System.EventHandler(this.txtTaskTitle_Enter);
            this.txtTaskTitle.Leave += new System.EventHandler(this.txtTaskTitle_Leave);
            // 
            // btnAddTask
            // 
            this.btnAddTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAddTask.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddTask.FlatAppearance.BorderSize = 0;
            this.btnAddTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTask.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddTask.ForeColor = System.Drawing.Color.White;
            this.btnAddTask.Location = new System.Drawing.Point(286, 487);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(196, 59);
            this.btnAddTask.TabIndex = 11;
            this.btnAddTask.Text = "Add Task";
            this.btnAddTask.UseVisualStyleBackColor = false;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(91)))));
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(33, 487);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(196, 59);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDeadline
            // 
            this.lblDeadline.AutoSize = true;
            this.lblDeadline.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeadline.Location = new System.Drawing.Point(23, 337);
            this.lblDeadline.Name = "lblDeadline";
            this.lblDeadline.Size = new System.Drawing.Size(92, 28);
            this.lblDeadline.TabIndex = 8;
            this.lblDeadline.Text = "Deadline";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(286, 211);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(67, 28);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status";
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriority.Location = new System.Drawing.Point(27, 211);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(77, 28);
            this.lblPriority.TabIndex = 4;
            this.lblPriority.Text = "Priority";
            // 
            // lblTaskTitle
            // 
            this.lblTaskTitle.AutoSize = true;
            this.lblTaskTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaskTitle.Location = new System.Drawing.Point(27, 87);
            this.lblTaskTitle.Name = "lblTaskTitle";
            this.lblTaskTitle.Size = new System.Drawing.Size(102, 28);
            this.lblTaskTitle.TabIndex = 2;
            this.lblTaskTitle.Text = "Task Title ";
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Image = global::PersonalExpenseCreditTracker.Properties.Resources.close;
            this.btnClose.Location = new System.Drawing.Point(450, 19);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(51, 44);
            this.btnClose.TabIndex = 1;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(20, 28);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(176, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add New Task";
            // 
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // AddTaskControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 580);
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
            this.pnlDeadline.ResumeLayout(false);
            this.pnlDeadline.PerformLayout();
            this.pnlStatus.ResumeLayout(false);
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
        private System.Windows.Forms.ComboBox cmbStatus;
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel pnlDeadlinePicker;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
    }
}