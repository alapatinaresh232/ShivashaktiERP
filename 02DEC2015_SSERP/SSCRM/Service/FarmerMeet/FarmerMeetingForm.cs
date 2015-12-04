using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using SSCRMDB;
using SSTrans;

namespace SSCRM
{
    public partial class FarmerMeetingForm : Form
    {
        SQLDB objSQLdb = null;
        ServiceDeptDB objServicedb = null;
        private Security objSecurity = null;
        private string strECode =string.Empty;
        public EmployeeDARWithTourBills objEmployeeDARWithTourBills;
       
        bool flagUpdate = false;
        public DataTable dtEmpDetails = new DataTable();

        private string strCompCode = "", strBranch = "", sEcode = "", sActDate = "", strTrnNo = "", strRefNo = "";
        

        public FarmerMeetingForm()
        {
            InitializeComponent();
        }
        public FarmerMeetingForm(string CompCode,string BranCode,string Ecode,string sDate)
        {
            InitializeComponent();
            strCompCode = CompCode;
            strBranch = BranCode;
            sEcode = Ecode;
            sActDate = sDate;
        }
        public FarmerMeetingForm(string CompCode, string BranCode, string Ecode, string sDate,string sTrnNo,string sRefNo)
        {
            InitializeComponent();
            strCompCode = CompCode;
            strBranch = BranCode;
            sEcode = Ecode;
            sActDate = sDate;
            strTrnNo = sTrnNo;
            strRefNo = sRefNo;
        }

