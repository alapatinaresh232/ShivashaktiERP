namespace SSCRM
{
    partial class SVAttendedStudentDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SVAttendedStudentDetails));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.cbRelation = new System.Windows.Forms.ComboBox();
            this.txtRelationName = new System.Windows.Forms.TextBox();
            this.txtStudentName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txtPin = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtHouseNo = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtVillage = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.btnVillageSearch = new System.Windows.Forms.Button();
            this.txtVillSearch = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtDistrict = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtMandal = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtLandMark = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.cbVillage = new MultiColumnComboBoxDemo.MultiColumnComboBox();
            this.groupBox4.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox4.Controls.Add(this.txtRemarks);
            this.groupBox4.Controls.Add(this.label32);
            this.groupBox4.Controls.Add(this.cbRelation);
            this.groupBox4.Controls.Add(this.txtRelationName);
            this.groupBox4.Controls.Add(this.txtStudentName);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.groupBox8);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Navy;
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(450, 394);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Student Details";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(75, 281);
            this.txtRemarks.MaxLength = 500;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(366, 65);
            this.txtRemarks.TabIndex = 11;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label32.Location = new System.Drawing.Point(10, 281);
            this.label32.Margin = new System.Windows.Forms.Padding(0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(64, 15);
            this.label32.TabIndex = 5;
            this.label32.Text = "Remarks";
            // 
            // cbRelation
            // 
            this.cbRelation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRelation.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbRelation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRelation.FormattingEnabled = true;
            this.cbRelation.Items.AddRange(new object[] {
            "FATHER",
            "GUARDIAN"});
            this.cbRelation.Location = new System.Drawing.Point(20, 45);
            this.cbRelation.Name = "cbRelation";
            this.cbRelation.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbRelation.Size = new System.Drawing.Size(97, 24);
            this.cbRelation.Sorted = true;
            this.cbRelation.TabIndex = 2;
            // 
            // txtRelationName
            // 
            this.txtRelationName.BackColor = System.Drawing.Color.White;
            this.txtRelationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRelationName.Location = new System.Drawing.Point(124, 46);
            this.txtRelationName.MaxLength = 50;
            this.txtRelationName.Name = "txtRelationName";
            this.txtRelationName.Size = new System.Drawing.Size(265, 21);
            this.txtRelationName.TabIndex = 3;
            this.txtRelationName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRelationName_KeyPress);
            // 
            // txtStudentName
            // 
            this.txtStudentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStudentName.Location = new System.Drawing.Point(124, 22);
            this.txtStudentName.MaxLength = 50;
            this.txtStudentName.Name = "txtStudentName";
            this.txtStudentName.Size = new System.Drawing.Size(265, 21);
            this.txtStudentName.TabIndex = 1;
            this.txtStudentName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStudentName_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label13.Location = new System.Drawing.Point(16, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(105, 16);
            this.label13.TabIndex = 0;
            this.label13.Text = "Student Name";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnClose);
            this.groupBox8.Controls.Add(this.btnClear);
            this.groupBox8.Controls.Add(this.btnSave);
            this.groupBox8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox8.Location = new System.Drawing.Point(77, 345);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(297, 45);
            this.groupBox8.TabIndex = 7;
            this.groupBox8.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(189, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 26);
            this.btnClose.TabIndex = 14;
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
            this.btnClear.Location = new System.Drawing.Point(111, 13);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(74, 26);
            this.btnClear.TabIndex = 13;
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
            this.btnSave.Location = new System.Drawing.Point(34, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtMobileNo);
            this.groupBox6.Controls.Add(this.label26);
            this.groupBox6.Controls.Add(this.txtPin);
            this.groupBox6.Controls.Add(this.label25);
            this.groupBox6.Controls.Add(this.txtHouseNo);
            this.groupBox6.Controls.Add(this.label24);
            this.groupBox6.Controls.Add(this.cbVillage);
            this.groupBox6.Controls.Add(this.txtVillage);
            this.groupBox6.Controls.Add(this.label23);
            this.groupBox6.Controls.Add(this.btnVillageSearch);
            this.groupBox6.Controls.Add(this.txtVillSearch);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Controls.Add(this.txtState);
            this.groupBox6.Controls.Add(this.label19);
            this.groupBox6.Controls.Add(this.txtDistrict);
            this.groupBox6.Controls.Add(this.label20);
            this.groupBox6.Controls.Add(this.txtMandal);
            this.groupBox6.Controls.Add(this.label21);
            this.groupBox6.Controls.Add(this.txtLandMark);
            this.groupBox6.Controls.Add(this.label22);
            this.groupBox6.ForeColor = System.Drawing.Color.Navy;
            this.groupBox6.Location = new System.Drawing.Point(7, 71);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(434, 203);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Address";
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNo.Location = new System.Drawing.Point(114, 176);
            this.txtMobileNo.MaxLength = 13;
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(265, 21);
            this.txtMobileNo.TabIndex = 10;
            this.txtMobileNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMobileNo_KeyPress);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label26.Location = new System.Drawing.Point(38, 178);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(75, 16);
            this.label26.TabIndex = 17;
            this.label26.Text = "MobileNo";
            // 
            // txtPin
            // 
            this.txtPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPin.Location = new System.Drawing.Point(296, 154);
            this.txtPin.MaxLength = 20;
            this.txtPin.Name = "txtPin";
            this.txtPin.Size = new System.Drawing.Size(84, 21);
            this.txtPin.TabIndex = 9;
            this.txtPin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPin_KeyPress);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label25.Location = new System.Drawing.Point(266, 156);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(30, 16);
            this.label25.TabIndex = 16;
            this.label25.Text = "Pin";
            // 
            // txtHouseNo
            // 
            this.txtHouseNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHouseNo.Location = new System.Drawing.Point(116, 17);
            this.txtHouseNo.MaxLength = 20;
            this.txtHouseNo.Name = "txtHouseNo";
            this.txtHouseNo.Size = new System.Drawing.Size(262, 21);
            this.txtHouseNo.TabIndex = 4;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label24.Location = new System.Drawing.Point(36, 20);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(77, 16);
            this.label24.TabIndex = 0;
            this.label24.Text = "House No";
            // 
            // txtVillage
            // 
            this.txtVillage.BackColor = System.Drawing.SystemColors.Info;
            this.txtVillage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVillage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVillage.Location = new System.Drawing.Point(115, 88);
            this.txtVillage.MaxLength = 20;
            this.txtVillage.Name = "txtVillage";
            this.txtVillage.ReadOnly = true;
            this.txtVillage.Size = new System.Drawing.Size(265, 21);
            this.txtVillage.TabIndex = 9;
            this.txtVillage.TabStop = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label23.Location = new System.Drawing.Point(59, 91);
            this.label23.Margin = new System.Windows.Forms.Padding(0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(51, 15);
            this.label23.TabIndex = 8;
            this.label23.Text = "Village";
            // 
            // btnVillageSearch
            // 
            this.btnVillageSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnVillageSearch.BackColor = System.Drawing.Color.YellowGreen;
            this.btnVillageSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnVillageSearch.FlatAppearance.BorderSize = 5;
            this.btnVillageSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVillageSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnVillageSearch.Image")));
            this.btnVillageSearch.Location = new System.Drawing.Point(353, 63);
            this.btnVillageSearch.Margin = new System.Windows.Forms.Padding(1);
            this.btnVillageSearch.Name = "btnVillageSearch";
            this.btnVillageSearch.Size = new System.Drawing.Size(26, 22);
            this.btnVillageSearch.TabIndex = 8;
            this.btnVillageSearch.Tag = "Village Search";
            this.btnVillageSearch.UseVisualStyleBackColor = false;
            this.btnVillageSearch.Click += new System.EventHandler(this.btnVillageSearch_Click);
            // 
            // txtVillSearch
            // 
            this.txtVillSearch.BackColor = System.Drawing.SystemColors.Window;
            this.txtVillSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVillSearch.Location = new System.Drawing.Point(115, 64);
            this.txtVillSearch.MaxLength = 50;
            this.txtVillSearch.Name = "txtVillSearch";
            this.txtVillSearch.Size = new System.Drawing.Size(80, 21);
            this.txtVillSearch.TabIndex = 6;
            this.txtVillSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtVillSearch_KeyUp);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label18.Location = new System.Drawing.Point(11, 66);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(100, 15);
            this.label18.TabIndex = 4;
            this.label18.Text = "Village Search";
            // 
            // txtState
            // 
            this.txtState.BackColor = System.Drawing.SystemColors.Info;
            this.txtState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtState.Location = new System.Drawing.Point(115, 154);
            this.txtState.MaxLength = 50;
            this.txtState.Name = "txtState";
            this.txtState.ReadOnly = true;
            this.txtState.Size = new System.Drawing.Size(151, 21);
            this.txtState.TabIndex = 15;
            this.txtState.TabStop = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label19.Location = new System.Drawing.Point(72, 156);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(40, 15);
            this.label19.TabIndex = 14;
            this.label19.Text = "State";
            // 
            // txtDistrict
            // 
            this.txtDistrict.BackColor = System.Drawing.SystemColors.Info;
            this.txtDistrict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDistrict.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDistrict.Location = new System.Drawing.Point(115, 132);
            this.txtDistrict.MaxLength = 50;
            this.txtDistrict.Name = "txtDistrict";
            this.txtDistrict.ReadOnly = true;
            this.txtDistrict.Size = new System.Drawing.Size(265, 21);
            this.txtDistrict.TabIndex = 13;
            this.txtDistrict.TabStop = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label20.Location = new System.Drawing.Point(60, 134);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(52, 15);
            this.label20.TabIndex = 12;
            this.label20.Text = "District";
            // 
            // txtMandal
            // 
            this.txtMandal.BackColor = System.Drawing.SystemColors.Info;
            this.txtMandal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMandal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMandal.Location = new System.Drawing.Point(115, 110);
            this.txtMandal.MaxLength = 50;
            this.txtMandal.Name = "txtMandal";
            this.txtMandal.ReadOnly = true;
            this.txtMandal.Size = new System.Drawing.Size(265, 21);
            this.txtMandal.TabIndex = 11;
            this.txtMandal.TabStop = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label21.Location = new System.Drawing.Point(56, 112);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(55, 15);
            this.label21.TabIndex = 10;
            this.label21.Text = "Mandal";
            // 
            // txtLandMark
            // 
            this.txtLandMark.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLandMark.Location = new System.Drawing.Point(116, 40);
            this.txtLandMark.MaxLength = 20;
            this.txtLandMark.Name = "txtLandMark";
            this.txtLandMark.Size = new System.Drawing.Size(262, 21);
            this.txtLandMark.TabIndex = 5;
            this.txtLandMark.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLandMark_KeyPress);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label22.Location = new System.Drawing.Point(42, 43);
            this.label22.Margin = new System.Windows.Forms.Padding(0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(71, 15);
            this.label22.TabIndex = 2;
            this.label22.Text = "LandMark";
            // 
            // cbVillage
            // 
            this.cbVillage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbVillage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbVillage.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.cbVillage.FormattingEnabled = true;
            this.cbVillage.Location = new System.Drawing.Point(195, 63);
            this.cbVillage.Name = "cbVillage";
            this.cbVillage.Size = new System.Drawing.Size(158, 22);
            this.cbVillage.TabIndex = 7;
            this.cbVillage.SelectedIndexChanged += new System.EventHandler(this.cbVillage_SelectedIndexChanged);
            // 
            // SVAttendedStudentDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(456, 399);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SVAttendedStudentDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SVAttendedStudentDetails";
            this.Load += new System.EventHandler(this.SVAttendedStudentDetails_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label32;
        public System.Windows.Forms.ComboBox cbRelation;
        public System.Windows.Forms.TextBox txtRelationName;
        public System.Windows.Forms.TextBox txtStudentName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox6;
        public System.Windows.Forms.TextBox txtMobileNo;
        private System.Windows.Forms.Label label26;
        public System.Windows.Forms.TextBox txtPin;
        private System.Windows.Forms.Label label25;
        public System.Windows.Forms.TextBox txtHouseNo;
        private System.Windows.Forms.Label label24;
        private MultiColumnComboBoxDemo.MultiColumnComboBox cbVillage;
        public System.Windows.Forms.TextBox txtVillage;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnVillageSearch;
        public System.Windows.Forms.TextBox txtVillSearch;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Label label19;
        public System.Windows.Forms.TextBox txtDistrict;
        private System.Windows.Forms.Label label20;
        public System.Windows.Forms.TextBox txtMandal;
        private System.Windows.Forms.Label label21;
        public System.Windows.Forms.TextBox txtLandMark;
        private System.Windows.Forms.Label label22;
    }
}