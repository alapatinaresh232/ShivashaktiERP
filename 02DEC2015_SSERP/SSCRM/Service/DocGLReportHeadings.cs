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
using System.Web.UI.WebControls;
using System.Collections;
namespace SSCRM
{
    public partial class DocGLReportHeadings : Form
    {
        ServiceDB objServiceDB;
        HRInfo objHRInfo;
        Security objSecurity;
        SQLDB objSQLDB;
        DataSet dsOld, dsNew;
        public DocGLReportHeadings()
        {
            InitializeComponent();
        }
        public string DocMonth = "", Fin_Year = "";
        public DocGLReportHeadings(string DMonth, string sFinYear)
        {
            InitializeComponent();
            DocMonth = DMonth;
            Fin_Year = sFinYear;
        }

        private void ServiceHeading_Load(object sender, EventArgs e)
        {
            GetBuindData();
        }

        public void GetBuindData()
        {
            string SqlQry = "SELECT SYSBPH_SORTORDER,SYSBPH_PRODUCT_SHORT_HD,SYSBPH_PRODUCT_ID FROM SSCRM_YTD_SALE_BULTIN_PROD_HEADINGS WHERE SYSBPH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SYSBPH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SYSBPH_FIN_YEAR='" + Fin_Year + "' AND SYSBPH_DOCUMENT_MONTH='" + DocMonth + "' ORDER BY SYSBPH_SORTORDER";
            objSQLDB = new SQLDB();
            dsOld = objSQLDB.ExecuteDataSet(SqlQry);
            objSQLDB = null;
            lstOldData.Items.Clear();
            foreach (DataRow dr in dsOld.Tables[0].Rows)
            {
                lstOldData.Items.Add(dr[1].ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lstOldData.Items.Count > 0)
            {
                //string strDel = "DELETE FROM SSCRM_YTD_SALE_BULTIN_PROD_HEADINGS WHERE SYSBPH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SYSBPH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SYSBPH_DOCUMENT_MONTH='" + DocMonth + "'";
                //objSQLDB = new SQLDB();
                //int iRetDel = objSQLDB.ExecuteSaveData(strDel);
                //objSQLDB = null;
                string strSql = "";
                //if (iRetDel > 0)
                //{
                for (int i = 0; i < lstOldData.Items.Count; i++)
                {
                    DataRow[] drData = dsOld.Tables[0].Select("SYSBPH_PRODUCT_SHORT_HD='" + lstOldData.Items[i].ToString() + "'");
                    strSql += "UPDATE SSCRM_YTD_SALE_BULTIN_PROD_HEADINGS SET SYSBPH_SORTORDER=" + Convert.ToInt32(i + 1) + " WHERE SYSBPH_PRODUCT_ID='" + drData[0][2].ToString() + "' AND SYSBPH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SYSBPH_BRANCH_CODE='" + CommonData.BranchCode + "' AND SYSBPH_FIN_YEAR='" + Fin_Year + "' AND SYSBPH_DOCUMENT_MONTH='" + DocMonth + "'";
                    //strSql += " INSERT INTO SSCRM_YTD_SALE_BULTIN_PROD_HEADINGS (SYSBPH_COMPANY_CODE,SYSBPH_STATE_CODE,SYSBPH_BRANCH_CODE,SYSBPH_FIN_YEAR,SYSBPH_DOCUMENT_MONTH,SYSBPH_SORTORDER,SYSBPH_PRODUCT_ID,SYSBPH_PRODUCT_SHORT_HD)" +
                    //    "SELECT '" + CommonData.CompanyCode + "','" + CommonData.StateCode + "','" + CommonData.BranchCode + "','" + CommonData.FinancialYear + "','" + DocMonth + "'," + Convert.ToInt32(i + 1) + ",'" + drData[0][2].ToString() + "','" + lstOldData.Items[i].ToString() + "'";
                }
                objSQLDB = new SQLDB();
                int iRetVal = objSQLDB.ExecuteSaveData(strSql);
                objSQLDB = null;
                //}
            }
            GetBuindData();

        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            string sProductID = "";
            if (lstOldData.SelectedIndices.Count == 1)
            {
                DataRow[] drData = dsOld.Tables[0].Select("SYSBPH_PRODUCT_SHORT_HD='" + lstOldData.Text + "'");
                sProductID = drData[0][2].ToString();
            }
            AddProducts chldAddprocuct = new AddProducts(CommonData.CompanyCode, CommonData.BranchCode, DocMonth, Fin_Year,sProductID, lstOldData.Text);
            chldAddprocuct.objServiceHeading = this;
            chldAddprocuct.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstOldData.SelectedIndices.Count > 0)
            {
                try
                {
                    DialogResult dlgResult = MessageBox.Show("Do you want delete this record?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        string strDel = "";
                        for (int x = lstOldData.SelectedIndices.Count - 1; x >= 0; x--)
                        {
                            int idx = lstOldData.SelectedIndices[x];
                            strDel += "'" + lstOldData.Items[idx].ToString() + "',";
                        }
                        string strQry = "DELETE FROM SSCRM_YTD_SALE_BULTIN_PROD_HEADINGS WHERE  SYSBPH_COMPANY_CODE='" + CommonData.CompanyCode + "' AND SYSBPH_BRANCH_CODE='" + CommonData.BranchCode + "'  AND SYSBPH_FIN_YEAR='" + Fin_Year + "' AND SYSBPH_DOCUMENT_MONTH='" + DocMonth + "' AND SYSBPH_PRODUCT_SHORT_HD IN (" + strDel.TrimEnd(',') + ")";
                        objSQLDB = new SQLDB();
                        int iretVal = objSQLDB.ExecuteSaveData(strQry);
                        objSQLDB = null;
                        //MessageBox.Show("Data deleted successfully", "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetBuindData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            GetBuindData();
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            lstOldData.BeginUpdate();
            int numberOfSelectedItems = lstOldData.SelectedItems.Count;
            for (int i = numberOfSelectedItems - 1; i >= 0; i--)
            {
                if (lstOldData.SelectedIndices[i] < lstOldData.Items.Count - 1)
                {
                    int indexToInsertIn = lstOldData.SelectedIndices[i] + 2;
                    lstOldData.Items.Insert(indexToInsertIn, lstOldData.SelectedItems[i]);
                    lstOldData.Items.RemoveAt(indexToInsertIn - 2);
                    lstOldData.SelectedItem = lstOldData.Items[indexToInsertIn - 1];
                }
            }
            lstOldData.EndUpdate();
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            lstOldData.BeginUpdate();
            int numberOfSelectedItems = lstOldData.SelectedItems.Count;
            for (int i = 0; i < numberOfSelectedItems; i++)
            {
                if (lstOldData.SelectedIndices[i] > 0)
                {
                    int indexToInsertIn = lstOldData.SelectedIndices[i] - 1;
                    lstOldData.Items.Insert(indexToInsertIn, lstOldData.SelectedItems[i]);
                    lstOldData.Items.RemoveAt(indexToInsertIn + 2);
                    lstOldData.SelectedItem = lstOldData.Items[indexToInsertIn];
                }
            }
            lstOldData.EndUpdate();
        }
    }
}
