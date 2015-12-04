using GroupCtrl;
namespace SSCRM
{
    partial class frmReference
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
            this.cmbOccupation_optional = new System.Windows.Forms.ComboBox();
            this.txtRefPhno_optional = new System.Windows.Forms.TextBox();
            this.txtPhoneno = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.addressCtrl1 = new SSCRM.AddressCtrl();
            this.label11 = new System.Windows.Forms.Label();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.PowderBlue;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.PowderBlue;
            this.grouper1.BackgroundGradientMode = GroupCtrl.Grouper.GroupBoxGradientMode.Vertical;
            this.grouper1.BorderColor = System.Drawing.Color.Empty;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.cmbOccupation_optional);
            this.grouper1.Controls.Add(this.txtRefPhno_optional);
            this.grouper1.Controls.Add(this.txtPhoneno);
            this.grouper1.Controls.Add(this.label12);
            this.grouper1.Controls.Add(this.addressCtrl1);
            this.grouper1.Controls.Add(this.label11);
            this.grouper1.Controls.Add(this.txtReference);
            this.grouper1.Controls.Add(this.btnClose);
            this.grouper1.Controls.Add(this.btnSave);
            this.grouper1.Controls.Add(this.label4);
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "";
            this.grouper1.Location = new System.Drawing.Point(1, -10);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 1;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(474, 236);
            this.grouper1.TabIndex = 3;
            // 
            // cmbOccupation_optional
            // 
            this.cmbOccupation_optional.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOccupation_optional.FormattingEnabled = true;
            this.cmbOccupation_optional.Items.AddRange(new object[] {
            "--SELECT--",
            "FATHER",
            "MOTHER",
            "BROTHER",
            "SISTER",
            "WIFE",
            "HUSBAND",
            "CHILDREN",
            "OTHERS"});
            this.cmbOccupation_optional.Location = new System.Drawing.Point(301, 25);
            this.cmbOccupation_optional.Name = "cmbOccupation_optional";
            this.cmbOccupation_optional.Size = new System.Drawing.Size(171, 23);
            this.cmbOccupation_optional.TabIndex = 1;
            // 
            // txtRefPhno_optional
            // 
            this.txtRefPhno_optional.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefPhno_optional.Location = new System.Drawing.Point(298, 151);
            this.txtRefPhno_optional.Name = "txtRefPhno_optional";
            this.txtRefPhno_optional.Size = new System.Drawing.Size(130, 22);
            this.txtRefPhno_optional.TabIndex = 4;
            // 
            // txtPhoneno
            // 
            this.txtPhoneno.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneno.Location = new System.Drawing.Point(96, 150);
            this.txtPhoneno.Name = "txtPhoneno";
            this.txtPhoneno.Size = new System.Drawing.Size(112, 22);
            this.txtPhoneno.TabIndex = 3;
            this.txtPhoneno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhoneno_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label12.Location = new System.Drawing.Point(22, 153);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 14);
            this.label12.TabIndex = 31;
            this.label12.Text = "Phone No";
            // 
            // addressCtrl1
            // 
            this.addressCtrl1.District = "";
            this.addressCtrl1.HouseNo = "";
            this.addressCtrl1.LandMark = "";
            this.addressCtrl1.Location = new System.Drawing.Point(-4, 50);
            this.addressCtrl1.Mondal = "";
            this.addressCtrl1.Name = "addressCtrl1";
            this.addressCtrl1.Pin = "";
            this.addressCtrl1.Size = new System.Drawing.Size(450, 106);
            this.addressCtrl1.State = "";
            this.addressCtrl1.TabIndex = 2;
            this.addressCtrl1.Village = "";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label11.Location = new System.Drawing.Point(214, 154);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 14);
            this.label11.TabIndex = 30;
            this.label11.Text = "Phone No 2";
            // 
            // txtReference
            // 
            this.txtReference.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReference.Location = new System.Drawing.Point(76, 25);
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(148, 22);
            this.txtReference.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.YellowGreen;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(240, 197);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 25);
            this.btnClose.TabIndex = 6;
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
            this.btnSave.Location = new System.Drawing.Point(177, 197);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 25);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(227, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 14);
            this.label4.TabIndex = 45;
            this.label4.Text = "Occupation";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(5, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 14);
            this.label1.TabIndex = 13;
            this.label1.Text = "Ref. Name";
            // 
            // frmReference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 225);
            this.ControlBox = false;
            this.Controls.Add(this.grouper1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "frmReference";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reference";
            this.Load += new System.EventHandler(this.frmReference_Load);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Grouper grouper1;
        private System.Windows.Forms.TextBox txtRefPhno_optional;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPhoneno;
        private System.Windows.Forms.Label label12;
        private SSCRM.AddressCtrl addressCtrl1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cmbOccupation_optional;
    }
}