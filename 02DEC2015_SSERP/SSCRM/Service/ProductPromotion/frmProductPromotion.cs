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
    public partial class frmProductPromotion : Form
    {

        SQLDB objSQLdb = null;
        ServiceDeptDB objServicedb = null;       

        bool flagUpdate = false;
        public EmployeeDARWithTourBills objEmployeeDARWithTourBills;
        public DataTable dtAttEmpDetails = new DataTable();       
        public DataTable dtItemDetails = new DataTable();

        private string strECode = "", sCompCode = "", sBranCode = "", sEcode = "", sActivityDate = "",strTrnNo = "", strRefNo = "";

        public frmProductPromotion()
        {
            InitializeComponent();
        }
        public frmProductPromotion(string CompCode,string BranCode,string sEmpEcode,string sActDate)
        {
            InitializeComponent();
            sCompCode = CompCode;
            sBranCode = BranCode;
            sEcode = sEmpEcode;
            sActivityDate = sActDate;
        }
        public frmProductPromotion(string CompCode, string BranCode, string sEmpEcode, string sActDate, string sTrnNo, string sRefNo)
        {
            InitializeComponent();
            sCompCode = CompCode;
            sBranCode = BranCode;
            sEcode = sEmpEcode;
            sActivityDate = sActDate;
            strTrnNo = sTrnNo;
            strRefNo = sRefNo;
        }

       
        private void frmProductPromotion_Load(object sender, EventArgs e)
        {
         
            gvAttendedEmpDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                      System.Drawing.FontStyle.Regular);
           
            gvItemDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                      System.Drawing.FontStyle.Regular);

            #region "CREATE EMP_DETAILS TABLE"
            dtAttEmpDetails.Columns.Add("SlNo_Emp");
            dtAttEmpDetails.Columns.Add("Ecode");
            dtAttEmpDetails.Columns.Add("EmpName");
            dtAttEmpDetails.Columns.Add("Desig");
            dtAttEmpDetails.Columns.Add("Dept");
            #endregion

            #region "CREATE Item_Details TABLE"
            dtItemDetails.Columns.Add("SLNo_Item");
            dtItemDetails.Columns.Add("ItemId");
            dtItemDetails.Columns.Add("ItemName");
            dtItemDetails.Columns.Add("Qty");
            dtItemDetails.Columns.Add("ItemRemarks");
            #endregion

            dtpTrnDate.Value = DateTime.Today;

            FillCompanyData();
            FillBranchData();

            if (CommonData.BranchType == "BR")
            {
                GenerateTransactionNo();
            }


            FillEmployeeData();
            EcodeSearch();
            if(cbBranches.SelectedIndex>0)
            FillCampComboBox(cbCompany.SelectedValue.ToString(), cbBranches.SelectedValue.ToString().Split('@')[0]);

            if (sCompCode.Length > 0 && sBranCode.Length > 0 && sEcode.Length > 0 && sActivityDate.Length > 0)
            {
                cbCompany.SelectedValue = sCompCode;
                cbBranches.SelectedValue = sBranCode;
                txtEcodeSearch.Text = sEcode;
                if (sEcode.Length > 1)
                    txtEcodeSearch_TextChanged(null, null);
                cbEcode.SelectedValue = sEcode;
                dtpTrnDate.Value = Convert.ToDateTime(sActivityDate);
                dtpTrnDate.Enabled = false;                
                txtTrnNo.ReadOnly = true;
                txtEcodeSearch.ReadOnly = true;
                cbEcode.Enabled = false;
                GenerateTransactionNo();
                if(strTrnNo.Length==0)
                txtTrnNo.CausesValidation = false;
            }
            else
            {
                txtTrnNo.ReadOnly = false;
                dtpTrnDate.Enabled = true;
                txtTrnNo.CausesValidation = true;
                txtEcodeSearch.ReadOnly = false;
                cbEcode.Enabled = true;
            }
            if (strTrnNo.Length > 0)
            {
                txtTrnNo.Text = strTrnNo;
                txtTrnNo_Validated(null, null);
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
            if (cbCompany.SelectedIndex > 0)
            {
                try
                {


                    string strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE+'@'+ STATE_CODE AS BranCode " +
                                         " FROM USER_BRANCH " +
                                         " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                         " WHERE COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                         "' AND UB_USER_ID ='" + CommonData.LogUserId +
                                         "' AND BRANCH_TYPE IN ('BR','HO') ORDER BY BRANCH_NAME ASC";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];


                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = "--Select--";
                        dr[1] = "--Select--";

                        dt.Rows.InsertAt(dr, 0);

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
            else
            {
                cbBranches.DataSource = null;
            }
        }

        private void GenerateTransactionNo()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            if (cbBranches.SelectedIndex > 0)
            {
                try
                {
                    string[] BranCode = cbBranches.SelectedValue.ToString().Split('@');
                    string finyear = CommonData.FinancialYear.Substring(2, 2) + CommonData.FinancialYear.Substring(7, 2);
                    string strNewNo = BranCode[0] + finyear + "PM-";

                    string strCommand = "SELECT ISNULL(MAX(SUBSTRING(ISNULL(SPPH_TRN_NUMBER, '" + strNewNo + "'),17,21)),0) + 1 " +
                                        " FROM SERVICES_PRODUCT_PROMOTION_HEAD " +
                                        " WHERE SPPH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "' AND SPPH_BRANCH_CODE='" + BranCode[0] + "' ";
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

        private void FillCampComboBox(string strCompCode,string strBranCode)
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

        private bool CheckData()
        {
            bool bFlag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbCompany.Focus();
            }
            else if (cbBranches.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbBranches.Focus();
            }
            else if (cbCamps.SelectedIndex == 0 || cbCamps.SelectedIndex == -1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Camp Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbCamps.Focus();
            }
            else if (cbEcode.SelectedIndex == -1)
            {
                bFlag = false;
                MessageBox.Show("Please Select Conducted Emp Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbEcode.Focus();
            }
            else if (txtVillage.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Village Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bFlag;
            }
            else if (gvItemDetails.Rows.Count == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Add Product Promotion Type Details", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bFlag;

            }           

            return bFlag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();

            if (CheckData() == true)
            {
                if (SaveHeadDetails() > 0)
                {
                    if (SaveItemDetails() > 0)
                    {
                        SaveEmpDetails();
                        MessageBox.Show("Data Saved Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        string strCommand = "DELETE FROM SERVICES_PRODUCT_PROMOTION_HEAD WHERE SPPH_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                        int iResult = objSQLdb.ExecuteSaveData(strCommand);
                        flagUpdate = false;
                    }
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    
        #region "SAVE AND UPDATE DATA"

        private int SaveHeadDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            try
            {
                string[] strBranCode = cbBranches.SelectedValue.ToString().Split('@');

                strCommand = "DELETE FROM SERVICES_PRODUCT_PROMOTION_ATTENDENTS WHERE SPPA_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
               
                strCommand += " DELETE FROM SERVICES_PRODUCT_PROMOTION_ITEMS WHERE SPPI_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
               
                strCommand += "DELETE FROM SERVICES_PRODUCT_PROMOTION_DEMO_PRODUCTS WHERE SPDP_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";               


                if (flagUpdate == true)
                {
                    strCommand += "UPDATE SERVICES_PRODUCT_PROMOTION_HEAD SET SPPH_COMPANY_CODE='"+ cbCompany.SelectedValue.ToString() +
                                "', SPPH_STATE_CODE='"+ strBranCode[1] +"',SPPH_BRANCH_CODE='"+ strBranCode[0] +
                                "', SPPH_DOC_MONTH='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                "', SPPH_TRN_DATE='"+ Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                "', SPPH_TRN_TYPE='PROD PROMOTION',SPPH_COND_BY_ECODE="+ Convert.ToInt32(cbEcode.SelectedValue) +
                                ",  SPPH_LOCATION_NAME='"+ cbCamps.Text.ToString() +
                                "', SPPH_ADDR='"+ txtHouseNo.Text.ToString() +
                                "', SPPH_LAND_MARK='"+ txtLandMark.Text.ToString() +
                                "', SPPH_VILLAGE='"+ txtVillage.Text.ToString() +
                                "', SPPH_MANDAL='"+ txtMandal.Text.ToString() +
                                "', SPPH_DISTRICT='"+ txtDistrict.Text.ToString() +
                                "', SPPH_STATE='"+ txtState.Text.ToString() +
                                "', SPPH_PIN='"+ txtPin.Text.ToString() +
                                "', SPPH_AUTHORIZED_BY='admin',SPPH_LAST_MODIFIED_BY='"+ CommonData.LogUserId +
                                "',SPPH_LAST_MODIFIED_DATE=getdate() " +
                                " WHERE SPPH_TRN_NUMBER='"+ txtTrnNo.Text.ToString() +"'";

                    flagUpdate = false;
                }
                else
                {
                    strCommand = "INSERT INTO SERVICES_PRODUCT_PROMOTION_HEAD(SPPH_COMPANY_CODE " +
                                                                         ", SPPH_STATE_CODE " +
                                                                         ", SPPH_BRANCH_CODE " +
                                                                         ", SPPH_DOC_MONTH " +
                                                                         ", SPPH_TRN_TYPE " +
                                                                         ", SPPH_TRN_NUMBER " +
                                                                         ", SPPH_TRN_DATE " +
                                                                         ", SPPH_COND_BY_ECODE " +
                                                                         ", SPPH_LOCATION_NAME " +
                                                                         ", SPPH_ADDR " +
                                                                         ", SPPH_LAND_MARK " +
                                                                         ", SPPH_VILLAGE " +
                                                                         ", SPPH_MANDAL " +
                                                                         ", SPPH_DISTRICT " +
                                                                         ", SPPH_STATE " +
                                                                         ", SPPH_PIN " +
                                                                         ", SPPH_CREATED_BY " +
                                                                         ", SPPH_AUTHORIZED_BY " +
                                                                         ", SPPH_CREATED_DATE " +
                                                                         ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                         "','" + strBranCode[1] +
                                                                         "','" + strBranCode[0] +
                                                                         "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                         "','PROD PROMOTION','" + txtTrnNo.Text.ToString() +
                                                                         "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                                                         "'," + Convert.ToInt32(cbEcode.SelectedValue) +
                                                                         ",'" + cbCamps.Text.ToString() +
                                                                         "','" + txtHouseNo.Text.ToString() +
                                                                         "','" + txtLandMark.Text.ToString() +
                                                                         "','" + txtVillage.Text.ToString() +
                                                                         "','" + txtMandal.Text.ToString() +
                                                                         "','" + txtDistrict.Text.ToString() +
                                                                         "','" + txtState.Text.ToString() +
                                                                         "','" + txtPin.Text.ToString() +
                                                                         "','" + CommonData.LogUserId +
                                                                         "','',getdate())";
                }

                if (strCommand != "")
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

        private int SaveEmpDetails()
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strInsert = "";
            try
            {
                string[] strBranCode = cbBranches.SelectedValue.ToString().Split('@');
               
                if (gvAttendedEmpDetails.Rows.Count > 0)
                {
                    for (int k = 0; k < gvAttendedEmpDetails.Rows.Count; k++)
                    {
                        strInsert += "INSERT INTO SERVICES_PRODUCT_PROMOTION_ATTENDENTS(SPPA_COMPANY_CODE " +
                                                                                     ", SPPA_STATE_CODE " +
                                                                                     ", SPPA_BRANCH_CODE " +
                                                                                     ", SPPA_DOC_MONTH " +
                                                                                     ", SPPA_TRN_TYPE " +
                                                                                     ", SPPA_TRN_NUMBER " +
                                                                                     ", SPPA_ATTND_TYPE " +
                                                                                     ", SPPA_SL_NO " +
                                                                                     ", SPPA_ECODE " +
                                                                                     ", SPPA_NAME " +
                                                                                     ", SPPA_DESIG " +                                                                                     
                                                                                     ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                                     "','" + strBranCode[1] +
                                                                                     "','" + strBranCode[0] +
                                                                                     "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                                     "','PROD PROMOTION','" + txtTrnNo.Text.ToString() +
                                                                                     "','COMPANY_STAFF' " +
                                                                                     "," + Convert.ToInt32(gvAttendedEmpDetails.Rows[k].Cells["SlNo_Emp"].Value) +
                                                                                     "," + Convert.ToInt32(gvAttendedEmpDetails.Rows[k].Cells["Ecode"].Value) +
                                                                                     ",'" + gvAttendedEmpDetails.Rows[k].Cells["EmpName"].Value.ToString() +
                                                                                     "','" + gvAttendedEmpDetails.Rows[k].Cells["Desig"].Value.ToString() + "')";
                    }
                }

                if (strInsert.Length>10)
                {
                    iRes = objSQLdb.ExecuteSaveData(strInsert);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }

        private int SaveItemDetails()
        {
            objSQLdb = new SQLDB();
            int iRec = 0;
            string strCmd = "";

            try
            {
                string[] strBranCode = cbBranches.SelectedValue.ToString().Split('@');
                if (gvItemDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvItemDetails.Rows.Count; i++)
                    {
                        strCmd += "INSERT INTO SERVICES_PRODUCT_PROMOTION_ITEMS(SPPI_COMPANY_CODE "+
                                                                             ", SPPI_STATE_CODE "+
                                                                             ", SPPI_BRANCH_CODE "+
                                                                             ", SPPI_DOC_MONTH "+
                                                                             ", SPPI_TRN_TYPE " +
                                                                             ", SPPI_TRN_NUMBER "+
                                                                             ", SPPI_ITEM_ID "+
                                                                             ", SPPI_QTY "+
                                                                             ", SPPI_REMARKS "+
                                                                             ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                            "','" + strBranCode[1] +
                                                                            "','" + strBranCode[0] +
                                                                           "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                           "','PROD PROMOTION','" + txtTrnNo.Text.ToString() +
                                                                          "','" + gvItemDetails.Rows[i].Cells["ItemId"].Value.ToString() +
                                                                          "',"+ Convert.ToInt32(gvItemDetails.Rows[i].Cells["Qty"].Value) +
                                                                         ",'"+ gvItemDetails.Rows[i].Cells["ItemRemarks"].Value.ToString() +"' )";
                    }
                }
                if (strCmd.Length > 10)
                {
                    iRec = objSQLdb.ExecuteSaveData(strCmd);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRec;
        }
        #endregion


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
            if (cbBranches.SelectedIndex > 0)
            {
                try
                {
                    string[] strBranCode = cbBranches.SelectedValue.ToString().Split('@');
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
                cbBranches.SelectedIndex = 0;
            }
        }

        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranches.SelectedIndex > 0 && flagUpdate == false)
            {
                GenerateTransactionNo();
                EcodeSearch();
                FillCampComboBox(cbCompany.SelectedValue.ToString(),cbBranches.SelectedValue.ToString().Split('@')[0]);
                
            }
            else
            {
                flagUpdate = false;
                txtEcodeSearch.Text = "";
                cbEcode.SelectedIndex = -1;
                GenerateTransactionNo();
                txtHouseNo.Text = "";
                txtLandMark.Text = "";
                txtVillage.Text = "";
                txtMandal.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtPin.Text = "";
                dtItemDetails.Rows.Clear();
                dtAttEmpDetails.Rows.Clear();               
                gvAttendedEmpDetails.Rows.Clear();
                gvItemDetails.Rows.Clear();
               
               
            }
        }
        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtEcodeSearch.Text != "")
            {
                EcodeSearch();
            }
            else
            {
                FillEmployeeData();
            }
        }

        private void btnVillageSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VillSearch = new VillageSearch("frmProductPromotion");
            VillSearch.objfrmProductPromotion = this;
            VillSearch.Show();
        }

        #region "GRIDVIEW DETAILS"

        public void GetEmpDetails()
        {
            int intRow = 1;
            gvAttendedEmpDetails.Rows.Clear();
            try
            {

                for (int i = 0; i < dtAttEmpDetails.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    dtAttEmpDetails.Rows[i]["SlNo_Emp"] = intRow;
                    tempRow.Cells.Add(cellSLNO);


                    DataGridViewCell cellEmpEcode = new DataGridViewTextBoxCell();
                    cellEmpEcode.Value = dtAttEmpDetails.Rows[i]["Ecode"];
                    tempRow.Cells.Add(cellEmpEcode);

                    DataGridViewCell cellEmpName = new DataGridViewTextBoxCell();
                    cellEmpName.Value = dtAttEmpDetails.Rows[i]["EmpName"];
                    tempRow.Cells.Add(cellEmpName);

                    DataGridViewCell cellEmpDesig = new DataGridViewTextBoxCell();
                    cellEmpDesig.Value = dtAttEmpDetails.Rows[i]["Desig"];
                    tempRow.Cells.Add(cellEmpDesig);

                    DataGridViewCell cellEmpDept = new DataGridViewTextBoxCell();
                    cellEmpDept.Value = dtAttEmpDetails.Rows[i]["Dept"];
                    tempRow.Cells.Add(cellEmpDept);


                    intRow = intRow + 1;
                    gvAttendedEmpDetails.Rows.Add(tempRow);                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }     

        public void GetItemDetails()
        {
            int intRow = 1;
            gvItemDetails.Rows.Clear();
            try
            {

                for (int i = 0; i < dtItemDetails.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    dtItemDetails.Rows[i]["SLNo_Item"] = intRow;
                    tempRow.Cells.Add(cellSLNO);


                    DataGridViewCell cellItemId = new DataGridViewTextBoxCell();
                    cellItemId.Value = dtItemDetails.Rows[i]["ItemId"];
                    tempRow.Cells.Add(cellItemId);

                    DataGridViewCell cellItemName = new DataGridViewTextBoxCell();
                    cellItemName.Value = dtItemDetails.Rows[i]["ItemName"];
                    tempRow.Cells.Add(cellItemName);

                    DataGridViewCell cellQty = new DataGridViewTextBoxCell();
                    cellQty.Value = dtItemDetails.Rows[i]["Qty"];
                    tempRow.Cells.Add(cellQty);

                    DataGridViewCell cellItemRemarks = new DataGridViewTextBoxCell();
                    cellItemRemarks.Value = dtItemDetails.Rows[i]["ItemRemarks"];
                    tempRow.Cells.Add(cellItemRemarks);


                    intRow = intRow + 1;
                    gvItemDetails.Rows.Add(tempRow);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        #endregion




        private void btnAddEmpdetails_Click(object sender, EventArgs e)
        {
            PMAttendedEmpDetails Empdetl = new PMAttendedEmpDetails(sEcode);
            Empdetl.objfrmProductPromotion = this;
            Empdetl.Show();
        }

      

        #region "EDITING AND DELETING GRID DETAILS"      

        private void gvAttendedEmpDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                if (e.ColumnIndex == gvAttendedEmpDetails.Columns["Del_EmpDetails"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvAttendedEmpDetails.Rows[e.RowIndex].Cells[gvAttendedEmpDetails.Columns["SlNo_Emp"].Index].Value);
                        DataRow[] dr = dtAttEmpDetails.Select("SlNo_Emp=" + SlNo);
                        dtAttEmpDetails.Rows.Remove(dr[0]);
                        GetEmpDetails();
                        MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
        }

        private void gvItemDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                if (e.ColumnIndex == gvItemDetails.Columns["Edit_ItemDetl"].Index)
                {
                    if (Convert.ToBoolean(gvItemDetails.Rows[e.RowIndex].Cells["Edit_ItemDetl"].Selected) == true)
                    {

                        int SlNo = Convert.ToInt32(gvItemDetails.Rows[e.RowIndex].Cells[gvItemDetails.Columns["SLNo_Item"].Index].Value);
                        DataRow[] dr = dtItemDetails.Select("SLNo_Item=" + SlNo);

                        PMProductItemDetails ItemDetl = new PMProductItemDetails(dr);
                        ItemDetl.objfrmProductPromotion = this;
                        ItemDetl.Show();

                    }

                }

                if (e.ColumnIndex == gvItemDetails.Columns["Del_ItemDetl"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        int SlNo = Convert.ToInt32(gvItemDetails.Rows[e.RowIndex].Cells[gvItemDetails.Columns["SLNo_Item"].Index].Value);
                        DataRow[] dr = dtItemDetails.Select("SLNo_Item=" + SlNo);
                        dtItemDetails.Rows.Remove(dr[0]);
                        GetItemDetails();
                        MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
        }                  
       
        #endregion

        #region "GET DATA FOR UPDATE"

        private void GetProdPromotionDetails()
        {
            objServicedb = new ServiceDeptDB();
            DataTable dtProdPrmHead;
            Hashtable ht;
            if (txtTrnNo.Text.Length > 21)
            {
                try
                {
                    ht = objServicedb.GetProductPromotionDetails(txtTrnNo.Text.ToString());
                    dtProdPrmHead = (DataTable)ht["ProdPromHead"];

                    FillHeadDetails(dtProdPrmHead);
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
            
            DataTable dtEmpDetails;           
            DataTable dtProdItemDetl;
            

            if (txtTrnNo.Text.Length > 21)
            {
                try
                {

                    ht = objServicedb.GetProductPromotionDetails(txtTrnNo.Text.ToString());
                                       
                    dtEmpDetails = (DataTable)ht["AttendentEmpDetails"];                   
                    dtProdItemDetl = (DataTable)ht["ProdItemDetails"];                   

                    if (dtHead.Rows.Count > 0)
                    {
                        flagUpdate = true;

                        string stECode = dtHead.Rows[0]["Ecode"] + "";
                        cbCompany.SelectedValue = dtHead.Rows[0]["CompCode"].ToString(); ;
                        cbBranches.SelectedValue = dtHead.Rows[0]["BranCode"].ToString();
                       
                        dtpTrnDate.Value = Convert.ToDateTime(dtHead.Rows[0]["TrnDate"].ToString());
                        cbEcode.SelectedValue = stECode;
                        txtEcodeSearch.Text = stECode;                        
                        txtHouseNo.Text = dtHead.Rows[0]["HouseNo"].ToString();
                        txtVillage.Text = dtHead.Rows[0]["Village"].ToString();
                        txtMandal.Text = dtHead.Rows[0]["Mandal"].ToString();
                        txtDistrict.Text = dtHead.Rows[0]["District"].ToString();
                        txtState.Text = dtHead.Rows[0]["State"].ToString();
                        txtPin.Text = dtHead.Rows[0]["Pin"].ToString();
                        txtLandMark.Text = dtHead.Rows[0]["LandMark"].ToString();
                        cbCamps.Text = dtHead.Rows[0]["CampName"].ToString();
                                               
                        FillEmpDetails(dtEmpDetails);                       
                        FillProdItemDetails(dtProdItemDetl);
                       
                    }
                    else
                    {

                        flagUpdate = false;
                        GenerateTransactionNo();
                        cbEcode.SelectedIndex = -1;
                        txtEcodeSearch.Text = "";                       
                        dtpTrnDate.Value = DateTime.Today;
                        txtHouseNo.Text = "";
                        txtLandMark.Text = "";
                        txtVillage.Text = "";
                        txtMandal.Text = "";
                        txtDistrict.Text = "";
                        txtState.Text = "";
                        txtPin.Text = "";

                        dtItemDetails.Rows.Clear();
                        dtAttEmpDetails.Rows.Clear();
                       
                        gvAttendedEmpDetails.Rows.Clear();
                        gvItemDetails.Rows.Clear();                       
                    }                 
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
           

        }

      
        private void FillEmpDetails(DataTable dtEmp)
        {
            dtAttEmpDetails.Rows.Clear();

            try
            {
                if (dtEmp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEmp.Rows.Count; i++)
                    {

                        dtAttEmpDetails.Rows.Add(new Object[] {"-1", dtEmp.Rows[i]["Ecode"].ToString(),
                                                                       dtEmp.Rows[i]["EmpName"].ToString(),                                                                      
                                                                       dtEmp.Rows[i]["Desig"].ToString(),
                                                                       dtEmp.Rows[i]["DeptName"].ToString() });


                        GetEmpDetails();

                    }
                }

                dtEmp = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            
        }
      

        private void FillProdItemDetails(DataTable dtItem)
        {
            dtItemDetails.Rows.Clear();

            try
            {
                if (dtItem.Rows.Count > 0)
                {
                    for (int i = 0; i < dtItem.Rows.Count; i++)
                    {

                        dtItemDetails.Rows.Add(new Object[] {"-1", dtItem.Rows[i]["ItemId"].ToString(),
                                                                       dtItem.Rows[i]["ItemName"].ToString(),                                                                      
                                                                       dtItem.Rows[i]["Qty"].ToString(),
                                                                       dtItem.Rows[i]["Remarks"].ToString() });


                        GetItemDetails();


                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                dtItem = null;
            }

        }           



        private void txtLocationName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);            
        }

   
        #endregion


        

        private void txtHouseNo_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.KeyChar = char.ToUpper(e.KeyChar);

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

        private void txtLandMark_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }   


     
        private void txtTrnNo_Validated(object sender, EventArgs e)
        {
            if (txtTrnNo.Text.Length >19)
            {               
                GetProdPromotionDetails();
            }
            else
            {
                flagUpdate = false;
                cbEcode.SelectedIndex = -1;
                txtEcodeSearch.Text = "";               
                txtHouseNo.Text = "";
                txtLandMark.Text = "";
                txtVillage.Text = "";
                txtMandal.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtPin.Text = "";
                cbCamps.SelectedIndex = 0;
                
                dtItemDetails.Rows.Clear();
                dtAttEmpDetails.Rows.Clear();
                GenerateTransactionNo();
                gvItemDetails.Rows.Clear();
                gvAttendedEmpDetails.Rows.Clear();
               
            }

        }

        private void btnAddItemDetails_Click(object sender, EventArgs e)
        {
            PMProductItemDetails ItemDetl = new PMProductItemDetails();
            ItemDetl.objfrmProductPromotion = this;
            ItemDetl.Show();
        }
             

        private void btnCancel_Click(object sender, EventArgs e)
        {         
            //txtEcodeSearch.Text = "";
            //cbEcode.SelectedIndex = -1;
            EcodeSearch();
            if (cbCamps.Items.Count != 0)
                cbCamps.SelectedIndex = 0;
            //dtpTrnDate.Value = DateTime.Today;
            txtHouseNo.Text = "";
            txtLandMark.Text = "";
            txtVillage.Text = "";
            txtMandal.Text = "";
            txtDistrict.Text = "";
            txtState.Text = "";
            txtPin.Text = "";
            txtTrnNo.Text = "";
            GenerateTransactionNo();
            dtItemDetails.Rows.Clear();
            dtAttEmpDetails.Rows.Clear();           
            gvAttendedEmpDetails.Rows.Clear();
            gvItemDetails.Rows.Clear();           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iResult = 0;

            string strCmd = "";


            if (txtTrnNo.Text.Length > 21 && flagUpdate == true)
            {
                DialogResult result = MessageBox.Show("Do you want to delete This Record ?",
                                    "Product Promotion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {

                        strCmd = "DELETE FROM SERVICES_PRODUCT_PROMOTION_ATTENDENTS WHERE SPPA_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";

                        strCmd += "DELETE FROM SERVICES_PRODUCT_PROMOTION_ITEMS WHERE SPPI_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";              
                        
                        strCmd += "DELETE FROM SERVICES_PRODUCT_PROMOTION_HEAD WHERE SPPH_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";

                        if (strCmd.Length > 10)
                        {
                            iResult = objSQLdb.ExecuteSaveData(strCmd);
                        }

                        if (iResult > 0)
                        {
                            MessageBox.Show("Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

       
    }
}
