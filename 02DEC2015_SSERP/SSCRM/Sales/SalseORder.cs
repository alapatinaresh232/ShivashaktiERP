using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Diagnostics;
using System.Configuration;
using SSCRMDB;
using SSAdmin;
using SSTrans;

namespace SSCRM
{
    public partial class SalseORder : Form
    {
        private SQLDB objSQLDB = null;
        private InvoiceDB objData = null;
        private OrderDB objOrdData = null;
        private string strFormerid = string.Empty;
        private string strOrderNo = "0";
        private string strVillage = string.Empty;
        private string strDateOfBirth = string.Empty;
        private string strMarriageDate = string.Empty;
        public string strStateCode = string.Empty;
        private string strBranchCode = string.Empty;
        private string strECode = string.Empty;
        private bool blCustomerSearch = false;
        private bool blIsCellQty = true;
        private string strCreditSale = "NO";
        private string docMonth = CommonData.DocMonth;
        public int iUpdateVal = 0;
        private bool isUpdate = false;
        public SalseORder()
        {
            InitializeComponent();
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            ClearForm(this);
            txtDocMonth.Text = CommonData.DocMonth;
            FillEmployeeData();
            cbEcode.SelectedIndex = -1;
            cbRelation.SelectedIndex = 0;
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);

            //objOrdData = new OrderDB();
            //objData = new InvoiceDB();
            //objData = null;
            //objOrdData = null;
            objSQLDB = new SQLDB();
            string strSql = "SELECT ISNULL(MAX(SOH_AR_NUMBER),0)+1 FROM SALES_ORD_HEAD WHERE SOH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SOH_FIN_YEAR='" + CommonData.FinancialYear + "'";
            txtArNumber.Text = objSQLDB.ExecuteDataSet(strSql).Tables[0].Rows[0][0].ToString();
            objSQLDB = null;
            meOrdDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm(this);
            txtOrderNo.Text = string.Empty;
            meOrdDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            btnSave.Enabled = true;
            isUpdate = false;
            txtArNumber.Text = GenerateARNumber().ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void FillAddressData(string sSearch)
        {
            Hashtable htParam = null;
            objData = new InvoiceDB();
            string strDist = string.Empty;
            DataSet dsVillage = null;
            DataTable dtVillage = new DataTable();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (sSearch.Trim().Length >= 0)
                    htParam = new Hashtable();
                htParam.Add("sVillage", sSearch);
                htParam.Add("sDistrict", strDist);
                htParam.Add("sCDState", CommonData.StateCode);
                dsVillage = new DataSet();
                dsVillage = objData.GetVillageDataSet(htParam);
                dtVillage = dsVillage.Tables[0];
                if (dtVillage.Rows.Count == 1)
                {
                    txtVillage.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();  // ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    txtMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2]+ ""; 
                    txtDistrict.Text = dtVillage.Rows[0]["District"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                    txtState.Text = dtVillage.Rows[0]["State"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                    txtPin.Text = dtVillage.Rows[0]["PIN"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[5] + "";
                    strStateCode = dtVillage.Rows[0]["CDState"].ToString();  //((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[0] + "";
                    //if (btnSave.Enabled == true)
                    //FillProductData();
                }
                else if (dtVillage.Rows.Count > 1)
                {
                    txtVillage.Text = "";
                    txtMandal.Text = "";
                    txtDistrict.Text = "";
                    txtState.Text = "";
                    txtPin.Text = "";
                    strStateCode = "";
                    FillAddressComboBox(dtVillage);
                }

                else
                {
                    htParam = new Hashtable();
                    htParam.Add("sVillage", "%" + sSearch);
                    htParam.Add("sDistrict", strDist);
                    dsVillage = new DataSet();
                    dsVillage = objData.GetVillageDataSet(htParam);
                    dtVillage = dsVillage.Tables[0];
                    FillAddressComboBox(dtVillage);
                    ClearVillageDetails();
                }
                Cursor.Current = Cursors.Default;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
                Cursor.Current = Cursors.Default;

            }

        }

        private void FillEmployeeData()
        {
            objData = new InvoiceDB();
            DataSet dsEmp = null;
            try
            {
                dsEmp = objData.GetEcodeList(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth);
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
                objData = null;
                Cursor.Current = Cursors.Default;
            }

        }

        private void FillCustomerFarmerData(string sSearch, string sCustId)
        {
            DataSet ds = null;
            objOrdData = new OrderDB();
            try
            {
                if (sSearch.Trim() != "" || sCustId.Length > 0)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    ds = new DataSet();
                    ds = objOrdData.SalseOrderSearch_Get(sSearch, txtVillage.Text.ToString(), txtMandal.Text.ToString(), txtState.Text.ToString());
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count == 1)
                    {
                        strFormerid = Convert.ToString(dt.Rows[0]["SOH_FARMER_ID"]);
                        lblCustomerId.Text = Convert.ToString(dt.Rows[0]["SOH_FARMER_NAME"]);
                        txtCustomerName.Text = dt.Rows[0]["SOH_FARMER_NAME"] + "";
                        txtRelationName.Text = dt.Rows[0]["SOH_FORG_NAME"] + "";
                        cbRelation.Text = dt.Rows[0]["SOH_SO_FO"] + "";
                        txtHouseNo.Text = Convert.ToString(dt.Rows[0]["SOH_HOUSE_NO"]);
                        txtLandMark.Text = Convert.ToString(dt.Rows[0]["SOH_LANDMARK"]);
                        txtMobileNo.Text = dt.Rows[0]["SOH_MOBILE_NUMBER"] + "";
                        txtLanLineNo.Text = dt.Rows[0]["SOH_LANDLINE"] + "";
                    }
                    else if (dt.Rows.Count > 1)
                    {

                        FillCustomerComboBox(dt);

                    }
                    else
                    {
                        cbCustomer.DataSource = null;
                        cbCustomer.DataBindings.Clear();
                        cbCustomer.Items.Clear();
                        txtCustomerName.Text = "";
                        txtRelationName.Text = "";
                        txtMobileNo.Text = "";
                        txtHouseNo.Text = "";
                        txtLandMark.Text = "";
                        txtLanLineNo.Text = "";
                        strFormerid = "";
                        lblCustomerId.Text = "";
                    }
                }

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objData = null;
                ds.Dispose();
                Cursor.Current = Cursors.Default;

            }

        }

        private void FillProductData()
        {
            DataSet ds = null;
            objData = new InvoiceDB();
            //string[] strBCode;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ds = new DataSet();
                ds = objData.GetProductDataSet(CommonData.CompanyCode, CommonData.BranchCode);
                DataTable dt = ds.Tables[0];
                int intRow = 1;
                this.gvProductDetails.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["pm_single_mrp"]) > 0)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                        cellMainProductID.Value = dt.Rows[i]["pm_product_id"];
                        tempRow.Cells.Add(cellMainProductID);

