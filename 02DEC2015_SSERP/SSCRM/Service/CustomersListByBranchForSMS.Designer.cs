namespace SSCRM
{
    partial class CustomersListByBranchForSMS
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.cbBranch = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbState = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbDistrict = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbVillage = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbProduct = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbProdType = new System.Windows.Forms.ComboBox();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FarmerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FarmerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detl = new System.Windows.Forms.DataGridViewImageColumn();
            this.Select = new System.Windows.Forms.DataGridViewImageColumn();
            this.Del = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Company";
            // 
            // cbCompany
            // 
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Location = new System.Drawing.Point(81, 23);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(330, 24);
            this.cbCompany.TabIndex = 1;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // cbBranch
            // 
            this.cbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranch.FormattingEnabled = true;
            this.cbBranch.Location = new System.Drawing.Point(496, 23);
            this.cbBranch.Name = "cbBranch";
            this.cbBranch.Size = new System.Drawing.Size(320, 24);
            this.cbBranch.TabIndex = 3;
            this.cbBranch.SelectedIndexChanged += new System.EventHandler(this.cbBranch_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(442, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Branch";
            // 
            // cbState
            // 
            this.cbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbState.FormattingEnabled = true;
            this.cbState.Location = new System.Drawing.Point(80, 53);
            this.cbState.Name = "cbState";
            this.cbState.Size = new System.Drawing.Size(200, 24);
            this.cbState.TabIndex = 5;
            this.cbState.SelectedIndexChanged += new System.EventHandler(this.cbState_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "State";
            // 
            // cbDistrict
            // 
            this.cbDistrict.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDistrict.FormattingEnabled = true;
            this.cbDistrict.Location = new System.Drawing.Point(349, 53);
            this.cbDistrict.Name = "cbDistrict";
            this.cbDistrict.Size = new System.Drawing.Size(200, 24);
            this.cbDistrict.TabIndex = 7;
            this.cbDistrict.SelectedIndexChanged += new System.EventHandler(this.cbDistrict_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(301, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "District";
            // 
            // cbVillage
            // 
            this.cbVillage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVillage.FormattingEnabled = true;
            this.cbVillage.Location = new System.Drawing.Point(616, 53);
            this.cbVillage.Name = "cbVillage";
            this.cbVillage.Size = new System.Drawing.Size(200, 24);
            this.cbVillage.TabIndex = 9;
            this.cbVillage.SelectedIndexChanged += new System.EventHandler(this.cbVillage_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(567, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Village";
            // 
            // cbProduct
            // 
            this.cbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProduct.FormattingEnabled = true;
            this.cbProduct.Location = new System.Drawing.Point(174, 82);
            this.cbProduct.Name = "cbProduct";
            this.cbProduct.Size = new System.Drawing.Size(375, 24);
            this.cbProduct.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Product";
            // 
            // cbProdType
            // 
            this.cbProdType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProdType.FormattingEnabled = true;
            this.cbProdType.Location = new System.Drawing.Point(80, 82);
            this.cbProdType.Name = "cbProdType";
            this.cbProdType.Size = new System.Drawing.Size(91, 24);
            this.cbProdType.TabIndex = 12;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "MMMyyyy";
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(616, 85);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(82, 22);
            this.dtpFrom.TabIndex = 13;
            this.dtpFrom.Value = new System.DateTime(2014, 2, 10, 0, 0, 0, 0);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(578, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "From";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(708, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 16);
            this.label8.TabIndex = 16;
            this.label8.Text = "To";
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "MMMyyyy";
            this.dtpTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(733, 85);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(83, 22);
            this.dtpTo.TabIndex = 15;
            this.dtpTo.Value = new System.DateTime(2014, 2, 10, 0, 0, 0, 0);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SlNo,
            this.OrderNo,
            this.InvNo,
            this.InvDate,
            this.FarmerID,
            this.FarmerName,
            this.Mobile,
            this.Qty,
            this.Detl,
            this.Select,
            this.Del});
            this.dataGridView1.Location = new System.Drawing.Point(17, 121);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(809, 337);
            this.dataGridView1.TabIndex = 17;
            // 
            // SlNo
            // 
            this.SlNo.HeaderText = "SlNo";
            this.SlNo.Name = "SlNo";
            this.SlNo.Width = 40;
            // 
            // OrderNo
            // 
            this.OrderNo.HeaderText = "OrderNo";
            this.OrderNo.Name = "OrderNo";
            this.OrderNo.ReadOnly = true;
            this.OrderNo.Width = 70;
            // 
            // InvNo
            // 
            this.InvNo.HeaderText = "InvNo";
            this.InvNo.Name = "InvNo";
            this.InvNo.Width = 70;
            // 
            // InvDate
            // 
            this.InvDate.HeaderText = "InvDate";
            this.InvDate.Name = "InvDate";
            // 
            // FarmerID
            // 
            this.FarmerID.HeaderText = "FarmerID";
            this.FarmerID.Name = "FarmerID";
            this.FarmerID.Visible = false;
            // 
            // FarmerName
            // 
            this.FarmerName.HeaderText = "Farmer Name";
            this.FarmerName.Name = "FarmerName";
            this.FarmerName.Width = 250;
            // 
            // Mobile
            // 
            this.Mobile.HeaderText = "Mobile";
            this.Mobile.Name = "Mobile";
            // 
            // Qty
            // 
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.Width = 60;
            // 
            // Detl
            // 
            this.Detl.HeaderText = "Detl";
            this.Detl.Name = "Detl";
            this.Detl.ReadOnly = true;
            this.Detl.Width = 30;
            // 
            // Select
            // 
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.ReadOnly = true;
            this.Select.Width = 30;
            // 
            // Del
            // 
            this.Del.HeaderText = "Del";
            this.Del.Name = "Del";
            this.Del.ReadOnly = true;
            this.Del.Width = 30;
            // 
            // btnExit
            // 
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(174, 486);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 26;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(94, 486);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 26);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.AutoEllipsis = true;
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(19, 486);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 24;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // CustomersListByBranchForSMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 530);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.cbProdType);
            this.Controls.Add(this.cbProduct);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbVillage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbDistrict);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbState);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbBranch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbCompany);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CustomersListByBranchForSMS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customers List";
            this.Load += new System.EventHandler(this.CustomersListByBranchForSMS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.ComboBox cbBranch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbState;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbDistrict;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbVillage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbProduct;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbProdType;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn FarmerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FarmerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewImageColumn Detl;
        private System.Windows.Forms.DataGridViewImageColumn Select;
        private System.Windows.Forms.DataGridViewImageColumn Del;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}