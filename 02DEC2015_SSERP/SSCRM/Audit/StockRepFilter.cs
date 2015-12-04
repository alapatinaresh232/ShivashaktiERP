using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SSCRMDB;
using SSTrans;
using SSAdmin;
using Excel = Microsoft.Office.Interop.Excel;

namespace SSCRM
{
    public partial class StockRepFilter : Form
    {
        AuditDB objAuditDB = null;
        SQLDB objSQLdb = null;
        ExcelDB objExDb = null;
        UtilityDB objUtilityDB = null;

        string strBranTypes = "", Branches = "", Company = "", DocumentMonth = "";
        ReportViewer childReportViewer;

        public StockRepFilter()
        {
            InitializeComponent();
        }

        private void StockRepFilter_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Now;
            dtpToDate.Value = DateTime.Now;
            cbReportType.SelectedIndex = 0;
            FillBranchTypes();
            FillBranches();
        }


        private void FillBranches()
        {
            tvBranches.Nodes.Clear();
            objAuditDB = new AuditDB();
            DataSet ds = new DataSet();
            chkAll.Visible = false;
            ds = objAuditDB.GetAuditBranchRegions("", "", "", "", CommonData.LogUserId, "PARENT");
            TreeNode tNode;
            if (strBranTypes.Length >= 2)
            {
                try
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        chkAll.Visible = true;

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            tvBranches.Nodes.Add(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), ds.Tables[0].Rows[i]["COMPANY_NAME"].ToString());
                            DataSet dschild = new DataSet();
                            dschild = objAuditDB.GetAuditBranchRegions(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), strBranTypes, "", "", "", "ZONES");
                            if (dschild.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                                {
                                    tvBranches.Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["ABM_STATE"].ToString(), dschild.Tables[0].Rows[j]["ABM_STATE"].ToString());

                                    DataSet dschild1 = new DataSet();
                                    dschild1 = objAuditDB.GetAuditBranchRegions(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), strBranTypes, dschild.Tables[0].Rows[j]["ABM_STATE"].ToString(), "", "", "REGIONS");
                                    if (dschild1.Tables[0].Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dschild1.Tables[0].Rows.Count; k++)
                                        {
                                            tvBranches.Nodes[i].Nodes[j].Nodes.Add(dschild1.Tables[0].Rows[k]["ABM_REGION"].ToString(), dschild1.Tables[0].Rows[k]["ABM_REGION"].ToString());

                                            DataSet dschild2 = new DataSet();


                                            dschild2 = objAuditDB.GetAuditBranchRegions(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), strBranTypes, dschild.Tables[0].Rows[j]["ABM_STATE"].ToString(), dschild1.Tables[0].Rows[k]["ABM_REGION"].ToString(), CommonData.LogUserId, "BRANCHES");

