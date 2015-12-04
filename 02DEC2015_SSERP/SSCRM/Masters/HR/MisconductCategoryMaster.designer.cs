namespace SSCRM
{
    partial class MisconductCategoryMaster
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
            this.cbSourceName = new System.Windows.Forms.ComboBox();
            this.chkMisConHeadName = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtMisConDetailName = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblVehModel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMisConHeadName = new System.Windows.Forms.TextBox();
            this.lstMappedMisconductDetails = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblVehMake = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbSourceName
            // 
            this.cbSourceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSourceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSourceName.FormattingEnabled = true;
            this.cbSourceName.Location = new System.Drawing.Point(140, 28);
            this.cbSourceName.Name = "cbSourceName";
            this.cbSourceName.Size = new System.Drawing.Size(224, 24);
            this.cbSourceName.TabIndex = 69;
            this.cbSourceName.SelectedIndexChanged += new System.EventHandler(this.cbSourceName_SelectedIndexChanged);
            // 
            // chkMisConHeadName
            // 
            this.chkMisConHeadName.AutoSize = true;
            this.chkMisConHeadName.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMisConHeadName.ForeColor = System.Drawing.Color.OrangeRed;
            this.chkMisConHeadName.Location = new System.Drawing.Point(369, 31);
            this.chkMisConHeadName.Name = "chkMisConHeadName";
            this.chkMisConHeadName.Size = new System.Drawing.Size(15, 14);
            this.chkMisConHeadName.TabIndex = 67;
            this.chkMisConHeadName.UseVisualStyleBackColor = true;
            this.chkMisConHeadName.CheckedChanged += new System.EventHandler(this.chkMisConHeadName_CheckedChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.AutoEllipsis = true;
            this.btnAdd.BackColor = System.Drawing.Color.LightCyan;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(367, 54);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(61, 25);
            this.btnAdd.TabIndex = 61;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtMisConDetailName
            // 
            this.txtMisConDetailName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMisConDetailName.Location = new System.Drawing.Point(140, 56);
            this.txtMisConDetailName.Name = "txtMisConDetailName";
            this.txtMisConDetailName.Size = new System.Drawing.Size(224, 22);
            this.txtMisConDetailName.TabIndex = 66;
            this.txtMisConDetailName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMisConDetailName_KeyPress);
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
            // lblVehModel
            // 
            this.lblVehModel.AutoSize = true;
            this.lblVehModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVehModel.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblVehModel.Location = new System.Drawing.Point(42, 57);
            this.lblVehModel.Margin = new System.Windows.Forms.Padding(0);
            this.lblVehModel.Name = "lblVehModel";
            this.lblVehModel.Size = new System.Drawing.Size(94, 16);
            this.lblVehModel.TabIndex = 64;
            this.lblVehModel.Text = "Detail Name";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.cbSourceName);
            this.groupBox1.Controls.Add(this.txtMisConHeadName);
            this.groupBox1.Controls.Add(this.chkMisConHeadName);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.txtMisConDetailName);
            this.groupBox1.Controls.Add(this.lblVehModel);
            this.groupBox1.Controls.Add(this.lstMappedMisconductDetails);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.lblVehMake);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 443);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Misconduct Category Master";
            // 
            // txtMisConHeadName
            // 
            this.txtMisConHeadName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMisConHeadName.Location = new System.Drawing.Point(140, 28);
            this.txtMisConHeadName.Name = "txtMisConHeadName";
            this.txtMisConHeadName.Size = new System.Drawing.Size(223, 22);
            this.txtMisConHeadName.TabIndex = 68;
            this.txtMisConHeadName.Visible = false;
            this.txtMisConHeadName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMisConHeadName_KeyPress);
            // 
            // lstMappedMisconductDetails
            // 
            this.lstMappedMisconductDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstMappedMisconductDetails.ForeColor = System.Drawing.Color.Black;
            this.lstMappedMisconductDetails.FormattingEnabled = true;
            this.lstMappedMisconductDetails.ItemHeight = 15;
            this.lstMappedMisconductDetails.Location = new System.Drawing.Point(4, 82);
            this.lstMappedMisconductDetails.Name = "lstMappedMisconductDetails";
            this.lstMappedMisconductDetails.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstMappedMisconductDetails.Size = new System.Drawing.Size(427, 319);
            this.lstMappedMisconductDetails.TabIndex = 60;
            this.lstMappedMisconductDetails.DoubleClick += new System.EventHandler(this.lstMappedMisconductDetails_DoubleClick);
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
            // lblVehMake
            // 
            this.lblVehMake.AutoSize = true;
            this.lblVehMake.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVehMake.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblVehMake.Location = new System.Drawing.Point(35, 29);
            this.lblVehMake.Margin = new System.Windows.Forms.Padding(0);
            this.lblVehMake.Name = "lblVehMake";
            this.lblVehMake.Size = new System.Drawing.Size(102, 16);
            this.lblVehMake.TabIndex = 63;
            this.lblVehMake.Text = "Source Name";
            // 
            // MisconductCategoryMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(440, 446);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MisconductCategoryMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Misconduct Category Master";
            this.Load += new System.EventHandler(this.MisconductCategoryMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSourceName;
        private System.Windows.Forms.CheckBox chkMisConHeadName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtMisConDetailName;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblVehModel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMisConHeadName;
        private System.Windows.Forms.ListBox lstMappedMisconductDetails;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblVehMake;
    }
}