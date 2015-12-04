namespace SDMS
{
    partial class DealerMapping
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbDesig = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gvEmpDetails = new System.Windows.Forms.DataGridView();
            this.SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desigId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ecode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DlName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desig = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Del = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnProductSearch = new System.Windows.Forms.Button();
            this.txtEcodeSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbEcode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpValidFromDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDealerSearch = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cbDealer = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbDesig);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnProductSearch);
            this.groupBox1.Controls.Add(this.txtEcodeSearch);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbEcode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpValidFromDate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDealerSearch);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.cbDealer);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Location = new System.Drawing.Point(3, -6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(728, 338);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cbDesig
            // 
            this.cbDesig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDesig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDesig.FormattingEnabled = true;
            this.cbDesig.Location = new System.Drawing.Point(64, 52);
            this.cbDesig.Name = "cbDesig";
            this.cbDesig.Size = new System.Drawing.Size(203, 23);
            this.cbDesig.TabIndex = 73;
            this.cbDesig.SelectedIndexChanged += new System.EventHandler(this.cbDesig_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(225, 292);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(279, 45);
            this.groupBox4.TabIndex = 72;
            this.groupBox4.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(179, 14);
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
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(102, 14);
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
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(27, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gvEmpDetails);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(5, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(719, 223);
            this.groupBox2.TabIndex = 71;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mapping Details";
            // 
            // gvEmpDetails
            // 
            this.gvEmpDetails.AllowUserToAddRows = false;
            this.gvEmpDetails.AllowUserToDeleteRows = false;
            this.gvEmpDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvEmpDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEmpDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SNo,
            this.desigId,
            this.Ecode,
            this.DlName,
            this.Desig,
            this.Del});
            this.gvEmpDetails.GridColor = System.Drawing.SystemColors.Info;
            this.gvEmpDetails.Location = new System.Drawing.Point(3, 23);
            this.gvEmpDetails.Name = "gvEmpDetails";
            this.gvEmpDetails.RowHeadersVisible = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvEmpDetails.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvEmpDetails.Size = new System.Drawing.Size(710, 193);
            this.gvEmpDetails.TabIndex = 0;
            this.gvEmpDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvEmpDetails_CellClick);
            // 
            // SNo
            // 
            this.SNo.HeaderText = "SNO";
            this.SNo.Name = "SNo";
            this.SNo.Width = 70;
            // 
            // desigId
            // 
            this.desigId.HeaderText = "desigId";
            this.desigId.Name = "desigId";
            this.desigId.Visible = false;
            // 
            // Ecode
            // 
            this.Ecode.HeaderText = "Ecode";
            this.Ecode.Name = "Ecode";
            this.Ecode.Width = 80;
            // 
            // DlName
            // 
            this.DlName.HeaderText = "Name";
            this.DlName.Name = "DlName";
            this.DlName.Width = 250;
            // 
            // Desig
            // 
            this.Desig.HeaderText = "Desig";
            this.Desig.Name = "Desig";
            this.Desig.Width = 250;
            // 
            // Del
            // 
            this.Del.HeaderText = "Del";
            this.Del.Image = global::SDMS.Properties.Resources.actions_delete;
            this.Del.Name = "Del";
            this.Del.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Del.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Del.Width = 50;
            // 
            // btnProductSearch
            // 
            this.btnProductSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnProductSearch.BackColor = System.Drawing.Color.YellowGreen;
            this.btnProductSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnProductSearch.FlatAppearance.BorderSize = 5;
            this.btnProductSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnProductSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProductSearch.ForeColor = System.Drawing.Color.Navy;
            this.btnProductSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProductSearch.Location = new System.Drawing.Point(659, 50);
            this.btnProductSearch.Margin = new System.Windows.Forms.Padding(1);
            this.btnProductSearch.Name = "btnProductSearch";
            this.btnProductSearch.Size = new System.Drawing.Size(62, 26);
            this.btnProductSearch.TabIndex = 70;
            this.btnProductSearch.Tag = "Product  Search";
            this.btnProductSearch.Text = "+ Add products";
            this.btnProductSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProductSearch.UseVisualStyleBackColor = false;
            this.btnProductSearch.Click += new System.EventHandler(this.btnProductSearch_Click);
            // 
            // txtEcodeSearch
            // 
            this.txtEcodeSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcodeSearch.Location = new System.Drawing.Point(327, 52);
            this.txtEcodeSearch.MaxLength = 20;
            this.txtEcodeSearch.Name = "txtEcodeSearch";
            this.txtEcodeSearch.Size = new System.Drawing.Size(86, 21);
            this.txtEcodeSearch.TabIndex = 67;
            this.txtEcodeSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEcodeSearch_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(273, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 69;
            this.label2.Text = "Ecode";
            // 
            // cbEcode
            // 
            this.cbEcode.AllowDrop = true;
            this.cbEcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbEcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEcode.FormattingEnabled = true;
            this.cbEcode.Location = new System.Drawing.Point(414, 51);
            this.cbEcode.Name = "cbEcode";
            this.cbEcode.Size = new System.Drawing.Size(241, 23);
            this.cbEcode.TabIndex = 68;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(9, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 53;
            this.label1.Text = "Desig";
            // 
            // dtpValidFromDate
            // 
            this.dtpValidFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpValidFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpValidFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpValidFromDate.Location = new System.Drawing.Point(587, 21);
            this.dtpValidFromDate.Name = "dtpValidFromDate";
            this.dtpValidFromDate.Size = new System.Drawing.Size(98, 23);
            this.dtpValidFromDate.TabIndex = 52;
            this.dtpValidFromDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(501, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 16);
            this.label3.TabIndex = 51;
            this.label3.Text = "Valid From";
            // 
            // txtDealerSearch
            // 
            this.txtDealerSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDealerSearch.Location = new System.Drawing.Point(63, 22);
            this.txtDealerSearch.MaxLength = 20;
            this.txtDealerSearch.Name = "txtDealerSearch";
            this.txtDealerSearch.Size = new System.Drawing.Size(86, 21);
            this.txtDealerSearch.TabIndex = 47;
            this.txtDealerSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDealerSearch_KeyUp);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label16.Location = new System.Drawing.Point(9, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 17);
            this.label16.TabIndex = 49;
            this.label16.Text = "Dealer";
            // 
            // cbDealer
            // 
            this.cbDealer.AllowDrop = true;
            this.cbDealer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbDealer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDealer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDealer.FormattingEnabled = true;
            this.cbDealer.Location = new System.Drawing.Point(150, 21);
            this.cbDealer.Name = "cbDealer";
            this.cbDealer.Size = new System.Drawing.Size(322, 23);
            this.cbDealer.TabIndex = 48;
            // 
            // DealerMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(733, 336);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "DealerMapping";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DealerMapping";
            this.Load += new System.EventHandler(this.DealerMapping_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDealerSearch;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbDealer;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DateTimePicker dtpValidFromDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEcodeSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbEcode;
        private System.Windows.Forms.Button btnProductSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView gvEmpDetails;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbDesig;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn desigId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ecode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DlName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Desig;
        private System.Windows.Forms.DataGridViewImageColumn Del;
    }
}