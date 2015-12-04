namespace SSCRM
{
    partial class IdCardPreparationsdetails
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.gvEmpDetl = new System.Windows.Forms.DataGridView();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.txtEcode = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblEcode = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbTrnType = new System.Windows.Forms.ComboBox();
            this.SINo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.applNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ecode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desigCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Design = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Branch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrepDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrintDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DispatchDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpDetl)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.cmbTrnType);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.gvEmpDetl);
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.lblDate);
            this.groupBox1.Controls.Add(this.txtEcode);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.lblEcode);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(2, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 479);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(139, 19);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(310, 23);
            this.txtName.TabIndex = 8;
            // 
            // gvEmpDetl
            // 
            this.gvEmpDetl.AllowUserToAddRows = false;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Navy;
            this.gvEmpDetl.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.gvEmpDetl.BackgroundColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvEmpDetl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gvEmpDetl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEmpDetl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SINo,
            this.transNo,
            this.applNo,
            this.Ecode,
            this.empName,
            this.desigCode,
            this.Design,
            this.brCode,
            this.Branch,
            this.Status,
            this.RecDate,
            this.PrepDate,
            this.PrintDate,
            this.DispatchDate,
            this.select});
            this.gvEmpDetl.Location = new System.Drawing.Point(6, 51);
            this.gvEmpDetl.Name = "gvEmpDetl";
            this.gvEmpDetl.RowHeadersVisible = false;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvEmpDetl.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.gvEmpDetl.Size = new System.Drawing.Size(869, 380);
            this.gvEmpDetl.TabIndex = 5;
            this.gvEmpDetl.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvEmpDetl_CellContentClick);
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(495, 20);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(103, 23);
            this.dtpDate.TabIndex = 4;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDate.Location = new System.Drawing.Point(451, 24);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(42, 17);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "Date";
            // 
            // txtEcode
            // 
            this.txtEcode.Location = new System.Drawing.Point(57, 19);
            this.txtEcode.Name = "txtEcode";
            this.txtEcode.Size = new System.Drawing.Size(80, 23);
            this.txtEcode.TabIndex = 2;
            this.txtEcode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEcode_KeyUp);
            this.txtEcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEcode_KeyPress);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Azure;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnAdd.Location = new System.Drawing.Point(601, 19);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 27);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblEcode
            // 
            this.lblEcode.AutoSize = true;
            this.lblEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEcode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblEcode.Location = new System.Drawing.Point(4, 22);
            this.lblEcode.Name = "lblEcode";
            this.lblEcode.Size = new System.Drawing.Size(53, 17);
            this.lblEcode.TabIndex = 0;
            this.lblEcode.Text = "Ecode";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Location = new System.Drawing.Point(318, 426);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(244, 47);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Azure;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(125, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 26);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Azure;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(44, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbTrnType
            // 
            this.cmbTrnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrnType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrnType.ForeColor = System.Drawing.SystemColors.MenuText;
            this.cmbTrnType.FormattingEnabled = true;
            this.cmbTrnType.Items.AddRange(new object[] {
            "--SELECT--",
            "RECIEVED",
            "PREPARED",
            "PRINTING",
            "DISPATCHED"});
            this.cmbTrnType.Location = new System.Drawing.Point(681, 19);
            this.cmbTrnType.Name = "cmbTrnType";
            this.cmbTrnType.Size = new System.Drawing.Size(193, 26);
            this.cmbTrnType.TabIndex = 86;
            this.cmbTrnType.SelectedIndexChanged += new System.EventHandler(this.cmbTrnType_SelectedIndexChanged);
            // 
            // SINo
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SINo.DefaultCellStyle = dataGridViewCellStyle8;
            this.SINo.HeaderText = "SI.No";
            this.SINo.Name = "SINo";
            this.SINo.ReadOnly = true;
            this.SINo.Width = 50;
            // 
            // transNo
            // 
            this.transNo.HeaderText = "Trans No";
            this.transNo.Name = "transNo";
            this.transNo.ReadOnly = true;
            this.transNo.Visible = false;
            this.transNo.Width = 80;
            // 
            // applNo
            // 
            this.applNo.HeaderText = "Appl No";
            this.applNo.Name = "applNo";
            this.applNo.Visible = false;
            // 
            // Ecode
            // 
            this.Ecode.HeaderText = "Ecode";
            this.Ecode.Name = "Ecode";
            this.Ecode.ReadOnly = true;
            this.Ecode.Width = 55;
            // 
            // empName
            // 
            this.empName.HeaderText = "Name";
            this.empName.Name = "empName";
            this.empName.ReadOnly = true;
            this.empName.Width = 180;
            // 
            // desigCode
            // 
            this.desigCode.HeaderText = "Desig Id";
            this.desigCode.Name = "desigCode";
            this.desigCode.Visible = false;
            this.desigCode.Width = 50;
            // 
            // Design
            // 
            this.Design.HeaderText = "Designation";
            this.Design.Name = "Design";
            this.Design.ReadOnly = true;
            this.Design.Width = 110;
            // 
            // brCode
            // 
            this.brCode.HeaderText = "Branch Code";
            this.brCode.Name = "brCode";
            this.brCode.Visible = false;
            this.brCode.Width = 75;
            // 
            // Branch
            // 
            this.Branch.HeaderText = "Branch";
            this.Branch.Name = "Branch";
            this.Branch.ReadOnly = true;
            this.Branch.Width = 200;
            // 
            // Status
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Status.DefaultCellStyle = dataGridViewCellStyle9;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // RecDate
            // 
            this.RecDate.HeaderText = "Recieve Date";
            this.RecDate.Name = "RecDate";
            this.RecDate.ReadOnly = true;
            this.RecDate.Width = 85;
            // 
            // PrepDate
            // 
            this.PrepDate.HeaderText = "Prepare Date";
            this.PrepDate.Name = "PrepDate";
            this.PrepDate.ReadOnly = true;
            this.PrepDate.Visible = false;
            this.PrepDate.Width = 85;
            // 
            // PrintDate
            // 
            this.PrintDate.HeaderText = "Print Date";
            this.PrintDate.Name = "PrintDate";
            this.PrintDate.ReadOnly = true;
            this.PrintDate.Visible = false;
            this.PrintDate.Width = 85;
            // 
            // DispatchDate
            // 
            this.DispatchDate.HeaderText = "Dispatch Date";
            this.DispatchDate.Name = "DispatchDate";
            this.DispatchDate.ReadOnly = true;
            this.DispatchDate.Visible = false;
            this.DispatchDate.Width = 85;
            // 
            // select
            // 
            this.select.HeaderText = "";
            this.select.Name = "select";
            this.select.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.select.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.select.Width = 40;
            // 
            // IdCardPreparationsdetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(884, 485);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "IdCardPreparationsdetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IdCardDetails";
            this.Load += new System.EventHandler(this.Idcarddetails_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpDetl)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TextBox txtEcode;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblEcode;
        private System.Windows.Forms.DataGridView gvEmpDetl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cmbTrnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn SINo;
        private System.Windows.Forms.DataGridViewTextBoxColumn transNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn applNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ecode;
        private System.Windows.Forms.DataGridViewTextBoxColumn empName;
        private System.Windows.Forms.DataGridViewTextBoxColumn desigCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Design;
        private System.Windows.Forms.DataGridViewTextBoxColumn brCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Branch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrepDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrintDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DispatchDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select;
    }
}

