namespace SSCRM
{
    partial class EmpFromToSelection
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkBrAll = new System.Windows.Forms.CheckBox();
            this.chkDemoType = new System.Windows.Forms.CheckedListBox();
            this.lblDempType = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.cbRepType = new System.Windows.Forms.ComboBox();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEcode = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblEcode = new System.Windows.Forms.Label();
            this.btnReport = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.chkBrAll);
            this.groupBox3.Controls.Add(this.chkDemoType);
            this.groupBox3.Controls.Add(this.lblDempType);
            this.groupBox3.Controls.Add(this.btnDownload);
            this.groupBox3.Controls.Add(this.cbRepType);
            this.groupBox3.Controls.Add(this.dtpToDate);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.dtpFromDate);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtEcode);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.lblEcode);
            this.groupBox3.Controls.Add(this.btnReport);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Location = new System.Drawing.Point(2, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(485, 291);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // chkBrAll
            // 
            this.chkBrAll.AutoSize = true;
            this.chkBrAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBrAll.ForeColor = System.Drawing.Color.Navy;
            this.chkBrAll.Location = new System.Drawing.Point(167, 107);
            this.chkBrAll.Name = "chkBrAll";
            this.chkBrAll.Size = new System.Drawing.Size(93, 20);
            this.chkBrAll.TabIndex = 79;
            this.chkBrAll.Text = "Select All";
            this.chkBrAll.UseVisualStyleBackColor = true;
            this.chkBrAll.CheckedChanged += new System.EventHandler(this.chkBrAll_CheckedChanged);
            // 
            // chkDemoType
            // 
            this.chkDemoType.CheckOnClick = true;
            this.chkDemoType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDemoType.FormattingEnabled = true;
            this.chkDemoType.Location = new System.Drawing.Point(79, 131);
            this.chkDemoType.Name = "chkDemoType";
            this.chkDemoType.Size = new System.Drawing.Size(317, 106);
            this.chkDemoType.TabIndex = 77;
            this.chkDemoType.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkDemoType_ItemCheck);
            // 
            // lblDempType
            // 
            this.lblDempType.AutoSize = true;
            this.lblDempType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDempType.ForeColor = System.Drawing.Color.Navy;
            this.lblDempType.Location = new System.Drawing.Point(37, 112);
            this.lblDempType.Name = "lblDempType";
            this.lblDempType.Size = new System.Drawing.Size(89, 16);
            this.lblDempType.TabIndex = 78;
            this.lblDempType.Text = "Demo Type";
            // 
            // btnDownload
            // 
            this.btnDownload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDownload.BackColor = System.Drawing.Color.OliveDrab;
            this.btnDownload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnDownload.FlatAppearance.BorderSize = 5;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDownload.Image = global::SSCRM.Properties.Resources.ic_download;
            this.btnDownload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDownload.Location = new System.Drawing.Point(376, 7);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(1);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(97, 24);
            this.btnDownload.TabIndex = 76;
            this.btnDownload.Tag = "Product  Search";
            this.btnDownload.Text = "Download";
            this.btnDownload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // cbRepType
            // 
            this.cbRepType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRepType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRepType.FormattingEnabled = true;
            this.cbRepType.Location = new System.Drawing.Point(156, 22);
            this.cbRepType.Name = "cbRepType";
            this.cbRepType.Size = new System.Drawing.Size(196, 24);
            this.cbRepType.TabIndex = 47;
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MMM/yyyy";
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(157, 78);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(124, 22);
            this.dtpToDate.TabIndex = 45;
            this.dtpToDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(70, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 44;
            this.label1.Text = "To Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MMM/yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(160, 51);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(124, 22);
            this.dtpFromDate.TabIndex = 42;
            this.dtpFromDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(54, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 43;
            this.label3.Text = "From Date";
            // 
            // txtEcode
            // 
            this.txtEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcode.Location = new System.Drawing.Point(160, 21);
            this.txtEcode.MaxLength = 20;
            this.txtEcode.Name = "txtEcode";
            this.txtEcode.Size = new System.Drawing.Size(74, 21);
            this.txtEcode.TabIndex = 12;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(228, 248);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblEcode
            // 
            this.lblEcode.AutoSize = true;
            this.lblEcode.ForeColor = System.Drawing.Color.Navy;
            this.lblEcode.Location = new System.Drawing.Point(81, 27);
            this.lblEcode.Name = "lblEcode";
            this.lblEcode.Size = new System.Drawing.Size(53, 16);
            this.lblEcode.TabIndex = 13;
            this.lblEcode.Text = "Ecode";
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Location = new System.Drawing.Point(144, 248);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(78, 30);
            this.btnReport.TabIndex = 0;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Location = new System.Drawing.Point(50, 241);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 47);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            // 
            // EmpFromToSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(492, 296);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EmpFromToSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recruitement Report";
            this.Load += new System.EventHandler(this.EmpFromToSelection_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.TextBox txtEcode;
        private System.Windows.Forms.Label lblEcode;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbRepType;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.CheckedListBox chkDemoType;
        private System.Windows.Forms.Label lblDempType;
        private System.Windows.Forms.CheckBox chkBrAll;
    }
}