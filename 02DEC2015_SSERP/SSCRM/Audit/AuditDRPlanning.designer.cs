namespace SSCRM
{
    partial class AuditDRPlanning
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAddEmpDRDetails = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromdate = new System.Windows.Forms.DateTimePicker();
            this.txtDept = new System.Windows.Forms.TextBox();
            this.txtEmpDesg = new System.Windows.Forms.TextBox();
            this.lblDesg = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbAuditEmployees = new System.Windows.Forms.ComboBox();
            this.txtEcodeSearch = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.gvAuditEmpPlanningDetl = new System.Windows.Forms.DataGridView();
            this.SlNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Del = new System.Windows.Forms.DataGridViewImageColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvAuditEmpPlanningDetl)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.btnAddEmpDRDetails);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.dtpFromdate);
            this.groupBox1.Controls.Add(this.txtDept);
            this.groupBox1.Controls.Add(this.txtEmpDesg);
            this.groupBox1.Controls.Add(this.lblDesg);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbAuditEmployees);
            this.groupBox1.Controls.Add(this.txtEcodeSearch);
            this.groupBox1.Controls.Add(this.lblMessage);
            this.groupBox1.Controls.Add(this.gvAuditEmpPlanningDetl);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(805, 501);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(213, 447);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 45);
            this.groupBox4.TabIndex = 173;
            this.groupBox4.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(229, 14);
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
            this.btnCancel.Location = new System.Drawing.Point(152, 14);
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
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(75, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAddEmpDRDetails
            // 
            this.btnAddEmpDRDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddEmpDRDetails.BackColor = System.Drawing.Color.YellowGreen;
            this.btnAddEmpDRDetails.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnAddEmpDRDetails.FlatAppearance.BorderSize = 5;
            this.btnAddEmpDRDetails.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddEmpDRDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddEmpDRDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddEmpDRDetails.Location = new System.Drawing.Point(741, 94);
            this.btnAddEmpDRDetails.Margin = new System.Windows.Forms.Padding(1);
            this.btnAddEmpDRDetails.Name = "btnAddEmpDRDetails";
            this.btnAddEmpDRDetails.Size = new System.Drawing.Size(52, 27);
            this.btnAddEmpDRDetails.TabIndex = 11;
            this.btnAddEmpDRDetails.Tag = "";
            this.btnAddEmpDRDetails.Text = "+&Add";
            this.btnAddEmpDRDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddEmpDRDetails.UseVisualStyleBackColor = false;
            this.btnAddEmpDRDetails.Click += new System.EventHandler(this.btnAddRecoveryDetl_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(8, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(275, 16);
            this.label4.TabIndex = 172;
            this.label4.Text = "Employee Audit Tour Schedule Details";
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(335, 72);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(96, 22);
            this.dtpToDate.TabIndex = 10;
            this.dtpToDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpToDate.ValueChanged += new System.EventHandler(this.dtpToDate_ValueChanged);
            // 
            // dtpFromdate
            // 
            this.dtpFromdate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromdate.Location = new System.Drawing.Point(89, 69);
            this.dtpFromdate.Name = "dtpFromdate";
            this.dtpFromdate.Size = new System.Drawing.Size(96, 22);
            this.dtpFromdate.TabIndex = 8;
            this.dtpFromdate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpFromdate.ValueChanged += new System.EventHandler(this.dtpFromdate_ValueChanged);
            // 
            // txtDept
            // 
            this.txtDept.BackColor = System.Drawing.SystemColors.Info;
            this.txtDept.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDept.Location = new System.Drawing.Point(88, 42);
            this.txtDept.MaxLength = 50;
            this.txtDept.Name = "txtDept";
            this.txtDept.ReadOnly = true;
            this.txtDept.Size = new System.Drawing.Size(188, 22);
            this.txtDept.TabIndex = 4;
            this.txtDept.TabStop = false;
            // 
            // txtEmpDesg
            // 
            this.txtEmpDesg.BackColor = System.Drawing.SystemColors.Info;
            this.txtEmpDesg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpDesg.Location = new System.Drawing.Point(334, 43);
            this.txtEmpDesg.MaxLength = 50;
            this.txtEmpDesg.Name = "txtEmpDesg";
            this.txtEmpDesg.ReadOnly = true;
            this.txtEmpDesg.Size = new System.Drawing.Size(217, 22);
            this.txtEmpDesg.TabIndex = 6;
            this.txtEmpDesg.TabStop = false;
            // 
            // lblDesg
            // 
            this.lblDesg.AutoSize = true;
            this.lblDesg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesg.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblDesg.Location = new System.Drawing.Point(282, 46);
            this.lblDesg.Name = "lblDesg";
            this.lblDesg.Size = new System.Drawing.Size(49, 17);
            this.lblDesg.TabIndex = 5;
            this.lblDesg.Text = "Desig";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(34, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ecode";
            // 
            // cbAuditEmployees
            // 
            this.cbAuditEmployees.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAuditEmployees.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAuditEmployees.FormattingEnabled = true;
            this.cbAuditEmployees.Location = new System.Drawing.Point(163, 14);
            this.cbAuditEmployees.Name = "cbAuditEmployees";
            this.cbAuditEmployees.Size = new System.Drawing.Size(388, 24);
            this.cbAuditEmployees.TabIndex = 2;
            this.cbAuditEmployees.SelectedIndexChanged += new System.EventHandler(this.cbAuditEmployees_SelectedIndexChanged);
            // 
            // txtEcodeSearch
            // 
            this.txtEcodeSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcodeSearch.Location = new System.Drawing.Point(88, 15);
            this.txtEcodeSearch.Name = "txtEcodeSearch";
            this.txtEcodeSearch.Size = new System.Drawing.Size(72, 23);
            this.txtEcodeSearch.TabIndex = 1;
            this.txtEcodeSearch.TextChanged += new System.EventHandler(this.txtEcodeSearch_TextChanged);
            this.txtEcodeSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEcodeSearch_KeyUp);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblMessage.Location = new System.Drawing.Point(43, 45);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(41, 16);
            this.lblMessage.TabIndex = 3;
            this.lblMessage.Text = "Dept";
            // 
            // gvAuditEmpPlanningDetl
            // 
            this.gvAuditEmpPlanningDetl.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvAuditEmpPlanningDetl.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvAuditEmpPlanningDetl.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvAuditEmpPlanningDetl.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvAuditEmpPlanningDetl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvAuditEmpPlanningDetl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvAuditEmpPlanningDetl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SlNO,
            this.CompCode,
            this.LocationCode,
            this.BranchType,
            this.Date,
            this.Location,
            this.Remarks,
            this.Edit,
            this.Del});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvAuditEmpPlanningDetl.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvAuditEmpPlanningDetl.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvAuditEmpPlanningDetl.Location = new System.Drawing.Point(9, 123);
            this.gvAuditEmpPlanningDetl.MultiSelect = false;
            this.gvAuditEmpPlanningDetl.Name = "gvAuditEmpPlanningDetl";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvAuditEmpPlanningDetl.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvAuditEmpPlanningDetl.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Navy;
            this.gvAuditEmpPlanningDetl.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvAuditEmpPlanningDetl.Size = new System.Drawing.Size(784, 324);
            this.gvAuditEmpPlanningDetl.TabIndex = 12;
            this.gvAuditEmpPlanningDetl.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvAuditEmpPlanningDetl_CellClick);
            // 
            // SlNO
            // 
            this.SlNO.HeaderText = "Sl.No";
            this.SlNO.Name = "SlNO";
            this.SlNO.ReadOnly = true;
            this.SlNO.Width = 50;
            // 
            // CompCode
            // 
            this.CompCode.HeaderText = "Comp Code";
            this.CompCode.Name = "CompCode";
            this.CompCode.ReadOnly = true;
            this.CompCode.Visible = false;
            // 
            // LocationCode
            // 
            this.LocationCode.HeaderText = "Location Code";
            this.LocationCode.Name = "LocationCode";
            this.LocationCode.ReadOnly = true;
            this.LocationCode.Visible = false;
            // 
            // BranchType
            // 
            this.BranchType.HeaderText = "Trn Type";
            this.BranchType.Name = "BranchType";
            this.BranchType.ReadOnly = true;
            this.BranchType.Visible = false;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // Location
            // 
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            this.Location.ReadOnly = true;
            this.Location.Width = 300;
            // 
            // Remarks
            // 
            this.Remarks.HeaderText = "Remarks";
            this.Remarks.Name = "Remarks";
            this.Remarks.ReadOnly = true;
            this.Remarks.Width = 220;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Edit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Edit.Width = 40;
            // 
            // Del
            // 
            this.Del.HeaderText = "Del";
            this.Del.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.Del.Name = "Del";
            this.Del.Width = 40;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(268, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "To Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(7, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "From Date";
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Edit";
            this.dataGridViewImageColumn1.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.Width = 40;
            // 
            // AuditDRPlanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(811, 507);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AuditDRPlanning";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Emp Audit Tour Schedule";
            this.Load += new System.EventHandler(this.AuditDRPlanning_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvAuditEmpPlanningDetl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView gvAuditEmpPlanningDetl;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ComboBox cbAuditEmployees;
        private System.Windows.Forms.TextBox txtEcodeSearch;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtDept;
        public System.Windows.Forms.TextBox txtEmpDesg;
        private System.Windows.Forms.Label lblDesg;
        private System.Windows.Forms.DateTimePicker dtpFromdate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAddEmpDRDetails;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Del;
    }
}