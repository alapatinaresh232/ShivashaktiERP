using GroupCtrl;
namespace SSCRM
{
    partial class frmShortCourse
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
            this.txtYearpass = new System.Windows.Forms.TextBox();
            this.txtPerofMarks_percent_optional = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.txtInstLocation = new System.Windows.Forms.TextBox();
            this.txtInstName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCoursename = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
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
            this.grouper1.Controls.Add(this.txtYearpass);
            this.grouper1.Controls.Add(this.txtPerofMarks_percent_optional);
            this.grouper1.Controls.Add(this.label7);
            this.grouper1.Controls.Add(this.txtSubject);
            this.grouper1.Controls.Add(this.txtDuration);
            this.grouper1.Controls.Add(this.txtInstLocation);
            this.grouper1.Controls.Add(this.txtInstName);
            this.grouper1.Controls.Add(this.label6);
            this.grouper1.Controls.Add(this.label5);
            this.grouper1.Controls.Add(this.label3);
            this.grouper1.Controls.Add(this.label2);
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.Controls.Add(this.txtCoursename);
            this.grouper1.Controls.Add(this.label4);
            this.grouper1.Controls.Add(this.btnClose);
            this.grouper1.Controls.Add(this.btnSave);
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
            this.grouper1.Size = new System.Drawing.Size(379, 242);
            this.grouper1.TabIndex = 2;
            // 
            // txtYearpass
            // 
            this.txtYearpass.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYearpass.Location = new System.Drawing.Point(173, 47);
            this.txtYearpass.MaxLength = 4;
            this.txtYearpass.Name = "txtYearpass";
            this.txtYearpass.Size = new System.Drawing.Size(60, 22);
            this.txtYearpass.TabIndex = 1;
            // 
            // txtPerofMarks_percent_optional
            // 
            this.txtPerofMarks_percent_optional.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPerofMarks_percent_optional.Location = new System.Drawing.Point(173, 170);
            this.txtPerofMarks_percent_optional.Name = "txtPerofMarks_percent_optional";
            this.txtPerofMarks_percent_optional.Size = new System.Drawing.Size(60, 22);
            this.txtPerofMarks_percent_optional.TabIndex = 6;
            this.txtPerofMarks_percent_optional.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(87, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 14);
            this.label7.TabIndex = 25;
            this.label7.Text = "% of Marks";
            this.label7.Visible = false;
            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.Location = new System.Drawing.Point(173, 121);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(171, 22);
            this.txtSubject.TabIndex = 4;
            // 
            // txtDuration
            // 
            this.txtDuration.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDuration.Location = new System.Drawing.Point(173, 145);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(104, 22);
            this.txtDuration.TabIndex = 5;
            // 
            // txtInstLocation
            // 
            this.txtInstLocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInstLocation.Location = new System.Drawing.Point(173, 97);
            this.txtInstLocation.Name = "txtInstLocation";
            this.txtInstLocation.Size = new System.Drawing.Size(171, 22);
            this.txtInstLocation.TabIndex = 3;
            // 
            // txtInstName
            // 
            this.txtInstName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInstName.Location = new System.Drawing.Point(173, 72);
            this.txtInstName.Name = "txtInstName";
            this.txtInstName.Size = new System.Drawing.Size(171, 22);
            this.txtInstName.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(105, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 14);
            this.label6.TabIndex = 17;
            this.label6.Text = "Duration";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(105, 124);
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
            this.label3.Location = new System.Drawing.Point(34, 102);
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
            this.label2.Location = new System.Drawing.Point(50, 76);
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
            this.label1.Location = new System.Drawing.Point(77, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 14);
            this.label1.TabIndex = 13;
            this.label1.Text = "Course Name";
            // 
            // txtCoursename
            // 
            this.txtCoursename.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCoursename.Location = new System.Drawing.Point(173, 23);
            this.txtCoursename.Name = "txtCoursename";
            this.txtCoursename.Size = new System.Drawing.Size(171, 22);
            this.txtCoursename.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(60, 50);
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
            this.btnClose.Location = new System.Drawing.Point(189, 207);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 23);
            this.btnClose.TabIndex = 8;
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
            this.btnSave.Location = new System.Drawing.Point(124, 207);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmShortCourse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 232);
            this.ControlBox = false;
            this.Controls.Add(this.grouper1);
            this.Name = "frmShortCourse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Short Course";
            this.Load += new System.EventHandler(this.frmShortCourse_Load);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Grouper grouper1;
        private System.Windows.Forms.TextBox txtYearpass;
        private System.Windows.Forms.TextBox txtPerofMarks_percent_optional;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.TextBox txtInstLocation;
        private System.Windows.Forms.TextBox txtInstName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCoursename;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}