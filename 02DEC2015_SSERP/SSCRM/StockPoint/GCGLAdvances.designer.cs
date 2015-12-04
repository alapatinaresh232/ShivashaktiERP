namespace SSCRM
{
    partial class GCGLAdvances
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbDocMonths = new System.Windows.Forms.ComboBox();
            this.txtEcodeSearch = new System.Windows.Forms.TextBox();
            this.cbEcode = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbFinYear = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gvAdvancesDetl = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocMonth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CampName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SRName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.cbBranches = new System.Windows.Forms.ComboBox();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvAdvancesDetl)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.cbDocMonths);
            this.groupBox1.Controls.Add(this.txtEcodeSearch);
            this.groupBox1.Controls.Add(this.cbEcode);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbFinYear);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cbBranches);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(982, 472);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // cbDocMonths
            // 
            this.cbDocMonths.AllowDrop = true;
            this.cbDocMonths.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbDocMonths.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDocMonths.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDocMonths.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDocMonths.FormattingEnabled = true;
            this.cbDocMonths.Items.AddRange(new object[] {
            "CURRENT MONTH",
            "PREVIOUS MONTHS"});
            this.cbDocMonths.Location = new System.Drawing.Point(293, 44);
            this.cbDocMonths.Name = "cbDocMonths";
            this.cbDocMonths.Size = new System.Drawing.Size(143, 23);
            this.cbDocMonths.TabIndex = 28;
            this.cbDocMonths.SelectedIndexChanged += new System.EventHandler(this.cbDocMonths_SelectedIndexChanged);
            // 
            // txtEcodeSearch
            // 
            this.txtEcodeSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcodeSearch.Location = new System.Drawing.Point(524, 47);
            this.txtEcodeSearch.MaxLength = 20;
            this.txtEcodeSearch.Name = "txtEcodeSearch";
            this.txtEcodeSearch.Size = new System.Drawing.Size(89, 21);
            this.txtEcodeSearch.TabIndex = 25;
            this.txtEcodeSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEcodeSearch_KeyUp);
            // 
            // cbEcode
            // 
            this.cbEcode.AllowDrop = true;
            this.cbEcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbEcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEcode.FormattingEnabled = true;
            this.cbEcode.Location = new System.Drawing.Point(614, 45);
            this.cbEcode.Name = "cbEcode";
            this.cbEcode.Size = new System.Drawing.Size(317, 23);
            this.cbEcode.TabIndex = 26;
            this.cbEcode.SelectedIndexChanged += new System.EventHandler(this.cbEcode_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label16.Location = new System.Drawing.Point(467, 49);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(50, 13);
            this.label16.TabIndex = 27;
            this.label16.Text = "GC /GL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(209, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 16);
            this.label4.TabIndex = 23;
            this.label4.Text = "Doc Month";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(21, 47);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 16);
            this.label8.TabIndex = 3;
            this.label8.Text = "Fin Year";
            // 
            // cbFinYear
            // 
            this.cbFinYear.AllowDrop = true;
            this.cbFinYear.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbFinYear.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFinYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFinYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFinYear.FormattingEnabled = true;
            this.cbFinYear.Location = new System.Drawing.Point(90, 44);
            this.cbFinYear.Name = "cbFinYear";
            this.cbFinYear.Size = new System.Drawing.Size(114, 23);
            this.cbFinYear.TabIndex = 4;
            this.cbFinYear.SelectedIndexChanged += new System.EventHandler(this.cbFinYear_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gvAdvancesDetl);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(3, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(975, 351);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "GC/GL Advances";
            // 
            // gvAdvancesDetl
            // 
            this.gvAdvancesDetl.AllowUserToAddRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            this.gvAdvancesDetl.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.gvAdvancesDetl.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvAdvancesDetl.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvAdvancesDetl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gvAdvancesDetl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvAdvancesDetl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.OrderNo,
            this.OrderDate,
            this.DocMonth,
            this.CampName,
            this.SRName,
            this.CustomerName,
            this.OrderQty,
            this.OrderAmt,
            this.Status,
            this.Edit});
            this.gvAdvancesDetl.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvAdvancesDetl.Location = new System.Drawing.Point(7, 20);
            this.gvAdvancesDetl.MultiSelect = false;
            this.gvAdvancesDetl.Name = "gvAdvancesDetl";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvAdvancesDetl.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.gvAdvancesDetl.RowHeadersVisible = false;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Navy;
            this.gvAdvancesDetl.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.gvAdvancesDetl.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.gvAdvancesDetl.Size = new System.Drawing.Size(962, 324);
            this.gvAdvancesDetl.TabIndex = 0;
            this.gvAdvancesDetl.TabStop = false;
            this.gvAdvancesDetl.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvAdvancesDetl_CellClick);
            // 
            // SLNO
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.SLNO.DefaultCellStyle = dataGridViewCellStyle8;
            this.SLNO.Frozen = true;
            this.SLNO.HeaderText = "Sl.No";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SLNO.Width = 50;
            // 
            // OrderNo
            // 
            this.OrderNo.HeaderText = "Order No";
            this.OrderNo.Name = "OrderNo";
            this.OrderNo.ReadOnly = true;
            // 
            // OrderDate
            // 
            this.OrderDate.HeaderText = "Date";
            this.OrderDate.Name = "OrderDate";
            this.OrderDate.ReadOnly = true;
            this.OrderDate.Width = 90;
            // 
            // DocMonth
            // 
            this.DocMonth.HeaderText = "Doc Month";
            this.DocMonth.Name = "DocMonth";
            this.DocMonth.ReadOnly = true;
            this.DocMonth.Width = 80;
            // 
            // CampName
            // 
            this.CampName.HeaderText = "Camp Name";
            this.CampName.Name = "CampName";
            this.CampName.ReadOnly = true;
            // 
            // SRName
            // 
            this.SRName.HeaderText = "SR Name";
            this.SRName.Name = "SRName";
            this.SRName.ReadOnly = true;
            this.SRName.Width = 160;
            // 
            // CustomerName
            // 
            this.CustomerName.HeaderText = "Cust Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.Width = 190;
            // 
            // OrderQty
            // 
            this.OrderQty.HeaderText = "Qty";
            this.OrderQty.Name = "OrderQty";
            this.OrderQty.ReadOnly = true;
            this.OrderQty.Width = 50;
            // 
            // OrderAmt
            // 
            this.OrderAmt.HeaderText = "Amount";
            this.OrderAmt.Name = "OrderAmt";
            this.OrderAmt.ReadOnly = true;
            this.OrderAmt.Width = 80;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Visible = false;
            this.Status.Width = 80;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.Edit.Name = "Edit";
            this.Edit.Width = 40;
            // 
            // cbBranches
            // 
            this.cbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranches.FormattingEnabled = true;
            this.cbBranches.Location = new System.Drawing.Point(523, 17);
            this.cbBranches.Name = "cbBranches";
            this.cbBranches.Size = new System.Drawing.Size(406, 24);
            this.cbBranches.TabIndex = 8;
            this.cbBranches.SelectedIndexChanged += new System.EventHandler(this.cbBranches_SelectedIndexChanged);
            // 
            // cbCompany
            // 
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Items.AddRange(new object[] {
            "                     Select"});
            this.cbCompany.Location = new System.Drawing.Point(89, 16);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(347, 24);
            this.cbCompany.TabIndex = 6;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(462, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Branch";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(14, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "Company";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(302, 421);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 45);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(192, 13);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 2;
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
            this.btnCancel.Location = new System.Drawing.Point(113, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Clea&r";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // GCGLAdvances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(989, 476);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GCGLAdvances";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order Book";
            this.Load += new System.EventHandler(this.GCGLAdvances_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvAdvancesDetl)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbFinYear;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.DataGridView gvAdvancesDetl;
        private System.Windows.Forms.ComboBox cbBranches;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtEcodeSearch;
        private System.Windows.Forms.ComboBox cbEcode;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbDocMonths;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocMonth;
        private System.Windows.Forms.DataGridViewTextBoxColumn CampName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SRName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
    }
}