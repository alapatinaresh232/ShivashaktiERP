﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSTrans;
using System.Collections;

namespace SDMS
{
    public partial class CashPayment : Form
    {

        SQLDB objSQLdb = null;
        InvoiceDB objInvDB = null;
        public ReceiptVoucherList objVoucher = new ReceiptVoucherList();
        public DataTable dtBillRecievedDetails = new DataTable();
        public int iSiNo = 1;
        private bool flagUpdate = false;
        string strCompCode, strBranchCode, strFinYear, strDocType, strVoucherId = null;
        public CashPayment()
        {
            InitializeComponent();
        }
        public CashPayment(string sCompCode, string sBranchCode, string sFinYear, string sDocType, string sVoucherId)
        {
            InitializeComponent();
            strCompCode = sCompCode;
            strBranchCode= sBranchCode;
            strFinYear= sFinYear;
            strDocType= sDocType;
            strVoucherId = sVoucherId;
            flagUpdate = true;
        }
        private void CashPayment_Load(object sender, EventArgs e)
        {
            dtpVoucherDate.Value = DateTime.Today;
            cbAccType.SelectedIndex = -1;
            FillCommanyData();
            GetAccountTypes();
            GenerateVoucherId();

            #region CREATING TABLE FOR BILLRECEIVEDDETAILS

            dtBillRecievedDetails.Columns.Add("SlNo");
            dtBillRecievedDetails.Columns.Add("isinos");
            dtBillRecievedDetails.Columns.Add("AccId");
            dtBillRecievedDetails.Columns.Add("AccName");
            dtBillRecievedDetails.Columns.Add("PaymentMode");
            dtBillRecievedDetails.Columns.Add("ChqRefNo");
            dtBillRecievedDetails.Columns.Add("CheqDate");
            dtBillRecievedDetails.Columns.Add("Remarks");
            dtBillRecievedDetails.Columns.Add("AmtReceived");
            dtBillRecievedDetails.Columns.Add("VOUCHER_ID");
            dtBillRecievedDetails.Columns.Add("VOUCHER_DATE");
            dtBillRecievedDetails.Columns.Add("DEBIT_CREDIT");
            dtBillRecievedDetails.Columns.Add("MAIN_COST_CENTRE_ID");
            dtBillRecievedDetails.Columns.Add("SUB_COST_CENTRE_ID");
            dtBillRecievedDetails.Columns.Add("VC_VOUCHER_NO");
            dtBillRecievedDetails.Columns.Add("VC_CASH_BANK_ID");
            dtBillRecievedDetails.Columns.Add("VC_VOUCHER_AMOUNT");
            dtBillRecievedDetails.Columns.Add("VC_VOUCHER_TYPE");
            dtBillRecievedDetails.Columns.Add("VC_APPROVED");
            dtBillRecievedDetails.Columns.Add("VC_POSTED");

            #endregion

            if (flagUpdate == true)
            {
                FillData(strCompCode, strBranchCode, strFinYear, strDocType, strVoucherId);
            }
        }
        private void FillData(string sCompCode, string sBranchCode, string sFinYear, string sDocType, string sVoucherId)
        {
            objInvDB = new InvoiceDB();
            Hashtable ht = new Hashtable();
            try
            {
                ht = objInvDB.GetRecieptVoucherData(sCompCode, sBranchCode, sFinYear, sDocType, sVoucherId);
                DataTable dtH = (DataTable)ht["Head"];
                DataTable dtD1 = (DataTable)ht["Detail1"];
                DataTable dtD2 = (DataTable)ht["Detail2"];
                FillCachPaymentHead(dtH, dtD1, dtD2);
                dtH = null;
                dtD1 = null;
                dtD2 = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objInvDB = null;
                ht = null;

            }
        }
        private void FillCachPaymentHead(DataTable dtH, DataTable dtD1, DataTable dtD2)
        {
            try
            {
                if (dtH.Rows.Count > 0)
                {
                    cbCompany.SelectedValue = dtH.Rows[0]["comp_code"] + "";
                    cbBranch.SelectedValue = dtH.Rows[0]["branch_code"] + "";
                    txtVoucherId.Text = dtH.Rows[0]["voucher_id"] + "";
                    cbAccType.SelectedValue = dtD1.Rows[0]["VC_CASH_BANK_ID"] + "";
                    dtpVoucherDate.Value = Convert.ToDateTime(Convert.ToDateTime(dtH.Rows[0]["voucher_date"]).ToString("dd/MM/yyyy"));
                    txtDesc1.Text = dtH.Rows[0]["narration1"] + "";
                    txtDesc2.Text = dtH.Rows[0]["narration2"] + "";
                    txtVoucherNo.Text = dtH.Rows[0]["voucher_no"] + "";
                    txtTotRecvdAmt.Text = dtD1.Rows[0]["VC_VOUCHER_AMOUNT"] + "";
                    FillCashPaymentDetail(dtD1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dtH = null;
            }
        }

        private void FillCashPaymentDetail(DataTable dtD1)
        {
            try
            {
                if (dtD1.Rows.Count > 0)
                {
                    dtD1.Columns.Add("isinos").SetOrdinal(1);
                    for (int i = 0; i < dtD1.Rows.Count; i++)
                    {
                        dtD1.Rows[i]["isinos"] = i + 1;
                    }
                    dtBillRecievedDetails = dtD1;
                    GetBillDetails();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dtD1 = null;
            }

        }
        private void btnBillDetails_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
            CashPaymentDetails obj = new CashPaymentDetails();
            obj.objCashPayment = this;
            obj.Show();
            }
        }

        private bool CheckData()
        {
            bool flag = true;
            if (cbCompany.SelectedIndex == 0)
            {
                MessageBox.Show("Select Company", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbCompany.Focus();
                flag = false;
            }
            else if (cbBranch.SelectedIndex == 0)
            {
                MessageBox.Show("Select Branch", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbBranch.Focus();
                flag = false;
            }
            else if (cbAccType.SelectedIndex == 0)
            {
                MessageBox.Show("Select CASH AC", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cbAccType.Focus();
                flag = false;
            }
            return flag; 
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

       

        private void FillCommanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbCompany.DataSource = null;
            cbBranch.DataSource = null;
            try
            {
                string strCommand = "SELECT CM_COMPANY_NAME,CM_COMPANY_CODE FROM COMPANY_MAS WHERE CM_COMPANY_CODE in ('SATL')";

                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbCompany.DataSource = dt;
                    cbCompany.DisplayMember = "CM_COMPANY_NAME";
                    cbCompany.ValueMember = "CM_COMPANY_CODE";
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

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                FillBranchData();
            }
            else
            {
                cbBranch.DataSource = null;
            }
        }

        private void FillBranchData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbBranch.DataSource = null;
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {
                    string strCommand = "SELECT BRANCH_CODE as branchCode,BRANCH_NAME FROM " +
                                        "BRANCH_MAS WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "'";
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
                    cbBranch.ValueMember = "branchCode";

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
        private void GenerateVoucherId()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                String strCommand = "SELECT ISNULL(MAX(VCO_VOUCHER_ID),0)+1 VoucherId FROM FA_VOUCHER_OTHERS";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtVoucherId.Text = dt.Rows[0]["VoucherId"] + "";
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
        private void GetAccountTypes()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                String strCmd = "SELECT AM_ACCOUNT_ID, AM_ACCOUNT_NAME+'-'+AM_ACCOUNT_ID AccountName FROM FA_ACCOUNT_MASTER WHERE AM_COMPANY_CODE='" + CommonData.CompanyCode + "' AND AM_ACCOUNT_TYPE_ID IN('CASH')";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";
                    dt.Rows.InsertAt(dr, 0);

                    cbAccType.DataSource = dt;
                    cbAccType.DisplayMember = "AccountName";
                    cbAccType.ValueMember = "AM_ACCOUNT_ID";
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
        public void CaluculatingTotalRcvdAmt()
        {
            double totalRcvdAmt = 0;
            if (gvBillDetails.Rows.Count > 0)
            {
                for (int iRow = 0; iRow < gvBillDetails.Rows.Count; iRow++)
                {
                    totalRcvdAmt += Convert.ToDouble(gvBillDetails.Rows[iRow].Cells["AmtReceived"].Value);
                }
            }
            txtTotRecvdAmt.Text = totalRcvdAmt.ToString();
        }
        public void GetBillDetails()
        {
            int intRow = 1;
            gvBillDetails.Rows.Clear();
            for (int i = 0; i < dtBillRecievedDetails.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSlNo = new DataGridViewTextBoxCell();
                cellSlNo.Value = i + 1;
                dtBillRecievedDetails.Rows[i]["SlNo"] = i + 1;
                tempRow.Cells.Add(cellSlNo);

                DataGridViewCell cellAccNo = new DataGridViewTextBoxCell();
                cellAccNo.Value = dtBillRecievedDetails.Rows[i]["AccId"];
                tempRow.Cells.Add(cellAccNo);

                DataGridViewCell cellAccName = new DataGridViewTextBoxCell();
                cellAccName.Value = dtBillRecievedDetails.Rows[i]["AccName"];
                tempRow.Cells.Add(cellAccName);

                DataGridViewCell cellPaymentMode = new DataGridViewTextBoxCell();
                cellPaymentMode.Value = dtBillRecievedDetails.Rows[i]["PaymentMode"];
                tempRow.Cells.Add(cellPaymentMode);

                DataGridViewCell cellChqRefNo = new DataGridViewTextBoxCell();
                cellChqRefNo.Value = dtBillRecievedDetails.Rows[i]["ChqRefNo"];
                tempRow.Cells.Add(cellChqRefNo);

                DataGridViewCell cellDate = new DataGridViewTextBoxCell();
                cellDate.Value = Convert.ToDateTime(dtBillRecievedDetails.Rows[i]["CheqDate"]).ToShortDateString();
                tempRow.Cells.Add(cellDate);

                DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                cellRemarks.Value = dtBillRecievedDetails.Rows[i]["Remarks"];
                tempRow.Cells.Add(cellRemarks);

                DataGridViewCell cellAmtReceived = new DataGridViewTextBoxCell();
                cellAmtReceived.Value = dtBillRecievedDetails.Rows[i]["AmtReceived"];
                tempRow.Cells.Add(cellAmtReceived);

                //DataGridViewCell cellAdjustType = new DataGridViewTextBoxCell();
                //cellAdjustType.Value = dtBillRecievedDetails.Rows[i]["AdjustmentType"];
                //tempRow.Cells.Add(cellAdjustType);

                DataGridViewCell cellisinos = new DataGridViewTextBoxCell();
                cellisinos.Value = dtBillRecievedDetails.Rows[i]["isinos"];
                tempRow.Cells.Add(cellisinos);

                gvBillDetails.Rows.Add(tempRow);
            }
            CaluculatingTotalRcvdAmt();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int intSave = 0;
            if (CheckData())
            {
                if (gvBillDetails.Rows.Count == 0)
                {
                    MessageBox.Show("Enter Details");
                }
                else if (gvBillDetails.Rows.Count > 0)
                {
                    if (SaveHeadData() > 0)
                    {
                        intSave = SaveDetlVoucharData();
                    }
                    else
                    {
                        try
                        {
                            string strSql = "DELETE from FA_VOUCHER_OTHERS" +
                                  " WHERE VCO_COMPANY_CODE='" + CommonData.CompanyCode +
                                  "' AND VCO_BRANCH_CODE='" + CommonData.BranchCode +
                                  "' AND VCO_VOUCHER_ID='" + txtVoucherId.Text +
                                  "'  AND VCO_FIN_YEAR='" + CommonData.FinancialYear + "'";
                            objSQLdb = new SQLDB();
                            objSQLdb.ExecuteSaveData(strSql);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    if (intSave > 0)
                    {
                        //try
                        //{
                        //    //PROCEDURE TO ADJUSTING OUTSTADING AMOUNT

                        //    objInvDB = new InvoiceDB();

                        //    objInvDB.UpdatingOutStandingAmt(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, "CR", Convert.ToInt32(txtVoucherId.Text));

                            MessageBox.Show("Data Saved Successfully ", "SDMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    GenerateVoucherId();
                        //    btnCancel_Click(null, null);
                        //}
                        //catch (Exception ex)
                        //{
                        //    //string strSql = "DELETE from FA_VOUCHER_OTHERS" +
                        //    //      " WHERE VCO_COMPANY_CODE='" + CommonData.CompanyCode +
                        //    //      "' AND VCO_BRANCH_CODE='" + CommonData.BranchCode +
                        //    //      "' AND VCO_VOUCHER_ID='" + txtVoucherId.Text +
                        //    //      "'  AND VCO_FIN_YEAR='" + CommonData.FinancialYear + "'";
                        //    //objSQLdb = new SQLDB();
                        //    //objSQLdb.ExecuteSaveData(strSql);
                        //    MessageBox.Show(ex.ToString());
                        //    MessageBox.Show("Data Not Saved ", "SDMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                        //finally
                        //{
                       // }
                        GenerateVoucherId();
                        btnCancel_Click(null, null);

                    }
                    else
                    {
                        MessageBox.Show("Data Not Saved ", "SDMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
               
        }
        public int SaveHeadData()
        {
            int iRes = 0;
            string strSQL = "";
            try
            {
                if (flagUpdate == false)
                {
                    strSQL = " INSERT INTO FA_VOUCHER_OTHERS(VCO_COMPANY_CODE" +
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
                                    ") VALUES('" + CommonData.CompanyCode +
                                    "','" + CommonData.BranchCode +
                                    "','" + CommonData.FinancialYear +
                                    "','DR'" +
                                    ",'" + txtVoucherId.Text +
                                    "','" + dtpVoucherDate.Value.ToString("dd/MMM/yyyy") +
                                    "','" + txtDesc1.Text +
                                    "','" + txtDesc2.Text +
                                    "','" + txtVoucherNo.Text +
                                    "','" +
                                    "','" + CommonData.LogUserId +
                                    "',getdate())";
                }
                else
                {


                    //objInvDB = new InvoiceDB();

                    //objInvDB.BeforeUpdatingOutStandingAmt(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, "CR", Convert.ToInt32(txtVoucherId.Text));
                    strSQL += " DELETE FROM FA_VOUCHER WHERE VC_COMPANY_CODE='" + CommonData.CompanyCode +
                              "' AND VC_BRANCH_CODE='" + CommonData.BranchCode +
                              "' AND VC_FIN_YEAR='" + CommonData.FinancialYear +
                              "' AND VC_DOC_TYPE='DR'" +
                              " AND VC_VOUCHER_ID='" + txtVoucherId.Text + "'";

                    objSQLdb = new SQLDB();
                    iRes = objSQLdb.ExecuteSaveData(strSQL);
                    strSQL = "";

                    strSQL = " UPDATE FA_VOUCHER_OTHERS SET VCO_COMPANY_CODE='" + CommonData.CompanyCode +
                                    "',VCO_BRANCH_CODE='" + CommonData.BranchCode +
                                    "',VCO_FIN_YEAR='" + CommonData.FinancialYear +
                                    "',VCO_DOC_TYPE='DR'" +
                                    ",VCO_VOUCHER_DATE='" + dtpVoucherDate.Value.ToString("dd/MMM/yyyy") +
                                    "',VCO_NARRATION_1='" + txtDesc1.Text +
                                    "',VCO_NARRATION_2='" + txtDesc2.Text +
                                    "',VCO_EFFECT_NAME='" +
                                    "',VCO_VOUCHER_NO='" + txtVoucherNo.Text +
                                    "',VCO_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                    "',VCO_LAST_MODIFIED_DATE=getdate()" +
                                    " WHERE VCO_VOUCHER_ID='" + txtVoucherId.Text + "'";

                }
                objSQLdb = new SQLDB();
                iRes = objSQLdb.ExecuteSaveData(strSQL);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
            }
            return iRes;
        }
        public int SaveDetlVoucharData()
        {
            int intSave = 0;
            string strSQL = "";
            try
            {
                strSQL += " DELETE FROM FA_VOUCHER WHERE VC_COMPANY_CODE='" + CommonData.CompanyCode +
                          "' AND VC_BRANCH_CODE='" + CommonData.BranchCode +
                          "' AND VC_FIN_YEAR='" + CommonData.FinancialYear +
                          "' AND VC_DOC_TYPE='DR'" +
                          " AND VC_VOUCHER_ID='" + txtVoucherId.Text + "'";

                objSQLdb = new SQLDB();
                intSave = objSQLdb.ExecuteSaveData(strSQL);
                strSQL = "";
                for (int i = 0; i < dtBillRecievedDetails.Rows.Count; i++)
                {
                    strSQL += " INSERT INTO FA_VOUCHER( VC_COMPANY_CODE" +
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
                        "','DR'" +
                         ",'" + dtBillRecievedDetails.Rows[i]["VOUCHER_ID"] +
                         "','" + Convert.ToDateTime(dtBillRecievedDetails.Rows[i]["VOUCHER_DATE"].ToString()).ToString("dd/MMM/yyyy") +
                         "','" + (i + 1) +
                         "','" + dtBillRecievedDetails.Rows[i]["AccId"] +
                         "','" + dtBillRecievedDetails.Rows[i]["DEBIT_CREDIT"] +
                         "','" + dtBillRecievedDetails.Rows[i]["AmtReceived"] +
                         "','" + dtBillRecievedDetails.Rows[i]["MAIN_COST_CENTRE_ID"] +
                         "','" + dtBillRecievedDetails.Rows[i]["SUB_COST_CENTRE_ID"] +
                         "','" + dtBillRecievedDetails.Rows[i]["PaymentMode"] +
                         "','" + dtBillRecievedDetails.Rows[i]["ChqRefNo"] +
                         "','" + Convert.ToDateTime(dtBillRecievedDetails.Rows[i]["CheqDate"].ToString()).ToString("dd/MMM/yyyy") +
                         "','" + dtBillRecievedDetails.Rows[i]["Remarks"] +
                         "','" + dtBillRecievedDetails.Rows[i]["VC_VOUCHER_NO"] +
                         "','" + dtBillRecievedDetails.Rows[i]["VC_CASH_BANK_ID"] +
                         "','" + txtTotRecvdAmt.Text +
                         "','DR'" +
                         ",'N" +
                         "','N" +
                         "','" + CommonData.LogUserId +
                         "',GETDATE())";
                }
                strSQL += " INSERT INTO FA_VOUCHER( VC_COMPANY_CODE" +
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
                        "','DR'" +
                         ",'" + dtBillRecievedDetails.Rows[0]["VOUCHER_ID"] +
                         "','" + Convert.ToDateTime(dtBillRecievedDetails.Rows[0]["VOUCHER_DATE"].ToString()).ToString("dd/MMM/yyyy") +
                         "','" + 0 +
                         "','CASH" +
                         "','C" +
                         "','" + txtTotRecvdAmt.Text +
                         "','" +
                         "','" +
                         "','" + dtBillRecievedDetails.Rows[0]["PaymentMode"] +
                         "','" +
                         "','" + Convert.ToDateTime(dtBillRecievedDetails.Rows[0]["CheqDate"].ToString()).ToString("dd/MMM/yyyy") +
                         "','" +
                         "','" + txtVoucherNo.Text +
                         "','" + cbAccType.SelectedValue.ToString() +
                         "','" + txtTotRecvdAmt.Text +
                         "','DR'" +
                         ",'N'" +
                         ",'N'" +
                         ",'" + CommonData.LogUserId +
                         "',GETDATE())";

                intSave = objSQLdb.ExecuteSaveData(strSQL);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
            }
            return intSave;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagUpdate = false;
            cbCompany.SelectedIndex = 0;
            GenerateVoucherId();
            cbAccType.SelectedIndex = 0;
            txtDesc1.Text = "";
            txtDesc2.Text = "";
            gvBillDetails.Rows.Clear();
            dtBillRecievedDetails.Rows.Clear();
            txtTotRecvdAmt.Text = "";
            iSiNo = 1;
        }

        private void gvBillDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (gvBillDetails.Rows[e.RowIndex].Cells["Edit"].Value.ToString().Trim() != "")
                {
                    if (Convert.ToBoolean(gvBillDetails.Rows[e.RowIndex].Cells["Edit"].Selected) == true)
                    {
                        string RefNo = gvBillDetails.Rows[e.RowIndex].Cells[gvBillDetails.Columns["isinos"].Index].Value.ToString();
                        DataRow[] dr = dtBillRecievedDetails.Select("isinos='" + RefNo + "'");
                        CashPaymentDetails objPaymentDetails = new CashPaymentDetails(dr);
                        objPaymentDetails.objCashPayment= this;
                        objPaymentDetails.ShowDialog();
                        objPaymentDetails.isModifyFlag = true;
                    }
                }
                if (e.ColumnIndex == gvBillDetails.Columns["Delete"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        //DataRow[] drOut = dtOutStanding.Select("OU_ACCOUNT_ID='" + gvBillDetails.Rows[e.RowIndex].Cells["AccountId"].Value + "'");
                        //if (drOut.Length > 0)
                        //{

                        //    for (int iRow = 0; iRow < drOut.Length; iRow++)
                        //    {
                        //        int iSNo = Convert.ToInt32(drOut[iRow][0].ToString());
                        //        DataRow[] drBills = dtAgnstVoucherBill.Select("AG_BILL_NUMBER='" + dtOutStanding.Rows[iSNo - 1]["OU_BILL_NUMBER"].ToString() + "' AND isinos='" + gvBillDetails.Rows[e.RowIndex].Cells["isinos"].Value + "'");
                        //        if (drBills.Length > 0)
                        //        {
                        //            double OutStdgAmt = Convert.ToDouble(dtOutStanding.Rows[iSNo - 1]["OU_AMOUNT"].ToString());
                        //            double PaidAmt = Convert.ToDouble(dtOutStanding.Rows[iSNo - 1]["OU_AMT_PAID_RCVD"].ToString());
                        //            OutStdgAmt += Convert.ToDouble(drBills[0][15].ToString());

                        //            PaidAmt -= Convert.ToDouble(drBills[0][15].ToString());
                        //            dtOutStanding.Rows[iSNo - 1]["OU_AMOUNT"] = OutStdgAmt;
                        //            dtOutStanding.Rows[iSNo - 1]["OU_AMT_PAID_RCVD"] = PaidAmt;
                        //        }
                        //    }
                        //}

                        int SlNo = Convert.ToInt32(gvBillDetails.Rows[e.RowIndex].Cells[gvBillDetails.Columns["isinos"].Index].Value);


                        //DataRow[] dtRow = dtAgnstVoucherBill.Select("isinos=" + SlNo);

                        //for (int i = 0; i < dtRow.Length; i++)
                        //    dtAgnstVoucherBill.Rows.Remove(dtRow[i]);


                        DataRow[] drs = dtBillRecievedDetails.Select("isinos=" + SlNo);
                        dtBillRecievedDetails.Rows.Remove(drs[0]);




                        gvBillDetails.Rows.RemoveAt(e.RowIndex);
                        CaluculatingTotalRcvdAmt();
                        OrderSerialNo();

                        MessageBox.Show("Selected information Has Been Deleted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            
        }
        private void OrderSerialNo()
        {
            if (gvBillDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvBillDetails.Rows.Count; i++)
                {
                    gvBillDetails.Rows[i].Cells["SlNo"].Value = i + 1;
                }
            }
        }
    }
}
