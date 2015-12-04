using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SSCRMDB;
using SSTrans;

namespace SSCRM
{
    public partial class ServiceActivities : Form
    {
        SQLDB objSQLdb = null;
        ServiceDeptDB objServicedb = null;       
        private bool flagUpdate = false;
        private string strECode = "";
        private string sCompCode = "", sBranCode = "", sEcode = "", sActivityDate = "", strTrnNo = "", strRefNo = "";
        public EmployeeDARWithTourBills objEmployeeDARWithTourBills;

        private DataTable dtMapEmp = new DataTable();
        private DataTable dtMappedEmp = new DataTable();
      

        public DataTable dtActivityDetails = new DataTable();
       

        public ServiceActivities()
        {
            InitializeComponent();
        }

        public ServiceActivities(string Company,string BranCode,string stEcode,string strDate)
        {
            InitializeComponent();
            sCompCode = Company;
            sBranCode = BranCode;
            sEcode = stEcode;
            sActivityDate = strDate;
        }
        public ServiceActivities(string Company, string BranCode, string stEcode, string strDate, string sTrnNo, string sRefNo)
        {
            InitializeComponent();
            sCompCode = Company;
            sBranCode = BranCode;
            sEcode = stEcode;
            sActivityDate = strDate;
            strTrnNo = sTrnNo;
            strRefNo = sRefNo;
        }

