namespace SSCRM
{
    partial class SRAdoption
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbEmployees = new System.Windows.Forms.ComboBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBranch = new System.Windows.Forms.TextBox();
            this.dtpDocMonth = new System.Windows.Forms.DateTimePicker();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gvAdoptionDetails = new System.Windows.Forms.DataGridView();
            this.GroupSlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SRslno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SRCamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SREcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GCEcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompanyCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Designation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Doj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Los = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PreviousMP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADPStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Adoptionchk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.label21 = new System.Windows.Forms.Label();
            this.txtEcode = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.btnPrintAdoptionList = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvAdoptionDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.btnPrintAdoptionList);
            this.groupBox1.Controls.Add(this.cbEmployees);
            this.groupBox1.Controls.Add(this.lblSearch);
            this.groupBox1.Controls.Add(this.txtSearch);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCompany);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtBranch);
            this.groupBox1.Controls.Add(this.dtpDocMonth);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.gvAdoptionDetails);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.txtEcode);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(2, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(936, 517);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SR Adoption Details";
            // 
            // cbEmployees
            // 
            this.cbEmployees.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEmployees.FormattingEnabled = true;
            this.cbEmployees.Location = new System.Drawing.Point(531, 26);
            this.cbEmployees.Name = "cbEmployees";
            this.cbEmployees.Size = new System.Drawing.Size(326, 23);
            this.cbEmployees.TabIndex = 143;
            this.cbEmployees.SelectedIndexChanged += new System.EventHandler(this.cbEmployees_SelectedIndexChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblSearch.Location = new System.Drawing.Point(701, 82);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(102, 16);
            this.lblSearch.TabIndex = 142;
            this.lblSearch.Text = "SearchEcode";
            this.lblSearch.Visible = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(809, 79);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(121, 23);
            this.txtSearch.TabIndex = 141;
            this.txtSearch.Visible = false;
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(10, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 16);
            this.label2.TabIndex = 140;
            this.label2.Text = "GC And SR Details";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(96, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 139;
            this.label1.Text = "Company";
            // 
            // txtCompany
            // 
            this.txtCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompany.Location = new System.Drawing.Point(170, 56);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.ReadOnly = true;
            this.txtCompany.Size = new System.Drawing.Size(289, 22);
            this.txtCompany.TabIndex = 138;
            this.txtCompany.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(467, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 137;
            this.label6.Text = "Branch";
            // 
            // txtBranch
            // 
            this.txtBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBranch.Location = new System.Drawing.Point(527, 54);
            this.txtBranch.Name = "txtBranch";
            this.txtBranch.ReadOnly = true;
            this.txtBranch.Size = new System.Drawing.Size(331, 22);
            this.txtBranch.TabIndex = 136;
            this.txtBranch.TabStop = false;
            // 
            // dtpDocMonth
            // 
            this.dtpDocMonth.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDocMonth.CustomFormat = "MMM/yyyy";
            this.dtpDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDocMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDocMonth.Location = new System.Drawing.Point(176, 25);
            this.dtpDocMonth.Name = "dtpDocMonth";
            this.dtpDocMonth.Size = new System.Drawing.Size(93, 22);
            this.dtpDocMonth.TabIndex = 134;
            this.dtpDocMonth.Value = new System.DateTime(2015, 4, 1, 0, 0, 0, 0);
            this.dtpDocMonth.ValueChanged += new System.EventHandler(this.dtpDocMonth_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnClear);
            this.groupBox4.Controls.Add(this.btnClose);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Location = new System.Drawing.Point(335, 464);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(266, 45);
            this.groupBox4.TabIndex = 72;
            this.groupBox4.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClear.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClear.Location = new System.Drawing.Point(102, 15);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(74, 26);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(182, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 26);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "C&lose";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoEllipsis = true;
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(21, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gvAdoptionDetails
            // 
            this.gvAdoptionDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvAdoptionDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvAdoptionDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvAdoptionDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvAdoptionDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvAdoptionDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvAdoptionDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GroupSlNo,
            this.SRslno,
            this.SLNO,
            this.SRCamp,
            this.SREcode,
            this.GCEcode,
            this.Ename,
            this.CompanyCode,
            this.BranchCode,
            this.Designation,
            this.Doj,
            this.Los,
            this.PreviousMP,
            this.ADPStatus,
            this.Adoptionchk,
            this.Delete});
            this.gvAdoptionDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvAdoptionDetails.Location = new System.Drawing.Point(10, 105);
            this.gvAdoptionDetails.MultiSelect = false;
            this.gvAdoptionDetails.Name = "gvAdoptionDetails";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvAdoptionDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvAdoptionDetails.RowHeadersVisible = false;
            this.gvAdoptionDetails.Size = new System.Drawing.Size(920, 353);
            this.gvAdoptionDetails.TabIndex = 71;
            this.gvAdoptionDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvAdoptionDetails_CellContentClick);
            // 
            // GroupSlNo
            // 
            this.GroupSlNo.Frozen = true;
            this.GroupSlNo.HeaderText = "GSlno";
            this.GroupSlNo.Name = "GroupSlNo";
            this.GroupSlNo.Visible = false;
            // 
            // SRslno
            // 
            this.SRslno.Frozen = true;
            this.SRslno.HeaderText = "SRslno";
            this.SRslno.Name = "SRslno";
            this.SRslno.Visible = false;
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
            // SRCamp
            // 
            this.SRCamp.Frozen = true;
            this.SRCamp.HeaderText = "Camp";
            this.SRCamp.Name = "SRCamp";
            this.SRCamp.Width = 140;
            // 
            // SREcode
            // 
            this.SREcode.Frozen = true;
            this.SREcode.HeaderText = "Ecode";
            this.SREcode.Name = "SREcode";
            this.SREcode.Width = 70;
            // 
            // GCEcode
            // 
            this.GCEcode.Frozen = true;
            this.GCEcode.HeaderText = "GCcode";
            this.GCEcode.Name = "GCEcode";
            this.GCEcode.Visible = false;
            // 
            // Ename
            // 
            this.Ename.Frozen = true;
            this.Ename.HeaderText = "Name";
            this.Ename.Name = "Ename";
            this.Ename.ReadOnly = true;
            this.Ename.Width = 210;
            // 
            // CompanyCode
            // 
            this.CompanyCode.Frozen = true;
            this.CompanyCode.HeaderText = "Company Code";
            this.CompanyCode.MinimumWidth = 20;
            this.CompanyCode.Name = "CompanyCode";
            this.CompanyCode.ReadOnly = true;
            this.CompanyCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CompanyCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CompanyCode.Visible = false;
            this.CompanyCode.Width = 250;
            // 
            // BranchCode
            // 
            this.BranchCode.Frozen = true;
            this.BranchCode.HeaderText = "Branch Code";
            this.BranchCode.Name = "BranchCode";
            this.BranchCode.ReadOnly = true;
            this.BranchCode.Visible = false;
            this.BranchCode.Width = 200;
            // 
            // Designation
            // 
            this.Designation.Frozen = true;
            this.Designation.HeaderText = "Desig";
            this.Designation.Name = "Designation";
            this.Designation.ReadOnly = true;
            this.Designation.Width = 80;
            // 
            // Doj
            // 
            this.Doj.Frozen = true;
            this.Doj.HeaderText = "DOJ";
            this.Doj.Name = "Doj";
            // 
            // Los
            // 
            dataGridViewCellStyle3.Format = "0.00";
            dataGridViewCellStyle3.NullValue = null;
            this.Los.DefaultCellStyle = dataGridViewCellStyle3;
            this.Los.Frozen = true;
            this.Los.HeaderText = "LOS";
            this.Los.Name = "Los";
            this.Los.Width = 70;
            // 
            // PreviousMP
            // 
            this.PreviousMP.Frozen = true;
            this.PreviousMP.HeaderText = "Previous Month Points";
            this.PreviousMP.Name = "PreviousMP";
            this.PreviousMP.Width = 80;
            // 
            // ADPStatus
            // 
            this.ADPStatus.HeaderText = "Status";
            this.ADPStatus.Name = "ADPStatus";
            this.ADPStatus.Visible = false;
            // 
            // Adoptionchk
            // 
            this.Adoptionchk.HeaderText = "Adop tion";
            this.Adoptionchk.Name = "Adoptionchk";
            this.Adoptionchk.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Adoptionchk.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Adoptionchk.Width = 60;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Del";
            this.Delete.Image = global::SSCRM.Properties.Resources.ic_delete;
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Width = 40;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label21.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label21.Location = new System.Drawing.Point(92, 29);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(77, 16);
            this.label21.TabIndex = 69;
            this.label21.Text = "DocMonth";
            // 
            // txtEcode
            // 
            this.txtEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcode.Location = new System.Drawing.Point(437, 26);
            this.txtEcode.MaxLength = 20;
            this.txtEcode.Name = "txtEcode";
            this.txtEcode.Size = new System.Drawing.Size(89, 21);
            this.txtEcode.TabIndex = 67;
            this.txtEcode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEcode_KeyUp);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label34.Location = new System.Drawing.Point(286, 29);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(145, 17);
            this.label34.TabIndex = 66;
            this.label34.Text = "TM && Above Ecode";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Azure;
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label23.Location = new System.Drawing.Point(410, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(101, 22);
            this.label23.TabIndex = 63;
            this.label23.Text = "ADOPTION";
            // 
            // btnPrintAdoptionList
            // 
            this.btnPrintAdoptionList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPrintAdoptionList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnPrintAdoptionList.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnPrintAdoptionList.FlatAppearance.BorderSize = 5;
            this.btnPrintAdoptionList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrintAdoptionList.Image = global::SSCRM.Properties.Resources.ic_print;
            this.btnPrintAdoptionList.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnPrintAdoptionList.Location = new System.Drawing.Point(861, 26);
            this.btnPrintAdoptionList.Margin = new System.Windows.Forms.Padding(1);
            this.btnPrintAdoptionList.Name = "btnPrintAdoptionList";
            this.btnPrintAdoptionList.Size = new System.Drawing.Size(29, 24);
            this.btnPrintAdoptionList.TabIndex = 144;
            this.btnPrintAdoptionList.Tag = "Product  Search";
            this.btnPrintAdoptionList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintAdoptionList.UseVisualStyleBackColor = false;
            this.btnPrintAdoptionList.Click += new System.EventHandler(this.btnPrintAdoptionList_Click);
            // 
            // SRAdoption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 527);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Name = "SRAdoption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SRAdoption";
            this.Load += new System.EventHandler(this.SRAdoption_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SRAdoption_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvAdoptionDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox txtEcode;
        private System.Windows.Forms.Label label21;
        public System.Windows.Forms.DataGridView gvAdoptionDetails;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker dtpDocMonth;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBranch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ComboBox cbEmployees;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupSlNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SRslno;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SRCamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn SREcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn GCEcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ename;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Designation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Doj;
        private System.Windows.Forms.DataGridViewTextBoxColumn Los;
        private System.Windows.Forms.DataGridViewTextBoxColumn PreviousMP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADPStatus;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Adoptionchk;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private System.Windows.Forms.Button btnPrintAdoptionList;
    }
}