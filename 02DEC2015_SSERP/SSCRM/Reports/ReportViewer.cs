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

namespace SSCRM
{
    public partial class ReportViewer : Form
    {
        public Security objSecurity = new Security();
        public ReportViewer()
        {
            InitializeComponent();
        }
        public int iApprovedId = 0, E_code = 0, iFrom = 0, iTo = 0, iRes = 0;
        public string CompanyCode = "", BranchCode = "", DocMonth = "", FinYear = "", rSalesInvoice = "", LeadType = "", Demo_Type = "", Report_Type = "";


        public int frmMnths = 0, ToMnths = 0, frmGrps = 0, ToGrps = 0, frmPersPoints = 0,
                   ToPersPoints = 0, frmPntsPerGrp = 0, ToPntsPerGrp = 0, frmPntsPerHead = 0, ToPntsPerHead = 0;
        public string strVal1 = "", strVal2 = "", strVal3 = "", strVal4 = "", strVal5="";
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
        public ReportViewer(string sVal1, string sVal2, string sVal3, string sVal4, string sVal5, string sVal6, string sVal7)
        {
            InitializeComponent();
            CompanyCode = sVal1;
            BranchCode = sVal2;
            FinYear = sVal3;
            DocMonth = sVal4;
            LeadType = sVal5;
            Demo_Type = sVal6;
            Report_Type = sVal7;
        }
        public ReportViewer(string sVal1, string sVal2, string sVal3, string sVal4, string sVal5, string sVal6, string sVal7, string sVal8)
        {
            InitializeComponent();
            CompanyCode = sVal1;
            BranchCode = sVal2;
            FinYear = sVal3;
            DocMonth = sVal4;
            LeadType = sVal5;
            Demo_Type = sVal6;
            Report_Type = sVal7;
            strVal1 = sVal8;
        }
        public ReportViewer(string sVal1, string sVal2, string sVal3, string sVal4, string sVal5, Int32 nQty, string RepType)
        {
            InitializeComponent();
            CompanyCode = sVal1;
            BranchCode = sVal2;
            FinYear = sVal3;
            DocMonth = sVal4;
            LeadType = sVal5;
            iFrom = nQty;
            Demo_Type = RepType;

        }
        public ReportViewer(string sVal1, string sVal2, int sVal3, int sVal4, string sVal5, string sVal6, string sVal7)
        {
            InitializeComponent();
            CompanyCode = sVal1;
            BranchCode = sVal2;
            iFrom = sVal3;
            iTo = sVal4;
            LeadType = sVal5;
            Demo_Type = sVal6;
            Report_Type = sVal7;
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
        public ReportViewer(string sVal1, string sVal2, string sVal3, string sVal4, string sVal5, string sVal6, string sVal7, string sVal8, string sVal9, string sVal10, string sVal11, string sVal12)
        {
            InitializeComponent();

            CompanyCode = sVal1;
            BranchCode = sVal2;
            DocMonth = sVal3;
            FinYear = sVal4;
            rSalesInvoice = sVal5;
            LeadType = sVal6;
            Demo_Type = sVal7;
            Report_Type = sVal8;
            strVal1 = sVal9;
            strVal2 = sVal10;
            strVal3 = sVal11;
            strVal4 = sVal12;

        }
        public ReportViewer(string sVal1, string sVal2, string sVal3, string sVal4, string sVal5, Int32 iVal6, Int32 iVal7, Int32 iVal8, Int32 iVal9, Int32 iVal10, string sVal11, string sVal12, string sVal13)
        {

            InitializeComponent();

            CompanyCode = sVal1;
            BranchCode = sVal2;
            DocMonth = sVal3;
            FinYear = sVal4;
            rSalesInvoice = sVal5;
            iApprovedId = iVal6;
            E_code = iVal7;
            iFrom = iVal8;
            iTo = iVal9;
            iRes = iVal10;
            LeadType = sVal11;
            Demo_Type = sVal12;
            Report_Type = sVal13;

        }
        public ReportViewer(string sVal1, string sVal2, string sVal3, string sVal4, string sVal5, string sVal6, string sVal7, string sVal10, Int32 nVal8, Int32 nVal9)
        {
            InitializeComponent();

            CompanyCode = sVal1;
            BranchCode = sVal2;
            DocMonth = sVal3;
            FinYear = sVal4;
            rSalesInvoice = sVal5;
            LeadType = sVal6;
            Demo_Type = sVal7;
            iFrom = nVal8;
            iTo = nVal9;
            strVal2 = sVal10;

        }
        public ReportViewer(string CCode, string BCode, string DMonth, string sLeadType, Int32 nEcode, string sRepType)
        {
            InitializeComponent();
            CompanyCode = CCode;
            BranchCode = BCode;
            DocMonth = DMonth;
            LeadType = sLeadType;
            E_code = nEcode;
            Report_Type = sRepType;
        }


        public ReportViewer(string CCode, string BCode, int Ecode, string FrmDMonth, string ToDMonth,
                  string sMnthFlag, Int32 nFrmMnths, Int32 nToMnths, string sGrpsFlag, Int32 nFrmGrps, Int32 nToGrps, string PersPntsFlag,
                  Int32 frmPnts, Int32 ToPnts, string sPntsPerGrpFlag, Int32 nFrmPntsPerGrp, Int32 nToPntsPerGrp,
                  string PerHeadFlag, Int32 nFrmPntsPerHead, Int32 nToPntsPerHead, string sortBy, string RepType, string sPntsHFlag,string sLosDate)
        {
            InitializeComponent();

            CompanyCode = CCode;
            BranchCode = BCode;
            DocMonth = FrmDMonth;
            FinYear = ToDMonth;
            rSalesInvoice = sMnthFlag;
            LeadType = sGrpsFlag;
            Demo_Type = PersPntsFlag;
            strVal2 = sPntsPerGrpFlag;
            strVal1 = PerHeadFlag;
            strVal3 = sortBy;
            Report_Type = RepType;
            frmMnths = nFrmMnths;
            ToMnths = nToMnths;
            frmGrps = nFrmGrps;
            ToGrps = nToGrps;
            frmPersPoints = frmPnts;
            ToPersPoints = ToPnts;
            frmPntsPerGrp = nFrmPntsPerGrp;
            ToPntsPerGrp = nToPntsPerGrp;
            frmPntsPerHead = nFrmPntsPerHead;
            ToPntsPerHead = nToPntsPerHead;
            strVal4 = sPntsHFlag;
            E_code = Ecode;
            strVal5 = sLosDate;
        }

        public ReportViewer(string sVal1, string sVal2, string sVal3, Int32 iVal4, Int32 iVal5, Int32 iVal6, Int32 iVal7, string sVal18, string sVal9)
        {

            InitializeComponent();

            CompanyCode = sVal1;
            BranchCode = sVal2;
            DocMonth = sVal3;

            iApprovedId = iVal4;
            E_code = iVal5;
            iFrom = iVal6;
            iTo = iVal7;

            LeadType = sVal18;
            Demo_Type = sVal9;

        }



        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (CommonData.ViewReport == "SERVICE_ACTIVITY_DETAILS")
            {
                SSCRM.Reports.SSCRM_REP_SERVICE_ACTIVITY_DETLX1 crp = new SSCRM.Reports.SSCRM_REP_SERVICE_ACTIVITY_DETLX1();
                SSCRM.Reports.Invoice.DSActivityService DSService = new SSCRM.Reports.Invoice.DSActivityService();
                crp.SetDataSource(Ds_Service.Tables[0]);
                rptViewer.ReportSource = crp;
                rptViewer.Refresh();
            }
            else if (CommonData.ViewReport == "SSHR_HRINFORMATION")
            {
                SSCRM.Reports.SSCRM_REP_HR_INFORMATION crp = new SSCRM.Reports.SSCRM_REP_HR_INFORMATION();
                crp.SetDataSource(Ds_Service);
                rptViewer.ReportSource = crp;
                rptViewer.Refresh();
            }
            else
                GetLoad();
        }

