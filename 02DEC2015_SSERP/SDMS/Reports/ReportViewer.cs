using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using SSCRMDB;

namespace SDMS
{
    public partial class ReportViewer : Form
    {
        public Security objSecurity = new Security();
        public ReportViewer()
        {
            InitializeComponent();
        }
        public int iApprovedId = 0, E_code = 0, iFrom = 0, iTo = 0;
        public string CompanyCode = "", BranchCode = "", DocMonth = "", FinYear = "", rSalesInvoice = "", LeadType = "", Demo_Type = "", Report_Type = "";
        public float CreditFrom = 0.00F, CreditTo = 0.00F, MnthFrm = 0.00F, MnthTo = 0.00F, DelayFrom = 0.00F, DelayTo=0.00F;
        DateTime Frmdate, ToDate;
        DataSet Ds_Service;
        public ReportViewer(int ApprovedId)
        {
            InitializeComponent();
            iApprovedId = ApprovedId;
        }
        public ReportViewer(string strValue)
        {
            InitializeComponent();
            Report_Type = strValue;
        }
        public ReportViewer(DataSet dsReport)
        {
            InitializeComponent();
            Ds_Service = dsReport;
        }
        public ReportViewer(int ecode, string reporttype)
        {
            InitializeComponent();
            E_code = ecode;
            Report_Type = reporttype;
        }
        public ReportViewer(string Salesinvoice, string docMonth)
        {
            InitializeComponent();
            rSalesInvoice = Salesinvoice;
            DocMonth = docMonth;
        }
        public ReportViewer(string CCode, string BCode, string DMonth)
        {
            InitializeComponent();
            CompanyCode = CCode;
            BranchCode = BCode;
            DocMonth = DMonth;
        }
        public ReportViewer(string CCode, string BCode, string DMonth, string RType)
        {
            InitializeComponent();
            CompanyCode = CCode;
            BranchCode = BCode;
            DocMonth = DMonth;
            Report_Type = RType;
        }
        public ReportViewer(string CCode, string BCode, string DMonth, int eCode)
        {
            InitializeComponent();
            CompanyCode = CCode;
            BranchCode = BCode;
            DocMonth = DMonth;
            E_code = eCode;
        }
        public ReportViewer(string CCode, string BCode, string DMonth, string sLeadType, string sDemType)
        {
            InitializeComponent();
            CompanyCode = CCode;
            BranchCode = BCode;
            DocMonth = DMonth;
            LeadType = sLeadType;
            Demo_Type = sDemType;
        }
        public ReportViewer(string CCode, string BCode, string DMonth, string sLeadType, string sDemType,string sRepType)
        {
            InitializeComponent();
            CompanyCode = CCode;
            BranchCode = BCode;
            DocMonth = DMonth;
            LeadType = sLeadType;
            Demo_Type = sDemType;
            Report_Type = sRepType;
        }
        public ReportViewer(string CCode, string BCode, string DMonth, int iValue1, int iValue2,string sType)
        {
            InitializeComponent();
            CompanyCode = CCode;
            BranchCode = BCode;
            DocMonth = DMonth;
            iFrom = iValue1;
            iTo = iValue2;
            Report_Type = sType;
        }
        public ReportViewer(string CCode, string BCode, string DMonth, string sLeadType, string sDemType, float CrFrom, float CrTo, float DealerUsFrom, float DealerUsTo, string sRepType)
        {
            InitializeComponent();
            CompanyCode = CCode;
            BranchCode = BCode;
            DocMonth = DMonth;
            LeadType = sLeadType;
            Demo_Type = sDemType;
            CreditFrom = CrFrom;
            CreditTo = CrTo;
            MnthFrm = DealerUsFrom;
            MnthTo = DealerUsTo;
            Report_Type = sRepType;
        }
        public ReportViewer(string CCode, string BCode, string DMonth, string sLeadType, string sDemType, float CrFrom, float CrTo, float DealerUsFrom, float DealerUsTo, float DlyFm, float DlyTo, string sRepType)
        {
            InitializeComponent();
            CompanyCode = CCode;
            BranchCode = BCode;
            DocMonth = DMonth;
            LeadType = sLeadType;
            Demo_Type = sDemType;
            CreditFrom = CrFrom;
            CreditTo = CrTo;
            MnthFrm = DealerUsFrom;
            MnthTo = DealerUsTo;
            DelayFrom = DlyFm;
            DelayTo = DlyTo;
            Report_Type = sRepType;
        }
        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (CommonData.ViewReport == "SERVICE_ACTIVITY_DETAILS")
            {
                //SSCRM.Reports.SSCRM_REP_SERVICE_ACTIVITY_DETLX1 crp = new SSCRM.Reports.SSCRM_REP_SERVICE_ACTIVITY_DETLX1();
                //SSCRM.Reports.Invoice.DSActivityService DSService = new SSCRM.Reports.Invoice.DSActivityService();
                //crp.SetDataSource(Ds_Service.Tables[0]);
                //rptViewer.ReportSource = crp;
                //rptViewer.Refresh();
            }
            if (CommonData.ViewReport == "SSHR_HRINFORMATION")
            {
                //SSCRM.Reports.SSCRM_REP_HR_INFORMATION crp = new SSCRM.Reports.SSCRM_REP_HR_INFORMATION();
                //crp.SetDataSource(Ds_Service);
                //rptViewer.ReportSource = crp;
                //rptViewer.Refresh();
            }
            else
                GetLoad();
        }

