using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SSCRMDB;
using System.IO;

namespace SSCRM
{
    public partial class EmpWorkingStatusRollback : Form
    {
        public EmpWorkingStatusRollback()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEcode.Text = "";
            txtName.Text = "";
            txtFatherName.Text = "";
            txtDesg.Text = "";
            txtBranch.Text = "";
            txtDob.Text = "";
            txtDoj.Text = "";
            //int age = DateTime.Today.Year - Convert.ToDateTime(ds.Tables[0].Rows[0]["EMP_DOB"].ToString()).Year;
            //txtAge.Text = age.ToString();
            txtLeftDate.Text = "";
            txtLeftReason.Text = "";
            txtLeftUpdateDate.Text = "";
            txtLeftUpdateEcode.Text = "";
            txtName.Tag = "";
            txtLeftUpdateName.Text = "";
            txtStatus.Text = "";
            txtBranch.Tag = "";
            txtFatherName.Tag = "";
            txtDob.Tag = "";
            btnUpdate.Enabled = false;
            txtRollReason.Text = "";
            txtDoj.Tag = "";
            txtStatus.Tag = "";
            //pictureBox1.BackgroundImage = global::SDMS.Properties.Resources.nomale;
        }

        private void txtEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtEcode_KeyUp(object sender, KeyEventArgs e)
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
                //pictureBox1.BackgroundImage = newImage;
                //this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Length > 0)
            {
                if (CheckData())
                {

                        string strSQL = "";
                     string strDevice="";

                        if (txtDoj.Tag.ToString() == "HO")
                        {
                            strSQL = "SELECT ICLOCK_DEVICES_LIST.ICLOCK_DEVICE_SLNO FROM ICLOCK_DEVICES_LIST WHERE ICLOCK_DEVICES_LIST.BRANCH_CODE='SSBAPCHYD'";
                        }
                        else
                        {
                            strSQL = "SELECT ICLOCK_DEVICES_LIST.ICLOCK_DEVICE_SLNO FROM ICLOCK_DEVICES_LIST WHERE ICLOCK_DEVICES_LIST.BRANCH_CODE='" + txtStatus.Tag + "'";
                        }

                        SQLDB obj = new SQLDB();
                        DataTable dt = obj.ExecuteDataSet(strSQL).Tables[0];

                        if (dt.Rows.Count > 0)
                        {
                            strDevice = dt.Rows[0][0].ToString();
                        }



                        if (txtName.Tag.ToString().Length == 0 && txtDob.Tag=="A")
                        {
                            strSQL = " UPDATE HR_APPL_MASTER_HEAD SET HAMH_LEFT_DATE=NULL,HAMH_LEFT_REASON=NULL," +
                           "HAMH_LEFT_UPDATED_DATE=NULL,HAMH_LEFT_APPROVAL_ECODE=NULL,HAMH_WORKING_STATUS='P' WHERE HAMH_EORA_CODE='" + txtEcode.Text + "' AND HAMH_EORA_TYPE='"+txtDob.Tag+"'";//WORKING STATUS = P
                           // strSQL += " UPDATE Unmappeddata SET updt_flag='L-P',updated_by='" + CommonData.LogUserId + "',updated_date=GETDATE() WHERE ECODE=" + txtEcode.Text + "";
                        }
                        else
                        {
                            strSQL = " UPDATE HR_APPL_MASTER_HEAD SET HAMH_LEFT_DATE=NULL,HAMH_LEFT_REASON=NULL," +
                           "HAMH_LEFT_UPDATED_DATE=NULL,HAMH_LEFT_APPROVAL_ECODE=NULL,HAMH_WORKING_STATUS='W' WHERE HAMH_EORA_CODE='" + txtEcode.Text + "'";//WORKING STATUS = W

                           // strSQL += " UPDATE Unmappeddata SET updt_flag='L-W',updated_by='" + CommonData.LogUserId + "',updated_date=GETDATE() WHERE ECODE=" + txtEcode.Text + "";
                        }

                        strSQL += " UPDATE EORA_MASTER SET EORA = HAMH_EORA_TYPE FROM HR_APPL_MASTER_HEAD WHERE HAMH_EORA_CODE = ECODE AND HAMH_WORKING_STATUS IN ('W','P') AND ECODE=" + txtEcode.Text;
                          strSQL += " exec Amsbd_BioTransfer_InsDel_OD " + txtEcode.Text + ",'"+strDevice+"','INSERT' ";
                          strSQL += " exec Amsbd_BioTransfer_InsDel_OD " + txtEcode.Text + ",'" + strDevice + "','INSERT_OD_BRANCHES' ";
                        strSQL += " EXEC LeftRollBackUpdate " + txtEcode.Text + "," + txtFatherName.Tag + ",'" + CommonData.LogUserId +
                                                "','"+DateTime.Now+"','','"+txtRollReason.Text+"','ROLLBACK'";
                        //if (txtEcode.Text.Length == 5)
                        //{
                        //    strSQL += " UPDATE EORA_MASTER SET EORA_MASTER.EORA ='E' ";
                        //}
                        //else if (txtEcode.Text.Length == 6)
                        //{
                        //    strSQL += " UPDATE EORA_MASTER SET EORA_MASTER.EORA ='A' ";
                        //}


                        try
                        {
                            SQLDB objDB = new SQLDB();
                            int ires = objDB.ExecuteSaveData(strSQL);
                            if (ires > 0)
                            {
                                MessageBox.Show("Data Updated Successfully", "Rollback :: Left", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                btnClear_Click(null, null);
                                txtEcode.Focus();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                }
                else
                {
                    //MessageBox.Show("Enter Valid Ecode", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            txtEcode.Focus();
        }

        private bool CheckData()
        {
            int iRec = 1;
            string sMessage = string.Empty;
            bool bFlag = true;
            if (txtEcode.Text.Length < 5)
            {
                sMessage += iRec.ToString() + ". Invalid Employee Ecode \n";
                iRec++;
                //MessageBox.Show("Invalid Employee Status to RollBack", "Rollback :: Left", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //return false;
                bFlag = false;
            }
            if (txtStatus.Text != "LEFT")
            {
                sMessage += iRec.ToString() + ". Invalid Employee Status to RollBack \n";
                iRec++;
                //MessageBox.Show("Invalid Employee Status to RollBack", "Rollback :: Left", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //return false;
                bFlag = false;
            }
            //if (txtBranch.Tag.ToString().Length == 0)
            //{
            //    sMessage += iRec.ToString() + ". You Cannot Modify the data for this employee. Please contact IT if you still need to modify this person\n";
            //    iRec++;
            //    bFlag = false;
            //}
            if (bFlag == false)
            {
                MessageBox.Show(sMessage, "Rollback :: Left", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return bFlag;

            }

            else if (txtRollReason.Text.Length == 0)
            {
                sMessage += iRec.ToString() + ". Enter Reason \n";
                iRec++;

                MessageBox.Show(sMessage, "Rollback :: Left", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //MessageBox.Show("Invalid Employee Status to RollBack", "Rollback :: Left", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //return false;
                bFlag = false;
                return bFlag;
            }
            else if (txtRollReason.Text.Length < 4)
            {
                sMessage += iRec.ToString() + ". Enter Valid Reason \n";
                iRec++;
                MessageBox.Show(sMessage, "Rollback :: Left", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //MessageBox.Show("Invalid Employee Status to RollBack", "Rollback :: Left", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //return false;
                bFlag = false;
                return bFlag;
            }
            else
            {
                return true;
            }
            return bFlag;
            
            return bFlag;
        }

        private void txtEcode_TextChanged(object sender, EventArgs e)
        {
            if (txtEcode.Text.Length > 4)
            {
                txtEcode.Text = txtEcode.Text.Trim();
                SQLDB objSQLdb = new SQLDB();
                SqlParameter[] param = new SqlParameter[1];
                DataSet ds = new DataSet();
                try
                {
                    param[0] = objSQLdb.CreateParameter("@xEcode", DbType.String, txtEcode.Text, ParameterDirection.Input);
                    ds = objSQLdb.ExecuteDataSet("GetEmpDetailsForRollback", CommandType.StoredProcedure, param);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    param = null;
                    objSQLdb = null;
                }
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    txtName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                  txtFatherName.Text = ds.Tables[0].Rows[0]["FatherName"].ToString();
                    txtDesg.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                    txtBranch.Text = ds.Tables[0].Rows[0]["BRANCH_NAME"].ToString();
                    txtDob.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Dob"]).ToShortDateString();
                    txtDoj.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Doj"]).ToShortDateString();
                    //int age = DateTime.Today.Year - Convert.ToDateTime(ds.Tables[0].Rows[0]["EMP_DOB"].ToString()).Year;
                    //txtAge.Text = age.ToString();
                    txtLeftDate.Text = ds.Tables[0].Rows[0]["HAMH_LEFT_DATE"].ToString();
                    txtLeftReason.Text = ds.Tables[0].Rows[0]["HAMH_LEFT_REASON"].ToString();
                    txtLeftUpdateDate.Text = ds.Tables[0].Rows[0]["HAMH_LEFT_UPDATED_DATE"].ToString();
                    txtLeftUpdateEcode.Text = ds.Tables[0].Rows[0]["LeftUpdatedByCode"].ToString();
                    txtLeftUpdateName.Text = ds.Tables[0].Rows[0]["LeftUpdatedByName"].ToString();//HAMH_APPROVAL_DATE
                    txtName.Tag = ds.Tables[0].Rows[0]["HAMH_APPROVAL_DATE"].ToString();
                    txtBranch.Tag = ds.Tables[0].Rows[0]["unmappedDataBranch"].ToString();
                    txtStatus.Text = dt.Rows[0]["Status"].ToString();
                    txtFatherName.Tag = dt.Rows[0]["HAMH_APPL_NUMBER"].ToString();
                    txtDob.Tag = dt.Rows[0]["HAMH_EORA_TYPE"].ToString();
                    txtDoj.Tag = dt.Rows[0]["BRANCH_TYPE"].ToString();
                    txtStatus.Tag = dt.Rows[0]["BRANCH_CODE"].ToString();
                    //if (txtName.Text.Length > 0)
                    //{
                    //    if (dt.Rows[0]["Status"].ToString().Length == 0)
                    //    {
                    //        try
                    //        {
                    //            string sql = "UPDATE Unmappeddata SET updt_flag='MG',updated_by='" + CommonData.LogUserId + "',updated_date=GETDATE() WHERE ECODE=" + txtEcode.Text + "";
                    //            SQLDB OBJ = new SQLDB();
                    //            OBJ.ExecuteSaveData(sql);
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            MessageBox.Show(ex.ToString());
                    //        }
                    //        MessageBox.Show("Employee was Migrated/Rejoined", "Rollback :: Left", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    }
                    //    if (dt.Rows[0]["Status"].ToString() == "WORKING" && dt.Rows[0]["updt_flag"] == null)
                    //    {
                    //        try
                    //        {
                    //            try
                    //            {
                    //                txtLeftDate.Text = "";
                    //                txtLeftReason.Text = "";
                    //                txtLeftUpdateDate.Text = "";
                    //                txtLeftUpdateEcode.Text = "";
                    //                txtLeftUpdateName.Text = "";
                    //                string sql = "UPDATE Unmappeddata SET updt_flag='NF',updated_by='" + CommonData.LogUserId + "',updated_date=GETDATE() WHERE ECODE=" + txtEcode.Text + "";
                    //                SQLDB OBJ = new SQLDB();
                    //                OBJ.ExecuteSaveData(sql);
                    //            }
                    //            catch (Exception ex)
                    //            {
                    //                MessageBox.Show(ex.ToString());
                    //            }
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            MessageBox.Show(ex.ToString());
                    //        }
                    //    }
                    //    if (dt.Rows[0]["Status"].ToString() == "LEFT")
                    //    {
                    //        btnUpdate.Enabled = true;
                    //    }

                    //}
                    if (dt.Rows[0]["Status"].ToString() == "LEFT")
                    {
                        btnUpdate.Enabled = true;
                    }
                    else
                    {
                        btnUpdate.Enabled = false;
                    }
                }
                else
                {
                    txtName.Text = "";
                    txtFatherName.Text = "";
                    txtDesg.Text = "";
                    txtBranch.Text = "";
                    txtDob.Text = "";
                    txtDoj.Text = "";
                    //int age = DateTime.Today.Year - Convert.ToDateTime(ds.Tables[0].Rows[0]["EMP_DOB"].ToString()).Year;
                    //txtAge.Text = age.ToString();
                    txtLeftDate.Text = "";
                    txtLeftReason.Text = "";
                    txtLeftUpdateDate.Text = "";
                    txtLeftUpdateEcode.Text = "";
                    txtName.Tag = "";
                    txtLeftUpdateName.Text = "";
                    txtStatus.Text = "";
                    txtBranch.Tag = "";
                    btnUpdate.Enabled = false;
                    txtFatherName.Tag = "";
                    txtDob.Tag = "";
                    txtRollReason.Text = "";
                    txtDoj.Tag = "";
                    txtStatus.Tag = "";
                    //pictureBox1.BackgroundImage = global::SDMS.Properties.Resources.nomale;
                }
            }
            else
            {
                txtName.Text = "";
                txtFatherName.Text = "";
                txtDesg.Text = "";
                txtBranch.Text = "";
                txtDob.Text = "";
                txtDoj.Text = "";
                //int age = DateTime.Today.Year - Convert.ToDateTime(ds.Tables[0].Rows[0]["EMP_DOB"].ToString()).Year;
                //txtAge.Text = age.ToString();
                txtLeftDate.Text = "";
                txtLeftReason.Text = "";
                txtLeftUpdateDate.Text = "";
                txtLeftUpdateEcode.Text = "";
                txtName.Tag = "";
                txtLeftUpdateName.Text = "";
                txtStatus.Text = "";
                txtBranch.Tag = "";
                btnUpdate.Enabled = false;
                txtFatherName.Tag = "";
                txtDob.Tag = "";
                txtRollReason.Text = "";
                txtDoj.Tag = "";
                txtStatus.Tag = "";
                //pictureBox1.BackgroundImage = global::SDMS.Properties.Resources.nomale;
            }
        }

        private void EmpWorkingStatusRollback_Load(object sender, EventArgs e)
        {

        }

    }
}
