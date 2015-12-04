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

namespace SSCRM
{
    public partial class ReportFDateTDate : Form
    {
        private ReportViewer childReportViewer = null;
        private string Rep_type = "";
        public ReportFDateTDate()
        {
            InitializeComponent();
        }
        public ReportFDateTDate(string Rtype)
        {
            InitializeComponent();
            Rep_type = Rtype;
        }


        private void btnReport_Click(object sender, EventArgs e)
        {
            if (Rep_type == "RECR_SUMMARY")
            {
                childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, Convert.ToDateTime(dtpFDate.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpTDate.Value).ToString("dd/MMM/yyyy"), "SUMMARY");
                CommonData.ViewReport = "RECRUITEMENT_DATA_SUMMARY";
                childReportViewer.Show();
            }
            else if (Rep_type == "RECR_SUMMARY_BY_COMPANY")
            {
                childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, Convert.ToDateTime(dtpFDate.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpTDate.Value).ToString("dd/MMM/yyyy"), "SUMMARY");
                CommonData.ViewReport = "RECRUITEMENT_SUMMARY_BY_COMPANY";
                childReportViewer.Show();
            }
            else if (Rep_type == "FOUNDATION_EYE_CAMP_REG")
            {
                childReportViewer = new ReportViewer(Convert.ToDateTime(dtpFDate.Value).ToString("dd/MMM/yyyy"), Convert.ToDateTime(dtpTDate.Value).ToString("dd/MMM/yyyy"));
                CommonData.ViewReport = "FOUNDATION_EYE_CAMP_REG";
                childReportViewer.Show();
            }
            else if (Rep_type == "SSERP_REP_STATIONARY_GRN_REGISTER")
            {
                childReportViewer = new ReportViewer("", "", Convert.ToDateTime(dtpFDate.Value).ToString("dd/MMM/yyyy").ToUpper(), Convert.ToDateTime(dtpTDate.Value).ToString("dd/MMM/yyyy").ToUpper(), "");
                CommonData.ViewReport = "SSERP_REP_STATIONARY_GRN_REGISTER";
                childReportViewer.Show();

            }
            else if (Rep_type == "SSERP_REP_STATIONARY_DELIVERY_CHALLAN_REGISTER")
            {
                childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, Convert.ToDateTime(dtpFDate.Value).ToString("dd/MMM/yyyy").ToUpper(), Convert.ToDateTime(dtpTDate.Value).ToString("dd/MMM/yyyy").ToUpper(), "");
                CommonData.ViewReport = "SSERP_REP_STATIONARY_DELIVERY_CHALLAN_REGISTER";
                childReportViewer.Show();

            }

            else if (Rep_type == "PENDING KNOCKING REGISTER")
            {
                childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, Convert.ToDateTime(dtpFDate.Value).ToString("dd/MMM/yyyy").ToUpper(), Convert.ToDateTime(dtpTDate.Value).ToString("dd/MMM/yyyy").ToUpper(), "PENDING");
                CommonData.ViewReport = "PENDING KNOCKING REGISTER";
                childReportViewer.Show();

            }
            else if (Rep_type == "SSCRM_REP_STATIONARY_SHORTAGE_REGISTER")
            {
                childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, CommonData.FinancialYear, Convert.ToDateTime(dtpFDate.Value).ToString("dd/MMM/yyyy").ToUpper(), Convert.ToDateTime(dtpTDate.Value).ToString("dd/MMM/yyyy").ToUpper(), "");
                CommonData.ViewReport = "SSCRM_REP_STATIONARY_SHORTAGE_REGISTER";
                childReportViewer.Show();

            }
            else if (Rep_type == "SSCRM_REP_STATIONARY_RECONSILATION_STORE")
            {
                childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, Convert.ToDateTime(dtpFDate.Value).ToString("dd/MMM/yyyy").ToUpper(), Convert.ToDateTime(dtpTDate.Value).ToString("dd/MMM/yyyy").ToUpper(), "");
                CommonData.ViewReport = "SSCRM_REP_STATIONARY_RECONSILATION_STORE";
                childReportViewer.Show();

            }

            else if (Rep_type == "SSCRM_REP_GL_WISE_STOCK_RECONCILLATION")
            {
                childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, Convert.ToDateTime(dtpFDate.Value).ToString("MMMyyyy").ToUpper(), Convert.ToDateTime(dtpTDate.Value).ToString("MMMyyyy").ToUpper(), "");
                CommonData.ViewReport = "SSCRM_REP_GL_WISE_STOCK_RECONCILLATION";
                childReportViewer.Show();
            }


            else
            {
                childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, Convert.ToDateTime(dtpFDate.Value).ToString("yyyy-MM-dd"), Convert.ToDateTime(dtpTDate.Value).ToString("dd/MMM/yyyy"), "RECRUITEMENT_DATA");
                CommonData.ViewReport = "RECRUITEMENT_DATA";
                childReportViewer.Show();
            }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void ReportFDateTDate_Load(object sender, EventArgs e)
        {
            if (Rep_type == "SSCRM_REP_GL_WISE_STOCK_RECONCILLATION")
            {
                dtpFDate.CustomFormat = "MMM/yyyy";
                dtpTDate.CustomFormat = "MMM/yyyy";
            }
            else
            {
                dtpFDate.Value = Convert.ToDateTime(Convert.ToDateTime("01" + CommonData.DocMonth).ToString("dd/MM/yyyy"));
                dtpTDate.Value = Convert.ToDateTime(Convert.ToDateTime(CommonData.CurrentDate).ToString("dd/MM/yyyy"));
            }
        }
    }
}
