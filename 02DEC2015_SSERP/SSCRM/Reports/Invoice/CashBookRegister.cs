using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSTrans;
using Excel = Microsoft.Office.Interop.Excel;

namespace SSCRM
{
    public partial class CashBookRegister : Form
    {
        private SQLDB objDB = null;
        private string strCashBankId = "";
        private string strType = "", strBookOrReg = "";
        
        public CashBookRegister()
        {
            InitializeComponent();
        }
        public CashBookRegister(string sType,string sBookOrReg)
        {
            InitializeComponent();
            strType = sType;
            strBookOrReg = sBookOrReg;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void CashBookRegister_Load(object sender, EventArgs e)
        {
            //lblname.Text = "";
            dtpInvoiceFromDate.Value = DateTime.Today.AddDays(-1);
           
            cbRegisterType.SelectedIndex = 0;
            rbtnSummary.Checked = true;
            rbTrans.Checked = true;
            if (strBookOrReg == "RECEIPTS")
            {
                //lblAcc.Visible = false;
                lblAcc.Text = "Report Type";
                //cbRegisterType.Visible = false;

                DataTable dt = new DataTable();
                 dt.Columns.Add("type", typeof(string));
                 dt.Columns.Add("name", typeof(string));

                 dt.Rows.Add("Transaction Wise", "Transaction Wise");
                 dt.Rows.Add("Bank A/c Wise", "Bank A/c Wise");
                 cmbAcc.DataSource = dt;
                 cmbAcc.DisplayMember="name";
                 cmbAcc.ValueMember = "type";

            }
            else
            {
                FillCashOrBankAccounts();
            }
            //cbType.SelectedIndex = 0;
            //cbReportType.SelectedIndex = 0;
        }
        private void FillCashOrBankAccounts()
        {
            try
            {

                if (strType == "CASH")
                {
                    string strSQL = "SELECT AM_ACCOUNT_NAME,AM_ACCOUNT_ID FROM FA_ACCOUNT_MASTER WHERE AM_ACCOUNT_TYPE_ID in ('CASH') and am_company_code='SATL'";
                    objDB = new SQLDB();
                    DataTable dt = objDB.ExecuteDataSet(strSQL).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        cmbAcc.DataSource = dt;
                        cmbAcc.DisplayMember = "AM_ACCOUNT_NAME";
                        cmbAcc.ValueMember = "AM_ACCOUNT_ID";
                    }
                }
                else
                {
                    string strSQL = "SELECT AM_ACCOUNT_NAME,AM_ACCOUNT_ID FROM FA_ACCOUNT_MASTER WHERE AM_ACCOUNT_TYPE_ID in ('BANK') AND AM_COMPANY_CODE='SATL'";
                    objDB = new SQLDB();
                    DataTable dt = objDB.ExecuteDataSet(strSQL).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        cmbAcc.DataSource = dt;
                        cmbAcc.DisplayMember = "AM_ACCOUNT_NAME";
                        cmbAcc.ValueMember = "AM_ACCOUNT_ID";
                    }
                }
                //tvDocumentMonth.Nodes.Add("ACCOUNTS", "ACCOUNTS");
                //tvDocumentMonth.Nodes[0].Nodes.Add("CASH", "CASH");
                //string strSQL = "SELECT AM_ACCOUNT_NAME,AM_ACCOUNT_ID FROM FA_ACCOUNT_MASTER WHERE AM_ACCOUNT_TYPE_ID in ('CASH')";
                //objDB = new SQLDB();
                //DataTable dt = objDB.ExecuteDataSet(strSQL).Tables[0];
                //if (dt.Rows.Count > 0)
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        tvDocumentMonth.Nodes[0].Nodes[0].Nodes.Add( dt.Rows[i]["AM_ACCOUNT_ID"].ToString(),dt.Rows[i]["AM_ACCOUNT_NAME"].ToString());
                //    }

                //tvDocumentMonth.Nodes[0].Nodes.Add("BANK", "BANK");

                //string strSQL1 = "SELECT AM_ACCOUNT_NAME,AM_ACCOUNT_ID FROM FA_ACCOUNT_MASTER WHERE AM_ACCOUNT_TYPE_ID in ('BANK')";
                //objDB = new SQLDB();
                //DataTable dt1 = objDB.ExecuteDataSet(strSQL1).Tables[0];
                //if (dt1.Rows.Count > 0)
                //    for (int i = 0; i < dt1.Rows.Count; i++)
                //    {
                //        tvDocumentMonth.Nodes[0].Nodes[1].Nodes.Add(dt1.Rows[i]["AM_ACCOUNT_ID"].ToString(), dt1.Rows[i]["AM_ACCOUNT_NAME"].ToString());
                //    }
                //tvDocumentMonth.ExpandAll();
            }
            catch(Exception EX)
            {
                MessageBox.Show(EX.ToString());
            }
        }

