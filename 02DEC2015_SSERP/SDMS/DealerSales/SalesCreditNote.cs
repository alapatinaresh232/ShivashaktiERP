using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using System.Data.SqlClient;
using SSAdmin;
using SSTrans;
using System.Collections;

namespace SDMS
{
    public partial class SalesCreditNote : Form
    {
        SQLDB objSQLdb = null;
        IndentDB objIndent = null;
        UtilityDB objUtilData = null;
        InvoiceDB objInvDB = null;
        bool flagUpdate = false;
        double netSales = 0;
        DealerInfo objDealerInfo = null;
        string strVoucherId = "";
        public SalesCreditNote()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //cbAccountName.SelectedIndex = 0;
            txtAccountSearch.Text = "";
            cbAccountName.DataSource = null;
            cbInvoices.DataSource = null;
            cbProducts.DataSource = null;
            txtReferenceNo.Text = "";
            txtTotalFreight.Text = "";
            txtToPay.Text = "";
            txtAdvancePaid.Text = "";
            txtLoadingCharges.Text = "";
            txtEcodeSearch.Text = "";
            cbReason.SelectedIndex = 0;
            txtTransporter.Text = "";
            txtTripLRNo.Text = "";
            txtVehicleNo.Text = "";
            cbVehicleType.SelectedIndex = 0;
            //txtTotalNetAmt.Text = "";
            netSales = 0;
            txtTotRecivedAmt.Text = "";
            txtTotRecvdQty.Text = "";
            txtRecvdDiscPer.Text = "";
            txtRecvdVatPer.Text = "";
            txtDscRcvdAmt.Text = "";
            txtVatRcvdAmt.Text = "";
            gvProductDetails.Rows.Clear();

        }

