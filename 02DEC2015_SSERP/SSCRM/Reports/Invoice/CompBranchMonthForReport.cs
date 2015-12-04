using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSCRMDB;
using SSAdmin;
using SSTrans;
using Excel = Microsoft.Office.Interop.Excel;
namespace SSCRM
{
    public partial class CompBranchMonthForReport : Form
    {
        private InvoiceDB objInv = null;
        private Master objMaster = null;
        private SQLDB objDA = null;
        private UtilityDB objUtilityDB = null;
        private ReportViewer childReportViewer = null;
        ExcelDB objExcelDB = null;
        private string sRep_Type = "";
        private int iForm = 0;

        public string branches = string.Empty;
        public string documentMonth = string.Empty;
        public string finyear = string.Empty;
        public string company = string.Empty;
        private string Products = "";


        public CompBranchMonthForReport()
        {
            InitializeComponent();
        }
        public CompBranchMonthForReport(string sRepType)
        {
            InitializeComponent();
            sRep_Type = sRepType;
        }
        public CompBranchMonthForReport(int iRepType)
        {
            InitializeComponent();
            iForm = iRepType;
        }
        private void CompBranchMonthForReport_Load(object sender, EventArgs e)
        {
            if (iForm == 2)
            {
                lblAvg.Visible = true;
                lblFrom.Visible = true;
                lblTo.Visible = true;
                txtFrom.Visible = true;
                txtTo.Visible = true;
            }
            else if (iForm == 3)
            {
                lblAvg.Visible = false;
                lblFrom.Visible = false;
                lblTo.Visible = false;
                txtFrom.Visible = false;
                txtTo.Visible = false;
            }
            else if (iForm == 4)
            {
                lblAvg.Visible = false;
                lblFrom.Visible = false;
                lblTo.Visible = false;
                txtFrom.Visible = false;
                txtTo.Visible = false;
            }
            else if (iForm == 5)
            {
                lblAvg.Visible = false;
                lblFrom.Visible = false;
                lblTo.Visible = false;
                txtFrom.Visible = false;
                txtTo.Visible = false;
            }
            else if (iForm == 6)
            {
                lblAvg.Visible = false;
                lblFrom.Visible = false;
                lblTo.Visible = false;
                txtFrom.Visible = false;
                txtTo.Visible = false;
            }
            else if (iForm == 7)
            {
                lblAvg.Visible = false;
                lblFrom.Visible = false;
                lblTo.Visible = false;
                txtFrom.Visible = false;
                txtTo.Visible = false;
            }
            else if (iForm == 8)
            {
                btnDownload.Visible = false;
                lblAvg.Visible = false;
                lblFrom.Visible = false;
                lblTo.Visible = false;
                txtFrom.Visible = false;
                txtTo.Visible = false;
            }
            else if (iForm == 9)
            {
                btnDownload.Visible = false;
                lblAvg.Visible = false;
                lblFrom.Visible = false;
                lblTo.Visible = false;
                txtFrom.Visible = false;
                txtTo.Visible = false;
            }
            else if (iForm == 10)
            {                
                lblAvg.Visible = false;
                lblFrom.Visible = false;
                lblTo.Visible = false;
                txtFrom.Visible = false;
                txtTo.Visible = false;
            }
            else
            {
                lblAvg.Visible = false;
                lblFrom.Visible = false;
                lblTo.Visible = false;
                txtFrom.Visible = false;
                txtTo.Visible = false;
            }

            FillBranches();
            FillDocumentMonths();
            FillReportType();
        }

        private void FillReportType()
        {
            objDA = new SQLDB();
            DataTable dtProducts = new DataTable();
            string strCmd = "";
            if (iForm == 0)
            {
                if (sRep_Type == "SP_CHECKLIST")
                {
                    NewCheckboxListItem oclBox = new NewCheckboxListItem();
                    oclBox.Tag = "CHECKLIST";
                    oclBox.Text = "CHECKLIST";
                    clbReport.Items.Add(oclBox);
                    oclBox = null;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("type", typeof(string));
                    dt.Columns.Add("name", typeof(string));

                    dt.Rows.Add("CHECKLIST", "STOCKPOINT CHECKLIST");

                    cbReport.DataSource = dt;
                    cbReport.DisplayMember = "name";
                    cbReport.ValueMember = "type";
                }
                else
                {
                    NewCheckboxListItem oclBox = new NewCheckboxListItem();
                    oclBox.Tag = "SUMMARY";
                    oclBox.Text = "SUMMARY";
                    clbReport.Items.Add(oclBox);
                    oclBox = null;
                    //NewCheckboxListItem oclBox1 = new NewCheckboxListItem();
                    //oclBox1.Tag = "DETAILED";
                    //oclBox1.Text = "DETAILED";
                    //clbReport.Items.Add(oclBox1);
                    //oclBox1 = null;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("type", typeof(string));
                    dt.Columns.Add("name", typeof(string));

                    dt.Rows.Add("PRODUCT", "SALES SUMMERY BY PRODUCT");
                    dt.Rows.Add("CATEGORY", "SALES PRODUCT BY CATEGORY");
                    dt.Rows.Add("RAWDATA", "SALES RAW DATA FOR EXCELL EXPORT");
                    cbReport.DataSource = dt;
                    cbReport.DisplayMember = "name";
                    cbReport.ValueMember = "type";
                }
            }
            else if (iForm == 1)
            {
                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                oclBox.Tag = "SUMMARY";
                oclBox.Text = "SUMMARY";
                clbReport.Items.Add(oclBox);
                oclBox = null;

                DataTable dt = new DataTable();
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("name", typeof(string));

                dt.Rows.Add("BRANCH_REVENUE_SUMMARY", "REVENUE SUMMERY BY BRANCH");
                //dt.Rows.Add("CATEGORY", "SALES PRODUCT BY CATEGORY");
                //dt.Rows.Add("RAWDATA", "SALES RAW DATA FOR EXCELL EXPORT");
                cbReport.DataSource = dt;
                cbReport.DisplayMember = "name";
                cbReport.ValueMember = "type";
            }
            else if (iForm == 2)
            {
                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                oclBox.Tag = "SUMMARY";
                oclBox.Text = "SUMMARY";
                clbReport.Items.Add(oclBox);
                oclBox = null;

                DataTable dt = new DataTable();
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("name", typeof(string));

                dt.Rows.Add("EMPLOYEE_PERFORMANCE_CUMULATIVE", "EMPLOYEE PERFORMANCE :: CUMULATIVE");
                dt.Rows.Add("EMPLOYEE_PERFORMANCE_CUMULATIVE_EXCEL", "EMPLOYEE PERFORMANCE :: CUMULATIVE :: EXCEL");
                dt.Rows.Add("EMPLOYEE_PERFORMANCE_CUMULATIVE_TEAK_EXCEL", "EMPLOYEE TEAK SALES :: CUMULATIVE :: EXCEL");
                dt.Rows.Add("TEAK_SALES_AGAINST_QTY", "CUSTOMERS AT DIFF TEAK SALE QTY");
                //dt.Rows.Add("CATEGORY", "SALES PRODUCT BY CATEGORY");
                //dt.Rows.Add("RAWDATA", "SALES RAW DATA FOR EXCELL EXPORT");
                cbReport.DataSource = dt;
                cbReport.DisplayMember = "name";
                cbReport.ValueMember = "type";
            }
            else if (iForm == 3)
            {
                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                oclBox.Tag = "SUMMARY";
                oclBox.Text = "SUMMARY";
                clbReport.Items.Add(oclBox);
                oclBox = null;

                DataTable dt = new DataTable();
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("name", typeof(string));

                dt.Rows.Add("GROUP AND INDIVIDUAL PERFORMANCE CROSSTAB", "GROUP AND INDIVIDUAL PERFORMANCE CROSSTAB");
                //dt.Rows.Add("CATEGORY", "SALES PRODUCT BY CATEGORY");
                //dt.Rows.Add("RAWDATA", "SALES RAW DATA FOR EXCELL EXPORT");
                cbReport.DataSource = dt;
                cbReport.DisplayMember = "name";
                cbReport.ValueMember = "type";
            }
            else if (iForm == 4)
            {
                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                oclBox.Tag = "SUMMARY";
                oclBox.Text = "SUMMARY";
                clbReport.Items.Add(oclBox);
                oclBox = null;

                DataTable dt = new DataTable();
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("name", typeof(string));

                dt.Rows.Add("AUDIT_QUERY_REGISTER", "AUDIT QUERY REGISTER");
                //dt.Rows.Add("CATEGORY", "SALES PRODUCT BY CATEGORY");
                //dt.Rows.Add("RAWDATA", "SALES RAW DATA FOR EXCELL EXPORT");
                cbReport.DataSource = dt;
                cbReport.DisplayMember = "name";
                cbReport.ValueMember = "type";
            }
            else if (iForm == 5)
            {
                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                oclBox.Tag = "SUMMARY";
                oclBox.Text = "SUMMARY";
                clbReport.Items.Add(oclBox);
                oclBox = null;

                DataTable dt = new DataTable();
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("name", typeof(string));

                dt.Rows.Add("COMPANY DISTRICT WISE GROUPS", "COMPANY DISTRICT WISE GROUPS");
                //dt.Rows.Add("CATEGORY", "SALES PRODUCT BY CATEGORY");
                //dt.Rows.Add("RAWDATA", "SALES RAW DATA FOR EXCELL EXPORT");
                cbReport.DataSource = dt;
                cbReport.DisplayMember = "name";
                cbReport.ValueMember = "type";
            }
            else if (iForm == 6)
            {
                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                oclBox.Tag = "DETAILED";
                oclBox.Text = "DETAILED";
                clbReport.Items.Add(oclBox);
                oclBox = null;

                DataTable dt = new DataTable();
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("name", typeof(string));

                dt.Rows.Add("EMPLOYEE BANK ACC DETAILS", "EMPLOYEE BANK ACC DETAILS");
                //dt.Rows.Add("CATEGORY", "SALES PRODUCT BY CATEGORY");
                //dt.Rows.Add("RAWDATA", "SALES RAW DATA FOR EXCELL EXPORT");
                cbReport.DataSource = dt;
                cbReport.DisplayMember = "name";
                cbReport.ValueMember = "type";
            }
            else if (iForm == 7)
            {
                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                oclBox.Tag = "SUMMARY";
                oclBox.Text = "SUMMARY";
                clbReport.Items.Add(oclBox);
                oclBox = null;

                DataTable dt = new DataTable();
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("name", typeof(string));

                dt.Rows.Add("SERVICE CHECKLIST", "SERVICE CHECKLIST");
                //dt.Rows.Add("CATEGORY", "SALES PRODUCT BY CATEGORY");
                //dt.Rows.Add("RAWDATA", "SALES RAW DATA FOR EXCELL EXPORT");
                cbReport.DataSource = dt;
                cbReport.DisplayMember = "name";
                cbReport.ValueMember = "type";
            }

            else if (iForm == 8)
            {
                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                oclBox.Tag = "DETAILED";
                oclBox.Text = "DETAILED";
                clbReport.Items.Add(oclBox);
                oclBox = null;

                DataTable dt = new DataTable();
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("name", typeof(string));

                dt.Rows.Add("TRAINING PROGRAM DETAILS", "TRAINING PROGRAM DETAILS");
                dt.Rows.Add("TRAINING PROGRAMS SUMMARY", "TRAINING PROGRAMS SUMMARY");
               
                cbReport.DataSource = dt;
                cbReport.DisplayMember = "name";
                cbReport.ValueMember = "type";
            }

            else if (iForm == 9)
            {
                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                oclBox.Tag = "SUMMARY";
                oclBox.Text = "SUMMARY";
                clbReport.Items.Add(oclBox);
                oclBox = null;

                DataTable dt = new DataTable();
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("name", typeof(string));

                //dt.Rows.Add("TRAINING PROGRAM DETAILS", "TRAINING PROGRAM DETAILS");
                dt.Rows.Add("STOCK RECONSILATION SUMMARY", "STOCK RECONSILATION SUMMARY");

                cbReport.DataSource = dt;
                cbReport.DisplayMember = "name";
                cbReport.ValueMember = "type";
            }
            else if (iForm == 10)
            {
                clbReport.Items.Clear();
                strCmd = "SELECT distinct PM_PRODUCT_ID ValMember,PM_PRODUCT_NAME DisMember "+
                          " from PRODUCT_MAS WHERE PM_CATEGORY_ID='001'";
                dtProducts = objDA.ExecuteDataSet(strCmd).Tables[0];
                if (dtProducts.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dtProducts.Rows)
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["ValMember"].ToString();
                        oclBox.Text = dataRow["DisMember"].ToString();

                        clbReport.Items.Add(oclBox);

                        oclBox = null;
                    }
                   
                }                               

