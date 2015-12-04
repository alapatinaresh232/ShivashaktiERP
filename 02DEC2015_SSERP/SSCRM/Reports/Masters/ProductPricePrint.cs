using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;

namespace SSCRM
{
    public partial class ProductPricePrint : Form
    {
        SQLDB objDb = null;
        ReportViewer childReportViewer=null;
        public ProductPricePrint()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void ProductPricePrint_Load(object sender, EventArgs e)
        {
            FillCompanyData();
        }

        private void FillCompanyData()
        {
            objDb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                string strCommand = "SELECT CM_COMPANY_NAME" +
                                    ",CM_COMPANY_CODE " +
                                    "FROM COMPANY_MAS " +
                                    "WHERE ACTIVE='T' ";

                dt = objDb.ExecuteDataSet(strCommand).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

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
                objDb = null;
                dt = null;
            }
        }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompany.SelectedIndex > 0)
            {
                objDb = new SQLDB();
                DataTable dt = new DataTable();
                cbBranch.DataSource = null;
                try
                {

                    string strCommand = "SELECT BRANCH_CODE+'@'+STATE_CODE+'@'+COMPANY_CODE as BranchCode"+
                                        ",BRANCH_NAME FROM BRANCH_MAS WHERE COMPANY_CODE='" + cbCompany.SelectedValue.ToString() + 
                                        "' and active='T' AND BRANCH_TYPE='BR' ORDER BY BRANCH_NAME ASC ";
                        dt = objDb.ExecuteDataSet(strCommand).Tables[0];

                        if (dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = "--Select--";
                            dr[1] = "--Select--";

                            dt.Rows.InsertAt(dr, 0);
                            cbBranch.DataSource = dt;
                            cbBranch.DisplayMember = "BRANCH_NAME";
                            cbBranch.ValueMember = "BranchCode";
                        }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDb = null;
                    dt = null;
                }
            }
        }

        private void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranch.SelectedIndex > 0)
            {
                objDb = new SQLDB();
                DataTable dt = null;
                string[] sBranchCode = cbBranch.SelectedValue.ToString().Split('@');
                string strCmd = " SELECT DISTINCT(PPM_WEF_DATE) FROM PRODUCT_PRICE_MASTER WHERE PPM_COMP_CODE='" + sBranchCode[2] + "' AND PPM_BRANCH_CODE='" + sBranchCode[0] + "'";
                try
                {
                    dt = objDb.ExecuteDataSet(strCmd).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        gvProductPrice.Rows.Clear();
                        for (int iVar = 0; iVar < dt.Rows.Count; iVar++)
                        {
                            DataGridViewRow tempRow = new DataGridViewRow();
                            DataGridViewCell cellSNo = new DataGridViewTextBoxCell();
                            cellSNo.Value = (iVar + 1).ToString();
                            tempRow.Cells.Add(cellSNo);

                            DataGridViewCell cellDate = new DataGridViewTextBoxCell();
                            cellDate.Value = Convert.ToDateTime(dt.Rows[iVar]["PPM_WEF_DATE"].ToString()).ToString("dd-MMM-yyyy").ToUpper();
                            tempRow.Cells.Add(cellDate);

                            gvProductPrice.Rows.Add(tempRow);

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objDb = null;
                    dt = null;
                }
            }
        }

        private void gvProductPrice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //if (gvProductPrice.Rows[e.RowIndex].Cells["Print"].Value.ToString().Trim() != "")
                //{
                if (Convert.ToBoolean(gvProductPrice.Rows[e.RowIndex].Cells["Print"].Selected) == true)
                {
                    string[] sBranchCode = cbBranch.SelectedValue.ToString().Split('@');
                    childReportViewer = new ReportViewer(sBranchCode[2], sBranchCode[1], sBranchCode[0], Convert.ToDateTime(gvProductPrice.Rows[e.RowIndex].Cells["WEFDate"].Value.ToString()).ToString("dd/MMM/yyyy"),"");
                    CommonData.ViewReport = "SSCRM_REP_PRODUCTPRICE";
                    childReportViewer.Show();
                }
                //}
                
            }
        }

    }
}
