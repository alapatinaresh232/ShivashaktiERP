namespace SSCRM
{
    partial class ServiceLevelGroupToDestination
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clbDestination = new System.Windows.Forms.CheckedListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtDsearch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gvMappedGroups = new System.Windows.Forms.DataGridView();
            this.ECode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupECodeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logBranchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogicalBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.cbBranches = new System.Windows.Forms.ComboBox();
            this.lblDocMonth = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbStates = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbLogcalBranch = new System.Windows.Forms.ComboBox();
            this.txtlogicalBranch = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvMappedGroups)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.clbDestination);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.txtDsearch);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbBranches);
            this.groupBox1.Controls.Add(this.lblDocMonth);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbStates);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(649, 594);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // clbDestination
            // 
            this.clbDestination.FormattingEnabled = true;
            this.clbDestination.Location = new System.Drawing.Point(9, 301);
            this.clbDestination.Name = "clbDestination";
            this.clbDestination.Size = new System.Drawing.Size(633, 244);
            this.clbDestination.TabIndex = 73;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.btnDelete);
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Location = new System.Drawing.Point(7, 545);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(634, 45);
            this.groupBox3.TabIndex = 72;
            this.groupBox3.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.Location = new System.Drawing.Point(283, 11);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(206, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(381, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(130, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtDsearch
            // 
            this.txtDsearch.Location = new System.Drawing.Point(534, 279);
            this.txtDsearch.Name = "txtDsearch";
            this.txtDsearch.Size = new System.Drawing.Size(108, 20);
            this.txtDsearch.TabIndex = 69;
            this.txtDsearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDsearch_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(440, 282);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 15);
            this.label5.TabIndex = 71;
            this.label5.Text = "Reporting To";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(16, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 15);
            this.label2.TabIndex = 67;
            this.label2.Text = "Logical Branch";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gvMappedGroups);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(6, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(637, 196);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mapped Groups";
            // 
            // gvMappedGroups
            // 
            this.gvMappedGroups.AllowUserToAddRows = false;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle21.ForeColor = System.Drawing.Color.Black;
            this.gvMappedGroups.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle21;
            this.gvMappedGroups.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvMappedGroups.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvMappedGroups.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.gvMappedGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvMappedGroups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ECode,
            this.GroupName,
            this.GroupECodeName,
            this.logBranchCode,
            this.LogicalBranch});
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvMappedGroups.DefaultCellStyle = dataGridViewCellStyle23;
            this.gvMappedGroups.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvMappedGroups.Location = new System.Drawing.Point(7, 19);
            this.gvMappedGroups.MultiSelect = false;
            this.gvMappedGroups.Name = "gvMappedGroups";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvMappedGroups.RowHeadersDefaultCellStyle = dataGridViewCellStyle24;
            this.gvMappedGroups.RowHeadersVisible = false;
            this.gvMappedGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvMappedGroups.Size = new System.Drawing.Size(623, 173);
            this.gvMappedGroups.TabIndex = 2;
            this.gvMappedGroups.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvMappedGroups_RowEnter);
            this.gvMappedGroups.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvMappedGroups_CellClick);
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
            this.GroupName.Width = 180;
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
            this.GroupECodeName.Width = 250;
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
            this.LogicalBranch.Width = 180;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(349, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 32;
            this.label7.Text = "Branch";
            // 
            // cbBranches
            // 
            this.cbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranches.FormattingEnabled = true;
            this.cbBranches.Location = new System.Drawing.Point(402, 15);
            this.cbBranches.Name = "cbBranches";
            this.cbBranches.Size = new System.Drawing.Size(242, 23);
            this.cbBranches.TabIndex = 28;
            this.cbBranches.SelectedIndexChanged += new System.EventHandler(this.cbBranches_SelectedIndexChanged);
            // 
            // lblDocMonth
            // 
            this.lblDocMonth.BackColor = System.Drawing.SystemColors.Info;
            this.lblDocMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocMonth.ForeColor = System.Drawing.Color.Maroon;
            this.lblDocMonth.Location = new System.Drawing.Point(86, 13);
            this.lblDocMonth.Name = "lblDocMonth";
            this.lblDocMonth.Size = new System.Drawing.Size(87, 23);
            this.lblDocMonth.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(10, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 15);
            this.label6.TabIndex = 30;
            this.label6.Text = "Doc Month";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(174, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 29;
            this.label1.Text = "State";
            // 
            // cbStates
            // 
            this.cbStates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStates.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStates.FormattingEnabled = true;
            this.cbStates.Location = new System.Drawing.Point(214, 14);
            this.cbStates.Name = "cbStates";
            this.cbStates.Size = new System.Drawing.Size(135, 23);
            this.cbStates.TabIndex = 27;
            this.cbStates.SelectedIndexChanged += new System.EventHandler(this.cbStates_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbLogcalBranch);
            this.groupBox4.Controls.Add(this.txtlogicalBranch);
            this.groupBox4.Location = new System.Drawing.Point(8, 238);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(634, 39);
            this.groupBox4.TabIndex = 74;
            this.groupBox4.TabStop = false;
            // 
            // cbLogcalBranch
            // 
            this.cbLogcalBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLogcalBranch.Enabled = false;
            this.cbLogcalBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLogcalBranch.FormattingEnabled = true;
            this.cbLogcalBranch.Location = new System.Drawing.Point(113, 11);
            this.cbLogcalBranch.Name = "cbLogcalBranch";
            this.cbLogcalBranch.Size = new System.Drawing.Size(306, 23);
            this.cbLogcalBranch.TabIndex = 75;
            // 
            // txtlogicalBranch
            // 
            this.txtlogicalBranch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtlogicalBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtlogicalBranch.Location = new System.Drawing.Point(113, 11);
            this.txtlogicalBranch.Name = "txtlogicalBranch";
            this.txtlogicalBranch.Size = new System.Drawing.Size(306, 24);
            this.txtlogicalBranch.TabIndex = 68;
            // 
            // ServiceLevelGroupToDestination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(655, 599);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceLevelGroupToDestination";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Service Level Mapping GroupsToDestination";
            this.Load += new System.EventHandler(this.ServiceLevelGroupToDestination_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvMappedGroups)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbBranches;
        private System.Windows.Forms.Label lblDocMonth;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbStates;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.DataGridView gvMappedGroups;
        private System.Windows.Forms.DataGridViewTextBoxColumn ECode;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupECodeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn logBranchCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogicalBranch;
        private System.Windows.Forms.TextBox txtlogicalBranch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDsearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckedListBox clbDestination;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbLogcalBranch;


    }
}