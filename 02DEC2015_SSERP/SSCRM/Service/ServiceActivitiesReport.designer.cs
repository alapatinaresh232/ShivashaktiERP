namespace SSCRM
{
    partial class ServiceActivitiesReport
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDsearch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpToDocMonth = new System.Windows.Forms.DateTimePicker();
            this.lblToMonth = new System.Windows.Forms.Label();
            this.dtpFrmDocMonth = new System.Windows.Forms.DateTimePicker();
            this.lblName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.clbEmployees = new System.Windows.Forms.CheckedListBox();
            this.chkEmployees = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.tvBranches = new SSCRM.TriStateTreeView();
            this.groupBox1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.txtDsearch);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpToDocMonth);
            this.groupBox1.Controls.Add(this.lblToMonth);
            this.groupBox1.Controls.Add(this.dtpFrmDocMonth);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tvBranches);
            this.groupBox1.Controls.Add(this.groupBox8);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(717, 511);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // txtDsearch
            // 
            this.txtDsearch.Location = new System.Drawing.Point(603, 45);
            this.txtDsearch.Name = "txtDsearch";
            this.txtDsearch.Size = new System.Drawing.Size(99, 20);
            this.txtDsearch.TabIndex = 188;
            this.txtDsearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDsearch_KeyUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(482, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 15);
            this.label5.TabIndex = 187;
            this.label5.Text = "Employee Search";
            // 
            // dtpToDocMonth
            // 
            this.dtpToDocMonth.CustomFormat = "MMM/yyyy";
            this.dtpToDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDocMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDocMonth.Location = new System.Drawing.Point(318, 16);
            this.dtpToDocMonth.Margin = new System.Windows.Forms.Padding(4);
            this.dtpToDocMonth.Name = "dtpToDocMonth";
            this.dtpToDocMonth.Size = new System.Drawing.Size(100, 22);
            this.dtpToDocMonth.TabIndex = 135;
            this.dtpToDocMonth.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpToDocMonth.ValueChanged += new System.EventHandler(this.dtpToDocMonth_ValueChanged);
            // 
            // lblToMonth
            // 
            this.lblToMonth.AutoSize = true;
            this.lblToMonth.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToMonth.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblToMonth.Location = new System.Drawing.Point(241, 19);
            this.lblToMonth.Name = "lblToMonth";
            this.lblToMonth.Size = new System.Drawing.Size(69, 17);
            this.lblToMonth.TabIndex = 134;
            this.lblToMonth.Text = "To Month";
            // 
            // dtpFrmDocMonth
            // 
            this.dtpFrmDocMonth.CustomFormat = "MMM/yyyy";
            this.dtpFrmDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrmDocMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrmDocMonth.Location = new System.Drawing.Point(111, 16);
            this.dtpFrmDocMonth.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFrmDocMonth.Name = "dtpFrmDocMonth";
            this.dtpFrmDocMonth.Size = new System.Drawing.Size(97, 22);
            this.dtpFrmDocMonth.TabIndex = 133;
            this.dtpFrmDocMonth.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpFrmDocMonth.ValueChanged += new System.EventHandler(this.dtpFrmDocMonth_ValueChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblName.Location = new System.Drawing.Point(18, 19);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(85, 17);
            this.lblName.TabIndex = 132;
            this.lblName.Text = "From Month";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(12, 52);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 15);
            this.label4.TabIndex = 136;
            this.label4.Text = "Company / Branch";
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox8.Controls.Add(this.clbEmployees);
            this.groupBox8.Controls.Add(this.chkEmployees);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.ForeColor = System.Drawing.Color.Navy;
            this.groupBox8.Location = new System.Drawing.Point(396, 61);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(308, 397);
            this.groupBox8.TabIndex = 131;
            this.groupBox8.TabStop = false;
            // 
            // clbEmployees
            // 
            this.clbEmployees.CheckOnClick = true;
            this.clbEmployees.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbEmployees.FormattingEnabled = true;
            this.clbEmployees.Location = new System.Drawing.Point(11, 22);
            this.clbEmployees.Name = "clbEmployees";
            this.clbEmployees.Size = new System.Drawing.Size(286, 372);
            this.clbEmployees.TabIndex = 2;
            // 
            // chkEmployees
            // 
            this.chkEmployees.AutoSize = true;
            this.chkEmployees.Location = new System.Drawing.Point(18, 0);
            this.chkEmployees.Name = "chkEmployees";
            this.chkEmployees.Size = new System.Drawing.Size(105, 21);
            this.chkEmployees.TabIndex = 1;
            this.chkEmployees.Text = "Employees";
            this.chkEmployees.UseVisualStyleBackColor = true;
            this.chkEmployees.CheckedChanged += new System.EventHandler(this.chkEmployees_CheckedChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox7.Controls.Add(this.btnClose);
            this.groupBox7.Controls.Add(this.btnReport);
            this.groupBox7.Location = new System.Drawing.Point(191, 464);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(335, 42);
            this.groupBox7.TabIndex = 128;
            this.groupBox7.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(169, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnReport.Location = new System.Drawing.Point(91, 10);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 30);
            this.btnReport.TabIndex = 8;
            this.btnReport.Text = "&Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // tvBranches
            // 
            this.tvBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvBranches.Location = new System.Drawing.Point(15, 70);
            this.tvBranches.Name = "tvBranches";
            this.tvBranches.Size = new System.Drawing.Size(373, 389);
            this.tvBranches.TabIndex = 137;
            this.tvBranches.TriStateStyleProperty = SSCRM.TriStateTreeView.TriStateStyles.Standard;
            this.tvBranches.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvBranches_AfterCheck);
           
            // 
            // ServiceActivitiesReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(724, 518);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServiceActivitiesReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tour Expenses Report";
            this.Load += new System.EventHandler(this.ServiceActivitiesReport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckedListBox clbEmployees;
        private System.Windows.Forms.CheckBox chkEmployees;
        private System.Windows.Forms.DateTimePicker dtpToDocMonth;
        private System.Windows.Forms.Label lblToMonth;
        private System.Windows.Forms.DateTimePicker dtpFrmDocMonth;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label4;
        private TriStateTreeView tvBranches;
        private System.Windows.Forms.TextBox txtDsearch;
        private System.Windows.Forms.Label label5;

    }
}