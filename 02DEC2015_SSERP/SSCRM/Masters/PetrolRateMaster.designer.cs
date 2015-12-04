namespace SSCRM
{
    partial class PetrolRateMaster
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnApprove = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gvPetrolDetails = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompanyCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompanyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StateCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StateName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EffDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PetrolQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApprovalFlg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.CbState = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpEffDate = new System.Windows.Forms.DateTimePicker();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.lblCompanyCode = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPetrolDetails)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.btnApprove);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.CbState);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.dtpEffDate);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.lblCompanyCode);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.groupBox1.Location = new System.Drawing.Point(4, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(686, 467);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // btnApprove
            // 
            this.btnApprove.BackColor = System.Drawing.Color.YellowGreen;
            this.btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnApprove.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnApprove.Location = new System.Drawing.Point(492, 426);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(96, 24);
            this.btnApprove.TabIndex = 138;
            this.btnApprove.Text = "A&pprove";
            this.btnApprove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnApprove.UseVisualStyleBackColor = false;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gvPetrolDetails);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox3.ForeColor = System.Drawing.Color.Navy;
            this.groupBox3.Location = new System.Drawing.Point(7, 77);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(672, 338);
            this.groupBox3.TabIndex = 137;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Petrol Details";
            // 
            // gvPetrolDetails
            // 
            this.gvPetrolDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvPetrolDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvPetrolDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvPetrolDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPetrolDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvPetrolDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPetrolDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.CompanyCode,
            this.CompanyName,
            this.StateCode,
            this.StateName,
            this.BranchCode,
            this.BranchName,
            this.EffDate,
            this.PetrolQty,
            this.Price,
            this.ApprovalFlg,
            this.Edit});
            this.gvPetrolDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvPetrolDetails.Location = new System.Drawing.Point(6, 21);
            this.gvPetrolDetails.MultiSelect = false;
            this.gvPetrolDetails.Name = "gvPetrolDetails";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPetrolDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvPetrolDetails.RowHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Blue;
            this.gvPetrolDetails.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvPetrolDetails.Size = new System.Drawing.Size(657, 307);
            this.gvPetrolDetails.TabIndex = 91;
            this.gvPetrolDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPetrolDetails_CellClick);
            // 
            // SLNO
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.SLNO.DefaultCellStyle = dataGridViewCellStyle3;
            this.SLNO.Frozen = true;
            this.SLNO.HeaderText = "Sl.No";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SLNO.Width = 50;
            // 
            // CompanyCode
            // 
            this.CompanyCode.HeaderText = "CompanyCode";
            this.CompanyCode.Name = "CompanyCode";
            this.CompanyCode.Visible = false;
            // 
            // CompanyName
            // 
            this.CompanyName.HeaderText = "CompanyName";
            this.CompanyName.Name = "CompanyName";
            this.CompanyName.Visible = false;
            // 
            // StateCode
            // 
            this.StateCode.HeaderText = "StateCode";
            this.StateCode.Name = "StateCode";
            this.StateCode.Visible = false;
            // 
            // StateName
            // 
            this.StateName.HeaderText = "StateName";
            this.StateName.Name = "StateName";
            this.StateName.Visible = false;
            // 
            // BranchCode
            // 
            this.BranchCode.HeaderText = "BranchCode";
            this.BranchCode.Name = "BranchCode";
            this.BranchCode.Visible = false;
            // 
            // BranchName
            // 
            this.BranchName.HeaderText = "Branch Name";
            this.BranchName.Name = "BranchName";
            this.BranchName.Width = 350;
            // 
            // EffDate
            // 
            this.EffDate.HeaderText = "EffDate";
            this.EffDate.Name = "EffDate";
            this.EffDate.Visible = false;
            // 
            // PetrolQty
            // 
            this.PetrolQty.HeaderText = "Litre";
            this.PetrolQty.Name = "PetrolQty";
            this.PetrolQty.ReadOnly = true;
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            // 
            // ApprovalFlg
            // 
            this.ApprovalFlg.HeaderText = "ApprovalFlg";
            this.ApprovalFlg.Name = "ApprovalFlg";
            this.ApprovalFlg.Visible = false;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "";
            this.Edit.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.Edit.Name = "Edit";
            this.Edit.Width = 30;
            // 
            // CbState
            // 
            this.CbState.BackColor = System.Drawing.SystemColors.Info;
            this.CbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbState.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CbState.ForeColor = System.Drawing.SystemColors.MenuText;
            this.CbState.FormattingEnabled = true;
            this.CbState.Location = new System.Drawing.Point(457, 43);
            this.CbState.Name = "CbState";
            this.CbState.Size = new System.Drawing.Size(221, 26);
            this.CbState.TabIndex = 135;
            this.CbState.SelectedIndexChanged += new System.EventHandler(this.CbState_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(415, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 17);
            this.label1.TabIndex = 136;
            this.label1.Text = "State";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Navy;
            this.label9.Location = new System.Drawing.Point(23, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 16);
            this.label9.TabIndex = 134;
            this.label9.Text = "WEF Date";
            // 
            // dtpEffDate
            // 
            this.dtpEffDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEffDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpEffDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEffDate.Location = new System.Drawing.Point(105, 15);
            this.dtpEffDate.Name = "dtpEffDate";
            this.dtpEffDate.Size = new System.Drawing.Size(100, 22);
            this.dtpEffDate.TabIndex = 133;
            this.dtpEffDate.Value = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            this.dtpEffDate.ValueChanged += new System.EventHandler(this.dtpEffDate_ValueChanged);
            // 
            // cbCompany
            // 
            this.cbCompany.BackColor = System.Drawing.SystemColors.Info;
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.ForeColor = System.Drawing.SystemColors.MenuText;
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Location = new System.Drawing.Point(75, 43);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(328, 26);
            this.cbCompany.TabIndex = 89;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // lblCompanyCode
            // 
            this.lblCompanyCode.AutoSize = true;
            this.lblCompanyCode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyCode.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblCompanyCode.Location = new System.Drawing.Point(9, 46);
            this.lblCompanyCode.Name = "lblCompanyCode";
            this.lblCompanyCode.Size = new System.Drawing.Size(66, 17);
            this.lblCompanyCode.TabIndex = 90;
            this.lblCompanyCode.Text = "Company";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Location = new System.Drawing.Point(145, 413);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(309, 45);
            this.groupBox2.TabIndex = 93;
            this.groupBox2.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.AutoEllipsis = true;
            this.btnClear.BackColor = System.Drawing.Color.White;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.btnClear.FlatAppearance.BorderSize = 5;
            this.btnClear.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClear.Location = new System.Drawing.Point(116, 14);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(81, 25);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "C&lear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.AutoEllipsis = true;
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(203, 14);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 25);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "C&lose";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoEllipsis = true;
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.btnSave.FlatAppearance.BorderSize = 5;
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(32, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 25);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 30;
            // 
            // PetrolRateMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(691, 469);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "PetrolRateMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Petrol Rate Master(Price/Litre)";
            this.Load += new System.EventHandler(this.PetrolRateMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvPetrolDetails)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.DataGridView gvPetrolDetails;
        private System.Windows.Forms.ComboBox CbState;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpEffDate;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label lblCompanyCode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StateCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn StateName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EffDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PetrolQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApprovalFlg;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
    }
}