namespace SSCRM
{
    partial class RecruitmentSourceMaster
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
            this.txtSourceName = new System.Windows.Forms.TextBox();
            this.chkSourceName = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbSourceName = new System.Windows.Forms.ComboBox();
            this.txtDetialName = new System.Windows.Forms.TextBox();
            this.lblVehModel = new System.Windows.Forms.Label();
            this.lstMappedSourceNames = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblVehMake = new System.Windows.Forms.Label();
            this.cbVehMake = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSourceName
            // 
            this.txtSourceName.Location = new System.Drawing.Point(140, 28);
            this.txtSourceName.Name = "txtSourceName";
            this.txtSourceName.Size = new System.Drawing.Size(223, 23);
            this.txtSourceName.TabIndex = 68;
            this.txtSourceName.Visible = false;
            // 
            // chkSourceName
            // 
            this.chkSourceName.AutoSize = true;
            this.chkSourceName.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSourceName.ForeColor = System.Drawing.Color.OrangeRed;
            this.chkSourceName.Location = new System.Drawing.Point(369, 31);
            this.chkSourceName.Name = "chkSourceName";
            this.chkSourceName.Size = new System.Drawing.Size(15, 14);
            this.chkSourceName.TabIndex = 67;
            this.chkSourceName.UseVisualStyleBackColor = true;
            this.chkSourceName.CheckedChanged += new System.EventHandler(this.chkSourceName_CheckedChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.AutoEllipsis = true;
            this.btnAdd.BackColor = System.Drawing.Color.LightCyan;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(367, 54);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(61, 25);
            this.btnAdd.TabIndex = 61;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.cbSourceName);
            this.groupBox1.Controls.Add(this.txtSourceName);
            this.groupBox1.Controls.Add(this.chkSourceName);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.txtDetialName);
            this.groupBox1.Controls.Add(this.lblVehModel);
            this.groupBox1.Controls.Add(this.lstMappedSourceNames);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.lblVehMake);
            this.groupBox1.Controls.Add(this.cbVehMake);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 443);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recruitment Master";
            // 
            // cbSourceName
            // 
            this.cbSourceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSourceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSourceName.FormattingEnabled = true;
            this.cbSourceName.Location = new System.Drawing.Point(140, 26);
            this.cbSourceName.Name = "cbSourceName";
            this.cbSourceName.Size = new System.Drawing.Size(222, 24);
            this.cbSourceName.TabIndex = 69;
            this.cbSourceName.SelectedIndexChanged += new System.EventHandler(this.cbSourceName_SelectedIndexChanged);
            // 
            // txtDetialName
            // 
            this.txtDetialName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetialName.Location = new System.Drawing.Point(139, 55);
            this.txtDetialName.Name = "txtDetialName";
            this.txtDetialName.Size = new System.Drawing.Size(224, 24);
            this.txtDetialName.TabIndex = 66;
            // 
            // lblVehModel
            // 
            this.lblVehModel.AutoSize = true;
            this.lblVehModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVehModel.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblVehModel.Location = new System.Drawing.Point(51, 56);
            this.lblVehModel.Margin = new System.Windows.Forms.Padding(0);
            this.lblVehModel.Name = "lblVehModel";
            this.lblVehModel.Size = new System.Drawing.Size(87, 15);
            this.lblVehModel.TabIndex = 64;
            this.lblVehModel.Text = "Detail Name";
            // 
            // lstMappedSourceNames
            // 
            this.lstMappedSourceNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstMappedSourceNames.ForeColor = System.Drawing.Color.Black;
            this.lstMappedSourceNames.FormattingEnabled = true;
            this.lstMappedSourceNames.ItemHeight = 15;
            this.lstMappedSourceNames.Location = new System.Drawing.Point(4, 82);
            this.lstMappedSourceNames.Name = "lstMappedSourceNames";
            this.lstMappedSourceNames.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstMappedSourceNames.Size = new System.Drawing.Size(427, 319);
            this.lstMappedSourceNames.TabIndex = 60;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Location = new System.Drawing.Point(103, 396);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(228, 40);
            this.groupBox2.TabIndex = 65;
            this.groupBox2.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.AutoEllipsis = true;
            this.btnClose.BackColor = System.Drawing.Color.LightCyan;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.btnClose.FlatAppearance.BorderSize = 5;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Location = new System.Drawing.Point(85, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(59, 25);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblVehMake
            // 
            this.lblVehMake.AutoSize = true;
            this.lblVehMake.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVehMake.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblVehMake.Location = new System.Drawing.Point(44, 28);
            this.lblVehMake.Margin = new System.Windows.Forms.Padding(0);
            this.lblVehMake.Name = "lblVehMake";
            this.lblVehMake.Size = new System.Drawing.Size(94, 15);
            this.lblVehMake.TabIndex = 63;
            this.lblVehMake.Text = "Source Name";
            // 
            // cbVehMake
            // 
            this.cbVehMake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVehMake.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbVehMake.FormattingEnabled = true;
            this.cbVehMake.Location = new System.Drawing.Point(140, 56);
            this.cbVehMake.Name = "cbVehMake";
            this.cbVehMake.Size = new System.Drawing.Size(223, 23);
            this.cbVehMake.TabIndex = 59;
            // 
            // RecruitmentSourceMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(440, 446);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "RecruitmentSourceMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Recruitment Source Master";
            this.Load += new System.EventHandler(this.HRRecruitmentMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSourceName;
        private System.Windows.Forms.CheckBox chkSourceName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDetialName;
        private System.Windows.Forms.Label lblVehModel;
        private System.Windows.Forms.ListBox lstMappedSourceNames;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblVehMake;
        private System.Windows.Forms.ComboBox cbVehMake;
        private System.Windows.Forms.ComboBox cbSourceName;

    }
}