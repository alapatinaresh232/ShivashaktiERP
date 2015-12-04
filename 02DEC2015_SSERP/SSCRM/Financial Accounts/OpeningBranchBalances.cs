using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSAdmin;
using SSCRMDB;
using SSCRM.App_Code;

namespace SSCRM
{
    public partial class OpeningBranchBalances : Form
    {
        DateTime startDate;
        public OpeningBranchBalances()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OpeningBranchBalances_Load(object sender, EventArgs e)
        {
            cbCrDr.SelectedIndex = 0;
            txtComanpanyName.Text = CommonData.CompanyName;
            txtBranch.Text = CommonData.BranchName;
            txtFinYear.Text = CommonData.FinancialYear;
            startDate = new DateTime(Convert.ToInt32(txtFinYear.Text.Split('-')[0]), 4, 1);

            SQLDB objDB = new SQLDB();
            DataTable dtAccMas = objDB.ExecuteDataSet(" SELECT AM_ACCOUNT_ID,AM_ACCOUNT_NAME AccName FROM FA_ACCOUNT_MASTER  WHERE AM_COMPANY_CODE='" + CommonData.CompanyCode + 
                "' AND AM_ACCOUNT_TYPE_ID IN('CASH','BANK')  and AM_ACCOUNT_GROUP_ID IS NULL ").Tables[0];


            UtilityLibrary.AutoCompleteComboBox(cmbAccounts, dtAccMas, "AM_ACCOUNT_ID", "AccName");
            if (dtAccMas.Rows.Count > 0)
            {
                DataRow dr = dtAccMas.NewRow();


                cmbAccounts.DataSource = dtAccMas;
                cmbAccounts.DisplayMember = "AccName";
                cmbAccounts.ValueMember = "AM_ACCOUNT_ID";
            }
            FillGrid();
            FillDocMonths();
        }

