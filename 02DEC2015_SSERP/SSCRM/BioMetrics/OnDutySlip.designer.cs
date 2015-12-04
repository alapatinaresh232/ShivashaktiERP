namespace SSCRM
{
    partial class OnDutySlip
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDept = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDesig = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBranch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtEcodeSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPhNo = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbTo = new System.Windows.Forms.ComboBox();
            this.cbFrom = new System.Windows.Forms.ComboBox();
            this.txtNoOfDays = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.dtFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtAppDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNameApprovBy = new System.Windows.Forms.TextBox();
            this.txtEcodeApprovBy = new System.Windows.Forms.TextBox();
            this.gvODDetl = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeaveDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeaveNoon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VisitingBranchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VisitingBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.txtApplNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblChecking = new System.Windows.Forms.Label();
            this.btnBalance = new System.Windows.Forms.Button();
            this.cmbVisitingBranch = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvODDetl)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(382, 431);
            this.txtEmail.MaxLength = 60;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(344, 21);
            this.txtEmail.TabIndex = 12;
            this.txtEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmail_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.txtDept);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtDesig);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtBranch);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtCompany);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.txtEcodeSearch);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(725, 93);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Employee Details";
            // 
            // txtDept
            // 
            this.txtDept.BackColor = System.Drawing.SystemColors.Info;
            this.txtDept.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDept.Location = new System.Drawing.Point(105, 69);
            this.txtDept.MaxLength = 20;
            this.txtDept.Name = "txtDept";
            this.txtDept.ReadOnly = true;
            this.txtDept.Size = new System.Drawing.Size(270, 21);
            this.txtDept.TabIndex = 0;
            this.txtDept.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.PowderBlue;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(60, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 16);
            this.label7.TabIndex = 65;
            this.label7.Text = "Dept";
            // 
            // txtDesig
            // 
            this.txtDesig.BackColor = System.Drawing.SystemColors.Info;
            this.txtDesig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesig.Location = new System.Drawing.Point(443, 69);
            this.txtDesig.MaxLength = 20;
            this.txtDesig.Name = "txtDesig";
            this.txtDesig.ReadOnly = true;
            this.txtDesig.Size = new System.Drawing.Size(270, 21);
            this.txtDesig.TabIndex = 0;
            this.txtDesig.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.PowderBlue;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(392, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 16);
            this.label6.TabIndex = 63;
            this.label6.Text = "Desig";
            // 
            // txtBranch
            // 
            this.txtBranch.BackColor = System.Drawing.SystemColors.Info;
            this.txtBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBranch.Location = new System.Drawing.Point(443, 45);
            this.txtBranch.MaxLength = 40;
            this.txtBranch.Name = "txtBranch";
            this.txtBranch.ReadOnly = true;
            this.txtBranch.Size = new System.Drawing.Size(270, 21);
            this.txtBranch.TabIndex = 0;
            this.txtBranch.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.PowderBlue;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(385, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 61;
            this.label5.Text = "Branch";
            // 
            // txtCompany
            // 
            this.txtCompany.BackColor = System.Drawing.SystemColors.Info;
            this.txtCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompany.Location = new System.Drawing.Point(106, 45);
            this.txtCompany.MaxLength = 40;
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.ReadOnly = true;
            this.txtCompany.Size = new System.Drawing.Size(270, 21);
            this.txtCompany.TabIndex = 0;
            this.txtCompany.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.PowderBlue;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(28, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 16);
            this.label4.TabIndex = 59;
            this.label4.Text = "Company";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.SystemColors.Info;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(175, 21);
            this.txtName.MaxLength = 40;
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(270, 21);
            this.txtName.TabIndex = 0;
            this.txtName.TabStop = false;
            // 
            // txtEcodeSearch
            // 
            this.txtEcodeSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcodeSearch.Location = new System.Drawing.Point(106, 21);
            this.txtEcodeSearch.MaxLength = 20;
            this.txtEcodeSearch.Name = "txtEcodeSearch";
            this.txtEcodeSearch.Size = new System.Drawing.Size(68, 21);
            this.txtEcodeSearch.TabIndex = 1;
            this.txtEcodeSearch.TextChanged += new System.EventHandler(this.txtEcodeSearch_TextChanged);
            this.txtEcodeSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEcodeSearch_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.PowderBlue;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(51, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 55;
            this.label2.Text = "Ecode";
            // 
            // txtPhNo
            // 
            this.txtPhNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhNo.Location = new System.Drawing.Point(86, 432);
            this.txtPhNo.MaxLength = 20;
            this.txtPhNo.Name = "txtPhNo";
            this.txtPhNo.Size = new System.Drawing.Size(167, 21);
            this.txtPhNo.TabIndex = 11;
            this.txtPhNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhNo_KeyPress);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(133, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "C&lear";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.PowderBlue;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label15.Location = new System.Drawing.Point(314, 436);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 16);
            this.label15.TabIndex = 67;
            this.label15.Text = "Email ID";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.btnDelete);
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Location = new System.Drawing.Point(153, 485);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(427, 47);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.Location = new System.Drawing.Point(220, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(306, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(48, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(382, 383);
            this.txtAddress.MaxLength = 500;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(344, 46);
            this.txtAddress.TabIndex = 10;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.PowderBlue;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label14.Location = new System.Drawing.Point(9, 435);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 16);
            this.label14.TabIndex = 67;
            this.label14.Text = "Phone No";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.PowderBlue;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label12.Location = new System.Drawing.Point(379, 364);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(247, 16);
            this.label12.TabIndex = 4;
            this.label12.Text = "Contact Address During OD Period";
            // 
            // txtReason
            // 
            this.txtReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReason.Location = new System.Drawing.Point(9, 383);
            this.txtReason.MaxLength = 500;
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(360, 46);
            this.txtReason.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.PowderBlue;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label11.Location = new System.Drawing.Point(6, 364);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(115, 16);
            this.label11.TabIndex = 67;
            this.label11.Text = "Reason For OD";
            // 
            // cbTo
            // 
            this.cbTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTo.FormattingEnabled = true;
            this.cbTo.Items.AddRange(new object[] {
            "FN",
            "AN"});
            this.cbTo.Location = new System.Drawing.Point(429, 145);
            this.cbTo.Name = "cbTo";
            this.cbTo.Size = new System.Drawing.Size(55, 23);
            this.cbTo.TabIndex = 5;
            this.cbTo.SelectedIndexChanged += new System.EventHandler(this.cbTo_SelectedIndexChanged);
            // 
            // cbFrom
            // 
            this.cbFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFrom.FormattingEnabled = true;
            this.cbFrom.Items.AddRange(new object[] {
            "FN",
            "AN"});
            this.cbFrom.Location = new System.Drawing.Point(224, 147);
            this.cbFrom.Name = "cbFrom";
            this.cbFrom.Size = new System.Drawing.Size(57, 23);
            this.cbFrom.TabIndex = 3;
            this.cbFrom.SelectedIndexChanged += new System.EventHandler(this.cbFrom_SelectedIndexChanged);
            // 
            // txtNoOfDays
            // 
            this.txtNoOfDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoOfDays.Location = new System.Drawing.Point(664, 148);
            this.txtNoOfDays.MaxLength = 20;
            this.txtNoOfDays.Name = "txtNoOfDays";
            this.txtNoOfDays.ReadOnly = true;
            this.txtNoOfDays.Size = new System.Drawing.Size(51, 21);
            this.txtNoOfDays.TabIndex = 0;
            this.txtNoOfDays.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.PowderBlue;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label10.Location = new System.Drawing.Point(571, 150);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 16);
            this.label10.TabIndex = 67;
            this.label10.Text = "No Of Days";
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(320, 146);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(103, 23);
            this.dtpToDate.TabIndex = 4;
            this.dtpToDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpToDate.ValueChanged += new System.EventHandler(this.dtpToDate_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.PowderBlue;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label9.Location = new System.Drawing.Point(287, 149);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 16);
            this.label9.TabIndex = 57;
            this.label9.Text = "To";
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFromDate.Location = new System.Drawing.Point(115, 146);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.Size = new System.Drawing.Size(103, 23);
            this.dtFromDate.TabIndex = 2;
            this.dtFromDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtFromDate.ValueChanged += new System.EventHandler(this.dtFromDate_ValueChanged);
            // 
            // dtAppDate
            // 
            this.dtAppDate.CustomFormat = "dd/MM/yyyy";
            this.dtAppDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtAppDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtAppDate.Location = new System.Drawing.Point(613, 19);
            this.dtAppDate.Name = "dtAppDate";
            this.dtAppDate.Size = new System.Drawing.Size(103, 23);
            this.dtAppDate.TabIndex = 22;
            this.dtAppDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.PowderBlue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(570, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 16);
            this.label1.TabIndex = 26;
            this.label1.Text = "Date";
            // 
            // txtNameApprovBy
            // 
            this.txtNameApprovBy.BackColor = System.Drawing.SystemColors.Info;
            this.txtNameApprovBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNameApprovBy.Location = new System.Drawing.Point(453, 455);
            this.txtNameApprovBy.MaxLength = 40;
            this.txtNameApprovBy.Name = "txtNameApprovBy";
            this.txtNameApprovBy.ReadOnly = true;
            this.txtNameApprovBy.Size = new System.Drawing.Size(273, 21);
            this.txtNameApprovBy.TabIndex = 0;
            this.txtNameApprovBy.TabStop = false;
            // 
            // txtEcodeApprovBy
            // 
            this.txtEcodeApprovBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcodeApprovBy.Location = new System.Drawing.Point(383, 455);
            this.txtEcodeApprovBy.MaxLength = 20;
            this.txtEcodeApprovBy.Name = "txtEcodeApprovBy";
            this.txtEcodeApprovBy.Size = new System.Drawing.Size(68, 21);
            this.txtEcodeApprovBy.TabIndex = 13;
            this.txtEcodeApprovBy.TextChanged += new System.EventHandler(this.txtEcodeApprovBy_TextChanged);
            this.txtEcodeApprovBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEcodeApprovBy_KeyPress);
            // 
            // gvODDetl
            // 
            this.gvODDetl.AllowUserToAddRows = false;
            this.gvODDetl.BackgroundColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvODDetl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvODDetl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvODDetl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.Date,
            this.LeaveDays,
            this.LeaveNoon,
            this.VisitingBranchCode,
            this.VisitingBranch,
            this.Remarks,
            this.Edit});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvODDetl.DefaultCellStyle = dataGridViewCellStyle4;
            this.gvODDetl.Location = new System.Drawing.Point(4, 203);
            this.gvODDetl.Name = "gvODDetl";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvODDetl.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gvODDetl.RowHeadersVisible = false;
            this.gvODDetl.Size = new System.Drawing.Size(725, 159);
            this.gvODDetl.TabIndex = 8;
            this.gvODDetl.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvODDetl_CellEndEdit);
            this.gvODDetl.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvODDetl_CellClick);
            // 
            // SLNO
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SLNO.DefaultCellStyle = dataGridViewCellStyle2;
            this.SLNO.HeaderText = "SlNo";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Width = 40;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // LeaveDays
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.LeaveDays.DefaultCellStyle = dataGridViewCellStyle3;
            this.LeaveDays.HeaderText = "OD Days";
            this.LeaveDays.Name = "LeaveDays";
            this.LeaveDays.ReadOnly = true;
            this.LeaveDays.Width = 70;
            // 
            // LeaveNoon
            // 
            this.LeaveNoon.HeaderText = "OD Noon";
            this.LeaveNoon.Name = "LeaveNoon";
            this.LeaveNoon.ReadOnly = true;
            // 
            // VisitingBranchCode
            // 
            this.VisitingBranchCode.HeaderText = "VisitingBranchCode";
            this.VisitingBranchCode.Name = "VisitingBranchCode";
            this.VisitingBranchCode.Visible = false;
            this.VisitingBranchCode.Width = 200;
            // 
            // VisitingBranch
            // 
            this.VisitingBranch.HeaderText = "VisitingBranch";
            this.VisitingBranch.Name = "VisitingBranch";
            this.VisitingBranch.ReadOnly = true;
            this.VisitingBranch.Width = 200;
            // 
            // Remarks
            // 
            this.Remarks.HeaderText = "Remarks";
            this.Remarks.Name = "Remarks";
            this.Remarks.ReadOnly = true;
            this.Remarks.Width = 150;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.Edit.Name = "Edit";
            this.Edit.Width = 40;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.PowderBlue;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(285, 460);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 16);
            this.label3.TabIndex = 72;
            this.label3.Text = "Approved by";
            // 
            // txtApplNo
            // 
            this.txtApplNo.BackColor = System.Drawing.SystemColors.Info;
            this.txtApplNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApplNo.Location = new System.Drawing.Point(120, 22);
            this.txtApplNo.MaxLength = 50;
            this.txtApplNo.Name = "txtApplNo";
            this.txtApplNo.Size = new System.Drawing.Size(106, 21);
            this.txtApplNo.TabIndex = 21;
            this.txtApplNo.TabStop = false;
            this.txtApplNo.Validated += new System.EventHandler(this.txtApplNo_Validated);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.PowderBlue;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(70, 149);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 16);
            this.label8.TabIndex = 55;
            this.label8.Text = "From";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.PowderBlue;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label13.Location = new System.Drawing.Point(8, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(110, 16);
            this.label13.TabIndex = 25;
            this.label13.Text = "Application No";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.Controls.Add(this.lblChecking);
            this.groupBox2.Controls.Add(this.btnBalance);
            this.groupBox2.Controls.Add(this.cmbVisitingBranch);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.txtNameApprovBy);
            this.groupBox2.Controls.Add(this.txtEcodeApprovBy);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.gvODDetl);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.txtEmail);
            this.groupBox2.Controls.Add(this.txtPhNo);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txtAddress);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtReason);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.cbTo);
            this.groupBox2.Controls.Add(this.cbFrom);
            this.groupBox2.Controls.Add(this.txtNoOfDays);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.dtpToDate);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.dtFromDate);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(733, 540);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            // 
            // lblChecking
            // 
            this.lblChecking.AutoSize = true;
            this.lblChecking.BackColor = System.Drawing.Color.PowderBlue;
            this.lblChecking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChecking.ForeColor = System.Drawing.Color.Red;
            this.lblChecking.Location = new System.Drawing.Point(269, 31);
            this.lblChecking.Name = "lblChecking";
            this.lblChecking.Size = new System.Drawing.Size(72, 16);
            this.lblChecking.TabIndex = 92;
            this.lblChecking.Text = "Checking";
            // 
            // btnBalance
            // 
            this.btnBalance.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBalance.BackColor = System.Drawing.Color.YellowGreen;
            this.btnBalance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnBalance.FlatAppearance.BorderSize = 5;
            this.btnBalance.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBalance.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBalance.Location = new System.Drawing.Point(498, 172);
            this.btnBalance.Margin = new System.Windows.Forms.Padding(1);
            this.btnBalance.Name = "btnBalance";
            this.btnBalance.Size = new System.Drawing.Size(55, 24);
            this.btnBalance.TabIndex = 7;
            this.btnBalance.Tag = "Product  Search";
            this.btnBalance.Text = "+ Add";
            this.btnBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBalance.UseVisualStyleBackColor = false;
            this.btnBalance.Visible = false;
            this.btnBalance.Click += new System.EventHandler(this.btnBalance_Click);
            // 
            // cmbVisitingBranch
            // 
            this.cmbVisitingBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVisitingBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbVisitingBranch.FormattingEnabled = true;
            this.cmbVisitingBranch.Items.AddRange(new object[] {
            "FN",
            "AN"});
            this.cmbVisitingBranch.Location = new System.Drawing.Point(113, 173);
            this.cmbVisitingBranch.Name = "cmbVisitingBranch";
            this.cmbVisitingBranch.Size = new System.Drawing.Size(371, 23);
            this.cmbVisitingBranch.TabIndex = 6;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.PowderBlue;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label17.Location = new System.Drawing.Point(3, 176);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(111, 16);
            this.label17.TabIndex = 91;
            this.label17.Text = "Visiting Branch";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Navy;
            this.label16.Location = new System.Drawing.Point(296, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(140, 22);
            this.label16.TabIndex = 90;
            this.label16.Text = "ONDUTY FORM";
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Edit";
            this.dataGridViewImageColumn1.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 40;
            // 
            // OnDutySlip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 547);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtAppDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtApplNo);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.groupBox2);
            this.Name = "OnDutySlip";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "On Duty Slip";
            this.Load += new System.EventHandler(this.OnDutyEntryForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvODDetl)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDept;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDesig;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBranch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtEcodeSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPhNo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbTo;
        private System.Windows.Forms.ComboBox cbFrom;
        private System.Windows.Forms.TextBox txtNoOfDays;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtFromDate;
        private System.Windows.Forms.DateTimePicker dtAppDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNameApprovBy;
        private System.Windows.Forms.TextBox txtEcodeApprovBy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtApplNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cmbVisitingBranch;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnBalance;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        public System.Windows.Forms.DataGridView gvODDetl;
        private System.Windows.Forms.Label lblChecking;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeaveDays;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeaveNoon;
        private System.Windows.Forms.DataGridViewTextBoxColumn VisitingBranchCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn VisitingBranch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
    }
}