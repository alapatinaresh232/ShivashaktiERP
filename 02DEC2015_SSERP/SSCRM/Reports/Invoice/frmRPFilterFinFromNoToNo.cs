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

namespace SSCRM
{
    public partial class frmRPFilterFinFromNoToNo : Form
    {
        SQLDB objSQLdb = null;
        private string sFrmType = "";

        public frmRPFilterFinFromNoToNo()
        {
            InitializeComponent();
        }

        public frmRPFilterFinFromNoToNo(string sFormType)
        {
            InitializeComponent();
            sFrmType = sFormType;
        }


        private void frmRPFilterFinFromNoToNo_Load(object sender, EventArgs e)
        {
            objSQLdb = new SQLDB();
            DataSet dsFinYear = new DataSet();
            DataTable dtfinYear = new DataTable();
            try
            {
                dsFinYear = objSQLdb.ExecuteDataSet("SELECT DISTINCT FY_FIN_YEAR FROM FIN_YEAR");
                dtfinYear = dsFinYear.Tables[0];
                cbFinYear.DataSource = null;
                if (dtfinYear.Rows.Count > 0)
                {
                    cbFinYear.DataSource = dtfinYear;
                    cbFinYear.DisplayMember = "FY_FIN_YEAR";
                    cbFinYear.ValueMember = "FY_FIN_YEAR";

                    cbFinYear.SelectedIndex = 0;
                    cbFinYear.SelectedValue = CommonData.FinancialYear;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try { Convert.ToInt32(txtFromNo.Text); }
            catch { MessageBox.Show("Invalid Invoice From No. Only Numeric Value to be Enetered.", "SSERP :: Invoice Print", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            try { Convert.ToInt32(txtToNo.Text); }
            catch { MessageBox.Show("Invalid Invoice From No. Only Numeric Value to be Enetered.", "SSERP :: Invoice Print", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            if (sFrmType == "")
            {
                ReportViewer childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, cbFinYear.SelectedValue.ToString(), "ALL", txtFromNo.Text, txtToNo.Text, "INVOICE WISE");
                CommonData.ViewReport = "SALES_INVOICE_BULK_PRINTING";
                childReportViewer.Show();
            }
            else if (sFrmType == "SP_INVOICE_PRINT")
            {
                ReportViewer childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, cbFinYear.SelectedValue.ToString(), "ALL", txtFromNo.Text, txtToNo.Text, "SP_INVOICE");
                CommonData.ViewReport = "STOCK_POINT_SALES_INVOICE_BULK_PRINTING";
                childReportViewer.Show();
            }
        }
    }
}
