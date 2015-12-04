namespace SSCRM
{
    partial class PunchMissing
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEcodeSearch = new System.Windows.Forms.TextBox();
            this.gvPunchDetails = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PunchSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PunTimeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtEName = new System.Windows.Forms.TextBox();
            this.lblEcode = new System.Windows.Forms.Label();
            this.dtpPunchMissDate = new System.Windows.Forms.DateTimePicker();
            this.dtpAppDate = new System.Windows.Forms.DateTimePicker();
            this.txtTranNo = new System.Windows.Forms.TextBox();
            this.lblTranNo = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblReason = new System.Windows.Forms.Label();
            this.lblPunchMissDate = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.txtEmpDesg = new System.Windows.Forms.TextBox();
            this.lblDesg = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMinutes = new System.Windows.Forms.TextBox();
            this.txtHours = new System.Windows.Forms.TextBox();
            this.txtNameApprovBy = new System.Windows.Forms.TextBox();
            this.txtEcodeApprovBy = new System.Windows.Forms.TextBox();
            this.lblApprovedBy = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbInOut = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPunchDetails)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtEcodeSearch);
            this.groupBox1.Controls.Add(this.gvPunchDetails);
            this.groupBox1.Controls.Add(this.txtEName);
            this.groupBox1.Controls.Add(this.lblEcode);
            this.groupBox1.Controls.Add(this.dtpPunchMissDate);
            this.groupBox1.Controls.Add(this.dtpAppDate);
            this.groupBox1.Controls.Add(this.txtTranNo);
            this.groupBox1.Controls.Add(this.lblTranNo);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.lblReason);
            this.groupBox1.Controls.Add(this.lblPunchMissDate);
            this.groupBox1.Controls.Add(this.lblDate);
            this.groupBox1.Controls.Add(this.txtEmpDesg);
            this.groupBox1.Controls.Add(this.lblDesg);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.groupBox1.Location = new System.Drawing.Point(4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 460);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(167, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 22);
            this.label3.TabIndex = 90;
            this.label3.Text = "PUNCH MISSING ";
            // 
            // txtEcodeSearch
            // 
            this.txtEcodeSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcodeSearch.Location = new System.Drawing.Point(86, 61);
            this.txtEcodeSearch.MaxLength = 20;
            this.txtEcodeSearch.Name = "txtEcodeSearch";
            this.txtEcodeSearch.Size = new System.Drawing.Size(68, 23);
            this.txtEcodeSearch.TabIndex = 1;
            this.txtEcodeSearch.TextChanged += new System.EventHandler(this.txtEcodeSearch_TextChanged);
            // 
            // gvPunchDetails
            // 
            this.gvPunchDetails.AllowUserToAddRows = false;
            this.gvPunchDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvPunchDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPunchDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.Date,
            this.Time,
            this.PunchSource,
            this.PunTimeId});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvPunchDetails.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvPunchDetails.Location = new System.Drawing.Point(13, 264);
            this.gvPunchDetails.Name = "gvPunchDetails";
            this.gvPunchDetails.RowHeadersVisible = false;
            this.gvPunchDetails.Size = new System.Drawing.Size(475, 140);
            this.gvPunchDetails.TabIndex = 4;
            // 
            // SLNO
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SLNO.DefaultCellStyle = dataGridViewCellStyle1;
            this.SLNO.HeaderText = "SlNo";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Width = 50;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // Time
            // 
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.Width = 60;
            // 
            // PunchSource
            // 
            this.PunchSource.HeaderText = "PunSource";
            this.PunchSource.Name = "PunchSource";
            this.PunchSource.ReadOnly = true;
            // 
            // PunTimeId
            // 
            this.PunTimeId.HeaderText = "PunTimeId";
            this.PunTimeId.Name = "PunTimeId";
            // 
            // txtEName
            // 
            this.txtEName.BackColor = System.Drawing.SystemColors.Info;
            this.txtEName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEName.Location = new System.Drawing.Point(155, 62);
            this.txtEName.MaxLength = 20;
            this.txtEName.Name = "txtEName";
            this.txtEName.ReadOnly = true;
            this.txtEName.Size = new System.Drawing.Size(322, 22);
            this.txtEName.TabIndex = 0;
            this.txtEName.TabStop = false;
            // 
            // lblEcode
            // 
            this.lblEcode.AutoSize = true;
            this.lblEcode.BackColor = System.Drawing.Color.PowderBlue;
            this.lblEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEcode.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblEcode.Location = new System.Drawing.Point(33, 65);
            this.lblEcode.Name = "lblEcode";
            this.lblEcode.Size = new System.Drawing.Size(53, 16);
            this.lblEcode.TabIndex = 2;
            this.lblEcode.Text = "Ecode";
            // 
            // dtpPunchMissDate
            // 
            this.dtpPunchMissDate.CustomFormat = "dd/MM/yyyy";
            this.dtpPunchMissDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPunchMissDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPunchMissDate.Location = new System.Drawing.Point(86, 138);
            this.dtpPunchMissDate.Name = "dtpPunchMissDate";
            this.dtpPunchMissDate.Size = new System.Drawing.Size(96, 22);
            this.dtpPunchMissDate.TabIndex = 2;
            this.dtpPunchMissDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // dtpAppDate
            // 
            this.dtpAppDate.CustomFormat = "dd/MM/yyyy";
            this.dtpAppDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpAppDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAppDate.Location = new System.Drawing.Point(383, 25);
            this.dtpAppDate.Name = "dtpAppDate";
            this.dtpAppDate.Size = new System.Drawing.Size(94, 23);
            this.dtpAppDate.TabIndex = 0;
            this.dtpAppDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpAppDate.ValueChanged += new System.EventHandler(this.dtpAppDate_ValueChanged);
            // 
            // txtTranNo
            // 
            this.txtTranNo.AcceptsTab = true;
            this.txtTranNo.BackColor = System.Drawing.SystemColors.Window;
            this.txtTranNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTranNo.Location = new System.Drawing.Point(124, 26);
            this.txtTranNo.Name = "txtTranNo";
            this.txtTranNo.Size = new System.Drawing.Size(79, 22);
            this.txtTranNo.TabIndex = 0;
            this.txtTranNo.TabStop = false;
            this.txtTranNo.Validated += new System.EventHandler(this.txtTranNo_Validated);
            // 
            // lblTranNo
            // 
            this.lblTranNo.AutoSize = true;
            this.lblTranNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTranNo.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblTranNo.Location = new System.Drawing.Point(6, 28);
            this.lblTranNo.Name = "lblTranNo";
            this.lblTranNo.Size = new System.Drawing.Size(114, 17);
            this.lblTranNo.TabIndex = 0;
            this.lblTranNo.Text = "TransactionNo";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Location = new System.Drawing.Point(71, 406);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 47);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(136, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Clear";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(220, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 30);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "C&lose";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(52, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblReason
            // 
            this.lblReason.AutoSize = true;
            this.lblReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReason.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblReason.Location = new System.Drawing.Point(23, 169);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(63, 17);
            this.lblReason.TabIndex = 0;
            this.lblReason.Text = "Reason";
            // 
            // lblPunchMissDate
            // 
            this.lblPunchMissDate.AutoSize = true;
            this.lblPunchMissDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPunchMissDate.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblPunchMissDate.Location = new System.Drawing.Point(44, 140);
            this.lblPunchMissDate.Name = "lblPunchMissDate";
            this.lblPunchMissDate.Size = new System.Drawing.Size(42, 17);
            this.lblPunchMissDate.TabIndex = 0;
            this.lblPunchMissDate.Text = "Date";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblDate.Location = new System.Drawing.Point(338, 28);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(42, 17);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "Date";
            // 
            // txtEmpDesg
            // 
            this.txtEmpDesg.BackColor = System.Drawing.SystemColors.Info;
            this.txtEmpDesg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpDesg.Location = new System.Drawing.Point(86, 90);
            this.txtEmpDesg.MaxLength = 50;
            this.txtEmpDesg.Name = "txtEmpDesg";
            this.txtEmpDesg.ReadOnly = true;
            this.txtEmpDesg.Size = new System.Drawing.Size(391, 22);
            this.txtEmpDesg.TabIndex = 0;
            this.txtEmpDesg.TabStop = false;
            // 
            // lblDesg
            // 
            this.lblDesg.AutoSize = true;
            this.lblDesg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesg.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblDesg.Location = new System.Drawing.Point(37, 90);
            this.lblDesg.Name = "lblDesg";
            this.lblDesg.Size = new System.Drawing.Size(49, 17);
            this.lblDesg.TabIndex = 0;
            this.lblDesg.Text = "Desig";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMinutes);
            this.groupBox2.Controls.Add(this.txtHours);
            this.groupBox2.Controls.Add(this.txtNameApprovBy);
            this.groupBox2.Controls.Add(this.txtEcodeApprovBy);
            this.groupBox2.Controls.Add(this.lblApprovedBy);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtReason);
            this.groupBox2.Controls.Add(this.lblTime);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbInOut);
            this.groupBox2.Location = new System.Drawing.Point(13, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(475, 140);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Punch Missing Details";
            // 
            // txtMinutes
            // 
            this.txtMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinutes.Location = new System.Drawing.Point(434, 21);
            this.txtMinutes.MaxLength = 2;
            this.txtMinutes.Name = "txtMinutes";
            this.txtMinutes.Size = new System.Drawing.Size(29, 22);
            this.txtMinutes.TabIndex = 5;
            // 
            // txtHours
            // 
            this.txtHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHours.Location = new System.Drawing.Point(400, 21);
            this.txtHours.MaxLength = 2;
            this.txtHours.Name = "txtHours";
            this.txtHours.Size = new System.Drawing.Size(28, 22);
            this.txtHours.TabIndex = 4;
            // 
            // txtNameApprovBy
            // 
            this.txtNameApprovBy.BackColor = System.Drawing.SystemColors.Info;
            this.txtNameApprovBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNameApprovBy.Location = new System.Drawing.Point(185, 110);
            this.txtNameApprovBy.MaxLength = 40;
            this.txtNameApprovBy.Name = "txtNameApprovBy";
            this.txtNameApprovBy.ReadOnly = true;
            this.txtNameApprovBy.Size = new System.Drawing.Size(279, 23);
            this.txtNameApprovBy.TabIndex = 0;
            this.txtNameApprovBy.TabStop = false;
            // 
            // txtEcodeApprovBy
            // 
            this.txtEcodeApprovBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcodeApprovBy.Location = new System.Drawing.Point(115, 110);
            this.txtEcodeApprovBy.MaxLength = 20;
            this.txtEcodeApprovBy.Name = "txtEcodeApprovBy";
            this.txtEcodeApprovBy.Size = new System.Drawing.Size(68, 23);
            this.txtEcodeApprovBy.TabIndex = 7;
            this.txtEcodeApprovBy.TextChanged += new System.EventHandler(this.txtEcodeApprovBy_TextChanged);
            this.txtEcodeApprovBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEcodeApprovBy_KeyPress);
            // 
            // lblApprovedBy
            // 
            this.lblApprovedBy.AutoSize = true;
            this.lblApprovedBy.BackColor = System.Drawing.Color.PowderBlue;
            this.lblApprovedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApprovedBy.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblApprovedBy.Location = new System.Drawing.Point(17, 115);
            this.lblApprovedBy.Name = "lblApprovedBy";
            this.lblApprovedBy.Size = new System.Drawing.Size(97, 16);
            this.lblApprovedBy.TabIndex = 0;
            this.lblApprovedBy.Text = "Approved by";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(425, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = ":";
            // 
            // txtReason
            // 
            this.txtReason.BackColor = System.Drawing.SystemColors.Window;
            this.txtReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReason.Location = new System.Drawing.Point(74, 49);
            this.txtReason.MaxLength = 50;
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(389, 55);
            this.txtReason.TabIndex = 6;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblTime.Location = new System.Drawing.Point(352, 22);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(43, 17);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "Time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(191, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "IN/OUT";
            // 
            // cmbInOut
            // 
            this.cmbInOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInOut.FormattingEnabled = true;
            this.cmbInOut.Items.AddRange(new object[] {
            "--Select--",
            "IN",
            "OUT"});
            this.cmbInOut.Location = new System.Drawing.Point(254, 19);
            this.cmbInOut.Name = "cmbInOut";
            this.cmbInOut.Size = new System.Drawing.Size(75, 24);
            this.cmbInOut.TabIndex = 3;
            // 
            // PunchMissing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(500, 465);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PunchMissing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Missing / Manual Punch";
            this.Load += new System.EventHandler(this.PunchMissing_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPunchDetails)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox txtEmpDesg;
        private System.Windows.Forms.Label lblDesg;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblPunchMissDate;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.ComboBox cmbInOut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.TextBox txtTranNo;
        private System.Windows.Forms.Label lblTranNo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker dtpAppDate;
        private System.Windows.Forms.DateTimePicker dtpPunchMissDate;
        private System.Windows.Forms.TextBox txtEName;
        private System.Windows.Forms.Label lblEcode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNameApprovBy;
        private System.Windows.Forms.TextBox txtEcodeApprovBy;
        private System.Windows.Forms.Label lblApprovedBy;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.DataGridView gvPunchDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn PunchSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn PunTimeId;
        private System.Windows.Forms.TextBox txtMinutes;
        private System.Windows.Forms.TextBox txtHours;
        private System.Windows.Forms.TextBox txtEcodeSearch;
        private System.Windows.Forms.Label label3;
    }
}