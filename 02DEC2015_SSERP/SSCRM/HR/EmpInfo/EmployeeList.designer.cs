namespace SSCRM
{
    partial class EmployeeList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.gvEmployeeList = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ECode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Doj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desig = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalStruct = new System.Windows.Forms.DataGridViewImageColumn();
            this.dtpMonth = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.cbDepatment = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbBranchType = new System.Windows.Forms.ComboBox();
            this.CbBrnype = new System.Windows.Forms.Label();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.label68 = new System.Windows.Forms.Label();
            this.cbBranches = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmployeeList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.CausesValidation = false;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.btnDisplay);
            this.groupBox1.Controls.Add(this.gvEmployeeList);
            this.groupBox1.Controls.Add(this.dtpMonth);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.cbDepatment);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbBranchType);
            this.groupBox1.Controls.Add(this.CbBrnype);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.label68);
            this.groupBox1.Controls.Add(this.cbBranches);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(4, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(732, 510);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(7, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Employee Details";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.Navy;
            this.label31.Location = new System.Drawing.Point(291, 10);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(150, 22);
            this.label31.TabIndex = 0;
            this.label31.Text = "EMPLOYEE LIST";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnClose);
            this.groupBox5.Controls.Add(this.btnCancel);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox5.Location = new System.Drawing.Point(250, 461);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(226, 42);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(116, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 26);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "C&lose";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(37, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 26);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Clea&r";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDisplay
            // 
            this.btnDisplay.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDisplay.BackColor = System.Drawing.Color.YellowGreen;
            this.btnDisplay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnDisplay.FlatAppearance.BorderSize = 5;
            this.btnDisplay.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplay.Location = new System.Drawing.Point(587, 117);
            this.btnDisplay.Margin = new System.Windows.Forms.Padding(1);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(136, 22);
            this.btnDisplay.TabIndex = 6;
            this.btnDisplay.Tag = "Village Search";
            this.btnDisplay.Text = "Display Emp List";
            this.btnDisplay.UseVisualStyleBackColor = false;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // gvEmployeeList
            // 
            this.gvEmployeeList.AllowUserToAddRows = false;
            dataGridViewCellStyle31.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle31.ForeColor = System.Drawing.Color.Black;
            this.gvEmployeeList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle31;
            this.gvEmployeeList.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvEmployeeList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle32.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle32.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle32.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle32.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvEmployeeList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle32;
            this.gvEmployeeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEmployeeList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.ECode,
            this.EName,
            this.Doj,
            this.Dept,
            this.Desig,
            this.SalStruct});
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle34.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle34.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle34.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle34.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvEmployeeList.DefaultCellStyle = dataGridViewCellStyle34;
            this.gvEmployeeList.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvEmployeeList.Location = new System.Drawing.Point(3, 141);
            this.gvEmployeeList.MultiSelect = false;
            this.gvEmployeeList.Name = "gvEmployeeList";
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle35.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle35.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle35.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle35.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvEmployeeList.RowHeadersDefaultCellStyle = dataGridViewCellStyle35;
            this.gvEmployeeList.RowHeadersVisible = false;
            this.gvEmployeeList.Size = new System.Drawing.Size(723, 317);
            this.gvEmployeeList.TabIndex = 13;
            this.gvEmployeeList.TabStop = false;
            // 
            // SLNO
            // 
            this.SLNO.Frozen = true;
            this.SLNO.HeaderText = "SlNo";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SLNO.Width = 45;
            // 
            // ECode
            // 
            this.ECode.Frozen = true;
            this.ECode.HeaderText = "ECode";
            this.ECode.Name = "ECode";
            this.ECode.ReadOnly = true;
            this.ECode.Width = 60;
            // 
            // EName
            // 
            this.EName.Frozen = true;
            this.EName.HeaderText = "EmpName";
            this.EName.MinimumWidth = 20;
            this.EName.Name = "EName";
            this.EName.ReadOnly = true;
            this.EName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.EName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.EName.Width = 160;
            // 
            // Doj
            // 
            this.Doj.Frozen = true;
            this.Doj.HeaderText = "Date of Join";
            this.Doj.Name = "Doj";
            this.Doj.ReadOnly = true;
            // 
            // Dept
            // 
            this.Dept.Frozen = true;
            this.Dept.HeaderText = "Department";
            this.Dept.Name = "Dept";
            this.Dept.Width = 120;
            // 
            // Desig
            // 
            dataGridViewCellStyle33.Format = "0.00";
            dataGridViewCellStyle33.NullValue = null;
            this.Desig.DefaultCellStyle = dataGridViewCellStyle33;
            this.Desig.Frozen = true;
            this.Desig.HeaderText = "Designation";
            this.Desig.Name = "Desig";
            this.Desig.Width = 150;
            // 
            // SalStruct
            // 
            this.SalStruct.Frozen = true;
            this.SalStruct.HeaderText = "Sal Structure";
            this.SalStruct.Image = global::SSCRM.Properties.Resources.tab_dashboard;
            this.SalStruct.Name = "SalStruct";
            this.SalStruct.ReadOnly = true;
            this.SalStruct.Width = 80;
            // 
            // dtpMonth
            // 
            this.dtpMonth.CustomFormat = "MMM/yyyy";
            this.dtpMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMonth.Location = new System.Drawing.Point(544, 68);
            this.dtpMonth.Name = "dtpMonth";
            this.dtpMonth.Size = new System.Drawing.Size(95, 23);
            this.dtpMonth.TabIndex = 4;
            this.dtpMonth.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpMonth.ValueChanged += new System.EventHandler(this.dtpMonth_ValueChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label16.Location = new System.Drawing.Point(490, 71);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(49, 16);
            this.label16.TabIndex = 7;
            this.label16.Text = "Month";
            // 
            // cbDepatment
            // 
            this.cbDepatment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDepatment.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbDepatment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDepatment.FormattingEnabled = true;
            this.cbDepatment.Location = new System.Drawing.Point(89, 92);
            this.cbDepatment.Name = "cbDepatment";
            this.cbDepatment.Size = new System.Drawing.Size(322, 23);
            this.cbDepatment.Sorted = true;
            this.cbDepatment.TabIndex = 5;
            this.cbDepatment.SelectedIndexChanged += new System.EventHandler(this.cbDepatment_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(3, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Department";
            // 
            // cbBranchType
            // 
            this.cbBranchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranchType.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbBranchType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranchType.FormattingEnabled = true;
            this.cbBranchType.Location = new System.Drawing.Point(544, 43);
            this.cbBranchType.Name = "cbBranchType";
            this.cbBranchType.Size = new System.Drawing.Size(179, 23);
            this.cbBranchType.Sorted = true;
            this.cbBranchType.TabIndex = 2;
            this.cbBranchType.SelectedIndexChanged += new System.EventHandler(this.cbBranchType_SelectedIndexChanged);
            // 
            // CbBrnype
            // 
            this.CbBrnype.AutoSize = true;
            this.CbBrnype.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CbBrnype.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.CbBrnype.Location = new System.Drawing.Point(444, 47);
            this.CbBrnype.Name = "CbBrnype";
            this.CbBrnype.Size = new System.Drawing.Size(96, 16);
            this.CbBrnype.TabIndex = 3;
            this.CbBrnype.Text = "Branch Type";
            // 
            // cbCompany
            // 
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Items.AddRange(new object[] {
            "                     Select"});
            this.cbCompany.Location = new System.Drawing.Point(89, 40);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(322, 23);
            this.cbCompany.TabIndex = 1;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label68.Location = new System.Drawing.Point(13, 44);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(73, 16);
            this.label68.TabIndex = 1;
            this.label68.Text = "Company";
            // 
            // cbBranches
            // 
            this.cbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranches.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranches.FormattingEnabled = true;
            this.cbBranches.Location = new System.Drawing.Point(89, 66);
            this.cbBranches.Name = "cbBranches";
            this.cbBranches.Size = new System.Drawing.Size(322, 23);
            this.cbBranches.Sorted = true;
            this.cbBranches.TabIndex = 3;
            this.cbBranches.SelectedIndexChanged += new System.EventHandler(this.cbBranches_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(30, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Branch";
            // 
            // EmployeeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(738, 515);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "EmployeeList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee List";
            this.Load += new System.EventHandler(this.EmployeeList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvEmployeeList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDisplay;
        public System.Windows.Forms.DataGridView gvEmployeeList;
        public System.Windows.Forms.DateTimePicker dtpMonth;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbDepatment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbBranchType;
        private System.Windows.Forms.Label CbBrnype;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.ComboBox cbBranches;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ECode;
        private System.Windows.Forms.DataGridViewTextBoxColumn EName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Doj;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dept;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desig;
        private System.Windows.Forms.DataGridViewImageColumn SalStruct;
    }
}