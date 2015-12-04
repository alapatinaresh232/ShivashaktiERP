namespace SSCRM
{
    partial class StationaryOpeningStock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StationaryOpeningStock));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtItemsCount = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnClearItems = new System.Windows.Forms.Button();
            this.txtIndentAmt = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.btnItemsSearch = new System.Windows.Forms.Button();
            this.gvItemsDetails = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemsDetails)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.dtpDate);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtItemsCount);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.btnClearItems);
            this.groupBox3.Controls.Add(this.txtIndentAmt);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.btnItemsSearch);
            this.groupBox3.Controls.Add(this.gvItemsDetails);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Location = new System.Drawing.Point(4, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(593, 438);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Product Details";
            // 
            // dtpDate
            // 
            this.dtpDate.Checked = false;
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(52, 22);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(110, 22);
            this.dtpDate.TabIndex = 80;
            this.dtpDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(8, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 16);
            this.label3.TabIndex = 79;
            this.label3.Text = "Date";
            // 
            // txtItemsCount
            // 
            this.txtItemsCount.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtItemsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemsCount.Location = new System.Drawing.Point(515, 476);
            this.txtItemsCount.MaxLength = 12;
            this.txtItemsCount.Name = "txtItemsCount";
            this.txtItemsCount.ReadOnly = true;
            this.txtItemsCount.Size = new System.Drawing.Size(58, 21);
            this.txtItemsCount.TabIndex = 77;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(433, 479);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 15);
            this.label13.TabIndex = 78;
            this.label13.Text = "Total Items";
            // 
            // btnClearItems
            // 
            this.btnClearItems.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClearItems.BackColor = System.Drawing.Color.Moccasin;
            this.btnClearItems.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClearItems.FlatAppearance.BorderSize = 5;
            this.btnClearItems.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearItems.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClearItems.Location = new System.Drawing.Point(367, 21);
            this.btnClearItems.Margin = new System.Windows.Forms.Padding(1);
            this.btnClearItems.Name = "btnClearItems";
            this.btnClearItems.Size = new System.Drawing.Size(99, 24);
            this.btnClearItems.TabIndex = 0;
            this.btnClearItems.Tag = "Product  Search";
            this.btnClearItems.Text = "Cl&ear Items";
            this.btnClearItems.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearItems.UseVisualStyleBackColor = false;
            this.btnClearItems.Click += new System.EventHandler(this.btnClearItems_Click);
            // 
            // txtIndentAmt
            // 
            this.txtIndentAmt.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtIndentAmt.Location = new System.Drawing.Point(688, 475);
            this.txtIndentAmt.MaxLength = 12;
            this.txtIndentAmt.Name = "txtIndentAmt";
            this.txtIndentAmt.ReadOnly = true;
            this.txtIndentAmt.Size = new System.Drawing.Size(89, 22);
            this.txtIndentAmt.TabIndex = 20;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(575, 478);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(109, 16);
            this.label26.TabIndex = 60;
            this.label26.Text = "Indent Amount:";
            // 
            // btnItemsSearch
            // 
            this.btnItemsSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnItemsSearch.BackColor = System.Drawing.Color.YellowGreen;
            this.btnItemsSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnItemsSearch.FlatAppearance.BorderSize = 5;
            this.btnItemsSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnItemsSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnItemsSearch.Image")));
            this.btnItemsSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnItemsSearch.Location = new System.Drawing.Point(468, 21);
            this.btnItemsSearch.Margin = new System.Windows.Forms.Padding(1);
            this.btnItemsSearch.Name = "btnItemsSearch";
            this.btnItemsSearch.Size = new System.Drawing.Size(117, 24);
            this.btnItemsSearch.TabIndex = 1;
            this.btnItemsSearch.Tag = "Product  Search";
            this.btnItemsSearch.Text = "+ Add Items";
            this.btnItemsSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnItemsSearch.UseVisualStyleBackColor = false;
            this.btnItemsSearch.Click += new System.EventHandler(this.btnItemsSearch_Click);
            // 
            // gvItemsDetails
            // 
            this.gvItemsDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvItemsDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvItemsDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvItemsDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvItemsDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvItemsDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvItemsDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.ItemID,
            this.ItemName,
            this.Qty,
            this.Delete});
            this.gvItemsDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvItemsDetails.Location = new System.Drawing.Point(6, 51);
            this.gvItemsDetails.MultiSelect = false;
            this.gvItemsDetails.Name = "gvItemsDetails";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvItemsDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvItemsDetails.RowHeadersVisible = false;
            this.gvItemsDetails.Size = new System.Drawing.Size(579, 341);
            this.gvItemsDetails.TabIndex = 18;
            this.gvItemsDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvItemsDetails_CellClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnDelete);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Location = new System.Drawing.Point(107, 388);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 45);
            this.groupBox4.TabIndex = 19;
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
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(111, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
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
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.MinimumWidth = 20;
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemName.Width = 350;
            // 
            // Qty
            // 
            this.Qty.Frozen = true;
            this.Qty.HeaderText = "Qty";
            this.Qty.MaxInputLength = 10;
            this.Qty.Name = "Qty";
            this.Qty.Width = 75;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "";
            this.Delete.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Width = 50;
            // 
            // StationaryOpeningStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(600, 446);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StationaryOpeningStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stationary Opening Stock";
            this.Load += new System.EventHandler(this.StationaryOpeningStock_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemsDetails)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtItemsCount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnClearItems;
        private System.Windows.Forms.TextBox txtIndentAmt;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button btnItemsSearch;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.DataGridView gvItemsDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
    }
}