namespace SSCRM
{
    partial class IndentFromBranchFRM
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblGroupEcode = new System.Windows.Forms.Label();
            this.cbGroup = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbBranches = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gvIndentList = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.SLNOList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndentNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GLName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndentFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalProducts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalReqQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndentFromCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvIndentList)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.gvIndentList);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Location = new System.Drawing.Point(4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(915, 557);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblGroupEcode);
            this.groupBox1.Controls.Add(this.cbGroup);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbBranches);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupBox1.Location = new System.Drawing.Point(25, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(884, 54);
            this.groupBox1.TabIndex = 77;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serch Criteria for Indents";
            // 
            // lblGroupEcode
            // 
            this.lblGroupEcode.BackColor = System.Drawing.Color.Azure;
            this.lblGroupEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupEcode.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblGroupEcode.Location = new System.Drawing.Point(672, 18);
            this.lblGroupEcode.Name = "lblGroupEcode";
            this.lblGroupEcode.Size = new System.Drawing.Size(206, 23);
            this.lblGroupEcode.TabIndex = 44;
            // 
            // cbGroup
            // 
            this.cbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGroup.FormattingEnabled = true;
            this.cbGroup.Location = new System.Drawing.Point(447, 18);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Size = new System.Drawing.Size(219, 23);
            this.cbGroup.TabIndex = 41;
            this.cbGroup.SelectedIndexChanged += new System.EventHandler(this.cbGroup_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(399, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 15);
            this.label4.TabIndex = 43;
            this.label4.Text = "Group";
            // 
            // cbBranches
            // 
            this.cbBranches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBranches.FormattingEnabled = true;
            this.cbBranches.Location = new System.Drawing.Point(133, 18);
            this.cbBranches.Name = "cbBranches";
            this.cbBranches.Size = new System.Drawing.Size(245, 23);
            this.cbBranches.TabIndex = 40;
            this.cbBranches.SelectedIndexChanged += new System.EventHandler(this.cbBranches_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(40, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 15);
            this.label5.TabIndex = 42;
            this.label5.Text = "Indents from";
            // 
            // gvIndentList
            // 
            this.gvIndentList.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvIndentList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvIndentList.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvIndentList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightSeaGreen;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvIndentList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvIndentList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvIndentList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNOList,
            this.IndentNo,
            this.IndentDate,
            this.GLName,
            this.IndentFrom,
            this.TotalProducts,
            this.TotalReqQty,
            this.IndentFromCode});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvIndentList.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvIndentList.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvIndentList.Location = new System.Drawing.Point(16, 75);
            this.gvIndentList.MultiSelect = false;
            this.gvIndentList.Name = "gvIndentList";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvIndentList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvIndentList.RowHeadersVisible = false;
            this.gvIndentList.Size = new System.Drawing.Size(893, 427);
            this.gvIndentList.TabIndex = 58;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Location = new System.Drawing.Point(428, 507);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(127, 45);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(30, 13);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 23;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SLNOList
            // 
            this.SLNOList.Frozen = true;
            this.SLNOList.HeaderText = "SL.NO";
            this.SLNOList.Name = "SLNOList";
            this.SLNOList.Width = 60;
            // 
            // IndentNo
            // 
            this.IndentNo.Frozen = true;
            this.IndentNo.HeaderText = "Indent No.";
            this.IndentNo.Name = "IndentNo";
            this.IndentNo.ReadOnly = true;
            this.IndentNo.Width = 110;
            // 
            // IndentDate
            // 
            this.IndentDate.Frozen = true;
            this.IndentDate.HeaderText = "Indent Date";
            this.IndentDate.MinimumWidth = 20;
            this.IndentDate.Name = "IndentDate";
            this.IndentDate.ReadOnly = true;
            this.IndentDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IndentDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IndentDate.Width = 120;
            // 
            // GLName
            // 
            this.GLName.HeaderText = "GLCode";
            this.GLName.Name = "GLName";
            // 
            // IndentFrom
            // 
            this.IndentFrom.HeaderText = "Indent From";
            this.IndentFrom.Name = "IndentFrom";
            this.IndentFrom.Width = 200;
            // 
            // TotalProducts
            // 
            this.TotalProducts.HeaderText = "Tot.Products";
            this.TotalProducts.Name = "TotalProducts";
            this.TotalProducts.Width = 130;
            // 
            // TotalReqQty
            // 
            this.TotalReqQty.HeaderText = "Tot.Req  Qty";
            this.TotalReqQty.Name = "TotalReqQty";
            this.TotalReqQty.Width = 130;
            // 
            // IndentFromCode
            // 
            this.IndentFromCode.HeaderText = "IndentFromCode";
            this.IndentFromCode.Name = "IndentFromCode";
            this.IndentFromCode.Visible = false;
            // 
            // IndentFromBranchFRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(920, 565);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "IndentFromBranchFRM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Indents from Branches";
            this.Load += new System.EventHandler(this.StockIndentFRM_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvIndentList)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        public System.Windows.Forms.DataGridView gvIndentList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblGroupEcode;
        private System.Windows.Forms.ComboBox cbGroup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbBranches;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNOList;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndentNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndentDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn GLName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndentFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalProducts;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalReqQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndentFromCode;
    }
}