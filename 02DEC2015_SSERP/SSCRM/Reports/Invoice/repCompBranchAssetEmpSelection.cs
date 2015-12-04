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
using SSAdmin;
using SSCRM.App_Code;
using Excel = Microsoft.Office.Interop.Excel;

namespace SSCRM
{
    public partial class repCompBranchAssetEmpSelection : Form
    {
        private InvoiceDB objInv = null;
        private UtilityDB objUtilityDB = null;
        private SQLDB objDB = null;
        private ExcelDB objExcelDB = null;
        private ReportViewer childReportViewer = null;

        private string company = "", branches = "", documentMonth = "", finyear = "", sAssetType = "", sAssetModel = "", sEcode = "";

        public repCompBranchAssetEmpSelection()
        {
            InitializeComponent();
        }

        private void repCompBranchAssetEmpSelection_Load(object sender, EventArgs e)
        {
            FillBranches();
            FillDocumentMonths();
            FillAssets();
        }

        private void FillAssets()
        {
            objInv = new InvoiceDB();
            objDB = new SQLDB();
            DataSet ds = new DataSet();
            DataSet dschild = new DataSet();
            string strSQL = "SELECT DISTINCT FAH_ASSET_TYPE DisplayMember,FAH_ASSET_TYPE ValueMember FROM FIXED_ASSETS_HEAD";
            ds = objDB.ExecuteDataSet(strSQL);
            ttvAssets.Nodes.Add("Assets", "Assets");
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ttvAssets.Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["DisplayMember"].ToString(),
                                                                ds.Tables[0].Rows[i]["ValueMember"].ToString());
                    strSQL = "SELECT DISTINCT FAH_ASSET_TYPE,case when FAH_ASSET_MAKE=FAH_MODEL then FAH_ASSET_MAKE "+
                                "else FAH_ASSET_MAKE+'-'+FAH_MODEL end as DisplayMember FROM FIXED_ASSETS_HEAD WHERE "+
                                "FAH_ASSET_TYPE ='" + ds.Tables[0].Rows[i]["ValueMember"].ToString() + "' ORDER BY DisplayMember ASC";
                    dschild = objDB.ExecuteDataSet(strSQL);
                    if (dschild.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                        {
                            ttvAssets.Nodes[0].Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["DisplayMember"].ToString(),
                                                            dschild.Tables[0].Rows[j]["DisplayMember"].ToString());
                        }
                    }
                }

            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.ttvAssets.SelectedNode = ttvAssets.Nodes[0];
                this.ttvAssets.SelectedNode.Expand();
            }
        }

        private void FillBranches()
        {
            objInv = new InvoiceDB();
            objUtilityDB = new UtilityDB();
            DataSet ds = new DataSet();
            if (CommonData.LogUserId.ToUpper() == "ADMIN")
                ds = objInv.AdminBranchCursor_Get("", "", "PARENT");
            else
                ds = objUtilityDB.UserBranchCursor_Get(CommonData.LogUserId, "", "", "PARENT");
            ttvBranch.Nodes.Add("Branches", "Branches");
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ttvBranch.Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), ds.Tables[0].Rows[i]["COMPANY_NAME"].ToString());
                    DataSet dschild = new DataSet();
                    if (CommonData.LogUserId.ToUpper() == "ADMIN")
                    {
                        //if (sRep_Type == "SP_CHECKLIST")
                        //    dschild = objInv.AdminBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "SP", "CHILD");
                        //else
                            dschild = objInv.AdminBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "BR", "CHILD");
                    }
                    else
                    {
                        //if (sRep_Type == "SP_CHECKLIST")
                        //    dschild = objUtilityDB.UserBranchCursor_Get(CommonData.LogUserId, ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "SP", "CHILD");
                        //else
                            dschild = objUtilityDB.UserBranchCursor_Get(CommonData.LogUserId, ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "BR", "CHILD");
                    }
                    //tvBranches.Nodes[i].Nodes.Add("BRANCHES" + "(" + dschild.Tables[0].Rows.Count + ")");
                    if (dschild.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                        {
                            ttvBranch.Nodes[0].Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                        }
                    }
                }
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.ttvBranch.SelectedNode = ttvBranch.Nodes[0];
                this.ttvBranch.SelectedNode.Expand();
            }

            
        }

        private void FillDocumentMonths()
        {
            objInv = new InvoiceDB();
            objDB = new SQLDB();
            DataSet ds = new DataSet();
            DataSet dschild = new DataSet();
            string strSQL = "SELECT DISTINCT FY_FIN_YEAR FROM FIN_YEAR";
            ds = objDB.ExecuteDataSet(strSQL);
            ttvDocMonth.Nodes.Add("DocMonths", "DocMonths");
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ttvDocMonth.Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["FY_FIN_YEAR"].ToString(), 
                                                                ds.Tables[0].Rows[i]["FY_FIN_YEAR"].ToString());
                    strSQL = "SELECT DISTINCT DOCUMENT_MONTH,start_date FROM DOCUMENT_MONTH WHERE FIN_YEAR = '" + ds.Tables[0].Rows[i]["FY_FIN_YEAR"].ToString() + "' ORDER BY START_DATE ASC";
                    dschild = objDB.ExecuteDataSet(strSQL);
                    if (dschild.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                        {
                            ttvDocMonth.Nodes[0].Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["DOCUMENT_MONTH"].ToString(), 
                                                            dschild.Tables[0].Rows[j]["DOCUMENT_MONTH"].ToString());
                        }
                    }
                }

            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.ttvDocMonth.SelectedNode = ttvDocMonth.Nodes[0];
                this.ttvDocMonth.SelectedNode.Expand();
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void lnkEmplList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GetSelectedControlsIDs();
            FillEmployeesList();
        }

        public void GetSelectedControlsIDs()
        {
            company = ""; branches = ""; documentMonth = "";
            finyear = ""; sAssetType = ""; sAssetModel = ""; sEcode = "";

            bool iscomp = false;
            TreeNodeCollection  mNodes = this.ttvBranch.Nodes[0].Nodes;
            foreach (TreeNode m in mNodes)
            {
                TreeNodeCollection nNodes = m.Nodes;
                foreach (TreeNode n in nNodes)
                {
                    if (n.StateImageIndex == 1 || n.StateImageIndex == 2)
                    {
                        if (branches != string.Empty)
                            branches += ",";
                        branches += n.Name;
                        iscomp = true;
                    }
                }
                if (iscomp == true)
                {
                    if (company != string.Empty)
                        company += ",";
                    company += m.Name;
                }
                iscomp = false;
            }
            mNodes = null;
            mNodes = this.ttvDocMonth.Nodes[0].Nodes;
            
            foreach (TreeNode m in mNodes)
            {
                TreeNodeCollection nNodes = m.Nodes;
                foreach (TreeNode n in nNodes)
                {
                    if (n.StateImageIndex == 1 || n.StateImageIndex == 2)
                    {
                        if (documentMonth != string.Empty)
                            documentMonth += ",";
                        if (finyear != string.Empty)
                            finyear += ",";
                        documentMonth += n.Name;
                        //finyear = nNodes.Tag;
                    }
                }
            }
            mNodes = null;
            mNodes = this.ttvAssets.Nodes[0].Nodes;
            bool isAsst = false;
            foreach (TreeNode m in mNodes)
            {
                TreeNodeCollection nNodes = m.Nodes;
                foreach (TreeNode n in nNodes)
                {
                    if (n.StateImageIndex == 1 || n.StateImageIndex == 2)
                    {
                        if (sAssetModel != string.Empty)
                            sAssetModel += ",";
                        if (sAssetType != string.Empty)
                            sAssetType += ",";
                        sAssetModel += n.Name;
                        //sAssetType += ttvAssets.Nodes[0].Nodes[i].Name.ToString();
                        isAsst = true;
                    }
                }
                if (isAsst == true)
                {
                    if (sAssetType != string.Empty)
                        sAssetType += ",";
                    sAssetType += m.Name;
                }
                isAsst = false;
            }
            mNodes = null;
            if (ttvEmployees.Nodes.Count > 0)
            {
                mNodes = this.ttvEmployees.Nodes[0].Nodes;

                foreach (TreeNode m in mNodes)
                {

                    if (m.StateImageIndex == 1 || m.StateImageIndex == 2)
                    {
                        if (sEcode != string.Empty)
                            sEcode += ",";
                        sEcode += m.Name;

                    }

                }
            }
        }

        private void FillEmployeesList()
        {
            
            if (branches.Length == 0)
            {
                MessageBox.Show("Select Branches", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (sAssetModel.Length == 0)
            {
                MessageBox.Show("Select Assets", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ttvEmployees.Nodes.Clear();
            objDB = new SQLDB();
            DataSet ds = new DataSet();
            string strSQL = "exec GetAssetIssuedEmployeeList '" + branches + "','" + sAssetType + "','" + sAssetModel + "',''";
            ds = objDB.ExecuteDataSet(strSQL);
            ttvEmployees.Nodes.Add("Employees", "Employees");
            if (ds.Tables[0].Rows.Count > 0)
            {                
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ttvEmployees.Nodes[0].Nodes.Add(ds.Tables[0].Rows[i]["Ecode"].ToString(),
                                                                ds.Tables[0].Rows[i]["Name"].ToString());
                }

            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.ttvEmployees.SelectedNode = ttvEmployees.Nodes[0];
                this.ttvEmployees.SelectedNode.Expand();
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            GetSelectedControlsIDs();
            childReportViewer = new ReportViewer(company, branches, finyear, documentMonth, sAssetType, sAssetModel, sEcode, "");
            CommonData.ViewReport = "SSCRM_REP_EMP_PERF_AGNST_ASSET";
            childReportViewer.Show();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            GetSelectedControlsIDs();
            objExcelDB = new ExcelDB();
            objUtilityDB = new UtilityDB();
            DataTable dtExcel = objExcelDB.GetEmpPerfAgainstAsset(company, branches, finyear, documentMonth, sAssetType, sAssetModel, sEcode, "").Tables[0];
            objExcelDB = null;

            int iTotColumns = 0;
            if (dtExcel.Rows.Count > 0)
            {
                Excel.Application oXL = new Excel.Application();
                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                oXL.Visible = true;
                iTotColumns = 8 + (7 * Convert.ToInt32(dtExcel.Rows[0]["sr_total_months"]));
                string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                Excel.Range rg = worksheet.get_Range("A3", sLastColumn+"3");
                Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sr_Noof_Emps"]) + 3).ToString());
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
                rg.Cells.ColumnWidth = 6;

                rg = worksheet.get_Range("C3", Type.Missing);
                rg.Cells.ColumnWidth = 30;

                rg = worksheet.get_Range("D3", Type.Missing);
                rg.Cells.ColumnWidth = 30;

                rg = worksheet.get_Range("E3", Type.Missing);
                rg.Cells.ColumnWidth = 10;

                rg = worksheet.get_Range("F3", Type.Missing);
                rg.Cells.ColumnWidth = 40;

                rg = worksheet.get_Range("G3", Type.Missing);
                rg.Cells.ColumnWidth = 10;

                rg = worksheet.get_Range("H3", Type.Missing);
                rg.Cells.ColumnWidth = 7;
                

                worksheet.Cells[3, 1] = "SlNo";
                worksheet.Cells[3, 2] = "Ecode";
                worksheet.Cells[3, 3] = "Name";
                worksheet.Cells[3, 4] = "Desig";
                worksheet.Cells[3, 5] = "Doj";
                worksheet.Cells[3, 6] = "Branch";
                worksheet.Cells[3, 7] = "PhoneNo";
                worksheet.Cells[3, 8] = "Issued Month";
                for (int iMonths = 1; iMonths <= Convert.ToInt32(dtExcel.Rows[0]["sr_total_months"]); iMonths++)
                {
                    Excel.Range rgHead = worksheet.get_Range("A1","H2");
                    rgHead.Merge(Type.Missing);
                    rgHead.Font.Size = 14;
                    rgHead.Font.ColorIndex = 1;
                    rgHead.Font.Bold = true;
                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead.Cells.Value2 = dtExcel.Rows[0]["sr_asset_type"].ToString().ToUpper() + " USER DETAILS WITH PERFORMANCE";
                    int iStartColumn = 0;
                    iStartColumn = (7 * iMonths)+2;

                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn)+"2", objUtilityDB.GetColumnName(iStartColumn+6) + "2");                    
                    //rgHead.Cells.ColumnWidth = 5;
                    rgHead.Merge(Type.Missing);
                    rgHead.Interior.ColorIndex = 32 + iMonths;
                    rgHead.Borders.Weight = 2;
                    rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                    rgHead.Cells.RowHeight = 20;
                    rgHead.Font.Size = 14;
                    rgHead.Font.ColorIndex = 1;
                    rgHead.Font.Bold = true;
                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "3", objUtilityDB.GetColumnName(iStartColumn + 6) + "3");
                    rgHead.Interior.ColorIndex = 32 + iMonths;
                    rgHead.Font.ColorIndex = 1;
                    rgHead.Cells.ColumnWidth = 5;
                    //worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 6) + "2").Merge(Type.Missing);
                    //rg.Application.ActiveWindow.FreezePanes = true;
                    //rgHead.Height = 10;
                    //rgHead.Cells.Value2 = CommonData.BranchName + "";
                    
                    worksheet.Cells[3, iStartColumn] = "Pers Points";
                    worksheet.Cells[3, iStartColumn+1] = "Pers Cust";
                    worksheet.Cells[3, iStartColumn+2] = "Groups";
                    worksheet.Cells[3, iStartColumn+3] = "Avg Pmd";
                    worksheet.Cells[3, iStartColumn+4] = "Group Points";
                    worksheet.Cells[3, iStartColumn+5] = "Points P/G";
                    worksheet.Cells[3, iStartColumn+6] = "Group Cust";
                    
                }
                int iRowCounter = 4; int iColumnCounter = 1;
                for (int i = 0; i < dtExcel.Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        if (dtExcel.Rows[i]["sr_eora_code"].ToString() == dtExcel.Rows[i - 1]["sr_eora_code"].ToString())
                        {
                            int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sr_Montn_SlNo"]);
                            //int iStartColumn = 0;
                            iColumnCounter = (7 * iMonthNo) + 2;
                            Excel.Range rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 6) + "2");
                            rgHead.Cells.Value2 = dtExcel.Rows[i]["sr_doc_month"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_points"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_cust"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_groups"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_avgpmd"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_points"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_points_pergroup"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_cust"];
                        }
                        else
                        {
                            iRowCounter++;
                            worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter-3;
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_eora_code"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_eora_name"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_desig"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_doj"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_branch_name"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_cont_no"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_issue_month"];
                            int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sr_Montn_SlNo"]);
                            //int iStartColumn = 0;
                            iColumnCounter = (7 * iMonthNo) + 2;
                            Excel.Range rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 6) + "2");
                            rgHead.Cells.Value2 = dtExcel.Rows[i]["sr_doc_month"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_points"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_cust"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_groups"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_avgpmd"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_points"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_points_pergroup"];
                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_cust"];
                        }
                    }
                    else
                    {
                        worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter-3;
                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_eora_code"];
                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_eora_name"];
                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_desig"];
                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_doj"];
                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_branch_name"];
                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_cont_no"];
                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_issue_month"];
                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sr_Montn_SlNo"]);
                        //int iStartColumn = 0;
                        iColumnCounter = (7 * iMonthNo) + 2;
                        Excel.Range rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 6) + "2");
                        rgHead.Cells.Value2 = dtExcel.Rows[i]["sr_doc_month"];
                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_points"];
                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_cust"];
                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_groups"];
                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_avgpmd"];
                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_points"];
                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_points_pergroup"];
                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_cust"];
                    }

                    iColumnCounter = 1;
                }


            }
        }
    }
}