        private void FarmerMeetingForm_Load(object sender, EventArgs e)
        {
            #region "AgriDeptEmpTable CREATE TABLE"
            dtEmpDetails.Columns.Add("SlNo2");
            dtEmpDetails.Columns.Add("AgriEmpName");
            dtEmpDetails.Columns.Add("AgriEmpDesig");
            dtEmpDetails.Columns.Add("AgriEmpDept");
            dtEmpDetails.Columns.Add("mobileNo1");
            #endregion

            gvDemoDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                       System.Drawing.FontStyle.Regular);
            gvAttendedEmpDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                       System.Drawing.FontStyle.Regular);
            gvAgricultureDeptEmpdetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                       System.Drawing.FontStyle.Regular);
            gvFarmerDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                       System.Drawing.FontStyle.Regular);
           
            txtDocMonth.Text = CommonData.DocMonth.ToUpper();
            dtpTrnDate.Value = DateTime.Today;
           
            FillCompanyData();
            FillBranchData();
            FillEmployeeData();
            if (CommonData.BranchType == "BR")
            {
                GenerateTransactionNo();
                FillCampComboBox(cbCompany.SelectedValue.ToString(), cbBranches.SelectedValue.ToString().Split('@')[0]);
            }
            if (strCompCode.Length != 0 && strBranch.Length != 0)
            {
                cbCompany.SelectedValue = strCompCode;
                cbBranches.SelectedValue = strBranch;
                GenerateTransactionNo();
                FillCampComboBox(cbCompany.SelectedValue.ToString(), cbBranches.SelectedValue.ToString().Split('@')[0]);
            }
           
            if (sActDate.Length != 0)
            {
                dtpTrnDate.Enabled = false;
                dtpTrnDate.Value = Convert.ToDateTime(sActDate);
            }
            else
            {
                dtpTrnDate.Enabled = true;
            }

            if (sEcode.Length != 0)
            {
                cbEcode.SelectedValue = sEcode;
                txtEcodeSearch.Text = sEcode;
                txtEcodeSearch_KeyUp(null, null);
                cbEcode.Enabled = false;
                txtEcodeSearch.ReadOnly = true;
                txtTrnNo.ReadOnly = true;
                cbCamps.Focus();
                if (strTrnNo.Length == 0)
                    txtTrnNo.CausesValidation = false;

            }
            else
            {
                cbEcode.Enabled = true;
                txtEcodeSearch.ReadOnly = false;
                txtTrnNo.CausesValidation = true;
                txtTrnNo.ReadOnly = false;
            }
            if (strTrnNo.Length > 0)
            {
                txtTrnNo.Text = strTrnNo;
                txtTrnNo_Validated(null, null);
                btnDelete.Enabled = false;
                btnCancel.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
                btnCancel.Enabled = true;
            }         

           
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
                    DataRow row = dt.NewRow();
                    row[0] = "--Select--";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row,0);

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

        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbBranches.DataSource = null;
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

                    cbBranches.DataSource = dt;
                    cbBranches.DisplayMember = "BRANCH_NAME";
                    cbBranches.ValueMember = "BranCode";
                }
                

                string BranCode = CommonData.BranchCode + '@' + CommonData.StateCode;
                cbBranches.SelectedValue = BranCode;
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

        private void FillCampComboBox(string strCompCode, string strBranCode)
        {
            objServicedb = new ServiceDeptDB();
            cbCamps.DataSource = null;
            if (strCompCode.Length > 0 && strBranCode.Length > 0)
            {
                try
                {
                    DataTable dt = objServicedb.LevelCampList_Get(strCompCode, strBranCode, "T").Tables[0];
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);

                    if (dt.Rows.Count > 1)
                    {
                        cbCamps.DataSource = dt;
                        cbCamps.DisplayMember = "CAMP_NAME";
                        cbCamps.ValueMember = "CAMP_CODE";
                    }
                    dt = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    objServicedb = null;
                }
            }
        }

        private void GenerateTransactionNo()
        {

            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                if (cbBranches.SelectedIndex > 0)
                {
                    string[] BranCode = cbBranches.SelectedValue.ToString().Split('@');
                    string finyear = CommonData.FinancialYear.Substring(2, 2) + CommonData.FinancialYear.Substring(7, 2);
                    string strNewNo = BranCode[0] + finyear + "FM-";

                    string strCommand = "SELECT ISNULL(MAX(SUBSTRING(ISNULL(SFMH_TRN_NUMBER, '" + strNewNo + "'),17,21)),0) + 1 " +
                                        " FROM SERVICES_FARMER_MEET_HEAD WHERE SFMH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "' AND SFMH_BRANCH_CODE='" + BranCode[0] + "'";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtTrnNo.Text = strNewNo + Convert.ToInt32(dt.Rows[0][0]).ToString("000000");
                    }
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
            try
            {
                string[] strBranCode = null;
                strBranCode = cbBranches.SelectedValue.ToString().Split('@');
                Cursor.Current = Cursors.WaitCursor;
                cbEcode.DataSource = null;
                cbEcode.Items.Clear();
                dsEmp = objServicedb.ServiceLevelEcodeSearch_Get(cbCompany.SelectedValue.ToString(),strBranCode[0],txtDocMonth.Text, txtEcodeSearch.Text.ToString());
                DataTable dtEmp = dsEmp.Tables[0];
                
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

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchData();
                cbBranches.SelectedIndex = 0;
            }
            else
            {
                cbBranches.DataSource = null;
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranches.SelectedIndex > 0)
            {
                GenerateTransactionNo();
                EcodeSearch();               
                if (cbEcode.Items.Count != 0)
                    cbEcode.SelectedIndex = 0;
                else
                    cbEcode.SelectedIndex = -1;
                FillCampComboBox(cbCompany.SelectedValue.ToString(), cbBranches.SelectedValue.ToString().Split('@')[0]);
                cbCamps.SelectedIndex = 0;
                txtEcodeSearch.Text = "";
                txtVillage.Text = "";
                txtMandal.Text = "";
                txtState.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtPin.Text = "";
                dtEmpDetails.Rows.Clear();
                gvAgricultureDeptEmpdetails.Rows.Clear();
                gvFarmerDetails.Rows.Clear();
                gvDemoDetails.Rows.Clear();
                gvAttendedEmpDetails.Rows.Clear();
            }
            else
            {
              
                cbEcode.SelectedIndex = -1;
                txtEcodeSearch.Text = "";
                txtVillage.Text = "";
                txtMandal.Text = "";
                txtState.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtPin.Text = "";
                dtEmpDetails.Rows.Clear();
                gvAgricultureDeptEmpdetails.Rows.Clear();
                gvFarmerDetails.Rows.Clear();
                gvDemoDetails.Rows.Clear();
                gvAttendedEmpDetails.Rows.Clear();
            }
        }

     
        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                if (txtEcodeSearch.Text.ToString().Trim().Length > 0)
                    EcodeSearch();
            }
            else
            {
                FillEmployeeData();
            }
            
        }
        

        private void btnAddProductDemoDetails_Click(object sender, EventArgs e)
        {
            frmDemoDetails demoDetails = new frmDemoDetails("FarmerMeetingForm");
            demoDetails.objFarmerMeetingForm = this;
            demoDetails.ShowDialog();

        }

        private void btnAddFarmerdetails_Click(object sender, EventArgs e)
        {
            AttendedFarmerDetails farmerDetails = new AttendedFarmerDetails("FarmerMeetingForm", txtVillage.Text, txtMandal.Text, txtDistrict.Text, txtState.Text, "1");
            farmerDetails.objFarmerMeetingForm = this;
            farmerDetails.ShowDialog();

        }

        private void btnAddAgriDeptEmpDetails_Click(object sender, EventArgs e)
        {
            frmAgriDeptEmpDetails AgriDeptEmpDetails = new frmAgriDeptEmpDetails();
            AgriDeptEmpDetails.objFarmerMeetingForm = this;
            AgriDeptEmpDetails.ShowDialog();
        }

        private void btnAddEmpdetails_Click(object sender, EventArgs e)
        {
            AttendedEmpDetails EmpDetails = new AttendedEmpDetails("FarmerMeetingForm",sEcode);
            EmpDetails.objFarmerMeetingForm = this;
            EmpDetails.ShowDialog();
        }

        private bool CheckData()
        {
            bool bFlag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Company","Farmers Meeting",MessageBoxButtons.OK,MessageBoxIcon.Error);
                cbCompany.Focus();
                return bFlag;
            }
            else if (cbBranches.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Branch", "Farmers Meeting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbBranches.Focus();
                return bFlag;
            }
            else if (cbCamps.SelectedIndex == 0)
            {

                bFlag = false;
                MessageBox.Show("Please Select Camp Name", "Farmers Meeting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbCamps.Focus();
                return bFlag;
            }
            else if (cbEcode.SelectedIndex == -1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Conducted Employee", "Farmers Meeting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbEcode.Focus();
                return bFlag;
            }
            else if (txtVillage.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Village Name", "Farmers Meeting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtVillage.Focus();
                return bFlag; 
            }
         
            else if (gvDemoDetails.Rows.Count == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Add Product Demo details", "Farmers Meeting", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnAddProductDemoDetails.Focus();
                return bFlag;

            }

            return bFlag;

        }

        #region "INSERT AND UPDATE DATA"

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            
            if (CheckData() == true)
            {
                if (SaveHeadDetails() > 0)
                {
                    if (SaveProductDemoDetails() > 0)
                    {
                        SaveFarmerMeetAttendedDetail();
                        SaveFarmerMeetingImageDetails();
                        MessageBox.Show("Data Saved Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flagUpdate = false;
                        btnCancel_Click(null,null);
                        txtTrnNo.ReadOnly = false;
                        GenerateTransactionNo();
                        if (sEcode.Length > 0)
                        {
                            this.Close();
                            this.Dispose();
                            objEmployeeDARWithTourBills.FillEmployeeActivityDetails(Convert.ToInt32(sEcode),sActDate);
                        }
                       
                    }
                    else
                    {
                        string strDelete = "DELETE FROM SERVICES_FARMER_MEET_HEAD WHERE SFMH_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                        int iRes = objSQLdb.ExecuteSaveData(strDelete);
                        MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {                    
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }
       
        private int SaveHeadDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCmd = "";
            try
            {
                 string[] strCode = cbBranches.SelectedValue.ToString().Split('@');

                strCmd = "DELETE FROM SERVICES_FARMER_MEET_ATTENDENTS WHERE SFMA_TRN_NUMBER='"+ txtTrnNo.Text.ToString() +"'";

                strCmd += "DELETE FROM SERVICES_FARMER_MEET_IMAGES WHERE SFMI_TRN_NUMBER= '" + txtTrnNo.Text.ToString() + "'";

                strCmd += "DELETE FROM SERVICES_FARMER_MEET_PRODUCTS WHERE SFMP_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
               

                if (flagUpdate == true)
                {
                    strCmd += "UPDATE SERVICES_FARMER_MEET_HEAD SET SFMH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                              "',SFMH_BRANCH_CODE='" + strCode[0] + "',SFMH_STATE_CODE='" + strCode[1] +
                              "',SFMH_DOC_MONTH='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                              "',SFMH_TRN_TYPE='FARMER MEET',SFMH_TRN_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                              "',SFMH_CAMP_NAME='" + cbCamps.Text.ToString() + "',SFMH_COND_BY_ECODE=" + Convert.ToInt32(cbEcode.SelectedValue) +
                              ",SFMH_VILLAGE='" + txtVillage.Text.ToString() + "',SFMH_MANDAL='" + txtMandal.Text.ToString() +
                              "',SFMH_DISTRICT='" + txtDistrict.Text.ToString() +"',SFMH_STATE='" + txtState.Text.ToString() +
                              "',SFMH_PIN='" + txtPin.Text +"',SFMH_AUTHORIZED_BY='',SFMH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                              "',SFMH_LAST_MODIFIED_DATE=getdate() " +
                              " WHERE SFMH_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                   
                }
                else if(flagUpdate==false)
                {
                    strCmd = "INSERT INTO SERVICES_FARMER_MEET_HEAD(SFMH_COMPANY_CODE " +
                                                                 ", SFMH_STATE_CODE " +
                                                                 ", SFMH_BRANCH_CODE " +
                                                                 ", SFMH_DOC_MONTH " +
                                                                 ", SFMH_TRN_TYPE " +
                                                                 ", SFMH_TRN_NUMBER " +
                                                                 ", SFMH_TRN_DATE " +
                                                                 ", SFMH_COND_BY_ECODE " +
                                                                 ", SFMH_CAMP_NAME " +
                                                                 ", SFMH_VILLAGE " +
                                                                 ", SFMH_MANDAL " +
                                                                 ", SFMH_DISTRICT " +
                                                                 ", SFMH_STATE " +
                                                                 ", SFMH_PIN " +
                                                                 ", SFMH_AUTHORIZED_BY " +
                                                                 ", SFMH_CREATED_BY " +
                                                                 ", SFMH_CREATED_DATE) " +
                                                                 " VALUES " +
                                                                 "('" + cbCompany.SelectedValue.ToString() +
                                                                  "','" + strCode[1] +
                                                                  "','" + strCode[0] +
                                                                  "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                  "','FARMER MEET','" + txtTrnNo.Text.ToString() +
                                                                  "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                                                  "'," + Convert.ToInt32(cbEcode.SelectedValue.ToString()) +
                                                                  ",'" + cbCamps.Text.ToString() +
                                                                  "','" + txtVillage.Text +
                                                                  "','" + txtMandal.Text +
                                                                  "','" + txtDistrict.Text +
                                                                  "','" + txtState.Text +
                                                                  "','" + txtPin.Text +
                                                                  "','','" + CommonData.LogUserId +
                                                                  "',getdate())";
                }

                if (strCmd.Length > 10)
                {
                    iRes = objSQLdb.ExecuteSaveData(strCmd);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
            return iRes;
        }
       

        private int SaveProductDemoDetails()
        {
            objSQLdb = new SQLDB();
            int iResult = 0;
            string strCommand = "";
            try
            {
                string[] strArr = cbBranches.SelectedValue.ToString().Split('@');

                //strCommand = "DELETE FROM SERVICES_FARMER_MEET_PRODUCTS WHERE SFMP_TRN_NUMBER='" + txtTrnNo.Text + "'";              

                for (int i = 0; i < gvDemoDetails.Rows.Count; i++)
                {
                    strCommand += "INSERT INTO SERVICES_FARMER_MEET_PRODUCTS(SFMP_COMPANY_CODE " +
                                                                         ", SFMP_STATE_CODE " +
                                                                         ", SFMP_BRANCH_CODE " +
                                                                         ", SFMP_DOC_MONTH " +
                                                                         ", SFMP_TRN_TYPE " +
                                                                         ", SFMP_TRN_NUMBER " +
                                                                         ", SFMP_PRODUCT_ID " +
                                                                         ", SFMP_DEMOS_COUNT " +
                                                                         ", SFMP_FARMERS_COUNT " +
                                                                         ", SFMP_REMARKS " +
                                                                         ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                         "','" + strArr[1] +
                                                                         "','" + strArr[0] +
                                                                         "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                         "','FARMER MEET','" + txtTrnNo.Text +
                                                                         "','" + gvDemoDetails.Rows[i].Cells["ProductID"].Value.ToString() +
                                                                         "'," + Convert.ToInt32(gvDemoDetails.Rows.Count) +
                                                                         "," + Convert.ToInt32(gvDemoDetails.Rows[i].Cells["Farmers"].Value) +
                                                                         ",'" + gvDemoDetails.Rows[i].Cells["Remarks"].Value.ToString() + "')";
                }
                if (strCommand.Length > 10)
                {
                    iResult = objSQLdb.ExecuteSaveData(strCommand);
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
            return iResult;
        }

        private int SaveFarmerMeetAttendedDetail()
        {
            objSQLdb = new SQLDB();
            int ires = 0;
            StringBuilder sb = new StringBuilder();
            string strCommand = "";
            try
            {
                string[] strData = cbBranches.SelectedValue.ToString().Split('@');

                if (gvFarmerDetails.Rows.Count > 0)
                {
                    for (int iVar = 0; iVar < gvFarmerDetails.Rows.Count; iVar++)
                    {
                        sb.Append("INSERT INTO SERVICES_FARMER_MEET_ATTENDENTS(SFMA_COMPANY_CODE " +
                                                                               ", SFMA_STATE_CODE " +
                                                                               ", SFMA_BRANCH_CODE " +
                                                                               ", SFMA_DOC_MONTH " +
                                                                               ", SFMA_TRN_NUMBER " +
                                                                               ", SFMA_TRN_TYPE " +
                                                                               ", SFMA_ATTND_TYPE " +
                                                                               ", SFMA_SL_NO " +
                                                                               ", SFMA_NAME " +
                                                                               ", SFMA_HOUSE_NO " +
                                                                               ", SFMA_LANDMARK " +
                                                                               ", SFMA_VILLAGE " +
                                                                               ", SFMA_MANDAL " +
                                                                               ", SFMA_DISTRICT " +
                                                                               ", SFMA_STATE " +
                                                                               ", SFMA_PIN " +
                                                                               ", SFMA_MOBILE_NUMBER " +
                                                                               ", SFMA_ACRES_COUNT " +
                                                                               ", SFMA_CROP_DTLS " +
                                                                               ", SFMA_REMARKS" +
                                                                               ", SFMA_RELA_TYPE "+
                                                                               ", SFMA_RELA_NAME "+
                                                                               ", SFMA_SALE_BOOKING "+
                                                                               ", SFMA_ADV_AMOUNT "+
                                                                               ", SFMA_ORDER_NO "+
                                                                               ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                               "','" + strData[1] +
                                                                               "','" + strData[0] +
                                                                               "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper()+
                                                                               "','" + txtTrnNo.Text +
                                                                               "','FARMER MEET','FARMERS'," + gvFarmerDetails.Rows[iVar].Cells["SlNo3"].Value.ToString() +
                                                                               ",'" + gvFarmerDetails.Rows[iVar].Cells["FarmerName"].Value.ToString() +
                                                                               "','" + gvFarmerDetails.Rows[iVar].Cells["HouseNo"].Value.ToString() +
                                                                               "','" + gvFarmerDetails.Rows[iVar].Cells["LandMark"].Value.ToString() +
                                                                               "','" + gvFarmerDetails.Rows[iVar].Cells["VillageName"].Value.ToString() +
                                                                               "','" + gvFarmerDetails.Rows[iVar].Cells["Mandal"].Value.ToString() +
                                                                               "','" + gvFarmerDetails.Rows[iVar].Cells["District"].Value.ToString() +
                                                                               "','" + gvFarmerDetails.Rows[iVar].Cells["State"].Value.ToString() +
                                                                               "','" + gvFarmerDetails.Rows[iVar].Cells["Pin"].Value.ToString() +
                                                                               "','" + gvFarmerDetails.Rows[iVar].Cells["MobileNo"].Value.ToString() +
                                                                               "','" + gvFarmerDetails.Rows[iVar].Cells["Acres"].Value.ToString() +
                                                                               "','" + gvFarmerDetails.Rows[iVar].Cells["CropName"].Value.ToString() +
                                                                               "','" + gvFarmerDetails.Rows[iVar].Cells["Remarks1"].Value.ToString() +
                                                                               "','" + gvFarmerDetails.Rows[iVar].Cells["ForhRel"].Value.ToString() +
                                                                               "','" + gvFarmerDetails.Rows[iVar].Cells["ForhRelName"].Value.ToString() + 
                                                                               "','"+ gvFarmerDetails.Rows[iVar].Cells["SpotSaleBooking"].Value.ToString() +
                                                                               "',"+ Convert.ToDouble(gvFarmerDetails.Rows[iVar].Cells["Amount"].Value) +
                                                                               ",'" + gvFarmerDetails.Rows[iVar].Cells["InvoiceNo"].Value.ToString() + "')");
                    }
                }
              

                if (gvAttendedEmpDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvAttendedEmpDetails.Rows.Count; i++)
                    {
                        sb.Append("INSERT INTO SERVICES_FARMER_MEET_ATTENDENTS(SFMA_COMPANY_CODE " +
                                                                               ", SFMA_STATE_CODE " +
                                                                               ", SFMA_BRANCH_CODE " +
                                                                               ", SFMA_DOC_MONTH " +
                                                                               ", SFMA_TRN_NUMBER " +
                                                                               ", SFMA_TRN_TYPE " +
                                                                               ", SFMA_ATTND_TYPE " +
                                                                               ", SFMA_SL_NO " +
                                                                               ", SFMA_ECODE " +
                                                                               ", SFMA_DESIG " +
                                                                               ", SFMA_DEPT " +
                                                                               ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                               "','" + strData[1] +
                                                                               "','" + strData[0] +
                                                                               "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                               "','" + txtTrnNo.Text +
                                                                               "','FARMER MEET','COMPANY_STAFF'," + Convert.ToInt32(gvAttendedEmpDetails.Rows[i].Cells["SlNo1"].Value) +
                                                                               "," + Convert.ToInt32(gvAttendedEmpDetails.Rows[i].Cells["Ecode"].Value) +
                                                                               ",'" + gvAttendedEmpDetails.Rows[i].Cells["Desig"].Value.ToString() +
                                                                               "','" + gvAttendedEmpDetails.Rows[i].Cells["Dept"].Value.ToString() + "')");
                    }

                }
                if (gvAgricultureDeptEmpdetails.Rows.Count > 0)
                {

                    for (int j = 0; j < gvAgricultureDeptEmpdetails.Rows.Count; j++)
                    {
                        sb.Append("INSERT INTO SERVICES_FARMER_MEET_ATTENDENTS(SFMA_COMPANY_CODE " +
                                                                               ", SFMA_STATE_CODE " +
                                                                               ", SFMA_BRANCH_CODE " +
                                                                               ", SFMA_DOC_MONTH " +
                                                                               ", SFMA_TRN_NUMBER " +
                                                                               ", SFMA_TRN_TYPE " +
                                                                               ", SFMA_ATTND_TYPE " +
                                                                               ", SFMA_SL_NO " +
                                                                               ", SFMA_NAME " +
                                                                               ", SFMA_DESIG " +
                                                                               ", SFMA_DEPT " +
                                                                               ", SFMA_MOBILE_NUMBER " +
                                                                               ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                               "','" + strData[1] +
                                                                               "','" + strData[0] +
                                                                               "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                               "','" + txtTrnNo.Text +
                                                                               "','FARMER MEET','AGRI_DEPT'," + Convert.ToInt32(gvAgricultureDeptEmpdetails.Rows[j].Cells["SlNo2"].Value) +
                                                                               ",'" + gvAgricultureDeptEmpdetails.Rows[j].Cells["AgriEmpName"].Value.ToString() +
                                                                               "','" + gvAgricultureDeptEmpdetails.Rows[j].Cells["AgriEmpDesig"].Value.ToString() +
                                                                               "','" + gvAgricultureDeptEmpdetails.Rows[j].Cells["AgriEmpDept"].Value.ToString() +
                                                                               "','" + gvAgricultureDeptEmpdetails.Rows[j].Cells["mobileNo1"].Value.ToString() + "')");
                    }
                }
                if (sb.Length != 0)
                {
                    strCommand += sb.ToString().Substring(0, sb.ToString().Length);
                }
                if (strCommand != "")
                {
                    ires = objSQLdb.ExecuteSaveData(strCommand);
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
            return ires;
        }

        private void SaveFarmerMeetingImageDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCmd = "";
            try
            {
                byte[] arr;
                ImageConverter converter = new ImageConverter();

                if (gvDocDetl.Rows.Count > 0)
                {
                    for (int i = 0; i < gvDocDetl.Rows.Count; i++)
                    {
                        arr = (byte[])gvDocDetl.Rows[i].Cells["DocImage"].Value;

                        strCmd = "INSERT INTO SERVICES_FARMER_MEET_IMAGES(SFMI_COMPANY_CODE " +
                                                                      ", SFMI_STATE_CODE " +
                                                                      ", SFMI_BRANCH_CODE " +
                                                                      ", SFMI_TRN_TYPE " +
                                                                      ", SFMI_TRN_NUMBER " +
                                                                      ", SFMI_SL_NO " +
                                                                      ", SFMI_DOC_NAME " +
                                                                      ", SFMI_DOC_DESC " +
                                                                      ", SFMI_IMAGE " +
                                                                      ", SFMI_CREATED_BY " +
                                                                      ", SFMI_CREATED_DATE " +
                                                                      ")VALUES " +
                                                                      "('" + cbCompany.SelectedValue.ToString() +
                                                                      "','" + cbBranches.SelectedValue.ToString().Split('@')[1] +
                                                                      "','" + cbBranches.SelectedValue.ToString().Split('@')[1] +
                                                                      "','FARMER MEET','" + txtTrnNo.Text.ToString() +
                                                                      "'," + Convert.ToInt32(gvDocDetl.Rows[i].Cells["SlNo_Image"].Value) +
                                                                      ",'" + gvDocDetl.Rows[i].Cells["DocumentName"].Value.ToString() +
                                                                      "','" + gvDocDetl.Rows[i].Cells["DocumentDesc"].Value.ToString() +
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
        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;       
                     
            cbCamps.SelectedIndex = 0;
            cbEcode.SelectedIndex = -1;
            txtEcodeSearch.Text = "";
            dtpTrnDate.Value = DateTime.Today;
            gvDocDetl.Rows.Clear();
            txtVillage.Text = "";
            txtMandal.Text = "";
            txtState.Text = "";
            txtDistrict.Text = "";
            txtState.Text = "";
            txtPin.Text = "";
            dtEmpDetails.Rows.Clear();
            gvDemoDetails.Rows.Clear();
            gvFarmerDetails.Rows.Clear();

            gvAttendedEmpDetails.Rows.Clear();
            gvAgricultureDeptEmpdetails.Rows.Clear();
            GenerateTransactionNo();
            dtpTrnDate.Focus();
        }

        #region "EDITING AND DELETING GRIDVIEW DETAILS"

        private void gvAttendedEmpDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvAttendedEmpDetails.Columns["Del_EmpDetails"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    DataGridViewRow dgvr = gvAttendedEmpDetails.Rows[e.RowIndex];
                    gvAttendedEmpDetails.Rows.Remove(dgvr);
                }
                if (gvAttendedEmpDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvAttendedEmpDetails.Rows.Count; i++)
                    {
                        gvAttendedEmpDetails.Rows[i].Cells["SlNo1"].Value = (i + 1).ToString();
                    }
                }
            }

        }

        private void gvDemoDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                if (e.ColumnIndex == gvDemoDetails.Columns["Edit_ProdDemoDetails"].Index)
                {
                    if (Convert.ToBoolean(gvDemoDetails.Rows[e.RowIndex].Cells["Edit_ProdDemoDetails"].Selected) == true)
                    {
                       
                        DataGridViewRow dgvr = gvDemoDetails.Rows[e.RowIndex];


                        frmDemoDetails DemoDetails = new frmDemoDetails("FarmerMeetingForm", dgvr);
                        DemoDetails.gvProductDetails.Rows.Clear();
                        DemoDetails.gvProductDetails.Rows.Add();

                        DemoDetails.gvProductDetails.Rows[0].Cells["SLNO"].Value = 1;
                        DemoDetails.gvProductDetails.Rows[0].Cells["ProductId"].Value = gvDemoDetails.Rows[e.RowIndex].Cells["ProductID"].Value.ToString();
                        DemoDetails.gvProductDetails.Rows[0].Cells["ProductName"].Value = gvDemoDetails.Rows[e.RowIndex].Cells["prodName"].Value.ToString();
                        DemoDetails.gvProductDetails.Rows[0].Cells["CategoryName"].Value = gvDemoDetails.Rows[e.RowIndex].Cells["Category"].Value.ToString();                       
                        DemoDetails.txtFarmersCnt.Text = gvDemoDetails.Rows[e.RowIndex].Cells["Farmers"].Value.ToString();
                        DemoDetails.txtRemarks.Text = gvDemoDetails.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();                                               
                    
                        DemoDetails.objFarmerMeetingForm = this;
                        DemoDetails.ShowDialog();
                    

                        if (gvDemoDetails.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvDemoDetails.Rows.Count; i++)
                            {
                                gvDemoDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                            }
                        }

                    }
                }


                if (e.ColumnIndex == gvDemoDetails.Columns["Del_ProdDemoDetails"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        DataGridViewRow dgvr = gvDemoDetails.Rows[e.RowIndex];
                        gvDemoDetails.Rows.Remove(dgvr);
                    }
                    if (gvDemoDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvDemoDetails.Rows.Count; i++)
                        {
                            gvDemoDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                        }
                    }
                }
            }
            
        }

       

        private void gvAgricultureDeptEmpdetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                if (e.ColumnIndex == gvAgricultureDeptEmpdetails.Columns["Edit_ADEmp"].Index)
                {
                    if (Convert.ToBoolean(gvAgricultureDeptEmpdetails.Rows[e.RowIndex].Cells["Edit_ADEmp"].Selected) == true)
                    {
                       
                        int SlNo = Convert.ToInt32(gvAgricultureDeptEmpdetails.Rows[e.RowIndex].Cells[gvAgricultureDeptEmpdetails.Columns["SlNo2"].Index].Value);
                        DataRow[] dr = dtEmpDetails.Select("SlNo2=" + SlNo);
                        
                        frmAgriDeptEmpDetails EmpDetails = new frmAgriDeptEmpDetails(dr);
                        EmpDetails.objFarmerMeetingForm = this;
                        EmpDetails.ShowDialog();                      
                                              
                    }


                }

                if (e.ColumnIndex == gvAgricultureDeptEmpdetails.Columns["Del_AgriEmpDetails"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvAgricultureDeptEmpdetails.Rows[e.RowIndex].Cells[gvAgricultureDeptEmpdetails.Columns["SlNo2"].Index].Value);
                        DataRow[] dr = dtEmpDetails.Select("SlNo2=" + SlNo);
                        dtEmpDetails.Rows.Remove(dr[0]);
                        GetAgriDeptEmpDetails();
                        MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                
            }

        }

        private void gvFarmerDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == gvFarmerDetails.Columns["Edit_Farmerdetails"].Index)
                {
                    if (Convert.ToBoolean(gvFarmerDetails.Rows[e.RowIndex].Cells["Edit_Farmerdetails"].Selected) == true)
                    {


                        DataGridViewRow dgvr = gvFarmerDetails.Rows[e.RowIndex];
                        AttendedFarmerDetails FarmerDetails = new AttendedFarmerDetails("FarmerMeetingForm", dgvr);

                       
                        FarmerDetails.txtFarmerName.Text = gvFarmerDetails.Rows[e.RowIndex].Cells["FarmerName"].Value.ToString();
                        FarmerDetails.cbRelation.Text = gvFarmerDetails.Rows[e.RowIndex].Cells["ForhRel"].Value.ToString();
                        FarmerDetails.txtRelationName.Text = gvFarmerDetails.Rows[e.RowIndex].Cells["ForhRelName"].Value.ToString();
                        FarmerDetails.txtHouseNo.Text = gvFarmerDetails.Rows[e.RowIndex].Cells["HouseNo"].Value.ToString();
                        FarmerDetails.txtLandMark.Text = gvFarmerDetails.Rows[e.RowIndex].Cells["LandMark"].Value.ToString();
                        FarmerDetails.txtVillage.Text = gvFarmerDetails.Rows[e.RowIndex].Cells["VillageName"].Value.ToString();
                        FarmerDetails.txtPin.Text = gvFarmerDetails.Rows[e.RowIndex].Cells["Pin"].Value.ToString();
                        FarmerDetails.txtMandal.Text = gvFarmerDetails.Rows[e.RowIndex].Cells["Mandal"].Value.ToString();
                        FarmerDetails.txtDistrict.Text = gvFarmerDetails.Rows[e.RowIndex].Cells["District"].Value.ToString();
                        FarmerDetails.txtState.Text = gvFarmerDetails.Rows[e.RowIndex].Cells["State"].Value.ToString();
                        FarmerDetails.txtMobileNo.Text = gvFarmerDetails.Rows[e.RowIndex].Cells["MobileNo"].Value.ToString();
                        FarmerDetails.txtAcrescnt.Text = gvFarmerDetails.Rows[e.RowIndex].Cells["Acres"].Value.ToString();
                        FarmerDetails.txtCropName.Text = gvFarmerDetails.Rows[e.RowIndex].Cells["CropName"].Value.ToString();
                        FarmerDetails.txtRemarks.Text = gvFarmerDetails.Rows[e.RowIndex].Cells["Remarks1"].Value.ToString();
                        FarmerDetails.cbSpotSaleBooking.Text = gvFarmerDetails.Rows[e.RowIndex].Cells["SpotSaleBooking"].Value.ToString();
                        FarmerDetails.txtAmount.Text = gvFarmerDetails.Rows[e.RowIndex].Cells["Amount"].Value.ToString();
                        FarmerDetails.txtInvNo.Text = gvFarmerDetails.Rows[e.RowIndex].Cells["InvoiceNo"].Value.ToString();
                        
                        FarmerDetails.objFarmerMeetingForm = this;
                        FarmerDetails.ShowDialog();

                        if (gvFarmerDetails.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvFarmerDetails.Rows.Count; i++)
                            {
                                gvFarmerDetails.Rows[i].Cells["SlNo3"].Value = (i + 1).ToString();
                            }
                        }

                    }

                }

                if (e.ColumnIndex == gvFarmerDetails.Columns["Del_FarmerDetails"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        DataGridViewRow dgvr = gvFarmerDetails.Rows[e.RowIndex];
                        gvFarmerDetails.Rows.Remove(dgvr);
                    }
                    if (gvFarmerDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvFarmerDetails.Rows.Count; i++)
                        {
                            gvFarmerDetails.Rows[i].Cells["SlNo3"].Value = (i + 1).ToString();
                        }
                    }
                }
            }
        }

        #endregion

     

        private void btnVillageSearch_Click(object sender, EventArgs e)
        {
            VillageSearch vilsearch = new VillageSearch("FarmerMeetingForm");
            vilsearch.objFarmerMeetingForm = this;
            vilsearch.ShowDialog();

        }

      #region "GET DATA FOR UPDATE"
        private void FillFarmerMeetingDetails()
        {
            objServicedb = new ServiceDeptDB();
            Hashtable ht;

            DataTable dtFMHead;
            
            if (txtTrnNo.Text.Length > 0)
            {
                try
                {
                    ht = objServicedb.GetServiceFarmerMeetingDetails(txtTrnNo.Text.ToString());
                    dtFMHead = (DataTable)ht["FarmerMeetingHead"];
                   
                    FillHeadDetails(dtFMHead);
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
        }
        private void FillHeadDetails(DataTable dtHead)
        {
            objServicedb = new ServiceDeptDB();
            Hashtable ht;
            DataTable dtFMProdDemoDetails;
            DataTable dtEmpDetails;
            DataTable dtAgriDeptEmpDetails;
            DataTable dtFarmerDetails;
            DataTable dtImageDetl;

            if (txtTrnNo.Text.Length > 0)
            {
                try
                {

                    ht = objServicedb.GetServiceFarmerMeetingDetails(txtTrnNo.Text.ToString());

                    dtFMProdDemoDetails = (DataTable)ht["FarmerMeetingProductDetails"];
                    dtEmpDetails = (DataTable)ht["AttendentEmpDetails"];
                    dtAgriDeptEmpDetails = (DataTable)ht["AgriDeptEmpDetails"];
                    dtFarmerDetails = (DataTable)ht["FarmerDetails"];
                    dtImageDetl = (DataTable)ht["ImageDetails"];


                    if (dtHead.Rows.Count > 0)
                    {
                        flagUpdate = true;

                        if ((DateTime.Now - Convert.ToDateTime(Convert.ToDateTime(dtHead.Rows[0]["CreatedDate"]).ToString("dd/MM/yyyy"))).TotalDays > 20 )
                        {
                            if (CommonData.LogUserId.ToUpper() == "ADMIN" || CommonData.LogUserRole=="MANAGEMENT")
                            {
                                btnSave.Enabled = true;
                                btnDelete.Enabled = true;
                                btnCancel.Enabled = true;
                            }
                            else
                            {
                                btnSave.Enabled = false;
                                btnDelete.Enabled = false;
                                btnCancel.Enabled = false;
                            }
                        }
                      
                        string stECode = dtHead.Rows[0]["Ecode"] + "";
                        strCompCode = dtHead.Rows[0]["CompCode"].ToString();
                        cbCompany.SelectedValue = strCompCode;
                        strBranch = dtHead.Rows[0]["BRANCH_NAME"].ToString();
                        cbBranches.Text = strBranch;                       
                        dtpTrnDate.Value = Convert.ToDateTime(dtHead.Rows[0]["TrnDate"].ToString());
                        cbEcode.SelectedValue = stECode;
                        txtEcodeSearch.Text = stECode;
                        txtDocMonth.Text = dtHead.Rows[0]["DocMon"].ToString();
                        cbCamps.Text = dtHead.Rows[0]["CampName"].ToString();
                        txtVillage.Text = dtHead.Rows[0]["Village"].ToString();
                        txtMandal.Text = dtHead.Rows[0]["Mandal"].ToString();
                        txtDistrict.Text = dtHead.Rows[0]["District"].ToString();
                        txtState.Text = dtHead.Rows[0]["State"].ToString();
                        txtPin.Text = dtHead.Rows[0]["Pin"].ToString();

                        FillProductDemoDetails(dtFMProdDemoDetails);
                        FillFarmerDetails(dtFarmerDetails);
                        FillAttendedEmpDetails(dtEmpDetails);
                        FillAgriDeptEmpDetails(dtAgriDeptEmpDetails);
                        FillImageDetails(dtImageDetl);
                    }
                    else
                    {
                        flagUpdate = false;
                        GenerateTransactionNo();
                        cbEcode.SelectedIndex = -1;
                        txtEcodeSearch.Text = "";
                        dtpTrnDate.Value = DateTime.Today;
                        txtDocMonth.Text = Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper();
                        cbCamps.SelectedIndex = 0;
                        txtVillage.Text = "";
                        txtMandal.Text = "";
                        txtState.Text = "";
                        txtDistrict.Text = "";
                        txtState.Text = "";
                        txtPin.Text = "";                     
                        dtEmpDetails.Rows.Clear();
                        gvDemoDetails.Rows.Clear();
                        gvFarmerDetails.Rows.Clear();
                        
                        gvAttendedEmpDetails.Rows.Clear();
                        gvAgricultureDeptEmpdetails.Rows.Clear();
                        gvDocDetl.Rows.Clear();
                        btnSave.Enabled = true;
                        btnDelete.Enabled = true;
                        btnCancel.Enabled = true;
                    }
                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    flagUpdate = false;
                }

            }           
            
        }

        private void FillProductDemoDetails(DataTable dtProdDetails)
        {            
            gvDemoDetails.Rows.Clear();
            if (txtTrnNo.Text.Length > 0)
            {
                try
                {
                                        
                    if (dtProdDetails.Rows.Count > 0)
                    {                       
                        for (int i = 0; i < dtProdDetails.Rows.Count; i++)
                        {
                            gvDemoDetails.Rows.Add();

                            gvDemoDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                            gvDemoDetails.Rows[i].Cells["ProductID"].Value = dtProdDetails.Rows[i]["ProductId"].ToString();
                            gvDemoDetails.Rows[i].Cells["prodName"].Value = dtProdDetails.Rows[i]["ProductName"].ToString();
                            gvDemoDetails.Rows[i].Cells["Category"].Value = dtProdDetails.Rows[i]["CategoryName"].ToString();
                            gvDemoDetails.Rows[i].Cells["Farmers"].Value = dtProdDetails.Rows[i]["FarmersCount"].ToString();
                            gvDemoDetails.Rows[i].Cells["Demos"].Value = dtProdDetails.Rows[i]["DemosCount"].ToString();
                            gvDemoDetails.Rows[i].Cells["Remarks"].Value = dtProdDetails.Rows[i]["Remarks"].ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
            }
            else
            {
                gvDemoDetails.Rows.Clear();
            }

        }

        private void FillImageDetails(DataTable dtImages)
        {
            gvDocDetl.Rows.Clear();
            try
            {
                if (dtImages.Rows.Count > 0)
                {
                    for (int i = 0; i < dtImages.Rows.Count; i++)
                    {
                        gvDocDetl.Rows.Add();

                        gvDocDetl.Rows[i].Cells["SlNo_Image"].Value = (i + 1).ToString();
                        gvDocDetl.Rows[i].Cells["DocumentName"].Value = dtImages.Rows[i]["DocName"].ToString();
                        gvDocDetl.Rows[i].Cells["DocumentDesc"].Value = dtImages.Rows[i]["DocumentDesc"].ToString();
                        gvDocDetl.Rows[i].Cells["DocImage"].Value = dtImages.Rows[i]["Image"];
                    }
                }
                else
                {
                    gvDocDetl.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FillAttendedEmpDetails(DataTable dtEmp)
        {           
            gvAttendedEmpDetails.Rows.Clear();
            if (txtTrnNo.Text.Length > 0)
            {
                try
                {
                   
                    if (dtEmp.Rows.Count > 0)
                    {                  

                        for (int iVar = 0; iVar < dtEmp.Rows.Count; iVar++)
                        {
                            gvAttendedEmpDetails.Rows.Add();

                            gvAttendedEmpDetails.Rows[iVar].Cells["SlNo1"].Value = (iVar + 1).ToString();
                            gvAttendedEmpDetails.Rows[iVar].Cells["Ecode"].Value = dtEmp.Rows[iVar]["Ecode"].ToString();
                            gvAttendedEmpDetails.Rows[iVar].Cells["EmpName"].Value = dtEmp.Rows[iVar]["EmpName"].ToString();
                            gvAttendedEmpDetails.Rows[iVar].Cells["Desig"].Value = dtEmp.Rows[iVar]["Desig"].ToString();
                            gvAttendedEmpDetails.Rows[iVar].Cells["Dept"].Value = dtEmp.Rows[iVar]["DeptName"].ToString();
                        }
                    }
                    
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
            }
            else
            {               
                gvAttendedEmpDetails.Rows.Clear();              

            }

        }
        private void FillAgriDeptEmpDetails(DataTable dt)
        {
            dtEmpDetails.Rows.Clear();
            if (txtTrnNo.Text.Length > 0)
            {
                try
                {
                    
                    if (dt.Rows.Count > 0)
                    {                      

                        for (int iVar = 0; iVar < dt.Rows.Count; iVar++)
                        {
                            dtEmpDetails.Rows.Add(new Object[] {"-1", dt.Rows[iVar]["Name"].ToString(),
                                                                       dt.Rows[iVar]["Desig"].ToString(),
                                                                       dt.Rows[iVar]["DeptName"].ToString(),
                                                                        dt.Rows[iVar]["MobileNo"].ToString()});

                            GetAgriDeptEmpDetails();
                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
            }
           
        }

        private void FillFarmerDetails(DataTable dtFarmerData)
        {
            gvFarmerDetails.Rows.Clear();

            if (txtTrnNo.Text.Length > 0)
            {
                try
                {

                    if (dtFarmerData.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtFarmerData.Rows.Count; j++)
                        {
                            gvFarmerDetails.Rows.Add();

                            gvFarmerDetails.Rows[j].Cells["SlNo3"].Value = (j + 1).ToString();
                            gvFarmerDetails.Rows[j].Cells["FarmerName"].Value = dtFarmerData.Rows[j]["FarmerName"].ToString();
                            gvFarmerDetails.Rows[j].Cells["ForhRel"].Value = dtFarmerData.Rows[j]["RelType"].ToString();
                            gvFarmerDetails.Rows[j].Cells["ForhRelName"].Value = dtFarmerData.Rows[j]["RelName"].ToString();
                            gvFarmerDetails.Rows[j].Cells["HouseNo"].Value = dtFarmerData.Rows[j]["HouseNo"].ToString();
                            gvFarmerDetails.Rows[j].Cells["LandMark"].Value = dtFarmerData.Rows[j]["LandMark"].ToString();
                            gvFarmerDetails.Rows[j].Cells["VillageName"].Value = dtFarmerData.Rows[j]["Village"].ToString();
                            gvFarmerDetails.Rows[j].Cells["Mandal"].Value = dtFarmerData.Rows[j]["Mandal"].ToString();
                            gvFarmerDetails.Rows[j].Cells["District"].Value = dtFarmerData.Rows[j]["District"].ToString();
                            gvFarmerDetails.Rows[j].Cells["State"].Value = dtFarmerData.Rows[j]["State"].ToString();
                            gvFarmerDetails.Rows[j].Cells["Pin"].Value = dtFarmerData.Rows[j]["Pin"].ToString();
                            gvFarmerDetails.Rows[j].Cells["MobileNo"].Value = dtFarmerData.Rows[j]["MobileNo"].ToString();
                            gvFarmerDetails.Rows[j].Cells["Acres"].Value = dtFarmerData.Rows[j]["AcresCnt"].ToString();
                            gvFarmerDetails.Rows[j].Cells["CropName"].Value = dtFarmerData.Rows[j]["CropName"].ToString();
                            gvFarmerDetails.Rows[j].Cells["Remarks1"].Value = dtFarmerData.Rows[j]["Remarks"].ToString();
                            gvFarmerDetails.Rows[j].Cells["SpotSaleBooking"].Value = dtFarmerData.Rows[j]["SaleBooking"].ToString();
                            gvFarmerDetails.Rows[j].Cells["InvoiceNo"].Value = dtFarmerData.Rows[j]["OrderNo"].ToString();
                            gvFarmerDetails.Rows[j].Cells["Amount"].Value = dtFarmerData.Rows[j]["AdvAmount"].ToString();                          
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            else
            {
                gvFarmerDetails.Rows.Clear();
            }
            
        }
          
      #endregion


        private bool CheckDeleteCondition()
        {
            bool flag = true;
            if (txtTrnNo.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Transaction Number","Farmers Meeting Form",MessageBoxButtons.OK,MessageBoxIcon.Information);
                txtTrnNo.Focus();
                return flag;
            }
         return flag;   
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int iRes = 0;
            objSQLdb = new SQLDB();
            string strCmd = "";

            if (CheckDeleteCondition() == true && flagUpdate==true)
            {
                DialogResult result = MessageBox.Show("Do you want to delete This Record ?",
                                        "Farmers Meeting", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                       

                        strCmd = "DELETE FROM SERVICES_FARMER_MEET_PRODUCTS WHERE SFMP_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                      
                        strCmd += " DELETE FROM SERVICES_FARMER_MEET_ATTENDENTS WHERE SFMA_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                        strCmd += " DELETE FROM SERVICES_FARMER_MEET_IMAGES WHERE SFMI_TRN_NUMBER= '" + txtTrnNo.Text.ToString() + "'";
                        strCmd += " DELETE FROM SERVICES_FARMER_MEET_HEAD WHERE SFMH_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";

                        if (strCmd.Length > 10)
                        {
                            iRes = objSQLdb.ExecuteSaveData(strCmd);
                        }
                        if (iRes > 0)
                        {
                            MessageBox.Show("Data Deleted Sucessfully", "Farmers Meeting", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            flagUpdate = false;
                            btnCancel_Click(null, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                else
                {
                    MessageBox.Show("Data Not Deleted", "Farmers Meeting", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }



        }

        #region "GRIDVIEW DETAILS"
        public void GetAgriDeptEmpDetails()
        {
            int intRow = 1;
            gvAgricultureDeptEmpdetails.Rows.Clear();
            try
            {
                for (int i = 0; i < dtEmpDetails.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    dtEmpDetails.Rows[i]["SlNo2"] = intRow;
                    tempRow.Cells.Add(cellSLNO);


                    DataGridViewCell cellEmpName = new DataGridViewTextBoxCell();
                    cellEmpName.Value = dtEmpDetails.Rows[i]["AgriEmpName"];
                    tempRow.Cells.Add(cellEmpName);

                    DataGridViewCell cellEmpDesig = new DataGridViewTextBoxCell();
                    cellEmpDesig.Value = dtEmpDetails.Rows[i]["AgriEmpDesig"];
                    tempRow.Cells.Add(cellEmpDesig);

                    DataGridViewCell cellEmpDept = new DataGridViewTextBoxCell();
                    cellEmpDept.Value = dtEmpDetails.Rows[i]["AgriEmpDept"];
                    tempRow.Cells.Add(cellEmpDept);

                    DataGridViewCell cellempMobileNo = new DataGridViewTextBoxCell();
                    cellempMobileNo.Value = dtEmpDetails.Rows[i]["mobileNo1"];
                    tempRow.Cells.Add(cellempMobileNo);

                    intRow = intRow + 1;
                    gvAgricultureDeptEmpdetails.Rows.Add(tempRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        #endregion

        private void FillAddressData(string svilsearch)
        {           
            objServicedb = new ServiceDeptDB();
            string strDist = string.Empty;           
            DataTable dtVillage = new DataTable();
            if (txtVillage.Text != "")
            {
                try
                {
                    dtVillage = objServicedb.ServiceVillageSearch_Get(svilsearch).Tables[0];

                    if (dtVillage.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtVillage.Rows.Count; i++)
                        {
                            if (txtVillage.Text.Equals(dtVillage.Rows[i]["PANCHAYAT"].ToString()))
                            {
                                txtMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();
                                txtDistrict.Text = dtVillage.Rows[0]["District"].ToString();
                                txtState.Text = dtVillage.Rows[0]["State"].ToString();
                                txtPin.Text = dtVillage.Rows[0]["PIN"].ToString();
                            }
                        }
                    }

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    objServicedb = null;
                    dtVillage = null;
                 

                }
            }
            else
            {               
                txtMandal.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtPin.Text = "";
            }

        }

        private void txtVillage_TextChanged(object sender, EventArgs e)
        {
            
            //FillAddressData(txtVillage.Text.ToString());

        }

       

        private void txtVillage_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
      

        private void txtTrnNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (txtTrnNo.SelectionStart < 17)
            //    e.Handled = true;
            //if (e.KeyChar != '\b' )
            //{
            //    if (!char.IsDigit(e.KeyChar))
            //    {
            //        e.Handled = true;
            //    }
            //}
        }

        private void txtTrnNo_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyValue == 37 || e.KeyValue == 39 || e.KeyValue == 35)
            //    e.Handled = false;
            //else if (txtTrnNo.SelectionStart < 17)
            //    e.Handled = true;
            //else if (e.KeyValue == 46)
            //    e.Handled = true;

        }

        private void txtTrnNo_Enter(object sender, EventArgs e)
        {
            //this.txtTrnNo.Select(txtTrnNo.Text.Length, 0);
        }

        private void txtTrnNo_Validated(object sender, EventArgs e)
        {
            if (txtTrnNo.Text.Length > 21)
            {                
                FillFarmerMeetingDetails();
            }
            else
            {
                flagUpdate = false;
                cbCamps.SelectedIndex = 0;
                cbEcode.SelectedIndex = -1;
                txtEcodeSearch.Text = "";
                txtVillage.Text = "";
                txtMandal.Text = "";
                txtState.Text = "";
                txtDistrict.Text = "";
                txtPin.Text = "";
                dtEmpDetails.Rows.Clear();
                gvAgricultureDeptEmpdetails.Rows.Clear();
                gvFarmerDetails.Rows.Clear();
                gvDemoDetails.Rows.Clear();
                gvDocDetl.Rows.Clear();
                gvAttendedEmpDetails.Rows.Clear();
                GenerateTransactionNo();
                btnSave.Enabled = true;
                btnDelete.Enabled = true;
                btnCancel.Enabled = true;

            }
        }

        private void dtpTrnDate_ValueChanged(object sender, EventArgs e)
        {
            txtDocMonth.Text = Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper();
        }

        private void cbCamps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCamps.SelectedIndex > 0)
            {
                txtVillage.Text = cbCamps.SelectedValue.ToString().Split('^')[1];
                txtMandal.Text = cbCamps.SelectedValue.ToString().Split('^')[2];
                txtDistrict.Text = cbCamps.SelectedValue.ToString().Split('^')[3];
                txtState.Text = cbCamps.SelectedValue.ToString().Split('^')[4];
            }
            else
            {
                txtVillage.Text = "";
                txtMandal.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
            }
        }

        private void btnAddDocDetails_Click(object sender, EventArgs e)
        {
            frmAddDocumentDetails DocDetl = new frmAddDocumentDetails("FARMER_MEET");
            DocDetl.objFarmerMeetingForm = this;
            DocDetl.ShowDialog();
        }

        private void gvDocDetl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                byte[] Arr;
                if (e.ColumnIndex == gvDocDetl.Columns["ImgView"].Index)
                {
                    Arr = null;
                    Arr = (byte[])gvDocDetl.Rows[e.RowIndex].Cells["DocImage"].Value;
                    frmDisplayImage ImgView = new frmDisplayImage(Arr);
                    ImgView.objFarmerMeetingForm = this;
                    ImgView.Show();

                }

                if (e.ColumnIndex == gvDocDetl.Columns["Del"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        DataGridViewRow dgvr = gvDocDetl.Rows[e.RowIndex];
                        gvDocDetl.Rows.Remove(dgvr);
                    }
                    if (gvDocDetl.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvDocDetl.Rows.Count; i++)
                        {
                            gvDocDetl.Rows[i].Cells["SlNo_Image"].Value = (i + 1).ToString();
                        }
                    }
                }
            }
        }

      
     
    }
}
