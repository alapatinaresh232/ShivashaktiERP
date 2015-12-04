namespace SSCRM
{
    partial class ReportFilters
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNoofRecords = new System.Windows.Forms.TextBox();
            this.lblNoOfRec = new System.Windows.Forms.Label();
            this.lblLOS = new System.Windows.Forms.Label();
            this.dtpLOSAsOnDate = new System.Windows.Forms.DateTimePicker();
            this.lblToDesgId = new System.Windows.Forms.Label();
            this.cbToDesigId = new System.Windows.Forms.ComboBox();
            this.lblFrmDesg = new System.Windows.Forms.Label();
            this.cbFrmDesg = new System.Windows.Forms.ComboBox();
            this.cbSortBy = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFrm = new System.Windows.Forms.Label();
            this.txtToGrps = new System.Windows.Forms.TextBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.txtFrmGrps = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtToGrpPerMnth = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFrmGrpPerMnth = new System.Windows.Forms.TextBox();
            this.chkTotalGrps = new System.Windows.Forms.CheckBox();
            this.lblSortBy = new System.Windows.Forms.Label();
            this.lblToDoc = new System.Windows.Forms.Label();
            this.dtpToDoc = new System.Windows.Forms.DateTimePicker();
            this.lblDocm = new System.Windows.Forms.Label();
            this.dtpFromDoc = new System.Windows.Forms.DateTimePicker();
            this.tvBranches = new SSCRM.TriStateTreeView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkGrpPerMnth = new System.Windows.Forms.CheckBox();
            this.lblGroup = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.txtNoofRecords);
            this.groupBox1.Controls.Add(this.lblNoOfRec);
            this.groupBox1.Controls.Add(this.lblLOS);
            this.groupBox1.Controls.Add(this.dtpLOSAsOnDate);
            this.groupBox1.Controls.Add(this.lblToDesgId);
            this.groupBox1.Controls.Add(this.cbToDesigId);
            this.groupBox1.Controls.Add(this.lblFrmDesg);
            this.groupBox1.Controls.Add(this.cbFrmDesg);
            this.groupBox1.Controls.Add(this.cbSortBy);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblFrm);
            this.groupBox1.Controls.Add(this.txtToGrps);
            this.groupBox1.Controls.Add(this.lblTo);
            this.groupBox1.Controls.Add(this.txtFrmGrps);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtToGrpPerMnth);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtFrmGrpPerMnth);
            this.groupBox1.Controls.Add(this.chkTotalGrps);
            this.groupBox1.Controls.Add(this.lblSortBy);
            this.groupBox1.Controls.Add(this.lblToDoc);
            this.groupBox1.Controls.Add(this.dtpToDoc);
            this.groupBox1.Controls.Add(this.lblDocm);
            this.groupBox1.Controls.Add(this.dtpFromDoc);
            this.groupBox1.Controls.Add(this.tvBranches);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkGrpPerMnth);
            this.groupBox1.Controls.Add(this.lblGroup);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 557);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtNoofRecords
            // 
            this.txtNoofRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoofRecords.Location = new System.Drawing.Point(340, 101);
            this.txtNoofRecords.MaxLength = 10;
            this.txtNoofRecords.Name = "txtNoofRecords";
            this.txtNoofRecords.Size = new System.Drawing.Size(90, 22);
            this.txtNoofRecords.TabIndex = 22;
            this.txtNoofRecords.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoofRecords_KeyPress);
            // 
            // lblNoOfRec
            // 
            this.lblNoOfRec.AutoSize = true;
            this.lblNoOfRec.BackColor = System.Drawing.Color.PowderBlue;
            this.lblNoOfRec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOfRec.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblNoOfRec.Location = new System.Drawing.Point(227, 104);
            this.lblNoOfRec.Name = "lblNoOfRec";
            this.lblNoOfRec.Size = new System.Drawing.Size(98, 15);
            this.lblNoOfRec.TabIndex = 21;
            this.lblNoOfRec.Text = "No of Records";
            // 
            // lblLOS
            // 
            this.lblLOS.AutoSize = true;
            this.lblLOS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLOS.ForeColor = System.Drawing.Color.Navy;
            this.lblLOS.Location = new System.Drawing.Point(209, 105);
            this.lblLOS.Name = "lblLOS";
            this.lblLOS.Size = new System.Drawing.Size(119, 16);
            this.lblLOS.TabIndex = 39;
            this.lblLOS.Text = "LOS As On Date";
            // 
            // dtpLOSAsOnDate
            // 
            this.dtpLOSAsOnDate.CustomFormat = "dd/MMM/yyyy";
            this.dtpLOSAsOnDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpLOSAsOnDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpLOSAsOnDate.Location = new System.Drawing.Point(329, 102);
            this.dtpLOSAsOnDate.Name = "dtpLOSAsOnDate";
            this.dtpLOSAsOnDate.Size = new System.Drawing.Size(106, 22);
            this.dtpLOSAsOnDate.TabIndex = 40;
            // 
            // lblToDesgId
            // 
            this.lblToDesgId.AutoSize = true;
            this.lblToDesgId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDesgId.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblToDesgId.Location = new System.Drawing.Point(244, 74);
            this.lblToDesgId.Name = "lblToDesgId";
            this.lblToDesgId.Size = new System.Drawing.Size(72, 16);
            this.lblToDesgId.TabIndex = 16;
            this.lblToDesgId.Text = "To Desig";
            // 
            // cbToDesigId
            // 
            this.cbToDesigId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbToDesigId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbToDesigId.FormattingEnabled = true;
            this.cbToDesigId.Location = new System.Drawing.Point(325, 71);
            this.cbToDesigId.Name = "cbToDesigId";
            this.cbToDesigId.Size = new System.Drawing.Size(105, 23);
            this.cbToDesigId.TabIndex = 18;
            // 
            // lblFrmDesg
            // 
            this.lblFrmDesg.AutoSize = true;
            this.lblFrmDesg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrmDesg.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblFrmDesg.Location = new System.Drawing.Point(16, 71);
            this.lblFrmDesg.Name = "lblFrmDesg";
            this.lblFrmDesg.Size = new System.Drawing.Size(88, 16);
            this.lblFrmDesg.TabIndex = 12;
            this.lblFrmDesg.Text = "From Desig";
            // 
            // cbFrmDesg
            // 
            this.cbFrmDesg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFrmDesg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFrmDesg.FormattingEnabled = true;
            this.cbFrmDesg.Location = new System.Drawing.Point(109, 70);
            this.cbFrmDesg.Name = "cbFrmDesg";
            this.cbFrmDesg.Size = new System.Drawing.Size(99, 23);
            this.cbFrmDesg.TabIndex = 13;
            this.cbFrmDesg.SelectedIndexChanged += new System.EventHandler(this.cbFrmDesg_SelectedIndexChanged);
            // 
            // cbSortBy
            // 
            this.cbSortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSortBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSortBy.FormattingEnabled = true;
            this.cbSortBy.Location = new System.Drawing.Point(96, 101);
            this.cbSortBy.Name = "cbSortBy";
            this.cbSortBy.Size = new System.Drawing.Size(112, 23);
            this.cbSortBy.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(42, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Document Month";
            // 
            // lblFrm
            // 
            this.lblFrm.AutoSize = true;
            this.lblFrm.BackColor = System.Drawing.Color.PowderBlue;
            this.lblFrm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrm.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblFrm.Location = new System.Drawing.Point(162, 75);
            this.lblFrm.Name = "lblFrm";
            this.lblFrm.Size = new System.Drawing.Size(40, 15);
            this.lblFrm.TabIndex = 14;
            this.lblFrm.Text = "From";
            // 
            // txtToGrps
            // 
            this.txtToGrps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToGrps.Location = new System.Drawing.Point(340, 73);
            this.txtToGrps.MaxLength = 10;
            this.txtToGrps.Name = "txtToGrps";
            this.txtToGrps.Size = new System.Drawing.Size(90, 22);
            this.txtToGrps.TabIndex = 14;
            this.txtToGrps.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtToGrps_KeyPress);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.BackColor = System.Drawing.Color.PowderBlue;
            this.lblTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblTo.Location = new System.Drawing.Point(314, 76);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(23, 15);
            this.lblTo.TabIndex = 17;
            this.lblTo.Text = "To";
            // 
            // txtFrmGrps
            // 
            this.txtFrmGrps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrmGrps.Location = new System.Drawing.Point(206, 72);
            this.txtFrmGrps.MaxLength = 10;
            this.txtFrmGrps.Name = "txtFrmGrps";
            this.txtFrmGrps.Size = new System.Drawing.Size(89, 22);
            this.txtFrmGrps.TabIndex = 15;
            this.txtFrmGrps.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFrmGrps_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.PowderBlue;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(162, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "From";
            // 
            // txtToGrpPerMnth
            // 
            this.txtToGrpPerMnth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToGrpPerMnth.Location = new System.Drawing.Point(340, 45);
            this.txtToGrpPerMnth.MaxLength = 10;
            this.txtToGrpPerMnth.Name = "txtToGrpPerMnth";
            this.txtToGrpPerMnth.Size = new System.Drawing.Size(90, 22);
            this.txtToGrpPerMnth.TabIndex = 10;
            this.txtToGrpPerMnth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtToGrpPerMnth_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.PowderBlue;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(314, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "To";
            // 
            // txtFrmGrpPerMnth
            // 
            this.txtFrmGrpPerMnth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrmGrpPerMnth.Location = new System.Drawing.Point(206, 44);
            this.txtFrmGrpPerMnth.MaxLength = 10;
            this.txtFrmGrpPerMnth.Name = "txtFrmGrpPerMnth";
            this.txtFrmGrpPerMnth.Size = new System.Drawing.Size(89, 22);
            this.txtFrmGrpPerMnth.TabIndex = 8;
            this.txtFrmGrpPerMnth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFrmGrpPerMnth_KeyPress);
            // 
            // chkTotalGrps
            // 
            this.chkTotalGrps.AutoSize = true;
            this.chkTotalGrps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTotalGrps.ForeColor = System.Drawing.Color.Navy;
            this.chkTotalGrps.Location = new System.Drawing.Point(15, 70);
            this.chkTotalGrps.Name = "chkTotalGrps";
            this.chkTotalGrps.Size = new System.Drawing.Size(108, 19);
            this.chkTotalGrps.TabIndex = 11;
            this.chkTotalGrps.Text = "Total Groups";
            this.chkTotalGrps.UseVisualStyleBackColor = true;
            // 
            // lblSortBy
            // 
            this.lblSortBy.AutoSize = true;
            this.lblSortBy.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSortBy.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblSortBy.Location = new System.Drawing.Point(11, 104);
            this.lblSortBy.Name = "lblSortBy";
            this.lblSortBy.Size = new System.Drawing.Size(56, 16);
            this.lblSortBy.TabIndex = 19;
            this.lblSortBy.Text = "Sort By";
            // 
            // lblToDoc
            // 
            this.lblToDoc.AutoSize = true;
            this.lblToDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDoc.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblToDoc.Location = new System.Drawing.Point(313, 18);
            this.lblToDoc.Name = "lblToDoc";
            this.lblToDoc.Size = new System.Drawing.Size(23, 15);
            this.lblToDoc.TabIndex = 3;
            this.lblToDoc.Text = "To";
            // 
            // dtpToDoc
            // 
            this.dtpToDoc.CustomFormat = "MMM/yyyy";
            this.dtpToDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDoc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDoc.Location = new System.Drawing.Point(340, 15);
            this.dtpToDoc.Name = "dtpToDoc";
            this.dtpToDoc.Size = new System.Drawing.Size(90, 22);
            this.dtpToDoc.TabIndex = 4;
            // 
            // lblDocm
            // 
            this.lblDocm.AutoSize = true;
            this.lblDocm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocm.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblDocm.Location = new System.Drawing.Point(157, 18);
            this.lblDocm.Name = "lblDocm";
            this.lblDocm.Size = new System.Drawing.Size(44, 15);
            this.lblDocm.TabIndex = 1;
            this.lblDocm.Text = " From";
            // 
            // dtpFromDoc
            // 
            this.dtpFromDoc.CustomFormat = "MMM/yyyy";
            this.dtpFromDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDoc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDoc.Location = new System.Drawing.Point(205, 15);
            this.dtpFromDoc.Name = "dtpFromDoc";
            this.dtpFromDoc.Size = new System.Drawing.Size(90, 22);
            this.dtpFromDoc.TabIndex = 2;
            // 
            // tvBranches
            // 
            this.tvBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvBranches.Location = new System.Drawing.Point(14, 147);
            this.tvBranches.Name = "tvBranches";
            this.tvBranches.Size = new System.Drawing.Size(410, 359);
            this.tvBranches.TabIndex = 24;
            this.tvBranches.TriStateStyleProperty = SSCRM.TriStateTreeView.TriStateStyles.Standard;
            this.tvBranches.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvBranches_AfterCheck);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnDownload);
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnCancel);
            this.groupBox4.Controls.Add(this.btnReport);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(23, 507);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(404, 45);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            // 
            // btnDownload
            // 
            this.btnDownload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDownload.BackColor = System.Drawing.Color.OliveDrab;
            this.btnDownload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnDownload.FlatAppearance.BorderSize = 5;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Image = global::SSCRM.Properties.Resources.ic_download;
            this.btnDownload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDownload.Location = new System.Drawing.Point(272, 13);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(1);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(97, 24);
            this.btnDownload.TabIndex = 3;
            this.btnDownload.Tag = "Product  Search";
            this.btnDownload.Text = "Download";
            this.btnDownload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnExit.Location = new System.Drawing.Point(193, 13);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 26);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "C&lose";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SandyBrown;
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(114, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Clea&r";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReport
            // 
            this.btnReport.AutoEllipsis = true;
            this.btnReport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnReport.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnReport.Location = new System.Drawing.Point(35, 13);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(74, 26);
            this.btnReport.TabIndex = 0;
            this.btnReport.Text = "&Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(14, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 15);
            this.label1.TabIndex = 23;
            this.label1.Text = "Company/Branch";
            // 
            // chkGrpPerMnth
            // 
            this.chkGrpPerMnth.AutoSize = true;
            this.chkGrpPerMnth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGrpPerMnth.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.chkGrpPerMnth.Location = new System.Drawing.Point(25, 45);
            this.chkGrpPerMnth.Name = "chkGrpPerMnth";
            this.chkGrpPerMnth.Size = new System.Drawing.Size(142, 19);
            this.chkGrpPerMnth.TabIndex = 5;
            this.chkGrpPerMnth.Text = "Groups Per Month";
            this.chkGrpPerMnth.UseVisualStyleBackColor = true;
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.BackColor = System.Drawing.Color.PowderBlue;
            this.lblGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroup.ForeColor = System.Drawing.Color.Navy;
            this.lblGroup.Location = new System.Drawing.Point(33, 47);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(99, 15);
            this.lblGroup.TabIndex = 6;
            this.lblGroup.Text = "Groups Range";
            // 
            // ReportFilters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(451, 562);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Name = "ReportFilters";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Filter";
            this.Load += new System.EventHandler(this.BranchReportFilter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private TriStateTreeView tvBranches;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblToDoc;
        private System.Windows.Forms.DateTimePicker dtpToDoc;
        private System.Windows.Forms.Label lblDocm;
        private System.Windows.Forms.DateTimePicker dtpFromDoc;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.TextBox txtNoofRecords;
        private System.Windows.Forms.Label lblNoOfRec;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label lblSortBy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtToGrpPerMnth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFrmGrpPerMnth;
        private System.Windows.Forms.CheckBox chkTotalGrps;
        private System.Windows.Forms.CheckBox chkGrpPerMnth;
        private System.Windows.Forms.Label lblFrm;
        private System.Windows.Forms.TextBox txtToGrps;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.TextBox txtFrmGrps;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.ComboBox cbSortBy;
        private System.Windows.Forms.Label lblFrmDesg;
        private System.Windows.Forms.ComboBox cbFrmDesg;
        private System.Windows.Forms.Label lblToDesgId;
        private System.Windows.Forms.ComboBox cbToDesigId;
        private System.Windows.Forms.Label lblLOS;
        private System.Windows.Forms.DateTimePicker dtpLOSAsOnDate;
    }
}