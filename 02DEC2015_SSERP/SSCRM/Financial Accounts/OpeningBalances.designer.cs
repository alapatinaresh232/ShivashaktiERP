namespace SSCRM
{
    partial class OpeningBalances
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gvProductDetails = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MonYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Debit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Credit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CloseBal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductDetails)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvProductDetails
            // 
            this.gvProductDetails.AllowUserToAddRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black;
            this.gvProductDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.gvProductDetails.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvProductDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvProductDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.gvProductDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProductDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.MonYear,
            this.OpenB,
            this.Debit,
            this.Credit,
            this.CloseBal,
            this.Delete});
            this.gvProductDetails.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvProductDetails.Location = new System.Drawing.Point(30, 8);
            this.gvProductDetails.MultiSelect = false;
            this.gvProductDetails.Name = "gvProductDetails";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvProductDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.gvProductDetails.RowHeadersVisible = false;
            this.gvProductDetails.Size = new System.Drawing.Size(499, 289);
            this.gvProductDetails.TabIndex = 167;
            // 
            // SLNO
            // 
            this.SLNO.Frozen = true;
            this.SLNO.HeaderText = "SlNo";
            this.SLNO.Name = "SLNO";
            this.SLNO.ReadOnly = true;
            this.SLNO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SLNO.Width = 40;
            // 
            // MonYear
            // 
            this.MonYear.Frozen = true;
            this.MonYear.HeaderText = "Mon/Year";
            this.MonYear.Name = "MonYear";
            this.MonYear.ReadOnly = true;
            // 
            // OpenB
            // 
            this.OpenB.Frozen = true;
            this.OpenB.HeaderText = "OpenBal";
            this.OpenB.MinimumWidth = 20;
            this.OpenB.Name = "OpenB";
            this.OpenB.ReadOnly = true;
            this.OpenB.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.OpenB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OpenB.Width = 80;
            // 
            // Debit
            // 
            this.Debit.Frozen = true;
            this.Debit.HeaderText = "Debit";
            this.Debit.Name = "Debit";
            this.Debit.ReadOnly = true;
            this.Debit.Width = 70;
            // 
            // Credit
            // 
            this.Credit.Frozen = true;
            this.Credit.HeaderText = "Credit";
            this.Credit.Name = "Credit";
            this.Credit.ReadOnly = true;
            this.Credit.Width = 70;
            // 
            // CloseBal
            // 
            this.CloseBal.Frozen = true;
            this.CloseBal.HeaderText = "Close Bal";
            this.CloseBal.Name = "CloseBal";
            this.CloseBal.ReadOnly = true;
            this.CloseBal.Width = 110;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "";
            this.Delete.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Visible = false;
            this.Delete.Width = 30;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnClose);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox5.Location = new System.Drawing.Point(220, 291);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(125, 44);
            this.groupBox5.TabIndex = 168;
            this.groupBox5.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(27, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 26);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "C&lose";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // OpeningBalances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(564, 336);
            this.ControlBox = false;
            this.Controls.Add(this.gvProductDetails);
            this.Controls.Add(this.groupBox5);
            this.Name = "OpeningBalances";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OpeningBalances";
            this.Load += new System.EventHandler(this.OpeningBalances_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvProductDetails)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView gvProductDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpenB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Debit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Credit;
        private System.Windows.Forms.DataGridViewTextBoxColumn CloseBal;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnClose;
    }
}