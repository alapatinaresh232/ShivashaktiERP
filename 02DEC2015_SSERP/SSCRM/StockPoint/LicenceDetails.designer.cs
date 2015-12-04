namespace SSCRM
{
    partial class LicenceDetails
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cmbWorkStatus = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmpName = new System.Windows.Forms.TextBox();
            this.txtEcode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dtVldTo = new System.Windows.Forms.DateTimePicker();
            this.dtVldFrm = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.txtLicenceNo = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.cmbLicStatus_optional = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cmbLicType_optional = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox6.Controls.Add(this.cmbWorkStatus);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.txtEmpName);
            this.groupBox6.Controls.Add(this.txtEcode);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.groupBox5);
            this.groupBox6.Controls.Add(this.dtVldTo);
            this.groupBox6.Controls.Add(this.dtVldFrm);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Controls.Add(this.txtLicenceNo);
            this.groupBox6.Controls.Add(this.label19);
            this.groupBox6.Controls.Add(this.label20);
            this.groupBox6.Controls.Add(this.cmbLicStatus_optional);
            this.groupBox6.Controls.Add(this.label21);
            this.groupBox6.Controls.Add(this.cmbLicType_optional);
            this.groupBox6.Controls.Add(this.label26);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox6.ForeColor = System.Drawing.Color.Navy;
            this.groupBox6.Location = new System.Drawing.Point(-1, -3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(459, 207);
            this.groupBox6.TabIndex = 85;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Licence Details";
            // 
            // cmbWorkStatus
            // 
            this.cmbWorkStatus.BackColor = System.Drawing.SystemColors.Info;
            this.cmbWorkStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWorkStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWorkStatus.ForeColor = System.Drawing.SystemColors.MenuText;
            this.cmbWorkStatus.FormattingEnabled = true;
            this.cmbWorkStatus.Items.AddRange(new object[] {
            "--select--",
            "WORKING",
            "RESIGNED",
            "TRANSFERED"});
            this.cmbWorkStatus.Location = new System.Drawing.Point(118, 82);
            this.cmbWorkStatus.Name = "cmbWorkStatus";
            this.cmbWorkStatus.Size = new System.Drawing.Size(115, 24);
            this.cmbWorkStatus.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(7, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 16);
            this.label2.TabIndex = 111;
            this.label2.Text = "Working Status";
            // 
            // txtEmpName
            // 
            this.txtEmpName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtEmpName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpName.Location = new System.Drawing.Point(191, 56);
            this.txtEmpName.Name = "txtEmpName";
            this.txtEmpName.Size = new System.Drawing.Size(260, 22);
            this.txtEmpName.TabIndex = 109;
            // 
            // txtEcode
            // 
            this.txtEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcode.Location = new System.Drawing.Point(119, 56);
            this.txtEcode.Name = "txtEcode";
            this.txtEcode.Size = new System.Drawing.Size(71, 22);
            this.txtEcode.TabIndex = 2;
            this.txtEcode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEcode_KeyUp);
            this.txtEcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEcode_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(9, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 16);
            this.label1.TabIndex = 106;
            this.label1.Text = "Authorised For";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnClose);
            this.groupBox5.Controls.Add(this.btnSave);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox5.Location = new System.Drawing.Point(121, 148);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(216, 45);
            this.groupBox5.TabIndex = 105;
            this.groupBox5.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(111, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 26);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoEllipsis = true;
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(36, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Add";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtVldTo
            // 
            this.dtVldTo.CustomFormat = "dd/MM/yyyy";
            this.dtVldTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtVldTo.Location = new System.Drawing.Point(312, 109);
            this.dtVldTo.Name = "dtVldTo";
            this.dtVldTo.Size = new System.Drawing.Size(103, 22);
            this.dtVldTo.TabIndex = 6;
            this.dtVldTo.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // dtVldFrm
            // 
            this.dtVldFrm.CustomFormat = "dd/MM/yyyy";
            this.dtVldFrm.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtVldFrm.Location = new System.Drawing.Point(119, 109);
            this.dtVldFrm.Name = "dtVldFrm";
            this.dtVldFrm.Size = new System.Drawing.Size(103, 22);
            this.dtVldFrm.TabIndex = 5;
            this.dtVldFrm.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label18.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label18.Location = new System.Drawing.Point(33, 112);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(87, 16);
            this.label18.TabIndex = 81;
            this.label18.Text = "Valid From ";
            // 
            // txtLicenceNo
            // 
            this.txtLicenceNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicenceNo.Location = new System.Drawing.Point(320, 82);
            this.txtLicenceNo.MaxLength = 30;
            this.txtLicenceNo.Name = "txtLicenceNo";
            this.txtLicenceNo.Size = new System.Drawing.Size(131, 22);
            this.txtLicenceNo.TabIndex = 4;
            this.txtLicenceNo.TabStop = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label19.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label19.Location = new System.Drawing.Point(233, 86);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(90, 16);
            this.label19.TabIndex = 79;
            this.label19.Text = "Licence No.";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label20.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label20.Location = new System.Drawing.Point(236, 112);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(67, 16);
            this.label20.TabIndex = 66;
            this.label20.Text = "Valid To";
            // 
            // cmbLicStatus_optional
            // 
            this.cmbLicStatus_optional.BackColor = System.Drawing.SystemColors.Info;
            this.cmbLicStatus_optional.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLicStatus_optional.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLicStatus_optional.ForeColor = System.Drawing.SystemColors.MenuText;
            this.cmbLicStatus_optional.FormattingEnabled = true;
            this.cmbLicStatus_optional.Items.AddRange(new object[] {
            "RUNNING",
            "LAPPSED",
            "CLOSED"});
            this.cmbLicStatus_optional.Location = new System.Drawing.Point(308, 27);
            this.cmbLicStatus_optional.Name = "cmbLicStatus_optional";
            this.cmbLicStatus_optional.Size = new System.Drawing.Size(143, 24);
            this.cmbLicStatus_optional.TabIndex = 1;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label21.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label21.Location = new System.Drawing.Point(257, 31);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(51, 16);
            this.label21.TabIndex = 77;
            this.label21.Text = "Status";
            // 
            // cmbLicType_optional
            // 
            this.cmbLicType_optional.BackColor = System.Drawing.SystemColors.Info;
            this.cmbLicType_optional.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLicType_optional.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLicType_optional.ForeColor = System.Drawing.SystemColors.MenuText;
            this.cmbLicType_optional.FormattingEnabled = true;
            this.cmbLicType_optional.Items.AddRange(new object[] {
            "RETAIL",
            "WHOLE SALE"});
            this.cmbLicType_optional.Location = new System.Drawing.Point(120, 27);
            this.cmbLicType_optional.Name = "cmbLicType_optional";
            this.cmbLicType_optional.Size = new System.Drawing.Size(126, 24);
            this.cmbLicType_optional.TabIndex = 0;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label26.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label26.Location = new System.Drawing.Point(17, 31);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(102, 16);
            this.label26.TabIndex = 66;
            this.label26.Text = "Licence Type";
            // 
            // LicenceDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 205);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox6);
            this.Name = "LicenceDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Licence Details";
            this.Load += new System.EventHandler(this.LicenceDetails_Load);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DateTimePicker dtVldTo;
        private System.Windows.Forms.DateTimePicker dtVldFrm;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.TextBox txtLicenceNo;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cmbLicStatus_optional;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cmbLicType_optional;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbWorkStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEmpName;
        private System.Windows.Forms.TextBox txtEcode;
    }
}