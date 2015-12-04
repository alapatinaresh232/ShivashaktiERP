namespace SDMS
{
    partial class BankDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankDetails));
            this.label1 = new System.Windows.Forms.Label();
            this.txtBankVillSearch = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBankHNo = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.txtBankVill = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.txtBankLandMark = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.txtBankDistricit = new System.Windows.Forms.TextBox();
            this.txtBankPin = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.txtBankMandal = new System.Windows.Forms.TextBox();
            this.txtBankState = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.btnBankVSearch = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.grouper1 = new System.Windows.Forms.GroupBox();
            this.txtPhoneNo = new System.Windows.Forms.TextBox();
            this.cbBankName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cbBankVill = new MultiColumnComboBoxDemo.MultiColumnComboBox();
            this.grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(65, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 15);
            this.label1.TabIndex = 177;
            this.label1.Text = "Bank Name";
            // 
            // txtBankVillSearch
            // 
            this.txtBankVillSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBankVillSearch.Location = new System.Drawing.Point(151, 92);
            this.txtBankVillSearch.MaxLength = 50;
            this.txtBankVillSearch.Name = "txtBankVillSearch";
            this.txtBankVillSearch.Size = new System.Drawing.Size(75, 21);
            this.txtBankVillSearch.TabIndex = 183;
            this.txtBankVillSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBankVillSearch_KeyUp);
            this.txtBankVillSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBankVillSearch_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label11.Location = new System.Drawing.Point(46, 94);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 15);
            this.label11.TabIndex = 196;
            this.label11.Text = "Village Search";
            // 
            // txtBankHNo
            // 
            this.txtBankHNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBankHNo.Location = new System.Drawing.Point(151, 44);
            this.txtBankHNo.MaxLength = 50;
            this.txtBankHNo.Name = "txtBankHNo";
            this.txtBankHNo.Size = new System.Drawing.Size(131, 21);
            this.txtBankHNo.TabIndex = 181;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label37.Location = new System.Drawing.Point(76, 47);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(70, 15);
            this.label37.TabIndex = 186;
            this.label37.Text = "House No";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label36.Location = new System.Drawing.Point(57, 116);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(89, 15);
            this.label36.TabIndex = 187;
            this.label36.Text = "Village/Town";
            // 
            // txtBankVill
            // 
            this.txtBankVill.BackColor = System.Drawing.SystemColors.Info;
            this.txtBankVill.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBankVill.Location = new System.Drawing.Point(151, 114);
            this.txtBankVill.MaxLength = 20;
            this.txtBankVill.Name = "txtBankVill";
            this.txtBankVill.ReadOnly = true;
            this.txtBankVill.Size = new System.Drawing.Size(133, 21);
            this.txtBankVill.TabIndex = 184;
            this.txtBankVill.TabStop = false;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label35.Location = new System.Drawing.Point(71, 71);
            this.label35.Margin = new System.Windows.Forms.Padding(0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(75, 15);
            this.label35.TabIndex = 188;
            this.label35.Text = "Land Mark";
            // 
            // txtBankLandMark
            // 
            this.txtBankLandMark.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBankLandMark.Location = new System.Drawing.Point(150, 68);
            this.txtBankLandMark.MaxLength = 50;
            this.txtBankLandMark.Name = "txtBankLandMark";
            this.txtBankLandMark.Size = new System.Drawing.Size(132, 21);
            this.txtBankLandMark.TabIndex = 182;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label34.Location = new System.Drawing.Point(94, 161);
            this.label34.Margin = new System.Windows.Forms.Padding(0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(52, 15);
            this.label34.TabIndex = 189;
            this.label34.Text = "District";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBankDistricit
            // 
            this.txtBankDistricit.BackColor = System.Drawing.SystemColors.Info;
            this.txtBankDistricit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBankDistricit.Location = new System.Drawing.Point(150, 159);
            this.txtBankDistricit.MaxLength = 20;
            this.txtBankDistricit.Name = "txtBankDistricit";
            this.txtBankDistricit.ReadOnly = true;
            this.txtBankDistricit.Size = new System.Drawing.Size(133, 21);
            this.txtBankDistricit.TabIndex = 185;
            this.txtBankDistricit.TabStop = false;
            // 
            // txtBankPin
            // 
            this.txtBankPin.BackColor = System.Drawing.Color.White;
            this.txtBankPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBankPin.Location = new System.Drawing.Point(150, 203);
            this.txtBankPin.MaxLength = 6;
            this.txtBankPin.Name = "txtBankPin";
            this.txtBankPin.Size = new System.Drawing.Size(133, 21);
            this.txtBankPin.TabIndex = 194;
            this.txtBankPin.TabStop = false;
            this.txtBankPin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBankPin_KeyPress);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label30.Location = new System.Drawing.Point(91, 139);
            this.label30.Margin = new System.Windows.Forms.Padding(0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(55, 15);
            this.label30.TabIndex = 191;
            this.label30.Text = "Mandal";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label28.Location = new System.Drawing.Point(118, 205);
            this.label28.Margin = new System.Windows.Forms.Padding(0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(28, 15);
            this.label28.TabIndex = 195;
            this.label28.Text = "Pin";
            // 
            // txtBankMandal
            // 
            this.txtBankMandal.BackColor = System.Drawing.SystemColors.Info;
            this.txtBankMandal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBankMandal.Location = new System.Drawing.Point(150, 137);
            this.txtBankMandal.MaxLength = 20;
            this.txtBankMandal.Name = "txtBankMandal";
            this.txtBankMandal.ReadOnly = true;
            this.txtBankMandal.Size = new System.Drawing.Size(133, 21);
            this.txtBankMandal.TabIndex = 190;
            this.txtBankMandal.TabStop = false;
            // 
            // txtBankState
            // 
            this.txtBankState.BackColor = System.Drawing.SystemColors.Info;
            this.txtBankState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBankState.Location = new System.Drawing.Point(150, 181);
            this.txtBankState.MaxLength = 20;
            this.txtBankState.Name = "txtBankState";
            this.txtBankState.ReadOnly = true;
            this.txtBankState.Size = new System.Drawing.Size(133, 21);
            this.txtBankState.TabIndex = 192;
            this.txtBankState.TabStop = false;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label29.Location = new System.Drawing.Point(106, 183);
            this.label29.Margin = new System.Windows.Forms.Padding(0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(40, 15);
            this.label29.TabIndex = 193;
            this.label29.Text = "State";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnBankVSearch
            // 
            this.btnBankVSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBankVSearch.BackColor = System.Drawing.Color.YellowGreen;
            this.btnBankVSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnBankVSearch.FlatAppearance.BorderSize = 5;
            this.btnBankVSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBankVSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnBankVSearch.Image")));
            this.btnBankVSearch.Location = new System.Drawing.Point(391, 90);
            this.btnBankVSearch.Margin = new System.Windows.Forms.Padding(1);
            this.btnBankVSearch.Name = "btnBankVSearch";
            this.btnBankVSearch.Size = new System.Drawing.Size(26, 22);
            this.btnBankVSearch.TabIndex = 180;
            this.btnBankVSearch.TabStop = false;
            this.btnBankVSearch.Tag = "Village Search";
            this.btnBankVSearch.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.YellowGreen;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(222, 259);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 25);
            this.btnClose.TabIndex = 198;
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
            this.btnSave.Location = new System.Drawing.Point(146, 259);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 25);
            this.btnSave.TabIndex = 197;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grouper1
            // 
            this.grouper1.Controls.Add(this.txtPhoneNo);
            this.grouper1.Controls.Add(this.cbBankName);
            this.grouper1.Controls.Add(this.label2);
            this.grouper1.Controls.Add(this.btnClose);
            this.grouper1.Controls.Add(this.btnSave);
            this.grouper1.Location = new System.Drawing.Point(4, 2);
            this.grouper1.Name = "grouper1";
            this.grouper1.Size = new System.Drawing.Size(424, 297);
            this.grouper1.TabIndex = 199;
            this.grouper1.TabStop = false;
            // 
            // txtPhoneNo
            // 
            this.txtPhoneNo.BackColor = System.Drawing.Color.White;
            this.txtPhoneNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneNo.Location = new System.Drawing.Point(146, 225);
            this.txtPhoneNo.MaxLength = 20;
            this.txtPhoneNo.Name = "txtPhoneNo";
            this.txtPhoneNo.Size = new System.Drawing.Size(133, 21);
            this.txtPhoneNo.TabIndex = 200;
            this.txtPhoneNo.TabStop = false;
            this.txtPhoneNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhoneNo_KeyPress);
            // 
            // cbBankName
            // 
            this.cbBankName.FormattingEnabled = true;
            this.cbBankName.Location = new System.Drawing.Point(147, 17);
            this.cbBankName.Name = "cbBankName";
            this.cbBankName.Size = new System.Drawing.Size(223, 21);
            this.cbBankName.TabIndex = 199;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(94, 227);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 15);
            this.label2.TabIndex = 201;
            this.label2.Text = "Phone";
            // 
            // cbBankVill
            // 
            this.cbBankVill.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbBankVill.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBankVill.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.cbBankVill.FormattingEnabled = true;
            this.cbBankVill.Location = new System.Drawing.Point(229, 91);
            this.cbBankVill.Name = "cbBankVill";
            this.cbBankVill.Size = new System.Drawing.Size(161, 22);
            this.cbBankVill.TabIndex = 179;
            this.cbBankVill.TabStop = false;
            this.cbBankVill.SelectedIndexChanged += new System.EventHandler(this.cbBankVill_SelectedIndexChanged);
            // 
            // BankDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(432, 306);
            this.ControlBox = false;
            this.Controls.Add(this.btnBankVSearch);
            this.Controls.Add(this.cbBankVill);
            this.Controls.Add(this.txtBankVillSearch);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtBankHNo);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.txtBankVill);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.txtBankLandMark);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.txtBankDistricit);
            this.Controls.Add(this.txtBankPin);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.txtBankMandal);
            this.Controls.Add(this.txtBankState);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grouper1);
            this.Name = "BankDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Banker Details";
            this.Load += new System.EventHandler(this.BankDetails_Load);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBankVSearch;
        private MultiColumnComboBoxDemo.MultiColumnComboBox cbBankVill;
        private System.Windows.Forms.TextBox txtBankVillSearch;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBankHNo;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        public System.Windows.Forms.TextBox txtBankVill;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox txtBankLandMark;
        private System.Windows.Forms.Label label34;
        public System.Windows.Forms.TextBox txtBankDistricit;
        public System.Windows.Forms.TextBox txtBankPin;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label28;
        public System.Windows.Forms.TextBox txtBankMandal;
        public System.Windows.Forms.TextBox txtBankState;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grouper1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cbBankName;
        public System.Windows.Forms.TextBox txtPhoneNo;
        private System.Windows.Forms.Label label2;
    }
}