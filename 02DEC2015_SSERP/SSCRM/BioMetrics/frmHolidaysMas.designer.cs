namespace SSCRM
{
    partial class frmHolidaysMas
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
            this.label7 = new System.Windows.Forms.Label();
            this.cbHolidayType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cbBranch = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.txtMaxHolidays = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbTrnType = new System.Windows.Forms.ComboBox();
            this.txtHolDesc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHolId = new System.Windows.Forms.TextBox();
            this.nmYear = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpHolidayDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtHolName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gvHolidaysDetails = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HolidayId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StateCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HolidayDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HolidayType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HolidayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HolidayDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHolidaysDetails)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbHolidayType);
            this.groupBox1.Controls.Add(this.lblType);
            this.groupBox1.Controls.Add(this.cbBranch);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.txtMaxHolidays);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbTrnType);
            this.groupBox1.Controls.Add(this.txtHolDesc);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtHolId);
            this.groupBox1.Controls.Add(this.nmYear);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpHolidayDate);
            this.groupBox1.Controls.Add(this.lblDate);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.txtHolName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.gvHolidaysDetails);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(817, 572);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(178, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "Holiday Type";
            // 
            // cbHolidayType
            // 
            this.cbHolidayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHolidayType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHolidayType.FormattingEnabled = true;
            this.cbHolidayType.Items.AddRange(new object[] {
            "HOLIDAY",
            "WEEK OFF"});
            this.cbHolidayType.Location = new System.Drawing.Point(281, 72);
            this.cbHolidayType.Name = "cbHolidayType";
            this.cbHolidayType.Size = new System.Drawing.Size(118, 24);
            this.cbHolidayType.TabIndex = 15;
            this.cbHolidayType.SelectedIndexChanged += new System.EventHandler(this.cbHolidayType_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblType.Location = new System.Drawing.Point(422, 46);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(44, 16);
            this.lblType.TabIndex = 10;
            this.lblType.Text = "State";
            // 
            // cbBranch
            // 
            this.cbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranch.FormattingEnabled = true;
            this.cbBranch.Location = new System.Drawing.Point(473, 42);
            this.cbBranch.MaxLength = 15;
            this.cbBranch.Name = "cbBranch";
            this.cbBranch.Size = new System.Drawing.Size(334, 24);
            this.cbBranch.TabIndex = 11;
            this.cbBranch.SelectedIndexChanged += new System.EventHandler(this.cbBranch_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(4, 45);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "Company";
            // 
            // cbCompany
            // 
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Location = new System.Drawing.Point(81, 42);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(319, 24);
            this.cbCompany.TabIndex = 9;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // txtMaxHolidays
            // 
            this.txtMaxHolidays.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaxHolidays.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxHolidays.Location = new System.Drawing.Point(733, 15);
            this.txtMaxHolidays.MaxLength = 50;
            this.txtMaxHolidays.Name = "txtMaxHolidays";
            this.txtMaxHolidays.Size = new System.Drawing.Size(72, 24);
            this.txtMaxHolidays.TabIndex = 7;
            this.txtMaxHolidays.TabStop = false;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(622, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Max  Holidays";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(162, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Trn Type";
            // 
            // cbTrnType
            // 
            this.cbTrnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrnType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTrnType.FormattingEnabled = true;
            this.cbTrnType.Items.AddRange(new object[] {
            "STATE WISE",
            "BRANCH WISE"});
            this.cbTrnType.Location = new System.Drawing.Point(236, 14);
            this.cbTrnType.Name = "cbTrnType";
            this.cbTrnType.Size = new System.Drawing.Size(162, 24);
            this.cbTrnType.TabIndex = 3;
            this.cbTrnType.SelectedIndexChanged += new System.EventHandler(this.cbTrnType_SelectedIndexChanged);
            // 
            // txtHolDesc
            // 
            this.txtHolDesc.BackColor = System.Drawing.SystemColors.Window;
            this.txtHolDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHolDesc.Location = new System.Drawing.Point(80, 102);
            this.txtHolDesc.MaxLength = 200;
            this.txtHolDesc.Multiline = true;
            this.txtHolDesc.Name = "txtHolDesc";
            this.txtHolDesc.Size = new System.Drawing.Size(725, 34);
            this.txtHolDesc.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(30, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 24);
            this.label3.TabIndex = 18;
            this.label3.Text = "Desc";
            // 
            // txtHolId
            // 
            this.txtHolId.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtHolId.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHolId.Location = new System.Drawing.Point(473, 15);
            this.txtHolId.MaxLength = 50;
            this.txtHolId.Name = "txtHolId";
            this.txtHolId.ReadOnly = true;
            this.txtHolId.Size = new System.Drawing.Size(89, 24);
            this.txtHolId.TabIndex = 5;
            this.txtHolId.TabStop = false;
            // 
            // nmYear
            // 
            this.nmYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nmYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmYear.Location = new System.Drawing.Point(83, 15);
            this.nmYear.Maximum = new decimal(new int[] {
            2020,
            0,
            0,
            0});
            this.nmYear.Name = "nmYear";
            this.nmYear.Size = new System.Drawing.Size(72, 22);
            this.nmYear.TabIndex = 1;
            this.nmYear.Value = new decimal(new int[] {
            2014,
            0,
            0,
            0});
            this.nmYear.ValueChanged += new System.EventHandler(this.nmYear_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(33, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Year";
            // 
            // dtpHolidayDate
            // 
            this.dtpHolidayDate.CustomFormat = "dd/MM/yyyy";
            this.dtpHolidayDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHolidayDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHolidayDate.Location = new System.Drawing.Point(80, 70);
            this.dtpHolidayDate.Name = "dtpHolidayDate";
            this.dtpHolidayDate.Size = new System.Drawing.Size(94, 23);
            this.dtpHolidayDate.TabIndex = 13;
            this.dtpHolidayDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblDate.Location = new System.Drawing.Point(34, 73);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(41, 16);
            this.lblDate.TabIndex = 12;
            this.lblDate.Text = "Date";
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label25.Location = new System.Drawing.Point(439, 18);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(28, 15);
            this.label25.TabIndex = 4;
            this.label25.Text = "ID";
            // 
            // txtHolName
            // 
            this.txtHolName.BackColor = System.Drawing.SystemColors.Window;
            this.txtHolName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHolName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHolName.Location = new System.Drawing.Point(473, 72);
            this.txtHolName.MaxLength = 100;
            this.txtHolName.Name = "txtHolName";
            this.txtHolName.Size = new System.Drawing.Size(334, 24);
            this.txtHolName.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(422, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "Name";
            // 
            // gvHolidaysDetails
            // 
            this.gvHolidaysDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Navy;
            this.gvHolidaysDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvHolidaysDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvHolidaysDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvHolidaysDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvHolidaysDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvHolidaysDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.HolidayId,
            this.StateCode,
            this.CompCode,
            this.TrnType,
            this.HolidayDate,
            this.HolidayType,
            this.HolidayName,
            this.HolidayDesc,
            this.Edit,
            this.Delete});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvHolidaysDetails.DefaultCellStyle = dataGridViewCellStyle4;
            this.gvHolidaysDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvHolidaysDetails.Location = new System.Drawing.Point(10, 191);
            this.gvHolidaysDetails.MultiSelect = false;
            this.gvHolidaysDetails.Name = "gvHolidaysDetails";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvHolidaysDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gvHolidaysDetails.RowHeadersVisible = false;
            this.gvHolidaysDetails.Size = new System.Drawing.Size(797, 372);
            this.gvHolidaysDetails.TabIndex = 21;
            this.gvHolidaysDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvHolidaysDetails_CellClick);
            // 
            // SLNO
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.SLNO.DefaultCellStyle = dataGridViewCellStyle3;
            this.SLNO.Frozen = true;
            this.SLNO.HeaderText = "Sl.No";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SLNO.Width = 50;
            // 
            // HolidayId
            // 
            this.HolidayId.Frozen = true;
            this.HolidayId.HeaderText = "HolidayId";
            this.HolidayId.Name = "HolidayId";
            this.HolidayId.ReadOnly = true;
            this.HolidayId.Visible = false;
            this.HolidayId.Width = 40;
            // 
            // StateCode
            // 
            this.StateCode.Frozen = true;
            this.StateCode.HeaderText = "StateCode";
            this.StateCode.Name = "StateCode";
            this.StateCode.ReadOnly = true;
            this.StateCode.Visible = false;
            // 
            // CompCode
            // 
            this.CompCode.Frozen = true;
            this.CompCode.HeaderText = "CompCode";
            this.CompCode.Name = "CompCode";
            this.CompCode.ReadOnly = true;
            this.CompCode.Visible = false;
            // 
            // TrnType
            // 
            this.TrnType.Frozen = true;
            this.TrnType.HeaderText = "Trn Type";
            this.TrnType.Name = "TrnType";
            this.TrnType.ReadOnly = true;
            this.TrnType.Visible = false;
            this.TrnType.Width = 180;
            // 
            // HolidayDate
            // 
            this.HolidayDate.Frozen = true;
            this.HolidayDate.HeaderText = "Date";
            this.HolidayDate.Name = "HolidayDate";
            // 
            // HolidayType
            // 
            this.HolidayType.Frozen = true;
            this.HolidayType.HeaderText = "Holiday Type";
            this.HolidayType.Name = "HolidayType";
            this.HolidayType.ReadOnly = true;
            this.HolidayType.Width = 150;
            // 
            // HolidayName
            // 
            this.HolidayName.Frozen = true;
            this.HolidayName.HeaderText = "Holiday Name";
            this.HolidayName.Name = "HolidayName";
            this.HolidayName.Width = 200;
            // 
            // HolidayDesc
            // 
            this.HolidayDesc.HeaderText = "Holiday Desc";
            this.HolidayDesc.Name = "HolidayDesc";
            this.HolidayDesc.Width = 180;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "";
            this.Edit.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Width = 30;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "";
            this.Delete.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Width = 30;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Location = new System.Drawing.Point(179, 137);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(458, 44);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(190, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Clear";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(276, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 26);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "C&lose";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(105, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Width = 30;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.ReadOnly = true;
            this.dataGridViewImageColumn2.Width = 30;
            // 
            // frmHolidaysMas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(823, 577);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHolidaysMas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Holiday Master";
            this.Load += new System.EventHandler(this.frmHolidaysMas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHolidaysDetails)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox txtHolDesc;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtHolId;
        private System.Windows.Forms.NumericUpDown nmYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpHolidayDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label label25;
        public System.Windows.Forms.TextBox txtHolName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.DataGridView gvHolidaysDetails;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbTrnType;
        public System.Windows.Forms.TextBox txtMaxHolidays;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cbBranch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbHolidayType;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn HolidayId;
        private System.Windows.Forms.DataGridViewTextBoxColumn StateCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn HolidayDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn HolidayType;
        private System.Windows.Forms.DataGridViewTextBoxColumn HolidayName;
        private System.Windows.Forms.DataGridViewTextBoxColumn HolidayDesc;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
    }
}