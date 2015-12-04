namespace SSCRM
{
    partial class AuditHierarchyMapping
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
            this.dtpDocMonth = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDesig = new System.Windows.Forms.ComboBox();
            this.txtMsearch = new System.Windows.Forms.TextBox();
            this.txtDsearch = new System.Windows.Forms.TextBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblMap = new System.Windows.Forms.Label();
            this.lbMapp = new System.Windows.Forms.ListBox();
            this.chkMapp = new System.Windows.Forms.CheckBox();
            this.clbDestination = new System.Windows.Forms.CheckedListBox();
            this.clbSource = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.dtpDocMonth);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cbDesig);
            this.groupBox3.Controls.Add(this.txtMsearch);
            this.groupBox3.Controls.Add(this.txtDsearch);
            this.groupBox3.Controls.Add(this.txtSearch);
            this.groupBox3.Controls.Add(this.lblMap);
            this.groupBox3.Controls.Add(this.lbMapp);
            this.groupBox3.Controls.Add(this.chkMapp);
            this.groupBox3.Controls.Add(this.clbDestination);
            this.groupBox3.Controls.Add(this.clbSource);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(945, 477);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // dtpDocMonth
            // 
            this.dtpDocMonth.CustomFormat = "MMM/yyyy";
            this.dtpDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDocMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDocMonth.Location = new System.Drawing.Point(154, 18);
            this.dtpDocMonth.Name = "dtpDocMonth";
            this.dtpDocMonth.ShowUpDown = true;
            this.dtpDocMonth.Size = new System.Drawing.Size(101, 22);
            this.dtpDocMonth.TabIndex = 1;
            this.dtpDocMonth.ValueChanged += new System.EventHandler(this.dtpDocMonth_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(64, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Visit Month";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(395, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Designation";
            // 
            // cbDesig
            // 
            this.cbDesig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDesig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDesig.FormattingEnabled = true;
            this.cbDesig.Location = new System.Drawing.Point(490, 20);
            this.cbDesig.Name = "cbDesig";
            this.cbDesig.Size = new System.Drawing.Size(216, 23);
            this.cbDesig.TabIndex = 3;
            this.cbDesig.SelectedIndexChanged += new System.EventHandler(this.cbDesig_SelectedIndexChanged);
            // 
            // txtMsearch
            // 
            this.txtMsearch.Location = new System.Drawing.Point(858, 31);
            this.txtMsearch.Name = "txtMsearch";
            this.txtMsearch.Size = new System.Drawing.Size(77, 20);
            this.txtMsearch.TabIndex = 14;
            this.txtMsearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMsearch_KeyUp);
            // 
            // txtDsearch
            // 
            this.txtDsearch.Location = new System.Drawing.Point(154, 55);
            this.txtDsearch.Name = "txtDsearch";
            this.txtDsearch.Size = new System.Drawing.Size(189, 20);
            this.txtDsearch.TabIndex = 5;
            this.txtDsearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDsearch_KeyUp);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(490, 55);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(216, 20);
            this.txtSearch.TabIndex = 8;
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            // 
            // lblMap
            // 
            this.lblMap.AutoSize = true;
            this.lblMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMap.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblMap.Location = new System.Drawing.Point(729, 33);
            this.lblMap.Margin = new System.Windows.Forms.Padding(0);
            this.lblMap.Name = "lblMap";
            this.lblMap.Size = new System.Drawing.Size(130, 15);
            this.lblMap.TabIndex = 12;
            this.lblMap.Text = "Unmapped  Source";
            // 
            // lbMapp
            // 
            this.lbMapp.FormattingEnabled = true;
            this.lbMapp.Location = new System.Drawing.Point(711, 56);
            this.lbMapp.Name = "lbMapp";
            this.lbMapp.Size = new System.Drawing.Size(228, 407);
            this.lbMapp.TabIndex = 13;
            // 
            // chkMapp
            // 
            this.chkMapp.AutoSize = true;
            this.chkMapp.BackColor = System.Drawing.Color.PowderBlue;
            this.chkMapp.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMapp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMapp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chkMapp.Location = new System.Drawing.Point(734, 12);
            this.chkMapp.Name = "chkMapp";
            this.chkMapp.Size = new System.Drawing.Size(84, 21);
            this.chkMapp.TabIndex = 11;
            this.chkMapp.Text = "Mapped";
            this.chkMapp.UseVisualStyleBackColor = false;
            this.chkMapp.CheckStateChanged += new System.EventHandler(this.chkMapp_CheckStateChanged);
            // 
            // clbDestination
            // 
            this.clbDestination.CheckOnClick = true;
            this.clbDestination.FormattingEnabled = true;
            this.clbDestination.Location = new System.Drawing.Point(6, 77);
            this.clbDestination.Name = "clbDestination";
            this.clbDestination.Size = new System.Drawing.Size(337, 334);
            this.clbDestination.Sorted = true;
            this.clbDestination.TabIndex = 6;
            this.clbDestination.UseCompatibleTextRendering = true;
            this.clbDestination.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbDestination_ItemCheck);
            // 
            // clbSource
            // 
            this.clbSource.CheckOnClick = true;
            this.clbSource.FormattingEnabled = true;
            this.clbSource.Location = new System.Drawing.Point(355, 77);
            this.clbSource.Name = "clbSource";
            this.clbSource.Size = new System.Drawing.Size(351, 334);
            this.clbSource.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Location = new System.Drawing.Point(40, 419);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(634, 45);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.Location = new System.Drawing.Point(283, 11);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(206, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
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
            this.btnClose.Location = new System.Drawing.Point(381, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(130, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(57, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Reporting To";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(433, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Source";
            // 
            // AuditHierarchyMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(951, 483);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AuditHierarchyMapping";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Audit Hierarchy Mapping(HO)";
            this.Load += new System.EventHandler(this.AuditHierarchyMapping_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtMsearch;
        private System.Windows.Forms.TextBox txtDsearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblMap;
        private System.Windows.Forms.ListBox lbMapp;
        private System.Windows.Forms.CheckBox chkMapp;
        private System.Windows.Forms.CheckedListBox clbDestination;
        private System.Windows.Forms.CheckedListBox clbSource;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDesig;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDocMonth;
    }
}