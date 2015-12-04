namespace SSCRM
{
    partial class frmCombiSplitting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpDocMonth = new System.Windows.Forms.DateTimePicker();
            this.lblDocMonth = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gvSingleProdDetails = new System.Windows.Forms.DataGridView();
            this.SlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SCombiProdId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SingleProdId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SingleProdName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prodQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SingleProdMrp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SinProdPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProdTotMRP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SinProdTotPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gvCombiProdDetails = new System.Windows.Forms.DataGridView();
            this.CombiSlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CombiId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FinYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CombiName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CombiPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CombiPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.lblCompany = new System.Windows.Forms.Label();
            this.lblBranch = new System.Windows.Forms.Label();
            this.cbUserBranch = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSingleProdDetails)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCombiProdDetails)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.dtpDocMonth);
            this.groupBox1.Controls.Add(this.lblDocMonth);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.lblCompany);
            this.groupBox1.Controls.Add(this.lblBranch);
            this.groupBox1.Controls.Add(this.cbUserBranch);
            this.groupBox1.Location = new System.Drawing.Point(4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(878, 571);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // dtpDocMonth
            // 
            this.dtpDocMonth.CustomFormat = "MMM/yyyy";
            this.dtpDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDocMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDocMonth.Location = new System.Drawing.Point(95, 38);
            this.dtpDocMonth.Name = "dtpDocMonth";
            this.dtpDocMonth.Size = new System.Drawing.Size(91, 22);
            this.dtpDocMonth.TabIndex = 129;
            this.dtpDocMonth.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpDocMonth.ValueChanged += new System.EventHandler(this.dtpDocMonth_ValueChanged);
            // 
            // lblDocMonth
            // 
            this.lblDocMonth.AutoSize = true;
            this.lblDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocMonth.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblDocMonth.Location = new System.Drawing.Point(8, 40);
            this.lblDocMonth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDocMonth.Name = "lblDocMonth";
            this.lblDocMonth.Size = new System.Drawing.Size(85, 17);
            this.lblDocMonth.TabIndex = 127;
            this.lblDocMonth.Text = "Doc Month";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gvSingleProdDetails);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Navy;
            this.groupBox3.Location = new System.Drawing.Point(6, 347);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(866, 176);
            this.groupBox3.TabIndex = 126;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Single Products";
            // 
            // gvSingleProdDetails
            // 
            this.gvSingleProdDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Navy;
            this.gvSingleProdDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvSingleProdDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvSingleProdDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSingleProdDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvSingleProdDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSingleProdDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SlNo,
            this.SCombiProdId,
            this.SingleProdId,
            this.SingleProdName,
            this.Category,
            this.prodQty,
            this.SingleProdMrp,
            this.SinProdPoints,
            this.ProdTotMRP,
            this.SinProdTotPoints});
            this.gvSingleProdDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvSingleProdDetails.Location = new System.Drawing.Point(5, 22);
            this.gvSingleProdDetails.MultiSelect = false;
            this.gvSingleProdDetails.Name = "gvSingleProdDetails";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSingleProdDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvSingleProdDetails.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.gvSingleProdDetails.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvSingleProdDetails.Size = new System.Drawing.Size(856, 149);
            this.gvSingleProdDetails.TabIndex = 20;
            this.gvSingleProdDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSingleProdDetails_CellEndEdit);
            this.gvSingleProdDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvSingleProdDetails_EditingControlShowing);
            // 
            // SlNo
            // 
            this.SlNo.HeaderText = "Sl.No";
            this.SlNo.Name = "SlNo";
            this.SlNo.ReadOnly = true;
            this.SlNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SlNo.Width = 50;
            // 
            // SCombiProdId
            // 
            this.SCombiProdId.HeaderText = "CombiProdId";
            this.SCombiProdId.Name = "SCombiProdId";
            this.SCombiProdId.ReadOnly = true;
            this.SCombiProdId.Visible = false;
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
            this.SingleProdName.HeaderText = "Product Name";
            this.SingleProdName.Name = "SingleProdName";
            this.SingleProdName.ReadOnly = true;
            this.SingleProdName.Width = 170;
            // 
            // Category
            // 
            this.Category.HeaderText = "Category";
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
            this.Category.Width = 140;
            // 
            // prodQty
            // 
            this.prodQty.HeaderText = "QTY";
            this.prodQty.Name = "prodQty";
            this.prodQty.ReadOnly = true;
            this.prodQty.Width = 80;
            // 
            // SingleProdMrp
            // 
            this.SingleProdMrp.HeaderText = "MRP";
            this.SingleProdMrp.Name = "SingleProdMrp";
            this.SingleProdMrp.Width = 90;
            // 
            // SinProdPoints
            // 
            this.SinProdPoints.HeaderText = "Points";
            this.SinProdPoints.Name = "SinProdPoints";
            this.SinProdPoints.Width = 90;
            // 
            // ProdTotMRP
            // 
            this.ProdTotMRP.HeaderText = "Tot Mrp";
            this.ProdTotMRP.Name = "ProdTotMRP";
            this.ProdTotMRP.ReadOnly = true;
            // 
            // SinProdTotPoints
            // 
            this.SinProdTotPoints.HeaderText = "Tot Points";
            this.SinProdTotPoints.Name = "SinProdTotPoints";
            this.SinProdTotPoints.ReadOnly = true;
            this.SinProdTotPoints.Width = 110;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gvCombiProdDetails);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(7, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(865, 282);
            this.groupBox2.TabIndex = 125;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Combi Products";
            // 
            // gvCombiProdDetails
            // 
            this.gvCombiProdDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Navy;
            this.gvCombiProdDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvCombiProdDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvCombiProdDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCombiProdDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gvCombiProdDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvCombiProdDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CombiSlNo,
            this.CombiId,
            this.FinYear,
            this.CombiName,
            this.CombiPrice,
            this.CombiPoints,
            this.Edit});
            this.gvCombiProdDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvCombiProdDetails.Location = new System.Drawing.Point(5, 24);
            this.gvCombiProdDetails.MultiSelect = false;
            this.gvCombiProdDetails.Name = "gvCombiProdDetails";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvCombiProdDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gvCombiProdDetails.RowHeadersVisible = false;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            this.gvCombiProdDetails.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.gvCombiProdDetails.Size = new System.Drawing.Size(854, 251);
            this.gvCombiProdDetails.TabIndex = 20;
            this.gvCombiProdDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvCombiProdDetails_CellClick);
            // 
            // CombiSlNo
            // 
            this.CombiSlNo.Frozen = true;
            this.CombiSlNo.HeaderText = "Sl.No";
            this.CombiSlNo.Name = "CombiSlNo";
            this.CombiSlNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CombiSlNo.Width = 50;
            // 
            // CombiId
            // 
            this.CombiId.HeaderText = "CombiId";
            this.CombiId.Name = "CombiId";
            this.CombiId.ReadOnly = true;
            this.CombiId.Visible = false;
            this.CombiId.Width = 150;
            // 
            // FinYear
            // 
            this.FinYear.HeaderText = "FinYear";
            this.FinYear.Name = "FinYear";
            this.FinYear.ReadOnly = true;
            this.FinYear.Visible = false;
            // 
            // CombiName
            // 
            this.CombiName.HeaderText = "Combi Name";
            this.CombiName.Name = "CombiName";
            this.CombiName.ReadOnly = true;
            this.CombiName.Width = 500;
            // 
            // CombiPrice
            // 
            this.CombiPrice.HeaderText = "Price";
            this.CombiPrice.Name = "CombiPrice";
            this.CombiPrice.ReadOnly = true;
            // 
            // CombiPoints
            // 
            this.CombiPoints.HeaderText = "Points";
            this.CombiPoints.Name = "CombiPoints";
            this.CombiPoints.ReadOnly = true;
            this.CombiPoints.Width = 80;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.Edit.Name = "Edit";
            this.Edit.Width = 60;
            // 
            // groupBox5
            // 
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
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(214, 13);
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
            this.btnCancel.Location = new System.Drawing.Point(135, 13);
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
            this.btnSave.Location = new System.Drawing.Point(56, 13);
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
            this.cbCompany.Location = new System.Drawing.Point(95, 11);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(335, 24);
            this.cbCompany.TabIndex = 32;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblCompany.Location = new System.Drawing.Point(20, 15);
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
            this.lblBranch.Location = new System.Drawing.Point(437, 16);
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
            this.cbUserBranch.Location = new System.Drawing.Point(497, 12);
            this.cbUserBranch.Name = "cbUserBranch";
            this.cbUserBranch.Size = new System.Drawing.Size(367, 24);
            this.cbUserBranch.TabIndex = 31;
            this.cbUserBranch.SelectedIndexChanged += new System.EventHandler(this.cbUserBranch_SelectedIndexChanged);
            // 
            // frmCombiSplitting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(886, 576);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCombiSplitting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Combi Splitting";
            this.Load += new System.EventHandler(this.frmCombiSplitting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvSingleProdDetails)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvCombiProdDetails)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDocMonth;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.DataGridView gvSingleProdDetails;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.DataGridView gvCombiProdDetails;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.ComboBox cbUserBranch;
        private System.Windows.Forms.DateTimePicker dtpDocMonth;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SCombiProdId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SingleProdId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SingleProdName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn SingleProdMrp;
        private System.Windows.Forms.DataGridViewTextBoxColumn SinProdPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProdTotMRP;
        private System.Windows.Forms.DataGridViewTextBoxColumn SinProdTotPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn CombiSlNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CombiId;
        private System.Windows.Forms.DataGridViewTextBoxColumn FinYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn CombiName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CombiPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn CombiPoints;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
    }
}