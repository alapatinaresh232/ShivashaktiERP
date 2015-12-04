namespace SSCRM
{
    partial class EmployeeTDSForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbWagePeriod = new System.Windows.Forms.ComboBox();
            this.txtDeductedAmt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbPaymentMode = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbFinYear = new System.Windows.Forms.ComboBox();
            this.txtPanNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gvEmpTdsDetails = new System.Windows.Forms.DataGridView();
            this.txtEmi = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotTdsAmt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEName = new System.Windows.Forms.TextBox();
            this.txtEcodeSearch = new System.Windows.Forms.TextBox();
            this.lblEcode = new System.Windows.Forms.Label();
            this.cbBranches = new System.Windows.Forms.ComboBox();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MonthSlNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TdsNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FinYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Recovered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WageMonth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmiAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpTdsDetails)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.cbWagePeriod);
            this.groupBox1.Controls.Add(this.txtDeductedAmt);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cbPaymentMode);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbFinYear);
            this.groupBox1.Controls.Add(this.txtPanNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txtEmi);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtTotTdsAmt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtEName);
            this.groupBox1.Controls.Add(this.txtEcodeSearch);
            this.groupBox1.Controls.Add(this.lblEcode);
            this.groupBox1.Controls.Add(this.cbBranches);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(841, 418);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cbWagePeriod
            // 
            this.cbWagePeriod.AllowDrop = true;
            this.cbWagePeriod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbWagePeriod.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbWagePeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWagePeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWagePeriod.FormattingEnabled = true;
            this.cbWagePeriod.Items.AddRange(new object[] {
            "SALARY",
            "CASH"});
            this.cbWagePeriod.Location = new System.Drawing.Point(549, 93);
            this.cbWagePeriod.Name = "cbWagePeriod";
            this.cbWagePeriod.Size = new System.Drawing.Size(113, 23);
            this.cbWagePeriod.TabIndex = 14;
            this.cbWagePeriod.SelectedIndexChanged += new System.EventHandler(this.cbWagePeriod_SelectedIndexChanged);
            // 
            // txtDeductedAmt
            // 
            this.txtDeductedAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeductedAmt.Location = new System.Drawing.Point(311, 92);
            this.txtDeductedAmt.MaxLength = 10;
            this.txtDeductedAmt.Name = "txtDeductedAmt";
            this.txtDeductedAmt.ReadOnly = true;
            this.txtDeductedAmt.Size = new System.Drawing.Size(99, 22);
            this.txtDeductedAmt.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.PowderBlue;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label9.Location = new System.Drawing.Point(202, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 16);
            this.label9.TabIndex = 11;
            this.label9.Text = "Deducted Amt";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label10.Location = new System.Drawing.Point(435, 124);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 16);
            this.label10.TabIndex = 19;
            this.label10.Text = "Payment Mode";
            // 
            // cbPaymentMode
            // 
            this.cbPaymentMode.AllowDrop = true;
            this.cbPaymentMode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbPaymentMode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbPaymentMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPaymentMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPaymentMode.FormattingEnabled = true;
            this.cbPaymentMode.Items.AddRange(new object[] {
            "SALARY",
            "CASH"});
            this.cbPaymentMode.Location = new System.Drawing.Point(550, 121);
            this.cbPaymentMode.Name = "cbPaymentMode";
            this.cbPaymentMode.Size = new System.Drawing.Size(113, 23);
            this.cbPaymentMode.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(416, 38);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 16);
            this.label8.TabIndex = 3;
            this.label8.Text = "Fin Year";
            // 
            // cbFinYear
            // 
            this.cbFinYear.AllowDrop = true;
            this.cbFinYear.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbFinYear.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFinYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFinYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFinYear.FormattingEnabled = true;
            this.cbFinYear.Location = new System.Drawing.Point(485, 35);
            this.cbFinYear.Name = "cbFinYear";
            this.cbFinYear.Size = new System.Drawing.Size(137, 23);
            this.cbFinYear.TabIndex = 4;
            this.cbFinYear.SelectedIndexChanged += new System.EventHandler(this.cbFinYear_SelectedIndexChanged);
            // 
            // txtPanNo
            // 
            this.txtPanNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPanNo.Location = new System.Drawing.Point(106, 121);
            this.txtPanNo.MaxLength = 10;
            this.txtPanNo.Name = "txtPanNo";
            this.txtPanNo.Size = new System.Drawing.Size(162, 22);
            this.txtPanNo.TabIndex = 18;
            this.txtPanNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPanNo_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.PowderBlue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(9, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Pan Card No";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Navy;
            this.label16.Location = new System.Drawing.Point(299, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(203, 22);
            this.label16.TabIndex = 0;
            this.label16.Text = "EMPLOYEE TDS FORM";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gvEmpTdsDetails);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.groupBox2.Location = new System.Drawing.Point(3, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(834, 222);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Emp TDS Details";
            // 
            // gvEmpTdsDetails
            // 
            this.gvEmpTdsDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvEmpTdsDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvEmpTdsDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvEmpTdsDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvEmpTdsDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvEmpTdsDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEmpTdsDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.MonthSlNO,
            this.TdsNo,
            this.FinYear,
            this.TotAmt,
            this.Recovered,
            this.WageMonth,
            this.EmiAmt,
            this.Edit});
            this.gvEmpTdsDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvEmpTdsDetails.Location = new System.Drawing.Point(7, 23);
            this.gvEmpTdsDetails.MultiSelect = false;
            this.gvEmpTdsDetails.Name = "gvEmpTdsDetails";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvEmpTdsDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvEmpTdsDetails.RowHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Navy;
            this.gvEmpTdsDetails.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvEmpTdsDetails.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.gvEmpTdsDetails.Size = new System.Drawing.Size(821, 192);
            this.gvEmpTdsDetails.TabIndex = 0;
            this.gvEmpTdsDetails.TabStop = false;
            this.gvEmpTdsDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvEmpTdsDetails_CellClick);
            // 
            // txtEmi
            // 
            this.txtEmi.BackColor = System.Drawing.Color.White;
            this.txtEmi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmi.Location = new System.Drawing.Point(731, 93);
            this.txtEmi.MaxLength = 10;
            this.txtEmi.Name = "txtEmi";
            this.txtEmi.Size = new System.Drawing.Size(90, 22);
            this.txtEmi.TabIndex = 16;
            this.txtEmi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmi_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.PowderBlue;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(669, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Amount";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(446, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "Wage Period";
            // 
            // txtTotTdsAmt
            // 
            this.txtTotTdsAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotTdsAmt.Location = new System.Drawing.Point(83, 91);
            this.txtTotTdsAmt.MaxLength = 10;
            this.txtTotTdsAmt.Name = "txtTotTdsAmt";
            this.txtTotTdsAmt.Size = new System.Drawing.Size(116, 22);
            this.txtTotTdsAmt.TabIndex = 10;
            this.txtTotTdsAmt.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTotTdsAmt_KeyUp);
            this.txtTotTdsAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotTdsAmt_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.PowderBlue;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(6, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Total Amt";
            // 
            // txtEName
            // 
            this.txtEName.BackColor = System.Drawing.SystemColors.Info;
            this.txtEName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEName.Location = new System.Drawing.Point(161, 34);
            this.txtEName.MaxLength = 20;
            this.txtEName.Name = "txtEName";
            this.txtEName.ReadOnly = true;
            this.txtEName.Size = new System.Drawing.Size(249, 22);
            this.txtEName.TabIndex = 2;
            this.txtEName.TabStop = false;
            // 
            // txtEcodeSearch
            // 
            this.txtEcodeSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcodeSearch.Location = new System.Drawing.Point(82, 34);
            this.txtEcodeSearch.MaxLength = 5;
            this.txtEcodeSearch.Name = "txtEcodeSearch";
            this.txtEcodeSearch.Size = new System.Drawing.Size(78, 22);
            this.txtEcodeSearch.TabIndex = 1;
            this.txtEcodeSearch.Validated += new System.EventHandler(this.txtEcodeSearch_Validated);
            // 
            // lblEcode
            // 
            this.lblEcode.AutoSize = true;
            this.lblEcode.BackColor = System.Drawing.Color.PowderBlue;
            this.lblEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEcode.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblEcode.Location = new System.Drawing.Point(29, 37);
            this.lblEcode.Name = "lblEcode";
            this.lblEcode.Size = new System.Drawing.Size(53, 16);
            this.lblEcode.TabIndex = 0;
            this.lblEcode.Text = "Ecode";
            // 
            // cbBranches
            // 
            this.cbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranches.FormattingEnabled = true;
            this.cbBranches.Location = new System.Drawing.Point(485, 62);
            this.cbBranches.Name = "cbBranches";
            this.cbBranches.Size = new System.Drawing.Size(344, 24);
            this.cbBranches.TabIndex = 8;
            // 
            // cbCompany
            // 
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Items.AddRange(new object[] {
            "                     Select"});
            this.cbCompany.Location = new System.Drawing.Point(82, 61);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(328, 24);
            this.cbCompany.TabIndex = 6;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(424, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Branch";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(7, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "Company";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(231, 369);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 45);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(229, 13);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(150, 13);
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
            this.btnSave.Location = new System.Drawing.Point(75, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Edit";
            this.dataGridViewImageColumn1.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 80;
            // 
            // SLNO
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.SLNO.DefaultCellStyle = dataGridViewCellStyle3;
            this.SLNO.Frozen = true;
            this.SLNO.HeaderText = "Sl.No";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SLNO.Width = 50;
            // 
            // MonthSlNO
            // 
            this.MonthSlNO.HeaderText = "Month SlNo";
            this.MonthSlNO.Name = "MonthSlNO";
            this.MonthSlNO.Visible = false;
            // 
            // TdsNo
            // 
            this.TdsNo.HeaderText = "Trn No";
            this.TdsNo.Name = "TdsNo";
            this.TdsNo.ReadOnly = true;
            this.TdsNo.Visible = false;
            this.TdsNo.Width = 120;
            // 
            // FinYear
            // 
            this.FinYear.HeaderText = "Fin Year";
            this.FinYear.Name = "FinYear";
            this.FinYear.ReadOnly = true;
            this.FinYear.Width = 150;
            // 
            // TotAmt
            // 
            this.TotAmt.HeaderText = "Total Amt";
            this.TotAmt.Name = "TotAmt";
            this.TotAmt.ReadOnly = true;
            this.TotAmt.Width = 170;
            // 
            // Recovered
            // 
            this.Recovered.HeaderText = "Recovered";
            this.Recovered.Name = "Recovered";
            this.Recovered.Width = 120;
            // 
            // WageMonth
            // 
            this.WageMonth.HeaderText = "Wage Month";
            this.WageMonth.Name = "WageMonth";
            this.WageMonth.ReadOnly = true;
            this.WageMonth.Width = 140;
            // 
            // EmiAmt
            // 
            this.EmiAmt.HeaderText = "EMI";
            this.EmiAmt.Name = "EmiAmt";
            this.EmiAmt.ReadOnly = true;
            this.EmiAmt.Width = 80;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.Edit.Name = "Edit";
            this.Edit.Width = 40;
            // 
            // EmployeeTDSForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(847, 425);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmployeeTDSForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee TDS Form";
            this.Load += new System.EventHandler(this.EmployeeTDSForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpTdsDetails)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.DataGridView gvEmpTdsDetails;
        private System.Windows.Forms.TextBox txtEmi;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTotTdsAmt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEName;
        private System.Windows.Forms.TextBox txtEcodeSearch;
        private System.Windows.Forms.Label lblEcode;
        private System.Windows.Forms.ComboBox cbBranches;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtPanNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbFinYear;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbPaymentMode;
        private System.Windows.Forms.TextBox txtDeductedAmt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbWagePeriod;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonthSlNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TdsNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FinYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Recovered;
        private System.Windows.Forms.DataGridViewTextBoxColumn WageMonth;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmiAmt;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
    }
}