namespace SSCRM
{
    partial class MonthlyAttdProcess
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtWagePeriod = new System.Windows.Forms.TextBox();
            this.cbPayRollType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gvEmpDetails = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ecode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Names = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desig = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Paid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HpComName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HpBrName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HpDeptCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HpdeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HpEname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HpDesigID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HpDesg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HpDOJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.PowderBlue;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(4, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 15);
            this.label5.TabIndex = 113;
            this.label5.Text = "Wage Period";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.txtWagePeriod);
            this.groupBox1.Controls.Add(this.cbPayRollType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.gvEmpDetails);
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(779, 554);
            this.groupBox1.TabIndex = 115;
            this.groupBox1.TabStop = false;
            // 
            // txtWagePeriod
            // 
            this.txtWagePeriod.BackColor = System.Drawing.SystemColors.Info;
            this.txtWagePeriod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWagePeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWagePeriod.Location = new System.Drawing.Point(97, 24);
            this.txtWagePeriod.Name = "txtWagePeriod";
            this.txtWagePeriod.ReadOnly = true;
            this.txtWagePeriod.Size = new System.Drawing.Size(117, 22);
            this.txtWagePeriod.TabIndex = 146;
            // 
            // cbPayRollType
            // 
            this.cbPayRollType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPayRollType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPayRollType.FormattingEnabled = true;
            this.cbPayRollType.Items.AddRange(new object[] {
            "OFFICE",
            "TM2DDSM",
            "SR2GC"});
            this.cbPayRollType.Location = new System.Drawing.Point(517, 25);
            this.cbPayRollType.Name = "cbPayRollType";
            this.cbPayRollType.Size = new System.Drawing.Size(255, 24);
            this.cbPayRollType.TabIndex = 145;
            this.cbPayRollType.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.PowderBlue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(417, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 15);
            this.label1.TabIndex = 144;
            this.label1.Text = "Pay Roll Type";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(237, 503);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(312, 45);
            this.groupBox4.TabIndex = 127;
            this.groupBox4.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(194, 13);
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
            this.btnCancel.Location = new System.Drawing.Point(119, 13);
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
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(44, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gvEmpDetails
            // 
            this.gvEmpDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle33.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle33.ForeColor = System.Drawing.Color.Black;
            this.gvEmpDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle33;
            this.gvEmpDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvEmpDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle34.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle34.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle34.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle34.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvEmpDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle34;
            this.gvEmpDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEmpDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.Ecode,
            this.Names,
            this.Dept,
            this.DeptCode,
            this.Desig,
            this.Total,
            this.Paid,
            this.LOP,
            this.HpComName,
            this.HpBrName,
            this.HpDeptCode,
            this.HpdeptName,
            this.HpEname,
            this.HpDesigID,
            this.HpDesg,
            this.HpDOJ});
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle35.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle35.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle35.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle35.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvEmpDetails.DefaultCellStyle = dataGridViewCellStyle35;
            this.gvEmpDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvEmpDetails.Location = new System.Drawing.Point(3, 60);
            this.gvEmpDetails.MultiSelect = false;
            this.gvEmpDetails.Name = "gvEmpDetails";
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle36.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle36.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle36.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle36.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle36.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle36.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvEmpDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle36;
            this.gvEmpDetails.RowHeadersVisible = false;
            this.gvEmpDetails.Size = new System.Drawing.Size(772, 437);
            this.gvEmpDetails.TabIndex = 143;
            this.gvEmpDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvEmpDetails_CellEndEdit);
            this.gvEmpDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvEmpDetails_EditingControlShowing);
            // 
            // SLNO
            // 
            this.SLNO.Frozen = true;
            this.SLNO.HeaderText = "SlNo";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SLNO.Width = 40;
            // 
            // Ecode
            // 
            this.Ecode.Frozen = true;
            this.Ecode.HeaderText = "Ecode";
            this.Ecode.Name = "Ecode";
            this.Ecode.ReadOnly = true;
            this.Ecode.Width = 70;
            // 
            // Names
            // 
            this.Names.Frozen = true;
            this.Names.HeaderText = "Name";
            this.Names.MinimumWidth = 20;
            this.Names.Name = "Names";
            this.Names.ReadOnly = true;
            this.Names.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Names.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Names.Width = 160;
            // 
            // Dept
            // 
            this.Dept.Frozen = true;
            this.Dept.HeaderText = "Dept";
            this.Dept.Name = "Dept";
            this.Dept.ReadOnly = true;
            this.Dept.Width = 150;
            // 
            // DeptCode
            // 
            this.DeptCode.Frozen = true;
            this.DeptCode.HeaderText = "DeptCode";
            this.DeptCode.Name = "DeptCode";
            this.DeptCode.Visible = false;
            // 
            // Desig
            // 
            this.Desig.HeaderText = "Desig";
            this.Desig.Name = "Desig";
            this.Desig.ReadOnly = true;
            this.Desig.Width = 140;
            // 
            // Total
            // 
            this.Total.HeaderText = "Total Days";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.Width = 60;
            // 
            // Paid
            // 
            this.Paid.HeaderText = "Paid Days";
            this.Paid.Name = "Paid";
            this.Paid.ReadOnly = true;
            this.Paid.Width = 60;
            // 
            // LOP
            // 
            this.LOP.HeaderText = "LOP Days";
            this.LOP.Name = "LOP";
            this.LOP.Width = 60;
            // 
            // HpComName
            // 
            this.HpComName.HeaderText = "HpComName";
            this.HpComName.Name = "HpComName";
            this.HpComName.Visible = false;
            // 
            // HpBrName
            // 
            this.HpBrName.HeaderText = "HpBrName";
            this.HpBrName.Name = "HpBrName";
            this.HpBrName.Visible = false;
            // 
            // HpDeptCode
            // 
            this.HpDeptCode.HeaderText = "HpDeptCode";
            this.HpDeptCode.Name = "HpDeptCode";
            this.HpDeptCode.Visible = false;
            // 
            // HpdeptName
            // 
            this.HpdeptName.HeaderText = "HpDeptName";
            this.HpdeptName.Name = "HpdeptName";
            this.HpdeptName.Visible = false;
            // 
            // HpEname
            // 
            this.HpEname.HeaderText = "HpEname";
            this.HpEname.Name = "HpEname";
            this.HpEname.Visible = false;
            // 
            // HpDesigID
            // 
            this.HpDesigID.HeaderText = "HpDesigID";
            this.HpDesigID.Name = "HpDesigID";
            this.HpDesigID.Visible = false;
            // 
            // HpDesg
            // 
            this.HpDesg.HeaderText = "HpDesg";
            this.HpDesg.Name = "HpDesg";
            this.HpDesg.Visible = false;
            // 
            // HpDOJ
            // 
            this.HpDOJ.HeaderText = "HpDOJ";
            this.HpDOJ.Name = "HpDOJ";
            this.HpDOJ.Visible = false;
            // 
            // MonthlyAttdProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.groupBox1);
            this.Name = "MonthlyAttdProcess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MonthlyPayRoll";
            this.Load += new System.EventHandler(this.MonthlyPayRoll_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.DataGridView gvEmpDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPayRollType;
        private System.Windows.Forms.TextBox txtWagePeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ecode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Names;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dept;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desig;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Paid;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOP;
        private System.Windows.Forms.DataGridViewTextBoxColumn HpComName;
        private System.Windows.Forms.DataGridViewTextBoxColumn HpBrName;
        private System.Windows.Forms.DataGridViewTextBoxColumn HpDeptCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn HpdeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn HpEname;
        private System.Windows.Forms.DataGridViewTextBoxColumn HpDesigID;
        private System.Windows.Forms.DataGridViewTextBoxColumn HpDesg;
        private System.Windows.Forms.DataGridViewTextBoxColumn HpDOJ;
    }
}