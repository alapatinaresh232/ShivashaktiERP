using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;


namespace SSCRM
{
    public partial class frmDisplayImage : Form
    {
        public frmSpecialApprovals objfrmSpecialApprovals;       
        public FarmerMeetingForm objFarmerMeetingForm;
        public frmDemoPlots objfrmDemoPlots;
        public MisconductForm objMisconductForm;
        public AuditMisconductBranch objAuditMisconductBranch;
        public Physicalstkcount objPhyStkCnt;
        byte[] ImgArr;
        public frmDisplayImage()
        {
            InitializeComponent();
        }
        public frmDisplayImage(byte[] arr)
        {
            InitializeComponent();
            ImgArr = arr;
        }

        private void frmDisplayImage_Load(object sender, EventArgs e)
        {
            btnDownload.Enabled = false;
            if (ImgArr != null)
            {
                GetImage(ImgArr);
                btnDownload.Enabled = true;
            }
            else
            {
                pbDocPic.Image = null;
            }

        }

        public void GetImage(byte[] imageData)
        {
            try
            {
                Image newImage;
                int imgWidth = 0;
                int imghieght = 0;
                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                {                    
                    ms.Write(imageData, 0, imageData.Length);
                    newImage = Image.FromStream(ms, true);
                }
                imgWidth = newImage.Width;
                imghieght = newImage.Height;

                pbDocPic.Width = imgWidth;
                pbDocPic.Height = imghieght;
                pbDocPic.Image = newImage;
               
               
            }
            catch (Exception ex)
            {
            }
        }



        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private string byteArrayToImage(byte[] byteArray)
        {
            System.Drawing.Image newImage;
            //if (File.Exists("D:\\Document.jpeg"))
            //     File.Delete("D:\\Document.jpeg");
            string strFileName = "D:\\Document.jpeg";
            if (byteArray != null)
            {
                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    newImage = System.Drawing.Image.FromStream(stream);
                    newImage.Save(strFileName);
                }
                return strFileName;
            }
            else
            {
                return "";
            }
        }
       

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string ImgFile = "";

            if (ImgArr!=null)
            {                              
                ImgFile = byteArrayToImage(ImgArr);

                if (ImgFile != "")
                {
                    MessageBox.Show("Image Download Successfully -  File Path-D:\\Document.jpeg", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    this.Dispose();
                }
            }          


        }
    }
}
