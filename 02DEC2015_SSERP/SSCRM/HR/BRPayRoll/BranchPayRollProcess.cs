using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSTrans;
using SSAdmin;
using SSCRMDB;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;


namespace SSCRM
{
    public partial class BranchPayRollProcess : Form
    {
      
        UtilityDB objUtilityDB = null;
        SQLDB objSQLdb = null;
        bool flagSave = false;
        ExcelDB objExDb = null;

        public BranchPayRollProcess()
        {
            InitializeComponent();
        }
        private void BranchPayRollProcess_Load(object sender, EventArgs e)
        {
            cbPayRollType.SelectedIndex = 0;
            getDocMonth();
           
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                objExDb = new ExcelDB();
                objUtilityDB = new UtilityDB();
                DataTable dtExcel = new DataTable();
                dtExcel = objExDb.GetBrPayRollCheckListReportsData(docMonth.Value.ToString("MMMyyyy").ToUpper(), CommonData.CompanyCode,CommonData.BranchCode, "ALL",cbPayRollType.SelectedItem.ToString(),"PAYCHECKLIST").Tables[0];
                objExDb = null;

                if (dtExcel.Rows.Count > 0)
                {
                    Excel.Application oXL = new Excel.Application();
                    Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                    Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                    oXL.Visible = true;
                    string sLastColumn = objUtilityDB.GetColumnName(51);
                    Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                    Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows.Count) + 3).ToString());
                    rgData.Font.Size = 11;
                    rgData.WrapText = true;
                    rgData.VerticalAlignment = Excel.Constants.xlCenter;
                    rgData.Borders.Weight = 2;



                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.RowHeight = 38;

                    rg = worksheet.get_Range("A3", Type.Missing);
                    rg.Cells.ColumnWidth = 4;

                    rg = worksheet.get_Range("B3", Type.Missing);
                    rg.Cells.ColumnWidth = 10;

                    rg = worksheet.get_Range("C3", Type.Missing);
                    rg.Cells.ColumnWidth = 6;

                    rg = worksheet.get_Range("D3", Type.Missing);
                    rg.Cells.ColumnWidth = 30;

                    rg = worksheet.get_Range("E3", Type.Missing);
                    rg.Cells.ColumnWidth = 30;

                    rg = worksheet.get_Range("F3", Type.Missing);
                    rg.Cells.ColumnWidth = 30;

                    rg = worksheet.get_Range("G3", Type.Missing);
                    rg.Cells.ColumnWidth = 15;

                    rg = worksheet.get_Range("H3", Type.Missing);
                    rg.Cells.ColumnWidth = 7;

                    rg = worksheet.get_Range("I3", Type.Missing);
                    rg.Cells.ColumnWidth = 7;

