namespace SSCRM
{
    partial class HamaliCharges
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStockPoint = new System.Windows.Forms.TextBox();
            this.gvLicence = new System.Windows.Forms.DataGridView();
            this.textBox25 = new System.Windows.Forms.TextBox();
            this.textBox26 = new System.Windows.Forms.TextBox();
            this.textBox24 = new System.Windows.Forms.TextBox();
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BrandID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vtog = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vtov = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gtov = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLicence)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.gvLicence);
            this.groupBox1.Controls.Add(this.txtStockPoint);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(-2, -8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(896, 373);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(48, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 69;
            this.label2.Text = "Stock Point";
            // 
            // txtStockPoint
            // 
            this.txtStockPoint.BackColor = System.Drawing.SystemColors.Info;
            this.txtStockPoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStockPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStockPoint.Location = new System.Drawing.Point(158, 22);
            this.txtStockPoint.MaxLength = 21;
            this.txtStockPoint.Name = "txtStockPoint";
            this.txtStockPoint.Size = new System.Drawing.Size(357, 22);
            this.txtStockPoint.TabIndex = 47;
            // 
            // gvLicence
            // 
            this.gvLicence.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvLicence.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvLicence.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvLicence.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvLicence.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvLicence.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvLicence.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.BrandID,
            this.ProductID,
            this.ProductName,
            this.vtog,
            this.vtov,
            this.gtov});
            this.gvLicence.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvLicence.Location = new System.Drawing.Point(48, 50);
            this.gvLicence.MultiSelect = false;
            this.gvLicence.Name = "gvLicence";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvLicence.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvLicence.RowHeadersVisible = false;
            this.gvLicence.Size = new System.Drawing.Size(801, 261);
            this.gvLicence.TabIndex = 96;
            // 
            // textBox25
            // 
            this.textBox25.BackColor = System.Drawing.Color.White;
            this.textBox25.Location = new System.Drawing.Point(148, -42);
            this.textBox25.MaxLength = 12;
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new System.Drawing.Size(76, 20);
            this.textBox25.TabIndex = 88;
            // 
            // textBox26
            // 
            this.textBox26.BackColor = System.Drawing.Color.White;
            this.textBox26.Location = new System.Drawing.Point(264, -42);
            this.textBox26.MaxLength = 12;
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new System.Drawing.Size(76, 20);
            this.textBox26.TabIndex = 89;
            // 
            // textBox24
            // 
            this.textBox24.BackColor = System.Drawing.Color.White;
            this.textBox24.Location = new System.Drawing.Point(32, -42);
            this.textBox24.MaxLength = 12;
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new System.Drawing.Size(76, 20);
            this.textBox24.TabIndex = 87;
            // 
            // textBox23
            // 
            this.textBox23.BackColor = System.Drawing.Color.White;
            this.textBox23.Location = new System.Drawing.Point(264, -79);
            this.textBox23.MaxLength = 12;
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new System.Drawing.Size(76, 20);
            this.textBox23.TabIndex = 86;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label15.ForeColor = System.Drawing.Color.Navy;
            this.label15.Location = new System.Drawing.Point(143, -167);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(86, 38);
            this.label15.TabIndex = 90;
            this.label15.Text = "Vehicle to Godown";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox22
            // 
            this.textBox22.BackColor = System.Drawing.Color.White;
            this.textBox22.Location = new System.Drawing.Point(148, -79);
            this.textBox22.MaxLength = 12;
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new System.Drawing.Size(76, 20);
            this.textBox22.TabIndex = 85;
            // 
            // textBox21
            // 
            this.textBox21.BackColor = System.Drawing.Color.White;
            this.textBox21.Location = new System.Drawing.Point(32, -79);
            this.textBox21.MaxLength = 12;
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new System.Drawing.Size(76, 20);
            this.textBox21.TabIndex = 84;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.Navy;
            this.label16.Location = new System.Drawing.Point(259, -167);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(86, 38);
            this.label16.TabIndex = 91;
            this.label16.Text = "Godown to Vehicle";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox20
            // 
            this.textBox20.BackColor = System.Drawing.Color.White;
            this.textBox20.Location = new System.Drawing.Point(264, -115);
            this.textBox20.MaxLength = 12;
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new System.Drawing.Size(76, 20);
            this.textBox20.TabIndex = 83;
            // 
            // textBox19
            // 
            this.textBox19.BackColor = System.Drawing.Color.White;
            this.textBox19.Location = new System.Drawing.Point(148, -115);
            this.textBox19.MaxLength = 12;
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new System.Drawing.Size(76, 20);
            this.textBox19.TabIndex = 82;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label17.ForeColor = System.Drawing.Color.Navy;
            this.label17.Location = new System.Drawing.Point(-92, -115);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(107, 17);
            this.label17.TabIndex = 92;
            this.label17.Text = "Gromin Bag 1";
            // 
            // textBox18
            // 
            this.textBox18.BackColor = System.Drawing.Color.White;
            this.textBox18.Location = new System.Drawing.Point(32, -115);
            this.textBox18.MaxLength = 12;
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(76, 20);
            this.textBox18.TabIndex = 81;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.Navy;
            this.label14.Location = new System.Drawing.Point(30, -167);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(81, 38);
            this.label14.TabIndex = 67;
            this.label14.Text = "Vehicel to Vehicle";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label18.ForeColor = System.Drawing.Color.Navy;
            this.label18.Location = new System.Drawing.Point(-92, -76);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(118, 17);
            this.label18.TabIndex = 93;
            this.label18.Text = "Other Products";
            // 
            // btnSave
            // 
            this.btnSave.AutoEllipsis = true;
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(23, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label19.ForeColor = System.Drawing.Color.Navy;
            this.label19.Location = new System.Drawing.Point(-92, -42);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 17);
            this.label19.TabIndex = 94;
            this.label19.Text = "Cartoon";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(103, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 26);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnClose);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.btnSave);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.textBox18);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.textBox19);
            this.groupBox5.Controls.Add(this.textBox20);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.textBox21);
            this.groupBox5.Controls.Add(this.textBox22);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.textBox23);
            this.groupBox5.Controls.Add(this.textBox24);
            this.groupBox5.Controls.Add(this.textBox26);
            this.groupBox5.Controls.Add(this.textBox25);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox5.Location = new System.Drawing.Point(348, 317);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(201, 45);
            this.groupBox5.TabIndex = 95;
            this.groupBox5.TabStop = false;
            // 
            // SLNO
            // 
            this.SLNO.Frozen = true;
            this.SLNO.HeaderText = "Sl.No";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SLNO.Width = 50;
            // 
            // BrandID
            // 
            this.BrandID.Frozen = true;
            this.BrandID.HeaderText = "Brand";
            this.BrandID.Name = "BrandID";
            this.BrandID.ReadOnly = true;
            this.BrandID.Width = 150;
            // 
            // ProductID
            // 
            this.ProductID.HeaderText = "ProductID";
            this.ProductID.MinimumWidth = 20;
            this.ProductID.Name = "ProductID";
            this.ProductID.ReadOnly = true;
            this.ProductID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductID.Visible = false;
            this.ProductID.Width = 150;
            // 
            // ProductName
            // 
            this.ProductName.HeaderText = "Product Name";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Width = 200;
            // 
            // vtog
            // 
            this.vtog.HeaderText = "Vehicle to Godown";
            this.vtog.Name = "vtog";
            this.vtog.Width = 120;
            // 
            // vtov
            // 
            this.vtov.HeaderText = "Vehicle to Vehicle";
            this.vtov.Name = "vtov";
            this.vtov.Width = 120;
            // 
            // gtov
            // 
            this.gtov.HeaderText = "Godown to Vehicle";
            this.gtov.Name = "gtov";
            this.gtov.Width = 120;
            // 
            // HamaliCharges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 364);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "HamaliCharges";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hamali Charges";
            this.Load += new System.EventHandler(this.HamaliCharges_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLicence)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtStockPoint;
        public System.Windows.Forms.DataGridView gvLicence;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox19;
        private System.Windows.Forms.TextBox textBox20;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox23;
        private System.Windows.Forms.TextBox textBox24;
        private System.Windows.Forms.TextBox textBox26;
        private System.Windows.Forms.TextBox textBox25;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn BrandID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn vtog;
        private System.Windows.Forms.DataGridViewTextBoxColumn vtov;
        private System.Windows.Forms.DataGridViewTextBoxColumn gtov;
    }
}