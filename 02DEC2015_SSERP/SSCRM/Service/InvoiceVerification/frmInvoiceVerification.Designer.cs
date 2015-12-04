namespace SSCRM
{
    partial class frmInvoiceVerification
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpTrnDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbBranch = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpDocMonth = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCampName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtGLName = new System.Windows.Forms.TextBox();
            this.txtGLEcode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSRName = new System.Windows.Forms.TextBox();
            this.txtSrEcode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnAddVerificationDetl = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbObservType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVerifiedName = new System.Windows.Forms.TextBox();
            this.txtVerifiedBy = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.gvInvVerificationDetl = new System.Windows.Forms.DataGridView();
            this.SlNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Observation_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Observation_Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Del = new System.Windows.Forms.DataGridViewImageColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvInvVerificationDetl)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(398, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "Date";
            // 
            // dtpTrnDate
            // 
            this.dtpTrnDate.CustomFormat = "dd/MM/yyyy";
            this.dtpTrnDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTrnDate.Location = new System.Drawing.Point(443, 79);
            this.dtpTrnDate.Name = "dtpTrnDate";
            this.dtpTrnDate.Size = new System.Drawing.Size(95, 22);
            this.dtpTrnDate.TabIndex = 9;
            this.dtpTrnDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(29, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Company";
            // 
            // cbCompany
            // 
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Items.AddRange(new object[] {
            "                     Select"});
            this.cbCompany.Location = new System.Drawing.Point(106, 24);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(432, 24);
            this.cbCompany.TabIndex = 1;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(43, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Branch";
            // 
            // cbBranch
            // 
            this.cbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranch.FormattingEnabled = true;
            this.cbBranch.Location = new System.Drawing.Point(106, 50);
            this.cbBranch.Name = "cbBranch";
            this.cbBranch.Size = new System.Drawing.Size(432, 24);
            this.cbBranch.TabIndex = 3;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label24.Location = new System.Drawing.Point(207, 82);
            this.label24.Margin = new System.Windows.Forms.Padding(0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(71, 16);
            this.label24.TabIndex = 6;
            this.label24.Text = "Order No";
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderNo.Location = new System.Drawing.Point(281, 79);
            this.txtOrderNo.MaxLength = 20;
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Size = new System.Drawing.Size(108, 22);
            this.txtOrderNo.TabIndex = 7;
            this.txtOrderNo.Validated += new System.EventHandler(this.txtOrderNo_Validated);
            this.txtOrderNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtOrderNo_KeyUp);
            this.txtOrderNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOrderNo_KeyPress);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnClose);
            this.groupBox5.Controls.Add(this.btnCancel);
            this.groupBox5.Controls.Add(this.btnSave);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox5.Location = new System.Drawing.Point(106, 518);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(395, 45);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(237, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 26);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "C&lose";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(160, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Clea&r";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoEllipsis = true;
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(83, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label9.Location = new System.Drawing.Point(23, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 16);
            this.label9.TabIndex = 4;
            this.label9.Text = "Doc Month";
            // 
            // dtpDocMonth
            // 
            this.dtpDocMonth.CustomFormat = "MMM/yyyy";
            this.dtpDocMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDocMonth.Location = new System.Drawing.Point(108, 79);
            this.dtpDocMonth.Name = "dtpDocMonth";
            this.dtpDocMonth.Size = new System.Drawing.Size(91, 22);
            this.dtpDocMonth.TabIndex = 5;
            this.dtpDocMonth.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCampName);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtGLName);
            this.groupBox2.Controls.Add(this.txtGLEcode);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtSRName);
            this.groupBox2.Controls.Add(this.txtSrEcode);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(23, 134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(515, 103);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SR and Camp Details";
            // 
            // txtCampName
            // 
            this.txtCampName.BackColor = System.Drawing.SystemColors.Info;
            this.txtCampName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCampName.Location = new System.Drawing.Point(112, 22);
            this.txtCampName.MaxLength = 50;
            this.txtCampName.Name = "txtCampName";
            this.txtCampName.ReadOnly = true;
            this.txtCampName.Size = new System.Drawing.Size(220, 22);
            this.txtCampName.TabIndex = 1;
            this.txtCampName.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(17, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "Camp Name";
            // 
            // txtGLName
            // 
            this.txtGLName.BackColor = System.Drawing.SystemColors.Info;
            this.txtGLName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGLName.Location = new System.Drawing.Point(192, 49);
            this.txtGLName.MaxLength = 50;
            this.txtGLName.Name = "txtGLName";
            this.txtGLName.ReadOnly = true;
            this.txtGLName.Size = new System.Drawing.Size(304, 22);
            this.txtGLName.TabIndex = 4;
            this.txtGLName.TabStop = false;
            // 
            // txtGLEcode
            // 
            this.txtGLEcode.BackColor = System.Drawing.Color.White;
            this.txtGLEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGLEcode.Location = new System.Drawing.Point(112, 48);
            this.txtGLEcode.MaxLength = 20;
            this.txtGLEcode.Name = "txtGLEcode";
            this.txtGLEcode.ReadOnly = true;
            this.txtGLEcode.Size = new System.Drawing.Size(77, 22);
            this.txtGLEcode.TabIndex = 3;
            this.txtGLEcode.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(39, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "GL Name";
            // 
            // txtSRName
            // 
            this.txtSRName.BackColor = System.Drawing.SystemColors.Info;
            this.txtSRName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSRName.Location = new System.Drawing.Point(192, 73);
            this.txtSRName.MaxLength = 50;
            this.txtSRName.Name = "txtSRName";
            this.txtSRName.ReadOnly = true;
            this.txtSRName.Size = new System.Drawing.Size(305, 22);
            this.txtSRName.TabIndex = 7;
            this.txtSRName.TabStop = false;
            // 
            // txtSrEcode
            // 
            this.txtSrEcode.BackColor = System.Drawing.Color.White;
            this.txtSrEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSrEcode.Location = new System.Drawing.Point(112, 73);
            this.txtSrEcode.MaxLength = 20;
            this.txtSrEcode.Name = "txtSrEcode";
            this.txtSrEcode.ReadOnly = true;
            this.txtSrEcode.Size = new System.Drawing.Size(77, 22);
            this.txtSrEcode.TabIndex = 6;
            this.txtSrEcode.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(36, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "SR Name";
            // 
            // btnAddVerificationDetl
            // 
            this.btnAddVerificationDetl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddVerificationDetl.BackColor = System.Drawing.Color.YellowGreen;
            this.btnAddVerificationDetl.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnAddVerificationDetl.FlatAppearance.BorderSize = 5;
            this.btnAddVerificationDetl.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddVerificationDetl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddVerificationDetl.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddVerificationDetl.Location = new System.Drawing.Point(487, 245);
            this.btnAddVerificationDetl.Margin = new System.Windows.Forms.Padding(1);
            this.btnAddVerificationDetl.Name = "btnAddVerificationDetl";
            this.btnAddVerificationDetl.Size = new System.Drawing.Size(52, 27);
            this.btnAddVerificationDetl.TabIndex = 18;
            this.btnAddVerificationDetl.Tag = "";
            this.btnAddVerificationDetl.Text = "+&Add";
            this.btnAddVerificationDetl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddVerificationDetl.UseVisualStyleBackColor = false;
            this.btnAddVerificationDetl.Click += new System.EventHandler(this.btnAddVerificationDetl_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbObservType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtVerifiedName);
            this.groupBox1.Controls.Add(this.txtVerifiedBy);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.gvInvVerificationDetl);
            this.groupBox1.Controls.Add(this.btnAddVerificationDetl);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.dtpDocMonth);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.txtOrderNo);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.cbBranch);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpTrnDate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(606, 567);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // txtRemarks
            // 
            this.txtRemarks.BackColor = System.Drawing.Color.White;
            this.txtRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(157, 273);
            this.txtRemarks.MaxLength = 100;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(382, 51);
            this.txtRemarks.TabIndex = 17;
            this.txtRemarks.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(80, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 16;
            this.label5.Text = "Remarks";
            // 
            // cbObservType
            // 
            this.cbObservType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbObservType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbObservType.FormattingEnabled = true;
            this.cbObservType.Items.AddRange(new object[] {
            "                     Select"});
            this.cbObservType.Location = new System.Drawing.Point(157, 245);
            this.cbObservType.Name = "cbObservType";
            this.cbObservType.Size = new System.Drawing.Size(323, 24);
            this.cbObservType.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(16, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Observation Type";
            // 
            // txtVerifiedName
            // 
            this.txtVerifiedName.BackColor = System.Drawing.SystemColors.Info;
            this.txtVerifiedName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVerifiedName.Location = new System.Drawing.Point(188, 107);
            this.txtVerifiedName.MaxLength = 50;
            this.txtVerifiedName.Name = "txtVerifiedName";
            this.txtVerifiedName.ReadOnly = true;
            this.txtVerifiedName.Size = new System.Drawing.Size(350, 22);
            this.txtVerifiedName.TabIndex = 12;
            this.txtVerifiedName.TabStop = false;
            // 
            // txtVerifiedBy
            // 
            this.txtVerifiedBy.BackColor = System.Drawing.Color.White;
            this.txtVerifiedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVerifiedBy.Location = new System.Drawing.Point(107, 106);
            this.txtVerifiedBy.MaxLength = 20;
            this.txtVerifiedBy.Name = "txtVerifiedBy";
            this.txtVerifiedBy.Size = new System.Drawing.Size(80, 22);
            this.txtVerifiedBy.TabIndex = 11;
            this.txtVerifiedBy.TabStop = false;
            this.txtVerifiedBy.Validated += new System.EventHandler(this.txtVerifiedBy_Validated);
            this.txtVerifiedBy.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtVerifiedBy_KeyUp);
            this.txtVerifiedBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVerifiedBy_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label11.Location = new System.Drawing.Point(20, 109);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 16);
            this.label11.TabIndex = 10;
            this.label11.Text = "Verified By";
            // 
            // gvInvVerificationDetl
            // 
            this.gvInvVerificationDetl.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvInvVerificationDetl.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvInvVerificationDetl.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvInvVerificationDetl.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvInvVerificationDetl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvInvVerificationDetl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvInvVerificationDetl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SlNO,
            this.Observation_Code,
            this.Observation_Desc,
            this.Remarks,
            this.Edit,
            this.Del});
            this.gvInvVerificationDetl.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvInvVerificationDetl.Location = new System.Drawing.Point(9, 348);
            this.gvInvVerificationDetl.MultiSelect = false;
            this.gvInvVerificationDetl.Name = "gvInvVerificationDetl";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvInvVerificationDetl.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvInvVerificationDetl.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Navy;
            this.gvInvVerificationDetl.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvInvVerificationDetl.Size = new System.Drawing.Size(587, 170);
            this.gvInvVerificationDetl.TabIndex = 20;
            this.gvInvVerificationDetl.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvInvVerificationDetl_CellClick);
            // 
            // SlNO
            // 
            this.SlNO.HeaderText = "Sl.No";
            this.SlNO.Name = "SlNO";
            this.SlNO.ReadOnly = true;
            this.SlNO.Width = 50;
            // 
            // Observation_Code
            // 
            this.Observation_Code.HeaderText = "Observation Code";
            this.Observation_Code.Name = "Observation_Code";
            this.Observation_Code.ReadOnly = true;
            this.Observation_Code.Visible = false;
            // 
            // Observation_Desc
            // 
            this.Observation_Desc.HeaderText = "Observation Type";
            this.Observation_Desc.Name = "Observation_Desc";
            this.Observation_Desc.ReadOnly = true;
            this.Observation_Desc.Width = 200;
            // 
            // Remarks
            // 
            this.Remarks.HeaderText = "Remarks";
            this.Remarks.Name = "Remarks";
            this.Remarks.ReadOnly = true;
            this.Remarks.Width = 250;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Edit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Edit.Width = 40;
            // 
            // Del
            // 
            this.Del.HeaderText = "Del";
            this.Del.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.Del.Name = "Del";
            this.Del.Width = 40;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Navy;
            this.label10.Location = new System.Drawing.Point(9, 329);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(199, 16);
            this.label10.TabIndex = 19;
            this.label10.Text = "Invoice Observation Details";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.White;
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Navy;
            this.label23.Location = new System.Drawing.Point(223, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(164, 22);
            this.label23.TabIndex = 0;
            this.label23.Text = "Invoice Verification";
            // 
            // frmInvoiceVerification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(610, 572);
            this.ControlBox = false;
            this.Controls.Add(this.label23);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmInvoiceVerification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invoice Verification";
            this.Load += new System.EventHandler(this.frmInvoiceVerification_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvInvVerificationDetl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpTrnDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbBranch;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtOrderNo;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpDocMonth;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.TextBox txtCampName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtGLName;
        private System.Windows.Forms.TextBox txtGLEcode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSRName;
        private System.Windows.Forms.TextBox txtSrEcode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnAddVerificationDetl;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.DataGridView gvInvVerificationDetl;
        private System.Windows.Forms.TextBox txtVerifiedName;
        private System.Windows.Forms.TextBox txtVerifiedBy;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cbObservType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Observation_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Observation_Desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Del;

    }
}