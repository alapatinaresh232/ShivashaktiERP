namespace SSCRM
{
    partial class TransportCostSummary
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbEcode = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtHireTotAmt = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtHireOthers = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtHireDiesel = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtHireRent = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtOwnTotAmt = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtOwnOthers = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtOwnDiesel = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtOwnRent = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txtGLEcode = new System.Windows.Forms.TextBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtDistanceKM = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCampName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cbBranches = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDocMonth = new System.Windows.Forms.TextBox();
            this.label67 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbEcode);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.txtGLEcode);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtDistanceKM);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCampName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.cbBranches);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtDocMonth);
            this.groupBox1.Controls.Add(this.label67);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(653, 444);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cbCompany
            // 
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.ItemHeight = 16;
            this.cbCompany.Items.AddRange(new object[] {
            "                     Select"});
            this.cbCompany.Location = new System.Drawing.Point(105, 37);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(397, 24);
            this.cbCompany.TabIndex = 1;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(27, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Company";
            // 
            // cbEcode
            // 
            this.cbEcode.AllowDrop = true;
            this.cbEcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbEcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEcode.FormattingEnabled = true;
            this.cbEcode.ItemHeight = 15;
            this.cbEcode.Location = new System.Drawing.Point(176, 89);
            this.cbEcode.Name = "cbEcode";
            this.cbEcode.Size = new System.Drawing.Size(326, 23);
            this.cbEcode.TabIndex = 8;
            this.cbEcode.SelectedIndexChanged += new System.EventHandler(this.cbEcode_SelectedIndexChanged);
            this.cbEcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbEcode_KeyPress);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtHireTotAmt);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.txtHireOthers);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.txtHireDiesel);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.txtHireRent);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.txtOwnTotAmt);
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.txtOwnOthers);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.txtOwnDiesel);
            this.groupBox5.Controls.Add(this.label23);
            this.groupBox5.Controls.Add(this.txtOwnRent);
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Controls.Add(this.label25);
            this.groupBox5.Controls.Add(this.label26);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox5.ForeColor = System.Drawing.Color.Navy;
            this.groupBox5.Location = new System.Drawing.Point(4, 143);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(639, 101);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Transport Cost";
            // 
            // txtHireTotAmt
            // 
            this.txtHireTotAmt.BackColor = System.Drawing.SystemColors.Info;
            this.txtHireTotAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHireTotAmt.Location = new System.Drawing.Point(549, 71);
            this.txtHireTotAmt.MaxLength = 10;
            this.txtHireTotAmt.Name = "txtHireTotAmt";
            this.txtHireTotAmt.ReadOnly = true;
            this.txtHireTotAmt.Size = new System.Drawing.Size(85, 22);
            this.txtHireTotAmt.TabIndex = 17;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label17.Location = new System.Drawing.Point(467, 74);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(83, 17);
            this.label17.TabIndex = 16;
            this.label17.Text = "Tot Amount";
            // 
            // txtHireOthers
            // 
            this.txtHireOthers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHireOthers.Location = new System.Drawing.Point(377, 70);
            this.txtHireOthers.MaxLength = 10;
            this.txtHireOthers.Name = "txtHireOthers";
            this.txtHireOthers.Size = new System.Drawing.Size(80, 22);
            this.txtHireOthers.TabIndex = 10;
            this.txtHireOthers.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtHireOthers_KeyUp);
            this.txtHireOthers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHireOthers_KeyPress);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label18.Location = new System.Drawing.Point(329, 73);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 17);
            this.label18.TabIndex = 14;
            this.label18.Text = "Others";
            // 
            // txtHireDiesel
            // 
            this.txtHireDiesel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHireDiesel.Location = new System.Drawing.Point(549, 44);
            this.txtHireDiesel.MaxLength = 10;
            this.txtHireDiesel.Name = "txtHireDiesel";
            this.txtHireDiesel.Size = new System.Drawing.Size(85, 22);
            this.txtHireDiesel.TabIndex = 8;
            this.txtHireDiesel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtHireDiesel_KeyUp);
            this.txtHireDiesel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHireDiesel_KeyPress);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label19.Location = new System.Drawing.Point(499, 47);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(46, 17);
            this.label19.TabIndex = 12;
            this.label19.Text = "Diesel";
            // 
            // txtHireRent
            // 
            this.txtHireRent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHireRent.Location = new System.Drawing.Point(376, 45);
            this.txtHireRent.MaxLength = 10;
            this.txtHireRent.Name = "txtHireRent";
            this.txtHireRent.Size = new System.Drawing.Size(81, 22);
            this.txtHireRent.TabIndex = 8;
            this.txtHireRent.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtHireRent_KeyUp);
            this.txtHireRent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHireRent_KeyPress);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label20.Location = new System.Drawing.Point(335, 48);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(36, 17);
            this.label20.TabIndex = 10;
            this.label20.Text = "Rent";
            // 
            // txtOwnTotAmt
            // 
            this.txtOwnTotAmt.BackColor = System.Drawing.SystemColors.Info;
            this.txtOwnTotAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOwnTotAmt.Location = new System.Drawing.Point(221, 73);
            this.txtOwnTotAmt.MaxLength = 10;
            this.txtOwnTotAmt.Name = "txtOwnTotAmt";
            this.txtOwnTotAmt.ReadOnly = true;
            this.txtOwnTotAmt.Size = new System.Drawing.Size(89, 22);
            this.txtOwnTotAmt.TabIndex = 16;
            this.txtOwnTotAmt.TabStop = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label21.Location = new System.Drawing.Point(136, 76);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(83, 17);
            this.label21.TabIndex = 8;
            this.label21.Text = "Tot Amount";
            // 
            // txtOwnOthers
            // 
            this.txtOwnOthers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOwnOthers.Location = new System.Drawing.Point(53, 72);
            this.txtOwnOthers.MaxLength = 10;
            this.txtOwnOthers.Name = "txtOwnOthers";
            this.txtOwnOthers.Size = new System.Drawing.Size(80, 22);
            this.txtOwnOthers.TabIndex = 7;
            this.txtOwnOthers.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtOwnOthers_KeyUp);
            this.txtOwnOthers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOwnOthers_KeyPress);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label22.Location = new System.Drawing.Point(3, 71);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(49, 17);
            this.label22.TabIndex = 6;
            this.label22.Text = "Others";
            // 
            // txtOwnDiesel
            // 
            this.txtOwnDiesel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOwnDiesel.Location = new System.Drawing.Point(221, 46);
            this.txtOwnDiesel.MaxLength = 10;
            this.txtOwnDiesel.Name = "txtOwnDiesel";
            this.txtOwnDiesel.Size = new System.Drawing.Size(89, 22);
            this.txtOwnDiesel.TabIndex = 6;
            this.txtOwnDiesel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtOwnDiesel_KeyUp);
            this.txtOwnDiesel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOwnDiesel_KeyPress);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label23.Location = new System.Drawing.Point(172, 49);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(46, 17);
            this.label23.TabIndex = 4;
            this.label23.Text = "Diesel";
            // 
            // txtOwnRent
            // 
            this.txtOwnRent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOwnRent.Location = new System.Drawing.Point(52, 47);
            this.txtOwnRent.MaxLength = 10;
            this.txtOwnRent.Name = "txtOwnRent";
            this.txtOwnRent.Size = new System.Drawing.Size(81, 22);
            this.txtOwnRent.TabIndex = 5;
            this.txtOwnRent.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtOwnRent_KeyUp);
            this.txtOwnRent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOwnRent_KeyPress);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label24.Location = new System.Drawing.Point(9, 46);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(36, 17);
            this.label24.TabIndex = 2;
            this.label24.Text = "Rent";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.Navy;
            this.label25.Location = new System.Drawing.Point(483, 18);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(37, 16);
            this.label25.TabIndex = 1;
            this.label25.Text = "Hire";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Navy;
            this.label26.Location = new System.Drawing.Point(133, 18);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(93, 16);
            this.label26.TabIndex = 0;
            this.label26.Text = "Own Vehicle";
            // 
            // txtGLEcode
            // 
            this.txtGLEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGLEcode.Location = new System.Drawing.Point(107, 89);
            this.txtGLEcode.MaxLength = 20;
            this.txtGLEcode.Name = "txtGLEcode";
            this.txtGLEcode.Size = new System.Drawing.Size(68, 22);
            this.txtGLEcode.TabIndex = 3;
            this.txtGLEcode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtGLEcode_KeyUp);
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(73, 247);
            this.txtRemarks.MaxLength = 100;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(570, 96);
            this.txtRemarks.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label14.Location = new System.Drawing.Point(9, 258);
            this.label14.Margin = new System.Windows.Forms.Padding(0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 15);
            this.label14.TabIndex = 15;
            this.label14.Text = "Remarks";
            // 
            // txtDistanceKM
            // 
            this.txtDistanceKM.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDistanceKM.Location = new System.Drawing.Point(444, 119);
            this.txtDistanceKM.MaxLength = 100;
            this.txtDistanceKM.Name = "txtDistanceKM";
            this.txtDistanceKM.Size = new System.Drawing.Size(61, 22);
            this.txtDistanceKM.TabIndex = 4;
            this.txtDistanceKM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDistanceKM_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(278, 119);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "PU/SP To Camp Dist KM";
            // 
            // txtCampName
            // 
            this.txtCampName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCampName.Location = new System.Drawing.Point(107, 114);
            this.txtCampName.MaxLength = 100;
            this.txtCampName.Name = "txtCampName";
            this.txtCampName.Size = new System.Drawing.Size(166, 22);
            this.txtCampName.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(17, 118);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Camp Name";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label16.Location = new System.Drawing.Point(11, 92);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(92, 15);
            this.label16.TabIndex = 6;
            this.label16.Text = "GL/GC Ecode";
            // 
            // cbBranches
            // 
            this.cbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranches.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranches.FormattingEnabled = true;
            this.cbBranches.ItemHeight = 15;
            this.cbBranches.Location = new System.Drawing.Point(105, 62);
            this.cbBranches.Name = "cbBranches";
            this.cbBranches.Size = new System.Drawing.Size(399, 23);
            this.cbBranches.Sorted = true;
            this.cbBranches.TabIndex = 2;
            this.cbBranches.SelectedIndexChanged += new System.EventHandler(this.cbBranches_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(47, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Branch";
            // 
            // txtDocMonth
            // 
            this.txtDocMonth.BackColor = System.Drawing.SystemColors.Info;
            this.txtDocMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocMonth.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocMonth.Location = new System.Drawing.Point(107, 13);
            this.txtDocMonth.MaxLength = 20;
            this.txtDocMonth.Name = "txtDocMonth";
            this.txtDocMonth.Size = new System.Drawing.Size(89, 22);
            this.txtDocMonth.TabIndex = 1;
            this.txtDocMonth.TabStop = false;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label67.Location = new System.Drawing.Point(30, 15);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(72, 15);
            this.label67.TabIndex = 0;
            this.label67.Text = "DocMonth";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDelete);
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Location = new System.Drawing.Point(111, 354);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(467, 47);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.Location = new System.Drawing.Point(242, 11);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(78, 30);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "D&elete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(158, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 30);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "&Clear";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(331, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 30);
            this.btnClose.TabIndex = 15;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "C&lose";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(75, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 30);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // TransportCostSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(666, 453);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "TransportCostSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transport Cost Summary";
            this.Load += new System.EventHandler(this.TransportCostSummary_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtDocMonth;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.ComboBox cbBranches;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtCampName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDistanceKM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cbEcode;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtHireTotAmt;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtHireOthers;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtHireDiesel;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtHireRent;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtOwnTotAmt;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtOwnOthers;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtOwnDiesel;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtOwnRent;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        public System.Windows.Forms.TextBox txtGLEcode;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label label3;
    }
}