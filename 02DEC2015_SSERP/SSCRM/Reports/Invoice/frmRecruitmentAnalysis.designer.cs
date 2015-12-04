namespace SSCRM
{
    partial class frmRecruitmentAnalysis
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gvStateDetails = new System.Windows.Forms.DataGridView();
            this.SlNo_ref = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StateCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StateName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Points = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbRecruitmentSource = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbReportType = new System.Windows.Forms.ComboBox();
            this.lblRepType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMinPoints = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clbCompany = new System.Windows.Forms.CheckedListBox();
            this.chkCompanyAll = new System.Windows.Forms.CheckBox();
            this.dtpFrmMonth = new System.Windows.Forms.DateTimePicker();
            this.dtpToMonth = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.clbBranch = new System.Windows.Forms.CheckedListBox();
            this.ChkBranch = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvStateDetails)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.gvStateDetails);
            this.groupBox1.Controls.Add(this.cbRecruitmentSource);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbReportType);
            this.groupBox1.Controls.Add(this.lblRepType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtMinPoints);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.dtpFrmMonth);
            this.groupBox1.Controls.Add(this.dtpToMonth);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(656, 518);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // gvStateDetails
            // 
            this.gvStateDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvStateDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvStateDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvStateDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvStateDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvStateDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvStateDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SlNo_ref,
            this.StateCode,
            this.StateName,
            this.Points});
            this.gvStateDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvStateDetails.Location = new System.Drawing.Point(22, 264);
            this.gvStateDetails.MultiSelect = false;
            this.gvStateDetails.Name = "gvStateDetails";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvStateDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvStateDetails.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Navy;
            this.gvStateDetails.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvStateDetails.Size = new System.Drawing.Size(594, 197);
            this.gvStateDetails.TabIndex = 123;
            this.gvStateDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvStateDetails_EditingControlShowing);
            
            // 
            // SlNo_ref
            // 
            this.SlNo_ref.Frozen = true;
            this.SlNo_ref.HeaderText = "Sl.No";
            this.SlNo_ref.Name = "SlNo_ref";
            this.SlNo_ref.ReadOnly = true;
            this.SlNo_ref.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SlNo_ref.Width = 50;
            // 
            // StateCode
            // 
            this.StateCode.HeaderText = "State Code";
            this.StateCode.Name = "StateCode";
            this.StateCode.Visible = false;
            // 
            // StateName
            // 
            this.StateName.HeaderText = "State Name";
            this.StateName.Name = "StateName";
            this.StateName.ReadOnly = true;
            this.StateName.Width = 350;
            // 
            // Points
            // 
            this.Points.HeaderText = "Points";
            this.Points.Name = "Points";
            // 
            // cbRecruitmentSource
            // 
            this.cbRecruitmentSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRecruitmentSource.FormattingEnabled = true;
            this.cbRecruitmentSource.Items.AddRange(new object[] {
            "--Select--",
            "DASH BOARD",
            "DEPARTMENT"});
            this.cbRecruitmentSource.Location = new System.Drawing.Point(172, 229);
            this.cbRecruitmentSource.Name = "cbRecruitmentSource";
            this.cbRecruitmentSource.Size = new System.Drawing.Size(152, 21);
            this.cbRecruitmentSource.TabIndex = 121;
            this.cbRecruitmentSource.SelectedIndexChanged += new System.EventHandler(this.cbRecruitmentSource_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(18, 230);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 17);
            this.label7.TabIndex = 122;
            this.label7.Text = "Recruitment Source";
            // 
            // cbReportType
            // 
            this.cbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReportType.FormattingEnabled = true;
            this.cbReportType.Items.AddRange(new object[] {
            "DETAILED",
            "SUMMARY"});
            this.cbReportType.Location = new System.Drawing.Point(439, 230);
            this.cbReportType.Name = "cbReportType";
            this.cbReportType.Size = new System.Drawing.Size(146, 21);
            this.cbReportType.TabIndex = 119;
            this.cbReportType.SelectedIndexChanged += new System.EventHandler(this.cbReportType_SelectedIndexChanged);
            // 
            // lblRepType
            // 
            this.lblRepType.AutoSize = true;
            this.lblRepType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRepType.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblRepType.Location = new System.Drawing.Point(337, 232);
            this.lblRepType.Name = "lblRepType";
            this.lblRepType.Size = new System.Drawing.Size(98, 17);
            this.lblRepType.TabIndex = 120;
            this.lblRepType.Text = "Report Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(272, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 16);
            this.label1.TabIndex = 118;
            this.label1.Text = "for Successful SR\"s.";
            // 
            // txtMinPoints
            // 
            this.txtMinPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinPoints.Location = new System.Drawing.Point(175, 201);
            this.txtMinPoints.MaxLength = 10;
            this.txtMinPoints.Name = "txtMinPoints";
            this.txtMinPoints.Size = new System.Drawing.Size(80, 22);
            this.txtMinPoints.TabIndex = 117;
            this.txtMinPoints.Validated += new System.EventHandler(this.txtMinPoints_Validated);
            this.txtMinPoints.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMinPoints_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(22, 204);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 16);
            this.label2.TabIndex = 116;
            this.label2.Text = "-->   Minimum Points";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.Controls.Add(this.clbCompany);
            this.groupBox2.Controls.Add(this.chkCompanyAll);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(9, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(309, 149);
            this.groupBox2.TabIndex = 112;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Company";
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
            this.chkCompanyAll.Location = new System.Drawing.Point(89, 0);
            this.chkCompanyAll.Name = "chkCompanyAll";
            this.chkCompanyAll.Size = new System.Drawing.Size(105, 21);
            this.chkCompanyAll.TabIndex = 1;
            this.chkCompanyAll.Text = "Select ALL";
            this.chkCompanyAll.UseVisualStyleBackColor = true;
            this.chkCompanyAll.CheckedChanged += new System.EventHandler(this.chkCompanyAll_CheckedChanged);
            // 
            // dtpFrmMonth
            // 
            this.dtpFrmMonth.CustomFormat = "MMM/yyyy";
            this.dtpFrmMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpFrmMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrmMonth.Location = new System.Drawing.Point(170, 172);
            this.dtpFrmMonth.Name = "dtpFrmMonth";
            this.dtpFrmMonth.Size = new System.Drawing.Size(94, 22);
            this.dtpFrmMonth.TabIndex = 108;
            this.dtpFrmMonth.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // dtpToMonth
            // 
            this.dtpToMonth.CustomFormat = "MMM/yyyy";
            this.dtpToMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpToMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToMonth.Location = new System.Drawing.Point(383, 173);
            this.dtpToMonth.Name = "dtpToMonth";
            this.dtpToMonth.Size = new System.Drawing.Size(94, 22);
            this.dtpToMonth.TabIndex = 109;
            this.dtpToMonth.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(301, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 17);
            this.label4.TabIndex = 107;
            this.label4.Text = "To Month";
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
            this.label3.Text = "From Month";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.clbBranch);
            this.groupBox3.Controls.Add(this.ChkBranch);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Navy;
            this.groupBox3.Location = new System.Drawing.Point(327, 14);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(319, 149);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Branch";
            // 
            // clbBranch
            // 
            this.clbBranch.CheckOnClick = true;
            this.clbBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbBranch.FormattingEnabled = true;
            this.clbBranch.Location = new System.Drawing.Point(12, 23);
            this.clbBranch.Name = "clbBranch";
            this.clbBranch.Size = new System.Drawing.Size(294, 116);
            this.clbBranch.TabIndex = 2;
            this.clbBranch.SelectedIndexChanged += new System.EventHandler(this.clbBranch_SelectedIndexChanged);
            // 
            // ChkBranch
            // 
            this.ChkBranch.AutoSize = true;
            this.ChkBranch.Location = new System.Drawing.Point(70, 0);
            this.ChkBranch.Name = "ChkBranch";
            this.ChkBranch.Size = new System.Drawing.Size(105, 21);
            this.ChkBranch.TabIndex = 1;
            this.ChkBranch.Text = "Select ALL";
            this.ChkBranch.UseVisualStyleBackColor = true;
            this.ChkBranch.CheckedChanged += new System.EventHandler(this.ChkBranch_CheckedChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnDownload);
            this.groupBox7.Controls.Add(this.btnClear);
            this.groupBox7.Controls.Add(this.btnClose);
            this.groupBox7.Controls.Add(this.btnReport);
            this.groupBox7.Location = new System.Drawing.Point(118, 466);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(421, 43);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            // 
            // btnDownload
            // 
            this.btnDownload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDownload.BackColor = System.Drawing.Color.OliveDrab;
            this.btnDownload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnDownload.FlatAppearance.BorderSize = 5;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDownload.Image = global::SSCRM.Properties.Resources.ic_download;
            this.btnDownload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDownload.Location = new System.Drawing.Point(300, 14);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(1);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(97, 24);
            this.btnDownload.TabIndex = 102;
            this.btnDownload.Tag = "Product  Search";
            this.btnDownload.Text = "Download";
            this.btnDownload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.Navy;
            this.btnClear.Location = new System.Drawing.Point(109, 13);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(73, 26);
            this.btnClear.TabIndex = 101;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.Navy;
            this.btnClose.Location = new System.Drawing.Point(185, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 26);
            this.btnClose.TabIndex = 100;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnReport.ForeColor = System.Drawing.Color.Navy;
            this.btnReport.Location = new System.Drawing.Point(31, 12);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(73, 26);
            this.btnReport.TabIndex = 0;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // frmRecruitmentAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(662, 523);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRecruitmentAnalysis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recruitment Analysis";
            this.Load += new System.EventHandler(this.frmRecruitmentAnalysis_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvStateDetails)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFrmMonth;
        private System.Windows.Forms.DateTimePicker dtpToMonth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox clbBranch;
        private System.Windows.Forms.CheckBox ChkBranch;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox clbCompany;
        private System.Windows.Forms.CheckBox chkCompanyAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMinPoints;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbReportType;
        private System.Windows.Forms.Label lblRepType;
        private System.Windows.Forms.ComboBox cbRecruitmentSource;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.DataGridView gvStateDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlNo_ref;
        private System.Windows.Forms.DataGridViewTextBoxColumn StateCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn StateName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Points;
        private System.Windows.Forms.Button btnDownload;

    }
}