                DataTable dt = new DataTable();
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("name", typeof(string));

                dt.Rows.Add("BRANCH AND DOCUMENT MONTH WISE REPLACEMENT SUMMARY", "BRANCH AND DOCUMENT MONTH WISE REPLACEMENT SUMMARY");
                dt.Rows.Add("CURRENT MONTH WISE REPLACEMENT SUMMARY", "CURRENT MONTH WISE REPLACEMENT SUMMARY");


                cbReport.DataSource = dt;
                cbReport.DisplayMember = "name";
                cbReport.ValueMember = "type";
            }
            else if (iForm == 11)
            {
                NewCheckboxListItem oclBox = new NewCheckboxListItem();
                oclBox.Tag = "SUMMARY";
                oclBox.Text = "SUMMARY";
                clbReport.Items.Add(oclBox);
                oclBox = null;

                DataTable dt = new DataTable();
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("name", typeof(string));

                dt.Rows.Add("BRANCH_YEARLY_SUMMARY", "AFC SUMMERY BY BRANCH");
                //dt.Rows.Add("CATEGORY", "SALES PRODUCT BY CATEGORY");
                //dt.Rows.Add("RAWDATA", "SALES RAW DATA FOR EXCELL EXPORT");
                cbReport.DataSource = dt;
                cbReport.DisplayMember = "name";
                cbReport.ValueMember = "type";
            }
        }

        private void FillDocumentMonths()
        {
            objInv = new InvoiceDB();
            objDA = new SQLDB();
            DataSet ds = new DataSet();
            DataSet dschild = new DataSet();
            string strSQL = "SELECT DISTINCT FY_FIN_YEAR FROM FIN_YEAR";
            ds = objDA.ExecuteDataSet(strSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    tvDocumentMonth.Nodes.Add(ds.Tables[0].Rows[i]["FY_FIN_YEAR"].ToString(), ds.Tables[0].Rows[i]["FY_FIN_YEAR"].ToString());
                    strSQL = "SELECT DISTINCT DOCUMENT_MONTH,start_date FROM DOCUMENT_MONTH WHERE FIN_YEAR = '" + ds.Tables[0].Rows[i]["FY_FIN_YEAR"].ToString() + "' ORDER BY START_DATE ASC";
                    dschild = objDA.ExecuteDataSet(strSQL);
                    if (dschild.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                        {
                            tvDocumentMonth.Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["DOCUMENT_MONTH"].ToString(), dschild.Tables[0].Rows[j]["DOCUMENT_MONTH"].ToString());
                        }
                    }
                }

            }
        }
        private void FillBranches()
        {
            objInv = new InvoiceDB();
            objUtilityDB = new UtilityDB();
            DataSet ds = new DataSet();
            if (CommonData.LogUserId.ToUpper() == "ADMIN")
                ds = objInv.AdminBranchCursor_Get("", "", "PARENT");
            else if(iForm ==5)
                ds = objInv.AdminBranchCursor_Get("", "", "PARENT");
            else
                ds = objUtilityDB.UserBranchCursor_Get(CommonData.LogUserId, "", "", "PARENT");
            TreeNode tNode;
            //tNode = tvProducts.Nodes.Add("Products");
            //tvBranches.Nodes[0].Nodes.Add("Single Product");
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    tvBranches.Nodes.Add(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), ds.Tables[0].Rows[i]["COMPANY_NAME"].ToString());
                    DataSet dschild = new DataSet();
                    if (CommonData.LogUserId.ToUpper() == "ADMIN")
                    {
                        if (sRep_Type == "SP_CHECKLIST" || iForm == 9)
                            dschild = objInv.AdminBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "SP", "CHILD");
                        else
                            dschild = objInv.AdminBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "BR", "CHILD");
                    }
                    else if (iForm == 5 || iForm == 9 || iForm == 10)
                    {
                        dschild = objInv.AdminBranchCursor_Get(ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "BR", "CHILD");
                    }
                    else
                    {
                        if (sRep_Type == "SP_CHECKLIST" || iForm == 9)
                            dschild = objUtilityDB.UserBranchCursor_Get(CommonData.LogUserId, ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "SP", "CHILD");
                        else
                            dschild = objUtilityDB.UserBranchCursor_Get(CommonData.LogUserId, ds.Tables[0].Rows[i]["COMPANY_CODE"].ToString(), "BR", "CHILD");
                    }
                    //tvBranches.Nodes[i].Nodes.Add("BRANCHES" + "(" + dschild.Tables[0].Rows.Count + ")");
                    if (dschild.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dschild.Tables[0].Rows.Count; j++)
                        {
                            tvBranches.Nodes[i].Nodes.Add(dschild.Tables[0].Rows[j]["BRANCH_CODE"].ToString(), dschild.Tables[0].Rows[j]["BRANCH_NAME"].ToString());
                        }
                    }
                }
            }

            //this.tvBranches.SelectedNode = tNode.Nodes[1];
            //this.tvBranches.SelectedNode.Expand();
        }

     

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void tvBranches_AfterCheck(object sender, TreeViewEventArgs e)
        {
            tvBranches.BeginUpdate();

            foreach (TreeNode Node in e.Node.Nodes)
            {
                Node.Checked = e.Node.Checked;
            }

            tvBranches.EndUpdate();
        }

        private void tvDocumentMonth_AfterCheck(object sender, TreeViewEventArgs e)
        {
            tvDocumentMonth.BeginUpdate();
            foreach (TreeNode Node in e.Node.Nodes)
            {
                Node.Checked = e.Node.Checked;
            }
            tvDocumentMonth.EndUpdate();
        }


        private void clbReport_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            if (iForm == 10 )
            {

            }
            else
            {
                for (int i = 0; i < clbReport.Items.Count; i++)
                {
                    if (e.Index != i)
                        clbReport.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (checkdata())
            {
                GetSelectedControlsIDs();
                GetSelectedProductIds();
                if (iForm == 0)
                {
                    if (sRep_Type == "SP_CHECKLIST")
                    {
                        childReportViewer = new ReportViewer(company, branches, documentMonth, "CHECK LIST");
                        CommonData.ViewReport = "STOCKPOINT_CHECKLIST";
                        childReportViewer.Show();
                    }
                    else
                    {
                        if (cbReport.SelectedValue.ToString() == "CATEGORY")
                        {
                            childReportViewer = new ReportViewer(company, branches, documentMonth, clbReport.SelectedItem.ToString());
                            CommonData.ViewReport = "SALES_PROD_SUMMARY_BY_CATG";
                            childReportViewer.Show();
                        }
                        else if (cbReport.SelectedValue.ToString() == "PRODUCT")
                        {
                            childReportViewer = new ReportViewer(company, branches, documentMonth, clbReport.SelectedItem.ToString());
                            CommonData.ViewReport = "SALES_PROD_SUMMARY_BY_PRODUCT";
                            childReportViewer.Show();
                        }
                        else if (cbReport.SelectedValue.ToString() == "RAWDATA")
                        {
                            childReportViewer = new ReportViewer(company, branches, documentMonth, clbReport.SelectedItem.ToString());
                            CommonData.ViewReport = "SSCRM_REP_SALES_SR_MULTI_DOCMM_SUMMARY";
                            childReportViewer.Show();
                        }
                    }
                }
                else if (iForm == 1)
                {
                    CommonData.ViewReport = "BRANCH_WISE_BULLETINS_BY_ALL";
                    childReportViewer = new ReportViewer(company, branches, documentMonth, "ALL");
                    childReportViewer.Show();

                }
                else if (iForm == 2)
                {
                    if (cbReport.SelectedValue.ToString()== "EMPLOYEE_PERFORMANCE_CUMULATIVE")
                    {
                        int iFrom = 0, iTo = 0;
                        string sType = "";
                        if (txtFrom.Text.ToString().Length != 0)
                            iFrom = Convert.ToInt32(txtFrom.Text);
                        if (txtTo.Text.ToString().Length != 0)
                            iTo = Convert.ToInt32(txtTo.Text);
                        if (iFrom == 0 && iTo != 0)
                            sType = "LESSTHAN";
                        else if (iFrom != 0 && iTo == 0)
                            sType = "GREATERTHEN";
                        else if (iFrom != 0 && iTo != 0)
                            sType = "BETWEEN";
                        CommonData.ViewReport = "EMPLOYEE_PERFORMANCE_CUMULATIVE";
                        childReportViewer = new ReportViewer(company, branches, documentMonth, iFrom, iTo, sType);
                        childReportViewer.Show();
                    }
                    if (cbReport.SelectedValue.ToString() == "TEAK_SALES_AGAINST_QTY")
                    {
                        int iDiff = 1, iMax = 100;
                        if (txtFrom.Text.ToString().Length != 0)
                            iDiff = Convert.ToInt32(txtFrom.Text);
                        if (txtTo.Text.ToString().Length != 0)
                            iMax = Convert.ToInt32(txtTo.Text);
                        CommonData.ViewReport = "TEAK_SALES_AGAINST_QTY";
                        childReportViewer = new ReportViewer(company, branches, documentMonth, iDiff, iMax, "");
                        childReportViewer.Show();
                    }
                    else
                    {
                        MessageBox.Show("No Report found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (iForm == 3)
                {
                    CommonData.ViewReport = "GROUP_AND_INDIVIDUAL_PERF_CROSSTAB";
                    childReportViewer = new ReportViewer(company, branches, documentMonth, "GRP_N_INDV_CRTAB");
                    childReportViewer.Show();

                }
                else if (iForm == 4)
                {
                    CommonData.ViewReport = "SSCRM_REP_AUDIT_QUERY_REGISTER";
                    childReportViewer = new ReportViewer(company, branches, finyear,documentMonth, "AUDIT_QUERY_REGISTER");
                    childReportViewer.Show();
                }
                else if (iForm == 5)
                {
                    CommonData.ViewReport = "SSERP_REP_COMP_LOCATION_WISE_GROUPS";
                    childReportViewer = new ReportViewer(company, branches, finyear, documentMonth, "");
                    childReportViewer.Show();
                }
                else if (iForm == 6)
                {
                    CommonData.ViewReport = "SSCRM_REP_EMP_BANK_ACCOUNT_DETAILS";
                    childReportViewer = new ReportViewer(company, branches, finyear, documentMonth, "DETAILED");
                    childReportViewer.Show();                    
                }
                else if (iForm == 7)
                {
                    CommonData.ViewReport = "SSCRM_REP_SERVICES_CHECKLIST";
                    childReportViewer = new ReportViewer(company, branches, finyear, documentMonth, "SUMMARY");
                    childReportViewer.Show();
                }
                else if (iForm == 8)
                {
                    if (cbReport.SelectedIndex == 0)
                    {
                        CommonData.ViewReport = "SSCRM_REP_TRAINING_PRG_DETAILS";
                        childReportViewer = new ReportViewer(company, branches, documentMonth, "DETAILED");
                        childReportViewer.Show();
                    }
                    else if (cbReport.SelectedIndex == 1)
                    {
                        CommonData.ViewReport = "SSCRM_REP_TRAINING_SUMM";
                        childReportViewer = new ReportViewer(company, branches, documentMonth, "SUMMARY");
                        childReportViewer.Show();
                    }
                }
                else if (iForm == 9)
                {
                    CommonData.ViewReport = "SSCRM_REP_STOCK_REC_SUMM";
                    childReportViewer = new ReportViewer(company, branches, finyear, documentMonth, "SUMM_ALL");
                    childReportViewer.Show();
                }
                else if (iForm == 10)
                {
                    if (cbReport.SelectedIndex == 0)
                    {
                        CommonData.ViewReport = "SERVICES_ACTIVITY_REPLACEMENT_SUMMARY";
                        childReportViewer = new ReportViewer(company, branches, documentMonth, Products, "BRANCH_WISE");
                        childReportViewer.Show();
                    }
                    else if (cbReport.SelectedIndex == 1)
                    {

                        CommonData.ViewReport = "SSERP_REP_SERVICES_MONTH_WISE_REPL_SUMMARY";
                        childReportViewer = new ReportViewer(company, branches, documentMonth, Products, "MONTH_WISE_REPLACEMENT");
                        childReportViewer.Show();
                    }
                }
                else if (iForm == 11)
                {
                    CommonData.ViewReport = "BRANCH_WISE_AFC_BY_ALL";
                    childReportViewer = new ReportViewer(company, branches, documentMonth, "ALL");
                    childReportViewer.Show();

                }

                else if(iForm==0)
                {
                    MessageBox.Show("No Report found", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        public void GetSelectedControlsIDs()
        {
            company = ""; branches = ""; documentMonth = ""; finyear = "";
            bool iscomp = false;
            for (int i = 0; i < tvBranches.Nodes.Count; i++)
            {
                for (int j = 0; j < tvBranches.Nodes[i].Nodes.Count; j++)
                {
                    if (tvBranches.Nodes[i].Nodes[j].Checked == true)
                    {
                        if (branches != string.Empty)
                            branches += ",";
                        branches += tvBranches.Nodes[i].Nodes[j].Name.ToString();
                        iscomp = true;
                    }
                }
                if (iscomp == true)
                {
                    if (company != string.Empty)
                        company += ",";
                    company += tvBranches.Nodes[i].Name.ToString();
                }
                iscomp = false;
            }
            for (int i = 0; i < tvDocumentMonth.Nodes.Count; i++)
            {
                for (int j = 0; j < tvDocumentMonth.Nodes[i].Nodes.Count; j++)
                {
                    if (tvDocumentMonth.Nodes[i].Nodes[j].Checked == true)
                    {
                        if (documentMonth != string.Empty)
                            documentMonth += ",";
                        documentMonth += tvDocumentMonth.Nodes[i].Nodes[j].Name.ToString();
                        finyear = tvDocumentMonth.Nodes[i].Name.ToString();
                    }
                }
            }
        }
        private bool checkdata()
        {
            bool blVil = false;
            for (int i = 0; i < clbReport.Items.Count; i++)
            {
                if (clbReport.GetItemCheckState(i) == CheckState.Checked)
                    blVil = true;
            }
            if (iForm == 10)
            {
                if (blVil == false)
                {
                    MessageBox.Show("Please Select Atleast One Product", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return blVil;
                }
                else
                    blVil = false;

            }
            else
            {
                if (blVil == false)
                {
                    MessageBox.Show("Select Report", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return blVil;
                }
                else
                    blVil = false;
            }
            for (int k = 0; k < tvBranches.Nodes.Count; k++)
            {

                for (int j = 0; j < tvBranches.Nodes[k].Nodes.Count; j++)
                {
                    if (tvBranches.Nodes[k].Nodes[j].Checked == true)
                    {
                        blVil = true;
                    }
                }

            }
            if (blVil == false)
            {
                MessageBox.Show("Select Branches For User", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return blVil;
            }
            blVil = false;
            for (int k = 0; k < tvDocumentMonth.Nodes.Count; k++)
            {

                for (int j = 0; j < tvDocumentMonth.Nodes[k].Nodes.Count; j++)
                {
                    if (tvDocumentMonth.Nodes[k].Nodes[j].Checked == true)
                    {
                        blVil = true;
                    }
                }

            }
            if (blVil == false)
            {
                MessageBox.Show("Select Document Month For User", "SSCRM-Report", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return blVil;
            }
            return blVil;
        }

        private void txtFrom_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtTo_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')
            {
                if (!char.IsDigit((e.KeyChar)))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            GetSelectedControlsIDs();
            GetSelectedProductIds();
            DataTable dtExcel = new DataTable();

            if (checkdata() == true)
            {
                #region iForm 0
                if (iForm == 0)
                {
                    if (cbReport.SelectedValue.ToString() == "RAWDATA")
                    {
                        objExcelDB = new ExcelDB();
                        dtExcel = objExcelDB.GetMonthWisePerformance(company, branches, documentMonth, finyear).Tables[0];
                        objExcelDB = null;
                        if (dtExcel.Rows.Count > 0)
                        {
                            try
                            {
                                Excel.Application oXL = new Excel.Application();
                                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                                oXL.Visible = true;

                                Excel.Range rg = worksheet.get_Range("A1", "AT1");
                                Excel.Range rgData = worksheet.get_Range("A2", "AT" + (dtExcel.Rows.Count + 1).ToString());
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
                                rg.Cells.ColumnWidth = 25;
                                rg.Cells.Value2 = "E/A Code & S/R Name";

                                rg = worksheet.get_Range("C1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "DOJ";

                                rg = worksheet.get_Range("D1", Type.Missing);
                                rg.Cells.ColumnWidth = 35;
                                rg.Cells.Value2 = "Source of Recuriting";

                                rg = worksheet.get_Range("E1", Type.Missing);
                                rg.Cells.ColumnWidth = 25;
                                rg.Cells.Value2 = "Recuriting Ecode";

                                rg = worksheet.get_Range("F1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Status";

                                rg = worksheet.get_Range("G1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "E-Code";

                                rg = worksheet.get_Range("H1", Type.Missing);
                                rg.Cells.ColumnWidth = 25;
                                rg.Cells.Value2 = "Trainer Name";

                                rg = worksheet.get_Range("I1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "From Date";

                                rg = worksheet.get_Range("J1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "To Date";

                                rg = worksheet.get_Range("K1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Y/N";

                                rg = worksheet.get_Range("L1", Type.Missing);
                                rg.Cells.ColumnWidth = 20;
                                rg.Cells.Value2 = "Vehicle Number";

                                rg = worksheet.get_Range("M1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Own/Camp";

                                rg = worksheet.get_Range("N1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Doc.Month";

                                rg = worksheet.get_Range("O1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "PMD";

                                rg = worksheet.get_Range("P1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "D.A.Days";

                                rg = worksheet.get_Range("Q1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Demos";

                                rg = worksheet.get_Range("R1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Cust";

                                rg = worksheet.get_Range("S1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Qty";

                                rg = worksheet.get_Range("T1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Revenue";

                                rg = worksheet.get_Range("U1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Prod Points";

                                rg = worksheet.get_Range("V1", Type.Missing);
                                rg.Cells.ColumnWidth = 20;
                                rg.Cells.Value2 = "Camp";

                                rg = worksheet.get_Range("W1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Grp e/a code";

                                rg = worksheet.get_Range("X1", Type.Missing);
                                rg.Cells.ColumnWidth = 20;
                                rg.Cells.Value2 = "Grp Lead Name";

                                rg = worksheet.get_Range("Y1", Type.Missing);
                                rg.Cells.ColumnWidth = 30;
                                rg.Cells.Value2 = "Branch Name";

                                rg = worksheet.get_Range("Z1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Status";

                                rg = worksheet.get_Range("AA1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Lvl-01";

                                rg = worksheet.get_Range("AB1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Lvl_02";

                                rg = worksheet.get_Range("AC1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "lvl-03";

                                rg = worksheet.get_Range("AD1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Lvl-04";

                                rg = worksheet.get_Range("AE1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Lvl-05";

                                rg = worksheet.get_Range("AF1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Lvl-06";

                                rg = worksheet.get_Range("AG1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Lvl-07";

                                rg = worksheet.get_Range("AH1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Lvl-08";

                                rg = worksheet.get_Range("AI1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Lvl-09";

                                rg = worksheet.get_Range("AJ1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Lvl-10";

                                rg = worksheet.get_Range("AK1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Lvl-11";

                                rg = worksheet.get_Range("AL1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Lvl_12";

                                rg = worksheet.get_Range("AM1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "lvl-13";

                                rg = worksheet.get_Range("AN1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Lvl-14";

                                rg = worksheet.get_Range("AO1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Lvl-15";

                                rg = worksheet.get_Range("AP1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Lvl-16";

                                rg = worksheet.get_Range("AQ1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Lvl-17";

                                rg = worksheet.get_Range("AR1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Lvl-18";

                                rg = worksheet.get_Range("AS1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Lvl-19";

                                rg = worksheet.get_Range("AT1", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg.Cells.Value2 = "Lvl-20";



                                int RowCounter = 1;
                                foreach (DataRow dr in dtExcel.Rows)
                                {
                                    int i = 1;
                                    worksheet.Cells[RowCounter + 1, i++] = RowCounter;
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_eora_code"].ToString() + " - " + dr["sr_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_doj"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_recr_source_name"].ToString() + "-" + dr["sr_recr_source_detl_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_recr_source_ecode"].ToString() + "-" + dr["sr_recr_source_ename"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_indu_training_status"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_indu_trainer_ecode"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_indu_trainer_ename"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_indu_training_from"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_indu_training_to"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_vehicle_YorN"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_vehicle_No"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_vehicle_LorW"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_document_month"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_pmd"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_da_days"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_demos"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_invoices"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_qty"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_amt"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_prod_points"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_grp_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_grp_eora_code"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_grp_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_branch_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_working_status"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl01_eora_code"].ToString() + "-" + dr["sr_lvl01_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl02_eora_code"].ToString() + "-" + dr["sr_lvl02_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl03_eora_code"].ToString() + "-" + dr["sr_lvl03_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl04_eora_code"].ToString() + "-" + dr["sr_lvl04_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl05_eora_code"].ToString() + "-" + dr["sr_lvl05_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl06_eora_code"].ToString() + "-" + dr["sr_lvl06_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl07_eora_code"].ToString() + "-" + dr["sr_lvl07_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl08_eora_code"].ToString() + "-" + dr["sr_lvl08_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl09_eora_code"].ToString() + "-" + dr["sr_lvl09_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl10_eora_code"].ToString() + "-" + dr["sr_lvl10_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl11_eora_code"].ToString() + "-" + dr["sr_lvl11_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl12_eora_code"].ToString() + "-" + dr["sr_lvl12_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl13_eora_code"].ToString() + "-" + dr["sr_lvl13_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl14_eora_code"].ToString() + "-" + dr["sr_lvl14_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl15_eora_code"].ToString() + "-" + dr["sr_lvl15_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl16_eora_code"].ToString() + "-" + dr["sr_lvl16_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl17_eora_code"].ToString() + "-" + dr["sr_lvl17_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl18_eora_code"].ToString() + "-" + dr["sr_lvl18_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl19_eora_code"].ToString() + "-" + dr["sr_lvl19_eora_name"].ToString();
                                    worksheet.Cells[RowCounter + 1, i++] = dr["sr_lvl20_eora_code"].ToString() + "-" + dr["sr_lvl20_eora_name"].ToString();




                                    RowCounter++;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                #endregion

                #region iForm 10 :: Replacement Details
                if (iForm == 10)
                {
                    if (cbReport.SelectedIndex == 0)
                    {
                        try
                        {

                            objExcelDB = new ExcelDB();
                            dtExcel = objExcelDB.Get_ServiceReplacementDetails(company, branches, documentMonth, Products, "BRANCH_WISE").Tables[0];
                            objExcelDB = null;

                            if (dtExcel.Rows.Count > 0)
                            {
                                Excel.Application oXL = new Excel.Application();
                                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                                oXL.Visible = true;
                                int iTotColumns = 0;
                                iTotColumns = 19;
                                string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                                Excel.Range rgHead = null;
                                Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                                Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 4).ToString());
                                rgData.Font.Size = 11;
                                rgData.WrapText = true;
                                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                rgData.Borders.Weight = 2;

                                rgData = worksheet.get_Range("A1", "S2");
                                rgData.Merge(Type.Missing);
                                rgData.Font.Bold = true; rgData.Font.Size = 16;
                                rgData.Value2 = "SERVICE REPLACEMENT DETAILS";
                                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                rgData.HorizontalAlignment = Excel.Constants.xlCenter;


                                rg.Font.Bold = true;
                                rg.Font.Name = "Times New Roman";
                                rg.Font.Size = 10;
                                rg.WrapText = true;
                                rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.Interior.ColorIndex = 31;
                                rg.Borders.Weight = 2;
                                rg.Borders.LineStyle = Excel.Constants.xlSolid;
                                rg.Cells.RowHeight = 38;

                                rg = worksheet.get_Range("A4", Type.Missing);
                                rg.Cells.ColumnWidth = 4;
                                rg = worksheet.get_Range("B4", Type.Missing);
                                rg.Cells.ColumnWidth = 35;
                                rg = worksheet.get_Range("C4", Type.Missing);
                                rg.Cells.ColumnWidth = 40;
                                rg = worksheet.get_Range("D4", Type.Missing);
                                rg.Cells.ColumnWidth = 8;
                                rg = worksheet.get_Range("E4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("F4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("G3", "I3");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "REPLACEMENT-1";
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg = worksheet.get_Range("G4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("H4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("I4", Type.Missing);
                                rg.Cells.ColumnWidth = 15;

                                rg = worksheet.get_Range("J3", "L3");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "REPLACEMENT-2";
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg = worksheet.get_Range("J4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("K4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("L4", Type.Missing);
                                rg.Cells.ColumnWidth = 15;

                                rg = worksheet.get_Range("M3", "N3");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "TOTAL REPLACEMENT";
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg = worksheet.get_Range("M4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("N4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("O4", Type.Missing);
                                rg.Cells.ColumnWidth = 15;

                                rg = worksheet.get_Range("P3", "Q3");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "REPL GIVEN CUSTOMERS";
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg = worksheet.get_Range("P4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("Q4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("R4", Type.Missing);
                                rg.Cells.ColumnWidth = 15;
                                rg = worksheet.get_Range("S4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;


                                int iColumn = 1, iStartRow = 4;
                                worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                                worksheet.Cells[iStartRow, iColumn++] = "Company";
                                worksheet.Cells[iStartRow, iColumn++] = "Branch";
                                worksheet.Cells[iStartRow, iColumn++] = "Doc Month";
                                worksheet.Cells[iStartRow, iColumn++] = "Total Sales";
                                worksheet.Cells[iStartRow, iColumn++] = "Repl Sales Against Tot Sales";
                                worksheet.Cells[iStartRow, iColumn++] = "Total Units";
                                worksheet.Cells[iStartRow, iColumn++] = "% Of Repl";
                                worksheet.Cells[iStartRow, iColumn++] = "% Of Repl Against Sales-1";
                                worksheet.Cells[iStartRow, iColumn++] = "Total Units";
                                worksheet.Cells[iStartRow, iColumn++] = "% Of Repl";
                                worksheet.Cells[iStartRow, iColumn++] = "% Of Repl Against Sales-2";
                                worksheet.Cells[iStartRow, iColumn++] = "Total Units";
                                worksheet.Cells[iStartRow, iColumn++] = "% Of Repl";
                                worksheet.Cells[iStartRow, iColumn++] = "Total Customers";
                                worksheet.Cells[iStartRow, iColumn++] = "1st Time";
                                worksheet.Cells[iStartRow, iColumn++] = "2nd Time";
                                worksheet.Cells[iStartRow, iColumn++] = "% Of Customers Got Repl";
                                worksheet.Cells[iStartRow, iColumn++] = "Repl Pending Customers";

                                iStartRow++; iColumn = 1;

                                for (int i = 0; i < dtExcel.Rows.Count; i++)
                                {

                                    worksheet.Cells[iStartRow, iColumn++] = i + 1;
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_company_name"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_branch_name"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_doc_month"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_Tot_Units"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_Repl_against_sales"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_Tot_Repl1_units"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_pers_of_Repl1"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_pers_of_repl1_agnst_sales"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_Tot_Repl2_units"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_pers_of_Repl2"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_pers_of_repl2_agnst_sales"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_tot_replacement"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_pers_of_repl"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_No_Of_cust"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_No_Of_Repl1_cust"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_No_Of_Repl2_cust"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_pers_of_repl_cust"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_pending_repl_cust"];

                                    iStartRow++; iColumn = 1;
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (cbReport.SelectedIndex == 1)
                    {
                        try
                        {
                            objExcelDB = new ExcelDB();
                            dtExcel = objExcelDB.Get_ServiceReplacementDetails(company, branches, documentMonth, Products, "MONTH_WISE_REPLACEMENT").Tables[0];
                            objExcelDB = null;

                            if (dtExcel.Rows.Count > 0)
                            {
                                Excel.Application oXL = new Excel.Application();
                                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                                oXL.Visible = true;
                                int iTotColumns = 0;
                                iTotColumns = 13;
                                string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                                Excel.Range rgHead = null;
                                Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                                Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 4).ToString());
                                rgData.Font.Size = 11;
                                rgData.WrapText = true;
                                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                rgData.Borders.Weight = 2;

                                rgData = worksheet.get_Range("A1", "M2");
                                rgData.Merge(Type.Missing);
                                rgData.Font.Bold = true; rgData.Font.Size = 16;
                                rgData.Value2 = "CURRENT MONTH REPLACEMENT DETAILS";
                                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                rgData.HorizontalAlignment = Excel.Constants.xlCenter;


                                rg.Font.Bold = true;
                                rg.Font.Name = "Times New Roman";
                                rg.Font.Size = 10;
                                rg.WrapText = true;
                                rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.Interior.ColorIndex = 31;
                                rg.Borders.Weight = 2;
                                rg.Borders.LineStyle = Excel.Constants.xlSolid;
                                rg.Cells.RowHeight = 38;

                                rg = worksheet.get_Range("A4", Type.Missing);
                                rg.Cells.ColumnWidth = 4;
                                rg = worksheet.get_Range("B4", Type.Missing);
                                rg.Cells.ColumnWidth = 35;
                                rg = worksheet.get_Range("C4", Type.Missing);
                                rg.Cells.ColumnWidth = 40;
                                rg = worksheet.get_Range("D4", Type.Missing);
                                rg.Cells.ColumnWidth = 8;
                                rg = worksheet.get_Range("E4", Type.Missing);
                                rg.Cells.ColumnWidth = 12;

                                rg = worksheet.get_Range("F3", "G3");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "REPLACEMENT-1";
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg = worksheet.get_Range("F4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("G4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("H3", "I3");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "REPLACEMENT-2";
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg = worksheet.get_Range("H4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("I4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("J3", "K3");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "TOTAL REPLACEMENT";
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg = worksheet.get_Range("J4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("K4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range("L3", "M3");
                                rg.Merge(Type.Missing);
                                rg.Value2 = "REPL GIVEN CUSTOMERS";
                                rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                                rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                                rg.VerticalAlignment = Excel.Constants.xlCenter;
                                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                                rg = worksheet.get_Range("L4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("M4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;


                                int iColumn = 1, iStartRow = 4;
                                worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                                worksheet.Cells[iStartRow, iColumn++] = "Company";
                                worksheet.Cells[iStartRow, iColumn++] = "Branch";
                                worksheet.Cells[iStartRow, iColumn++] = "Doc Month";
                                worksheet.Cells[iStartRow, iColumn++] = "Sales Against Replacement";
                                worksheet.Cells[iStartRow, iColumn++] = "Total Units";
                                worksheet.Cells[iStartRow, iColumn++] = "% Of Repl";
                                worksheet.Cells[iStartRow, iColumn++] = "Total Units";
                                worksheet.Cells[iStartRow, iColumn++] = "% Of Repl";
                                worksheet.Cells[iStartRow, iColumn++] = "Total Units";
                                worksheet.Cells[iStartRow, iColumn++] = "% Of Repl";
                                worksheet.Cells[iStartRow, iColumn++] = "1st Time";
                                worksheet.Cells[iStartRow, iColumn++] = "2nd Time";

                                iStartRow++; iColumn = 1;

                                for (int i = 0; i < dtExcel.Rows.Count; i++)
                                {

                                    worksheet.Cells[iStartRow, iColumn++] = i + 1;
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_company_name"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_branch_name"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_doc_month"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_Repl_against_sales"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_Tot_Repl1_units"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_pers_of_Repl1"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_Tot_Repl2_units"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_pers_of_Repl2"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_tot_replacement"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_pers_of_repl"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_No_Of_Repl1_cust"];
                                    worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["chr_No_Of_Repl2_cust"];

                                    iStartRow++; iColumn = 1;
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                #endregion 


                if (iForm == 2)
                {

                    #region iForm 2 :: EMPLOYEE_PERFORMANCE_CUMULATIVE
                    if (cbReport.SelectedValue.ToString() == "EMPLOYEE_PERFORMANCE_CUMULATIVE")
                    {
                        try
                        {
                            objExcelDB = new ExcelDB();
                            objUtilityDB = new UtilityDB();
                            dtExcel = objExcelDB.GetSRWiseCumulativeBulletins(company, branches, documentMonth, finyear, "", "").Tables[0];
                            objExcelDB = null;

                            if (dtExcel.Rows.Count > 0)
                            {
                                Excel.Application oXL = new Excel.Application();
                                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                                oXL.Visible = true;
                                int iTotColumns = 0;
                                iTotColumns = 20;
                                string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                                Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                                Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString());
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
                                rgData = worksheet.get_Range("A4", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4).ToString());
                                rgData.WrapText = false;
                                rg = worksheet.get_Range("A3", Type.Missing);
                                rg.Cells.ColumnWidth = 4;
                                rg = worksheet.get_Range("B3", Type.Missing);
                                rg.Cells.ColumnWidth = 8;
                                rg = worksheet.get_Range("C3", Type.Missing);
                                rg.Cells.ColumnWidth = 8;

                                rg = worksheet.get_Range("D3", Type.Missing);
                                rg.Cells.ColumnWidth = 30;
                                rg = worksheet.get_Range("E3", "F3");
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("F3", Type.Missing);
                                rg.Cells.ColumnWidth = 40;
                                rg = worksheet.get_Range("G3", Type.Missing);
                                rg.Cells.ColumnWidth = 40;
                                rg = worksheet.get_Range("H3", Type.Missing);
                                rg.Cells.HorizontalAlignment = HorizontalAlignment.Center;
                                rg = worksheet.get_Range("J3", Type.Missing);
                                rg.Cells.ColumnWidth = 30;

                                rg = worksheet.get_Range(objUtilityDB.GetColumnName(iTotColumns - 5) + "3", objUtilityDB.GetColumnName(iTotColumns - 5) + "3");
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range(objUtilityDB.GetColumnName(iTotColumns - 2) + "3", objUtilityDB.GetColumnName(iTotColumns - 2) + "3");
                                rg.Cells.ColumnWidth = 10;


                                int iColumn = 1;
                                worksheet.Cells[3, iColumn++] = "SlNo";
                                worksheet.Cells[3, iColumn++] = "Acode";
                                worksheet.Cells[3, iColumn++] = "Ecode";
                                worksheet.Cells[3, iColumn++] = "Name";
                                worksheet.Cells[3, iColumn++] = "Doj";
                                worksheet.Cells[3, iColumn++] = "Company";
                                worksheet.Cells[3, iColumn++] = "Branch";
                                worksheet.Cells[3, iColumn++] = "Groups";

                                Excel.Range rgHead = null;

                                //worksheet.Cells[3, iColumn++] = "Source";
                                //worksheet.Cells[3, iColumn++] = "Rec Team";
                                //worksheet.Cells[3, iColumn++] = "Trained By";
                                worksheet.Cells[3, iColumn++] = "Region";
                                rgHead = worksheet.get_Range("L3", "R3");
                                rgHead.ColumnWidth = 30;
                                worksheet.Cells[3, iColumn++] = "GC/GL";
                                worksheet.Cells[3, iColumn++] = "Level1";
                                worksheet.Cells[3, iColumn++] = "Level2";
                                worksheet.Cells[3, iColumn++] = "Level3";
                                worksheet.Cells[3, iColumn++] = "Level4";
                                worksheet.Cells[3, iColumn++] = "Level5";
                                worksheet.Cells[3, iColumn++] = "Level6";

                                worksheet.Cells[3, iColumn++] = "Level7";
                                worksheet.Cells[3, iColumn++] = "Level8";
                                worksheet.Cells[3, iColumn++] = "Level9";
                                worksheet.Cells[3, iColumn++] = "Level10";

                                rgHead = worksheet.get_Range("A1", "G2");
                                rgHead.Merge(Type.Missing);
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "CUMULATIVE BULLETINS";





                                int iRowCounter = 4; int iColumnCounter = 1;
                                for (int i = 0; i < dtExcel.Rows.Count; i++)
                                {
                                    if (i > 0)
                                    {
                                        if (dtExcel.Rows[i]["sr_eora_code"].ToString() != dtExcel.Rows[i - 1]["sr_eora_code"].ToString())
                                        {
                                            rgHead = worksheet.get_Range("J1", "T2");
                                            rgHead.ColumnWidth = 40;
                                            rgHead.Merge(Type.Missing);
                                            //rgHead.Font.Size = 14;
                                            rgHead.Font.ColorIndex = 1;
                                            rgHead.Font.Bold = true;
                                            rgHead.Borders.Weight = 2;
                                            rgHead.Interior.ColorIndex = 31;
                                            rgHead.Font.ColorIndex = 2;
                                            rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                            rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                            rgHead.Cells.Value2 = "REPORTING STRUCTURE OF " + dtExcel.Rows[i]["sr_doc_month"].ToString().ToUpper();

                                            iRowCounter++;
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_agent_code"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_eora_code"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_eora_name"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToDateTime(dtExcel.Rows[i]["sr_doj"]).ToString("dd-MMM-yyyy");
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_comp_name"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_branch_name"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_groups"];
                                            //worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rec_source"];
                                            //worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rec_dept"];
                                            //worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_trained_by"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_region"];

                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_gc_code"] + "-" + dtExcel.Rows[i]["sr_rep_gc_gl"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level1_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level1"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level2_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level2"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level3_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level3"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level4_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level4"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level5_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level5"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level6_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level6"];


                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level7_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level7"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level8_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level8"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level9_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level9"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level10_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level10"];

                                        }
                                    }
                                    else
                                    {
                                        rgHead = worksheet.get_Range("L1", "R2");
                                        rgHead.Merge(Type.Missing);
                                        //rgHead.Font.Size = 14;
                                        rgHead.Font.ColorIndex = 1;
                                        rgHead.Font.Bold = true;
                                        rgHead.Borders.Weight = 2;
                                        rgHead.Interior.ColorIndex = 31;
                                        rgHead.Font.ColorIndex = 2;
                                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                        rgHead.Cells.Value2 = "REPORTING STRUCTURE OF " + dtExcel.Rows[i]["sr_doc_month"].ToString().ToUpper();


                                        worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_agent_code"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_eora_code"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_eora_name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToDateTime(dtExcel.Rows[i]["sr_doj"]).ToString("dd-MMM-yyyy");
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_comp_name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_branch_name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_groups"];
                                        //worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rec_source"];
                                        //worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rec_dept"];
                                        //worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_trained_by"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_region"];

                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_gc_code"] + "-" + dtExcel.Rows[i]["sr_rep_gc_gl"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level1_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level1"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level2_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level2"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level3_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level3"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level4_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level4"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level5_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level5"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level6_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level6"];


                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level7_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level7"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level8_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level8"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level9_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level9"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level10_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level10"];


                                    }

                                    iColumnCounter = 1;
                                }

                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    #endregion

                    #region iForm 2 :: CUMULATIVE_EXCEL
                    if (cbReport.SelectedValue.ToString() == "EMPLOYEE_PERFORMANCE_CUMULATIVE_EXCEL")
                    {
                        try
                        {
                            objExcelDB = new ExcelDB();
                            objUtilityDB = new UtilityDB();
                            dtExcel = objExcelDB.GetSRWiseCumulativeBulletins(company, branches, documentMonth, finyear, "", "").Tables[0];
                            objExcelDB = null;

                            if (dtExcel.Rows.Count > 0)
                            {
                                Excel.Application oXL = new Excel.Application();
                                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                                oXL.Visible = true;
                                int iTotColumns = 0;
                                iTotColumns = 18 + (17 * Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"])) + 19 + 6;
                                string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                                Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                                Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString());
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
                                rgData = worksheet.get_Range("A4", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4).ToString());
                                rgData.WrapText = false;
                                rg = worksheet.get_Range("A3", Type.Missing);
                                rg.Cells.ColumnWidth = 4;
                                rg = worksheet.get_Range("B3", Type.Missing);
                                rg.Cells.ColumnWidth = 8;
                                rg = worksheet.get_Range("C3", Type.Missing);
                                rg.Cells.ColumnWidth = 30;

                                rg = worksheet.get_Range("D3", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("E3", "F3");
                                rg.Cells.ColumnWidth = 40;
                                rg = worksheet.get_Range("F3", Type.Missing);
                                rg.Cells.ColumnWidth = 40;
                                rg = worksheet.get_Range("H3", "J3");
                                rg.Cells.ColumnWidth = 25;
                                rg = worksheet.get_Range("I3", Type.Missing);
                                rg.Cells.ColumnWidth = 10;

                                rg = worksheet.get_Range(objUtilityDB.GetColumnName(iTotColumns - 5) + "3", objUtilityDB.GetColumnName(iTotColumns - 5) + "3");
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range(objUtilityDB.GetColumnName(iTotColumns - 2) + "3", objUtilityDB.GetColumnName(iTotColumns - 2) + "3");
                                rg.Cells.ColumnWidth = 10;


                                int iColumn = 1;
                                worksheet.Cells[3, iColumn++] = "SlNo";
                                worksheet.Cells[3, iColumn++] = "Ecode";
                                worksheet.Cells[3, iColumn++] = "Name";
                                worksheet.Cells[3, iColumn++] = "Doj";
                                worksheet.Cells[3, iColumn++] = "Company";
                                worksheet.Cells[3, iColumn++] = "Branch";
                                worksheet.Cells[3, iColumn++] = "Length of Service";

                                Excel.Range rgHead = worksheet.get_Range("H1", "J2");
                                rgHead.Merge(Type.Missing);
                                //rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Borders.Weight = 2;
                                rgHead.Interior.ColorIndex = 31;
                                rgHead.Font.ColorIndex = 2;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "SOURCE OF RECRUITEMENT";

                                worksheet.Cells[3, iColumn++] = "Source";
                                worksheet.Cells[3, iColumn++] = "Rec Team";
                                worksheet.Cells[3, iColumn++] = "Trained By";
                                worksheet.Cells[3, iColumn++] = "Region";
                                rgHead = worksheet.get_Range("L3", "R3");
                                rgHead.ColumnWidth = 30;
                                worksheet.Cells[3, iColumn++] = "GC/GL";
                                worksheet.Cells[3, iColumn++] = "Level1";
                                worksheet.Cells[3, iColumn++] = "Level2";
                                worksheet.Cells[3, iColumn++] = "Level3";
                                worksheet.Cells[3, iColumn++] = "Level4";
                                worksheet.Cells[3, iColumn++] = "Level5";
                                worksheet.Cells[3, iColumn++] = "Level6";

                                int iStartColumn = 0;
                                for (int iMonths = 0; iMonths < Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"]); iMonths++)
                                {
                                    rgHead = worksheet.get_Range("A1", "G2");
                                    rgHead.Merge(Type.Missing);
                                    rgHead.Font.Size = 14;
                                    rgHead.Font.ColorIndex = 1;
                                    rgHead.Font.Bold = true;
                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.Value2 = "CUMULATIVE BULLETINS";


                                    iStartColumn = (17 * iMonths) + 19;

                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "1", objUtilityDB.GetColumnName(iStartColumn + 16) + "1");
                                    //rgHead.Cells.ColumnWidth = 5;
                                    rgHead.Merge(Type.Missing);
                                    rgHead.Interior.ColorIndex = 34 + iMonths;
                                    rgHead.Borders.Weight = 2;
                                    rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                                    rgHead.Cells.RowHeight = 20;
                                    rgHead.Font.Size = 14;
                                    rgHead.Font.ColorIndex = 1;
                                    rgHead.Font.Bold = true;
                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 7) + "2");
                                    rgHead.Interior.ColorIndex = 34 + iMonths;
                                    rgHead.Font.ColorIndex = 1;
                                    rgHead.Cells.ColumnWidth = 5;
                                    rgHead.Merge(Type.Missing);
                                    rgHead.Borders.Weight = 2;
                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Font.Bold = true;
                                    rgHead.Value2 = "PERSONAL";
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn + 8) + "2", objUtilityDB.GetColumnName(iStartColumn + 16) + "2");
                                    rgHead.Interior.ColorIndex = 34 + iMonths;
                                    rgHead.Font.ColorIndex = 1;
                                    rgHead.Cells.ColumnWidth = 5;
                                    rgHead.Merge(Type.Missing);
                                    rgHead.Borders.Weight = 2;
                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Font.Bold = true;
                                    rgHead.Value2 = "GROUP";
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "3", objUtilityDB.GetColumnName(iStartColumn + 16) + "3");
                                    rgHead.Interior.ColorIndex = 34 + iMonths;
                                    rgHead.Font.ColorIndex = 1;
                                    rgHead.Cells.ColumnWidth = 5;
                                    //worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 6) + "2").Merge(Type.Missing);
                                    //rg.Application.ActiveWindow.FreezePanes = true;
                                    //rgHead.Height = 10;
                                    //rgHead.Cells.Value2 = CommonData.BranchName + "";

                                    worksheet.Cells[3, iStartColumn++] = "PMD";
                                    worksheet.Cells[3, iStartColumn++] = "Plants";
                                    worksheet.Cells[3, iStartColumn++] = "Growmin";
                                    worksheet.Cells[3, iStartColumn++] = "Others";
                                    worksheet.Cells[3, iStartColumn++] = "Total";
                                    worksheet.Cells[3, iStartColumn++] = "Points";
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "3", objUtilityDB.GetColumnName(iStartColumn) + "3");
                                    rgHead.Cells.ColumnWidth = 8;
                                    worksheet.Cells[3, iStartColumn++] = "Revenue";
                                    worksheet.Cells[3, iStartColumn++] = "Cust";
                                    worksheet.Cells[3, iStartColumn++] = "Groups";
                                    worksheet.Cells[3, iStartColumn++] = "PMD";
                                    worksheet.Cells[3, iStartColumn++] = "Plants";
                                    worksheet.Cells[3, iStartColumn++] = "Growmin";
                                    worksheet.Cells[3, iStartColumn++] = "Others";
                                    worksheet.Cells[3, iStartColumn++] = "Total";
                                    worksheet.Cells[3, iStartColumn++] = "Points";
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "3", objUtilityDB.GetColumnName(iStartColumn) + "3");
                                    rgHead.Cells.ColumnWidth = 8;
                                    worksheet.Cells[3, iStartColumn++] = "Revenue";
                                    worksheet.Cells[3, iStartColumn++] = "Cust";

                                }
                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "1", objUtilityDB.GetColumnName(iStartColumn + 18) + "1");
                                rgHead.Merge(Type.Missing);
                                rgHead.Interior.ColorIndex = 34 + Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"]) + 1;
                                rgHead.Borders.Weight = 2;
                                rgHead.Borders.LineStyle = Excel.Constants.xlSolid;
                                rgHead.Cells.RowHeight = 20;
                                rgHead.Font.Size = 14;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Font.Bold = true;
                                rgHead.Value2 = "GRAND TOTAL";
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 8) + "2");
                                rgHead.Merge(Type.Missing);
                                rgHead.Interior.ColorIndex = 34 + Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"]) + 1;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Cells.ColumnWidth = 5;
                                rgHead.Merge(Type.Missing);
                                rgHead.Borders.Weight = 2;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Font.Bold = true;
                                rgHead.Value2 = "PERSONAL";
                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn + 9) + "2", objUtilityDB.GetColumnName(iStartColumn + 18) + "2");
                                rgHead.Interior.ColorIndex = 34 + Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"]) + 1;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Cells.ColumnWidth = 5;
                                rgHead.Merge(Type.Missing);
                                rgHead.Borders.Weight = 2;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Font.Bold = true;
                                rgHead.Value2 = "GROUP";
                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "3", objUtilityDB.GetColumnName(iStartColumn + 18) + "3");
                                rgHead.Interior.ColorIndex = 34 + Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"]) + 1;
                                rgHead.Font.ColorIndex = 1;
                                rgHead.Cells.ColumnWidth = 5;
                                worksheet.Cells[3, iStartColumn++] = "No of Months";
                                worksheet.Cells[3, iStartColumn++] = "PMD";
                                worksheet.Cells[3, iStartColumn++] = "Plants";
                                worksheet.Cells[3, iStartColumn++] = "Growmin";
                                worksheet.Cells[3, iStartColumn++] = "Others";
                                worksheet.Cells[3, iStartColumn++] = "Total";
                                worksheet.Cells[3, iStartColumn++] = "Points";
                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "3", objUtilityDB.GetColumnName(iStartColumn) + "3");
                                rgHead.Cells.ColumnWidth = 8;
                                worksheet.Cells[3, iStartColumn++] = "Revenue";
                                worksheet.Cells[3, iStartColumn++] = "Cust";
                                worksheet.Cells[3, iStartColumn++] = "No of Months";
                                worksheet.Cells[3, iStartColumn++] = "Groups";
                                worksheet.Cells[3, iStartColumn++] = "PMD";
                                worksheet.Cells[3, iStartColumn++] = "Plants";
                                worksheet.Cells[3, iStartColumn++] = "Growmin";
                                worksheet.Cells[3, iStartColumn++] = "Others";
                                worksheet.Cells[3, iStartColumn++] = "Total";
                                worksheet.Cells[3, iStartColumn++] = "Points";
                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "3", objUtilityDB.GetColumnName(iStartColumn) + "3");
                                rgHead.Cells.ColumnWidth = 8;
                                worksheet.Cells[3, iStartColumn++] = "Revenue";
                                worksheet.Cells[3, iStartColumn++] = "Cust";

                                worksheet.Cells[3, iStartColumn++] = "Working Status";
                                worksheet.Cells[3, iStartColumn++] = "If Resigned (Month)";
                                worksheet.Cells[3, iStartColumn++] = "Vehicle Status";
                                worksheet.Cells[3, iStartColumn++] = "Vehicle EFF";
                                worksheet.Cells[3, iStartColumn++] = "Tab Issue Status";
                                worksheet.Cells[3, iStartColumn++] = "Tab Issue Month";



                                int iRowCounter = 4; int iColumnCounter = 1;
                                for (int i = 0; i < dtExcel.Rows.Count; i++)
                                {
                                    if (i > 0)
                                    {
                                        if (dtExcel.Rows[i]["sr_eora_code"].ToString() == dtExcel.Rows[i - 1]["sr_eora_code"].ToString())
                                        {
                                            int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sr_month_slno"]);
                                            //int iStartColumn = 0;
                                            iColumnCounter = (17 * (iMonthNo - 1)) + 19;
                                            rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "1", objUtilityDB.GetColumnName(iColumnCounter + 16) + "1");
                                            rgHead.Cells.Value2 = dtExcel.Rows[i]["sr_doc_month"];

                                            rgHead = worksheet.get_Range("L1", "R2");
                                            rgHead.Merge(Type.Missing);
                                            //rgHead.Font.Size = 14;
                                            rgHead.Font.ColorIndex = 1;
                                            rgHead.Font.Bold = true;
                                            rgHead.Borders.Weight = 2;
                                            rgHead.Interior.ColorIndex = 31;
                                            rgHead.Font.ColorIndex = 2;
                                            rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                            rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                            rgHead.Cells.Value2 = "REPORTING STRUCTURE OF " + dtExcel.Rows[i]["sr_doc_month"].ToString().ToUpper();


                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_pmd"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_plants"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_growmin"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_others"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_total_qty"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_points"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_revenue"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_cust"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_groups"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_pmd"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_plants"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_growmin"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_others"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_totqty"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_totpoints"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_revenue"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_cust"];
                                        }
                                        else
                                        {
                                            rgHead = worksheet.get_Range("L1", "R2");
                                            rgHead.Merge(Type.Missing);
                                            //rgHead.Font.Size = 14;
                                            rgHead.Font.ColorIndex = 1;
                                            rgHead.Font.Bold = true;
                                            rgHead.Borders.Weight = 2;
                                            rgHead.Interior.ColorIndex = 31;
                                            rgHead.Font.ColorIndex = 2;
                                            rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                            rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                            rgHead.Cells.Value2 = "REPORTING STRUCTURE OF " + dtExcel.Rows[i]["sr_doc_month"].ToString().ToUpper();

                                            iRowCounter++;
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_eora_code"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_eora_name"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToDateTime(dtExcel.Rows[i]["sr_doj"]).ToString("dd-MMM-yyyy");
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_comp_name"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_branch_name"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_serv_length"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rec_source"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rec_dept"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_trained_by"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_region"];

                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_gc_code"] + "-" + dtExcel.Rows[i]["sr_rep_gc_gl"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level1_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level1"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level2_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level2"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level3_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level3"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level4_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level4"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level5_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level5"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level6_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level6"];

                                            int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sr_month_slno"]);
                                            //int iStartColumn = 0;
                                            iColumnCounter = (17 * (iMonthNo - 1)) + 19;
                                            rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "1", objUtilityDB.GetColumnName(iColumnCounter + 16) + "1");
                                            rgHead.Cells.Value2 = dtExcel.Rows[i]["sr_doc_month"];


                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_pmd"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_plants"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_growmin"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_others"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_total_qty"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_points"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_revenue"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_cust"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_groups"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_pmd"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_plants"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_growmin"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_others"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_totqty"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_totpoints"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_revenue"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_cust"];

                                            iColumnCounter = iTotColumns - 24;
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_tot_permonths"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totpers_pmd"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totpers_plants"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totpers_growmin"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totpers_others"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totpers_totqty"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totpers_totpoints"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totpers_revenue"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totpers_cust"];

                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_tot_groupmonths"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroups"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_pmd"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_plants"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_growmin"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_others"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_totqty"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_totpoints"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_revenue"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_cust"];

                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_work_status"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_resign_month"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_veh_status"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_veh_eff"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_tabissue_status"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_tabissue_month"];
                                        }
                                    }
                                    else
                                    {
                                        rgHead = worksheet.get_Range("L1", "R2");
                                        rgHead.Merge(Type.Missing);
                                        //rgHead.Font.Size = 14;
                                        rgHead.Font.ColorIndex = 1;
                                        rgHead.Font.Bold = true;
                                        rgHead.Borders.Weight = 2;
                                        rgHead.Interior.ColorIndex = 31;
                                        rgHead.Font.ColorIndex = 2;
                                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                        rgHead.Cells.Value2 = "REPORTING STRUCTURE OF " + dtExcel.Rows[i]["sr_doc_month"].ToString().ToUpper();


                                        worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 3;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_eora_code"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_eora_name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToDateTime(dtExcel.Rows[i]["sr_doj"]).ToString("dd-MMM-yyyy");
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_comp_name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_branch_name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_serv_length"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rec_source"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rec_dept"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_trained_by"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_region"];

                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_gc_code"] + "-" + dtExcel.Rows[i]["sr_rep_gc_gl"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level1_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level1"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level2_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level2"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level3_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level3"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level4_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level4"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level5_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level5"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_rep_level6_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level6"];


                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sr_month_slno"]);
                                        //int iStartColumn = 0;
                                        iColumnCounter = (17 * (iMonthNo - 1)) + 19;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "1", objUtilityDB.GetColumnName(iColumnCounter + 16) + "1");
                                        rgHead.Cells.Value2 = dtExcel.Rows[i]["sr_doc_month"];

                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_pmd"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_plants"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_growmin"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_others"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_total_qty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_points"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_revenue"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_pers_cust"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_groups"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_pmd"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_plants"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_growmin"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_others"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_totqty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_totpoints"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_revenue"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_cust"];

                                        iColumnCounter = iTotColumns - 24;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_tot_permonths"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totpers_pmd"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totpers_plants"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totpers_growmin"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totpers_others"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totpers_totqty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totpers_totpoints"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totpers_revenue"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totpers_cust"];

                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_tot_groupmonths"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroups"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_pmd"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_plants"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_growmin"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_others"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_totqty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_totpoints"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_revenue"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_cust"];

                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_work_status"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_resign_month"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_veh_status"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_veh_eff"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_tabissue_status"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_tabissue_month"];

                                    }

                                    iColumnCounter = 1;
                                }
                                iStartColumn = (17 * (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"]) + 1)) + 1 + 19; iColumnCounter = iStartColumn;
                                rgHead = worksheet.get_Range("S" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4).ToString(),
                                                   objUtilityDB.GetColumnName(iStartColumn) + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4).ToString());
                                rgHead.Borders.Weight = 2;
                                rgHead.Font.Size = 12; rgHead.Font.Bold = true;

                                for (int iMonths = 0; iMonths <= Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"]); iMonths++)
                                {
                                    iStartColumn = (17 * iMonths) + 19; iColumnCounter = iStartColumn;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                    iColumnCounter = iColumnCounter + 1;
                                    worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4, iColumnCounter] = "=SUM(" + objUtilityDB.GetColumnName(iColumnCounter) + "4:" + objUtilityDB.GetColumnName(iColumnCounter) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 3).ToString() + ")";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    #endregion

                    #region iForm 2 :: CUMULATIVE_TEAK_EXCEL
                    else if (cbReport.SelectedValue.ToString() == "EMPLOYEE_PERFORMANCE_CUMULATIVE_TEAK_EXCEL")
                    {

                        try
                        {
                            objExcelDB = new ExcelDB();
                            objUtilityDB = new UtilityDB();
                            dtExcel = objExcelDB.GetSRWiseCumulativeBulletins(company, branches, documentMonth, finyear, "", "EMPLOYEE_PERFORMANCE_CUMULATIVE_TEAK_EXCEL").Tables[0];
                            objExcelDB = null;

                            if (dtExcel.Rows.Count > 0)
                            {
                                Excel.Application oXL = new Excel.Application();
                                Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                                Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                                oXL.Visible = true;
                                int iTotColumns = 0;
                                iTotColumns = 7 + (12 * (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"]) + 1));
                                string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                                Excel.Range rgHead = null;
                                Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                                Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4).ToString());
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
                                rgData = worksheet.get_Range("A5", sLastColumn + (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_emps"]) + 4).ToString());
                                rgData.WrapText = false;
                                rg = worksheet.get_Range("A4", Type.Missing);
                                rg.Cells.ColumnWidth = 4;
                                rg = worksheet.get_Range("B4", Type.Missing);
                                rg.Cells.ColumnWidth = 8;
                                rg = worksheet.get_Range("C4", Type.Missing);
                                rg.Cells.ColumnWidth = 30;
                                rg = worksheet.get_Range("D4", Type.Missing);
                                rg.Cells.ColumnWidth = 10;
                                rg = worksheet.get_Range("E4", Type.Missing);
                                rg.Cells.ColumnWidth = 30;
                                rg = worksheet.get_Range("F4", Type.Missing);
                                rg.Cells.ColumnWidth = 30;

                                //rg = worksheet.get_Range("D3", Type.Missing);
                                //rg.Cells.ColumnWidth = 10;
                                //rg = worksheet.get_Range("E3", "F3");
                                //rg.Cells.ColumnWidth = 40;
                                //rg = worksheet.get_Range("F3", Type.Missing);
                                //rg.Cells.ColumnWidth = 40;
                                //rg = worksheet.get_Range("H3", "J3");
                                //rg.Cells.ColumnWidth = 25;
                                //rg = worksheet.get_Range("I3", Type.Missing);
                                //rg.Cells.ColumnWidth = 10;



                                int iColumn = 1;
                                worksheet.Cells[4, iColumn++] = "SlNo";
                                worksheet.Cells[4, iColumn++] = "Ecode";
                                worksheet.Cells[4, iColumn++] = "Name";
                                worksheet.Cells[4, iColumn++] = "Doj";
                                worksheet.Cells[4, iColumn++] = "Company";
                                worksheet.Cells[4, iColumn++] = "Branch";
                                worksheet.Cells[4, iColumn++] = "Length of Service";


                                int iStartColumn = 0;
                                for (int iMonths = 0; iMonths < Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"]); iMonths++)
                                {
                                    rgHead = worksheet.get_Range("A1", "G2");
                                    rgHead.Merge(Type.Missing);
                                    rgHead.Font.Size = 14;
                                    rgHead.Font.ColorIndex = 1;
                                    rgHead.Font.Bold = true;
                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.Value2 = "CUMULATIVE BULLETINS";
                                    //objUtilityDB.GetColumnName(iTotColumns)

                                    iStartColumn = (12 * iMonths) + 8;
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 11) + "2");
                                    rgHead.Merge(Type.Missing); rgHead.Font.Size = 14; rgHead.Font.Bold = true;
                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                    //rgHead.Cells.Value2 = "TOTAL";
                                    rgHead.Font.Color = Excel.XlRgbColor.rgbBlack;
                                    rgHead.Borders.Weight = 2; rgHead.Interior.ColorIndex = 35 + iMonths;
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "4", objUtilityDB.GetColumnName(iStartColumn + 11) + "4");
                                    rgHead.Font.Bold = true;
                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                    //rgHead.Cells.Value2 = "TOTAL";
                                    rgHead.Font.Color = Excel.XlRgbColor.rgbBlack;
                                    rgHead.Borders.Weight = 2; rgHead.Interior.ColorIndex = 35 + iMonths;

                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "3", objUtilityDB.GetColumnName(iStartColumn + 2) + "3");
                                    rgHead.Merge(Type.Missing); rgHead.Font.Size = 14; rgHead.Font.Bold = true;
                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.Value2 = "SINGLE";
                                    rgHead.Borders.Weight = 2; rgHead.Interior.ColorIndex = 35 + iMonths;
                                    worksheet.Cells[4, iStartColumn++] = "Cust";
                                    worksheet.Cells[4, iStartColumn++] = "Units";
                                    worksheet.Cells[4, iStartColumn++] = "Amount";
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "3", objUtilityDB.GetColumnName(iStartColumn + 2) + "3");
                                    rgHead.Merge(Type.Missing); rgHead.Font.Size = 14; rgHead.Font.Bold = true;
                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.Value2 = "DISCOUNT";
                                    rgHead.Borders.Weight = 2; rgHead.Interior.ColorIndex = 35 + iMonths;
                                    worksheet.Cells[4, iStartColumn++] = "Cust";
                                    worksheet.Cells[4, iStartColumn++] = "Units";
                                    worksheet.Cells[4, iStartColumn++] = "Amount";
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "3", objUtilityDB.GetColumnName(iStartColumn + 2) + "3");
                                    rgHead.Merge(Type.Missing); rgHead.Font.Size = 14; rgHead.Font.Bold = true;
                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.Value2 = "BULK";
                                    rgHead.Borders.Weight = 2; rgHead.Interior.ColorIndex = 35 + iMonths;
                                    worksheet.Cells[4, iStartColumn++] = "Cust";
                                    worksheet.Cells[4, iStartColumn++] = "Units";
                                    worksheet.Cells[4, iStartColumn++] = "Amount";
                                    rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "3", objUtilityDB.GetColumnName(iStartColumn + 2) + "3");
                                    rgHead.Merge(Type.Missing); rgHead.Font.Size = 14; rgHead.Font.Bold = true;
                                    rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                    rgHead.Cells.Value2 = "TOTAL";
                                    rgHead.Borders.Weight = 2; rgHead.Interior.ColorIndex = 35 + iMonths;
                                    worksheet.Cells[4, iStartColumn++] = "Cust";
                                    worksheet.Cells[4, iStartColumn++] = "Units";
                                    worksheet.Cells[4, iStartColumn++] = "Amount";

                                }

                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "2", objUtilityDB.GetColumnName(iStartColumn + 11) + "2");
                                rgHead.Merge(Type.Missing); rgHead.Font.Size = 14; rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "GRAND TOTAL";
                                rgHead.Font.Color = Excel.XlRgbColor.rgbBlack;
                                rgHead.Borders.Weight = 2; rgHead.Interior.ColorIndex = 35 + Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"]);
                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "4", objUtilityDB.GetColumnName(iStartColumn + 11) + "4");
                                rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                //rgHead.Cells.Value2 = "TOTAL";
                                rgHead.Font.Color = Excel.XlRgbColor.rgbBlack;
                                rgHead.Borders.Weight = 2; rgHead.Interior.ColorIndex = 35 + Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"]);

                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "3", objUtilityDB.GetColumnName(iStartColumn + 2) + "3");
                                rgHead.Merge(Type.Missing); rgHead.Font.Size = 14; rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "SINGLE";
                                rgHead.Borders.Weight = 2; rgHead.Interior.ColorIndex = 35 + Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"]);
                                worksheet.Cells[4, iStartColumn++] = "Cust";
                                worksheet.Cells[4, iStartColumn++] = "Units";
                                worksheet.Cells[4, iStartColumn++] = "Amount";
                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "3", objUtilityDB.GetColumnName(iStartColumn + 2) + "3");
                                rgHead.Merge(Type.Missing); rgHead.Font.Size = 14; rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "DISCOUNT";
                                rgHead.Borders.Weight = 2; rgHead.Interior.ColorIndex = 35 + Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"]);
                                worksheet.Cells[4, iStartColumn++] = "Cust";
                                worksheet.Cells[4, iStartColumn++] = "Units";
                                worksheet.Cells[4, iStartColumn++] = "Amount";
                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "3", objUtilityDB.GetColumnName(iStartColumn + 2) + "3");
                                rgHead.Merge(Type.Missing); rgHead.Font.Size = 14; rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "BULK";
                                rgHead.Borders.Weight = 2; rgHead.Interior.ColorIndex = 35 + Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"]);
                                worksheet.Cells[4, iStartColumn++] = "Cust";
                                worksheet.Cells[4, iStartColumn++] = "Units";
                                worksheet.Cells[4, iStartColumn++] = "Amount";
                                rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iStartColumn) + "3", objUtilityDB.GetColumnName(iStartColumn + 2) + "3");
                                rgHead.Merge(Type.Missing); rgHead.Font.Size = 14; rgHead.Font.Bold = true;
                                rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;
                                rgHead.Cells.Value2 = "TOTAL";
                                rgHead.Borders.Weight = 2; rgHead.Interior.ColorIndex = 35 + Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"]);
                                worksheet.Cells[4, iStartColumn++] = "Cust";
                                worksheet.Cells[4, iStartColumn++] = "Units";
                                worksheet.Cells[4, iStartColumn++] = "Amount";




                                int iRowCounter = 5; int iColumnCounter = 1;
                                for (int i = 0; i < dtExcel.Rows.Count; i++)
                                {
                                    if (i > 0)
                                    {
                                        if (dtExcel.Rows[i]["sr_eora_code"].ToString() == dtExcel.Rows[i - 1]["sr_eora_code"].ToString())
                                        {
                                            int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sr_month_slno"]);
                                            //int iStartColumn = 0;
                                            iColumnCounter = (12 * (iMonthNo - 1)) + 8;
                                            rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 11) + "2");
                                            rgHead.Cells.Value2 = dtExcel.Rows[i]["sr_doc_month"];

                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_scust"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_sqty"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_srev"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_dcust"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_dqty"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_drev"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_bcust"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_bqty"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_brev"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_scust"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_dcust"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_bcust"]);
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_sqty"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_dqty"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_bqty"]);
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_srev"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_drev"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_brev"]);

                                            iColumnCounter = (12 * (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"]))) + 8;
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_scust"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_sqty"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_srev"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_dcust"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_dqty"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_drev"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_bcust"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_bqty"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_brev"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_scust"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_dcust"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_bcust"]);
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_sqty"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_dqty"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_bqty"]);
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_srev"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_drev"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_brev"]);
                                        }
                                        else
                                        {


                                            worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 4;
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_eora_code"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_eora_name"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToDateTime(dtExcel.Rows[i]["sr_doj"]).ToString("dd-MMM-yyyy");
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_comp_name"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_branch_name"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_serv_length"];

                                            iRowCounter++;
                                            int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sr_month_slno"]);
                                            //int iStartColumn = 0;
                                            iColumnCounter = (12 * (iMonthNo - 1)) + 8;
                                            rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 11) + "2");
                                            rgHead.Cells.Value2 = dtExcel.Rows[i]["sr_doc_month"];

                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_scust"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_sqty"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_srev"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_dcust"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_dqty"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_drev"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_bcust"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_bqty"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_brev"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_scust"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_dcust"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_bcust"]);
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_sqty"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_dqty"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_bqty"]);
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_srev"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_drev"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_brev"]);

                                            iColumnCounter = (12 * (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"]))) + 8;
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_scust"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_sqty"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_srev"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_dcust"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_dqty"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_drev"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_bcust"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_bqty"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_brev"];
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_scust"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_dcust"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_bcust"]);
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_sqty"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_dqty"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_bqty"]);
                                            worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_srev"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_drev"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_brev"]);
                                        }
                                    }
                                    else
                                    {


                                        worksheet.Cells[iRowCounter, iColumnCounter++] = iRowCounter - 4;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_eora_code"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_eora_name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToDateTime(dtExcel.Rows[i]["sr_doj"]).ToString("dd-MMM-yyyy");
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_comp_name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_branch_name"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_serv_length"];

                                        int iMonthNo = Convert.ToInt32(dtExcel.Rows[i]["sr_month_slno"]);
                                        //int iStartColumn = 0;
                                        iColumnCounter = (12 * (iMonthNo - 1)) + 8;
                                        rgHead = worksheet.get_Range(objUtilityDB.GetColumnName(iColumnCounter) + "2", objUtilityDB.GetColumnName(iColumnCounter + 11) + "2");
                                        rgHead.Cells.Value2 = dtExcel.Rows[i]["sr_doc_month"];

                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_scust"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_sqty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_srev"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_dcust"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_dqty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_drev"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_bcust"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_bqty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_group_teak_brev"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_scust"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_dcust"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_bcust"]);
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_sqty"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_dqty"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_bqty"]);
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_srev"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_drev"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_group_teak_brev"]);

                                        iColumnCounter = (12 * (Convert.ToInt32(dtExcel.Rows[0]["sr_noof_months"]))) + 8;
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_scust"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_sqty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_srev"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_dcust"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_dqty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_drev"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_bcust"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_bqty"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = dtExcel.Rows[i]["sr_totgroup_teak_brev"];
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_scust"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_dcust"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_bcust"]);
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_sqty"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_dqty"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_bqty"]);
                                        worksheet.Cells[iRowCounter, iColumnCounter++] = Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_srev"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_drev"]) + Convert.ToInt32(dtExcel.Rows[i]["sr_totgroup_teak_brev"]);
                                    }

                                    iColumnCounter = 1;
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

                //else
                //{
                //    MessageBox.Show("There is no Download for this Report", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }

        }

        private void cbReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (iForm == 2)
            {
                if (cbReport.SelectedValue.ToString() == "TEAK_SALES_AGAINST_QTY")
                {
                    lblFrom.Text = "Qty Diff";
                    lblTo.Text = "Max";
                    lblAvg.Visible = false;
                }
                else
                {
                    lblFrom.Text = "From";
                    lblTo.Text = "To";
                    lblAvg.Visible = true;
                }
            }
            if (iForm == 8)
            {
                if (cbReport.SelectedIndex == 0)
                {
                    clbReport.Items.Clear();

                    NewCheckboxListItem oclBox = new NewCheckboxListItem();
                    oclBox.Tag = "DETAILED";
                    oclBox.Text = "DETAILED";
                    clbReport.Items.Add(oclBox);
                    oclBox = null;
                }
                else if (cbReport.SelectedIndex == 1)
                {
                    clbReport.Items.Clear();

                    NewCheckboxListItem oclBox = new NewCheckboxListItem();
                    oclBox.Tag = "SUMMARY";
                    oclBox.Text = "SUMMARY";
                    clbReport.Items.Add(oclBox);
                    oclBox = null;
                }
            }
        }

        private void clbReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            Products = "";
            if (clbReport.Items.Count > 0)
            {
                for (int i = 0; i < clbReport.CheckedItems.Count; i++)
                {
                    Products += "" + ((NewCheckboxListItem)clbReport.CheckedItems[i]).Tag + ",";
                }

                Products = Products.TrimEnd(',');
            }
        }
        private void GetSelectedProductIds()
        {
            if (iForm == 10 || iForm==11)
            {
                Products = "";
                if (clbReport.Items.Count > 0)
                {
                    for (int i = 0; i < clbReport.CheckedItems.Count; i++)
                    {
                        Products += "" + ((NewCheckboxListItem)clbReport.CheckedItems[i]).Tag + ",";
                    }

                    Products = Products.TrimEnd(',');
                }
            }
        }
       
    }
}
