namespace SDMS
{
    partial class PrevTurnOver
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtPrevTurnOver = new System.Windows.Forms.TextBox();
            this.txtPrevTurnOverProduct = new System.Windows.Forms.TextBox();
            this.txtPrevTurnOverYear = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.PowderBlue;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouper1.BackgroundGradientMode = GroupCtrl.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.White;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.btnClose);
            this.grouper1.Controls.Add(this.btnSave);
            this.grouper1.Controls.Add(this.txtPrevTurnOver);
            this.grouper1.Controls.Add(this.txtPrevTurnOverProduct);
            this.grouper1.Controls.Add(this.txtPrevTurnOverYear);
            this.grouper1.Controls.Add(this.label2);
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.Controls.Add(this.label5);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "";
            this.grouper1.Location = new System.Drawing.Point(1, -9);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 3;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(281, 177);
            this.grouper1.TabIndex = 127;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.YellowGreen;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(146, 125);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 25);
            this.btnClose.TabIndex = 134;
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
            this.btnSave.Location = new System.Drawing.Point(70, 125);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 25);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtPrevTurnOver
            // 
            this.txtPrevTurnOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrevTurnOver.Location = new System.Drawing.Point(126, 83);
            this.txtPrevTurnOver.MaxLength = 20;
            this.txtPrevTurnOver.Name = "txtPrevTurnOver";
            this.txtPrevTurnOver.Size = new System.Drawing.Size(110, 21);
            this.txtPrevTurnOver.TabIndex = 2;
            this.txtPrevTurnOver.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrevTurnOver_KeyPress);
            // 
            // txtPrevTurnOverProduct
            // 
            this.txtPrevTurnOverProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrevTurnOverProduct.Location = new System.Drawing.Point(126, 54);
            this.txtPrevTurnOverProduct.MaxLength = 50;
            this.txtPrevTurnOverProduct.Name = "txtPrevTurnOverProduct";
            this.txtPrevTurnOverProduct.Size = new System.Drawing.Size(110, 21);
            this.txtPrevTurnOverProduct.TabIndex = 1;
            this.txtPrevTurnOverProduct.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrevTurnOverProduct_KeyPress);
            // 
            // txtPrevTurnOverYear
            // 
            this.txtPrevTurnOverYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrevTurnOverYear.Location = new System.Drawing.Point(126, 26);
            this.txtPrevTurnOverYear.MaxLength = 4;
            this.txtPrevTurnOverYear.Name = "txtPrevTurnOverYear";
            this.txtPrevTurnOverYear.Size = new System.Drawing.Size(110, 21);
            this.txtPrevTurnOverYear.TabIndex = 0;
            this.txtPrevTurnOverYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrevTurnOverYear_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(51, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 132;
            this.label2.Text = "Turn Over";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(64, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 15);
            this.label1.TabIndex = 131;
            this.label1.Text = "Product";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(84, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 15);
            this.label5.TabIndex = 130;
            this.label5.Text = "Year";
            // 
            // PrevTurnOver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(284, 169);
            this.ControlBox = false;
            this.Controls.Add(this.grouper1);
            this.Name = "PrevTurnOver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PrevTurnOver";
            this.Load += new System.EventHandler(this.PrevTurnOver_Load);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupCtrl.Grouper grouper1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtPrevTurnOver;
        private System.Windows.Forms.TextBox txtPrevTurnOverProduct;
        private System.Windows.Forms.TextBox txtPrevTurnOverYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}