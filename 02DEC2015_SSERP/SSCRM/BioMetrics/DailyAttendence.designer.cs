namespace SSCRM
{
    partial class DailyAttendence
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dtpWagePerioad = new System.Windows.Forms.DateTimePicker();
            this.dtpFDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.cbReportType = new System.Windows.Forms.ComboBox();
            this.lblrep = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.clbDepartment = new System.Windows.Forms.CheckedListBox();
            this.chkDeptAll = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clbCompany = new System.Windows.Forms.CheckedListBox();
            this.chkCompAll = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.dtpWagePerioad);
            this.groupBox1.Controls.Add(this.dtpFDate);
            this.groupBox1.Controls.Add(this.lblDate);
            this.groupBox1.Controls.Add(this.cbReportType);
            this.groupBox1.Controls.Add(this.lblrep);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(3, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(579, 298);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnClear);
            this.groupBox4.Controls.Add(this.btnReport);
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Location = new System.Drawing.Point(143, 232);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(299, 55);
            this.groupBox4.TabIndex = 114;
            this.groupBox4.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.AutoEllipsis = true;
            this.btnClear.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClear.Location = new System.Drawing.Point(116, 17);
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
            this.btnReport.Location = new System.Drawing.Point(33, 17);
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
            this.btnExit.Location = new System.Drawing.Point(201, 17);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 112;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dtpWagePerioad
            // 
            this.dtpWagePerioad.CustomFormat = "MMM/yyyy";
            this.dtpWagePerioad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpWagePerioad.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpWagePerioad.Location = new System.Drawing.Point(132, 160);
            this.dtpWagePerioad.Name = "dtpWagePerioad";
            this.dtpWagePerioad.Size = new System.Drawing.Size(123, 22);
            this.dtpWagePerioad.TabIndex = 110;
            this.dtpWagePerioad.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // dtpFDate
            // 
            this.dtpFDate.CustomFormat = "dd/MMM/yyyy";
            this.dtpFDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFDate.Location = new System.Drawing.Point(351, 202);
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
            this.lblDate.Location = new System.Drawing.Point(305, 205);
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
            "MTD ATTENDENCE",
            "DAILY ATTENDENCE"});
            this.cbReportType.Location = new System.Drawing.Point(128, 203);
            this.cbReportType.Name = "cbReportType";
            this.cbReportType.Size = new System.Drawing.Size(170, 24);
            this.cbReportType.TabIndex = 107;
            this.cbReportType.SelectedIndexChanged += new System.EventHandler(this.cbReportType_SelectedIndexChanged);
            // 
            // lblrep
            // 
            this.lblrep.AutoSize = true;
            this.lblrep.BackColor = System.Drawing.Color.PowderBlue;
            this.lblrep.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblrep.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblrep.Location = new System.Drawing.Point(35, 205);
            this.lblrep.Name = "lblrep";
            this.lblrep.Size = new System.Drawing.Size(91, 16);
            this.lblrep.TabIndex = 106;
            this.lblrep.Text = "ReportType";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.PowderBlue;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(30, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 16);
            this.label5.TabIndex = 32;
            this.label5.Text = "Wage Period";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.clbDepartment);
            this.groupBox3.Controls.Add(this.chkDeptAll);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Navy;
            this.groupBox3.Location = new System.Drawing.Point(289, 17);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(262, 123);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Department";
            // 
            // clbDepartment
            // 
            this.clbDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbDepartment.FormattingEnabled = true;
            this.clbDepartment.Location = new System.Drawing.Point(8, 19);
            this.clbDepartment.Name = "clbDepartment";
            this.clbDepartment.Size = new System.Drawing.Size(245, 100);
            this.clbDepartment.TabIndex = 2;
            this.clbDepartment.SelectedIndexChanged += new System.EventHandler(this.clbDepartment_SelectedIndexChanged);
            this.clbDepartment.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbDepartment_ItemCheck);
            // 
            // chkDeptAll
            // 
            this.chkDeptAll.AutoSize = true;
            this.chkDeptAll.ForeColor = System.Drawing.Color.Green;
            this.chkDeptAll.Location = new System.Drawing.Point(113, 1);
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
            this.groupBox2.Location = new System.Drawing.Point(21, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(262, 122);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Company";
            // 
            // clbCompany
            // 
            this.clbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbCompany.FormattingEnabled = true;
            this.clbCompany.Location = new System.Drawing.Point(6, 18);
            this.clbCompany.Name = "clbCompany";
            this.clbCompany.Size = new System.Drawing.Size(250, 100);
            this.clbCompany.TabIndex = 1;
            this.clbCompany.SelectedIndexChanged += new System.EventHandler(this.clbCompany_SelectedIndexChanged);
            this.clbCompany.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbCompany_ItemCheck);
            // 
            // chkCompAll
            // 
            this.chkCompAll.AutoSize = true;
            this.chkCompAll.ForeColor = System.Drawing.Color.Green;
            this.chkCompAll.Location = new System.Drawing.Point(110, 0);
            this.chkCompAll.Name = "chkCompAll";
            this.chkCompAll.Size = new System.Drawing.Size(45, 22);
            this.chkCompAll.TabIndex = 0;
            this.chkCompAll.Text = "All";
            this.chkCompAll.UseVisualStyleBackColor = true;
            this.chkCompAll.CheckedChanged += new System.EventHandler(this.chkCompAll_CheckedChanged);
            // 
            // DailyAttendence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(585, 298);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "DailyAttendence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DailyAttendence";
            this.Load += new System.EventHandler(this.DailyAttendence_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkCompAll;
        private System.Windows.Forms.CheckBox chkDeptAll;
        private System.Windows.Forms.CheckedListBox clbDepartment;
        private System.Windows.Forms.CheckedListBox clbCompany;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbReportType;
        private System.Windows.Forms.Label lblrep;
        private System.Windows.Forms.DateTimePicker dtpFDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpWagePerioad;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnExit;
    }
}