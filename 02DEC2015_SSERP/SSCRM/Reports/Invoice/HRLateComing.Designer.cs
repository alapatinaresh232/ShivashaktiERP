namespace SSCRM
{
    partial class HRLateComing
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
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpRepFrDate = new System.Windows.Forms.DateTimePicker();
            this.dtpRepToDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbReportType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEarlyGo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLateComing = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.clbBranch = new System.Windows.Forms.CheckedListBox();
            this.ChkBranch = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clbCompany = new System.Windows.Forms.CheckedListBox();
            this.chkCompanyAll = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpRepFrDate);
            this.groupBox1.Controls.Add(this.dtpRepToDate);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbReportType);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtEarlyGo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtLateComing);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Location = new System.Drawing.Point(1, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(665, 302);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(481, 202);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 17);
            this.label6.TabIndex = 111;
            this.label6.Text = "mins";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(261, 202);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 17);
            this.label5.TabIndex = 110;
            this.label5.Text = "mins";
            // 
            // dtpRepFrDate
            // 
            this.dtpRepFrDate.CustomFormat = "dd/MM/yyyy";
            this.dtpRepFrDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpRepFrDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRepFrDate.Location = new System.Drawing.Point(167, 172);
            this.dtpRepFrDate.Name = "dtpRepFrDate";
            this.dtpRepFrDate.Size = new System.Drawing.Size(94, 22);
            this.dtpRepFrDate.TabIndex = 108;
            this.dtpRepFrDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpRepFrDate.ValueChanged += new System.EventHandler(this.dtpRepFrDate_ValueChanged);
            // 
            // dtpRepToDate
            // 
            this.dtpRepToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpRepToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpRepToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRepToDate.Location = new System.Drawing.Point(383, 172);
            this.dtpRepToDate.Name = "dtpRepToDate";
            this.dtpRepToDate.Size = new System.Drawing.Size(94, 22);
            this.dtpRepToDate.TabIndex = 109;
            this.dtpRepToDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpRepToDate.ValueChanged += new System.EventHandler(this.dtpRepToDate_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(356, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 17);
            this.label4.TabIndex = 107;
            this.label4.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(73, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 17);
            this.label3.TabIndex = 106;
            this.label3.Text = "ReportFrom";
            // 
            // cbReportType
            // 
            this.cbReportType.FormattingEnabled = true;
            this.cbReportType.Items.AddRange(new object[] {
            "ALL",
            "LATE"});
            this.cbReportType.Location = new System.Drawing.Point(166, 227);
            this.cbReportType.Name = "cbReportType";
            this.cbReportType.Size = new System.Drawing.Size(95, 21);
            this.cbReportType.TabIndex = 104;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(67, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 17);
            this.label8.TabIndex = 105;
            this.label8.Text = "Report Type";
            // 
            // txtEarlyGo
            // 
            this.txtEarlyGo.Location = new System.Drawing.Point(382, 201);
            this.txtEarlyGo.Name = "txtEarlyGo";
            this.txtEarlyGo.Size = new System.Drawing.Size(95, 20);
            this.txtEarlyGo.TabIndex = 10;
            this.txtEarlyGo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEarlyGo_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(308, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "EearlyGo";
            // 
            // txtLateComing
            // 
            this.txtLateComing.Location = new System.Drawing.Point(166, 201);
            this.txtLateComing.Name = "txtLateComing";
            this.txtLateComing.Size = new System.Drawing.Size(95, 20);
            this.txtLateComing.TabIndex = 8;
            this.txtLateComing.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLateComing_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(72, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "LateComing";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.clbBranch);
            this.groupBox3.Controls.Add(this.ChkBranch);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Navy;
            this.groupBox3.Location = new System.Drawing.Point(337, 14);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(312, 149);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // clbBranch
            // 
            this.clbBranch.CheckOnClick = true;
            this.clbBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbBranch.FormattingEnabled = true;
            this.clbBranch.Location = new System.Drawing.Point(12, 23);
            this.clbBranch.Name = "clbBranch";
            this.clbBranch.Size = new System.Drawing.Size(287, 116);
            this.clbBranch.TabIndex = 2;
            this.clbBranch.SelectedIndexChanged += new System.EventHandler(this.clbBranch_SelectedIndexChanged);
            // 
            // ChkBranch
            // 
            this.ChkBranch.AutoSize = true;
            this.ChkBranch.Location = new System.Drawing.Point(18, 0);
            this.ChkBranch.Name = "ChkBranch";
            this.ChkBranch.Size = new System.Drawing.Size(78, 21);
            this.ChkBranch.TabIndex = 1;
            this.ChkBranch.Text = "Branch";
            this.ChkBranch.UseVisualStyleBackColor = true;
            this.ChkBranch.CheckedChanged += new System.EventHandler(this.ChkBranch_CheckedChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnClear);
            this.groupBox7.Controls.Add(this.btnClose);
            this.groupBox7.Controls.Add(this.btnDisplay);
            this.groupBox7.Location = new System.Drawing.Point(181, 251);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(250, 43);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.Navy;
            this.btnClear.Location = new System.Drawing.Point(89, 13);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(73, 26);
            this.btnClear.TabIndex = 101;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.Navy;
            this.btnClose.Location = new System.Drawing.Point(170, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 26);
            this.btnClose.TabIndex = 100;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDisplay
            // 
            this.btnDisplay.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDisplay.ForeColor = System.Drawing.Color.Navy;
            this.btnDisplay.Location = new System.Drawing.Point(7, 13);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(73, 26);
            this.btnDisplay.TabIndex = 0;
            this.btnDisplay.Text = "Report";
            this.btnDisplay.UseVisualStyleBackColor = false;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.Controls.Add(this.clbCompany);
            this.groupBox2.Controls.Add(this.chkCompanyAll);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(19, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(309, 149);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // clbCompany
            // 
            this.clbCompany.CheckOnClick = true;
            this.clbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbCompany.FormattingEnabled = true;
            this.clbCompany.Location = new System.Drawing.Point(11, 23);
            this.clbCompany.Name = "clbCompany";
            this.clbCompany.Size = new System.Drawing.Size(287, 116);
            this.clbCompany.TabIndex = 2;
            this.clbCompany.SelectedIndexChanged += new System.EventHandler(this.clbCompany_SelectedIndexChanged);
            // 
            // chkCompanyAll
            // 
            this.chkCompanyAll.AutoSize = true;
            this.chkCompanyAll.Location = new System.Drawing.Point(18, 0);
            this.chkCompanyAll.Name = "chkCompanyAll";
            this.chkCompanyAll.Size = new System.Drawing.Size(93, 21);
            this.chkCompanyAll.TabIndex = 1;
            this.chkCompanyAll.Text = "Company";
            this.chkCompanyAll.UseVisualStyleBackColor = true;
            this.chkCompanyAll.CheckedChanged += new System.EventHandler(this.chkCompanyAll_CheckedChanged);
            // 
            // HRLateComing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(667, 301);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "HRLateComing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HR-LateComing :: Report";
            this.Load += new System.EventHandler(this.HRLateComing_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox clbBranch;
        private System.Windows.Forms.CheckBox ChkBranch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox clbCompany;
        private System.Windows.Forms.CheckBox chkCompanyAll;
        private System.Windows.Forms.TextBox txtEarlyGo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLateComing;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbReportType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpRepFrDate;
        private System.Windows.Forms.DateTimePicker dtpRepToDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}