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
            this.pnlNoteMain = new System.Windows.Forms.Panel();
            this.pnlNoteHeader = new System.Windows.Forms.Panel();
            this.lblNoteTitle = new System.Windows.Forms.Label();
            this.lblNoteSubtitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PicNoteMenu = new System.Windows.Forms.Button();
            this.picNoteList = new System.Windows.Forms.Button();
            this.pnlNoteMain.SuspendLayout();
            this.pnlNoteHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlNoteMain
            // 
            this.pnlNoteMain.AutoScroll = true;
            this.pnlNoteMain.Controls.Add(this.pnlNoteHeader);
            this.pnlNoteMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNoteMain.Location = new System.Drawing.Point(0, 0);
            this.pnlNoteMain.Name = "pnlNoteMain";
            this.pnlNoteMain.Size = new System.Drawing.Size(1203, 575);
            this.pnlNoteMain.TabIndex = 0;
            // 
            // pnlNoteHeader
            // 
            this.pnlNoteHeader.Controls.Add(this.panel1);
            this.pnlNoteHeader.Controls.Add(this.lblNoteSubtitle);
            this.pnlNoteHeader.Controls.Add(this.lblNoteTitle);
            this.pnlNoteHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNoteHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlNoteHeader.Name = "pnlNoteHeader";
            this.pnlNoteHeader.Size = new System.Drawing.Size(1203, 90);
            this.pnlNoteHeader.TabIndex = 0;
            // 
            // lblNoteTitle
            // 
            this.lblNoteTitle.AutoSize = true;
            this.lblNoteTitle.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoteTitle.ForeColor = System.Drawing.Color.Black;
            this.lblNoteTitle.Location = new System.Drawing.Point(5, 2);
            this.lblNoteTitle.Name = "lblNoteTitle";
            this.lblNoteTitle.Padding = new System.Windows.Forms.Padding(5);
            this.lblNoteTitle.Size = new System.Drawing.Size(92, 42);
            this.lblNoteTitle.TabIndex = 0;
            this.lblNoteTitle.Text = "Notes";
            // 
            // lblNoteSubtitle
            // 
            this.lblNoteSubtitle.AutoSize = true;
            this.lblNoteSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoteSubtitle.ForeColor = System.Drawing.Color.Blue;
            this.lblNoteSubtitle.Location = new System.Drawing.Point(4, 40);
            this.lblNoteSubtitle.Name = "lblNoteSubtitle";
            this.lblNoteSubtitle.Size = new System.Drawing.Size(369, 20);
            this.lblNoteSubtitle.TabIndex = 1;
            this.lblNoteSubtitle.Text = " Capture your thoughts and keep everything organized";
            this.lblNoteSubtitle.Click += new System.EventHandler(this.lblNoteSubtitle_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PicNoteMenu);
            this.panel1.Controls.Add(this.picNoteList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1050, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(153, 90);
            this.panel1.TabIndex = 2;
            // 
            // PicNoteMenu
            // 
            this.PicNoteMenu.Image = global::PersonalExpenseCreditTracker.Properties.Resources.menu1;
            this.PicNoteMenu.Location = new System.Drawing.Point(14, 21);
            this.PicNoteMenu.Name = "PicNoteMenu";
            this.PicNoteMenu.Size = new System.Drawing.Size(48, 40);
            this.PicNoteMenu.TabIndex = 4;
            this.PicNoteMenu.UseVisualStyleBackColor = true;
            this.PicNoteMenu.Click += new System.EventHandler(this.PicNoteMenu_Click);
            // 
            // picNoteList
            // 
            this.picNoteList.Image = global::PersonalExpenseCreditTracker.Properties.Resources.list;
            this.picNoteList.Location = new System.Drawing.Point(73, 22);
            this.picNoteList.Name = "picNoteList";
            this.picNoteList.Size = new System.Drawing.Size(48, 40);
            this.picNoteList.TabIndex = 3;
            this.picNoteList.UseVisualStyleBackColor = true;
            this.picNoteList.Click += new System.EventHandler(this.button1_Click);
            // 
            // NoteControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 575);
            this.Controls.Add(this.pnlNoteMain);
            this.Name = "NoteControl";
            this.Text = "NoteControl";
            this.Load += new System.EventHandler(this.NoteControl_Load);
            this.pnlNoteMain.ResumeLayout(false);
            this.pnlNoteHeader.ResumeLayout(false);
            this.pnlNoteHeader.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlNoteMain;
        private System.Windows.Forms.Panel pnlNoteHeader;
        private System.Windows.Forms.Label lblNoteSubtitle;
        private System.Windows.Forms.Label lblNoteTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button picNoteList;
        private System.Windows.Forms.Button PicNoteMenu;
    }
}