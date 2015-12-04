namespace SSCRM
{
    partial class ServiceReportByTrnNo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbReportType = new System.Windows.Forms.ComboBox();
            this.lblReport = new System.Windows.Forms.Label();
            this.btnDisplayPromotionDetails = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblReportName = new System.Windows.Forms.Label();
            this.gvTransactionDetails = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrnNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConductedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CampName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductsCnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DemosCnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OthersCnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompanyStaffCnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Print = new System.Windows.Forms.DataGridViewImageColumn();
            this.cbBranches = new System.Windows.Forms.ComboBox();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.dtpDocMonth = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransactionDetails)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.dtpDocMonth);
            this.groupBox1.Controls.Add(this.cbReportType);
            this.groupBox1.Controls.Add(this.lblReport);
            this.groupBox1.Controls.Add(this.btnDisplayPromotionDetails);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblReportName);
            this.groupBox1.Controls.Add(this.gvTransactionDetails);
            this.groupBox1.Controls.Add(this.cbBranches);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(935, 494);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // cbReportType
            // 
            this.cbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReportType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbReportType.FormattingEnabled = true;
            this.cbReportType.Items.AddRange(new object[] {
            "--Select--",
            "DEMO PLOTS",
            "FARMER MEETINGS",
            "SCHOOL VISITS",
            ""});
            this.cbReportType.Location = new System.Drawing.Point(99, 14);
            this.cbReportType.Name = "cbReportType";
            this.cbReportType.Size = new System.Drawing.Size(172, 24);
            this.cbReportType.TabIndex = 165;
            this.cbReportType.SelectedIndexChanged += new System.EventHandler(this.cbReportType_SelectedIndexChanged);
            // 
            // lblReport
            // 
            this.lblReport.AutoSize = true;
            this.lblReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReport.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblReport.Location = new System.Drawing.Point(1, 16);
            this.lblReport.Name = "lblReport";
            this.lblReport.Size = new System.Drawing.Size(98, 17);
            this.lblReport.TabIndex = 164;
            this.lblReport.Text = "Report Type";
            // 
            // btnDisplayPromotionDetails
            // 
            this.btnDisplayPromotionDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDisplayPromotionDetails.BackColor = System.Drawing.Color.Green;
            this.btnDisplayPromotionDetails.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnDisplayPromotionDetails.FlatAppearance.BorderSize = 5;
            this.btnDisplayPromotionDetails.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDisplayPromotionDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplayPromotionDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDisplayPromotionDetails.Location = new System.Drawing.Point(856, 44);
            this.btnDisplayPromotionDetails.Margin = new System.Windows.Forms.Padding(1);
            this.btnDisplayPromotionDetails.Name = "btnDisplayPromotionDetails";
            this.btnDisplayPromotionDetails.Size = new System.Drawing.Size(72, 24);
            this.btnDisplayPromotionDetails.TabIndex = 163;
            this.btnDisplayPromotionDetails.Tag = "";
            this.btnDisplayPromotionDetails.Text = "Display";
            this.btnDisplayPromotionDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisplayPromotionDetails.UseVisualStyleBackColor = false;
            this.btnDisplayPromotionDetails.Click += new System.EventHandler(this.btnDisplayPromotionDetails_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(733, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 161;
            this.label1.Text = "Doc Month";
            // 
            // lblReportName
            // 
            this.lblReportName.AutoSize = true;
            this.lblReportName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportName.ForeColor = System.Drawing.Color.Navy;
            this.lblReportName.Location = new System.Drawing.Point(7, 76);
            this.lblReportName.Name = "lblReportName";
            this.lblReportName.Size = new System.Drawing.Size(0, 17);
            this.lblReportName.TabIndex = 140;
            // 
            // gvTransactionDetails
            // 
            this.gvTransactionDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.Black;
            this.gvTransactionDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle19;
            this.gvTransactionDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvTransactionDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvTransactionDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.gvTransactionDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTransactionDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.TrnNo,
            this.ConductedBy,
            this.CampName,
            this.ProductsCnt,
            this.DemosCnt,
            this.OthersCnt,
            this.CompanyStaffCnt,
            this.Print});
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvTransactionDetails.DefaultCellStyle = dataGridViewCellStyle22;
            this.gvTransactionDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvTransactionDetails.Location = new System.Drawing.Point(6, 69);
            this.gvTransactionDetails.MultiSelect = false;
            this.gvTransactionDetails.Name = "gvTransactionDetails";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvTransactionDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.gvTransactionDetails.RowHeadersVisible = false;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.Color.Navy;
            this.gvTransactionDetails.RowsDefaultCellStyle = dataGridViewCellStyle24;
            this.gvTransactionDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvTransactionDetails.Size = new System.Drawing.Size(923, 376);
            this.gvTransactionDetails.TabIndex = 138;
            this.gvTransactionDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvTransactionDetails_CellClick);
            // 
            // SLNO
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.SLNO.DefaultCellStyle = dataGridViewCellStyle21;
            this.SLNO.Frozen = true;
            this.SLNO.HeaderText = "Sl.No";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SLNO.Width = 50;
            // 
            // TrnNo
            // 
            this.TrnNo.HeaderText = "Trn No";
            this.TrnNo.Name = "TrnNo";
            this.TrnNo.ReadOnly = true;
            this.TrnNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TrnNo.Width = 190;
            // 
            // ConductedBy
            // 
            this.ConductedBy.HeaderText = "Conducted By";
            this.ConductedBy.Name = "ConductedBy";
            this.ConductedBy.ReadOnly = true;
            this.ConductedBy.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ConductedBy.Width = 190;
            // 
            // CampName
            // 
            this.CampName.HeaderText = "Camp Name";
            this.CampName.Name = "CampName";
            this.CampName.ReadOnly = true;
            this.CampName.Width = 150;
            // 
            // ProductsCnt
            // 
            this.ProductsCnt.HeaderText = "Products Cnt";
            this.ProductsCnt.Name = "ProductsCnt";
            this.ProductsCnt.ReadOnly = true;
            this.ProductsCnt.Width = 70;
            // 
            // DemosCnt
            // 
            this.DemosCnt.HeaderText = "Demos Cnt";
            this.DemosCnt.Name = "DemosCnt";
            this.DemosCnt.ReadOnly = true;
            this.DemosCnt.Width = 70;
            // 
            // OthersCnt
            // 
            this.OthersCnt.HeaderText = "Others Cnt";
            this.OthersCnt.Name = "OthersCnt";
            this.OthersCnt.ReadOnly = true;
            this.OthersCnt.Width = 70;
            // 
            // CompanyStaffCnt
            // 
            this.CompanyStaffCnt.HeaderText = "Staff Cnt";
            this.CompanyStaffCnt.Name = "CompanyStaffCnt";
            this.CompanyStaffCnt.ReadOnly = true;
            this.CompanyStaffCnt.Width = 70;
            // 
            // Print
            // 
            this.Print.HeaderText = "Print";
            this.Print.Image = global::SSCRM.Properties.Resources.print_icon;
            this.Print.Name = "Print";
            this.Print.Width = 50;
            // 
            // cbBranches
            // 
            this.cbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranches.FormattingEnabled = true;
            this.cbBranches.Location = new System.Drawing.Point(433, 16);
            this.cbBranches.Name = "cbBranches";
            this.cbBranches.Size = new System.Drawing.Size(298, 24);
            this.cbBranches.TabIndex = 131;
            // 
            // cbCompany
            // 
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Items.AddRange(new object[] {
            "                     Select"});
            this.cbCompany.Location = new System.Drawing.Point(86, 16);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(282, 24);
            this.cbCompany.TabIndex = 129;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(374, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 130;
            this.label5.Text = "Branch";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(9, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 17);
            this.label6.TabIndex = 128;
            this.label6.Text = "Company";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Location = new System.Drawing.Point(292, 443);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 46);
            this.groupBox3.TabIndex = 160;
            this.groupBox3.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(136, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 30);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dtpDocMonth
            // 
            this.dtpDocMonth.CustomFormat = "MMM/yyyy";
            this.dtpDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDocMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDocMonth.Location = new System.Drawing.Point(821, 17);
            this.dtpDocMonth.Name = "dtpDocMonth";
            this.dtpDocMonth.Size = new System.Drawing.Size(104, 22);
            this.dtpDocMonth.TabIndex = 169;
            // 
            // ServiceReportByTrnNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(941, 499);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceReportByTrnNo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Service Report Summary";
            this.Load += new System.EventHandler(this.ServiceReportByTrnNo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransactionDetails)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblReportName;
        public System.Windows.Forms.DataGridView gvTransactionDetails;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbBranches;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDisplayPromotionDetails;
        private System.Windows.Forms.Label lblReport;
        private System.Windows.Forms.ComboBox cbReportType;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrnNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConductedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn CampName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductsCnt;
        private System.Windows.Forms.DataGridViewTextBoxColumn DemosCnt;
        private System.Windows.Forms.DataGridViewTextBoxColumn OthersCnt;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyStaffCnt;
        private System.Windows.Forms.DataGridViewImageColumn Print;
        private System.Windows.Forms.DateTimePicker dtpDocMonth;
    }
}