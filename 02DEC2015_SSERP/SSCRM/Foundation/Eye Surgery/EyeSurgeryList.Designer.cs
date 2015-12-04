namespace SSCRM
{
    partial class EyeSurgeryList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.gvSurgeryList = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrnNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MrdNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatientName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Eye = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hospital = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SurgeryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSurgeryList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.gvSurgeryList);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(917, 566);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Surgery List";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(421, 534);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 26);
            this.btnClose.TabIndex = 95;
            this.btnClose.Text = "C&lose";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gvSurgeryList
            // 
            this.gvSurgeryList.AllowUserToAddRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            this.gvSurgeryList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.gvSurgeryList.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvSurgeryList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSurgeryList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.gvSurgeryList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSurgeryList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.TrnNo,
            this.MrdNo,
            this.PatientName,
            this.Eye,
            this.Hospital,
            this.SurgeryDate,
            this.Edit});
            this.gvSurgeryList.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvSurgeryList.Location = new System.Drawing.Point(8, 25);
            this.gvSurgeryList.MultiSelect = false;
            this.gvSurgeryList.Name = "gvSurgeryList";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSurgeryList.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.gvSurgeryList.RowHeadersVisible = false;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Navy;
            this.gvSurgeryList.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.gvSurgeryList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvSurgeryList.Size = new System.Drawing.Size(902, 506);
            this.gvSurgeryList.TabIndex = 94;
            this.gvSurgeryList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSurgeryList_CellClick);
            // 
            // SLNO
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.SLNO.DefaultCellStyle = dataGridViewCellStyle9;
            this.SLNO.Frozen = true;
            this.SLNO.HeaderText = "Sl.No";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SLNO.Width = 50;
            // 
            // TrnNo
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.TrnNo.DefaultCellStyle = dataGridViewCellStyle10;
            this.TrnNo.Frozen = true;
            this.TrnNo.HeaderText = "Trn No";
            this.TrnNo.Name = "TrnNo";
            this.TrnNo.ReadOnly = true;
            this.TrnNo.Visible = false;
            this.TrnNo.Width = 80;
            // 
            // MrdNo
            // 
            this.MrdNo.Frozen = true;
            this.MrdNo.HeaderText = "MRD No";
            this.MrdNo.Name = "MrdNo";
            this.MrdNo.ReadOnly = true;
            this.MrdNo.Width = 120;
            // 
            // PatientName
            // 
            this.PatientName.Frozen = true;
            this.PatientName.HeaderText = "Name";
            this.PatientName.Name = "PatientName";
            this.PatientName.ReadOnly = true;
            this.PatientName.Width = 220;
            // 
            // Eye
            // 
            this.Eye.Frozen = true;
            this.Eye.HeaderText = "Eye";
            this.Eye.Name = "Eye";
            this.Eye.ReadOnly = true;
            this.Eye.Width = 80;
            // 
            // Hospital
            // 
            this.Hospital.Frozen = true;
            this.Hospital.HeaderText = "Hospital";
            this.Hospital.Name = "Hospital";
            this.Hospital.ReadOnly = true;
            this.Hospital.Width = 200;
            // 
            // SurgeryDate
            // 
            this.SurgeryDate.Frozen = true;
            this.SurgeryDate.HeaderText = "Surgery Date";
            this.SurgeryDate.Name = "SurgeryDate";
            this.SurgeryDate.Width = 140;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.Edit.Name = "Edit";
            this.Edit.Width = 50;
            // 
            // EyeSurgeryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(922, 571);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EyeSurgeryList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EyeSurgeryList";
            this.Load += new System.EventHandler(this.EyeSurgeryList_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvSurgeryList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.DataGridView gvSurgeryList;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrnNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn MrdNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatientName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Eye;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hospital;
        private System.Windows.Forms.DataGridViewTextBoxColumn SurgeryDate;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
    }
}