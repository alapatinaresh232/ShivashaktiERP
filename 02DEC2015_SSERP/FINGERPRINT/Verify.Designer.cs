namespace ZkFingerDemo
{
    partial class Verify
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Verify));
            this.ZKFPEngX1 = new AxZKFPEngXControl.AxZKFPEngX();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblresult = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ZKFPEngX1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ZKFPEngX1
            // 
            this.ZKFPEngX1.Enabled = true;
            this.ZKFPEngX1.Location = new System.Drawing.Point(225, 238);
            this.ZKFPEngX1.Name = "ZKFPEngX1";
            this.ZKFPEngX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("ZKFPEngX1.OcxState")));
            this.ZKFPEngX1.Size = new System.Drawing.Size(24, 24);
            this.ZKFPEngX1.TabIndex = 23;
            this.ZKFPEngX1.Visible = false;
            this.ZKFPEngX1.OnImageReceived += new AxZKFPEngXControl.IZKFPEngXEvents_OnImageReceivedEventHandler(this.ZKFPEngX1_OnImageReceived);
            this.ZKFPEngX1.OnEnroll += new AxZKFPEngXControl.IZKFPEngXEvents_OnEnrollEventHandler(this.ZKFPEngX1_OnEnroll);
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.PictureBox1.Location = new System.Drawing.Point(54, 61);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(161, 178);
            this.PictureBox1.TabIndex = 22;
            this.PictureBox1.TabStop = false;
            // 
            // lblresult
            // 
            this.lblresult.AutoSize = true;
            this.lblresult.Location = new System.Drawing.Point(73, 399);
            this.lblresult.Name = "lblresult";
            this.lblresult.Size = new System.Drawing.Size(0, 13);
            this.lblresult.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(18, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Please put Finger on the Divice ";
            // 
            // Verify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(268, 271);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ZKFPEngX1);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.lblresult);
            this.Name = "Verify";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Verify";
            this.Load += new System.EventHandler(this.Verify_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ZKFPEngX1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxZKFPEngXControl.AxZKFPEngX ZKFPEngX1;
        private System.Windows.Forms.Label lblresult;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.PictureBox PictureBox1;

    }
}