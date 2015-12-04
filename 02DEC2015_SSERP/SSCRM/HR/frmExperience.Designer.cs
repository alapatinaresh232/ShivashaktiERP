using GroupCtrl;
namespace SSCRM
{
    partial class frmExperience
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grouper1 = new GroupCtrl.Grouper();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOrganisation = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtJ_Desg = new System.Windows.Forms.TextBox();
            this.txtReason_optional = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtRemarks_optional = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSalary_num = new System.Windows.Forms.TextBox();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.txtL_Desg = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.addressCtrl1 = new SSCRM.AddressCtrl();
            this.grouper1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.PowderBlue;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.PowderBlue;
            this.grouper1.BackgroundGradientMode = GroupCtrl.Grouper.GroupBoxGradientMode.Vertical;
            this.grouper1.BorderColor = System.Drawing.Color.Empty;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.Controls.Add(this.txtOrganisation);
            this.grouper1.Controls.Add(this.groupBox1);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "";
            this.grouper1.Location = new System.Drawing.Point(1, -10);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 1;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(510, 280);
            this.grouper1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(13, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 14);
            this.label1.TabIndex = 13;
            this.label1.Text = "Organisation";
            // 
            // txtOrganisation
            // 
            this.txtOrganisation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrganisation.Location = new System.Drawing.Point(103, 34);
            this.txtOrganisation.Name = "txtOrganisation";
            this.txtOrganisation.Size = new System.Drawing.Size(141, 22);
            this.txtOrganisation.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtJ_Desg);
            this.groupBox1.Controls.Add(this.txtReason_optional);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.txtRemarks_optional);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtSalary_num);
            this.groupBox1.Controls.Add(this.dtpTo);
            this.groupBox1.Controls.Add(this.txtL_Desg);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.dtpFrom);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.addressCtrl1);
            this.groupBox1.Location = new System.Drawing.Point(2, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(503, 259);
            this.groupBox1.TabIndex = 71;
            this.groupBox1.TabStop = false;
            // 
            // txtJ_Desg
            // 
            this.txtJ_Desg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJ_Desg.Location = new System.Drawing.Point(102, 47);
            this.txtJ_Desg.Name = "txtJ_Desg";
            this.txtJ_Desg.Size = new System.Drawing.Size(140, 22);
            this.txtJ_Desg.TabIndex = 2;
            // 
            // txtReason_optional
            // 
            this.txtReason_optional.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReason_optional.Location = new System.Drawing.Point(334, 172);
            this.txtReason_optional.Multiline = true;
            this.txtReason_optional.Name = "txtReason_optional";
            this.txtReason_optional.Size = new System.Drawing.Size(156, 46);
            this.txtReason_optional.TabIndex = 7;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(264, 222);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 25);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtRemarks_optional
            // 
            this.txtRemarks_optional.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks_optional.Location = new System.Drawing.Point(82, 173);
            this.txtRemarks_optional.Multiline = true;
            this.txtRemarks_optional.Name = "txtRemarks_optional";
            this.txtRemarks_optional.Size = new System.Drawing.Size(160, 42);
            this.txtRemarks_optional.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(204, 222);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 25);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(381, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 14);
            this.label8.TabIndex = 27;
            this.label8.Text = "To";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label15.Location = new System.Drawing.Point(247, 176);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 28);
            this.label15.TabIndex = 34;
            this.label15.Text = "Reasons for\r\nLeaving";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label14.Location = new System.Drawing.Point(12, 176);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 14);
            this.label14.TabIndex = 33;
            this.label14.Text = "Remarks";
            // 
            // txtSalary_num
            // 
            this.txtSalary_num.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSalary_num.Location = new System.Drawing.Point(344, 72);
            this.txtSalary_num.MaxLength = 7;
            this.txtSalary_num.Name = "txtSalary_num";
            this.txtSalary_num.Size = new System.Drawing.Size(64, 22);
            this.txtSalary_num.TabIndex = 4;
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "MMM - yyyy";
            this.dtpTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(404, 22);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(86, 21);
            this.dtpTo.TabIndex = 1;
            this.dtpTo.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // txtL_Desg
            // 
            this.txtL_Desg.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtL_Desg.Location = new System.Drawing.Point(344, 47);
            this.txtL_Desg.Name = "txtL_Desg";
            this.txtL_Desg.Size = new System.Drawing.Size(146, 22);
            this.txtL_Desg.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(249, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 14);
            this.label4.TabIndex = 11;
            this.label4.Text = "From";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label13.Location = new System.Drawing.Point(293, 76);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 14);
            this.label13.TabIndex = 32;
            this.label13.Text = "Salary";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "MMM - yyyy";
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(289, 22);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(86, 21);
            this.dtpFrom.TabIndex = 0;
            this.dtpFrom.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label11.Location = new System.Drawing.Point(12, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 14);
            this.label11.TabIndex = 30;
            this.label11.Text = "Joining Desg.";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label12.Location = new System.Drawing.Point(248, 50);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(92, 14);
            this.label12.TabIndex = 31;
            this.label12.Text = "Leaving Desg.";
            // 
            // addressCtrl1
            // 
            this.addressCtrl1.District = "";
            this.addressCtrl1.HouseNo = "";
            this.addressCtrl1.LandMark = "";
            this.addressCtrl1.Location = new System.Drawing.Point(3, 71);
            this.addressCtrl1.Mondal = "";
            this.addressCtrl1.Name = "addressCtrl1";
            this.addressCtrl1.Pin = "";
            this.addressCtrl1.Size = new System.Drawing.Size(487, 102);
            this.addressCtrl1.State = "";
            this.addressCtrl1.TabIndex = 5;
            this.addressCtrl1.Village = "";
            // 
            // frmExperience
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 271);
            this.ControlBox = false;
            this.Controls.Add(this.grouper1);
            this.Name = "frmExperience";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Experience";
            this.Load += new System.EventHandler(this.frmExperience_Load);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Grouper grouper1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOrganisation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtReason_optional;
        private System.Windows.Forms.TextBox txtRemarks_optional;
        private System.Windows.Forms.TextBox txtSalary_num;
        private System.Windows.Forms.TextBox txtL_Desg;
        private System.Windows.Forms.TextBox txtJ_Desg;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.ToolTip toolTip1;
        private SSCRM.AddressCtrl addressCtrl1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}