namespace SSCRM
{
    partial class ActualProgramsForFeedBack
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
            this.label2 = new System.Windows.Forms.Label();
            this.dtpPrgToDate = new System.Windows.Forms.DateTimePicker();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnAddFeedbackDetails = new System.Windows.Forms.Button();
            this.cbProgramName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpPrgFromDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpPrgToDate);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.cbProgramName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpPrgFromDate);
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 158);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(280, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Date";
            // 
            // dtpPrgToDate
            // 
            this.dtpPrgToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpPrgToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpPrgToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPrgToDate.Location = new System.Drawing.Point(348, 14);
            this.dtpPrgToDate.Name = "dtpPrgToDate";
            this.dtpPrgToDate.Size = new System.Drawing.Size(94, 22);
            this.dtpPrgToDate.TabIndex = 3;
            this.dtpPrgToDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpPrgToDate.ValueChanged += new System.EventHandler(this.dtpPrgToDate_ValueChanged);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(304, 110);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnAddFeedbackDetails);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(47, 97);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(379, 45);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            // 
            // btnAddFeedbackDetails
            // 
            this.btnAddFeedbackDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddFeedbackDetails.BackColor = System.Drawing.Color.Green;
            this.btnAddFeedbackDetails.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnAddFeedbackDetails.FlatAppearance.BorderSize = 5;
            this.btnAddFeedbackDetails.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddFeedbackDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFeedbackDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddFeedbackDetails.Location = new System.Drawing.Point(8, 14);
            this.btnAddFeedbackDetails.Margin = new System.Windows.Forms.Padding(1);
            this.btnAddFeedbackDetails.Name = "btnAddFeedbackDetails";
            this.btnAddFeedbackDetails.Size = new System.Drawing.Size(222, 24);
            this.btnAddFeedbackDetails.TabIndex = 0;
            this.btnAddFeedbackDetails.Tag = "";
            this.btnAddFeedbackDetails.Text = "+&Add Emp FeedBack Details";
            this.btnAddFeedbackDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddFeedbackDetails.UseVisualStyleBackColor = false;
            this.btnAddFeedbackDetails.Click += new System.EventHandler(this.btnAddFeedbackDetails_Click);
            // 
            // cbProgramName
            // 
            this.cbProgramName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProgramName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProgramName.FormattingEnabled = true;
            this.cbProgramName.Location = new System.Drawing.Point(123, 42);
            this.cbProgramName.Name = "cbProgramName";
            this.cbProgramName.Size = new System.Drawing.Size(321, 23);
            this.cbProgramName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(11, 45);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Program Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(40, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "From Date";
            // 
            // dtpPrgFromDate
            // 
            this.dtpPrgFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpPrgFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpPrgFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPrgFromDate.Location = new System.Drawing.Point(123, 14);
            this.dtpPrgFromDate.Name = "dtpPrgFromDate";
            this.dtpPrgFromDate.Size = new System.Drawing.Size(94, 22);
            this.dtpPrgFromDate.TabIndex = 1;
            this.dtpPrgFromDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpPrgFromDate.ValueChanged += new System.EventHandler(this.dtpPrgFromDate_ValueChanged);
            // 
            // ActualProgramsForFeedBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(478, 166);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ActualProgramsForFeedBack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Program FeedBack Form";
            this.Load += new System.EventHandler(this.ActualProgramsForFeedBack_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpPrgFromDate;
        private System.Windows.Forms.ComboBox cbProgramName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddFeedbackDetails;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpPrgToDate;
    }
}