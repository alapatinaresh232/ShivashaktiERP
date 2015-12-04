using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSAdmin;
using SSTrans;

namespace SDMS
{
    public partial class ReceiptVoucherList : Form
    {
        SQLDB objSQLdb = null;
        DateTime defaultDate;
        InvoiceDB objInvDB = null;
        public ReceiptVoucherList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void ReceiptVoucherList_Load(object sender, EventArgs e)
        {
            FillCommanyData();
            cbCompany.SelectedIndex = 0;
            string strDate = "01-01-1900";
            defaultDate = Convert.ToDateTime(strDate);
            dtpFromDate.Value = defaultDate;
            dtpToDate.Value = defaultDate;
            cbCompany.SelectedValue = "SATL";
        }

        private void FillCommanyData()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            cbCompany.DataSource = null;
            try
            {
                string strCommand = "SELECT CM_COMPANY_NAME,CM_COMPANY_CODE FROM COMPANY_MAS WHERE ACTIVE='T' AND CM_COMPANY_CODE IN('SATL','SHS')";

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
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCmd = "SELECT TOP 100 VCO_COMPANY_CODE,VCO_BRANCH_CODE,VCO_FIN_YEAR,VCO_DOC_TYPE,VCO_VOUCHER_ID,"+
                "VCO_VOUCHER_DATE,VCO_VOUCHER_NO,VC_CASH_BANK_ID,VC_VOUCHER_AMOUNT FROM FA_VOUCHER_OTHERS INNER JOIN FA_VOUCHER ON VC_VOUCHER_ID=VCO_VOUCHER_ID WHERE " +
                "VCO_COMPANY_CODE ='"+cbCompany.SelectedValue.ToString()+"' ORDER BY VCO_VOUCHER_DATE DESC ";
                dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                //FillDataToGrid(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dt = null;
                objSQLdb = null;

            }
        }

