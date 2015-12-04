namespace SSCRM
{
    partial class GroupToDestination
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbBranches = new System.Windows.Forms.ComboBox();
            this.lblDocMonth = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbStates = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gvMappedGroups = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDsearch = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtlogicalBranch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbLogcalBranch = new System.Windows.Forms.ComboBox();
            this.clbDestination = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ECode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupECodeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logBranchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogicalBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvMappedGroups)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbBranches);
            this.groupBox1.Controls.Add(this.lblDocMonth);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbStates);
            this.groupBox1.Location = new System.Drawing.Point(6, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(862, 40);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(438, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 26;
            this.label7.Text = "Branch";
            // 
            // cbBranches
            // 
            this.cbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranches.FormattingEnabled = true;
            this.cbBranches.Location = new System.Drawing.Point(495, 13);
            this.cbBranches.Name = "cbBranches";
            this.cbBranches.Size = new System.Drawing.Size(361, 23);
            this.cbBranches.TabIndex = 12;
            this.cbBranches.SelectedIndexChanged += new System.EventHandler(this.cbBranches_SelectedIndexChanged);
            // 
            // lblDocMonth
            // 
            this.lblDocMonth.BackColor = System.Drawing.SystemColors.Info;
            this.lblDocMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocMonth.ForeColor = System.Drawing.Color.Maroon;
            this.lblDocMonth.Location = new System.Drawing.Point(78, 12);
            this.lblDocMonth.Name = "lblDocMonth";
            this.lblDocMonth.Size = new System.Drawing.Size(87, 23);
            this.lblDocMonth.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(2, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 15);
            this.label6.TabIndex = 23;
            this.label6.Text = "Doc Month";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(166, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 15;
            this.label1.Text = "State";
            // 
            // cbStates
            // 
            this.cbStates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStates.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStates.FormattingEnabled = true;
            this.cbStates.Location = new System.Drawing.Point(210, 12);
            this.cbStates.Name = "cbStates";
            this.cbStates.Size = new System.Drawing.Size(222, 23);
            this.cbStates.TabIndex = 11;
            this.cbStates.SelectedIndexChanged += new System.EventHandler(this.cbStates_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Location = new System.Drawing.Point(121, 546);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(634, 45);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.Location = new System.Drawing.Point(283, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(206, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(381, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(130, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.gvMappedGroups);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.clbDestination);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Location = new System.Drawing.Point(4, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(877, 596);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            // 
            // gvMappedGroups
            // 
            this.gvMappedGroups.AllowUserToAddRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.gvMappedGroups.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvMappedGroups.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvMappedGroups.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvMappedGroups.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gvMappedGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvMappedGroups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ECode,
            this.GroupName,
            this.GroupECodeName,
            this.logBranchCode,
            this.LogicalBranch});
            this.gvMappedGroups.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvMappedGroups.Location = new System.Drawing.Point(10, 58);
            this.gvMappedGroups.MultiSelect = false;
            this.gvMappedGroups.Name = "gvMappedGroups";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvMappedGroups.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gvMappedGroups.RowHeadersVisible = false;
            this.gvMappedGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvMappedGroups.Size = new System.Drawing.Size(856, 173);
            this.gvMappedGroups.TabIndex = 1;
            this.gvMappedGroups.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvMappedGroups_RowEnter);
            this.gvMappedGroups.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvMappedGroups_CellClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label9.Location = new System.Drawing.Point(4, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 15);
            this.label9.TabIndex = 63;
            this.label9.Text = "Mapped Groups";
            // 
            // txtDsearch
            // 
            this.txtDsearch.Location = new System.Drawing.Point(748, 12);
            this.txtDsearch.Name = "txtDsearch";
            this.txtDsearch.Size = new System.Drawing.Size(108, 20);
            this.txtDsearch.TabIndex = 3;
            this.txtDsearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDsearch_KeyPress);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtlogicalBranch);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtDsearch);
            this.groupBox4.Controls.Add(this.cbLogcalBranch);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(6, 227);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(856, 37);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            // 
            // txtlogicalBranch
            // 
            this.txtlogicalBranch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtlogicalBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtlogicalBranch.Location = new System.Drawing.Point(118, 10);
            this.txtlogicalBranch.Name = "txtlogicalBranch";
            this.txtlogicalBranch.Size = new System.Drawing.Size(296, 24);
            this.txtlogicalBranch.TabIndex = 66;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(6, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 15);
            this.label2.TabIndex = 20;
            this.label2.Text = "Logical Branch";
            // 
            // cbLogcalBranch
            // 
            this.cbLogcalBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLogcalBranch.Enabled = false;
            this.cbLogcalBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLogcalBranch.FormattingEnabled = true;
            this.cbLogcalBranch.Location = new System.Drawing.Point(118, 9);
            this.cbLogcalBranch.Name = "cbLogcalBranch";
            this.cbLogcalBranch.Size = new System.Drawing.Size(296, 23);
            this.cbLogcalBranch.TabIndex = 2;
            // 
            // clbDestination
            // 
            this.clbDestination.CheckOnClick = true;
            this.clbDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbDestination.FormattingEnabled = true;
            this.clbDestination.HorizontalScrollbar = true;
            this.clbDestination.Location = new System.Drawing.Point(6, 268);
            this.clbDestination.MultiColumn = true;
            this.clbDestination.Name = "clbDestination";
            this.clbDestination.Size = new System.Drawing.Size(862, 276);
            this.clbDestination.TabIndex = 4;
            this.clbDestination.ThreeDCheckBoxes = true;
            this.clbDestination.UseCompatibleTextRendering = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(654, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 15);
            this.label5.TabIndex = 19;
            this.label5.Text = "Reporting To";
            // 
            // ECode
            // 
            this.ECode.Frozen = true;
            this.ECode.HeaderText = "ECode";
            this.ECode.Name = "ECode";
            this.ECode.Visible = false;
            // 
            // GroupName
            // 
            this.GroupName.Frozen = true;
            this.GroupName.HeaderText = "Camp Name";
            this.GroupName.Name = "GroupName";
            this.GroupName.ReadOnly = true;
            this.GroupName.Width = 200;
            // 
            // GroupECodeName
            // 
            this.GroupECodeName.Frozen = true;
            this.GroupECodeName.HeaderText = "ECode Name";
            this.GroupECodeName.MinimumWidth = 20;
            this.GroupECodeName.Name = "GroupECodeName";
            this.GroupECodeName.ReadOnly = true;
            this.GroupECodeName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GroupECodeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GroupECodeName.Width = 400;
            // 
            // logBranchCode
            // 
            this.logBranchCode.HeaderText = "logBranchCode";
            this.logBranchCode.Name = "logBranchCode";
            this.logBranchCode.ReadOnly = true;
            this.logBranchCode.Visible = false;
            // 
            // LogicalBranch
            // 
            this.LogicalBranch.HeaderText = "Logical Branch";
            this.LogicalBranch.Name = "LogicalBranch";
            this.LogicalBranch.ReadOnly = true;
            this.LogicalBranch.Width = 200;
            // 
            // GroupToDestination
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(884, 605);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Name = "GroupToDestination";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mapping Groups to Destination";
            this.Load += new System.EventHandler(this.GroupToDestination_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvMappedGroups)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbStates;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckedListBox clbDestination;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDocMonth;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbLogcalBranch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbBranches;
        private System.Windows.Forms.TextBox txtDsearch;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.DataGridView gvMappedGroups;
        private System.Windows.Forms.TextBox txtlogicalBranch;
        private System.Windows.Forms.DataGridViewTextBoxColumn ECode;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupECodeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn logBranchCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogicalBranch;
    }
}