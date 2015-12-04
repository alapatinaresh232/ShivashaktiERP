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
using SSCRM.App_Code;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using SSAdmin;

namespace SSCRM
{
    public partial class frmRecruitmentAnalysis : Form
    {
        private SQLDB objData = null;
        private ExcelDB objExDb = null;
        private UtilityDB objUtilityDB = null;
        private string strChkCmp = "", strBranch = "", sCompany = "", sBranch = "", sDocMonth = "";
        string Formtype="";
        private bool flagText = true;

        public frmRecruitmentAnalysis()
        {
            InitializeComponent();
        }
        public frmRecruitmentAnalysis(string iFormType)
        {
            Formtype=iFormType;
            InitializeComponent();
        }

        private void frmRecruitmentAnalysis_Load(object sender, EventArgs e)
        {
            FillCompanyData();
            cbRecruitmentSource.SelectedIndex = 1;
            cbReportType.SelectedIndex = 0;
            //if (cbRecruitmentSource.SelectedIndex == 1)
            //{
            //    cbReportType.Visible = true;
            //    lblRepType.Visible = true;
            //    cbReportType.SelectedIndex = 1;
            //}
            //else
            //{
            //    cbReportType.Visible = false;
            //    lblRepType.Visible = false;
            //}

            txtMinPoints.Text = "35";
           

            dtpFrmMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
            dtpToMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));
        }
        private void FillCompanyData()
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            try
            {
                //string strCommand = "SELECT DISTINCT(CM_COMPANY_NAME),CM_COMPANY_CODE FROM COMPANY_MAS WHERE ACTIVE='T'";
                string strCmd = "SELECT DISTINCT CM_COMPANY_NAME ,CM_COMPANY_CODE " +
                                  " FROM USER_BRANCH " +
                                  " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                  " INNER JOIN COMPANY_MAS ON CM_COMPANY_CODE = COMPANY_CODE " +
                                  " WHERE UB_USER_ID ='" + CommonData.LogUserId +
                                  "' ORDER BY CM_COMPANY_NAME";

                dt = objData.ExecuteDataSet(strCmd).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = item["CM_COMPANY_CODE"].ToString();
                        oclBox.Text = item["CM_COMPANY_NAME"].ToString();
                        clbCompany.Items.Add(oclBox);
                        oclBox = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
                dt = null;
            }

        }
        private void FillBranchData(string sCmp)
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            clbBranch.Items.Clear();
            string strCommand = "";
            try
            {
                if (chkCompanyAll.Checked == true || clbCompany.CheckedItems.Count > 0)
                {
                    //if (strChkCmp == "ALL")
                    //{
                    //    strCommand = "SELECT BRANCH_CODE as branchCode,BRANCH_NAME FROM BRANCH_MAS WHERE active='T' and branch_type='BR' ";
                    //}
                    //else
                    //{
                    //    strCommand = "SELECT BRANCH_CODE as branchCode,BRANCH_NAME FROM BRANCH_MAS WHERE active='T' " +
                    //                 " and branch_type='BR' and COMPANY_CODE in (" + sCmp + ")";
                    //}

                    strCommand = "SELECT DISTINCT BRANCH_NAME,BRANCH_CODE branchCode " +
                                       " FROM USER_BRANCH " +
                                       " INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE " +
                                       " WHERE COMPANY_CODE in (" + sCmp + ") " +
                                       " AND UB_USER_ID ='" + CommonData.LogUserId +
                                       "' and branch_type='BR' ORDER BY BRANCH_NAME ASC";

                    dt = objData.ExecuteDataSet(strCommand).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            NewCheckboxListItem oclBox = new NewCheckboxListItem();
                            oclBox.Tag = item["branchCode"].ToString();
                            oclBox.Text = item["BRANCH_NAME"].ToString();
                            clbBranch.Items.Add(oclBox);
                            oclBox = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                objData = null;
                dt = null;
            }
        }

        private void chkCompanyAll_CheckedChanged(object sender, EventArgs e)
        {
             strChkCmp = "";
            ChkBranch.Checked = false;

            if (chkCompanyAll.Checked == true)
            {
                for (int iVar = 0; iVar < clbCompany.Items.Count; iVar++)
                {
                    clbCompany.SetItemCheckState(iVar, CheckState.Checked);
                }
            }

            for (int iVar = 0; iVar < clbCompany.Items.Count; iVar++)
            {
                if (clbCompany.GetItemCheckState(iVar) == CheckState.Checked)
                {
                    strChkCmp += "'" + ((NewCheckboxListItem)clbCompany.Items[iVar]).Tag + "',";
                }
            }           
            if(chkCompanyAll.Checked==false)
            {
                for (int iVar = 0; iVar < clbCompany.Items.Count; iVar++)
                {
                    clbCompany.SetItemCheckState(iVar, CheckState.Unchecked);
                }
                strChkCmp = "";
            }
            strChkCmp = strChkCmp.TrimEnd(',');
            FillBranchData(strChkCmp);
        }

        private void ChkBranch_CheckedChanged(object sender, EventArgs e)
        {
            gvStateDetails.Rows.Clear();
            if (ChkBranch.Checked == true)
            {
                for (int iVar = 0; iVar < clbBranch.Items.Count; iVar++)
                {
                    clbBranch.SetItemCheckState(iVar, CheckState.Checked);
                }

                strBranch = "";

            }
            else
            {
                for (int iVar = 0; iVar < clbBranch.Items.Count; iVar++)
                {
                    clbBranch.SetItemCheckState(iVar, CheckState.Unchecked);
                }
                strBranch = "";
            }
            for (int iVar = 0; iVar < clbBranch.Items.Count; iVar++)
            {
                if (clbBranch.GetItemCheckState(iVar) == CheckState.Checked)
                {
                    strBranch += "'" + ((NewCheckboxListItem)clbBranch.Items[iVar]).Tag + "',";
                }
            }
            if (strBranch.Length > 0)
            {
                strBranch = strBranch.TrimEnd(',');
                FillStatesData(strBranch);

            }
        }

        private void clbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            strChkCmp = "";
            ChkBranch.Checked = false;
            chkCompanyAll.Checked = false;
            for (int iVar = 0; iVar < clbCompany.Items.Count; iVar++)
            {
                if (clbCompany.GetItemCheckState(iVar) == CheckState.Checked)
                {
                    strChkCmp += "'" + ((NewCheckboxListItem)clbCompany.Items[iVar]).Tag + "',";
                }
            }
            if (strChkCmp.Length > 0)
            {
                strChkCmp = strChkCmp.TrimEnd(',');
                FillBranchData(strChkCmp);
            }
            else
            {
                clbBranch.Items.Clear();
            }
        }
    

        private void txtMinPoints_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void clbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvStateDetails.Rows.Clear();
            strBranch = "";
            for (int iVar = 0; iVar < clbBranch.Items.Count; iVar++)
            {
                if (clbBranch.GetItemCheckState(iVar) == CheckState.Checked)
                {
                    strBranch += "'" + ((NewCheckboxListItem)clbBranch.Items[iVar]).Tag + "',";
                }
            }
            if (strBranch.Length > 0)
            {
                strBranch = strBranch.Remove(strBranch.Length - 1);
                FillStatesData(strBranch);

            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void GetSelectedValues()
        {
            sCompany = ""; sBranch = ""; sDocMonth = "";

            for (int iVar = 0; iVar < clbCompany.Items.Count; iVar++)
            {
                if (clbCompany.GetItemCheckState(iVar) == CheckState.Checked)
                {
                    sCompany += ((NewCheckboxListItem)clbCompany.Items[iVar]).Tag + ',';
                }
            }

            for (int iVar = 0; iVar < clbBranch.Items.Count; iVar++)
            {
                if (clbBranch.GetItemCheckState(iVar) == CheckState.Checked)
                {
                    sBranch += ((NewCheckboxListItem)clbBranch.Items[iVar]).Tag + ',';
                }
            }

            //string sqlText = "SELECT DISTINCT STUFF((SELECT ',' +document_month FROM document_month " +
            //                 "WHERE company_code = '" + CommonData.CompanyCode + "' AND CAST(document_month AS DATETIME) " +
            //                 "BETWEEN '" + dtpFrmMonth.Value.ToString("MMMyyyy").ToUpper() +
            //                 "' AND '" + dtpToMonth.Value.ToString("MMMyyyy").ToUpper() +
            //                 "' ORDER BY start_date asc FOR XML PATH('')),1,1,'') AS DocMonths";


            //SQLDB objData = new SQLDB();
            //try { sDocMonth = objData.ExecuteDataSet(sqlText).Tables[0].Rows[0]["DocMonths"].ToString(); }
            //catch { sDocMonth = dtpFrmMonth.Value.ToString("MMMyyyy").ToUpper(); }


            sCompany = sCompany.TrimEnd(',');
            sBranch = sBranch.TrimEnd(',');
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


        private void GetDocumentMonths()
        {
            sDocMonth = "";
            if ((dtpFrmMonth.Value > dtpToMonth.Value))
            {
                dtpFrmMonth.Value = dtpToMonth.Value;
            }
            else
            {
                int months = MonthDiff(dtpFrmMonth.Value, dtpToMonth.Value);
                months = months + 1;

                for (int i = 0; i < months; i++)
                {
                    sDocMonth += Convert.ToDateTime(dtpFrmMonth.Value).AddMonths(i).ToString("MMMyyyy").ToUpper() + ',';
                }

                sDocMonth = sDocMonth.TrimEnd(',');
            }

        }


        private bool CheckData()
        {
            bool flag = true;

            GetSelectedValues();
            if (sCompany.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Atleast One Company", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            if (sBranch.Length == 0)
            {
                flag = false;
                MessageBox.Show("Please Select Atleast One Branch", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            if (dtpFrmMonth.Value > dtpToMonth.Value)
            {
                flag = false;
                MessageBox.Show("Please Select Valid Months", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return flag;
            }
            return flag;
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                GetSelectedValues();
                GetDocumentMonths();

                if (cbRecruitmentSource.SelectedIndex == 1)
                {
                    CommonData.ViewReport = "SSERP_REP_RECRUITMTENT_ANALYSIS_DASHBOARD_DETL";
                    ReportViewer objReportview = new ReportViewer(sCompany, sBranch, "", sDocMonth, "RECRU DASHBOARD",txtMinPoints.Text );
                    objReportview.Show();
                }
                if (cbRecruitmentSource.SelectedIndex == 2)
                {
                    if (cbReportType.SelectedIndex == 0)
                    {
                        CommonData.ViewReport = "SSERP_REP_RECRUITMTENT_ANALYSIS_BY_DEPT";
                        ReportViewer objReportview = new ReportViewer(sCompany, sBranch, "", sDocMonth, "RECRU DASHBOARD DEPT", txtMinPoints.Text);
                        objReportview.Show();
                    }
                    else
                    {
                        CommonData.ViewReport = "SSERP_REP_RECRUITMTENT_ANALYSIS_BY_DEPT_SUM";
                        ReportViewer objReportview = new ReportViewer(sCompany, sBranch, "", sDocMonth, "RECRU DASHBOARD SUM", txtMinPoints.Text);
                        objReportview.Show();
                    }
                }

                //if (cbRecruitmentSource.SelectedIndex == 3)
                //{
                //    CommonData.ViewReport = "SSERP_REP_RECRUITMTENT_ANALYSIS_DASHBOARD_DETL";
                //    ReportViewer objReportview = new ReportViewer(sCompany, sBranch, "", sDocMonth, "HR SUMMARY", "");
                //    objReportview.Show();
                //}

            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            FillCompanyData();
           
            txtMinPoints.Text = "";
            gvStateDetails.Rows.Clear();
            cbRecruitmentSource.SelectedIndex = 0;
            cbReportType.SelectedIndex = 0;
        }

        private void FillStatesData(string sBranch)
        {
            objData = new SQLDB();
            DataTable dt = new DataTable();
            string strCmd = "";
            gvStateDetails.Rows.Clear();
            try
            {
                if (sBranch.Length > 8)
                {
                    strCmd = "SELECT DISTINCT STATE_CODE StateCode,sm_state StateName FROM BRANCH_MAS  " +
                             " INNER JOIN state_mas on state_mas.sm_state_code=STATE_CODE " +
                             " WHERE BRANCH_CODE in (" + sBranch + ")";
                    dt = objData.ExecuteDataSet(strCmd).Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        gvStateDetails.Rows.Add();
                        gvStateDetails.Rows[i].Cells["SlNo_ref"].Value = (i + 1).ToString();
                        gvStateDetails.Rows[i].Cells["StateCode"].Value = dt.Rows[i]["StateCode"].ToString();
                        gvStateDetails.Rows[i].Cells["StateName"].Value = dt.Rows[i]["StateName"].ToString();
                        gvStateDetails.Rows[i].Cells["Points"].Value = "35";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46) && (flagText == false))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            else if (e.KeyChar == 46 && flagText == false)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void gvStateDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            flagText = true;

            if (this.gvStateDetails.CurrentCell.ColumnIndex == gvStateDetails.Columns["Points"].Index & (e.Control != null))
            {
                TextBox txtQty = e.Control as TextBox;
                if (txtQty != null)
                {
                    flagText = false;
                    txtQty.MaxLength = 12;
                    txtQty.KeyPress += new KeyPressEventHandler(txt_KeyPress);
                }
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                GetSelectedValues();
                GetDocumentMonths();
                objUtilityDB = new UtilityDB();

                if (cbRecruitmentSource.SelectedIndex == 1)
                {
                    #region SR ANALASIS RECRU DASHBOARD
                    try
                    {
                        DataTable dtExcel = new DataTable();
                        objExDb = new ExcelDB();
                        if (cbReportType.SelectedIndex == 0)
                        {
                            dtExcel = objExDb.StaffRecruitmentAnalasis(sCompany, sBranch, "", sDocMonth, "RECRU DASHBOARD", txtMinPoints.Text).Tables[0];
                        }
                        if (cbReportType.SelectedIndex == 1)
                        {
                            dtExcel = objExDb.StaffRecruitmentAnalasis(sCompany, sBranch, "", sDocMonth, "RECRU DASHBOARD SUM", txtMinPoints.Text).Tables[0];
                        }
                        objExDb = null;

                        if (dtExcel.Rows.Count > 0)
                        {
                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;

                            Excel.Range rg = worksheet.get_Range("A5", "AF5");

                            Excel.Range rgData = worksheet.get_Range("A5", "AF" + (dtExcel.Rows.Count + 5).ToString());
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


                            rg = worksheet.get_Range("A1", "Z1");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "SR ANALASIS \t FROM " + dtpFrmMonth.Value.ToString("MMMyyyy").ToUpper() + "  \t TO " + dtpToMonth.Value.ToString("MMMyyyy").ToUpper() + "  ";
                            rg.Font.Bold = true; rg.Font.Size = 16;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;


                            rg = worksheet.get_Range("A5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Cells.Value2 = "Sl.No";

                            rg = worksheet.get_Range("B5", Type.Missing);
                            rg.Cells.ColumnWidth = 15;
                            rg.Value2 = " State Name";


                            rg = worksheet.get_Range("C5", Type.Missing);
                            rg.Cells.ColumnWidth = 20;
                            rg.Value2 = " Company Name";


                            rg = worksheet.get_Range("D5", Type.Missing);
                            rg.Cells.ColumnWidth = 25;
                            rg.Value2 = " Branch Name";


                            rg = worksheet.get_Range("E5", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.Value2 = " Doc Month";
                            if (cbReportType.Visible = true && cbReportType.SelectedIndex == 2)
                                worksheet.get_Range("E5", Type.Missing).EntireColumn.Hidden = true;
                            else
                                worksheet.get_Range("E5", Type.Missing).EntireColumn.Hidden = false;

                            cbReportType.Visible = true;

                            rg = worksheet.get_Range("F3", "k3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = " SALES";
                            rg.Font.ColorIndex = 2;
                            rg.Interior.ColorIndex = 21;
                            rg.Font.Bold = true; rg.Font.Size = 12;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;


                            rg = worksheet.get_Range("F4", "G4");
                            rg.Merge(Type.Missing);
                            rg.Font.ColorIndex = 2;
                            rg.Interior.ColorIndex = 21;
                            rg.Value2 = " SR'S";
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;

                            rg.Interior.ColorIndex = 21;
                            rg.Font.Bold = true; rg.Font.Size = 10;

                            rg = worksheet.get_Range("F5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " NO'S";


                            rg = worksheet.get_Range("G5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";


                            rg = worksheet.get_Range("H4", "I4");
                            rg.Merge(Type.Missing);
                            rg.Value2 = " POINTS";
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Font.ColorIndex = 2;
                            rg.Interior.ColorIndex = 21;
                            rg.Font.Bold = true; rg.Font.Size = 10;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;

                            rg = worksheet.get_Range("H5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " PT'S";


                            rg = worksheet.get_Range("I5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";


                            rg = worksheet.get_Range("J4", "K4");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "SUCCESSFUL ";
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg.Font.ColorIndex = 2;
                            rg.Interior.ColorIndex = 21;
                            rg.Font.Bold = true; rg.Font.Size = 10;

                            rg = worksheet.get_Range("J5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " SR'S";

                            rg = worksheet.get_Range("K5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";

                            rg = worksheet.get_Range("L3", "Q3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = " HR";

                            rg.Interior.ColorIndex = 8;
                            rg.Font.Bold = true; rg.Font.Size = 12;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("L4", "M4");
                            rg.Merge(Type.Missing);
                            rg.Interior.ColorIndex = 8;
                            rg.Value2 = " SR'S";
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Font.Bold = true; rg.Font.Size = 10;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;

                            rg = worksheet.get_Range("L5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " NO'S";


                            rg = worksheet.get_Range("M5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";


                            rg = worksheet.get_Range("N4", "O4");
                            rg.Merge(Type.Missing);
                            rg.Value2 = " POINTS";
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Interior.ColorIndex = 8;
                            rg.Font.Bold = true; rg.Font.Size = 10;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;

                            rg = worksheet.get_Range("N5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " PT'S";

                            rg = worksheet.get_Range("O5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";

                            rg = worksheet.get_Range("P4", "Q4");
                            rg.Merge(Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.Interior.ColorIndex = 8;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "SUCCESSFUL ";
                            rg.Font.Bold = true; rg.Font.Size = 10;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;

                            rg = worksheet.get_Range("P5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " SR'S";

                            rg = worksheet.get_Range("Q5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";

                            rg = worksheet.get_Range("R3", "W3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = " OTHERS";
                            rg.Font.ColorIndex = 2;
                            rg.Interior.ColorIndex = 9;
                            rg.Font.Bold = true; rg.Font.Size = 12;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;

                            rg = worksheet.get_Range("R4", "S4");
                            rg.Merge(Type.Missing);
                            rg.Interior.ColorIndex = 9;
                            rg.Font.ColorIndex = 2;
                            rg.Value2 = " SR'S";
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg.Font.Bold = true; rg.Font.Size = 10;

                            rg = worksheet.get_Range("R5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " NO'S";

                            rg = worksheet.get_Range("S5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";

                            rg = worksheet.get_Range("T4", "U4");
                            rg.Merge(Type.Missing);
                            rg.Value2 = " POINTS";
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg.Font.ColorIndex = 2;
                            rg.Interior.ColorIndex = 9;
                            rg.Font.Bold = true; rg.Font.Size = 10;


                            rg = worksheet.get_Range("T5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " PT'S";


                            rg = worksheet.get_Range("U5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";

                            rg = worksheet.get_Range("V4", "W4");
                            rg.Merge(Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.Font.ColorIndex = 2;
                            rg.Value2 = "SUCCESSFUL ";
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg.Interior.ColorIndex = 9;
                            rg.Font.Bold = true; rg.Font.Size = 10;

                            rg = worksheet.get_Range("V5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " SR'S";
                            rg = worksheet.get_Range("W5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";

                            rg = worksheet.get_Range("X3", "Z4");
                            rg.Merge(Type.Missing);
                            rg.Interior.ColorIndex = 5;
                            rg.Font.ColorIndex = 2;
                            rg.Value2 = " TOTAL RECRUITMENT";
                            rg.Font.Bold = true; rg.Font.Size = 12;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("X5", Type.Missing);
                            rg.Merge(Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " SR.NO'S";

                            rg = worksheet.get_Range("y5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " SR.PTS";

                            rg = worksheet.get_Range("Z5", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.Value2 = " SUCCEESS";

                            rg = worksheet.get_Range("AA2", "AF2");
                            rg.Merge(Type.Missing);
                            rg.Interior.ColorIndex = 10;
                            rg.Font.ColorIndex = 2;
                            rg.Value2 = " RESOURCE OF RECRUITMENT";
                            rg.Font.Bold = true; rg.Font.Size = 12;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg = worksheet.get_Range("AA3", "AC3");
                            rg.Merge(Type.Missing);
                            rg.Interior.ColorIndex = 10;
                            rg.Font.ColorIndex = 2;
                            rg.Value2 = " REFERENCE";
                            rg.Font.Bold = true; rg.Font.Size = 12;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg = worksheet.get_Range("AA4", "AC4");
                            rg.Merge(Type.Missing);
                            rg.Interior.ColorIndex = 10;
                            rg.Font.ColorIndex = 2;
                            rg.Value2 = " SR'S";
                            rg.Font.Bold = true; rg.Font.Size = 12;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg = worksheet.get_Range("AA5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " NO'S";

                            rg = worksheet.get_Range("AB5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";


                            rg = worksheet.get_Range("AC5", "AC5");
                            rg.Merge(Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.Interior.ColorIndex = 31;
                            rg.Font.ColorIndex = 2;
                            rg.Value2 = " SUCCESS %";


                            rg = worksheet.get_Range("AD3", "AF3");
                            rg.Merge(Type.Missing);
                            rg.Interior.ColorIndex = 10;
                            rg.Font.ColorIndex = 2;
                            rg.Value2 = " OTHER SOURCE";
                            rg.Font.Bold = true; rg.Font.Size = 12;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg = worksheet.get_Range("AD4", "AF4");
                            rg.Merge(Type.Missing);
                            rg.Interior.ColorIndex = 10;
                            rg.Font.ColorIndex = 2;
                            rg.Value2 = " SR'S";
                            rg.Font.Bold = true; rg.Font.Size = 12;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("AD5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " NO'S";

                            rg = worksheet.get_Range("AE5", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";


                            rg = worksheet.get_Range("AF5", "AF5");
                            rg.Merge(Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.Interior.ColorIndex = 31;
                            rg.Font.ColorIndex = 2;
                            rg.Value2 = " SUCCESS %";

                            int RowCounter = 1;

                            foreach (DataRow dr in dtExcel.Rows)
                            {
                                int i = 1;
                                worksheet.Cells[RowCounter + 5, i++] = RowCounter;
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_STATE_NAME"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_COMPANY_NAME"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_BRANCH_NAME"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_DOC_MONTH"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_SALE_SRS"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_SALE_SRS_PERC"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_SALE_SRS_PTS"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_SALE_SRS_PTS_PERC"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_SALE_SRS_35AB"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_SALE_SRS_35ABPERC"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_HR_SRS"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_HR_SRS_PERC"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_HR_SRS_PTS"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_HR_SRS_PTS_PERC"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_HR_SRS_35AB"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_HR_SRS_35ABPERC"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_OTHR_SRS"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_OTHR_SRS_PERC"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_OTHR_SRS_PTS"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_OTHR_SRS_PTS_PERC"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_OTHR_SRS_35AB"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_OTHR_SRS_35ABPERC"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_TOTAL_SRS"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_TOTAL_SRS_PTS"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_TOTAL_SRS_35AB"].ToString();
                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_REFRECRU_SRS"].ToString();

                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_REFRECRU_SRS_PERC"].ToString();

                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_REFRECRU_SRS_35ABPERC"].ToString();

                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_OTHRECRU_SRS"].ToString();

                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_OTHRECRU_SRS_PERC"].ToString();

                                worksheet.Cells[RowCounter + 5, i++] = dr["SR_RECR_BY_OTHRECRU_SRS_35ABPERC"].ToString();
                                RowCounter++;
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    #endregion
                }


                else if (cbRecruitmentSource.SelectedIndex == 2)
                {
                    #region SR ANALASIS RECRU DEPT WISE
                    try
                    {
                        objExDb = new ExcelDB();
                        DataTable dtExcel = new DataTable();
                        if (cbReportType.SelectedIndex == 0)
                        {
                            dtExcel = objExDb.StaffRecruitmentAnalasis(sCompany, sBranch, "", sDocMonth, "RECRU DASHBOARD DEPT", txtMinPoints.Text).Tables[0];
                        }
                        if (cbReportType.SelectedIndex == 1)
                        {
                            dtExcel = objExDb.StaffRecruitmentAnalasis(sCompany, sBranch, "", sDocMonth, "RECRU DASHBOARD SUM", txtMinPoints.Text).Tables[0];
                        }
                        objExDb = null;

                        if (dtExcel.Rows.Count > 0)
                        {
                            Excel.Application oXL = new Excel.Application();
                            Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                            Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                            oXL.Visible = true;

                            Excel.Range rg = worksheet.get_Range("A4", "AD4");

                            Excel.Range rgData = worksheet.get_Range("A4", "AD" + (dtExcel.Rows.Count + 4).ToString());
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


                            rg = worksheet.get_Range("A1", "AD1");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "SR ANALASIS \t FROM " + dtpFrmMonth.Value.ToString("MMMyyyy").ToUpper() + "  \t TO " + dtpToMonth.Value.ToString("MMMyyyy").ToUpper() + "";


                            rg.Font.Bold = true; rg.Font.Size = 16;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;


                            rg = worksheet.get_Range("A4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Cells.Value2 = "Sl.No";

                            rg = worksheet.get_Range("B4", Type.Missing);
                            rg.Cells.ColumnWidth = 15;
                            rg.Value2 = " State Name";


                            rg = worksheet.get_Range("C4", Type.Missing);
                            rg.Cells.ColumnWidth = 20;
                            rg.Value2 = " Company Name";


                            rg = worksheet.get_Range("D4", Type.Missing);
                            rg.Cells.ColumnWidth = 25;
                            rg.Value2 = " Branch Name";


                            rg = worksheet.get_Range("E4", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.Value2 = " Doc Month";
                            if (cbReportType.Visible = true && cbReportType.SelectedIndex == 2)
                                worksheet.get_Range("E4", Type.Missing).EntireColumn.Hidden = true;
                            else
                                worksheet.get_Range("E4", Type.Missing).EntireColumn.Hidden = false;

                            cbReportType.Visible = true;                          


                            rg = worksheet.get_Range("F2", "L2");
                            rg.Merge(Type.Missing);
                            rg.Value2 = " SALES";
                            rg.Font.ColorIndex = 2;
                            rg.Interior.ColorIndex = 21;
                            rg.Font.Bold = true; rg.Font.Size = 12;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;

                            rg = worksheet.get_Range("F3", "G3");
                            rg.Merge(Type.Missing);
                            rg.Font.ColorIndex = 2;
                            rg.Interior.ColorIndex = 21;
                            rg.Value2 = " SR'S";
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg.Interior.ColorIndex = 21;
                            rg.Font.Bold = true; rg.Font.Size = 10;

                            rg = worksheet.get_Range("F4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " NO'S";


                            rg = worksheet.get_Range("G4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";


                            rg = worksheet.get_Range("H3", "J3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = " POINTS";
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;

                            rg.Font.ColorIndex = 2;
                            rg.Interior.ColorIndex = 21;
                            rg.Font.Bold = true; rg.Font.Size = 10;

                            rg = worksheet.get_Range("H4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " PT'S";


                            rg = worksheet.get_Range("I4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";


                            rg = worksheet.get_Range("J4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " PT'S/SR'S ";


                            rg = worksheet.get_Range("K3", "L3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = "SUCCESSFUL ";
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg.Font.ColorIndex = 2;
                            rg.Interior.ColorIndex = 21;
                            rg.Font.Bold = true; rg.Font.Size = 10;

                            rg = worksheet.get_Range("K4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " SR'S";


                            rg = worksheet.get_Range("L4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";

                            rg = worksheet.get_Range("M2", "S2");
                            rg.Merge(Type.Missing);
                            rg.Value2 = " HR";

                            rg.Interior.ColorIndex = 8;
                            rg.Font.Bold = true; rg.Font.Size = 12;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;

                            rg = worksheet.get_Range("M3", "N3");
                            rg.Merge(Type.Missing);
                            rg.Interior.ColorIndex = 8;
                            rg.Value2 = " SR'S";
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg.Font.Bold = true; rg.Font.Size = 10;


                            rg = worksheet.get_Range("M4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " NO'S";


                            rg = worksheet.get_Range("N4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";


                            rg = worksheet.get_Range("O3", "Q3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = " POINTS";
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;

                            rg.Interior.ColorIndex = 8;
                            rg.Font.Bold = true; rg.Font.Size = 10;

                            rg = worksheet.get_Range("O4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " PT'S";


                            rg = worksheet.get_Range("P4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";



                            rg = worksheet.get_Range("Q4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " PT'S/SR'S ";



                            rg = worksheet.get_Range("R3", "S3");
                            rg.Merge(Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.Interior.ColorIndex = 8;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Value2 = "SUCCESSFUL ";
                            rg.Font.Bold = true; rg.Font.Size = 10;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;

                            rg = worksheet.get_Range("R4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " SR'S";



                            rg = worksheet.get_Range("S4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";

                            rg = worksheet.get_Range("T2", "Z2");
                            rg.Merge(Type.Missing);
                            rg.Value2 = " OTHERS";
                            rg.Font.ColorIndex = 2;
                            rg.Interior.ColorIndex = 9;
                            rg.Font.Bold = true; rg.Font.Size = 12;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg = worksheet.get_Range("T3", "U3");
                            rg.Merge(Type.Missing);
                            rg.Interior.ColorIndex = 9;
                            rg.Font.ColorIndex = 2;
                            rg.Value2 = " SR'S";
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg.Font.Bold = true; rg.Font.Size = 10;

                            rg = worksheet.get_Range("T4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " NO'S";

                            rg = worksheet.get_Range("U4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";

                            rg = worksheet.get_Range("V3", "X3");
                            rg.Merge(Type.Missing);
                            rg.Value2 = " POINTS";
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg.Font.ColorIndex = 2;
                            rg.Interior.ColorIndex = 9;
                            rg.Font.Bold = true; rg.Font.Size = 10;


                            rg = worksheet.get_Range("V4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " PT'S";


                            rg = worksheet.get_Range("W4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";


                            rg = worksheet.get_Range("X4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " PT'S/SR'S ";

                            rg = worksheet.get_Range("Y3", "Z3");
                            rg.Merge(Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.Font.ColorIndex = 2;
                            rg.Value2 = "SUCCESSFUL ";
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg.Interior.ColorIndex = 9;
                            rg.Font.Bold = true; rg.Font.Size = 10;

                            rg = worksheet.get_Range("Y4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " SR'S";



                            rg = worksheet.get_Range("Z4", Type.Missing);
                            rg.Cells.ColumnWidth = 5;
                            rg.Value2 = " % ";



                            rg = worksheet.get_Range("AA2", "AD3");
                            rg.Merge(Type.Missing);
                            rg.Interior.ColorIndex = 5;
                            rg.Font.ColorIndex = 2;
                            rg.Value2 = " TOTAL RECRUITMENT";
                            rg.Font.Bold = true; rg.Font.Size = 12;
                            rg.VerticalAlignment = Excel.Constants.xlCenter;
                            rg.HorizontalAlignment = Excel.Constants.xlCenter;
                            rg.Borders.Weight = 2;
                            rg.Borders.LineStyle = Excel.Constants.xlSolid;
                            rg = worksheet.get_Range("AA4", Type.Missing);
                            rg.Merge(Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg.Value2 = " SR.NO'S";



                            rg = worksheet.get_Range("AB4", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg.Value2 = " SR.PTS";


                            rg = worksheet.get_Range("AC4", Type.Missing);
                            rg.Cells.ColumnWidth = 8;
                            rg.Value2 = " PTS/SR'S";


                            rg = worksheet.get_Range("AD4", Type.Missing);
                            rg.Cells.ColumnWidth = 10;
                            rg.Value2 = " Success";



                            int RowCounter = 1;

                            foreach (DataRow dr in dtExcel.Rows)
                            {
                                int i = 1;
                                worksheet.Cells[RowCounter + 4, i++] = RowCounter;
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_STATE_NAME"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_COMPANY_NAME"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_BRANCH_NAME"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_DOC_MONTH"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_SALE_SRS"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_SALE_SRS_PERC"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_SALE_SRS_PTS"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_SALE_SRS_PTS_PERC"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_SALE_PTS_PER_SR"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_SALE_SRS_35AB"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_SALE_SRS_35ABPERC"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_HR_SRS"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_HR_SRS_PERC"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_HR_SRS_PTS"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_HR_SRS_PTS_PERC"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_HR_PTS_PER_SR"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_HR_SRS_35AB"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_HR_SRS_35ABPERC"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_OTHR_SRS"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_OTHR_SRS_PERC"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_OTHR_SRS_PTS"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_OTHR_SRS_PTS_PERC"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_OTHR_PTS_PER_SR"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_OTHR_SRS_35AB"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_OTHR_SRS_35ABPERC"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_TOTAL_SRS"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_TOTAL_SRS_PTS"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_TOTAL_PTS_PER_SR"].ToString();
                                worksheet.Cells[RowCounter + 4, i++] = dr["SR_RECR_BY_TOTAL_SRS_35AB"].ToString();


                                RowCounter++;
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    #endregion
                }
            }          

        }

        private void cbRecruitmentSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            //btnReport.Visible = true;
            //if (cbRecruitmentSource.SelectedIndex == 1)
            //{
            //    cbReportType.Visible = true;
            //    lblRepType.Visible = true;
            //}
            //else
            //{
            //    cbReportType.Visible = false;
            //    lblRepType.Visible = false;
            //}
        }

        private void cbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbReportType.SelectedIndex == 1)
            {
                btnReport.Visible = false;
            }
            else
            {
                btnReport.Visible = true;
            }
        }

        private void txtMinPoints_Validated(object sender, EventArgs e)
        {
            if (gvStateDetails.Rows.Count > 0)
            {
                for (int i = 0; i < gvStateDetails.Rows.Count; i++)
                {
                  gvStateDetails.Rows[i].Cells["Points"].Value = txtMinPoints.Text;
                }
            }

        }

   

    }

}

    





