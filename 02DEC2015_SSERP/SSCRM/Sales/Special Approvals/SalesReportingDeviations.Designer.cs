namespace SSCRM
{
    partial class SalesReportingDeviations
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtEcodeSearch = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtEmpName = new System.Windows.Forms.TextBox();
            this.txtDesig = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbFinYear = new System.Windows.Forms.ComboBox();
            this.cbDocMonth = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGroups = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDevType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gvProductDetails = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocMonth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ECode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Groups = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Deviation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Del = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductDetails)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.gvProductDetails);
            this.groupBox2.Controls.Add(this.txtRemarks);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cbDevType);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtGroups);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbDocMonth);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbFinYear);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtDesig);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtEmpName);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.txtEcodeSearch);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(2, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(679, 457);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // txtEcodeSearch
            // 
            this.txtEcodeSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcodeSearch.Location = new System.Drawing.Point(153, 63);
            this.txtEcodeSearch.MaxLength = 20;
            this.txtEcodeSearch.Name = "txtEcodeSearch";
            this.txtEcodeSearch.Size = new System.Drawing.Size(89, 21);
            this.txtEcodeSearch.TabIndex = 2;
            this.txtEcodeSearch.Validated += new System.EventHandler(this.txtEcodeSearch_Validated);
            this.txtEcodeSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEcodeSearch_KeyUp);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label16.Location = new System.Drawing.Point(102, 65);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 15);
            this.label16.TabIndex = 13;
            this.label16.Text = "Ecode";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Azure;
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label23.Location = new System.Drawing.Point(250, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(179, 22);
            this.label23.TabIndex = 63;
            this.label23.Text = "Reporting Deviations";
            // 
            // txtEmpName
            // 
            this.txtEmpName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpName.Location = new System.Drawing.Point(244, 63);
            this.txtEmpName.MaxLength = 50;
            this.txtEmpName.Name = "txtEmpName";
            this.txtEmpName.ReadOnly = true;
            this.txtEmpName.Size = new System.Drawing.Size(313, 21);
            this.txtEmpName.TabIndex = 64;
            // 
            // txtDesig
            // 
            this.txtDesig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesig.Location = new System.Drawing.Point(154, 87);
            this.txtDesig.MaxLength = 50;
            this.txtDesig.Name = "txtDesig";
            this.txtDesig.ReadOnly = true;
            this.txtDesig.Size = new System.Drawing.Size(187, 21);
            this.txtDesig.TabIndex = 66;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(58, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 16);
            this.label1.TabIndex = 67;
            this.label1.Text = "Designation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(84, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 16);
            this.label2.TabIndex = 69;
            this.label2.Text = "FinYear";
            // 
            // cbFinYear
            // 
            this.cbFinYear.AllowDrop = true;
            this.cbFinYear.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbFinYear.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFinYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFinYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFinYear.FormattingEnabled = true;
            this.cbFinYear.Location = new System.Drawing.Point(152, 38);
            this.cbFinYear.Name = "cbFinYear";
            this.cbFinYear.Size = new System.Drawing.Size(187, 23);
            this.cbFinYear.TabIndex = 0;
            this.cbFinYear.SelectedIndexChanged += new System.EventHandler(this.cbFinYear_SelectedIndexChanged);
            // 
            // cbDocMonth
            // 
            this.cbDocMonth.AllowDrop = true;
            this.cbDocMonth.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbDocMonth.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDocMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDocMonth.FormattingEnabled = true;
            this.cbDocMonth.Location = new System.Drawing.Point(445, 38);
            this.cbDocMonth.Name = "cbDocMonth";
            this.cbDocMonth.Size = new System.Drawing.Size(112, 23);
            this.cbDocMonth.TabIndex = 1;
            this.cbDocMonth.SelectedIndexChanged += new System.EventHandler(this.cbDocMonth_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(366, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 71;
            this.label3.Text = "DocMonth";
            // 
            // txtGroups
            // 
            this.txtGroups.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGroups.Location = new System.Drawing.Point(447, 89);
            this.txtGroups.MaxLength = 50;
            this.txtGroups.Name = "txtGroups";
            this.txtGroups.ReadOnly = true;
            this.txtGroups.Size = new System.Drawing.Size(74, 21);
            this.txtGroups.TabIndex = 73;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(385, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 16);
            this.label4.TabIndex = 74;
            this.label4.Text = "Groups";
            // 
            // cbDevType
            // 
            this.cbDevType.AllowDrop = true;
            this.cbDevType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbDevType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDevType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDevType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDevType.FormattingEnabled = true;
            this.cbDevType.Location = new System.Drawing.Point(154, 112);
            this.cbDevType.Name = "cbDevType";
            this.cbDevType.Size = new System.Drawing.Size(187, 23);
            this.cbDevType.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(35, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 16);
            this.label5.TabIndex = 75;
            this.label5.Text = "Deviation Type";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(153, 137);
            this.txtRemarks.MaxLength = 50;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(490, 53);
            this.txtRemarks.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(78, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 16);
            this.label6.TabIndex = 78;
            this.label6.Text = "Remarks";
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvProductDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvProductDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProductDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.DocMonth,
            this.ECode,
            this.EmpName,
            this.Groups,
            this.Deviation,
            this.Remarks,
            this.Del});
            this.gvProductDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvProductDetails.Location = new System.Drawing.Point(6, 241);
            this.gvProductDetails.MultiSelect = false;
            this.gvProductDetails.Name = "gvProductDetails";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvProductDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvProductDetails.RowHeadersVisible = false;
            this.gvProductDetails.Size = new System.Drawing.Size(667, 211);
            this.gvProductDetails.TabIndex = 79;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnDelete);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Location = new System.Drawing.Point(150, 191);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 45);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(276, 13);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDelete.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.Location = new System.Drawing.Point(187, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(74, 26);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(111, 13);
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
            this.btnSave.Location = new System.Drawing.Point(36, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            // DocMonth
            // 
            this.DocMonth.HeaderText = "DocMonth";
            this.DocMonth.Name = "DocMonth";
            this.DocMonth.ReadOnly = true;
            this.DocMonth.Width = 90;
            // 
            // ECode
            // 
            this.ECode.HeaderText = "ECode";
            this.ECode.Name = "ECode";
            this.ECode.ReadOnly = true;
            this.ECode.Width = 80;
            // 
            // EmpName
            // 
            this.EmpName.HeaderText = "EmpName";
            this.EmpName.Name = "EmpName";
            this.EmpName.ReadOnly = true;
            this.EmpName.Width = 200;
            // 
            // Groups
            // 
            this.Groups.HeaderText = "Groups";
            this.Groups.Name = "Groups";
            this.Groups.ReadOnly = true;
            this.Groups.Visible = false;
            this.Groups.Width = 60;
            // 
            // Deviation
            // 
            this.Deviation.HeaderText = "Deviation";
            this.Deviation.Name = "Deviation";
            this.Deviation.ReadOnly = true;
            this.Deviation.Width = 130;
            // 
            // Remarks
            // 
            this.Remarks.HeaderText = "Remarks";
            this.Remarks.Name = "Remarks";
            this.Remarks.ReadOnly = true;
            // 
            // Del
            // 
            this.Del.HeaderText = "";
            this.Del.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.Del.Name = "Del";
            this.Del.ReadOnly = true;
            this.Del.Width = 30;
            // 
            // SalesReportingDeviations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SalesReportingDeviations";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Reporting Deviations";
            this.Load += new System.EventHandler(this.SalesReportingDeviations_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductDetails)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtEcodeSearch;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtEmpName;
        private System.Windows.Forms.TextBox txtDesig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbFinYear;
        private System.Windows.Forms.ComboBox cbDocMonth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGroups;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbDevType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.DataGridView gvProductDetails;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocMonth;
        private System.Windows.Forms.DataGridViewTextBoxColumn ECode;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Groups;
        private System.Windows.Forms.DataGridViewTextBoxColumn Deviation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private System.Windows.Forms.DataGridViewImageColumn Del;
    }
}