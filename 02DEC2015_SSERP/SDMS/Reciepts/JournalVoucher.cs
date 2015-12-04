using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSTrans;

namespace SDMS
{
    public partial class JournalVoucher : Form
    {
        SQLDB objSQLdb=null;
        InvoiceDB objInvDB = null;
        IndentDB objIndent = null;
        public DataTable dtBillRecievedDetails = new DataTable();
        public DataTable dtAgnstVoucherBill = new DataTable();
        public DataTable dtOutStanding = new DataTable();
        private bool flagUpdate = false;
        public int iSiNo = 1;
        public JournalVoucher()
        {
            InitializeComponent();
        }

        private void JournalVoucher_Load(object sender, EventArgs e)
        {
            dtpVoucherDate.Value = DateTime.Today;
            FillCommanyData();
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

            #region CREATING TABLE FOR AGAINSTVOUCHERBILL
            dtAgnstVoucherBill.Columns.Add("isinos");
            dtAgnstVoucherBill.Columns.Add("VOUCHER_ID");
            dtAgnstVoucherBill.Columns.Add("VOUCHER_DATE");
            dtAgnstVoucherBill.Columns.Add("MASTER_SL_NO");
            dtAgnstVoucherBill.Columns.Add("SL_NO");
            dtAgnstVoucherBill.Columns.Add("ADJUSTMENT_TYPE");
            dtAgnstVoucherBill.Columns.Add("AG_COMPANY_CODE");
            dtAgnstVoucherBill.Columns.Add("AG_STATE_CODE");
            dtAgnstVoucherBill.Columns.Add("AG_BRANCH_CODE");
            dtAgnstVoucherBill.Columns.Add("AG_FIN_YEAR");
            dtAgnstVoucherBill.Columns.Add("AG_BILL_TYPE");
            dtAgnstVoucherBill.Columns.Add("AG_BILL_NUMBER");
            dtAgnstVoucherBill.Columns.Add("AG_BILL_DATE");
            dtAgnstVoucherBill.Columns.Add("AG_INVOICE_AMOUNT");
            dtAgnstVoucherBill.Columns.Add("AG_OUTSTANDING_AMOUNT");
            dtAgnstVoucherBill.Columns.Add("AG_BILL_AMOUNT");
            dtAgnstVoucherBill.Columns.Add("ChqRefNo");

            #endregion

            #region CREATING TABLE FOR OUTSTANDING AMOUNT
            dtOutStanding.Columns.Add("SL_NO");
            dtOutStanding.Columns.Add("OU_COMPANY_CODE");
            dtOutStanding.Columns.Add("OU_STATE_CODE");
            dtOutStanding.Columns.Add("OU_BRANCH_CODE");
            dtOutStanding.Columns.Add("OU_FIN_YEAR");
            dtOutStanding.Columns.Add("OU_ACCOUNT_ID");
            dtOutStanding.Columns.Add("OU_BILL_TYPE");
            dtOutStanding.Columns.Add("OU_BILL_NUMBER");
            dtOutStanding.Columns.Add("OU_BILL_DATE");
            dtOutStanding.Columns.Add("OU_DUE_DATE");
            dtOutStanding.Columns.Add("OU_BILL_AMOUNT");
            dtOutStanding.Columns.Add("OU_AMOUNT");
            dtOutStanding.Columns.Add("OU_DR_CR_ID");
            dtOutStanding.Columns.Add("OU_DOCUMENT_MONTH");
            dtOutStanding.Columns.Add("OU_AMT_PAID_RCVD");
            dtOutStanding.Columns.Add("AG_BILL_AMOUNT");

            #endregion
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

        private void btnBillDetails_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                JournalVoucherDetails  journalVoucherDetails = new JournalVoucherDetails();
                journalVoucherDetails.objJournalVoucher= this;
                journalVoucherDetails.ShowDialog();
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
            else if (txtReferenceNo.Text.Length == 0)
            {
                MessageBox.Show("Enter Reference No", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtReferenceNo.Focus();
                flag = false;
            }
            return flag;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
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
                cellPaymentMode.Value = dtBillRecievedDetails.Rows[i]["DEBIT_CREDIT"];
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


                if (dtBillRecievedDetails.Rows[i]["DEBIT_CREDIT"].ToString() == "D")
                {
                    DataGridViewCell cellDrAmtReceived = new DataGridViewTextBoxCell();
                    cellDrAmtReceived.Value = dtBillRecievedDetails.Rows[i]["AmtReceived"];
                    tempRow.Cells.Add(cellDrAmtReceived);

                    DataGridViewCell cellCrAmtReceived = new DataGridViewTextBoxCell();
                    cellCrAmtReceived.Value = "0";
                    tempRow.Cells.Add(cellCrAmtReceived);
                }
                if (dtBillRecievedDetails.Rows[i]["DEBIT_CREDIT"].ToString() == "C")
                {
                    DataGridViewCell cellDrAmtReceived = new DataGridViewTextBoxCell();
                    cellDrAmtReceived.Value = "0";
                    tempRow.Cells.Add(cellDrAmtReceived);

                    DataGridViewCell cellCrAmtReceived = new DataGridViewTextBoxCell();
                    cellCrAmtReceived.Value = dtBillRecievedDetails.Rows[i]["AmtReceived"];
                    tempRow.Cells.Add(cellCrAmtReceived);
                }

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
        public void CaluculatingTotalRcvdAmt()
        {
            double totalRcvdAmt = 0,totalCreditAmt=0;
            if (gvBillDetails.Rows.Count > 0)
            {
                for (int iRow = 0; iRow < gvBillDetails.Rows.Count; iRow++)
                {
                    if (gvBillDetails.Rows[iRow].Cells["PaymentMode"].Value.ToString() == "D")
                    {
                        totalRcvdAmt += Convert.ToDouble(gvBillDetails.Rows[iRow].Cells["DebitAmount"].Value);
                    }
                    if (gvBillDetails.Rows[iRow].Cells["PaymentMode"].Value.ToString()=="C")
                    {
                        totalCreditAmt += Convert.ToDouble(gvBillDetails.Rows[iRow].Cells["CreditAmount"].Value);
                    }
                }
                
            }
            txtTotRecvdAmt.Text = totalRcvdAmt.ToString();
            txtTotCrAmt.Text = totalCreditAmt.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckCrDrEqual() && CheckData())
            {
            //    if (SaveHeadData() > 0)
            //    {
            //        intSave = SaveDetlVoucharData();
            //    }
            //    else
            //    {
            //        try
            //        {
            //            string strSql = "DELETE from FA_VOUCHER_OTHERS" +
            //                  " WHERE VCO_COMPANY_CODE='" + CommonData.CompanyCode +
            //                  "' AND VCO_BRANCH_CODE='" + CommonData.BranchCode +
            //                  "' AND VCO_VOUCHER_ID='" + txtVoucherId.Text +
            //                  "'  AND VCO_FIN_YEAR='" + CommonData.FinancialYear + "'";
            //            objSQLdb = new SQLDB();
            //            objSQLdb.ExecuteSaveData(strSql);
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.ToString());
            //        }
            //    }
            //    if (intSave > 0)
            //    {
            //        try
            //        {
            //            //PROCEDURE TO ADJUSTING OUTSTADING AMOUNT

            //            objInvDB = new InvoiceDB();

            //            //objInvDB.UpdatingOutStandingAmt(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, "CR", Convert.ToInt32(txtVoucherId.Text));

            //            MessageBox.Show("Data Saved Successfully ", "SDMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            GenerateVoucherId();
            //            btnCancel_Click(null, null);
            //        }
            //        catch (Exception ex)
            //        {
            //            //string strSql = "DELETE from FA_VOUCHER_OTHERS" +
            //            //      " WHERE VCO_COMPANY_CODE='" + CommonData.CompanyCode +
            //            //      "' AND VCO_BRANCH_CODE='" + CommonData.BranchCode +
            //            //      "' AND VCO_VOUCHER_ID='" + txtVoucherId.Text +
            //            //      "'  AND VCO_FIN_YEAR='" + CommonData.FinancialYear + "'";
            //            //objSQLdb = new SQLDB();
            //            //objSQLdb.ExecuteSaveData(strSql);
            //            MessageBox.Show(ex.ToString());
            //            MessageBox.Show("Data Not Saved ", "SDMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        finally
            //        {
            //        }
            //        GenerateVoucherId();
            //        btnCancel_Click(null, null);

            //    }
            //    else
            //    {
            //        MessageBox.Show("Data Not Saved ", "SDMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            }
        }


        private bool CheckCrDrEqual()
        {
            bool flag = false;
            if (Convert.ToDouble(txtTotRecvdAmt.Text) == Convert.ToDouble(txtTotCrAmt.Text) && Convert.ToDouble(txtTotRecvdAmt.Text) > 0 && Convert.ToDouble(txtTotCrAmt.Text)>0)
            {
                flag = true;
            }
            return flag;
        }
        public int SaveHeadData()
        {
            int iRes = 0;
            string strSQL = "";
            try
            {
                if (flagUpdate == false)
                {
                    GenerateVoucherId();
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
                                    "','CR'" +
                                    ",'" + txtVoucherId.Text +
                                    "','" + dtpVoucherDate.Value.ToString("dd/MMM/yyyy") +
                                    "','" + txtDesc1.Text +
                                    "','" + txtDesc2.Text +
                                    //"','" + txtVoucherNo.Text +
                                    "','" +
                                    "','" + CommonData.LogUserId +
                                    "',getdate())";
                }
                else
                {


                    objInvDB = new InvoiceDB();

                    objInvDB.BeforeUpdatingOutStandingAmt(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, "CR", Convert.ToInt32(txtVoucherId.Text));




                    strSQL = " DELETE FROM FA_VOUCHER_BILLS WHERE VCB_COMPANY_CODE='" + CommonData.CompanyCode +
                          "' AND VCB_BRANCH_CODE='" + CommonData.BranchCode +
                          "' AND VCB_FIN_YEAR='" + CommonData.FinancialYear +
                          "' AND VCB_DOC_TYPE='CR'" +
                          " AND VCB_VOUCHER_ID='" + txtVoucherId.Text +
                        //"' AND VCB_VOUCHER_DATE='" + dtpVoucherDate.Value.ToString("dd/MMM/yyyy")+ 
                          "'";

                    strSQL += " DELETE FROM FA_VOUCHER WHERE VC_COMPANY_CODE='" + CommonData.CompanyCode +
                              "' AND VC_BRANCH_CODE='" + CommonData.BranchCode +
                              "' AND VC_FIN_YEAR='" + CommonData.FinancialYear +
                              "' AND VC_DOC_TYPE='CR'" +
                              " AND VC_VOUCHER_ID='" + txtVoucherId.Text + "'";

                    objSQLdb = new SQLDB();
                    iRes = objSQLdb.ExecuteSaveData(strSQL);
                    strSQL = "";

                    strSQL = " UPDATE FA_VOUCHER_OTHERS SET VCO_COMPANY_CODE='" + CommonData.CompanyCode +
                                    "',VCO_BRANCH_CODE='" + CommonData.BranchCode +
                                    "',VCO_FIN_YEAR='" + CommonData.FinancialYear +
                                    "',VCO_DOC_TYPE='CR'" +
                                    ",VCO_VOUCHER_DATE='" + dtpVoucherDate.Value.ToString("dd/MMM/yyyy") +
                                    "',VCO_NARRATION_1='" + txtDesc1.Text +
                                    "',VCO_NARRATION_2='" + txtDesc2.Text +
                                    "',VCO_EFFECT_NAME='" +
                                    //"',VCO_VOUCHER_NO='" + txtVoucherNo.Text +
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


                strSQL = " DELETE FROM FA_VOUCHER_BILLS WHERE VCB_COMPANY_CODE='" + CommonData.CompanyCode +
                          "' AND VCB_BRANCH_CODE='" + CommonData.BranchCode +
                          "' AND VCB_FIN_YEAR='" + CommonData.FinancialYear +
                          "' AND VCB_DOC_TYPE='CR'" +
                          " AND VCB_VOUCHER_ID='" + txtVoucherId.Text +
                    //"' AND VCB_VOUCHER_DATE='" + dtpVoucherDate.Value.ToString("dd/MMM/yyyy")+ 
                          "'";

                strSQL += " DELETE FROM FA_VOUCHER WHERE VC_COMPANY_CODE='" + CommonData.CompanyCode +
                          "' AND VC_BRANCH_CODE='" + CommonData.BranchCode +
                          "' AND VC_FIN_YEAR='" + CommonData.FinancialYear +
                          "' AND VC_DOC_TYPE='CR'" +
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
                        "','CR'" +
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
                         "','CR'" +
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
                        "','CR'" +
                         ",'" + dtBillRecievedDetails.Rows[0]["VOUCHER_ID"] +
                         "','" + Convert.ToDateTime(dtBillRecievedDetails.Rows[0]["VOUCHER_DATE"].ToString()).ToString("dd/MMM/yyyy") +
                         "','" + 0 +
                         "','CASH" +
                         "','D" +
                         "','" + txtTotRecvdAmt.Text +
                         "','" +
                         "','" +
                         "','" + dtBillRecievedDetails.Rows[0]["PaymentMode"] +
                         "','" +
                         "','" + Convert.ToDateTime(dtBillRecievedDetails.Rows[0]["CheqDate"].ToString()).ToString("dd/MMM/yyyy") +
                         "','" +
                         //"','" + txtVoucherNo.Text +
                         //"','" + cbAccType.SelectedValue.ToString() +
                         "','" + txtTotRecvdAmt.Text +
                         "','CR'" +
                         ",'N'" +
                         ",'N'" +
                         ",'" + CommonData.LogUserId +
                         "',GETDATE())";

                intSave = objSQLdb.ExecuteSaveData(strSQL);
                intSave = SaveDetlBillData();
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
        private int SaveDetlBillData()
        {
            int intSave = 0;
            string strSQL = "";
            try
            {
                int count = 1;
                for (int i = 0; i < dtAgnstVoucherBill.Rows.Count; i++)
                {
                    //// ASSIGNING SI.NOS TO VOUCHER BILLS
                    if (dtAgnstVoucherBill.Rows.Count > 1 && i > 0)
                    {
                        if (Convert.ToInt32(dtAgnstVoucherBill.Rows[i]["isinos"].ToString()) != Convert.ToInt32(dtAgnstVoucherBill.Rows[i - 1]["isinos"].ToString()))
                        {
                            count++;
                        }
                    }
                    if (Convert.ToDouble(dtAgnstVoucherBill.Rows[i]["AG_BILL_AMOUNT"].ToString()) > 0)
                    {
                        strSQL += " INSERT INTO FA_VOUCHER_BILLS(VCB_COMPANY_CODE" +
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
                              "','CR'" +
                              ",'" + dtAgnstVoucherBill.Rows[i]["VOUCHER_ID"] +
                              "','" + Convert.ToDateTime(dtAgnstVoucherBill.Rows[i]["VOUCHER_DATE"].ToString()).ToString("dd/MMM/yyyy") +
                              "','" + count +
                              "','" + dtAgnstVoucherBill.Rows[i]["SL_NO"] +
                              "','" + dtAgnstVoucherBill.Rows[i]["ADJUSTMENT_TYPE"] +
                              "','" + dtAgnstVoucherBill.Rows[i]["AG_COMPANY_CODE"] +
                              "','" + dtAgnstVoucherBill.Rows[i]["AG_STATE_CODE"] +
                              "','" + dtAgnstVoucherBill.Rows[i]["AG_BRANCH_CODE"] +
                              "','" + dtAgnstVoucherBill.Rows[i]["AG_FIN_YEAR"] +
                              "','" + dtAgnstVoucherBill.Rows[i]["AG_BILL_TYPE"] +
                              "','" + dtAgnstVoucherBill.Rows[i]["AG_BILL_NUMBER"];
                        if (dtAgnstVoucherBill.Rows[i]["ADJUSTMENT_TYPE"].ToString() == "A")
                        {
                            strSQL += "','" + Convert.ToDateTime(dtAgnstVoucherBill.Rows[i]["AG_BILL_DATE"].ToString()).ToString("dd/MMM/yyyy");
                        }
                        else
                        {
                            strSQL += "','";
                        }
                        strSQL += "','" + dtAgnstVoucherBill.Rows[i]["AG_BILL_AMOUNT"] +
                          "','" + CommonData.LogUserId +
                          "',GETDATE())";
                    }
                }
                objSQLdb = new SQLDB();
                intSave = objSQLdb.ExecuteSaveData(strSQL);
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString());
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
            //cbAccType.SelectedIndex = 0;
            txtDesc1.Text = "";
            txtDesc2.Text = "";
            gvBillDetails.Rows.Clear();
            dtBillRecievedDetails.Rows.Clear();
            dtAgnstVoucherBill.Rows.Clear();
            txtTotRecvdAmt.Text = "";
            txtTotCrAmt.Text = "";
            dtBillRecievedDetails.Rows.Clear();
            dtAgnstVoucherBill.Rows.Clear();
            dtOutStanding.Rows.Clear();
            iSiNo = 1;
        }
    }
}
