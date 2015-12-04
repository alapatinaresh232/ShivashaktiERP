namespace SSCRM
{
    partial class CamptoSP
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
            this.label3 = new System.Windows.Forms.Label();
            this.chkStockPoints = new System.Windows.Forms.CheckedListBox();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnMap = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.clbCampLst = new System.Windows.Forms.CheckedListBox();
            this.dtDocMon = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lblREcode = new System.Windows.Forms.Label();
            this.cmbBranch = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chkStockPoints);
            this.groupBox1.Controls.Add(this.btnDisplay);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.clbCampLst);
            this.groupBox1.Controls.Add(this.dtDocMon);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblREcode);
            this.groupBox1.Controls.Add(this.cmbBranch);
            this.groupBox1.Location = new System.Drawing.Point(-2, -10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(635, 522);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(288, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 17);
            this.label3.TabIndex = 85;
            this.label3.Text = "Stock Points ";
            // 
            // chkStockPoints
            // 
            this.chkStockPoints.CheckOnClick = true;
            this.chkStockPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkStockPoints.FormattingEnabled = true;
            this.chkStockPoints.Location = new System.Drawing.Point(292, 77);
            this.chkStockPoints.Name = "chkStockPoints";
            this.chkStockPoints.Size = new System.Drawing.Size(336, 388);
            this.chkStockPoints.Sorted = true;
            this.chkStockPoints.TabIndex = 84;
            this.chkStockPoints.UseCompatibleTextRendering = true;
            this.chkStockPoints.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkStockPoints_ItemCheck);
            // 
            // btnDisplay
            // 
            this.btnDisplay.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplay.Location = new System.Drawing.Point(553, 21);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(78, 25);
            this.btnDisplay.TabIndex = 2;
            this.btnDisplay.Text = "Display";
            this.btnDisplay.UseVisualStyleBackColor = false;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnMap);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnReport);
            this.groupBox3.Location = new System.Drawing.Point(142, 466);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 47);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            // 
            // btnMap
            // 
            this.btnMap.BackColor = System.Drawing.Color.AliceBlue;
            this.btnMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMap.Location = new System.Drawing.Point(46, 11);
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(90, 30);
            this.btnMap.TabIndex = 2;
            this.btnMap.Text = "View Map";
            this.btnMap.UseVisualStyleBackColor = false;
            this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(226, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Location = new System.Drawing.Point(142, 11);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(78, 30);
            this.btnReport.TabIndex = 0;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 17);
            this.label2.TabIndex = 83;
            this.label2.Text = "Select Camps";
            // 
            // clbCampLst
            // 
            this.clbCampLst.CheckOnClick = true;
            this.clbCampLst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbCampLst.FormattingEnabled = true;
            this.clbCampLst.Location = new System.Drawing.Point(6, 76);
            this.clbCampLst.Name = "clbCampLst";
            this.clbCampLst.Size = new System.Drawing.Size(283, 388);
            this.clbCampLst.Sorted = true;
            this.clbCampLst.TabIndex = 82;
            this.clbCampLst.UseCompatibleTextRendering = true;
            this.clbCampLst.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbCampLst_ItemCheck);
            // 
            // dtDocMon
            // 
            this.dtDocMon.CustomFormat = "MMM/yyyy";
            this.dtDocMon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtDocMon.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDocMon.Location = new System.Drawing.Point(447, 22);
            this.dtDocMon.Name = "dtDocMon";
            this.dtDocMon.Size = new System.Drawing.Size(103, 22);
            this.dtDocMon.TabIndex = 81;
            this.dtDocMon.Value = new System.DateTime(2013, 8, 22, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(397, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 80;
            this.label1.Text = "Month";
            // 
            // lblREcode
            // 
            this.lblREcode.AutoSize = true;
            this.lblREcode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblREcode.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblREcode.Location = new System.Drawing.Point(6, 22);
            this.lblREcode.Name = "lblREcode";
            this.lblREcode.Size = new System.Drawing.Size(50, 17);
            this.lblREcode.TabIndex = 79;
            this.lblREcode.Text = "Branch";
            // 
            // cmbBranch
            // 
            this.cmbBranch.BackColor = System.Drawing.SystemColors.Info;
            this.cmbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBranch.ForeColor = System.Drawing.SystemColors.MenuText;
            this.cmbBranch.FormattingEnabled = true;
            this.cmbBranch.Location = new System.Drawing.Point(56, 19);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Size = new System.Drawing.Size(341, 24);
            this.cmbBranch.TabIndex = 2;
            // 
            // CamptoSP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 511);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "CamptoSP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Camp to SP";
            this.Load += new System.EventHandler(this.CamptoSP_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbBranch;
        private System.Windows.Forms.Label lblREcode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtDocMon;
        private System.Windows.Forms.CheckedListBox clbCampLst;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.CheckedListBox chkStockPoints;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnMap;
    }
}