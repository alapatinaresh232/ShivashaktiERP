using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SSCRMDB;
using SSCRM;

namespace SSCRM
{
    public partial class ProductMasterCompany : Form
    {
        private SQLDB objSQLData = null;
                           

        public ProductMasterCompany()
        {
            InitializeComponent();
        }

        
        
        private void FillCompanyData()
        {
            objSQLData = new SQLDB();
            DataTable dt=new DataTable();
            try
            {
                string strCommand = "select CM_COMPANY_NAME,CM_COMPANY_CODE from COMPANY_MAS";
                dt = objSQLData.ExecuteDataSet(strCommand).Tables[0];

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
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLData = null;
                dt = null;
            }
                      
        }

              
        private void ProductMasterCompany_Load(object sender, EventArgs e)
        {
            gvProductDetails.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9,
                                                        System.Drawing.FontStyle.Regular);
            FillCompanyData();
           
        }

        private void FillCompanyProductDataToGrid()
        {
            objSQLData = new SQLDB();
            DataTable dt = new DataTable(); ;
            int intRow = 1;
            this.gvProductDetails.Rows.Clear();
            try
            {
                if (cbCompany.SelectedIndex > 0)
                {
                    string strCommand = "SELECT CM.CATEGORY_NAME,CM.CATEGORY_ID, PMC.pmc_product_id,PM.PM_PRODUCT_NAME from product_mas_company AS PMC INNER JOIN PRODUCT_MAS as PM ON PMC.pmc_product_id=PM.PM_PRODUCT_ID INNER JOIN CATEGORY_MASTER as CM ON  PM.PM_CATEGORY_ID=CM.CATEGORY_ID WHERE pmc_product_company='" + cbCompany.SelectedValue.ToString() + "'";
                    dt = objSQLData.ExecuteDataSet(strCommand).Tables[0];
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow tempRow = new DataGridViewRow();
                    DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                    cellSLNO.Value = intRow;
                    intRow = intRow + 1;
                    tempRow.Cells.Add(cellSLNO);

                    DataGridViewCell cellCategoryName = new DataGridViewTextBoxCell();
                    cellCategoryName.Value = dt.Rows[i]["CATEGORY_NAME"];
                    tempRow.Cells.Add(cellCategoryName);

                    DataGridViewCell cellProductName = new DataGridViewTextBoxCell();
                    cellProductName.Value = dt.Rows[i]["PM_PRODUCT_NAME"];
                    tempRow.Cells.Add(cellProductName);

                    DataGridViewCell cellProductId = new DataGridViewTextBoxCell();
                    cellProductId.Value = dt.Rows[i]["pmc_product_id"];
                    tempRow.Cells.Add(cellProductId);

                    DataGridViewCell cellCategoryId = new DataGridViewTextBoxCell();
                    cellCategoryId.Value = dt.Rows[i]["CATEGORY_ID"];
                    tempRow.Cells.Add(cellCategoryId);


                    gvProductDetails.Rows.Add(tempRow);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }

            finally
            {
                objSQLData = null;
                dt = null;
            }
        }
        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCompanyProductDataToGrid();            
        }


        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                ProductsAdd Products = new ProductsAdd(cbCompany.SelectedValue.ToString());
                Products.objProductMasterCompany = this;
                Products.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                objSQLData = new SQLDB();
                //DataTable dt = new DataTable();
                string strCommand = string.Empty;

                int iRes = 0;
                int iRes1 = 0;
                try
                {

                    strCommand = "DELETE FROM product_mas_company where pmc_product_company='" + cbCompany.SelectedValue.ToString() + "' ";
                    iRes = objSQLData.ExecuteSaveData(strCommand);

                    for (int i = 0; i < gvProductDetails.Rows.Count; i++)
                    {
                        strCommand += "INSERT INTO product_mas_company(pmc_product_id,pmc_product_company)VALUES('" + gvProductDetails.Rows[i].Cells["ProductId"].Value + "','" + cbCompany.SelectedValue.ToString() + "')";

                    }

                    iRes1 = objSQLData.ExecuteSaveData(strCommand);


                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLData = null;
                }
                if (iRes1 > 0)
                {
                    MessageBox.Show("Data Saved Successfully", "Product Master Company", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (cbCompany.SelectedIndex > 0)
                    {
                        FillCompanyProductDataToGrid();
                    }

                }
                else
                {
                    MessageBox.Show("Data Not Saved", "Product Master Company", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            

        }

        private void gvProductDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.ColumnIndex ==5)
            {
                DialogResult dlgResult = MessageBox.Show("Do you want Delete this Record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                   
                        string ProductId = gvProductDetails.Rows[e.RowIndex].Cells[gvProductDetails.Columns["ProductId"].Index].Value.ToString();
                        DataGridViewRow dgvr = gvProductDetails.Rows[e.RowIndex];
                        gvProductDetails.Rows.Remove(dgvr);
                        MessageBox.Show("Data Deleted Successfully", "Product Master Company", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                      MessageBox.Show("Data Not Deleted", "Product Master Company", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


               
    }
}
