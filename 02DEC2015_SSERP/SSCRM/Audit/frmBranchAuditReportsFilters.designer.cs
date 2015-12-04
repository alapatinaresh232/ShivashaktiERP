namespace SSCRM
{
    partial class frmBranchAuditReportsFilters
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
            this.label1 = new System.Windows.Forms.Label();
            this.clbBranchTypes = new System.Windows.Forms.CheckedListBox();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.chkSolvedFollUp = new System.Windows.Forms.CheckBox();
            this.chkUnsolved = new System.Windows.Forms.CheckBox();
            this.chkSolved = new System.Windows.Forms.CheckBox();
            this.ChkStatusAll = new System.Windows.Forms.CheckBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.chkComp = new System.Windows.Forms.CheckBox();
            this.tvDocMonth = new SSCRM.TriStateTreeView();
            this.tvBranches = new SSCRM.TriStateTreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.clbDepartment = new System.Windows.Forms.CheckedListBox();
            this.label27 = new System.Windows.Forms.Label();
            this.cbReportType = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grpStatus.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.clbBranchTypes);
            this.groupBox1.Controls.Add(this.grpStatus);
            this.groupBox1.Controls.Add(this.btnDownload);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.chkComp);
            this.groupBox1.Controls.Add(this.tvDocMonth);
            this.groupBox1.Controls.Add(this.tvBranches);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.clbDepartment);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.cbReportType);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(674, 502);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(8, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 183;
            this.label1.Text = "Branch Type";
            // 
            // clbBranchTypes
            // 
            this.clbBranchTypes.CheckOnClick = true;
            this.clbBranchTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbBranchTypes.FormattingEnabled = true;
            this.clbBranchTypes.Location = new System.Drawing.Point(11, 82);
            this.clbBranchTypes.Name = "clbBranchTypes";
            this.clbBranchTypes.Size = new System.Drawing.Size(152, 228);
            this.clbBranchTypes.TabIndex = 184;
            this.clbBranchTypes.SelectedIndexChanged += new System.EventHandler(this.clbBranchTypes_SelectedIndexChanged);
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.chkSolvedFollUp);
            this.grpStatus.Controls.Add(this.chkUnsolved);
            this.grpStatus.Controls.Add(this.chkSolved);
            this.grpStatus.Controls.Add(this.ChkStatusAll);
            this.grpStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpStatus.ForeColor = System.Drawing.Color.Navy;
            this.grpStatus.Location = new System.Drawing.Point(327, 334);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(164, 104);
            this.grpStatus.TabIndex = 182;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Status";
            // 
            // chkSolvedFollUp
            // 
            this.chkSolvedFollUp.AutoSize = true;
            this.chkSolvedFollUp.ForeColor = System.Drawing.Color.Black;
            this.chkSolvedFollUp.Location = new System.Drawing.Point(12, 73);
            this.chkSolvedFollUp.Name = "chkSolvedFollUp";
            this.chkSolvedFollUp.Size = new System.Drawing.Size(146, 20);
            this.chkSolvedFollUp.TabIndex = 3;
            this.chkSolvedFollUp.Text = "Solved-FollowUp";
            this.chkSolvedFollUp.UseVisualStyleBackColor = true;
            // 
            // chkUnsolved
            // 
            this.chkUnsolved.AutoSize = true;
            this.chkUnsolved.ForeColor = System.Drawing.Color.Black;
            this.chkUnsolved.Location = new System.Drawing.Point(12, 51);
            this.chkUnsolved.Name = "chkUnsolved";
            this.chkUnsolved.Size = new System.Drawing.Size(95, 20);
            this.chkUnsolved.TabIndex = 2;
            this.chkUnsolved.Text = "UnSolved";
            this.chkUnsolved.UseVisualStyleBackColor = true;
            // 
            // chkSolved
            // 
            this.chkSolved.AutoSize = true;
            this.chkSolved.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSolved.ForeColor = System.Drawing.Color.Black;
            this.chkSolved.Location = new System.Drawing.Point(12, 26);
            this.chkSolved.Name = "chkSolved";
            this.chkSolved.Size = new System.Drawing.Size(76, 20);
            this.chkSolved.TabIndex = 1;
            this.chkSolved.Text = "Solved";
            this.chkSolved.UseVisualStyleBackColor = true;
            // 
            // ChkStatusAll
            // 
            this.ChkStatusAll.AutoSize = true;
            this.ChkStatusAll.Location = new System.Drawing.Point(78, 0);
            this.ChkStatusAll.Name = "ChkStatusAll";
            this.ChkStatusAll.Size = new System.Drawing.Size(53, 20);
            this.ChkStatusAll.TabIndex = 0;
            this.ChkStatusAll.Text = "ALL";
            this.ChkStatusAll.UseVisualStyleBackColor = true;
            this.ChkStatusAll.CheckedChanged += new System.EventHandler(this.ChkStatusAll_CheckedChanged);
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
            this.btnDownload.Location = new System.Drawing.Point(569, 461);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(1);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(97, 24);
            this.btnDownload.TabIndex = 177;
            this.btnDownload.Tag = "Product  Search";
            this.btnDownload.Text = "Download";
            this.btnDownload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label5.Location = new System.Drawing.Point(498, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 16);
            this.label5.TabIndex = 175;
            this.label5.Text = "Visit Month";
            // 
            // chkComp
            // 
            this.chkComp.AutoSize = true;
            this.chkComp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.chkComp.ForeColor = System.Drawing.Color.MidnightBlue;
            this.chkComp.Location = new System.Drawing.Point(174, 57);
            this.chkComp.Name = "chkComp";
            this.chkComp.Size = new System.Drawing.Size(93, 20);
            this.chkComp.TabIndex = 174;
            this.chkComp.Text = "Select All";
            this.chkComp.UseVisualStyleBackColor = true;
            this.chkComp.CheckedChanged += new System.EventHandler(this.chkComp_CheckedChanged);
            // 
            // tvDocMonth
            // 
            this.tvDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvDocMonth.Location = new System.Drawing.Point(501, 82);
            this.tvDocMonth.Name = "tvDocMonth";
            this.tvDocMonth.Size = new System.Drawing.Size(158, 229);
            this.tvDocMonth.TabIndex = 87;
            this.tvDocMonth.TriStateStyleProperty = SSCRM.TriStateTreeView.TriStateStyles.Standard;
            this.tvDocMonth.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvDocMonth_AfterCheck);
            // 
            // tvBranches
            // 
            this.tvBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvBranches.Location = new System.Drawing.Point(169, 82);
            this.tvBranches.Name = "tvBranches";
            this.tvBranches.Size = new System.Drawing.Size(327, 228);
            this.tvBranches.TabIndex = 85;
            this.tvBranches.TriStateStyleProperty = SSCRM.TriStateTreeView.TriStateStyles.Standard;
            this.tvBranches.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvBranches_AfterCheck);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label2.Location = new System.Drawing.Point(9, 322);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 80;
            this.label2.Text = "Department";
            // 
            // clbDepartment
            // 
            this.clbDepartment.CheckOnClick = true;
            this.clbDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbDepartment.FormattingEnabled = true;
            this.clbDepartment.Location = new System.Drawing.Point(10, 341);
            this.clbDepartment.Name = "clbDepartment";
            this.clbDepartment.ScrollAlwaysVisible = true;
            this.clbDepartment.Size = new System.Drawing.Size(215, 148);
            this.clbDepartment.TabIndex = 79;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label27.Location = new System.Drawing.Point(10, 22);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(95, 16);
            this.label27.TabIndex = 68;
            this.label27.Text = "Report Type";
            // 
            // cbReportType
            // 
            this.cbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReportType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbReportType.FormattingEnabled = true;
            this.cbReportType.Location = new System.Drawing.Point(110, 19);
            this.cbReportType.Name = "cbReportType";
            this.cbReportType.Size = new System.Drawing.Size(313, 23);
            this.cbReportType.TabIndex = 67;
            this.cbReportType.SelectedIndexChanged += new System.EventHandler(this.cbReportType_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnClear);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnReport);
            this.groupBox3.Location = new System.Drawing.Point(243, 448);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(308, 47);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(117, 11);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(78, 30);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clea&r";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(199, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "C&lose";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Location = new System.Drawing.Point(35, 11);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(78, 30);
            this.btnReport.TabIndex = 0;
            this.btnReport.Text = "&Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // frmBranchAuditReportsFilters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(680, 508);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBranchAuditReportsFilters";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Audit Reports";
            this.Load += new System.EventHandler(this.AuditReportsFilters_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox cbReportType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox clbDepartment;
        private TriStateTreeView tvBranches;
        private TriStateTreeView tvDocMonth;
        private System.Windows.Forms.CheckBox chkComp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.CheckBox ChkStatusAll;
        private System.Windows.Forms.CheckBox chkSolvedFollUp;
        private System.Windows.Forms.CheckBox chkUnsolved;
        private System.Windows.Forms.CheckBox chkSolved;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox clbBranchTypes;
    }
}