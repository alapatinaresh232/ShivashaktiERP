namespace SSCRM
{
    partial class StationaryGRN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StationaryGRN));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbSupplierCode = new System.Windows.Forms.ComboBox();
            this.lblBranchCode = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSupplyerName = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gvIndentDetails = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DcBull = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Accepted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Shortage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FrmNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClearItems = new System.Windows.Forms.Button();
            this.btnItemsSearch = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dtPO = new System.Windows.Forms.DateTimePicker();
            this.txtPo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtDC = new System.Windows.Forms.DateTimePicker();
            this.txtDCNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtGrn = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.txtGrnNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvIndentDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.cbSupplierCode);
            this.groupBox1.Controls.Add(this.lblBranchCode);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtSupplyerName);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.gvIndentDetails);
            this.groupBox1.Controls.Add(this.btnClearItems);
            this.groupBox1.Controls.Add(this.btnItemsSearch);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dtPO);
            this.groupBox1.Controls.Add(this.txtPo);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtDC);
            this.groupBox1.Controls.Add(this.txtDCNo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtGrn);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.txtGrnNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(804, 478);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cbSupplierCode
            // 
            this.cbSupplierCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSupplierCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSupplierCode.FormattingEnabled = true;
            this.cbSupplierCode.Location = new System.Drawing.Point(120, 105);
            this.cbSupplierCode.Name = "cbSupplierCode";
            this.cbSupplierCode.Size = new System.Drawing.Size(102, 23);
            this.cbSupplierCode.TabIndex = 81;
            this.cbSupplierCode.SelectedIndexChanged += new System.EventHandler(this.cbSupplierCode_SelectedIndexChanged);
            // 
            // lblBranchCode
            // 
            this.lblBranchCode.AutoSize = true;
            this.lblBranchCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblBranchCode.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblBranchCode.Location = new System.Drawing.Point(425, 24);
            this.lblBranchCode.Margin = new System.Windows.Forms.Padding(0);
            this.lblBranchCode.Name = "lblBranchCode";
            this.lblBranchCode.Size = new System.Drawing.Size(0, 16);
            this.lblBranchCode.TabIndex = 80;
            this.lblBranchCode.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label9.Location = new System.Drawing.Point(231, 108);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 16);
            this.label9.TabIndex = 79;
            this.label9.Text = "Name";
            // 
            // txtSupplyerName
            // 
            this.txtSupplyerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSupplyerName.Location = new System.Drawing.Point(298, 105);
            this.txtSupplyerName.MaxLength = 9;
            this.txtSupplyerName.Name = "txtSupplyerName";
            this.txtSupplyerName.Size = new System.Drawing.Size(237, 21);
            this.txtSupplyerName.TabIndex = 8;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnDelete);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Location = new System.Drawing.Point(136, 427);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 45);
            this.groupBox4.TabIndex = 76;
            this.groupBox4.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
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
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
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
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
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
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(36, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gvIndentDetails
            // 
            this.gvIndentDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvIndentDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvIndentDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvIndentDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvIndentDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvIndentDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvIndentDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.ItemID,
            this.ItemName,
            this.DcBull,
            this.Accepted,
            this.Shortage,
            this.FrmNo,
            this.ToNo,
            this.Remarks});
            this.gvIndentDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvIndentDetails.Location = new System.Drawing.Point(6, 141);
            this.gvIndentDetails.MultiSelect = false;
            this.gvIndentDetails.Name = "gvIndentDetails";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvIndentDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvIndentDetails.RowHeadersVisible = false;
            this.gvIndentDetails.Size = new System.Drawing.Size(794, 285);
            this.gvIndentDetails.TabIndex = 11;
            this.gvIndentDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvIndentDetails_CellEndEdit);
            this.gvIndentDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvIndentDetails_EditingControlShowing);
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
            this.ItemID.Width = 50;
            // 
            // ItemName
            // 
            this.ItemName.Frozen = true;
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.MinimumWidth = 20;
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ItemName.Width = 250;
            // 
            // DcBull
            // 
            this.DcBull.Frozen = true;
            this.DcBull.HeaderText = "DC/Bill Qty";
            this.DcBull.Name = "DcBull";
            this.DcBull.Width = 75;
            // 
            // Accepted
            // 
            this.Accepted.Frozen = true;
            this.Accepted.HeaderText = "Acce. Qty";
            this.Accepted.Name = "Accepted";
            this.Accepted.Width = 75;
            // 
            // Shortage
            // 
            this.Shortage.Frozen = true;
            this.Shortage.HeaderText = "Shortage / Excess";
            this.Shortage.Name = "Shortage";
            this.Shortage.ReadOnly = true;
            // 
            // FrmNo
            // 
            this.FrmNo.Frozen = true;
            this.FrmNo.HeaderText = "From No";
            this.FrmNo.Name = "FrmNo";
            this.FrmNo.Width = 75;
            // 
            // ToNo
            // 
            this.ToNo.Frozen = true;
            this.ToNo.HeaderText = "To No";
            this.ToNo.Name = "ToNo";
            this.ToNo.Width = 75;
            // 
            // Remarks
            // 
            this.Remarks.Frozen = true;
            this.Remarks.HeaderText = "Remarks";
            this.Remarks.Name = "Remarks";
            // 
            // btnClearItems
            // 
            this.btnClearItems.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClearItems.BackColor = System.Drawing.Color.Moccasin;
            this.btnClearItems.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClearItems.FlatAppearance.BorderSize = 5;
            this.btnClearItems.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClearItems.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClearItems.Location = new System.Drawing.Point(554, 112);
            this.btnClearItems.Margin = new System.Windows.Forms.Padding(1);
            this.btnClearItems.Name = "btnClearItems";
            this.btnClearItems.Size = new System.Drawing.Size(99, 24);
            this.btnClearItems.TabIndex = 10;
            this.btnClearItems.Tag = "Product  Search";
            this.btnClearItems.Text = "Cl&ear Items";
            this.btnClearItems.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearItems.UseVisualStyleBackColor = false;
            this.btnClearItems.Click += new System.EventHandler(this.btnClearItems_Click);
            // 
            // btnItemsSearch
            // 
            this.btnItemsSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnItemsSearch.BackColor = System.Drawing.Color.YellowGreen;
            this.btnItemsSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnItemsSearch.FlatAppearance.BorderSize = 5;
            this.btnItemsSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnItemsSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnItemsSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnItemsSearch.Image")));
            this.btnItemsSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnItemsSearch.Location = new System.Drawing.Point(655, 112);
            this.btnItemsSearch.Margin = new System.Windows.Forms.Padding(1);
            this.btnItemsSearch.Name = "btnItemsSearch";
            this.btnItemsSearch.Size = new System.Drawing.Size(117, 24);
            this.btnItemsSearch.TabIndex = 9;
            this.btnItemsSearch.Tag = "Product  Search";
            this.btnItemsSearch.Text = "+ Add Items";
            this.btnItemsSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnItemsSearch.UseVisualStyleBackColor = false;
            this.btnItemsSearch.Click += new System.EventHandler(this.btnItemsSearch_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(231, 79);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 16);
            this.label7.TabIndex = 71;
            this.label7.Text = "PO Dt.";
            // 
            // dtPO
            // 
            this.dtPO.Checked = false;
            this.dtPO.CustomFormat = "dd/MM/yyyy";
            this.dtPO.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtPO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPO.Location = new System.Drawing.Point(298, 76);
            this.dtPO.Name = "dtPO";
            this.dtPO.Size = new System.Drawing.Size(110, 22);
            this.dtPO.TabIndex = 6;
            this.dtPO.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // txtPo
            // 
            this.txtPo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPo.Location = new System.Drawing.Point(124, 76);
            this.txtPo.MaxLength = 9;
            this.txtPo.Name = "txtPo";
            this.txtPo.Size = new System.Drawing.Size(99, 21);
            this.txtPo.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(5, 79);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 16);
            this.label8.TabIndex = 69;
            this.label8.Text = "PO No.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(5, 108);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 16);
            this.label6.TabIndex = 67;
            this.label6.Text = "Supplier Code";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(599, 52);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 16);
            this.label4.TabIndex = 65;
            this.label4.Text = "DC Dt.";
            // 
            // dtDC
            // 
            this.dtDC.Checked = false;
            this.dtDC.CustomFormat = "dd/MM/yyyy";
            this.dtDC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDC.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDC.Location = new System.Drawing.Point(654, 49);
            this.dtDC.Name = "dtDC";
            this.dtDC.Size = new System.Drawing.Size(110, 22);
            this.dtDC.TabIndex = 4;
            this.dtDC.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // txtDCNo
            // 
            this.txtDCNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDCNo.Location = new System.Drawing.Point(495, 49);
            this.txtDCNo.MaxLength = 9;
            this.txtDCNo.Name = "txtDCNo";
            this.txtDCNo.Size = new System.Drawing.Size(94, 21);
            this.txtDCNo.TabIndex = 3;
            this.txtDCNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDCNo_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(437, 52);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 16);
            this.label5.TabIndex = 63;
            this.label5.Text = "DC No.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(231, 52);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 16);
            this.label3.TabIndex = 61;
            this.label3.Text = "GRN Dt.";
            // 
            // dtGrn
            // 
            this.dtGrn.Checked = false;
            this.dtGrn.CustomFormat = "dd/MM/yyyy";
            this.dtGrn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtGrn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtGrn.Location = new System.Drawing.Point(298, 49);
            this.dtGrn.Name = "dtGrn";
            this.dtGrn.Size = new System.Drawing.Size(110, 22);
            this.dtGrn.TabIndex = 2;
            this.dtGrn.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtGrn.ValueChanged += new System.EventHandler(this.dtGrn_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(5, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 16);
            this.label2.TabIndex = 53;
            this.label2.Text = "Company Name";
            // 
            // cbCompany
            // 
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Location = new System.Drawing.Point(125, 20);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(287, 23);
            this.cbCompany.TabIndex = 0;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // txtGrnNo
            // 
            this.txtGrnNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrnNo.Location = new System.Drawing.Point(124, 49);
            this.txtGrnNo.MaxLength = 9;
            this.txtGrnNo.Name = "txtGrnNo";
            this.txtGrnNo.Size = new System.Drawing.Size(99, 21);
            this.txtGrnNo.TabIndex = 1;
            this.txtGrnNo.Validated += new System.EventHandler(this.txtGrnNo_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(5, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 46;
            this.label1.Text = "GRN No.";
            // 
            // StationaryGRN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(809, 480);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "StationaryGRN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "01";
            this.Load += new System.EventHandler(this.StationaryGRN_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvIndentDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtGrnNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtGrn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtDC;
        private System.Windows.Forms.TextBox txtDCNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtPO;
        private System.Windows.Forms.TextBox txtPo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnItemsSearch;
        private System.Windows.Forms.Button btnClearItems;
        public System.Windows.Forms.DataGridView gvIndentDetails;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtSupplyerName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblBranchCode;
        private System.Windows.Forms.ComboBox cbSupplierCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DcBull;
        private System.Windows.Forms.DataGridViewTextBoxColumn Accepted;
        private System.Windows.Forms.DataGridViewTextBoxColumn Shortage;
        private System.Windows.Forms.DataGridViewTextBoxColumn FrmNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
    }
}