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

namespace SSCRM
{
    public partial class MisconductForm : Form
    {
        SQLDB objSQLdb = null;        
        HRInfo objHRdb = null;
        private Security objSecurity = null;
        private bool flagUpdate = false;

        public DataTable dtExplDetl = new DataTable();
        public DataTable dtModeOfRecDetl = new DataTable();
        public DataTable dtActualRecDetl = new DataTable();

        string sCompCode = "", sBranCode = "", sDocMonth = "";

        public MisconductForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            AutoValidate = AutoValidate.EnableAllowFocusChange;
        }

       
        private void MisconductForm_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            FillDeviationType();
            FillFinYear();
            flagUpdate = false;
            btnSave.Enabled = false;
            btnClear.Enabled = false;

            dtpDoctMnth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));

            FillDeptDetails();
            txtMisCondId.Text = GenerateMisConId().ToString();

            FillHOEmployeeDetl();

            cbMgntPoint.SelectedIndex = 2;
            cbPptPoint.SelectedIndex = 2;
            cbMisconduct.SelectedIndex = 2;
            cbStatus.SelectedIndex = 0;
            grp1.Visible = true;
            grp2.Visible = false;

            gvExplDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                    System.Drawing.FontStyle.Regular);
            gvDepartment.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                    System.Drawing.FontStyle.Regular);
            gvEmployeeDetls.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                    System.Drawing.FontStyle.Regular);
            gvActualRecDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                   System.Drawing.FontStyle.Regular);
            gvModeOfRecDetl.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                   System.Drawing.FontStyle.Regular);
            gvMgntEmpDetl.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                   System.Drawing.FontStyle.Regular);

           
            FillEmployeeData();

            
            
            cbFinYear.SelectedValue = CommonData.FinancialYear;
            dtpVisitMnth.Focus();

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

            #region "CREATE MODE_OF_RECOVERY_DETL TABLE"
            dtModeOfRecDetl.Columns.Add("SlNo_Rec");
            dtModeOfRecDetl.Columns.Add("RecEcode");
            dtModeOfRecDetl.Columns.Add("RecEmpName");
            dtModeOfRecDetl.Columns.Add("ReceiptType");
            dtModeOfRecDetl.Columns.Add("ReceiptDate");
            dtModeOfRecDetl.Columns.Add("ReceiptNo");
            dtModeOfRecDetl.Columns.Add("RecAmount");
            dtModeOfRecDetl.Columns.Add("RecRemarks");
            #endregion

            #region "CREATE ACTUAL_RECOVERY_DETL TABLE"
            dtActualRecDetl.Columns.Add("SlNo_Actual");
            dtActualRecDetl.Columns.Add("ActualRecEcode");
            dtActualRecDetl.Columns.Add("ActualRecName");
            dtActualRecDetl.Columns.Add("ActualRcptType");
            dtActualRecDetl.Columns.Add("ActualRcptDate");
            dtActualRecDetl.Columns.Add("ActualRcptNo");
            dtActualRecDetl.Columns.Add("ActualRecAmount");
            dtActualRecDetl.Columns.Add("ActualRecRemarks");
            #endregion

            
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {           
            txtMisCondId.CausesValidation = false;
            if (CheckData() == true)
            {
                btnSave.Enabled = true;
                
                grp1.Visible = false;
                grp2.Visible = true;
                txtMisCondId.CausesValidation = true;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
           
            txtMisCondId.CausesValidation = false;
            grp1.Visible = true;
            grp2.Visible = false;
            txtMisCondId.CausesValidation = true;
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
                if (cbCompany.SelectedIndex > 0 && flagUpdate==true)
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
                                        "' union "+
                                        " SELECT DISTINCT BRANCH_NAME,BRANCH_CODE+'@'+STATE_CODE as branchCode "+
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

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {         
                FillZonesList();
                FillEmployeeData();
                FillBranchData();

                if (flagUpdate == true)
                {
                    FillBranchDetails();
                }
            }
        }

      

        private void FillLogicalBranch()
        {
            objHRdb = new HRInfo();
            DataTable dt = new DataTable();
            char Active='T';
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

        private void FillDeviationType()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT HRMC_MISCONDUCT_HEAD,HRMC_MISCONDUCT_HEAD+'@'+CAST(HRMC_MISCONDUCT_CODE AS VARCHAR) AS MisconDesc "+
                                " FROM HR_MISCONDUCT_MAS ORDER BY HRMC_MISCONDUCT_HEAD";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    

                    dt.Rows.InsertAt(dr, 0);

                    cbDeviationType.DataSource = dt;
                    cbDeviationType.DisplayMember = "HRMC_MISCONDUCT_HEAD";
                    cbDeviationType.ValueMember = "MisconDesc";
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

        private void FillSubDeviationType()
        {

            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbSubDeviationType.DataSource = null;
            try
            {
                if (cbDeviationType.SelectedIndex > 0)
                {
                    string[] strMisconcode = cbDeviationType.SelectedValue.ToString().Split('@');
                    string strCmd = "SELECT HMD_MISCONDUCT_DETL_DESC,HMD_MISCONDUCT_DETL_ID "+
                                    " FROM HR_MISCONDUCT_MAS_DETL "+
                                    " WHERE HMD_MISCONDUCT_ID=" + Convert.ToInt32(strMisconcode[1]) + "";

                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    //dr[1] = "--Select--";                    

                    dt.Rows.InsertAt(dr, 0);

                    cbSubDeviationType.DataSource = dt;
                    cbSubDeviationType.DisplayMember = "HMD_MISCONDUCT_DETL_DESC";
                    cbSubDeviationType.ValueMember = "HMD_MISCONDUCT_DETL_DESC";
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
                                 "' union "+
                                 " SELECT DISTINCT STATE_CODE Zone FROM BRANCH_MAS "+
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


        //private void FillDeptComboBox()
        //{
        //    objSQLdb = new SQLDB();
        //    DataTable dt = new DataTable();
        //    try
        //    {

        //        string strCmd = "SELECT dept_desc , dept_desc+'@'+CAST(dept_code AS VARCHAR) AS DeptCode  FROM Dept_Mas";
        //        dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

        //        if (dt.Rows.Count > 0)
        //        {
        //            DataRow dr = dt.NewRow();
        //            dr[0] = "--Select--";

        //            dt.Rows.InsertAt(dr, 0);

        //            cbDept.DataSource = dt;
        //            cbDept.DisplayMember = "dept_desc";
        //            cbDept.ValueMember = "DeptCode";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        objSQLdb = null;
        //        dt = null;

        //    }

        //}
        //private void FillDesigComboBox()
        //{
        //    objSQLdb = new SQLDB();
        //    DataTable dt = new DataTable();
        //    cbDesig.DataSource = null;
        //    try
        //    {
        //        if (cbDept.SelectedIndex > 0)
        //        {
        //            string[] strDeptData = cbDept.SelectedValue.ToString().Split('@');

        //            string strCmd = "SELECT desig_desc, desig_code FROM DESIG_MAS "+
        //                            " WHERE dept_code=" + Convert.ToInt32(strDeptData[1]) + 
        //                            " ORDER BY desig_desc";
        //            dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
        //        }

        //        if (dt.Rows.Count > 0)
        //        {
        //            DataRow dr = dt.NewRow();
        //            dr[0] = "--Select--";

        //            dt.Rows.InsertAt(dr, 0);

        //            cbDesig.DataSource = dt;
        //            cbDesig.DisplayMember = "desig_desc";
        //            cbDesig.ValueMember = "desig_code";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        objSQLdb = null;
        //        dt = null;

        //    }
        //}


        private void cbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLocation.SelectedIndex > 0)
            {                
                FillLogicalBranch();
                FillEmployeeData();              
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
       
        private void GetAuditEmployeeName()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            if (txtAuditByEcode.Text != "")
            {
                try
                {
                    string strCmd = "SELECT MEMBER_NAME+'('+DESIG+')' EName FROM EORA_MASTER "+
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
        //private void GetConcernEmpName()
        //{
        //    objSQLdb = new SQLDB();
        //    DataTable dt = new DataTable();
        //    if (txtConcernEcode.Text != "")
        //    {
        //        try
        //        {
        //            string strCmd = "SELECT MEMBER_NAME EName,DESIG,dept_name+'@'+CAST(dept_code AS VARCHAR) DeptName,DESG_ID "+
        //                            " FROM EORA_MASTER " +
        //                            " INNER JOIN Dept_Mas DM ON DM.dept_code=DEPT_ID "+  
        //                            " WHERE ECODE=" + Convert.ToInt32(txtConcernEcode.Text) + "";
        //            dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {                        
                        
        //                txtConcernName.Text = dt.Rows[0]["EName"].ToString();
        //                cbDept.SelectedValue = dt.Rows[0]["DeptName"].ToString();
        //                cbDesig.SelectedValue = dt.Rows[0]["DESG_ID"].ToString();

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.ToString());
        //        }
        //        finally
        //        {
        //            objSQLdb = null;
        //            dt = null;
        //        }
        //    }
        //    else
        //    {
        //        txtConcernName.Text = "";
        //        cbDept.SelectedIndex = 0;
        //        cbDesig.SelectedIndex = -1;
        //    }

        //}

        //private void GetMgntEmpName()
        //{
        //    objSQLdb = new SQLDB();
        //    DataTable dt = new DataTable();
        //    if (txtEcodeSearch.Text != "")
        //    {
        //        try
        //        {
        //            string strCmd = "SELECT MEMBER_NAME EName,DESIG+'@'+CAST(DESG_ID AS VARCHAR) Desig,dept_name+'@'+ CAST(DM.dept_code AS VARCHAR) AS Dept " +
        //                            "  FROM EORA_MASTER EM " +
        //                            " INNER JOIN Dept_Mas DM ON DM.dept_code=DEPT_ID " +
        //                            "  WHERE ECODE=" + Convert.ToInt32(txtEcodeSearch.Text) + "";

        //            dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

        //            if (dt.Rows.Count > 0)
        //            {
        //                txtEname.Text = dt.Rows[0]["EName"].ToString();
        //                txtEname.Tag = dt.Rows[0]["Desig"].ToString().Split('@')[0];
        //            }
        //            else
        //            {
        //                txtEname.Text = "";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.ToString());
        //        }
        //        finally
        //        {
        //            objSQLdb = null;
        //            dt = null;
        //        }
        //    }
        //    else
        //    {
        //        txtEname.Text = "";
        //    }

        //}

        private string GenerateMisConId()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string sQueryId = "";
            try
            {
                string strCmd = "SELECT ISNULL(MAX(HMH_TRN_ID),0)+1 MisConId FROM HR_MISCONDUCT_HEAD";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    sQueryId = dt.Rows[0]["MisConId"].ToString();
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

            return sQueryId;

        }

        private bool CheckData()
        {
            AutoValidate = AutoValidate.EnableAllowFocusChange;

            bool bFlag = true;
            //if (txtDevationAmt.Text == "")
            //{
            //    txtDevationAmt.Text = "0";
            //    //bFlag = false;
            //    //MessageBox.Show("Please Enter Deviation Amount", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    //txtDevationAmt.Focus();
            //}
            //if (txtRecoveredAmt.Text == "")
            //{
            //    txtRecoveredAmt.Text = "0";
            //    //bFlag = false;
            //    //MessageBox.Show("Please Enter Recovered Amount", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    //txtRecoveredAmt.Focus();
            //}
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
            if (cbLocation.SelectedIndex == 0)
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

            if (cbZones.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Zone", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbZones.Focus();
                return bFlag;
            }

            if (cbRegion.SelectedIndex == 0 || cbRegion.SelectedIndex==-1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Region", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbRegion.Focus();
                return bFlag;
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

            btnSave.Enabled = true;
            btnClear.Enabled = true;

            return bFlag;


        }

        private bool CheckDetails()
        {
            bool flag = true;
            AutoValidate = AutoValidate.EnableAllowFocusChange;

            //if (gvDepartment.Rows.Count == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Add Department Details", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cbDeptAdd.Focus();
            //}
            //else if (gvEmployeeDetls.Rows.Count == 0)
            //{
            //    flag = false;
            //    MessageBox.Show("Please Add Employee Details", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtEcodeAdd.Focus();
            //}

            if (rtbAuditPoint.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Audit Point", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                rtbAuditPoint.Focus();
                return flag;
            }                

            if (cbDeviationType.SelectedIndex == 0 || cbDeviationType.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select DeviationType", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbDeviationType.Focus();
                return flag;
            }
            if (cbSubDeviationType.SelectedIndex == 0 || cbSubDeviationType.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Sub DeviationType", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbSubDeviationType.Focus();
                return flag;
            }
            if (cbMisconduct.SelectedIndex == 0 || cbMisconduct.SelectedIndex == -1)
            {
                flag = false;
                MessageBox.Show("Please Select Misconduct Yes/No", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMisconduct.Focus();
                return flag;
            }
            if (cbPptPoint.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select  PPT Point Yes/No", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbPptPoint.Focus();
                return flag;
            }

            if (cbMgntPoint.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select  Management Point Yes/No", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMgntPoint.Focus();
                return flag;
            }
            if (cbMisconduct.SelectedIndex == 1)
            {
                if (gvEmployeeDetls.Rows.Count == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Add Concern Person Details", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bntAddEmpDetails.Focus();
                    return flag;
                }
               
            }
            if (cbStatus.SelectedIndex == 1)
            {
                if (txtUnsolvedReason.Text.Length == 0)
                {
                    flag = false;
                    MessageBox.Show("Please Enter Unsolved Reason", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUnsolvedReason.Focus();
                    return flag;
                }
            }       
            
            return flag;
        }

        //private void CheckExistId()
        //{
        //    objSQLdb = new SQLDB();
        //    DataTable dt = new DataTable();
        //    string strCmd = "";

        //    try
        //    {
        //        if (flagUpdate == false)
        //        {
        //            strCmd = "SELECT * FROM HR_MISCONDUCT_HEAD WHERE HMH_TRN_ID=" + txtMisCondId.Text + "";
        //            dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {                       
        //                MessageBox.Show("Already Data Exists On this Id", "Audit-DataBank", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                txtMisCondId.Text = GenerateMisConId().ToString();             
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}


        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();

            if (CheckDetails() == true)
            {
                if (SaveMisConHeadDetails() > 0)
                {
                    SaveMisconEmpDetails();
                    SaveMisConDeptDetails();

                    SaveMisCondExplDetails();
                    SaveRecoveryModeDetails();
                    SaveActualRecoveryAmtDetails();
                    SaveMgntEmpDetails();
                    SaveImageDetails();

                    MessageBox.Show("Data Saved SucessFully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    flagUpdate = false;

                    dtExplDetl.Rows.Clear();
                    //txtAuditByEcode.Text = "";
                    //txtAuditName.Text = "";
                    gvExplDetails.Rows.Clear();
                    gvDepartment.Rows.Clear();
                    gvEmployeeDetls.Rows.Clear();
                    dtActualRecDetl.Rows.Clear();
                    dtModeOfRecDetl.Rows.Clear();
                    gvModeOfRecDetl.Rows.Clear();
                    gvActualRecDetails.Rows.Clear();
                    gvDocumentDetl.Rows.Clear();
                    cbMisconduct.SelectedIndex = 2;
                    txtDevationAmt.Text = "";
                    txtRecoveredAmt.Text = "";
                    txtUnsolvedReason.Text = "";
                    cbPptPoint.SelectedIndex = 2;
                    grp1.Visible = true;
                    grp2.Visible = false;
                    txtEnameSearch.Text = "";
                    gvMgntEmpDetl.Rows.Clear();
                    txtMgntRemarks.Text = "";

                    rtbAuditPoint.Text = "";
                    gvMgntEmpDetl.Rows.Clear();
                    cbMgntPoint.SelectedIndex = 2;
                    txtMisCondId.Text = GenerateMisConId().ToString();


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
            string[] sDept =new string[]{"0"};
            string strDevData = "";           

            try
            {
                string[] strData = null;
                strData = cbLocation.SelectedValue.ToString().Split('@');

                if (cbDeviationType.SelectedIndex > 0)
                {
                    strDevData = cbDeviationType.SelectedValue.ToString().Split('@')[0];
                }
                else
                {
                    strDevData = "";
                }                         


                string sDevDesc = rtbAuditPoint.Text.ToString().Replace("\'", "");
                if (cbLogicalBranch.SelectedIndex == -1)
                {
                    logicalBranch = "";
                }
                else
                {
                    logicalBranch = cbLogicalBranch.SelectedValue.ToString();
                }
                if (txtDevationAmt.Text == "")
                {
                    txtDevationAmt.Text = "0";
                }
                if (txtRecoveredAmt.Text == "")
                {
                    txtRecoveredAmt.Text = "0";
                }

                
                if (flagUpdate == true)
                {
                    strCommand = "UPDATE HR_MISCONDUCT_HEAD SET HMH_FIN_YEAR='" + cbFinYear.SelectedValue.ToString() +
                                  "', HMH_VISIT_MONTH='" + Convert.ToDateTime(dtpVisitMnth.Value).ToString("MMMyyyy").ToUpper() +
                                  "', HMH_DOC_MONTH='" + Convert.ToDateTime(dtpDoctMnth.Value).ToString("MMMyyyy").ToUpper() +
                                  "', HMH_COMP_CODE='" + cbCompany.SelectedValue.ToString() +
                                  "', HMH_STATE_CODE='" + strData[1] + "',HMH_BRANCH_CODE='" + strData[0] +
                                  "', HMH_LOG_BRANCH_CODE='" + logicalBranch +
                                  "', HMH_ZONE='" + cbZones.Text.ToString() + 
                                  "',HMH_REGION='" + cbRegion.Text.ToString() +
                                  "', HMH_AUDIT_POINT='" + rtbAuditPoint.Text.Replace("\'", " ") +
                                  //"',HMH_EXPLANATION='" + rtbExpln.Text.Replace("'", "") +
                                  "', HMH_STATUS='" + cbStatus.SelectedItem.ToString() +
                                  "', HMH_UNSOLVED_REASON='" + txtUnsolvedReason.Text.Replace("\'"," ") +
                                  "', HMH_DEVIATION_TYPE='" + strDevData +
                                  "', HMH_SUB_DEVIATION_TYPE='" + cbSubDeviationType.SelectedValue.ToString() +
                                  "', HMH_DEVIATION_AMT=" + txtDevationAmt.Text +
                                  ", HMH_RECOVERED_AMT=" + txtRecoveredAmt.Text +                                                                    
                                  ", HMH_AUDIT_BY=" + Convert.ToInt32(txtAuditByEcode.Text) +
                                  ", HMH_MODIFIED_BY='" + CommonData.LogUserId +
                                  "',HMH_MODIFIED_DATE=getdate() "+
                                  ",HMH_IS_MISCONDUCT='" + cbMisconduct.Text.ToString() +
                                  "',HMH_IS_MGNT_POINT='" + cbMgntPoint.Text.ToString() +
                                  "',HMH_MGNT_DESC='"+ txtMgntRemarks.Text.ToString().Replace("'"," ") +
                                  "',HMH_IS_PPT_POINT='"+ cbPptPoint.Text.ToString() +
                                  "' WHERE HMH_TRN_ID=" + txtMisCondId.Text + "";


                }
                else if (flagUpdate == false)
                {
                    txtMisCondId.Text = GenerateMisConId().ToString();
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
                                                                     //", HMH_EXPLANATION " +
                                                                     ", HMH_STATUS " +
                                                                     ", HMH_UNSOLVED_REASON " +
                                                                     ", HMH_DEVIATION_TYPE " +
                                                                     ", HMH_SUB_DEVIATION_TYPE " +
                                                                     ", HMH_DEVIATION_AMT " +
                                                                     ", HMH_RECOVERED_AMT " +                                                                     
                                                                     ",HMH_AUDIT_BY " +
                                                                     ",HMH_CREATED_BY " +
                                                                     ",HMH_CREATED_DATE " +
                                                                     ",HMH_IS_MISCONDUCT " +
                                                                     ",HMH_IS_MGNT_POINT " +
                                                                     ",HMH_MGNT_DESC "+
                                                                     ",HMH_IS_PPT_POINT "+
                                                                     ")VALUES " +
                                                                     "('" + cbFinYear.SelectedValue.ToString() +
                                                                     "'," + Convert.ToInt32(txtMisCondId.Text) +
                                                                     ",'" + Convert.ToDateTime(dtpVisitMnth.Value).ToString("MMMyyyy").ToUpper() +
                                                                     "','" + Convert.ToDateTime(dtpDoctMnth.Value).ToString("MMMyyyy").ToUpper() +
                                                                     "','" + cbCompany.SelectedValue.ToString() +
                                                                     "','" + strData[1] + "','" + strData[0] +
                                                                     "','" + logicalBranch +
                                                                     "','" + cbZones.Text.ToString() +
                                                                     "','" + cbRegion.Text.ToString() +
                                                                     "',N'" + rtbAuditPoint.Text.Replace("\'", " ") +
                                                                     //"',N'" + rtbExpln.Text.Replace("\'", "") +
                                                                     "','" + cbStatus.SelectedItem.ToString() +
                                                                     "','" + txtUnsolvedReason.Text.Replace("\'"," ") +
                                                                     "','" + strDevData +
                                                                     "','" + cbSubDeviationType.SelectedValue.ToString() +
                                                                     "'," + Convert.ToInt32(txtDevationAmt.Text) +
                                                                     "," + Convert.ToInt32(txtRecoveredAmt.Text) +                                                                     
                                                                     "," + Convert.ToInt32(txtAuditByEcode.Text) +
                                                                     ",'" + CommonData.LogUserId +
                                                                     "',getdate(),'" + cbMisconduct.Text.ToString() +
                                                                     "','"+ cbMgntPoint.Text.ToString() +
                                                                     "','"+ txtMgntRemarks.Text.ToString().Replace("'"," ") +
                                                                     "','"+ cbPptPoint.Text.ToString() +"')";
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

        private int SaveMisconEmpDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            try
            {
                strCommand = "DELETE FROM HR_MISCONDUCT_INC_EMP WHERE HMIE_MISCONDUCT_ID= "+ Convert.ToInt32(txtMisCondId.Text) +" ";
                iRes = objSQLdb.ExecuteSaveData(strCommand);

                strCommand = "";

                if (gvEmployeeDetls.Rows.Count > 0)
                {
                    for (int i = 0; i < gvEmployeeDetls.Rows.Count; i++)
                    {
                        strCommand += "INSERT INTO HR_MISCONDUCT_INC_EMP(HMIE_MISCONDUCT_ID " +
                                                                           ", HMIE_ECODE " +
                                                                           ", HMIE_DESIG " +
                                                                           ", HMIE_DEPT " +
                                                                           ")VALUES "+
                                                                           "(" + Convert.ToInt32(txtMisCondId.Text) +
                                                                           "," + Convert.ToInt32(gvEmployeeDetls.Rows[i].Cells["Ecode"].Value) +
                                                                           "," + Convert.ToInt32(gvEmployeeDetls.Rows[i].Cells["DesigId"].Value) +
                                                                           "," + Convert.ToInt32(gvEmployeeDetls.Rows[i].Cells["EmpDeptId"].Value) + ")";
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

        private int SaveMisConDeptDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            try
            {
                strCommand = "DELETE FROM HR_MISCONDUCT_INC_DEPT WHERE HMID_MISCONDUCT_ID="+ Convert.ToInt32(txtMisCondId.Text) +" ";
                iRes = objSQLdb.ExecuteSaveData(strCommand);

                strCommand = "";

                if (gvDepartment.Rows.Count > 0)
                {
                    for (int i = 0; i < gvDepartment.Rows.Count; i++)
                    {

                        strCommand += "INSERT INTO HR_MISCONDUCT_INC_DEPT(HMID_MISCONDUCT_ID " +
                                                                              ", HMID_DEPT_ID " +
                                                                              ")VALUES "+
                                                                              "(" + Convert.ToInt32(txtMisCondId.Text) +
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
                        
                        strCommand += "INSERT INTO HR_MISCONDUCT_EXP_DETL(HMED_MISCONDUCT_ID "+
                                                                       ", HMED_SL_NO "+
                                                                       ", HMED_EORA_CODE " +
                                                                       ", HMED_DEPT_ID "+
                                                                       ", HMED_DESG_ID "+
                                                                       ", HMED_EXPLANATION "+
                                                                       ", HMED_CREATED_BY "+
                                                                       ", HMED_CREATED_DATE "+
                                                                       ")VALUES("+ Convert.ToInt32(txtMisCondId.Text) +
                                                                       ","+ Convert.ToInt32(gvExplDetails.Rows[i].Cells["SlNo_Expl"].Value)+
                                                                       ", "+ Convert.ToInt32(gvExplDetails.Rows[i].Cells["EmpCode_Expl"].Value) +
                                                                       ","+ Convert.ToInt32(gvExplDetails.Rows[i].Cells["DeptCode_Expl"].Value) +
                                                                       ","+ Convert.ToInt32(gvExplDetails.Rows[i].Cells["DesigCode_Expl"].Value) +
                                                                       ",'"+ gvExplDetails.Rows[i].Cells["Explanation"].Value.ToString() +
                                                                       "','"+ CommonData.LogUserId +
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

        private int SaveRecoveryModeDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            try
            {
                strCommand = "DELETE FROM HR_MISCONDUCT_RECOVERY_MODE WHERE HMMR_MISCONDUCT_ID=" + Convert.ToInt32(txtMisCondId.Text) + " ";                
                iRes = objSQLdb.ExecuteSaveData(strCommand);

                strCommand = "";
                iRes = 0;

                
                if (gvModeOfRecDetl.Rows.Count > 0)
                {
                    for (int i = 0; i < gvModeOfRecDetl.Rows.Count; i++)
                    {

                        strCommand += "INSERT INTO HR_MISCONDUCT_RECOVERY_MODE(HMMR_MISCONDUCT_ID " +
                                                                       ", HMMR_SL_NO " +
                                                                       ", HMMR_EORA_CODE " +
                                                                       ", HMMR_RECIEPT_TYPE " +
                                                                       ", HMMR_RECIEPT_DATE " +
                                                                       ", HMMR_RECIEPT_NO " +
                                                                       ", HMMR_AMT " +
                                                                       ", HMMR_REMARKS " +
                                                                       ")VALUES(" + Convert.ToInt32(txtMisCondId.Text) +
                                                                       "," + Convert.ToInt32(gvModeOfRecDetl.Rows[i].Cells["SlNo_Rec"].Value) +
                                                                       "," + Convert.ToInt32(gvModeOfRecDetl.Rows[i].Cells["RecEcode"].Value) +
                                                                       ",'" + gvModeOfRecDetl.Rows[i].Cells["ReceiptType"].Value.ToString() +
                                                                       "','" + Convert.ToDateTime(gvModeOfRecDetl.Rows[i].Cells["ReceiptDate"].Value).ToString("dd/MMM/yyyy") +
                                                                       "','" + gvModeOfRecDetl.Rows[i].Cells["ReceiptNo"].Value.ToString() +
                                                                       "','" + gvModeOfRecDetl.Rows[i].Cells["RecAmount"].Value.ToString() +
                                                                       "','" + gvModeOfRecDetl.Rows[i].Cells["RecRemarks"].Value.ToString() +"')";
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


        private int SaveActualRecoveryAmtDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            try
            {
                strCommand = "DELETE FROM HR_MISCONDUCT_RECOVERY_ACTUAL WHERE HMAR_MISCONDUCT_ID=" + Convert.ToInt32(txtMisCondId.Text) + " ";
                iRes = objSQLdb.ExecuteSaveData(strCommand);

                strCommand = "";
                iRes = 0;

                
                if (gvActualRecDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvActualRecDetails.Rows.Count; i++)
                    {

                        strCommand += "INSERT INTO HR_MISCONDUCT_RECOVERY_ACTUAL(HMAR_MISCONDUCT_ID " +
                                                                       ", HMAR_SL_NO " +
                                                                       ", HMAR_EORA_CODE " +
                                                                       ", HMAR_RECIEPT_TYPE " +
                                                                       ", HMAR_RECIEPT_DATE " +
                                                                       ", HMAR_RECIEPT_NO " +
                                                                       ", HMAR_AMT " +
                                                                       ", HMAR_REMARKS " +
                                                                       ")VALUES(" + Convert.ToInt32(txtMisCondId.Text) +
                                                                       "," + Convert.ToInt32(gvActualRecDetails.Rows[i].Cells["SlNo_Actual"].Value) +
                                                                       "," + Convert.ToInt32(gvActualRecDetails.Rows[i].Cells["ActualRecEcode"].Value) +
                                                                       ",'" + gvActualRecDetails.Rows[i].Cells["ActualRcptType"].Value.ToString() +
                                                                       "','" + Convert.ToDateTime(gvActualRecDetails.Rows[i].Cells["ActualRcptDate"].Value).ToString("dd/MMM/yyyy") +
                                                                       "','" + gvActualRecDetails.Rows[i].Cells["ActualRcptNo"].Value.ToString() +
                                                                       "','" + gvActualRecDetails.Rows[i].Cells["ActualRecAmount"].Value.ToString() +
                                                                       "','" + gvActualRecDetails.Rows[i].Cells["ActualRecRemarks"].Value.ToString() + "')";
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


        private int SaveMgntEmpDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            try
            {
                strCommand = "DELETE FROM HR_MISCONDUCT_MGNT_EMP WHERE HMME_MISCONDUCT_ID=" + Convert.ToInt32(txtMisCondId.Text) + " ";
                iRes = objSQLdb.ExecuteSaveData(strCommand);

                strCommand = "";
                iRes = 0;

                if (gvMgntEmpDetl.Rows.Count > 0)
                {
                    for (int i = 0; i < gvMgntEmpDetl.Rows.Count; i++)
                    {

                        strCommand += "INSERT INTO HR_MISCONDUCT_MGNT_EMP(HMME_MISCONDUCT_ID " +
                                                                       ", HMME_SL_NO " +
                                                                       ", HMME_EORA_CODE " +                                                                      
                                                                       ")VALUES(" + Convert.ToInt32(txtMisCondId.Text) +
                                                                       "," + Convert.ToInt32(gvMgntEmpDetl.Rows[i].Cells["Mgnt_Slno"].Value) +
                                                                       "," + Convert.ToInt32(gvMgntEmpDetl.Rows[i].Cells["MgntEcode"].Value) +
                                                                       ")";
                    }
                }

                if (strCommand.Length > 5)
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
                                                                    ", HMDD_SL_NO "+
                                                                    ", HMDD_DOC_NAME "+
                                                                    ", HMDD_DOC_DESC "+
                                                                    ", HMDD_DOC_IMAGE "+
                                                                    ", HMDD_CREATED_BY "+
                                                                    ", HMDD_CREATED_DATE "+
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




        void RestrictingCharacters(KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        void RestrictingDigits(KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

      

        private void txtAuditByEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            RestrictingCharacters(e);
        }
      
         

        private bool CheckDuplicateEmp()
        {
            bool flagCheck = true;
            string[] strEname;
            string sEcode = "";

            if (gvEmployeeDetls.Rows.Count > 0)
            {
                for (int i = 0; i < gvEmployeeDetls.Rows.Count; i++)
                {
                    if (cbEmployees.SelectedIndex > -1)
                    {
                        strEname = cbEmployees.SelectedValue.ToString().Split('-');
                        sEcode = cbEmployees.SelectedValue.ToString().Split('-')[0];
                    }

                    if (sEcode.Equals(gvEmployeeDetls.Rows[i].Cells["Ecode"].Value.ToString()))
                    {
                        flagCheck = false;
                        MessageBox.Show("Already Exists", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return flagCheck;
                        
                    }
                }
            }
            return flagCheck;
        }

        private bool CheckDuplicateDept()
        {
            bool flag = true;

            for (int i = 0; i < gvDepartment.Rows.Count; i++)
            {
                string[] strDept = cbDeptAdd.SelectedValue.ToString().Split('@'); 

                if (strDept[1].Equals(gvDepartment.Rows[i].Cells["DeptId"].Value.ToString()))
                {
                    flag = false;
                    MessageBox.Show("Already Exists","SSCRM",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                    break;
                }
            }

            return flag;
        }



        private bool CheckDuplicateMgntEmp()
        {
            bool flag = true;

            if (gvMgntEmpDetl.Rows.Count > 0)
            {
                for (int i = 0; i < gvMgntEmpDetl.Rows.Count; i++)
                {

                    if (cbMgntEmployees.SelectedValue.ToString().Split('@')[0].Equals(gvMgntEmpDetl.Rows[i].Cells["MgntEcode"].Value.ToString()))
                    {
                        flag = false;
                        MessageBox.Show("Already Exists", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return flag;

                    }
                }
            }

            return flag;
        }

       


        private void bntAddEmpDetails_Click(object sender, EventArgs e)
        {
            int intRow = 1;

            if (cbEmployees.SelectedIndex>-1)
            {
                if (CheckDuplicateEmp()==true)
                {                   
                   
                    DataGridViewRow tempRow = new DataGridViewRow();
                    if (gvEmployeeDetls.Rows.Count > 0)
                    {
                        DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();

                        cellSlNo.Value = gvEmployeeDetls.Rows.Count + 1;
                        tempRow.Cells.Add(cellSlNo);

                    }
                    else
                    {

                        DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                        cellSlNo.Value = intRow;
                        intRow = intRow + 1;
                        tempRow.Cells.Add(cellSlNo);
                    }

                    DataGridViewCell cellEmpEcode = new DataGridViewTextBoxCell();
                    cellEmpEcode.Value = cbEmployees.SelectedValue.ToString().Split('-')[0];
                    tempRow.Cells.Add(cellEmpEcode);

                    DataGridViewCell cellEmpDeptId = new DataGridViewTextBoxCell();
                    cellEmpDeptId.Value = cbEmployees.SelectedValue.ToString().Split('-')[3];
                    tempRow.Cells.Add(cellEmpDeptId);

                    DataGridViewCell cellDesigId = new DataGridViewTextBoxCell();
                    cellDesigId.Value = cbEmployees.SelectedValue.ToString().Split('-')[2];
                    tempRow.Cells.Add(cellDesigId);


                    DataGridViewCell cellEmpName = new DataGridViewTextBoxCell();
                    cellEmpName.Value = cbEmployees.SelectedValue.ToString().Split('-')[1];
                    tempRow.Cells.Add(cellEmpName);

                    //DataGridViewCell cellEmpDept = new DataGridViewTextBoxCell();
                    //cellEmpDept.Value = strDept[0];
                    //tempRow.Cells.Add(cellEmpDept);

                    DataGridViewCell cellEmpDesig = new DataGridViewTextBoxCell();
                    cellEmpDesig.Value = cbEmployees.SelectedValue.ToString().Split('-')[4];
                    tempRow.Cells.Add(cellEmpDesig);


                    gvEmployeeDetls.Rows.Add(tempRow);
                }

                txtEnameSearch.Text = "";
               
            }
            else
            {
                MessageBox.Show("Please Select Employee Name", "Misconduct Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbEmployees.Focus();
               

            }

        }
     



        #region "GRIDVIEW DATA DELETING"
      

        private void gvEmployeeDetls_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvEmployeeDetls.Columns["Del"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    DataGridViewRow dgvr = gvEmployeeDetls.Rows[e.RowIndex];
                    gvEmployeeDetls.Rows.Remove(dgvr);
                }
                if (gvEmployeeDetls.Rows.Count > 0)
                {
                    for (int i = 0; i < gvEmployeeDetls.Rows.Count; i++)
                    {
                        gvEmployeeDetls.Rows[i].Cells["SlNo_Emp"].Value = (i + 1).ToString();
                    }
                }
            }

        }

        #endregion

        private void btnClear_Click(object sender, EventArgs e)
        {
                      
            gvEmployeeDetls.Rows.Clear();
            txtEnameSearch.Text = "";
            txtRecoveredAmt.Text = "";
            txtDevationAmt.Text = "";

            dtModeOfRecDetl.Rows.Clear();
            dtActualRecDetl.Rows.Clear();
            gvActualRecDetails.Rows.Clear();
            gvModeOfRecDetl.Rows.Clear();
           
            cbMisconduct.SelectedIndex = 2;
           
            cbDeviationType.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;
            txtUnsolvedReason.Text = "";

            gvMgntEmpDetl.Rows.Clear();
            cbMgntPoint.SelectedIndex = 2;
            cbPptPoint.SelectedIndex = 2;

           // txtMisCondId.Text = GenerateMisConId().ToString();       
            
        }
           
       
        #region "GET DATA FOR UPDATE"
        private void GetMisConHeadDetails(Int32 MisConId)
        {
            objHRdb = new HRInfo();
            DataTable dt ;
            DataTable dtEmp;
            DataTable dtDept;
            DataTable dtExplanationDetl;
            DataTable dtRecoveryMode;
            DataTable dtActualRecovery;
            DataTable dtMgntEmpDetl;
            DataTable dtImageDetl;
            Hashtable ht;
            try
            {
                ht = objHRdb.GetMisconductDetails(MisConId);
                dt = (DataTable)ht["MisconductHeadDetails"];
                dtEmp = (DataTable)ht["MisconductEmpDetails"];
                dtDept = (DataTable)ht["MisconductDeptDetails"];
                dtExplanationDetl = (DataTable)ht["MisconductExplDetails"];
                dtRecoveryMode = (DataTable)ht["MisCondRecoveryDetl"];
                dtActualRecovery = (DataTable)ht["MisCondActualRecovery"];
                dtMgntEmpDetl = (DataTable)ht["MisCondMgntEmpDetl"];
                dtImageDetl = (DataTable)ht["MisCondImageDetl"];

                if (dt.Rows.Count > 0)
                {
                    flagUpdate = true;

                    FillBranchDetails();

                    if (dt.Rows[0]["DeleteFlag"].ToString().Equals("DELETED"))
                    {
                        cbCompany.SelectedIndex = 0;

                        cbLocation.SelectedIndex = -1;
                        cbLogicalBranch.SelectedIndex = -1;
                        cbDeviationType.SelectedIndex = 0;
                        cbStatus.SelectedIndex = 0;
                        cbZones.SelectedIndex = 0;
                        cbRegion.SelectedIndex = -1;
                        gvDocumentDetl.Rows.Clear();
                        txtRecoveredAmt.Text = "";
                        txtDevationAmt.Text = "";
                        txtEnameSearch.Text = "";
                        
                        dtActualRecDetl.Rows.Clear();
                        dtModeOfRecDetl.Rows.Clear();
                        gvModeOfRecDetl.Rows.Clear();
                        gvActualRecDetails.Rows.Clear();

                        cbFinYear.SelectedIndex = 0;
                        dtpDoctMnth.Value = DateTime.Today;
                        dtpVisitMnth.Value = DateTime.Today;

                        gvMgntEmpDetl.Rows.Clear();

                       
                        txtUnsolvedReason.Text = "";
                        cbDeptAdd.SelectedIndex = 0;
                        gvDepartment.Rows.Clear();
                        gvEmployeeDetls.Rows.Clear();
                        txtAuditByEcode.Text = "";
                        txtAuditName.Text = "";
                        cbMisconduct.SelectedIndex = 0;
                        cbPptPoint.SelectedIndex = 2;

                        txtMgntRemarks.Text = "";
                        cbMgntPoint.SelectedIndex = 2;

                        gvExplDetails.Rows.Clear();

                        MessageBox.Show("This Point Is Deleted By  - "+ dt.Rows[0]["DeleteBy"].ToString());
                        txtMisCondId.Text = GenerateMisConId().ToString();
                        flagUpdate = false;
                        return;
                    }
                    else
                    {
                        
                        flagUpdate = true;

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

                        if (dt.Rows[0]["DevType"].ToString() != "")
                            cbDeviationType.SelectedValue = dt.Rows[0]["DevType"].ToString();
                        else
                            cbDeviationType.SelectedIndex = 0;

                        cbSubDeviationType.SelectedValue = dt.Rows[0]["SubDevType"].ToString();
                        rtbAuditPoint.Text = dt.Rows[0]["AuditPoint"].ToString();
                        txtDevationAmt.Text = dt.Rows[0]["DevAmt"].ToString();
                        txtAuditByEcode.Text = dt.Rows[0]["Audit_Ecode"].ToString();
                        txtAuditName.Text = dt.Rows[0]["AuditName"].ToString();
                        txtRecoveredAmt.Text = dt.Rows[0]["RecoveredAmt"].ToString();

                        string Status = dt.Rows[0]["Status"].ToString();

                        cbMgntPoint.Text = dt.Rows[0]["IsMgnt"].ToString();
                        cbPptPoint.Text = dt.Rows[0]["PPTPoint"].ToString();
                        txtMgntRemarks.Text = dt.Rows[0]["MgntDesc"].ToString();

                        cbMisconduct.Text = dt.Rows[0]["Misconduct"].ToString();

                        if (Status == "SOLVED")
                        {
                            cbStatus.SelectedIndex = 0;
                        }
                        else if (Status == "UNSOLVED")
                        {
                            cbStatus.SelectedIndex = 1;
                        }
                        else if (Status == "SOLVED-FOLLOWUP")
                        {
                            cbStatus.SelectedIndex = 2;
                        }

                        txtUnsolvedReason.Text = dt.Rows[0]["UnsolvedReason"].ToString();
                        //rtbExpln.Text = dt.Rows[0]["Explanation"].ToString();

                        FillMisConEmpData(dtEmp);
                        FillMisConDeptDetails(dtDept);
                        FillEmpMisCondExplDetails(dtExplanationDetl);
                        FillMisCondRecModeDetails(dtRecoveryMode);
                        FillMisCondActualRecoveryDetails(dtActualRecovery);
                        FillMisConMgntEmpDetl(dtMgntEmpDetl);
                        FillImageDetails(dtImageDetl);
                    }

                }
                else
                {
                    //cbCompany.SelectedIndex = 0;

                    flagUpdate = false;
                    //cbLocation.SelectedIndex = -1;

                    //if(cbLogicalBranch.Items.Count!=0)
                    //cbLogicalBranch.SelectedIndex = 0;
                    cbDeviationType.SelectedIndex = 0;
                    cbStatus.SelectedIndex = 0;
                    //cbZones.SelectedIndex = 0;
                    //cbRegion.SelectedIndex = -1;
                    txtRecoveredAmt.Text = "";
                    txtDevationAmt.Text = "";
                    txtEnameSearch.Text = "";

                    gvDocumentDetl.Rows.Clear();
                    txtMgntRemarks.Text = "";
                    gvMgntEmpDetl.Rows.Clear();
                    cbMgntPoint.SelectedIndex = 2;

                    gvDepartment.Rows.Clear();
                    dtExplDetl.Rows.Clear();
                    gvExplDetails.Rows.Clear();

                    //cbFinYear.SelectedIndex = 0;
                    //dtpDoctMnth.Value = DateTime.Today;
                    //dtpVisitMnth.Value = DateTime.Today;
                   
                    rtbAuditPoint.Text = "";
                    txtUnsolvedReason.Text = "";
                   
                    gvEmployeeDetls.Rows.Clear();
                    txtAuditByEcode.Text = "";
                    txtAuditName.Text = "";
                    dtActualRecDetl.Rows.Clear();
                    dtModeOfRecDetl.Rows.Clear();
                    gvActualRecDetails.Rows.Clear();
                    gvModeOfRecDetl.Rows.Clear();

                    cbMisconduct.SelectedIndex = 2;
                    cbPptPoint.SelectedIndex = 2;

                    txtMisCondId.Text = GenerateMisConId().ToString();
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


        private void FillMisConEmpData(DataTable dtEmpData)
        {
            gvEmployeeDetls.Rows.Clear();
            if (txtMisCondId.Text.Length > 0)
            {
                try
                {

                    if (dtEmpData.Rows.Count > 0)
                    {                        
                        for (int i = 0; i < dtEmpData.Rows.Count; i++)
                        {
                            gvEmployeeDetls.Rows.Add();
                            
                            gvEmployeeDetls.Rows[i].Cells["SlNo_Emp"].Value = (i + 1).ToString();
                            gvEmployeeDetls.Rows[i].Cells["Ecode"].Value = dtEmpData.Rows[i]["Emp_Ecode"].ToString();
                            gvEmployeeDetls.Rows[i].Cells["EmpDeptId"].Value = dtEmpData.Rows[i]["Dept_Code"].ToString();
                            gvEmployeeDetls.Rows[i].Cells["DesigId"].Value = dtEmpData.Rows[i]["Desig_Code"].ToString();
                            gvEmployeeDetls.Rows[i].Cells["EmpName"].Value = dtEmpData.Rows[i]["Emp_Name"].ToString();                            
                            gvEmployeeDetls.Rows[i].Cells["Desig"].Value = dtEmpData.Rows[i]["DESIG"].ToString();

                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
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

        private void FillMisCondRecModeDetails(DataTable dtRecMode)
        {           
            dtModeOfRecDetl.Rows.Clear();
            gvModeOfRecDetl.Rows.Clear();

            try
            {
                if (dtRecMode.Rows.Count > 0)
                {

                    for (int i = 0; i < dtRecMode.Rows.Count; i++)
                    {
                        
                        dtModeOfRecDetl.Rows.Add(new object[]{"-1",dtRecMode.Rows[i]["EmpCode"].ToString(),
                                                                    dtRecMode.Rows[i]["EmpName"].ToString(),
                                                                    dtRecMode.Rows[i]["ReceiptType"].ToString(),
                                                                    Convert.ToDateTime(dtRecMode.Rows[i]["ReceiptDate"]).ToString("dd/MMM/yyyy"),
                                                                    dtRecMode.Rows[i]["ReceiptNo"].ToString(),
                                                                    dtRecMode.Rows[i]["Amount"].ToString(),
                                                                    dtRecMode.Rows[i]["Remarks"].ToString()});
                        GetModeOfRecoveryDetails();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FillMisCondActualRecoveryDetails(DataTable dtActRec)
        {
            gvActualRecDetails.Rows.Clear();
            dtActualRecDetl.Rows.Clear();
            try
            {
                if (dtActRec.Rows.Count > 0)
                {

                    for (int i = 0; i < dtActRec.Rows.Count; i++)
                    {

                        dtActualRecDetl.Rows.Add(new object[]{"-1",dtActRec.Rows[i]["EmpCode"].ToString(),
                                                                    dtActRec.Rows[i]["EmpName"].ToString(),
                                                                    dtActRec.Rows[i]["ReceiptType"].ToString(),
                                                                    Convert.ToDateTime(dtActRec.Rows[i]["ReceiptDate"]).ToString("dd/MMM/yyyy"),
                                                                    dtActRec.Rows[i]["ReceiptNo"].ToString(),
                                                                    dtActRec.Rows[i]["Amount"].ToString(),
                                                                    dtActRec.Rows[i]["Remarks"].ToString()});
                        GetActualRecoveryDetails();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FillMisConMgntEmpDetl(DataTable dtMgntEmpData)
        {
            gvMgntEmpDetl.Rows.Clear();

            if (txtMisCondId.Text.Length > 0)
            {
                try
                {


                    if (dtMgntEmpData.Rows.Count > 0)
                    {                       
                        for (int i = 0; i < dtMgntEmpData.Rows.Count; i++)
                        {
                            gvMgntEmpDetl.Rows.Add();

                            gvMgntEmpDetl.Rows[i].Cells["Mgnt_Slno"].Value = (i + 1).ToString();
                            gvMgntEmpDetl.Rows[i].Cells["MgntEcode"].Value = dtMgntEmpData.Rows[i]["EmpCode"].ToString();
                            gvMgntEmpDetl.Rows[i].Cells["MgntEmpName"].Value = dtMgntEmpData.Rows[i]["EmpName"].ToString();
                            gvMgntEmpDetl.Rows[i].Cells["MgntEmpDesig"].Value = dtMgntEmpData.Rows[i]["EmpDesig"].ToString();

                        }
                    }
                    else
                    {
                        gvMgntEmpDetl.Rows.Clear();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }


        #endregion
             
        
        #region "GRID VIEW DETAILS"

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

        public void GetModeOfRecoveryDetails()
        {
            int intRow = 1;
            gvModeOfRecDetl.Rows.Clear();

            try
            {
                if (dtModeOfRecDetl.Rows.Count > 0)
                {

                    for (int i = 0; i < dtModeOfRecDetl.Rows.Count; i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        dtModeOfRecDetl.Rows[i]["SlNo_Rec"] = intRow;
                        tempRow.Cells.Add(cellSLNO);                   


                        DataGridViewCell cellEmpCode = new DataGridViewTextBoxCell();
                        cellEmpCode.Value = dtModeOfRecDetl.Rows[i]["RecEcode"].ToString();
                        tempRow.Cells.Add(cellEmpCode);


                        DataGridViewCell cellEmpName = new DataGridViewTextBoxCell();
                        cellEmpName.Value = dtModeOfRecDetl.Rows[i]["RecEmpName"].ToString();
                        tempRow.Cells.Add(cellEmpName);

                        DataGridViewCell cellReceiptType = new DataGridViewTextBoxCell();
                        cellReceiptType.Value = dtModeOfRecDetl.Rows[i]["ReceiptType"].ToString();
                        tempRow.Cells.Add(cellReceiptType);

                        DataGridViewCell cellReceiptDate = new DataGridViewTextBoxCell();
                        cellReceiptDate.Value = dtModeOfRecDetl.Rows[i]["ReceiptDate"].ToString();
                        tempRow.Cells.Add(cellReceiptDate);

                        DataGridViewCell cellReceiptNo = new DataGridViewTextBoxCell();
                        cellReceiptNo.Value = dtModeOfRecDetl.Rows[i]["ReceiptNo"].ToString();
                        tempRow.Cells.Add(cellReceiptNo);

                        DataGridViewCell cellRecAmount = new DataGridViewTextBoxCell();
                        cellRecAmount.Value = dtModeOfRecDetl.Rows[i]["RecAmount"].ToString();
                        tempRow.Cells.Add(cellRecAmount);

                        DataGridViewCell cellRecRemarks = new DataGridViewTextBoxCell();
                        cellRecRemarks.Value = dtModeOfRecDetl.Rows[i]["RecRemarks"].ToString();
                        tempRow.Cells.Add(cellRecRemarks);


                        intRow = intRow + 1;
                        gvModeOfRecDetl.Rows.Add(tempRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void GetActualRecoveryDetails()
        {
            int intRow = 1;
            gvActualRecDetails.Rows.Clear();

            try
            {
                if (dtActualRecDetl.Rows.Count > 0)
                {

                    for (int i = 0; i < dtActualRecDetl.Rows.Count; i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        dtActualRecDetl.Rows[i]["SlNo_Actual"] = intRow;
                        tempRow.Cells.Add(cellSLNO);


                        DataGridViewCell cellEmpCode = new DataGridViewTextBoxCell();
                        cellEmpCode.Value = dtActualRecDetl.Rows[i]["ActualRecEcode"].ToString();
                        tempRow.Cells.Add(cellEmpCode);


                        DataGridViewCell cellEmpName = new DataGridViewTextBoxCell();
                        cellEmpName.Value = dtActualRecDetl.Rows[i]["ActualRecName"].ToString();
                        tempRow.Cells.Add(cellEmpName);

                        DataGridViewCell cellReceiptType = new DataGridViewTextBoxCell();
                        cellReceiptType.Value = dtActualRecDetl.Rows[i]["ActualRcptType"].ToString();
                        tempRow.Cells.Add(cellReceiptType);

                        DataGridViewCell cellReceiptDate = new DataGridViewTextBoxCell();
                        cellReceiptDate.Value = dtActualRecDetl.Rows[i]["ActualRcptDate"].ToString();
                        tempRow.Cells.Add(cellReceiptDate);

                        DataGridViewCell cellReceiptNo = new DataGridViewTextBoxCell();
                        cellReceiptNo.Value = dtActualRecDetl.Rows[i]["ActualRcptNo"].ToString();
                        tempRow.Cells.Add(cellReceiptNo);

                        DataGridViewCell cellRecAmount = new DataGridViewTextBoxCell();
                        cellRecAmount.Value = dtActualRecDetl.Rows[i]["ActualRecAmount"].ToString();
                        tempRow.Cells.Add(cellRecAmount);

                        DataGridViewCell cellRecRemarks = new DataGridViewTextBoxCell();
                        cellRecRemarks.Value = dtActualRecDetl.Rows[i]["ActualRecRemarks"].ToString();
                        tempRow.Cells.Add(cellRecRemarks);


                        intRow = intRow + 1;
                        gvActualRecDetails.Rows.Add(tempRow);
                    }
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
            //cbCompany.SelectedIndex = 0;

            //dtExplDetl.Rows.Clear();
            //gvExplDetails.Rows.Clear();

            ////cbLocation.SelectedIndex = -1;
            //if (cbLogicalBranch.Items.Count != 0)
            //    cbLogicalBranch.SelectedIndex = 0;

          

            ////cbFinYear.SelectedIndex = 0;
            ////dtpDoctMnth.Value = DateTime.Today;
            ////dtpVisitMnth.Value = DateTime.Today;

            ////rtbAuditPoint.Text = "";


            //gvDepartment.Rows.Clear();
            //txtAuditByEcode.Text = "";
            //txtAuditName.Text = "";
           
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
            MisconductExplanationDetl MisExplDetl = new MisconductExplanationDetl("HO");
            MisExplDetl.objMisconductForm = this;
            MisExplDetl.ShowDialog();
        }

        private void cbDeviationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSubDeviationType();
        }
             
      
        private void txtDevationAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && e.KeyChar != '.')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtRecoveredAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && e.KeyChar != '.')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtConcernEcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

  

        private void btnAddRecoveryDetl_Click(object sender, EventArgs e)
        {
            sCompCode = "";
            sBranCode = "";
            sDocMonth = "";
            if (cbLocation.SelectedIndex > 0)
            {
                sCompCode = cbCompany.SelectedValue.ToString();
                sBranCode = cbLocation.SelectedValue.ToString().Split('@')[0];
                sDocMonth = Convert.ToDateTime(dtpDoctMnth.Value).ToString("MMMyyyy").ToUpper();

                AddAmountRecoveryDetails AmtRecDetl = new AddAmountRecoveryDetails("AmountRecovery",sCompCode,sBranCode,sDocMonth);
                AmtRecDetl.objMisconductForm = this;
                AmtRecDetl.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please Select Location","Audit Data Bank",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btnActualRecDetl_Click(object sender, EventArgs e)
        {
            sCompCode = "";
            sBranCode = "";
            sDocMonth = "";
            if (cbLocation.SelectedIndex > 0)
            {
                sCompCode = cbCompany.SelectedValue.ToString();
                sBranCode = cbLocation.SelectedValue.ToString().Split('@')[0];
                sDocMonth = Convert.ToDateTime(dtpDoctMnth.Value).ToString("MMMyyyy").ToUpper();

                AddAmountRecoveryDetails AmtRecDetl = new AddAmountRecoveryDetails("ActualAmountRecovery",sCompCode,sBranCode,sDocMonth);
                AmtRecDetl.objMisconductForm = this;
                AmtRecDetl.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please Select Location", "Audit Data Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region "GRID VIEW DETAILS EDITING AND DELETING"

        private void gvDepartment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
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
        }

        private void gvExplDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
               
                if (e.ColumnIndex == gvExplDetails.Columns["Edit_Expl"].Index)
                {

                    if (Convert.ToBoolean(gvExplDetails.Rows[e.RowIndex].Cells["Edit_Expl"].Selected) == true)
                    {

                        int SlNo = Convert.ToInt32(gvExplDetails.Rows[e.RowIndex].Cells[gvExplDetails.Columns["SlNo_Expl"].Index].Value);
                        DataRow[] dr = dtExplDetl.Select("SlNo_Expl=" + SlNo);

                        MisconductExplanationDetl MisExplDetl = new MisconductExplanationDetl("HO", dr);
                        MisExplDetl.objMisconductForm = this;
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

        private void gvModeOfRecDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvModeOfRecDetl.Columns["Edit_ModeOfRec"].Index)
                {

                    if (Convert.ToBoolean(gvModeOfRecDetl.Rows[e.RowIndex].Cells["Edit_ModeOfRec"].Selected) == true)
                    {

                        int SlNo = Convert.ToInt32(gvModeOfRecDetl.Rows[e.RowIndex].Cells[gvModeOfRecDetl.Columns["SlNo_Rec"].Index].Value);
                        DataRow[] dr = dtModeOfRecDetl.Select("SlNo_Rec=" + SlNo);

                        AddAmountRecoveryDetails AmtRecDetl = new AddAmountRecoveryDetails("AmountRecovery",dr);
                        AmtRecDetl.objMisconductForm = this;
                        AmtRecDetl.ShowDialog();

                    }
                }


                if (e.ColumnIndex == gvModeOfRecDetl.Columns["Del_ModOfRec"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvModeOfRecDetl.Rows[e.RowIndex].Cells[gvModeOfRecDetl.Columns["SlNo_Rec"].Index].Value);
                        DataRow[] dr = dtModeOfRecDetl.Select("SlNo_Rec=" + SlNo);
                        dtModeOfRecDetl.Rows.Remove(dr[0]);
                        GetModeOfRecoveryDetails();
                        MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
        }

        private void gvActualRecDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvActualRecDetails.Columns["Edit_Actual"].Index)
                {

                    if (Convert.ToBoolean(gvActualRecDetails.Rows[e.RowIndex].Cells["Edit_Actual"].Selected) == true)
                    {

                        int SlNo = Convert.ToInt32(gvActualRecDetails.Rows[e.RowIndex].Cells[gvActualRecDetails.Columns["SlNo_Actual"].Index].Value);
                        DataRow[] dr = dtActualRecDetl.Select("SlNo_Actual=" + SlNo);

                        AddAmountRecoveryDetails AmtRecDetl = new AddAmountRecoveryDetails("ActualAmountRecovery",dr);
                        AmtRecDetl.objMisconductForm = this;
                        AmtRecDetl.ShowDialog();

                    }
                }


                if (e.ColumnIndex == gvActualRecDetails.Columns["Del_Actual"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvActualRecDetails.Rows[e.RowIndex].Cells[gvActualRecDetails.Columns["SlNo_Actual"].Index].Value);
                        DataRow[] dr = dtActualRecDetl.Select("SlNo_Actual=" + SlNo);
                        dtActualRecDetl.Rows.Remove(dr[0]);
                        GetActualRecoveryDetails();
                        MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
        }

        #endregion


      

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            string strCommand = "";
            int iRes = 0;

            if (txtMisCondId.Text != "" && flagUpdate == true)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    try
                    {
                        strCommand = "UPDATE HR_MISCONDUCT_HEAD SET HMH_DELETE_FLAG='DELETED' " +
                                         ", HMH_DELETE_BY='" + CommonData.LogUserId +
                                         "',HMH_DELETED_DATE=getdate() " +
                                         " WHERE HMH_TRN_ID=" + txtMisCondId.Text + "";

                        if (strCommand.Length > 5)
                        {
                            iRes = objSQLdb.ExecuteSaveData(strCommand);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flagUpdate = false;
                        rtbAuditPoint.Text = "";
                        dtExplDetl.Rows.Clear();
                        gvExplDetails.Rows.Clear();
                        gvDepartment.Rows.Clear();
                        gvEmployeeDetls.Rows.Clear();
                        dtActualRecDetl.Rows.Clear();
                        dtModeOfRecDetl.Rows.Clear();
                        gvModeOfRecDetl.Rows.Clear();
                        gvActualRecDetails.Rows.Clear();
                        cbMisconduct.SelectedIndex = 2;
                        txtDevationAmt.Text = "";
                        txtRecoveredAmt.Text = "";
                        txtUnsolvedReason.Text = "";
                        txtEnameSearch.Text = "";
                        gvDocumentDetl.Rows.Clear();                       
                        grp1.Visible = true;
                        grp2.Visible = false;

                        txtMisCondId.Text = GenerateMisConId().ToString();
                    }
                    else
                    {
                        MessageBox.Show("Data Not Deleted", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void txtEnameSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != '\b')
            {
                if (!char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void FillHOEmployeeDetl()
        {
            objHRdb = new HRInfo();
            DataTable dt = new DataTable();
            cbMgntEmployees.DataBindings.Clear();

            
                try
                {

                    dt = objHRdb.GetHOEmployeesForAuditDataBank(txtEcodeSearch.Text.ToString()).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        //DataRow dr = dt.NewRow();
                        //dr[0] = "--Select--";
                        //dr[1] = "--Select--";

                        //dt.Rows.InsertAt(dr, 0);

                        cbMgntEmployees.DataSource = dt;
                        cbMgntEmployees.DisplayMember = "EmpName";
                        cbMgntEmployees.ValueMember = "EmpDetl";
                    }
                    else
                    {
                        cbMgntEmployees.DataBindings.Clear();
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

        private void FillEmployeeData()
        {
            objHRdb = new HRInfo();
            DataTable dt = new DataTable();
            cbEmployees.DataBindings.Clear();
            
            if (cbCompany.SelectedIndex > 0 && cbLocation.SelectedIndex > 0)
            {
                try
                {

                    dt = objHRdb.GetEmployeesForMisconduct(cbCompany.SelectedValue.ToString(), cbLocation.SelectedValue.ToString().Split('@')[0], Convert.ToDateTime(dtpDoctMnth.Value).ToString("MMMyyyy").ToUpper(), txtEnameSearch.Text.ToString()).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        //DataRow dr = dt.NewRow();
                        //dr[1] = "--Select--";
                        //dr[3] = "--Select--";

                        //dt.Rows.InsertAt(dr, 0);

                        cbEmployees.DataSource = dt;
                        cbEmployees.DisplayMember = "ENAME";
                        cbEmployees.ValueMember = "EmpDetl";
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
        }

        private void txtEnameSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEnameSearch.Text != "")
            {
                FillEmployeeData();
            }
        }

       

        private void txtAuditByEcode_KeyUp(object sender, KeyEventArgs e)
        {
            GetAuditEmployeeName();
        }

        private void dtpDoctMnth_ValueChanged(object sender, EventArgs e)
        {
            FillEmployeeData();
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            FillHOEmployeeDetl();

        }

        private void txtEcodeSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnAddMgntEmpDetl_Click(object sender, EventArgs e)
        {
            int intRow = 1;

            if (cbMgntEmployees.SelectedIndex > -1)
            {
                if (CheckDuplicateMgntEmp() == true)
                {

                    DataGridViewRow tempRow = new DataGridViewRow();

                    if (gvMgntEmpDetl.Rows.Count > 0)
                    {
                        DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                        cellSlNo.Value = gvMgntEmpDetl.Rows.Count + 1;
                        tempRow.Cells.Add(cellSlNo);

                    }
                    else
                    {
                        DataGridViewCell cellSlNoEmp = new DataGridViewTextBoxCell();
                        cellSlNoEmp.Value = intRow;
                        intRow = intRow + 1;
                        tempRow.Cells.Add(cellSlNoEmp);
                    }

                    DataGridViewCell cellEcode = new DataGridViewTextBoxCell();
                    cellEcode.Value = cbMgntEmployees.SelectedValue.ToString().Split('@')[0];
                    tempRow.Cells.Add(cellEcode);

                    DataGridViewCell cellEmpName = new DataGridViewTextBoxCell();
                    cellEmpName.Value = cbMgntEmployees.SelectedValue.ToString().Split('@')[1];
                    tempRow.Cells.Add(cellEmpName);

                    DataGridViewCell cellEmpDesig = new DataGridViewTextBoxCell();
                    cellEmpDesig.Value = cbMgntEmployees.SelectedValue.ToString().Split('@')[2];
                    tempRow.Cells.Add(cellEmpDesig);


                    gvMgntEmpDetl.Rows.Add(tempRow);
                }

                txtEcodeSearch.Text = "";
                //txtEcodeSearch.Focus();
            }

            else
            {
                MessageBox.Show("Please Select Emp Name", "Audit Data Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMgntEmployees.Focus();
            }
        }

        private void gvMgntEmpDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvMgntEmpDetl.Columns["Mgnt_Del"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    DataGridViewRow dgvr = gvMgntEmpDetl.Rows[e.RowIndex];
                    gvMgntEmpDetl.Rows.Remove(dgvr);
                }
                if (gvMgntEmpDetl.Rows.Count > 0)
                {
                    for (int i = 0; i < gvMgntEmpDetl.Rows.Count; i++)
                    {
                        gvMgntEmpDetl.Rows[i].Cells["Mgnt_Slno"].Value = (i + 1).ToString();
                    }
                }
            }
        }

        private void cbZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbZones.SelectedIndex > 0)
            {
                FillRegionsList();
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

      
        private void btnDisplay_Click(object sender, EventArgs e)
        {
            if (txtMisCondId.Text != "")
            {
                txtMisCondId.CausesValidation = true;
                flagUpdate = false;
                GetMisConHeadDetails(Convert.ToInt32(txtMisCondId.Text));
            }
            else
            {
                //cbCompany.SelectedIndex = 0;

                //cbLocation.SelectedIndex = -1;
                //cbLogicalBranch.SelectedIndex = -1;
                cbDeviationType.SelectedIndex = 0;
                cbStatus.SelectedIndex = 0;
                flagUpdate = false;
                txtRecoveredAmt.Text = "";
                txtDevationAmt.Text = "";
                txtEnameSearch.Text = "";

                dtActualRecDetl.Rows.Clear();
                dtModeOfRecDetl.Rows.Clear();
                gvModeOfRecDetl.Rows.Clear();
                gvActualRecDetails.Rows.Clear();
                gvDepartment.Rows.Clear();
                cbMisconduct.SelectedIndex = 2;

                cbFinYear.SelectedIndex = 0;
                dtpDoctMnth.Value = DateTime.Today;
                dtpVisitMnth.Value = DateTime.Today;
                gvDocumentDetl.Rows.Clear();
                gvMgntEmpDetl.Rows.Clear();

                rtbAuditPoint.Text = "";
                txtUnsolvedReason.Text = "";
                cbDeptAdd.SelectedIndex = 0;

                txtMgntRemarks.Text = "";

                gvEmployeeDetls.Rows.Clear();
                txtAuditByEcode.Text = "";
                txtAuditName.Text = "";

                cbPptPoint.SelectedIndex = 2;
                cbMisconduct.SelectedIndex = 0;
                dtExplDetl.Rows.Clear();
                gvExplDetails.Rows.Clear();

                txtMisCondId.Text = GenerateMisConId().ToString();
            }

        }

        private void btnAddDocDetails_Click(object sender, EventArgs e)
        {
            frmAddDocumentDetails DocDetails = new frmAddDocumentDetails("AUDIT_QUERIES");
            DocDetails.objMisconductForm = this;
            DocDetails.ShowDialog();
        }

        private void gvDocumentDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            byte[] Arr;
            if (e.RowIndex >=0)
            {
                if (e.ColumnIndex == gvDocumentDetl.Columns["ImgView"].Index)
                {
                    Arr = null;
                    Arr = (byte[])gvDocumentDetl.Rows[e.RowIndex].Cells["DocImage"].Value;
                    frmDisplayImage ImgView = new frmDisplayImage(Arr);
                    ImgView.objMisconductForm = this;
                    ImgView.ShowDialog();
                }

                if (e.ColumnIndex == gvDocumentDetl.Columns["Del_Image"].Index)
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

   
    
    }
}
