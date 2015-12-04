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
    public partial class ReportGLSelection : Form
    {
        private StaffLevel objStfLvl = null;
        private ReportViewer childReportViewer = null;
        private UtilityDB objUtil = null;
        private SQLDB objDB = null;
        private ExcelDB objExDb = null;
        private string strRep = "";
        public ReportGLSelection()
        {
            InitializeComponent();
        }

        public ReportGLSelection(string sRep)
        {
            InitializeComponent();
            strRep = sRep;
        }

        private void ReportGLSelection_Load(object sender, EventArgs e)
        {
            dtpDate.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.DocMonth).ToString("dd/MMM/yyyy"));
            if (strRep == "VEHICLE_LOAN")
            {
                this.Text = "Vehicle Loan Recovery :: Summary";
                chkAll.Visible = true;
                lblDocm.Visible = false;
                dtpDate.Visible = false;
            }
            else if (strRep == "VEHICLE_INFO")
            {
                this.Text = "Vehicle Loan Recovery :: Summary";
                chkAll.Visible = true;
                lblDocm.Visible = false;
                dtpDate.Visible = false;
            }
            else if (strRep == "SERVICE_CONSOLIDATION")
            {
                this.Text = "Service Consolidation Report :: Summary";
                chkAll.Visible = true;
                lblDocm.Visible = false;
                dtpDate.Visible = true;
            }
            else if (strRep == "STOCK_SUMMARY")
            {
                this.Text = "Stock Report :: Summary";
                chkAll.Visible = true;
                lblDocm.Visible = false;
                dtpDate.Visible = true;
            }
            else if (strRep == "STOCK_LEDGER")
            {
                this.Text = "Stock Report :: Ledger";
                chkAll.Visible = true;
                lblDocm.Visible = false;
                dtpDate.Visible = true;
            }
            else if (strRep == "SALES_STAFF_ALL")
            {
                this.Text = "HR :: Staff Report";
                chkAll.Visible = false;
                lblDocm.Visible = false;
                dtpDate.Visible = false;
            }
            else if (strRep == "IT_SYS_INV_CPU")
            {
                this.Text = "IT :: SYSTEM INVENTORY";
                chkAll.Visible = true;
                lblDocm.Visible = false;
                dtpDate.Visible = false;
            }
            else if (strRep == "EMP_CONTACT_DETAILS")
            {
                this.Text = "HR :: Emp Contact Details";
                chkAll.Visible = true;
                lblDocm.Visible = false;
                dtpDate.Visible = false;
            }
            else if (strRep == "SaleSummaryBulletin")
            {
                this.Text = "Process :: Sales Summary Bulletin";
                chkAll.Visible = true;
                lblDocm.Visible = false;
                dtpDate.Visible = true;
                btnReport.Text = "Process";
            }
            else
            {
                chkAll.Visible = false;
                lblDocm.Visible = true;
                dtpDate.Visible = true;
            }
            if (strRep == "STOCKPOINT_DC" || strRep == "STOCKPOINT_DCST" || strRep == "STOCKPOINT_GRN"
                || strRep == "STOCK_REC" || strRep == "VEHICLE_LOAN" || strRep == "VEHICLE_INFO"
                || strRep == "STOCK_SUMMARY" || strRep == "STOCK_LEDGER" || strRep == "STOCKPOINT_RECONSILATION"
                || strRep == "SALES_STAFF_ALL" || strRep == "SaleSummaryBulletin" || strRep == "IT_SYS_INV_CPU"
                || strRep == "EMP_CONTACT_DETAILS" || strRep == "SERVICE_CONSOLIDATION" || strRep == "SP_PENDING_DC")
            {
                FillBranchList();
            }
            else
            {
                FillGLList();
            }
        }

        private void FillBranchList()
        {
            objUtil = new UtilityDB();
            DataTable dt = null;
            //string sqlText = "";
            try
            {
                if (strRep == "STOCKPOINT_DC" || strRep == "STOCKPOINT_DCST" || strRep == "STOCKPOINT_GRN" || strRep == "STOCK_REC" || strRep == "STOCK_SUMMARY" || strRep == "STOCK_LEDGER" || strRep == "STOCKPOINT_RECONSILATION" || strRep == "SP_PENDING_DC")                
                    dt = objUtil.UserBranch(CommonData.LogUserId,"SP");
                else if (strRep == "VEHICLE_LOAN" || strRep == "VEHICLE_INFO" || strRep == "SALES_STAFF_ALL" || strRep == "SaleSummaryBulletin" || strRep == "SERVICE_CONSOLIDATION")
                    dt = objUtil.UserBranch(CommonData.LogUserId,"BR");
                else
                    dt = objUtil.dtUserBranch(CommonData.LogUserId);
                clbGLList.Items.Clear();
                foreach (DataRow dataRow in dt.Rows)
                {
                    if (dataRow["branch_Code"] + "" != "")
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["branch_Code"].ToString();
                        oclBox.Text = dataRow["branch_name"].ToString();
                        clbGLList.Items.Add(oclBox);
                        oclBox = null;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objStfLvl = null;
            }
        }

        private void FillGLList()
        {
            objStfLvl = new StaffLevel();
            DataTable dt = null;
            try
            {

                dt = objStfLvl.BranchGLList_Get(CommonData.CompanyCode, CommonData.BranchCode, Convert.ToDateTime(dtpDate.Value).ToString("MMMyyyy")).Tables[0];
                clbGLList.Items.Clear();
                foreach (DataRow dataRow in dt.Rows)
                {
                    if (dataRow["ECODE"] + "" != "")
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["ECODE"].ToString();
                        oclBox.Text = dataRow["ENAME"].ToString();
                        clbGLList.Items.Add(oclBox);
                        oclBox = null;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                objStfLvl = null;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            SearchEcode(txtDsearch.Text.ToString(), clbGLList);
        }

        private void SearchEcode(string searchString, CheckedListBox cbEcodes)
        {
            if (searchString.Trim().Length > 0)
            {
                for (int i = 0; i < cbEcodes.Items.Count; i++)
                {
                    if (cbEcodes.Items[i].ToString() == "System.Data.DataRowView")  // for listbox search
                    {
                        if (((System.Data.DataRowView)(cbEcodes.Items[i])).Row.ItemArray[1].ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            cbEcodes.SetSelected(i, true);
                            break;
                        }
                        else
                            cbEcodes.SetSelected(i, false);

                    }
                    else  // for checkbox list search
                    {
                        if (cbEcodes.Items[i].ToString().IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            cbEcodes.SetSelected(i, true);
                            break;
                        }
                        else
                            cbEcodes.SetSelected(i, false);

                    }

                }
            }
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            if (strRep != "STOCKPOINT_DC" && strRep != "STOCKPOINT_DCST" && strRep != "STOCKPOINT_GRN"
                && strRep != "STOCK_SUMMARY" && strRep != "STOCK_LEDGER" && strRep != "STOCKPOINT_RECONSILATION"
                && strRep != "SALES_STAFF_ALL" && strRep != "SaleSummaryBulletin" && strRep != "SERVICE_CONSOLIDATION" && strRep != "SP_PENDING_DC")
            {
                FillGLList();
            }
           
        }

        private void clbGLList_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            if (strRep != "VEHICLE_LOAN" && strRep != "VEHICLE_INFO" && strRep != "STOCK_SUMMARY" 
                && strRep != "STOCK_LEDGER" && strRep != "SaleSummaryBulletin" && strRep != "IT_SYS_INV_CPU"
                && strRep != "EMP_CONTACT_DETAILS" &&  strRep != "SERVICE_CONSOLIDATION")
            {
                for (int i = 0; i < clbGLList.Items.Count; i++)
                {
                    if (e.Index != i)
                        clbGLList.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
            
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                if (strRep == "STOCKPOINT_DC")
                {
                    string[] strBranchCode;
                    strBranchCode = ((SSAdmin.NewCheckboxListItem)(clbGLList.SelectedItem)).Tag.ToString().Split('@');
                    CommonData.ViewReport = "STOCKPOINT_DC";
                    ReportViewer objReportview = new ReportViewer(strBranchCode[1], strBranchCode[0], Convert.ToDateTime(dtpDate.Value).ToString("MMMyyyy"),"");
                    objReportview.Show();
                }
                else if (strRep == "SP_PENDING_DC")
                {
                    string[] strBranchCode;
                    strBranchCode = ((SSAdmin.NewCheckboxListItem)(clbGLList.SelectedItem)).Tag.ToString().Split('@');
                    CommonData.ViewReport = "STOCKPOINT_DC";
                    ReportViewer objReportview = new ReportViewer(strBranchCode[1], strBranchCode[0], Convert.ToDateTime(dtpDate.Value).ToString("MMMyyyy"),"PENDING");
                    objReportview.Show();
                }
                else if (strRep == "STOCKPOINT_DCST")
                {
                    string[] strBranchCode;
                    strBranchCode = ((SSAdmin.NewCheckboxListItem)(clbGLList.SelectedItem)).Tag.ToString().Split('@');
                    CommonData.ViewReport = "STOCKPOINT_DCST";
                    ReportViewer objReportview = new ReportViewer(strBranchCode[1], strBranchCode[0], Convert.ToDateTime(dtpDate.Value).ToString("MMMyyyy"));
                    objReportview.Show();
                }
                else if (strRep == "STOCKPOINT_GRN")
                {
                    string[] strBranchCode;
                    strBranchCode = ((SSAdmin.NewCheckboxListItem)(clbGLList.SelectedItem)).Tag.ToString().Split('@');
                    CommonData.ViewReport = "STOCKPOINT_GRN";
                    ReportViewer objReportview = new ReportViewer(strBranchCode[1], strBranchCode[0], Convert.ToDateTime(dtpDate.Value).ToString("MMMyyyy"));
                    objReportview.Show();
                }
                else if (strRep == "STOCK_REC")
                {
                    string[] strBranchCode;
                    strBranchCode = ((SSAdmin.NewCheckboxListItem)(clbGLList.SelectedItem)).Tag.ToString().Split('@');
                    CommonData.ViewReport = "SP_STOCK_RECONSILATION";
                    ReportViewer objReportview = new ReportViewer(strBranchCode[1], strBranchCode[0], "SUMMARY");
                    objReportview.Show();
                }
                else if (strRep == "STOCKPOINT_RECONSILATION")
                {
                    string[] strBranchCode;
                    strBranchCode = ((SSAdmin.NewCheckboxListItem)(clbGLList.SelectedItem)).Tag.ToString().Split('@');
                    CommonData.ViewReport = "STOCKPOINT_STOCK_RECONSILATION";
                    ReportViewer objReportview = new ReportViewer(strBranchCode[1], strBranchCode[0], Convert.ToDateTime(dtpDate.Value).ToString("MMMyyyy"));
                    objReportview.Show();
                }
                else if (strRep == "VEHICLE_LOAN")
                {
                    string strBranchCode = "";
                    string[] arrBranch;
                    for (int i = 0; i < clbGLList.Items.Count; i++)
                    {
                        if (clbGLList.GetItemCheckState(i) == CheckState.Checked)
                        {
                            //arrBranch = clbGLList.Items[i].Tag.ToString().Split('@');
                            arrBranch = ((SSAdmin.NewCheckboxListItem)(clbGLList.Items[i])).Tag.ToString().Split('@');
                            if (strBranchCode != "")
                                strBranchCode += ",";
                            strBranchCode += arrBranch[0];
                        }
                        arrBranch = null;
                    }
                    //strBranchCode = ((SSAdmin.NewCheckboxListItem)(clbGLList.SelectedItem)).Tag.ToString().Split('@');
                    CommonData.ViewReport = "VEHICLE_LOAN_RECOVERY_SUMMARY";
                    ReportViewer objReportview = new ReportViewer(strBranchCode, strBranchCode, CommonData.FinancialYear);
                    objReportview.Show();
                }
                else if (strRep == "VEHICLE_INFO")
                {
                    string strBranchCode = "";
                    string[] arrBranch;
                    for (int i = 0; i < clbGLList.Items.Count; i++)
                    {
                        if (clbGLList.GetItemCheckState(i) == CheckState.Checked)
                        {
                            //arrBranch = clbGLList.Items[i].Tag.ToString().Split('@');
                            arrBranch = ((SSAdmin.NewCheckboxListItem)(clbGLList.Items[i])).Tag.ToString().Split('@');
                            if (strBranchCode != "")
                                strBranchCode += ",";
                            strBranchCode += arrBranch[0];
                        }
                        arrBranch = null;
                    }
                    //strBranchCode = ((SSAdmin.NewCheckboxListItem)(clbGLList.SelectedItem)).Tag.ToString().Split('@');
                    CommonData.ViewReport = "VEHICLE_INFORMATION";
                    ReportViewer objReportview = new ReportViewer(strBranchCode, strBranchCode, CommonData.FinancialYear);
                    objReportview.Show();
                }
                else if (strRep == "SaleSummaryBulletin")
                {
                    string strBranchCode = "";
                    string strCompCode = "";
                    string[] arrBranch;
                    for (int i = 0; i < clbGLList.Items.Count; i++)
                    {
                        if (clbGLList.GetItemCheckState(i) == CheckState.Checked)
                        {
                            //arrBranch = clbGLList.Items[i].Tag.ToString().Split('@');
                            arrBranch = ((SSAdmin.NewCheckboxListItem)(clbGLList.Items[i])).Tag.ToString().Split('@');
                            if (strBranchCode != "")
                            {
                                strBranchCode += ",";
                                strCompCode += ",";
                            }
                            strBranchCode += arrBranch[0];
                            strCompCode += arrBranch[1];
                        }
                        arrBranch = null;
                    }
                    objDB = new SQLDB();
                    string sqlText = " EXEC Process_SalesBulletinSummary '" + strCompCode + "','" + strBranchCode + "','"+Convert.ToDateTime(dtpDate.Value).ToString("MMMyyyy").ToUpper()+"',''";
                    try
                    {
                        objDB.ExecuteSaveData(sqlText);
                        MessageBox.Show("Process Completed", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        objDB = null;
                    }
                    //strBranchCode = ((SSAdmin.NewCheckboxListItem)(clbGLList.SelectedItem)).Tag.ToString().Split('@');
                    //CommonData.ViewReport = "VEHICLE_INFORMATION";
                    //ReportViewer objReportview = new ReportViewer(strBranchCode, strBranchCode, CommonData.FinancialYear);
                    //objReportview.Show();
                }
                else if (strRep == "STOCK_SUMMARY")
                {
                    string strBranchCode = "";
                    string strCompany = "";
                    string[] arrBranch;
                    for (int i = 0; i < clbGLList.Items.Count; i++)
                    {
                        if (clbGLList.GetItemCheckState(i) == CheckState.Checked)
                        {
                            //arrBranch = clbGLList.Items[i].Tag.ToString().Split('@');
                            arrBranch = ((SSAdmin.NewCheckboxListItem)(clbGLList.Items[i])).Tag.ToString().Split('@');
                            if (strBranchCode != "")
                                strBranchCode += ",";
                            if (strCompany != "")
                                strCompany += ",";
                            strBranchCode += arrBranch[0];
                            strCompany += arrBranch[1];
                        }
                        arrBranch = null;
                    }
                    //strBranchCode = ((SSAdmin.NewCheckboxListItem)(clbGLList.SelectedItem)).Tag.ToString().Split('@');
                    CommonData.ViewReport = "STOCK_POINT_STOCK_SUMMARY";
                    ReportViewer objReportview = new ReportViewer(strCompany, strBranchCode, Convert.ToDateTime(dtpDate.Value).ToString("MMMyyyy"),"SUMMARY");
                    objReportview.Show();
                }
                else if (strRep == "STOCK_LEDGER")
                {
                    string strBranchCode = "";
                    string strCompany = "";
                    string[] arrBranch;
                    for (int i = 0; i < clbGLList.Items.Count; i++)
                    {
                        if (clbGLList.GetItemCheckState(i) == CheckState.Checked)
                        {
                            //arrBranch = clbGLList.Items[i].Tag.ToString().Split('@');
                            arrBranch = ((SSAdmin.NewCheckboxListItem)(clbGLList.Items[i])).Tag.ToString().Split('@');
                            if (strBranchCode != "")
                                strBranchCode += ",";
                            if (strCompany != "")
                                strCompany += ",";
                            strBranchCode += arrBranch[0];
                            strCompany += arrBranch[1];
                        }
                        arrBranch = null;
                    }
                    //strBranchCode = ((SSAdmin.NewCheckboxListItem)(clbGLList.SelectedItem)).Tag.ToString().Split('@');
                    CommonData.ViewReport = "STOCK_POINT_STOCK_LEDGER";
                    ReportViewer objReportview = new ReportViewer(strCompany, strBranchCode, Convert.ToDateTime(dtpDate.Value).ToString("MMMyyyy"),"LEDGER");
                    objReportview.Show();
                }
                else if (strRep == "SALES_STAFF_ALL")
                {
                    string[] strBranchCode;
                    strBranchCode = ((SSAdmin.NewCheckboxListItem)(clbGLList.SelectedItem)).Tag.ToString().Split('@');
                    CommonData.ViewReport = "BRANCH_WISE_SALES_STAFF_ALL";
                    ReportViewer objReportview = new ReportViewer(strBranchCode[1], strBranchCode[0], "SALES_STAFF_ALL");
                    objReportview.Show();
                }
                else if (strRep == "IT_SYS_INV_CPU")
                {
                    string strBranchCode = "";
                    string strCompany = "";
                    string[] arrBranch;
                    for (int i = 0; i < clbGLList.Items.Count; i++)
                    {
                        if (clbGLList.GetItemCheckState(i) == CheckState.Checked)
                        {
                            //arrBranch = clbGLList.Items[i].Tag.ToString().Split('@');
                            arrBranch = ((SSAdmin.NewCheckboxListItem)(clbGLList.Items[i])).Tag.ToString().Split('@');
                            if (strBranchCode != "")
                                strBranchCode += ",";
                            if (strCompany != "")
                                strCompany += ",";
                            strBranchCode += arrBranch[0];
                            strCompany += arrBranch[1];
                        }
                        arrBranch = null;
                    }
                    //strBranchCode = ((SSAdmin.NewCheckboxListItem)(clbGLList.SelectedItem)).Tag.ToString().Split('@');
                    CommonData.ViewReport = "IT_SYS_INV_CPU";
                    ReportViewer objReportview = new ReportViewer(strCompany, strBranchCode, Convert.ToDateTime(dtpDate.Value).ToString("MMMyyyy"), "DETAILED");
                    objReportview.Show();
                }
                else if (strRep == "EMP_CONTACT_DETAILS")
                {
                    string strBranchCode = "";
                    string strCompany = "";
                    string[] arrBranch;
                    for (int i = 0; i < clbGLList.Items.Count; i++)
                    {
                        if (clbGLList.GetItemCheckState(i) == CheckState.Checked)
                        {
                            //arrBranch = clbGLList.Items[i].Tag.ToString().Split('@');
                            arrBranch = ((SSAdmin.NewCheckboxListItem)(clbGLList.Items[i])).Tag.ToString().Split('@');
                            if (strBranchCode != "")
                                strBranchCode += ",";
                            if (strCompany != "")
                                strCompany += ",";
                            strBranchCode += arrBranch[0];
                            strCompany += arrBranch[1];
                        }
                        arrBranch = null;
                    }
                    //strBranchCode = ((SSAdmin.NewCheckboxListItem)(clbGLList.SelectedItem)).Tag.ToString().Split('@');
                    CommonData.ViewReport = "SSCRM_HR_REP_EMP_CONTACT_DETAILS";
                    ReportViewer objReportview = new ReportViewer(strCompany, strBranchCode, Convert.ToDateTime(dtpDate.Value).ToString("MMMyyyy"), "DETAILED");
                    objReportview.Show();
                }
                else if (strRep == "SERVICE_CONSOLIDATION")
                {
                    string strBranchCode = "";
                    string strCompany = "";
                    string[] arrBranch;
                    for (int i = 0; i < clbGLList.Items.Count; i++)
                    {
                        if (clbGLList.GetItemCheckState(i) == CheckState.Checked)
                        {
                            //arrBranch = clbGLList.Items[i].Tag.ToString().Split('@');
                            arrBranch = ((SSAdmin.NewCheckboxListItem)(clbGLList.Items[i])).Tag.ToString().Split('@');
                            if (strBranchCode != "")
                                strBranchCode += ",";
                            if (strCompany != "")
                                strCompany += ",";
                            strBranchCode += arrBranch[0];
                            strCompany += arrBranch[1];
                        }
                        arrBranch = null;
                    }
                    //strBranchCode = ((SSAdmin.NewCheckboxListItem)(clbGLList.SelectedItem)).Tag.ToString().Split('@');
                    CommonData.ViewReport = "SSCRM_REP_SERVICE_MON_CUMULATIVE";
                    ReportViewer objReportview = new ReportViewer(strCompany, strBranchCode, Convert.ToDateTime(dtpDate.Value).ToString("MMMyyyy").ToUpper(), "ALL");
                    objReportview.Show();
                }
                else
                {
                    childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, Convert.ToDateTime(dtpDate.Value).ToString("MMMyyyy"), ((SSAdmin.NewCheckboxListItem)(clbGLList.SelectedItem)).Tag.ToString());
                    CommonData.ViewReport = "GC_GL_WISE_PRODUCT_RECONSILATION";
                    childReportViewer.Show();
                }
            }
        }

        private bool CheckData()
        {
            bool blVil = false;
            for (int i = 0; i < clbGLList.Items.Count; i++)
            {
                if (clbGLList.GetItemCheckState(i) == CheckState.Checked)
                {
                    blVil = true;
                }
            }
            return blVil;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
            {
                for (int i = 0; i < clbGLList.Items.Count; i++)
                {
                    clbGLList.SetItemCheckState(i, CheckState.Checked);
                }
            }
            else
            {
                for (int i = 0; i < clbGLList.Items.Count; i++)
                {
                    clbGLList.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (strRep == "STOCKPOINT_RECONSILATION")
            {
                #region BR WISE STOCK POINT RECONCILATION
                try
                {

                    string[] strBranchCode;
                    strBranchCode = ((SSAdmin.NewCheckboxListItem)(clbGLList.SelectedItem)).Tag.ToString().Split('@');
                    objExDb = new ExcelDB();                   
                    DataTable dtExcel = objExDb.BRWiseStockReconsilation(strBranchCode[1], strBranchCode[0], Convert.ToDateTime(dtpDate.Value).ToString("MMMyyyy"),"").Tables[0];
                    objExDb = null;

                    if (dtExcel.Rows.Count > 0)
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;

                        Excel.Range rg = worksheet.get_Range("A4", "Y4");

                        Excel.Range rgData = worksheet.get_Range("A4", "Y" + (dtExcel.Rows.Count + 4).ToString());
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


                        rg = worksheet.get_Range("A1", "U1");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "STOCK RECONSILATION";
                        rg.Font.Bold = true; rg.Font.Size = 16;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;


                        rgData = worksheet.get_Range("W1", "Y1");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 14;
                        rgData.Value2 = "DOC MONTH : " + dtpDate.Value.ToString("MMMyyyy");
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.HorizontalAlignment = Excel.Constants.xlCenter;



                        rg = worksheet.get_Range("A4", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Sl.No";

                        rg = worksheet.get_Range("B4", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Value2 = " SP NAME";


                        rg = worksheet.get_Range("C4", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Value2 = " PRODUCT NAME";

                        rg = worksheet.get_Range("D2", "F3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "OPENING STOCK";
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 21;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("D4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "GOOD";

                        rg = worksheet.get_Range("E4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "DAMAGE";

                        rg = worksheet.get_Range("F4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "TOTAL";



                        rg = worksheet.get_Range("G2", "I3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "RECEIPTS(GRN)";
                        rg.Font.ColorIndex = 3;
                        rg.Interior.ColorIndex = 8;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("G4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "GOOD";

                        rg = worksheet.get_Range("H4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "DAMAGE";

                        rg = worksheet.get_Range("I4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "TOTAL";


                        rg = worksheet.get_Range("J2", "P2");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "ISSUES";
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 5;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;



                        rg = worksheet.get_Range("J3", "J4");
                        rg.Merge(Type.Missing);
                        rg.WrapText = true;
                        rg.Interior.ColorIndex = 31;
                        rg.Font.ColorIndex = 2;
                        rg.Value2 = " DC GOOD";

                        rg = worksheet.get_Range("K3", "M3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "DCST";
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 9;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;


                        rg = worksheet.get_Range("K4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "GOOD";

                        rg = worksheet.get_Range("L4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "DAMAGE";

                        rg = worksheet.get_Range("M4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "TOTAL";

                        rg = worksheet.get_Range("N3", "P3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "TOTAL";
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 21;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("N4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "GOOD";

                        rg = worksheet.get_Range("O4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "DAMAGE";

                        rg = worksheet.get_Range("P4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "TOTAL";


                        //rg = worksheet.get_Range("Q2", "Q3");
                        //rg.Merge(Type.Missing);
                        //rg.Value2 = "";
                        //rg.Font.ColorIndex = 2;



                        //rg = worksheet.get_Range("Q4", Type.Missing);

                        rg = worksheet.get_Range("Q2", "Q4");
                        rg.Merge(Type.Missing);
                        rg.Interior.ColorIndex = 31;
                        rg.Font.ColorIndex = 2;                      
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.Value2 = "DSPU_ISSUE DAMAGE";
                        rg.WrapText = true;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;


                        //rg = worksheet.get_Range("R2", "R3");
                        //rg.Merge(Type.Missing);
                        //rg.Value2 = "";
                        //rg.Font.ColorIndex = 2;


                        //rg = worksheet.get_Range("R4", Type.Missing);
                        //rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("R2", "R4");
                        rg.Merge(Type.Missing);
                        rg.Interior.ColorIndex = 21;
                        rg.Font.ColorIndex = 2;
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.Value2 = "GRPU RECEIPT GOOD";
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;


                        rg = worksheet.get_Range("S2", "T3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "INTERNAL CONVERSION";
                        rg.WrapText = true;
                        rg.Font.ColorIndex = 3;
                        rg.Interior.ColorIndex = 8;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("S4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "G2D";

                        rg = worksheet.get_Range("T4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "D2G";


                        //rg = worksheet.get_Range("U2", "U3");
                        //rg.Merge(Type.Missing);
                        //rg.Value2 = "";


                        //rg = worksheet.get_Range("U4", Type.Missing);
                        //rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("U2", "U4");
                        rg.Merge(Type.Missing);
                        rg.Interior.ColorIndex = 21;
                        rg.Font.ColorIndex = 2;
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.Value2 = "SHORTAGE/WRITE OFF";
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;


                        //rg = worksheet.get_Range("V2", "V3");
                        //rg.Merge(Type.Missing);
                        //rg.Value2 = "";


                        //rg = worksheet.get_Range("V4", Type.Missing);
                        //rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("V2", "V4");
                        rg.Merge(Type.Missing);
                        rg.Interior.ColorIndex = 31;
                        rg.Font.ColorIndex = 2;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.Value2 = "EXCESS";
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;




                        rg = worksheet.get_Range("W2", "Y3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "CLOSING STOCK";
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 21;
                        rg.Font.Bold = true; rg.Font.Size = 12;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("W4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "GOOD";

                        rg = worksheet.get_Range("X4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "DAMAGE";

                        rg = worksheet.get_Range("Y4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Value2 = "TOTAL";

                        int RowCounter = 1;


                        foreach (DataRow dr in dtExcel.Rows)
                        {
                            int i = 1;
                            worksheet.Cells[RowCounter + 4, i++] = RowCounter;
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_branch_name"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_product_name"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_open_stock_good"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_open_stock_damage"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_total_open_stock"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_recieved_good"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_recieved_damage"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_recieved_total"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_stock_dispatched_DC"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_stock_dispatched_DCST_Good"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_stock_dispatched_DCST_Dmg"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_stock_dispatched_DCST"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_stock_dispatched_good"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_stock_dispatched_dmg"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_stock_dispatched"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_refill_issue"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_refill_recieved"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_interconv_to_bad"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_interconv_to_good"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_remarks_shortage"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_remarks_excess"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_closing_stock_good"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_closing_stock_damage"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["rs_closing_stock_total"].ToString();                  

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
}
