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
using SSAdmin;
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class SalesContraForReconsialtion : Form
    {
        InvoiceDB objData = null;
        Security objSecurity = null;
        SQLDB objSqlDB = null;
        private string strECode = string.Empty;
        private bool IsUpdate = false;
        public SalesContraForReconsialtion()
        {
            InitializeComponent();
        }

        private void SalesContraForReconsialtion_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            txtDocMonth.Text = CommonData.DocMonth.ToUpper();
            FillEmployeeData();
            txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
            objSecurity = new Security();
            DataTable dtCpy = new DataTable();
            DataTable dtAct = new DataTable();
            try
            {
                dtCpy = objSecurity.GetSingleProductsDataSet().Tables[0];
                dtAct = objSecurity.GetSingleProductsDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            UtilityLibrary.PopulateControl(cmbActProduct, dtCpy.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            UtilityLibrary.PopulateControl(cmbSaleProduct, dtAct.DefaultView, 1, 0, "--PLEASE SELECT--", 0);
            objSecurity = null;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private Int64 GenerateNewInvoiceNo()
        {

            Int64 intNewInvNo = 0;
            try
            {

                objData = new InvoiceDB();
                intNewInvNo = objData.GenerateTrnNoForStkContra(CommonData.CompanyCode, CommonData.BranchCode);
                objData = null;
            }
            catch (Exception ex)
            {
                intNewInvNo = 0;
                MessageBox.Show(ex.Message, "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return intNewInvNo;
        }

        private void FillEmployeeData()
        {
            objData = new InvoiceDB();

            DataSet dsEmp = null;
            try
            {
                dsEmp = objData.GetGCListForStkContra(CommonData.CompanyCode, CommonData.BranchCode, txtDocMonth.Text);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            objSqlDB = new SQLDB();
            string sqlText = "";
            int iRes=0;
            try { Convert.ToDouble(txtSaleQty.Text); }
            catch { txtSaleQty.Text = "0"; }
            try { Convert.ToDouble(txtActQty.Text); }
            catch { txtActQty.Text = "0"; }

            if (CheckData())
            {
                try{
                if(IsUpdate == false)
                {
                    txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
                    sqlText = "INSERT INTO GC_STK_SALES_CONTRA(" +
                                "GSSC_COMP_CODE" +
                                ", GSSC_BRANCH_CODE" +
                                ", GSSC_STATE_CODE" +
                                ", GSSC_FIN_YEAR" +
                                ", GSSC_DOC_MONTH" +
                                ", GSSC_TRN_NO" +
                                ", GSSC_SALE_PRODUCT_ID" +
                                ", GSSC_SALE_BRAND_ID" +
                                ", GSSC_SALE_QTY" +
                                ", GSSC_ACT_PRODUCT_ID" +
                                ", GSSC_ACT_BRAND_ID" +
                                ", GSSC_ACT_QTY" +
                                ", GSSC_REMARKS" +
                                ", GSSC_CREATED_BY" +
                                ", GSSC_CREATED_DATE" +
                                ", GSSC_EORA_CODE" +
                                ", GSSC_ECODE)" +
                                " VALUES('" + CommonData.CompanyCode +
                                "','" + CommonData.BranchCode +
                                "','" + CommonData.StateCode +
                                "','" + CommonData.FinancialYear +
                                "','" + txtDocMonth.Text.ToUpper() +
                                "'," + txtInvoiceNo.Text +
                                ",'" + cmbSaleProduct.SelectedValue.ToString() +
                                "',''," + txtSaleQty.Text +
                                ",'" + cmbActProduct.SelectedValue.ToString() +
                                "',''," + txtActQty.Text +
                                ",'" + txtRemarks.Text.Replace("'", "") +
                                "','" + CommonData.LogUserId +
                                "',getdate()," + strECode +
                                "," + strECode + ")";
                }
                else
                {
                    sqlText = "UPDATE GC_STK_SALES_CONTRA " +
                                "SET GSSC_SALE_PRODUCT_ID='" + cmbSaleProduct.SelectedValue.ToString() + "', " +
                                "GSSC_SALE_BRAND_ID=''," +
                                "GSSC_SALE_QTY=" + txtSaleQty.Text + "," +
                                "GSSC_ACT_PRODUCT_ID='" + cmbActProduct.SelectedValue.ToString() + "'," +
                                "GSSC_ACT_BRAND_ID=''," +
                                "GSSC_ACT_QTY=" + txtActQty.Text + "," +
                                "GSSC_REMARKS='" + txtRemarks.Text.Replace("'", "") + "'," +
                                "GSSC_MODIFIED_BY='" + CommonData.LogUserId + "'," +
                                "GSSC_MODIFIED_DATE=GETDATE(), GSSC_EORA_CODE=" + strECode +
                                ", GSSC_ECODE=" + strECode + "" +
                                "WHERE GSSC_BRANCH_CODE='" + CommonData.BranchCode + "' " +
                                "AND GSSC_FIN_YEAR = '" + CommonData.FinancialYear + "'" +
                                "AND GSSC_TRN_NO=" + txtInvoiceNo.Text;

                }

                if(sqlText.Length>10)
                    iRes = objSqlDB.ExecuteSaveData(sqlText);
                }
                catch(Exception ex){
                    iRes=0;
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSqlDB=null;
                }
                if (iRes > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "Stock Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
                    IsUpdate = false;
                    FillInvoiceList();
                    txtSaleQty.Text = "";
                    txtActQty.Text = "";
                    cmbSaleProduct.SelectedIndex = 0;
                    cmbActProduct.SelectedIndex = 0;
                    txtRemarks.Text = "";
                    txtDocMonth.Text = CommonData.DocMonth.ToUpper();
                }
                else
                    MessageBox.Show("Data not Saved", "Stock Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckData()
        {
            bool bFlag = true;
            if (strECode == "" || cbEcode.SelectedIndex < 0)
            {
                MessageBox.Show("Select GC/Gl Ecode", "Stock Adjust", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if(cmbSaleProduct.SelectedIndex<=0)
            {
                MessageBox.Show("Select Sale Product Details", "Stock Adjust", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if(cmbActProduct.SelectedIndex<=0)
            {
                MessageBox.Show("Select Actual Product Details", "Stock Adjust", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if(Convert.ToDouble(txtSaleQty.Text)==0 || Convert.ToDouble(txtActQty.Text)==0)
            {
                MessageBox.Show("Enter Product Qty", "Stock Adjust", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return bFlag;
        }

        private void cbEcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEcode.SelectedIndex > -1)
            {
                strECode = ((System.Data.DataRowView)(cbEcode.SelectedItem)).Row.ItemArray[0].ToString();
                if (strECode != "")
                {
                    FillInvoiceList();
                }
            }
        }

        private void FillInvoiceList()
        {
            objData = new InvoiceDB();

            try
            {
                gvInvoice.Rows.Clear();
                txtInvTotAmt.Text = "0.00";
                txtRecTotAmt.Text = "0.00";
                //txtAdvTotAmt.Text = "0.00";
                //txtBalTotAmt.Text = "0.00";
                //txtInvTotPoints.Text = "0.00";
                if (cbEcode.SelectedIndex > -1)
                {
                    DataTable dt = objData.StkAdjustTrnList_Get(Convert.ToInt32(strECode), txtDocMonth.Text).Tables[0];
                    //txtNumberOFInvoice.Text = dt.Rows.Count.ToString();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataGridViewRow tempRow = new DataGridViewRow();
                            DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                            cellSLNO.Value = gvInvoice.Rows.Count + 1;
                            tempRow.Cells.Add(cellSLNO);

                            DataGridViewCell cellOrdNo = new DataGridViewTextBoxCell();
                            cellOrdNo.Value = dt.Rows[i]["GSSC_TRN_NO"];
                            tempRow.Cells.Add(cellOrdNo);

                            DataGridViewCell cellInvNo = new DataGridViewTextBoxCell();
                            cellInvNo.Value = dt.Rows[i]["GSSC_EORA_CODE"];
                            tempRow.Cells.Add(cellInvNo);

                            DataGridViewCell cellInvDate = new DataGridViewTextBoxCell();
                            cellInvDate.Value = dt.Rows[i]["MEMBER_NAME"]; ;
                            tempRow.Cells.Add(cellInvDate);


                            DataGridViewCell cellInvAdvAmt = new DataGridViewTextBoxCell();
                            cellInvAdvAmt.Value = dt.Rows[i]["GSSC_SALE_PRODUCT_ID"];                            
                            tempRow.Cells.Add(cellInvAdvAmt);

                            DataGridViewCell cellInvRecAmt = new DataGridViewTextBoxCell();
                            cellInvRecAmt.Value = dt.Rows[i]["SALE_PRODUCT_NAME"];                            
                            tempRow.Cells.Add(cellInvRecAmt);

                            DataGridViewCell cellInvBalAmt = new DataGridViewTextBoxCell();
                            cellInvBalAmt.Value = dt.Rows[i]["GSSC_SALE_BRAND_ID"];
                            tempRow.Cells.Add(cellInvBalAmt);

                            DataGridViewCell cellInvTotPoints = new DataGridViewTextBoxCell();
                            cellInvTotPoints.Value = Convert.ToDouble(dt.Rows[i]["GSSC_SALE_QTY"]).ToString("f");
                            txtInvTotAmt.Text = (Convert.ToDouble(txtInvTotAmt.Text) + Convert.ToDouble(dt.Rows[i]["GSSC_SALE_QTY"])).ToString("f");
                            tempRow.Cells.Add(cellInvTotPoints);

                            DataGridViewCell cellActProdId = new DataGridViewTextBoxCell();
                            cellActProdId.Value = dt.Rows[i]["GSSC_ACT_PRODUCT_ID"];
                            tempRow.Cells.Add(cellActProdId);

                            DataGridViewCell cellActProdName = new DataGridViewTextBoxCell();
                            cellActProdName.Value = dt.Rows[i]["ACT_PRODUCT_NAME"];
                            tempRow.Cells.Add(cellActProdName);

                            DataGridViewCell cellActBrandName = new DataGridViewTextBoxCell();
                            cellActBrandName.Value = dt.Rows[i]["GSSC_ACT_BRAND_ID"];
                            tempRow.Cells.Add(cellActBrandName);

                            DataGridViewCell cellActProdQty = new DataGridViewTextBoxCell();
                            cellActProdQty.Value = Convert.ToDouble(dt.Rows[i]["GSSC_ACT_QTY"]).ToString("f");
                            txtRecTotAmt.Text = (Convert.ToDouble(txtRecTotAmt.Text) + Convert.ToDouble(dt.Rows[i]["GSSC_ACT_QTY"])).ToString("f");
                            tempRow.Cells.Add(cellActProdQty);

                            DataGridViewCell cellRemarks = new DataGridViewTextBoxCell();
                            cellRemarks.Value = dt.Rows[i]["GSSC_REMARKS"];
                            tempRow.Cells.Add(cellRemarks);

                            gvInvoice.Rows.Add(tempRow);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Stock Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
            }
        }

        private void txtEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtEcodeSearch.Text.ToString().Trim().Length > 4)
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
                dsEmp = objData.StkAdjustEcodeSearch_Get(CommonData.CompanyCode, CommonData.BranchCode, txtDocMonth.Text, txtEcodeSearch.Text.ToString());
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
            IsUpdate = false;
            txtEcodeSearch.Text = "";
            FillEmployeeData();
            txtSaleQty.Text = "";
            txtActQty.Text = "";
            txtRemarks.Text = "";
        }

        private void gvInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == gvInvoice.Columns["Edit"].Index)
                {
                    txtInvoiceNo.Text = gvInvoice.Rows[e.RowIndex].Cells["TrnNo"].Value.ToString();
                    IsUpdate = true;
                    txtEcodeSearch.Text = gvInvoice.Rows[e.RowIndex].Cells["Ecode"].Value.ToString();
                    txtEcodeSearch_KeyUp(null, null);
                    cbEcode.SelectedValue = gvInvoice.Rows[e.RowIndex].Cells["Ecode"].Value.ToString();
                    strECode = gvInvoice.Rows[e.RowIndex].Cells["Ecode"].Value.ToString();
                    cmbSaleProduct.SelectedValue = gvInvoice.Rows[e.RowIndex].Cells["ProductID"].Value.ToString();
                    cmbActProduct.SelectedValue = gvInvoice.Rows[e.RowIndex].Cells["ActProductID"].Value.ToString();
                    txtSaleQty.Text = gvInvoice.Rows[e.RowIndex].Cells["Qty"].Value.ToString();
                    txtActQty.Text = gvInvoice.Rows[e.RowIndex].Cells["ActQty"].Value.ToString();
                    txtRemarks.Text = gvInvoice.Rows[e.RowIndex].Cells["Remarks"].Value.ToString();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSqlDB = new SQLDB();
            string sqlText = "";
            int iRes = 0;
            if (IsUpdate == true)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want to Delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    sqlText = "DELETE FROM GC_STK_SALES_CONTRA " +
                                "WHERE GSSC_BRANCH_CODE='" + CommonData.BranchCode +
                                "' AND GSSC_FIN_YEAR='" + CommonData.FinancialYear +
                                "' AND GSSC_TRN_NO=" + txtInvoiceNo.Text;
                    try
                    {
                        iRes = objSqlDB.ExecuteSaveData(sqlText);
                    }
                    catch (Exception ex)
                    {
                        iRes = 0;
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        objSqlDB = null;
                    }

                    if (iRes > 0)
                    {
                        MessageBox.Show("Data Deleted Successfully", "Stock Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInvoiceNo.Text = GenerateNewInvoiceNo().ToString();
                        IsUpdate = false;
                        txtSaleQty.Text = "";
                        txtActQty.Text = "";
                        FillInvoiceList();
                        cmbSaleProduct.SelectedIndex = 0;
                        cmbActProduct.SelectedIndex = 0;
                        txtRemarks.Text = "";
                        txtDocMonth.Text = CommonData.DocMonth.ToUpper();

                    }
                    else
                    {
                        MessageBox.Show("Data not Deleted", "Stock Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid Data", "Stock Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
