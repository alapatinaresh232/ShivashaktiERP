using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSCRM.App_Code;
using System.IO;
using SSTrans;

namespace SSCRM
{
    public partial class AdharMaster : Form
    {
        SQLDB objsql=null;
        HRInfo objHrinfo=null;

        public AdharMaster()
        {
            InitializeComponent();
        }

        private void AdharMaster_Load(object sender, EventArgs e)
        {

        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
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
                picEmpPhoto.BackgroundImage = newImage;
                this.picEmpPhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GetEmpDetails()
        {
        
            objsql = new SQLDB();
            objHrinfo = new HRInfo();
            DataSet dsPhoto = new DataSet();
            DataTable dt = new DataTable(); 
          
            try
            {


                dt = objHrinfo.Get_Adhar_MasterDetails(Convert.ToInt32(txtEcodeSearch.Text.ToString())).Tables[0];
                dsPhoto = objsql.ExecuteDataSet("SELECT HAPS_PHOTO_SIG FROM HR_APPL_PHOTO_SIG WHERE HAPS_EORA_CODE = " + txtEcodeSearch.Text);
                if (dt.Rows.Count > 0)
                {
                    txtApplNo.Text = dt.Rows[0]["AppNo"].ToString();
                    txtEName.Text = dt.Rows[0]["EmpName"].ToString();
                    txtFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                    txtDob.Text = Convert.ToDateTime(dt.Rows[0]["EmpDob"].ToString()).ToShortDateString();
                    txtDoj.Text = Convert.ToDateTime(dt.Rows[0]["EmpDoj"].ToString()).ToShortDateString();
                    if (dt.Rows[0]["Status"].Equals("W"))
                    {
                        txtStatus.Text = "WORKING";
                    }
                    if (dt.Rows[0]["Status"].Equals("P"))
                    {
                        txtStatus.Text = "PENDING";
                    }
                    if (dt.Rows[0]["Status"].Equals("L"))
                    {
                        txtStatus.Text = "LEFT";
                    }
                 
                    txtDept.Text = dt.Rows[0]["EmpDept"].ToString();
                    txtDesig.Text = dt.Rows[0]["EmpDesig"].ToString();
                    txtadharNo.Text = dt.Rows[0]["AdharNo"].ToString();
                    txtPanNo.Text = dt.Rows[0]["PancardNo"].ToString();
                    txtPassportNo.Text = dt.Rows[0]["PassportNo"].ToString();
                    txtDrivingLinceNo.Text = dt.Rows[0]["DrivingLinceNO"].ToString();
                    if (dsPhoto.Tables[0].Rows.Count > 0)
                        GetImage((byte[])dsPhoto.Tables[0].Rows[0]["HAPS_PHOTO_SIG"]);
                    else
                        picEmpPhoto.BackgroundImage = null;

                }
                else
                {
                    txtEName.Text = "";
                    txtFatherName.Text = "";
                    txtDept.Text = "";
                    txtDesig.Text = "";
                    txtDob.Text = "";
                    txtDoj.Text = "";
                    txtStatus.Text = "";
                    txtPassportNo.Text = "";
                    txtDrivingLinceNo.Text = "";
                    txtadharNo.Text = "";
                    txtApplNo.Text = "";
                    txtPanNo.Text = "";

                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objsql = null;
                dt = null;

            }
         }
     

        private void btnSave_Click(object sender, EventArgs e)
        {
            int ival = 0;
            objsql = new SQLDB();
            DataTable dt = new DataTable();
             string StrCommand="";


             if (txtEcodeSearch.Text != "" || txtEName.Text!="")
             {
                 StrCommand = " UPDATE HR_APPL_MASTER_HEAD SET HAMH_VD_DL_NUMBER='" + txtDrivingLinceNo.Text.ToString().Replace(" ", "").ToUpper() +
                                "',HAMH_VD_PAN_CARD_NUMBER='" + txtPanNo.Text.ToString().Replace(" ", "").ToUpper() +
                                "',HAMH_VD_PASSPORT_NUMBER='" + txtPassportNo.Text.ToString().Replace(" ", "").ToUpper() +
                                "',HAMH_ADHAR_NO='" + txtadharNo.Text.ToString().Replace(" ", "").ToUpper() +
                                "',HAMH_MODIFIED_BY='" + CommonData.LogUserId +
                                "',HAMH_MODIFIED_DATE=getdate()" +
                                " WHERE HAMH_EORA_CODE =" + Convert.ToInt32(txtEcodeSearch.Text.ToString()) + "";
                 try
                 {
                     ival = objsql.ExecuteSaveData(StrCommand);
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.ToString());
                 }
                 finally
                 {
                     objsql = null;
                     dt = null;
                 }
                 if (ival > 0)
                 {

                     MessageBox.Show("Data Saved Sucessfully", "Audit Details", MessageBoxButtons.OK, MessageBoxIcon.Information);

                     btnClear_Click(null, null);                 
                     
                 }
                 else
                 {
                     MessageBox.Show(" Data Not Saved", "Adhar  Details", MessageBoxButtons.OK, MessageBoxIcon.Error);

                 }
             }
             else
             {
                 MessageBox.Show("Please Enter Ecode", "Adhar Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
             
        }

        private void btnClear_Click(object sender, EventArgs e)
        {   
            txtEcodeSearch.Text="";
            txtEName.Text = "";
            txtFatherName.Text="";
            txtDept.Text="";
            txtDesig.Text = "";
            txtDob.Text="";
            txtDoj.Text="";
            txtStatus.Text="";        
            txtDrivingLinceNo.Text="";
            txtPassportNo.Text="";
            txtadharNo.Text="";
            txtApplNo.Text="";
            txtPanNo.Text = "";
            picEmpPhoto.BackgroundImage = null;
            
          }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            txtEName.Text = "";
            txtFatherName.Text = "";
            txtDept.Text = "";
            txtDesig.Text = "";
            txtDob.Text = "";
            txtDoj.Text = "";
            txtStatus.Text = "";
            txtPanNo.Text = "";
            txtDrivingLinceNo.Text = "";
            txtPassportNo.Text = "";
            txtadharNo.Text = "";
            txtApplNo.Text = "";
            picEmpPhoto.BackgroundImage = null;
        }

        private void txtEcodeSearch_Validated(object sender, EventArgs e)
        {
            
           
        }

        private void txtPanNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
            e.KeyChar = Char.ToUpper(e.KeyChar);

            //if (char.IsLetter(e.KeyChar) == false)
            //    e.Handled = true;
            //e.KeyChar = Char.ToUpper(e.KeyChar);
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsLetterOrDigit((e.KeyChar)))
                {
                    e.KeyChar = Char.ToUpper(e.KeyChar);
                    e.Handled = true;
                }
            }
        }

        private void txtPassportNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
            e.Handled = (e.KeyChar == (char)Keys.Space);
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsLetterOrDigit((e.KeyChar)))
                {
                    e.KeyChar = Char.ToUpper(e.KeyChar);
                    e.Handled = true;
                }
            }
        }

        private void txtadharNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
            e.Handled = (e.KeyChar == (char)Keys.Space);
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsLetterOrDigit((e.KeyChar)))
                {
                    e.KeyChar = Char.ToUpper(e.KeyChar);
                    e.Handled = true;
                }
            }
        }

        private void txtDrivingLinceNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
            e.Handled = (e.KeyChar == (char)Keys.Space);
            if ((e.KeyChar != '\b') && (e.KeyChar != ' '))
            {
                if (!char.IsLetterOrDigit((e.KeyChar)))
                {
                    e.KeyChar = Char.ToUpper(e.KeyChar);
                    e.Handled = true;
                }
            }
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.Length > 4)
            {
                GetEmpDetails();

            }
        }
     
    }
}
