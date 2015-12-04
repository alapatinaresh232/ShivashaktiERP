using GroupCtrl;
namespace SSCRM
{
    partial class frmFamily
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
            this.btnSave = new System.Windows.Forms.Button();
            this.grouper1 = new GroupCtrl.Grouper();
            this.cmbOccupation_optional = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.cmbRelationShip = new System.Windows.Forms.ComboBox();
            this.cmbDepending = new System.Windows.Forms.ComboBox();
            this.cmbResiding = new System.Windows.Forms.ComboBox();
            this.dtpDOB = new System.Windows.Forms.DateTimePicker();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.YellowGreen;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(123, 207);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 25);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.PowderBlue;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.PowderBlue;
            this.grouper1.BackgroundGradientMode = GroupCtrl.Grouper.GroupBoxGradientMode.Vertical;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.cmbOccupation_optional);
            this.grouper1.Controls.Add(this.label7);
            this.grouper1.Controls.Add(this.txtAge);
            this.grouper1.Controls.Add(this.cmbRelationShip);
            this.grouper1.Controls.Add(this.cmbDepending);
            this.grouper1.Controls.Add(this.cmbResiding);
            this.grouper1.Controls.Add(this.dtpDOB);
            this.grouper1.Controls.Add(this.txtName);
            this.grouper1.Controls.Add(this.label6);
            this.grouper1.Controls.Add(this.label5);
            this.grouper1.Controls.Add(this.label3);
            this.grouper1.Controls.Add(this.label2);
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.Controls.Add(this.label4);
            this.grouper1.Controls.Add(this.btnClose);
            this.grouper1.Controls.Add(this.btnSave);
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
            this.grouper1.Size = new System.Drawing.Size(383, 242);
            this.grouper1.TabIndex = 0;
            // 
            // cmbOccupation_optional
            // 
            this.cmbOccupation_optional.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOccupation_optional.FormattingEnabled = true;
            this.cmbOccupation_optional.Items.AddRange(new object[] {
            "--SELECT--"});
            this.cmbOccupation_optional.Location = new System.Drawing.Point(187, 173);
            this.cmbOccupation_optional.Name = "cmbOccupation_optional";
            this.cmbOccupation_optional.Size = new System.Drawing.Size(171, 23);
            this.cmbOccupation_optional.Sorted = true;
            this.cmbOccupation_optional.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(223, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 14);
            this.label7.TabIndex = 19;
            this.label7.Text = "/";
            // 
            // txtAge
            // 
            this.txtAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAge.Location = new System.Drawing.Point(188, 87);
            this.txtAge.MaxLength = 2;
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(32, 21);
            this.txtAge.TabIndex = 2;
            this.txtAge.TextChanged += new System.EventHandler(this.txtAge_TextChanged);
            // 
            // cmbRelationShip
            // 
            this.cmbRelationShip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRelationShip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRelationShip.FormattingEnabled = true;
            this.cmbRelationShip.Items.AddRange(new object[] {
            "--SELECT--",
            "FATHER",
            "MOTHER",
            "BROTHER",
            "SISTER",
            "WIFE",
            "HUSBAND",
            "CHILDREN",
            "OTHERS"});
            this.cmbRelationShip.Location = new System.Drawing.Point(187, 30);
            this.cmbRelationShip.Name = "cmbRelationShip";
            this.cmbRelationShip.Size = new System.Drawing.Size(171, 23);
            this.cmbRelationShip.TabIndex = 0;
            // 
            // cmbDepending
            // 
            this.cmbDepending.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepending.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDepending.FormattingEnabled = true;
            this.cmbDepending.Items.AddRange(new object[] {
            "--Select--",
            "Y",
            "N"});
            this.cmbDepending.Location = new System.Drawing.Point(187, 146);
            this.cmbDepending.Name = "cmbDepending";
            this.cmbDepending.Size = new System.Drawing.Size(86, 23);
            this.cmbDepending.TabIndex = 5;
            // 
            // cmbResiding
            // 
            this.cmbResiding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResiding.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbResiding.FormattingEnabled = true;
            this.cmbResiding.Items.AddRange(new object[] {
            "--Select--",
            "Y",
            "N"});
            this.cmbResiding.Location = new System.Drawing.Point(187, 119);
            this.cmbResiding.Name = "cmbResiding";
            this.cmbResiding.Size = new System.Drawing.Size(86, 23);
            this.cmbResiding.TabIndex = 4;
            // 
            // dtpDOB
            // 
            this.dtpDOB.CustomFormat = "dd/MM/yyyy";
            this.dtpDOB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDOB.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDOB.Location = new System.Drawing.Point(245, 87);
            this.dtpDOB.Name = "dtpDOB";
            this.dtpDOB.Size = new System.Drawing.Size(103, 21);
            this.dtpDOB.TabIndex = 3;
            this.dtpDOB.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            this.dtpDOB.ValueChanged += new System.EventHandler(this.dtpDOB_ValueChanged);
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.267326F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(187, 57);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(171, 21);
            this.txtName.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(99, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 14);
            this.label6.TabIndex = 17;
            this.label6.Text = "Occupation";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(8, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 14);
            this.label5.TabIndex = 16;
            this.label5.Text = "Depending on me Yes/No";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(10, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 14);
            this.label3.TabIndex = 15;
            this.label3.Text = "Residing with me Yes/No";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(46, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 14);
            this.label2.TabIndex = 14;
            this.label2.Text = "Age / Date of Birth";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(92, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 14);
            this.label1.TabIndex = 13;
            this.label1.Text = "Relationship";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(136, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 14);
            this.label4.TabIndex = 11;
            this.label4.Text = "Name";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.YellowGreen;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(199, 207);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 25);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmFamily
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 232);
            this.ControlBox = false;
            this.Controls.Add(this.grouper1);
            this.Name = "frmFamily";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Family Information";
            this.Load += new System.EventHandler(this.frmFamily_Load);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Grouper grouper1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.DateTimePicker dtpDOB;
        private System.Windows.Forms.ComboBox cmbDepending;
        private System.Windows.Forms.ComboBox cmbResiding;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cmbRelationShip;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbOccupation_optional;

    }
}