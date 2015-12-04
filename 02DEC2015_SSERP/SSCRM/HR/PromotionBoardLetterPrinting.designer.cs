namespace SSCRM
{
    partial class PromotionBoardLetterPrinting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDisplayPromotionDetails = new System.Windows.Forms.Button();
            this.cbFilterType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEName = new System.Windows.Forms.TextBox();
            this.txtEcodeSearch = new System.Windows.Forms.TextBox();
            this.lblEcode = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.gvEmpPromotionDetails = new System.Windows.Forms.DataGridView();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.dtpFrmDate = new System.Windows.Forms.DateTimePicker();
            this.cbBranches = new System.Windows.Forms.ComboBox();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PromotionCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApplNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ecode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LetterType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LetterRefNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Print = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpPromotionDetails)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.btnDisplayPromotionDetails);
            this.groupBox1.Controls.Add(this.cbFilterType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtEName);
            this.groupBox1.Controls.Add(this.txtEcodeSearch);
            this.groupBox1.Controls.Add(this.lblEcode);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.gvEmpPromotionDetails);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label33);
            this.groupBox1.Controls.Add(this.dtpFrmDate);
            this.groupBox1.Controls.Add(this.cbBranches);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(925, 546);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnDisplayPromotionDetails
            // 
            this.btnDisplayPromotionDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDisplayPromotionDetails.BackColor = System.Drawing.Color.Green;
            this.btnDisplayPromotionDetails.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnDisplayPromotionDetails.FlatAppearance.BorderSize = 5;
            this.btnDisplayPromotionDetails.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDisplayPromotionDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplayPromotionDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDisplayPromotionDetails.Location = new System.Drawing.Point(845, 99);
            this.btnDisplayPromotionDetails.Margin = new System.Windows.Forms.Padding(1);
            this.btnDisplayPromotionDetails.Name = "btnDisplayPromotionDetails";
            this.btnDisplayPromotionDetails.Size = new System.Drawing.Size(72, 24);
            this.btnDisplayPromotionDetails.TabIndex = 146;
            this.btnDisplayPromotionDetails.Tag = "";
            this.btnDisplayPromotionDetails.Text = "Display";
            this.btnDisplayPromotionDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDisplayPromotionDetails.UseVisualStyleBackColor = false;
            this.btnDisplayPromotionDetails.Click += new System.EventHandler(this.btnDisplayPromotionDetails_Click);
            // 
            // cbFilterType
            // 
            this.cbFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterType.FormattingEnabled = true;
            this.cbFilterType.Items.AddRange(new object[] {
            "--Select--       ",
            "BETWEEN DATES",
            "ECODE"});
            this.cbFilterType.Location = new System.Drawing.Point(79, 18);
            this.cbFilterType.Name = "cbFilterType";
            this.cbFilterType.Size = new System.Drawing.Size(159, 24);
            this.cbFilterType.TabIndex = 145;
            this.cbFilterType.SelectedIndexChanged += new System.EventHandler(this.cbFilterType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(11, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 144;
            this.label1.Text = "Filter By";
            // 
            // txtEName
            // 
            this.txtEName.BackColor = System.Drawing.SystemColors.Info;
            this.txtEName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEName.Location = new System.Drawing.Point(549, 18);
            this.txtEName.MaxLength = 20;
            this.txtEName.Name = "txtEName";
            this.txtEName.ReadOnly = true;
            this.txtEName.Size = new System.Drawing.Size(271, 22);
            this.txtEName.TabIndex = 142;
            this.txtEName.TabStop = false;
            // 
            // txtEcodeSearch
            // 
            this.txtEcodeSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcodeSearch.Location = new System.Drawing.Point(472, 18);
            this.txtEcodeSearch.MaxLength = 20;
            this.txtEcodeSearch.Name = "txtEcodeSearch";
            this.txtEcodeSearch.Size = new System.Drawing.Size(76, 22);
            this.txtEcodeSearch.TabIndex = 143;
            this.txtEcodeSearch.TextChanged += new System.EventHandler(this.txtEcodeSearch_TextChanged);
            // 
            // lblEcode
            // 
            this.lblEcode.AutoSize = true;
            this.lblEcode.BackColor = System.Drawing.Color.PowderBlue;
            this.lblEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEcode.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblEcode.Location = new System.Drawing.Point(419, 21);
            this.lblEcode.Name = "lblEcode";
            this.lblEcode.Size = new System.Drawing.Size(53, 16);
            this.lblEcode.TabIndex = 141;
            this.lblEcode.Text = "Ecode";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Navy;
            this.label7.Location = new System.Drawing.Point(7, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(211, 17);
            this.label7.TabIndex = 140;
            this.label7.Text = "Employee Promotion Details";
            // 
            // gvEmpPromotionDetails
            // 
            this.gvEmpPromotionDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvEmpPromotionDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvEmpPromotionDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvEmpPromotionDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvEmpPromotionDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvEmpPromotionDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEmpPromotionDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.PromotionCode,
            this.ApplNo,
            this.Ecode,
            this.EmpName,
            this.DeptName,
            this.LetterType,
            this.LetterRefNo,
            this.Print});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvEmpPromotionDetails.DefaultCellStyle = dataGridViewCellStyle4;
            this.gvEmpPromotionDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvEmpPromotionDetails.Location = new System.Drawing.Point(6, 124);
            this.gvEmpPromotionDetails.MultiSelect = false;
            this.gvEmpPromotionDetails.Name = "gvEmpPromotionDetails";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvEmpPromotionDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gvEmpPromotionDetails.RowHeadersVisible = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Navy;
            this.gvEmpPromotionDetails.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.gvEmpPromotionDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvEmpPromotionDetails.Size = new System.Drawing.Size(912, 372);
            this.gvEmpPromotionDetails.TabIndex = 138;
            this.gvEmpPromotionDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvEmpPromotionDetails_CellClick);
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(475, 74);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(102, 22);
            this.dtpToDate.TabIndex = 137;
            this.dtpToDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(445, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 16);
            this.label2.TabIndex = 136;
            this.label2.Text = "To";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label33.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label33.Location = new System.Drawing.Point(29, 76);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(47, 16);
            this.label33.TabIndex = 135;
            this.label33.Text = "From ";
            // 
            // dtpFrmDate
            // 
            this.dtpFrmDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFrmDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrmDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrmDate.Location = new System.Drawing.Point(78, 73);
            this.dtpFrmDate.Name = "dtpFrmDate";
            this.dtpFrmDate.Size = new System.Drawing.Size(102, 22);
            this.dtpFrmDate.TabIndex = 134;
            this.dtpFrmDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // cbBranches
            // 
            this.cbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranches.FormattingEnabled = true;
            this.cbBranches.Location = new System.Drawing.Point(472, 45);
            this.cbBranches.Name = "cbBranches";
            this.cbBranches.Size = new System.Drawing.Size(348, 24);
            this.cbBranches.TabIndex = 131;
            // 
            // cbCompany
            // 
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Items.AddRange(new object[] {
            "                     Select"});
            this.cbCompany.Location = new System.Drawing.Point(78, 45);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(329, 24);
            this.cbCompany.TabIndex = 129;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(413, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 130;
            this.label5.Text = "Branch";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(3, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 17);
            this.label6.TabIndex = 128;
            this.label6.Text = "Company";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Location = new System.Drawing.Point(287, 495);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 46);
            this.groupBox3.TabIndex = 160;
            this.groupBox3.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(136, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 30);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SLNO
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.SLNO.DefaultCellStyle = dataGridViewCellStyle3;
            this.SLNO.Frozen = true;
            this.SLNO.HeaderText = "Sl.No";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SLNO.Width = 50;
            // 
            // PromotionCode
            // 
            this.PromotionCode.HeaderText = "PromotionCode";
            this.PromotionCode.Name = "PromotionCode";
            this.PromotionCode.ReadOnly = true;
            this.PromotionCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PromotionCode.Visible = false;
            // 
            // ApplNo
            // 
            this.ApplNo.Frozen = true;
            this.ApplNo.HeaderText = "Appl No";
            this.ApplNo.Name = "ApplNo";
            this.ApplNo.ReadOnly = true;
            this.ApplNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ApplNo.Width = 90;
            // 
            // Ecode
            // 
            this.Ecode.HeaderText = "Ecode";
            this.Ecode.Name = "Ecode";
            this.Ecode.ReadOnly = true;
            this.Ecode.Width = 80;
            // 
            // EmpName
            // 
            this.EmpName.HeaderText = "Name";
            this.EmpName.Name = "EmpName";
            this.EmpName.ReadOnly = true;
            this.EmpName.Width = 190;
            // 
            // DeptName
            // 
            this.DeptName.HeaderText = "Dept Name";
            this.DeptName.Name = "DeptName";
            this.DeptName.ReadOnly = true;
            this.DeptName.Width = 140;
            // 
            // LetterType
            // 
            this.LetterType.HeaderText = "Letter Type";
            this.LetterType.Name = "LetterType";
            this.LetterType.ReadOnly = true;
            this.LetterType.Width = 130;
            // 
            // LetterRefNo
            // 
            this.LetterRefNo.HeaderText = "Letter Ref No";
            this.LetterRefNo.Name = "LetterRefNo";
            this.LetterRefNo.ReadOnly = true;
            this.LetterRefNo.Width = 160;
            // 
            // Print
            // 
            this.Print.HeaderText = "Print";
            this.Print.Image = global::SSCRM.Properties.Resources.print_icon;
            this.Print.Name = "Print";
            this.Print.Width = 60;
            // 
            // PromotionBoardLetterPrinting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(930, 550);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PromotionBoardLetterPrinting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Promotion Board Letter Printing";
            this.Load += new System.EventHandler(this.PromotionBoardLetterPrinting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpPromotionDetails)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.DateTimePicker dtpFrmDate;
        private System.Windows.Forms.ComboBox cbBranches;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.DataGridView gvEmpPromotionDetails;
        private System.Windows.Forms.TextBox txtEName;
        private System.Windows.Forms.TextBox txtEcodeSearch;
        private System.Windows.Forms.Label lblEcode;
        private System.Windows.Forms.ComboBox cbFilterType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDisplayPromotionDetails;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PromotionCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApplNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ecode;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LetterType;
        private System.Windows.Forms.DataGridViewTextBoxColumn LetterRefNo;
        private System.Windows.Forms.DataGridViewImageColumn Print;
    }
}