using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using SSCRMDB;
using SSTrans;

namespace SSCRM
{
    public partial class frmDemoPlotResults : Form
    {
        public frmDemoPlots objfrmDemoPlots = null;
        ServiceDeptDB objServicedb = null;
        DataRow[] drs;
        private string CompCode = "", BranCode = "", strECode = "";
        byte[] CropImg;
        byte[] TreatedImg;

        public frmDemoPlotResults()
        {
            InitializeComponent();
            dtpObservDate.Value = DateTime.Today;
        }
        public frmDemoPlotResults(string strCompCode,string strBranCode,string sEcode)
        {
            InitializeComponent();
            dtpObservDate.Value = DateTime.Today;
            CompCode = strCompCode;
            BranCode = strBranCode;
            strECode = sEcode;
        }
        public frmDemoPlotResults(DataRow[] dr, string strCompCode, string strBranCode, byte[] CropImgArr,byte[] TreatedImgArr)
        {
            InitializeComponent();
            drs = dr;

            CompCode = strCompCode;
            BranCode = strBranCode;
            CropImg = CropImgArr;
            TreatedImg = TreatedImgArr;
        }

        private void frmDemoPlotResults_Load(object sender, EventArgs e)
        {
            dtpObservDate.Value = DateTime.Today;
            FillEmployeeData(CompCode, BranCode);
            cbEcode.SelectedValue = strECode;

            if (drs != null)
            {
                dtpObservDate.Text = Convert.ToDateTime(drs[0]["ObservDate"]).ToString("dd/MMM/yyyy");
                rtbNotifyResult.Text = drs[0]["NotifyResult"].ToString();
                rtbFarmerOpinion.Text = drs[0]["FarmerOpinion"].ToString();
                txtRemarks.Text = drs[0]["ResultRemarks"].ToString();
                if (drs[0]["AoEcode"].ToString() != "")
                    cbEcode.SelectedValue = drs[0]["AoEcode"].ToString();

                //GetCropImage(CropImg);
                //GetTreatedImage(TreatedImg);
            } 
            
        }

