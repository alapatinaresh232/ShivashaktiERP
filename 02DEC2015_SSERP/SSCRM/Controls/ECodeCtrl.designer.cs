namespace SSCRM
{
    partial class ECodeCtrl
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
            this.label5 = new System.Windows.Forms.Label();
            this.txtECode = new System.Windows.Forms.TextBox();
            this.txtEName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(1, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 17);
            this.label5.TabIndex = 57;
            this.label5.Text = "E Code";
            // 
            // txtECode
            // 
            this.txtECode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtECode.Location = new System.Drawing.Point(50, 2);
            this.txtECode.MaxLength = 6;
            this.txtECode.Name = "txtECode";
            this.txtECode.Size = new System.Drawing.Size(84, 22);
            this.txtECode.TabIndex = 58;
            this.txtECode.TextChanged += new System.EventHandler(this.txtECode_TextChanged);
            this.txtECode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtECode_KeyUp);
            this.txtECode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtECode_KeyPress);
            // 
            // txtEName
            // 
            this.txtEName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEName.Location = new System.Drawing.Point(136, 2);
            this.txtEName.MaxLength = 6;
            this.txtEName.Name = "txtEName";
            this.txtEName.ReadOnly = true;
            this.txtEName.Size = new System.Drawing.Size(238, 22);
            this.txtEName.TabIndex = 59;
            // 
            // ECodeCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.txtECode);
            this.Controls.Add(this.txtEName);
            this.Controls.Add(this.label5);
            this.Name = "ECodeCtrl";
            this.Size = new System.Drawing.Size(378, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtECode;
        private System.Windows.Forms.TextBox txtEName;
    }
}
