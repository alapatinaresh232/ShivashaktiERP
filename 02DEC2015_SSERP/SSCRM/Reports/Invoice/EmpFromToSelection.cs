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
    public partial class EmpFromToSelection : Form
    {
        private ReportViewer childReportViewer = null;
        private ExcelDB objExDb = null;
        private UtilityDB objUtilityDB = null;
        private int iFormType = 0;
        string strLogicalBranches = "";
        private SQLDB objDB = null;
        public EmpFromToSelection()
        {
            InitializeComponent();
        }
        public EmpFromToSelection(int iForm)
        {
            InitializeComponent();
            iFormType = iForm;
        }
        private void EmpFromToSelection_Load(object sender, EventArgs e)
        {
            chkBrAll.Visible = false;
            lblDempType.Visible = false;
            chkDemoType.Visible = false;
            FillReportType();
            if (iFormType == 1)
            {
                this.Text = "Employee :: Performance";
                cbRepType.Visible = false;
            }
            else if (iFormType == 2)
            {
                this.Text = "Advance Refund Register";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
               
            }
            else if (iFormType == 3)
            {
                this.Text = "SP DC's of an Emp";
                //lblEcode.Visible = false;
                //txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 4)
            {
                this.Text = "Stock Point :: Reconsilation";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 5)
            {
                this.Text = "Dash Board :: EmpAppl";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 6)
            {
                this.Text = "Stationary :: Reconsilation";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 7)
            {
                this.Text = "Stationary :: Issue Register";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 8)
            {
                this.Text = "Stationary :: Indent Register";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 9)
            {
                this.Text = "Production :: Finished Goods";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 10)
            {
                this.Text = "StockPoint :: Stock Ledger";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
                dtpFromDate.CustomFormat = "MMMyyyy";
                dtpToDate.CustomFormat = "MMMyyyy";
            }
            else if (iFormType == 11)
            {
                this.Text = "StockPoint :: Stock Summary";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
                dtpFromDate.CustomFormat = "MMMyyyy";
                dtpToDate.CustomFormat = "MMMyyyy";
            }
            else if (iFormType == 12)
            {
                this.Text = "IDCards :: History";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 13)
            {
                this.Text = "PU :: DC Register";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 14)
            {
                this.Text = "PU :: DCST Register";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 15)
            {
                this.Text = "HR :: Approvals List By Date";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 16)
            {
                this.Text = "HR :: LateCommings Summary";
                lblEcode.Visible = true;
                txtEcode.Visible = false;
                cbRepType.Visible = true;
                lblEcode.Text = "Report";
            }
            else if (iFormType == 17)
            {
                this.Text = "Dealer Marketing :: Order Bookings Reg";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 18)
            {
                this.Text = "Production Unit :: GRN";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 19)
            {
                this.Text = "Production :: Stock Summary";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 20)
            {
                this.Text = "SP DC's of an Emp";
                //lblEcode.Visible = false;
                //txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 21)
            {
                this.Text = "Stationary GRN";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 22)
            {
                this.Text = "Service Activity Register";              
                txtEcode.Visible = false;
                cbRepType.Visible = false;
                lblEcode.Visible = false;
                btnDownload.Visible = false;
            }
            else if (iFormType == 23)
            {
                this.Text = "PU :: DCR Register";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 24)
            {
                this.Text = "New SR : Joinings";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 25)
            {
                this.Text = "SR : Left";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 26)
            {
                this.Text = "Special Approval Register";
                txtEcode.Visible = false;
                cbRepType.Visible = false;
                lblEcode.Visible = false;
                btnDownload.Visible = false;
            }
            else if (iFormType == 27)
            {

                this.Text = "EMP TOUR BILL DETAILS:: ACTIVITY WISE";
                txtEcode.Visible = false;
                cbRepType.Visible = false;
                lblEcode.Visible = false;
                btnDownload.Visible = false;
            }
            else if (iFormType == 28)
            {
                this.Text = "Stationary Emp Indent Details";
                cbRepType.Visible = false;
                lblEcode.Visible = true;
                txtEcode.Visible = true;

            }
            else if (iFormType == 29)
            {
                this.Text = "Planning Training Program Details";
                cbRepType.Visible = false;
                btnDownload.Visible = false;
            }
            else if (iFormType == 30)
            {
                this.Text = "Training Programs Details";
                cbRepType.Visible = false;
                btnDownload.Visible = false;
            }
            else if (iFormType == 31)
            {
                this.Text = "% Stock Return Employee Wise";
                //lblEcode.Visible = false;
                //txtEcode.Visible = false;
                cbRepType.Visible = false;
                dtpFromDate.CustomFormat = "MMMyyyy";
                dtpToDate.CustomFormat = "MMMyyyy";
            }
            else if (iFormType == 32)
            {
                txtEcode.Focus();
                this.Text = "Field Support Report";
                cbRepType.Visible = false;
                dtpToDate.Visible = false;
                label3.Text = "From Month";
                label1.Visible = false;
                dtpFromDate.CustomFormat = "MMMyyyy";
                dtpToDate.CustomFormat = "MMMyyyy";
                btnDownload.Visible = true;
            }
            else if (iFormType == 33)
            {
                this.Text = "Branch Attendance";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
            }
            else if (iFormType == 34)
            {
                this.Text = "Audit Points";
                cbRepType.Visible = false;
                label3.Text = "From Month";
                dtpFromDate.CustomFormat = "MMMyyyy";
                dtpToDate.CustomFormat = "MMMyyyy";
                btnDownload.Visible = true;
            }
            else if (iFormType == 35)
            {
                lblDempType.Visible = true;
                chkDemoType.Visible = true;
                lblDempType.Text = "Logical Branches";
                this.Text = "Advance Refund Register";
                lblEcode.Visible = false;
                txtEcode.Visible = false;
                cbRepType.Visible = false;
                FillLogBranchesToList();
            }
            else if (iFormType == 36)
            {
                lblDempType.Visible = false;
                chkDemoType.Visible = false;
                this.Text = "Ao Wise Replacement Register";
                //lblEcode.Visible = false;
                //txtEcode.Visible = false;
                cbRepType.Visible = false;
                dtpFromDate.CustomFormat = "MMMyyyy";
                dtpToDate.CustomFormat = "MMMyyyy";
            }
            else if (iFormType == 37)
            {
                lblDempType.Visible = false;
                chkDemoType.Visible = false;
                this.Text = "Emp Daily Attendance Report";
                //lblEcode.Visible = false;
                //txtEcode.Visible = false;
                cbRepType.Visible = false;
                dtpFromDate.CustomFormat = "dd/MMM/yyyy";
                dtpToDate.CustomFormat = "dd/MMM/yyyy";
                txtEcode.Focus();
            }


            else
            {
                this.Text = "Recruitement By Individuals";
                lblEcode.Visible = true;
                txtEcode.Visible = true;
                cbRepType.Visible = false;
            }
            if (iFormType == 2 || iFormType == 11 || iFormType == 19 || iFormType == 32 || iFormType == 34 || iFormType == 35 || iFormType==24 || iFormType==25 || iFormType==37)
            {
                btnDownload.Visible = true;
            }
            else
            {
                btnDownload.Visible = false;
            }
            dtpFromDate.Value = Convert.ToDateTime(CommonData.CurrentDate);
            dtpToDate.Value = Convert.ToDateTime(CommonData.CurrentDate);

        }
        private void FillReportType()
        {

            if (iFormType == 16)
            {
                //NewCheckboxListItem oclBox = new NewCheckboxListItem();
                //oclBox.Tag = "DEPT & EMP WISE SUMMARY";
                //oclBox.Text = "DEPT & EMP WISE SUMMARY";
                //cbRepType.Items.Add(oclBox);
                //oclBox = null;

                DataTable dt = new DataTable();
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("name", typeof(string));

                dt.Rows.Add("DEPT & EMP WISE SUMMARY", "DEPT & EMP WISE SUMMARY");
                dt.Rows.Add("DEPT WISE SUMMARY", "DEPT WISE SUMMARY");

                cbRepType.DataSource = dt;
                cbRepType.DisplayMember = "name";
                cbRepType.ValueMember = "type";
                cbRepType.SelectedIndex = 0;
            }
            else
            {

            }
                
        }
        private void FillLogBranchesToList()
        {
            objDB = new SQLDB();
            string sqlText = "";
            chkDemoType.Items.Clear();
            DataTable dt = new DataTable();
            sqlText = "SELECT DISTINCT LGM_LOG_BRANCH_CODE,LOG_BRANCH_NAME FROM LevelGroup_map " +
                " INNER JOIN BRANCH_MAS_LOGICAL  ON LOG_BRANCH_CODE=LGM_LOG_BRANCH_CODE " +
                " WHERE LGM_BRANCH_CODE='" + CommonData.BranchCode +
                // "' AND LGM_DOC_MONTH='" + "" + dtpDocMonth.Value.ToString("MMMyyyy") + 
               "' AND ACTIVE='T'";
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            GetSelectedLogicalBranches();
            if (iFormType == 1)
            {
                CommonData.ViewReport = "EMPLOYEE_SALE_PERFORMANCE_INDIVIDUALS";
                childReportViewer = new ReportViewer(txtEcode.Text, txtEcode.Text, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "");
                childReportViewer.Show();
            }
            else if (iFormType == 2)
            {
                CommonData.ViewReport = "ADVANCE_REFUND_REGISTER";
                childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "");
                childReportViewer.Show();
            }
            else if (iFormType == 3)
            {
                try { Convert.ToInt32(txtEcode.Text); }
                catch { txtEcode.Text = "0"; }
                CommonData.ViewReport = "SP_DC_BY_INDIVIDUALS";
                childReportViewer = new ReportViewer(CommonData.BranchCode.ToString(), txtEcode.Text, dtpFromDate.Value.ToString("dd-MMM-yyyy").ToUpper(), dtpToDate.Value.ToString("dd-MMM-yyyy").ToUpper(), "DETAILED");
                childReportViewer.Show();
            }
            else if (iFormType == 4)
            {
                CommonData.ViewReport = "STOCKPOINT_STOCK_RECONSILATION_BYDATE";
                ReportViewer objReportview = new ReportViewer("BYDATE", CommonData.BranchCode.ToString(), dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper());
                objReportview.Show();
            }
            else if (iFormType == 5)
            {
                CommonData.ViewReport = "BRANCH_WISE_APPLICATIONS_STATUS_SUMMARY";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(),"");
                objReportview.Show();
            }
            else if (iFormType == 6)
            {
                CommonData.ViewReport = "BRANCHWISE_STATIONARY_RECONSILATION_SUMMARY";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "");
                objReportview.Show();
            }
            else if (iFormType == 7)
            {
                CommonData.ViewReport = "BRANCHWISE_STATIONARY_ISSUE_REGISTER";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "");
                objReportview.Show();
            }
            else if (iFormType == 8)
            {
                CommonData.ViewReport = "BRANCHWISE_STATIONARY_INDENT_REGISTER";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "");
                objReportview.Show();
            }
            else if (iFormType == 9)
            {
                CommonData.ViewReport = "PRODUCTION_UNIT_FINISHED_GOODS_REGISTER";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "");
                objReportview.Show();
            }
            else if (iFormType == 10)
            {
                CommonData.ViewReport = "STOCK_POINT_STOCK_LEDGER";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, "LEDGER", dtpFromDate.Value.ToString("MMMyyyy").ToUpper(), dtpToDate.Value.ToString("MMMyyyy").ToUpper());
                objReportview.Show();
            }
            else if (iFormType == 11)
            {
                CommonData.ViewReport = "STOCK_POINT_STOCK_SUMMARY";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, "SUMMARY", dtpFromDate.Value.ToString("MMMyyyy").ToUpper(), dtpToDate.Value.ToString("MMMyyyy").ToUpper());
                objReportview.Show();
            }
            else if (iFormType == 12)
            {
                CommonData.ViewReport = "SSCRM_REP_ID_PREPARATIONS";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, "", dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper());
                objReportview.Show();
            }
            else if (iFormType == 13)
            {
                CommonData.ViewReport = "SSCRM_REP_PU_DCST_REG_DETL";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, "", dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(),"DC");
                objReportview.Show();
            }
            else if (iFormType == 23)
            {
                CommonData.ViewReport = "SSCRM_REP_PU_DCST_REG_DETL";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, "", dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "DCR");
                objReportview.Show();
            }
            else if (iFormType == 14)
            {
                CommonData.ViewReport = "SSCRM_REP_PU_DCST_REG_DETL";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, "", dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(),"DCST");
                objReportview.Show();
            }
            else if (iFormType == 15)
            {
                CommonData.ViewReport = "SSCRM_REP_APPROVALS_BY_DATE";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, "", dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "BYDATE");
                objReportview.Show();
            }
            else if (iFormType == 16)
            {
                if (cbRepType.SelectedIndex == 0)
                {
                    CommonData.ViewReport = "HR_REP_LATE_COMMINGS";
                    ReportViewer objReportview = new ReportViewer("", "0", "", dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "");
                    objReportview.Show();
                }
                else if (cbRepType.SelectedIndex == 1)
                {
                    CommonData.ViewReport = "HR_REP_LATE_COMMINGS_SUMMARY";
                    ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, "", dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "");
                    objReportview.Show();
                }
            }
            else if (iFormType == 17)
            {
                CommonData.ViewReport = "DL_REP_ORDER_BOOKING_REG";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, 0, 0, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(),"");
                objReportview.Show();
            }
            else if (iFormType == 18)
            {
                CommonData.ViewReport = "SSCRM_REP_PU_GRN_REG_DETL";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, 0, 0, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "");
                objReportview.Show();
            }
            else if (iFormType == 19)
            {
                
            }
            else if (iFormType == 20)
            {
                try { Convert.ToInt32(txtEcode.Text); }
                catch { txtEcode.Text = "0"; }
                CommonData.ViewReport = "SP_GRN_BY_INDIVIDUALS";
                childReportViewer = new ReportViewer(CommonData.BranchCode.ToString(), txtEcode.Text, dtpFromDate.Value.ToString("dd-MMM-yyyy").ToUpper(), dtpToDate.Value.ToString("dd-MMM-yyyy").ToUpper(), "GRN");
                childReportViewer.Show();
            }
            else if (iFormType == 21)
            {
                CommonData.ViewReport = "BRANCHWISE_STATIONARY_GRN_REGISTER";
                ReportViewer objReportview = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("dd-MMM-yyyy").ToUpper(), dtpToDate.Value.ToString("dd-MMM-yyyy").ToUpper(), "");
                objReportview.Show();
            }
            else if (iFormType == 0)
            {
                CommonData.ViewReport = "RECRUITMENT_REPORT_BY_INDIVIDUALS";
                childReportViewer = new ReportViewer(CommonData.BranchCode.ToString(), txtEcode.Text, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "");
                childReportViewer.Show();
            }

            else if (iFormType == 22)
            {
                CommonData.ViewReport = "SERVICES_ACTIVITY_REGISTER";
                childReportViewer = new ReportViewer(CommonData.CompanyCode,CommonData.BranchCode,dtpFromDate.Value.ToString("dd/MMM/yyyy"), dtpToDate.Value.ToString("dd/MMM/yyyy"), "DETAILED");
                childReportViewer.Show();
            }
            else if (iFormType == 24)
            {
                CommonData.ViewReport = "SSERP_REP_BRANCH_WISE_NEW_SR_JOININGS";
                childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "NEW SRS");
                childReportViewer.Show();
            }
            else if (iFormType == 25)
            {
                CommonData.ViewReport = "SSERP_REP_BRANCH_WISE_NEW_SR_LEFT";
                childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "LEFT");
                childReportViewer.Show();
            }
            else if (iFormType == 26)
            {
                CommonData.ViewReport = "SSCRM_REP_SPECIAL_APPROVALS_REGISTER";
                childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("dd/MMM/yyyy"), dtpToDate.Value.ToString("dd/MMM/yyyy"), "");
                childReportViewer.Show();
            }
            else if (iFormType == 27)
            {
                CommonData.ViewReport = "SSCRM_REP_EMP_TOUR_BILL_DETL_ACTIVITY_WISE";
                childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("dd/MMM/yyyy"), dtpToDate.Value.ToString("dd/MMM/yyyy"), "SUMMARY");
                childReportViewer.Show();
            }

            else if (iFormType == 28)
            {
                if (txtEcode.Text.Length != 0)
                {
                    try { Convert.ToInt32(txtEcode.Text); }
                    catch { txtEcode.Text = "0"; }
                    CommonData.ViewReport = "SSCRM_REP_EMPLOYEE_STATIONARY_INDENT";
                    childReportViewer = new ReportViewer("", "", txtEcode.Text, dtpFromDate.Value.ToString("dd/MMM/yyyy"), dtpToDate.Value.ToString("dd/MMM/yyyy"), "");
                    childReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("Please Enter Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (iFormType == 29)
            {
                if (txtEcode.Text.Length != 0)
                {
                    CommonData.ViewReport = "TRAINER_PROGRAM_DETAILS";
                    childReportViewer = new ReportViewer("", "", txtEcode.Text, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "PLANNING_PROGRAMS");
                    childReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("Please Enter Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (iFormType == 30)
            {
                if (txtEcode.Text.Length != 0)
                {
                    CommonData.ViewReport = "ACTUAL_TRAINING_PROGRAM_DETAILS";
                    childReportViewer = new ReportViewer("", "", txtEcode.Text, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "ACTUAL_PROGRAMS");
                    childReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("Please Enter Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (iFormType == 31)
            {
                if (txtEcode.Text.Length != 0)
                {
                    CommonData.ViewReport = "SSERP_STK_RETURN_GL_IND";
                    childReportViewer = new ReportViewer(txtEcode.Text, dtpFromDate.Value.ToString("MMMyyyy").ToUpper(), dtpToDate.Value.ToString("MMMyyyy").ToUpper(), "GL SUMM");
                    childReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("Please Enter Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (iFormType == 32)
            {
                if (txtEcode.Text.Length != 0)
                {
                    CommonData.ViewReport = "SSERP_REP_EMP_WISE_FIELD_SUPPORT_DETAILS";
                    childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("MMMyyyy").ToUpper(), dtpFromDate.Value.ToString("MMMyyyy").ToUpper(), txtEcode.Text, "EMP_WISE");
                    childReportViewer.Show();
                }
                else
                {
                    MessageBox.Show("Please Enter Ecode", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (iFormType == 33)
            {
                ReportViewer childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, CommonData.DocMonth, dtpFromDate.Value.ToString("dd-MMM-yyyy"), dtpToDate.Value.ToString("dd-MMM-yyyy"), "WAGEATTD");
                CommonData.ViewReport = "SSERP_REP_HR_BR_ATTD_REG";
                childReportViewer.Show();
            }
            else if (iFormType == 34)
            {
                CommonData.ViewReport = "SSCRM_REP_AUDIT_QUERY_REGISTER";
                childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("MMMyyyy").ToUpper(), dtpToDate.Value.ToString("MMMyyyy").ToUpper(), txtEcode.Text, "ALL", "", "", "", "", "ALL", "MISCONDUCT_EMP_WISE");
                childReportViewer.Show();
            }
            else if (iFormType == 35)
            {
                CommonData.ViewReport = "SSCRM_REP_ADVANCE_REFUND_LOGICALBRANCH";
                childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode,strLogicalBranches, dtpFromDate.Value.ToString("MMMyyyy").ToUpper(),dtpToDate.Value.ToString("MMMyyyy").ToUpper(),"");
                childReportViewer.Show();
            }
            else if (iFormType == 36)
            {
                if (txtEcode.Text.Length == 0)
                {
                    txtEcode.Text = "0";
                }

                CommonData.ViewReport = "SSERP_REP_AO_WISE_REPLACEMENT_REG";
                childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, txtEcode.Text, dtpFromDate.Value.ToString("MMMyyyy").ToUpper(), dtpToDate.Value.ToString("MMMyyyy").ToUpper(), "");
                childReportViewer.Show();

            }
            else if (iFormType == 37)
            {
                ReportViewer childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, "0",txtEcode.Text,dtpFromDate.Value.ToString("dd-MMM-yyyy"), dtpToDate.Value.ToString("dd-MMM-yyyy"), "EMP_DETL_ATTD");
                CommonData.ViewReport = "EMP_WISE_DAILY_ATTD";
                childReportViewer.Show();
            }
        }

      
        private void btnDownload_Click(object sender, EventArgs e)
        {
            objUtilityDB = new UtilityDB();
            int iTotColumns = 0;
            int iColumnStart = 1;
            int iRowStart = 5;
            GetSelectedLogicalBranches();

            #region FormID 2
            if (iFormType == 2)
            {
                objExDb = new ExcelDB();
                DataTable dtExcel = objExDb.GetAdvanceRefundRegister(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("dd-MMM-yyyy"), dtpToDate.Value.ToString("dd-MMM-yyyy"),"").Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;

                        Excel.Range rg = worksheet.get_Range("A1", "O1");
                        Excel.Range rgData = worksheet.get_Range("A2", "O" + (dtExcel.Rows.Count + 1).ToString());
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
                        rg.Cells.Value2 = "GLName";

                        rg = worksheet.get_Range("G1", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "TrnNo";

                        rg = worksheet.get_Range("H1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "RefundDate";

                        rg = worksheet.get_Range("I1", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "RecievedBy";

                        rg = worksheet.get_Range("J1", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Cells.Value2 = "Address";

                        rg = worksheet.get_Range("K1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "RefPhNo";

                        rg = worksheet.get_Range("L1", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg.Cells.Value2 = "PayMode";

                        rg = worksheet.get_Range("M1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "AdvanceAmount";

                        rg = worksheet.get_Range("N1", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "RefundAmount";

                        rg = worksheet.get_Range("O1", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Cells.Value2 = "Remarks";


                        int RowCounter = 1;

                        foreach (DataRow dr in dtExcel.Rows)
                        {
                            int i = 1;
                            worksheet.Cells[RowCounter + 1, i++] = RowCounter;
                            worksheet.Cells[RowCounter + 1, i++] = dr["ar_orderno"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ar_number"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = Convert.ToDateTime(dr["ar_orderdt"].ToString()).ToString("dd-MMM-yyyy");
                            worksheet.Cells[RowCounter + 1, i++] = dr["ar_srCode"].ToString() + "-" + dr["ar_srName"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ar_glcode"].ToString() + "-" + dr["ar_glName"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ar_ref_trn_No"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = Convert.ToDateTime(dr["ar_refund_dt"].ToString()).ToString("dd-MMM-yyyy");
                            worksheet.Cells[RowCounter + 1, i++] = dr["ar_ref_recBy"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ar_ref_recByAdd"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ar_ref_phone"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ar_payMode"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ar_order_adv"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ar_refundAmt"].ToString();
                            worksheet.Cells[RowCounter + 1, i++] = dr["ar_remarks"].ToString();

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

            #region FormID 11 :: Stockpoint Stock Summary
            if (iFormType == 11)
            {
                objExDb = new ExcelDB();
                DataTable dtExcel = objExDb.GetStockPointStockSummary(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("dd-MMM-yyyy"), dtpToDate.Value.ToString("dd-MMM-yyyy"), "EXCEL_DOWNLOAD").Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {

                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;
                        iTotColumns = 53;
                        string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                        Excel.Range rg = worksheet.get_Range("A5", sLastColumn + "5");
                        Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString());
                        rgData.Font.Size = 11;
                        //rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;
                        //COMPANY NAME
                        rgData = worksheet.get_Range("A1", "D1");
                        rgData.Merge(Type.Missing); rgData.Value2 = dtExcel.Rows[0]["sp_company_name"];
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; //rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 16;
                        rgData = worksheet.get_Range("A2", "D2");
                        rgData.Merge(Type.Missing); rgData.Value2 = dtExcel.Rows[0]["sp_branch_name"];
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; //rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 16;
                        rgData = worksheet.get_Range("A3", "D4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "Stock Summary From " + dtpFromDate.Value.ToString("dd-MMM-yyyy") + " to " + dtpToDate.Value.ToString("dd-MMM-yyyy");
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; //rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 13;
                        //rgData.Interior.ColorIndex = 36;
                        //string strPath = Application.StartupPath;
                        //strPath = strPath.Replace("\\bin\\Debug", "");
                        //worksheet.Shapes.AddPicture("C:\\csharp-xl-picture.JPG", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 50, 50, 300, 45);
                        rg.Font.Bold = true;
                        rg.Font.Name = "Times New Roman";
                        rg.Font.Size = 10;
                        rg.WrapText = true;
                        rg.ColumnWidth = 7;
                        rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Interior.ColorIndex = 31;
                        rg.Borders.Weight = 2;
                        rg.Borders.LineStyle = Excel.Constants.xlSolid;
                        rg.Cells.RowHeight = 38;

                        rg = worksheet.get_Range("A5", Type.Missing);
                        rg.Cells.ColumnWidth = 4;
                        rg = worksheet.get_Range("B5", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg = worksheet.get_Range("C5", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("D5", Type.Missing);
                        rg.Cells.ColumnWidth = 20;

                        worksheet.Cells[iRowStart, iColumnStart++] = "SlNo";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Category";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Brand";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Product";
                        rgData = worksheet.get_Range("E3", "G4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "Opening Stock\n(" + dtpFromDate.Value.ToString("dd-MMM-yyyy") + ")";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 35;
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        rgData = worksheet.get_Range("H3", "V3");
                        //RECIEPTS
                        rgData.Merge(Type.Missing); rgData.Value2 = "RECIEPTS";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 36;
                        rgData = worksheet.get_Range("H4", "J4");
                        //BR2SP GRN
                        rgData.Merge(Type.Missing); rgData.Value2 = "BR2SP";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 36;
                        //SP2SP GRN
                        rgData = worksheet.get_Range("K4", "M4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "SP2SP";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 36;
                        //OL2SP GRN
                        rgData = worksheet.get_Range("N4", "P4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "OL2SP";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        //PU2SP GRN
                        rgData.Interior.ColorIndex = 36;
                        rgData = worksheet.get_Range("Q4", "S4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "PU2SP";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 36;
                        //TOTAL GRN QTY
                        rgData = worksheet.get_Range("T4", "V4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "TOTAL RECIEPTS";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 36;
                        //DC-DCR
                        rgData = worksheet.get_Range("W3", "AN3");
                        rgData.Merge(Type.Missing); rgData.Value2 = "ISSUES (DC/DCST/DCR)";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 37;
                        rgData = worksheet.get_Range("W4", "Y4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "SP2BR DCR";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 37;
                        rgData = worksheet.get_Range("Z4", "AB4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "SP2BR DC";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 37;
                        //SP2SP DCST
                        rgData = worksheet.get_Range("AC4", "AE4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "SP2SP";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 37;
                        //SP2OL DCST
                        rgData = worksheet.get_Range("AF4", "AH4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "SP2OL";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 37;
                        //SP2PU DCST
                        rgData = worksheet.get_Range("AI4", "AK4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "SP2PU";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 37;
                        //TOTAL ISSUES
                        rgData = worksheet.get_Range("AL4", "AN4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "TOTAL ISSUES";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 37;
                        //INTERNAL CONVERSION
                        rgData = worksheet.get_Range("AO3", "AT3");
                        rgData.Merge(Type.Missing); rgData.Value2 = "INTERNAL CONVERSION";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 38;
                        rgData = worksheet.get_Range("AO4", "AQ4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "ISSUES";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 38;
                        rgData = worksheet.get_Range("AR4", "AT4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "RECIEPT";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 38;
                        //REFILL PROCESS
                        rgData = worksheet.get_Range("AU4", "AU4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "ISSUED";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 39;
                        rgData = worksheet.get_Range("AV4", "AV4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "RECIEPT";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 39;
                        rgData = worksheet.get_Range("AU3", "AV3");
                        rgData.Merge(Type.Missing); rgData.Value2 = "REFILL";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 39;
                        rgData = worksheet.get_Range("AW3", "AX4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "SHORTAGE";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 40;

                        rgData = worksheet.get_Range("AY3", "BA4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "Closing Stock\n(" + dtpToDate.Value.ToString("dd-MMM-yyyy") + ")";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 42;
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";



                        iRowStart++; iColumnStart = 1;
                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            worksheet.Cells[iRowStart, iColumnStart++] = i + 1;
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_category"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_brand_id"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_product_name"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_open_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_open_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_open_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_open_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_br2sp_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_br2sp_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_grn_br2sp_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_grn_br2sp_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_sp2sp_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_sp2sp_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_grn_sp2sp_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_grn_sp2sp_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_ol2sp_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_ol2sp_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_grn_ol2sp_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_grn_ol2sp_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_pu2sp_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_pu2sp_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_grn_pu2sp_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_grn_pu2sp_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_grn_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_grn_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dcr_sp2br_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dcr_sp2br_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_dcr_sp2br_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_dcr_sp2br_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_sp2br_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_sp2br_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_dc_sp2br_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_dc_sp2br_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_sp2sp_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_sp2sp_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_dc_sp2sp_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_dc_sp2sp_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_sp2ol_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_sp2ol_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_dc_sp2ol_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_dc_sp2ol_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_sp2pu_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_sp2pu_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_dc_sp2pu_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_dc_sp2pu_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_dc_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_dc_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_intdcgood"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_intdcbad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_intdcgood"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_intdcbad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_intgrngood"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_intgrnbad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_intgrngood"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_intgrnbad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_refdcbad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_refgrngood"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_shortage_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_shortage_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_clos_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_clos_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_clos_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_clos_bad"]);

                            iRowStart++; iColumnStart = 1;
                        }
                        //CONDITIONAL FORMATING
                        //Excel.Range range = worksheet.get_Range("E6", "BA" + (dtExcel.Rows.Count+5).ToString());
                        //Excel.FormatConditions fcs = range.FormatConditions;
                        //Excel.FormatCondition fc = (Excel.FormatCondition)fcs.Add(Excel.XlFormatConditionType.xlCellValue, Excel.XlFormatConditionOperator.xlLess, "$BA$6<0", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        //fc.Font.ColorIndex = 45;


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Download incomplete! \n" + ex.ToString(), "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Data not found", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion

            #region FormID 19 :: Production Stock Summary
            if (iFormType == 19)
            {
                objExDb = new ExcelDB();
                DataTable dtExcel = objExDb.GetProductionStockSummary(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("dd-MMM-yyyy"), dtpToDate.Value.ToString("dd-MMM-yyyy"), "EXCEL_DOWNLOAD").Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {

                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;
                        iTotColumns = 54;
                        string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                        Excel.Range rg = worksheet.get_Range("A5", sLastColumn + "5");
                        Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString());
                        rgData.Font.Size = 11;
                        //rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;
                        //COMPANY NAME
                        rgData = worksheet.get_Range("A1", "D1");
                        rgData.Merge(Type.Missing); rgData.Value2 = dtExcel.Rows[0]["sp_company_name"];
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; //rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 16;
                        rgData = worksheet.get_Range("A2", "D2");
                        rgData.Merge(Type.Missing); rgData.Value2 = dtExcel.Rows[0]["sp_branch_name"];
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; //rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 16;
                        rgData = worksheet.get_Range("A3", "D4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "Stock Summary From " + dtpFromDate.Value.ToString("dd-MMM-yyyy") + " to " + dtpToDate.Value.ToString("dd-MMM-yyyy");
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; //rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 13;
                        //rgData.Interior.ColorIndex = 36;
                        //string strPath = Application.StartupPath;
                        //strPath = strPath.Replace("\\bin\\Debug", "");
                        //worksheet.Shapes.AddPicture("C:\\csharp-xl-picture.JPG", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 50, 50, 300, 45);
                        rg.Font.Bold = true;
                        rg.Font.Name = "Times New Roman";
                        rg.Font.Size = 10;
                        rg.WrapText = true;
                        rg.ColumnWidth = 7;
                        rg.Font.ColorIndex = 2; // White Color : 2 and Red = 3,30; Green = 10,43; 
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg.Interior.ColorIndex = 31;
                        rg.Borders.Weight = 2;
                        rg.Borders.LineStyle = Excel.Constants.xlSolid;
                        rg.Cells.RowHeight = 38;

                        rg = worksheet.get_Range("A5", Type.Missing);
                        rg.Cells.ColumnWidth = 4;
                        rg = worksheet.get_Range("B5", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg = worksheet.get_Range("C5", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("D5", Type.Missing);
                        rg.Cells.ColumnWidth = 20;

                        worksheet.Cells[iRowStart, iColumnStart++] = "SlNo";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Category";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Brand";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Product";
                        rgData = worksheet.get_Range("E3", "G4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "Opening Stock\n(" + dtpFromDate.Value.ToString("dd-MMM-yyyy") + ")";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 35;
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        rgData = worksheet.get_Range("H3", "H4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "Production";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 43;
                        rgData = worksheet.get_Range("I3", "W3");
                        //RECIEPTS
                        rgData.Merge(Type.Missing); rgData.Value2 = "RECIEPTS";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 36;
                        rgData = worksheet.get_Range("I4", "K4");
                        //BR2SP GRN
                        rgData.Merge(Type.Missing); rgData.Value2 = "BR2PU";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 36;
                        //SP2SP GRN
                        rgData = worksheet.get_Range("L4", "N4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "SP2PU";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 36;
                        //OL2SP GRN
                        rgData = worksheet.get_Range("O4", "Q4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "OL2PU";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        //PU2SP GRN
                        rgData.Interior.ColorIndex = 36;
                        rgData = worksheet.get_Range("R4", "T4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "PU2PU";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 36;
                        //TOTAL GRN QTY
                        rgData = worksheet.get_Range("U4", "W4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "TOTAL RECIEPTS";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 36;
                        //DC-DCR
                        rgData = worksheet.get_Range("X3", "AO3");
                        rgData.Merge(Type.Missing); rgData.Value2 = "ISSUES (DC/DCST/DCR)";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 37;
                        rgData = worksheet.get_Range("X4", "Z4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "PU2BR DCR";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 37;
                        rgData = worksheet.get_Range("AA4", "AC4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "PU2BR DC";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 37;
                        //SP2SP DCST
                        rgData = worksheet.get_Range("AD4", "AF4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "PU2SP";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 37;
                        //SP2OL DCST
                        rgData = worksheet.get_Range("AG4", "AI4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "PU2OL";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 37;
                        //SP2PU DCST
                        rgData = worksheet.get_Range("AJ4", "AL4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "PU2PU";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 37;
                        //TOTAL ISSUES
                        rgData = worksheet.get_Range("AM4", "AO4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "TOTAL ISSUES";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 37;
                        //INTERNAL CONVERSION
                        rgData = worksheet.get_Range("AP3", "AU3");
                        rgData.Merge(Type.Missing); rgData.Value2 = "INTERNAL CONVERSION";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 38;
                        rgData = worksheet.get_Range("AP4", "AR4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "ISSUES";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 38;
                        rgData = worksheet.get_Range("AS4", "AU4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "RECIEPT";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 38;
                        //REFILL PROCESS
                        rgData = worksheet.get_Range("AV4", "AV4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "ISSUED";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 39;
                        rgData = worksheet.get_Range("AW4", "AW4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "RECIEPT";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 39;
                        rgData = worksheet.get_Range("AV3", "AW3");
                        rgData.Merge(Type.Missing); rgData.Value2 = "REFILL";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 39;
                        rgData = worksheet.get_Range("AX3", "AY4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "SHORTAGE";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 40;

                        rgData = worksheet.get_Range("AZ3", "BB4");
                        rgData.Merge(Type.Missing); rgData.Value2 = "Closing Stock\n(" + dtpToDate.Value.ToString("dd-MMM-yyyy") + ")";
                        rgData.Borders.Weight = 2; rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter; rgData.HorizontalAlignment = Excel.Constants.xlCenter;
                        rgData.Font.Bold = true; rgData.Font.Size = 12;
                        rgData.Interior.ColorIndex = 42;
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Good";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Damage";
                        worksheet.Cells[iRowStart, iColumnStart++] = "Total";



                        iRowStart++; iColumnStart = 1;
                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            worksheet.Cells[iRowStart, iColumnStart++] = i + 1;
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_category"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_brand_id"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_product_name"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_open_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_open_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_open_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_open_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_packing_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_br2sp_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_br2sp_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_grn_br2sp_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_grn_br2sp_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_sp2sp_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_sp2sp_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_grn_sp2sp_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_grn_sp2sp_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_ol2sp_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_ol2sp_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_grn_ol2sp_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_grn_ol2sp_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_pu2sp_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_pu2sp_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_grn_pu2sp_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_grn_pu2sp_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_grn_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_grn_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_grn_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dcr_sp2br_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dcr_sp2br_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_dcr_sp2br_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_dcr_sp2br_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_sp2br_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_sp2br_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_dc_sp2br_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_dc_sp2br_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_sp2sp_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_sp2sp_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_dc_sp2sp_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_dc_sp2sp_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_sp2ol_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_sp2ol_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_dc_sp2ol_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_dc_sp2ol_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_sp2pu_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_sp2pu_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_dc_sp2pu_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_dc_sp2pu_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_dc_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_dc_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_dc_bad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_intdcgood"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_intdcbad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_intdcgood"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_intdcbad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_intgrngood"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_intgrnbad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_intgrngood"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_intgrnbad"]);
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_refdcbad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_refgrngood"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_shortage_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_shortage_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_clos_good"];
                            worksheet.Cells[iRowStart, iColumnStart++] = dtExcel.Rows[i]["sp_clos_bad"];
                            worksheet.Cells[iRowStart, iColumnStart++] = Convert.ToDouble(dtExcel.Rows[i]["sp_clos_good"]) + Convert.ToDouble(dtExcel.Rows[i]["sp_clos_bad"]);

                            iRowStart++; iColumnStart = 1;
                        }
                        //CONDITIONAL FORMATING
                        //Excel.Range range = worksheet.get_Range("E6", "BA" + (dtExcel.Rows.Count+5).ToString());
                        //Excel.FormatConditions fcs = range.FormatConditions;
                        //Excel.FormatCondition fc = (Excel.FormatCondition)fcs.Add(Excel.XlFormatConditionType.xlCellValue, Excel.XlFormatConditionOperator.xlLess, "$BA$6<0", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        //fc.Font.ColorIndex = 45;


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Download incomplete! \n" + ex.ToString(), "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Data not found", "SSERP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion

            #region FormID 32
            if (iFormType == 32)
            {
                string strHead = "";
                objExDb = new ExcelDB();
                DataTable dtExcel = objExDb.Get_FieldSupportDetails(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("MMMyyyy").ToUpper(), dtpFromDate.Value.ToString("MMMyyyy").ToUpper(), txtEcode.Text, "").Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;
                        iTotColumns = 9;

                        string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                        Excel.Range rgHead = null;
                        Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                        Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 4).ToString());
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;

                        rgData = worksheet.get_Range("A1", "I2");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 16;
                        rgData.Value2 = dtExcel.Rows[0]["sr_company_name"].ToString() + " \n FIELD SUPPORT REPORT";
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.HorizontalAlignment = Excel.Constants.xlCenter;

                        rgData.ColumnWidth = 20;
                        rgData.RowHeight = 20;
                        rgData.Font.ColorIndex = 31;
                        rgData = worksheet.get_Range("A3", "I3");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 13;

                        strHead = " Branch : " + dtExcel.Rows[0]["sr_branch_name"].ToString() +
                                    "\n Field Support By : " + dtExcel.Rows[0]["sr_Field_Sup_Emp_Name"].ToString()
                                  + "                                                                                                                           Doc Month : " + dtExcel.Rows[0]["sr_document_month"].ToString();
                        strHead = strHead.TrimEnd(',');
                        rgData.Value2 = strHead;
                        rgData.Font.ColorIndex = 13;
                        rgData.ColumnWidth = 200;
                        rgData.RowHeight = 50;

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

                        rg = worksheet.get_Range("A4", Type.Missing);
                        rg.Cells.ColumnWidth = 4;
                        rg = worksheet.get_Range("B4", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("C4", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("D4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("E4", Type.Missing);
                        rg.Cells.ColumnWidth = 35;
                        rg = worksheet.get_Range("F4", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg = worksheet.get_Range("G4", Type.Missing);
                        rg.Cells.ColumnWidth = 40;
                        rg = worksheet.get_Range("H4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("I4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;


                        int iColumn = 1, iStartRow = 4;
                        worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                        worksheet.Cells[iStartRow, iColumn++] = "Order No";
                        worksheet.Cells[iStartRow, iColumn++] = "Invoice No";
                        worksheet.Cells[iStartRow, iColumn++] = "Order Date";
                        worksheet.Cells[iStartRow, iColumn++] = "Customer Name";
                        worksheet.Cells[iStartRow, iColumn++] = "Name Of The Executive";
                        worksheet.Cells[iStartRow, iColumn++] = "Product Name";
                        worksheet.Cells[iStartRow, iColumn++] = "No.Of Points";
                        worksheet.Cells[iStartRow, iColumn++] = "Amount";

                        int iRow = 5; iColumn = 1;
                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            int iMerg = 1;
                            if (i == 0)
                            {
                                iMerg = Convert.ToInt32(dtExcel.Rows[i]["sr_No_Of_Products"]);
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

                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_sl_No"];
                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_order_number"];
                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_invoice_number"];
                                if (dtExcel.Rows[i]["sr_order_date"].ToString() != "" && dtExcel.Rows[i]["sr_order_date"].ToString() != null)
                                    worksheet.Cells[iRow, iColumn++] = Convert.ToDateTime(dtExcel.Rows[i]["sr_order_date"]).ToString("dd/MMM/yyyy");
                                else
                                    worksheet.Cells[iRow, iColumn++] = "";
                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_cnv_farmer_name"];
                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_eora_name"];
                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_prod_name"];
                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_product_points"];
                                worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_amt"];

                            }
                            else
                            {
                                if (dtExcel.Rows[i]["sr_order_number"].ToString() != dtExcel.Rows[i - 1]["sr_order_number"].ToString())
                                {
                                    iMerg = Convert.ToInt32(dtExcel.Rows[i]["sr_No_Of_Products"]);
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


                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_sl_No"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_order_number"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_invoice_number"];
                                    if (dtExcel.Rows[i]["sr_order_date"].ToString() != "" && dtExcel.Rows[i]["sr_order_date"].ToString() != null)
                                        worksheet.Cells[iRow, iColumn++] = Convert.ToDateTime(dtExcel.Rows[i]["sr_order_date"]).ToString("dd/MMM/yyyy");
                                    else
                                        worksheet.Cells[iRow, iColumn++] = "";
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_cnv_farmer_name"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_eora_name"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_prod_name"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_product_points"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_amt"];
                                }
                                else
                                {
                                    iColumn = 7;
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_prod_name"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_product_points"];
                                    worksheet.Cells[iRow, iColumn++] = dtExcel.Rows[i]["sr_amt"];
                                }
                            }

                            iColumn = 1; iRow++;
                        }

                        iRow = 8;
                        iColumn = iRow;
                        rgHead = worksheet.get_Range("H" + (Convert.ToInt32(dtExcel.Rows[0]["sr_no_of_cust"]) + 5).ToString(),
                                               "I" + (Convert.ToInt32(dtExcel.Rows[0]["sr_no_of_cust"]) + 5).ToString());
                        //rgHead = worksheet.get_Range("I" + (Convert.ToInt32(dtExcel.Rows[0]["sr_no_of_cust"]) + 5).ToString(),
                        //                       objUtilityDB.GetColumnName(iRow) + (Convert.ToInt32(dtExcel.Rows[0]["sr_no_of_cust"]) + 5).ToString());

                        rgHead.Borders.Weight = 2;
                        rgHead.Font.Size = 12; rgHead.Font.Bold = true;


                        for (int iMonths = 0; iMonths <= Convert.ToInt32(dtExcel.Rows[0]["sr_no_of_cust"]); iMonths++)
                        {
                            iRow = 8; iColumn = iRow;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_no_of_cust"]) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_no_of_cust"]) + 4).ToString() + ")";
                            iColumn = iColumn + 1;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows[0]["sr_no_of_cust"]) + 5, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows[0]["sr_no_of_cust"]) + 4).ToString() + ")";
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

            #region "iFrmType == 34 :: Audit Query Register"
            if (iFormType == 34)
            {

                try
                {
                    objExDb = new ExcelDB();
                    DataTable dtExcel = objExDb.GetAuditQueryReg(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("MMMyyyy").ToUpper(), dtpToDate.Value.ToString("MMMyyyy").ToUpper(), txtEcode.Text, "ALL", "", "", "", "", "ALL", "MISCONDUCT_EMP_WISE").Tables[0];
                    objExDb = null;


                    if (dtExcel.Rows.Count > 0)
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;
                        iTotColumns = 25;
                        string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                        Excel.Range rgHead = null;
                        Excel.Range rg = worksheet.get_Range("A4", sLastColumn + "4");
                        Excel.Range rgData = worksheet.get_Range("A5", sLastColumn + (dtExcel.Rows.Count + 4).ToString());
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;

                        rgData = worksheet.get_Range("A1", "K2");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 16;
                        rgData.Value2 = "AUDIT MAJOR POINTS REGISTER";
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
                        rg.Cells.ColumnWidth = 5;
                        rg = worksheet.get_Range("C4", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("D4", Type.Missing);
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("E4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("F4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("G4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("H4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("I4", Type.Missing);
                        rg.Cells.ColumnWidth = 50;
                        rg = worksheet.get_Range("K4", Type.Missing);
                        rg.Cells.ColumnWidth = 50;
                        rg = worksheet.get_Range("L4", Type.Missing);
                        rg.Cells.ColumnWidth = 50;
                        rg = worksheet.get_Range("M4", Type.Missing);
                        rg.Cells.ColumnWidth = 50;
                        rg = worksheet.get_Range("N4", Type.Missing);
                        rg.Cells.ColumnWidth = 50;
                        rg = worksheet.get_Range("O4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("P4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("Q4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("R4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("S4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("T4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("U3", "W3");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "CONCERN DETAILS";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 11;
                        rg.Interior.ColorIndex = 31; rg.Font.ColorIndex = 2;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;
                        rg = worksheet.get_Range("W4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("X4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("Y4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("D3", Type.Missing);
                        rg.Cells.ColumnWidth = 10;


                        int iColumn = 1, iStartRow = 4;
                        worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                        worksheet.Cells[iStartRow, iColumn++] = "ID";
                        worksheet.Cells[iStartRow, iColumn++] = "Doc Month";
                        worksheet.Cells[iStartRow, iColumn++] = "Visit Month";
                        worksheet.Cells[iStartRow, iColumn++] = "Branch";
                        worksheet.Cells[iStartRow, iColumn++] = "Logical Branch";
                        worksheet.Cells[iStartRow, iColumn++] = "Zone";
                        worksheet.Cells[iStartRow, iColumn++] = "Region";
                        worksheet.Cells[iStartRow, iColumn++] = "Audit Point";
                        worksheet.Cells[iStartRow, iColumn++] = "Dept";
                        worksheet.Cells[iStartRow, iColumn++] = "Explanation By Accounts Head";
                        worksheet.Cells[iStartRow, iColumn++] = "Explanation By Sales Head";
                        worksheet.Cells[iStartRow, iColumn++] = "Explanation By Service Head";
                        worksheet.Cells[iStartRow, iColumn++] = "Explanation By Unit Head";
                        worksheet.Cells[iStartRow, iColumn++] = "Status";
                        worksheet.Cells[iStartRow, iColumn++] = "Unsolved Reason";
                        worksheet.Cells[iStartRow, iColumn++] = "Deviation";
                        worksheet.Cells[iStartRow, iColumn++] = "Sub Deviation";
                        worksheet.Cells[iStartRow, iColumn++] = "Deviation Amount";
                        worksheet.Cells[iStartRow, iColumn++] = "Recovered Amount";
                        worksheet.Cells[iStartRow, iColumn++] = "Ecode";
                        worksheet.Cells[iStartRow, iColumn++] = "Name";
                        worksheet.Cells[iStartRow, iColumn++] = "Designation";
                        worksheet.Cells[iStartRow, iColumn++] = "AuditBy";
                        worksheet.Cells[iStartRow, iColumn++] = "Misconduct";


                        iStartRow++; iColumn = 1;
                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            worksheet.Cells[iStartRow, iColumn++] = i + 1;
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_query_id"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_doc_month"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_visit_month"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_branch_name"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_LogBranch"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_zone"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_region"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_Audit_point"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_dept"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_exp_HAcc"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_exp_Hsales"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_exp_Hservice"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_exp_Others"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_status"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_reason"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_deviation_type"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_subdeviation_type"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_dev_amt"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_rec_amt"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_concern_ecode"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_concern_emp_name"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_concern_desig"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_audit_ecode"] + "-" + dtExcel.Rows[i]["ad_audit_name"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["ad_misconduct"];

                            iStartRow++; iColumn = 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            #endregion

            #region FormID 35:: Advance Refund Register Logical Branch Wiswe
            if (iFormType == 35)
            {
                objExDb = new ExcelDB();
                DataTable dtExcel = objExDb.GetAdvanceRefundRegisterLOgBranchWise(CommonData.CompanyCode, CommonData.BranchCode, strLogicalBranches, dtpFromDate.Value.ToString("MMMyyyy").ToUpper(), dtpToDate.Value.ToString("MMMyyyy").ToUpper(), "").Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;

                        Excel.Range rg = worksheet.get_Range("A4", "P4");
                        Excel.Range rgData = worksheet.get_Range("A4", "P" + (dtExcel.Rows.Count + 4).ToString());
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


                        rg = worksheet.get_Range("A1", "P1");
                        rg.Merge(Type.Missing);
                        rg.Value2 = "ADVANCE REFUND REGISTER";
                        rg.Font.Bold = true; rg.Font.Size = 14;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlCenter;


                        string Branch = dtExcel.Rows[0]["ar_branch_name"].ToString();

                        rg = worksheet.get_Range("A2", "K2");
                        rg.Merge(Type.Missing);
                        rg.Value2 = " BRANCH NAME :" + " " + Branch +
                                      " \n  From Date :" + dtpFromDate.Value.ToString("MMMyyyy").ToUpper() + " \t To Date : " + dtpToDate.Value.ToString("MMMyyyy").ToUpper();
                        rg.Font.Bold = true; rg.Font.Size = 12;

                        //string LogBranch = dtExcel.Rows[0]["ar_log_branch_name"].ToString();

                        //rg = worksheet.get_Range("A3", "K3");
                        //rg.Merge(Type.Missing);
                        //rg.Value2 = " LOGICAL BRANCH NAME :" + " " + LogBranch;
                        //rg.Font.Bold = true; rg.Font.Size = 12;


                        rg = worksheet.get_Range("A4", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Sl.No";

                        rg = worksheet.get_Range("B4", Type.Missing);
                        rg.Cells.ColumnWidth = 25;
                        rg.Cells.Value2 = "Logical Branch Name";

                        rg = worksheet.get_Range("C4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "OrderNo";

                        rg = worksheet.get_Range("D4", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "ARNo";

                        rg = worksheet.get_Range("E4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Order Date";

                        rg = worksheet.get_Range("F4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "SR Name";

                        rg = worksheet.get_Range("G4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "GL Name";

                        rg = worksheet.get_Range("H4", Type.Missing);
                        rg.Cells.ColumnWidth = 5;
                        rg.Cells.Value2 = "Trn No";

                        rg = worksheet.get_Range("I4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Refund Date";

                        rg = worksheet.get_Range("J4", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg.Cells.Value2 = "Received By";

                        rg = worksheet.get_Range("K4", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Cells.Value2 = "Address";

                        rg = worksheet.get_Range("L4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Ref PhNo";
                        string FromDate = dtpFromDate.Value.ToString("MMMyyyy").ToUpper();

                        //rg = worksheet.get_Range("l2", "O2");
                        //rg.Merge(Type.Missing);
                        //rg.Value2 = "FROM DATE :" + " " + FromDate;  
                        //rg.Font.Bold = true; rg.Font.Size = 12;

                        //string ToDate = dtpToDate.Value.ToString("MMMyyyy").ToUpper();

                        //rg = worksheet.get_Range("l3", "O3");
                        //rg.Merge(Type.Missing);
                        //rg.Value2 = " TO DATE:" + " " + ToDate;
                        //rg.Font.Bold = true; rg.Font.Size = 12;


                        rg = worksheet.get_Range("M4", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg.Cells.Value2 = "Pay Mode";

                        rg = worksheet.get_Range("N4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Advance Amount";

                        rg = worksheet.get_Range("O4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg.Cells.Value2 = "Refund Amount";

                        rg = worksheet.get_Range("P4", Type.Missing);
                        rg.Cells.ColumnWidth = 30;
                        rg.Cells.Value2 = "Remarks";


                        int RowCounter = 1;

                        foreach (DataRow dr in dtExcel.Rows)
                        {
                            int i = 1;
                            worksheet.Cells[RowCounter + 4, i++] = RowCounter;
                            worksheet.Cells[RowCounter + 4, i++] = dr["ar_log_branch_name"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["ar_orderno"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["ar_number"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = Convert.ToDateTime(dr["ar_orderdt"].ToString()).ToString("dd-MMM-yyyy");
                            worksheet.Cells[RowCounter + 4, i++] = dr["ar_srCode"].ToString() + "-" + dr["ar_srName"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["ar_glcode"].ToString() + "-" + dr["ar_glName"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["ar_ref_trn_No"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = Convert.ToDateTime(dr["ar_refund_dt"].ToString()).ToString("dd-MMM-yyyy");
                            worksheet.Cells[RowCounter + 4, i++] = dr["ar_ref_recBy"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["ar_ref_recByAdd"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["ar_ref_phone"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["ar_payMode"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["ar_order_adv"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["ar_refundAmt"].ToString();
                            worksheet.Cells[RowCounter + 4, i++] = dr["ar_remarks"].ToString();

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

            #region FormID 24 :: New SR's Joining
            if (iFormType == 24)
            {
                string strHead = "";
                objExDb = new ExcelDB();
                DataTable dtExcel = objExDb.Get_NewSRJoinings(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "NEW SRS").Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;
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
                        rgData.Value2 = "RECRUITED EMPLOYEES";
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.HorizontalAlignment = Excel.Constants.xlCenter;

                        rgData.ColumnWidth = 20;
                        rgData.RowHeight = 15;
                        rgData.Font.ColorIndex = 25;
                        rgData = worksheet.get_Range("A3", "M3");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 13;

                        strHead = " Company : " + dtExcel.Rows[0]["rs_comp_name"].ToString() + "  \t\t   Branch : " + dtExcel.Rows[0]["rs_branch_name"].ToString() +
                                  "   \t\t   From Date : " + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "  \t\t  To Date : " + dtpToDate.Value.ToString("dd/MMM/yyyy");

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
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("D4", Type.Missing);
                        rg.Cells.ColumnWidth = 25;
                        rg = worksheet.get_Range("E4", Type.Missing);
                        rg.Cells.ColumnWidth = 25;
                        rg = worksheet.get_Range("F4", Type.Missing);
                        rg.Cells.ColumnWidth = 12;
                        rg = worksheet.get_Range("G4", Type.Missing);
                        rg.Cells.ColumnWidth = 12;
                        rg = worksheet.get_Range("H4", Type.Missing);
                        rg.Cells.ColumnWidth = 12;
                        rg = worksheet.get_Range("I4", Type.Missing);
                        rg.Cells.ColumnWidth = 25;
                        rg = worksheet.get_Range("J4", Type.Missing);
                        rg.Cells.ColumnWidth = 25;
                        rg = worksheet.get_Range("K4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("L4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("M4", Type.Missing);
                        rg.Cells.ColumnWidth = 25;


                        int iColumn = 1, iStartRow = 4;
                        worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                        worksheet.Cells[iStartRow, iColumn++] = "Appl No";
                        worksheet.Cells[iStartRow, iColumn++] = "Agent Code";
                        worksheet.Cells[iStartRow, iColumn++] = "Name";
                        worksheet.Cells[iStartRow, iColumn++] = "Father Name";
                        worksheet.Cells[iStartRow, iColumn++] = "DOB";
                        worksheet.Cells[iStartRow, iColumn++] = "DOJ";
                        worksheet.Cells[iStartRow, iColumn++] = "Qualification";
                        worksheet.Cells[iStartRow, iColumn++] = "Recruitment Source";
                        worksheet.Cells[iStartRow, iColumn++] = "Recruited By";
                        worksheet.Cells[iStartRow, iColumn++] = "Approval Status";
                        worksheet.Cells[iStartRow, iColumn++] = "Working Status";
                        worksheet.Cells[iStartRow, iColumn++] = "Remarks";
                        iStartRow++; iColumn = 1;

                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {

                            worksheet.Cells[iStartRow, iColumn++] = (i + 1).ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_appl_no"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_acode"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_emp_name"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_father"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_dob"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_doj"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_qualification"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_recruitement_source_name"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_recby_emp_name"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_approval_status"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_status"];
                            worksheet.Cells[iStartRow, iColumn++] = "";
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

            #region FormID 25 :: Left SR's
            if (iFormType == 25)
            {
                string strHead = "";
                objExDb = new ExcelDB();
                DataTable dtExcel = objExDb.Get_NewSRJoinings(CommonData.CompanyCode, CommonData.BranchCode, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd/MMM/yyyy").ToUpper(), "LEFT").Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;
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
                        rgData.Value2 = "RESIGNED EMPLOYEES";
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.HorizontalAlignment = Excel.Constants.xlCenter;

                        rgData.ColumnWidth = 20;
                        rgData.RowHeight = 15;
                        rgData.Font.ColorIndex = 25;
                        rgData = worksheet.get_Range("A3", "M3");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 13;

                        strHead = " Company : " + dtExcel.Rows[0]["rs_comp_name"].ToString() + "  \t\t   Branch : " + dtExcel.Rows[0]["rs_branch_name"].ToString() +
                                  "   \t\t   From Date : " + dtpFromDate.Value.ToString("dd/MMM/yyyy") + "  \t\t  To Date : " + dtpToDate.Value.ToString("dd/MMM/yyyy");

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
                        rg.Cells.ColumnWidth = 8;
                        rg = worksheet.get_Range("D4", Type.Missing);
                        rg.Cells.ColumnWidth = 25;
                        rg = worksheet.get_Range("E4", Type.Missing);
                        rg.Cells.ColumnWidth = 25;
                        rg = worksheet.get_Range("F4", Type.Missing);
                        rg.Cells.ColumnWidth = 12;
                        rg = worksheet.get_Range("G4", Type.Missing);
                        rg.Cells.ColumnWidth = 12;
                        rg = worksheet.get_Range("H4", Type.Missing);
                        rg.Cells.ColumnWidth = 12;
                        rg = worksheet.get_Range("I4", Type.Missing);
                        rg.Cells.ColumnWidth = 25;
                        rg = worksheet.get_Range("J4", Type.Missing);
                        rg.Cells.ColumnWidth = 25;
                        rg = worksheet.get_Range("K4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("L4", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("M4", Type.Missing);
                        rg.Cells.ColumnWidth = 25;


                        int iColumn = 1, iStartRow = 4;
                        worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                        worksheet.Cells[iStartRow, iColumn++] = "Appl No";
                        worksheet.Cells[iStartRow, iColumn++] = "Agent Code";
                        worksheet.Cells[iStartRow, iColumn++] = "Name";
                        worksheet.Cells[iStartRow, iColumn++] = "Father Name";
                        worksheet.Cells[iStartRow, iColumn++] = "DOB";
                        worksheet.Cells[iStartRow, iColumn++] = "DOJ";
                        worksheet.Cells[iStartRow, iColumn++] = "Qualification";
                        worksheet.Cells[iStartRow, iColumn++] = "Recruitment Source";
                        worksheet.Cells[iStartRow, iColumn++] = "Recruited By";
                        worksheet.Cells[iStartRow, iColumn++] = "Approval Status";
                        worksheet.Cells[iStartRow, iColumn++] = "Working Status";
                        worksheet.Cells[iStartRow, iColumn++] = "Remarks";
                        iStartRow++; iColumn = 1;

                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {

                            worksheet.Cells[iStartRow, iColumn++] = (i + 1).ToString();
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_appl_no"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_acode"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_emp_name"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_father"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_dob"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_doj"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_qualification"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_recruitement_source_name"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_recby_emp_name"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_approval_status"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["rs_status"];
                            worksheet.Cells[iStartRow, iColumn++] = "";
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

            #region FormID 37 :: Emp Wise Daily Attendance
            if (iFormType == 37)
            {

                objExDb = new ExcelDB();
                DataTable dtExcel = objExDb.Get_EmpDailyAttendance(CommonData.CompanyCode, CommonData.BranchCode, txtEcode.Text, dtpFromDate.Value.ToString("dd/MMM/yyyy").ToUpper(), dtpToDate.Value.ToString("dd-MMM-yyyy").ToUpper(), "EMP_DETL_ATTD").Tables[0];
                objExDb = null;
                if (dtExcel.Rows.Count > 0)
                {
                    try
                    {
                        Excel.Application oXL = new Excel.Application();
                        Excel.Workbook theWorkbook = oXL.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                        Excel.Worksheet worksheet = (Excel.Worksheet)oXL.ActiveSheet;
                        oXL.Visible = true;
                        iTotColumns = 8;

                        string sLastColumn = objUtilityDB.GetColumnName(iTotColumns);
                        Excel.Range rgHead = null;
                        Excel.Range rg = worksheet.get_Range("A5", sLastColumn + "5");
                        Excel.Range rgData = worksheet.get_Range("A1", sLastColumn + (dtExcel.Rows.Count + 6).ToString());
                        rgData.Font.Size = 11;
                        rgData.WrapText = true;
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.Borders.Weight = 2;

                        rgData = worksheet.get_Range("A1", "H1");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 16;
                        rgData.Value2 = "Emp Daily Attendance Report";
                        rgData.VerticalAlignment = Excel.Constants.xlCenter;
                        rgData.HorizontalAlignment = Excel.Constants.xlCenter;

                        rgData.ColumnWidth = 20;
                        rgData.RowHeight = 25;
                        rgData.Font.ColorIndex = 30;

                        //rgData = worksheet.get_Range("A2", "H4");
                        //rgData.WrapText = true;
                        //rgData.Merge(Type.Missing);

                        rgData = worksheet.get_Range("A2", "D2");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 11;

                        rgData.Value2 = "Name : " + dtExcel.Rows[0]["HPAM_ENAME"].ToString();

                        rgData = worksheet.get_Range("A3", "D3");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 11;
                        rgData.Value2 = "Desig  : " + dtExcel.Rows[0]["HPAM_DESIGNATION"].ToString();


                        rgData = worksheet.get_Range("A4", "D4");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 11;
                        rgData.Value2 = "Dept   : " + dtExcel.Rows[0]["HPAM_DEPT_NAME"].ToString();


                        rgData = worksheet.get_Range("E2", "G2");
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 11;
                        rgData.Value2 = "DOJ : " + Convert.ToDateTime(dtExcel.Rows[0]["HPAM_DOJ"].ToString()).ToShortDateString();
                        rgData.WrapText = true;

                        rgData = worksheet.get_Range("E3", "G3");
                        rgData.WrapText = true;
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 11;
                        rgData.Value2 = "Company : " + dtExcel.Rows[0]["HPAM_COMPANY_NAME"].ToString();


                        rgData = worksheet.get_Range("E4", "G4");
                        rgData.WrapText = true;
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 11;
                        rgData.Value2 = "Branch : " + dtExcel.Rows[0]["HPAM_BRANCH_NAME"].ToString();

                        rgData = worksheet.get_Range("H2", "H2");
                        rgData.WrapText = true;
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 11;
                        rgData.Value2 = "Present  :  " + dtExcel.Rows[0]["HPAM_PRE"].ToString();

                        rgData = worksheet.get_Range("H3", "H3");
                        rgData.WrapText = true;
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 11;
                        rgData.Value2 = "Absent   :  " + dtExcel.Rows[0]["HPAM_ABSX"].ToString();

                        rgData = worksheet.get_Range("H4", "H4");
                        rgData.WrapText = true;
                        rgData.Merge(Type.Missing);
                        rgData.Font.Bold = true; rgData.Font.Size = 11;
                        rgData.Value2 = "Leaves    :  " + dtExcel.Rows[0]["LVS_AVAILED"].ToString();


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

                        rg = worksheet.get_Range("A5", Type.Missing);
                        rg.Cells.ColumnWidth = 4;
                        rg = worksheet.get_Range("B5", Type.Missing);
                        rg.Cells.ColumnWidth = 12;
                        rg = worksheet.get_Range("C5", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("D5", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("E5", Type.Missing);
                        rg.Cells.ColumnWidth = 10;
                        rg = worksheet.get_Range("F5", Type.Missing);
                        rg.Cells.ColumnWidth = 20;
                        rg = worksheet.get_Range("G5", Type.Missing);
                        rg.Cells.ColumnWidth = 15;
                        rg = worksheet.get_Range("H5", Type.Missing);
                        rg.Cells.ColumnWidth = 30;


                        int iColumn = 1, iStartRow = 5;
                        worksheet.Cells[iStartRow, iColumn++] = "SlNo";
                        worksheet.Cells[iStartRow, iColumn++] = "Date";
                        worksheet.Cells[iStartRow, iColumn++] = "In";
                        worksheet.Cells[iStartRow, iColumn++] = "Out";
                        worksheet.Cells[iStartRow, iColumn++] = "Late(Min)";
                        worksheet.Cells[iStartRow, iColumn++] = "Status";
                        worksheet.Cells[iStartRow, iColumn++] = "Short Status";
                        worksheet.Cells[iStartRow, iColumn++] = "Remarks";

                        iStartRow++; iColumn = 1;

                        for (int i = 0; i < dtExcel.Rows.Count; i++)
                        {
                            worksheet.Cells[iStartRow, iColumn++] = (i + 1).ToString();

                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["HPAMT_DATE"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["HPAMT_FROM"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["HPAMT_TO"];
                            worksheet.Cells[iStartRow, iColumn++] = Convert.ToDouble(dtExcel.Rows[i]["HPAMT_LATE_MNTS"]) * 100;
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["HPAMT_STATUS"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["shortStatus"];
                            worksheet.Cells[iStartRow, iColumn++] = dtExcel.Rows[i]["HPAMT_REMARKS"];

                            iStartRow++; iColumn = 1;
                        }

                        iStartRow = 5;
                        iColumn = iStartRow;
                        rgHead = worksheet.get_Range("E" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString(),
                                                "E" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString());

                        rg = worksheet.get_Range("A" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString(),
                                                "D" + (Convert.ToInt32(dtExcel.Rows.Count) + 6).ToString());
                        rg.Merge(Type.Missing);
                        rg.Value2 = "Total Late Minutes";
                        rg.Font.Bold = true; rg.Borders.Weight = 2; rg.Font.Size = 14;
                        rg.Font.ColorIndex = 30;
                        rg.VerticalAlignment = Excel.Constants.xlCenter;
                        rg.HorizontalAlignment = Excel.Constants.xlRight;

                        rgHead.Borders.Weight = 2;
                        rgHead.Font.Size = 12; rgHead.Font.Bold = true;
                        //rgHead.HorizontalAlignment = Excel.Constants.xlCenter;

                        for (int j = 0; j < Convert.ToInt32(dtExcel.Rows.Count); j++)
                        {
                            iStartRow = 5; iColumn = iStartRow;
                            worksheet.Cells[Convert.ToInt32(dtExcel.Rows.Count) + 6, iColumn] = "=SUM(" + objUtilityDB.GetColumnName(iColumn) + "2:" + objUtilityDB.GetColumnName(iColumn) + "" + (Convert.ToInt32(dtExcel.Rows.Count) + 5).ToString() + ")";

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
            //if (iFormType == 35)
            //{
            //    for (int i = 0; i < chkDemoType.Items.Count; i++)
            //    {
            //        if (e.Index != i)
            //            chkDemoType.SetItemCheckState(i, CheckState.Unchecked);
            //    }
            //}
            //else
            //{
            //}
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