        private void FillDocMonths()
        {
            try
            {
                string strSQL = " SELECT document_month,document_month FROM document_month WHERE fin_year='"+CommonData.FinancialYear
                                +"' AND company_code='"+CommonData.CompanyCode+"' ORDER BY CAST(document_month AS DATETIME)";
                SQLDB objDB = new SQLDB();
                DataTable dt = objDB.ExecuteDataSet(strSQL).Tables[0];
                cmbOPAsOn.DataSource = dt;
                cmbOPAsOn.DisplayMember = "document_month";
                cmbOPAsOn.ValueMember = "document_month";

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOB.Text = "0";
        }
        private void FillGrid()
        {
            try
            {
                SQLDB objDB = new SQLDB();
                DataTable dt = new DataTable();
                DateTime date = startDate;
                string strCMD = "";
                for (int i = 0; i < 12; i++)
                {
                    
                        strCMD = " SELECT AB_MONTH Month,AB_YEAR Year,AB_OPEN_BALANCE openBalance,AB_MONTH_DEBITS MonthDebit,AB_MONTH_CREDITS MonthCredit,AB_CLOS_BALANCE CloseBalance FROM FA_ACCOUNT_BALANCES WHERE AP_FIN_YEAR='" + 
                            CommonData.FinancialYear + "' and AB_ACCOUNT_ID='" + cmbAccounts.SelectedValue + "' and AB_COMPANY_CODE='" + CommonData.CompanyCode +
                            "' and AB_MONTH='" + date.Month + "' AND AB_YEAR='" + date.Year + "' ";
                        strCMD = "SELECT ABB_MONTH Month,ABB_YEAR Year,ABB_OPEN_BALANCE openBalance,ABB_MONTH_DEBITS MonthDebit,ABB_MONTH_CREDITS MonthCredit,ABB_CLOS_BALANCE CloseBalance" +
                                  " FROM FA_ACCOUNT_BRANCH_BALANCES WHERE AP_FIN_YEAR='"+CommonData.FinancialYear+"' AND ABB_ACCOUNT_ID='"+cmbAccounts.SelectedValue+"' and ABB_COMPANY_CODE='"+CommonData.CompanyCode+
                                  "' and ABB_MONTH='"+date.Month+"' AND ABB_YEAR='"+date.Year+"'" +
                                    "AND ABB_BRANCH_CODE='"+CommonData.BranchCode+"'";
                        dt = objDB.ExecuteDataSet(strCMD).Tables[0];
                   
                  
                    // strCMD = " SELECT * FROM FA_ACCOUNT_BALANCES WHERE AB_MONTH='" + date.Month + "' AND AB_YEAR='" + date.Year + "' and AB_ACCOUNT_ID='" + sAccId + "'";
                    //dt = objDB.ExecuteDataSet(strCMD).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        gvProductDetails.Rows.Add();
                        gvProductDetails.Rows[i].Cells["SLNO"].Value = (i + 1);
                        gvProductDetails.Rows[i].Cells["MonYear"].Value = date.ToString("MMMyyyy").ToUpper();
                        gvProductDetails.Rows[i].Cells["OpenB"].Value = dt.Rows[0]["openBalance"];
                        gvProductDetails.Rows[i].Cells["Debit"].Value = dt.Rows[0]["MonthDebit"];
                        gvProductDetails.Rows[i].Cells["Credit"].Value = dt.Rows[0]["MonthCredit"];
                        gvProductDetails.Rows[i].Cells["CloseBal"].Value = dt.Rows[0]["CloseBalance"];
                    }
                    date = date.AddMonths(1);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime date = startDate;
           
            string strSQL = "";
            for (int i = 0; i < 12;i++ )
            {
                int starMonth = date.Month;
                int startYear = date.Year;
                if (cmbOPAsOn.Text == date.ToString("MMMyyyy").ToUpper())
                {
                    strSQL += " insert into FA_ACCOUNT_BRANCH_BALANCES(ABB_COMPANY_CODE," +
                                                                        "ABB_ACCOUNT_ID," +
                                                                        "ABB_MONTH," +
                                                                        "ABB_YEAR," +
                                                                        "ABB_OPEN_BALANCE," +
                                                                        "ABB_MONTH_DEBITS," +
                                                                        "ABB_MONTH_CREDITS," +
                                                                        "ABB_CLOS_BALANCE," +
                                                                        "ABB_CREATED_BY," +
                                                                        "ABB_AUTHORISED_BY," +
                                                                        "ABB_CREATED_DATE," +
                                                                        "AP_FIN_YEAR," +
                                                                        "ABB_BRANCH_CODE," +
                                                                        "ABB_OB_AS_ON," +
                                                                        "ABB_OB_AMT,ABB_CRORDR ) (select * from(select '" + CommonData.CompanyCode + "' CompCode,'" + cmbAccounts.SelectedValue + "' AcctId," +
                                                                        "'" + starMonth + "' Month,'" + startYear + "' Year,'" + txtOB.Text + "' Ob,'0' Debit,'0' Credit,'0' Cb,'" + CommonData.LogUserId
                                                                        + "' UserId,'' AuthorId,getdate() CurrDate,'" + CommonData.FinancialYear + "' FinYear,'" + CommonData.BranchCode + "' Branch,'"
                                                                        + cmbOPAsOn.Text + "' DocMonth,'" + txtOB.Text + "' OBa,";
                                                                        if (cbCrDr.Text == "CREDIT")
                                                                        {
                                                                            strSQL += "'C'";
                                                                        }
                                                                        else
                                                                        {
                                                                            strSQL += "'D'";
                                                                        }
                                                           strSQL+=    " crdr)a where not exists(select * from FA_ACCOUNT_BRANCH_BALANCES"+
                                                                        " where a.Branch=FA_ACCOUNT_BRANCH_BALANCES.ABB_BRANCH_CODE and FA_ACCOUNT_BRANCH_BALANCES.ABB_ACCOUNT_ID = a.AcctId "+
                                                                        " and FA_ACCOUNT_BRANCH_BALANCES.ABB_MONTH = a.Month and FA_ACCOUNT_BRANCH_BALANCES.ABB_YEAR = a.Year))";
                }
                else
                {
                    strSQL += " insert into FA_ACCOUNT_BRANCH_BALANCES(ABB_COMPANY_CODE," +
                                                                       "ABB_ACCOUNT_ID," +
                                                                       "ABB_MONTH," +
                                                                       "ABB_YEAR," +
                                                                       "ABB_OPEN_BALANCE," +
                                                                       "ABB_MONTH_DEBITS," +
                                                                       "ABB_MONTH_CREDITS," +
                                                                       "ABB_CLOS_BALANCE," +
                                                                       "ABB_CREATED_BY," +
                                                                       "ABB_AUTHORISED_BY," +
                                                                       "ABB_CREATED_DATE," +
                                                                       "AP_FIN_YEAR," +
                                                                       "ABB_BRANCH_CODE," +
                                                                       "ABB_OB_AS_ON," +
                                                                       "ABB_OB_AMT) (select * from(select '" + CommonData.CompanyCode + "' CompCode,'" + cmbAccounts.SelectedValue + "' AcctId," +
                                                                       "'" + starMonth + "' Month,'" + startYear + "' Year,'0' Ob,'0' Debit,'0' Credit,'0' Cb,'" + CommonData.LogUserId
                                                                       + "' UserId,'' AuthorId,getdate() CurrDate,'" + CommonData.FinancialYear + "' FinYear,'" + CommonData.BranchCode +
                                                                       "' Branch,'' DocMonth,'0' OBa)a where  not exists(select * from FA_ACCOUNT_BRANCH_BALANCES" +
                                                                        " where a.Branch=FA_ACCOUNT_BRANCH_BALANCES.ABB_BRANCH_CODE and FA_ACCOUNT_BRANCH_BALANCES.ABB_ACCOUNT_ID = a.AcctId " +
                                                                        " and FA_ACCOUNT_BRANCH_BALANCES.ABB_MONTH = a.Month and FA_ACCOUNT_BRANCH_BALANCES.ABB_YEAR = a.Year))";
                }
                date = date.AddMonths(1);
            }
            try
            {
                SQLDB obj=new SQLDB();
                int iRes = obj.ExecuteSaveData(strSQL);
                if( iRes > 0)
                {
                    MessageBox.Show("Data Saved Successfully","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    FillGrid();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
