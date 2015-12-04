using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.Text.RegularExpressions;
namespace SSCRM
{
    public partial class MemberEnrollment : Form
    {
        SQLDB objSqlDb = null;
        Security objSecurity = null;
        byte[] imageData;
        bool flagUpdate = false;
        int tdpId=0;
        public MemberEnrollment()
        {
            InitializeComponent();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            photoCapture.Stop();
            imageData = ImageToByte(photoCapture.ImgWebCam.Image);
        }

        private void btnCapture_MouseHover(object sender, EventArgs e)
        {
            photoCapture.Start();
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            //photoCapture.BackgroundImage = null;
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "Images (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|" + "All files (*.*)|*.*";
            od.Multiselect = true;
            if (od.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lblPath.Text = od.FileNames[0].ToString();
                //Image loadedImage = Image.FromFile(lblPath.Text);
                //if (loadedImage.Height > 600 || loadedImage.Width > 800)
                //{
                //    lblPath.Text = "";
                //    MessageBox.Show("Please select image between size(600 W * 800 H)", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                byte[] image = getResizedImage(lblPath.Text, photoCapture.ImgWebCam.Size.Width, photoCapture.ImgWebCam.Size.Height);
                Image newImage;
                using (MemoryStream ms = new MemoryStream(image, 0, image.Length))
                {
                    ms.Write(image, 0, image.Length);
                    newImage = Image.FromStream(ms, true);
                }
                photoCapture.ImgWebCam.Image = null;
                photoCapture.ImgWebCam.Image = newImage;

                imageData = ImageToByte(newImage);
            }
        }
        byte[] getResizedImage(String path, int width, int height)
        {
            Bitmap imgIn = new Bitmap(path);
            double y = imgIn.Height;
            double x = imgIn.Width;

            double factor = 1;
            if (width > 0)
            {
                factor = width / x;
            }
            else if (height > 0)
            {
                factor = height / y;
            }
            System.IO.MemoryStream outStream = new System.IO.MemoryStream();
            Bitmap imgOut = new Bitmap((int)(x * factor), (int)(y * factor));

            // Set DPI of image (xDpi, yDpi)
            imgOut.SetResolution(72, 72);

            Graphics g = Graphics.FromImage(imgOut);
            g.Clear(Color.White);
            g.DrawImage(imgIn, new Rectangle(0, 0, (int)(factor * x), (int)(factor * y)),
              new Rectangle(0, 0, (int)x, (int)y), GraphicsUnit.Pixel);

            imgOut.Save(outStream, getImageFormat(path));
            return outStream.ToArray();
        }
        ImageFormat getImageFormat(String path)
        {
            switch (Path.GetExtension(path))
            {
                case ".bmp": return ImageFormat.Bmp;
                case ".gif": return ImageFormat.Gif;
                case ".jpg": return ImageFormat.Jpeg;
                case ".png": return ImageFormat.Png;
                default: break;
            }
            return ImageFormat.Jpeg;
        }
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        public void GetImage(byte[] imageDatas)
        {
            try
            {
                Image newImage;
                imageData = imageDatas;
                using (MemoryStream ms = new MemoryStream(imageDatas, 0, imageDatas.Length))
                {
                    ms.Write(imageDatas, 0, imageDatas.Length);
                    newImage = Image.FromStream(ms, true);
                }
                photoCapture.ImgWebCam.Image= newImage;
                this.photoCapture.ImgWebCam.BackgroundImageLayout= System.Windows.Forms.ImageLayout.Stretch;
             
            }
            catch (Exception ex)
            {
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtMemberName.Text = "";
            txtSurName.Text = "";
            txtfName.Text = "";
           
            txtMemberSince.Text = "";
            txtBoothNo.Text = "";
            txtMandal.Text = "";
            txtMobileNo.Text = "";
            txtPanchayat.Text = "";
            txtVillage.Text = "";
            txtVoterId.Text = "";
            txtWardNo.Text = "";
            lblPath.Text = "";
            photoCapture.ImgWebCam.Image = null;
            dtpDOB.Value = DateTime.Today;
            txtActivity1.Text = "";
            txtActivity2.Text = "";
            txtActivity3.Text = "";
            txtVilSearch.Text = "";
            txtEmailId.Text = "";
            cmbCast.SelectedIndex = 0;
            cmbPostInParty.SelectedIndex = 0;
        }

        private void MemberEnrollment_Load(object sender, EventArgs e)
        {
            dtpDOB.Value = DateTime.Today;
            cmbForH.SelectedIndex = 0;
            cmbSex.SelectedIndex = 0;
            FillPositionsInParty();
            cmbPostInParty.SelectedIndex = 0;
            FillBooths(0);
            FillCasts();
        }

        private void FillCasts()
        {
            objSqlDb = new SQLDB();
            DataTable ds = new DataTable();
            try
            {
                string strCmd = "SELECT CAST_ID,CAST_NAME FROM TDP_CAST_MASTER";
                ds = objSqlDb.ExecuteDataSet(strCmd).Tables[0];
                DataRow dr = ds.NewRow();
                dr["CAST_NAME"] = "--Select--";
                dr["CAST_NAME"] = "--Select--";

                ds.Rows.InsertAt(dr, 0);
                if (ds.Rows.Count > 0)
                {
                    cmbCast.DataSource = ds;
                    cmbCast.DisplayMember = "CAST_NAME";
                    cmbCast.ValueMember = "CAST_ID";
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {

            }

        }

        private void FillBooths(int iBoothNO)
        {
            objSqlDb = new SQLDB();
            SqlParameter[] param = new SqlParameter[1];
            DataTable ds = new DataTable();
            try
            {
                param[0] = objSqlDb.CreateParameter("@sBoothNo", DbType.Int32, iBoothNO, ParameterDirection.Input);
                ds = objSqlDb.ExecuteDataSet("GetTDPBooths", CommandType.StoredProcedure, param).Tables[0];

                if (ds.Rows.Count > 0)
                {
                    cmbBooths.DataSource = ds;
                    cmbBooths.DisplayMember = "PBM_NAME";
                    cmbBooths.ValueMember = "pbm_no";
                }
                else
                {
                    MessageBox.Show("Entered Booth No Does Not Exist");
                    txtBoothNo.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;

            }

        }

        private void FillPositionsInParty()
        {
            objSqlDb = new SQLDB();
            string strCmd = "Select cast(TPD_ID as varchar)TPD_ID ,TPD_DESIG_NAME from TDP_PARTY_DESIG";
            try
            {
                DataTable dt = objSqlDb.ExecuteDataSet(strCmd).Tables[0];

                DataRow dr = dt.NewRow();
                dr["TPD_DESIG_NAME"] = "--Select--";
                dr["TPD_DESIG_NAME"] = "--Select--";

                dt.Rows.InsertAt(dr, 0);
                cmbPostInParty.DataSource = dt;
                cmbPostInParty.DisplayMember = "TPD_DESIG_NAME";
                cmbPostInParty.ValueMember = "TPD_ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void txtBoothNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtBoothNo.Text.Length > 0)
            {
                FillBooths(Convert.ToInt32(txtBoothNo.Text));
            }
        }

        private void txtMemberSince_KeyPress(object sender, KeyPressEventArgs e)
        {
            RestrictToDigits(e);
        }
        private void RestrictToDigits(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false)
                e.Handled = true;
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtBoothNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            RestrictToDigits(e);
        }

        private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            RestrictToDigits(e);
        }

        private void dtpDOJ_ValueChanged(object sender, EventArgs e)
        {
            int age = DateTime.Today.Year - dtpDOB.Value.Year;
            txtAge.Text = age.ToString();
        }
        private bool CheckData()
        {
            bool flag = true;
            if (txtMemberName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Enter Member Name", "TDP Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtMemberName.Focus();
                return flag;
            }
            if (txtSurName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Enter SurName", "TDP Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtSurName.Focus();
                return flag;
            }
            if (txtfName.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Enter Father/Husmand Name", "TDP Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtfName.Focus();
                return flag;
            }
            if(dtpDOB.Value==DateTime.Today)
            {
                flag = false;
                MessageBox.Show("Select Date of DOB", "TDP Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                dtpDOB.Focus();
                return flag;
            }
            if (Convert.ToInt32(txtAge.Text) == 0)
            {
                flag = false;
                MessageBox.Show("Select Date of DOB", "TDP Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                dtpDOB.Focus();
                return flag;
            }
            if (cmbPostInParty.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Select Position in Party", "TDP Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbPostInParty.Focus();
                return flag;
            }
            if (txtMemberSince.Text.Length == 0 || txtMemberSince.Text.Length < 4)
            {
                flag = false;
                MessageBox.Show("Enter valid Year", "TDP Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtMemberSince.Focus();
                return flag;
            }
            if (cmbCast.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Select Cast", "TDP Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbCast.Focus();
                return flag;
            }
            if (cmbBooths.SelectedIndex < 0)
            {
                flag = false;
                MessageBox.Show("Enter Booth NO", "TDP Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cmbBooths.Focus();
                return flag;
            }
            if (txtWardNo.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Enter Ward No", "TDP Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtWardNo.Focus();
                return flag;
            }
            if (txtVoterId.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Enter VoterID No", "TDP Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtWardNo.Focus();
                return flag;
            }
            if (txtMobileNo.Text.Length == 0 || txtMobileNo.Text.Length < 10)
            {
                flag = false;
                MessageBox.Show("Enter Mobile No", "TDP Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtMobileNo.Focus();
                return flag;
            }
            if (txtEmailId.Text.Length==0)
            {
                flag = false;
                MessageBox.Show("Enter Mail ID", "TDP Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtEmailId.Focus();
                return flag;
            }
            if (txtEmailId.Text.Length > 0)
            {
              flag = Regex.Match(txtEmailId.Text, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$").Value == txtEmailId.Text;
              if (flag == false)
              {
                  MessageBox.Show("Enter Valid Mail ID", "TDP Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
              }
              txtEmailId.Focus();
              return flag;
            }
            if (txtVillage.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Enter Village", "TDP Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtVillage.Focus();
                return flag;
            }
            if (txtPanchayat.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Enter Panchayat", "TDP Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtPanchayat.Focus();
                return flag;
            }
            if (txtMandal.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Enter Mandal", "TDP Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtMandal.Focus();
                return flag;
            }
            if (imageData == null)
            {
                flag = false;
                MessageBox.Show("Capture the Picture", "TDP Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return flag;
            }
            return flag;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                string strCmd = "";
                if (flagUpdate==false)
                {
                    strCmd = "INSERT into TDP_MEMBERS(" +
                        "TDP_VOTER_ID," +
                        "TDP_NAME," +
                        "TDP_SUR_NAME," +
                        "TDP_TOT_NAME," +
                        "TDP_FORH_TYPE," +
                        "TDP_FORH_NAME," +
                        "TDP_GENDER," +
                        "TDP_DOB," +
                        "TDP_AGE," +
                        "TDP_POST_IN_PARTY," +
                        "TDP_MEMBER_SINCE," +
                        "TDP_BOOTH_NO," +
                        "TDP_WARD_NO," +
                        "TDP_MOBILE_NO," +
                        "TDP_VALLAGE," +
                        "TDP_PANCHAYAT," +
                         "TDP_EMAIL," +
                        "TDP_MANDAL," +
                        "TDP_DISTRICT," +
                        "TDP_CREATED_BY," +
                        "TDP_CREATED_DATE," +
                        "TDP_MEMBER_ACTIVITY1," +
                        "TDP_MEMBER_ACTIVITY2," +
                        "TDP_MEMBER_ACTIVITY3," +
                        "TDP_CAST" +
                        ") VALUES('" + txtVoterId.Text.ToString().Replace(" ", "") +
                        "','" + txtMemberName.Text.Trim() +
                         "','" + txtSurName.Text.Trim() +
                         "','" + (txtMemberName.Text +' '+ txtSurName.Text) +
                         "','" + cmbForH.GetItemText(cmbForH.SelectedItem) +
                         "','" + txtfName.Text +
                         "','" + cmbSex.GetItemText(cmbSex.SelectedItem) +
                         "','" + dtpDOB.Value.ToString("dd/MMM/yyyy") +
                         "'," + Convert.ToInt32(txtAge.Text) +
                         ",'" + cmbPostInParty.Text.ToString()+
                         "'," + Convert.ToInt32(txtMemberSince.Text) +
                         "," + Convert.ToInt32(cmbBooths.SelectedValue.ToString()) +
                         ",'" + txtWardNo.Text +
                         "','" + txtMobileNo.Text +
                         "','" + txtVillage.Text +
                         "','" + txtPanchayat.Text +
                         "','" + txtEmailId.Text +
                         "','" + txtMandal.Text +
                         "','GUNTUR'" +
                         ",'" + CommonData.LogUserId +
                         "',getdate()" +
                         ",'" + txtActivity1.Text +
                         "','" + txtActivity2.Text +
                         "','" + txtActivity3.Text +
                         "','" + cmbCast.Text.ToString() + "')";
                }
                else
                {
                    strCmd = "UPDATE TDP_MEMBERS SET TDP_VOTER_ID='" + txtVoterId.Text.ToString().Replace(" ", "") +
                                                     "',TDP_NAME='" + txtMemberName.Text.Trim() +
                                                     "',TDP_SUR_NAME='" + txtSurName.Text.Trim() +
                                                     "',TDP_TOT_NAME='" + (txtMemberName.Text +' '+ txtSurName.Text) +
                                                     "',TDP_FORH_TYPE='" + cmbForH.GetItemText(cmbForH.SelectedItem) +
                                                     "',TDP_FORH_NAME='" + txtfName.Text +
                                                     "',TDP_GENDER='" + cmbSex.GetItemText(cmbSex.SelectedItem) +
                                                     "',TDP_DOB='" + dtpDOB.Value.ToString("dd/MMM/yyyy") +
                                                     "',TDP_AGE=" + Convert.ToInt32(txtAge.Text) +
                                                     ",TDP_POST_IN_PARTY='" + cmbPostInParty.Text.ToString()+
                                                     "',TDP_MEMBER_SINCE=" + Convert.ToInt32(txtMemberSince.Text) +
                                                     ",TDP_BOOTH_NO=" + Convert.ToInt32(cmbBooths.SelectedValue.ToString()) +
                                                     ",TDP_WARD_NO='" + txtWardNo.Text +
                                                     "',TDP_MOBILE_NO=" + Convert.ToInt64(txtMobileNo.Text) +
                                                     ",TDP_VALLAGE='" + txtVillage.Text +
                                                     "',TDP_PANCHAYAT='" + txtPanchayat.Text +
                                                     "',TDP_MANDAL='" + txtMandal.Text +
                                                     "',TDP_EMAIL='" + txtEmailId.Text +
                                                     "',TDP_DISTRICT='GUNTUR'" +
                                                     ",TDP_MODIFIED_BY='" + CommonData.LogUserId +
                                                     "',TDP_MODIFIED_DATE=getdate()" +
                                                     ",TDP_MEMBER_ACTIVITY1='" + txtActivity1.Text +
                                                     "',TDP_MEMBER_ACTIVITY2='" + txtActivity2.Text +
                                                     "',TDP_MEMBER_ACTIVITY3='" + txtActivity3.Text +
                                                     "',TDP_CAST='" + cmbCast.Text.ToString() + "' WHERE TDP_ID = "+tdpId;
                }
                int iRes = 0;
                try
                {
                    objSqlDb = new SQLDB();
                    iRes = objSqlDb.ExecuteSaveData(strCmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                //byte[] imageData = { 0 };
                //if (lblPath.Text != "")
                //    imageData = ReadFile(lblPath.Text);
                if (iRes > 0)
                {
                    if (imageData.Length > 0)
                    {
                        try
                        {
                            
                            if (flagUpdate==false)
                            {
                                strCmd = "SELECT TDP_ID FROM TDP_MEMBERS WHERE TDP_ID=(SELECT max(TDP_ID) FROM TDP_MEMBERS)";
                                objSqlDb = new SQLDB();
                                DataTable dt = objSqlDb.ExecuteDataSet(strCmd).Tables[0];
                                tdpId = Convert.ToInt32(dt.Rows[0]["TDP_ID"].ToString());
                            }
                            UpdatePhoto(tdpId, imageData);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    MessageBox.Show("Data Saved Successfully", "Tdp Entrollment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flagUpdate = false;
                    tdpId = 0;
                    txtMemberName.Text = "";
                    txtSurName.Text = "";
                    txtfName.Text = "";
                    txtMemberSince.Text = "";
                    txtMobileNo.Text = "";
                    txtVoterId.Text = "";                   
                    cmbPostInParty.SelectedIndex = 0;
                    photoCapture.ImgWebCam.Image = null;
                    dtpDOB.Value = DateTime.Today;
                    imageData = null;
                    lblPath.Text = "";
                    txtActivity1.Text = "";
                    txtActivity2.Text = "";
                    txtActivity3.Text = "";
                    txtVilSearch.Text = "";
                    cmbCast.SelectedIndex = 0;
                    cmbPostInParty.SelectedIndex = 0;
                    txtEmailId.Text = "";
                    txtMemberName.Focus();
                }
            }
        }
        byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.           
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes to read from file.
            //In this case we want to read entire file. So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);
            return data;
        }
        public void UpdatePhoto(int ApplicationID, byte[] buffer)
        {
            try
            {
                objSecurity = new Security();
                SqlConnection CN = new SqlConnection(objSecurity.GetDecodeString(ConfigurationSettings.AppSettings["DBCon"].ToString()));
                SqlCommand cmdPhoto = new SqlCommand("TDP_imageStore", CN);
                cmdPhoto.CommandType = CommandType.StoredProcedure;
                cmdPhoto.Parameters.AddWithValue("@ApplicatioNo", ApplicationID);
                cmdPhoto.Parameters.AddWithValue("@ivHAMH_MY_PHOTO", buffer);
                CN.Open();
                cmdPhoto.ExecuteNonQuery();
                CN.Close();
                objSecurity = null;
                //string strCmd = "UPDATE TDP_MEMBERS SET TDP_MEMBER_PHOTO=" + buffer + " WHERE TDP_ID = " + ApplicationID;
                //objSqlDb = new SQLDB();
                //int iRes = objSqlDb.ExecuteSaveData(strCmd);
            }
            //objSQLdb = new SQLDB();
            //SqlParameter[] param = new SqlParameter[3];
            //DataSet ds = new DataSet();
            //try
            //{
            //    param[0] = objSQLdb.CreateParameter("@imode", DbType.Int32, 111, ParameterDirection.Input);


            //    param[1] = objSQLdb.CreateParameter("@ApplicatioNo", DbType.Int32, ApplicationID, ParameterDirection.Input);
            //    param[2] = objSQLdb.CreateParameter("@ivHAMH_MY_PHOTO", DbType.Byte[], buffer, ParameterDirection.Input);
            //    ds = objSQLdb.ExecuteDataSet("DL_imageStore", CommandType.StoredProcedure, param);
            //}
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {

                //objSQLdb = null;
            }

        }

        private void txtVilSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtVilSearch.Text.Length == 0)
                {
                    cbVillage.DataSource = null;
                    cbVillage.DataBindings.Clear();
                    cbVillage.Items.Clear();
                    //if (btnSave.Enabled == true)
                    txtVillage.Text = "";
                    txtPanchayat.Text = "";
                    txtMandal.Text = "";

                }
                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {
                    if (FindInputAddressSearch() == false)
                    {
                        FillAddressData(txtVilSearch.Text);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private bool FindInputAddressSearch()
        {
            bool blFind = false;
            try
            {
                
                    for (int i = 0; i < this.cbVillage.Items.Count; i++)
                    {
                        string strItem = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                        if (strItem.IndexOf(txtVilSearch.Text) > -1)
                        {
                            blFind = true;
                            cbVillage.SelectedIndex = i;
                            txtVillage.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[1] + "";
                            txtPanchayat.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[1] + "";
                            txtMandal.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[2] + "";
                            //txtFirmDistrict.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[3] + "";
                            //txtFirmState.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[4] + "";
                            //txtFirmPin.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[5] + "";
                            //strStateCode = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[0] + "";
                            break;
                        }
                        break;
                    }
            }
            catch(Exception EX)
            {
                MessageBox.Show(EX.ToString());
            }
            return blFind;
        }

        private void FillAddressData(string sSearch)
        {
            Hashtable htParam = null;
            // objInvoiceData = new InvoiceDB();
            string strDist = string.Empty;
            DataSet dsVillage = null;
            DataTable dtVillage = new DataTable();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (sSearch.Trim().Length >= 0)
                    htParam = new Hashtable();
                //htParam.Add("sVillage", sSearch);
                //htParam.Add("sDistrict", "GUNTUR");



                //htParam.Add("sCDState", CommonData.StateCode);
                dsVillage = new DataSet();
                //dsVillage = objInvoiceData.GetVillageDataSet(htParam);
                dsVillage   =   GetVillages(sSearch, "GUNTUR", "AP");
                dtVillage = dsVillage.Tables[0];
                if (dtVillage.Rows.Count == 1)
                {


                    txtVillage.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();  // ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    txtPanchayat.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();  // ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    txtMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2]+ ""; 
                    //txtFirmDistrict.Text = dtVillage.Rows[0]["District"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                    //txtFirmState.Text = dtVillage.Rows[0]["State"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                    //txtFirmPin.Text = dtVillage.Rows[0]["PIN"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[5] + "";
                    //strStateCode = dtVillage.Rows[0]["CDState"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[0] + "";

                }
                else if(dtVillage.Rows.Count>1)
                {
                    txtVillage.Text = "";
                    txtPanchayat.Text = "";
                    txtMandal.Text = "";
                    FillAddressComboBox(dtVillage);
                }
                else
                {
                    //htParam = new Hashtable();
                    //htParam.Add("sVillage", "%" + sSearch);
                    //htParam.Add("sDistrict", strDist);
                    dsVillage = new DataSet();
                    dsVillage = GetVillages(sSearch,"GUNTUR","AP");
                    dtVillage = dsVillage.Tables[0];
                    FillAddressComboBox(dtVillage);
                    txtVillage.Text = "";
                    txtPanchayat.Text = "";
                    txtMandal.Text = "";
                }
            }
            catch(Exception EX)
            {
                MessageBox.Show(EX.ToString());
            }
        }
        private void FillAddressComboBox(DataTable dt)
        {
            DataTable dataTable = new DataTable("Village");

            dataTable.Columns.Add("StateID", typeof(String));
            dataTable.Columns.Add("Panchayath", typeof(String));
            dataTable.Columns.Add("Mandal", typeof(String));
            dataTable.Columns.Add("District", typeof(String));
            dataTable.Columns.Add("State", typeof(String));
            dataTable.Columns.Add("Pin", typeof(String));


            for (int i = 0; i < dt.Rows.Count; i++)
                dataTable.Rows.Add(new String[] { dt.Rows[i]["CDState"] + 
                     "", dt.Rows[i]["PANCHAYAT"] + 
                     "", dt.Rows[i]["MANDAL"] + 
                     "", dt.Rows[i]["DISTRICT"] + 
                     "", dt.Rows[i]["STATE"] + "", dt.Rows[i]["PIN"] + ""});

            
                cbVillage.DataBindings.Clear();
                cbVillage.DataSource = dataTable;
                cbVillage.DisplayMember = "Panchayath";
                cbVillage.ValueMember = "StateID";
           
        }
        private DataSet GetVillages(string vSearch, string sDistrict, string sStateCode)
        {
            objSqlDb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataSet ds = new DataSet();
            try
            {

                param[0] = objSqlDb.CreateParameter("@sVillage", DbType.String, vSearch, ParameterDirection.Input);
                param[1] = objSqlDb.CreateParameter("@sDistrict", DbType.String, sDistrict, ParameterDirection.Input);
                param[2] = objSqlDb.CreateParameter("@sCDState", DbType.String, sStateCode, ParameterDirection.Input);
                ds = objSqlDb.ExecuteDataSet("GetVinukondaVillageMaster_Proc", CommandType.StoredProcedure, param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSqlDb = null;
            }
            return ds;
        }

        private void btnVSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("MemberEntrollMent");
            VSearch.objMemberEnrollment = this;
            VSearch.ShowDialog();
        }

        private void cbVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVillage.SelectedIndex > -1)
            {
                if (this.cbVillage.Items[cbVillage.SelectedIndex].ToString() != "")
                {
                    txtVillage.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    txtPanchayat.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    txtMandal.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2] + "";
                }
            }
        }

        private void txtMemberName_TextChanged(object sender, EventArgs e)
        {
            if (txtMemberName.Text.Length > 0 && txtSurName.Text.Length > 0)
            {
                GetMemberDetails(txtMemberName.Text, txtSurName.Text);
            }
        }

        private void GetMemberDetails(string sName, string sSurName)
        {
            objSqlDb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataTable dt = new DataTable();
            try
            {

                param[0] = objSqlDb.CreateParameter("@sName", DbType.String, sName.Trim(), ParameterDirection.Input);
                param[1] = objSqlDb.CreateParameter("@sSurName", DbType.String, sSurName.Trim(), ParameterDirection.Input);
                dt = objSqlDb.ExecuteDataSet("GetMemberDetails", CommandType.StoredProcedure, param).Tables[0];

                

                    
                    if (dt.Rows.Count > 0)
                    {
                        flagUpdate = true;
                        tdpId = Convert.ToInt32(dt.Rows[0]["TDP_ID"].ToString());
                        txtVoterId.Text = dt.Rows[0]["VoterID"].ToString();
                        txtMemberName.Text = dt.Rows[0]["Name"].ToString();
                        txtMobileNo.Text =  dt.Rows[0]["MobileNO"].ToString();
                        txtSurName.Text = dt.Rows[0]["SurName"].ToString();
                        cmbForH.SelectedItem = dt.Rows[0]["FORHType"].ToString();
                        txtfName.Text = dt.Rows[0]["FatherName"].ToString();
                        cmbSex.SelectedItem = dt.Rows[0]["Gender"].ToString();
                        dtpDOB.Value = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString());
                        txtAge.Text = dt.Rows[0]["Age"].ToString();
                        cmbPostInParty.SelectedValue = dt.Rows[0]["PostInParty"].ToString();
                        txtMemberSince.Text = dt.Rows[0]["MemberSince"].ToString();
                        cmbBooths.SelectedValue = dt.Rows[0]["BoothNo"].ToString();
                        txtWardNo.Text = dt.Rows[0]["WardNo"].ToString();
                        txtVillage.Text = dt.Rows[0]["Village"].ToString();
                        txtPanchayat.Text = dt.Rows[0]["Panchayat"].ToString();
                        txtMandal.Text = dt.Rows[0]["Mandal"].ToString();
                        txtEmailId.Text = dt.Rows[0]["Email"].ToString();
                        if (dt.Rows[0]["Photo"].ToString() != "")
                            GetImage((byte[])dt.Rows[0]["Photo"]);
                        //txtVoterId.Text = dt.Rows[0]["VoterID"].ToString();
                        txtActivity1.Text = dt.Rows[0]["Activity1"].ToString();
                        txtActivity2.Text = dt.Rows[0]["Activity2"].ToString();
                        txtActivity3.Text = dt.Rows[0]["Activity3"].ToString();
                        cmbCast.SelectedValue = dt.Rows[0]["TDP_CAST"].ToString();
                    }
                    else
                    {
                        flagUpdate = false;
                        txtfName.Text = "";
                        txtAge.Text = "";
                        txtMemberSince.Text = "";
                        txtBoothNo.Text = "";
                        txtMandal.Text = "";
                        txtMobileNo.Text = "";
                        txtPanchayat.Text = "";
                        txtVillage.Text = "";
                        txtVoterId.Text = "";
                        txtWardNo.Text = "";
                        lblPath.Text = "";
                        photoCapture.ImgWebCam.Image = null;
                        dtpDOB.Value = DateTime.Today;
                        txtActivity1.Text = "";
                        txtActivity2.Text = "";
                        txtActivity3.Text = "";
                        txtVilSearch.Text = "";
                        cmbCast.SelectedIndex = 0;
                        cmbPostInParty.SelectedIndex = 0;
                    }
                }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSqlDb = null;
            }
        }

        private void txtSurName_TextChanged(object sender, EventArgs e)
        {
            if (txtMemberName.Text.Length > 0 && txtSurName.Text.Length > 0)
            {
                GetMemberDetails(txtMemberName.Text, txtSurName.Text);
            }
        }
    }
}
