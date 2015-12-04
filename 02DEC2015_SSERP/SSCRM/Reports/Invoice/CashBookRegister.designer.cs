namespace SSCRM
{
    partial class CashBookRegister
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
            this.cmbAcc = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbTrans = new System.Windows.Forms.RadioButton();
            this.dtpInvoiceFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpInvoiceToDate = new System.Windows.Forms.DateTimePicker();
            this.cmbTranPeriod = new System.Windows.Forms.ComboBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.rbTrnPeriod = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.rbtnSummary = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cbRegisterType = new System.Windows.Forms.ComboBox();
            this.lblAcc = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbAcc);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.rbtnSummary);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbRegisterType);
            this.groupBox1.Controls.Add(this.lblAcc);
            this.groupBox1.Location = new System.Drawing.Point(3, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(516, 219);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cmbAcc
            // 
            this.cmbAcc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAcc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAcc.FormattingEnabled = true;
            this.cmbAcc.Items.AddRange(new object[] {
            "BOOK",
            "RECEIPTS"});
            this.cmbAcc.Location = new System.Drawing.Point(129, 107);
            this.cmbAcc.Name = "cmbAcc";
            this.cmbAcc.Size = new System.Drawing.Size(357, 24);
            this.cmbAcc.TabIndex = 127;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.rbTrans);
            this.groupBox4.Controls.Add(this.dtpInvoiceFromDate);
            this.groupBox4.Controls.Add(this.dtpInvoiceToDate);
            this.groupBox4.Controls.Add(this.cmbTranPeriod);
            this.groupBox4.Controls.Add(this.lblTo);
            this.groupBox4.Controls.Add(this.rbTrnPeriod);
            this.groupBox4.Location = new System.Drawing.Point(34, 21);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(444, 79);
            this.groupBox4.TabIndex = 126;
            this.groupBox4.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label3.Location = new System.Drawing.Point(203, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 16);
            this.label3.TabIndex = 128;
            this.label3.Text = "Last";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(295, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 127;
            this.label1.Text = "Months";
            // 
            // rbTrans
            // 
            this.rbTrans.AutoSize = true;
            this.rbTrans.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTrans.ForeColor = System.Drawing.Color.Navy;
            this.rbTrans.Location = new System.Drawing.Point(20, 19);
            this.rbTrans.Name = "rbTrans";
            this.rbTrans.Size = new System.Drawing.Size(153, 21);
            this.rbTrans.TabIndex = 122;
            this.rbTrans.TabStop = true;
            this.rbTrans.Text = "Transaction From";
            this.rbTrans.UseVisualStyleBackColor = true;
            this.rbTrans.CheckedChanged += new System.EventHandler(this.rbTrans_CheckedChanged);
            // 
            // dtpInvoiceFromDate
            // 
            this.dtpInvoiceFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpInvoiceFromDate.Enabled = false;
            this.dtpInvoiceFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInvoiceFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvoiceFromDate.Location = new System.Drawing.Point(185, 19);
            this.dtpInvoiceFromDate.Name = "dtpInvoiceFromDate";
            this.dtpInvoiceFromDate.Size = new System.Drawing.Size(104, 22);
            this.dtpInvoiceFromDate.TabIndex = 11;
            // 
            // dtpInvoiceToDate
            // 
            this.dtpInvoiceToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpInvoiceToDate.Enabled = false;
            this.dtpInvoiceToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInvoiceToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvoiceToDate.Location = new System.Drawing.Point(316, 19);
            this.dtpInvoiceToDate.Name = "dtpInvoiceToDate";
            this.dtpInvoiceToDate.Size = new System.Drawing.Size(105, 22);
            this.dtpInvoiceToDate.TabIndex = 13;
            // 
            // cmbTranPeriod
            // 
            this.cmbTranPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTranPeriod.Enabled = false;
            this.cmbTranPeriod.FormattingEnabled = true;
            this.cmbTranPeriod.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24"});
            this.cmbTranPeriod.Location = new System.Drawing.Point(244, 47);
            this.cmbTranPeriod.Name = "cmbTranPeriod";
            this.cmbTranPeriod.Size = new System.Drawing.Size(45, 21);
            this.cmbTranPeriod.TabIndex = 124;
            this.cmbTranPeriod.SelectedIndexChanged += new System.EventHandler(this.cmbTranPeriod_SelectedIndexChanged);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Enabled = false;
            this.lblTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTo.Location = new System.Drawing.Point(289, 22);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(27, 16);
            this.lblTo.TabIndex = 14;
            this.lblTo.Text = "To";
            // 
            // rbTrnPeriod
            // 
            this.rbTrnPeriod.AutoSize = true;
            this.rbTrnPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTrnPeriod.ForeColor = System.Drawing.Color.Navy;
            this.rbTrnPeriod.Location = new System.Drawing.Point(20, 46);
            this.rbTrnPeriod.Name = "rbTrnPeriod";
            this.rbTrnPeriod.Size = new System.Drawing.Size(164, 21);
            this.rbTrnPeriod.TabIndex = 123;
            this.rbTrnPeriod.TabStop = true;
            this.rbTrnPeriod.Text = "Transaction Period";
            this.rbTrnPeriod.UseVisualStyleBackColor = true;
            this.rbTrnPeriod.CheckedChanged += new System.EventHandler(this.rbTrnPeriod_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDownload);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnReport);
            this.groupBox3.Location = new System.Drawing.Point(70, 164);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(389, 47);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Location = new System.Drawing.Point(46, 11);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(91, 30);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "&Download";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(227, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Location = new System.Drawing.Point(143, 11);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(78, 30);
            this.btnReport.TabIndex = 0;
            this.btnReport.Text = "&Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // rbtnSummary
            // 
            this.rbtnSummary.AutoSize = true;
            this.rbtnSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.rbtnSummary.ForeColor = System.Drawing.Color.MidnightBlue;
            this.rbtnSummary.Location = new System.Drawing.Point(219, 138);
            this.rbtnSummary.Name = "rbtnSummary";
            this.rbtnSummary.Size = new System.Drawing.Size(90, 20);
            this.rbtnSummary.TabIndex = 125;
            this.rbtnSummary.TabStop = true;
            this.rbtnSummary.Text = "Summary";
            this.rbtnSummary.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label2.Location = new System.Drawing.Point(63, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "ReportType";
            this.label2.Visible = false;
            // 
            // cbRegisterType
            // 
            this.cbRegisterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRegisterType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRegisterType.FormattingEnabled = true;
            this.cbRegisterType.Items.AddRange(new object[] {
            "BOOK",
            "RECEIPTS"});
            this.cbRegisterType.Location = new System.Drawing.Point(160, 134);
            this.cbRegisterType.Name = "cbRegisterType";
            this.cbRegisterType.Size = new System.Drawing.Size(134, 24);
            this.cbRegisterType.TabIndex = 17;
            this.cbRegisterType.Visible = false;
            // 
            // lblAcc
            // 
            this.lblAcc.AutoSize = true;
            this.lblAcc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcc.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblAcc.Location = new System.Drawing.Point(39, 110);
            this.lblAcc.Name = "lblAcc";
            this.lblAcc.Size = new System.Drawing.Size(63, 16);
            this.lblAcc.TabIndex = 128;
            this.lblAcc.Text = "Account";
            // 
            // CashBookRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(523, 217);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "CashBookRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CashBook";
            this.Load += new System.EventHandler(this.CashBookRegister_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpInvoiceToDate;
        private System.Windows.Forms.DateTimePicker dtpInvoiceFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbRegisterType;
        private System.Windows.Forms.RadioButton rbTrans;
        private System.Windows.Forms.RadioButton rbTrnPeriod;
        private System.Windows.Forms.ComboBox cmbTranPeriod;
        private System.Windows.Forms.RadioButton rbtnSummary;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbAcc;
        private System.Windows.Forms.Label lblAcc;
        private System.Windows.Forms.Button btnDownload;
    }
}