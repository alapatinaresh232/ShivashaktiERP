using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSCRM.App_Code;
namespace SSCRM
{
    public partial class AddProducts : Form
    {
        string CmpCode = "", BranchCode = "", DocMonth = "",Fin_Year="" ,Product = "", ProductDesc = "";
        public DocGLReportHeadings objServiceHeading;
        SQLDB objSQLDB;
        public AddProducts()
        {
            InitializeComponent();
        }
        public AddProducts(string CCode, string BCode, string DMonth,string sFin_year, string sProduct, string sProdDesc)
        {
            InitializeComponent();
            CmpCode = CCode;
            BranchCode = BCode;
            Fin_Year = sFin_year;
            DocMonth = DMonth;
            Product = sProduct;
            ProductDesc = sProdDesc;
        }
        private void AddProducts_Load(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            string sqlQry = "SELECT PM_PRODUCT_ID,PM_PRODUCT_NAME from dbo.PRODUCT_MAS a inner join product_mas_company b on a.pm_product_id=b.pmc_product_id where pmc_product_company='" + CmpCode + "'";
            UtilityLibrary.PopulateControl(cmbProducts, objSQLDB.ExecuteDataSet(sqlQry).Tables[0].DefaultView, 1, 0, "-- Please Select --", 0);
            objSQLDB = null;
            if (Product != "")
            {
                cmbProducts.Enabled = false;
                cmbProducts.SelectedValue = Product;
                txtProdDesc.Text = ProductDesc;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                objSQLDB = new SQLDB();
                string iMaxVal = objSQLDB.ExecuteDataSet("SELECT isnull(MAX(SYSBPH_SORTORDER),0)+1 FROM SSCRM_YTD_SALE_BULTIN_PROD_HEADINGS  WHERE SYSBPH_COMPANY_CODE='" + CmpCode + "' AND SYSBPH_BRANCH_CODE='" + BranchCode + "'  AND SYSBPH_FIN_YEAR='" + Fin_Year + "' AND SYSBPH_DOCUMENT_MONTH='" + DocMonth + "'").Tables[0].Rows[0][0].ToString();
                string iCnt = objSQLDB.ExecuteDataSet("SELECT Count(*) FROM SSCRM_YTD_SALE_BULTIN_PROD_HEADINGS WHERE SYSBPH_COMPANY_CODE='" + CmpCode + "' AND SYSBPH_BRANCH_CODE='" + BranchCode + "' AND SYSBPH_PRODUCT_ID='" + cmbProducts.SelectedValue + "' AND SYSBPH_FIN_YEAR='" + Fin_Year + "' AND SYSBPH_DOCUMENT_MONTH='" + DocMonth + "'").Tables[0].Rows[0][0].ToString();
                string sqlInsert = "";
                if (iCnt == "0")
                {
                    sqlInsert = "INSERT INTO SSCRM_YTD_SALE_BULTIN_PROD_HEADINGS (SYSBPH_COMPANY_CODE,SYSBPH_STATE_CODE,SYSBPH_BRANCH_CODE,SYSBPH_FIN_YEAR,SYSBPH_DOCUMENT_MONTH,SYSBPH_SORTORDER,SYSBPH_PRODUCT_ID,SYSBPH_PRODUCT_SHORT_HD)" +
                        "SELECT '" + CmpCode + "','" + CommonData.StateCode + "','" + BranchCode + "','" + Fin_Year + "','" + DocMonth + "'," + iMaxVal + ",'" + cmbProducts.SelectedValue + "','" + txtProdDesc.Text + "'";
                }
                else
                {
                    sqlInsert = "UPDATE SSCRM_YTD_SALE_BULTIN_PROD_HEADINGS SET SYSBPH_PRODUCT_SHORT_HD='" + txtProdDesc.Text + "' WHERE SYSBPH_PRODUCT_ID='" + cmbProducts.SelectedValue + "' AND SYSBPH_COMPANY_CODE='" + CmpCode + "' AND SYSBPH_BRANCH_CODE='" + BranchCode + "'";
                }
                int iretVal = objSQLDB.ExecuteSaveData(sqlInsert);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data not saved", "Sale Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ((DocGLReportHeadings)objServiceHeading).GetBuindData();
            this.Close();
        }
    }
}
