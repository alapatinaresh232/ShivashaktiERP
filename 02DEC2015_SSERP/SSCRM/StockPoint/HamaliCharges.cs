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
    public partial class HamaliCharges : Form
    {
        public StockPointDetails objStockPointDetl;
        SQLDB objSQLDB;
        string sStockPoint = "", sStockPointName = "", sCompanyCode = "";
        public HamaliCharges()
        {
            InitializeComponent();
        }
        public HamaliCharges(string StockPoint, string SpName, string CompanyCode)
        {
            InitializeComponent();
            sStockPoint = StockPoint;
            sCompanyCode = CompanyCode;
            sStockPointName = SpName;
        }

        private void HamaliCharges_Load(object sender, EventArgs e)
        {
            txtStockPoint.Text = sStockPointName;
            objSQLDB = new SQLDB();
            DataSet ds;
            string sqlQry = "SELECT SPHC_BRANCH_CODE,PM_PRODUCT_ID,PM_PRODUCT_NAME,PM_BRAND_ID,SPHC_VtoG,SPHC_VtoV,SPHC_GtoV FROM SP_HAMELI_CHARGES " +
                " A INNER JOIN PRODUCT_MAS B ON A.SPHC_PRODUCT_ID=B.PM_PRODUCT_ID WHERE SPHC_BRANCH_CODE='" + sStockPoint + "'";
            DataSet dsExists = objSQLDB.ExecuteDataSet(sqlQry);
            if (dsExists.Tables[0].Rows.Count > 0)
            {
                GetHamali(dsExists.Tables[0], 0);
            }
            else
            {
                sqlQry = "SELECT PMC_PRODUCT_COMPANY,PM_PRODUCT_ID,PM_PRODUCT_NAME,PM_BRAND_ID FROM PRODUCT_MAS A " +
                    "INNER JOIN PRODUCT_MAS_COMPANY B ON A.PM_PRODUCT_ID=B.PMC_PRODUCT_ID WHERE PM_PRODUCT_TYPE='SNGPK' AND PMC_PRODUCT_COMPANY='" + sCompanyCode + "' ORDER BY PM_BRAND_ID";
                ds = objSQLDB.ExecuteDataSet(sqlQry);
                GetHamali(ds.Tables[0], 1);
            }
            objSQLDB = null;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void GetHamali(DataTable Dt, int Type)
        {
            int intRow = 1;
            gvLicence.Rows.Clear();
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                DataGridViewRow tempRow = new DataGridViewRow();
                DataGridViewCell cellSLNO = new DataGridViewTextBoxCell();
                cellSLNO.Value = intRow;
                tempRow.Cells.Add(cellSLNO);

                DataGridViewCell cellExamPass = new DataGridViewTextBoxCell();
                cellExamPass.Value = Dt.Rows[i]["PM_BRAND_ID"];
                tempRow.Cells.Add(cellExamPass);

                DataGridViewCell cellYearPass = new DataGridViewTextBoxCell();
                cellYearPass.Value = Dt.Rows[i]["PM_PRODUCT_ID"];
                tempRow.Cells.Add(cellYearPass);

                DataGridViewCell cellSubject = new DataGridViewTextBoxCell();
                cellSubject.Value = Dt.Rows[i]["PM_PRODUCT_NAME"];
                tempRow.Cells.Add(cellSubject);

                if (Type == 0)
                {
                    DataGridViewCell cellV_G = new DataGridViewTextBoxCell();
                    cellV_G.Value = Dt.Rows[i]["SPHC_VtoG"];
                    tempRow.Cells.Add(cellV_G);
                    DataGridViewCell cellV_V = new DataGridViewTextBoxCell();
                    cellV_V.Value = Dt.Rows[i]["SPHC_VtoV"];
                    tempRow.Cells.Add(cellV_V);
                    DataGridViewCell cellG_V = new DataGridViewTextBoxCell();
                    cellG_V.Value = Dt.Rows[i]["SPHC_GtoV"];
                    tempRow.Cells.Add(cellG_V);
                }
                intRow = intRow + 1;
                gvLicence.Rows.Add(tempRow);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sqlQty = "";
            sqlQty += "DELETE FROM SP_HAMELI_CHARGES WHERE SPHC_BRANCH_CODE='" + sStockPoint + "'";
            for (int i = 0; i < gvLicence.Rows.Count; i++)
            {
                decimal vtog = Convert.ToDecimal(gvLicence.Rows[i].Cells[4].Value == null ? "0" : gvLicence.Rows[i].Cells[4].Value.ToString());
                decimal vtov = Convert.ToDecimal(gvLicence.Rows[i].Cells[5].Value == null ? "0" : gvLicence.Rows[i].Cells[5].Value.ToString());
                decimal gtov = Convert.ToDecimal(gvLicence.Rows[i].Cells[6].Value == null ? "0" : gvLicence.Rows[i].Cells[6].Value.ToString());
                sqlQty += " INSERT INTO SP_HAMELI_CHARGES(SPHC_SLNO,SPHC_BRANCH_CODE,SPHC_PRODUCT_ID,SPHC_VtoG,SPHC_VtoV,SPHC_GtoV) VALUES (" +
                Convert.ToInt32(i + 1) + ",'" + sStockPoint + "','" + gvLicence.Rows[i].Cells[2].Value.ToString() + "'," + vtog + "," + vtov + "," + gtov + ")";
            }
            objSQLDB = new SQLDB();
            int iRetVal = objSQLDB.ExecuteSaveData(sqlQty);
            objSQLDB = null;
            if (iRetVal > 0)
                MessageBox.Show("Inserted Data Successfully", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Data not Inserted", "SSCRM Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
