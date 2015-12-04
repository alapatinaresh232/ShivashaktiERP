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
    public partial class OpeningBalances : Form
    {
        string sAccId, sFin,sComp,sType,sSubId;
        SQLDB objDB;
        DateTime startDate, endDate;

        public OpeningBalances()
        {
            InitializeComponent();
        }
        public OpeningBalances(string strAccId,string strFin,string strComp,string strType)
        {
            InitializeComponent();
            sAccId = strAccId;
            sFin = strFin;
            sComp = strComp;
            sType = strType;
        }
        public OpeningBalances(string strAccId,string strSubId, string strFin, string strComp, string strType)
        {
            InitializeComponent();
            sAccId = strAccId;
            sFin = strFin;
            sComp = strComp;
            sType = strType;
            sSubId = strSubId;
        }

        private void OpeningBalances_Load(object sender, EventArgs e)
        {
            
            startDate = new DateTime(Convert.ToInt32(sFin.Split('-')[0]), 4, 1);
            endDate = new DateTime(Convert.ToInt32(sFin.Split('-')[0]), 3, 31);
            FillGrid();
        }
        //private void FillGrid()
        //{
        //    try
        //    {
        //        objDB = new SQLDB();
        //        DataTable dt=new DataTable();
        //        DateTime date = startDate;
                
        //            string strCMD = "";
        //            if (sType == "ACCOUNTS")
        //            {
        //                strCMD = " SELECT AB_MONTH Month,AB_YEAR Year,AB_OPEN_BALANCE openBalance,AB_MONTH_DEBITS MonthDebit,AB_MONTH_CREDITS MonthCredit,AB_CLOS_BALANCE CloseBalance FROM FA_ACCOUNT_BALANCES WHERE AP_FIN_YEAR='" + sFin + "' and AB_ACCOUNT_ID='" + sAccId + "' and AB_COMPANY_CODE='" + sComp + "' order by AB_YEAR asc ";
        //                dt = objDB.ExecuteDataSet(strCMD).Tables[0];
        //            }
        //            if (sType == "MAJOR")
        //            {
        //                strCMD = " SELECT MCCB_MONTH Month,MCCB_YEAR Year,MCCB_OPENING_BALANCE openBalance,MCCB_DEBIT_AMOUNT MonthDebit,MCCB_CREDIT_AMOUNT MonthCredit,MCCB_CLOSING_BALANCE CloseBalance FROM FA_MAJOR_COST_CENTRE_BALANCES WHERE MCCB_FIN_YEAR='" + sFin + "' and MCCB_MAJOR_COST_CENTRE_ID='" + sAccId + "' and MCCB_COMPANY_CODE='" + sComp + "' order by MCCB_YEAR asc ";
        //                dt = objDB.ExecuteDataSet(strCMD).Tables[0];
        //            }
        //            if(sType=="COST")
        //            {
        //                strCMD = " SELECT CCB_MONTH Month,CCB_YEAR Year,CCB_OPENING_BALANCE openBalance,CCB_DEBIT_AMOUNT MonthDebit,CCB_CREDIT_AMOUNT MonthCredit,CCB_CLOSING_BALANCE CloseBalance FROM FA_COST_CENTRE_BALANCES WHERE CCB_FIN_YEAR='" + sFin + "' and CCB_COST_CENTRE_ID='" + sAccId + "' and CCB_MAJOR_COST_CENTRE_ID='" + sSubId + "' and CCB_COMPANY_CODE='" + sComp + "'  order by  CCB_YEAR asc ";
        //                dt = objDB.ExecuteDataSet(strCMD).Tables[0];
        //            }
        //            if(dt.Rows.Count>0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count;i++ )
        //                {
        //                    gvProductDetails.Rows.Add();
        //                    gvProductDetails.Rows[i].Cells["SLNO"].Value = (i + 1);
        //                    date = new DateTime(Convert.ToInt32(dt.Rows[i]["Year"].ToString()), Convert.ToInt32(dt.Rows[i]["Month"].ToString()), 1);
        //                    gvProductDetails.Rows[i].Cells["MonYear"].Value = date.ToString("MMMyyyy").ToUpper();
        //                    gvProductDetails.Rows[i].Cells["OpenB"].Value = dt.Rows[i]["openBalance"];
        //                    gvProductDetails.Rows[i].Cells["Debit"].Value = dt.Rows[i]["MonthDebit"];
        //                    gvProductDetails.Rows[i].Cells["Credit"].Value = dt.Rows[i]["MonthCredit"];
        //                    gvProductDetails.Rows[i].Cells["CloseBal"].Value = dt.Rows[i]["CloseBalance"];
        //                }
        //            }
        //             date = date.AddMonths(1);
                


        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}
        private void FillGrid()
        {
            try
            {
                objDB = new SQLDB();
                DataTable dt=new DataTable();
                DateTime date = startDate;
                 string strCMD="";
                for (int i = 0; i < 12; i++)
                {
                     if (sType == "ACCOUNTS")
                     {
                         strCMD = " SELECT AB_MONTH Month,AB_YEAR Year,AB_OPEN_BALANCE openBalance,AB_MONTH_DEBITS MonthDebit,AB_MONTH_CREDITS MonthCredit,AB_CLOS_BALANCE CloseBalance FROM FA_ACCOUNT_BALANCES WHERE AP_FIN_YEAR='" + sFin + "' and AB_ACCOUNT_ID='" + sAccId + "' and AB_COMPANY_CODE='" + sComp +
                             "' and AB_MONTH='" + date.Month + "' AND AB_YEAR='" + date.Year + "' ";
                         dt = objDB.ExecuteDataSet(strCMD).Tables[0];
                     }
                     if (sType == "MAJOR")
                     {
                         strCMD = " SELECT MCCB_MONTH Month,MCCB_YEAR Year,MCCB_OPENING_BALANCE openBalance,MCCB_DEBIT_AMOUNT MonthDebit,MCCB_CREDIT_AMOUNT MonthCredit,MCCB_CLOSING_BALANCE CloseBalance FROM FA_MAJOR_COST_CENTRE_BALANCES WHERE MCCB_FIN_YEAR='" + sFin + "' and MCCB_MAJOR_COST_CENTRE_ID='" + sAccId + "' and MCCB_COMPANY_CODE='" + sComp +
                             "'  and MCCB_MONTH='" + date.Month + "' AND MCCB_YEAR='" + date.Year + "' ";
                         dt = objDB.ExecuteDataSet(strCMD).Tables[0];
                     }
                     if (sType == "COST")
                     {
                         strCMD = " SELECT CCB_MONTH Month,CCB_YEAR Year,CCB_OPENING_BALANCE openBalance,CCB_DEBIT_AMOUNT MonthDebit,CCB_CREDIT_AMOUNT MonthCredit,CCB_CLOSING_BALANCE CloseBalance FROM FA_COST_CENTRE_BALANCES WHERE CCB_FIN_YEAR='" + sFin + "' and CCB_COST_CENTRE_ID='" + sSubId + "' and CCB_MAJOR_COST_CENTRE_ID='" + sAccId + "' and CCB_COMPANY_CODE='" + sComp +
                             "'  and CCB_MONTH='" + date.Month + "' AND CCB_YEAR='" + date.Year + "'  ";
                         dt = objDB.ExecuteDataSet(strCMD).Tables[0];
                     }

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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();

        }

    }
}
