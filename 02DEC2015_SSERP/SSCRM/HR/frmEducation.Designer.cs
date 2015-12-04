using GroupCtrl;
namespace SSCRM
{
    partial class frmEducation
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
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grouper1 = new GroupCtrl.Grouper();
            this.cmbTypeEdu = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtyearofpass_num = new System.Windows.Forms.TextBox();
            this.txtPerMarks_percent = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.txtUniversity = new System.Windows.Forms.TextBox();
            this.txtInstLocation = new System.Windows.Forms.TextBox();
            this.txtInst_Name = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExamPassed = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.PowderBlue;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.PowderBlue;
            this.grouper1.BackgroundGradientMode = GroupCtrl.Grouper.GroupBoxGradientMode.Vertical;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.cmbTypeEdu);
            this.grouper1.Controls.Add(this.label8);
            this.grouper1.Controls.Add(this.txtyearofpass_num);
            this.grouper1.Controls.Add(this.txtPerMarks_percent);
            this.grouper1.Controls.Add(this.label7);
            this.grouper1.Controls.Add(this.txtSubject);
            this.grouper1.Controls.Add(this.txtUniversity);
            this.grouper1.Controls.Add(this.txtInstLocation);
            this.grouper1.Controls.Add(this.txtInst_Name);
            this.grouper1.Controls.Add(this.label6);
            this.grouper1.Controls.Add(this.label5);
            this.grouper1.Controls.Add(this.label3);
            this.grouper1.Controls.Add(this.label2);
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.Controls.Add(this.txtExamPassed);
            this.grouper1.Controls.Add(this.label4);
            this.grouper1.Controls.Add(this.btnClose);
            this.grouper1.Controls.Add(this.btnSave);
            this.grouper1.Controls.Add(this.groupBox1);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "";
            this.grouper1.Location = new System.Drawing.Point(0, -10);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 1;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(365, 292);
            this.grouper1.TabIndex = 1;
            // 
            // cmbTypeEdu
            // 
            this.cmbTypeEdu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeEdu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTypeEdu.FormattingEnabled = true;
            this.cmbTypeEdu.Items.AddRange(new object[] {
            "--SELECT--",
            "SSC",
            "SSC+2 ",
            "GRADUATION",
            "POST GRADUATION",
            "ENGINEERING",
            "MEDICINE"});
            this.cmbTypeEdu.Location = new System.Drawing.Point(171, 51);
            this.cmbTypeEdu.Name = "cmbTypeEdu";
            this.cmbTypeEdu.Size = new System.Drawing.Size(171, 23);
            this.cmbTypeEdu.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(22, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 14);
            this.label8.TabIndex = 27;
            this.label8.Text = "Type of Examination";
            // 
            // txtyearofpass_num
            // 
            this.txtyearofpass_num.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtyearofpass_num.Location = new System.Drawing.Point(171, 78);
            this.txtyearofpass_num.MaxLength = 4;
            this.txtyearofpass_num.Name = "txtyearofpass_num";
            this.txtyearofpass_num.Size = new System.Drawing.Size(56, 22);
            this.txtyearofpass_num.TabIndex = 2;
            this.txtyearofpass_num.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtyearofpass_num_KeyPress);
            // 
            // txtPerMarks_percent
            // 
            this.txtPerMarks_percent.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPerMarks_percent.Location = new System.Drawing.Point(171, 201);
            this.txtPerMarks_percent.Name = "txtPerMarks_percent";
            this.txtPerMarks_percent.Size = new System.Drawing.Size(43, 22);
            this.txtPerMarks_percent.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(77, 205);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 14);
            this.label7.TabIndex = 25;
            this.label7.Text = "% of Marks";
            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.Location = new System.Drawing.Point(171, 152);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(171, 22);
            this.txtSubject.TabIndex = 5;
            // 
            // txtUniversity
            // 
            this.txtUniversity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUniversity.Location = new System.Drawing.Point(171, 176);
            this.txtUniversity.Name = "txtUniversity";
            this.txtUniversity.Size = new System.Drawing.Size(171, 22);
            this.txtUniversity.TabIndex = 6;
            // 
            // txtInstLocation
            // 
            this.txtInstLocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInstLocation.Location = new System.Drawing.Point(171, 128);
            this.txtInstLocation.Name = "txtInstLocation";
            this.txtInstLocation.Size = new System.Drawing.Size(171, 22);
            this.txtInstLocation.TabIndex = 4;
            // 
            // txtInst_Name
            // 
            this.txtInst_Name.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInst_Name.Location = new System.Drawing.Point(171, 103);
            this.txtInst_Name.Name = "txtInst_Name";
            this.txtInst_Name.Size = new System.Drawing.Size(171, 22);
            this.txtInst_Name.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(86, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 14);
            this.label6.TabIndex = 17;
            this.label6.Text = "University";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(95, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 14);
            this.label5.TabIndex = 16;
            this.label5.Text = "Subjects";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(24, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 14);
            this.label3.TabIndex = 15;
            this.label3.Text = "Institution Location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(40, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 14);
            this.label2.TabIndex = 14;
            this.label2.Text = "Institution Name ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(24, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 14);
            this.label1.TabIndex = 13;
            this.label1.Text = "Examination Passed";
            // 
            // txtExamPassed
            // 
            this.txtExamPassed.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExamPassed.Location = new System.Drawing.Point(171, 23);
            this.txtExamPassed.Name = "txtExamPassed";
            this.txtExamPassed.Size = new System.Drawing.Size(171, 22);
            this.txtExamPassed.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(50, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 14);
            this.label4.TabIndex = 11;
            this.label4.Text = "Year of Passing";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.YellowGreen;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(193, 244);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 25);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.YellowGreen;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(117, 244);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 25);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(4, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 280);
            this.groupBox1.TabIndex = 71;
            this.groupBox1.TabStop = false;
            // 
            // frmEducation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 281);
            this.ControlBox = false;
            this.Controls.Add(this.grouper1);
            this.Name = "frmEducation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Education";
            this.Load += new System.EventHandler(this.frmEducation_Load);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Grouper grouper1;
        private System.Windows.Forms.TextBox txtInstLocation;
        private System.Windows.Forms.TextBox txtInst_Name;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExamPassed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtyearofpass_num;
        private System.Windows.Forms.TextBox txtPerMarks_percent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.TextBox txtUniversity;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cmbTypeEdu;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;


    }
}