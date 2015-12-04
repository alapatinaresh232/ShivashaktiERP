namespace SDMS
{
    partial class AddressCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddressCtrl));
            this.btnAddress = new System.Windows.Forms.Button();
            this.txtState = new System.Windows.Forms.TextBox();
            this.txtPin_num = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDistrict = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtVillage = new System.Windows.Forms.TextBox();
            this.txtMondal = new System.Windows.Forms.TextBox();
            this.txtLandMark = new System.Windows.Forms.TextBox();
            this.txtHouseNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnAddress
            // 
            this.btnAddress.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddress.BackgroundImage")));
            this.btnAddress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddress.Location = new System.Drawing.Point(237, 7);
            this.btnAddress.Name = "btnAddress";
            this.btnAddress.Size = new System.Drawing.Size(22, 18);
            this.btnAddress.TabIndex = 1;
            this.btnAddress.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.btnAddress, "Village Search Help Button");
            this.btnAddress.UseVisualStyleBackColor = true;
            this.btnAddress.Click += new System.EventHandler(this.btnAddress_Click);
            // 
            // txtState
            // 
            this.txtState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtState.Location = new System.Drawing.Point(302, 52);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(137, 23);
            this.txtState.TabIndex = 6;
            // 
            // txtPin_num
            // 
            this.txtPin_num.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPin_num.Location = new System.Drawing.Point(302, 77);
            this.txtPin_num.MaxLength = 6;
            this.txtPin_num.Name = "txtPin_num";
            this.txtPin_num.Size = new System.Drawing.Size(72, 23);
            this.txtPin_num.TabIndex = 7;
            this.txtPin_num.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPin_num_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label10.Location = new System.Drawing.Point(268, 81);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 19);
            this.label10.TabIndex = 65;
            this.label10.Text = "Pin";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label9.Location = new System.Drawing.Point(257, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 19);
            this.label9.TabIndex = 64;
            this.label9.Text = "State";
            // 
            // txtDistrict
            // 
            this.txtDistrict.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDistrict.Location = new System.Drawing.Point(302, 26);
            this.txtDistrict.Name = "txtDistrict";
            this.txtDistrict.Size = new System.Drawing.Size(137, 23);
            this.txtDistrict.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(243, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 19);
            this.label7.TabIndex = 62;
            this.label7.Text = "District";
            // 
            // txtVillage
            // 
            this.txtVillage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVillage.Location = new System.Drawing.Point(100, 51);
            this.txtVillage.Name = "txtVillage";
            this.txtVillage.Size = new System.Drawing.Size(137, 23);
            this.txtVillage.TabIndex = 3;
            // 
            // txtMondal
            // 
            this.txtMondal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMondal.Location = new System.Drawing.Point(100, 75);
            this.txtMondal.Name = "txtMondal";
            this.txtMondal.Size = new System.Drawing.Size(137, 23);
            this.txtMondal.TabIndex = 4;
            // 
            // txtLandMark
            // 
            this.txtLandMark.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLandMark.Location = new System.Drawing.Point(100, 27);
            this.txtLandMark.Name = "txtLandMark";
            this.txtLandMark.Size = new System.Drawing.Size(137, 23);
            this.txtLandMark.TabIndex = 2;
            // 
            // txtHouseNo
            // 
            this.txtHouseNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHouseNo.Location = new System.Drawing.Point(100, 3);
            this.txtHouseNo.Name = "txtHouseNo";
            this.txtHouseNo.Size = new System.Drawing.Size(137, 23);
            this.txtHouseNo.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(39, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 19);
            this.label6.TabIndex = 57;
            this.label6.Text = "Mondal";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(3, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 19);
            this.label5.TabIndex = 56;
            this.label5.Text = "Village/Town";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(17, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 19);
            this.label3.TabIndex = 55;
            this.label3.Text = "Land Mark.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(25, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 19);
            this.label2.TabIndex = 54;
            this.label2.Text = "House No";
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.toolTip1.ForeColor = System.Drawing.Color.Red;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Village Search Help Button";
            // 
            // AddressCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAddress);
            this.Controls.Add(this.txtState);
            this.Controls.Add(this.txtPin_num);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDistrict);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtVillage);
            this.Controls.Add(this.txtMondal);
            this.Controls.Add(this.txtLandMark);
            this.Controls.Add(this.txtHouseNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "AddressCtrl";
            this.Size = new System.Drawing.Size(446, 106);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddress;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.TextBox txtPin_num;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDistrict;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtVillage;
        private System.Windows.Forms.TextBox txtMondal;
        private System.Windows.Forms.TextBox txtLandMark;
        private System.Windows.Forms.TextBox txtHouseNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
