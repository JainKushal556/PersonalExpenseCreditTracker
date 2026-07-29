namespace PersonalExpenseCreditTracker.Modules.Note
{
    partial class NoteViewDetailsControl
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
            this.pnlViewNoteDetails = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlViewHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlColor = new System.Windows.Forms.Panel();
            this.lblPriority = new System.Windows.Forms.Label();
            this.pnlseparator5 = new System.Windows.Forms.Panel();
            this.lblUpdatedDate = new System.Windows.Forms.Label();
            this.lblUpdatedCaption = new System.Windows.Forms.Label();
            this.pnlseparator4 = new System.Windows.Forms.Panel();
            this.lblCreatedDate = new System.Windows.Forms.Label();
            this.lblCreatedCaption = new System.Windows.Forms.Label();
            this.pnlseparator3 = new System.Windows.Forms.Panel();
            this.lblColorName = new System.Windows.Forms.Label();
            this.lblDescriptionCaption = new System.Windows.Forms.Label();
            this.pnlColorPreview = new System.Windows.Forms.Panel();
            this.lblColorCaption = new System.Windows.Forms.Label();
            this.pnlseparator2 = new System.Windows.Forms.Panel();
            this.lblPriorityCaption = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.pnlSeperator1 = new System.Windows.Forms.Panel();
            this.lblNoteTitle = new System.Windows.Forms.Label();
            this.lblTitleCaption = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlViewNoteDetails.SuspendLayout();
            this.pnlViewHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlViewNoteDetails
            // 
            this.pnlViewNoteDetails.BackColor = System.Drawing.Color.Transparent;
            this.pnlViewNoteDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlViewNoteDetails.Controls.Add(this.panel1);
            this.pnlViewNoteDetails.Controls.Add(this.btnClose);
            this.pnlViewNoteDetails.Controls.Add(this.btnCancel);
            this.pnlViewNoteDetails.Controls.Add(this.pnlViewHeader);
            this.pnlViewNoteDetails.Controls.Add(this.pnlBody);
            this.pnlViewNoteDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlViewNoteDetails.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlViewNoteDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlViewNoteDetails.Margin = new System.Windows.Forms.Padding(0);
            this.pnlViewNoteDetails.Name = "pnlViewNoteDetails";
            this.pnlViewNoteDetails.Padding = new System.Windows.Forms.Padding(10);
            this.pnlViewNoteDetails.Size = new System.Drawing.Size(503, 595);
            this.pnlViewNoteDetails.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(367, 540);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 41);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = global::PersonalExpenseCreditTracker.Properties.Resources.close;
            this.btnCancel.Location = new System.Drawing.Point(448, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(39, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.btnCancel_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.btnCancel_MouseLeave);
            // 
            // pnlViewHeader
            // 
            this.pnlViewHeader.Controls.Add(this.lblTitle);
            this.pnlViewHeader.Location = new System.Drawing.Point(18, 10);
            this.pnlViewHeader.Name = "pnlViewHeader";
            this.pnlViewHeader.Size = new System.Drawing.Size(196, 40);
            this.pnlViewHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(15, 7);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(111, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "View Note";
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Controls.Add(this.pnlColor);
            this.pnlBody.Controls.Add(this.lblPriority);
            this.pnlBody.Controls.Add(this.pnlseparator5);
            this.pnlBody.Controls.Add(this.lblUpdatedDate);
            this.pnlBody.Controls.Add(this.lblUpdatedCaption);
            this.pnlBody.Controls.Add(this.pnlseparator4);
            this.pnlBody.Controls.Add(this.lblCreatedDate);
            this.pnlBody.Controls.Add(this.lblCreatedCaption);
            this.pnlBody.Controls.Add(this.pnlseparator3);
            this.pnlBody.Controls.Add(this.lblColorName);
            this.pnlBody.Controls.Add(this.lblDescriptionCaption);
            this.pnlBody.Controls.Add(this.pnlColorPreview);
            this.pnlBody.Controls.Add(this.lblColorCaption);
            this.pnlBody.Controls.Add(this.pnlseparator2);
            this.pnlBody.Controls.Add(this.lblPriorityCaption);
            this.pnlBody.Controls.Add(this.lblDescription);
            this.pnlBody.Controls.Add(this.pnlSeperator1);
            this.pnlBody.Controls.Add(this.lblNoteTitle);
            this.pnlBody.Controls.Add(this.lblTitleCaption);
            this.pnlBody.Location = new System.Drawing.Point(15, 68);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(471, 459);
            this.pnlBody.TabIndex = 22;
            // 
            // pnlColor
            // 
            this.pnlColor.BackColor = System.Drawing.Color.Orange;
            this.pnlColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlColor.Location = new System.Drawing.Point(24, 205);
            this.pnlColor.Name = "pnlColor";
            this.pnlColor.Size = new System.Drawing.Size(18, 18);
            this.pnlColor.TabIndex = 21;
            // 
            // lblPriority
            // 
            this.lblPriority.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPriority.AutoSize = true;
            this.lblPriority.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriority.ForeColor = System.Drawing.Color.Orange;
            this.lblPriority.Location = new System.Drawing.Point(48, 204);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(67, 20);
            this.lblPriority.TabIndex = 20;
            this.lblPriority.Text = "Medium";
            // 
            // pnlseparator5
            // 
            this.pnlseparator5.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlseparator5.Location = new System.Drawing.Point(13, 384);
            this.pnlseparator5.Name = "pnlseparator5";
            this.pnlseparator5.Size = new System.Drawing.Size(446, 1);
            this.pnlseparator5.TabIndex = 15;
            // 
            // lblUpdatedDate
            // 
            this.lblUpdatedDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUpdatedDate.AutoSize = true;
            this.lblUpdatedDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdatedDate.ForeColor = System.Drawing.Color.Blue;
            this.lblUpdatedDate.Location = new System.Drawing.Point(25, 423);
            this.lblUpdatedDate.Name = "lblUpdatedDate";
            this.lblUpdatedDate.Size = new System.Drawing.Size(188, 20);
            this.lblUpdatedDate.TabIndex = 17;
            this.lblUpdatedDate.Text = "May 31, 2024   •   10:30 AM";
            // 
            // lblUpdatedCaption
            // 
            this.lblUpdatedCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUpdatedCaption.AutoSize = true;
            this.lblUpdatedCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdatedCaption.Location = new System.Drawing.Point(14, 393);
            this.lblUpdatedCaption.Name = "lblUpdatedCaption";
            this.lblUpdatedCaption.Size = new System.Drawing.Size(111, 23);
            this.lblUpdatedCaption.TabIndex = 16;
            this.lblUpdatedCaption.Text = "Last Updated";
            // 
            // pnlseparator4
            // 
            this.pnlseparator4.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlseparator4.Location = new System.Drawing.Point(13, 307);
            this.pnlseparator4.Name = "pnlseparator4";
            this.pnlseparator4.Size = new System.Drawing.Size(446, 1);
            this.pnlseparator4.TabIndex = 12;
            // 
            // lblCreatedDate
            // 
            this.lblCreatedDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCreatedDate.AutoSize = true;
            this.lblCreatedDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedDate.ForeColor = System.Drawing.Color.Blue;
            this.lblCreatedDate.Location = new System.Drawing.Point(20, 348);
            this.lblCreatedDate.Name = "lblCreatedDate";
            this.lblCreatedDate.Size = new System.Drawing.Size(192, 20);
            this.lblCreatedDate.TabIndex = 14;
            this.lblCreatedDate.Text = " May 31, 2024   •   10:30 AM";
            // 
            // lblCreatedCaption
            // 
            this.lblCreatedCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCreatedCaption.AutoSize = true;
            this.lblCreatedCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedCaption.Location = new System.Drawing.Point(14, 317);
            this.lblCreatedCaption.Name = "lblCreatedCaption";
            this.lblCreatedCaption.Size = new System.Drawing.Size(111, 23);
            this.lblCreatedCaption.TabIndex = 13;
            this.lblCreatedCaption.Text = "Created Date";
            // 
            // pnlseparator3
            // 
            this.pnlseparator3.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlseparator3.Location = new System.Drawing.Point(13, 236);
            this.pnlseparator3.Name = "pnlseparator3";
            this.pnlseparator3.Size = new System.Drawing.Size(446, 1);
            this.pnlseparator3.TabIndex = 8;
            // 
            // lblColorName
            // 
            this.lblColorName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblColorName.AutoSize = true;
            this.lblColorName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColorName.ForeColor = System.Drawing.Color.Red;
            this.lblColorName.Location = new System.Drawing.Point(50, 271);
            this.lblColorName.Name = "lblColorName";
            this.lblColorName.Size = new System.Drawing.Size(36, 20);
            this.lblColorName.TabIndex = 11;
            this.lblColorName.Text = "Red";
            // 
            // lblDescriptionCaption
            // 
            this.lblDescriptionCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDescriptionCaption.AutoSize = true;
            this.lblDescriptionCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescriptionCaption.Location = new System.Drawing.Point(14, 86);
            this.lblDescriptionCaption.Name = "lblDescriptionCaption";
            this.lblDescriptionCaption.Size = new System.Drawing.Size(96, 23);
            this.lblDescriptionCaption.TabIndex = 4;
            this.lblDescriptionCaption.Text = "Description";
            // 
            // pnlColorPreview
            // 
            this.pnlColorPreview.BackColor = System.Drawing.Color.Red;
            this.pnlColorPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlColorPreview.Location = new System.Drawing.Point(24, 272);
            this.pnlColorPreview.Name = "pnlColorPreview";
            this.pnlColorPreview.Size = new System.Drawing.Size(18, 18);
            this.pnlColorPreview.TabIndex = 10;
            // 
            // lblColorCaption
            // 
            this.lblColorCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblColorCaption.AutoSize = true;
            this.lblColorCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColorCaption.Location = new System.Drawing.Point(14, 242);
            this.lblColorCaption.Name = "lblColorCaption";
            this.lblColorCaption.Size = new System.Drawing.Size(94, 23);
            this.lblColorCaption.TabIndex = 9;
            this.lblColorCaption.Text = "Note Color";
            // 
            // pnlseparator2
            // 
            this.pnlseparator2.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlseparator2.Location = new System.Drawing.Point(13, 167);
            this.pnlseparator2.Name = "pnlseparator2";
            this.pnlseparator2.Size = new System.Drawing.Size(446, 1);
            this.pnlseparator2.TabIndex = 6;
            // 
            // lblPriorityCaption
            // 
            this.lblPriorityCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPriorityCaption.AutoSize = true;
            this.lblPriorityCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriorityCaption.Location = new System.Drawing.Point(14, 173);
            this.lblPriorityCaption.Name = "lblPriorityCaption";
            this.lblPriorityCaption.Size = new System.Drawing.Size(65, 23);
            this.lblPriorityCaption.TabIndex = 7;
            this.lblPriorityCaption.Text = "Priority";
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.ForeColor = System.Drawing.Color.Black;
            this.lblDescription.Location = new System.Drawing.Point(24, 119);
            this.lblDescription.MaximumSize = new System.Drawing.Size(420, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(419, 40);
            this.lblDescription.TabIndex = 5;
            this.lblDescription.Text = "Finish the project proposal, review team updates and prepare for tomorrow\'s meeti" +
    "ng.";
            // 
            // pnlSeperator1
            // 
            this.pnlSeperator1.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlSeperator1.Location = new System.Drawing.Point(13, 77);
            this.pnlSeperator1.Name = "pnlSeperator1";
            this.pnlSeperator1.Size = new System.Drawing.Size(446, 1);
            this.pnlSeperator1.TabIndex = 3;
            // 
            // lblNoteTitle
            // 
            this.lblNoteTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNoteTitle.AutoSize = true;
            this.lblNoteTitle.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoteTitle.ForeColor = System.Drawing.Color.Blue;
            this.lblNoteTitle.Location = new System.Drawing.Point(22, 45);
            this.lblNoteTitle.Name = "lblNoteTitle";
            this.lblNoteTitle.Size = new System.Drawing.Size(85, 23);
            this.lblNoteTitle.TabIndex = 2;
            this.lblNoteTitle.Text = "Daily Plan";
            // 
            // lblTitleCaption
            // 
            this.lblTitleCaption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTitleCaption.AutoSize = true;
            this.lblTitleCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleCaption.Location = new System.Drawing.Point(14, 15);
            this.lblTitleCaption.Name = "lblTitleCaption";
            this.lblTitleCaption.Size = new System.Drawing.Size(85, 23);
            this.lblTitleCaption.TabIndex = 1;
            this.lblTitleCaption.Text = "Note Title";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(13, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(475, 1);
            this.panel1.TabIndex = 23;
            // 
            // NoteViewDetailsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(503, 595);
            this.Controls.Add(this.pnlViewNoteDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "NoteViewDetailsControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NoteViewDetailsControl";
            this.Load += new System.EventHandler(this.NoteViewDetailsControl_Load);
            this.pnlViewNoteDetails.ResumeLayout(false);
            this.pnlViewHeader.ResumeLayout(false);
            this.pnlViewHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlViewNoteDetails;
        private System.Windows.Forms.Panel pnlViewHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTitleCaption;
        private System.Windows.Forms.Label lblNoteTitle;
        private System.Windows.Forms.Panel pnlSeperator1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblUpdatedDate;
        private System.Windows.Forms.Label lblUpdatedCaption;
        private System.Windows.Forms.Panel pnlseparator5;
        private System.Windows.Forms.Label lblCreatedDate;
        private System.Windows.Forms.Label lblCreatedCaption;
        private System.Windows.Forms.Panel pnlseparator4;
        private System.Windows.Forms.Label lblColorName;
        private System.Windows.Forms.Panel pnlColorPreview;
        private System.Windows.Forms.Label lblColorCaption;
        private System.Windows.Forms.Panel pnlseparator3;
        private System.Windows.Forms.Label lblPriorityCaption;
        private System.Windows.Forms.Panel pnlseparator2;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblDescriptionCaption;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.Panel pnlColor;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel panel1;
    }
}