        public void GetLoad()
        {
            try
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

                #region "InvoiceDetailSplitting"
                if (CommonData.ViewReport == "InvoiceDetailSplitting")
                {
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSERP_REP_SP_SALESREGReport1.rpt");

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
                    paramRepType.Value = "INVOICE WISE";
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
                    
                    if (Report_Type == "Detailed")
                        cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_DOCMM_GROUPSX.rpt");
                    else
                        cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_DOCMM_GROUPS_OnlyX.rpt");

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
                    if (Report_Type == "Detailed")
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

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@RepType"].CurrentValues.Add(paramRepType);

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

                #region "This is used for SSCRM_SERVICE_REP_CHR"
                if (CommonData.ViewReport == "SSCRM_SERVICE_REP_CHR")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_SERVICE_CHR_Report.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = LeadType;
                    cryRpt.ParameterFields["@xFRDT"].CurrentValues.Add(paramEcode);


                    ParameterDiscreteValue paramTodate = new ParameterDiscreteValue();
                    paramTodate.Value = Demo_Type;
                    cryRpt.ParameterFields["@xTODT"].CurrentValues.Add(paramTodate);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Report_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramStatus);
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
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\SSCRM_SALES_STAFF_STRENGTH.rpt");

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

                #region "This Is Used STATIONARY STORE RECONSILATION"
                if (CommonData.ViewReport == "SSCRM_REP_STATIONARY_RECONSILATION_STORE")
                {

                    cryRpt.Load(strPath + "\\Reports\\StationaryReports\\SSCRM_REP_STATIONARY_RECONSILATION_STORE.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFromDate = new ParameterDiscreteValue();
                    paramFromDate.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFromDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = LeadType;
                    cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@RepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This Is Used STATIONARY PENDING KNOCKING  REGISTER"
                if (CommonData.ViewReport == "PENDING KNOCKING REGISTER")
                {

                    cryRpt.Load(strPath + "\\Reports\\StationaryReports\\SSERP_REP_STATIONARY_PENDING_KNOCHING_REGISTER.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFromDate = new ParameterDiscreteValue();
                    paramFromDate.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFromDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = LeadType;
                    cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for EMP_STATIONRY_INDENT REPORT"
                if (CommonData.ViewReport == "SSCRM_REP_EMPLOYEE_STATIONARY_INDENT")
                {


                    cryRpt.Load(strPath + "\\Reports\\StationaryReports\\SSCRM_REP_EMPLOYEE_STATIONARY_INDENT.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = DocMonth;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = LeadType;
                    cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = Demo_Type;
                    cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Report_Type;
                    cryRpt.ParameterFields["@RepType"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "This Is Used STATIONAR SHORTAGE REGISTER"
                if (CommonData.ViewReport == "SSCRM_REP_STATIONARY_SHORTAGE_REGISTER")
                {

                    cryRpt.Load(strPath + "\\Reports\\StationaryReports\\SSCRM_REP_STATIONARY_SHORTAGE_REGISTER.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@BranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = DocMonth;
                    cryRpt.ParameterFields["@Finyear"].CurrentValues.Add(paramFinYear);


                    ParameterDiscreteValue paramFromDate = new ParameterDiscreteValue();
                    paramFromDate.Value = LeadType;
                    cryRpt.ParameterFields["@FromMonth"].CurrentValues.Add(paramFromDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = Demo_Type;
                    cryRpt.ParameterFields["@ToMonth"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@RepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This Is Used STATIONAR DC   REGISTER"
                if (CommonData.ViewReport == "SSERP_REP_STATIONARY_DELIVERY_CHALLAN_REGISTER")
                {

                    cryRpt.Load(strPath + "\\Reports\\StationaryReports\\SSERP_REP_STATIONARY_DELIVERY_CHALLAN_REGISTER.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFromDate = new ParameterDiscreteValue();
                    paramFromDate.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFromDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = LeadType;
                    cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
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
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_SP_PU_DC_REGISTER.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@Company"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@ToBranch"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = BranchCode;
                    cryRpt.ParameterFields["@Ecode"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@FromDate"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = LeadType;
                    cryRpt.ParameterFields["@ToDate"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Demo_Type;
                    cryRpt.ParameterFields["@RepType"].CurrentValues.Add(paramReportType);
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

                #region "This is used for SSERP_REP_SP_RENTAL_AGRIMENTS REPORT"
                if (CommonData.ViewReport == "SSERP_REP_SP_RENTAL_AGRIMENT_DETAILS")
                {
                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSERP_REP_SP_RENTAL_AGRIMENT_DETAILS.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = DocMonth;
                    cryRpt.ParameterFields["@xStateCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = LeadType;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
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

                #region "This is used for Employee_Salary_Structure_Details REPORT"
                if (CommonData.ViewReport == "Employee_Salary_Structure_Details")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\SSCRM_EMP_SAL_STRUCTURE.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = iApprovedId;
                    cryRpt.ParameterFields["@xApplNo"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = "";
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for Reconsilation_By_Destination_Branch REPORT"
                if (CommonData.ViewReport == "Reconsilation_By_Destination_Branch")
                {
                    cryRpt.Load(strPath + "\\Reports\\PUReports\\SSCRM_REP_PU_ALLDCDCST_FOR_DEST.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCOMP"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue paramDestBranch = new ParameterDiscreteValue();
                    paramDestBranch.Value = DocMonth;
                    cryRpt.ParameterFields["@xDestBranchCode"].CurrentValues.Add(paramDestBranch);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = LeadType;
                    cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = Demo_Type;
                    cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramTo);


                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
                }

                #endregion

                #region "This is used for Product_Price_Circulars_Print REPORT"
                if (CommonData.ViewReport == "SSCRM_REP_PRODUCTPRICE")
                {
                    cryRpt.Load(strPath + "\\Reports\\Masters\\SSCRM_REP_PRODUCT_PRICE.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xStateCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = DocMonth;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue paramWEFDate = new ParameterDiscreteValue();
                    paramWEFDate.Value = LeadType;
                    cryRpt.ParameterFields["@xWefDate"].CurrentValues.Add(paramWEFDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for BRANCH_WISE_HOLIDAY_MASTER REPORT"
                if (CommonData.ViewReport == "SSCRM_HOLIDAY_MASTER_BRANCH_WISE")
                {
                    cryRpt.Load(strPath + "\\Reports\\BioMetrics\\SSCRM_REP_BRANCH_HOLIDAYS_LIST.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramYear = new ParameterDiscreteValue();
                    paramYear.Value = Convert.ToInt32(DocMonth);
                    cryRpt.ParameterFields["@xYear"].CurrentValues.Add(paramYear);
                }
                #endregion

                #region "This is used for HOLIDAY_MASTER REPORT"
                if (CommonData.ViewReport == "SSCRM_HOLIDAYMASTER")
                {
                    cryRpt.Load(strPath + "\\Reports\\BioMetrics\\SSCRM_REP_HOLIDAYS_LIST.rpt");

                    ParameterDiscreteValue paramYear = new ParameterDiscreteValue();
                    paramYear.Value = iApprovedId;
                    cryRpt.ParameterFields["@xYear"].CurrentValues.Add(paramYear);
                }
                #endregion

                #region "This is used for SSCRM_HR_REP_EMP_CONTACT_DETAILS REPORT"
                if (CommonData.ViewReport == "SSCRM_HR_REP_EMP_CONTACT_DETAILS")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\SSCRM_HR_REP_EMP_CONTACT_DETAILS.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue paramAppl = new ParameterDiscreteValue();
                    paramAppl.Value = "";
                    cryRpt.ParameterFields["@xApplNo"].CurrentValues.Add(paramAppl);

                    ParameterDiscreteValue paramDept = new ParameterDiscreteValue();
                    paramDept.Value = "";
                    cryRpt.ParameterFields["@xDeptID"].CurrentValues.Add(paramDept);

                    ParameterDiscreteValue paramDesg = new ParameterDiscreteValue();
                    paramDesg.Value = "";
                    cryRpt.ParameterFields["@xDesgCode"].CurrentValues.Add(paramDesg);

                    ParameterDiscreteValue paramDOB = new ParameterDiscreteValue();
                    paramDOB.Value = "";
                    cryRpt.ParameterFields["@xDOB"].CurrentValues.Add(paramDOB);

                    ParameterDiscreteValue paramDOJ = new ParameterDiscreteValue();
                    paramDOJ.Value = "";
                    cryRpt.ParameterFields["@xDOM"].CurrentValues.Add(paramDOJ);
                }
                #endregion

                #region "This is used for SSCRM_REP_APPROVALS_BY_DATE REPORT"
                if (CommonData.ViewReport == "SSCRM_REP_APPROVALS_BY_DATE")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\SSCRM_REP_APPROVALS_BY_DATE.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue paramFDate = new ParameterDiscreteValue();
                    paramFDate.Value = LeadType;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramToDate);
                }
                #endregion

                #region "This is used for SSBPLHO_ATTD"
                if (CommonData.ViewReport == "SSBPLHO_ATTD")
                {

                    cryRpt.Load(strPath + "\\Reports\\BioMetrics\\SSBPLHO_ATTD.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = "ALL";
                    //paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramDept = new ParameterDiscreteValue();
                    paramDept.Value = "ALL";
                    cryRpt.ParameterFields["@xDept_cd"].CurrentValues.Add(paramDept);

                    ParameterDiscreteValue paramFDate = new ParameterDiscreteValue();
                    paramFDate.Value = crReportParams.FromDate;
                    cryRpt.ParameterFields["@FR_DATE"].CurrentValues.Add(paramFDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = crReportParams.ToDate;
                    cryRpt.ParameterFields["@TO_DATE"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramLate = new ParameterDiscreteValue();
                    paramLate.Value = Convert.ToDouble(iFrom);
                    cryRpt.ParameterFields["@LateAllowed"].CurrentValues.Add(paramLate);

                    ParameterDiscreteValue paramEarly = new ParameterDiscreteValue();
                    paramEarly.Value = Convert.ToDouble(iTo);
                    cryRpt.ParameterFields["@EarlyGoAllowed"].CurrentValues.Add(paramEarly);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@REP_TYPE"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for SSBPLHO_ATTD_LateComing"
                if (CommonData.ViewReport == "SSBPLHO_ATTD_LateComing")
                {

                    cryRpt.Load(strPath + "\\Reports\\BioMetrics\\SSBPLHO_ATTD_LateComing.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = "ALL";
                    //paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramDept = new ParameterDiscreteValue();
                    paramDept.Value = "ALL";
                    cryRpt.ParameterFields["@xDept_cd"].CurrentValues.Add(paramDept);

                    ParameterDiscreteValue paramFDate = new ParameterDiscreteValue();
                    paramFDate.Value = crReportParams.FromDate;
                    cryRpt.ParameterFields["@FR_DATE"].CurrentValues.Add(paramFDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = crReportParams.ToDate;
                    cryRpt.ParameterFields["@TO_DATE"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramLate = new ParameterDiscreteValue();
                    paramLate.Value = Convert.ToDouble(iFrom);
                    cryRpt.ParameterFields["@LateAllowed"].CurrentValues.Add(paramLate);

                    ParameterDiscreteValue paramEarly = new ParameterDiscreteValue();
                    paramEarly.Value = Convert.ToDouble(iTo);
                    cryRpt.ParameterFields["@EarlyGoAllowed"].CurrentValues.Add(paramEarly);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@REP_TYPE"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for HR_REP_LATE_COMMINGS"
                if (CommonData.ViewReport == "HR_REP_LATE_COMMINGS")
                {

                    cryRpt.Load(strPath + "\\Reports\\BioMetrics\\HR_REP_LATE_COMMINGS.rpt");
                    
                    ParameterDiscreteValue paramFDate = new ParameterDiscreteValue();
                    paramFDate.Value = LeadType;
                    cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = Demo_Type;
                    cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = BranchCode;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramEcode);
                    
                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for HR_REP_LATE_COMMINGS_SUMMARY"
                if (CommonData.ViewReport == "HR_REP_LATE_COMMINGS_SUMMARY")
                {
                    cryRpt.Load(strPath + "\\Reports\\BioMetrics\\HR_REP_LATE_COMMINGS_SUMMARY.rpt");

                    ParameterDiscreteValue paramFDate = new ParameterDiscreteValue();
                    paramFDate.Value = LeadType;
                    cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = Demo_Type;
                    cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for HR_REP_EMPS_SALAREES_STATEMENT"
                if (CommonData.ViewReport == "HR_REP_EMPS_SALAREES_STATEMENT")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\PayRoll\\HR_REP_EMPS_SALAREES_STATEMENT.rpt");

                    ParameterDiscreteValue paramCCode = new ParameterDiscreteValue();
                    paramCCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramCCode);

                    ParameterDiscreteValue paramBCode = new ParameterDiscreteValue();
                    paramBCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBCode);

                    ParameterDiscreteValue paramBType = new ParameterDiscreteValue();
                    paramBType.Value = FinYear;
                    cryRpt.ParameterFields["@xBranchType"].CurrentValues.Add(paramBType);

                    ParameterDiscreteValue paramDCode = new ParameterDiscreteValue();
                    paramDCode.Value = DocMonth;
                    cryRpt.ParameterFields["@xDeptCode"].CurrentValues.Add(paramDCode);

                    ParameterDiscreteValue paramMonth = new ParameterDiscreteValue();
                    paramMonth.Value = LeadType;
                    cryRpt.ParameterFields["@xMonth"].CurrentValues.Add(paramMonth);

                    ParameterDiscreteValue paramDate = new ParameterDiscreteValue();
                    paramDate.Value = Demo_Type;
                    cryRpt.ParameterFields["@xDate"].CurrentValues.Add(paramDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for DL_REP_ORDER_BOOKING_REG"
                if (CommonData.ViewReport == "DL_REP_ORDER_BOOKING_REG")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\DL_REP_ORDER_BOOKING_REG.rpt");

                    ParameterDiscreteValue paramCCode = new ParameterDiscreteValue();
                    paramCCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramCCode);

                    ParameterDiscreteValue paramBCode = new ParameterDiscreteValue();
                    paramBCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBCode);

                    ParameterDiscreteValue paramBType = new ParameterDiscreteValue();
                    paramBType.Value = iFrom;
                    cryRpt.ParameterFields["@xSREcode"].CurrentValues.Add(paramBType);

                    ParameterDiscreteValue paramDCode = new ParameterDiscreteValue();
                    paramDCode.Value = iTo;
                    cryRpt.ParameterFields["@xDealerCode"].CurrentValues.Add(paramDCode);

                    ParameterDiscreteValue paramMonth = new ParameterDiscreteValue();
                    paramMonth.Value = LeadType;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramMonth);

                    ParameterDiscreteValue paramDate = new ParameterDiscreteValue();
                    paramDate.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for SSCRM_REP_PRODUCT_LICENCES_DETAILS"
                if (CommonData.ViewReport == "SSCRM_REP_PRODUCT_LICENCES_DETAILS")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_PRODUCT_LICENCES_DETAILS.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xStateCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDistrict"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = LeadType;
                    cryRpt.ParameterFields["@xStatus"].CurrentValues.Add(paramStatus);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);
                }
                #endregion

                #region "This is used for GROUP_AND_INDIVIDUAL_PERF_CROSSTAB"
                if (CommonData.ViewReport == "GROUP_AND_INDIVIDUAL_PERF_CROSSTAB")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_YTD_SALE_BULTIN_GRP_N_INDV_CRTAB_Repz.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_Month"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for GROUP_AND_INDIVIDUAL_PERF_CROSSTAB"
                if (CommonData.ViewReport == "FOUNDATION_EYE_CAMP_REG")
                {

                    cryRpt.Load(strPath + "\\Reports\\Foundation\\FOUNDATION_EYE_CAMP_REG.rpt");

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = "";
                    cryRpt.ParameterFields["@xState"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = "";
                    cryRpt.ParameterFields["@xDist"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramMandal = new ParameterDiscreteValue();
                    paramMandal.Value = "";
                    cryRpt.ParameterFields["@xMandal"].CurrentValues.Add(paramMandal);

                    ParameterDiscreteValue paramVillage = new ParameterDiscreteValue();
                    paramVillage.Value = "";
                    cryRpt.ParameterFields["@xVillage"].CurrentValues.Add(paramVillage);

                    ParameterDiscreteValue paramCampNo = new ParameterDiscreteValue();
                    paramCampNo.Value = "";
                    cryRpt.ParameterFields["@xCampNo"].CurrentValues.Add(paramCampNo);

                    ParameterDiscreteValue paramFmDate = new ParameterDiscreteValue();
                    paramFmDate.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFmDate);


                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = DocMonth;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = "";
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for REP_BRANCH_GROUP_SR_WISE_PRODUCT_REC_SUMMARY"
                if (CommonData.ViewReport == "REP_BRANCH_GROUP_SR_WISE_PRODUCT_REC_SUMMARY")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_SALE_BULTIN_GCSTOCK_CRTAB_TotalsOnly.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_Month"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = "0";
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = "GCSUM";
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for REP_BRANCH_GROUP_SR_WISE_CRATES_REC_SUMMARY"
                if (CommonData.ViewReport == "REP_BRANCH_GROUP_SR_WISE_CRATES_REC_SUMMARY")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_SALE_BULTIN_GCSTOCK_CRTAB_TotalsOnly.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_Month"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = "0";
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = "GCSUM_CRTS";
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for REP_BRANCH_GROUP_WISE_PRODUCT_REC_SUMMARY"
                if (CommonData.ViewReport == "REP_BRANCH_GROUP_WISE_PRODUCT_REC_SUMMARY")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_SALE_BULTIN_GCSTOCK_CRTABx.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_Month"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = "0";
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = "SUM";
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                }
                #endregion
                
                #region "This is used for SSCRM_SERVICE_REP_CHR"
                if (CommonData.ViewReport == "SSCRM_SERVICE_REP_CHR")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_SERVICE_CHR_Report.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = LeadType;
                    cryRpt.ParameterFields["@xFRDT"].CurrentValues.Add(paramEcode);


                    ParameterDiscreteValue paramTodate = new ParameterDiscreteValue();
                    paramTodate.Value = Demo_Type;
                    cryRpt.ParameterFields["@xTODT"].CurrentValues.Add(paramTodate);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Report_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for PRINT_SALES_APPT_LETTER"
                if (CommonData.ViewReport == "PRINT_SALES_APPT_LETTER")
                {

                    cryRpt.Load(strPath + "\\Reports\\SSHRIS_REP_SALES_APPOINTMENT_LETTER.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xApplNo"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = "0";
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramTodate = new ParameterDiscreteValue();
                    paramTodate.Value = DocMonth;
                    cryRpt.ParameterFields["@xLetterRefNo"].CurrentValues.Add(paramTodate);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);

                    cryRpt.SetParameterValue("@xCompCode", "", cryRpt.Subreports[0].Name.ToString());
                    cryRpt.SetParameterValue("@xBranchCode", "", cryRpt.Subreports[0].Name.ToString());
                    cryRpt.SetParameterValue("@xApplNo", rSalesInvoice, cryRpt.Subreports[0].Name.ToString());
                    cryRpt.SetParameterValue("@xECode", "0", cryRpt.Subreports[0].Name.ToString());
                    cryRpt.SetParameterValue("@xLetterRefNo", "", cryRpt.Subreports[0].Name.ToString());
                    cryRpt.SetParameterValue("@xRepType", "", cryRpt.Subreports[0].Name.ToString());

                    cryRpt.SetParameterValue("@xCompCode", "", cryRpt.Subreports[1].Name.ToString());
                    cryRpt.SetParameterValue("@xBranchCode", "", cryRpt.Subreports[1].Name.ToString());
                    cryRpt.SetParameterValue("@xApplNo", rSalesInvoice, cryRpt.Subreports[1].Name.ToString());
                    cryRpt.SetParameterValue("@xECode", "0", cryRpt.Subreports[1].Name.ToString());
                    cryRpt.SetParameterValue("@xLetterRefNo", "", cryRpt.Subreports[1].Name.ToString());
                    cryRpt.SetParameterValue("@xRepType", "", cryRpt.Subreports[1].Name.ToString());

                }
                #endregion

                #region "This is used for PROMOTION_BOARD_TRN_LETTER"
                if (CommonData.ViewReport == "PROMOTION_BOARD_TRN_LETTER")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\PromotionBoard\\PROMOTION_BOARD_TRN_LETTER.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xApplNo"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = LeadType;
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramTodate = new ParameterDiscreteValue();
                    paramTodate.Value = Demo_Type;
                    cryRpt.ParameterFields["@xLetterRefNo"].CurrentValues.Add(paramTodate);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for SSCRM_REP_TRIPSHEET_DOCISSU"
                if (CommonData.ViewReport == "SSCRM_REP_TRIPSHEET_DOCISSU")
                {
                    cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_TRIPSHEET_DOCISSU.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = DocMonth;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                }
                #endregion

                #region "This is used for IT_SYS_INV_CPU"
                if (CommonData.ViewReport == "IT_SYS_INV_CPU")
                {
                    cryRpt.Load(strPath + "\\Reports\\FixedAssets\\REP_FIXED_ASSETS_SYSTEM_INVENTORY.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue paramAsset = new ParameterDiscreteValue();
                    paramAsset.Value = "CPU";
                    cryRpt.ParameterFields["@xAssetType"].CurrentValues.Add(paramAsset);

                }
                #endregion

                #region "This is used for REP_BRANCH_GROUP_WISE_SALES_ACCOUNTABILITY"
                if (CommonData.ViewReport == "REP_BRANCH_GROUP_WISE_SALES_ACCOUNTABILITY")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_SALE_BULTIN_ACCOUNTABILITY.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_Month"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = "ALL";
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = "SUM";
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for REP_BRANCH_GCGL_SALES_ACCOUNTABILITY"
                if (CommonData.ViewReport == "REP_BRANCH_GCGL_SALES_ACCOUNTABILITY")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_SALE_BULTIN_ACCOUNTABILITY_GCONLYx.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_Month"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = "ALL";
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for SSBPLHO_ATTD"
                if (CommonData.ViewReport == "SSBPLHO_ATTD")
                {

                    cryRpt.Load(strPath + "\\Reports\\BioMetrics\\SSBPLHO_ATTD.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = "ALL";
                    //paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramDept = new ParameterDiscreteValue();
                    paramDept.Value = "ALL";
                    cryRpt.ParameterFields["@xDept_cd"].CurrentValues.Add(paramDept);

                    ParameterDiscreteValue paramFDate = new ParameterDiscreteValue();
                    paramFDate.Value = crReportParams.FromDate;
                    cryRpt.ParameterFields["@FR_DATE"].CurrentValues.Add(paramFDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = crReportParams.ToDate;
                    cryRpt.ParameterFields["@TO_DATE"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramLate = new ParameterDiscreteValue();
                    paramLate.Value = Convert.ToDouble(iFrom);
                    cryRpt.ParameterFields["@LateAllowed"].CurrentValues.Add(paramLate);

                    ParameterDiscreteValue paramEarly = new ParameterDiscreteValue();
                    paramEarly.Value = Convert.ToDouble(iTo);
                    cryRpt.ParameterFields["@EarlyGoAllowed"].CurrentValues.Add(paramEarly);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@REP_TYPE"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for SSBPLHO_ATTD_LateComing"
                if (CommonData.ViewReport == "SSBPLHO_ATTD_LateComing")
                {

                    cryRpt.Load(strPath + "\\Reports\\BioMetrics\\SSBPLHO_ATTD_LateComing.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = "ALL";
                    //paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramDept = new ParameterDiscreteValue();
                    paramDept.Value = "ALL";
                    cryRpt.ParameterFields["@xDept_cd"].CurrentValues.Add(paramDept);

                    ParameterDiscreteValue paramFDate = new ParameterDiscreteValue();
                    paramFDate.Value = crReportParams.FromDate;
                    cryRpt.ParameterFields["@FR_DATE"].CurrentValues.Add(paramFDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = crReportParams.ToDate;
                    cryRpt.ParameterFields["@TO_DATE"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramLate = new ParameterDiscreteValue();
                    paramLate.Value = Convert.ToDouble(iFrom);
                    cryRpt.ParameterFields["@LateAllowed"].CurrentValues.Add(paramLate);

                    ParameterDiscreteValue paramEarly = new ParameterDiscreteValue();
                    paramEarly.Value = Convert.ToDouble(iTo);
                    cryRpt.ParameterFields["@EarlyGoAllowed"].CurrentValues.Add(paramEarly);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@REP_TYPE"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for SSCRM_REP_EMP_LEAVE_DETAILS"
                if (CommonData.ViewReport == "SSCRM_REP_EMP_LEAVE_DETAILS")
                {
                    cryRpt.Load(strPath + "\\Reports\\BioMetrics\\SSCRM_LEAVE_DETAILS.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = E_code;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);

                }
                #endregion

                #region "This is used for SSCRM_REP_LEGAL_CASE_DETAILS"
                if (CommonData.ViewReport == "SSCRM_REP_LEGAL_CASE_DETAILS")
                {
                    cryRpt.Load(strPath + "\\Reports\\Legal\\SSCRM_REP_LEGAL_CASE_DETAILS.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue paramCaseType = new ParameterDiscreteValue();
                    paramCaseType.Value = DocMonth;
                    cryRpt.ParameterFields["@xCaseType"].CurrentValues.Add(paramCaseType);

                    ParameterDiscreteValue paramFromDate = new ParameterDiscreteValue();
                    paramFromDate.Value = LeadType;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFromDate);

                    ParameterDiscreteValue paramTodate = new ParameterDiscreteValue();
                    paramTodate.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramTodate);

                }
                #endregion

                #region "This is used for SSCRM_SERVICE_REP_SCHOOL_VISIT"
                if (CommonData.ViewReport == "SSCRM_REP_SCHOOL_VISIT_DETAILS")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_SCHOOL_VISIT_DETAILS.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDist);


                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for SSCRM_SERVICE_REP_FARMER_MEETINGS_DETAILS"
                if (CommonData.ViewReport == "SSCRM_REP_FARMER_MEETINGS_SUMMARY")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_FARMER_MEETINGS_SUMMARY.rpt");


                    ParameterDiscreteValue paramTrnNo = new ParameterDiscreteValue();
                    paramTrnNo.Value = Report_Type;
                    cryRpt.ParameterFields["@sTransactionNo"].CurrentValues.Add(paramTrnNo);

                }
                #endregion

                #region "This is used for SSCRM_REP_DEMO_PLOTS_SUMMARY_DETAILS"
                if (CommonData.ViewReport == "SSCRM_REP_DEMO_PLOTS_SUMMARY")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_DEMO_PLOTS_SUMMARY.rpt");


                    ParameterDiscreteValue paramTrnNo = new ParameterDiscreteValue();
                    paramTrnNo.Value = Report_Type;
                    cryRpt.ParameterFields["@sTrnNo"].CurrentValues.Add(paramTrnNo);

                }
                #endregion

                #region "This is used for SSCRM_REP_SCHOOL_VISITS_SUMMARY_DETAILS"
                if (CommonData.ViewReport == "SSCRM_REP_SCHOOL_VISITS_SUMMARY")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_SCHOOL_VISITS_SUMMARY.rpt");


                    ParameterDiscreteValue paramTrnNo = new ParameterDiscreteValue();
                    paramTrnNo.Value = Report_Type;
                    cryRpt.ParameterFields["@sTransactionNo"].CurrentValues.Add(paramTrnNo);

                }
                #endregion

                #region "This is used for SSCRM_SERVICE_REP_FARMER_MEETINGS"
                if (CommonData.ViewReport == "SSCRM_REP_FARMER_MEETINGS_DETL")
                {


                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_FARMER_MEETINGS_DETL.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDist);


                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for SSCRM_SERVICE_REP_WRONG COMMITMENT/FINANCIAL FRAUDS_MEETINGS"
                if (CommonData.ViewReport == "SSCRM_REP_SERVICE_WC_FF_DETAILS")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_SERVICE_WC_FF_DETAILS.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDist);


                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for SSCRM_SERVICE_REP_DEMO_PLOTS_DETL"
                if (CommonData.ViewReport == "SSCRM_REP_SERVICE_DEMO_PLOTS")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_SERVICE_DEMO_PLOTS.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDist);


                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for SSCRM_REP_SERVICES_EMP_DAILY_ACTIVITIES"
                if (CommonData.ViewReport == "SSCRM_REP_SERVICES_EMP_DAILY_ACTIVITIES")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_SERVICES_EMP_DAILY_ACTIVITIES.rpt");


                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = E_code;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = Report_Type;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDocMonth);

                }
                #endregion

                #region "This is used for SSCRM_REP_GC_GL_COLLECTION_STATEMENT"
                if (CommonData.ViewReport == "SSCRM_REP_GC_GL_COLLECTION_STATEMENT")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_SALE_BULTIN_AMOUNTCOLL_CRTAB.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_Month"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = "ALL";
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = "SUM";
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for SSCRM_REP_GC_GL_COLLECTION_STATEMENT_2"
                if (CommonData.ViewReport == "SSCRM_REP_GC_GL_COLLECTION_STATEMENT_2")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_SALE_BULTIN_AMOUNTCOLL_CRTAB_2.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_Month"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = "ALL";
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = "SUM";
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                }
                #endregion


                #region "This is used for SSCRM_REP_GC_GL_CHECKLIST"
                if (CommonData.ViewReport == "SSCRM_REP_GC_GL_CHECKLIST")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_DOCMM_SUMM_CHECKLIST.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDist);

                    //ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    //paramEcode.Value = "ALL";
                    //cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = "SUMMARY";
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                    //System.Web.HttpResponse Response = new System.Web.HttpResponse();
                    //cryRpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "PersonDetails");
                    
                }
                #endregion

                #region "This is used for SSCRM_REP_BRANCH_BULTN_CHECKLIST"
                if (CommonData.ViewReport == "SSCRM_REP_BRANCH_BULTN_CHECKLIST")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_DOCMM_SUMM_CHECKLISTbronly.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDist);

                    //ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    //paramEcode.Value = "ALL";
                    //cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = "BRANCH SUMMARY";
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                    //System.Web.HttpResponse Response = new System.Web.HttpResponse();
                    //cryRpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "PersonDetails");

                }
                #endregion

                #region "This is used for SSCRM_REP_GCGL_BULTN_CHECKLIST"
                if (CommonData.ViewReport == "SSCRM_REP_GCGL_BULTN_CHECKLIST")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_DOCMM_SUMM_CHECKLISTgconly.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDist);

                    //ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    //paramEcode.Value = "ALL";
                    //cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = "GROUP SUMMARY";
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                    //System.Web.HttpResponse Response = new System.Web.HttpResponse();
                    //cryRpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "PersonDetails");

                }
                #endregion

                #region "This is used for REP_FIXED_ASSETS_DETAILS"
                if (CommonData.ViewReport == "REP_FIXED_ASSETS_DETAILS")
                {

                    cryRpt.Load(strPath + "\\Reports\\FixedAssets\\REP_FIXED_ASSETS_DETAILS.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xAssets"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = "";
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = "DETAILED";
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);

                }
                #endregion

                #region "This is used for REP_FIXED_ASSETS_DETAILS_SUMMARY"
                if (CommonData.ViewReport == "REP_FIXED_ASSETS_DETAILS_SUMMARY")
                {

                    cryRpt.Load(strPath + "\\Reports\\FixedAssets\\REP_FIXED_ASSETS_DETAILS_SUMMARY.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xAssets"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = "";
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = "SUMMARY_BY_BRANCH";
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);

                }
                #endregion

                #region "This is used for FIXED_ASSETS_ONE_SYSTEM_DETAILS"
                if (CommonData.ViewReport == "REP_FIXED_ASSETS_CONFIG_MAINTENANCE_DETL")
                {
                    cryRpt.Load(strPath + "\\Reports\\FixedAssets\\REP_FIXED_ASSETS_CONFIG_MAINTENANCE_DETL.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramAssetSlno = new ParameterDiscreteValue();
                    paramAssetSlno.Value = DocMonth;
                    cryRpt.ParameterFields["@xAssetSLNO"].CurrentValues.Add(paramAssetSlno);


                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Report_Type;
                    cryRpt.ParameterFields["@xReptype"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for SSCRM_REP_BRANCH_BULTN_CHECKLIST"
                if (CommonData.ViewReport == "SSCRM_REP_COFF")
                {

                    cryRpt.Load(strPath + "\\Reports\\BioMetrics\\SSCRM_REP_COFF.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = iApprovedId;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramComp);


                }
                #endregion

                #region "This is used for HR_HO_ATTD_MTODY"
                if (CommonData.ViewReport == "HR_HO_ATTD_MTODY")
                {

                    cryRpt.Load(strPath + "\\Reports\\BioMetrics\\HR_HO_ATTD_MTODY.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xWagePeriod"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue paramFin = new ParameterDiscreteValue();
                    paramFin.Value = DocMonth;
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramFin);

                    ParameterDiscreteValue paramDoc = new ParameterDiscreteValue();
                    paramDoc.Value = LeadType;
                    cryRpt.ParameterFields["@xToday"].CurrentValues.Add(paramDoc);

                    ParameterDiscreteValue paramLead = new ParameterDiscreteValue();
                    paramLead.Value = Demo_Type;
                    cryRpt.ParameterFields["@xPROCTYPE"].CurrentValues.Add(paramLead);

                    ParameterDiscreteValue paramDemo = new ParameterDiscreteValue();
                    paramDemo.Value = Report_Type;
                    cryRpt.ParameterFields["@xDeptCode"].CurrentValues.Add(paramDemo);



                }
                #endregion

                #region "This is used for HR_HO_ATTD_DAYATTDy"
                if (CommonData.ViewReport == "HR_HO_ATTD_DAYATTDy")
                {

                    cryRpt.Load(strPath + "\\Reports\\BioMetrics\\HR_HO_ATTD_DAYATTDy.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xWagePeriod"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue paramFin = new ParameterDiscreteValue();
                    paramFin.Value = DocMonth;
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramFin);

                    ParameterDiscreteValue paramDoc = new ParameterDiscreteValue();
                    paramDoc.Value = LeadType;
                    cryRpt.ParameterFields["@xToday"].CurrentValues.Add(paramDoc);

                    ParameterDiscreteValue paramLead = new ParameterDiscreteValue();
                    paramLead.Value = Demo_Type;
                    cryRpt.ParameterFields["@xPROCTYPE"].CurrentValues.Add(paramLead);

                    ParameterDiscreteValue paramDemo = new ParameterDiscreteValue();
                    paramDemo.Value = Report_Type;
                    cryRpt.ParameterFields["@xDeptCode"].CurrentValues.Add(paramDemo);



                }
                #endregion
                #region "This is used for HR_HO_ATTD_DAYATTDy"
                if (CommonData.ViewReport == "HR_HO_ATTD_DAYATTDy2")
                {

                    cryRpt.Load(strPath + "\\Reports\\BioMetrics\\HR_HO_ATTD_DAYATTDy2.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = DocMonth;
                    cryRpt.ParameterFields["@xDeptCode"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = "ALL";
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = "";
                    cryRpt.ParameterFields["@xWagePeriod"].CurrentValues.Add(paramRep);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = LeadType;
                    cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToday"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xPROCTYPE"].CurrentValues.Add(paramRepType);



                }
                #endregion

                #region "This is used for SSCRM_REP_CRO_INVOICE_SEARCH_PRINT"
                if (CommonData.ViewReport == "SSCRM_REP_INVOICE_PRINT")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_INVOICE_PRINT.rpt");


                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@sCompanyCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@sBranchCode"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = DocMonth;
                    cryRpt.ParameterFields["@sFinYear"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramInvNo = new ParameterDiscreteValue();
                    paramInvNo.Value = E_code;
                    cryRpt.ParameterFields["@nInvoice"].CurrentValues.Add(paramInvNo);
                }
                #endregion

                #region "This is used for MOBILE_NO_DETAILS"
                if (CommonData.ViewReport == "MOBILE_NO_DETAILS")
                {

                    cryRpt.Load(strPath + "\\Reports\\FixedAssets\\SSCRM_REP_MOBILE_NO_DETAILS.rpt");


                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@xLocation"].CurrentValues.Add(paramBranch);

                }
                #endregion

                #region "This is used for BirthDaysMarriageEvents"
                if (CommonData.ViewReport == "BirthDaysMarriageEvents")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\BirthDaysMarriageEvents.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xToday"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = DocMonth;
                    cryRpt.ParameterFields["@xTommorrow"].CurrentValues.Add(paramState);


                    //System.Web.HttpResponse Response = new System.Web.HttpResponse();
                    //cryRpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "PersonDetails");

                }
                #endregion

                #region "This is used for SSCRM_REP_MOBILE_BILL_PAYMENTS"
                if (CommonData.ViewReport == "SSCRM_REP_MOBILE_BILL_PAYMENTS")
                {

                    cryRpt.Load(strPath + "\\Reports\\FixedAssets\\SSCRM_REP_MOBILE_NO_MONTHLY_BILL_DETAILS.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = DocMonth;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = "REPORT";
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);


                    //System.Web.HttpResponse Response = new System.Web.HttpResponse();
                    //cryRpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "PersonDetails");

                }
                #endregion

                #region "This is used for LOAN_DETAILS_ALL"
                if (CommonData.ViewReport == "LOAN_DETAILS_ALL")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\PayRoll\\SSCRM_REP_EMP_LOAN_DETAILS.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = "";
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = "";
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = "";
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDocMonth);
                    
                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = "";
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);

                }
                #endregion

                #region "This is used for SSCRM_REP_EMP_PERF_AGNST_ASSET"
                if (CommonData.ViewReport == "SSCRM_REP_EMP_PERF_AGNST_ASSET")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_EMP_PERF_AGNST_ASSET.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = "";
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = "";
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = "";
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = "";
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramRep);

                    ParameterDiscreteValue paramAssType = new ParameterDiscreteValue();
                    paramAssType.Value = "";
                    cryRpt.ParameterFields["@xAssetType"].CurrentValues.Add(paramAssType);

                    ParameterDiscreteValue paramAssMak = new ParameterDiscreteValue();
                    paramAssMak.Value = "";
                    cryRpt.ParameterFields["@xAssetModel"].CurrentValues.Add(paramAssMak);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = "";
                    cryRpt.ParameterFields["@xEoraCode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = "";
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for HR_PAYROLL_PRINT_MONYY"
                if (CommonData.ViewReport == "HR_PAYROLL_PRINT_MONYY")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\PayRoll\\HR_PAYROLL_PAYSLIP_PRINT_FORMAT2.rpt");

                    ParameterDiscreteValue paramWage = new ParameterDiscreteValue();
                    paramWage.Value = CompanyCode;
                    cryRpt.ParameterFields["@xWagePeriod"].CurrentValues.Add(paramWage);

                    ParameterDiscreteValue paramCCode = new ParameterDiscreteValue();
                    paramCCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramCCode);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = DocMonth;
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = "PAYSLIP";
                    cryRpt.ParameterFields["@xREPORT_TYPE"].CurrentValues.Add(paramRep);

                }
                #endregion

                #region "This is used for HR_PAYROLL_MANUAL_ATTD_MTOD_CHECKLIST"
                if (CommonData.ViewReport == "HR_PAYROLL_MANUAL_ATTD_MTOD_CHECKLIST")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\PayRoll\\HR_PAYROLL_MANUAL_ATTD_MTOD_CHECKLIST.rpt");

                    ParameterDiscreteValue paramWage = new ParameterDiscreteValue();
                    paramWage.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xWagePeriod"].CurrentValues.Add(paramWage);

                    ParameterDiscreteValue paramCCode = new ParameterDiscreteValue();
                    paramCCode.Value = DocMonth;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramCCode);




                }
                #endregion

                #region "This is used for HR_PAYROLL_PAY_REG_BANK"
                if (CommonData.ViewReport == "HR_PAYROLL_PAY_REG_BANK")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\PayRoll\\HR_PAYROLL_PAYREG_BANKx.rpt");

                    ParameterDiscreteValue paramWage = new ParameterDiscreteValue();
                    paramWage.Value = CompanyCode;
                    cryRpt.ParameterFields["@xWagePeriod"].CurrentValues.Add(paramWage);

                    ParameterDiscreteValue paramCCode = new ParameterDiscreteValue();
                    paramCCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramCCode);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = DocMonth;
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xREPORT_TYPE"].CurrentValues.Add(paramRep);


                }
                #endregion

                #region "This is used for HR_PAYROLL_PAY_REG_CASH"
                if (CommonData.ViewReport == "HR_PAYROLL_PAY_REG_CASH")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\PayRoll\\HR_PAYROLL_PAYREG_CASH.rpt");

                    ParameterDiscreteValue paramWage = new ParameterDiscreteValue();
                    paramWage.Value = CompanyCode;
                    cryRpt.ParameterFields["@xWagePeriod"].CurrentValues.Add(paramWage);

                    ParameterDiscreteValue paramCCode = new ParameterDiscreteValue();
                    paramCCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramCCode);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = DocMonth;
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xREPORT_TYPE"].CurrentValues.Add(paramRep);


                }
                #endregion

                #region "This is used for HR_PAYROLL_PAY_REG_DEDU"
                if (CommonData.ViewReport == "HR_PAYROLL_PAY_REG_DEDU")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\PayRoll\\HR_PAYROLL_PAYREG_DEDU.rpt");

                    ParameterDiscreteValue paramWage = new ParameterDiscreteValue();
                    paramWage.Value = CompanyCode;
                    cryRpt.ParameterFields["@xWagePeriod"].CurrentValues.Add(paramWage);

                    ParameterDiscreteValue paramCCode = new ParameterDiscreteValue();
                    paramCCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramCCode);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = DocMonth;
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xREPORT_TYPE"].CurrentValues.Add(paramRep);


                }
                #endregion

                #region "This is used for HR_PAYROLL_PAY_REG_ESI"
                if (CommonData.ViewReport == "HR_PAYROLL_PAY_REG_ESI")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\PayRoll\\HR_PAYROLL_PAYREG_ESI.rpt");

                    ParameterDiscreteValue paramWage = new ParameterDiscreteValue();
                    paramWage.Value = CompanyCode;
                    cryRpt.ParameterFields["@xWagePeriod"].CurrentValues.Add(paramWage);

                    ParameterDiscreteValue paramCCode = new ParameterDiscreteValue();
                    paramCCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramCCode);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = DocMonth;
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xREPORT_TYPE"].CurrentValues.Add(paramRep);


                }
                #endregion

                #region "This is used for HR_PAYROLL_PAY_REG_PROFTAX"
                if (CommonData.ViewReport == "HR_PAYROLL_PAY_REG_PROFTAX")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\PayRoll\\HR_PAYROLL_PAYREG_PROFTAX.rpt");

                    ParameterDiscreteValue paramWage = new ParameterDiscreteValue();
                    paramWage.Value = CompanyCode;
                    cryRpt.ParameterFields["@xWagePeriod"].CurrentValues.Add(paramWage);

                    ParameterDiscreteValue paramCCode = new ParameterDiscreteValue();
                    paramCCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramCCode);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = DocMonth;
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xREPORT_TYPE"].CurrentValues.Add(paramRep);
                }
                #endregion

                #region "This is used for HR_PAYROLL_PAYLIST_BANKWISE"
                if (CommonData.ViewReport == "HR_PAYROLL_PAYLIST_BANKWISE")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\PayRoll\\HR_PAYROLL_PAYLIST_BANKWISE.rpt");

                    ParameterDiscreteValue paramWage = new ParameterDiscreteValue();
                    paramWage.Value = CompanyCode;
                    cryRpt.ParameterFields["@xWagePeriod"].CurrentValues.Add(paramWage);

                    ParameterDiscreteValue paramCCode = new ParameterDiscreteValue();
                    paramCCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramCCode);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = DocMonth;
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xREPORT_TYPE"].CurrentValues.Add(paramRep);
                }
                #endregion

                #region "This is used for HR_PAYROLL_CALC_CHECKLIST_FOR_SAL_STATEMENT_BEFORE_PAYROLL_PROCESS"
                if (CommonData.ViewReport == "HR_PAYROLL_CALC_CHECKLIST")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\PayRoll\\HR_PAYROLL_CALC_CHECKLIST.rpt");

                    ParameterDiscreteValue paramWage = new ParameterDiscreteValue();
                    paramWage.Value = CompanyCode;
                    cryRpt.ParameterFields["@xWagePeriod"].CurrentValues.Add(paramWage);

                    ParameterDiscreteValue paramCCode = new ParameterDiscreteValue();
                    paramCCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramCCode);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = DocMonth;
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xPROCTYPE"].CurrentValues.Add(paramRep);
                }
                #endregion

                #region "This is used for SSCRM_REP_PROM_ELIG_LIST"
                if (CommonData.ViewReport == "SSCRM_REP_PROM_ELIG_LIST")
                {
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_PROM_ELIG_LIST.rpt");

                    ParameterDiscreteValue paramWage = new ParameterDiscreteValue();
                    paramWage.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramWage);

                    ParameterDiscreteValue paramCCode = new ParameterDiscreteValue();
                    paramCCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramCCode);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = DocMonth;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = LeadType;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramRep);

