using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using SSCRMDB;
using SSTrans;
using SSAdmin;
using Excel = Microsoft.Office.Interop.Excel;


namespace SSCRM
{
    public partial class AuditMisconductBranch : Form
    {
        SQLDB objSQLdb = null;
        HRInfo objHRdb = null;
        private Security objSecurity = null;
        private UtilityDB objUtilityDB = null;

        private bool flagUpdate = false;
        private ReportViewer childReportViewer = null;

        string strDept = "";

        ExcelDB objExcelDB = null;

        public DataTable dtExplDetl = new DataTable();

        public AuditMisconductBranch()
        {
            InitializeComponent();
        }

        private void AuditMisconductBranch_Load(object sender, EventArgs e)
        {
            FillCompanyData();

           // strDept = "100000,200000,300000,400000,500000,600000,800000,900000,1000000,1100000,1200000,1300000,1400000,1600000";
            lblKnocking.Visible = false;
            flagUpdate = false;
            FillFinYear();

            FillDeptDetails();
            GenerateMisConId();

            dtpDoctMnth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
            
            gvExplDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                    System.Drawing.FontStyle.Regular);
            gvDepartment.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                    System.Drawing.FontStyle.Regular);
            

                    
            cbFinYear.SelectedValue = CommonData.FinancialYear;
           
