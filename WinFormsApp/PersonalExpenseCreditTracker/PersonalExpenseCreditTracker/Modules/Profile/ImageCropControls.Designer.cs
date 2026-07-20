namespace PersonalExpenseCreditTracker.Modules.Profile
{
    partial class ImageCropControls
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
            this.pnlImageCrop = new System.Windows.Forms.Panel();
            this.btnCrop = new System.Windows.Forms.Button();
            this.ImgBoxCrop = new Cyotek.Windows.Forms.ImageBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCropImageCancel = new System.Windows.Forms.Button();
            this.pnlImageCrop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlImageCrop
            // 
            this.pnlImageCrop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlImageCrop.Controls.Add(this.label1);
            this.pnlImageCrop.Controls.Add(this.btnCropImageCancel);
            this.pnlImageCrop.Controls.Add(this.btnCrop);
            this.pnlImageCrop.Controls.Add(this.ImgBoxCrop);
            this.pnlImageCrop.Location = new System.Drawing.Point(2, 2);
            this.pnlImageCrop.Name = "pnlImageCrop";
            this.pnlImageCrop.Size = new System.Drawing.Size(988, 734);
            this.pnlImageCrop.TabIndex = 0;
            this.pnlImageCrop.Resize += new System.EventHandler(this.pnlImageCrop_Resize);
            // 
            // btnCrop
            // 
            this.btnCrop.BackColor = System.Drawing.Color.Silver;
            this.btnCrop.FlatAppearance.BorderSize = 0;
            this.btnCrop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrop.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrop.Location = new System.Drawing.Point(591, 672);
            this.btnCrop.Name = "btnCrop";
            this.btnCrop.Size = new System.Drawing.Size(169, 43);
            this.btnCrop.TabIndex = 1;
            this.btnCrop.Text = "Crop";
            this.btnCrop.UseVisualStyleBackColor = false;
            this.btnCrop.Click += new System.EventHandler(this.btnCrop_Click);
            this.btnCrop.Resize += new System.EventHandler(this.btnCrop_Resize);
            // 
            // ImgBoxCrop
            // 
            this.ImgBoxCrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImgBoxCrop.Cursor = System.Windows.Forms.Cursors.Cross;
            this.ImgBoxCrop.ImageBorderColor = System.Drawing.Color.Black;
            this.ImgBoxCrop.Location = new System.Drawing.Point(2, 52);
            this.ImgBoxCrop.Name = "ImgBoxCrop";
            this.ImgBoxCrop.Size = new System.Drawing.Size(983, 602);
            this.ImgBoxCrop.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(351, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Drag to adjust • Scroll to zoom";
            // 
            // btnCropImageCancel
            // 
            this.btnCropImageCancel.BackColor = System.Drawing.Color.Silver;
            this.btnCropImageCancel.FlatAppearance.BorderSize = 0;
            this.btnCropImageCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCropImageCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCropImageCancel.Location = new System.Drawing.Point(795, 672);
            this.btnCropImageCancel.Name = "btnCropImageCancel";
            this.btnCropImageCancel.Size = new System.Drawing.Size(169, 43);
            this.btnCropImageCancel.TabIndex = 1;
            this.btnCropImageCancel.Text = "Cancel";
            this.btnCropImageCancel.UseVisualStyleBackColor = false;
            this.btnCropImageCancel.Click += new System.EventHandler(this.btnCropImageCancel_Click);
            this.btnCropImageCancel.Resize += new System.EventHandler(this.btnCropImageCancel_Resize);
            // 
            // ImageCropControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(992, 738);
            this.Controls.Add(this.pnlImageCrop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ImageCropControls";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImageCropControls";
            this.Load += new System.EventHandler(this.ImageCropControls_Load);
            this.pnlImageCrop.ResumeLayout(false);
            this.pnlImageCrop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlImageCrop;
        private System.Windows.Forms.Button btnCrop;
        protected internal Cyotek.Windows.Forms.ImageBox ImgBoxCrop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCropImageCancel;
    }
}