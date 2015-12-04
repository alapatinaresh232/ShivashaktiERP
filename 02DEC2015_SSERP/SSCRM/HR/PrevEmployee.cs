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
namespace SSCRM
{
    public partial class PrevEmployee : Form
    {
        SQLDB objSQLDB = new SQLDB();
        int iFormType = 0;
        public PrevEmployee()
        {
            InitializeComponent();
        }
        public PrevEmployee(int iForm)
        {
            iFormType = iForm;
            InitializeComponent();
        }

        private void PrevEmployee_Load(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            DataSet ds = objSQLDB.ExecuteDataSet("SELECT CM_COMPANY_CODE, CM_COMPANY_NAME FROM COMPANY_MAS WHERE ACTIVE='T'");
            UtilityLibrary.PopulateControl(cmbCompany, ds.Tables[0].DefaultView, 1, 0, "-- Please Select --", 0);

            DataView dvDept = objSQLDB.ExecuteDataSet("SELECT DEPT_CODE, DEPT_DESC FROM DEPT_MAS").Tables[0].DefaultView;
            UtilityLibrary.PopulateControl(cmbDepartment, dvDept, 1, 0, "--PLEASE SELECT--", 0);

            string strSQL1 = "SELECT HRSM_RECRUITMENT_SOURCE_CODE, HRSM_RECRUITMENT_SOURCE_NAME from HR_RECRUITMENT_SOURCE_MASTER";
            DataTable dt1 = objSQLDB.ExecuteDataSet(strSQL1).Tables[0];
            UtilityLibrary.PopulateControl(cmbRecSour, dt1.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            objSQLDB = null;
            dtDoj.Value = Convert.ToDateTime(CommonData.CurrentDate);
            if (iFormType == 2)
            {
                this.Text = "Agent Rejoin";
                cmbCompany.SelectedValue = CommonData.CompanyCode;
                cmbDepartment.SelectedValue = "1200000";
                cmbRecSour.SelectedValue = "SRC000006";
            }
        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompany.SelectedIndex > 0)
            {
                objSQLDB = new SQLDB();
                DataSet ds = objSQLDB.ExecuteDataSet("SELECT BRANCH_CODE,BRANCH_NAME FROM BRANCH_MAS WHERE ACTIVE='T' AND COMPANY_CODE='" + cmbCompany.SelectedValue + "'");
                objSQLDB = null;
                UtilityLibrary.PopulateControl(cmbBranch, ds.Tables[0].DefaultView, 1, 0, "-- Please Select --", 0);
                if (iFormType == 2)
                {                    
                    cmbBranch.SelectedValue = CommonData.BranchCode;
                    cmbCompany.Enabled = false;
                    cmbBranch.Enabled = false;
                }
            }
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDepartment.SelectedIndex > 0)
            {
                objSQLDB = new SQLDB();
                DataView dvDesg = objSQLDB.ExecuteDataSet("SELECT DESIG_CODE,DESIG_NAME FROM DESIG_MAS WHERE DEPT_CODE=" + cmbDepartment.SelectedValue).Tables[0].DefaultView;
                UtilityLibrary.PopulateControl(cmbDesig, dvDesg, 1, 0, "--PLEASE SELECT--", 0);
                objSQLDB = null;
                if (iFormType == 2)
                {
                    cmbDesig.SelectedValue = "198";
                    cmbDepartment.Enabled = false;
                    cmbDesig.Enabled = false;
                }
            }
        }

