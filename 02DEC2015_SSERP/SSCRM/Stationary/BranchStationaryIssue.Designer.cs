namespace SSCRM
{
    partial class BranchStationaryIssue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BranchStationaryIssue));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmpName = new System.Windows.Forms.TextBox();
            this.dtpTrnDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEcode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTrnNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClearProd = new System.Windows.Forms.Button();
            this.btnProductSearch = new System.Windows.Forms.Button();
            this.gvStatItemsDetails = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AvailableQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DBRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DBPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvStatItemsDetails)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(651, 517);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stationary Issue";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.Controls.Add(this.txtRemarks);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtEmpName);
            this.groupBox2.Controls.Add(this.dtpTrnDate);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtEcode);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtTrnNo);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(5, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(640, 90);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(268, 42);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(364, 38);
            this.txtRemarks.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(197, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Remarks";
            // 
            // txtEmpName
            // 
            this.txtEmpName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtEmpName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpName.Location = new System.Drawing.Point(342, 17);
            this.txtEmpName.Name = "txtEmpName";
            this.txtEmpName.Size = new System.Drawing.Size(290, 22);
            this.txtEmpName.TabIndex = 6;
            // 
            // dtpTrnDate
            // 
            this.dtpTrnDate.CustomFormat = "dd/MM/yyyy";
            this.dtpTrnDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTrnDate.Location = new System.Drawing.Point(72, 42);
            this.dtpTrnDate.Name = "dtpTrnDate";
            this.dtpTrnDate.Size = new System.Drawing.Size(103, 22);
            this.dtpTrnDate.TabIndex = 5;
            this.dtpTrnDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(6, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Trn Date";
            // 
            // txtEcode
            // 
            this.txtEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcode.Location = new System.Drawing.Point(268, 17);
            this.txtEcode.Name = "txtEcode";
            this.txtEcode.Size = new System.Drawing.Size(71, 22);
            this.txtEcode.TabIndex = 3;
            this.txtEcode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEcode_KeyUp);
            this.txtEcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEcode_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(214, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ecode";
            // 
            // txtTrnNo
            // 
            this.txtTrnNo.Location = new System.Drawing.Point(73, 17);
            this.txtTrnNo.Name = "txtTrnNo";
            this.txtTrnNo.Size = new System.Drawing.Size(102, 22);
            this.txtTrnNo.TabIndex = 1;
            this.txtTrnNo.Validated += new System.EventHandler(this.txtTrnNo_Validated);
            this.txtTrnNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTrnNo_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(19, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trn No";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.btnClearProd);
            this.groupBox3.Controls.Add(this.btnProductSearch);
            this.groupBox3.Controls.Add(this.gvStatItemsDetails);
            this.groupBox3.Location = new System.Drawing.Point(5, 107);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(640, 362);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Issued Items";
            // 
            // btnClearProd
            // 
            this.btnClearProd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClearProd.BackColor = System.Drawing.Color.Moccasin;
            this.btnClearProd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClearProd.FlatAppearance.BorderSize = 5;
            this.btnClearProd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearProd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClearProd.Location = new System.Drawing.Point(362, 13);
            this.btnClearProd.Margin = new System.Windows.Forms.Padding(1);
            this.btnClearProd.Name = "btnClearProd";
            this.btnClearProd.Size = new System.Drawing.Size(123, 24);
            this.btnClearProd.TabIndex = 21;
            this.btnClearProd.Tag = "Product  Search";
            this.btnClearProd.Text = "Cl&ear Products";
            this.btnClearProd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearProd.UseVisualStyleBackColor = false;
            this.btnClearProd.Click += new System.EventHandler(this.btnClearProd_Click);
            // 
            // btnProductSearch
            // 
            this.btnProductSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnProductSearch.BackColor = System.Drawing.Color.YellowGreen;
            this.btnProductSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnProductSearch.FlatAppearance.BorderSize = 5;
            this.btnProductSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnProductSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnProductSearch.Image")));
            this.btnProductSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProductSearch.Location = new System.Drawing.Point(490, 13);
            this.btnProductSearch.Margin = new System.Windows.Forms.Padding(1);
            this.btnProductSearch.Name = "btnProductSearch";
            this.btnProductSearch.Size = new System.Drawing.Size(141, 24);
            this.btnProductSearch.TabIndex = 22;
            this.btnProductSearch.Tag = "Product  Search";
            this.btnProductSearch.Text = "+ Add products";
            this.btnProductSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProductSearch.UseVisualStyleBackColor = false;
            this.btnProductSearch.Click += new System.EventHandler(this.btnProductSearch_Click);
            // 
            // gvStatItemsDetails
            // 
            this.gvStatItemsDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.gvStatItemsDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvStatItemsDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvStatItemsDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvStatItemsDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gvStatItemsDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvStatItemsDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.ItemID,
            this.ItemName,
            this.AvailableQty,
            this.Qty,
            this.Delete,
            this.Rate,
            this.Amount,
            this.DBRate,
            this.DBPoints});
            this.gvStatItemsDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvStatItemsDetails.Location = new System.Drawing.Point(7, 40);
            this.gvStatItemsDetails.MultiSelect = false;
            this.gvStatItemsDetails.Name = "gvStatItemsDetails";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvStatItemsDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.gvStatItemsDetails.RowHeadersVisible = false;
            this.gvStatItemsDetails.Size = new System.Drawing.Size(626, 316);
            this.gvStatItemsDetails.TabIndex = 19;
            this.gvStatItemsDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvStatItemsDetails_CellClick);
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
            // ItemID
            // 
            this.ItemID.Frozen = true;
            this.ItemID.HeaderText = "ItemID";
            this.ItemID.Name = "ItemID";
            this.ItemID.ReadOnly = true;
            this.ItemID.Visible = false;
            // 
            // ItemName
            // 
            this.ItemName.Frozen = true;
            this.ItemName.HeaderText = "Item Description";
            this.ItemName.MinimumWidth = 20;
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ItemName.Width = 350;
            // 
            // AvailableQty
            // 
            this.AvailableQty.Frozen = true;
            this.AvailableQty.HeaderText = "Available Qty";
            this.AvailableQty.Name = "AvailableQty";
            this.AvailableQty.ReadOnly = true;
            // 
            // Qty
            // 
            this.Qty.Frozen = true;
            this.Qty.HeaderText = "Issued Qty";
            this.Qty.Name = "Qty";
            // 
            // Delete
            // 
            this.Delete.Frozen = true;
            this.Delete.HeaderText = "";
            this.Delete.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Width = 30;
            // 
            // Rate
            // 
            dataGridViewCellStyle7.Format = "0.00";
            dataGridViewCellStyle7.NullValue = null;
            this.Rate.DefaultCellStyle = dataGridViewCellStyle7;
            this.Rate.Frozen = true;
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.Visible = false;
            this.Rate.Width = 70;
            // 
            // Amount
            // 
            this.Amount.Frozen = true;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.Visible = false;
            // 
            // DBRate
            // 
            this.DBRate.HeaderText = "DBRate";
            this.DBRate.Name = "DBRate";
            this.DBRate.Visible = false;
            // 
            // DBPoints
            // 
            this.DBPoints.HeaderText = "DBPoints";
            this.DBPoints.Name = "DBPoints";
            this.DBPoints.Visible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnDelete);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Location = new System.Drawing.Point(136, 466);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 45);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(276, 13);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDelete.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.Location = new System.Drawing.Point(187, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(74, 26);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(111, 13);
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
            this.btnSave.Location = new System.Drawing.Point(36, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // BranchStationaryIssue
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(658, 522);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BranchStationaryIssue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stationary Issue";
            this.Load += new System.EventHandler(this.BranchStationaryIssue_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvStatItemsDetails)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEcode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTrnNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTrnDate;
        private System.Windows.Forms.TextBox txtEmpName;
        public System.Windows.Forms.DataGridView gvStatItemsDetails;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClearProd;
        private System.Windows.Forms.Button btnProductSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AvailableQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn DBRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DBPoints;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRemarks;
    }
}