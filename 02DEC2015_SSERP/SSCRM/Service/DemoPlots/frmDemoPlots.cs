using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using SSCRMDB;
using SSTrans;
using SSAdmin;

namespace SSCRM
{
    public partial class frmDemoPlots : Form
    {
        SQLDB objSQLdb = null;
        ServiceDeptDB objServicedb = null;
        private Security objSecurity = null;
        private bool flagUpdate = false;
        string strECode = string.Empty;
        private string sCompCode = "", sBranCode = "", sEcode = "", sActivityDate = "", strTrnNo = "", strRefNo = "";
        public EmployeeDARWithTourBills objEmployeeDARWithTourBills;
        
        public DataTable dtDemoPlotResults = new DataTable();
        public DataTable dtProductDetails = new DataTable();


        public frmDemoPlots()
        {
            InitializeComponent();
        }
        public frmDemoPlots(string Company, string BranCode, string stEcode, string strDate)
        {
            InitializeComponent();
            sCompCode = Company;
            sBranCode = BranCode;
            sEcode = stEcode;
            sActivityDate = strDate;
        }
        public frmDemoPlots(string Company, string BranCode, string stEcode, string strDate, string sTrnNo, string sRefNo)
        {
            InitializeComponent();
            sCompCode = Company;
            sBranCode = BranCode;
            sEcode = stEcode;
            sActivityDate = strDate;
            strTrnNo = sTrnNo;
            strRefNo = sRefNo;
        }

        private void frmDemoPlots_Load(object sender, EventArgs e)
        {
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            
            gvDemoPlotResults.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                      System.Drawing.FontStyle.Regular);

            #region "CREATE DEMOPLOT_RESULTS TABLE"
            dtDemoPlotResults.Columns.Add("SlNo_Results");
            dtDemoPlotResults.Columns.Add("AoEcode");
            dtDemoPlotResults.Columns.Add("ObservDate");
            dtDemoPlotResults.Columns.Add("AoName");
            dtDemoPlotResults.Columns.Add("FarmerOpinion");
            dtDemoPlotResults.Columns.Add("NotifyResult");
            dtDemoPlotResults.Columns.Add("ResultRemarks");
            dtDemoPlotResults.Columns.Add("CropImage");
            dtDemoPlotResults.Columns.Add("TreatedImage");
            #endregion

            #region "CREATE PRODUCT_DETAILS TABLE"
            dtProductDetails.Columns.Add("SLNO_Product");
            dtProductDetails.Columns.Add("ProductID");
            dtProductDetails.Columns.Add("CropId");
            dtProductDetails.Columns.Add("CropName");
            dtProductDetails.Columns.Add("CropArea");
            dtProductDetails.Columns.Add("CropStage");
            dtProductDetails.Columns.Add("TreatedArea");
            dtProductDetails.Columns.Add("prodName");
            dtProductDetails.Columns.Add("Qty");
            dtProductDetails.Columns.Add("Category");
            dtProductDetails.Columns.Add("Remarks");
            #endregion

            cbFarmerMeet.Visible = false;
            lblFarmerMeet.Visible = false;
            lblReason.Visible = false;
            txtReason.Visible = false;
            cbDPStatus.SelectedIndex = 0;
            cbDemoPlot.SelectedIndex = 0;
            cbFarmerMeet.SelectedIndex = 0;
           
            dtpTrnDate.Value = DateTime.Today;

            FillCompanyData();
            FillBranchData();

            if (CommonData.BranchType == "BR")
            {
                GenerateTransactionNo();
                FillCampComboBox(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString().Split('@')[0]);
            }
           
            FillEmployeeData();
            EcodeSearch();

            if (sCompCode.Length > 0 && sBranCode.Length > 0 && sActivityDate.Length > 0)
            {                
                cbCompany.SelectedValue = sCompCode;
                cbBranch.SelectedValue = sBranCode;
                
                dtpTrnDate.Value = Convert.ToDateTime(sActivityDate);
                dtpTrnDate.Enabled = false;
               
                GenerateTransactionNo();
                
            }
            FillEmployeeData();
            EcodeSearch();

            if (sEcode.Length != 0)
            {
                cbEcode.Enabled = false;                
                txtEcodeSearch.Text = sEcode;
                cbEcode.SelectedValue = sEcode;
                txtEcodeSearch.ReadOnly = true;
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
           
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {

                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE+'@'+ STATE_CODE AS BranCode " +
                                         " FROM USER_BRANCH " +
                                         " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                         " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                         "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                         "' AND BRANCH_TYPE IN ('BR','HO') ORDER BY BRANCH_NAME ASC";
                  
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);

                    cbBranch.DataSource = dt;
                    cbBranch.DisplayMember = "BRANCH_NAME";
                    cbBranch.ValueMember = "BranCode";
                }

