namespace SSCRM
{
    partial class OrderFrmMissingFineCircular
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtOtherFineAmt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOtherFineDesc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMinOrderForms = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaxGrps = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbRole = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tvBranches = new SSCRM.TriStateTreeView();
            this.chkBranchAll = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clbCompany = new System.Windows.Forms.CheckedListBox();
            this.chkCompAll = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFineAmt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnDelete);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Location = new System.Drawing.Point(107, 235);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 45);
            this.groupBox4.TabIndex = 132;
            this.groupBox4.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(276, 13);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 23;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDelete.Enabled = false;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.Location = new System.Drawing.Point(187, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(74, 26);
            this.btnDelete.TabIndex = 22;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(111, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 26);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoEllipsis = true;
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(36, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtOtherFineAmt
            // 
            this.txtOtherFineAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherFineAmt.Location = new System.Drawing.Point(164, 208);
            this.txtOtherFineAmt.MaxLength = 9;
            this.txtOtherFineAmt.Name = "txtOtherFineAmt";
            this.txtOtherFineAmt.Size = new System.Drawing.Size(126, 21);
            this.txtOtherFineAmt.TabIndex = 145;
            this.txtOtherFineAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOtherFineAmt_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(27, 211);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 16);
            this.label4.TabIndex = 144;
            this.label4.Text = "Other Fine Amount";
            // 
            // txtOtherFineDesc
            // 
            this.txtOtherFineDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherFineDesc.Location = new System.Drawing.Point(165, 184);
            this.txtOtherFineDesc.MaxLength = 9;
            this.txtOtherFineDesc.Name = "txtOtherFineDesc";
            this.txtOtherFineDesc.Size = new System.Drawing.Size(126, 21);
            this.txtOtherFineDesc.TabIndex = 143;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(2, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 16);
            this.label3.TabIndex = 142;
            this.label3.Text = "Other Fine Description";
            // 
            // txtMinOrderForms
            // 
            this.txtMinOrderForms.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinOrderForms.Location = new System.Drawing.Point(447, 184);
            this.txtMinOrderForms.MaxLength = 9;
            this.txtMinOrderForms.Name = "txtMinOrderForms";
            this.txtMinOrderForms.Size = new System.Drawing.Size(126, 21);
            this.txtMinOrderForms.TabIndex = 141;
            this.txtMinOrderForms.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMinOrderForms_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(323, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 16);
            this.label2.TabIndex = 140;
            this.label2.Text = "Min Order Forms";
            // 
            // txtMaxGrps
            // 
            this.txtMaxGrps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxGrps.Location = new System.Drawing.Point(447, 208);
            this.txtMaxGrps.MaxLength = 9;
            this.txtMaxGrps.Name = "txtMaxGrps";
            this.txtMaxGrps.Size = new System.Drawing.Size(126, 21);
            this.txtMaxGrps.TabIndex = 139;
            this.txtMaxGrps.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaxGrps_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(315, 210);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 16);
            this.label1.TabIndex = 138;
            this.label1.Text = "Max Valid Groups";
            // 
            // cbRole
            // 
            this.cbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRole.FormattingEnabled = true;
            this.cbRole.Location = new System.Drawing.Point(202, 152);
            this.cbRole.Name = "cbRole";
            this.cbRole.Size = new System.Drawing.Size(228, 24);
            this.cbRole.TabIndex = 136;
            this.cbRole.SelectedIndexChanged += new System.EventHandler(this.cbRole_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.PowderBlue;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(159, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 16);
            this.label5.TabIndex = 135;
            this.label5.Text = "Role";
            // 
            // dtpFDate
            // 
            this.dtpFDate.CustomFormat = "dd/MMM/yyyy";
            this.dtpFDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFDate.Location = new System.Drawing.Point(49, 150);
            this.dtpFDate.Name = "dtpFDate";
            this.dtpFDate.Size = new System.Drawing.Size(106, 22);
            this.dtpFDate.TabIndex = 134;
            this.dtpFDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblDate.Location = new System.Drawing.Point(6, 153);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(41, 16);
            this.lblDate.TabIndex = 133;
            this.lblDate.Text = "WEF";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tvBranches);
            this.groupBox5.Controls.Add(this.chkBranchAll);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.Color.Navy;
            this.groupBox5.Location = new System.Drawing.Point(324, 19);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(293, 128);
            this.groupBox5.TabIndex = 131;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Branch";
            // 
            // tvBranches
            // 
            this.tvBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvBranches.Location = new System.Drawing.Point(9, 18);
            this.tvBranches.Name = "tvBranches";
            this.tvBranches.Size = new System.Drawing.Size(270, 109);
            this.tvBranches.TabIndex = 1;
            this.tvBranches.TriStateStyleProperty = SSCRM.TriStateTreeView.TriStateStyles.Standard;
            // 
            // chkBranchAll
            // 
            this.chkBranchAll.AutoSize = true;
            this.chkBranchAll.ForeColor = System.Drawing.Color.Green;
            this.chkBranchAll.Location = new System.Drawing.Point(223, -3);
            this.chkBranchAll.Name = "chkBranchAll";
            this.chkBranchAll.Size = new System.Drawing.Size(45, 22);
            this.chkBranchAll.TabIndex = 0;
            this.chkBranchAll.Text = "All";
            this.chkBranchAll.UseVisualStyleBackColor = true;
            this.chkBranchAll.CheckedChanged += new System.EventHandler(this.chkBranchAll_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clbCompany);
            this.groupBox2.Controls.Add(this.chkCompAll);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(22, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(296, 128);
            this.groupBox2.TabIndex = 130;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Company";
            // 
            // clbCompany
            // 
            this.clbCompany.CheckOnClick = true;
            this.clbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbCompany.FormattingEnabled = true;
            this.clbCompany.Location = new System.Drawing.Point(6, 18);
            this.clbCompany.Name = "clbCompany";
            this.clbCompany.Size = new System.Drawing.Size(284, 100);
            this.clbCompany.TabIndex = 1;
            this.clbCompany.SelectedIndexChanged += new System.EventHandler(this.clbCompany_SelectedIndexChanged);
            // 
            // chkCompAll
            // 
            this.chkCompAll.AutoSize = true;
            this.chkCompAll.ForeColor = System.Drawing.Color.Green;
            this.chkCompAll.Location = new System.Drawing.Point(236, -4);
            this.chkCompAll.Name = "chkCompAll";
            this.chkCompAll.Size = new System.Drawing.Size(45, 22);
            this.chkCompAll.TabIndex = 0;
            this.chkCompAll.Text = "All";
            this.chkCompAll.UseVisualStyleBackColor = true;
            this.chkCompAll.CheckedChanged += new System.EventHandler(this.chkCompAll_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(436, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 16);
            this.label6.TabIndex = 146;
            this.label6.Text = "Fine Amount";
            // 
            // txtFineAmt
            // 
            this.txtFineAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFineAmt.Location = new System.Drawing.Point(531, 151);
            this.txtFineAmt.MaxLength = 9;
            this.txtFineAmt.Name = "txtFineAmt";
            this.txtFineAmt.Size = new System.Drawing.Size(104, 21);
            this.txtFineAmt.TabIndex = 147;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.txtFineAmt);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblDate);
            this.groupBox1.Controls.Add(this.dtpFDate);
            this.groupBox1.Controls.Add(this.txtOtherFineAmt);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbRole);
            this.groupBox1.Controls.Add(this.txtOtherFineDesc);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtMaxGrps);
            this.groupBox1.Controls.Add(this.txtMinOrderForms);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(3, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(643, 292);
            this.groupBox1.TabIndex = 148;
            this.groupBox1.TabStop = false;
            // 
            // OrderFrmMissingFineCircular
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(657, 306);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "OrderFrmMissingFineCircular";
            this.Text = "OrderFrmMissingFineCircular";
            this.Load += new System.EventHandler(this.OrderFrmMissingFineCircular_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtOtherFineAmt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOtherFineDesc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMinOrderForms;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaxGrps;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbRole;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.GroupBox groupBox5;
        private TriStateTreeView tvBranches;
        private System.Windows.Forms.CheckBox chkBranchAll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox clbCompany;
        private System.Windows.Forms.CheckBox chkCompAll;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFineAmt;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}