namespace SSCRM
{
    partial class frmDoorknocks
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
            this.chkBrAll = new System.Windows.Forms.CheckBox();
            this.chkDemoType = new System.Windows.Forms.CheckedListBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.cbReportType = new System.Windows.Forms.ComboBox();
            this.dtpDocMonth = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDempType = new System.Windows.Forms.Label();
            this.cmleadType = new System.Windows.Forms.ComboBox();
            this.lblLeadType = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.chkBrAll);
            this.groupBox1.Controls.Add(this.chkDemoType);
            this.groupBox1.Controls.Add(this.btnDownload);
            this.groupBox1.Controls.Add(this.webBrowser1);
            this.groupBox1.Controls.Add(this.cbReportType);
            this.groupBox1.Controls.Add(this.dtpDocMonth);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblDempType);
            this.groupBox1.Controls.Add(this.cmleadType);
            this.groupBox1.Controls.Add(this.lblLeadType);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(452, 240);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chkBrAll
            // 
            this.chkBrAll.AutoSize = true;
            this.chkBrAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBrAll.ForeColor = System.Drawing.Color.Navy;
            this.chkBrAll.Location = new System.Drawing.Point(204, 72);
            this.chkBrAll.Name = "chkBrAll";
            this.chkBrAll.Size = new System.Drawing.Size(93, 20);
            this.chkBrAll.TabIndex = 76;
            this.chkBrAll.Text = "Select All";
            this.chkBrAll.UseVisualStyleBackColor = true;
            this.chkBrAll.CheckedChanged += new System.EventHandler(this.chkBrAll_CheckedChanged);
            // 
            // chkDemoType
            // 
            this.chkDemoType.CheckOnClick = true;
            this.chkDemoType.FormattingEnabled = true;
            this.chkDemoType.Items.AddRange(new object[] {
            "CAMPAIGN REQUIRED",
            "TO BE DONE BY SR.",
            "TO BE DONE BY SELF",
            "DEMO GIVEN"});
            this.chkDemoType.Location = new System.Drawing.Point(65, 96);
            this.chkDemoType.Name = "chkDemoType";
            this.chkDemoType.Size = new System.Drawing.Size(317, 94);
            this.chkDemoType.TabIndex = 4;
            this.chkDemoType.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkDemoType_ItemCheck);
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
            this.btnDownload.Location = new System.Drawing.Point(347, 12);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(1);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(97, 24);
            this.btnDownload.TabIndex = 75;
            this.btnDownload.Tag = "Product  Search";
            this.btnDownload.Text = "Download";
            this.btnDownload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(416, 193);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(20, 20);
            this.webBrowser1.TabIndex = 74;
            // 
            // cbReportType
            // 
            this.cbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReportType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbReportType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbReportType.FormattingEnabled = true;
            this.cbReportType.Items.AddRange(new object[] {
            "GROUP BY SR",
            "ORDER BY ORDER-NO"});
            this.cbReportType.Location = new System.Drawing.Point(167, 49);
            this.cbReportType.Name = "cbReportType";
            this.cbReportType.Size = new System.Drawing.Size(165, 23);
            this.cbReportType.Sorted = true;
            this.cbReportType.TabIndex = 73;
            // 
            // dtpDocMonth
            // 
            this.dtpDocMonth.CustomFormat = "MMM/yyyy";
            this.dtpDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDocMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDocMonth.Location = new System.Drawing.Point(168, 23);
            this.dtpDocMonth.Name = "dtpDocMonth";
            this.dtpDocMonth.Size = new System.Drawing.Size(106, 22);
            this.dtpDocMonth.TabIndex = 2;
            this.dtpDocMonth.ValueChanged += new System.EventHandler(this.dtpDocMonth_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(55, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 16);
            this.label3.TabIndex = 72;
            this.label3.Text = "Doc.Month";
            // 
            // lblDempType
            // 
            this.lblDempType.AutoSize = true;
            this.lblDempType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDempType.ForeColor = System.Drawing.Color.Navy;
            this.lblDempType.Location = new System.Drawing.Point(55, 73);
            this.lblDempType.Name = "lblDempType";
            this.lblDempType.Size = new System.Drawing.Size(89, 16);
            this.lblDempType.TabIndex = 69;
            this.lblDempType.Text = "Demo Type";
            // 
            // cmleadType
            // 
            this.cmleadType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmleadType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmleadType.FormattingEnabled = true;
            this.cmleadType.Items.AddRange(new object[] {
            "ALL",
            "NON-PROSPECTIVE",
            "PROSPECTIVE"});
            this.cmleadType.Location = new System.Drawing.Point(166, 48);
            this.cmleadType.Name = "cmleadType";
            this.cmleadType.Size = new System.Drawing.Size(165, 23);
            this.cmleadType.Sorted = true;
            this.cmleadType.TabIndex = 3;
            // 
            // lblLeadType
            // 
            this.lblLeadType.AutoSize = true;
            this.lblLeadType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeadType.ForeColor = System.Drawing.Color.Navy;
            this.lblLeadType.Location = new System.Drawing.Point(55, 50);
            this.lblLeadType.Name = "lblLeadType";
            this.lblLeadType.Size = new System.Drawing.Size(83, 16);
            this.lblLeadType.TabIndex = 60;
            this.lblLeadType.Text = "Lead Type";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnReport);
            this.groupBox3.Location = new System.Drawing.Point(67, 189);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(317, 47);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(178, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Location = new System.Drawing.Point(94, 11);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(78, 30);
            this.btnReport.TabIndex = 0;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // frmDoorknocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 244);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmDoorknocks";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Doorknocks ";
            this.Load += new System.EventHandler(this.frmDoorknocks_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmleadType;
        private System.Windows.Forms.Label lblLeadType;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label lblDempType;
        private System.Windows.Forms.CheckedListBox chkDemoType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDocMonth;
        private System.Windows.Forms.ComboBox cbReportType;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.CheckBox chkBrAll;
    }
}