        private void FillDataToGrid(DataTable dt)
        {
            gvVoucherList.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                gvVoucherList.Rows.Add();
                gvVoucherList.Rows[i].Cells["SlNo"].Value = i + 1;

                gvVoucherList.Rows[i].Cells["compcode"].Value = dt.Rows[i]["VCO_COMPANY_CODE"].ToString();
                gvVoucherList.Rows[i].Cells["branchcode"].Value = dt.Rows[i]["VCO_BRANCH_CODE"].ToString();
                gvVoucherList.Rows[i].Cells["finYear"].Value = dt.Rows[i]["VCO_FIN_YEAR"].ToString();
                gvVoucherList.Rows[i].Cells["doctype"].Value = dt.Rows[i]["VCO_DOC_TYPE"].ToString();
                if (gvVoucherList.Rows[i].Cells["doctype"].Value.ToString() == "CR")
                {
                    gvVoucherList.Rows[i].Cells["VoucherType"].Value = "RECEIPT";
                }
                else if (gvVoucherList.Rows[i].Cells["doctype"].Value.ToString() == "DR")
                {
                    gvVoucherList.Rows[i].Cells["VoucherType"].Value = "PAYMENT";
                }
                gvVoucherList.Rows[i].Cells["VoucherId"].Value = dt.Rows[i]["VCO_VOUCHER_ID"].ToString();
                gvVoucherList.Rows[i].Cells["DealerCode"].Value = dt.Rows[i]["VC_ACCOUNT_ID"].ToString();
                gvVoucherList.Rows[i].Cells["CashOrBankId"].Value = dt.Rows[i]["VC_CASH_BANK_ID"].ToString();
                gvVoucherList.Rows[i].Cells["Date"].Value = Convert.ToDateTime( dt.Rows[i]["VCO_VOUCHER_DATE"].ToString()).ToShortDateString();
                gvVoucherList.Rows[i].Cells["Amount"].Value = dt.Rows[i]["VC_VOUCHER_AMOUNT"].ToString();
                gvVoucherList.Rows[i].Cells["AM_ACCOUNT_TYPE_ID"].Value = dt.Rows[i]["AM_ACCOUNT_TYPE_ID"].ToString();         
            }
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            dtpToDate.Value = dtpFromDate.Value.AddDays(1);
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFromDate.Value > dtpToDate.Value)
            {
                dtpFromDate.Value = dtpToDate.Value.AddDays(-1);
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
             if (cbCompany.SelectedIndex <= 0)
            {
                MessageBox.Show("Please Select Company","SDMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dtpFromDate.Value == defaultDate || dtpToDate.Value == defaultDate)
            {
                MessageBox.Show("Please Select Date", "SDMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
            else
            {
                objSQLdb = new SQLDB();
                DataTable dt = new DataTable();
                try
                {
                    //string strCmd = "SELECT DISTINCT TOP 100 VCO_COMPANY_CODE,VCO_BRANCH_CODE,VCO_FIN_YEAR,VCO_DOC_TYPE,VCO_VOUCHER_ID,VCO_VOUCHER_DATE,VCO_VOUCHER_NO," +
                    //    " VC_CASH_BANK_ID,VC_VOUCHER_AMOUNT FROM FA_VOUCHER_OTHERS INNER JOIN FA_VOUCHER ON VC_VOUCHER_ID=VCO_VOUCHER_ID" +
                    //    " WHERE VCO_COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() + "' AND" +
                    //    " (VCO_VOUCHER_DATE BETWEEN '" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "' AND '" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "') " +
                    //    "ORDER BY VCO_VOUCHER_DATE DESC  ";
                    objInvDB = new InvoiceDB();
                    if (txtDealerCode.Text.Length == 0)
                    {
                        dt = objInvDB.GetVoucherListData(cbCompany.SelectedValue.ToString(), dtpFromDate.Value.ToString("dd/MMM/yyyy"), dtpToDate.Value.ToString("dd/MMM/yyyy"), "0").Tables[0];
                    }
                    else
                    {
                        dt = objInvDB.GetVoucherListData(cbCompany.SelectedValue.ToString(), dtpFromDate.Value.ToString("dd/MMM/yyyy"), dtpToDate.Value.ToString("dd/MMM/yyyy"), txtDealerCode.Text).Tables[0];
                    }
                    FillDataToGrid(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    dt = null;
                    objSQLdb = null;

                }
            }
        }

        private void gvVoucherList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string strCompCode = gvVoucherList.Rows[e.RowIndex].Cells["compcode"].Value.ToString();
                string strBranchCode = gvVoucherList.Rows[e.RowIndex].Cells["branchcode"].Value.ToString();
                string strFinYear = gvVoucherList.Rows[e.RowIndex].Cells["finYear"].Value.ToString();
                string strDocType = gvVoucherList.Rows[e.RowIndex].Cells["doctype"].Value.ToString();
                string strVoucherId = gvVoucherList.Rows[e.RowIndex].Cells["VoucherId"].Value.ToString();


                if (gvVoucherList.Rows[e.RowIndex].Cells["Edit"].Value.ToString().Trim() != "")
                {
                    if (e.ColumnIndex == gvVoucherList.Columns["Edit"].Index)
                    {
                        if (gvVoucherList.Rows[e.RowIndex].Cells["AM_ACCOUNT_TYPE_ID"].Value.ToString() == "CASH" && gvVoucherList.Rows[e.RowIndex].Cells["doctype"].Value.ToString() == "CR")
                        {
                            ReceiptVoucher objReceiptVoucher = new ReceiptVoucher(strCompCode, strBranchCode, strFinYear, strDocType, strVoucherId);
                            objReceiptVoucher.objVoucher = this;
                            objReceiptVoucher.ShowDialog();
                        }
                        if (gvVoucherList.Rows[e.RowIndex].Cells["AM_ACCOUNT_TYPE_ID"].Value.ToString() == "CASH" && gvVoucherList.Rows[e.RowIndex].Cells["doctype"].Value.ToString() == "DR")
                        {
                            CashPayment objReceiptVoucher = new CashPayment(strCompCode, strBranchCode, strFinYear, strDocType, strVoucherId);
                            objReceiptVoucher.objVoucher = this;
                            objReceiptVoucher.ShowDialog();
                        }
                        if (gvVoucherList.Rows[e.RowIndex].Cells["AM_ACCOUNT_TYPE_ID"].Value.ToString() == "BANK" && gvVoucherList.Rows[e.RowIndex].Cells["doctype"].Value.ToString() == "CR")
                        {
                            BankRecieptVoucher objReceiptVoucher = new BankRecieptVoucher(strCompCode, strBranchCode, strFinYear, strDocType, strVoucherId);
                            objReceiptVoucher.objVoucher = this;
                            objReceiptVoucher.ShowDialog();
                        }
                        if (gvVoucherList.Rows[e.RowIndex].Cells["AM_ACCOUNT_TYPE_ID"].Value.ToString() == "BANK" && gvVoucherList.Rows[e.RowIndex].Cells["doctype"].Value.ToString() == "DR")
                        {
                            BankPayment objReceiptVoucher = new BankPayment(strCompCode, strBranchCode, strFinYear, strDocType, strVoucherId);
                            objReceiptVoucher.objVoucher = this;
                            objReceiptVoucher.ShowDialog();
                        }
                    }
                }
                if (e.ColumnIndex == gvVoucherList.Columns["Delete"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {

                        try
                        {
                            objInvDB = new InvoiceDB();
                            objSQLdb = new SQLDB();
                            string strSQL = null;
                            if (gvVoucherList.Rows[e.RowIndex].Cells["doctype"].Value.ToString() == "CR")
                            {
                                
                                objInvDB.BeforeUpdatingOutStandingAmt(strCompCode, strBranchCode, strFinYear, "CR", Convert.ToInt32(strVoucherId));


                                strSQL = " DELETE FROM FA_VOUCHER_BILLS WHERE VCB_COMPANY_CODE='" + strCompCode +
                                      "' AND VCB_BRANCH_CODE='" + strBranchCode +
                                      "' AND VCB_FIN_YEAR='" + strFinYear +
                                      "' AND VCB_DOC_TYPE='CR'" +
                                      " AND VCB_VOUCHER_ID='" + strVoucherId +
                                    //"' AND VCB_VOUCHER_DATE='" + dtpVoucherDate.Value.ToString("dd/MMM/yyyy")+ 
                                      "'";

                                strSQL += " DELETE FROM FA_VOUCHER WHERE VC_COMPANY_CODE='" + strCompCode +
                                          "' AND VC_BRANCH_CODE='" + strBranchCode +
                                          "' AND VC_FIN_YEAR='" + strFinYear +
                                          "' AND VC_DOC_TYPE='CR'" +
                                          " AND VC_VOUCHER_ID='" + strVoucherId + "'";

                                strSQL += " DELETE FROM FA_VOUCHER_OTHERS WHERE VCO_COMPANY_CODE='" + strCompCode +
                                          "' AND VCO_BRANCH_CODE='" + strBranchCode +
                                          "' AND VCO_FIN_YEAR='" + strFinYear +
                                          "' AND VCO_DOC_TYPE='CR'" +
                                          " AND VCO_VOUCHER_ID='" + strVoucherId + "'";
                            }
                            else if (gvVoucherList.Rows[e.RowIndex].Cells["doctype"].Value.ToString() == "DR")
                            {
                                objSQLdb = new SQLDB();
                                strSQL += " DELETE FROM FA_VOUCHER WHERE VC_COMPANY_CODE='" + strCompCode +
                                         "' AND VC_BRANCH_CODE='" + strBranchCode +
                                         "' AND VC_FIN_YEAR='" + strFinYear +
                                         "' AND VC_DOC_TYPE='DR'" +
                                         " AND VC_VOUCHER_ID='" + strVoucherId + "'";

                                strSQL += " DELETE FROM FA_VOUCHER_OTHERS WHERE VCO_COMPANY_CODE='" + strCompCode +
                                          "' AND VCO_BRANCH_CODE='" + strBranchCode +
                                          "' AND VCO_FIN_YEAR='" + strFinYear +
                                          "' AND VCO_DOC_TYPE='DR'" +
                                          " AND VCO_VOUCHER_ID='" + strVoucherId + "'";

                            }
                            int iRes = objSQLdb.ExecuteSaveData(strSQL);
                            if(iRes>0)
                            {
                                MessageBox.Show("Selected  Record is Deleted");
                                //btnDisplay_Click(null, null);

                                objSQLdb = new SQLDB();
                                DataTable dt = new DataTable();
                                try
                                {
                                    //string strCmd = "SELECT DISTINCT TOP 100 VCO_COMPANY_CODE,VCO_BRANCH_CODE,VCO_FIN_YEAR,VCO_DOC_TYPE,VCO_VOUCHER_ID,VCO_VOUCHER_DATE,VCO_VOUCHER_NO," +
                                    //    " VC_CASH_BANK_ID,VC_VOUCHER_AMOUNT FROM FA_VOUCHER_OTHERS INNER JOIN FA_VOUCHER ON VC_VOUCHER_ID=VCO_VOUCHER_ID" +
                                    //    " WHERE VCO_COMPANY_CODE ='" + cbCompany.SelectedValue.ToString() + "' AND" +
                                    //    " (VCO_VOUCHER_DATE BETWEEN '" + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "' AND '" + dtpToDate.Value.ToString("dd/MMM/yyyy") + "') " +
                                    //    "ORDER BY VCO_VOUCHER_DATE DESC  ";
                                    objInvDB = new InvoiceDB();
                                    
                                        dt = objInvDB.GetVoucherListData(cbCompany.SelectedValue.ToString(), dtpFromDate.Value.ToString("dd/MMM/yyyy"), dtpToDate.Value.ToString("dd/MMM/yyyy"), "0").Tables[0];
                                    
                                    FillDataToGrid(dt);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString());
                                }
                                finally
                                {
                                    dt = null;
                                    objSQLdb = null;

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        finally
                        {
                        }


                    }
                }
            }
        }

        private void txtDealerCode_KeyUp(object sender, KeyEventArgs e)
        {
            gvVoucherList.ClearSelection();
            int rowIndex = 0;
            foreach (DataGridViewRow row in gvVoucherList.Rows)
            {
                if (row.Cells[5].Value.ToString().Contains(txtDealerCode.Text) == true)
                {
                    rowIndex = row.Index;
                    gvVoucherList.CurrentCell = gvVoucherList.Rows[rowIndex].Cells[5];
                    break;
                }
            }
        }

        
    }
}
