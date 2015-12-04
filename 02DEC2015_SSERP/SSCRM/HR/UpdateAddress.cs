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
using System.Data.SqlClient;
using System.Configuration;
namespace SSCRM
{
    public partial class UpdateAddress : Form
    {
        SQLDB objSQLDB = new SQLDB();
        Security objSecurity = new Security();
        HRInfo objHrInfo;
        public UpdateAddress()
        {
            InitializeComponent();
        }

        private void UpdateAddress_Load(object sender, EventArgs e)
        {
            if (CommonData.LogUserId.ToUpper() == "ADMIN")
                btnProcessBulkUpload.Visible = true;
            else
                btnProcessBulkUpload.Visible = false;
        }

        private void txtEcodeSearch_Validated(object sender, EventArgs e)
        {
            
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
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            int iRetVal = 0;
            byte[] imageData = { 0 };
            int iPin = 0;
            if (addressPresent.Pin.Length > 0)
                iPin = Convert.ToInt32(addressPresent.Pin);
            else
                iPin = 0;
            if (lblPath.Text != "")
                imageData = ReadFile(lblPath.Text);
            //else
            //    imageData = (byte)pictureBox1.BackgroundImage;
            //string sConnVal = ConfigurationSettings.AppSettings["DBCon"].ToString();
            //objSecurity = new Security();
            //SqlConnection CN = new SqlConnection(objSecurity.GetDecodeString(sConnVal));
            try
            {

                string sqlStr = "UPDATE HR_APPL_MASTER_HEAD SET HAMH_PD_BLOOD_GROUP_CODE='" + cmbBloodGrp_optional.Text + "',HAMH_ADD_PRES_ADDR_HNO='" + addressPresent.HouseNo + "'," +
                    "HAMH_ADD_PRES_ADDR_LANDMARK='" + addressPresent.LandMark + "',HAMH_ADD_PRES_ADDR_VILL_OR_TOWN='" + addressPresent.Village + "',HAMH_ADD_PRES_ADDR_MANDAL='" + addressPresent.Mondal + "'," +
                    "HAMH_ADD_PRES_ADDR_DISTRICT='" + addressPresent.District + "',HAMH_ADD_PRES_ADDR_STATE='" + addressPresent.State + "',HAMH_ADD_PRES_ADDR_PIN=" + iPin + ",HAMH_ADD_PRES_ADDR_PHONE='" + txtMobileNo.Text + "'";
                //if (lblPath.Text != "")
                //    sqlStr += ", HAMH_MY_PHOTO='" + (object)imageData + "'";
                sqlStr += " WHERE HAMH_EORA_CODE=" + txtEcodeSearch.Text;
                //SqlCommand SqlCom = new SqlCommand(sqlStr, CN);
                //if (lblPath.Text.Length > 1)
                //    SqlCom.Parameters.Add(new SqlParameter("@Photo", (object)imageData));
                //CN.Open();
                iRetVal = objSQLDB.ExecuteSaveData(sqlStr);
                if (lblPath.Text != "")
                {
                    objHrInfo = new HRInfo();
                    objHrInfo.UpdatePhoto(Convert.ToInt32(lblApplNo.Text), imageData);
                }
                //CN.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
            }
            if (iRetVal > 0)
            {
                btnClear_Click(null, null);
                MessageBox.Show("Selected information Has Been Updated", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Selected information not Updated", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            objSecurity = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEcodeSearch.Text = "";
            txtMemberName.Text = "";
            txtFatherName.Text = "";
            txtHRISDesig.Text = "";
            meHRISDataofBirth.Text = "";
            meHRISDateOfJoin.Text = "";
            txtMobileNo.Text = "";
            lblApplNo.Text = "0";
            lblPath.Text = "";
            cmbBloodGrp_optional.SelectedIndex = 0;
            addressPresent.HouseNo = "";
            addressPresent.Village = "";
            addressPresent.LandMark = "";
            addressPresent.Mondal = "";
            addressPresent.District = "";
            addressPresent.State = "";
            addressPresent.Pin = "";
            pictureBox1.BackgroundImage = SSCRM.Properties.Resources.nomale;
        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnProcessBulkUpload_Click(object sender, EventArgs e)
        {
            int iCount = 0;
            int iUploaded = 0;
            int iEcode = 0;
            objSQLDB = new SQLDB();
            try
            {
                iCount = Convert.ToInt32(objSQLDB.ExecuteDataSet("SELECT count(ecode) FROM temp_hr_photo_upload21112013 " +
                            "INNER JOIN HR_APPL_MASTER_HEAD ON HAMH_EORA_CODE = ecode " +
                            "where HAMH_MY_PHOTO is null").Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            for(;iCount!=0;)
            {
                try
                {
                    iEcode = UpdatePhoto();
                    iUploaded++;
                    iCount = Convert.ToInt32(objSQLDB.ExecuteDataSet("SELECT count(ecode) FROM temp_hr_photo_upload21112013 " +
                            "INNER JOIN HR_APPL_MASTER_HEAD ON HAMH_EORA_CODE = ecode " +
                            "where HAMH_MY_PHOTO is null").Tables[0].Rows[0][0].ToString());
                    lblApplNo.Text = iUploaded.ToString();
                    lblApplNo.Visible = true;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(iUploaded.ToString() + " Pics Uploaded\n" + ex.ToString());
                    //iCount = 0;
                    objSQLDB.ExecuteSaveData("DELETE FROM temp_hr_photo_upload21112013 WHERE ecode = " + iEcode);
                }
            }
            MessageBox.Show(iUploaded.ToString() + " Pics Uploaded\n");
            lblApplNo.Text = "0";            
            objSQLDB = null;
        }
        private int UpdatePhoto()
        {
            objSQLDB = new SQLDB();
            objHrInfo = new HRInfo();
            DataTable dt = new DataTable();
            int iEcode = 0;
            try
            {
                dt = objSQLDB.ExecuteDataSet("SELECT top 1 HAMH_APPL_NUMBER,HAMH_EORA_CODE FROM temp_hr_photo_upload21112013 " +
                            "INNER JOIN HR_APPL_MASTER_HEAD ON HAMH_EORA_CODE = ecode " +
                            "where HAMH_MY_PHOTO is null").Tables[0];
                int iApplNo = Convert.ToInt32(dt.Rows[0]["HAMH_APPL_NUMBER"].ToString());
                iEcode = Convert.ToInt32(dt.Rows[0]["HAMH_EORA_CODE"].ToString());
                Image loadedImage = Image.FromFile("D:\\Scan Photos\\" + iEcode + ".jpg");
                byte[] imageData = { 0 };
                imageData = ReadFile("D:\\Scan Photos\\" + iEcode + ".jpg");
                objHrInfo.UpdatePhoto(iApplNo, imageData);
            }
            catch (Exception ex)
            {
                objSQLDB.ExecuteSaveData("DELETE FROM temp_hr_photo_upload21112013 WHERE ecode = " + iEcode);
            }
            objHrInfo = null;
            dt = null;
            return iEcode;
            
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.ToString().Trim().Length > 4)
            {
                objSQLDB = new SQLDB();
                string sqlQry = "SELECT HAMH_APPL_NUMBER,MEMBER_NAME,FATHER_NAME,DESIG,EMP_DOJ,EMP_DOB,HAMH_PD_BLOOD_GROUP_CODE,HAMH_ADD_PRES_ADDR_HNO,HAMH_ADD_PRES_ADDR_LANDMARK," +
                        "HAMH_ADD_PRES_ADDR_VILL_OR_TOWN,HAMH_ADD_PRES_ADDR_MANDAL,HAMH_ADD_PRES_ADDR_DISTRICT,HAMH_ADD_PRES_ADDR_STATE," +
                        "HAMH_ADD_PRES_ADDR_PIN,HAMH_ADD_PRES_ADDR_PHONE,HAMH_MY_PHOTO FROM EORA_MASTER A INNER JOIN HR_APPL_MASTER_HEAD B ON A.ECODE=B.HAMH_EORA_CODE" +
                        " WHERE ECODE='" + txtEcodeSearch.Text + "'";
                DataSet ds = objSQLDB.ExecuteDataSet(sqlQry);
                objSQLDB = null;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblApplNo.Text = ds.Tables[0].Rows[0]["HAMH_APPL_NUMBER"].ToString();
                    txtMemberName.Text = ds.Tables[0].Rows[0]["MEMBER_NAME"].ToString();
                    txtFatherName.Text = ds.Tables[0].Rows[0]["FATHER_NAME"].ToString();
                    txtHRISDesig.Text = ds.Tables[0].Rows[0]["DESIG"].ToString();
                    meHRISDataofBirth.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EMP_DOB"]).ToString("dd/MM/yyyy");
                    meHRISDateOfJoin.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["EMP_DOJ"]).ToString("dd/MM/yyyy");
                    if (ds.Tables[0].Rows[0]["HAMH_PD_BLOOD_GROUP_CODE"].ToString() == "")
                        cmbBloodGrp_optional.SelectedIndex = 0;
                    else
                        cmbBloodGrp_optional.Text = ds.Tables[0].Rows[0]["HAMH_PD_BLOOD_GROUP_CODE"].ToString();
                    addressPresent.HouseNo = ds.Tables[0].Rows[0]["HAMH_ADD_PRES_ADDR_HNO"].ToString();
                    addressPresent.LandMark = ds.Tables[0].Rows[0]["HAMH_ADD_PRES_ADDR_LANDMARK"].ToString();
                    addressPresent.Village = ds.Tables[0].Rows[0]["HAMH_ADD_PRES_ADDR_VILL_OR_TOWN"].ToString();
                    addressPresent.Mondal = ds.Tables[0].Rows[0]["HAMH_ADD_PRES_ADDR_MANDAL"].ToString();
                    addressPresent.District = ds.Tables[0].Rows[0]["HAMH_ADD_PRES_ADDR_DISTRICT"].ToString();
                    addressPresent.State = ds.Tables[0].Rows[0]["HAMH_ADD_PRES_ADDR_STATE"].ToString();
                    addressPresent.Pin = ds.Tables[0].Rows[0]["HAMH_ADD_PRES_ADDR_PIN"].ToString();
                    txtMobileNo.Text = ds.Tables[0].Rows[0]["HAMH_ADD_PRES_ADDR_PHONE"].ToString();
                    if (ds.Tables[0].Rows[0]["HAMH_MY_PHOTO"].ToString() != "")
                    {
                        GetImage((byte[])ds.Tables[0].Rows[0]["HAMH_MY_PHOTO"]);
                        //if (CommonData.BranchType == "HO" || CommonData.LogUserId == "ADMIN")
                        //    btnBrowser.Enabled = true;
                        //else
                        //    btnBrowser.Enabled = false;
                    }
                    else
                    {
                        pictureBox1.BackgroundImage = SSCRM.Properties.Resources.nomale;
                        btnBrowser.Enabled = true;
                    }
                }
                else
                {
                    //txtEcodeSearch.Text = "";
                    txtMemberName.Text = "";
                    txtFatherName.Text = "";
                    txtHRISDesig.Text = "";
                    meHRISDataofBirth.Text = "";
                    meHRISDateOfJoin.Text = "";
                    txtMobileNo.Text = "";
                    lblApplNo.Text = "0";
                    cmbBloodGrp_optional.SelectedIndex = 0;
                    addressPresent.HouseNo = "";
                    addressPresent.Village = "";
                    addressPresent.LandMark = "";
                    addressPresent.Mondal = "";
                    addressPresent.District = "";
                    addressPresent.State = "";
                    addressPresent.Pin = "";
                    pictureBox1.BackgroundImage = SSCRM.Properties.Resources.nomale;
                }
            }
            else
            {
                txtMemberName.Text = "";
                txtFatherName.Text = "";
                txtHRISDesig.Text = "";
                meHRISDataofBirth.Text = "";
                meHRISDateOfJoin.Text = "";
                txtMobileNo.Text = "";
                lblApplNo.Text = "0";
                cmbBloodGrp_optional.SelectedIndex = 0;
                addressPresent.HouseNo = "";
                addressPresent.Village = "";
                addressPresent.LandMark = "";
                addressPresent.Mondal = "";
                addressPresent.District = "";
                addressPresent.State = "";
                addressPresent.Pin = "";
                pictureBox1.BackgroundImage = SSCRM.Properties.Resources.nomale;
            }
        }
    }
}
