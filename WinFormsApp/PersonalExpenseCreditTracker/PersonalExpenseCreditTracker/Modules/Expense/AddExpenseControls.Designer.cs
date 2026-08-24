namespace PersonalExpenseCreditTracker.Modules.Expense
{
    partial class AddExpenseControls
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
            this.panelExpenseDetailsMainBody = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAddExpenseDescription = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblAddExpenseDescription = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtAddExpenseAmount = new System.Windows.Forms.TextBox();
            this.pictureBoxLentRupee = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblAddExpenseAmount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblAddExpenseSubCategory = new System.Windows.Forms.Label();
            this.cmbAddExpenseSubCategory = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAddExpensePaymentType = new System.Windows.Forms.Label();
            this.cmbAddExpensePaymentType = new System.Windows.Forms.ComboBox();
            this.lblRedStar = new System.Windows.Forms.Label();
            this.lblAddExpenseCategory = new System.Windows.Forms.Label();
            this.cmbAddExpenseCategory = new System.Windows.Forms.ComboBox();
            this.btnSaveExpense = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelExpenseDetailsMainBody.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLentRupee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelExpenseDetailsMainBody
            // 
            this.panelExpenseDetailsMainBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.panelExpenseDetailsMainBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelExpenseDetailsMainBody.Controls.Add(this.label2);
            this.panelExpenseDetailsMainBody.Controls.Add(this.txtAddExpenseDescription);
            this.panelExpenseDetailsMainBody.Controls.Add(this.label9);
            this.panelExpenseDetailsMainBody.Controls.Add(this.lblAddExpenseDescription);
            this.panelExpenseDetailsMainBody.Controls.Add(this.panel3);
            this.panelExpenseDetailsMainBody.Controls.Add(this.label7);
            this.panelExpenseDetailsMainBody.Controls.Add(this.lblAddExpenseAmount);
            this.panelExpenseDetailsMainBody.Controls.Add(this.label5);
            this.panelExpenseDetailsMainBody.Controls.Add(this.lblAddExpenseSubCategory);
            this.panelExpenseDetailsMainBody.Controls.Add(this.cmbAddExpenseSubCategory);
            this.panelExpenseDetailsMainBody.Controls.Add(this.label3);
            this.panelExpenseDetailsMainBody.Controls.Add(this.lblAddExpensePaymentType);
            this.panelExpenseDetailsMainBody.Controls.Add(this.cmbAddExpensePaymentType);
            this.panelExpenseDetailsMainBody.Controls.Add(this.lblRedStar);
            this.panelExpenseDetailsMainBody.Controls.Add(this.lblAddExpenseCategory);
            this.panelExpenseDetailsMainBody.Controls.Add(this.cmbAddExpenseCategory);
            this.panelExpenseDetailsMainBody.Controls.Add(this.btnSaveExpense);
            this.panelExpenseDetailsMainBody.Controls.Add(this.btnCancel);
            this.panelExpenseDetailsMainBody.Controls.Add(this.btnClear);
            this.panelExpenseDetailsMainBody.Controls.Add(this.panel2);
            this.panelExpenseDetailsMainBody.Controls.Add(this.panel1);
            this.panelExpenseDetailsMainBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExpenseDetailsMainBody.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelExpenseDetailsMainBody.Location = new System.Drawing.Point(0, 0);
            this.panelExpenseDetailsMainBody.Name = "panelExpenseDetailsMainBody";
            this.panelExpenseDetailsMainBody.Padding = new System.Windows.Forms.Padding(15);
            this.panelExpenseDetailsMainBody.Size = new System.Drawing.Size(568, 582);
            this.panelExpenseDetailsMainBody.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(15, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 28);
            this.label2.TabIndex = 32;
            this.label2.Text = "Expense Details";
            // 
            // txtAddExpenseDescription
            // 
            this.txtAddExpenseDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddExpenseDescription.Location = new System.Drawing.Point(186, 334);
            this.txtAddExpenseDescription.Multiline = true;
            this.txtAddExpenseDescription.Name = "txtAddExpenseDescription";
            this.txtAddExpenseDescription.Size = new System.Drawing.Size(354, 150);
            this.txtAddExpenseDescription.TabIndex = 4;
            this.txtAddExpenseDescription.TextChanged += new System.EventHandler(this.txtAddExpenseDescription_TextChanged);
            this.txtAddExpenseDescription.Enter += new System.EventHandler(this.txtDescription_Enter);
            this.txtAddExpenseDescription.Leave += new System.EventHandler(this.txtDescription_Leave);
            // 
            // label9
            // 
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(118, 333);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 21);
            this.label9.TabIndex = 30;
            this.label9.Text = "*";
            // 
            // lblAddExpenseDescription
            // 
            this.lblAddExpenseDescription.Location = new System.Drawing.Point(16, 334);
            this.lblAddExpenseDescription.Margin = new System.Windows.Forms.Padding(0);
            this.lblAddExpenseDescription.Name = "lblAddExpenseDescription";
            this.lblAddExpenseDescription.Size = new System.Drawing.Size(123, 30);
            this.lblAddExpenseDescription.TabIndex = 29;
            this.lblAddExpenseDescription.Text = "Description";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtAddExpenseAmount);
            this.panel3.Controls.Add(this.pictureBoxLentRupee);
            this.panel3.Location = new System.Drawing.Point(186, 203);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(354, 31);
            this.panel3.TabIndex = 2;
            // 
            // txtAddExpenseAmount
            // 
            this.txtAddExpenseAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddExpenseAmount.Location = new System.Drawing.Point(46, 3);
            this.txtAddExpenseAmount.Name = "txtAddExpenseAmount";
            this.txtAddExpenseAmount.Size = new System.Drawing.Size(304, 23);
            this.txtAddExpenseAmount.TabIndex = 1;
            this.txtAddExpenseAmount.TextChanged += new System.EventHandler(this.txtAddExpenseAmount_TextChanged);
            this.txtAddExpenseAmount.Enter += new System.EventHandler(this.txtAmount_Enter);
            this.txtAddExpenseAmount.Leave += new System.EventHandler(this.txtAmount_Leave);
            // 
            // pictureBoxLentRupee
            // 
            this.pictureBoxLentRupee.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBoxLentRupee.ErrorImage = global::PersonalExpenseCreditTracker.Properties.Resources.rupee;
            this.pictureBoxLentRupee.Image = global::PersonalExpenseCreditTracker.Properties.Resources.rupee;
            this.pictureBoxLentRupee.Location = new System.Drawing.Point(-1, -1);
            this.pictureBoxLentRupee.Name = "pictureBoxLentRupee";
            this.pictureBoxLentRupee.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.pictureBoxLentRupee.Size = new System.Drawing.Size(43, 32);
            this.pictureBoxLentRupee.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxLentRupee.TabIndex = 0;
            this.pictureBoxLentRupee.TabStop = false;
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(94, 205);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 21);
            this.label7.TabIndex = 24;
            this.label7.Text = "*";
            // 
            // lblAddExpenseAmount
            // 
            this.lblAddExpenseAmount.Location = new System.Drawing.Point(16, 205);
            this.lblAddExpenseAmount.Margin = new System.Windows.Forms.Padding(0);
            this.lblAddExpenseAmount.Name = "lblAddExpenseAmount";
            this.lblAddExpenseAmount.Size = new System.Drawing.Size(123, 30);
            this.lblAddExpenseAmount.TabIndex = 23;
            this.lblAddExpenseAmount.Text = "Amount";
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(136, 138);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 21);
            this.label5.TabIndex = 22;
            this.label5.Text = "*";
            // 
            // lblAddExpenseSubCategory
            // 
            this.lblAddExpenseSubCategory.Location = new System.Drawing.Point(16, 139);
            this.lblAddExpenseSubCategory.Margin = new System.Windows.Forms.Padding(0);
            this.lblAddExpenseSubCategory.Name = "lblAddExpenseSubCategory";
            this.lblAddExpenseSubCategory.Size = new System.Drawing.Size(141, 30);
            this.lblAddExpenseSubCategory.TabIndex = 21;
            this.lblAddExpenseSubCategory.Text = "Sub Category";
            // 
            // cmbAddExpenseSubCategory
            // 
            this.cmbAddExpenseSubCategory.FormattingEnabled = true;
            this.cmbAddExpenseSubCategory.IntegralHeight = false;
            this.cmbAddExpenseSubCategory.ItemHeight = 23;
            this.cmbAddExpenseSubCategory.Location = new System.Drawing.Point(186, 136);
            this.cmbAddExpenseSubCategory.Name = "cmbAddExpenseSubCategory";
            this.cmbAddExpenseSubCategory.Size = new System.Drawing.Size(354, 31);
            this.cmbAddExpenseSubCategory.TabIndex = 1;
            this.cmbAddExpenseSubCategory.SelectedIndexChanged += new System.EventHandler(this.cmbAddExpenseSubCategory_SelectedIndexChanged);
            this.cmbAddExpenseSubCategory.TextChanged += new System.EventHandler(this.cmbAddExpenseSubCategory_TextChanged);
            this.cmbAddExpenseSubCategory.Click += new System.EventHandler(this.cmbAddExpenseSubCategory_Click);
            this.cmbAddExpenseSubCategory.Enter += new System.EventHandler(this.cmbAddExpenseSubCategory_Enter);
            this.cmbAddExpenseSubCategory.Leave += new System.EventHandler(this.cmbAddExpenseSubCategory_Leave);
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(139, 269);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 21);
            this.label3.TabIndex = 19;
            this.label3.Text = "*";
            // 
            // lblAddExpensePaymentType
            // 
            this.lblAddExpensePaymentType.Location = new System.Drawing.Point(16, 271);
            this.lblAddExpensePaymentType.Margin = new System.Windows.Forms.Padding(0);
            this.lblAddExpensePaymentType.Name = "lblAddExpensePaymentType";
            this.lblAddExpensePaymentType.Size = new System.Drawing.Size(133, 30);
            this.lblAddExpensePaymentType.TabIndex = 18;
            this.lblAddExpensePaymentType.Text = "Payment Type";
            // 
            // cmbAddExpensePaymentType
            // 
            this.cmbAddExpensePaymentType.FormattingEnabled = true;
            this.cmbAddExpensePaymentType.IntegralHeight = false;
            this.cmbAddExpensePaymentType.ItemHeight = 23;
            this.cmbAddExpensePaymentType.Items.AddRange(new object[] {
            "Select Payment Type",
            "Cash",
            "UPI",
            "Credit Card"});
            this.cmbAddExpensePaymentType.Location = new System.Drawing.Point(186, 270);
            this.cmbAddExpensePaymentType.Name = "cmbAddExpensePaymentType";
            this.cmbAddExpensePaymentType.Size = new System.Drawing.Size(354, 31);
            this.cmbAddExpensePaymentType.TabIndex = 3;
            this.cmbAddExpensePaymentType.SelectedIndexChanged += new System.EventHandler(this.cmbAddExpensePaymentType_SelectedIndexChanged);
            this.cmbAddExpensePaymentType.TextChanged += new System.EventHandler(this.cmbAddExpensePaymentType_TextChanged);
            this.cmbAddExpensePaymentType.Enter += new System.EventHandler(this.cmbAddExpensePaymentType_Enter);
            this.cmbAddExpensePaymentType.Leave += new System.EventHandler(this.cmbAddExpensePaymentType_Leave);
            // 
            // lblRedStar
            // 
            this.lblRedStar.ForeColor = System.Drawing.Color.Red;
            this.lblRedStar.Location = new System.Drawing.Point(99, 72);
            this.lblRedStar.Margin = new System.Windows.Forms.Padding(0);
            this.lblRedStar.Name = "lblRedStar";
            this.lblRedStar.Size = new System.Drawing.Size(46, 21);
            this.lblRedStar.TabIndex = 16;
            this.lblRedStar.Text = "*";
            // 
            // lblAddExpenseCategory
            // 
            this.lblAddExpenseCategory.Location = new System.Drawing.Point(16, 73);
            this.lblAddExpenseCategory.Margin = new System.Windows.Forms.Padding(0);
            this.lblAddExpenseCategory.Name = "lblAddExpenseCategory";
            this.lblAddExpenseCategory.Size = new System.Drawing.Size(123, 30);
            this.lblAddExpenseCategory.TabIndex = 15;
            this.lblAddExpenseCategory.Text = "Category";
            // 
            // cmbAddExpenseCategory
            // 
            this.cmbAddExpenseCategory.FormattingEnabled = true;
            this.cmbAddExpenseCategory.IntegralHeight = false;
            this.cmbAddExpenseCategory.ItemHeight = 23;
            this.cmbAddExpenseCategory.Location = new System.Drawing.Point(186, 73);
            this.cmbAddExpenseCategory.Name = "cmbAddExpenseCategory";
            this.cmbAddExpenseCategory.Size = new System.Drawing.Size(354, 31);
            this.cmbAddExpenseCategory.TabIndex = 0;
            this.cmbAddExpenseCategory.SelectedIndexChanged += new System.EventHandler(this.cmbAddExpenseCategory_SelectedIndexChanged);
            this.cmbAddExpenseCategory.TextChanged += new System.EventHandler(this.cmbAddExpenseCategory_TextChanged);
            this.cmbAddExpenseCategory.Click += new System.EventHandler(this.cmbAddExpenseCategory_Click);
            this.cmbAddExpenseCategory.Enter += new System.EventHandler(this.cmbAddExpenseCategory_Enter);
            this.cmbAddExpenseCategory.Leave += new System.EventHandler(this.cmbAddExpenseCategory_Leave);
            // 
            // btnSaveExpense
            // 
            this.btnSaveExpense.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSaveExpense.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnSaveExpense.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnSaveExpense.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnSaveExpense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveExpense.ForeColor = System.Drawing.Color.White;
            this.btnSaveExpense.Image = global::PersonalExpenseCreditTracker.Properties.Resources.add__2_;
            this.btnSaveExpense.Location = new System.Drawing.Point(351, 525);
            this.btnSaveExpense.Name = "btnSaveExpense";
            this.btnSaveExpense.Size = new System.Drawing.Size(189, 41);
            this.btnSaveExpense.TabIndex = 7;
            this.btnSaveExpense.Text = "Add  Expense";
            this.btnSaveExpense.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveExpense.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveExpense.UseVisualStyleBackColor = false;
            this.btnSaveExpense.Click += new System.EventHandler(this.btnSaveExpense_Click);
            this.btnSaveExpense.Resize += new System.EventHandler(this.btnSaveExpense_Resize);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(90)))));
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(199, 525);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 41);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = " Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.Resize += new System.EventHandler(this.btnCancel_Resize);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Image = global::PersonalExpenseCreditTracker.Properties.Resources.redownload;
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClear.Location = new System.Drawing.Point(18, 525);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(133, 41);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "  Clear";
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            this.btnClear.Resize += new System.EventHandler(this.btnLentAddClear_Resize);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(19, 510);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(528, 1);
            this.panel2.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(19, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(528, 1);
            this.panel1.TabIndex = 1;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // AddExpenseControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 582);
            this.Controls.Add(this.panelExpenseDetailsMainBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AddExpenseControls";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExpenseDetailsControl";
            this.Load += new System.EventHandler(this.ExpenseDetailsControl_Load);
            this.panelExpenseDetailsMainBody.ResumeLayout(false);
            this.panelExpenseDetailsMainBody.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLentRupee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelExpenseDetailsMainBody;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAddExpenseDescription;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblAddExpenseDescription;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtAddExpenseAmount;
        private System.Windows.Forms.PictureBox pictureBoxLentRupee;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblAddExpenseAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblAddExpenseSubCategory;
        private System.Windows.Forms.ComboBox cmbAddExpenseSubCategory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAddExpensePaymentType;
        private System.Windows.Forms.ComboBox cmbAddExpensePaymentType;
        private System.Windows.Forms.Label lblRedStar;
        private System.Windows.Forms.Label lblAddExpenseCategory;
        private System.Windows.Forms.ComboBox cmbAddExpenseCategory;
        private System.Windows.Forms.Button btnSaveExpense;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ErrorProvider errorProvider1;

    }
}