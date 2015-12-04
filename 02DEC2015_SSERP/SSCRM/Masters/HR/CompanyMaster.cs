using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using System.IO;
using SSTrans;
using SSAdmin;
namespace SSCRM
{
    public partial class CompanyMaster : Form
    {
        Master objMaster = null;
        SQLDB objSQLDB = new SQLDB();

        public CompanyMaster()
        {
            InitializeComponent();
        }

        private void CompanyMaster_Load(object sender, EventArgs e)
        {
            GetDataBind();
            gvCompany.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);

        }
        public void GetDataBind()
        {
            int intRow = 1;
            objSQLDB = new SQLDB();
            DataSet ds = objSQLDB.ExecuteDataSet("SELECT CM_COMPANY_CODE,CM_COMPANY_NAME,ACTIVE,CM_LOGO From company_mas");
            objSQLDB = null;
            DataTable dt = ds.Tables[0];
            gvCompany.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellCCode = new DataGridViewTextBoxCell();
                cellCCode.Value = dt.Rows[i]["CM_COMPANY_CODE"];
                tempRow.Cells.Add(cellCCode);

                DataGridViewCell cellCName = new DataGridViewTextBoxCell();
                cellCName.Value = dt.Rows[i]["CM_COMPANY_NAME"];
                tempRow.Cells.Add(cellCName);
                intRow = intRow + 1;
                gvCompany.Rows.Add(tempRow);
            }
        }
        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "Images (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|" + "All files (*.*)|*.*";
            od.Multiselect = true;
            if (od.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblPath.Text = od.FileNames[0].ToString();
                Image loadedImage = Image.FromFile(lblPath.Text);
                pictureBox1.BackgroundImage = loadedImage;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objMaster = new Master();
            byte[] imageData = { 0 };
            if (lblPath.Text != "")
                imageData = ReadFile(lblPath.Text);
            string sAct = "";
            if (chkActive.Checked == true)
                sAct = "T";
            else
                sAct = "F";
            int ival = objMaster.SaveCompany(txtCompanyCode.Text, txtCompanyName.Text, sAct, imageData);
            if (ival > 0)
                MessageBox.Show("The record Saved successfully.");
            else
                MessageBox.Show("Data is not Updated.");
            GetDataBind();
            btbClear_Click(null, null);
        }

        byte[] ReadFile(string sPath)
        {
            byte[] data = null;
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            data = br.ReadBytes((int)numBytes);
            return data;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvCompany_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvCompany.Rows[e.RowIndex].Cells["Edit"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvCompany.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
                    {
                        string sCmpCode = gvCompany.Rows[e.RowIndex].Cells[gvCompany.Columns["CompanyCode"].Index].Value.ToString();
                        objSQLDB = new SQLDB();
                        DataSet ds = objSQLDB.ExecuteDataSet("SELECT CM_COMPANY_CODE,CM_COMPANY_NAME,ACTIVE,CM_LOGO From company_mas WHERE CM_COMPANY_CODE='" + sCmpCode + "'");
                        objSQLDB = null;
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            txtCompanyCode.Text = ds.Tables[0].Rows[0]["CM_COMPANY_CODE"].ToString();
                            txtCompanyName.Text = ds.Tables[0].Rows[0]["CM_COMPANY_NAME"].ToString();
                            if (ds.Tables[0].Rows[0]["ACTIVE"].ToString() == "T")
                                chkActive.Checked = true;
                            else
                                chkActive.Checked = false;
                            if (ds.Tables[0].Rows[0]["CM_LOGO"].ToString() != "")
                                GetImage((byte[])ds.Tables[0].Rows[0]["CM_LOGO"]);
                            else
                                pictureBox1.BackgroundImage = null;//SSCRM.Properties.Resources.nomale;
                        }
                    }
                }
                if (e.ColumnIndex == gvCompany.Columns["Delete"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        string sCmpCode = gvCompany.Rows[e.RowIndex].Cells[gvCompany.Columns["CompanyCode"].Index].Value.ToString();
                        objSQLDB = new SQLDB();
                        int isqlqry = objSQLDB.ExecuteSaveData("DELETE FROM COMPANY_MAS WHERE CM_COMPANY_CODE='" + sCmpCode + "'");
                        objSQLDB = null;
                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
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
                pictureBox1.BackgroundImage = newImage;
                this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
            }
        }

        private void btbClear_Click(object sender, EventArgs e)
        {
            txtCompanyCode.Text = "";
            txtCompanyName.Text = "";
            chkActive.Checked = false;
            pictureBox1.BackgroundImage = null;//SSCRM.Properties.Resources.nomale;
        }
    }
}
