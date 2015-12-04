namespace SDMS
{
    partial class VillageMaster
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
            this.chkDistr = new System.Windows.Forms.CheckBox();
            this.chkMand = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtDsearch = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lstMappedBranches = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblMandal = new System.Windows.Forms.Label();
            this.cbMandal = new System.Windows.Forms.ComboBox();
            this.lblDistrict = new System.Windows.Forms.Label();
            this.cbDistrict = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbStates = new System.Windows.Forms.ComboBox();
            this.txtDistrict = new System.Windows.Forms.TextBox();
            this.txtMandal = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.chkDistr);
            this.groupBox1.Controls.Add(this.chkMand);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.txtDsearch);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lstMappedBranches);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.lblMandal);
            this.groupBox1.Controls.Add(this.cbMandal);
            this.groupBox1.Controls.Add(this.lblDistrict);
            this.groupBox1.Controls.Add(this.cbDistrict);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbStates);
            this.groupBox1.Controls.Add(this.txtDistrict);
            this.groupBox1.Controls.Add(this.txtMandal);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 492);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Village Search";
            // 
            // chkDistr
            // 
            this.chkDistr.AutoSize = true;
            this.chkDistr.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDistr.ForeColor = System.Drawing.Color.OrangeRed;
            this.chkDistr.Location = new System.Drawing.Point(372, 57);
            this.chkDistr.Name = "chkDistr";
            this.chkDistr.Size = new System.Drawing.Size(15, 14);
            this.chkDistr.TabIndex = 58;
            this.chkDistr.UseVisualStyleBackColor = true;
            this.chkDistr.Visible = false;
            this.chkDistr.CheckedChanged += new System.EventHandler(this.chkDistr_CheckedChanged);
            // 
            // chkMand
            // 
            this.chkMand.AutoSize = true;
            this.chkMand.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMand.ForeColor = System.Drawing.Color.OrangeRed;
            this.chkMand.Location = new System.Drawing.Point(372, 84);
            this.chkMand.Name = "chkMand";
            this.chkMand.Size = new System.Drawing.Size(15, 14);
            this.chkMand.TabIndex = 57;
            this.chkMand.UseVisualStyleBackColor = true;
            this.chkMand.Visible = false;
            this.chkMand.CheckedChanged += new System.EventHandler(this.chkMand_CheckedChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.AutoEllipsis = true;
            this.btnAdd.BackColor = System.Drawing.Color.LightCyan;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(372, 106);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(61, 25);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtDsearch
            // 
            this.txtDsearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDsearch.Location = new System.Drawing.Point(142, 105);
            this.txtDsearch.Name = "txtDsearch";
            this.txtDsearch.Size = new System.Drawing.Size(223, 24);
            this.txtDsearch.TabIndex = 45;
            this.txtDsearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDsearch_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(88, 109);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 15);
            this.label4.TabIndex = 24;
            this.label4.Text = "Village";
            // 
            // lstMappedBranches
            // 
            this.lstMappedBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstMappedBranches.ForeColor = System.Drawing.Color.Black;
            this.lstMappedBranches.FormattingEnabled = true;
            this.lstMappedBranches.ItemHeight = 15;
            this.lstMappedBranches.Location = new System.Drawing.Point(6, 135);
            this.lstMappedBranches.Name = "lstMappedBranches";
            this.lstMappedBranches.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstMappedBranches.Size = new System.Drawing.Size(431, 319);
            this.lstMappedBranches.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Location = new System.Drawing.Point(107, 449);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(228, 40);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.AutoEllipsis = true;
            this.btnClose.BackColor = System.Drawing.Color.LightCyan;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.btnClose.FlatAppearance.BorderSize = 5;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Location = new System.Drawing.Point(85, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(59, 25);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblMandal
            // 
            this.lblMandal.AutoSize = true;
            this.lblMandal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMandal.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblMandal.Location = new System.Drawing.Point(3, 81);
            this.lblMandal.Margin = new System.Windows.Forms.Padding(0);
            this.lblMandal.Name = "lblMandal";
            this.lblMandal.Size = new System.Drawing.Size(137, 15);
            this.lblMandal.TabIndex = 21;
            this.lblMandal.Text = "Mandal/Tahsil/Block";
            // 
            // cbMandal
            // 
            this.cbMandal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMandal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMandal.FormattingEnabled = true;
            this.cbMandal.Location = new System.Drawing.Point(142, 77);
            this.cbMandal.Name = "cbMandal";
            this.cbMandal.Size = new System.Drawing.Size(223, 23);
            this.cbMandal.TabIndex = 3;
            this.cbMandal.SelectedIndexChanged += new System.EventHandler(this.cbMandal_SelectedIndexChanged);
            // 
            // lblDistrict
            // 
            this.lblDistrict.AutoSize = true;
            this.lblDistrict.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistrict.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblDistrict.Location = new System.Drawing.Point(52, 53);
            this.lblDistrict.Margin = new System.Windows.Forms.Padding(0);
            this.lblDistrict.Name = "lblDistrict";
            this.lblDistrict.Size = new System.Drawing.Size(88, 15);
            this.lblDistrict.TabIndex = 19;
            this.lblDistrict.Text = "District/Zone";
            // 
            // cbDistrict
            // 
            this.cbDistrict.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDistrict.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDistrict.FormattingEnabled = true;
            this.cbDistrict.Location = new System.Drawing.Point(142, 50);
            this.cbDistrict.Name = "cbDistrict";
            this.cbDistrict.Size = new System.Drawing.Size(223, 23);
            this.cbDistrict.TabIndex = 2;
            this.cbDistrict.SelectedIndexChanged += new System.EventHandler(this.cbDistrict_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(99, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 17;
            this.label1.Text = "State";
            // 
            // cbStates
            // 
            this.cbStates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStates.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStates.FormattingEnabled = true;
            this.cbStates.Location = new System.Drawing.Point(142, 23);
            this.cbStates.Name = "cbStates";
            this.cbStates.Size = new System.Drawing.Size(223, 23);
            this.cbStates.TabIndex = 1;
            this.cbStates.SelectedIndexChanged += new System.EventHandler(this.cbStates_SelectedIndexChanged);
            // 
            // txtDistrict
            // 
            this.txtDistrict.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDistrict.Location = new System.Drawing.Point(143, 50);
            this.txtDistrict.Name = "txtDistrict";
            this.txtDistrict.Size = new System.Drawing.Size(223, 24);
            this.txtDistrict.TabIndex = 60;
            // 
            // txtMandal
            // 
            this.txtMandal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMandal.Location = new System.Drawing.Point(143, 77);
            this.txtMandal.Name = "txtMandal";
            this.txtMandal.Size = new System.Drawing.Size(223, 24);
            this.txtMandal.TabIndex = 59;
            // 
            // VillageMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(448, 498);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "VillageMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Village Master";
            this.Load += new System.EventHandler(this.VillageMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblMandal;
        private System.Windows.Forms.ComboBox cbMandal;
        private System.Windows.Forms.Label lblDistrict;
        private System.Windows.Forms.ComboBox cbDistrict;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbStates;
        private System.Windows.Forms.ListBox lstMappedBranches;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDsearch;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox chkMand;
        private System.Windows.Forms.CheckBox chkDistr;
        private System.Windows.Forms.TextBox txtMandal;
        private System.Windows.Forms.TextBox txtDistrict;

    }
}