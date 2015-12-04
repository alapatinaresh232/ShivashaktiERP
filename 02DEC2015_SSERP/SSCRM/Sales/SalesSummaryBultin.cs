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
using System.Collections;

namespace SSCRM
{
    public partial class SalesSummaryBulletin : Form
    {
        SQLDB objSQLdb = null;
        InvoiceDB objInvdb = null;
        string sLock = "N";
        bool isUpdate = false;
        private string strECode = string.Empty;
        bool flagUpdate = false;
       

        public SalesSummaryBulletin()
        {
            InitializeComponent();
        }

        private void SalesSummaryBulletin_Load(object sender, EventArgs e)
        {           
            txtDocMonth.Text = CommonData.DocMonth;
            CalculateTotals();
            GetGcGlEnames();
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10,
                                                        System.Drawing.FontStyle.Regular);
            dgvFreeProduct.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10,
                                                        System.Drawing.FontStyle.Regular);
        }

        private void GetGcGlEnames()
        {
            objInvdb = new InvoiceDB();
            DataTable dt = new DataTable();
            cbGCEcode.DataSource = null;
            cbSREcode.DataSource = null;
            Int32 iGLCode = 0;
            if (cbGCEcode.Items.Count > 0 && cbGCEcode.SelectedIndex >= 0)
                iGLCode = Convert.ToInt32(cbGCEcode.SelectedValue.ToString());
            else
                iGLCode = 0;
            try
            {
                //if (txtSREcodeSearch.Text.Length > 0)
                //{
                    dt = objInvdb.InvLevelGLEcodeSearch_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, txtGCEcodeSearch.Text.ToString()).Tables[0];
                //}
                if (dt.Rows.Count > 0)
                {
                    //DataRow dr = dt.NewRow();
                    cbGCEcode.DisplayMember = "ENAME";
                    cbGCEcode.ValueMember = "ECODE";
                    cbGCEcode.DataSource = dt;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (cbGCEcode.SelectedIndex > -1)
                {
                    cbGCEcode.SelectedIndex = 0;
                    //strECode = ((System.Data.DataRowView)(cbGCEcode.SelectedItem)).Row.ItemArray[0].ToString();
                }
                objInvdb = null;
                Cursor.Current = Cursors.Default;
            }
        }

        private void GetSREnames(Int32 GCEcode)
        {            
            objInvdb = new InvoiceDB();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds = null;
            cbSREcode.DataSource = null;
            try
            {
                
                string strSRName = txtSREcodeSearch.Text.ToString();
                //cbGCEcode.Items.Clear();
                if (cbGCEcode.SelectedIndex > -1)
                {

                    ds = objInvdb.InvLevelSREcodeSearchByGL_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, GCEcode, strSRName);
                    dt=ds.Tables[0];

                }
                
                 if (dt.Rows.Count > 0)
                {
                    cbSREcode.DisplayMember = "ENAME";
                    cbSREcode.ValueMember = "ECODE";
                    cbSREcode.DataSource = dt;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (cbSREcode.SelectedIndex > -1)
                {
                    cbSREcode.SelectedIndex = 0;
                    strECode = ((System.Data.DataRowView)(cbSREcode.SelectedItem)).Row.ItemArray[0].ToString();
                }
                objInvdb = null;
                Cursor.Current = Cursors.Default;
            }

        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            ProductSearchAll PSearch = new ProductSearchAll("SalesSummaryBulletin");
            PSearch.objSalesSummaryBulletin = this;
            PSearch.ShowDialog();


        }

       
        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int iRes = 0;
            string strCommand = "";
            if (cbSREcode.SelectedIndex>-1 && cbGCEcode.SelectedIndex>-1)
            {
                try
                {
                    strCommand = " DELETE FROM SALES_SUMMARY_DETL_FREE WHERE SBDF_DOC_MONTH='" + CommonData.DocMonth + "' AND SBDF_EORA_CODE=" + cbSREcode.SelectedValue.ToString();
                    strCommand = " DELETE FROM SALES_SUMMARY_DETL WHERE SSBD_DOC_MONTH='" + CommonData.DocMonth + "' AND SSBD_EORA_CODE=" + cbSREcode.SelectedValue.ToString();
                    strCommand += " DELETE FROM SALES_SUMMARY_BULLETINS WHERE SSBH_DOC_MONTH='" + CommonData.DocMonth + "' AND SSBH_EORA_CODE=" + cbSREcode.SelectedValue.ToString();
                    objSQLdb.ExecuteSaveData(strCommand);
                    if (iRes > 1)
                    {
                        MessageBox.Show("Data Deleted Successfully", "SSERP-Bulletins", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    btnCancel_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Records Not Deleted \n\n"+ex.ToString());
                }
                finally
                {
                    objSQLdb = null;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtAdvance.Text = "";
            txtOldAdv.Text = "";
            txtDA.Text = "";
            txtDemos.Text = "";
            txtInvCount.Text = "";
            txtPmd.Text = "";
            txtTolRev.Text = "";
            txtTotPoints.Text = "";
            txtTotQty.Text="";
            gvProductDetails.Rows.Clear();
            dgvFreeProduct.Rows.Clear();
            //txtDocMonth.Text="";
            cbGCEcode.DataSource = null;
            cbSREcode.DataSource = null;
            flagUpdate = false;
        }

      

       
        
        private void cbGCEcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGCEcode.SelectedIndex > -1)
            {
                GetSREnames(Convert.ToInt32(cbGCEcode.SelectedValue.ToString()));
            }
            
            
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            int ival = 0;
            int iRes = 0;
            string strCommand = "";
            CalculateTotals();
            if (CheckData())
            {
                try
                {
                    strCommand = "";
                    if (txtPmd.Text.ToString().Trim().Length == 0)
                        txtPmd.Text = "0";
                    if (txtDemos.Text.ToString().Trim().Length == 0)
                        txtDemos.Text = "0";
                    if (txtInvCount.Text.ToString().Trim().Length == 0)
                        txtInvCount.Text = "0";
                    if (txtAdvance.Text.ToString().Trim().Length == 0)
                        txtAdvance.Text = "0";
                    if (txtDA.Text.ToString().Trim().Length == 0)
                        txtDA.Text = "0";
                    if (txtTotQty.Text.ToString().Trim().Length == 0)
                        txtTotQty.Text = "0";
                    if (txtTolRev.Text.ToString().Trim().Length == 0)
                        txtTolRev.Text = "0";
                    if (txtTotPoints.Text.ToString().Trim().Length == 0)
                        txtTotPoints.Text = "0";
                    if (txtOldAdv.Text.ToString().Trim().Length == 0)
                        txtOldAdv.Text = "0";
                    try
                    {
                        if (cbSREcode.SelectedIndex >= 0)
                        {
                            strCommand += " DELETE FROM SALES_SUMMARY_DETL_FREE WHERE SBDF_DOC_MONTH='" + CommonData.DocMonth + "' AND SBDF_EORA_CODE=" + cbSREcode.SelectedValue.ToString();
                            strCommand += " DELETE FROM SALES_SUMMARY_DETL WHERE SSBD_DOC_MONTH='" + CommonData.DocMonth + "' AND SSBD_EORA_CODE=" + cbSREcode.SelectedValue.ToString();
                            strCommand += " DELETE FROM SALES_SUMMARY_BULLETINS WHERE SSBH_DOC_MONTH='" + CommonData.DocMonth + "' AND SSBH_EORA_CODE=" + cbSREcode.SelectedValue.ToString();
                            objSQLdb.ExecuteSaveData(strCommand);
                        }

                    }
                    catch
                    {

                    }
                    finally
                    {
                        objSQLdb = null;
                    }

                    //if (flagUpdate == true)
                    //{
                    //    strCommand = " UPDATE SALES_SUMMARY_BULLETINS SET SSBH_COMPANY_CODE='" + CommonData.CompanyCode +
                    //                "',SSBH_BRANCH_CODE='" + CommonData.BranchCode + "',SSBH_FIN_YEAR='" + CommonData.FinancialYear +
                    //                "',SSBH_DOC_MONTH='" + txtDocMonth.Text + "',SSBH_PMD=" + Convert.ToDouble(txtPmd.Text).ToString("f") +
                    //                ",SSBH_DA=" + Convert.ToDouble(txtDA.Text).ToString("f") + ",SSBH_DEMOS=" + Convert.ToDouble(txtDemos.Text.ToString()) +
                    //                ",SSBH_CUST=" + Convert.ToInt32(txtInvCount.Text.ToString()) + ",SSBH_QTY=" + txtTotQty.Text.ToString() +
                    //                ",SSBH_REV=" + txtTolRev.Text.ToString() +
                    //                ",SSBH_POINTS=" + txtTotPoints.Text.ToString() +
                    //                ",SSBH_ADV_REC=" + Convert.ToDouble(txtAdvance.Text).ToString("f") +
                    //                ",SSBH_OLD_ADV=" + Convert.ToDouble(txtOldAdv.Text).ToString("f") +
                    //                ",SSBH_MODIFIED_BY='" + CommonData.LogUserId + "',SSBH_MODIFIED_DATE='" + CommonData.CurrentDate +
                    //                "' WHERE SSBH_EORA_CODE= " + Convert.ToInt32(cbSREcode.SelectedValue.ToString()) + " AND SSBH_DOC_MONTH='" + CommonData.DocMonth + "'";


                    //}
                    //else
                    {
                        strCommand = " INSERT INTO SALES_SUMMARY_BULLETINS(SSBH_COMPANY_CODE " +
                                                                        ",SSBH_BRANCH_CODE " +
                                                                        ",SSBH_FIN_YEAR " +
                                                                        ",SSBH_DOC_MONTH " +
                                                                        ",SSBH_EORA_CODE " +
                                                                        ",SSBH_ECODE " +
                                                                        ",SSBH_PMD " +
                                                                        ",SSBH_DA " +
                                                                        ",SSBH_DEMOS " +
                                                                        ",SSBH_CUST " +
                                                                        ",SSBH_QTY " +
                                                                        ",SSBH_REV " +
                                                                        ",SSBH_POINTS " +
                                                                        ",SSBH_ADV_REC " +
                                                                        ",SSBH_OLD_ADV " +
                                                                        ",SSBH_CREATED_BY " +
                                                                        ",SSBH_CREATED_DATE " +
                                                                        ",SSBH_LOCKING " +
                                                                        ")VALUES('" + CommonData.CompanyCode +
                                                                        "','" + CommonData.BranchCode +
                                                                        "','" + CommonData.FinancialYear +
                                                                        "','" + CommonData.DocMonth +
                                                                        "'," + cbSREcode.SelectedValue.ToString() +
                                                                        "," + cbSREcode.SelectedValue.ToString() +
                                                                        "," + Convert.ToDouble(txtPmd.Text).ToString("f") +
                                                                        "," + Convert.ToDouble(txtDA.Text).ToString("f") +
                                                                        "," + Convert.ToDouble(txtDemos.Text.ToString()) +
                                                                        "," + Convert.ToDouble(txtInvCount.Text.ToString()) +
                                                                        "," + txtTotQty.Text.ToString() +
                                                                        "," + txtTolRev.Text.ToString() +
                                                                        "," + txtTotPoints.Text.ToString() +
                                                                        "," + Convert.ToDouble(txtAdvance.Text).ToString("f") +
                                                                        "," + Convert.ToDouble(txtOldAdv.Text).ToString("f") +
                                                                        ",'" + CommonData.LogUserId +
                                                                        "','" + CommonData.CurrentDate +
                                                                        "','N')";


                        //}
                        strCommand += "";
                        objSQLdb = new SQLDB();
                       
                        string sqlText = "SELECT COUNT(*) FROM SALES_DM_SR_DATA WHERE SDSD_DOCUMENT_MONTH='" + CommonData.DocMonth +
                                            "' AND SDSD_EORA_CODE=" + cbSREcode.SelectedValue.ToString();

                        //Convert.ToInt32(objSQLdb.ExecuteDataSet(sqlText).Tables[0].Rows[0][0]);
                        if (Convert.ToInt32(objSQLdb.ExecuteDataSet(sqlText).Tables[0].Rows[0][0]) > 0)
                        {

                            strCommand += " UPDATE SALES_DM_SR_DATA " +
                                            "SET SDSD_COMPANY_CODE='" + CommonData.CompanyCode + "'," +
                                            "SDSD_STATE_CODE='" + CommonData.StateCode + "'," +
                                            "SDSD_BRANCH_CODE='" + CommonData.BranchCode + "'," +
                                            "SDSD_FIN_YEAR='" + CommonData.FinancialYear + "'," +
                                            "SDSD_DOCUMENT_MONTH='" + CommonData.DocMonth + "'," +
                                            "SDSD_GROUP_NAME=''," +
                                            "SDSD_GROUP_LEAD_ECODE=" + cbGCEcode.SelectedValue.ToString() + "," +
                                            "SDSD_PMD=" + txtPmd.Text + "," +
                                            "SDSD_DA_DAYS=" + txtDA.Text + "," +
                                            "SDSD_DEMOS=" + txtDemos.Text + "," +
                                            "SDSD_LAST_MODIFIED_BY='" + CommonData.LogUserId + "'," +
                                            "SDSD_LAST_MODIFIED_DATE=getdate()" +
                                            " WHERE SDSD_EORA_CODE=" + cbSREcode.SelectedValue.ToString() +
                                            " AND SDSD_DOCUMENT_MONTH='" + CommonData.DocMonth + "'";
                        }
                        else
                        {

                            strCommand += " INSERT INTO SALES_DM_SR_DATA" +
                                            "(SDSD_COMPANY_CODE," +
                                            "SDSD_STATE_CODE," +
                                            "SDSD_BRANCH_CODE," +
                                            "SDSD_FIN_YEAR," +
                                            "SDSD_DOCUMENT_MONTH," +
                                            "SDSD_GROUP_NAME," +
                                            "SDSD_GROUP_LEAD_ECODE," +
                                            "SDSD_EORA_CODE," +
                                            "SDSD_PMD," +
                                            "SDSD_DA_DAYS," +
                                            "SDSD_DEMOS," +
                                            "SDSD_CREATED_BY," +
                                            "SDSD_CREATED_DATE) " +
                                            "VALUES('" + CommonData.CompanyCode +
                                            "','" + CommonData.StateCode +
                                            "','" + CommonData.BranchCode +
                                            "','" + CommonData.FinancialYear +
                                            "','" + CommonData.DocMonth +
                                            "','" + "" +
                                            "'," + cbGCEcode.SelectedValue.ToString() +
                                            "," + cbSREcode.SelectedValue.ToString() +
                                            "," + txtPmd.Text +
                                            "," + txtDA.Text +
                                            "," + txtDemos.Text +
                                            ",'" + CommonData.LogUserId +
                                            "',getdate())";
                        }

                    }

                    iRes = objSQLdb.ExecuteSaveData(strCommand);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                if (iRes > 0)
                {
                    if (gvProductDetails.Rows.Count > 0)
                    {
                        //strCommand = "DELETE FROM SALES_SUMMARY_DETL WHERE SSBD_EORA_CODE=" + Convert.ToInt32(cbSREcode.SelectedValue.ToString()) + " AND SSBD_DOC_MONTH='" + CommonData.DocMonth + "'";

                        //iRes = objSQLdb.ExecuteSaveData(strCommand);

                        //strCommand = "DELETE FROM SALES_SUMMARY_DETL_FREE WHERE SBDF_EORA_CODE=" + Convert.ToInt32(cbSREcode.SelectedValue.ToString()) + " AND SBDF_DOC_MONTH='" + CommonData.DocMonth + "'";

                        //ival = objSQLdb.ExecuteSaveData(strCommand);
                        if (SaveSalesSummaryDetail() > 0)
                        {
                            if (SalesSummaryFreedetails() > 0)
                            {
                                MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                flagUpdate = false;
                                //btnCancel_Click(null, null);
                                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                                {
                                    gvProductDetails.Rows[i].Cells["ProdQty"].Value = "0";
                                    gvProductDetails.Rows[i].Cells["ProdAmount"].Value = "0";
                                    gvProductDetails.Rows[i].Cells["ProdTotPoints"].Value = "0";
                                }
                                for (int i = 0; i < dgvFreeProduct.Rows.Count; i++)
                                {
                                    dgvFreeProduct.Rows[i].Cells["FreePrQty"].Value = "0";
                                }
                                CalculateTotals();
                                txtGCEcodeSearch.Focus();

                            }
                        }



                    }

                    else
                    {

                        MessageBox.Show("Data Saved Successfully", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        flagUpdate = false;
                        txtAdvance.Text = "";
                        txtOldAdv.Text = "";
                        txtDA.Text = "";
                        txtDemos.Text = "";
                        txtInvCount.Text = "";
                        txtPmd.Text = "";
                        txtTolRev.Text = "";
                        txtTotPoints.Text = "";
                        txtTotQty.Text = "";
                        //gvProductDetails.Rows.Clear();
                        //txtDocMonth.Text="";
                        //cbGCEcode.DataSource = null;
                        //cbSREcode.DataSource = null;
                        for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                        {
                            gvProductDetails.Rows[i].Cells["ProdQty"].Value = "0";
                            gvProductDetails.Rows[i].Cells["ProdAmount"].Value = "0";
                            gvProductDetails.Rows[i].Cells["ProdTotPoints"].Value = "0";
                        }
                        for (int i = 0; i < dgvFreeProduct.Rows.Count; i++)
                        {
                            dgvFreeProduct.Rows[i].Cells["FreePrQty"].Value = "0";
                        }
                        CalculateTotals();
                        flagUpdate = false;
                        cbGCEcode.SelectedIndex = 0;
                        cbSREcode.SelectedIndex = 0;
                        //btnCancel_Click(null, null);

                    }




                }
                else
                {
                    MessageBox.Show("Data Not Saved ", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }




        }
        

        private bool CheckData()
        {
            bool sFlag = true;
            if (sLock == "Y")
            {
                MessageBox.Show("Sales Entry for this GC/Gl is Locked, \nPlease Contact you manager for Information/Unlocking", "SSERP-SummaryBultn", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private int SaveSalesSummaryDetail()
        {

            objSQLdb = new SQLDB();
            int iRes = 0;

            string strCommand = "";
            try
            {
                //strCommand = "DELETE FROM SALES_SUMMARY_DETL WHERE SSBD_EORA_CODE=" + Convert.ToInt32(cbSREcode.SelectedValue.ToString()) + " AND SSBD_DOC_MONTH='" + CommonData.DocMonth + "'";
                //iRes = objSQLdb.ExecuteSaveData(strCommand);
                //strCommand = "";

                if (gvProductDetails.Rows.Count > 0)
                {
                    int iSlNO = 0;
                    for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                    {
                        if (Convert.ToDouble(gvProductDetails.Rows[i].Cells["ProdQty"].Value.ToString()) > 0)
                        {
                            iSlNO++;
                            strCommand += "INSERT INTO SALES_SUMMARY_DETL(SSBD_COMPANY_CODE " +
                                                                           ",SSBD_BRANCH_CODE " +
                                                                           ",SSBD_FIN_YEAR " +
                                                                           ",SSBD_DOC_MONTH " +
                                                                           ",SSBD_EORA_CODE " +
                                                                           ",SSBD_ECODE " +
                                                                           ",SSBD_SL_NO " +
                                                                           ",SSBD_PRODUCT_ID " +
                                                                           ",SSBD_QTY " +
                                                                           ",SSBD_RATE " +
                                                                           ",SSBD_AMT " +
                                                                           ",SSBD_SNG_PTS " +
                                                                           ",SSBD_TOTAL_POINTS " +
                                                                           ")VALUES('" + CommonData.CompanyCode +
                                                                           "','" + CommonData.BranchCode +
                                                                           "','" + CommonData.FinancialYear +
                                                                           "','" + txtDocMonth.Text.ToString() +
                                                                           "'," + cbSREcode.SelectedValue.ToString() +
                                                                           "," + cbSREcode.SelectedValue.ToString() +
                                                                           "," + iSlNO +
                                                                           ",'" + gvProductDetails.Rows[i].Cells["ProductId"].Value.ToString() +
                                                                           "', " + gvProductDetails.Rows[i].Cells["ProdQty"].Value.ToString() +
                                                                           "," + gvProductDetails.Rows[i].Cells["ProdRate"].Value.ToString() +
                                                                           "," + gvProductDetails.Rows[i].Cells["ProdAmount"].Value.ToString() +
                                                                           "," + gvProductDetails.Rows[i].Cells["ProdPoints"].Value.ToString() +
                                                                           "," + gvProductDetails.Rows[i].Cells["ProdTotPoints"].Value.ToString() + ")";
                        }
                    }
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

            return iRes;
        }
        private int SalesSummaryFreedetails()
        {
            objSQLdb = new SQLDB();
            int ival = 0;
            string StrCommand = "";
            try
            {

                if (dgvFreeProduct.Rows.Count > 0)
                {
                    int iSlNo = 0;
                    for (int i = 0; i < dgvFreeProduct.Rows.Count; i++)
                    {
                        try { Convert.ToDouble(dgvFreeProduct.Rows[i].Cells["FreePrQty"].Value.ToString()); }
                        catch { dgvFreeProduct.Rows[i].Cells["FreePrQty"].Value = "0"; }

                        if (Convert.ToDouble(dgvFreeProduct.Rows[i].Cells["FreePrQty"].Value.ToString()) > 0)
                        {
                            iSlNo++;
                            StrCommand += " INSERT INTO SALES_SUMMARY_DETL_FREE (SBDF_COMP_CODE " +
                                                                             ",SBDF_BRANCH_CODE " +
                                                                             ",SBDF_FIN_YEAR  " +
                                                                             ",SBDF_DOC_MONTH " +
                                                                             ",SBDF_EORA_CODE " +
                                                                             ",SBDF_ECODE " +
                                                                             ",SBDF_SL_NO " +
                                                                             ",SBDF_PRODUCT_ID " +
                                                                             ",SBDF_FREE_QTY " +
                                                                             ")VALUES('" + CommonData.CompanyCode +
                                                                             "','" + CommonData.BranchCode +
                                                                             "','" + CommonData.FinancialYear +
                                                                             "','" + txtDocMonth.Text.ToString() +
                                                                             "'," + cbSREcode.SelectedValue.ToString() +
                                                                             "," + cbSREcode.SelectedValue.ToString() +
                                                                             "," + iSlNo +
                                                                           ",'" + dgvFreeProduct.Rows[i].Cells["FreePrID"].Value.ToString() +
                                                                           "', " + dgvFreeProduct.Rows[i].Cells["FreePrQty"].Value.ToString() + ") ";
                        }
                    }
                }
                else
                {
                    ival = 1;
                }

                if (StrCommand.Length > 10)
                {
                    ival = objSQLdb.ExecuteSaveData(StrCommand);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return ival;


        }
        private void CalculateTotals()
        {
            double amt = 0;
            double totalQty = 0;
            double totalPoints = 0;
            if (gvProductDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    try { Convert.ToDouble(gvProductDetails.Rows[i].Cells["ProdQty"].Value.ToString()); }
                    catch { gvProductDetails.Rows[i].Cells["ProdQty"].Value = 0; }
                    try { Convert.ToDouble(gvProductDetails.Rows[i].Cells["ProdRate"].Value.ToString()); }
                    catch { gvProductDetails.Rows[i].Cells["ProdRate"].Value = 0; }
                    try { Convert.ToDouble(gvProductDetails.Rows[i].Cells["ProdPoints"].Value.ToString()); }
                    catch { gvProductDetails.Rows[i].Cells["ProdPoints"].Value = 0; }

                    gvProductDetails.Rows[i].Cells["ProdAmount"].Value = (Convert.ToDouble(gvProductDetails.Rows[i].Cells["ProdQty"].Value.ToString()) * Convert.ToDouble(gvProductDetails.Rows[i].Cells["ProdRate"].Value.ToString())).ToString("f");
                    gvProductDetails.Rows[i].Cells["ProdTotPoints"].Value = (Convert.ToDouble(gvProductDetails.Rows[i].Cells["ProdQty"].Value.ToString()) * Convert.ToDouble(gvProductDetails.Rows[i].Cells["ProdPoints"].Value.ToString())).ToString("f");

                    try { Convert.ToDouble(gvProductDetails.Rows[i].Cells["ProdAmount"].Value.ToString()); }
                    catch { gvProductDetails.Rows[i].Cells["ProdAmount"].Value = 0; }
                    try { Convert.ToDouble(gvProductDetails.Rows[i].Cells["ProdTotPoints"].Value.ToString()); }
                    catch { gvProductDetails.Rows[i].Cells["ProdTotPoints"].Value = 0; }

                    //if (gvProductDetails.Rows[i].Cells["ProdQty"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["ProdAmount"].Value.ToString() != "" && gvProductDetails.Rows[i].Cells["ProdPoints"].Value.ToString() != "")
                    //{
                    amt += Convert.ToDouble(gvProductDetails.Rows[i].Cells["ProdAmount"].Value);
                    totalQty += Convert.ToDouble(gvProductDetails.Rows[i].Cells["ProdQty"].Value);
                    totalPoints += Convert.ToDouble(gvProductDetails.Rows[i].Cells["ProdTotPoints"].Value);
                    //}
                }

            }
            else
            {
                amt = 0; totalQty = 0; totalPoints = 0;
            }

            txtTolRev.Text = Convert.ToDouble(amt).ToString("f");
            txtTotQty.Text = Convert.ToDouble(totalQty).ToString("f");
            txtTotPoints.Text = Convert.ToDouble(totalPoints).ToString("f");
        }

        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >=3 && e.ColumnIndex<=7)
            {

                if ((Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["ProdQty"].Value) != "") && (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["ProdRate"].Value) != ""))
                {
                    //gvProductDetails.Rows[e.RowIndex].Cells["ProdQty"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["ProdQty"].Value).ToString("f");
                    //gvProductDetails.Rows[e.RowIndex].Cells["ProdRate"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["ProdRate"].Value).ToString("f");


                    gvProductDetails.Rows[e.RowIndex].Cells["ProdAmount"].Value = (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["ProdQty"].Value) * Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["ProdRate"].Value));
                    gvProductDetails.Rows[e.RowIndex].Cells["ProdAmount"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["ProdAmount"].Value).ToString("f");

                }
                if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["ProdPoints"].Value) != "" && Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["ProdQty"].Value) != "")
                {
                    gvProductDetails.Rows[e.RowIndex].Cells["ProdTotPoints"].Value = (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["ProdPoints"].Value) * Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["ProdQty"].Value));
                    gvProductDetails.Rows[e.RowIndex].Cells["ProdTotPoints"].Value = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["ProdTotPoints"].Value).ToString("f");
                }
            }
            CalculateTotals();

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
                    if (gvProductDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                        {
                            gvProductDetails.Rows[i].Cells["slNo"].Value = i + 1;
                        }
                    }
                }
            }
        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvProductDetails.Rows.Clear();
        }
        


        //private void FillSalesSRBulletinHead(DataTable dtH,DataTable dtD)
        //{
        //    //objSQLdb = new SQLDB();
        //    //DataTable dt = new DataTable();
        //    try
        //    {
        //        //string strCmd = "SELECT SSBH_DA " +
        //        //                ",SSBH_ADV_REC " +
        //        //                ",SSBH_PMD " +
        //        //                ",SSBH_POINTS " +
        //        //                ",SSBH_CUST " +
        //        //                ",SSBH_DEMOS " +
        //        //                ",SSBH_DOC_MONTH " +
        //        //                ",SSBD_AMT " +
        //        //                ",SSBD_QTY " +
        //        //                ",SSBD_RATE " +
        //        //                ",SSBD_SNG_PTS " +
        //        //                ",SSBD_TOTAL_POINTS " +
        //        //                ",SSBD_PRODUCT_ID " +
        //        //                ",PM_PRODUCT_NAME " +
        //        //                ",CATEGORY_NAME " +
        //        //                " FROM SALES_SUMMARY_DETL " +
        //        //                " INNER JOIN SALES_SUMMARY_BULLETINS ON SSBH_EORA_CODE=SSBD_EORA_CODE " +
        //        //                " INNER JOIN PRODUCT_MAS ON PM_PRODUCT_ID=SSBD_PRODUCT_ID " +
        //        //                " INNER JOIN CATEGORY_MASTER ON CATEGORY_ID=PM_CATEGORY_ID " +
        //        //                " WHERE SSBD_EORA_CODE=" + SREcode;
        //        //dt = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
        //        //if (dt.Rows.Count > 0)
        //        //{
        //        //    flagUpdate = true;
        //        //    txtDocMonth.Text = dt.Rows[0]["SSBH_DOC_MONTH"].ToString();
        //        //    txtPmd.Text = dt.Rows[0]["SSBH_PMD"].ToString();
        //        //    txtDA.Text = dt.Rows[0]["SSBH_DA"].ToString();
        //        //    txtDemos.Text = dt.Rows[0]["SSBH_DEMOS"].ToString();
        //        //    txtInvCount.Text = dt.Rows[0]["SSBH_CUST"].ToString();
        //        //    txtTolRev.Text = dt.Rows[0]["SSBD_AMT"].ToString();
        //        //    txtTotPoints.Text = dt.Rows[0]["SSBH_POINTS"].ToString();
        //        //    for (int iVar = 0; iVar < dt.Rows.Count; iVar++)
        //        //    {
        //        //        gvProductDetails.Rows.Add();
        //        //        gvProductDetails.Rows[iVar].Cells["slNo"].Value = (iVar + 1).ToString();
        //        //        gvProductDetails.Rows[iVar].Cells["ProductId"].Value = dt.Rows[iVar]["SSBD_PRODUCT_ID"].ToString();
        //        //        gvProductDetails.Rows[iVar].Cells["productName"].Value = dt.Rows[iVar]["PM_PRODUCT_NAME"].ToString();
        //        //        gvProductDetails.Rows[iVar].Cells["categoryName"].Value = dt.Rows[iVar]["CATEGORY_NAME"].ToString();
        //        //        gvProductDetails.Rows[iVar].Cells["ProdQty"].Value = dt.Rows[iVar]["SSBD_QTY"].ToString();
        //        //        gvProductDetails.Rows[iVar].Cells["ProdRate"].Value = dt.Rows[iVar]["SSBD_RATE"].ToString();
        //        //        gvProductDetails.Rows[iVar].Cells["ProdAmount"].Value = dt.Rows[iVar]["SSBD_AMT"].ToString();
        //        //        gvProductDetails.Rows[iVar].Cells["ProdPoints"].Value = dt.Rows[iVar]["SSBD_SNG_PTS"].ToString();
        //        //        gvProductDetails.Rows[iVar].Cells["ProdTotPoints"].Value = dt.Rows[iVar]["SSBD_TOTAL_POINTS"].ToString();
        //        //    }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        objSQLdb = null;
        //        dt = null;
        //    }
        //}

        private void cbSREcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSREcode.SelectedIndex > -1)
            {
                getSalesSRProductDetails(Convert.ToInt32(cbSREcode.SelectedValue.ToString()));
            }
            else
            {
                flagUpdate = false;
                txtAdvance.Text = "";
                txtOldAdv.Text = "";
                txtDA.Text = "";
                txtDemos.Text = "";
                txtInvCount.Text = "";
                txtPmd.Text = "";
                txtTolRev.Text = "";
                txtTotPoints.Text = "";
                txtTotQty.Text = "";
                //gvProductDetails.Rows.Clear();
                //FillSRPMDDADemos(ecode);
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    gvProductDetails.Rows[i].Cells["ProdQty"].Value = "0";
                    gvProductDetails.Rows[i].Cells["ProdAmount"].Value = "0";
                    gvProductDetails.Rows[i].Cells["ProdTotPoints"].Value = "0";
                }
                for (int i = 0; i < dgvFreeProduct.Rows.Count; i++)
                {
                    dgvFreeProduct.Rows[i].Cells["FreePrQty"].Value = "0";
                }
                CalculateTotals();
            }
        }

        private void getSalesSRProductDetails(int ecode)
        {
            objSQLdb = new SQLDB();
            objInvdb = new InvoiceDB();
            Hashtable ht = new Hashtable();
            DataTable dtHead = new DataTable();
            DataTable dtDetl = new DataTable();
            DataTable dtFreeDetl = new DataTable(); 
            try
            {
                ht = objInvdb.SRSummaryBulletin_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, ecode.ToString());
                dtHead = (DataTable)ht["SRSumHead"];
                dtDetl = (DataTable)ht["SRSumDetl"];
                dtFreeDetl = (DataTable)ht["SRFreeProdDetl"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                objInvdb = null;
            }

            if (dtHead.Rows.Count > 0)
            {

                flagUpdate = true;
                txtPmd.Text = dtHead.Rows[0]["Pmd"].ToString();
                txtDA.Text = dtHead.Rows[0]["Da"].ToString();
                txtDemos.Text = dtHead.Rows[0]["Demos"].ToString();
                txtInvCount.Text = dtHead.Rows[0]["Cust"].ToString();
                txtTotQty.Text = dtHead.Rows[0]["Qty"].ToString();
                txtTolRev.Text = dtHead.Rows[0]["Rev"].ToString();
                txtTotPoints.Text = dtHead.Rows[0]["Points"].ToString();
                txtAdvance.Text = dtHead.Rows[0]["AdvAmt"].ToString();
                txtOldAdv.Text = dtHead.Rows[0]["OldAdv"].ToString();
                sLock = dtHead.Rows[0]["LockStatus"].ToString();
                FillSRSummaryBulletinDetlToGrid(dtDetl);
                FillFreeProdDetailsToGrid(dtFreeDetl);
                

            }
            else
            {
                flagUpdate = false;
                txtAdvance.Text = "";
                txtOldAdv.Text = "";
                txtDA.Text = "";
                txtDemos.Text = "";
                txtInvCount.Text = "";
                txtPmd.Text = "";
                txtTolRev.Text = "";
                txtTotPoints.Text = "";
                sLock = "N";
                txtTotQty.Text = "";
                //gvProductDetails.Rows.Clear();
                FillSRPMDDADemos(ecode);
                //txtDocMonth.Text=""; 
                for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                {
                    gvProductDetails.Rows[i].Cells["ProdQty"].Value = "0";
                    gvProductDetails.Rows[i].Cells["ProdAmount"].Value = "0";
                    gvProductDetails.Rows[i].Cells["ProdTotPoints"].Value = "0";
                }
                for (int i = 0; i < dgvFreeProduct.Rows.Count; i++)
                {
                    dgvFreeProduct.Rows[i].Cells["FreePrQty"].Value = "0";
                }
                CalculateTotals();
            }
          

        }

        private void FillSRPMDDADemos(int ecode)
        {           
            objInvdb = new InvoiceDB();
            DataTable dt = new DataTable();
            try
            {
                dt = objInvdb.SRPMDDADemos_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, ecode.ToString()).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtPmd.Text = dt.Rows[0]["Pmd"].ToString();
                    txtDA.Text = dt.Rows[0]["Da"].ToString();
                    txtDemos.Text = dt.Rows[0]["Demos"].ToString();
                }
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            finally
            {                
                objInvdb = null;
            }
        }

        private void FillSRSummaryBulletinDetlToGrid(DataTable dtDetl)
        {
            //gvProductDetails.Rows.Clear();
            try
            {
                int intRow = 1;
                //gvProductDetails.Rows.Clear();
                if (dtDetl.Rows.Count > 0)
                {
                    gvProductDetails.Rows.Clear();
                    for (int i = 0; i < dtDetl.Rows.Count; i++)
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                        cellSLNO.Value = intRow;
                        tempRow.Cells.Add(cellSLNO);

                        DataGridViewCell cellProdID = new DataGridViewTextBoxCell();
                        cellProdID.Value = dtDetl.Rows[i]["ProdID"].ToString();
                        tempRow.Cells.Add(cellProdID);

                        DataGridViewCell cellProdName = new DataGridViewTextBoxCell();
                        cellProdName.Value = dtDetl.Rows[i]["ProdName"].ToString();
                        tempRow.Cells.Add(cellProdName);

                        DataGridViewCell cellCatName = new DataGridViewTextBoxCell();
                        cellCatName.Value = dtDetl.Rows[i]["CatName"].ToString();
                        tempRow.Cells.Add(cellCatName);

                        DataGridViewCell cellQty = new DataGridViewTextBoxCell();
                        cellQty.Value = dtDetl.Rows[i]["Qty"].ToString();
                        tempRow.Cells.Add(cellQty);

                        DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                        cellRate.Value = dtDetl.Rows[i]["Rate"].ToString();
                        tempRow.Cells.Add(cellRate);

                        DataGridViewCell cellAmt = new DataGridViewTextBoxCell();
                        cellAmt.Value = dtDetl.Rows[i]["Amt"].ToString();
                        tempRow.Cells.Add(cellAmt);

                        DataGridViewCell cellPoints = new DataGridViewTextBoxCell();
                        cellPoints.Value = dtDetl.Rows[i]["Points"].ToString();
                        tempRow.Cells.Add(cellPoints);

                        DataGridViewCell cellTotPoints = new DataGridViewTextBoxCell();
                        cellTotPoints.Value = dtDetl.Rows[i]["TotalPoints"].ToString();
                        tempRow.Cells.Add(cellTotPoints);

                        intRow++;
                        gvProductDetails.Rows.Add(tempRow);

                    }
                }
                else
                {
                    for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                    {
                        gvProductDetails.Rows[i].Cells["ProdQty"].Value = "0";
                    }
                    CalculateTotals();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dtDetl = null;
            }
        }

        private void FillFreeProdDetailsToGrid(DataTable dtFreeProd)
        {
            dgvFreeProduct.Rows.Clear();
            
            try
            {
                if (dtFreeProd.Rows.Count > 0)
                {
                    for (int i = 0; i < dtFreeProd.Rows.Count; i++)
                    {
                        dgvFreeProduct.Rows.Add();
                        dgvFreeProduct.Rows[i].Cells["SLNOFree"].Value = (i + 1).ToString();
                        dgvFreeProduct.Rows[i].Cells["FreePrID"].Value = dtFreeProd.Rows[i]["ProductId"].ToString();
                        dgvFreeProduct.Rows[i].Cells["FreePrNAme"].Value = dtFreeProd.Rows[i]["ProductName"].ToString();
                        dgvFreeProduct.Rows[i].Cells["FreePRCat"].Value = dtFreeProd.Rows[i]["CategoryName"].ToString();
                        dgvFreeProduct.Rows[i].Cells["FreePrQty"].Value = dtFreeProd.Rows[i]["FreeQty"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dtFreeProd = null;
            }

        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }
        private void gvProductDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);           
        }

      

        private void txtPmd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }

        }

        private void txtDA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }

        }

        private void txtDemos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }

        }

        private void txtInvCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void txtAdvance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void txtSREcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtSREcodeSearch.Text.Length > 0)
            {
                GetSRNames();
            }
        }

        private void txtGCEcodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            //if (txtGCEcodeSearch.Text.Length > 0)
            //  {
            GetGcGlEnames();
            //}
        }

        private void GetSRNames()
        {
            objInvdb = new InvoiceDB();
            DataTable dt = new DataTable();
            cbSREcode.DataSource = null;
            Int32 iGLCode = 0;
            if (cbGCEcode.Items.Count > 0 && cbGCEcode.SelectedIndex >= 0)
                iGLCode = Convert.ToInt32(cbGCEcode.SelectedValue.ToString());
            else
                iGLCode = 0;
            try
            {
                //if (txtSREcodeSearch.Text.Length > 0)
                //{
                dt = objInvdb.InvLevelSREcodeSearchByGL_Get(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, iGLCode,txtSREcodeSearch.Text.ToString()).Tables[0];
                //}
                if (dt.Rows.Count > 0)
                {
                    //DataRow dr = dt.NewRow();
                    cbSREcode.DisplayMember = "ENAME";
                    cbSREcode.ValueMember = "ECODE";
                    cbSREcode.DataSource = dt;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (cbSREcode.SelectedIndex > -1)
                {
                    cbSREcode.SelectedIndex = 0;
                    //strECode = ((System.Data.DataRowView)(cbGCEcode.SelectedItem)).Row.ItemArray[0].ToString();
                }
                objInvdb = null;
                Cursor.Current = Cursors.Default;
            }
        }

       

        private void dgvFreeProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvFreeProduct.Columns["FreeDel"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    DataGridViewRow dr = dgvFreeProduct.Rows[e.RowIndex];
                    dgvFreeProduct.Rows.Remove(dr);
                    if (dgvFreeProduct.Rows.Count>0)
                    {
                        for (int i = 0; i<dgvFreeProduct.Rows.Count; i++)
                        {
                            dgvFreeProduct.Rows[i].Cells["SLNOFree"].Value = (i + 1).ToString();
                        }
                    }
                }
            }
        }

        private void btnClrFreeProducts_Click(object sender, EventArgs e)
        {
            dgvFreeProduct.Rows.Clear();
        }

        private void btnAddFreeProducts_Click(object sender, EventArgs e)
        {
            ProductSearchAll freeProdSearch = new ProductSearchAll("FreeProduct");
            freeProdSearch.objSalesSummaryBulletin = this;
            freeProdSearch.Show();
        }

      
     
    }
}
