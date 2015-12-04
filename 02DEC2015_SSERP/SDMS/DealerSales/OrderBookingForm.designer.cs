namespace SDMS
{
    partial class OrderBookingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderBookingForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDealerSearch = new System.Windows.Forms.TextBox();
            this.cbDealer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDocMonth = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.dtpInvoiceDate = new System.Windows.Forms.DateTimePicker();
            this.txtEcodeSearch = new System.Windows.Forms.TextBox();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.cbEcode = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClearProd = new System.Windows.Forms.Button();
            this.txtBalAmt = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtAdvanceAmt = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtInvAmt = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.btnProductSearch = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gvProductDetails = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MainProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Brand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Points = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DBRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DBPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.txtDealerSearch);
            this.groupBox1.Controls.Add(this.cbDealer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDocMonth);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.dtpInvoiceDate);
            this.groupBox1.Controls.Add(this.txtEcodeSearch);
            this.groupBox1.Controls.Add(this.txtOrderNo);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.cbEcode);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(5, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(779, 509);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // txtDealerSearch
            // 
            this.txtDealerSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDealerSearch.Location = new System.Drawing.Point(452, 46);
            this.txtDealerSearch.MaxLength = 20;
            this.txtDealerSearch.Name = "txtDealerSearch";
            this.txtDealerSearch.Size = new System.Drawing.Size(74, 21);
            this.txtDealerSearch.TabIndex = 3;
            this.txtDealerSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDealerSearch_KeyUp);
            // 
            // cbDealer
            // 
            this.cbDealer.AllowDrop = true;
            this.cbDealer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbDealer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDealer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDealer.FormattingEnabled = true;
            this.cbDealer.Location = new System.Drawing.Point(528, 45);
            this.cbDealer.Name = "cbDealer";
            this.cbDealer.Size = new System.Drawing.Size(238, 23);
            this.cbDealer.TabIndex = 61;
            this.cbDealer.TabStop = false;
            this.cbDealer.SelectedIndexChanged += new System.EventHandler(this.cbDealer_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(397, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 62;
            this.label1.Text = "Dealer";
            // 
            // txtDocMonth
            // 
            this.txtDocMonth.BackColor = System.Drawing.SystemColors.Info;
            this.txtDocMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocMonth.Location = new System.Drawing.Point(289, 18);
            this.txtDocMonth.MaxLength = 20;
            this.txtDocMonth.Name = "txtDocMonth";
            this.txtDocMonth.ReadOnly = true;
            this.txtDocMonth.Size = new System.Drawing.Size(103, 22);
            this.txtDocMonth.TabIndex = 59;
            this.txtDocMonth.TabStop = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label21.Location = new System.Drawing.Point(183, 20);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(104, 15);
            this.label21.TabIndex = 57;
            this.label21.Text = "Documentation";
            // 
            // dtpInvoiceDate
            // 
            this.dtpInvoiceDate.CustomFormat = "dd/MM/yyyy";
            this.dtpInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInvoiceDate.Location = new System.Drawing.Point(452, 19);
            this.dtpInvoiceDate.Name = "dtpInvoiceDate";
            this.dtpInvoiceDate.Size = new System.Drawing.Size(103, 22);
            this.dtpInvoiceDate.TabIndex = 1;
            this.dtpInvoiceDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // txtEcodeSearch
            // 
            this.txtEcodeSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcodeSearch.Location = new System.Drawing.Point(79, 44);
            this.txtEcodeSearch.MaxLength = 20;
            this.txtEcodeSearch.Name = "txtEcodeSearch";
            this.txtEcodeSearch.Size = new System.Drawing.Size(74, 21);
            this.txtEcodeSearch.TabIndex = 2;
            this.txtEcodeSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEcodeSearch_KeyUp);
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderNo.Location = new System.Drawing.Point(79, 18);
            this.txtOrderNo.MaxLength = 20;
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Size = new System.Drawing.Size(74, 21);
            this.txtOrderNo.TabIndex = 0;
            this.txtOrderNo.Validated += new System.EventHandler(this.txtOrderNo_Validated);
            this.txtOrderNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOrderNo_KeyPress);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label24.Location = new System.Drawing.Point(9, 20);
            this.label24.Margin = new System.Windows.Forms.Padding(0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(71, 16);
            this.label24.TabIndex = 47;
            this.label24.Text = "Order No";
            // 
            // cbEcode
            // 
            this.cbEcode.AllowDrop = true;
            this.cbEcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbEcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEcode.FormattingEnabled = true;
            this.cbEcode.Location = new System.Drawing.Point(156, 43);
            this.cbEcode.Name = "cbEcode";
            this.cbEcode.Size = new System.Drawing.Size(237, 23);
            this.cbEcode.TabIndex = 5;
            this.cbEcode.TabStop = false;
            this.cbEcode.SelectedIndexChanged += new System.EventHandler(this.cbEcode_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label16.Location = new System.Drawing.Point(27, 46);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 16);
            this.label16.TabIndex = 11;
            this.label16.Text = "Ecode";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnClearProd);
            this.groupBox3.Controls.Add(this.txtBalAmt);
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Controls.Add(this.txtAdvanceAmt);
            this.groupBox3.Controls.Add(this.label27);
            this.groupBox3.Controls.Add(this.txtInvAmt);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.btnProductSearch);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.gvProductDetails);
            this.groupBox3.Location = new System.Drawing.Point(6, 70);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(768, 434);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Product Details";
            // 
            // btnClearProd
            // 
            this.btnClearProd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClearProd.BackColor = System.Drawing.Color.Moccasin;
            this.btnClearProd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClearProd.FlatAppearance.BorderSize = 5;
            this.btnClearProd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearProd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClearProd.Location = new System.Drawing.Point(488, 9);
            this.btnClearProd.Margin = new System.Windows.Forms.Padding(1);
            this.btnClearProd.Name = "btnClearProd";
            this.btnClearProd.Size = new System.Drawing.Size(123, 24);
            this.btnClearProd.TabIndex = 1;
            this.btnClearProd.Tag = "Product  Search";
            this.btnClearProd.Text = "Cl&ear Products";
            this.btnClearProd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearProd.UseVisualStyleBackColor = false;
            this.btnClearProd.Click += new System.EventHandler(this.btnClearProd_Click);
            // 
            // txtBalAmt
            // 
            this.txtBalAmt.Location = new System.Drawing.Point(632, 364);
            this.txtBalAmt.MaxLength = 12;
            this.txtBalAmt.Name = "txtBalAmt";
            this.txtBalAmt.ReadOnly = true;
            this.txtBalAmt.Size = new System.Drawing.Size(127, 22);
            this.txtBalAmt.TabIndex = 3;
            this.txtBalAmt.TabStop = false;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(510, 367);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(124, 16);
            this.label28.TabIndex = 64;
            this.label28.Text = "Balance Amount:";
            // 
            // txtAdvanceAmt
            // 
            this.txtAdvanceAmt.Location = new System.Drawing.Point(371, 364);
            this.txtAdvanceAmt.MaxLength = 12;
            this.txtAdvanceAmt.Name = "txtAdvanceAmt";
            this.txtAdvanceAmt.Size = new System.Drawing.Size(127, 22);
            this.txtAdvanceAmt.TabIndex = 2;
            this.txtAdvanceAmt.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAdvanceAmt_KeyUp);
            this.txtAdvanceAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAdvanceAmt_KeyPress);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(245, 367);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(128, 16);
            this.label27.TabIndex = 62;
            this.label27.Text = "Advance Amount:";
            // 
            // txtInvAmt
            // 
            this.txtInvAmt.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtInvAmt.Location = new System.Drawing.Point(110, 364);
            this.txtInvAmt.MaxLength = 12;
            this.txtInvAmt.Name = "txtInvAmt";
            this.txtInvAmt.ReadOnly = true;
            this.txtInvAmt.Size = new System.Drawing.Size(127, 22);
            this.txtInvAmt.TabIndex = 20;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(3, 367);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(106, 16);
            this.label26.TabIndex = 60;
            this.label26.Text = "Order Amount:";
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
            this.btnProductSearch.Location = new System.Drawing.Point(616, 9);
            this.btnProductSearch.Margin = new System.Windows.Forms.Padding(1);
            this.btnProductSearch.Name = "btnProductSearch";
            this.btnProductSearch.Size = new System.Drawing.Size(141, 24);
            this.btnProductSearch.TabIndex = 0;
            this.btnProductSearch.Tag = "Product  Search";
            this.btnProductSearch.Text = "+ Add products";
            this.btnProductSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProductSearch.UseVisualStyleBackColor = false;
            this.btnProductSearch.Click += new System.EventHandler(this.btnProductSearch_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnDelete);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Location = new System.Drawing.Point(195, 385);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 45);
            this.groupBox4.TabIndex = 4;
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
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(111, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Clear";
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
            // gvProductDetails
            // 
            this.gvProductDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvProductDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvProductDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvProductDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvProductDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvProductDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProductDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.ProductID,
            this.MainProduct,
            this.Brand,
            this.Description,
            this.Qty,
            this.Rate,
            this.Amount,
            this.Points,
            this.DBRate,
            this.DBPoints});
            this.gvProductDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvProductDetails.Location = new System.Drawing.Point(6, 34);
            this.gvProductDetails.MultiSelect = false;
            this.gvProductDetails.Name = "gvProductDetails";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvProductDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvProductDetails.RowHeadersVisible = false;
            this.gvProductDetails.Size = new System.Drawing.Size(755, 323);
            this.gvProductDetails.TabIndex = 18;
            this.gvProductDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvProductDetails_CellEndEdit);
            this.gvProductDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvProductDetails_EditingControlShowing);
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
            // ProductID
            // 
            this.ProductID.Frozen = true;
            this.ProductID.HeaderText = "ProductID";
            this.ProductID.Name = "ProductID";
            this.ProductID.ReadOnly = true;
            this.ProductID.Visible = false;
            // 
            // MainProduct
            // 
            this.MainProduct.Frozen = true;
            this.MainProduct.HeaderText = "Main Product";
            this.MainProduct.MinimumWidth = 20;
            this.MainProduct.Name = "MainProduct";
            this.MainProduct.ReadOnly = true;
            this.MainProduct.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MainProduct.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MainProduct.Width = 170;
            // 
            // Brand
            // 
            this.Brand.Frozen = true;
            this.Brand.HeaderText = "Brand";
            this.Brand.Name = "Brand";
            this.Brand.ReadOnly = true;
            this.Brand.Width = 300;
            // 
            // Description
            // 
            this.Description.Frozen = true;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Visible = false;
            this.Description.Width = 310;
            // 
            // Qty
            // 
            this.Qty.Frozen = true;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.Width = 60;
            // 
            // Rate
            // 
            dataGridViewCellStyle3.Format = "0.00";
            dataGridViewCellStyle3.NullValue = null;
            this.Rate.DefaultCellStyle = dataGridViewCellStyle3;
            this.Rate.Frozen = true;
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.Width = 70;
            // 
            // Amount
            // 
            this.Amount.Frozen = true;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            // 
            // Points
            // 
            this.Points.HeaderText = "Points";
            this.Points.Name = "Points";
            this.Points.Visible = false;
            this.Points.Width = 70;
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(411, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 16);
            this.label3.TabIndex = 41;
            this.label3.Text = "Date";
            // 
            // OrderBookingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(789, 517);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "OrderBookingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order Booking Form";
            this.Load += new System.EventHandler(this.OrderBookingForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvProductDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDocMonth;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.DateTimePicker dtpInvoiceDate;
        private System.Windows.Forms.TextBox txtEcodeSearch;
        private System.Windows.Forms.TextBox txtOrderNo;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cbEcode;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClearProd;
        private System.Windows.Forms.TextBox txtBalAmt;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtAdvanceAmt;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtInvAmt;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button btnProductSearch;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.DataGridView gvProductDetails;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDealerSearch;
        private System.Windows.Forms.ComboBox cbDealer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MainProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn Brand;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Points;
        private System.Windows.Forms.DataGridViewTextBoxColumn DBRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DBPoints;
    }
}