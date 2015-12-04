using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSTrans;
using SSCRMDB;
using SSCRM.App_Code;
namespace SSCRM
{
    public partial class StockTransfer : Form
    {
        private SQLDB objSQLDB = null;
        Security objSecurity = new Security();
        HRInfo objHrInfo = new HRInfo();
        InvoiceDB objInvoiceDB = new InvoiceDB();
        StockTransferTrn oStockTransfer = new StockTransferTrn();
        private bool isUpdate = false;
        public int IsExist = 0;

        public StockTransfer()
        {
            InitializeComponent();
        }

        private void StockTransfer_Load(object sender, EventArgs e)
        {
            GetPopupdropDown();
            lblDocMonth.Text = CommonData.DocMonth;
            txtTrnNo.Text = oStockTransfer.GetMaxTransNo(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, "GC2GC");
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            cmbCompany.SelectedValue = CommonData.CompanyCode;
            cmbBranch.SelectedValue = CommonData.BranchCode;
            meStockDate.Value = Convert.ToDateTime(CommonData.DocMonth);
            txtTransQty.Text = "0.00";
        }
        public void GetPopupdropDown()
        {
            objSecurity = new Security();
            DataTable dtCpy = objSecurity.GetCompanyDataSet().Tables[0];
            UtilityLibrary.PopulateControl(cmbCompany, dtCpy.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            objSecurity = null;
        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCompany.SelectedIndex > 0)
            {
                objHrInfo = new HRInfo();
                DataTable dtBranch = objHrInfo.GetAllBranchList(cmbCompany.SelectedValue.ToString(), "BR", "").Tables[0];
                UtilityLibrary.PopulateControl(cmbBranch, dtBranch.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
                objHrInfo = null;
            }
        }

        private void txtEcodeFrm_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeFrm.Text.ToString().Trim().Length > 4)
            {
                if (txtEcodeFrm.Text.ToString().Trim().Length > 0)
                    EcodeSearch(CommonData.CompanyCode, CommonData.BranchCode, txtEcodeFrm.Text, cbEcodeFrm);
                else
                    FillEmployeeData(CommonData.CompanyCode, CommonData.BranchCode, cbEcodeFrm);
            }
        }

