using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
namespace SSCRM
{
    public partial class ImageBrowser : Form
    {
        PFUANMaster childPFUANMaster = null;
        PictureBox pic = null;
        string strFormType = "", ScreenType = "";
        frmAddDocumentDetails objfrmAddDocumentDetails = null;
        frmDemoPlotResults objfrmDemoPlotResults = null;
        Int32 nRowIndex = 0;
        public Physicalstkcount objPhyStkCnt = null;


        public ImageBrowser()
        {
            InitializeComponent();
        }
        public ImageBrowser(PFUANMaster pfUANMaster,PictureBox picBox,String strType)
        {
            childPFUANMaster = pfUANMaster;
            pic = picBox;
            strFormType= strType;
            InitializeComponent();
        }
        public ImageBrowser(string strScreenType, PictureBox picBox, String strType)
        {
            ScreenType = strScreenType;
            pic = picBox;
            strFormType = strType;
            InitializeComponent();
        }
        public ImageBrowser(Physicalstkcount objStkCnt, Int32 rowValue, string sFrmType)
        {
            InitializeComponent();
            nRowIndex = rowValue;
            objPhyStkCnt = objStkCnt;
            strFormType = sFrmType;
            
        }

        private Image Img;
        private Size OriginalImageSize;
        private Size ModifiedImageSize;

        int cropX;
        int cropY;
        int cropWidth;

        int cropHeight;
        int oCropX;
        int oCropY;
        public Pen cropPen;

        public DashStyle cropDashStyle = DashStyle.DashDot;
        public bool Makeselection = false;

        public bool CreateText = false;

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog Dlg = new OpenFileDialog();
            Dlg.Filter = "";
            Dlg.Title = "Select image";
            if (Dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Img = Image.FromFile(Dlg.FileName);
                //Image.FromFile(String) method creates an image from the specifed file, here dlg.Filename contains the name of the file from which to create the image
                LoadImage();
            }

        }
        private void LoadImage()
        {
            //we set the picturebox size according to image, we can get image width and height with the help of Image.Width and Image.height properties.
            int imgWidth = Img.Width;
            int imghieght = Img.Height;
            ImageHandler imageHandler = new ImageHandler();
            //FileInfo fileInfo = new FileInfo();
            var ms = new MemoryStream(1000);
            Img.Save(ms, ImageFormat.Jpeg);
            long jpegByteSize;
            jpegByteSize = ms.Length;
            imgSize.Text = "Image Size: " + (jpegByteSize / 1024.0).ToString("f") + "KB";
            
            //int imgSize = (Img.Length / 1024.0);
            pbPictureBox.Width = imgWidth;
            pbPictureBox.Height = imghieght;
            pbPictureBox.Image = Img;
            PictureBoxLocation();
            
            OriginalImageSize = new Size(imgWidth, imghieght);

            SetResizeInfo();
        }
        private void PictureBoxLocation()
        {
            int _x = 0;
            int _y = 0;
            if (SplitContainer1.Panel1.Width > pbPictureBox.Width)
            {
                _x = (SplitContainer1.Panel1.Width - pbPictureBox.Width) / 2;
            }
            if (SplitContainer1.Panel1.Height > pbPictureBox.Height)
            {
                _y = (SplitContainer1.Panel1.Height - pbPictureBox.Height) / 2;
            }
            pbPictureBox.Location = new Point(_x, _y);
        }

        private void SetResizeInfo()
        {

            lbloriginalSize.Text = OriginalImageSize.ToString();
            lblModifiedSize.Text = ModifiedImageSize.ToString();
            var ms = new MemoryStream(1000);
            pbPictureBox.Image.Save(ms, ImageFormat.Jpeg);
            long jpegByteSize;
            jpegByteSize = ms.Length;
            imgSize.Text = "Image Size: " + (jpegByteSize / 1024.0).ToString("f") + "KB";

        }

