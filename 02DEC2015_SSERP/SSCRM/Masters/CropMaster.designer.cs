namespace SSCRM
{
    partial class CropMaster
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CropMaster));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvCropMaster = new System.Windows.Forms.DataGridView();
            this.SNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CROP_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CROP_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CROP_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtCropName = new System.Windows.Forms.TextBox();
            this.lblCropName = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btbClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCropMaster)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.dgvCropMaster);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.lblDescription);
            this.groupBox1.Controls.Add(this.txtCropName);
            this.groupBox1.Controls.Add(this.lblCropName);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(7, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(652, 338);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Crop Master";
            // 
            // dgvCropMaster
            // 
            this.dgvCropMaster.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Navy;
            this.dgvCropMaster.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCropMaster.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgvCropMaster.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvCropMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCropMaster.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SNo,
            this.CROP_ID,
            this.CROP_NAME,
            this.CROP_DESC,
            this.Edit,
            this.Delete});
            this.dgvCropMaster.GridColor = System.Drawing.SystemColors.Info;
            this.dgvCropMaster.Location = new System.Drawing.Point(5, 81);
            this.dgvCropMaster.Name = "dgvCropMaster";
            this.dgvCropMaster.RowHeadersVisible = false;
            this.dgvCropMaster.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvCropMaster.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvCropMaster.Size = new System.Drawing.Size(642, 204);
            this.dgvCropMaster.TabIndex = 64;
            this.dgvCropMaster.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCropMaster_CellContentClick);
            // 
            // SNo
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.SNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.SNo.Frozen = true;
            this.SNo.HeaderText = "SLNo";
            this.SNo.Name = "SNo";
            this.SNo.ReadOnly = true;
            this.SNo.Width = 50;
            // 
            // CROP_ID
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CROP_ID.DefaultCellStyle = dataGridViewCellStyle3;
            this.CROP_ID.Frozen = true;
            this.CROP_ID.HeaderText = "Crop Id";
            this.CROP_ID.Name = "CROP_ID";
            this.CROP_ID.ReadOnly = true;
            // 
            // CROP_NAME
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CROP_NAME.DefaultCellStyle = dataGridViewCellStyle4;
            this.CROP_NAME.Frozen = true;
            this.CROP_NAME.HeaderText = " Crop Name";
            this.CROP_NAME.Name = "CROP_NAME";
            this.CROP_NAME.Width = 200;
            // 
            // CROP_DESC
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CROP_DESC.DefaultCellStyle = dataGridViewCellStyle5;
            this.CROP_DESC.HeaderText = "Description";
            this.CROP_DESC.Name = "CROP_DESC";
            this.CROP_DESC.ReadOnly = true;
            this.CROP_DESC.Width = 200;
            // 
            // Edit
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle6.NullValue")));
            this.Edit.DefaultCellStyle = dataGridViewCellStyle6;
            this.Edit.HeaderText = "";
            this.Edit.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.Edit.Name = "Edit";
            this.Edit.Width = 30;
            // 
            // Delete
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle7.NullValue")));
            this.Delete.DefaultCellStyle = dataGridViewCellStyle7;
            this.Delete.HeaderText = "";
            this.Delete.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.Delete.Name = "Delete";
            this.Delete.Width = 30;
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(320, 30);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(320, 40);
            this.txtDescription.TabIndex = 63;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblDescription.Location = new System.Drawing.Point(240, 32);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(79, 17);
            this.lblDescription.TabIndex = 62;
            this.lblDescription.Text = "Description";
            // 
            // txtCropName
            // 
            this.txtCropName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCropName.Location = new System.Drawing.Point(89, 33);
            this.txtCropName.Name = "txtCropName";
            this.txtCropName.Size = new System.Drawing.Size(138, 22);
            this.txtCropName.TabIndex = 60;
            // 
            // lblCropName
            // 
            this.lblCropName.AutoSize = true;
            this.lblCropName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCropName.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblCropName.Location = new System.Drawing.Point(9, 35);
            this.lblCropName.Name = "lblCropName";
            this.lblCropName.Size = new System.Drawing.Size(77, 17);
            this.lblCropName.TabIndex = 58;
            this.lblCropName.Text = "Crop Name";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btbClear);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Location = new System.Drawing.Point(151, 285);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 47);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
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
            this.btnClose.Location = new System.Drawing.Point(220, 11);
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
            // CropMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(661, 346);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CropMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CropMaster";
            this.Load += new System.EventHandler(this.CropMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCropMaster)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCropName;
        private System.Windows.Forms.Label lblCropName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btbClear;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        public System.Windows.Forms.DataGridView dgvCropMaster;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CROP_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CROP_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn CROP_DESC;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Delete;

    }
}