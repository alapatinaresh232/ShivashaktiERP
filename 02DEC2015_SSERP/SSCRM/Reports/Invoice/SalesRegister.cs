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
using SSTrans;
using Excel = Microsoft.Office.Interop.Excel;

namespace SSCRM
{
    public partial class SalesRegister : Form
    {
        private Master objData = null;
        private ExcelDB objExDb = null;
        private string strDocMonths = string.Empty;
        UtilityDB objUtilityDB = null;
        private string strForm = string.Empty;
        private int iFormType = 0;
        public SalesRegister()
        {
            InitializeComponent();
        }

        public SalesRegister(int iForm)
        {
            iFormType = iForm;
            InitializeComponent();
        }

        //public SalesRegister(string SForm)
        //{
        //    InitializeComponent();
        //    strForm = SForm;
        //}

        private void SalesRegister_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.X + 170, Screen.PrimaryScreen.WorkingArea.Y + 160);
            this.StartPosition = FormStartPosition.CenterScreen;
            FillBranchData();
            //cbBranches.SelectedIndex = 0;
            cmbSalesInvoice.SelectedIndex = 0;
            rdbInvoiceDetail.Checked = true;
            rdbInvoiceDetail_CheckedChanged(null, null);
            dtpInvoiceFromDate.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
            dtpInvoiceToDate.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
            if (iFormType == 2)
            {
                this.Text = "Advance Register";
                cmbSalesInvoice.Visible = false;
                rdbInvoiceDetail.Visible = false;
            }
            else if (iFormType == 3)
            {
                this.Text = "Shortage/WriteOff/Excess Register";
                cmbSalesInvoice.Visible = false;
                rdbInvoiceDetail.Visible = false;
            }
            if (iFormType == 4)
            {
                this.Text = "Sales Regster With out Cuistmer data";
                rdbGroupSales.Visible = false;
                rdbProductCodes.Visible = false;
                cmbSalesInvoice.Items.Clear();
                cmbSalesInvoice.Items.Add("Invoice number wise");
                cmbSalesInvoice.SelectedIndex = 0;
                rdbInvoiceDetail.Checked = true;

            }

