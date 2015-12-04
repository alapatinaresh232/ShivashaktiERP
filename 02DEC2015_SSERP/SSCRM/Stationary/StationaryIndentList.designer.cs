namespace SSCRM
{
    partial class StationaryIndentList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StationaryIndentList));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gvIndentDetails = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.SlNo_ref = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sih_company_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SIH_FIN_YEAR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MemberName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndentNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndentAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SDN_DC_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lnkDetails = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ImgPrint = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvIndentDetails)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.cmbStatus);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.gvIndentDetails);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(0, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(964, 477);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Branch Wise Indent List";
            // 
            // cmbStatus
            // 
            this.cmbStatus.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "PENDING",
            "APPROVED",
            "VERIFIED",
            "REJECT",
            "DISPATCHED"});
            this.cmbStatus.Location = new System.Drawing.Point(814, 14);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(134, 24);
            this.cmbStatus.TabIndex = 64;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(754, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 65;
            this.label1.Text = "Status";
            // 
            // gvIndentDetails
            // 
            this.gvIndentDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            this.gvIndentDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.gvIndentDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvIndentDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvIndentDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.gvIndentDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvIndentDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SlNo_ref,
            this.sih_company_code,
            this.SIH_FIN_YEAR,
            this.BranchCode,
            this.BranchName,
            this.MemberName,
            this.IndentNo,
            this.IndentAmount,
            this.IndentDate,
            this.SDN_DC_DATE,
            this.Status,
            this.lnkDetails,
            this.ImgPrint});
            this.gvIndentDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvIndentDetails.Location = new System.Drawing.Point(5, 42);
            this.gvIndentDetails.MultiSelect = false;
            this.gvIndentDetails.Name = "gvIndentDetails";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvIndentDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.gvIndentDetails.RowHeadersVisible = false;
            this.gvIndentDetails.Size = new System.Drawing.Size(954, 392);
            this.gvIndentDetails.TabIndex = 63;
            this.gvIndentDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvIndentDetails_CellClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Location = new System.Drawing.Point(308, 431);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(266, 45);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(96, 13);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            // sih_company_code
            // 
            this.sih_company_code.HeaderText = "Company Code";
            this.sih_company_code.Name = "sih_company_code";
            this.sih_company_code.ReadOnly = true;
            this.sih_company_code.Visible = false;
            // 
            // SIH_FIN_YEAR
            // 
            this.SIH_FIN_YEAR.HeaderText = "FIN_YEAR";
            this.SIH_FIN_YEAR.Name = "SIH_FIN_YEAR";
            this.SIH_FIN_YEAR.ReadOnly = true;
            this.SIH_FIN_YEAR.Visible = false;
            // 
            // BranchCode
            // 
            this.BranchCode.HeaderText = "Branch Code";
            this.BranchCode.Name = "BranchCode";
            this.BranchCode.ReadOnly = true;
            this.BranchCode.Visible = false;
            // 
            // BranchName
            // 
            this.BranchName.HeaderText = "Branch  Name";
            this.BranchName.Name = "BranchName";
            this.BranchName.ReadOnly = true;
            this.BranchName.Width = 220;
            // 
            // MemberName
            // 
            this.MemberName.HeaderText = "Self Name";
            this.MemberName.Name = "MemberName";
            this.MemberName.Width = 120;
            // 
            // IndentNo
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IndentNo.DefaultCellStyle = dataGridViewCellStyle9;
            this.IndentNo.HeaderText = "Indent No";
            this.IndentNo.Name = "IndentNo";
            this.IndentNo.ReadOnly = true;
            this.IndentNo.Width = 80;
            // 
            // IndentAmount
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IndentAmount.DefaultCellStyle = dataGridViewCellStyle10;
            this.IndentAmount.HeaderText = "Indent Amt";
            this.IndentAmount.Name = "IndentAmount";
            this.IndentAmount.ReadOnly = true;
            this.IndentAmount.Width = 110;
            // 
            // IndentDate
            // 
            this.IndentDate.HeaderText = "Indent Date";
            this.IndentDate.Name = "IndentDate";
            this.IndentDate.ReadOnly = true;
            this.IndentDate.Width = 105;
            // 
            // SDN_DC_DATE
            // 
            this.SDN_DC_DATE.HeaderText = "Dispatch  Date";
            this.SDN_DC_DATE.Name = "SDN_DC_DATE";
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // lnkDetails
            // 
            this.lnkDetails.HeaderText = "Details";
            this.lnkDetails.Name = "lnkDetails";
            this.lnkDetails.Text = "Details";
            this.lnkDetails.UseColumnTextForLinkValue = true;
            this.lnkDetails.Width = 60;
            // 
            // ImgPrint
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle11.NullValue")));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.White;
            this.ImgPrint.DefaultCellStyle = dataGridViewCellStyle11;
            this.ImgPrint.HeaderText = "Print";
            this.ImgPrint.Image = global::SSCRM.Properties.Resources.print_icon2;
            this.ImgPrint.Name = "ImgPrint";
            this.ImgPrint.Width = 50;
            // 
            // StationaryIndentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 477);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "StationaryIndentList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stationary Indent List";
            this.Load += new System.EventHandler(this.StationaryIndentList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvIndentDetails)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.DataGridView gvIndentDetails;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlNo_ref;
        private System.Windows.Forms.DataGridViewTextBoxColumn sih_company_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIH_FIN_YEAR;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MemberName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndentNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndentAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndentDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SDN_DC_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewLinkColumn lnkDetails;
        private System.Windows.Forms.DataGridViewImageColumn ImgPrint;
    }
}