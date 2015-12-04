using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Diagnostics;
using System.Configuration;
using SSCRM.App_Code;
using SSCRMDB;
using SSAdmin;
using SSTrans;
namespace SSCRM
{
    public partial class InvoiceTemplateProducts : Form
    {
        private Security objData = null;
        private UtilityDB objUtil = null;
        private SQLDB objSQLdb = null;
        public InvoiceTemplateProducts()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InvoiceTemplateProducts_Load(object sender, EventArgs e)
        {
                
                Hashtable htComp = new Hashtable();

                if (CommonData.LogUserId.ToUpper() == "ADMIN")
                {
                    cbBranch.Visible = true;
                    cbCompany.Visible = true;
                    FillCompanyData();
                    cbCompany.SelectedValue = CommonData.CompanyCode;
                    FillBranchData();
                    cbBranch.SelectedValue = CommonData.BranchCode;
                    FillInvoiceTemplateProductsToGrid();

                }
                else
                {
                    cbUserBranch.Visible = true;
                    lblCompany.Visible = false;
                    FillUserBranch();
                    cbUserBranch.SelectedValue = CommonData.BranchCode;
                    FillInvoiceTemplateProductsToGrid();
                }
                gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10,
                                                            System.Drawing.FontStyle.Regular);
                
        }

        private void FillInvoiceTemplateProductsToGrid()
        {
            DataTable dt = new DataTable();
            if (CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                gvProductDetails.Rows.Clear();
                dt = GetInvTemplateProducts(cbCompany.SelectedValue.ToString(), cbBranch.SelectedValue.ToString(), CommonData.FinancialYear);
                foreach (DataRow dr in dt.Rows)
                {
                    
                    gvProductDetails.Rows.Add();
                    int intCurRow = gvProductDetails.Rows.Count - 1;
                    gvProductDetails.Rows[intCurRow].Cells["SLNO"].Value = intCurRow + 1;
                    gvProductDetails.Rows[intCurRow].Cells["ProductID"].Value = dr["PRODUCT_ID"] + "";
                    gvProductDetails.Rows[intCurRow].Cells["MainProduct"].Value = dr["PRODUCT_NAME"] + "";
                    gvProductDetails.Rows[intCurRow].Cells["Brand"].Value = dr["CATEGORY_NAME"] + "";
                    gvProductDetails.Rows[intCurRow].Cells["Qty"].Value = 1;
                    gvProductDetails.Rows[intCurRow].Cells["Rate"].Value = dr["PRICE"] + "";
                    gvProductDetails.Rows[intCurRow].Cells["Points"].Value = dr["POINTS"] + "";
                    
                }
            }
            else
            {
                if (cbUserBranch.SelectedIndex >= 0)
                {
                    gvProductDetails.Rows.Clear();
                    string[] strBranchCode = cbUserBranch.SelectedValue.ToString().Split('@');
                    dt = GetInvTemplateProducts(strBranchCode[1].ToString(), strBranchCode[0].ToString(), CommonData.FinancialYear);
                    foreach (DataRow dr in dt.Rows)
                    {

                        gvProductDetails.Rows.Add();
                        int intCurRow = gvProductDetails.Rows.Count - 1;
                        gvProductDetails.Rows[intCurRow].Cells["SLNO"].Value = intCurRow + 1;
                        gvProductDetails.Rows[intCurRow].Cells["ProductID"].Value = dr["PRODUCT_ID"] + "";
                        gvProductDetails.Rows[intCurRow].Cells["MainProduct"].Value = dr["PRODUCT_NAME"] + "";
                        gvProductDetails.Rows[intCurRow].Cells["Brand"].Value = dr["CATEGORY_NAME"] + "";
                        gvProductDetails.Rows[intCurRow].Cells["Qty"].Value = 1;
                        gvProductDetails.Rows[intCurRow].Cells["Rate"].Value = Convert.ToSingle(dr["PRICE"]).ToString() + "";
                        gvProductDetails.Rows[intCurRow].Cells["Points"].Value = dr["POINTS"] + "";

                    }
                }
            }
        }

