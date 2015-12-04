namespace SSCRM
{
    partial class RepDateCompBranchSelection
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
            this.chkTrnType = new System.Windows.Forms.CheckedListBox();
            this.lblTrnType = new System.Windows.Forms.Label();
            this.dtpToDocMonth = new System.Windows.Forms.DateTimePicker();
            this.lblToMonth = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.lblQty = new System.Windows.Forms.Label();
            this.dtpFrmDocMonth = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tvBranches = new SSCRM.TriStateTreeView();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.chkTrnType);
            this.groupBox1.Controls.Add(this.lblTrnType);
            this.groupBox1.Controls.Add(this.dtpToDocMonth);
            this.groupBox1.Controls.Add(this.lblToMonth);
            this.groupBox1.Controls.Add(this.txtQty);
            this.groupBox1.Controls.Add(this.lblQty);
            this.groupBox1.Controls.Add(this.dtpFrmDocMonth);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tvBranches);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(437, 543);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chkTrnType
            // 
            this.chkTrnType.CheckOnClick = true;
            this.chkTrnType.FormattingEnabled = true;
            this.chkTrnType.Location = new System.Drawing.Point(16, 415);
            this.chkTrnType.Name = "chkTrnType";
            this.chkTrnType.Size = new System.Drawing.Size(396, 79);
            this.chkTrnType.TabIndex = 70;
            this.chkTrnType.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkTrnType_ItemCheck);
            // 
            // lblTrnType
            // 
            this.lblTrnType.AutoSize = true;
            this.lblTrnType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrnType.ForeColor = System.Drawing.Color.Navy;
            this.lblTrnType.Location = new System.Drawing.Point(13, 398);
            this.lblTrnType.Name = "lblTrnType";
            this.lblTrnType.Size = new System.Drawing.Size(130, 16);
            this.lblTrnType.TabIndex = 71;
            this.lblTrnType.Text = "Transaction Type";
            // 
            // dtpToDocMonth
            // 
            this.dtpToDocMonth.CustomFormat = "MMM/yyyy";
            this.dtpToDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDocMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDocMonth.Location = new System.Drawing.Point(316, 13);
            this.dtpToDocMonth.Margin = new System.Windows.Forms.Padding(4);
            this.dtpToDocMonth.Name = "dtpToDocMonth";
            this.dtpToDocMonth.Size = new System.Drawing.Size(100, 22);
            this.dtpToDocMonth.TabIndex = 3;
            this.dtpToDocMonth.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // lblToMonth
            // 
            this.lblToMonth.AutoSize = true;
            this.lblToMonth.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToMonth.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblToMonth.Location = new System.Drawing.Point(239, 16);
            this.lblToMonth.Name = "lblToMonth";
            this.lblToMonth.Size = new System.Drawing.Size(69, 17);
            this.lblToMonth.TabIndex = 2;
            this.lblToMonth.Text = "To Month";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(316, 39);
            this.txtQty.MaxLength = 10;
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(99, 20);
            this.txtQty.TabIndex = 5;
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblQty.Location = new System.Drawing.Point(250, 41);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(59, 16);
            this.lblQty.TabIndex = 4;
            this.lblQty.Text = "Min Qty";
            // 
            // dtpFrmDocMonth
            // 
            this.dtpFrmDocMonth.CustomFormat = "dd/MM/yyyy";
            this.dtpFrmDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrmDocMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrmDocMonth.Location = new System.Drawing.Point(103, 13);
            this.dtpFrmDocMonth.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFrmDocMonth.Name = "dtpFrmDocMonth";
            this.dtpFrmDocMonth.Size = new System.Drawing.Size(97, 22);
            this.dtpFrmDocMonth.TabIndex = 1;
            this.dtpFrmDocMonth.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.btnDownload);
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnReport);
            this.groupBox3.Location = new System.Drawing.Point(8, 495);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(421, 42);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            // 
            // btnDownload
            // 
            this.btnDownload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDownload.BackColor = System.Drawing.Color.OliveDrab;
            this.btnDownload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnDownload.FlatAppearance.BorderSize = 5;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Image = global::SSCRM.Properties.Resources.ic_download;
            this.btnDownload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDownload.Location = new System.Drawing.Point(314, 12);
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
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(150, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(231, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 27);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnReport.Location = new System.Drawing.Point(74, 10);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 27);
            this.btnReport.TabIndex = 0;
            this.btnReport.Text = "&Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblName.Location = new System.Drawing.Point(16, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(37, 17);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(10, 49);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Company / Branch";
            // 
            // tvBranches
            // 
            this.tvBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvBranches.Location = new System.Drawing.Point(13, 67);
            this.tvBranches.Name = "tvBranches";
            this.tvBranches.Size = new System.Drawing.Size(399, 328);
            this.tvBranches.TabIndex = 7;
            this.tvBranches.TriStateStyleProperty = SSCRM.TriStateTreeView.TriStateStyles.Standard;
            this.tvBranches.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvBranches_AfterCheck);
            // 
            // RepDateCompBranchSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(444, 549);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RepDateCompBranchSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Date Comp Branch Selection";
            this.Load += new System.EventHandler(this.RepDateCompBranchSelection_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private TriStateTreeView tvBranches;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.DateTimePicker dtpFrmDocMonth;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.DateTimePicker dtpToDocMonth;
        private System.Windows.Forms.Label lblToMonth;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.CheckedListBox chkTrnType;
        private System.Windows.Forms.Label lblTrnType;
    }
}