            else
            {
                this.Text = "Sales Register";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            string strDocMonths = "";
            string sqlText = "SELECT DISTINCT STUFF((SELECT ',' +document_month FROM document_month " +
                                "WHERE company_code = '"+CommonData.CompanyCode+"' AND CAST(document_month AS DATETIME) " +
                                "BETWEEN '" + dtpInvoiceFromDate.Value.ToString("MMMyyyy").ToUpper() +
                                "' AND '" + dtpInvoiceToDate.Value.ToString("MMMyyyy").ToUpper() +
                                "' ORDER BY start_date asc FOR XML PATH('')),1,1,'') AS DocMonths";
            SQLDB objDB = new SQLDB();
            try { strDocMonths = objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0]["DocMonths"].ToString(); }
            catch { strDocMonths = dtpInvoiceFromDate.Value.ToString("MMMyyyy").ToUpper(); }
            if (iFormType == 2)
            {
                //if (cbReportType.SelectedIndex == 0)
                //{
                CommonData.ViewReport = "SALESORDER_ORDER_BY_ORDER_NO";
                    ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), strDocMonths, 0);
                    objReportview.Show();
                //}
                //else if (cbReportType.SelectedIndex == 1)
                //{
                //    CommonData.ViewReport = "SALESORDER_ORDER_BY_ORDER_NO";
                //    ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"), 0);
                //    objReportview.Show();
                //}
            }
            if (iFormType == 3)
            {
                //if (cbReportType.SelectedIndex == 0)
                //{
                CommonData.ViewReport = "SHORTAGE_WRITEOFF_EXCESS_REG";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), CommonData.FinancialYear, dtpInvoiceFromDate.Value.ToString("MMMyyyy").ToUpper(), dtpInvoiceToDate.Value.ToString("MMMyyyy").ToUpper(),  "");
                objReportview.Show();
                //}
                //else if (cbReportType.SelectedIndex == 1)
                //{
                //    CommonData.ViewReport = "SALESORDER_ORDER_BY_ORDER_NO";
                //    ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"), 0);
                //    objReportview.Show();
                //}
            }
            if (iFormType == 4)
            {
                CommonData.ViewReport = "SSCRM_REP_INVOICE_WISE_DOC_SHEET";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), "", dtpInvoiceFromDate.Value.ToString("MMMyyyy").ToUpper(), dtpInvoiceToDate.Value.ToString("MMMyyyy").ToUpper(), "");
                objReportview.Show();
            }
            if (iFormType == 0)
            {
                ReportViewer childForm = new ReportViewer();
                strDocMonths = GetDocumentMonths();
                if (rdbProductCodes.Checked == true)
                {

                    CommonData.ViewReport = "ProductCode";
                    childForm.Show();
                }
                else if (rdbInvoiceDetail.Checked == true)
                {
                    if (strDocMonths.Length > 5)
                    {
                        //crReportParams.DocMonths = strDocMonths;
                        crReportParams.FromDate = dtpInvoiceFromDate.Value.ToString("MMMyyyy").ToUpper();
                        crReportParams.ToDate = dtpInvoiceToDate.Value.ToString("MMMyyyy").ToUpper();
                        CommonData.ViewReport = "InvoiceDetail";
                        if (cmbSalesInvoice.SelectedIndex == 0)
                            childForm = new ReportViewer("BRANCH-ORDNO", "");
                        else if (cmbSalesInvoice.SelectedIndex == 1)
                            childForm = new ReportViewer("BRANCH-INVNO", "");
                        else if (cmbSalesInvoice.SelectedIndex == 2)
                            childForm = new ReportViewer("BRANCH-GRP-ORDNO", "");
                        else if (cmbSalesInvoice.SelectedIndex == 3)
                            childForm = new ReportViewer("BRANCH-GRP-INVOICE", "");
                        else if (cmbSalesInvoice.SelectedIndex == 4)
                            childForm = new ReportViewer("VIJAYA_GROMIN_SALES","");
                        childForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Check document month selection");
                    }

                }
                else if (rdbGroupSales.Checked == true)
                {

                    if (strDocMonths.Length > 5)
                    {
                        CommonData.ViewReport = "GroupInvoiceDetail";
                        childForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Check document month selection");
                    }
                }
                else
                {

                    MessageBox.Show("Select report option.");
                }
            }

        }
        private void FillBranchData()
        {
            //objData = new Master();
            //DataSet dsBranch = null;
            //try
            //{
            //    dsBranch = objData.GetBranchDataSet(CommonData.CompanyCode.ToString());
            //    DataTable dtBranch = dsBranch.Tables[0];
            //    if (dtBranch.Rows.Count > 0)
            //    {
            //        cbBranches.DataSource = dtBranch;
            //        cbBranches.DisplayMember = "branch_name";
            //        cbBranches.ValueMember = "branch_code";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{

            //    objData = null;
            //    Cursor.Current = Cursors.Default;
            //}

        }

        private string GetDocumentMonths()
        {
            string strDM = "'";
            int intMonths = 0;
            intMonths = TotalMonthDifference(dtpInvoiceFromDate.Value, dtpInvoiceToDate.Value);
            for (int i = 0; i <= intMonths; i++)
            {
                strDM += dtpInvoiceFromDate.Value.AddMonths(i).ToString("MMM/yyyy").ToUpper() + "','";
            }
            if (strDM.Length > 5)
                strDM = strDM.Substring(0, strDM.Length - 2);
            return strDM;
        }
        private int TotalMonthDifference(DateTime dtThis, DateTime dtOther)
        {
            int intReturn = 0;

            dtThis = dtThis.Date.AddDays(-(dtThis.Day - 1));
            dtOther = dtOther.Date.AddDays(-(dtOther.Day - 1));

            while (dtOther.Date > dtThis.Date)
            {
                intReturn++;
                dtThis = dtThis.AddMonths(1);
            }

            return intReturn;
        }
        private void rdbInvoiceDetail_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbInvoiceDetail.Checked == true)
                cmbSalesInvoice.Visible = true;
            else
                cmbSalesInvoice.Visible = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string strDocMonths = "";
            string sqlText = "SELECT DISTINCT STUFF((SELECT ',' +document_month FROM document_month " +
                                "WHERE company_code = '" + CommonData.CompanyCode + "' AND CAST(document_month AS DATETIME) " +
                                "BETWEEN '" + dtpInvoiceFromDate.Value.ToString("MMMyyyy").ToUpper() +
                                "' AND '" + dtpInvoiceToDate.Value.ToString("MMMyyyy").ToUpper() +
                                "' ORDER BY start_date asc FOR XML PATH('')),1,1,'') AS DocMonths";
            SQLDB objDB = new SQLDB();
            try { strDocMonths = objDB.ExecuteDataSet(sqlText).Tables[0].Rows[0]["DocMonths"].ToString(); }
            catch { strDocMonths = dtpInvoiceFromDate.Value.ToString("MMMyyyy").ToUpper(); }

            #region SALES REGISTER
            if (iFormType == 0)
            {
                try
                {
                    if (rdbInvoiceDetail.Checked == true)
                    {
                        //if (strDocMonths.Length > 5)
                        //{
                        //crReportParams.DocMonths = strDocMonths;
                        dtpInvoiceFromDate.Value.ToString("dd/MM/yyyy");
                        dtpInvoiceToDate.Value.ToString("dd/MM/yyyy");
                        string sRep = "";
                        if (cmbSalesInvoice.SelectedIndex == 0)
                            sRep = "BRANCH-ORDNO";
                        else if (cmbSalesInvoice.SelectedIndex == 1)
                            sRep = "BRANCH-INVNO";
                        else if (cmbSalesInvoice.SelectedIndex == 2)
                            sRep = "BRANCH-GRP-ORDNO";
                        else if (cmbSalesInvoice.SelectedIndex == 3)
                            sRep = "BRANCH-GRP-INVOICE";
                       

                        DataTable dtEx = null;
                        objExDb = new ExcelDB();
                        if (cmbSalesInvoice.SelectedIndex == 4)
                            dtEx = objExDb.GetSalesRegisterDetails(CommonData.CompanyCode, CommonData.BranchCode, dtpInvoiceFromDate.Value.ToString("MMMyyyy").ToUpper(), dtpInvoiceFromDate.Value.ToString("dd/MMM/yyyy"), dtpInvoiceToDate.Value.ToString("dd/MMM/yyyy"), "VIJAYA_GROMIN_SALES").Tables[0];                        
                        else
                         dtEx = objExDb.GetSalesRegisterDetails(CommonData.CompanyCode, CommonData.BranchCode, dtpInvoiceFromDate.Value.ToString("MMMyyyy").ToUpper(), dtpInvoiceFromDate.Value.ToString("dd/MMM/yyyy"), dtpInvoiceToDate.Value.ToString("dd/MMM/yyyy"), "").Tables[0];
                        if (dtEx.Rows.Count > 0)
                        {
                            //try
                            //{
                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            //theWorkbook.Name = CommonData.BranchName + " SALES REGISTER " + CommonData.DocMonth.ToUpper();
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            worksheet.Name = CommonData.DocMonth.ToUpper();
                            oXL.Visible = true;
                            Excel.Range rgHead = worksheet.get_Range("A1", "Y1");
                            rgHead.Font.Size = 14;
                            rgHead.Cells.MergeCells = true;
                            rgHead.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead.Font.Bold = true;
                            rgHead.Font.ColorIndex = 30;
                            rgHead.Borders.Weight = 2;

                            Excel.Range rg = worksheet.get_Range("A2", "Y2");
                            Excel.Range rgData = worksheet.get_Range("A3", "Y" + (dtEx.Rows.Count + 2).ToString());
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

                            rgHead = worksheet.get_Range("A1", "Y1");
                            rgHead.Cells.ColumnWidth = 5;
                            //rg.Application.ActiveWindow.FreezePanes = true;
                            //rgHead.Height = 10;
                            rgHead.Cells.Value2 = CommonData.BranchName + "";

                            rg = worksheet.get_Range("A2", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Cells.Value2 = "Sl.No";

                            rg = worksheet.get_Range("B2", Type.Missing);
                            rg.Cells.ColumnWidth = 30;
                            rg.Cells.Value2 = "Company";

                            rg = worksheet.get_Range("C2", Type.Missing);
                            rg.Cells.ColumnWidth = 30;
                            rg.Cells.Value2 = "Branch";

                            rg = worksheet.get_Range("D2", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.Cells.Value2 = "Financial Year";

                            rg = worksheet.get_Range("E2", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.Cells.Value2 = "Document Month";

                            rgData = worksheet.get_Range("H3", "H" + (dtEx.Rows.Count + 2).ToString());
                            rgData.Cells.NumberFormat = "dd/MMM/yyyy";

                            rg = worksheet.get_Range("F2", Type.Missing);
                            rg.Cells.ColumnWidth = 7;
                            rg.Cells.Value2 = "Invoice No";

                            rg = worksheet.get_Range("G2", Type.Missing);
                            rg.Cells.ColumnWidth = 7;
                            rg.Cells.Value2 = "Order No";

                            rg = worksheet.get_Range("H2", Type.Missing);
                            rg.Cells.ColumnWidth = 13;
                            rg.Cells.Value2 = "Invoice Date";

                            rg = worksheet.get_Range("I2", Type.Missing);
                            rg.Cells.ColumnWidth = 7;
                            rg.Cells.Value2 = "SR Code";

                            rg = worksheet.get_Range("J2", Type.Missing);
                            rg.Cells.ColumnWidth = 20;
                            rg.Cells.Value2 = "SR Name";

                            rg = worksheet.get_Range("K2", Type.Missing);
                            rg.Cells.ColumnWidth = 15;
                            rg.Cells.Value2 = "Group Name";

                            rg = worksheet.get_Range("L2", Type.Missing);
                            rg.Cells.ColumnWidth = 20;
                            rg.Cells.Value2 = "GL Name";
                            rg = worksheet.get_Range("M2", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.Cells.Value2 = "Customer ID";
                            rg = worksheet.get_Range("N2", Type.Missing);
                            rg.Cells.ColumnWidth = 30;
                            rg.Cells.Value2 = "Customer Address";
                            rg = worksheet.get_Range("O2", Type.Missing);
                            rg.Cells.ColumnWidth = 12;
                            rg.Cells.Value2 = "Mobile No";
                            rg = worksheet.get_Range("P2", Type.Missing);
                            rg.Cells.ColumnWidth = 25;
                            rg.Cells.Value2 = "Products";
                            rg = worksheet.get_Range("Q2", Type.Missing);
                            rg.Cells.ColumnWidth = 7;
                            rg.Cells.Value2 = "Sold Qty";
                            rg = worksheet.get_Range("R2", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.Cells.Value2 = "Price";
                            rg = worksheet.get_Range("S2", Type.Missing);
                            rg.Cells.ColumnWidth = 20;
                            rg.Cells.Value2 = "FreeProducts";
                            rg = worksheet.get_Range("T2", Type.Missing);
                            //rg.Cells.ColumnWidth = 15;
                            rg.Cells.Value2 = "Free Qty";
                            rg = worksheet.get_Range("U2", Type.Missing);
                            //rg.Cells.ColumnWidth = 15;
                            rg.Cells.Value2 = "Total Qty";
                            rg = worksheet.get_Range("V2", Type.Missing);
                            //rg.Cells.ColumnWidth = 10;
                            rg.Cells.Value2 = "Total Points";
                            rg = worksheet.get_Range("W2", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.Cells.Value2 = "Advance Recieved";
                            rg = worksheet.get_Range("X2", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.Cells.Value2 = "Recieved Amount";
                            rg = worksheet.get_Range("Y2", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.Cells.Value2 = "Total Invoice Amt";

                            int RowCounter = 2;
                            int iData = 0;
                            foreach (DataRow dr in dtEx.Rows)
                            {
                                int i = 1;
                                //int iData = 0;
                                iData++;
                                int iSoldProd = 0, iFreeProd = 0;
                                if (dr["sr_prod_name1"].ToString() != "")
                                    iSoldProd++;
                                if (dr["sr_prod_name2"].ToString() != "")
                                    iSoldProd++;
                                if (dr["sr_prod_name3"].ToString() != "")
                                    iSoldProd++;
                                if (dr["sr_prod_name4"].ToString() != "")
                                    iSoldProd++;
                                if (dr["sr_prod_name5"].ToString() != "")
                                    iSoldProd++;
                                if (dr["sr_prod_name6"].ToString() != "")
                                    iSoldProd++;
                                if (dr["sr_prod_name7"].ToString() != "")
                                    iSoldProd++;
                                if (dr["sr_prod_name8"].ToString() != "")
                                    iSoldProd++;
                                if (dr["sr_prod_name9"].ToString() != "")
                                    iSoldProd++;
                                if (dr["sr_prodfr_name1"].ToString() != "")
                                    iFreeProd++;
                                if (dr["sr_prodfr_name2"].ToString() != "")
                                    iFreeProd++;
                                if (dr["sr_prodfr_name3"].ToString() != "")
                                    iFreeProd++;
                                if (dr["sr_prodfr_name4"].ToString() != "")
                                    iFreeProd++;
                                if (dr["sr_prodfr_name5"].ToString() != "")
                                    iFreeProd++;
                                if (dr["sr_prodfr_name6"].ToString() != "")
                                    iFreeProd++;
                                if (dr["sr_prodfr_name7"].ToString() != "")
                                    iFreeProd++;
                                if (dr["sr_prodfr_name8"].ToString() != "")
                                    iFreeProd++;
                                if (dr["sr_prodfr_name9"].ToString() != "")
                                    iFreeProd++;
                                int iMergRows = 0;
                                if (iSoldProd > iFreeProd)
                                    iMergRows = iSoldProd;
                                if (iSoldProd < iFreeProd)
                                    iMergRows = iFreeProd;
                                if (iSoldProd == iFreeProd)
                                    iMergRows = iFreeProd;
                                if (iMergRows == 0)
                                    iMergRows = 1;

                                rg = worksheet.get_Range("A" + (RowCounter + 1).ToString(), "A" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("B" + (RowCounter + 1).ToString(), "B" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("C" + (RowCounter + 1).ToString(), "C" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("D" + (RowCounter + 1).ToString(), "D" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("E" + (RowCounter + 1).ToString(), "E" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("F" + (RowCounter + 1).ToString(), "F" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("G" + (RowCounter + 1).ToString(), "G" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("H" + (RowCounter + 1).ToString(), "H" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("I" + (RowCounter + 1).ToString(), "I" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("J" + (RowCounter + 1).ToString(), "J" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("K" + (RowCounter + 1).ToString(), "K" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("L" + (RowCounter + 1).ToString(), "L" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("M" + (RowCounter + 1).ToString(), "M" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("N" + (RowCounter + 1).ToString(), "N" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("O" + (RowCounter + 1).ToString(), "O" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("P" + (RowCounter + 1).ToString(), "P" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.WrapText = true;
                                rg = worksheet.get_Range("Q" + (RowCounter + 1).ToString(), "Q" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.WrapText = true;
                                rg = worksheet.get_Range("R" + (RowCounter + 1).ToString(), "R" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.WrapText = true;
                                rg = worksheet.get_Range("S" + (RowCounter + 1).ToString(), "S" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.WrapText = true;
                                rg = worksheet.get_Range("T" + (RowCounter + 1).ToString(), "T" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.WrapText = true;
                                rg = worksheet.get_Range("U" + (RowCounter + 1).ToString(), "U" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("V" + (RowCounter + 1).ToString(), "V" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("W" + (RowCounter + 1).ToString(), "W" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("X" + (RowCounter + 1).ToString(), "X" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                                rg = worksheet.get_Range("Y" + (RowCounter + 1).ToString(), "Y" + (iMergRows + RowCounter).ToString());
                                rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;

                                worksheet.Cells[RowCounter + 1, i++] = iData;
                                worksheet.Cells[RowCounter + 1, i++] = dr["sr_company_name"].ToString();
                                worksheet.Cells[RowCounter + 1, i++] = dr["sr_branch_name"].ToString();
                                worksheet.Cells[RowCounter + 1, i++] = dr["sr_fin_year"].ToString();
                                worksheet.Cells[RowCounter + 1, i++] = dr["sr_document_month"].ToString();
                                worksheet.Cells[RowCounter + 1, i++] = dr["sr_invoice_number"].ToString();
                                worksheet.Cells[RowCounter + 1, i++] = dr["sr_order_number"].ToString();
                                worksheet.Cells[RowCounter + 1, i++] = Convert.ToDateTime(dr["sr_invoice_date"]).ToString("dd/MMM/yyyy");
                                worksheet.Cells[RowCounter + 1, i++] = dr["sr_eora_code"].ToString();
                                worksheet.Cells[RowCounter + 1, i++] = dr["sr_eora_name"].ToString();
                                worksheet.Cells[RowCounter + 1, i++] = dr["sr_grp_name"].ToString();
                                worksheet.Cells[RowCounter + 1, i++] = dr["sr_grp_eora_code"].ToString() + "-" + dr["sr_grp_eora_name"].ToString();
                                worksheet.Cells[RowCounter + 1, i++] = dr["sr_farmer_ID"].ToString();
                                worksheet.Cells[RowCounter + 1, i++] = dr["sr_cnv_farmer_name"].ToString() +
                                                                        ", " + dr["sr_cnv_so_fo"].ToString() +
                                                                        ":" + dr["sr_cnv_forg_name"].ToString() +
                                                                        ", " + dr["sr_cnv_village"].ToString() +
                                                                        ", " + dr["sr_cnv_mandal"].ToString() +
                                                                        ", " + dr["sr_cnv_district"].ToString() +
                                                                        ", " + dr["sr_cnv_state"].ToString() +
                                                                        ", " + dr["sr_pin_code"].ToString();
                                worksheet.Cells[RowCounter + 1, i++] = dr["sr_cnv_mobile_number"].ToString();

                                for (int isp = 0; isp < iSoldProd; isp++)
                                {
                                    worksheet.Cells[RowCounter + 1 + isp, i + 0] = dr["sr_prod_name" + (isp + 1).ToString()].ToString();
                                    worksheet.Cells[RowCounter + 1 + isp, i + 1] = dr["sr_qty" + (isp + 1).ToString()].ToString();
                                    worksheet.Cells[RowCounter + 1 + isp, i + 2] = dr["sr_rate" + (isp + 1).ToString()].ToString();
                                }
                                i = i + 3;
                                for (int isp = 0; isp < iFreeProd; isp++)
                                {
                                    worksheet.Cells[RowCounter + 1 + isp, i + 0] = dr["sr_prodfr_name" + (isp + 1).ToString()].ToString();
                                    worksheet.Cells[RowCounter + 1 + isp, i + 1] = dr["sr_prodfr_qty" + (isp + 1).ToString()].ToString();
                                }
                                i = i + 2;
                                //string strProduct = dr["sr_prod_name1"].ToString() + "";
                                //if (dr["sr_prod_name2"].ToString() != "")
                                //    strProduct += "\n" + dr["sr_prod_name2"].ToString() + "";
                                //if (dr["sr_prod_name3"].ToString() != "")
                                //    strProduct += "\n" + dr["sr_prod_name3"].ToString() + "";
                                //if (dr["sr_prod_name4"].ToString() != "")
                                //    strProduct += "\n" + dr["sr_prod_name4"].ToString() + "";
                                //if (dr["sr_prod_name5"].ToString() != "")
                                //    strProduct += "\n" + dr["sr_prod_name5"].ToString() + "";
                                //if (dr["sr_prod_name6"].ToString() != "")
                                //    strProduct += "\n" + dr["sr_prod_name6"].ToString() + "";
                                //if (dr["sr_prod_name7"].ToString() != "")
                                //    strProduct += "\n" + dr["sr_prod_name7"].ToString() + "";
                                //if (dr["sr_prod_name8"].ToString() != "")
                                //    strProduct += "\n" + dr["sr_prod_name8"].ToString() + "";
                                //if (dr["sr_prod_name9"].ToString() != "")
                                //    strProduct += "\n" + dr["sr_prod_name9"].ToString() + "";
                                //worksheet.Cells[RowCounter + 1, i++] = strProduct;
                                string strQty = "";
                                double dbQty = 0.00;
                                double dbPts = 0.00;
                                if (dr["sr_prod_name1"].ToString() != "")
                                {
                                    strQty += dr["sr_qty1"].ToString() + "";
                                    dbQty += Convert.ToDouble(dr["sr_qty1"]);
                                    dbPts += Convert.ToDouble(dr["sr_prod_points1"]);
                                }
                                if (dr["sr_prod_name2"].ToString() != "")
                                {
                                    strQty += "\n" + dr["sr_qty2"].ToString() + "";
                                    dbQty += Convert.ToDouble(dr["sr_qty2"]);
                                    dbPts += Convert.ToDouble(dr["sr_prod_points2"]);
                                }
                                if (dr["sr_prod_name3"].ToString() != "")
                                {
                                    strQty += "\n" + dr["sr_qty3"].ToString() + "";
                                    dbQty += Convert.ToDouble(dr["sr_qty3"]);
                                    dbPts += Convert.ToDouble(dr["sr_prod_points3"]);
                                }
                                if (dr["sr_prod_name4"].ToString() != "")
                                {
                                    strQty += "\n" + dr["sr_qty4"].ToString() + "";
                                    dbQty += Convert.ToDouble(dr["sr_qty4"]);
                                    dbPts += Convert.ToDouble(dr["sr_prod_points4"]);
                                }
                                if (dr["sr_prod_name5"].ToString() != "")
                                {
                                    strQty += "\n" + dr["sr_qty5"].ToString() + "";
                                    dbQty += Convert.ToDouble(dr["sr_qty5"]);
                                    dbPts += Convert.ToDouble(dr["sr_prod_points5"]);
                                }
                                if (dr["sr_prod_name6"].ToString() != "")
                                {
                                    strQty += "\n" + dr["sr_qty6"].ToString() + "";
                                    dbQty += Convert.ToDouble(dr["sr_qty6"]);
                                    dbPts += Convert.ToDouble(dr["sr_prod_points6"]);
                                }
                                if (dr["sr_prod_name7"].ToString() != "")
                                {
                                    strQty += "\n" + dr["sr_qty7"].ToString() + "";
                                    dbQty += Convert.ToDouble(dr["sr_qty7"]);
                                    dbPts += Convert.ToDouble(dr["sr_prod_points7"]);
                                }
                                if (dr["sr_prod_name8"].ToString() != "")
                                {
                                    strQty += "\n" + dr["sr_qty8"].ToString() + "";
                                    dbQty += Convert.ToDouble(dr["sr_qty8"]);
                                    dbPts += Convert.ToDouble(dr["sr_prod_points8"]);
                                }
                                if (dr["sr_prod_name9"].ToString() != "")
                                {
                                    strQty += "\n" + dr["sr_qty9"].ToString() + "";
                                    dbQty += Convert.ToDouble(dr["sr_qty9"]);
                                    dbPts += Convert.ToDouble(dr["sr_prod_points9"]);
                                }
                                //worksheet.Cells[RowCounter + 1, i++] = strQty;
                                //string strPrice = dr["sr_rate1"].ToString() + "";
                                //if (dr["sr_prod_name2"].ToString() != "")
                                //    strPrice += "\n" + dr["sr_rate2"].ToString() + "";
                                //if (dr["sr_prod_name3"].ToString() != "")
                                //    strPrice += "\n" + dr["sr_rate3"].ToString() + "";
                                //if (dr["sr_prod_name4"].ToString() != "")
                                //    strPrice += "\n" + dr["sr_rate4"].ToString() + "";
                                //if (dr["sr_prod_name5"].ToString() != "")
                                //    strPrice += "\n" + dr["sr_rate5"].ToString() + "";
                                //if (dr["sr_prod_name6"].ToString() != "")
                                //    strPrice += "\n" + dr["sr_rate6"].ToString() + "";
                                //if (dr["sr_prod_name7"].ToString() != "")
                                //    strPrice += "\n" + dr["sr_rate7"].ToString() + "";
                                //if (dr["sr_prod_name8"].ToString() != "")
                                //    strPrice += "\n" + dr["sr_rate8"].ToString() + "";
                                //if (dr["sr_prod_name9"].ToString() != "")
                                //    strPrice += "\n" + dr["sr_rate9"].ToString() + "";
                                //worksheet.Cells[RowCounter + 1, i++] = strPrice;

                                //strProduct = dr["sr_prodfr_name1"].ToString() + "";
                                //if (dr["sr_prodfr_name2"].ToString() != "")
                                //    strProduct += "\n" + dr["sr_prodfr_name2"].ToString() + "";
                                //if (dr["sr_prodfr_name3"].ToString() != "")
                                //    strProduct += "\n" + dr["sr_prodfr_name3"].ToString() + "";
                                //if (dr["sr_prodfr_name4"].ToString() != "")
                                //    strProduct += "\n" + dr["sr_prodfr_name4"].ToString() + "";
                                //if (dr["sr_prodfr_name5"].ToString() != "")
                                //    strProduct += "\n" + dr["sr_prodfr_name5"].ToString() + "";
                                //if (dr["sr_prodfr_name6"].ToString() != "")
                                //    strProduct += "\n" + dr["sr_prodfr_name6"].ToString() + "";
                                //if (dr["sr_prodfr_name7"].ToString() != "")
                                //    strProduct += "\n" + dr["sr_prodfr_name7"].ToString() + "";
                                //if (dr["sr_prodfr_name8"].ToString() != "")
                                //    strProduct += "\n" + dr["sr_prodfr_name8"].ToString() + "";
                                //if (dr["sr_prodfr_name9"].ToString() != "")
                                //    strProduct += "\n" + dr["sr_prodfr_name9"].ToString() + "";
                                //worksheet.Cells[RowCounter + 1, i++] = strProduct;

                                //strPrice = dr["sr_prodfr_qty1"].ToString() + "";
                                //if (dr["sr_prodfr_qty2"].ToString() != "")
                                //    strPrice += "\n" + dr["sr_prodfr_qty2"].ToString() + "";
                                //if (dr["sr_prodfr_qty3"].ToString() != "")
                                //    strPrice += "\n" + dr["sr_prodfr_qty3"].ToString() + "";
                                //if (dr["sr_prodfr_qty4"].ToString() != "")
                                //    strPrice += "\n" + dr["sr_prodfr_qty4"].ToString() + "";
                                //if (dr["sr_prodfr_qty5"].ToString() != "")
                                //    strPrice += "\n" + dr["sr_prodfr_qty5"].ToString() + "";
                                //if (dr["sr_prodfr_qty6"].ToString() != "")
                                //    strPrice += "\n" + dr["sr_prodfr_qty6"].ToString() + "";
                                //if (dr["sr_prodfr_qty7"].ToString() != "")
                                //    strPrice += "\n" + dr["sr_prodfr_qty7"].ToString() + "";
                                //if (dr["sr_prodfr_qty8"].ToString() != "")
                                //    strPrice += "\n" + dr["sr_prodfr_qty8"].ToString() + "";
                                //if (dr["sr_prodfr_qty9"].ToString() != "")
                                //    strPrice += "\n" + dr["sr_prodfr_qty9"].ToString() + "";
                                //worksheet.Cells[RowCounter + 1, i++] = strPrice;
                                worksheet.Cells[RowCounter + 1, i++] = dbQty.ToString("0.00");
                                worksheet.Cells[RowCounter + 1, i++] = dbPts.ToString("0.00");

                                worksheet.Cells[RowCounter + 1, i++] = dr["sr_advance_amount"].ToString();
                                worksheet.Cells[RowCounter + 1, i++] = dr["sr_received_amount"].ToString();
                                worksheet.Cells[RowCounter + 1, i++] = dr["sr_invoice_amount"].ToString();
                                RowCounter += iMergRows;
                            }
                            //worksheet.Cells[RowCounter + 2, 22] = "=SUM(V3:V" + (dtEx.Rows.Count + 2) + ")";
                            //}
                            //catch (Exception ex)
                            //{
                            //    MessageBox.Show(ex.ToString());
                            //}
                        }
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Check document month selection");
                        //}

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            #endregion

            #region ADVANCE REGISTER
            if (iFormType == 2)
            {
                objExDb = new ExcelDB();
                DataTable dtExcel = objExDb.GetSalesOrderRegister(CommonData.CompanyCode, CommonData.BranchCode, strDocMonths).Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;

                        Excel.Range rg = worksheet.get_Range("A1", "N1");
                        Excel.Range rgData = worksheet.get_Range("A2", "N" + (dtExcel.Rows.Count + 1).ToString());
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

                        rg = worksheet.get_Range("A1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Sl.No";

                        rg = worksheet.get_Range("B1", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        rg.Cells.Value2 = "DocMonth";

                        rg = worksheet.get_Range("C1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "OrderNo";

                        rg = worksheet.get_Range("D1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "ARNo";

                        rg = worksheet.get_Range("E1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "OrderDate";

                        rg = worksheet.get_Range("F1", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "SRName";

                        rg = worksheet.get_Range("G1", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "Customer Name";

                        rg = worksheet.get_Range("H1", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Cells.Value2 = "Customer Address";

                        rg = worksheet.get_Range("I1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Customer MobileNo";

                        rg = worksheet.get_Range("J1", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Cells.Value2 = "Product Details";

                        rg = worksheet.get_Range("K1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Price";

                        rg = worksheet.get_Range("L1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Qty";

                        rg = worksheet.get_Range("M1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "OrderAmount";

                        rg = worksheet.get_Range("N1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "AdvanceAmount";


                        int RowCounter = 1;

                        foreach (DataRow dr in dtExcel.Rows)
                        {
                            int i = 1;
                            worksheet.Cells[RowCounter + 1, i++] = RowCounter;
                            worksheet.Cells[RowCounter + 1, i++] = dr["so_DocMonth"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["so_order_number"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["so_ar_number"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = Convert.ToDateTime(dr["so_OrderDt"].ToString()).ToString("dd-MMM-yyyy");
                            worksheet.Cells[RowCounter + 1, i++] = dr["so_eora_code"].ToString() + "-" + dr["so_eora_name"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["so_FarmerName"].ToString();
                            string sAddress = "";
                            sAddress = dr["so_sofo"].ToString() + ":" + dr["so_Fathername"].ToString();
                            if (dr["so_Village"].ToString() != "")
                                sAddress += ", " + dr["so_Village"].ToString();
                            if (dr["so_Mondal"].ToString() != "")
                                sAddress += ", " + dr["so_Mondal"].ToString();
                            if (dr["so_Dirstict"].ToString() != "")
                                sAddress += ", " + dr["so_Dirstict"].ToString();
                            if (dr["so_state"].ToString() != "")
                                sAddress += ", " + dr["so_state"].ToString();
                            if (dr["so_pin"].ToString() != "")
                                sAddress += "-" + dr["so_pin"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = sAddress;
                            worksheet.Cells[RowCounter + 1, i++] = dr["so_MobileNo"].ToString();
                            string sProducts = "";
                            sProducts = dr["so_ProductName1"].ToString();
                            if (dr["so_ProductName2"].ToString() != "")
                                sProducts += ", \n" + dr["so_ProductName2"].ToString();
                            if (dr["so_ProductName3"].ToString() != "")
                                sProducts += ", \n" + dr["so_ProductName3"].ToString();
                            if (dr["so_ProductName4"].ToString() != "")
                                sProducts += ", \n" + dr["so_ProductName4"].ToString();
                            if (dr["so_ProductName5"].ToString() != "")
                                sProducts += ", \n" + dr["so_ProductName5"].ToString();
                            if (dr["so_ProductName6"].ToString() != "")
                                sProducts += ", \n" + dr["so_ProductName6"].ToString();
                            if (dr["so_ProductName7"].ToString() != "")
                                sProducts += ", \n" + dr["so_ProductName7"].ToString();
                            if (dr["so_ProductName8"].ToString() != "")
                                sProducts += ", \n" + dr["so_ProductName8"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = sProducts;
                            string sMrp = "";
                            sMrp = dr["so_SingleMRP1"].ToString();
                            if (dr["so_SingleMRP2"].ToString() != "")
                                sMrp += ", \n" + dr["so_SingleMRP2"].ToString();
                            if (dr["so_SingleMRP3"].ToString() != "")
                                sMrp += ", \n" + dr["so_SingleMRP3"].ToString();
                            if (dr["so_SingleMRP4"].ToString() != "")
                                sMrp += ", \n" + dr["so_SingleMRP4"].ToString();
                            if (dr["so_SingleMRP5"].ToString() != "")
                                sMrp += ", \n" + dr["so_SingleMRP5"].ToString();
                            if (dr["so_SingleMRP6"].ToString() != "")
                                sMrp += ", \n" + dr["so_SingleMRP6"].ToString();
                            if (dr["so_SingleMRP7"].ToString() != "")
                                sMrp += ", \n" + dr["so_SingleMRP7"].ToString();
                            if (dr["so_SingleMRP8"].ToString() != "")
                                sMrp += ", \n" + dr["so_SingleMRP8"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = sMrp;
                            string sQty = "";
                            sQty = dr["so_Qty1"].ToString();
                            if (dr["so_Qty2"].ToString() != "")
                                sQty += ", \n" + dr["so_Qty2"].ToString();
                            if (dr["so_Qty3"].ToString() != "")
                                sQty += ", \n" + dr["so_Qty3"].ToString();
                            if (dr["so_Qty4"].ToString() != "")
                                sQty += ", \n" + dr["so_Qty4"].ToString();
                            if (dr["so_Qty5"].ToString() != "")
                                sQty += ", \n" + dr["so_Qty5"].ToString();
                            if (dr["so_Qty6"].ToString() != "")
                                sQty += ", \n" + dr["so_Qty6"].ToString();
                            if (dr["so_Qty7"].ToString() != "")
                                sQty += ", \n" + dr["so_Qty7"].ToString();
                            if (dr["so_Qty8"].ToString() != "")
                                sQty += ", \n" + dr["so_Qty8"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = sQty;
                            worksheet.Cells[RowCounter + 1, i++] = dr["so_order_amount"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["so_order_Adv_amt"].ToString();

                            RowCounter++;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            #endregion
            #region "FormId 4 :: Sales Register With out Customer data"
            if (iFormType == 4)
            {
                try
                {
                    objExDb = new ExcelDB();
                    objUtilityDB = new UtilityDB();
                    //string strChkDemo = "";
                    //for (int i = 0; i < chkDemoType.CheckedItems.Count; i++)
                    //{
                    //    NewCheckboxListItem CL = (NewCheckboxListItem)chkDemoType.CheckedItems[i];
                    //    strChkDemo += "" + CL.Tag.ToString() + ",";
                    //}
                    DataTable dtExcel = objExDb.Get_SalesInvoiceDetails(CommonData.CompanyCode, CommonData.BranchCode, "", dtpInvoiceFromDate.Value.ToString("MMMyyyy"), dtpInvoiceToDate.Value.ToString("MMMyyyy"), "").Tables[0];
                    objExDb = null;

                    DataTable dtProducts = new DataTable();
                    dtProducts.Columns.Add("SlNo");
                    dtProducts.Columns.Add("ProductName");
                    dtProducts.Columns.Add("ProductType");
                    if (dtExcel.Rows.Count > 0)
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;


                        int totalColumnCount = Convert.ToInt32(dtExcel.Rows[0]["sr_no_srs"]);

                        string strColCount = objUtilityDB.GetColumnName(totalColumnCount + 11);
                        Excel.Range rg = worksheet.get_Range("A4", strColCount + "" + 4);
                        string strToData = strColCount + "" + ((Convert.ToInt32(dtExcel.Rows[0]["sr_inv_count"].ToString())) + 7).ToString();
                        Excel.Range rgData = worksheet.get_Range("A7", strToData);
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;





                        Excel.Range rgHead = worksheet.get_Range("A1", strColCount + "" + 1);
                        rgHead.Cells.ColumnWidth = 5;
                        rgHead.Cells.MergeCells = true;
                        rgHead.Cells.Value2 = CommonData.CompanyName + "";
                        rgHead.Font.ColorIndex = 30;
                        rgHead.Font.Bold = true;
                        rgHead.Font.Size = 14;
                        rgHead.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgHead.Borders.Weight = 2;

                        Excel.Range rgHead1 = worksheet.get_Range("A2", strColCount + "" + 2);
                        rgHead1.Cells.ColumnWidth = 5;
                        rgHead1.Cells.MergeCells = true;
                        rgHead1.Cells.Value2 = "SALES REGISTER WITH OUT CUSTAOMER DATA";
                        rgHead1.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgHead1.Font.Bold = true;
                        rgHead1.Font.Size = 14;
                        rgHead1.Font.ColorIndex = 30; rgHead1.Borders.Weight = 2;

                        Excel.Range rgHead2 = worksheet.get_Range("A3", strColCount + "" + 3);
                        rgHead2.Cells.ColumnWidth = 5;
                        rgHead2.Cells.MergeCells = true;
                        rgHead2.Cells.Value2 = "Physical Branch:" + dtExcel.Rows[0]["sr_branch_name"] +
                                                                                            "             DocMonth:" + dtExcel.Rows[0]["sr_document_month"];
                        rgHead2.Font.Bold = true;
                        rgHead2.Font.Size = 13;
                        rgHead2.Font.ColorIndex = 30;
                        rgHead2.Borders.Weight = 2;

                        rg.Font.Bold = true;
                        rg.Font.Name = "Times New Roman";
                        rg.Font.Size = 10;
                        rg.WrapText = true;
                        rg.Font.ColorIndex = 2;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.RowHeight = 25;

                        rg = worksheet.get_Range("A6:A7", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Borders.Weight = 2;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        rg.Font.Bold = true;
                        rg.Cells.Value2 = "Sl.No";

                        rg = worksheet.get_Range("B6:B7", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg.Font.Bold = true;
                        rg.Borders.Weight = 2;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        rg.Cells.Value2 = "Logical Branch";

                        rg = worksheet.get_Range("C6:C7", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth =10;
                        rg.Font.Bold = true;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        rg.Cells.Value2 = "Group Name";

                        rg = worksheet.get_Range("D6:D7", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 25;
                        rg.Font.Bold = true;
                        //rg.Orientation = 90;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        rg.Cells.Value2 = "GL Name";

                        rg = worksheet.get_Range("E6:E7", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 25;
                        rg.WrapText = true;
                        rg.Font.Bold = true;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        rg.Cells.Value2 = "SR Name";


                        rg = worksheet.get_Range("F6:F7", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 10;
                        rg.Font.Bold = true;
                        //rg.Orientation = 90;
                        rg.Font.ColorIndex = 2;
                        rg.Cells.WrapText = true;
                        rg.Interior.ColorIndex = 31;
                        rg.Cells.Value2 = "Invoice No";

                        rg = worksheet.get_Range("G6:G7", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 10;
                        rg.Font.Bold = true;
                        //rg.Orientation = 90;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        rg.Cells.WrapText = true;
                        rg.Cells.Value2 = "Invoice Date";

                        rg = worksheet.get_Range("H6:H7", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 10;
                        rg.Font.Bold = true;
                        //rg.Orientation = 90;
                        rg.Font.ColorIndex = 2;
                        rg.Cells.WrapText = true;
                        rg.Interior.ColorIndex = 31;
                        rg.Cells.Value2 = "Order No";


                        rg = worksheet.get_Range("I6:I7", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 10;
                        rg.Font.Bold = true;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        rg.Cells.WrapText = true;
                        //rg.Orientation = 90;
                        rg.Cells.Value2 = "Invoice Amt";

                        rg = worksheet.get_Range("J6:J7", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 10;
                        rg.Font.Bold = true;
                        rg.Cells.WrapText = true;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        //rg.Orientation = 90;
                        rg.Cells.Value2 = "Advance Amt";

                        rg = worksheet.get_Range("K6:K7", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 15;
                        rg.Font.Bold = true;
                        rg.Cells.WrapText = true;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        rgHead1.HorizontalAlignment = Excel.Constants.xlCenter;
                        //rg.Orientation = 90;
                        rg.Cells.Value2 = "Received Amt";

                        rg = worksheet.get_Range("L4:" + objUtilityDB.GetColumnName(Convert.ToInt32(dtExcel.Rows[0]["sr_sale_prd_count"]) + 11) + "4", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 10;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        rg.Cells.Value2 = "SALE PRODUCTS";

                        strToData = objUtilityDB.GetColumnName(Convert.ToInt32(dtExcel.Rows[0]["sr_sale_prd_count"].ToString()) + 11);
                        string strToData1 = objUtilityDB.GetColumnName(Convert.ToInt32(dtExcel.Rows[0]["sr_sale_prd_count"].ToString())
                            + Convert.ToInt32(dtExcel.Rows[0]["sr_free_prd_count"].ToString()) + 10);
                        rg = worksheet.get_Range(strToData + "4:" + strToData1 + "4", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 10;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        rg.Cells.Value2 = "FREE PRODUCTS";


                        int columnCount = 12;
                        int iCount = 1;
                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            if (dtExcel.Rows[i]["sr_product_type"].ToString() == "SALE PRODUCT")
                            {

                                DataRow[] DR = dtProducts.Select("ProductName='" + dtExcel.Rows[i]["sr_prod_name1"] + "' AND ProductType='SALE PRODUCT'");
                                if (DR.Length == 0)
                                {
                                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(columnCount) + "5:" + objUtilityDB.GetColumnName(columnCount) + "5", Type.Missing);
                                    rg.Borders.Weight = 2;
                                    rg.Cells.ColumnWidth = 10;

                                    //rg.Cells.col = 10;
                                    rg.Font.Bold = true;
                                    rg.Font.ColorIndex = 2;
                                    rg.Interior.ColorIndex = 31;
                                    rg.Cells.WrapText = true;
                                    rg.Orientation = 90;
                                    rg.Cells.Value2 = dtExcel.Rows[i]["sr_prod_name1"];

                                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(columnCount) + "6:" + objUtilityDB.GetColumnName(columnCount) + "6", Type.Missing);
                                    rg.Borders.Weight = 2;
                                    rg.Cells.ColumnWidth = 10;
                                    rg.Font.Bold = true;
                                    rg.Font.ColorIndex = 2;
                                    rg.Interior.ColorIndex = 31;
                                    rg.Cells.Value2 = dtExcel.Rows[i]["sr_rate1"];

                                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(columnCount) + "7:" + objUtilityDB.GetColumnName(columnCount) + "7", Type.Missing);
                                    rg.Borders.Weight = 2;
                                    rg.Cells.ColumnWidth = 10;
                                    rg.Font.Bold = true;
                                    rg.Font.ColorIndex = 2;
                                    rg.Interior.ColorIndex = 31;
                                    rg.Cells.Value2 = dtExcel.Rows[i]["sr_prod_points1"];
                                    columnCount++;
                                    dtProducts.Rows.Add(iCount++, dtExcel.Rows[i]["sr_prod_name1"], "SALE PRODUCT");
                                }
                            }

                        }
                        columnCount = 12 + Convert.ToInt32(dtExcel.Rows[0]["sr_sale_prd_count"].ToString());
                        iCount = Convert.ToInt32(dtExcel.Rows[0]["sr_sale_prd_count"].ToString()) + 1;
                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            if (dtExcel.Rows[i]["sr_product_type"].ToString() == "FREE PRODUCT")
                            {

                                DataRow[] DR = dtProducts.Select("ProductName='" + dtExcel.Rows[i]["sr_prod_name1"] + "' AND ProductType='FREE PRODUCT'");
                                if (DR.Length == 0)
                                {
                                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(columnCount) + "5:" + objUtilityDB.GetColumnName(columnCount) + "5", Type.Missing);
                                    rg.Borders.Weight = 2;
                                    rg.Cells.ColumnWidth = 10;
                                    rg.Font.Bold = true;
                                    rg.Font.ColorIndex = 2;
                                    rg.Interior.ColorIndex = 31;
                                    rg.Cells.WrapText = true;
                                    rg.Orientation = 90;
                                    rg.Cells.Value2 = dtExcel.Rows[i]["sr_prod_name1"];

                                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(columnCount) + "6:" + objUtilityDB.GetColumnName(columnCount) + "6", Type.Missing);
                                    rg.Borders.Weight = 2;
                                    rg.Cells.ColumnWidth = 10;
                                    rg.Font.Bold = true;
                                    rg.Font.ColorIndex = 2;
                                    rg.Interior.ColorIndex = 31;
                                    rg.Cells.Value2 = dtExcel.Rows[i]["sr_rate1"];

                                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(columnCount) + "7:" + objUtilityDB.GetColumnName(columnCount) + "7", Type.Missing);
                                    rg.Borders.Weight = 2;
                                    rg.Cells.ColumnWidth = 10;
                                    rg.Font.Bold = true;
                                    rg.Font.ColorIndex = 2;
                                    rg.Interior.ColorIndex = 31;
                                    rg.Cells.Value2 = dtExcel.Rows[i]["sr_prod_points1"];
                                    columnCount++;
                                    dtProducts.Rows.Add(iCount++, dtExcel.Rows[i]["sr_prod_name1"], "FREE PRODUCT");
                                }
                            }
                        }
                        int RowCounter = 8, ColCounter = 1, iData = 1;

                        int srCode = Convert.ToInt32(dtExcel.Rows[0]["sr_invoice_number"]);
                        int tmCode = Convert.ToInt32(dtExcel.Rows[0]["sr_eora_code"]);
                        int tempRowcounter = 8;

                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            int code = Convert.ToInt32(dtExcel.Rows[i]["sr_eora_code"]);

                            if (code != tmCode && i != 0)
                            {



                                Excel.Range rgHead3 = worksheet.get_Range("C" + (RowCounter) + ":C" + (RowCounter), Type.Missing);
                                rgHead3.Cells.ColumnWidth = 25;
                                rgHead3.Font.Bold = true;
                                rgHead3.Cells.Value2 = " Total";
                                rgHead3.Font.Size = 10;
                                rgHead3.VerticalAlignment = Excel.Constants.xlCenter;

                                for (int iProd = 0; iProd < dtProducts.Rows.Count + 3; iProd++)
                                {
                                    string strCol = objUtilityDB.GetColumnName(8 + iProd + 1);
                                    Excel.Range rgHead4 = worksheet.get_Range(strCol + "" + (RowCounter) + ":" + strCol + "" + (RowCounter), Type.Missing);
                                    rgHead4.Cells.ColumnWidth = 8;
                                    rgHead4.Font.Bold = true;
                                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                                    rgHead4.Font.Size = 10;
                                    rgHead4.Formula = "=Sum(" + strCol + "" + tempRowcounter + ":" + strCol + (RowCounter - 1) + ")";
                                }
                                tmCode = Convert.ToInt32(dtExcel.Rows[i]["sr_eora_code"]);
                                iData = 1;
                                RowCounter = RowCounter + 1;
                                tempRowcounter = RowCounter;
                            }

                            if (srCode != Convert.ToInt32(dtExcel.Rows[i]["sr_invoice_number"]))
                            {
                                worksheet.Cells[RowCounter, ColCounter++] = iData;
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_log_branch_name"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_grp_name"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_grp_eora_name"];
                                //worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_log_branch_name"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_eora_name"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_invoice_number"];
                                worksheet.Cells[RowCounter, ColCounter++] = Convert.ToDateTime(dtExcel.Rows[i]["sr_invoice_date"]).ToString("dd/MMM/yyyy");
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_order_number"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_invoice_amount"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_advance_amount"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_received_amount"];

                                DataRow[] dr = dtProducts.Select("ProductName='" + dtExcel.Rows[i]["sr_prod_name1"] + "' AND  ProductType='" + dtExcel.Rows[i]["sr_product_type"] + "'");
                                string strNo = dr[0]["SlNo"].ToString();
                                worksheet.Cells[RowCounter, ColCounter + Convert.ToInt32(strNo) - 1] = dtExcel.Rows[i]["sr_qty1"];


                                srCode = Convert.ToInt32(dtExcel.Rows[i]["sr_invoice_number"]);
                            }
                            else if (i != 0)
                            {
                                DataRow[] dr = dtProducts.Select("ProductName='" + dtExcel.Rows[i]["sr_prod_name1"] + "' AND  ProductType='" + dtExcel.Rows[i]["sr_product_type"] + "'");
                                string strNo = dr[0]["SlNo"].ToString();
                                worksheet.Cells[RowCounter, ColCounter + Convert.ToInt32(strNo) - 1] = dtExcel.Rows[i]["sr_qty1"];
                            }
                            if (i == 0)
                            {
                                worksheet.Cells[RowCounter, ColCounter++] = iData;
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_log_branch_name"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_grp_name"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_grp_eora_name"];
                                //worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_log_branch_name"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_eora_name"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_invoice_number"];
                                worksheet.Cells[RowCounter, ColCounter++] = Convert.ToDateTime(dtExcel.Rows[i]["sr_invoice_date"]).ToString("dd/MMM/yyyy");
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_order_number"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_invoice_amount"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_advance_amount"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_received_amount"];


                                DataRow[] dr = dtProducts.Select("ProductName='" + dtExcel.Rows[i]["sr_prod_name1"] + "' AND  ProductType='" + dtExcel.Rows[i]["sr_product_type"] + "'");
                                string strNo = dr[0]["SlNo"].ToString();
                                worksheet.Cells[RowCounter, ColCounter + Convert.ToInt32(strNo) - 1] = dtExcel.Rows[i]["sr_qty1"];
                            }
                            if (i != dtExcel.Rows.Count - 1)
                            {
                                if (srCode != Convert.ToInt32(dtExcel.Rows[i + 1]["sr_invoice_number"]))
                                {
                                    RowCounter++;
                                    iData++;
                                    ColCounter = 1;
                                }
                            }

                            if (i == dtExcel.Rows.Count - 1)
                            {
                                Excel.Range rgHead3 = worksheet.get_Range("C" + (RowCounter + 1) + ":C" + (RowCounter + 1), Type.Missing);
                                rgHead3.Cells.ColumnWidth = 25;
                                rgHead3.Font.Bold = true;
                                rgHead3.Cells.Value2 = " Total";
                                rgHead3.Font.Size = 10;
                                rgHead3.VerticalAlignment = Excel.Constants.xlCenter;

                                for (int iProd = 0; iProd < dtProducts.Rows.Count + 3; iProd++)
                                {
                                    string strCol = objUtilityDB.GetColumnName(8 + iProd + 1);
                                    Excel.Range rgHead4 = worksheet.get_Range(strCol + "" + (RowCounter + 1) + ":" + strCol + "" + (RowCounter + 1), Type.Missing);
                                    rgHead4.Cells.ColumnWidth = 8;
                                    rgHead4.Font.Bold = true;
                                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                                    rgHead4.Font.Size = 10;
                                    rgHead4.Formula = "=Sum(" + strCol + "" + tempRowcounter + ":" + strCol + (RowCounter) + ")";
                                }
                                //rgHead3 = worksheet.get_Range("C" + (RowCounter + 2) + ":C" + (RowCounter + 2), Type.Missing);
                                //rgHead3.Cells.ColumnWidth = 25;
                                //rgHead3.Font.Bold = true;
                                //rgHead3.Cells.Value2 = " Branch Total";
                                //rgHead3.Font.Size = 10;
                                //rgHead3.VerticalAlignment = Excel.Constants.xlCenter;

                                //for (int iProd = 0; iProd < dtProducts.Rows.Count; iProd++)
                                //{
                                //    string strCol = objUtilityDB.GetColumnName(8 + iProd + 1);
                                //    Excel.Range rgHead4 = worksheet.get_Range(strCol + "" + (RowCounter + 2) + ":" + strCol + "" + (RowCounter + 2), Type.Missing);
                                //    rgHead4.Cells.ColumnWidth = 8;
                                //    rgHead4.Font.Bold = true;
                                //    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                                //    rgHead4.Font.Size = 10;
                                //    rgHead4.Formula = "=Sum(" + strCol + "" + tempRowcounter + ":" + strCol + (RowCounter - 1) + ")";
                                //}

                            }
                        }



                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            #endregion

        }
    }
}
