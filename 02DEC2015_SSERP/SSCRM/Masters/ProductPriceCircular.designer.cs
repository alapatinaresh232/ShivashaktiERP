namespace SSCRM
{
    partial class ProductPriceCircular
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductPriceCircular));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpEffDate = new System.Windows.Forms.DateTimePicker();
            this.lblEffDate = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gvSubProdDetails = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gvProductDetails = new System.Windows.Forms.DataGridView();
            this.slNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prodMrp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.offerPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prodPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VatPer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnProductSearch = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.lblCompany = new System.Windows.Forms.Label();
            this.lblBranch = new System.Windows.Forms.Label();
            this.cbUserBranch = new System.Windows.Forms.ComboBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.slNo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CombiProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SingleProdId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SingleProdName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prodQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subProdMrp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subProdOffPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subProdPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prodSlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.singleSalePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.singleVatPer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.singleVat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSubProdDetails)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductDetails)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.dtpEffDate);
            this.groupBox1.Controls.Add(this.lblEffDate);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.lblCompany);
            this.groupBox1.Controls.Add(this.lblBranch);
            this.groupBox1.Controls.Add(this.cbUserBranch);
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(950, 571);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dtpEffDate
            // 
            this.dtpEffDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEffDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEffDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEffDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEffDate.Location = new System.Drawing.Point(85, 39);
            this.dtpEffDate.Name = "dtpEffDate";
            this.dtpEffDate.Size = new System.Drawing.Size(102, 22);
            this.dtpEffDate.TabIndex = 128;
            this.dtpEffDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpEffDate.ValueChanged += new System.EventHandler(this.dtpEffDate_ValueChanged);
            // 
            // lblEffDate
            // 
            this.lblEffDate.AutoSize = true;
            this.lblEffDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEffDate.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblEffDate.Location = new System.Drawing.Point(47, 39);
            this.lblEffDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEffDate.Name = "lblEffDate";
            this.lblEffDate.Size = new System.Drawing.Size(41, 17);
            this.lblEffDate.TabIndex = 127;
            this.lblEffDate.Text = "WEF";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gvSubProdDetails);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Navy;
            this.groupBox3.Location = new System.Drawing.Point(8, 336);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(941, 188);
            this.groupBox3.TabIndex = 126;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sub Product Details";
            // 
            // gvSubProdDetails
            // 
            this.gvSubProdDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Navy;
            this.gvSubProdDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvSubProdDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvSubProdDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSubProdDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvSubProdDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSubProdDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.slNo1,
            this.SProductId,
            this.CombiProductName,
            this.Category,
            this.SingleProdId,
            this.SingleProdName,
            this.prodQty,
            this.subProdMrp,
            this.subProdOffPrice,
            this.subProdPoints,
            this.prodSlNo,
            this.singleSalePrice,
            this.singleVatPer,
            this.singleVat});
            this.gvSubProdDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvSubProdDetails.Location = new System.Drawing.Point(4, 18);
            this.gvSubProdDetails.MultiSelect = false;
            this.gvSubProdDetails.Name = "gvSubProdDetails";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSubProdDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvSubProdDetails.RowHeadersVisible = false;
            this.gvSubProdDetails.Size = new System.Drawing.Size(932, 166);
            this.gvSubProdDetails.TabIndex = 20;
            this.gvSubProdDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSubProdDetails_CellEndEdit);
            this.gvSubProdDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvSubProdDetails_EditingControlShowing);
            this.gvSubProdDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSubProdDetails_CellContentClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gvProductDetails);
            this.groupBox2.Controls.Add(this.btnProductSearch);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(6, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(941, 271);
            this.groupBox2.TabIndex = 125;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Product Details";
            // 
            // gvProductDetails
            // 
            this.gvProductDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Navy;
            this.gvProductDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvProductDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvProductDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvProductDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gvProductDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProductDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.slNo,
            this.categoryName,
            this.ProductId,
            this.productName,
            this.prodMrp,
            this.offerPrice,
            this.prodPoints,
            this.SalePrice,
            this.VatPer,
            this.Vat,
            this.Delete});
            this.gvProductDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvProductDetails.Location = new System.Drawing.Point(5, 37);
            this.gvProductDetails.MultiSelect = false;
            this.gvProductDetails.Name = "gvProductDetails";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvProductDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gvProductDetails.RowHeadersVisible = false;
            this.gvProductDetails.Size = new System.Drawing.Size(930, 229);
            this.gvProductDetails.TabIndex = 20;
            this.gvProductDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProductDetails_CellEndEdit);
            this.gvProductDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProductDetails_CellClick);
            this.gvProductDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvProductDetails_EditingControlShowing);
            // 
            // slNo
            // 
            this.slNo.Frozen = true;
            this.slNo.HeaderText = "SlNo";
            this.slNo.Name = "slNo";
            this.slNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.slNo.Width = 40;
            // 
            // categoryName
            // 
            this.categoryName.HeaderText = "Category";
            this.categoryName.Name = "categoryName";
            this.categoryName.ReadOnly = true;
            // 
            // ProductId
            // 
            this.ProductId.HeaderText = "ProductId";
            this.ProductId.Name = "ProductId";
            this.ProductId.ReadOnly = true;
            this.ProductId.Visible = false;
            this.ProductId.Width = 150;
            // 
            // productName
            // 
            this.productName.HeaderText = "Product Name";
            this.productName.Name = "productName";
            this.productName.ReadOnly = true;
            this.productName.Width = 350;
            // 
            // prodMrp
            // 
            this.prodMrp.HeaderText = "MRP";
            this.prodMrp.Name = "prodMrp";
            this.prodMrp.Width = 70;
            // 
            // offerPrice
            // 
            this.offerPrice.HeaderText = "Offer";
            this.offerPrice.Name = "offerPrice";
            this.offerPrice.Width = 70;
            // 
            // prodPoints
            // 
            this.prodPoints.HeaderText = "Points";
            this.prodPoints.Name = "prodPoints";
            this.prodPoints.Width = 50;
            // 
            // SalePrice
            // 
            this.SalePrice.HeaderText = "Sale Price";
            this.SalePrice.Name = "SalePrice";
            this.SalePrice.Width = 70;
            // 
            // VatPer
            // 
            this.VatPer.HeaderText = "Vat %";
            this.VatPer.Name = "VatPer";
            this.VatPer.Width = 50;
            // 
            // Vat
            // 
            this.Vat.HeaderText = "Vat Amt";
            this.Vat.Name = "Vat";
            this.Vat.Width = 70;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "";
            this.Delete.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.Delete.Name = "Delete";
            this.Delete.Width = 30;
            // 
            // btnProductSearch
            // 
            this.btnProductSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnProductSearch.BackColor = System.Drawing.Color.YellowGreen;
            this.btnProductSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnProductSearch.FlatAppearance.BorderSize = 5;
            this.btnProductSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnProductSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProductSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnProductSearch.Image")));
            this.btnProductSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProductSearch.Location = new System.Drawing.Point(788, 11);
            this.btnProductSearch.Margin = new System.Windows.Forms.Padding(1);
            this.btnProductSearch.Name = "btnProductSearch";
            this.btnProductSearch.Size = new System.Drawing.Size(147, 25);
            this.btnProductSearch.TabIndex = 35;
            this.btnProductSearch.Tag = "Product Search";
            this.btnProductSearch.Text = "+&Add Products ";
            this.btnProductSearch.UseVisualStyleBackColor = false;
            this.btnProductSearch.Click += new System.EventHandler(this.btnProductSearch_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnDelete);
            this.groupBox5.Controls.Add(this.btnClose);
            this.groupBox5.Controls.Add(this.btnCancel);
            this.groupBox5.Controls.Add(this.btnSave);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox5.Location = new System.Drawing.Point(267, 521);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(345, 45);
            this.groupBox5.TabIndex = 124;
            this.groupBox5.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.Location = new System.Drawing.Point(109, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(74, 26);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(261, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 26);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "C&lose";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(184, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Clea&r";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoEllipsis = true;
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(34, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbCompany
            // 
            this.cbCompany.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Location = new System.Drawing.Point(85, 12);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(382, 24);
            this.cbCompany.TabIndex = 32;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblCompany.Location = new System.Drawing.Point(14, 15);
            this.lblCompany.Margin = new System.Windows.Forms.Padding(0);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(73, 16);
            this.lblCompany.TabIndex = 33;
            this.lblCompany.Text = "Company";
            // 
            // lblBranch
            // 
            this.lblBranch.AutoSize = true;
            this.lblBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBranch.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblBranch.Location = new System.Drawing.Point(470, 16);
            this.lblBranch.Margin = new System.Windows.Forms.Padding(0);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Size = new System.Drawing.Size(60, 16);
            this.lblBranch.TabIndex = 34;
            this.lblBranch.Text = " Branch";
            // 
            // cbUserBranch
            // 
            this.cbUserBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUserBranch.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbUserBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUserBranch.FormattingEnabled = true;
            this.cbUserBranch.Location = new System.Drawing.Point(530, 12);
            this.cbUserBranch.Name = "cbUserBranch";
            this.cbUserBranch.Size = new System.Drawing.Size(411, 24);
            this.cbUserBranch.TabIndex = 31;
            this.cbUserBranch.SelectedIndexChanged += new System.EventHandler(this.cbUserBranch_SelectedIndexChanged);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Delete";
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Visible = false;
            this.dataGridViewImageColumn2.Width = 40;
            // 
            // slNo1
            // 
            this.slNo1.HeaderText = "Sl.No";
            this.slNo1.Name = "slNo1";
            this.slNo1.ReadOnly = true;
            this.slNo1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.slNo1.Width = 50;
            // 
            // SProductId
            // 
            this.SProductId.HeaderText = "CombiProdId";
            this.SProductId.Name = "SProductId";
            this.SProductId.ReadOnly = true;
            this.SProductId.Visible = false;
            // 
            // CombiProductName
            // 
            this.CombiProductName.HeaderText = "Combi Name";
            this.CombiProductName.Name = "CombiProductName";
            this.CombiProductName.ReadOnly = true;
            // 
            // Category
            // 
            this.Category.HeaderText = "Category";
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
            // 
            // SingleProdId
            // 
            this.SingleProdId.HeaderText = "SingleProductsId";
            this.SingleProdId.Name = "SingleProdId";
            this.SingleProdId.ReadOnly = true;
            this.SingleProdId.Visible = false;
            // 
            // SingleProdName
            // 
            this.SingleProdName.HeaderText = "Single Products";
            this.SingleProdName.Name = "SingleProdName";
            this.SingleProdName.ReadOnly = true;
            this.SingleProdName.Width = 200;
            // 
            // prodQty
            // 
            this.prodQty.HeaderText = "QTY";
            this.prodQty.Name = "prodQty";
            this.prodQty.Width = 50;
            // 
            // subProdMrp
            // 
            this.subProdMrp.HeaderText = "MRP";
            this.subProdMrp.Name = "subProdMrp";
            this.subProdMrp.Width = 70;
            // 
            // subProdOffPrice
            // 
            this.subProdOffPrice.HeaderText = "Offer";
            this.subProdOffPrice.Name = "subProdOffPrice";
            this.subProdOffPrice.Width = 70;
            // 
            // subProdPoints
            // 
            this.subProdPoints.HeaderText = "Points";
            this.subProdPoints.Name = "subProdPoints";
            this.subProdPoints.Width = 60;
            // 
            // prodSlNo
            // 
            this.prodSlNo.HeaderText = "ProdSlNo";
            this.prodSlNo.Name = "prodSlNo";
            this.prodSlNo.Visible = false;
            // 
            // singleSalePrice
            // 
            this.singleSalePrice.HeaderText = "Sale Price";
            this.singleSalePrice.Name = "singleSalePrice";
            this.singleSalePrice.Width = 70;
            // 
            // singleVatPer
            // 
            this.singleVatPer.HeaderText = "Vat %";
            this.singleVatPer.Name = "singleVatPer";
            this.singleVatPer.Width = 50;
            // 
            // singleVat
            // 
            this.singleVat.HeaderText = "Vat Amt";
            this.singleVat.Name = "singleVat";
            this.singleVat.Width = 70;
            // 
            // ProductPriceCircular
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(956, 577);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProductPriceCircular";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProductPriceCircular";
            this.Load += new System.EventHandler(this.ProductPriceCircular_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvSubProdDetails)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvProductDetails)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.ComboBox cbUserBranch;
        private System.Windows.Forms.Button btnProductSearch;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.DataGridView gvProductDetails;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.DataGridView gvSubProdDetails;
        private System.Windows.Forms.DateTimePicker dtpEffDate;
        private System.Windows.Forms.Label lblEffDate;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn slNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn productName;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodMrp;
        private System.Windows.Forms.DataGridViewTextBoxColumn offerPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn VatPer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vat;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn slNo1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CombiProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn SingleProdId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SingleProdName;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn subProdMrp;
        private System.Windows.Forms.DataGridViewTextBoxColumn subProdOffPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn subProdPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodSlNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn singleSalePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn singleVatPer;
        private System.Windows.Forms.DataGridViewTextBoxColumn singleVat;
    }
}