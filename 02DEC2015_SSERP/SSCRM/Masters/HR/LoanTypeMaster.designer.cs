namespace SSCRM
{
    partial class LoanTypeMaster
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkLoanActive = new System.Windows.Forms.CheckBox();
            this.dgvLoanMaster = new System.Windows.Forms.DataGridView();
            this.SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HPLTM_LOAN_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HPLTM_LOAN_TYPE_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HPLTM_RECORD_STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.txtLoanDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtLoanType = new System.Windows.Forms.TextBox();
            this.lblLoanName = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btndelete = new System.Windows.Forms.Button();
            this.btbClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoanMaster)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.chkLoanActive);
            this.groupBox1.Controls.Add(this.dgvLoanMaster);
            this.groupBox1.Controls.Add(this.txtLoanDescription);
            this.groupBox1.Controls.Add(this.lblDescription);
            this.groupBox1.Controls.Add(this.txtLoanType);
            this.groupBox1.Controls.Add(this.lblLoanName);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(5, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(606, 384);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Loan Master";
            // 
            // chkLoanActive
            // 
            this.chkLoanActive.AutoSize = true;
            this.chkLoanActive.BackColor = System.Drawing.Color.PowderBlue;
            this.chkLoanActive.ForeColor = System.Drawing.Color.Red;
            this.chkLoanActive.Location = new System.Drawing.Point(486, 27);
            this.chkLoanActive.Name = "chkLoanActive";
            this.chkLoanActive.Size = new System.Drawing.Size(70, 20);
            this.chkLoanActive.TabIndex = 65;
            this.chkLoanActive.Text = "Active";
            this.chkLoanActive.UseVisualStyleBackColor = false;
            // 
            // dgvLoanMaster
            // 
            this.dgvLoanMaster.AllowUserToAddRows = false;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Navy;
            this.dgvLoanMaster.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvLoanMaster.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgvLoanMaster.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvLoanMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLoanMaster.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SNo,
            this.HPLTM_LOAN_TYPE,
            this.HPLTM_LOAN_TYPE_DESC,
            this.HPLTM_RECORD_STATUS,
            this.Edit});
            this.dgvLoanMaster.GridColor = System.Drawing.SystemColors.Info;
            this.dgvLoanMaster.Location = new System.Drawing.Point(5, 99);
            this.dgvLoanMaster.Name = "dgvLoanMaster";
            this.dgvLoanMaster.RowHeadersVisible = false;
            this.dgvLoanMaster.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvLoanMaster.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvLoanMaster.Size = new System.Drawing.Size(595, 236);
            this.dgvLoanMaster.TabIndex = 64;
            this.dgvLoanMaster.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLoanMaster_CellClick);
            // 
            // SNo
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.InfoText;
            this.SNo.DefaultCellStyle = dataGridViewCellStyle12;
            this.SNo.Frozen = true;
            this.SNo.HeaderText = "SLNo";
            this.SNo.Name = "SNo";
            this.SNo.ReadOnly = true;
            this.SNo.Width = 50;
            // 
            // HPLTM_LOAN_TYPE
            // 
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HPLTM_LOAN_TYPE.DefaultCellStyle = dataGridViewCellStyle13;
            this.HPLTM_LOAN_TYPE.Frozen = true;
            this.HPLTM_LOAN_TYPE.HeaderText = "Loan Type";
            this.HPLTM_LOAN_TYPE.Name = "HPLTM_LOAN_TYPE";
            this.HPLTM_LOAN_TYPE.ReadOnly = true;
            this.HPLTM_LOAN_TYPE.Width = 200;
            // 
            // HPLTM_LOAN_TYPE_DESC
            // 
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HPLTM_LOAN_TYPE_DESC.DefaultCellStyle = dataGridViewCellStyle14;
            this.HPLTM_LOAN_TYPE_DESC.HeaderText = "Description";
            this.HPLTM_LOAN_TYPE_DESC.Name = "HPLTM_LOAN_TYPE_DESC";
            this.HPLTM_LOAN_TYPE_DESC.ReadOnly = true;
            this.HPLTM_LOAN_TYPE_DESC.Width = 200;
            // 
            // HPLTM_RECORD_STATUS
            // 
            this.HPLTM_RECORD_STATUS.HeaderText = "Status";
            this.HPLTM_RECORD_STATUS.Name = "HPLTM_RECORD_STATUS";
            // 
            // Edit
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.NullValue = null;
            this.Edit.DefaultCellStyle = dataGridViewCellStyle15;
            this.Edit.HeaderText = "";
            this.Edit.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.Edit.Name = "Edit";
            this.Edit.Width = 30;
            // 
            // txtLoanDescription
            // 
            this.txtLoanDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoanDescription.Location = new System.Drawing.Point(135, 53);
            this.txtLoanDescription.MaxLength = 30;
            this.txtLoanDescription.Multiline = true;
            this.txtLoanDescription.Name = "txtLoanDescription";
            this.txtLoanDescription.Size = new System.Drawing.Size(421, 40);
            this.txtLoanDescription.TabIndex = 63;
            this.txtLoanDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLoanDescription_KeyPress);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblDescription.Location = new System.Drawing.Point(50, 61);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(79, 17);
            this.lblDescription.TabIndex = 62;
            this.lblDescription.Text = "Description";
            // 
            // txtLoanType
            // 
            this.txtLoanType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoanType.Location = new System.Drawing.Point(135, 24);
            this.txtLoanType.MaxLength = 10;
            this.txtLoanType.Name = "txtLoanType";
            this.txtLoanType.Size = new System.Drawing.Size(216, 22);
            this.txtLoanType.TabIndex = 60;
            this.txtLoanType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLoanType_KeyPress);
            // 
            // lblLoanName
            // 
            this.lblLoanName.AutoSize = true;
            this.lblLoanName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoanName.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblLoanName.Location = new System.Drawing.Point(55, 26);
            this.lblLoanName.Name = "lblLoanName";
            this.lblLoanName.Size = new System.Drawing.Size(72, 17);
            this.lblLoanName.TabIndex = 58;
            this.lblLoanName.Text = "Loan Type";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btndelete);
            this.groupBox3.Controls.Add(this.btbClear);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Location = new System.Drawing.Point(104, 332);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(398, 47);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            // 
            // btndelete
            // 
            this.btndelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btndelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.Location = new System.Drawing.Point(220, 11);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(78, 30);
            this.btndelete.TabIndex = 3;
            this.btndelete.Text = "Delete";
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // btbClear
            // 
            this.btbClear.BackColor = System.Drawing.Color.AliceBlue;
            this.btbClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btbClear.Location = new System.Drawing.Point(136, 11);
            this.btbClear.Name = "btbClear";
            this.btbClear.Size = new System.Drawing.Size(78, 30);
            this.btbClear.TabIndex = 2;
            this.btbClear.Text = "Clear";
            this.btbClear.UseVisualStyleBackColor = false;
            this.btbClear.Click += new System.EventHandler(this.btbClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(301, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(52, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // LoanTypeMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(616, 392);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LoanTypeMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loan Type Master";
            this.Load += new System.EventHandler(this.LoanMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoanMaster)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.DataGridView dgvLoanMaster;
        private System.Windows.Forms.TextBox txtLoanDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtLoanType;
        private System.Windows.Forms.Label lblLoanName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btbClear;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkLoanActive;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn HPLTM_LOAN_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn HPLTM_LOAN_TYPE_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn HPLTM_RECORD_STATUS;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
    }
}