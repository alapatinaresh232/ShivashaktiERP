namespace SDMS
{
    partial class AgingIntervals
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
            this.dtpValidTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpValidFrom = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.txtTranNo = new System.Windows.Forms.TextBox();
            this.lblTranNo = new System.Windows.Forms.Label();
            this.gvAgingIntervalDetails = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fromDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IntRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.lblCompany = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvAgingIntervalDetails)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.dtpValidTo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpValidFrom);
            this.groupBox1.Controls.Add(this.lblDate);
            this.groupBox1.Controls.Add(this.txtTranNo);
            this.groupBox1.Controls.Add(this.lblTranNo);
            this.groupBox1.Controls.Add(this.gvAgingIntervalDetails);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.lblCompany);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(1, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(587, 454);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dtpValidTo
            // 
            this.dtpValidTo.CustomFormat = "dd/MM/yyyy";
            this.dtpValidTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpValidTo.Location = new System.Drawing.Point(413, 47);
            this.dtpValidTo.Name = "dtpValidTo";
            this.dtpValidTo.Size = new System.Drawing.Size(94, 23);
            this.dtpValidTo.TabIndex = 7;
            this.dtpValidTo.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(346, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Valid To";
            // 
            // dtpValidFrom
            // 
            this.dtpValidFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpValidFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpValidFrom.Location = new System.Drawing.Point(239, 47);
            this.dtpValidFrom.Name = "dtpValidFrom";
            this.dtpValidFrom.Size = new System.Drawing.Size(94, 23);
            this.dtpValidFrom.TabIndex = 5;
            this.dtpValidFrom.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblDate.Location = new System.Drawing.Point(155, 50);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(85, 17);
            this.lblDate.TabIndex = 4;
            this.lblDate.Text = "Valid From";
            // 
            // txtTranNo
            // 
            this.txtTranNo.AcceptsTab = true;
            this.txtTranNo.BackColor = System.Drawing.SystemColors.Window;
            this.txtTranNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTranNo.Location = new System.Drawing.Point(82, 48);
            this.txtTranNo.Name = "txtTranNo";
            this.txtTranNo.Size = new System.Drawing.Size(58, 22);
            this.txtTranNo.TabIndex = 3;
            this.txtTranNo.TabStop = false;
            this.txtTranNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTranNo_KeyUp);
            // 
            // lblTranNo
            // 
            this.lblTranNo.AutoSize = true;
            this.lblTranNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTranNo.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblTranNo.Location = new System.Drawing.Point(18, 50);
            this.lblTranNo.Name = "lblTranNo";
            this.lblTranNo.Size = new System.Drawing.Size(58, 17);
            this.lblTranNo.TabIndex = 2;
            this.lblTranNo.Text = "Trn No";
            // 
            // gvAgingIntervalDetails
            // 
            this.gvAgingIntervalDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvAgingIntervalDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvAgingIntervalDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvAgingIntervalDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvAgingIntervalDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvAgingIntervalDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvAgingIntervalDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.fromDay,
            this.toDay,
            this.IntRate,
            this.Delete});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvAgingIntervalDetails.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvAgingIntervalDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvAgingIntervalDetails.Location = new System.Drawing.Point(12, 90);
            this.gvAgingIntervalDetails.MultiSelect = false;
            this.gvAgingIntervalDetails.Name = "gvAgingIntervalDetails";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvAgingIntervalDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvAgingIntervalDetails.RowHeadersVisible = false;
            this.gvAgingIntervalDetails.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.gvAgingIntervalDetails.Size = new System.Drawing.Size(564, 296);
            this.gvAgingIntervalDetails.TabIndex = 9;
            this.gvAgingIntervalDetails.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gvAgingIntervalDetails_CellValidating);
            this.gvAgingIntervalDetails.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.gvAgingIntervalDetails_RowsAdded);
            this.gvAgingIntervalDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvAgingIntervalDetails_CellEndEdit);
            this.gvAgingIntervalDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvAgingIntervalDetails_CellClick);
            this.gvAgingIntervalDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvAgingIntervalDetails_EditingControlShowing);
            // 
            // SLNO
            // 
            this.SLNO.Frozen = true;
            this.SLNO.HeaderText = "Sl.No";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SLNO.Width = 50;
            // 
            // fromDay
            // 
            this.fromDay.Frozen = true;
            this.fromDay.HeaderText = "From Days";
            this.fromDay.MinimumWidth = 20;
            this.fromDay.Name = "fromDay";
            this.fromDay.ReadOnly = true;
            this.fromDay.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.fromDay.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.fromDay.Width = 110;
            // 
            // toDay
            // 
            this.toDay.Frozen = true;
            this.toDay.HeaderText = "To Days";
            this.toDay.Name = "toDay";
            this.toDay.Width = 110;
            // 
            // IntRate
            // 
            this.IntRate.Frozen = true;
            this.IntRate.HeaderText = "Interest Rate%";
            this.IntRate.Name = "IntRate";
            this.IntRate.Width = 140;
            // 
            // Delete
            // 
            this.Delete.Frozen = true;
            this.Delete.HeaderText = "Del";
            this.Delete.Image = global::SDMS.Properties.Resources.actions_delete;
            this.Delete.Name = "Delete";
            this.Delete.Width = 50;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnClose);
            this.groupBox5.Controls.Add(this.btnCancel);
            this.groupBox5.Controls.Add(this.btnSave);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox5.Location = new System.Drawing.Point(145, 393);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(296, 45);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(188, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 26);
            this.btnClose.TabIndex = 2;
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
            this.btnCancel.Location = new System.Drawing.Point(111, 13);
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
            this.btnSave.Location = new System.Drawing.Point(34, 13);
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
            this.cbCompany.Location = new System.Drawing.Point(82, 16);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(399, 24);
            this.cbCompany.TabIndex = 1;
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblCompany.Location = new System.Drawing.Point(8, 19);
            this.lblCompany.Margin = new System.Windows.Forms.Padding(0);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(73, 16);
            this.lblCompany.TabIndex = 0;
            this.lblCompany.Text = "Company";
            // 
            // groupBox2
            // 
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(6, 73);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(574, 320);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Aging Interval Details";
            // 
            // AgingIntervals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(590, 457);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AgingIntervals";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AgingIntervals";
            this.Load += new System.EventHandler(this.AgingIntervals_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvAgingIntervalDetails)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.DataGridView gvAgingIntervalDetails;
        private System.Windows.Forms.TextBox txtTranNo;
        private System.Windows.Forms.Label lblTranNo;
        private System.Windows.Forms.DateTimePicker dtpValidTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpValidFrom;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn fromDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn toDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn IntRate;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
    }
}