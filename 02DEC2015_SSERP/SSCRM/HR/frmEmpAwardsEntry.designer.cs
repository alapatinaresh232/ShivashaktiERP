namespace SSCRM
{
    partial class frmEmpAwardsEntry
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn4 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn5 = new System.Windows.Forms.DataGridViewImageColumn();
            this.grp1 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtEventName = new System.Windows.Forms.TextBox();
            this.chkAddEvent = new System.Windows.Forms.CheckBox();
            this.txtPerfPnts = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtPerfDetails = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label16 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pbDocPic = new System.Windows.Forms.PictureBox();
            this.btnClearImage = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.gvEmpAwardDetl = new System.Windows.Forms.DataGridView();
            this.SLNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Zone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Region = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocMonth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PerfDetail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Points = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TripName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AwardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BackDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpImage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FinYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AwardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AwardDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Performance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorthOfGiftCheque = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Memento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.txtMementoType = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCash = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTripName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtAwardName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAwardType = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEName = new System.Windows.Forms.TextBox();
            this.txtEcodeSearch = new System.Windows.Forms.TextBox();
            this.lblEcode = new System.Windows.Forms.Label();
            this.txtEmpDesg = new System.Windows.Forms.TextBox();
            this.lblDesg = new System.Windows.Forms.Label();
            this.dtpAwardDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cbEventName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbRegion = new System.Windows.Forms.ComboBox();
            this.cbZones = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbLocation = new System.Windows.Forms.ComboBox();
            this.cbCompany = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbFinYear = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grp1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDocPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpAwardDetl)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Edit";
            this.dataGridViewImageColumn1.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 50;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "Del";
            this.dataGridViewImageColumn2.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.dataGridViewImageColumn2.MinimumWidth = 50;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.ReadOnly = true;
            this.dataGridViewImageColumn2.Width = 50;
            // 
            // dataGridViewImageColumn3
            // 
            this.dataGridViewImageColumn3.HeaderText = "Edit";
            this.dataGridViewImageColumn3.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
            this.dataGridViewImageColumn3.Visible = false;
            this.dataGridViewImageColumn3.Width = 40;
            // 
            // dataGridViewImageColumn4
            // 
            this.dataGridViewImageColumn4.HeaderText = "Del";
            this.dataGridViewImageColumn4.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.dataGridViewImageColumn4.Name = "dataGridViewImageColumn4";
            this.dataGridViewImageColumn4.ReadOnly = true;
            this.dataGridViewImageColumn4.Width = 40;
            // 
            // dataGridViewImageColumn5
            // 
            this.dataGridViewImageColumn5.HeaderText = "Del";
            this.dataGridViewImageColumn5.Image = global::SSCRM.Properties.Resources.actions_delete;
            this.dataGridViewImageColumn5.MinimumWidth = 50;
            this.dataGridViewImageColumn5.Name = "dataGridViewImageColumn5";
            this.dataGridViewImageColumn5.ReadOnly = true;
            this.dataGridViewImageColumn5.Width = 50;
            // 
            // grp1
            // 
            this.grp1.BackColor = System.Drawing.Color.PowderBlue;
            this.grp1.Controls.Add(this.btnAdd);
            this.grp1.Controls.Add(this.txtEventName);
            this.grp1.Controls.Add(this.chkAddEvent);
            this.grp1.Controls.Add(this.txtPerfPnts);
            this.grp1.Controls.Add(this.label17);
            this.grp1.Controls.Add(this.txtPerfDetails);
            this.grp1.Controls.Add(this.label18);
            this.grp1.Controls.Add(this.label15);
            this.grp1.Controls.Add(this.btnBrowse);
            this.grp1.Controls.Add(this.splitContainer1);
            this.grp1.Controls.Add(this.btnClearImage);
            this.grp1.Controls.Add(this.label14);
            this.grp1.Controls.Add(this.gvEmpAwardDetl);
            this.grp1.Controls.Add(this.txtMementoType);
            this.grp1.Controls.Add(this.label13);
            this.grp1.Controls.Add(this.txtCash);
            this.grp1.Controls.Add(this.label12);
            this.grp1.Controls.Add(this.txtTripName);
            this.grp1.Controls.Add(this.label11);
            this.grp1.Controls.Add(this.txtAwardName);
            this.grp1.Controls.Add(this.label10);
            this.grp1.Controls.Add(this.txtAwardType);
            this.grp1.Controls.Add(this.label7);
            this.grp1.Controls.Add(this.txtEName);
            this.grp1.Controls.Add(this.txtEcodeSearch);
            this.grp1.Controls.Add(this.lblEcode);
            this.grp1.Controls.Add(this.txtEmpDesg);
            this.grp1.Controls.Add(this.lblDesg);
            this.grp1.Controls.Add(this.dtpAwardDate);
            this.grp1.Controls.Add(this.label3);
            this.grp1.Controls.Add(this.cbEventName);
            this.grp1.Controls.Add(this.label1);
            this.grp1.Controls.Add(this.cbRegion);
            this.grp1.Controls.Add(this.cbZones);
            this.grp1.Controls.Add(this.groupBox1);
            this.grp1.Controls.Add(this.label9);
            this.grp1.Controls.Add(this.label8);
            this.grp1.Controls.Add(this.cbLocation);
            this.grp1.Controls.Add(this.cbCompany);
            this.grp1.Controls.Add(this.label5);
            this.grp1.Controls.Add(this.label6);
            this.grp1.Controls.Add(this.cbFinYear);
            this.grp1.Controls.Add(this.label2);
            this.grp1.Location = new System.Drawing.Point(3, 3);
            this.grp1.Name = "grp1";
            this.grp1.Size = new System.Drawing.Size(961, 577);
            this.grp1.TabIndex = 0;
            this.grp1.TabStop = false;
            // 
            // btnAdd
            // 
            this.btnAdd.AutoEllipsis = true;
            this.btnAdd.BackColor = System.Drawing.Color.LightCyan;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(446, 127);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(43, 25);
            this.btnAdd.TabIndex = 18;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtEventName
            // 
            this.txtEventName.BackColor = System.Drawing.Color.White;
            this.txtEventName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEventName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEventName.Location = new System.Drawing.Point(121, 128);
            this.txtEventName.MaxLength = 200;
            this.txtEventName.Name = "txtEventName";
            this.txtEventName.Size = new System.Drawing.Size(298, 22);
            this.txtEventName.TabIndex = 16;
            this.txtEventName.TabStop = false;
            // 
            // chkAddEvent
            // 
            this.chkAddEvent.AutoSize = true;
            this.chkAddEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAddEvent.Location = new System.Drawing.Point(429, 132);
            this.chkAddEvent.Name = "chkAddEvent";
            this.chkAddEvent.Size = new System.Drawing.Size(15, 14);
            this.chkAddEvent.TabIndex = 17;
            this.chkAddEvent.UseVisualStyleBackColor = true;
            this.chkAddEvent.CheckedChanged += new System.EventHandler(this.chkAddEvent_CheckedChanged);
            // 
            // txtPerfPnts
            // 
            this.txtPerfPnts.BackColor = System.Drawing.Color.White;
            this.txtPerfPnts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPerfPnts.Location = new System.Drawing.Point(389, 260);
            this.txtPerfPnts.MaxLength = 10;
            this.txtPerfPnts.Name = "txtPerfPnts";
            this.txtPerfPnts.Size = new System.Drawing.Size(99, 22);
            this.txtPerfPnts.TabIndex = 31;
            this.txtPerfPnts.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPerfPnts_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label17.Location = new System.Drawing.Point(333, 263);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 17);
            this.label17.TabIndex = 30;
            this.label17.Text = "Points";
            // 
            // txtPerfDetails
            // 
            this.txtPerfDetails.BackColor = System.Drawing.Color.White;
            this.txtPerfDetails.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPerfDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPerfDetails.Location = new System.Drawing.Point(120, 259);
            this.txtPerfDetails.MaxLength = 50;
            this.txtPerfDetails.Name = "txtPerfDetails";
            this.txtPerfDetails.Size = new System.Drawing.Size(209, 22);
            this.txtPerfDetails.TabIndex = 29;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label18.Location = new System.Drawing.Point(18, 262);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(100, 17);
            this.label18.TabIndex = 28;
            this.label18.Text = "Performance";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label15.Location = new System.Drawing.Point(593, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(137, 17);
            this.label15.TabIndex = 4;
            this.label15.Text = "Image Attachment";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.AliceBlue;
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnBrowse.Location = new System.Drawing.Point(742, 8);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(101, 30);
            this.btnBrowse.TabIndex = 5;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(499, 41);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label16);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1Collapsed = true;
            this.splitContainer1.Panel1MinSize = 15;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.pbDocPic);
            this.splitContainer1.Size = new System.Drawing.Size(450, 288);
            this.splitContainer1.SplitterDistance = 31;
            this.splitContainer1.TabIndex = 40;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label16.Location = new System.Drawing.Point(82, 11);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(142, 16);
            this.label16.TabIndex = 0;
            this.label16.Text = "Document Attached";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.AliceBlue;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(313, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "Browse";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // pbDocPic
            // 
            this.pbDocPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbDocPic.Location = new System.Drawing.Point(6, 6);
            this.pbDocPic.Name = "pbDocPic";
            this.pbDocPic.Size = new System.Drawing.Size(437, 275);
            this.pbDocPic.TabIndex = 0;
            this.pbDocPic.TabStop = false;
            // 
            // btnClearImage
            // 
            this.btnClearImage.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClearImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClearImage.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnClearImage.Location = new System.Drawing.Point(849, 7);
            this.btnClearImage.Name = "btnClearImage";
            this.btnClearImage.Size = new System.Drawing.Size(100, 31);
            this.btnClearImage.TabIndex = 6;
            this.btnClearImage.TabStop = false;
            this.btnClearImage.Text = "Clear Image";
            this.btnClearImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClearImage.UseVisualStyleBackColor = false;
            this.btnClearImage.Click += new System.EventHandler(this.btnClearImage_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Navy;
            this.label14.Location = new System.Drawing.Point(7, 340);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(247, 16);
            this.label14.TabIndex = 38;
            this.label14.Text = "Employee Previous Award  Details";
            // 
            // gvEmpAwardDetl
            // 
            this.gvEmpAwardDetl.AllowUserToAddRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Navy;
            this.gvEmpAwardDetl.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvEmpAwardDetl.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.gvEmpAwardDetl.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvEmpAwardDetl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gvEmpAwardDetl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEmpAwardDetl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNO,
            this.TrnId,
            this.CompCode,
            this.BranCode,
            this.Zone,
            this.Region,
            this.DocMonth,
            this.PerfDetail,
            this.Points,
            this.TripName,
            this.Cash,
            this.EventId,
            this.AwardType,
            this.BackDays,
            this.EmpImage,
            this.FinYear,
            this.EventName,
            this.AwardName,
            this.AwardDate,
            this.Performance,
            this.WorthOfGiftCheque,
            this.Memento,
            this.Edit});
            this.gvEmpAwardDetl.GridColor = System.Drawing.SystemColors.Desktop;
            this.gvEmpAwardDetl.Location = new System.Drawing.Point(7, 360);
            this.gvEmpAwardDetl.MultiSelect = false;
            this.gvEmpAwardDetl.Name = "gvEmpAwardDetl";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvEmpAwardDetl.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gvEmpAwardDetl.RowHeadersVisible = false;
            this.gvEmpAwardDetl.Size = new System.Drawing.Size(944, 167);
            this.gvEmpAwardDetl.TabIndex = 39;
            this.gvEmpAwardDetl.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvEmpAwardDetl_CellClick);
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
            // TrnId
            // 
            this.TrnId.HeaderText = "TrnId";
            this.TrnId.Name = "TrnId";
            this.TrnId.ReadOnly = true;
            this.TrnId.Visible = false;
            this.TrnId.Width = 50;
            // 
            // CompCode
            // 
            this.CompCode.HeaderText = "Comp Code";
            this.CompCode.Name = "CompCode";
            this.CompCode.ReadOnly = true;
            this.CompCode.Visible = false;
            // 
            // BranCode
            // 
            this.BranCode.HeaderText = "Brancode";
            this.BranCode.Name = "BranCode";
            this.BranCode.ReadOnly = true;
            this.BranCode.Visible = false;
            // 
            // Zone
            // 
            this.Zone.HeaderText = "Zone";
            this.Zone.Name = "Zone";
            this.Zone.ReadOnly = true;
            this.Zone.Visible = false;
            // 
            // Region
            // 
            this.Region.HeaderText = "Region";
            this.Region.Name = "Region";
            this.Region.ReadOnly = true;
            this.Region.Visible = false;
            // 
            // DocMonth
            // 
            this.DocMonth.HeaderText = "DocMonth";
            this.DocMonth.Name = "DocMonth";
            this.DocMonth.ReadOnly = true;
            this.DocMonth.Visible = false;
            // 
            // PerfDetail
            // 
            this.PerfDetail.HeaderText = "Perf";
            this.PerfDetail.Name = "PerfDetail";
            this.PerfDetail.ReadOnly = true;
            this.PerfDetail.Visible = false;
            // 
            // Points
            // 
            this.Points.HeaderText = "Points";
            this.Points.Name = "Points";
            this.Points.ReadOnly = true;
            this.Points.Visible = false;
            // 
            // TripName
            // 
            this.TripName.HeaderText = "Trip";
            this.TripName.Name = "TripName";
            this.TripName.ReadOnly = true;
            this.TripName.Visible = false;
            // 
            // Cash
            // 
            this.Cash.HeaderText = "Cash";
            this.Cash.Name = "Cash";
            this.Cash.ReadOnly = true;
            this.Cash.Visible = false;
            // 
            // EventId
            // 
            this.EventId.HeaderText = "EventId";
            this.EventId.Name = "EventId";
            this.EventId.ReadOnly = true;
            this.EventId.Visible = false;
            // 
            // AwardType
            // 
            this.AwardType.HeaderText = "AwardType";
            this.AwardType.Name = "AwardType";
            this.AwardType.ReadOnly = true;
            this.AwardType.Visible = false;
            // 
            // BackDays
            // 
            this.BackDays.Frozen = true;
            this.BackDays.HeaderText = "BackDays";
            this.BackDays.Name = "BackDays";
            this.BackDays.ReadOnly = true;
            this.BackDays.Visible = false;
            // 
            // EmpImage
            // 
            this.EmpImage.HeaderText = "Image";
            this.EmpImage.Name = "EmpImage";
            this.EmpImage.ReadOnly = true;
            this.EmpImage.Visible = false;
            // 
            // FinYear
            // 
            this.FinYear.HeaderText = "FinYear";
            this.FinYear.Name = "FinYear";
            this.FinYear.ReadOnly = true;
            this.FinYear.Width = 80;
            // 
            // EventName
            // 
            this.EventName.HeaderText = "Event Name";
            this.EventName.Name = "EventName";
            this.EventName.ReadOnly = true;
            this.EventName.Width = 200;
            // 
            // AwardName
            // 
            this.AwardName.HeaderText = "Award Name";
            this.AwardName.Name = "AwardName";
            this.AwardName.ReadOnly = true;
            this.AwardName.Width = 170;
            // 
            // AwardDate
            // 
            this.AwardDate.HeaderText = "Date";
            this.AwardDate.Name = "AwardDate";
            this.AwardDate.ReadOnly = true;
            this.AwardDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AwardDate.Width = 90;
            // 
            // Performance
            // 
            this.Performance.HeaderText = "Performance";
            this.Performance.Name = "Performance";
            this.Performance.ReadOnly = true;
            // 
            // WorthOfGiftCheque
            // 
            this.WorthOfGiftCheque.HeaderText = "Gift / Cheque";
            this.WorthOfGiftCheque.Name = "WorthOfGiftCheque";
            this.WorthOfGiftCheque.ReadOnly = true;
            this.WorthOfGiftCheque.Width = 120;
            // 
            // Memento
            // 
            this.Memento.HeaderText = "Memento";
            this.Memento.Name = "Memento";
            this.Memento.ReadOnly = true;
            this.Memento.Width = 90;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "";
            this.Edit.Image = global::SSCRM.Properties.Resources.actions_edit;
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Width = 30;
            // 
            // txtMementoType
            // 
            this.txtMementoType.BackColor = System.Drawing.Color.White;
            this.txtMementoType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMementoType.Location = new System.Drawing.Point(120, 308);
            this.txtMementoType.MaxLength = 100;
            this.txtMementoType.Name = "txtMementoType";
            this.txtMementoType.Size = new System.Drawing.Size(368, 22);
            this.txtMementoType.TabIndex = 37;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label13.Location = new System.Drawing.Point(2, 311);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(114, 17);
            this.label13.TabIndex = 36;
            this.label13.Text = "Memento Type";
            // 
            // txtCash
            // 
            this.txtCash.BackColor = System.Drawing.Color.White;
            this.txtCash.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCash.Location = new System.Drawing.Point(389, 284);
            this.txtCash.MaxLength = 10;
            this.txtCash.Name = "txtCash";
            this.txtCash.Size = new System.Drawing.Size(99, 22);
            this.txtCash.TabIndex = 35;
            this.txtCash.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCash_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label12.Location = new System.Drawing.Point(343, 287);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 17);
            this.label12.TabIndex = 34;
            this.label12.Text = "Cash";
            // 
            // txtTripName
            // 
            this.txtTripName.BackColor = System.Drawing.Color.White;
            this.txtTripName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTripName.Location = new System.Drawing.Point(120, 283);
            this.txtTripName.MaxLength = 100;
            this.txtTripName.Name = "txtTripName";
            this.txtTripName.Size = new System.Drawing.Size(209, 22);
            this.txtTripName.TabIndex = 33;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label11.Location = new System.Drawing.Point(32, 286);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 17);
            this.label11.TabIndex = 32;
            this.label11.Text = "Trip Name";
            // 
            // txtAwardName
            // 
            this.txtAwardName.BackColor = System.Drawing.Color.White;
            this.txtAwardName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAwardName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAwardName.Location = new System.Drawing.Point(120, 235);
            this.txtAwardName.MaxLength = 200;
            this.txtAwardName.Name = "txtAwardName";
            this.txtAwardName.Size = new System.Drawing.Size(370, 22);
            this.txtAwardName.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label10.Location = new System.Drawing.Point(19, 238);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 17);
            this.label10.TabIndex = 26;
            this.label10.Text = "Award Name";
            // 
            // txtAwardType
            // 
            this.txtAwardType.BackColor = System.Drawing.Color.White;
            this.txtAwardType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAwardType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAwardType.Location = new System.Drawing.Point(120, 208);
            this.txtAwardType.MaxLength = 200;
            this.txtAwardType.Name = "txtAwardType";
            this.txtAwardType.Size = new System.Drawing.Size(370, 22);
            this.txtAwardType.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(21, 211);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 17);
            this.label7.TabIndex = 24;
            this.label7.Text = "Award Type";
            // 
            // txtEName
            // 
            this.txtEName.BackColor = System.Drawing.SystemColors.Info;
            this.txtEName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEName.Location = new System.Drawing.Point(189, 156);
            this.txtEName.MaxLength = 20;
            this.txtEName.Name = "txtEName";
            this.txtEName.ReadOnly = true;
            this.txtEName.Size = new System.Drawing.Size(301, 22);
            this.txtEName.TabIndex = 21;
            this.txtEName.TabStop = false;
            // 
            // txtEcodeSearch
            // 
            this.txtEcodeSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEcodeSearch.Location = new System.Drawing.Point(120, 156);
            this.txtEcodeSearch.MaxLength = 20;
            this.txtEcodeSearch.Name = "txtEcodeSearch";
            this.txtEcodeSearch.Size = new System.Drawing.Size(68, 22);
            this.txtEcodeSearch.TabIndex = 20;
            this.txtEcodeSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEcodeSearch_KeyUp);
            this.txtEcodeSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEcodeSearch_KeyPress);
            // 
            // lblEcode
            // 
            this.lblEcode.AutoSize = true;
            this.lblEcode.BackColor = System.Drawing.Color.PowderBlue;
            this.lblEcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEcode.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblEcode.Location = new System.Drawing.Point(62, 159);
            this.lblEcode.Name = "lblEcode";
            this.lblEcode.Size = new System.Drawing.Size(53, 16);
            this.lblEcode.TabIndex = 19;
            this.lblEcode.Text = "Ecode";
            // 
            // txtEmpDesg
            // 
            this.txtEmpDesg.BackColor = System.Drawing.SystemColors.Info;
            this.txtEmpDesg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpDesg.Location = new System.Drawing.Point(120, 182);
            this.txtEmpDesg.MaxLength = 50;
            this.txtEmpDesg.Name = "txtEmpDesg";
            this.txtEmpDesg.ReadOnly = true;
            this.txtEmpDesg.Size = new System.Drawing.Size(370, 22);
            this.txtEmpDesg.TabIndex = 23;
            this.txtEmpDesg.TabStop = false;
            // 
            // lblDesg
            // 
            this.lblDesg.AutoSize = true;
            this.lblDesg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesg.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblDesg.Location = new System.Drawing.Point(66, 185);
            this.lblDesg.Name = "lblDesg";
            this.lblDesg.Size = new System.Drawing.Size(49, 17);
            this.lblDesg.TabIndex = 22;
            this.lblDesg.Text = "Desig";
            // 
            // dtpAwardDate
            // 
            this.dtpAwardDate.CustomFormat = "dd/MM/yyyy";
            this.dtpAwardDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpAwardDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAwardDate.Location = new System.Drawing.Point(388, 14);
            this.dtpAwardDate.Name = "dtpAwardDate";
            this.dtpAwardDate.Size = new System.Drawing.Size(97, 22);
            this.dtpAwardDate.TabIndex = 3;
            this.dtpAwardDate.Value = new System.DateTime(2013, 2, 22, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(296, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Award Date";
            // 
            // cbEventName
            // 
            this.cbEventName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEventName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEventName.FormattingEnabled = true;
            this.cbEventName.Location = new System.Drawing.Point(120, 127);
            this.cbEventName.Name = "cbEventName";
            this.cbEventName.Size = new System.Drawing.Size(299, 24);
            this.cbEventName.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(20, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Event Name";
            // 
            // cbRegion
            // 
            this.cbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRegion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRegion.FormattingEnabled = true;
            this.cbRegion.Location = new System.Drawing.Point(309, 70);
            this.cbRegion.Name = "cbRegion";
            this.cbRegion.Size = new System.Drawing.Size(182, 24);
            this.cbRegion.TabIndex = 12;
            this.cbRegion.SelectedIndexChanged += new System.EventHandler(this.cbRegion_SelectedIndexChanged);
            // 
            // cbZones
            // 
            this.cbZones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbZones.FormattingEnabled = true;
            this.cbZones.Location = new System.Drawing.Point(120, 71);
            this.cbZones.Name = "cbZones";
            this.cbZones.Size = new System.Drawing.Size(125, 24);
            this.cbZones.TabIndex = 10;
            this.cbZones.SelectedIndexChanged += new System.EventHandler(this.cbZones_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Location = new System.Drawing.Point(257, 526);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 47);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.Navy;
            this.btnExit.Location = new System.Drawing.Point(266, 11);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(78, 30);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "C&lose";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Navy;
            this.btnSave.Location = new System.Drawing.Point(102, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.Navy;
            this.btnClear.Location = new System.Drawing.Point(184, 11);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(78, 30);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clea&r";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label9.Location = new System.Drawing.Point(250, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 17);
            this.label9.TabIndex = 11;
            this.label9.Text = "Region";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(68, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 17);
            this.label8.TabIndex = 9;
            this.label8.Text = "Zone";
            // 
            // cbLocation
            // 
            this.cbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLocation.FormattingEnabled = true;
            this.cbLocation.Location = new System.Drawing.Point(120, 99);
            this.cbLocation.Name = "cbLocation";
            this.cbLocation.Size = new System.Drawing.Size(370, 24);
            this.cbLocation.TabIndex = 14;
            // 
            // cbCompany
            // 
            this.cbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompany.FormattingEnabled = true;
            this.cbCompany.Items.AddRange(new object[] {
            "                     Select"});
            this.cbCompany.Location = new System.Drawing.Point(120, 43);
            this.cbCompany.Name = "cbCompany";
            this.cbCompany.Size = new System.Drawing.Size(370, 24);
            this.cbCompany.TabIndex = 8;
            this.cbCompany.SelectedIndexChanged += new System.EventHandler(this.cbCompany_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(56, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Branch";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(41, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Company";
            // 
            // cbFinYear
            // 
            this.cbFinYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFinYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFinYear.FormattingEnabled = true;
            this.cbFinYear.Location = new System.Drawing.Point(121, 12);
            this.cbFinYear.Name = "cbFinYear";
            this.cbFinYear.Size = new System.Drawing.Size(124, 24);
            this.cbFinYear.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(51, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Fin Year";
            // 
            // frmEmpAwardsEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(965, 582);
            this.ControlBox = false;
            this.Controls.Add(this.grp1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmpAwardsEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Emp Awards Entry";
            this.Load += new System.EventHandler(this.frmEmpAwardsEntry_Load);
            this.grp1.ResumeLayout(false);
            this.grp1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDocPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEmpAwardDetl)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn3;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn4;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn5;
        private System.Windows.Forms.GroupBox grp1;
        private System.Windows.Forms.ComboBox cbRegion;
        private System.Windows.Forms.ComboBox cbZones;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbLocation;
        private System.Windows.Forms.ComboBox cbCompany;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbFinYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbEventName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpAwardDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEName;
        private System.Windows.Forms.TextBox txtEcodeSearch;
        private System.Windows.Forms.Label lblEcode;
        public System.Windows.Forms.TextBox txtEmpDesg;
        private System.Windows.Forms.Label lblDesg;
        public System.Windows.Forms.TextBox txtAwardType;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtMementoType;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.TextBox txtCash;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox txtTripName;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox txtAwardName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.DataGridView gvEmpAwardDetl;
        private System.Windows.Forms.Button btnClearImage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.PictureBox pbDocPic;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.TextBox txtPerfPnts;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.TextBox txtPerfDetails;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.TextBox txtEventName;
        private System.Windows.Forms.CheckBox chkAddEvent;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Zone;
        private System.Windows.Forms.DataGridViewTextBoxColumn Region;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocMonth;
        private System.Windows.Forms.DataGridViewTextBoxColumn PerfDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn Points;
        private System.Windows.Forms.DataGridViewTextBoxColumn TripName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cash;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventId;
        private System.Windows.Forms.DataGridViewTextBoxColumn AwardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn BackDays;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn FinYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AwardName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AwardDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Performance;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorthOfGiftCheque;
        private System.Windows.Forms.DataGridViewTextBoxColumn Memento;
        private System.Windows.Forms.DataGridViewImageColumn Edit;

    }
}