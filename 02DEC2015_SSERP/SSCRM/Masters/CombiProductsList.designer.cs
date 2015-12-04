namespace SSCRM
{
    partial class CombiProductsList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.gvSingleProducts = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gvCombiProductDetails = new System.Windows.Forms.DataGridView();
            this.SlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CombiId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CombiName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.SlNo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SProdName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProdFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SProdQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSingleProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCombiProductDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.gvSingleProducts);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.gvCombiProductDetails);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(752, 608);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Combi Products List";
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.AliceBlue;
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnNew.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnNew.Location = new System.Drawing.Point(472, 573);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(74, 26);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "&New";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(552, 573);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 26);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "C&lose";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gvSingleProducts
            // 
            this.gvSingleProducts.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.gvSingleProducts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvSingleProducts.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvSingleProducts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSingleProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvSingleProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSingleProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SlNo1,
            this.SProductId,
            this.SProdName,
            this.CategoryName,
            this.ProdFlag,
            this.SProdQty});
            this.gvSingleProducts.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvSingleProducts.Location = new System.Drawing.Point(7, 431);
            this.gvSingleProducts.MultiSelect = false;
            this.gvSingleProducts.Name = "gvSingleProducts";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSingleProducts.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvSingleProducts.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.MidnightBlue;
            this.gvSingleProducts.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvSingleProducts.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.gvSingleProducts.Size = new System.Drawing.Size(735, 134);
            this.gvSingleProducts.TabIndex = 129;
            // 
            // groupBox2
            // 
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(2, 405);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(744, 160);
            this.groupBox2.TabIndex = 130;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Single Products In Combi";
            // 
            // gvCombiProductDetails
            // 
            this.gvCombiProductDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.MidnightBlue;
            this.gvCombiProductDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvCombiProductDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvCombiProductDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCombiProductDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gvCombiProductDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvCombiProductDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SlNo,
            this.CombiId,
            this.CombiName,
            this.Edit,
            this.Delete});
            this.gvCombiProductDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvCombiProductDetails.Location = new System.Drawing.Point(9, 19);
            this.gvCombiProductDetails.MultiSelect = false;
            this.gvCombiProductDetails.Name = "gvCombiProductDetails";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCombiProductDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gvCombiProductDetails.RowHeadersVisible = false;
            this.gvCombiProductDetails.Size = new System.Drawing.Size(739, 382);
            this.gvCombiProductDetails.TabIndex = 21;
            this.gvCombiProductDetails.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCombiProductDetails_RowEnter);
            this.gvCombiProductDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCombiProductDetails_CellClick);
            // 
            // SlNo
            // 
            this.SlNo.Frozen = true;
            this.SlNo.HeaderText = "Sl.No";
            this.SlNo.Name = "SlNo";
            this.SlNo.ReadOnly = true;
            this.SlNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SlNo.Width = 50;
            // 
            // CombiId
            // 
            this.CombiId.Frozen = true;
            this.CombiId.HeaderText = "Combi ID";
            this.CombiId.MinimumWidth = 20;
            this.CombiId.Name = "CombiId";
            this.CombiId.ReadOnly = true;
            this.CombiId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CombiId.Width = 105;
            // 
            // CombiName
            // 
            this.CombiName.Frozen = true;
            this.CombiName.HeaderText = "Name/Description";
            this.CombiName.Name = "CombiName";
            this.CombiName.ReadOnly = true;
            this.CombiName.Width = 460;
            // 
            // Edit
            // 
            this.Edit.Frozen = true;
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.Edit.Name = "Edit";
            this.Edit.Width = 40;
            // 
            // Delete
            // 
            this.Delete.Frozen = true;
            this.Delete.HeaderText = "Del";
            this.Delete.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.Delete.Name = "Delete";
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Delete.Width = 40;
            // 
            // SlNo1
            // 
            this.SlNo1.Frozen = true;
            this.SlNo1.HeaderText = "Sl.No";
            this.SlNo1.Name = "SlNo1";
            this.SlNo1.ReadOnly = true;
            this.SlNo1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SlNo1.Width = 50;
            // 
            // SProductId
            // 
            this.SProductId.Frozen = true;
            this.SProductId.HeaderText = "Product ID";
            this.SProductId.MinimumWidth = 20;
            this.SProductId.Name = "SProductId";
            this.SProductId.ReadOnly = true;
            this.SProductId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SProductId.Width = 110;
            // 
            // SProdName
            // 
            this.SProdName.Frozen = true;
            this.SProdName.HeaderText = "Product Name";
            this.SProdName.Name = "SProdName";
            this.SProdName.ReadOnly = true;
            this.SProdName.Width = 200;
            // 
            // CategoryName
            // 
            this.CategoryName.Frozen = true;
            this.CategoryName.HeaderText = "Category";
            this.CategoryName.Name = "CategoryName";
            this.CategoryName.Width = 250;
            // 
            // ProdFlag
            // 
            this.ProdFlag.HeaderText = "F/R";
            this.ProdFlag.Name = "ProdFlag";
            this.ProdFlag.Width = 50;
            // 
            // SProdQty
            // 
            this.SProdQty.HeaderText = "Qty";
            this.SProdQty.Name = "SProdQty";
            this.SProdQty.Width = 50;
            // 
            // CombiProductsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(757, 611);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CombiProductsList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CombiProductsList";
            this.Load += new System.EventHandler(this.CombiProductsList_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvSingleProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCombiProductDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.DataGridView gvCombiProductDetails;
        public System.Windows.Forms.DataGridView gvSingleProducts;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CombiId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CombiName;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlNo1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SProdName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProdFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn SProdQty;
    }
}