                        DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                        cellMainProduct.Value = dt.Rows[i]["pm_product_name"];
                        tempRow.Cells.Add(cellMainProduct);

                        DataGridViewCell cellSubProduct = new DataGridViewTextBoxCell();
                        cellSubProduct.Value = dt.Rows[i]["category_name"];
                        tempRow.Cells.Add(cellSubProduct);

                        DataGridViewCell cellDessc = new DataGridViewTextBoxCell();
                        cellDessc.Value = dt.Rows[i]["pm_product_name"];
                        tempRow.Cells.Add(cellDessc);

                        DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                        cellQTY.Value = "";
                        tempRow.Cells.Add(cellQTY);

                        DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                        cellRate.Value = dt.Rows[i]["pm_single_mrp"];
                        tempRow.Cells.Add(cellRate);

                        DataGridViewCell cellAmt = new DataGridViewTextBoxCell();
                        cellAmt.Value = "";
                        tempRow.Cells.Add(cellAmt);

                        DataGridViewCell cellPoints = new DataGridViewTextBoxCell();
                        cellPoints.Value = Convert.ToDouble(dt.Rows[i]["TIPPoints"]).ToString("f");
                        tempRow.Cells.Add(cellPoints);

                        DataGridViewCell cellDBRate = new DataGridViewTextBoxCell();
                        cellDBRate.Value = dt.Rows[i]["pm_single_mrp"];
                        tempRow.Cells.Add(cellDBRate);

                        DataGridViewCell cellDBPoints = new DataGridViewTextBoxCell();
                        cellDBPoints.Value = Convert.ToDouble(dt.Rows[i]["TIPPoints"]).ToString("f");
                        tempRow.Cells.Add(cellDBPoints);

