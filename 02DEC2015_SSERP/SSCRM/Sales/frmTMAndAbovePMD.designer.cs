namespace SSCRM
{
    partial class frmTMAndAbovePMD
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
            this.dtpDocmonth = new System.Windows.Forms.DateTimePicker();
            this.gvTMDetails = new System.Windows.Forms.DataGridView();
            this.cbBranch = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbComp = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ecode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Designation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Groups = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PMD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DAdays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Demos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StateCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FinYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FlgStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTMDetails)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.dtpDocmonth);
            this.groupBox1.Controls.Add(this.gvTMDetails);
            this.groupBox1.Controls.Add(this.cbBranch);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbComp);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(708, 433);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // dtpDocmonth
            // 
            this.dtpDocmonth.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDocmonth.CustomFormat = "MMM/yyyy";
            this.dtpDocmonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDocmonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDocmonth.Location = new System.Drawing.Point(564, 47);
            this.dtpDocmonth.Name = "dtpDocmonth";
            this.dtpDocmonth.Size = new System.Drawing.Size(92, 22);
            this.dtpDocmonth.TabIndex = 132;
            this.dtpDocmonth.Value = new System.DateTime(2015, 4, 1, 0, 0, 0, 0);
            this.dtpDocmonth.ValueChanged += new System.EventHandler(this.dtpDocmonth_ValueChanged);
            // 
            // gvTMDetails
            // 
            this.gvTMDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvTMDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvTMDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvTMDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvTMDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvTMDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTMDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.Ecode,
            this.EmpName,
            this.Designation,
            this.Groups,
            this.PMD,
            this.DAdays,
            this.Demos,
            this.GroupName,
            this.StateCode,
            this.FinYear,
            this.FlgStatus});
            this.gvTMDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvTMDetails.Location = new System.Drawing.Point(9, 78);
            this.gvTMDetails.MultiSelect = false;
            this.gvTMDetails.Name = "gvTMDetails";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvTMDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvTMDetails.RowHeadersVisible = false;
            this.gvTMDetails.Size = new System.Drawing.Size(691, 300);
            this.gvTMDetails.TabIndex = 119;
            this.gvTMDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvTMDetails_CellEndEdit);
            this.gvTMDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvProductDetails_EditingControlShowing);
            // 
            // cbBranch
            // 
            this.cbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranch.FormattingEnabled = true;
            this.cbBranch.Location = new System.Drawing.Point(90, 44);
            this.cbBranch.Name = "cbBranch";
            this.cbBranch.Size = new System.Drawing.Size(332, 24);
            this.cbBranch.TabIndex = 117;
            this.cbBranch.SelectedIndexChanged += new System.EventHandler(this.cbBranch_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(21, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 16);
            this.label7.TabIndex = 118;
            this.label7.Text = "Branch";
            // 
            // cbComp
            // 
            this.cbComp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbComp.FormattingEnabled = true;
            this.cbComp.Location = new System.Drawing.Point(90, 14);
            this.cbComp.Name = "cbComp";
            this.cbComp.Size = new System.Drawing.Size(332, 24);
            this.cbComp.TabIndex = 113;
            this.cbComp.SelectedIndexChanged += new System.EventHandler(this.cbComp_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label10.Location = new System.Drawing.Point(6, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 16);
            this.label10.TabIndex = 114;
            this.label10.Text = "Company";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(486, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 15);
            this.label6.TabIndex = 29;
            this.label6.Text = "Doc Month";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnDelete);
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Location = new System.Drawing.Point(129, 383);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 45);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.Location = new System.Drawing.Point(271, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(74, 26);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "D&elete";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(191, 13);
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
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.SLNO.Width = 50;
            // 
            // Ecode
            // 
            this.Ecode.Frozen = true;
            this.Ecode.HeaderText = "Ecode";
            this.Ecode.Name = "Ecode";
            this.Ecode.ReadOnly = true;
            this.Ecode.Width = 60;
            // 
            // EmpName
            // 
            this.EmpName.Frozen = true;
            this.EmpName.HeaderText = "Emp Name";
            this.EmpName.MinimumWidth = 20;
            this.EmpName.Name = "EmpName";
            this.EmpName.ReadOnly = true;
            this.EmpName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.EmpName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.EmpName.Width = 160;
            // 
            // Designation
            // 
            this.Designation.Frozen = true;
            this.Designation.HeaderText = "Designation";
            this.Designation.Name = "Designation";
            this.Designation.ReadOnly = true;
            this.Designation.Width = 150;
            // 
            // Groups
            // 
            this.Groups.Frozen = true;
            this.Groups.HeaderText = "Groups";
            this.Groups.Name = "Groups";
            this.Groups.ReadOnly = true;
            this.Groups.Width = 75;
            // 
            // PMD
            // 
            this.PMD.Frozen = true;
            this.PMD.HeaderText = "PMD";
            this.PMD.Name = "PMD";
            this.PMD.Width = 50;
            // 
            // DAdays
            // 
            this.DAdays.Frozen = true;
            this.DAdays.HeaderText = "DAdays";
            this.DAdays.Name = "DAdays";
            this.DAdays.Width = 65;
            // 
            // Demos
            // 
            this.Demos.Frozen = true;
            this.Demos.HeaderText = "Demos";
            this.Demos.Name = "Demos";
            this.Demos.Width = 70;
            // 
            // GroupName
            // 
            this.GroupName.Frozen = true;
            this.GroupName.HeaderText = "GroupName";
            this.GroupName.Name = "GroupName";
            this.GroupName.Visible = false;
            // 
            // StateCode
            // 
            this.StateCode.Frozen = true;
            this.StateCode.HeaderText = "StateCode";
            this.StateCode.Name = "StateCode";
            this.StateCode.Visible = false;
            // 
            // FinYear
            // 
            this.FinYear.Frozen = true;
            this.FinYear.HeaderText = "FinYear";
            this.FinYear.Name = "FinYear";
            this.FinYear.Visible = false;
            // 
            // FlgStatus
            // 
            this.FlgStatus.Frozen = true;
            this.FlgStatus.HeaderText = "FlgStatus";
            this.FlgStatus.Name = "FlgStatus";
            this.FlgStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FlgStatus.Visible = false;
            // 
            // frmTMAndAbovePMD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(712, 436);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "frmTMAndAbovePMD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TM&Above(PMD)";
            this.Load += new System.EventHandler(this.frmTMAndAbovePMD_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTMDetails)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbComp;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbBranch;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.DataGridView gvTMDetails;
        private System.Windows.Forms.DateTimePicker dtpDocmonth;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ecode;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Designation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Groups;
        private System.Windows.Forms.DataGridViewTextBoxColumn PMD;
        private System.Windows.Forms.DataGridViewTextBoxColumn DAdays;
        private System.Windows.Forms.DataGridViewTextBoxColumn Demos;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StateCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn FinYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn FlgStatus;
    }
}