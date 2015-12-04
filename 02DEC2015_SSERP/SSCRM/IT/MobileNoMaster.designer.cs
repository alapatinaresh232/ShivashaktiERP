namespace SSCRM
{
    partial class MobileNoMaster
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gvMobileNoDetails = new System.Windows.Forms.DataGridView();
            this.txtName = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.DtpIssuedDate = new System.Windows.Forms.DateTimePicker();
            this.label21 = new System.Windows.Forms.Label();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.lblCompany = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCompanyLimit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDeptorDesign = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPlace = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.slNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MBM_MOBILE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MBM_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MBM_COMPANY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompanyCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MBM_LOCATION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MBM_DEPT_DESIG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MBM_LIMIT_FLAG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MBM_LIMIT_AMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MBM_ISSUED_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MBM_STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvMobileNoDetails)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.DtpIssuedDate);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.cbStatus);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.lblCompany);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCompanyLimit);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtDeptorDesign);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtPlace);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtMobileNo);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(877, 504);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gvMobileNoDetails);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(4, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(869, 335);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mobile Number Details";
            // 
            // gvMobileNoDetails
            // 
            this.gvMobileNoDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvMobileNoDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvMobileNoDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvMobileNoDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvMobileNoDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvMobileNoDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvMobileNoDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.slNo,
            this.MBM_MOBILE_NO,
            this.MBM_NAME,
            this.MBM_COMPANY,
            this.CompanyCode,
            this.MBM_LOCATION,
            this.MBM_DEPT_DESIG,
            this.MBM_LIMIT_FLAG,
            this.MBM_LIMIT_AMT,
            this.MBM_ISSUED_DATE,
            this.MBM_STATUS,
            this.Edit});
            this.gvMobileNoDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvMobileNoDetails.Location = new System.Drawing.Point(5, 22);
            this.gvMobileNoDetails.MultiSelect = false;
            this.gvMobileNoDetails.Name = "gvMobileNoDetails";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvMobileNoDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvMobileNoDetails.RowHeadersVisible = false;
            this.gvMobileNoDetails.Size = new System.Drawing.Size(859, 306);
            this.gvMobileNoDetails.TabIndex = 0;
            this.gvMobileNoDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvMobileNoDetails_CellClick);
            // 
            // txtName
            // 
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(573, 16);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(236, 21);
            this.txtName.TabIndex = 2;
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnDelete);
            this.groupBox5.Controls.Add(this.btnClose);
            this.groupBox5.Controls.Add(this.btnCancel);
            this.groupBox5.Controls.Add(this.btnSave);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox5.Location = new System.Drawing.Point(255, 448);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(367, 51);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.Location = new System.Drawing.Point(186, 16);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(74, 26);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(265, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 26);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "C&lose";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(107, 16);
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
            this.btnSave.Location = new System.Drawing.Point(28, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // DtpIssuedDate
            // 
            this.DtpIssuedDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtpIssuedDate.CustomFormat = "dd/MM/yyyy";
            this.DtpIssuedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpIssuedDate.Location = new System.Drawing.Point(574, 67);
            this.DtpIssuedDate.Name = "DtpIssuedDate";
            this.DtpIssuedDate.Size = new System.Drawing.Size(90, 20);
            this.DtpIssuedDate.TabIndex = 6;
            this.DtpIssuedDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label21.Location = new System.Drawing.Point(479, 67);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(91, 16);
            this.label21.TabIndex = 13;
            this.label21.Text = "Issued Date";
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "--Select--",
            "ACTIVE",
            "DEACTIVE"});
            this.cbStatus.Location = new System.Drawing.Point(145, 89);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(115, 24);
            this.cbStatus.TabIndex = 7;
            this.cbStatus.SelectedIndexChanged += new System.EventHandler(this.cbStatus_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(91, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Status";
            // 
            // cbCompany
            // 
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Location = new System.Drawing.Point(145, 38);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(300, 24);
            this.cbCompany.TabIndex = 3;
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblCompany.Location = new System.Drawing.Point(71, 42);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(73, 16);
            this.lblCompany.TabIndex = 10;
            this.lblCompany.Text = "Company";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(521, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Name";
            // 
            // txtCompanyLimit
            // 
            this.txtCompanyLimit.BackColor = System.Drawing.SystemColors.Window;
            this.txtCompanyLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyLimit.Location = new System.Drawing.Point(572, 92);
            this.txtCompanyLimit.MaxLength = 10;
            this.txtCompanyLimit.Name = "txtCompanyLimit";
            this.txtCompanyLimit.Size = new System.Drawing.Size(92, 21);
            this.txtCompanyLimit.TabIndex = 10;
            this.txtCompanyLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCompanyLimit_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(432, 94);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "Company Limit Amt";
            // 
            // txtDeptorDesign
            // 
            this.txtDeptorDesign.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeptorDesign.Location = new System.Drawing.Point(573, 41);
            this.txtDeptorDesign.MaxLength = 100;
            this.txtDeptorDesign.Name = "txtDeptorDesign";
            this.txtDeptorDesign.Size = new System.Drawing.Size(236, 21);
            this.txtDeptorDesign.TabIndex = 4;
            this.txtDeptorDesign.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDeptorDesign_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(483, 42);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "Dept/Desig";
            // 
            // txtPlace
            // 
            this.txtPlace.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlace.Location = new System.Drawing.Point(146, 64);
            this.txtPlace.MaxLength = 100;
            this.txtPlace.Name = "txtPlace";
            this.txtPlace.Size = new System.Drawing.Size(299, 21);
            this.txtPlace.TabIndex = 5;
            this.txtPlace.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPlace_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(95, 68);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Place";
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMobileNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNo.Location = new System.Drawing.Point(145, 14);
            this.txtMobileNo.MaxLength = 10;
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(158, 21);
            this.txtMobileNo.TabIndex = 1;
            this.txtMobileNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMobileNo_KeyUp);
            this.txtMobileNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMobileNo_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label16.Location = new System.Drawing.Point(67, 17);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(79, 16);
            this.label16.TabIndex = 0;
            this.label16.Text = "Mobile No";
            // 
            // slNo
            // 
            this.slNo.Frozen = true;
            this.slNo.HeaderText = "Sl.No";
            this.slNo.Name = "slNo";
            this.slNo.ReadOnly = true;
            this.slNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.slNo.Width = 50;
            // 
            // MBM_MOBILE_NO
            // 
            this.MBM_MOBILE_NO.HeaderText = "MobileNo";
            this.MBM_MOBILE_NO.Name = "MBM_MOBILE_NO";
            this.MBM_MOBILE_NO.ReadOnly = true;
            // 
            // MBM_NAME
            // 
            this.MBM_NAME.HeaderText = "Name";
            this.MBM_NAME.Name = "MBM_NAME";
            this.MBM_NAME.ReadOnly = true;
            this.MBM_NAME.Width = 150;
            // 
            // MBM_COMPANY
            // 
            this.MBM_COMPANY.HeaderText = "Company ";
            this.MBM_COMPANY.Name = "MBM_COMPANY";
            this.MBM_COMPANY.ReadOnly = true;
            this.MBM_COMPANY.Width = 200;
            // 
            // CompanyCode
            // 
            this.CompanyCode.HeaderText = "Company Code";
            this.CompanyCode.Name = "CompanyCode";
            this.CompanyCode.ReadOnly = true;
            this.CompanyCode.Visible = false;
            // 
            // MBM_LOCATION
            // 
            this.MBM_LOCATION.HeaderText = "Place";
            this.MBM_LOCATION.Name = "MBM_LOCATION";
            this.MBM_LOCATION.ReadOnly = true;
            // 
            // MBM_DEPT_DESIG
            // 
            this.MBM_DEPT_DESIG.HeaderText = "Dept/Desig";
            this.MBM_DEPT_DESIG.Name = "MBM_DEPT_DESIG";
            this.MBM_DEPT_DESIG.ReadOnly = true;
            // 
            // MBM_LIMIT_FLAG
            // 
            this.MBM_LIMIT_FLAG.HeaderText = "Flage";
            this.MBM_LIMIT_FLAG.Name = "MBM_LIMIT_FLAG";
            this.MBM_LIMIT_FLAG.ReadOnly = true;
            this.MBM_LIMIT_FLAG.Visible = false;
            // 
            // MBM_LIMIT_AMT
            // 
            this.MBM_LIMIT_AMT.HeaderText = "Company Limit Amt";
            this.MBM_LIMIT_AMT.Name = "MBM_LIMIT_AMT";
            this.MBM_LIMIT_AMT.ReadOnly = true;
            this.MBM_LIMIT_AMT.Visible = false;
            // 
            // MBM_ISSUED_DATE
            // 
            this.MBM_ISSUED_DATE.HeaderText = "Issued Date";
            this.MBM_ISSUED_DATE.Name = "MBM_ISSUED_DATE";
            this.MBM_ISSUED_DATE.ReadOnly = true;
            this.MBM_ISSUED_DATE.Visible = false;
            // 
            // MBM_STATUS
            // 
            this.MBM_STATUS.HeaderText = "Status";
            this.MBM_STATUS.Name = "MBM_STATUS";
            this.MBM_STATUS.ReadOnly = true;
            this.MBM_STATUS.Width = 75;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "";
            this.Edit.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.Edit.Name = "Edit";
            this.Edit.Width = 30;
            // 
            // MobileNoMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(884, 509);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "MobileNoMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mobile No Master";
            this.Load += new System.EventHandler(this.MobileNoMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvMobileNoDetails)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.DataGridView gvMobileNoDetails;
        private System.Windows.Forms.TextBox txtCompanyLimit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDeptorDesign;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPlace;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMobileNo;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.DateTimePicker DtpIssuedDate;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.DataGridViewTextBoxColumn slNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn MBM_MOBILE_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn MBM_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn MBM_COMPANY;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn MBM_LOCATION;
        private System.Windows.Forms.DataGridViewTextBoxColumn MBM_DEPT_DESIG;
        private System.Windows.Forms.DataGridViewTextBoxColumn MBM_LIMIT_FLAG;
        private System.Windows.Forms.DataGridViewTextBoxColumn MBM_LIMIT_AMT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MBM_ISSUED_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MBM_STATUS;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
    }
}