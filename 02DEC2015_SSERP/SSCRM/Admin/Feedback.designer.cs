namespace SSCRM
{
    partial class FeedBack
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gvFeedBack = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FeedBackNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FeedBackType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnterDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FeedBackGV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFeedBackType = new System.Windows.Forms.ComboBox();
            this.txtFeedBack = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvFeedBack)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.gvFeedBack);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Location = new System.Drawing.Point(4, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(768, 401);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            // 
            // gvFeedBack
            // 
            this.gvFeedBack.AllowUserToAddRows = false;
            this.gvFeedBack.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvFeedBack.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvFeedBack.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvFeedBack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvFeedBack.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.FeedBackNo,
            this.FeedBackType,
            this.EnterDate,
            this.FeedBackGV});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SaddleBrown;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvFeedBack.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvFeedBack.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gvFeedBack.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvFeedBack.Location = new System.Drawing.Point(4, 181);
            this.gvFeedBack.MultiSelect = false;
            this.gvFeedBack.Name = "gvFeedBack";
            this.gvFeedBack.RowHeadersVisible = false;
            this.gvFeedBack.RowTemplate.ReadOnly = true;
            this.gvFeedBack.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvFeedBack.Size = new System.Drawing.Size(761, 158);
            this.gvFeedBack.TabIndex = 10;
            this.gvFeedBack.DoubleClick += new System.EventHandler(this.gvFeedBack_DoubleClick);
            // 
            // SLNO
            // 
            this.SLNO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SLNO.Frozen = true;
            this.SLNO.HeaderText = "S.No";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SLNO.Width = 45;
            // 
            // FeedBackNo
            // 
            this.FeedBackNo.Frozen = true;
            this.FeedBackNo.HeaderText = "F.B.No";
            this.FeedBackNo.Name = "FeedBackNo";
            this.FeedBackNo.ReadOnly = true;
            this.FeedBackNo.Width = 120;
            // 
            // FeedBackType
            // 
            this.FeedBackType.Frozen = true;
            this.FeedBackType.HeaderText = "Feed Back Type";
            this.FeedBackType.Name = "FeedBackType";
            this.FeedBackType.ReadOnly = true;
            this.FeedBackType.Width = 150;
            // 
            // EnterDate
            // 
            this.EnterDate.Frozen = true;
            this.EnterDate.HeaderText = "EnterDate";
            this.EnterDate.Name = "EnterDate";
            // 
            // FeedBackGV
            // 
            this.FeedBackGV.Frozen = true;
            this.FeedBackGV.HeaderText = "FeedBack";
            this.FeedBackGV.Name = "FeedBackGV";
            this.FeedBackGV.ReadOnly = true;
            this.FeedBackGV.Width = 320;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbFeedBackType);
            this.groupBox1.Controls.Add(this.txtFeedBack);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(6, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(758, 154);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(48, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 16);
            this.label1.TabIndex = 22;
            this.label1.Text = "Feed back Type";
            // 
            // cbFeedBackType
            // 
            this.cbFeedBackType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFeedBackType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.980198F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFeedBackType.FormattingEnabled = true;
            this.cbFeedBackType.Items.AddRange(new object[] {
            "PROBLEM",
            "SUGGESTION"});
            this.cbFeedBackType.Location = new System.Drawing.Point(174, 35);
            this.cbFeedBackType.Name = "cbFeedBackType";
            this.cbFeedBackType.Size = new System.Drawing.Size(240, 24);
            this.cbFeedBackType.TabIndex = 21;
            this.cbFeedBackType.SelectedIndexChanged += new System.EventHandler(this.cbFeedBackType_SelectedIndexChanged);
            // 
            // txtFeedBack
            // 
            this.txtFeedBack.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFeedBack.Location = new System.Drawing.Point(174, 66);
            this.txtFeedBack.MaxLength = 500;
            this.txtFeedBack.Multiline = true;
            this.txtFeedBack.Name = "txtFeedBack";
            this.txtFeedBack.Size = new System.Drawing.Size(544, 71);
            this.txtFeedBack.TabIndex = 4;
            this.txtFeedBack.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFeedBack_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(88, 68);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "Feed back";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Location = new System.Drawing.Point(5, 343);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(759, 45);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(335, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(428, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(259, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FeedBack
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClientSize = new System.Drawing.Size(777, 412);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Name = "FeedBack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Feed back";
            this.Load += new System.EventHandler(this.FeedBack_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvFeedBack)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFeedBack;
        private System.Windows.Forms.DataGridView gvFeedBack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFeedBackType;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn FeedBackNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FeedBackType;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnterDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn FeedBackGV;
    }
}