namespace SSCRM
{
    partial class frmActionTakenForFF
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
            this.dtpActionDate = new System.Windows.Forms.DateTimePicker();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtFineAmt = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txtJvNo = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.txtActionTakenName = new System.Windows.Forms.TextBox();
            this.txtActionTakenEcode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.dtpActionDate);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.txtFineAmt);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.txtJvNo);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.txtActionTakenName);
            this.groupBox1.Controls.Add(this.txtActionTakenEcode);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(608, 241);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dtpActionDate
            // 
            this.dtpActionDate.CustomFormat = "dd/MM/yyyy";
            this.dtpActionDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpActionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpActionDate.Location = new System.Drawing.Point(134, 46);
            this.dtpActionDate.Name = "dtpActionDate";
            this.dtpActionDate.Size = new System.Drawing.Size(94, 22);
            this.dtpActionDate.TabIndex = 4;
            this.dtpActionDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label19.Location = new System.Drawing.Point(44, 49);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(88, 16);
            this.label19.TabIndex = 3;
            this.label19.Text = "Action Date";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Location = new System.Drawing.Point(109, 185);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(391, 45);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(235, 13);
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
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(158, 13);
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
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(81, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(136, 75);
            this.txtDescription.MaxLength = 500;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(434, 96);
            this.txtDescription.TabIndex = 10;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label21.Location = new System.Drawing.Point(45, 75);
            this.label21.Margin = new System.Windows.Forms.Padding(0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(87, 16);
            this.label21.TabIndex = 9;
            this.label21.Text = "Description";
            // 
            // txtFineAmt
            // 
            this.txtFineAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFineAmt.Location = new System.Drawing.Point(477, 46);
            this.txtFineAmt.MaxLength = 10;
            this.txtFineAmt.Name = "txtFineAmt";
            this.txtFineAmt.Size = new System.Drawing.Size(92, 23);
            this.txtFineAmt.TabIndex = 8;
            this.txtFineAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFineAmt_KeyPress);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label30.Location = new System.Drawing.Point(402, 49);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(71, 17);
            this.label30.TabIndex = 7;
            this.label30.Text = "Fine Amt";
            // 
            // txtJvNo
            // 
            this.txtJvNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtJvNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJvNo.Location = new System.Drawing.Point(286, 46);
            this.txtJvNo.MaxLength = 10;
            this.txtJvNo.Name = "txtJvNo";
            this.txtJvNo.Size = new System.Drawing.Size(110, 23);
            this.txtJvNo.TabIndex = 6;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label31.Location = new System.Drawing.Point(231, 49);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(51, 17);
            this.label31.TabIndex = 5;
            this.label31.Text = "JV No";
            // 
            // txtActionTakenName
            // 
            this.txtActionTakenName.BackColor = System.Drawing.SystemColors.Info;
            this.txtActionTakenName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtActionTakenName.Location = new System.Drawing.Point(224, 18);
            this.txtActionTakenName.MaxLength = 50;
            this.txtActionTakenName.Name = "txtActionTakenName";
            this.txtActionTakenName.ReadOnly = true;
            this.txtActionTakenName.Size = new System.Drawing.Size(346, 22);
            this.txtActionTakenName.TabIndex = 2;
            this.txtActionTakenName.TabStop = false;
            // 
            // txtActionTakenEcode
            // 
            this.txtActionTakenEcode.BackColor = System.Drawing.Color.White;
            this.txtActionTakenEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtActionTakenEcode.Location = new System.Drawing.Point(136, 18);
            this.txtActionTakenEcode.MaxLength = 7;
            this.txtActionTakenEcode.Name = "txtActionTakenEcode";
            this.txtActionTakenEcode.Size = new System.Drawing.Size(86, 22);
            this.txtActionTakenEcode.TabIndex = 1;
            this.txtActionTakenEcode.TabStop = false;
            this.txtActionTakenEcode.Validated += new System.EventHandler(this.txtActionTakenEcode_Validated);
            this.txtActionTakenEcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtActionTakenEcode_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(13, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "Action Taken By";
            // 
            // frmActionTakenForFF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(613, 247);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmActionTakenForFF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Action Taken By";
            this.Load += new System.EventHandler(this.frmActionTakenForFF_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtActionTakenName;
        private System.Windows.Forms.TextBox txtActionTakenEcode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFineAmt;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtJvNo;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker dtpActionDate;
        private System.Windows.Forms.Label label19;
    }
}