using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSCRM;
using SSTrans;
using SSCRM.App_Code;
namespace SSCRM
{
    public partial class FreeProducts : Form
    {
        SQLDB objSQLDB;
        ServiceDB oServiceDB;
        Security objSecurity;
        HRInfo objHRInfo;
        public FreeProducts()
        {
            InitializeComponent();
        }

        private void FreeProducts_Load(object sender, EventArgs e)
        {
            objSecurity = new Security();
            UtilityLibrary.PopulateControl(cbCompany, objSecurity.GetCompanyDataSet().Tables[0].DefaultView, 1, 0, "-- Please Select --", 0);
            btnDelete.Enabled = false;
            lblOfferID.Text = "0";
            objSQLDB = new SQLDB();
            string sqlQry = "SELECT PM_PRODUCT_ID,PM_PRODUCT_NAME FROM dbo.PRODUCT_MAS a " +
                            "inner join product_mas_company b on a.pm_product_id=b.pmc_product_id " +
                            "where pmc_product_company='" + CommonData.CompanyCode + "' and PM_PRODUCT_TYPE='SNGPK'";
            UtilityLibrary.PopulateControl(cmbProducts, objSQLDB.ExecuteDataSet(sqlQry).Tables[0].DefaultView, 1, 0, "-- Please Select --", 0);
            objSQLDB = null;
        }

        private void btnSaleProduct_Click(object sender, EventArgs e)
        {
            ProductSearchAll PSearch = new ProductSearchAll("FreeProducts1");
            PSearch.objSaleProducts = this;
            PSearch.ShowDialog();
        }

