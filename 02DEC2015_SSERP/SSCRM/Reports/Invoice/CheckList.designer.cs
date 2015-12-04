namespace SSCRM
{
    partial class CheckList
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
            this.lblCnt = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.chkBranch = new System.Windows.Forms.CheckBox();
            this.cgkCmp = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.chklBranch = new System.Windows.Forms.CheckedListBox();
            this.chklCompany = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.lblCnt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.chkBranch);
            this.groupBox1.Controls.Add(this.cgkCmp);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.chklBranch);
            this.groupBox1.Controls.Add(this.chklCompany);
            this.groupBox1.Location = new System.Drawing.Point(1, -3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(641, 277);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblCnt
            // 
            this.lblCnt.AutoSize = true;
            this.lblCnt.Location = new System.Drawing.Point(481, 136);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(35, 13);
            this.lblCnt.TabIndex = 17;
            this.lblCnt.Text = "label3";
            this.lblCnt.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label4.Location = new System.Drawing.Point(34, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "Select Company";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(322, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Select Branch";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label2.Location = new System.Drawing.Point(34, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Document Month";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "MMM/yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(162, 136);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(104, 22);
            this.dtpDate.TabIndex = 12;
            // 
            // chkBranch
            // 
            this.chkBranch.AutoSize = true;
            this.chkBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.chkBranch.ForeColor = System.Drawing.Color.MidnightBlue;
            this.chkBranch.Location = new System.Drawing.Point(432, 28);
            this.chkBranch.Name = "chkBranch";
            this.chkBranch.Size = new System.Drawing.Size(93, 20);
            this.chkBranch.TabIndex = 11;
            this.chkBranch.Text = "Select All";
            this.chkBranch.UseVisualStyleBackColor = true;
            this.chkBranch.CheckedChanged += new System.EventHandler(this.chkBranch_CheckedChanged);
            // 
            // cgkCmp
            // 
            this.cgkCmp.AutoSize = true;
            this.cgkCmp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.cgkCmp.ForeColor = System.Drawing.Color.MidnightBlue;
            this.cgkCmp.Location = new System.Drawing.Point(160, 26);
            this.cgkCmp.Name = "cgkCmp";
            this.cgkCmp.Size = new System.Drawing.Size(93, 20);
            this.cgkCmp.TabIndex = 10;
            this.cgkCmp.Text = "Select All";
            this.cgkCmp.UseVisualStyleBackColor = true;
            this.cgkCmp.CheckedChanged += new System.EventHandler(this.cgkCmp_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnReport);
            this.groupBox3.Location = new System.Drawing.Point(187, 219);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(266, 47);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(136, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Location = new System.Drawing.Point(52, 11);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(78, 30);
            this.btnReport.TabIndex = 0;
            this.btnReport.Text = "&Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // chklBranch
            // 
            this.chklBranch.CheckOnClick = true;
            this.chklBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklBranch.FormattingEnabled = true;
            this.chklBranch.Location = new System.Drawing.Point(325, 49);
            this.chklBranch.Name = "chklBranch";
            this.chklBranch.ScrollAlwaysVisible = true;
            this.chklBranch.Size = new System.Drawing.Size(282, 68);
            this.chklBranch.TabIndex = 1;
            // 
            // chklCompany
            // 
            this.chklCompany.CheckOnClick = true;
            this.chklCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklCompany.FormattingEnabled = true;
            this.chklCompany.Location = new System.Drawing.Point(37, 49);
            this.chklCompany.Name = "chklCompany";
            this.chklCompany.ScrollAlwaysVisible = true;
            this.chklCompany.Size = new System.Drawing.Size(282, 68);
            this.chklCompany.TabIndex = 0;
            this.chklCompany.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chklCompany_ItemCheck);
            // 
            // CheckList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 274);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "CheckList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Check List";
            this.Load += new System.EventHandler(this.CheckList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox chklCompany;
        private System.Windows.Forms.CheckedListBox chklBranch;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.CheckBox chkBranch;
        private System.Windows.Forms.CheckBox cgkCmp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCnt;
    }
}