            #region "CREATE MISCOND_EXPL_DETL TABLE"
            dtExplDetl.Columns.Add("SlNo_Expl");
            dtExplDetl.Columns.Add("EmpCode_Expl");
            dtExplDetl.Columns.Add("DesigCode_Expl");
            dtExplDetl.Columns.Add("DeptCode_Expl");
            dtExplDetl.Columns.Add("EmpName_Expl");
            dtExplDetl.Columns.Add("EmpDesig_Expl");
            dtExplDetl.Columns.Add("EmpDept_Expl");
            dtExplDetl.Columns.Add("Explanation");
            #endregion


        }

        private void FillCompanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
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
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
                }

                cbCompany.SelectedValue = CommonData.CompanyCode;
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

        private void FillBranchDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                if (cbCompany.SelectedIndex > 0 && flagUpdate == true)
                {
                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE+'@'+STATE_CODE as branchCode " +
                                        " FROM USER_BRANCH " +
                                        " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                        " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                        "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                        "' union " +
                                        "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE+'@'+STATE_CODE as branchCode " +
                                        " FROM BRANCH_MAS WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "'ORDER BY BRANCH_NAME ASC ";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbLocation.DataSource = dt;
                    cbLocation.DisplayMember = "BRANCH_NAME";
                    cbLocation.ValueMember = "branchCode";

                }

                //string BranCode = CommonData.BranchCode + '@' + CommonData.StateCode;
                //cbLocation.SelectedValue = BranCode;
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

        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();

            try
            {
                if (cbCompany.SelectedIndex > 0 && cbZones.SelectedIndex > 0 && cbRegion.SelectedIndex > 0)
                {

                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE+'@'+STATE_CODE as branchCode " +
                                        " FROM USER_BRANCH " +
                                        " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                        " INNER JOIN AUDIT_BRANCH_MAS ON ABM_BRANCH_CODE=UB_BRANCH_CODE " +
                                        " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                        "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                        "' AND ABM_STATE='" + cbZones.SelectedValue.ToString() +
                                        "' AND ABM_REGION='" + cbRegion.SelectedValue.ToString() +
                                        "' union " +
                                        " SELECT DISTINCT BRANCH_NAME,BRANCH_CODE+'@'+STATE_CODE as branchCode " +
                                        " FROM BRANCH_MAS WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "'and STATE_CODE='" + cbZones.SelectedValue.ToString() +
                                        "'and DISTRICT='" + cbRegion.SelectedValue.ToString() +
                                        "'AND BRANCH_CODE NOT IN (SELECT ABM_BRANCH_CODE FROM AUDIT_BRANCH_MAS) ORDER BY BRANCH_NAME ASC";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbLocation.DataSource = dt;
                    cbLocation.DisplayMember = "BRANCH_NAME";
                    cbLocation.ValueMember = "branchCode";

                }



                //string BranCode = CommonData.BranchCode + '@' + CommonData.StateCode;
                //cbLocation.SelectedValue = BranCode;
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

        private void FillFinYear()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {

                string strCmd = "SELECT DISTINCT FY_FIN_YEAR  FROM FIN_YEAR";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbFinYear.DataSource = dt;
                    cbFinYear.DisplayMember = "FY_FIN_YEAR";
                    cbFinYear.ValueMember = "FY_FIN_YEAR";
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

        private void FillDeptDetails()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {

                string strCmd = "SELECT dept_desc ,dept_desc+'@'+CAST(dept_code AS VARCHAR) AS DeptCode FROM Dept_Mas";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbDeptAdd.DataSource = dt;
                    cbDeptAdd.DisplayMember = "dept_desc";
                    cbDeptAdd.ValueMember = "DeptCode";
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

        private void FillZonesList()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";

            if (cbCompany.SelectedIndex > 0)
            {
                try
                {
                    strCommand = "SELECT DISTINCT ABM_STATE Zone FROM AUDIT_BRANCH_MAS " +
                                 " WHERE ABM_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                                 "' union " +
                                 " SELECT DISTINCT STATE_CODE Zone FROM BRANCH_MAS " +
                                 " WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "'";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = "--Select--";

                        dt.Rows.InsertAt(dr, 0);

                        cbZones.DataSource = dt;
                        cbZones.DisplayMember = "Zone";
                        cbZones.ValueMember = "Zone";
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
            else
            {
                cbZones.DataSource = null;
            }
        }

        private void FillRegionsList()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";

            if (cbZones.SelectedIndex > 0)
            {
                try
                {
                    strCommand = "SELECT DISTINCT ABM_REGION Region FROM AUDIT_BRANCH_MAS " +
                                " WHERE ABM_STATE='" + cbZones.SelectedValue.ToString() + "' " +
                                " AND ABM_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                                "' union SELECT DISTINCT DISTRICT Region FROM BRANCH_MAS " +
                                " WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                "'and STATE_CODE='" + cbZones.SelectedValue.ToString() +
                                "' and DISTRICT is not null and DISTRICT!=''";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = "--Select--";

                        dt.Rows.InsertAt(dr, 0);

                        cbRegion.DataSource = dt;
                        cbRegion.DisplayMember = "Region";
                        cbRegion.ValueMember = "Region";
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
            else
            {
                cbRegion.DataSource = null;
            }
        }

        private void GetAuditEmployeeName()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            if (txtAuditByEcode.Text != "")
            {
                try
                {
                    string strCmd = "SELECT MEMBER_NAME+'('+DESIG+')' EName FROM EORA_MASTER " +
                                    " WHERE ECODE=" + txtAuditByEcode.Text + "";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtAuditName.Text = dt.Rows[0]["EName"].ToString();
                    }
                    else
                    {
                        txtAuditName.Text = "";
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
            else
            {
                txtAuditName.Text = "";
            }

        }

        private void GenerateMisConId()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT ISNULL(MAX(HMH_TRN_ID),0)+1 MisConId FROM HR_MISCONDUCT_HEAD";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtMisCondId.Text = dt.Rows[0]["MisConId"].ToString();
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


        private void FillLogicalBranch()
        {
            objHRdb = new HRInfo();
            DataTable dt = new DataTable();
            char Active = 'T';
            string strLogBranCode = string.Empty;
            cbLogicalBranch.DataSource = null;

            try
            {
                string[] strData = cbLocation.SelectedValue.ToString().Split('@');

                dt = objHRdb.GetLogicalBranches(strData[0], strLogBranCode, Active).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbLogicalBranch.DataSource = dt;
                    cbLogicalBranch.DisplayMember = "branch_name";
                    cbLogicalBranch.ValueMember = "branch_code";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objHRdb = null;
                dt = null;
            }


        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private bool CheckData()
        {
            bool bFlag = true;
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";

            if (cbFinYear.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Financial year", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbFinYear.Focus();
                return bFlag;
            }

            if (cbCompany.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Company", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCompany.Focus();
                return bFlag;
            }

            if (cbZones.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Zone", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbZones.Focus();
                return bFlag;
            }

            if (cbRegion.SelectedIndex == 0 && cbRegion.SelectedIndex == -1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Region", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbRegion.Focus();
                return bFlag;
            }

            if (cbLocation.SelectedIndex == 0 || cbLocation.SelectedIndex == -1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Location", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbLocation.Focus();
                return bFlag;
            }
            if (cbLogicalBranch.Items.Count != 0)
            {
                if (cbLogicalBranch.SelectedIndex == 0)
                {
                    bFlag = false;
                    MessageBox.Show("Please Select LogicalBranch", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbLogicalBranch.Focus();
                    return bFlag;
                }
            }

            if (rtbAuditPoint.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Audit Point", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                rtbAuditPoint.Focus();
                return bFlag;
            }

            if (txtAuditByEcode.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter AuditBy Emp Ecode", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAuditByEcode.Focus();
                return bFlag;
            }
           
            return bFlag;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();

            if (CheckData() == true)
            {
                if (SaveMisConHeadDetails() > 0)
                {
                    SaveMisConDeptDetails();
                    SaveMisCondExplDetails();
                    SaveImageDetails();
                    MessageBox.Show("Data Saved SucessFully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    flagUpdate = false;
                    rtbAuditPoint.Text = "";
                    dtExplDetl.Rows.Clear();
                    //txtAuditByEcode.Text = "";
                    //txtAuditName.Text = "";
                    gvExplDetails.Rows.Clear();
                    gvDepartment.Rows.Clear();
                    GenerateMisConId();
                    gvDocumentDetl.Rows.Clear();
                }
                else
                {

                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
             
        

        #region "SAVE AND UPDATE DATA"
        private int SaveMisConHeadDetails()
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            int iRes = 0;
            string logicalBranch = "";
            
            try
            {
                string[] strData = null;
                strData = cbLocation.SelectedValue.ToString().Split('@');            
                               
                
                if (cbLogicalBranch.SelectedIndex == -1)
                {
                    logicalBranch = "";
                }
                else
                {
                    logicalBranch = cbLogicalBranch.SelectedValue.ToString();
                }
               
                if (flagUpdate == true)
                {
                    strCommand = "UPDATE HR_MISCONDUCT_HEAD SET HMH_FIN_YEAR='" + cbFinYear.SelectedValue.ToString() +
                                  "', HMH_VISIT_MONTH='" + Convert.ToDateTime(dtpVisitMnth.Value).ToString("MMMyyyy").ToUpper() +
                                  "', HMH_DOC_MONTH='" + Convert.ToDateTime(dtpDoctMnth.Value).ToString("MMMyyyy").ToUpper() +
                                  "', HMH_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                                  "', HMH_STATE_CODE='" + strData[1] + "',HMH_BRANCH_CODE='" + strData[0] +
                                  "', HMH_LOG_BRANCH_CODE='" + logicalBranch +
                                  "', HMH_ZONE='" + cbZones.SelectedValue.ToString() + 
                                  "',HMH_REGION='" + cbRegion.SelectedValue.ToString() +
                                  "', HMH_AUDIT_POINT='" + rtbAuditPoint.Text.Replace("\'", "") +
                        //"',HMH_EXPLANATION='" + rtbExpln.Text.Replace("'", "") +
                                  "', HMH_STATUS='UNSOLVED'" +                                  
                                  ", HMH_AUDIT_BY=" + Convert.ToInt32(txtAuditByEcode.Text) +
                                  ", HMH_MODIFIED_BY='" + CommonData.LogUserId +
                                  "',HMH_MODIFIED_DATE='" + CommonData.CurrentDate +                                  
                                  "' WHERE HMH_TRN_ID=" + txtMisCondId.Text + "";


                }


                else if (flagUpdate == false)
                {
                    GenerateMisConId();
                    objSQLdb = new SQLDB();

                    strCommand = "INSERT INTO HR_MISCONDUCT_HEAD(HMH_FIN_YEAR " +
                                                                     ",HMH_TRN_ID" +
                                                                     ", HMH_VISIT_MONTH " +
                                                                     ", HMH_DOC_MONTH " +
                                                                     ", HMH_COMP_CODE " +
                                                                     ", HMH_STATE_CODE " +
                                                                     ", HMH_BRANCH_CODE " +
                                                                     ", HMH_LOG_BRANCH_CODE " +
                                                                     ", HMH_ZONE " +
                                                                     ", HMH_REGION " +
                                                                     ", HMH_AUDIT_POINT " +
                                                                     ", HMH_STATUS " +                                                                     
                                                                     ",HMH_AUDIT_BY " +
                                                                     ",HMH_CREATED_BY " +
                                                                     ",HMH_CREATED_DATE " +                                                                    
                                                                     ") VALUES " +
                                                                     "('" + cbFinYear.SelectedValue.ToString() +
                                                                     "'," + Convert.ToInt32(txtMisCondId.Text) +
                                                                     ",'" + Convert.ToDateTime(dtpVisitMnth.Value).ToString("MMMyyyy").ToUpper() +
                                                                     "','" + Convert.ToDateTime(dtpDoctMnth.Value).ToString("MMMyyyy").ToUpper() +
                                                                     "','" + cbCompany.SelectedValue.ToString() +
                                                                     "','" + strData[1] + "','" + strData[0] +
                                                                     "','" + logicalBranch +
                                                                     "','" + cbZones.SelectedValue.ToString() +
                                                                     "','" + cbRegion.SelectedValue.ToString() +
                                                                     "',N'" + rtbAuditPoint.Text.Replace("\'", "") +                        
                                                                     "','UNSOLVED' "+  
                                                                     "," + Convert.ToInt32(txtAuditByEcode.Text) +
                                                                     ",'" + CommonData.LogUserId +
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
            finally
            {
                objSQLdb = null;
            }
            return iRes;
        }

        

        private int SaveMisConDeptDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            try
            {
                strCommand = "DELETE FROM HR_MISCONDUCT_INC_DEPT WHERE HMID_MISCONDUCT_ID=" + Convert.ToInt32(txtMisCondId.Text) + " ";
                iRes = objSQLdb.ExecuteSaveData(strCommand);

                strCommand = "";

                if (gvDepartment.Rows.Count > 0)
                {
                    for (int i = 0; i < gvDepartment.Rows.Count; i++)
                    {

                        strCommand += "INSERT INTO HR_MISCONDUCT_INC_DEPT(HMID_MISCONDUCT_ID " +
                                                                              ", HMID_DEPT_ID " +
                                                                              ")VALUES(" + Convert.ToInt32(txtMisCondId.Text) +
                                                                              "," + Convert.ToInt32(gvDepartment.Rows[i].Cells["DeptId"].Value) + ")";
                    }
                }


                if (strCommand.Length > 10)
                {
                    iRes = objSQLdb.ExecuteSaveData(strCommand);
                }

                if (iRes > 0)
                {
                    return iRes;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
            }
            return iRes;
        }

        private int SaveMisCondExplDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            try
            {
                strCommand = "DELETE FROM HR_MISCONDUCT_EXP_DETL WHERE HMED_MISCONDUCT_ID=" + Convert.ToInt32(txtMisCondId.Text) + " ";
                iRes = objSQLdb.ExecuteSaveData(strCommand);

                strCommand = "";
                iRes = 0;

                if (gvExplDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvExplDetails.Rows.Count; i++)
                    {

                        strCommand += "INSERT INTO HR_MISCONDUCT_EXP_DETL(HMED_MISCONDUCT_ID " +
                                                                       ", HMED_SL_NO " +
                                                                       ", HMED_EORA_CODE " +
                                                                       ", HMED_DEPT_ID " +
                                                                       ", HMED_DESG_ID " +
                                                                       ", HMED_EXPLANATION " +
                                                                       ", HMED_CREATED_BY " +
                                                                       ", HMED_CREATED_DATE " +
                                                                       ")VALUES(" + Convert.ToInt32(txtMisCondId.Text) +
                                                                       "," + Convert.ToInt32(gvExplDetails.Rows[i].Cells["SlNo_Expl"].Value) +
                                                                       ", " + Convert.ToInt32(gvExplDetails.Rows[i].Cells["EmpCode_Expl"].Value) +
                                                                       "," + Convert.ToInt32(gvExplDetails.Rows[i].Cells["DeptCode_Expl"].Value) +
                                                                       "," + Convert.ToInt32(gvExplDetails.Rows[i].Cells["DesigCode_Expl"].Value) +
                                                                       ",'" + gvExplDetails.Rows[i].Cells["Explanation"].Value.ToString() +
                                                                       "','" + CommonData.LogUserId +
                                                                       "',getdate())";
                    }
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
            finally
            {
                objSQLdb = null;
            }
            return iRes;
        }

        private int SaveImageDetails()
        {
            string strCmd = "";
            objSQLdb = new SQLDB();
            int iRes = 0;

            try
            {

                byte[] arr;
                ImageConverter converter = new ImageConverter();

                strCmd = "DELETE FROM HR_MISCONDUCT_IMAGE_DETL " +
                        " WHERE  HMDD_MISCONDUCT_ID=" + Convert.ToInt32(txtMisCondId.Text) + " ";
                iRes = objSQLdb.ExecuteSaveData(strCmd);
                strCmd = "";
                iRes = 0;

                if (gvDocumentDetl.Rows.Count > 0)
                {
                    for (int i = 0; i < gvDocumentDetl.Rows.Count; i++)
                    {
                        arr = (byte[])gvDocumentDetl.Rows[i].Cells["DocImage"].Value;

                        strCmd += "INSERT INTO HR_MISCONDUCT_IMAGE_DETL(HMDD_MISCONDUCT_ID " +
                                                                    ", HMDD_SL_NO " +
                                                                    ", HMDD_DOC_NAME " +
                                                                    ", HMDD_DOC_DESC " +
                                                                    ", HMDD_DOC_IMAGE " +
                                                                    ", HMDD_CREATED_BY " +
                                                                    ", HMDD_CREATED_DATE " +
                                                                    ")values " +
                                                                    "(" + Convert.ToInt32(txtMisCondId.Text) +
                                                                    "," + Convert.ToInt32(gvDocumentDetl.Rows[i].Cells["SLNO"].Value) +
                                                                    ",'" + gvDocumentDetl.Rows[i].Cells["DocumentName"].Value.ToString() +
                                                                    "','" + gvDocumentDetl.Rows[i].Cells["DocumentDesc"].Value.ToString() +
                                                                    "',@Image,'" + CommonData.LogUserId +
                                                                    "',getdate())";



                        string sConnVal = ConfigurationSettings.AppSettings["DBCon"].ToString();
                        objSecurity = new Security();
                        SqlConnection Con = new SqlConnection(objSecurity.GetDecodeString(sConnVal));
                        SqlCommand SqlCom = new SqlCommand(strCmd, Con);

                        SqlCom.Parameters.Add(new SqlParameter("@Image", (object)arr));
                        Con.Open();
                        iRes = SqlCom.ExecuteNonQuery();
                        Con.Close();
                        arr = null;
                        strCmd = "";
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return iRes;

        }            

        #endregion

    

        private void txtAuditByEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

      
      

        public void GetExplanationDetails()
        {
            int intRow = 1;
            gvExplDetails.Rows.Clear();

            try
            {
                if (dtExplDetl.Rows.Count > 0)
                {

                    for (int i = 0; i < dtExplDetl.Rows.Count; i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        dtExplDetl.Rows[i]["SlNo_Expl"] = intRow;
                        tempRow.Cells.Add(cellSLNO);


                        DataGridViewCell cellEmpCode = new DataGridViewTextBoxCell();
                        cellEmpCode.Value = dtExplDetl.Rows[i]["EmpCode_Expl"].ToString();
                        tempRow.Cells.Add(cellEmpCode);

                        DataGridViewCell cellDesigCode = new DataGridViewTextBoxCell();
                        cellDesigCode.Value = dtExplDetl.Rows[i]["DesigCode_Expl"].ToString();
                        tempRow.Cells.Add(cellDesigCode);

                        DataGridViewCell cellDeptCode = new DataGridViewTextBoxCell();
                        cellDeptCode.Value = dtExplDetl.Rows[i]["DeptCode_Expl"].ToString();
                        tempRow.Cells.Add(cellDeptCode);

                        DataGridViewCell cellEmpName = new DataGridViewTextBoxCell();
                        cellEmpName.Value = dtExplDetl.Rows[i]["EmpName_Expl"].ToString();
                        tempRow.Cells.Add(cellEmpName);

                        DataGridViewCell cellDesigName = new DataGridViewTextBoxCell();
                        cellDesigName.Value = dtExplDetl.Rows[i]["EmpDesig_Expl"].ToString();
                        tempRow.Cells.Add(cellDesigName);

                        DataGridViewCell cellDeptName = new DataGridViewTextBoxCell();
                        cellDeptName.Value = dtExplDetl.Rows[i]["EmpDept_Expl"].ToString();
                        tempRow.Cells.Add(cellDeptName);

                        DataGridViewCell cellExplanation = new DataGridViewTextBoxCell();
                        cellExplanation.Value = dtExplDetl.Rows[i]["Explanation"].ToString();
                        tempRow.Cells.Add(cellExplanation);


                        intRow = intRow + 1;
                        gvExplDetails.Rows.Add(tempRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #region "GRID VIEW DATA EDITING AND DELETING"

        private void gvDepartment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvDepartment.Columns["Delete"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    DataGridViewRow dgvr = gvDepartment.Rows[e.RowIndex];
                    gvDepartment.Rows.Remove(dgvr);
                }
                if (gvDepartment.Rows.Count > 0)
                {
                    for (int i = 0; i < gvDepartment.Rows.Count; i++)
                    {
                        gvDepartment.Rows[i].Cells["SLNO_Dept"].Value = (i + 1).ToString();
                    }
                }
            }
        }

        private void gvExplDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvExplDetails.Columns["Edit_Expl"].Index)
                {

                    if (Convert.ToBoolean(gvExplDetails.Rows[e.RowIndex].Cells["Edit_Expl"].Selected) == true)
                    {
                        //objSQLdb = new SQLDB();
                        //DataTable dt = new DataTable();
                        //string strCmd = "SELECT * FROM '#dtExplDetl'";
                        //dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                        int SlNo = Convert.ToInt32(gvExplDetails.Rows[e.RowIndex].Cells[gvExplDetails.Columns["SlNo_Expl"].Index].Value);
                        DataRow[] dr = dtExplDetl.Select("SlNo_Expl=" + SlNo);

                        MisconductExplanationDetl MisExplDetl = new MisconductExplanationDetl("BRANCH",dr);
                        MisExplDetl.objAuditMisconductBranch = this;
                        MisExplDetl.ShowDialog();

                    }
                }


                if (e.ColumnIndex == gvExplDetails.Columns["Del_Expl"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvExplDetails.Rows[e.RowIndex].Cells[gvExplDetails.Columns["SlNo_Expl"].Index].Value);
                        DataRow[] dr = dtExplDetl.Select("SlNo_Expl=" + SlNo);
                        dtExplDetl.Rows.Remove(dr[0]);
                        GetExplanationDetails();
                        MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }

        }
        #endregion


        private bool CheckDuplicateDept()
        {
            bool flag = true;

            for (int i = 0; i < gvDepartment.Rows.Count; i++)
            {
                string[] strDept = cbDeptAdd.SelectedValue.ToString().Split('@');

                if (strDept[1].Equals(gvDepartment.Rows[i].Cells["DeptId"].Value.ToString()))
                {
                    flag = false;
                    MessageBox.Show("Already Exists", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    break;
                }
            }

            return flag;
        }

        private void btnAddDept_Click(object sender, EventArgs e)
        {
            int intRow = 1;

            if (cbDeptAdd.SelectedIndex > 0)
            {
                if (CheckDuplicateDept() == true)
                {
                    string[] strDept = cbDeptAdd.SelectedValue.ToString().Split('@');
                    DataGridViewRow tempRow = new DataGridViewRow();

                    if (gvDepartment.Rows.Count > 0)
                    {
                        DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                        cellSlNo.Value = gvDepartment.Rows.Count + 1;
                        tempRow.Cells.Add(cellSlNo);


                    }
                    else
                    {


                        DataGridViewCell cellSlNoDept = new DataGridViewTextBoxCell();
                        cellSlNoDept.Value = intRow;
                        intRow = intRow + 1;
                        tempRow.Cells.Add(cellSlNoDept);
                    }

                    DataGridViewCell cellDeptId = new DataGridViewTextBoxCell();
                    cellDeptId.Value = strDept[1];
                    tempRow.Cells.Add(cellDeptId);

                    DataGridViewCell cellDeptName = new DataGridViewTextBoxCell();
                    cellDeptName.Value = strDept[0];
                    tempRow.Cells.Add(cellDeptName);

                    gvDepartment.Rows.Add(tempRow);
                }
                cbDeptAdd.SelectedIndex = 0;
                cbDeptAdd.Focus();
            }

            else
            {
                MessageBox.Show("Please Select Department Name", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbDeptAdd.Focus();
            }
        }



        #region "GET DATA FOR UPDATE"
        private void GetMisConHeadDetails(Int32 MisConId)
        {
            objHRdb = new HRInfo();
            DataTable dt;
            
            DataTable dtDept;
            DataTable dtExplanationDetl;
            DataTable dtImageDetl;
            Hashtable ht;
            if (txtMisCondId.Text != "")
            {
                try
                {
                    ht = objHRdb.GetMisconductDetails(MisConId);
                    dt = (DataTable)ht["MisconductHeadDetails"];
                    dtDept = (DataTable)ht["MisconductDeptDetails"];
                    dtExplanationDetl = (DataTable)ht["MisconductExplDetails"];
                    dtImageDetl = (DataTable)ht["MisCondImageDetl"];

                    if (dt.Rows.Count > 0)
                    {
                        flagUpdate = true;
                        //FillBranchDetails();

                        if (dt.Rows[0]["DeleteFlag"].ToString().Equals("DELETED"))
                        {
                            //cbCompany.SelectedIndex = 0;

                            //cbLocation.SelectedIndex = -1;
                            //cbLogicalBranch.SelectedIndex = -1;

                            //cbZones.SelectedIndex = 0;
                            //cbRegion.SelectedIndex = -1;

                            cbFinYear.SelectedIndex = 0;
                            dtpDoctMnth.Value = DateTime.Today;
                            dtpVisitMnth.Value = DateTime.Today;

                            rtbAuditPoint.Text = "";
                            gvDocumentDetl.Rows.Clear();
                            cbDeptAdd.SelectedIndex = 0;
                            gvDepartment.Rows.Clear();

                            dtExplanationDetl.Rows.Clear();
                            txtAuditByEcode.Text = "";
                            txtAuditName.Text = "";
                            //cbMisconduct.SelectedIndex = 0;

                            gvExplDetails.Rows.Clear();

                            MessageBox.Show("This Point Is Deleted By  - " + dt.Rows[0]["DeleteBy"].ToString());
                            GenerateMisConId();
                            return;
                        }
                        else
                        {
                            
                            cbFinYear.SelectedValue = dt.Rows[0]["FinYear"].ToString();
                            dtpVisitMnth.Value = Convert.ToDateTime(dt.Rows[0]["VisitMonth"]);
                            dtpDoctMnth.Value = Convert.ToDateTime(dt.Rows[0]["DocMon"]);
                            cbCompany.SelectedValue = dt.Rows[0]["CompCode"].ToString();
                            cbLocation.SelectedValue = dt.Rows[0]["BranCode"].ToString();
                            if (dt.Rows[0]["Log_BranCode"].ToString() != "")
                                cbLogicalBranch.SelectedValue = dt.Rows[0]["Log_BranCode"].ToString();
                            else if (dt.Rows[0]["Log_BranCode"].ToString() == "" && cbLogicalBranch.Items.Count != 0)
                                cbLogicalBranch.SelectedIndex = 0;

                            if (dt.Rows[0]["ZONE"].ToString() != "")
                                cbZones.SelectedValue = dt.Rows[0]["ZONE"].ToString();
                            else
                                cbZones.SelectedIndex = 0;
                            cbRegion.SelectedValue = dt.Rows[0]["Region"].ToString();

                            rtbAuditPoint.Text = dt.Rows[0]["AuditPoint"].ToString();

                            txtAuditByEcode.Text = dt.Rows[0]["Audit_Ecode"].ToString();
                            txtAuditName.Text = dt.Rows[0]["AuditName"].ToString();

                            FillMisConDeptDetails(dtDept);
                            FillEmpMisCondExplDetails(dtExplanationDetl);
                            FillImageDetails(dtImageDetl);
                        }

                    }
                    else
                    {
                        //cbCompany.SelectedIndex = 0;

                        flagUpdate = false;
                        //cbLocation.SelectedIndex = -1;

                        //if (cbLogicalBranch.Items.Count != 0)
                        //    cbLogicalBranch.SelectedIndex = 0;

                        //cbZones.SelectedIndex = 0;

                        //cbFinYear.SelectedIndex = 0;
                        //dtpDoctMnth.Value = DateTime.Today;
                        //dtpVisitMnth.Value = DateTime.Today;
                        dtExplDetl.Rows.Clear();

                        rtbAuditPoint.Text = "";
                        gvDocumentDetl.Rows.Clear();
                        cbDeptAdd.SelectedIndex = 0;
                        gvDepartment.Rows.Clear();
                        gvExplDetails.Rows.Clear();

                        txtAuditByEcode.Text = "";
                        txtAuditName.Text = "";


                        GenerateMisConId();
                    }

                }
                catch (Exception ex)
                {
                    flagUpdate = false;
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objHRdb = null;
                    dt = null;
                }
            }

        }

        private void FillImageDetails(DataTable dtImages)
        {
            gvDocumentDetl.Rows.Clear();
            if (dtImages.Rows.Count > 0)
            {
                for (int i = 0; i < dtImages.Rows.Count; i++)
                {
                    gvDocumentDetl.Rows.Add();
                    gvDocumentDetl.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                    gvDocumentDetl.Rows[i].Cells["DocumentName"].Value = dtImages.Rows[i]["DocName"].ToString();
                    gvDocumentDetl.Rows[i].Cells["DocumentDesc"].Value = dtImages.Rows[i]["Doc_Desc"].ToString();
                    gvDocumentDetl.Rows[i].Cells["DocImage"].Value = dtImages.Rows[i]["DocImage"];

                }
            }
        }        

        private void FillMisConDeptDetails(DataTable dtDeptData)
        {
            gvDepartment.Rows.Clear();
            if (txtMisCondId.Text.Length > 0)
            {
                try
                {


                    if (dtDeptData.Rows.Count > 0)
                    {
                        flagUpdate = true;
                        for (int i = 0; i < dtDeptData.Rows.Count; i++)
                        {
                            gvDepartment.Rows.Add();

                            gvDepartment.Rows[i].Cells["SLNO_Dept"].Value = (i + 1).ToString();
                            gvDepartment.Rows[i].Cells["DeptId"].Value = dtDeptData.Rows[i]["DeptId"].ToString();
                            gvDepartment.Rows[i].Cells["DeptName"].Value = dtDeptData.Rows[i]["DeptName"].ToString();

                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        private void FillEmpMisCondExplDetails(DataTable dt)
        {
            gvExplDetails.Rows.Clear();
            dtExplDetl.Rows.Clear();
            try
            {
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        dtExplDetl.Rows.Add(new object[]{"-1",dt.Rows[i]["EmpCode"].ToString(),
                                                                    dt.Rows[i]["DesigId"].ToString(),
                                                                    dt.Rows[i]["DeptId"].ToString(),
                                                                    dt.Rows[i]["EmpName"].ToString(),
                                                                    dt.Rows[i]["DesigName"].ToString(),
                                                                    dt.Rows[i]["DeptName"].ToString(),
                                                                    dt.Rows[i]["Explanation"].ToString()});
                        GetExplanationDetails();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

       

       


        #endregion

        private void txtMisCondId_KeyUp(object sender, KeyEventArgs e)
        {
            //if (txtMisCondId.Text != "")
            //{
            //    flagUpdate = false;
            //    GetMisConHeadDetails(Convert.ToInt32(txtMisCondId.Text));
            //}
            //else
            //{
            //    flagUpdate = false;
            //    //cbLocation.SelectedIndex = -1;

            //    if (cbLogicalBranch.Items.Count != 0)
            //        cbLogicalBranch.SelectedIndex = 0;


            //    cbZones.SelectedIndex = 0;
            //    cbRegion.SelectedIndex = -1;

            //    //cbFinYear.SelectedIndex = 0;
            //    //dtpDoctMnth.Value = DateTime.Today;
            //    //dtpVisitMnth.Value = DateTime.Today;
            //    dtExplDetl.Rows.Clear();

            //    rtbAuditPoint.Text = "";

            //    cbDeptAdd.SelectedIndex = 0;
            //    gvDepartment.Rows.Clear();
            //    gvExplDetails.Rows.Clear();

            //    txtAuditByEcode.Text = "";
            //    txtAuditName.Text = "";


            //    GenerateMisConId();


            //}
        }

        private void txtMisCondId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnAddExplDetails_Click(object sender, EventArgs e)
        {
            MisconductExplanationDetl MisExplDetl = new MisconductExplanationDetl("BRANCH");
            MisExplDetl.objAuditMisconductBranch = this;
            MisExplDetl.ShowDialog();
        }

        private void txtAuditByEcode_KeyUp(object sender, KeyEventArgs e)
        {
            GetAuditEmployeeName();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dtExplDetl.Rows.Clear();
            gvExplDetails.Rows.Clear();

            //cbLocation.SelectedIndex = -1;
            ////cbLocation.SelectedIndex = -1;
            //if (cbLogicalBranch.Items.Count != 0)
            //    cbLogicalBranch.SelectedIndex = -1;

            //cbZones.SelectedIndex = 0;
            //cbRegion.SelectedIndex = -1;
            lblKnocking.Text = "";
            //cbFinYear.SelectedIndex = 0;
            //dtpDoctMnth.Value = DateTime.Today;
            //dtpVisitMnth.Value = DateTime.Today;

            rtbAuditPoint.Text = "";
            gvDocumentDetl.Rows.Clear();
            gvDepartment.Rows.Clear();
            //txtAuditByEcode.Text = "";
            //txtAuditName.Text = "";
        }

        private void cbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLocation.SelectedIndex > 0)
            {
                FillLogicalBranch();                
            }
        }

        private void cbZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbZones.SelectedIndex > 0)
            {                               
                FillRegionsList();
            }
        }

        private bool CheckDetails()
        {
            bool flagCheck = true;
            if (cbCompany.SelectedIndex == 0)
            {
                flagCheck = false;
                MessageBox.Show("Please Select Company","Audit Data Bank",MessageBoxButtons.OK,MessageBoxIcon.Information);               
                cbLocation.Focus();
                return flagCheck;
            }

            if (cbLocation.SelectedIndex == 0 || cbLocation.SelectedIndex == -1)
            {
                flagCheck = false;
                MessageBox.Show("Please Select Location", "Audit Data Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbLocation.Focus();
                return flagCheck;
            }
            if (dtpVisitMnth.Value > DateTime.Now)
            {
                flagCheck = false;
                MessageBox.Show("Please Select Valid DocMonth", "Audit Data Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpDoctMnth.Focus();
                return flagCheck;
            }

            return flagCheck;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (CheckDetails() == true)
            {
                CommonData.ViewReport = "SSCRM_REP_AUDIT_MAJOR_POINTS";
                childReportViewer = new ReportViewer(cbCompany.SelectedValue.ToString(), cbLocation.SelectedValue.ToString().Split('@')[0], "", Convert.ToDateTime(dtpVisitMnth.Value).ToString("MMMyyyy").ToUpper(), "0", "ALL", strDept, "ALL", "ALL", "ALL", "ALL", "AUDIT_QUERY_REGISTER");
                childReportViewer.Show();
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            DataTable dtExcel = new DataTable();
            string strHead = "";
            objExcelDB = new ExcelDB();
            objUtilityDB = new UtilityDB();

            if (CheckDetails() == true)
            {
                try
                {

                    dtExcel = objExcelDB.GetAuditQueryReg(cbCompany.SelectedValue.ToString(), cbLocation.SelectedValue.ToString().Split('@')[0], "", Convert.ToDateTime(dtpVisitMnth.Value).ToString("MMMyyyy").ToUpper(), "0", "ALL", strDept, "ALL", "ALL", "ALL", "ALL", "AUDIT_QUERY_REGISTER").Tables[0];
                    objExcelDB = null;

                    if (dtExcel.Rows.Count > 0)
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;
                        int iTotColumns = 0;
                        iTotColumns = 22;
                        string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                        Excel.Range rgHead = null;
                        Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                        Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 4).ToString());
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;

                        rgData = worksheet.get_Range("A1", "J2");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 16;
                        rgData.ColumnWidth = 200;
                        rgData.RowHeight = 50;
                        
                        strHead = dtExcel.Rows[0]["ad_comp_name"].ToString() + '\n' + dtExcel.Rows[0]["ad_branch_name"].ToString() + '\n' + dtExcel.Rows[0]["ad_zone"].ToString() + '\n' + dtExcel.Rows[0]["ad_region"];
                        strHead = strHead.TrimEnd(',');
                        rgData.Value2 = strHead;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.HorizontalAlignment = Excel.Constants.xlCenter;


                        rg.Font.Bold = true;
                        rg.Font.Name = "Times New Roman";
                        rg.Font.Size = 10;
                        rg.WrapText = true;
                        rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.Interior.ColorIndex = 31;
                        rg.Borders.Weight = 2;
                        rg.Borders.LineStyle = Excel.Constants.xlSolid;
                        rg.Cells.RowHeight = 38;

                        rg = worksheet.get_Range("A4", Type.Missing);
                        rg.Cells.ColumnWidth = 4;
                        rg = worksheet.get_Range("B4", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg = worksheet.get_Range("C4", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("D4", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("E4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("F4", Type.Missing);
                        rg.Cells.ColumnWidth = 50;
                        rg = worksheet.get_Range("G4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("H4", Type.Missing);
                        rg.Cells.ColumnWidth = 50;
                        rg = worksheet.get_Range("I4", Type.Missing);
                        rg.Cells.ColumnWidth = 50;
                        rg = worksheet.get_Range("J4", Type.Missing);
                        rg.Cells.ColumnWidth = 50;
                        rg = worksheet.get_Range("K4", Type.Missing);
                        rg.Cells.ColumnWidth = 50;
                        rg = worksheet.get_Range("L4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("M4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("N4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("O4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("P4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("Q4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("R4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("S4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("R3", "T3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "CONCERN DETAILS";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("W4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("X4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("Y4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("D3", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        //rg = worksheet.get_Range("E3", "F3");
                        //rg.Cells.ColumnWidth = 40;
                        //rg = worksheet.get_Range("F3", Type.Missing);
                        //rg.Cells.ColumnWidth = 40;
                        //rg = worksheet.get_Range("H3", "J3");
                        //rg.Cells.ColumnWidth = 25;
                        //rg = worksheet.get_Range("I3", Type.Missing);
                        //rg.Cells.ColumnWidth = 10;



                        int iColumn = 1, iStartRow = 4;
                        worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                        worksheet.Cells[iStartRow, iColumn++] = "ID";
                        worksheet.Cells[iStartRow, iColumn++] = "Doc Month";
                        worksheet.Cells[iStartRow, iColumn++] = "Visit Month";
                        //worksheet.Cells[iStartRow, iColumn++] = "Company";
                        //worksheet.Cells[iStartRow, iColumn++] = "State";
                        //worksheet.Cells[iStartRow, iColumn++] = "Branch";
                        worksheet.Cells[iStartRow, iColumn++] = "Logical Branch";
                        //worksheet.Cells[iStartRow, iColumn++] = "Zone";
                        //worksheet.Cells[iStartRow, iColumn++] = "Region";
                        worksheet.Cells[iStartRow, iColumn++] = "Audit Point";
                        worksheet.Cells[iStartRow, iColumn++] = "Dept";
                        worksheet.Cells[iStartRow, iColumn++] = "Explanation By Accounts Head";
                        worksheet.Cells[iStartRow, iColumn++] = "Explanation By Sales Head";
                        worksheet.Cells[iStartRow, iColumn++] = "Explanation By Service Head";
                        worksheet.Cells[iStartRow, iColumn++] = "Explanation By Unit Head";
                        worksheet.Cells[iStartRow, iColumn++] = "Status";
                        worksheet.Cells[iStartRow, iColumn++] = "Unsolved Reason";
                        worksheet.Cells[iStartRow, iColumn++] = "Deviation";
                        worksheet.Cells[iStartRow, iColumn++] = "Sub Deviation";
                        worksheet.Cells[iStartRow, iColumn++] = "Deviation Amount";
                        worksheet.Cells[iStartRow, iColumn++] = "Recovered Amount";
                        worksheet.Cells[iStartRow, iColumn++] = "Ecode";
                        worksheet.Cells[iStartRow, iColumn++] = "Name";
                        worksheet.Cells[iStartRow, iColumn++] = "Designation";
                        worksheet.Cells[iStartRow, iColumn++] = "AuditBy";
                        worksheet.Cells[iStartRow, iColumn++] = "Misconduct";

                        iStartRow++; iColumn = 1;
                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            worksheet.Cells[iStartRow, iColumn++] = i + 1;
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_query_id"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_doc_month"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_visit_month"];
                            //worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_comp_name"];
                            //worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_state_name"];
                            //worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_branch_name"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_LogBranch"];
                            //worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_zone"];
                            //worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_region"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_Audit_point"];

                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_dept"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_exp_HAcc"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_exp_Hsales"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_exp_Hservice"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_exp_Others"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_status"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_reason"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_deviation_type"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_subdeviation_type"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_dev_amt"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_rec_amt"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_concern_ecode"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_concern_emp_name"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_concern_desig"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_audit_ecode"] + "-" + dtExcel.Rows[i]["ad_audit_name"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_misconduct"];

                            iStartRow++; iColumn = 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                cbRegion.DataSource = null;               
                FillZonesList();
                if (flagUpdate = true)
                {
                    FillBranchDetails();
                }
               
            }
        }

        private void cbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRegion.SelectedIndex > 0)
            {
                if (flagUpdate == false)
                {
                    FillBranchData();
                }
            }
        }


        private void gvDocumentDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            byte[] Arr;
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == gvDocumentDetl.Columns["ImgView"].Index)
                {
                    Arr = null;
                    Arr = (byte[])gvDocumentDetl.Rows[e.RowIndex].Cells["DocImage"].Value;
                    frmDisplayImage ImgView = new frmDisplayImage(Arr);
                    ImgView.objAuditMisconductBranch = this;
                    ImgView.ShowDialog();
                }

                if (e.ColumnIndex == gvDocumentDetl.Columns["Del_image"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        DataGridViewRow dgvr = gvDocumentDetl.Rows[e.RowIndex];
                        gvDocumentDetl.Rows.Remove(dgvr);
                    }
                    if (gvDocumentDetl.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvDocumentDetl.Rows.Count; i++)
                        {
                            gvDocumentDetl.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                        }
                    }
                }
            }
        }

        private void btnAddDocDetails_Click(object sender, EventArgs e)
        {
            frmAddDocumentDetails DocDetails = new frmAddDocumentDetails("AUDIT_BRANCH");
            DocDetails.objAuditMisconductBranch = this;
            DocDetails.ShowDialog();
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";


            if (txtMisCondId.Text != "")
            {
                try
                {
                    strCmd = "SELECT * FROM HR_MISCONDUCT_HEAD " +
                             " WHERE HMH_DEVIATION_TYPE is not null " +
                             " and HMH_DELETE_BY is null and HMH_TRN_ID=" + txtMisCondId.Text + "";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["HMH_CREATED_BY"].ToString().Equals(CommonData.LogUserId) || CommonData.LogUserId == "admin")
                        {
                            lblKnocking.Visible = false;
                            lblKnocking.Text = "";
                            btnSave.Enabled = true;
                            btnClear.Enabled = true;
                        }
                        else
                        {

                            lblKnocking.Visible = true;
                            btnSave.Enabled = false;
                            btnClear.Enabled = false;

                            lblKnocking.Text = "Can't Modify This Data Approved By:" + dt.Rows[0]["HMH_CREATED_BY"].ToString();

                        }
                    }

                    flagUpdate = false;
                    GetMisConHeadDetails(Convert.ToInt32(txtMisCondId.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                flagUpdate = false;
                //cbLocation.SelectedIndex = -1;

                if (cbLogicalBranch.Items.Count != 0)
                    cbLogicalBranch.SelectedIndex = 0;


                //cbZones.SelectedIndex = 0;
                //cbRegion.SelectedIndex = -1;

                //cbFinYear.SelectedIndex = 0;
                //dtpDoctMnth.Value = DateTime.Today;
                //dtpVisitMnth.Value = DateTime.Today;
                dtExplDetl.Rows.Clear();

                rtbAuditPoint.Text = "";

                cbDeptAdd.SelectedIndex = 0;
                gvDepartment.Rows.Clear();
                gvExplDetails.Rows.Clear();
                gvDocumentDetl.Rows.Clear();
                txtAuditByEcode.Text = "";
                txtAuditName.Text = "";


                GenerateMisConId();


            }
        }

       

    }
}
