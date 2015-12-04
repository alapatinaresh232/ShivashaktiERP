using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRM;
using SSTrans;
using SSCRM.App_Code;
using SSCRMDB;
namespace SSCRM
{
    public partial class ProductInterval : Form
    {
        SQLDB objSQLDB;
        ServiceDB oServiceDB;
        Security objSecurity;
        HRInfo objHRInfo;
        DataTable dt = new DataTable();
        public ProductInterval()
        {
            InitializeComponent();
        }

        private void ProductInterval_Load(object sender, EventArgs e)
        {
            dtFromdate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            dtTodate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            objSecurity = new Security();
            UtilityLibrary.PopulateControl(cbCompany, objSecurity.GetCompanyDataSet().Tables[0].DefaultView, 1, 0, "-- Please Select --", 0);
            objSecurity = null;
            lblCheckID.Text = "0";
            btnDelete.Enabled = false;
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("FrmQty");
                dt.Columns.Add("ToQty");
                dt.Columns.Add("Rate");
                dt.Columns.Add("Points");
            }
            if (CommonData.LogUserId.ToUpper() != "ADMIN")
            {
                cbCompany.SelectedValue = CommonData.CompanyCode;
                cbCompany.Enabled = false;
                cbBranch.SelectedValue = CommonData.BranchCode;
                cbBranch.Enabled = false;
            }
            else
            {
                cbCompany.SelectedValue = CommonData.CompanyCode;
                cbBranch.SelectedValue = CommonData.BranchCode;
            }
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                objHRInfo = new HRInfo();
                UtilityLibrary.PopulateControl(cbBranch, objHRInfo.GetAllBranchList(cbCompany.SelectedValue.ToString(), "BR", "").Tables[0].DefaultView, 1, 0, "--Please Select--", 0);
                objHRInfo = null;
                objSQLDB = new SQLDB();
                string sqlQry = "SELECT PM_PRODUCT_ID,PM_PRODUCT_NAME from dbo.PRODUCT_MAS a inner join product_mas_company b on a.pm_product_id=b.pmc_product_id where pmc_product_company='" + cbCompany.SelectedValue.ToString() + "'";
                UtilityLibrary.PopulateControl(cmbProducts, objSQLDB.ExecuteDataSet(sqlQry).Tables[0].DefaultView, 1, 0, "-- Please Select --", 0);
                objSQLDB = null;
            }
        }

        public void GetGridBind(DataTable dt)
        {
            try
            {
                int intRow = 1;
                gvProductDetails.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    //DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    //cellSLNO.Value = intRow;
                    //tempRow.Cells.Add(cellSLNO);

                    DataGridViewCell cellFrmQty = new DataGridViewTextBoxCell();
                    cellFrmQty.Value = dt.Rows[i]["FrmQty"];
                    tempRow.Cells.Add(cellFrmQty);

                    DataGridViewCell cellToQty = new DataGridViewTextBoxCell();
                    cellToQty.Value = dt.Rows[i]["ToQty"];
                    tempRow.Cells.Add(cellToQty);

                    DataGridViewCell cellRate = new DataGridViewTextBoxCell();
                    cellRate.Value = dt.Rows[i]["Rate"];
                    tempRow.Cells.Add(cellRate);

                    DataGridViewCell cellPoints = new DataGridViewTextBoxCell();
                    cellPoints.Value = dt.Rows[i]["Points"];
                    tempRow.Cells.Add(cellPoints);
                    intRow = intRow + 1;
                    gvProductDetails.Rows.Add(tempRow);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckData() == true)
                {

                    string strInsert = "";
                    int iRetVal = 0, i = 0;
                    objSQLDB = new SQLDB();
                    if (chkActive.Checked == false)
                    {
                        if (lblCheckID.Text != "0")
                        {
                            strInsert = "UPDATE PRODUCT_RATE_RANGE_MAS SET PRRM_VALID_STATUS='CLOSED' WHERE PRRM_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "' AND PRRM_BRANCH_CODE='" + cbBranch.SelectedValue.ToString() + "' AND PRRM_FIN_YEAR='" + CommonData.FinancialYear + "' AND PRRM_PRODUCT_ID='" + cmbProducts.SelectedValue.ToString() + "'";
                            iRetVal = objSQLDB.ExecuteSaveData(strInsert);
                            if (iRetVal > 0)
                            {
                                MessageBox.Show("Rate Range Closed successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            btnCancel_Click(null, null);
                            return;
                        }
                    }
                    if (lblCheckID.Text == "0")
                    {
                        lblCheckID.Text = objSQLDB.ExecuteDataSet("SELECT ISNULL(MAX(PRRM_VALID_TABLE_NUMBER),0)+1 FROM PRODUCT_RATE_RANGE_MAS WHERE PRRM_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "' AND PRRM_BRANCH_CODE='" + cbBranch.SelectedValue.ToString() + "' AND PRRM_FIN_YEAR='" + CommonData.FinancialYear + "'").Tables[0].Rows[0][0].ToString();
                    }
                    else 
                    {
                        strInsert += " DELETE FROM PRODUCT_RATE_RANGE_MAS WHERE PRRM_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "' AND PRRM_BRANCH_CODE='" + cbBranch.SelectedValue.ToString() + "'AND PRRM_FIN_YEAR='" + CommonData.FinancialYear + "' AND PRRM_PRODUCT_ID='" + cmbProducts.SelectedValue.ToString() + "'AND PRRM_VALID_TABLE_NUMBER=" + lblCheckID.Text;
                    }
                    foreach (DataGridViewRow row in gvProductDetails.Rows)
                    {
                        i++;
                        if ((row.Cells[1].Value != null) && (row.Cells[1].Value.ToString() != ""))
                        {
                            strInsert += " INSERT INTO PRODUCT_RATE_RANGE_MAS (PRRM_COMPANY_CODE,PRRM_STATE_CODE,PRRM_BRANCH_CODE,PRRM_FIN_YEAR,PRRM_PRODUCT_ID,PRRM_VALID_TABLE_NUMBER," +
                                "PRRM_VALID_STATUS,PRRM_FROM_DATE,PRRM_TO_DATE,PRRM_SL_NO,PRRM_FROM_QTY_RANGE,PRRM_TO_QTY_RANGE,PRRM_RATE,PRRM_PRODUCT_POINTS,PRRM_AUTHORIZED_BY,PRRM_CREATED_BY,PRRM_CREATED_DATE,PRRM_LAST_MODIFIED_BY,PRRM_LAST_MODIFIED_DATE)" +
                                " SELECT '" + cbCompany.SelectedValue.ToString() + "','" + CommonData.StateCode + "','" + cbBranch.SelectedValue.ToString() + "','" +
                                CommonData.FinancialYear + "','" + cmbProducts.SelectedValue.ToString() + "','" + lblCheckID.Text + "','RUNNING','" + dtFromdate.Value.ToString("dd/MMM/yyyy") + "','" + dtTodate.Value.ToString("dd/MMM/yyyy") +
                                "'," + i + "," + row.Cells[0].Value + "," + row.Cells[1].Value + "," + row.Cells[2].Value + "," + row.Cells[3].Value + ",'" + CommonData.LogUserId + "','" + CommonData.LogUserId + "','" + CommonData.CurrentDate + "','" + CommonData.LogUserId + "','" + CommonData.CurrentDate + "'";
                        }
                    }
                    if (strInsert != "")
                    {
                        iRetVal = objSQLDB.ExecuteSaveData(strInsert);
                        if (iRetVal > 0)
                        {
                            MessageBox.Show("Data Saved successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
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

        public bool CheckData()
        {
            bool blInvDtl = true;
            if (cbCompany.SelectedIndex == 0)
            {
                MessageBox.Show("Select Company!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blInvDtl = false;
                cbCompany.Focus();
                return blInvDtl;
            }
            if (cbBranch.SelectedIndex == 0)
            {
                MessageBox.Show("Select Branch!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blInvDtl = false;
                cbBranch.Focus();
                return blInvDtl;
            }
            if (cmbProducts.SelectedIndex == 0)
            {
                MessageBox.Show("Select Products!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blInvDtl = false;
                cmbProducts.Focus();
                return blInvDtl;
            }
            if (Convert.ToDateTime(Convert.ToDateTime(dtFromdate.Value).ToString()) > Convert.ToDateTime(Convert.ToDateTime(dtTodate.Value).ToString()))
            {
                MessageBox.Show("To date should be less than From date.", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blInvDtl = false;
                dtFromdate.Focus();
                return blInvDtl;
            }
            //for (int i = 0; i < gvProductDetails.Rows.Count; i++)
            //{
            //    if (Convert.ToString(gvProductDetails.Rows[i].Cells[0].Value) == "")
            //    {
            //        blInvDtl = false;
            //        break;
            //    }
            //}
            if (gvProductDetails.Rows.Count == 0)
            {
                MessageBox.Show("Enter Product Details!", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blInvDtl = false;
            }
            return blInvDtl;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbCompany.SelectedIndex = 0;
            cbCompany_SelectedIndexChanged(null, null);
            cbBranch.SelectedIndex = 0;
            cmbProducts.SelectedIndex = 0;
            dtFromdate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            dtTodate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            dt.Rows.Clear();
            GetGridBind(dt);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            string strInsert = " DELETE FROM PRODUCT_RATE_RANGE_MAS WHERE PRRM_COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + "' AND PRRM_BRANCH_CODE='" + cbBranch.SelectedValue.ToString() + "' AND PRRM_FIN_YEAR='" + CommonData.FinancialYear + "' AND PRRM_PRODUCT_ID='" + cmbProducts.SelectedValue.ToString() + "' AND PRRM_VALID_TABLE_NUMBER=" + lblCheckID.Text;
            int iRetVal = objSQLDB.ExecuteSaveData(strInsert);
            if (iRetVal > 0)
            {
                MessageBox.Show("Data Deleted successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            objSQLDB = null;
            btnCancel_Click(null, null);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedIndex > 0)
            {
                dt.Rows.Clear();
                oServiceDB = new ServiceDB();
                DataSet ds = oServiceDB.GetProductRateRange(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString(), CommonData.FinancialYear, cmbProducts.SelectedValue.ToString());
                oServiceDB = null;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtFromdate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["PRRM_FROM_DATE"]);
                    dtTodate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["PRRM_TO_DATE"]);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dt.Rows.Add(new Object[] { dr["PRRM_FROM_QTY_RANGE"].ToString(), dr["PRRM_TO_QTY_RANGE"].ToString(), dr["PRRM_RATE"].ToString(), dr["PRRM_PRODUCT_POINTS"].ToString() });
                    }
                    lblCheckID.Text = ds.Tables[0].Rows[0]["PRRM_VALID_TABLE_NUMBER"].ToString();
                    if (ds.Tables[0].Rows[0]["PRRM_VALID_STATUS"].ToString() == "RUNNING")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;
                    btnDelete.Enabled = true;
                }
                else
                {
                    btnDelete.Enabled = false;
                    lblCheckID.Text = "0";
                    dt.Rows.Add(new Object[] { 1, "", "", "" });
                    GetGridBind(dt);
                }
                GetGridBind(dt);
            }
            else
            {
                dt.Rows.Clear();
                GetGridBind(dt);
            }
        }


        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                int iRVal = gvProductDetails.Rows.Count;
                string From = Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["From"].Value);
                string To = Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["To"].Value);
                string Rate = Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value);
                string Points = Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value);
                if (UtilityFunctions.IsNumeric(Points) == false)
                {
                    gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value = string.Empty;
                    return;
                }

                if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["To"].Value) != "")
                {
                    if (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["To"].Value) <= Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["From"].Value))
                    {
                        gvProductDetails.Rows[e.RowIndex].Cells["To"].Value = string.Empty;
                    }
                    else
                    {
                        CreateTable();
                        if ((To != "") || (Rate != "") || (Points != ""))
                        {
                            if (e.RowIndex == gvProductDetails.Rows.Count - 1)
                            {
                                DataGridViewRow dgwr = new DataGridViewRow();
                                gvProductDetails.Rows.Insert(e.RowIndex + 1, dgwr);
                                if(UtilityFunctions.IsWholeNumeric(To) == false)
                                    gvProductDetails.Rows[e.RowIndex + 1].Cells["From"].Value = Convert.ToDouble(To) + 0.1;
                                else
                                    gvProductDetails.Rows[e.RowIndex + 1].Cells["From"].Value = Convert.ToDouble(To) + 1;
                                dt.Rows.Add(new Object[] { From, To, Rate, Points });
                            }
                        }
                    }
                }
            }
            if (e.ColumnIndex == 1)
            {
                string sTo = Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["To"].Value);
                if (UtilityFunctions.IsWholeNumeric(sTo) == false)
                {
                    if (UtilityFunctions.IsDecimalNumeric(sTo) == false)
                    {
                        gvProductDetails.Rows[e.RowIndex].Cells["To"].Value = string.Empty;
                        return;
                    }
                }
                if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["To"].Value) != "")
                {
                    if (dt.Rows.Count - 1 > e.RowIndex)
                    {
                        double To = Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["To"].Value);
                        RemoveRows(e.RowIndex, To);
                    }
                    if (Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["To"].Value) <= Convert.ToDouble(gvProductDetails.Rows[e.RowIndex].Cells["From"].Value))
                        gvProductDetails.Rows[e.RowIndex].Cells["To"].Value = string.Empty;

                    if (gvProductDetails.Rows[e.RowIndex].Cells["From"].Value == null)
                        gvProductDetails.Rows[e.RowIndex].Cells["To"].Value = string.Empty;
                }
                if (Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value) != "")
                    gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value = string.Empty;
            }
            if (e.ColumnIndex == 2)
            {
                string sTo = Convert.ToString(gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value);
                if (UtilityFunctions.IsNumeric(sTo) == false)
                {
                    gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value = string.Empty;
                    return;
                }
            }
        }
        public void CreateTable()
        {
            dt.Rows.Clear();
            foreach (DataGridViewRow row in gvProductDetails.Rows)
            {
                if (row.Cells[0].Value != null)
                    dt.Rows.Add(new Object[] { row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value });
            }
        }
        public void RemoveRows(int iVal, double To)
        {
            DataTable dts;
            dts = dt.Copy();
            int i = 0;
            dts.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                if (i == iVal)
                    dts.Rows.Add(new Object[] { dr[0], To, dr[2], dr[3] });
                if (i < iVal)
                    dts.Rows.Add(new Object[] { dr[0], dr[1], dr[2], dr[3] });
                i++;
            }
            dts.AcceptChanges();
            GetGridBind(dts);
        }

        private void cmbProducts_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbProducts.SelectedIndex > 0)
            {
                dt.Rows.Clear();
                oServiceDB = new ServiceDB();
                DataSet ds = oServiceDB.GetProductRateRange(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString(), CommonData.FinancialYear, cmbProducts.SelectedValue.ToString());
                oServiceDB = null;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtFromdate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["PRRM_FROM_DATE"]);
                    dtTodate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["PRRM_TO_DATE"]);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dt.Rows.Add(new Object[] { dr["PRRM_FROM_QTY_RANGE"].ToString(), dr["PRRM_TO_QTY_RANGE"].ToString(), dr["PRRM_RATE"].ToString(), dr["PRRM_PRODUCT_POINTS"].ToString() });
                    }
                    lblCheckID.Text = ds.Tables[0].Rows[0]["PRRM_VALID_TABLE_NUMBER"].ToString();
                    if (ds.Tables[0].Rows[0]["PRRM_VALID_STATUS"].ToString() == "RUNNING")
                        chkActive.Checked = true;
                    else
                        chkActive.Checked = false;
                    btnDelete.Enabled = true;
                }
                else
                {
                    btnDelete.Enabled = false;
                    lblCheckID.Text = "0";
                    dt.Rows.Add(new Object[] { 1, "", "", "" });
                    GetGridBind(dt);
                }
                GetGridBind(dt);
            }
            else
            {
                dt.Rows.Clear();
                GetGridBind(dt);
            }
        }
    }
}