        private void SplitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            PictureBoxLocation();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (pbPictureBox.Image != null && ModifiedImageSize.Width > 0 && ModifiedImageSize.Height>0)
            {
                Bitmap bm_source = new Bitmap(pbPictureBox.Image);
                // Make a bitmap for the result.
                Bitmap bm_dest = new Bitmap(Convert.ToInt32(ModifiedImageSize.Width), Convert.ToInt32(ModifiedImageSize.Height));
                // Make a Graphics object for the result Bitmap.
                Graphics gr_dest = Graphics.FromImage(bm_dest);
                // Copy the source image into the destination bitmap.
                gr_dest.DrawImage(bm_source, 0, 0, bm_dest.Width + 1, bm_dest.Height + 1);
                // Display the result.
                pbPictureBox.Image = bm_dest;
                pbPictureBox.Width = bm_dest.Width;
                pbPictureBox.Height = bm_dest.Height;
                PictureBoxLocation();
                var ms = new MemoryStream(1000);
                pbPictureBox.Image.Save(ms, ImageFormat.Jpeg);
                long jpegByteSize;
                jpegByteSize = ms.Length;
                imgSize.Text = "Image Size: " + (jpegByteSize / 1024.0).ToString("f") + "KB";
            }
            else
            {
                MessageBox.Show("Please select an Image to resize", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DomainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            int percentage = 0;
            if (OriginalImageSize.Width > 0 && OriginalImageSize.Height > 0)
            {
                try
                {
                    percentage = Convert.ToInt32(DomainUpDown1.Text);
                    ModifiedImageSize = new Size((OriginalImageSize.Width * percentage) / 100, (OriginalImageSize.Height * percentage) / 100);
                    SetResizeInfo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid Percentage");
                    return;
                }
            }

        }
        private void BindDomainIUpDown()
        {
            DomainUpDown1.Items.Clear();
            for (int i = 999; i >= 1; i--)
            {
                DomainUpDown1.Items.Add(i);
            }
            DomainUpDown1.Text = "100";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindDomainIUpDown();
        }

        # region "-----------------------------Crop Image------------------------------------"



        private void btnMakeSelection_Click(object sender, EventArgs e)
        {
            Makeselection = true;
            btnCrop.Enabled = true;

        }

        private void btnCrop_Click(object sender, EventArgs e)
        {

            Cursor = Cursors.Default;

            try
            {
                if (cropWidth < 1)
                {
                    return;
                }
                Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
                //First we define a rectangle with the help of already calculated points
                Bitmap OriginalImage = new Bitmap(pbPictureBox.Image, pbPictureBox.Width, pbPictureBox.Height);
                //Original image
                Bitmap _img = new Bitmap(cropWidth, cropHeight);
                // for cropinf image
                Graphics g = Graphics.FromImage(_img);
                // create graphics
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //set image attributes
                g.DrawImage(OriginalImage, 0, 0, rect, GraphicsUnit.Pixel);

                pbPictureBox.Image = _img;
                pbPictureBox.Width = _img.Width;
                pbPictureBox.Height = _img.Height;
                lbloriginalSize.Text = OriginalImageSize.ToString();
                lblModifiedSize.Text = _img.Size.ToString();
                PictureBoxLocation();
                btnCrop.Enabled = false;
            }
            catch (Exception ex)
            {
            }
            var ms = new MemoryStream(1000);
            pbPictureBox.Image.Save(ms, ImageFormat.Jpeg);
            long jpegByteSize;
            jpegByteSize = ms.Length;
            imgSize.Text = "Image Size: " + (jpegByteSize / 1024.0).ToString("f") + "KB";
        }


        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (tbcEditingOptions.SelectedIndex == 4)
            {
                Point TextStartLocation = e.Location;
                if (CreateText)
                {
                    Cursor = Cursors.IBeam;
                }
            }
            else
            {
                Cursor = Cursors.Default;
                if (Makeselection)
                {

                    try
                    {
                        if (e.Button == System.Windows.Forms.MouseButtons.Left)
                        {
                            Cursor = Cursors.Cross;
                            cropX = e.X;
                            cropY = e.Y;

                            cropPen = new Pen(Color.Black, 1);
                            cropPen.DashStyle = DashStyle.DashDotDot;


                        }
                        pbPictureBox.Refresh();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }


        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (Makeselection)
            {
                Cursor = Cursors.Default;
            }

        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (tbcEditingOptions.SelectedIndex == 4)
            {
                Point TextStartLocation = e.Location;
                if (CreateText)
                {
                    Cursor = Cursors.IBeam;
                }
            }
            else
            {
                Cursor = Cursors.Default;
                if (Makeselection)
                {

                    try
                    {
                        if (pbPictureBox.Image == null)
                            return;


                        if (e.Button == System.Windows.Forms.MouseButtons.Left)
                        {
                            pbPictureBox.Refresh();
                            cropWidth = e.X - cropX;
                            cropHeight = e.Y - cropY;
                            pbPictureBox.CreateGraphics().DrawRectangle(cropPen, cropX, cropY, cropWidth, cropHeight);
                        }



                    }
                    catch (Exception ex)
                    {
                        //if (ex.Number == 5)
                        //    return;
                    }
                }
            }

        }
        # endregion

        private void TrackBarBrightness_Scroll(object sender, EventArgs e)
        {
            //DomainUpDownBrightness.Text = TrackBarBrightness.Value.ToString();


            //float value = TrackBarBrightness.Value * 0.01f;
            //float[][] colorMatrixElements = {
            //                new float[] { 1, 0, 0, 0, 0},
            //                new float[] { 0, 1, 0, 0, 0},
            //                new float[] { 0, 0, 1, 0, 0},
            //                new float[] { 0, 0, 0, 1, 0},
            //                new float[] { value, value, value, 0, 1}
            //            };
            //ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
            ImageAttributes imageAttributes = new ImageAttributes();


            //imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);



            Image _img = pbPictureBox.Image;
                //Img;
            //PictureBox1.Image
            Graphics _g = default(Graphics);
            Bitmap bm_dest = new Bitmap(Convert.ToInt32(_img.Width), Convert.ToInt32(_img.Height));
            _g = Graphics.FromImage(bm_dest);
            _g.DrawImage(_img, new Rectangle(0, 0, bm_dest.Width + 1, bm_dest.Height + 1), 0, 0, bm_dest.Width + 1, bm_dest.Height + 1, GraphicsUnit.Pixel, imageAttributes);
            pbPictureBox.Image = bm_dest;

            var ms = new MemoryStream(1000);
            pbPictureBox.Image.Save(ms, ImageFormat.Jpeg);
            long jpegByteSize;
            jpegByteSize = ms.Length;
            imgSize.Text = "Image Size: " + (jpegByteSize / 1024.0).ToString("f") + "KB";

        }

        private void btnRotateLeft_Click(object sender, EventArgs e)
        {
               pbPictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
               pbPictureBox.Refresh();
        }

        private void btnRotateRight_Click(object sender, EventArgs e)
        {
            pbPictureBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
        pbPictureBox.Refresh();
        }

        private void btnRotateHorizantal_Click(object sender, EventArgs e)
        {
            pbPictureBox.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
        pbPictureBox.Refresh();
        }

        private void btnRotatevertical_Click(object sender, EventArgs e)
        {
            pbPictureBox.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
        pbPictureBox.Refresh();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           ////  int imgWidth = Img.Width;
           ////int imghieght = Img.Height;
           // if (pbPictureBox.Image != null && strFormType=="DOC")
           // {
           //     var ms = new MemoryStream(1000);
           //     pbPictureBox.Image.Save(ms, ImageFormat.Jpeg);
           //     long jpegByteSize;
           //     jpegByteSize = ms.Length;
           //     imgSize.Text = "Image Size: " + (jpegByteSize / 1024.0).ToString("f") + "KB";
           //     try
           //     {
           //         if (Convert.ToInt32(jpegByteSize / 1024.0) < 300)
           //         {
           //             pic.Width = pbPictureBox.Width;
           //             pic.Height = pbPictureBox.Height;
           //             pic.Image = pbPictureBox.Image;
           //             //double fileByte = (((pbPictureBox.Image.Width) * (pbPictureBox.Image.Height)) / 1024);
           //             //childPFUANMaster.lblFileSize.Text = "Image Size: " + fileByte.ToString() + " KB";
           //             this.Close();
           //             this.Dispose();
           //         }
           //         else
           //         {
           //             MessageBox.Show("Image size Should be less than 300KB", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
           //         }
           //     }
           //     catch (Exception ex)
           //     {
           //         MessageBox.Show(ex.ToString());
           //     }
           // }
           // //else
           // //{
           // //    MessageBox.Show("Please select an Image For ID-PROOF Documents", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
           // //}
           //else if (pbPictureBox.Image != null && strFormType == "SIG")
           // {
           //     var ms = new MemoryStream(1000);
           //     pbPictureBox.Image.Save(ms, ImageFormat.Jpeg);
           //     long jpegByteSize;
           //     jpegByteSize = ms.Length;
           //     imgSize.Text = "Image Size: " + (jpegByteSize / 1024.0).ToString("f") + "KB";
           //     try
           //     {
           //         if (Convert.ToInt32(jpegByteSize / 1024.0) < 50)
           //         {
           //             pic.Width = pbPictureBox.Width;
           //             pic.Height = pbPictureBox.Height;
           //             pic.Image = pbPictureBox.Image;
           //             //double fileByte = (((pbPictureBox.Image.Width) * (pbPictureBox.Image.Height)) / 1024);
           //             //childPFUANMaster.lblFileSize.Text = "Image Size: " + fileByte.ToString() + " KB";
           //             this.Close();
           //             this.Dispose();
           //         }
           //         else
           //         {
           //             MessageBox.Show("Image size Should be less than 50KB", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
           //         }
           //     }
           //     catch (Exception ex)
           //     {
           //         MessageBox.Show(ex.ToString());
           //     }
           // }
           // else
           // {
           //     MessageBox.Show("Please select an Image ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
           // }
            
        }

        private void addImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  int imgWidth = Img.Width;
            //int imghieght = Img.Height;
            if (pbPictureBox.Image != null && strFormType == "PHY_STK_CNT")
            {
                // objPhyStkCnt = new Physicalstkcount();

                Image img = pbPictureBox.Image;
                byte[] arr;
                ImageConverter converter = new ImageConverter();
                arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                //Int32 RowCnt = objPhyStkCnt.gvProductDetails.Rows.Count;

                objPhyStkCnt.gvProductDetails.Rows[nRowIndex].Cells["LetterImage"].Value = arr;
                this.Close();
                this.Dispose();

            }

            else if (pbPictureBox.Image != null && strFormType == "DOCUMENT")
            {
                var ms = new MemoryStream(1000);
                pbPictureBox.Image.Save(ms, ImageFormat.Jpeg);
                long jpegByteSize;
                jpegByteSize = ms.Length;
                imgSize.Text = "Image Size: " + (jpegByteSize / 1024.0).ToString("f") + "KB";
                try
                {
                    //if (Convert.ToInt32(jpegByteSize / 1024.0) < 300)
                    //{
                    pic.Width = pbPictureBox.Width;
                    pic.Height = pbPictureBox.Height;
                    pic.Image = pbPictureBox.Image;
                    //double fileByte = (((pbPictureBox.Image.Width) * (pbPictureBox.Image.Height)) / 1024);
                    //childPFUANMaster.lblFileSize.Text = "Image Size: " + fileByte.ToString() + " KB";
                    this.Close();
                    this.Dispose();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Image size Should be less than 300KB", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }


            else if (pbPictureBox.Image != null && strFormType == "DOC")
            {
                var ms = new MemoryStream(1000);
                pbPictureBox.Image.Save(ms, ImageFormat.Jpeg);
                long jpegByteSize;
                jpegByteSize = ms.Length;
                imgSize.Text = "Image Size: " + (jpegByteSize / 1024.0).ToString("f") + "KB";
                try
                {
                    if (Convert.ToInt32(jpegByteSize / 1024.0) < 300)
                    {
                        pic.Width = pbPictureBox.Width;
                        pic.Height = pbPictureBox.Height;
                        pic.Image = pbPictureBox.Image;
                        //double fileByte = (((pbPictureBox.Image.Width) * (pbPictureBox.Image.Height)) / 1024);
                        //childPFUANMaster.lblFileSize.Text = "Image Size: " + fileByte.ToString() + " KB";
                        this.Close();
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Image size Should be less than 300KB", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            //else
            //{
            //    MessageBox.Show("Please select an Image For ID-PROOF Documents", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            else if (pbPictureBox.Image != null && strFormType == "SIG")
            {
                var ms = new MemoryStream(1000);
                pbPictureBox.Image.Save(ms, ImageFormat.Jpeg);
                long jpegByteSize;
                jpegByteSize = ms.Length;
                imgSize.Text = "Image Size: " + (jpegByteSize / 1024.0).ToString("f") + "KB";
                try
                {
                    if (Convert.ToInt32(jpegByteSize / 1024.0) < 50)
                    {
                        pic.Width = pbPictureBox.Width;
                        pic.Height = pbPictureBox.Height;
                        pic.Image = pbPictureBox.Image;
                        //double fileByte = (((pbPictureBox.Image.Width) * (pbPictureBox.Image.Height)) / 1024);
                        //childPFUANMaster.lblFileSize.Text = "Image Size: " + fileByte.ToString() + " KB";
                        this.Close();
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Image size Should be less than 50KB", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Please select an Image ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


        
    }
}