        private void ServiceActivities_Load(object sender, EventArgs e)
        {

            #region "CREATE SERVICES_DAILY_ACTIVITY_DETAILS TABLE"
            dtActivityDetails.Columns.Add("SLNO");
            dtActivityDetails.Columns.Add("ActivityId");
            dtActivityDetails.Columns.Add("BranchCode");
            dtActivityDetails.Columns.Add("BranchName");
            dtActivityDetails.Columns.Add("Purpose");
            dtActivityDetails.Columns.Add("LicenceType");
            dtActivityDetails.Columns.Add("LicenceNo");
            dtActivityDetails.Columns.Add("RecruitDeptName");
            dtActivityDetails.Columns.Add("NoOfrecruitPersons");
            dtActivityDetails.Columns.Add("LegalCaseType");
            dtActivityDetails.Columns.Add("LegalCaseNo");
            dtActivityDetails.Columns.Add("Activity");
            dtActivityDetails.Columns.Add("Desc");
            dtActivityDetails.Columns.Add("Remarks");
            dtActivityDetails.Columns.Add("GCGLEcode");
            dtActivityDetails.Columns.Add("Amount");
            dtActivityDetails.Columns.Add("RelatedWork");
            #endregion


            dtpTrnDate.Value = DateTime.Today;         
            FillActivityTypes();
            cbActivityType.SelectedIndex = 0;
            FillCompanyData();
            FillBranchData();
            FillEmployeeData();
            EcodeSearch();
            if (CommonData.BranchType == "BR")
            {
                FillBranchData();
                GenerateTransactionNo();
            }
            else
            {
                cbBranch.SelectedIndex = 0;
            }

           
            if (sCompCode.Length > 0 && sBranCode.Length > 0 && sEcode.Length > 0 && sActivityDate.Length > 0)
            {
                cbCompany.SelectedValue = sCompCode;
                cbBranch.SelectedValue = sBranCode;
                txtEcodeSearch.Text = sEcode;
                if(sEcode.Length>1)
                txtEcodeSearch_TextChanged(null,null);
                cbEcode.SelectedValue = sEcode;
                dtpTrnDate.Value = Convert.ToDateTime(sActivityDate);
                dtpTrnDate.Enabled = false;
                cbEcode.Enabled = false;
                txtEcodeSearch.ReadOnly = true;
                txtTrnNo.ReadOnly = true;                
                GenerateTransactionNo();
                if (strTrnNo.Length == 0)
                    txtTrnNo.CausesValidation = false;
            }
            else
            {
                txtTrnNo.ReadOnly = false;
                dtpTrnDate.Enabled = true;
                cbEcode.Enabled = true;
                txtEcodeSearch.ReadOnly = false;
                txtTrnNo.CausesValidation = true;
            }
            if (strTrnNo.Length > 0)
            {
                txtTrnNo.Text = strTrnNo;
                txtTrnNo_Validated(null, null);
            }

           
        }
        private DataTable dtActivityType()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));
       
            table.Rows.Add("xx", "Select Activity Type");           
            table.Rows.Add("BR VISITS",       "BRANCH VISIT");
            table.Rows.Add("CAMP VISITS",     "CAMP VISIT");
            table.Rows.Add("SP VISITS",       "STOCK POINT VISIT");            
            table.Rows.Add("RECRUITMENT",     "RECRUITMENT");
            table.Rows.Add("PU VISITS",       "PRODUCTION UNIT VISIT");
            table.Rows.Add("LEGAL",           "LEGAL CASES");
            table.Rows.Add("LICENCE",         "LICENCE WORK");
            table.Rows.Add("FIELD SUPPORT",   "FIELD SUPPORT");
            table.Rows.Add("PROBLEM SOLVING", "PROBLEM SOLVING");
            table.Rows.Add("OS COLLECTION",   "OS COLLECTION");
            table.Rows.Add("STOCKREPORT SUBMISSION", "STOCK REPORT SUBMISSION");
            table.Rows.Add("PRESS COVERAGE",  "PRESS COVERAGE");
            table.Rows.Add("TENDER WORK",    "TENDER WORK");           
            table.Rows.Add("DOCUMENTATION", "DOCUMENTATION");
            table.Rows.Add("SEMINORS AND EXHIBITIONS", "SEMINORS AND EXHIBITIONS");
            table.Rows.Add("ADVANCE VERIFICATION", "ADVANCE VERIFICATION");
            table.Rows.Add("OTHERS", "OTHERS");

            
            return table;
        }

        public void FillActivityTypes()
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            DataTable dt = new DataTable();
            try
            {
                strCmd = "SELECT Distinct SOAM_ACTIVITY_NAME ActivityName "+
                         ",SOAM_ACTIVITY_DESC ActivityDesc "+
                         " FROM SERVICES_OTHER_ACTIVITY_MASTER";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    row[1] = "--Select--";
                    dt.Rows.InsertAt(row, 0);

                    cbActivityType.DataSource = dt;
                    cbActivityType.DisplayMember = "ActivityDesc";
                    cbActivityType.ValueMember = "ActivityName";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
      

        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbBranch.DataSource = null;
            try
            {
                string strCmd = "SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                   " FROM USER_BRANCH " +
                                   " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                   " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                   " WHERE UB_USER_ID ='" + CommonData.LogUserId +
                                   "' ORDER BY CM_COMPANY_NAME";

                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
                }

                cbCompany.SelectedValue = CommonData.CompanyCode;
                dt = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;

            }
        }

        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbBranch.DataSource = null;
            string BranCode = "";
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {

                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE+'@'+ STATE_CODE AS BranCode " +
                                         " FROM USER_BRANCH " +
                                         " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                         " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                         "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                         "' and BRANCH_TYPE IN ('BR','HO') ORDER BY BRANCH_NAME ASC";
                    
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr,0);

                    cbBranch.DataSource = dt;
                    cbBranch.DisplayMember = "BRANCH_NAME";
                    cbBranch.ValueMember = "BranCode";
                }

                BranCode = CommonData.BranchCode + '@' + CommonData.StateCode;
                cbBranch.SelectedValue = BranCode;               
          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                dt = null;
               
            }
        }

        private void GenerateTransactionNo()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            if (cbBranch.SelectedIndex > 0)
            {
                try
                {
                    string[] BranCode = null;

                    BranCode = cbBranch.SelectedValue.ToString().Split('@');
                    string finyear = CommonData.FinancialYear.Substring(2, 2) + CommonData.FinancialYear.Substring(7, 2);
                    string strNewNo = BranCode[0] + finyear + "DAR-";

                    string strCommand = "SELECT ISNULL(MAX(SUBSTRING(ISNULL(SDH_TRN_NUMBER, '" + strNewNo + "'),18,22)),0) + 1 " +
                                        " FROM SERVICES_DAR_HEAD " +
                                        " WHERE SDH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "' AND SDH_BRANCH_CODE='" + BranCode[0] + "' ";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtTrnNo.Text = strNewNo + Convert.ToInt32(dt.Rows[0][0]).ToString("000000");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLdb = null;
                    dt = null;                   
                }
            }
        }

        private void FillEmployeeData()
        {
            objServicedb = new ServiceDeptDB();
            DataSet dsEmp = null;
            try
            {
                //string[] strBranCode = cbBranches.SelectedValue.ToString().Split('@');
                Cursor.Current = Cursors.WaitCursor;
                cbEcode.DataSource = null;
                cbEcode.Items.Clear();

                dsEmp = objServicedb.Get_EcodesforService();
                DataTable dtEmp = dsEmp.Tables[0];
                dtMapEmp = dsEmp.Tables[0];
               
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

        private void EcodeSearch()
        {
            objServicedb = new ServiceDeptDB();
            DataSet dsEmp = null;
            if (cbCompany.SelectedIndex > 0 && cbBranch.SelectedIndex > 0)
            {
                try
                {
                    string[] strBranCode = cbBranch.SelectedValue.ToString().Split('@');
                    Cursor.Current = Cursors.WaitCursor;
                    cbEcode.DataSource = null;
                    cbEcode.Items.Clear();
                    dsEmp = objServicedb.ServiceLevelEcodeSearch_Get(cbCompany.SelectedValue.ToString(), strBranCode[0], CommonData.DocMonth, txtEcodeSearch.Text.ToString());
                    DataTable dtEmp = dsEmp.Tables[0];
                    dtMappedEmp = dsEmp.Tables[0];

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


        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchData();
                cbBranch.SelectedIndex = 0;
            }
           

        }
        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranch.SelectedIndex > 0)
            {
                GenerateTransactionNo();
                EcodeSearch();
                dtActivityDetails.Rows.Clear();
                gvActivityDetails.Rows.Clear();
            }
            else
            {
                dtActivityDetails.Rows.Clear();
                gvActivityDetails.Rows.Clear();
            }
        }
        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            EcodeSearch();
        }


        private bool CheckData()
        {
            bool bFlag = true;

            if (cbCompany.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Company", "Services Daily Activities", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbCompany.Focus();
                return bFlag;
            }
            if (cbBranch.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Branch", "Services Daily Activities", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbBranch.Focus();
                return bFlag;
            }
            if (cbEcode.SelectedIndex == -1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Conducted Employee Name", "Services Daily Activities", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbEcode.Focus();
                return bFlag;
            }
            if (gvActivityDetails.Rows.Count == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Add Activity Details", "Services Daily Activities", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bFlag;
            }
            if (gvActivityDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvActivityDetails.Rows.Count; i++)
                {
                    if (Convert.ToString(gvActivityDetails.Rows[i].Cells["LicenceNo"].Value) == "")
                    {
                        gvActivityDetails.Rows[i].Cells["LicenceNo"].Value = "";
                    }
                    if (gvActivityDetails.Rows[i].Cells["LicenceType"].Value.ToString() == "")
                    {
                        gvActivityDetails.Rows[i].Cells["LicenceType"].Value = "";
                    }
                    if (gvActivityDetails.Rows[i].Cells["LegalCaseNo"].Value.ToString() == "")
                    {
                        gvActivityDetails.Rows[i].Cells["LegalCaseNo"].Value = "";
                    }
                    if (gvActivityDetails.Rows[i].Cells["LegalCaseType"].Value.ToString() == "")
                    {
                        gvActivityDetails.Rows[i].Cells["LegalCaseType"].Value = "";
                    }
                    if (gvActivityDetails.Rows[i].Cells["RecruitDeptName"].Value.ToString() == "")
                    {
                        gvActivityDetails.Rows[i].Cells["RecruitDeptName"].Value = "";
                    }
                    if (gvActivityDetails.Rows[i].Cells["NoOfrecruitPersons"].Value.ToString() == "")
                    {
                        gvActivityDetails.Rows[i].Cells["NoOfrecruitPersons"].Value = 0;
                    }
                    if (gvActivityDetails.Rows[i].Cells["BranchCode"].Value.ToString() == "")
                    {
                        gvActivityDetails.Rows[i].Cells["BranchCode"].Value = "";
                    }
                    if (gvActivityDetails.Rows[i].Cells["Purpose"].Value.ToString() == "")
                    {
                        gvActivityDetails.Rows[i].Cells["Purpose"].Value = "";
                    }
                }


            }
                     
            return bFlag;
        }

        #region "SAVE AND UPDATE DATA"
        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;

            if (CheckData() == true)
            {
                if (SaveDARHeadDetails() > 0)
                {
                    if (SaveServicesDARDetl() > 0)
                    {
                        MessageBox.Show("Data Saved Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                        flagUpdate = false;
                        GenerateTransactionNo();
                        if (sEcode.Length>0 && sActivityDate.Length > 0)
                        {
                            objEmployeeDARWithTourBills.FillEmployeeActivityDetails(Convert.ToInt32(sEcode), sActivityDate);
                            this.Close();
                            this.Dispose();
                        }
                    }
                    else
                    {
                        string strCmd = "DELETE FROM SERVICES_DAR_HEAD WHERE SDH_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                        iRes = objSQLdb.ExecuteSaveData(strCmd);                       
                        MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {                   
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
            }
        }
        private int SaveDARHeadDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";

            try
            {
                string[] strBranCode = cbBranch.SelectedValue.ToString().Split('@');

                strCommand = "DELETE FROM SERVICES_DAR_DETL WHERE SDD_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";

                if (flagUpdate == true)
                {
                    strCommand += " UPDATE SERVICES_DAR_HEAD SET SDH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                  "',SDH_BRANCH_CODE='" + strBranCode[0] +
                                  "',SDH_STATE_CODE='" + strBranCode[1] + "',SDH_TRN_TYPE='DAR' " +
                                  ",SDH_TRN_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                  "',SDH_DOC_MONTH='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                  "',SDH_AUTHORIZED_BY='',SDH_ECODE=" + Convert.ToInt32(cbEcode.SelectedValue) +
                                  ", SDH_LAST_MODIFIED_BY='" + CommonData.LogUserId + "',SDH_LAST_MODIFIED_DATE=getdate() " +
                                  " WHERE SDH_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                    flagUpdate = false;
                }
                else
                {
                    GenerateTransactionNo();
                    objSQLdb = new SQLDB();

                    strCommand = "INSERT INTO SERVICES_DAR_HEAD(SDH_COMPANY_CODE " +
                                                                   ", SDH_STATE_CODE " +
                                                                   ", SDH_BRANCH_CODE " +
                                                                   ", SDH_DOC_MONTH " +
                                                                   ", SDH_TRN_TYPE " +
                                                                   ", SDH_TRN_NUMBER " +
                                                                   ", SDH_TRN_DATE " +
                                                                   ", SDH_ECODE " +
                                                                   ", SDH_AUTHORIZED_BY " +
                                                                   ", SDH_CREATED_BY " +
                                                                   ", SDH_CREATED_DATE " +
                                                                   "  )VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                   "','" + strBranCode[1] +
                                                                   "','" + strBranCode[0] +
                                                                   "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                   "','DAR','" + txtTrnNo.Text.ToString() +
                                                                   "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                                                   "', " + Convert.ToInt32(cbEcode.SelectedValue) +
                                                                   ",'','" + CommonData.LogUserId +
                                                                   "',getdate())";
                }
                if (strCommand.Length > 10)
                {
                    iRes = objSQLdb.ExecuteSaveData(strCommand);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
            return iRes;
        }

        private int SaveServicesDARDetl()
        {
            objSQLdb = new SQLDB();
            int iRes1 = 0;
            string strCommand = "";           
           
            try
            {                
                string[] strBranCode = cbBranch.SelectedValue.ToString().Split('@');

                if (gvActivityDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvActivityDetails.Rows.Count; i++)
                    {
                        strCommand += "INSERT INTO SERVICES_DAR_DETL(SDD_COMPANY_CODE " +
                                                                            ", SDD_STATE_CODE " +
                                                                            ", SDD_BRANCH_CODE " +
                                                                            ", SDD_DOC_MONTH " +
                                                                            ", SDD_TRN_TYPE " +
                                                                            ", SDD_TRN_NUMBER " +
                                                                            ", SDD_DAILY_ACTIVITY " +
                                                                            ", SDD_SL_NO " +
                                                                            ", SDD_VISIT_BRANCH_CODE " +
                                                                            ", SDD_PURPOSE " +
                                                                            ", SDD_LEGAL_LICENCE_NUMBER " +
                                                                            ", SDD_LEGAL_LICENCE_TYPE " +
                                                                            ", SDD_LEGAL_CASE_NUMBER " +
                                                                            ", SDD_LEGAL_CASE_TYPE " +
                                                                            ", SDD_RECRU_DEPT_FOR " +
                                                                            ", SDD_RECRU_NOE " +
                                                                            ", SDD_ACTIVITY_REMARKS " +
                                                                            ", SDD_GC_GL_ECODE " +
                                                                            ", SDD_AMOUNT " +
                                                                            ", SDD_RELATED_WORK " +
                                                                            ", SDD_AUTHORIZED_BY " +
                                                                            ", SDD_CREATED_BY " +
                                                                            ", SDD_CREATED_DATE " +
                                                                            ")VALUES(" + " '" + cbCompany.SelectedValue.ToString() +
                                                                            "','" + strBranCode[1] +
                                                                            "','" + strBranCode[0] +
                                                                            "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                            "','DAR','" + txtTrnNo.Text.ToString() +
                                                                            "','" + gvActivityDetails.Rows[i].Cells["ActivityId"].Value.ToString() +
                                                                            "'," + Convert.ToInt32(gvActivityDetails.Rows[i].Cells["SLNO"].Value) +
                                                                            ",'" + gvActivityDetails.Rows[i].Cells["BranchCode"].Value.ToString() +
                                                                            "','" + gvActivityDetails.Rows[i].Cells["Purpose"].Value.ToString() +
                                                                            "','" + gvActivityDetails.Rows[i].Cells["LicenceNo"].Value.ToString() +
                                                                            "','" + gvActivityDetails.Rows[i].Cells["LicenceType"].Value.ToString() +
                                                                            "','" + gvActivityDetails.Rows[i].Cells["LegalCaseNo"].Value.ToString() +
                                                                            "','" + gvActivityDetails.Rows[i].Cells["LegalCaseType"].Value.ToString() +
                                                                            "','" + gvActivityDetails.Rows[i].Cells["RecruitDeptName"].Value.ToString() +
                                                                            "','" + gvActivityDetails.Rows[i].Cells["NoOfrecruitPersons"].Value.ToString() +
                                                                            "','" + gvActivityDetails.Rows[i].Cells["Remarks"].Value.ToString() +
                                                                            "'," + Convert.ToInt32(gvActivityDetails.Rows[i].Cells["GcGLEcode"].Value) +
                                                                            "," + Convert.ToDouble(gvActivityDetails.Rows[i].Cells["Amount"].Value) +
                                                                            ",'" + gvActivityDetails.Rows[i].Cells["RelatedWork"].Value.ToString() +
                                                                            "','','" + CommonData.LogUserId +
                                                                            "',getdate())";


                    }
                }

                if (strCommand.Length > 10)
                {
                    iRes1 = objSQLdb.ExecuteSaveData(strCommand);
                }
       

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes1;
        }

        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbActivityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbActivityType.Text == "STOCK POINT VISIT")
            //{                
            //    cbActivityType.Tag = "SP VISITS";
            //}
            //else if (cbActivityType.Text == "BRANCH VISIT")
            //{                
            //    cbActivityType.Tag = "BR VISITS";
            //}
            //else if (cbActivityType.Text == "LICENCE WORK")
            //{                           
            //    cbActivityType.Tag = "LICENCE";
            //}
            //else if (cbActivityType.Text == "RECRUITMENT")
            //{
            //    cbActivityType.Tag = "RECRUITMENT";
            //}
            //else if (cbActivityType.Text == "LEGAL CASES")
            //{               
            //    cbActivityType.Tag = "LEGAL";
            //}
            //else if (cbActivityType.Text == "PRODUCTION UNIT VISIT")
            //{
            //    cbActivityType.Tag = "PU VISITS";
            //}
            //else if (cbActivityType.Text == "CAMP VISIT")
            //{
            //    cbActivityType.Tag = "CAMP VISITS";
            //}
            //else if (cbActivityType.Text == "OTHERS")
            //{
            //    cbActivityType.Tag = "OTHERS";
            //}           
            //else if (cbActivityType.Text == "FIELD SUPPORT")
            //{
            //    cbActivityType.Tag = "FIELD SUPPORT";
            //}
            //else if (cbActivityType.Text == "PROBLEM SOLVING")
            //{
            //    cbActivityType.Tag = "PROBLEM SOLVING";
            //}
            //else if (cbActivityType.Text == "OS COLLECTION")
            //{
            //    cbActivityType.Tag = "OS COLLECTION";
            //}
            //else if (cbActivityType.Text == "STOCK REPORT SUBMISSION")
            //{
            //    cbActivityType.Tag = "STOCK REPORT SUBMISSION";
            //}
            //else if (cbActivityType.Text == "PRESS COVERAGE")
            //{
            //    cbActivityType.Tag = "PRESS COVERAGE";
            //}
            //else if (cbActivityType.Text == "TENDER WORK")
            //{
            //    cbActivityType.Tag = "TENDER WORK";
            //}
           
        }   


        #region "GET DATA FOR UPDATE"
        private void GetServicesDARDetails()
        {
            objServicedb = new ServiceDeptDB();
            Hashtable ht;
            DataTable dtServiceDarHead;

            if (txtTrnNo.Text.Length > 20)
            {
                try
                {

                    ht = objServicedb.GetServiceDailyActivityDetails(txtTrnNo.Text.ToString());
                    dtServiceDarHead = (DataTable)ht["ServicesDARHead"];

                    FillServicesDARHeadDetails(dtServiceDarHead);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objServicedb = null;
                    ht = null;
                }
            }
            else
            {
                flagUpdate = false;
                //cbCompany.SelectedIndex = 0;
                //cbBranch.SelectedIndex = -1;
                GenerateTransactionNo();
                dtpTrnDate.Value = DateTime.Today;
                cbEcode.SelectedIndex = -1;
                txtEcodeSearch.Text = "";
                cbActivityType.SelectedIndex = 1;
                dtpTrnDate.Value = DateTime.Today;
                dtActivityDetails.Rows.Clear();
                gvActivityDetails.Rows.Clear();
                EcodeSearch();
            }
         

        }

        private void FillServicesDARHeadDetails(DataTable dtHead)
        {
            objServicedb = new ServiceDeptDB();
            Hashtable ht;
            DataTable dtDARDetl;
            try
            {

                ht = objServicedb.GetServiceDailyActivityDetails(txtTrnNo.Text.ToString());

                dtDARDetl = (DataTable)ht["ServicesDARDetl"];

                if (dtHead.Rows.Count > 0)
                {
                    flagUpdate = true;

                    string stECode = dtHead.Rows[0]["Ecode"] + "";
                    cbCompany.SelectedValue = dtHead.Rows[0]["CompCode"].ToString(); 
                    cbBranch.SelectedValue = dtHead.Rows[0]["BranCode"].ToString();                    
                    dtpTrnDate.Value = Convert.ToDateTime(dtHead.Rows[0]["TrnDate"]);
                    txtEcodeSearch.Text = stECode;
                    cbEcode.SelectedValue = stECode;

                    FillServicesDARDetl(dtDARDetl);
                }

                else if (dtHead.Rows.Count == 0)
                {
                    flagUpdate = false;
                    //cbCompany.SelectedIndex = 0;
                    //cbBranch.SelectedIndex = -1;
                    GenerateTransactionNo();
                    dtpTrnDate.Value = DateTime.Today;
                    cbEcode.SelectedIndex = -1;
                    txtEcodeSearch.Text = "";
                    cbActivityType.SelectedIndex = 1;
                    dtpTrnDate.Value = DateTime.Today;
                    dtActivityDetails.Rows.Clear();
                    gvActivityDetails.Rows.Clear();
                    EcodeSearch();

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }            
        

        private void FillServicesDARDetl(DataTable dtDetl)
        {
            string strActivity = "";
            string strActivityDesc = "";
            string strBranchName = "";
            dtActivityDetails.Rows.Clear();

            try
            {
                if (dtDetl.Rows.Count > 0)
                {                  

                    for (int i = 0; i < dtDetl.Rows.Count; i++)
                    {
                        if (dtDetl.Rows[i]["Activity"].ToString() == "SP VISITS")
                        {
                            strActivity = "STOCK POINT VISITS";
                            strActivityDesc = "Stock Point Name" + '-' + dtDetl.Rows[i]["BranchName"].ToString();
                        }
                        if (dtDetl.Rows[i]["Activity"].ToString() == "BR VISITS")
                        {
                            strActivity = "BRANCH VISITS";
                            strActivityDesc = "Branch Name" + '-' + dtDetl.Rows[i]["BranchName"].ToString();
                        }

                        if (dtDetl.Rows[i]["Activity"].ToString() == "PU VISITS")
                        {
                            strActivity = "PRODUCTION UNIT VISITS";
                            strActivityDesc = "Production Unit Name" + '-' + dtDetl.Rows[i]["BranchName"].ToString();
                        }
                        if (dtDetl.Rows[i]["Activity"].ToString() == "LEGAL")
                        {
                            strActivity = "LEGAL CASE WORK";
                            strActivityDesc = dtDetl.Rows[i]["LegalCaseType"].ToString() + '-' + dtDetl.Rows[i]["LegalCaseNo"].ToString();
                        }
                        if (dtDetl.Rows[i]["Activity"].ToString() == "LICENCE")
                        {
                            strActivity = "LICENCE WORK";
                            strActivityDesc = dtDetl.Rows[i]["LicenceType"].ToString() + '-' + dtDetl.Rows[i]["LicenceNo"].ToString();
                        }
                        if (dtDetl.Rows[i]["Activity"].ToString() == "RECRUITMENT")
                        {
                            strActivity = "RECRUITMENT";
                            strActivityDesc = dtDetl.Rows[i]["RecruitDept"].ToString();
                        }
                        if (dtDetl.Rows[i]["Activity"].ToString() == "CAMP VISITS")
                        {
                            strActivity = "CAMP VISIT";
                            strActivityDesc = "Camp Name" + '-' + dtDetl.Rows[i]["CampName"].ToString();
                        }
                        if (dtDetl.Rows[i]["Activity"].ToString() == "OTHERS")
                        {
                            strActivity = "OTHER WORKS";
                            strActivityDesc = "Other Works" + '-' + dtDetl.Rows[i]["VisitPurpose"].ToString();
                        }
                  
                        if (dtDetl.Rows[i]["Activity"].ToString() == "FIELD SUPPORT")
                        {
                            strActivity = "FIELD SUPPORT";
                            strActivityDesc = "Field Support Given To" + '-' + dtDetl.Rows[i]["EmpName"].ToString();
                        }
                        if (dtDetl.Rows[i]["Activity"].ToString() == "PROBLEM SOLVING")
                        {
                            strActivity = "PROBLEM SOLVING";
                            strActivityDesc = dtDetl.Rows[i]["WorkRelated"].ToString()+'-'+"Problem Solved";
                        }
                        if (dtDetl.Rows[i]["Activity"].ToString() == "OS COLLECTION")
                        {
                            strActivity = "OS COLLECTION";
                            strActivityDesc = dtDetl.Rows[i]["Amount"].ToString() + '-' + "Amount Collection of" + '(' + dtDetl.Rows[i]["EmpName"].ToString()+')';
                        }
                        if (dtDetl.Rows[i]["Activity"].ToString() == "STOCK REPORT SUBMISSION")
                        {
                            strActivity = "STOCK REPORT SUBMISSION";
                            strActivityDesc = "Product Licence Stock Report Submission of" + '-' + dtDetl.Rows[i]["LicenceNo"].ToString() + ')';
                        }

                        if (dtDetl.Rows[i]["Activity"].ToString() == "PRESS COVERAGE")
                        {
                            strActivity = "PRESS COVERAGE";
                            strActivityDesc = "Press Coverage Of"+'-'+dtDetl.Rows[i]["WorkRelated"].ToString();
                        }

                        if (dtDetl.Rows[i]["Activity"].ToString() == "TENDER WORK")
                        {
                            strActivity = "TENDER WORK";
                            strActivityDesc ="Tender Work Of" +'-'+ dtDetl.Rows[i]["WorkRelated"].ToString();
                        }
                        if (dtDetl.Rows[i]["Activity"].ToString() == "DOCUMENTATION")
                        {
                            strActivity = "DOCUMENTATION";
                            strActivityDesc = "DOCUMENTATION" + '-' + dtDetl.Rows[i]["VisitPurpose"].ToString();
                        }
                        if (dtDetl.Rows[i]["Activity"].ToString() == "ADVANCE VERIFICATION")
                        {
                            strActivity = "ADVANCE VERIFICATION";
                            strActivityDesc = "ADVANCE VERIFICATION" + '-' + dtDetl.Rows[i]["VisitPurpose"].ToString();
                        }
                        if (dtDetl.Rows[i]["Activity"].ToString() == "SEMINORS AND EXHIBITIONS")
                        {
                            strActivity = "SEMINORS AND EXHIBITIONS";
                            strActivityDesc = "SEMINORS AND EXHIBITIONS" + '-' + dtDetl.Rows[i]["VisitPurpose"].ToString();
                        }
                        else
                        {
                            strActivity = dtDetl.Rows[i]["Activity"].ToString();
                            strActivityDesc = dtDetl.Rows[i]["Activity"].ToString() + '-' + dtDetl.Rows[i]["VisitPurpose"].ToString();
                        }
                        if (dtDetl.Rows[i]["BranchName"].ToString() == "")
                        {
                            strBranchName = dtDetl.Rows[i]["CampName"].ToString();
                        }
                        else
                        {
                            strBranchName = dtDetl.Rows[i]["BranchName"].ToString();
                        }
                        


                        dtActivityDetails.Rows.Add(new Object[] {"-1",dtDetl.Rows[i]["Activity"].ToString(),
                                                                       dtDetl.Rows[i]["BranCode"].ToString(),
                                                                       strBranchName,                                                                      
                                                                       dtDetl.Rows[i]["VisitPurpose"].ToString(),
                                                                       dtDetl.Rows[i]["LicenceType"].ToString(),
                                                                       dtDetl.Rows[i]["LicenceNo"].ToString(),
                                                                       dtDetl.Rows[i]["RecruitDept"].ToString(),
                                                                       dtDetl.Rows[i]["NoOfEmployees"].ToString(),
                                                                       dtDetl.Rows[i]["LegalCaseType"].ToString(),
                                                                       dtDetl.Rows[i]["LegalCaseNo"].ToString(),
                                                                       strActivity,strActivityDesc,
                                                                       dtDetl.Rows[i]["ActivityRemarks"].ToString(),
                                                                       dtDetl.Rows[i]["GCGLEcode"].ToString(),
                                                                       dtDetl.Rows[i]["Amount"].ToString(),
                                                                       dtDetl.Rows[i]["WorkRelated"].ToString()});
                        strActivity = "";
                        strActivityDesc = "";
                        GetActivityDetails();


                    }
                }
                else if (dtDetl.Rows.Count == 0)
                {
                    GenerateTransactionNo();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            

        }
                    
        #endregion


        #region "GRIDVIEW DETAILS"

        public void GetActivityDetails()
        {
            objSQLdb = new SQLDB();
            int intRow = 1;
            gvActivityDetails.Rows.Clear();
            try
            {

                for (int i = 0; i < dtActivityDetails.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    dtActivityDetails.Rows[i]["SLNO"] = intRow;
                    tempRow.Cells.Add(cellSLNO);                                       

                    DataGridViewCell cellActivityId = new DataGridViewTextBoxCell();
                    cellActivityId.Value = dtActivityDetails.Rows[i]["ActivityId"].ToString();
                    tempRow.Cells.Add(cellActivityId);

                    DataGridViewCell cellBranchName = new DataGridViewTextBoxCell();
                    cellBranchName.Value = dtActivityDetails.Rows[i]["BranchName"];
                    tempRow.Cells.Add(cellBranchName);

                    DataGridViewCell cellBranchCode = new DataGridViewTextBoxCell();
                    cellBranchCode.Value = dtActivityDetails.Rows[i]["BranchCode"];
                    tempRow.Cells.Add(cellBranchCode);                                
                 
                    DataGridViewCell cellVisitPurpose = new DataGridViewTextBoxCell();
                    cellVisitPurpose.Value = dtActivityDetails.Rows[i]["Purpose"];
                    tempRow.Cells.Add(cellVisitPurpose);

                    DataGridViewCell cellLegalCaseType = new DataGridViewTextBoxCell();
                    cellLegalCaseType.Value = dtActivityDetails.Rows[i]["LegalCaseType"];
                    tempRow.Cells.Add(cellLegalCaseType);

                    DataGridViewCell cellLegalCaseNo = new DataGridViewTextBoxCell();
                    cellLegalCaseNo.Value = dtActivityDetails.Rows[i]["LegalCaseNo"];
                    tempRow.Cells.Add(cellLegalCaseNo);

                   
                    DataGridViewCell cellRecrDeptName = new DataGridViewTextBoxCell();
                    cellRecrDeptName.Value = dtActivityDetails.Rows[i]["RecruitDeptName"];
                    tempRow.Cells.Add(cellRecrDeptName);

                    DataGridViewCell cellNoOfRecrPersons = new DataGridViewTextBoxCell();
                    cellNoOfRecrPersons.Value = dtActivityDetails.Rows[i]["NoOfrecruitPersons"];
                    tempRow.Cells.Add(cellNoOfRecrPersons);

                    DataGridViewCell cellLicenceType = new DataGridViewTextBoxCell();
                    cellLicenceType.Value = dtActivityDetails.Rows[i]["LicenceType"];
                    tempRow.Cells.Add(cellLicenceType);

                    DataGridViewCell cellLicenceNo = new DataGridViewTextBoxCell();
                    cellLicenceNo.Value = dtActivityDetails.Rows[i]["LicenceNo"];
                    tempRow.Cells.Add(cellLicenceNo);


                    DataGridViewCell cellActivity = new DataGridViewTextBoxCell();
                    cellActivity.Value = dtActivityDetails.Rows[i]["Activity"];
                    tempRow.Cells.Add(cellActivity);

                    DataGridViewCell cellDesc = new DataGridViewTextBoxCell();
                    cellDesc.Value = dtActivityDetails.Rows[i]["Desc"];
                    tempRow.Cells.Add(cellDesc);
                    
                    DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                    cellRemarks.Value = dtActivityDetails.Rows[i]["Remarks"];
                    tempRow.Cells.Add(cellRemarks);
                                      
                    DataGridViewCell cellEcode = new DataGridViewTextBoxCell();
                    cellEcode.Value = dtActivityDetails.Rows[i]["GCGLEcode"];
                    tempRow.Cells.Add(cellEcode);

                    DataGridViewCell cellAmount = new DataGridViewTextBoxCell();
                    cellAmount.Value = dtActivityDetails.Rows[i]["Amount"];
                    tempRow.Cells.Add(cellAmount);

                    DataGridViewCell cellRelatedWork = new DataGridViewTextBoxCell();
                    cellRelatedWork.Value = dtActivityDetails.Rows[i]["RelatedWork"];
                    tempRow.Cells.Add(cellRelatedWork);

                    intRow = intRow + 1;
                    gvActivityDetails.Rows.Add(tempRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }                  

        #endregion    

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
            cbActivityType.SelectedIndex = 0;           
            GenerateTransactionNo();         
            //txtEcodeSearch.Text = "";
            //cbEcode.SelectedIndex = -1;
            //dtpTrnDate.Value = DateTime.Today;
            dtActivityDetails.Rows.Clear();
            gvActivityDetails.Rows.Clear();                      
            EcodeSearch();
            FillEmployeeData();
           
        }


        #region "EDITING AND DELETING GRID DETAILS"
        private void gvActivityDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int SlNo = 0;

                if (e.ColumnIndex == gvActivityDetails.Columns["Edit"].Index)
                {
                    if (Convert.ToBoolean(gvActivityDetails.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
                    {
                        DataRow[] dr = { };

                        string strActivityId = gvActivityDetails.Rows[e.RowIndex].Cells[gvActivityDetails.Columns["ActivityId"].Index].Value.ToString();
                        SlNo = Convert.ToInt32(gvActivityDetails.Rows[e.RowIndex].Cells[gvActivityDetails.Columns["SLNO"].Index].Value);
                        dr = dtActivityDetails.Select("SLNO=" + SlNo);
                        DataGridViewRow dgvr = gvActivityDetails.Rows[e.RowIndex];

                        if (strActivityId == "LEGAL")
                        {
                            LegalCaseDetails LegalDetails = new LegalCaseDetails(dr);
                            LegalDetails.objServiceActivities = this;
                            LegalDetails.ShowDialog();
                        }
                        else if (strActivityId == "LICENCE")
                        {                          
                            LicenceRelatedDetails LicenceDetails = new LicenceRelatedDetails(dr, "LICENCE");
                            LicenceDetails.objServiceActivities = this;
                            LicenceDetails.ShowDialog();
                        }
                        else if (strActivityId == "SP VISITS")
                        {                          
                            VisitStockPointDetails VSPDetails = new VisitStockPointDetails(dr);
                            VSPDetails.objServiceActivities = this;
                            VSPDetails.ShowDialog();
                        }
                        else if (strActivityId == "BR VISITS")
                        {
                            VisitBranchDetails BranchDetails = new VisitBranchDetails(dr);
                            BranchDetails.objServiceActivities = this;
                            BranchDetails.ShowDialog();
                        }
                        else if (strActivityId == "RECRUITMENT")
                        {
                            
                            RecruitmentDetails RecruitDetails = new RecruitmentDetails(dr);
                            RecruitDetails.objServiceActivities = this;
                            RecruitDetails.ShowDialog();
                        }
                        else if (strActivityId == "PU VISITS")
                        {
                            VisitPUDetails PuDetails = new VisitPUDetails(dr);
                            PuDetails.objServiceActivities = this;
                            PuDetails.ShowDialog();
                        }
                        else if (strActivityId == "CAMP VISITS")
                        {
                            CampVisitDetails CampDetails = new CampVisitDetails(dr);
                            CampDetails.objServiceActivities = this;
                            CampDetails.ShowDialog();
                        }                        
                        else if (strActivityId == "OS COLLECTION")
                        {
                            frmOsCollection OsColl = new frmOsCollection(dr);
                            OsColl.objServiceActivities = this;
                            OsColl.ShowDialog();
                        }
                        else if (strActivityId == "PROBLEM SOLVING")
                        {
                            frmProblemSolving ProbSolving = new frmProblemSolving(dr);
                            ProbSolving.objServiceActivities = this;
                            ProbSolving.ShowDialog();
                        }
                        else if (strActivityId == "FIELD SUPPORT")
                        {
                            frmFieldSupport fieldSupport = new frmFieldSupport(dr);
                            fieldSupport.objServiceActivities = this;
                            fieldSupport.ShowDialog();

                        }
                        else if (strActivityId == "PRESS COVERAGE")
                        {
                            PressCoverage pressCovr = new PressCoverage(dr);
                            pressCovr.objServiceActivities = this;
                            pressCovr.ShowDialog();
                        }
                        else if (strActivityId == "TENDER WORK")
                        {
                            frmTenderWork tender = new frmTenderWork(dr);
                            tender.objServiceActivities = this;
                            tender.ShowDialog();
                        }
                        else if (strActivityId == "STOCK REPORT SUBMISSION")
                        {
                            LicenceRelatedDetails licenceDetl = new LicenceRelatedDetails(dr,"STOCK REPORT SUBMISSION");
                            licenceDetl.objServiceActivities = this;
                            licenceDetl.ShowDialog();
                        }
                        else if (strActivityId == "SEMINORS AND EXHIBITIONS")
                        {
                            OtherWorks OthersDetails = new OtherWorks(dr, "SEMINORS","");
                            OthersDetails.objServiceActivities = this;
                            OthersDetails.ShowDialog();
                        }
                        else if (strActivityId == "ADVANCE VERIFICATION")
                        {
                            OtherWorks OthersDetails = new OtherWorks(dr, "ADV_VERIFICATION","");
                            OthersDetails.objServiceActivities = this;
                            OthersDetails.ShowDialog();
                        }
                        else if (strActivityId == "DOCUMENTATION")
                        {
                            OtherWorks OthersDetails = new OtherWorks(dr, "DOCUMENTATION","");
                            OthersDetails.objServiceActivities = this;
                            OthersDetails.ShowDialog();
                        }
                        else if (strActivityId == "OTHERS")
                        {
                            OtherWorks OthersDetails = new OtherWorks(dr, "OTHERS","");
                            OthersDetails.objServiceActivities = this;
                            OthersDetails.ShowDialog();
                        }
                        else
                        {
                            OtherWorks OthersDetails = new OtherWorks(dr,"",strActivityId);
                            OthersDetails.objServiceActivities = this;
                            OthersDetails.ShowDialog();
                        }
                    }

                }

                else if (e.ColumnIndex == gvActivityDetails.Columns["Del"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        SlNo = Convert.ToInt32(gvActivityDetails.Rows[e.RowIndex].Cells[gvActivityDetails.Columns["SLNO"].Index].Value);
                        DataRow[] dr = dtActivityDetails.Select("SLNO=" + SlNo);
                        dtActivityDetails.Rows.Remove(dr[0]);
                        GetActivityDetails();
                        MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }

        }
        #endregion


        private void btnAddActivity_Click(object sender, EventArgs e)
        {
            //if (dtMapEmp.Rows.Count > 0 || dtMappedEmp.Rows.Count > 0)
            //{
                if (cbActivityType.SelectedIndex > 0)
                {

                    if (cbActivityType.Text == "STOCK POINT VISIT")
                    {
                        VisitStockPointDetails SPDetails = new VisitStockPointDetails();
                        SPDetails.objServiceActivities = this;
                        SPDetails.ShowDialog();
                        
                    }
                    else if (cbActivityType.Text == "BRANCH VISIT")
                    {
                        VisitBranchDetails BranchDetails = new VisitBranchDetails();
                        BranchDetails.objServiceActivities = this;
                        BranchDetails.ShowDialog();
                       
                    }
                    else if (cbActivityType.Text == "LICENCE WORK")
                    {
                        LicenceRelatedDetails licenceDetl = new LicenceRelatedDetails("LICENCE");
                        licenceDetl.objServiceActivities = this;
                        licenceDetl.ShowDialog();
                    }
                    else if (cbActivityType.Text == "RECRUITMENT")
                    {
                        RecruitmentDetails RecDetl = new RecruitmentDetails();
                        RecDetl.objServiceActivities = this;
                        RecDetl.ShowDialog();

                    }
                    else if (cbActivityType.Text == "LEGAL CASES")
                    {
                        LegalCaseDetails LegalDetl = new LegalCaseDetails();
                        LegalDetl.objServiceActivities = this;
                        LegalDetl.ShowDialog();
                    }
                    else if (cbActivityType.Text == "PRODUCTION UNIT VISIT")
                    {
                        VisitPUDetails PUDetl = new VisitPUDetails();
                        PUDetl.objServiceActivities = this;
                        PUDetl.ShowDialog();
                    }
                    else if (cbActivityType.Text == "CAMP VISIT")
                    {
                        CampVisitDetails CampDetails = new CampVisitDetails();
                        CampDetails.objServiceActivities = this;
                        CampDetails.ShowDialog();
                    }                    
                    else if (cbActivityType.Text == "OS COLLECTION")
                    {
                        frmOsCollection OsColl = new frmOsCollection();
                        OsColl.objServiceActivities = this;
                        OsColl.ShowDialog();

                    }
                    else if (cbActivityType.Text == "PROBLEM SOLVING")
                    {
                        frmProblemSolving ProbSolving = new frmProblemSolving();
                        ProbSolving.objServiceActivities = this;
                        ProbSolving.ShowDialog();
                    }
                    else if (cbActivityType.Text == "FIELD SUPPORT")
                    {
                        frmFieldSupport fieldSupport = new frmFieldSupport();
                        fieldSupport.objServiceActivities = this;
                        fieldSupport.ShowDialog();
                    }
                    else if (cbActivityType.Text == "PRESS COVERAGE")
                    {
                        PressCoverage pressCovr = new PressCoverage();
                        pressCovr.objServiceActivities = this;
                        pressCovr.ShowDialog();
                    }
                    else if (cbActivityType.Text == "TENDER WORK")
                    {
                        frmTenderWork tender = new frmTenderWork();
                        tender.objServiceActivities = this;
                        tender.ShowDialog();
                    }
                    else if (cbActivityType.Text == "STOCK REPORT SUBMISSION")
                    {
                        LicenceRelatedDetails licenceDetl = new LicenceRelatedDetails("STOCK REPORT SUBMISSION");
                        licenceDetl.objServiceActivities = this;
                        licenceDetl.ShowDialog();
                    }
                    else if (cbActivityType.Text == "DOCUMENTATION")
                    {
                        OtherWorks OthersDetails = new OtherWorks("DOCUMENTATION","");
                        OthersDetails.objServiceActivities = this;
                        OthersDetails.ShowDialog();
                    }
                    else if (cbActivityType.Text == "SEMINORS AND EXHIBITIONS")
                    {
                        OtherWorks OthersDetails = new OtherWorks("SEMINORS","");
                        OthersDetails.objServiceActivities = this;
                        OthersDetails.ShowDialog();
                    }
                    else if (cbActivityType.Text == "ADVANCE VERIFICATION")
                    {
                        OtherWorks OthersDetails = new OtherWorks("ADV_VERIFICATION","");
                        OthersDetails.objServiceActivities = this;
                        OthersDetails.ShowDialog();
                    }
                    else if (cbActivityType.Text == "OTHERS")
                    {
                        OtherWorks OthersDetails = new OtherWorks("OTHERS","");
                        OthersDetails.objServiceActivities = this;
                        OthersDetails.ShowDialog();
                    }
                    else
                    {
                        OtherWorks OthersDetails = new OtherWorks("",cbActivityType.SelectedValue.ToString());
                        OthersDetails.objServiceActivities = this;
                        OthersDetails.ShowDialog();
                    }
                   
                   
                }
                else
                {
                    MessageBox.Show("Please Select Activity", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbActivityType.Focus();
                }
            //}
            //else
            //{
            //    MessageBox.Show("No Mapping! Please Map Employees", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }


        private void btnClearActivity_Click(object sender, EventArgs e)
        {
            dtActivityDetails.Rows.Clear();
            gvActivityDetails.Rows.Clear();
        }

        private void txtTrnNo_Validated(object sender, EventArgs e)
        {
            if (txtTrnNo.Text.Length > 20)
            {
                flagUpdate = false;
                GetServicesDARDetails();
            }
            else
            {
                flagUpdate = false;
                cbActivityType.SelectedIndex = 0;
                cbEcode.SelectedIndex = -1;
                dtpTrnDate.Value = DateTime.Today;
                dtActivityDetails.Rows.Clear();
                gvActivityDetails.Rows.Clear();


            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            int iRes = 0;
            if (txtTrnNo.Text.Length > 20 && flagUpdate == true)
            {
                DialogResult result = MessageBox.Show("Do you want to delete This Record ?",
                                        "SSERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        strCmd = "DELETE FROM SERVICES_DAR_DETL WHERE SDD_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                        strCmd += " DELETE FROM SERVICES_DAR_HEAD WHERE SDH_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";

                        if (strCmd.Length > 10)
                        {
                            iRes = objSQLdb.ExecuteSaveData(strCmd);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancel_Click(null, null);
                    }
                }
            }
        }


    }
}
