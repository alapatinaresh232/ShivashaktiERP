namespace SSCRM
{
    partial class frmDemoPlotResults
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtEcodeSearch = new System.Windows.Forms.TextBox();
            this.cbEcode = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.rtbFarmerOpinion = new System.Windows.Forms.RichTextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbNotifyResult = new System.Windows.Forms.RichTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpObservDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label14 = new System.Windows.Forms.Label();
            this.btnBrowseCropArea = new System.Windows.Forms.Button();
            this.picCropArea = new System.Windows.Forms.PictureBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTreatedAreaImage = new System.Windows.Forms.Button();
            this.picTreatedArea = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCropArea)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTreatedArea)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.splitContainer2);
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtEcodeSearch);
            this.groupBox1.Controls.Add(this.cbEcode);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.rtbFarmerOpinion);
            this.groupBox1.Controls.Add(this.groupBox8);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rtbNotifyResult);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.dtpObservDate);
            this.groupBox1.Controls.Add(this.lblDate);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(941, 567);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Demo Plot Result Details";
            // 
            // txtRemarks
            // 
            this.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(11, 233);
            this.txtRemarks.MaxLength = 100;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(920, 47);
            this.txtRemarks.TabIndex = 10;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label17.Location = new System.Drawing.Point(10, 216);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(109, 16);
            this.label17.TabIndex = 9;
            this.label17.Text = "Ao Suggestion";
            // 
            // txtEcodeSearch
            // 
            this.txtEcodeSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcodeSearch.Location = new System.Drawing.Point(289, 17);
            this.txtEcodeSearch.MaxLength = 20;
            this.txtEcodeSearch.Name = "txtEcodeSearch";
            this.txtEcodeSearch.Size = new System.Drawing.Size(85, 21);
            this.txtEcodeSearch.TabIndex = 1;
            this.txtEcodeSearch.TextChanged += new System.EventHandler(this.txtEcodeSearch_TextChanged);
            // 
            // cbEcode
            // 
            this.cbEcode.AllowDrop = true;
            this.cbEcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cbEcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEcode.FormattingEnabled = true;
            this.cbEcode.Location = new System.Drawing.Point(375, 16);
            this.cbEcode.Name = "cbEcode";
            this.cbEcode.Size = new System.Drawing.Size(323, 23);
            this.cbEcode.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label16.Location = new System.Drawing.Point(192, 19);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(93, 16);
            this.label16.TabIndex = 0;
            this.label16.Text = "Name Of AO";
            // 
            // rtbFarmerOpinion
            // 
            this.rtbFarmerOpinion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbFarmerOpinion.Location = new System.Drawing.Point(7, 142);
            this.rtbFarmerOpinion.Name = "rtbFarmerOpinion";
            this.rtbFarmerOpinion.Size = new System.Drawing.Size(927, 68);
            this.rtbFarmerOpinion.TabIndex = 8;
            this.rtbFarmerOpinion.Text = "";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnClose);
            this.groupBox8.Controls.Add(this.btnClear);
            this.groupBox8.Controls.Add(this.btnSave);
            this.groupBox8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox8.Location = new System.Drawing.Point(322, 519);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(297, 45);
            this.groupBox8.TabIndex = 11;
            this.groupBox8.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(189, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 26);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "C&lose";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClear.Location = new System.Drawing.Point(111, 13);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(74, 26);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clea&r";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoEllipsis = true;
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(34, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(6, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Farmer Opinion";
            // 
            // rtbNotifyResult
            // 
            this.rtbNotifyResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbNotifyResult.Location = new System.Drawing.Point(7, 43);
            this.rtbNotifyResult.Name = "rtbNotifyResult";
            this.rtbNotifyResult.Size = new System.Drawing.Size(925, 77);
            this.rtbNotifyResult.TabIndex = 6;
            this.rtbNotifyResult.Text = "";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label12.Location = new System.Drawing.Point(6, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 17);
            this.label12.TabIndex = 5;
            this.label12.Text = "Notify Result";
            // 
            // dtpObservDate
            // 
            this.dtpObservDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpObservDate.CustomFormat = "dd/MM/yyyy";
            this.dtpObservDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpObservDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpObservDate.Location = new System.Drawing.Point(829, 19);
            this.dtpObservDate.Name = "dtpObservDate";
            this.dtpObservDate.Size = new System.Drawing.Size(100, 22);
            this.dtpObservDate.TabIndex = 4;
            this.dtpObservDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblDate.Location = new System.Drawing.Point(711, 20);
            this.lblDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(117, 17);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "Observed Date";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(6, 286);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label14);
            this.splitContainer1.Panel1.Controls.Add(this.btnBrowseCropArea);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.picCropArea);
            this.splitContainer1.Size = new System.Drawing.Size(459, 239);
            this.splitContainer1.SplitterDistance = 26;
            this.splitContainer1.TabIndex = 98;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label14.Location = new System.Drawing.Point(82, 6);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(198, 16);
            this.label14.TabIndex = 70;
            this.label14.Text = "Crop Area Image Download";
            // 
            // btnBrowseCropArea
            // 
            this.btnBrowseCropArea.BackColor = System.Drawing.Color.AliceBlue;
            this.btnBrowseCropArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnBrowseCropArea.Location = new System.Drawing.Point(293, 2);
            this.btnBrowseCropArea.Name = "btnBrowseCropArea";
            this.btnBrowseCropArea.Size = new System.Drawing.Size(101, 24);
            this.btnBrowseCropArea.TabIndex = 94;
            this.btnBrowseCropArea.Text = "Browse";
            this.btnBrowseCropArea.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBrowseCropArea.UseVisualStyleBackColor = false;
            this.btnBrowseCropArea.Click += new System.EventHandler(this.btnBrowseCropArea_Click);
            // 
            // picCropArea
            // 
            this.picCropArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCropArea.Location = new System.Drawing.Point(6, 0);
            this.picCropArea.Name = "picCropArea";
            this.picCropArea.Size = new System.Drawing.Size(440, 202);
            this.picCropArea.TabIndex = 0;
            this.picCropArea.TabStop = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Location = new System.Drawing.Point(475, 286);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.btnTreatedAreaImage);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.Controls.Add(this.picTreatedArea);
            this.splitContainer2.Size = new System.Drawing.Size(459, 239);
            this.splitContainer2.SplitterDistance = 26;
            this.splitContainer2.TabIndex = 99;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(61, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 16);
            this.label2.TabIndex = 70;
            this.label2.Text = "Treated Area Image Download";
            // 
            // btnTreatedAreaImage
            // 
            this.btnTreatedAreaImage.BackColor = System.Drawing.Color.AliceBlue;
            this.btnTreatedAreaImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnTreatedAreaImage.Location = new System.Drawing.Point(293, 2);
            this.btnTreatedAreaImage.Name = "btnTreatedAreaImage";
            this.btnTreatedAreaImage.Size = new System.Drawing.Size(101, 24);
            this.btnTreatedAreaImage.TabIndex = 94;
            this.btnTreatedAreaImage.Text = "Browse";
            this.btnTreatedAreaImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTreatedAreaImage.UseVisualStyleBackColor = false;
            this.btnTreatedAreaImage.Click += new System.EventHandler(this.btnTreatedAreaImage_Click);
            // 
            // picTreatedArea
            // 
            this.picTreatedArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picTreatedArea.Location = new System.Drawing.Point(6, 0);
            this.picTreatedArea.Name = "picTreatedArea";
            this.picTreatedArea.Size = new System.Drawing.Size(447, 200);
            this.picTreatedArea.TabIndex = 0;
            this.picTreatedArea.TabStop = false;
            // 
            // frmDemoPlotResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(945, 572);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDemoPlotResults";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDemoPlotResults";
            this.Load += new System.EventHandler(this.frmDemoPlotResults_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCropArea)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTreatedArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpObservDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.RichTextBox rtbFarmerOpinion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbNotifyResult;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtEcodeSearch;
        private System.Windows.Forms.ComboBox cbEcode;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTreatedAreaImage;
        public System.Windows.Forms.PictureBox picTreatedArea;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnBrowseCropArea;
        public System.Windows.Forms.PictureBox picCropArea;
    }
}