        private DataTable GetInvTemplateProducts(string sCompany, string sBranch, string sFinYear)
        {
            objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[3];
            DataTable dt = new DataTable();
            try
            {

                param[0] = objSQLdb.CreateParameter("@sCompany", DbType.String, sCompany, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@sBranch", DbType.String, sBranch, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@sFinYear", DbType.String, sFinYear, ParameterDirection.Input);
                dt = objSQLdb.ExecuteDataSet("InvTemplateProducts_Get", CommandType.StoredProcedure, param).Tables[0];
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                param = null;
                objSQLdb = null;
            }
            return dt;
        
        }
        private void FillUserBranch()
        {
            objUtil = new UtilityDB();
            try
            {
                DataTable dtUB = objUtil.dtUserBranch(CommonData.LogUserId);
                //if (dtUB.Rows.Count == 0)
                //dtUB.Rows.Add(CommonData.BranchCode + '@' + CommonData.CompanyCode + '@' + CommonData.BranchName + '@' + CommonData.CompanyName, CommonData.BranchName);


                cbUserBranch.DisplayMember = "branch_name";
                cbUserBranch.ValueMember = "branch_Code";
                cbUserBranch.DataSource = dtUB;
                cbUserBranch.SelectedIndex = 0;
                //cbUserBranch.SelectedValue = CommonData.BranchCode;

                dtUB = null;
               
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objUtil = null;

            }

        }
        private void FillCompanyData()
        {
            DataSet ds = null;
            objData = new Security();
            try
            {
                ds = new DataSet();

                ds = objData.GetCompanyDataSet();
                DataTable dtCompany = ds.Tables[0];
                if (dtCompany.Rows.Count > 0)
                {
                    cbCompany.DisplayMember = "CM_Company_Name";
                    cbCompany.ValueMember = "CM_Company_Code";
                    cbCompany.DataSource = dtCompany;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
                ds.Dispose();
            }

        }
       

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > -1)
            {
                FillBranchData();
                
            }
        }
        private void FillBranchData()
        {
            DataSet ds = null;
            objData = new Security();
            try
            {
                ds = new DataSet();
                ds = objData.UserBranchList(cbCompany.SelectedValue.ToString());
                DataTable dtCompany = ds.Tables[0];
                DataView dv1 = dtCompany.DefaultView;
                dv1.RowFilter = " Active = 'T' ";
                DataTable dtBR = dv1.ToTable();
                if (dtBR.Rows.Count > 0)
                {
                    cbBranch.DisplayMember = "branch_name";
                    cbBranch.ValueMember = "branch_Code";
                    cbBranch.DataSource = dtBR;
                }
                dtCompany = null;
                dv1 = null;
                dtBR = null;
               
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objData = null;
                ds.Dispose();
            }

        }
        private void cbUserBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillInvoiceTemplateProductsToGrid();
        }

        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillInvoiceTemplateProductsToGrid();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            if (CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                try
                {
                    if (gvProductDetails.Rows.Count > 0)
                    {
                        string strIns = "";
                        string strDel = "DELETE FROM TEMPLATE_INV_PRODUCT WHERE TIP_Company_Code = '" + cbCompany.SelectedValue.ToString() + "' AND TIP_BRANCH_CODE = '" + cbBranch.SelectedValue.ToString() + "' AND TIP_Fin_Year = '" + CommonData.FinancialYear + "'";
                        int iretval = objSQLdb.ExecuteSaveData(strDel);
                        for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                        {
                            string Rate = gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() == "" ? "0" : gvProductDetails.Rows[i].Cells["Rate"].Value.ToString();
                            string Points = gvProductDetails.Rows[i].Cells["Points"].Value.ToString() == "" ? "0" : gvProductDetails.Rows[i].Cells["Points"].Value.ToString();
                            strIns = "INSERT INTO TEMPLATE_INV_PRODUCT (TIP_Company_Code,TIP_BRANCH_CODE,TIP_Fin_Year,TIP_SortOrder,TIP_Product_code,TIP_Rate,TIP_Product_Points) VALUES('" + cbCompany.SelectedValue.ToString() + "','" + cbBranch.SelectedValue.ToString() + "','" + CommonData.FinancialYear + "','" + gvProductDetails.Rows[i].Cells["SLNO"].Value.ToString() + "','" + gvProductDetails.Rows[i].Cells["ProductID"].Value.ToString() + "','" + Rate + "','" + Points + "');";
                            iretval = objSQLdb.ExecuteSaveData(strIns);
                        }
                        objSQLdb = null;
                        MessageBox.Show("Data saved successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No Products In the List", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    FillInvoiceTemplateProductsToGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    if (gvProductDetails.Rows.Count > 0)
                    {
                        string[] strBranchCode = cbUserBranch.SelectedValue.ToString().Split('@');
                        string strIns = "";
                        string strDel = "DELETE FROM TEMPLATE_INV_PRODUCT WHERE TIP_Company_Code = '" + strBranchCode[1].ToString() + "' AND TIP_BRANCH_CODE = '" + strBranchCode[0].ToString() + "' AND TIP_Fin_Year = '" + CommonData.FinancialYear + "'";
                        int iretval = objSQLdb.ExecuteSaveData(strDel);
                        for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                        {
                            string Rate = gvProductDetails.Rows[i].Cells["Rate"].Value.ToString() == "" ? "0" : gvProductDetails.Rows[i].Cells["Rate"].Value.ToString();
                            string Points = gvProductDetails.Rows[i].Cells["Points"].Value.ToString() == "" ? "0" : gvProductDetails.Rows[i].Cells["Points"].Value.ToString();
                            strIns = "INSERT INTO TEMPLATE_INV_PRODUCT (TIP_Company_Code,TIP_BRANCH_CODE,TIP_Fin_Year,TIP_SortOrder,TIP_Product_code,TIP_Rate,TIP_Product_Points) VALUES('" + strBranchCode[1].ToString() + "','" + strBranchCode[0].ToString() + "','" + CommonData.FinancialYear + "','" + gvProductDetails.Rows[i].Cells["SLNO"].Value.ToString() + "','" + gvProductDetails.Rows[i].Cells["ProductID"].Value.ToString() + "','" + Rate + "','" + Points + "');";
                            iretval = objSQLdb.ExecuteSaveData(strIns);
                        }
                        objSQLdb = null;
                        MessageBox.Show("Data saved successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No Products In the List", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    FillInvoiceTemplateProductsToGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClearProd_Click(object sender, EventArgs e)
        {
            gvProductDetails.Rows.Clear();
        }

        private void gvProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvProductDetails.Columns["Delete"].Index)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    string ProductId = gvProductDetails.Rows[e.RowIndex].Cells[gvProductDetails.Columns["ProductId"].Index].Value.ToString();
                    DataGridViewRow dgvr = gvProductDetails.Rows[e.RowIndex];
                    gvProductDetails.Rows.Remove(dgvr);
                    OrderSlNo();
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

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            ProductSearchAll PSearch = new ProductSearchAll("InvoiceTemplateProducts");
            PSearch.objInvoiceTemplateProducts = this;
            PSearch.ShowDialog();
        }

        private void gvProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 || e.ColumnIndex == 6)
            {
                string Rate = gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value.ToString();
                string Points = gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value.ToString();
                if (UtilityFunctions.IsNumeric(Rate) == false)
                    gvProductDetails.Rows[e.RowIndex].Cells["Rate"].Value = string.Empty;
                else if (UtilityFunctions.IsNumeric(Points) == false)
                    gvProductDetails.Rows[e.RowIndex].Cells["Points"].Value = string.Empty;
                return;
            }
            
        }

        
    }
}
