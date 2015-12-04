namespace SSCRM
{
    partial class frmSpecialApprovals
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddDocDetails = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.gvDocumentDetl = new System.Windows.Forms.DataGridView();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.rtbDescription = new System.Windows.Forms.RichTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbApprType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtApprEmpName = new System.Windows.Forms.TextBox();
            this.txtApprEcode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpApprDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.txtApprAmt = new System.Windows.Forms.TextBox();
            this.txtReqAmt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNoOfReq = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPurpose = new System.Windows.Forms.TextBox();
            this.dtpTrnDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReqEmpName = new System.Windows.Forms.TextBox();
            this.txtReqbyEcode = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTrnNo = new System.Windows.Forms.TextBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocImage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImgView = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Del = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvDocumentDetl)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.btnAddDocDetails);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.gvDocumentDetl);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.rtbDescription);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.cbApprType);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtApprEmpName);
            this.groupBox1.Controls.Add(this.txtApprEcode);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.dtpApprDate);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtApprAmt);
            this.groupBox1.Controls.Add(this.txtReqAmt);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtNoOfReq);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPurpose);
            this.groupBox1.Controls.Add(this.dtpTrnDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtReqEmpName);
            this.groupBox1.Controls.Add(this.txtReqbyEcode);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTrnNo);
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(918, 564);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnAddDocDetails
            // 
            this.btnAddDocDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddDocDetails.BackColor = System.Drawing.Color.Green;
            this.btnAddDocDetails.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnAddDocDetails.FlatAppearance.BorderSize = 5;
            this.btnAddDocDetails.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddDocDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDocDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddDocDetails.Location = new System.Drawing.Point(856, 333);
            this.btnAddDocDetails.Margin = new System.Windows.Forms.Padding(1);
            this.btnAddDocDetails.Name = "btnAddDocDetails";
            this.btnAddDocDetails.Size = new System.Drawing.Size(52, 27);
            this.btnAddDocDetails.TabIndex = 27;
            this.btnAddDocDetails.Tag = "";
            this.btnAddDocDetails.Text = "+&Add";
            this.btnAddDocDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddDocDetails.UseVisualStyleBackColor = false;
            this.btnAddDocDetails.Click += new System.EventHandler(this.btnAddDocDetails_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnDelete);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(270, 515);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 45);
            this.groupBox4.TabIndex = 29;
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
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Navy;
            this.label13.Location = new System.Drawing.Point(6, 343);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(125, 15);
            this.label13.TabIndex = 26;
            this.label13.Text = "Document  Details";
            // 
            // gvDocumentDetl
            // 
            this.gvDocumentDetl.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Navy;
            this.gvDocumentDetl.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvDocumentDetl.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvDocumentDetl.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvDocumentDetl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvDocumentDetl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvDocumentDetl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.DocumentName,
            this.DocumentDesc,
            this.DocImage,
            this.ImgView,
            this.Edit,
            this.Del});
            this.gvDocumentDetl.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvDocumentDetl.Location = new System.Drawing.Point(7, 362);
            this.gvDocumentDetl.MultiSelect = false;
            this.gvDocumentDetl.Name = "gvDocumentDetl";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvDocumentDetl.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvDocumentDetl.RowHeadersVisible = false;
            this.gvDocumentDetl.Size = new System.Drawing.Size(901, 153);
            this.gvDocumentDetl.TabIndex = 28;
            this.gvDocumentDetl.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvDocumentDetl_CellClick);
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(74, 292);
            this.txtRemarks.MaxLength = 300;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(835, 37);
            this.txtRemarks.TabIndex = 25;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Navy;
            this.label20.Location = new System.Drawing.Point(6, 295);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(64, 15);
            this.label20.TabIndex = 24;
            this.label20.Text = "Remarks";
            // 
            // rtbDescription
            // 
            this.rtbDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbDescription.Location = new System.Drawing.Point(7, 116);
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.Size = new System.Drawing.Size(902, 171);
            this.rtbDescription.TabIndex = 23;
            this.rtbDescription.Text = "";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Navy;
            this.label12.Location = new System.Drawing.Point(4, 97);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 15);
            this.label12.TabIndex = 22;
            this.label12.Text = "Description";
            // 
            // cbApprType
            // 
            this.cbApprType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbApprType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbApprType.FormattingEnabled = true;
            this.cbApprType.Items.AddRange(new object[] {
            "--SELECT--",
            "ORAL",
            "WRITTEN"});
            this.cbApprType.Location = new System.Drawing.Point(276, 74);
            this.cbApprType.Name = "cbApprType";
            this.cbApprType.Size = new System.Drawing.Size(116, 24);
            this.cbApprType.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.PowderBlue;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label11.Location = new System.Drawing.Point(184, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 15);
            this.label11.TabIndex = 17;
            this.label11.Text = "Appr Method";
            // 
            // txtApprEmpName
            // 
            this.txtApprEmpName.BackColor = System.Drawing.SystemColors.Info;
            this.txtApprEmpName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApprEmpName.Location = new System.Drawing.Point(591, 73);
            this.txtApprEmpName.Name = "txtApprEmpName";
            this.txtApprEmpName.ReadOnly = true;
            this.txtApprEmpName.Size = new System.Drawing.Size(318, 23);
            this.txtApprEmpName.TabIndex = 21;
            this.txtApprEmpName.TabStop = false;
            // 
            // txtApprEcode
            // 
            this.txtApprEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApprEcode.Location = new System.Drawing.Point(511, 73);
            this.txtApprEcode.MaxLength = 7;
            this.txtApprEcode.Name = "txtApprEcode";
            this.txtApprEcode.Size = new System.Drawing.Size(79, 23);
            this.txtApprEcode.TabIndex = 20;
            this.txtApprEcode.Validated += new System.EventHandler(this.txtApprEcode_Validated);
            this.txtApprEcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtApprEcode_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label10.Location = new System.Drawing.Point(419, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 15);
            this.label10.TabIndex = 19;
            this.label10.Text = "Approved By";
            // 
            // dtpApprDate
            // 
            this.dtpApprDate.CustomFormat = "dd/MM/yyyy";
            this.dtpApprDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpApprDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpApprDate.Location = new System.Drawing.Point(81, 74);
            this.dtpApprDate.Name = "dtpApprDate";
            this.dtpApprDate.Size = new System.Drawing.Size(96, 22);
            this.dtpApprDate.TabIndex = 16;
            this.dtpApprDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label9.Location = new System.Drawing.Point(4, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 15);
            this.label9.TabIndex = 15;
            this.label9.Text = "Appr Date";
            // 
            // txtApprAmt
            // 
            this.txtApprAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApprAmt.Location = new System.Drawing.Point(821, 44);
            this.txtApprAmt.MaxLength = 10;
            this.txtApprAmt.Name = "txtApprAmt";
            this.txtApprAmt.Size = new System.Drawing.Size(88, 23);
            this.txtApprAmt.TabIndex = 14;
            this.txtApprAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtApprAmt_KeyPress);
            // 
            // txtReqAmt
            // 
            this.txtReqAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReqAmt.Location = new System.Drawing.Point(657, 44);
            this.txtReqAmt.MaxLength = 10;
            this.txtReqAmt.Name = "txtReqAmt";
            this.txtReqAmt.Size = new System.Drawing.Size(92, 23);
            this.txtReqAmt.TabIndex = 12;
            this.txtReqAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReqAmt_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(752, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 15);
            this.label8.TabIndex = 13;
            this.label8.Text = "Appr Amt.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(591, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "Req Amt.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(397, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "No.Of Requests";
            // 
            // txtNoOfReq
            // 
            this.txtNoOfReq.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoOfReq.Location = new System.Drawing.Point(512, 44);
            this.txtNoOfReq.MaxLength = 10;
            this.txtNoOfReq.Name = "txtNoOfReq";
            this.txtNoOfReq.Size = new System.Drawing.Size(76, 23);
            this.txtNoOfReq.TabIndex = 10;
            this.txtNoOfReq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoOfReq_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(14, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Purpose";
            // 
            // txtPurpose
            // 
            this.txtPurpose.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPurpose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPurpose.Location = new System.Drawing.Point(81, 45);
            this.txtPurpose.MaxLength = 50;
            this.txtPurpose.Name = "txtPurpose";
            this.txtPurpose.Size = new System.Drawing.Size(311, 23);
            this.txtPurpose.TabIndex = 8;
            this.txtPurpose.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPurpose_KeyPress);
            // 
            // dtpTrnDate
            // 
            this.dtpTrnDate.CustomFormat = "dd/MM/yyyy";
            this.dtpTrnDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTrnDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTrnDate.Location = new System.Drawing.Point(291, 16);
            this.dtpTrnDate.Name = "dtpTrnDate";
            this.dtpTrnDate.Size = new System.Drawing.Size(99, 22);
            this.dtpTrnDate.TabIndex = 3;
            this.dtpTrnDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(223, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Trn Date";
            // 
            // txtReqEmpName
            // 
            this.txtReqEmpName.BackColor = System.Drawing.SystemColors.Info;
            this.txtReqEmpName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReqEmpName.Location = new System.Drawing.Point(590, 15);
            this.txtReqEmpName.Name = "txtReqEmpName";
            this.txtReqEmpName.ReadOnly = true;
            this.txtReqEmpName.Size = new System.Drawing.Size(319, 22);
            this.txtReqEmpName.TabIndex = 6;
            this.txtReqEmpName.TabStop = false;
            // 
            // txtReqbyEcode
            // 
            this.txtReqbyEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReqbyEcode.Location = new System.Drawing.Point(510, 15);
            this.txtReqbyEcode.MaxLength = 7;
            this.txtReqbyEcode.Name = "txtReqbyEcode";
            this.txtReqbyEcode.Size = new System.Drawing.Size(79, 23);
            this.txtReqbyEcode.TabIndex = 5;
            this.txtReqbyEcode.Validated += new System.EventHandler(this.txtReqbyEcode_Validated);
            this.txtReqbyEcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReqbyEcode_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label15.Location = new System.Drawing.Point(409, 18);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 15);
            this.label15.TabIndex = 4;
            this.label15.Text = "Requested By";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(24, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trn No";
            // 
            // txtTrnNo
            // 
            this.txtTrnNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrnNo.Location = new System.Drawing.Point(81, 16);
            this.txtTrnNo.MaxLength = 10;
            this.txtTrnNo.Name = "txtTrnNo";
            this.txtTrnNo.Size = new System.Drawing.Size(101, 23);
            this.txtTrnNo.TabIndex = 1;
            this.txtTrnNo.Validated += new System.EventHandler(this.txtTrnNo_Validated);
            this.txtTrnNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTrnNo_KeyPress);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Edit";
            this.dataGridViewImageColumn1.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Visible = false;
            this.dataGridViewImageColumn1.Width = 40;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "Del";
            this.dataGridViewImageColumn2.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.ReadOnly = true;
            this.dataGridViewImageColumn2.Width = 40;
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
            // DocumentName
            // 
            this.DocumentName.HeaderText = "Document Name";
            this.DocumentName.Name = "DocumentName";
            this.DocumentName.ReadOnly = true;
            this.DocumentName.Width = 220;
            // 
            // DocumentDesc
            // 
            this.DocumentDesc.HeaderText = "Document Desc";
            this.DocumentDesc.Name = "DocumentDesc";
            this.DocumentDesc.ReadOnly = true;
            this.DocumentDesc.Width = 280;
            // 
            // DocImage
            // 
            this.DocImage.HeaderText = "Doc / Img";
            this.DocImage.Name = "DocImage";
            this.DocImage.ReadOnly = true;
            this.DocImage.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DocImage.Width = 180;
            // 
            // ImgView
            // 
            this.ImgView.HeaderText = "";
            this.ImgView.Name = "ImgView";
            this.ImgView.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ImgView.Text = "View Image";
            this.ImgView.UseColumnTextForLinkValue = true;
            this.ImgView.Width = 90;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.Edit.Name = "Edit";
            this.Edit.Visible = false;
            this.Edit.Width = 40;
            // 
            // Del
            // 
            this.Del.HeaderText = "Del";
            this.Del.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.Del.Name = "Del";
            this.Del.ReadOnly = true;
            this.Del.Width = 40;
            // 
            // frmSpecialApprovals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(924, 570);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSpecialApprovals";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Special  Approvals";
            this.Load += new System.EventHandler(this.frmSpecialApprovals_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvDocumentDetl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTrnNo;
        private System.Windows.Forms.TextBox txtReqEmpName;
        private System.Windows.Forms.TextBox txtReqbyEcode;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker dtpTrnDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPurpose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNoOfReq;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtReqAmt;
        private System.Windows.Forms.TextBox txtApprAmt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtApprEmpName;
        private System.Windows.Forms.TextBox txtApprEcode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpApprDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbApprType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RichTextBox rtbDescription;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label20;
        public System.Windows.Forms.DataGridView gvDocumentDetl;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAddDocDetails;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocImage;
        private System.Windows.Forms.DataGridViewLinkColumn ImgView;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Del;
    }
}