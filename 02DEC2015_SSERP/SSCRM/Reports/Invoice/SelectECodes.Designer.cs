namespace SSCRM
{
    partial class SelectECodes
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
            this.txtEname = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtEcodeSearch = new System.Windows.Forms.TextBox();
            this.clbGLList = new System.Windows.Forms.CheckedListBox();
            this.btnEcodeAdd = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.txtEname);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txtEcodeSearch);
            this.groupBox1.Controls.Add(this.clbGLList);
            this.groupBox1.Controls.Add(this.btnEcodeAdd);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(567, 353);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ID Cards";
            // 
            // txtEname
            // 
            this.txtEname.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtEname.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEname.Location = new System.Drawing.Point(145, 17);
            this.txtEname.MaxLength = 50;
            this.txtEname.Name = "txtEname";
            this.txtEname.ReadOnly = true;
            this.txtEname.Size = new System.Drawing.Size(351, 22);
            this.txtEname.TabIndex = 55;
            this.txtEname.TabStop = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label16.Location = new System.Drawing.Point(7, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 16);
            this.label16.TabIndex = 17;
            this.label16.Text = "Ecode";
            // 
            // txtEcodeSearch
            // 
            this.txtEcodeSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcodeSearch.Location = new System.Drawing.Point(61, 17);
            this.txtEcodeSearch.MaxLength = 20;
            this.txtEcodeSearch.Name = "txtEcodeSearch";
            this.txtEcodeSearch.Size = new System.Drawing.Size(83, 21);
            this.txtEcodeSearch.TabIndex = 16;
            this.txtEcodeSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEcodeSearch_KeyUp);
            this.txtEcodeSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEcodeSearch_KeyPress);
            // 
            // clbGLList
            // 
            this.clbGLList.CheckOnClick = true;
            this.clbGLList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbGLList.FormattingEnabled = true;
            this.clbGLList.Location = new System.Drawing.Point(7, 45);
            this.clbGLList.Name = "clbGLList";
            this.clbGLList.Size = new System.Drawing.Size(553, 260);
            this.clbGLList.Sorted = true;
            this.clbGLList.TabIndex = 14;
            this.clbGLList.UseCompatibleTextRendering = true;
            // 
            // btnEcodeAdd
            // 
            this.btnEcodeAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEcodeAdd.BackColor = System.Drawing.Color.YellowGreen;
            this.btnEcodeAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnEcodeAdd.FlatAppearance.BorderSize = 5;
            this.btnEcodeAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEcodeAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEcodeAdd.Location = new System.Drawing.Point(499, 16);
            this.btnEcodeAdd.Margin = new System.Windows.Forms.Padding(1);
            this.btnEcodeAdd.Name = "btnEcodeAdd";
            this.btnEcodeAdd.Size = new System.Drawing.Size(56, 24);
            this.btnEcodeAdd.TabIndex = 2;
            this.btnEcodeAdd.Tag = "Product  Search";
            this.btnEcodeAdd.Text = "+ Add";
            this.btnEcodeAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEcodeAdd.UseVisualStyleBackColor = false;
            this.btnEcodeAdd.Click += new System.EventHandler(this.btnEcodeAdd_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnReport);
            this.groupBox3.Location = new System.Drawing.Point(108, 301);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 47);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.AliceBlue;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(136, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(220, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Location = new System.Drawing.Point(52, 11);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(78, 30);
            this.btnReport.TabIndex = 0;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // SelectECodes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(573, 360);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SelectECodes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ID Cards :: Print";
            this.Load += new System.EventHandler(this.SelectECodes_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnEcodeAdd;
        private System.Windows.Forms.CheckedListBox clbGLList;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.TextBox txtEcodeSearch;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox txtEname;
        private System.Windows.Forms.Button button1;

    }
}