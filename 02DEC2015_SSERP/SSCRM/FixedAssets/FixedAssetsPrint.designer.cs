namespace SSCRM
{
    partial class FixedAssetsPrint
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.clbAssets = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbRepType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDsearch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkAllAssets = new System.Windows.Forms.CheckBox();
            this.tvBranches = new SSCRM.TriStateTreeView();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.chkAllAssets);
            this.groupBox1.Controls.Add(this.txtDsearch);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tvBranches);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.clbAssets);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbRepType);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(613, 427);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnReport);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(117, 377);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 45);
            this.groupBox4.TabIndex = 115;
            this.groupBox4.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(227, 13);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(152, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Clea&r";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReport
            // 
            this.btnReport.AutoEllipsis = true;
            this.btnReport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReport.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnReport.Location = new System.Drawing.Point(77, 13);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(74, 26);
            this.btnReport.TabIndex = 0;
            this.btnReport.Text = "&Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // clbAssets
            // 
            this.clbAssets.CheckOnClick = true;
            this.clbAssets.FormattingEnabled = true;
            this.clbAssets.Location = new System.Drawing.Point(360, 58);
            this.clbAssets.Name = "clbAssets";
            this.clbAssets.Size = new System.Drawing.Size(239, 319);
            this.clbAssets.TabIndex = 114;
            this.clbAssets.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbAssets_ItemCheck);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(9, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 15);
            this.label1.TabIndex = 111;
            this.label1.Text = "Company/Branches";
            // 
            // cbRepType
            // 
            this.cbRepType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRepType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRepType.FormattingEnabled = true;
            this.cbRepType.Items.AddRange(new object[] {
            "--Select--",
            "DETAILED",
            "SUMMARY"});
            this.cbRepType.Location = new System.Drawing.Point(105, 11);
            this.cbRepType.Name = "cbRepType";
            this.cbRepType.Size = new System.Drawing.Size(207, 24);
            this.cbRepType.TabIndex = 109;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(9, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 16);
            this.label6.TabIndex = 110;
            this.label6.Text = "Report Type";
            // 
            // txtDsearch
            // 
            this.txtDsearch.Location = new System.Drawing.Point(508, 35);
            this.txtDsearch.Name = "txtDsearch";
            this.txtDsearch.Size = new System.Drawing.Size(90, 20);
            this.txtDsearch.TabIndex = 188;
            this.txtDsearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDsearch_KeyUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(416, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 15);
            this.label5.TabIndex = 187;
            this.label5.Text = "Asset Search";
            // 
            // chkAllAssets
            // 
            this.chkAllAssets.AutoSize = true;
            this.chkAllAssets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllAssets.Location = new System.Drawing.Point(360, 38);
            this.chkAllAssets.Name = "chkAllAssets";
            this.chkAllAssets.Size = new System.Drawing.Size(48, 17);
            this.chkAllAssets.TabIndex = 189;
            this.chkAllAssets.Text = "ALL";
            this.chkAllAssets.UseVisualStyleBackColor = true;
            this.chkAllAssets.CheckedChanged += new System.EventHandler(this.chkAllAssets_CheckedChanged);
            // 
            // tvBranches
            // 
            this.tvBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvBranches.Location = new System.Drawing.Point(8, 57);
            this.tvBranches.Name = "tvBranches";
            this.tvBranches.Size = new System.Drawing.Size(344, 320);
            this.tvBranches.TabIndex = 116;
            this.tvBranches.TriStateStyleProperty = SSCRM.TriStateTreeView.TriStateStyles.Standard;
            this.tvBranches.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvBranches_AfterCheck);
            this.tvBranches.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvBranches_NodeMouseClick);
            // 
            // FixedAssetsPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(620, 434);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FixedAssetsPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fixed Assets ";
            this.Load += new System.EventHandler(this.FixedAssetsPrint_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbRepType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox clbAssets;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReport;
        private TriStateTreeView tvBranches;
        private System.Windows.Forms.TextBox txtDsearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkAllAssets;
    }
}