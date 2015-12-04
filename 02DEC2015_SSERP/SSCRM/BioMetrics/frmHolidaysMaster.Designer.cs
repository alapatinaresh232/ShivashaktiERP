namespace SSCRM
{
    partial class frmHolidaysMaster
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkBrAll = new System.Windows.Forms.CheckBox();
            this.clbbranch = new System.Windows.Forms.CheckedListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.clbBrType = new System.Windows.Forms.CheckedListBox();
            this.chkBrTypeAll = new System.Windows.Forms.CheckBox();
            this.txtHolDesc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHolId = new System.Windows.Forms.TextBox();
            this.nmYear = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpHolidayDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtHolName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpStates = new System.Windows.Forms.GroupBox();
            this.clbState = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmYear)).BeginInit();
            this.grpStates.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.txtHolDesc);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtHolId);
            this.groupBox1.Controls.Add(this.nmYear);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpHolidayDate);
            this.groupBox1.Controls.Add(this.lblDate);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.txtHolName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.grpStates);
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(661, 538);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnReport);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Location = new System.Drawing.Point(113, 488);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(434, 44);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnReport.Location = new System.Drawing.Point(313, 12);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(78, 26);
            this.btnReport.TabIndex = 3;
            this.btnReport.Text = "&Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(126, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Clear";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(209, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 26);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "C&lose";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(43, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkBrAll);
            this.groupBox4.Controls.Add(this.clbbranch);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox4.Location = new System.Drawing.Point(248, 93);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(400, 381);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Branches";
            // 
            // chkBrAll
            // 
            this.chkBrAll.AutoSize = true;
            this.chkBrAll.ForeColor = System.Drawing.Color.Green;
            this.chkBrAll.Location = new System.Drawing.Point(350, -2);
            this.chkBrAll.Name = "chkBrAll";
            this.chkBrAll.Size = new System.Drawing.Size(45, 20);
            this.chkBrAll.TabIndex = 0;
            this.chkBrAll.Text = "All";
            this.chkBrAll.UseVisualStyleBackColor = true;
            this.chkBrAll.CheckedChanged += new System.EventHandler(this.chkBrAll_CheckedChanged);
            // 
            // clbbranch
            // 
            this.clbbranch.CheckOnClick = true;
            this.clbbranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbbranch.FormattingEnabled = true;
            this.clbbranch.Location = new System.Drawing.Point(8, 19);
            this.clbbranch.Name = "clbbranch";
            this.clbbranch.Size = new System.Drawing.Size(383, 356);
            this.clbbranch.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.clbBrType);
            this.groupBox3.Controls.Add(this.chkBrTypeAll);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox3.Location = new System.Drawing.Point(7, 293);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(223, 180);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "BranchType";
            // 
            // clbBrType
            // 
            this.clbBrType.CheckOnClick = true;
            this.clbBrType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbBrType.FormattingEnabled = true;
            this.clbBrType.Location = new System.Drawing.Point(6, 25);
            this.clbBrType.Name = "clbBrType";
            this.clbBrType.Size = new System.Drawing.Size(211, 148);
            this.clbBrType.TabIndex = 1;
            this.clbBrType.SelectedIndexChanged += new System.EventHandler(this.clbBrType_SelectedIndexChanged);
            this.clbBrType.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbBrType_ItemCheck);
            // 
            // chkBrTypeAll
            // 
            this.chkBrTypeAll.AutoSize = true;
            this.chkBrTypeAll.ForeColor = System.Drawing.Color.Green;
            this.chkBrTypeAll.Location = new System.Drawing.Point(125, -2);
            this.chkBrTypeAll.Name = "chkBrTypeAll";
            this.chkBrTypeAll.Size = new System.Drawing.Size(45, 20);
            this.chkBrTypeAll.TabIndex = 0;
            this.chkBrTypeAll.Text = "All";
            this.chkBrTypeAll.UseVisualStyleBackColor = true;
            this.chkBrTypeAll.CheckedChanged += new System.EventHandler(this.chkBrTypeAll_CheckedChanged);
            // 
            // txtHolDesc
            // 
            this.txtHolDesc.BackColor = System.Drawing.SystemColors.Window;
            this.txtHolDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHolDesc.Location = new System.Drawing.Point(353, 49);
            this.txtHolDesc.MaxLength = 50;
            this.txtHolDesc.Multiline = true;
            this.txtHolDesc.Name = "txtHolDesc";
            this.txtHolDesc.Size = new System.Drawing.Size(293, 37);
            this.txtHolDesc.TabIndex = 9;
            this.txtHolDesc.TabStop = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(259, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Description";
            // 
            // txtHolId
            // 
            this.txtHolId.BackColor = System.Drawing.Color.White;
            this.txtHolId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHolId.Location = new System.Drawing.Point(163, 20);
            this.txtHolId.MaxLength = 50;
            this.txtHolId.Name = "txtHolId";
            this.txtHolId.Size = new System.Drawing.Size(67, 22);
            this.txtHolId.TabIndex = 3;
            this.txtHolId.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtHolId_KeyUp);
            // 
            // nmYear
            // 
            this.nmYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nmYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmYear.Location = new System.Drawing.Point(59, 20);
            this.nmYear.Maximum = new decimal(new int[] {
            2020,
            0,
            0,
            0});
            this.nmYear.Name = "nmYear";
            this.nmYear.Size = new System.Drawing.Size(70, 22);
            this.nmYear.TabIndex = 1;
            this.nmYear.Value = new decimal(new int[] {
            2014,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(14, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Year";
            // 
            // dtpHolidayDate
            // 
            this.dtpHolidayDate.CustomFormat = "dd/MM/yyyy";
            this.dtpHolidayDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHolidayDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHolidayDate.Location = new System.Drawing.Point(121, 52);
            this.dtpHolidayDate.Name = "dtpHolidayDate";
            this.dtpHolidayDate.Size = new System.Drawing.Size(109, 23);
            this.dtpHolidayDate.TabIndex = 7;
            this.dtpHolidayDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblDate.Location = new System.Drawing.Point(16, 54);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(99, 16);
            this.lblDate.TabIndex = 6;
            this.lblDate.Text = "Holiday Date";
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label25.Location = new System.Drawing.Point(137, 23);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(39, 16);
            this.label25.TabIndex = 2;
            this.label25.Text = "ID";
            // 
            // txtHolName
            // 
            this.txtHolName.BackColor = System.Drawing.SystemColors.Window;
            this.txtHolName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHolName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHolName.Location = new System.Drawing.Point(353, 19);
            this.txtHolName.MaxLength = 50;
            this.txtHolName.Name = "txtHolName";
            this.txtHolName.Size = new System.Drawing.Size(293, 24);
            this.txtHolName.TabIndex = 5;
            this.txtHolName.TabStop = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(243, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Holiday Name";
            // 
            // grpStates
            // 
            this.grpStates.Controls.Add(this.clbState);
            this.grpStates.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpStates.ForeColor = System.Drawing.Color.Navy;
            this.grpStates.Location = new System.Drawing.Point(6, 91);
            this.grpStates.Name = "grpStates";
            this.grpStates.Size = new System.Drawing.Size(224, 197);
            this.grpStates.TabIndex = 10;
            this.grpStates.TabStop = false;
            this.grpStates.Text = "State";
            // 
            // clbState
            // 
            this.clbState.CheckOnClick = true;
            this.clbState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbState.FormattingEnabled = true;
            this.clbState.Location = new System.Drawing.Point(7, 26);
            this.clbState.Name = "clbState";
            this.clbState.Size = new System.Drawing.Size(209, 164);
            this.clbState.TabIndex = 0;
            this.clbState.SelectedIndexChanged += new System.EventHandler(this.clbState_SelectedIndexChanged);
            this.clbState.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbState_ItemCheck);
            // 
            // frmHolidaysMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(666, 543);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "frmHolidaysMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Holidays Master";
            this.Load += new System.EventHandler(this.frmHolidaysMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmYear)).EndInit();
            this.grpStates.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpStates;
        private System.Windows.Forms.CheckedListBox clbState;
        public System.Windows.Forms.TextBox txtHolDesc;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtHolId;
        private System.Windows.Forms.NumericUpDown nmYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpHolidayDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label label25;
        public System.Windows.Forms.TextBox txtHolName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckedListBox clbBrType;
        private System.Windows.Forms.CheckBox chkBrTypeAll;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckedListBox clbbranch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkBrAll;
    }
}