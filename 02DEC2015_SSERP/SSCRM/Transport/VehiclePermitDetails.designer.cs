namespace SSCRM
{
    partial class VehiclePermitDetails
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpValidTo = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpValidFrom = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPermitNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbPermit = new System.Windows.Forms.ComboBox();
            this.cbVehNo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbLocation = new System.Windows.Forms.ComboBox();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gvPermitDetails = new System.Windows.Forms.DataGridView();
            this.sino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.perpolSno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicleno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehicleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.permitno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.permittype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.del = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPermitDetails)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.dtpValidTo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dtpValidFrom);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtPermitNo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbPermit);
            this.groupBox1.Controls.Add(this.cbVehNo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbLocation);
            this.groupBox1.Controls.Add(this.cbCompany);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(797, 102);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dtpValidTo
            // 
            this.dtpValidTo.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpValidTo.CustomFormat = "dd/MM/yyyy";
            this.dtpValidTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpValidTo.Location = new System.Drawing.Point(690, 65);
            this.dtpValidTo.Name = "dtpValidTo";
            this.dtpValidTo.Size = new System.Drawing.Size(100, 22);
            this.dtpValidTo.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(620, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Valid To";
            // 
            // dtpValidFrom
            // 
            this.dtpValidFrom.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpValidFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpValidFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpValidFrom.Location = new System.Drawing.Point(486, 65);
            this.dtpValidFrom.Name = "dtpValidFrom";
            this.dtpValidFrom.Size = new System.Drawing.Size(96, 22);
            this.dtpValidFrom.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(401, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Valid From";
            // 
            // txtPermitNo
            // 
            this.txtPermitNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPermitNo.Location = new System.Drawing.Point(93, 67);
            this.txtPermitNo.Name = "txtPermitNo";
            this.txtPermitNo.Size = new System.Drawing.Size(180, 22);
            this.txtPermitNo.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(12, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Permit No";
            // 
            // cbPermit
            // 
            this.cbPermit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPermit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPermit.FormattingEnabled = true;
            this.cbPermit.Items.AddRange(new object[] {
            "Select",
            "NATIONAL",
            "STATE"});
            this.cbPermit.Location = new System.Drawing.Point(486, 39);
            this.cbPermit.Name = "cbPermit";
            this.cbPermit.Size = new System.Drawing.Size(306, 24);
            this.cbPermit.TabIndex = 11;
            // 
            // cbVehNo
            // 
            this.cbVehNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVehNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbVehNo.FormattingEnabled = true;
            this.cbVehNo.Items.AddRange(new object[] {
            "                     Select"});
            this.cbVehNo.Location = new System.Drawing.Point(92, 39);
            this.cbVehNo.Name = "cbVehNo";
            this.cbVehNo.Size = new System.Drawing.Size(299, 24);
            this.cbVehNo.TabIndex = 9;
            this.cbVehNo.SelectedIndexChanged += new System.EventHandler(this.cbVehNo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(391, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Permit Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(5, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Vehicle No";
            // 
            // cbLocation
            // 
            this.cbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLocation.FormattingEnabled = true;
            this.cbLocation.Location = new System.Drawing.Point(486, 13);
            this.cbLocation.Name = "cbLocation";
            this.cbLocation.Size = new System.Drawing.Size(306, 24);
            this.cbLocation.TabIndex = 7;
            this.cbLocation.SelectedIndexChanged += new System.EventHandler(this.cbLocation_SelectedIndexChanged);
            // 
            // cbCompany
            // 
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Items.AddRange(new object[] {
            "                     Select"});
            this.cbCompany.Location = new System.Drawing.Point(92, 12);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(299, 24);
            this.cbCompany.TabIndex = 5;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(416, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Location";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(17, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Company";
            // 
            // gvPermitDetails
            // 
            this.gvPermitDetails.AllowUserToAddRows = false;
            this.gvPermitDetails.BackgroundColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPermitDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.gvPermitDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPermitDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sino,
            this.perpolSno,
            this.vehicleno,
            this.vehicleName,
            this.permitno,
            this.permittype,
            this.from,
            this.to,
            this.edit,
            this.del});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvPermitDetails.DefaultCellStyle = dataGridViewCellStyle11;
            this.gvPermitDetails.Location = new System.Drawing.Point(8, 108);
            this.gvPermitDetails.Name = "gvPermitDetails";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPermitDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.gvPermitDetails.RowHeadersVisible = false;
            this.gvPermitDetails.Size = new System.Drawing.Size(792, 306);
            this.gvPermitDetails.TabIndex = 1;
            this.gvPermitDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPermitDetails_CellContentClick);
            // 
            // sino
            // 
            this.sino.HeaderText = "SI.No";
            this.sino.Name = "sino";
            this.sino.Width = 50;
            // 
            // perpolSno
            // 
            this.perpolSno.HeaderText = "PermitPolSino";
            this.perpolSno.Name = "perpolSno";
            this.perpolSno.Visible = false;
            // 
            // vehicleno
            // 
            this.vehicleno.HeaderText = "Vehicle No";
            this.vehicleno.Name = "vehicleno";
            this.vehicleno.Width = 120;
            // 
            // vehicleName
            // 
            this.vehicleName.HeaderText = "Vehicle Name";
            this.vehicleName.Name = "vehicleName";
            // 
            // permitno
            // 
            this.permitno.HeaderText = "Permit No";
            this.permitno.Name = "permitno";
            this.permitno.Width = 150;
            // 
            // permittype
            // 
            this.permittype.HeaderText = "Permit Type";
            this.permittype.Name = "permittype";
            // 
            // from
            // 
            this.from.HeaderText = "From";
            this.from.Name = "from";
            // 
            // to
            // 
            this.to.HeaderText = "To";
            this.to.Name = "to";
            // 
            // edit
            // 
            this.edit.HeaderText = "";
            this.edit.Name = "edit";
            this.edit.Width = 25;
            // 
            // del
            // 
            this.del.HeaderText = "";
            this.del.Name = "del";
            this.del.Width = 25;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.gvPermitDetails);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(1, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(808, 460);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnClear);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Location = new System.Drawing.Point(239, 411);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(331, 45);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Azure;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(210, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 31);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Azure;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClear.Location = new System.Drawing.Point(129, 9);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 31);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Azure;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(46, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 31);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // VehiclePermitDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(811, 465);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Name = "VehiclePermitDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vehicle Permit Detials";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPermitDetails)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gvPermitDetails;
        private System.Windows.Forms.ComboBox cbLocation;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbPermit;
        private System.Windows.Forms.ComboBox cbVehNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpValidFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPermitNo;
        private System.Windows.Forms.DateTimePicker dtpValidTo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn sino;
        private System.Windows.Forms.DataGridViewTextBoxColumn perpolSno;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicleno;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehicleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn permitno;
        private System.Windows.Forms.DataGridViewTextBoxColumn permittype;
        private System.Windows.Forms.DataGridViewTextBoxColumn from;
        private System.Windows.Forms.DataGridViewTextBoxColumn to;
        private System.Windows.Forms.DataGridViewImageColumn edit;
        private System.Windows.Forms.DataGridViewImageColumn del;
    }
}

