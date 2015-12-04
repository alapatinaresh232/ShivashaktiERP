namespace SSCRM
{
    partial class BranchAttendence
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbReport = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.clbDept = new System.Windows.Forms.CheckedListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.clbBrType = new System.Windows.Forms.CheckedListBox();
            this.chkCompAll = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tvCompBranches = new System.Windows.Forms.TreeView();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbReport);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.lblToDate);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.lblFromDate);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(1, 3);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(568, 510);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label1.Location = new System.Drawing.Point(3, 450);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 183;
            this.label1.Text = "Report Type";
            // 
            // cbReport
            // 
            this.cbReport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbReport.FormattingEnabled = true;
            this.cbReport.Items.AddRange(new object[] {
            "MONTHWISE",
            "DAYWISE"});
            this.cbReport.Location = new System.Drawing.Point(101, 449);
            this.cbReport.Name = "cbReport";
            this.cbReport.Size = new System.Drawing.Size(154, 23);
            this.cbReport.TabIndex = 182;
            this.cbReport.SelectedIndexChanged += new System.EventHandler(this.cbReport_SelectedIndexChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.ForeColor = System.Drawing.Color.Green;
            this.checkBox1.Location = new System.Drawing.Point(145, 235);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(45, 20);
            this.checkBox1.TabIndex = 181;
            this.checkBox1.Text = "All";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.clbDept);
            this.groupBox4.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox4.Location = new System.Drawing.Point(11, 240);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(197, 189);
            this.groupBox4.TabIndex = 181;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Department";
            // 
            // clbDept
            // 
            this.clbDept.CheckOnClick = true;
            this.clbDept.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbDept.FormattingEnabled = true;
            this.clbDept.Location = new System.Drawing.Point(8, 19);
            this.clbDept.Name = "clbDept";
            this.clbDept.Size = new System.Drawing.Size(185, 164);
            this.clbDept.TabIndex = 5;
            this.clbDept.SelectedIndexChanged += new System.EventHandler(this.clbDept_SelectedIndexChanged);
            this.clbDept.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbDept_ItemCheck);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.clbBrType);
            this.groupBox3.Controls.Add(this.chkCompAll);
            this.groupBox3.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox3.Location = new System.Drawing.Point(11, 60);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(197, 173);
            this.groupBox3.TabIndex = 179;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "BranchType";
            // 
            // clbBrType
            // 
            this.clbBrType.CheckOnClick = true;
            this.clbBrType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbBrType.FormattingEnabled = true;
            this.clbBrType.Location = new System.Drawing.Point(6, 21);
            this.clbBrType.Name = "clbBrType";
            this.clbBrType.Size = new System.Drawing.Size(185, 148);
            this.clbBrType.TabIndex = 4;
            this.clbBrType.SelectedIndexChanged += new System.EventHandler(this.lbBranchType_SelectedIndexChanged);
            // 
            // chkCompAll
            // 
            this.chkCompAll.AutoSize = true;
            this.chkCompAll.ForeColor = System.Drawing.Color.Green;
            this.chkCompAll.Location = new System.Drawing.Point(134, -2);
            this.chkCompAll.Name = "chkCompAll";
            this.chkCompAll.Size = new System.Drawing.Size(45, 20);
            this.chkCompAll.TabIndex = 3;
            this.chkCompAll.Text = "All";
            this.chkCompAll.UseVisualStyleBackColor = true;
            this.chkCompAll.CheckedChanged += new System.EventHandler(this.chkCompAll_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox5.Controls.Add(this.tvCompBranches);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox5.Location = new System.Drawing.Point(211, 60);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(348, 366);
            this.groupBox5.TabIndex = 122;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Comp/Branchs";
            // 
            // tvCompBranches
            // 
            this.tvCompBranches.CheckBoxes = true;
            this.tvCompBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvCompBranches.Location = new System.Drawing.Point(6, 21);
            this.tvCompBranches.Name = "tvCompBranches";
            this.tvCompBranches.Size = new System.Drawing.Size(336, 339);
            this.tvCompBranches.TabIndex = 32;
            this.tvCompBranches.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvCompBranches_AfterCheck);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button1);
            this.groupBox6.Controls.Add(this.button2);
            this.groupBox6.Location = new System.Drawing.Point(161, 518);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(236, 39);
            this.groupBox6.TabIndex = 31;
            this.groupBox6.TabStop = false;
            // 
            // button1
            // 
            this.button1.AutoEllipsis = true;
            this.button1.BackColor = System.Drawing.Color.LightCyan;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(113, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 25);
            this.button1.TabIndex = 6;
            this.button1.Text = "&Cancel";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.AutoEllipsis = true;
            this.button2.BackColor = System.Drawing.Color.LightCyan;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.button2.FlatAppearance.BorderSize = 5;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(47, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(59, 25);
            this.button2.TabIndex = 5;
            this.button2.Text = "&OK";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(346, 23);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(97, 22);
            this.dtpToDate.TabIndex = 43;
            this.dtpToDate.Value = new System.DateTime(2015, 6, 29, 0, 0, 0, 0);
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblToDate.Location = new System.Drawing.Point(313, 25);
            this.lblToDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(27, 17);
            this.lblToDate.TabIndex = 45;
            this.lblToDate.Text = "To";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(199, 21);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(97, 22);
            this.dtpFromDate.TabIndex = 42;
            this.dtpFromDate.Value = new System.DateTime(2015, 6, 29, 0, 0, 0, 0);
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblFromDate.Location = new System.Drawing.Point(146, 24);
            this.lblFromDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(44, 17);
            this.lblFromDate.TabIndex = 43;
            this.lblFromDate.Text = "From";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnReport);
            this.groupBox2.Location = new System.Drawing.Point(258, 433);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(302, 47);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.MidnightBlue;
            this.button3.Location = new System.Drawing.Point(104, 14);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 26);
            this.button3.TabIndex = 7;
            this.button3.Text = "&Download";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(207, 15);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 26);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "C&lose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReport
            // 
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnReport.Location = new System.Drawing.Point(15, 15);
            this.btnReport.Margin = new System.Windows.Forms.Padding(4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(74, 26);
            this.btnReport.TabIndex = 5;
            this.btnReport.Text = "&Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // BranchAttendence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(572, 516);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "BranchAttendence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.StationaryCategory_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private TriStateTreeView tvItems;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReport;
        private TriStateTreeView tvBranches;
        private System.Windows.Forms.TreeView tvCompBranches;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox chkCompAll;
        private System.Windows.Forms.CheckedListBox clbDept;
        private System.Windows.Forms.CheckedListBox clbBrType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbReport;
    }
}