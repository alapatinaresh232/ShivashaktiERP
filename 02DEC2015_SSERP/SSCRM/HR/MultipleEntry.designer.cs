namespace SSCRM
{
    partial class MultipleEntry
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
            this.btnClose = new System.Windows.Forms.Button();
            this.gvMultiple = new System.Windows.Forms.DataGridView();
            this.SlNo_ref = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_APPL_NUMBER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_EORA_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_FORH_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_DOB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HAMH_DOJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvMultiple)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Location = new System.Drawing.Point(-2, -9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(863, 479);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.Navy;
            this.btnClose.Location = new System.Drawing.Point(394, 431);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 32);
            this.btnClose.TabIndex = 64;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gvMultiple
            // 
            this.gvMultiple.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.gvMultiple.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvMultiple.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvMultiple.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvMultiple.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvMultiple.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvMultiple.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SlNo_ref,
            this.HAMH_APPL_NUMBER,
            this.HAMH_EORA_CODE,
            this.HAMH_NAME,
            this.HAMH_FORH_NAME,
            this.HAMH_DOB,
            this.HAMH_DOJ,
            this.STATUS,
            this.Delete});
            this.gvMultiple.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvMultiple.Location = new System.Drawing.Point(3, 28);
            this.gvMultiple.MultiSelect = false;
            this.gvMultiple.Name = "gvMultiple";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvMultiple.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvMultiple.RowHeadersVisible = false;
            this.gvMultiple.Size = new System.Drawing.Size(850, 375);
            this.gvMultiple.TabIndex = 63;
            this.gvMultiple.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvMultiple_CellClick);
            // 
            // SlNo_ref
            // 
            this.SlNo_ref.Frozen = true;
            this.SlNo_ref.HeaderText = "Sl.No";
            this.SlNo_ref.Name = "SlNo_ref";
            this.SlNo_ref.ReadOnly = true;
            this.SlNo_ref.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SlNo_ref.Width = 40;
            // 
            // HAMH_APPL_NUMBER
            // 
            this.HAMH_APPL_NUMBER.Frozen = true;
            this.HAMH_APPL_NUMBER.HeaderText = "ApplNo";
            this.HAMH_APPL_NUMBER.Name = "HAMH_APPL_NUMBER";
            this.HAMH_APPL_NUMBER.ReadOnly = true;
            this.HAMH_APPL_NUMBER.Width = 85;
            // 
            // HAMH_EORA_CODE
            // 
            this.HAMH_EORA_CODE.Frozen = true;
            this.HAMH_EORA_CODE.HeaderText = "ECode";
            this.HAMH_EORA_CODE.Name = "HAMH_EORA_CODE";
            this.HAMH_EORA_CODE.ReadOnly = true;
            this.HAMH_EORA_CODE.Width = 85;
            // 
            // HAMH_NAME
            // 
            this.HAMH_NAME.Frozen = true;
            this.HAMH_NAME.HeaderText = "Name";
            this.HAMH_NAME.Name = "HAMH_NAME";
            this.HAMH_NAME.ReadOnly = true;
            this.HAMH_NAME.Width = 150;
            // 
            // HAMH_FORH_NAME
            // 
            this.HAMH_FORH_NAME.Frozen = true;
            this.HAMH_FORH_NAME.HeaderText = "Father Name";
            this.HAMH_FORH_NAME.Name = "HAMH_FORH_NAME";
            this.HAMH_FORH_NAME.ReadOnly = true;
            this.HAMH_FORH_NAME.Width = 150;
            // 
            // HAMH_DOB
            // 
            this.HAMH_DOB.Frozen = true;
            this.HAMH_DOB.HeaderText = "DOB";
            this.HAMH_DOB.Name = "HAMH_DOB";
            this.HAMH_DOB.ReadOnly = true;
            this.HAMH_DOB.Width = 80;
            // 
            // HAMH_DOJ
            // 
            this.HAMH_DOJ.Frozen = true;
            this.HAMH_DOJ.HeaderText = "DOJ";
            this.HAMH_DOJ.Name = "HAMH_DOJ";
            this.HAMH_DOJ.ReadOnly = true;
            this.HAMH_DOJ.Width = 80;
            // 
            // STATUS
            // 
            this.STATUS.Frozen = true;
            this.STATUS.HeaderText = "Status";
            this.STATUS.Name = "STATUS";
            this.STATUS.ReadOnly = true;
            // 
            // Delete
            // 
            this.Delete.Frozen = true;
            this.Delete.HeaderText = "Del";
            this.Delete.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Width = 40;
            // 
            // MultipleEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 466);
            this.ControlBox = false;
            this.Controls.Add(this.gvMultiple);
            this.Controls.Add(this.groupBox1);
            this.Name = "MultipleEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Multiple Entry";
            this.Load += new System.EventHandler(this.MultipleEntry_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvMultiple)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.DataGridView gvMultiple;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlNo_ref;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_APPL_NUMBER;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_EORA_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_FORH_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_DOB;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAMH_DOJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
    }
}