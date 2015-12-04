namespace SDMS
{
    partial class DealersSearch
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
            this.cbReportType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDalerTo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDalerFrom = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCreditLimitGreater = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCreditLimitLesser = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.clbBussinesType = new System.Windows.Forms.CheckedListBox();
            this.chkBussinessTypeAll = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.clbFirmType = new System.Windows.Forms.CheckedListBox();
            this.chkFirmTypeAll = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkDistrictAll = new System.Windows.Forms.CheckBox();
            this.clbDistrict = new System.Windows.Forms.CheckedListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.clbState = new System.Windows.Forms.CheckedListBox();
            this.chkStateAll = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clbCompany = new System.Windows.Forms.CheckedListBox();
            this.chkCompanyAll = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.cbReportType);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtDalerTo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtDalerFrom);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCreditLimitGreater);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCreditLimitLesser);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(1, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(661, 398);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cbReportType
            // 
            this.cbReportType.FormattingEnabled = true;
            this.cbReportType.Items.AddRange(new object[] {
            "SUMMARY",
            "DETAILED"});
            this.cbReportType.Location = new System.Drawing.Point(136, 302);
            this.cbReportType.Name = "cbReportType";
            this.cbReportType.Size = new System.Drawing.Size(121, 21);
            this.cbReportType.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(37, 303);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 17);
            this.label8.TabIndex = 103;
            this.label8.Text = "Report Type";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnClose);
            this.groupBox7.Controls.Add(this.btnDisplay);
            this.groupBox7.Location = new System.Drawing.Point(175, 332);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(305, 55);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.Navy;
            this.btnClose.Location = new System.Drawing.Point(169, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 34);
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
            this.btnDisplay.Location = new System.Drawing.Point(51, 15);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(93, 34);
            this.btnDisplay.TabIndex = 0;
            this.btnDisplay.Text = "Report";
            this.btnDisplay.UseVisualStyleBackColor = false;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(479, 269);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 17);
            this.label7.TabIndex = 17;
            this.label7.Text = "months";
            // 
            // txtDalerTo
            // 
            this.txtDalerTo.Location = new System.Drawing.Point(353, 267);
            this.txtDalerTo.Name = "txtDalerTo";
            this.txtDalerTo.Size = new System.Drawing.Size(120, 20);
            this.txtDalerTo.TabIndex = 3;
            this.txtDalerTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDalerTo_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(324, 269);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "To";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(257, 269);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "months";
            // 
            // txtDalerFrom
            // 
            this.txtDalerFrom.Location = new System.Drawing.Point(135, 269);
            this.txtDalerFrom.Name = "txtDalerFrom";
            this.txtDalerFrom.Size = new System.Drawing.Size(120, 20);
            this.txtDalerFrom.TabIndex = 2;
            this.txtDalerFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDalerFrom_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(8, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Dealership From";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(325, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "<=";
            // 
            // txtCreditLimitGreater
            // 
            this.txtCreditLimitGreater.Location = new System.Drawing.Point(352, 235);
            this.txtCreditLimitGreater.Name = "txtCreditLimitGreater";
            this.txtCreditLimitGreater.Size = new System.Drawing.Size(120, 20);
            this.txtCreditLimitGreater.TabIndex = 1;
            this.txtCreditLimitGreater.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCreditLimitGreater_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(259, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = ">=";
            // 
            // txtCreditLimitLesser
            // 
            this.txtCreditLimitLesser.Location = new System.Drawing.Point(135, 235);
            this.txtCreditLimitLesser.Name = "txtCreditLimitLesser";
            this.txtCreditLimitLesser.Size = new System.Drawing.Size(120, 20);
            this.txtCreditLimitLesser.TabIndex = 0;
            this.txtCreditLimitLesser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCreditLimitLesser_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(45, 236);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Credit Limit";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.clbBussinesType);
            this.groupBox6.Controls.Add(this.chkBussinessTypeAll);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.ForeColor = System.Drawing.Color.Navy;
            this.groupBox6.Location = new System.Drawing.Point(30, 125);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(248, 95);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            // 
            // clbBussinesType
            // 
            this.clbBussinesType.CheckOnClick = true;
            this.clbBussinesType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbBussinesType.FormattingEnabled = true;
            this.clbBussinesType.Items.AddRange(new object[] {
            "RETAIL",
            "WHOLESALE"});
            this.clbBussinesType.Location = new System.Drawing.Point(12, 21);
            this.clbBussinesType.Name = "clbBussinesType";
            this.clbBussinesType.Size = new System.Drawing.Size(223, 68);
            this.clbBussinesType.TabIndex = 2;
            this.clbBussinesType.SelectedIndexChanged += new System.EventHandler(this.clbBussinesType_SelectedIndexChanged);
            // 
            // chkBussinessTypeAll
            // 
            this.chkBussinessTypeAll.AutoSize = true;
            this.chkBussinessTypeAll.Location = new System.Drawing.Point(18, 0);
            this.chkBussinessTypeAll.Name = "chkBussinessTypeAll";
            this.chkBussinessTypeAll.Size = new System.Drawing.Size(141, 21);
            this.chkBussinessTypeAll.TabIndex = 1;
            this.chkBussinessTypeAll.Text = "Bussiness Type";
            this.chkBussinessTypeAll.UseVisualStyleBackColor = true;
            this.chkBussinessTypeAll.CheckedChanged += new System.EventHandler(this.chkBussinessTypeAll_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.clbFirmType);
            this.groupBox5.Controls.Add(this.chkFirmTypeAll);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.Color.Navy;
            this.groupBox5.Location = new System.Drawing.Point(291, 128);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(284, 92);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            // 
            // clbFirmType
            // 
            this.clbFirmType.CheckOnClick = true;
            this.clbFirmType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbFirmType.FormattingEnabled = true;
            this.clbFirmType.Items.AddRange(new object[] {
            "PUBLIC LTD",
            "PVT LTD",
            "PARTNERSHIP",
            "SOLE PROPRIETORSHIP"});
            this.clbFirmType.Location = new System.Drawing.Point(12, 19);
            this.clbFirmType.Name = "clbFirmType";
            this.clbFirmType.Size = new System.Drawing.Size(259, 68);
            this.clbFirmType.TabIndex = 2;
            this.clbFirmType.SelectedIndexChanged += new System.EventHandler(this.clbFirmType_SelectedIndexChanged);
            // 
            // chkFirmTypeAll
            // 
            this.chkFirmTypeAll.AutoSize = true;
            this.chkFirmTypeAll.Location = new System.Drawing.Point(18, 0);
            this.chkFirmTypeAll.Name = "chkFirmTypeAll";
            this.chkFirmTypeAll.Size = new System.Drawing.Size(99, 21);
            this.chkFirmTypeAll.TabIndex = 1;
            this.chkFirmTypeAll.Text = "Firm Type";
            this.chkFirmTypeAll.UseVisualStyleBackColor = true;
            this.chkFirmTypeAll.CheckedChanged += new System.EventHandler(this.chkFirmTypeAll_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkDistrictAll);
            this.groupBox4.Controls.Add(this.clbDistrict);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Navy;
            this.groupBox4.Location = new System.Drawing.Point(485, 11);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(153, 113);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            // 
            // chkDistrictAll
            // 
            this.chkDistrictAll.AutoSize = true;
            this.chkDistrictAll.Location = new System.Drawing.Point(12, 0);
            this.chkDistrictAll.Name = "chkDistrictAll";
            this.chkDistrictAll.Size = new System.Drawing.Size(78, 21);
            this.chkDistrictAll.TabIndex = 1;
            this.chkDistrictAll.Text = "District";
            this.chkDistrictAll.UseVisualStyleBackColor = true;
            this.chkDistrictAll.CheckedChanged += new System.EventHandler(this.chkDistrictAll_CheckedChanged);
            // 
            // clbDistrict
            // 
            this.clbDistrict.CheckOnClick = true;
            this.clbDistrict.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbDistrict.FormattingEnabled = true;
            this.clbDistrict.Location = new System.Drawing.Point(12, 21);
            this.clbDistrict.Name = "clbDistrict";
            this.clbDistrict.Size = new System.Drawing.Size(128, 84);
            this.clbDistrict.TabIndex = 2;
            this.clbDistrict.SelectedIndexChanged += new System.EventHandler(this.clbDistrict_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.clbState);
            this.groupBox3.Controls.Add(this.chkStateAll);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Navy;
            this.groupBox3.Location = new System.Drawing.Point(284, 14);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(185, 108);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // clbState
            // 
            this.clbState.CheckOnClick = true;
            this.clbState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbState.FormattingEnabled = true;
            this.clbState.Location = new System.Drawing.Point(13, 17);
            this.clbState.Name = "clbState";
            this.clbState.Size = new System.Drawing.Size(161, 84);
            this.clbState.TabIndex = 3;
            this.clbState.SelectedIndexChanged += new System.EventHandler(this.clbState_SelectedIndexChanged);
            // 
            // chkStateAll
            // 
            this.chkStateAll.AutoSize = true;
            this.chkStateAll.Location = new System.Drawing.Point(18, 0);
            this.chkStateAll.Name = "chkStateAll";
            this.chkStateAll.Size = new System.Drawing.Size(65, 21);
            this.chkStateAll.TabIndex = 1;
            this.chkStateAll.Text = "State";
            this.chkStateAll.UseVisualStyleBackColor = true;
            this.chkStateAll.CheckedChanged += new System.EventHandler(this.chkStateAll_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clbCompany);
            this.groupBox2.Controls.Add(this.chkCompanyAll);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(30, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(248, 110);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // clbCompany
            // 
            this.clbCompany.CheckOnClick = true;
            this.clbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbCompany.FormattingEnabled = true;
            this.clbCompany.Location = new System.Drawing.Point(12, 19);
            this.clbCompany.Name = "clbCompany";
            this.clbCompany.Size = new System.Drawing.Size(223, 84);
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
            // DealersSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(664, 400);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "DealersSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DealersSearch :: Report";
            this.Load += new System.EventHandler(this.DealersSearch_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox clbCompany;
        private System.Windows.Forms.CheckBox chkCompanyAll;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckedListBox clbBussinesType;
        private System.Windows.Forms.CheckBox chkBussinessTypeAll;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckedListBox clbFirmType;
        private System.Windows.Forms.CheckBox chkFirmTypeAll;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkDistrictAll;
        private System.Windows.Forms.CheckedListBox clbDistrict;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkStateAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCreditLimitGreater;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCreditLimitLesser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDalerFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox clbState;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDalerTo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.ComboBox cbReportType;
        private System.Windows.Forms.Label label8;
    }
}