        private void SalesCreditNote_Load(object sender, EventArgs e)
        {
            FillReasons();
            FillTrnType();
            GenerateNewTrnNo();
            FillVehcleType();
            cbVehicleType.SelectedIndex = 0;
            FillDriverEmployeeData();
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                     System.Drawing.FontStyle.Regular);
            //cbDriverEcode.Visible = true;
            //txtDriverNo.Visible = false;
        }

        private void FillDriverEmployeeData()
        {
            objIndent = new IndentDB();

            try
            {
                cbDriverEcode.DataSource = null;
                cbDriverEcode.Items.Clear();
                DataTable dtEcode = objIndent.DCOtherEmployeeList_Get().Tables[0];
                if (dtEcode.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dtEcode.Rows)
                    {
                        ComboboxItem objItem = new ComboboxItem();
                        objItem.Value = dataRow["ENAME"].ToString();
                        objItem.Text = dataRow["ENAME"].ToString();
                        cbDriverEcode.Items.Add(objItem);
                        objItem = null;
                    }

                }
                dtEcode = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objIndent = null;
            }

        }

        private void FillVehcleType()
        {
            objUtilData = new UtilityDB();
            try
            {
                DataTable dt = objUtilData.dtVehicleType();
                if (dt.Rows.Count > 0)
                {
                    cbVehicleType.DataSource = dt;
                    cbVehicleType.DisplayMember = "type";
                    cbVehicleType.ValueMember = "name";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                objUtilData = null;
                Cursor.Current = Cursors.Default;
            }
        }
        private void FillDealerInvoices(string strRefNo)
        {

            SqlParameter[] param = new SqlParameter[2];
            DataTable dt = new DataTable();
            cbInvoices.DataSource = null;
            try
            {
                objSQLdb = new SQLDB();
                param[0] = objSQLdb.CreateParameter("@sDealerCode", DbType.Int32, Convert.ToInt32(cbAccountName.SelectedValue.ToString()), ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sRefNumber", DbType.String, strRefNo, ParameterDirection.Input);
                dt = objSQLdb.ExecuteDataSet("GetDealerInvoices", CommandType.StoredProcedure, param).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbInvoices.DataSource = dt;
                    cbInvoices.DisplayMember = "DLDH_REFERENCE_NUMBER";
                    cbInvoices.ValueMember = "DLDH_TRN_NUMBER";
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

        private void GenerateNewTrnNo()
        {
            objSQLdb = new SQLDB();
            string strNewNo = string.Empty;
            string sCompCode = CommonData.CompanyCode;
            string strStCode = CommonData.StateCode;
            string strBranchName = CommonData.BranchName;
            string strBranchCod = CommonData.BranchCode;
            string strFinYear = CommonData.FinancialYear;
            string sDcNo = string.Empty;
            string[] strArrBr = strBranchName.Split(' ');
            if (strArrBr.Length > 2)
                strBranchName = strArrBr[strArrBr.Length - 2];
            else if (strArrBr.Length > 1)
                strBranchName = strArrBr[strArrBr.Length - 2];
            else if (strArrBr.Length == 1)
                strBranchName = strArrBr[0];

            strBranchName = strBranchName.Replace(".", "");
            try
            {
                DataTable dt = new DataTable();

                strNewNo = sCompCode.Substring(0, 3) + strStCode + strBranchCod.Substring(6, 3) + "CN" + strFinYear.Substring(2, 2) + strFinYear.Substring(7, 2) + "-";

                dt = objSQLdb.ExecuteDataSet("Select IsNull(Max(Substring(IsNull(DLCNH_TRN_NUMBER, '" + strNewNo +
                    "'),16,20)),0) + 1 from DL_CREDITNOTE_HEAD WHERE DLCNH_COMPANY_CODE  = '" + sCompCode +
                    "' AND DLCNH_BRANCH_CODE = '" + strBranchCod + "' AND DLCNH_FIN_YEAR= '" + CommonData.FinancialYear + "'").Tables[0];

                if (dt.Rows.Count > 0)
                {
                    sDcNo = Convert.ToInt32(dt.Rows[0][0]).ToString().PadLeft(6, '0');
                }
                strNewNo = strNewNo + sDcNo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
            }
            txtTransactionNo.Text = strNewNo;
        }

        private void FillTrnType()
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("type", typeof(string));
                table.Columns.Add("name", typeof(string));

                table.Rows.Add("DL2PUCN", "DL2PUCN");
                table.Rows.Add("DL2SPCN", "DL2SPCN");
                if (table.Rows.Count > 0)
                {
                    cbTrnType.DataSource = table;
                    cbTrnType.DisplayMember = "type";
                    cbTrnType.ValueMember = "name";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

                Cursor.Current = Cursors.Default;
            }
        }
        private void FillReasons()
        {
            DataTable table = new DataTable();
            table.Columns.Add("type", typeof(string));
            table.Columns.Add("name", typeof(string));

            table.Rows.Add("--Select--", "--Select--");
            table.Rows.Add("Batch Failure", "Batch Failure");
            table.Rows.Add("Same Indent Double Time Sent", "Same Indent Double Time Sent");
            table.Rows.Add("Stock Damages", "Stock Damages");
            table.Rows.Add("Transport Problems", "Transport Problems");
            table.Rows.Add("Others", "Others");

            cbReason.DataSource = table;
            cbReason.DisplayMember = "type";
            cbReason.ValueMember = "name";
        }


        private void txtAccountSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtAccountSearch.Text.ToString().Trim().Length > 0)
            {
                EcodeSearch();
                FillDealerInvoices(txtInvoiceSerch.Text);
            }
            else
            {
                cbAccountName.SelectedIndex = -1;
            }
        }

        private void EcodeSearch()
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[2];
            DataSet ds = new DataSet();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cbAccountName.DataSource = null;
                cbAccountName.Items.Clear();

                param[0] = objSQLdb.CreateParameter("@xCompCode", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xNameLike", DbType.String, txtAccountSearch.Text, ParameterDirection.Input);
                ds = objSQLdb.ExecuteDataSet("DL_GetAcctNameSearch", CommandType.StoredProcedure, param);

                DataTable dtEmp = ds.Tables[0];

                if (dtEmp.Rows.Count > 0)
                {
                    cbAccountName.DataSource = dtEmp;
                    cbAccountName.DisplayMember = "AccountName";
                    cbAccountName.ValueMember = "AM_ACCOUNT_ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cbAccountName.SelectedIndex > -1)
                {
                    cbAccountName.SelectedIndex = 0;
                }
                objSQLdb = null;
                Cursor.Current = Cursors.Default;
            }
            if (ds.Tables == null)
            {
                gvProductDetails.Rows.Clear();
            }
        }

        private void txtEcodeSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtEcodeSearch.Text.ToString().Trim().Length > 4)
            {
                GetEmpData();
            }
            else
            {
                txtEname.Text = "";
            }
        }

        private void GetEmpData()
        {
            objSQLdb = new SQLDB();
            if (txtEcodeSearch.Text.ToString().Trim().Length > 4)
            {
                DataTable dt = objSQLdb.ExecuteDataSet("SELECT ISNULL(HAAM_EMP_CODE,ECODE) ECODE,CAST(ISNULL(HAAM_EMP_CODE,ECODE) AS VARCHAR)+'--'+MEMBER_NAME+' ('+DESIG+')' AS ENAME " +
                                                       "FROM EORA_MASTER LEFT JOIN HR_APPL_A2E_MIGRATION ON HAAM_AGENT_CODE = ECODE " +
                                                       "WHERE ECODE=" + txtEcodeSearch.Text).Tables[0];
                if (dt.Rows.Count > 0)
                    txtEname.Text = dt.Rows[0]["ENAME"].ToString();
                else
                    txtEname.Text = "";
            }
            else
                txtEname.Text = "";
            objSQLdb = null;
        }

        private void txtAccountSearch_Validated(object sender, EventArgs e)
        {
            if (cbAccountName.SelectedIndex >= 0)
            {
                FillDealerInvoices(txtInvoiceSerch.Text);
            }
            else
            {
                cbInvoices.DataSource = null;
                cbProducts.DataSource = null;
            }
        }



        private void clbInvoices_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //string strInvoices = null;
            //for (int i = 0; i < clbInvoices.Items.Count;i++ )
            //{
            //    if (clbInvoices.GetItemCheckState(i) == CheckState.Checked)
            //    {
            //        strInvoices += ((SSAdmin.NewCheckboxListItem)(clbInvoices.Items[i])).Tag.ToString() + ",";
            //    }
            //}
            //string [] strInvoicess = strInvoices.Split(',');
            //StringBuilder strCmd = null;
            //for (int i = 0; i < strInvoicess.Length;i++ )
            //{
            //    strCmd.Append(" SELECT DLDD_STATE_CODE,DLDD_BRANCH_CODE,DLDD_FIN_YEAR,DLDD_DOCUMENT_MONTH,DLDD_TRN_TYPE,DLDD_TRN_NUMBER,"+
            //                    " DLDD_SL_NO,DLDD_PRODUCT_ID,DLDD_ISS_QTY,DLDD_ISS_RATE,DLDD_ISS_AMT,DLDD_DISC_PER,DLDD_DISC_AMOUNT,"+
            //                    " DLDD_VAT_PER,DLDD_VAT_AMOUNT,DLDD_BED_PER,DLDD_COMPANY_CODE,DLDD_BED_AMOUNT,DLDD_SED_PER,DLDD_SED_AMOUNT,"+
            //                    " DLDD_CESS_PER,DLDD_CESS_AMOUNT,DLDD_NET_AMOUNT,DLDD_BATCH_NO,DLDH_REFERENCE_NUMBER,PM_PRODUCT_NAME,"+
            //                    " CATEGORY_NAME from DL_DCINV_DETL INNER JOIN DL_DCINV_HEAD on DLDH_TRN_NUMBER=dldd_trn_number"+
            //                    " INNER JOIN PRODUCT_MAS ON DLDD_PRODUCT_ID=PM_PRODUCT_ID"+
            //                    " INNER JOIN CATEGORY_MASTER ON CATEGORY_ID=PM_CATEGORY_ID"+
            //                    " WHERE DLDD_TRN_NUMBER='"+strInvoicess[i]+"'");
            //}
            //objSQLdb = new SQLDB();
            //  DataTable dt =objSQLdb.ExecuteDataSet(strCmd.ToString()).Tables[0];
            //  AddToGrid(dt);


        }

        private void AddProductsToGrid(DataTable dt)
        {
            try
            {
                //gvProductDetails.Rows.Clear();
                //cbIndents.SelectedIndex = -1;
                if (dt.Rows.Count > 0)
                {


                    gvProductDetails.Rows.Add();
                    int intRow = gvProductDetails.Rows.Count - 1;
                    gvProductDetails.Rows[intRow].Cells["SLNO"].Value = gvProductDetails.Rows.Count;
                    gvProductDetails.Rows[intRow].Cells["CompCode"].Value = dt.Rows[0]["DLDD_COMPANY_CODE"].ToString();
                    gvProductDetails.Rows[intRow].Cells["StateCode"].Value = dt.Rows[0]["DLDD_STATE_CODE"].ToString();
                    gvProductDetails.Rows[intRow].Cells["BranchCode"].Value = dt.Rows[0]["DLDD_BRANCH_CODE"].ToString();
                    gvProductDetails.Rows[intRow].Cells["FinYear"].Value = dt.Rows[0]["DLDD_FIN_YEAR"].ToString();
                    gvProductDetails.Rows[intRow].Cells["DocMonth"].Value = dt.Rows[0]["DLDD_DOCUMENT_MONTH"].ToString();
                    gvProductDetails.Rows[intRow].Cells["TrnType"].Value = dt.Rows[0]["DLDD_TRN_TYPE"].ToString();
                    gvProductDetails.Rows[intRow].Cells["TrnNumber"].Value = dt.Rows[0]["DLDD_TRN_NUMBER"].ToString();
                    gvProductDetails.Rows[intRow].Cells["Date"].Value = dt.Rows[0]["DLDH_DISPATCH_DOC_DATE"].ToString();
                    gvProductDetails.Rows[intRow].Cells["InvoiceNo"].Value = dt.Rows[0]["DLDH_REFERENCE_NUMBER"].ToString();
                    gvProductDetails.Rows[intRow].Cells["Category"].Value = dt.Rows[0]["CATEGORY_NAME"].ToString();
                    gvProductDetails.Rows[intRow].Cells["Product"].Value = dt.Rows[0]["PM_PRODUCT_NAME"].ToString();
                    gvProductDetails.Rows[intRow].Cells["BatchNo"].Value = dt.Rows[0]["DLDD_BATCH_NO"].ToString();
                    gvProductDetails.Rows[intRow].Cells["ProductId"].Value = dt.Rows[0]["DLDD_PRODUCT_ID"].ToString();
                    gvProductDetails.Rows[intRow].Cells["IssQty"].Value = dt.Rows[0]["DLDD_ISS_QTY"].ToString();
                    gvProductDetails.Rows[intRow].Cells["Rate"].Value = dt.Rows[0]["DLDD_ISS_RATE"].ToString();
                    gvProductDetails.Rows[intRow].Cells["Amount"].Value = dt.Rows[0]["DLDD_ISS_AMT"].ToString();
                    gvProductDetails.Rows[intRow].Cells["Discper"].Value = dt.Rows[0]["DLDD_DISC_PER"].ToString();
                    gvProductDetails.Rows[intRow].Cells["DiscAmount"].Value = dt.Rows[0]["DLDD_DISC_AMOUNT"].ToString();
                    gvProductDetails.Rows[intRow].Cells["Vatper"].Value = dt.Rows[0]["DLDD_VAT_PER"].ToString();
                    gvProductDetails.Rows[intRow].Cells["VatAmount"].Value = dt.Rows[0]["DLDD_VAT_AMOUNT"].ToString();
                    gvProductDetails.Rows[intRow].Cells["NetAmt"].Value = dt.Rows[0]["DLDD_NET_AMOUNT"].ToString();
                    gvProductDetails.Rows[intRow].Cells["CNprevQty"].Value = dt.Rows[0]["CNQty"].ToString();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {

            }
        }

        //private void clbInvoices_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string strInvoices = null;
        //    for (int i = 0; i < clbInvoices.CheckedItems.Count; i++)
        //    {
        //            strInvoices += "'"+((SSAdmin.NewCheckboxListItem)(clbInvoices.CheckedItems[i])).Tag.ToString() + "',";
        //    }
        //   //string[] strInvoicess = strInvoices.Split(',');
        //    if (strInvoices != null)
        //    {
        //        strInvoices = strInvoices.Remove(strInvoices.Length - 1);
        //        StringBuilder strCmd = new StringBuilder();

        //        strCmd.Append(" SELECT DLDD_STATE_CODE,DLDD_BRANCH_CODE,DLDD_FIN_YEAR,DLDD_DOCUMENT_MONTH,DLDD_TRN_TYPE,DLDD_TRN_NUMBER," +
        //                        " DLDD_SL_NO,DLDD_PRODUCT_ID,DLDD_ISS_QTY,DLDD_ISS_RATE,DLDD_ISS_AMT,DLDD_DISC_PER,DLDD_DISC_AMOUNT," +
        //                        " DLDD_VAT_PER,DLDD_VAT_AMOUNT,DLDD_BED_PER,DLDD_COMPANY_CODE,DLDD_BED_AMOUNT,DLDD_SED_PER,DLDD_SED_AMOUNT," +
        //                        " DLDD_CESS_PER,DLDD_CESS_AMOUNT,DLDD_NET_AMOUNT,DLDD_BATCH_NO,DLDH_REFERENCE_NUMBER,PM_PRODUCT_NAME," +
        //                        " CATEGORY_NAME from DL_DCINV_DETL INNER JOIN DL_DCINV_HEAD on DLDH_TRN_NUMBER=dldd_trn_number" +
        //                        " INNER JOIN PRODUCT_MAS ON DLDD_PRODUCT_ID=PM_PRODUCT_ID" +
        //                        " INNER JOIN CATEGORY_MASTER ON CATEGORY_ID=PM_CATEGORY_ID" +
        //                        " WHERE DLDD_TRN_NUMBER in (" + strInvoices + ")");

        //        objSQLdb = new SQLDB();
        //        DataTable dt = objSQLdb.ExecuteDataSet(strCmd.ToString()).Tables[0];
        //        AddToGrid(dt);
        //    }
        //    else
        //    {
        //        gvProductDetails.Rows.Clear();
        //    }

        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {

                if (SaveHeadData() > 0)
                {
                    if (SaveDetlData() > 0)
                    {
                        if (SaveInFaVoucher() > 0)
                        {


                            objInvDB = new InvoiceDB();

                            objInvDB.UpdatingOutStandingAmt(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, "CN", Convert.ToInt32(strVoucherId));




                            MessageBox.Show("Data Saved Successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //for (int i = 0; i < gvProductDetails.Rows.Count;i++ )
                            //{
                            //    objSQLdb = new SQLDB();
                            //    SqlParameter[] param = new SqlParameter[6];
                            //    DataSet ds = new DataSet();
                            //    try
                            //    {
                            //        param[0] = objSQLdb.CreateParameter("@xCompany", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                            //        param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                            //        param[2] = objSQLdb.CreateParameter("@xFinYr", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                            //        param[3] = objSQLdb.CreateParameter("@xDocType", DbType.String, cbTrnType.SelectedValue.ToString(), ParameterDirection.Input);
                            //        param[4] = objSQLdb.CreateParameter("@xTrnNumber", DbType.String, txtTransactionNo.Text, ParameterDirection.Input);
                            //        param[5] = objSQLdb.CreateParameter("@xProductId", DbType.String, gvProductDetails.Rows[i].Cells["ProductID"].Value.ToString(), ParameterDirection.Input);

                            //        ds = objSQLdb.ExecuteDataSet("FA_UpdateAgDLCreditNote", CommandType.StoredProcedure, param);

                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        MessageBox.Show(ex.Message);
                            //    }
                            //}
                            btnCancel_Click(null, null);
                            flagUpdate = false;
                            strVoucherId = "";
                            GenerateNewTrnNo();
                            txtReferenceNo.Focus();
                        }
                    }
                    else
                    {
                        try
                        {
                            string strCMD = " DELETE  DL_CREDITNOTE_HEAD WHERE DLCNH_COMPANY_CODE='" + CommonData.CompanyCode +
                                                                               "' AND DLCNH_STATE_CODE='" + CommonData.StateCode +
                                                                               "' AND DLCNH_TRN_NUMBER='" + txtTransactionNo.Text +
                                                                               "' AND DLCNH_BRANCH_CODE='" + CommonData.BranchCode +
                                                                               "' AND DLCNH_FIN_YEAR='" + CommonData.FinancialYear + "'";
                            objSQLdb = new SQLDB();
                            objSQLdb.ExecuteSaveData(strCMD);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }

            }
        }
        private string GenerateVoucherId()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
             strVoucherId=null;
            try
            {
                String strCommand = "SELECT ISNULL(MAX(VCO_VOUCHER_ID),0)+1 VoucherId FROM FA_VOUCHER_OTHERS";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    strVoucherId = dt.Rows[0]["VoucherId"] + "";
                    return strVoucherId;
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
            return strVoucherId;
        }
        private int SaveInFaVoucher()
        {
            int iRes1, iRes2, iRes3 = 0;
            try
            {
                string strSQLVCOTH = "", strSQLVC = "", strSQLVCBILLS = "";
                string strVoucherId = GenerateVoucherId();
                strSQLVCOTH = " INSERT INTO FA_VOUCHER_OTHERS(VCO_COMPANY_CODE" +
                                        ",VCO_BRANCH_CODE" +
                                        ",VCO_FIN_YEAR" +
                                        ",VCO_DOC_TYPE" +
                                        ",VCO_VOUCHER_ID" +
                                        ",VCO_VOUCHER_DATE" +
                                        ",VCO_NARRATION_1" +
                                        ",VCO_NARRATION_2" +
                                        ",VCO_EFFECT_NAME" +
                                        ",VCO_VOUCHER_NO" +
                                        ",VCO_CREATED_BY" +
                                        ",VCO_CREATED_DATE" +
                                        ",VCO_COLL_ECODE" +
                                        ") VALUES('" + CommonData.CompanyCode +
                                        "','" + CommonData.BranchCode +
                                        "','" + CommonData.FinancialYear +
                                        "','CN'" +
                                        ",'" + strVoucherId +
                                        "','" + dtpReceivedDate.Value.ToString("dd/MMM/yyyy") +
                                        "','','','','" +
                                        "','" + CommonData.LogUserId +
                                        "',getdate(),'" + cbAccountName.SelectedValue.ToString() + "')";

                strSQLVC = "INSERT INTO FA_VOUCHER( VC_COMPANY_CODE" +
                            ",VC_BRANCH_CODE" +
                            ",VC_FIN_YEAR" +
                            ",VC_DOC_TYPE" +
                            ",VC_VOUCHER_ID" +
                            ",VC_VOUCHER_DATE" +
                            ",VC_SL_NO" +
                            ",VC_ACCOUNT_ID" +
                            ",VC_DEBIT_CREDIT" +
                            ",VC_AMOUNT" +
                            ",VC_MAIN_COST_CENTRE_ID" +
                            ",VC_SUB_COST_CENTRE_ID" +
                            ",VC_PAYMENT_MODE" +
                            ",VC_CHEQUE_NO" +
                            ",VC_CHEQUE_DATE" +
                            ",VC_REMARKS" +
                            ",VC_VOUCHER_NO" +
                            ",VC_CASH_BANK_ID" +
                            ",VC_VOUCHER_AMOUNT" +
                            ",VC_VOUCHER_TYPE" +
                            ",VC_APPROVED" +
                            ",VC_POSTED" +
                            ",VC_CREATED_BY" +
                            ",VC_CREATED_DATE)" +
                            " VALUES('" + CommonData.CompanyCode +
                            "','" + CommonData.BranchCode +
                            "','" + CommonData.FinancialYear +
                            "','CN'" +
                             ",'" + strVoucherId +
                             "','" + dtpReceivedDate.Value.ToString("dd/MMM/yyyy") +
                             "','" + 1 +
                             "','" + cbAccountName.SelectedValue.ToString() +
                             "','C'" +
                             ",'" + txtTotRecivedAmt.Text +
                             "',''" +
                             ",''" +
                             ",''" +
                             ",''" +
                             ",''" +
                             ",''" +
                             ",''" +
                             ",'" +
                             "','" + txtTotRecivedAmt.Text +
                             "','CN'" +
                             ",'N" +
                             "','N" +
                             "','" + CommonData.LogUserId +
                             "',GETDATE())";

                strSQLVC += "INSERT INTO FA_VOUCHER( VC_COMPANY_CODE" +
                            ",VC_BRANCH_CODE" +
                            ",VC_FIN_YEAR" +
                            ",VC_DOC_TYPE" +
                            ",VC_VOUCHER_ID" +
                            ",VC_VOUCHER_DATE" +
                            ",VC_SL_NO" +
                            ",VC_ACCOUNT_ID" +
                            ",VC_DEBIT_CREDIT" +
                            ",VC_AMOUNT" +
                            ",VC_MAIN_COST_CENTRE_ID" +
                            ",VC_SUB_COST_CENTRE_ID" +
                            ",VC_PAYMENT_MODE" +
                            ",VC_CHEQUE_NO" +
                            ",VC_CHEQUE_DATE" +
                            ",VC_REMARKS" +
                            ",VC_VOUCHER_NO" +
                            ",VC_CASH_BANK_ID" +
                            ",VC_VOUCHER_AMOUNT" +
                            ",VC_VOUCHER_TYPE" +
                            ",VC_APPROVED" +
                            ",VC_POSTED" +
                            ",VC_CREATED_BY" +
                            ",VC_CREATED_DATE)" +
                            " VALUES('" + CommonData.CompanyCode +
                            "','" + CommonData.BranchCode +
                            "','" + CommonData.FinancialYear +
                            "','CN'" +
                             ",'" + strVoucherId +
                             "','" + dtpReceivedDate.Value.ToString("dd/MMM/yyyy") +
                             "','" + 0 +
                             "','CASH" +
                             "','D" +
                             "','" + txtTotRecivedAmt.Text +
                             "','" +
                             "','" +
                             "',''" +
                             ",'" +
                             "','" +
                             "','" +
                             "','" +
                             "','" +
                             "','" + txtTotRecivedAmt.Text +
                             "','CN'" +
                             ",'N'" +
                             ",'N'" +
                             ",'" + CommonData.LogUserId +
                             "',GETDATE())";

                objInvDB = new InvoiceDB();
                DataTable dt = objInvDB.GetAdjustingAmount(cbAccountName.SelectedValue.ToString()).Tables[0];
                double dOutstAmt = 0;
                for (int iVar = 0; iVar < dt.Rows.Count; iVar++)
                {
                    if (dt.Rows[iVar]["OU_BILL_NUMBER"].ToString() == gvProductDetails.Rows[iVar].Cells["TrnNumber"].Value.ToString() && Convert.ToDouble(dt.Rows[iVar]["OU_AMOUNT"].ToString()) < Convert.ToDouble(gvProductDetails.Rows[iVar].Cells["RecvdAmt"].Value.ToString()))
                    {
                        dOutstAmt += (((Convert.ToDouble(gvProductDetails.Rows[iVar].Cells["RecvdAmt"].Value.ToString())) - Convert.ToDouble(dt.Rows[iVar]["OU_AMOUNT"].ToString())));
                    }
                }


                for (int iVar = 0; iVar < gvProductDetails.Rows.Count; iVar++)
                {
                    if (gvProductDetails.Rows[iVar].Cells["ReceivedQty"].Value != null && gvProductDetails.Rows[iVar].Cells["ReceivedQty"].Value.ToString() != "0" && gvProductDetails.Rows[iVar].Cells["RecvdAmt"].Value.ToString() != "")
                    {
                        //if (Convert.ToDouble(dtAgnstVoucherBill.Rows[i]["AG_BILL_AMOUNT"].ToString()) > 0)
                        //{
                        strSQLVCBILLS += " INSERT INTO FA_VOUCHER_BILLS(VCB_COMPANY_CODE" +
                              ",VCB_BRANCH_CODE" +
                              ",VCB_FIN_YEAR" +
                              ",VCB_DOC_TYPE" +
                              ",VCB_VOUCHER_ID" +
                              ",VCB_VOUCHER_DATE" +
                              ",VCB_MASTER_SL_NO" +
                              ",VCB_SL_NO" +
                              ",VCB_ADJUSTMENT_TYPE" +
                              ",VCB_AG_COMPANY_CODE" +
                              ",VCB_AG_STATE_CODE" +
                              ",VCB_AG_BRANCH_CODE" +
                              ",VCB_AG_FIN_YEAR" +
                              ",VCB_AG_BILL_TYPE" +
                              ",VCB_AG_BILL_NUMBER" +
                              ",VCB_AG_BILL_DATE" +
                              ",VCB_AG_BILL_AMOUNT" +
                              ",VCB_CREATED_BY" +
                              ",VCB_CREATED_DATE) VALUES('" + CommonData.CompanyCode +
                              "','" + CommonData.BranchCode +
                              "','" + CommonData.FinancialYear +
                              "','CN'" +
                              ",'" + strVoucherId +
                              "','" + dtpReceivedDate.Value.ToString("dd/MMM/yyyy") +
                              "',' 1 " +
                              "','" + (iVar + 1) +
                              "','A" +
                              "','" + gvProductDetails.Rows[iVar].Cells["CompCode"].Value.ToString() +
                              "','" + gvProductDetails.Rows[iVar].Cells["StateCode"].Value.ToString() +
                              "','" + gvProductDetails.Rows[iVar].Cells["BranchCode"].Value.ToString() +
                              "','" + gvProductDetails.Rows[iVar].Cells["FinYear"].Value.ToString() +
                              "','" + gvProductDetails.Rows[iVar].Cells["TrnType"].Value.ToString() +
                              "','" + gvProductDetails.Rows[iVar].Cells["TrnNumber"].Value.ToString() +
                            "','" + Convert.ToDateTime(gvProductDetails.Rows[iVar].Cells["Date"].Value.ToString()).ToString("dd/MMM/yyyy");
                        DataRow[] dr = dt.Select("OU_BILL_NUMBER='" + gvProductDetails.Rows[iVar].Cells["TrnNumber"].Value.ToString() + "'");
                        double returnAmt = 0;
                        if (Convert.ToDouble(dr[0]["OU_AMOUNT"].ToString()) < Convert.ToDouble(gvProductDetails.Rows[iVar].Cells["RecvdAmt"].Value.ToString()))
                        {
                            returnAmt = Convert.ToDouble(gvProductDetails.Rows[iVar].Cells["RecvdAmt"].Value.ToString()) - Convert.ToDouble(dr[0]["OU_AMOUNT"].ToString());
                        }
                        else
                        {
                            returnAmt = Convert.ToDouble(gvProductDetails.Rows[iVar].Cells["RecvdAmt"].Value.ToString());
                        }
                        strSQLVCBILLS += "','" + returnAmt +
                         "','" + CommonData.LogUserId +
                         "',GETDATE())";
                        //}
                    }
                }
                if (dOutstAmt > 0)
                {
                    strSQLVCBILLS += " INSERT INTO FA_VOUCHER_BILLS(VCB_COMPANY_CODE" +
                                      ",VCB_BRANCH_CODE" +
                                      ",VCB_FIN_YEAR" +
                                      ",VCB_DOC_TYPE" +
                                      ",VCB_VOUCHER_ID" +
                                      ",VCB_VOUCHER_DATE" +
                                      ",VCB_MASTER_SL_NO" +
                                      ",VCB_SL_NO" +
                                      ",VCB_ADJUSTMENT_TYPE" +
                                      ",VCB_AG_COMPANY_CODE" +
                                      ",VCB_AG_STATE_CODE" +
                                      ",VCB_AG_BRANCH_CODE" +
                                      ",VCB_AG_FIN_YEAR" +
                                      ",VCB_AG_BILL_TYPE" +
                                      ",VCB_AG_BILL_NUMBER" +
                                      ",VCB_AG_BILL_DATE" +
                                      ",VCB_AG_BILL_AMOUNT" +
                                      ",VCB_CREATED_BY" +
                                      ",VCB_CREATED_DATE) VALUES('" + CommonData.CompanyCode +
                                      "','" + CommonData.BranchCode +
                                      "','" + CommonData.FinancialYear +
                                      "','CN'" +
                                      ",'" + strVoucherId +
                                      "','" + dtpReceivedDate.Value.ToString("dd/MMM/yyyy") +
                                      "',' 1 " +
                                      "','" + (gvProductDetails.Rows.Count) +
                                      "','O" +
                                      "','" +
                                      "','" +
                                      "','" +
                                      "','" +
                                      "','" +
                                      "','" +
                                      "','" +
                        //DataRow[] dr = dt.Select("OU_BILL_NUMBER='" + gvProductDetails.Rows[iVar].Cells["TrnNumber"].Value.ToString() + "'");
                        //double returnAmt = 0;
                        //if (Convert.ToDouble(dr[0]["OU_AMOUNT"].ToString()) < Convert.ToDouble(gvProductDetails.Rows[iVar].Cells["RecvdAmt"].Value.ToString()))
                        //{
                        //    returnAmt = Convert.ToDouble(gvProductDetails.Rows[iVar].Cells["RecvdAmt"].Value.ToString()) - Convert.ToDouble(dr[0]["OU_AMOUNT"].ToString());
                        //}
                                     "','" + dOutstAmt +
                                     "','" + CommonData.LogUserId +
                                     "',GETDATE())";

                }
                objSQLdb = new SQLDB();
                 iRes1 = objSQLdb.ExecuteSaveData(strSQLVCOTH);
                 iRes2 = objSQLdb.ExecuteSaveData(strSQLVC);
                if (strSQLVCBILLS.Length > 0)
                {
                     iRes3 = objSQLdb.ExecuteSaveData(strSQLVCBILLS);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return iRes3;
        }

        private int SaveDetlData()
        {
            int iRes = 0;
            string strSQL = string.Empty;
            StringBuilder sbSQL = new StringBuilder();
            objSQLdb = new SQLDB();
            string sdocMonth = dtpReceivedDate.Value.ToString("MMM").ToUpper() + "" + dtpReceivedDate.Value.ToString("yyyy");
            try
            {
                strSQL = " DELETE DL_CREDITNOTE_DETL" +
                                " WHERE DLCND_COMPANY_CODE='" + CommonData.CompanyCode +
                                    "' AND dlcnd_branch_code='" + CommonData.BranchCode +
                                    "' AND DLCND_TRN_NUMBER='" + txtTransactionNo.Text +
                                    "' AND DLCND_FIN_YEAR='" + CommonData.FinancialYear + "'";

                iRes = objSQLdb.ExecuteSaveData(strSQL);
                sbSQL = new StringBuilder();
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    if (gvProductDetails.Rows[i].Cells["ReceivedQty"].Value != null && gvProductDetails.Rows[i].Cells["ReceivedQty"].Value.ToString() != "0" && gvProductDetails.Rows[i].Cells["RecvdAmt"].Value.ToString() != "")
                    {
                        sbSQL.Append(" INSERT INTO DL_CREDITNOTE_DETL (DLCND_COMPANY_CODE" +
                                        ", DLCND_STATE_CODE, DLCND_BRANCH_CODE, DLCND_FIN_YEAR" +
                                        ", DLCND_DOCUMENT_MONTH,DLCND_TRN_TYPE, DLCND_TRN_NUMBER,DLCND_SL_NO" +
                                        ",DLCND_PRODUCT_ID,DLCND_ISS_QTY,DLCND_ISS_RATE,DLCND_ISS_AMT" +
                                        ",DLCND_DISC_PER,DLCND_DISC_AMOUNT,DLCND_VAT_PER,DLCND_VAT_AMOUNT"+
                                //",DLCND_BED_PER,DLCND_BED_AMOUNT"+
                            //",DLCND_SED_PER,DLCND_SED_AMOUNT,DLCND_CESS_PER,DLCND_CESS_AMOUNT"+"
                                        ",DLCND_NET_AMOUNT,DLCND_AG_COMPANY_CODE,DLCND_AG_STATE_CODE,DLCND_AG_BRANCH_CODE,DLCND_AG_FIN_YEAR" +
                                        ",DLCND_AG_DOCUMENT_MONTH,DLCND_AG_TRN_TYPE,DLCND_AG_TRN_NUMBER,DLCND_AG_TRN_DATE,DLCND_AG_REFERENCE_NUMBER,DLCND_BATCH_NO)" +
                                        " VALUES ('" + CommonData.CompanyCode + "', '" + CommonData.StateCode +
                                        "', '" + CommonData.BranchCode + "', '" + CommonData.FinancialYear + "', '" + sdocMonth +
                                        "', '" + cbTrnType.SelectedValue.ToString() + "', '" + txtTransactionNo.Text +
                                        "', " + gvProductDetails.Rows[i].Cells["SLNO"].Value +
                                        ", '" + gvProductDetails.Rows[i].Cells["ProductId"].Value +
                                        "', " + gvProductDetails.Rows[i].Cells["ReceivedQty"].Value +
                                        ", " + gvProductDetails.Rows[i].Cells["Rate"].Value +
                                        ", " + gvProductDetails.Rows[i].Cells["RcvdNetAmt"].Value +
                                        ", '" + gvProductDetails.Rows[i].Cells["DiscRcvdPer"].Value +
                                        "', '" + gvProductDetails.Rows[i].Cells["ReceivedDiscAmt"].Value +
                                        "', '" + gvProductDetails.Rows[i].Cells["VatReceived"].Value +
                                        "', '" + gvProductDetails.Rows[i].Cells["ReceivedVatAmt"].Value +
                                        "', " + gvProductDetails.Rows[i].Cells["RecvdAmt"].Value +
                                        ", '" + gvProductDetails.Rows[i].Cells["CompCode"].Value +
                                        "', '" + gvProductDetails.Rows[i].Cells["StateCode"].Value +
                                        "', '" + gvProductDetails.Rows[i].Cells["BranchCode"].Value +
                                        "', '" + gvProductDetails.Rows[i].Cells["FinYear"].Value +
                                        "', '" + gvProductDetails.Rows[i].Cells["DocMonth"].Value +
                                        "','" + gvProductDetails.Rows[i].Cells["TrnType"].Value +
                                        "', '" + gvProductDetails.Rows[i].Cells["TrnNumber"].Value +
                                        "', '" + Convert.ToDateTime(gvProductDetails.Rows[i].Cells["Date"].Value.ToString()).ToString("dd/MMM/yyyy") +
                                        "', '" + gvProductDetails.Rows[i].Cells["InvoiceNo"].Value +
                                        "','" + gvProductDetails.Rows[i].Cells["BatchNo"].Value + "'); ");
                    }



                }
                iRes = 0;
                if (sbSQL.ToString().Length > 10)
                {
                    iRes = objSQLdb.ExecuteSaveData(sbSQL.ToString());

                    
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

        private int SaveHeadData()
        {
            int iRes = 0;
            string strDriver = string.Empty;
            string ecode = string.Empty;
            string strCMD = "";

            string sDocMonth = dtpReceivedDate.Value.ToString("MMM").ToUpper() + "" + dtpReceivedDate.Value.ToString("yyyy");
            //string sDocMonth = meTransactionDate.Value.ToString("MMMyyyy");
            if (cbVehicleType.Text == "OWN")
                strDriver = cbDriverEcode.Text.ToString();
            else
                strDriver = txtDriverNo.Text.ToString();
            string strOtherName = "";

            if (txtReferenceNo.Text.Trim().Length == 0)
                txtReferenceNo.Text = "0";

            if (txtTotalFreight.Text.ToString().Trim().Length == 0)
                txtTotalFreight.Text = "0.00";
            if (txtAdvancePaid.Text.ToString().Trim().Length == 0)
                txtAdvancePaid.Text = "0.00";
            if (txtToPay.Text.ToString().Trim().Length == 0)
                txtToPay.Text = "0.00";
            if (txtLoadingCharges.Text.ToString().Trim().Length == 0)
                txtLoadingCharges.Text = "0.00";
            if (!flagUpdate)
            {
           
                GenerateNewTrnNo();
                strCMD = " INSERT INTO DL_CREDITNOTE_HEAD( DLCNH_COMPANY_CODE," +
                                                                "DLCNH_STATE_CODE," +
                                                                "DLCNH_BRANCH_CODE," +
                                                                "DLCNH_FIN_YEAR," +
                                                                "DLCNH_DOCUMENT_MONTH," +
                                                                "DLCNH_TRN_TYPE," +
                                                                "DLCNH_TRN_NUMBER," +
                                                                "DLCNH_TRN_DATE," +
                                                                "DLCNH_REFERENCE_NUMBER," +
                                                                "DLCNH_VEHICLE_SOURCE," +
                                                                "DLCNH_DEALER_CODE," +
                                                                "DLCNH_REF_ECODE," +
                                                                "DLCNH_TRIP_OR_LR_NUMBER," +
                                                                "DLCNH_VEHICLE_NUMBER," +
                                                                "DLCNH_TRANSPORTER_NAME," +
                                                                "DLCNH_DRIVER_NAME," +
                                                                "DLCNH_TOTAL_FREIGHT," +
                                                                "DLCNH_ADVANCE_PAID," +
                                                                "DLCNH_TO_PAY_FREIGHT," +
                                                                "DLCNH_LOADING_CHARGES," +
                                                                "DLCNH_DISCOUNT_AMOUNT," +
                                                                "DLCNH_NET_SALES," +
                                                                "DLCNH_VAT_AMOUNT," +
                                                                "DLCNH_BED_AMOUNT," +
                                                                "DLCNH_SED_AMOUNT," +
                                                                "DLCNH_CESS_AMOUNT," +
                                                                "DLCNH_CRNOTE_AMOUNT," +
                                                                "DLCNH_REMARKS," +
                                                                "DLCNH_REASONS," +
                                                                "DLCNH_CREATED_BY," +
                                                                "DLCNH_CREATED_DATE) VALUES(" +
                                                                "'" + CommonData.CompanyCode +
                                                                "','" + CommonData.StateCode +
                                                                "','" + CommonData.BranchCode +
                                                                "','" + CommonData.FinancialYear +
                                                                "','" + sDocMonth +
                                                                "','" + cbTrnType.SelectedValue.ToString() +
                                                                "','" + txtTransactionNo.Text +
                                                                "','" + dtpReceivedDate.Value.ToString("dd/MMM/yyyy") +
                                                                "','" + txtReferenceNo.Text +
                                                                "','" + cbVehicleType.Text +
                                                                "'," + cbAccountName.SelectedValue.ToString() +
                                                                ", " + Convert.ToInt32(txtEcodeSearch.Text) +
                                                                ", '" + txtTripLRNo.Text +
                                                                "', '" + txtVehicleNo.Text +
                                                                "', '" + txtTransporter.Text +
                                                                "', '" + strDriver +
                                                                "','" + txtTotalFreight.Text +
                                                                "','" + txtAdvancePaid.Text +
                                                                "','" + txtToPay.Text +
                                                                "','" + txtLoadingCharges.Text +
                                                                "','" + txtDscRcvdAmt.Text +
                                                                "','" + netSales.ToString() +
                                                                "','" + txtVatRcvdAmt.Text +
                                                                "','" + 0 +
                                                                "','" + 0 +
                                                                "','" + 0 +
                                                                "','" + txtTotRecivedAmt.Text +
                                                                "','" + txtRemarks.Text +
                                                                "','" + cbReason.SelectedValue.ToString() +
                                                                "','" + CommonData.LogUserId +
                                                                "',getdate()" +
                                                                ")";

            }
            else
            {

                //string strSQL = "SELECT DLCND_PRODUCT_ID FROM DL_CREDITNOTE_DETL WHERE DLCND_TRN_NUMBER='"+txtTransactionNo.Text+"'";
                //objSQLdb = new SQLDB();
                //DataTable dt = objSQLdb.ExecuteDataSet(strSQL).Tables[0];
                //if(dt.Rows.Count>0)
                //{
                //    for (int i = 0; i < dt.Rows.Count;i++ )
                //    {
                //        objSQLdb = new SQLDB();
                //        SqlParameter[] param = new SqlParameter[6];
                //        DataSet ds = new DataSet();
                //        try
                //        {

                //            param[0] = objSQLdb.CreateParameter("@xCompany", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                //            param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                //            param[2] = objSQLdb.CreateParameter("@xFinYr", DbType.String, CommonData.FinancialYear, ParameterDirection.Input);
                //            param[3] = objSQLdb.CreateParameter("@xDocType", DbType.String, cbTrnType.SelectedValue.ToString(), ParameterDirection.Input);
                //            param[4] = objSQLdb.CreateParameter("@xTrnNumber", DbType.String, txtTransactionNo.Text, ParameterDirection.Input);
                //            param[5] = objSQLdb.CreateParameter("@xProductId", DbType.String, dt.Rows[i]["DLCND_PRODUCT_ID"].ToString(), ParameterDirection.Input);

                //            ds = objSQLdb.ExecuteDataSet("FA_OU_UpdateBeforeUpdateCreditNote", CommandType.StoredProcedure, param);

                //        }
                //        catch (Exception ex)
                //        {
                //            MessageBox.Show(ex.Message);
                //        }
                //    }
                //}


                strCMD = " DELETE DL_CREDITNOTE_DETL" +
                               " WHERE DLCND_COMPANY_CODE='" + CommonData.CompanyCode +
                                   "' AND dlcnd_branch_code='" + CommonData.BranchCode +
                                   "' AND DLCND_TRN_NUMBER='" + txtTransactionNo.Text +
                                   "' AND DLCND_FIN_YEAR='" + CommonData.FinancialYear + "'";
                objSQLdb = new SQLDB();
                objSQLdb.ExecuteSaveData(strCMD);
                strCMD = "UPDATE DL_CREDITNOTE_HEAD SET DLCNH_TRN_TYPE='"+ cbTrnType.SelectedValue.ToString()+
                                                     "',DLCNH_TRN_DATE='" + dtpReceivedDate.Value.ToString("dd/MMM/yyyy") +
                                                     "',DLCNH_VEHICLE_SOURCE='" + cbVehicleType.Text +
                                                     "',DLCNH_DEALER_CODE='" + cbAccountName.SelectedValue.ToString() +
                                                     "',DLCNH_REF_ECODE='" + Convert.ToInt32(txtEcodeSearch.Text) +
                                                     "',DLCNH_TRIP_OR_LR_NUMBER='" + txtTripLRNo.Text +
                                                     "',DLCNH_VEHICLE_NUMBER='" + txtVehicleNo.Text +
                                                     "',DLCNH_TRANSPORTER_NAME='" + strDriver +
                                                     "',DLCNH_TOTAL_FREIGHT='" + txtTotalFreight.Text +
                                                     "',DLCNH_ADVANCE_PAID='" + txtAdvancePaid.Text +
                                                     "',DLCNH_TO_PAY_FREIGHT='" + txtToPay.Text +
                                                     "',DLCNH_LOADING_CHARGES='" + txtLoadingCharges.Text +
                                                     "',DLCNH_DISCOUNT_AMOUNT='" + txtDscRcvdAmt.Text +
                                                     "',DLCNH_NET_SALES='" + netSales.ToString() +
                                                     "',DLCNH_VAT_AMOUNT='" + txtVatRcvdAmt.Text +
                                                     "',DLCNH_BED_AMOUNT='"+0+
                                                     "',DLCNH_SED_AMOUNT='"+0+
                                                     "',DLCNH_CESS_AMOUNT='"+0+
                                                     "',DLCNH_CRNOTE_AMOUNT='" + txtTotRecivedAmt.Text +
                                                     "',DLCNH_REMARKS='" +txtRemarks.ToString() +
                                                     "',DLCNH_REASONS='" + cbReason.SelectedValue.ToString() +
                                                     "',DLCNH_LAST_MODIFIED_BY='"+CommonData.LogUserId+
                                                     "',DLCNH_LAST_MODIFIED_DATE=getdate()"+
                                                     " where DLCNH_TRN_NUMBER='"+txtTransactionNo.Text+
                                                     "' and DLCNH_COMPANY_CODE='"+CommonData.CompanyCode+
                                                     "' and DLCNH_STATE_CODE='"+CommonData.StateCode+
                                                     "' and DLCNH_BRANCH_CODE='"+CommonData.BranchCode+
                                                     "' and DLCNH_FIN_YEAR='"+CommonData.FinancialYear+
                                                     "' and DLCNH_DOCUMENT_MONTH='" + sDocMonth + "'";
            }
            try
            {
                objSQLdb = new SQLDB();
                iRes = objSQLdb.ExecuteSaveData(strCMD);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return iRes;
        }

        private bool CheckData()
        {
            bool flag = true;
            if (cbAccountName.SelectedIndex < 0)
            {
                flag = false;
                MessageBox.Show("Please Select Dealer", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }
            if (txtEname.Text.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Enter Correct Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }
            if (gvProductDetails.Rows.Count == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Invoices", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }
            int count = 0;
            for (int iVar = 0; iVar < gvProductDetails.Rows.Count;iVar++ )
            {
                if (gvProductDetails.Rows[iVar].Cells["ReceivedQty"].Value == null)
                {
                    gvProductDetails.Rows[iVar].Cells["ReceivedQty"].Value = 0;
                }
                if (Convert.ToDouble(gvProductDetails.Rows[iVar].Cells["ReceivedQty"].Value.ToString()) > 0)
                {
                    count++;
                }
            }
            if(count==0)
            {
                flag = false;
                MessageBox.Show("Please Enter Return Products Quantity", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }
            return flag;
        }

        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= gvProductDetails.Columns["ReceivedQty"].Index && e.ColumnIndex <= gvProductDetails.Columns["RecvdAmt"].Index)
            {
                if (gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
                }
                if (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["IssQty"].Value) < (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["ReceivedQty"].Value) + Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["CNprevQty"].Value)))
                {
                    MessageBox.Show("Received Quantity is Exceeded than the Issued Quantity", "SSERP");
                    gvProductDetails.Rows[e.RowIndex].Cells["ReceivedQty"].Value = "0";
                }
                CaluculateNetAmt(e.RowIndex);
            }
            //if (e.ColumnIndex == gvProductDetails.Columns["Vatper"].Index)
            //{
            //    if (gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
            //    {
            //        gvProductDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
            //    }
            //     CaluculateDisVat(e.RowIndex);
            //}
            CalculateTotals();
        }


        private void CaluculateNetAmt(int iRow)
        {
            double Qty = 0, Amt = 0, disc = 0, vat = 0;
            if (gvProductDetails.Rows[iRow].Cells["ReceivedQty"].Value.ToString() != "")
                Qty = Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["ReceivedQty"].Value);
            if (Qty >= 0 && Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["Rate"].Value) >= 0)
            {
                gvProductDetails.Rows[iRow].Cells["RcvdNetAmt"].Value = Qty * (Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["Rate"].Value));
                gvProductDetails.Rows[iRow].Cells["RcvdNetAmt"].Value = Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["RcvdNetAmt"].Value).ToString("f");
                gvProductDetails.Rows[iRow].Cells["RecvdAmt"].Value = Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["RcvdNetAmt"].Value).ToString("f");
            }
            if (Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["DiscRcvdPer"].Value) >= 0)
            {
                double amt = Qty * (Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["Rate"].Value));
                disc = (amt * Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["DiscRcvdPer"].Value)) / 100;

                int iDisc = Convert.ToInt32(Convert.ToDouble(disc).ToString("0"));
                if (disc > iDisc)
                {
                    iDisc++;
                }
                gvProductDetails.Rows[iRow].Cells["ReceivedDiscAmt"].Value = iDisc;

                gvProductDetails.Rows[iRow].Cells["RecvdAmt"].Value = amt - iDisc;
                gvProductDetails.Rows[iRow].Cells["RecvdAmt"].Value = Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["RecvdAmt"].Value).ToString("f");
            }
            if (Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["VatReceived"].Value) >= 0)
            {
                double amt = Qty * (Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["Rate"].Value));

                disc = (amt * Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["DiscRcvdPer"].Value)) / 100;
                int iDisc = Convert.ToInt32(Convert.ToDouble(disc).ToString("0"));
                if (disc > iDisc)
                {
                    iDisc++;
                }
                double discAmt = amt - iDisc;
                gvProductDetails.Rows[iRow].Cells["ReceivedDiscAmt"].Value = iDisc;

                vat = (discAmt * Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["VatReceived"].Value)) / 100;
                int iVat = Convert.ToInt32(Convert.ToDouble(vat).ToString("0"));
                if (vat > iVat)
                {
                    iVat++;
                }
                gvProductDetails.Rows[iRow].Cells["ReceivedVatAmt"].Value = iVat;
                gvProductDetails.Rows[iRow].Cells["RecvdAmt"].Value = discAmt + iVat;

                gvProductDetails.Rows[iRow].Cells["RecvdAmt"].Value = Convert.ToDouble(gvProductDetails.Rows[iRow].Cells["RecvdAmt"].Value).ToString("f");
            }
        }

        private void CalculateTotals()
        {
            double netAmt = 0;
            netSales = 0;
            double totalQty = 0;
            double dicAmt = 0, vatAmt = 0, count = 0, disPer = 0, vatPer = 0;
            if (gvProductDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    if (gvProductDetails.Rows[i].Cells["ReceivedQty"].Value == null)
                    {
                        gvProductDetails.Rows[i].Cells["ReceivedQty"].Value = 0;
                    }
                    if (gvProductDetails.Rows[i].Cells["ReceivedQty"].Value != null && gvProductDetails.Rows[i].Cells["RecvdAmt"].Value != null)
                    {
                        netAmt += Convert.ToDouble(gvProductDetails.Rows[i].Cells["RecvdAmt"].Value);

                        totalQty += Convert.ToDouble(gvProductDetails.Rows[i].Cells["ReceivedQty"].Value);

                        netSales += Convert.ToDouble(gvProductDetails.Rows[i].Cells["RcvdNetAmt"].Value);


                    }

                    if (gvProductDetails.Rows[i].Cells["ReceivedQty"].Value  != "" && gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Amount"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["DiscRcvdPer"].Value != null)
                    {
                        disPer += Convert.ToDouble(gvProductDetails.Rows[i].Cells["DiscRcvdPer"].Value);
                    }
                    if (gvProductDetails.Rows[i].Cells["ReceivedQty"].Value != ""  && gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["Amount"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["VatReceived"].Value != null)
                    {
                        vatPer += Convert.ToDouble(gvProductDetails.Rows[i].Cells["VatReceived"].Value);
                    }
                    if (gvProductDetails.Rows[i].Cells["ReceivedQty"].Value != "" && gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["NetAmt"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["DiscRcvdPer"].Value != null)
                    {
                        dicAmt += Convert.ToDouble(gvProductDetails.Rows[i].Cells["ReceivedDiscAmt"].Value);
                    }
                    if (gvProductDetails.Rows[i].Cells["ReceivedQty"].Value != "" && gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["NetAmt"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["VatReceived"].Value != null)
                    {
                        vatAmt += Convert.ToDouble(gvProductDetails.Rows[i].Cells["ReceivedVatAmt"].Value);
                    }
                    if (Convert.ToDouble(gvProductDetails.Rows[i].Cells["ReceivedQty"].Value.ToString()) > 0)
                    {
                        count++;
                    }
                }
            }
            txtRecvdDiscPer.Text = (Convert.ToDouble(disPer) / count).ToString("f");
            txtRecvdVatPer.Text = (Convert.ToDouble(vatPer) / count).ToString("f");
            txtTotRecivedAmt.Text = Convert.ToDouble(netAmt).ToString("f");
            txtTotRecvdQty.Text = Convert.ToDouble(totalQty).ToString("f");
            txtDscRcvdAmt.Text = Convert.ToDouble(dicAmt).ToString("f");
            txtVatRcvdAmt.Text = Convert.ToDouble(vatAmt).ToString("f");
        }

        private void cbInvoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbInvoices.SelectedIndex > 0 && cbAccountName.SelectedIndex >= 0)
            {
                FillProducts();
            }
            else
            {
                cbProducts.DataSource = null;
            }
        }

        private void FillProducts()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            cbProducts.DataSource = null;
            try
            {
                //string strCommand = "SELECT CM_COMPANY_NAME,CM_COMPANY_CODE FROM COMPANY_MAS WHERE CM_COMPANY_CODE in ('SATL')";
                param[0] = objSQLdb.CreateParameter("@sTrnNumber", DbType.String, cbInvoices.SelectedValue.ToString(), ParameterDirection.Input);
                dt = objSQLdb.ExecuteDataSet("GetDealerProducts", CommandType.StoredProcedure, param).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";


                    dt.Rows.InsertAt(dr, 0);
                    cbProducts.DataSource = dt;
                    cbProducts.DisplayMember = "productName";
                    cbProducts.ValueMember = "DLDD_PRODUCT_ID";
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

        private void cbVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVehicleType.Text == "OWN")
            {
                cbDriverEcode.Visible = true;
                txtDriverNo.Visible = false;
                txtTripLRNo.Enabled = true;
                txtDriverNo.Enabled = true;
                txtVehicleNo.Enabled = true;
                txtTransporter.Enabled = true;
            }
            else if (cbVehicleType.Text == "BYHAND")
            {
                txtTripLRNo.Enabled = false;
                txtDriverNo.Enabled = false;
                txtVehicleNo.Enabled = false;
                txtTransporter.Enabled = false;
                cbDriverEcode.Visible = false;
                txtDriverNo.Visible = true;
            }
            else
            {
                cbDriverEcode.Visible = false;
                txtTripLRNo.Enabled = true;
                txtDriverNo.Enabled = true;
                txtVehicleNo.Enabled = true;
                txtTransporter.Enabled = true;
                txtDriverNo.Visible = true;
            }
        }

        private void txtTotalFreight_KeyPress(object sender, KeyPressEventArgs e)
        {
            RestrictToDigits(e);
        }

        private void RestrictToDigits(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false)
                e.Handled = true;
            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtAdvancePaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            RestrictToDigits(e);
        }

        private void txtAdvancePaid_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTotalFreight.Text.ToString().Length > 0 && txtAdvancePaid.Text.ToString().Length > 0)
            {
                if (Convert.ToDouble(txtTotalFreight.Text.ToString()) - Convert.ToDouble(txtAdvancePaid.Text.ToString()) < 0)
                    e.Handled = false;
                else
                    txtToPay.Text = Convert.ToString(Convert.ToDouble(txtTotalFreight.Text.ToString()) - Convert.ToDouble(txtAdvancePaid.Text.ToString()));
            }
            else if (txtTotalFreight.Text.ToString().Length > 0 && txtAdvancePaid.Text.ToString().Length == 0)
            {
                txtToPay.Text = txtTotalFreight.Text.ToString();
            }
        }

        private void txtLoadingCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            RestrictToDigits(e);
        }

        private void txtInvoiceSerch_KeyUp(object sender, KeyEventArgs e)
        {
            FillDealerInvoices(txtInvoiceSerch.Text);
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            if (cbInvoices.SelectedIndex > 0 && cbProducts.SelectedIndex > 0)
            {
                objSQLdb = new SQLDB();
                DataTable dt = new DataTable();
                SqlParameter[] param = new SqlParameter[2];

                try
                {
                    //string strCommand = "SELECT CM_COMPANY_NAME,CM_COMPANY_CODE FROM COMPANY_MAS WHERE CM_COMPANY_CODE in ('SATL')";
                    param[0] = objSQLdb.CreateParameter("@sTrnNumber", DbType.String, cbInvoices.SelectedValue.ToString(), ParameterDirection.Input);
                    param[1] = objSQLdb.CreateParameter("@sSlNo", DbType.String, cbProducts.SelectedValue.ToString(), ParameterDirection.Input);
                    dt = objSQLdb.ExecuteDataSet("GetDlInvProductDetails", CommandType.StoredProcedure, param).Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                bool isExists = false;
                //foreach (DataGridViewRow row in gvProductDetails.Rows)
                //{
                //    if (row.Cells[gvProductDetails.Columns["InvoiceNo"].Index].Value.ToString().Equals(cbInvoices.SelectedValue.ToString()) && row.Cells[gvProductDetails.Columns["ProductID"].Index].Value.ToString().Equals(cbProducts.SelectedValue.ToString()))
                //    {
                //        gvProductDetails.Rows[row.Index].Selected = true;
                //        isExists = true;
                //        break;
                //    }
                //}
                for (int iRow = 0; iRow < gvProductDetails.Rows.Count; iRow++)
                {
                    if (gvProductDetails.Rows[iRow].Cells[gvProductDetails.Columns["InvoiceNo"].Index].Value.ToString() == ((System.Data.DataRowView)(cbInvoices.SelectedItem)).Row.ItemArray[0].ToString() &&
                         gvProductDetails.Rows[iRow].Cells[gvProductDetails.Columns["ProductID"].Index].Value.ToString() == cbProducts.SelectedValue.ToString())
                    {
                        isExists = true;
                        break;
                    }
                }
                if (!isExists)
                    AddProductsToGrid(dt);
            }
        }

        private void txtDiscPer_Validated(object sender, EventArgs e)
        {
            if (txtRecvdDiscPer.Text.Length > 0)
            {
                for (int iVar = 0; iVar < gvProductDetails.Rows.Count; iVar++)
                {
                    gvProductDetails.Rows[iVar].Cells["DiscRcvdPer"].Value = txtRecvdDiscPer.Text;
                    CaluculateNetAmt(iVar);
                }
                CalculateTotals();
            }
        }

        private void txtVatPer_Validated(object sender, EventArgs e)
        {
            if (txtRecvdVatPer.Text.Length > 0)
            {
                for (int iVar = 0; iVar < gvProductDetails.Rows.Count; iVar++)
                {
                    gvProductDetails.Rows[iVar].Cells["VatReceived"].Value = txtRecvdVatPer.Text;
                    CaluculateNetAmt(iVar);
                }
                CalculateTotals();
            }
        }

        private void txtReferenceNo_Validated(object sender, EventArgs e)
        {
            if(txtReferenceNo.Text.Trim().Length>0)
            {
                FillSalesCN(txtReferenceNo.Text);
            }
        }
        private void FillSalesCN(string sRefNo)
        {
            objDealerInfo = new DealerInfo();
            Hashtable ht = new Hashtable();
            try
            {
                ht = objDealerInfo.GetDlSalesCreditNoteDetails(sRefNo);
                DataTable dtH = (DataTable)ht["Head"];
                DataTable dtD = (DataTable)ht["Detail"];
                FillTransactionHead(dtH, dtD);
                dtH = null;
                dtD = null;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                objDealerInfo = null;
                ht = null;
            }
        }

        private bool FillTransactionHead(DataTable dtH, DataTable dtD)
        {
            bool isData = false;
            
            try
            {
                if (dtH.Rows.Count > 0)
                {
                    flagUpdate = true;
                    //strTransactionNo = dtH.Rows[0]["TrnNumber"].ToString();
                    txtTransactionNo.Text = dtH.Rows[0]["TrnNumber"] + "";
                    txtReferenceNo.Text = dtH.Rows[0]["RefNo"] + "";
                    dtpReceivedDate.Value = Convert.ToDateTime(Convert.ToDateTime(dtH.Rows[0]["TrnDate"]).ToString("dd/MM/yyyy"));
                    txtTotalFreight.Text = dtH.Rows[0]["TotalFreight"] + "";
                    txtAdvancePaid.Text = dtH.Rows[0]["AdvPaid"] + "";
                    txtToPay.Text = dtH.Rows[0]["ToPayFreight"] + "";
                    txtLoadingCharges.Text = dtH.Rows[0]["LoadingCharges"] + "";
                    cbTrnType.Text = dtH.Rows[0]["TrnType"] + "";
                    txtAccountSearch.Text = dtH.Rows[0]["DealerCode"] + "";
                    txtEcodeSearch.Text = dtH.Rows[0]["RefCode"] + "";
                    cbReason.SelectedValue = dtH.Rows[0]["Reasons"] + "";
                    txtRemarks.Text = dtH.Rows[0]["Remarks"] + "";
                    //strECode = dtHead.Rows[0]["ToEcode"] + "";
                    //cbEcode.SelectedIndex = objGeneral.GetComboBoxSelectedIndex(strECode, cbEcode);

                    cbVehicleType.SelectedValue = dtH.Rows[0]["VehicleSource"] + "";

                    if (cbVehicleType.Text == "OWN")
                    {
                        cbDriverEcode.Visible = true;
                        cbDriverEcode.Text = dtH.Rows[0]["DriverName"] + "";
                    }
                    else
                    {
                        cbDriverEcode.Visible = false;
                        txtDriverNo.Text = dtH.Rows[0]["DriverName"] + "";
                    }

                    txtTripLRNo.Text = Convert.ToString(dtH.Rows[0]["TripLrNo"]);
                    txtVehicleNo.Text = Convert.ToString(dtH.Rows[0]["VehicleNo"]);

                    txtTransporter.Text = dtH.Rows[0]["TransporterName"] + "";
                    FillTransactionDetl(dtD);
                }
                else
                {
                    flagUpdate = false;
                    gvProductDetails.Rows.Clear();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dtH = null;
            }
            return isData;
        }

        private void FillTransactionDetl(DataTable dtD)
        {
           
            try
            {
                gvProductDetails.Rows.Clear();
                if (dtD.Rows.Count > 0)
                {
                    for (int intRow = 0; intRow < dtD.Rows.Count; intRow++)
                    {
                        // if(cbIndents.SelectedIndex==-1 && dt.Rows[intRow]["IndentNo"].ToString()!="0")
                        //     cbIndents.SelectedIndex = objGeneral.GetComboBoxSelectedIndex(dt.Rows[intRow]["IndentNo"].ToString(), cbIndents);

                        gvProductDetails.Rows.Add();
                        gvProductDetails.Rows[intRow].Cells["SLNO"].Value = intRow + 1;
                        gvProductDetails.Rows[intRow].Cells["CompCode"].Value = dtD.Rows[intRow]["AgCompCode"].ToString();
                        gvProductDetails.Rows[intRow].Cells["StateCode"].Value = dtD.Rows[intRow]["AgStateCode"].ToString();
                        gvProductDetails.Rows[intRow].Cells["BranchCode"].Value = dtD.Rows[intRow]["AgBranchCode"].ToString();
                        gvProductDetails.Rows[intRow].Cells["FinYear"].Value = dtD.Rows[intRow]["AgFinYear"].ToString();
                        gvProductDetails.Rows[intRow].Cells["DocMonth"].Value = dtD.Rows[intRow]["AgDocMonth"].ToString();
                        gvProductDetails.Rows[intRow].Cells["TrnType"].Value = dtD.Rows[intRow]["AgTrnType"].ToString();
                        gvProductDetails.Rows[intRow].Cells["TrnNumber"].Value = dtD.Rows[intRow]["AgTrnNO"].ToString();
                        gvProductDetails.Rows[intRow].Cells["Date"].Value = dtD.Rows[intRow]["AgTrnDate"].ToString();
                        gvProductDetails.Rows[intRow].Cells["InvoiceNo"].Value = dtD.Rows[intRow]["AgRefNO"].ToString();
                        gvProductDetails.Rows[intRow].Cells["Category"].Value = dtD.Rows[intRow]["CategoryName"].ToString();
                        gvProductDetails.Rows[intRow].Cells["Product"].Value = dtD.Rows[intRow]["ProductName"].ToString();
                        gvProductDetails.Rows[intRow].Cells["BatchNo"].Value = dtD.Rows[intRow]["AgBatchNo"].ToString();
                        gvProductDetails.Rows[intRow].Cells["IssQty"].Value = dtD.Rows[intRow]["InvIssQty"].ToString();
                        gvProductDetails.Rows[intRow].Cells["ProductId"].Value = dtD.Rows[intRow]["ProductId"].ToString();
                        gvProductDetails.Rows[intRow].Cells["Rate"].Value = dtD.Rows[intRow]["InvRate"].ToString();
                        gvProductDetails.Rows[intRow].Cells["Amount"].Value = dtD.Rows[intRow]["InvAmt"].ToString();
                        gvProductDetails.Rows[intRow].Cells["Discper"].Value = dtD.Rows[intRow]["InvDisPer"].ToString();
                        gvProductDetails.Rows[intRow].Cells["DiscAmount"].Value = dtD.Rows[intRow]["InvDisAmt"].ToString();
                        gvProductDetails.Rows[intRow].Cells["Vatper"].Value = dtD.Rows[intRow]["InvVatPer"].ToString();
                        gvProductDetails.Rows[intRow].Cells["VatAmount"].Value = dtD.Rows[intRow]["InvVatAmt"].ToString();
                        gvProductDetails.Rows[intRow].Cells["NetAmt"].Value = dtD.Rows[intRow]["InvNetAmt"].ToString();


                        gvProductDetails.Rows[intRow].Cells["ReceivedQty"].Value = dtD.Rows[intRow]["IssQty"].ToString();
                        gvProductDetails.Rows[intRow].Cells["RcvdNetAmt"].Value = dtD.Rows[intRow]["IssAmt"].ToString();
                        gvProductDetails.Rows[intRow].Cells["DiscRcvdPer"].Value = dtD.Rows[intRow]["DiscPer"].ToString();
                        gvProductDetails.Rows[intRow].Cells["ReceivedDiscAmt"].Value = dtD.Rows[intRow]["DiscAmt"].ToString();
                        gvProductDetails.Rows[intRow].Cells["VatReceived"].Value = dtD.Rows[intRow]["VatPer"].ToString();
                        gvProductDetails.Rows[intRow].Cells["ReceivedVatAmt"].Value = dtD.Rows[intRow]["VatAmt"].ToString();
                        gvProductDetails.Rows[intRow].Cells["RecvdAmt"].Value = dtD.Rows[intRow]["NetAmt"].ToString();

                        CalculateTotals();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DC SP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                
            }
        }

        private void gvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvProductDetails.Columns["Delete"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    DataGridViewRow dgvr = gvProductDetails.Rows[e.RowIndex];
                    gvProductDetails.Rows.Remove(dgvr);
                    OrderSlNo();
                    CalculateTotals();
                }
            }
        }

        private void OrderSlNo()
        {
            if (gvProductDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    gvProductDetails.Rows[i].Cells["SLNO"].Value = i + 1;
                }
            }
        }
    }



}

