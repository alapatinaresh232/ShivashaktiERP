namespace SSCRM
{
    partial class FixedAssetMaster
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
            this.cbAssetType = new System.Windows.Forms.ComboBox();
            this.txtAssetType = new System.Windows.Forms.TextBox();
            this.chkAssetType = new System.Windows.Forms.CheckBox();
            this.cbAssetMake = new System.Windows.Forms.ComboBox();
            this.txtAssetMake = new System.Windows.Forms.TextBox();
            this.chkAssetMake = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtDsearch = new System.Windows.Forms.TextBox();
            this.lblVehModel = new System.Windows.Forms.Label();
            this.lblVehMake = new System.Windows.Forms.Label();
            this.lblVehType = new System.Windows.Forms.Label();
            this.lstMappedAssets = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.cbAssetType);
            this.groupBox1.Controls.Add(this.txtAssetType);
            this.groupBox1.Controls.Add(this.chkAssetType);
            this.groupBox1.Controls.Add(this.cbAssetMake);
            this.groupBox1.Controls.Add(this.txtAssetMake);
            this.groupBox1.Controls.Add(this.chkAssetMake);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.txtDsearch);
            this.groupBox1.Controls.Add(this.lblVehModel);
            this.groupBox1.Controls.Add(this.lblVehMake);
            this.groupBox1.Controls.Add(this.lblVehType);
            this.groupBox1.Controls.Add(this.lstMappedAssets);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 483);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Asset Master";
            // 
            // cbAssetType
            // 
            this.cbAssetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAssetType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAssetType.FormattingEnabled = true;
            this.cbAssetType.Location = new System.Drawing.Point(141, 21);
            this.cbAssetType.Name = "cbAssetType";
            this.cbAssetType.Size = new System.Drawing.Size(223, 24);
            this.cbAssetType.TabIndex = 1;
            this.cbAssetType.SelectedIndexChanged += new System.EventHandler(this.cbAssetType_SelectedIndexChanged);
            // 
            // txtAssetType
            // 
            this.txtAssetType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAssetType.Location = new System.Drawing.Point(142, 21);
            this.txtAssetType.Name = "txtAssetType";
            this.txtAssetType.Size = new System.Drawing.Size(223, 22);
            this.txtAssetType.TabIndex = 83;
            this.txtAssetType.Visible = false;
            this.txtAssetType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAssetType_KeyPress);
            // 
            // chkAssetType
            // 
            this.chkAssetType.AutoSize = true;
            this.chkAssetType.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAssetType.ForeColor = System.Drawing.Color.OrangeRed;
            this.chkAssetType.Location = new System.Drawing.Point(371, 23);
            this.chkAssetType.Name = "chkAssetType";
            this.chkAssetType.Size = new System.Drawing.Size(15, 14);
            this.chkAssetType.TabIndex = 2;
            this.chkAssetType.UseVisualStyleBackColor = true;
            this.chkAssetType.Visible = false;
            this.chkAssetType.CheckedChanged += new System.EventHandler(this.chkAssetType_CheckedChanged);
            // 
            // cbAssetMake
            // 
            this.cbAssetMake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAssetMake.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAssetMake.FormattingEnabled = true;
            this.cbAssetMake.Location = new System.Drawing.Point(141, 49);
            this.cbAssetMake.Name = "cbAssetMake";
            this.cbAssetMake.Size = new System.Drawing.Size(223, 24);
            this.cbAssetMake.TabIndex = 4;
            this.cbAssetMake.SelectedIndexChanged += new System.EventHandler(this.cbAssetMake_SelectedIndexChanged);
            // 
            // txtAssetMake
            // 
            this.txtAssetMake.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAssetMake.Location = new System.Drawing.Point(142, 48);
            this.txtAssetMake.Name = "txtAssetMake";
            this.txtAssetMake.Size = new System.Drawing.Size(223, 22);
            this.txtAssetMake.TabIndex = 78;
            this.txtAssetMake.Visible = false;
            this.txtAssetMake.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAssetMake_KeyPress);
            // 
            // chkAssetMake
            // 
            this.chkAssetMake.AutoSize = true;
            this.chkAssetMake.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAssetMake.ForeColor = System.Drawing.Color.OrangeRed;
            this.chkAssetMake.Location = new System.Drawing.Point(372, 52);
            this.chkAssetMake.Name = "chkAssetMake";
            this.chkAssetMake.Size = new System.Drawing.Size(15, 14);
            this.chkAssetMake.TabIndex = 5;
            this.chkAssetMake.UseVisualStyleBackColor = true;
            this.chkAssetMake.Visible = false;
            this.chkAssetMake.CheckedChanged += new System.EventHandler(this.chkAssetMake_CheckedChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.AutoEllipsis = true;
            this.btnAdd.BackColor = System.Drawing.Color.LightCyan;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(372, 74);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(61, 25);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtDsearch
            // 
            this.txtDsearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDsearch.Location = new System.Drawing.Point(142, 75);
            this.txtDsearch.Name = "txtDsearch";
            this.txtDsearch.Size = new System.Drawing.Size(223, 22);
            this.txtDsearch.TabIndex = 7;
            this.txtDsearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDsearch_KeyPress);
            // 
            // lblVehModel
            // 
            this.lblVehModel.AutoSize = true;
            this.lblVehModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVehModel.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblVehModel.Location = new System.Drawing.Point(44, 78);
            this.lblVehModel.Margin = new System.Windows.Forms.Padding(0);
            this.lblVehModel.Name = "lblVehModel";
            this.lblVehModel.Size = new System.Drawing.Size(92, 16);
            this.lblVehModel.TabIndex = 6;
            this.lblVehModel.Text = "Asset Name";
            // 
            // lblVehMake
            // 
            this.lblVehMake.AutoSize = true;
            this.lblVehMake.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVehMake.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblVehMake.Location = new System.Drawing.Point(47, 49);
            this.lblVehMake.Margin = new System.Windows.Forms.Padding(0);
            this.lblVehMake.Name = "lblVehMake";
            this.lblVehMake.Size = new System.Drawing.Size(89, 16);
            this.lblVehMake.TabIndex = 3;
            this.lblVehMake.Text = "Asset Make";
            // 
            // lblVehType
            // 
            this.lblVehType.AutoSize = true;
            this.lblVehType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVehType.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblVehType.Location = new System.Drawing.Point(51, 21);
            this.lblVehType.Margin = new System.Windows.Forms.Padding(0);
            this.lblVehType.Name = "lblVehType";
            this.lblVehType.Size = new System.Drawing.Size(87, 16);
            this.lblVehType.TabIndex = 0;
            this.lblVehType.Text = "Asset Type";
            // 
            // lstMappedAssets
            // 
            this.lstMappedAssets.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstMappedAssets.ForeColor = System.Drawing.Color.Black;
            this.lstMappedAssets.FormattingEnabled = true;
            this.lstMappedAssets.ItemHeight = 15;
            this.lstMappedAssets.Location = new System.Drawing.Point(6, 102);
            this.lstMappedAssets.Name = "lstMappedAssets";
            this.lstMappedAssets.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstMappedAssets.Size = new System.Drawing.Size(431, 334);
            this.lstMappedAssets.TabIndex = 9;
            this.lstMappedAssets.DoubleClick += new System.EventHandler(this.lstMappedAssets_DoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Location = new System.Drawing.Point(109, 438);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(228, 40);
            this.groupBox2.TabIndex = 10;
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
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FixedAssetMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(452, 488);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FixedAssetMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fixed Asset Master";
            this.Load += new System.EventHandler(this.FixedAssetMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAssetMake;
        private System.Windows.Forms.CheckBox chkAssetMake;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtDsearch;
        private System.Windows.Forms.Label lblVehModel;
        private System.Windows.Forms.Label lblVehMake;
        private System.Windows.Forms.Label lblVehType;
        private System.Windows.Forms.ListBox lstMappedAssets;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbAssetMake;
        private System.Windows.Forms.CheckBox chkAssetType;
        private System.Windows.Forms.TextBox txtAssetType;
        private System.Windows.Forms.ComboBox cbAssetType;
    }
}