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

namespace SSCRM
{
    public partial class FinishedGoods : Form
    {
        private Master objMaster = null;
        private InvoiceDB  objInv = null;
        private ProductUnitDB objPU = null;
        private SQLDB objData = null;
        string strBCode = string.Empty;
        private General objGeneral = new General();
        private string strProductId = string.Empty;
        private string strFLoad = "NO";
        private string strModify = "NO";
        public FinishedGoods()
        {
            InitializeComponent();
        }
        private void FinishedGoods_Load(object sender, EventArgs e)
        {
            strFLoad = "YES";
            btnDelete.Enabled = false;
            gvProduct.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                      System.Drawing.FontStyle.Regular);

            gvProduct.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Bold);
            //FillCompanyData();
            
                //objGeneral.GetComboBoxSelectedIndex(CommonData.CompanyName, cbCompany);
            txtBranchName.Text = CommonData.BranchName;
            FillCategoryComboBox();
            dtpBatchDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            dtpMFGDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            dtpEXPDate.Value = Convert.ToDateTime(CommonData.CurrentDate).AddYears(1);
            TimeSpan tDays = dtpEXPDate.Value - dtpMFGDate.Value;
            txtEXPDays.Text = tDays.TotalDays.ToString();
            strFLoad = "NO";
            //cbCompany.SelectedValue = CommonData.CompanyCode;
            //cbCompany.Enabled = false;
        }
        //private void FillCompanyData()
        //{
        //    DataSet ds = null;
        //    Security objComp = new Security();
        //    try
        //    {
        //        ds = new DataSet();
        //        ds = objComp.GetCompanyDataSet();
        //        DataTable dtCompany = ds.Tables[0];
        //        if (dtCompany.Rows.Count > 0)
        //        {
        //            foreach (DataRow dataRow in dtCompany.Rows)
        //            {
        //                ComboboxItem objItem = new ComboboxItem();
        //                objItem.Value = dataRow["CM_Company_Code"].ToString();
        //                objItem.Text = dataRow["CM_Company_Name"].ToString();
        //                cbCompany.Items.Add(objItem);
        //                objItem = null;

        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        objComp = null;
        //        ds.Dispose();
        //    }

        //}

        private void FillCategoryComboBox()
        {
            objMaster = new Master();
            try
            {
                DataTable dt = objMaster.Get_PUProductCategory(CommonData.CompanyCode,CommonData.BranchCode).Tables[0];
                if (dt.Rows.Count > 1)
                {
                    cbCategory.DataSource = dt;
                    cbCategory.DisplayMember = "CATEGORY_NAME";
                    cbCategory.ValueMember = "CATEGORY_ID";
                }
                dt = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                objMaster = null;
            }
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                if (cbCategory.SelectedIndex > -1 && strModify=="NO")
                {
                    cbProduct.DataSource = null;
                    cbProduct.DataBindings.Clear();
                    cbProduct.Items.Clear();
                    FillProductData(cbCategory.SelectedValue.ToString());
                    FillProductBatchList();
                }
            
        }

        private void FillProductData(string sCategoryCode)
        {
            objInv = new InvoiceDB();
            
            try
            {
                if (strFLoad == "NO" && strModify == "NO")
                {
                    DataTable dt = objInv.GetProductMasterSearch(CommonData.CompanyCode, sCategoryCode).Tables[0];
                    DataRow dr = dt.NewRow();
                    dr[0] = "Select";
                    dr[1] = "Select";
                    dt.Rows.InsertAt(dr, 0);
                    dr = null;
                    if (dt.Rows.Count > 1)
                    {
                        cbProduct.DataSource = dt;
                        cbProduct.DisplayMember = "pm_product_name";
                        cbProduct.ValueMember = "pm_product_id";

                    }
                    dt = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                objInv = null;
            }

        }

      

        private void FillProductBatchList()
        {
            objPU = new ProductUnitDB();
            DataTable dt = null;
            try
            {
                if (strFLoad == "NO" && strModify=="NO")
                {
                    if (cbCategory.SelectedIndex != -1 && cbProduct.SelectedIndex >-1)
                    {

                        this.gvProduct.Rows.Clear();

                        if (cbProduct.SelectedIndex > 0)
                            dt = objPU.FinishedProductList_Get(CommonData.CompanyCode, CommonData.BranchCode, cbProduct.SelectedValue.ToString(), cbCategory.SelectedValue.ToString()).Tables[0];
                        else
                            dt = objPU.FinishedProductList_Get(CommonData.CompanyCode, CommonData.BranchCode, string.Empty, cbCategory.SelectedValue.ToString()).Tables[0];

                        if (dt.Rows.Count > 0)
                        {
                            for (int intRow = 0; intRow < dt.Rows.Count; intRow++)
                            {
                                gvProduct.Rows.Add();
                                gvProduct.Rows[intRow].Cells["SLNO"].Value = intRow + 1;
                                gvProduct.Rows[intRow].Cells["ProductId"].Value = dt.Rows[intRow]["ProductId"].ToString();
                                gvProduct.Rows[intRow].Cells["ProductName"].Value = dt.Rows[intRow]["Product"].ToString();
                                gvProduct.Rows[intRow].Cells["BatchNo"].Value = dt.Rows[intRow]["BatchNo"].ToString();
                                gvProduct.Rows[intRow].Cells["BatchDate"].Value = Convert.ToDateTime(dt.Rows[intRow]["BatchDate"]).ToString("dd/MM/yyyy");
                                gvProduct.Rows[intRow].Cells["MFGDate"].Value = Convert.ToDateTime(dt.Rows[intRow]["MFGDate"]).ToString("dd/MM/yyyy");
                                gvProduct.Rows[intRow].Cells["EXPDate"].Value = Convert.ToDateTime(dt.Rows[intRow]["EXPDate"]).ToString("dd/MM/yyyy");
                                gvProduct.Rows[intRow].Cells["ExpDays"].Value = dt.Rows[intRow]["ExpDays"].ToString();
                                gvProduct.Rows[intRow].Cells["Qty"].Value = dt.Rows[intRow]["Qty"].ToString();
                                gvProduct.Rows[intRow].Cells["Rate"].Value = dt.Rows[intRow]["Rate"].ToString();
                                gvProduct.Rows[intRow].Cells["Amount"].Value = dt.Rows[intRow]["Amount"].ToString();
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objPU = null;
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            objGeneral = null;
            this.Close();
            this.Dispose();
        }

        private void gvProduct_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                
            }
            catch
            {
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strSQl = string.Empty;
            objData = new SQLDB();
            objMaster = new Master();
            
            try
            {
                if (CheckData())
                {
                    DataTable dt = objData.ExecuteDataSet("SELECT * FROM  PRODUCT_BATCH_MASTER " +
                                            " WHERE PBM_PRODUCT_ID='" + strProductId +
                                            "' AND PBM_BATCH_NO='" +txtBatchNo.Text+  
                                            "' AND PBM_BRANCH_CODE='" + CommonData.BranchCode +
                                            "' AND PBM_FIN_YEAR ='" + CommonData.FinancialYear +
                                            "' AND PBM_COMPANY_CODE='" + CommonData.CompanyCode + "';").Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        strSQl = " UPDATE PRODUCT_BATCH_MASTER " +
                                 " SET PBM_BATCH_NO = '" + txtBatchNo.Text.Replace(" ", "") +
                                 "', PBM_BATCH_DATE = '" + dtpBatchDate.Value.ToString("dd/MMM/yyyy") +
                                 "', PBM_BATCH_MFG_DATE = '" + dtpMFGDate.Value.ToString("dd/MMM/yyyy") +
                                 "', PBM_BATCH_EXP_DAYS  = '" + txtEXPDays.Text +
                                 "', PBM_BATCH_EXP_DATE  = '" + dtpMFGDate.Value.ToString("dd/MMM/yyyy") +
                                 "', PBM_QTY=" + txtQty.Text +
                                 ", PBM_RATE=" + txtRate.Text +
                                 ", PBM_AMT=" + txtAmount.Text +
                                 ", PBM_LAST_MODIFIED_BY='" + CommonData.LogUserId +
                                 "', PBM_LAST_MODIFIED_DATE=getdate()" + 
                                 " WHERE " +
                                 " PBM_PRODUCT_ID='" + strProductId +
                                 "' AND PBM_BATCH_NO='" + txtBatchNo.Text +  
                                 "' AND PBM_BRANCH_CODE='" + CommonData.BranchCode +
                                 "' AND PBM_FIN_YEAR ='" + CommonData.FinancialYear +
                                 "' AND PBM_COMPANY_CODE='" + CommonData.CompanyCode + "'";

                    }
                    else
                    {
                        strSQl = " INSERT into PRODUCT_BATCH_MASTER(PBM_COMPANY_CODE, PBM_STATE_CODE, PBM_BRANCH_CODE, PBM_FIN_YEAR, PBM_PRODUCT_ID, PBM_BATCH_NO, PBM_BATCH_DATE, PBM_BATCH_MFG_DATE, PBM_BATCH_EXP_DAYS, PBM_BATCH_EXP_DATE, PBM_QTY, PBM_RATE, PBM_AMT, PBM_CREATED_BY, PBM_CREATED_DATE)" +
                                 " VALUES('" + CommonData.CompanyCode +
                                 "', '" + CommonData.StateCode +
                                 "', '" + CommonData.BranchCode +
                                 "', '" + CommonData.FinancialYear +
                                 "', '" + strProductId +
                                 "', '" + txtBatchNo.Text.Replace(" ", "") +
                                 "', '" + dtpBatchDate.Value.ToString("dd/MMM/yyyy") +
                                 "', '" + dtpMFGDate.Value.ToString("dd/MMM/yyyy") +
                                 "', " + txtEXPDays.Text +
                                 ", '" + dtpEXPDate.Value.ToString("dd/MMM/yyyy") +
                                 "', " + txtQty.Text +
                                 ", " + txtRate.Text +
                                 ", " + txtAmount.Text +
                                 ", '" + CommonData.LogUserId +
                                 "', getdate())";
                    }

                    int rec = objData.ExecuteSaveData(strSQl);
                    CleareEntryData();
                    btnDelete.Enabled = false;
                    strModify = "NO";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
                objMaster = null;
             }
        }
        private bool CheckData()
        {
            bool blValue = true;

            if (cbProduct.SelectedIndex == 0)
            {
                MessageBox.Show("Select Product", "Finished Goods");
                blValue = false;
                cbProduct.Focus();
                return blValue;
            }

            if (txtBatchNo.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter batchno!", "Finished Goods");
                blValue = false;
                txtBatchNo.Focus();
                return blValue;
            }
            if (txtQty.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter quantity!", "Finished Goods");
                blValue = false;
                txtQty.Focus();
                return blValue;
            }
            if (txtRate.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter Rate!", "Finished Goods");
                blValue = false;
                txtRate.Focus();
                return blValue;
            }
            if (txtEXPDays.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter EXP Days!", "Finished Goods");
                blValue = false;
                txtEXPDays.Focus();
                return blValue;
            }

            return blValue;
        }
        private bool CheckDeleteData()
        {
            bool blValue = true;

            if (cbProduct.SelectedIndex == 0)
            {
                MessageBox.Show("Select Product", "Finished Goods");
                blValue = false;
                cbProduct.Focus();
                return blValue;
            }

            if (txtBatchNo.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("Enter batchno!", "Finished Goods");
                blValue = false;
                txtBatchNo.Focus();
                return blValue;
            }
            return blValue;
        }
        private void CleareEntryData()
        {
            this.gvProduct.Rows.Clear();
            txtBatchNo.Text = "";
            txtQty.Text = "";
            txtRate.Text = "";
            txtAmount.Text = "";
            txtEXPDays.Text = "";
            cbProduct.SelectedIndex = 0;
            btnDelete.Enabled = false;
            FillProductBatchList();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strSQl = string.Empty;
            objData = new SQLDB();
            objMaster = new Master();
            try
            {
                if (CheckDeleteData())
                {
                    DialogResult result = MessageBox.Show("Do you want to delete " + cbProduct.Text + " product ?",
                                            "Finished Goods", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {

                        strSQl = " DELETE FROM  PRODUCT_BATCH_MASTER" +
                                    " WHERE PBM_PRODUCT_ID='" + strProductId +
                                    "' AND PBM_BATCH_NO='" + txtBatchNo.Text +
                                     "' AND PBM_BRANCH_CODE='" + CommonData.BranchCode +
                                     "' AND PBM_FIN_YEAR ='" + CommonData.FinancialYear +
                                     "' AND PBM_COMPANY_CODE='" + CommonData.CompanyCode + "'";

                        int rec = objData.ExecuteSaveData(strSQl);
                        CleareEntryData();
                        strModify = "NO";
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
                objMaster = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CleareEntryData();
            strModify = "NO";
        }

        private void txtBatchNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (char.IsLetterOrDigit(e.KeyChar) == false)
                e.Handled = true;
            e.KeyChar = Char.ToUpper(e.KeyChar);

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (char.IsDigit(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
         
        }

        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false)
                e.Handled = true;
            
            if (e.KeyChar == 8)
                e.Handled = false;

         }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsDigit(e.KeyChar) == false)
                e.Handled = true;

            if (e.KeyChar == 8)
                e.Handled = false;
        }

        //private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cbCompany.SelectedIndex > -1)
        //        FillCategoryComboBox();
        //}

        private void dtpMFGDate_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void dtpEXPDate_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void cbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProduct.SelectedIndex > 0)
            {
                strProductId = cbProduct.SelectedValue.ToString();
                FillProductBatchList();
            }
        }

        private void txtQty_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtQty.Text.Length > 0 && txtRate.Text.Length > 0)
                txtAmount.Text = Convert.ToDouble(Convert.ToDouble(txtQty.Text) * Convert.ToDouble(txtRate.Text)).ToString("f");

        }

        private void txtRate_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtQty.Text.Length > 0 && txtRate.Text.Length > 0)
                txtAmount.Text = Convert.ToDouble(Convert.ToDouble(txtQty.Text) * Convert.ToDouble(txtRate.Text)).ToString("f");

        }

        private void gvProduct_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dtpMFGDate_KeyUp(object sender, KeyEventArgs e)
        {
            TimeSpan tDays = dtpEXPDate.Value - dtpMFGDate.Value;

            txtEXPDays.Text = Math.Round(tDays.TotalDays).ToString();
        }

        private void dtpEXPDate_KeyUp(object sender, KeyEventArgs e)
        {
            TimeSpan tDays = dtpEXPDate.Value - dtpMFGDate.Value;
            txtEXPDays.Text = Math.Round(tDays.TotalDays).ToString();
        }

        private void gvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            strModify = "YES";
            strProductId = gvProduct.Rows[e.RowIndex].Cells["ProductId"].Value.ToString();
            cbProduct.SelectedValue = strProductId;
            txtBatchNo.Text = gvProduct.Rows[e.RowIndex].Cells["BatchNo"].Value.ToString();
            txtEXPDays.Text = gvProduct.Rows[e.RowIndex].Cells["EXPDays"].Value.ToString();
            txtAmount.Text = gvProduct.Rows[e.RowIndex].Cells["Amount"].Value.ToString();
            txtRate.Text = gvProduct.Rows[e.RowIndex].Cells["Rate"].Value.ToString();
            txtQty.Text = gvProduct.Rows[e.RowIndex].Cells["Qty"].Value.ToString();
            dtpBatchDate.Value = Convert.ToDateTime(gvProduct.Rows[e.RowIndex].Cells["BatchDate"].Value.ToString());
            dtpMFGDate.Value = Convert.ToDateTime(gvProduct.Rows[e.RowIndex].Cells["MFGDate"].Value.ToString());
            dtpEXPDate.Value = Convert.ToDateTime(gvProduct.Rows[e.RowIndex].Cells["EXPDate"].Value.ToString());
            btnDelete.Enabled = true;
        }

        private void txtBatchNo_Validated(object sender, EventArgs e)
        {
            
          
            try
            {
                if (cbProduct.SelectedIndex > 0)
                {
                    if (txtBatchNo.Text.Trim().Length > 0)
                    {
                        objData = new SQLDB();
                        string strSQl = " SELECT PBM_BATCH_NO BatchNo , PBM_BATCH_DATE BatchDate, PBM_PRODUCT_ID ProductId, " +
                                        "PBM_BATCH_MFG_DATE MFGDate, PBM_BATCH_EXP_DATE EXPDate, PBM_BATCH_EXP_DAYS ExpDays, " +
                                        "PBM_QTY QTY, PBM_RATE RATE, PBM_AMT Amount " +
                                        ", pm_category_id as CategoryID " +
                                        ", category_name as CategoryName, pm_product_name As Product " +
                                        "FROM PRODUCT_BATCH_MASTER PBM " +
                                        "Inner Join PRODUCT_MAS  PM " +
                                        "on PBM.PBM_PRODUCT_ID=PM.pm_product_id " +
                                        "Inner Join category_master CM " +
                                        "on CM.category_id=PM.pm_category_id " +
                                        "WHERE PBM_COMPANY_CODE ='" + CommonData.CompanyCode +
                                        "' AND PBM_BRANCH_CODE ='" + CommonData.BranchCode +
                                        "' AND PBM_FIN_YEAR = '" + CommonData.FinancialYear +
                                        "' AND pm_category_id = '" + cbCategory.SelectedValue.ToString() +
                                        "' AND upper(PBM_PRODUCT_ID) ='" + cbProduct.SelectedValue.ToString().ToUpper() +
                                        "' AND PBM_BATCH_NO='" + txtBatchNo.Text.ToUpper() + "'" ;
                        DataTable dt = objData.ExecuteDataSet(strSQl).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            strModify = "YES";
                            strProductId = dt.Rows[0]["ProductId"].ToString();
                            cbProduct.SelectedValue = strProductId;// dt.Rows[intRow]["Product"].ToString();
                            dtpBatchDate.Value = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["BatchDate"]).ToString("dd/MM/yyyy"));
                            dtpMFGDate.Value = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["MFGDate"]).ToString("dd/MM/yyyy"));
                            dtpEXPDate.Value = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["EXPDate"]).ToString("dd/MM/yyyy"));
                            txtEXPDays.Text = dt.Rows[0]["ExpDays"].ToString();
                            txtQty.Text = dt.Rows[0]["Qty"].ToString();
                            txtRate.Text = dt.Rows[0]["Rate"].ToString();
                            txtAmount.Text = dt.Rows[0]["Amount"].ToString();
                        }
                        objData = null;
                    }
                }
            }
            catch
            {
            }
           
        }

        private void dtpMFGDate_ValueChanged(object sender, EventArgs e)
        {
            dtpEXPDate.Value = dtpMFGDate.Value.AddYears(1);
            TimeSpan tDays = dtpEXPDate.Value - dtpMFGDate.Value;

            txtEXPDays.Text = Math.Round(tDays.TotalDays).ToString();
        }

        private void dtpEXPDate_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan tDays = dtpEXPDate.Value - dtpMFGDate.Value;
            txtEXPDays.Text = Math.Round(tDays.TotalDays).ToString();
        }
        
    }
}
