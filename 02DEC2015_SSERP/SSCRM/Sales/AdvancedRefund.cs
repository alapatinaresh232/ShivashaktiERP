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
    public partial class AdvancedRefund : Form
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
        private string strRefFinYear = string.Empty;
        private string strRefDocMonth = string.Empty;
        private string strECode = string.Empty;
        private bool blCustomerSearch = false;
        private bool blIsCellQty = true;
        private string strCreditSale = "NO";
        private string docMonth = CommonData.DocMonth;
        public int iUpdateVal = 0;
        private bool isUpdate = false;
        public AdvancedRefund()
        {
            InitializeComponent();
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            ClearForm(this);
            FillFinYear();
            cmbFinYear.Text = CommonData.FinancialYear;
            cmbDocMonth.Text = CommonData.DocMonth;
            cmbFinYear_SelectedIndexChanged(null, null);
            cmbDocMonth.SelectedIndex = 0;
            txtDocMonth.Text = CommonData.DocMonth;
            //FillEmployeeData();
            //cbEcode.SelectedIndex = -1;
            cbRelation.SelectedIndex = 0;
            cmbPayMode.SelectedIndex = 0;
            cmbNullfy.SelectedIndex = 0;
            dtRefDate.Value = System.DateTime.Now;
            dtRefundDt.Value = System.DateTime.Now;
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            objSQLDB = new SQLDB();
            string strSql = "SELECT MAX(ISNULL(SOH_AR_NUMBER,0))+1 FROM SALES_ORD_HEAD WHERE SOH_BRANCH_CODE='" + CommonData.BranchCode + 
                                "' AND SOH_FIN_YEAR='" + cmbFinYear.Text.ToString() + "'";
            txtArNumber.Text = objSQLDB.ExecuteDataSet(strSql).Tables[0].Rows[0][0].ToString();
            objSQLDB = null;
            GetRefundNo();
            cmbNullfy.SelectedIndex = 1;
            meOrdDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            txtDocMonth.Text = CommonData.DocMonth;
        }

        private void FillFinYear()
        {
            objSQLDB = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT FY_COMPANY_CODE," +
                                        "FY_FIN_YEAR DisplayMember," +
                                        "FY_FIN_YEAR ValueMember," +
                                        "FY_START_DATE," +
                                        "FY_END_DATE" +
                                        " FROM FIN_YEAR" +
                                        " WHERE FY_COMPANY_CODE = '" + CommonData.CompanyCode + "'" +
                                        " ORDER BY FY_START_DATE DESC";

                dt = objSQLDB.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    
                    cmbFinYear.DataSource = dt;
                    cmbFinYear.DisplayMember = "DisplayMember";
                    cmbFinYear.ValueMember = "ValueMember";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
                dt = null;
            }
            cmbFinYear.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm(this);
            txtOrderNo.Text = string.Empty;
            meOrdDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            btnSave.Enabled = true;
            isUpdate = false;
            GetRefundNo();
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
                    txtrefVillage.Text = dtVillage.Rows[0]["PANCHAYAT"].ToString();
                    txtrefMandal.Text = dtVillage.Rows[0]["Mandal"].ToString();
                    txtrefDistrict.Text = dtVillage.Rows[0]["District"].ToString();
                    txtrefState.Text = dtVillage.Rows[0]["State"].ToString();
                    txtrefPin.Text = dtVillage.Rows[0]["PIN"].ToString();
                    strStateCode = dtVillage.Rows[0]["CDState"].ToString();
                }
                else if (dtVillage.Rows.Count > 1)
                {
                    txtrefVillage.Text = "";
                    txtrefMandal.Text = "";
                    txtrefDistrict.Text = "";
                    txtrefState.Text = "";
                    txtrefPin.Text = "";
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

        //private void FillEmployeeData()
        //{
        //    objData = new InvoiceDB();
        //    DataSet dsEmp = null;
        //    try
        //    {
        //        dsEmp = objData.GetEcodeList(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth);
        //        DataTable dtEmp = dsEmp.Tables[0];
        //        if (dtEmp.Rows.Count > 0)
        //        {
        //            cbEcode.DataSource = dtEmp;
        //            cbEcode.DisplayMember = "ENAME";
        //            cbEcode.ValueMember = "ECODE";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        if (cbEcode.SelectedIndex > -1)
        //        {
        //            cbEcode.SelectedIndex = 0;
        //            strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
        //        }
        //        objData = null;
        //        Cursor.Current = Cursors.Default;
        //    }
        //}

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
                        //cbCustomer.DataSource = null;
                        //cbCustomer.DataBindings.Clear();
                        //cbCustomer.Items.Clear();
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
            try
            {
                if (txtRefundAmtRecBy.Text == "")
                {
                    MessageBox.Show("Please enter Refund Amout Received By Name", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtrefVillage.Text == "")
                {
                    MessageBox.Show("Please enter Address", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtRefundAmt.Text == "")
                {
                    MessageBox.Show("Please enter Refund Amount", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbAdjustment.Text == "ADJUSTED")
                {
                    try
                    { Convert.ToInt32(txtAdjustedOrder.Text); }
                    catch
                    { txtAdjustedOrder.Text = "0"; }

                    if (Convert.ToInt32(txtAdjustedOrder.Text) == 0 || txtAdjustedOrder.Text.Length == 0)
                    {
                        MessageBox.Show("Please Enter the Adjusted OrderNo", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                int iInvNo = 0;

                try
                { iInvNo = Convert.ToInt32(txtAdjustedInvoiceNo.Text); }
                catch
                {
                    txtAdjustedInvoiceNo.Text = "0";
                    iInvNo = 0;
                }
                try { Convert.ToDouble(txtRefundAmt.Text); }
                catch { txtRefundAmt.Text = "0"; }
                try { Convert.ToDouble(txtOrdAmt.Text); }
                catch { txtOrdAmt.Text = "0"; }
                try { txtRefBalAmt.Text = (Convert.ToDouble(txtOrdAmt.Text) - Convert.ToDouble(txtRefundAmt.Text)).ToString("f"); }
                catch { txtRefBalAmt.Text = "0"; }
                string sAdjustmentStatus = "";
                if (cmbAdjustment.Text == "ADJUSTED")
                    sAdjustmentStatus = "ADJUSTED";
                else
                    sAdjustmentStatus = "RETURNED";

                objSQLDB = new SQLDB();
                string strSqlCnt = "SELECT * FROM SALES_ORD_ADVANCE_REFUND WHERE SOAR_REFUND_NUMBER=" + txtRefundNo.Text +
                    " AND SOAR_BRANCH_CODE='" + CommonData.BranchCode + "' AND SOAR_FIN_YEAR='" + cmbFinYear.Text.ToString() + "'";
                DataSet ds = objSQLDB.ExecuteDataSet(strSqlCnt);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string sSqlStr = "UPDATE SALES_ORD_ADVANCE_REFUND SET SOAR_REFUND_DATE='" +
                        "" + Convert.ToDateTime(dtRefundDt.Value).ToString("dd/MMM/yyyy") +
                        "',SOAR_REFUND_RECD_BY='" + txtRefundAmtRecBy.Text +
                        "',SOAR_REFUND_RCVR_VILL='" + txtrefVillage.Text +
                        "',SOAR_REFUND_RCVR_MAND='" + txtrefMandal.Text +
                        "',SOAR_REFUND_RCVR_DIST='" + txtrefDistrict.Text +
                        "',SOAR_REFUND_RCVR_STATE='" + txtrefState.Text +
                        "',SOAR_REFUND_RCVR_PIN='" + txtrefPin.Text +
                        "',SOAR_REFUND_RCVR_PHONE='" + txtPhoneNo.Text +
                        "',SOAR_REFUND_PAY_MODE='" + cmbPayMode.Text +
                        "',SOAR_REFUND_REFR_NO='" + txtRefNumber.Text +
                        "',SOAR_REFUND_REFR_DATE='" + Convert.ToDateTime(dtRefundDt.Value).ToString("dd/MMM/yyyy") +
                        "',SOAR_REFUND_AMOUNT='" + txtRefundAmt.Text +
                        "',SOAR_BALANCE_AMOUNT='" + txtRefBalAmt.Text +
                        "',SOAR_BALANCE_NULLIFY='" + cmbNullfy.Text +
                        "',SOAR_REMARKS='" + txtRemarks.Text +
                        "',SOAR_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                        "',SOAR_LAST_MODIFIED_DATE=GETDATE(),SOAR_REFUNDED_FIN_YEAR='" + CommonData.FinancialYear +
                        "',SOAR_REFUNDED_DOC_MONTH='" + CommonData.DocMonth +
                        "',SOAR_ADJUSTMENT_FLAG='" + sAdjustmentStatus + "'";
                    //if (cmbAdjustment.Text == "ADJUSTED")
                    //{
                        sSqlStr += ",SOAR_ADJUST_TO_INVOICE_NO=" + iInvNo + "";
                    //}
                    sSqlStr += ",SOAR_ADJUST_TO_ORDER_NO='" + txtAdjustedOrder.Text +
                        "' WHERE SOAR_COMPANY_CODE='" + CommonData.CompanyCode +
                        "' AND SOAR_STATE_CODE='" + CommonData.StateCode + "' AND SOAR_FIN_YEAR=" +
                        "'" + cmbFinYear.Text.ToString() + "' AND SOAR_REFUND_NUMBER=" + txtRefundNo.Text;
                    int intRec = objSQLDB.ExecuteSaveData(sSqlStr);
                    if (intRec > 0)
                        MessageBox.Show("This Record is Updated Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("This Record is Not Updated ", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    GetRefundNo();
                    objSQLDB = new SQLDB();
                    string sSqlStr = "INSERT INTO SALES_ORD_ADVANCE_REFUND(SOAR_COMPANY_CODE,SOAR_STATE_CODE,SOAR_BRANCH_CODE" +
                        ",SOAR_FIN_YEAR,SOAR_ORDER_DATE,SOAR_ORDER_NUMBER,SOAR_AR_NUMBER,SOAR_REFUND_NUMBER," +
                        "SOAR_REFUND_DATE,SOAR_REFUND_RECD_BY,SOAR_REFUND_RCVR_VILL,SOAR_REFUND_RCVR_MAND" +
                        ",SOAR_REFUND_RCVR_DIST,SOAR_REFUND_RCVR_STATE,SOAR_REFUND_RCVR_PIN,SOAR_REFUND_RCVR_PHONE,SOAR_REFUND_PAY_MODE," +
                        "SOAR_REFUND_REFR_NO,SOAR_REFUND_REFR_DATE,SOAR_REFUND_AMOUNT,SOAR_BALANCE_AMOUNT" +
                        ",SOAR_BALANCE_NULLIFY,SOAR_REMARKS,SOAR_CREATED_BY,SOAR_AUTHORIZED_BY,SOAR_CREATED_DATE" +
                        ",SOAR_REFUNDED_FIN_YEAR,SOAR_REFUNDED_DOC_MONTH,SOAR_ADJUSTMENT_FLAG,SOAR_ADJUST_TO_INVOICE_NO,SOAR_ADJUST_TO_ORDER_NO) VALUES " +
                        "('" + CommonData.CompanyCode + "', '" + CommonData.StateCode + "', '" + CommonData.BranchCode +
                        "','" + cmbFinYear.Text.ToString() + "','" + Convert.ToDateTime(meOrdDate.Value).ToString("dd/MMM/yyyy") +
                        "','" + Convert.ToInt32(txtOrderNo.Text).ToString("00000") + "'," + txtArNumber.Text + "," + txtRefundNo.Text +
                        ",'" + Convert.ToDateTime(dtRefundDt.Value).ToString("dd/MMM/yyyy") + "','" + txtRefundAmtRecBy.Text +
                        "','" + txtrefVillage.Text + "','" + txtrefMandal.Text + "','" + txtrefDistrict.Text +
                        "','" + txtrefState.Text + "','" + txtrefPin.Text + "','" + txtPhoneNo.Text + "','" + cmbPayMode.Text +
                        "','" + txtRefNumber.Text + "','" + Convert.ToDateTime(dtRefundDt.Value).ToString("dd/MMM/yyyy") +
                        "'," + txtRefundAmt.Text + "," + txtRefBalAmt.Text + ",'" + cmbNullfy.Text + "','" + txtRemarks.Text +
                        "','" + CommonData.LogUserId + "','" + CommonData.LogUserId +
                        "',GETDATE(),'" + CommonData.FinancialYear + "','" + CommonData.DocMonth + "','" + sAdjustmentStatus +
                        "'," + iInvNo + ",'" + txtAdjustedOrder.Text + "')";
                    int intRec = objSQLDB.ExecuteSaveData(sSqlStr);
                    if (intRec > 0)
                        MessageBox.Show("This Record is Inserted Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("This Record is Not Insert", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                objSQLDB = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ClearForm(this);
        }
        //private void txtCustomerSearch_KeyUp(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (txtCustomerSearch.Text.Length > 0)
        //        {
        //            blCustomerSearch = true;
        //            if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
        //            {
        //                if (FindInputCustomerSearch() == false)
        //                {
        //                    FillCustomerFarmerData(txtCustomerSearch.Text, "");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    if (e.KeyValue == 8)
        //    {
        //        if (this.txtCustomerSearch.TextLength >= 2)
        //            FillCustomerFarmerData(Convert.ToString(txtCustomerSearch.Text.Trim()), "");
        //        this.txtCustomerName.SelectionStart = this.txtCustomerSearch.TextLength;
        //    }
        //}
        private void FillOrderData(string sOrdNo, string sType)
        {
            Hashtable ht = new Hashtable();
            try
            {
                objOrdData = new OrderDB();
                if (sType == "ORDERNO")
                    ht = objOrdData.GetSalseOrderData(CommonData.CompanyCode, CommonData.BranchCode, cmbDocMonth.Text.ToString(), sOrdNo);
                else
                    ht = objOrdData.GetSalseOrderDataByARNumber(CommonData.CompanyCode, CommonData.BranchCode, cmbDocMonth.Text.ToString(), txtArNumber.Text);
                DataTable dtInvH = (DataTable)ht["OrdHead"];
                DataTable dtRefund = (DataTable)ht["REFUND"];
                DataTable dtInvD = (DataTable)ht["OrdDetail"];
                if (dtInvH.Rows.Count > 0)
                {
                    ClearForm(this);
                    isUpdate = true;
                    strRefFinYear = dtInvH.Rows[0]["SOH_FIN_YEAR"] + "";
                    cmbFinYear.Text = dtInvH.Rows[0]["SOH_FIN_YEAR"] + "";
                    strRefDocMonth = dtInvH.Rows[0]["SOH_DOCUMENT_MONTH"] + "";
                    strOrderNo = dtInvH.Rows[0]["soh_order_number"] + "";
                    strFormerid = Convert.ToString(dtInvH.Rows[0]["soh_farmer_id"] + "");
                    strECode = dtInvH.Rows[0]["soh_eora_code"] + "";
                    txtEcodeSearch.Text = strECode;                    
                    txtEcodeSearch_KeyUp(null, null);
                    docMonth = dtInvH.Rows[0]["SOH_DOCUMENT_MONTH"] + "";
                    cmbDocMonth.Text = dtInvH.Rows[0]["SOH_DOCUMENT_MONTH"] + "";
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
                    txtRefundAmt.Text = dtInvH.Rows[0]["soh_order_amount"] + "";
                    lblCustomerId.Text = strFormerid;

                    FillInvoiceDetail(dtInvD);
                    if (dtRefund.Rows.Count > 0)
                    {
                        txtRefundNo.Text = dtRefund.Rows[0]["SOAR_REFUND_NUMBER"].ToString();
                        dtRefundDt.Value = Convert.ToDateTime(dtRefund.Rows[0]["SOAR_REFUND_DATE"]);
                        txtRefundAmtRecBy.Text = dtRefund.Rows[0]["SOAR_REFUND_RECD_BY"].ToString();
                        txtrefVillage.Text = dtRefund.Rows[0]["SOAR_REFUND_RCVR_VILL"].ToString();
                        txtrefMandal.Text = dtRefund.Rows[0]["SOAR_REFUND_RCVR_MAND"].ToString();
                        txtrefDistrict.Text = dtRefund.Rows[0]["SOAR_REFUND_RCVR_DIST"].ToString();
                        txtrefState.Text = dtRefund.Rows[0]["SOAR_REFUND_RCVR_STATE"].ToString();
                        txtrefPin.Text = dtRefund.Rows[0]["SOAR_REFUND_RCVR_PIN"].ToString();
                        txtPhoneNo.Text = dtRefund.Rows[0]["SOAR_REFUND_RCVR_PHONE"].ToString();
                        txtRefundAmt.Text = dtRefund.Rows[0]["SOAR_REFUND_AMOUNT"].ToString();
                        txtRefBalAmt.Text = dtRefund.Rows[0]["SOAR_BALANCE_AMOUNT"].ToString();
                        cmbNullfy.Text = dtRefund.Rows[0]["SOAR_BALANCE_NULLIFY"].ToString();
                        cmbPayMode.Text = dtRefund.Rows[0]["SOAR_REFUND_PAY_MODE"].ToString();
                        txtRefNumber.Text = dtRefund.Rows[0]["SOAR_REFUND_REFR_NO"].ToString();
                        dtRefDate.Value = Convert.ToDateTime(dtRefund.Rows[0]["SOAR_REFUND_REFR_DATE"]);
                        txtRemarks.Text = dtRefund.Rows[0]["SOAR_REMARKS"].ToString();
                        cmbAdjustment.Text = dtRefund.Rows[0]["SOAR_ADJUSTMENT_FLAG"].ToString();
                        txtAdjustedOrder.Text = dtRefund.Rows[0]["SOAR_ADJUST_TO_ORDER_NO"].ToString();
                        txtAdjustedInvoiceNo.Text = dtRefund.Rows[0]["SOAR_ADJUST_TO_INVOICE_NO"].ToString();
                        if (CommonData.LogUserId == "ADMIN" || CommonData.LogUserRole == "MANAGEMENT" || Convert.ToInt32(CommonData.LogUserBackDays) > Convert.ToInt32((Convert.ToDateTime(CommonData.CurrentDate) - Convert.ToDateTime(dtRefund.Rows[0]["SOAR_CREATED_DATE"])).TotalDays))
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
                        GetRefundNo();
                        cmbAdjustment.SelectedIndex = 1;
                        dtRefDate.Value = Convert.ToDateTime(CommonData.DocMonth);
                        txtRefundAmtRecBy.Text = dtInvH.Rows[0]["soh_farmer_name"] + "";
                        txtrefVillage.Text = dtInvH.Rows[0]["soh_village"] + "";
                        txtrefMandal.Text = dtInvH.Rows[0]["soh_mandal"] + "";
                        txtrefDistrict.Text = dtInvH.Rows[0]["soh_district"] + "";
                        txtrefState.Text = dtInvH.Rows[0]["soh_state"] + "";
                        txtrefPin.Text = dtInvH.Rows[0]["soh_pin"] + "";
                        txtPhoneNo.Text = dtInvH.Rows[0]["soh_mobile_number"] + "";
                        cmbPayMode.SelectedIndex = 0;
                        cmbNullfy.SelectedIndex = 1;
                        btnSave.Enabled = true;
                        btnDelete.Enabled = false;
                    }
                    iUpdateVal = 1;
                    //if (CommonData.LogUserRole == "MANAGEMENT" || CommonData.LogUserId == "ADMIN")
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
                else
                {
                    isUpdate = false;
                    strRefFinYear = "";
                    strRefDocMonth = "";
                    iUpdateVal = 0;
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                    docMonth = CommonData.DocMonth;
                    txtArNumber.Text = GenerateARNumber().ToString();
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                    btnCancel_Click(null, null);
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
            string strSql = "SELECT MAX(ISNULL(SOH_AR_NUMBER,0))+1 FROM SALES_ORD_HEAD WHERE SOH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SOH_FIN_YEAR='" + CommonData.FinancialYear + "'";
            iArNumber = Convert.ToInt32(objSQLDB.ExecuteDataSet(strSql).Tables[0].Rows[0][0].ToString());
            return iArNumber;
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
                if (txtRefVillSearch.Text.Length == 0)
                {
                    cbRefVillage.DataSource = null;
                    cbRefVillage.DataBindings.Clear();
                    cbRefVillage.Items.Clear();
                    if (btnSave.Enabled == true)
                        ClearVillageDetails();
                }
                else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                {
                    if (FindInputAddressSearch() == false)
                    {
                        FillAddressData(txtRefVillSearch.Text);
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
            //cbVillage.DataBindings.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
                dataTable.Rows.Add(new String[] { dt.Rows[i]["CDState"] + 
                     "", dt.Rows[i]["PANCHAYAT"] + 
                     "", dt.Rows[i]["MANDAL"] + 
                     "", dt.Rows[i]["DISTRICT"] + 
                     "", dt.Rows[i]["STATE"] + "", dt.Rows[i]["PIN"] + ""});


            cbRefVillage.DataSource = dataTable;
            cbRefVillage.DisplayMember = "Panchayath";
            cbRefVillage.ValueMember = "StateID";
        }
        private void FillCustomerComboBox(DataTable dt)
        {
            DataTable dataCustomer = new DataTable("Customer");
            dataCustomer.Columns.Add("farmer_ID", typeof(String));
            dataCustomer.Columns.Add("farmer_name", typeof(String));
            dataCustomer.Columns.Add("forg_name", typeof(String));
            dataCustomer.Columns.Add("village", typeof(String));

            //cbCustomer.DataBindings.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
                dataCustomer.Rows.Add(new String[] { dt.Rows[i]["SOH_FARMER_ID"] + 
                     "", dt.Rows[i]["SOH_FARMER_NAME"] + "", dt.Rows[i]["SOH_FORG_NAME"] + 
                     "", dt.Rows[i]["SOH_VILLAGE"] + ""});


            //cbCustomer.DataSource = dataCustomer;
            //cbCustomer.DisplayMember = "farmer_name";
            //cbCustomer.ValueMember = "farmer_ID";
        }

        private bool FindInputAddressSearch()
        {
            bool blFind = false;
            try
            {
                for (int i = 0; i < this.cbRefVillage.Items.Count; i++)
                {
                    string strItem = ((System.Data.DataRowView)(this.cbRefVillage.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
                    if (strItem.IndexOf(txtRefVillSearch.Text) > -1)
                    {
                        blFind = true;
                        cbRefVillage.SelectedIndex = i;
                        txtrefVillage.Text = ((System.Data.DataRowView)(this.cbRefVillage.Items[i])).Row.ItemArray[1] + "";
                        txtrefMandal.Text = ((System.Data.DataRowView)(this.cbRefVillage.Items[i])).Row.ItemArray[2] + "";
                        txtrefDistrict.Text = ((System.Data.DataRowView)(this.cbRefVillage.Items[i])).Row.ItemArray[3] + "";
                        txtrefState.Text = ((System.Data.DataRowView)(this.cbRefVillage.Items[i])).Row.ItemArray[4] + "";
                        txtrefPin.Text = ((System.Data.DataRowView)(this.cbRefVillage.Items[i])).Row.ItemArray[5] + "";
                        strStateCode = ((System.Data.DataRowView)(this.cbRefVillage.Items[i])).Row.ItemArray[0] + "";
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
        //private bool FindInputCustomerSearch()
        //{
        //    bool blFind = false;
        //    objOrdData = new OrderDB();
        //    try
        //    {
        //        for (int i = 0; i < this.cbCustomer.Items.Count; i++)
        //        {
        //            string strItem = ((System.Data.DataRowView)(this.cbCustomer.Items[i])).Row.ItemArray[1] + "".ToString().Trim();
        //            if (strItem.IndexOf(txtCustomerSearch.Text) > -1)
        //            {
        //                DataSet ds = new DataSet();
        //                blFind = true;
        //                cbCustomer.SelectedIndex = i;
        //                ds = objOrdData.SalseOrderSearch_Get(strItem, txtVillage.Text.ToString(), txtMandal.Text.ToString(), txtState.Text.ToString());
        //                DataTable dt = ds.Tables[0];

        //                if (dt.Rows.Count == 1)
        //                {
        //                    strFormerid = Convert.ToString(dt.Rows[0]["soh_farmer_id"]);
        //                    txtOrderNo.Text = dt.Rows[0]["soh_order_number"] + "";
        //                    meOrdDate.Value = Convert.ToDateTime(dt.Rows[0]["soh_order_date"]);
        //                    txtCustomerName.Text = dt.Rows[0]["soh_farmer_name"] + "";
        //                    txtRelationName.Text = dt.Rows[0]["soh_forg_name"] + "";
        //                    cbRelation.Text = dt.Rows[0]["soh_so_fo"] + "";
        //                    txtHouseNo.Text = Convert.ToString(dt.Rows[0]["soh_house_no"]);
        //                    txtLandMark.Text = Convert.ToString(dt.Rows[0]["soh_landmark"]);
        //                    txtVillage.Text = dt.Rows[0]["soh_village"] + "";
        //                    txtMandal.Text = dt.Rows[0]["soh_mandal"] + "";
        //                    txtDistrict.Text = dt.Rows[0]["soh_district"] + "";
        //                    txtState.Text = dt.Rows[0]["soh_state"] + "";
        //                    txtPin.Text = dt.Rows[0]["soh_pin"] + "";
        //                    txtMobileNo.Text = dt.Rows[0]["soh_mobile_number"] + "";
        //                    txtLanLineNo.Text = dt.Rows[0]["soh_landline"] + "";
        //                    txtOrdAmt.Text = dt.Rows[0]["soh_order_amount"] + "";
        //                    txtAdvanceAmt.Text = dt.Rows[0]["soh_order_adv_amt"] + "";

        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString());
        //    }
        //    finally
        //    {
        //        objData = null;
        //    }
        //    return blFind;
        //}

        //private void cbVillage_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cbRefVillage.SelectedIndex > -1)
        //    {
        //        if (this.cbRefVillage.Items[cbRefVillage.SelectedIndex].ToString() != "")
        //        {
        //            txtrefVillage.Text = ((System.Data.DataRowView)(this.cbRefVillage.Items[cbRefVillage.SelectedIndex])).Row.ItemArray[1] + "";
        //            txtrefMandal.Text = ((System.Data.DataRowView)(this.cbRefVillage.Items[cbRefVillage.SelectedIndex])).Row.ItemArray[2] + "";
        //            txtrefDistrict.Text = ((System.Data.DataRowView)(this.cbRefVillage.Items[cbRefVillage.SelectedIndex])).Row.ItemArray[3] + "";
        //            txtrefState.Text = ((System.Data.DataRowView)(this.cbRefVillage.Items[cbRefVillage.SelectedIndex])).Row.ItemArray[4] + "";
        //            txtrefPin.Text = ((System.Data.DataRowView)(this.cbRefVillage.Items[cbRefVillage.SelectedIndex])).Row.ItemArray[5] + "";
        //            strStateCode = ((System.Data.DataRowView)(this.cbRefVillage.Items[cbRefVillage.SelectedIndex])).Row.ItemArray[0] + "";
        //        }
        //    }
        //}
        //private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    objOrdData = new OrderDB();
        //    try
        //    {
        //        if (cbCustomer.SelectedIndex > -1)
        //        {
        //            if (this.cbCustomer.Items[cbCustomer.SelectedIndex].ToString() != "")
        //            {
        //                DataSet ds = new DataSet();
        //                ds = objOrdData.SalseOrderSearch_Get(((System.Data.DataRowView)(cbCustomer.Items[cbCustomer.SelectedIndex])).Row.ItemArray[1].ToString(), txtVillage.Text.ToString(), txtMandal.Text.ToString(), txtState.Text.ToString());
        //                DataTable dt = ds.Tables[0];

        //                if (dt.Rows.Count == 1)
        //                {
        //                    strFormerid = Convert.ToString(dt.Rows[0]["soh_farmer_id"]);
        //                    txtOrderNo.Text = dt.Rows[0]["soh_order_number"] + "";
        //                    meOrdDate.Value = Convert.ToDateTime(dt.Rows[0]["soh_order_date"]);
        //                    txtCustomerName.Text = dt.Rows[0]["soh_farmer_name"] + "";
        //                    txtRelationName.Text = dt.Rows[0]["soh_forg_name"] + "";
        //                    cbRelation.Text = dt.Rows[0]["soh_so_fo"] + "";
        //                    txtHouseNo.Text = Convert.ToString(dt.Rows[0]["soh_house_no"]);
        //                    txtLandMark.Text = Convert.ToString(dt.Rows[0]["soh_landmark"]);
        //                    txtVillage.Text = dt.Rows[0]["soh_village"] + "";
        //                    txtMandal.Text = dt.Rows[0]["soh_mandal"] + "";
        //                    txtDistrict.Text = dt.Rows[0]["soh_district"] + "";
        //                    txtState.Text = dt.Rows[0]["soh_state"] + "";
        //                    txtPin.Text = dt.Rows[0]["soh_pin"] + "";
        //                    txtMobileNo.Text = dt.Rows[0]["soh_mobile_number"] + "";
        //                    txtLanLineNo.Text = dt.Rows[0]["soh_landline"] + "";
        //                    txtOrdAmt.Text = dt.Rows[0]["soh_order_amount"] + "";
        //                    txtAdvanceAmt.Text = dt.Rows[0]["soh_order_adv_amt"] + "";
        //                }
        //                ds = null;
        //                dt = null;

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString());
        //    }
        //    finally
        //    {
        //        objData = null;

        //    }
        //}

        private void ClearVillageDetails()
        {
            txtrefVillage.Text = "";
            txtrefMandal.Text = "";
            txtrefDistrict.Text = "";
            txtrefState.Text = "";
            txtrefPin.Text = "";
        }
        //private void cbEcode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cbEcode.SelectedIndex > -1)
        //    {
        //        strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
        //    }
        //}

        private void ClearForm(System.Windows.Forms.Control parent)
        {
            
            foreach (System.Windows.Forms.Control ctrControl in parent.Controls)
            {
                //Loop through all controls 
                if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.TextBox)))
                {
                    //Check to see if it's a textbox 
                    if (((System.Windows.Forms.TextBox)ctrControl).Name != "txtOrderNo")
                        ((System.Windows.Forms.TextBox)ctrControl).Text = string.Empty;

                    //If it is then set the text to String.Empty (empty textbox) 
                }
                else if (object.ReferenceEquals(ctrControl.GetType(), typeof(System.Windows.Forms.MaskedTextBox)))
                {
                    if (((System.Windows.Forms.MaskedTextBox)ctrControl).Name != "meOrderDate")
                        ((System.Windows.Forms.MaskedTextBox)ctrControl).Text = string.Empty;
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
                }
                if (ctrControl.Controls.Count > 0)
                {
                    ClearForm(ctrControl);
                }
            }
            strOrderNo = "0";
            strFormerid = "";
            txtOrderNo.Text = "";
            strVillage = string.Empty;
            cmbFinYear.Text = CommonData.FinancialYear;
            cmbDocMonth.Text = CommonData.DocMonth;
            lblCustomerId.Text = "";
            txtDocMonth.Text = CommonData.DocMonth;
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
                try { Convert.ToInt32(txtOrderNo.Text).ToString("00000"); }
                catch { txtOrderNo.Text = "0"; }
                txtOrderNo.Text = Convert.ToInt32(txtOrderNo.Text).ToString("00000");
                FillOrderData(Convert.ToInt32(txtOrderNo.Text).ToString("00000").Trim(), "ORDERNO");
            }
        }

        private void btnVSearch_Click(object sender, EventArgs e)
        {
            VillageSearch VSearch = new VillageSearch("AdvancedRefund");
            VSearch.objAdvanced = this;
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
        private void txtReceivedAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            txt_KeyPress(sender, e);
        }
        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.ToString().Trim().Length > 0)
                EcodeSearch();
            //else
                //FillEmployeeData();
        }

        private void EcodeSearch()
        {

            objSQLDB = new SQLDB();
            if (txtEcodeSearch.Text.ToString().Trim().Length > 4)
            {
                DataTable dt = objSQLDB.ExecuteDataSet("SELECT ISNULL(HAAM_EMP_CODE,ECODE) ECODE,CAST(ISNULL(HAAM_EMP_CODE,ECODE) AS VARCHAR)+'--'+MEMBER_NAME+' ('+DESIG+')' AS ENAME " +
                                                       "FROM EORA_MASTER LEFT JOIN HR_APPL_A2E_MIGRATION ON HAAM_AGENT_CODE = ECODE " +
                                                       "WHERE ECODE=" + txtEcodeSearch.Text).Tables[0];
                if (dt.Rows.Count > 0)
                    txtSRName.Text = dt.Rows[0]["ENAME"].ToString();
                else
                    txtSRName.Text = "";
            }
            else
                txtSRName.Text = "";
            objSQLDB = null;

        }
        private void txtArNumber_Validated(object sender, EventArgs e)
        {
            if (txtArNumber.Text.ToString().Trim().Length > 0)
            {
                FillOrderData(txtArNumber.Text.ToString().Trim(), "ARNO");
            }
        }

        private void cmbPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPayMode.SelectedIndex > 0)
            {
                grpbPay.Visible = true;
            }
            else
                grpbPay.Visible = false;
        }

        private void txtRefundAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtRefundAmt_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtRefundAmt.Text != "")
            {
                if (Convert.ToDecimal(txtRefundAmt.Text) > Convert.ToDecimal(txtAdvanceAmt.Text))
                {
                    txtRefundAmt.Text = "";
                    txtRefBalAmt.Text = "";
                }
                else
                {
                    decimal ival = Convert.ToDecimal(txtAdvanceAmt.Text) - Convert.ToDecimal(txtRefundAmt.Text);
                    txtRefBalAmt.Text = ival.ToString();
                }
            }
            else
            {
                txtRefundAmt.Text = "";
                txtRefBalAmt.Text = "";
            }
        }

        public void GetRefundNo()
        {
            objSQLDB = new SQLDB();
            string strSqlRef = "SELECT ISNULL(MAX(SOAR_REFUND_NUMBER),0)+1 FROM SALES_ORD_ADVANCE_REFUND  " +
                                "WHERE SOAR_BRANCH_CODE='" + CommonData.BranchCode + "' AND SOAR_FIN_YEAR='" + cmbFinYear.Text.ToString() + "'";
            txtRefundNo.Text = objSQLDB.ExecuteDataSet(strSqlRef).Tables[0].Rows[0][0].ToString();
            objSQLDB = null;
        }

        private void txtPhoneNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void cmbFinYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            DataTable dt = new DataTable();
            cmbDocMonth.DataSource = null; ;
            try
            {
                string strCommand = "SELECT company_code," +
                                        "fin_year,document_month DisplayMember,document_month ValueMember," +
                                        "start_date,end_date," +
                                        "status FROM document_month " +
                                        "WHERE company_code = '" + CommonData.CompanyCode + "'" +
                                        " AND fin_year = '" + cmbFinYear.Text.ToString() + "'" +
                                        " ORDER BY start_date desc";

                dt = objSQLDB.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {

                    cmbDocMonth.DataSource = dt;
                    cmbDocMonth.DisplayMember = "DisplayMember";
                    cmbDocMonth.ValueMember = "ValueMember";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
                dt = null;
            }
            if (cmbDocMonth.Items.Count > 0)
                cmbDocMonth.SelectedIndex = 0;
        }

        private void cmbAdjustment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAdjustment.SelectedIndex == 0)
            {
                cmbPayMode.SelectedIndex = 0;
                lblInvoiceNo.Visible = true;
                txtAdjustedInvoiceNo.Visible = true;
                txtAdjustedOrder.Visible = true;
                lblOrder.Visible = true;
                txtAdjustedOrder.Text = "";
                txtAdjustedInvoiceNo.Text = "";
            }
            else
            {
                lblInvoiceNo.Visible = false;
                lblOrder.Visible = false;
                txtAdjustedInvoiceNo.Visible = false;
                txtAdjustedOrder.Visible = false;
                txtAdjustedOrder.Text = "";
                txtAdjustedInvoiceNo.Text = "0";
            }


        }

        private void txtAdjustedOrder_Validated(object sender, EventArgs e)
        {
            if (txtAdjustedOrder.Text.Length > 0)
            {
                try
                { Convert.ToInt32(txtAdjustedOrder.Text); }
                catch
                { txtAdjustedOrder.Text = "0"; }

                FillInvOrBultInData(0, Convert.ToInt32(txtAdjustedOrder.Text).ToString("00000"));
            }
        }

        private void FillInvOrBultInData(int nInvNo, string sOrderNo)
        {
            Hashtable ht;
            DataTable InvH;
            DataTable InvD;
            DataSet ds = new DataSet();
            try
            {
                objData = new InvoiceDB();
                ht = objData.GetInvoiceData(CommonData.StateCode, CommonData.BranchCode, nInvNo, sOrderNo);

                InvH = (DataTable)ht["InvHead"];
                InvD = (DataTable)ht["InvDetail"];
                if (InvH.Rows.Count == 0)
                {                    
                    ht = objData.InvoiceBulitin_Get(CommonData.StateCode, CommonData.BranchCode, nInvNo, sOrderNo);
                    InvH = (DataTable)ht["InvHead"];
                    InvD = (DataTable)ht["InvDetail"];                    
                }

                if (InvH.Rows.Count > 0)
                {
                    txtAdjustedInvoiceNo.Text = InvH.Rows[0]["invoice_number"].ToString();
                    txtAdjustedOrder.Text = InvH.Rows[0]["order_number"].ToString();
                }
                else
                {
                    txtAdjustedInvoiceNo.Text = "";
                    txtAdjustedOrder.Text = "";
                    MessageBox.Show("First Enter Invoice to Adjust into Invoice", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                objData = null;
                ht = null;
                InvH = null;
                InvD = null;
                MessageBox.Show(ex.Message, "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objData = null;
                ht = null;
                InvH = null;
                InvD = null;
                ds = null;
            }
            
        }

        private void txtAdjustedInvoiceNo_Validated(object sender, EventArgs e)
        {
            if (txtAdjustedInvoiceNo.Text.Length > 0)
            {
                try
                { Convert.ToInt32(txtAdjustedInvoiceNo.Text); }
                catch
                { txtAdjustedInvoiceNo.Text = "0"; }

                FillInvOrBultInData(Convert.ToInt32(txtAdjustedInvoiceNo.Text), "");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
