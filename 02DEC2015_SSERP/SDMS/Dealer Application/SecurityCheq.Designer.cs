namespace SDMS
{
    partial class SecurityCheq
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecurityCheq));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grouper1 = new GroupCtrl.Grouper();
            this.cbBranchName = new System.Windows.Forms.TextBox();
            this.cbBankName = new System.Windows.Forms.ComboBox();
            this.grouper7 = new GroupCtrl.Grouper();
            this.label79 = new System.Windows.Forms.Label();
            this.gvSecurityCheq = new System.Windows.Forms.DataGridView();
            this.SLNO_SecCheq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChequeNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BankName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Branch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit_SecCheq = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete_SecCheq = new System.Windows.Forms.DataGridViewImageColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCheqNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grouper1.SuspendLayout();
            this.grouper7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSecurityCheq)).BeginInit();
            this.SuspendLayout();
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.PowderBlue;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouper1.BackgroundGradientMode = GroupCtrl.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.White;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.cbBranchName);
            this.grouper1.Controls.Add(this.cbBankName);
            this.grouper1.Controls.Add(this.grouper7);
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.Controls.Add(this.label5);
            this.grouper1.Controls.Add(this.txtCheqNo);
            this.grouper1.Controls.Add(this.label2);
            this.grouper1.Controls.Add(this.btnSave);
            this.grouper1.Controls.Add(this.btnClose);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "";
            this.grouper1.Location = new System.Drawing.Point(6, -8);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 3;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(764, 270);
            this.grouper1.TabIndex = 177;
            // 
            // cbBranchName
            // 
            this.cbBranchName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranchName.Location = new System.Drawing.Point(421, 48);
            this.cbBranchName.MaxLength = 50;
            this.cbBranchName.Name = "cbBranchName";
            this.cbBranchName.Size = new System.Drawing.Size(167, 21);
            this.cbBranchName.TabIndex = 201;
            this.cbBranchName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbBranchName_KeyPress);
            // 
            // cbBankName
            // 
            this.cbBankName.FormattingEnabled = true;
            this.cbBankName.Location = new System.Drawing.Point(107, 48);
            this.cbBankName.Name = "cbBankName";
            this.cbBankName.Size = new System.Drawing.Size(214, 21);
            this.cbBankName.TabIndex = 200;
            // 
            // grouper7
            // 
            this.grouper7.BackgroundColor = System.Drawing.Color.White;
            this.grouper7.BackgroundGradientColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.grouper7.BackgroundGradientMode = GroupCtrl.Grouper.GroupBoxGradientMode.Vertical;
            this.grouper7.BorderColor = System.Drawing.Color.Black;
            this.grouper7.BorderThickness = 1F;
            this.grouper7.Controls.Add(this.label79);
            this.grouper7.Controls.Add(this.gvSecurityCheq);
            this.grouper7.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.980198F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grouper7.GroupImage = null;
            this.grouper7.GroupTitle = "";
            this.grouper7.Location = new System.Drawing.Point(6, 76);
            this.grouper7.Name = "grouper7";
            this.grouper7.Padding = new System.Windows.Forms.Padding(20);
            this.grouper7.PaintGroupBox = false;
            this.grouper7.RoundCorners = 3;
            this.grouper7.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper7.ShadowControl = false;
            this.grouper7.ShadowThickness = 3;
            this.grouper7.Size = new System.Drawing.Size(756, 176);
            this.grouper7.TabIndex = 199;
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label79.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label79.Location = new System.Drawing.Point(5, 13);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(164, 17);
            this.label79.TabIndex = 52;
            this.label79.Text = "Security Cheq Details";
            // 
            // gvSecurityCheq
            // 
            this.gvSecurityCheq.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvSecurityCheq.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvSecurityCheq.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvSecurityCheq.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.980198F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSecurityCheq.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvSecurityCheq.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSecurityCheq.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO_SecCheq,
            this.ChequeNo,
            this.BankName,
            this.Branch,
            this.Edit_SecCheq,
            this.Delete_SecCheq});
            this.gvSecurityCheq.GridColor = System.Drawing.Color.AliceBlue;
            this.gvSecurityCheq.Location = new System.Drawing.Point(4, 30);
            this.gvSecurityCheq.MultiSelect = false;
            this.gvSecurityCheq.Name = "gvSecurityCheq";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.980198F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSecurityCheq.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvSecurityCheq.RowHeadersVisible = false;
            this.gvSecurityCheq.Size = new System.Drawing.Size(746, 139);
            this.gvSecurityCheq.TabIndex = 51;
            this.gvSecurityCheq.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSecurityCheq_CellClick);
            // 
            // SLNO_SecCheq
            // 
            this.SLNO_SecCheq.Frozen = true;
            this.SLNO_SecCheq.HeaderText = "Sl.No";
            this.SLNO_SecCheq.MinimumWidth = 50;
            this.SLNO_SecCheq.Name = "SLNO_SecCheq";
            this.SLNO_SecCheq.ReadOnly = true;
            this.SLNO_SecCheq.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SLNO_SecCheq.Width = 50;
            // 
            // ChequeNo
            // 
            this.ChequeNo.HeaderText = "Cheque No";
            this.ChequeNo.MinimumWidth = 155;
            this.ChequeNo.Name = "ChequeNo";
            this.ChequeNo.ReadOnly = true;
            this.ChequeNo.Width = 155;
            // 
            // BankName
            // 
            this.BankName.HeaderText = "Bank Name";
            this.BankName.Name = "BankName";
            this.BankName.Width = 240;
            // 
            // Branch
            // 
            this.Branch.HeaderText = "Branch";
            this.Branch.MinimumWidth = 115;
            this.Branch.Name = "Branch";
            this.Branch.ReadOnly = true;
            this.Branch.Width = 200;
            // 
            // Edit_SecCheq
            // 
            this.Edit_SecCheq.HeaderText = "Edit";
            this.Edit_SecCheq.Image = ((System.Drawing.Image)(resources.GetObject("Edit_SecCheq.Image")));
            this.Edit_SecCheq.MinimumWidth = 45;
            this.Edit_SecCheq.Name = "Edit_SecCheq";
            this.Edit_SecCheq.ReadOnly = true;
            this.Edit_SecCheq.Width = 45;
            // 
            // Delete_SecCheq
            // 
            this.Delete_SecCheq.HeaderText = "Delete";
            this.Delete_SecCheq.Image = global::SDMS.Properties.Resources.Del;
            this.Delete_SecCheq.MinimumWidth = 50;
            this.Delete_SecCheq.Name = "Delete_SecCheq";
            this.Delete_SecCheq.ReadOnly = true;
            this.Delete_SecCheq.Width = 50;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(22, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 15);
            this.label1.TabIndex = 139;
            this.label1.Text = "Bank Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(25, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 15);
            this.label5.TabIndex = 138;
            this.label5.Text = "Cheque No";
            // 
            // txtCheqNo
            // 
            this.txtCheqNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheqNo.Location = new System.Drawing.Point(107, 24);
            this.txtCheqNo.MaxLength = 50;
            this.txtCheqNo.Name = "txtCheqNo";
            this.txtCheqNo.Size = new System.Drawing.Size(167, 21);
            this.txtCheqNo.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(327, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 15);
            this.label2.TabIndex = 144;
            this.label2.Text = "Bank Branch";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.YellowGreen;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(624, 48);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 25);
            this.btnSave.TabIndex = 141;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.YellowGreen;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(697, 48);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 25);
            this.btnClose.TabIndex = 142;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SecurityCheq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(770, 267);
            this.ControlBox = false;
            this.Controls.Add(this.grouper1);
            this.Name = "SecurityCheq";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Security Cheq";
            this.Load += new System.EventHandler(this.SecurityCheq_Load);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.grouper7.ResumeLayout(false);
            this.grouper7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSecurityCheq)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtCheqNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private GroupCtrl.Grouper grouper1;
        private System.Windows.Forms.ToolTip toolTip1;
        private GroupCtrl.Grouper grouper7;
        private System.Windows.Forms.Label label79;
        public System.Windows.Forms.DataGridView gvSecurityCheq;
        private System.Windows.Forms.ComboBox cbBankName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO_SecCheq;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChequeNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BankName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Branch;
        private System.Windows.Forms.DataGridViewImageColumn Edit_SecCheq;
        private System.Windows.Forms.DataGridViewImageColumn Delete_SecCheq;
        private System.Windows.Forms.TextBox cbBranchName;
    }
}