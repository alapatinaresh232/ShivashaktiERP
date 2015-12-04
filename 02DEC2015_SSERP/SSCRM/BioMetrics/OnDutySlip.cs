using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SSCRMDB;
using System.Data.SqlClient;

namespace SSCRM
{
    public partial class OnDutySlip : Form
    {
        private SQLDB objData = null;
        private bool flagUpdate = false;
        string strCommand1 = "";
        private string sFormType;
        private string sFinYear = "";
        public OnDutySlip()
        {
            InitializeComponent();
        }
        public OnDutySlip(string sType)
        {
            InitializeComponent();
            sFormType = sType;
        }

        private void OnDutyEntryForm_Load(object sender, EventArgs e)
        {
            GeneratingApplNo();
            lblChecking.Visible = false;
            lblChecking.Text = "";

            dtpToDate.Value = DateTime.Today;
            cbTo.SelectedIndex = 1;
            cbFrom.SelectedIndex = 0;

            dtAppDate.Value = DateTime.Today;
            dtFromDate.Value = DateTime.Today;

            if (sFormType == "BR" &&  CommonData.LogUserId.ToUpper() != "ADMIN")
            {
                FillVisitingBranch();
                btnDelete.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
            }
            

        }
         private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void GeneratingApplNo()
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            int iMax = 0;
            try
            {
                String strCmd ="";
                if (sFormType == "HO")
                {
                    strCmd = "SELECT ISNULL(MAX(HEOT_APPL_NUMBER),0)+1 TRNNO FROM HR_EMPLOYEE_OD_TRN  where heot_loc_type='" + sFormType + "'";
                }
                else
                {
                    strCmd = "SELECT ISNULL(MAX(HEOT_APPL_NUMBER),0)+1 TRNNO FROM HR_EMPLOYEE_OD_TRN  where heot_loc_type='" + sFormType + "' and "+
                                "heot_branch_code='"+CommonData.BranchCode+"'";
                }
                iMax = Convert.ToInt32(objData.ExecuteDataSet(strCmd).Tables[0].Rows[0][0].ToString()); 
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
            //sFinYear = dtAppDate.Value.ToString("yyyy");
            if (txtEcodeSearch.Text.Length > 0)
            {
                try
                {

                    string strCmd = "";
                    if (sFormType == "HO")
                   {
                        strCmd = "SELECT EM.BRANCH_CODE,EM.MEMBER_NAME,EM.DESIG,EM.COMPANY_CODE,"
                                        + "CM_COMPANY_NAME,BM.BRANCH_NAME,dept_name,(SELECT HAMH_ADD_PRES_ADDR_HNO+','+HAMH_ADD_PRES_ADDR_LANDMARK+','+HAMH_ADD_PRES_ADDR_VILL_OR_TOWN+','+HAMH_ADD_PRES_ADDR_MANDAL+','+HAMH_ADD_PRES_ADDR_DISTRICT+','+HAMH_ADD_PRES_ADDR_STATE+','+CAST(HAMH_ADD_PRES_ADDR_PIN as VARCHAR(10)) FROM HR_APPL_MASTER_HEAD WHERE HAMH_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text)
                                        + ") address,HECD_EMP_MOBILE_NO,HECD_EMP_EMAIL_ID FROM EORA_MASTER EM "
                                        + "left join COMPANY_MAS ON CM_COMPANY_CODE=COMPANY_CODE left join "
                                        + "BRANCH_MAS BM ON BM.BRANCH_CODE=EM.BRANCH_CODE left join Dept_Mas "
                                        + "ON dept_code=DEPT_ID  left join HR_EMP_CONTACT_DETL ON ECODE=HECD_EORA_CODE "
                                        + " LEFT JOIN HR_EMPLOYEE_LEAVE_CREDITS ON ECODE=HELC_EORA_CODE     "
                                        + " WHERE ecode=" + Convert.ToInt32(txtEcodeSearch.Text)
                                        + " AND HELC_LEAVE_YEAR='" + dtFromDate.Value.Year + "' order by helc_leave_year desc "; 
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
                        if (sFormType == "HO")
                        {
                            FillVisitingBranch();
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

        private void FillVisitingBranch()
        {
            try
            {
                string strSQL ="";
                if (sFormType == "HO")
                    strSQL = "SELECT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MAS WHERE  ACTIVE='T' and BRANCH_CODE <> '" + txtBranch.Tag + "' order by BRANCH_NAME";
                else
                    strSQL = "SELECT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MAS WHERE  ACTIVE='T' and BRANCH_CODE <> '" + CommonData.BranchCode + "' order by BRANCH_NAME";
                objData = new SQLDB();
                DataTable dt = objData.ExecuteDataSet(strSQL).Tables[0];
                cmbVisitingBranch.DataSource = null;
                if(dt.Rows.Count>0)
                {
                    cmbVisitingBranch.DataSource = dt;
                    cmbVisitingBranch.DisplayMember = "BRANCH_NAME";
                    cmbVisitingBranch.ValueMember = "BRANCH_CODE";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void txtEcodeSearch_Validated(object sender, EventArgs e)
        {

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
                //if (CheckEmployeeIsonLeave())
                //{
                //if ( > 0)
                //{
                SaveLeavesEntryHead();

                SaveLeavesEntryDetail();
                //MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //GeneratingApplNo();
                //btnCancel_Click(null, null);

                //else
                //{
                //    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //}


                //}
                //}

            }
        }
        private int SaveLeavesEntryHead()
        {
            objData = new SQLDB();
            int result = 0;
            try
            {
                
                if (flagUpdate == true)
                {

                    if (sFormType == "HO")
                    {

                        strCommand1 = " UPDATE HR_EMPLOYEE_OD_TRN SET HEOT_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) + "," +
                                    " HEOT_COMPANY_CODE='" + txtCompany.Tag + "',HEOT_BRANCH_CODE='" + txtBranch.Tag + "'," +
                                    " HEOT_OD_FROM_DATE='" + Convert.ToDateTime(dtFromDate.Value).ToString("dd/MMM/yyyy") +
                                    "',HEOT_OD_FROM_NOON='" + cbFrom.Text + "',HEOT_OD_TO_DATE='" + Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy") +
                                    "',HEOT_OD_TO_NOON='" + cbTo.Text + "',HEOT_NOOF_OD_DAYS=" + Convert.ToDouble(txtNoOfDays.Text) +
                                    ",HEOT_OD_REASON='" + txtReason.Text.Trim().Replace("'","") + "',HEOT_ADDR_OD_PERIOD='" + txtAddress.Text.Replace("'","") +
                                    "',HEOT_PHONE_OD_PERIOD='" + txtPhNo.Text + "',HEOT_EMAIL_OD_PERIOD='" + txtEmail.Text +
                                    "',HEOT_OD_APPROVED_BY_ECODE=" + Convert.ToInt32(txtEcodeApprovBy.Text) + ",HEOT_MODIFIED_BY='" + CommonData.LogUserId +
                                    "',HEOT_MODIFIED_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                    "' WHERE HEOT_APPL_NUMBER=" + Convert.ToInt32(txtApplNo.Text) + " AND isnull(heot_loc_type,'HO')='"+ sFormType +"'";
                    }
                    else
                    {
                        strCommand1 = " UPDATE HR_EMPLOYEE_OD_TRN SET HEOT_EORA_CODE=" + Convert.ToInt32(txtEcodeSearch.Text) + "," +
                                    " HEOT_COMPANY_CODE='" + txtCompany.Tag + "',HEOT_BRANCH_CODE='" + txtBranch.Tag + "'," +
                                    " HEOT_OD_FROM_DATE='" + Convert.ToDateTime(dtFromDate.Value).ToString("dd/MMM/yyyy") +
                                    "',HEOT_OD_FROM_NOON='" + cbFrom.Text + "',HEOT_OD_TO_DATE='" + Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy") +
                                    "',HEOT_OD_TO_NOON='" + cbTo.Text + "',HEOT_NOOF_OD_DAYS=" + Convert.ToDouble(txtNoOfDays.Text) +
                                    ",HEOT_OD_REASON='" + txtReason.Text.Trim().Replace("'", "") + "',HEOT_ADDR_OD_PERIOD='" + txtAddress.Text.Replace("'","") +
                                    "',HEOT_PHONE_OD_PERIOD='" + txtPhNo.Text + "',HEOT_EMAIL_OD_PERIOD='" + txtEmail.Text +
                                    "',HEOT_OD_APPROVED_BY_ECODE=" + Convert.ToInt32(txtEcodeApprovBy.Text) + ",HEOT_MODIFIED_BY='" + CommonData.LogUserId +
                                    "',HEOT_MODIFIED_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                                    "' WHERE HEOT_APPL_NUMBER=" + Convert.ToInt32(txtApplNo.Text) +
                                    " and isnull(heot_loc_type,'HO')='" + sFormType + "' and HEOT_BRANCH_CODE='"+ CommonData.BranchCode +"'";
                    }
                   
                }
                else
                {
                    GeneratingApplNo();
                    objData = new SQLDB();
                    
                    strCommand1 = " INSERT INTO HR_EMPLOYEE_OD_TRN (" +
                                   "HEOT_COMPANY_CODE," +
                                   "HEOT_BRANCH_CODE," +
                                   "HEOT_APPL_NUMBER," +
                                   "HEOT_EORA_CODE," +
                                   "HEOT_OD_FROM_DATE," +
                                   "HEOT_OD_FROM_NOON," +
                                   "HEOT_OD_TO_DATE," +
                                   "HEOT_OD_TO_NOON," +
                                   "HEOT_NOOF_OD_DAYS," +
                                   "HEOT_OD_REASON," +
                                   "HEOT_ADDR_OD_PERIOD," +
                                   "HEOT_PHONE_OD_PERIOD," +
                                   "HEOT_EMAIL_OD_PERIOD," +
                                   "HEOT_OD_APPROVED_BY_ECODE," +
                                   "HEOT_CREATED_BY," +
                                   "HEOT_CREATED_DATE,heot_loc_type)" +
                           "VALUES('" + txtCompany.Tag + "','" + txtBranch.Tag + "'," + Convert.ToInt32(txtApplNo.Text)
                           + "," + Convert.ToInt32(txtEcodeSearch.Text) + ",'" + Convert.ToDateTime(dtFromDate.Value).ToString("dd/MMM/yyyy")
                           + "','" + cbFrom.Text + "','" + Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy") + "','" + cbTo.Text + "'," + Convert.ToDouble(txtNoOfDays.Text)
                           + ",'" + txtReason.Text.Trim().Replace("'","") + "','" + txtAddress.Text.Replace("'","") + "','" + txtPhNo.Text + "','" + txtEmail.Text + "','" + Convert.ToInt32(txtEcodeApprovBy.Text)
                           + "','" + CommonData.LogUserId + "','" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + "','"+sFormType+"')";
                }
               // result = objData.ExecuteSaveData(strCommand);
                if (result > 0)
                {


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
            int result = 0;
            try
            {
               // String sqlText="";
                if (sFormType == "HO")
                    strCommand1 += " DELETE FROM HR_EMPLOYEE_OD_TRN_LNITEM WHERE HEOTL_COMPANY_CODE='" + txtCompany.Tag + "' AND HEOTL_BRANCH_CODE='" + txtBranch.Tag
                                    + "' AND HEOTL_APPL_NUMBER=" + Convert.ToInt32(txtApplNo.Text) + " and heotl_loc_type='" + sFormType + "' ";
                else
                    strCommand1 += " DELETE FROM HR_EMPLOYEE_OD_TRN_LNITEM WHERE HEOTL_COMPANY_CODE='" + txtCompany.Tag + "' AND HEOTL_BRANCH_CODE='" + CommonData.BranchCode
                                    + "' AND HEOTL_APPL_NUMBER=" + Convert.ToInt32(txtApplNo.Text) + " and heotl_loc_type='" + sFormType + "' ";

                //result = objData.ExecuteSaveData(sqlText);
                //strCommand = "";
                for (int iVar = 0; iVar < gvODDetl.Rows.Count; iVar++)
                {
                    String strDate = Convert.ToDateTime(gvODDetl.Rows[iVar].Cells["Date"].Value).ToString("dd/MMM/yyyy");
                    string strToBranch = "",strRemarks="";
                    if(gvODDetl.Rows[iVar].Cells["VisitingBranchCode"].Value != null)
                     strToBranch = gvODDetl.Rows[iVar].Cells["VisitingBranchCode"].Value.ToString();
                    if (gvODDetl.Rows[iVar].Cells["Remarks"].Value != null)
                        strRemarks = gvODDetl.Rows[iVar].Cells["Remarks"].Value.ToString().Replace("'","");

                    strCommand1 += " INSERT INTO HR_EMPLOYEE_OD_TRN_LNITEM (" +
                                       "HEOTL_COMPANY_CODE," +
                                       "HEOTL_BRANCH_CODE," +
                                       "HEOTL_APPL_NUMBER," +
                                       "HEOTL_SNO," +
                                       "HEOTL_EORA_CODE," +
                                       "HEOTL_OD_DATE," +
                                       "HEOTL_OD_NOON," +
                                       "HEOTL_OD_DAYS," +
                                       "HEOTL_OD_REMARKS," +
                                       "HEOTL_CREATED_BY," +
                                       "HEOTL_CREATED_DATE,heotl_loc_type,HEOTL_TO_BRANCH_CODE)" +
                                               "VALUES('" + txtCompany.Tag + "','" + txtBranch.Tag + "'," + Convert.ToInt32(txtApplNo.Text)
                                               + "," + Convert.ToInt32(gvODDetl.Rows[iVar].Cells["SLNO"].Value) + "," + Convert.ToInt32(txtEcodeSearch.Text)
                                               + ",'" + strDate
                                               + "','" + gvODDetl.Rows[iVar].Cells["LeaveNoon"].Value.ToString().Trim() + "','" + Convert.ToDouble(gvODDetl.Rows[iVar].Cells["LeaveDays"].Value)
                                               + "','" + strRemarks
                                               + "','" + CommonData.LogUserId 
                                               + "','" + Convert.ToDateTime(dtAppDate.Value).ToString("dd/MMM/yyyy")
                                               + "','" + sFormType + "','" + strToBranch + "')";
                }
                objData = new SQLDB();
                result = objData.ExecuteSaveData(strCommand1);
                if (result > 0)
                {

                    MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCancel_Click(null, null);
                    GeneratingApplNo();
                    flagUpdate = false;
                }
                else
                {
                    MessageBox.Show("Data not Saved", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnCancel_Click(null, null);
                    GeneratingApplNo();
                    flagUpdate = false;
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
                MessageBox.Show("Please Enter Valid Ecode", "OD Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEcodeSearch.Focus();
            }
            else if (txtReason.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Reason", "OD Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtReason.Focus();
            }
            else if (txtAddress.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Address", "OD Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
            }
            else if (txtPhNo.Text.Length != 10)
            {
                flag = false;
                MessageBox.Show("Please Enter Valid Phone No", "OD Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhNo.Focus();
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
            {
                flag = false;
                MessageBox.Show("Please Enter Valid Email", "OD Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
            }
            //else if (Convert.ToDouble(txtNoOfDays.Text) < .5)
            //{
            //    flag = false;
            //    MessageBox.Show("Enter Valid Dates", "Leaves Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            else if (txtNameApprovBy.Text.Length == 0 || txtEcodeApprovBy.Text.Equals(txtEcodeSearch.Text))
            {
                flag = false;
                MessageBox.Show("Ecode and Approved Ecode Should not be Same", "OD Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEcodeApprovBy.Text = "";
                txtNameApprovBy.Text = "";
                txtEcodeApprovBy.Focus();
            }
            //else if ((dtFromDate.Value - DateTime.Now).TotalDays < -10)
            //{
            //    flag = false;
            //    MessageBox.Show("Minimum 10 days Back Data Can Enter", "OD Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            else if (CheckingRecord() == false)
            {
                flag = false;
                MessageBox.Show("This Record already Exists", "OD Application Entry Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }
           
            return flag;
        }

        private bool CheckEmployeeIsonLeave()
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            bool flag = true;

            try
            {
                string strCmd = "SELECT HELT_EORA_CODE,HELT_LEAVE_FROM_DATE,HELT_LEAVE_TO_DATE FROM HR_EMPLOYEE_LEAVE_TRN WHERE HELT_EORA_CODE="+ txtEcodeSearch.Text +"";
                dt = objData.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DateTime fromDate = Convert.ToDateTime(dt.Rows[i]["HELT_LEAVE_FROM_DATE"].ToString());
                        DateTime toDate = Convert.ToDateTime(dt.Rows[i]["HELT_LEAVE_TO_DATE"].ToString());

                        if ((dtFromDate.Value <= fromDate && dtpToDate.Value >= toDate) || (dtFromDate.Value >= (toDate) && dtpToDate.Value <= (toDate)))
                        {
                            flag = false;
                            MessageBox.Show("Employee applied for Leave on these Dates", "On DutySlip Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return flag;
                        }
                        else if ((dtFromDate.Value >= (fromDate) && dtpToDate.Value <= (toDate)) || (dtFromDate.Value >= (fromDate) && dtpToDate.Value <= (fromDate)))
                        {
                            flag = false;
                            MessageBox.Show("Employee applied for Leave on these Dates", "On DutySlip Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private bool CheckingRecord()
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            bool FlagCheck = true;
            try
            {
                for (int i = 0; i < gvODDetl.Rows.Count; i++)
                {
                    string strCmd = "SELECT HEOTL_OD_DATE,HEOTL_OD_NOON FROM HR_EMPLOYEE_OD_TRN_LNITEM WHERE HEOTL_EORA_CODE=" + txtEcodeSearch.Text + " AND HEOTL_OD_DATE='" + Convert.ToDateTime(gvODDetl.Rows[i].Cells["Date"].Value).ToString("yyyy/MM/dd") + "' AND HEOTL_OD_NOON='" + gvODDetl.Rows[i].Cells["LeaveNoon"].Value +"'";
                            //+ "' and HEOTL_LOC_TYPE='"+sFormType+"' ";
                    dt = objData.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        FlagCheck = false;
                        return FlagCheck;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return FlagCheck;
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
                FillODDetailsToGv();
                FillVisitBranch();
            }
        }

        private void dtFromDate_ValueChanged(object sender, EventArgs e)
        {
            dtpToDate.Value = dtFromDate.Value;
            FillODDetailsToGv();
            FillVisitBranch();
            //CaluculateNOOfDays();
        }

        private void cbFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            // CaluculateNOOfDays();
            if (cbFrom.SelectedIndex == 1)
            {
                cbTo.SelectedIndex = 1;
                FillODDetailsToGv();
                FillVisitBranch();
            }
            else
            {
                FillODDetailsToGv();
                FillVisitBranch();
            }
        }

        private void cbTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int i = Convert.ToInt32((dtpToDate.Value - dtFromDate.Value).TotalDays);
            //CaluculateNOOfDays();
            if (cbFrom.SelectedIndex == 1 && i == 0)
            {
                cbTo.SelectedIndex = 1;
                FillODDetailsToGv();
                FillVisitBranch();
            }
            else
            {
                FillODDetailsToGv();
                FillVisitBranch();
            }
        }

        private void txtNoOfDays_Validated(object sender, EventArgs e)
        {
            //FillLeaveDetailsToGv();

        }
        private void txtNoOfDays_TextChanged(object sender, EventArgs e)
        {
            //FillLeaveDetailsToGv();
        }
        private void FillODDetailsToGv()
        {
            
            DataTable dt = new DataTable();
            try
            {
                String strCommand = "SELECT HLTM_LEAVE_TYPE,HLTM_LEAVE_TYPE_DESC FROM HR_LEAVE_TYPE_MASTER ";
                objData = new SQLDB();
                dt = objData.ExecuteDataSet(strCommand).Tables[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            int noOfDays = Convert.ToInt32((dtpToDate.Value - dtFromDate.Value).TotalDays) + 1;
            gvODDetl.Rows.Clear();
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

                //DataGridViewComboBoxCell cellLeaveType = new DataGridViewComboBoxCell();
                //if (dt.Rows.Count > 0)
                //{
                //    cellLeaveType.DataSource = dt;
                //    cellLeaveType.DisplayMember = "HLTM_LEAVE_TYPE";
                //    cellLeaveType.ValueMember = "HLTM_LEAVE_TYPE_DESC";

                //}
                //tempRow.Cells.Add(cellLeaveType);

                //DataGridViewCell celllType = new DataGridViewTextBoxCell();
                //celllType.Value = "";
                //tempRow.Cells.Add(celllType);

                //DataGridViewCell cellDesc = new DataGridViewTextBoxCell();
                //cellDesc.Value = "";
                //tempRow.Cells.Add(cellDesc);

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

                gvODDetl.Rows.Add(tempRow);

            }
            double totNoOfDays = 0;
            if (gvODDetl.Rows.Count > 0)
            {
                for (int iVar = 0; iVar < gvODDetl.Rows.Count; iVar++)
                {
                    totNoOfDays += Convert.ToDouble(gvODDetl.Rows[iVar].Cells["LeaveDays"].Value);
                }
                txtNoOfDays.Text = totNoOfDays + "";
            }
        }

       

  

        private void gvLeaveDetl_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //if (gvLeaveDetl.CurrentCell.ColumnIndex == gvLeaveDetl.Columns["LeaveType"].Index)
            //{
            //    ComboBox cmBox = e.Control as ComboBox;
            //    if (cmBox == null)
            //        return;
            //    cmBox.SelectedIndexChanged += cmBox_SelectedIndexChanged;
            //}
        }
        void cmBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            //ComboBox cmBox=(ComboBox)sender;

            //if (cmBox.SelectedValue != null)
            //{
            //    //MessageBox.Show(cmBox.SelectedValue.ToString());
            //}
        }

      
        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
            gvODDetl.Rows.Clear();
            gvODDetl.Enabled = true;
            txtName.Text = string.Empty;
            txtCompany.Text = string.Empty;
            txtBranch.Text = string.Empty;
            txtDept.Text = string.Empty;
            txtDesig.Text = string.Empty;
            btnSave.Enabled = true;
            txtReason.Text = string.Empty;
            txtPhNo.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtEcodeApprovBy.Text = string.Empty;
            txtNameApprovBy.Text = string.Empty;
            txtAddress.Text = string.Empty;

            txtEcodeSearch.Text = string.Empty;
           
            dtpToDate.Value = DateTime.Today;
            cbTo.SelectedIndex = 0;
            cbTo.SelectedIndex = 1;
            cbFrom.SelectedIndex = 0;

            dtAppDate.Value = DateTime.Today;
            dtFromDate.Value = DateTime.Today;

            lblChecking.Visible = false;
            lblChecking.Text = "";
            btnSave.Enabled = true;
            GeneratingApplNo();
        }

        private void txtApplNo_Validated(object sender, EventArgs e)
        {


            DataTable dt = new DataTable();
           // String sqlText = "SELECT HEOT_EORA_CODE,HEOT_OD_FROM_DATE,HEOT_OD_FROM_NOON,HEOT_OD_TO_DATE,HEOT_OD_TO_NOON,HEOT_OD_REASON,HEOT_ADDR_OD_PERIOD,HEOT_PHONE_OD_PERIOD,HEOT_EMAIL_OD_PERIOD,HEOT_OD_APPROVED_BY_ECODE,HEOT_CREATED_DATE FROM HR_EMPLOYEE_OD_TRN WHERE HEOT_APPL_NUMBER=" + Convert.ToInt32(txtApplNo.Text);
            objData = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = objData.CreateParameter("@xApplNo", DbType.String, txtApplNo.Text, ParameterDirection.Input);
            param[1] = objData.CreateParameter("@xLocType", DbType.String, sFormType, ParameterDirection.Input);
            if (sFormType == "HO")
                param[2] = objData.CreateParameter("@xBranch", DbType.String, "", ParameterDirection.Input);
            else
                param[2] = objData.CreateParameter("@xBranch", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
           DataSet DS = objData.ExecuteDataSet("GetODFormDetails", CommandType.StoredProcedure, param);
           dt = DS.Tables[0];
            //}
       
          
            try
            {
               
                if (dt.Rows.Count > 0)
                {
                    flagUpdate = true;
                    if (Convert.ToDouble(dt.Rows[0]["BackDays"].ToString()) > 4)
                    {
                        btnSave.Enabled = false;
                        lblChecking.Visible = true;
                        lblChecking.Text = "This Data Can Not Modify";
                        gvODDetl.Enabled = false;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        lblChecking.Visible = false;
                        lblChecking.Text = "";
                        gvODDetl.Enabled = true;
                    }

                    txtEcodeSearch.Text = dt.Rows[0]["HEOT_EORA_CODE"].ToString();
                    txtReason.Text = dt.Rows[0]["HEOT_OD_REASON"].ToString();
                    txtAddress.Text = dt.Rows[0]["HEOT_ADDR_OD_PERIOD"].ToString();
                    txtPhNo.Text = dt.Rows[0]["HEOT_PHONE_OD_PERIOD"].ToString();
                    txtEmail.Text = dt.Rows[0]["HEOT_EMAIL_OD_PERIOD"].ToString();
                    txtEcodeApprovBy.Text = dt.Rows[0]["HEOT_OD_APPROVED_BY_ECODE"].ToString();

                    dtAppDate.Value = Convert.ToDateTime(dt.Rows[0]["HEOT_CREATED_DATE"].ToString());
                    dtFromDate.Value = Convert.ToDateTime(dt.Rows[0]["HEOT_OD_FROM_DATE"].ToString());
                    dtpToDate.Value = Convert.ToDateTime(dt.Rows[0]["HEOT_OD_TO_DATE"].ToString());
                    cbFrom.SelectedItem = dt.Rows[0]["HEOT_OD_FROM_NOON"].ToString();
                    cbTo.SelectedItem = dt.Rows[0]["HEOT_OD_TO_NOON"].ToString();
                    objData = new SQLDB();
                    dt = new DataTable();
                    //sqlText = "";
                    //sqlText = "SELECT HEOTL_OD_REMARKS FROM HR_EMPLOYEE_OD_TRN_LNITEM WHERE HEOTL_APPL_NUMBER="+Convert.ToInt32(txtApplNo.Text);
                    DataTable dt1 = DS.Tables[1];
                    gvODDetl.Rows.Clear();
                    for (int iVar = 0; iVar < dt1.Rows.Count; iVar++)
                    {
                        gvODDetl.Rows.Add();
                        gvODDetl.Rows[iVar].Cells["SLNO"].Value = (iVar +1);
                        gvODDetl.Rows[iVar].Cells["Date"].Value = Convert.ToDateTime( dt1.Rows[iVar]["HEOTL_OD_DATE"].ToString()).ToShortDateString();
                        gvODDetl.Rows[iVar].Cells["LeaveDays"].Value = dt1.Rows[iVar]["HEOTL_OD_DAYS"].ToString();
                        gvODDetl.Rows[iVar].Cells["LeaveNoon"].Value = dt1.Rows[iVar]["HEOTL_OD_NOON"].ToString();
                        gvODDetl.Rows[iVar].Cells["VisitingBranchCode"].Value = dt1.Rows[iVar]["HEOTL_TO_BRANCH_CODE"].ToString();
                        gvODDetl.Rows[iVar].Cells["VisitingBranch"].Value = dt1.Rows[iVar]["BRANCH_NAME"].ToString();
                        gvODDetl.Rows[iVar].Cells["Remarks"].Value = dt1.Rows[iVar]["HEOTL_OD_REMARKS"].ToString();
                    }
                }
                else
                {
                    GeneratingApplNo();
                    flagUpdate = false;
                    btnSave.Enabled = true;
                    lblChecking.Visible = false;
                    lblChecking.Text = "";
                    btnCancel_Click(null, null);
                    gvODDetl.Enabled = true;
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



        private void gvLeaveDetl_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //SetComboBoxCellType objChangeCellType = new SetComboBoxCellType(ChangeCellToComboBox);
            //if (e.ColumnIndex == this.gvLeaveDetl.Columns["Remarks"].Index)
            //{
            //    this.gvLeaveDetl.BeginInvoke(objChangeCellType, e.RowIndex);
            //    //bIsComboBox = false;
            //}
        }
        private void ChangeCellToComboBox(int iRowIndex)
        {
            ////if (bIsComboBox == false)
            ////{
            //    int cmdSelectIndex = 0;
            //    DataGridViewComboBoxCell dgComboCell = new DataGridViewComboBoxCell();
            //    dgComboCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            //    DataTable dt = new DataTable();
            //    dt.Columns.Add("Value", typeof(string));
            //    dt.Columns.Add("Text", typeof(string));
            //    dt.Rows.Add(new Object[] { "1", "CL" });
            //    dt.Rows.Add(new Object[] { "2", "EL" });
            //    dt.Rows.Add(new Object[] { "3", "SL" });
            //    if (gvLeaveDetl.Rows[iRowIndex].Cells[gvLeaveDetl.CurrentCell.ColumnIndex].Value.ToString() != "")
            //        cmdSelectIndex = Convert.ToInt32(gvLeaveDetl.Rows[iRowIndex].Cells[gvLeaveDetl.CurrentCell.ColumnIndex].Value);
            //    //else
            //    //    cmdSelectIndex = 0;
            //    dgComboCell.DataSource = dt;
            //    dgComboCell.ValueMember = "Value";
            //    dgComboCell.DisplayMember = "Text";

            //    gvLeaveDetl.Rows[iRowIndex].Cells[gvLeaveDetl.CurrentCell.ColumnIndex] = dgComboCell;

            //    //((DataGridViewComboBoxCell)gvDCDetails.Rows[iRowIndex].Cells[gvDCDetails.CurrentCell.ColumnIndex]). = true;
            //    //bIsComboBox = true;
            //}
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToLower(e.KeyChar);
        }

        private void txtEcodeApprovBy_TextChanged(object sender, EventArgs e)
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            if (txtEcodeApprovBy.Text.Length > 0)
            {
                try
                {
                    String strCmd = "SELECT MEMBER_NAME FROM EORA_MASTER WHERE ecode=" + Convert.ToInt32(txtEcodeApprovBy.Text) + " ";
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

        private void txtEcodeApprovBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void gvODDetl_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvODDetl.Columns["Remarks"].Index)
            {
                gvODDetl.Rows[0].Cells["Remarks"].Value += "";
                txtReason.Text = gvODDetl.Rows[0].Cells["Remarks"].Value.ToString();
                for (int iVar = 0; iVar < gvODDetl.Rows.Count;iVar++ )
                {
                    gvODDetl.Rows[iVar].Cells["Remarks"].Value = gvODDetl.Rows[0].Cells["Remarks"].Value.ToString();
                }
            }
        }

      

        private void btnBalance_Click(object sender, EventArgs e)
        {
            //if(gvODDetl.Rows.Count>0)
            //{
            //    for (int i = 0; i < gvODDetl.Rows.Count;i++ )
            //    {
            //        gvODDetl.Rows[i].Cells["VisitingBranch"].Value = cmbVisitingBranch.Text;
            //        gvODDetl.Rows[i].Cells["VisitingBranchCode"].Value = cmbVisitingBranch.SelectedValue;
            //    }
            //}
        
        }
        private void FillVisitBranch()
        {
            if (gvODDetl.Rows.Count > 0)
            {
                for (int i = 0; i < gvODDetl.Rows.Count; i++)
                {
                    gvODDetl.Rows[i].Cells["VisitingBranch"].Value = cmbVisitingBranch.Text;
                    gvODDetl.Rows[i].Cells["VisitingBranchCode"].Value = cmbVisitingBranch.SelectedValue;
                }
            }
        }
        private void gvODDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex == gvODDetl.Rows[e.RowIndex].Cells["Edit"].ColumnIndex)
            {
                if (txtName.Text.Length > 0)
                {
                    string strRemarks = "",strToBeVisitBranch="";
                    if (gvODDetl.Rows[e.RowIndex].Cells["Remarks"].Value != null)
                    {
                        strRemarks = gvODDetl.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();
                       
                    }
                    if(Convert.ToString(gvODDetl.Rows[e.RowIndex].Cells["VisitingBranchCode"].Value) != "")
                    {
                         strToBeVisitBranch = gvODDetl.Rows[e.RowIndex].Cells["VisitingBranchCode"].Value.ToString();
                    }
                    AddOdDetail obj = new AddOdDetail(gvODDetl.Rows[e.RowIndex].Cells["SLNO"].Value.ToString(),
                            gvODDetl.Rows[e.RowIndex].Cells["Date"].Value.ToString(), txtBranch.Tag.ToString(), strToBeVisitBranch
                            , strRemarks, this);
                    obj.Show();
                }
                else
                {
                    MessageBox.Show("Please Enter Valid Ecode", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtEcodeSearch.Focus();
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
                    int iRes = 0;
                    string strSQL = "";
                    if (sFormType == "HO")
                    {
                        strSQL = " delete from HR_EMPLOYEE_OD_TRN_LNITEM where HEOTL_APPL_NUMBER =" + Convert.ToInt32(txtApplNo.Text) + " and heotl_loc_type='" + sFormType + "' ";
                        strSQL += " delete from HR_EMPLOYEE_OD_TRN where HEOT_APPL_NUMBER =" + Convert.ToInt32(txtApplNo.Text) + " and heot_loc_type='" + sFormType + "' ";
                    }
                    else
                    {
                        strSQL = " delete from HR_EMPLOYEE_OD_TRN_LNITEM where HEOTL_APPL_NUMBER =" + Convert.ToInt32(txtApplNo.Text) + " and " +
                            " heotl_loc_type='" + sFormType + "' and heotl_branch_code='" + CommonData.BranchCode + "'";
                        strSQL += " delete from HR_EMPLOYEE_OD_TRN where HEOT_APPL_NUMBER =" + Convert.ToInt32(txtApplNo.Text) + " and " +
                            " heot_loc_type='" + sFormType + "' and heot_branch_code='" + CommonData.BranchCode + "'";
                    }
                    objData = new SQLDB();
                    iRes = objData.ExecuteSaveData(strSQL);
                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully", "CRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                        flagUpdate = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }

   
      
    
    }
}
