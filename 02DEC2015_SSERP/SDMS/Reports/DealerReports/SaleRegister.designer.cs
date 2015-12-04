namespace SDMS
{
    partial class SaleRegister
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnDetailed = new System.Windows.Forms.RadioButton();
            this.rbtnSummary = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cbBranches = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpInvoiceToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpInvoiceFromDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Location = new System.Drawing.Point(2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(526, 182);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnReport);
            this.groupBox3.Location = new System.Drawing.Point(88, 129);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 47);
            this.groupBox3.TabIndex = 6;
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
            this.btnClose.Text = "&Close";
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
            this.btnReport.Text = "&Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.rbtnDetailed);
            this.groupBox1.Controls.Add(this.rbtnSummary);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbBranches);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpInvoiceToDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpInvoiceFromDate);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(8, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 116);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // rbtnDetailed
            // 
            this.rbtnDetailed.AutoSize = true;
            this.rbtnDetailed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.rbtnDetailed.ForeColor = System.Drawing.Color.MidnightBlue;
            this.rbtnDetailed.Location = new System.Drawing.Point(126, 84);
            this.rbtnDetailed.Name = "rbtnDetailed";
            this.rbtnDetailed.Size = new System.Drawing.Size(85, 20);
            this.rbtnDetailed.TabIndex = 20;
            this.rbtnDetailed.TabStop = true;
            this.rbtnDetailed.Tag = "DETAILED";
            this.rbtnDetailed.Text = "Detailed";
            this.rbtnDetailed.UseVisualStyleBackColor = true;
            // 
            // rbtnSummary
            // 
            this.rbtnSummary.AutoSize = true;
            this.rbtnSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.rbtnSummary.ForeColor = System.Drawing.Color.MidnightBlue;
            this.rbtnSummary.Location = new System.Drawing.Point(302, 83);
            this.rbtnSummary.Name = "rbtnSummary";
            this.rbtnSummary.Size = new System.Drawing.Size(90, 20);
            this.rbtnSummary.TabIndex = 19;
            this.rbtnSummary.TabStop = true;
            this.rbtnSummary.Text = "Summary";
            this.rbtnSummary.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label3.Location = new System.Drawing.Point(66, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "SP/PU";
            // 
            // cbBranches
            // 
            this.cbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranches.FormattingEnabled = true;
            this.cbBranches.Items.AddRange(new object[] {
            "Order number wise",
            "Invoice number wise",
            "Group, Order number wise",
            "Group, Invoice number wise"});
            this.cbBranches.Location = new System.Drawing.Point(126, 22);
            this.cbBranches.Name = "cbBranches";
            this.cbBranches.Size = new System.Drawing.Size(319, 24);
            this.cbBranches.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(288, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "To";
            // 
            // dtpInvoiceToDate
            // 
            this.dtpInvoiceToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpInvoiceToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInvoiceToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvoiceToDate.Location = new System.Drawing.Point(315, 55);
            this.dtpInvoiceToDate.Name = "dtpInvoiceToDate";
            this.dtpInvoiceToDate.Size = new System.Drawing.Size(105, 22);
            this.dtpInvoiceToDate.TabIndex = 9;
            this.dtpInvoiceToDate.ValueChanged += new System.EventHandler(this.dtpInvoiceToDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label2.Location = new System.Drawing.Point(95, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "From";
            // 
            // dtpInvoiceFromDate
            // 
            this.dtpInvoiceFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpInvoiceFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInvoiceFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvoiceFromDate.Location = new System.Drawing.Point(144, 56);
            this.dtpInvoiceFromDate.Name = "dtpInvoiceFromDate";
            this.dtpInvoiceFromDate.Size = new System.Drawing.Size(104, 22);
            this.dtpInvoiceFromDate.TabIndex = 2;
            this.dtpInvoiceFromDate.ValueChanged += new System.EventHandler(this.dtpInvoiceFromDate_ValueChanged);
            // 
            // SaleRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(530, 186);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Name = "SaleRegister";
            this.Text = "SalesRegister";
            this.Load += new System.EventHandler(this.SaleRegister_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbBranches;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpInvoiceToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpInvoiceFromDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbtnDetailed;
        private System.Windows.Forms.RadioButton rbtnSummary;
    }
}