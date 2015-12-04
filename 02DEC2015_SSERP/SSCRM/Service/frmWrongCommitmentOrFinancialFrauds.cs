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
    public partial class frmWrongCommitmentOrFinancialFrauds : Form
    {
        SQLDB objSQLdb = null;
        ServiceDeptDB objServicedb = null;
        public frmAttendedFarmerDetails FarmerDetails;
        string strStateCode = string.Empty;
        string strFormerId = string.Empty;
        Int32 nInvNo = 0;
        bool flagUpdate = false;
        string strInvDate = string.Empty;
        string DevStatus = string.Empty;        
        public DataTable dtFarmerDetails = new DataTable();
        public DataTable dtActionTakenDetails = new DataTable();
        double dInvAmt = 0, dActualAmt = 0;
        string sEmpName = "";

        public frmWrongCommitmentOrFinancialFrauds()
        {
            InitializeComponent();
        }

        private void frmWrongCommitmentOrFinancialFrauds_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            FillDeviationType();
            cmbRefType.SelectedIndex = 0;
            cmbDevStatus.SelectedIndex = 0;       
          
            dtpTrnDate.Value = DateTime.Today;
            lblKnocking.Visible = false;

            dtpInvMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,

                                                     System.Drawing.FontStyle.Regular);
            #region "CREATE FARMERS TABLE"
            dtFarmerDetails.Columns.Add("SlNo_Farmer");
            dtFarmerDetails.Columns.Add("FarmerName");
            dtFarmerDetails.Columns.Add("HouseNo");
            dtFarmerDetails.Columns.Add("LandMark");
            dtFarmerDetails.Columns.Add("VillageName");
            dtFarmerDetails.Columns.Add("Mandal");
            dtFarmerDetails.Columns.Add("District");
            dtFarmerDetails.Columns.Add("State");
            dtFarmerDetails.Columns.Add("Pin");
            dtFarmerDetails.Columns.Add("MobileNo");
            dtFarmerDetails.Columns.Add("Remarks");
            
            #endregion

            #region "CREATE ACTION_TAKEN_EMP TABLE"
            dtActionTakenDetails.Columns.Add("TrnNo");
            dtActionTakenDetails.Columns.Add("ActionTakenEcode");
            dtActionTakenDetails.Columns.Add("ActionDate");
            dtActionTakenDetails.Columns.Add("EmpName");
            dtActionTakenDetails.Columns.Add("JvNo");
            dtActionTakenDetails.Columns.Add("FineAmt");
            dtActionTakenDetails.Columns.Add("Description");         

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
                                         "' and BRANCH_TYPE='BR' ORDER BY BRANCH_NAME ASC";

                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                }

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


        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchData();
                cbBranches.SelectedIndex = 0;
            }
           
        }
   

        private void GenerateTrnNoforWCAndFF(string strType)
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strNewNo=string.Empty;

            if (cbBranches.SelectedIndex > 0 && cmbTrnType.SelectedIndex > 0)
            {
                try
                {
                    string[] strBranCode = cbBranches.SelectedValue.ToString().Split('@');
                    string strFinYear = CommonData.FinancialYear.Substring(2, 2) + CommonData.FinancialYear.Substring(7, 2);
                    if (strType == "WC")
                    {
                        strNewNo = strBranCode[0] + strFinYear + "WC-";
                    }
                    else if (strType == "FF")
                    {
                        strNewNo = strBranCode[0] + strFinYear + "FF-";
                    }
                    
                    string strCommand = "SELECT ISNULL(MAX(SUBSTRING(ISNULL(SWFH_TRN_NUMBER, '" + strNewNo + "'),17,21)),0) + 1 " +
                                        " FROM SERVICES_WC_FF_HEAD " +
                                        "  WHERE SWFH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                        "' AND SWFH_BRANCH_CODE='" + strBranCode[0] +
                                        "' AND SWFH_TRN_TYPE='" + cmbTrnType.Tag + "' ";
                    dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                    txtTrnNo.Text = strNewNo + Convert.ToInt32(dt.Rows[0][0]).ToString("000000");
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
        #region "INVOICE DATA"
        private void FillInvoiceHeadData(Int32 nInvNo,string sOrderNo)
        {
            objSQLdb = new SQLDB();
            objServicedb = new ServiceDeptDB();
            Hashtable ht;
            DataTable dtInvH;
            DataTable dtInvD;           
            DataTable dt = new DataTable();

            try
            {
                string[] strData = cbBranches.SelectedValue.ToString().Split('@');
                ht = objServicedb.GetInvoiceData(cbCompany.SelectedValue.ToString(),strData[1],strData[0], nInvNo, sOrderNo);

                dtInvH = (DataTable)ht["InvHead"];
                dtInvD = (DataTable)ht["InvDetail"];
                if (dtInvH.Rows.Count == 0)
                {
                   
                    ht = objServicedb.InvoiceBulitin_Get(cbCompany.SelectedValue.ToString(),strData[1],strData[0], nInvNo, sOrderNo);
                    dtInvH = (DataTable)ht["InvHead"];
                    dtInvD = (DataTable)ht["InvDetail"];
                    
                }
                if (dtInvH.Rows.Count > 0)
                {

                    strFormerId = Convert.ToString(dtInvH.Rows[0]["farmer_ID"] + "");
                    string strECode = dtInvH.Rows[0]["eora_code"] + "";
                    txtSREcode.Text = strECode;
                    string strCmd = "SELECT MEMBER_NAME+'('+DESIG+')' EName FROM EORA_MASTER WHERE ECODE=" + strECode + "";
                    dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtSrEname.Text = dt.Rows[0]["EName"].ToString();
                    }


                    txtHouseNo.Text = Convert.ToString(dtInvH.Rows[0]["cm_house_no"]);
                    txtLandMark.Text = Convert.ToString(dtInvH.Rows[0]["cm_landmark"]);
                    txtVillage.Text = dtInvH.Rows[0]["cm_village"] + "";
                    txtMandal.Text = dtInvH.Rows[0]["cm_mandal"] + "";
                    txtDistrict.Text = dtInvH.Rows[0]["cm_district"] + "";
                    txtState.Text = dtInvH.Rows[0]["CM_STATE"] + "";
                    strStateCode = dtInvH.Rows[0]["state_code"] + "";
                    txtPin.Text = dtInvH.Rows[0]["cm_pin"] + "";
                    txtCustomerName.Text = dtInvH.Rows[0]["cm_farmer_name"] + "";
                    txtMobileNo.Text = dtInvH.Rows[0]["cm_mobile_number"] + "";                    
                    txtOrderNo.Text = dtInvH.Rows[0]["order_number"] + "";
                    txtInvoiceNo.Text = dtInvH.Rows[0]["invoice_number"] + "";
                    dtpInvoiceDate.Value = Convert.ToDateTime(dtInvH.Rows[0]["invoice_date"]);
                    dtpInvMonth.Value = Convert.ToDateTime(dtInvH.Rows[0]["document_month"] + "");
                    
                    txtInvAmt.Text = dtInvH.Rows[0]["INVOICE_AMOUNT"] + "";                    
                    txtGLEcode.Text = dtInvH.Rows[0]["GroupEcode"].ToString();
                    txtGLName.Text = dtInvH.Rows[0]["GLName"].ToString();

                    FillInvoiceDetail(dtInvD);
                }
                else
                {
                    txtVillage.Text = "";
                    txtCustomerName.Text = "";
                    txtState.Text = "";
                    txtGLName.Text = "";
                    txtGLEcode.Text = "";
                   
                    txtInvAmt.Text = "";
                    txtActualAmt.Text = "";
                                                           
                    txtOrderNo.Text = "";
                    txtMobileNo.Text = "";
                    txtMandal.Text = "";
                    txtDistrict.Text = "";
                    txtSREcode.Text = "";
                    txtSrEname.Text = "";
                    gvProductDetails.Rows.Clear();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                objServicedb = null;
                dt = null;
                dtInvH = null;
                dtInvD = null;
            }
        }

        private void FillInvoiceDetail(DataTable dt)
        {
            try
            {
                int intRow = 1;
                gvProductDetails.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                     DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        if (dt.Rows[i]["invoice_sl_no"] + "" != "0")
                            cellSLNO.Value = (i+1).ToString();
                        else
                            cellSLNO.Value = intRow;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                        cellMainProductID.Value = dt.Rows[i]["product_id"];
                        tempRow.Cells.Add(cellMainProductID);

                        DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                        cellMainProduct.Value = dt.Rows[i]["pm_product_name"];
                        tempRow.Cells.Add(cellMainProduct);

                        DataGridViewCell cellSubProduct = new DataGridViewTextBoxCell();
                        cellSubProduct.Value = dt.Rows[i]["category_name"];
                        tempRow.Cells.Add(cellSubProduct);

                      

                        DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                        if (Convert.ToDouble(dt.Rows[i]["qty"]) > 0)
                            cellQTY.Value = Convert.ToDouble(dt.Rows[i]["qty"]).ToString("f");
                        else
                            cellQTY.Value = "";

                        tempRow.Cells.Add(cellQTY);

                  
                        intRow = intRow + 1;
                        gvProductDetails.Rows.Add(tempRow);
                    }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dt = null;
            }
        }
        #endregion

        private void txtInvoiceNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                if (txtInvoiceNo.Text != "")
                {
                    FillInvoiceHeadData(Convert.ToInt32(txtInvoiceNo.Text), "");
                    
                }
                else
                {

                    txtVillage.Text = "";
                    txtCustomerName.Text = "";
                    txtState.Text = "";
                    txtPin.Text = "";
                    txtLandMark.Text = "";
                    cmbRefType.SelectedIndex = 0;
                    
                    txtOrderNo.Text = "";
                    txtMobileNo.Text = "";
                    txtMandal.Text = "";
                    txtDistrict.Text = "";
                    txtSREcode.Text = "";
                    txtSrEname.Text = "";
                    txtHouseNo.Text = "";
                   gvSplitCustomerDetails.Rows.Clear();
                   gvProductDetails.Rows.Clear();
                }
            }
        }

        private void txtOrderNo_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void FillDeviationType()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cmbDevType.DataSource = null;
            try
            {
                string strCmd = "SELECT HRMC_MISCONDUCT_CODE,HRMC_MISCONDUCT_HEAD FROM HR_MISCONDUCT_MAS";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = "0";
                    row[1] = "--Select--";

                    dt.Rows.InsertAt(row, 0);

                    cmbDevType.DataSource = dt;
                    cmbDevType.DisplayMember = "HRMC_MISCONDUCT_HEAD";
                    cmbDevType.ValueMember = "HRMC_MISCONDUCT_HEAD";

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
       

        private bool CheckData()
        {
            bool bFlag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Company", "Wrong Commitment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbCompany.Focus();
            }
            else if (cbBranches.SelectedIndex == -1 || cbBranches.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Branch", "Wrong Commitment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbBranches.Focus();
            }
           
            else if (cmbRefType.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Reference Type", "Wrong Commitment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbRefType.Focus();
            }
           
            else if (txtAoName.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter AO Ecode", "Wrong Commitment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAoEcode.Focus();
            }
            else if (cmbRefType.SelectedIndex == 1 || cmbRefType.SelectedIndex == 2)
            {
                if (txtInvoiceNo.Text.Length == 0 || txtOrderNo.Text.Length == 0)
                {
                    bFlag = false;
                    MessageBox.Show("Please Enter Invoice Number or Order No", "Wrong Commitment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtInvoiceNo.Focus();
                }
            }
            else if (txtCustomerName.Text.Length < 3)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Customer Name", "Wrong Commitment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCustomerName.Focus();
            }
            else if (txtVillage.Text.Length ==0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Village", "Wrong Commitment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnVillageSearch.Focus();
            }
            else if (txtSrEname.Text.Length == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Enter SR Ecode", "Wrong Commitment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSREcode.Focus();
            }
            else if (cmbDevType.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Deviation Type", "Wrong Commitment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbDevType.Focus();
            }
            else if (cmbDevStatus.SelectedIndex == 0)
            {
                bFlag = false;
                MessageBox.Show("Please Select Deviation Status", "Wrong Commitment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbDevStatus.Focus();
            }
            else if (txtDeviationDetails.Text.Length == 0 || txtDeviationDetails.Text.Length < 15)
            {
                bFlag = false;
                MessageBox.Show("Please Enter Deviation Particulars", "Wrong Commitment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDeviationDetails.Focus();
            }

            //else if (flagUpdate == true)
            //{
            //    if (DevStatus == "CLOSED")
            //    {
            //        bFlag = false;
            //        MessageBox.Show("This Data Can Not Be Manuplated", "Wrong Commitment", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return bFlag;
            //    }
            //}
            
            return bFlag;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            if (CheckData() == true)
            {
                try
                {
                    
                    if (txtActualAmt.Text.Length == 0)
                        txtActualAmt.Text = "0";

                    string[] strData = cbBranches.SelectedValue.ToString().Split('@');

                    strCommand = "DELETE FROM SERVICES_WC_FF_SPLIT_CUSTOMER_DETL WHERE SWFD_TRN_NUMBER='"+ txtTrnNo.Text.ToString() +"'";
                    strCommand += " DELETE FROM SERVICES_WC_FF_PRODUCTS WHERE SWFP_TRN_NUMBER='"+ txtTrnNo.Text.ToString() +"'";

                    if (flagUpdate == true)
                    {
                        strCommand += " UPDATE SERVICES_WC_FF_HEAD SET SWFH_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() +
                                    "',SWFH_STATE_CODE ='" + strData[1] + "', SWFH_BRANCH_CODE ='" + strData[0] +
                                     "', SWFH_DOC_MONTH='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                     "', SWFH_TRN_TYPE ='" + cmbTrnType.Tag.ToString() +
                                     "', SWFH_TRN_DATE='" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                     "',SWFH_AO_ECODE=" + Convert.ToInt32(txtAoEcode.Text) +
                                     ", SWFH_REF_TYPE ='" + cmbRefType.SelectedItem.ToString() +
                                     "',SWFH_SIBH_COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() +
                                     "',SWFH_SIBH_BRANCH_CODE ='" + strData[0] + "',SWFH_SIBH_STATE_CODE ='" + strData[1] +
                                     "',SWFH_SIBH_FIN_YEAR ='" + CommonData.FinancialYear +
                                     "', SWFH_INVOICE_NUMBER =" + Convert.ToInt32(txtInvoiceNo.Text) +
                                     ",SWFH_SIBH_DOCUMENT_MONTH='" + Convert.ToDateTime(dtpInvMonth.Value).ToString("MMMyyyy").ToUpper() +
                                     "',SWFH_ORDER_NUMBER='" + txtOrderNo.Text +
                                     "',SWFH_SR_ECODE =" + Convert.ToInt32(txtSREcode.Text) +
                                     ",SWFH_DEVIATION_TYPE='" + cmbDevType.SelectedValue.ToString() +
                                     "',SWFH_DEVIATION_DETAILS ='" + txtDeviationDetails.Text.ToString().Replace("\'", "") +
                                     "',SWFH_DEVIATION_STATUS ='" + cmbDevStatus.SelectedItem.ToString() +
                                     "',SWFH_HOUSE_NO ='"+ txtHouseNo.Text.ToString().Replace("'"," ") +
                                     "',SWFH_LANDMARK ='" + txtLandMark.Text.ToString().Replace("'"," ") +
                                     "',SWFH_VILLAGE ='" + txtVillage.Text + "',SWFH_MANDAL ='" + txtMandal.Text +
                                     "',SWFH_DISTRICT='" + txtDistrict.Text + "',SWFH_STATE ='" + txtState.Text +
                                     "',SWFH_PIN ='"+ txtPin.Text.ToString() +"',SWFH_NAME ='" + txtCustomerName.Text.ToString().Replace("'"," ") + 
                                     "',SWFH_MOBILE_NUMBER ='" + txtMobileNo.Text + "',SWFH_FARMER_ID ='" + strFormerId +
                                     "',SWFH_AUTHORIZED_BY ='',SWFH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                     "',SWFH_LAST_MODIFIED_DATE=getdate() " +
                                     ", SWFH_ACTUAL_AMT=" + Convert.ToDouble(txtActualAmt.Text) +
                                     ", SWFH_JV_NO='" + dtActionTakenDetails.Rows[0]["JvNo"].ToString() +
                                     "', SWFH_FINE_AMT=" + Convert.ToDouble(dtActionTakenDetails.Rows[0]["FineAmt"]) +
                                     ",SWFH_ACTION_TAKEN_BY=" + Convert.ToInt32(dtActionTakenDetails.Rows[0]["ActionTakenEcode"]) +
                                     ",SWFH_DESC='" + dtActionTakenDetails.Rows[0]["Description"].ToString() +
                                     "',SWFH_ACTION_DATE='" + Convert.ToDateTime(dtActionTakenDetails.Rows[0]["ActionDate"]).ToString("dd/MMM/yyyy") +
                                     "' WHERE SWFH_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                       
                        flagUpdate = false;
                    }
                    else
                    {
                        GenerateTrnNoforWCAndFF(cmbTrnType.Tag.ToString());
                        objSQLdb = new SQLDB();

                        strCommand = "INSERT INTO SERVICES_WC_FF_HEAD(SWFH_COMPANY_CODE " +
                                                                   ", SWFH_STATE_CODE " +
                                                                   ", SWFH_BRANCH_CODE " +
                                                                   ", SWFH_DOC_MONTH " +
                                                                   ", SWFH_TRN_TYPE " +
                                                                   ", SWFH_TRN_NUMBER " +
                                                                   ", SWFH_TRN_DATE " +
                                                                   ", SWFH_AO_ECODE " +
                                                                   ", SWFH_REF_TYPE " +
                                                                   ", SWFH_SIBH_COMPANY_CODE " +
                                                                   ", SWFH_SIBH_BRANCH_CODE " +
                                                                   ", SWFH_SIBH_STATE_CODE " +
                                                                   ", SWFH_SIBH_FIN_YEAR " +
                                                                   ", SWFH_INVOICE_NUMBER " +
                                                                   ", SWFH_SIBH_DOCUMENT_MONTH " +
                                                                   ", SWFH_ORDER_NUMBER " +
                                                                   ", SWFH_SR_ECODE " +
                                                                   ", SWFH_DEVIATION_TYPE " +
                                                                   ", SWFH_DEVIATION_DETAILS " +
                                                                   ", SWFH_DEVIATION_STATUS " +
                                                                   ", SWFH_HOUSE_NO " +
                                                                   ", SWFH_LANDMARK " +
                                                                   ", SWFH_VILLAGE " +
                                                                   ", SWFH_MANDAL " +
                                                                   ", SWFH_DISTRICT " +
                                                                   ", SWFH_STATE " +
                                                                   ", SWFH_PIN " +
                                                                   ", SWFH_NAME " +                                                                   
                                                                   ", SWFH_MOBILE_NUMBER " +
                                                                   ", SWFH_FARMER_ID " +
                                                                   ", SWFH_AUTHORIZED_BY " +
                                                                   ", SWFH_CREATED_BY " +
                                                                   ", SWFH_CREATED_DATE " +
                                                                   ", SWFH_ACTUAL_AMT "+
                                                                   ", SWFH_JV_NO "+
                                                                   ", SWFH_FINE_AMT "+
                                                                   ", SWFH_ACTION_TAKEN_BY "+
                                                                   ", SWFH_DESC "+
                                                                   ",SWFH_ACTION_DATE "+
                                                                   ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                   "','" + strData[1] +
                                                                   "','" + strData[0] +
                                                                   "','"+ Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                   "','" + cmbTrnType.Tag.ToString() +
                                                                   "','" + txtTrnNo.Text.ToString() +
                                                                   "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("dd/MMM/yyyy") +
                                                                   "'," + Convert.ToInt32(txtAoEcode.Text) +
                                                                   ",'" + cmbRefType.SelectedItem.ToString() +
                                                                   "','" + cbCompany.SelectedValue.ToString() +
                                                                   "','" + strData[0] +
                                                                   "','" + strData[1] +
                                                                   "','" + CommonData.FinancialYear +
                                                                   "'," + Convert.ToInt32(txtInvoiceNo.Text) +
                                                                   ",'"+ Convert.ToDateTime(dtpInvMonth.Value).ToString("MMMyyyy").ToUpper() +
                                                                   "','" + txtOrderNo.Text +
                                                                   "',"+ Convert.ToInt32(txtSREcode.Text) +
                                                                   ",'" + cmbDevType.SelectedValue.ToString() +
                                                                   "','" + txtDeviationDetails.Text.ToString().Replace("\'","") +
                                                                   "','" + cmbDevStatus.SelectedItem.ToString() +
                                                                   "','" + txtHouseNo.Text.ToString() +
                                                                   "','" + txtLandMark.Text.ToString().Replace("'"," ") +
                                                                   "','" + txtVillage.Text +
                                                                   "','" + txtMandal.Text +
                                                                   "','" + txtDistrict.Text +
                                                                   "','" + txtState.Text +
                                                                   "','"+ txtPin.Text +
                                                                   "','"+ txtCustomerName.Text.ToString().Replace("'"," ") +                                                                   
                                                                   "','" + txtMobileNo.Text +
                                                                   "','" + strFormerId +
                                                                   "','','" + CommonData.LogUserId +
                                                                   "',getdate()," + Convert.ToDouble(txtActualAmt.Text) +
                                                                   ",'" + dtActionTakenDetails.Rows[0]["JvNo"].ToString() +
                                                                   "'," + Convert.ToDouble(dtActionTakenDetails.Rows[0]["FineAmt"]) +
                                                                   ","+ Convert.ToInt32(dtActionTakenDetails.Rows[0]["ActionTakenEcode"]) +
                                                                   ",'"+ dtActionTakenDetails.Rows[0]["Description"].ToString() +
                                                                   "','"+ Convert.ToDateTime(dtActionTakenDetails.Rows[0]["ActionDate"]).ToString("dd/MMM/yyyy") +"')";

                      
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
                if (iRes > 0)
                {
                    SaveProductDetails();
                    SaveSplitCustomerDetails();
                    MessageBox.Show("Data Saved Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flagUpdate = false;
                    cmbRefType.Focus();
                    btnCancel_Click(null, null);
                    
                }
                else
                {
                    MessageBox.Show("Data Not Saved", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int SaveProductDetails()
        {
            objSQLdb = new SQLDB();
            string strCmd = "";
            int iRes = 0;

            try
            {
                if (gvProductDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                    {
                        if (Convert.ToString(gvProductDetails.Rows[i].Cells["Qty"].Value) == "")
                        {
                            gvProductDetails.Rows[i].Cells["Qty"].Value = "0";
                        }
                        strCmd += "INSERT INTO SERVICES_WC_FF_PRODUCTS(SWFP_COMPANY_CODE " +
                                                                   ", SWFP_STATE_CODE " +
                                                                   ", SWFP_BRANCH_CODE " +
                                                                   ", SWFP_TRN_TYPE " +
                                                                   ", SWFP_TRN_NUMBER " +
                                                                   ", SWFP_SL_NO " +
                                                                   ", SWFP_PRODUCT_ID " +
                                                                   ", SWFP_QTY " +
                                                                   ", SWFP_CREATED_BY "+
                                                                   ", SWFP_CREATED_DATE "+
                                                                   ")VALUES('" + cbCompany.SelectedValue.ToString() +
                                                                   "','" + cbBranches.SelectedValue.ToString().Split('@')[1] +
                                                                   "','" + cbBranches.SelectedValue.ToString().Split('@')[0] +
                                                                   "','" + cmbTrnType.Tag.ToString() +
                                                                   "','" + txtTrnNo.Text.ToString() +
                                                                   "',"+ Convert.ToInt32(gvProductDetails.Rows[i].Cells["SLNO"].Value) +
                                                                   ",'"+ gvProductDetails.Rows[i].Cells["ProductID"].Value.ToString() +
                                                                   "',"+ Convert.ToDouble(gvProductDetails.Rows[i].Cells["Qty"].Value) +
                                                                   ",'"+ CommonData.LogUserId +
                                                                   "',getdate())";
                    }
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

        private int SaveSplitCustomerDetails()
        {
            objSQLdb = new SQLDB();
            string strcmd = "";
            int iRes = 0;
            try
            {
                string[] strData = cbBranches.SelectedValue.ToString().Split('@');

                if (gvSplitCustomerDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < gvSplitCustomerDetails.Rows.Count; i++)
                    {
                        strcmd += "INSERT INTO SERVICES_WC_FF_SPLIT_CUSTOMER_DETL(SWFD_COMPANY_CODE " +
                                                                              ", SWFD_STATE_CODE " +
                                                                              ", SWFD_BRANCH_CODE " +
                                                                              ", SWFD_DOC_MONTH " +
                                                                              ", SWFD_TRN_TYPE " +
                                                                              ", SWFD_TRN_NUMBER " +
                                                                              ", SWFD_SL_NO "+
                                                                              ", SWFD_HOUSE_NO " +
                                                                              ", SWFD_LANDMARK " +
                                                                              ", SWFD_VILLAGE " +
                                                                              ", SWFD_MANDAL " +
                                                                              ", SWFD_DISTRICT " +
                                                                              ", SWFD_STATE " +
                                                                              ", SWFD_PIN " +                                                                             
                                                                              ", SWFD_MOBILE_NUMBER " +
                                                                              ", SWFD_FARMER_NAME " +
                                                                              ", SWFD_CREATED_BY " +
                                                                              ", SWFD_CREATED_DATE " +
                                                                              ", SWFD_REMARKS "+
                                                                              ")VALUES " +
                                                                              "('" + cbCompany.SelectedValue.ToString() +
                                                                                "','" + strData[1] +
                                                                                "','" + strData[0] +
                                                                                "','" + Convert.ToDateTime(dtpTrnDate.Value).ToString("MMMyyyy").ToUpper() +
                                                                                "','" + cmbTrnType.Tag.ToString() +
                                                                                "','" + txtTrnNo.Text.ToString() +
                                                                                "'," + Convert.ToInt32(gvSplitCustomerDetails.Rows[i].Cells["SlNo_Farmer"].Value) + 
                                                                                ",'"+ gvSplitCustomerDetails.Rows[i].Cells["HouseNo"].Value.ToString() +
                                                                                "','"+ gvSplitCustomerDetails.Rows[i].Cells["LandMark"].Value.ToString() +
                                                                                "','"+ gvSplitCustomerDetails.Rows[i].Cells["VillageName"].Value.ToString() +
                                                                                "','" + gvSplitCustomerDetails.Rows[i].Cells["Mandal"].Value.ToString() + 
                                                                                "','"+ gvSplitCustomerDetails.Rows[i].Cells["District"].Value.ToString() +
                                                                                "','"+ gvSplitCustomerDetails.Rows[i].Cells["State"].Value.ToString() +
                                                                                "','"+ gvSplitCustomerDetails.Rows[i].Cells["Pin"].Value.ToString() +
                                                                                "','"+ gvSplitCustomerDetails.Rows[i].Cells["MobileNo"].Value.ToString() +
                                                                                "','" + gvSplitCustomerDetails.Rows[i].Cells["FarmerName"].Value.ToString() +
                                                                                "','"+ CommonData.LogUserId +"',getdate() "+
                                                                                ",'"+ gvSplitCustomerDetails.Rows[i].Cells["CropRemarks"].Value.ToString() +"')";
                    }
                }

                if (strcmd.Length > 5)
                {
                    iRes = objSQLdb.ExecuteSaveData(strcmd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes;
        }
               
        private void cbBranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranches.SelectedIndex > 0)
            {
                cmbTrnType.SelectedIndex = 0;
                if (flagUpdate == false)
                {
                    txtTrnNo.Text = "";
                }
            }
        }

        private void cmbTrnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranches.SelectedIndex >0)
            {

                if (cmbTrnType.SelectedIndex == 1)
                {
                    cmbTrnType.Tag = "WC";
                    GenerateTrnNoforWCAndFF(cmbTrnType.Tag.ToString());

                }
                else if (cmbTrnType.SelectedIndex == 2)
                {
                    cmbTrnType.Tag = "FF";                  
                    GenerateTrnNoforWCAndFF(cmbTrnType.Tag.ToString());                   
                }
            }
           
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }      

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //cbCompany.SelectedIndex = 0;
            //cbBranches.SelectedIndex = -1;
            
            cmbRefType.SelectedIndex = 0;
            cmbDevType.SelectedIndex = 0;
            cmbDevStatus.SelectedIndex = 0;
            txtSREcode.Text = "";
            
            txtSrEname.Text = "";
            txtVillage.Text = "";
            txtCustomerName.Text = "";
            txtState.Text = "";
            lblKnocking.Visible = false;
            cmbTrnType.SelectedIndex = 1;
            txtDeviationDetails.Text = "";
            gvSplitCustomerDetails.Rows.Clear();
            txtGLEcode.Text = "";
            txtGLName.Text = "";
            dtpInvoiceDate.Value = DateTime.Today;
            txtInvAmt.Text = "";
            txtActualAmt.Text = "";
          
            txtInvoiceNo.Text = "";
            txtOrderNo.Text = "";
            txtMobileNo.Text = "";
            txtMandal.Text = "";
            txtDistrict.Text = "";
            txtAoEcode.Text = "";
            txtAoName.Text = "";
            dtActionTakenDetails.Rows.Clear();
            dtpTrnDate.Value = DateTime.Today;
            gvProductDetails.Rows.Clear();
            txtLandMark.Text = "";
            txtHouseNo.Text = "";
            txtDiffAmt.Text = "";
            if (cbBranches.SelectedIndex > 0 && cmbTrnType.SelectedIndex > 0)
                GenerateTrnNoforWCAndFF(cmbTrnType.Tag.ToString());
        }              

        private void FillServiceWCorFFData(string sTrnNo)
        {
            objServicedb = new ServiceDeptDB();
            DataTable dt = new DataTable();
            DataTable dtFarmer = new DataTable();
            dtActionTakenDetails.Rows.Clear();

            try
            {
                dt = objServicedb.GetServiceWCorFFDetails(sTrnNo).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    flagUpdate = true;
                    if ((DateTime.Now - Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["CreatedDate"]).ToString("dd/MM/yyyy"))).TotalDays > 10 && dt.Rows[0]["DevStatus"].ToString()=="CLOSED")
                    {
                        if (CommonData.LogUserId.ToUpper() == "ADMIN")
                        {
                            btnSave.Enabled = true;
                            btnActionTaken.Enabled = true;
                            btnCancel.Enabled = true;
                            lblKnocking.Visible = false;
                        }
                        else
                        {
                            lblKnocking.Visible = true;
                            lblKnocking.Text = "Data Can Not Be Modified - Once Status is Closed";
                            btnSave.Enabled = false;
                            btnActionTaken.Enabled = false;
                            btnDelete.Enabled = false;
                        }
                    }
                    cbCompany.SelectedValue = dt.Rows[0]["CompCode"].ToString();
                    cbBranches.SelectedValue = dt.Rows[0]["BranCode"].ToString();
                    
                    dtpTrnDate.Value = Convert.ToDateTime(dt.Rows[0]["TrnDate"].ToString());
                    string strEcode = dt.Rows[0]["Ecode"] + "";
                    txtAoEcode.Text = strEcode;
                    dtpInvMonth.Value = Convert.ToDateTime(dt.Rows[0]["InvDocMon"].ToString());
                    txtAoName.Text = dt.Rows[0]["EmpName"].ToString();
                    txtOrderNo.Text = dt.Rows[0]["OrderNo"].ToString();
                    txtInvoiceNo.Text = dt.Rows[0]["InvNo"].ToString();

                    if (dt.Rows[0]["TrnType"].ToString() == "WC")
                    {
                        cmbTrnType.SelectedIndex = 1;
                    }
                    if (dt.Rows[0]["TrnType"].ToString() == "FF")
                    {
                        cmbTrnType.SelectedIndex = 2;
                    }
                  
                    txtHouseNo.Text = Convert.ToString(dt.Rows[0]["HouseNo"]);
                    txtLandMark.Text = Convert.ToString(dt.Rows[0]["LandMark"]);
                    txtVillage.Text = dt.Rows[0]["Village"] + "";
                    txtMandal.Text = dt.Rows[0]["Mandal"] + "";
                    txtDistrict.Text = dt.Rows[0]["District"] + "";
                    txtState.Text = dt.Rows[0]["State"] + "";
                    txtInvAmt.Text = dt.Rows[0]["InvAmt"].ToString();
                   
                    txtActualAmt.Text = dt.Rows[0]["ActualAmt"].ToString();               
                                                            
                    txtGLEcode.Text = dt.Rows[0]["GroupEcode"].ToString();
                    txtGLName.Text = dt.Rows[0]["GLName"].ToString();
                    CalculateDiffAmt();
                   
                    txtPin.Text = dt.Rows[0]["Pin"] + "";
                    txtCustomerName.Text = dt.Rows[0]["Name"] + "";
                    txtMobileNo.Text = dt.Rows[0]["MobileNo"] + "";
                    strFormerId = dt.Rows[0]["FarmerId"] + "";

                    dtpInvoiceDate.Value = Convert.ToDateTime(dt.Rows[0]["InvDate"].ToString());

                    DevStatus = dt.Rows[0]["DevStatus"].ToString();
                    cmbDevStatus.SelectedItem = DevStatus;
                    cmbRefType.SelectedItem = dt.Rows[0]["RefType"].ToString();
                    txtDeviationDetails.Text = dt.Rows[0]["DevDetails"].ToString();
                    cmbDevType.SelectedValue = dt.Rows[0]["DevType"].ToString();
                    txtSREcode.Text = dt.Rows[0]["SREcode"].ToString();
                    txtSrEname.Text = dt.Rows[0]["SRName"].ToString();
                 
                    dtActionTakenDetails.Rows.Add(new Object[] { "-1",dt.Rows[0]["ActionTakenEcode"].ToString(),
                   dt.Rows[0]["ActionDate"].ToString(), dt.Rows[0]["ActionTakenName"].ToString(),dt.Rows[0]["JvNo"].ToString()
                  ,dt.Rows[0]["FineAmt"].ToString(),dt.Rows[0]["Description"].ToString()});

                    gvProductDetails.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        gvProductDetails.Rows.Add();
                        gvProductDetails.Rows[i].Cells["SLNO"].Value = (i + 1).ToString();
                        gvProductDetails.Rows[i].Cells["ProductID"].Value = dt.Rows[i]["ProdId"].ToString();
                        gvProductDetails.Rows[i].Cells["MainProduct"].Value = dt.Rows[i]["prodName"].ToString();
                        gvProductDetails.Rows[i].Cells["Brand"].Value = dt.Rows[i]["categoryName"].ToString();
                        gvProductDetails.Rows[i].Cells["Qty"].Value = dt.Rows[i]["Qty"].ToString();
                    }

                    objSQLdb = new SQLDB();
                    string strCmd = "exec Get_SplitCustomerDetails_WcFF '"+ txtTrnNo.Text.ToString() +"'";
                    dtFarmer = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                    FillFarmerDetails(dtFarmer);
                }

                else
                {

                    GenerateTrnNoforWCAndFF(cmbTrnType.Tag.ToString());
                    cmbRefType.SelectedIndex = 0;
                    cmbDevType.SelectedIndex = 0;
                    cmbDevStatus.SelectedIndex = 0;
                    txtSREcode.Text = "";
                   
                    txtSrEname.Text = "";
                    txtVillage.Text = "";
                    txtCustomerName.Text = "";
                    txtState.Text = "";
                    txtPin.Text = "";                    
                    txtLandMark.Text = "";
                    txtDeviationDetails.Text = "";
                    dtActionTakenDetails.Rows.Clear();
                    txtInvoiceNo.Text = "";
                    txtOrderNo.Text = "";
                    txtMobileNo.Text = "";
                    txtMandal.Text = "";
                    txtDistrict.Text = "";
                    txtAoEcode.Text = "";
                    txtAoName.Text = "";
                    txtHouseNo.Text = "";
                    dtpTrnDate.Value = DateTime.Today;
                    dtFarmerDetails.Rows.Clear();
                    gvProductDetails.Rows.Clear();
                   
                    txtInvAmt.Text = "";
                    txtActualAmt.Text = "";
                    txtGLName.Text = "";
                    txtGLEcode.Text = "";
                    txtDiffAmt.Text = "";
                    btnSave.Enabled = true;
                    btnActionTaken.Enabled = true;
                    btnCancel.Enabled = true;
                    lblKnocking.Visible = false;
                   
                    dtFarmerDetails.Rows.Clear();
                    gvSplitCustomerDetails.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objServicedb = null;
                objSQLdb = null;
                dt = null;
            }
        }

        private void FillFarmerDetails(DataTable dtFarmer)
        {
            dtFarmerDetails.Rows.Clear();
            if (txtTrnNo.Text.Length > 0)
            {
                try
                {
                    if (dtFarmer.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtFarmer.Rows.Count; i++)
                        {

                            dtFarmerDetails.Rows.Add(new Object[] {"-1", dtFarmer.Rows[i]["FarmerName"].ToString(),
                                                                       dtFarmer.Rows[i]["HouseNo"].ToString(),
                                                                       dtFarmer.Rows[i]["LandMark"].ToString(),
                                                                       dtFarmer.Rows[i]["Village"].ToString(),
                                                                       dtFarmer.Rows[i]["Mandal"].ToString(),
                                                                       dtFarmer.Rows[i]["District"].ToString(),
                                                                       dtFarmer.Rows[i]["State"].ToString(),                                                                      
                                                                       dtFarmer.Rows[i]["Pin"].ToString(),
                                                                       dtFarmer.Rows[i]["MobileNo"].ToString(),
                                                                       dtFarmer.Rows[i]["Remarks"].ToString()});
                            GetFarmerDetails();


                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }
        public void GetFarmerDetails()
        {
            int intRow = 1;
            gvSplitCustomerDetails.Rows.Clear();
            try
            {

                for (int i = 0; i < dtFarmerDetails.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    dtFarmerDetails.Rows[i]["SlNo_Farmer"] = intRow;
                    tempRow.Cells.Add(cellSLNO);


                    DataGridViewCell cellFarmerName = new DataGridViewTextBoxCell();
                    cellFarmerName.Value = dtFarmerDetails.Rows[i]["FarmerName"];
                    tempRow.Cells.Add(cellFarmerName);

                    DataGridViewCell cellHouseNo = new DataGridViewTextBoxCell();
                    cellHouseNo.Value = dtFarmerDetails.Rows[i]["HouseNo"];
                    tempRow.Cells.Add(cellHouseNo);

                    DataGridViewCell cellLandMark = new DataGridViewTextBoxCell();
                    cellLandMark.Value = dtFarmerDetails.Rows[i]["LandMark"];
                    tempRow.Cells.Add(cellLandMark);

                    DataGridViewCell cellVillage = new DataGridViewTextBoxCell();
                    cellVillage.Value = dtFarmerDetails.Rows[i]["VillageName"];
                    tempRow.Cells.Add(cellVillage);

                    DataGridViewCell cellMandal = new DataGridViewTextBoxCell();
                    cellMandal.Value = dtFarmerDetails.Rows[i]["Mandal"];
                    tempRow.Cells.Add(cellMandal);

                    DataGridViewCell cellDistrict = new DataGridViewTextBoxCell();
                    cellDistrict.Value = dtFarmerDetails.Rows[i]["District"];
                    tempRow.Cells.Add(cellDistrict);

                    DataGridViewCell cellState = new DataGridViewTextBoxCell();
                    cellState.Value = dtFarmerDetails.Rows[i]["State"];
                    tempRow.Cells.Add(cellState);

                    DataGridViewCell cellPin = new DataGridViewTextBoxCell();
                    cellPin.Value = dtFarmerDetails.Rows[i]["Pin"];
                    tempRow.Cells.Add(cellPin);

                    DataGridViewCell cellMobileNo = new DataGridViewTextBoxCell();
                    cellMobileNo.Value = dtFarmerDetails.Rows[i]["MobileNo"];
                    tempRow.Cells.Add(cellMobileNo);

                    DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                    cellRemarks.Value = dtFarmerDetails.Rows[i]["Remarks"];
                    tempRow.Cells.Add(cellRemarks);

                    intRow = intRow + 1;
                    gvSplitCustomerDetails.Rows.Add(tempRow);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CheckDeleteCondition()
        {
            bool flag = true;
            if (txtTrnNo.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Transaction Number", "Wrong Commitment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTrnNo.Focus();
            }
            return flag;
        }

         private void btnDelete_Click(object sender, EventArgs e)
         {
             int iRes = 0;
             objSQLdb = new SQLDB();
             string strDelete = "";

             if (CheckDeleteCondition() == true && flagUpdate==true)
             {
                 DialogResult result = MessageBox.Show("Do you want to delete This Record ?",
                                         "Wrong Commitment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (result == DialogResult.Yes)
                 {

                     strDelete = "DELETE FROM SERVICES_WC_FF_PRODUCTS WHERE SWFP_TRN_NUMBER='"+ txtTrnNo.Text.ToString() +"'";
                     strDelete += " DELETE FROM SERVICES_WC_FF_SPLIT_CUSTOMER_DETL WHERE SWFD_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";
                     strDelete += " DELETE FROM SERVICES_WC_FF_HEAD WHERE SWFH_TRN_NUMBER='" + txtTrnNo.Text.ToString() + "'";

                     if (strDelete.Length > 10)
                     {
                         iRes = objSQLdb.ExecuteSaveData(strDelete);
                     }

                     MessageBox.Show("Data Deleted Sucessfully", "Wrong Commitment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     btnCancel_Click(null, null);

                 }
                 else
                 {
                     MessageBox.Show(" Data Not Deleted", "Wrong Commitment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }

             }
         }

         private void txtAoEcode_TextChanged(object sender, EventArgs e)
         {
             objSQLdb = new SQLDB();
             DataTable dt = new DataTable();
             if (txtAoEcode.Text != "")
             {
                 try
                 {
                     string strCmd = "SELECT MEMBER_NAME+'('+DESIG+')' EName FROM EORA_MASTER WHERE ECODE=" + Convert.ToInt32(txtAoEcode.Text) + "";
                     dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                     if (dt.Rows.Count > 0)
                     {
                         txtAoName.Text = dt.Rows[0]["EName"].ToString();
                     }
                     else
                     {
                         txtAoName.Text = "";
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
                 txtAoName.Text = "";
             }


         }

         private void txtDeviationDetails_KeyPress(object sender, KeyPressEventArgs e)
         {
             //e.KeyChar = Char.ToUpper(e.KeyChar);
             //if (e.KeyChar != '\b')
             //{
             //    if (!char.IsLetter(e.KeyChar))
             //    {
             //        e.Handled = true;
             //    }
             //}

         }

         private void txtAoEcode_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (e.KeyChar != '\b')
             {
                 if (!char.IsDigit(e.KeyChar))
                 {
                     e.Handled = true;
                 }
             }
         }
     
         private void txtInvoiceNo_Validated(object sender, EventArgs e)
         {

             if (cbCompany.SelectedIndex > 0)
             {
                 if (txtInvoiceNo.Text != "")
                 {
                     FillInvoiceHeadData(Convert.ToInt32(txtInvoiceNo.Text), "");
                     
                 }
                 else
                 {

                     txtVillage.Text = "";
                     txtCustomerName.Text = "";
                     txtState.Text = "";
                     txtGLName.Text = "";
                     txtGLEcode.Text = "";
                     txtHouseNo.Text = "";
                     txtLandMark.Text = "";
                     txtPin.Text = "";
                     txtInvAmt.Text = "";
                     txtActualAmt.Text = "";
                                                   
                     txtOrderNo.Text = "";
                     txtMobileNo.Text = "";
                     txtMandal.Text = "";
                     txtDistrict.Text = "";
                     txtSREcode.Text = "";
                     txtSrEname.Text = "";
                     txtInvAmt.Text = "";
                     gvProductDetails.Rows.Clear();
                 }
             }
         }

         private void txtOrderNo_Validated(object sender, EventArgs e)
         {
             if (cbCompany.SelectedIndex > 0)
             {
                 if (txtOrderNo.Text != "")
                 {
                     FillInvoiceHeadData(0, txtOrderNo.Text);                   

                 }
                 else
                 {

                     txtVillage.Text = "";
                     txtCustomerName.Text = "";
                     txtState.Text = "";
                     //txtPin.Text = "";

                     //cmbRefType.SelectedIndex = 0;
                    
                     //txtInvoiceNo.Text = "";
                     txtMobileNo.Text = "";
                     txtMandal.Text = "";
                     txtDistrict.Text = "";
                     txtSREcode.Text = "";
                     txtSrEname.Text = "";
                     txtGLEcode.Text = "";
                     txtGLName.Text = "";
                     txtInvAmt.Text = "";
                   
                     txtActualAmt.Text = "";
                     txtDiffAmt.Text = "";
                     txtHouseNo.Text = "";
                     txtLandMark.Text = "";
                     txtPin.Text = "";                     
                     gvProductDetails.Rows.Clear();
                 }
             }
         }

         private void txtTrnNo_Validated(object sender, EventArgs e)
         {
             if (txtTrnNo.Text.Length > 21)
             {                
                 FillServiceWCorFFData(txtTrnNo.Text.ToString());
             }
             else
             {
                 flagUpdate = false;
                 cmbRefType.SelectedIndex = 0;
                 cmbDevType.SelectedIndex = 0;
                 cmbDevStatus.SelectedIndex = 0;
                 txtSREcode.Text = "";

                 txtSrEname.Text = "";
                 txtVillage.Text = "";
                 txtCustomerName.Text = "";
                 txtState.Text = "";                 
                 txtDeviationDetails.Text = "";                                
                 txtInvoiceNo.Text = "";
                 txtOrderNo.Text = "";
                 txtMobileNo.Text = "";
                 txtMandal.Text = "";
                 txtDistrict.Text = "";
                 txtAoEcode.Text = "";
                 txtAoName.Text = "";                 
                 dtpTrnDate.Value = DateTime.Today;
                 gvProductDetails.Rows.Clear();
                
                 txtInvAmt.Text = "";
                 txtActualAmt.Text = "";
                 txtGLName.Text = "";
                 txtGLEcode.Text = "";
                 txtDiffAmt.Text = "";
                 dtFarmerDetails.Rows.Clear();
                 txtHouseNo.Text = "";
                 txtLandMark.Text = "";
                 txtPin.Text = "";
                 gvSplitCustomerDetails.Rows.Clear();
                 btnSave.Enabled = true;
                 btnActionTaken.Enabled = true;
                 btnCancel.Enabled = true;
                 lblKnocking.Visible = false;
             }
         }

       
         private void gvSplitCustomerDetails_CellClick(object sender, DataGridViewCellEventArgs e)
         {
             if (e.RowIndex >= 0)
             {

                 if (e.RowIndex >= 0)
                 {

                     if (e.ColumnIndex == gvSplitCustomerDetails.Columns["Edit_Farmerdetails"].Index)
                     {
                         if (Convert.ToBoolean(gvSplitCustomerDetails.Rows[e.RowIndex].Cells["Edit_Farmerdetails"].Selected) == true)
                         {

                             int SlNo = Convert.ToInt32(gvSplitCustomerDetails.Rows[e.RowIndex].Cells[gvSplitCustomerDetails.Columns["SlNo_Farmer"].Index].Value);
                             DataRow[] dr = dtFarmerDetails.Select("SlNo_Farmer=" + SlNo);

                             frmAttendedFarmerDetails FarmerDetails = new frmAttendedFarmerDetails(dr);
                             FarmerDetails.objWCORFF = this;
                             FarmerDetails.ShowDialog();
                         }

                     }

                     if (e.ColumnIndex == gvSplitCustomerDetails.Columns["Del_FarmerDetails"].Index)
                     {
                         DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                         if (dlgResult == DialogResult.Yes)
                         {
                             int SlNo = Convert.ToInt32(gvSplitCustomerDetails.Rows[e.RowIndex].Cells[gvSplitCustomerDetails.Columns["SlNo_Farmer"].Index].Value);
                             DataRow[] dr = dtFarmerDetails.Select("SlNo_Farmer=" + SlNo);
                             dtFarmerDetails.Rows.Remove(dr[0]);
                             GetFarmerDetails();
                             MessageBox.Show("Selected Data Deleted Sucessfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         }
                     }

                 }
             }

         }

         private void txtActualAmt_Validated(object sender, EventArgs e)
         {
             CalculateDiffAmt();
         }
         private void CalculateDiffAmt()
         {
             txtDiffAmt.Text="";

             if (txtInvAmt.Text != "" && txtActualAmt.Text != "")
             {
                 dInvAmt = Convert.ToDouble(txtInvAmt.Text);
                 dActualAmt = Convert.ToDouble(txtActualAmt.Text);
                 if (dInvAmt > dActualAmt)
                     txtDiffAmt.Text = Convert.ToString(Convert.ToDouble(txtInvAmt.Text) - Convert.ToDouble(txtActualAmt.Text));
                 if (dInvAmt < dActualAmt)
                     txtDiffAmt.Text = Convert.ToString(Convert.ToDouble(txtActualAmt.Text) - Convert.ToDouble(txtInvAmt.Text));
                 if (dInvAmt==dActualAmt)
                     txtDiffAmt.Text = "0";
                 if (dActualAmt == 0)
                     txtDiffAmt.Text = "0";
                 
             }
         }

         private void txtActualAmt_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (e.KeyChar != '\b')
             {
                 if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                 {
                     e.Handled = true;
                 }
             }
             if (e.KeyChar == 46)
             {
                 if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                     e.Handled = true;
             }
            
         }

         private void txtFineAmt_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (e.KeyChar != '\b')
             {
                 if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                 {
                     e.Handled = true;
                 }
             }
             if (e.KeyChar == 46)
             {
                 if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                     e.Handled = true;
             }
         }

         private string GetEmployeeName(Int32 EmpEcode)
         {
             objSQLdb = new SQLDB();
             DataTable dt = new DataTable();
            
                 try
                 {
                     string strCmd = "SELECT MEMBER_NAME+'('+DESIG+')' EName FROM EORA_MASTER WHERE ECODE=" + EmpEcode + "";
                     dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                     if (dt.Rows.Count > 0)
                     {
                         sEmpName = dt.Rows[0]["EName"].ToString();
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
                 return sEmpName;
                 
         }

         private void txtGLEcode_Validated(object sender, EventArgs e)
         {
             if (txtGLEcode.Text.Length > 4)
             {
                 txtGLName.Text = GetEmployeeName(Convert.ToInt32(txtGLEcode.Text));
             }
             else
             {
                 txtGLName.Text = "";
             }
         }

         private void txtGLEcode_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (e.KeyChar != '\b')
             {
                 if (!char.IsDigit(e.KeyChar))
                 {
                     e.Handled = true;
                 }
             }
         }

         private void txtSREcode_Validated(object sender, EventArgs e)
         {
             if (txtSREcode.Text.Length > 4)
             {
                 txtSrEname.Text = GetEmployeeName(Convert.ToInt32(txtSREcode.Text));
             }
             else
             {
                 txtSrEname.Text = "";
             }
         }

         private void btnActionTaken_Click(object sender, EventArgs e)
         {             
             frmActionTakenForFF ATDetl = new frmActionTakenForFF(dtActionTakenDetails);
             ATDetl.objWCFF = this;
             ATDetl.Show();
         }

         private void btnAddCustDetails_Click(object sender, EventArgs e)
         {
             frmAttendedFarmerDetails FarmerDetails = new frmAttendedFarmerDetails(txtVillage.Text, txtMandal.Text, txtDistrict.Text, txtState.Text);
             FarmerDetails.objWCORFF = this;
             FarmerDetails.ShowDialog();
         }

         private void btnProductSearch_Click(object sender, EventArgs e)
         {
             ProductSearchAll PSearch = new ProductSearchAll("SERVICE_WC_FF");
             PSearch.objWCFF = this;
             PSearch.ShowDialog();
         }

         private void btnClearProd_Click(object sender, EventArgs e)
         {
             gvProductDetails.Rows.Clear();
         }

         private void btnVillageSearch_Click(object sender, EventArgs e)
         {
             VillageSearch vilsearch = new VillageSearch("SERVICE_WC_FF");
             vilsearch.objWCFF = this;
             vilsearch.ShowDialog();
         }

         void Control_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (e.KeyChar != '\b')
             {
                 if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                 {
                     e.Handled = true;
                 }
             }
             if (e.KeyChar == 46)
             {
                 if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                     e.Handled = true;
             }
         }
       
         private void gvProductDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
         {
             e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
         }

       
       
    }
}