        private void btnFreeProduct_Click(object sender, EventArgs e)
        {
            ProductSearchAll PSearch = new ProductSearchAll("FreeProducts2");
            PSearch.objFreeProducts = this;
            PSearch.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtOfferNo.Text = "";
            txtDescription.Text = "";
            dtFromdate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            txtToDate.Text = Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy");
            cmbProducts.SelectedIndex = 0;
            txtQty.Text = "";
            txtMinQty.Text = "";
            gvFreeProduct.Rows.Clear();
            objSQLDB = new SQLDB();
            string strSql = "SELECT ISNULL(MAX(CAST(FPM_OFFER_NUMBER AS NUMERIC)),0)+1 FROM FREE_PRODUCT_MAS WHERE FPM_BRANCH_CODE='" + cbBranch.SelectedValue + "' AND FPM_COMPANY_CODE='" + cbCompany.SelectedValue + "' AND FPM_FIN_YEAR='" + CommonData.FinancialYear + "'";
            txtOfferNo.Text = objSQLDB.ExecuteDataSet(strSql).Tables[0].Rows[0][0].ToString();
            objSQLDB = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlInsert = "", sqlSale = "";
                int iretVal = 0;
                objSQLDB = new SQLDB();
                if (CheckData() == true)
                {
                    if (chkActive.Checked == false)
                    {
                        if (lblOfferID.Text == "1")
                        {
                            sqlInsert = " UPDATE FREE_PRODUCT_MAS SET FPM_OFFER_STATUS='CLOSED',FPM_OFFER_VALID_TO='" + Convert.ToDateTime(CommonData.CurrentDate) + "' WHERE FPM_COMPANY_CODE='" +
                                cbCompany.SelectedValue + "' AND FPM_BRANCH_CODE='" + cbBranch.SelectedValue + "' AND FPM_FIN_YEAR='" + CommonData.FinancialYear + "' and FPM_OFFER_NUMBER=" + txtOfferNo.Text;
                            iretVal = objSQLDB.ExecuteSaveData(sqlInsert);
                            MessageBox.Show("Offier is Closed", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnCancel_Click(null, null);
                            return;
                        }
                    }

                    if (lblOfferID.Text == "0")
                    {
                        sqlInsert = "INSERT INTO FREE_PRODUCT_MAS(FPM_COMPANY_CODE,FPM_STATE_CODE,FPM_BRANCH_CODE,FPM_FIN_YEAR,FPM_OFFER_STATUS,FPM_OFFER_VALID_FROM," +
                            "FPM_OFFER_NUMBER,FPM_OFFER_DESCRIPTION,FPM_CREATED_BY,FPM_AUTHORIZED_BY,FPM_CREATED_DATE)" +
                            "SELECT '" + cbCompany.SelectedValue + "','" + CommonData.StateCode + "','" + cbBranch.SelectedValue + "','" + CommonData.FinancialYear + "','RUNNING','" + Convert.ToDateTime(dtFromdate.Value).ToString("dd/MMM/yyyy") + "'," +
                            txtOfferNo.Text + ",'" + txtDescription.Text + "','" + CommonData.LogUserId + "','" + CommonData.LogUserId + "', '" + CommonData.CurrentDate + "'";
                        iretVal = objSQLDB.ExecuteSaveData(sqlInsert);
                    }
                    else
                    {
                        string delete = " DELETE FROM FREE_PRODUCT_MAS_OFFER WHERE FPMO_OFFER_NUMBER=" + txtOfferNo.Text +
                            " AND FPMO_COMPANY_CODE='" + cbCompany.SelectedValue + "' AND FPMO_BRANCH_CODE='" + cbBranch.SelectedValue + "' AND FPMO_FIN_YEAR='" + CommonData.FinancialYear + "'";
                        delete += " DELETE FROM FREE_PRODUCT_MAS_SOLD WHERE FPMS_OFFER_NUMBER=" + txtOfferNo.Text +
                            " AND FPMS_COMPANY_CODE='" + cbCompany.SelectedValue + "' AND FPMS_BRANCH_CODE='" + cbBranch.SelectedValue + "' AND FPMS_FIN_YEAR='" + CommonData.FinancialYear + "'";

                        sqlInsert = " UPDATE FREE_PRODUCT_MAS SET FPM_OFFER_DESCRIPTION='" + txtDescription.Text + "',FPM_OFFER_VALID_FROM='" + Convert.ToDateTime(dtFromdate.Value).ToString("dd/MMM/yyyy") + "' WHERE FPM_OFFER_NUMBER=" + txtOfferNo.Text +
                            " AND FPM_COMPANY_CODE='" + cbCompany.SelectedValue + "' AND FPM_BRANCH_CODE='" + cbBranch.SelectedValue + "' AND FPM_FIN_YEAR='" + CommonData.FinancialYear + "'";
                        iretVal = objSQLDB.ExecuteSaveData(sqlInsert + delete);
                    }
                    if (iretVal > 0)
                    {
                        sqlSale += " INSERT INTO FREE_PRODUCT_MAS_SOLD(FPMS_COMPANY_CODE,FPMS_STATE_CODE,FPMS_BRANCH_CODE,FPMS_FIN_YEAR,FPMS_OFFER_NUMBER,FPMS_SOLD_PRODUCT_ID,FPMS_SOLD_UNITS,FPMS_MIN_SOLD_UNITS,FPMS_MAX_SOLD_UNITS)" +
                            "SELECT '" + cbCompany.SelectedValue + "','" + CommonData.StateCode + "','" + cbBranch.SelectedValue + "','" + CommonData.FinancialYear + "'," + txtOfferNo.Text + ",'" + cmbProducts.SelectedValue.ToString() + "','" + txtQty.Text + "','" + txtMinQty.Text + "','" + txtMaxQty.Text + "'";

                        foreach (DataGridViewRow dr in gvFreeProduct.Rows)
                        {
                            sqlSale += " INSERT INTO FREE_PRODUCT_MAS_OFFER(FPMO_COMPANY_CODE,FPMO_STATE_CODE,FPMO_BRANCH_CODE,FPMO_FIN_YEAR,FPMO_OFFER_NUMBER,FPMO_FREE_PRODUCT_ID,FPMO_FREE_UNITS)" +
                                "SELECT '" + cbCompany.SelectedValue + "','" + CommonData.StateCode + "','" + cbBranch.SelectedValue + "','" + CommonData.FinancialYear + "'," + txtOfferNo.Text + ",'" + dr.Cells["fPRODUCTID"].Value + "','" + dr.Cells["fUnits"].Value + "'";
                        }
                        int iretFree = objSQLDB.ExecuteSaveData(sqlSale);
                        MessageBox.Show("Data Saved successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    btnCancel_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLDB = null;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string strDel = "DELETE FROM FREE_PRODUCT_MAS_OFFER WHERE FPMO_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "' AND FPMO_BRANCH_CODE='" + cbBranch.SelectedValue.ToString() + "' AND FPMO_FIN_YEAR='" + CommonData.FinancialYear + "' AND FPMO_OFFER_NUMBER=" + txtOfferNo.Text;
                strDel += " DELETE FROM FREE_PRODUCT_MAS_SOLD WHERE FPMS_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "' AND FPMS_BRANCH_CODE='" + cbBranch.SelectedValue.ToString() + "' AND FPMS_FIN_YEAR='" + CommonData.FinancialYear + "' AND FPMS_OFFER_NUMBER=" + txtOfferNo.Text;
                strDel += " DELETE FROM FREE_PRODUCT_MAS WHERE FPM_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "' AND FPM_BRANCH_CODE='" + cbBranch.SelectedValue.ToString() + "' AND FPM_FIN_YEAR='" + CommonData.FinancialYear + "' AND FPM_OFFER_NUMBER=" + txtOfferNo.Text;
                objSQLDB = new SQLDB();
                int iretFree = objSQLDB.ExecuteSaveData(strDel);
                objSQLDB = null;
                MessageBox.Show("Data Deleted successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCancel_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CheckData()
        {
            bool blInvDtl = false;

            if (Convert.ToString(txtOfferNo.Text).Length == 0)
            {
                MessageBox.Show("Enter Free Offer number!", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blInvDtl = false;
                txtOfferNo.Focus();
                return blInvDtl;
            }
            for (int i = 0; i < gvFreeProduct.Rows.Count; i++)
            {
                if (Convert.ToString(gvFreeProduct.Rows[i].Cells["fUnits"].Value) != "")
                {
                    blInvDtl = true;
                    break;
                }
            }
            if (blInvDtl == false)
            {
                MessageBox.Show("Enter product details", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return blInvDtl;
        }

        private void txtOfferNo_Validated(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Convert.ToString(txtOfferNo.Text + "0")) > 0)
            {
                int intOfferNo = Convert.ToInt32(Convert.ToString(txtOfferNo.Text));
                btnCancel_Click(null, null);
                txtOfferNo.Text = intOfferNo.ToString();
                GetFreeOfferProducts(Convert.ToInt32(txtOfferNo.Text));
            }
            else
            {
                objSQLDB = new SQLDB();
                string strSql = "SELECT ISNULL(MAX(FPM_OFFER_NUMBER),0)+1 FROM FREE_PRODUCT_MAS WHERE FPM_BRANCH_CODE='" + cbBranch.SelectedValue + "' AND FPM_COMPANY_CODE='" + cbCompany.SelectedValue + "' AND FPM_FIN_YEAR='" + CommonData.FinancialYear + "'";
                txtOfferNo.Text = objSQLDB.ExecuteDataSet(strSql).Tables[0].Rows[0][0].ToString();
                objSQLDB = null;
                lblOfferID.Text = "0";
            }
        }
        public void GetFreeOfferProducts(int iOfficeNo)
        {
            if (cbBranch.SelectedValue != null)
            {
                oServiceDB = new ServiceDB();
                DataSet ds = oServiceDB.GetFreeOfferProducts(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString(), CommonData.FinancialYear, iOfficeNo);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblOfferID.Text = "1";
                    btnDelete.Enabled = true;
                    txtOfferNo.Text = ds.Tables[0].Rows[0]["FPM_OFFER_NUMBER"].ToString();
                    txtDescription.Text = ds.Tables[0].Rows[0]["FPM_OFFER_DESCRIPTION"].ToString();
                    string status = ds.Tables[0].Rows[0]["FPM_OFFER_STATUS"].ToString();
                    if (status.ToUpper() == "RUNNING")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;
                    dtFromdate.Value = ds.Tables[0].Rows[0]["FPM_OFFER_VALID_FROM"].ToString() == "" ? Convert.ToDateTime(CommonData.CurrentDate) : Convert.ToDateTime(ds.Tables[0].Rows[0]["FPM_OFFER_VALID_FROM"]);
                    txtToDate.Text = ds.Tables[0].Rows[0]["FPM_OFFER_VALID_TO"].ToString() == "" ? Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy") : Convert.ToDateTime(ds.Tables[0].Rows[0]["FPM_OFFER_VALID_TO"]).ToString();
                    if (ds.Tables[0].Rows[0]["FPM_OFFER_STATUS"].ToString() == "RUNNING")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;
                }
                else
                {
                    btnDelete.Enabled = false;
                    chkActive.Checked = true;
                    lblOfferID.Text = "0";
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    cmbProducts.SelectedValue = ds.Tables[1].Rows[0]["ProductID"].ToString();
                    txtQty.Text = ds.Tables[1].Rows[0]["FPMS_SOLD_UNITS"].ToString();
                    txtMinQty.Text = ds.Tables[1].Rows[0]["MinUnits"].ToString();
                    txtMaxQty.Text = ds.Tables[1].Rows[0]["MaxUnits"].ToString();
                }
                else
                {
                    cmbProducts.SelectedIndex = 0;
                    txtQty.Text = "";
                    txtMinQty.Text = "";
                    txtMaxQty.Text = "";
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    dtFreeProducts(ds.Tables[2]);
                }
            }
        }

        public void dtFreeProducts(DataTable dt)
        {
            try
            {
                int intRow = 1;
                gvFreeProduct.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    tempRow.Cells.Add(cellSLNO);

                    DataGridViewCell cellMainProductID = new DataGridViewTextBoxCell();
                    cellMainProductID.Value = dt.Rows[i]["ProductID"];
                    tempRow.Cells.Add(cellMainProductID);

                    DataGridViewCell cellMainProduct = new DataGridViewTextBoxCell();
                    cellMainProduct.Value = dt.Rows[i]["ProductName"];
                    tempRow.Cells.Add(cellMainProduct);

                    DataGridViewCell cellCategory = new DataGridViewTextBoxCell();
                    cellCategory.Value = dt.Rows[i]["category_name"];
                    tempRow.Cells.Add(cellCategory);

                    DataGridViewCell cellUnits = new DataGridViewTextBoxCell();
                    cellUnits.Value = dt.Rows[i]["FPMO_FREE_UNITS"];
                    tempRow.Cells.Add(cellUnits);
                    intRow = intRow + 1;
                    gvFreeProduct.Rows.Add(tempRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dt = null;
            }
        }

        private void txtOfferNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnClearFree_Click(object sender, EventArgs e)
        {
            gvFreeProduct.Rows.Clear();
        }
        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                objHRInfo = new HRInfo();
                UtilityLibrary.PopulateControl(cbBranch, objHRInfo.GetAllBranchList(cbCompany.SelectedValue.ToString(), "BR", "").Tables[0].DefaultView, 1, 0, "-- Please Select --", 0);
                objHRInfo = null;
            }
        }

        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranch.SelectedIndex > -1)
            {
                objSQLDB = new SQLDB();
                string strSql = "SELECT ISNULL(MAX(CAST(FPM_OFFER_NUMBER AS NUMERIC)),0)+1 FROM FREE_PRODUCT_MAS WHERE FPM_BRANCH_CODE='" + cbBranch.SelectedValue + "' AND FPM_COMPANY_CODE='" + cbCompany.SelectedValue + "' AND FPM_FIN_YEAR='" + CommonData.FinancialYear + "'";
                txtOfferNo.Text = objSQLDB.ExecuteDataSet(strSql).Tables[0].Rows[0][0].ToString();
                objSQLDB = null;
                btnCancel_Click(null, null);
                btnDelete.Enabled = false;
                lblOfferID.Text = "0";
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
