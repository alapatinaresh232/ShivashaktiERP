namespace SSCRM
{
    partial class TrainingReports
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbPrgType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromdate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbTrainerNames = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblReportName = new System.Windows.Forms.Label();
            this.gvTrainingPrgDetl = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrgNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrgName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpCnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TopicsCnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Print = new System.Windows.Forms.DataGridViewImageColumn();
            this.cbTrainerType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrainingPrgDetl)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.cbPrgType);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.dtpFromdate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbTrainerNames);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblReportName);
            this.groupBox1.Controls.Add(this.gvTrainingPrgDetl);
            this.groupBox1.Controls.Add(this.cbTrainerType);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(886, 529);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // cbPrgType
            // 
            this.cbPrgType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrgType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPrgType.FormattingEnabled = true;
            this.cbPrgType.Items.AddRange(new object[] {
            "ACTUAL",
            "PLANNING"});
            this.cbPrgType.Location = new System.Drawing.Point(118, 16);
            this.cbPrgType.Name = "cbPrgType";
            this.cbPrgType.Size = new System.Drawing.Size(99, 24);
            this.cbPrgType.TabIndex = 172;
            this.cbPrgType.SelectedIndexChanged += new System.EventHandler(this.cbPrgType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(5, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 17);
            this.label5.TabIndex = 171;
            this.label5.Text = "Program Type";
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(330, 48);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(96, 22);
            this.dtpToDate.TabIndex = 170;
            this.dtpToDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpToDate.ValueChanged += new System.EventHandler(this.dtpToDate_ValueChanged);
            // 
            // dtpFromdate
            // 
            this.dtpFromdate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromdate.Location = new System.Drawing.Point(119, 48);
            this.dtpFromdate.Name = "dtpFromdate";
            this.dtpFromdate.Size = new System.Drawing.Size(96, 22);
            this.dtpFromdate.TabIndex = 168;
            this.dtpFromdate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpFromdate.ValueChanged += new System.EventHandler(this.dtpFromdate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(261, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 169;
            this.label1.Text = "To Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(37, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 167;
            this.label4.Text = "From Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(433, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 16);
            this.label3.TabIndex = 164;
            this.label3.Text = "Trainer Name";
            // 
            // cbTrainerNames
            // 
            this.cbTrainerNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrainerNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTrainerNames.FormattingEnabled = true;
            this.cbTrainerNames.Location = new System.Drawing.Point(538, 16);
            this.cbTrainerNames.Name = "cbTrainerNames";
            this.cbTrainerNames.Size = new System.Drawing.Size(333, 24);
            this.cbTrainerNames.TabIndex = 166;
            this.cbTrainerNames.SelectedIndexChanged += new System.EventHandler(this.cbTrainerNames_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(8, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 16);
            this.label2.TabIndex = 163;
            this.label2.Text = "Training Program Details";
            // 
            // lblReportName
            // 
            this.lblReportName.AutoSize = true;
            this.lblReportName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportName.ForeColor = System.Drawing.Color.Navy;
            this.lblReportName.Location = new System.Drawing.Point(7, 76);
            this.lblReportName.Name = "lblReportName";
            this.lblReportName.Size = new System.Drawing.Size(0, 17);
            this.lblReportName.TabIndex = 140;
            // 
            // gvTrainingPrgDetl
            // 
            this.gvTrainingPrgDetl.AllowUserToAddRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            this.gvTrainingPrgDetl.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.gvTrainingPrgDetl.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvTrainingPrgDetl.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvTrainingPrgDetl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.gvTrainingPrgDetl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTrainingPrgDetl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.PrgNo,
            this.PrgName,
            this.Location,
            this.EmpCnt,
            this.TopicsCnt,
            this.Print});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvTrainingPrgDetl.DefaultCellStyle = dataGridViewCellStyle10;
            this.gvTrainingPrgDetl.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvTrainingPrgDetl.Location = new System.Drawing.Point(9, 96);
            this.gvTrainingPrgDetl.MultiSelect = false;
            this.gvTrainingPrgDetl.Name = "gvTrainingPrgDetl";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvTrainingPrgDetl.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.gvTrainingPrgDetl.RowHeadersVisible = false;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Navy;
            this.gvTrainingPrgDetl.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.gvTrainingPrgDetl.Size = new System.Drawing.Size(864, 378);
            this.gvTrainingPrgDetl.TabIndex = 138;
            this.gvTrainingPrgDetl.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvTrainingPrgDetl_CellClick);
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
            // PrgNo
            // 
            this.PrgNo.HeaderText = "Prg No";
            this.PrgNo.Name = "PrgNo";
            this.PrgNo.ReadOnly = true;
            this.PrgNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PrgNo.Width = 90;
            // 
            // PrgName
            // 
            this.PrgName.HeaderText = "Prg Name";
            this.PrgName.Name = "PrgName";
            this.PrgName.ReadOnly = true;
            this.PrgName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PrgName.Width = 150;
            // 
            // Location
            // 
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            this.Location.ReadOnly = true;
            this.Location.Width = 250;
            // 
            // EmpCnt
            // 
            this.EmpCnt.HeaderText = "Emp Cnt";
            this.EmpCnt.Name = "EmpCnt";
            this.EmpCnt.ReadOnly = true;
            // 
            // TopicsCnt
            // 
            this.TopicsCnt.HeaderText = "Topics Cnt";
            this.TopicsCnt.Name = "TopicsCnt";
            this.TopicsCnt.ReadOnly = true;
            this.TopicsCnt.Width = 110;
            // 
            // Print
            // 
            this.Print.HeaderText = "Print";
            this.Print.Image = global::SSCRM.Properties.Resources.print_icon;
            this.Print.Name = "Print";
            this.Print.Width = 50;
            // 
            // cbTrainerType
            // 
            this.cbTrainerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrainerType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTrainerType.FormattingEnabled = true;
            this.cbTrainerType.Items.AddRange(new object[] {
            "INTERNAL",
            "EXTERNAL"});
            this.cbTrainerType.Location = new System.Drawing.Point(327, 16);
            this.cbTrainerType.Name = "cbTrainerType";
            this.cbTrainerType.Size = new System.Drawing.Size(104, 24);
            this.cbTrainerType.TabIndex = 129;
            this.cbTrainerType.SelectedIndexChanged += new System.EventHandler(this.cbTrainerType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(223, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 17);
            this.label6.TabIndex = 128;
            this.label6.Text = "Trainer Type";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Location = new System.Drawing.Point(266, 476);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(354, 46);
            this.groupBox3.TabIndex = 160;
            this.groupBox3.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(138, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 30);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // TrainingReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(892, 535);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MinimizeBox = false;
            this.Name = "TrainingReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Training Reports";
            this.Load += new System.EventHandler(this.TrainingReports_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrainingPrgDetl)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblReportName;
        public System.Windows.Forms.DataGridView gvTrainingPrgDetl;
        private System.Windows.Forms.ComboBox cbTrainerType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbTrainerNames;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrgNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrgName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpCnt;
        private System.Windows.Forms.DataGridViewTextBoxColumn TopicsCnt;
        private System.Windows.Forms.DataGridViewImageColumn Print;
        private System.Windows.Forms.ComboBox cbPrgType;
        private System.Windows.Forms.Label label5;
    }
}