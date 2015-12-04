namespace SSCRM
{
    partial class BranchAboveLevelMapping
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tvComp = new System.Windows.Forms.TreeView();
            this.tbLinkDelink = new System.Windows.Forms.TabControl();
            this.tbpLink = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddlinkDest = new System.Windows.Forms.Button();
            this.btnAddlinkSrc = new System.Windows.Forms.Button();
            this.btnRemlinkSrc = new System.Windows.Forms.Button();
            this.btnRemlinkDest = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtclblinkSourceSearch = new System.Windows.Forms.TextBox();
            this.clblinkDest = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clblinkSource = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.clblinkEmplist = new System.Windows.Forms.CheckedListBox();
            this.txtclblinkEmplistSearch = new System.Windows.Forms.TextBox();
            this.tbpDelink = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtclbdlinkSourceSearch = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.clbdlinkEmplist = new System.Windows.Forms.CheckedListBox();
            this.txtclbdlinkEmplistSearch = new System.Windows.Forms.TextBox();
            this.clbdlinkSource = new System.Windows.Forms.CheckedListBox();
            this.cbDocmonth = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.checkedListBox3 = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.tbLinkDelink.SuspendLayout();
            this.tbpLink.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tbpDelink.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox3.Controls.Add(this.tvComp);
            this.groupBox3.Controls.Add(this.tbLinkDelink);
            this.groupBox3.Controls.Add(this.cbDocmonth);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox3.Location = new System.Drawing.Point(5, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(639, 501);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Branch Above Level Mapping";
            // 
            // tvComp
            // 
            this.tvComp.CheckBoxes = true;
            this.tvComp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvComp.Location = new System.Drawing.Point(232, 12);
            this.tvComp.Name = "tvComp";
            this.tvComp.Size = new System.Drawing.Size(396, 98);
            this.tvComp.TabIndex = 72;
            this.tvComp.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvComp_AfterCheck);
            // 
            // tbLinkDelink
            // 
            this.tbLinkDelink.Controls.Add(this.tbpLink);
            this.tbLinkDelink.Controls.Add(this.tbpDelink);
            this.tbLinkDelink.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLinkDelink.Location = new System.Drawing.Point(6, 91);
            this.tbLinkDelink.Name = "tbLinkDelink";
            this.tbLinkDelink.SelectedIndex = 0;
            this.tbLinkDelink.Size = new System.Drawing.Size(626, 362);
            this.tbLinkDelink.TabIndex = 70;
            this.tbLinkDelink.Tag = "";
            this.tbLinkDelink.TabIndexChanged += new System.EventHandler(this.tbLinkDelink_TabIndexChanged);
            this.tbLinkDelink.SelectedIndexChanged += new System.EventHandler(this.tbLinkDelink_SelectedIndexChanged);
            // 
            // tbpLink
            // 
            this.tbpLink.BackColor = System.Drawing.Color.PowderBlue;
            this.tbpLink.Controls.Add(this.groupBox1);
            this.tbpLink.Location = new System.Drawing.Point(4, 25);
            this.tbpLink.Name = "tbpLink";
            this.tbpLink.Padding = new System.Windows.Forms.Padding(3);
            this.tbpLink.Size = new System.Drawing.Size(618, 333);
            this.tbpLink.TabIndex = 0;
            this.tbpLink.Text = "Link";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox1.Controls.Add(this.btnAddlinkDest);
            this.groupBox1.Controls.Add(this.btnAddlinkSrc);
            this.groupBox1.Controls.Add(this.btnRemlinkSrc);
            this.groupBox1.Controls.Add(this.btnRemlinkDest);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtclblinkSourceSearch);
            this.groupBox1.Controls.Add(this.clblinkDest);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.clblinkSource);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.clblinkEmplist);
            this.groupBox1.Controls.Add(this.txtclblinkEmplistSearch);
            this.groupBox1.Location = new System.Drawing.Point(2, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(616, 334);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // btnAddlinkDest
            // 
            this.btnAddlinkDest.BackColor = System.Drawing.Color.AliceBlue;
            this.btnAddlinkDest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddlinkDest.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnAddlinkDest.Image = global::SSCRM.Properties.Resources.arrows_back_forward;
            this.btnAddlinkDest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddlinkDest.Location = new System.Drawing.Point(321, 36);
            this.btnAddlinkDest.Name = "btnAddlinkDest";
            this.btnAddlinkDest.Size = new System.Drawing.Size(33, 30);
            this.btnAddlinkDest.TabIndex = 93;
            this.btnAddlinkDest.UseVisualStyleBackColor = false;
            this.btnAddlinkDest.Click += new System.EventHandler(this.btnRemlinkDest_Click);
            // 
            // btnAddlinkSrc
            // 
            this.btnAddlinkSrc.BackColor = System.Drawing.Color.AliceBlue;
            this.btnAddlinkSrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddlinkSrc.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnAddlinkSrc.Image = global::SSCRM.Properties.Resources.arrows_back_forward;
            this.btnAddlinkSrc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddlinkSrc.Location = new System.Drawing.Point(320, 147);
            this.btnAddlinkSrc.Name = "btnAddlinkSrc";
            this.btnAddlinkSrc.Size = new System.Drawing.Size(33, 30);
            this.btnAddlinkSrc.TabIndex = 90;
            this.btnAddlinkSrc.UseVisualStyleBackColor = false;
            this.btnAddlinkSrc.Click += new System.EventHandler(this.btnRemlinkSrc_Click);
            // 
            // btnRemlinkSrc
            // 
            this.btnRemlinkSrc.BackColor = System.Drawing.Color.AliceBlue;
            this.btnRemlinkSrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemlinkSrc.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnRemlinkSrc.Image = global::SSCRM.Properties.Resources.arrows_back_forward;
            this.btnRemlinkSrc.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemlinkSrc.Location = new System.Drawing.Point(321, 183);
            this.btnRemlinkSrc.Name = "btnRemlinkSrc";
            this.btnRemlinkSrc.Size = new System.Drawing.Size(33, 30);
            this.btnRemlinkSrc.TabIndex = 91;
            this.btnRemlinkSrc.UseVisualStyleBackColor = false;
            this.btnRemlinkSrc.Click += new System.EventHandler(this.btnAddlinkSrc_Click);
            // 
            // btnRemlinkDest
            // 
            this.btnRemlinkDest.BackColor = System.Drawing.Color.AliceBlue;
            this.btnRemlinkDest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemlinkDest.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnRemlinkDest.Image = global::SSCRM.Properties.Resources.arrows_back_forward;
            this.btnRemlinkDest.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemlinkDest.Location = new System.Drawing.Point(320, 74);
            this.btnRemlinkDest.Name = "btnRemlinkDest";
            this.btnRemlinkDest.Size = new System.Drawing.Size(33, 30);
            this.btnRemlinkDest.TabIndex = 92;
            this.btnRemlinkDest.UseVisualStyleBackColor = false;
            this.btnRemlinkDest.Click += new System.EventHandler(this.btnAddlinkDest_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(362, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 15);
            this.label2.TabIndex = 89;
            this.label2.Text = "Reporting To";
            // 
            // txtclblinkSourceSearch
            // 
            this.txtclblinkSourceSearch.Location = new System.Drawing.Point(479, 124);
            this.txtclblinkSourceSearch.Name = "txtclblinkSourceSearch";
            this.txtclblinkSourceSearch.Size = new System.Drawing.Size(130, 22);
            this.txtclblinkSourceSearch.TabIndex = 87;
            // 
            // clblinkDest
            // 
            this.clblinkDest.CheckOnClick = true;
            this.clblinkDest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clblinkDest.FormattingEnabled = true;
            this.clblinkDest.Location = new System.Drawing.Point(361, 35);
            this.clblinkDest.Name = "clblinkDest";
            this.clblinkDest.Size = new System.Drawing.Size(248, 68);
            this.clblinkDest.TabIndex = 81;
            this.clblinkDest.UseCompatibleTextRendering = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(362, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 88;
            this.label1.Text = "Source";
            // 
            // clblinkSource
            // 
            this.clblinkSource.CheckOnClick = true;
            this.clblinkSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clblinkSource.FormattingEnabled = true;
            this.clblinkSource.Location = new System.Drawing.Point(361, 147);
            this.clblinkSource.Name = "clblinkSource";
            this.clblinkSource.Size = new System.Drawing.Size(248, 180);
            this.clblinkSource.TabIndex = 82;
            this.clblinkSource.UseCompatibleTextRendering = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(121, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 74;
            this.label4.Text = "Search";
            // 
            // clblinkEmplist
            // 
            this.clblinkEmplist.CheckOnClick = true;
            this.clblinkEmplist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clblinkEmplist.FormattingEnabled = true;
            this.clblinkEmplist.Location = new System.Drawing.Point(6, 36);
            this.clblinkEmplist.Name = "clblinkEmplist";
            this.clblinkEmplist.Size = new System.Drawing.Size(309, 292);
            this.clblinkEmplist.TabIndex = 72;
            this.clblinkEmplist.UseCompatibleTextRendering = true;
            // 
            // txtclblinkEmplistSearch
            // 
            this.txtclblinkEmplistSearch.Location = new System.Drawing.Point(174, 13);
            this.txtclblinkEmplistSearch.Name = "txtclblinkEmplistSearch";
            this.txtclblinkEmplistSearch.Size = new System.Drawing.Size(141, 22);
            this.txtclblinkEmplistSearch.TabIndex = 73;
            this.txtclblinkEmplistSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtclblinkEmplistSearch_KeyUp);
            // 
            // tbpDelink
            // 
            this.tbpDelink.BackColor = System.Drawing.Color.PowderBlue;
            this.tbpDelink.Controls.Add(this.groupBox4);
            this.tbpDelink.Location = new System.Drawing.Point(4, 25);
            this.tbpDelink.Name = "tbpDelink";
            this.tbpDelink.Padding = new System.Windows.Forms.Padding(3);
            this.tbpDelink.Size = new System.Drawing.Size(618, 333);
            this.tbpDelink.TabIndex = 1;
            this.tbpDelink.Text = "Delink";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.txtclbdlinkSourceSearch);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.clbdlinkEmplist);
            this.groupBox4.Controls.Add(this.txtclbdlinkEmplistSearch);
            this.groupBox4.Controls.Add(this.clbdlinkSource);
            this.groupBox4.Location = new System.Drawing.Point(2, -5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(616, 334);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(131, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 15);
            this.label9.TabIndex = 74;
            this.label9.Text = "Search";
            // 
            // txtclbdlinkSourceSearch
            // 
            this.txtclbdlinkSourceSearch.Location = new System.Drawing.Point(472, 13);
            this.txtclbdlinkSourceSearch.Name = "txtclbdlinkSourceSearch";
            this.txtclbdlinkSourceSearch.Size = new System.Drawing.Size(139, 22);
            this.txtclbdlinkSourceSearch.TabIndex = 78;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(414, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 15);
            this.label10.TabIndex = 79;
            this.label10.Text = "Source";
            // 
            // clbdlinkEmplist
            // 
            this.clbdlinkEmplist.CheckOnClick = true;
            this.clbdlinkEmplist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbdlinkEmplist.FormattingEnabled = true;
            this.clbdlinkEmplist.Location = new System.Drawing.Point(6, 37);
            this.clbdlinkEmplist.Name = "clbdlinkEmplist";
            this.clbdlinkEmplist.Size = new System.Drawing.Size(320, 292);
            this.clbdlinkEmplist.TabIndex = 72;
            this.clbdlinkEmplist.UseCompatibleTextRendering = true;
            this.clbdlinkEmplist.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbdlinkEmplist_ItemCheck);
            // 
            // txtclbdlinkEmplistSearch
            // 
            this.txtclbdlinkEmplistSearch.Location = new System.Drawing.Point(184, 12);
            this.txtclbdlinkEmplistSearch.Name = "txtclbdlinkEmplistSearch";
            this.txtclbdlinkEmplistSearch.Size = new System.Drawing.Size(141, 22);
            this.txtclbdlinkEmplistSearch.TabIndex = 73;
            // 
            // clbdlinkSource
            // 
            this.clbdlinkSource.CheckOnClick = true;
            this.clbdlinkSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbdlinkSource.FormattingEnabled = true;
            this.clbdlinkSource.Location = new System.Drawing.Point(332, 37);
            this.clbdlinkSource.Name = "clbdlinkSource";
            this.clbdlinkSource.Size = new System.Drawing.Size(279, 292);
            this.clbdlinkSource.TabIndex = 73;
            this.clbdlinkSource.UseCompatibleTextRendering = true;
            // 
            // cbDocmonth
            // 
            this.cbDocmonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDocmonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDocmonth.FormattingEnabled = true;
            this.cbDocmonth.Location = new System.Drawing.Point(99, 43);
            this.cbDocmonth.Name = "cbDocmonth";
            this.cbDocmonth.Size = new System.Drawing.Size(105, 23);
            this.cbDocmonth.TabIndex = 12;
            this.cbDocmonth.SelectedIndexChanged += new System.EventHandler(this.cbDocmonth_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(21, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 15);
            this.label6.TabIndex = 23;
            this.label6.Text = "Doc Month";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Location = new System.Drawing.Point(4, 449);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(630, 45);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDelete.Location = new System.Drawing.Point(305, 11);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnCancel.Location = new System.Drawing.Point(228, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(403, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Location = new System.Drawing.Point(152, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(8, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 15);
            this.label3.TabIndex = 80;
            this.label3.Text = "Reporting To";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(418, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 74;
            this.label5.Text = "Search";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(125, 123);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(130, 20);
            this.textBox1.TabIndex = 78;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(7, 34);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(248, 68);
            this.checkedListBox1.TabIndex = 71;
            this.checkedListBox1.UseCompatibleTextRendering = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(8, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 79;
            this.label7.Text = "Source";
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.CheckOnClick = true;
            this.checkedListBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox2.FormattingEnabled = true;
            this.checkedListBox2.Location = new System.Drawing.Point(303, 36);
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(309, 292);
            this.checkedListBox2.TabIndex = 72;
            this.checkedListBox2.UseCompatibleTextRendering = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(471, 13);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(141, 20);
            this.textBox2.TabIndex = 73;
            // 
            // checkedListBox3
            // 
            this.checkedListBox3.CheckOnClick = true;
            this.checkedListBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox3.FormattingEnabled = true;
            this.checkedListBox3.Location = new System.Drawing.Point(7, 146);
            this.checkedListBox3.Name = "checkedListBox3";
            this.checkedListBox3.Size = new System.Drawing.Size(248, 180);
            this.checkedListBox3.TabIndex = 73;
            this.checkedListBox3.UseCompatibleTextRendering = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.AliceBlue;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.button1.Image = global::SSCRM.Properties.Resources.arrows_back_forward;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(261, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 30);
            this.button1.TabIndex = 77;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.AliceBlue;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.button2.Image = global::SSCRM.Properties.Resources.arrows_back_forward;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(260, 166);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(33, 30);
            this.button2.TabIndex = 74;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.AliceBlue;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.MidnightBlue;
            this.button3.Image = global::SSCRM.Properties.Resources.arrows_back_forward;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.Location = new System.Drawing.Point(261, 202);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(33, 30);
            this.button3.TabIndex = 75;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.AliceBlue;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.MidnightBlue;
            this.button4.Image = global::SSCRM.Properties.Resources.arrows_back_forward;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.Location = new System.Drawing.Point(260, 72);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(33, 30);
            this.button4.TabIndex = 76;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // BranchAboveLevelMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(646, 508);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BranchAboveLevelMapping";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Above Branch level Mapping";
            this.Load += new System.EventHandler(this.BranchAboveLevelMapping_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tbLinkDelink.ResumeLayout(false);
            this.tbpLink.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tbpDelink.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbDocmonth;
        private System.Windows.Forms.TabControl tbLinkDelink;
        private System.Windows.Forms.TabPage tbpLink;
        private System.Windows.Forms.TabPage tbpDelink;
        private System.Windows.Forms.CheckedListBox clblinkEmplist;
        private System.Windows.Forms.TreeView tvComp;
        private System.Windows.Forms.TextBox txtclblinkEmplistSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckedListBox checkedListBox2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckedListBox checkedListBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtclbdlinkSourceSearch;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckedListBox clbdlinkEmplist;
        private System.Windows.Forms.TextBox txtclbdlinkEmplistSearch;
        private System.Windows.Forms.CheckedListBox clbdlinkSource;
        private System.Windows.Forms.Button btnAddlinkDest;
        private System.Windows.Forms.Button btnAddlinkSrc;
        private System.Windows.Forms.Button btnRemlinkSrc;
        private System.Windows.Forms.Button btnRemlinkDest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtclblinkSourceSearch;
        private System.Windows.Forms.CheckedListBox clblinkDest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox clblinkSource;
    }
}