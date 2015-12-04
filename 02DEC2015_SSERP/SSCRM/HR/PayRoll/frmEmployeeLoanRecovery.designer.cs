namespace SSCRM
{
    partial class frmEmployeeLoanRecovery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gvEmpLoanRecoveryDetails = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnStopReq = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbBranches = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpMonth = new System.Windows.Forms.DateTimePicker();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoanNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoanSlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoanLastSlno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActualEmi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ecode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desig = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoanType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoanAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecoveredAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BalanceAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkStopReq = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpLoanRecoveryDetails)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.cbBranches);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpMonth);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(938, 445);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gvEmpLoanRecoveryDetails);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.groupBox2.Location = new System.Drawing.Point(5, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(929, 317);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Employee Loan Recovery Details";
            // 
            // gvEmpLoanRecoveryDetails
            // 
            this.gvEmpLoanRecoveryDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvEmpLoanRecoveryDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvEmpLoanRecoveryDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvEmpLoanRecoveryDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvEmpLoanRecoveryDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvEmpLoanRecoveryDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEmpLoanRecoveryDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.LoanNo,
            this.LoanSlNo,
            this.LoanLastSlno,
            this.ActualEmi,
            this.Ecode,
            this.EmpName,
            this.Desig,
            this.LoanType,
            this.LoanAmt,
            this.RecoveredAmt,
            this.BalanceAmt,
            this.Emi,
            this.Remarks,
            this.chkStopReq});
            this.gvEmpLoanRecoveryDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvEmpLoanRecoveryDetails.Location = new System.Drawing.Point(6, 23);
            this.gvEmpLoanRecoveryDetails.MultiSelect = false;
            this.gvEmpLoanRecoveryDetails.Name = "gvEmpLoanRecoveryDetails";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvEmpLoanRecoveryDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvEmpLoanRecoveryDetails.RowHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Navy;
            this.gvEmpLoanRecoveryDetails.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvEmpLoanRecoveryDetails.Size = new System.Drawing.Size(916, 287);
            this.gvEmpLoanRecoveryDetails.TabIndex = 0;
            this.gvEmpLoanRecoveryDetails.TabStop = false;
            this.gvEmpLoanRecoveryDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvEmpLoanRecoveryDetails_CellEndEdit);
            this.gvEmpLoanRecoveryDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvEmpLoanRecoveryDetails_EditingControlShowing);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnStopReq);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(230, 392);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(479, 45);
            this.groupBox4.TabIndex = 40;
            this.groupBox4.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(225, 13);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnStopReq
            // 
            this.btnStopReq.BackColor = System.Drawing.Color.AliceBlue;
            this.btnStopReq.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnStopReq.Location = new System.Drawing.Point(302, 13);
            this.btnStopReq.Name = "btnStopReq";
            this.btnStopReq.Size = new System.Drawing.Size(132, 26);
            this.btnStopReq.TabIndex = 2;
            this.btnStopReq.Text = "Stop &Request";
            this.btnStopReq.UseVisualStyleBackColor = false;
            this.btnStopReq.Click += new System.EventHandler(this.btnStopReq_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(148, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoEllipsis = true;
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(45, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Recovered";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbBranches
            // 
            this.cbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranches.FormattingEnabled = true;
            this.cbBranches.Location = new System.Drawing.Point(474, 16);
            this.cbBranches.Name = "cbBranches";
            this.cbBranches.Size = new System.Drawing.Size(370, 24);
            this.cbBranches.TabIndex = 26;
            this.cbBranches.SelectedIndexChanged += new System.EventHandler(this.cbBranches_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(415, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 25;
            this.label5.Text = "Branch";
            // 
            // cbCompany
            // 
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Items.AddRange(new object[] {
            "                     Select"});
            this.cbCompany.Location = new System.Drawing.Point(92, 15);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(310, 24);
            this.cbCompany.TabIndex = 24;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(17, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 17);
            this.label6.TabIndex = 23;
            this.label6.Text = "Company";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(41, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 16);
            this.label4.TabIndex = 21;
            this.label4.Text = "Month";
            // 
            // dtpMonth
            // 
            this.dtpMonth.CustomFormat = "MMM/yyyy";
            this.dtpMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMonth.Location = new System.Drawing.Point(92, 46);
            this.dtpMonth.Name = "dtpMonth";
            this.dtpMonth.Size = new System.Drawing.Size(92, 22);
            this.dtpMonth.TabIndex = 22;
            this.dtpMonth.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpMonth.ValueChanged += new System.EventHandler(this.dtpMonth_ValueChanged);
            // 
            // SLNO
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.SLNO.DefaultCellStyle = dataGridViewCellStyle3;
            this.SLNO.Frozen = true;
            this.SLNO.HeaderText = "Sl.No";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SLNO.Width = 50;
            // 
            // LoanNo
            // 
            this.LoanNo.HeaderText = "Loan No";
            this.LoanNo.Name = "LoanNo";
            this.LoanNo.ReadOnly = true;
            this.LoanNo.Visible = false;
            this.LoanNo.Width = 160;
            // 
            // LoanSlNo
            // 
            this.LoanSlNo.HeaderText = "Loan SlNo";
            this.LoanSlNo.Name = "LoanSlNo";
            this.LoanSlNo.ReadOnly = true;
            this.LoanSlNo.Visible = false;
            // 
            // LoanLastSlno
            // 
            this.LoanLastSlno.HeaderText = "Loan LastSlNo";
            this.LoanLastSlno.Name = "LoanLastSlno";
            this.LoanLastSlno.ReadOnly = true;
            this.LoanLastSlno.Visible = false;
            // 
            // ActualEmi
            // 
            this.ActualEmi.HeaderText = "Actual Emi";
            this.ActualEmi.Name = "ActualEmi";
            this.ActualEmi.ReadOnly = true;
            this.ActualEmi.Visible = false;
            // 
            // Ecode
            // 
            this.Ecode.HeaderText = "Ecode";
            this.Ecode.Name = "Ecode";
            this.Ecode.ReadOnly = true;
            this.Ecode.Width = 60;
            // 
            // EmpName
            // 
            this.EmpName.HeaderText = "Emp Name";
            this.EmpName.Name = "EmpName";
            this.EmpName.ReadOnly = true;
            this.EmpName.Width = 150;
            // 
            // Desig
            // 
            this.Desig.HeaderText = "Desig";
            this.Desig.Name = "Desig";
            this.Desig.ReadOnly = true;
            // 
            // LoanType
            // 
            this.LoanType.HeaderText = "Loan Type";
            this.LoanType.Name = "LoanType";
            this.LoanType.ReadOnly = true;
            this.LoanType.Width = 90;
            // 
            // LoanAmt
            // 
            this.LoanAmt.HeaderText = "Loan Amt";
            this.LoanAmt.Name = "LoanAmt";
            this.LoanAmt.ReadOnly = true;
            this.LoanAmt.Width = 70;
            // 
            // RecoveredAmt
            // 
            this.RecoveredAmt.HeaderText = "Recovered";
            this.RecoveredAmt.Name = "RecoveredAmt";
            this.RecoveredAmt.ReadOnly = true;
            this.RecoveredAmt.Width = 80;
            // 
            // BalanceAmt
            // 
            this.BalanceAmt.HeaderText = "Balance";
            this.BalanceAmt.Name = "BalanceAmt";
            this.BalanceAmt.ReadOnly = true;
            this.BalanceAmt.Width = 80;
            // 
            // Emi
            // 
            this.Emi.HeaderText = "EMI";
            this.Emi.Name = "Emi";
            this.Emi.Width = 70;
            // 
            // Remarks
            // 
            this.Remarks.HeaderText = "Remarks";
            this.Remarks.Name = "Remarks";
            // 
            // chkStopReq
            // 
            this.chkStopReq.HeaderText = "Select";
            this.chkStopReq.Name = "chkStopReq";
            this.chkStopReq.Width = 30;
            // 
            // frmEmployeeLoanRecovery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(945, 452);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmployeeLoanRecovery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmEmployeeLoanRecovery";
            this.Load += new System.EventHandler(this.frmEmployeeLoanRecovery_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpLoanRecoveryDetails)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpMonth;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbBranches;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.DataGridView gvEmpLoanRecoveryDetails;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnStopReq;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoanNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoanSlNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoanLastSlno;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActualEmi;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ecode;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desig;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoanType;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoanAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecoveredAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn BalanceAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emi;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkStopReq;
    }
}