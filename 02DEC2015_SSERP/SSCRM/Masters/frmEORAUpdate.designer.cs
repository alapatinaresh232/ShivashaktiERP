namespace SSCRM
{
    partial class frmEORAMasterUpdate
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEORAMasterUpdate));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.cbddlBranch = new System.Windows.Forms.ComboBox();
            this.bRANCHMASBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.cbddlEORADesig = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtEORACode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtHRISBranch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHRISDesig = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.meHRISDateOfJoin = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHRISAge = new System.Windows.Forms.TextBox();
            this.meHRISDataofBirth = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFatherName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMemberName = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtEcodeSearch = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.picEmpPhoto = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bRANCHMASBindingSource)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmpPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.cbddlBranch);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbddlEORADesig);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(796, 461);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Agent/Emp Update Form";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnClear);
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnUpdate);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Location = new System.Drawing.Point(274, 396);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(249, 45);
            this.groupBox4.TabIndex = 66;
            this.groupBox4.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClear.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClear.Location = new System.Drawing.Point(86, 13);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(74, 26);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "&Clear";
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(163, 13);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.AliceBlue;
            this.btnUpdate.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnUpdate.Location = new System.Drawing.Point(12, 13);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(74, 26);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // cbddlBranch
            // 
            this.cbddlBranch.DataSource = this.bRANCHMASBindingSource;
            this.cbddlBranch.DisplayMember = "BRANCH_NAME";
            this.cbddlBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbddlBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.980198F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbddlBranch.FormattingEnabled = true;
            this.cbddlBranch.Location = new System.Drawing.Point(414, 172);
            this.cbddlBranch.Name = "cbddlBranch";
            this.cbddlBranch.Size = new System.Drawing.Size(344, 24);
            this.cbddlBranch.TabIndex = 1;
            this.cbddlBranch.ValueMember = "BRANCH_CODE";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(411, 150);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 16);
            this.label8.TabIndex = 60;
            this.label8.Text = "Working Branch";
            // 
            // cbddlEORADesig
            // 
            this.cbddlEORADesig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbddlEORADesig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.980198F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbddlEORADesig.FormattingEnabled = true;
            this.cbddlEORADesig.Location = new System.Drawing.Point(414, 115);
            this.cbddlEORADesig.Name = "cbddlEORADesig";
            this.cbddlEORADesig.Size = new System.Drawing.Size(344, 24);
            this.cbddlEORADesig.TabIndex = 1;
            this.cbddlEORADesig.SelectedIndexChanged += new System.EventHandler(this.cbddlEORADesig_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(411, 97);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 16);
            this.label7.TabIndex = 58;
            this.label7.Text = "Designation";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.txtEORACode);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox5.Location = new System.Drawing.Point(401, 22);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(379, 369);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Present Details";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9.980198F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label18.Location = new System.Drawing.Point(13, 189);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(358, 78);
            this.label18.TabIndex = 78;
            this.label18.Text = resources.GetString("label18.Text");
            // 
            // txtEORACode
            // 
            this.txtEORACode.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtEORACode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEORACode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEORACode.Location = new System.Drawing.Point(110, 24);
            this.txtEORACode.MaxLength = 20;
            this.txtEORACode.Name = "txtEORACode";
            this.txtEORACode.ReadOnly = true;
            this.txtEORACode.Size = new System.Drawing.Size(83, 21);
            this.txtEORACode.TabIndex = 56;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(8, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 16);
            this.label6.TabIndex = 55;
            this.label6.Text = "Ecode/Acode";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtHRISBranch);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtHRISDesig);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.meHRISDateOfJoin);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtHRISAge);
            this.groupBox2.Controls.Add(this.meHRISDataofBirth);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtFatherName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtMemberName);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.txtEcodeSearch);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox2.Location = new System.Drawing.Point(16, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(379, 369);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "HRIS Details";
            // 
            // txtHRISBranch
            // 
            this.txtHRISBranch.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtHRISBranch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHRISBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHRISBranch.Location = new System.Drawing.Point(108, 148);
            this.txtHRISBranch.MaxLength = 50;
            this.txtHRISBranch.Name = "txtHRISBranch";
            this.txtHRISBranch.ReadOnly = true;
            this.txtHRISBranch.Size = new System.Drawing.Size(251, 21);
            this.txtHRISBranch.TabIndex = 69;
            this.txtHRISBranch.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(50, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 68;
            this.label5.Text = "Branch";
            // 
            // txtHRISDesig
            // 
            this.txtHRISDesig.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtHRISDesig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHRISDesig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHRISDesig.Location = new System.Drawing.Point(108, 123);
            this.txtHRISDesig.MaxLength = 50;
            this.txtHRISDesig.Name = "txtHRISDesig";
            this.txtHRISDesig.ReadOnly = true;
            this.txtHRISDesig.Size = new System.Drawing.Size(251, 21);
            this.txtHRISDesig.TabIndex = 67;
            this.txtHRISDesig.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(15, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 16);
            this.label3.TabIndex = 66;
            this.label3.Text = "Designation";
            // 
            // meHRISDateOfJoin
            // 
            this.meHRISDateOfJoin.BackColor = System.Drawing.SystemColors.MenuBar;
            this.meHRISDateOfJoin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.meHRISDateOfJoin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.meHRISDateOfJoin.Location = new System.Drawing.Point(278, 98);
            this.meHRISDateOfJoin.Mask = "00/00/0000";
            this.meHRISDateOfJoin.Name = "meHRISDateOfJoin";
            this.meHRISDateOfJoin.ReadOnly = true;
            this.meHRISDateOfJoin.Size = new System.Drawing.Size(81, 21);
            this.meHRISDateOfJoin.TabIndex = 63;
            this.meHRISDateOfJoin.ValidatingType = typeof(System.DateTime);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(240, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 16);
            this.label4.TabIndex = 65;
            this.label4.Text = "DOJ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(189, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 16);
            this.label2.TabIndex = 62;
            this.label2.Text = "/";
            // 
            // txtHRISAge
            // 
            this.txtHRISAge.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtHRISAge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHRISAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHRISAge.Location = new System.Drawing.Point(203, 98);
            this.txtHRISAge.MaxLength = 2;
            this.txtHRISAge.Name = "txtHRISAge";
            this.txtHRISAge.ReadOnly = true;
            this.txtHRISAge.Size = new System.Drawing.Size(28, 21);
            this.txtHRISAge.TabIndex = 60;
            // 
            // meHRISDataofBirth
            // 
            this.meHRISDataofBirth.BackColor = System.Drawing.SystemColors.MenuBar;
            this.meHRISDataofBirth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.meHRISDataofBirth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.meHRISDataofBirth.Location = new System.Drawing.Point(108, 98);
            this.meHRISDataofBirth.Mask = "00/00/0000";
            this.meHRISDataofBirth.Name = "meHRISDataofBirth";
            this.meHRISDataofBirth.ReadOnly = true;
            this.meHRISDataofBirth.Size = new System.Drawing.Size(81, 21);
            this.meHRISDataofBirth.TabIndex = 59;
            this.meHRISDataofBirth.ValidatingType = typeof(System.DateTime);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label9.Location = new System.Drawing.Point(25, 101);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 16);
            this.label9.TabIndex = 61;
            this.label9.Text = "DOB / Age";
            // 
            // txtFatherName
            // 
            this.txtFatherName.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtFatherName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFatherName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFatherName.Location = new System.Drawing.Point(108, 73);
            this.txtFatherName.MaxLength = 50;
            this.txtFatherName.Name = "txtFatherName";
            this.txtFatherName.ReadOnly = true;
            this.txtFatherName.Size = new System.Drawing.Size(251, 21);
            this.txtFatherName.TabIndex = 58;
            this.txtFatherName.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(8, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 16);
            this.label1.TabIndex = 57;
            this.label1.Text = "Father Name";
            // 
            // txtMemberName
            // 
            this.txtMemberName.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtMemberName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMemberName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMemberName.Location = new System.Drawing.Point(108, 48);
            this.txtMemberName.MaxLength = 50;
            this.txtMemberName.Name = "txtMemberName";
            this.txtMemberName.ReadOnly = true;
            this.txtMemberName.Size = new System.Drawing.Size(251, 21);
            this.txtMemberName.TabIndex = 56;
            this.txtMemberName.TabStop = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label17.Location = new System.Drawing.Point(60, 50);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(49, 16);
            this.label17.TabIndex = 55;
            this.label17.Text = "Name";
            // 
            // txtEcodeSearch
            // 
            this.txtEcodeSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcodeSearch.Location = new System.Drawing.Point(108, 22);
            this.txtEcodeSearch.MaxLength = 20;
            this.txtEcodeSearch.Name = "txtEcodeSearch";
            this.txtEcodeSearch.Size = new System.Drawing.Size(83, 21);
            this.txtEcodeSearch.TabIndex = 0;
            this.txtEcodeSearch.Validated += new System.EventHandler(this.txtEcodeSearch_Validated);
            this.txtEcodeSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEcodeSearch_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label16.Location = new System.Drawing.Point(6, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(103, 16);
            this.label16.TabIndex = 53;
            this.label16.Text = "Ecode/Acode";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.picEmpPhoto);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox3.Location = new System.Drawing.Point(108, 174);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(161, 187);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Photo";
            // 
            // picEmpPhoto
            // 
            this.picEmpPhoto.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.picEmpPhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picEmpPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEmpPhoto.Location = new System.Drawing.Point(20, 25);
            this.picEmpPhoto.Name = "picEmpPhoto";
            this.picEmpPhoto.Size = new System.Drawing.Size(122, 148);
            this.picEmpPhoto.TabIndex = 70;
            this.picEmpPhoto.TabStop = false;
            // 
            // frmEORAMasterUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(800, 464);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "frmEORAMasterUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EORA Master Update Form for CRM";
            this.Load += new System.EventHandler(this.frmEORAMasterUpdate_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bRANCHMASBindingSource)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picEmpPhoto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtEcodeSearch;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox txtMemberName;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.TextBox txtFatherName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHRISAge;
        private System.Windows.Forms.MaskedTextBox meHRISDataofBirth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MaskedTextBox meHRISDateOfJoin;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtHRISDesig;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtHRISBranch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox picEmpPhoto;
        private System.Windows.Forms.TextBox txtEORACode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbddlEORADesig;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbddlBranch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.BindingSource bRANCHMASBindingSource;
        private System.Windows.Forms.Button btnClear;

    }
}

