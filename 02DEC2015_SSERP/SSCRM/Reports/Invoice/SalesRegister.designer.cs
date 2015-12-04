namespace SSCRM
{
    partial class SalesRegister
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
            this.cmbSalesInvoice = new System.Windows.Forms.ComboBox();
            this.rdbGroupSales = new System.Windows.Forms.RadioButton();
            this.rdbInvoiceDetail = new System.Windows.Forms.RadioButton();
            this.rdbProductCodes = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpInvoiceToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpInvoiceFromDate = new System.Windows.Forms.DateTimePicker();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.cmbSalesInvoice);
            this.groupBox1.Controls.Add(this.rdbGroupSales);
            this.groupBox1.Controls.Add(this.rdbInvoiceDetail);
            this.groupBox1.Controls.Add(this.rdbProductCodes);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpInvoiceToDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpInvoiceFromDate);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(8, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 122);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // cmbSalesInvoice
            // 
            this.cmbSalesInvoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSalesInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSalesInvoice.FormattingEnabled = true;
            this.cmbSalesInvoice.Items.AddRange(new object[] {
            "Order number wise",
            "Invoice number wise",
            "Group, Order number wise",
            "Group, Invoice number wise",
            "Organic Manure Orders"});
            this.cmbSalesInvoice.Location = new System.Drawing.Point(227, 32);
            this.cmbSalesInvoice.Name = "cmbSalesInvoice";
            this.cmbSalesInvoice.Size = new System.Drawing.Size(227, 26);
            this.cmbSalesInvoice.TabIndex = 15;
            // 
            // rdbGroupSales
            // 
            this.rdbGroupSales.AutoSize = true;
            this.rdbGroupSales.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbGroupSales.ForeColor = System.Drawing.Color.MidnightBlue;
            this.rdbGroupSales.Location = new System.Drawing.Point(176, 89);
            this.rdbGroupSales.Name = "rdbGroupSales";
            this.rdbGroupSales.Size = new System.Drawing.Size(158, 20);
            this.rdbGroupSales.TabIndex = 13;
            this.rdbGroupSales.TabStop = true;
            this.rdbGroupSales.Text = "Group Sale Invoice";
            this.rdbGroupSales.UseVisualStyleBackColor = true;
            this.rdbGroupSales.Visible = false;
            this.rdbGroupSales.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rdbInvoiceDetail
            // 
            this.rdbInvoiceDetail.AutoSize = true;
            this.rdbInvoiceDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbInvoiceDetail.ForeColor = System.Drawing.Color.MidnightBlue;
            this.rdbInvoiceDetail.Location = new System.Drawing.Point(56, 33);
            this.rdbInvoiceDetail.Name = "rdbInvoiceDetail";
            this.rdbInvoiceDetail.Size = new System.Drawing.Size(165, 20);
            this.rdbInvoiceDetail.TabIndex = 12;
            this.rdbInvoiceDetail.TabStop = true;
            this.rdbInvoiceDetail.Text = "Sales Invoice Detail";
            this.rdbInvoiceDetail.UseVisualStyleBackColor = true;
            this.rdbInvoiceDetail.CheckedChanged += new System.EventHandler(this.rdbInvoiceDetail_CheckedChanged);
            // 
            // rdbProductCodes
            // 
            this.rdbProductCodes.AutoSize = true;
            this.rdbProductCodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbProductCodes.ForeColor = System.Drawing.Color.MidnightBlue;
            this.rdbProductCodes.Location = new System.Drawing.Point(176, 104);
            this.rdbProductCodes.Name = "rdbProductCodes";
            this.rdbProductCodes.Size = new System.Drawing.Size(128, 20);
            this.rdbProductCodes.TabIndex = 11;
            this.rdbProductCodes.TabStop = true;
            this.rdbProductCodes.Text = "Product Codes";
            this.rdbProductCodes.UseVisualStyleBackColor = true;
            this.rdbProductCodes.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(306, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "To";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dtpInvoiceToDate
            // 
            this.dtpInvoiceToDate.CustomFormat = "MMMyyyy";
            this.dtpInvoiceToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInvoiceToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvoiceToDate.Location = new System.Drawing.Point(333, 69);
            this.dtpInvoiceToDate.Name = "dtpInvoiceToDate";
            this.dtpInvoiceToDate.Size = new System.Drawing.Size(105, 22);
            this.dtpInvoiceToDate.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label2.Location = new System.Drawing.Point(101, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Doc. Month ";
            // 
            // dtpInvoiceFromDate
            // 
            this.dtpInvoiceFromDate.CustomFormat = "MMMyyyy";
            this.dtpInvoiceFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInvoiceFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvoiceFromDate.Location = new System.Drawing.Point(196, 69);
            this.dtpInvoiceFromDate.Name = "dtpInvoiceFromDate";
            this.dtpInvoiceFromDate.Size = new System.Drawing.Size(104, 22);
            this.dtpInvoiceFromDate.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(224, 11);
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
            this.btnReport.Location = new System.Drawing.Point(48, 11);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(78, 30);
            this.btnReport.TabIndex = 0;
            this.btnReport.Text = "&Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Location = new System.Drawing.Point(4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(527, 198);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDownload);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnReport);
            this.groupBox3.Location = new System.Drawing.Point(88, 143);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 47);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Location = new System.Drawing.Point(132, 11);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(86, 30);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "&Download";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // SalesRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(535, 206);
            this.Controls.Add(this.groupBox2);
            this.Name = "SalesRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SalesRegister";
            this.Load += new System.EventHandler(this.SalesRegister_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpInvoiceFromDate;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpInvoiceToDate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdbProductCodes;
        private System.Windows.Forms.RadioButton rdbInvoiceDetail;
        private System.Windows.Forms.RadioButton rdbGroupSales;
        private System.Windows.Forms.ComboBox cmbSalesInvoice;
        private System.Windows.Forms.Button btnDownload;
    }
}