using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;
using SSCRMDB;
using SSTrans;
namespace SSCRM
{
    public partial class LeaveEntryForm : Form
    {
        delegate void SetComboBoxCellType(int iRowIndex);
        private SQLDB objData = null;
        private bool flagUpdate = false,updateLeaveMaster=false;
        private double cl = 0, balanceCl = 0,availedCl=0, el = 0, balanceEl = 0,availedEl=0, sl = 0, balanceSl = 0,availedSl=0;
        private string sLocType = "";

        public LeaveEntryForm()
        {
            InitializeComponent();
        }

        public LeaveEntryForm(string sFrmType)
        {
            InitializeComponent();
            sLocType = sFrmType;
        }

        private void LeaveEntryForm_Load(object sender, EventArgs e)
        {

            GeneratingApplNo();
            lblChecking.Visible = false;

            gvLeaveDetl.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                       System.Drawing.FontStyle.Regular);
            dtpToDate.Value = DateTime.Today;
            cbTo.SelectedIndex = 1;
            cbFrom.SelectedIndex = 0;
            txtEcodeSearch.Focus();

            if (CommonData.LogUserId.ToUpper() == "ADMIN" || CommonData.LogUserId=="11379")
            {
                btnDelete.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;
            }

            dtAppDate.Value = DateTime.Today;
            dtFromDate.Value = DateTime.Today;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void GeneratingApplNo()
        {
            objData = new SQLDB();
            int iMax = 0;
            String strCmd="";
            try
            {
                if (sLocType == "HO")
                {
                    strCmd = "SELECT ISNULL(MAX(HELT_APPL_NUMBER),0)+1 TRNNO FROM HR_EMPLOYEE_LEAVE_TRN WHERE isnull(HELT_LOC_TYPE,'HO')='" + sLocType + "'";
                }
                else
                {
                    strCmd = "SELECT ISNULL(MAX(HELT_APPL_NUMBER),0)+1 TRNNO FROM HR_EMPLOYEE_LEAVE_TRN WHERE isnull(HELT_LOC_TYPE,'HO')='" + sLocType +
                             "' AND HELT_BRANCH_CODE='"+ CommonData.BranchCode +"' ";
                }


                iMax = Convert.ToInt32(objData.ExecuteDataSet(strCmd).Tables[0].Rows[0][0].ToString()); ;
                if (iMax > 0)
                {
                    txtApplNo.Text = iMax.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
            }
        }
        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            if (txtEcodeSearch.Text.Length > 0)
            {
                try
                {
                    if (sLocType == "HO")
                    {
                        String strCmd = "SELECT EM.BRANCH_CODE,EM.MEMBER_NAME,EM.DESIG,EM.COMPANY_CODE,"+
                                        "CM_COMPANY_NAME,BM.BRANCH_NAME,dept_name,"+
	         "(isnull(HAMH_ADD_PRES_ADDR_HNO,'')+','+isnull( HAMH_ADD_PRES_ADDR_LANDMARK,'')+','+isnull(HAMH_ADD_PRES_ADDR_VILL_OR_TOWN,'')"+
	        "','+isnull(HAMH_ADD_PRES_ADDR_MANDAL,'')+','+isnull(HAMH_ADD_PRES_ADDR_DISTRICT,'')+','+isnull(HAMH_ADD_PRES_ADDR_STATE,'')"+
	        "','+CAST(HAMH_ADD_PRES_ADDR_PIN as VARCHAR(10)))address,isnull(HECD_EMP_MOBILE_NO,HAMH_ADD_PRES_ADDR_PHONE),HECD_EMP_EMAIL_ID,HELC_BALANCE_LEAVES,HELC_LEAVE_TYPE FROM EORA_MASTER EM "+
						                "LEFT JOIN HR_APPL_MASTER_HEAD ON hamh_eora_code = em.ECODE"+
                                         "left join COMPANY_MAS ON CM_COMPANY_CODE=COMPANY_CODE left join "+
                                        "BRANCH_MAS BM ON BM.BRANCH_CODE=EM.BRANCH_CODE INNER JOIN Dept_Mas "+
                                        "ON dept_code=DEPT_ID left join HR_EMP_CONTACT_DETL ON ECODE=HECD_EORA_CODE "+
                                         "LEFT JOIN HR_EMPLOYEE_LEAVE_CREDITS ON ECODE=HELC_EORA_CODE "+
                                         "WHERE ecode=40012 order by helc_leave_year desc ";

                        dt = objData.ExecuteDataSet(strCmd).Tables[0];
                    }
                    else
                    {
                        objData = new SQLDB();
                        SqlParameter[] param = new SqlParameter[3];
                        DataSet ds = new DataSet();
                        try
                        {

                            param[0] = objData.CreateParameter("@xEcode", DbType.String, txtEcodeSearch.Text, ParameterDirection.Input);
                            param[1] = objData.CreateParameter("@xUserBranches", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                            param[2] = objData.CreateParameter("@xFinYear", DbType.String, dtFromDate.Value.Year, ParameterDirection.Input);

                            ds = objData.ExecuteDataSet("GetEmpDetails", CommandType.StoredProcedure, param);
                            dt = ds.Tables[0];

                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.ToString());
                        }
                        finally
                        {
                            param = null;
                            objData = null;
                        }
                    }
                   
                    if (dt.Rows.Count > 0)
                    {
                        txtName.Text = dt.Rows[0]["MEMBER_NAME"] + "";
                        txtCompany.Text = dt.Rows[0]["CM_COMPANY_NAME"] + "";
                        txtCompany.Tag = dt.Rows[0]["COMPANY_CODE"] + "";
                        txtBranch.Text = dt.Rows[0]["BRANCH_NAME"] + "";
                        txtBranch.Tag = dt.Rows[0]["BRANCH_CODE"] + "";
                        txtDept.Text = dt.Rows[0]["dept_name"] + "";
                        txtDesig.Text = dt.Rows[0]["DESIG"] + "";
                        txtAddress.Text = dt.Rows[0]["address"] + "";
                        txtPhNo.Text = dt.Rows[0]["HECD_EMP_MOBILE_NO"] + "";
                        txtEmail.Text = dt.Rows[0]["HECD_EMP_EMAIL_ID"] + "";

                        if (dt.Rows[0]["HELC_LEAVE_TYPE"].ToString() != "")
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (dt.Rows[i]["HELC_LEAVE_TYPE"].ToString() == "CL")
                                    txtCLS.Text = dt.Rows[i]["HELC_BALANCE_LEAVES"] + "";
                                if (dt.Rows[i]["HELC_LEAVE_TYPE"].ToString() == "EL")
                                    txtELS.Text = dt.Rows[i]["HELC_BALANCE_LEAVES"] + "";
                                if (dt.Rows[i]["HELC_LEAVE_TYPE"].ToString() == "SL")
                                    txtSLS.Text = dt.Rows[i]["HELC_BALANCE_LEAVES"] + "";
                            }
                        }
                    }
                    else
                    {
                        txtName.Text = string.Empty;
                        txtCompany.Text = string.Empty;
                        txtBranch.Text = string.Empty;
                        txtDept.Text = string.Empty;
                        txtDesig.Text = string.Empty;
                        txtAddress.Text = string.Empty;
                        txtPhNo.Text = string.Empty;
                        txtEmail.Text = string.Empty;
                        txtCLS.Text = "";
                        txtELS.Text = "";
                        txtSLS.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    dt = null;
                }
            }
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
        private void txtPhNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                if (CheckEmployeeIsOnDuty() == true)
                {
                        if (CheckingForLeaves() == true)
                        {
                            try
                            {
                                if (SaveLeavesEntryHead() > 0)
                                {
                                    if (SaveLeavesEntryDetail() > 0)
                                    {
                                        flagUpdate = false;
                                        MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        btnCancel_Click(null, null);
                                        //gvLeaveDetl.Rows.Clear();
                                        GeneratingApplNo();
                                    }
                                }
                            }
                            catch(Exception ex)
                            {
                                flagUpdate = false;
                                MessageBox.Show("Data Not Saved", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnCancel_Click(null, null);
                                GeneratingApplNo();
                            }

                        }
                   
                }

                else
                {
                    dtpToDate.Value = DateTime.Today;
                    cbTo.SelectedIndex = 1;
                    cbFrom.SelectedIndex = 0;
                    dtFromDate.Value = DateTime.Today;
                }
            }
        }
        private int SaveLeavesEntryHead()
        {
            objData = new SQLDB();
            int result = 0;
            try
            {
                string strCommand = "", sqlText = "";
                if (flagUpdate == true)
                {
                    HRInfo objHr = new HRInfo();

                    objHr.UpdatingLeavesCreditAgainstLeaveUpdate(Convert.ToInt32(txtEcodeSearch.Text), Convert.ToInt32(txtApplNo.Text),dtFromDate.Value.Year);
                    if (sLocType == "HO")
                    {
                        strCommand = " DELETE FROM HR_EMPLOYEE_LEAVE_TRN_LNITEM WHERE HELTL_COMPANY_CODE='" + txtCompany.Tag + "' AND HELTL_BRANCH_CODE='" + txtBranch.Tag
                                  + "' AND HELTL_APPL_NUMBER=" + Convert.ToInt32(txtApplNo.Text) + " AND isnull(HELT_LOC_TYPE,'HO')='" + sLocType + "' ";

                        strCommand += " UPDATE HR_EMPLOYEE_LEAVE_TRN SET HELT_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) + "," +
                                " HELT_COMPANY_CODE='" + txtCompany.Tag + "',HELT_BRANCH_CODE='" + txtBranch.Tag + "'" +
                                ",HELT_LEAVE_FROM_DATE='" + Convert.ToDateTime(dtFromDate.Value).ToString("dd/MMM/yyyy") +
                                "',HELT_LEAVE_FROM_NOON='" + cbFrom.Text + "',HELT_LEAVE_TO_DATE='" + Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy") +
                                "',HELT_LEAVE_TO_NOON='" + cbTo.Text + "',HELT_NOOF_LEAVE_DAYS=" + Convert.ToDouble(txtNoOfDays.Text) +
                                ",HELT_LEAVE_REASON='" + txtReason.Text.Trim() + "',HELT_ADDR_LEAVE_PERIOD='" + txtAddress.Text +
                                "',HELT_PHONE_LEAVE_PERIOD='" + txtPhNo.Text + "',HELT_EMAIL_LEAVE_PERIOD='" + txtEmail.Text +
                                "',HELT_LEAVE_APPROVED_BY_ECODE=" + Convert.ToInt32(txtEcodeApprovBy.Text) +
                                ",HELT_MODIFIED_BY='" + CommonData.LogUserId +
                                "',HELT_MODIFIED_DATE=GETDATE() "+
                                " WHERE HELT_APPL_NUMBER=" + Convert.ToInt32(txtApplNo.Text) + 
                                 " AND isnull(HELT_LOC_TYPE,'HO')='" + sLocType + "'";
                    }
                    else
                    {
                        strCommand = " DELETE FROM HR_EMPLOYEE_LEAVE_TRN_LNITEM WHERE HELTL_COMPANY_CODE='" + CommonData.CompanyCode + 
                                  "' AND HELTL_BRANCH_CODE='" + CommonData.BranchCode +
                                  "' AND HELTL_APPL_NUMBER=" + Convert.ToInt32(txtApplNo.Text) + 
                                  " AND isnull(HELTL_LOC_TYPE,'HO')='" + sLocType + "' ";

                        strCommand += " UPDATE HR_EMPLOYEE_LEAVE_TRN SET HELT_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) + "," +
                                " HELT_COMPANY_CODE='" + txtCompany.Tag + "',HELT_BRANCH_CODE='" + txtBranch.Tag + "'" +
                                ",HELT_LEAVE_FROM_DATE='" + Convert.ToDateTime(dtFromDate.Value).ToString("dd/MMM/yyyy") +
                                "',HELT_LEAVE_FROM_NOON='" + cbFrom.Text + "',HELT_LEAVE_TO_DATE='" + Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy") +
                                "',HELT_LEAVE_TO_NOON='" + cbTo.Text + "',HELT_NOOF_LEAVE_DAYS=" + Convert.ToDouble(txtNoOfDays.Text) +
                                ",HELT_LEAVE_REASON='" + txtReason.Text.Trim() + "',HELT_ADDR_LEAVE_PERIOD='" + txtAddress.Text +
                                "',HELT_PHONE_LEAVE_PERIOD='" + txtPhNo.Text + "',HELT_EMAIL_LEAVE_PERIOD='" + txtEmail.Text +
                                "',HELT_LEAVE_APPROVED_BY_ECODE=" + Convert.ToInt32(txtEcodeApprovBy.Text) +
                                ",HELT_MODIFIED_BY='" + CommonData.LogUserId +
                                "',HELT_MODIFIED_DATE= GETDATE() "+
                                " WHERE  HELT_BRANCH_CODE='" + CommonData.BranchCode +
                                "' AND HELT_APPL_NUMBER=" + Convert.ToInt32(txtApplNo.Text) + 
                                " AND isnull(HELT_LOC_TYPE,'HO')='" + sLocType + "'";
                    }
            
                }
                else
                {
                    
                    GeneratingApplNo();
                    objData = new SQLDB();

                    strCommand = "INSERT INTO HR_EMPLOYEE_LEAVE_TRN (" +
                                   "HELT_COMPANY_CODE," +
                                   "HELT_BRANCH_CODE," +
                                   "HELT_APPL_NUMBER," +
                                   "HELT_EORA_CODE," +
                                  // "HELT_LEAVE_TYPE," +
                                   "HELT_LEAVE_FROM_DATE," +
                                   "HELT_LEAVE_FROM_NOON," +
                                   "HELT_LEAVE_TO_DATE," +
                                   "HELT_LEAVE_TO_NOON," +
                                   "HELT_NOOF_LEAVE_DAYS," +
                                   "HELT_LEAVE_REASON," +
                                   "HELT_ADDR_LEAVE_PERIOD," +
                                   "HELT_PHONE_LEAVE_PERIOD," +
                                   "HELT_EMAIL_LEAVE_PERIOD," +
                                   "HELT_LEAVE_APPROVED_BY_ECODE," +
                                   "HELT_CREATED_BY," +
                                   "HELT_CREATED_DATE, "+
                                   "HELT_LOC_TYPE)" +
                           "VALUES('" + txtCompany.Tag + "','" + txtBranch.Tag + "'," + Convert.ToInt32(txtApplNo.Text)
                           + "," + Convert.ToInt32(txtEcodeSearch.Text) + ""+
                           //"''"+
                           ",'" + Convert.ToDateTime(dtFromDate.Value).ToString("dd/MMM/yyyy")
                           + "','" + cbFrom.Text + "','" + Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy") + "','" + cbTo.Text + 
                           "'," + Convert.ToDouble(txtNoOfDays.Text)
                           + ",'" + txtReason.Text.Trim() + "','" + txtAddress.Text + "','" + txtPhNo.Text + "','" + txtEmail.Text + 
                           "','" + Convert.ToInt32(txtEcodeApprovBy.Text) +
                           "','" + CommonData.LogUserId +
                           "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + "','"+ sLocType +"')";
                 }
               
                if (strCommand.Length > 10)
                {
                    result = objData.ExecuteSaveData(strCommand);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
            }
            return result;
        }
        private int SaveLeavesEntryDetail()
        {
            objData = new SQLDB();
            string strSqlCoff = "";
            int result = 0;
            try
            {
               
                string sqlText = "";
                for (int iVar = 0; iVar < gvLeaveDetl.Rows.Count; iVar++)
                {
                    String strDate = Convert.ToDateTime(gvLeaveDetl.Rows[iVar].Cells["Date"].Value).ToString("dd/MMM/yyyy");
                    String strLeaveType = gvLeaveDetl.Rows[iVar].Cells["LeaveType"].Value.ToString();


                    sqlText += " INSERT INTO HR_EMPLOYEE_LEAVE_TRN_LNITEM (" +
                                       "HELTL_COMPANY_CODE," +
                                       "HELTL_BRANCH_CODE," +
                                       "HELTL_APPL_NUMBER," +
                                       "HELTL_SNO," +
                                       "HELTL_EORA_CODE," +
                                       "HELTL_LEAVE_TYPE," +
                                       "HELTL_LEAVE_DATE," +
                                       "HELTL_LEAVE_NOON," +
                                       "HELTL_LEAVE_DAYS," +
                                       "HELTL_COFF_AGNST_DATE," +
                                       "HELTL_LEAVE_REMARKS," +
                                       "HELTL_CREATED_BY," +
                                       "HELTL_CREATED_DATE, "+
                                       "HELTL_LOC_TYPE)" +
                                               "VALUES('" + txtCompany.Tag + "','" + txtBranch.Tag + "'," + Convert.ToInt32(txtApplNo.Text)
                                               + "," + Convert.ToInt32(gvLeaveDetl.Rows[iVar].Cells["SLNO"].Value) + "," + Convert.ToInt32(txtEcodeSearch.Text)
                                               + ",'" + strLeaveType
                                               + "','" + strDate
                                               + "','" + gvLeaveDetl.Rows[iVar].Cells["LeaveNoon"].Value + "','" + Convert.ToDouble(gvLeaveDetl.Rows[iVar].Cells["LeaveDays"].Value);
                                            if (strLeaveType == "COFF")
                                            {
                                                sqlText += "','" + Convert.ToDateTime(gvLeaveDetl.Rows[iVar].Cells["CoffAgntDate"].Value).ToString("dd/MMM/yyyy");
                                            }
                                            else
                                            {
                                                sqlText+="','";
                                            }
                                      sqlText += "','" + gvLeaveDetl.Rows[iVar].Cells["Remarks"].Value.ToString().Trim()
                                               + "','" + CommonData.LogUserId + "',getdate(),'"+ sLocType +"')";

                    //if (strLeaveType == "COFF")
                    //{
                    //    string strCoffDate = Convert.ToDateTime(gvLeaveDetl.Rows[iVar].Cells["CoffAgntDate"].Value).ToString("dd/MMM/yyyy");
                    //    strSqlCoff += " UPDATE HR_EMPLOYEE_COFF_TRN SET hect_coff_valid_flag='A' WHERE hect_eora_code=" + txtEcodeSearch.Text + " AND hect_coff_from_date ='" + strCoffDate + "' ";
                    //}

                }
                objData = new SQLDB();
                result = objData.ExecuteSaveData(sqlText);
                if (result > 0)
                {
                    sqlText = "";
                    if(flagUpdate==true)
                    {
                        //string strSQL = "SELECT heltl_leave_type,heltl_leave_days,heltl_leave_date from HR_EMPLOYEE_LEAVE_TRN_LNITEM WHERE  heltl_eora_code="+txtEcodeSearch.Text+"AND heltl_appl_number ="+txtApplNo.Text;

                        //DataTable dt = objData.ExecuteDataSet(strSQL).Tables[0];

                    }
                    if (updateLeaveMaster)
                    {
                        //availedCl += cl;
                        //balanceCl -= cl;

                        //availedEl += el;
                        //balanceEl -= el;

                        //availedSl += sl;
                        //balanceSl -= sl;
                        //string strYear = DateTime.Now.Year.ToString();

                        //sqlText += " update HR_EMPLOYEE_LEAVE_CREDITS set HELC_AVAILED_LEAVES=" + availedCl + ",HELC_BALANCE_LEAVES=" + balanceCl + " where HELC_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) + " and HELC_LEAVE_TYPE='CL' ";
                        //sqlText += " update HR_EMPLOYEE_LEAVE_CREDITS set HELC_AVAILED_LEAVES=" + availedEl + ",HELC_BALANCE_LEAVES=" + balanceEl + " where HELC_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) + " and HELC_LEAVE_TYPE='EL' ";
                        //sqlText += " update HR_EMPLOYEE_LEAVE_CREDITS set HELC_AVAILED_LEAVES=" + availedSl + ",HELC_BALANCE_LEAVES=" + balanceSl + " where HELC_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) + " and HELC_LEAVE_TYPE='SL' ";

                        //sqlText += strSqlCoff;
                        //int iRes = objData.ExecuteSaveData(sqlText);

                       



                    }
                    HRInfo objHr = new HRInfo();
                    objHr.UpdatingLeavesCredit(Convert.ToInt32(txtEcodeSearch.Text), Convert.ToInt32(txtApplNo.Text), dtFromDate.Value.Year);
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;

            }
            return result;
        }
        private bool CheckData()
        {
            bool flag = true;
                if (txtName.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Enter Valid Ecode", "Leave Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEcodeSearch.Focus();
                }
                else if (txtReason.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Enter Reason For Leave", "Leave Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtReason.Focus();
                }
                else if (txtAddress.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Enter Address", "Leave Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAddress.Focus();
                }
                else if (txtPhNo.Text.Length != 10)
                {
                    flag = false;
                    MessageBox.Show("Enter Valid Phone No", "Leave Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPhNo.Focus();
                }
                else if (!Regex.IsMatch(txtEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
                {
                    flag = false;
                    MessageBox.Show("Enter Valid Email", "Leave Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Focus();
                }
                else if (Convert.ToDouble(txtNoOfDays.Text) < .5)
                {
                    //flag = false;
                    //MessageBox.Show("Enter Valid Dates", "Leaves Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtNameApprovBy.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Enter Valid Approved Ecode ", "Leave Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEcodeApprovBy.Focus();
                }
                else if (txtEcodeApprovBy.Text.Equals(txtEcodeSearch.Text))
                {
                    flag = false;
                    MessageBox.Show("Ecode and Approved Ecode Should not be Same!", "Leaves Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNameApprovBy.Text = "";
                    txtEcodeApprovBy.Text = "";
                    txtEcodeApprovBy.Focus();
                }

                //else if (dtFromDate.Value <= DateTime.Now.AddDays(-5))
                //{
                //    flag = false;
                //    MessageBox.Show("Back Data Cannot Enter", "Leaves Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);


                //}
                //else if ((dtFromDate.Value - DateTime.Now).TotalDays < -10)
                //{
                //    flag = false;
                //    MessageBox.Show("Minimum 10 days Back Data Can Enter", "Leaves Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);


                //}
                else if (CheckingRecord() == false)
                {
                    flag = false;
                    MessageBox.Show("This Record already Exists","Leave Entry Form",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return flag;
                }
                //else if (CheckingForCOff() == false)
                //{
                //    flag = false;
                //    return flag;
                //}

                else
                {
                    for (int iVar = 0; iVar < gvLeaveDetl.Rows.Count; iVar++)
                    {
                        string strSelectdValue = gvLeaveDetl.Rows[iVar].Cells["Desc"].Value.ToString();
                        if (strSelectdValue == "")
                        {
                            flag = false;
                            MessageBox.Show("Please Select the Leave Type", "Leave Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gvLeaveDetl.Focus();
                            return flag;
                        }

                    }
                }
             

            return flag;
        }
        private bool CheckingForLeaves()
        {
            bool flag = true;
            cl = 0; el = 0; sl = 0;
            DataTable dt=null;
            for (int iVar = 0; iVar < gvLeaveDetl.Rows.Count;iVar++ )
            {
                string strSelectdValue = gvLeaveDetl.Rows[iVar].Cells["Desc"].Value + "";

                if (strSelectdValue == "CASUAL LEAVE")
                    cl += Convert.ToDouble(gvLeaveDetl.Rows[iVar].Cells["LeaveDays"].Value);

                else if (strSelectdValue == "EARNED LEAVE")
                    el += Convert.ToDouble(gvLeaveDetl.Rows[iVar].Cells["LeaveDays"].Value);

                else if (strSelectdValue == "SICK LEAVE")
                    sl += Convert.ToDouble(gvLeaveDetl.Rows[iVar].Cells["LeaveDays"].Value);
            }
            String strCmd = "select HELC_LEAVE_TYPE,HELC_AVAILED_LEAVES,HELC_BALANCE_LEAVES "+
                            " from HR_EMPLOYEE_LEAVE_CREDITS where HELC_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) + 
                            " and HELC_LEAVE_YEAR="+dtFromDate.Value.Year;
            try
            {
                objData = new SQLDB();
                dt=objData.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    updateLeaveMaster = true;
                    for (int iVar = 0; iVar < dt.Rows.Count; iVar++)
                    {
                        String strLevType = dt.Rows[iVar]["HELC_LEAVE_TYPE"].ToString();
                        if (strLevType == "CL")
                        {
                            balanceCl = Convert.ToDouble(dt.Rows[iVar]["HELC_BALANCE_LEAVES"]);
                            availedCl = Convert.ToDouble(dt.Rows[iVar]["HELC_AVAILED_LEAVES"]);
                        }
                        else if (strLevType == "EL")
                        {
                            balanceEl = Convert.ToDouble(dt.Rows[iVar]["HELC_BALANCE_LEAVES"]);
                            availedEl = Convert.ToDouble(dt.Rows[iVar]["HELC_AVAILED_LEAVES"]);
                        }
                        else if (strLevType == "SL")
                        {
                            balanceSl = Convert.ToDouble(dt.Rows[iVar]["HELC_BALANCE_LEAVES"]);
                            availedSl = Convert.ToDouble(dt.Rows[iVar]["HELC_AVAILED_LEAVES"]);
                        }
                       
                    }
                    if (cl > balanceCl)
                    {
                        MessageBox.Show(txtName.Text + " have only " + balanceCl + " Casual Leaves");
                        flag = false;
                        return flag;
                    }
                    else if (el > balanceEl)
                    {
                        MessageBox.Show(txtName.Text + " have only " + balanceEl + " Earned Leaves");
                        flag = false;
                        return flag;
                    }
                    else if (sl > balanceSl)
                    {
                        MessageBox.Show(txtName.Text + " have only " + balanceSl + " Sick Leaves");
                        flag = false;
                        return flag;
                    }
                    else if (balanceCl == 0 && balanceEl == 0 && balanceSl == 0)
                    {
                        updateLeaveMaster = false;
                    }
                    
                                        
                }
                else if (balanceCl == 0 && balanceEl == 0 && balanceSl == 0)
                
                {
                        for (int i = 0; i < gvLeaveDetl.Rows.Count; i++)
                        {

                            if (gvLeaveDetl.Rows[i].Cells[4].Value.Equals("LOSS OF PAY") || gvLeaveDetl.Rows[i].Cells[4].Value.Equals("COMPENSATORY OFF"))
                            {
                                updateLeaveMaster = false;
                            }
                            
                            else
                            {
                                MessageBox.Show(txtName.Text + " have No Leaves");
                                flag = false;
                                return flag;
                            }
                            
                        }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
                dt = null;
            }
            return flag;
        }

        private bool CheckingRecord()
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            bool bFlag = true;
            try
            {
                for (int j = 0; j < gvLeaveDetl.Rows.Count; j++)
                {

                    string strCmd = " SELECT HELTL_EORA_CODE, HELTL_LEAVE_DATE FROM HR_EMPLOYEE_LEAVE_TRN_LNITEM WHERE HELTL_EORA_CODE=" + txtEcodeSearch.Text + " AND HELTL_LEAVE_DATE='" + Convert.ToDateTime(gvLeaveDetl.Rows[j].Cells["Date"].Value).ToString("yyyy/MM/dd") + "' AND HELTL_LEAVE_TYPE='" + gvLeaveDetl.Rows[j].Cells["LeevType"].Value + "'";
                    dt = objData.ExecuteDataSet(strCmd).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        bFlag = false;
                        return bFlag;
                    }
                }
                
               
               
            }
           
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
                dt = null;
            }
            return bFlag;
        }

        private bool CheckEmployeeIsOnDuty()
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            bool flag = true;
            
            try
            {
                string strCmd = "SELECT HEOT_EORA_CODE,HEOT_OD_FROM_DATE,HEOT_OD_TO_DATE FROM HR_EMPLOYEE_OD_TRN WHERE HEOT_EORA_CODE="+ txtEcodeSearch.Text +" ";
                dt=objData.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                                                      
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DateTime fromDate = Convert.ToDateTime(dt.Rows[i]["HEOT_OD_FROM_DATE"].ToString());
                        DateTime toDate = Convert.ToDateTime(dt.Rows[i]["HEOT_OD_TO_DATE"].ToString());
                       
                            if ((dtFromDate.Value <= fromDate && dtpToDate.Value >= toDate) || (dtFromDate.Value >= (toDate) && dtpToDate.Value <= (toDate)))
                            {
                                flag = false;
                                MessageBox.Show("Employee applied for OnDuty On these days","Leave Application Entry Form",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                return flag;
                            }
                            else if ((dtFromDate.Value >= (fromDate) && dtpToDate.Value <= (toDate)) || (dtFromDate.Value >= (fromDate) && dtpToDate.Value <= (fromDate)))
                            {
                                flag = false;
                                MessageBox.Show("Employee applied for OnDuty On these days","Leave Application Entry form",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                return flag;
                            }
                    
                    }
                    
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                objData = null;
                dt = null;
            }
            return flag;
        }

        private bool CheckingForCOff()
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            bool flag = true;
           
            try
            {
                string strCmd = " SELECT HECT_EORA_CODE,HECT_COFF_FROM_DATE FROM HR_EMPLOYEE_COFF_TRN WHERE HECT_EORA_CODE="+ txtEcodeSearch.Text +"  ";
                dt = objData.ExecuteDataSet(strCmd).Tables[0];
                for (int j = 0; j < gvLeaveDetl.Rows.Count; j++)
                {
                    if (gvLeaveDetl.Rows[j].Cells[4].Value.Equals("COMPENSATORY OFF"))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            updateLeaveMaster = false;

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                DateTime fromDate = Convert.ToDateTime(dt.Rows[i]["HECT_COFF_FROM_DATE"].ToString());
                                if (Convert.ToBoolean((dtFromDate.Value) >= (fromDate.AddMonths(2))) && Convert.ToBoolean((dtpToDate.Value) >= (fromDate.AddMonths(2))))
                                {
                                    flag = false;
                                    MessageBox.Show("Employee COFF Working Date is Expired", "Leave Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return flag;
                                }
                                //else
                                //{
                                //    flag = false;
                                //    MessageBox.Show("Employee COFF date is expired","Leave Entry Form",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                //    return flag;
                                //}

                            }
                        }

                        else if (dt.Rows.Count == 0)
                        {
                            flag = false;
                            MessageBox.Show("Employee Not Eligible For COFF", "Leave Application Entry form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return flag;

                        }

                    }
                }

                             

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
                dt = null;
            }
            return flag;

        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            // CaluculateNOOfDays();
            double noDays = (dtpToDate.Value - dtFromDate.Value).TotalDays;
            if (noDays < 0)
            {
                //MessageBox.Show("Enter Valid Dates", "Leaves Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpToDate.Value = dtFromDate.Value;
            }

            else
            {
                FillLeaveDetailsToGv();
            }
        }

        private void dtFromDate_ValueChanged(object sender, EventArgs e)
        {
            dtpToDate.Value = dtFromDate.Value;
            FillLeaveDetailsToGv();
            //CaluculateNOOfDays();
            //MessageBox.Show((dtFromDate.Value-DateTime.Today).TotalDays.ToString());
        }

        private void cbFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            // CaluculateNOOfDays();
            if (cbFrom.SelectedIndex == 1)
            {
                cbTo.SelectedIndex = 1;
                FillLeaveDetailsToGv();
            }
            else
            {
                FillLeaveDetailsToGv();
            }
        }

        private void cbTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = Convert.ToInt32((dtpToDate.Value - dtFromDate.Value).TotalDays);
            //CaluculateNOOfDays();
            if (cbFrom.SelectedIndex == 1 && i == 0)
            {
                cbTo.SelectedIndex = 1;
                FillLeaveDetailsToGv();
            }
            else
            {
                FillLeaveDetailsToGv();
            }
        }

        
        private void FillLeaveDetailsToGv()
        {
            
            DataTable dt = new DataTable();
            try
            {
                String strCommand = "SELECT HLTM_LEAVE_TYPE,HLTM_LEAVE_TYPE_DESC FROM HR_LEAVE_TYPE_MASTER";
                objData = new SQLDB();
               
                dt = objData.ExecuteDataSet(strCommand).Tables[0];
       
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            int noOfDays = Convert.ToInt32((dtpToDate.Value - dtFromDate.Value).TotalDays) + 1;
            gvLeaveDetl.Rows.Clear();
            DateTime dtTemp = dtFromDate.Value;
            for (int i = 0; i < noOfDays; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSNo = new DataGridViewTextBoxCell();
                cellSNo.Value = (i + 1).ToString();
                tempRow.Cells.Add(cellSNo);

                DataGridViewCell cellDate = new DataGridViewTextBoxCell();

                cellDate.Value = dtTemp.ToString("dd-MM-yyyy");
                dtTemp = dtTemp.AddDays(1);
                tempRow.Cells.Add(cellDate);

                DataGridViewCell cellLeaveType = new DataGridViewTextBoxCell();
                //if (dt.Rows.Count > 0)
                //{
                //    //DataRow dr = dt.NewRow();
                //    //dr[0] = "LOP";
                //    //dr[1] = "LOSS OF PAY";
                //    //dt.Rows.InsertAt(dr, 4);

                //    //DataRow dr1 = dt.NewRow();
                //    //dr1[0] = "COFF";
                //    //dr1[1] = "COMPENSATORY OFF";
                //    //dt.Rows.InsertAt(dr1, 5);
                   
                //    cellLeaveType.DataSource = dt;
                //    cellLeaveType.DisplayMember = "HLTM_LEAVE_TYPE";
                //    cellLeaveType.ValueMember = "HLTM_LEAVE_TYPE_DESC";
                //}
                cellLeaveType.Value = "";                               
                tempRow.Cells.Add(cellLeaveType);

                DataGridViewCell celllType = new DataGridViewTextBoxCell();
                celllType.Value = "";
                tempRow.Cells.Add(celllType);

                DataGridViewCell cellDesc = new DataGridViewTextBoxCell();
                cellDesc.Value = "";
                tempRow.Cells.Add(cellDesc);

                DataGridViewCell cellLeaveDays = new DataGridViewTextBoxCell();
                DataGridViewCell cellLeaveNoon = new DataGridViewTextBoxCell();
                if (noOfDays == 1)
                {
                    if (i == 0 && cbFrom.SelectedIndex == 0 && cbTo.SelectedIndex == 0)
                    {
                        cellLeaveDays.Value = "0.5";
                        cellLeaveNoon.Value = "1ST HALF";
                    }
                    else if ((i == 0 && cbFrom.SelectedIndex == 1 && cbTo.SelectedIndex == 1))
                    {
                        cellLeaveDays.Value = "0.5";
                        cellLeaveNoon.Value = "2ND HALF";
                    }
                    else if (i == 0 && cbFrom.SelectedIndex == 0 && cbTo.SelectedIndex == 1)
                    {
                        cellLeaveDays.Value = "1";
                        cellLeaveNoon.Value = "FULL DAY";
                    }
                }

                else if ((noOfDays - 1) == i)
                {
                    if (cbTo.SelectedIndex == 0)
                    {
                        cellLeaveDays.Value = "0.5";
                        cellLeaveNoon.Value = "1ST HALF";
                    }
                    else if (cbTo.SelectedIndex == 1)
                    {
                        cellLeaveDays.Value = "1";
                        cellLeaveNoon.Value = "FULL DAY";
                    }
                }
                else if (noOfDays > i)
                {
                    if (i == 0 && cbFrom.SelectedIndex == 0)
                    {
                        cellLeaveDays.Value = "1";
                        cellLeaveNoon.Value = "FULL DAY";
                    }
                    else if (i == 0 && cbFrom.SelectedIndex == 1)
                    {
                        cellLeaveDays.Value = "0.5";
                        cellLeaveNoon.Value = "2ND HALF";
                    }
                    else
                    {
                        cellLeaveDays.Value = "1";
                        cellLeaveNoon.Value = "FULL DAY";
                    }
                }
                tempRow.Cells.Add(cellLeaveDays);
                tempRow.Cells.Add(cellLeaveNoon);

                DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                cellRemarks.Value = "";
                tempRow.Cells.Add(cellRemarks);

                gvLeaveDetl.Rows.Add(tempRow);

            }
            double totNoOfDays = 0;
            if (gvLeaveDetl.Rows.Count > 0)
            {
                for (int iVar = 0; iVar < gvLeaveDetl.Rows.Count; iVar++)
                {
                    totNoOfDays += Convert.ToDouble(gvLeaveDetl.Rows[iVar].Cells["LeaveDays"].Value);
                }
                txtNoOfDays.Text = totNoOfDays + "";
            }
        }

        //private void gvLeaveDetl_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == gvLeaveDetl.Columns["LeaveType"].Index)
        //    {
        //        string strSelectdValue = Convert.ToString((gvLeaveDetl.Rows[e.RowIndex].Cells["LeaveType"] as DataGridViewComboBoxCell).FormattedValue.ToString());
              
        //        if (strSelectdValue == "CL")
        //        {
        //            gvLeaveDetl.Rows[e.RowIndex].Cells["Desc"].Value = "CASUAL LEAVE";
        //            gvLeaveDetl.Rows[e.RowIndex].Cells["LeevType"].Value = "CL";

        //        }
        //        else if (strSelectdValue == "EL")
        //        {
        //            gvLeaveDetl.Rows[e.RowIndex].Cells["Desc"].Value = "EARNED LEAVE";
        //            gvLeaveDetl.Rows[e.RowIndex].Cells["LeevType"].Value = "EL";
        //        }
        //        else if (strSelectdValue == "SL")
        //        {
        //            gvLeaveDetl.Rows[e.RowIndex].Cells["Desc"].Value = "SICK LEAVE";
        //            gvLeaveDetl.Rows[e.RowIndex].Cells["LeevType"].Value = "SL";
        //        }
                               
        //        else if (strSelectdValue == "LOP")
        //        {
        //            gvLeaveDetl.Rows[e.RowIndex].Cells["Desc"].Value = "LOSS OF PAY";
        //            gvLeaveDetl.Rows[e.RowIndex].Cells["LeevType"].Value = "LOP";
        //        }
                
        //        else if (strSelectdValue == "COFF")
        //        {
        //            gvLeaveDetl.Rows[e.RowIndex].Cells["Desc"].Value = "COMPENSATORY OFF";
        //            gvLeaveDetl.Rows[e.RowIndex].Cells["LeevType"].Value = "COFF";

        //            VillageSearch VSearch = new VillageSearch("PresentLawyer");
        //            VSearch.oobj = this;
        //            VSearch.ShowDialog();
        //        }
        //    }
        //    else if (e.ColumnIndex == gvLeaveDetl.Columns["Remarks"].Index)
        //    {
        //        gvLeaveDetl.Rows[0].Cells["Remarks"].Value += "";
        //        txtReason.Text = gvLeaveDetl.Rows[0].Cells["Remarks"].Value.ToString();
        //    }
        //}

  

        //private void gvLeaveDetl_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    //if (gvLeaveDetl.CurrentCell.ColumnIndex == gvLeaveDetl.Columns["LeaveType"].Index)
        //    //{
        //    //    ComboBox cmBox = e.Control as ComboBox;
        //    //    if (cmBox == null)
        //    //        return;
        //    //    cmBox.SelectedIndexChanged += cmBox_SelectedIndexChanged;
        //    //}
        //}
        //void cmBox_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    //ComboBox cmBox=(ComboBox)sender;

        //    //if (cmBox.SelectedValue != null)
        //    //{
        //    //    //MessageBox.Show(cmBox.SelectedValue.ToString());
        //    //}
        //}

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            if (txtEcodeApprovBy.Text.Length > 0)
            {
                try
                {
                    String strCmd = "SELECT MEMBER_NAME FROM EORA_MASTER WHERE ecode=" + Convert.ToInt32(txtEcodeApprovBy.Text);
                    dt = objData.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtNameApprovBy.Text = dt.Rows[0]["MEMBER_NAME"] + "";
                    }
                    else
                    {
                        txtNameApprovBy.Text = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    dt = null;
                    objData = null;
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = string.Empty;
            txtCompany.Text = string.Empty;
            txtBranch.Text = string.Empty;
            txtDept.Text = string.Empty;
            txtDesig.Text = string.Empty;
            txtEcodeSearch.Focus();
            txtReason.Text = string.Empty;
            txtPhNo.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtEcodeApprovBy.Text = string.Empty;
            txtNameApprovBy.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtSLS.Text = "";
            txtELS.Text = "";
            txtCLS.Text = "";
            btnSave.Enabled = true;
            lblChecking.Visible = false;
            lblChecking.Text = "";
            //gvLeaveDetl.Rows.Clear();
       

            txtEcodeSearch.Text = string.Empty;
           
            dtpToDate.Value = DateTime.Today;
            cbTo.SelectedIndex = 1;
            cbFrom.SelectedIndex = 0;

            dtAppDate.Value = DateTime.Today;
            dtFromDate.Value = DateTime.Today;

            flagUpdate = false;

            dtFromDate.Enabled = true; dtpToDate.Enabled = true;
            cbFrom.Enabled = true; cbTo.Enabled = true;
            GeneratingApplNo();
        }

        private void txtApplNo_Validated(object sender, EventArgs e)
        {
            if (txtApplNo.Text.Length > 0)
            {
                objData = new SQLDB();
                DataTable dt = new DataTable();

                SqlParameter[] param = new SqlParameter[3];

                if (sLocType == "HO")
                    param[0] = objData.CreateParameter("@xBranchCode", DbType.String, "", ParameterDirection.Input);
                else
                    param[0] = objData.CreateParameter("@xBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[1] = objData.CreateParameter("@xApplNo", DbType.Int32, Convert.ToInt32(txtApplNo.Text), ParameterDirection.Input);
                param[2] = objData.CreateParameter("@xLocType", DbType.String, sLocType, ParameterDirection.Input);

                DataSet DS = objData.ExecuteDataSet("Get_EmpLeaveDetails", CommandType.StoredProcedure, param);
                dt = DS.Tables[0];
                              
                try
                {                   
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToDouble(dt.Rows[0]["BackDays"].ToString()) > 3)
                        {
                            btnSave.Enabled = false;
                            lblChecking.Visible = true;
                            lblChecking.Text = "This Data Can Not Modify!";
                            //MessageBox.Show("This Data Can Not Modify!", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            lblChecking.Visible = false;
                            lblChecking.Text = "";
                        }

                            flagUpdate = true;

                            txtEcodeSearch.Text = dt.Rows[0]["HELT_EORA_CODE"].ToString();
                            txtReason.Text = dt.Rows[0]["HELT_LEAVE_REASON"].ToString();
                            txtAddress.Text = dt.Rows[0]["HELT_ADDR_LEAVE_PERIOD"].ToString();
                            txtPhNo.Text = dt.Rows[0]["HELT_PHONE_LEAVE_PERIOD"].ToString();
                            txtEmail.Text = dt.Rows[0]["HELT_EMAIL_LEAVE_PERIOD"].ToString();
                            txtEcodeApprovBy.Text = dt.Rows[0]["HELT_LEAVE_APPROVED_BY_ECODE"].ToString();

                            dtAppDate.Value = Convert.ToDateTime(dt.Rows[0]["HELT_CREATED_DATE"].ToString());
                            dtFromDate.Value = Convert.ToDateTime(dt.Rows[0]["HELT_LEAVE_FROM_DATE"].ToString());
                            dtpToDate.Value = Convert.ToDateTime(dt.Rows[0]["HELT_LEAVE_TO_DATE"].ToString());
                            cbFrom.SelectedItem = dt.Rows[0]["HELT_LEAVE_FROM_NOON"].ToString();
                            cbTo.SelectedItem = dt.Rows[0]["HELT_LEAVE_TO_NOON"].ToString();

                            dtFromDate.Enabled = false; dtpToDate.Enabled = false;
                            cbFrom.Enabled = false; cbTo.Enabled = false;


                            objData = new SQLDB();
                            DataTable dtDetl = new DataTable();
                            dtDetl = DS.Tables[1];

                            gvLeaveDetl.Rows.Clear();
                            for (int iVar = 0; iVar < dtDetl.Rows.Count; iVar++)
                            {
                                gvLeaveDetl.Rows.Add();
                                gvLeaveDetl.Rows[iVar].Cells["SLNO"].Value = iVar + 1;
                                gvLeaveDetl.Rows[iVar].Cells["Date"].Value = Convert.ToDateTime(dtDetl.Rows[iVar]["HELTL_LEAVE_DATE"].ToString()).ToString("dd/MMM/yyyy");
                                gvLeaveDetl.Rows[iVar].Cells["LeaveDays"].Value = dtDetl.Rows[iVar]["HELTL_LEAVE_DAYS"].ToString();
                                gvLeaveDetl.Rows[iVar].Cells["Remarks"].Value = dtDetl.Rows[iVar]["HELTL_LEAVE_REMARKS"].ToString();
                                gvLeaveDetl.Rows[iVar].Cells["LeaveNoon"].Value = dtDetl.Rows[iVar]["HELTL_LEAVE_NOON"].ToString();
                                string strLType = dtDetl.Rows[iVar]["HELTL_LEAVE_TYPE"].ToString();
                                gvLeaveDetl.Rows[iVar].Cells["LeaveType"].Value = strLType;
                                if (strLType == "CL")
                                {
                                    gvLeaveDetl.Rows[iVar].Cells["LeaveType"].Value = "CL";
                                    gvLeaveDetl.Rows[iVar].Cells["Desc"].Value = "CASUAL LEAVE";
                                }
                                else if (strLType == "EL")
                                {
                                    gvLeaveDetl.Rows[iVar].Cells["LeaveType"].Value = "EL";
                                    gvLeaveDetl.Rows[iVar].Cells["Desc"].Value = "EARNED LEAVE";
                                }
                                else if (strLType == "SL")
                                {
                                    gvLeaveDetl.Rows[iVar].Cells["LeaveType"].Value = "SL";
                                    gvLeaveDetl.Rows[iVar].Cells["Desc"].Value = "SICK LEAVE";
                                }
                                else if (strLType == "LOP")
                                {
                                    gvLeaveDetl.Rows[iVar].Cells["LeaveType"].Value = "LOP";
                                    gvLeaveDetl.Rows[iVar].Cells["Desc"].Value = "LOSS OF PAY";
                                }
                                else if (strLType == "COFF")
                                {
                                    gvLeaveDetl.Rows[iVar].Cells["LeaveType"].Value = "COFF";
                                    gvLeaveDetl.Rows[iVar].Cells["Desc"].Value = "COMPENSATORY OFF";
                                    gvLeaveDetl.Rows[iVar].Cells["CoffAgntDate"].Value = Convert.ToDateTime(dtDetl.Rows[iVar]["HELTL_COFF_AGNST_DATE"].ToString()).ToShortDateString();
                                }
                            }
                            //availedCl -= cl;
                            //balanceCl += cl;

                            //availedEl -= el;
                            //balanceEl += el;

                            //availedSl -= sl;
                            //balanceSl += sl;
                            //string strYear = DateTime.Now.Year.ToString();

                            //sqlText += " update HR_EMPLOYEE_LEAVE_CREDITS set HELC_AVAILED_LEAVES=" + availedCl + ",HELC_BALANCE_LEAVES=" + balanceCl + " where HELC_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) + " and HELC_LEAVE_TYPE='CL' and HELC_LEAVE_YEAR="+strYear+" ";
                            //sqlText += " update HR_EMPLOYEE_LEAVE_CREDITS set HELC_AVAILED_LEAVES=" + availedEl + ",HELC_BALANCE_LEAVES=" + balanceEl + " where HELC_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) + " and HELC_LEAVE_TYPE='EL' and HELC_LEAVE_YEAR="+strYear+" ";
                            //sqlText += " update HR_EMPLOYEE_LEAVE_CREDITS set HELC_AVAILED_LEAVES=" + availedSl + ",HELC_BALANCE_LEAVES=" + balanceSl + " where HELC_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) + " and HELC_LEAVE_TYPE='SL' and HELC_LEAVE_YEAR="+strYear+" ";

                            //int iRes = objData.ExecuteSaveData(sqlText);
                                               

                    }
                    else
                    {
                        GeneratingApplNo();
                        flagUpdate = false;
                        btnCancel_Click(null, null);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    dt = null;
                    objData = null;
                }
            }
            else
            {
                btnCancel_Click(null, null);
            }
        }



        private void gvLeaveDetl_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //SetComboBoxCellType objChangeCellType = new SetComboBoxCellType(ChangeCellToComboBox);
            //if (e.ColumnIndex == this.gvLeaveDetl.Columns["Remarks"].Index)
            //{
            //    this.gvLeaveDetl.BeginInvoke(objChangeCellType, e.RowIndex);
            //    //bIsComboBox = false;
            //}
        }
        //private void ChangeCellToComboBox(int iRowIndex)
        //{
        //    ////if (bIsComboBox == false)
        //    ////{
        //    //    int cmdSelectIndex = 0;
        //    //    DataGridViewComboBoxCell dgComboCell = new DataGridViewComboBoxCell();
        //    //    dgComboCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
        //    //    DataTable dt = new DataTable();
        //    //    dt.Columns.Add("Value", typeof(string));
        //    //    dt.Columns.Add("Text", typeof(string));
        //    //    dt.Rows.Add(new Object[] { "1", "CL" });
        //    //    dt.Rows.Add(new Object[] { "2", "EL" });
        //    //    dt.Rows.Add(new Object[] { "3", "SL" });
        //    //    if (gvLeaveDetl.Rows[iRowIndex].Cells[gvLeaveDetl.CurrentCell.ColumnIndex].Value.ToString() != "")
        //    //        cmdSelectIndex = Convert.ToInt32(gvLeaveDetl.Rows[iRowIndex].Cells[gvLeaveDetl.CurrentCell.ColumnIndex].Value);
        //    //    //else
        //    //    //    cmdSelectIndex = 0;
        //    //    dgComboCell.DataSource = dt;
        //    //    dgComboCell.ValueMember = "Value";
        //    //    dgComboCell.DisplayMember = "Text";

        //    //    gvLeaveDetl.Rows[iRowIndex].Cells[gvLeaveDetl.CurrentCell.ColumnIndex] = dgComboCell;

        //    //    //((DataGridViewComboBoxCell)gvDCDetails.Rows[iRowIndex].Cells[gvDCDetails.CurrentCell.ColumnIndex]). = true;
        //    //    //bIsComboBox = true;
        //    //}
        //}

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToLower(e.KeyChar);
        }

        private void gvLeaveDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvLeaveDetl.Columns["Edit"].Index == e.ColumnIndex)
                {
                    if (txtName.Text.Length > 0)
                    {
                        AddingLeaveDetails VSearch = new AddingLeaveDetails(e.RowIndex, this, txtEcodeSearch.Text);
                        VSearch.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtEcodeSearch.Focus();
                    }
                }
            }

            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Delete",
                                              "CRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes && flagUpdate == true)
            {
                try
                {
                    int res = 0;
                    HRInfo objHr = new HRInfo();
                    String sqlText = "";

                    objHr.UpdatingLeavesCreditAgainstLeaveUpdate(Convert.ToInt32(txtEcodeSearch.Text), Convert.ToInt32(txtApplNo.Text),dtFromDate.Value.Year);

                    if (sLocType == "HO")
                    {
                        sqlText = " DELETE FROM HR_EMPLOYEE_LEAVE_TRN_LNITEM WHERE HELTL_COMPANY_CODE='" + txtCompany.Tag + "' AND HELTL_BRANCH_CODE='" + txtBranch.Tag
                                  + "' AND HELTL_APPL_NUMBER=" + Convert.ToInt32(txtApplNo.Text) + " AND isnull(HELTL_LOC_TYPE,'HO')='" + sLocType + "' ";
                        sqlText += " DELETE FROM HR_EMPLOYEE_LEAVE_TRN WHERE HELT_COMPANY_CODE='" + txtCompany.Tag + "' AND HELT_BRANCH_CODE='" + txtBranch.Tag
                                  + "' AND HELT_APPL_NUMBER=" + Convert.ToInt32(txtApplNo.Text) + " AND isnull(HELT_LOC_TYPE,'HO')='" + sLocType + "'";
                    }
                    else
                    {
                        sqlText = " DELETE FROM HR_EMPLOYEE_LEAVE_TRN_LNITEM WHERE HELTL_COMPANY_CODE='" + CommonData.CompanyCode +
                                  "' AND HELTL_BRANCH_CODE='" + CommonData.BranchCode +
                                  "' AND HELTL_APPL_NUMBER=" + Convert.ToInt32(txtApplNo.Text) +
                                  " AND isnull(HELTL_LOC_TYPE,'HO')='" + sLocType + "' ";

                        sqlText += " DELETE FROM HR_EMPLOYEE_LEAVE_TRN WHERE HELT_COMPANY_CODE='" + CommonData.CompanyCode + "' AND HELT_BRANCH_CODE='" + CommonData.BranchCode
                                  + "' AND HELT_APPL_NUMBER=" + Convert.ToInt32(txtApplNo.Text) + " AND isnull(HELT_LOC_TYPE,'HO')='" + sLocType + "'";
                        
                    }
                    objData = new SQLDB();
                    res = objData.ExecuteSaveData(sqlText);
                    if(res>0)
                    {
                        MessageBox.Show("Data Deleted Successfully","CRM", MessageBoxButtons.OK,MessageBoxIcon.Information);
                        flagUpdate = false;
                        btnCancel_Click(null, null);
                        GeneratingApplNo();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

       
    }
}