        private void FillEmployeeData(string sCompCode,string sBranCode)
        {
            objServicedb = new ServiceDeptDB();
            DataSet dsEmp = null;

            if (sCompCode.Length>0 && sBranCode.Length > 0)
            {
                try
                {
                    
                    Cursor.Current = Cursors.WaitCursor;
                    cbEcode.DataSource = null;
                    cbEcode.Items.Clear();
                    dsEmp = objServicedb.ServiceLevelEcodeSearch_Get(sCompCode, sBranCode, CommonData.DocMonth, txtEcodeSearch.Text.ToString());
                    DataTable dtEmp = dsEmp.Tables[0];
                    
                    if (dtEmp.Rows.Count > 0)
                    {
                        cbEcode.DataSource = dtEmp;
                        cbEcode.DisplayMember = "ENAME";
                        cbEcode.ValueMember = "ECODE";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (cbEcode.SelectedIndex > -1)
                    {
                        cbEcode.SelectedIndex = 0;
                        strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                    }
                    objServicedb = null;
                    Cursor.Current = Cursors.Default;
                }
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private bool CheckData()
        {
            bool bFlag = true;

            if (cbEcode.SelectedIndex == -1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Ao Name", "Demo Plot Results", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbEcode.Focus();
                return bFlag;
            }
            if (dtpObservDate.Value > DateTime.Today)
            {
                bFlag = false;
                MessageBox.Show("Please Select Valid Observed Date", "Demo Plot Results", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpObservDate.Focus();
                return bFlag;
            }

            if (rtbNotifyResult.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Notify Results", "Demo Plot Results", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rtbNotifyResult.Focus();
                return bFlag;

            }
            
            else if (rtbFarmerOpinion.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Farmer Opinion", "Demo Plot Results", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rtbFarmerOpinion.Focus();
                return bFlag;

            }
            return bFlag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                DataGridView dgvDocumentDetl = null;
                

                dgvDocumentDetl = ((frmDemoPlots)objfrmDemoPlots).gvDemoPlotResults;
                AddDemoPlotResultDetailsToGrid(dgvDocumentDetl);

                //((frmDemoPlots)objfrmDemoPlots).dtDemoPlotResults.Rows.Add(new Object[] { "-1", cbEcode.SelectedValue.ToString(), Convert.ToDateTime(dtpObservDate.Value).ToString("dd/MMM/yyyy"), cbEcode.Text.ToString(), rtbFarmerOpinion.Text.Replace("\'", ""), rtbNotifyResult.Text.Replace("\'", "")
                //    ,txtRemarks.Text.ToString().Replace("'",""),arr,arr1 });
                //((frmDemoPlots)objfrmDemoPlots).GetDemoPlotResultDetails();

                this.Close();
            }
        }

        public void AddDemoPlotResultDetailsToGrid(DataGridView dgvDocDetl)
        {
            int intRow = 0;
            bool IsItemExisted = false;
            intRow = dgvDocDetl.Rows.Count + 1;

            try
            {
                Image Cropimg = picCropArea.Image;
                byte[] arr = null;
                ImageConverter converter = new ImageConverter();
                if (Cropimg != null)
                    arr = (byte[])converter.ConvertTo(Cropimg, typeof(byte[]));

                Image TreatedAreaImg = picTreatedArea.Image;
                byte[] arr1 = null;
                if (TreatedAreaImg != null)
                    arr1 = (byte[])converter.ConvertTo(TreatedAreaImg, typeof(byte[]));
                

                DataGridViewRow tempRow = new DataGridViewRow();

                DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                cellSlNo.Value = intRow;
                tempRow.Cells.Add(cellSlNo);

                DataGridViewCell cellAoEcode = new DataGridViewTextBoxCell();
                cellAoEcode.Value = cbEcode.SelectedValue.ToString();
                tempRow.Cells.Add(cellAoEcode);

                DataGridViewCell cellObservDate = new DataGridViewTextBoxCell();
                cellObservDate.Value = Convert.ToDateTime(dtpObservDate.Value).ToString("dd/MMM/yyyy");
                tempRow.Cells.Add(cellObservDate);

                DataGridViewCell cellAoName = new DataGridViewTextBoxCell();
                cellAoName.Value = cbEcode.Text.ToString();
                tempRow.Cells.Add(cellAoName);

                DataGridViewCell cellFarmerOpinion = new DataGridViewTextBoxCell();
                cellFarmerOpinion.Value = rtbFarmerOpinion.Text.Replace("\'", "");
                tempRow.Cells.Add(cellFarmerOpinion);

                DataGridViewCell cellNotifyResult = new DataGridViewTextBoxCell();
                cellNotifyResult.Value = rtbNotifyResult.Text.Replace("\'", "");
                tempRow.Cells.Add(cellNotifyResult);

                DataGridViewCell cellAoSuggestion = new DataGridViewTextBoxCell();
                cellAoSuggestion.Value = txtRemarks.Text.ToString().Replace("'", "");
                tempRow.Cells.Add(cellAoSuggestion);

                DataGridViewCell cellCropImage = new DataGridViewTextBoxCell();
                cellCropImage.Value = arr;
                tempRow.Cells.Add(cellCropImage);

                DataGridViewCell cellTreatedImage = new DataGridViewTextBoxCell();
                cellTreatedImage.Value = arr1;
                tempRow.Cells.Add(cellTreatedImage);

                intRow = intRow + 1;
                dgvDocDetl.Rows.Add(tempRow);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dtpObservDate.Value = DateTime.Today;
            rtbNotifyResult.Text = string.Empty;
            rtbFarmerOpinion.Text = string.Empty;
            cbEcode.SelectedIndex = 0;
            txtRemarks.Text = "";
            picCropArea.Image = null;
            picTreatedArea.Image = null;
        }

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            FillEmployeeData(CompCode, BranCode);
        }

        private void btnBrowseCropArea_Click(object sender, EventArgs e)
        {
            ImageBrowser img = new ImageBrowser("DEMO_PLOT_RESULT", picCropArea, "DOCUMENT");
            img.ShowDialog();
        }

        private void btnTreatedAreaImage_Click(object sender, EventArgs e)
        {
            ImageBrowser img = new ImageBrowser("DEMO_PLOT_RESULT", picTreatedArea, "DOCUMENT");
            img.ShowDialog();
        }

        public void GetCropImage(byte[] imageData)
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

                picCropArea.Width = imgWidth;
                picCropArea.Height = imghieght;
                picCropArea.Image = newImage;


            }
            catch (Exception ex)
            {
            }
        }

        public void GetTreatedImage(byte[] imageData)
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

                picTreatedArea.Width = imgWidth;
                picTreatedArea.Height = imghieght;
                picTreatedArea.Image = newImage;


            }
            catch (Exception ex)
            {
            }
        }
     
    }
}
