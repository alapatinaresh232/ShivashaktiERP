namespace ZkFingerDemo
{
    partial class FingerDemo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FingerDemo));
            this.lblresult = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.ZKFPEngX1 = new AxZKFPEngXControl.AxZKFPEngX();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ZKFPEngX1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblresult
            // 
            this.lblresult.AutoSize = true;
            this.lblresult.Location = new System.Drawing.Point(12, 359);
            this.lblresult.Name = "lblresult";
            this.lblresult.Size = new System.Drawing.Size(0, 13);
            this.lblresult.TabIndex = 16;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblMessage.Location = new System.Drawing.Point(79, 25);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(241, 17);
            this.lblMessage.TabIndex = 21;
            this.lblMessage.Text = "Please put Finger on the Divice ";
            // 
            // ZKFPEngX1
            // 
            this.ZKFPEngX1.Enabled = true;
            this.ZKFPEngX1.Location = new System.Drawing.Point(80, 294);
            this.ZKFPEngX1.Name = "ZKFPEngX1";
            this.ZKFPEngX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("ZKFPEngX1.OcxState")));
            this.ZKFPEngX1.Size = new System.Drawing.Size(24, 24);
            this.ZKFPEngX1.TabIndex = 20;
            this.ZKFPEngX1.Visible = false;
            this.ZKFPEngX1.OnImageReceived += new AxZKFPEngXControl.IZKFPEngXEvents_OnImageReceivedEventHandler(this.ZKFPEngX1_OnImageReceived);
            this.ZKFPEngX1.OnEnroll += new AxZKFPEngXControl.IZKFPEngXEvents_OnEnrollEventHandler(this.ZKFPEngX1_OnEnroll);
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.PictureBox1.Location = new System.Drawing.Point(113, 62);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(165, 177);
            this.PictureBox1.TabIndex = 17;
            this.PictureBox1.TabStop = false;
            // 
            // FingerDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(395, 273);
            this.ControlBox = false;
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.ZKFPEngX1);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.lblresult);
            this.Name = "FingerDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.FingerDemo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ZKFPEngX1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblresult;
        private AxZKFPEngXControl.AxZKFPEngX ZKFPEngX1;
        public System.Windows.Forms.PictureBox PictureBox1;
        public System.Windows.Forms.Label lblMessage;

    }
}

