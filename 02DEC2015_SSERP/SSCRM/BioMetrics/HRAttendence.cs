using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace SSCRM
{
    public partial class HRAttendence : Form
    {
        string formType = "";
        public HRAttendence()
        {
            InitializeComponent();
        }

        public HRAttendence(string sformType)
        {
            formType = sformType;
            InitializeComponent();
            dtpFDate.Value = DateTime.Today;
            dtpTDate.Value = DateTime.Today;

            if(formType=="3")
            {
                dtpTDate.Visible = false;
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {

            if (dtpFDate.Value > dtpTDate.Value && formType != "5")
            {
                MessageBox.Show("Please Enter Valid Dates");
                return;
            }

            if (formType == "1")
            {
                crReportParams.FromDate = dtpFDate.Value.ToString("dd/MMM/yyyy");
                crReportParams.ToDate = dtpTDate.Value.ToString("dd/MMM/yyyy");
                ReportViewer childLateComing = new ReportViewer("ALL", "ALL", "ALL", 0, 0, "ALL");

                if (cbRepType.SelectedIndex == 1)
                {
                    CommonData.ViewReport = "SSBPLHO_ATTD_LateComing";
                }
                else
                {
                    CommonData.ViewReport = "SSBPLHO_ATTD";
                }
                childLateComing.Show();
            }
            if (formType == "2")
            {
                crReportParams.FromDate = dtpFDate.Value.ToString("dd/MMM/yyyy");
                crReportParams.ToDate = dtpTDate.Value.ToString("dd/MMM/yyyy");
                ReportViewer childLateComing;
                if (CommonData.LogUserRole == "MANAGEMENT" || CommonData.LogUserRole == "ADMIN")
                    childLateComing = new ReportViewer("", "", "", dtpFDate.Value.ToString("dd/MMM/yyyy"), dtpTDate.Value.ToString("dd/MMM/yyyy"), "0");
                else
                    childLateComing = new ReportViewer("", "", "", dtpFDate.Value.ToString("dd/MMM/yyyy"), dtpTDate.Value.ToString("dd/MMM/yyyy"), CommonData.LogUserId);
                CommonData.ViewReport = "SSCRM_REP_LEGAL_CASE_DETAILS";
                childLateComing.Show();
            }
            if(formType=="3")
            {
                crReportParams.FromDate = dtpFDate.Value.ToString("dd/MMM/yyyy");
                DateTime dtTommarow = dtpFDate.Value.AddDays(1);

                crReportParams.ToDate = dtTommarow.ToString("dd/MMM/yyyy");
                ReportViewer childLateComing = new ReportViewer(dtpFDate.Value.ToString("dd/MMM/yyyy"), dtTommarow.ToString("dd/MMM/yyyy"));
                CommonData.ViewReport = "BirthDaysMarriageEvents";
                childLateComing.Show();
            }
            if(formType=="4")
            {
                ReportViewer childPaymentRegister = new ReportViewer(CommonData.CompanyCode,CommonData.BranchCode,CommonData.FinancialYear, "0","CP", dtpFDate.Value.ToString("dd/MMM/yyyy"), dtpTDate.Value.ToString("dd/MMM/yyyy"));
                CommonData.ViewReport = "SSCRM_REP_PAYMENT_VOUCHER_REGISTER";
                childPaymentRegister.Show();
            }
            if(formType == "5")
            {
                ReportViewer childPaymentRegister = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, "A020301003", dtpFDate.Value.ToString("dd/MMM/yyyy"), dtpTDate.Value.ToString("dd/MMM/yyyy"), "", "0");
                CommonData.ViewReport = "SSCRM_REP_CASH_DFR";
                childPaymentRegister.Show();
            }
            if (formType == "6")
            {
                ReportViewer childPaymentRegister = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, "", dtpFDate.Value.ToString("dd/MMM/yyyy"), dtpTDate.Value.ToString("dd/MMM/yyyy"), "CR");
                CommonData.ViewReport = "SSCRM_REP_RECEIPT_VOUCHER_REGISTER";
                childPaymentRegister.Show();
            }
            if (formType == "7")
            {
                ReportViewer childPaymentRegister = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, dtpFDate.Value.ToString("dd/MMM/yyyy"), dtpTDate.Value.ToString("dd/MMM/yyyy"));
                CommonData.ViewReport = "SSCRM_REP_DAY_WISE_DENIM_DETL1";
                childPaymentRegister.Show();
            }
        }
        public void GetDFRExcelDownload()
        {
            SQLDB objSQLdb = new SQLDB();
            SqlParameter[] param = new SqlParameter[7];
            DataSet ds = new DataSet();
            try
            {               
                param[0] = objSQLdb.CreateParameter("@xcmp_cd", DbType.String, CommonData.CompanyCode, ParameterDirection.Input);
                param[1] = objSQLdb.CreateParameter("@xBranchCode", DbType.String, CommonData.BranchCode, ParameterDirection.Input);
                param[2] = objSQLdb.CreateParameter("@xCashBankAccount", DbType.String, "A020301003", ParameterDirection.Input);
                param[3] = objSQLdb.CreateParameter("@xFromDate", DbType.String, dtpTDate.Value.ToString("dd/MMM/yyyy"), ParameterDirection.Input);
                param[4] = objSQLdb.CreateParameter("@xToDate", DbType.String, dtpTDate.Value.ToString("dd/MMM/yyyy"), ParameterDirection.Input);
                param[5] = objSQLdb.CreateParameter("@xRepType", DbType.String, "", ParameterDirection.Input);
                param[6] = objSQLdb.CreateParameter("@xEcode", DbType.Int32, 0, ParameterDirection.Input);


                ds = objSQLdb.ExecuteDataSet("SSCRM_REP_CASH_DFR", CommandType.StoredProcedure, param);

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
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtExcel = ds.Tables[0];
                DataTable dtExcel2 = ds.Tables[1];
                Excel.Application oXL = new Excel.Application();
                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                oXL.Visible = true;

                Excel.Range rg = worksheet.get_Range("A3", "L3");
                Excel.Range rgData = worksheet.get_Range("A6", "L" + (dtExcel.Rows.Count + 6).ToString());
                rgData.Font.Size = 11;
                rgData.WrapText = true;
                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                rgData.Borders.Weight = 2;

                Excel.Range rgHead = worksheet.get_Range("A1", "L1");
                rgHead.Cells.ColumnWidth = 5;
                rgHead.Cells.MergeCells = true;
                rgHead.Cells.Value2 = CommonData.CompanyName + "";
                rgHead.HorizontalAlignment = Excel.Constants.xlCenter;
                rgHead.Font.ColorIndex = 30;
                rgHead.Font.Bold = true;
                rgHead.Font.Size = 13;
                rgHead.HorizontalAlignment = Excel.Constants.xlCenter;
                rgHead.Borders.Weight = 2;

                Excel.Range rgHead1 = worksheet.get_Range("A2", "L2");
                rgHead1.Cells.ColumnWidth = 5;
                rgHead1.Cells.MergeCells = true;
                rgHead1.Cells.Value2 = "DAILY FINANCIAL REPORT (CASH)";
                rgHead1.HorizontalAlignment = Excel.Constants.xlCenter;
                rgHead1.Font.Bold = true;
                rgHead1.Font.Size = 13;
                rgHead1.Font.ColorIndex = 30; rgHead1.Borders.Weight = 2;

                Excel.Range rgHead2 = worksheet.get_Range("A3", "L3");
                rgHead2.Cells.ColumnWidth = 5;
                rgHead2.Cells.MergeCells = true;
                rgHead2.Font.ColorIndex = 30;
                rgHead2.Cells.Value2 = "Branch:" + dtExcel.Rows[0]["cbb_branch_name"] + "              " + dtpTDate.Value.ToString("dd/MMM/yyyy");

                rgHead2.Font.Bold = true;
                //rgHead2.Font.Size = 13;
                //rgHead2.Font.ColorIndex = 30;
                rgHead2.Borders.Weight = 2;

                //rg.Font.Bold = true;
                //rg.Font.Name = "Times New Roman";
                //rg.Font.Size = 10;
                //rg.WrapText = true;
                //rg.Font.ColorIndex = 2;
                //rg.HorizontalAlignment = Excel.Constants.xlCenter;
                //rg.Interior.ColorIndex = 31;
                //rg.Borders.Weight = 2;
                //rg.Borders.LineStyle = Excel.Constants.xlSolid;
                //rg.Cells.RowHeight = 25;

                rg = worksheet.get_Range("A4:A5", Type.Missing);
                rg.Merge(Type.Missing);
                rg.Cells.ColumnWidth = 5;
                rg.Borders.Weight = 2;
                rg.Font.Bold = true;
                rg.Font.ColorIndex = 2;
                rg.Interior.ColorIndex = 31;
                rg.Cells.Value2 = "Sl.No";

                rg = worksheet.get_Range("B4:B5", Type.Missing);
                rg.Merge(Type.Missing);
                rg.Cells.ColumnWidth = 30;
                rg.Borders.Weight = 2;
                rg.Font.Bold = true;
                rg.Font.ColorIndex = 2;
                rg.Interior.ColorIndex = 31;
                rg.Cells.Value2 = "Head of the Account";

                rg = worksheet.get_Range("C4:C5", Type.Missing);
                rg.Merge(Type.Missing);
                rg.Cells.ColumnWidth = 30;
                rg.Borders.Weight = 2;
                rg.Font.ColorIndex = 2;
                rg.Font.Bold = true;
                rg.Interior.ColorIndex = 31;
                rg.Cells.Value2 = "Particulars";

                rg = worksheet.get_Range("D4:D5", Type.Missing);
                rg.Merge(Type.Missing);
                rg.Borders.Weight = 2;
                rg.Cells.ColumnWidth = 12;
                rg.Font.ColorIndex = 2;
                rg.Font.Bold = true;
                rg.Interior.ColorIndex = 31;
                rg.Cells.Value2 = "Opning\nBalance";

                rg = worksheet.get_Range("E4:E5", Type.Missing);
                rg.Merge(Type.Missing);
                rg.Borders.Weight = 2;
                rg.Cells.ColumnWidth = 12;
                rg.Font.ColorIndex = 2;
                rg.Font.Bold = true;
                rg.Interior.ColorIndex = 31;
                rg.Cells.Value2 = "Receipt";

                rg = worksheet.get_Range("F4:F5", Type.Missing);
                rg.Merge(Type.Missing);
                rg.Borders.Weight = 2;
                rg.Cells.ColumnWidth = 12;
                rg.Font.ColorIndex = 2;
                rg.Font.Bold = true;
                rg.Interior.ColorIndex = 31;
                rg.Cells.Value2 = "Payment";

                rg = worksheet.get_Range("G4:H4", Type.Missing);
                rg.Merge(Type.Missing);
                rg.Borders.Weight = 2;
                rg.Cells.ColumnWidth = 10;
                rg.Font.ColorIndex = 2;
                rg.Font.Bold = true;
                rg.Interior.ColorIndex = 31;
                rg.Cells.Value2 = "Receipt Details";

                rg = worksheet.get_Range("G5:G5", Type.Missing);
                rg.Font.Name = "Times New Roman";
                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                rg.Borders.Weight = 2;
                rg.Font.Size = 10;
                rg.Font.Bold = true;
                rg.Font.ColorIndex = 2;
                rg.Interior.ColorIndex = 31;
                rg.Cells.ColumnWidth = 12;
                rg.Cells.Value2 = "Bills";

                rg = worksheet.get_Range("H5:H5", Type.Missing);
                rg.Font.Name = "Times New Roman";
                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                rg.Borders.Weight = 2;
                rg.Font.Size = 10;
                rg.Font.Bold = true;
                rg.Font.ColorIndex = 2;
                rg.Interior.ColorIndex = 31;
                rg.Cells.ColumnWidth = 12;
                rg.Cells.Value2 = "Cash";

                rg = worksheet.get_Range("I4:J4", Type.Missing);
                rg.Merge(Type.Missing);
                rg.Borders.Weight = 2;
                rg.Cells.ColumnWidth = 10;
                rg.Font.Bold = true;
                rg.Font.ColorIndex = 2;
                rg.Interior.ColorIndex = 31;
                rg.Cells.Value2 = "Payment Details";

                rg = worksheet.get_Range("I5:I5", Type.Missing);
                rg.Font.Name = "Times New Roman";
                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                rg.Borders.Weight = 2;
                rg.Font.Size = 10;
                rg.Font.Bold = true;
                rg.Font.ColorIndex = 2;
                rg.Interior.ColorIndex = 31;
                rg.Cells.ColumnWidth = 12;
                rg.Cells.Value2 = "Bills";

                rg = worksheet.get_Range("J5:J5", Type.Missing);
                rg.Font.Name = "Times New Roman";
                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                rg.Borders.Weight = 2;
                rg.Font.Size = 10;
                rg.Font.Bold = true;
                rg.Font.ColorIndex = 2;
                rg.Interior.ColorIndex = 31;
                rg.Cells.ColumnWidth = 12;
                rg.Cells.Value2 = "Cash";

                rg = worksheet.get_Range("K4:K5", Type.Missing);
                rg.Merge(Type.Missing);
                rg.Cells.ColumnWidth = 15;
                rg.Font.Bold = true;
                rg.Borders.Weight = 2;
                rg.Font.ColorIndex = 2;
                rg.Interior.ColorIndex = 31;
                rg.Cells.Value2 = "Closing\nBalance";

                rg = worksheet.get_Range("L4:L5", Type.Missing);
                rg.Merge(Type.Missing);
                rg.Cells.ColumnWidth = 20;
                rg.Font.Bold = true;
                rg.Borders.Weight = 2;
                rg.Font.ColorIndex = 2;
                rg.Interior.ColorIndex = 31;
                rg.Cells.Value2 = "Remitted/Receiver \n Signature";

                int RowCounter = 6, ColCounter = 1, iData = 1;
                for (int i = 0; i < dtExcel.Rows.Count; i++)
                {
                    worksheet.Cells[RowCounter, ColCounter++] = iData++;
                    worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["cbb_account_name"];
                    worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["cbb_Emp_name"];
                    worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["cbb_opening_balance"];
                    worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["cbb_DEBIT_amount"];
                    worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["cbb_CREDIT_amount"];
                    if (i == 0)
                    {
                        if (dtExcel.Rows[i]["cbb_debit_credit"].ToString() == "D")
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["cbb_bills"];
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["cbb_cash"];
                            worksheet.Cells[RowCounter, ColCounter++] = "0";
                            worksheet.Cells[RowCounter, ColCounter++] = "0";
                        }
                        else
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = "0";
                            worksheet.Cells[RowCounter, ColCounter++] = "0";
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["cbb_bills"];
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["cbb_cash"];
                        }
                    }
                    else if (dtExcel.Rows[i]["cbb_voucher_id"].ToString() != dtExcel.Rows[i - 1]["cbb_voucher_id"].ToString() &&
                            dtExcel.Rows[i]["cbb_doc_type"].ToString() != dtExcel.Rows[i - 1]["cbb_doc_type"].ToString())
                    {
                        if (dtExcel.Rows[i]["cbb_debit_credit"].ToString() == "D")
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["cbb_bills"];
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["cbb_cash"];
                            worksheet.Cells[RowCounter, ColCounter++] = "0";
                            worksheet.Cells[RowCounter, ColCounter++] = "0";
                        }
                        else
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = "0";
                            worksheet.Cells[RowCounter, ColCounter++] = "0";
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["cbb_bills"];
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["cbb_cash"];
                        }
                    }
                    else
                    {
                        worksheet.Cells[RowCounter, ColCounter++] = "0";
                        worksheet.Cells[RowCounter, ColCounter++] = "0";
                        worksheet.Cells[RowCounter, ColCounter++] = "0";
                        worksheet.Cells[RowCounter, ColCounter++] = "0";
                    }
                    worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["cbb_closing_balance"];

                    RowCounter++;
                    ColCounter = 1;
                }
                Excel.Range rgHead3 = worksheet.get_Range("B" + (dtExcel.Rows.Count + 6) + ":B" + (dtExcel.Rows.Count + 6), Type.Missing);
                rgHead3.Cells.ColumnWidth = 25;
                rgHead3.Font.Bold = true;
                rgHead3.Cells.Value2 = "Total";
                rgHead3.VerticalAlignment = Excel.Constants.xlCenter;
                rgHead3.Font.Size = 12;

                Excel.Range rgHead4 = worksheet.get_Range("E" + (dtExcel.Rows.Count + 6) + ":E" + (dtExcel.Rows.Count + 6), Type.Missing);
                rgHead4.Cells.ColumnWidth = 10;
                rgHead4.Font.Bold = true;
                rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                rgHead4.Formula = "=Sum(E6:E" + (dtExcel.Rows.Count + 5) + ")";
                rgHead4.Font.Size = 12;

                rgHead4 = worksheet.get_Range("F" + (dtExcel.Rows.Count + 6) + ":F" + (dtExcel.Rows.Count + 6), Type.Missing);
                rgHead4.Cells.ColumnWidth = 10;
                rgHead4.Font.Bold = true;
                rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                rgHead4.Formula = "=Sum(F6:F" + (dtExcel.Rows.Count + 5) + ")";
                rgHead4.Font.Size = 12;

                rgHead4 = worksheet.get_Range("G" + (dtExcel.Rows.Count + 6) + ":G" + (dtExcel.Rows.Count + 6), Type.Missing);
                rgHead4.Cells.ColumnWidth = 10;
                rgHead4.Font.Bold = true;
                rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                rgHead4.Formula = "=Sum(G6:G" + (dtExcel.Rows.Count + 5) + ")";
                rgHead4.Font.Size = 12;

                rgHead4 = worksheet.get_Range("H" + (dtExcel.Rows.Count + 6) + ":H" + (dtExcel.Rows.Count + 6), Type.Missing);
                rgHead4.Cells.ColumnWidth = 10;
                rgHead4.Font.Bold = true;
                rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                rgHead4.Formula = "=Sum(H6:H" + (dtExcel.Rows.Count + 5) + ")";
                rgHead4.Font.Size = 12;

                rgHead4 = worksheet.get_Range("I" + (dtExcel.Rows.Count + 6) + ":I" + (dtExcel.Rows.Count + 6), Type.Missing);
                rgHead4.Cells.ColumnWidth = 10;
                rgHead4.Font.Bold = true;
                rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                rgHead4.Formula = "=Sum(I6:I" + (dtExcel.Rows.Count + 5) + ")";
                rgHead4.Font.Size = 12;

                rgHead4 = worksheet.get_Range("J" + (dtExcel.Rows.Count + 6) + ":J" + (dtExcel.Rows.Count + 6), Type.Missing);
                rgHead4.Cells.ColumnWidth = 10;
                rgHead4.Font.Bold = true;
                rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                rgHead4.Formula = "=Sum(J6:J" + (dtExcel.Rows.Count + 5) + ")";
                rgHead4.Font.Size = 12;


                rgData = worksheet.get_Range("D" + (dtExcel.Rows.Count + 8), "G" + (dtExcel.Rows.Count + 8 + dtExcel2.Rows.Count));
                rgData.Font.Size = 11;
                rgData.WrapText = true;

                rgData.Borders.Weight = 2;







                rgHead3 = worksheet.get_Range("D" + (dtExcel.Rows.Count + 7) + ":D" + (dtExcel.Rows.Count + 7), Type.Missing);
                //rgHead3.Cells.ColumnWidth = 25;
                rgHead3.Font.Bold = true;
                rgHead3.Cells.Value2 = "Denominations";
                rgHead3.Cells.MergeCells = true;
                rgHead3.VerticalAlignment = Excel.Constants.xlCenter;
                rgHead3.Font.Size = 12;
                rgHead3 = worksheet.get_Range("E" + (dtExcel.Rows.Count + 8) + ":E" + (dtExcel.Rows.Count + 8), Type.Missing);
                //rgHead3.Cells.ColumnWidth = 25;
                rgHead3.Font.Bold = true;
                rgHead3.Cells.MergeCells = true;
                rgHead3.Cells.Value2 = "Notes";
                rgHead3.VerticalAlignment = Excel.Constants.xlCenter;
                rgHead3.Font.Size = 12;
                rgHead3 = worksheet.get_Range("F" + (dtExcel.Rows.Count + 8) + ":F" + (dtExcel.Rows.Count + 8), Type.Missing);
                //rgHead3.Cells.ColumnWidth = 25;
                rgHead3.Font.Bold = true;
                rgHead3.Cells.MergeCells = true;
                rgHead3.Cells.Value2 = "Count";
                rgHead3.VerticalAlignment = Excel.Constants.xlCenter;
                rgHead3.Font.Size = 12;
                rgHead3 = worksheet.get_Range("G" + (dtExcel.Rows.Count + 8) + ":G" + (dtExcel.Rows.Count + 8), Type.Missing);
                rgHead3.Font.Bold = true;
                rgHead3.Cells.MergeCells = true;
                rgHead3.Cells.Value2 = "Rs.";
                rgHead3.VerticalAlignment = Excel.Constants.xlCenter;
                rgHead3.Font.Size = 12;

                RowCounter = dtExcel.Rows.Count + 9;
                ColCounter = 4;
                iData = 1;
                for (int i = 0; i < dtExcel2.Rows.Count; i++)
                {
                    worksheet.Cells[RowCounter, ColCounter++] = iData++;
                    worksheet.Cells[RowCounter, ColCounter++] = dtExcel2.Rows[i]["DPD_DENOMINATIONS"];
                    worksheet.Cells[RowCounter, ColCounter++] = dtExcel2.Rows[i]["DPD_DENM_QTY"];
                    worksheet.Cells[RowCounter, ColCounter++] = dtExcel2.Rows[i]["DPD_AMOUNT"];
                    RowCounter++;
                    ColCounter = 4;
                }
                rgHead3 = worksheet.get_Range("E" + (dtExcel.Rows.Count + 9 + dtExcel2.Rows.Count) + ":E" + (dtExcel.Rows.Count + 9 + dtExcel2.Rows.Count), Type.Missing);
                rgHead3.Font.Bold = true;
                rgHead3.Cells.MergeCells = true;
                rgHead3.Cells.Value2 = "Grand Totals:";
                rgHead3.VerticalAlignment = Excel.Constants.xlCenter;
                rgHead3.Font.Size = 12;

                //rgHead3 = worksheet.get_Range("G" + (dtExcel.Rows.Count +9 + dtExcel2.Rows.Count) + ":G" + (dtExcel.Rows.Count + 9 + dtExcel2.Rows.Count), Type.Missing);
                //rgHead4.Cells.ColumnWidth = 10;
                //rgHead4.Font.Bold = true;
                //rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                ////rgHead4.Formula = "=Sum(G" + (dtExcel.Rows.Count + 9) + ":G" + (dtExcel.Rows.Count + 8 + dtExcel2.Rows.Count) + ")";
                //rgHead4.Font.Size = 12;

                rgHead4 = worksheet.get_Range("G" + (dtExcel.Rows.Count + 9 + dtExcel2.Rows.Count) + ":G" + (dtExcel.Rows.Count + 9 + dtExcel2.Rows.Count), Type.Missing);
                rgHead4.Cells.ColumnWidth = 10;
                rgHead4.Font.Bold = true;
                if (dtExcel2.Rows.Count > 0)
                {
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(G" + (dtExcel.Rows.Count + 9) + ":G" + (dtExcel.Rows.Count + 8 + dtExcel2.Rows.Count) + ")";
                    rgHead4.Font.Size = 12;
                }
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void HRAttendence_Load(object sender, EventArgs e)
        {
            if (formType == "2")
            {
                cbRepType.Visible = false;
            }
            //if(formType == "5")
            //{
            //    btnReport.Text = "Download";
            //    dtpFDate.Visible = false;
            //    label7.Visible = false;
            //    label1.Text = "Date";
            //}
            dtpTDate.Value = DateTime.Today;
            //dtpFDate.Value = DateTime.Today;
           
        }

        private void dtpFDate_ValueChanged(object sender, EventArgs e)
        {
            //if(dtpTDate.Value<dtpFDate.Value)
            //{
            //    MessageBox.Show("Enter Valid Dates");
            //    dtpFDate.Value = dtpTDate.Value;
            //}

        }

        private void dtpTDate_ValueChanged(object sender, EventArgs e)
        {
            //if (dtpTDate.Value < dtpFDate.Value)
            //{
            //    MessageBox.Show("Enter Valid Dates");
            //    dtpTDate.Value = dtpFDate.Value;
            //}
        }

    }
}
