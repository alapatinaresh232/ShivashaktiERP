namespace SSCRM
{
    partial class StockIndentFRM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockIndentFRM));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblGroupEcode = new System.Windows.Forms.Label();
            this.cbGroup = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbBranches = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClearProd = new System.Windows.Forms.Button();
            this.btnProductSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gvIndentList = new System.Windows.Forms.DataGridView();
            this.SLNOList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndentNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalProducts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalReqQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.meIndentDate = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIndentNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gvIndentDetails = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GLCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EcodeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockPoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Product = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvIndentList)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvIndentDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.btnClearProd);
            this.groupBox2.Controls.Add(this.btnProductSearch);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.gvIndentList);
            this.groupBox2.Controls.Add(this.meIndentDate);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtIndentNo);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.gvIndentDetails);
            this.groupBox2.Location = new System.Drawing.Point(4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(983, 557);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblGroupEcode);
            this.groupBox1.Controls.Add(this.cbGroup);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbBranches);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBox1.Location = new System.Drawing.Point(9, 416);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 137);
            this.groupBox1.TabIndex = 77;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serch Criteria for Saved Indents";
            // 
            // lblGroupEcode
            // 
            this.lblGroupEcode.BackColor = System.Drawing.Color.Azure;
            this.lblGroupEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupEcode.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblGroupEcode.Location = new System.Drawing.Point(92, 44);
            this.lblGroupEcode.Name = "lblGroupEcode";
            this.lblGroupEcode.Size = new System.Drawing.Size(239, 23);
            this.lblGroupEcode.TabIndex = 44;
            // 
            // cbGroup
            // 
            this.cbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGroup.FormattingEnabled = true;
            this.cbGroup.Location = new System.Drawing.Point(92, 18);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Size = new System.Drawing.Size(239, 23);
            this.cbGroup.TabIndex = 41;
            this.cbGroup.SelectedIndexChanged += new System.EventHandler(this.cbGroup_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(40, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 15);
            this.label4.TabIndex = 43;
            this.label4.Text = "Group";
            // 
            // cbBranches
            // 
            this.cbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranches.FormattingEnabled = true;
            this.cbBranches.Location = new System.Drawing.Point(92, 68);
            this.cbBranches.Name = "cbBranches";
            this.cbBranches.Size = new System.Drawing.Size(239, 23);
            this.cbBranches.TabIndex = 40;
            this.cbBranches.SelectedIndexChanged += new System.EventHandler(this.cbBranches_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(7, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 15);
            this.label5.TabIndex = 42;
            this.label5.Text = "Stock Point";
            // 
            // btnClearProd
            // 
            this.btnClearProd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClearProd.BackColor = System.Drawing.Color.Moccasin;
            this.btnClearProd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClearProd.FlatAppearance.BorderSize = 5;
            this.btnClearProd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearProd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClearProd.Location = new System.Drawing.Point(711, 17);
            this.btnClearProd.Margin = new System.Windows.Forms.Padding(1);
            this.btnClearProd.Name = "btnClearProd";
            this.btnClearProd.Size = new System.Drawing.Size(123, 26);
            this.btnClearProd.TabIndex = 76;
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
            this.btnProductSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProductSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnProductSearch.Image")));
            this.btnProductSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProductSearch.Location = new System.Drawing.Point(839, 17);
            this.btnProductSearch.Margin = new System.Windows.Forms.Padding(1);
            this.btnProductSearch.Name = "btnProductSearch";
            this.btnProductSearch.Size = new System.Drawing.Size(141, 26);
            this.btnProductSearch.TabIndex = 75;
            this.btnProductSearch.Tag = "Product  Search";
            this.btnProductSearch.Text = "+ Add Lineitems";
            this.btnProductSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProductSearch.UseVisualStyleBackColor = false;
            this.btnProductSearch.Click += new System.EventHandler(this.btnProductSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 16);
            this.label1.TabIndex = 59;
            this.label1.Text = "New Indent Details";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // gvIndentList
            // 
            this.gvIndentList.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvIndentList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvIndentList.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvIndentList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvIndentList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvIndentList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvIndentList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNOList,
            this.IndentNo,
            this.IndentDate,
            this.TotalProducts,
            this.TotalReqQty});
            this.gvIndentList.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvIndentList.Location = new System.Drawing.Point(350, 416);
            this.gvIndentList.MultiSelect = false;
            this.gvIndentList.Name = "gvIndentList";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvIndentList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvIndentList.RowHeadersVisible = false;
            this.gvIndentList.Size = new System.Drawing.Size(630, 138);
            this.gvIndentList.TabIndex = 58;
            this.gvIndentList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvIndentList_CellClick);
            // 
            // SLNOList
            // 
            this.SLNOList.Frozen = true;
            this.SLNOList.HeaderText = "SL.NO";
            this.SLNOList.Name = "SLNOList";
            this.SLNOList.Width = 60;
            // 
            // IndentNo
            // 
            this.IndentNo.Frozen = true;
            this.IndentNo.HeaderText = "Indent No.";
            this.IndentNo.Name = "IndentNo";
            this.IndentNo.ReadOnly = true;
            this.IndentNo.Width = 110;
            // 
            // IndentDate
            // 
            this.IndentDate.Frozen = true;
            this.IndentDate.HeaderText = "Indent Date";
            this.IndentDate.MinimumWidth = 20;
            this.IndentDate.Name = "IndentDate";
            this.IndentDate.ReadOnly = true;
            this.IndentDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IndentDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IndentDate.Width = 150;
            // 
            // TotalProducts
            // 
            this.TotalProducts.HeaderText = "Total Products";
            this.TotalProducts.Name = "TotalProducts";
            this.TotalProducts.Width = 150;
            // 
            // TotalReqQty
            // 
            this.TotalReqQty.HeaderText = "Total Req  Qty";
            this.TotalReqQty.Name = "TotalReqQty";
            this.TotalReqQty.Width = 140;
            // 
            // meIndentDate
            // 
            this.meIndentDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.meIndentDate.Location = new System.Drawing.Point(496, 22);
            this.meIndentDate.Mask = "00/00/0000";
            this.meIndentDate.Name = "meIndentDate";
            this.meIndentDate.Size = new System.Drawing.Size(77, 21);
            this.meIndentDate.TabIndex = 43;
            this.meIndentDate.ValidatingType = typeof(System.DateTime);
            this.meIndentDate.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.meIndentDate_MaskInputRejected);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(402, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 16);
            this.label3.TabIndex = 45;
            this.label3.Text = "Indent  Date";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtIndentNo
            // 
            this.txtIndentNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIndentNo.Location = new System.Drawing.Point(292, 22);
            this.txtIndentNo.MaxLength = 9;
            this.txtIndentNo.Name = "txtIndentNo";
            this.txtIndentNo.Size = new System.Drawing.Size(94, 21);
            this.txtIndentNo.TabIndex = 42;
            this.txtIndentNo.TextChanged += new System.EventHandler(this.txtIndentNo_TextChanged);
            this.txtIndentNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIndentNo_KeyPress);
            this.txtIndentNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtIndentNo_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(212, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 16);
            this.label2.TabIndex = 44;
            this.label2.Text = "Indnet No";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnDelete);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Location = new System.Drawing.Point(285, 364);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 45);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(276, 13);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 23;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.Location = new System.Drawing.Point(187, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(74, 26);
            this.btnDelete.TabIndex = 22;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(111, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 26);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoEllipsis = true;
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(36, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gvIndentDetails
            // 
            this.gvIndentDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.gvIndentDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvIndentDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvIndentDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvIndentDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gvIndentDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvIndentDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.Group,
            this.GLCode,
            this.EcodeName,
            this.StockPoint,
            this.Category,
            this.Product,
            this.Qty,
            this.ProductId,
            this.BranchCode});
            this.gvIndentDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvIndentDetails.Location = new System.Drawing.Point(2, 46);
            this.gvIndentDetails.MultiSelect = false;
            this.gvIndentDetails.Name = "gvIndentDetails";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvIndentDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gvIndentDetails.RowHeadersVisible = false;
            this.gvIndentDetails.Size = new System.Drawing.Size(978, 316);
            this.gvIndentDetails.TabIndex = 21;
            this.gvIndentDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvIndentDetails_CellClick);
            this.gvIndentDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvIndentDetails_EditingControlShowing);
            this.gvIndentDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvIndentDetails_CellContentClick);
            // 
            // SLNO
            // 
            this.SLNO.Frozen = true;
            this.SLNO.HeaderText = "SL.NO";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Width = 55;
            // 
            // Group
            // 
            this.Group.Frozen = true;
            this.Group.HeaderText = "Group";
            this.Group.Name = "Group";
            this.Group.ReadOnly = true;
            this.Group.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Group.Width = 140;
            // 
            // GLCode
            // 
            this.GLCode.Frozen = true;
            this.GLCode.HeaderText = "GL Code";
            this.GLCode.MinimumWidth = 20;
            this.GLCode.Name = "GLCode";
            this.GLCode.ReadOnly = true;
            this.GLCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GLCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GLCode.Width = 80;
            // 
            // EcodeName
            // 
            this.EcodeName.Frozen = true;
            this.EcodeName.HeaderText = "Name";
            this.EcodeName.Name = "EcodeName";
            this.EcodeName.ReadOnly = true;
            this.EcodeName.Width = 150;
            // 
            // StockPoint
            // 
            dataGridViewCellStyle6.Format = "0.00";
            this.StockPoint.DefaultCellStyle = dataGridViewCellStyle6;
            this.StockPoint.Frozen = true;
            this.StockPoint.HeaderText = "Stock Point";
            this.StockPoint.Name = "StockPoint";
            this.StockPoint.ReadOnly = true;
            this.StockPoint.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.StockPoint.Width = 150;
            // 
            // Category
            // 
            this.Category.HeaderText = "Category";
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
            this.Category.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Category.Width = 120;
            // 
            // Product
            // 
            this.Product.HeaderText = "Product";
            this.Product.Name = "Product";
            this.Product.ReadOnly = true;
            this.Product.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Product.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Product.Width = 200;
            // 
            // Qty
            // 
            this.Qty.HeaderText = "Req.Qty";
            this.Qty.Name = "Qty";
            this.Qty.Width = 70;
            // 
            // ProductId
            // 
            this.ProductId.HeaderText = "ProductId";
            this.ProductId.Name = "ProductId";
            this.ProductId.ReadOnly = true;
            this.ProductId.Visible = false;
            // 
            // BranchCode
            // 
            this.BranchCode.HeaderText = "BranchCode";
            this.BranchCode.Name = "BranchCode";
            this.BranchCode.Visible = false;
            // 
            // StockIndentFRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(989, 565);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "StockIndentFRM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Indent To Stock Points";
            this.Load += new System.EventHandler(this.StockIndentFRM_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvIndentList)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvIndentDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MaskedTextBox meIndentDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIndentNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.DataGridView gvIndentDetails;
        public System.Windows.Forms.DataGridView gvIndentList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClearProd;
        private System.Windows.Forms.Button btnProductSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group;
        private System.Windows.Forms.DataGridViewTextBoxColumn GLCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn EcodeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockPoint;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn Product;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblGroupEcode;
        private System.Windows.Forms.ComboBox cbGroup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbBranches;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNOList;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndentNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndentDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalProducts;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalReqQty;
    }
}