namespace SSCRM
{
    partial class AfterTrainingEmpPerf
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
            this.grpMonths = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAftTrMonths = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTrBeforeMonths = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromdate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkAllPrgs = new System.Windows.Forms.CheckBox();
            this.clbPrograms = new System.Windows.Forms.CheckedListBox();
            this.lblTrName = new System.Windows.Forms.Label();
            this.cbTrainerNames = new System.Windows.Forms.ComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblReportName = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grpMonths.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.grpMonths);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.chkAllPrgs);
            this.groupBox1.Controls.Add(this.clbPrograms);
            this.groupBox1.Controls.Add(this.lblTrName);
            this.groupBox1.Controls.Add(this.cbTrainerNames);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.lblReportName);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 543);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // grpMonths
            // 
            this.grpMonths.Controls.Add(this.label7);
            this.grpMonths.Controls.Add(this.txtAftTrMonths);
            this.grpMonths.Controls.Add(this.label8);
            this.grpMonths.Controls.Add(this.label6);
            this.grpMonths.Controls.Add(this.txtTrBeforeMonths);
            this.grpMonths.Controls.Add(this.label5);
            this.grpMonths.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMonths.ForeColor = System.Drawing.Color.Navy;
            this.grpMonths.Location = new System.Drawing.Point(13, 428);
            this.grpMonths.Name = "grpMonths";
            this.grpMonths.Size = new System.Drawing.Size(436, 67);
            this.grpMonths.TabIndex = 7;
            this.grpMonths.TabStop = false;
            this.grpMonths.Text = "Employee Performane";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(289, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "Months";
            // 
            // txtAftTrMonths
            // 
            this.txtAftTrMonths.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAftTrMonths.Location = new System.Drawing.Point(199, 42);
            this.txtAftTrMonths.MaxLength = 1;
            this.txtAftTrMonths.Name = "txtAftTrMonths";
            this.txtAftTrMonths.Size = new System.Drawing.Size(80, 22);
            this.txtAftTrMonths.TabIndex = 4;
            this.txtAftTrMonths.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAftTrMonths_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(88, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 16);
            this.label8.TabIndex = 3;
            this.label8.Text = "After Training";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(287, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Months";
            // 
            // txtTrBeforeMonths
            // 
            this.txtTrBeforeMonths.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrBeforeMonths.Location = new System.Drawing.Point(198, 18);
            this.txtTrBeforeMonths.MaxLength = 1;
            this.txtTrBeforeMonths.Name = "txtTrBeforeMonths";
            this.txtTrBeforeMonths.Size = new System.Drawing.Size(80, 22);
            this.txtTrBeforeMonths.TabIndex = 1;
            this.txtTrBeforeMonths.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTrBeforeMonths_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(75, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Training Before";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpToDate);
            this.groupBox2.Controls.Add(this.dtpFromdate);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(14, 34);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(436, 47);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Program Dates";
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(333, 17);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(96, 22);
            this.dtpToDate.TabIndex = 3;
            this.dtpToDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpToDate.ValueChanged += new System.EventHandler(this.dtpToDate_ValueChanged);
            // 
            // dtpFromdate
            // 
            this.dtpFromdate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromdate.Location = new System.Drawing.Point(162, 17);
            this.dtpFromdate.Name = "dtpFromdate";
            this.dtpFromdate.Size = new System.Drawing.Size(96, 22);
            this.dtpFromdate.TabIndex = 1;
            this.dtpFromdate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpFromdate.ValueChanged += new System.EventHandler(this.dtpFromdate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(264, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "To Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(80, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "From Date";
            // 
            // chkAllPrgs
            // 
            this.chkAllPrgs.AutoSize = true;
            this.chkAllPrgs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllPrgs.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.chkAllPrgs.Location = new System.Drawing.Point(160, 85);
            this.chkAllPrgs.Name = "chkAllPrgs";
            this.chkAllPrgs.Size = new System.Drawing.Size(42, 19);
            this.chkAllPrgs.TabIndex = 5;
            this.chkAllPrgs.Text = "All";
            this.chkAllPrgs.UseVisualStyleBackColor = true;
            this.chkAllPrgs.CheckedChanged += new System.EventHandler(this.chkAllPrgs_CheckedChanged);
            // 
            // clbPrograms
            // 
            this.clbPrograms.CheckOnClick = true;
            this.clbPrograms.FormattingEnabled = true;
            this.clbPrograms.Location = new System.Drawing.Point(17, 107);
            this.clbPrograms.Name = "clbPrograms";
            this.clbPrograms.Size = new System.Drawing.Size(430, 319);
            this.clbPrograms.TabIndex = 6;
            this.clbPrograms.SelectedIndexChanged += new System.EventHandler(this.clbPrograms_SelectedIndexChanged);
            this.clbPrograms.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbPrograms_ItemCheck);
            // 
            // lblTrName
            // 
            this.lblTrName.AutoSize = true;
            this.lblTrName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrName.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblTrName.Location = new System.Drawing.Point(14, 12);
            this.lblTrName.Name = "lblTrName";
            this.lblTrName.Size = new System.Drawing.Size(103, 16);
            this.lblTrName.TabIndex = 0;
            this.lblTrName.Text = "Trainer Name";
            // 
            // cbTrainerNames
            // 
            this.cbTrainerNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTrainerNames.FormattingEnabled = true;
            this.cbTrainerNames.Location = new System.Drawing.Point(119, 8);
            this.cbTrainerNames.Name = "cbTrainerNames";
            this.cbTrainerNames.Size = new System.Drawing.Size(331, 24);
            this.cbTrainerNames.TabIndex = 1;
            this.cbTrainerNames.SelectedIndexChanged += new System.EventHandler(this.cbTrainerNames_SelectedIndexChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.Navy;
            this.lblName.Location = new System.Drawing.Point(16, 86);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(124, 16);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Program Details ";
            // 
            // lblReportName
            // 
            this.lblReportName.AutoSize = true;
            this.lblReportName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportName.ForeColor = System.Drawing.Color.Navy;
            this.lblReportName.Location = new System.Drawing.Point(7, 76);
            this.lblReportName.Name = "lblReportName";
            this.lblReportName.Size = new System.Drawing.Size(0, 17);
            this.lblReportName.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.btnReport);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Location = new System.Drawing.Point(60, 492);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(354, 46);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Location = new System.Drawing.Point(97, 12);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(78, 30);
            this.btnReport.TabIndex = 0;
            this.btnReport.Text = "&Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(179, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 30);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // AfterTrainingEmpPerf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(481, 548);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AfterTrainingEmpPerf";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Before/After Training Emp Performance";
            this.Load += new System.EventHandler(this.AfterTrainingEmpPerf_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpMonths.ResumeLayout(false);
            this.grpMonths.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTrName;
        private System.Windows.Forms.ComboBox cbTrainerNames;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblReportName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkAllPrgs;
        private System.Windows.Forms.CheckedListBox clbPrograms;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpMonths;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAftTrMonths;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTrBeforeMonths;
        private System.Windows.Forms.Label label5;
    }
}