        private void txtECodeTo_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtECodeTo.Text.ToString().Trim().Length > 4)
            {
                if ((cmbCompany.SelectedIndex > 0) && (cmbBranch.SelectedIndex > 0))
                {
                    if (txtECodeTo.Text.ToString().Trim().Length > 0)
                        EcodeSearch(cmbCompany.SelectedValue.ToString(), cmbBranch.SelectedValue.ToString(), txtECodeTo.Text, cbEcodeTo);
                    else
                        FillEmployeeData(cmbCompany.SelectedValue.ToString(), cmbBranch.SelectedValue.ToString(), cbEcodeTo);
                }
            }
        }

        public void FillEmployeeData(string CompanyCode, string BranchCode, ComboBox cbEcode)
        {
            objInvoiceDB = new InvoiceDB();
            DataSet dsEmp = null;
            try
            {
                dsEmp = objInvoiceDB.GetAllEcodeList(CompanyCode, BranchCode, CommonData.DocMonth);
                DataTable dtEmp = dsEmp.Tables[0];
                DataRow dr = dtEmp.NewRow();
                dr[0] = "0";
                dr[1] = "Select";
                dtEmp.Rows.InsertAt(dr, 0);
                dr = null;
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
                    //string strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                }
                objInvoiceDB = null;
                Cursor.Current = Cursors.Default;
            }
        }

        private void EcodeSearch(string CompanyCode, string BranchCode, string eCode, ComboBox cbEcode)
        {
            objInvoiceDB = new InvoiceDB();
            DataSet dsEmp = null;
            SQLDB objDB = new SQLDB();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cbEcode.DataSource = null;
                cbEcode.Items.Clear();
                //dsEmp = objInvoiceDB.InvLevelAllEcodeSearch_Get(CompanyCode, BranchCode, CommonData.DocMonth, eCode);
                dsEmp = objDB.ExecuteDataSet("SELECT ECODE, CAST(ECODE AS VARCHAR)+'-'+MEMBER_NAME ENAME " +
                                            "FROM EORA_MASTER WHERE CAST(ECODE AS VARCHAR)+'-'+MEMBER_NAME LIKE '%" + eCode + "%'");
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
                    //strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                }
                objInvoiceDB = null;
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbEcodeFrm.SelectedValue) > 0)
            {
                ProductSearchAll PSearch = new ProductSearchAll("StockTransfer", Convert.ToInt32(cbEcodeFrm.SelectedValue));
                PSearch.objStockTransfer = this;
                PSearch.ShowDialog();
            }
        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvProductDetails.Rows.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            int iRetValDetl = 0;
            string sqlStr = "";
            if (isUpdate == false)
            {
                txtTrnNo.Text = oStockTransfer.GetMaxTransNo(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, "GC2GC");
                sqlStr = " INSERT INTO STK_INTR_HEAD (SIH_COMPANY_CODE, SIH_STATE_CODE, SIH_BRANCH_CODE, " +
                            "SIH_FIN_YEAR, SIH_TRN_TYPE, SIH_TRN_NUMBER,SIH_TRN_DATE," +
                            "SIH_DOC_MONTH, SIH_FROM_GROUP_ECODE, SIH_TO_COMPANY_CODE, SIH_TO_BRANCH_CODE, " +
                            "SIH_TO_GROUP_ECODE,SIH_CREATED_BY,SIH_AUTHORIZED_BY,SIH_CREATED_DATE) VALUES " +
                            "('" + CommonData.CompanyCode + "','" + CommonData.StateCode + "','" + CommonData.BranchCode +
                            "','" + CommonData.FinancialYear + "','GC2GC'," + txtTrnNo.Text +
                            ",'" + Convert.ToDateTime(meStockDate.Value).ToString("dd/MMM/yyyy") + "','" + lblDocMonth.Text.ToUpper() +
                            "','" + cbEcodeFrm.SelectedValue.ToString() + "','" + cmbCompany.SelectedValue.ToString() +
                            "','" + cmbBranch.SelectedValue.ToString() + "','" + cbEcodeTo.SelectedValue.ToString() +
                            "','" + CommonData.LogUserId + "','" + CommonData.LogUserId + "',getdate())";
            }
            else
            {
                sqlStr = " UPDATE STK_INTR_HEAD SET SIH_TRN_DATE='" + Convert.ToDateTime(meStockDate.Value).ToString("dd/MMM/yyyy") +
                            "', SIH_DOC_MONTH='" + lblDocMonth.Text.ToUpper() +
                            "', SIH_FROM_GROUP_ECODE=" + cbEcodeFrm.SelectedValue.ToString() +
                            ", SIH_TO_COMPANY_CODE='" + cmbCompany.SelectedValue.ToString() +
                            "', SIH_TO_BRANCH_CODE='" + cmbBranch.SelectedValue.ToString() +
                            "', SIH_TO_GROUP_ECODE=" + cbEcodeTo.SelectedValue.ToString() +
                            ", SIH_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                            "', SIH_AUTHORIZED_BY='" + CommonData.LogUserId +
                            "', SIH_LAST_MODIFIED_DATE=getdate() WHERE SIH_BRANCH_CODE='" + CommonData.BranchCode +
                            "' AND SIH_FIN_YEAR='" + CommonData.FinancialYear + "' AND SIH_TRN_NUMBER=" + txtTrnNo.Text;
            }
            int iRetVal=0;

            try { iRetVal = objSQLDB.ExecuteSaveData(sqlStr); }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
            finally { objSQLDB = null; }

            string strDetl = "";
            if (iRetVal > 0)
            {
                strDetl = " DELETE FROM STK_INTR_DETL WHERE SID_BRANCH_CODE='" + CommonData.BranchCode +
                            "' AND SID_FIN_YEAR='" + CommonData.FinancialYear +
                            "' AND SID_TRN_NUMBER=" + txtTrnNo.Text;
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    try { Convert.ToDouble(gvProductDetails.Rows[i].Cells["ToQty"].Value.ToString()); }
                    catch { gvProductDetails.Rows[i].Cells["ToQty"].Value = "0"; }

                    strDetl += " INSERT INTO STK_INTR_DETL(SID_COMPANY_CODE, SID_STATE_CODE, SID_BRANCH_CODE, SID_FIN_YEAR, SID_TRN_TYPE, "+
                                    "SID_TRN_NUMBER, SID_SL_NO, SID_PRODUCT_ID, SID_ISS_QTY) VALUES " +
                                    "('" + CommonData.CompanyCode + "','" + CommonData.StateCode + "','" + CommonData.BranchCode + 
                                    "','" + CommonData.FinancialYear + "','GC2GC'," + txtTrnNo.Text + "," + Convert.ToInt32(i + 1) +
                                    ",'" + gvProductDetails.Rows[i].Cells["ProductID"].Value.ToString() + "'," + gvProductDetails.Rows[i].Cells["ToQty"].Value.ToString() + ")";
                }
            }
            objSQLDB = new SQLDB();

            if (strDetl.Length > 10)
            {
                try
                {
                    iRetValDetl = objSQLDB.ExecuteSaveData(strDetl);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            objSQLDB = null;

            if (iRetValDetl > 0)
            {                
                MessageBox.Show("The Transaction No is :" + txtTrnNo.Text + "\n\nData saved successfully", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCancel_Click(null, null);
            }
            else
                MessageBox.Show("Data not saved", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            isUpdate = false;
            lblDocMonth.Text = CommonData.DocMonth;
            meStockDate.Value = Convert.ToDateTime(CommonData.DocMonth);
            txtEcodeFrm.Text = "";
            txtECodeTo.Text = "";
            //cmbCompany.SelectedIndex = 0;
            //cmbBranch.SelectedIndex = 0;
            cmbCompany.SelectedValue = CommonData.CompanyCode;
            cmbBranch.SelectedValue = CommonData.BranchCode;
            txtTrnNo.Text = oStockTransfer.GetMaxTransNo(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, "GC2GC");
            //gvProductDetails.Rows.Clear();
            for (int i = 0; i < gvProductDetails.Rows.Count; i++)
            {
                gvProductDetails.Rows[i].Cells["ToQty"].Value = "0";
            }
            IsExist = 0;
            txtTransQty.Text = "0.00";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sqlStr = " DELETE FROM STK_INTR_DETL WHERE SID_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SID_BRANCH_CODE='" + CommonData.BranchCode + "' AND SID_FIN_YEAR='" + CommonData.FinancialYear + "' AND SID_TRN_TYPE='GC2GC' AND SID_TRN_NUMBER=" + txtTrnNo.Text;
            sqlStr += "DELETE FROM STK_INTR_HEAD WHERE SIH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SIH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SIH_FIN_YEAR='" + CommonData.FinancialYear + "' AND SIH_TRN_TYPE='GC2GC' AND SIH_TRN_NUMBER=" + txtTrnNo.Text;            
            objSQLDB = new SQLDB();
            int iRetValDetl = objSQLDB.ExecuteSaveData(sqlStr);
            objSQLDB = null;
            btnCancel_Click(null, null);
            MessageBox.Show("Data deleted successfully", "SSCRM APPLICATION", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 4)
            //{
            //    try
            //    {
            //        if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["ToQty"].Value) != "")
            //        {
            //            if (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["ToQty"].Value) > Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["Qty"].Value))
            //            {
            //                gvProductDetails.Rows[e.RowIndex].Cells["ToQty"].Value = string.Empty;
            //            }
            //        }
            //    }
            //    catch(Exception ex)
            //    {
            //        gvProductDetails.Rows[e.RowIndex].Cells["ToQty"].Value = string.Empty;
            //    }
            //}
            txtTransQty.Text = GetTransferQty().ToString("f");
        }

        private void txtTrnNo_Validated(object sender, EventArgs e)
        {
            if (txtTrnNo.Text != "")
            {
                DataSet ds = oStockTransfer.GetStockTransferDetails(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, Convert.ToInt32(txtTrnNo.Text));
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        isUpdate = true;
                        IsExist = 1;
                        txtEcodeFrm.Text = ds.Tables[0].Rows[0]["SIH_FROM_GROUP_ECODE"].ToString();
                        txtEcodeFrm_KeyUp(null, null);
                        txtECodeTo.Text = ds.Tables[0].Rows[0]["SIH_TO_GROUP_ECODE"].ToString();
                        txtECodeTo_KeyUp(null, null);
                        cmbCompany.SelectedValue = ds.Tables[0].Rows[0]["SIH_TO_COMPANY_CODE"].ToString();
                        cmbCompany_SelectedIndexChanged(null, null);
                        cmbBranch.SelectedValue = ds.Tables[0].Rows[0]["SIH_TO_BRANCH_CODE"].ToString();
                        lblDocMonth.Text = ds.Tables[0].Rows[0]["SIH_DOC_MONTH"].ToString();
                        FillStockTransferDetail(ds.Tables[1]);
                    }
                    else
                    {
                        isUpdate = false;
                        txtTrnNo.Text = oStockTransfer.GetMaxTransNo(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, "GC2GC");
                        lblDocMonth.Text = CommonData.DocMonth;
                        IsExist = 0;
                        gvProductDetails.Rows.Clear();
                        //gvProductDetails.Columns[3].Visible = true;
                    }
                }
                else
                {
                    isUpdate = false;
                    txtTrnNo.Text = oStockTransfer.GetMaxTransNo(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, "GC2GC");
                    lblDocMonth.Text = CommonData.DocMonth;
                    IsExist = 0;
                    gvProductDetails.Rows.Clear();
                    //gvProductDetails.Columns[3].Visible = true;
                    //txtTrnNo.Text = oStockTransfer.GetMaxTransNo(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, "GC2GC");
                }
            }
        }
        private void FillStockTransferDetail(DataTable dt)
        {
            try
            {
                int intRow = 1;
                gvProductDetails.Rows.Clear();
                //gvProductDetails.Columns[3].Visible = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["SID_PRODUCT_ID"].ToString().Length > 0)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                        cellMainProductID.Value = dt.Rows[i]["SID_PRODUCT_ID"].ToString();
                        tempRow.Cells.Add(cellMainProductID);

                        DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                        cellMainProduct.Value = dt.Rows[i]["PM_PRODUCT_NAME"].ToString();
                        tempRow.Cells.Add(cellMainProduct);

                        DataGridViewCell cellQty = new DataGridViewTextBoxCell();
                        cellQty.Value = 0;
                        tempRow.Cells.Add(cellQty);

                        DataGridViewCell cellToQty = new DataGridViewTextBoxCell();
                        cellToQty.Value = Convert.ToDouble(dt.Rows[i]["SID_ISS_QTY"]).ToString("f");
                        tempRow.Cells.Add(cellToQty);                      

                        intRow = intRow + 1;
                        gvProductDetails.Rows.Add(tempRow);
                    }
                    
                }
                txtTransQty.Text = GetTransferQty().ToString("f");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }
        private double GetTransferQty()
        {
            double dbTrnsQty = 0.00;
            for (int i = 0; i < gvProductDetails.Rows.Count; i++)
            {
                if (Convert.ToString(gvProductDetails.Rows[i].Cells["ToQty"].Value) != "")
                {
                    if (Convert.ToDouble(gvProductDetails.Rows[i].Cells["ToQty"].Value.ToString()) >= 1)
                        dbTrnsQty += Convert.ToDouble(gvProductDetails.Rows[i].Cells["ToQty"].Value);
                }

            }

            return dbTrnsQty;
        }
        private void gvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == gvProductDetails.Columns["Delete"].Index)
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        DataGridViewRow dgvr = gvProductDetails.Rows[e.RowIndex];
                        gvProductDetails.Rows.Remove(dgvr);
                        if (gvProductDetails.Rows.Count > 0)
                        {
                            for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                            {
                                gvProductDetails.Rows[i].Cells["SlNo"].Value = i + 1;
                            }
                        }
                    }
                }
                txtTransQty.Text = GetTransferQty().ToString("f");
            }
        }

       
    }
}
