namespace SSCRM
{
    partial class StationaryBrochureList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StationaryBrochureList));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.gvIndentDetails = new System.Windows.Forms.DataGridView();
            this.SlNo_ref = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sih_company_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SIH_STATE_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SIH_FIN_YEAR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MemberName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndentNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndentAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DispatchDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DelivedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lnkDetails = new System.Windows.Forms.DataGridViewImageColumn();
            this.ImgPrint = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvIndentDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.btnDownload);
            this.groupBox1.Controls.Add(this.btnDisplay);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.cmbStatus);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.gvIndentDetails);
            this.groupBox1.Location = new System.Drawing.Point(1, -4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(887, 521);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
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
            this.btnDownload.Location = new System.Drawing.Point(776, 14);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(1);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(97, 24);
            this.btnDownload.TabIndex = 76;
            this.btnDownload.Tag = "Product  Search";
            this.btnDownload.Text = "Download";
            this.btnDownload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnDisplay
            // 
            this.btnDisplay.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDisplay.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDisplay.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDisplay.Location = new System.Drawing.Point(556, 13);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(74, 26);
            this.btnDisplay.TabIndex = 71;
            this.btnDisplay.Text = "&Display";
            this.btnDisplay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDisplay.UseVisualStyleBackColor = false;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(400, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 16);
            this.label3.TabIndex = 70;
            this.label3.Text = "To";
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(429, 15);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(103, 22);
            this.dtpToDate.TabIndex = 69;
            this.dtpToDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(238, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 68;
            this.label2.Text = "From";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(284, 15);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(103, 22);
            this.dtpFromDate.TabIndex = 3;
            this.dtpFromDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // cmbStatus
            // 
            this.cmbStatus.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "APPROVED",
            "DISPATCHED",
            "DELIVERED"});
            this.cmbStatus.Location = new System.Drawing.Point(55, 15);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(134, 24);
            this.cmbStatus.TabIndex = 66;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(4, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 67;
            this.label1.Text = "Status";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(403, 490);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 64;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // gvIndentDetails
            // 
            this.gvIndentDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvIndentDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvIndentDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvIndentDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvIndentDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvIndentDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvIndentDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SlNo_ref,
            this.sih_company_code,
            this.SIH_STATE_CODE,
            this.SIH_FIN_YEAR,
            this.BranchCode,
            this.BranchName,
            this.MemberName,
            this.IndentNo,
            this.IndentAmount,
            this.IndentDate,
            this.DispatchDate,
            this.DelivedDate,
            this.Status,
            this.lnkDetails,
            this.ImgPrint});
            this.gvIndentDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvIndentDetails.Location = new System.Drawing.Point(5, 44);
            this.gvIndentDetails.MultiSelect = false;
            this.gvIndentDetails.Name = "gvIndentDetails";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvIndentDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gvIndentDetails.RowHeadersVisible = false;
            this.gvIndentDetails.Size = new System.Drawing.Size(876, 442);
            this.gvIndentDetails.TabIndex = 63;
            this.gvIndentDetails.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvIndentDetails_ColumnHeaderMouseClick);
            this.gvIndentDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvIndentDetails_CellClick);
            // 
            // SlNo_ref
            // 
            this.SlNo_ref.Frozen = true;
            this.SlNo_ref.HeaderText = "Sl.No";
            this.SlNo_ref.Name = "SlNo_ref";
            this.SlNo_ref.ReadOnly = true;
            this.SlNo_ref.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SlNo_ref.Width = 40;
            // 
            // sih_company_code
            // 
            this.sih_company_code.HeaderText = "Company Code";
            this.sih_company_code.Name = "sih_company_code";
            this.sih_company_code.ReadOnly = true;
            this.sih_company_code.Visible = false;
            // 
            // SIH_STATE_CODE
            // 
            this.SIH_STATE_CODE.HeaderText = "SIH_STATE_CODE";
            this.SIH_STATE_CODE.Name = "SIH_STATE_CODE";
            this.SIH_STATE_CODE.ReadOnly = true;
            this.SIH_STATE_CODE.Visible = false;
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
            this.BranchName.HeaderText = "Branch Name";
            this.BranchName.Name = "BranchName";
            this.BranchName.ReadOnly = true;
            this.BranchName.Width = 200;
            // 
            // MemberName
            // 
            this.MemberName.HeaderText = "Self Name";
            this.MemberName.Name = "MemberName";
            this.MemberName.Width = 160;
            // 
            // IndentNo
            // 
            this.IndentNo.HeaderText = "Indent No";
            this.IndentNo.Name = "IndentNo";
            this.IndentNo.ReadOnly = true;
            this.IndentNo.Width = 50;
            // 
            // IndentAmount
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IndentAmount.DefaultCellStyle = dataGridViewCellStyle3;
            this.IndentAmount.HeaderText = "Indent Amt.";
            this.IndentAmount.Name = "IndentAmount";
            this.IndentAmount.ReadOnly = true;
            this.IndentAmount.Width = 80;
            // 
            // IndentDate
            // 
            this.IndentDate.HeaderText = "Approved Date";
            this.IndentDate.Name = "IndentDate";
            this.IndentDate.ReadOnly = true;
            this.IndentDate.Width = 90;
            // 
            // DispatchDate
            // 
            this.DispatchDate.HeaderText = "Dispatch Date";
            this.DispatchDate.Name = "DispatchDate";
            this.DispatchDate.ReadOnly = true;
            this.DispatchDate.Width = 90;
            // 
            // DelivedDate
            // 
            this.DelivedDate.HeaderText = "Delived Date";
            this.DelivedDate.Name = "DelivedDate";
            this.DelivedDate.ReadOnly = true;
            this.DelivedDate.Width = 90;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // lnkDetails
            // 
            this.lnkDetails.HeaderText = "";
            this.lnkDetails.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.lnkDetails.Name = "lnkDetails";
            this.lnkDetails.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.lnkDetails.Width = 30;
            // 
            // ImgPrint
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle4.NullValue")));
            this.ImgPrint.DefaultCellStyle = dataGridViewCellStyle4;
            this.ImgPrint.HeaderText = "";
            this.ImgPrint.Image = global::SSCRM.Properties.Resources.print_icon2;
            this.ImgPrint.Name = "ImgPrint";
            this.ImgPrint.Width = 30;
            // 
            // StationaryBrochureList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(892, 516);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "StationaryBrochureList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stationary Brocher List";
            this.Load += new System.EventHandler(this.StationaryIndentList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvIndentDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.DataGridView gvIndentDetails;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlNo_ref;
        private System.Windows.Forms.DataGridViewTextBoxColumn sih_company_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIH_STATE_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIH_FIN_YEAR;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MemberName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndentNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndentAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndentDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DispatchDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DelivedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewImageColumn lnkDetails;
        private System.Windows.Forms.DataGridViewImageColumn ImgPrint;
    }
}