namespace SSCRM
{
    partial class Damaged_Stock_Received_Details
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
            this.btnReport = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.cbProbuct = new System.Windows.Forms.ComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.cbUnit = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TodtpDocMonth = new System.Windows.Forms.DateTimePicker();
            this.lblToMonth = new System.Windows.Forms.Label();
            this.FromdtpDocMonth = new System.Windows.Forms.DateTimePicker();
            this.lblFromMonth = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.btnReport);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.cbProbuct);
            this.groupBox1.Controls.Add(this.lblBranch);
            this.groupBox1.Controls.Add(this.cbUnit);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(511, 163);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReport.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnReport.Location = new System.Drawing.Point(176, 132);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(74, 26);
            this.btnReport.TabIndex = 8;
            this.btnReport.Text = "&Report";
            this.btnReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(255, 132);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label18.Location = new System.Drawing.Point(18, 96);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(61, 16);
            this.label18.TabIndex = 6;
            this.label18.Text = "Product";
            // 
            // cbProbuct
            // 
            this.cbProbuct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProbuct.FormattingEnabled = true;
            this.cbProbuct.Location = new System.Drawing.Point(95, 96);
            this.cbProbuct.MaxLength = 15;
            this.cbProbuct.Name = "cbProbuct";
            this.cbProbuct.Size = new System.Drawing.Size(396, 23);
            this.cbProbuct.TabIndex = 5;
            this.cbProbuct.SelectedIndexChanged += new System.EventHandler(this.cbProbuct_SelectedIndexChanged);
            this.cbProbuct.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbProbuct_KeyPress);
            // 
            // lblBranch
            // 
            this.lblBranch.AutoSize = true;
            this.lblBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBranch.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblBranch.Location = new System.Drawing.Point(6, 71);
            this.lblBranch.Margin = new System.Windows.Forms.Padding(0);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Size = new System.Drawing.Size(86, 16);
            this.lblBranch.TabIndex = 4;
            this.lblBranch.Text = "Destination";
            // 
            // cbUnit
            // 
            this.cbUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUnit.FormattingEnabled = true;
            this.cbUnit.Location = new System.Drawing.Point(95, 68);
            this.cbUnit.Name = "cbUnit";
            this.cbUnit.Size = new System.Drawing.Size(396, 23);
            this.cbUnit.TabIndex = 3;
            this.cbUnit.SelectedIndexChanged += new System.EventHandler(this.cbUnit_SelectedIndexChanged);
            this.cbUnit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbUnit_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.Controls.Add(this.TodtpDocMonth);
            this.groupBox2.Controls.Add(this.lblToMonth);
            this.groupBox2.Controls.Add(this.FromdtpDocMonth);
            this.groupBox2.Controls.Add(this.lblFromMonth);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(8, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(494, 51);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // TodtpDocMonth
            // 
            this.TodtpDocMonth.CustomFormat = "MMM/yyyy";
            this.TodtpDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TodtpDocMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TodtpDocMonth.Location = new System.Drawing.Point(376, 16);
            this.TodtpDocMonth.Name = "TodtpDocMonth";
            this.TodtpDocMonth.Size = new System.Drawing.Size(106, 22);
            this.TodtpDocMonth.TabIndex = 5;
            // 
            // lblToMonth
            // 
            this.lblToMonth.AutoSize = true;
            this.lblToMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToMonth.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblToMonth.Location = new System.Drawing.Point(261, 19);
            this.lblToMonth.Name = "lblToMonth";
            this.lblToMonth.Size = new System.Drawing.Size(114, 17);
            this.lblToMonth.TabIndex = 4;
            this.lblToMonth.Text = " To Doc Month";
            // 
            // FromdtpDocMonth
            // 
            this.FromdtpDocMonth.CustomFormat = "MMM/yyyy";
            this.FromdtpDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FromdtpDocMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromdtpDocMonth.Location = new System.Drawing.Point(139, 15);
            this.FromdtpDocMonth.Name = "FromdtpDocMonth";
            this.FromdtpDocMonth.Size = new System.Drawing.Size(108, 22);
            this.FromdtpDocMonth.TabIndex = 3;
            // 
            // lblFromMonth
            // 
            this.lblFromMonth.AutoSize = true;
            this.lblFromMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromMonth.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblFromMonth.Location = new System.Drawing.Point(4, 17);
            this.lblFromMonth.Name = "lblFromMonth";
            this.lblFromMonth.Size = new System.Drawing.Size(136, 17);
            this.lblFromMonth.TabIndex = 2;
            this.lblFromMonth.Text = "  From Doc Month";
            // 
            // Damaged_Stock_Received_Details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(515, 166);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "Damaged_Stock_Received_Details";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Damaged Stock Received Details";
            this.Load += new System.EventHandler(this.Damaged_Stock_Received_Details_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cbProbuct;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.ComboBox cbUnit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker TodtpDocMonth;
        private System.Windows.Forms.Label lblToMonth;
        private System.Windows.Forms.DateTimePicker FromdtpDocMonth;
        private System.Windows.Forms.Label lblFromMonth;
        private System.Windows.Forms.Button btnReport;
    }
}