                                            if (dschild2.Tables[0].Rows.Count > 0)
                                            {
                                                for (int ivar = 0; ivar < dschild2.Tables[0].Rows.Count; ivar++)
                                                {
                                                    tvBranches.Nodes[i].Nodes[j].Nodes[k].Nodes.Add(dschild2.Tables[0].Rows[ivar]["BranchCode"].ToString(), dschild2.Tables[0].Rows[ivar]["BranchName"].ToString());
                                                }
                                            }

                                        }
                                    }
                                }
                            }

                        }
                    }


                    for (int i = 0; i < tvBranches.Nodes.Count; i++)
                    {
                        for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
                        {
                            if (tvBranches.Nodes[i].Nodes[j].Nodes.Count > 0)
                                tvBranches.Nodes[i].FirstNode.Expand();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void FillBranchTypes()
        {
            objSQLdb = new SQLDB();
            DataTable dt = new DataTable();
            string strCommand = "";
            //clbBranchTypes.Items.Clear();
            try
            {
                strCommand = "SELECT Distinct(BRANCH_TYPE) BranchType " +
                             ", CASE WHEN BRANCH_TYPE='SP' THEN 'STOCK POINT' " +
                             "  WHEN BRANCH_TYPE='PU' THEN 'PRODUCTION UNIT' " +
                             "  WHEN BRANCH_TYPE='TR' THEN 'TRANSPORT UNIT' " +
                             "  ELSE '' END BranchTypeName " +
                             "  FROM BRANCH_MAS WHERE BRANCH_TYPE not in ('PO','HO','BR','OL','ST','RS','WH')";
                dt = objSQLdb.ExecuteDataSet(strCommand).Tables[0];

                if (dt.Rows.Count > 0)
                {

                    DataRow dr = dt.NewRow();
                    dr[0] = "--Select--";
                    dr[1] = "--Select--";

                    dt.Rows.InsertAt(dr, 0);
                    cbBranchType.DataSource = dt;
                    cbBranchType.DisplayMember = "BranchTypeName";
                    cbBranchType.ValueMember = "BranchType";

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

        private void cbBranchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBranchType.SelectedIndex > 0)
            {
                strBranTypes = "";
                strBranTypes = cbBranchType.SelectedValue.ToString();
                FillBranches();
            }

        }

        private void GetSelectedValues()
        {
            Branches = ""; Company = "";

            bool iscomp = false;

            for (int ivar = 0; ivar < tvBranches.Nodes.Count; ivar++)
            {
                for (int k = 0; k < tvBranches.Nodes[ivar].Nodes.Count; k++)
                {
                    for (int i = 0; i < tvBranches.Nodes[ivar].Nodes[k].Nodes.Count; i++)
                    {
                        for (int j = 0; j < tvBranches.Nodes[ivar].Nodes[k].Nodes[i].Nodes.Count; j++)
                        {
                            if (tvBranches.Nodes[ivar].Nodes[k].Nodes[i].Nodes[j].Checked == true)
                            {
                                if (Branches != string.Empty)
                                    Branches += ",";
                                Branches += tvBranches.Nodes[ivar].Nodes[k].Nodes[i].Nodes[j].Name.ToString();
                                iscomp = true;
                            }
                        }
                    }
                }

                if (iscomp == true)
                {
                    if (Company != string.Empty)
                        Company += ",";
                    Company += tvBranches.Nodes[ivar].Name.ToString();
                }
                iscomp = false;
            }

        }

        private bool CheckData()
        {
            bool flag = true;

            GetSelectedValues();

            if (cbBranchType.SelectedIndex == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Location Type", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }
            if (Company.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Atleast One Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }
            if (Branches.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Atleast One Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return flag;
            }

            return flag;

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                if (cbReportType.SelectedIndex == 0)
                {
                    CommonData.ViewReport = "SSERP_REP_SP_PHY_STK_CLOS_STATUS";
                    childReportViewer = new ReportViewer(Company, Branches, Convert.ToDateTime(dtpFromDate.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy"), "CLOS_STK");
                    childReportViewer.Show();
                }
                if (cbReportType.SelectedIndex == 1)
                {
                    CommonData.ViewReport = "SSERP_REP_AUDIT_PHY_CNT_STK_DIFF";
                    childReportViewer = new ReportViewer(Company, Branches, Convert.ToDateTime(dtpFromDate.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy"), "STK_DIFF");
                    childReportViewer.Show();
                }
                if (cbReportType.SelectedIndex == 2)
                {
                    CommonData.ViewReport = "SSERP_REP_AUDIT_PHY_STK_NON_CNT_PROD_DETL";
                    childReportViewer = new ReportViewer(Company, Branches, Convert.ToDateTime(dtpFromDate.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy"), "NON_COUNT_PROD_DETL");
                    childReportViewer.Show();
                }
                if (cbReportType.SelectedIndex == 3)
                {
                    CommonData.ViewReport = "SSERP_REP_AUDIT_PHY_STK_CNT_PLANTS_DETL";
                    childReportViewer = new ReportViewer(Company, Branches, Convert.ToDateTime(dtpFromDate.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy"), "PLANTS");
                    childReportViewer.Show();
                }
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbBranchType.SelectedIndex = 0;
            tvBranches.Nodes.Clear();
            dtpFromDate.Value = DateTime.Today;
            dtpToDate.Value = DateTime.Today;

        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                GetSelectedValues();
                GetDocumentMonths();
                DataTable dtExcel = new DataTable();
                objExDb = new ExcelDB();
                objUtilityDB = new UtilityDB();

                #region "Audit Physical Stk Cnt Closing Stk Status "

                if (cbReportType.SelectedIndex == 0)
                {
                    dtExcel = objExDb.Get_AuditPhyStkCntDetl(Company, Branches, Convert.ToDateTime(dtpFromDate.Value).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(dtpToDate.Value).ToString("MMMyyyy").ToUpper(), "CLOS_STK").Tables[0];
                    objExDb = null;
                    if (dtExcel.Rows.Count > 0)
                    {
                        try
                        {
                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;
                            int iTotColumns = 0;
                            iTotColumns = 6 + (4 * Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Dates"]));
                            string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                            Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                            Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Branchs"]) + 3).ToString());
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
                            rgData = worksheet.get_Range("A4", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Branchs"]) + 3).ToString());
                            rgData.WrapText = false;
                            rg = worksheet.get_Range("A3", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg = worksheet.get_Range("B3", Type.Missing);
                            rg.Cells.ColumnWidth = 40;
                            rg = worksheet.get_Range("C3", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("D3", Type.Missing);
                            rg.Cells.ColumnWidth = 20;
                            rg = worksheet.get_Range("E3", Type.Missing);
                            rg.Cells.ColumnWidth = 40;
                            rg = worksheet.get_Range("F3", Type.Missing);
                            rg.Cells.ColumnWidth = 25;
                            rg.WrapText = true;


                            int iColumn = 1;
                            worksheet.Cells[3, iColumn++] = "SlNo";
                            worksheet.Cells[3, iColumn++] = "Company";
                            worksheet.Cells[3, iColumn++] = "Zone";
                            worksheet.Cells[3, iColumn++] = "Region";
                            worksheet.Cells[3, iColumn++] = "Branch";
                            worksheet.Cells[3, iColumn++] = "Product Name";

                            Excel.Range rgHead;
                            int iStartColumn = 0;
                            for (int iProd = 0; iProd < Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Dates"]); iProd++)
                            {
                                rgHead = worksheet.get_Range("A1", "F2");
                                rgHead.Merge(Type.Missing);
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.RowHeight = 30;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "Closing Stock Status \t  From  " + (dtpFromDate.Value).ToString("dd/MMM/yyyy").ToUpper() + " \t  To  " + (dtpToDate.Value).ToString("dd/MMM/yyyy").ToUpper() + " ";


                                iStartColumn = (4 * iProd) + 7;

                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 3) + "2");


                                rgHead.Merge(Type.Missing);
                                rgHead.Interior.ColorIndex = 34 + 1;
                                rgHead.Borders.Weight = 2;
                                rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                                rgHead.Cells.RowHeight = 30;
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;

                                rgHead.Cells.ColumnWidth = 15;

                                worksheet.Cells[3, iStartColumn++] = "As Per Book Good";
                                worksheet.Cells[3, iStartColumn++] = "As Per Bok Damage";
                                worksheet.Cells[3, iStartColumn++] = "As Per Cnt Good";
                                worksheet.Cells[3, iStartColumn++] = "As Per Cnt Damage";


                            }

                            int iRowCounter = 4; int iColumnCounter = 1;
                            for (int i = 0; i < dtExcel.Rows.Count; i++)
                            {
                                if (i > 0)
                                {

                                    if ((dtExcel.Rows[i]["sp_Bran_Code"].ToString() == dtExcel.Rows[i - 1]["sp_Bran_Code"].ToString())
                                        && (dtExcel.Rows[i]["sp_Product_Id"].ToString() == dtExcel.Rows[i - 1]["sp_Product_Id"].ToString()))
                                    {
                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sp_Sl_No"]);
                                        //int iStartColumn = 0;
                                        iColumnCounter = (4 * (iMonthNo - 1)) + 7;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 3) + "2");
                                        rgHead.Cells.Value2 = Convert.ToDateTime(dtExcel.Rows[i]["sp_Trn_Date"]).ToString("dd/MMM/yyyy");
                                        rgHead.WrapText = true;

                                        rgHead.Interior.ColorIndex = 34 + 1;
                                        rgHead.Font.ColorIndex = 1;
                                        rgHead.Font.Bold = true;
                                        rgHead.Borders.Weight = 2;
                                        //rgHead.Interior.ColorIndex = 31;
                                        //rgHead.Font.ColorIndex = 2;
                                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;


                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Good_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Dmg_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_cnt_Good_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_cnt_Dmg_Qty"];

                                    }

                                    else
                                    {

                                        iRowCounter++;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_comp_name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Zone"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Region"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Branch_Name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Product_Name"];


                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sp_Sl_No"]);

                                        iColumnCounter = (4 * (iMonthNo - 1)) + 7;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 3) + "2");
                                        rgHead.Cells.Value2 = Convert.ToDateTime(dtExcel.Rows[i]["sp_Trn_Date"]).ToString("dd/MMM/yyyy");
                                        rgHead.WrapText = true;

                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Good_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Dmg_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_cnt_Good_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_cnt_Dmg_Qty"];


                                    }
                                }
                                else
                                {

                                    worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_comp_name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Zone"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Region"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Branch_Name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Product_Name"];

                                    int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sp_Sl_No"]);
                                    //int iStartColumn = 0;
                                    iColumnCounter = (4 * (iMonthNo - 1)) + 7;
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 3) + "2");
                                    rgHead.Cells.Value2 = Convert.ToDateTime(dtExcel.Rows[i]["sp_Trn_Date"]).ToString("dd/MMM/yyyy");
                                    rgHead.WrapText = true;

                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Good_Qty"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Dmg_Qty"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_cnt_Good_Qty"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_cnt_Dmg_Qty"];

                                }

                                iColumnCounter = 1;
                            }


                            rgHead = worksheet.get_Range("A3", sLastColumn + (worksheet.UsedRange.Rows.Count).ToString());
                            rgHead.Font.Size = 11;
                            // rgHead.WrapText = true;                          
                            rgHead.Borders.Weight = 2;


                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }

                #endregion

                #region "Audit Physical Stk Cnt Difference"

                if (cbReportType.SelectedIndex == 1)
                {
                    dtExcel = objExDb.Get_AuditPhyStkCntDetl(Company, Branches, Convert.ToDateTime(dtpFromDate.Value).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(dtpToDate.Value).ToString("MMMyyyy").ToUpper(), "STK_DIFF").Tables[0];
                    objExDb = null;
                    if (dtExcel.Rows.Count > 0)
                    {
                        try
                        {
                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;
                            int iTotColumns = 0;
                            iTotColumns = 6 + (3 * Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Dates"]));
                            string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                            Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                            Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Branchs"]) + 3).ToString());
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
                            rgData = worksheet.get_Range("A4", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Branchs"]) + 3).ToString());
                            rgData.WrapText = false;
                            rg = worksheet.get_Range("A3", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg = worksheet.get_Range("B3", Type.Missing);
                            rg.Cells.ColumnWidth = 40;
                            rg = worksheet.get_Range("C3", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("D3", Type.Missing);
                            rg.Cells.ColumnWidth = 20;
                            rg = worksheet.get_Range("E3", Type.Missing);
                            rg.Cells.ColumnWidth = 40;
                            rg = worksheet.get_Range("F3", Type.Missing);
                            rg.Cells.ColumnWidth = 25;
                            rg.WrapText = true;


                            int iColumn = 1;
                            worksheet.Cells[3, iColumn++] = "SlNo";
                            worksheet.Cells[3, iColumn++] = "Company";
                            worksheet.Cells[3, iColumn++] = "Zone";
                            worksheet.Cells[3, iColumn++] = "Region";
                            worksheet.Cells[3, iColumn++] = "Branch";
                            worksheet.Cells[3, iColumn++] = "Product Name";

                            Excel.Range rgHead;
                            int iStartColumn = 0;
                            for (int iProd = 0; iProd < Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Dates"]); iProd++)
                            {
                                rgHead = worksheet.get_Range("A1", "F2");
                                rgHead.Merge(Type.Missing);
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.RowHeight = 30;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "Physical Stock Difference \t  From  " + (dtpFromDate.Value).ToString("dd/MMM/yyyy").ToUpper() + " \t  To  " + (dtpToDate.Value).ToString("dd/MMM/yyyy").ToUpper() + " ";


                                iStartColumn = (3 * iProd) + 7;

                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 2) + "2");


                                rgHead.Merge(Type.Missing);
                                rgHead.Interior.ColorIndex = 34 + 1;
                                rgHead.Borders.Weight = 2;
                                rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                                rgHead.Cells.RowHeight = 30;
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.WrapText = true;
                                rgHead.Cells.ColumnWidth = 10;

                                worksheet.Cells[3, iStartColumn++] = "As Per Book";
                                worksheet.Cells[3, iStartColumn++] = "As Per Counting";
                                worksheet.Cells[3, iStartColumn++] = "Diff.";


                            }

                            int iRowCounter = 4; int iColumnCounter = 1;
                            for (int i = 0; i < dtExcel.Rows.Count; i++)
                            {
                                if (i > 0)
                                {

                                    if ((dtExcel.Rows[i]["sp_Bran_Code"].ToString() == dtExcel.Rows[i - 1]["sp_Bran_Code"].ToString())
                                        && (dtExcel.Rows[i]["sp_Product_Id"].ToString() == dtExcel.Rows[i - 1]["sp_Product_Id"].ToString()))
                                    {
                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sp_Sl_No"]);
                                        //int iStartColumn = 0;
                                        iColumnCounter = (3 * (iMonthNo - 1)) + 7;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 2) + "2");
                                        rgHead.Cells.Value2 = Convert.ToDateTime(dtExcel.Rows[i]["sp_Trn_Date"]).ToString("dd/MMM/yyyy");
                                        rgHead.WrapText = true;

                                        rgHead.Interior.ColorIndex = 34 + 1;
                                        rgHead.Font.ColorIndex = 1;
                                        rgHead.Font.Bold = true;
                                        rgHead.Borders.Weight = 2;
                                        //rgHead.Interior.ColorIndex = 31;
                                        //rgHead.Font.ColorIndex = 2;
                                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;


                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Book_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Cnt_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Diff_Tot_Qty"];
                                    }

                                    else
                                    {

                                        iRowCounter++;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_comp_name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Zone"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Region"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Branch_Name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Product_Name"];


                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sp_Sl_No"]);

                                        iColumnCounter = (3 * (iMonthNo - 1)) + 7;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 2) + "2");
                                        rgHead.Cells.Value2 = Convert.ToDateTime(dtExcel.Rows[i]["sp_Trn_Date"]).ToString("dd/MMM/yyyy");
                                        rgHead.WrapText = true;

                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Book_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Cnt_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Diff_Tot_Qty"];


                                    }
                                }
                                else
                                {

                                    worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_comp_name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Zone"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Region"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Branch_Name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Product_Name"];

                                    int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sp_Sl_No"]);
                                    //int iStartColumn = 0;
                                    iColumnCounter = (3 * (iMonthNo - 1)) + 7;
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 2) + "2");
                                    rgHead.Cells.Value2 = Convert.ToDateTime(dtExcel.Rows[i]["sp_Trn_Date"]).ToString("dd/MMM/yyyy");
                                    rgHead.WrapText = true;

                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Book_Qty"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Cnt_Qty"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Diff_Tot_Qty"];


                                }

                                iColumnCounter = 1;
                            }


                            rgHead = worksheet.get_Range("A3", sLastColumn + (worksheet.UsedRange.Rows.Count).ToString());
                            rgHead.Font.Size = 11;
                            // rgHead.WrapText = true;                          
                            rgHead.Borders.Weight = 2;


                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
                #endregion

                #region "Audit Physical Stk Non Cnt Product Detl"

                if (cbReportType.SelectedIndex == 2)
                {
                    dtExcel = objExDb.Get_AuditPhyStkCntDetl(Company, Branches, Convert.ToDateTime(dtpFromDate.Value).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(dtpToDate.Value).ToString("MMMyyyy").ToUpper(), "NON_COUNT_PROD_DETL").Tables[0];
                    objExDb = null;
                    if (dtExcel.Rows.Count > 0)
                    {
                        try
                        {
                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;
                            int iTotColumns = 0;
                            iTotColumns = 6 + (2 * Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Dates"]));
                            string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                            Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                            Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Branchs"]) + 3).ToString());
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
                            rgData = worksheet.get_Range("A4", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Branchs"]) + 3).ToString());
                            rgData.WrapText = false;
                            rg = worksheet.get_Range("A3", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg = worksheet.get_Range("B3", Type.Missing);
                            rg.Cells.ColumnWidth = 40;
                            rg = worksheet.get_Range("C3", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("D3", Type.Missing);
                            rg.Cells.ColumnWidth = 20;
                            rg = worksheet.get_Range("E3", Type.Missing);
                            rg.Cells.ColumnWidth = 40;
                            rg = worksheet.get_Range("F3", Type.Missing);
                            rg.Cells.ColumnWidth = 25;
                            rg.WrapText = true;


                            int iColumn = 1;
                            worksheet.Cells[3, iColumn++] = "SlNo";
                            worksheet.Cells[3, iColumn++] = "Company";
                            worksheet.Cells[3, iColumn++] = "Zone";
                            worksheet.Cells[3, iColumn++] = "Region";
                            worksheet.Cells[3, iColumn++] = "Branch";
                            worksheet.Cells[3, iColumn++] = "Product Name";

                            Excel.Range rgHead;
                            int iStartColumn = 0;
                            for (int iProd = 0; iProd < Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Dates"]); iProd++)
                            {
                                rgHead = worksheet.get_Range("A1", "F2");
                                rgHead.Merge(Type.Missing);
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.RowHeight = 30;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "Non Counting Product Details \t  From  " + (dtpFromDate.Value).ToString("dd/MMM/yyyy").ToUpper() + " \t  To  " + (dtpToDate.Value).ToString("dd/MMM/yyyy").ToUpper() + " ";


                                iStartColumn = (2 * iProd) + 7;

                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 1) + "2");


                                rgHead.Merge(Type.Missing);
                                rgHead.Interior.ColorIndex = 34 + 1;
                                rgHead.Borders.Weight = 2;
                                rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                                rgHead.Cells.RowHeight = 30;
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.WrapText = true;
                                rgHead.Cells.ColumnWidth = 10;

                                worksheet.Cells[3, iStartColumn++] = "Book Stock";
                                worksheet.Cells[3, iStartColumn++] = "Letter Coll. Or Not";
                            }

                            int iRowCounter = 4; int iColumnCounter = 1;
                            for (int i = 0; i < dtExcel.Rows.Count; i++)
                            {
                                if (i > 0)
                                {

                                    if ((dtExcel.Rows[i]["sp_Bran_Code"].ToString() == dtExcel.Rows[i - 1]["sp_Bran_Code"].ToString())
                                        && (dtExcel.Rows[i]["sp_Product_Id"].ToString() == dtExcel.Rows[i - 1]["sp_Product_Id"].ToString()))
                                    {
                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sp_Sl_No"]);
                                        //int iStartColumn = 0;
                                        iColumnCounter = (2 * (iMonthNo - 1)) + 7;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 1) + "2");
                                        rgHead.Cells.Value2 = Convert.ToDateTime(dtExcel.Rows[i]["sp_Trn_Date"]).ToString("dd/MMM/yyyy");
                                        rgHead.WrapText = true;

                                        rgHead.Interior.ColorIndex = 34 + 1;
                                        rgHead.Font.ColorIndex = 1;
                                        rgHead.Font.Bold = true;
                                        rgHead.Borders.Weight = 2;
                                        //rgHead.Interior.ColorIndex = 31;
                                        //rgHead.Font.ColorIndex = 2;
                                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;


                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Book_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Letter_Coll_Flag"];

                                    }

                                    else
                                    {

                                        iRowCounter++;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_comp_name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Zone"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Region"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Branch_Name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Product_Name"];


                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sp_Sl_No"]);

                                        iColumnCounter = (2 * (iMonthNo - 1)) + 7;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 1) + "2");
                                        rgHead.Cells.Value2 = Convert.ToDateTime(dtExcel.Rows[i]["sp_Trn_Date"]).ToString("dd/MMM/yyyy");
                                        rgHead.WrapText = true;

                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Book_Qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Letter_Coll_Flag"];

                                    }
                                }
                                else
                                {

                                    worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_comp_name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Zone"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Region"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Branch_Name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Product_Name"];

                                    int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sp_Sl_No"]);
                                    //int iStartColumn = 0;
                                    iColumnCounter = (2 * (iMonthNo - 1)) + 7;
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 1) + "2");
                                    rgHead.Cells.Value2 = Convert.ToDateTime(dtExcel.Rows[i]["sp_Trn_Date"]).ToString("dd/MMM/yyyy");
                                    rgHead.WrapText = true;

                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Tot_Book_Qty"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Letter_Coll_Flag"];


                                }

                                iColumnCounter = 1;
                            }


                            rgHead = worksheet.get_Range("A3", sLastColumn + (worksheet.UsedRange.Rows.Count).ToString());
                            rgHead.Font.Size = 11;
                            // rgHead.WrapText = true;                          
                            rgHead.Borders.Weight = 2;
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
                #endregion

                #region "Audit Physical Stk Cnt Plants Destroy Details"

                if (cbReportType.SelectedIndex == 3)
                {
                    dtExcel = objExDb.Get_AuditPhyStkCntDetl(Company, Branches, Convert.ToDateTime(dtpFromDate.Value).ToString("dd/MMM/yyyy").ToUpper(), Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy").ToUpper(), "PLANTS").Tables[0];
                    objExDb = null;
                    if (dtExcel.Rows.Count > 0)
                    {
                        try
                        {
                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;
                            int iTotColumns = 0;
                            iTotColumns = 6 + (2 * Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Dates"]));
                            string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                            Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                            Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Branchs"])).ToString());                           
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
                            rgData = worksheet.get_Range("A4", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Branchs"]) + 3).ToString());
                            rgData.WrapText = false;
                            rg = worksheet.get_Range("A3", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg = worksheet.get_Range("B3", Type.Missing);
                            rg.Cells.ColumnWidth = 40;
                            rg = worksheet.get_Range("C3", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("D3", Type.Missing);
                            rg.Cells.ColumnWidth = 20;
                            rg = worksheet.get_Range("E3", Type.Missing);
                            rg.Cells.ColumnWidth = 40;
                            rg = worksheet.get_Range("F3", Type.Missing);
                            rg.Cells.ColumnWidth = 25;
                            rg.WrapText = true;


                            int iColumn = 1;
                            worksheet.Cells[3, iColumn++] = "SlNo";
                            worksheet.Cells[3, iColumn++] = "Company";
                            worksheet.Cells[3, iColumn++] = "Zone";
                            worksheet.Cells[3, iColumn++] = "Region";
                            worksheet.Cells[3, iColumn++] = "Branch";
                            worksheet.Cells[3, iColumn++] = "Product Name";

                            Excel.Range rgHead;
                            int iStartColumn = 0;
                            for (int iProd = 0; iProd < Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Dates"]); iProd++)
                            {
                                rgHead = worksheet.get_Range("A1", "F2");
                                rgHead.Merge(Type.Missing);
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.RowHeight = 30;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "Destroy Plants\t  From  " + (dtpFromDate.Value).ToString("dd/MMM/yyyy").ToUpper() + " \t  To  " + (dtpToDate.Value).ToString("dd/MMM/yyyy").ToUpper() + " ";


                                iStartColumn = (2 * iProd) + 7;

                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 1) + "2");


                                rgHead.Merge(Type.Missing);
                                rgHead.Interior.ColorIndex = 34 + 1;
                                rgHead.Borders.Weight = 2;
                                rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                                rgHead.Cells.RowHeight = 30;
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.WrapText = true;
                                rgHead.Cells.ColumnWidth = 10;

                                worksheet.Cells[3, iStartColumn++] = "Plants";
                                worksheet.Cells[3, iStartColumn++] = "Destroyed By";

                            }

                            int iRowCounter = 4; int iColumnCounter = 1;
                            for (int i = 0; i < dtExcel.Rows.Count; i++)
                            {
                                if (i > 0)
                                {

                                    if ((dtExcel.Rows[i]["sp_Bran_Code"].ToString() == dtExcel.Rows[i - 1]["sp_Bran_Code"].ToString())
                                        && (dtExcel.Rows[i]["sp_Product_Id"].ToString() == dtExcel.Rows[i - 1]["sp_Product_Id"].ToString()))
                                    {
                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sp_Sl_No"]);
                                        //int iStartColumn = 0;
                                        iColumnCounter = (2 * (iMonthNo - 1)) + 7;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 1) + "2");
                                        rgHead.Cells.Value2 = Convert.ToDateTime(dtExcel.Rows[i]["sp_Trn_Date"]).ToString("dd/MMM/yyyy");
                                        rgHead.WrapText = true;

                                        rgHead.Interior.ColorIndex = 34 + 1;
                                        rgHead.Font.ColorIndex = 1;
                                        rgHead.Font.Bold = true;
                                        rgHead.Borders.Weight = 2;
                                        //rgHead.Interior.ColorIndex = 31;
                                        //rgHead.Font.ColorIndex = 2;
                                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;


                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Destroy_Plants"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Audit_Name"];

                                    }

                                    else
                                    {

                                        iRowCounter++;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_comp_name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Zone"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Region"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Branch_Name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Product_Name"];


                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sp_Sl_No"]);

                                        iColumnCounter = (2 * (iMonthNo - 1)) + 7;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 1) + "2");
                                        rgHead.Cells.Value2 = Convert.ToDateTime(dtExcel.Rows[i]["sp_Trn_Date"]).ToString("dd/MMM/yyyy");
                                        rgHead.WrapText = true;

                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Destroy_Plants"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Audit_Name"];

                                    }
                                }
                                else
                                {

                                    worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_comp_name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Zone"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Region"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Branch_Name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Product_Name"];

                                    int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sp_Sl_No"]);
                                    //int iStartColumn = 0;
                                    iColumnCounter = (2 * (iMonthNo - 1)) + 7;
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 1) + "2");
                                    rgHead.Cells.Value2 = Convert.ToDateTime(dtExcel.Rows[i]["sp_Trn_Date"]).ToString("dd/MMM/yyyy");
                                    rgHead.WrapText = true;

                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Destroy_Plants"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Audit_Name"];


                                }

                                iColumnCounter = 1;
                            }


                            rgHead = worksheet.get_Range("A3", sLastColumn + (worksheet.UsedRange.Rows.Count).ToString());
                            rgHead.Font.Size = 11;
                            // rgHead.WrapText = true;                          
                            rgHead.Borders.Weight = 2;
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
                #endregion

                #region "Audit Physical Stock Count Not Done"

                if (cbReportType.SelectedIndex == 4)
                {
                    DataSet ds = new DataSet();
                    dtExcel = objExDb.Get_AuditPhyStkCntDetl(Company, Branches, DocumentMonth, Convert.ToDateTime(dtpToDate.Value).ToString("dd/MMM/yyyy").ToUpper(), "CNT_NOT_DONE").Tables[0];
                    objExDb = null;
                    if (dtExcel.Rows.Count > 0)
                    {
                       // dtExcel = ds.Tables[0];
                        try
                        {
                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;
                            int iTotColumns = 0;
                            iTotColumns = 5 + (1 * Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Dates"]));
                            string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                            Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                            Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Branchs"])).ToString());
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
                            rgData = worksheet.get_Range("A4", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Branchs"]) + 3).ToString());
                            rgData.WrapText = false;
                            rg = worksheet.get_Range("A3", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg = worksheet.get_Range("B3", Type.Missing);
                            rg.Cells.ColumnWidth = 40;
                            rg = worksheet.get_Range("C3", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg = worksheet.get_Range("D3", Type.Missing);
                            rg.Cells.ColumnWidth = 20;
                            rg = worksheet.get_Range("E3", Type.Missing);
                            rg.Cells.ColumnWidth = 40;                            
                            rg.WrapText = true;


                            int iColumn = 1;
                            worksheet.Cells[3, iColumn++] = "SlNo";
                            worksheet.Cells[3, iColumn++] = "Company";
                            worksheet.Cells[3, iColumn++] = "Zone";
                            worksheet.Cells[3, iColumn++] = "Region";
                            worksheet.Cells[3, iColumn++] = "Branch";                           

                            Excel.Range rgHead;
                            int iStartColumn = 0;
                            for (int iProd = 0; iProd < Convert.ToInt32(dtExcel.Rows[0]["sp_No_Of_Dates"]); iProd++)
                            {
                                rgHead = worksheet.get_Range("A2", "E2");
                                rgHead.Merge(Type.Missing);
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.RowHeight = 30;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "AUDIT NOT DONE "+ cbBranchType.Text.ToString() +" DETAILS \t  From  " + (dtpFromDate.Value).ToString("dd/MMM/yyyy").ToUpper() + " \t  To  " + (dtpToDate.Value).ToString("dd/MMM/yyyy").ToUpper() + " ";


                                iStartColumn = (1 * iProd) + 6;

                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn) + "2");


                                rgHead.Merge(Type.Missing);
                                rgHead.Interior.ColorIndex = 34 + 1;
                                rgHead.Borders.Weight = 2;
                                rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                                rgHead.Cells.RowHeight = 30;
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.WrapText = true;
                                rgHead.Cells.ColumnWidth = 10;

                                worksheet.Cells[3, iStartColumn++] = "Reason";                              

                            }

                            int iRowCounter = 4; int iColumnCounter = 1;
                            for (int i = 0; i < dtExcel.Rows.Count; i++)
                            {
                                if (i > 0)
                                {

                                    if (dtExcel.Rows[i]["sp_Bran_Code"].ToString() == dtExcel.Rows[i - 1]["sp_Bran_Code"].ToString())
                                        
                                    {
                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sp_Sl_No"]);
                                        //int iStartColumn = 0;
                                        iColumnCounter = (1 * (iMonthNo - 1)) + 6;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter) + "2");
                                        rgHead.Cells.Value2 = dtExcel.Rows[i]["sp_DocMonth"].ToString().ToUpper();
                                        rgHead.WrapText = true;

                                        rgHead.Interior.ColorIndex = 34 + 1;
                                        rgHead.Font.ColorIndex = 1;
                                        rgHead.Font.Bold = true;
                                        rgHead.Borders.Weight = 2;
                                        //rgHead.Interior.ColorIndex = 31;
                                        //rgHead.Font.ColorIndex = 2;
                                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                        
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Remarks"];
                                        
                                    }

                                    else
                                    {

                                        iRowCounter++;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_comp_name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Zone"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Region"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Branch_Name"];                                        


                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sp_Sl_No"]);

                                        iColumnCounter = (1 * (iMonthNo - 1)) + 6;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter) + "2");
                                        rgHead.Cells.Value2 = dtExcel.Rows[i]["sp_DocMonth"].ToString().ToUpper();
                                        rgHead.WrapText = true;

                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Remarks"];

                                    }
                                }
                                else
                                {

                                    worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_comp_name"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Zone"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Region"];
                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Branch_Name"];                                   

                                    int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sp_Sl_No"]);
                                    //int iStartColumn = 0;
                                    iColumnCounter = (1 * (iMonthNo - 1)) + 6;
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter) + "2");
                                    rgHead.Cells.Value2 = dtExcel.Rows[i]["sp_DocMonth"].ToString().ToUpper();
                                    rgHead.WrapText = true;

                                    worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sp_Remarks"];                                  


                                }

                                iColumnCounter = 1;
                            }


                            rgHead = worksheet.get_Range("A3", sLastColumn + (worksheet.UsedRange.Rows.Count).ToString());
                            rgHead.Font.Size = 11;
                            // rgHead.WrapText = true;                          
                            rgHead.Borders.Weight = 2;
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
                #endregion
            }

        }

        private void GetDocumentMonths()
        {
            if (cbReportType.SelectedIndex==4)
            {
                DocumentMonth = "";
                if ((dtpFromDate.Value > dtpToDate.Value))
                {
                    dtpFromDate.Value = dtpToDate.Value;
                }
                else
                {
                    int months = MonthDiff(dtpFromDate.Value, dtpToDate.Value);
                    months = months + 1;

                    for (int i = 0; i < months; i++)
                    {
                        DocumentMonth += Convert.ToDateTime(dtpFromDate.Value).AddMonths(i).ToString("MMMyyyy").ToUpper() + ',';
                    }

                    DocumentMonth = DocumentMonth.TrimEnd(',');
                }
            }
        }

        public static int MonthDiff(DateTime d2, DateTime d1)
        {

            int retVal = 0;

            if (d1.Month < d2.Month)
            {
                retVal = (d1.Month + 12) - d2.Month;
                retVal += ((d1.Year - 1) - d2.Year) * 12;
            }
            else
            {
                retVal = d1.Month - d2.Month;
                retVal += (d1.Year - d2.Year) * 12;
            }
            return retVal;

        }


        private void cbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbReportType.SelectedIndex == 4)
            {
                btnReport.Enabled = false;
            }
            else
            {
                btnReport.Enabled = true;
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
            {
                for (int k = 0; k < tvBranches.Nodes.Count; k++)
                {
                    for (int i = 0; i < tvBranches.Nodes[k].Nodes.Count; i++)
                    {
                        for (int j = 0; j < tvBranches.Nodes[k].Nodes[i].Nodes.Count; j++)
                        {
                            tvBranches.Nodes[k].Nodes[i].Nodes[j].Checked = true;

                        }
                    }

                }
            }
            else
            {
                for (int k = 0; k < tvBranches.Nodes.Count; k++)
                {
                    for (int i = 0; i < tvBranches.Nodes[k].Nodes.Count; i++)
                    {
                        for (int j = 0; j < tvBranches.Nodes[k].Nodes[i].Nodes.Count; j++)
                        {
                            tvBranches.Nodes[k].Nodes[i].Nodes[j].Checked = false;

                        }
                    }

                }

            }
        }

        private void tvBranches_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TriStateTreeView.getStatus(e);

            tvBranches.BeginUpdate();

            foreach (TreeNode Node in e.Node.Nodes)
            {
                Node.Checked = e.Node.Checked;
            }

            tvBranches.EndUpdate();
        }
        

    }
}
