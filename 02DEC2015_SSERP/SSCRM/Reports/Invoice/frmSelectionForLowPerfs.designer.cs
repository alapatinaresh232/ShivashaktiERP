namespace SSCRM
{
    partial class frmSelectionForLowPerfs
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
            this.label16 = new System.Windows.Forms.Label();
            this.txtEcode = new System.Windows.Forms.TextBox();
            this.dtpFromDoc = new System.Windows.Forms.DateTimePicker();
            this.lblDocMonth = new System.Windows.Forms.Label();
            this.dtpToDoc = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkMonths = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkGrps = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkPersPnts = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpLOSAsOnDate = new System.Windows.Forms.DateTimePicker();
            this.cbSelection = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.cbSortBy = new System.Windows.Forms.ComboBox();
            this.txtToPntsPerHead = new System.Windows.Forms.TextBox();
            this.txtToPntsPerGrp = new System.Windows.Forms.TextBox();
            this.txtFrmPntsPerHead = new System.Windows.Forms.TextBox();
            this.txtFrmPntsPerGrp = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chkPntsPerHead = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.chkPntsPerGrp = new System.Windows.Forms.CheckBox();
            this.txtToPersPts = new System.Windows.Forms.TextBox();
            this.txtToGrps = new System.Windows.Forms.TextBox();
            this.txtToMnths = new System.Windows.Forms.TextBox();
            this.txtFrmPersPts = new System.Windows.Forms.TextBox();
            this.txtFrmGrps = new System.Windows.Forms.TextBox();
            this.txFrmMnths = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.cbEcode = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label16.Location = new System.Drawing.Point(9, 25);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 16);
            this.label16.TabIndex = 0;
            this.label16.Text = "Ecode";
            // 
            // txtEcode
            // 
            this.txtEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcode.Location = new System.Drawing.Point(63, 21);
            this.txtEcode.MaxLength = 20;
            this.txtEcode.Name = "txtEcode";
            this.txtEcode.Size = new System.Drawing.Size(79, 22);
            this.txtEcode.TabIndex = 1;
            this.txtEcode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEcode_KeyUp);
            // 
            // dtpFromDoc
            // 
            this.dtpFromDoc.CustomFormat = "MMM/yyyy";
            this.dtpFromDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDoc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDoc.Location = new System.Drawing.Point(233, 55);
            this.dtpFromDoc.Name = "dtpFromDoc";
            this.dtpFromDoc.Size = new System.Drawing.Size(90, 22);
            this.dtpFromDoc.TabIndex = 5;
            // 
            // lblDocMonth
            // 
            this.lblDocMonth.AutoSize = true;
            this.lblDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocMonth.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblDocMonth.Location = new System.Drawing.Point(53, 59);
            this.lblDocMonth.Name = "lblDocMonth";
            this.lblDocMonth.Size = new System.Drawing.Size(81, 16);
            this.lblDocMonth.TabIndex = 3;
            this.lblDocMonth.Text = "Doc Month";
            // 
            // dtpToDoc
            // 
            this.dtpToDoc.CustomFormat = "MMM/yyyy";
            this.dtpToDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDoc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDoc.Location = new System.Drawing.Point(379, 58);
            this.dtpToDoc.Name = "dtpToDoc";
            this.dtpToDoc.Size = new System.Drawing.Size(90, 22);
            this.dtpToDoc.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(344, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "To ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(184, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "From";
            // 
            // chkMonths
            // 
            this.chkMonths.AutoSize = true;
            this.chkMonths.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMonths.ForeColor = System.Drawing.Color.MidnightBlue;
            this.chkMonths.Location = new System.Drawing.Point(32, 89);
            this.chkMonths.Name = "chkMonths";
            this.chkMonths.Size = new System.Drawing.Size(134, 20);
            this.chkMonths.TabIndex = 8;
            this.chkMonths.Text = "Months Worked";
            this.chkMonths.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(347, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "To ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(186, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "From";
            // 
            // chkGrps
            // 
            this.chkGrps.AutoSize = true;
            this.chkGrps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.chkGrps.ForeColor = System.Drawing.Color.MidnightBlue;
            this.chkGrps.Location = new System.Drawing.Point(32, 116);
            this.chkGrps.Name = "chkGrps";
            this.chkGrps.Size = new System.Drawing.Size(77, 20);
            this.chkGrps.TabIndex = 13;
            this.chkGrps.Text = "Groups";
            this.chkGrps.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(347, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "To ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(186, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "From";
            // 
            // chkPersPnts
            // 
            this.chkPersPnts.AutoSize = true;
            this.chkPersPnts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.chkPersPnts.ForeColor = System.Drawing.Color.MidnightBlue;
            this.chkPersPnts.Location = new System.Drawing.Point(32, 144);
            this.chkPersPnts.Name = "chkPersPnts";
            this.chkPersPnts.Size = new System.Drawing.Size(136, 20);
            this.chkPersPnts.TabIndex = 18;
            this.chkPersPnts.Text = "Personal Points";
            this.chkPersPnts.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(351, 146);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 16);
            this.label8.TabIndex = 21;
            this.label8.Text = "  <";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(208, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 16);
            this.label7.TabIndex = 19;
            this.label7.Text = " >";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.dtpLOSAsOnDate);
            this.groupBox1.Controls.Add(this.cbSelection);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.cbSortBy);
            this.groupBox1.Controls.Add(this.txtToPntsPerHead);
            this.groupBox1.Controls.Add(this.txtToPntsPerGrp);
            this.groupBox1.Controls.Add(this.txtFrmPntsPerHead);
            this.groupBox1.Controls.Add(this.txtFrmPntsPerGrp);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.chkPntsPerHead);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.chkPntsPerGrp);
            this.groupBox1.Controls.Add(this.txtToPersPts);
            this.groupBox1.Controls.Add(this.txtToGrps);
            this.groupBox1.Controls.Add(this.txtToMnths);
            this.groupBox1.Controls.Add(this.txtFrmPersPts);
            this.groupBox1.Controls.Add(this.txtFrmGrps);
            this.groupBox1.Controls.Add(this.txFrmMnths);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.chkPersPnts);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.chkGrps);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chkMonths);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpToDoc);
            this.groupBox1.Controls.Add(this.lblDocMonth);
            this.groupBox1.Controls.Add(this.dtpFromDoc);
            this.groupBox1.Controls.Add(this.cbEcode);
            this.groupBox1.Controls.Add(this.txtEcode);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 354);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Navy;
            this.label15.Location = new System.Drawing.Point(51, 118);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 16);
            this.label15.TabIndex = 40;
            this.label15.Text = "Groups";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Navy;
            this.label14.Location = new System.Drawing.Point(51, 90);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(115, 16);
            this.label14.TabIndex = 39;
            this.label14.Text = "Months Worked";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Navy;
            this.label13.Location = new System.Drawing.Point(16, 269);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(212, 16);
            this.label13.TabIndex = 37;
            this.label13.Text = "Length Of Service As On Date";
           
            // 
            // dtpLOSAsOnDate
            // 
            this.dtpLOSAsOnDate.CustomFormat = "dd/MMM/yyyy";
            this.dtpLOSAsOnDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpLOSAsOnDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpLOSAsOnDate.Location = new System.Drawing.Point(234, 266);
            this.dtpLOSAsOnDate.Name = "dtpLOSAsOnDate";
            this.dtpLOSAsOnDate.Size = new System.Drawing.Size(110, 22);
            this.dtpLOSAsOnDate.TabIndex = 38;
            // 
            // cbSelection
            // 
            this.cbSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSelection.FormattingEnabled = true;
            this.cbSelection.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.cbSelection.Location = new System.Drawing.Point(234, 226);
            this.cbSelection.Name = "cbSelection";
            this.cbSelection.Size = new System.Drawing.Size(76, 23);
            this.cbSelection.TabIndex = 33;
            this.cbSelection.SelectedIndexChanged += new System.EventHandler(this.cbSelection_SelectedIndexChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label27.Location = new System.Drawing.Point(319, 231);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(56, 16);
            this.label27.TabIndex = 34;
            this.label27.Text = "Sort By";
            // 
            // cbSortBy
            // 
            this.cbSortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSortBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSortBy.FormattingEnabled = true;
            this.cbSortBy.Items.AddRange(new object[] {
            "GROUP",
            "PERSONAL"});
            this.cbSortBy.Location = new System.Drawing.Point(379, 228);
            this.cbSortBy.Name = "cbSortBy";
            this.cbSortBy.Size = new System.Drawing.Size(117, 23);
            this.cbSortBy.TabIndex = 35;
            // 
            // txtToPntsPerHead
            // 
            this.txtToPntsPerHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToPntsPerHead.Location = new System.Drawing.Point(379, 199);
            this.txtToPntsPerHead.MaxLength = 5;
            this.txtToPntsPerHead.Name = "txtToPntsPerHead";
            this.txtToPntsPerHead.Size = new System.Drawing.Size(91, 22);
            this.txtToPntsPerHead.TabIndex = 32;
            this.txtToPntsPerHead.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtToPntsPerHead_KeyPress);
            // 
            // txtToPntsPerGrp
            // 
            this.txtToPntsPerGrp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToPntsPerGrp.Location = new System.Drawing.Point(380, 170);
            this.txtToPntsPerGrp.MaxLength = 5;
            this.txtToPntsPerGrp.Name = "txtToPntsPerGrp";
            this.txtToPntsPerGrp.Size = new System.Drawing.Size(90, 22);
            this.txtToPntsPerGrp.TabIndex = 27;
            this.txtToPntsPerGrp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtToPntsPerGrp_KeyPress);
            // 
            // txtFrmPntsPerHead
            // 
            this.txtFrmPntsPerHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrmPntsPerHead.Location = new System.Drawing.Point(233, 199);
            this.txtFrmPntsPerHead.MaxLength = 5;
            this.txtFrmPntsPerHead.Name = "txtFrmPntsPerHead";
            this.txtFrmPntsPerHead.Size = new System.Drawing.Size(89, 22);
            this.txtFrmPntsPerHead.TabIndex = 30;
            this.txtFrmPntsPerHead.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFrmPntsPerHead_KeyPress);
            // 
            // txtFrmPntsPerGrp
            // 
            this.txtFrmPntsPerGrp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrmPntsPerGrp.Location = new System.Drawing.Point(233, 170);
            this.txtFrmPntsPerGrp.MaxLength = 5;
            this.txtFrmPntsPerGrp.Name = "txtFrmPntsPerGrp";
            this.txtFrmPntsPerGrp.Size = new System.Drawing.Size(89, 22);
            this.txtFrmPntsPerGrp.TabIndex = 25;
            this.txtFrmPntsPerGrp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFrmPntsPerGrp_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label9.Location = new System.Drawing.Point(207, 202);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 16);
            this.label9.TabIndex = 29;
            this.label9.Text = " >";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label10.Location = new System.Drawing.Point(354, 202);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 16);
            this.label10.TabIndex = 31;
            this.label10.Text = " <";
            // 
            // chkPntsPerHead
            // 
            this.chkPntsPerHead.AutoSize = true;
            this.chkPntsPerHead.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.chkPntsPerHead.ForeColor = System.Drawing.Color.MidnightBlue;
            this.chkPntsPerHead.Location = new System.Drawing.Point(32, 200);
            this.chkPntsPerHead.Name = "chkPntsPerHead";
            this.chkPntsPerHead.Size = new System.Drawing.Size(100, 20);
            this.chkPntsPerHead.TabIndex = 28;
            this.chkPntsPerHead.Text = "Points P/H";
            this.chkPntsPerHead.UseVisualStyleBackColor = true;
            this.chkPntsPerHead.CheckedChanged += new System.EventHandler(this.chkPntsPerHead_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label11.Location = new System.Drawing.Point(212, 173);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 16);
            this.label11.TabIndex = 24;
            this.label11.Text = ">";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label12.Location = new System.Drawing.Point(354, 173);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(20, 16);
            this.label12.TabIndex = 26;
            this.label12.Text = " <";
            // 
            // chkPntsPerGrp
            // 
            this.chkPntsPerGrp.AutoSize = true;
            this.chkPntsPerGrp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.chkPntsPerGrp.ForeColor = System.Drawing.Color.MidnightBlue;
            this.chkPntsPerGrp.Location = new System.Drawing.Point(32, 171);
            this.chkPntsPerGrp.Name = "chkPntsPerGrp";
            this.chkPntsPerGrp.Size = new System.Drawing.Size(100, 20);
            this.chkPntsPerGrp.TabIndex = 23;
            this.chkPntsPerGrp.Text = "Points P/G";
            this.chkPntsPerGrp.UseVisualStyleBackColor = true;
            this.chkPntsPerGrp.CheckedChanged += new System.EventHandler(this.chkPntsPerGrp_CheckedChanged);
            // 
            // txtToPersPts
            // 
            this.txtToPersPts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToPersPts.Location = new System.Drawing.Point(379, 143);
            this.txtToPersPts.MaxLength = 5;
            this.txtToPersPts.Name = "txtToPersPts";
            this.txtToPersPts.Size = new System.Drawing.Size(91, 22);
            this.txtToPersPts.TabIndex = 22;
            this.txtToPersPts.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtToPersPts_KeyPress);
            // 
            // txtToGrps
            // 
            this.txtToGrps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToGrps.Location = new System.Drawing.Point(380, 115);
            this.txtToGrps.MaxLength = 5;
            this.txtToGrps.Name = "txtToGrps";
            this.txtToGrps.Size = new System.Drawing.Size(90, 22);
            this.txtToGrps.TabIndex = 17;
            this.txtToGrps.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtToGrps_KeyPress);
            // 
            // txtToMnths
            // 
            this.txtToMnths.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToMnths.Location = new System.Drawing.Point(380, 88);
            this.txtToMnths.MaxLength = 5;
            this.txtToMnths.Name = "txtToMnths";
            this.txtToMnths.Size = new System.Drawing.Size(90, 22);
            this.txtToMnths.TabIndex = 12;
            this.txtToMnths.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtToMnths_KeyPress);
            // 
            // txtFrmPersPts
            // 
            this.txtFrmPersPts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrmPersPts.Location = new System.Drawing.Point(233, 143);
            this.txtFrmPersPts.MaxLength = 5;
            this.txtFrmPersPts.Name = "txtFrmPersPts";
            this.txtFrmPersPts.Size = new System.Drawing.Size(89, 22);
            this.txtFrmPersPts.TabIndex = 20;
            this.txtFrmPersPts.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFrmPersPts_KeyPress);
            // 
            // txtFrmGrps
            // 
            this.txtFrmGrps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrmGrps.Location = new System.Drawing.Point(233, 115);
            this.txtFrmGrps.MaxLength = 5;
            this.txtFrmGrps.Name = "txtFrmGrps";
            this.txtFrmGrps.Size = new System.Drawing.Size(89, 22);
            this.txtFrmGrps.TabIndex = 15;
            this.txtFrmGrps.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFrmGrps_KeyPress);
            // 
            // txFrmMnths
            // 
            this.txFrmMnths.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txFrmMnths.Location = new System.Drawing.Point(233, 88);
            this.txFrmMnths.MaxLength = 5;
            this.txFrmMnths.Name = "txFrmMnths";
            this.txFrmMnths.Size = new System.Drawing.Size(89, 22);
            this.txFrmMnths.TabIndex = 10;
            this.txFrmMnths.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txFrmMnths_KeyPress);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnDownload);
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnReport);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(58, 298);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(410, 45);
            this.groupBox4.TabIndex = 36;
            this.groupBox4.TabStop = false;
            // 
            // btnDownload
            // 
            this.btnDownload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDownload.BackColor = System.Drawing.Color.OliveDrab;
            this.btnDownload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnDownload.FlatAppearance.BorderSize = 5;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Image = global::SSCRM.Properties.Resources.ic_download;
            this.btnDownload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDownload.Location = new System.Drawing.Point(286, 13);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(1);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(97, 24);
            this.btnDownload.TabIndex = 3;
            this.btnDownload.Tag = "Product  Search";
            this.btnDownload.Text = "Download";
            this.btnDownload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(177, 13);
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
            this.btnCancel.Location = new System.Drawing.Point(102, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Clea&r";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReport
            // 
            this.btnReport.AutoEllipsis = true;
            this.btnReport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReport.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnReport.Location = new System.Drawing.Point(27, 13);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(74, 26);
            this.btnReport.TabIndex = 0;
            this.btnReport.Text = "&Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // cbEcode
            // 
            this.cbEcode.AllowDrop = true;
            this.cbEcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEcode.FormattingEnabled = true;
            this.cbEcode.ItemHeight = 15;
            this.cbEcode.Location = new System.Drawing.Point(144, 20);
            this.cbEcode.Name = "cbEcode";
            this.cbEcode.Size = new System.Drawing.Size(326, 23);
            this.cbEcode.TabIndex = 2;
            this.cbEcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbEcode_KeyPress);
            // 
            // frmSelectionForLowPerfs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(533, 358);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "frmSelectionForLowPerfs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "List Of Low Performers";
            this.Load += new System.EventHandler(this.frmSelectionForLowPerfs_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox txtEcode;
        private System.Windows.Forms.DateTimePicker dtpFromDoc;
        private System.Windows.Forms.Label lblDocMonth;
        private System.Windows.Forms.DateTimePicker dtpToDoc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkMonths;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkGrps;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkPersPnts;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.TextBox txtFrmPersPts;
        private System.Windows.Forms.TextBox txtFrmGrps;
        private System.Windows.Forms.TextBox txFrmMnths;
        private System.Windows.Forms.TextBox txtToPersPts;
        private System.Windows.Forms.TextBox txtToGrps;
        private System.Windows.Forms.TextBox txtToMnths;
        private System.Windows.Forms.TextBox txtToPntsPerHead;
        private System.Windows.Forms.TextBox txtToPntsPerGrp;
        private System.Windows.Forms.TextBox txtFrmPntsPerHead;
        private System.Windows.Forms.TextBox txtFrmPntsPerGrp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkPntsPerHead;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkPntsPerGrp;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox cbSortBy;
        private System.Windows.Forms.ComboBox cbSelection;
        private System.Windows.Forms.ComboBox cbEcode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpLOSAsOnDate;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;

    }
}