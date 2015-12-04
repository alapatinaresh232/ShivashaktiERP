namespace SSCRM
{
    partial class SalesDataMonthClosing
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
            this.dtpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cbUserBranch = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gvProductDetails = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ecode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GCName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Camp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiffCust = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiffQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiffRev = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiffPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiffFreeUnits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SumCust = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SumQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SumRev = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SumPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvCust = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvRev = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aflag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkApprove = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.dtpInvoiceDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbUserBranch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(5, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(939, 573);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Invoice Details";
            // 
            // dtpInvoiceDate
            // 
            this.dtpInvoiceDate.CustomFormat = "MMMyyyy";
            this.dtpInvoiceDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvoiceDate.Location = new System.Drawing.Point(96, 61);
            this.dtpInvoiceDate.Name = "dtpInvoiceDate";
            this.dtpInvoiceDate.Size = new System.Drawing.Size(103, 24);
            this.dtpInvoiceDate.TabIndex = 53;
            this.dtpInvoiceDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpInvoiceDate.ValueChanged += new System.EventHandler(this.dtpInvoiceDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(13, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 52;
            this.label2.Text = "Doc Month";
            // 
            // cbUserBranch
            // 
            this.cbUserBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUserBranch.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbUserBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUserBranch.FormattingEnabled = true;
            this.cbUserBranch.Location = new System.Drawing.Point(549, 27);
            this.cbUserBranch.Name = "cbUserBranch";
            this.cbUserBranch.Size = new System.Drawing.Size(360, 26);
            this.cbUserBranch.TabIndex = 51;
            this.cbUserBranch.SelectedIndexChanged += new System.EventHandler(this.cbUserBranch_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(490, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 50;
            this.label1.Text = "Branch";
            // 
            // cbCompany
            // 
            this.cbCompany.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Location = new System.Drawing.Point(96, 27);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(360, 26);
            this.cbCompany.TabIndex = 49;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label24.Location = new System.Drawing.Point(20, 31);
            this.label24.Margin = new System.Windows.Forms.Padding(0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(73, 16);
            this.label24.TabIndex = 48;
            this.label24.Text = "Company";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.gvProductDetails);
            this.groupBox3.Location = new System.Drawing.Point(6, 95);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(930, 477);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Product Details";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Location = new System.Drawing.Point(276, 426);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 45);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(233, 13);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(147, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoEllipsis = true;
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(72, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gvProductDetails
            // 
            this.gvProductDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvProductDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvProductDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvProductDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvProductDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvProductDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProductDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.Ecode,
            this.GCName,
            this.Camp,
            this.DiffCust,
            this.DiffQty,
            this.DiffRev,
            this.DiffPoints,
            this.DiffFreeUnits,
            this.SumCust,
            this.SumQty,
            this.SumRev,
            this.SumPoints,
            this.InvCust,
            this.InvQty,
            this.InvRev,
            this.InvPoints,
            this.Status,
            this.aflag,
            this.chkApprove});
            this.gvProductDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvProductDetails.Location = new System.Drawing.Point(6, 25);
            this.gvProductDetails.MultiSelect = false;
            this.gvProductDetails.Name = "gvProductDetails";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvProductDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvProductDetails.RowHeadersVisible = false;
            this.gvProductDetails.Size = new System.Drawing.Size(918, 397);
            this.gvProductDetails.TabIndex = 18;
            this.gvProductDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProductDetails_CellEndEdit);
            this.gvProductDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProductDetails_CellClick);
            // 
            // SLNO
            // 
            this.SLNO.Frozen = true;
            this.SLNO.HeaderText = "Sl.No";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SLNO.Width = 40;
            // 
            // Ecode
            // 
            this.Ecode.HeaderText = "Ecode";
            this.Ecode.Name = "Ecode";
            this.Ecode.ReadOnly = true;
            this.Ecode.Width = 60;
            // 
            // GCName
            // 
            this.GCName.HeaderText = "Name";
            this.GCName.Name = "GCName";
            this.GCName.ReadOnly = true;
            this.GCName.Width = 180;
            // 
            // Camp
            // 
            this.Camp.HeaderText = "Camp";
            this.Camp.Name = "Camp";
            this.Camp.ReadOnly = true;
            this.Camp.Width = 120;
            // 
            // DiffCust
            // 
            this.DiffCust.HeaderText = "Diff Cust";
            this.DiffCust.Name = "DiffCust";
            this.DiffCust.ReadOnly = true;
            this.DiffCust.Width = 65;
            // 
            // DiffQty
            // 
            this.DiffQty.HeaderText = "Diff Qty";
            this.DiffQty.Name = "DiffQty";
            this.DiffQty.ReadOnly = true;
            this.DiffQty.Width = 65;
            // 
            // DiffRev
            // 
            this.DiffRev.HeaderText = "Diff Rev";
            this.DiffRev.Name = "DiffRev";
            this.DiffRev.ReadOnly = true;
            this.DiffRev.Width = 80;
            // 
            // DiffPoints
            // 
            this.DiffPoints.HeaderText = "Diff Points";
            this.DiffPoints.Name = "DiffPoints";
            this.DiffPoints.ReadOnly = true;
            this.DiffPoints.Width = 70;
            // 
            // DiffFreeUnits
            // 
            this.DiffFreeUnits.HeaderText = "Diff FreeUnits";
            this.DiffFreeUnits.Name = "DiffFreeUnits";
            this.DiffFreeUnits.ReadOnly = true;
            this.DiffFreeUnits.Width = 60;
            // 
            // SumCust
            // 
            this.SumCust.HeaderText = "SumCust";
            this.SumCust.Name = "SumCust";
            this.SumCust.ReadOnly = true;
            this.SumCust.Visible = false;
            // 
            // SumQty
            // 
            this.SumQty.HeaderText = "SumQty";
            this.SumQty.Name = "SumQty";
            this.SumQty.ReadOnly = true;
            this.SumQty.Visible = false;
            // 
            // SumRev
            // 
            this.SumRev.HeaderText = "SumRev";
            this.SumRev.Name = "SumRev";
            this.SumRev.ReadOnly = true;
            this.SumRev.Visible = false;
            // 
            // SumPoints
            // 
            this.SumPoints.HeaderText = "SumPoints";
            this.SumPoints.Name = "SumPoints";
            this.SumPoints.ReadOnly = true;
            this.SumPoints.Visible = false;
            // 
            // InvCust
            // 
            this.InvCust.HeaderText = "InvCust";
            this.InvCust.Name = "InvCust";
            this.InvCust.ReadOnly = true;
            this.InvCust.Visible = false;
            // 
            // InvQty
            // 
            this.InvQty.HeaderText = "InvQty";
            this.InvQty.Name = "InvQty";
            this.InvQty.ReadOnly = true;
            this.InvQty.Visible = false;
            // 
            // InvRev
            // 
            this.InvRev.HeaderText = "InvRev";
            this.InvRev.Name = "InvRev";
            this.InvRev.ReadOnly = true;
            this.InvRev.Visible = false;
            // 
            // InvPoints
            // 
            this.InvPoints.HeaderText = "InvPoints";
            this.InvPoints.Name = "InvPoints";
            this.InvPoints.ReadOnly = true;
            this.InvPoints.Visible = false;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // aflag
            // 
            this.aflag.HeaderText = "aflag";
            this.aflag.Name = "aflag";
            this.aflag.ReadOnly = true;
            this.aflag.Visible = false;
            // 
            // chkApprove
            // 
            this.chkApprove.HeaderText = "";
            this.chkApprove.Name = "chkApprove";
            this.chkApprove.Width = 35;
            // 
            // SalesDataMonthClosing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 581);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "SalesDataMonthClosing";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Data Month Closing";
            this.Load += new System.EventHandler(this.SalesDataMonthClosing_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvProductDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.DataGridView gvProductDetails;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.ComboBox cbUserBranch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ecode;
        private System.Windows.Forms.DataGridViewTextBoxColumn GCName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Camp;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiffCust;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiffQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiffRev;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiffPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiffFreeUnits;
        private System.Windows.Forms.DataGridViewTextBoxColumn SumCust;
        private System.Windows.Forms.DataGridViewTextBoxColumn SumQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn SumRev;
        private System.Windows.Forms.DataGridViewTextBoxColumn SumPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvCust;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvRev;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn aflag;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkApprove;
    }
}