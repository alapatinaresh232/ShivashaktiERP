namespace SSCRM
{
    partial class CampMasAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CampMasAdd));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gvCamp = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CampCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CampName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.District = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mandal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Village = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LandMark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Branch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompanyCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnVSearch = new System.Windows.Forms.Button();
            this.txtLandMark = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtVillage = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDistrict = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMandal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.txtCampAddress = new System.Windows.Forms.TextBox();
            this.txtCampName = new System.Windows.Forms.TextBox();
            this.txtCampCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCamp)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.gvCamp);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Location = new System.Drawing.Point(1, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(786, 431);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            // 
            // gvCamp
            // 
            this.gvCamp.AllowUserToAddRows = false;
            this.gvCamp.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvCamp.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCamp.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvCamp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvCamp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.CampCode,
            this.CampName,
            this.Phone,
            this.State,
            this.District,
            this.Mandal,
            this.Village,
            this.LandMark,
            this.Address,
            this.Active,
            this.Branch,
            this.CompanyCode,
            this.BranchCode,
            this.Edit});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SaddleBrown;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvCamp.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvCamp.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gvCamp.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvCamp.Location = new System.Drawing.Point(9, 166);
            this.gvCamp.MultiSelect = false;
            this.gvCamp.Name = "gvCamp";
            this.gvCamp.RowHeadersVisible = false;
            this.gvCamp.RowTemplate.ReadOnly = true;
            this.gvCamp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvCamp.Size = new System.Drawing.Size(769, 217);
            this.gvCamp.TabIndex = 6;
            this.gvCamp.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCamp_RowEnter);
            this.gvCamp.DoubleClick += new System.EventHandler(this.gvCamp_DoubleClick);
            this.gvCamp.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCamp_CellClick);
            // 
            // SLNO
            // 
            this.SLNO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SLNO.Frozen = true;
            this.SLNO.HeaderText = "S.No";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SLNO.Width = 40;
            // 
            // CampCode
            // 
            this.CampCode.Frozen = true;
            this.CampCode.HeaderText = "CampCode";
            this.CampCode.Name = "CampCode";
            this.CampCode.ReadOnly = true;
            // 
            // CampName
            // 
            this.CampName.Frozen = true;
            this.CampName.HeaderText = "Camp Name";
            this.CampName.Name = "CampName";
            this.CampName.ReadOnly = true;
            this.CampName.Width = 200;
            // 
            // Phone
            // 
            this.Phone.HeaderText = "Phone";
            this.Phone.Name = "Phone";
            // 
            // State
            // 
            this.State.HeaderText = "State";
            this.State.Name = "State";
            this.State.ReadOnly = true;
            // 
            // District
            // 
            this.District.HeaderText = "District";
            this.District.Name = "District";
            this.District.ReadOnly = true;
            // 
            // Mandal
            // 
            this.Mandal.HeaderText = "Mandal";
            this.Mandal.Name = "Mandal";
            this.Mandal.ReadOnly = true;
            // 
            // Village
            // 
            this.Village.HeaderText = "Village";
            this.Village.Name = "Village";
            this.Village.ReadOnly = true;
            // 
            // LandMark
            // 
            this.LandMark.HeaderText = "LandMark";
            this.LandMark.Name = "LandMark";
            this.LandMark.ReadOnly = true;
            // 
            // Address
            // 
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            this.Address.Width = 250;
            // 
            // Active
            // 
            this.Active.HeaderText = "Active";
            this.Active.Name = "Active";
            this.Active.Visible = false;
            // 
            // Branch
            // 
            this.Branch.HeaderText = "Branch";
            this.Branch.Name = "Branch";
            this.Branch.Visible = false;
            this.Branch.Width = 150;
            // 
            // CompanyCode
            // 
            this.CompanyCode.HeaderText = "CompanyCode";
            this.CompanyCode.MinimumWidth = 20;
            this.CompanyCode.Name = "CompanyCode";
            this.CompanyCode.ReadOnly = true;
            this.CompanyCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CompanyCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CompanyCode.Visible = false;
            this.CompanyCode.Width = 80;
            // 
            // BranchCode
            // 
            this.BranchCode.HeaderText = "BranchCode";
            this.BranchCode.Name = "BranchCode";
            this.BranchCode.Visible = false;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = global::SSCRM.Properties.Resources.Edit;
            this.Edit.Name = "Edit";
            this.Edit.Width = 50;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.btnVSearch);
            this.groupBox1.Controls.Add(this.txtLandMark);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtVillage);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtState);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtDistrict);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMandal);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPhone);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkActive);
            this.groupBox1.Controls.Add(this.txtCampAddress);
            this.groupBox1.Controls.Add(this.txtCampName);
            this.groupBox1.Controls.Add(this.txtCampCode);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(768, 153);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            // 
            // btnVSearch
            // 
            this.btnVSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnVSearch.BackColor = System.Drawing.Color.YellowGreen;
            this.btnVSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnVSearch.FlatAppearance.BorderSize = 5;
            this.btnVSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnVSearch.Image")));
            this.btnVSearch.Location = new System.Drawing.Point(384, 37);
            this.btnVSearch.Margin = new System.Windows.Forms.Padding(1);
            this.btnVSearch.Name = "btnVSearch";
            this.btnVSearch.Size = new System.Drawing.Size(26, 22);
            this.btnVSearch.TabIndex = 65;
            this.btnVSearch.Tag = "Village Search";
            this.btnVSearch.UseVisualStyleBackColor = false;
            this.btnVSearch.Click += new System.EventHandler(this.btnVSearch_Click);
            // 
            // txtLandMark
            // 
            this.txtLandMark.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLandMark.Location = new System.Drawing.Point(517, 81);
            this.txtLandMark.MaxLength = 20;
            this.txtLandMark.Name = "txtLandMark";
            this.txtLandMark.Size = new System.Drawing.Size(219, 21);
            this.txtLandMark.TabIndex = 63;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label15.Location = new System.Drawing.Point(437, 83);
            this.label15.Margin = new System.Windows.Forms.Padding(0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(75, 15);
            this.label15.TabIndex = 64;
            this.label15.Text = "Land Mark";
            // 
            // txtVillage
            // 
            this.txtVillage.BackColor = System.Drawing.SystemColors.Window;
            this.txtVillage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVillage.Location = new System.Drawing.Point(144, 37);
            this.txtVillage.MaxLength = 50;
            this.txtVillage.Name = "txtVillage";
            this.txtVillage.Size = new System.Drawing.Size(239, 21);
            this.txtVillage.TabIndex = 62;
            this.txtVillage.TabStop = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label17.Location = new System.Drawing.Point(20, 39);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(121, 15);
            this.label17.TabIndex = 61;
            this.label17.Text = "Village/Panchayat";
            // 
            // txtState
            // 
            this.txtState.BackColor = System.Drawing.SystemColors.Info;
            this.txtState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtState.Location = new System.Drawing.Point(144, 103);
            this.txtState.MaxLength = 50;
            this.txtState.Name = "txtState";
            this.txtState.ReadOnly = true;
            this.txtState.Size = new System.Drawing.Size(239, 21);
            this.txtState.TabIndex = 59;
            this.txtState.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(101, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 15);
            this.label7.TabIndex = 60;
            this.label7.Text = "State";
            // 
            // txtDistrict
            // 
            this.txtDistrict.BackColor = System.Drawing.SystemColors.Info;
            this.txtDistrict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDistrict.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDistrict.Location = new System.Drawing.Point(144, 81);
            this.txtDistrict.MaxLength = 50;
            this.txtDistrict.Name = "txtDistrict";
            this.txtDistrict.ReadOnly = true;
            this.txtDistrict.Size = new System.Drawing.Size(239, 21);
            this.txtDistrict.TabIndex = 57;
            this.txtDistrict.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(89, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 58;
            this.label2.Text = "District";
            // 
            // txtMandal
            // 
            this.txtMandal.BackColor = System.Drawing.SystemColors.Info;
            this.txtMandal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMandal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMandal.Location = new System.Drawing.Point(144, 59);
            this.txtMandal.MaxLength = 50;
            this.txtMandal.Name = "txtMandal";
            this.txtMandal.ReadOnly = true;
            this.txtMandal.Size = new System.Drawing.Size(239, 21);
            this.txtMandal.TabIndex = 55;
            this.txtMandal.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(4, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 15);
            this.label3.TabIndex = 56;
            this.label3.Text = "Tahsil/Block/Mandal";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(517, 58);
            this.txtPhone.MaxLength = 20;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(219, 20);
            this.txtPhone.TabIndex = 3;
            this.txtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhone_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(464, 61);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 27;
            this.label1.Text = "Phone";
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActive.ForeColor = System.Drawing.Color.Crimson;
            this.chkActive.Location = new System.Drawing.Point(518, 130);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(70, 20);
            this.chkActive.TabIndex = 5;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.Click += new System.EventHandler(this.chkActive_Click);
            // 
            // txtCampAddress
            // 
            this.txtCampAddress.Location = new System.Drawing.Point(517, 105);
            this.txtCampAddress.MaxLength = 200;
            this.txtCampAddress.Multiline = true;
            this.txtCampAddress.Name = "txtCampAddress";
            this.txtCampAddress.Size = new System.Drawing.Size(219, 21);
            this.txtCampAddress.TabIndex = 4;
            this.txtCampAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCampAddress_KeyPress);
            // 
            // txtCampName
            // 
            this.txtCampName.Location = new System.Drawing.Point(516, 36);
            this.txtCampName.MaxLength = 100;
            this.txtCampName.Name = "txtCampName";
            this.txtCampName.Size = new System.Drawing.Size(220, 20);
            this.txtCampName.TabIndex = 2;
            this.txtCampName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCampName_KeyPress);
            // 
            // txtCampCode
            // 
            this.txtCampCode.Location = new System.Drawing.Point(146, 13);
            this.txtCampCode.MaxLength = 20;
            this.txtCampCode.Name = "txtCampCode";
            this.txtCampCode.Size = new System.Drawing.Size(112, 20);
            this.txtCampCode.TabIndex = 1;
            this.txtCampCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCampCode_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(470, 106);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 15);
            this.label6.TabIndex = 20;
            this.label6.Text = "Other";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(426, 39);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 15);
            this.label5.TabIndex = 19;
            this.label5.Text = "Camp Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(60, 16);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 15);
            this.label4.TabIndex = 18;
            this.label4.Text = "Camp Code";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Location = new System.Drawing.Point(8, 380);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(771, 45);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.Location = new System.Drawing.Point(382, 10);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(305, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(480, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(229, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // CampMasAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 435);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Name = "CampMasAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Camp  Add";
            this.Load += new System.EventHandler(this.CampMasAdd_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvCamp)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView gvCamp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.TextBox txtCampAddress;
        private System.Windows.Forms.TextBox txtCampName;
        private System.Windows.Forms.TextBox txtCampCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtVillage;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtDistrict;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtMandal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLandMark;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnVSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CampCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CampName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn District;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mandal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Village;
        private System.Windows.Forms.DataGridViewTextBoxColumn LandMark;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn Active;
        private System.Windows.Forms.DataGridViewTextBoxColumn Branch;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchCode;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
    }
}