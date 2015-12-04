namespace SDMS
{
    partial class ReceiptVoucherList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDealerCode = new System.Windows.Forms.TextBox();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.gvVoucherList = new System.Windows.Forms.DataGridView();
            this.SlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.compcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.finYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doctype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DealerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CashOrBankId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AM_ACCOUNT_TYPE_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblCompany = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvVoucherList)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDealerCode);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.gvVoucherList);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.btnDisplay);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.lblCompany);
            this.groupBox1.Location = new System.Drawing.Point(1, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(763, 390);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.PowderBlue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(322, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 16);
            this.label1.TabIndex = 44;
            this.label1.Text = "To";
            // 
            // txtDealerCode
            // 
            this.txtDealerCode.Location = new System.Drawing.Point(563, 14);
            this.txtDealerCode.Name = "txtDealerCode";
            this.txtDealerCode.Size = new System.Drawing.Size(106, 20);
            this.txtDealerCode.TabIndex = 1;
            this.txtDealerCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDealerCode_KeyUp);
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(355, 41);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(98, 23);
            this.dtpToDate.TabIndex = 3;
            this.dtpToDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpToDate.ValueChanged += new System.EventHandler(this.dtpToDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.PowderBlue;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(468, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 16);
            this.label2.TabIndex = 46;
            this.label2.Text = "DealerCode";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.PowderBlue;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label11.Location = new System.Drawing.Point(96, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 16);
            this.label11.TabIndex = 42;
            this.label11.Text = "From";
            // 
            // gvVoucherList
            // 
            this.gvVoucherList.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.gvVoucherList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvVoucherList.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvVoucherList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvVoucherList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvVoucherList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvVoucherList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SlNo,
            this.compcode,
            this.branchcode,
            this.finYear,
            this.doctype,
            this.DealerCode,
            this.VoucherId,
            this.VoucherType,
            this.CashOrBankId,
            this.Amount,
            this.Date,
            this.AM_ACCOUNT_TYPE_ID,
            this.Edit,
            this.Delete});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvVoucherList.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvVoucherList.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvVoucherList.Location = new System.Drawing.Point(4, 72);
            this.gvVoucherList.MultiSelect = false;
            this.gvVoucherList.Name = "gvVoucherList";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvVoucherList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvVoucherList.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.MidnightBlue;
            this.gvVoucherList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvVoucherList.Size = new System.Drawing.Size(753, 274);
            this.gvVoucherList.TabIndex = 5;
            this.gvVoucherList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvVoucherList_CellClick);
            // 
            // SlNo
            // 
            this.SlNo.Frozen = true;
            this.SlNo.HeaderText = "Sl.No";
            this.SlNo.Name = "SlNo";
            this.SlNo.ReadOnly = true;
            this.SlNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SlNo.Width = 40;
            // 
            // compcode
            // 
            this.compcode.Frozen = true;
            this.compcode.HeaderText = "compcode";
            this.compcode.Name = "compcode";
            this.compcode.Visible = false;
            // 
            // branchcode
            // 
            this.branchcode.Frozen = true;
            this.branchcode.HeaderText = "branchcode";
            this.branchcode.Name = "branchcode";
            this.branchcode.Visible = false;
            // 
            // finYear
            // 
            this.finYear.Frozen = true;
            this.finYear.HeaderText = "finYear";
            this.finYear.Name = "finYear";
            this.finYear.Visible = false;
            // 
            // doctype
            // 
            this.doctype.Frozen = true;
            this.doctype.HeaderText = "doctype";
            this.doctype.Name = "doctype";
            this.doctype.Visible = false;
            // 
            // DealerCode
            // 
            this.DealerCode.Frozen = true;
            this.DealerCode.HeaderText = "Dealer";
            this.DealerCode.Name = "DealerCode";
            // 
            // VoucherId
            // 
            this.VoucherId.Frozen = true;
            this.VoucherId.HeaderText = "Voucher Id";
            this.VoucherId.MinimumWidth = 20;
            this.VoucherId.Name = "VoucherId";
            this.VoucherId.ReadOnly = true;
            this.VoucherId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.VoucherId.Width = 70;
            // 
            // VoucherType
            // 
            this.VoucherType.Frozen = true;
            this.VoucherType.HeaderText = "Voucher Type";
            this.VoucherType.Name = "VoucherType";
            // 
            // CashOrBankId
            // 
            this.CashOrBankId.Frozen = true;
            this.CashOrBankId.HeaderText = "Cash/Bank Id";
            this.CashOrBankId.Name = "CashOrBankId";
            this.CashOrBankId.ReadOnly = true;
            this.CashOrBankId.Width = 150;
            // 
            // Amount
            // 
            this.Amount.Frozen = true;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            // 
            // Date
            // 
            this.Date.Frozen = true;
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // AM_ACCOUNT_TYPE_ID
            // 
            this.AM_ACCOUNT_TYPE_ID.Frozen = true;
            this.AM_ACCOUNT_TYPE_ID.HeaderText = "AM_ACCOUNT_TYPE_ID";
            this.AM_ACCOUNT_TYPE_ID.Name = "AM_ACCOUNT_TYPE_ID";
            this.AM_ACCOUNT_TYPE_ID.Visible = false;
            // 
            // Edit
            // 
            this.Edit.Frozen = true;
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = global::SDMS.Properties.Resources.actions_edit;
            this.Edit.Name = "Edit";
            this.Edit.Width = 40;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Del";
            this.Delete.Image = global::SDMS.Properties.Resources.actions_delete;
            this.Delete.Name = "Delete";
            this.Delete.Width = 40;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(141, 41);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(98, 23);
            this.dtpFromDate.TabIndex = 2;
            this.dtpFromDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.dtpFromDate_ValueChanged);
            // 
            // btnDisplay
            // 
            this.btnDisplay.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplay.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDisplay.Location = new System.Drawing.Point(563, 39);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(75, 27);
            this.btnDisplay.TabIndex = 4;
            this.btnDisplay.Text = "Display";
            this.btnDisplay.UseVisualStyleBackColor = false;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // cbCompany
            // 
            this.cbCompany.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Location = new System.Drawing.Point(99, 13);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(355, 24);
            this.cbCompany.TabIndex = 0;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Location = new System.Drawing.Point(252, 343);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(259, 43);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(92, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 27);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.BackColor = System.Drawing.Color.PowderBlue;
            this.lblCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblCompany.Location = new System.Drawing.Point(26, 16);
            this.lblCompany.Margin = new System.Windows.Forms.Padding(0);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(73, 16);
            this.lblCompany.TabIndex = 37;
            this.lblCompany.Text = "Company";
            // 
            // ReceiptVoucherList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(766, 391);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "ReceiptVoucherList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Receipt Or Payment List";
            this.Load += new System.EventHandler(this.ReceiptVoucherList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvVoucherList)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.DataGridView gvVoucherList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn compcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn branchcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn finYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn doctype;
        private System.Windows.Forms.DataGridViewTextBoxColumn DealerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherId;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherType;
        private System.Windows.Forms.DataGridViewTextBoxColumn CashOrBankId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn AM_ACCOUNT_TYPE_ID;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private System.Windows.Forms.TextBox txtDealerCode;
        private System.Windows.Forms.Label label2;
    }
}