namespace SSCRM
{
    partial class SouceToDestination
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ECode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupECodeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logBranchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogicalBranch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtMsearch = new System.Windows.Forms.TextBox();
            this.txtDsearch = new System.Windows.Forms.TextBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblMap = new System.Windows.Forms.Label();
            this.lbMapp = new System.Windows.Forms.ListBox();
            this.chkMapp = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbGroupCamp = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbLogcalBranch = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.rdbOthers = new System.Windows.Forms.RadioButton();
            this.rdbGroup = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cbLevels = new System.Windows.Forms.ComboBox();
            this.clbDestination = new System.Windows.Forms.CheckedListBox();
            this.clbSource = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLock = new System.Windows.Forms.Button();
            this.btnUnlock = new System.Windows.Forms.Button();
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
            this.groupBox1.Location = new System.Drawing.Point(6, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 40);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(423, 14);
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
            this.cbBranches.Location = new System.Drawing.Point(476, 11);
            this.cbBranches.Name = "cbBranches";
            this.cbBranches.Size = new System.Drawing.Size(259, 23);
            this.cbBranches.TabIndex = 25;
            this.cbBranches.SelectedIndexChanged += new System.EventHandler(this.cbBranches_SelectedIndexChanged);
            // 
            // lblDocMonth
            // 
            this.lblDocMonth.BackColor = System.Drawing.SystemColors.Info;
            this.lblDocMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblDocMonth.Location = new System.Drawing.Point(80, 11);
            this.lblDocMonth.Name = "lblDocMonth";
            this.lblDocMonth.Size = new System.Drawing.Size(89, 23);
            this.lblDocMonth.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(2, 14);
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
            this.label1.Location = new System.Drawing.Point(172, 14);
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
            this.cbStates.Location = new System.Drawing.Point(218, 10);
            this.cbStates.Name = "cbStates";
            this.cbStates.Size = new System.Drawing.Size(203, 23);
            this.cbStates.TabIndex = 0;
            this.cbStates.SelectedIndexChanged += new System.EventHandler(this.cbStates_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.Controls.Add(this.btnUnlock);
            this.groupBox2.Controls.Add(this.btnLock);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Location = new System.Drawing.Point(6, 507);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(735, 45);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.Location = new System.Drawing.Point(163, 9);
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
            this.btnCancel.Location = new System.Drawing.Point(86, 9);
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
            this.btnClose.Location = new System.Drawing.Point(261, 9);
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
            this.btnSave.Location = new System.Drawing.Point(10, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.gvMappedGroups);
            this.groupBox3.Controls.Add(this.txtMsearch);
            this.groupBox3.Controls.Add(this.txtDsearch);
            this.groupBox3.Controls.Add(this.txtSearch);
            this.groupBox3.Controls.Add(this.lblMap);
            this.groupBox3.Controls.Add(this.lbMapp);
            this.groupBox3.Controls.Add(this.chkMapp);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.clbDestination);
            this.groupBox3.Controls.Add(this.clbSource);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(0, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(961, 562);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            // 
            // gvMappedGroups
            // 
            this.gvMappedGroups.AllowUserToAddRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            this.gvMappedGroups.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.gvMappedGroups.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvMappedGroups.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvMappedGroups.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.gvMappedGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvMappedGroups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.ECode,
            this.GroupName,
            this.GroupECodeName,
            this.logBranchCode,
            this.LogicalBranch,
            this.Phone,
            this.Address});
            this.gvMappedGroups.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvMappedGroups.Location = new System.Drawing.Point(5, 52);
            this.gvMappedGroups.MultiSelect = false;
            this.gvMappedGroups.Name = "gvMappedGroups";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvMappedGroups.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.gvMappedGroups.RowHeadersVisible = false;
            this.gvMappedGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvMappedGroups.Size = new System.Drawing.Size(742, 150);
            this.gvMappedGroups.TabIndex = 1;
            this.gvMappedGroups.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvMappedGroups_RowEnter);
            // 
            // SLNO
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.SLNO.DefaultCellStyle = dataGridViewCellStyle11;
            this.SLNO.Frozen = true;
            this.SLNO.HeaderText = "SlNo";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SLNO.Width = 40;
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
            this.GroupName.Width = 120;
            // 
            // GroupECodeName
            // 
            this.GroupECodeName.Frozen = true;
            this.GroupECodeName.HeaderText = "Group ECode Name";
            this.GroupECodeName.MinimumWidth = 20;
            this.GroupECodeName.Name = "GroupECodeName";
            this.GroupECodeName.ReadOnly = true;
            this.GroupECodeName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
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
            this.LogicalBranch.Width = 140;
            // 
            // Phone
            // 
            this.Phone.HeaderText = "Phone";
            this.Phone.Name = "Phone";
            // 
            // Address
            // 
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            this.Address.Width = 200;
            // 
            // txtMsearch
            // 
            this.txtMsearch.Location = new System.Drawing.Point(880, 31);
            this.txtMsearch.Name = "txtMsearch";
            this.txtMsearch.Size = new System.Drawing.Size(77, 20);
            this.txtMsearch.TabIndex = 29;
            this.txtMsearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMsearch_KeyUp);
            // 
            // txtDsearch
            // 
            this.txtDsearch.Location = new System.Drawing.Point(182, 288);
            this.txtDsearch.Name = "txtDsearch";
            this.txtDsearch.Size = new System.Drawing.Size(171, 20);
            this.txtDsearch.TabIndex = 7;
            this.txtDsearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDsearch_KeyUp);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(598, 288);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(147, 20);
            this.txtSearch.TabIndex = 8;
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            // 
            // lblMap
            // 
            this.lblMap.AutoSize = true;
            this.lblMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMap.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblMap.Location = new System.Drawing.Point(751, 33);
            this.lblMap.Margin = new System.Windows.Forms.Padding(0);
            this.lblMap.Name = "lblMap";
            this.lblMap.Size = new System.Drawing.Size(130, 15);
            this.lblMap.TabIndex = 26;
            this.lblMap.Text = "Unmapped  Source";
            // 
            // lbMapp
            // 
            this.lbMapp.FormattingEnabled = true;
            this.lbMapp.Location = new System.Drawing.Point(749, 53);
            this.lbMapp.Name = "lbMapp";
            this.lbMapp.Size = new System.Drawing.Size(210, 498);
            this.lbMapp.TabIndex = 25;
            // 
            // chkMapp
            // 
            this.chkMapp.AutoSize = true;
            this.chkMapp.BackColor = System.Drawing.Color.PowderBlue;
            this.chkMapp.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMapp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMapp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chkMapp.Location = new System.Drawing.Point(756, 12);
            this.chkMapp.Name = "chkMapp";
            this.chkMapp.Size = new System.Drawing.Size(84, 21);
            this.chkMapp.TabIndex = 24;
            this.chkMapp.Text = "Mapped";
            this.chkMapp.UseVisualStyleBackColor = false;
            this.chkMapp.CheckStateChanged += new System.EventHandler(this.chkMapp_CheckStateChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbGroupCamp);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.cbLogcalBranch);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtAddress);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtPhone);
            this.groupBox4.Controls.Add(this.rdbOthers);
            this.groupBox4.Controls.Add(this.rdbGroup);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.cbLevels);
            this.groupBox4.Location = new System.Drawing.Point(6, 200);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(742, 87);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            // 
            // cbGroupCamp
            // 
            this.cbGroupCamp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroupCamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGroupCamp.FormattingEnabled = true;
            this.cbGroupCamp.Location = new System.Drawing.Point(12, 29);
            this.cbGroupCamp.Name = "cbGroupCamp";
            this.cbGroupCamp.Size = new System.Drawing.Size(264, 23);
            this.cbGroupCamp.TabIndex = 4;
            this.cbGroupCamp.SelectedIndexChanged += new System.EventHandler(this.cbGroupCamp_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label9.Location = new System.Drawing.Point(334, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 15);
            this.label9.TabIndex = 34;
            this.label9.Text = "Logical Branch";
            // 
            // cbLogcalBranch
            // 
            this.cbLogcalBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLogcalBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLogcalBranch.FormattingEnabled = true;
            this.cbLogcalBranch.Location = new System.Drawing.Point(278, 29);
            this.cbLogcalBranch.Name = "cbLogcalBranch";
            this.cbLogcalBranch.Size = new System.Drawing.Size(227, 23);
            this.cbLogcalBranch.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(278, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 15);
            this.label8.TabIndex = 32;
            this.label8.Text = "Camp Address";
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.Color.Cornsilk;
            this.txtAddress.Location = new System.Drawing.Point(381, 53);
            this.txtAddress.MaxLength = 200;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(357, 30);
            this.txtAddress.TabIndex = 31;
            this.txtAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAddress_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(7, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 15);
            this.label2.TabIndex = 30;
            this.label2.Text = "Phone";
            // 
            // txtPhone
            // 
            this.txtPhone.BackColor = System.Drawing.Color.Cornsilk;
            this.txtPhone.Location = new System.Drawing.Point(57, 54);
            this.txtPhone.MaxLength = 20;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.ReadOnly = true;
            this.txtPhone.Size = new System.Drawing.Size(219, 20);
            this.txtPhone.TabIndex = 29;
            // 
            // rdbOthers
            // 
            this.rdbOthers.AutoSize = true;
            this.rdbOthers.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbOthers.ForeColor = System.Drawing.Color.Maroon;
            this.rdbOthers.Location = new System.Drawing.Point(90, 9);
            this.rdbOthers.Name = "rdbOthers";
            this.rdbOthers.Size = new System.Drawing.Size(90, 18);
            this.rdbOthers.TabIndex = 3;
            this.rdbOthers.TabStop = true;
            this.rdbOthers.Text = "Office Sales";
            this.rdbOthers.UseVisualStyleBackColor = true;
            this.rdbOthers.Click += new System.EventHandler(this.rdbOthers_Click);
            // 
            // rdbGroup
            // 
            this.rdbGroup.AutoSize = true;
            this.rdbGroup.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbGroup.Location = new System.Drawing.Point(14, 8);
            this.rdbGroup.Name = "rdbGroup";
            this.rdbGroup.Size = new System.Drawing.Size(57, 18);
            this.rdbGroup.TabIndex = 2;
            this.rdbGroup.TabStop = true;
            this.rdbGroup.Text = "Camp";
            this.rdbGroup.UseVisualStyleBackColor = true;
            this.rdbGroup.Click += new System.EventHandler(this.rdbGroup_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(588, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 15);
            this.label3.TabIndex = 21;
            this.label3.Text = "Level";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // cbLevels
            // 
            this.cbLevels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevels.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLevels.FormattingEnabled = true;
            this.cbLevels.Location = new System.Drawing.Point(509, 28);
            this.cbLevels.Name = "cbLevels";
            this.cbLevels.Size = new System.Drawing.Size(229, 23);
            this.cbLevels.TabIndex = 6;
            this.cbLevels.SelectedIndexChanged += new System.EventHandler(this.cbLevels_SelectedIndexChanged);
            // 
            // clbDestination
            // 
            this.clbDestination.CheckOnClick = true;
            this.clbDestination.FormattingEnabled = true;
            this.clbDestination.Location = new System.Drawing.Point(6, 310);
            this.clbDestination.Name = "clbDestination";
            this.clbDestination.Size = new System.Drawing.Size(347, 199);
            this.clbDestination.Sorted = true;
            this.clbDestination.TabIndex = 9;
            this.clbDestination.UseCompatibleTextRendering = true;
            this.clbDestination.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbDestination_ItemCheck);
            // 
            // clbSource
            // 
            this.clbSource.CheckOnClick = true;
            this.clbSource.FormattingEnabled = true;
            this.clbSource.Location = new System.Drawing.Point(355, 310);
            this.clbSource.Name = "clbSource";
            this.clbSource.Size = new System.Drawing.Size(392, 199);
            this.clbSource.TabIndex = 10;
            this.clbSource.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbSource_ItemCheck);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(82, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 15);
            this.label5.TabIndex = 19;
            this.label5.Text = "Reporting To";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(541, 290);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 18;
            this.label4.Text = "Source";
            // 
            // btnLock
            // 
            this.btnLock.BackColor = System.Drawing.Color.AliceBlue;
            this.btnLock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLock.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnLock.Location = new System.Drawing.Point(563, 10);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(75, 30);
            this.btnLock.TabIndex = 15;
            this.btnLock.Text = "&Lock";
            this.btnLock.UseVisualStyleBackColor = false;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // btnUnlock
            // 
            this.btnUnlock.BackColor = System.Drawing.Color.AliceBlue;
            this.btnUnlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnlock.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnUnlock.Location = new System.Drawing.Point(644, 10);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(75, 30);
            this.btnUnlock.TabIndex = 16;
            this.btnUnlock.Text = "&UnLock";
            this.btnUnlock.UseVisualStyleBackColor = false;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // SouceToDestination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(961, 568);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Name = "SouceToDestination";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mapping Source to Destination";
            this.Load += new System.EventHandler(this.SouceToDestination_Load);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckedListBox clbSource;
        private System.Windows.Forms.CheckedListBox clbDestination;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDocMonth;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbLevels;
        private System.Windows.Forms.CheckBox chkMapp;
        private System.Windows.Forms.ListBox lbMapp;
        private System.Windows.Forms.Label lblMap;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbBranches;
        private System.Windows.Forms.RadioButton rdbGroup;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox txtDsearch;
        private System.Windows.Forms.TextBox txtMsearch;
        private System.Windows.Forms.RadioButton rdbOthers;
        public System.Windows.Forms.DataGridView gvMappedGroups;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbLogcalBranch;
        private System.Windows.Forms.ComboBox cbGroupCamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ECode;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupECodeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn logBranchCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogicalBranch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.Button btnUnlock;
    }
}