namespace PersonalExpenseCreditTracker.Modules.Borrow
{
    partial class AddBorrowControls
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
            this.pnlBorrowAddCalenderShow = new System.Windows.Forms.Panel();
            this.monthCalendarAddBorrow = new System.Windows.Forms.MonthCalendar();
            this.txtBorrowAddDescription = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblBorrowDescription = new System.Windows.Forms.Label();
            this.panelBorrowAddDeadlineAt = new System.Windows.Forms.Panel();
            this.txtBorrowAddDeadlineDatePicker = new System.Windows.Forms.TextBox();
            this.btnBorrowAddCalendar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBorrowDeadlineAt = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtBorrowAddAmount = new System.Windows.Forms.TextBox();
            this.picBorrowRupee = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblLentAmount = new System.Windows.Forms.Label();
            this.lblBorrowStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBorrowPaymentType = new System.Windows.Forms.Label();
            this.cmbBorrowPaymentType = new System.Windows.Forms.ComboBox();
            this.lblRedStar = new System.Windows.Forms.Label();
            this.lblBorrowPersonName = new System.Windows.Forms.Label();
            this.cmbBorrowSelectPerson = new System.Windows.Forms.ComboBox();
            this.btnBorrowAddSave = new System.Windows.Forms.Button();
            this.btnBorrowAddCancel = new System.Windows.Forms.Button();
            this.pnlAddBorrowMainBody = new System.Windows.Forms.Panel();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.txtBorrowStatus = new System.Windows.Forms.TextBox();
            this.btnBorrowAddClear = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBorrowDetails = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlBorrowAddCalenderShow.SuspendLayout();
            this.panelBorrowAddDeadlineAt.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBorrowRupee)).BeginInit();
            this.pnlAddBorrowMainBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBorrowAddCalenderShow
            // 
            this.pnlBorrowAddCalenderShow.BackColor = System.Drawing.Color.White;
            this.pnlBorrowAddCalenderShow.Controls.Add(this.monthCalendarAddBorrow);
            this.pnlBorrowAddCalenderShow.Location = new System.Drawing.Point(224, 370);
            this.pnlBorrowAddCalenderShow.Name = "pnlBorrowAddCalenderShow";
            this.pnlBorrowAddCalenderShow.Size = new System.Drawing.Size(301, 207);
            this.pnlBorrowAddCalenderShow.TabIndex = 32;
            this.pnlBorrowAddCalenderShow.Visible = false;
            // 
            // monthCalendarAddBorrow
            // 
            this.monthCalendarAddBorrow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monthCalendarAddBorrow.Location = new System.Drawing.Point(0, 0);
            this.monthCalendarAddBorrow.Name = "monthCalendarAddBorrow";
            this.monthCalendarAddBorrow.TabIndex = 0;
            this.monthCalendarAddBorrow.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarAddBorrow_DateSelected);
            // 
            // txtBorrowAddDescription
            // 
            this.txtBorrowAddDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBorrowAddDescription.Location = new System.Drawing.Point(186, 399);
            this.txtBorrowAddDescription.Multiline = true;
            this.txtBorrowAddDescription.Name = "txtBorrowAddDescription";
            this.txtBorrowAddDescription.Size = new System.Drawing.Size(354, 178);
            this.txtBorrowAddDescription.TabIndex = 31;
            this.txtBorrowAddDescription.Enter += new System.EventHandler(this.txtBorrowAddDescription_Enter);
            this.txtBorrowAddDescription.Leave += new System.EventHandler(this.txtBorrowAddDescription_Leave);
            // 
            // label9
            // 
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(121, 400);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 21);
            this.label9.TabIndex = 30;
            this.label9.Text = "*";
            // 
            // lblBorrowDescription
            // 
            this.lblBorrowDescription.Location = new System.Drawing.Point(16, 400);
            this.lblBorrowDescription.Margin = new System.Windows.Forms.Padding(0);
            this.lblBorrowDescription.Name = "lblBorrowDescription";
            this.lblBorrowDescription.Size = new System.Drawing.Size(123, 30);
            this.lblBorrowDescription.TabIndex = 29;
            this.lblBorrowDescription.Text = "Description";
            // 
            // panelBorrowAddDeadlineAt
            // 
            this.panelBorrowAddDeadlineAt.BackColor = System.Drawing.Color.White;
            this.panelBorrowAddDeadlineAt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBorrowAddDeadlineAt.Controls.Add(this.txtBorrowAddDeadlineDatePicker);
            this.panelBorrowAddDeadlineAt.Controls.Add(this.btnBorrowAddCalendar);
            this.panelBorrowAddDeadlineAt.Location = new System.Drawing.Point(186, 333);
            this.panelBorrowAddDeadlineAt.Name = "panelBorrowAddDeadlineAt";
            this.panelBorrowAddDeadlineAt.Size = new System.Drawing.Size(354, 31);
            this.panelBorrowAddDeadlineAt.TabIndex = 28;
            // 
            // txtBorrowAddDeadlineDatePicker
            // 
            this.txtBorrowAddDeadlineDatePicker.BackColor = System.Drawing.Color.White;
            this.txtBorrowAddDeadlineDatePicker.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBorrowAddDeadlineDatePicker.Location = new System.Drawing.Point(5, 3);
            this.txtBorrowAddDeadlineDatePicker.Name = "txtBorrowAddDeadlineDatePicker";
            this.txtBorrowAddDeadlineDatePicker.Size = new System.Drawing.Size(283, 23);
            this.txtBorrowAddDeadlineDatePicker.TabIndex = 2;
            this.txtBorrowAddDeadlineDatePicker.TextChanged += new System.EventHandler(this.txtBorrowAddDeadlineDatePicker_TextChanged);
            this.txtBorrowAddDeadlineDatePicker.Enter += new System.EventHandler(this.txtBorrowAddDeadlineDatePicker_Enter);
            this.txtBorrowAddDeadlineDatePicker.Leave += new System.EventHandler(this.txtBorrowAddDeadlineDatePicker_Leave);
            // 
            // btnBorrowAddCalendar
            // 
            this.btnBorrowAddCalendar.FlatAppearance.BorderSize = 0;
            this.btnBorrowAddCalendar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrowAddCalendar.Image = global::PersonalExpenseCreditTracker.Properties.Resources.calendar__1_;
            this.btnBorrowAddCalendar.Location = new System.Drawing.Point(313, -1);
            this.btnBorrowAddCalendar.Name = "btnBorrowAddCalendar";
            this.btnBorrowAddCalendar.Size = new System.Drawing.Size(37, 31);
            this.btnBorrowAddCalendar.TabIndex = 1;
            this.btnBorrowAddCalendar.UseVisualStyleBackColor = true;
            this.btnBorrowAddCalendar.Click += new System.EventHandler(this.btnBorrowAddCalendar_Click);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(126, 333);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 21);
            this.label1.TabIndex = 27;
            this.label1.Text = "*";
            // 
            // lblBorrowDeadlineAt
            // 
            this.lblBorrowDeadlineAt.Location = new System.Drawing.Point(16, 334);
            this.lblBorrowDeadlineAt.Margin = new System.Windows.Forms.Padding(0);
            this.lblBorrowDeadlineAt.Name = "lblBorrowDeadlineAt";
            this.lblBorrowDeadlineAt.Size = new System.Drawing.Size(123, 30);
            this.lblBorrowDeadlineAt.TabIndex = 26;
            this.lblBorrowDeadlineAt.Text = "Deadline At";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtBorrowAddAmount);
            this.panel3.Controls.Add(this.picBorrowRupee);
            this.panel3.Location = new System.Drawing.Point(186, 270);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(354, 32);
            this.panel3.TabIndex = 25;
            // 
            // txtBorrowAddAmount
            // 
            this.txtBorrowAddAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBorrowAddAmount.Location = new System.Drawing.Point(46, 3);
            this.txtBorrowAddAmount.Name = "txtBorrowAddAmount";
            this.txtBorrowAddAmount.Size = new System.Drawing.Size(310, 23);
            this.txtBorrowAddAmount.TabIndex = 1;
            this.txtBorrowAddAmount.Enter += new System.EventHandler(this.txtBorrowAddAmount_Enter);
            this.txtBorrowAddAmount.Leave += new System.EventHandler(this.txtBorrowAddAmount_Leave);
            // 
            // picBorrowRupee
            // 
            this.picBorrowRupee.BackColor = System.Drawing.Color.Gainsboro;
            this.picBorrowRupee.ErrorImage = global::PersonalExpenseCreditTracker.Properties.Resources.rupee;
            this.picBorrowRupee.Image = global::PersonalExpenseCreditTracker.Properties.Resources.rupee;
            this.picBorrowRupee.Location = new System.Drawing.Point(-1, -1);
            this.picBorrowRupee.Name = "picBorrowRupee";
            this.picBorrowRupee.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.picBorrowRupee.Size = new System.Drawing.Size(43, 32);
            this.picBorrowRupee.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBorrowRupee.TabIndex = 0;
            this.picBorrowRupee.TabStop = false;
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(90, 270);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 21);
            this.label7.TabIndex = 24;
            this.label7.Text = "*";
            // 
            // lblLentAmount
            // 
            this.lblLentAmount.Location = new System.Drawing.Point(16, 271);
            this.lblLentAmount.Margin = new System.Windows.Forms.Padding(0);
            this.lblLentAmount.Name = "lblLentAmount";
            this.lblLentAmount.Size = new System.Drawing.Size(123, 30);
            this.lblLentAmount.TabIndex = 23;
            this.lblLentAmount.Text = "Amount";
            // 
            // lblBorrowStatus
            // 
            this.lblBorrowStatus.Location = new System.Drawing.Point(16, 205);
            this.lblBorrowStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lblBorrowStatus.Name = "lblBorrowStatus";
            this.lblBorrowStatus.Size = new System.Drawing.Size(123, 30);
            this.lblBorrowStatus.TabIndex = 21;
            this.lblBorrowStatus.Text = "Status";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(138, 138);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 21);
            this.label3.TabIndex = 19;
            this.label3.Text = "*";
            // 
            // lblBorrowPaymentType
            // 
            this.lblBorrowPaymentType.Location = new System.Drawing.Point(16, 139);
            this.lblBorrowPaymentType.Margin = new System.Windows.Forms.Padding(0);
            this.lblBorrowPaymentType.Name = "lblBorrowPaymentType";
            this.lblBorrowPaymentType.Size = new System.Drawing.Size(133, 30);
            this.lblBorrowPaymentType.TabIndex = 18;
            this.lblBorrowPaymentType.Text = "Payment Type";
            // 
            // cmbBorrowPaymentType
            // 
            this.cmbBorrowPaymentType.FormattingEnabled = true;
            this.cmbBorrowPaymentType.IntegralHeight = false;
            this.cmbBorrowPaymentType.ItemHeight = 23;
            this.cmbBorrowPaymentType.Items.AddRange(new object[] {
            "Cash",
            "UPI",
            "Debit Card",
            "Credit Card"});
            this.cmbBorrowPaymentType.Location = new System.Drawing.Point(186, 138);
            this.cmbBorrowPaymentType.Name = "cmbBorrowPaymentType";
            this.cmbBorrowPaymentType.Size = new System.Drawing.Size(354, 31);
            this.cmbBorrowPaymentType.TabIndex = 17;
            this.cmbBorrowPaymentType.Enter += new System.EventHandler(this.cmbBorrowPaymentType_Enter);
            this.cmbBorrowPaymentType.Leave += new System.EventHandler(this.cmbBorrowPaymentType_Leave);
            // 
            // lblRedStar
            // 
            this.lblRedStar.ForeColor = System.Drawing.Color.Red;
            this.lblRedStar.Location = new System.Drawing.Point(133, 72);
            this.lblRedStar.Margin = new System.Windows.Forms.Padding(0);
            this.lblRedStar.Name = "lblRedStar";
            this.lblRedStar.Size = new System.Drawing.Size(46, 21);
            this.lblRedStar.TabIndex = 16;
            this.lblRedStar.Text = "*";
            // 
            // lblBorrowPersonName
            // 
            this.lblBorrowPersonName.Location = new System.Drawing.Point(16, 73);
            this.lblBorrowPersonName.Margin = new System.Windows.Forms.Padding(0);
            this.lblBorrowPersonName.Name = "lblBorrowPersonName";
            this.lblBorrowPersonName.Size = new System.Drawing.Size(123, 30);
            this.lblBorrowPersonName.TabIndex = 15;
            this.lblBorrowPersonName.Text = "Person Name";
            // 
            // cmbBorrowSelectPerson
            // 
            this.cmbBorrowSelectPerson.FormattingEnabled = true;
            this.cmbBorrowSelectPerson.IntegralHeight = false;
            this.cmbBorrowSelectPerson.ItemHeight = 23;
            this.cmbBorrowSelectPerson.Items.AddRange(new object[] {
            "Akhmal",
            "Sujit",
            "Kushal",
            "Anikat",
            "Arpita",
            "Debajyoti"});
            this.cmbBorrowSelectPerson.Location = new System.Drawing.Point(186, 72);
            this.cmbBorrowSelectPerson.Name = "cmbBorrowSelectPerson";
            this.cmbBorrowSelectPerson.Size = new System.Drawing.Size(354, 31);
            this.cmbBorrowSelectPerson.TabIndex = 14;
            this.cmbBorrowSelectPerson.Enter += new System.EventHandler(this.cmbBorrowSelectPerson_Enter);
            this.cmbBorrowSelectPerson.Leave += new System.EventHandler(this.cmbBorrowSelectPerson_Leave);
            // 
            // btnBorrowAddSave
            // 
            this.btnBorrowAddSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBorrowAddSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnBorrowAddSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnBorrowAddSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnBorrowAddSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnBorrowAddSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrowAddSave.ForeColor = System.Drawing.Color.White;
            this.btnBorrowAddSave.Image = global::PersonalExpenseCreditTracker.Properties.Resources.save__1_;
            this.btnBorrowAddSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBorrowAddSave.Location = new System.Drawing.Point(370, 616);
            this.btnBorrowAddSave.Name = "btnBorrowAddSave";
            this.btnBorrowAddSave.Size = new System.Drawing.Size(177, 41);
            this.btnBorrowAddSave.TabIndex = 8;
            this.btnBorrowAddSave.Text = "  Save Borrow";
            this.btnBorrowAddSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBorrowAddSave.UseVisualStyleBackColor = false;
            this.btnBorrowAddSave.Click += new System.EventHandler(this.btnBorrowAddSave_Click);
            // 
            // btnBorrowAddCancel
            // 
            this.btnBorrowAddCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBorrowAddCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(91)))));
            this.btnBorrowAddCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.btnBorrowAddCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.btnBorrowAddCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.btnBorrowAddCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrowAddCancel.ForeColor = System.Drawing.Color.White;
            this.btnBorrowAddCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBorrowAddCancel.Location = new System.Drawing.Point(213, 616);
            this.btnBorrowAddCancel.Name = "btnBorrowAddCancel";
            this.btnBorrowAddCancel.Size = new System.Drawing.Size(140, 41);
            this.btnBorrowAddCancel.TabIndex = 7;
            this.btnBorrowAddCancel.Text = " Cancel";
            this.btnBorrowAddCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBorrowAddCancel.UseVisualStyleBackColor = false;
            this.btnBorrowAddCancel.Click += new System.EventHandler(this.btnBorrowAddCancel_Click);
            // 
            // pnlAddBorrowMainBody
            // 
            this.pnlAddBorrowMainBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlAddBorrowMainBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddBorrowMainBody.Controls.Add(this.pictureBox6);
            this.pnlAddBorrowMainBody.Controls.Add(this.txtBorrowStatus);
            this.pnlAddBorrowMainBody.Controls.Add(this.pnlBorrowAddCalenderShow);
            this.pnlAddBorrowMainBody.Controls.Add(this.txtBorrowAddDescription);
            this.pnlAddBorrowMainBody.Controls.Add(this.label9);
            this.pnlAddBorrowMainBody.Controls.Add(this.lblBorrowDescription);
            this.pnlAddBorrowMainBody.Controls.Add(this.panelBorrowAddDeadlineAt);
            this.pnlAddBorrowMainBody.Controls.Add(this.label1);
            this.pnlAddBorrowMainBody.Controls.Add(this.lblBorrowDeadlineAt);
            this.pnlAddBorrowMainBody.Controls.Add(this.panel3);
            this.pnlAddBorrowMainBody.Controls.Add(this.label7);
            this.pnlAddBorrowMainBody.Controls.Add(this.lblLentAmount);
            this.pnlAddBorrowMainBody.Controls.Add(this.lblBorrowStatus);
            this.pnlAddBorrowMainBody.Controls.Add(this.label3);
            this.pnlAddBorrowMainBody.Controls.Add(this.lblBorrowPaymentType);
            this.pnlAddBorrowMainBody.Controls.Add(this.cmbBorrowPaymentType);
            this.pnlAddBorrowMainBody.Controls.Add(this.lblRedStar);
            this.pnlAddBorrowMainBody.Controls.Add(this.lblBorrowPersonName);
            this.pnlAddBorrowMainBody.Controls.Add(this.cmbBorrowSelectPerson);
            this.pnlAddBorrowMainBody.Controls.Add(this.btnBorrowAddSave);
            this.pnlAddBorrowMainBody.Controls.Add(this.btnBorrowAddCancel);
            this.pnlAddBorrowMainBody.Controls.Add(this.btnBorrowAddClear);
            this.pnlAddBorrowMainBody.Controls.Add(this.panel2);
            this.pnlAddBorrowMainBody.Controls.Add(this.panel1);
            this.pnlAddBorrowMainBody.Controls.Add(this.lblBorrowDetails);
            this.pnlAddBorrowMainBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAddBorrowMainBody.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAddBorrowMainBody.Location = new System.Drawing.Point(0, 0);
            this.pnlAddBorrowMainBody.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAddBorrowMainBody.Name = "pnlAddBorrowMainBody";
            this.pnlAddBorrowMainBody.Padding = new System.Windows.Forms.Padding(15);
            this.pnlAddBorrowMainBody.Size = new System.Drawing.Size(568, 675);
            this.pnlAddBorrowMainBody.TabIndex = 1;
            this.pnlAddBorrowMainBody.Click += new System.EventHandler(this.pnlAddBorrowMainBody_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox6.Image = global::PersonalExpenseCreditTracker.Properties.Resources.padlock__5_;
            this.pictureBox6.Location = new System.Drawing.Point(500, 206);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(25, 26);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 36;
            this.pictureBox6.TabStop = false;
            // 
            // txtBorrowStatus
            // 
            this.txtBorrowStatus.BackColor = System.Drawing.Color.Gainsboro;
            this.txtBorrowStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBorrowStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtBorrowStatus.Location = new System.Drawing.Point(186, 204);
            this.txtBorrowStatus.Name = "txtBorrowStatus";
            this.txtBorrowStatus.ReadOnly = true;
            this.txtBorrowStatus.Size = new System.Drawing.Size(354, 30);
            this.txtBorrowStatus.TabIndex = 35;
            this.txtBorrowStatus.Text = "  Pending";
            // 
            // btnBorrowAddClear
            // 
            this.btnBorrowAddClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBorrowAddClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnBorrowAddClear.FlatAppearance.BorderSize = 0;
            this.btnBorrowAddClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrowAddClear.Image = global::PersonalExpenseCreditTracker.Properties.Resources.redownload;
            this.btnBorrowAddClear.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBorrowAddClear.Location = new System.Drawing.Point(16, 616);
            this.btnBorrowAddClear.Name = "btnBorrowAddClear";
            this.btnBorrowAddClear.Size = new System.Drawing.Size(133, 41);
            this.btnBorrowAddClear.TabIndex = 6;
            this.btnBorrowAddClear.Text = "  Clear";
            this.btnBorrowAddClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBorrowAddClear.UseVisualStyleBackColor = false;
            this.btnBorrowAddClear.Click += new System.EventHandler(this.btnBorrowAddClear_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(19, 601);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(528, 1);
            this.panel2.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(19, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(528, 1);
            this.panel1.TabIndex = 1;
            // 
            // lblBorrowDetails
            // 
            this.lblBorrowDetails.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBorrowDetails.Location = new System.Drawing.Point(15, 15);
            this.lblBorrowDetails.Name = "lblBorrowDetails";
            this.lblBorrowDetails.Size = new System.Drawing.Size(152, 25);
            this.lblBorrowDetails.TabIndex = 0;
            this.lblBorrowDetails.Text = "Borrow Details";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // AddBorrowControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.ClientSize = new System.Drawing.Size(568, 675);
            this.ControlBox = false;
            this.Controls.Add(this.pnlAddBorrowMainBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddBorrowControls";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AddBorrowControls_Load);
            this.Click += new System.EventHandler(this.btnBorrowAddCalendar_Click);
            this.pnlBorrowAddCalenderShow.ResumeLayout(false);
            this.panelBorrowAddDeadlineAt.ResumeLayout(false);
            this.panelBorrowAddDeadlineAt.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBorrowRupee)).EndInit();
            this.pnlAddBorrowMainBody.ResumeLayout(false);
            this.pnlAddBorrowMainBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBorrowAddCalenderShow;
        private System.Windows.Forms.MonthCalendar monthCalendarAddBorrow;
        private System.Windows.Forms.TextBox txtBorrowAddDescription;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblBorrowDescription;
        private System.Windows.Forms.Panel panelBorrowAddDeadlineAt;
        private System.Windows.Forms.TextBox txtBorrowAddDeadlineDatePicker;
        private System.Windows.Forms.Button btnBorrowAddCalendar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBorrowDeadlineAt;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtBorrowAddAmount;
        private System.Windows.Forms.PictureBox picBorrowRupee;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblLentAmount;
        private System.Windows.Forms.Label lblBorrowStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBorrowPaymentType;
        private System.Windows.Forms.ComboBox cmbBorrowPaymentType;
        private System.Windows.Forms.Label lblRedStar;
        private System.Windows.Forms.Label lblBorrowPersonName;
        private System.Windows.Forms.ComboBox cmbBorrowSelectPerson;
        private System.Windows.Forms.Button btnBorrowAddSave;
        private System.Windows.Forms.Button btnBorrowAddCancel;
        private System.Windows.Forms.Panel pnlAddBorrowMainBody;
        private System.Windows.Forms.Button btnBorrowAddClear;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblBorrowDetails;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.TextBox txtBorrowStatus;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}