                        intRow = intRow + 1;
                        gvProductDetails.Rows.Add(tempRow);
                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
                ds.Dispose();
                Cursor.Current = Cursors.Default;

            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int intSaved = 0;
            try
            {
                if (CheckData() == true)
                {
                    if (SaveInvoiceHeadData() >= 1)
                    {
                        //intSaved = SaveInvoiceDeatailData();
                        //if (intSaved > 0)
                        //{
                            strOrderNo = "0";
                            strFormerid = "";
                            ClearForm(this);
                            txtOrderNo.Text = string.Empty;
                            txtAdvanceAmt.Text = string.Empty;
                            meOrdDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
                            cbVillage.DataBindings.Clear();
                            objData = new InvoiceDB();
                            objData = null;
                            btnCancel_Click(null, null);
                            isUpdate = false;
                            txtDocMonth.Text = CommonData.DocMonth;
                            MessageBox.Show("Data saved successfully", "Sale Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                    }
                    else
                        MessageBox.Show("Data not saved", "Sale Order", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                    MessageBox.Show("Data not saved", "Sale Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }

        }

        private bool CheckData()
        {
            bool blValue = true;
            strCreditSale = "NO";
            if (Convert.ToString(txtOrderNo.Text).Length == 0)
            {
                MessageBox.Show("Enter Order number!", "Sales Order");
                blValue = false;
                txtOrderNo.Focus();
                return blValue;
            }
            //if (CheckOrderNoInInvoice())
            //{
            //    MessageBox.Show("Given order number already invoiced\n You cannot modify data.", "Sales Order");
            //    blValue = false;
            //    txtOrderNo.Focus();
            //    return blValue;
            //}
            if (Convert.ToString(txtOrdAmt.Text).Length == 0)
            {
                MessageBox.Show("Enter Order amount!", "Sales Order");
                blValue = false;
                txtOrdAmt.Focus();
                return blValue;
            }
            if (Convert.ToString(txtAdvanceAmt.Text).Length == 0)
            {
                MessageBox.Show("Enter Advance amount!", "Sales Order");
                blValue = false;
                txtAdvanceAmt.Focus();
                return blValue;
            }

            if (cbEcode.SelectedIndex == -1)
            {
                MessageBox.Show("Enter Employee Code!", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                cbEcode.Focus();
                return blValue;
            }
            else
            {
                string sEcode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                string sLockStatus = GetLockingStatus(sEcode, txtDocMonth.Text.ToUpper());
                if (sLockStatus == "Y")
                {
                    MessageBox.Show("Sales Entry for this GC/Gl is Locked, \nPlease Contact you manager for Information/Unlocking", "SSERP-SummaryBultn", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                //else if (sLockStatus == "")
                //{
                //    MessageBox.Show("First Enter Sales Summary Bulletin for this SR", "SSERP-SummaryBultn", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    return false;
                //}

            }
            if (Convert.ToString(txtVillage.Text).Length == 0 || txtVillage.Text.ToString().Trim() == "NOVILLAGE")
            {
                MessageBox.Show("Enter Village!", "Sales Order");
                blValue = false;
                txtVillageSearch.Focus();
                return blValue;
            }
            if (strStateCode.Length == 0)
            {
                MessageBox.Show("No State for " + txtVillage.Text + " Village!", "Sales Order");
                blValue = false;
                txtVillageSearch.Focus();
                return blValue;
            }
            if (Convert.ToString(txtCustomerName.Text).Length == 0 || txtCustomerName.Text.ToString().Trim() == "NOVILLAGE")
            {
                MessageBox.Show("Enter Customer name!", "Sales Order");
                blValue = false;
                txtCustomerName.Focus();
                return blValue;
            }
            bool blInvDtl = true;
            for (int i = 0; i < gvProductDetails.Rows.Count; i++)
            {
                if (Convert.ToString(gvProductDetails.Rows[i].Cells["Qty"].Value) == "")
                {
                    blInvDtl = false;
                    break;
                }
            }
            if (blInvDtl == false)
            {
                blValue = false;
                MessageBox.Show("Enter product details", "Sales Order");
                return false;
            }
            //blInvDtl = false;
            //if (IsSaleOrder == false)
            //{
                objData = new InvoiceDB();
                DataTable dtOrderStatus = new DataTable();
                try
                {
                    dtOrderStatus = objData.Get_OrderSheetIssueDetails_ByOrderNo(CommonData.CompanyCode, CommonData.BranchCode, txtDocMonth.Text.ToUpper(), Convert.ToInt32(txtOrderNo.Text).ToString("00000")).Tables[0];
                }
                catch { return false; }
                if (dtOrderStatus.Rows.Count > 0)
                {
                    if (dtOrderStatus.Rows[0]["Status"].ToString() != "DOCUMENTED")
                    {
                        MessageBox.Show("Order Form " + txtOrderNo.Text.ToString() +
                                    " Status updated as " + dtOrderStatus.Rows[0]["Status"].ToString() +
                                    " in the Order Sheet Returns Screen", "SSERP :: Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Order Form " + txtOrderNo.Text.ToString() +
                                    " Not Issued to any One in the Order Form Issue Screen", "SSERP :: Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            //}
            //Security objSecur = new Security();
            //if (objSecur.CanModifyDataUserAsPerBackDays(Convert.ToDateTime(meOrdDate.Text)) == false)
            //{
            //    MessageBox.Show("You cannot manipulate backdays data!\n If you want to modify, Contact to IT-Department", "Sale Order");
            //    blValue = false;
            //    txtOrderNo.Focus();
            //    return blValue;
            //}
            //objSecur = null;
            return blValue;
        }
        private string GetLockingStatus(string sEcode, string sDocMonth)
        {
            string sStatus = "";
            objSQLDB = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                dt = objSQLDB.ExecuteDataSet("EXEC GetSRSummaryBulletinHeadData '','','" + sEcode + "','" + sDocMonth + "'").Tables[0];
                sStatus = dt.Rows[0]["LockStatus"].ToString();
            }
            catch
            {
                sStatus = "";
            }
            finally
            {
                objSQLDB = null;
                dt = null;
            }
            return sStatus;
        }
        private int SaveInvoiceHeadData()
        {
            objSQLDB = new SQLDB();
            objOrdData = new OrderDB();
            string sqlText = "";
            object objVMob = "NULL";
            int intRec = 0;
            try
            {
                if (txtMobileNo.Text != "")
                    objVMob = txtMobileNo.Text;

                DataSet dsCnt = objOrdData.SalseOrderSearch_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, Convert.ToInt32(txtOrderNo.Text).ToString("00000"));
                if (dsCnt.Tables[0].Rows.Count > 0)
                    txtArNumber.Text = dsCnt.Tables[0].Rows[0]["SOH_AR_NUMBER"].ToString();
                if (dsCnt.Tables[0].Rows.Count == 0)
                {
                    DataTable dtInv = objSQLDB.ExecuteDataSet(" SELECT SIBH_DOCUMENT_MONTH, SIBH_INVOICE_NUMBER "+
                                                "FROM SALES_INV_BULTIN_HEAD WHERE SIBH_BRANCH_CODE='"+CommonData.BranchCode+"' "+
                                                "AND SIBH_FIN_YEAR = '" + CommonData.FinancialYear + 
                                                "' AND SIBH_ORDER_NUMBER = '" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") + "'").Tables[0];
                    if (dtInv.Rows.Count > 0)
                    {
                        MessageBox.Show("Order No Allready Entered With Invoice " + dtInv.Rows[0]["SIBH_INVOICE_NUMBER"] +
                                            " in DocMonth " + dtInv.Rows[0]["SIBH_DOCUMENT_MONTH"],
                                            "SSERP - Sale Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return 0;
                    }
                    string strSql = " SELECT ISNULL(MAX(SOH_AR_NUMBER),0)+1 FROM SALES_ORD_HEAD WHERE SOH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SOH_FIN_YEAR='" + CommonData.FinancialYear + "'";
                    txtArNumber.Text = objSQLDB.ExecuteDataSet(strSql).Tables[0].Rows[0][0].ToString();

                    sqlText = " INSERT INTO SALES_ORD_HEAD(SOH_COMPANY_CODE,SOH_STATE_CODE,SOH_BRANCH_CODE,SOH_FIN_YEAR,SOH_DOCUMENT_MONTH,SOH_ORDER_DATE,SOH_ORDER_NUMBER,SOH_EORA_CODE,SOH_ORDER_AMOUNT,SOH_ORDER_ADV_AMT,SOH_VILLAGE,SOH_FARMER_NAME,SOH_SO_FO,SOH_FORG_NAME,SOH_MOBILE_NUMBER,SOH_FARMER_ID,SOH_MANDAL,SOH_DISTRICT,SOH_STATE,SOH_PIN,SOH_HOUSE_NO,SOH_LANDMARK,SOH_LANDLINE,SOH_CREATED_BY,SOH_AUTHORIZED_BY,SOH_CREATED_DATE,SOH_AR_NUMBER)" +
                     " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode + "' , '" + CommonData.FinancialYear + "','" + CommonData.DocMonth + "','" + Convert.ToDateTime(meOrdDate.Value).ToString("dd/MMM/yyy") + "'" +
                     ", '" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") + "', " + strECode + ", " + txtOrdAmt.Text + ", " + txtAdvanceAmt.Text + ", '" + txtVillage.Text +
                     "','" + txtCustomerName.Text + "','" + cbRelation.Text + "', '" + txtRelationName.Text + "'," + objVMob + ",'99999','" + txtMandal.Text +
                   "','" + txtDistrict.Text + "', '" + txtState.Text + "', '" + txtPin.Text + "','" + txtHouseNo.Text + "','" + txtLandMark.Text + "','" + txtLanLineNo.Text + "', '" + CommonData.LogUserId + "', '" + CommonData.LogUserId + "', '" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") + "'," + txtArNumber.Text + ")";

                    for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                    {
                        if (gvProductDetails.Rows[i].Cells["Qty"].Value.ToString() != "")
                        {
                            sqlText += " INSERT INTO SALES_ORD_DETL(SOD_COMPANY_CODE,SOD_STATE_CODE,SOD_BRANCH_CODE" +
                                        ",SOD_FIN_YEAR,SOD_DOCUMENT_MONTH,SOD_ORDER_DATE,SOD_ORDER_NUMBER,SOD_PRODUCT_ID" +
                                        ",SOD_QTY, SOD_PRICE, SOD_AMOUNT)" +
                                        " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode +
                                        "', '" + CommonData.BranchCode + "','" + CommonData.FinancialYear +
                                        "','" + docMonth + "','" + Convert.ToDateTime(meOrdDate.Value).ToString("dd/MMM/yyyy") + "','" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") + "','" +
                                         gvProductDetails.Rows[i].Cells["ProductID"].Value.ToString().Trim() +
                                         "'," + gvProductDetails.Rows[i].Cells["Qty"].Value +
                                         ", " + gvProductDetails.Rows[i].Cells["Rate"].Value +
                                         ", " + gvProductDetails.Rows[i].Cells["Amount"].Value + ")";
                        }
                    }
                }
                else
                {
                    sqlText = " UPDATE SALES_ORD_HEAD SET SOH_ORDER_DATE='" + Convert.ToDateTime(meOrdDate.Value).ToString("dd/MMM/yyy") +
                        "',SOH_EORA_CODE=" + strECode + ",SOH_ORDER_AMOUNT=" + Convert.ToDouble(txtOrdAmt.Text).ToString("f") + ",SOH_ORDER_ADV_AMT=" + Convert.ToDouble(txtAdvanceAmt.Text).ToString("f") + ",SOH_VILLAGE='" + txtVillage.Text +
                        "',SOH_FARMER_NAME='" + txtCustomerName.Text + "',SOH_SO_FO='" + cbRelation.Text + "',SOH_FORG_NAME='" + txtRelationName.Text +
                        "',SOH_MOBILE_NUMBER=" + objVMob + ",SOH_MANDAL='" + txtMandal.Text + "',SOH_DISTRICT='" + txtDistrict.Text +
                        "',SOH_STATE='" + txtState.Text + "',SOH_PIN='" + txtPin.Text + "',SOH_HOUSE_NO='" + txtHouseNo.Text + "',SOH_LANDMARK='" + txtLandMark.Text +
                        "',SOH_LANDLINE='" + txtLanLineNo.Text + "',SOH_LAST_MODIFIED_BY='" + CommonData.LogUserId + "',SOH_LAST_MODIFIED_DATE='" + Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MMM/yyyy") +
                        "' WHERE ltrim(rtrim(SOH_ORDER_NUMBER))='" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") +
                        "' AND SOH_company_code='" + CommonData.CompanyCode +
                        "' AND SOH_branch_code='" + CommonData.BranchCode +
                        "' AND SOH_fin_year='" + CommonData.FinancialYear +
                        "' AND SOH_AR_NUMBER='" + txtArNumber.Text + "'";

                    sqlText += " DELETE from SALES_ORD_DETL" +
                                     " WHERE sod_company_code='" + CommonData.CompanyCode +
                                         "' AND sod_branch_code='" + CommonData.BranchCode +
                                         "' AND sod_order_number='" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") +
                                         "' AND sod_fin_year='" + CommonData.FinancialYear + "'";

                    for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                    {
                        if (gvProductDetails.Rows[i].Cells["Qty"].Value.ToString() != "")
                        {
                            sqlText += " INSERT INTO SALES_ORD_DETL(SOD_COMPANY_CODE,SOD_STATE_CODE,SOD_BRANCH_CODE" +
                                        ",SOD_FIN_YEAR,SOD_DOCUMENT_MONTH,SOD_ORDER_DATE,SOD_ORDER_NUMBER,SOD_PRODUCT_ID" +
                                        ",SOD_QTY, SOD_PRICE, SOD_AMOUNT)" +
                                        " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode +
                                        "', '" + CommonData.BranchCode + "','" + CommonData.FinancialYear +
                                        "','" + docMonth + "','" + Convert.ToDateTime(meOrdDate.Value).ToString("dd/MMM/yyyy") + "','" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") + "','" +
                                         gvProductDetails.Rows[i].Cells["ProductID"].Value.ToString().Trim() +
                                         "'," + gvProductDetails.Rows[i].Cells["Qty"].Value +
                                         ", " + gvProductDetails.Rows[i].Cells["Rate"].Value +
                                         ", " + gvProductDetails.Rows[i].Cells["Amount"].Value + ")";
                        }
                    }
                    
                    
                }
                intRec = objSQLDB.ExecuteSaveData(sqlText);
            }
            catch (Exception ex)
            {
                intRec = 0;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objSQLDB = null;                
            }
            return intRec;
        }

        private int SaveInvoiceDeatailData()
        {
            objSQLDB = new SQLDB();
            string sqlText = "";
            string sqlDelete = "";
            int intRec = 0;
            try
            {
                if (isUpdate == true)
                {
                    sqlDelete = "DELETE from SALES_ORD_DETL" +
                                    " WHERE SOD_COMPANY_CODE='" + CommonData.CompanyCode +
                                        "' AND SOD_BRANCH_CODE='" + CommonData.BranchCode +
                                        "' AND SOD_ORDER_NUMBER='" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") +
                                        "' AND SOD_FIN_YEAR='" + CommonData.FinancialYear + "'";
                    intRec = objSQLDB.ExecuteSaveData(sqlDelete);
                }
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    if (gvProductDetails.Rows[i].Cells["Qty"].Value.ToString() != "")
                    {
                        sqlText += " INSERT INTO SALES_ORD_DETL(SOD_COMPANY_CODE,SOD_STATE_CODE,SOD_BRANCH_CODE"+
                                    ",SOD_FIN_YEAR,SOD_DOCUMENT_MONTH,SOD_ORDER_DATE,SOD_ORDER_NUMBER,SOD_PRODUCT_ID"+
                                    ",SOD_QTY, SOD_PRICE, SOD_AMOUNT)" +
                                    " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + 
                                    "', '" + CommonData.BranchCode + "','" + CommonData.FinancialYear + 
                                    "','" + docMonth + "','" + Convert.ToDateTime(meOrdDate.Value).ToString("dd/MMM/yyyy") + "','" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") + "','" +
                                     gvProductDetails.Rows[i].Cells["ProductID"].Value.ToString().Trim() + 
                                     "'," + gvProductDetails.Rows[i].Cells["Qty"].Value + 
                                     ", " + gvProductDetails.Rows[i].Cells["Rate"].Value + 
                                     ", " + gvProductDetails.Rows[i].Cells["Amount"].Value + ")";
                    }
                }
                intRec = 0;
                if (sqlText.Length > 10)
                    intRec = objSQLDB.ExecuteSaveData(sqlText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
            }
            return intRec;
        }

        private void txtCustomerSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtCustomerSearch.Text.Length > 0)
                {
                    blCustomerSearch = true;
                    if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                    {
                        if (FindInputCustomerSearch() == false)
                        {
                            FillCustomerFarmerData(txtCustomerSearch.Text, "");
                        }
                    }
                }
            }
            catch// (Exception ex)
            {

            }
            if (e.KeyValue == 8)
            {
                if (this.txtCustomerSearch.TextLength >= 2)
                    FillCustomerFarmerData(Convert.ToString(txtCustomerSearch.Text.Trim()), "");
                this.txtCustomerName.SelectionStart = this.txtCustomerSearch.TextLength;
            }
        }

        private void FillOrderData(string sOrdNo,string sType)
        {
            Hashtable ht = new Hashtable();
            try
            {
                objOrdData = new OrderDB();
                if (sType == "ORDERNO")
                    ht = objOrdData.GetSalseOrderData(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, sOrdNo);
                else
                    ht = objOrdData.GetSalseOrderDataByARNumber(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, txtArNumber.Text);
                DataTable dtInvH = (DataTable)ht["OrdHead"];
                DataTable dtInvD = (DataTable)ht["OrdDetail"];
                if (dtInvH.Rows.Count > 0)
                {
                    ClearForm(this);
                    isUpdate = true;
                    strOrderNo = dtInvH.Rows[0]["soh_order_number"] + "";
                    strFormerid = Convert.ToString(dtInvH.Rows[0]["soh_farmer_id"] + "");
                    strECode = dtInvH.Rows[0]["soh_eora_code"] + "";
                    txtEcodeSearch.Text = strECode;
                    txtEcodeSearch_KeyUp(null, null);
                    docMonth = dtInvH.Rows[0]["SOH_DOCUMENT_MONTH"] + "";
                    txtDocMonth.Text = dtInvH.Rows[0]["SOH_DOCUMENT_MONTH"] + "";
                    meOrdDate.Value = Convert.ToDateTime(dtInvH.Rows[0]["soh_order_date"]);
                    txtHouseNo.Text = Convert.ToString(dtInvH.Rows[0]["soh_house_no"]);
                    txtLandMark.Text = Convert.ToString(dtInvH.Rows[0]["soh_landmark"]);
                    txtVillage.Text = dtInvH.Rows[0]["soh_village"] + "";
                    txtMandal.Text = dtInvH.Rows[0]["soh_mandal"] + "";
                    txtDistrict.Text = dtInvH.Rows[0]["soh_district"] + "";
                    txtState.Text = dtInvH.Rows[0]["soh_state"] + "";
                    strStateCode = dtInvH.Rows[0]["soh_state_code"] + "";
                    txtPin.Text = dtInvH.Rows[0]["soh_pin"] + "";
                    txtCustomerName.Text = dtInvH.Rows[0]["soh_farmer_name"] + "";
                    cbRelation.Text = dtInvH.Rows[0]["soh_so_fo"] + "";
                    txtRelationName.Text = dtInvH.Rows[0]["soh_forg_name"] + "";
                    txtMobileNo.Text = dtInvH.Rows[0]["soh_mobile_number"] + "";
                    txtLanLineNo.Text = dtInvH.Rows[0]["soh_landline"] + "";
                    txtOrderNo.Text = dtInvH.Rows[0]["soh_order_number"] + "";
                    txtOrdAmt.Text = dtInvH.Rows[0]["soh_order_amount"] + "";
                    txtAdvanceAmt.Text = dtInvH.Rows[0]["soh_order_adv_amt"] + "";
                    txtArNumber.Text = dtInvH.Rows[0]["soh_ar_number"] + "";
                    lblCustomerId.Text = strFormerid;
                    SaveDeleteEnableDisable();
                    FillInvoiceDetail(dtInvD); 
                    iUpdateVal = 1;
                    //int inu = Convert.ToInt32(CommonData.LogUserBackDays);
                        //Convert.ToInt32((Convert.ToDateTime(CommonData.CurrentDate)-Convert.ToDateTime(dtInvH.Rows[0]["SOH_CREATED_DATE"])).TotalDays);
                    if (CommonData.LogUserRole == "MANAGEMENT" || CommonData.LogUserId == "ADMIN" || Convert.ToInt32(CommonData.LogUserBackDays) > Convert.ToInt32((Convert.ToDateTime(CommonData.CurrentDate) - Convert.ToDateTime(dtInvH.Rows[0]["SOH_CREATED_DATE"])).TotalDays))
                    {
                        btnSave.Enabled = true;
                        btnDelete.Enabled = true;                        
                    }
                    else
                    {
                        btnSave.Enabled = false;
                        btnDelete.Enabled = false;
                    }
                }
                else
                {
                    isUpdate = false;
                    iUpdateVal = 0;
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                    docMonth = CommonData.DocMonth;
                    txtDocMonth.Text = CommonData.DocMonth;
                    txtArNumber.Text = GenerateARNumber().ToString();
                    // MessageBox.Show("This salse order number data not available.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
            }
        }
        private int GenerateARNumber()
        {
            objSQLDB = new SQLDB();
            int iArNumber = 0;
            string strSql = "SELECT ISNULL(MAX(SOH_AR_NUMBER),0)+1 FROM SALES_ORD_HEAD WHERE SOH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SOH_FIN_YEAR='" + CommonData.FinancialYear + "'";
            iArNumber = Convert.ToInt32(objSQLDB.ExecuteDataSet(strSql).Tables[0].Rows[0][0].ToString());
            return iArNumber;
        }
        private void SaveDeleteEnableDisable()
        {
            try
            {
                //if (CommonData.BranchType == "HO" || CommonData.LogUserId.ToUpper() == "ADMIN" || CommonData.LogUserRole.ToUpper() == "MANAGEMENT")
                //{
                //    btnSave.Enabled = true;
                //    btnDelete.Enabled = true;
                //}
                //else
                //{
                //    btnSave.Enabled = false;
                //    btnDelete.Enabled = false;
                //}
            }
            catch
            {
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
                    if (Convert.ToDouble(dt.Rows[i]["sod_qty"]) > 0)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                        cellMainProductID.Value = dt.Rows[i]["SOD_PRODUCT_ID"];
                        tempRow.Cells.Add(cellMainProductID);

                        DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                        cellMainProduct.Value = dt.Rows[i]["pm_product_name"] + "(Rs = " + Convert.ToDecimal(dt.Rows[i]["SingleMRP"]).ToString("0") + ")";
                        tempRow.Cells.Add(cellMainProduct);

                        DataGridViewCell cellDessc = new DataGridViewTextBoxCell();
                        cellDessc.Value = dt.Rows[i]["category_name"];
                        tempRow.Cells.Add(cellDessc);

                        DataGridViewCell cellQTY = new DataGridViewTextBoxCell();
                        if (Convert.ToInt32(dt.Rows[i]["sod_qty"]) > 0)
                            cellQTY.Value = Convert.ToInt32(dt.Rows[i]["sod_qty"]);
                        else
                            cellQTY.Value = "0";

                        tempRow.Cells.Add(cellQTY);

                        DataGridViewCell cellPrice = new DataGridViewTextBoxCell();
                        cellPrice.Value = Convert.ToDouble(dt.Rows[i]["SOD_PRICE"]).ToString("f");
                        tempRow.Cells.Add(cellPrice);

                        DataGridViewCell cellAmount = new DataGridViewTextBoxCell();
                        cellAmount.Value = Convert.ToDouble(dt.Rows[i]["SOD_AMOUNT"]).ToString("f");
                        tempRow.Cells.Add(cellAmount);

                        if (dt.Rows[i]["ProductType"].ToString() == "Combi")
                            tempRow.Cells[2].Style.BackColor = Color.FromArgb(192, 192, 255);
                        else if (dt.Rows[i]["ProductType"].ToString() == "Free")   //Free
                            tempRow.Cells[2].Style.BackColor = Color.MediumTurquoise;

                        intRow = intRow + 1;
                        gvProductDetails.Rows.Add(tempRow);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }

        private void txtVillage_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtVillageSearch.Text.Length == 0)
                {
                    cbVillage.DataSource = null;
                    cbVillage.DataBindings.Clear();
                    cbVillage.Items.Clear();
                    if (btnSave.Enabled == true)
                        ClearVillageDetails();
                }
                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {
                    if (FindInputAddressSearch() == false)
                    {
                        FillAddressData(txtVillageSearch.Text);
                    }
                }
            }
            catch// (Exception ex)
            {

            }
        }

        private void gvProductDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            blIsCellQty = true;
            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 4)
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    txtQty.MaxLength = 3;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }

            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 5)
            {
                TextBox txtRate = e.Control as TextBox;
                if (txtRate != null)
                {
                    txtRate.MaxLength = 10;
                    txtRate.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }

            if ((((System.Windows.Forms.DataGridView)(sender))).CurrentCell.ColumnIndex == 6)
            {
                if (((System.Windows.Forms.DataGridView)(sender)).CurrentRow.Cells[5].Value + "" != "")
                {
                    TextBox txtAmt = e.Control as TextBox;
                    if (txtAmt != null)
                    {
                        txtAmt.MaxLength = 10;
                        txtAmt.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                    }
                }
                else
                    blIsCellQty = false;
            }


        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46) || (blIsCellQty == false))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }


            //if (char.IsNumber(e.KeyChar) == false || blIsCellQty == false)
            //    e.Handled = true;
            //if (e.KeyChar == 8)
            //    e.Handled = false;
        }

        private void txtCustomerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }
        private void txtRelationName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }
        private void gvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridView dgv = (DataGridView)sender;
                if (e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6)
                {
                    DataGridViewCell textBoxCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (textBoxCell != null)
                    {
                        gvProductDetails.CurrentCell = textBoxCell;
                        dgv.BeginEdit(true);
                    }
                }
            }
        }
        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) != "")
                {
                    if (Convert.ToInt32(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) >= 0 && Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value) >= 0)
                    {
                        gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToInt32(gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) * (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value));
                        gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value).ToString("f");
                    }
                }
                else
                    gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = string.Empty;
            }
            if (e.ColumnIndex == 5)
            {
                if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) != "")
                {
                    if (Convert.ToInt32(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) >= 0 && Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value) >= 0)
                    {
                        gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) * (Convert.ToInt32(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value));
                        gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value).ToString("f");
                    }
                }
                else
                    gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value = "0.00";
            }
            if (e.ColumnIndex == 6)
            {
                if ((Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value) == "") || (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Amount"].Value) == "0"))
                {
                    gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value = string.Empty;
                }


            }
            txtOrdAmt.Text = GetInvoiceAmt().ToString("f");
            txtAdvanceAmt.Text = txtOrdAmt.Text;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtOrderNo.Text.Length > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to Delete Sale Order Details?",
                                       "Sales Order", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (!CheckOrderNoInInvoice())
                    {
                        objSQLDB = new SQLDB();
                        string strDelete = " DELETE from SALES_ORD_DETL " +
                                            " WHERE sod_company_code='" + CommonData.CompanyCode +
                                            "' AND  sod_branch_code='" + CommonData.BranchCode +
                                            "' AND sod_order_number='" + txtOrderNo.Text +
                                            "' AND sod_fin_year='" + CommonData.FinancialYear + "' AND SOD_DOCUMENT_MONTH='" + CommonData.DocMonth + "'";

                        int intRec = objSQLDB.ExecuteSaveData(strDelete);
                        if (intRec > 0)
                        {
                            intRec = 1;
                            strDelete = " DELETE from SALES_ORD_HEAD " +
                                                " WHERE soh_company_code='" + CommonData.CompanyCode +
                                                "' AND  soh_branch_code='" + CommonData.BranchCode +
                                                "' AND soh_order_number='" + txtOrderNo.Text +
                                                "' AND soh_fin_year='" + CommonData.FinancialYear + "' AND SOH_DOCUMENT_MONTH='" + CommonData.DocMonth + "'";

                        }
                        intRec = objSQLDB.ExecuteSaveData(strDelete);
                        if (intRec > 0)
                        {
                            MessageBox.Show("Salse Order " + txtOrderNo.Text + " Deleted ");
                            ClearForm(this);
                            isUpdate = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Given order number already invoiced\n You cannot delete.", "Sale Order");
                    }
                    //objData = new InvoiceDB();
                    //txtInvoiceNo.Text = objData.GenerateInvoiceNo(CommonData.CompanyCode, CommonData.BranchCode).ToString();
                    //objData = null;
                }
            }
        }

        private bool CheckOrderNoInInvoice()
        {
            objOrdData = new OrderDB();
            bool blOrdno = false;
            try
            {
                blOrdno = objOrdData.CheckOrderNo(txtOrderNo.Text.ToString().Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objOrdData = null;
            }
            return blOrdno;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            this.Close();
            InvoiceView childForm = new InvoiceView();
            childForm.Show();
        }


        private void FillAddressComboBox(DataTable dt)
        {
            DataTable dataTable = new DataTable("Village");

            dataTable.Columns.Add("StateID", typeof(String));
            dataTable.Columns.Add("Panchayath", typeof(String));
            dataTable.Columns.Add("Mandal", typeof(String));
            dataTable.Columns.Add("District", typeof(String));
            dataTable.Columns.Add("State", typeof(String));
            dataTable.Columns.Add("Pin", typeof(String));
            cbVillage.DataBindings.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
                dataTable.Rows.Add(new String[] { dt.Rows[i]["CDState"] + 
                     "", dt.Rows[i]["PANCHAYAT"] + 
                     "", dt.Rows[i]["MANDAL"] + 
                     "", dt.Rows[i]["DISTRICT"] + 
                     "", dt.Rows[i]["STATE"] + "", dt.Rows[i]["PIN"] + ""});


            cbVillage.DataSource = dataTable;
            cbVillage.DisplayMember = "Panchayath";
            cbVillage.ValueMember = "StateID";
        }
        private void FillCustomerComboBox(DataTable dt)
        {
            DataTable dataCustomer = new DataTable("Customer");
            dataCustomer.Columns.Add("farmer_ID", typeof(String));
            dataCustomer.Columns.Add("farmer_name", typeof(String));
            dataCustomer.Columns.Add("forg_name", typeof(String));
            dataCustomer.Columns.Add("village", typeof(String));

            cbCustomer.DataBindings.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
                dataCustomer.Rows.Add(new String[] { dt.Rows[i]["SOH_FARMER_ID"] + 
                     "", dt.Rows[i]["SOH_FARMER_NAME"] + "", dt.Rows[i]["SOH_FORG_NAME"] + 
                     "", dt.Rows[i]["SOH_VILLAGE"] + ""});


            cbCustomer.DataSource = dataCustomer;
            cbCustomer.DisplayMember = "farmer_name";
            cbCustomer.ValueMember = "farmer_ID";
        }

        private bool FindInputAddressSearch()
        {
            bool blFind = false;
            try
            {
                for (int i = 0; i < this.cbVillage.Items.Count; i++)
                {
                    string strItem = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                    if (strItem.IndexOf(txtVillageSearch.Text) > -1)
                    {
                        blFind = true;
                        cbVillage.SelectedIndex = i;
                        txtVillage.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[1] + "";
                        txtMandal.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[2] + "";
                        txtDistrict.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[3] + "";
                        txtState.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[4] + "";
                        txtPin.Text = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[5] + "";
                        strStateCode = ((System.Data.DataRowView)(this.cbVillage.Items[i])).Row.ItemArray[0] + "";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
            }
            return blFind;
        }
        private bool FindInputCustomerSearch()
        {
            bool blFind = false;
            objOrdData = new OrderDB();
            try
            {
                for (int i = 0; i < this.cbCustomer.Items.Count; i++)
                {
                    string strItem = ((System.Data.DataRowView)(this.cbCustomer.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                    if (strItem.IndexOf(txtCustomerSearch.Text) > -1)
                    {
                        DataSet ds = new DataSet();
                        blFind = true;
                        cbCustomer.SelectedIndex = i;
                        ds = objOrdData.SalseOrderSearch_Get(strItem, txtVillage.Text.ToString(), txtMandal.Text.ToString(), txtState.Text.ToString());
                        DataTable dt = ds.Tables[0];

                        if (dt.Rows.Count == 1)
                        {
                            strFormerid = Convert.ToString(dt.Rows[0]["soh_farmer_id"]);
                            txtOrderNo.Text = dt.Rows[0]["soh_order_number"] + "";
                            meOrdDate.Value = Convert.ToDateTime(dt.Rows[0]["soh_order_date"]);
                            txtCustomerName.Text = dt.Rows[0]["soh_farmer_name"] + "";
                            txtRelationName.Text = dt.Rows[0]["soh_forg_name"] + "";
                            cbRelation.Text = dt.Rows[0]["soh_so_fo"] + "";
                            txtHouseNo.Text = Convert.ToString(dt.Rows[0]["soh_house_no"]);
                            txtLandMark.Text = Convert.ToString(dt.Rows[0]["soh_landmark"]);
                            txtVillage.Text = dt.Rows[0]["soh_village"] + "";
                            txtMandal.Text = dt.Rows[0]["soh_mandal"] + "";
                            txtDistrict.Text = dt.Rows[0]["soh_district"] + "";
                            txtState.Text = dt.Rows[0]["soh_state"] + "";
                            txtPin.Text = dt.Rows[0]["soh_pin"] + "";
                            txtMobileNo.Text = dt.Rows[0]["soh_mobile_number"] + "";
                            txtLanLineNo.Text = dt.Rows[0]["soh_landline"] + "";
                            txtOrdAmt.Text = dt.Rows[0]["soh_order_amount"] + "";
                            txtAdvanceAmt.Text = dt.Rows[0]["soh_order_adv_amt"] + "";
                            //if (btnSave.Enabled == true)
                            //    FillProductData();

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                objData = null;
            }
            return blFind;
        }

        private void cbVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVillage.SelectedIndex > -1)
            {
                if (this.cbVillage.Items[cbVillage.SelectedIndex].ToString() != "")
                {
                    txtVillage.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[1] + "";
                    txtMandal.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[2] + "";
                    txtDistrict.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[3] + "";
                    txtState.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[4] + "";
                    txtPin.Text = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[5] + "";
                    strStateCode = ((System.Data.DataRowView)(this.cbVillage.Items[cbVillage.SelectedIndex])).Row.ItemArray[0] + "";
                    //if (btnSave.Enabled == true)
                    //FillProductData();
                }
            }

        }

        private void txtVillage_Validated(object sender, EventArgs e)
        {
            //txtVillage.Text = strVillage;
        }
        private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            objOrdData = new OrderDB();
            try
            {
                if (cbCustomer.SelectedIndex > -1)
                {
                    if (this.cbCustomer.Items[cbCustomer.SelectedIndex].ToString() != "")
                    {
                        DataSet ds = new DataSet();
                        ds = objOrdData.SalseOrderSearch_Get(((System.Data.DataRowView)(cbCustomer.Items[cbCustomer.SelectedIndex])).Row.ItemArray[1].ToString(), txtVillage.Text.ToString(), txtMandal.Text.ToString(), txtState.Text.ToString());
                        DataTable dt = ds.Tables[0];

                        if (dt.Rows.Count == 1)
                        {
                            strFormerid = Convert.ToString(dt.Rows[0]["soh_farmer_id"]);
                            txtOrderNo.Text = dt.Rows[0]["soh_order_number"] + "";
                            meOrdDate.Value = Convert.ToDateTime(dt.Rows[0]["soh_order_date"]);
                            txtCustomerName.Text = dt.Rows[0]["soh_farmer_name"] + "";
                            txtRelationName.Text = dt.Rows[0]["soh_forg_name"] + "";
                            cbRelation.Text = dt.Rows[0]["soh_so_fo"] + "";
                            txtHouseNo.Text = Convert.ToString(dt.Rows[0]["soh_house_no"]);
                            txtLandMark.Text = Convert.ToString(dt.Rows[0]["soh_landmark"]);
                            txtVillage.Text = dt.Rows[0]["soh_village"] + "";
                            txtMandal.Text = dt.Rows[0]["soh_mandal"] + "";
                            txtDistrict.Text = dt.Rows[0]["soh_district"] + "";
                            txtState.Text = dt.Rows[0]["soh_state"] + "";
                            txtPin.Text = dt.Rows[0]["soh_pin"] + "";
                            txtMobileNo.Text = dt.Rows[0]["soh_mobile_number"] + "";
                            txtLanLineNo.Text = dt.Rows[0]["soh_landline"] + "";
                            txtOrdAmt.Text = dt.Rows[0]["soh_order_amount"] + "";
                            txtAdvanceAmt.Text = dt.Rows[0]["soh_order_adv_amt"] + "";
                        }
                        ds = null;
                        dt = null;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                objData = null;

            }
        }
        
        private void ClearVillageDetails()
        {
            txtVillage.Text = "";
            txtMandal.Text = "";
            txtDistrict.Text = "";
            txtState.Text = "";
            txtPin.Text = "";
        }

        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbBranch.SelectedIndex > -1)
            //{
            //    strBranchCode = ((System.Data.DataRowView)(cbBranch.SelectedItem)).Row.ItemArray[0].ToString();
            //    string[] strArr = strBranchCode.Split('@');
            //    strStateCode = strArr[1].ToString();
            //    strBranchCode = strArr[0].ToString();
            //    ClearForm(this);
            //    //FillProductData();
            //}
        }

        private void cbEcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEcode.SelectedIndex > -1)
            {
                strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
            }
        }
        
        private void ClearForm(System.Windows.Forms.Control parent)
        {
            strOrderNo = "0";
            strFormerid = "";
            strVillage = string.Empty;
            foreach (System.Windows.Forms.Control ctrControl in parent.Controls)
            {
                //Loop through all controls 
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.TextBox)))
                {
                    //Check to see if it's a textbox 
                    if (((System.Windows.Forms.TextBox)ctrControl).Name != "txtEcodeSearch")
                        ((System.Windows.Forms.TextBox)ctrControl).Text = string.Empty;

                    //If it is then set the text to String.Empty (empty textbox) 
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.MaskedTextBox)))
                {

                    //Check to see if it's a textbox 
                    if (((System.Windows.Forms.MaskedTextBox)ctrControl).Name != "meOrderDate")
                        ((System.Windows.Forms.MaskedTextBox)ctrControl).Text = string.Empty;
                    //If it is then set the text to String.Empty (empty textbox) 
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RichTextBox)))
                {
                    ((System.Windows.Forms.RichTextBox)ctrControl).Text = string.Empty;
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.ComboBox)))
                {
                    cbRelation.SelectedIndex = 0;
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.CheckBox)))
                {
                    ((System.Windows.Forms.CheckBox)ctrControl).Checked = false;
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.RadioButton)))
                {
                    ((System.Windows.Forms.RadioButton)ctrControl).Checked = false;
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.DataGridView)))
                {
                    ((System.Windows.Forms.DataGridView)ctrControl).Rows.Clear();
                    //FillProductData();

                }
                //else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.DateTimePicker)))
                //{
                //    ((System.Windows.Forms.DateTimePicker)ctrControl).Text = DateTime.Now.Date.ToString("dd/MM/yy");
                //}
                if (ctrControl.Controls.Count > 0)
                {
                    ClearForm(ctrControl);
                }
            }
            //lblCreditSale.Visible = false;
            lblCustomerId.Text = "";
            //cbEcode.SelectedIndex = -1;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            txtMobileNo.Text = "";
        }

        private void txtOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsLetterOrDigit(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;

            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void txtOrderNo_Validated(object sender, EventArgs e)
        {
            if (txtOrderNo.Text.ToString().Trim().Length > 0)
            {
                //int intOrdNo = Convert.ToInt32(Convert.ToString(txtOrderNo.Text));
                //ClearForm(this);
                //txtOrderNo.Text = intOrdNo.ToString();
                FillOrderData(Convert.ToInt32(txtOrderNo.Text.ToString().Trim()).ToString("00000"),"ORDERNO");
            }
        }

        private void meOrderDate_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btnVSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("SalesOrders");
            VSearch.objSalseORder = this;
            VSearch.ShowDialog();
        }

        private void SearchItems(ComboBox acComboBox, ref KeyPressEventArgs e)
        {
            int selectionStart = acComboBox.SelectionStart;
            int selectionLength = acComboBox.SelectionLength;
            int selectionEnd = selectionStart + selectionLength;
            int index;
            StringBuilder sb = new StringBuilder();

            sb.Append(acComboBox.Text.Substring(0, selectionStart))
                .Append(e.KeyChar.ToString())
                .Append(acComboBox.Text.Substring(selectionEnd));

            index = acComboBox.FindString(sb.ToString());

            if (index == -1)
                e.Handled = false;
            else
            {
                acComboBox.SelectedIndex = index;
                acComboBox.Select(selectionStart + 1, acComboBox.Text.Length - (selectionStart + 1));
                e.Handled = true;
            }
        }

        private void gvProductDetails_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            double actualPrice;
            double plusAmt;
            double lessAmt;
            if (e.ColumnIndex == 7)
            {
                if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) != "")
                {
                    if (Convert.ToInt32(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value) >= 0 && Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value) > 0)
                    {
                        actualPrice = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells[5].Value) * (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value));
                        plusAmt = actualPrice + (actualPrice * 10) / 100;
                        lessAmt = actualPrice - (actualPrice * 10) / 100;
                        if (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Amount"].EditedFormattedValue) > plusAmt || Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Amount"].EditedFormattedValue) < lessAmt)
                        {
                            MessageBox.Show("Please check amount !", "Product amount");
                            e.Cancel = true;
                        }
                    }
                }
                txtOrdAmt.Text = GetInvoiceAmt().ToString("f");
            }

        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            ProductSearchAll PSearch = new ProductSearchAll("SalesOrders");
            PSearch.objFrmSalseOrder = this;
            PSearch.ShowDialog();
        }

        private double GetInvoiceAmt()
        {
            double dbInvAmt = 0;
            for (int i = 0; i < gvProductDetails.Rows.Count; i++)
            {
                if (gvProductDetails.Rows[i].Cells["Amount"].Value.ToString() != "")
                {
                    if (Convert.ToDouble(gvProductDetails.Rows[i].Cells["Amount"].Value.ToString()) >= 1)
                        dbInvAmt += Convert.ToDouble(gvProductDetails.Rows[i].Cells["Amount"].Value);

                }

            }

            return dbInvAmt;
        }

        private void txtAdvanceAmt_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtReceivedAmt_KeyUp(object sender, KeyEventArgs e)
        {
            //if (txtAdvanceAmt.Text == "")
            //    txtAdvanceAmt.Text = "0.00";
            //if (txtReceivedAmt.Text == "")
            //    txtReceivedAmt.Text = "0.00";
            //txtBalAmt.Text = Convert.ToDouble(Convert.ToDouble(txtInvAmt.Text) - (Convert.ToDouble(txtAdvanceAmt.Text) + Convert.ToDouble(txtReceivedAmt.Text))).ToString("f");

        }

        private void txtAdvanceAmt_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtReceivedAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            txt_KeyPress(sender, e);
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.ToString().Trim().Length > 0)
                EcodeSearch();
            else
                FillEmployeeData();
        }

        private void EcodeSearch()
        {
            objData = new InvoiceDB();
            DataSet dsEmp = null;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cbEcode.DataSource = null;
                cbEcode.Items.Clear();
                dsEmp = objData.InvLevelEcodeSearch_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, txtEcodeSearch.Text.ToString());
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
                objData = null;
                Cursor.Current = Cursors.Default;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvProductDetails.Rows.Clear();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtArNumber_Validated(object sender, EventArgs e)
        {
            if (txtArNumber.Text.ToString().Trim().Length > 0)
            {
                //int intOrdNo = Convert.ToInt32(Convert.ToString(txtOrderNo.Text));
                //ClearForm(this);
                //txtOrderNo.Text = intOrdNo.ToString();
                FillOrderData(txtArNumber.Text.ToString().Trim(),"ARNO");
            }
        }
    }
}
