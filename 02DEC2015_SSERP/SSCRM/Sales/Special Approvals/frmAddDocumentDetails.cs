using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SSCRMDB;
using SSTrans;
using SSCRM;

namespace SSCRM
{
    public partial class frmAddDocumentDetails : Form
    {
        public frmSpecialApprovals objfrmSpecialApprovals = null;
        public frmDemoPlots objfrmDemoPlots = null;
        public FarmerMeetingForm objFarmerMeetingForm = null;
        public MisconductForm objMisconductForm = null;
        public AuditMisconductBranch objAuditMisconductBranch = null;
        private bool flagUpdate = false;
        string strDocName = "", strDocDesc = "", sfrmType = "";
        byte[] ImgArr;
             
        public frmAddDocumentDetails(string sType)
        {
            InitializeComponent();
            sfrmType = sType;
        }    


        private void frmAddDocumentDetails_Load(object sender, EventArgs e)
        {
           
        }
    
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private bool CheckData()
        {
            bool flag = true;


            if (txtDocmentName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Document or Image Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDocmentName.Focus();
                return flag;
            }
            if (sfrmType == "SPECIAL_APPROVALS" || sfrmType == "AUDIT_QUERIES")
            {
                if (txtDocDesc.Text.Length == 0 || txtDocDesc.Text.Length < 10)
                {
                    flag = false;
                    MessageBox.Show("Please Enter Document Description", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDocDesc.Focus();
                    return flag;
                }
            }
            if (pbDocPic.Image == null)
            {
                flag = false;
                MessageBox.Show("Please Attach Document or Image", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnBrowse.Focus();
                return flag;
            }

            return flag;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataGridView dgvDocumentDetl = null;
            try
            {
                if (CheckData() == true)
                {
                    if (sfrmType == "SPECIAL_APPROVALS")
                    {
                        dgvDocumentDetl = ((frmSpecialApprovals)objfrmSpecialApprovals).gvDocumentDetl;
                        AddDocumentDetailsToGrid(dgvDocumentDetl);
                    }                    
                    else if (sfrmType == "FARMER_MEET")
                    {
                        dgvDocumentDetl = ((FarmerMeetingForm)objFarmerMeetingForm).gvDocDetl;
                        AddDocumentDetailsToGrid(dgvDocumentDetl);
                    }
                    else if (sfrmType == "AUDIT_QUERIES")
                    {
                        dgvDocumentDetl = ((MisconductForm)objMisconductForm).gvDocumentDetl;
                        AddDocumentDetailsToGrid(dgvDocumentDetl);
                    }
                    else if (sfrmType == "AUDIT_BRANCH")
                    {
                        dgvDocumentDetl = ((AuditMisconductBranch)objAuditMisconductBranch).gvDocumentDetl;
                        AddDocumentDetailsToGrid(dgvDocumentDetl);
                    }
                   
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void AddDocumentDetailsToGrid(DataGridView dgvDocDetl)
        {
            int intRow = 0;
            bool IsItemExisted = false;
            intRow = dgvDocDetl.Rows.Count + 1;

            try
            {

                Image img = pbDocPic.Image;
                byte[] arr;
                ImageConverter converter = new ImageConverter();
                arr = (byte[])converter.ConvertTo(img, typeof(byte[]));


                DataGridViewRow tempRow = new DataGridViewRow();

                DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                cellSlNo.Value = intRow;
                tempRow.Cells.Add(cellSlNo);

                DataGridViewCell cellDocName = new DataGridViewTextBoxCell();
                cellDocName.Value = txtDocmentName.Text.ToString().Replace("'", "");
                tempRow.Cells.Add(cellDocName);

                DataGridViewCell cellDocDesc = new DataGridViewTextBoxCell();
                cellDocDesc.Value = txtDocDesc.Text.ToString().Replace("'", "");
                tempRow.Cells.Add(cellDocDesc);

                DataGridViewCell cellImage = new DataGridViewTextBoxCell();
                cellImage.Value = arr;
                tempRow.Cells.Add(cellImage);

                intRow = intRow + 1;
                dgvDocDetl.Rows.Add(tempRow);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

       
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            ImageBrowser img = new ImageBrowser("IMAGE_DETL", pbDocPic,"DOCUMENT");
            img.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtDocmentName.Text = "";
            txtDocDesc.Text = "";            
            pbDocPic.Image = null;
        }

        private void txtDocmentName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtDocDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        public void GetImage(byte[] imageData)
        {
            try
            {
                Image newImage;
                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                {
                    ms.Write(imageData, 0, imageData.Length);
                    newImage = Image.FromStream(ms, true);
                }
                pbDocPic.BackgroundImage = newImage;
                this.pbDocPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
            }
        }

      

    }
}
