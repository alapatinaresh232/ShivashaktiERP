﻿namespace SDMS
{
    partial class BankBillRecievedDetails
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gvOutstandigDetails = new System.Windows.Forms.DataGridView();
            this.chkRow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgCompCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgStateCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgBranchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgFinYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgBillType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Against = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OutstandingAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceivedAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtAgstBilBalAmt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAgstBilAdjAmt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAgnstBillRecvdAmt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSlNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReceivedAmt = new System.Windows.Forms.TextBox();
            this.dtpBillDate = new System.Windows.Forms.DateTimePicker();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtChqNo = new System.Windows.Forms.TextBox();
            this.txtAccountSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbESI = new System.Windows.Forms.Label();
            this.cbAccountName = new System.Windows.Forms.ComboBox();
            this.cbPaymentMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label31 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvOutstandigDetails)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.gvOutstandigDetails);
            this.groupBox1.Controls.Add(this.txtAgstBilBalAmt);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtAgstBilAdjAmt);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtAgnstBillRecvdAmt);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtSlNo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtReceivedAmt);
            this.groupBox1.Controls.Add(this.dtpBillDate);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtChqNo);
            this.groupBox1.Controls.Add(this.txtAccountSearch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lbESI);
            this.groupBox1.Controls.Add(this.cbAccountName);
            this.groupBox1.Controls.Add(this.cbPaymentMode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(699, 412);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // gvOutstandigDetails
            // 
            this.gvOutstandigDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvOutstandigDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvOutstandigDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvOutstandigDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvOutstandigDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvOutstandigDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvOutstandigDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkRow,
            this.SlNo,
            this.AgCompCode,
            this.AgStateCode,
            this.AgBranchCode,
            this.AgFinYear,
            this.AgBillType,
            this.Against,
            this.InvoiceNumber,
            this.InvoiceDate,
            this.InvoiceAmt,
            this.OutstandingAmt,
            this.ReceivedAmt});
            this.gvOutstandigDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvOutstandigDetails.Location = new System.Drawing.Point(10, 123);
            this.gvOutstandigDetails.MultiSelect = false;
            this.gvOutstandigDetails.Name = "gvOutstandigDetails";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvOutstandigDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvOutstandigDetails.RowHeadersVisible = false;
            this.gvOutstandigDetails.Size = new System.Drawing.Size(678, 204);
            this.gvOutstandigDetails.TabIndex = 160;
            this.gvOutstandigDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvOutstandigDetails_CellEndEdit);
            this.gvOutstandigDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvOutstandigDetails_CellClick);
            this.gvOutstandigDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvOutstandigDetails_EditingControlShowing);
            // 
            // chkRow
            // 
            this.chkRow.Frozen = true;
            this.chkRow.HeaderText = "";
            this.chkRow.Name = "chkRow";
            this.chkRow.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chkRow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.chkRow.Visible = false;
            this.chkRow.Width = 30;
            // 
            // SlNo
            // 
            this.SlNo.Frozen = true;
            this.SlNo.HeaderText = "SlNo";
            this.SlNo.Name = "SlNo";
            this.SlNo.Width = 40;
            // 
            // AgCompCode
            // 
            this.AgCompCode.Frozen = true;
            this.AgCompCode.HeaderText = "AgCompCode";
            this.AgCompCode.Name = "AgCompCode";
            this.AgCompCode.Visible = false;
            // 
            // AgStateCode
            // 
            this.AgStateCode.Frozen = true;
            this.AgStateCode.HeaderText = "AgStateCode";
            this.AgStateCode.Name = "AgStateCode";
            this.AgStateCode.Visible = false;
            // 
            // AgBranchCode
            // 
            this.AgBranchCode.Frozen = true;
            this.AgBranchCode.HeaderText = "AgBranchCode";
            this.AgBranchCode.Name = "AgBranchCode";
            this.AgBranchCode.Visible = false;
            // 
            // AgFinYear
            // 
            this.AgFinYear.Frozen = true;
            this.AgFinYear.HeaderText = "AgFinYear";
            this.AgFinYear.Name = "AgFinYear";
            this.AgFinYear.Visible = false;
            // 
            // AgBillType
            // 
            this.AgBillType.Frozen = true;
            this.AgBillType.HeaderText = "AgBillType";
            this.AgBillType.Name = "AgBillType";
            this.AgBillType.Visible = false;
            // 
            // Against
            // 
            this.Against.Frozen = true;
            this.Against.HeaderText = "Against";
            this.Against.Name = "Against";
            this.Against.ReadOnly = true;
            this.Against.Width = 70;
            // 
            // InvoiceNumber
            // 
            this.InvoiceNumber.Frozen = true;
            this.InvoiceNumber.HeaderText = "Invoice No";
            this.InvoiceNumber.Name = "InvoiceNumber";
            this.InvoiceNumber.ReadOnly = true;
            this.InvoiceNumber.Width = 180;
            // 
            // InvoiceDate
            // 
            this.InvoiceDate.Frozen = true;
            this.InvoiceDate.HeaderText = "Invoice Date";
            this.InvoiceDate.MinimumWidth = 20;
            this.InvoiceDate.Name = "InvoiceDate";
            this.InvoiceDate.ReadOnly = true;
            this.InvoiceDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.InvoiceDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // InvoiceAmt
            // 
            this.InvoiceAmt.Frozen = true;
            this.InvoiceAmt.HeaderText = "Invoice Amt";
            this.InvoiceAmt.Name = "InvoiceAmt";
            this.InvoiceAmt.ReadOnly = true;
            this.InvoiceAmt.Width = 90;
            // 
            // OutstandingAmt
            // 
            this.OutstandingAmt.Frozen = true;
            this.OutstandingAmt.HeaderText = "Outstanding Amt";
            this.OutstandingAmt.Name = "OutstandingAmt";
            this.OutstandingAmt.ReadOnly = true;
            this.OutstandingAmt.Width = 95;
            // 
            // ReceivedAmt
            // 
            this.ReceivedAmt.HeaderText = "Received Amt";
            this.ReceivedAmt.Name = "ReceivedAmt";
            this.ReceivedAmt.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ReceivedAmt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ReceivedAmt.Width = 90;
            // 
            // txtAgstBilBalAmt
            // 
            this.txtAgstBilBalAmt.Location = new System.Drawing.Point(567, 380);
            this.txtAgstBilBalAmt.MaxLength = 20;
            this.txtAgstBilBalAmt.Name = "txtAgstBilBalAmt";
            this.txtAgstBilBalAmt.ReadOnly = true;
            this.txtAgstBilBalAmt.Size = new System.Drawing.Size(106, 20);
            this.txtAgstBilBalAmt.TabIndex = 159;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label9.Location = new System.Drawing.Point(472, 382);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 16);
            this.label9.TabIndex = 158;
            this.label9.Text = "Balance Amt";
            // 
            // txtAgstBilAdjAmt
            // 
            this.txtAgstBilAdjAmt.Location = new System.Drawing.Point(568, 356);
            this.txtAgstBilAdjAmt.MaxLength = 20;
            this.txtAgstBilAdjAmt.Name = "txtAgstBilAdjAmt";
            this.txtAgstBilAdjAmt.ReadOnly = true;
            this.txtAgstBilAdjAmt.Size = new System.Drawing.Size(106, 20);
            this.txtAgstBilAdjAmt.TabIndex = 157;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(469, 359);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 16);
            this.label8.TabIndex = 156;
            this.label8.Text = "Adjusted Amt";
            // 
            // txtAgnstBillRecvdAmt
            // 
            this.txtAgnstBillRecvdAmt.Location = new System.Drawing.Point(568, 332);
            this.txtAgnstBillRecvdAmt.MaxLength = 20;
            this.txtAgnstBillRecvdAmt.Name = "txtAgnstBillRecvdAmt";
            this.txtAgnstBillRecvdAmt.ReadOnly = true;
            this.txtAgnstBillRecvdAmt.Size = new System.Drawing.Size(106, 20);
            this.txtAgnstBillRecvdAmt.TabIndex = 155;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(463, 335);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 16);
            this.label7.TabIndex = 154;
            this.label7.Text = "Received Amt";
            // 
            // txtSlNo
            // 
            this.txtSlNo.BackColor = System.Drawing.SystemColors.Info;
            this.txtSlNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSlNo.Location = new System.Drawing.Point(114, 22);
            this.txtSlNo.MaxLength = 20;
            this.txtSlNo.Name = "txtSlNo";
            this.txtSlNo.ReadOnly = true;
            this.txtSlNo.Size = new System.Drawing.Size(106, 23);
            this.txtSlNo.TabIndex = 153;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(234, 100);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 16);
            this.label5.TabIndex = 149;
            this.label5.Text = "Remarks/RefNo";
            // 
            // txtReceivedAmt
            // 
            this.txtReceivedAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceivedAmt.Location = new System.Drawing.Point(115, 95);
            this.txtReceivedAmt.MaxLength = 20;
            this.txtReceivedAmt.Name = "txtReceivedAmt";
            this.txtReceivedAmt.Size = new System.Drawing.Size(106, 23);
            this.txtReceivedAmt.TabIndex = 4;
            this.txtReceivedAmt.TextChanged += new System.EventHandler(this.txtReceivedAmt_TextChanged);
            this.txtReceivedAmt.Validated += new System.EventHandler(this.txtReceivedAmt_Validated);
            this.txtReceivedAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReceivedAmt_KeyPress);
            // 
            // dtpBillDate
            // 
            this.dtpBillDate.CustomFormat = "dd/MM/yyyy";
            this.dtpBillDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBillDate.Location = new System.Drawing.Point(581, 72);
            this.dtpBillDate.Name = "dtpBillDate";
            this.dtpBillDate.Size = new System.Drawing.Size(102, 23);
            this.dtpBillDate.TabIndex = 3;
            this.dtpBillDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // txtRemarks
            // 
            this.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(352, 96);
            this.txtRemarks.MaxLength = 300;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(330, 23);
            this.txtRemarks.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(10, 99);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 16);
            this.label2.TabIndex = 145;
            this.label2.Text = "Received Amt";
            // 
            // txtChqNo
            // 
            this.txtChqNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChqNo.Location = new System.Drawing.Point(352, 71);
            this.txtChqNo.MaxLength = 20;
            this.txtChqNo.Name = "txtChqNo";
            this.txtChqNo.Size = new System.Drawing.Size(189, 23);
            this.txtChqNo.TabIndex = 2;
            // 
            // txtAccountSearch
            // 
            this.txtAccountSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountSearch.Location = new System.Drawing.Point(115, 46);
            this.txtAccountSearch.MaxLength = 20;
            this.txtAccountSearch.Name = "txtAccountSearch";
            this.txtAccountSearch.Size = new System.Drawing.Size(106, 23);
            this.txtAccountSearch.TabIndex = 0;
            this.txtAccountSearch.TextChanged += new System.EventHandler(this.txtAccountSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(265, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 16);
            this.label1.TabIndex = 141;
            this.label1.Text = "Chq/DD No";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(65, 25);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 16);
            this.label6.TabIndex = 152;
            this.label6.Text = "Sl No.";
            // 
            // lbESI
            // 
            this.lbESI.AutoSize = true;
            this.lbESI.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbESI.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lbESI.Location = new System.Drawing.Point(4, 74);
            this.lbESI.Name = "lbESI";
            this.lbESI.Size = new System.Drawing.Size(111, 16);
            this.lbESI.TabIndex = 140;
            this.lbESI.Text = "Payment Mode";
            // 
            // cbAccountName
            // 
            this.cbAccountName.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cbAccountName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbAccountName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAccountName.FormattingEnabled = true;
            this.cbAccountName.Location = new System.Drawing.Point(223, 46);
            this.cbAccountName.Name = "cbAccountName";
            this.cbAccountName.Size = new System.Drawing.Size(459, 24);
            this.cbAccountName.TabIndex = 138;
            this.cbAccountName.SelectedIndexChanged += new System.EventHandler(this.cbAccountName_SelectedIndexChanged);
            // 
            // cbPaymentMode
            // 
            this.cbPaymentMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPaymentMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPaymentMode.FormattingEnabled = true;
            this.cbPaymentMode.Items.AddRange(new object[] {
            "--Select--",
            "CASH",
            "CHEQUE",
            "DD",
            "ONLINE"});
            this.cbPaymentMode.Location = new System.Drawing.Point(115, 70);
            this.cbPaymentMode.Name = "cbPaymentMode";
            this.cbPaymentMode.Size = new System.Drawing.Size(106, 24);
            this.cbPaymentMode.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(35, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 137;
            this.label3.Text = "Account Id";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnClose);
            this.groupBox5.Controls.Add(this.btnCancel);
            this.groupBox5.Controls.Add(this.btnSave);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox5.Location = new System.Drawing.Point(148, 341);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(296, 44);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(188, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 26);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "C&lose";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(111, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Clea&r";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoEllipsis = true;
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(34, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.Navy;
            this.label31.Location = new System.Drawing.Point(283, 1);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(131, 22);
            this.label31.TabIndex = 112;
            this.label31.Text = "Received From";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label11.Location = new System.Drawing.Point(537, 75);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 16);
            this.label11.TabIndex = 143;
            this.label11.Text = " Date";
            // 
            // BankBillRecievedDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(703, 418);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "BankBillRecievedDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BankBillRecievedDetails";
            this.Load += new System.EventHandler(this.BankBillRecievedDetails_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvOutstandigDetails)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAgstBilBalAmt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAgstBilAdjAmt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAgnstBillRecvdAmt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSlNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtReceivedAmt;
        private System.Windows.Forms.DateTimePicker dtpBillDate;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtChqNo;
        private System.Windows.Forms.TextBox txtAccountSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbESI;
        private System.Windows.Forms.ComboBox cbAccountName;
        private System.Windows.Forms.ComboBox cbPaymentMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.DataGridView gvOutstandigDetails;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgCompCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgStateCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgBranchCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgFinYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgBillType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Against;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn OutstandingAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceivedAmt;
    }
}