namespace SSCRM
{
    partial class SalesReceipt
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRefNo = new System.Windows.Forms.TextBox();
            this.txtRevenue = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotReciepts = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.gvReceiptList = new System.Windows.Forms.DataGridView();
            this.SlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ecode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Names = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceiptNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RefNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.cbRefType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dtpReceiptDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDocMonth = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEcodeSearch = new System.Windows.Forms.TextBox();
            this.cbEcode = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtReceiptNo = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvReceiptList)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRefNo);
            this.groupBox1.Controls.Add(this.txtRevenue);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtTotReciepts);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.gvReceiptList);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.cbRefType);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.dtpReceiptDate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtDocMonth);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEcodeSearch);
            this.groupBox1.Controls.Add(this.cbEcode);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtReceiptNo);
            this.groupBox1.Location = new System.Drawing.Point(4, -6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(741, 476);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtRefNo
            // 
            this.txtRefNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefNo.Location = new System.Drawing.Point(545, 79);
            this.txtRefNo.MaxLength = 20;
            this.txtRefNo.Name = "txtRefNo";
            this.txtRefNo.Size = new System.Drawing.Size(156, 21);
            this.txtRefNo.TabIndex = 5;
            // 
            // txtRevenue
            // 
            this.txtRevenue.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtRevenue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRevenue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRevenue.Location = new System.Drawing.Point(579, 189);
            this.txtRevenue.MaxLength = 20;
            this.txtRevenue.Name = "txtRevenue";
            this.txtRevenue.ReadOnly = true;
            this.txtRevenue.Size = new System.Drawing.Size(104, 21);
            this.txtRevenue.TabIndex = 68;
            this.txtRevenue.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(400, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 17);
            this.label7.TabIndex = 66;
            this.label7.Text = "DD/Cheque Ref No";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label9.Location = new System.Drawing.Point(460, 191);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 17);
            this.label9.TabIndex = 69;
            this.label9.Text = "Total Revenue";
            this.label9.Visible = false;
            // 
            // txtTotReciepts
            // 
            this.txtTotReciepts.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtTotReciepts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotReciepts.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotReciepts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtTotReciepts.Location = new System.Drawing.Point(502, 147);
            this.txtTotReciepts.MaxLength = 20;
            this.txtTotReciepts.Name = "txtTotReciepts";
            this.txtTotReciepts.ReadOnly = true;
            this.txtTotReciepts.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotReciepts.Size = new System.Drawing.Size(199, 38);
            this.txtTotReciepts.TabIndex = 66;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(454, 164);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 17);
            this.label8.TabIndex = 67;
            this.label8.Text = "Total";
            // 
            // gvReceiptList
            // 
            this.gvReceiptList.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.gvReceiptList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvReceiptList.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvReceiptList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvReceiptList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvReceiptList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvReceiptList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SlNo,
            this.Ecode,
            this.Names,
            this.Type,
            this.ReceiptNo,
            this.Date,
            this.Amount,
            this.Remarks,
            this.id,
            this.RefNo,
            this.CreatedDate,
            this.Edit});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvReceiptList.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvReceiptList.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvReceiptList.Location = new System.Drawing.Point(6, 202);
            this.gvReceiptList.MultiSelect = false;
            this.gvReceiptList.Name = "gvReceiptList";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvReceiptList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvReceiptList.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.MidnightBlue;
            this.gvReceiptList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvReceiptList.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.gvReceiptList.Size = new System.Drawing.Size(725, 267);
            this.gvReceiptList.TabIndex = 55;
            this.gvReceiptList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvReceiptList_CellClick);
            // 
            // SlNo
            // 
            this.SlNo.Frozen = true;
            this.SlNo.HeaderText = "SlNo";
            this.SlNo.Name = "SlNo";
            this.SlNo.ReadOnly = true;
            this.SlNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SlNo.Width = 40;
            // 
            // Ecode
            // 
            this.Ecode.Frozen = true;
            this.Ecode.HeaderText = "Ecode";
            this.Ecode.Name = "Ecode";
            this.Ecode.Visible = false;
            // 
            // Names
            // 
            this.Names.Frozen = true;
            this.Names.HeaderText = "Emp Name";
            this.Names.Name = "Names";
            this.Names.Width = 200;
            // 
            // Type
            // 
            this.Type.Frozen = true;
            this.Type.HeaderText = "Reciept Type";
            this.Type.Name = "Type";
            // 
            // ReceiptNo
            // 
            this.ReceiptNo.Frozen = true;
            this.ReceiptNo.HeaderText = "Receipt No";
            this.ReceiptNo.MinimumWidth = 20;
            this.ReceiptNo.Name = "ReceiptNo";
            this.ReceiptNo.ReadOnly = true;
            this.ReceiptNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ReceiptNo.Width = 110;
            // 
            // Date
            // 
            this.Date.Frozen = true;
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // Amount
            // 
            this.Amount.Frozen = true;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            // 
            // Remarks
            // 
            this.Remarks.Frozen = true;
            this.Remarks.HeaderText = "Remarks";
            this.Remarks.Name = "Remarks";
            this.Remarks.Visible = false;
            // 
            // id
            // 
            this.id.Frozen = true;
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // RefNo
            // 
            this.RefNo.Frozen = true;
            this.RefNo.HeaderText = "RefNo";
            this.RefNo.Name = "RefNo";
            this.RefNo.Visible = false;
            // 
            // CreatedDate
            // 
            this.CreatedDate.Frozen = true;
            this.CreatedDate.HeaderText = "CreatedDate";
            this.CreatedDate.Name = "CreatedDate";
            this.CreatedDate.ReadOnly = true;
            this.CreatedDate.Visible = false;
            // 
            // Edit
            // 
            this.Edit.Frozen = true;
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.Edit.Name = "Edit";
            this.Edit.Width = 40;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(117, 104);
            this.txtRemarks.MaxLength = 4000;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(583, 34);
            this.txtRemarks.TabIndex = 6;
            // 
            // cbRefType
            // 
            this.cbRefType.AllowDrop = true;
            this.cbRefType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbRefType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbRefType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRefType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRefType.FormattingEnabled = true;
            this.cbRefType.Location = new System.Drawing.Point(325, 24);
            this.cbRefType.Name = "cbRefType";
            this.cbRefType.Size = new System.Drawing.Size(157, 23);
            this.cbRefType.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(44, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 17);
            this.label5.TabIndex = 54;
            this.label5.Text = "Remarks";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(225, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 17);
            this.label6.TabIndex = 64;
            this.label6.Text = "ReceiptType";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnDelete);
            this.groupBox5.Controls.Add(this.btnClose);
            this.groupBox5.Controls.Add(this.btnCancel);
            this.groupBox5.Controls.Add(this.btnSave);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox5.Location = new System.Drawing.Point(42, 142);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(409, 45);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.Location = new System.Drawing.Point(206, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(74, 26);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(287, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 26);
            this.btnClose.TabIndex = 2;
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
            this.btnCancel.Location = new System.Drawing.Point(128, 13);
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
            this.btnSave.Location = new System.Drawing.Point(48, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtpReceiptDate
            // 
            this.dtpReceiptDate.CustomFormat = "dd/MM/yyyy";
            this.dtpReceiptDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpReceiptDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReceiptDate.Location = new System.Drawing.Point(116, 77);
            this.dtpReceiptDate.Name = "dtpReceiptDate";
            this.dtpReceiptDate.Size = new System.Drawing.Size(98, 23);
            this.dtpReceiptDate.TabIndex = 4;
            this.dtpReceiptDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(75, 80);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 16);
            this.label3.TabIndex = 49;
            this.label3.Text = "Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(39, 28);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 16);
            this.label4.TabIndex = 41;
            this.label4.Text = "DocMonth";
            // 
            // txtDocMonth
            // 
            this.txtDocMonth.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtDocMonth.Location = new System.Drawing.Point(116, 26);
            this.txtDocMonth.MaxLength = 15;
            this.txtDocMonth.Name = "txtDocMonth";
            this.txtDocMonth.ReadOnly = true;
            this.txtDocMonth.Size = new System.Drawing.Size(87, 20);
            this.txtDocMonth.TabIndex = 40;
            this.txtDocMonth.TabStop = false;
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(596, 51);
            this.txtAmount.MaxLength = 20;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(104, 21);
            this.txtAmount.TabIndex = 3;
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(530, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 48;
            this.label2.Text = "Amount";
            // 
            // txtEcodeSearch
            // 
            this.txtEcodeSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcodeSearch.Location = new System.Drawing.Point(117, 51);
            this.txtEcodeSearch.MaxLength = 20;
            this.txtEcodeSearch.Name = "txtEcodeSearch";
            this.txtEcodeSearch.Size = new System.Drawing.Size(86, 21);
            this.txtEcodeSearch.TabIndex = 2;
            this.txtEcodeSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEcodeSearch_KeyUp);
            // 
            // cbEcode
            // 
            this.cbEcode.AllowDrop = true;
            this.cbEcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbEcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEcode.FormattingEnabled = true;
            this.cbEcode.Location = new System.Drawing.Point(204, 50);
            this.cbEcode.Name = "cbEcode";
            this.cbEcode.Size = new System.Drawing.Size(280, 23);
            this.cbEcode.TabIndex = 45;
            this.cbEcode.SelectedIndexChanged += new System.EventHandler(this.cbEcode_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label16.Location = new System.Drawing.Point(63, 53);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 17);
            this.label16.TabIndex = 46;
            this.label16.Text = "Ecode";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(506, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 16);
            this.label1.TabIndex = 43;
            this.label1.Text = "Receipt No";
            // 
            // txtReceiptNo
            // 
            this.txtReceiptNo.BackColor = System.Drawing.Color.White;
            this.txtReceiptNo.Location = new System.Drawing.Point(596, 27);
            this.txtReceiptNo.MaxLength = 15;
            this.txtReceiptNo.Name = "txtReceiptNo";
            this.txtReceiptNo.Size = new System.Drawing.Size(103, 20);
            this.txtReceiptNo.TabIndex = 1;
            this.txtReceiptNo.TabStop = false;
            // 
            // SalesReceipt
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(747, 475);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "SalesReceipt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Receipt";
            this.Load += new System.EventHandler(this.Receipt_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvReceiptList)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtDocMonth;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtReceiptNo;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEcodeSearch;
        private System.Windows.Forms.ComboBox cbEcode;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.DateTimePicker dtpReceiptDate;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.DataGridView gvReceiptList;
        private System.Windows.Forms.TextBox txtRefNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbRefType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ecode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Names;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiptNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remarks;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn RefNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedDate;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.TextBox txtTotReciepts;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtRevenue;
        private System.Windows.Forms.Label label9;
    }
}