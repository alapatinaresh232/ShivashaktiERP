namespace SSCRM
{
    partial class ZonalHeadMaster
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbFinYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gvBranchHeadDetails = new System.Windows.Forms.DataGridView();
            this.SlNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FinYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocMonth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchHead = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DivHead = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegHead = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZonalHead = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.lblCompany = new System.Windows.Forms.Label();
            this.lblBranch = new System.Windows.Forms.Label();
            this.cbUserBranch = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvBranchHeadDetails)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbFinYear);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.gvBranchHeadDetails);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.lblCompany);
            this.groupBox1.Controls.Add(this.lblBranch);
            this.groupBox1.Controls.Add(this.cbUserBranch);
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(942, 509);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(768, 18);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 16);
            this.label8.TabIndex = 132;
            this.label8.Text = "Fin Year";
            // 
            // cbFinYear
            // 
            this.cbFinYear.AllowDrop = true;
            this.cbFinYear.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbFinYear.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFinYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFinYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFinYear.FormattingEnabled = true;
            this.cbFinYear.Location = new System.Drawing.Point(837, 15);
            this.cbFinYear.Name = "cbFinYear";
            this.cbFinYear.Size = new System.Drawing.Size(92, 23);
            this.cbFinYear.TabIndex = 133;
            this.cbFinYear.SelectedIndexChanged += new System.EventHandler(this.cbFinYear_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(14, 486);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(386, 17);
            this.label1.TabIndex = 131;
            this.label1.Text = "* Once  Approve  Data Will not be modified for ever.";
            // 
            // gvBranchHeadDetails
            // 
            this.gvBranchHeadDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Navy;
            this.gvBranchHeadDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvBranchHeadDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvBranchHeadDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvBranchHeadDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvBranchHeadDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvBranchHeadDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SlNo,
            this.FinYear,
            this.DocMonth,
            this.BranchHead,
            this.DivHead,
            this.RegHead,
            this.ZonalHead,
            this.Status});
            this.gvBranchHeadDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvBranchHeadDetails.Location = new System.Drawing.Point(9, 57);
            this.gvBranchHeadDetails.MultiSelect = false;
            this.gvBranchHeadDetails.Name = "gvBranchHeadDetails";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvBranchHeadDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvBranchHeadDetails.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.gvBranchHeadDetails.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvBranchHeadDetails.Size = new System.Drawing.Size(924, 373);
            this.gvBranchHeadDetails.TabIndex = 130;
            this.gvBranchHeadDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvBranchHeadDetails_CellEndEdit);
            this.gvBranchHeadDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvBranchHeadDetails_EditingControlShowing);
            // 
            // SlNo
            // 
            this.SlNo.Frozen = true;
            this.SlNo.HeaderText = "Sl.No";
            this.SlNo.Name = "SlNo";
            this.SlNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SlNo.Width = 50;
            // 
            // FinYear
            // 
            this.FinYear.HeaderText = "Fin Year";
            this.FinYear.Name = "FinYear";
            this.FinYear.ReadOnly = true;
            this.FinYear.Width = 90;
            // 
            // DocMonth
            // 
            this.DocMonth.HeaderText = "Doc Month";
            this.DocMonth.Name = "DocMonth";
            this.DocMonth.ReadOnly = true;
            this.DocMonth.Width = 70;
            // 
            // BranchHead
            // 
            this.BranchHead.HeaderText = "Branch Head";
            this.BranchHead.Name = "BranchHead";
            this.BranchHead.Width = 150;
            // 
            // DivHead
            // 
            this.DivHead.HeaderText = "Div Head";
            this.DivHead.Name = "DivHead";
            this.DivHead.Width = 150;
            // 
            // RegHead
            // 
            this.RegHead.HeaderText = "Reg Head";
            this.RegHead.Name = "RegHead";
            this.RegHead.Width = 150;
            // 
            // ZonalHead
            // 
            this.ZonalHead.HeaderText = "Zonal Head";
            this.ZonalHead.Name = "ZonalHead";
            this.ZonalHead.Width = 150;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 80;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnApprove);
            this.groupBox5.Controls.Add(this.btnClose);
            this.groupBox5.Controls.Add(this.btnCancel);
            this.groupBox5.Controls.Add(this.btnSave);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox5.Location = new System.Drawing.Point(258, 432);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(426, 45);
            this.groupBox5.TabIndex = 124;
            this.groupBox5.TabStop = false;
            // 
            // btnApprove
            // 
            this.btnApprove.BackColor = System.Drawing.Color.AliceBlue;
            this.btnApprove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnApprove.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApprove.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnApprove.Location = new System.Drawing.Point(132, 13);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(78, 26);
            this.btnApprove.TabIndex = 4;
            this.btnApprove.Text = "Appro&ve";
            this.btnApprove.UseVisualStyleBackColor = false;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(304, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 26);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "C&lose";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(220, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Clea&r";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoEllipsis = true;
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(48, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbCompany
            // 
            this.cbCompany.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Location = new System.Drawing.Point(80, 13);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(275, 24);
            this.cbCompany.TabIndex = 32;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblCompany.Location = new System.Drawing.Point(5, 17);
            this.lblCompany.Margin = new System.Windows.Forms.Padding(0);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(73, 16);
            this.lblCompany.TabIndex = 33;
            this.lblCompany.Text = "Company";
            // 
            // lblBranch
            // 
            this.lblBranch.AutoSize = true;
            this.lblBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBranch.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblBranch.Location = new System.Drawing.Point(357, 18);
            this.lblBranch.Margin = new System.Windows.Forms.Padding(0);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Size = new System.Drawing.Size(60, 16);
            this.lblBranch.TabIndex = 34;
            this.lblBranch.Text = " Branch";
            // 
            // cbUserBranch
            // 
            this.cbUserBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUserBranch.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbUserBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUserBranch.FormattingEnabled = true;
            this.cbUserBranch.Location = new System.Drawing.Point(417, 14);
            this.cbUserBranch.Name = "cbUserBranch";
            this.cbUserBranch.Size = new System.Drawing.Size(347, 24);
            this.cbUserBranch.TabIndex = 31;
            this.cbUserBranch.SelectedIndexChanged += new System.EventHandler(this.cbUserBranch_SelectedIndexChanged);
            // 
            // ZonalHeadMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(947, 514);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZonalHeadMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zonal Head Master";
            this.Load += new System.EventHandler(this.ZonalHeadMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvBranchHeadDetails)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.ComboBox cbUserBranch;
        private System.Windows.Forms.Button btnApprove;
        public System.Windows.Forms.DataGridView gvBranchHeadDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbFinYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FinYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocMonth;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchHead;
        private System.Windows.Forms.DataGridViewTextBoxColumn DivHead;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegHead;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZonalHead;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}