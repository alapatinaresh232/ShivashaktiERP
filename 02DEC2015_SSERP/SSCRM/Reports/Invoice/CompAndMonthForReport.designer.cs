namespace SSCRM
{
    partial class CompAndMonthForReport
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
            this.btnDownload = new System.Windows.Forms.Button();
            this.lblAvg = new System.Windows.Forms.Label();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.cbReport = new System.Windows.Forms.ComboBox();
            this.clbReport = new System.Windows.Forms.CheckedListBox();
            this.tvDocumentMonth = new System.Windows.Forms.TreeView();
            this.tvBranches = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.btnDownload);
            this.groupBox1.Controls.Add(this.lblAvg);
            this.groupBox1.Controls.Add(this.txtTo);
            this.groupBox1.Controls.Add(this.lblTo);
            this.groupBox1.Controls.Add(this.txtFrom);
            this.groupBox1.Controls.Add(this.lblFrom);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.cbReport);
            this.groupBox1.Controls.Add(this.clbReport);
            this.groupBox1.Controls.Add(this.tvDocumentMonth);
            this.groupBox1.Controls.Add(this.tvBranches);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(2, -4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(482, 373);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
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
            this.btnDownload.Location = new System.Drawing.Point(69, 337);
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
            // lblAvg
            // 
            this.lblAvg.AutoSize = true;
            this.lblAvg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblAvg.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblAvg.Location = new System.Drawing.Point(130, 47);
            this.lblAvg.Name = "lblAvg";
            this.lblAvg.Size = new System.Drawing.Size(121, 16);
            this.lblAvg.TabIndex = 71;
            this.lblAvg.Text = "Average Groups";
            // 
            // txtTo
            // 
            this.txtTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTo.Location = new System.Drawing.Point(407, 46);
            this.txtTo.MaxLength = 9;
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(63, 21);
            this.txtTo.TabIndex = 69;
            this.txtTo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTo_KeyUp);
            this.txtTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTo_KeyPress);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblTo.Location = new System.Drawing.Point(381, 48);
            this.lblTo.Margin = new System.Windows.Forms.Padding(0);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(27, 16);
            this.lblTo.TabIndex = 70;
            this.lblTo.Text = "To";
            // 
            // txtFrom
            // 
            this.txtFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrom.Location = new System.Drawing.Point(288, 45);
            this.txtFrom.MaxLength = 9;
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(63, 21);
            this.txtFrom.TabIndex = 67;
            this.txtFrom.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFrom_KeyUp);
            this.txtFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFrom_KeyPress);
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblFrom.Location = new System.Drawing.Point(247, 47);
            this.lblFrom.Margin = new System.Windows.Forms.Padding(0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(43, 16);
            this.lblFrom.TabIndex = 68;
            this.lblFrom.Text = "From";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label27.Location = new System.Drawing.Point(4, 22);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(50, 14);
            this.label27.TabIndex = 66;
            this.label27.Text = "Report";
            // 
            // cbReport
            // 
            this.cbReport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbReport.FormattingEnabled = true;
            this.cbReport.Items.AddRange(new object[] {
            "--SELECT--",
            "MALE",
            "FEMALE"});
            this.cbReport.Location = new System.Drawing.Point(58, 18);
            this.cbReport.Name = "cbReport";
            this.cbReport.Size = new System.Drawing.Size(412, 23);
            this.cbReport.TabIndex = 63;
            this.cbReport.SelectedIndexChanged += new System.EventHandler(this.cbReport_SelectedIndexChanged);
            // 
            // clbReport
            // 
            this.clbReport.CheckOnClick = true;
            this.clbReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbReport.FormattingEnabled = true;
            this.clbReport.Location = new System.Drawing.Point(321, 262);
            this.clbReport.Name = "clbReport";
            this.clbReport.Size = new System.Drawing.Size(157, 64);
            this.clbReport.Sorted = true;
            this.clbReport.TabIndex = 44;
            this.clbReport.UseCompatibleTextRendering = true;
            this.clbReport.SelectedIndexChanged += new System.EventHandler(this.clbReport_SelectedIndexChanged);
            this.clbReport.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbReport_ItemCheck);
            // 
            // tvDocumentMonth
            // 
            this.tvDocumentMonth.CheckBoxes = true;
            this.tvDocumentMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvDocumentMonth.Location = new System.Drawing.Point(321, 70);
            this.tvDocumentMonth.Name = "tvDocumentMonth";
            this.tvDocumentMonth.Size = new System.Drawing.Size(157, 192);
            this.tvDocumentMonth.TabIndex = 43;
            this.tvDocumentMonth.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvDocumentMonth_AfterCheck);
            // 
            // tvBranches
            // 
            this.tvBranches.CheckBoxes = true;
            this.tvBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvBranches.Location = new System.Drawing.Point(6, 70);
            this.tvBranches.Name = "tvBranches";
            this.tvBranches.Size = new System.Drawing.Size(315, 256);
            this.tvBranches.TabIndex = 4;
            this.tvBranches.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvBranches_AfterCheck);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.btnOk);
            this.groupBox2.Location = new System.Drawing.Point(238, 559);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(228, 40);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoEllipsis = true;
            this.btnCancel.BackColor = System.Drawing.Color.LightCyan;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(120, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 25);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnOk
            // 
            this.btnOk.AutoEllipsis = true;
            this.btnOk.BackColor = System.Drawing.Color.LightCyan;
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.btnOk.FlatAppearance.BorderSize = 5;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Location = new System.Drawing.Point(37, 11);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(81, 25);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = false;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnReport);
            this.groupBox3.Location = new System.Drawing.Point(58, 325);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(367, 42);
            this.groupBox3.TabIndex = 42;
            this.groupBox3.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.AliceBlue;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.button1.Location = new System.Drawing.Point(202, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 9;
            this.button1.Text = "&Cancel";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(283, 10);
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
            this.btnReport.Location = new System.Drawing.Point(126, 10);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 30);
            this.btnReport.TabIndex = 8;
            this.btnReport.Text = "&Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // CompAndMonthForReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 371);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "CompAndMonthForReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report";
            this.Load += new System.EventHandler(this.CompAndMonthForReport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView tvBranches;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.TreeView tvDocumentMonth;
        private System.Windows.Forms.CheckedListBox clbReport;
        private System.Windows.Forms.ComboBox cbReport;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblAvg;
        private System.Windows.Forms.Button btnDownload;
    }
}