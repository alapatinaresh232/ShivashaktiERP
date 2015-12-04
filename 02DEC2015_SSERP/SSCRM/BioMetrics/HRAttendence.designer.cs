namespace SSCRM
{
    partial class HRAttendence
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
            this.cbRepType = new System.Windows.Forms.ComboBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.dtpTDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.lblEcode = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.cbRepType);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.btnReport);
            this.groupBox1.Controls.Add(this.dtpTDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpFDate);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(1, -4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 207);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cbRepType
            // 
            this.cbRepType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRepType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRepType.FormattingEnabled = true;
            this.cbRepType.Location = new System.Drawing.Point(109, 92);
            this.cbRepType.Name = "cbRepType";
            this.cbRepType.Size = new System.Drawing.Size(110, 24);
            this.cbRepType.TabIndex = 49;
            this.cbRepType.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(155, 136);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 24;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReport
            // 
            this.btnReport.AutoEllipsis = true;
            this.btnReport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnReport.Location = new System.Drawing.Point(49, 136);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(100, 26);
            this.btnReport.TabIndex = 23;
            this.btnReport.Text = "&Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // dtpTDate
            // 
            this.dtpTDate.CustomFormat = "dd/MMM/yyyy";
            this.dtpTDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTDate.Location = new System.Drawing.Point(109, 62);
            this.dtpTDate.Name = "dtpTDate";
            this.dtpTDate.Size = new System.Drawing.Size(123, 22);
            this.dtpTDate.TabIndex = 22;
            this.dtpTDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpTDate.ValueChanged += new System.EventHandler(this.dtpTDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(65, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 16);
            this.label1.TabIndex = 21;
            this.label1.Text = "To";
            // 
            // dtpFDate
            // 
            this.dtpFDate.CustomFormat = "dd/MMM/yyyy";
            this.dtpFDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFDate.Location = new System.Drawing.Point(109, 31);
            this.dtpFDate.Name = "dtpFDate";
            this.dtpFDate.Size = new System.Drawing.Size(123, 22);
            this.dtpFDate.TabIndex = 20;
            this.dtpFDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpFDate.ValueChanged += new System.EventHandler(this.dtpFDate_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(63, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 16);
            this.label7.TabIndex = 19;
            this.label7.Text = "From";
            // 
            // lblEcode
            // 
            this.lblEcode.AutoSize = true;
            this.lblEcode.BackColor = System.Drawing.Color.PowderBlue;
            this.lblEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEcode.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblEcode.Location = new System.Drawing.Point(65, 91);
            this.lblEcode.Name = "lblEcode";
            this.lblEcode.Size = new System.Drawing.Size(44, 17);
            this.lblEcode.TabIndex = 48;
            this.lblEcode.Text = "Type";
            this.lblEcode.Visible = false;
            // 
            // HRAttendence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 203);
            this.ControlBox = false;
            this.Controls.Add(this.lblEcode);
            this.Controls.Add(this.groupBox1);
            this.Name = "HRAttendence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report";
            this.Load += new System.EventHandler(this.HRAttendence_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.DateTimePicker dtpTDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbRepType;
        private System.Windows.Forms.Label lblEcode;
    }
}