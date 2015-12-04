namespace SSCRM
{
    partial class VehicleMaster
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
            this.txtVehMake = new System.Windows.Forms.TextBox();
            this.chkVehMake = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtDsearch = new System.Windows.Forms.TextBox();
            this.lblVehModel = new System.Windows.Forms.Label();
            this.lstMappedBranches = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblVehMake = new System.Windows.Forms.Label();
            this.cbVehMake = new System.Windows.Forms.ComboBox();
            this.lblVehType = new System.Windows.Forms.Label();
            this.cbVehType = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.txtVehMake);
            this.groupBox1.Controls.Add(this.chkVehMake);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.txtDsearch);
            this.groupBox1.Controls.Add(this.lblVehModel);
            this.groupBox1.Controls.Add(this.lstMappedBranches);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.lblVehMake);
            this.groupBox1.Controls.Add(this.cbVehMake);
            this.groupBox1.Controls.Add(this.lblVehType);
            this.groupBox1.Controls.Add(this.cbVehType);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(2, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 470);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vehicle Master";
            // 
            // txtVehMake
            // 
            this.txtVehMake.Location = new System.Drawing.Point(139, 55);
            this.txtVehMake.Name = "txtVehMake";
            this.txtVehMake.Size = new System.Drawing.Size(223, 23);
            this.txtVehMake.TabIndex = 68;
            this.txtVehMake.Visible = false;
            // 
            // chkVehMake
            // 
            this.chkVehMake.AutoSize = true;
            this.chkVehMake.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkVehMake.ForeColor = System.Drawing.Color.OrangeRed;
            this.chkVehMake.Location = new System.Drawing.Point(369, 61);
            this.chkVehMake.Name = "chkVehMake";
            this.chkVehMake.Size = new System.Drawing.Size(15, 14);
            this.chkVehMake.TabIndex = 67;
            this.chkVehMake.UseVisualStyleBackColor = true;
            this.chkVehMake.Visible = false;
            this.chkVehMake.CheckedChanged += new System.EventHandler(this.chkVehMake_CheckedChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.AutoEllipsis = true;
            this.btnAdd.BackColor = System.Drawing.Color.LightCyan;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(369, 83);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(61, 25);
            this.btnAdd.TabIndex = 61;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtDsearch
            // 
            this.txtDsearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDsearch.Location = new System.Drawing.Point(139, 82);
            this.txtDsearch.Name = "txtDsearch";
            this.txtDsearch.Size = new System.Drawing.Size(223, 24);
            this.txtDsearch.TabIndex = 66;
            // 
            // lblVehModel
            // 
            this.lblVehModel.AutoSize = true;
            this.lblVehModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVehModel.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblVehModel.Location = new System.Drawing.Point(41, 86);
            this.lblVehModel.Margin = new System.Windows.Forms.Padding(0);
            this.lblVehModel.Name = "lblVehModel";
            this.lblVehModel.Size = new System.Drawing.Size(96, 15);
            this.lblVehModel.TabIndex = 64;
            this.lblVehModel.Text = "Vehicle Name";
            // 
            // lstMappedBranches
            // 
            this.lstMappedBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstMappedBranches.ForeColor = System.Drawing.Color.Black;
            this.lstMappedBranches.FormattingEnabled = true;
            this.lstMappedBranches.ItemHeight = 15;
            this.lstMappedBranches.Location = new System.Drawing.Point(3, 112);
            this.lstMappedBranches.Name = "lstMappedBranches";
            this.lstMappedBranches.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstMappedBranches.Size = new System.Drawing.Size(431, 319);
            this.lstMappedBranches.TabIndex = 60;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Location = new System.Drawing.Point(104, 426);
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
            this.lblVehMake.Location = new System.Drawing.Point(44, 58);
            this.lblVehMake.Margin = new System.Windows.Forms.Padding(0);
            this.lblVehMake.Name = "lblVehMake";
            this.lblVehMake.Size = new System.Drawing.Size(93, 15);
            this.lblVehMake.TabIndex = 63;
            this.lblVehMake.Text = "Vehicle Make";
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
            this.cbVehMake.SelectedIndexChanged += new System.EventHandler(this.cbVehMake_SelectedIndexChanged);
            // 
            // lblVehType
            // 
            this.lblVehType.AutoSize = true;
            this.lblVehType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVehType.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblVehType.Location = new System.Drawing.Point(49, 30);
            this.lblVehType.Margin = new System.Windows.Forms.Padding(0);
            this.lblVehType.Name = "lblVehType";
            this.lblVehType.Size = new System.Drawing.Size(88, 15);
            this.lblVehType.TabIndex = 62;
            this.lblVehType.Text = "Vehicle Type";
            // 
            // cbVehType
            // 
            this.cbVehType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVehType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbVehType.FormattingEnabled = true;
            this.cbVehType.Location = new System.Drawing.Point(139, 27);
            this.cbVehType.Name = "cbVehType";
            this.cbVehType.Size = new System.Drawing.Size(223, 23);
            this.cbVehType.TabIndex = 58;
            this.cbVehType.SelectedIndexChanged += new System.EventHandler(this.cbVehType_SelectedIndexChanged);
            // 
            // VehicleMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(440, 475);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "VehicleMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vehicle Master";
            this.Load += new System.EventHandler(this.VehicleMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkVehMake;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtDsearch;
        private System.Windows.Forms.Label lblVehModel;
        private System.Windows.Forms.ListBox lstMappedBranches;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblVehMake;
        private System.Windows.Forms.ComboBox cbVehMake;
        private System.Windows.Forms.Label lblVehType;
        private System.Windows.Forms.ComboBox cbVehType;
        private System.Windows.Forms.TextBox txtVehMake;
    }
}