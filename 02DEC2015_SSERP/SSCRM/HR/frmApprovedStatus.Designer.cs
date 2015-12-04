namespace SSCRM
{
    partial class frmApprovedStatus
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
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.grouper1 = new GroupCtrl.Grouper();
            this.lblEcode = new System.Windows.Forms.Label();
            this.lblQulf = new System.Windows.Forms.Label();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.gvPendingData = new System.Windows.Forms.DataGridView();
            this.SlNo_ref = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_COMPANY_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_BRANCH_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_APPL_NUMBER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_EORA_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAED_SSC_NUMBER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_FORH_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_DOB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_DOJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qualfication = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_REASON = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkApproved = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.lnkReason = new System.Windows.Forms.DataGridViewLinkColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtEcodeSearch = new System.Windows.Forms.TextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbBranch_optional = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.grouper1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPendingData)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.Frozen = true;
            this.dataGridViewImageColumn1.HeaderText = "Edit";
            this.dataGridViewImageColumn1.Image = global::SSCRM.Properties.Resources.Edit;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Width = 40;
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.PowderBlue;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouper1.BackgroundGradientMode = GroupCtrl.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.lblEcode);
            this.grouper1.Controls.Add(this.lblQulf);
            this.grouper1.Controls.Add(this.btnReject);
            this.grouper1.Controls.Add(this.btnClose);
            this.grouper1.Controls.Add(this.btnClear);
            this.grouper1.Controls.Add(this.btnSubmit);
            this.grouper1.Controls.Add(this.cmbCompany);
            this.grouper1.Controls.Add(this.label27);
            this.grouper1.Controls.Add(this.gvPendingData);
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.Controls.Add(this.groupBox2);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "";
            this.grouper1.Location = new System.Drawing.Point(0, -11);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 1;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(948, 523);
            this.grouper1.TabIndex = 65;
            // 
            // lblEcode
            // 
            this.lblEcode.AutoSize = true;
            this.lblEcode.Location = new System.Drawing.Point(641, 498);
            this.lblEcode.Name = "lblEcode";
            this.lblEcode.Size = new System.Drawing.Size(0, 13);
            this.lblEcode.TabIndex = 72;
            this.lblEcode.Visible = false;
            // 
            // lblQulf
            // 
            this.lblQulf.AutoSize = true;
            this.lblQulf.Location = new System.Drawing.Point(677, 498);
            this.lblQulf.Name = "lblQulf";
            this.lblQulf.Size = new System.Drawing.Size(0, 13);
            this.lblQulf.TabIndex = 71;
            this.lblQulf.Visible = false;
            // 
            // btnReject
            // 
            this.btnReject.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnReject.ForeColor = System.Drawing.Color.Navy;
            this.btnReject.Location = new System.Drawing.Point(364, 489);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(75, 32);
            this.btnReject.TabIndex = 68;
            this.btnReject.Text = "Reject";
            this.btnReject.UseVisualStyleBackColor = false;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.Navy;
            this.btnClose.Location = new System.Drawing.Point(521, 489);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 32);
            this.btnClose.TabIndex = 65;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.Navy;
            this.btnClear.Location = new System.Drawing.Point(442, 489);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 32);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.ForeColor = System.Drawing.Color.Navy;
            this.btnSubmit.Location = new System.Drawing.Point(287, 489);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 32);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Approve";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cmbCompany
            // 
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Items.AddRange(new object[] {
            "--SELECT--",
            "MALE",
            "FEMALE"});
            this.cmbCompany.Location = new System.Drawing.Point(79, 21);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(352, 23);
            this.cmbCompany.TabIndex = 0;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label27.Location = new System.Drawing.Point(12, 23);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(64, 14);
            this.label27.TabIndex = 61;
            this.label27.Text = "Company";
            // 
            // gvPendingData
            // 
            this.gvPendingData.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvPendingData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvPendingData.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvPendingData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPendingData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvPendingData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPendingData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SlNo_ref,
            this.HAMH_COMPANY_CODE,
            this.HAMH_BRANCH_CODE,
            this.HAMH_APPL_NUMBER,
            this.HAMH_EORA_CODE,
            this.HAED_SSC_NUMBER,
            this.HAMH_NAME,
            this.HAMH_FORH_NAME,
            this.HAMH_DOB,
            this.HAMH_DOJ,
            this.Qualfication,
            this.HAMH_REASON,
            this.aFlag,
            this.chkApproved,
            this.Edit,
            this.lnkReason});
            this.gvPendingData.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvPendingData.Location = new System.Drawing.Point(2, 78);
            this.gvPendingData.MultiSelect = false;
            this.gvPendingData.Name = "gvPendingData";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPendingData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvPendingData.RowHeadersVisible = false;
            this.gvPendingData.Size = new System.Drawing.Size(944, 410);
            this.gvPendingData.TabIndex = 62;
            this.gvPendingData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPendingData_CellEndEdit);
            this.gvPendingData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPendingData_CellClick);
            // 
            // SlNo_ref
            // 
            this.SlNo_ref.Frozen = true;
            this.SlNo_ref.HeaderText = "Sl.No";
            this.SlNo_ref.Name = "SlNo_ref";
            this.SlNo_ref.ReadOnly = true;
            this.SlNo_ref.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SlNo_ref.Width = 40;
            // 
            // HAMH_COMPANY_CODE
            // 
            this.HAMH_COMPANY_CODE.Frozen = true;
            this.HAMH_COMPANY_CODE.HeaderText = "cCODE";
            this.HAMH_COMPANY_CODE.Name = "HAMH_COMPANY_CODE";
            this.HAMH_COMPANY_CODE.ReadOnly = true;
            this.HAMH_COMPANY_CODE.Visible = false;
            // 
            // HAMH_BRANCH_CODE
            // 
            this.HAMH_BRANCH_CODE.Frozen = true;
            this.HAMH_BRANCH_CODE.HeaderText = "bCODE";
            this.HAMH_BRANCH_CODE.Name = "HAMH_BRANCH_CODE";
            this.HAMH_BRANCH_CODE.ReadOnly = true;
            this.HAMH_BRANCH_CODE.Visible = false;
            // 
            // HAMH_APPL_NUMBER
            // 
            this.HAMH_APPL_NUMBER.Frozen = true;
            this.HAMH_APPL_NUMBER.HeaderText = "appno";
            this.HAMH_APPL_NUMBER.Name = "HAMH_APPL_NUMBER";
            this.HAMH_APPL_NUMBER.ReadOnly = true;
            this.HAMH_APPL_NUMBER.Visible = false;
            // 
            // HAMH_EORA_CODE
            // 
            this.HAMH_EORA_CODE.Frozen = true;
            this.HAMH_EORA_CODE.HeaderText = "E CODE";
            this.HAMH_EORA_CODE.Name = "HAMH_EORA_CODE";
            this.HAMH_EORA_CODE.ReadOnly = true;
            this.HAMH_EORA_CODE.Width = 60;
            // 
            // HAED_SSC_NUMBER
            // 
            this.HAED_SSC_NUMBER.Frozen = true;
            this.HAED_SSC_NUMBER.HeaderText = "SSC No.";
            this.HAED_SSC_NUMBER.MinimumWidth = 100;
            this.HAED_SSC_NUMBER.Name = "HAED_SSC_NUMBER";
            this.HAED_SSC_NUMBER.ReadOnly = true;
            // 
            // HAMH_NAME
            // 
            this.HAMH_NAME.Frozen = true;
            this.HAMH_NAME.HeaderText = "Name";
            this.HAMH_NAME.Name = "HAMH_NAME";
            this.HAMH_NAME.ReadOnly = true;
            this.HAMH_NAME.Width = 120;
            // 
            // HAMH_FORH_NAME
            // 
            this.HAMH_FORH_NAME.Frozen = true;
            this.HAMH_FORH_NAME.HeaderText = "Father Name";
            this.HAMH_FORH_NAME.Name = "HAMH_FORH_NAME";
            this.HAMH_FORH_NAME.ReadOnly = true;
            this.HAMH_FORH_NAME.Width = 120;
            // 
            // HAMH_DOB
            // 
            this.HAMH_DOB.Frozen = true;
            this.HAMH_DOB.HeaderText = "DOB";
            this.HAMH_DOB.Name = "HAMH_DOB";
            this.HAMH_DOB.ReadOnly = true;
            this.HAMH_DOB.Width = 80;
            // 
            // HAMH_DOJ
            // 
            this.HAMH_DOJ.Frozen = true;
            this.HAMH_DOJ.HeaderText = "DOJ";
            this.HAMH_DOJ.Name = "HAMH_DOJ";
            this.HAMH_DOJ.ReadOnly = true;
            this.HAMH_DOJ.Width = 80;
            // 
            // Qualfication
            // 
            this.Qualfication.Frozen = true;
            this.Qualfication.HeaderText = "Qualfication";
            this.Qualfication.Name = "Qualfication";
            this.Qualfication.Width = 80;
            // 
            // HAMH_REASON
            // 
            this.HAMH_REASON.Frozen = true;
            this.HAMH_REASON.HeaderText = "Reason";
            this.HAMH_REASON.Name = "HAMH_REASON";
            this.HAMH_REASON.ReadOnly = true;
            // 
            // aFlag
            // 
            this.aFlag.HeaderText = "aFlag";
            this.aFlag.Name = "aFlag";
            this.aFlag.ReadOnly = true;
            this.aFlag.Visible = false;
            // 
            // chkApproved
            // 
            this.chkApproved.HeaderText = "Select";
            this.chkApproved.MinimumWidth = 30;
            this.chkApproved.Name = "chkApproved";
            this.chkApproved.Width = 30;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = global::SSCRM.Properties.Resources.Edit;
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Width = 30;
            // 
            // lnkReason
            // 
            this.lnkReason.HeaderText = "Reason";
            this.lnkReason.Name = "lnkReason";
            this.lnkReason.Text = "Reason";
            this.lnkReason.UseColumnTextForLinkValue = true;
            this.lnkReason.Width = 60;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(39, 318);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 14);
            this.label1.TabIndex = 64;
            this.label1.Text = "State ";
            this.label1.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtEcodeSearch);
            this.groupBox2.Controls.Add(this.cmbStatus);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmbBranch_optional);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmbType);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Location = new System.Drawing.Point(4, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(939, 68);
            this.groupBox2.TabIndex = 70;
            this.groupBox2.TabStop = false;
            // 
            // txtEcodeSearch
            // 
            this.txtEcodeSearch.Location = new System.Drawing.Point(846, 43);
            this.txtEcodeSearch.Name = "txtEcodeSearch";
            this.txtEcodeSearch.Size = new System.Drawing.Size(80, 20);
            this.txtEcodeSearch.TabIndex = 74;
            this.txtEcodeSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEcodeSearch_KeyUp);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "PENDING",
            "REJOIN PENDING",
            "REJECT",
            "LEFT"});
            this.cmbStatus.Location = new System.Drawing.Point(659, 41);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(120, 23);
            this.cmbStatus.TabIndex = 66;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label4.Location = new System.Drawing.Point(785, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 73;
            this.label4.Text = "Search";
            // 
            // cmbBranch_optional
            // 
            this.cmbBranch_optional.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBranch_optional.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBranch_optional.FormattingEnabled = true;
            this.cmbBranch_optional.Location = new System.Drawing.Point(76, 41);
            this.cmbBranch_optional.Name = "cmbBranch_optional";
            this.cmbBranch_optional.Size = new System.Drawing.Size(352, 23);
            this.cmbBranch_optional.TabIndex = 1;
            this.cmbBranch_optional.SelectedIndexChanged += new System.EventHandler(this.cmbBranch_optional_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(608, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 14);
            this.label2.TabIndex = 67;
            this.label2.Text = "Status";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "AGENTES",
            "EMPLOYEES"});
            this.cmbType.Location = new System.Drawing.Point(478, 41);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(120, 23);
            this.cmbType.TabIndex = 71;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(432, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 14);
            this.label3.TabIndex = 72;
            this.label3.Text = "Select";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label24.Location = new System.Drawing.Point(23, 46);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(49, 14);
            this.label24.TabIndex = 43;
            this.label24.Text = "Branch";
            // 
            // frmApprovedStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 512);
            this.ControlBox = false;
            this.Controls.Add(this.grouper1);
            this.Name = "frmApprovedStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Approved Status";
            this.Load += new System.EventHandler(this.frmApprovedStatus_Load);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPendingData)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cmbBranch_optional;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label27;
        public System.Windows.Forms.DataGridView gvPendingData;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label1;
        private GroupCtrl.Grouper grouper1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlNo_ref;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_COMPANY_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_BRANCH_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_APPL_NUMBER;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_EORA_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAED_SSC_NUMBER;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_FORH_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_DOB;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_DOJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qualfication;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_REASON;
        private System.Windows.Forms.DataGridViewTextBoxColumn aFlag;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkApproved;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewLinkColumn lnkReason;
        private System.Windows.Forms.Label lblQulf;
        private System.Windows.Forms.Label lblEcode;
        private System.Windows.Forms.TextBox txtEcodeSearch;
        private System.Windows.Forms.Label label4;
    }
}