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
    public partial class frmDoorknocks : Form
    {
        //Security objData;
        //HRInfo objHRInfo;
        private SQLDB objDB = null;
        private ExcelDB objExDb = null;
        UtilityDB objUtilityDB = null;
        string strBranchType = "";
        int FormID = 0;
        string strRep = "", strLogicalBranches="",strEcodes = "";
        public frmDoorknocks()
        {
            InitializeComponent();
        }
        public frmDoorknocks(int FrmID)
        {
            InitializeComponent();
            FormID = FrmID;
            if (FrmID == 1)
                Text = "Door Knocks";
            else if (FrmID == 3)
                Text = "SR Wise Bulletins";
            else if (FrmID == 4)
                Text = "GC&GL Wise Bulletins";
            else if (FrmID == 5)
                Text = "GC&GL Advances Report";
            else if (FrmID == 6)
                Text = "Sales Staff Strength Report";
            else if (FrmID == 7)
                Text = "Branch Wise Bulletins";
            else if (FrmID == 8)
                Text = "Organization Chart";
            else if (FrmID == 9)
                Text = "SALES BULLETIN REG WITHOUT CUSTOMER DATA";
            else if (FrmID == 11)
                Text = "Branch Bulletins :: All";
            else if (FrmID == 12)
                Text = "Branch Product Sales :: All";
            else if (FrmID == 13)
                Text = "Employee Performance";
            else if (FrmID == 14)
                Text = "SR Wise PMD DA Demos";
            else if (FrmID == 15)
                Text = "SR Wise Bulletins";
            else if (FrmID == 16)
                Text = "GC&GL Wise Bulletins";
            else if (FrmID == 17)
                Text = "Group & SR Wise Product Reconsilation";
            else if (FrmID == 18)
                Text = "Group Wise Product Reconsilation";
            else if (FrmID == 19)
                Text = "Group Wise Sales Accountability";
            else if (FrmID == 20)
                Text = "School Visits :: Summary";
            else if (FrmID == 21)
                Text = "Farmer Meet :: Summary";
            else if (FrmID == 22)
                Text = "Demo Plots :: Summary";
            else if (FrmID == 23)
                Text = "Wrong Commitments / Financial Frauds :: Summary";
            else if (FrmID == 24)
                Text = "GC/GL Collection Statement";
            else if (FrmID == 25)
                Text = "GC/GL Check List";
            else if (FrmID == 26)
                Text = "Branch Wise Bulletin Checklist";
            else if (FrmID == 27)
                Text = "GCGL Wise Bulletin Checklist";
            else if (FrmID == 28)
                Text = "Mobile Bill Payments";
            else if (FrmID == 29)
                Text = "Sales Register :: Combi Splitting";
            else if (FrmID == 30)
                Text = "PayRoll :: Loan Recivery Statement";
            else if (FrmID == 31)
                Text = "Promotions :: Eligible Candidates";
            else if (FrmID == 32)
                Text = "Promotions :: Not Eligible Candidates";
            else if (FrmID == 33)
                Text = "GC/GL Wise Sales Accountability";
            else if (FrmID == 34)
                Text = "Branch Wise Recruitement Summary";
            else if (FrmID == 35)
                Text = "Branch Wise Petrol Allw ExpiryList";
            else if (FrmID == 36)
                Text = "SalesTeam Organization Chart";
            else if (FrmID == 37)
                Text = "Audit Organization Chart";
            else if (FrmID == 38)
                Text = "Product Wise Sales :: SR Wise";
            else if (FrmID == 39)
                Text = "Product Wise Sales :: Product CrossTab";
            else if (FrmID == 40)
                Text = "Low Dispatch SP's";
            else if (FrmID == 41)
                Text = "Recruitment vs Resigned";
            else if (FrmID == 43)
                Text = "Service Organization Chart";
            else if (FrmID == 44)
                Text = "Crates Reconsilation Summary";
            else if (FrmID == 45)
                Text = "SR Wise Bulletins";
            else if (FrmID == 46)
                Text = "Sales Organization Chart Excel";
            else if (FrmID == 47)            
                Text = "Transport Cost_Sales & Replacement";
            else if (FrmID == 48)
                Text = "Group Wise Bulletins";
            else if (FrmID == 49)
                Text = "TM & ABOVE Stock Reconciliation";
            else if (FrmID == 50)
                Text = "Order Form Reconcilation GC";
            else if (FrmID == 51)
                Text = "Order Form Reconcilation TM & ABOVE";
            else if (FrmID == 52)
                Text = "GL Wise Branch Performance Statement";
            else if (FrmID == 53)
                Text = "GL Wise Sales Bulletins";
            else if (FrmID == 54)
                Text = "TM & ABOVE Sales Bulletins";
            else if (FrmID == 55)
                Text = "AO Wise Stock Reconciliation";

            else if (FrmID == 56)
                Text = "Advance Register";
            else if (FrmID == 57)
                Text = "GC Wise Product Reconciliation";
            else if (FrmID == 58)
                Text = "GC/GL Wise Accountability";
            else if (FrmID == 59)
                Text = "GC/GL Wise Doc Sheet";
            else if (FrmID == 60)
                Text = "Sales Register";
            else if (FrmID == 61)
                Text = "Recruitment vs Resigned ";
            else if (FrmID == 62)
                Text = "Sales Register Detailed";

            else
                Text = "Sales Order";

        }

        public frmDoorknocks(string sRep)
        {
            InitializeComponent();
            strRep = sRep;
            if (strRep == "STOCKPOINT_DC")
                Text = "STOCK POINT :: DC";
            else if (strRep == "SP_PENDING_DC")
                Text = "STOCK POINT :: PENDING DC";
            else if (strRep == "STOCKPOINT_DCST")
                Text = "STOCK POINT :: DCST";
            else if (strRep == "STOCKPOINT_GRN")
                Text = "STOCK POINT :: GRN";
            else if (strRep == "STOCKPOINT_RECONSILATION")
                Text = "STOCK POINT :: RECONSILATION";
            else if (strRep == "EMP_CONTACT_DETAILS")
                Text = "HR :: Emp Contact Details";
            else
                Text = "";
        }

        private void frmDoorknocks_Load(object sender, EventArgs e)
        {
            dtpDocMonth.Value = Convert.ToDateTime(Convert.ToDateTime(String.Format("01" + CommonData.DocMonth).ToString()).ToString("dd/MM/yyyy"));

            chkBrAll.Visible = false;
            if (FormID == 2)
            {
                lblLeadType.Visible = true;
                lblLeadType.Text = "Report";
                lblDempType.Visible = false;
                cmleadType.Visible = false;
                chkDemoType.Visible = false;
                cbReportType.Visible = true;
                cbReportType.SelectedIndex = 1;
            }

            else if (FormID == 3 || FormID == 4 || FormID == 5 || FormID == 6 || FormID == 7
                || FormID == 8 || FormID == 9 || FormID == 11 || FormID == 12 || FormID == 13
                || FormID == 14 || FormID == 15 || FormID == 16 || FormID == 17 || FormID == 18
                || FormID == 19 || FormID == 20 || FormID == 21 || FormID == 22 || FormID == 23
                || FormID == 25 || FormID == 26 || FormID == 27 || FormID == 28 || FormID == 29 
                || FormID == 30 || FormID == 31 || FormID == 32 || FormID == 33 || FormID == 34 
                || FormID == 35 || FormID == 36 || FormID == 37 || FormID == 38 || FormID == 39 
                || FormID == 41 || FormID == 42 || FormID == 43 || FormID == 44 || FormID == 49
                || FormID == 51 || FormID == 54 || FormID == 55 || FormID == 61 || FormID == 62)
            {
                lblLeadType.Visible = false;
                lblDempType.Visible = false;
                cmleadType.Visible = false;
                chkDemoType.Visible = false;
                cbReportType.Visible = false;
                lblLeadType.Visible = false;

            }

            else if (FormID == 45 || FormID == 48 || FormID == 47 || FormID == 50
                   || FormID == 52 || FormID == 53 || FormID == 56 || FormID == 58 || FormID == 59 || FormID == 60)
            {
                lblLeadType.Visible = false;
                lblDempType.Visible = true;
                lblDempType.Text = "Logical Branches";
                cmleadType.Visible = false;
                chkDemoType.Visible = true;
                cbReportType.Visible = false;
                lblLeadType.Visible = false;
                FillLogBranchesToList();

            }
            else if (FormID == 57)
            {
                lblLeadType.Visible = false;
                lblDempType.Visible = true;
                lblDempType.Text = "GC/GL's List ";
                cmleadType.Visible = false;
                chkDemoType.Visible = true;
                cbReportType.Visible = false;
                lblLeadType.Visible = false;
                FillGCGLEcodesToList();

            }

            //else if (FormID == 14)
            //{
            //    lblLeadType.Visible = false;
            //    lblDempType.Visible = true;
            //    lblDempType.Text = "Logical Branches";
            //    cmleadType.Visible = false;
            //    chkDemoType.Visible = true;
            //    cbReportType.Visible = false;
            //    lblLeadType.Visible = false;
            //    FillLogBranchesToList();

            //}
            else if (FormID == 46)
            {
                lblLeadType.Visible = false;
                lblDempType.Visible = true;
                lblDempType.Text = "Logical Branches";
                cmleadType.Visible = false;
                chkDemoType.Visible = true;
                cbReportType.Visible = false;
                lblLeadType.Visible = false;
                btnReport.Enabled = false;
                FillLogBranchesToList();
            }
            else if (FormID == 24)
            {
                FillReportType();
                cbReportType.SelectedIndex = 0;
                lblLeadType.Text = "ReportType";
                lblDempType.Visible = false;
                cmleadType.Visible = false;
                chkDemoType.Visible = false;
                cbReportType.Visible = true;

            }
            else if (strRep == "STOCKPOINT_DC" || strRep == "STOCKPOINT_DCST" || strRep == "STOCKPOINT_GRN"
            || strRep == "STOCKPOINT_RECONSILATION" || strRep == "EMP_CONTACT_DETAILS" || strRep == "SP_PENDING_DC")
            {
                lblLeadType.Visible = false;
                lblDempType.Visible = false;
                cmleadType.Visible = false;
                chkDemoType.Visible = false;
                cbReportType.Visible = false;
                lblLeadType.Visible = false;
            }
            else
            {
                //if (FormID != 24)
                {
                    lblLeadType.Visible = true;
                    lblDempType.Visible = true;
                    cmleadType.Visible = true;
                    chkDemoType.Visible = true;
                    cbReportType.Visible = false;
                    FillReportTypeToList();
                }
            }
            if ((FormID == 7) || (FormID == 2) || (FormID == 3) || (FormID == 4) || FormID == 30 || FormID == 45
                              || FormID == 46 || FormID == 47 || FormID == 48 || FormID == 49 || FormID == 50 
                              || FormID == 51 || FormID == 52 || FormID == 53 || FormID == 54 || FormID == 55
                              || FormID == 56 || FormID == 58 || FormID == 59 || FormID == 60 || FormID==6)
                btnDownload.Visible = true;
            else
                btnDownload.Visible = false;

            //objData = new Security();
            //UtilityLibrary.PopulateControl(cmbCompany, objData.GetCompanyDataSet().Tables[0].DefaultView, 1, 0, "-- Please Select --", 0);
            //cmleadType.SelectedIndex = 0;
        }
        private void FillReportType()
        {
            cbReportType.Items.Clear();
            if (FormID == 24)
            {
                cbReportType.Items.Add("REPORT1");
                cbReportType.Items.Add("REPORT2");
            }
        }

        private void FillReportTypeToList()
        {

            NewCheckboxListItem oclBox = new NewCheckboxListItem();
            oclBox.Tag = "CAMPAIGN REQUIRED";
            oclBox.Text = "CAMPAIGN REQUIRED";
            cbReportType.Items.Add(oclBox);
            oclBox = null;
            NewCheckboxListItem oclBox1 = new NewCheckboxListItem();
            oclBox.Tag = "TO BE DONE BY SR";
            oclBox.Text = "TO BE DONE BY SR";
            cbReportType.Items.Add(oclBox1);
            oclBox = null;
            NewCheckboxListItem oclBox2 = new NewCheckboxListItem();
            oclBox.Tag = "CAMPAIGN REQUIRED";
            oclBox.Text = "CAMPAIGN REQUIRED";
            cbReportType.Items.Add(oclBox2);
            oclBox = null;
            NewCheckboxListItem oclBox3 = new NewCheckboxListItem();
            oclBox.Tag = "CAMPAIGN REQUIRED";
            oclBox.Text = "CAMPAIGN REQUIRED";
            cbReportType.Items.Add(oclBox3);
            oclBox = null;
                    
                
           
        }

        private void FillLogBranchesToList()
        {
          

            objDB = new SQLDB();
            string sqlText = "";
            chkDemoType.Items.Clear();
            chkBrAll.Checked = false;
            DataTable dt = new DataTable();
            sqlText = "SELECT DISTINCT LGM_LOG_BRANCH_CODE,LOG_BRANCH_NAME FROM LevelGroup_map " +
                " INNER JOIN BRANCH_MAS_LOGICAL  ON LOG_BRANCH_CODE=LGM_LOG_BRANCH_CODE " +
                " WHERE LGM_BRANCH_CODE='" + CommonData.BranchCode + "' AND LGM_DOC_MONTH='" +
                ""+ dtpDocMonth.Value.ToString("MMMyyyy") + "' AND ACTIVE='T'";
            dt = objDB.ExecuteDataSet(sqlText).Tables[0];
            if (dt.Rows.Count > 0)
            {
                chkBrAll.Visible = true;
                foreach (DataRow dataRow in dt.Rows)
                {
                    if (dataRow["LOG_BRANCH_NAME"] + "" != "")
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["LGM_LOG_BRANCH_CODE"].ToString();
                        oclBox.Text = dataRow["LOG_BRANCH_NAME"].ToString();
                        chkDemoType.Items.Add(oclBox);
                        oclBox = null;
                    }
                }
            }
        }

        private void GetSelectedLogicalBranches()
        {
            strLogicalBranches = "";
            if (chkDemoType.Items.Count > 0)
            {
                for (int i = 0; i < chkDemoType.CheckedItems.Count; i++)
                {
                    strLogicalBranches += "" + ((NewCheckboxListItem)chkDemoType.CheckedItems[i]).Tag + ",";
                }

                strLogicalBranches = strLogicalBranches.TrimEnd(',');
            }
        }


        private void FillGCGLEcodesToList()
        {
            objDB = new SQLDB();
            string sqlText = "";
            chkDemoType.Items.Clear();         
            DataTable dt = new DataTable();
            sqlText = "SELECT DISTINCT lgm_gl_ecode EmpCode " +
                      ",cast(lgm_gl_ecode as varchar)+'-'+MEMBER_NAME EmpName " +
                      " FROM LevelGroup_map " +
                      "INNER JOIN EORA_MASTER ON ECODE=lgm_gl_ecode " +
                      " WHERE lgm_company_code='" + CommonData.CompanyCode +
                      "' AND lgm_branch_code='" + CommonData.BranchCode +
                      "' AND lgm_doc_month='" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy").ToUpper() +
                      "' order by lgm_gl_ecode asc ";
            dt = objDB.ExecuteDataSet(sqlText).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    if (dataRow["EmpCode"] + "" != "")
                    {
                        NewCheckboxListItem oclBox = new NewCheckboxListItem();
                        oclBox.Tag = dataRow["EmpCode"].ToString();
                        oclBox.Text = dataRow["EmpName"].ToString();
                        chkDemoType.Items.Add(oclBox);
                        oclBox = null;
                    }
                }
            }
        }


        private void GetSelectedGCGLEcodes()
        {
            strEcodes = "";
            if (chkDemoType.Items.Count > 0)
            {
                for (int i = 0; i < chkDemoType.Items.Count; i++)
                {
                    if (chkDemoType.GetItemCheckState(i) == CheckState.Checked)
                    {
                        strEcodes += ((NewCheckboxListItem)chkDemoType.Items[i]).Tag.ToString() + ',';
                    }
                }

                strEcodes = strLogicalBranches.TrimEnd(',');
            }
        }


        private void btnReport_Click(object sender, EventArgs e)
        {
            GetSelectedLogicalBranches();
            GetSelectedGCGLEcodes();
            string strComp = "", strBranch = "";
            if (FormID == 7 || FormID == 11 || FormID == 26 || FormID == 34
                || FormID == 35 || FormID == 40 || FormID == 42)
            {
                objDB = new SQLDB();
                string sqlText = "";
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();

                sqlText = "SELECT DISTINCT COMPANY_CODE FROM USER_BRANCH INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE WHERE UB_USER_ID = '" + CommonData.LogUserId + "'";
                dt = objDB.ExecuteDataSet(sqlText).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (strComp != "")
                            strComp += ",";
                        strComp += dt.Rows[i]["COMPANY_CODE"].ToString();
                    }
                }
                else
                {
                    strComp += CommonData.CompanyCode.ToString();
                }
                sqlText = "SELECT UB_BRANCH_CODE FROM USER_BRANCH WHERE UB_USER_ID = '" + CommonData.LogUserId + "'";
                dt2 = objDB.ExecuteDataSet(sqlText).Tables[0];
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        if (strBranch != "")
                            strBranch += ",";
                        strBranch += dt2.Rows[i]["UB_BRANCH_CODE"].ToString();
                    }
                }
                else
                {
                    strBranch += CommonData.BranchCode.ToString();
                }
            }
            if (FormID == 2)
            {
                if (cbReportType.SelectedIndex > -1)
                {
                    if (cbReportType.SelectedIndex == 0)
                    {
                        CommonData.ViewReport = "SALESORDER";
                        ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"), 0);
                        objReportview.Show();
                    }
                    else if (cbReportType.SelectedIndex == 1)
                    {
                        CommonData.ViewReport = "SALESORDER_ORDER_BY_ORDER_NO";
                        ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"), 0);
                        objReportview.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Select Report Type", "SSCRM-SeleOrders-Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (FormID == 3)
            {
                CommonData.ViewReport = "SR_WISE_SALES_BULLETINS";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"), "SR-S ONLY");
                objReportview.Show();
            }
            else if (FormID == 4)
            {
                CommonData.ViewReport = "GL_WISE_SALES_BULLETINS";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"), "GL-S ONLY");
                objReportview.Show();
            }
            else if (FormID == 5)
            {
                CommonData.ViewReport = "GL_WISE_ADVANCES_SUMMERY";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"), "");
                objReportview.Show();
            }
            else if (FormID == 6)
            {
                CommonData.ViewReport = "Sales Staff Strength";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"), "");
                objReportview.Show();
            }
            else if (FormID == 7)
            {                
                CommonData.ViewReport = "BRANCH_WISE_BULLETINS";
                ReportViewer objReportview = new ReportViewer(strComp, strBranch, dtpDocMonth.Value.ToString("MMMyyyy"), "");
                objReportview.Show();
            }
            else if (FormID == 8)
            {
                try
                {
                    //frmorganizationChart childfrmorganizationChart = new frmorganizationChart(Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy"));
                    //childfrmorganizationChart.Show();
                    webBrowser1.Navigate("http://www.shivashakthigroup.net/sty/default.aspx?Company=" + CommonData.CompanyCode + "&&Branch=" + CommonData.BranchCode + "&&DocMonth=" + Convert.ToDateTime(dtpDocMonth.Value).ToString("MMMyyyy"), true);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (FormID == 9)
            {
                CommonData.ViewReport = "SALES_BULLETIN_REG_WITHOUT_CUST";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"), "");
                objReportview.Show();
            }
            else if (FormID == 11)
            {
                CommonData.ViewReport = "BRANCH_WISE_BULLETINS_BY_ALL";
                ReportViewer objReportview = new ReportViewer(strComp, strBranch, dtpDocMonth.Value.ToString("MMMyyyy"), "ALL");
                objReportview.Show();
            }
            else if (FormID == 12)
            {
                CommonData.ViewReport = "BRANCH_WISE_PRODUCT_RECONSILATION";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"), "BRANCH_WISE");
                objReportview.Show();
            }
            else if (FormID == 13)
            {
                CommonData.ViewReport = "BRANCH_WISE_EMP_PERFORMANCE_MONTHLY";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"), "0", "ALL");
                objReportview.Show();
            }
            else if (FormID == 14)
            {
                CommonData.ViewReport = "BRANCH_WISE_EMP_PMD_DA_DEMOS";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"), "0", "ALL");
                objReportview.Show();
            }
            else if (FormID == 15)
            {
                CommonData.ViewReport = "SR_WISE_SALES_BULLETINS";
                ReportViewer objReportview = new ReportViewer("SSBVERBAL", CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "SR-S ONLY");
                objReportview.Show();
            }
            else if (FormID == 16)
            {
                CommonData.ViewReport = "GL_WISE_SALES_BULLETINS";
                ReportViewer objReportview = new ReportViewer("SSBVERBAL", CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "GL-S ONLY");
                objReportview.Show();
            }
            else if (FormID == 17)
            {
                CommonData.ViewReport = "REP_BRANCH_GROUP_SR_WISE_PRODUCT_REC_SUMMARY";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "BRANCH_GROUP_SR_WISE_SUMMARY");
                objReportview.Show();
            }
            else if (FormID == 18)
            {
                CommonData.ViewReport = "REP_BRANCH_GROUP_WISE_PRODUCT_REC_SUMMARY";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "BRANCH_GROUP_WISE_SUMMARY");
                objReportview.Show();
            }
            else if (FormID == 19)
            {
                CommonData.ViewReport = "REP_BRANCH_GROUP_WISE_SALES_ACCOUNTABILITY";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "BRANCH_GROUP_WISE_SUMMARY");
                objReportview.Show();
            }
            else if (FormID == 20)
            {
                CommonData.ViewReport = "SSCRM_REP_SCHOOL_VISIT_DETAILS";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "DETAILED");
                objReportview.Show();
            }
            else if (FormID == 21)
            {
                CommonData.ViewReport = "SSCRM_REP_FARMER_MEETINGS_DETL";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "DETAILED");
                objReportview.Show();
            }
            else if (FormID == 22)
            {
                CommonData.ViewReport = "SSCRM_REP_SERVICE_DEMO_PLOTS";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "DETAILED");
                objReportview.Show();
            }
            else if (FormID == 23)
            {
                CommonData.ViewReport = "SSCRM_REP_SERVICE_WC_FF_DETAILS";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "DETAILED");
                objReportview.Show();
            }
            else if (FormID == 24)
            {
                if (cbReportType.SelectedIndex == 0)
                {
                    CommonData.ViewReport = "SSCRM_REP_GC_GL_COLLECTION_STATEMENT";
                    ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "DETAILED");
                    objReportview.Show();
                }
                else
                {
                    CommonData.ViewReport = "SSCRM_REP_GC_GL_COLLECTION_STATEMENT_2";
                    ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "DETAILED");
                    objReportview.Show();
                }
            }
            else if (FormID == 25)
            {
                CommonData.ViewReport = "SSCRM_REP_GC_GL_CHECKLIST";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "GROUP");
                objReportview.Show();
            }
            else if (FormID == 26)
            {
                CommonData.ViewReport = "SSCRM_REP_BRANCH_BULTN_CHECKLIST";
                ReportViewer objReportview = new ReportViewer(strComp, strBranch, dtpDocMonth.Value.ToString("MMMyyyy"), "ALL");
                objReportview.Show();
            }
            else if (FormID == 27)
            {
                CommonData.ViewReport = "SSCRM_REP_GCGL_BULTN_CHECKLIST";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "GROUP SUMMARY");
                objReportview.Show();
            }
            else if (FormID == 28)
            {
                CommonData.ViewReport = "SSCRM_REP_MOBILE_BILL_PAYMENTS";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "GROUP SUMMARY");
                objReportview.Show();
            }
            else if (FormID == 29)
            {
                CommonData.ViewReport = "InvoiceDetailSplitting";
                crReportParams.FromDate = dtpDocMonth.Value.ToString("dd/MMM/yyyy");
                crReportParams.ToDate = dtpDocMonth.Value.ToString("dd/MMM/yyyy");
                ReportViewer objReportview = new ReportViewer("SPLITTING",CommonData.DocMonth);
                objReportview.Show();
            }
            else if (FormID == 31)
            {
                CommonData.ViewReport = "SSCRM_REP_PROM_ELIG_LIST";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), "", dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "", "ELIGIBLE");
                objReportview.Show();
            }
            else if (FormID == 32)
            {
                CommonData.ViewReport = "SSCRM_REP_PROM_ELIG_LIST";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), "", dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "", "NOT ELIGIBLE");
                objReportview.Show();
            }
            else if (FormID == 33)
            {
                CommonData.ViewReport = "REP_BRANCH_GCGL_SALES_ACCOUNTABILITY";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "SUM-GC-ONLY");
                objReportview.Show();
            }
            else if (FormID == 34)
            {
                CommonData.ViewReport = "BRANCH_WISE_RECRUITEMENT_SUMMARY";
                ReportViewer objReportview = new ReportViewer(strComp, strBranch, dtpDocMonth.Value.ToString("MMMyyyy"), "");
                objReportview.Show();
            }
            else if (FormID == 35)
            {
                CommonData.ViewReport = "SSCRM_REP_VEH_PETROL_ALLOW_EXP";
                ReportViewer objReportview = new ReportViewer(strComp, strBranch, dtpDocMonth.Value.ToString("MMMyyyy"), "");
                objReportview.Show();
            }
             else if (FormID == 36)
            {
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth);
                CommonData.ViewReport = "Organisation Chart";
                objReportview.Show();
            }

            else if (FormID == 37)
            {
                CommonData.ViewReport = "SSCRM_REP_AUDIT_ORG_CHART";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode,CommonData.BranchCode,"400000",dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(),"DETAILED");               
                objReportview.Show();
            }
            else if (FormID == 38)
            {
                CommonData.ViewReport = "SSCRM_REP_PRODUCT_WISE_SALES_PRODCRTAB";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "DETAILED");
                objReportview.Show();
            }
            else if (FormID == 39)
            {
                CommonData.ViewReport = "SSCRM_REP_PRODUCT_WISE_SALES_CRTAB";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "DETAILED");
                objReportview.Show();
            }
            else if (FormID == 41)
            {
                ReportViewer objReportview = new ReportViewer("ALL", "ALL", "", dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "");
                CommonData.ViewReport = "RECRUITMENT_VS_RESIGNED_DETAILS";
                objReportview.Show();
            }
            else if (FormID == 42)
            {
                CommonData.ViewReport = "BRANCH_WISE_AFC_STATEMENT_MNTHLY";
                ReportViewer objReportview = new ReportViewer(strComp, strBranch, dtpDocMonth.Value.ToString("MMMyyyy"), "");
                objReportview.Show();
            }
            else if (FormID == 43)
            {
                CommonData.ViewReport = "SSCRM_REP_SERVICE_ORG_CHART";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, "800000", dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "DETAILED");
                objReportview.Show();
            }
            else if (FormID == 44)
            {
                CommonData.ViewReport = "REP_BRANCH_GROUP_SR_WISE_CRATES_REC_SUMMARY";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "BRANCH_GROUP_SR_WISE_SUMMARY");
                objReportview.Show();
            }
            else if (FormID == 45)
            {
                CommonData.ViewReport = "SSERP_REP_SR_WISE_TOP_TO_BOTTOM";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), strLogicalBranches, "");
                objReportview.Show();
            }
            else if (FormID == 47)
            {
                CommonData.ViewReport = "TRANSPORT_COST_SALES_REPLACEMENT";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), strLogicalBranches, "", dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "");
                objReportview.Show();
            }
            else if (FormID == 48)
            {
                CommonData.ViewReport = "GROUP_WISE_STOCK_RECONCILIATION";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), strLogicalBranches, 0, "GROUP_WISE_SALES");
                objReportview.Show();
            }
            else if (FormID == 49)
            {
                CommonData.ViewReport = "SSERP_REP_TM_AND_ABOVE_STOCK_RECONCILIATION";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "ALL", 0, "TM_AND_ABOVE");
                objReportview.Show();

            }

            else if (strRep == "STOCKPOINT_DC")
            {
                CommonData.ViewReport = "STOCKPOINT_DC";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"),"");
                objReportview.Show();
            }
            else if (strRep == "SP_PENDING_DC")
            {
                CommonData.ViewReport = "STOCKPOINT_DC";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"),"PENDING");
                objReportview.Show();
            }
            else if (strRep == "STOCKPOINT_DCST")
            {
                CommonData.ViewReport = "STOCKPOINT_DCST";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"));
                objReportview.Show();
            }
            else if (strRep == "STOCKPOINT_GRN")
            {
                CommonData.ViewReport = "STOCKPOINT_GRN";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"));
                objReportview.Show();
            }
            else if (strRep == "STOCKPOINT_RECONSILATION")
            {
                CommonData.ViewReport = "STOCKPOINT_STOCK_RECONSILATION";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"));
                objReportview.Show();
            }
            else if (strRep == "EMP_CONTACT_DETAILS")
            {
                CommonData.ViewReport = "SSCRM_HR_REP_EMP_CONTACT_DETAILS";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"));
                objReportview.Show();
            }
            else if (strRep == "IT_SYS_INV_CPU")
            {
                CommonData.ViewReport = "IT_SYS_INV_CPU";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy"));
                objReportview.Show();
            }
            else if (FormID == 50)
            {
                string strChkDemo = "";
                for (int i = 0; i < chkDemoType.CheckedItems.Count; i++)
                {
                    NewCheckboxListItem CL = (NewCheckboxListItem)chkDemoType.CheckedItems[i];
                    strChkDemo += "" + CL.Tag.ToString() + ",";
                }

                CommonData.ViewReport = "SSCRM_MIS_REP_GL_LOG_WISE_ORDERFORM_RECONSILATION";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, strChkDemo.TrimEnd(','), CommonData.FinancialYear, dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "GL_ORDER_RECON_SUMMERY");
                objReportview.Show();
            }

            else if (FormID == 52)
            {
                string strChkDemo = "";
                for (int i = 0; i < chkDemoType.CheckedItems.Count; i++)
                {
                    NewCheckboxListItem CL = (NewCheckboxListItem)chkDemoType.CheckedItems[i];
                    strChkDemo += "" + CL.Tag.ToString() + ",";
                }

                CommonData.ViewReport = "SSCRM_MIS_GL_WISE_BRANCH_PERFORMANCE_STMENT";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, strChkDemo.TrimEnd(','),  dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "GL_PRODUCTWISE_STATEMENT");
                objReportview.Show();
            }
            else if (FormID == 53)
            {
                string strChkDemo = "";
                for (int i = 0; i < chkDemoType.CheckedItems.Count; i++)
                {
                    NewCheckboxListItem CL = (NewCheckboxListItem)chkDemoType.CheckedItems[i];
                    strChkDemo += "" + CL.Tag.ToString() + ",";
                }

                CommonData.ViewReport = "SSCRM_MIS_GCGL_WISE_SALES_BULLETIN";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, strChkDemo.TrimEnd(','), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "GL_PRODUCTWISE_STATEMENT");
                objReportview.Show();
            }
            else if (FormID == 51)
            {
                CommonData.ViewReport = "SSCRM_MIS_REP_TM_ORDERFRM_RECONSILATION";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, "", CommonData.FinancialYear, dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "TM_ORDER_RECON_SUMMERY");
                objReportview.Show();
            }
            else if (FormID == 54)
            {
                CommonData.ViewReport = "SSCRM_MIS_TMAB_SALES_BULLETIN";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "", "TM_PRODUCTWISE_STATEMENT");
                objReportview.Show();
            }
            else if (FormID == 55)
            {
                CommonData.ViewReport = "SSERP_REP_AO_WISE_STOCK_RECONCILIATION";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "", "", "");
                objReportview.Show();

            }
            else if (FormID == 56)
            {
                 string strChkDemo = "";
                for (int i = 0; i < chkDemoType.CheckedItems.Count; i++)
                {
                    NewCheckboxListItem CL = (NewCheckboxListItem)chkDemoType.CheckedItems[i];
                    strChkDemo += "" + CL.Tag.ToString() + ",";
                }

                CommonData.ViewReport = "SSCRM_REP_SALE_BY_ORDNO";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), strChkDemo.TrimEnd(','),dtpDocMonth.Value.ToString("MMMyyyy").ToUpper()  );
                objReportview.Show();

            }
            else if (FormID == 57)
            {
                CommonData.ViewReport = "REP_GC_WISE_PRODUCT_REC_SUMMARY";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), strEcodes, "SUM");
                objReportview.Show();
            }


            else if (FormID == 58)
            {
                CommonData.ViewReport = "REP_LOGICAL_BRANCH_WISE_GCGL_SALES_ACCOUNTABILITY";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "ALL", strLogicalBranches, "SUM-GC-ONLY");
                objReportview.Show();
            }

            else if (FormID == 59)
            {
                string strChkDemo = "";
                for (int i = 0; i < chkDemoType.CheckedItems.Count; i++)
                {
                    NewCheckboxListItem CL = (NewCheckboxListItem)chkDemoType.CheckedItems[i];
                    strChkDemo += "" + CL.Tag.ToString() + ",";
                }

                CommonData.ViewReport = "SSCRM_GL_WISE_DOC_SHEET";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), strChkDemo.TrimEnd(','), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(),"");
                objReportview.Show();
            }
            else if (FormID == 60)
            {
                string strChkDemo = "";
                for (int i = 0; i < chkDemoType.CheckedItems.Count; i++)
                {
                    NewCheckboxListItem CL = (NewCheckboxListItem)chkDemoType.CheckedItems[i];
                    strChkDemo += "" + CL.Tag.ToString() + ",";
                }
                var firstDayOfMonth = new DateTime(dtpDocMonth.Value.Year, dtpDocMonth.Value.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                CommonData.ViewReport = "SALESORDER_LOGWISE_ORDER_BY_ORDER_NO";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, strChkDemo.TrimEnd(','), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), firstDayOfMonth.ToString("dd/MMM/yyyy"), lastDayOfMonth.ToString("dd/MMM/yyyy"), "BRANCH-ORDNO");
                objReportview.Show();
            }

            else if (FormID == 61)
            {
                CommonData.ViewReport = "SSCRM_REP_HR_RECRUIT_VS_RESIGND";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode,CommonData.FinancialYear, dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(),"");
                objReportview.Show();
            }
            else if (FormID == 62)
            {
                CommonData.ViewReport = "SSERP_REP_SP_SALES_REGISTER_DETL_FORMAT2";
                crReportParams.FromDate = dtpDocMonth.Value.ToString("dd/MMM/yyyy");
                crReportParams.ToDate = dtpDocMonth.Value.ToString("dd/MMM/yyyy");
                ReportViewer objReportview = new ReportViewer("SPLITTING", CommonData.DocMonth);
                //ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "");
                objReportview.Show();
            }

            else
            {
                string strChkDemo = "";
                for (int i = 0; i < chkDemoType.CheckedItems.Count; i++)
                {
                    strChkDemo += "'" + chkDemoType.CheckedItems[i].ToString() + "',";
                }
                CommonData.ViewReport = "Doorknocks";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(),dtpDocMonth.Value.ToString("MMMyyyy"), cmleadType.Text, strChkDemo.TrimEnd(','));
                objReportview.Show();
            }
        }

        //private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbCompany.SelectedIndex > 0)
        //    {
        //        objHRInfo = new HRInfo();
        //        UtilityLibrary.PopulateControl(cmbBranch, objHRInfo.GetAllBranchList(cmbCompany.SelectedValue.ToString(), "", "").Tables[0].DefaultView, 1, 0, "-- Please Select --", 0);
        //        objHRInfo = null;
        //    }
        //}

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            GetSelectedLogicalBranches();

            #region FormID 2
            if (FormID == 2)
            {
                objExDb = new ExcelDB();
                DataTable dtExcel = objExDb.GetSalesOrderRegister(CommonData.CompanyCode, CommonData.BranchCode, dtpDocMonth.Value.ToString("MMMyyyy")).Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;

                        Excel.Range rg = worksheet.get_Range("A1", "M1");
                        Excel.Range rgData = worksheet.get_Range("A2", "M" + (dtExcel.Rows.Count + 1).ToString());
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
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "OrderNo";

                        rg = worksheet.get_Range("C1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "ARNo";

                        rg = worksheet.get_Range("D1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "OrderDate";

                        rg = worksheet.get_Range("E1", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "SRName";

                        rg = worksheet.get_Range("F1", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "Customer Name";

                        rg = worksheet.get_Range("G1", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Cells.Value2 = "Customer Address";

                        rg = worksheet.get_Range("H1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Customer MobileNo";

                        rg = worksheet.get_Range("I1", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Cells.Value2 = "Product Details";

                        rg = worksheet.get_Range("J1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Price";

                        rg = worksheet.get_Range("K1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Qty";

                        rg = worksheet.get_Range("L1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "OrderAmount";

                        rg = worksheet.get_Range("M1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "AdvanceAmount";


                        int RowCounter = 1;

                        foreach (DataRow dr in dtExcel.Rows)
                        {
                            int i = 1;
                            worksheet.Cells[RowCounter + 1, i++] = RowCounter;
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

            #region FormID 3
            if (FormID == 3)
            {
                objExDb = new ExcelDB();
                DataTable dtExcel = objExDb.GetSRWiseSalesBulletins(dtpDocMonth.Value.ToString("MMMyyyy"), "SR-S ONLY").Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;

                        Excel.Range rg = worksheet.get_Range("A1", "V1");
                        Excel.Range rgData = worksheet.get_Range("A2", "V" + (dtExcel.Rows.Count + 1).ToString());
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
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "E/A Code";

                        rg = worksheet.get_Range("C1", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Cells.Value2 = "Employee Name";

                        rg = worksheet.get_Range("D1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Desig";

                        rg = worksheet.get_Range("E1", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "Date of Joining";

                        rg = worksheet.get_Range("F1", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "Camp Name";

                        rg = worksheet.get_Range("G1", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Cells.Value2 = "GL/GC Name";

                        rg = worksheet.get_Range("H1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "PMD";

                        rg = worksheet.get_Range("I1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "D.A.Days";

                        rg = worksheet.get_Range("J1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Demos";

                        rg = worksheet.get_Range("K1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Units";

                        rg = worksheet.get_Range("L1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Points";

                        rg = worksheet.get_Range("M1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Cust.";

                        rg = worksheet.get_Range("N1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "%(in Camp Prd Pnts)";

                        rg = worksheet.get_Range("O1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "%(in Branch Prd Pnts)";

                        rg = worksheet.get_Range("P1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Revenue";
                        int RowCounter = 1;

                        foreach (DataRow dr in dtExcel.Rows)
                        {
                            int i = 1;
                            worksheet.Cells[RowCounter + 1, i++] = RowCounter;
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_eora_code"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_eora_name"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_eora_desg"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_doj"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_grp_name"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_grp_eora_name"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_pmd"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_dadays"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_demos"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_qty"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_prod_points"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_invoices"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["syp_grp_prodpoints_perc"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["syp_eora_prodpoints_perc"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_invamt"].ToString();
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

            #region FormID 4
            if (FormID == 4)
            {
                objExDb = new ExcelDB();
                DataTable dtExcel4 = objExDb.GetSRWiseSalesBulletins(dtpDocMonth.Value.ToString("MMMyyyy"), "GL-S ONLY").Tables[0];
                objExDb = null;
                if (dtExcel4.Rows.Count > 0)
                {
                    try
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;

                        Excel.Range rg = worksheet.get_Range("A1", "U2");
                        Excel.Range rgData = worksheet.get_Range("A3", "U" + (dtExcel4.Rows.Count + 1).ToString());
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;

                        rg.Font.Bold = true;
                        rg.Font.Name = "Times New Roman";
                        rg.Font.Size = 10;
                        rg.WrapText = true;
                        rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                        rg.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Interior.ColorIndex = 31;
                        rg.Borders.Weight = 2;
                        rg.Borders.LineStyle = Excel.Constants.xlSolid;
                        rg.Cells.RowHeight = 25;

                        rg = worksheet.get_Range("A1:A2", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Sl.No";

                        rg = worksheet.get_Range("B1:B2", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "E/A Code";

                        rg = worksheet.get_Range("C1:C2", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Cells.Value2 = "Name of the GC/GL";

                        rg = worksheet.get_Range("D1:D2", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Desig";

                        rg = worksheet.get_Range("E1:E2", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "Date of Joining";

                        rg = worksheet.get_Range("F1:F2", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "Expiereance";

                        rg = worksheet.get_Range("G1:G2", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Cells.Value2 = "Company Name";

                        rg = worksheet.get_Range("H1:N1", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = " PERSONAL ";

                        rg = worksheet.get_Range("H2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "PMD";

                        rg = worksheet.get_Range("I2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "D.A.Days";

                        rg = worksheet.get_Range("J2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Demos";

                        rg = worksheet.get_Range("K2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Units";

                        rg = worksheet.get_Range("L2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Points";

                        rg = worksheet.get_Range("M2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Cust.";

                        rg = worksheet.get_Range("N2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Revenue";

                        rg = worksheet.get_Range("O1:U1", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = " GROUP (including personal)";

                        rg = worksheet.get_Range("O2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "PMD";

                        rg = worksheet.get_Range("P2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "D.A.Days";

                        rg = worksheet.get_Range("Q2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Demos";

                        rg = worksheet.get_Range("R2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Units";

                        rg = worksheet.get_Range("S2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Points";

                        rg = worksheet.get_Range("T2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Cust.";

                        rg = worksheet.get_Range("U2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Revenue";



                        int RowCounter = 2;

                        foreach (DataRow dr in dtExcel4.Rows)
                        {
                            int i = 1;
                            worksheet.Cells[RowCounter + 1, i++] = RowCounter - 1;
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_eora_code"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_eora_name"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_eora_desg"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_doj"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["EXP_MONTHS"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_grp_name"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_pmd"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_dadays"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_demos"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_qty"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_prod_points"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_invoices"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sysbh_invamt"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["syp_grp_pmd"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["syp_grp_dadays"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["syp_grp_demos"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["syp_grp_qty"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["syp_grp_prodpoints"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["syp_grp_invoices"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["syp_grp_invamt"].ToString();
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

            #region FormID 7
            if (FormID == 7)
            {
                objDB = new SQLDB();
                string sqlText = "";
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                string strComp = "", strBranch = "";
                sqlText = "SELECT DISTINCT COMPANY_CODE FROM USER_BRANCH INNER JOIN BRANCH_MAS ON BRANCH_CODE = UB_BRANCH_CODE WHERE UB_USER_ID = '" + CommonData.LogUserId + "'";
                dt = objDB.ExecuteDataSet(sqlText).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (strComp != "")
                            strComp += ",";
                        strComp += dt.Rows[i]["COMPANY_CODE"].ToString();
                    }
                }
                else
                {
                    strComp += CommonData.CompanyCode.ToString();
                }
                sqlText = "SELECT UB_BRANCH_CODE FROM USER_BRANCH WHERE UB_USER_ID = '" + CommonData.LogUserId + "'";
                dt2 = objDB.ExecuteDataSet(sqlText).Tables[0];
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        if (strBranch != "")
                            strBranch += ",";
                        strBranch += dt2.Rows[i]["UB_BRANCH_CODE"].ToString();
                    }
                }
                else
                {
                    strBranch += CommonData.BranchCode.ToString();
                }
                objExDb = new ExcelDB();
                DataTable dtEx = new DataTable();
                dtEx = objExDb.GetBranchWiseBulletins(strComp, strBranch, dtpDocMonth.Value.ToString("MMMyyyy"), "").Tables[0];
                if (dtEx.Rows.Count > 0)
                {
                    try
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;

                        Excel.Range rg = worksheet.get_Range("A1", "AA1");
                        Excel.Range rgData = worksheet.get_Range("A2", "AA" + (dtEx.Rows.Count + 1).ToString());
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
                        rg.Cells.ColumnWidth = 30;
                        rg.Cells.Value2 = "Company";

                        rg = worksheet.get_Range("C1", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Cells.Value2 = "Branch";

                        rg = worksheet.get_Range("D1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Financial Year";

                        rg = worksheet.get_Range("E1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Document Month";

                        rgData = worksheet.get_Range("E2", "E" + (dtEx.Rows.Count + 1).ToString());
                        rgData.Cells.NumberFormat = "MMMyyyy";

                        rg = worksheet.get_Range("F1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Groups";

                        rg = worksheet.get_Range("G1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "SR's";

                        rg = worksheet.get_Range("H1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "PMD";

                        rg = worksheet.get_Range("I1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "DA Days";

                        rg = worksheet.get_Range("J1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Demos";

                        rg = worksheet.get_Range("K1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Invoice";

                        rg = worksheet.get_Range("L1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Qty";
                        rg = worksheet.get_Range("M1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Points";
                        rg = worksheet.get_Range("N1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Revenue";
                        rg = worksheet.get_Range("O1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Advance";
                        rg = worksheet.get_Range("P1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Avg Sales Price P/Point";
                        rg = worksheet.get_Range("Q1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Avg PMD P/Group";
                        rg = worksheet.get_Range("R1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Plants P/Group";
                        rg = worksheet.get_Range("S1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Growmin P/Group";
                        rg = worksheet.get_Range("T1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Focused Products";
                        rg = worksheet.get_Range("U1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Points P/Group";
                        rg = worksheet.get_Range("V1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Ratios D/C";
                        rg = worksheet.get_Range("W1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Ratios P/C";
                        rg = worksheet.get_Range("X1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "PPH Demos";
                        rg = worksheet.get_Range("Y1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "PPH Points";
                        rg = worksheet.get_Range("Z1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "PPH Customers";
                        rg = worksheet.get_Range("AA1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "PPH Revenue";
                        int RowCounter = 1;

                        foreach (DataRow dr in dtEx.Rows)
                        {
                            int i = 1;
                            worksheet.Cells[RowCounter + 1, i++] = RowCounter;
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_company_name"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_branch_name"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_fin_year"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_document_month"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_groups"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_total_srs"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_total_pmd"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_total_da_days"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_total_demos"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_invoice_count"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_qty"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_points"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_revenue_amt"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_advance_amt"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_avg_sales_price"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_avg_pmd_per_group"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_plants_per_group"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_gromin_per_group"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_fp_pg"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_points_per_group"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_ratios_dc"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_ratios_pc"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_pph_demos"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_pph_points"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_pph_customers"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["rs_pph_revenue"].ToString();
                            RowCounter++;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            #endregion

            #region FormID 9
            if (FormID == 9)
            {

                objExDb = new ExcelDB();
                DataTable dtEx = new DataTable();
                dtEx = objExDb.GetSalesRegisterWithoutCustDetails(CommonData.CompanyCode, CommonData.BranchCode, dtpDocMonth.Value.ToString("MMMyyyy"), "", "", "BRANCH-INVNO").Tables[0];
                if (dtEx.Rows.Count > 0)
                {
                    try
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;

                        Excel.Range rg = worksheet.get_Range("A1", "AA1");
                        Excel.Range rgData = worksheet.get_Range("A2", "AA" + (dtEx.Rows.Count + 1).ToString());
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


                        int RowCounter = 1;

                        foreach (DataRow dr in dtEx.Rows)
                        {
                            int i = 1;
                            worksheet.Cells[RowCounter + 1, i++] = RowCounter;

                            RowCounter++;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            #endregion

            #region FormID 2
            if (FormID == 30)
            {
                try
                {
                    objExDb = new ExcelDB();
                    DataTable dtExcel = objExDb.GetLoanRecoveryStatement(CommonData.CompanyCode, CommonData.BranchCode, dtpDocMonth.Value.ToString("MMMyyyy"),"").Tables[0];
                    objExDb = null;
                    if (dtExcel.Rows.Count > 0)
                    {
                        objUtilityDB = new UtilityDB();
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;
                        string sLastColumn = objUtilityDB.GetColumnName(12);
                        Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                        Excel.Range rgData = worksheet.get_Range("A3", sLastColumn + "3");
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;



                        rg.Font.Bold = true;
                        //rg.Font.Name = "Times New Roman";
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
                        rg = worksheet.get_Range("C3", "E3");
                        rg.Cells.ColumnWidth = 30;
                        rg = worksheet.get_Range("F3", Type.Missing);
                        rg.Cells.ColumnWidth = 7;
                        rg = worksheet.get_Range("G3", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("H3", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("I3", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg = worksheet.get_Range("I3", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg = worksheet.get_Range("J3", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("K3", Type.Missing);
                        rg.Cells.ColumnWidth = 13;
                        
                        

                        Excel.Range rgHead = null;
                        rgHead = worksheet.get_Range("A1", "L2");
                        rgHead.Merge(Type.Missing);
                        rgHead.Font.Size = 14;
                        rgHead.Font.ColorIndex = 1;
                        rgHead.Font.Bold = true;
                        rgHead.Cells.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgHead.Cells.VerticalAlignment = Excel.Constants.xlCenter;

                        rgHead.Cells.Value2 = "LOAN DEDUCTION STATEMENT FOR THE MONTH OF " + dtpDocMonth.Value.ToString("MMMyyyy").ToUpper() + "";

                        int iColumn = 1;
                        worksheet.Cells[3, iColumn++] = "SlNo";
                        worksheet.Cells[3, iColumn++] = "Ecode";
                        worksheet.Cells[3, iColumn++] = "Name";
                        worksheet.Cells[3, iColumn++] = "Department";
                        worksheet.Cells[3, iColumn++] = "Designation";
                        worksheet.Cells[3, iColumn++] = "Total Deduction";
                        worksheet.Cells[3, iColumn++] = "Pay Mode";                        
                        worksheet.Cells[3, iColumn++] = "Loan Type";
                        worksheet.Cells[3, iColumn++] = "Loan No";
                        worksheet.Cells[3, iColumn++] = "Deduction Amount";
                        worksheet.Cells[3, iColumn++] = "Status";
                        worksheet.Cells[3, iColumn++] = "Pay Mode";
                        
                        

                        int iRow = 4; iColumn = 1;
                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            int iMerg = 1;
                            if (i == 0)
                            {
                                iMerg = Convert.ToInt32(dtExcel.Rows[i]["ld_no_of_loans"]);
                                rgData = worksheet.get_Range("A" + iRow.ToString(), sLastColumn + (iRow + iMerg - 1).ToString());
                                rgData.Font.Size = 11;
                                rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                rgData.Borders.Weight = 2;
                                rgData = worksheet.get_Range("A" + iRow.ToString(), "A" + (iRow + iMerg - 1).ToString());
                                rgData.Merge(Type.Missing);
                                rgData = worksheet.get_Range("B" + iRow.ToString(), "B" + (iRow + iMerg - 1).ToString());
                                rgData.Merge(Type.Missing);
                                rgData = worksheet.get_Range("C" + iRow.ToString(), "C" + (iRow + iMerg - 1).ToString());
                                rgData.Merge(Type.Missing);
                                rgData = worksheet.get_Range("D" + iRow.ToString(), "D" + (iRow + iMerg - 1).ToString());
                                rgData.Merge(Type.Missing);
                                rgData = worksheet.get_Range("E" + iRow.ToString(), "E" + (iRow + iMerg - 1).ToString());
                                rgData.Merge(Type.Missing);
                                rgData = worksheet.get_Range("F" + iRow.ToString(), "F" + (iRow + iMerg - 1).ToString());
                                rgData.Merge(Type.Missing);
                                rgData = worksheet.get_Range("G" + iRow.ToString(), "G" + (iRow + iMerg - 1).ToString());
                                rgData.Merge(Type.Missing);

                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_sl_no"];
                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_eora_code"];
                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_emp_name"];
                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_desig_name"];
                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_dept_name"];
                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_total_deduction"];
                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_pay_mode"];
                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_loan_type"];
                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_loan_no"];
                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_loan_emi"];
                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_emi_status"];
                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_pay_mode"];

                            }
                            else
                            {
                                if (dtExcel.Rows[i]["ld_eora_code"].ToString() != dtExcel.Rows[i - 1]["ld_eora_code"].ToString())
                                {
                                    iMerg = Convert.ToInt32(dtExcel.Rows[i]["ld_no_of_loans"]);
                                    rgData = worksheet.get_Range("A" + iRow.ToString(), sLastColumn + (iRow + iMerg - 1).ToString());
                                    rgData.Font.Size = 11;
                                    rgData.VerticalAlignment = Excel.Constants.xlCenter;
                                    rgData.Borders.Weight = 2;
                                    rgData = worksheet.get_Range("A" + iRow.ToString(), "A" + (iRow + iMerg - 1).ToString());
                                    rgData.Merge(Type.Missing);
                                    rgData = worksheet.get_Range("B" + iRow.ToString(), "B" + (iRow + iMerg - 1).ToString());
                                    rgData.Merge(Type.Missing);
                                    rgData = worksheet.get_Range("C" + iRow.ToString(), "C" + (iRow + iMerg - 1).ToString());
                                    rgData.Merge(Type.Missing);
                                    rgData = worksheet.get_Range("D" + iRow.ToString(), "D" + (iRow + iMerg - 1).ToString());
                                    rgData.Merge(Type.Missing);
                                    rgData = worksheet.get_Range("E" + iRow.ToString(), "E" + (iRow + iMerg - 1).ToString());
                                    rgData.Merge(Type.Missing);
                                    rgData = worksheet.get_Range("F" + iRow.ToString(), "F" + (iRow + iMerg - 1).ToString());
                                    rgData.Merge(Type.Missing);
                                    rgData = worksheet.get_Range("G" + iRow.ToString(), "G" + (iRow + iMerg - 1).ToString());
                                    rgData.Merge(Type.Missing);

                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_sl_no"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_eora_code"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_emp_name"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_desig_name"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_dept_name"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_total_deduction"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_pay_mode"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_loan_type"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_loan_no"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_loan_emi"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_emi_status"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_pay_mode"];

                                }
                                else
                                {
                                    iColumn = 8;
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_loan_type"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_loan_no"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_loan_emi"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_emi_status"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["ld_pay_mode"];
                                }
                            }

                            iColumn = 1; iRow++;
                        }

                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            #endregion

            #region "FormID 45 :: SR Bulletins"

            if (FormID == 45)
            {


                DataTable dtExcel = new DataTable();
                objExDb = new ExcelDB();
                objUtilityDB = new UtilityDB();
                dtExcel = objExDb.Get_SRWiseTopToBottom(CommonData.CompanyCode, CommonData.BranchCode, dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), strLogicalBranches, "").Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        string strHead = "";
                        //Excel.Sheets worksheets = theWorkbook.Worksheets;
                        //var worksheet = (Excel.Worksheet)worksheets.Add(worksheets[ivar+1], Type.Missing, Type.Missing, Type.Missing);

                        //worksheet.Name = dtExcel.Rows[0]["sr_Log_Branch_Name"].ToString();                                                               

                        oXL.Visible = true;
                        int iTotColumns = 0;
                        iTotColumns = 18;
                        string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                        Excel.Range rgHead = null;
                        Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                        Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 4).ToString());
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;

                        rgData = worksheet.get_Range("A1", "R1");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 16;
                        rgData.Value2 = dtExcel.Rows[0]["sr_company_name"].ToString() + " \n SR WISE TOP TO BOTTOM";
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.HorizontalAlignment = Excel.Constants.xlCenter;

                        rgData.ColumnWidth = 20;
                        rgData.RowHeight = 40;
                        rgData.Font.ColorIndex = 11;
                        rgData = worksheet.get_Range("A2", "R2");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 14;

                        strHead = "  Name Of Branch Incharge : " + dtExcel.Rows[0]["sr_Bran_Head_Name"].ToString() +
                                  "\n Physical Branch : " + dtExcel.Rows[0]["sr_branch_name"].ToString() + "  \t     Doc Month : " + dtExcel.Rows[0]["sr_document_month"].ToString();
                        strHead = strHead.TrimEnd(',');
                        rgData.Value2 = strHead;
                        rgData.Font.ColorIndex = 30;
                        rgData.ColumnWidth = 200;
                        rgData.RowHeight = 40;

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

                        rg = worksheet.get_Range("A4:A3", Type.Missing);
                        rg.Merge(Type.Missing); rg.Cells.ColumnWidth = 4;
                        rg.Cells.Value2 = "Sl.No";
                        rg.Borders.Weight = 2;
                        rg.Font.Bold = true; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("B4:B3", Type.Missing);
                        rg.Merge(Type.Missing); rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "Logical Branch Name";
                        rg.Borders.Weight = 2;
                        rg.Font.Bold = true; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("C4:C3", Type.Missing);
                        rg.Merge(Type.Missing); rg.Cells.ColumnWidth = 8;
                        rg.Cells.Value2 = "Ecode";
                        rg.Borders.Weight = 2; rg.Font.Bold = true; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("D4:D3", Type.Missing);
                        rg.Merge(Type.Missing); rg.Cells.ColumnWidth = 35;
                        rg.Cells.Value2 = "Name Of SR / SE";
                        rg.Borders.Weight = 2; rg.Font.Bold = true; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("E4:E3", Type.Missing);
                        rg.Merge(Type.Missing); rg.Cells.ColumnWidth = 8;
                        rg.Cells.Value2 = "Desig.";
                        rg.Borders.Weight = 2; rg.Font.Bold = true; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("F4:F3", Type.Missing);
                        rg.Merge(Type.Missing); rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "DOJ";
                        rg.Borders.Weight = 2; rg.Font.Bold = true; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("G4:G3", Type.Missing);
                        rg.Merge(Type.Missing); rg.Cells.ColumnWidth = 7;
                        rg.Cells.Value2 = "PMD";
                        rg.Borders.Weight = 2; rg.Font.Bold = true; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("H4:H3", Type.Missing);
                        rg.Merge(Type.Missing); rg.Cells.ColumnWidth = 7;
                        rg.Cells.Value2 = "Demos";
                        rg.Borders.Weight = 2; rg.Font.Bold = true; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("I3", "L3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "SALES";

                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("I4", Type.Missing);
                        rg.Cells.ColumnWidth = 6;
                        rg.Cells.Value2 = "Gromin";
                        rg.Borders.Weight = 2;
                        rg = worksheet.get_Range("J4", Type.Missing);
                        rg.Cells.ColumnWidth = 6;
                        rg.Cells.Value2 = "Teak";
                        rg.Borders.Weight = 2;
                        rg = worksheet.get_Range("K4", Type.Missing);
                        rg.Cells.ColumnWidth = 6;
                        rg.Cells.Value2 = "Focuse";
                        rg.Borders.Weight = 2;
                        rg = worksheet.get_Range("L4", Type.Missing);
                        rg.Cells.ColumnWidth = 6;
                        rg.Cells.Value2 = "Total";
                        rg.Borders.Weight = 2;

                        rg = worksheet.get_Range("M3", "P3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "POINTS";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("M4", Type.Missing);
                        rg.Cells.ColumnWidth = 6;
                        rg.Cells.Value2 = "Gromin";
                        rg.Borders.Weight = 2;
                        rg = worksheet.get_Range("N4", Type.Missing);
                        rg.Cells.ColumnWidth = 6;
                        rg.Cells.ColumnWidth = 6;
                        rg.Cells.Value2 = "Teak";
                        rg = worksheet.get_Range("O4", Type.Missing);
                        rg.Cells.ColumnWidth = 6;
                        rg.Cells.ColumnWidth = 6;
                        rg.Cells.Value2 = "Focuse";
                        rg = worksheet.get_Range("P4", Type.Missing);
                        rg.Cells.ColumnWidth = 6;
                        rg.Cells.Value2 = "Total";
                        rg.Borders.Weight = 2;
                        rg = worksheet.get_Range("Q4:Q3", Type.Missing);
                        rg.Cells.ColumnWidth = 6;
                        rg.Merge(Type.Missing);
                        rg.Cells.Value2 = "Cust.";
                        rg.Borders.Weight = 2; rg.Font.Bold = true; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("R4:R3", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Merge(Type.Missing);
                        rg.Cells.Value2 = "Revenue";
                        rg.Borders.Weight = 2; rg.Font.Bold = true; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        int iColumn = 1, iStartRow = 5;
                        //worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                        //worksheet.Cells[iStartRow, iColumn++] = "Logical Branch Name";
                        //worksheet.Cells[iStartRow, iColumn++] = "Ecode";
                        //worksheet.Cells[iStartRow, iColumn++] = "Name Of SR/SE";
                        //worksheet.Cells[iStartRow, iColumn++] = "Desig.";
                        //worksheet.Cells[iStartRow, iColumn++] = "DOJ";
                        //worksheet.Cells[iStartRow, iColumn++] = "PMD";
                        //worksheet.Cells[iStartRow, iColumn++] = "Demos";
                        //worksheet.Cells[iStartRow, iColumn++] = "Gromin";
                        //worksheet.Cells[iStartRow, iColumn++] = "Teak";
                        //worksheet.Cells[iStartRow, iColumn++] = "Focuse";
                        //worksheet.Cells[iStartRow, iColumn++] = "Total";
                        //worksheet.Cells[iStartRow, iColumn++] = "Gromin";
                        //worksheet.Cells[iStartRow, iColumn++] = "Teak";
                        //worksheet.Cells[iStartRow, iColumn++] = "Focuse";
                        //worksheet.Cells[iStartRow, iColumn++] = "Total";
                        //worksheet.Cells[iStartRow, iColumn++] = "Cust.";
                        //worksheet.Cells[iStartRow, iColumn++] = "Revenue";


                        //iStartRow++; iColumn = 1;

                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            worksheet.Cells[iStartRow, iColumn++] = i + 1;
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Log_Branch_Name"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Ecode"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Emp_Name"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Emp_Desig"].ToString();
                            if (dtExcel.Rows[i]["sr_doj"].ToString() != "")
                                worksheet.Cells[iStartRow, iColumn++] = Convert.ToDateTime(dtExcel.Rows[i]["sr_doj"]).ToString("dd/MMM/yyyy");
                            else
                                worksheet.Cells[iStartRow, iColumn++] = "";
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_PMD"].ToString();

                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Demos"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Gromin_Sales"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Teak_Sales"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Focuse_Sales"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Total_Sales"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Gromin_Points"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Teak_Points"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Focuse_Points"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Total_Points"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_No_Of_Cust"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_Revenue"].ToString();

                            iStartRow++; iColumn = 1;
                        }

                        iStartRow = 7;
                        iColumn = iStartRow;
                        rgHead = worksheet.get_Range("G" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString(),
                                                "R" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString());
                        rg = worksheet.get_Range("A" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString(),
                                                "F" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString());
                        rg.Merge(Type.Missing);
                        rg.Value2 = " Branch Total";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 14;
                        rg.Font.ColorIndex = 30;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgHead.Borders.Weight = 2;
                        rgHead.Font.Size = 12; rgHead.Font.Bold = true;

                        for (int iMonths = 0; iMonths <= Convert.ToInt32(dtExcel.Rows.Count); iMonths++)
                        {
                            iStartRow = 7; iColumn = iStartRow;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;


                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }

            #endregion 
           
            #region FormID 46 :: ORGANIZATION CHART
            if (FormID == 46)
            {
                try
                {
                    objExDb = new ExcelDB();
                    objUtilityDB = new UtilityDB();
                    string strChkDemo = "";
                    for (int i = 0; i < chkDemoType.CheckedItems.Count; i++)
                    {
                        NewCheckboxListItem CL = (NewCheckboxListItem)chkDemoType.CheckedItems[i];
                        strChkDemo += "" + CL.Tag.ToString() + ",";
                    }
                    DataTable dtExcel = objExDb.GetSRWiseCumulativeBulletins2(CommonData.CompanyCode, CommonData.BranchCode, strChkDemo.TrimEnd(','), CommonData.FinancialYear, dtpDocMonth.Value.ToString("MMMyyyy"), "", "").Tables[0];
                    objExDb = null;


                    Excel.Application oXL = new Excel.Application();
                    Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                    //theWorkbook.Name = CommonData.BranchName + " SALES REGISTER " + CommonData.DocMonth.ToUpper();

                    Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;

                    //Excel.Range rgData = worksheet.get_Range("A5",  (dtExcel.Rows.Count + 4).ToString());
                    //rgData.Font.Size = 11;
                    //rgData.WrapText = true;
                    //rgData.VerticalAlignment = Excel.Constants.xlCenter;
                    //rgData.Borders.Weight = 2;

                    worksheet.Name = dtpDocMonth.Value.ToString("MMMyyyy").ToUpper();
                    oXL.Visible = true;
                    Excel.Range rgHead = worksheet.get_Range("A1", "K1");
                    rgHead.Font.Size = 14;
                    rgHead.Cells.MergeCells = true;
                    rgHead.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgHead.Font.Bold = true;
                    rgHead.Font.ColorIndex = 30;
                    rgHead.Borders.Weight = 2;

                    Excel.Range rgHead1 = worksheet.get_Range("A2", "K2");
                    rgHead1.Font.Size = 14;
                    rgHead1.Cells.MergeCells = true;
                    rgHead1.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead1.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgHead1.Font.Bold = true;
                    rgHead1.Font.ColorIndex = 30;
                    rgHead1.Borders.Weight = 2;


                    Excel.Range rg = worksheet.get_Range("A4", "K4");
                    rg.Font.Size = 14;
                    //rg.Cells.MergeCells = true;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 30;
                    rg.Borders.Weight = 2;

                    Excel.Range rgHead2 = worksheet.get_Range("A2", "K2");
                    rgHead2.Font.Size = 14;
                    rgHead2.Font.Bold = true;
                    rgHead2.Cells.MergeCells = true;
                    rgHead2.VerticalAlignment = Excel.Constants.xlCenter;

                    rgHead2.Font.Bold = true;
                    rgHead2.Font.ColorIndex = 30;
                    rgHead2.Borders.Weight = 2;


                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.Font.ColorIndex = 30;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.RowHeight = 38;

                    rgHead = worksheet.get_Range("A1", "K1");
                    rgHead.Cells.ColumnWidth = 5;

                    rgHead.Cells.Value2 = CommonData.CompanyName + "";
                    rgHead1 = worksheet.get_Range("A2", "K2");
                    rgHead1.Cells.ColumnWidth = 5;

                    rgHead1.Cells.Value2 = "ORGANIZATION CHART";
                    rgHead2 = worksheet.get_Range("A3", "K3");
                    rgHead2.Cells.ColumnWidth = 5;
                    rgHead2.Cells.MergeCells = true;

                    rgHead2.Cells.Value2 = "Physical Branch: " + dtExcel.Rows[0]["sr_branch_name"] + " \t                                                                                                                                                                                            Doc Month :  " + dtExcel.Rows[0]["sr_doc_month"] + "";
                    //rgHead2.HorizontalAlignment = Excel.Constants.xlRight;
                    rgHead2.Font.Bold = true;
                    rgHead2.Font.Size = 13;

                    rg = worksheet.get_Range("A4", Type.Missing);
                    rg.Cells.ColumnWidth = 3;
                    rg.Cells.Value2 = "Sl.No";

                    rg = worksheet.get_Range("B4", Type.Missing);
                    rg.Cells.ColumnWidth = 25;
                    rg.Cells.Value2 = "Logical Branch Name";

                    rg = worksheet.get_Range("C4", Type.Missing);
                    rg.Cells.ColumnWidth = 20;
                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                    {
                        if (dtExcel.Rows[i]["sr_rep_level6_desig"].ToString().Length > 0)
                        {
                            rg.Cells.Value2 = "" + dtExcel.Rows[i]["sr_rep_level6_desig"];
                            break;
                        }

                    }

                    rg = worksheet.get_Range("D4", Type.Missing);
                    rg.Cells.ColumnWidth = 20;
                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                    {
                        if (dtExcel.Rows[i]["sr_rep_level5_desig"].ToString().Length > 0)
                        {
                            rg.Cells.Value2 = "" + dtExcel.Rows[i]["sr_rep_level5_desig"];
                            break;
                        }

                    }


                    rg = worksheet.get_Range("E4", Type.Missing);
                    rg.Cells.ColumnWidth = 20;
                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                    {
                        if (dtExcel.Rows[i]["sr_rep_level4_desig"].ToString().Length > 0)
                        {
                            rg.Cells.Value2 = "" + dtExcel.Rows[i]["sr_rep_level4_desig"];
                            break;
                        }

                    }


                    rg = worksheet.get_Range("F4", Type.Missing);
                    rg.Cells.ColumnWidth = 20;
                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                    {
                        if (dtExcel.Rows[i]["sr_rep_level3_desig"].ToString().Length > 0)
                        {
                            rg.Cells.Value2 = "" + dtExcel.Rows[i]["sr_rep_level3_desig"];
                            break;
                        }

                    }


                    rg = worksheet.get_Range("G4", Type.Missing);
                    rg.Cells.ColumnWidth = 20;
                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                    {
                        if (dtExcel.Rows[i]["sr_rep_level2_desig"].ToString().Length > 0)
                        {
                            rg.Cells.Value2 = "" + dtExcel.Rows[i]["sr_rep_level2_desig"];
                            break;
                        }

                    }

                    rg = worksheet.get_Range("H4", Type.Missing);
                    rg.Cells.ColumnWidth = 20;
                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                    {
                        if (dtExcel.Rows[i]["sr_rep_level1_desig"].ToString().Length > 0)
                        {
                            rg.Cells.Value2 = "" + dtExcel.Rows[i]["sr_rep_level1_desig"];
                            break;
                        }

                    }


                    rg = worksheet.get_Range("I4", Type.Missing);
                    rg.Cells.ColumnWidth = 20;
                    rg.Cells.Value2 = "GC NAME";

                    //rgData = worksheet.get_Range("H4", "H" + (dtExcel.Rows.Count + 2).ToString());
                    //rgData.Cells.NumberFormat = "dd/MMM/yyyy";

                    rg = worksheet.get_Range("J4", Type.Missing);
                    rg.Cells.ColumnWidth = 30;
                    rg.Cells.Value2 = "SR NAMES";

                    rg = worksheet.get_Range("K4", Type.Missing);
                    rg.Cells.ColumnWidth = 20;
                    rg.Cells.Value2 = "CAMP";

                    Excel.Range rgData = worksheet.get_Range("A5", "K" + (Convert.ToInt32(dtExcel.Rows[0]["sr_gl_count"]) + 4).ToString());
                    rgData.Font.Size = 11;
                    rgData.WrapText = true;
                    rgData.VerticalAlignment = Excel.Constants.xlCenter;
                    rgData.Borders.Weight = 2;

                    int RowCounter = 5;
                    int ColCounter = 1;
                    int iData = 1;
                    string temp = dtExcel.Rows[0]["sr_rep_gc_code"].ToString();
                    string strSrName = dtExcel.Rows[0]["sr_eora_code"] + "-" + dtExcel.Rows[0]["sr_eora_name"] + "\n";
                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                    {
                        //worksheet.Cells[5, 1] = 5;

                        if ((i + 1) < dtExcel.Rows.Count)
                        {
                            if (temp == dtExcel.Rows[i + 1]["sr_rep_gc_code"].ToString())
                            {
                                strSrName += dtExcel.Rows[i + 1]["sr_eora_code"] + "-" + dtExcel.Rows[i + 1]["sr_eora_name"] + "\n";
                            }
                            else
                            {


                                worksheet.Cells[RowCounter, ColCounter++] = iData;
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_log_branch_name"].ToString();

                                if (dtExcel.Rows[i]["sr_rep_level6"].ToString().Length > 0)
                                {
                                    worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_rep_level6_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level6"] + "(" + dtExcel.Rows[i]["sr_rep_level6_GRP_COUNT"] + ")";
                                }
                                else
                                {
                                    worksheet.Cells[RowCounter, ColCounter++] = "";
                                }
                                if (dtExcel.Rows[i]["sr_rep_level5"].ToString().Length > 0)
                                {
                                    worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_rep_level5_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level5"] + "(" + dtExcel.Rows[i]["sr_rep_level5_GRP_COUNT"] + ")";
                                }
                                else
                                {
                                    worksheet.Cells[RowCounter, ColCounter++] = "";
                                }
                                if (dtExcel.Rows[i]["sr_rep_level4"].ToString().Length > 0)
                                {
                                    worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_rep_level4_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level4"] + "(" + dtExcel.Rows[i]["sr_rep_level4_GRP_COUNT"] + ")";
                                }
                                else
                                {
                                    worksheet.Cells[RowCounter, ColCounter++] = "";
                                }
                                if (dtExcel.Rows[i]["sr_rep_level3"].ToString().Length > 0)
                                {
                                    worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_rep_level3_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level3"] + "(" + dtExcel.Rows[i]["sr_rep_level3_GRP_COUNT"] + ")";
                                }
                                else
                                {
                                    worksheet.Cells[RowCounter, ColCounter++] = "";
                                }
                                if (dtExcel.Rows[i]["sr_rep_level2"].ToString().Length > 0)
                                {
                                    worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_rep_level2_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level2"] + "(" + dtExcel.Rows[i]["sr_rep_level2_GRP_COUNT"] + ")";
                                }
                                else
                                {
                                    worksheet.Cells[RowCounter, ColCounter++] = "";
                                }
                                if (dtExcel.Rows[i]["sr_rep_level1"].ToString().Length > 0)
                                {
                                    worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_rep_level1_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level1"] + "(" + dtExcel.Rows[i]["sr_rep_level1_GRP_COUNT"] + ")";
                                }
                                else
                                {
                                    worksheet.Cells[RowCounter, ColCounter++] = "";
                                }
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_rep_gc_code"] + "-" + dtExcel.Rows[i]["sr_rep_gc_gl"] + "(1+" + dtExcel.Rows[i]["sr_srs"] + ")";
                                //worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_eora_code"] + "-" + dtExcel.Rows[i]["sr_eora_name"];
                                worksheet.Cells[RowCounter, ColCounter++] = strSrName.TrimEnd();
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_group_name"];
                                strSrName = dtExcel.Rows[i + 1]["sr_eora_code"] + "-" + dtExcel.Rows[i + 1]["sr_eora_name"] + "\n";

                                temp = dtExcel.Rows[i + 1]["sr_rep_gc_code"].ToString();
                                iData++;
                                RowCounter++;
                                ColCounter = 1;
                            }
                        }
                        else
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = iData;
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_log_branch_name"].ToString();
                            if (dtExcel.Rows[i]["sr_rep_level6"].ToString().Length > 0)
                            {
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_rep_level6_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level6"] + "(" + dtExcel.Rows[i]["sr_rep_level6_GRP_COUNT"] + ")";
                            }
                            else
                            {
                                worksheet.Cells[RowCounter, ColCounter++] = "";
                            }
                            if (dtExcel.Rows[i]["sr_rep_level5"].ToString().Length > 0)
                            {
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_rep_level5_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level5"] + "(" + dtExcel.Rows[i]["sr_rep_level5_GRP_COUNT"] + ")";
                            }
                            else
                            {
                                worksheet.Cells[RowCounter, ColCounter++] = "";
                            }
                            if (dtExcel.Rows[i]["sr_rep_level4"].ToString().Length > 0)
                            {
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_rep_level4_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level4"] + "(" + dtExcel.Rows[i]["sr_rep_level4_GRP_COUNT"] + ")";
                            }
                            else
                            {
                                worksheet.Cells[RowCounter, ColCounter++] = "";
                            }
                            if (dtExcel.Rows[i]["sr_rep_level3"].ToString().Length > 0)
                            {
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_rep_level3_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level3"] + "(" + dtExcel.Rows[i]["sr_rep_level3_GRP_COUNT"] + ")";
                            }
                            else
                            {
                                worksheet.Cells[RowCounter, ColCounter++] = "";
                            }
                            if (dtExcel.Rows[i]["sr_rep_level2"].ToString().Length > 0)
                            {
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_rep_level2_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level2"] + "(" + dtExcel.Rows[i]["sr_rep_level2_GRP_COUNT"] + ")";
                            }
                            else
                            {
                                worksheet.Cells[RowCounter, ColCounter++] = "";
                            }
                            if (dtExcel.Rows[i]["sr_rep_level1"].ToString().Length > 0)
                            {
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_rep_level1_ecode"] + "-" + dtExcel.Rows[i]["sr_rep_level1"] + "(" + dtExcel.Rows[i]["sr_rep_level1_GRP_COUNT"] + ")";
                            }
                            else
                            {
                                worksheet.Cells[RowCounter, ColCounter++] = "";
                            }
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_rep_gc_code"] + "-" + dtExcel.Rows[i]["sr_rep_gc_gl"] + "(1+" + dtExcel.Rows[i]["sr_srs"] + ")";
                            worksheet.Cells[RowCounter, ColCounter++] = strSrName.TrimEnd();
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["sr_group_name"];
                        }


                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            #endregion

            #region FormID 47
            if (FormID == 47)
            {
                objExDb = new ExcelDB();

                DataTable dtExcel47 = objExDb.GetTransportCostSummary(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), strLogicalBranches, "", dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "").Tables[0];

                objExDb = null;
                if (dtExcel47.Rows.Count > 0)
                {
                    try
                    {
                        objUtilityDB = new UtilityDB();
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;
                        int iTotColumns = 0;
                        iTotColumns = 26;
                        string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                        Excel.Range rgHead = null;
                        Excel.Range rg = worksheet.get_Range("A5", sLastColumn + "5");
                        Excel.Range rgData = worksheet.get_Range("A6", sLastColumn + (dtExcel47.Rows.Count + 6).ToString());
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;

                        rgData = worksheet.get_Range("A1", "W2");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 16;
                        rgData.Value2 = "TRANSPORT COST SALES & REPLACEMENT ";
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

                        rg = worksheet.get_Range("A6", Type.Missing);
                        rg.Cells.ColumnWidth = 4;
                        rg = worksheet.get_Range("B6", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("C6", Type.Missing);
                        rg.Cells.ColumnWidth = 25;
                        rg = worksheet.get_Range("D6", Type.Missing);
                        rg.Cells.ColumnWidth = 25;
                        rg = worksheet.get_Range("E6", Type.Missing);
                        rg.Cells.ColumnWidth = 25;
                        rg = worksheet.get_Range("F6", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("G6", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("H6", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("I6", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("J6", Type.Missing);
                        rg.Cells.ColumnWidth = 10;


                        rg = worksheet.get_Range("K4", "L4");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "AS PER PU/TU TOTAL";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rg = worksheet.get_Range("K6", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("L6", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("M6", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("N6", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("O6", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("P6", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("Q6", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("R3", "W3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "TRANSPORTATION COST";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("R4", "T4");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "BRANCH BILL";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("R6", Type.Missing);
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("S6", Type.Missing);
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("T6", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        string DocMonth = dtExcel47.Rows[0]["tc_doc_month"].ToString();

                        rg = worksheet.get_Range("X1", "Z2");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "DOC MONTH :" + " " + DocMonth;
                        rg.Font.Bold = true; rg.Font.Size = 11;
                        rg = worksheet.get_Range("T1", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("U4", "W4");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "PU/SP BILL";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("S6", Type.Missing);
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("V6", Type.Missing);
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("W6", Type.Missing);
                        rg.Cells.ColumnWidth = 8;

                        rg = worksheet.get_Range("X4", "X5");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "TOTAL";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("X6", Type.Missing);
                        rg.Cells.ColumnWidth = 10;

                        rg = worksheet.get_Range("Y4", "Z4");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "COST";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("Y6", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("Z6", Type.Missing);
                        rg.Cells.ColumnWidth = 8;

                        rg.WrapText = true;

                        int iColumn = 1, iStartRow = 5;
                        worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                        worksheet.Cells[iStartRow, iColumn++] = "Logical Branch";
                        worksheet.Cells[iStartRow, iColumn++] = "Name Of The GL/GC/AO";
                        worksheet.Cells[iStartRow, iColumn++] = "Name Of The TM/ABM ";
                        worksheet.Cells[iStartRow, iColumn++] = "Name Of The DBM/BM";
                        worksheet.Cells[iStartRow, iColumn++] = "Camp Place";
                        worksheet.Cells[iStartRow, iColumn++] = "District";
                        worksheet.Cells[iStartRow, iColumn++] = "State";
                        worksheet.Cells[iStartRow, iColumn++] = "PU/SPD Dist To Camp";
                        worksheet.Cells[iStartRow, iColumn++] = "Purpose(sales/Replacement)";
                        worksheet.Cells[iStartRow, iColumn++] = "Des.Units";
                        worksheet.Cells[iStartRow, iColumn++] = "Return units";
                        worksheet.Cells[iStartRow, iColumn++] = "Net Received Units";
                        worksheet.Cells[iStartRow, iColumn++] = "%Of StockReturns";
                        worksheet.Cells[iStartRow, iColumn++] = "Doc.Units";
                        worksheet.Cells[iStartRow, iColumn++] = "Doc.Ponts";
                        worksheet.Cells[iStartRow, iColumn++] = "Doc.cust";
                        worksheet.Cells[iStartRow, iColumn++] = "Own Vehile";
                        worksheet.Cells[iStartRow, iColumn++] = "Hire Vehile";
                        worksheet.Cells[iStartRow, iColumn++] = "Total";
                        worksheet.Cells[iStartRow, iColumn++] = "Own Vehile";
                        worksheet.Cells[iStartRow, iColumn++] = "Hire Vehile";
                        worksheet.Cells[iStartRow, iColumn++] = "Total";
                        worksheet.Cells[iStartRow, iColumn++] = "TOTAL";
                        worksheet.Cells[iStartRow, iColumn++] = "P/Units";
                        worksheet.Cells[iStartRow, iColumn++] = "P/Points";


                        iStartRow++; iColumn = 1;
                        for (int i = 0; i < dtExcel47.Rows.Count; i++)
                        {

                            worksheet.Cells[iStartRow, iColumn++] = (i + 1).ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_log_branch_name"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_GC_GL_or_AO_Name"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_TM_ABM_Name"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_DBM_BM_name"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_camp_place"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_camp_dist"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_camp_state"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_dist_from_pu_or_sp"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_sales_or_replace"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_tot_received_units"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_tot_return_units"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_net_received_units"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_pers_stock_units"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_doc_units"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_doc_points"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_doc_customers"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_brn_own_vehicle_totl"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_brn_hire_vehicle_totl"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_brn_bill_totl"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_pu_or_sp_own_vehicle_totl"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_pu_or_sp_hire_vehicle_totl"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_pu_or_sp_bill_totl"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_brn_and_pu_or_sp_bill_totamt"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_pers_of_unit"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel47.Rows[i]["tc_pers_of_points"].ToString();
                            iStartRow++; iColumn = 1;

                        }

                    }


                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            #endregion  

            #region "FormID 48 :: Group Wise Stock Reconciliation"

            if (FormID == 48)
            {
                DataTable dtExcel = new DataTable();
                objExDb = new ExcelDB();
                objUtilityDB = new UtilityDB();
                dtExcel = objExDb.Get_GroupWiseStockReconciliation(CommonData.CompanyCode, CommonData.BranchCode, dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), 0, strLogicalBranches, "GROUP_WISE_SALES").Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {
                        string strHead = "";

                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;
                        int iTotColumns = 0;
                        iTotColumns = 26;
                        string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                        Excel.Range rgHead = null;
                        Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                        Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 4).ToString());
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;

                        rgData = worksheet.get_Range("A1", "Z1");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 16;
                        rgData.Value2 = dtExcel.Rows[0]["company_name"].ToString() + " \n GROUP WISE STOCK DISPATCHES, RETURN UNITS & % OF RETURNED UNITS";
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.HorizontalAlignment = Excel.Constants.xlCenter;

                        rgData.ColumnWidth = 20;
                        rgData.RowHeight = 40;
                        rgData.Font.ColorIndex = 11;
                        rgData = worksheet.get_Range("A2", "Z2");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 13;

                        strHead = " Name of Branch Incharge : " + dtExcel.Rows[0]["sysbh_branch_incharge"].ToString() +
                                  "\n  Physical Branch : " + dtExcel.Rows[0]["branch_name"].ToString() + "  \t    Doc Month : " + dtExcel.Rows[0]["sysbh_document_month"].ToString();
                        strHead = strHead.TrimEnd(',');
                        rgData.Value2 = strHead;
                        rgData.Font.ColorIndex = 9;
                        rgData.ColumnWidth = 200;
                        rgData.RowHeight = 35;

                        rgData = worksheet.get_Range("A3", "A3");
                        rgData.Merge(Type.Missing);
                        rgData.ColumnWidth = 80;
                        rgData.RowHeight = 30;
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
                        rg.Cells.RowHeight = 52;

                        rg = worksheet.get_Range("A4", "A3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Sl.No";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 3;
                        rg = worksheet.get_Range("B4", "B3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Name of the Employee";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 35;

                        rg = worksheet.get_Range("C4", "C3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Logical Branch Name";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 25;

                        rg = worksheet.get_Range("D4", "D3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Groups";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 7;
                        rg = worksheet.get_Range("E3", "F3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "OPENING STOCK";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("E4", Type.Missing);
                        rg.Value2 = "Stock At Camp";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("F4", Type.Missing);
                        rg.Value2 = "O/S Units(Inc Previous Month)";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("G3", "I3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "STOCK RECEIVED";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("G4", Type.Missing);
                        rg.Value2 = "From PU/SP";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("H4", Type.Missing);
                        rg.Value2 = "From With in the Team";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 9;
                        rg = worksheet.get_Range("I4", Type.Missing);
                        rg.Value2 = "From OutSide Team";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("J4", "J3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Total Stock";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("K3", "N3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "SALES & STOCK TRANSFERS";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("K4", Type.Missing);
                        rg.Value2 = "Doc. Units";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("L4", Type.Missing);
                        rg.Value2 = "Free Units";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("M4", Type.Missing);
                        rg.Value2 = "Short Units";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("N4", Type.Missing);
                        rg.Value2 = "Total";
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("O4", "O3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Closing Stock";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("P4", "P3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "% of Returned Units";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("Q4", "Q3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Stock Transfer to Other Camps";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("R4", "R3");
                        rg.Merge(Type.Missing);
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Value2 = "Return Units";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("S3", "V3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "NET CLOSING STOCK";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("S4", Type.Missing);
                        rg.Value2 = "Total";
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("T4", Type.Missing);
                        rg.Value2 = "Stock At Camp";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("U4", Type.Missing);
                        rg.WrapText = true;
                        rg.Value2 = "O/S Units Present Month";
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("V4", Type.Missing);
                        rg.WrapText = true;
                        rg.Value2 = "O/S Units Previous Months";
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("W3", "Y3");
                        rg.Merge(Type.Missing);
                        rg.WrapText = true;
                        rg.Value2 = "TOTAL RETURNED UNITS FOR CALCULATION OF FINES";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.WrapText = true;
                        rg = worksheet.get_Range("W4", Type.Missing);
                        rg.WrapText = true;
                        rg.Value2 = "Total Units";
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("X4", Type.Missing);
                        rg.Value2 = "Gromin";
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("Y4", Type.Missing);
                        rg.Value2 = "Others";
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("Z4", "Z3");
                        rg.Merge(Type.Missing);
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Value2 = "Fine Amount";
                        rg.Cells.ColumnWidth = 10;

                        int iColumn = 1, iStartRow = 5;


                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            worksheet.Cells[iStartRow, iColumn++] = i + 1;
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_grp_eora_name"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["Logical_Branch_Name"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_tot_groups"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_open_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_OS_units_with_prev_month"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_recd_qty_SP2BR"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_recd_qty_same_team"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_recd_qty_diff_team"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_TotStock_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_Doc_Units"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_free_units"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_short_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_tot_sale_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_clos_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_pers_of_return_units"].ToString() + '%';
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_stock_trnsf_to_other_camps"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_retu_qty_BR2SP"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_tot_clos_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_clos_stock_at_camp"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_clos_OS_units_pres_month"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_clos_OS_units_prev_month"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_retu_qty_BR2SP"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_gromin_pr_retu_units"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_other_pr_retu_units"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_fine_amt"].ToString();

                            iStartRow++; iColumn = 1;
                        }

                        iStartRow = 4;
                        iColumn = iStartRow;
                        rgHead = worksheet.get_Range("D" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString(),
                                                "Z" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString());

                        rg = worksheet.get_Range("A" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString(),
                                                "C" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString());
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Branch Total";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 14;
                        rg.Font.ColorIndex = 30;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rgHead.Borders.Weight = 2;
                        rgHead.Font.Size = 12; rgHead.Font.Bold = true;
                        double TotStock = 0, TotClosStock = 0, TotOSUnitsPrevMonth = 0;

                        for (int j = 0; j < Convert.ToInt32(dtExcel.Rows.Count); j++)
                        {
                            TotStock += Convert.ToDouble(dtExcel.Rows[j]["sysbh_TotStock_qty"]);
                            TotClosStock += Convert.ToDouble(dtExcel.Rows[j]["sysbh_clos_qty"]);
                            TotOSUnitsPrevMonth += Convert.ToDouble(dtExcel.Rows[j]["sysbh_clos_OS_units_prev_month"]);

                            iStartRow = 4; iColumn = iStartRow;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            if (TotClosStock != 0)
                            {
                                worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = (((TotClosStock - TotOSUnitsPrevMonth) / (TotStock - TotOSUnitsPrevMonth)) * 100).ToString("0.00") + '%';
                            }
                            iColumn = iColumn + 1;

                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }

            #endregion
            
            #region "FormID 49 :: TM & ABOVE SALES"

            if (FormID == 49)
            {
                DataTable dtExcel = new DataTable();
                objExDb = new ExcelDB();
                objUtilityDB = new UtilityDB();
                dtExcel = objExDb.Get_GroupWiseStockReconciliation(CommonData.CompanyCode, CommonData.BranchCode, dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), 0, "ALL", "TM_AND_ABOVE").Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {
                        string strHead = "";

                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;
                        int iTotColumns = 0;
                        iTotColumns = 25;
                        string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                        Excel.Range rgHead = null;
                        Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                        Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 4).ToString());
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;

                        rgData = worksheet.get_Range("A1", "Y1");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 16;
                        rgData.Value2 = dtExcel.Rows[0]["company_name"].ToString() + " \n TM & ABOVE STOCK DISPATCHES, RETURN UNITS & % OF RETURNED UNITS";
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.HorizontalAlignment = Excel.Constants.xlCenter;

                        rgData.ColumnWidth = 20;
                        rgData.RowHeight = 50;
                        rgData.Font.ColorIndex = 11;
                        rgData = worksheet.get_Range("A2", "Y2");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 13;

                        strHead = "Branch : " + dtExcel.Rows[0]["branch_name"].ToString() + "  \t  Doc Month : " + dtExcel.Rows[0]["sysbh_document_month"].ToString();
                        strHead = strHead.TrimEnd(',');
                        rgData.Value2 = strHead;
                        rgData.Font.ColorIndex = 9;
                        rgData.ColumnWidth = 200;
                        rgData.RowHeight = 20;

                        rgData = worksheet.get_Range("A3", "A3");
                        rgData.Merge(Type.Missing);
                        rgData.ColumnWidth = 80;
                        rgData.RowHeight = 30;
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
                        rg.Cells.RowHeight = 52;

                        rg = worksheet.get_Range("A4", "A3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Sl.No";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 3;
                        rg = worksheet.get_Range("B4", "B3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Name of the Employee";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 30;
                        rg = worksheet.get_Range("C4", "C3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Groups";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 7;
                        rg = worksheet.get_Range("D3", "E3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "OPENING STOCK";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("D4", Type.Missing);
                        rg.Value2 = "Stock At Camp";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("E4", Type.Missing);
                        rg.Value2 = "O/S Units(Inc Previous Month)";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("F3", "H3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "STOCK RECEIVED";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("F4", Type.Missing);
                        rg.Value2 = "From PU/SP";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("G4", Type.Missing);
                        rg.Value2 = "From With in the Team";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 9;
                        rg = worksheet.get_Range("H4", Type.Missing);
                        rg.Value2 = "From OutSide Team";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("I4", "I3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Total Stock";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("J3", "M3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "SALES & STOCK TRANSFERS";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("J4", Type.Missing);
                        rg.Value2 = "Doc. Units";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("K4", Type.Missing);
                        rg.Value2 = "Free Units";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("L4", Type.Missing);
                        rg.Value2 = "Short Units";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("M4", Type.Missing);
                        rg.Value2 = "Total";
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("N4", "N3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Closing Stock";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("O4", "O3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "% of Returned Units";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("P4", "P3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Stock Transfer to Other Camps";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("Q4", "Q3");
                        rg.Merge(Type.Missing);
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Value2 = "Return Units";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("R3", "U3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "NET CLOSING STOCK";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("R4", Type.Missing);
                        rg.Value2 = "Total";
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("S4", Type.Missing);
                        rg.Value2 = "Stock At Camp";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("T4", Type.Missing);
                        rg.WrapText = true;
                        rg.Value2 = "O/S Units Present Month";
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("U4", Type.Missing);
                        rg.WrapText = true;
                        rg.Value2 = "O/S Units Previous Months";
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("V3", "X3");
                        rg.Merge(Type.Missing);
                        rg.WrapText = true;
                        rg.Value2 = "TOTAL RETURNED UNITS FOR CALCULATION OF FINES";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.WrapText = true;
                        rg = worksheet.get_Range("V4", Type.Missing);
                        rg.WrapText = true;
                        rg.Value2 = "Total Units";
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("W4", Type.Missing);
                        rg.Value2 = "Gromin";
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("X4", Type.Missing);
                        rg.Value2 = "Others";
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("Y4", "Y3");
                        rg.Merge(Type.Missing);
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Value2 = "Fine Amount";
                        rg.Cells.ColumnWidth = 10;


                        int iColumn = 1, iStartRow = 5;


                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            worksheet.Cells[iStartRow, iColumn++] = i + 1;
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_Lvl1_Name"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_tot_groups"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_open_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_OS_units_with_prev_month"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_recd_qty_SP2BR"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_recd_qty_same_team"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_recd_qty_diff_team"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_TotStock_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_Doc_Units"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_free_units"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_short_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_tot_sale_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_clos_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_pers_of_return_units"].ToString() + '%';
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_stock_trnsf_to_other_camps"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_retu_qty_BR2SP"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_tot_clos_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_clos_stock_at_camp"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_clos_OS_units_pres_month"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_clos_OS_units_prev_month"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_retu_qty_BR2SP"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_gromin_pr_retu_units"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_other_pr_retu_units"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_fine_amt"].ToString();

                            iStartRow++; iColumn = 1;
                        }

                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }

            #endregion

            #region FormID==50 :: SSCRM_MIS_REP_GL_LOG_WISE_ORDERFORM_RECONSILATION
            if (FormID == 50)
            {
                try
                {
                    objExDb = new ExcelDB();
                    objUtilityDB = new UtilityDB();
                    string strChkDemo = "";
                    for (int i = 0; i < chkDemoType.CheckedItems.Count; i++)
                    {
                        NewCheckboxListItem CL = (NewCheckboxListItem)chkDemoType.CheckedItems[i];
                        strChkDemo += "" + CL.Tag.ToString() + ",";
                    }
                    DataTable dtExcel = objExDb.GetBranchOrderSheetReconsilation2(CommonData.CompanyCode, CommonData.BranchCode, strChkDemo.TrimEnd(','), CommonData.FinancialYear, dtpDocMonth.Value.ToString("MMMyyyy"), "GL_ORDER_RECON_SUMMERY").Tables[0];
                    objExDb = null;


                    Excel.Application oXL = new Excel.Application();
                    Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                    Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                    oXL.Visible = true;

                    Excel.Range rg = worksheet.get_Range("A4", "M4");
                    Excel.Range rgData = worksheet.get_Range("A6", "M" + (dtExcel.Rows.Count + 6).ToString());
                    rgData.Font.Size = 11;
                    rgData.WrapText = true;
                    rgData.VerticalAlignment = Excel.Constants.xlCenter;
                    rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgData.Borders.Weight = 2;


                    Excel.Range rgHead = worksheet.get_Range("A1", "M1");
                    rgHead.Cells.ColumnWidth = 5;
                    rgHead.Cells.MergeCells = true;
                    rgHead.Cells.Value2 = CommonData.CompanyName + "";
                    rgHead.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgHead.Font.ColorIndex = 30;
                    rgHead.Font.Bold = true;
                    rgHead.Font.Size = 13;
                    rgHead.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgHead.Borders.Weight = 2;

                    Excel.Range rgHead1 = worksheet.get_Range("A2", "M2");
                    rgHead1.Cells.ColumnWidth = 5;
                    rgHead1.Cells.MergeCells = true;
                    rgHead1.Cells.Value2 = "GL STATEMENT OF ORDER FORM RECONCILATON";
                    rgHead1.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgHead1.Font.Bold = true;
                    rgHead1.Font.Size = 13;
                    rgHead1.Font.ColorIndex = 30; rgHead1.Borders.Weight = 2;

                    Excel.Range rgHead2 = worksheet.get_Range("A3", "M3");
                    rgHead2.Cells.ColumnWidth = 5;
                    rgHead2.Cells.MergeCells = true;
                    rgHead2.Cells.Value2 = "Physical Branch:" + dtExcel.Rows[0]["BRANCH_NAME"] + "               Month:" + dtExcel.Rows[0]["DOCU_MONTH"] + "";

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
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.RowHeight = 25;

                    rg = worksheet.get_Range("A4:A5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 5;
                    rg.Borders.Weight = 2;
                    rg.Cells.Value2 = "Sl.No";

                    rg = worksheet.get_Range("B4:B5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 30;
                    rg.Borders.Weight = 2;
                    rg.Cells.Value2 = "Name Of The GL";

                    rg = worksheet.get_Range("C4:C5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 30;
                    rg.Borders.Weight = 2;
                    rg.Cells.Value2 = "Logical Branch";

                    rg = worksheet.get_Range("D4:D5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 15;
                    rg.Cells.Value2 = "Total Invoices Recieved";

                    rg = worksheet.get_Range("E4:I4", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 10;
                    rg.Cells.Value2 = "Details of Invoices";

                    rg = worksheet.get_Range("E5:E5", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 12;
                    rg.Cells.Value2 = "Documented";

                    rg = worksheet.get_Range("F5:F5", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 12;
                    rg.Cells.Value2 = "Only Adv.";

                    rg = worksheet.get_Range("G5:G5", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 12;
                    rg.Cells.Value2 = "Blank";

                    rg = worksheet.get_Range("H5:H5", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 12;
                    rg.Cells.Value2 = "Cancelled";

                    rg = worksheet.get_Range("I5:I5", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 12;
                    rg.Cells.Value2 = "Missed";

                    rg = worksheet.get_Range("J4:J5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 10;
                    rg.Cells.Value2 = "Total";

                    rg = worksheet.get_Range("K4:K5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 15;
                    rg.Cells.Value2 = "% of Cancelled Order Forms";

                    rg = worksheet.get_Range("L4:L5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 20;
                    rg.Cells.Value2 = "Missing Order Form Numbers";

                    rg = worksheet.get_Range("M4:M5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 10;
                    rg.Cells.Value2 = "Fine Amount";

                    int RowCounter = 6, ColCounter = 1, iData = 1;

                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                    {
                        worksheet.Cells[RowCounter, ColCounter++] = iData;
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["GLMEMBERNAME"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["LOG_BRANCH_NAME"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["TOTAL_ORDER_FORMS"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["TOTAL_DOCUMENTED"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["TOTAL_ADVANCE_CLCT"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["TOTAL_BLANK_SHEET"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["TOTAL_CANCELLED_ORDER"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["TOTAL_MISPLACED_SHEET"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["TOTAL"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["CANCEL_PER"] + "%";
                        string STR = "" + dtExcel.Rows[i]["MISPLACED_ORDER_SHEETS"].ToString();
                        worksheet.Cells[RowCounter, ColCounter++] = STR + "-";
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["FINE_AMOUNT"];
                        RowCounter++;
                        iData++;
                        ColCounter = 1;
                    }
                    Excel.Range rgHead3 = worksheet.get_Range("B" + (dtExcel.Rows.Count + 6) + ":B" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead3.Cells.ColumnWidth = 25;
                    rgHead3.Font.Bold = true;
                    rgHead3.Cells.Value2 = "Total";
                    rgHead3.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead3.Font.Size = 12;

                    Excel.Range rgHead4 = worksheet.get_Range("D" + (dtExcel.Rows.Count + 6) + ":D" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead4.Cells.ColumnWidth = 10;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(D6:D" + (dtExcel.Rows.Count+5) + ")";
                    rgHead4.Font.Size = 12;

                    rgHead4 = worksheet.get_Range("E" + (dtExcel.Rows.Count + 6) + ":E" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(E6:E" + (dtExcel.Rows.Count + 5) + ")";
                    rgHead4.Font.Size = 12;

                    rgHead4 = worksheet.get_Range("F" + (dtExcel.Rows.Count + 6) + ":F" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(F6:F" + (dtExcel.Rows.Count + 5) + ")";
                    rgHead4.Font.Size = 12;

                    rgHead4 = worksheet.get_Range("G" + (dtExcel.Rows.Count + 6) + ":G" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(G6:G" + (dtExcel.Rows.Count + 5) + ")";
                    rgHead4.Font.Size = 12;

                    rgHead4 = worksheet.get_Range("H" + (dtExcel.Rows.Count + 6) + ":H" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(H6:H" + (dtExcel.Rows.Count + 5) + ")";
                    rgHead4.Font.Size = 12;

                    rgHead4 = worksheet.get_Range("I" + (dtExcel.Rows.Count + 6) + ":I" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(I6:I" + (dtExcel.Rows.Count + 5) + ")";
                    rgHead4.Font.Size = 12;

                    rgHead4 = worksheet.get_Range("J" + (dtExcel.Rows.Count + 6) + ":J" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(J6:J" + (dtExcel.Rows.Count + 5) + ")";
                    rgHead4.Font.Size = 12;

                    rgHead4 = worksheet.get_Range("K" + (dtExcel.Rows.Count + 6) + ":K" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=(H"+(dtExcel.Rows.Count+6)+"/E" +(dtExcel.Rows.Count+6)+")*100";
                    rgHead4.Font.Size = 12;

                    rgHead4 = worksheet.get_Range("M" + (dtExcel.Rows.Count + 6) + ":M" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=SUM(M6:M" + (dtExcel.Rows.Count + 5) + ")";
                    rgHead4.Font.Size = 12;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            #endregion


            #region FormID==51 :: SSCRM_MIS_REP_TM_ORDERFRM_RECONSILATION
            if (FormID == 51)
            {
                try
                {
                    objExDb = new ExcelDB();
                    objUtilityDB = new UtilityDB();
                    string strChkDemo = "";
                    for (int i = 0; i < chkDemoType.CheckedItems.Count; i++)
                    {
                        NewCheckboxListItem CL = (NewCheckboxListItem)chkDemoType.CheckedItems[i];
                        strChkDemo += "" + CL.Tag.ToString() + ",";
                    }
                    DataTable dtExcel = objExDb.GetBranchOrderSheetReconsilation2(CommonData.CompanyCode, CommonData.BranchCode, "", CommonData.FinancialYear, dtpDocMonth.Value.ToString("MMMyyyy"), "TM_ORDER_RECON_SUMMERY").Tables[0];
                    objExDb = null;


                    Excel.Application oXL = new Excel.Application();
                    Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                    Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                    oXL.Visible = true;

                    Excel.Range rg = worksheet.get_Range("A4", "L4");
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
                    rgHead.Font.ColorIndex = 30;
                    rgHead.Font.Bold = true;
                    rgHead.Font.Size = 13;
                    rgHead.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgHead.Borders.Weight = 2;

                    Excel.Range rgHead1 = worksheet.get_Range("A2", "L2");
                    rgHead1.Cells.ColumnWidth = 5;
                    rgHead1.Cells.MergeCells = true;
                    rgHead1.Cells.Value2 = "STATEMENT OF ORDER FORM RECONCILATON TM & ABOVE";
                    rgHead1.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgHead1.Font.Bold = true;
                    rgHead1.Font.Size = 13;
                    rgHead1.Font.ColorIndex = 30; rgHead1.Borders.Weight = 2;

                    Excel.Range rgHead2 = worksheet.get_Range("A3", "L3");
                    rgHead2.Cells.ColumnWidth = 5;
                    rgHead2.Cells.MergeCells = true;
                    rgHead2.Cells.Value2 = "Physical Branch:" + dtExcel.Rows[0]["BRANCH_NAME"] + "                                                                                                              Month:" + dtExcel.Rows[0]["DOCU_MONTH"] + "";
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
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.RowHeight = 25;

                    rg = worksheet.get_Range("A4:A5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 5;
                    rg.Borders.Weight = 2;
                    rg.Cells.Value2 = "Sl.No";

                    rg = worksheet.get_Range("B4:B5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 30;
                    rg.Borders.Weight = 2;
                    rg.Cells.Value2 = "Name Of The TM/ABM";

                    rg = worksheet.get_Range("C4:C5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 15;
                    rg.Cells.Value2 = "Total Invoices Recieved";

                    rg = worksheet.get_Range("D4:H4", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 10;
                    rg.Cells.Value2 = "Details of Invoices";

                    rg = worksheet.get_Range("D5:D5", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 12;
                    rg.Cells.Value2 = "Documented";

                    rg = worksheet.get_Range("E5:E5", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 12;
                    rg.Cells.Value2 = "Only Adv.";

                    rg = worksheet.get_Range("F5:F5", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 12;
                    rg.Cells.Value2 = "Blank";

                    rg = worksheet.get_Range("G5:G5", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 12;
                    rg.Cells.Value2 = "Cancelled";

                    rg = worksheet.get_Range("H5:H5", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 12;
                    rg.Cells.Value2 = "Missed";

                    rg = worksheet.get_Range("I4:I5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 10;
                    rg.Cells.Value2 = "Total";

                    rg = worksheet.get_Range("J4:J5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 15;
                    rg.Cells.Value2 = "% of Cancelled Order Forms";

                    rg = worksheet.get_Range("K4:K5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 20;
                    rg.Cells.Value2 = "Missing Order Form Numbers";

                    rg = worksheet.get_Range("L4:L5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 10;
                    rg.Cells.Value2 = "Fine Amount";

                    int RowCounter = 6, ColCounter = 1, iData = 1;

                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                    {
                        worksheet.Cells[RowCounter, ColCounter++] = iData;
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["TMMEMBERNAME"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["TOTAL_ORDER_FORMS"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["TOTAL_DOCUMENTED"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["TOTAL_ADVANCE_CLCT"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["TOTAL_BLANK_SHEET"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["TOTAL_CANCELLED_ORDER"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["TOTAL_MISPLACED_SHEET"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["TOTAL"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["CANCEL_PER"] + "%";
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["MISPLACED_ORDER_SHEETS"] + "-";
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["FINE_AMOUNT"];
                        RowCounter++;
                        iData++;
                        ColCounter = 1;
                    }

                    Excel.Range rgHead3 = worksheet.get_Range("B" + (dtExcel.Rows.Count + 6) + ":B" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead3.Cells.ColumnWidth = 25;
                    rgHead3.Font.Bold = true;
                    rgHead3.Cells.Value2 = "Total";
                    rgHead3.Font.Size = 12;
                    rgHead3.VerticalAlignment = Excel.Constants.xlCenter;


                    Excel.Range rgHead4 = worksheet.get_Range("C" + (dtExcel.Rows.Count + 6) + ":C" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead4.Cells.ColumnWidth = 10;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Font.Size = 12;
                    rgHead4.Formula = "=Sum(C6:C" + dtExcel.Rows.Count + ")";

                    rgHead4 = worksheet.get_Range("D" + (dtExcel.Rows.Count + 6) + ":D" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(D6:D" + dtExcel.Rows.Count + ")";
                    rgHead4.Font.Size = 12;

                    rgHead4 = worksheet.get_Range("E" + (dtExcel.Rows.Count + 6) + ":E" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(E6:E" + dtExcel.Rows.Count + ")";
                    rgHead4.Font.Size = 12;

                    rgHead4 = worksheet.get_Range("F" + (dtExcel.Rows.Count + 6) + ":F" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(F6:F" + dtExcel.Rows.Count + ")";
                    rgHead4.Font.Size = 12;

                    rgHead4 = worksheet.get_Range("G" + (dtExcel.Rows.Count + 6) + ":G" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(G6:G" + dtExcel.Rows.Count + ")";
                    rgHead4.Font.Size = 12;

                    rgHead4 = worksheet.get_Range("H" + (dtExcel.Rows.Count + 6) + ":H" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(H6:H" + dtExcel.Rows.Count + ")";
                    rgHead4.Font.Size = 12;

                    rgHead4 = worksheet.get_Range("I" + (dtExcel.Rows.Count + 6) + ":I" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(I6:I" + dtExcel.Rows.Count + ")";
                    rgHead4.Font.Size = 12;

                    rgHead4 = worksheet.get_Range("J" + (dtExcel.Rows.Count + 6) + ":J" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=AVERAGE(J6:J" + dtExcel.Rows.Count + ")";
                    rgHead4.Font.Size = 12;

                    rgHead4 = worksheet.get_Range("L" + (dtExcel.Rows.Count + 6) + ":L" + (dtExcel.Rows.Count + 6), Type.Missing);
                    rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=SUM(L6:L" + dtExcel.Rows.Count + ")";
                    rgHead4.Font.Size = 12;


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            #endregion

            #region FormID==52 :: SSCRM_MIS_GL_WISE_BRANCH_PERFORMANCE_STMENT
            if (FormID == 52)
            {
                try
                {
                    objExDb = new ExcelDB();
                    objUtilityDB = new UtilityDB();
                    string strChkDemo = "";
                    for (int i = 0; i < chkDemoType.CheckedItems.Count; i++)
                    {
                        NewCheckboxListItem CL = (NewCheckboxListItem)chkDemoType.CheckedItems[i];
                        strChkDemo += "" + CL.Tag.ToString() + ",";
                    }
                    DataTable dtExcel = objExDb.GetGlWiseBranchPerfStatment(CommonData.CompanyCode, CommonData.BranchCode, strChkDemo.TrimEnd(','), dtpDocMonth.Value.ToString("MMMyyyy"), "GL_PRODUCTWISE_STATEMENT").Tables[0];
                    objExDb = null;


                    Excel.Application oXL = new Excel.Application();
                    Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                    Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                    oXL.Visible = true;

                    Excel.Range rg = worksheet.get_Range("A4", "AB4");
                    Excel.Range rgData = worksheet.get_Range("A7", "AB" + (dtExcel.Rows.Count + 7).ToString());
                    rgData.Font.Size = 11;
                    rgData.WrapText = true;
                    rgData.VerticalAlignment = Excel.Constants.xlCenter;
                    //rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgData.Borders.Weight = 2;


                    Excel.Range rgHead = worksheet.get_Range("A1", "AB1");
                    rgHead.Cells.ColumnWidth = 5;
                    rgHead.Cells.MergeCells = true;
                    rgHead.Cells.Value2 = CommonData.CompanyName + "";
                    rgHead.Font.ColorIndex = 30;
                    rgHead.Font.Bold = true;
                    rgHead.Font.Size = 13;
                    rgHead.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgHead.Borders.Weight = 2;

                    Excel.Range rgHead1 = worksheet.get_Range("A2", "AB2");
                    rgHead1.Cells.ColumnWidth = 5;
                    rgHead1.Cells.MergeCells = true;
                    rgHead1.Cells.Value2 = "BRANCH PERFORMANCE STATEMENT";
                    rgHead1.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgHead1.Font.Bold = true;
                    rgHead1.Font.Size = 13;
                    rgHead1.Font.ColorIndex = 30; rgHead1.Borders.Weight = 2;

                    Excel.Range rgHead2 = worksheet.get_Range("A3", "AB3");
                    rgHead2.Cells.ColumnWidth = 5;
                    rgHead2.Cells.MergeCells = true;
                    rgHead2.Cells.Value2 = "Physical Branch:" + dtExcel.Rows[0]["BRANCH_NAME"] +
                                                            "                                      DocMonth" + dtExcel.Rows[0]["DOCU_MONTH"];
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
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.RowHeight = 25;

                    rg = worksheet.get_Range("A4:A6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 5;
                    rg.Borders.Weight = 2;
                    rg.Cells.Value2 = "Sl.No";

                    rg = worksheet.get_Range("B4:B6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 7;
                    rg.Borders.Weight = 2;
                    rg.Cells.Value2 = "ECODE";

                    rg = worksheet.get_Range("C4:C6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 10;
                    rg.Cells.Value2 = "Name Of The GC/GL";

                    rg = worksheet.get_Range("D4:D6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 10;
                    rg.Cells.Value2 = "Logical Branch";

                    rg = worksheet.get_Range("E4:E6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 3;
                    rg.Orientation = 90;
                    rg.Cells.Value2 = "Desig";

                    rg = worksheet.get_Range("F4:F6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 3;
                    rg.Orientation = 90;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.Cells.Value2 = "No of SRs";


                    rg = worksheet.get_Range("G4:S4", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 10;
                    rg.Cells.Value2 = "TOTAL";

                    rg = worksheet.get_Range("G5:G6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 5;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlRight;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "PMD";

                    rg = worksheet.get_Range("H5:H6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 5;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "Avg PMD";

                    rg = worksheet.get_Range("I5:I6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 5;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "Demos";

                    rg = worksheet.get_Range("J5:M5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 30;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "Sales/Units";

                    rg = worksheet.get_Range("J6:J6", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 5;
                    rg.Orientation = 90;
                    rg.Cells.Value2 = "Gromin";

                    rg = worksheet.get_Range("K6:K6", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 5;
                    rg.Orientation = 90;
                    rg.Cells.Value2 = "Teak";

                    rg = worksheet.get_Range("L6:L6", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 5;
                    rg.Orientation = 90;
                    rg.Cells.Value2 = "Focused";

                    rg = worksheet.get_Range("M6:M6", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 5;
                    rg.Orientation = 90;
                    rg.Cells.Value2 = "Total";


                    rg = worksheet.get_Range("N5:Q5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 30;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "Points";


                    rg = worksheet.get_Range("N6:N6", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 5;
                    rg.Orientation = 90;
                    rg.Cells.Value2 = "Gromin";

                    rg = worksheet.get_Range("O6:O6", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 5;
                    rg.Orientation = 90;
                    rg.Cells.Value2 = "Teak";

                    rg = worksheet.get_Range("P6:P6", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 5;
                    rg.Orientation = 90;
                    rg.Cells.Value2 = "Focused";

                    rg = worksheet.get_Range("Q6:Q6", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 5;
                    rg.Orientation = 90;
                    rg.Cells.Value2 = "Total";

                    rg = worksheet.get_Range("R5:R6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 5;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "Cust";

                    rg = worksheet.get_Range("S5:S6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 8;
                    rg.Orientation = 45;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "Revenue";

                    rg = worksheet.get_Range("T4:V4", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 10;
                    rg.Cells.Value2 = "RATIOS";


                    rg = worksheet.get_Range("T5:T6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 5;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "S/C";

                    rg = worksheet.get_Range("U5:U6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 5;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "D/C";

                    rg = worksheet.get_Range("V5:V6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 5;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "D/S";

                    rg = worksheet.get_Range("W4:AA4", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 10;
                    rg.Cells.Value2 = "Productivity Per Head";

                    rg = worksheet.get_Range("W5:W6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 8;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "Demos";

                    rg = worksheet.get_Range("X5:X6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 5;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "Cust";

                    rg = worksheet.get_Range("Y5:Y6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 5;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "Sales";

                    rg = worksheet.get_Range("Z5:Z6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 8;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "Bags/Points";

                    rg = worksheet.get_Range("AA5:AA6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 8;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "Revenue";

                    rg = worksheet.get_Range("AB4:AB6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 15;
                    rg.Cells.Value2 = "Name of the TM/ABM";



                    int RowCounter = 7, ColCounter = 1, iData = 1;

                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                    {
                        worksheet.Cells[RowCounter, ColCounter++] = iData;
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["GL_ECODE"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["GL_NAME"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["LOG_BRANCH_NAME"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gl_desig"];

                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["no_srs"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["pmd"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["avgpmd"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["demos"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gromin_qty"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["teak_qty"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["focused_qty"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["total_qty"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gromin_points"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["teak_points"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["focused_points"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["total_points"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["cust"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["revenue"];
                        if (dtExcel.Rows[i]["ratios_S_BY_C"].ToString().Length == 0)
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = 0;
                        }
                        else
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["ratios_S_BY_C"];
                        }
                        if (dtExcel.Rows[i]["ratios_D_BY_C"].ToString().Length == 0)
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = 0;
                        }
                        else
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["ratios_D_BY_C"];
                        }
                        if (dtExcel.Rows[i]["ratios_D_BY_S"].ToString().Length == 0)
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = 0;
                        }
                        else
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["ratios_D_BY_S"];
                        }
                        if (dtExcel.Rows[i]["HEAD_DEMOS"].ToString().Length == 0)
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = 0;
                        }
                        else
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["HEAD_DEMOS"];
                        }
                        if (dtExcel.Rows[i]["HEAD_CUST"].ToString().Length == 0)
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = 0;
                        }
                        else
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["HEAD_CUST"];
                        }
                        if (dtExcel.Rows[i]["HEAD_SALES"].ToString().Length == 0)
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = 0;
                        }
                        else
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["HEAD_SALES"];
                        }
                        if (dtExcel.Rows[i]["HEAD_BAG_PER_POINTS"].ToString().Length == 0)
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = 0;
                        }
                        else
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["HEAD_BAG_PER_POINTS"];
                        }
                        if (dtExcel.Rows[i]["HEAD_REVENUE"].ToString().Length == 0)
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = 0;
                        }
                        else
                        {
                            worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["HEAD_REVENUE"];
                        }
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["tm_name"];
                        RowCounter++;
                        iData++;
                        ColCounter = 1;
                    }

                    Excel.Range rgHead3 = worksheet.get_Range("C" + (dtExcel.Rows.Count + 7) + ":C" + (dtExcel.Rows.Count + 7), Type.Missing);
                    rgHead3.Cells.ColumnWidth = 25;
                    rgHead3.Font.Bold = true;
                    rgHead3.Cells.Value2 = "Total";
                    rgHead3.Font.Size = 10;
                    rgHead3.VerticalAlignment = Excel.Constants.xlCenter;


                    //Excel.Range rgHead4 = worksheet.get_Range("E" + (dtExcel.Rows.Count + 7) + ":E" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 3;
                    //rgHead4.Font.Bold = true;
                    //rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    //rgHead4.Font.Size = 10;
                    //rgHead4.Formula = "=Sum(E6:E" + dtExcel.Rows.Count + ")";

                    Excel.Range rgHead4 = worksheet.get_Range("F" + (dtExcel.Rows.Count + 7) + ":F" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(F7:F" + (dtExcel.Rows.Count + 6) + ")";
                    rgHead4.Font.Size = 10;

                    rgHead4 = worksheet.get_Range("G" + (dtExcel.Rows.Count + 7) + ":G" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(G7:G" + (dtExcel.Rows.Count + 6) + ")";
                    rgHead4.Font.Size = 10;

                    rgHead4 = worksheet.get_Range("H" + (dtExcel.Rows.Count + 7) + ":H" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(H7:H" + (dtExcel.Rows.Count + 6) + ")";
                    rgHead4.Font.Size = 10;

                    rgHead4 = worksheet.get_Range("I" + (dtExcel.Rows.Count + 7) + ":I" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(I7:I" + (dtExcel.Rows.Count + 6) + ")";
                    rgHead4.Font.Size = 10;

                    rgHead4 = worksheet.get_Range("J" + (dtExcel.Rows.Count + 7) + ":J" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(J7:J" + (dtExcel.Rows.Count + 6) + ")";
                    rgHead4.Font.Size = 10;

                    rgHead4 = worksheet.get_Range("K" + (dtExcel.Rows.Count + 7) + ":K" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(K7:K" + (dtExcel.Rows.Count + 6) + ")";
                    rgHead4.Font.Size = 10;


                    rgHead4 = worksheet.get_Range("L" + (dtExcel.Rows.Count + 7) + ":L" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(L7:L" + (dtExcel.Rows.Count + 6) + ")";
                    rgHead4.Font.Size = 10;


                    rgHead4 = worksheet.get_Range("M" + (dtExcel.Rows.Count + 7) + ":M" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(M7:M" + (dtExcel.Rows.Count + 6) + ")";
                    rgHead4.Font.Size = 10;


                    rgHead4 = worksheet.get_Range("N" + (dtExcel.Rows.Count + 7) + ":N" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(N7:N" + (dtExcel.Rows.Count + 6) + ")";
                    rgHead4.Font.Size = 10;


                    rgHead4 = worksheet.get_Range("O" + (dtExcel.Rows.Count + 7) + ":O" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(O7:O" + (dtExcel.Rows.Count + 6) + ")";
                    rgHead4.Font.Size = 10;


                    rgHead4 = worksheet.get_Range("P" + (dtExcel.Rows.Count + 7) + ":P" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(P7:P" + (dtExcel.Rows.Count + 6) + ")";
                    rgHead4.Font.Size = 10;


                    rgHead4 = worksheet.get_Range("Q" + (dtExcel.Rows.Count + 7) + ":Q" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(Q7:Q" + (dtExcel.Rows.Count + 6) + ")";
                    rgHead4.Font.Size = 10;


                    rgHead4 = worksheet.get_Range("R" + (dtExcel.Rows.Count + 7) + ":R" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(R7:R" + (dtExcel.Rows.Count + 6) + ")";
                    rgHead4.Font.Size = 10;


                    rgHead4 = worksheet.get_Range("S" + (dtExcel.Rows.Count + 7) + ":S" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=Sum(S7:S" + (dtExcel.Rows.Count + 6) + ")";
                    rgHead4.Font.Size = 10;

                    rgHead4 = worksheet.get_Range("T" + (dtExcel.Rows.Count + 7) + ":T" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=(M" + (dtExcel.Rows.Count + 7) + "/R" + (dtExcel.Rows.Count + 7) + ")";
                    rgHead4.Font.Size = 10;

                    rgHead4 = worksheet.get_Range("U" + (dtExcel.Rows.Count + 7) + ":U" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=(I" + (dtExcel.Rows.Count + 7) + "/R" + (dtExcel.Rows.Count + 7) + ")";
                    rgHead4.Font.Size = 10;

                    rgHead4 = worksheet.get_Range("V" + (dtExcel.Rows.Count + 7) + ":V" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=(I" + (dtExcel.Rows.Count + 7) + "/M" + (dtExcel.Rows.Count + 7) + ")";
                    rgHead4.Font.Size = 10;

                    rgHead4 = worksheet.get_Range("W" + (dtExcel.Rows.Count + 7) + ":W" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=(I" + (dtExcel.Rows.Count + 7) + "/H" + (dtExcel.Rows.Count + 7) + ")";
                    rgHead4.Font.Size = 10;

                    rgHead4 = worksheet.get_Range("X" + (dtExcel.Rows.Count + 7) + ":X" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=(R" + (dtExcel.Rows.Count + 7) + "/H" + (dtExcel.Rows.Count + 7) + ")";
                    rgHead4.Font.Size = 10;

                    rgHead4 = worksheet.get_Range("Y" + (dtExcel.Rows.Count + 7) + ":Y" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=(M" + (dtExcel.Rows.Count + 7) + "/H" + (dtExcel.Rows.Count + 7) + ")";
                    rgHead4.Font.Size = 10;

                    rgHead4 = worksheet.get_Range("Z" + (dtExcel.Rows.Count + 7) + ":Z" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=(Q" + (dtExcel.Rows.Count + 7) + "/H" + (dtExcel.Rows.Count + 7) + ")";
                    rgHead4.Font.Size = 10;

                    rgHead4 = worksheet.get_Range("AA" + (dtExcel.Rows.Count + 7) + ":AA" + (dtExcel.Rows.Count + 7), Type.Missing);
                    //rgHead4.Cells.ColumnWidth = 12;
                    rgHead4.Font.Bold = true;
                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                    rgHead4.Formula = "=(S" + (dtExcel.Rows.Count + 7) + "/H" + (dtExcel.Rows.Count + 7) + ")";
                    rgHead4.Font.Size = 10;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            #endregion

            #region "FormID 53 :: GC/GL WISE SALE BULLETINS"

            if (FormID == 53)
            {
                DataTable dtExcel = new DataTable();
                objExDb = new ExcelDB();
                objUtilityDB = new UtilityDB();
                dtExcel = objExDb.Get_GCGLWiseSaleBulletins(CommonData.CompanyCode, CommonData.BranchCode, strLogicalBranches, dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "GL_PRODUCTWISE_STATEMENT").Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {
                        string strHead = "";

                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;
                        int iTotColumns = 0;
                        iTotColumns = 32;
                        string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                        Excel.Range rgHead = null;
                        Excel.Range rg = worksheet.get_Range("A6", sLastColumn + "6");
                        Excel.Range rgData = worksheet.get_Range("A7", sLastColumn + (dtExcel.Rows.Count + 6).ToString());
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;

                        rgData = worksheet.get_Range("A1", "S1");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 16;
                        rgData.Value2 = "GC /GL WISE SALE BULLETIN";
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.HorizontalAlignment = Excel.Constants.xlCenter;

                        rgData.ColumnWidth = 20;
                        rgData.RowHeight = 20;
                        rgData.Font.ColorIndex = 11;
                        rgData = worksheet.get_Range("A2", "Y2");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 13;

                        strHead = "Name Of the Branch Incharge :" + dtExcel.Rows[0]["branch_incharge_name"].ToString() + "     \t   Physical Branch : " + dtExcel.Rows[0]["BRANCH_NAME"].ToString() + "   \t     Doc Month : " + dtExcel.Rows[0]["DOCU_MONTH"].ToString();
                        strHead = strHead.TrimEnd(',');
                        rgData.Value2 = strHead;
                        rgData.Font.ColorIndex = 9;
                        rgData.ColumnWidth = 200;
                        rgData.RowHeight = 20;

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
                        rg.Cells.RowHeight = 30;

                        rg = worksheet.get_Range("A6", "A4");
                        rg.Merge(Type.Missing);
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Value2 = "Sl.No";
                        rg.Cells.ColumnWidth = 4;
                        rg = worksheet.get_Range("B6", "B4");
                        rg.Merge(Type.Missing);
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Value2 = "Ecode";
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("C6", "C4");
                        rg.Merge(Type.Missing);
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Value2 = "Name of the GC /GL";
                        rg.Cells.ColumnWidth = 25;
                        rg = worksheet.get_Range("D6", "D4");
                        rg.Merge(Type.Missing);
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Value2 = "Desig.";
                        rg.Cells.ColumnWidth = 5;
                        rg = worksheet.get_Range("E6", "E4");
                        rg.Merge(Type.Missing);
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Value2 = "DOJ";
                        rg.Cells.ColumnWidth = 10;

                        rg = worksheet.get_Range("F6", "F4");
                        rg.Merge(Type.Missing);
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Value2 = "Logical Branch";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 10;

                        rg = worksheet.get_Range("G6", "G4");
                        rg.Merge(Type.Missing);
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Value2 = "Last Promotion Date";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("H4", "S4");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Personal";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("H6", "H5");
                        rg.Merge(Type.Missing);
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Value2 = "PMD";
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("I6", "I5");
                        rg.Merge(Type.Missing);
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Value2 = "Demos";
                        rg.Cells.ColumnWidth = 7;
                        rg = worksheet.get_Range("J5", "M5");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Sales/Units";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("J6", Type.Missing);
                        rg.Value2 = "Gromin";
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("K6", Type.Missing);
                        rg.Value2 = "Teak";
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("L6", Type.Missing);
                        rg.Value2 = "Focused";
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("M6", Type.Missing);
                        rg.Value2 = "Total";
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("N5", "Q5");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Points";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("N6", Type.Missing);
                        rg.Value2 = "Gromin";
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("O6", Type.Missing);
                        rg.Value2 = "Teak";
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("P6", Type.Missing);
                        rg.Value2 = "Focused";
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("Q6", Type.Missing);
                        rg.Value2 = "Total";
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("R6", "R5");
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Cust.";
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("S6", "S5");
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Revenue";
                        rg.Cells.ColumnWidth = 12;
                        rg = worksheet.get_Range("T4", "AF4");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Group(Including Personal)";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("T6", "T5");
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Merge(Type.Missing);
                        rg.Value2 = "No.of SR's";
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("U6", "U5");
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Merge(Type.Missing);
                        rg.Value2 = "PMD";
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("V6", "V5");
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Demos";
                        rg.Cells.ColumnWidth = 7;
                        rg = worksheet.get_Range("W5", "Z5");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Sales/Units";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("W6", Type.Missing);
                        rg.Value2 = "Gromin";
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("X6", Type.Missing);
                        rg.Value2 = "Teak";
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("Y6", Type.Missing);
                        rg.Value2 = "Focused";
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("Z6", Type.Missing);
                        rg.Value2 = "Total";
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("AA5", "AD5");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Points";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("AA6", Type.Missing);
                        rg.Value2 = "Gromin";
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("AB6", Type.Missing);
                        rg.Value2 = "Teak";
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("AC6", Type.Missing);
                        rg.Value2 = "Focused";
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("AD6", Type.Missing);
                        rg.Value2 = "Total";
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("AE6", "AE5");
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Cust.";
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("AF6", "AF5");
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Revenue";
                        rg.Cells.ColumnWidth = 10;

                        int iColumn = 1, iStartRow = 7;


                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            worksheet.Cells[iStartRow, iColumn++] = i + 1;
                            if (dtExcel.Rows[i]["GL_NAME"].ToString() == "OFFICE SALES")
                                worksheet.Cells[iStartRow, iColumn++] = "";
                            else
                                worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["GL_ECODE"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["GL_NAME"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["gl_desig"].ToString();

                            if (dtExcel.Rows[i]["gl_doj"].ToString() != "")
                                worksheet.Cells[iStartRow, iColumn++] = Convert.ToDateTime(dtExcel.Rows[i]["gl_doj"].ToString()).ToString("dd/MMM/yyyy");
                            else
                                worksheet.Cells[iStartRow, iColumn++] = "";
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["LOG_BRANCH_NAME"].ToString();
                            if (dtExcel.Rows[i]["gl_last_promotion_date"].ToString() != "")
                                worksheet.Cells[iStartRow, iColumn++] = Convert.ToDateTime(dtExcel.Rows[i]["gl_last_promotion_date"].ToString()).ToString("dd/MMM/yyyy");
                            else
                                worksheet.Cells[iStartRow, iColumn++] = "";
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["pers_pmd"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["pers_demos"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["pers_gromin_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["pers_teak_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["pers_focused_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["pers_total_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["pers_gromin_points"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["pers_teak_points"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["pers_focused_points"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["pers_total_points"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["pers_cust"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["pers_revenue"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["no_srs"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["pmd"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["demos"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["gromin_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["teak_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["focused_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["total_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["gromin_points"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["teak_points"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["focused_points"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["total_points"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["cust"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["revenue"].ToString();

                            iStartRow++; iColumn = 1;
                        }

                        iStartRow = 8;
                        iColumn = iStartRow;
                        rgHead = worksheet.get_Range("H" + (Convert.ToInt32(dtExcel.Rows.Count) + 7).ToString(),
                                                "AF" + (Convert.ToInt32(dtExcel.Rows.Count) + 7).ToString());

                        rg = worksheet.get_Range("A" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString(),
                                                "G" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString());
                        //rg.Merge(Type.Missing);
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Font.ColorIndex = 30;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("A" + (Convert.ToInt32(dtExcel.Rows.Count) + 7).ToString(),
                                                "G" + (Convert.ToInt32(dtExcel.Rows.Count) + 7).ToString());
                        rg.Merge(Type.Missing);
                        rg.Value2 = "BRANCH TOTAL";

                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Font.ColorIndex = 30;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;

                        rgHead.Borders.Weight = 2;
                        rgHead.Font.Size = 12; rgHead.Font.Bold = true;

                        for (int iMonths = 0; iMonths <= Convert.ToInt32(dtExcel.Rows.Count); iMonths++)
                        {
                            iStartRow = 8; iColumn = iStartRow;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 7, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString() + ")";
                            iColumn = iColumn + 1;

                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }

            #endregion

            #region FormID==54 :: SSCRM_MIS_TMAB_SALES_BULLETIN
            if (FormID == 54)
            {
                try
                {
                    objExDb = new ExcelDB();
                    objUtilityDB = new UtilityDB();
                    string strChkDemo = "";
                    for (int i = 0; i < chkDemoType.CheckedItems.Count; i++)
                    {
                        NewCheckboxListItem CL = (NewCheckboxListItem)chkDemoType.CheckedItems[i];
                        strChkDemo += "" + CL.Tag.ToString() + ",";
                    }
                    DataTable dtExcel = objExDb.GetTMWiseSalesBulletins(CommonData.CompanyCode, CommonData.BranchCode, dtpDocMonth.Value.ToString("MMMyyyy"), "", "TM_PRODUCTWISE_STATEMENT").Tables[0];
                    objExDb = null;


                    Excel.Application oXL = new Excel.Application();
                    Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                    Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                    oXL.Visible = true;

                    Excel.Range rg = worksheet.get_Range("A4", "S4");
                    Excel.Range rgData = worksheet.get_Range("A7", "S" + (dtExcel.Rows.Count + (Convert.ToInt32(dtExcel.Rows[0]["tm_count"])) * 2 + 6).ToString());
                    rgData.Font.Size = 11;
                    rgData.WrapText = true;
                    rgData.VerticalAlignment = Excel.Constants.xlCenter;
                    //rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgData.Borders.Weight = 2;


                    Excel.Range rgHead = worksheet.get_Range("A1", "S1");
                    rgHead.Cells.ColumnWidth = 5;
                    rgHead.Cells.MergeCells = true;
                    rgHead.Cells.Value2 = CommonData.CompanyName + "";
                    rgHead.Font.ColorIndex = 30;
                    rgHead.Font.Bold = true;
                    rgHead.Font.Size = 14;
                    rgHead.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgHead.Borders.Weight = 2;

                    Excel.Range rgHead1 = worksheet.get_Range("A2", "S2");
                    rgHead1.Cells.ColumnWidth = 5;
                    rgHead1.Cells.MergeCells = true;
                    rgHead1.Cells.Value2 = "TM & ABOVE SALES BULLETINS";
                    rgHead1.HorizontalAlignment = Excel.Constants.xlCenter;
                    rgHead1.Font.Bold = true;
                    rgHead1.Font.Size = 14;
                    rgHead1.Font.ColorIndex = 30; rgHead1.Borders.Weight = 2;

                    Excel.Range rgHead2 = worksheet.get_Range("A3", "S3");
                    rgHead2.Cells.ColumnWidth = 5;
                    rgHead2.Cells.MergeCells = true;
                    rgHead2.Cells.Value2 = "Physical Branch:" + dtExcel.Rows[0]["BRANCH_NAME"] + "                                                                                   " +
                                                                                        "                                                          DocMonth:" + dtExcel.Rows[0]["DOCU_MONTH"];
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
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.RowHeight = 25;

                    rg = worksheet.get_Range("A4:A6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 5;
                    rg.Borders.Weight = 2;
                    rg.Cells.Value2 = "Sl.No";

                    rg = worksheet.get_Range("B4:B6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 7;
                    rg.Borders.Weight = 2;
                    rg.Cells.Value2 = "ECODE";

                    rg = worksheet.get_Range("C4:C6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 20;
                    rg.Cells.Value2 = "Name Of The GC/GL";

                    rg = worksheet.get_Range("D4:D6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 3;
                    rg.Orientation = 90;
                    rg.Cells.Value2 = "Desig";

                    rg = worksheet.get_Range("E4:E6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 15;
                    //rg.Orientation = 90;
                    rg.Cells.Value2 = "DOJ";

                    rg = worksheet.get_Range("F4:F6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 15;
                    //rg.Orientation = 90;
                    rg.Cells.Value2 = "Last Promotion Date";

                    rg = worksheet.get_Range("G4:S4", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Borders.Weight = 2;
                    rg.Cells.ColumnWidth = 10;
                    rg.Cells.Value2 = "Group (Including Personal)";


                    rg = worksheet.get_Range("G5:G6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 5;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlRight;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "No of SRs";




                    rg = worksheet.get_Range("H5:H6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 5;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlRight;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "PMD";


                    rg = worksheet.get_Range("I5:I6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 5;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "Demos";

                    rg = worksheet.get_Range("J5:M5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 30;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "Sales/Units";

                    rg = worksheet.get_Range("J6:J6", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 5;
                    rg.Orientation = 90;
                    rg.Cells.Value2 = "Gromin";

                    rg = worksheet.get_Range("K6:K6", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 5;
                    rg.Orientation = 90;
                    rg.Cells.Value2 = "Teak";

                    rg = worksheet.get_Range("L6:L6", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 5;
                    rg.Orientation = 90;
                    rg.Cells.Value2 = "Others";

                    rg = worksheet.get_Range("M6:M6", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 5;
                    rg.Orientation = 90;
                    rg.Cells.Value2 = "Total";


                    rg = worksheet.get_Range("N5:Q5", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 30;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "Points";


                    rg = worksheet.get_Range("N6:N6", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 5;
                    rg.Orientation = 90;
                    rg.Cells.Value2 = "Gromin";

                    rg = worksheet.get_Range("O6:O6", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 5;
                    rg.Orientation = 90;
                    rg.Cells.Value2 = "Teak";

                    rg = worksheet.get_Range("P6:P6", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 5;
                    rg.Orientation = 90;
                    rg.Cells.Value2 = "Others";

                    rg = worksheet.get_Range("Q6:Q6", Type.Missing);
                    rg.Font.Name = "Times New Roman";
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Borders.Weight = 2;
                    rg.Font.Size = 10;
                    rg.Font.Bold = true;
                    rg.Font.ColorIndex = 2;
                    rg.Interior.ColorIndex = 31;
                    rg.Cells.ColumnWidth = 5;
                    rg.Orientation = 90;
                    rg.Cells.Value2 = "Total";

                    rg = worksheet.get_Range("R5:R6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 5;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "Cust";

                    rg = worksheet.get_Range("S5:S6", Type.Missing);
                    rg.Merge(Type.Missing);
                    rg.Cells.ColumnWidth = 12;
                    //rg.Orientation = 45;
                    rg.Font.Bold = true;
                    rg.Font.Name = "Times New Roman";
                    rg.Font.Size = 10;
                    rg.WrapText = true;
                    rg.Font.ColorIndex = 2;
                    rg.HorizontalAlignment = Excel.Constants.xlCenter;
                    rg.VerticalAlignment = Excel.Constants.xlCenter;
                    rg.Interior.ColorIndex = 31;
                    rg.Borders.Weight = 2;
                    rg.Borders.LineStyle = Excel.Constants.xlSolid;
                    rg.Cells.Value2 = "Revenue";

                    int tmCode = Convert.ToInt32(dtExcel.Rows[0]["tm_ecode"]);

                    Excel.Range rgHead5 = worksheet.get_Range("A7", "S7");
                    rgHead5.Cells.ColumnWidth = 5;
                    rgHead5.Cells.MergeCells = true;
                    rgHead5.Cells.Value2 = dtExcel.Rows[0]["tm_name"];
                    rgHead5.Font.Bold = true;
                    rgHead5.Font.Size = 13;
                    rgHead5.Font.ColorIndex = 30;
                    rgHead5.Borders.Weight = 2;
                    int tempRowcounter = 8;
                    int RowCounter = 8, ColCounter = 1, iData = 1;
                    for (int i = 0; i < dtExcel.Rows.Count; i++)
                    {
                        int code = Convert.ToInt32(dtExcel.Rows[i]["tm_ecode"]);
                        if (code != tmCode && i != 0)
                        {

                            Excel.Range rgHead3 = worksheet.get_Range("C" + (RowCounter) + ":C" + (RowCounter), Type.Missing);
                            rgHead3.Cells.ColumnWidth = 25;
                            rgHead3.Font.Bold = true;
                            rgHead3.Cells.Value2 = "TM/ABM/DBM/BM  Total";
                            rgHead3.Font.Size = 10;
                            rgHead3.VerticalAlignment = Excel.Constants.xlCenter;


                            Excel.Range rgHead4 = worksheet.get_Range("G" + (RowCounter) + ":G" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(G" + tempRowcounter + ":G" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("H" + (RowCounter) + ":H" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(H" + tempRowcounter + ":H" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("I" + (RowCounter) + ":I" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(I" + tempRowcounter + ":I" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("J" + (RowCounter) + ":J" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(J" + tempRowcounter + ":J" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("K" + (RowCounter) + ":K" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(K" + tempRowcounter + ":K" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("L" + (RowCounter) + ":L" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(L" + tempRowcounter + ":L" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("M" + (RowCounter) + ":M" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(M" + tempRowcounter + ":M" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("N" + (RowCounter) + ":M" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(M" + tempRowcounter + ":M" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("N" + (RowCounter) + ":N" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(N" + tempRowcounter + ":N" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("O" + (RowCounter) + ":O" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(O" + tempRowcounter + ":O" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("P" + (RowCounter) + ":P" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(P" + tempRowcounter + ":P" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("Q" + (RowCounter) + ":Q" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(Q" + tempRowcounter + ":Q" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("R" + (RowCounter) + ":R" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(R" + tempRowcounter + ":R" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("S" + (RowCounter) + ":S" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(S" + tempRowcounter + ":S" + (RowCounter - 1) + ")";

                            rgHead5 = worksheet.get_Range("A" + (RowCounter + 1), "S" + (RowCounter + 1));
                            //rgHead5.Cells.ColumnWidth = 25;
                            rgHead5.Cells.MergeCells = true;
                            rgHead5.Cells.Value2 = dtExcel.Rows[i]["tm_name"];
                            rgHead5.Font.Bold = true;
                            rgHead5.Font.Size = 13;
                            rgHead5.Font.ColorIndex = 30;
                            rgHead5.Borders.Weight = 2;
                            tmCode = Convert.ToInt32(dtExcel.Rows[i]["tm_ecode"]);

                            iData = 1;
                            RowCounter = RowCounter + 2;
                            tempRowcounter = RowCounter;
                        }



                        //for (int i = 0; i < dtExcel.Rows.Count; i++)
                        //{
                        worksheet.Cells[RowCounter, ColCounter++] = iData;
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["GL_ECODE"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["GL_NAME"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gl_desig"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gl_doj"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gl_last_promotion_date"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["no_srs"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["pmd"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["demos"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gromin_qty"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["teak_qty"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["focused_qty"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["total_qty"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gromin_points"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["teak_points"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["focused_points"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["total_points"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["cust"];
                        worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["revenue"];
                        RowCounter++;
                        iData++;
                        ColCounter = 1;

                        if (i == dtExcel.Rows.Count - 1)
                        {
                            Excel.Range rgHead3 = worksheet.get_Range("C" + (RowCounter) + ":C" + (RowCounter), Type.Missing);
                            rgHead3.Cells.ColumnWidth = 25;
                            rgHead3.Font.Bold = true;
                            rgHead3.Cells.Value2 = "TM/ABM/DBM/BM  Total";
                            rgHead3.Font.Size = 10;
                            rgHead3.VerticalAlignment = Excel.Constants.xlCenter;

                            Excel.Range rgHead4 = worksheet.get_Range("E" + (RowCounter) + ":E" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 10;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 16;
                            //rgHead4.Cells.Value2 = "-";
                            //rgHead4.Formula = "=Sum(G" + tempRowcounter + ":G" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("F" + (RowCounter) + ":F" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 10;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 16;
                            //rgHead4.Cells.Value2 = "-";
                            //rgHead4.Formula = "=Sum(G" + tempRowcounter + ":G" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("G" + (RowCounter) + ":G" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(G" + tempRowcounter + ":G" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("H" + (RowCounter) + ":H" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(H" + tempRowcounter + ":H" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("I" + (RowCounter) + ":I" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(I" + tempRowcounter + ":I" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("J" + (RowCounter) + ":J" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(J" + tempRowcounter + ":J" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("K" + (RowCounter) + ":K" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(K" + tempRowcounter + ":K" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("L" + (RowCounter) + ":L" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(L" + tempRowcounter + ":L" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("M" + (RowCounter) + ":M" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(M" + tempRowcounter + ":M" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("N" + (RowCounter) + ":N" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(N" + tempRowcounter + ":N" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("O" + (RowCounter) + ":O" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(O" + tempRowcounter + ":O" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("P" + (RowCounter) + ":P" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(P" + tempRowcounter + ":P" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("Q" + (RowCounter) + ":Q" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(Q" + tempRowcounter + ":Q" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("R" + (RowCounter) + ":R" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(R" + tempRowcounter + ":R" + (RowCounter - 1) + ")";

                            rgHead4 = worksheet.get_Range("S" + (RowCounter) + ":S" + (RowCounter), Type.Missing);
                            rgHead4.Cells.ColumnWidth = 8;
                            rgHead4.Font.Bold = true;
                            rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                            rgHead4.Font.Size = 10;
                            rgHead4.Formula = "=Sum(S" + tempRowcounter + ":S" + (RowCounter - 1) + ")";

                        }


                        //rgHead4 = worksheet.get_Range("H" + (dtExcel.Rows.Count + 8) + ":H" + (dtExcel.Rows.Count + 8), Type.Missing);
                        ////rgHead4.Cells.ColumnWidth = 12;
                        //rgHead4.Font.Bold = true;
                        //rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                        //rgHead4.Formula = "=Sum(H7:H" + dtExcel.Rows.Count + ")";
                        //rgHead4.Font.Size = 10;

                        //rgHead4 = worksheet.get_Range("I" + (dtExcel.Rows.Count + 8) + ":I" + (dtExcel.Rows.Count + 8), Type.Missing);
                        ////rgHead4.Cells.ColumnWidth = 12;
                        //rgHead4.Font.Bold = true;
                        //rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                        //rgHead4.Formula = "=Sum(I7:I" + dtExcel.Rows.Count + ")";
                        //rgHead4.Font.Size = 10;

                        //rgHead4 = worksheet.get_Range("J" + (dtExcel.Rows.Count + 8) + ":J" + (dtExcel.Rows.Count + 8), Type.Missing);
                        ////rgHead4.Cells.ColumnWidth = 12;
                        //rgHead4.Font.Bold = true;
                        //rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                        //rgHead4.Formula = "=Sum(J7:J" + dtExcel.Rows.Count + ")";
                        //rgHead4.Font.Size = 10;

                        //rgHead4 = worksheet.get_Range("K" + (dtExcel.Rows.Count + 8) + ":K" + (dtExcel.Rows.Count + 8), Type.Missing);
                        ////rgHead4.Cells.ColumnWidth = 12;
                        //rgHead4.Font.Bold = true;
                        //rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                        //rgHead4.Formula = "=Sum(K7:K" + dtExcel.Rows.Count + ")";
                        //rgHead4.Font.Size = 10;

                        //rgHead4 = worksheet.get_Range("L" + (dtExcel.Rows.Count + 8) + ":L" + (dtExcel.Rows.Count + 8), Type.Missing);
                        ////rgHead4.Cells.ColumnWidth = 12;
                        //rgHead4.Font.Bold = true;
                        //rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                        //rgHead4.Formula = "=Sum(L7:L" + dtExcel.Rows.Count + ")";
                        //rgHead4.Font.Size = 10;

                        //rgHead4 = worksheet.get_Range("M" + (dtExcel.Rows.Count + 8) + ":M" + (dtExcel.Rows.Count + 8), Type.Missing);
                        ////rgHead4.Cells.ColumnWidth = 12;
                        //rgHead4.Font.Bold = true;
                        //rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                        //rgHead4.Formula = "=Sum(M7:M" + dtExcel.Rows.Count + ")";
                        //rgHead4.Font.Size = 10;


                        //rgHead4 = worksheet.get_Range("N" + (dtExcel.Rows.Count + 8) + ":N" + (dtExcel.Rows.Count + 8), Type.Missing);
                        ////rgHead4.Cells.ColumnWidth = 12;
                        //rgHead4.Font.Bold = true;
                        //rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                        //rgHead4.Formula = "=Sum(N7:N" + dtExcel.Rows.Count + ")";
                        //rgHead4.Font.Size = 10;


                        //rgHead4 = worksheet.get_Range("O" + (dtExcel.Rows.Count + 8) + ":O" + (dtExcel.Rows.Count + 8), Type.Missing);
                        ////rgHead4.Cells.ColumnWidth = 12;
                        //rgHead4.Font.Bold = true;
                        //rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                        //rgHead4.Formula = "=Sum(O7:O" + dtExcel.Rows.Count + ")";
                        //rgHead4.Font.Size = 10;


                        //rgHead4 = worksheet.get_Range("P" + (dtExcel.Rows.Count + 8) + ":P" + (dtExcel.Rows.Count + 8), Type.Missing);
                        ////rgHead4.Cells.ColumnWidth = 12;
                        //rgHead4.Font.Bold = true;
                        //rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                        //rgHead4.Formula = "=Sum(P7:P" + dtExcel.Rows.Count + ")";
                        //rgHead4.Font.Size = 10;


                        //rgHead4 = worksheet.get_Range("Q" + (dtExcel.Rows.Count + 8) + ":Q" + (dtExcel.Rows.Count + 8), Type.Missing);
                        ////rgHead4.Cells.ColumnWidth = 12;
                        //rgHead4.Font.Bold = true;
                        //rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                        //rgHead4.Formula = "=Sum(Q7:Q" + dtExcel.Rows.Count + ")";
                        //rgHead4.Font.Size = 10;


                        //rgHead4 = worksheet.get_Range("R" + (dtExcel.Rows.Count + 8) + ":R" + (dtExcel.Rows.Count + 8), Type.Missing);
                        ////rgHead4.Cells.ColumnWidth = 12;
                        //rgHead4.Font.Bold = true;
                        //rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                        //rgHead4.Formula = "=Sum(R7:R" + dtExcel.Rows.Count + ")";
                        //rgHead4.Font.Size = 10;


                        //rgHead4 = worksheet.get_Range("S" + (dtExcel.Rows.Count + 8) + ":S" + (dtExcel.Rows.Count + 8), Type.Missing);
                        ////rgHead4.Cells.ColumnWidth = 12;
                        //rgHead4.Font.Bold = true;
                        //rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                        //rgHead4.Formula = "=Sum(S7:S" + dtExcel.Rows.Count + ")";
                        //rgHead4.Font.Size = 10;
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            #endregion

            #region "FormID 55 :: AO Wise Stock Reconciliation"

            if (FormID == 55)
            {
                DataTable dtExcel = new DataTable();
                objExDb = new ExcelDB();
                objUtilityDB = new UtilityDB();
                dtExcel = objExDb.Get_AOWiseReconciliation(CommonData.CompanyCode, CommonData.BranchCode, dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), dtpDocMonth.Value.ToString("MMMyyyy"), "0", "").Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {
                        string strHead = "";

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

                        rgData = worksheet.get_Range("A1", "M1");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 16;
                        rgData.Value2 = "AO WISE STOCK RECONCILIATION STATEMENT";
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.HorizontalAlignment = Excel.Constants.xlCenter;

                        rgData.ColumnWidth = 20;
                        rgData.RowHeight = 20;
                        rgData.Font.ColorIndex = 11;
                        rgData = worksheet.get_Range("A2", "M2");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 13;

                        strHead = " Branch : " + dtExcel.Rows[0]["sr_branch_name"].ToString()
                                  + "   \t    Doc Month : " + dtExcel.Rows[0]["sr_doc_month"].ToString();
                        strHead = strHead.TrimEnd(',');
                        rgData.Value2 = strHead;
                        rgData.Font.ColorIndex = 9;
                        rgData.ColumnWidth = 200;
                        rgData.RowHeight = 20;

                        rgData = worksheet.get_Range("A3", "A3");
                        rgData.Merge(Type.Missing);
                        rgData.ColumnWidth = 80;
                        rgData.RowHeight = 20;
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
                        rg.Cells.RowHeight = 52;

                        rg = worksheet.get_Range("A4", Type.Missing);
                        rg.Cells.ColumnWidth = 4;
                        rg = worksheet.get_Range("B4", Type.Missing);
                        rg.Cells.ColumnWidth = 35;
                        rg = worksheet.get_Range("C4", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg = worksheet.get_Range("D4", Type.Missing);
                        rg.WrapText = true;
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("E4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("F4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("G4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("H4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("I4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("J4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("K4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("L4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("M4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;


                        int iColumn = 1, iStartRow = 4;
                        worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                        worksheet.Cells[iStartRow, iColumn++] = "Name of the Employee";
                        worksheet.Cells[iStartRow, iColumn++] = "DCR NO";
                        worksheet.Cells[iStartRow, iColumn++] = "Total Received";
                        worksheet.Cells[iStartRow, iColumn++] = "Received from Other(AO/GC/GL)";
                        worksheet.Cells[iStartRow, iColumn++] = "Returned Units to (PU/TU/SP)";
                        worksheet.Cells[iStartRow, iColumn++] = "Net Received";
                        worksheet.Cells[iStartRow, iColumn++] = "Replacement Given Cust.";
                        worksheet.Cells[iStartRow, iColumn++] = "Total Replaced Units";
                        worksheet.Cells[iStartRow, iColumn++] = "Sales Against Repl.";
                        worksheet.Cells[iStartRow, iColumn++] = "Transfer UNITS to AO/GC/GL";
                        worksheet.Cells[iStartRow, iColumn++] = "% Of Repl";
                        worksheet.Cells[iStartRow, iColumn++] = "% Of Return";

                        iStartRow++; iColumn = 1;

                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            worksheet.Cells[iStartRow, iColumn++] = i + 1;
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_emp_name"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_DCR_NO"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_tot_receiv_units"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_stock_receiv_frm_others"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_return_units_good"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_net_received_units"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_no_of_repl_cust"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_tot_repl_units"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_sales_against_repl"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_stock_transfer_to_others"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_pers_of_repl"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sr_pers_of_stock_return"].ToString();

                            iStartRow++; iColumn = 1;
                        }


                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }

            #endregion

            #region FormID 56 :: ADVANCE REGISTER
            if (FormID == 56)
            {
                string strChkDemo = "";
                for (int i = 0; i < chkDemoType.CheckedItems.Count; i++)
                {
                    NewCheckboxListItem AR = (NewCheckboxListItem)chkDemoType.CheckedItems[i];
                    strChkDemo += "" + AR.Tag.ToString() + ",";
                }
                objExDb = new ExcelDB();
                DataTable dtExcel = objExDb.GetSalesOrderRegisterNew(CommonData.CompanyCode, CommonData.BranchCode, strChkDemo.TrimEnd(','), dtpDocMonth.Value.ToString("MMMyyyy")).Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;
                        objUtilityDB = new UtilityDB();
                        int iTotColumns = 0;
                        iTotColumns = 15;
                        string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);

                        Excel.Range rg = worksheet.get_Range("A3", sLastColumn + "3");
                        Excel.Range rgData = worksheet.get_Range("A4", sLastColumn + (dtExcel.Rows.Count + 3).ToString());

                        //Excel.Range rg = worksheet.get_Range("A3", "O3");
                        //Excel.Range rgData = worksheet.get_Range("A4", "O4" + (dtExcel.Rows.Count + 4).ToString());
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;

                        rgData = worksheet.get_Range("A1", "O1");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 16;
                        rgData.Value2 = dtExcel.Rows[0]["so_company_code"].ToString() + " \n ADVANCE REGISTER";
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.HorizontalAlignment = Excel.Constants.xlCenter;

                        rgData.ColumnWidth = 20;
                        rgData.RowHeight = 40;
                        rgData.Font.ColorIndex = 31;
                        rgData = worksheet.get_Range("A2", "O2");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 14;

                        string strHead = " Physical Branch : " + dtExcel.Rows[0]["so_branch_code"].ToString() + "  \t  Doc Month : " + dtExcel.Rows[0]["so_DocMonth"].ToString();

                        strHead = strHead.TrimEnd(',');
                        rgData.Value2 = strHead;
                        rgData.Font.ColorIndex = 13;
                        rgData.ColumnWidth = 200;
                        rgData.RowHeight = 25;

                        rg.Font.Bold = true;
                        rg.Font.Name = "Times New Roman";
                        rg.Font.Size = 10;
                        rg.WrapText = true;
                        rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 

                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Interior.ColorIndex = 31;
                        rg.Borders.Weight = 2;
                        rg.Borders.LineStyle = Excel.Constants.xlSolid;
                        rg.Cells.RowHeight = 38;

                        rg = worksheet.get_Range("A3", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Sl.No";

                        rg = worksheet.get_Range("B3", Type.Missing);
                        rg.Cells.ColumnWidth = 25;
                        rg.Cells.Value2 = "Logical Branch Name";

                        rg = worksheet.get_Range("C3", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        rg.Cells.Value2 = "Doc Month";

                        rg = worksheet.get_Range("D3", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Order No";

                        rg = worksheet.get_Range("E3", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "ARNo";

                        rg = worksheet.get_Range("F3", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Order Date";

                        rg = worksheet.get_Range("G3", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "SR Name";

                        rg = worksheet.get_Range("H3", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "Customer Name";

                        rg = worksheet.get_Range("I3", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Cells.Value2 = "Customer Address";

                        rg = worksheet.get_Range("J3", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Customer MobileNo";

                        rg = worksheet.get_Range("K3", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Cells.Value2 = "Product Details";

                        rg = worksheet.get_Range("L3", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Price";

                        rg = worksheet.get_Range("M3", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Qty";

                        rg = worksheet.get_Range("N3", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Order Amount";

                        rg = worksheet.get_Range("O3", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Advance Amount";


                        int RowCounter = 3;
                        int data = 1;
                        //rgData.HorizontalAlignment = Excel.Constants.xlCenter;

                        foreach (DataRow dr in dtExcel.Rows)
                        {
                            int i = 1;

                            worksheet.Cells[RowCounter + 1, i++] = data;
                            worksheet.Cells[RowCounter + 1, i++] = dr["so_lgBranch_name"].ToString();
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
                            data++;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            #endregion

            #region "FormID 58:: Doc Clearence Certificate"

            if (FormID == 58)
            {
                DataTable dtExcel = new DataTable();
                objExDb = new ExcelDB();
                objUtilityDB = new UtilityDB();
                dtExcel = objExDb.Get_DocClearenceCertificate(CommonData.CompanyCode, CommonData.BranchCode, dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "ALL", strLogicalBranches, "SUM-GC-ONLY").Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {
                        string strHead = "";

                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;
                        int iTotColumns = 0;
                        iTotColumns = 22;
                        string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                        Excel.Range rgHead = null;
                        Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                        Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 4).ToString());
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;

                        rgData = worksheet.get_Range("A1", "V1");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 16;
                        rgData.Value2 = "GC/GL Wise Accountability For The Month Of \t\t " + dtpDocMonth.Value.ToString("MMMyyyy").ToUpper() + "";
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.HorizontalAlignment = Excel.Constants.xlCenter;

                        rgData.ColumnWidth = 20;
                        rgData.RowHeight = 20;
                        rgData.Font.ColorIndex = 11;
                        rgData = worksheet.get_Range("A2", "V2");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 14;

                        strHead = "  Company :  " + dtExcel.Rows[0]["company_name"].ToString() + "     \t\t Physical Branch :  " + dtExcel.Rows[0]["branch_name"].ToString();
                        strHead = strHead.TrimEnd(',');
                        rgData.Value2 = strHead;
                        rgData.Font.ColorIndex = 30;
                        rgData.ColumnWidth = 200;
                        rgData.RowHeight = 22;

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
                        rg.Cells.RowHeight = 28;

                        rg = worksheet.get_Range("A4", "A3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Sl.No";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 3;
                        rg = worksheet.get_Range("B4", "B3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Ecode";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("C4", "C3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Name Of GC/GL";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 25;
                        rg = worksheet.get_Range("D4", "D3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Desig.";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 7;
                        rg = worksheet.get_Range("E4", "E3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Logical Branch";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("F4", "F3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "DOJ";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("G4", "G3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "PMD";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 7;
                        rg = worksheet.get_Range("H4", "H3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "DA days";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("I4", "I3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Demos";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 7;
                        rg = worksheet.get_Range("J4", "J3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Units";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 6;
                        rg = worksheet.get_Range("K4", "K3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Total Points";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 6;

                        rg = worksheet.get_Range("L3", "O3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Product Points";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("L4", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Gromin Points";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 7;
                        rg = worksheet.get_Range("M4", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Fpp Points";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 7;
                        rg = worksheet.get_Range("N4", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Cpp Points";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 7;
                        rg = worksheet.get_Range("O4", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Teak Points";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 7;

                        rg = worksheet.get_Range("P4", "P3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "No.of Cust.";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("Q4", "Q3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Revenue";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("R4", "R3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Fresh Adv. Amt.";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("S4", "S3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Old Adv. Amt";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("T4", "T3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Total Amount To Be";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("U4", "U3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Recd Amt.";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("V4", "V3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Diff Amt.";
                        rg.WrapText = true;
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Cells.ColumnWidth = 10;

                        int iColumn = 1, iStartRow = 5;

                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            worksheet.Cells[iStartRow, iColumn++] = i + 1;
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_eora_code"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_eora_name"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_eora_desg"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_log_branch_name"].ToString();
                            if (dtExcel.Rows[i]["sysbh_doj"].ToString() != "")
                                worksheet.Cells[iStartRow, iColumn++] = Convert.ToDateTime(dtExcel.Rows[i]["sysbh_doj"]).ToString("dd/MMM/yyyy");
                            else
                                worksheet.Cells[iStartRow, iColumn++] = "";
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_grp_pmd"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_grp_dadays"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_grp_demos"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_grp_qty"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_grp_prod_points"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_grp_GRW_points"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_grp_FPP_points"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_grp_CPP_points"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_grp_TEK_points"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_grp_invoices"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_grp_invamt"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_grp_FreshAdvance"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_grp_OldAdvance"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_grp_tobeCollected"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_grp_recdamt"].ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["sysbh_grp_due"].ToString();

                            iStartRow++; iColumn = 1;
                        }

                        iStartRow = 16;
                        iColumn = iStartRow;
                        rgHead = worksheet.get_Range("P" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString(),
                                                "V" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString());
                        rg = worksheet.get_Range("A" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString(),
                                                "O" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString());
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Total Of " + dtExcel.Rows[0]["branch_name"].ToString();
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 12;
                        rg.Font.ColorIndex = 30;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgHead.Borders.Weight = 2;
                        rgHead.Font.Size = 12; rgHead.Font.Bold = true;

                        for (int iMonths = 0; iMonths <= Convert.ToInt32(dtExcel.Rows.Count); iMonths++)
                        {
                            iStartRow = 16; iColumn = iStartRow;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 4).ToString() + ")";
                            iColumn = iColumn + 1;

                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }

            #endregion

            #region "FormId 59 :: GroupWise Doc Sheet"
            if (FormID == 59)
            {
                try
                {
                    objExDb = new ExcelDB();
                    objUtilityDB = new UtilityDB();
                    string strChkDemo = "";
                    for (int i = 0; i < chkDemoType.CheckedItems.Count; i++)
                    {
                        NewCheckboxListItem CL = (NewCheckboxListItem)chkDemoType.CheckedItems[i];
                        strChkDemo += "" + CL.Tag.ToString() + ",";
                    }
                    DataTable dtExcel = objExDb.GetGlWiseDocSheetDetails(CommonData.CompanyCode, CommonData.BranchCode, strChkDemo.TrimEnd(','), dtpDocMonth.Value.ToString("MMMyyyy"), "").Tables[0];
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


                        int totalColumnCount = Convert.ToInt32(dtExcel.Rows[0]["gds_gl_no_srs"]);

                        string strColCount = objUtilityDB.GetColumnName(totalColumnCount + 9);
                        Excel.Range rg = worksheet.get_Range("A4", strColCount + "" + 4);
                        string strToData = strColCount + "" + ((Convert.ToInt32(dtExcel.Rows[0]["gds_gl_no_srs1"].ToString())) + (Convert.ToInt32(dtExcel.Rows[0]["gds_gl_no_gl"].ToString())) + 8).ToString();
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
                        rgHead1.Cells.Value2 = "GROUP WISE DOC SHEET";
                        rgHead1.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgHead1.Font.Bold = true;
                        rgHead1.Font.Size = 14;
                        rgHead1.Font.ColorIndex = 30; rgHead1.Borders.Weight = 2;

                        Excel.Range rgHead2 = worksheet.get_Range("A3", strColCount + "" + 3);
                        rgHead2.Cells.ColumnWidth = 5;
                        rgHead2.Cells.MergeCells = true;
                        rgHead2.Cells.Value2 = "Physical Branch:" + dtExcel.Rows[0]["gds_branch_name"] +
                                                                                            "             DocMonth:" + dtExcel.Rows[0]["gds_doc_month"];
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
                        rg.Cells.ColumnWidth = 7;
                        rg.Font.Bold = true;
                        rg.Borders.Weight = 2;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        rg.Cells.Value2 = "ECODE";

                        rg = worksheet.get_Range("C6:C7", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 20;
                        rg.Font.Bold = true;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        rg.Cells.Value2 = "Name Of The SR/SE";

                        rg = worksheet.get_Range("D6:D7", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 3;
                        rg.Font.Bold = true;
                        rg.Orientation = 90;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        rg.Cells.Value2 = "Desig";

                        rg = worksheet.get_Range("E6:E7", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 10;
                        rg.WrapText = true;
                        rg.Font.Bold = true;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        rg.Cells.Value2 = "Logical Branch";


                        rg = worksheet.get_Range("F6:F7", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 12;
                        rg.Font.Bold = true;
                        //rg.Orientation = 90;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        rg.Cells.Value2 = "DOJ";

                        rg = worksheet.get_Range("G6:G7", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 10;
                        rg.Font.Bold = true;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        //rg.Orientation = 90;
                        rg.Cells.Value2 = "PMD";

                        rg = worksheet.get_Range("H6:H7", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 10;
                        rg.Font.Bold = true;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        //rg.Orientation = 90;
                        rg.Cells.Value2 = "DA DAYS";

                        rg = worksheet.get_Range("I6:I7", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 10;
                        rg.Font.Bold = true;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        //rg.Orientation = 90;
                        rg.Cells.Value2 = "DEMOS";

                        rg = worksheet.get_Range("J4:" + objUtilityDB.GetColumnName(Convert.ToInt32(dtExcel.Rows[0]["gds_sr_sale_prd_count"]) + 9) + "4", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 10;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        rg.Cells.Value2 = "SALE PRODUCTS";

                        strToData = objUtilityDB.GetColumnName(Convert.ToInt32(dtExcel.Rows[0]["gds_sr_sale_prd_count"].ToString()) + 10);
                        string strToData1 = objUtilityDB.GetColumnName(Convert.ToInt32(dtExcel.Rows[0]["gds_sr_sale_prd_count"].ToString())
                            + Convert.ToInt32(dtExcel.Rows[0]["gds_sr_free_prd_count"].ToString()) + 9);
                        rg = worksheet.get_Range(strToData + "4:" + strToData1 + "4", Type.Missing);
                        rg.Merge(Type.Missing);
                        rg.Borders.Weight = 2;
                        rg.Cells.ColumnWidth = 10;
                        rg.Font.ColorIndex = 2;
                        rg.Interior.ColorIndex = 31;
                        rg.Cells.Value2 = "FREE PRODUCTS";


                        int columnCount = 10;
                        int iCount = 1;
                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            if (dtExcel.Rows[i]["gds_sr_product_type"].ToString() == "SALE PRODUCT")
                            {

                                DataRow[] DR = dtProducts.Select("ProductName='" + dtExcel.Rows[i]["gds_sr_product_name"] + "' AND ProductType='SALE PRODUCT'");
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
                                    rg.Cells.Value2 = dtExcel.Rows[i]["gds_sr_product_name"];

                                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(columnCount) + "6:" + objUtilityDB.GetColumnName(columnCount) + "6", Type.Missing);
                                    rg.Borders.Weight = 2;
                                    rg.Cells.ColumnWidth = 10;
                                    rg.Font.Bold = true;
                                    rg.Font.ColorIndex = 2;
                                    rg.Interior.ColorIndex = 31;
                                    rg.Cells.Value2 = dtExcel.Rows[i]["gds_sr_product_rate"];

                                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(columnCount) + "7:" + objUtilityDB.GetColumnName(columnCount) + "7", Type.Missing);
                                    rg.Borders.Weight = 2;
                                    rg.Cells.ColumnWidth = 10;
                                    rg.Font.Bold = true;
                                    rg.Font.ColorIndex = 2;
                                    rg.Interior.ColorIndex = 31;
                                    rg.Cells.Value2 = dtExcel.Rows[i]["gds_sr_product_points"];
                                    columnCount++;
                                    dtProducts.Rows.Add(iCount++, dtExcel.Rows[i]["gds_sr_product_name"], "SALE PRODUCT");
                                }
                            }

                        }
                        columnCount = 10 + Convert.ToInt32(dtExcel.Rows[0]["gds_sr_sale_prd_count"].ToString());
                        iCount = Convert.ToInt32(dtExcel.Rows[0]["gds_sr_sale_prd_count"].ToString()) + 1;
                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            if (dtExcel.Rows[i]["gds_sr_product_type"].ToString() == "FREE PRODUCT")
                            {

                                DataRow[] DR = dtProducts.Select("ProductName='" + dtExcel.Rows[i]["gds_sr_product_name"] + "' AND ProductType='FREE PRODUCT'");
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
                                    rg.Cells.Value2 = dtExcel.Rows[i]["gds_sr_product_name"];

                                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(columnCount) + "6:" + objUtilityDB.GetColumnName(columnCount) + "6", Type.Missing);
                                    rg.Borders.Weight = 2;
                                    rg.Cells.ColumnWidth = 10;
                                    rg.Font.Bold = true;
                                    rg.Font.ColorIndex = 2;
                                    rg.Interior.ColorIndex = 31;
                                    rg.Cells.Value2 = dtExcel.Rows[i]["gds_sr_product_rate"];

                                    rg = worksheet.get_Range(objUtilityDB.GetColumnName(columnCount) + "7:" + objUtilityDB.GetColumnName(columnCount) + "7", Type.Missing);
                                    rg.Borders.Weight = 2;
                                    rg.Cells.ColumnWidth = 10;
                                    rg.Font.Bold = true;
                                    rg.Font.ColorIndex = 2;
                                    rg.Interior.ColorIndex = 31;
                                    rg.Cells.Value2 = dtExcel.Rows[i]["gds_sr_product_points"];
                                    columnCount++;
                                    dtProducts.Rows.Add(iCount++, dtExcel.Rows[i]["gds_sr_product_name"], "FREE PRODUCT");
                                }
                            }
                        }
                        int RowCounter = 8, ColCounter = 1, iData = 1;
                        int srCode = Convert.ToInt32(dtExcel.Rows[0]["gds_sr_ecode"]);
                        int tmCode = Convert.ToInt32(dtExcel.Rows[0]["gds_gl_ecode"]);
                        int tempRowcounter = 8;

                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            int code = Convert.ToInt32(dtExcel.Rows[i]["gds_gl_ecode"]);
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
                                    string strCol = objUtilityDB.GetColumnName(6 + iProd + 1);
                                    Excel.Range rgHead4 = worksheet.get_Range(strCol + "" + (RowCounter) + ":" + strCol + "" + (RowCounter), Type.Missing);
                                    rgHead4.Cells.ColumnWidth = 8;
                                    rgHead4.Font.Bold = true;
                                    rgHead4.VerticalAlignment = Excel.Constants.xlCenter;
                                    rgHead4.Font.Size = 10;
                                    rgHead4.Formula = "=Sum(" + strCol + "" + tempRowcounter + ":" + strCol + (RowCounter - 1) + ")";
                                }
                                tmCode = Convert.ToInt32(dtExcel.Rows[i]["gds_gl_ecode"]);
                                iData = 1;
                                RowCounter = RowCounter + 1;
                                tempRowcounter = RowCounter;
                            }

                            if (srCode != Convert.ToInt32(dtExcel.Rows[i]["gds_sr_ecode"]))
                            {
                                worksheet.Cells[RowCounter, ColCounter++] = iData;
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gds_sr_ecode"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gds_sr_name"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gds_sr_desig"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gds_log_branch_name"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gds_sr_doj"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gds_sr_pmd"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gds_sr_da"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gds_sr_demos"];

                                DataRow[] dr = dtProducts.Select("ProductName='" + dtExcel.Rows[i]["gds_sr_product_name"] + "' AND  ProductType='" + dtExcel.Rows[i]["gds_sr_product_type"] + "'");
                                string strNo = dr[0]["SlNo"].ToString();
                                worksheet.Cells[RowCounter, ColCounter + Convert.ToInt32(strNo) - 1] = dtExcel.Rows[i]["gds_sr_product_qty"];


                                srCode = Convert.ToInt32(dtExcel.Rows[i]["gds_sr_ecode"]);
                            }
                            else
                            {
                                DataRow[] dr = dtProducts.Select("ProductName='" + dtExcel.Rows[i]["gds_sr_product_name"] + "' AND  ProductType='" + dtExcel.Rows[i]["gds_sr_product_type"] + "'");
                                string strNo = dr[0]["SlNo"].ToString();
                                worksheet.Cells[RowCounter, ColCounter + Convert.ToInt32(strNo) - 1] = dtExcel.Rows[i]["gds_sr_product_qty"];
                            }
                            if (i == 0)
                            {
                                worksheet.Cells[RowCounter, ColCounter++] = iData;
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gds_sr_ecode"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gds_sr_name"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gds_sr_desig"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gds_log_branch_name"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gds_sr_doj"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gds_sr_pmd"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gds_sr_da"];
                                worksheet.Cells[RowCounter, ColCounter++] = dtExcel.Rows[i]["gds_sr_demos"];

                                DataRow[] dr = dtProducts.Select("ProductName='" + dtExcel.Rows[i]["gds_sr_product_name"] + "' AND  ProductType='" + dtExcel.Rows[i]["gds_sr_product_type"] + "'");
                                string strNo = dr[0]["SlNo"].ToString();
                                worksheet.Cells[RowCounter, ColCounter + Convert.ToInt32(strNo) - 1] = dtExcel.Rows[i]["gds_sr_product_qty"];
                            }
                            if (i != dtExcel.Rows.Count - 1)
                            {
                                if (srCode != Convert.ToInt32(dtExcel.Rows[i + 1]["gds_sr_ecode"]))
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
                                    string strCol = objUtilityDB.GetColumnName(6 + iProd + 1);
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
            #region "FormId 60 :: SALES REGISTER"
            if (FormID == 60)
            {
                try
                {

                    //if (strDocMonths.Length > 5)
                    //{
                    //crReportParams.DocMonths = strDocMonths;
                    //dtpInvoiceFromDate.Value.ToString("dd/MM/yyyy");
                    //dtpInvoiceToDate.Value.ToString("dd/MM/yyyy");

                    var firstDayOfMonth = new DateTime(dtpDocMonth.Value.Year, dtpDocMonth.Value.Month, 1);
                    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                    string sRep = "BRANCH-ORDNO";
                    //if (cmbSalesInvoice.SelectedIndex == 0)
                    //    sRep = "BRANCH-ORDNO";
                    //else if (cmbSalesInvoice.SelectedIndex == 1)
                    //    sRep = "BRANCH-INVNO";
                    //else if (cmbSalesInvoice.SelectedIndex == 2)
                    //    sRep = "BRANCH-GRP-ORDNO";
                    //else if (cmbSalesInvoice.SelectedIndex == 3)
                    //    sRep = "BRANCH-GRP-INVOICE";

                    string strChkDemo = "";
                    for (int i = 0; i < chkDemoType.CheckedItems.Count; i++)
                    {
                        NewCheckboxListItem CL = (NewCheckboxListItem)chkDemoType.CheckedItems[i];
                        strChkDemo += "" + CL.Tag.ToString() + ",";
                    }

                    DataTable dtEx = null;
                    objExDb = new ExcelDB();
                    dtEx = objExDb.GetSalesRegisterDetails1(CommonData.CompanyCode, CommonData.BranchCode, strChkDemo.TrimEnd(','), dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), firstDayOfMonth.ToString("dd/MMM/yyyy"), lastDayOfMonth.ToString("dd/MMM/yyyy"), sRep).Tables[0];
                    if (dtEx.Rows.Count > 0)
                    {
                        //try
                        //{
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        //theWorkbook.Name = CommonData.BranchName + " SALES REGISTER " + CommonData.DocMonth.ToUpper();
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        worksheet.Name = dtpDocMonth.Value.ToString("MMMyyyy").ToUpper();
                        oXL.Visible = true;

                        Excel.Range rgHead = worksheet.get_Range("A1", "O1");
                        rgHead.Font.Size = 14;
                        rgHead.Cells.MergeCells = true;
                        rgHead.VerticalAlignment = Excel.Constants.xlCenter;
                        rgHead.Font.Bold = true;
                        rgHead.Font.ColorIndex = 30;
                        rgHead.Borders.Weight = 2;

                        Excel.Range rg = worksheet.get_Range("A2", "Z2");
                        Excel.Range rgData = worksheet.get_Range("A3", "Z" + (dtEx.Rows.Count + 2).ToString());
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
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.Interior.ColorIndex = 31;
                        rg.Borders.Weight = 2;
                        rg.Borders.LineStyle = Excel.Constants.xlSolid;
                        rg.Cells.RowHeight = 38;

                        rgHead = worksheet.get_Range("A1", "K1");
                        rgHead.Cells.ColumnWidth = 5;
                        rgHead.Cells.Value2 = "SALES REGISTER \n " + CommonData.BranchName;
                        rgHead.Cells.RowHeight = 40;
                        rgHead.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgHead.VerticalAlignment = Excel.Constants.xlCenter;

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
                        rg.Cells.ColumnWidth = 25;
                        rg.Cells.Value2 = "Logical Branch Name";

                        rg = worksheet.get_Range("E2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Financial Year";

                        rg = worksheet.get_Range("F2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Document Month";

                        rgData = worksheet.get_Range("I3", "I" + (dtEx.Rows.Count + 2).ToString());
                        rgData.Cells.NumberFormat = "dd/MMM/yyyy";

                        rg = worksheet.get_Range("G2", Type.Missing);
                        rg.Cells.ColumnWidth = 7;
                        rg.Cells.Value2 = "Invoice No";

                        rg = worksheet.get_Range("H2", Type.Missing);
                        rg.Cells.ColumnWidth = 7;
                        rg.Cells.Value2 = "Order No";

                        rg = worksheet.get_Range("I2", Type.Missing);
                        rg.Cells.ColumnWidth = 13;
                        rg.Cells.Value2 = "Invoice Date";

                        rg = worksheet.get_Range("J2", Type.Missing);
                        rg.Cells.ColumnWidth = 7;
                        rg.Cells.Value2 = "SR Code";

                        rg = worksheet.get_Range("K2", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "SR Name";

                        rg = worksheet.get_Range("L2", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg.Cells.Value2 = "Group Name";

                        rg = worksheet.get_Range("M2", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "GL Name";
                        rg = worksheet.get_Range("N2", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "Customer ID";
                        rg = worksheet.get_Range("O2", Type.Missing);
                        rg.Cells.ColumnWidth = 40;
                        rg.Cells.Value2 = "Customer Address";
                        rg = worksheet.get_Range("P2", Type.Missing);
                        rg.Cells.ColumnWidth = 12;
                        rg.Cells.Value2 = "Mobile No";
                        rg = worksheet.get_Range("Q2", Type.Missing);
                        rg.Cells.ColumnWidth = 25;
                        rg.Cells.Value2 = "Products";
                        rg = worksheet.get_Range("R2", Type.Missing);
                        rg.Cells.ColumnWidth = 7;
                        rg.Cells.Value2 = "Sold Qty";
                        rg = worksheet.get_Range("S2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Price";
                        rg = worksheet.get_Range("T2", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "FreeProducts";
                        rg = worksheet.get_Range("U2", Type.Missing);
                        //rg.Cells.ColumnWidth = 15;
                        rg.Cells.Value2 = "Free Qty";
                        rg = worksheet.get_Range("V2", Type.Missing);
                        //rg.Cells.ColumnWidth = 15;
                        rg.Cells.Value2 = "Total Qty";
                        rg = worksheet.get_Range("W2", Type.Missing);
                        //rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Total Points";
                        rg = worksheet.get_Range("X2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Advance Recieved";
                        rg = worksheet.get_Range("Y2", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Recieved Amount";
                        rg = worksheet.get_Range("Z2", Type.Missing);
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
                            rg.Borders.Weight = 2; rg.WrapText = true; rg.Merge(Type.Missing);
                            rg = worksheet.get_Range("Q" + (RowCounter + 1).ToString(), "Q" + (iMergRows + RowCounter).ToString());
                            rg.Borders.Weight = 2; rg.WrapText = true;
                            rg = worksheet.get_Range("R" + (RowCounter + 1).ToString(), "R" + (iMergRows + RowCounter).ToString());
                            rg.Borders.Weight = 2; rg.WrapText = true;
                            rg = worksheet.get_Range("S" + (RowCounter + 1).ToString(), "S" + (iMergRows + RowCounter).ToString());
                            rg.Borders.Weight = 2; rg.WrapText = true;
                            rg = worksheet.get_Range("T" + (RowCounter + 1).ToString(), "T" + (iMergRows + RowCounter).ToString());
                            rg.Borders.Weight = 2; rg.WrapText = true;
                            rg = worksheet.get_Range("U" + (RowCounter + 1).ToString(), "U" + (iMergRows + RowCounter).ToString());
                            rg.Borders.Weight = 2; rg.WrapText = true;
                            rg = worksheet.get_Range("V" + (RowCounter + 1).ToString(), "V" + (iMergRows + RowCounter).ToString());
                            rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                            rg = worksheet.get_Range("W" + (RowCounter + 1).ToString(), "W" + (iMergRows + RowCounter).ToString());
                            rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                            rg = worksheet.get_Range("X" + (RowCounter + 1).ToString(), "X" + (iMergRows + RowCounter).ToString());
                            rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                            rg = worksheet.get_Range("Y" + (RowCounter + 1).ToString(), "Y" + (iMergRows + RowCounter).ToString());
                            rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;
                            rg = worksheet.get_Range("Z" + (RowCounter + 1).ToString(), "Z" + (iMergRows + RowCounter).ToString());
                            rg.Borders.Weight = 2; rg.Merge(Type.Missing); rg.WrapText = true;

                            worksheet.Cells[RowCounter + 1, i++] = iData;
                            worksheet.Cells[RowCounter + 1, i++] = dr["sr_company_name"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sr_branch_name"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["sr_log_branch_name"].ToString();
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
                            worksheet.Cells[RowCounter + 1, i++] = dbQty.ToString("0.0");
                            worksheet.Cells[RowCounter + 1, i++] = dbPts.ToString("0.0");

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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            #endregion

            #region FormID 6 :: Sales Working Employees
            if (FormID == 6)
            {
                string strHead = "";
                objExDb = new ExcelDB();
                objUtilityDB = new UtilityDB();
                DataTable dtExcel = objExDb.Get_WorkingSalesEmployees(CommonData.CompanyCode, CommonData.BranchCode, "", dtpDocMonth.Value.ToString("MMMyyyy").ToUpper(), "SALES STAFF").Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;
                        int iTotColumns = 10;

                        string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                        Excel.Range rgHead = null;
                        Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                        Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 4).ToString());
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;

                        rgData = worksheet.get_Range("A1", "J2");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 16;
                        rgData.Value2 = "WORKING EMPLOYEES";
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.HorizontalAlignment = Excel.Constants.xlCenter;

                        rgData.ColumnWidth = 20;
                        rgData.RowHeight = 15;
                        rgData.Font.ColorIndex = 25;
                        rgData = worksheet.get_Range("A3", "J3");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 13;

                        strHead = " Company : " + dtExcel.Rows[0]["em_company_name"].ToString() + "  \t\t   Branch : " + dtExcel.Rows[0]["em_branch_name"].ToString() +
                                  "   \t\t   Doc Month : " + dtpDocMonth.Value.ToString("MMMyyyy").ToUpper();

                        strHead = strHead.TrimEnd(',');
                        rgData.Value2 = strHead;
                        rgData.Font.ColorIndex = 30;
                        rgData.ColumnWidth = 180;
                        rgData.RowHeight = 20;

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
                        rg.Cells.RowHeight = 25;

                        rg = worksheet.get_Range("A4", Type.Missing);
                        rg.Cells.ColumnWidth = 4;
                        rg = worksheet.get_Range("B4", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("C4", Type.Missing);
                        rg.Cells.ColumnWidth = 25;
                        rg = worksheet.get_Range("D4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("E4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("F4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("G4", Type.Missing);
                        rg.Cells.ColumnWidth = 25;
                        rg = worksheet.get_Range("H4", Type.Missing);
                        rg.Cells.ColumnWidth = 12;
                        rg = worksheet.get_Range("I4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("J4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;


                        int iColumn = 1, iStartRow = 4;
                        worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                        worksheet.Cells[iStartRow, iColumn++] = "Ecode";
                        worksheet.Cells[iStartRow, iColumn++] = "Name";
                        worksheet.Cells[iStartRow, iColumn++] = "Desig.";
                        worksheet.Cells[iStartRow, iColumn++] = "DOJ";
                        worksheet.Cells[iStartRow, iColumn++] = "DOB";
                        worksheet.Cells[iStartRow, iColumn++] = "Father Name";
                        worksheet.Cells[iStartRow, iColumn++] = "Qualification";
                        worksheet.Cells[iStartRow, iColumn++] = "Working Status";
                        worksheet.Cells[iStartRow, iColumn++] = "Mapped Status";

                        iStartRow++; iColumn = 1;

                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {

                            worksheet.Cells[iStartRow, iColumn++] = (i + 1).ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["em_eora_code"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["em_eora_name"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["em_desig"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["em_doj"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["em_dob"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["em_father_name"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["em_qualification"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["em_status"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["em_mapped_status"];

                            iStartRow++; iColumn = 1;
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            #endregion           

        }

        private void chkDemoType_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (FormID == 57)
            {
                for (int i = 0; i < chkDemoType.Items.Count; i++)
                {
                    if (e.Index != i)
                        chkDemoType.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
           
        }

        private void dtpDocMonth_ValueChanged(object sender, EventArgs e)
        {
            if (FormID == 45 || FormID == 48 || FormID == 46 || FormID == 50
                || FormID == 52 || FormID == 53 || FormID == 56 || FormID == 58 || FormID == 59 || FormID == 60)
                FillLogBranchesToList();
            if (FormID == 57)
                FillGCGLEcodesToList();
        }

        private void chkBrAll_CheckedChanged(object sender, EventArgs e)
        {
           
            if (chkBrAll.Checked == true)
            {
                chkDemoType.Enabled = false;
                for (int i = 0; i < chkDemoType.Items.Count; i++)
                {
                    chkDemoType.SetItemCheckState(i, CheckState.Checked);
                }               
            }
            else
            {
                chkDemoType.Enabled = true;
                for (int i = 0; i < chkDemoType.Items.Count; i++)
                {
                    chkDemoType.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }
    }
}