                    ParameterDiscreteValue paramDoc = new ParameterDiscreteValue();
                    paramDoc.Value = Demo_Type;
                    cryRpt.ParameterFields["@xPromType"].CurrentValues.Add(paramDoc);

                    ParameterDiscreteValue paramReptype = new ParameterDiscreteValue();
                    paramReptype.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramReptype);
                }
                #endregion

                #region "This is used for SSCRM_REP_PU_GRN_REG_DETL"
                if (CommonData.ViewReport == "SSCRM_REP_PU_GRN_REG_DETL")
                {
                    cryRpt.Load(strPath + "\\Reports\\PUReports\\SSCRM_REP_PU_GRN_REG_DETL.rpt");

                    ParameterDiscreteValue paramWage = new ParameterDiscreteValue();
                    paramWage.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCOMP"].CurrentValues.Add(paramWage);

                    ParameterDiscreteValue paramCCode = new ParameterDiscreteValue();
                    paramCCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramCCode);

                    ParameterDiscreteValue paramDoc = new ParameterDiscreteValue();
                    paramDoc.Value = "";
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDoc);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = LeadType;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramRep);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = "GRN";
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for SSCRM_REP_PU_GRN_REG_DETL"
                if (CommonData.ViewReport == "SSERP_REP_MONTH_SALES_EMP_LIST_BY_BRANCH")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\SSERP_REP_MONTH_SALES_EMP_LIST_BY_BRANCH.rpt");

                    ParameterDiscreteValue paramWage = new ParameterDiscreteValue();
                    paramWage.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramWage);

                    ParameterDiscreteValue paramCCode = new ParameterDiscreteValue();
                    paramCCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramCCode);

                    ParameterDiscreteValue paramDoc = new ParameterDiscreteValue();
                    paramDoc.Value = DocMonth;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramDoc);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = LeadType;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);

                }
                #endregion

                #region "This is used for TEAK_SALES_AGAINST_QTY REPORT"
                if (CommonData.ViewReport == "TEAK_SALES_AGAINST_QTY")
                {
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSERP_REP_CUMULATIVE_TEAK_BLTNS.rpt");

                    ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                    paramcmdCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramcmdCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFin = new ParameterDiscreteValue();
                    paramFin.Value = "";
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramFin);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = DocMonth;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = Report_Type;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = iFrom;
                    cryRpt.ParameterFields["@xRangeDiff"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = iTo;
                    cryRpt.ParameterFields["@xUptoMax"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "This is used for SSCRM_REP_SERVICE_MON_CUMULATIVE"
                if (CommonData.ViewReport == "SSCRM_REP_SERVICE_MON_CUMULATIVE")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_SERVICE_MON_CUMULATIVE.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_Month"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);

                }
                #endregion              

                #region "This is used for BRANCH_WISE_RECRUITEMENT_SUMMARY REPORT"
                if (CommonData.ViewReport == "BRANCH_WISE_RECRUITEMENT_SUMMARY")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\SSHR_REP_RECRUITMENT_SUMMAR_BY_BRANCH.rpt");

                    ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                    paramcmdCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramcmdCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = DocMonth;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xFROMDATE"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = DocMonth;
                    cryRpt.ParameterFields["@xTODATE"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Report_Type;
                    cryRpt.ParameterFields["@xREPORTTYPE"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "This is used for SSERP_REP_COMP_LOCATION_WISE_GROUPS"
                if (CommonData.ViewReport == "SSERP_REP_COMP_LOCATION_WISE_GROUPS")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSERP_REP_COMP_LOCATION_WISE_GROUPS.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);                    

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = LeadType;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = DocMonth;
                    cryRpt.ParameterFields["@xState"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramDistr = new ParameterDiscreteValue();
                    paramDistr.Value = DocMonth;
                    cryRpt.ParameterFields["@xDistrict"].CurrentValues.Add(paramDistr);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Demo_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRep);

                }
                #endregion

                #region "This is used for SSERP_REP_BRANCH_MASTER"
                if (CommonData.ViewReport == "SSERP_REP_BRANCH_MASTER")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\SSERP_REP_BRANCH_MASTER.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xBranchType"].CurrentValues.Add(paramDocMon);
                                        
                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRep);

                }
                #endregion

                #region "This is used for SSCRM_REP_VEH_PETROL_ALLOW_EXP"
                if (CommonData.ViewReport == "SSCRM_REP_VEH_PETROL_ALLOW_EXP")
                {

                    cryRpt.Load(strPath + "\\Reports\\FixedAssets\\SSCRM_REP_VEH_PETROL_ALLOW_EXP.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = DocMonth;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRep);

                }
                #endregion

                #region "This is used for BRANCH_WISE_EMP_PMD_DA_DEMOS REPORT"
                if (CommonData.ViewReport == "BRANCH_WISE_EMP_PMD_DA_DEMOS")
                {
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSERP_REP_SR_PMD_DA_DEMOS.rpt");

                    ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                    paramcmdCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramcmdCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = DocMonth;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = DocMonth;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "This is used for SP_GRN_BY_INDIVIDUALS REPORT"
                if (CommonData.ViewReport == "SP_GRN_BY_INDIVIDUALS")
                {
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_SP_PU_GRN_BRANCH_EMPLOYEE.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@Company"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@ToBranch"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = BranchCode;
                    cryRpt.ParameterFields["@Ecode"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@FromDate"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = LeadType;
                    cryRpt.ParameterFields["@ToDate"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Demo_Type;
                    cryRpt.ParameterFields["@RepType"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "This is used for SSCRM_REP_AUDIT_POINTS_SUMMARY"
                if (CommonData.ViewReport == "SSCRM_REP_AUDIT_POINTS_SUMMARY")
                {
                    
                    cryRpt.Load(strPath + "\\Reports\\Audit\\SSCRM_REP_AUD_POINTS_STATUS_CRTAB.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramRegion = new ParameterDiscreteValue();
                    paramRegion.Value = "";
                    cryRpt.ParameterFields["@xRegion"].CurrentValues.Add(paramRegion);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = LeadType;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);

                }
                #endregion

                #region "This is used for SSERP_REP_AUDIT_RECOVERY_DETLS"
                if (CommonData.ViewReport == "SSERP_REP_AUDIT_RECOVERY_DETLS")
                {

                    cryRpt.Load(strPath + "\\Reports\\Audit\\SSERP_REP_AUDIT_RECOVERY_DETLS.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = FinYear;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramRegion = new ParameterDiscreteValue();
                    paramRegion.Value = DocMonth;
                    cryRpt.ParameterFields["@xRegion"].CurrentValues.Add(paramRegion);

                    ParameterDiscreteValue paramZone = new ParameterDiscreteValue();
                    paramZone.Value = LeadType;
                    cryRpt.ParameterFields["@xZone"].CurrentValues.Add(paramZone);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = Demo_Type;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRep);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = strVal1;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramEcode);

                }
                #endregion

                #region "This is used for SSCRM_REP_EMP_BANK_ACCOUNT_DETAILS"
                if (CommonData.ViewReport == "SSCRM_REP_EMP_BANK_ACCOUNT_DETAILS")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\PayRoll\\SSCRM_REP_EMP_BANK_ACCOUNT_DETAILS.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = DocMonth;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = LeadType;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);

                }
                #endregion               

                #region "This is used for SSERP_REP_HR_BR_ATTD_REG"
                if (CommonData.ViewReport == "SSERP_REP_HR_BR_ATTD_REG")
                {

                    cryRpt.Load(strPath + "\\Reports\\BioMetrics\\SSERP_REP_BR_ATTD_REG_MTOD.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = DocMonth;
                    cryRpt.ParameterFields["@xDeptCode"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = "ALL";
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = "";
                    cryRpt.ParameterFields["@xWagePeriod"].CurrentValues.Add(paramRep);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = LeadType;
                    cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToday"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xPROCTYPE"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for SHORTAGE_WRITEOFF_EXCESS_REG"
                if (CommonData.ViewReport == "SHORTAGE_WRITEOFF_EXCESS_REG")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_SHORTAGE_REGISTER.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = DocMonth;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = LeadType;
                    cryRpt.ParameterFields["@FromMonth"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Demo_Type;
                    cryRpt.ParameterFields["@ToMonth"].CurrentValues.Add(paramRep);

                    ParameterDiscreteValue paramRep1 = new ParameterDiscreteValue();
                    paramRep1.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep1);
                }
                #endregion

                #region "This is used for TRAINING_SUMMARY_REPORT"
                if (CommonData.ViewReport == "SSCRM_REP_TRAINING_SUMM")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\Training\\SSCRM_REP_TRAINING_SUMM.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDeptId = new ParameterDiscreteValue();
                    paramDeptId.Value = DocMonth;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDeptId);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for TRAINING_PROGRAM_DETAILS"
                if (CommonData.ViewReport == "SSCRM_REP_TRAINING_PRG_DETAILS")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\Training\\SSCRM_REP_TRAINING_PRG_DETL.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDeptId = new ParameterDiscreteValue();
                    paramDeptId.Value = DocMonth;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDeptId);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for TRAINING_PLANNING_DETAILS"
                if (CommonData.ViewReport == "TRAINING_PLANNING_DETAILS")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\Training\\SSCRM_REP_TRAINING_PRG_cros_tab.rpt");


                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramTrainerCode = new ParameterDiscreteValue();
                    paramTrainerCode.Value = DocMonth;
                    cryRpt.ParameterFields["@xTrainerEcode"].CurrentValues.Add(paramTrainerCode);

                    ParameterDiscreteValue paramFromDate = new ParameterDiscreteValue();
                    paramFromDate.Value = LeadType;
                    cryRpt.ParameterFields["@xFRDT"].CurrentValues.Add(paramFromDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = Demo_Type;
                    cryRpt.ParameterFields["@xTODT"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRep);

                }
                #endregion

                #region "This is used for SSCRM_REP_ACTUAL_TRAINING_DETL_BY_TRAINER"
                if (CommonData.ViewReport == "SSCRM_REP_TRAINING_DETL_BY_TRAINER")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\Training\\SSCRM_REP_TRAINING_DETL_BY_TRAINER.rpt");


                    ParameterDiscreteValue paramPrgNo = new ParameterDiscreteValue();
                    paramPrgNo.Value = Report_Type;
                    cryRpt.ParameterFields["@xProgramNo"].CurrentValues.Add(paramPrgNo);



                }
                #endregion

                #region "This is used for SSCRM_REP_TRAINING_PLAN_DETL_BY_TRAINER"
                if (CommonData.ViewReport == "SSCRM_REP_TRAINING_PLAN_DETL_BY_TRAINER")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\Training\\SSCRM_REP_TRAINING_PLAN_DETL_BY_TRAINER.rpt");

                    ParameterDiscreteValue paramPrgNo = new ParameterDiscreteValue();
                    paramPrgNo.Value = Report_Type;
                    cryRpt.ParameterFields["@xProgramNo"].CurrentValues.Add(paramPrgNo);



                }
                #endregion                             

                #region "This is used for SSCRM_REP_AUDIT_QUERY_REGISTER"
                if (CommonData.ViewReport == "SSCRM_REP_AUDIT_QUERY_REGISTER")
                {

                    cryRpt.Load(strPath + "\\Reports\\Audit\\SSCRM_REP_AUDIT_QUERY_REGISTER.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = DocMonth;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = FinYear;
                    cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramAuditEcode = new ParameterDiscreteValue();
                    paramAuditEcode.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xAuditEcode"].CurrentValues.Add(paramAuditEcode);

                    ParameterDiscreteValue paramDevType = new ParameterDiscreteValue();
                    paramDevType.Value = LeadType;
                    cryRpt.ParameterFields["@xDevType"].CurrentValues.Add(paramDevType);

                    ParameterDiscreteValue paramDept = new ParameterDiscreteValue();
                    paramDept.Value = Demo_Type;
                    cryRpt.ParameterFields["@xDept"].CurrentValues.Add(paramDept);

                    ParameterDiscreteValue paramIsMiscond = new ParameterDiscreteValue();
                    paramIsMiscond.Value = Report_Type;
                    cryRpt.ParameterFields["@xIsMisCon"].CurrentValues.Add(paramIsMiscond);

                    ParameterDiscreteValue paramMgntPnt = new ParameterDiscreteValue();
                    paramMgntPnt.Value = strVal1;
                    cryRpt.ParameterFields["@xMgntPoint"].CurrentValues.Add(paramMgntPnt);

                    ParameterDiscreteValue paramPPTPnt = new ParameterDiscreteValue();
                    paramPPTPnt.Value = strVal2;
                    cryRpt.ParameterFields["@xPptPoint"].CurrentValues.Add(paramPPTPnt);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = strVal3;
                    cryRpt.ParameterFields["@xStatus"].CurrentValues.Add(paramStatus);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = strVal4;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);



                }
                #endregion

                #region "This is used for SSCRM_REP_AUDIT_QUERY_REGISTER_FOR_BRANCH"
                if (CommonData.ViewReport == "SSCRM_REP_AUDIT_MAJOR_POINTS")
                {

                    cryRpt.Load(strPath + "\\Reports\\Audit\\SSCRM_REP_AUDIT_MAJOR_POINTS.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = DocMonth;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = FinYear;
                    cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramAuditEcode = new ParameterDiscreteValue();
                    paramAuditEcode.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xAuditEcode"].CurrentValues.Add(paramAuditEcode);

                    ParameterDiscreteValue paramDevType = new ParameterDiscreteValue();
                    paramDevType.Value = LeadType;
                    cryRpt.ParameterFields["@xDevType"].CurrentValues.Add(paramDevType);

                    ParameterDiscreteValue paramDept = new ParameterDiscreteValue();
                    paramDept.Value = Demo_Type;
                    cryRpt.ParameterFields["@xDept"].CurrentValues.Add(paramDept);

                    ParameterDiscreteValue paramIsMiscond = new ParameterDiscreteValue();
                    paramIsMiscond.Value = Report_Type;
                    cryRpt.ParameterFields["@xIsMisCon"].CurrentValues.Add(paramIsMiscond);

                    ParameterDiscreteValue paramMgntPnt = new ParameterDiscreteValue();
                    paramMgntPnt.Value = strVal1;
                    cryRpt.ParameterFields["@xMgntPoint"].CurrentValues.Add(paramMgntPnt);

                    ParameterDiscreteValue paramPPTPnt = new ParameterDiscreteValue();
                    paramPPTPnt.Value = strVal2;
                    cryRpt.ParameterFields["@xPptPoint"].CurrentValues.Add(paramPPTPnt);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = strVal3;
                    cryRpt.ParameterFields["@xStatus"].CurrentValues.Add(paramStatus);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = strVal4;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);



                }
                #endregion

                #region "This is used for AUDIT_DEVIATION_TYPES_SUMMARY"
                if (CommonData.ViewReport == "AUDIT_DEVIATION_TYPES_SUMMARY")
                {
                    cryRpt.Load(strPath + "\\Reports\\Audit\\SSCRM_REP_AUD_DEVCRTAB.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramRegion = new ParameterDiscreteValue();
                    paramRegion.Value = FinYear;
                    cryRpt.ParameterFields["@xRegion"].CurrentValues.Add(paramRegion);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = LeadType;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);

                    ParameterDiscreteValue paramDevType = new ParameterDiscreteValue();
                    paramDevType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xDevType"].CurrentValues.Add(paramDevType);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = Report_Type;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramEcode);


                }
                #endregion

                #region "This is used for AUDIT_DR_PLANNING_DETAILS"
                if (CommonData.ViewReport == "SSCRM_AUDIT_PLAN_CRTAB_REP")
                {
                    cryRpt.Load(strPath + "\\Reports\\Audit\\SSCRM_AUDIT_PLAN_CRTAB_REP.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = FinYear;
                    cryRpt.ParameterFields["@xdocMM"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramAuditEcode = new ParameterDiscreteValue();
                    paramAuditEcode.Value = DocMonth;
                    cryRpt.ParameterFields["@xAuditorsEcode"].CurrentValues.Add(paramAuditEcode);

                    ParameterDiscreteValue paramFromDate = new ParameterDiscreteValue();
                    paramFromDate.Value = LeadType;
                    cryRpt.ParameterFields["@xFRDT"].CurrentValues.Add(paramFromDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = Demo_Type;
                    cryRpt.ParameterFields["@xTODT"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRep);

                }
                #endregion

                #region "This is used for AUDIT_ORGANIZATION_CHART"
                if (CommonData.ViewReport == "SSCRM_REP_AUDIT_ORG_CHART")
                {

                    cryRpt.Load(strPath + "\\Reports\\Audit\\SSCRM_REP_AUDIT_ORG_CHART.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDeptId = new ParameterDiscreteValue();
                    paramDeptId.Value = DocMonth;
                    cryRpt.ParameterFields["@xDeptId"].CurrentValues.Add(paramDeptId);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = LeadType;
                    cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for SERVICE_CHECKLIST"
                if (CommonData.ViewReport == "SSCRM_REP_SERVICES_CHECKLIST")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_SERVICES_CHECKLIST_cross_tab.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDeptId = new ParameterDiscreteValue();
                    paramDeptId.Value = DocMonth;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramDeptId);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = LeadType;
                    cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for SSCRM_REP_STOCK_REC_SUMM"
                if (CommonData.ViewReport == "SSCRM_REP_STOCK_REC_SUMM")
                {
                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSCRM_REP_SP_STOCK_RECONSILATION.rpt");

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = LeadType;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Demo_Type;
                    cryRpt.ParameterFields["@RepType"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "This is used for SP_GRN_DAMAGE_STOCK_DETLS"
                if (CommonData.ViewReport == "SP_GRN_DAMAGE_STOCK_DETLS")
                {
                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSCRM_REP_SP_GRN_DAMAGED_STOCK.rpt");

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = FinYear;
                    cryRpt.ParameterFields["@DocMonth"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = FinYear;
                    cryRpt.ParameterFields["@FromDate"].CurrentValues.Add(paramReportType);

                    ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                    paramTO.Value = DocMonth;
                    cryRpt.ParameterFields["@ToDate"].CurrentValues.Add(paramTO);

                    ParameterDiscreteValue paramDest = new ParameterDiscreteValue();
                    paramDest.Value = LeadType;
                    cryRpt.ParameterFields["@desination"].CurrentValues.Add(paramDest);

                    ParameterDiscreteValue paramProd = new ParameterDiscreteValue();
                    paramProd.Value = Demo_Type;
                    cryRpt.ParameterFields["@xProductId"].CurrentValues.Add(paramProd);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@RepType"].CurrentValues.Add(paramRep);
                }
                #endregion

                #region "This is used for SP_REFILL_STATEMENT_DETLS"
                if (CommonData.ViewReport == "SP_REFILL_STATEMENT_DETLS")
                {
                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSCRM_REP_REF_STATEMENT.rpt");

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = FinYear;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = FinYear;
                    cryRpt.ParameterFields["@xFinyear"].CurrentValues.Add(paramReportType);

                    ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                    paramTO.Value = FinYear;
                    cryRpt.ParameterFields["@xfrmDate"].CurrentValues.Add(paramTO);

                    ParameterDiscreteValue paramDest = new ParameterDiscreteValue();
                    paramDest.Value = DocMonth;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramDest);

                    ParameterDiscreteValue paramProd = new ParameterDiscreteValue();
                    paramProd.Value = Demo_Type;
                    cryRpt.ParameterFields["@xProductId"].CurrentValues.Add(paramProd);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xReportType"].CurrentValues.Add(paramRep);
                }
                #endregion

                #region "This is used for SSCRM_REP_PRODUCT_WISE_SALES_PRODCRTAB"
                if (CommonData.ViewReport == "SSCRM_REP_PRODUCT_WISE_SALES_PRODCRTAB")
                {
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_SUMMBULTIN_PROD_SALES_EMP_PROD.rpt");

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramFrom);

                    //ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    //paramReportType.Value = FinYear;
                    //cryRpt.ParameterFields["@xFinyear"].CurrentValues.Add(paramReportType);

                    //ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                    //paramTO.Value = FinYear;
                    //cryRpt.ParameterFields["@xfrmDate"].CurrentValues.Add(paramTO);

                    //ParameterDiscreteValue paramDest = new ParameterDiscreteValue();
                    //paramDest.Value = DocMonth;
                    //cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramDest);

                    //ParameterDiscreteValue paramProd = new ParameterDiscreteValue();
                    //paramProd.Value = Demo_Type;
                    //cryRpt.ParameterFields["@xProductId"].CurrentValues.Add(paramProd);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRep);
                }
                #endregion

                #region "This is used for SSCRM_REP_PRODUCT_WISE_SALES_CRTAB"
                if (CommonData.ViewReport == "SSCRM_REP_PRODUCT_WISE_SALES_CRTAB")
                {
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_SUMMBULTIN_PROD_SALES.rpt");

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramFrom);

                    //ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    //paramReportType.Value = FinYear;
                    //cryRpt.ParameterFields["@xFinyear"].CurrentValues.Add(paramReportType);

                    //ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                    //paramTO.Value = FinYear;
                    //cryRpt.ParameterFields["@xfrmDate"].CurrentValues.Add(paramTO);

                    //ParameterDiscreteValue paramDest = new ParameterDiscreteValue();
                    //paramDest.Value = DocMonth;
                    //cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramDest);

                    //ParameterDiscreteValue paramProd = new ParameterDiscreteValue();
                    //paramProd.Value = Demo_Type;
                    //cryRpt.ParameterFields["@xProductId"].CurrentValues.Add(paramProd);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRep);
                }
                #endregion

                #region "This is used for BRANCHWISE_STATIONARY_INDENT_REGISTER REPORT"
                if (CommonData.ViewReport == "BRANCHWISE_STATIONARY_INDENT_REGISTER")
                {
                    cryRpt.Load(strPath + "\\Reports\\StationaryReports\\SSCRM_REP_STATIONARY_GRN_DETAILS.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@FromDate"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = LeadType;
                    cryRpt.ParameterFields["@ToDate"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Demo_Type;
                    cryRpt.ParameterFields["@RepType"].CurrentValues.Add(paramRep);

                }
                #endregion

                #region "This is used SSERP_REP_STAFF_DETAILS"
                if (CommonData.ViewReport == "SSERP_REP_STAFF_DETAILS")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\SSERP_REP_STAFF_DETAILS.rpt");

                    ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                    paramcmdCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramcmdCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);
                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramFrom);


                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRep);

                }
                #endregion

                #region "This is used for COMPANY_BRANCH_PRODUCT_WISE_STOCK_SUMMARY"

                if (CommonData.ViewReport == "COMPANY_BRANCH_PRODUCT_WISE_STOCK_SUMMARY")
                {

                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSERP_REP_SP_PRODUCT_OUTSTANDINGS.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = DocMonth;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                    ParameterDiscreteValue paramFrmDocMonth = new ParameterDiscreteValue();
                    paramFrmDocMonth.Value = LeadType;
                    cryRpt.ParameterFields["@FromDocMM"].CurrentValues.Add(paramFrmDocMonth);

                    ParameterDiscreteValue paramToDocMonth = new ParameterDiscreteValue();
                    paramToDocMonth.Value = Demo_Type;
                    cryRpt.ParameterFields["@ToDocMM"].CurrentValues.Add(paramToDocMonth);


                }
                #endregion

                #region "This is used for LOW_DISP_STOCK_POINT_DETAILS"

                if (CommonData.ViewReport == "LOW_DISP_STOCK_POINT_DETAILS")
                {

                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSERP_REP_LOW_DISP_SPS.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = FinYear;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrmDate = new ParameterDiscreteValue();
                    paramFrmDate.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFrmDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = LeadType;
                    cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramQty = new ParameterDiscreteValue();
                    paramQty.Value = iFrom;
                    cryRpt.ParameterFields["@xMinQty"].CurrentValues.Add(paramQty);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for SP_RF_SUMMARY"
                if (CommonData.ViewReport == "SP_RF_SUMMARY")
                {
                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SP_REFILL_SUMM.rpt");

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = FinYear;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = FinYear;
                    cryRpt.ParameterFields["@xFinyear"].CurrentValues.Add(paramReportType);

                    ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                    paramTO.Value = FinYear;
                    cryRpt.ParameterFields["@xfrmDate"].CurrentValues.Add(paramTO);

                    ParameterDiscreteValue paramDest = new ParameterDiscreteValue();
                    paramDest.Value = DocMonth;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramDest);

                    ParameterDiscreteValue paramProd = new ParameterDiscreteValue();
                    paramProd.Value = Demo_Type;
                    cryRpt.ParameterFields["@xProductId"].CurrentValues.Add(paramProd);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xReportType"].CurrentValues.Add(paramRep);
                }
                #endregion

                #region "This is used for Recruitment vs Resigned"
                if (CommonData.ViewReport == "RECRUITMENT_VS_RESIGNED_DETAILS")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\RecruitmentvsResign.rpt");

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = LeadType;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "This is used for BRANCH_WISE_FIXED_ASSETS_DETAILS"
                if (CommonData.ViewReport == "FIXED_ASSETS_DETAILS")
                {

                    cryRpt.Load(strPath + "\\Reports\\FixedAssets\\SSERP_REP_FIXED_ASSETS_DETAILS.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramAssetType = new ParameterDiscreteValue();
                    paramAssetType.Value = DocMonth;
                    cryRpt.ParameterFields["@xAssetType"].CurrentValues.Add(paramAssetType);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "SSCRM_REP_ALL_INDIA_TOP_SRS"
                if (CommonData.ViewReport == "SSCRM_REP_ALL_INDIA_TOP_SRS")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_ALL_INDIA TOP_SRS.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramNoofRecords = new ParameterDiscreteValue();
                    paramNoofRecords.Value = DocMonth;

                    cryRpt.ParameterFields["@xNoofRecords"].CurrentValues.Add(paramNoofRecords);

                    ParameterDiscreteValue paramFromDoc = new ParameterDiscreteValue();
                    paramFromDoc.Value = LeadType;
                    cryRpt.ParameterFields["@xFromDocMonth"].CurrentValues.Add(paramFromDoc);

                    ParameterDiscreteValue paramToDoc = new ParameterDiscreteValue();
                    paramToDoc.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToDocMonth"].CurrentValues.Add(paramToDoc);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for SERVICES_ACTIVITY_REGISTER"
                if (CommonData.ViewReport == "SERVICES_ACTIVITY_REGISTER")
                {
                    cryRpt.Load(strPath + "\\Reports\\Services\\SSERP_REP_SERVICES_ACTIVITY_REGISTER.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = LeadType;
                    cryRpt.ParameterFields["@xTo_DocMonth"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);

                }
                #endregion

                #region "This is used for SERVICES_ACTIVITY_REPLACEMENT_SUMMARY_BRANCH_WISE"
                if (CommonData.ViewReport == "SERVICES_ACTIVITY_REPLACEMENT_SUMMARY")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSERP_REP_SERVICES_REPL_SUMMARY.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramProdType = new ParameterDiscreteValue();
                    paramProdType.Value = LeadType;
                    cryRpt.ParameterFields["@xProductId"].CurrentValues.Add(paramProdType);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);

                }
                #endregion

                #region "This is used for SERVICES_ACTIVITY_REPLACEMENT_SUMMARY_COMPANY_WISE"
                if (CommonData.ViewReport == "SERVICES_ACTIVITY_REPL_SUMMARY_COMP_WISE")
                {
                    cryRpt.Load(strPath + "\\Reports\\Services\\SSERP_REP_SERVICES_REPL_SUMMARY_COMP_WISE.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramProdType = new ParameterDiscreteValue();
                    paramProdType.Value = LeadType;
                    cryRpt.ParameterFields["@xProductId"].CurrentValues.Add(paramProdType);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);

                }
                #endregion

                #region "SSCRM_REP_ALL_INDIA_TOP_GLS"
                if (CommonData.ViewReport == "SSCRM_REP_ALL_INDIA_TOP_GL")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_ALL_INDIA TOP_GLS.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramNoofRecords = new ParameterDiscreteValue();
                    paramNoofRecords.Value = DocMonth;

                    cryRpt.ParameterFields["@xNoofRecords"].CurrentValues.Add(paramNoofRecords);

                    ParameterDiscreteValue paramFromDoc = new ParameterDiscreteValue();
                    paramFromDoc.Value = LeadType;
                    cryRpt.ParameterFields["@xFromDocMonth"].CurrentValues.Add(paramFromDoc);

                    ParameterDiscreteValue paramToDoc = new ParameterDiscreteValue();
                    paramToDoc.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToDocMonth"].CurrentValues.Add(paramToDoc);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for SSCRM_REP_ALL_INDIA_TOPPERS_FOR_AWARDS"
                if (CommonData.ViewReport == "SSCRM_REP_ALL_INDIA_TOPPERS_FOR_AWARDS")
                {
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSERP_REP_ALL_INDIA TOPPERS_FOR_AWARDS.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramZone = new ParameterDiscreteValue();
                    paramZone.Value = DocMonth;
                    cryRpt.ParameterFields["@xZone"].CurrentValues.Add(paramZone);

                    ParameterDiscreteValue paramRegion = new ParameterDiscreteValue();
                    paramRegion.Value = FinYear;
                    cryRpt.ParameterFields["@xRegion"].CurrentValues.Add(paramRegion);

                    ParameterDiscreteValue paramDivision = new ParameterDiscreteValue();
                    paramDivision.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xDivision"].CurrentValues.Add(paramDivision);

                    ParameterDiscreteValue paramFromGrpPerMnth = new ParameterDiscreteValue();
                    paramFromGrpPerMnth.Value = iApprovedId;
                    cryRpt.ParameterFields["@xFromGrp_PerMonth"].CurrentValues.Add(paramFromGrpPerMnth);

                    ParameterDiscreteValue paramToGrpPerMnth = new ParameterDiscreteValue();
                    paramToGrpPerMnth.Value = E_code;
                    cryRpt.ParameterFields["@xToGrp_PerMonth"].CurrentValues.Add(paramToGrpPerMnth);

                    ParameterDiscreteValue paramFrmGrps = new ParameterDiscreteValue();
                    paramFrmGrps.Value = iFrom;
                    cryRpt.ParameterFields["@xFrom_Grps"].CurrentValues.Add(paramFrmGrps);

                    ParameterDiscreteValue paramToGrps = new ParameterDiscreteValue();
                    paramToGrps.Value = iTo;
                    cryRpt.ParameterFields["@xTo_Grps"].CurrentValues.Add(paramToGrps);

                    ParameterDiscreteValue paramNoOfRecords = new ParameterDiscreteValue();
                    paramNoOfRecords.Value = iRes;
                    cryRpt.ParameterFields["@xNoofRecords"].CurrentValues.Add(paramNoOfRecords);

                    ParameterDiscreteValue paramFrmDocMnth = new ParameterDiscreteValue();
                    paramFrmDocMnth.Value = LeadType;
                    cryRpt.ParameterFields["@xFromDocMonth"].CurrentValues.Add(paramFrmDocMnth);

                    ParameterDiscreteValue paramToDocMnth = new ParameterDiscreteValue();
                    paramToDocMnth.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToDocMonth"].CurrentValues.Add(paramToDocMnth);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);



                }
                #endregion

                #region "This is used for BRANCH_WISE_AFC_STATEMENT_MNTHLY REPORT"
                if (CommonData.ViewReport == "BRANCH_WISE_AFC_STATEMENT_MNTHLY")
                {
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_AFC_RECONCILIATION_STATEMENT_DOCWISE.rpt");

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

                #region "This is used for BRANCH_WISE_AFC_BY_ALL REPORT"
                if (CommonData.ViewReport == "BRANCH_WISE_AFC_BY_ALL")
                {
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_AFC_RECONCILIATION_STATEMENT_YEARLY.rpt");

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

                #region "This Is Used STATIONARY GRN REGISTER"
                if (CommonData.ViewReport == "SSERP_REP_STATIONARY_GRN_REGISTER")
                {

                    cryRpt.Load(strPath + "\\Reports\\StationaryReports\\SSERP_REP_STATIONARY_GRN_REGISTER_Stores.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFromDate = new ParameterDiscreteValue();
                    paramFromDate.Value = DocMonth;
                    cryRpt.ParameterFields["@FromDate"].CurrentValues.Add(paramFromDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = LeadType;
                    cryRpt.ParameterFields["@ToDate"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@RepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used SSCRM_REP_CASH_VOUCHER_PRINT"
                if (CommonData.ViewReport == "SSCRM_REP_CASH_VOUCHER_PRINT")
                {
                    cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_CASH_PAYMENT_VOUCHER_PRINT.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@company_code"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@branch_code"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = DocMonth;
                    cryRpt.ParameterFields["@Fin_year"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramVType = new ParameterDiscreteValue();
                    paramVType.Value = "CP";
                    cryRpt.ParameterFields["@Voucher_Type"].CurrentValues.Add(paramVType);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@Voucher_No"].CurrentValues.Add(paramRep);
                }
                #endregion

                #region "This is used SSCRM_REP_RECEIPT_VOUCHER_PRINT"
                if (CommonData.ViewReport == "SSCRM_REP_RECEIPT_VOUCHER_PRINT")
                {
                    cryRpt.Load(strPath + "\\Reports\\Financial Accounts\\SSCRM_REP_CASH_RECEIPT_VOUCHER_PRINT.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@company_code"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@branch_code"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = DocMonth;
                    cryRpt.ParameterFields["@Fin_year"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramVType = new ParameterDiscreteValue();
                    paramVType.Value = "CR";
                    cryRpt.ParameterFields["@Voucher_Type"].CurrentValues.Add(paramVType);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@Voucher_No"].CurrentValues.Add(paramRep);
                }
                #endregion

                #region "This is used SSCRM_REP_PAYMENT_VOUCHER_REGISTER"
                if (CommonData.ViewReport == "SSCRM_REP_PAYMENT_VOUCHER_REGISTER")
                {
                    cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_PAYMENT_VOUCHER_REGISTER.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = FinYear;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramVType = new ParameterDiscreteValue();
                    paramVType.Value = DocMonth;
                    cryRpt.ParameterFields["@xAccountHead"].CurrentValues.Add(paramVType);

                    ParameterDiscreteValue paramType = new ParameterDiscreteValue();
                    paramType.Value = LeadType;
                    cryRpt.ParameterFields["@xVoucherType"].CurrentValues.Add(paramType);


                    //ParameterDiscreteValue paramType = new ParameterDiscreteValue();
                    //paramType.Value = LeadType;
                    //cryRpt.ParameterFields["@xVoucherType"].CurrentValues.Add(paramType);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Demo_Type;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramRep);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = Report_Type;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramToDate);

                   
                }
                #endregion

                #region "This is used SSCRM_REP_RECEIPT_VOUCHER_REGISTER"
                if (CommonData.ViewReport == "SSCRM_REP_RECEIPT_VOUCHER_REGISTER")
                {
                    cryRpt.Load(strPath + "\\Reports\\Financial Accounts\\SSCRM_REP_RECEIPT_VOUCHER_REGISTER.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = FinYear;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramVType = new ParameterDiscreteValue();
                    paramVType.Value = DocMonth;
                    cryRpt.ParameterFields["@xAccountHead"].CurrentValues.Add(paramVType);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = LeadType;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramRep);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramType = new ParameterDiscreteValue();
                    paramType.Value = Report_Type;
                    cryRpt.ParameterFields["@xVoucherType"].CurrentValues.Add(paramType);
                }
                #endregion

                #region "This is used SSERP_REP_BRANCH_WISE_NEW_SR_JOININGS"
                if (CommonData.ViewReport == "SSERP_REP_BRANCH_WISE_NEW_SR_JOININGS")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\StaffDetls\\SSERP_REP_NEW_JOININGS.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);                    

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = LeadType;
                    cryRpt.ParameterFields["@xToDOC_MONTH"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Demo_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRep);
                }
                #endregion

                #region "This is used SSERP_REP_BRANCH_WISE_NEW_SR_LEFT"
                if (CommonData.ViewReport == "SSERP_REP_BRANCH_WISE_NEW_SR_LEFT")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\StaffDetls\\SSERP_REP_LEFT_SRS.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = LeadType;
                    cryRpt.ParameterFields["@xToDOC_MONTH"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Demo_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRep);
                }
                #endregion

                #region "This is used for SPECIAL_APPROVALS_REGISTER"
                if (CommonData.ViewReport == "SSCRM_REP_SPECIAL_APPROVALS_REGISTER")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSERP_REP_SPECIAL_APPROVAL_REGISTER.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFromDate = new ParameterDiscreteValue();
                    paramFromDate.Value = DocMonth;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFromDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = LeadType;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is Used for SPECIAL_APPROVALS_SUMMARY"
                if (CommonData.ViewReport == "SSCRM_REP_SPECIAL_APPROVALS_SUMMARY")
                {
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSERP_REP_SPECIAL_APPROVAL_SUMMARY.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFromDate = new ParameterDiscreteValue();
                    paramFromDate.Value = DocMonth;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFromDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = LeadType;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for AFTER_TRAINING_EMP_PERFORMANCE"

                if (CommonData.ViewReport == "AFTER_TRAINING_EMP_PERFORMANCE")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\Training\\SSCRM_REP_AFT_TRAINING_EMP_PERF.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramDistrict = new ParameterDiscreteValue();
                    paramDistrict.Value = FinYear;
                    cryRpt.ParameterFields["@xTrainerEcode"].CurrentValues.Add(paramDistrict);

                    ParameterDiscreteValue paramVillage = new ParameterDiscreteValue();
                    paramVillage.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramVillage);

                    ParameterDiscreteValue paramProdType = new ParameterDiscreteValue();
                    paramProdType.Value = LeadType;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramProdType);

                    ParameterDiscreteValue paramActivityType = new ParameterDiscreteValue();
                    paramActivityType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xProgramId"].CurrentValues.Add(paramActivityType);


                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = strVal2;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                    ParameterDiscreteValue paramFrmQty = new ParameterDiscreteValue();
                    paramFrmQty.Value = iFrom;
                    cryRpt.ParameterFields["@xTrBfrMonths"].CurrentValues.Add(paramFrmQty);

                    ParameterDiscreteValue paramToQty = new ParameterDiscreteValue();
                    paramToQty.Value = iTo;
                    cryRpt.ParameterFields["@xAftTrMonths"].CurrentValues.Add(paramToQty);
                }

                #endregion

                #region "This is used for TRAINNING_PROGRAM_DETAILS_TRAINER_WISE"

                if (CommonData.ViewReport == "TRAINER_PROGRAM_DETAILS")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\Training\\SSCRM_REP_TRAINER_PRG_DETL.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xTrainerEcode"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramDistrict = new ParameterDiscreteValue();
                    paramDistrict.Value = LeadType;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramDistrict);

                    ParameterDiscreteValue paramVillage = new ParameterDiscreteValue();
                    paramVillage.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramVillage);

                    ParameterDiscreteValue paramProdType = new ParameterDiscreteValue();
                    paramProdType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramProdType);

                }

                #endregion

                #region "This is used for ACTUAL_TRAINING_PROGRAM_DETAILS_TRAINER_WISE"

                if (CommonData.ViewReport == "ACTUAL_TRAINING_PROGRAM_DETAILS")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\Training\\SSCRM_REP_TRAINER_ACTUAL_PRG_DETL.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xTrainerEcode"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramDistrict = new ParameterDiscreteValue();
                    paramDistrict.Value = LeadType;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramDistrict);

                    ParameterDiscreteValue paramVillage = new ParameterDiscreteValue();
                    paramVillage.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramVillage);

                    ParameterDiscreteValue paramProdType = new ParameterDiscreteValue();
                    paramProdType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramProdType);

                }

                #endregion

                #region "This is Used for SERVICE_EMP_TOUR_BILL_DETAIL_ACTIVITY_WISE"
                if (CommonData.ViewReport == "SSCRM_REP_EMP_TOUR_BILL_DETL_ACTIVITY_WISE")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_TOUR_BILL_DETAILS_ACTIVITY_WISE.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFromDate = new ParameterDiscreteValue();
                    paramFromDate.Value = DocMonth;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFromDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = LeadType;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for SERVICES_ACTIVITY_STATUS_REPORT"
                if (CommonData.ViewReport == "SERVICES_ACTIVITY_STATUS_LIST")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\sscrm_services_crtab_Report.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramDistrict = new ParameterDiscreteValue();
                    paramDistrict.Value = FinYear;
                    cryRpt.ParameterFields["@xDistrict"].CurrentValues.Add(paramDistrict);

                    ParameterDiscreteValue paramCampName = new ParameterDiscreteValue();
                    paramCampName.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xCamp_Name"].CurrentValues.Add(paramCampName);

                    ParameterDiscreteValue paramVillage = new ParameterDiscreteValue();
                    paramVillage.Value = LeadType;
                    cryRpt.ParameterFields["@xVillage"].CurrentValues.Add(paramVillage);

                    ParameterDiscreteValue paramProdType = new ParameterDiscreteValue();
                    paramProdType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xProdType"].CurrentValues.Add(paramProdType);

                    ParameterDiscreteValue paramActivityType = new ParameterDiscreteValue();
                    paramActivityType.Value = Report_Type;
                    cryRpt.ParameterFields["@xActivityType"].CurrentValues.Add(paramActivityType);

                    ParameterDiscreteValue paramActivityStatus = new ParameterDiscreteValue();
                    paramActivityStatus.Value = strVal1;
                    cryRpt.ParameterFields["@xActivityStatus"].CurrentValues.Add(paramActivityStatus);

                    ParameterDiscreteValue paramFrmQty = new ParameterDiscreteValue();
                    paramFrmQty.Value = Convert.ToInt32(strVal2);
                    cryRpt.ParameterFields["@xFromQty"].CurrentValues.Add(paramFrmQty);

                    ParameterDiscreteValue paramToQty = new ParameterDiscreteValue();
                    paramToQty.Value = Convert.ToInt32(strVal3);
                    cryRpt.ParameterFields["@xToQty"].CurrentValues.Add(paramToQty);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = strVal4;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for DISTRICT_WISE_SERVICES_ACTIVITY_STATUS_REPORT"
                if (CommonData.ViewReport == "DISTRICT_WISE_SERVICES_ACTIVITY_STATUS_LIST")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\sscrm_services_crtab_district_wise_Report.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramDistrict = new ParameterDiscreteValue();
                    paramDistrict.Value = FinYear;
                    cryRpt.ParameterFields["@xDistrict"].CurrentValues.Add(paramDistrict);

                    ParameterDiscreteValue paramCampName = new ParameterDiscreteValue();
                    paramCampName.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xCamp_Name"].CurrentValues.Add(paramCampName);

                    ParameterDiscreteValue paramVillage = new ParameterDiscreteValue();
                    paramVillage.Value = LeadType;
                    cryRpt.ParameterFields["@xVillage"].CurrentValues.Add(paramVillage);

                    ParameterDiscreteValue paramProdType = new ParameterDiscreteValue();
                    paramProdType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xProdType"].CurrentValues.Add(paramProdType);

                    ParameterDiscreteValue paramActivityType = new ParameterDiscreteValue();
                    paramActivityType.Value = Report_Type;
                    cryRpt.ParameterFields["@xActivityType"].CurrentValues.Add(paramActivityType);

                    ParameterDiscreteValue paramActivityStatus = new ParameterDiscreteValue();
                    paramActivityStatus.Value = strVal1;
                    cryRpt.ParameterFields["@xActivityStatus"].CurrentValues.Add(paramActivityStatus);

                    ParameterDiscreteValue paramFrmQty = new ParameterDiscreteValue();
                    paramFrmQty.Value = Convert.ToInt32(strVal2);
                    cryRpt.ParameterFields["@xFromQty"].CurrentValues.Add(paramFrmQty);

                    ParameterDiscreteValue paramToQty = new ParameterDiscreteValue();
                    paramToQty.Value = Convert.ToInt32(strVal3);
                    cryRpt.ParameterFields["@xToQty"].CurrentValues.Add(paramToQty);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = strVal4;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);




                }
                #endregion

                #region "This is used for VILLAGE_WISE_SERVICES_ACTIVITY_STATUS_REPORT"
                if (CommonData.ViewReport == "VILLAGE_WISE_SERVICES_ACTIVITY_STATUS_LIST")
                {
                    cryRpt.Load(strPath + "\\Reports\\Services\\sscrm_services_crtab_village_wise_Report.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramDistrict = new ParameterDiscreteValue();
                    paramDistrict.Value = FinYear;
                    cryRpt.ParameterFields["@xDistrict"].CurrentValues.Add(paramDistrict);

                    ParameterDiscreteValue paramCampName = new ParameterDiscreteValue();
                    paramCampName.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xCamp_Name"].CurrentValues.Add(paramCampName);

                    ParameterDiscreteValue paramVillage = new ParameterDiscreteValue();
                    paramVillage.Value = LeadType;
                    cryRpt.ParameterFields["@xVillage"].CurrentValues.Add(paramVillage);

                    ParameterDiscreteValue paramProdType = new ParameterDiscreteValue();
                    paramProdType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xProdType"].CurrentValues.Add(paramProdType);

                    ParameterDiscreteValue paramActivityType = new ParameterDiscreteValue();
                    paramActivityType.Value = Report_Type;
                    cryRpt.ParameterFields["@xActivityType"].CurrentValues.Add(paramActivityType);

                    ParameterDiscreteValue paramActivityStatus = new ParameterDiscreteValue();
                    paramActivityStatus.Value = strVal1;
                    cryRpt.ParameterFields["@xActivityStatus"].CurrentValues.Add(paramActivityStatus);

                    ParameterDiscreteValue paramFrmQty = new ParameterDiscreteValue();
                    paramFrmQty.Value = Convert.ToInt32(strVal2);
                    cryRpt.ParameterFields["@xFromQty"].CurrentValues.Add(paramFrmQty);

                    ParameterDiscreteValue paramToQty = new ParameterDiscreteValue();
                    paramToQty.Value = Convert.ToInt32(strVal3);
                    cryRpt.ParameterFields["@xToQty"].CurrentValues.Add(paramToQty);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = strVal4;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);


                }
                #endregion

                #region "This is used for SERVICES_ACTIVITY_SUMMARY"

                if (CommonData.ViewReport == "SERVICES_ACTIVITIES_SUMMARY")
                {
                    cryRpt.Load(strPath + "\\Reports\\Services\\sscrm_services_crtab_Summary_Report.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramDistrict = new ParameterDiscreteValue();
                    paramDistrict.Value = FinYear;
                    cryRpt.ParameterFields["@xDistrict"].CurrentValues.Add(paramDistrict);

                    ParameterDiscreteValue paramCampName = new ParameterDiscreteValue();
                    paramCampName.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xCamp_Name"].CurrentValues.Add(paramCampName);

                    ParameterDiscreteValue paramVillage = new ParameterDiscreteValue();
                    paramVillage.Value = LeadType;
                    cryRpt.ParameterFields["@xVillage"].CurrentValues.Add(paramVillage);

                    ParameterDiscreteValue paramProdType = new ParameterDiscreteValue();
                    paramProdType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xProdType"].CurrentValues.Add(paramProdType);

                    ParameterDiscreteValue paramActivityType = new ParameterDiscreteValue();
                    paramActivityType.Value = Report_Type;
                    cryRpt.ParameterFields["@xActivityType"].CurrentValues.Add(paramActivityType);

                    ParameterDiscreteValue paramActivityStatus = new ParameterDiscreteValue();
                    paramActivityStatus.Value = strVal1;
                    cryRpt.ParameterFields["@xActivityStatus"].CurrentValues.Add(paramActivityStatus);

                    ParameterDiscreteValue paramFrmQty = new ParameterDiscreteValue();
                    paramFrmQty.Value = Convert.ToInt32(strVal2);
                    cryRpt.ParameterFields["@xFromQty"].CurrentValues.Add(paramFrmQty);

                    ParameterDiscreteValue paramToQty = new ParameterDiscreteValue();
                    paramToQty.Value = Convert.ToInt32(strVal3);
                    cryRpt.ParameterFields["@xToQty"].CurrentValues.Add(paramToQty);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = strVal4;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);


                }
                #endregion

                #region "This is used for DISTRICT_WISE_SERVICES_ACTIVITY_SUMMARY"

                if (CommonData.ViewReport == "DISTRICT_WISE_SERVICES_ACTIVITIES_SUMMARY")
                {
                    cryRpt.Load(strPath + "\\Reports\\Services\\sscrm_services_district_wise_Summary_Rep.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramDistrict = new ParameterDiscreteValue();
                    paramDistrict.Value = FinYear;
                    cryRpt.ParameterFields["@xDistrict"].CurrentValues.Add(paramDistrict);

                    ParameterDiscreteValue paramCampName = new ParameterDiscreteValue();
                    paramCampName.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xCamp_Name"].CurrentValues.Add(paramCampName);

                    ParameterDiscreteValue paramVillage = new ParameterDiscreteValue();
                    paramVillage.Value = LeadType;
                    cryRpt.ParameterFields["@xVillage"].CurrentValues.Add(paramVillage);

                    ParameterDiscreteValue paramProdType = new ParameterDiscreteValue();
                    paramProdType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xProdType"].CurrentValues.Add(paramProdType);

                    ParameterDiscreteValue paramActivityType = new ParameterDiscreteValue();
                    paramActivityType.Value = Report_Type;
                    cryRpt.ParameterFields["@xActivityType"].CurrentValues.Add(paramActivityType);

                    ParameterDiscreteValue paramActivityStatus = new ParameterDiscreteValue();
                    paramActivityStatus.Value = strVal1;
                    cryRpt.ParameterFields["@xActivityStatus"].CurrentValues.Add(paramActivityStatus);

                    ParameterDiscreteValue paramFrmQty = new ParameterDiscreteValue();
                    paramFrmQty.Value = Convert.ToInt32(strVal2);
                    cryRpt.ParameterFields["@xFromQty"].CurrentValues.Add(paramFrmQty);

                    ParameterDiscreteValue paramToQty = new ParameterDiscreteValue();
                    paramToQty.Value = Convert.ToInt32(strVal3);
                    cryRpt.ParameterFields["@xToQty"].CurrentValues.Add(paramToQty);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = strVal4;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);


                }
                #endregion

                #region "This is used for VILLAGE_WISE_SERVICES_ACTIVITY_SUMMARY"

                if (CommonData.ViewReport == "VILLAGE_WISE_SERVICES_ACTIVITIES_SUMMARY")
                {
                    cryRpt.Load(strPath + "\\Reports\\Services\\sscrm_services_village_wise_Summary_Rep.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramDistrict = new ParameterDiscreteValue();
                    paramDistrict.Value = FinYear;
                    cryRpt.ParameterFields["@xDistrict"].CurrentValues.Add(paramDistrict);

                    ParameterDiscreteValue paramCampName = new ParameterDiscreteValue();
                    paramCampName.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xCamp_Name"].CurrentValues.Add(paramCampName);

                    ParameterDiscreteValue paramVillage = new ParameterDiscreteValue();
                    paramVillage.Value = LeadType;
                    cryRpt.ParameterFields["@xVillage"].CurrentValues.Add(paramVillage);

                    ParameterDiscreteValue paramProdType = new ParameterDiscreteValue();
                    paramProdType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xProdType"].CurrentValues.Add(paramProdType);

                    ParameterDiscreteValue paramActivityType = new ParameterDiscreteValue();
                    paramActivityType.Value = Report_Type;
                    cryRpt.ParameterFields["@xActivityType"].CurrentValues.Add(paramActivityType);

                    ParameterDiscreteValue paramActivityStatus = new ParameterDiscreteValue();
                    paramActivityStatus.Value = strVal1;
                    cryRpt.ParameterFields["@xActivityStatus"].CurrentValues.Add(paramActivityStatus);

                    ParameterDiscreteValue paramFrmQty = new ParameterDiscreteValue();
                    paramFrmQty.Value = Convert.ToInt32(strVal2);
                    cryRpt.ParameterFields["@xFromQty"].CurrentValues.Add(paramFrmQty);

                    ParameterDiscreteValue paramToQty = new ParameterDiscreteValue();
                    paramToQty.Value = Convert.ToInt32(strVal3);
                    cryRpt.ParameterFields["@xToQty"].CurrentValues.Add(paramToQty);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = strVal4;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);




                }
                #endregion

                #region "This is used for CAMP_WISE_SERVICE_ACTIVITIES_LIST"

                if (CommonData.ViewReport == "CAMP_WISE_SERVICE_ACTIVITIES_LIST")
                {
                    cryRpt.Load(strPath + "\\Reports\\Services\\sscrm_services_Camp_Wise_Report.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramDistrict = new ParameterDiscreteValue();
                    paramDistrict.Value = FinYear;
                    cryRpt.ParameterFields["@xDistrict"].CurrentValues.Add(paramDistrict);

                    ParameterDiscreteValue paramCampName = new ParameterDiscreteValue();
                    paramCampName.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xCamp_Name"].CurrentValues.Add(paramCampName);

                    ParameterDiscreteValue paramVillage = new ParameterDiscreteValue();
                    paramVillage.Value = LeadType;
                    cryRpt.ParameterFields["@xVillage"].CurrentValues.Add(paramVillage);

                    ParameterDiscreteValue paramProdType = new ParameterDiscreteValue();
                    paramProdType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xProdType"].CurrentValues.Add(paramProdType);

                    ParameterDiscreteValue paramActivityType = new ParameterDiscreteValue();
                    paramActivityType.Value = Report_Type;
                    cryRpt.ParameterFields["@xActivityType"].CurrentValues.Add(paramActivityType);

                    ParameterDiscreteValue paramActivityStatus = new ParameterDiscreteValue();
                    paramActivityStatus.Value = strVal1;
                    cryRpt.ParameterFields["@xActivityStatus"].CurrentValues.Add(paramActivityStatus);

                    ParameterDiscreteValue paramFrmQty = new ParameterDiscreteValue();
                    paramFrmQty.Value = Convert.ToInt32(strVal2);
                    cryRpt.ParameterFields["@xFromQty"].CurrentValues.Add(paramFrmQty);

                    ParameterDiscreteValue paramToQty = new ParameterDiscreteValue();
                    paramToQty.Value = Convert.ToInt32(strVal3);
                    cryRpt.ParameterFields["@xToQty"].CurrentValues.Add(paramToQty);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = strVal4;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for CAMP_WISE_SERVICE_ACTIVITY_SUMMARY"

                if (CommonData.ViewReport == "CAMP_WISE_SERVICE_ACTIVITY_SUMMARY")
                {
                    cryRpt.Load(strPath + "\\Reports\\Services\\sscrm_services_Camp_Wise_Summary_Rep.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramDistrict = new ParameterDiscreteValue();
                    paramDistrict.Value = FinYear;
                    cryRpt.ParameterFields["@xDistrict"].CurrentValues.Add(paramDistrict);

                    ParameterDiscreteValue paramCampName = new ParameterDiscreteValue();
                    paramCampName.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xCamp_Name"].CurrentValues.Add(paramCampName);

                    ParameterDiscreteValue paramVillage = new ParameterDiscreteValue();
                    paramVillage.Value = LeadType;
                    cryRpt.ParameterFields["@xVillage"].CurrentValues.Add(paramVillage);

                    ParameterDiscreteValue paramProdType = new ParameterDiscreteValue();
                    paramProdType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xProdType"].CurrentValues.Add(paramProdType);

                    ParameterDiscreteValue paramActivityType = new ParameterDiscreteValue();
                    paramActivityType.Value = Report_Type;
                    cryRpt.ParameterFields["@xActivityType"].CurrentValues.Add(paramActivityType);

                    ParameterDiscreteValue paramActivityStatus = new ParameterDiscreteValue();
                    paramActivityStatus.Value = strVal1;
                    cryRpt.ParameterFields["@xActivityStatus"].CurrentValues.Add(paramActivityStatus);

                    ParameterDiscreteValue paramFrmQty = new ParameterDiscreteValue();
                    paramFrmQty.Value = Convert.ToInt32(strVal2);
                    cryRpt.ParameterFields["@xFromQty"].CurrentValues.Add(paramFrmQty);

                    ParameterDiscreteValue paramToQty = new ParameterDiscreteValue();
                    paramToQty.Value = Convert.ToInt32(strVal3);
                    cryRpt.ParameterFields["@xToQty"].CurrentValues.Add(paramToQty);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = strVal4;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for SSCRM_REP_SERVICE_EMP_TOUR_EXPENSES_DETL"
                if (CommonData.ViewReport == "SSCRM_REP_TOUR_EXPENSES_DETL")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_TOUR_EXPENSES_DETL.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramFrmMonth = new ParameterDiscreteValue();
                    paramFrmMonth.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom_Month"].CurrentValues.Add(paramFrmMonth);

                    ParameterDiscreteValue paramToMonth = new ParameterDiscreteValue();
                    paramToMonth.Value = LeadType;
                    cryRpt.ParameterFields["@xTo_Month"].CurrentValues.Add(paramToMonth);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = Demo_Type;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for SSCRM_REP_SERVICE_BRANCH_WISE_TOUR_EXPENSES_DETL"
                if (CommonData.ViewReport == "SSCRM_REP_TOUR_EXPENSES_BRANCH_WISE")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_TOUR_EXPENSES_BRANCH_WISE.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramFrmMonth = new ParameterDiscreteValue();
                    paramFrmMonth.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom_Month"].CurrentValues.Add(paramFrmMonth);

                    ParameterDiscreteValue paramToMonth = new ParameterDiscreteValue();
                    paramToMonth.Value = LeadType;
                    cryRpt.ParameterFields["@xTo_Month"].CurrentValues.Add(paramToMonth);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = Demo_Type;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for SSERP_REP_SERVICES_MONTH_WISE_REPL_SUMMARY"
                if (CommonData.ViewReport == "SSERP_REP_SERVICES_MONTH_WISE_REPL_SUMMARY")
                {
                    cryRpt.Load(strPath + "\\Reports\\Services\\SSERP_REP_SERVICES_MONTH_WISE_REPL_SUM.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramProdType = new ParameterDiscreteValue();
                    paramProdType.Value = LeadType;
                    cryRpt.ParameterFields["@xProductId"].CurrentValues.Add(paramProdType);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);

                }
                #endregion

                #region "This is used for SERVICES_COUNTING_AND_REPL_SUMMARY_BRANCH_WISE"
                if (CommonData.ViewReport == "SERVICES_COUNTING_AND_REPL_SUMMARY_BRANCH_WISE")
                {
                    cryRpt.Load(strPath + "\\Reports\\Services\\SSERP_REP_SERV_COUNT_AND_REPL_SUM_Branch_Wise.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramProdType = new ParameterDiscreteValue();
                    paramProdType.Value = LeadType;
                    cryRpt.ParameterFields["@xProductId"].CurrentValues.Add(paramProdType);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);

                }
                #endregion

                #region "This is used for SSCRM_SERVICE_REP_PRODUCT_PROMOTION_DETL"
                if (CommonData.ViewReport == "SSCRM_SERVICE_REP_PRODUCT_PROMOTION_DETL")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_PRODUCT_PROMOTION_DETAILS.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDist);


                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for SERVICE_ORGANIZATION_CHART"
                if (CommonData.ViewReport == "SSCRM_REP_SERVICE_ORG_CHART")
                {

                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_SERVICE_ORG_CHART.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@cmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDeptId = new ParameterDiscreteValue();
                    paramDeptId.Value = DocMonth;
                    cryRpt.ParameterFields["@xDeptId"].CurrentValues.Add(paramDeptId);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = LeadType;
                    cryRpt.ParameterFields["@xDoc_month"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = E_code;
                    cryRpt.ParameterFields["@xEoraEcode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for SSCRM_SERVICE_REP_FARMER_MEETINGS"
                if (CommonData.ViewReport == "SSCRM_REP_FM_SUMMARY")
                {
                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_FM_SUMMARY_BRANCH_WISE.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDist);


                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for SSERP_STK_RETURN_SPVGL"
                if (CommonData.ViewReport == "SSERP_STK_RETURN_SPVGL")
                {
                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSERP_STK_RETURN_SPVGL.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramfROM = new ParameterDiscreteValue();
                    paramfROM.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramfROM);

                    ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                    paramTO.Value = LeadType;
                    cryRpt.ParameterFields["@xTO_DocMonth"].CurrentValues.Add(paramTO);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Demo_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for SSERP_STK_RETURN_SPVGL_PRWISE"
                if (CommonData.ViewReport == "SSERP_STK_RETURN_SPVGL_PRWISE")
                {
                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSERP_STK_RETURN_SPVGL_PRWISE.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramfROM = new ParameterDiscreteValue();
                    paramfROM.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramfROM);

                    ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                    paramTO.Value = LeadType;
                    cryRpt.ParameterFields["@xTO_DocMonth"].CurrentValues.Add(paramTO);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Demo_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for SSERP_STK_RETURN_SPVGL_BRANCH_WISE"
                if (CommonData.ViewReport == "SSERP_STK_RETURN_SPVGL_BRANCH_WISE")
                {
                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSERP_STK_RETURN_BRVGL.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramfROM = new ParameterDiscreteValue();
                    paramfROM.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramfROM);

                    ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                    paramTO.Value = LeadType;
                    cryRpt.ParameterFields["@xTO_DocMonth"].CurrentValues.Add(paramTO);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Demo_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for SSERP_STK_RETURN_GL_IND"
                if (CommonData.ViewReport == "SSERP_STK_RETURN_GL_IND")
                {
                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSERP_STK_RETURN_GL_IND.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramfROM = new ParameterDiscreteValue();
                    paramfROM.Value = BranchCode;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramfROM);

                    ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                    paramTO.Value = DocMonth;
                    cryRpt.ParameterFields["@xTO_DocMonth"].CurrentValues.Add(paramTO);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Report_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for SALES_INVOICE_BULK_PRINTING"
                if (CommonData.ViewReport == "SALES_INVOICE_BULK_PRINTING")
                {
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_SALES_REG_DETL_SPLT_INV_PRINT.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = FinYear;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramDist);

                    //ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    //paramEcode.Value = CompanyCode;
                    //cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramfROM = new ParameterDiscreteValue();
                    paramfROM.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramfROM);

                    ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                    paramTO.Value = DocMonth;
                    cryRpt.ParameterFields["@xFRDT"].CurrentValues.Add(paramTO);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = DocMonth;
                    cryRpt.ParameterFields["@xTODT"].CurrentValues.Add(paramStatus);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = LeadType;
                    cryRpt.ParameterFields["@XFromInvNo"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToInvNo"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRep);
                }
                #endregion

                #region "This is used for SP_DCST_INTRSTATE_EQLCOMP"
                if (CommonData.ViewReport == "SP_DCST_INTRSTATE_EQLCOMP")
                {
                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSERP_REP_SP_INTSTATE_STOCK_DETL_EQLCOMP.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramfROM = new ParameterDiscreteValue();
                    paramfROM.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramfROM);

                    ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                    paramTO.Value = LeadType;
                    cryRpt.ParameterFields["@xTO_DocMonth"].CurrentValues.Add(paramTO);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Demo_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for SP_DCST_INTRSTATE_DIFFCOMP"
                if (CommonData.ViewReport == "SP_DCST_INTRSTATE_DIFFCOMP")
                {
                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSERP_REP_SP_INTSTATE_STOCK_DETL_EQLCOMP.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramfROM = new ParameterDiscreteValue();
                    paramfROM.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramfROM);

                    ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                    paramTO.Value = LeadType;
                    cryRpt.ParameterFields["@xTO_DocMonth"].CurrentValues.Add(paramTO);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Demo_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramStatus);
                }
                #endregion                

                #region "This is used for SSERP_REP_EMP_WISE_FIELD_SUPPORT_DETAILS"

                if (CommonData.ViewReport == "SSERP_REP_EMP_WISE_FIELD_SUPPORT_DETAILS")
                {
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSERP_REP_FIELD_SUPPORT_DETAILS.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompany"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramDistrict = new ParameterDiscreteValue();
                    paramDistrict.Value = LeadType;
                    cryRpt.ParameterFields["@xTo_DocMonth"].CurrentValues.Add(paramDistrict);

                    ParameterDiscreteValue paramVillage = new ParameterDiscreteValue();
                    paramVillage.Value = Demo_Type;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramVillage);

                    ParameterDiscreteValue paramProdType = new ParameterDiscreteValue();
                    paramProdType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRep_Type"].CurrentValues.Add(paramProdType);

                }

                #endregion

                #region "This is used for SSERP_REP_SR_WISE_TOP_TO_BOTTOM"

                if (CommonData.ViewReport == "SSERP_REP_SR_WISE_TOP_TO_BOTTOM")
                {
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSERP_REP_SR_WISE_TOP_TO_BOTTOM.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompany"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramLogBranCode = new ParameterDiscreteValue();
                    paramLogBranCode.Value = LeadType;
                    cryRpt.ParameterFields["@xLog_BranCode"].CurrentValues.Add(paramLogBranCode);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRep_Type"].CurrentValues.Add(paramRepType);

                }

                #endregion

                #region "This is used for TRANSPORT COST_SALES & REPLACEMENT"
                if (CommonData.ViewReport == "TRANSPORT_COST_SALES_REPLACEMENT")
                {
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_TRANSPORT_COST_SUMMARY.rpt");



                    ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                    paramcmdCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@sCompCode"].CurrentValues.Add(paramcmdCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@sBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramLogBranch = new ParameterDiscreteValue();
                    paramLogBranch.Value = DocMonth;
                    cryRpt.ParameterFields["@sLogBranchCode"].CurrentValues.Add(paramLogBranch);

                    ParameterDiscreteValue paramFinyear = new ParameterDiscreteValue();
                    paramFinyear.Value = LeadType;
                    cryRpt.ParameterFields["@sFinYear"].CurrentValues.Add(paramFinyear);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = Demo_Type;
                    cryRpt.ParameterFields["@sDocMonth"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Report_Type;
                    cryRpt.ParameterFields["@sRepType"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "This is used for STATIONARY WITH OUT INDENT DISPATCH DETAILS"
                if (CommonData.ViewReport == "STATIONARY_WITH_OUT_INDENT_FOR_DC")
                {
                    cryRpt.Load(strPath + "\\Reports\\StationaryReports\\SSCRM_REP_STATIONARY_DELIVERY_WITH_OUT_INDENT_CHALLAN.rpt");

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
                    cryRpt.ParameterFields["@DcNo"].CurrentValues.Add(paramEcode);
                }
                #endregion

                #region "This is used for GROUP_WISE_STOCK_RECONCILIATION"

                if (CommonData.ViewReport == "GROUP_WISE_STOCK_RECONCILIATION")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSERP_REP_GROUP_WISE_STOCK_RECONCILIATION.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_Month"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramLogBranCode = new ParameterDiscreteValue();
                    paramLogBranCode.Value = LeadType;
                    cryRpt.ParameterFields["@xLog_Branch"].CurrentValues.Add(paramLogBranCode);

                    ParameterDiscreteValue paramEmpCode = new ParameterDiscreteValue();
                    paramEmpCode.Value = E_code;
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEmpCode);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }

                #endregion

                #region "This is used for SSERP_REP_TM_AND_ABOVE_STOCK_RECONCILIATION"

                if (CommonData.ViewReport == "SSERP_REP_TM_AND_ABOVE_STOCK_RECONCILIATION")
                {
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSERP_REP_TM_ABOVE_STOCK_RECONCILIATION.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_Month"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramLogBranCode = new ParameterDiscreteValue();
                    paramLogBranCode.Value = LeadType;
                    cryRpt.ParameterFields["@xLog_Branch"].CurrentValues.Add(paramLogBranCode);

                    ParameterDiscreteValue paramEmpCode = new ParameterDiscreteValue();
                    paramEmpCode.Value = E_code;
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEmpCode);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }

                #endregion

                #region "This is used for SSERP_REP_AUDIT_TOUR_SCHEDULE_SUMMARY"

                if (CommonData.ViewReport == "SSERP_REP_AUDIT_TOUR_SCHEDULE_SUMMARY")
                {
                    cryRpt.Load(strPath + "\\Reports\\Audit\\SSERP_REP_AUDIT_TOUR_SCHEDULE_SUMMARY.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = FinYear;
                    cryRpt.ParameterFields["@xdocMM"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramAuditEcode = new ParameterDiscreteValue();
                    paramAuditEcode.Value = DocMonth;
                    cryRpt.ParameterFields["@xAuditorsEcode"].CurrentValues.Add(paramAuditEcode);

                    ParameterDiscreteValue paramFrmDate = new ParameterDiscreteValue();
                    paramFrmDate.Value = LeadType;
                    cryRpt.ParameterFields["@xFRDT"].CurrentValues.Add(paramFrmDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = Demo_Type;
                    cryRpt.ParameterFields["@xTODT"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRepType);

                }

                #endregion

                #region "This is used for SSERP_REP_AUDIT_TOUR_SCHEDULE_SUMMARY"

                if (CommonData.ViewReport == "SSERP_REP_AUDIT_TOUR_SCHEDULE_SUMMARY")
                {
                    cryRpt.Load(strPath + "\\Reports\\SSERP_REP_AUDIT_TOUR_SCHEDULE_SUMMARY.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = FinYear;
                    cryRpt.ParameterFields["@xdocMM"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramAuditEcode = new ParameterDiscreteValue();
                    paramAuditEcode.Value = DocMonth;
                    cryRpt.ParameterFields["@xAuditorsEcode"].CurrentValues.Add(paramAuditEcode);

                    ParameterDiscreteValue paramFrmDate = new ParameterDiscreteValue();
                    paramFrmDate.Value = LeadType;
                    cryRpt.ParameterFields["@xFRDT"].CurrentValues.Add(paramFrmDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = Demo_Type;
                    cryRpt.ParameterFields["@xTODT"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRepType);

                }

                #endregion

                #region "This is used for SSCRM_MIS_REP_GL_LOG_WISE_ORDERFORM_RECONSILATION"
                if (CommonData.ViewReport == "SSCRM_MIS_REP_GL_LOG_WISE_ORDERFORM_RECONSILATION")
                {
                    cryRpt.Load(strPath + "\\Reports\\SSCRM_MIS_REP_GL_LOG_WISE_ORDERFORM_RECONSILATION.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@COMPANY_CODE"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@BRANCH_CODE"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@LogBranchCode"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramfROM = new ParameterDiscreteValue();
                    paramfROM.Value = LeadType;
                    cryRpt.ParameterFields["@FIN_YEAR"].CurrentValues.Add(paramfROM);

                    ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                    paramTO.Value = Demo_Type;
                    cryRpt.ParameterFields["@DOCMONTH"].CurrentValues.Add(paramTO);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Report_Type;
                    cryRpt.ParameterFields["@REPORT_TYPE"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for SSCRM_MIS_REP_TM_ORDERFRM_RECONSILATION"
                if (CommonData.ViewReport == "SSCRM_MIS_REP_TM_ORDERFRM_RECONSILATION")
                {
                    cryRpt.Load(strPath + "\\Reports\\SSCRM_MIS_REP_TM_ORDERFRM_RECONSILATION.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@COMPANY_CODE"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@BRANCH_CODE"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@LogBranchCode"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramfROM = new ParameterDiscreteValue();
                    paramfROM.Value = LeadType;
                    cryRpt.ParameterFields["@FIN_YEAR"].CurrentValues.Add(paramfROM);

                    ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                    paramTO.Value = Demo_Type;
                    cryRpt.ParameterFields["@DOCMONTH"].CurrentValues.Add(paramTO);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Report_Type;
                    cryRpt.ParameterFields["@REPORT_TYPE"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for SSCRM_MIS_GL_WISE_BRANCH_PERFORMANCE_STMENT"
                if (CommonData.ViewReport == "SSCRM_MIS_GL_WISE_BRANCH_PERFORMANCE_STMENT")
                {
                    cryRpt.Load(strPath + "\\Reports\\SSCRM_MIS_GL_WISE_BRANCH_PERFORMANCE_STMENT.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xLogBranch"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramfROM = new ParameterDiscreteValue();
                    paramfROM.Value = LeadType;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramfROM);

                    ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                    paramTO.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramTO);

                
                }
                #endregion

                //#region "This is used for SSCRM_MIS_GL_WISE_BRANCH_PERFORMANCE_STMENT"
                //if (CommonData.ViewReport == "SSCRM_MIS_GL_WISE_BRANCH_PERFORMANCE_STMENT")
                //{
                //    cryRpt.Load(strPath + "\\Reports\\SSCRM_MIS_GL_WISE_BRANCH_PERFORMANCE_STMENT.rpt");

                //    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                //    paramComp.Value = CompanyCode;
                //    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                //    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                //    paramState.Value = BranchCode;
                //    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                //    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                //    paramDist.Value = DocMonth;
                //    cryRpt.ParameterFields["@xLogBranch"].CurrentValues.Add(paramDist);

                //    ParameterDiscreteValue paramfROM = new ParameterDiscreteValue();
                //    paramfROM.Value = LeadType;
                //    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramfROM);

                //    ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                //    paramTO.Value = Demo_Type;
                //    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramTO);


                //}
                //#endregion

                #region "This is used for SSCRM_MIS_GCGL_WISE_SALES_BULLETIN"
                if (CommonData.ViewReport == "SSCRM_MIS_GCGL_WISE_SALES_BULLETIN")
                {
                    cryRpt.Load(strPath + "\\Reports\\SSCRM_MIS_GCGL_WISE_SALES_BULLETIN.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xLogBranch"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramfROM = new ParameterDiscreteValue();
                    paramfROM.Value = LeadType;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramfROM);

                    ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                    paramTO.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramTO);


                }
                #endregion

                #region "This is used for SSCRM_MIS_TMAB_SALES_BULLETIN"
                if (CommonData.ViewReport == "SSCRM_MIS_TMAB_SALES_BULLETIN")
                {
                    cryRpt.Load(strPath + "\\Reports\\SSCRM_MIS_REP_TMWISE_SALESBULLETINS.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramfROM = new ParameterDiscreteValue();
                    paramfROM.Value = LeadType;
                    cryRpt.ParameterFields["@xTMECode"].CurrentValues.Add(paramfROM);

                    ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                    paramTO.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramTO);

                  

                }
                #endregion

                #region "This is used for SSERP_REP_AO_WISE_STOCK_RECONCILIATION"

                if (CommonData.ViewReport == "SSERP_REP_AO_WISE_STOCK_RECONCILIATION")
                {
                    cryRpt.Load(strPath + "\\Reports\\Services\\SSCRM_REP_AO_WISE_RECONCILIATION_STMT.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompany"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramAuditEcode = new ParameterDiscreteValue();
                    paramAuditEcode.Value = LeadType;
                    cryRpt.ParameterFields["@xTo_DocMonth"].CurrentValues.Add(paramAuditEcode);

                    ParameterDiscreteValue paramFrmDate = new ParameterDiscreteValue();
                    paramFrmDate.Value = Demo_Type;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramFrmDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = Report_Type;
                    cryRpt.ParameterFields["@xRep_Type"].CurrentValues.Add(paramToDate);

                }

                #endregion

                #region "This is used for SSCRM_REP_SALE_BY_ORDNO"

                if (CommonData.ViewReport == "SSCRM_REP_SALE_BY_ORDNO")
                {
                    cryRpt.Load(strPath + "\\Reports\\SSCRM_REP_SALE_BY_ORDNO.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@CompanyCode"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@BranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xLogBranch"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramAuditEcode = new ParameterDiscreteValue();
                    paramAuditEcode.Value = Report_Type;
                    cryRpt.ParameterFields["@DocMonth"].CurrentValues.Add(paramAuditEcode);                  

                }

                #endregion

                #region "This is used for REP_GC_WISE_PRODUCT_REC_SUMMARY"
                if (CommonData.ViewReport == "REP_GC_WISE_PRODUCT_REC_SUMMARY")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_SALE_BULTIN_GC_WISE_CRTABx.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_Month"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = Convert.ToInt32(LeadType);
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for REP_LOGICAL_BRANCH_WISE_GCGL_SALES_ACCOUNTABILITY"
                if (CommonData.ViewReport == "REP_LOGICAL_BRANCH_WISE_GCGL_SALES_ACCOUNTABILITY")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_SALE_BULTIN_ACCOUNTABILITY_LOG_BRANCH_WISE_GCONLYx.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_Month"].CurrentValues.Add(paramDist);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = LeadType;
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramLogBranch = new ParameterDiscreteValue();
                    paramLogBranch.Value = Demo_Type;
                    cryRpt.ParameterFields["@xLog_Branch"].CurrentValues.Add(paramLogBranch);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramStatus);
                }
                #endregion

                #region "This is used for ADVANCE_REFUND_REGISTER REPORT LOGICAL BRANCH WISE"
                if (CommonData.ViewReport == "SSCRM_REP_ADVANCE_REFUND_LOGICALBRANCH")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_ADVANCEREFUND_LOGICALBRANCH.rpt");

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramLogBranch = new ParameterDiscreteValue();
                    paramLogBranch.Value = DocMonth;
                    cryRpt.ParameterFields["@sLogBranchCode"].CurrentValues.Add(paramLogBranch);


                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = LeadType;
                    cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = Demo_Type;
                    cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "This is used for SSCRM_GL_WISE_DOC_SHEET"
                if (CommonData.ViewReport == "SSCRM_GL_WISE_DOC_SHEET")
                {

                    cryRpt.Load(strPath + "\\Reports\\SSCRM_GL_WISE _DOC_SHEET.rpt");

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramLogBranch = new ParameterDiscreteValue();
                    paramLogBranch.Value = DocMonth;
                    cryRpt.ParameterFields["@xLogBranch"].CurrentValues.Add(paramLogBranch);


                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = LeadType;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramTo);

                 
                }
                #endregion

                #region "This is used for SSERP_REP_SP_FORM_N_REGISTER"
                if (CommonData.ViewReport == "SSERP_REP_SP_FORM_N_REGISTER")
                {

                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSERP_REP_SP_FORM_N_REGISTER.rpt");

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramLogBranch = new ParameterDiscreteValue();
                    paramLogBranch.Value = DocMonth;
                    cryRpt.ParameterFields["@xProductId"].CurrentValues.Add(paramLogBranch);


                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = LeadType;
                    cryRpt.ParameterFields["@FromDocMM"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = Demo_Type;
                    cryRpt.ParameterFields["@ToDocMM"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "This is used for SALESORDER_LOGWISE_ORDER_BY_ORDER_NO"
                if (CommonData.ViewReport == "SALESORDER_LOGWISE_ORDER_BY_ORDER_NO")
                {

                    cryRpt.Load(strPath + "\\Reports\\SSCRM_LOG_WISE_SAL_REG.rpt");

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramLogBranch = new ParameterDiscreteValue();
                    paramLogBranch.Value = FinYear;
                    cryRpt.ParameterFields["@xLogBranch"].CurrentValues.Add(paramLogBranch);


                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = LeadType;
                    cryRpt.ParameterFields["@xFRDT"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xTODT"].CurrentValues.Add(paramReportType);

                    ParameterDiscreteValue paramReportType1 = new ParameterDiscreteValue();
                    paramReportType1.Value = Report_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramReportType1);
                }
                #endregion

                #region "This is used for SSERP_REP_SP_VS_PU_DAMAGE_STOCK_DETL"

                if (CommonData.ViewReport == "SSERP_REP_SP_VS_PU_DAMAGE_STOCK_DETL")
                {
                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSERP_REP_SPvsPU_DAMAGED_STOCK_DETL.rpt");

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = LeadType;
                    cryRpt.ParameterFields["@xTO_DocMonth"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "This is used for SSERP_REP_SP_VS_PU_DAMAGE_STOCK_SUMMARY"

                if (CommonData.ViewReport == "SSERP_REP_SP_VS_PU_DAMAGE_STOCK_SUMMARY")
                {
                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSERP_REP_SPvsPU_DAMAGE_STOCK_SUMMARY.rpt");

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = LeadType;
                    cryRpt.ParameterFields["@xTO_DocMonth"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "This is used for SSERP_REP_RECRUITMTENT_ANALYSIS_DASHBOARD_DETLXy"

                if (CommonData.ViewReport == "SSERP_REP_RECRUITMTENT_ANALYSIS_DASHBOARD_DETL")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\StaffDetls\\SSERP_REP_RECRUITMTENT_ANALYSIS_DASHBOARD_DETLXy.rpt");

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = LeadType;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramReportType);

                    ParameterDiscreteValue paramSuccsPoints = new ParameterDiscreteValue();
                    paramSuccsPoints.Value = Report_Type;
                    cryRpt.ParameterFields["@xSuccPTs"].CurrentValues.Add(paramSuccsPoints);
                }
                #endregion

                #region "This is used for SSERP_REP_RECRUITMTENT_ANALYSIS_BY_DEPT"

                if (CommonData.ViewReport == "SSERP_REP_RECRUITMTENT_ANALYSIS_BY_DEPT")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\StaffDetls\\SSERP_REP_RECRUITMTENT_ANALYSIS_DASHBOARD_DEPT.rpt");

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = LeadType;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramReportType);

                    ParameterDiscreteValue paramSuccsPoints = new ParameterDiscreteValue();
                    paramSuccsPoints.Value = Report_Type;
                    cryRpt.ParameterFields["@xSuccPTs"].CurrentValues.Add(paramSuccsPoints);
                }
                #endregion

                #region "This is used for SSCRM_REP_HR_RECRUIT_VS_RESIGND"

                if (CommonData.ViewReport == "SSCRM_REP_HR_RECRUIT_VS_RESIGND")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\SSCRM_REP_HR_RECRUIT_VS_RESIGND.rpt");

                    ParameterDiscreteValue raramEcode = new ParameterDiscreteValue();
                    raramEcode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(raramEcode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = LeadType;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramReportType);

                   
                }
                #endregion

                #region "This Is Used STATIONARY BRANCH WISE DC FILTER  REGISTER"
                if (CommonData.ViewReport == "SSERP_REP_STATIONARY_DELIVERY_CHALLAN_REGISTER")
                {

                    cryRpt.Load(strPath + "\\Reports\\StationaryReports\\SSERP_REP_STATIONARY_DELIVERY_CHALLAN_REGISTER.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFromDate = new ParameterDiscreteValue();
                    paramFromDate.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFromDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = LeadType;
                    cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This Is Used STOCK DESPATCHES & RETURNS STATEMENT"
                if (CommonData.ViewReport == "SSCRM_REP_GL_WISE_STOCK_RECONCILLATION")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_SP_GL_WISE_DC_GRN_RECONCILLIATION.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFromDate = new ParameterDiscreteValue();
                    paramFromDate.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrmDocMonth"].CurrentValues.Add(paramFromDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = LeadType;
                    cryRpt.ParameterFields["@xToDocMonth"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for STOCKPOINT_STOCK_PROCESS REPORT"
                if (CommonData.ViewReport == "MIS_SP_STOCK_PROCESS")
                {
                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSCRM_REP_SP_STOCK_PROCESS.rpt");

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
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramReportType);

                }
                #endregion

                #region "This is used for SSERP_REP_LIST_OF_LOW_PERFORMERS"

                if (CommonData.ViewReport == "SSERP_REP_LIST_OF_LOW_PERFORMERS")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSERP_REP_LIST_OF_LOW_PERF.rpt");

                    ParameterDiscreteValue praramComp = new ParameterDiscreteValue();
                    praramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(praramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = E_code;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = FinYear;
                    cryRpt.ParameterFields["@xTo_DocMonth"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramMnthFlag = new ParameterDiscreteValue();
                    paramMnthFlag.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xMonths_Flag"].CurrentValues.Add(paramMnthFlag);

                    ParameterDiscreteValue paramFrmMnths = new ParameterDiscreteValue();
                    paramFrmMnths.Value = frmMnths;
                    cryRpt.ParameterFields["@xFrom_Mnths"].CurrentValues.Add(paramFrmMnths);

                    ParameterDiscreteValue paramToMnths = new ParameterDiscreteValue();
                    paramToMnths.Value = ToMnths;
                    cryRpt.ParameterFields["@xTo_Mnths"].CurrentValues.Add(paramToMnths);

                    ParameterDiscreteValue paramGrpsFlag = new ParameterDiscreteValue();
                    paramGrpsFlag.Value = LeadType;
                    cryRpt.ParameterFields["@xGroups_Flag"].CurrentValues.Add(paramGrpsFlag);

                    ParameterDiscreteValue paramFrmGrps = new ParameterDiscreteValue();
                    paramFrmGrps.Value = frmGrps;
                    cryRpt.ParameterFields["@xFrom_Grps"].CurrentValues.Add(paramFrmGrps);

                    ParameterDiscreteValue paramToGrps = new ParameterDiscreteValue();
                    paramToGrps.Value = ToGrps;
                    cryRpt.ParameterFields["@xTo_Grps"].CurrentValues.Add(paramToGrps);

                    ParameterDiscreteValue paramPntsFlag = new ParameterDiscreteValue();
                    paramPntsFlag.Value = Demo_Type;
                    cryRpt.ParameterFields["@xPers_Pnts_Flag"].CurrentValues.Add(paramPntsFlag);

                    ParameterDiscreteValue paramFrmPnts = new ParameterDiscreteValue();
                    paramFrmPnts.Value = frmPersPoints;
                    cryRpt.ParameterFields["@xFrm_PersPnts"].CurrentValues.Add(paramFrmPnts);

                    ParameterDiscreteValue paramToPnts = new ParameterDiscreteValue();
                    paramToPnts.Value = ToPersPoints;
                    cryRpt.ParameterFields["@xTo_PersPnts"].CurrentValues.Add(paramToPnts);

                    ParameterDiscreteValue paramPntsPerGrpFlag = new ParameterDiscreteValue();
                    paramPntsPerGrpFlag.Value = strVal2;
                    cryRpt.ParameterFields["@xPnts_PerGrp_Flag"].CurrentValues.Add(paramPntsPerGrpFlag);

                    ParameterDiscreteValue paramFrmPntsPerGrp = new ParameterDiscreteValue();
                    paramFrmPntsPerGrp.Value = frmPntsPerGrp;
                    cryRpt.ParameterFields["@xFrom_Pnts_PerGrp"].CurrentValues.Add(paramFrmPntsPerGrp);

                    ParameterDiscreteValue paramToPntsPerGrp = new ParameterDiscreteValue();
                    paramToPntsPerGrp.Value = ToPntsPerGrp;
                    cryRpt.ParameterFields["@xTo_Pnts_PerGrp"].CurrentValues.Add(paramToPntsPerGrp);

                    ParameterDiscreteValue paramPntsPerHeadFlag = new ParameterDiscreteValue();
                    paramPntsPerHeadFlag.Value = strVal1;
                    cryRpt.ParameterFields["@xPnts_PerHead_Flag"].CurrentValues.Add(paramPntsPerHeadFlag);

                    ParameterDiscreteValue paramFrmPntsPerHead = new ParameterDiscreteValue();
                    paramFrmPntsPerHead.Value = frmPntsPerHead;
                    cryRpt.ParameterFields["@xFrom_Pnts_PerHead"].CurrentValues.Add(paramFrmPntsPerHead);

                    ParameterDiscreteValue paramToPntsPerHead = new ParameterDiscreteValue();
                    paramToPntsPerHead.Value = ToPntsPerHead;
                    cryRpt.ParameterFields["@xTo_Pnts_PerHead"].CurrentValues.Add(paramToPntsPerHead);

                    ParameterDiscreteValue paramSortBy = new ParameterDiscreteValue();
                    paramSortBy.Value = strVal3;
                    cryRpt.ParameterFields["@xSort_By"].CurrentValues.Add(paramSortBy);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramReportType);

                    ParameterDiscreteValue paramPntsHGFlg = new ParameterDiscreteValue();
                    paramPntsHGFlg.Value = strVal4;
                    cryRpt.ParameterFields["@xPnts_PerHeadGrp_Flag"].CurrentValues.Add(paramPntsHGFlg);

                    ParameterDiscreteValue paramLosAsOnDate = new ParameterDiscreteValue();
                    paramLosAsOnDate.Value = strVal5;
                    cryRpt.ParameterFields["@xLosAsOnDate"].CurrentValues.Add(paramLosAsOnDate);


                }
                #endregion

                #region "This is used for SSERP_REP_AO_WISE_REPLACEMENT_REG"

                if (CommonData.ViewReport == "SSERP_REP_AO_WISE_REPLACEMENT_REG")
                {
                    cryRpt.Load(strPath + "\\Reports\\Services\\SSERP_REP_AO_WISE_REPL_REG.rpt");

                    ParameterDiscreteValue praramComp = new ParameterDiscreteValue();
                    praramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(praramComp);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = Convert.ToInt32(DocMonth);
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = LeadType;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = Demo_Type;
                    cryRpt.ParameterFields["@xTo_DocMonth"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "This is used for SSERP_REP_SP_CALIBRATION_CERTIFICATE REPORT"
                if (CommonData.ViewReport == "SSERP_REP_SP_CALIBRATION_CERTIFICATE")
                {
                    cryRpt.Load(strPath + "\\Reports\\FixedAssets\\SSERP_REP_SP_CALIBRATION_CERTIFICATE.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramBranch = new ParameterDiscreteValue();
                    paramBranch.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranch);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = DocMonth;
                    cryRpt.ParameterFields["@xStateCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = LeadType;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRep);


                }
                #endregion    

                #region "This is used for SSERP_REP_SP_TRANSPORT_COST_MONTH_WISE"

                if (CommonData.ViewReport == "SSERP_REP_SP_TRANSPORT_COST_MONTH_WISE")
                {
                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSERP_REP_SP_TRANSPORT_COST_MONTH_WISE.rpt");

                    ParameterDiscreteValue praramComp = new ParameterDiscreteValue();
                    praramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(praramComp);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = LeadType;
                    cryRpt.ParameterFields["@xTo_DocMonth"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "This is used for SSERP_REP_SP_STOCK_TRANSACTIONS_SUMMARY"

                if (CommonData.ViewReport == "SSERP_REP_SP_STOCK_TRANSACTIONS_SUMMARY")
                {
                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSERP_REP_SP_STOCK_TRANSACTIONS_SUMMARY.rpt");

                    ParameterDiscreteValue praramComp = new ParameterDiscreteValue();
                    praramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(praramComp);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = LeadType;
                    cryRpt.ParameterFields["@xTo_DocMonth"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramTrnType = new ParameterDiscreteValue();
                    paramTrnType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xTran_Type"].CurrentValues.Add(paramTrnType);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRep_Type"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "SSCRM_REP_STATIONARY_REC_ALLBR_SUMM"
                if (CommonData.ViewReport == "CATEGORY WISE BRANCH ITEMS SEARCH")
                {

                    cryRpt.Load(strPath + "\\Reports\\StationaryReports\\SSCRM_REP_STATIONARY_CATEGORY_SEARCH_PIVOT.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFromDate = new ParameterDiscreteValue();
                    paramFromDate.Value = FinYear;
                    cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFromDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = DocMonth;
                    cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramItemId = new ParameterDiscreteValue();
                    paramItemId.Value = LeadType;
                    cryRpt.ParameterFields["@xItemID"].CurrentValues.Add(paramItemId);

                    ParameterDiscreteValue paramTrnType = new ParameterDiscreteValue();
                    paramTrnType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xTrnType"].CurrentValues.Add(paramTrnType);


                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@RepType"].CurrentValues.Add(paramRepType);
                }
                if (CommonData.ViewReport == "BRANCH WISE ITEMS SEARCH")
                {

                    cryRpt.Load(strPath + "\\Reports\\StationaryReports\\SSCRM_REP_STATIONARY_CATEGORY_SEARCH.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFromDate = new ParameterDiscreteValue();
                    paramFromDate.Value = FinYear;
                    cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFromDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = DocMonth;
                    cryRpt.ParameterFields["@xTo"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramItemId = new ParameterDiscreteValue();
                    paramItemId.Value = LeadType;
                    cryRpt.ParameterFields["@xItemID"].CurrentValues.Add(paramItemId);

                    ParameterDiscreteValue paramTrnType = new ParameterDiscreteValue();
                    paramTrnType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xTrnType"].CurrentValues.Add(paramTrnType);


                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@RepType"].CurrentValues.Add(paramRepType);
                }


                #endregion

                #region "This is used for SSERP_REP_SP_TRANSPORT_COST_TRIP_WISE"

                if (CommonData.ViewReport == "SSERP_REP_SP_TRANSPORT_COST_TRIP_WISE")
                {
                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSERP_REP_TRANSPORT_COST_TRIP_WISE.rpt");

                    ParameterDiscreteValue praramComp = new ParameterDiscreteValue();
                    praramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(praramComp);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = LeadType;
                    cryRpt.ParameterFields["@xTo_DocMonth"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region This is Used for Field Support "SSERP_REP_FieldSupport_Deviations"
                if (CommonData.ViewReport == "SSERP_REP_FieldSupport_Deviations_GC2ABM")
                {
                    cryRpt.Load(strPath + "\\Reports\\SSCRM_FIELD_SUPPORT_DEVIATIONS_GC_ABM.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompany"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranch"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFromdoc = new ParameterDiscreteValue();
                    paramFromdoc.Value = FinYear;
                    cryRpt.ParameterFields["@xFromDocMonth"].CurrentValues.Add(paramFromdoc);

                    ParameterDiscreteValue paramToDoc = new ParameterDiscreteValue();
                    paramToDoc.Value = DocMonth;
                    cryRpt.ParameterFields["@xToDocMonth"].CurrentValues.Add(paramToDoc);

                    ParameterDiscreteValue paramGroups = new ParameterDiscreteValue();
                    paramGroups.Value = LeadType;
                    cryRpt.ParameterFields["@xGroupFrom"].CurrentValues.Add(paramGroups);

                    ParameterDiscreteValue paramTogroups = new ParameterDiscreteValue();
                    paramTogroups.Value = Demo_Type;
                    cryRpt.ParameterFields["@xGroupTo"].CurrentValues.Add(paramTogroups);


                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xReportType"].CurrentValues.Add(paramRepType);
                }
                if (CommonData.ViewReport == "SSERP_REP_FieldSupport_Deviations_DBM2SR.BM")
                {


                    cryRpt.Load(strPath + "\\Reports\\SSCRM_FIELD_SUPPORT_DEVEATIONS_DBM_SR.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompany"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranch"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramFromdoc = new ParameterDiscreteValue();
                    paramFromdoc.Value = FinYear;
                    cryRpt.ParameterFields["@xFromDocMonth"].CurrentValues.Add(paramFromdoc);

                    ParameterDiscreteValue paramToDoc = new ParameterDiscreteValue();
                    paramToDoc.Value = DocMonth;
                    cryRpt.ParameterFields["@xToDocMonth"].CurrentValues.Add(paramToDoc);

                    ParameterDiscreteValue paramGroups = new ParameterDiscreteValue();
                    paramGroups.Value = LeadType;
                    cryRpt.ParameterFields["@xGroupFrom"].CurrentValues.Add(paramGroups);

                    ParameterDiscreteValue paramTogroups = new ParameterDiscreteValue();
                    paramTogroups.Value = Demo_Type;
                    cryRpt.ParameterFields["@xGroupTo"].CurrentValues.Add(paramTogroups);


                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xReportType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for SSERP_REP_RECRUITMENT_VS_RETAINED_SRS"

                if (CommonData.ViewReport == "SSERP_REP_RECRUITMENT_VS_RETAINED_SRS")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\StaffDetls\\SSERP_REP_RECRUITMENT_VS_RETENTION_SRs.rpt");

                    ParameterDiscreteValue praramComp = new ParameterDiscreteValue();
                    praramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xComp_Code"].CurrentValues.Add(praramComp);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = FinYear;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = DocMonth;
                    cryRpt.ParameterFields["@xTo_DocMonth"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramFrmGrps = new ParameterDiscreteValue();
                    paramFrmGrps.Value = Convert.ToInt32(LeadType);
                    cryRpt.ParameterFields["@xFrom_Grps"].CurrentValues.Add(paramFrmGrps);

                    ParameterDiscreteValue paramToGrps = new ParameterDiscreteValue();
                    paramToGrps.Value = Convert.ToInt32(Demo_Type);
                    cryRpt.ParameterFields["@xTo_Grps"].CurrentValues.Add(paramToGrps);

                    ParameterDiscreteValue paramPoints = new ParameterDiscreteValue();
                    paramPoints.Value = Convert.ToInt32(Report_Type);
                    cryRpt.ParameterFields["@xPoints"].CurrentValues.Add(paramPoints);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = strVal1;
                    cryRpt.ParameterFields["@xRep_Type"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "This is used for SSERP_REP_BRANCH_MEETING_DETAILS"

                if (CommonData.ViewReport == "SSERP_REP_BRANCH_MEETING_DETAILS")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\Training\\SSCRM_REP_BRANCH_MEETING_DETL.rpt");

                    ParameterDiscreteValue praramComp = new ParameterDiscreteValue();
                    praramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xComp_Code"].CurrentValues.Add(praramComp);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom_Date"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = LeadType;
                    cryRpt.ParameterFields["@xTo_Date"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRep_Type"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "This is used for SSERP_REP_GROUPS_SALES_ANALYSATION"

                if (CommonData.ViewReport == "SSERP_REP_GROUPS_SALES_ANALYSATION")
                {

                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSERP_REP_GROUPS_SALES_ANALYSATION.rpt");

                    ParameterDiscreteValue praramComp = new ParameterDiscreteValue();
                    praramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xComp_Code"].CurrentValues.Add(praramComp);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = FinYear;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = DocMonth;
                    cryRpt.ParameterFields["@xTo_DocMonth"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramGrpRange = new ParameterDiscreteValue();
                    paramGrpRange.Value = LeadType;
                    cryRpt.ParameterFields["@xGrp_Range"].CurrentValues.Add(paramGrpRange);

                    ParameterDiscreteValue paramFrmGrp = new ParameterDiscreteValue();
                    paramFrmGrp.Value = Convert.ToInt32(Demo_Type);
                    cryRpt.ParameterFields["@xFrom_Grps"].CurrentValues.Add(paramFrmGrp);

                    ParameterDiscreteValue paramToGrp = new ParameterDiscreteValue();
                    paramToGrp.Value = Convert.ToInt32(Report_Type);
                    cryRpt.ParameterFields["@xTo_Grps"].CurrentValues.Add(paramToGrp);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = strVal1;
                    cryRpt.ParameterFields["@xRep_Type"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "This is used for SSERP_REP_EMP_AWARD_DETL"

                if (CommonData.ViewReport == "SSERP_REP_EMP_AWARD_DETL")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\SSERP_REP_EMP_AWARD_DETL.rpt");

                    ParameterDiscreteValue praramComp = new ParameterDiscreteValue();
                    praramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(praramComp);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramEmpCode = new ParameterDiscreteValue();
                    paramEmpCode.Value = FinYear;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramEmpCode);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xFrom_Date"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = LeadType;
                    cryRpt.ParameterFields["@xTo_Date"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramEvent = new ParameterDiscreteValue();
                    paramEvent.Value = Convert.ToInt32(Demo_Type);
                    cryRpt.ParameterFields["@xEventId"].CurrentValues.Add(paramEvent);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRep_Type"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "This is used for SSERP_REP_SP_SALES_REGISTER_DETL_FORMAT2"
                if (CommonData.ViewReport == "SSERP_REP_SP_SALES_REGISTER_DETL_FORMAT2")
                {

                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSERP_REP_SP_SALESREGReport2.rpt");

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
                    paramRepType.Value = "SP_INVOICE";
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for Sales Register With Out Customer Data"
                if (CommonData.ViewReport == "SSCRM_REP_INVOICE_WISE_DOC_SHEET")
                {

                    cryRpt.Load(strPath + "\\Reports\\SSCRM_INVOICE_WISE _DOC_SHEET.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CommonData.CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = CommonData.BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramLogBranch = new ParameterDiscreteValue();
                    paramLogBranch.Value = DocMonth;
                    cryRpt.ParameterFields["@xLogBranch"].CurrentValues.Add(paramLogBranch);

                    ParameterDiscreteValue paramFromDate = new ParameterDiscreteValue();
                    paramFromDate.Value = LeadType;
                    cryRpt.ParameterFields["@xfrmDocMonth"].CurrentValues.Add(paramFromDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToDocMonth"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for STOCK_POINT_SALES_INVOICE_BULK_PRINTING"
                if (CommonData.ViewReport == "STOCK_POINT_SALES_INVOICE_BULK_PRINTING")
                {
                    cryRpt.Load(strPath + "\\Reports\\StockPoint Reports\\SSCRM_REP_SALES_REG_DETL_SP_INV_PRINT.rpt");

                    ParameterDiscreteValue paramComp = new ParameterDiscreteValue();
                    paramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramComp);

                    ParameterDiscreteValue paramState = new ParameterDiscreteValue();
                    paramState.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramState);

                    ParameterDiscreteValue paramDist = new ParameterDiscreteValue();
                    paramDist.Value = FinYear;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramDist);

                    //ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    //paramEcode.Value = CompanyCode;
                    //cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramfROM = new ParameterDiscreteValue();
                    paramfROM.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramfROM);

                    ParameterDiscreteValue paramTO = new ParameterDiscreteValue();
                    paramTO.Value = DocMonth;
                    cryRpt.ParameterFields["@xFRDT"].CurrentValues.Add(paramTO);

                    ParameterDiscreteValue paramStatus = new ParameterDiscreteValue();
                    paramStatus.Value = DocMonth;
                    cryRpt.ParameterFields["@xTODT"].CurrentValues.Add(paramStatus);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = LeadType;
                    cryRpt.ParameterFields["@XFromInvNo"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToInvNo"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRep);
                }
                #endregion

                #region "This is used for SSERP_REP_AGE_WISE_SR_ANALYSIS"

                if (CommonData.ViewReport == "SSERP_REP_AGE_WISE_SR_ANALYSIS")
                {
                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\SSERP_REP_SRs_ANALYSIS_BY_AGE.rpt");


                    ParameterDiscreteValue praramComp = new ParameterDiscreteValue();
                    praramComp.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(praramComp);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramEmpCode = new ParameterDiscreteValue();
                    paramEmpCode.Value = FinYear;
                    cryRpt.ParameterFields["@xFrom_DocMonth"].CurrentValues.Add(paramEmpCode);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = DocMonth;
                    cryRpt.ParameterFields["@xTo_DocMonth"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = Convert.ToInt32(LeadType);
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramEvent = new ParameterDiscreteValue();
                    paramEvent.Value = iFrom;
                    cryRpt.ParameterFields["@xSucc_Pnts"].CurrentValues.Add(paramEvent);

                    ParameterDiscreteValue paramReportType = new ParameterDiscreteValue();
                    paramReportType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramReportType);
                }
                #endregion

                #region "SSCRM_REP_SALES_REG_VAT_CAL_SUM"
                if (CommonData.ViewReport == "SSCRM_REP_SALES_REG_VAT_CAL_SUM")
                {
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SSCRM_REP_SALES_REG_DETL_VAT_CAL_SUM.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCMPNY"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = DocMonth;
                    cryRpt.ParameterFields["@xDOC_MONTH"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFromDate = new ParameterDiscreteValue();
                    paramFromDate.Value = LeadType;
                    cryRpt.ParameterFields["@xFRDT"].CurrentValues.Add(paramFromDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = Demo_Type;
                    cryRpt.ParameterFields["@xTODT"].CurrentValues.Add(paramToDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xREP_TYPE"].CurrentValues.Add(paramRepType);
                }
                #endregion

                #region "This is used for EMP_WISE_DAILY_ATTENDANCE"
                if (CommonData.ViewReport == "EMP_WISE_DAILY_ATTD")
                {
                    cryRpt.Load(strPath + "\\Reports\\BioMetrics\\HR_EMP_IND_ATTD_DETL.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = FinYear;
                    cryRpt.ParameterFields["@xDeptCode"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = DocMonth;
                    cryRpt.ParameterFields["@xECode"].CurrentValues.Add(paramDocMon);

                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = "";
                    cryRpt.ParameterFields["@xWagePeriod"].CurrentValues.Add(paramRep);

                    ParameterDiscreteValue paramFrom = new ParameterDiscreteValue();
                    paramFrom.Value = LeadType;
                    cryRpt.ParameterFields["@xFrom"].CurrentValues.Add(paramFrom);

                    ParameterDiscreteValue paramTo = new ParameterDiscreteValue();
                    paramTo.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToday"].CurrentValues.Add(paramTo);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xPROCTYPE"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for EMP_WISE_LEAVE_RECONCILIATION"
                if (CommonData.ViewReport == "EMP_WISE_LEAVE_RECONCILIATION")
                {
                    cryRpt.Load(strPath + "\\Reports\\BioMetrics\\HR_EMP_LEAVE_RECON.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompany"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = Convert.ToInt32(DocMonth);
                    cryRpt.ParameterFields["@xYear"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramDept = new ParameterDiscreteValue();
                    paramDept.Value = LeadType;
                    cryRpt.ParameterFields["@xDepartment"].CurrentValues.Add(paramDept);

                    ParameterDiscreteValue paramEcode = new ParameterDiscreteValue();
                    paramEcode.Value = Demo_Type;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramEcode);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for Vehicle_Loan_Legal_Notice_Print"
                if (CommonData.ViewReport == "Vehicle_Loan_Legal_Notice_Print")
                {
                    cryRpt.Load(strPath + "\\Reports\\Legal\\Vehicle_Loan_Legal_Notice_Print.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = rSalesInvoice;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = DocMonth;
                    cryRpt.ParameterFields["@xLetterNo"].CurrentValues.Add(paramBranCode);
                }
                #endregion

                #region "SATL_REP_CBBOOK_Report"
                if (CommonData.ViewReport == "SATL_REP_CBBOOK_Report")
                {
                    cryRpt.Load(strPath + "\\Reports\\Financial Accounts\\SATL_REP_CBBOOK_Report.rpt");

                    ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                    paramcmdCode.Value = CommonData.CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramcmdCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = CommonData.BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = DocMonth;
                    cryRpt.ParameterFields["@xCashBankAccount"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrmDate = new ParameterDiscreteValue();
                    paramFrmDate.Value = Convert.ToDateTime(CompanyCode).ToString("dd/MMM/yyy");
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFrmDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = Convert.ToDateTime(BranchCode).ToString("dd/MMM/yyyy");
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramToDate);



                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = Report_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);
                }
                #endregion

                #region "SATL_REP_CASH_DFR"
                if (CommonData.ViewReport == "SSCRM_REP_CASH_DFR")
                {
                    cryRpt.Load(strPath + "\\Reports\\Financial Accounts\\SSCRM_REP_CASH_DFR.rpt");

                    ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                    paramcmdCode.Value = CommonData.CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramcmdCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = CommonData.BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = FinYear;
                    cryRpt.ParameterFields["@xCashBankAccount"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrmDate = new ParameterDiscreteValue();
                    paramFrmDate.Value = DocMonth;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFrmDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = LeadType;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramToDate);



                    ParameterDiscreteValue paramRep = new ParameterDiscreteValue();
                    paramRep.Value = "";
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRep);
                    ParameterDiscreteValue parameCODE = new ParameterDiscreteValue();
                    parameCODE.Value = 0;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(parameCODE);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = CommonData.FinancialYear;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramFinYear);



                    //cryRpt.SetParameterValue("@xCompCode", CommonData.CompanyCode, cryRpt.Subreports[0].Name.ToString());
                    //cryRpt.SetParameterValue("@xBranchCode", CommonData.BranchCode, cryRpt.Subreports[0].Name.ToString());
                    //cryRpt.SetParameterValue("@xFinYear", CommonData.FinancialYear, cryRpt.Subreports[0].Name.ToString());
                    //cryRpt.SetParameterValue("@xFromDate", DocMonth, cryRpt.Subreports[0].Name.ToString());
                    //cryRpt.SetParameterValue("@xToDate", LeadType, cryRpt.Subreports[0].Name.ToString());
                    






                }
                #endregion


                #region "SSCRM_REP_DAY_WISE_DENIM_DETL1"
                if (CommonData.ViewReport == "SSCRM_REP_DAY_WISE_DENIM_DETL1")
                {
                    cryRpt.Load(strPath + "\\Reports\\Financial Accounts\\SSCRM_REP_DAY_WISE_DENIM_DETL1.rpt");

                    ParameterDiscreteValue paramcmdCode = new ParameterDiscreteValue();
                    paramcmdCode.Value = CommonData.CompanyCode;
                    cryRpt.ParameterFields["@xCompCode"].CurrentValues.Add(paramcmdCode);

                    ParameterDiscreteValue paramBrancCode = new ParameterDiscreteValue();
                    paramBrancCode.Value = CommonData.BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBrancCode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = DocMonth;
                    cryRpt.ParameterFields["@xFinYear"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrmDate = new ParameterDiscreteValue();
                    paramFrmDate.Value = LeadType;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFrmDate);

                    ParameterDiscreteValue paramToDate = new ParameterDiscreteValue();
                    paramToDate.Value = Demo_Type;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramToDate);


                }
                #endregion

                #region "This is used for AUDIT_PHY_COUNTING_STK_CLOSING_STATUS"
                if (CommonData.ViewReport == "SSERP_REP_SP_PHY_STK_CLOS_STATUS")
                {
                    cryRpt.Load(strPath + "\\Reports\\Audit\\SSERP_REP_SP_PHY_STK_CLOS_STATUS.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xComp_Code"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranch_Code"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = DocMonth;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramDept = new ParameterDiscreteValue();
                    paramDept.Value = LeadType;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramDept);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for AUDIT_PHY_COUNTING_STK_DIFF"
                if (CommonData.ViewReport == "SSERP_REP_AUDIT_PHY_CNT_STK_DIFF")
                {

                    cryRpt.Load(strPath + "\\Reports\\Audit\\SSERP_REP_SP_PHY_AND_STK_CNT_DIFF.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xComp_Code"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranch_Code"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = DocMonth;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramDept = new ParameterDiscreteValue();
                    paramDept.Value = LeadType;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramDept);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for AUDIT_PHY_STK_NON_CNT_PROD_DETL"
                if (CommonData.ViewReport == "SSERP_REP_AUDIT_PHY_STK_NON_CNT_PROD_DETL")
                {
                    cryRpt.Load(strPath + "\\Reports\\Audit\\SSERP_REP_AUDIT_PHY_STK_NON_CNT_PROD_DETL.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xComp_Code"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranch_Code"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = DocMonth;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramDept = new ParameterDiscreteValue();
                    paramDept.Value = LeadType;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramDept);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for AUDIT_PHY_STK_CNT_PLANTS_DESTROY_DETL"
                if (CommonData.ViewReport == "SSERP_REP_AUDIT_PHY_STK_CNT_PLANTS_DETL")
                {

                    cryRpt.Load(strPath + "\\Reports\\Audit\\SSERP_REP_AUDIT_PHY_CNT_PLANTS_DESTROY_REP.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xComp_Code"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranch_Code"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramFinYear = new ParameterDiscreteValue();
                    paramFinYear.Value = DocMonth;
                    cryRpt.ParameterFields["@xFromDate"].CurrentValues.Add(paramFinYear);

                    ParameterDiscreteValue paramDept = new ParameterDiscreteValue();
                    paramDept.Value = LeadType;
                    cryRpt.ParameterFields["@xToDate"].CurrentValues.Add(paramDept);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRepType"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for SSERP_REP_SALES_EMP_LIST_BASED_ON_GRPS"
                if (CommonData.ViewReport == "SSERP_REP_SALES_EMP_LIST")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\StaffDetls\\SSERP_REP_SALES_EMP_LIST_FILTER_BY_GRPS.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_Month"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrmGrps = new ParameterDiscreteValue();
                    paramFrmGrps.Value = iApprovedId;
                    cryRpt.ParameterFields["@xFrom_Grps"].CurrentValues.Add(paramFrmGrps);

                    ParameterDiscreteValue paramToGrps = new ParameterDiscreteValue();
                    paramToGrps.Value = E_code;
                    cryRpt.ParameterFields["@xTo_Grps"].CurrentValues.Add(paramToGrps);

                    ParameterDiscreteValue paramFrmDesgId = new ParameterDiscreteValue();
                    paramFrmDesgId.Value = iFrom;
                    cryRpt.ParameterFields["@xFrom_DesgId"].CurrentValues.Add(paramFrmDesgId);

                    ParameterDiscreteValue paramToDesgId = new ParameterDiscreteValue();
                    paramToDesgId.Value = iTo;
                    cryRpt.ParameterFields["@xTo_DesgId"].CurrentValues.Add(paramToDesgId);

                    ParameterDiscreteValue paramLOSAsOnDate = new ParameterDiscreteValue();
                    paramLOSAsOnDate.Value = LeadType;
                    cryRpt.ParameterFields["@xLOsAsOnDate"].CurrentValues.Add(paramLOSAsOnDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRep_Type"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for SSERP_REP_SALES_EMP_SUM_BASED_ON_GRPS"
                if (CommonData.ViewReport == "SSERP_REP_SALES_EMP_SUM_BASED_ON_GRPS")
                {

                    cryRpt.Load(strPath + "\\Reports\\HR Reports\\StaffDetls\\SSERP_REP_SALES_EMP_SUM_BY_GRPS_RANGE.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xcmp_cd"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramDocMonth = new ParameterDiscreteValue();
                    paramDocMonth.Value = DocMonth;
                    cryRpt.ParameterFields["@xDoc_Month"].CurrentValues.Add(paramDocMonth);

                    ParameterDiscreteValue paramFrmGrps = new ParameterDiscreteValue();
                    paramFrmGrps.Value = iApprovedId;
                    cryRpt.ParameterFields["@xFrom_Grps"].CurrentValues.Add(paramFrmGrps);

                    ParameterDiscreteValue paramToGrps = new ParameterDiscreteValue();
                    paramToGrps.Value = E_code;
                    cryRpt.ParameterFields["@xTo_Grps"].CurrentValues.Add(paramToGrps);

                    ParameterDiscreteValue paramFrmDesgId = new ParameterDiscreteValue();
                    paramFrmDesgId.Value = iFrom;
                    cryRpt.ParameterFields["@xFrom_DesgId"].CurrentValues.Add(paramFrmDesgId);

                    ParameterDiscreteValue paramToDesgId = new ParameterDiscreteValue();
                    paramToDesgId.Value = iTo;
                    cryRpt.ParameterFields["@xTo_DesgId"].CurrentValues.Add(paramToDesgId);

                    ParameterDiscreteValue paramLOSAsOnDate = new ParameterDiscreteValue();
                    paramLOSAsOnDate.Value = LeadType;
                    cryRpt.ParameterFields["@xLOsAsOnDate"].CurrentValues.Add(paramLOSAsOnDate);

                    ParameterDiscreteValue paramRepType = new ParameterDiscreteValue();
                    paramRepType.Value = Demo_Type;
                    cryRpt.ParameterFields["@xRep_Type"].CurrentValues.Add(paramRepType);

                }
                #endregion

                #region "This is used for SalesSRAdoptionDetails"

                if (CommonData.ViewReport == "SalesSRAdoptionDetails")
                {
                    cryRpt.Load(strPath + "\\Reports\\Sales Reports\\SalesSRAdoptionDetails.rpt");

                    ParameterDiscreteValue paramCompCode = new ParameterDiscreteValue();
                    paramCompCode.Value = CompanyCode;
                    cryRpt.ParameterFields["@xCompany"].CurrentValues.Add(paramCompCode);

                    ParameterDiscreteValue paramBranCode = new ParameterDiscreteValue();
                    paramBranCode.Value = BranchCode;
                    cryRpt.ParameterFields["@xBranchCode"].CurrentValues.Add(paramBranCode);

                    ParameterDiscreteValue paramProdType = new ParameterDiscreteValue();
                    paramProdType.Value = DocMonth;
                    cryRpt.ParameterFields["@xEcode"].CurrentValues.Add(paramProdType);

                    ParameterDiscreteValue paramDocMon = new ParameterDiscreteValue();
                    paramDocMon.Value = Report_Type;
                    cryRpt.ParameterFields["@xDocMonth"].CurrentValues.Add(paramDocMon);


                }

                #endregion



                SetDataSourceConnectionToReport(ref cryRpt);
                    rptViewer.ReportSource = cryRpt;
                    rptViewer.Refresh();

                    //cryRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, @"D:\ASD.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
                if (CommonData.ConnectionType == "LOCAL")
                    connectionParser = new SqlConnectionStringBuilder(objSecurity.GetDecodeString(ConfigurationManager.AppSettings["DBConLoc"].ToString()));
                else
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
