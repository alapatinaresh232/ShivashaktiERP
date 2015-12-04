namespace SSCRM
{
    partial class SalesPromotionEligibulityMaster
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gvSalePromotionDetails = new System.Windows.Forms.DataGridView();
            this.slNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PromotionType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExcellentMonths = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExcellentPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GoodMonths = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GoodPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AverageMonths = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AveragePoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExtOrdMnths = new System.Windows.Forms.TextBox();
            this.txtExtOrdPts = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAvgMonths = new System.Windows.Forms.TextBox();
            this.txtAvgPoints = new System.Windows.Forms.TextBox();
            this.txtGoodMonths = new System.Windows.Forms.TextBox();
            this.txtGoodPoints = new System.Windows.Forms.TextBox();
            this.txtExeMonts = new System.Windows.Forms.TextBox();
            this.lbaSalBasic = new System.Windows.Forms.Label();
            this.txtExePoints = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbPromotionType = new System.Windows.Forms.ComboBox();
            this.lblSalStructType = new System.Windows.Forms.Label();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSalePromotionDetails)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.gvSalePromotionDetails);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cbPromotionType);
            this.groupBox1.Controls.Add(this.lblSalStructType);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 476);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnDelete);
            this.groupBox6.Controls.Add(this.btnClose);
            this.groupBox6.Controls.Add(this.btnClear);
            this.groupBox6.Controls.Add(this.btnSave);
            this.groupBox6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox6.Location = new System.Drawing.Point(185, 421);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(363, 47);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.Location = new System.Drawing.Point(104, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(74, 26);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "D&elete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(262, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 26);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "C&lose";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClear.Location = new System.Drawing.Point(183, 13);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(74, 26);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Clea&r";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoEllipsis = true;
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(25, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gvSalePromotionDetails
            // 
            this.gvSalePromotionDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvSalePromotionDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvSalePromotionDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvSalePromotionDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSalePromotionDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvSalePromotionDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSalePromotionDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.slNo,
            this.DeptId,
            this.PromotionType,
            this.ExcellentMonths,
            this.ExcellentPoints,
            this.GoodMonths,
            this.GoodPoints,
            this.AverageMonths,
            this.AveragePoints,
            this.Edit});
            this.gvSalePromotionDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvSalePromotionDetails.Location = new System.Drawing.Point(8, 150);
            this.gvSalePromotionDetails.MultiSelect = false;
            this.gvSalePromotionDetails.Name = "gvSalePromotionDetails";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSalePromotionDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvSalePromotionDetails.RowHeadersVisible = false;
            this.gvSalePromotionDetails.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.gvSalePromotionDetails.Size = new System.Drawing.Size(724, 269);
            this.gvSalePromotionDetails.TabIndex = 6;
            this.gvSalePromotionDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSalePromotionDetails_CellClick);
            // 
            // slNo
            // 
            this.slNo.Frozen = true;
            this.slNo.HeaderText = "Sl.No";
            this.slNo.Name = "slNo";
            this.slNo.ReadOnly = true;
            this.slNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.slNo.Width = 50;
            // 
            // DeptId
            // 
            this.DeptId.HeaderText = "DeptId";
            this.DeptId.Name = "DeptId";
            this.DeptId.ReadOnly = true;
            this.DeptId.Visible = false;
            this.DeptId.Width = 200;
            // 
            // PromotionType
            // 
            this.PromotionType.HeaderText = "Promotion Type";
            this.PromotionType.Name = "PromotionType";
            this.PromotionType.ReadOnly = true;
            this.PromotionType.Width = 120;
            // 
            // ExcellentMonths
            // 
            this.ExcellentMonths.HeaderText = "Excellent Months";
            this.ExcellentMonths.Name = "ExcellentMonths";
            this.ExcellentMonths.ReadOnly = true;
            // 
            // ExcellentPoints
            // 
            this.ExcellentPoints.HeaderText = "Excellent Points";
            this.ExcellentPoints.Name = "ExcellentPoints";
            this.ExcellentPoints.ReadOnly = true;
            // 
            // GoodMonths
            // 
            this.GoodMonths.HeaderText = "Good Months";
            this.GoodMonths.Name = "GoodMonths";
            this.GoodMonths.ReadOnly = true;
            this.GoodMonths.Width = 75;
            // 
            // GoodPoints
            // 
            this.GoodPoints.HeaderText = "Good Points";
            this.GoodPoints.Name = "GoodPoints";
            this.GoodPoints.ReadOnly = true;
            this.GoodPoints.Width = 75;
            // 
            // AverageMonths
            // 
            this.AverageMonths.HeaderText = "Average Months";
            this.AverageMonths.Name = "AverageMonths";
            this.AverageMonths.ReadOnly = true;
            this.AverageMonths.Width = 80;
            // 
            // AveragePoints
            // 
            this.AveragePoints.HeaderText = "Average Points";
            this.AveragePoints.Name = "AveragePoints";
            this.AveragePoints.ReadOnly = true;
            this.AveragePoints.Width = 80;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "";
            this.Edit.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.Edit.Name = "Edit";
            this.Edit.Width = 30;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtExtOrdMnths);
            this.groupBox2.Controls.Add(this.txtExtOrdPts);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtAvgMonths);
            this.groupBox2.Controls.Add(this.txtAvgPoints);
            this.groupBox2.Controls.Add(this.txtGoodMonths);
            this.groupBox2.Controls.Add(this.txtGoodPoints);
            this.groupBox2.Controls.Add(this.txtExeMonts);
            this.groupBox2.Controls.Add(this.lbaSalBasic);
            this.groupBox2.Controls.Add(this.txtExePoints);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.groupBox2.Location = new System.Drawing.Point(8, 48);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox2.Size = new System.Drawing.Size(722, 99);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Points Creteria";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.PowderBlue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(210, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "Ex.Ord";
            // 
            // txtExtOrdMnths
            // 
            this.txtExtOrdMnths.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtExtOrdMnths.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExtOrdMnths.Location = new System.Drawing.Point(185, 35);
            this.txtExtOrdMnths.MaxLength = 20;
            this.txtExtOrdMnths.Name = "txtExtOrdMnths";
            this.txtExtOrdMnths.Size = new System.Drawing.Size(106, 22);
            this.txtExtOrdMnths.TabIndex = 17;
            // 
            // txtExtOrdPts
            // 
            this.txtExtOrdPts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExtOrdPts.Location = new System.Drawing.Point(185, 62);
            this.txtExtOrdPts.MaxLength = 20;
            this.txtExtOrdPts.Name = "txtExtOrdPts";
            this.txtExtOrdPts.Size = new System.Drawing.Size(106, 22);
            this.txtExtOrdPts.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.PowderBlue;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(529, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 16);
            this.label5.TabIndex = 16;
            this.label5.Text = "Average";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.PowderBlue;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(432, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "Good";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.PowderBlue;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(306, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Excellent";
            // 
            // txtAvgMonths
            // 
            this.txtAvgMonths.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAvgMonths.Location = new System.Drawing.Point(508, 35);
            this.txtAvgMonths.MaxLength = 20;
            this.txtAvgMonths.Name = "txtAvgMonths";
            this.txtAvgMonths.Size = new System.Drawing.Size(106, 22);
            this.txtAvgMonths.TabIndex = 5;
            this.txtAvgMonths.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAvgMonths_KeyPress);
            // 
            // txtAvgPoints
            // 
            this.txtAvgPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAvgPoints.Location = new System.Drawing.Point(508, 62);
            this.txtAvgPoints.MaxLength = 20;
            this.txtAvgPoints.Name = "txtAvgPoints";
            this.txtAvgPoints.Size = new System.Drawing.Size(106, 22);
            this.txtAvgPoints.TabIndex = 8;
            this.txtAvgPoints.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAvgPoints_KeyPress);
            // 
            // txtGoodMonths
            // 
            this.txtGoodMonths.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGoodMonths.Location = new System.Drawing.Point(400, 35);
            this.txtGoodMonths.MaxLength = 20;
            this.txtGoodMonths.Name = "txtGoodMonths";
            this.txtGoodMonths.Size = new System.Drawing.Size(106, 22);
            this.txtGoodMonths.TabIndex = 4;
            this.txtGoodMonths.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGoodMonths_KeyPress);
            // 
            // txtGoodPoints
            // 
            this.txtGoodPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGoodPoints.Location = new System.Drawing.Point(400, 62);
            this.txtGoodPoints.MaxLength = 20;
            this.txtGoodPoints.Name = "txtGoodPoints";
            this.txtGoodPoints.Size = new System.Drawing.Size(106, 22);
            this.txtGoodPoints.TabIndex = 7;
            this.txtGoodPoints.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGoodPoints_KeyPress);
            // 
            // txtExeMonts
            // 
            this.txtExeMonts.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtExeMonts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExeMonts.Location = new System.Drawing.Point(292, 35);
            this.txtExeMonts.MaxLength = 20;
            this.txtExeMonts.Name = "txtExeMonts";
            this.txtExeMonts.Size = new System.Drawing.Size(106, 22);
            this.txtExeMonts.TabIndex = 3;
            this.txtExeMonts.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtExeMonts_KeyPress);
            // 
            // lbaSalBasic
            // 
            this.lbaSalBasic.AutoSize = true;
            this.lbaSalBasic.BackColor = System.Drawing.Color.PowderBlue;
            this.lbaSalBasic.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbaSalBasic.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lbaSalBasic.Location = new System.Drawing.Point(111, 39);
            this.lbaSalBasic.Name = "lbaSalBasic";
            this.lbaSalBasic.Size = new System.Drawing.Size(73, 16);
            this.lbaSalBasic.TabIndex = 13;
            this.lbaSalBasic.Text = "MONTHS";
            // 
            // txtExePoints
            // 
            this.txtExePoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExePoints.Location = new System.Drawing.Point(292, 62);
            this.txtExePoints.MaxLength = 20;
            this.txtExePoints.Name = "txtExePoints";
            this.txtExePoints.Size = new System.Drawing.Size(106, 22);
            this.txtExePoints.TabIndex = 6;
            this.txtExePoints.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtExePoints_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.PowderBlue;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label12.Location = new System.Drawing.Point(53, 65);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(133, 16);
            this.label12.TabIndex = 14;
            this.label12.Text = "AVERAGE(Points)";
            // 
            // cbPromotionType
            // 
            this.cbPromotionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPromotionType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPromotionType.FormattingEnabled = true;
            this.cbPromotionType.Location = new System.Drawing.Point(586, 19);
            this.cbPromotionType.Name = "cbPromotionType";
            this.cbPromotionType.Size = new System.Drawing.Size(137, 24);
            this.cbPromotionType.TabIndex = 2;
            this.cbPromotionType.SelectedIndexChanged += new System.EventHandler(this.cbPromotionType_SelectedIndexChanged);
            // 
            // lblSalStructType
            // 
            this.lblSalStructType.AutoSize = true;
            this.lblSalStructType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalStructType.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblSalStructType.Location = new System.Drawing.Point(458, 23);
            this.lblSalStructType.Name = "lblSalStructType";
            this.lblSalStructType.Size = new System.Drawing.Size(127, 17);
            this.lblSalStructType.TabIndex = 10;
            this.lblSalStructType.Text = "Promotion Type ";
            // 
            // cbCompany
            // 
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Items.AddRange(new object[] {
            "                     Select"});
            this.cbCompany.Location = new System.Drawing.Point(87, 19);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(365, 24);
            this.cbCompany.TabIndex = 1;
            this.cbCompany.Tag = "";
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(12, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Company";
            // 
            // SalesPromotionEligibulityMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(754, 486);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Name = "SalesPromotionEligibulityMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Promotion Eligibulity Master";
            this.Load += new System.EventHandler(this.SalesPromotionEligibulityMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvSalePromotionDetails)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbPromotionType;
        private System.Windows.Forms.Label lblSalStructType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAvgMonths;
        private System.Windows.Forms.TextBox txtAvgPoints;
        private System.Windows.Forms.TextBox txtGoodMonths;
        private System.Windows.Forms.TextBox txtGoodPoints;
        private System.Windows.Forms.TextBox txtExeMonts;
        private System.Windows.Forms.Label lbaSalBasic;
        private System.Windows.Forms.TextBox txtExePoints;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.DataGridView gvSalePromotionDetails;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn slNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptId;
        private System.Windows.Forms.DataGridViewTextBoxColumn PromotionType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExcellentMonths;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExcellentPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoodMonths;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoodPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn AverageMonths;
        private System.Windows.Forms.DataGridViewTextBoxColumn AveragePoints;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExtOrdMnths;
        private System.Windows.Forms.TextBox txtExtOrdPts;
    }
}