                string BranCode = CommonData.BranchCode + '@' + CommonData.StateCode;
                cbBranch.SelectedValue = BranCode;
               
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
                    MessageBox.Show(ex.Message, "Product Promotion", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (cbBranch.SelectedIndex > 0)
            {
                try
                {

                    string[] BranCode = cbBranch.SelectedValue.ToString().Split('@');
                    string finyear = CommonData.FinancialYear.Substring(2, 2) + CommonData.FinancialYear.Substring(7, 2);
                    string strNewNo = BranCode[0] + finyear + "DP-";

                    string strCommand = "SELECT ISNULL(MAX(SUBSTRING(ISNULL(SDPH_TRN_NUMBER, '" + strNewNo + "'),17,21)),0) + 1 " +
                                        " FROM SERVICES_DEMO_PLOTS_HEAD " +
                                        " WHERE SDPH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "' AND SDPH_BRANCH_CODE='" + BranCode[0] + "' ";
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
                FillCampComboBox(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString().Split('@')[0]);
                EcodeSearch();
                FillEmployeeData();
                txtEcodeSearch.Text = "";
                txtVillage.Text = "";
                txtMandal.Text = "";
                txtState.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtPin.Text = "";
                txtCustomerName.Text = "";
                txtHouseNo.Text = "";
                txtLandMark.Text = "";
                txtMobileNo.Text = "";

                dtDemoPlotResults.Rows.Clear();
                dtProductDetails.Rows.Clear();
                gvProductDetails.Rows.Clear();
                gvDemoPlotResults.Rows.Clear();     
            }
            else
            {
               
                flagUpdate = false;
                GenerateTransactionNo();                
                //cbEcode.SelectedIndex = -1;
                //txtEcodeSearch.Text = "";
                txtVillage.Text = "";
                txtMandal.Text = "";
                txtState.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtPin.Text = "";
                txtCustomerName.Text = "";
                txtHouseNo.Text = "";
                txtLandMark.Text = "";
                txtMobileNo.Text = "";

                dtDemoPlotResults.Rows.Clear();
                dtProductDetails.Rows.Clear();
                gvProductDetails.Rows.Clear();
                gvDemoPlotResults.Rows.Clear();             
            }
        }

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
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

        

        private bool CheckData()
        {
            bool bFlag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                bFlag = false;             
                MessageBox.Show("Please Select Company", "Demo Plots", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbCompany.Focus();
                return bFlag;              
            }
            if (cbBranch.SelectedIndex == 0)
            {
                bFlag = false;            
                MessageBox.Show("Please Select Branch", "Demo Plots", MessageBoxButtons.OK, MessageBoxIcon.Error);              
                cbBranch.Focus();
                return bFlag;
            }
            if (cbCamps.SelectedIndex == 0)
            {
                bFlag = false;         
                MessageBox.Show("Please Select Camp Name", "Demo Plots", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbCamps.Focus();
                return bFlag;
            }
            if (cbEcode.SelectedIndex == -1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Conducted Employee", "Demo Plots", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbEcode.Focus();
                return bFlag;
            }

            if (txtCustomerName.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Farmer Name", "Demo Plots", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCustomerName.Focus();
                return bFlag;
            }

            if (txtVillage.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Village Name", "Demo Plots", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnVillageSearch.Focus();
                return bFlag;
            }       
            if (gvProductDetails.Rows.Count == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Add Product Details", "Demo Plots", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnAddProductDetails.Focus();
                return bFlag;
            }
            if (cbDPStatus.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Demo Plot Working Status", "Demo Plots", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbDPStatus.Focus();
                return bFlag;
            }
            if (cbDemoPlot.Text == "SUCCESS")
            {
                if (cbFarmerMeet.SelectedIndex == 0)
                {
                    bFlag = false;
                    MessageBox.Show("Please Select This is Select For Farmer Meet Or Not?", "Demo Plots", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    cbFarmerMeet.Focus();
                    return bFlag;
                }
               
            }
            if (cbDemoPlot.SelectedIndex > 0)
            {
                if (txtReason.Text.Length == 0 )
                {
                    bFlag = false;
                    MessageBox.Show("Please Enter Reason", "Demo Plots", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    txtReason.Focus();
                    return bFlag;
                }
            }
            return bFlag;

        }
      

        #region "INSERT AND UPDATE DATA"

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            if (CheckData() == true)
            {
                if (SaveDemoPlotHeadDetails() > 0)
                {
                    if (SaveDemoPlotProductDetails() > 0)
                    {
                        SaveDemoPlotResults();
                        //SaveDemoPlotImageDetails();
                        MessageBox.Show("Data Saved Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flagUpdate = false;
                        btnCancel_Click(null, null);
                        GenerateTransactionNo();
                        if (sEcode.Length > 0 && sActivityDate.Length > 0)
                        {
                            objEmployeeDARWithTourBills.FillEmployeeActivityDetails(Convert.ToInt32(sEcode), sActivityDate);
                            this.Close();
                            this.Dispose();
                        }
                    }
                    else
                    {
                        objSQLdb = new SQLDB();
                        flagUpdate = false;
                        string strCmd = "DELETE FROM SERVICES_DEMO_PLOTS_HEAD WHERE SDPH_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                        int iRes = objSQLdb.ExecuteSaveData(strCmd);
                        MessageBox.Show("Data Not Saved ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Data Not Saved ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }
     
        private int SaveDemoPlotHeadDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            string sDemoPlot = "", sFarmerMeet = "";
            try
            {
                string[] strData = null;
               
                strData = cbBranch.SelectedValue.ToString().Split('@');
                if (cbDemoPlot.SelectedIndex > 0)
                {
                    sDemoPlot = cbDemoPlot.Text.ToString();
                }
                else
                {
                    sDemoPlot = "";
                }
                if (cbFarmerMeet.SelectedIndex > 0)
                {
                    sFarmerMeet = cbFarmerMeet.Text.ToString();
                }
                else
                {
                    sFarmerMeet = "";
                }

                strCommand = "DELETE FROM SERVICES_DEMO_PLOTS_RESULT WHERE SDPR_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                strCommand += " DELETE FROM SERVICES_DEMO_PLOTS_ATTENDENTS WHERE SDPA_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                strCommand += " DELETE FROM SERVICES_DEMO_PLOTS_PRODUCTS WHERE SDPP_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";               

                if (flagUpdate == true)
                {
                    strCommand += " UPDATE SERVICES_DEMO_PLOTS_HEAD SET SDPH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                "', SDPH_STATE_CODE='" + strData[1] + "',SDPH_BRANCH_CODE='" + strData[0] +
                                "', SDPH_TRN_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                "', SDPH_CAMP_NAME='" + cbCamps.Text.ToString()  +
                                "', SDPH_DOC_MONTH='"+ Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                "', SDPH_COND_BY_ECODE=" + Convert.ToInt32(cbEcode.SelectedValue) +
                                ",  SDPH_VILLAGE='" + txtVillage.Text.ToString() +
                                "', SDPH_MANDAL='" + txtMandal.Text + "',SDPH_DISTRICT='" + txtDistrict.Text.ToString() +
                                "', SDPH_STATE='" + txtState.Text.ToString() +
                                "', SDPH_PIN='"+ txtPin.Text.ToString() +
                                "', SDPH_AUTHORIZED_BY='',SDPH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                "', SDPH_LAST_MODIFIED_DATE=getdate(),SDPH_SUCCESS_OR_FAILURE='" + sDemoPlot + 
                                "', SDPH_IS_FOR_FARMER_MEET='"+ sFarmerMeet +
                                "', SDPH_REASON='"+ txtReason.Text.ToString().Replace("'","") +
                                "', SDPH_STATUS='"+ cbDPStatus.Text.ToString() +
                                "', SDPH_FARMER_NAME='"+ txtCustomerName.Text.ToString().Replace("'"," ") +
                                "', SDPH_HOUSE_NO='"+ txtHouseNo.Text.ToString().Replace("'"," ") +
                                "', SDPH_LAND_MARK='"+ txtLandMark.Text.ToString().Replace("'"," ") +
                                "', SDPH_MOBILE_NO='"+ txtMobileNo.Text.ToString() +
                                "' WHERE SDPH_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";                   
                }
                else if (flagUpdate == false)
                {
                    GenerateTransactionNo();
                    objSQLdb = new SQLDB();

                    strCommand = "INSERT INTO SERVICES_DEMO_PLOTS_HEAD(SDPH_COMPANY_CODE " +
                                                                       ", SDPH_STATE_CODE " +
                                                                       ", SDPH_BRANCH_CODE " +
                                                                       ", SDPH_DOC_MONTH " +
                                                                       ", SDPH_TRN_TYPE " +
                                                                       ", SDPH_TRN_NUMBER " +
                                                                       ", SDPH_TRN_DATE " +
                                                                       ", SDPH_COND_BY_ECODE " +
                                                                       ", SDPH_CAMP_NAME " +
                                                                       ", SDPH_VILLAGE " +
                                                                       ", SDPH_AUTHORIZED_BY " +
                                                                       ", SDPH_MANDAL " +
                                                                       ", SDPH_DISTRICT " +
                                                                       ", SDPH_STATE " +
                                                                       ", SDPH_FARMER_NAME " +
                                                                       ", SDPH_HOUSE_NO " +
                                                                       ", SDPH_LAND_MARK " +
                                                                       ", SDPH_MOBILE_NO " +
                                                                       ", SDPH_SUCCESS_OR_FAILURE " +     
                                                                       ", SDPH_IS_FOR_FARMER_MEET "+
                                                                       ", SDPH_REASON "+
                                                                       ", SDPH_CREATED_BY "+
                                                                       ", SDPH_CREATED_DATE "+
                                                                       ", SDPH_STATUS "+
                                                                       ", SDPH_PIN "+
                                                                       ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                       "','" + strData[1] + "','" + strData[0] +
                                                                       "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                       "','DEMO PLOTS','" + txtTrnNo.Text.ToString() +
                                                                       "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                                                       "'," + Convert.ToInt32(cbEcode.SelectedValue) +
                                                                       ",'" + cbCamps.Text.ToString() +
                                                                       "','" + txtVillage.Text.ToString() +
                                                                       "','','" + txtMandal.Text.ToString() +
                                                                       "','" + txtDistrict.Text.ToString() +
                                                                       "','" + txtState.Text.ToString() +                                                                      
                                                                       "','"+ txtCustomerName.Text.ToString().Replace("'","") +
                                                                       "','"+ txtHouseNo.Text.ToString().Replace("'","") +
                                                                       "','"+ txtLandMark.Text.ToString().Replace("'","") +
                                                                       "','"+ txtMobileNo.Text.ToString() +
                                                                       "','"+ sDemoPlot +
                                                                       "','"+ sFarmerMeet +"','"+ txtReason.Text.ToString().Replace("'","") +
                                                                       "','"+ CommonData.LogUserId +
                                                                       "',getdate(),'"+ cbDPStatus.Text.ToString() +
                                                                       "','"+ txtPin.Text.ToString() +"')";
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

        private int SaveDemoPlotProductDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            try
            {
                string[] strData = cbBranch.SelectedValue.ToString().Split('@');
                if (gvProductDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                    {
                        if (Convert.ToString(gvProductDetails.Rows[i].Cells["TreatedArea"].Value) == "")
                        {
                            gvProductDetails.Rows[i].Cells["TreatedArea"].Value = gvProductDetails.Rows[i].Cells["CropArea"].Value.ToString();
                        }
                        strCommand += "INSERT INTO SERVICES_DEMO_PLOTS_PRODUCTS(SDPP_COMPANY_CODE " +
                                                                            ", SDPP_STATE_CODE " +
                                                                            ", SDPP_BRANCH_CODE " +
                                                                            ", SDPP_DOC_MONTH " +
                                                                            ", SDPP_TRN_TYPE " +
                                                                            ", SDPP_TRN_NUMBER " +
                                                                            ", SDPP_PRODUCT_ID " +
                                                                            ", SDPP_CROP_ID " +
                                                                            ", SDPP_CROP_STAGE " +
                                                                            ", SDPP_AREA " +
                                                                            ", SDPP_TREATED_AREA "+
                                                                            ", SDPP_REMARKS " +
                                                                            ", SDPP_QTY "+
                                                                            ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                            "','" + strData[1] +
                                                                            "','" + strData[0] +
                                                                            "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                            "','DEMO PLOTS','" + txtTrnNo.Text.ToString() +
                                                                            "','" + gvProductDetails.Rows[i].Cells["ProductID"].Value.ToString() +
                                                                            "','" + gvProductDetails.Rows[i].Cells["CropId"].Value.ToString() +
                                                                            "','" + gvProductDetails.Rows[i].Cells["CropStage"].Value.ToString().Replace("'","") +
                                                                            "','" + gvProductDetails.Rows[i].Cells["CropArea"].Value.ToString() +
                                                                            "','" + gvProductDetails.Rows[i].Cells["TreatedArea"].Value.ToString()+
                                                                            "','" + gvProductDetails.Rows[i].Cells["Remarks"].Value.ToString().Replace("'","") + 
                                                                            "',"+ Convert.ToDouble(gvProductDetails.Rows[i].Cells["Qty"].Value) +")";
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
           
            return iRes;

        }
         

        private int SaveDemoPlotResults()
        {
            objSQLdb = new SQLDB();
            int ires = 0;
            string strCmd = "";
            try
            {
                byte[] arrCropArea;
                byte[] arrTreatedArea;
                arrCropArea = null;
                arrTreatedArea = null;
                ImageConverter converter = new ImageConverter();
                string[] strData = cbBranch.SelectedValue.ToString().Split('@');

                if (gvDemoPlotResults.Rows.Count > 0)
                {
                    for (int j = 0; j < gvDemoPlotResults.Rows.Count; j++)
                    {
                        if (Convert.ToString(gvDemoPlotResults.Rows[j].Cells["AoEcode"].Value) == "")
                        {
                            gvDemoPlotResults.Rows[j].Cells["AoEcode"].Value = cbEcode.SelectedValue.ToString();
                        }
                        if (Convert.ToString(gvDemoPlotResults.Rows[j].Cells["CropImage"].Value) != "")
                            arrCropArea = (byte[])gvDemoPlotResults.Rows[j].Cells["CropImage"].Value;
                        if (Convert.ToString(gvDemoPlotResults.Rows[j].Cells["TreatedAreaImage"].Value) != "")
                            arrTreatedArea = (byte[])gvDemoPlotResults.Rows[j].Cells["TreatedAreaImage"].Value;

                        strCmd += "INSERT INTO SERVICES_DEMO_PLOTS_RESULT(SDPR_COMPANY_CODE " +
                                                                             ", SDPR_STATE_CODE " +
                                                                             ", SDPR_BRANCH_CODE " +
                                                                             ", SDPR_DOC_MONTH " +
                                                                             ", SDPR_TRN_TYPE " +
                                                                             ", SDPR_TRN_NUMBER " +
                                                                             ", SDPR_SL_NO " +
                                                                             ", SDPR_APPL_DATE " +
                                                                             ", SDPR_OBSERV_DATE " +
                                                                             ", SDPR_RESULT_NOTIFY " +
                                                                             ", SDPR_REMARKS " +
                                                                             ", SDPR_AO_ECODE "+
                                                                             ", SDPR_FARMER_OPINION ";
                        if (arrCropArea != null && arrTreatedArea == null)
                        {
                            strCmd += ",SDPR_CROP_AREA_IMAGE ";
                        }
                        if (arrTreatedArea != null && arrCropArea == null)
                        {
                            strCmd += ",SDPR_TREATED_AREA_IMAGE";

                        }
                        if (arrTreatedArea != null && arrCropArea != null)
                        {
                            strCmd += ",SDPR_CROP_AREA_IMAGE,SDPR_TREATED_AREA_IMAGE";

                        }
                        strCmd += ")VALUES" +
                                "('" + cbCompany.SelectedValue.ToString() +
                                "','" + strData[1] +
                                "','" + strData[0] +
                                "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                "','DEMO PLOTS','" + txtTrnNo.Text.ToString() +
                                "'," + Convert.ToInt32(gvDemoPlotResults.Rows[j].Cells["SlNo_Results"].Value) +
                                ",'" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                "','" + Convert.ToDateTime(gvDemoPlotResults.Rows[j].Cells["ObservDate"].Value).ToString("dd/MMM/yyyy") +
                                "','" + gvDemoPlotResults.Rows[j].Cells["NotifyResult"].Value.ToString().Replace("'", " ") +
                                "','" + gvDemoPlotResults.Rows[j].Cells["ResultRemarks"].Value.ToString().Replace("'", " ") +
                                "'," + Convert.ToInt32(gvDemoPlotResults.Rows[j].Cells["AoEcode"].Value) +
                                ",'" + gvDemoPlotResults.Rows[j].Cells["FarmerOpinion"].Value.ToString() + "'";

                        if (arrCropArea != null && arrTreatedArea == null)
                        {
                            strCmd += ", @ImageCrop";                            
                        }
                        if (arrTreatedArea != null && arrCropArea == null)
                        {
                            strCmd += ", @ImageTreatedArea";                            
                        }
                        if (arrTreatedArea != null && arrCropArea != null)
                        {
                            strCmd += ", @ImageCrop,@ImageTreatedArea";                           
                        }

                        strCmd += ")";


                        string sConnVal = ConfigurationSettings.AppSettings["DBCon"].ToString();
                        objSecurity = new Security();
                        SqlConnection Con = new SqlConnection(objSecurity.GetDecodeString(sConnVal));
                        SqlCommand SqlCom = new SqlCommand(strCmd, Con);
                        if (arrCropArea != null && arrTreatedArea == null)
                        {                            
                            SqlCom.Parameters.Add(new SqlParameter("@ImageCrop", (object)arrCropArea));
                        }
                        if (arrTreatedArea != null && arrCropArea == null)
                        {                            
                            SqlCom.Parameters.Add(new SqlParameter("@ImageTreatedArea", (object)arrTreatedArea));
                        }
                        if (arrTreatedArea != null && arrCropArea != null)
                        {                         
                            SqlCom.Parameters.Add(new SqlParameter("@ImageCrop", (object)arrCropArea));
                            SqlCom.Parameters.Add(new SqlParameter("@ImageTreatedArea", (object)arrTreatedArea));
                        }

                       
                                              

                        Con.Open();
                        ires = SqlCom.ExecuteNonQuery();
                        Con.Close();
                        arrCropArea = null;
                        arrTreatedArea = null;
                        strCmd = "";
                    }
                }
                //if (strCmd != "")
                //{
                //    ires = objSQLdb.ExecuteSaveData(strCmd);
                //}
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return ires;
        }

        private void SaveDemoPlotImageDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCmd = "";
            try
            {
                byte[] arr;
                ImageConverter converter = new ImageConverter();

                //if (gvDocDetl.Rows.Count > 0)
                //{
                //    for (int i = 0; i < gvDocDetl.Rows.Count; i++)
                //    {
                //        arr = (byte[])gvDocDetl.Rows[i].Cells["DocImage"].Value;

                //        strCmd = "INSERT INTO SERVICES_DEMO_PLOTS_IMAGES(SDPI_COMPANY_CODE " +
                //                                                      ", SDPI_STATE_CODE " +
                //                                                      ", SDPI_BRANCH_CODE " +
                //                                                      ", SDPI_TRN_TYPE " +
                //                                                      ", SDPI_TRN_NUMBER " +
                //                                                      ", SDPI_SL_NO " +
                //                                                      ", SDPI_DOC_NAME " +
                //                                                      ", SDPI_DOC_DESC " +
                //                                                      ", SDPI_IMAGE " +
                //                                                      ", SDPI_CREATED_BY " +
                //                                                      ", SDPI_CREATED_DATE " +
                //                                                      ")VALUES " +
                //                                                      "('" + cbCompany.SelectedValue.ToString() +
                //                                                      "','" + cbBranch.SelectedValue.ToString().Split('@')[1] +
                //                                                      "','" + cbBranch.SelectedValue.ToString().Split('@')[1] +
                //                                                      "','DEMO PLOTS','" + txtTrnNo.Text.ToString() +
                //                                                      "'," + Convert.ToInt32(gvDocDetl.Rows[i].Cells["SLNO"].Value) +
                //                                                      ",'" + gvDocDetl.Rows[i].Cells["DocumentName"].Value.ToString() +
                //                                                      "','" + gvDocDetl.Rows[i].Cells["DocumentDesc"].Value.ToString() +
                //                                                      "',@Image,'" + CommonData.LogUserId +
                //                                                     "',getdate())";

                //        string sConnVal = ConfigurationSettings.AppSettings["DBCon"].ToString();
                //        objSecurity = new Security();
                //        SqlConnection Con = new SqlConnection(objSecurity.GetDecodeString(sConnVal));
                //        SqlCommand SqlCom = new SqlCommand(strCmd, Con);

                //        SqlCom.Parameters.Add(new SqlParameter("@Image", (object)arr));
                //        Con.Open();
                //        iRes = SqlCom.ExecuteNonQuery();
                //        Con.Close();
                //        arr = null;
                //        strCmd = "";
                //    }
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
                
        #endregion

        

        private void btnAddProductDetails_Click(object sender, EventArgs e)
        {
            frmProductDetails productDetails = new frmProductDetails();
            productDetails.objfrmDemoPlots = this;
            productDetails.ShowDialog();
        }       
        

        #region "GET DATA FOR UPDATE"
        private void GetDemoPlotsDetails()
        {
            objServicedb = new ServiceDeptDB();
            Hashtable ht;

            DataTable dtDemoPlotsHead;

            if (txtTrnNo.Text.Length > 0)
            {
                try
                {
                    ht = objServicedb.GetDemoPlotsDetails(txtTrnNo.Text.ToString());
                    dtDemoPlotsHead = (DataTable)ht["DemoPlotsHead"];

                    FillDemoPlotHeadDetails(dtDemoPlotsHead);
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

        private void FillDemoPlotHeadDetails(DataTable dtHead)
        {
            objServicedb = new ServiceDeptDB();
            Hashtable ht;
            DataTable dtProdDetails;           
            DataTable dtDPResult;

            if (txtTrnNo.Text.Length > 0)
            {
                try
                {

                    ht = objServicedb.GetDemoPlotsDetails(txtTrnNo.Text.ToString());

                    dtProdDetails = (DataTable)ht["DemoPlotsProductDetails"];                   
                    dtDPResult = (DataTable)ht["DemoPlotsResultDetails"];

                    if (dtHead.Rows.Count > 0)
                    {
                        flagUpdate = true;

                        if ((DateTime.Now - Convert.ToDateTime(Convert.ToDateTime(dtHead.Rows[0]["CreatedDate"]).ToString("dd/MM/yyyy"))).TotalDays > 15 || dtDPResult.Rows.Count==2)
                        {
                            if (CommonData.LogUserId.ToUpper() == "ADMIN")
                            {
                                cbCamps.Enabled = true;
                                txtLandMark.ReadOnly = false;
                                txtCustomerName.ReadOnly = false;
                                txtHouseNo.ReadOnly = false;
                                dtpTrnDate.Enabled = true;
                                cbEcode.Enabled = true;
                                txtEcodeSearch.ReadOnly = false;
                                btnVillageSearch.Enabled = true;
                                btnAddProductDetails.Enabled = true;
                                gvProductDetails.Enabled = true;
                                btnDelete.Enabled = true;
                            }
                            else
                            {                                
                                cbCamps.Enabled = false;
                                txtLandMark.ReadOnly = true;
                                txtCustomerName.ReadOnly = true;
                                txtHouseNo.ReadOnly = true;
                                dtpTrnDate.Enabled = false;
                                cbEcode.Enabled = false;
                                txtEcodeSearch.ReadOnly = true;
                                btnVillageSearch.Enabled = false;
                                btnAddProductDetails.Enabled = false;
                                gvProductDetails.Enabled = false;
                                btnDelete.Enabled = false;
                            }
                        }
                        if (dtHead.Rows[0]["Status"].ToString() == "CLOSED")
                        {
                            if (CommonData.LogUserId.ToUpper() == "ADMIN")
                            {
                                btnSave.Enabled = true;
                                btnDelete.Enabled = true;
                                lblKnocking.Visible = false;
                            }
                            else
                            {
                                lblKnocking.Visible = true;
                                lblKnocking.Text = "Data Can't Modify.Demo Plot Working Status is Closed ";
                                btnSave.Enabled = false;
                                btnDelete.Enabled = false;
                            }
                        }
                        if (dtDPResult.Rows.Count >= 2 && sEcode.Length>0)
                        {
                            cbCamps.Enabled = false;
                            txtLandMark.ReadOnly = true;
                            txtCustomerName.ReadOnly = true;
                            txtHouseNo.ReadOnly = true;
                            dtpTrnDate.Enabled = false;
                            cbEcode.Enabled = false;
                            txtEcodeSearch.ReadOnly = true;
                            btnVillageSearch.Enabled = false;
                            btnAddProductDetails.Enabled = false;
                            gvProductDetails.Enabled = false;
                            btnDelete.Enabled = false;
                        }
                            

                        string stECode = dtHead.Rows[0]["Conducted_Ecode"] + "";
                        cbCompany.SelectedValue = dtHead.Rows[0]["Comp_Code"].ToString();
                        cbBranch.SelectedValue = dtHead.Rows[0]["Bran_Code"].ToString();
                       
                        dtpTrnDate.Value = Convert.ToDateTime(dtHead.Rows[0]["Trn_Date"].ToString());
                        txtEcodeSearch.Text = stECode;
                        cbEcode.SelectedValue = stECode;
                        cbCamps.Text = dtHead.Rows[0]["Camp_Name"].ToString();
                        txtVillage.Text = dtHead.Rows[0]["Village"].ToString();
                        txtMandal.Text = dtHead.Rows[0]["Mandal"].ToString();
                        txtDistrict.Text = dtHead.Rows[0]["District"].ToString();
                        txtState.Text = dtHead.Rows[0]["State"].ToString();
                        txtPin.Text = dtHead.Rows[0]["Pin"].ToString();                  

                        txtCustomerName.Text = dtHead.Rows[0]["FarmerName"].ToString();
                        txtHouseNo.Text = dtHead.Rows[0]["HouseNo"].ToString();
                        txtLandMark.Text = dtHead.Rows[0]["LandMark"].ToString();
                        txtMobileNo.Text = dtHead.Rows[0]["MobileNo"].ToString();

                        cbDPStatus.Text = dtHead.Rows[0]["Status"].ToString();

                        if (dtHead.Rows[0]["SuccessOrFailure"].ToString() != "")
                            cbDemoPlot.Text = dtHead.Rows[0]["SuccessOrFailure"].ToString();
                        else
                            cbDemoPlot.SelectedIndex = 0;

                        if (dtHead.Rows[0]["FarmerMeet"].ToString() != "")
                            cbFarmerMeet.Text = dtHead.Rows[0]["FarmerMeet"].ToString();
                        else
                            cbFarmerMeet.SelectedIndex = 0;
                        txtReason.Text=dtHead.Rows[0]["Reason"].ToString();
                       
                        FillProductDetails(dtProdDetails);                       
                        FillDempPlotsResults(dtDPResult);

                    }
                    else
                    {                       
                        flagUpdate = false;
                        GenerateTransactionNo();
                        lblKnocking.Visible = false;
                        dtpTrnDate.Value = DateTime.Today;
                        cbEcode.SelectedIndex = -1;
                        txtEcodeSearch.Text = "";
                        txtVillage.Text = "";
                        txtMandal.Text = "";
                        txtState.Text = "";
                        txtDistrict.Text = "";
                        btnSave.Enabled = true;
                        btnDelete.Enabled = true;
                        txtState.Text = "";
                        dtDemoPlotResults.Rows.Clear();
                        dtProductDetails.Rows.Clear();
                        gvProductDetails.Rows.Clear();
                        gvDemoPlotResults.Rows.Clear();
                        cbDPStatus.SelectedIndex = 0;
                        txtPin.Text = "";
                        cbCamps.SelectedIndex = 0;
                        txtCustomerName.Text = "";
                        txtHouseNo.Text = "";
                        txtLandMark.Text = "";
                        txtMobileNo.Text = "";
                        cbCamps.Enabled = true;
                        txtLandMark.ReadOnly = false;
                        txtCustomerName.ReadOnly = false;
                        txtHouseNo.ReadOnly = false;
                        dtpTrnDate.Enabled = true;
                        cbEcode.Enabled = true;
                        txtEcodeSearch.ReadOnly = false;
                        btnVillageSearch.Enabled = true;
                        btnAddProductDetails.Enabled = true;
                        gvProductDetails.Enabled = true;
                    }                                     
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
           

        }

        private void FillProductDetails(DataTable dtProducts)
        {                     
            dtProductDetails.Rows.Clear();
           
            if (txtTrnNo.Text.Length > 0)
            {
                try
                {
                    if (dtProducts.Rows.Count > 0)
                    {                       
                        for (int i = 0; i < dtProducts.Rows.Count; i++)
                        {                          

                            dtProductDetails.Rows.Add(new Object[] {"-1", dtProducts.Rows[i]["Product_Id"].ToString(),
                                                                       dtProducts.Rows[i]["Crop_Id"].ToString(),                                                                      
                                                                       dtProducts.Rows[i]["CropName"].ToString(),
                                                                       dtProducts.Rows[i]["Crop_Area"].ToString(),
                                                                       dtProducts.Rows[i]["Crop_Stage"].ToString(),
                                                                       dtProducts.Rows[i]["TreatedArea"].ToString(),  
                                                                       dtProducts.Rows[i]["Prod_Name"].ToString(),
                                                                       dtProducts.Rows[i]["ProdQty"].ToString(),
                                                                       dtProducts.Rows[i]["Category_Name"].ToString(),                                                                                                                                           
                                                                       dtProducts.Rows[i]["Remarks"].ToString()});
                            GetProductDetails();


                        }
                    }
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }           

        }
            
     
        private void FillDempPlotsResults(DataTable dtResult)
        {
            gvDemoPlotResults.Rows.Clear();
            if (txtTrnNo.Text.Length > 0)
            {
                try
                {
                    if (dtResult.Rows.Count > 0)
                    {                        
                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                            gvDemoPlotResults.Rows.Add();
                            gvDemoPlotResults.Rows[i].Cells["SlNo_Results"].Value = (i + 1).ToString();
                            gvDemoPlotResults.Rows[i].Cells["AoEcode"].Value = dtResult.Rows[i]["AoEcode"].ToString();
                            gvDemoPlotResults.Rows[i].Cells["ObservDate"].Value = dtResult.Rows[i]["Observ_Date"].ToString();
                            gvDemoPlotResults.Rows[i].Cells["AoName"].Value = dtResult.Rows[i]["AoName"].ToString();
                            gvDemoPlotResults.Rows[i].Cells["FarmerOpinion"].Value = dtResult.Rows[i]["FarmerOpinion"].ToString();
                            gvDemoPlotResults.Rows[i].Cells["NotifyResult"].Value = dtResult.Rows[i]["Notify_Result"].ToString();
                            gvDemoPlotResults.Rows[i].Cells["ResultRemarks"].Value = dtResult.Rows[i]["Remarks"].ToString();
                            gvDemoPlotResults.Rows[i].Cells["CropImage"].Value = dtResult.Rows[i]["CropAreaImage"];
                            gvDemoPlotResults.Rows[i].Cells["TreatedAreaImage"].Value = dtResult.Rows[i]["TreatedImage"];

                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }
        #endregion

        #region "GRIDVIEW DETAILS"
       
        //public void GetDemoPlotResultDetails()
        //{
        //    int intRow = 1;
        //    gvDemoPlotResults.Rows.Clear();
        //    try
        //    {
                
        //        for (int i = 0; i < dtDemoPlotResults.Rows.Count; i++)
        //        {
        //            DataGridViewRow tempRow = new DataGridViewRow();
        //            DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
        //            cellSLNO.Value = intRow;
        //            dtDemoPlotResults.Rows[i]["SlNo_Results"] = intRow;
        //            tempRow.Cells.Add(cellSLNO);
                                       
        //            DataGridViewCell cellAoEcode = new DataGridViewTextBoxCell();
        //            cellAoEcode.Value = dtDemoPlotResults.Rows[i]["AoEcode"];
        //            tempRow.Cells.Add(cellAoEcode);

        //            DataGridViewCell cellObservDate = new DataGridViewTextBoxCell();
        //            cellObservDate.Value = Convert.ToDateTime(dtDemoPlotResults.Rows[i]["ObservDate"]).ToShortDateString();
        //            tempRow.Cells.Add(cellObservDate);

        //            DataGridViewCell cellAoName = new DataGridViewTextBoxCell();
        //            cellAoName.Value = dtDemoPlotResults.Rows[i]["AoName"];
        //            tempRow.Cells.Add(cellAoName);

        //            DataGridViewCell cellFarmerOpinion = new DataGridViewTextBoxCell();
        //            cellFarmerOpinion.Value = dtDemoPlotResults.Rows[i]["FarmerOpinion"];
        //            tempRow.Cells.Add(cellFarmerOpinion);

        //            DataGridViewCell cellNotifyResult = new DataGridViewTextBoxCell();
        //            cellNotifyResult.Value = dtDemoPlotResults.Rows[i]["NotifyResult"];
        //            tempRow.Cells.Add(cellNotifyResult);

        //            DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
        //            cellRemarks.Value = dtDemoPlotResults.Rows[i]["ResultRemarks"];
        //            tempRow.Cells.Add(cellRemarks);                                    

        //            DataGridViewCell cellCropImage = new DataGridViewTextBoxCell();
        //            cellCropImage.Value = dtDemoPlotResults.Rows[i]["CropImage"];
        //            tempRow.Cells.Add(cellCropImage);
                   
        //            DataGridViewCell cellTreatedAreaImage = new DataGridViewTextBoxCell();
        //            cellTreatedAreaImage.Value = dtDemoPlotResults.Rows[i]["TreatedImage"];
        //            tempRow.Cells.Add(cellTreatedAreaImage);

        //            intRow = intRow + 1;
        //            gvDemoPlotResults.Rows.Add(tempRow);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }

        //}
                   
        public void GetProductDetails()
        {
            int intRow = 1;
            gvProductDetails.Rows.Clear();
            try
            {
                for (int i = 0; i < dtProductDetails.Rows.Count; i++)
                {

                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    dtProductDetails.Rows[i]["SLNO_Product"] = intRow;
                    tempRow.Cells.Add(cellSLNO);

                    DataGridViewCell cellProductId = new DataGridViewTextBoxCell();
                    cellProductId.Value = dtProductDetails.Rows[i]["ProductID"];
                    tempRow.Cells.Add(cellProductId);

                    DataGridViewCell cellCropId = new DataGridViewTextBoxCell();
                    cellCropId.Value = dtProductDetails.Rows[i]["CropId"];
                    tempRow.Cells.Add(cellCropId);

                    DataGridViewCell cellCropName = new DataGridViewTextBoxCell();
                    cellCropName.Value = dtProductDetails.Rows[i]["CropName"];
                    tempRow.Cells.Add(cellCropName);

                    DataGridViewCell cellCropArea = new DataGridViewTextBoxCell();
                    cellCropArea.Value = dtProductDetails.Rows[i]["CropArea"];
                    tempRow.Cells.Add(cellCropArea);

                    DataGridViewCell cellCropStage = new DataGridViewTextBoxCell();
                    cellCropStage.Value = dtProductDetails.Rows[i]["CropStage"];
                    tempRow.Cells.Add(cellCropStage);
                    
                    DataGridViewCell cellTreatedArea = new DataGridViewTextBoxCell();
                    cellTreatedArea.Value = dtProductDetails.Rows[i]["TreatedArea"];
                    tempRow.Cells.Add(cellTreatedArea);

                    DataGridViewCell cellProductName = new DataGridViewTextBoxCell();
                    cellProductName.Value = dtProductDetails.Rows[i]["prodName"];
                    tempRow.Cells.Add(cellProductName);

                    DataGridViewCell cellQty = new DataGridViewTextBoxCell();
                    cellQty.Value = dtProductDetails.Rows[i]["Qty"];
                    tempRow.Cells.Add(cellQty);

                    DataGridViewCell cellCategoryName = new DataGridViewTextBoxCell();
                    cellCategoryName.Value = dtProductDetails.Rows[i]["Category"];
                    tempRow.Cells.Add(cellCategoryName);                 


                    DataGridViewCell cellCropRemarks = new DataGridViewTextBoxCell();
                    cellCropRemarks.Value = dtProductDetails.Rows[i]["Remarks"];
                    tempRow.Cells.Add(cellCropRemarks);

                    intRow = intRow + 1;
                    gvProductDetails.Rows.Add(tempRow);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        #endregion

        #region "EDITING AND DELETING GRID DETAILS"

        private void gvDemoPlotResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                byte[] CropImageArr;
                byte[] TreatedAreaImageArr;
                CropImageArr = null;
                TreatedAreaImageArr = null;

                if (e.ColumnIndex == gvDemoPlotResults.Columns["viewCropImage"].Index)
                {
                    if (Convert.ToString(gvDemoPlotResults.Rows[e.RowIndex].Cells["CropImage"].Value) != "")
                        CropImageArr = (byte[])gvDemoPlotResults.Rows[e.RowIndex].Cells["CropImage"].Value;
                    frmDisplayImage ImgView = new frmDisplayImage(CropImageArr);
                    ImgView.objfrmDemoPlots = this;
                    ImgView.ShowDialog();
                }
                if (e.ColumnIndex == gvDemoPlotResults.Columns["ViewTreatedImage"].Index)
                {
                    if (Convert.ToString(gvDemoPlotResults.Rows[e.RowIndex].Cells["TreatedAreaImage"].Value) != "")
                        TreatedAreaImageArr = (byte[])gvDemoPlotResults.Rows[e.RowIndex].Cells["TreatedAreaImage"].Value;
                    frmDisplayImage ImgView = new frmDisplayImage(TreatedAreaImageArr);
                    ImgView.objfrmDemoPlots = this;
                    ImgView.ShowDialog();
                }
                

                if (e.ColumnIndex == gvDemoPlotResults.Columns["Del_Results"].Index)
                {

                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        DataGridViewRow dgvr = gvDemoPlotResults.Rows[e.RowIndex];
                        gvDemoPlotResults.Rows.Remove(dgvr);
                    }
                    if (gvDemoPlotResults.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvDemoPlotResults.Rows.Count; i++)
                        {
                            gvDemoPlotResults.Rows[i].Cells["SlNo_Results"].Value = (i + 1).ToString();
                        }
                    }


                    //DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (dlgResult == DialogResult.Yes)
                    //{
                    //    int SlNo = Convert.ToInt32(gvDemoPlotResults.Rows[e.RowIndex].Cells[gvDemoPlotResults.Columns["SlNo_Results"].Index].Value);
                    //    DataRow[] dr = dtDemoPlotResults.Select("SlNo_Results=" + SlNo);
                    //    dtDemoPlotResults.Rows.Remove(dr[0]);
                    //    GetDemoPlotResultDetails();
                    //    MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                }

            }
        }
          

        private void gvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                
                if (e.ColumnIndex == gvProductDetails.Columns["Edit_ProdDetails"].Index)
                {
                    if (Convert.ToBoolean(gvProductDetails.Rows[e.RowIndex].Cells["Edit_ProdDetails"].Selected) == true)
                    {
                        
                        int SlNo = Convert.ToInt32(gvProductDetails.Rows[e.RowIndex].Cells[gvProductDetails.Columns["SLNO_Product"].Index].Value);
                        DataRow[] dr = dtProductDetails.Select("SLNO_Product=" + SlNo);

                        frmProductDetails ProductDetails = new frmProductDetails(dr);
                        ProductDetails.objfrmDemoPlots = this;
                        ProductDetails.ShowDialog();
                   }

                }

                if (e.ColumnIndex == gvProductDetails.Columns["Del_ProdDetails"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvProductDetails.Rows[e.RowIndex].Cells[gvProductDetails.Columns["SLNO_Product"].Index].Value);
                        DataRow[] dr = dtProductDetails.Select("SLNO_Product=" + SlNo);
                        dtProductDetails.Rows.Remove(dr[0]);
                        GetProductDetails();
                        MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
        }
        #endregion

        

        private void txtTrnNo_KeyPress(object sender, KeyPressEventArgs e)
        {           
            //if ( txtTrnNo.SelectionStart < 17)
            //    e.Handled = true;
            //if (e.KeyChar != '\b')
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
        }

        private void txtTrnNo_Enter(object sender, EventArgs e)
        {
            //this.txtTrnNo.Select(txtTrnNo.Text.Length, 0);
        }

        private void txtTrnNo_Validated(object sender, EventArgs e)
        {
            if (txtTrnNo.Text.Length > 19)
            {               
                GetDemoPlotsDetails();
            }
            else
            {
                flagUpdate = false;
              
                txtCustomerName.Text = "";
                txtHouseNo.Text = "";
                txtLandMark.Text = "";
                txtMobileNo.Text = "";
                cbDPStatus.SelectedIndex = 0;
                txtPin.Text = "";
                cbEcode.SelectedIndex = -1;
                txtEcodeSearch.Text = "";
                cbCamps.SelectedIndex = 0;
                
                txtVillage.Text = "";
                txtMandal.Text = "";
                txtState.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                dtProductDetails.Rows.Clear();
                dtDemoPlotResults.Rows.Clear();               
                gvProductDetails.Rows.Clear();
                gvDemoPlotResults.Rows.Clear();
              
                GenerateTransactionNo();

            }
               
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iResult = 0;
            string strCommand = "";

            if (txtTrnNo.Text != "" && flagUpdate == true)
            {
                DialogResult result = MessageBox.Show("Do you want to delete This Record ?",
                                    "Demo Plots", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        strCommand = "DELETE FROM SERVICES_DEMO_PLOTS_RESULT WHERE SDPR_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";

                        strCommand += " DELETE FROM SERVICES_DEMO_PLOTS_ATTENDENTS WHERE SDPA_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";

                        strCommand += " DELETE FROM SERVICES_DEMO_PLOTS_PRODUCTS WHERE SDPP_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";

                        strCommand += " DELETE FROM SERVICES_DEMO_PLOTS_HEAD WHERE SDPH_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                        iResult = objSQLdb.ExecuteSaveData(strCommand);

                        if (iResult > 0)
                        {
                            MessageBox.Show("Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show(" Data Not Deleted", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
            //cbCompany.SelectedIndex = 0;
            //cbBranch.SelectedIndex = -1;          
            txtTrnNo.Text = "";
            dtpTrnDate.Value = DateTime.Today;
            cbEcode.SelectedIndex = -1;
            txtEcodeSearch.Text = "";
            txtVillage.Text = "";
            txtMandal.Text = "";
            txtState.Text = "";
            txtDistrict.Text = "";
            txtPin.Text = "";
            txtCustomerName.Text = "";
            txtHouseNo.Text = "";
            txtLandMark.Text = "";
            txtMobileNo.Text = "";
            lblKnocking.Visible = false;
            cbCamps.SelectedIndex = 0;
            dtProductDetails.Rows.Clear();
            dtDemoPlotResults.Rows.Clear();
            cbDemoPlot.SelectedIndex = 0;
            cbFarmerMeet.SelectedIndex = 0;
            txtReason.Text = "";
            cbDPStatus.SelectedIndex = 0;
            cbFarmerMeet.Visible = false;
            lblFarmerMeet.Visible = false;
            lblReason.Visible = false;
            txtReason.Visible = false;
            btnSave.Enabled = true;
            btnDelete.Enabled = true;
            gvProductDetails.Rows.Clear();
            gvDemoPlotResults.Rows.Clear();
            GenerateTransactionNo();
            dtpTrnDate.Focus();
            cbCamps.Enabled = true;
            txtLandMark.ReadOnly = false;
            txtCustomerName.ReadOnly = false;
            txtHouseNo.ReadOnly = false;
            dtpTrnDate.Enabled = true;
            cbEcode.Enabled = true;
            txtEcodeSearch.ReadOnly = false;
            btnVillageSearch.Enabled = true;
            btnAddProductDetails.Enabled = true;
            gvProductDetails.Enabled = true;
            EcodeSearch();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cbDemoPlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDemoPlot.SelectedIndex>0)
            {
                txtReason.Text = "";
                cbFarmerMeet.SelectedIndex = 0;
                cbFarmerMeet.Visible = true;
                lblFarmerMeet.Visible = true;
                lblReason.Visible = true;
                txtReason.Visible = true;
            }
            else
            {
                txtReason.Text = "";
                cbFarmerMeet.SelectedIndex = 0;
                cbFarmerMeet.Visible = false;
                lblFarmerMeet.Visible = false;
                lblReason.Visible = false;
                txtReason.Visible = false;           
            }
        }

        private void btnVillageSearch_Click(object sender, EventArgs e)
        {
            VillageSearch vilsearch = new VillageSearch("frmDemoPlots");
            vilsearch.objfrmDemoPlots = this;
            vilsearch.ShowDialog();
        }

        private void btnAddDemoPlotResults_Click(object sender, EventArgs e)
        {
            frmDemoPlotResults DemoPlotResults = new frmDemoPlotResults(cbCompany.SelectedValue.ToString(),cbBranch.SelectedValue.ToString().Split('@')[0],cbEcode.SelectedValue.ToString());
            DemoPlotResults.objfrmDemoPlots = this;
            DemoPlotResults.ShowDialog();
        }

        private void btnAddDocDetails_Click(object sender, EventArgs e)
        {
            frmAddDocumentDetails DocDetl = new frmAddDocumentDetails("DEMOPLOT");
            DocDetl.objfrmDemoPlots = this;
            DocDetl.ShowDialog();
        }
               

        private void cbCamps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCamps.SelectedIndex > 0)
            {
                txtVillage.Text = cbCamps.SelectedValue.ToString().Split('^')[1];
                txtMandal.Text = cbCamps.SelectedValue.ToString().Split('^')[2];
                txtDistrict.Text = cbCamps.SelectedValue.ToString().Split('^')[3];
                txtState.Text = cbCamps.SelectedValue.ToString().Split('^')[4];
                txtLandMark.Text = cbCamps.SelectedValue.ToString().Split('^')[5];
            }
            else
            {
                txtVillage.Text = "";
                txtMandal.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtLandMark.Text = "";
            }
        }

   
             
     
    }
}
