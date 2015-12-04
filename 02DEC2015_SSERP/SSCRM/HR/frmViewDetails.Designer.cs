namespace SSCRM
{
    partial class frmViewDetails
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewDetails));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.grouper1 = new GroupCtrl.Grouper();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdStatus = new System.Windows.Forms.ComboBox();
            this.txtEcodeSearch = new System.Windows.Forms.TextBox();
            this.lblEcode = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.cmbBranch_optional = new System.Windows.Forms.ComboBox();
            this.gvPendingData = new System.Windows.Forms.DataGridView();
            this.SlNo_ref = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_APPL_NUMBER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_EORA_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_FORH_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESIG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DEPT_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_BRANCH_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_COMPANY_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Left = new System.Windows.Forms.DataGridViewImageColumn();
            this.grouper1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPendingData)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Left";
            this.dataGridViewImageColumn1.Image = global::SSCRM.Properties.Resources.Del;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Width = 50;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Edit.jpg");
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.PowderBlue;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouper1.BackgroundGradientMode = GroupCtrl.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.Transparent;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.label2);
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.Controls.Add(this.cmdStatus);
            this.grouper1.Controls.Add(this.txtEcodeSearch);
            this.grouper1.Controls.Add(this.lblEcode);
            this.grouper1.Controls.Add(this.btnExport);
            this.grouper1.Controls.Add(this.cmbType);
            this.grouper1.Controls.Add(this.btnClose);
            this.grouper1.Controls.Add(this.lblTotal);
            this.grouper1.Controls.Add(this.cmbCompany);
            this.grouper1.Controls.Add(this.label24);
            this.grouper1.Controls.Add(this.label27);
            this.grouper1.Controls.Add(this.cmbBranch_optional);
            this.grouper1.Controls.Add(this.gvPendingData);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "";
            this.grouper1.Location = new System.Drawing.Point(2, -9);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 1;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(913, 570);
            this.grouper1.TabIndex = 66;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(443, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 14);
            this.label2.TabIndex = 75;
            this.label2.Text = "E/A";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(587, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 14);
            this.label1.TabIndex = 74;
            this.label1.Text = "Status";
            // 
            // cmdStatus
            // 
            this.cmdStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmdStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStatus.FormattingEnabled = true;
            this.cmdStatus.Items.AddRange(new object[] {
            "ALL",
            "PENDING",
            "WORKING",
            "LEFT",
            "REJECT"});
            this.cmdStatus.Location = new System.Drawing.Point(636, 41);
            this.cmdStatus.Name = "cmdStatus";
            this.cmdStatus.Size = new System.Drawing.Size(116, 24);
            this.cmdStatus.TabIndex = 3;
            this.cmdStatus.SelectedIndexChanged += new System.EventHandler(this.cmdStatus_SelectedIndexChanged);
            // 
            // txtEcodeSearch
            // 
            this.txtEcodeSearch.Location = new System.Drawing.Point(822, 43);
            this.txtEcodeSearch.Name = "txtEcodeSearch";
            this.txtEcodeSearch.Size = new System.Drawing.Size(80, 20);
            this.txtEcodeSearch.TabIndex = 4;
            this.txtEcodeSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEcodeSearch_KeyUp);
            // 
            // lblEcode
            // 
            this.lblEcode.AutoSize = true;
            this.lblEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEcode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblEcode.Location = new System.Drawing.Point(761, 46);
            this.lblEcode.Name = "lblEcode";
            this.lblEcode.Size = new System.Drawing.Size(59, 17);
            this.lblEcode.TabIndex = 71;
            this.lblEcode.Text = "Search";
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.Navy;
            this.btnExport.Location = new System.Drawing.Point(419, 534);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(111, 30);
            this.btnExport.TabIndex = 70;
            this.btnExport.Text = "Excel Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "ALL",
            "AGENT",
            "EMPLOYEE"});
            this.cmbType.Location = new System.Drawing.Point(479, 41);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(100, 24);
            this.cmbType.TabIndex = 2;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.Navy;
            this.btnClose.Location = new System.Drawing.Point(318, 534);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(83, 30);
            this.btnClose.TabIndex = 68;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblTotal.Location = new System.Drawing.Point(738, 353);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(0, 14);
            this.lblTotal.TabIndex = 67;
            this.lblTotal.Visible = false;
            // 
            // cmbCompany
            // 
            this.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Items.AddRange(new object[] {
            "--SELECT--",
            "MALE",
            "FEMALE"});
            this.cmbCompany.Location = new System.Drawing.Point(67, 15);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(334, 24);
            this.cmbCompany.TabIndex = 0;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label24.Location = new System.Drawing.Point(16, 46);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(49, 14);
            this.label24.TabIndex = 64;
            this.label24.Text = "Branch";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label27.Location = new System.Drawing.Point(1, 20);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(64, 14);
            this.label27.TabIndex = 65;
            this.label27.Text = "Company";
            // 
            // cmbBranch_optional
            // 
            this.cmbBranch_optional.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBranch_optional.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBranch_optional.FormattingEnabled = true;
            this.cmbBranch_optional.Location = new System.Drawing.Point(67, 41);
            this.cmbBranch_optional.Name = "cmbBranch_optional";
            this.cmbBranch_optional.Size = new System.Drawing.Size(334, 24);
            this.cmbBranch_optional.TabIndex = 1;
            this.cmbBranch_optional.SelectedIndexChanged += new System.EventHandler(this.cmbBranch_optional_SelectedIndexChanged);
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPendingData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvPendingData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPendingData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SlNo_ref,
            this.HAMH_APPL_NUMBER,
            this.HAMH_EORA_CODE,
            this.HAMH_NAME,
            this.DOJ,
            this.HAMH_FORH_NAME,
            this.DESIG,
            this.DEPT_DESC,
            this.Status,
            this.HAMH_BRANCH_CODE,
            this.HAMH_COMPANY_CODE,
            this.Left});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvPendingData.DefaultCellStyle = dataGridViewCellStyle13;
            this.gvPendingData.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvPendingData.Location = new System.Drawing.Point(6, 67);
            this.gvPendingData.MultiSelect = false;
            this.gvPendingData.Name = "gvPendingData";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPendingData.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.gvPendingData.RowHeadersVisible = false;
            this.gvPendingData.Size = new System.Drawing.Size(896, 461);
            this.gvPendingData.TabIndex = 66;
            this.gvPendingData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPendingData_CellClick);
            // 
            // SlNo_ref
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SlNo_ref.DefaultCellStyle = dataGridViewCellStyle3;
            this.SlNo_ref.Frozen = true;
            this.SlNo_ref.HeaderText = "SlNo";
            this.SlNo_ref.Name = "SlNo_ref";
            this.SlNo_ref.ReadOnly = true;
            this.SlNo_ref.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SlNo_ref.Width = 40;
            // 
            // HAMH_APPL_NUMBER
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HAMH_APPL_NUMBER.DefaultCellStyle = dataGridViewCellStyle4;
            this.HAMH_APPL_NUMBER.HeaderText = "HAMH_APPL_NUMBER";
            this.HAMH_APPL_NUMBER.Name = "HAMH_APPL_NUMBER";
            this.HAMH_APPL_NUMBER.ReadOnly = true;
            this.HAMH_APPL_NUMBER.Visible = false;
            this.HAMH_APPL_NUMBER.Width = 50;
            // 
            // HAMH_EORA_CODE
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HAMH_EORA_CODE.DefaultCellStyle = dataGridViewCellStyle5;
            this.HAMH_EORA_CODE.HeaderText = "E.Code";
            this.HAMH_EORA_CODE.Name = "HAMH_EORA_CODE";
            this.HAMH_EORA_CODE.ReadOnly = true;
            this.HAMH_EORA_CODE.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.HAMH_EORA_CODE.Width = 60;
            // 
            // HAMH_NAME
            // 
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HAMH_NAME.DefaultCellStyle = dataGridViewCellStyle6;
            this.HAMH_NAME.HeaderText = "Name";
            this.HAMH_NAME.Name = "HAMH_NAME";
            this.HAMH_NAME.ReadOnly = true;
            this.HAMH_NAME.Width = 180;
            // 
            // DOJ
            // 
            this.DOJ.HeaderText = "DOJ";
            this.DOJ.Name = "DOJ";
            this.DOJ.ReadOnly = true;
            this.DOJ.Width = 90;
            // 
            // HAMH_FORH_NAME
            // 
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HAMH_FORH_NAME.DefaultCellStyle = dataGridViewCellStyle7;
            this.HAMH_FORH_NAME.HeaderText = "Father Name";
            this.HAMH_FORH_NAME.Name = "HAMH_FORH_NAME";
            this.HAMH_FORH_NAME.ReadOnly = true;
            this.HAMH_FORH_NAME.Width = 150;
            // 
            // DESIG
            // 
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DESIG.DefaultCellStyle = dataGridViewCellStyle8;
            this.DESIG.HeaderText = "Designation";
            this.DESIG.Name = "DESIG";
            this.DESIG.ReadOnly = true;
            this.DESIG.Width = 130;
            // 
            // DEPT_DESC
            // 
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DEPT_DESC.DefaultCellStyle = dataGridViewCellStyle9;
            this.DEPT_DESC.HeaderText = "Department";
            this.DEPT_DESC.Name = "DEPT_DESC";
            this.DEPT_DESC.ReadOnly = true;
            this.DEPT_DESC.Width = 110;
            // 
            // Status
            // 
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Status.DefaultCellStyle = dataGridViewCellStyle10;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 70;
            // 
            // HAMH_BRANCH_CODE
            // 
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HAMH_BRANCH_CODE.DefaultCellStyle = dataGridViewCellStyle11;
            this.HAMH_BRANCH_CODE.HeaderText = "Branch";
            this.HAMH_BRANCH_CODE.Name = "HAMH_BRANCH_CODE";
            this.HAMH_BRANCH_CODE.ReadOnly = true;
            this.HAMH_BRANCH_CODE.Visible = false;
            this.HAMH_BRANCH_CODE.Width = 80;
            // 
            // HAMH_COMPANY_CODE
            // 
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HAMH_COMPANY_CODE.DefaultCellStyle = dataGridViewCellStyle12;
            this.HAMH_COMPANY_CODE.HeaderText = "Company";
            this.HAMH_COMPANY_CODE.Name = "HAMH_COMPANY_CODE";
            this.HAMH_COMPANY_CODE.ReadOnly = true;
            this.HAMH_COMPANY_CODE.Visible = false;
            // 
            // Left
            // 
            this.Left.HeaderText = "for Left";
            this.Left.Image = global::SSCRM.Properties.Resources.Edit;
            this.Left.Name = "Left";
            this.Left.ReadOnly = true;
            this.Left.Visible = false;
            this.Left.Width = 40;
            // 
            // frmViewDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 562);
            this.ControlBox = false;
            this.Controls.Add(this.grouper1);
            this.Name = "frmViewDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Details";
            this.Load += new System.EventHandler(this.frmViewDetails_Load);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPendingData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox cmbBranch_optional;
        private System.Windows.Forms.Label label24;
        private GroupCtrl.Grouper grouper1;
        public System.Windows.Forms.DataGridView gvPendingData;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txtEcodeSearch;
        private System.Windows.Forms.Label lblEcode;
        private System.Windows.Forms.ComboBox cmdStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlNo_ref;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_APPL_NUMBER;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_EORA_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_FORH_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESIG;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEPT_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_BRANCH_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_COMPANY_CODE;
        private System.Windows.Forms.DataGridViewImageColumn Left;
    }
}