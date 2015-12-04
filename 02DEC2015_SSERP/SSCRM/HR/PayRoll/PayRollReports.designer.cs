namespace SSCRM
{
    partial class PayRollReports
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbWagePeriod = new System.Windows.Forms.ComboBox();
            this.grpEmployees = new System.Windows.Forms.GroupBox();
            this.txtDsearch = new System.Windows.Forms.TextBox();
            this.tvEmployees = new SSCRM.TriStateTreeView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.clbBranch = new System.Windows.Forms.CheckedListBox();
            this.chkBranchAll = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dtpFDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.cbReportType = new System.Windows.Forms.ComboBox();
            this.lblrep = new System.Windows.Forms.Label();
            this.lblWagePeriod = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.clbDepartment = new System.Windows.Forms.CheckedListBox();
            this.chkDeptAll = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clbCompany = new System.Windows.Forms.CheckedListBox();
            this.chkCompAll = new System.Windows.Forms.CheckBox();
            this.nmYear = new System.Windows.Forms.NumericUpDown();
            this.lblYear = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.grpEmployees.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmYear)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.nmYear);
            this.groupBox1.Controls.Add(this.lblYear);
            this.groupBox1.Controls.Add(this.cbWagePeriod);
            this.groupBox1.Controls.Add(this.grpEmployees);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.dtpFDate);
            this.groupBox1.Controls.Add(this.lblDate);
            this.groupBox1.Controls.Add(this.cbReportType);
            this.groupBox1.Controls.Add(this.lblrep);
            this.groupBox1.Controls.Add(this.lblWagePeriod);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(3, -6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(779, 443);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // cbWagePeriod
            // 
            this.cbWagePeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWagePeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWagePeriod.FormattingEnabled = true;
            this.cbWagePeriod.Location = new System.Drawing.Point(117, 14);
            this.cbWagePeriod.Name = "cbWagePeriod";
            this.cbWagePeriod.Size = new System.Drawing.Size(116, 24);
            this.cbWagePeriod.TabIndex = 117;
            this.cbWagePeriod.SelectedIndexChanged += new System.EventHandler(this.cbWagePeriod_SelectedIndexChanged);
            // 
            // grpEmployees
            // 
            this.grpEmployees.Controls.Add(this.txtDsearch);
            this.grpEmployees.Controls.Add(this.tvEmployees);
            this.grpEmployees.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpEmployees.ForeColor = System.Drawing.Color.Navy;
            this.grpEmployees.Location = new System.Drawing.Point(378, 175);
            this.grpEmployees.Name = "grpEmployees";
            this.grpEmployees.Size = new System.Drawing.Size(382, 188);
            this.grpEmployees.TabIndex = 3;
            this.grpEmployees.TabStop = false;
            this.grpEmployees.Text = "Employees";
            // 
            // txtDsearch
            // 
            this.txtDsearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDsearch.Location = new System.Drawing.Point(106, -3);
            this.txtDsearch.Name = "txtDsearch";
            this.txtDsearch.Size = new System.Drawing.Size(225, 22);
            this.txtDsearch.TabIndex = 47;
            this.txtDsearch.Visible = false;
            // 
            // tvEmployees
            // 
            this.tvEmployees.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvEmployees.Location = new System.Drawing.Point(7, 25);
            this.tvEmployees.Name = "tvEmployees";
            this.tvEmployees.Size = new System.Drawing.Size(368, 157);
            this.tvEmployees.TabIndex = 117;
            this.tvEmployees.TriStateStyleProperty = SSCRM.TriStateTreeView.TriStateStyles.Standard;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.clbBranch);
            this.groupBox5.Controls.Add(this.chkBranchAll);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.Color.Navy;
            this.groupBox5.Location = new System.Drawing.Point(11, 175);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(361, 186);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Branch";
            // 
            // clbBranch
            // 
            this.clbBranch.CheckOnClick = true;
            this.clbBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbBranch.FormattingEnabled = true;
            this.clbBranch.Location = new System.Drawing.Point(4, 18);
            this.clbBranch.Name = "clbBranch";
            this.clbBranch.Size = new System.Drawing.Size(351, 164);
            this.clbBranch.TabIndex = 1;
            this.clbBranch.SelectedIndexChanged += new System.EventHandler(this.clbBranch_SelectedIndexChanged);
            this.clbBranch.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbBranch_ItemCheck);
            // 
            // chkBranchAll
            // 
            this.chkBranchAll.AutoSize = true;
            this.chkBranchAll.ForeColor = System.Drawing.Color.Green;
            this.chkBranchAll.Location = new System.Drawing.Point(307, -3);
            this.chkBranchAll.Name = "chkBranchAll";
            this.chkBranchAll.Size = new System.Drawing.Size(45, 22);
            this.chkBranchAll.TabIndex = 0;
            this.chkBranchAll.Text = "All";
            this.chkBranchAll.UseVisualStyleBackColor = true;
            this.chkBranchAll.CheckedChanged += new System.EventHandler(this.chkBranchAll_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnDownload);
            this.groupBox4.Controls.Add(this.btnClear);
            this.groupBox4.Controls.Add(this.btnReport);
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Location = new System.Drawing.Point(202, 399);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(407, 55);
            this.groupBox4.TabIndex = 114;
            this.groupBox4.TabStop = false;
            // 
            // btnDownload
            // 
            this.btnDownload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDownload.BackColor = System.Drawing.Color.OliveDrab;
            this.btnDownload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnDownload.FlatAppearance.BorderSize = 5;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Image = global::SSCRM.Properties.Resources.ic_download;
            this.btnDownload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDownload.Location = new System.Drawing.Point(35, 18);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(1);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(97, 24);
            this.btnDownload.TabIndex = 114;
            this.btnDownload.Tag = "Product  Search";
            this.btnDownload.Text = "Download";
            this.btnDownload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnClear
            // 
            this.btnClear.AutoEllipsis = true;
            this.btnClear.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClear.Location = new System.Drawing.Point(218, 17);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(74, 26);
            this.btnClear.TabIndex = 113;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnReport
            // 
            this.btnReport.AutoEllipsis = true;
            this.btnReport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnReport.Location = new System.Drawing.Point(138, 17);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(74, 26);
            this.btnReport.TabIndex = 111;
            this.btnReport.Text = "&Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(298, 17);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 112;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dtpFDate
            // 
            this.dtpFDate.CustomFormat = "dd/MMM/yyyy";
            this.dtpFDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFDate.Location = new System.Drawing.Point(614, 376);
            this.dtpFDate.Name = "dtpFDate";
            this.dtpFDate.Size = new System.Drawing.Size(133, 22);
            this.dtpFDate.TabIndex = 109;
            this.dtpFDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpFDate.Visible = false;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblDate.Location = new System.Drawing.Point(568, 379);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(41, 16);
            this.lblDate.TabIndex = 108;
            this.lblDate.Text = "Date";
            this.lblDate.Visible = false;
            // 
            // cbReportType
            // 
            this.cbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReportType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbReportType.FormattingEnabled = true;
            this.cbReportType.Items.AddRange(new object[] {
            "--Select--",
            "PAY SLIP",
            "BANK PAYMENT REGISTER",
            "CASH PAYMENT REGISTER",
            "PAYMENT DEDUCTIONS REPORT",
            "ESI REGISTER",
            "PROFIT TAX REGISTER",
            "SALARIES  LIST COMP & BANK WISE"});
            this.cbReportType.Location = new System.Drawing.Point(142, 374);
            this.cbReportType.Name = "cbReportType";
            this.cbReportType.Size = new System.Drawing.Size(337, 24);
            this.cbReportType.TabIndex = 107;
            // 
            // lblrep
            // 
            this.lblrep.AutoSize = true;
            this.lblrep.BackColor = System.Drawing.Color.PowderBlue;
            this.lblrep.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblrep.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblrep.Location = new System.Drawing.Point(49, 376);
            this.lblrep.Name = "lblrep";
            this.lblrep.Size = new System.Drawing.Size(91, 16);
            this.lblrep.TabIndex = 106;
            this.lblrep.Text = "ReportType";
            // 
            // lblWagePeriod
            // 
            this.lblWagePeriod.AutoSize = true;
            this.lblWagePeriod.BackColor = System.Drawing.Color.PowderBlue;
            this.lblWagePeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWagePeriod.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblWagePeriod.Location = new System.Drawing.Point(16, 17);
            this.lblWagePeriod.Name = "lblWagePeriod";
            this.lblWagePeriod.Size = new System.Drawing.Size(99, 16);
            this.lblWagePeriod.TabIndex = 32;
            this.lblWagePeriod.Text = "Wage Period";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.clbDepartment);
            this.groupBox3.Controls.Add(this.chkDeptAll);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Navy;
            this.groupBox3.Location = new System.Drawing.Point(378, 39);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(382, 123);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Department";
            // 
            // clbDepartment
            // 
            this.clbDepartment.CheckOnClick = true;
            this.clbDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbDepartment.FormattingEnabled = true;
            this.clbDepartment.Location = new System.Drawing.Point(8, 19);
            this.clbDepartment.Name = "clbDepartment";
            this.clbDepartment.Size = new System.Drawing.Size(368, 100);
            this.clbDepartment.TabIndex = 2;
            this.clbDepartment.SelectedIndexChanged += new System.EventHandler(this.clbDepartment_SelectedIndexChanged);
            this.clbDepartment.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbDepartment_ItemCheck);
            // 
            // chkDeptAll
            // 
            this.chkDeptAll.AutoSize = true;
            this.chkDeptAll.ForeColor = System.Drawing.Color.Green;
            this.chkDeptAll.Location = new System.Drawing.Point(329, -2);
            this.chkDeptAll.Name = "chkDeptAll";
            this.chkDeptAll.Size = new System.Drawing.Size(45, 22);
            this.chkDeptAll.TabIndex = 1;
            this.chkDeptAll.Text = "All";
            this.chkDeptAll.UseVisualStyleBackColor = true;
            this.chkDeptAll.CheckedChanged += new System.EventHandler(this.chkDeptAll_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clbCompany);
            this.groupBox2.Controls.Add(this.chkCompAll);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(12, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(360, 122);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Company";
            // 
            // clbCompany
            // 
            this.clbCompany.CheckOnClick = true;
            this.clbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbCompany.FormattingEnabled = true;
            this.clbCompany.Location = new System.Drawing.Point(6, 18);
            this.clbCompany.Name = "clbCompany";
            this.clbCompany.Size = new System.Drawing.Size(348, 100);
            this.clbCompany.TabIndex = 1;
            this.clbCompany.SelectedIndexChanged += new System.EventHandler(this.clbCompany_SelectedIndexChanged);
            this.clbCompany.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbCompany_ItemCheck);
            // 
            // chkCompAll
            // 
            this.chkCompAll.AutoSize = true;
            this.chkCompAll.ForeColor = System.Drawing.Color.Green;
            this.chkCompAll.Location = new System.Drawing.Point(309, -4);
            this.chkCompAll.Name = "chkCompAll";
            this.chkCompAll.Size = new System.Drawing.Size(45, 22);
            this.chkCompAll.TabIndex = 0;
            this.chkCompAll.Text = "All";
            this.chkCompAll.UseVisualStyleBackColor = true;
            this.chkCompAll.CheckedChanged += new System.EventHandler(this.chkCompAll_CheckedChanged);
            // 
            // nmYear
            // 
            this.nmYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nmYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmYear.Location = new System.Drawing.Point(117, 15);
            this.nmYear.Maximum = new decimal(new int[] {
            2020,
            0,
            0,
            0});
            this.nmYear.Name = "nmYear";
            this.nmYear.Size = new System.Drawing.Size(72, 22);
            this.nmYear.TabIndex = 119;
            this.nmYear.Value = new decimal(new int[] {
            2014,
            0,
            0,
            0});
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYear.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblYear.Location = new System.Drawing.Point(67, 18);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(41, 16);
            this.lblYear.TabIndex = 118;
            this.lblYear.Text = "Year";
            // 
            // PayRollReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "PayRollReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PayRoll Reports";
            this.Load += new System.EventHandler(this.PayRollReports_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpEmployees.ResumeLayout(false);
            this.grpEmployees.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmYear)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DateTimePicker dtpFDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.ComboBox cbReportType;
        private System.Windows.Forms.Label lblrep;
        private System.Windows.Forms.Label lblWagePeriod;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox clbDepartment;
        private System.Windows.Forms.CheckBox chkDeptAll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox clbCompany;
        private System.Windows.Forms.CheckBox chkCompAll;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckedListBox clbBranch;
        private System.Windows.Forms.CheckBox chkBranchAll;
        private System.Windows.Forms.GroupBox grpEmployees;
        private System.Windows.Forms.ComboBox cbWagePeriod;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.TextBox txtDsearch;
        private TriStateTreeView tvEmployees;
        private System.Windows.Forms.NumericUpDown nmYear;
        private System.Windows.Forms.Label lblYear;

    }
}