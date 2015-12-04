using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using Excel = Microsoft.Office.Interop.Excel;
using SSTrans;
namespace SSCRM
{
    public partial class SaleBuiltinReport : Form
    {
        SQLDB objSQLDB;
        ExcelDB objExDb;
        public SaleBuiltinReport()
        {
            InitializeComponent();
        }

        private void SaleBuiltinReport_Load(object sender, EventArgs e)
        {
            objSQLDB = new SQLDB();
            DataSet ds = objSQLDB.ExecuteDataSet("SELECT DISTINCT DOCUMENT_MONTH,FIN_YEAR FROM DOCUMENT_MONTH");
            objSQLDB = null;
            cmbDocMonth.DataSource = ds.Tables[0];
            cmbDocMonth.ValueMember = "FIN_YEAR";
            cmbDocMonth.DisplayMember = "DOCUMENT_MONTH";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            CommonData.ViewReport = "SSCRM_YTD_SALE_BULTIN_REPORT";
            ReportViewer oReportViewer = new ReportViewer("", cmbDocMonth.Text);
            oReportViewer.Show();
        }

        private void btnHeading_Click(object sender, EventArgs e)
        {
            DocGLReportHeadings ochld = new DocGLReportHeadings(cmbDocMonth.Text, cmbDocMonth.SelectedValue.ToString());
            ochld.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            //objExDb = new ExcelDB();
            //DataTable dtExcel = objExDb.GetSaleBuiltinReport(cmbDocMonth.Text).Tables[0];
            //objExDb = null;
            //if (dtExcel.Rows.Count > 0)
            //{
            //    try
            //    {
            //        Excel.Application oXL = new Excel.Application();
            //        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
            //        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
            //        oXL.Visible = true;

            //        Excel.Range rg = worksheet.get_Range("A1", "AF1");
            //        Excel.Range rgData = worksheet.get_Range("A2", "AF" + (dtExcel.Rows.Count + 1).ToString());
            //        rgData.Font.Size = 11;
            //        rgData.WrapText = true;
            //        rgData.VerticalAlignment = Excel.Constants.xlCenter;
            //        rgData.Borders.Weight = 2;

            //        rg.Font.Bold = true;
            //        rg.Font.Name = "Times New Roman";
            //        rg.Font.Size = 10;
            //        rg.WrapText = true;
            //        rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
            //        rg.HorizontalAlignment = Excel.Constants.xlCenter;
            //        rg.Interior.ColorIndex = 31;
            //        rg.Borders.Weight = 2;
            //        rg.Borders.LineStyle = Excel.Constants.xlSolid;
            //        rg.Cells.RowHeight = 38;

            //        rg = worksheet.get_Range("A1", Type.Missing);
            //        rg.Cells.ColumnWidth = 5;
            //        rg.Cells.Value2 = "Sl.No";

            //        rg = worksheet.get_Range("B1", Type.Missing);
            //        rg.Cells.ColumnWidth = 20;
            //        rg.Cells.Value2 = "Doc.Month";

            //        rg = worksheet.get_Range("C1", Type.Missing);
            //        rg.Cells.ColumnWidth = 20;
            //        rg.Cells.Value2 = "E-Code";

            //        rg = worksheet.get_Range("D1", Type.Missing);
            //        rg.Cells.ColumnWidth = 30;
            //        rg.Cells.Value2 = "Employee Name";

            //        rg = worksheet.get_Range("E1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = "Desig";

            //        rg = worksheet.get_Range("F1", Type.Missing);
            //        rg.Cells.ColumnWidth = 20;
            //        rg.Cells.Value2 = "Date of Joining";

            //        rg = worksheet.get_Range("G1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = "PMD";

            //        rg = worksheet.get_Range("H1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = "D.A.Days";

            //        rg = worksheet.get_Range("I1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = "Demos";

            //        rg = worksheet.get_Range("J1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = "Units";

            //        rg = worksheet.get_Range("K1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = "Points";

            //        rg = worksheet.get_Range("L1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = "Cust.";

            //        rg = worksheet.get_Range("M1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = "Revenue";

            //        rg = worksheet.get_Range("N1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_01"].ToString();

            //        rg = worksheet.get_Range("O1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_02"].ToString();

            //        rg = worksheet.get_Range("P1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_03"].ToString();

            //        rg = worksheet.get_Range("Q1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_04"].ToString();

            //        rg = worksheet.get_Range("R1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_05"].ToString();

            //        rg = worksheet.get_Range("S1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_06"].ToString();

            //        rg = worksheet.get_Range("T1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_07"].ToString();

            //        rg = worksheet.get_Range("U1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_08"].ToString();

            //        rg = worksheet.get_Range("V1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_09"].ToString();

            //        rg = worksheet.get_Range("W1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_10"].ToString();

            //        rg = worksheet.get_Range("X1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_11"].ToString();

            //        rg = worksheet.get_Range("Y1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_12"].ToString();

            //        rg = worksheet.get_Range("Z1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_13"].ToString();

            //        rg = worksheet.get_Range("AA1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_14"].ToString();

            //        rg = worksheet.get_Range("AB1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_15"].ToString();

            //        rg = worksheet.get_Range("AC1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_16"].ToString();

            //        rg = worksheet.get_Range("AD1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_17"].ToString();

            //        rg = worksheet.get_Range("AE1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_18"].ToString();

            //        rg = worksheet.get_Range("AF1", Type.Missing);
            //        rg.Cells.ColumnWidth = 10;
            //        rg.Cells.Value2 = dtExcel.Rows[0]["syp_prod_19"].ToString();



            //        int RowCounter = 1;

            //        foreach (DataRow dr in dtExcel.Rows)
            //        {
            //            int i = 1;
            //            worksheet.Cells[RowCounter + 1, i++] = RowCounter;
            //            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_document_month"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_eora_code"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_eora_name"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_eora_desg"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_doj"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_pmd"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_dadays"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_demos"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_qty"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_prod_points"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_invoices"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_invamt"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_01"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_02"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_03"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_04"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_05"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_06"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_07"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_08"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_09"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_10"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_11"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_12"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_13"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_14"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_15"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_16"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_17"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_18"].ToString();
            //            worksheet.Cells[RowCounter + 1, i++] = dr["syp_prodPoints_19"].ToString();
            //            RowCounter++;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString(), "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }
    }
}