        public void GetLoad()
        {
            ReportDocument cryRpt = new ReportDocument();

            string strPath = Application.StartupPath;
            strPath = strPath.Replace("\\bin\\Debug", "");

            if (CommonData.ViewReport == "ProductCode")
                cryRpt.Load(strPath + "\\Reports\\Product_Codes_Report.rpt");
            
            #region "This is used STOCK_POINT_STOCK_SUMMARY"
            if (CommonData.ViewReport == "STOCK_POINT_STOCK_SUMMARY")
            {
                cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSCRM_SP_SUMMARY_LEDGER_new.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = CompanyCode;
                cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                paramRep.Value = DocMonth;
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = LeadType;
                cryRpt.ParameterFields["@FromDocMM"].CurrentValues.Add(paramFrom);

                ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                paramTo.Value = Demo_Type;
                cryRpt.ParameterFields["@ToDocMM"].CurrentValues.Add(paramTo);
            }
            #endregion

            #region "This is used STOCK_POINT_STOCK_LEDGER"
            if (CommonData.ViewReport == "STOCK_POINT_STOCK_LEDGER")
            {
                cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSCRM_SP_DETAILED_LEDGER_NEWZ.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = CompanyCode;
                cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                paramRep.Value = DocMonth;
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = LeadType;
                cryRpt.ParameterFields["@FromDocMM"].CurrentValues.Add(paramFrom);

                ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                paramTo.Value = Demo_Type;
                cryRpt.ParameterFields["@ToDocMM"].CurrentValues.Add(paramTo);

            }
            #endregion

            #region "This is used VEHICLE_LOAN_RECOVERY_SUMMARY"
            if (CommonData.ViewReport == "VEHICLE_LOAN_RECOVERY_SUMMARY")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_VEHICLELOANDETAILS.rpt");

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                paramFinYear.Value = DocMonth;
                cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramFinYear);

                ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                paramRep.Value = "RECOVERY";
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);
            }
            #endregion

            #region "This is used VEHICLE_INFORMATION"
            if (CommonData.ViewReport == "VEHICLE_INFORMATION")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_VEHICLE_INFO.rpt");

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                paramFinYear.Value = DocMonth;
                cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramFinYear);

                ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                paramRep.Value = "INFO";
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);
            }
            #endregion
            
            #region "InvoiceDetail"
            if (CommonData.ViewReport == "InvoiceDetail")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_SALES_REG_DETLX.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CommonData.CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = CommonData.BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = Convert.ToDateTime(crReportParams.FromDate).ToString("MMMyyyy").ToUpper();
                cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramFromDate = new ParameterDiscreteValue();
                paramFromDate.Value = crReportParams.FromDate;
                cryRpt.ParameterFields["@xFRDT"].CurrentValues.Add(paramFromDate);

                ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                paramToDate.Value = crReportParams.ToDate;
                cryRpt.ParameterFields["@xTODT"].CurrentValues.Add(paramToDate);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = rSalesInvoice;
                cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "GroupInvoiceDetail"
            if (CommonData.ViewReport == "GroupInvoiceDetail")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_SALES_GRP_TOT.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = CommonData.CompanyCode;
                cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = CommonData.BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = CommonData.DocMonth;
                cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);
            }
            #endregion

            #region "SALES_BULLETIN_REG_WITHOUT_CUSTOMER_DATA"
            if (CommonData.ViewReport == "SALES_BULLETIN_REG_WITHOUT_CUST")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REPORT_SALESBULLETIN_REG.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramFDate = new ParameterDiscreteValue();
                paramFDate.Value = Convert.ToDateTime(CommonData.DocMonth).ToString("dd/MMM/yyyy");
                cryRpt.ParameterFields["@xFRDT"].CurrentValues.Add(paramFDate);

                ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                paramToDate.Value = Convert.ToDateTime(CommonData.DocMonth).ToString("dd/MMM/yyyy");
                cryRpt.ParameterFields["@xTODT"].CurrentValues.Add(paramToDate);

                ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                paramRep.Value = Report_Type;
                cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRep);
            }
            #endregion

            #region "ApprovedDetails"
            if (CommonData.ViewReport == "ApprovedDetails")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_APPROVEDDATAT.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = iApprovedId;
                cryRpt.ParameterFields["@ApprovalID"].CurrentValues.Add(paramcmdCode);
            }
            #endregion

            #region "VEHICLE_INCENTIVE_APPROVAL"
            if (CommonData.ViewReport == "VEHICLE_INCENTIVE_APPROVAL")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_VEHICLE_INCENTIVE_APPROVAL.rpt");

                ParameterDiscreteValue paramRefNo = new ParameterDiscreteValue();
                paramRefNo.Value = Report_Type;
                cryRpt.ParameterFields["@xREFNO"].CurrentValues.Add(paramRefNo);
            }
            #endregion

            #region "PETROL_ALLOWANCE_APPROVAL"
            if (CommonData.ViewReport == "PETROL_ALLOWANCE_APPROVAL")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_PETROL_ALLOWANCE_APPROVAL.rpt");

                ParameterDiscreteValue paramRefNo = new ParameterDiscreteValue();
                paramRefNo.Value = Report_Type;
                cryRpt.ParameterFields["@xREFNO"].CurrentValues.Add(paramRefNo);
            }
            #endregion

            #region "AppointmentLetter"
            if (CommonData.ViewReport == "AppointmentLetter")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_APPOINTMENTLETTER.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = iApprovedId;
                cryRpt.ParameterFields["@ApplicationNo"].CurrentValues.Add(paramcmdCode);
            }
            #endregion

            #region "SSCRM_REP_DOCMM_GROUPSX";
            if (CommonData.ViewReport == "SSCRM_REP_DOCMM_GROUPSX")
            {
                if (rSalesInvoice == "Detailed")
                    cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_DOCMM_GROUPSX.rpt");
                else
                    cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_DOCMM_GROUPS_OnlyX.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CommonData.CompanyCode;
                cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = CommonData.BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                if (rSalesInvoice == "Detailed")
                    paramRepType.Value = "BRANCH HEAD TO SR";
                else
                    paramRepType.Value = "BRANCH HEAD TO GL";
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used SSCRM_REP_SALES_SR_DOCMM_ACCOUNTABILITY"
            if (CommonData.ViewReport == "SSCRM_REP_SALES_SR_DOCMM_ACCOUNTABILITY")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_SR_Accountability.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CompanyCode;
                cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = E_code;
                cryRpt.ParameterFields["@xeora_code"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used STOCKPOINT_GRN"
            if (CommonData.ViewReport == "STOCKPOINT_GRN")
            {
                cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSCRM_REP_STOCKPOINTS_GRN.rpt");

                //ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                //paramcmdCode.Value = CompanyCode;
                //cryRpt.ParameterFields["@Company"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@BranchName"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@DocMonth"].CurrentValues.Add(paramDocMonth);

            }
            #endregion

            #region "This is used STOCKPOINT_DCST"
            if (CommonData.ViewReport == "STOCKPOINT_DCST")
            {
                cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSCRM_REP_STOCKPOINT_DCST.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = CompanyCode;
                cryRpt.ParameterFields["@Company"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@BranchName"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@DocMonth"].CurrentValues.Add(paramDocMonth);

            }
            #endregion

            #region "This is used STOCKPOINT_DC"
            if (CommonData.ViewReport == "STOCKPOINT_DC")
            {
                cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSCRM_REP_STOCKPOINT_DC.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = CompanyCode;
                cryRpt.ParameterFields["@Company"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@BranchName"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@DocMonth"].CurrentValues.Add(paramDocMonth);
                               
            }
            #endregion

            #region "This is used GC_GL_WISE_PRODUCT_RECONSILATION"
            if (CommonData.ViewReport == "GC_GL_WISE_PRODUCT_RECONSILATION")
            {
                cryRpt.Load(strPath + "\\Reports\\GC_GL_WISE_PRODUCT_RECONSILATION.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                paramEcode.Value = Report_Type;
                cryRpt.ParameterFields["@xECODE"].CurrentValues.Add(paramEcode);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = "GC_GL_WISE";
                cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used SSCRM_REP_SALES_SR_MULTI_DOCMM_SUMMARY"
            if (CommonData.ViewReport == "SSCRM_REP_SALES_SR_MULTI_DOCMM_SUMMARY")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_SALES_SR_MULTI_DOCMM_LEVEL_ORDER.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CompanyCode;
                cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = "LEVEL ORDER";
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used SALES_PROD_SUMMARY_BY_CATG"
            if (CommonData.ViewReport == "SALES_PROD_SUMMARY_BY_CATG")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_DOCMM_PROD_SUMM_CATG_STATE.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CompanyCode;
                cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = Report_Type;
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used RECRUITEMENT_DATA"
            if (CommonData.ViewReport == "RECRUITEMENT_DATA")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_RECRUITEMENT_DATA.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CompanyCode;
                cryRpt.ParameterFields["@CompanyCode"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@BranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramFDate = new ParameterDiscreteValue();
                paramFDate.Value = DocMonth;
                cryRpt.ParameterFields["@FROMDATE"].CurrentValues.Add(paramFDate);

                ParameterDiscreteValue paramTDate = new ParameterDiscreteValue();
                paramTDate.Value = LeadType;
                cryRpt.ParameterFields["@TODATE"].CurrentValues.Add(paramTDate);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = Demo_Type;
                cryRpt.ParameterFields["@REPORTTYPE"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used RECRUITEMENT_DATA_SUMMARY"
            if (CommonData.ViewReport == "RECRUITEMENT_DATA_SUMMARY")
            {
                cryRpt.Load(strPath + "\\Reports\\SSHRIS_REP_RECRUITEMENT_SUMMARY.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CompanyCode;
                cryRpt.ParameterFields["@CompanyCode"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@BranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramFDate = new ParameterDiscreteValue();
                paramFDate.Value = DocMonth;
                cryRpt.ParameterFields["@FROMDATE"].CurrentValues.Add(paramFDate);

                ParameterDiscreteValue paramTDate = new ParameterDiscreteValue();
                paramTDate.Value = LeadType;
                cryRpt.ParameterFields["@TODATE"].CurrentValues.Add(paramTDate);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = Demo_Type;
                cryRpt.ParameterFields["@REPORTTYPE"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used RECRUITEMENT_SUMMARY_BY_COMPANY"
            if (CommonData.ViewReport == "RECRUITEMENT_SUMMARY_BY_COMPANY")
            {
                cryRpt.Load(strPath + "\\Reports\\SSHR_REP_RECRUITMENT_SUMMAR_BY_COMPANY.rpt");

                ParameterDiscreteValue paramFDate = new ParameterDiscreteValue();
                paramFDate.Value = DocMonth;
                cryRpt.ParameterFields["@FROMDATE"].CurrentValues.Add(paramFDate);

                ParameterDiscreteValue paramTDate = new ParameterDiscreteValue();
                paramTDate.Value = LeadType;
                cryRpt.ParameterFields["@TODATE"].CurrentValues.Add(paramTDate);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = Demo_Type;
                cryRpt.ParameterFields["@REPORTTYPE"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used SALES_PROD_SUMMARY_BY_PRODUCT"
            if (CommonData.ViewReport == "SALES_PROD_SUMMARY_BY_PRODUCT")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_DOCMM_PROD_SUMM.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CompanyCode;
                cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = Report_Type;
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used STOCKPOINT_CHECKLIST"
            if (CommonData.ViewReport == "STOCKPOINT_CHECKLIST")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_SP_CheckList_Report.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CompanyCode;
                cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = Report_Type;
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used for Order issues report"
            if (CommonData.ViewReport == "SSCRMREP_ORDERISSUE")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_ORDERISSUE.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CommonData.CompanyCode;
                cryRpt.ParameterFields["@CompanyCode"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = CommonData.BranchCode;
                cryRpt.ParameterFields["@BranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = CommonData.DocMonth;
                cryRpt.ParameterFields["@DocMonth"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = iApprovedId;
                cryRpt.ParameterFields["@ECode"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used for Branch Wise Order Reconsilation report"
            if (CommonData.ViewReport == "Branch Wise OrderSheet Reconsilation")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_MIS_REP_GL_ORDERSHEETS_RECONSILATION.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CompanyCode;
                cryRpt.ParameterFields["@COMPANY_CODE"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@BRANCH_CODE"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@DOCMONTH"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramFinyear = new ParameterDiscreteValue();
                paramFinyear.Value = LeadType;
                cryRpt.ParameterFields["@FIN_YEAR"].CurrentValues.Add(paramFinyear);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = Demo_Type;
                cryRpt.ParameterFields["@REPORT_TYPE"].CurrentValues.Add(paramRepType);

            }
            #endregion

            #region "This is used for Check list"
            if (CommonData.ViewReport == "SSCRM_CHECKLIST")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_DOCMM_SUMMARY_CrossTab.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CompanyCode;
                cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = "SUMMARY";
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used for sales Order form
            if (CommonData.ViewReport == "SSCRM_REP_SR_OrderForm")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_SR_ORDERSHEETRETURN.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CommonData.CompanyCode;
                cryRpt.ParameterFields["@COMPANY_CODE"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = CommonData.BranchCode;
                cryRpt.ParameterFields["@BRANCH_CODE"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = CommonData.DocMonth;
                cryRpt.ParameterFields["@DOCMONTH"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramFy = new ParameterDiscreteValue();
                paramFy.Value = CommonData.FinancialYear;
                cryRpt.ParameterFields["@FIN_YEAR"].CurrentValues.Add(paramFy);

                ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                paramEcode.Value = E_code;
                cryRpt.ParameterFields["@ECODE"].CurrentValues.Add(paramEcode);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = Report_Type;
                cryRpt.ParameterFields["@REPORT_TYPE"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used for Doorknock Report"
            if (CommonData.ViewReport == "Doorknocks")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_DOORKNOCKS_INFO.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CompanyCode;
                cryRpt.ParameterFields["@CompanyCode"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@BranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@DocMonth"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramLeadType = new ParameterDiscreteValue();
                paramLeadType.Value = LeadType;
                cryRpt.ParameterFields["@LeadType"].CurrentValues.Add(paramLeadType);

                ParameterDiscreteValue paramDemoType = new ParameterDiscreteValue();
                paramDemoType.Value = Demo_Type;
                cryRpt.ParameterFields["@DemoType"].CurrentValues.Add(paramDemoType);
            }
            #endregion

            #region "This is used for Sales Order"
            if (CommonData.ViewReport == "SALESORDER")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_SALES_ORDER.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CompanyCode;
                cryRpt.ParameterFields["@CompanyCode"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@BranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@DocMonth"].CurrentValues.Add(paramDocMonth);
            }
            if (CommonData.ViewReport == "SALESORDER_ORDER_BY_ORDER_NO")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_SALES_ORDERS_BY_ORDNO.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CompanyCode;
                cryRpt.ParameterFields["@CompanyCode"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@BranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@DocMonth"].CurrentValues.Add(paramDocMonth);
            }
            #endregion

            #region "This is used for SSCRM_YTD_SALE_BULTIN_REPORT"
            if (CommonData.ViewReport == "SSCRM_YTD_SALE_BULTIN_REPORT")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_YTD_SALE_BULTIN_Report.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CommonData.CompanyCode;
                cryRpt.ParameterFields["@xCompanyCOde"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = CommonData.BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xdocMonth"].CurrentValues.Add(paramDocMonth);
            }
            #endregion

            #region "This is used for SR_WISE SALES BULLETIN REPORT"
            if (CommonData.ViewReport == "SR_WISE_SALES_BULLETINS")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_YTD_SALE_BULTIN_REPORT_SR.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CompanyCode;
                cryRpt.ParameterFields["@xCompanyCOde"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xdocMonth"].CurrentValues.Add(paramDocMonth);
                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = Report_Type;
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used for GL_WISE SALES BULLETIN REPORT"
            if (CommonData.ViewReport == "GL_WISE_SALES_BULLETINS")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_YTD_SALE_BULTIN_Report_gl_or_gc.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CompanyCode;
                cryRpt.ParameterFields["@xCompanyCOde"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xdocMonth"].CurrentValues.Add(paramDocMonth);
                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = Report_Type;
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used for GL_WISE_ADVANCES_SUMMERY REPORT"
            if (CommonData.ViewReport == "GL_WISE_ADVANCES_SUMMERY")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_GL_ADVANCE_RECONSILATION.rpt");

                ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                paramCompCode.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramCompCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);
                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = Report_Type;
                cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used for Organisation Chart report"
            if (CommonData.ViewReport == "Organisation Chart")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_DOCMM_GROUPS_HIR_CHART.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = CommonData.CompanyCode;
                cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = CommonData.BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = CommonData.DocMonth;
                cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                paramReportType.Value = "BRANCH HEAD TO SR";
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramReportType);
            }
            #endregion

            #region "This is used for Sales Staff Strength"
            if (CommonData.ViewReport == "Sales Staff Strength")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_SALES_STAFF_STRENGTH.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                paramReportType.Value = "SALES STAFF";
                cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramReportType);
            }
            #endregion

            #region "This is used for BRANCH_WISE_BULLETINS REPORT"
            if (CommonData.ViewReport == "BRANCH_WISE_BULLETINS")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_MIS_REP_BRANCH_WISE_BULLETINS.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                paramReportType.Value = Report_Type;
                cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramReportType);
            }
            #endregion

            #region "This is used for BRANCH_WISE_BULLETINS REPORT"
            if (CommonData.ViewReport == "BRANCH_WISE_BULLETINS_BY_ALL")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_BRANCH_PERFORMANCE_BY_ALL_MONTHS.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                paramReportType.Value = "ALL";
                cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramReportType);
            }
            #endregion

            #region "This is used for BRANCH_WISE_PRODUCT_RECONSILATION REPORT"
            if (CommonData.ViewReport == "BRANCH_WISE_PRODUCT_RECONSILATION")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_BRANCH_WISE_PRODUCT_RECONSILATION.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                paramEcode.Value = "0";
                cryRpt.ParameterFields["@xECODE"].CurrentValues.Add(paramEcode);

                ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                paramReportType.Value = Report_Type;
                cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramReportType);
            }
            #endregion

            #region "This is used for STATIONARY INDENT DETAILS"
            if (CommonData.ViewReport == "STATIONARYINDENT")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_STATIONARY_INDENT.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = CompanyCode;
                cryRpt.ParameterFields["@Company_Code"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@Bracnh_Code"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@FinYear"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                paramEcode.Value = E_code;
                cryRpt.ParameterFields["@IndentNo"].CurrentValues.Add(paramEcode);
            }
            #endregion

            #region "This is used for STATIONARY INDENT_FOR_DC DETAILS"
            if (CommonData.ViewReport == "STATIONARYINDENT_FOR_DC")
            {
                cryRpt.Load(strPath + "\\Reports\\StationaryReports\\SSCRM_REP_STATIONARY_INDENT_ForDispatch.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = CompanyCode;
                cryRpt.ParameterFields["@Company_Code"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@Bracnh_Code"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@FinYear"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                paramEcode.Value = E_code;
                cryRpt.ParameterFields["@IndentNo"].CurrentValues.Add(paramEcode);
            }
            #endregion

            #region "This is used for STATIONARY DISPATCH DETAILS"
            if (CommonData.ViewReport == "STATIONARYDISPATCH")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_STATIONARY_DELIVERY_CHALLAN.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = CompanyCode;
                cryRpt.ParameterFields["@Company_Code"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@Bracnh_Code"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@FinYear"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                paramEcode.Value = E_code;
                cryRpt.ParameterFields["@IndentNo"].CurrentValues.Add(paramEcode);
            }
            #endregion

            #region "This is used for BRANCH_WISE_EMP_PERFORMANCE_MONTHLY REPORT"
            if (CommonData.ViewReport == "BRANCH_WISE_EMP_PERFORMANCE_MONTHLY")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_MONTH_WISE_EMP_PERFORMANCE.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                paramEcode.Value = LeadType;
                cryRpt.ParameterFields["@xECODE"].CurrentValues.Add(paramEcode);

                ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                paramReportType.Value = Demo_Type;
                cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramReportType);
            }
            #endregion

            #region "This is used for GOOGLE MAP DISTANCE"
            if (CommonData.ViewReport == "CAMPTOSPDISTANCE")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_CamptoSP_Distance.rpt");

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = rSalesInvoice;
                cryRpt.ParameterFields["@BranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                paramEcode.Value = DocMonth;
                cryRpt.ParameterFields["@DocMonth"].CurrentValues.Add(paramEcode);
            }
            #endregion

            #region "This is used for BRANCH_WISE_EMP_PERFORMANCE_MONTHLY REPORT"
            if (CommonData.ViewReport == "RECRUITMENT_REPORT_BY_INDIVIDUALS")
            {
                cryRpt.Load(strPath + "\\Reports\\SSHRIS_REP_RECRUITED_BY.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = CompanyCode;
                cryRpt.ParameterFields["@xBranch"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xECODE"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                paramEcode.Value = LeadType;
                cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramEcode);

                ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                paramReportType.Value = Demo_Type;
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramReportType);
            }
            #endregion

            #region "This is used for EMPLOYEE_PERFORMANCE_CUMULATIVE REPORT"
            if (CommonData.ViewReport == "EMPLOYEE_PERFORMANCE_CUMULATIVE")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_CUMULATIVE_EMP_PERFORMANCE.rpt");

                ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                paramcmdCode.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramcmdCode);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = iFrom;
                cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFrom);

                ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                paramTo.Value = iTo;
                cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramTo);

                ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                paramReportType.Value = Report_Type;
                cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramReportType);
            }
            #endregion

            #region "This is used for NOT MAPPED LIST"
            if (CommonData.ViewReport == "NOTMAPPEDLIST")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_NOT_MAPPEDLIST.rpt");

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@DocMonth"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                paramBrancCode.Value = rSalesInvoice;
                cryRpt.ParameterFields["@BranchCode"].CurrentValues.Add(paramBrancCode);
            }
            #endregion

            #region "This is used for EMPLOYEE_SALE_PERFORMANCE_INDIVIDUALS REPORT"
            if (CommonData.ViewReport == "EMPLOYEE_SALE_PERFORMANCE_INDIVIDUALS")
            {
                cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_EMP_IND_PERFORMANCE.rpt");

                ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                raramEcode.Value = CompanyCode;
                cryRpt.ParameterFields["@xECODE"].CurrentValues.Add(raramEcode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = BranchCode;
                cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = DocMonth;
                cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFrom);

                ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                paramTo.Value = LeadType;
                cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramTo);

                ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                paramReportType.Value = Demo_Type;
                cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramReportType);
            }
            #endregion

            #region "This is used for EMPLOYEE ID-CARDS NORMAL REPORT"
            if (CommonData.ViewReport == "EMPLOYEE ID-CARDS")
            {
                cryRpt.Load(strPath + "\\Reports\\HR Reports\\SSCRM_REP_IDCARDS_NORMAL.rpt");

                ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                raramEcode.Value = Report_Type;
                cryRpt.ParameterFields["@xECodes"].CurrentValues.Add(raramEcode);               
            }
            #endregion

            #region "This is used for EMPLOYEE ID-CARDS DIGITAL REPORT"
            if (CommonData.ViewReport == "EMPLOYEE ID-CARDS_DIGITAL")
            {
                cryRpt.Load(strPath + "\\Reports\\HR Reports\\SSCRM_REP_IDCARDS.rpt");

                ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                raramEcode.Value = Report_Type;
                cryRpt.ParameterFields["@xECodes"].CurrentValues.Add(raramEcode);
            }
            #endregion

            #region "This is used for ADVANCE_REFUND_REGISTER REPORT"
            if (CommonData.ViewReport == "ADVANCE_REFUND_REGISTER")
            {
                cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_ADVANCEREFUND.rpt");

                ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                raramEcode.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(raramEcode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = DocMonth;
                cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFrom);

                ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                paramTo.Value = LeadType;
                cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramTo);

                ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                paramReportType.Value = Demo_Type;
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramReportType);
            }
            #endregion

            #region "This is used for STOCKPOINT_STOCK_RECONSILATION REPORT"
            if (CommonData.ViewReport == "STOCKPOINT_STOCK_RECONSILATION")
            {
                cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSCRM_REP_SP_STOCK_RECONSILATION.rpt");

                ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                raramEcode.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(raramEcode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = DocMonth;
                cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramFrom);

                ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                paramReportType.Value = Demo_Type;
                cryRpt.ParameterFields["@RepType"].CurrentValues.Add(paramReportType);
            }
            if (CommonData.ViewReport == "STOCKPOINT_STOCK_RECONSILATION_BYDATE")
            {
                cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSCRM_REP_SP_STOCK_RECONSILATION_bydate.rpt");

                ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                raramEcode.Value = Report_Type;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(raramEcode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = DocMonth;
                cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramFrom);

                ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                paramReportType.Value = CompanyCode;
                cryRpt.ParameterFields["@RepType"].CurrentValues.Add(paramReportType);
            }
            #endregion

            #region "This is used for SP_DC_BY_INDIVIDUALS REPORT"
            if (CommonData.ViewReport == "SP_DC_BY_INDIVIDUALS")
            {
                cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSCRM_REP_STOCK_FROM_SP_TO_EMP.rpt");

                ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                raramEcode.Value = BranchCode;
                cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(raramEcode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = LeadType;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramFrom);

                ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                paramTo.Value = Report_Type;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramTo);

                ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                paramReportType.Value = CompanyCode;
                cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramReportType);
            }
            #endregion

            #region "This is used for STOCKPOINT_OPENING_STOCK REPORT"
            if (CommonData.ViewReport == "STOCKPOINT_OPENING_STOCK")
            {
                cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSCRM_REP_SP_OPENING_STOCK.rpt");

                ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                raramEcode.Value = CompanyCode;
                cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(raramEcode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = Report_Type;
                cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramFrom);

                ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                paramTo.Value = DocMonth;
                cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramTo);
                
            }
            #endregion

            #region "This is used for BRANCH_WISE_SALES_STAFF_ALL REPORT"
            if (CommonData.ViewReport == "BRANCH_WISE_SALES_STAFF_ALL")
            {
                cryRpt.Load(strPath + "\\Reports\\HR Reports\\SSCRM_REP_STAFF_DATA.rpt");

                ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                raramEcode.Value = CompanyCode;
                cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(raramEcode);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = DocMonth;
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramFrom);                

            }
            #endregion

            #region "This is used for BRANCH_WISE_APPLICATIONS_STATUS_SUMMARY REPORT"
            if (CommonData.ViewReport == "BRANCH_WISE_APPLICATIONS_STATUS_SUMMARY")
            {
                cryRpt.Load(strPath + "\\Reports\\HR Reports\\SSHRIS_REP_BRANCH_WISE_APPL_DASHBOARD.rpt");

                ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                paramComp.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                paramBranch.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = DocMonth;
                cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFrom);

                ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                paramTo.Value = LeadType;
                cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramTo);

                ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                paramRep.Value = Demo_Type;
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);

            }
            #endregion

            #region "This is used for BRANCHWISE_STATIONARY_RECONSILATION_SUMMARY REPORT"
            if (CommonData.ViewReport == "BRANCHWISE_STATIONARY_RECONSILATION_SUMMARY")
            {
                cryRpt.Load(strPath + "\\Reports\\StationaryReports\\SSCRM_REP_STATIONARY_RECONSILATION_BRANCH.rpt");

                ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                paramComp.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                paramBranch.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = DocMonth;
                cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFrom);

                ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                paramTo.Value = LeadType;
                cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramTo);

                ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                paramRep.Value = Demo_Type;
                cryRpt.ParameterFields["@RepType"].CurrentValues.Add(paramRep);

            }
            #endregion

            #region "This is used for BRANCHWISE_STATIONARY_ISSUE_REGISTER REPORT"
            if (CommonData.ViewReport == "BRANCHWISE_STATIONARY_ISSUE_REGISTER")
            {
                cryRpt.Load(strPath + "\\Reports\\StationaryReports\\SSCRM_REP_BR_STATIONARY_ISSUE_REG.rpt");

                ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                paramComp.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                paramBranch.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = DocMonth;
                cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFrom);

                ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                paramTo.Value = LeadType;
                cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramTo);

                ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                paramRep.Value = Demo_Type;
                cryRpt.ParameterFields["@RepType"].CurrentValues.Add(paramRep);

            }
            #endregion

            #region "This is used for BRANCHWISE_STATIONARY_INDENT_REGISTER REPORT"
            if (CommonData.ViewReport == "BRANCHWISE_STATIONARY_INDENT_REGISTER")
            {
                cryRpt.Load(strPath + "\\Reports\\StationaryReports\\SSCRM_REP_ST_INDENT_REG.rpt");

                ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                paramComp.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                paramBranch.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = DocMonth;
                cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFrom);

                ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                paramTo.Value = LeadType;
                cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramTo);

                ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                paramRep.Value = Demo_Type;
                cryRpt.ParameterFields["@RepType"].CurrentValues.Add(paramRep);

            }
            #endregion

            #region "This is used for PRODUCTION_UNIT_FINISHED_GOODS_REGISTER REPORT"
            if (CommonData.ViewReport == "PRODUCTION_UNIT_FINISHED_GOODS_REGISTER")
            {
                cryRpt.Load(strPath + "\\Reports\\PUReports\\SSCRM_REP_PU_FINISHED_GOODS_REG_DETL.rpt");

                ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                paramComp.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                paramBranch.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = DocMonth;
                cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFrom);

                ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                paramTo.Value = LeadType;
                cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramTo);

                ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                paramRep.Value = Demo_Type;
                cryRpt.ParameterFields["@RepType"].CurrentValues.Add(paramRep);

            }
            #endregion

            #region "This is used for TRANSIT_STATIONARY_DC REPORT"
            if (CommonData.ViewReport == "TRANSIT_STATIONARY_DC")
            {
                cryRpt.Load(strPath + "\\Reports\\StationaryReports\\SSCRM_REP_TRANSIT_STATIONARY_DC.rpt");

                ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                paramComp.Value = CompanyCode;
                cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                paramBranch.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);                
            }
            #endregion

            #region "This is used for SSCRM_REP_ID_PREPARATIONS REPORT"
            if (CommonData.ViewReport == "SSCRM_REP_ID_PREPARATIONS")
            {
                cryRpt.Load(strPath + "\\Reports\\HR Reports\\SSCRM_REP_ID_PREPARATIONS.rpt");

                ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                paramComp.Value = CompanyCode;
                cryRpt.ParameterFields["@xCOMP"].CurrentValues.Add(paramComp);

                ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                paramBranch.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = LeadType;
                cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFrom);

                ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                paramTo.Value = Demo_Type;
                cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramTo);
            }
            #endregion

            #region "This is used for SSCRM_REP_PU_DCST_REG_DETL REPORT"
            if (CommonData.ViewReport == "SSCRM_REP_PU_DCST_REG_DETL")
            {
                cryRpt.Load(strPath + "\\Reports\\PUReports\\SSCRM_REP_PU_DCST_REG_DETL.rpt");

                ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                paramComp.Value = CompanyCode;
                cryRpt.ParameterFields["@xCOMP"].CurrentValues.Add(paramComp);

                ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                paramBranch.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = LeadType;
                cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFrom);

                ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                paramTo.Value = Demo_Type;
                cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramTo);

                ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                paramRep.Value = Report_Type;
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);
            }
            #endregion

            #region "This is used for SSCRM_REP_SP_HEAD_DETAILS REPORT"
            if (CommonData.ViewReport == "SSCRM_REP_SP_HEAD_DETAILS")
            {
                cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSCRM_REP_SP_HEAD_DETAILS.rpt");

                ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                paramComp.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                paramBranch.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);                

                ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                paramRep.Value = DocMonth;
                cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRep);
            }
            #endregion

            #region "This is used for SSCRM_REP_TR_VEHICLE_MASTER REPORT"
            if (CommonData.ViewReport == "SSCRM_REP_TR_VEHICLE_MASTER")
            {
                cryRpt.Load(strPath + "\\Reports\\TransportReports\\SSCRM_REP_TR_VEHICLE_MASTER.rpt");

                ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                paramComp.Value = CompanyCode;
                cryRpt.ParameterFields["@xCOMP"].CurrentValues.Add(paramComp);

                ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                paramBranch.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                paramRep.Value = DocMonth;
                cryRpt.ParameterFields["@xState"].CurrentValues.Add(paramRep);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = DocMonth;
                cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used for Dealer CheckedList"
            if (CommonData.ViewReport == "Dealers Checked List")
            {
                cryRpt.Load(strPath + "\\Reports\\DealerReports\\DL_APPL_EDITLIST.rpt");

                ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                paramComp.Value = CompanyCode;
                cryRpt.ParameterFields["@xCmp_Cd"].CurrentValues.Add(paramComp);

                ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                paramBranch.Value = BranchCode;
                cryRpt.ParameterFields["@xStateCd"].CurrentValues.Add(paramBranch);

                ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                paramRep.Value = DocMonth;
                cryRpt.ParameterFields["@xDistCd"].CurrentValues.Add(paramRep);

                ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                paramDist.Value = LeadType;
                cryRpt.ParameterFields["@xFirmType"].CurrentValues.Add(paramDist);

                ParameterDiscreteValue paramBus = new ParameterDiscreteValue();
                paramBus.Value = Demo_Type;
                cryRpt.ParameterFields["@xBusinessType"].CurrentValues.Add(paramBus);

                ParameterDiscreteValue parmCFrom = new ParameterDiscreteValue();
                parmCFrom.Value = CreditFrom;
                cryRpt.ParameterFields["@xCrLimitFrom"].CurrentValues.Add(parmCFrom);

                ParameterDiscreteValue paramCTo = new ParameterDiscreteValue();
                paramCTo.Value = CreditTo;
                cryRpt.ParameterFields["@xCrLimitUpto"].CurrentValues.Add(paramCTo);

                ParameterDiscreteValue paramDFrom = new ParameterDiscreteValue();
                paramDFrom.Value = MnthFrm;
                cryRpt.ParameterFields["@xDlrWithUsFrom"].CurrentValues.Add(paramDFrom);

                ParameterDiscreteValue paramDTo = new ParameterDiscreteValue();
                paramDTo.Value = MnthTo;
                cryRpt.ParameterFields["@xDlrWithUsUpto"].CurrentValues.Add(paramDTo);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = Report_Type;
                cryRpt.ParameterFields["@xReportType"].CurrentValues.Add(paramRepType);
            }
            #endregion


            #region "This is used for SATL_REP_SALES_REG_DETL REPORT"
            if (CommonData.ViewReport == "SATL_REP_SALES_REG_DETL")
            {
                cryRpt.Load(strPath + "\\Reports\\DealerReports\\SATL_SALREG.rpt");

                ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                paramComp.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                paramBranch.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = LeadType;
                cryRpt.ParameterFields["@xFRDT"].CurrentValues.Add(paramFrom);

                ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                paramTo.Value = Demo_Type;
                cryRpt.ParameterFields["@xTODT"].CurrentValues.Add(paramTo);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = Report_Type;
                cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used for SATL_SALREG_SUMM_REP REPORT"
            if (CommonData.ViewReport == "SATL_SALREG_SUMM_REP")
            {
                cryRpt.Load(strPath + "\\Reports\\DealerReports\\SATL_SALREG_SUMM_REP.rpt");

                ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                paramComp.Value = CompanyCode;
                cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                paramBranch.Value = BranchCode;
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = DocMonth;
                cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                paramFrom.Value = LeadType;
                cryRpt.ParameterFields["@xFRDT"].CurrentValues.Add(paramFrom);

                ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                paramTo.Value = Demo_Type;
                cryRpt.ParameterFields["@xTODT"].CurrentValues.Add(paramTo);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = Report_Type;
                cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used for OUTSTANDING MODEL1"
            if (CommonData.ViewReport == "DL_OUTSTANDING_AGE_REP")
            {
                cryRpt.Load(strPath + "\\Reports\\DealerReports\\SATL_OS_DETL_Report.rpt");

                ParameterDiscreteValue paramComp = new ParameterDiscreteValue();

                paramComp.Value = CompanyCode;
                cryRpt.ParameterFields["@xCmp_Cd"].CurrentValues.Add(paramComp);

                ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                paramBranch.Value = BranchCode;
                cryRpt.ParameterFields["@xStateCd"].CurrentValues.Add(paramBranch);

                ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                paramRep.Value = DocMonth;
                cryRpt.ParameterFields["@xDistCd"].CurrentValues.Add(paramRep);

                ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                paramDist.Value = LeadType;
                cryRpt.ParameterFields["@xFirmType"].CurrentValues.Add(paramDist);

                ParameterDiscreteValue paramBus = new ParameterDiscreteValue();
                paramBus.Value = Demo_Type;
                cryRpt.ParameterFields["@xBusinessType"].CurrentValues.Add(paramBus);



                ParameterDiscreteValue parmDaysFrom = new ParameterDiscreteValue();
                parmDaysFrom.Value = CreditFrom;
                cryRpt.ParameterFields["@xOSDAYFrom"].CurrentValues.Add(parmDaysFrom);

                ParameterDiscreteValue paramDaysTo = new ParameterDiscreteValue();
                paramDaysTo.Value = CreditTo;
                cryRpt.ParameterFields["@xOSDAYUpto"].CurrentValues.Add(paramDaysTo);

                ParameterDiscreteValue paramOsAmtFrom = new ParameterDiscreteValue();
                paramOsAmtFrom.Value = MnthFrm;
                cryRpt.ParameterFields["@xOSAMTFrom"].CurrentValues.Add(paramOsAmtFrom);

                ParameterDiscreteValue paramOsAmtTo = new ParameterDiscreteValue();
                paramOsAmtTo.Value = MnthTo;
                cryRpt.ParameterFields["@xOSAMTUpto"].CurrentValues.Add(paramOsAmtTo);

                ParameterDiscreteValue parmDlrWithFrom = new ParameterDiscreteValue();
                parmDlrWithFrom.Value = CreditFrom;
                cryRpt.ParameterFields["@xDlrWithUsFrom"].CurrentValues.Add(parmDlrWithFrom);

                ParameterDiscreteValue paramDlrWithTo = new ParameterDiscreteValue();
                paramDlrWithTo.Value = CreditTo;
                cryRpt.ParameterFields["@xDlrWithUsUpto"].CurrentValues.Add(paramDlrWithTo);

                ParameterDiscreteValue paramOndate = new ParameterDiscreteValue();
                paramOndate.Value = DateTime.Today.ToString("dd/MMM/yyyy");
                cryRpt.ParameterFields["@xAsOnDate"].CurrentValues.Add(paramOndate);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = Report_Type;
                cryRpt.ParameterFields["@xReportType"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used for OUTSTANDING MODEL2"
            if (CommonData.ViewReport == "SATL_OS_DETL_Report_ModelTwo")
            {
                cryRpt.Load(strPath + "\\Reports\\DealerReports\\SATL_OS_DETL_Report_ModelTwo.rpt");

                ParameterDiscreteValue paramComp = new ParameterDiscreteValue();

                paramComp.Value = CompanyCode;
                cryRpt.ParameterFields["@xCmp_Cd"].CurrentValues.Add(paramComp);

                ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                paramBranch.Value = BranchCode;
                cryRpt.ParameterFields["@xStateCd"].CurrentValues.Add(paramBranch);

                ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                paramRep.Value = DocMonth;
                cryRpt.ParameterFields["@xDistCd"].CurrentValues.Add(paramRep);

                ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                paramDist.Value = LeadType;
                cryRpt.ParameterFields["@xFirmType"].CurrentValues.Add(paramDist);

                ParameterDiscreteValue paramBus = new ParameterDiscreteValue();
                paramBus.Value = Demo_Type;
                cryRpt.ParameterFields["@xBusinessType"].CurrentValues.Add(paramBus);



                ParameterDiscreteValue parmDaysFrom = new ParameterDiscreteValue();
                parmDaysFrom.Value = CreditFrom;
                cryRpt.ParameterFields["@xOSDAYFrom"].CurrentValues.Add(parmDaysFrom);

                ParameterDiscreteValue paramDaysTo = new ParameterDiscreteValue();
                paramDaysTo.Value = CreditTo;
                cryRpt.ParameterFields["@xOSDAYUpto"].CurrentValues.Add(paramDaysTo);

                ParameterDiscreteValue paramOsAmtFrom = new ParameterDiscreteValue();
                paramOsAmtFrom.Value = MnthFrm;
                cryRpt.ParameterFields["@xOSAMTFrom"].CurrentValues.Add(paramOsAmtFrom);

                ParameterDiscreteValue paramOsAmtTo = new ParameterDiscreteValue();
                paramOsAmtTo.Value = MnthTo;
                cryRpt.ParameterFields["@xOSAMTUpto"].CurrentValues.Add(paramOsAmtTo);

                ParameterDiscreteValue parmDlrWithFrom = new ParameterDiscreteValue();
                parmDlrWithFrom.Value = CreditFrom;
                cryRpt.ParameterFields["@xDlrWithUsFrom"].CurrentValues.Add(parmDlrWithFrom);

                ParameterDiscreteValue paramDlrWithTo = new ParameterDiscreteValue();
                paramDlrWithTo.Value = CreditTo;
                cryRpt.ParameterFields["@xDlrWithUsUpto"].CurrentValues.Add(paramDlrWithTo);

                ParameterDiscreteValue paramOndate = new ParameterDiscreteValue();
                paramOndate.Value = DateTime.Today.ToString("dd/MMM/yyyy");
                cryRpt.ParameterFields["@xAsOnDate"].CurrentValues.Add(paramOndate);

                ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                paramRepType.Value = Report_Type;
                cryRpt.ParameterFields["@xReportType"].CurrentValues.Add(paramRepType);
            }
            #endregion

            #region "This is used for InvoicePrint"
            if (CommonData.ViewReport == "DL_REP_INVOICE")
            {
                cryRpt.Load(strPath + "\\Reports\\DealerReports\\DL_REP_INVOICE.rpt");

                ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                paramComp.Value = "";
                cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                paramBranch.Value = "";
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                ParameterDiscreteValue paramFin = new ParameterDiscreteValue();
                paramFin.Value = "";
                cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramFin);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = "";
                cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDocMonth);


                ParameterDiscreteValue paramTrnNo = new ParameterDiscreteValue();
                paramTrnNo.Value = rSalesInvoice;
                cryRpt.ParameterFields["@xTrnNumber"].CurrentValues.Add(paramTrnNo);

                ParameterDiscreteValue paramTotalInv = new ParameterDiscreteValue();
                paramTotalInv.Value = DocMonth;
                cryRpt.ParameterFields["@xTotalInvoiceAmount"].CurrentValues.Add(paramTotalInv);

            }
            #endregion

            #region "This is used for NewInvoicePrint"
            if (CommonData.ViewReport == "DL_INV_REPORT")
            {
                cryRpt.Load(strPath + "\\Reports\\DealerReports\\DL_INV_REPORT.rpt");

                ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                paramComp.Value = "";
                cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                paramBranch.Value = "";
                cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                ParameterDiscreteValue paramFin = new ParameterDiscreteValue();
                paramFin.Value = "";
                cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramFin);

                ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                paramDocMonth.Value = "";
                cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDocMonth);


                ParameterDiscreteValue paramTrnNo = new ParameterDiscreteValue();
                paramTrnNo.Value = Report_Type;
                cryRpt.ParameterFields["@xTrnNo"].CurrentValues.Add(paramTrnNo);

                //ParameterDiscreteValue paramTotalInv = new ParameterDiscreteValue();
                //paramTotalInv.Value = DocMonth;
                //cryRpt.ParameterFields["@xTotalInvoiceAmount"].CurrentValues.Add(paramTotalInv);

            }
            #endregion


            SetDataSourceConnectionToReport(ref cryRpt);
            rptViewer.ReportSource = cryRpt;
            rptViewer.Refresh();
        }


        #region SetDataSourceConnectionToReport
        private void SetDataSourceConnectionToReport(ref ReportDocument InvoiceRepDocument)
        {
            try
            {
                ConnectionInfo SQLConnection;
                SqlConnectionStringBuilder connectionParser;
                TableLogOnInfo RepTableLogOnInfo = null;
                SQLConnection = new ConnectionInfo();
                objSecurity = new Security();
                connectionParser = new SqlConnectionStringBuilder(objSecurity.GetDecodeString(ConfigurationManager.AppSettings["DBCon"].ToString()));
                SQLConnection.ServerName = connectionParser.DataSource.ToUpper();
                SQLConnection.DatabaseName = connectionParser.InitialCatalog.ToUpper();
                SQLConnection.IntegratedSecurity = connectionParser.IntegratedSecurity;
                SQLConnection.UserID = connectionParser.UserID;
                SQLConnection.Password = connectionParser.Password;
                for (int CntSr = 0; CntSr < InvoiceRepDocument.Subreports.Count; CntSr++)
                {
                    for (int Cnt = 0; Cnt < InvoiceRepDocument.Subreports[CntSr].Database.Tables.Count; Cnt++)
                    {
                        RepTableLogOnInfo = InvoiceRepDocument.Subreports[CntSr].Database.Tables[Cnt].LogOnInfo;
                        RepTableLogOnInfo.ConnectionInfo.ServerName = SQLConnection.ServerName;
                        RepTableLogOnInfo.ConnectionInfo.DatabaseName = SQLConnection.DatabaseName;
                        RepTableLogOnInfo.ConnectionInfo.UserID = SQLConnection.UserID;
                        RepTableLogOnInfo.ConnectionInfo.Password = SQLConnection.Password;
                        InvoiceRepDocument.Subreports[CntSr].Database.Tables[Cnt].ApplyLogOnInfo(RepTableLogOnInfo);
                        InvoiceRepDocument.Subreports[CntSr].Database.Tables[Cnt].Location = SQLConnection.DatabaseName + ".dbo." + InvoiceRepDocument.Subreports[CntSr].Database.Tables[Cnt].Location.Replace(";1", "");
                    }

                }

                for (int Cnt = 0; Cnt < InvoiceRepDocument.Database.Tables.Count; Cnt++)
                {
                    RepTableLogOnInfo = InvoiceRepDocument.Database.Tables[Cnt].LogOnInfo;
                    RepTableLogOnInfo.ConnectionInfo.ServerName = SQLConnection.ServerName;
                    RepTableLogOnInfo.ConnectionInfo.DatabaseName = SQLConnection.DatabaseName;
                    RepTableLogOnInfo.ConnectionInfo.IntegratedSecurity = SQLConnection.IntegratedSecurity;
                    RepTableLogOnInfo.ConnectionInfo.UserID = SQLConnection.UserID;
                    RepTableLogOnInfo.ConnectionInfo.Password = SQLConnection.Password;
                    InvoiceRepDocument.Database.Tables[Cnt].ApplyLogOnInfo(RepTableLogOnInfo);
                    InvoiceRepDocument.Database.Tables[Cnt].Location = SQLConnection.DatabaseName + ".dbo." + InvoiceRepDocument.Database.Tables[Cnt].Location.Replace(";1", "");

                }
            }
            catch (Exception ex)
            {
                string sErrMsg = ex.Message.ToString();
                MessageBox.Show(sErrMsg);
            }

        }

        #endregion
    }
}