        //private void tvDocumentMonth_AfterCheck(object sender, TreeViewEventArgs e)
        //{
        //    tvDocumentMonth.BeginUpdate();

        //    foreach (TreeNode Node in e.Node.Nodes)
        //    {
        //        Node.Checked = e.Node.Checked;
        //    }

        //    tvDocumentMonth.EndUpdate();
        //}

        private void btnReport_Click(object sender, EventArgs e)
        {


            strCashBankId = "";
            //for (int i = 0; i < tvDocumentMonth.Nodes[0].Nodes.Count; i++)
            //{
            //    for (int j = 0; j < tvDocumentMonth.Nodes[0].Nodes[i].Nodes.Count; j++)
            //    {
            //        if (tvDocumentMonth.Nodes[0].Nodes[i].Nodes[j].Checked == true)
            //        {
            //            if (strCashBankId != string.Empty)
            //                strCashBankId += ",";
            //            strCashBankId +=  tvDocumentMonth.Nodes[0].Nodes[i].Nodes[j].Name.ToString();
            //            //iscomp = true;
            //        }
            //    }
            //}
            if(CheckData())
            {
            if (strBookOrReg == "BOOK")
            {
                CommonData.ViewReport = "SATL_REP_CBBOOK_Report";
                ReportViewer objReportview = new ReportViewer(dtpInvoiceFromDate.Value.ToString("dd/MMM/yyyy"), dtpInvoiceToDate.Value.ToString("dd/MMM/yyyy"), cmbAcc.SelectedValue.ToString(), "SUMMARY");
                objReportview.Show();
            }
            if (strBookOrReg == "RECEIPTS")
            {
                if (cmbAcc.SelectedValue == "Bank A/c Wise")
                {
                    CommonData.ViewReport = "SATL_CASHBANK_REG";
                    ReportViewer objReportview = new ReportViewer(dtpInvoiceFromDate.Value.ToString("dd/MMM/yyyy"), dtpInvoiceToDate.Value.ToString("dd/MMM/yyyy"), cmbAcc.SelectedValue.ToString(), "SUMMARY");
                    objReportview.Show();
                }
                if (cmbAcc.SelectedValue == "Transaction Wise")
                {
                    CommonData.ViewReport = "SATL_CASH_REG_TRN_WISE";
                    ReportViewer objReportview = new ReportViewer(dtpInvoiceFromDate.Value.ToString("dd/MMM/yyyy"), dtpInvoiceToDate.Value.ToString("dd/MMM/yyyy"), cmbAcc.SelectedValue.ToString(), "SUMMARY");
                    objReportview.Show();
                }
            }
            }
        }
        private bool CheckData()
        {
            bool blValue = true;
            //if (strCashBankId.Length == 0)
            //{
            //    MessageBox.Show("Please Select Any Account", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    blValue = false;
            //    return blValue;
            //}
            if (rbTrans.Checked == false && rbTrnPeriod.Checked == false)
            {
                MessageBox.Show("Please Select Transaction Dates", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                blValue = false;
                return blValue;
            }
            return blValue;
        }

        private void rbTrans_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTrans.Checked == true)
            {
                dtpInvoiceFromDate.Enabled = true;
                dtpInvoiceToDate.Enabled = true;
            }
            else
            {
                dtpInvoiceFromDate.Enabled = false;
                dtpInvoiceToDate.Enabled = false;
            }
        }

        private void rbTrnPeriod_CheckedChanged(object sender, EventArgs e)
        {

            if(rbTrnPeriod.Checked==true)
            {
                cmbTranPeriod.Enabled = true;
                cmbTranPeriod.SelectedIndex = 0;
            }
            else
            {
                cmbTranPeriod.Enabled = false;
            }
        }

        private void cmbTranPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);       
           

            //if(cmbTranPeriod.SelectedIndex==0)
            //{
            //    var lastMFirstDate = month.AddMonths(-1);
            //    var lastMLastDate = month.AddDays(-1);
            //    dtpInvoiceFromDate.Value = lastMFirstDate;
            //    dtpInvoiceToDate.Value = lastMLastDate;
            //}
            //if (cmbTranPeriod.SelectedIndex == 1)
            //{
            //    var lastMFirstDate = month.AddMonths(-2);
            //    var lastMLastDate = month.AddDays(-1);
            //    dtpInvoiceFromDate.Value = lastMFirstDate;
            //    dtpInvoiceToDate.Value = lastMLastDate;
            //}
            //if (cmbTranPeriod.SelectedIndex == 2)
            //{
            //    var lastMFirstDate = month.AddMonths(-3);
            //    var lastMLastDate = month.AddDays(-1);
            //    dtpInvoiceFromDate.Value = lastMFirstDate;
            //    dtpInvoiceToDate.Value = lastMLastDate;
            //}
            if (cmbTranPeriod.SelectedIndex >=0 )
            {
                var lastMFirstDate = month.AddMonths(-(Convert.ToInt32(cmbTranPeriod.SelectedItem.ToString())));
                var lastMLastDate = month.AddDays(-1);
                dtpInvoiceFromDate.Value = lastMFirstDate;
                dtpInvoiceToDate.Value = lastMLastDate;
            }

        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            //excelDwnldCollectReg();
        }
        private void excelDwnldCollectReg()
        {

            try
            {
                DataTable dtEx = new DataTable();
                ExcelDB objExDb = new ExcelDB();
                dtEx = objExDb.GetDlCollReg(CommonData.CompanyCode, CommonData.BranchCode, dtpInvoiceFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpInvoiceToDate.Value.ToString("dd/MMM/yyyy"), "ALL", "").Tables[0];
                if (dtEx.Rows.Count > 0)
                {
                    Excel.Application oXL = new Excel.Application();
                    Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                    Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                    worksheet.Name = CommonData.DocMonth.ToUpper();
                    oXL.Visible = true;
                    Excel.Range rgHead = worksheet.get_Range("A1", "VP1");
                    rgHead.Font.Size = 14;
                    rgHead.Cells.MergeCells = true;
                    rgHead.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead.Font.Bold = true;
                    rgHead.Font.ColorIndex = 30;
                    rgHead.Borders.Weight = 2;

                    Excel.Range rg = worksheet.get_Range("A2", "S2");
                    Excel.Range rgData = worksheet.get_Range("A3", "S2" + (dtEx.Rows.Count + 2).ToString());
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

                    rgHead = worksheet.get_Range("A1", "S1");
                    rgHead.Cells.ColumnWidth = 5;
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
                    rg.Cells.ColumnWidth = 7;
                    rg.Cells.Value2 = "TRN Date";

                    rg = worksheet.get_Range("F2", Type.Missing);
                    rg.Cells.ColumnWidth = 7;
                    rg.Cells.Value2 = "Transaction No";

                    rg = worksheet.get_Range("G2", Type.Missing);
                    rg.Cells.ColumnWidth = 7;
                    rg.Cells.Value2 = "Ref No";


                    rg = worksheet.get_Range("H2", Type.Missing);
                    rg.Cells.ColumnWidth = 7;
                    rg.Cells.Value2 = "Dealer Code";

                    rg = worksheet.get_Range("I2", Type.Missing);
                    rg.Cells.ColumnWidth = 13;
                    rg.Cells.Value2 = "Firm Name";

                    rg = worksheet.get_Range("J2", Type.Missing);
                    rg.Cells.ColumnWidth = 7;
                    rg.Cells.Value2 = "Dealer Village";

                    rg = worksheet.get_Range("K2", Type.Missing);
                    rg.Cells.ColumnWidth = 10;
                    rg.Cells.Value2 = "Dealer Mandal";

                    rg = worksheet.get_Range("L2", Type.Missing);
                    rg.Cells.ColumnWidth = 10;
                    rg.Cells.Value2 = "Dealer Distrcit";

                    rg = worksheet.get_Range("M2", Type.Missing);
                    rg.Cells.ColumnWidth = 10;
                    rg.Cells.Value2 = "Dealer State";

                    rg = worksheet.get_Range("N2", Type.Missing);
                    rg.Cells.ColumnWidth = 10;
                    rg.Cells.Value2 = "Payment Mode";

                    rg = worksheet.get_Range("O2", Type.Missing);
                    rg.Cells.ColumnWidth = 10;
                    rg.Cells.Value2 = "Cheq/DD No";

                    rg = worksheet.get_Range("P2", Type.Missing);
                    rg.Cells.ColumnWidth = 20;
                    rg.Cells.Value2 = "Collected By";

                    rg = worksheet.get_Range("Q2", Type.Missing);
                    rg.Cells.ColumnWidth = 20;
                    rg.Cells.Value2 = "Received Amount";

                    rg = worksheet.get_Range("R2", Type.Missing);
                    rg.Cells.ColumnWidth = 20;
                    rg.Cells.Value2 = "Remarks";


                    rg = worksheet.get_Range("S2", Type.Missing);
                    rg.Cells.ColumnWidth = 20;
                    rg.Cells.Value2 = "Entered By";

                     int RowCounter = 2;
                    foreach (DataRow dr in dtEx.Rows)
                    {
                        int i = 1;
                        worksheet.Cells[RowCounter + 1, i++] = RowCounter - 1;
                        worksheet.Cells[RowCounter + 1, i++] = dr["Company_Name"].ToString();
                        worksheet.Cells[RowCounter + 1, i++] = dr["Branch_Name"].ToString();
                        worksheet.Cells[RowCounter + 1, i++] = dr["FinYear"].ToString();
                        worksheet.Cells[RowCounter + 1, i++] = Convert.ToDateTime(dr["ReceivedDate"]).ToString("dd/MMM/yyyy");
                        worksheet.Cells[RowCounter + 1, i++] = dr["VoucherId"].ToString();
                        worksheet.Cells[RowCounter + 1, i++] = dr["ReferenceNo"].ToString();
                        worksheet.Cells[RowCounter + 1, i++] = dr["DealerCode"].ToString();
                        worksheet.Cells[RowCounter + 1, i++] = dr["FirmName"].ToString();
                        worksheet.Cells[RowCounter + 1, i++] = dr["DealerVillage"].ToString();
                        worksheet.Cells[RowCounter + 1, i++] = dr["DealerMandal"].ToString();
                        worksheet.Cells[RowCounter + 1, i++] = dr["DealerDistrict"].ToString();
                        worksheet.Cells[RowCounter + 1, i++] = dr["DealerSate"].ToString();

                        worksheet.Cells[RowCounter + 1, i++] = dr["PaymentMode"].ToString();

                        worksheet.Cells[RowCounter + 1, i++] = dr["CheqOrDDNo"].ToString();
                        worksheet.Cells[RowCounter + 1, i++] = dr["CollectedBy"].ToString();

                        worksheet.Cells[RowCounter + 1, i++] = dr["ReceivedAmount"].ToString();
                        worksheet.Cells[RowCounter + 1, i++] = dr["Remarks"].ToString();
                        worksheet.Cells[RowCounter + 1, i++] = dr["EnteredBy"].ToString();
                        RowCounter++;
                    }
                    //worksheet.Cells[RowCounter + 2, 22] = "=SUM(V3:V" + (dtEx.Rows.Count + 2) + ")";
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.ToString());
                    //}
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

       
       

        //private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if(cbType.SelectedItem=="CASH")
        //    {

        //    }
        //    if (cbType.SelectedItem == "BANK")
        //    {

        //    }
        //    if (cbType.SelectedItem == "BANK")
        //    {
        //    }
        //}
    }
}
