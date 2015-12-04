namespace SSCRM
{
    partial class frmEditingInfo
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
            this.btnReport = new System.Windows.Forms.Button();
            this.rbtAgent = new System.Windows.Forms.RadioButton();
            this.rbtEmployee = new System.Windows.Forms.RadioButton();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtEoraCode_num = new System.Windows.Forms.TextBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grouper1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.PowderBlue;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouper1.BackgroundGradientMode = GroupCtrl.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.btnReport);
            this.grouper1.Controls.Add(this.rbtAgent);
            this.grouper1.Controls.Add(this.rbtEmployee);
            this.grouper1.Controls.Add(this.btnClose);
            this.grouper1.Controls.Add(this.label18);
            this.grouper1.Controls.Add(this.txtEoraCode_num);
            this.grouper1.Controls.Add(this.btnEdit);
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
            this.grouper1.Size = new System.Drawing.Size(379, 225);
            this.grouper1.TabIndex = 0;
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnReport.ForeColor = System.Drawing.Color.Navy;
            this.btnReport.Location = new System.Drawing.Point(151, 178);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 26);
            this.btnReport.TabIndex = 38;
            this.btnReport.Text = "View";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Visible = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // rbtAgent
            // 
            this.rbtAgent.AutoSize = true;
            this.rbtAgent.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.rbtAgent.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.rbtAgent.Location = new System.Drawing.Point(203, 107);
            this.rbtAgent.Name = "rbtAgent";
            this.rbtAgent.Size = new System.Drawing.Size(63, 18);
            this.rbtAgent.TabIndex = 2;
            this.rbtAgent.TabStop = true;
            this.rbtAgent.Text = "Agent";
            this.rbtAgent.UseVisualStyleBackColor = true;
            this.rbtAgent.CheckedChanged += new System.EventHandler(this.rbtAgent_CheckedChanged);
            // 
            // rbtEmployee
            // 
            this.rbtEmployee.AutoSize = true;
            this.rbtEmployee.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.rbtEmployee.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.rbtEmployee.Location = new System.Drawing.Point(112, 107);
            this.rbtEmployee.Name = "rbtEmployee";
            this.rbtEmployee.Size = new System.Drawing.Size(83, 18);
            this.rbtEmployee.TabIndex = 1;
            this.rbtEmployee.TabStop = true;
            this.rbtEmployee.Text = "Employee";
            this.rbtEmployee.UseVisualStyleBackColor = true;
            this.rbtEmployee.CheckedChanged += new System.EventHandler(this.rbtEmployee_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.Navy;
            this.btnClose.Location = new System.Drawing.Point(229, 178);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 26);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblResult.Location = new System.Drawing.Point(102, 130);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(0, 16);
            this.lblResult.TabIndex = 37;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label18.Location = new System.Drawing.Point(57, 68);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(137, 14);
            this.label18.TabIndex = 36;
            this.label18.Text = "Enter Application No.";
            // 
            // txtEoraCode_num
            // 
            this.txtEoraCode_num.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEoraCode_num.Location = new System.Drawing.Point(200, 65);
            this.txtEoraCode_num.Name = "txtEoraCode_num";
            this.txtEoraCode_num.Size = new System.Drawing.Size(121, 22);
            this.txtEoraCode_num.TabIndex = 0;
            this.txtEoraCode_num.TextChanged += new System.EventHandler(this.txtNominee_num_TextChanged);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.Navy;
            this.btnEdit.Location = new System.Drawing.Point(73, 178);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 26);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.lblResult);
            this.groupBox1.Location = new System.Drawing.Point(6, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 205);
            this.groupBox1.TabIndex = 71;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(54, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(261, 42);
            this.groupBox2.TabIndex = 71;
            this.groupBox2.TabStop = false;
            // 
            // frmEditingInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 211);
            this.ControlBox = false;
            this.Controls.Add(this.grouper1);
            this.Name = "frmEditingInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editing Information";
            this.Load += new System.EventHandler(this.frmEditingInfo_Load);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupCtrl.Grouper grouper1;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtEoraCode_num;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.RadioButton rbtAgent;
        private System.Windows.Forms.RadioButton rbtEmployee;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}