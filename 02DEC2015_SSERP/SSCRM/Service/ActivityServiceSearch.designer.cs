namespace SSCRM
{
    partial class ActivityServiceSearch
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lblFrm = new System.Windows.Forms.Label();
            this.txtFrm = new System.Windows.Forms.TextBox();
            this.cmbQty = new System.Windows.Forms.ComboBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.lblCnt = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkDocM = new System.Windows.Forms.CheckBox();
            this.chkDocMonth = new System.Windows.Forms.CheckedListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkVillage = new System.Windows.Forms.CheckedListBox();
            this.chkVill = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBranch = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkProducts = new System.Windows.Forms.CheckedListBox();
            this.chkProduct = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.Controls.Add(this.groupBox7);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.groupBox8);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox2.Location = new System.Drawing.Point(5, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(748, 358);
            this.groupBox2.TabIndex = 93;
            this.groupBox2.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.lblFrm);
            this.groupBox1.Controls.Add(this.txtFrm);
            this.groupBox1.Controls.Add(this.cmbQty);
            this.groupBox1.Controls.Add(this.lblTo);
            this.groupBox1.Controls.Add(this.txtTo);
            this.groupBox1.Controls.Add(this.lblCnt);
            this.groupBox1.Location = new System.Drawing.Point(535, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 117);
            this.groupBox1.TabIndex = 99;
            this.groupBox1.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.Navy;
            this.label12.Location = new System.Drawing.Point(19, 23);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 16);
            this.label12.TabIndex = 82;
            this.label12.Text = "Quantity";
            // 
            // lblFrm
            // 
            this.lblFrm.AutoSize = true;
            this.lblFrm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblFrm.ForeColor = System.Drawing.Color.Navy;
            this.lblFrm.Location = new System.Drawing.Point(19, 74);
            this.lblFrm.Name = "lblFrm";
            this.lblFrm.Size = new System.Drawing.Size(47, 16);
            this.lblFrm.TabIndex = 89;
            this.lblFrm.Text = "From ";
            // 
            // txtFrm
            // 
            this.txtFrm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrm.Location = new System.Drawing.Point(70, 72);
            this.txtFrm.MaxLength = 4;
            this.txtFrm.Name = "txtFrm";
            this.txtFrm.Size = new System.Drawing.Size(38, 21);
            this.txtFrm.TabIndex = 2;
            this.txtFrm.Validated += new System.EventHandler(this.txtFrm_Validated);
            this.txtFrm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFrm_KeyPress);
            // 
            // cmbQty
            // 
            this.cmbQty.AllowDrop = true;
            this.cmbQty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbQty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbQty.FormattingEnabled = true;
            this.cmbQty.Items.AddRange(new object[] {
            "-- SELECT --",
            "LESS THAN OR EQUALS",
            "GREATER THAN OR EQUALS",
            "EQUALS",
            "BETWEEN"});
            this.cmbQty.Location = new System.Drawing.Point(19, 43);
            this.cmbQty.Name = "cmbQty";
            this.cmbQty.Size = new System.Drawing.Size(156, 23);
            this.cmbQty.TabIndex = 1;
            this.cmbQty.SelectedIndexChanged += new System.EventHandler(this.cmbQty_SelectedIndexChanged);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTo.ForeColor = System.Drawing.Color.Navy;
            this.lblTo.Location = new System.Drawing.Point(110, 74);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(27, 16);
            this.lblTo.TabIndex = 91;
            this.lblTo.Text = "To";
            // 
            // txtTo
            // 
            this.txtTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTo.Location = new System.Drawing.Point(138, 72);
            this.txtTo.MaxLength = 4;
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(38, 21);
            this.txtTo.TabIndex = 3;
            this.txtTo.Validated += new System.EventHandler(this.txtTo_Validated);
            this.txtTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTo_KeyPress);
            // 
            // lblCnt
            // 
            this.lblCnt.AutoSize = true;
            this.lblCnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblCnt.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblCnt.Location = new System.Drawing.Point(147, 17);
            this.lblCnt.Margin = new System.Windows.Forms.Padding(0);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(28, 16);
            this.lblCnt.TabIndex = 47;
            this.lblCnt.Text = "cnt";
            this.lblCnt.Visible = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkDocM);
            this.groupBox5.Controls.Add(this.chkDocMonth);
            this.groupBox5.Location = new System.Drawing.Point(16, 70);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(158, 261);
            this.groupBox5.TabIndex = 98;
            this.groupBox5.TabStop = false;
            // 
            // chkDocM
            // 
            this.chkDocM.AutoSize = true;
            this.chkDocM.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.chkDocM.ForeColor = System.Drawing.Color.Navy;
            this.chkDocM.Location = new System.Drawing.Point(9, -3);
            this.chkDocM.Name = "chkDocM";
            this.chkDocM.Size = new System.Drawing.Size(141, 20);
            this.chkDocM.TabIndex = 84;
            this.chkDocM.Text = "Document Month";
            this.chkDocM.UseVisualStyleBackColor = true;
            this.chkDocM.CheckedChanged += new System.EventHandler(this.chkDocM_CheckedChanged);
            // 
            // chkDocMonth
            // 
            this.chkDocMonth.CheckOnClick = true;
            this.chkDocMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDocMonth.FormattingEnabled = true;
            this.chkDocMonth.Location = new System.Drawing.Point(10, 20);
            this.chkDocMonth.Name = "chkDocMonth";
            this.chkDocMonth.Size = new System.Drawing.Size(138, 228);
            this.chkDocMonth.TabIndex = 77;
            this.chkDocMonth.SelectedIndexChanged += new System.EventHandler(this.chkDocMonth_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkVillage);
            this.groupBox4.Controls.Add(this.chkVill);
            this.groupBox4.Location = new System.Drawing.Point(183, 70);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(157, 261);
            this.groupBox4.TabIndex = 97;
            this.groupBox4.TabStop = false;
            // 
            // chkVillage
            // 
            this.chkVillage.CheckOnClick = true;
            this.chkVillage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkVillage.FormattingEnabled = true;
            this.chkVillage.Location = new System.Drawing.Point(6, 20);
            this.chkVillage.Name = "chkVillage";
            this.chkVillage.Size = new System.Drawing.Size(144, 228);
            this.chkVillage.TabIndex = 1;
            this.chkVillage.SelectedIndexChanged += new System.EventHandler(this.chkVillage_SelectedIndexChanged);
            // 
            // chkVill
            // 
            this.chkVill.AutoSize = true;
            this.chkVill.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.chkVill.ForeColor = System.Drawing.Color.Navy;
            this.chkVill.Location = new System.Drawing.Point(8, -1);
            this.chkVill.Name = "chkVill";
            this.chkVill.Size = new System.Drawing.Size(84, 20);
            this.chkVill.TabIndex = 0;
            this.chkVill.Text = "Villages";
            this.chkVill.UseVisualStyleBackColor = true;
            this.chkVill.CheckedChanged += new System.EventHandler(this.chkVill_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(8, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 41;
            this.label2.Text = "Branch";
            // 
            // cmbBranch
            // 
            this.cmbBranch.AllowDrop = true;
            this.cmbBranch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbBranch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBranch.FormattingEnabled = true;
            this.cmbBranch.Location = new System.Drawing.Point(66, 14);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Size = new System.Drawing.Size(301, 23);
            this.cmbBranch.TabIndex = 0;
            this.cmbBranch.SelectedIndexChanged += new System.EventHandler(this.cmbBranch_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkProducts);
            this.groupBox3.Controls.Add(this.chkProduct);
            this.groupBox3.Location = new System.Drawing.Point(352, 70);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(168, 261);
            this.groupBox3.TabIndex = 96;
            this.groupBox3.TabStop = false;
            // 
            // chkProducts
            // 
            this.chkProducts.CheckOnClick = true;
            this.chkProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkProducts.FormattingEnabled = true;
            this.chkProducts.Location = new System.Drawing.Point(6, 20);
            this.chkProducts.Name = "chkProducts";
            this.chkProducts.Size = new System.Drawing.Size(156, 228);
            this.chkProducts.TabIndex = 1;
            this.chkProducts.SelectedIndexChanged += new System.EventHandler(this.chkProducts_SelectedIndexChanged);
            // 
            // chkProduct
            // 
            this.chkProduct.AutoSize = true;
            this.chkProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.chkProduct.ForeColor = System.Drawing.Color.Navy;
            this.chkProduct.Location = new System.Drawing.Point(12, -1);
            this.chkProduct.Name = "chkProduct";
            this.chkProduct.Size = new System.Drawing.Size(88, 20);
            this.chkProduct.TabIndex = 0;
            this.chkProduct.Text = "Products";
            this.chkProduct.UseVisualStyleBackColor = true;
            this.chkProduct.CheckedChanged += new System.EventHandler(this.chkProduct_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnClose);
            this.groupBox6.Controls.Add(this.btnDisplay);
            this.groupBox6.Location = new System.Drawing.Point(535, 194);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(195, 137);
            this.groupBox6.TabIndex = 101;
            this.groupBox6.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.Navy;
            this.btnClose.Location = new System.Drawing.Point(51, 71);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 34);
            this.btnClose.TabIndex = 100;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDisplay
            // 
            this.btnDisplay.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDisplay.ForeColor = System.Drawing.Color.Navy;
            this.btnDisplay.Location = new System.Drawing.Point(51, 31);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(93, 34);
            this.btnDisplay.TabIndex = 4;
            this.btnDisplay.Text = "Print";
            this.btnDisplay.UseVisualStyleBackColor = false;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Location = new System.Drawing.Point(16, 19);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(324, 45);
            this.groupBox7.TabIndex = 102;
            this.groupBox7.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(100)))), ((int)(((byte)(178)))));
            this.label1.Location = new System.Drawing.Point(26, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 18);
            this.label1.TabIndex = 103;
            this.label1.Text = "Invoice list Print for Service Activity";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label2);
            this.groupBox8.Controls.Add(this.cmbBranch);
            this.groupBox8.Location = new System.Drawing.Point(352, 19);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(379, 45);
            this.groupBox8.TabIndex = 104;
            this.groupBox8.TabStop = false;
            // 
            // ActivityServiceSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(756, 367);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Name = "ActivityServiceSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Activity Salse Service Search";
            this.Load += new System.EventHandler(this.SalesService_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBranch;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.Label lblCnt;
        private System.Windows.Forms.CheckedListBox chkDocMonth;
        private System.Windows.Forms.CheckedListBox chkVillage;
        private System.Windows.Forms.CheckedListBox chkProducts;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.TextBox txtFrm;
        private System.Windows.Forms.Label lblFrm;
        private System.Windows.Forms.ComboBox cmbQty;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkProduct;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkVill;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox chkDocM;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox8;
    }
}