        private void cmbRecSour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRecSour.SelectedIndex > 0)
            {
                objSQLDB = new SQLDB();
                string strReq = "SELECT HRSMD_RECRUITMENT_SOURCE_DETL_CODE,HRSMD_RECRUITMENT_SOURCE_DETL_NAME From HR_RECRUITMENT_SOURCE_MASTER_DETL WHERE HRSMD_RECRUITMENT_SOURCE_CODE='" + cmbRecSour.SelectedValue + "'";
                DataTable dtRQT = objSQLDB.ExecuteDataSet(strReq).Tables[0];
                UtilityLibrary.PopulateControl(cmbRecDetl, dtRQT.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                objSQLDB = null;
            }
        }

        private void txtPreEcode_Validated(object sender, EventArgs e)
        {
            if (txtPreEcode.Text.Length > 4)
            {
                objSQLDB = new SQLDB();
                DataSet dsData = new DataSet();
                string strReq = "SELECT MEMBER_NAME, HAMH_FORH_NAME, HAMH_DOJ, DESIG, DEPT_NAME, HAMH_LEFT_DATE, " +
                                    "HAMH_LEFT_REASON, EORA, DEPT_ID, DESG_ID, EMP_DOJ, EMP_DOB, FATHER_NAME, " +
                                    "HAMH_RECRUITMENT_SOURCE_CODE, HAMH_RECRUITMENT_SOURCE_DELT_CODE, HAMH_RECRUITMENT_SOURCE_ECODE " +
                                    "FROM HR_APPL_MASTER_HEAD A INNER JOIN EORA_MASTER B ON " +
                                    "A.HAMH_EORA_CODE=B.ECODE INNER JOIN DEPT_MAS C ON B.DEPT_ID=C.DEPT_CODE " +
                                    "WHERE ECODE=" + txtPreEcode.Text;
                try
                {
                    dsData = objSQLDB.ExecuteDataSet(strReq);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                objSQLDB = null;
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    txtPrEName.Text = dsData.Tables[0].Rows[0]["MEMBER_NAME"].ToString();
                    txtFName.Text = dsData.Tables[0].Rows[0]["HAMH_FORH_NAME"].ToString();
                    txtDoj.Text = Convert.ToDateTime(dsData.Tables[0].Rows[0]["HAMH_DOJ"]).ToString("dd/MMM/yyyy");
                    txtDesig.Text = dsData.Tables[0].Rows[0]["DESIG"].ToString();
                    txtDept.Text = dsData.Tables[0].Rows[0]["DEPT_NAME"].ToString();
                    if (dsData.Tables[0].Rows[0]["HAMH_RECRUITMENT_SOURCE_CODE"].ToString() != "")
                    {
                        cmbRecSour.SelectedValue = dsData.Tables[0].Rows[0]["HAMH_RECRUITMENT_SOURCE_CODE"].ToString();
                        
                    }
                    if (dsData.Tables[0].Rows[0]["HAMH_RECRUITMENT_SOURCE_DELT_CODE"].ToString() != "")
                    {
                        cmbRecDetl.SelectedValue = dsData.Tables[0].Rows[0]["HAMH_RECRUITMENT_SOURCE_DELT_CODE"].ToString();
                        
                    }
                    if (dsData.Tables[0].Rows[0]["HAMH_RECRUITMENT_SOURCE_ECODE"].ToString() != "")
                    {
                        txtECode.Text = dsData.Tables[0].Rows[0]["HAMH_RECRUITMENT_SOURCE_ECODE"].ToString();
                        txtECode_KeyUp(null, null);
                    }
                    else
                    {
                        txtECode.Text = txtPreEcode.Text;
                        txtECode_KeyUp(null, null);                        
                        
                    }
                    if (dsData.Tables[0].Rows[0]["HAMH_LEFT_DATE"].ToString() != "")
                        txtLefDate.Text = Convert.ToDateTime(dsData.Tables[0].Rows[0]["HAMH_LEFT_DATE"]).ToString("dd/MMM/yyyy");
                    else
                        txtLefDate.Text = "";
                    txtLefReason.Text = dsData.Tables[0].Rows[0]["HAMH_LEFT_REASON"].ToString();
                    lblEora.Text = dsData.Tables[0].Rows[0]["EORA"].ToString();
                    if (iFormType == 2)
                    {
                        cmbRecSour.Enabled = false;
                        cmbRecDetl.Enabled = false;
                        txtECode.Enabled = false;
                        if (dsData.Tables[0].Rows[0]["DEPT_ID"].ToString() != "1200000")
                        {
                            MessageBox.Show("You Can not Rejoin this employee other than Sales Department!\n " +
                                            "Please contact HR-Department for rejoin process of this employee",
                                            "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPreEcode.Text = "";
                            txtPreEcode_Validated(null, null);
                            dtDoj.Value = DateTime.Now;
                        }
                    }
                }
                else
                {
                    txtPrEName.Text = "";
                    txtFName.Text = "";
                    txtDoj.Text = "";
                    txtDesig.Text = "";
                    txtDept.Text = "";
                    txtLefDate.Text = "";
                    txtLefReason.Text = "";
                    txtECode.Text = "";
                    txtECode_KeyUp(null, null);
                }
            }
            else
            {
                //txtECode.Text = "";
                txtPrEName.Text = "";
                txtFName.Text = "";
                txtDoj.Text = "";
                txtDesig.Text = "";
                txtDept.Text = "";
                txtLefDate.Text = "";
                txtLefReason.Text = "";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPreEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtECode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtECode_KeyUp(object sender, KeyEventArgs e)
        {
            objSQLDB = new SQLDB();
            if (txtECode.Text.Length > 4)
            {
                DataTable dt = objSQLDB.ExecuteDataSet("SELECT MEMBER_NAME+' ('+DESIG+')' AS DATA FROM EORA_MASTER WHERE EORA IN ('E','A') AND ECODE=" + txtECode.Text).Tables[0];
                if (dt.Rows.Count > 0)
                    txtEName.Text = dt.Rows[0][0].ToString();
                else
                    txtEName.Text = "";
            }
            objSQLDB = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            int iretVal = 0;
            string sCodeType = "";
            string iMaxEcode = "";
            string sStatus = "";
            string eLevel = "";
            try
            {
                sStatus = objSQLDB.ExecuteDataSet("SELECT HAMH_WORKING_STATUS FROM HR_APPL_MASTER_HEAD WHERE HAMH_EORA_CODE=" + txtPreEcode.Text).Tables[0].Rows[0][0].ToString();
                
                if (sStatus == "L")
                {
                    if (iFormType == 2)
                        eLevel = objSQLDB.ExecuteDataSet("SELECT ldm_elevel_id FROM EORA_MASTER INNER JOIN LevelsDesig_mas ON LDM_DESIG_ID = DESG_ID AND ldm_company_code=company_code WHERE ECODE=" + txtPreEcode.Text).Tables[0].Rows[0][0].ToString();
                    else
                        eLevel = "100";

                    if (Convert.ToInt32(eLevel) > 85)
                    {
                        if (iFormType == 1)
                        {
                            iMaxEcode = objSQLDB.ExecuteDataSet("EXEC HR_GetEORACode 'E'").Tables[0].Rows[0][0].ToString();
                            sCodeType = "ECODE";
                        }
                        else if (iFormType == 2)
                        {

                            iMaxEcode = objSQLDB.ExecuteDataSet("EXEC HR_GetEORACode 'A'").Tables[0].Rows[0][0].ToString();
                            sCodeType = "ACODE";
                        }
                        else
                        {
                            iMaxEcode = objSQLDB.ExecuteDataSet("EXEC HR_GetEORACode 'A'").Tables[0].Rows[0][0].ToString();
                            sCodeType = "ACODE";
                        }
                        string sqlQry = "";
                        //Update Master head
                        if (iMaxEcode != "")
                        {
                            sqlQry += " INSERT INTO HR_APPL_REJOIN_HISTORY(HARH_APPL_NUMBER,HARH_PREV_ECODE,HARH_PREV_RECRUITEMENT_SOURCE_CODE,HARH_PREV_RECRUITMENT_SOURCE_DELT_CODE,HARH_PREV_RECRUITMENT_SOURCE_ECODE,HARH_PREV_DOJ,HARH_PREV_INTERVIEW_DONE_BY_ECODE,HARH_PREV_INTERVIEW_DATE," +
                                "HARH_LEFT_DATE,HARH_LEFT_REASON,HARH_LEFT_APPROVAL_ECODE,HARH_PRES_ECODE,HARH_PRES_DOJ,HARH_REMARKS,HARH_CREATED_BY,HARH_CREATED_DATE)" +
                                "SELECT HAMH_APPL_NUMBER," + txtPreEcode.Text + ",'" + cmbRecSour.SelectedValue + "','" + cmbRecDetl.SelectedValue + "'," + txtECode.Text + ",'" + Convert.ToDateTime(dtDoj.Value).ToString("dd/MMM/yyyy") + "',HAMH_INTERVIEW_DONE_BY_ECODE,HAMH_INTERVIEW_DATE,HAMH_LEFT_DATE," +
                                "HAMH_LEFT_REASON,HAMH_LEFT_APPROVAL_ECODE," + iMaxEcode + ",'" + Convert.ToDateTime(dtDoj.Value).ToString("dd/MMM/yyyy") + "','" + txtRemarks.Text + "','" + CommonData.LogUserId + "',GETDATE() FROM HR_APPL_MASTER_HEAD WHERE HAMH_EORA_CODE=" + iMaxEcode;

                            if (iFormType == 1)
                            {
                                sqlQry += " UPDATE HR_APPL_MASTER_HEAD SET HAMH_EORA_CODE=" + iMaxEcode +
                                            ", HAMH_EORA_TYPE='E',HAMH_WORKING_STATUS='A', HAMH_DOJ='" + Convert.ToDateTime(dtDoj.Value).ToString("dd/MMM/yyyy") +
                                            "', HAMH_RECRUITMENT_SOURCE_CODE='" + cmbRecSour.SelectedValue +
                                            "', HAMH_RECRUITMENT_SOURCE_DELT_CODE='" + cmbRecDetl.SelectedValue +
                                            "', HAMH_RECRUITMENT_SOURCE_ECODE=" + txtECode.Text + " WHERE HAMH_EORA_CODE=" + txtPreEcode.Text;
                                //Eora Master Insert
                                sqlQry += " INSERT INTO EORA_MASTER(BRANCH_CODE, DEPT_ID, ECODE, MEMBER_NAME" +
                                            ", EORA, HRIS_EMP_NAME, DESG_ID, DESIG, EMP_DOJ, EMP_DOB, FATHER_NAME" +
                                            ", elevel_id, company_code, edu_qualification, HRIS_DESIG_ID, HRIS_DESIG) " +
                                            "SELECT '" + cmbBranch.SelectedValue + "', '" + cmbDepartment.SelectedValue +
                                            "', " + iMaxEcode + ", MEMBER_NAME, 'E', HRIS_EMP_NAME," + cmbDesig.SelectedValue +
                                            ", '" + cmbDesig.Text + "', '" + Convert.ToDateTime(dtDoj.Value).ToString("dd/MMM/yyyy") +
                                            "', EMP_DOB, FATHER_NAME, elevel_id, '" + cmbCompany.SelectedValue +
                                            "', edu_qualification, " + cmbDesig.SelectedValue + ", '" + cmbDesig.Text +
                                            "' FROM EORA_MASTER WHERE ECODE=" + txtPreEcode.Text;
                            }
                            else if (iFormType == 2 || iFormType == 0)
                            {
                                sqlQry += " UPDATE HR_APPL_MASTER_HEAD SET HAMH_EORA_CODE=" + iMaxEcode + ", HAMH_EORA_TYPE='A',HAMH_WORKING_STATUS='P',HAMH_DOJ='" + Convert.ToDateTime(dtDoj.Value).ToString("dd/MMM/yyyy") +
                                "',HAMH_RECRUITMENT_SOURCE_CODE='" + cmbRecSour.SelectedValue + "',HAMH_RECRUITMENT_SOURCE_DELT_CODE='" + cmbRecDetl.SelectedValue + "',HAMH_RECRUITMENT_SOURCE_ECODE=" + txtECode.Text + " WHERE HAMH_EORA_CODE=" + txtPreEcode.Text;
                                //Eora Master Insert
                                sqlQry += " INSERT INTO EORA_MASTER (BRANCH_CODE,DEPT_ID,ECODE,MEMBER_NAME,EORA,HRIS_EMP_NAME,DESG_ID,DESIG,EMP_DOJ,EMP_DOB,FATHER_NAME,elevel_id,company_code,edu_qualification,HRIS_DESIG_ID,HRIS_DESIG)" +
                                "SELECT '" + cmbBranch.SelectedValue + "','" + cmbDepartment.SelectedValue + "'," + iMaxEcode + ",MEMBER_NAME,'A',HRIS_EMP_NAME," + cmbDesig.SelectedValue + ",'" + cmbDesig.Text + "','" +
                                Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + "',EMP_DOB,FATHER_NAME,elevel_id,'" + cmbCompany.SelectedValue + "',edu_qualification," + cmbDesig.SelectedValue + ",'" + cmbDesig.Text + "' FROM EORA_MASTER WHERE ECODE=" + txtPreEcode.Text;
                            }
                            //ReJoin Histroy

                        }
                        iretVal = objSQLDB.ExecuteSaveData(sqlQry);
                    }
                    else
                    {
                        MessageBox.Show("You can not rejoin this ecode, Only SRs are allowed for this screen.", "HR-Rejoin", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                else
                {
                    MessageBox.Show("Cannot Process Rejoin for this code, Please update left to Rejoin.", "HR-Rejoin", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
            }
            if (iretVal > 0)
                MessageBox.Show("Code Generated Successfully \n " + sCodeType + " : " + iMaxEcode, "SSERP Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Rejoin Process Not Completed", "SSERP Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtECode.Text = "";
            txtPreEcode_Validated(null, null);
            dtDoj.Value = DateTime.Now;
        }
    }
}
