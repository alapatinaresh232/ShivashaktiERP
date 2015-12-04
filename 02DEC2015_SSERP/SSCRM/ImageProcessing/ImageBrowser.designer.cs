namespace SSCRM
{
    partial class ImageBrowser
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
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pbPictureBox = new System.Windows.Forms.PictureBox();
            this.tbcEditingOptions = new System.Windows.Forms.TabControl();
            this.tbpResize = new System.Windows.Forms.TabPage();
            this.imgSize = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.Label7 = new System.Windows.Forms.Label();
            this.lbloriginalSize = new System.Windows.Forms.Label();
            this.lblModifiedSize = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.DomainUpDown1 = new System.Windows.Forms.DomainUpDown();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.tbpCrop = new System.Windows.Forms.TabPage();
            this.btnCrop = new System.Windows.Forms.Button();
            this.btnMakeSelection = new System.Windows.Forms.Button();
            this.tbpRotate = new System.Windows.Forms.TabPage();
            this.btnRotateRight = new System.Windows.Forms.Button();
            this.btnRotateHorizantal = new System.Windows.Forms.Button();
            this.btnRotatevertical = new System.Windows.Forms.Button();
            this.btnRotateLeft = new System.Windows.Forms.Button();
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msbMenu_File_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UndoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPictureBox)).BeginInit();
            this.tbcEditingOptions.SuspendLayout();
            this.tbpResize.SuspendLayout();
            this.tbpCrop.SuspendLayout();
            this.tbpRotate.SuspendLayout();
            this.msMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer1.Location = new System.Drawing.Point(0, 24);
            this.SplitContainer1.Name = "SplitContainer1";
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.AutoScroll = true;
            this.SplitContainer1.Panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.SplitContainer1.Panel1.Controls.Add(this.pbPictureBox);
            this.SplitContainer1.Panel1.Resize += new System.EventHandler(this.SplitContainer1_Panel1_Resize);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.tbcEditingOptions);
            this.SplitContainer1.Size = new System.Drawing.Size(858, 591);
            this.SplitContainer1.SplitterDistance = 575;
            this.SplitContainer1.TabIndex = 6;
            // 
            // pbPictureBox
            // 
            this.pbPictureBox.Location = new System.Drawing.Point(3, 22);
            this.pbPictureBox.Name = "pbPictureBox";
            this.pbPictureBox.Size = new System.Drawing.Size(585, 452);
            this.pbPictureBox.TabIndex = 0;
            this.pbPictureBox.TabStop = false;
            this.pbPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseMove);
            this.pbPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseDown);
            this.pbPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseUp);
            // 
            // tbcEditingOptions
            // 
            this.tbcEditingOptions.Controls.Add(this.tbpResize);
            this.tbcEditingOptions.Controls.Add(this.tbpCrop);
            this.tbcEditingOptions.Controls.Add(this.tbpRotate);
            this.tbcEditingOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcEditingOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcEditingOptions.Location = new System.Drawing.Point(0, 0);
            this.tbcEditingOptions.Name = "tbcEditingOptions";
            this.tbcEditingOptions.SelectedIndex = 0;
            this.tbcEditingOptions.Size = new System.Drawing.Size(279, 591);
            this.tbcEditingOptions.TabIndex = 0;
            // 
            // tbpResize
            // 
            this.tbpResize.Controls.Add(this.imgSize);
            this.tbpResize.Controls.Add(this.btnAdd);
            this.tbpResize.Controls.Add(this.Label7);
            this.tbpResize.Controls.Add(this.lbloriginalSize);
            this.tbpResize.Controls.Add(this.lblModifiedSize);
            this.tbpResize.Controls.Add(this.Label4);
            this.tbpResize.Controls.Add(this.btnOk);
            this.tbpResize.Controls.Add(this.Label3);
            this.tbpResize.Controls.Add(this.DomainUpDown1);
            this.tbpResize.Controls.Add(this.Label2);
            this.tbpResize.Controls.Add(this.Label1);
            this.tbpResize.Location = new System.Drawing.Point(4, 24);
            this.tbpResize.Name = "tbpResize";
            this.tbpResize.Padding = new System.Windows.Forms.Padding(3);
            this.tbpResize.Size = new System.Drawing.Size(271, 563);
            this.tbpResize.TabIndex = 0;
            this.tbpResize.Text = "Resize";
            this.tbpResize.UseVisualStyleBackColor = true;
            // 
            // imgSize
            // 
            this.imgSize.AutoSize = true;
            this.imgSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imgSize.Location = new System.Drawing.Point(27, 263);
            this.imgSize.Name = "imgSize";
            this.imgSize.Size = new System.Drawing.Size(78, 16);
            this.imgSize.TabIndex = 10;
            this.imgSize.Text = "Image Size:";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(81, 292);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 24);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "Add Image";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(27, 217);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(84, 16);
            this.Label7.TabIndex = 8;
            this.Label7.Text = "Original size:";
            // 
            // lbloriginalSize
            // 
            this.lbloriginalSize.AutoSize = true;
            this.lbloriginalSize.Location = new System.Drawing.Point(110, 217);
            this.lbloriginalSize.Name = "lbloriginalSize";
            this.lbloriginalSize.Size = new System.Drawing.Size(0, 15);
            this.lbloriginalSize.TabIndex = 7;
            // 
            // lblModifiedSize
            // 
            this.lblModifiedSize.AutoSize = true;
            this.lblModifiedSize.Location = new System.Drawing.Point(110, 240);
            this.lblModifiedSize.Name = "lblModifiedSize";
            this.lblModifiedSize.Size = new System.Drawing.Size(0, 15);
            this.lblModifiedSize.TabIndex = 6;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(27, 240);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(90, 16);
            this.Label4.TabIndex = 5;
            this.Label4.Text = "Modified size:";
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(113, 61);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(51, 24);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(92, 68);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(18, 15);
            this.Label3.TabIndex = 3;
            this.Label3.Text = "%";
            // 
            // DomainUpDown1
            // 
            this.DomainUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DomainUpDown1.Location = new System.Drawing.Point(11, 63);
            this.DomainUpDown1.Name = "DomainUpDown1";
            this.DomainUpDown1.Size = new System.Drawing.Size(75, 22);
            this.DomainUpDown1.TabIndex = 2;
            this.DomainUpDown1.SelectedItemChanged += new System.EventHandler(this.DomainUpDown1_SelectedItemChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(8, 188);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(124, 15);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Size setting summery";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(8, 28);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(220, 15);
            this.Label1.TabIndex = 0;
            this.Label1.Tag = "";
            this.Label1.Text = "Percantage of original width and height";
            // 
            // tbpCrop
            // 
            this.tbpCrop.Controls.Add(this.btnCrop);
            this.tbpCrop.Controls.Add(this.btnMakeSelection);
            this.tbpCrop.Location = new System.Drawing.Point(4, 24);
            this.tbpCrop.Name = "tbpCrop";
            this.tbpCrop.Size = new System.Drawing.Size(271, 563);
            this.tbpCrop.TabIndex = 2;
            this.tbpCrop.Text = "Crop";
            this.tbpCrop.UseVisualStyleBackColor = true;
            // 
            // btnCrop
            // 
            this.btnCrop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrop.Location = new System.Drawing.Point(93, 102);
            this.btnCrop.Name = "btnCrop";
            this.btnCrop.Size = new System.Drawing.Size(75, 26);
            this.btnCrop.TabIndex = 1;
            this.btnCrop.Text = "Crop";
            this.btnCrop.UseVisualStyleBackColor = true;
            this.btnCrop.Click += new System.EventHandler(this.btnCrop_Click);
            // 
            // btnMakeSelection
            // 
            this.btnMakeSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMakeSelection.Location = new System.Drawing.Point(70, 68);
            this.btnMakeSelection.Name = "btnMakeSelection";
            this.btnMakeSelection.Size = new System.Drawing.Size(115, 27);
            this.btnMakeSelection.TabIndex = 0;
            this.btnMakeSelection.Text = "Make Selection";
            this.btnMakeSelection.UseVisualStyleBackColor = true;
            this.btnMakeSelection.Click += new System.EventHandler(this.btnMakeSelection_Click);
            // 
            // tbpRotate
            // 
            this.tbpRotate.Controls.Add(this.btnRotateRight);
            this.tbpRotate.Controls.Add(this.btnRotateHorizantal);
            this.tbpRotate.Controls.Add(this.btnRotatevertical);
            this.tbpRotate.Controls.Add(this.btnRotateLeft);
            this.tbpRotate.Location = new System.Drawing.Point(4, 24);
            this.tbpRotate.Name = "tbpRotate";
            this.tbpRotate.Size = new System.Drawing.Size(271, 563);
            this.tbpRotate.TabIndex = 3;
            this.tbpRotate.Text = "Rotate";
            this.tbpRotate.UseVisualStyleBackColor = true;
            // 
            // btnRotateRight
            // 
            this.btnRotateRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRotateRight.Location = new System.Drawing.Point(75, 120);
            this.btnRotateRight.Name = "btnRotateRight";
            this.btnRotateRight.Size = new System.Drawing.Size(122, 25);
            this.btnRotateRight.TabIndex = 3;
            this.btnRotateRight.Text = "Rotate right";
            this.btnRotateRight.UseVisualStyleBackColor = true;
            this.btnRotateRight.Click += new System.EventHandler(this.btnRotateRight_Click);
            // 
            // btnRotateHorizantal
            // 
            this.btnRotateHorizantal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRotateHorizantal.Location = new System.Drawing.Point(75, 182);
            this.btnRotateHorizantal.Name = "btnRotateHorizantal";
            this.btnRotateHorizantal.Size = new System.Drawing.Size(122, 25);
            this.btnRotateHorizantal.TabIndex = 2;
            this.btnRotateHorizantal.Text = "Rotate horizantal";
            this.btnRotateHorizantal.UseVisualStyleBackColor = true;
            this.btnRotateHorizantal.Click += new System.EventHandler(this.btnRotateHorizantal_Click);
            // 
            // btnRotatevertical
            // 
            this.btnRotatevertical.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRotatevertical.Location = new System.Drawing.Point(75, 211);
            this.btnRotatevertical.Name = "btnRotatevertical";
            this.btnRotatevertical.Size = new System.Drawing.Size(122, 25);
            this.btnRotatevertical.TabIndex = 1;
            this.btnRotatevertical.Text = "Rotate vertical";
            this.btnRotatevertical.UseVisualStyleBackColor = true;
            this.btnRotatevertical.Click += new System.EventHandler(this.btnRotatevertical_Click);
            // 
            // btnRotateLeft
            // 
            this.btnRotateLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRotateLeft.Location = new System.Drawing.Point(75, 91);
            this.btnRotateLeft.Name = "btnRotateLeft";
            this.btnRotateLeft.Size = new System.Drawing.Size(122, 25);
            this.btnRotateLeft.TabIndex = 0;
            this.btnRotateLeft.Text = "Rotate left";
            this.btnRotateLeft.UseVisualStyleBackColor = true;
            this.btnRotateLeft.Click += new System.EventHandler(this.btnRotateLeft_Click);
            // 
            // msMenu
            // 
            this.msMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.msMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.addImageToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.msMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(858, 24);
            this.msMenu.TabIndex = 5;
            this.msMenu.Text = "MenuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msbMenu_File_Open,
            this.SaveToolStripMenuItem,
            this.ToolStripSeparator1,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // msbMenu_File_Open
            // 
            this.msbMenu_File_Open.Name = "msbMenu_File_Open";
            this.msbMenu_File_Open.Size = new System.Drawing.Size(109, 22);
            this.msbMenu_File_Open.Text = "Open";
            this.msbMenu_File_Open.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.SaveToolStripMenuItem.Text = "Save";
            this.SaveToolStripMenuItem.Visible = false;
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(106, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Visible = false;
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UndoToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.EditToolStripMenuItem.Text = "Edit";
            this.EditToolStripMenuItem.Visible = false;
            // 
            // UndoToolStripMenuItem
            // 
            this.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem";
            this.UndoToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.UndoToolStripMenuItem.Text = "Undo";
            // 
            // addImageToolStripMenuItem
            // 
            this.addImageToolStripMenuItem.Name = "addImageToolStripMenuItem";
            this.addImageToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.addImageToolStripMenuItem.Text = "Add Image";
            this.addImageToolStripMenuItem.Click += new System.EventHandler(this.addImageToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // ImageBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 615);
            this.ControlBox = false;
            this.Controls.Add(this.SplitContainer1);
            this.Controls.Add(this.msMenu);
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "ImageBrowser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImageEditor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            this.SplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPictureBox)).EndInit();
            this.tbcEditingOptions.ResumeLayout(false);
            this.tbpResize.ResumeLayout(false);
            this.tbpResize.PerformLayout();
            this.tbpCrop.ResumeLayout(false);
            this.tbpRotate.ResumeLayout(false);
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.SplitContainer SplitContainer1;
        internal System.Windows.Forms.PictureBox pbPictureBox;
        internal System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem msbMenu_File_Open;
        internal System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem UndoToolStripMenuItem;
        private System.Windows.Forms.MenuStrip msMenu;
        internal System.Windows.Forms.TabControl tbcEditingOptions;
        internal System.Windows.Forms.TabPage tbpResize;
        internal System.Windows.Forms.Label imgSize;
        internal System.Windows.Forms.Button btnAdd;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label lbloriginalSize;
        internal System.Windows.Forms.Label lblModifiedSize;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Button btnOk;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.DomainUpDown DomainUpDown1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TabPage tbpCrop;
        internal System.Windows.Forms.Button btnCrop;
        internal System.Windows.Forms.Button btnMakeSelection;
        internal System.Windows.Forms.TabPage tbpRotate;
        internal System.Windows.Forms.Button btnRotateRight;
        internal System.Windows.Forms.Button btnRotateHorizantal;
        internal System.Windows.Forms.Button btnRotatevertical;
        internal System.Windows.Forms.Button btnRotateLeft;
        private System.Windows.Forms.ToolStripMenuItem addImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    }
}