                    //rg = worksheet.get_Range("I3", Type.Missing);
                    //rg.Cells.ColumnWidth = 10;
                    //rg.Interior.ColorIndex = 44;

                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(47) + "3", Type.Missing);
                    rg.Cells.ColumnWidth = 10;
                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(48) + "3", Type.Missing);
                    rg.Cells.ColumnWidth = 10;
                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(51) + "3", Type.Missing);
                    rg.Cells.ColumnWidth = 15;
                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(49) + "3", Type.Missing);
                    rg.Cells.ColumnWidth = 20;
                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(50) + "3", Type.Missing);
                    rg.Cells.ColumnWidth = 20;

                    Excel.Range rgHead = null;
                    rgHead = worksheet.get_Range("A1", "I2");
                    rgHead.Merge(Type.Missing);
                    rgHead.Font.Size = 14;
                    rgHead.Font.ColorIndex = 1;
                    rgHead.Font.Bold = true;
                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;

                    rgHead.Cells.Value2 = "BRANCH PAY ROLL REGISTER FOR THE MONTH OF " + dtExcel.Rows[0]["HPCM_WAGEMONTH"].ToString() + "";

                    int iColumn = 1;
                    worksheet.Cells[3, iColumn++] = "SlNo";
                    worksheet.Cells[3, iColumn++] = "Company";
                    worksheet.Cells[3, iColumn++] = "Ecode";
                    worksheet.Cells[3, iColumn++] = "Name";
                    worksheet.Cells[3, iColumn++] = "Desig";
                    worksheet.Cells[3, iColumn++] = "Department";
                    worksheet.Cells[3, iColumn++] = "Doj";
                    worksheet.Cells[3, iColumn++] = "Present Daye";
                    worksheet.Cells[3, iColumn++] = "Lops";

                    for (int i = 0; i < 2; i++)
                    {
                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumn) + "2", objUtilityDB.GetColumnName(iColumn + 13) + "2");
                        //rgHead.Cells.ColumnWidth = 5;
                        rgHead.Merge(Type.Missing);
                        if (i == 0)
                            rgHead.Value2 = "ACTUALS";
                        else
                            rgHead.Value2 = "EARNINGS";
                        rgHead.Interior.ColorIndex = 44 + i;
                        rgHead.Borders.Weight = 2;
                        rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                        rgHead.Cells.RowHeight = 20;
                        rgHead.Font.Size = 14;
                        rgHead.Font.ColorIndex = 1;
                        rgHead.Font.Bold = true;
                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumn) + "3", objUtilityDB.GetColumnName(iColumn + 13) + "3");
                        rgHead.Interior.ColorIndex = 44 + i;
                        rgHead.Font.ColorIndex = 1;
                        rgHead.Cells.ColumnWidth = 5;

                        worksheet.Cells[3, iColumn++] = "PF Basic";
                        worksheet.Cells[3, iColumn++] = "Basic";
                        worksheet.Cells[3, iColumn++] = "HRA";
                        worksheet.Cells[3, iColumn++] = "Conv Allw";
                        worksheet.Cells[3, iColumn++] = "CCA Allw";
                        worksheet.Cells[3, iColumn++] = "LTA Allw";
                        worksheet.Cells[3, iColumn++] = "Special Allw";
                        worksheet.Cells[3, iColumn++] = "Books & Period.";
                        worksheet.Cells[3, iColumn++] = "Med Reimb";
                        worksheet.Cells[3, iColumn++] = "Children";
                        worksheet.Cells[3, iColumn++] = "Vehicle Allw";
                        worksheet.Cells[3, iColumn++] = "Petrol Allw";
                        worksheet.Cells[3, iColumn++] = "Uniform Allw";
                        worksheet.Cells[3, iColumn++] = "Earning Total";
                    }

                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumn) + "2", objUtilityDB.GetColumnName(iColumn + 7) + "2");
                    //rgHead.Cells.ColumnWidth = 5;
                    rgHead.Merge(Type.Missing);
                    rgHead.Value2 = "DEDUCTIONS";
                    rgHead.Interior.ColorIndex = 46;
                    rgHead.Borders.Weight = 2;
                    rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                    rgHead.Cells.RowHeight = 20;
                    rgHead.Font.Size = 14;
                    rgHead.Font.ColorIndex = 1;
                    rgHead.Font.Bold = true;
                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumn) + "3", objUtilityDB.GetColumnName(iColumn + 7) + "3");
                    rgHead.Interior.ColorIndex = 46;
                    rgHead.Font.ColorIndex = 1;
                    rgHead.Cells.ColumnWidth = 5;

                    worksheet.Cells[3, iColumn++] = "PF";
                    worksheet.Cells[3, iColumn++] = "Proff Tax";
                    worksheet.Cells[3, iColumn++] = "ESI";
                    worksheet.Cells[3, iColumn++] = "Sal Adv";
                    worksheet.Cells[3, iColumn++] = "Pers Loan";
                    worksheet.Cells[3, iColumn++] = "TDS";
                    worksheet.Cells[3, iColumn++] = "Others";
                    worksheet.Cells[3, iColumn++] = "Total Deductions";

                    worksheet.Cells[3, iColumn++] = "Net Pay";
                    worksheet.Cells[3, iColumn++] = "Pay Mode";
                    worksheet.Cells[3, iColumn++] = "Bank Name";
                    worksheet.Cells[3, iColumn++] = "Account No";
                    worksheet.Cells[3, iColumn++] = "PF No";
                    worksheet.Cells[3, iColumn++] = "ESI No";

                    iColumn = 1;
                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                    {
                        worksheet.Cells[i + 4, iColumn++] = i + 1;
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_COMPANY_CODE"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_EORA_CODE"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_NAME"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_DESIG"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_DEPT_NAME"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_DOJ"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PRE"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_LOP"].ToString();

                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PF_BASIC"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_BASIC"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_HRA"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_CONV_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_CCA"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_LTA_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_SPL_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_BNP_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_MED_REIMB"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_CH_ED_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_VEH_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PET_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_UNF_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = "=U" + (i + 4).ToString() + "+V" + (i + 4).ToString() +
                                                            "+K" + (i + 4).ToString() + "+L" + (i + 4).ToString() +
                                                            "+M" + (i + 4).ToString() + "+N" + (i + 4).ToString() +
                                                            "+O" + (i + 4).ToString() + "+P" + (i + 4).ToString() +
                                                            "+Q" + (i + 4).ToString() + "+R" + (i + 4).ToString() +
                                                            "+S" + (i + 4).ToString() + "+T" + (i + 4).ToString() + "";

                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_Pf_ERNG_BASIC"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_BASIC"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_HRA"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_CONV_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_CCA"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_LTA_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_SPL_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_BNP_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_MED_REIMB"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_CH_ED_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_VEH_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_PET_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_UNF_ALW"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ERNG_TOTAL"].ToString();
                        //worksheet.Cells[i + 4, iColumn++] = "=AG" + (i + 4).ToString() + "+AH" + (i + 4).ToString() +
                        //                                    "+AI" + (i + 4).ToString() + "+AJ" + (i + 4).ToString() +
                        //                                    "+Y" + (i + 4).ToString() + "+Z" + (i + 4).ToString() +
                        //                                    "+AA" + (i + 4).ToString() + "+AB" + (i + 4).ToString() +
                        //                                    "+AC" + (i + 4).ToString() + "+AD" + (i + 4).ToString() +
                        //                                    "+AE" + (i + 4).ToString() + "+AF" + (i + 4).ToString() + "";

                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_PF"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_PROFTAX"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_ESI"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_SAL_ADV"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_PERS_LOAN"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_TDS"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_OTHERS"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_DEDU_TOTAL"].ToString();

                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_NET_PAY"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PAY_MODE"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_BANK_NAME"].ToString();
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_BANK_ACCOUNT_NO"].ToString() + "'";
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_PF_NUMBER"].ToString() + "";
                        worksheet.Cells[i + 4, iColumn++] = dtExcel.Rows[i]["HPCM_PS_ESI_NUMBER"].ToString() + "";

                        iColumn = 1;
                    }
                }
                else
                {
                    MessageBox.Show("No Data Found!", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void getDocMonth()
        {
            string strGet = "";
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
               strGet = "SELECT BPWP_DOC_MONTH,BPWP_FROM_DATE FROM BR_PAYROLL_WAGE_PERIOD WHERE BPWP_STATUS='RUNNING' AND BPWP_BRANCH_CODE='" + CommonData.BranchCode + "'";
                dt = objSQLdb.ExecuteDataSet(strGet).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(" Selected Branch Do Not Have Running WagePeriod ");
                }
                else if (dt.Rows.Count > 0)
                {
                    docMonth.Value = Convert.ToDateTime(dt.Rows[0]["BPWP_FROM_DATE"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objSQLdb = null;
                dt = null;
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
                DataTable dt = new DataTable();
                objSQLdb = new SQLDB();
                SqlParameter[] param = new SqlParameter[6];
                try
                {
                    param[0] = objSQLdb.CreateParameter("@xWagePeriod", DbType.String, docMonth.Value.ToString("MMMyyyy").ToUpper(), ParameterDirection.Input);
                    param[1] = objSQLdb.CreateParameter("@xCmp_cd", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                    param[2] = objSQLdb.CreateParameter("@xBranch_cd", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                    param[3] = objSQLdb.CreateParameter("@xECode", DbType.String, "ALL", ParameterDirection.Input);
                    param[4] = objSQLdb.CreateParameter("@xPayRollType", DbType.String, cbPayRollType.SelectedItem.ToString(), ParameterDirection.Input);
                    param[5] = objSQLdb.CreateParameter("@xPROCTYPE", DbType.String, "PROCESS", ParameterDirection.Input);
                    dt = objSQLdb.ExecuteDataSet("HR_BR_PAYROLL_CALC_PROCESS", CommandType.StoredProcedure, param).Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objSQLdb = null;
                    param = null;
                }
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show(dt.Rows.Count + " Employees Details Updated");                                
                }
          }
      
        private void docMonth_ValueChanged(object sender, EventArgs e)
        {
           
            gvEmpLopDetails.Rows.Clear();
            gvEmpSal.Rows.Clear();
            btnReport.Enabled = false;
            DateTime dtfrom ;
            DateTime dtTo ;
            double NoDays;
                try
                {
                    if (cbPayRollType.SelectedIndex > 0)
                    {
                        string strCMD1 = "select * from BR_PAYROLL_WAGE_PERIOD where BPWP_DOC_MONTH='" + docMonth.Value.ToString("MMMyyyy") + "'AND BPWP_PAYROLL_TYPE='" + cbPayRollType.SelectedItem.ToString() + "'  AND BPWP_BRANCH_CODE='" + CommonData.BranchCode + "'";
                        objSQLdb = new SQLDB();
                        DataTable dt1 = objSQLdb.ExecuteDataSet(strCMD1).Tables[0];

                        if (dt1.Rows.Count > 0)
                        {
                            dtfrom = Convert.ToDateTime(dt1.Rows[0]["BPWP_FROM_DATE"]);
                            dtTo = Convert.ToDateTime(dt1.Rows[0]["BPWP_TO_DATE"]);
                            NoDays = (dtTo - dtfrom).TotalDays + 1;
                            if (dt1.Rows[0]["BPWP_STATUS"].ToString() == "RUNNING")
                            {
                                if (cbPayRollType.SelectedIndex > 0)
                                {
                                    string strCmd = " SELECT HPOAM_COMPANY_CODE CCode,HPOAM_BRANCH_CODE BCode,HPOAM_WAGEMONTH WageMonth,HPMTM_LOCATION ,HPOAM_ECODE Ecode,HPOAM_NET Net," +
                                            " HPOAM_PRE Pre,HPOAM_ABSX Abs, HPOAM_WOF WOff,HPOAM_HDAY Holiday,HPOAM_ELS_AVAILED Els,HPOAM_CLS_AVAILED Cls,HPOAM_SLS_AVAILED Sls,HPOAM_COFFS_AVAILED Coff, " +
                                            " HPOAM_LTMNTS Lmnts,HPOAM_EARLYGOMNTS Emnts,HPOAM_COMPANY_NAME CName,HPOAM_BRANCH_NAME BName,HPOAM_DEPT_CODE DCode,HPOAM_DEPT_NAME DName," +
                                            " HPOAM_ENAME EName,HPOAM_DESIG_ID DesigId,HPOAM_DESIGNATION DesingName,HPOAM_DOJ Doj from HR_PAYROLL_MANUAL_ATTD_MTOD WHERE HPOAM_WAGEMONTH ='" + docMonth.Value.ToString("MMMyyyy") +
                                            "' AND HPOAM_PAYROLL_TYPE='" + cbPayRollType.SelectedItem.ToString() + "' AND HPOAM_BRANCH_CODE='" + CommonData.BranchCode + "' AND (HPOAM_PRE+HPOAM_ABSX)<> (" + NoDays + ")";

                                    DataTable dt2 = objSQLdb.ExecuteDataSet(strCmd).Tables[0];

                                    if (dt2.Rows.Count > 0)
                                    {
                                        gvEmpLopDetails.Rows.Clear();
                                        for (int iVar = 0; iVar < dt2.Rows.Count; iVar++)
                                        {
                                            gvEmpLopDetails.Rows.Add();
                                            gvEmpLopDetails.Rows[iVar].Cells["SLNO"].Value = (iVar + 1).ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["Ecode"].Value = dt2.Rows[iVar]["Ecode"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["EmpName"].Value = dt2.Rows[iVar]["EName"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["Desig"].Value = dt2.Rows[iVar]["DesingName"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["TotDays"].Value = NoDays;
                                            gvEmpLopDetails.Rows[iVar].Cells["Lops"].Value = dt2.Rows[iVar]["Abs"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["EmpCLs"].Value = dt2.Rows[iVar]["Cls"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["EmpSLs"].Value = dt2.Rows[iVar]["Sls"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["EmpELs"].Value = dt2.Rows[iVar]["Els"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["CCode"].Value = dt2.Rows[iVar]["CCode"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["BCode"].Value = dt2.Rows[iVar]["BCode"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["Desig"].Value = dt2.Rows[iVar]["DesingName"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["WageMonth"].Value = dt2.Rows[iVar]["WageMonth"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["Location"].Value = dt2.Rows[iVar]["HPMTM_LOCATION"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["Woff"].Value = dt2.Rows[iVar]["WOff"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["Holiday"].Value = dt2.Rows[iVar]["Holiday"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["Lmnts"].Value = dt2.Rows[iVar]["Lmnts"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["Emnts"].Value = dt2.Rows[iVar]["Emnts"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["CName"].Value = dt2.Rows[iVar]["CName"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["BName"].Value = dt2.Rows[iVar]["BName"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["DCode"].Value = dt2.Rows[iVar]["DCode"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["DName"].Value = dt2.Rows[iVar]["DName"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["DesigId"].Value = dt2.Rows[iVar]["DesigId"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["Doj"].Value = dt2.Rows[iVar]["Doj"].ToString();

                                            //gvEmpLopDetails.Rows[iVar].Cells["WorkedDays"].Value = dt.Rows[iVar]["Ecode"].ToString();
                                            gvEmpLopDetails.Rows[iVar].Cells["PaidDays"].Value = dt2.Rows[iVar]["Pre"].ToString(); ;

                                            //if (Convert.ToDouble(gvEmpLopDetails.Rows[iVar].Cells["PaidDays"].Value.ToString()) > Convert.ToDouble(txtTotDays.Text.ToString()))
                                            //{
                                            //    gvEmpLopDetails.Rows[iVar].Cells["PaidDays"].Value = txtTotDays.Text.ToString();
                                            //}
                                            //else
                                            //{
                                            //    gvEmpLopDetails.Rows[iVar].Cells["PaidDays"].Value = dt.Rows[iVar]["Pre"].ToString();
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        btnReport.Enabled = true;
                                    }
                                    string strCmd3 = "SELECT * FROM HR_PAYROLL_MANUAL_ATTD_MTOD WHERE HPOAM_WAGEMONTH ='" + docMonth.Value.ToString("MMMyyyy") + "' AND HPOAM_BRANCH_CODE='" + CommonData.BranchCode + "' AND  HPOAM_PAYROLL_TYPE='" + cbPayRollType.SelectedItem.ToString() + "' ";

                                    string strCMD = "SELECT * FROM HR_PAYROLL_MANUAL_ATTD_MTOD WHERE HPOAM_ECODE  not IN (SELECT HESS_EORA_CODE FROM HR_EMP_SAL_STRU )AND HPOAM_WAGEMONTH='" + docMonth.Value.ToString("MMMyyyy") + "' AND HPOAM_BRANCH_CODE='" + CommonData.BranchCode + "' AND HPOAM_PAYROLL_TYPE='" + cbPayRollType.SelectedItem.ToString() + "'";

                                    objSQLdb = new SQLDB();
                                    DataTable dt = objSQLdb.ExecuteDataSet(strCMD).Tables[0];
                                    DataTable dt3 = objSQLdb.ExecuteDataSet(strCmd3).Tables[0];
                                    if (dt3.Rows.Count > 0)
                                    {
                                        btnReport.Enabled = false;
                                    }
                                    //string strcount= dt1.Rows[iVar]["HWP_DAYS"].ToString();
                                    if (dt.Rows.Count > 0)
                                    {
                                        gvEmpSal.Rows.Clear();

                                        for (int iVar = 0; iVar < dt.Rows.Count; iVar++)
                                        {
                                            gvEmpSal.Rows.Add();
                                            gvEmpSal.Rows[iVar].Cells["SLNO1"].Value = (iVar + 1).ToString();
                                            gvEmpSal.Rows[iVar].Cells["Ecode1"].Value = dt.Rows[iVar]["HPOAM_ECODE"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["EmpName1"].Value = dt.Rows[iVar]["HPOAM_ENAME"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["Desig1"].Value = dt.Rows[iVar]["HPOAM_DESIGNATION"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["TotDays1"].Value = NoDays;
                                            gvEmpSal.Rows[iVar].Cells["Lops1"].Value = dt.Rows[iVar]["HPOAM_ABSX"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["EmpCLs1"].Value = dt.Rows[iVar]["HPOAM_CLS_AVAILED"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["EmpSLs1"].Value = dt.Rows[iVar]["HPOAM_SLS_AVAILED"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["EmpELs1"].Value = dt.Rows[iVar]["HPOAM_ELS_AVAILED"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["CCode1"].Value = dt.Rows[iVar]["HPOAM_COMPANY_CODE"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["BCode1"].Value = dt.Rows[iVar]["HPOAM_BRANCH_CODE"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["Desig1"].Value = dt.Rows[iVar]["HPOAM_DESIGNATION"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["WageMonth1"].Value = dt.Rows[iVar]["HPOAM_WAGEMONTH"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["Location1"].Value = dt.Rows[iVar]["HPMTM_LOCATION"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["Woff1"].Value = dt.Rows[iVar]["HPOAM_WOF"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["Holiday1"].Value = dt.Rows[iVar]["HPOAM_HDAY"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["Lmnts1"].Value = dt.Rows[iVar]["HPOAM_LTMNTS"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["Emnts1"].Value = dt.Rows[iVar]["HPOAM_EARLYGOMNTS"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["CName1"].Value = dt.Rows[iVar]["HPOAM_COMPANY_NAME"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["BName1"].Value = dt.Rows[iVar]["HPOAM_BRANCH_NAME"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["DCode1"].Value = dt.Rows[iVar]["HPOAM_DEPT_CODE"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["DName1"].Value = dt.Rows[iVar]["HPOAM_DEPT_NAME"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["DesigId1"].Value = dt.Rows[iVar]["HPOAM_DESIG_ID"].ToString();
                                            gvEmpSal.Rows[iVar].Cells["Doj1"].Value = dt.Rows[iVar]["HPOAM_DOJ"].ToString();

                                            //gvEmpLopDetails.Rows[iVar].Cells["WorkedDays"].Value = dt.Rows[iVar]["Ecode"].ToString();

                                            gvEmpSal.Rows[iVar].Cells["PaidDays1"].Value = dt.Rows[iVar]["HPOAM_PRE"].ToString(); ;
                                        }
                                    }
                                    else
                                    {
                                        btnReport.Enabled = true;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please Select PayRollType");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please Select Running WagePeriod");
                            }
                        }                        
                    }
                  }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        private void cbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvEmpLopDetails.Rows.Clear();
            gvEmpSal.Rows.Clear();
             btnReport.Enabled = false;
            DateTime dtfrom;
            DateTime dtTo;
            double NoDays;
            try
            {
                string strCMD1 = "select * from BR_PAYROLL_WAGE_PERIOD where BPWP_DOC_MONTH='" + docMonth.Value.ToString("MMMyyyy") + "'AND BPWP_PAYROLL_TYPE='" + cbPayRollType.SelectedItem.ToString() + "' AND BPWP_BRANCH_CODE='" + CommonData.BranchCode + "'";
                objSQLdb = new SQLDB();
                DataTable dt1 = objSQLdb.ExecuteDataSet(strCMD1).Tables[0];

                if (dt1.Rows.Count > 0)
                {
                    dtfrom = Convert.ToDateTime(dt1.Rows[0]["BPWP_FROM_DATE"]);
                    dtTo = Convert.ToDateTime(dt1.Rows[0]["BPWP_TO_DATE"]);
                    NoDays = (dtTo - dtfrom).TotalDays + 1;
                    if (dt1.Rows[0]["BPWP_STATUS"].ToString() == "RUNNING")
                    {
                        if (cbPayRollType.SelectedIndex > 0)
                        {
                            string strCmd = " SELECT HPOAM_COMPANY_CODE CCode,HPOAM_BRANCH_CODE BCode,HPOAM_WAGEMONTH WageMonth,HPMTM_LOCATION ,HPOAM_ECODE Ecode,HPOAM_NET Net," +
                                    " HPOAM_PRE Pre,HPOAM_ABSX Abs, HPOAM_WOF WOff,HPOAM_HDAY Holiday,HPOAM_ELS_AVAILED Els,HPOAM_CLS_AVAILED Cls,HPOAM_SLS_AVAILED Sls,HPOAM_COFFS_AVAILED Coff, " +
                                    " HPOAM_LTMNTS Lmnts,HPOAM_EARLYGOMNTS Emnts,HPOAM_COMPANY_NAME CName,HPOAM_BRANCH_NAME BName,HPOAM_DEPT_CODE DCode,HPOAM_DEPT_NAME DName," +
                                    " HPOAM_ENAME EName,HPOAM_DESIG_ID DesigId,HPOAM_DESIGNATION DesingName,HPOAM_DOJ Doj from HR_PAYROLL_MANUAL_ATTD_MTOD WHERE HPOAM_WAGEMONTH ='" + docMonth.Value.ToString("MMMyyyy") + "' AND HPOAM_PAYROLL_TYPE='" + cbPayRollType.SelectedItem.ToString() + "' AND HPOAM_BRANCH_CODE='" + CommonData.BranchCode + "' AND (HPOAM_PRE+HPOAM_ABSX)<> (" + NoDays + ")";

                            DataTable dt2 = objSQLdb.ExecuteDataSet(strCmd).Tables[0];
                            // if( flagSave==true)
                            if (dt2.Rows.Count > 0)
                            {
                                gvEmpLopDetails.Rows.Clear();
                                for (int iVar = 0; iVar < dt2.Rows.Count; iVar++)
                                {
                                    gvEmpLopDetails.Rows.Add();
                                    gvEmpLopDetails.Rows[iVar].Cells["SLNO"].Value = (iVar + 1).ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["Ecode"].Value = dt2.Rows[iVar]["Ecode"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["EmpName"].Value = dt2.Rows[iVar]["EName"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["Desig"].Value = dt2.Rows[iVar]["DesingName"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["TotDays"].Value = NoDays;
                                    gvEmpLopDetails.Rows[iVar].Cells["Lops"].Value = dt2.Rows[iVar]["Abs"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["EmpCLs"].Value = dt2.Rows[iVar]["Cls"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["EmpSLs"].Value = dt2.Rows[iVar]["Sls"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["EmpELs"].Value = dt2.Rows[iVar]["Els"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["CCode"].Value = dt2.Rows[iVar]["CCode"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["BCode"].Value = dt2.Rows[iVar]["BCode"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["Desig"].Value = dt2.Rows[iVar]["DesingName"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["WageMonth"].Value = dt2.Rows[iVar]["WageMonth"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["Location"].Value = dt2.Rows[iVar]["HPMTM_LOCATION"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["Woff"].Value = dt2.Rows[iVar]["WOff"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["Holiday"].Value = dt2.Rows[iVar]["Holiday"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["Lmnts"].Value = dt2.Rows[iVar]["Lmnts"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["Emnts"].Value = dt2.Rows[iVar]["Emnts"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["CName"].Value = dt2.Rows[iVar]["CName"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["BName"].Value = dt2.Rows[iVar]["BName"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["DCode"].Value = dt2.Rows[iVar]["DCode"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["DName"].Value = dt2.Rows[iVar]["DName"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["DesigId"].Value = dt2.Rows[iVar]["DesigId"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["Doj"].Value = dt2.Rows[iVar]["Doj"].ToString();

                                    //gvEmpLopDetails.Rows[iVar].Cells["WorkedDays"].Value = dt.Rows[iVar]["Ecode"].ToString();
                                    gvEmpLopDetails.Rows[iVar].Cells["PaidDays"].Value = dt2.Rows[iVar]["Pre"].ToString(); ;

                                    //if (Convert.ToDouble(gvEmpLopDetails.Rows[iVar].Cells["PaidDays"].Value.ToString()) > Convert.ToDouble(txtTotDays.Text.ToString()))
                                    //{
                                    //    gvEmpLopDetails.Rows[iVar].Cells["PaidDays"].Value = txtTotDays.Text.ToString();
                                    //}
                                    //else
                                    //{
                                    //    gvEmpLopDetails.Rows[iVar].Cells["PaidDays"].Value = dt.Rows[iVar]["Pre"].ToString();
                                    //}

                                }
                            }
                            else
                            {
                                btnReport.Enabled = true;
                            }
                            string strCmd3 = "SELECT * FROM HR_PAYROLL_MANUAL_ATTD_MTOD WHERE HPOAM_WAGEMONTH ='" + docMonth.Value.ToString("MMMyyyy") + "' AND  HPOAM_BRANCH_CODE='" + CommonData.BranchCode + "' AND HPOAM_PAYROLL_TYPE='" + cbPayRollType.SelectedItem.ToString() + "' ";

                            string strCMD = "SELECT * FROM HR_PAYROLL_MANUAL_ATTD_MTOD WHERE HPOAM_ECODE  not IN (SELECT HESS_EORA_CODE FROM HR_EMP_SAL_STRU )AND HPOAM_WAGEMONTH='" + docMonth.Value.ToString("MMMyyyy") + "' AND  HPOAM_BRANCH_CODE='" + CommonData.BranchCode + "' AND HPOAM_PAYROLL_TYPE='" + cbPayRollType.SelectedItem.ToString() + "'";

                            objSQLdb = new SQLDB();
                            DataTable dt = objSQLdb.ExecuteDataSet(strCMD).Tables[0];
                            DataTable dt3 = objSQLdb.ExecuteDataSet(strCmd3).Tables[0];
                            if (dt3.Rows.Count > 0)
                            {
                                btnReport.Enabled = false;

                            }
                            //string strcount= dt1.Rows[iVar]["HWP_DAYS"].ToString();
                            if (dt.Rows.Count > 0)
                            {
                                gvEmpSal.Rows.Clear();

                                for (int iVar = 0; iVar < dt.Rows.Count; iVar++)
                                {
                                    gvEmpSal.Rows.Add();
                                    gvEmpSal.Rows[iVar].Cells["SLNO1"].Value = (iVar + 1).ToString();
                                    gvEmpSal.Rows[iVar].Cells["Ecode1"].Value = dt.Rows[iVar]["HPOAM_ECODE"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["EmpName1"].Value = dt.Rows[iVar]["HPOAM_ENAME"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["Desig1"].Value = dt.Rows[iVar]["HPOAM_DESIGNATION"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["TotDays1"].Value = NoDays;
                                    gvEmpSal.Rows[iVar].Cells["Lops1"].Value = dt.Rows[iVar]["HPOAM_ABSX"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["EmpCLs1"].Value = dt.Rows[iVar]["HPOAM_CLS_AVAILED"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["EmpSLs1"].Value = dt.Rows[iVar]["HPOAM_SLS_AVAILED"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["EmpELs1"].Value = dt.Rows[iVar]["HPOAM_ELS_AVAILED"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["CCode1"].Value = dt.Rows[iVar]["HPOAM_COMPANY_CODE"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["BCode1"].Value = dt.Rows[iVar]["HPOAM_BRANCH_CODE"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["Desig1"].Value = dt.Rows[iVar]["HPOAM_DESIGNATION"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["WageMonth1"].Value = dt.Rows[iVar]["HPOAM_WAGEMONTH"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["Location1"].Value = dt.Rows[iVar]["HPMTM_LOCATION"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["Woff1"].Value = dt.Rows[iVar]["HPOAM_WOF"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["Holiday1"].Value = dt.Rows[iVar]["HPOAM_HDAY"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["Lmnts1"].Value = dt.Rows[iVar]["HPOAM_LTMNTS"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["Emnts1"].Value = dt.Rows[iVar]["HPOAM_EARLYGOMNTS"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["CName1"].Value = dt.Rows[iVar]["HPOAM_COMPANY_NAME"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["BName1"].Value = dt.Rows[iVar]["HPOAM_BRANCH_NAME"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["DCode1"].Value = dt.Rows[iVar]["HPOAM_DEPT_CODE"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["DName1"].Value = dt.Rows[iVar]["HPOAM_DEPT_NAME"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["DesigId1"].Value = dt.Rows[iVar]["HPOAM_DESIG_ID"].ToString();
                                    gvEmpSal.Rows[iVar].Cells["Doj1"].Value = dt.Rows[iVar]["HPOAM_DOJ"].ToString();

                                    //gvEmpLopDetails.Rows[iVar].Cells["WorkedDays"].Value = dt.Rows[iVar]["Ecode"].ToString();

                                    gvEmpSal.Rows[iVar].Cells["PaidDays1"].Value = dt.Rows[iVar]["HPOAM_PRE"].ToString(); ;
                              }
                            }
                            else
                            {
                                btnReport.Enabled = true;
                            }
                          }
                          else
                            {
                                MessageBox.Show("Please Select PayRollType");
                             }     
                            }
                            else
                            {
                                MessageBox.Show("Please Select Running WagePeriod");
                            }
                        }                       
                    }
                
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }        
        }
        

        
    }
}
