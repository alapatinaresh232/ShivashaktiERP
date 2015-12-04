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
using System.Diagnostics;
using ZkFingerDemo;
using System.Data.SqlClient;

namespace SSCRM
{
    public partial class MDIParent : Form
    {
        private Boolean terminating;
        private UserTaskDB objTask = null;
        private DataTable dt = null;
        private SQLDB objDB = null;

        
        #region Form Object Variables
        private Invoice childInvoiceForm = null;
        private PrevInvoice childPrevInvoiceForm = null;
        private InvoiceView childInvoiceViewForm = null;
        private SalesRegister childSalesRegReportForm = null;
        private SouceToDestination childSouceToDestinationForm = null;
        private GroupToDestination childGroupToDestinationForm = null;
        private BranchAdd childBranchAddForm = null;
        private LogicalBranch childLogicalBranchForm = null;
        private UserMaster childUserMasterForm = null;
        private UserTask childUserTaskForm = null;
        private InvoiceBultin childInvoiceBultinForm = null;
        private SalseORder childSalseORderForm = null;
        private DoorKnocks childDoorKnocksForm = null;
        private frmSrmonthlyInputes childSrmonthlyInputesForm = null;
        private frmOrderSheetIssue childOrderSheetIssueForm = null;
        private DocumentMonthFRM childDocumentMonthFRMForm = null;
        private ResetPasswordFRM childResetPasswordFRMForm = null;
        private frmApprovedStatus childApprovedStatusHRForm = null;
        private frmViewDetails childViewDetailsHRForm = null;
        private DeliveryChallan childDeliveryChallanForm = null;
        private StockIndentFRM childStockIndentForm = null;
        private frmBulletIns childBulletInsForm = null;        
        private frmOrderSheetReturn childSROrderSheetReturn = null;
        private ActivityServiceUpdate childSalesService = null;
        private frmEORAMasterUpdate childfrmEORAMasterUpdate = null;
        private IndentFromBranchFRM childIndentFromBranchFRM = null;
        private Search childSearchFRM = null;
        private ActivityServiceSearch childActivityServiceSearch = null;
        private SaleBuiltinReport childSaleBuiltinReport = null;
        private CheckList childCheckList = null;
        private frmChangeToBranch childfrmChangeToBranch = null;
        private FeedBack childFeedBack = null;
        private DeliveryChallanPU childDeliveryChallanPU = null;
        private FreeProducts childFreeProducts = null;
        private ProductInterval childProductInterval = null;
        private CustomerMaster childCustomerMaster = null;
        private ReportViewer childReportViewer = null;
        private InvoiceTemplateProducts childInvoiceTemplateProducts = null;
        private StockPointGRN childStockPointGRN = null;
        private FinishedGoods childFinishedGoods = null;
        private OutletAdd childOutletAdd = null;
        private BranchAboveDestination childBranchAboveDestination = null;
        private CampMasAdd childCampMasAdd = null;
        private UserBranchMaster chldUserBranchMaster = null;
        private CompBranchMonthForReport childCompBranchMonthForReport = null;
        private ReportFDateTDate chldReportFDateTDate = null;        
        private StationaryIndent chldStationaryIndent = null;
        private VehicleLoanForm childVehicleLoanForm = null;
        private DummyAgentApplication childDummyAgentApplication = null;
        private VillageMaster childVillageMaster = null;
        private VehicleLoanRecovery childVehicleLoanRecovery = null;
        private PetrolAllowanceApproval childPetrolAllowanceApproval = null;
        private frmDoorknocks chldfrmDoorknocks = null;
        private CompBranchMonthForReport chldCompBranchMonthForReport = null;
        private ReportGLSelection chldReportGLSelection = null;
        private VehicleAudit childVehicleAudit = null;
        private PetrolAllowReport childPetrolAllowReport = null;
        private VehicleIncentiveApproval childVehicleIncentiveApproval = null;
        private OpeningStock childOpeningStock = null;
        private StockTransfer childStockTransfer = null;
        private frmEditingInfo childfrmEditingInfo = null;
        private AgentApprovalLetters childAgentApprovalLetters = null;
        private StationaryIndentList childStationaryIndentList = null;
        private StationaryItemsMaster childStationaryItemsMaster = null;
        private StationaryBrochureList childStationaryBrochureList = null;
        private CamptoSP childCamptoSP = null;
        private EmpFromToSelection childEmpFromToSelection = null;
        private EmployeeInfo childEmployeeInfo = null;
        private NotMappedList childNotMappedList = null;
        private StationaryGRN childStationaryGRN = null;
        private AdvancedRefund childAdvancedRefund = null;
        private SelectECodes childSelectECodes = null;
        private CompanyMaster childCompanyMaster = null;
        private MultipleEntry childMultipleEntry = null;
        private UpdateAddress childUpdateAddress = null;
        private SPRefill childSPRefill = null;
        private supplier childSupplierMaster = null;
        private StationaryBrGRN childStationaryBrGRN = null;
        private PrevEmployee childPrevEmployee = null;
        private BranchStationaryIssue childBranchStationaryIssue = null;
        private StockPointDetails childStockPointDetails = null;
        private FcoReturns childFcoReturns = null;
        private InspectionDetails childInspectionDetails = null;
        private StockDumpdetails childStockDumpdetails = null;
        private SalesInvoiceEdit childSalesInvoiceEdit = null;
        private InternalDamage childInternalDamage = null;
        private StationaryOpeningStock childStationaryOpeningStock = null;
        private SPPackingMtrlGRN childSPPackingMtrlGRN = null;
        private SPRentalAgriments childSPRentalAgriments = null;
        private SPPackingMtrlDC childSPPackingMtrlDC = null;
        private PromotionBoard childPromotionBoard = null;        
        private EmpPresentSitUpdate childEmpPresentSitUpdate = null;
        private PromotionBoardApproval childPromotionBoardApproval = null;
        private CorrierMaster childCorrierMaster = null;
        private EmpDOjUpdate childEmpDOjUpdate = null;
        private StationaryDCByHand childStationaryDCByHand = null;
        private TransportMaster childTransportMaster = null;
        private IdCardPreparationsdetails childIdCardPreparationsdetails = null;
        private TransportVehicleMaster childTransportVehicleMaster = null;
        private SendSMS childSendSMS = null;
        private VehicleMaster childVehicleMaster = null;
        private frmBranchMasterUpdate childfrmBranchMasterUpdate = null;
        private VehiclePermitDetails childVehiclePermitDetails = null;
        private PolutionCheckDetails childPolutionCheckDetails = null;
        private GoodsReceiptNotePU childGoodsReceiptNotePU = null;
        private RecruitmentSourceMaster childRecruitmentSourceMaster = null;
        private FDateTDateBranchSelection childFDateTDateBranchSelection = null;
        private ProductMasterCompany childProductMasterCompany = null;
        private SPGRNInputBranch childSPGRNInputBranch = null;
        private AboveGrpSourceToDestMapping childAboveGrpSourceToDestMapping = null;
        private AboveBranchLevelMapping childAboveBranchLevelMapping = null;
        private ProcessOverAllSalesBulletinCalc childProcessOverAllSalesBulletinCalc = null;
        private PetrolIncentiveRequest childPetrolIncentiveRequest = null;
        private ProductPriceCircular childProductPriceCircular = null;
        private LeaveEntryForm childLeaveEntryForm = null;
        private OnDutySlip childOnDutySlip = null;
        private CompensatoryOFF childCompensatoryOFF = null;
        private PunchMissing childPunchMissing = null;
        private EmployeeLeavesCredit childEmployeeLeavesCredit = null;
        private ProductPricePrint childProductPricePrint = null;
        private DesignationMaster childDesignationMaster = null;
        private HolidaysMaster childHolidaysMaster = null;
        private EmployeeContactDetails childEmployeeContactDetails = null;
        private frmProductPriceCircular childfrmProductPriceCircular = null;
        private MisconductCategoryMaster childMisconductCategoryMaster = null;
        private TaskMaster childTaskMaster = null;
        private HRISTOERP childHRISTOERP = null;
        private CombiProductsList childCombiProductsList = null;
        private ProductLicenceDetails childProductLicenceDetails = null;
        private ProductLicence childProductLicence = null;
        private SalesSummaryBulletin childSalesSummaryBulletin = null;
        private ReportGLSelection childReportGLSelection = null;
        private ServiceMappingFromSourceToDestination childServiceMappingFromSourceToDestination = null;
        private CustomersListByBranchForSMS childCustomersListByBranchForSMS = null;
        private ServiceLevelGroupToDestination childServiceLevelGroupToDestination = null;
        private FarmerMeetingForm childFarmerMeetingForm = null;
        private frmWrongCommitmentOrFinancialFrauds childfrmWrongCommitmentOrFinancialFrauds = null;
        private EyeCampPatientDetails childEyeCampPatientDetails = null;
        private ReportFDateTDate childReportFDateTDate = null;
        private MisconductForm childMisconductForm = null;
        private frmDemoPlots childfrmDemoPlots = null;
        private EyeSurgeryPatientDetails childEyeSurgeryPatientDetails = null;
        private EyeSurgeryList childEyeSurgeryList = null;
        private FixedAssetMaster childFixedAssetMaster = null;
        private FixedAssetsDetails childFixedAssetsDetails = null;
        private EmployeeLOPDetails childEmployeeLOPDetails = null;
        private MemberEnrollment childMemberEnrollment = null;
        private ServiceReports childServiceReports = null;
        private SalesReceipt childSalesReceipt = null;
        private TripSheetIssue childTripSheetIssue = null;
        private ServiceActivities childServiceActivities = null;
        private frmSchoolVisits childfrmSchoolVisits = null;
        private frmProductPromotion childfrmProductPromotion = null;
        private ItInventory childItInventory = null;
        private FixedAssetsServiceDetails childFixedAssetsServiceDetails = null;
        private PromotionBoardLetterPrinting childPromotionBoardLetterPrinting = null;
        private TrainingTopicsMaster childTrainingTopicsMaster = null;
        private HRAttendence childHRAttendence = null;
        private LeagalCaseDetails childLegalCaseDetails = null;
        private ComplainantMaster childComplainantMaster = null;
        private LawyerMaster childLawyerMaster = null;
        private CaseHearings childCaseHearings = null;
        private CropMaster childCropMaster = null;
        private ServiceReportByTrnNo childServiceReportByTrnNo = null;
        private EmployeeLoanForm childEmployeeLoanForm = null;
        private LoanTypeMaster childLoanTypeMaster = null;
        private ServicesDailyActivitiesPrint childServicesDailyActivitiesPrint = null;
        private CollectedAttendence childCollectedAttendence = null;
        private SalaryStructureMaster childSalaryStructureMaster = null;
        private ServiceAboveBranchLevelMapping childServiceAboveBranchLevelMapping = null;
        private SystemConfigaration childSystemConfigaration = null;
        private FixedAssetsPrint childFixedAssetsPrint = null;
        private DailyAttendence childDailyAttendence = null;
        private CROCustomerSearch childCROCustomerSearch = null;
        private MobileNoMaster childMobileNoMaster = null;
        private MobileNoMonthlyBills childMobileNoMonthlyBills = null;
        private TrainingPlanningDetails childTrainingPlanningDetails = null;
        private TrainingProgramDetails childTrainingProgramDetails = null;
        private SalesDataMonthClosing childSalesDataMonthClosing = null;
        private MobileNoBillDtails childMobileNoBillDtails = null;
        private frmEmployeeLoanRecovery childfrmEmployeeLoanRecovery = null;
        private frmCombiSplitting childfrmCombiSplitting = null;
        private ServicesTourExpenses childServicesTourExpenses = null;
        private EmployeeTDSForm childEmployeeTDSForm = null;
        private MonthlyAttendenceForEmployes childMonthlyAttendenceForEmployes = null;
        private repCompBranchAssetEmpSelection childrepCompBranchAssetEmpSelection = null;
        private PayRollReports childPayRollReports = null;
        private ESIMaster childESIMaster = null;
        private PfMaster childPfMaster = null;
        private BankMaster childBankMaster = null;
        private AttendenceProcess childAttendenceProcess = null;
        private PayRollProcess childPayRollProcess = null;
        private RollBackProcess childRollBackProcess = null;
        private MonthlyAttendanceForOtherBranches childMonthlyAttendanceForOtherBranches = null;
        private StopSalaryPayment childStopSalaryPayment = null;
        private PayRollBranch childPayRollBranch = null;
        private EmpTicketRestaurantForm childEmpTicketRestaurantForm = null;
        private EmployeeWiseLoanRecovery childEmployeeWiseLoanRecovery = null;
        private PaymentMode chldPaymentMode = null;
        private PUDeliveryChallanReceipt childPUDeliveryChallanReceipt = null;
        private StockPointDCR childStockPointDCR = null;
        private SalesPromotionEligibulityMaster childSalesPromotionEligibulityMaster = null;
        private ActualProgramsForFeedBack childActualProgramsForFeedBack = null;
        private ImageBrowser childImageBrowser = null;
        private PFUANMaster childPFUANMaster = null;
        private NewSystemIssue childNewSystemIssue = null;
        private AuditMisconductBranch childAuditMisconductBranch = null;
        private NewAssetIssue childNewAssetIssue = null;
        private AboveBranchLevelUserBranches childAboveBranchLevelUserBranches = null;
        private repCompBranchTypeSelection childrepCompBranchTypeSelection = null;
        private UpdateShiftMaster childUpdateShiftMaster = null;
        private AuditDRPlanning childAuditDRPlanning = null;
        private AuditHierarchyMapping childAuditHierarchyMapping = null;
        private AdharMaster childAdharMaster = null;
        private ShortageStockDetails childShortageStockDetails = null;
        private UploadBiometricsData_BR childUploadBiometricsData_BR = null;
        private MonthlyAttdProcess childMonthlyAttdProcess = null;
        private TrainingReports childTrainingReports = null;
        private ServiceHierarchyMapping childServiceHierarchyMapping = null;
        private Damaged_Stock_Received_Details childDamaged_Stock_Received_Details = null;
        private TripSheet childTripSheet = null;
        private EmployeeList childEmployeeList = null;
        private RepDateCompBranchSelection childRepDateCompBranchSelection = null;
        private frmEmployeeInfo childfrmEmployeeInfo = null;
        private LoanDeductions childLoanDeductions = null;
        private MajorCostCenter_Details childMajorCostCenter_Details = null;
        private PaymentVouchers childPaymentVoucher = null;
        private BranchReportFilter childBranchReportFilter = null;
        private CompAndMonthForReport childCompAndMonthForReport = null;
        private EmployeeFingerEnroll childEmployeeFingerEnroll = null;
        private ReportFilters childReportFilters = null;
        private ChartOfAccounts childChartOfAccounts = null;
        private frmSpecialApprovals childfrmSpecialApprovals = null;
        private SalesReportingDeviations childSalesReportingDeviations = null;
        private ServiceActivityReportFilters childServiceActivityReportFilters = null;
        private ShortageStationaryDetails childShortageStationaryDetails = null;
        private ReceiptVoucher childReceiptVoucher = null;
        private JournalVoucher childJournalVoucher = null;
        private AfterTrainingEmpPerf childAfterTrainingEmpPerf = null;
        private frmRPFilterFinFromNoToNo childfrmRPFilterFinFromNoToNo = null;
        private ServiceActivitiesReport childServiceActivitiesReport = null;
        private EmployeeDARWithTourBills childEmployeeDARWithTourBills = null;
        private frmInvoiceVerification childfrmInvoiceVerification = null;
        private TransportCostSummary childTransportCostSummary = null;
        private SalesContraForReconsialtion childSalesContraForReconsialtion = null;
        private frmBranchAuditReportsFilters childfrmBranchAuditReportsFilters = null;
        private frmRecruitmentAnalysis childfrmRecruitmentAnalysis = null;
        private frmSelectionForLowPerfs childfrmSelectionForLowPerfs = null;
        private Calibration_Certificate childCalibration_Certificate = null;
        private StationaryCategory childStationaryCategory = null;
        private ZonalHeadMaster childZonalHeadMaster = null;
        private PetrolRateMaster childPetrolRateMaster = null;
        private frmTMAndAbovePMD childfrmTMAndAbovePMD = null;
        private BranchAttendence childBranchAttendence = null;
        private Employee_History childEmployee_History = null;
        private frmEmpAwardsEntry childfrmEmpAwardsEntry = null;
        private SPInvoice childSPInvoice = null;
        private GCGLAdvances childGCGLAdvances = null;
        private StationaryBulkIndent childStationaryBulkIndent = null;
        private Physicalstkcount childPhysicalstkcount = null;
        private frmHolidaysMas childfrmHolidaysMas = null;
        private EmpWorkingStatusRollback childEmpWorkingStatusRollback = null;
        private VehicleLegalNotice childVehicleLegalNotice = null;
        private frmEmpResignations childfrmEmpResignations = null;
        private KRAMaster childKRAMaster = null;
        private OpeningBranchBalances childOpeningBranchBalances = null;
        private StockRepFilter childStockRepFilter = null;
        private SRAdoption childSRAdoption = null;
        private StockDumping childStockDumping = null;
        

        #endregion Form Object Variaables


        public MDIParent()
        {
            InitializeComponent();
        }

        public void MDIParent_Load(object sender, EventArgs e)
        {
            statusStrip.Visible = true;
            MenuEnableDisable();
            try
            {
                statusStrip.Items[0].Text = "USER: " + CommonData.LogUserId;
                statusStrip.Items[1].Text = "Financial Year : " + CommonData.FinancialYear.ToString();
                statusStrip.Items[2].Text = "Document Month : " + CommonData.DocMonth.ToString();
                statusStrip.Items["dmFromDate"].Text = " From: " + CommonData.DocFDate.ToString();
                statusStrip.Items["dmToDate"].Text = " To: " + CommonData.DocTDate.ToString();

                if (CommonData.LogUserId.ToUpper() != "ADMIN" || CommonData.LogUserRole != "MANAGEMENT")
                {

                    if (CommonData.LogUserId.ToUpper() != "ADMIN")
                    {
                        //msbTrans_CRM.Visible = false;
                        //msbTrans_HR.Visible = false;
                        //msbTrans_StockPoints.Visible = false;
                        //msbTrans_ProductionUnits.Visible = false;
                        //msbReport_CRM.Visible = false;
                        //msbReport_HR.Visible = false;
                        //msbReport_StockPoint.Visible = false;
                        //mbAdmin.Visible = false;
                        //msb_Transaction_Transport.Visible = false;
                        //mbUser.Visible = true;

                        //if (CommonData.BranchType == "HO")
                        //{
                        //    msbTrans_CRM.Visible = true;
                        //    msbTrans_HR.Visible = true;
                        //    msbTrans_StockPoints.Visible = true;
                        //    msbTrans_ProductionUnits.Visible = true;
                        //    msbReport_CRM.Visible = true;
                        //    msbReport_HR.Visible = true;
                        //    msbReport_StockPoint.Visible = true;
                        //    msb_Transaction_Transport.Visible = true;
                        //}

                        //if (CommonData.BranchType == "BR")
                        //{
                        //    msbTrans_CRM.Visible = true;
                        //    msbTrans_HR.Visible = true;
                        //    msbReport_CRM.Visible = true;
                        //    msbReport_HR.Visible = true;
                        //}

                        //if (CommonData.BranchType == "SP")
                        //{
                        //    msbTrans_StockPoints.Visible = true;
                        //    msbReport_StockPoint.Visible = true;
                        //}

                        //if (CommonData.BranchType == "PU")
                        //{
                        //    msbTrans_ProductionUnits.Visible = true;
                        //}
                        //if (CommonData.BranchType == "TR")
                        //{
                        //    msb_Transaction_Transport.Visible = true;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }
        
        
        private void msbTrans_CRM_Market_DoorKnock_Click(object sender, EventArgs e)
        {
            if (childDoorKnocksForm == null || childDoorKnocksForm.Text == "")
            {
                childDoorKnocksForm = new DoorKnocks();
                childDoorKnocksForm.MdiParent = this;
                childDoorKnocksForm.Show();
            }
        }

        private void msbTrans_CRM_Market_SaleOrder_Click(object sender, EventArgs e)
        {
            if (childSalseORderForm == null || childSalseORderForm.Text == "")
            {
                childSalseORderForm = new SalseORder();
                childSalseORderForm.MdiParent = this;
                childSalseORderForm.Show();
            }
        }
        
        private void msbTrans_CRM_Sale_HierarMap_GroupMap_Click(object sender, EventArgs e)
        {
            if (childSouceToDestinationForm == null || childSouceToDestinationForm.Text == "")
            {
                childSouceToDestinationForm = new SouceToDestination();
                childSouceToDestinationForm.MdiParent = this;
                childSouceToDestinationForm.Show();
            }
        }

        private void msbTrans_CRM_Sale_HierarMap_GroupAbove_Click(object sender, EventArgs e)
        {
            if (childGroupToDestinationForm == null || childGroupToDestinationForm.Text == "")
            {
                childGroupToDestinationForm = new GroupToDestination();
                childGroupToDestinationForm.MdiParent = this;
                childGroupToDestinationForm.Show();
            }
        }

        private void msbTrans_CRM_Sale_Reconcil_OrdSheetIssue_Click(object sender, EventArgs e)
        {
            if (childOrderSheetIssueForm == null || childOrderSheetIssueForm.Text == "")
            {
                childOrderSheetIssueForm = new frmOrderSheetIssue();
                childOrderSheetIssueForm.MdiParent = this;
                childOrderSheetIssueForm.Show();
            }
        }

        private void msbTrans_CRM_Sale_Reconcil_OrdSheetReturn_Click(object sender, EventArgs e)
        {
            if (childSROrderSheetReturn == null || childSROrderSheetReturn.Text == "")
            {
                childSROrderSheetReturn = new frmOrderSheetReturn();
                childSROrderSheetReturn.MdiParent = this;
                childSROrderSheetReturn.Show();
            }
        }

        private void msbTrans_CRM_Sale_Reconcil_MonthInputs_Click(object sender, EventArgs e)
        {
            if (childSrmonthlyInputesForm == null || childSrmonthlyInputesForm.Text == "")
            {
                childSrmonthlyInputesForm = new frmSrmonthlyInputes();
                childSrmonthlyInputesForm.MdiParent = this;
                childSrmonthlyInputesForm.Show();
            }
        }

        private void msbTrans_CRM_Sale_Builtin_Click(object sender, EventArgs e)
        {
            if (childInvoiceBultinForm == null || childInvoiceBultinForm.Text == "")
            {
                childInvoiceBultinForm = new InvoiceBultin();
                childInvoiceBultinForm.MdiParent = this;
                childInvoiceBultinForm.Show();
            }
        }

        private void msbTrans_CRM_Sale_Invoices_Click(object sender, EventArgs e)
        {
            if (childInvoiceForm == null || childInvoiceForm.Text == "")
            {
                childInvoiceForm = new Invoice();
                childInvoiceForm.MdiParent = this;
                childInvoiceForm.Show();
            }
        }

        private void msbTrans_CRM_Sale_PrevInvoice_Click(object sender, EventArgs e)
        {
            if (childPrevInvoiceForm == null || childPrevInvoiceForm.Text == "")
            {
                childPrevInvoiceForm = new PrevInvoice();
                childPrevInvoiceForm.MdiParent = this;
                childPrevInvoiceForm.Show();
            }
        }

        private void msbTrans_CRM_Service_AOvsSales_Click(object sender, EventArgs e)
        {

        }

        private void msbTrans_CRM_Service_FormVisit_Click(object sender, EventArgs e)
        {

        }

        private void msbTrans_CRM_Service_CountActves_Click(object sender, EventArgs e)
        {

        }

        private void msbTrans_CRM_Service_Replace_Click(object sender, EventArgs e)
        {

        }

        private void msbMaster_General_Customer_Click(object sender, EventArgs e)
        {
            if (childCustomerMaster == null || childCustomerMaster.Text == "")
            {
                childCustomerMaster = new CustomerMaster();
                childCustomerMaster.MdiParent = this;
                childCustomerMaster.Show();
            }
        }

        private void msbMaster_General_PhysicalBranch_Click(object sender, EventArgs e)
        {
            if (childBranchAddForm == null || childBranchAddForm.Text == "")
            {
                childBranchAddForm = new BranchAdd();
                childBranchAddForm.MdiParent = this;
                childBranchAddForm.Show();
            }
        }

        private void msbMaster_General_LogicalBranch_Click(object sender, EventArgs e)
        {
            if (childLogicalBranchForm == null || childLogicalBranchForm.Text == "")
            {
                childLogicalBranchForm = new LogicalBranch();
                childLogicalBranchForm.MdiParent = this;
                childLogicalBranchForm.Show();
            }
        }

        private void msbMaster_General_Product_Click(object sender, EventArgs e)
        {

        }

        private void mbViews_Invoice_Click(object sender, EventArgs e)
        {
            if (childInvoiceViewForm == null || childInvoiceViewForm.Text == "")
            {
                childInvoiceViewForm = new InvoiceView();
                childInvoiceViewForm.MdiParent = this;
                childInvoiceViewForm.Show();
            }
        }

        private void msbReport_SalesRegister_Click(object sender, EventArgs e)
        {

        }

        private void msbAdmin_User_CreateUser_Click(object sender, EventArgs e)
        {
            if (childUserMasterForm == null || childUserMasterForm.Text == "")
            {
                childUserMasterForm = new UserMaster();
                childUserMasterForm.MdiParent = this;
                childUserMasterForm.Show();
            }
        }

        private void msbAdmin_User_Task_Click(object sender, EventArgs e)
        {
            if (childUserTaskForm == null || childUserTaskForm.Text == "")
            {
                childUserTaskForm = new UserTask();
                childUserTaskForm.MdiParent = this;
                childUserTaskForm.Show();
            }
        }

        private void msbAdmin_User_ResetPWD_Click(object sender, EventArgs e)
        {

            if (childResetPasswordFRMForm == null || childResetPasswordFRMForm.Text == "")
            {
                childResetPasswordFRMForm = new ResetPasswordFRM();
                childResetPasswordFRMForm.MdiParent = this;
                childResetPasswordFRMForm.Show();
            }
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to close ?",
                                       "CRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                InsertUserHistory("LOGOUT");
                Application.Exit();
                
            }

        }
        private void InsertUserHistory(string STATUS)
        {
            try
            {
                objDB = new SQLDB();
                string sqlInsert = "INSERT INTO USER_HISTORY (UH_USER_ID, UH_DATE_TIME, UH_STATUS)" +
                    " VALUES('" + CommonData.LogUserId + "',GETDATE(),'" + STATUS + "')";
                int intRec = objDB.ExecuteSaveData(sqlInsert);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "SSCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                objDB = null;
            }
        }

        private void MenuEnableDisable()
        {
            objTask = new UserTaskDB();
            try
            {
                if (CommonData.LogUserId.ToUpper() != "ADMIN")
                {
                    foreach (ToolStripMenuItem mainMenu in menuStrip.Items)  // Disable all menus
                    {
                        mainMenu.Visible = false;
                        for (int intMB = 0; intMB < mainMenu.DropDownItems.Count; intMB++)
                        {
                            mainMenu.DropDownItems[intMB].Visible = false;
                            ToolStripMenuItem subMenu = (ToolStripMenuItem)mainMenu.DropDownItems[intMB];
                            if (subMenu.HasDropDownItems)
                            {
                                for (int intMSB = 0; intMSB < subMenu.DropDownItems.Count; intMSB++)
                                {
                                    subMenu.DropDownItems[intMSB].Visible = false;
                                    ToolStripMenuItem sub2Menu = (ToolStripMenuItem)subMenu.DropDownItems[intMSB];
                                    if (sub2Menu.HasDropDownItems)
                                    {
                                        for (int intMS2B = 0; intMS2B < sub2Menu.DropDownItems.Count; intMS2B++)
                                        {
                                            sub2Menu.DropDownItems[intMS2B].Visible = false;
                                            ToolStripMenuItem sub3Menu = (ToolStripMenuItem)sub2Menu.DropDownItems[intMS2B];
                                            if (sub3Menu.HasDropDownItems)
                                            {
                                                for (int intMS3B = 0; intMS3B < sub3Menu.DropDownItems.Count; intMS3B++)
                                                    sub3Menu.DropDownItems[intMS3B].Visible = false;

                                            }
                                        }

                                    }
                                }

                            }
                        }
                    }

                    dt = objTask.UserTasks_Get(CommonData.LogUserId).Tables[0];

                    if (dt.Rows.Count > 0)  // Enable as DB
                    {
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            string strMenu = dataRow["MenuName"].ToString();

                            foreach (ToolStripMenuItem mainMenu in menuStrip.Items)
                            {

                                //if (strMenu.IndexOf("mb") > -1)
                                //{
                                    if (strMenu == mainMenu.Name && dataRow["STATUS"].ToString() == "E")
                                    {
                                        mainMenu.Visible = true;
                                        //break;                                        
                                    }
                                    for (int iMB = 0; iMB < mainMenu.DropDownItems.Count; iMB++)
                                    {
                                        if (mainMenu.DropDownItems[iMB].Name.ToString() == strMenu && dataRow["STATUS"].ToString() == "E")
                                        {
                                            mainMenu.DropDownItems[iMB].Visible = true;
                                            //this.break;                                            
                                        }
                                        ToolStripMenuItem subMenu = (ToolStripMenuItem)mainMenu.DropDownItems[iMB];
                                        //if (subMenu.HasDropDownItems)
                                        //{
                                            for (int jMSB = 0; jMSB < subMenu.DropDownItems.Count; jMSB++)
                                            {
                                                if (subMenu.DropDownItems[jMSB].Name.ToString() == strMenu && dataRow["STATUS"].ToString() == "E")
                                                {
                                                    subMenu.DropDownItems[jMSB].Visible = true;
                                                    //break;                                                        
                                                }
                                                ToolStripMenuItem sub2Menu = (ToolStripMenuItem)subMenu.DropDownItems[jMSB];
                                                //if (sub2Menu.HasDropDownItems)
                                                //{
                                                    for (int jMS2B = 0; jMS2B < sub2Menu.DropDownItems.Count; jMS2B++)
                                                    {
                                                        if (sub2Menu.DropDownItems[jMS2B].Name.ToString() == strMenu && dataRow["STATUS"].ToString() == "E")
                                                        {
                                                            sub2Menu.DropDownItems[jMS2B].Visible = true;
                                                            //break;                                                            
                                                        }
                                                        ToolStripMenuItem sub3Menu = (ToolStripMenuItem)sub2Menu.DropDownItems[jMS2B];
                                                        //if (sub3Menu.HasDropDownItems)
                                                        //{
                                                            for (int jMS3B = 0; jMS3B < sub3Menu.DropDownItems.Count; jMS3B++)
                                                            {
                                                                if (sub3Menu.DropDownItems[jMS3B].Name.ToString() == strMenu && dataRow["STATUS"].ToString() == "E")
                                                                {
                                                                    sub3Menu.DropDownItems[jMS3B].Visible = true;
                                                                    //break;                                                                            
                                                                }
                                                                ToolStripMenuItem sub4Menu = (ToolStripMenuItem)sub3Menu.DropDownItems[jMS3B];
                                                                //if (sub4Menu.HasDropDownItems)
                                                                //{
                                                                    for (int jMS4B = 0; jMS4B < sub4Menu.DropDownItems.Count; jMS4B++)
                                                                    {
                                                                        if (sub4Menu.DropDownItems[jMS4B].Name.ToString() == strMenu && dataRow["STATUS"].ToString() == "E")
                                                                        {
                                                                            sub4Menu.DropDownItems[jMS4B].Visible = true;
                                                                            //break;
                                                                        }

                                                                    }
                                                                //}

                                                            }
                                                        //}

                                                    }
                                                //}
                                            }
                                        //}

                                    }

                                //}


                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                mbExit.Visible = true;
                throw new Exception(ex.Message, ex);
            }

            mbExit.Visible = true;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!terminating)
            {
                terminating = true;
                Close();
            }

            base.OnClosing(e);
        }

        private void MDIParent_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure do you want to close ?",
                                       "CRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                Application.Exit();
            else
                e.Cancel = true;

        }
       
        private void msbTrans_HR_ApplEntry_Employee_Click(object sender, EventArgs e)
        {
            //This is used for Employee childSearchHRForm

            //Search frmChld = new Search("E");
            //frmChld.MdiParent = this;
            //frmChld.Show();

            if (childSearchFRM == null || childSearchFRM.Text == "")
            {
                childSearchFRM = new Search("E");
                childSearchFRM.MdiParent = this;
                childSearchFRM.Show();
            }
        }

        private void msbTrans_HR_ApplEntry_Agent_Click(object sender, EventArgs e)
        {
            ////This is used for Agent
            //Search frmChld = new Search("A");
            //frmChld.MdiParent = this;
            //frmChld.Show();
            if (childSearchFRM == null || childSearchFRM.Text == "")
            {
                childSearchFRM = new Search("A");
                childSearchFRM.MdiParent = this;
                childSearchFRM.Show();
            }
        }

        private void msbTrans_HR_ApprProcess(object sender, EventArgs e)
        {

            if (childApprovedStatusHRForm == null || childApprovedStatusHRForm.Text == "")
            {
                childApprovedStatusHRForm = new frmApprovedStatus();
                childApprovedStatusHRForm.MdiParent = this;
                childApprovedStatusHRForm.Show();
            }

        }

        private void msbTrans_HR_ViewDetails_Click(object sender, EventArgs e)
        {
            if (childViewDetailsHRForm == null || childViewDetailsHRForm.Text == "")
            {
                childViewDetailsHRForm = new frmViewDetails();
                childViewDetailsHRForm.MdiParent = this;
                childViewDetailsHRForm.Show();
            }
        }

        private void msbTrans_ProductionUnits_Click(object sender, EventArgs e)
        {

        }

        private void msbTrans_HR_ApplEntry_Click(object sender, EventArgs e)
        {

        }

        private void mbTransactions_Click(object sender, EventArgs e)
        {

        }

        private void msbTrans_HR_Click(object sender, EventArgs e)
        {

        }

        private void mbReports_Click(object sender, EventArgs e)
        {

        }

        private void msbTrans_CRM_Sale_StockIndent_Click(object sender, EventArgs e)
        {
            if (childStockIndentForm == null || childStockIndentForm.Text == "")
            {
                childStockIndentForm = new StockIndentFRM();
                childStockIndentForm.MdiParent = this;
                childStockIndentForm.Show();
            }
            
        }

        private void msbReport_CRM_Sale_Register_Click(object sender, EventArgs e)
        {
            if (childSalesRegReportForm == null || childSalesRegReportForm.Text == "")
            {
                childSalesRegReportForm = new SalesRegister();
                childSalesRegReportForm.MdiParent = this;
                childSalesRegReportForm.Show();
            }
        }

        private void msbReport_CRM_Sale_BulletIns_Click(object sender, EventArgs e)
        {

            if (childBulletInsForm == null || childBulletInsForm.Text == "")
            {
                childBulletInsForm = new frmBulletIns();
                childBulletInsForm.MdiParent = this;
                childBulletInsForm.Show();
            }
            
        }

        private void msbReport_MIS_CheckList_Click(object sender, EventArgs e)
        {
            if (childCheckList == null || childCheckList.Text == "")
            {
                childCheckList = new CheckList();
                childCheckList.MdiParent = this;
                childCheckList.Show();
            }
        }

        private void msbTrans_StockPoint_DeliverChalanStockTransfer_Click(object sender, EventArgs e)
        {
            if (childDeliveryChallanForm == null || childDeliveryChallanForm.Text == "")
            {
                childDeliveryChallanForm = new DeliveryChallan("DC");
                childDeliveryChallanForm.MdiParent = this;
                childDeliveryChallanForm.Show();
            }
        }

        private void msbProcess_DocMonth_Click(object sender, EventArgs e)
        {
            if (childDocumentMonthFRMForm == null || childDocumentMonthFRMForm.Text == "")
            {
                childDocumentMonthFRMForm = new DocumentMonthFRM();
                childDocumentMonthFRMForm.MdiParent = this;
                childDocumentMonthFRMForm.Show();
            }
        }

        private void msbTrans_CRM_Service_Click(object sender, EventArgs e)
        {
            
        }

        private void msbMaster_EoraUpdate_Click(object sender, EventArgs e)
        {
            
        }

        private void msbTrans_StockPoint_IndentFormBranch_Click(object sender, EventArgs e)
        {
            if (childIndentFromBranchFRM == null || childIndentFromBranchFRM.Text == "")
            {
                childIndentFromBranchFRM = new IndentFromBranchFRM();
                childIndentFromBranchFRM.MdiParent = this;
                childIndentFromBranchFRM.Show();
            }
        }

        private void msbReport_CRM_Service_List_Click(object sender, EventArgs e)
        {
            if (childActivityServiceSearch == null || childActivityServiceSearch.Text == "")
            {
                childActivityServiceSearch = new ActivityServiceSearch();
                childActivityServiceSearch.MdiParent = this;
                childActivityServiceSearch.Show();
            }

        }

        private void mspReport_CRM_Doc_GroupwiseBulletin_Click(object sender, EventArgs e)
        {
            if (childSaleBuiltinReport == null || childSaleBuiltinReport.Text == "")
            {
                childSaleBuiltinReport = new SaleBuiltinReport();
                childSaleBuiltinReport.MdiParent = this;
                childSaleBuiltinReport.Show();
            }
        }

        private void msbReport_CRM_Market_DoorKnock_Click(object sender, EventArgs e)
        {
            frmDoorknocks chldfrmDoorknocks = new frmDoorknocks(1);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReport_CRM_Market_SaleOrder_Click(object sender, EventArgs e)
        {
            frmDoorknocks chldfrmDoorknocks = new frmDoorknocks(2);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void Trans_CRM_Sales_Hierarch_EoraUpdate_Click(object sender, EventArgs e)
        {
            if (childfrmEORAMasterUpdate == null || childfrmEORAMasterUpdate.Text == "")
            {
                childfrmEORAMasterUpdate = new frmEORAMasterUpdate();
                childfrmEORAMasterUpdate.MdiParent = this;
                childfrmEORAMasterUpdate.Show();
            }
        }

        private void mbUser_PwdReset_Click(object sender, EventArgs e)
        {

            if (childResetPasswordFRMForm == null || childResetPasswordFRMForm.Text == "")
            {
                childResetPasswordFRMForm = new ResetPasswordFRM();
                childResetPasswordFRMForm.MdiParent = this;
                childResetPasswordFRMForm.Show();
            }
        }

        private void mbUser_ChangeBranch_Click(object sender, EventArgs e)
        {
            if (childfrmChangeToBranch == null || childfrmChangeToBranch.Text == "")
            {
                childfrmChangeToBranch = new frmChangeToBranch();
                childfrmChangeToBranch.MdiParent = this;
                childfrmChangeToBranch.Show();
            }
        }
        private void msbUser_FeedBack_Click(object sender, EventArgs e)
        {
            if (childFeedBack == null || childFeedBack.Text == "")
            {
                childFeedBack = new FeedBack();
                childFeedBack.MdiParent = this;
                childFeedBack.Show();
            }
        }

        private void msbTrans_ProdUnit_DeliveryChallana_Click(object sender, EventArgs e)
        {

            if (childDeliveryChallanPU == null || childDeliveryChallanPU.Text == "")
            {
                childDeliveryChallanPU = new DeliveryChallanPU("DC");
                childDeliveryChallanPU.MdiParent = this;
                childDeliveryChallanPU.Show();
            }
        }

        private void msbMasters_Products_FreeProductsMaster_Click(object sender, EventArgs e)
        {
            if (childFreeProducts == null || childFreeProducts.Text == "")
            {
                childFreeProducts = new FreeProducts();
                childFreeProducts.MdiParent = this;
                childFreeProducts.Show();
            }
        }

        private void msbMasters_Products_PriceRangeMaster_Click(object sender, EventArgs e)
        {
            if (childProductInterval == null || childProductInterval.Text == "")
            {
                childProductInterval = new ProductInterval();
                childProductInterval.MdiParent = this;
                childProductInterval.Show();
            }
        }
        private void msbReports_CRM_Sales_OrganisationChart_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(36);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();

        }

        private void msbMasters_Products_InvoiceTemplateProducts_Click(object sender, EventArgs e)
        {
            if (childInvoiceTemplateProducts == null || childInvoiceTemplateProducts.Text == "")
            {
                childInvoiceTemplateProducts = new InvoiceTemplateProducts();
                childInvoiceTemplateProducts.MdiParent = this;
                childInvoiceTemplateProducts.Show();
            }
        }

        private void msbTrans_StockPoint_GoodsRecieptNote_Click(object sender, EventArgs e)
        {
            if (childStockPointGRN == null || childStockPointGRN.Text == "")
            {
                childStockPointGRN = new StockPointGRN();
                childStockPointGRN.MdiParent = this;
                childStockPointGRN.Show();
            }
            
        }

        private void msbTrans_ProdUnit_Packing_Click(object sender, EventArgs e)
        {
            if (childFinishedGoods == null || childFinishedGoods.Text == "")
            {
                childFinishedGoods = new  FinishedGoods();
                childFinishedGoods.MdiParent = this;
                childFinishedGoods.Show();
            }
        }

        private void msbMaster_General_OutletAdd_Click(object sender, EventArgs e)
        {
            if (childOutletAdd == null || childOutletAdd.Text == "")
            {
                childOutletAdd = new  OutletAdd();
                childOutletAdd.MdiParent = this;
                childOutletAdd.Show();
            }
        }

        private void msbTrans_CRM_Sale_HierarMap_GroupBRAbove_Click(object sender, EventArgs e)
        {
            //if (childBranchAboveDestination == null || childBranchAboveDestination.Text == "")
            //{
            //    childBranchAboveDestination = new BranchAboveDestination();
            //    childBranchAboveDestination.MdiParent = this;
            //    childBranchAboveDestination.Show();
            //}
        }

        private void msbMaster_General_CampAdd_Click(object sender, EventArgs e)
        {
            if (childCampMasAdd == null || childCampMasAdd.Text == "")
            {
                childCampMasAdd = new CampMasAdd();
                childCampMasAdd.MdiParent = this;
                childCampMasAdd.Show();
            }

        }

        private void msbReports_CRM_Documentation_OrderReconsilation_Click(object sender, EventArgs e)
        {
            childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, CommonData.FinancialYear, "GL_ORDER_RECON_SUMMERY");
            CommonData.ViewReport = "Branch Wise OrderSheet Reconsilation";
            childReportViewer.Show();
        }

        private void msbReports_MIS_OrderSheetReconsilation_Click(object sender, EventArgs e)
        {
            childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, CommonData.LogUserId, "BRANCH_ORDER_REC_SUMMERY");
            CommonData.ViewReport = "Branch Wise OrderSheet Reconsilation";
            childReportViewer.Show();
        }

        private void msbReports_CRM_Doc_SRWiseBulletins_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(3);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_CRM_Doc_GCGL_Bulletins_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(4);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReport_CRM_Sales_AdvanceRegister_Click(object sender, EventArgs e)
        {
            //chldfrmDoorknocks = new frmDoorknocks(2);
            //chldfrmDoorknocks.MdiParent = this;
            //chldfrmDoorknocks.Show();
            childSalesRegReportForm = new SalesRegister(2);
            childSalesRegReportForm.MdiParent = this;
            childSalesRegReportForm.Show();
        }

        private void msbReport_HR_Strength_Click(object sender, EventArgs e)
        {

        }

        private void msbReports_HR_Strengths_SalesStaff_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(6);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_CRM_Sales_GCGLAdvSummery_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(5);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_MIS_BranchBulletins_Click(object sender, EventArgs e)
        {
            
        }

        private void msbAdmin_User_UserBranch_Click(object sender, EventArgs e)
        {
            chldUserBranchMaster = new UserBranchMaster();
            chldUserBranchMaster.MdiParent = this;
            chldUserBranchMaster.Show();
        }

        private void msbReports_MIS_SalesBulletinsSummery_Click(object sender, EventArgs e)
        {
            //chldCompBranchMonthForReport = new CompBranchMonthForReport();
            //chldCompBranchMonthForReport.MdiParent = this;
            //chldCompBranchMonthForReport.Show();
        }

        private void msbReports_HR_Recruitement_Click(object sender, EventArgs e)
        {
            chldReportFDateTDate = new ReportFDateTDate();
            chldReportFDateTDate.MdiParent = this;
            chldReportFDateTDate.Show();
        }

        private void msb_Reports_HR_RecruitementSummary_Click(object sender, EventArgs e)
        {
            chldReportFDateTDate = new ReportFDateTDate("RECR_SUMMARY");
            chldReportFDateTDate.MdiParent = this;
            chldReportFDateTDate.Show();
        }

        private void msbTrans_CRM_Stationary_Indent_Click(object sender, EventArgs e)
        {
            
        }

        private void msbReports_MIS_StockPoints_Checklist_Click(object sender, EventArgs e)
        {
            chldCompBranchMonthForReport = new CompBranchMonthForReport("SP_CHECKLIST");
            chldCompBranchMonthForReport.MdiParent = this;
            chldCompBranchMonthForReport.Show();
        }

        private void msbReports_HR_Recruitement_CompanyWiseSummary_Click(object sender, EventArgs e)
        {
            chldReportFDateTDate = new ReportFDateTDate("RECR_SUMMARY_BY_COMPANY");
            chldReportFDateTDate.MdiParent = this;
            chldReportFDateTDate.Show();
        }

        private void msbReports_CRM_Sales_OrgChart2_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(8);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();

        }

        private void msbReports_CRM_Docm_GCGLSaleProdSummary_Click(object sender, EventArgs e)
        {
            //chldReportGLSelection = new ReportGLSelection();
            //chldReportGLSelection.MdiParent = this;
            //chldReportGLSelection.Show();
            frmDoorknocks chldfrmDoorknocks = new frmDoorknocks(18);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbTrans_CRM_VehicleLoan_ApplForm_Click(object sender, EventArgs e)
        {
            if (childVehicleLoanForm == null || childVehicleLoanForm.Text == "")
            {
                childVehicleLoanForm = new VehicleLoanForm();
                childVehicleLoanForm.MdiParent = this;
                childVehicleLoanForm.Show();
            }
        }

        private void msbTrans_Masters_AgentAppl_Click(object sender, EventArgs e)
        {
            if (childDummyAgentApplication == null || childDummyAgentApplication.Text == "")
            {
                childDummyAgentApplication = new DummyAgentApplication();
                childDummyAgentApplication.MdiParent = this;
                childDummyAgentApplication.Show();
            }
        }

        private void msbMasters_VillageMaster_Click(object sender, EventArgs e)
        {
            if (childVillageMaster == null || childVillageMaster.Text == "")
            {
                childVillageMaster = new VillageMaster();
                //childVillageMaster.MdiParent = this;
                childVillageMaster.Show();
            }
        }

        private void msbReport_StockPoint_DC_Click(object sender, EventArgs e)
        {
           
            if (CommonData.LogUserRole == "MANAGEMENT" || CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                childRepDateCompBranchSelection = new RepDateCompBranchSelection("STOCKPOINT_DC");
                childRepDateCompBranchSelection.MdiParent = this;
                childRepDateCompBranchSelection.Show();
            }
            else
            {
                chldfrmDoorknocks = new frmDoorknocks("STOCKPOINT_DC");
                chldfrmDoorknocks.MdiParent = this;
                chldfrmDoorknocks.Show();
            }
        }

        private void msbReports_StockPoint_DCST_Click(object sender, EventArgs e)
        {
            
        }

        private void msbTrans_CRM_VehicleLoan_Recovery_Click(object sender, EventArgs e)
        {
            if (childVehicleLoanRecovery == null || childVehicleLoanRecovery.Text == "")
            {
                childVehicleLoanRecovery = new VehicleLoanRecovery();
                childVehicleLoanRecovery.MdiParent = this;
                childVehicleLoanRecovery.Show();
            }
        }

        private void msbTrans_CRM_Vehicle_PtrlAllowApproval_Click(object sender, EventArgs e)
        {
            if (childPetrolAllowanceApproval == null || childPetrolAllowanceApproval.Text == "")
            {
                childPetrolAllowanceApproval = new PetrolAllowanceApproval();
                childPetrolAllowanceApproval.MdiParent = this;
                childPetrolAllowanceApproval.Show();
            }
        }

        private void msbTrans_Audit_VehicleAudit_Click(object sender, EventArgs e)
        {
            if (childVehicleAudit == null || childVehicleAudit.Text == "")
            {
                childVehicleAudit = new VehicleAudit("AUDIT");
                childVehicleAudit.MdiParent = this;
                childVehicleAudit.Show();
            }
        }

        private void msbTrans_CRM_Vehicle_PtrlAllowFinalAppr_Click(object sender, EventArgs e)
        {
            if (childPetrolAllowanceApproval == null || childPetrolAllowanceApproval.Text == "")
            {
                childPetrolAllowanceApproval = new PetrolAllowanceApproval("FINAL");
                childPetrolAllowanceApproval.MdiParent = this;
                childPetrolAllowanceApproval.Show();
            }
        }

        private void msbTrans_CRM_Vehicle_PtrlAllowLetterPrint_Click(object sender, EventArgs e)
        {
            if (childPetrolAllowReport == null || childPetrolAllowReport.Text == "")
            {
                childPetrolAllowReport = new PetrolAllowReport();
                childPetrolAllowReport.MdiParent = this;
                childPetrolAllowReport.Show();
            }
        }

        private void msbReport_CRM_Sale_RegisterWOUTCust_Click(object sender, EventArgs e)
        {
            if (childSalesRegReportForm == null || childSalesRegReportForm.Text == "")
            {
                childSalesRegReportForm = new SalesRegister(4);
                childSalesRegReportForm.MdiParent = this;
                childSalesRegReportForm.Show();
            }           

        }

        private void msbTrans_CRM_Vehicle_PtrlIncentiveAppr_Click(object sender, EventArgs e)
        {
            if (childVehicleIncentiveApproval == null || childVehicleIncentiveApproval.Text == "")
            {
                childVehicleIncentiveApproval = new VehicleIncentiveApproval();
                childVehicleIncentiveApproval.MdiParent = this;
                childVehicleIncentiveApproval.Show();
            }
        }

        private void msbReport_StockPoint_StockReconsilation_Click(object sender, EventArgs e)
        {
            if (CommonData.LogUserRole == "MANAGEMENT" || CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                chldReportGLSelection = new ReportGLSelection("STOCK_REC");
                chldReportGLSelection.MdiParent = this;
                chldReportGLSelection.Show();
            }
            else
            {
                chldfrmDoorknocks = new frmDoorknocks(10);
                chldfrmDoorknocks.MdiParent = this;
                chldfrmDoorknocks.Show();
            }
        }

        private void msbReports_CRM_VehicleLoanSummary_Click(object sender, EventArgs e)
        {
            chldReportGLSelection = new ReportGLSelection("VEHICLE_LOAN");
            chldReportGLSelection.MdiParent = this;
            chldReportGLSelection.Show();
        }

        private void msbReports_CRM_VehicleInfo_Click(object sender, EventArgs e)
        {
            chldReportGLSelection = new ReportGLSelection("VEHICLE_INFO");
            chldReportGLSelection.MdiParent = this;
            chldReportGLSelection.Show();
        }

        private void msbReport_StockPoint_StockSummary_Click(object sender, EventArgs e)
        {
            //chldReportGLSelection = new ReportGLSelection("STOCK_SUMMARY");
            //chldReportGLSelection.MdiParent = this;
            //chldReportGLSelection.Show();
            childEmpFromToSelection = new EmpFromToSelection(11);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbReport_StockPoint_StockLedger_Click(object sender, EventArgs e)
        {
            //chldReportGLSelection = new ReportGLSelection("STOCK_LEDGER");
            //chldReportGLSelection.MdiParent = this;
            //chldReportGLSelection.Show();
            childEmpFromToSelection = new EmpFromToSelection(10);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbReports_MIS_BranchBulletins_Current_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(7);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_MIS_BranchBulletinsTotal_Click(object sender, EventArgs e)
        {

            childCompBranchMonthForReport = new CompBranchMonthForReport(1);
            childCompBranchMonthForReport.MdiParent = this;
            childCompBranchMonthForReport.Show();

            //chldfrmDoorknocks = new frmDoorknocks(11);
            //chldfrmDoorknocks.MdiParent = this;
            //chldfrmDoorknocks.Show();
        }

        private void msbReports_MIS_BranchProductWiseSaleORRec_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(12);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbTrans_CRM_Sale_OpeningStock_Click(object sender, EventArgs e)
        {
            if (childOpeningStock == null || childOpeningStock.Text == "")
            {
                childOpeningStock = new OpeningStock();
                childOpeningStock.MdiParent = this;
                childOpeningStock.Show(); 
            }
        }

        private void msbTrans_CRM_Sale_StockTransfer_Click(object sender, EventArgs e)
        {
            if (childStockTransfer == null || childStockTransfer.Text == "")
            {
                childStockTransfer = new StockTransfer();
                childStockTransfer.MdiParent = this;
                childStockTransfer.Show();
            }
        }

        private void msbReport_StockPoint_GRNs_Click(object sender, EventArgs e)
        {
          
            if (CommonData.LogUserRole == "MANAGEMENT" || CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                childRepDateCompBranchSelection = new RepDateCompBranchSelection("STOCKPOINT_GRN");
                childRepDateCompBranchSelection.MdiParent = this;
                childRepDateCompBranchSelection.Show();
            }
            else
            {
                chldfrmDoorknocks = new frmDoorknocks("STOCKPOINT_GRN");
                chldfrmDoorknocks.MdiParent = this;
                chldfrmDoorknocks.Show();
            }
        }

        private void msbTrans_HR_MasterEdit_Click(object sender, EventArgs e)
        {
            
        }

        private void msbTrans_CRM_Stationary_Indent_Click_1(object sender, EventArgs e)
        {
            if (chldStationaryIndent == null || chldStationaryIndent.Text == "")
            {
                chldStationaryIndent = new StationaryIndent();
                chldStationaryIndent.MdiParent = this;
                chldStationaryIndent.Show();
            }
        }

        private void msbTrans_StockPoint_OpeningStock_Click(object sender, EventArgs e)
        {
            if (childOpeningStock == null || childOpeningStock.Text == "")
            {
                childOpeningStock = new OpeningStock(1);
                childOpeningStock.MdiParent = this;
                childOpeningStock.Show();
            }
        }
        
        private void msbTrans_HR_AgentApprovalLetters_Click(object sender, EventArgs e)
        {
            if (childAgentApprovalLetters == null || childAgentApprovalLetters.Text == "")
            {
                childAgentApprovalLetters = new AgentApprovalLetters();
                childAgentApprovalLetters.MdiParent = this;
                childAgentApprovalLetters.Show();
            }
        }

        private void msbTrans_CRM_Stationary_IndentList_Click(object sender, EventArgs e)
        {
            if (childStationaryIndentList == null || childStationaryIndentList.Text == "")
            {
                childStationaryIndentList = new StationaryIndentList("BR");
                childStationaryIndentList.MdiParent = this;
                childStationaryIndentList.Show();
            }
        }

        private void msbTrans_CRM_Stationary_IndentVerification_Click(object sender, EventArgs e)
        {
            if (childStationaryIndentList == null || childStationaryIndentList.Text == "")
            {
                childStationaryIndentList = new StationaryIndentList("MGR");
                childStationaryIndentList.MdiParent = this;
                childStationaryIndentList.Show();
            }
        }

        private void msbTrans_CRM_Stationary_IndentApproval_Click(object sender, EventArgs e)
        {
            if (childStationaryIndentList == null || childStationaryIndentList.Text == "")
            {
                childStationaryIndentList = new StationaryIndentList("HEAD");
                childStationaryIndentList.MdiParent = this;
                childStationaryIndentList.Show();
            }
        }

        private void msbMasters_StationaryItemsMaster_Click(object sender, EventArgs e)
        {
            if (childStationaryItemsMaster == null || childStationaryItemsMaster.Text == "")
            {
                childStationaryItemsMaster = new StationaryItemsMaster();
                childStationaryItemsMaster.MdiParent = this;
                childStationaryItemsMaster.Show();
            }
        }

        private void msbTrans_Statnry_IndtFromBranches_Click(object sender, EventArgs e)
        {
            if (childStationaryBrochureList == null || childStationaryBrochureList.Text == "")
            {
                childStationaryBrochureList = new StationaryBrochureList();
                childStationaryBrochureList.MdiParent = this;
                childStationaryBrochureList.Show();
            }
        }

        private void msbReports_MIS_EmpPerfm_MonthWise_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(13);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReport_StockPoint_CampToSPDistance_Click(object sender, EventArgs e)
        {
            if (childCamptoSP == null || childCamptoSP.Text == "")
            {
                childCamptoSP = new CamptoSP();
                childCamptoSP.MdiParent = this;
                childCamptoSP.Show();
            }
        }

        private void msbReports_HR_Recruitement_RecruitByIndvid_Click(object sender, EventArgs e)
        {
            if (childEmpFromToSelection == null || childEmpFromToSelection.Text == "")
            {
                childEmpFromToSelection = new EmpFromToSelection(0);
                childEmpFromToSelection.MdiParent = this;
                childEmpFromToSelection.Show();
            }
        }

        private void msbReports_MIS_EmpPerfm_CumulativeReport_Click(object sender, EventArgs e)
        {
            childCompBranchMonthForReport = new CompBranchMonthForReport(2);
            childCompBranchMonthForReport.MdiParent = this;
            childCompBranchMonthForReport.Show();
        }

        private void msb_Reports_HR_EmpInfo_PersonalInfo_Click(object sender, EventArgs e)
        {
            //if (childEmployeeInfo == null || childEmployeeInfo.Text == "")
            //{
            //    childEmployeeInfo = new EmployeeInfo();
            //    childEmployeeInfo.MdiParent = this;
            //    childEmployeeInfo.Show();
            //}
            if (childfrmEmployeeInfo == null || childfrmEmployeeInfo.Text == "")
            {
                childfrmEmployeeInfo = new frmEmployeeInfo();
                childfrmEmployeeInfo.MdiParent = this;
                childfrmEmployeeInfo.Show();
            }
        }

        private void msbReports_HR_Strengths_NotMappedList_Click(object sender, EventArgs e)
        {
            if (childNotMappedList == null || childNotMappedList.Text == "")
            {
                childNotMappedList = new NotMappedList();
                childNotMappedList.MdiParent = this;
                childNotMappedList.Show();
            }
            
        }

        private void msbTrans_Statnry_GRN_Click(object sender, EventArgs e)
        {
            if (childStationaryGRN == null || childStationaryGRN.Text == "")
            {
                childStationaryGRN = new StationaryGRN();
                childStationaryGRN.MdiParent = this;
                childStationaryGRN.Show();
            }
        }

        private void msbReports_MIS_EmpPerfm_Individual_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(1);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbTrans_CRM_Sales_AdvanceRefund_Click(object sender, EventArgs e)
        {
            if (childAdvancedRefund == null || childAdvancedRefund.Text == "")
            {
                childAdvancedRefund = new AdvancedRefund();
                childAdvancedRefund.MdiParent = this;
                childAdvancedRefund.Show();
            }
        }

        private void msbTrans_HR_IDCards_Click(object sender, EventArgs e)
        {
            if (childSelectECodes == null || childSelectECodes.Text == "")
            {
                childSelectECodes = new SelectECodes();
                childSelectECodes.MdiParent = this;
                childSelectECodes.Show();
            }
        }

        private void msbMaster_General_CompanyMaster_Click(object sender, EventArgs e)
        {
            if (childCompanyMaster == null || childCompanyMaster.Text == "")
            {
                childCompanyMaster = new CompanyMaster();
                childCompanyMaster.MdiParent = this;
                childCompanyMaster.Show();
            }
        }

        private void msbReport_CRM_Sale_AdvanceRefund_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(2);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbTrans_HR_ApplEntry_DeleteMultipleEntries_Click(object sender, EventArgs e)
        {
            if (childMultipleEntry == null || childMultipleEntry.Text == "")
            {
                childMultipleEntry = new MultipleEntry();
                childMultipleEntry.MdiParent = this;
                childMultipleEntry.Show();
            }
        }

        private void msbTrans_HR_AddressEdit_Click(object sender, EventArgs e)
        {
            if (childUpdateAddress == null || childUpdateAddress.Text == "")
            {
                childUpdateAddress = new UpdateAddress();
                childUpdateAddress.MdiParent = this;
                childUpdateAddress.Show();
            }
        }

        private void msbTrans_StockPoint_RefillProcess_Click(object sender, EventArgs e)
        {
            if (childSPRefill == null || childSPRefill.Text == "")
            {
                childSPRefill = new SPRefill();
                childSPRefill.MdiParent = this;
                childSPRefill.Show();
            }
        }

        private void msbReport_StockPoint_StockReconsilation_Click_1(object sender, EventArgs e)
        {
            if (CommonData.LogUserRole == "MANAGEMENT" || CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                chldReportGLSelection = new ReportGLSelection("STOCKPOINT_RECONSILATION");
                chldReportGLSelection.MdiParent = this;
                chldReportGLSelection.Show();
            }
            else
            {
                chldfrmDoorknocks = new frmDoorknocks("STOCKPOINT_RECONSILATION");
                chldfrmDoorknocks.MdiParent = this;
                chldfrmDoorknocks.Show();
            }
        }

        private void msbReports_CRM_StockDetails_SPDCToEmp_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(3);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbMasters_StationarySupplierMaster_Click(object sender, EventArgs e)
        {
            if (childSupplierMaster == null || childSupplierMaster.Text == "")
            {
                childSupplierMaster = new supplier();
                childSupplierMaster.MdiParent = this;
                childSupplierMaster.Show();
            }
        }

        private void msbReport_StockPoint_StockReconsilationUptoDate_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(4);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbTrans_CRM_Stationary_StationaryGRN_BR_Click(object sender, EventArgs e)
        {
            if (childStationaryBrGRN == null || childStationaryBrGRN.Text == "")
            {
                childStationaryBrGRN = new StationaryBrGRN();
                childStationaryBrGRN.MdiParent = this;
                childStationaryBrGRN.Show();
            }
        }

        private void msbTrans_HR_ApplEntry_AgentRejoin_Click(object sender, EventArgs e)
        {
            if (childPrevEmployee == null || childPrevEmployee.Text == "")
            {
                childPrevEmployee = new PrevEmployee(0);
                childPrevEmployee.MdiParent = this;
                childPrevEmployee.Show();
            }
        }

        private void msbTrans_CRM_Stationary_StationaryIssue_Click(object sender, EventArgs e)
        {
            if (childBranchStationaryIssue == null || childBranchStationaryIssue.Text == "")
            {
                childBranchStationaryIssue = new BranchStationaryIssue();
                childBranchStationaryIssue.MdiParent = this;
                childBranchStationaryIssue.Show();
            }
        }

        private void msbMasters_StockPoints_SPDetails_Click(object sender, EventArgs e)
        {
            if (childStockPointDetails == null || childStockPointDetails.Text == "")
            {
                childStockPointDetails = new StockPointDetails();
                childStockPointDetails.MdiParent = this;
                childStockPointDetails.Show();
            }
        }

        private void msbMasters_StockPoints_FCOProducts_Click(object sender, EventArgs e)
        {
            if (childFcoReturns == null || childFcoReturns.Text == "")
            {
                childFcoReturns = new FcoReturns();
                childFcoReturns.MdiParent = this;
                childFcoReturns.Show();
            }
        }

        private void msbMasters_StockPoints_Inspections_Click(object sender, EventArgs e)
        {
            if (childInspectionDetails == null || childInspectionDetails.Text == "")
            {
                childInspectionDetails = new InspectionDetails();
                childInspectionDetails.MdiParent = this;
                childInspectionDetails.Show();
            }
        }

        private void msbReport_StockPoint_OpeningStock_Click(object sender, EventArgs e)
        {
            CommonData.ViewReport = "STOCKPOINT_OPENING_STOCK";
            childReportViewer = new ReportViewer(CommonData.CompanyCode,CommonData.BranchCode,CommonData.DocMonth);
            childReportViewer.Show();
        }

        private void msbTrans_StockPoint_StockDump_Click(object sender, EventArgs e)
        {
            if (childStockDumpdetails == null || childStockDumpdetails.Text == "")
            {
                childStockDumpdetails = new StockDumpdetails();
                childStockDumpdetails.MdiParent = this;
                childStockDumpdetails.Show();
            }
        }

        private void msbReport_HR_Agent_BranchWise_Click(object sender, EventArgs e)
        {
            chldReportGLSelection = new ReportGLSelection("SALES_STAFF_ALL");
            chldReportGLSelection.MdiParent = this;
            chldReportGLSelection.Show();
        }

        private void msb_Reports_HR_DashBoard_ApplStatusSummary_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(5);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbTrans_CRM_Sale_SalesEdits_OrderNoChange_Click(object sender, EventArgs e)
        {
            if (childSalesInvoiceEdit == null || childSalesInvoiceEdit.Text == "")
            {
                childSalesInvoiceEdit = new SalesInvoiceEdit();
                childSalesInvoiceEdit.MdiParent = this;
                childSalesInvoiceEdit.Show();
            }
        }

        private void msbTrans_StockPoint_StockInternalTransfer_Click(object sender, EventArgs e)
        {
            if (childInternalDamage == null || childInternalDamage.Text == "")
            {
                childInternalDamage = new InternalDamage();
                childInternalDamage.MdiParent = this;
                childInternalDamage.Show();
            }
        }

        private void msbTrans_CRM_Stationary_StationaryOpeningStock_Click(object sender, EventArgs e)
        {
            if (childStationaryOpeningStock == null || childStationaryOpeningStock.Text == "")
            {
                childStationaryOpeningStock = new StationaryOpeningStock();
                childStationaryOpeningStock.MdiParent = this;
                childStationaryOpeningStock.Show();
            }
        }

        private void msbTrans_Statnry_OpeningStock_Click(object sender, EventArgs e)
        {
            if (childStationaryOpeningStock == null || childStationaryOpeningStock.Text == "")
            {
                childStationaryOpeningStock = new StationaryOpeningStock();
                childStationaryOpeningStock.MdiParent = this;
                childStationaryOpeningStock.Show();
            }
             if (childStationaryOpeningStock == null || childStationaryOpeningStock.Text == "")
            {
                childStationaryOpeningStock = new StationaryOpeningStock();
                childStationaryOpeningStock.MdiParent = this;
                childStationaryOpeningStock.Show();
            }
        }

        private void msbReport_Stationary_StockRecons_Click(object sender, EventArgs e)
        {
            childReportFDateTDate = new ReportFDateTDate("SSCRM_REP_STATIONARY_RECONSILATION_STORE");
            childReportFDateTDate.MdiParent = this;
            childReportFDateTDate.Show();
        }

        private void msbReports_CRM_Stationary_ClosStOrRecons_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(6);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbReports_CRM_Stationary_STIssueRegister_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(7);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbReports_CRM_Stationary_STIndentRegister_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(8);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbTrans_StockPoint_PackingMtrl_GRN_Click(object sender, EventArgs e)
        {
            if (childSPPackingMtrlGRN == null || childSPPackingMtrlGRN.Text == "")
            {
                childSPPackingMtrlGRN = new SPPackingMtrlGRN();
                childSPPackingMtrlGRN.MdiParent = this;
                childSPPackingMtrlGRN.Show();
            }
        }

        private void msbMasters_StockPoints_RentalAgriments_Click(object sender, EventArgs e)
        {
            if (childSPRentalAgriments == null || childSPRentalAgriments.Text == "")
            {
                childSPRentalAgriments = new SPRentalAgriments();
                childSPRentalAgriments.MdiParent = this;
                childSPRentalAgriments.Show();
            }
        }

        private void msbTrans_StockPoint_PackingMtrl_DCDCST_Click(object sender, EventArgs e)
        {
            if (childSPPackingMtrlDC == null || childSPPackingMtrlDC.Text == "")
            {
                childSPPackingMtrlDC = new SPPackingMtrlDC();
                childSPPackingMtrlDC.MdiParent = this;
                childSPPackingMtrlDC.Show();
            }
        }

        private void msbReport_Production_FinishedGoods_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(9);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbTrans_HR_PromotionBoard_PB_Click(object sender, EventArgs e)
        {
            if (childPromotionBoard == null || childPromotionBoard.Text == "")
            {
                childPromotionBoard = new PromotionBoard();
                childPromotionBoard.MdiParent = this;
                childPromotionBoard.Show();
            }
        }

        private void msbTrans_HR_DigitalIDCards_Click(object sender, EventArgs e)
        {
            if (childSelectECodes == null || childSelectECodes.Text == "")
            {
                childSelectECodes = new SelectECodes(1);
                childSelectECodes.MdiParent = this;
                childSelectECodes.Show();
            }
        }

        private void msbTrans_CRM_Sales_Hierarch_BranchAboveLvlMap_Click(object sender, EventArgs e)
        {

            if (childAboveBranchLevelMapping == null || childAboveBranchLevelMapping.Text == "")
            {
                childAboveBranchLevelMapping = new AboveBranchLevelMapping();
                childAboveBranchLevelMapping.MdiParent = this;
                childAboveBranchLevelMapping.Show();
            }
        }

        private void msbTrans_HR_PromotionBoard_EmpPositUpdate_Click(object sender, EventArgs e)
        {
            if (childEmpPresentSitUpdate == null || childEmpPresentSitUpdate.Text == "")
            {
                childEmpPresentSitUpdate = new EmpPresentSitUpdate();
                childEmpPresentSitUpdate.MdiParent = this;
                childEmpPresentSitUpdate.Show();
            }
        }

        private void msbTrans_HR_PromotionBoard_PBApprovals_Click(object sender, EventArgs e)
        {
            if (childPromotionBoardApproval == null || childPromotionBoardApproval.Text == "")
            {
                childPromotionBoardApproval = new PromotionBoardApproval();
                childPromotionBoardApproval.MdiParent = this;
                childPromotionBoardApproval.Show();
            }
        }        

        private void msbReports_CRM_Stationary_TransitDCFromStationary_Click(object sender, EventArgs e)
        {
            childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, CommonData.FinancialYear);
            CommonData.ViewReport = "TRANSIT_STATIONARY_DC";
            childReportViewer.Show();
        }

        private void msbMasters_CorrierMaster_Click(object sender, EventArgs e)
        {
            if (childCorrierMaster == null || childCorrierMaster.Text == "")
            {
                childCorrierMaster = new CorrierMaster();
                childCorrierMaster.MdiParent = this;
                childCorrierMaster.Show();
            }
        }

        private void msbTrans_HR_MasterEdit_DOJUpdate_Click(object sender, EventArgs e)
        {
            if (childEmpDOjUpdate == null || childEmpDOjUpdate.Text == "")
            {
                childEmpDOjUpdate = new EmpDOjUpdate();
                childEmpDOjUpdate.MdiParent = this;
                childEmpDOjUpdate.Show();
            }
        }

        private void msbTrans_Statnry_StationaryDCWithoutIndent_Click(object sender, EventArgs e)
        {
            if (childStationaryDCByHand == null || childStationaryDCByHand.Text == "")
            {
                childStationaryDCByHand = new StationaryDCByHand();
                childStationaryDCByHand.MdiParent = this;
                childStationaryDCByHand.Show();
            }
        }

        private void msbMasters_Stationary_TransportMaster_Click(object sender, EventArgs e)
        {
            if (childTransportMaster == null || childTransportMaster.Text == "")
            {
                childTransportMaster = new TransportMaster();
                childTransportMaster.MdiParent = this;
                childTransportMaster.Show();
            }
        }

        private void msbReport_CRM_Sale_MonthlyInputs_Click(object sender, EventArgs e)
        {
            frmDoorknocks chldfrmDoorknocks = new frmDoorknocks(14);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbTrans_HR_IDPreparation_Click(object sender, EventArgs e)
        {
            if (childIdCardPreparationsdetails == null || childIdCardPreparationsdetails.Text == "")
            {
                childIdCardPreparationsdetails = new IdCardPreparationsdetails();
                childIdCardPreparationsdetails.MdiParent = this;
                childIdCardPreparationsdetails.Show();
            }
        }

        private void msb_Reports_HR_IDCardHistory_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(12);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbTrans_ProdUnit_DCST_Click(object sender, EventArgs e)
        {
            if (childDeliveryChallanPU == null || childDeliveryChallanPU.Text == "")
            {
                childDeliveryChallanPU = new DeliveryChallanPU("DCST");
                childDeliveryChallanPU.MdiParent = this;
                childDeliveryChallanPU.Show();
            }
        }

        private void msbReport_Production_DCRegister_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(13);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbReport_Production_DCSTRegister_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(14);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbTrans_ProdUnit_OpeningStock_Click(object sender, EventArgs e)
        {
            if (childOpeningStock == null || childOpeningStock.Text == "")
            {
                childOpeningStock = new OpeningStock(2);
                childOpeningStock.MdiParent = this;
                childOpeningStock.Show();
            }
        }

        private void msbReport_StockPoint_SPHeadDetails_Click(object sender, EventArgs e)
        {

        }

        private void msbReport_StockPoint_SPHeadDetails_MasterDetls_Click(object sender, EventArgs e)
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
            CommonData.ViewReport = "SSCRM_REP_SP_HEAD_DETAILS";
            ReportViewer objReportview = new ReportViewer(strComp, strBranch, "");
            objReportview.Show();
        }

        private void msbProcess_SendSMS_Click(object sender, EventArgs e)
        {
            if (childSendSMS == null || childSendSMS.Text == "")
            {
                childSendSMS = new SendSMS();
                childSendSMS.MdiParent = this;
                childSendSMS.Show();
            }
        }

        private void msb_Transaction_Transport_VehicleDetails_Click(object sender, EventArgs e)
        {
            
        }

        private void msbReport_Transport_VehicleDetails_Click(object sender, EventArgs e)
        {
            CommonData.ViewReport = "SSCRM_REP_TR_VEHICLE_MASTER";
            ReportViewer objReportview = new ReportViewer();
            objReportview.Show();
        }

        private void msbMasters_TransportMasters_VehModMaster_Click(object sender, EventArgs e)
        {
            if (childVehicleMaster == null || childVehicleMaster.Text == "")
            {
                childVehicleMaster = new VehicleMaster();
                childVehicleMaster.MdiParent = this;
                childVehicleMaster.Show();
            }
        }

        private void msbMaster_General_BranchAddrUpdate_Click(object sender, EventArgs e)
        {
            if (childfrmBranchMasterUpdate == null || childfrmBranchMasterUpdate.Text == "")
            {
                childfrmBranchMasterUpdate = new frmBranchMasterUpdate();
                childfrmBranchMasterUpdate.MdiParent = this;
                childfrmBranchMasterUpdate.Show();
            }
        }

        private void msb_Transaction_Transport_VehicleDetails_MasterDetails_Click(object sender, EventArgs e)
        {
            if (childTransportVehicleMaster == null || childTransportVehicleMaster.Text == "")
            {
                childTransportVehicleMaster = new TransportVehicleMaster();
                childTransportVehicleMaster.MdiParent = this;
                childTransportVehicleMaster.Show();
            }
        }

        private void msb_Transaction_Transport_VehicleDetails_VehiclePermitDetls_Click(object sender, EventArgs e)
        {
            if (childVehiclePermitDetails == null || childVehiclePermitDetails.Text == "")
            {
                childVehiclePermitDetails = new VehiclePermitDetails();
                childVehiclePermitDetails.MdiParent = this;
                childVehiclePermitDetails.Show();
            }
        }

        private void msb_Transaction_Transport_VehicleDetails_PolutionCheckDetls_Click(object sender, EventArgs e)
        {
            if (childPolutionCheckDetails == null || childPolutionCheckDetails.Text == "")
            {
                childPolutionCheckDetails = new PolutionCheckDetails();
                childPolutionCheckDetails.MdiParent = this;
                childPolutionCheckDetails.Show();
            }
        }

        private void msbTrans_ProdUnit_GRN_Click(object sender, EventArgs e)
        {
            if (childGoodsReceiptNotePU == null || childGoodsReceiptNotePU.Text == "")
            {
                childGoodsReceiptNotePU = new GoodsReceiptNotePU();
                childGoodsReceiptNotePU.MdiParent = this;
                childGoodsReceiptNotePU.Show();
            }
        }

        private void msbMasters_HRISMasters_RecruitementMaster_Click(object sender, EventArgs e)
        {
            if (childRecruitmentSourceMaster == null || childRecruitmentSourceMaster.Text == "")
            {
                childRecruitmentSourceMaster = new RecruitmentSourceMaster();
                childRecruitmentSourceMaster.MdiParent = this;
                childRecruitmentSourceMaster.Show();
            }
        }

        private void msbReport_Stationary_GRNReg_Click(object sender, EventArgs e)
        {
            childReportFDateTDate = new ReportFDateTDate("SSERP_REP_STATIONARY_GRN_REGISTER");
            childReportFDateTDate.MdiParent = this;
            childReportFDateTDate.Show();
        }

        private void msbReport_Production__RecByDestBranch_Click(object sender, EventArgs e)
        {
            if (childFDateTDateBranchSelection == null || childFDateTDateBranchSelection.Text == "")
            {
                childFDateTDateBranchSelection = new FDateTDateBranchSelection(1);
                childFDateTDateBranchSelection.MdiParent = this;
                childFDateTDateBranchSelection.Show();
            }
        }

        private void dmLink_Click(object sender, EventArgs e)
        {
            Process.Start("http://shivashakthigroup.com");
        }

        private void msbTrans_CRM_Sale_StockGRN_Click(object sender, EventArgs e)
        {
            if (childSPGRNInputBranch == null || childSPGRNInputBranch.Text == "")
            {
                childSPGRNInputBranch = new SPGRNInputBranch();
                childSPGRNInputBranch.MdiParent = this;
                childSPGRNInputBranch.Show();
            }
        }

        private void msbMasters_Products_ProductMasCompany_Click(object sender, EventArgs e)
        {
            if (childProductMasterCompany == null || childProductMasterCompany.Text == "")
            {
                childProductMasterCompany = new ProductMasterCompany();
                childProductMasterCompany.MdiParent = this;
                childProductMasterCompany.Show();
            }
        }

        private void msbTrans_HR_ApplEntry_EmployeeRejoin_Click(object sender, EventArgs e)
        {
            if (childPrevEmployee == null || childPrevEmployee.Text == "")
            {
                childPrevEmployee = new PrevEmployee(1);
                childPrevEmployee.MdiParent = this;
                childPrevEmployee.Show();
            }
        }

        private void msb_Reports_HR_EmpInfo_SalPromTransInfo_Click(object sender, EventArgs e)
        {
            if (childAgentApprovalLetters == null || childAgentApprovalLetters.Text == "")
            {
                childAgentApprovalLetters = new AgentApprovalLetters(1);
                childAgentApprovalLetters.MdiParent = this;
                childAgentApprovalLetters.Show();
            }
        }

        private void msbTrans_Production_StockInternalTransfer_Click(object sender, EventArgs e)
        {
            if (childInternalDamage == null || childInternalDamage.Text == "")
            {
                childInternalDamage = new InternalDamage();
                childInternalDamage.MdiParent = this;
                childInternalDamage.Show();
            }
        }

        private void msbTrans_Production_RefillProcess_Click(object sender, EventArgs e)
        {
            if (childSPRefill == null || childSPRefill.Text == "")
            {
                childSPRefill = new SPRefill();
                childSPRefill.MdiParent = this;
                childSPRefill.Show();
            }
        }

        private void msbTrans_CRM_Sales_Hierarch_SourceToDestMapping_Click(object sender, EventArgs e)
        {
            //if (childAboveGrpSourceToDestMapping == null || childAboveGrpSourceToDestMapping.Text == "")
            //{
            //    childAboveGrpSourceToDestMapping = new AboveGrpSourceToDestMapping();
            //    childAboveGrpSourceToDestMapping.MdiParent = this;
            //    childAboveGrpSourceToDestMapping.Show();
            //}
        }

        private void msbProcess_ProcessSaleBulletins_Click(object sender, EventArgs e)
        {
            if (childProcessOverAllSalesBulletinCalc == null || childProcessOverAllSalesBulletinCalc.Text == "")
            {
                childProcessOverAllSalesBulletinCalc = new ProcessOverAllSalesBulletinCalc();
                childProcessOverAllSalesBulletinCalc.MdiParent = this;
                childProcessOverAllSalesBulletinCalc.Show();
            }
        }

        private void msbTrans_CRM_Vehicle_PtrlIncentiveRequest_Click(object sender, EventArgs e)
        {
            if (childPetrolIncentiveRequest == null || childPetrolIncentiveRequest.Text == "")
            {
                childPetrolIncentiveRequest = new PetrolIncentiveRequest();
                childPetrolIncentiveRequest.MdiParent = this;
                childPetrolIncentiveRequest.Show();
            }
        }

        private void msbMasters_Products_ProductPriceCircular_Click(object sender, EventArgs e)
        {
            if (childProductPriceCircular == null || childProductPriceCircular.Text == "")
            {
                childProductPriceCircular = new ProductPriceCircular();
                childProductPriceCircular.MdiParent = this;
                childProductPriceCircular.Show();
            }
        }

        private void msbTrans_HR_Biometrics_LeaveForm_Click(object sender, EventArgs e)
        {
            if (childLeaveEntryForm == null || childLeaveEntryForm.Text == "")
            {
                childLeaveEntryForm = new LeaveEntryForm("HO");
                childLeaveEntryForm.MdiParent = this;
                childLeaveEntryForm.Show();
            }
        }

        private void msbTrans_HR_Biometrics_ODForm_Click(object sender, EventArgs e)
        {
            if (childOnDutySlip == null || childOnDutySlip.Text == "")
            {
                childOnDutySlip = new OnDutySlip("HO");
                childOnDutySlip.MdiParent = this;
                childOnDutySlip.Show();
            }
        }

        private void msbTrans_HR_Biometrics_CoffForm_Click(object sender, EventArgs e)
        {
            if (childCompensatoryOFF == null || childCompensatoryOFF.Text == "")
            {
                childCompensatoryOFF = new CompensatoryOFF("HO");
                childCompensatoryOFF.MdiParent = this;
                childCompensatoryOFF.Show();
            }
        }

        private void msbTrans_HR_Biometrics_PunchMissing_Click(object sender, EventArgs e)
        {
            if (childPunchMissing == null || childPunchMissing.Text == "")
            {
                childPunchMissing = new PunchMissing();
                childPunchMissing.MdiParent = this;
                childPunchMissing.Show();
            }
        }

        private void msbTrans_HR_Biometrics_LeaveCredits_Click(object sender, EventArgs e)
        {
            if (childEmployeeLeavesCredit == null || childEmployeeLeavesCredit.Text == "")
            {
                childEmployeeLeavesCredit = new EmployeeLeavesCredit("HO");
                childEmployeeLeavesCredit.MdiParent = this;
                childEmployeeLeavesCredit.Show();
            }
        }

        private void msbReport_CRM_Sale_PriceCirculars_Click(object sender, EventArgs e)
        {
            if (childProductPricePrint == null || childProductPricePrint.Text == "")
            {
                childProductPricePrint = new ProductPricePrint();
                childProductPricePrint.MdiParent = this;
                childProductPricePrint.Show();
            }
        }

        private void msbMasters_HRISMasters_DesigMaster_Click(object sender, EventArgs e)
        {
            if (childDesignationMaster == null || childDesignationMaster.Text == "")
            {
                childDesignationMaster = new DesignationMaster();
                childDesignationMaster.MdiParent = this;
                childDesignationMaster.Show();
            }
        }

        private void msbTrans_HR_Biometrics_HolidaysList_Click(object sender, EventArgs e)
        {
            if (childHolidaysMaster == null || childHolidaysMaster.Text == "")
            {
                childHolidaysMaster = new HolidaysMaster();
                childHolidaysMaster.MdiParent = this;
                childHolidaysMaster.Show();
            }
        }

        private void msb_Reports_HR_BioMetrics_HolidaysList_Click(object sender, EventArgs e)
        {
            ReportViewer childReportViewer = new ReportViewer(Convert.ToInt32(Convert.ToDateTime(CommonData.CurrentDate).ToString("yyyy")));
            CommonData.ViewReport = "SSCRM_HOLIDAYMASTER";
            childReportViewer.Show();
        }

        private void msbTrans_HR_MasterEdit_EmpContactDetails_Click(object sender, EventArgs e)
        {
            if (childEmployeeContactDetails == null || childEmployeeContactDetails.Text == "")
            {
                childEmployeeContactDetails = new EmployeeContactDetails();
                childEmployeeContactDetails.MdiParent = this;
                childEmployeeContactDetails.Show();
            }
        }

        private void msb_Reports_HR_EmpInfo_ContactDetails_Click(object sender, EventArgs e)
        {
            if (CommonData.LogUserRole == "MANAGEMENT" || CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                chldReportGLSelection = new ReportGLSelection("EMP_CONTACT_DETAILS");
                chldReportGLSelection.MdiParent = this;
                chldReportGLSelection.Show();
            }
            else
            {
                chldfrmDoorknocks = new frmDoorknocks("EMP_CONTACT_DETAILS");
                chldfrmDoorknocks.MdiParent = this;
                chldfrmDoorknocks.Show();
            }
            //if (CommonData.LogUserRole == "MANAGEMENT" || CommonData.LogUserRole == "ADMIN" || CommonData.LogUserId.ToUpper() == "ADMIN")
            //{
            //    ReportViewer childReportViewer = new ReportViewer("");
            //    CommonData.ViewReport = "SSCRM_HR_REP_EMP_CONTACT_DETAILS";
            //    childReportViewer.Show();
            //}
            //else
            //{
            //    ReportViewer childReportViewer = new ReportViewer(CommonData.BranchCode);
            //    CommonData.ViewReport = "SSCRM_HR_REP_EMP_CONTACT_DETAILS";
            //    childReportViewer.Show();
            //}
        }

        private void msb_Reports_HR_DashBoard_ApprovalsListByDate_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(15);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbMasters_Products_ProductPriceCircularAll_Click(object sender, EventArgs e)
        {
            if (childfrmProductPriceCircular == null || childfrmProductPriceCircular.Text == "")
            {
                childfrmProductPriceCircular = new frmProductPriceCircular();
                childfrmProductPriceCircular.MdiParent = this;
                childfrmProductPriceCircular.Show();
            }
        }

        private void msbMasters_HRISMasters_MisCondCatMas_Click(object sender, EventArgs e)
        {
            if (childMisconductCategoryMaster == null || childMisconductCategoryMaster.Text == "")
            {
                childMisconductCategoryMaster = new MisconductCategoryMaster();
                childMisconductCategoryMaster.MdiParent = this;
                childMisconductCategoryMaster.Show();
            }
        }        

        private void msbTrans_HR_MasterEdit_EmpMasterEdit_Click_1(object sender, EventArgs e)
        {
            if (childfrmEditingInfo == null || childfrmEditingInfo.Text == "")
            {
                childfrmEditingInfo = new frmEditingInfo();
                childfrmEditingInfo.MdiParent = this;
                childfrmEditingInfo.Show();
            }
        }

        private void msbAdmin_TaskMaster_Click(object sender, EventArgs e)
        {
            if (childTaskMaster == null || childTaskMaster.Text == "")
            {
                childTaskMaster = new TaskMaster();
                childTaskMaster.MdiParent = this;
                childTaskMaster.Show();
            }
        }

        private void msbTrans_HR_MasterEdit_HRIStoCRM_Click(object sender, EventArgs e)
        {
            if (childHRISTOERP == null || childHRISTOERP.Text == "")
            {
                childHRISTOERP = new HRISTOERP();
                childHRISTOERP.MdiParent = this;
                childHRISTOERP.Show();
            }
        }

        private void msbMasters_Products_CombiMaster_Click(object sender, EventArgs e)
        {
            if (childCombiProductsList == null || childCombiProductsList.Text == "")
            {
                childCombiProductsList = new CombiProductsList();
                childCombiProductsList.MdiParent = this;
                childCombiProductsList.Show();
            }
        }

        private void msbTrans_LegalDept_ProdLiceList_Click(object sender, EventArgs e)
        {
            if (childProductLicenceDetails == null || childProductLicenceDetails.Text == "")
            {
                childProductLicenceDetails = new ProductLicenceDetails();
                childProductLicenceDetails.MdiParent = this;
                childProductLicenceDetails.Show();
            }
        }

        private void msbTrans_LegalDept_ProdLiceEntry_Click(object sender, EventArgs e)
        {
            if (childProductLicence == null || childProductLicence.Text == "")
            {
                childProductLicence = new ProductLicence();
                childProductLicence.MdiParent = this;
                childProductLicence.Show();
            }
        }

        private void msbTrans_Stationary_Indent_Click(object sender, EventArgs e)
        {
            if (chldStationaryIndent == null || chldStationaryIndent.Text == "")
            {
                chldStationaryIndent = new StationaryIndent();
                chldStationaryIndent.MdiParent = this;
                chldStationaryIndent.Show();
            }
        }

        private void msb_Reports_HR_BioMetrics_LateCommingsSummary_Click(object sender, EventArgs e)
        {
            if (childEmpFromToSelection == null || childEmpFromToSelection.Text == "")
            {
                childEmpFromToSelection = new EmpFromToSelection(16);
                childEmpFromToSelection.MdiParent = this;
                childEmpFromToSelection.Show();
            }
        }

        private void msb_Reports_HR_PayRoll_EmpSalDetls_Click(object sender, EventArgs e)
        {
            CommonData.ViewReport = "HR_REP_EMPS_SALAREES_STATEMENT";
            childReportViewer = new ReportViewer(CommonData.CompanyCode,CommonData.BranchCode,CommonData.BranchType,"","","","");
            childReportViewer.Show();
        }

        private void msbTrans_HR_PayRoll_EmpPFMaster_Click(object sender, EventArgs e)
        {

        }

        private void msbTrans_CRM_Sale_SummaryBulletin_Click(object sender, EventArgs e)
        {
            if (childSalesSummaryBulletin == null || childSalesSummaryBulletin.Text == "")
            {
                childSalesSummaryBulletin = new SalesSummaryBulletin();
                childSalesSummaryBulletin.MdiParent = this;
                childSalesSummaryBulletin.Show();
            }
        }

        private void msbReport_CRM_Sale_SSB_Bulletins_Click(object sender, EventArgs e)
        {
            if (childBulletInsForm == null || childBulletInsForm.Text == "")
            {
                childBulletInsForm = new frmBulletIns(1);
                childBulletInsForm.MdiParent = this;
                childBulletInsForm.Show();
            }
        }

        private void msbReport_CRM_Sale_SSB_SRWiseBulletins_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(15);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReport_CRM_Sale_SSB_GCGLWiseBulletins_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(16);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbProcess_ProcessSaleBulletins_PSB_SSB_Click(object sender, EventArgs e)
        {
            childReportGLSelection = new ReportGLSelection("SaleSummaryBulletin");
            childReportGLSelection.MdiParent = this;
            childReportGLSelection.Show();
        }

        private void msbTrans_ServiceDept_HierarchyMap_AOMap_Click(object sender, EventArgs e)
        {
            //if (childServiceMappingFromSourceToDestination == null || childServiceMappingFromSourceToDestination.Text == "")
            //{
            //    childServiceMappingFromSourceToDestination = new ServiceMappingFromSourceToDestination();
            //    childServiceMappingFromSourceToDestination.MdiParent = this;
            //    childServiceMappingFromSourceToDestination.Show();
            //}
        }

        private void msbTrans_ServiceDept_SMS2Cust_Click(object sender, EventArgs e)
        {
            if (childCustomersListByBranchForSMS == null || childCustomersListByBranchForSMS.Text == "")
            {
                childCustomersListByBranchForSMS = new CustomersListByBranchForSMS();
                childCustomersListByBranchForSMS.MdiParent = this;
                childCustomersListByBranchForSMS.Show();
            }
        }

        private void msbTrans_ServiceDept_HierarchyMap_AboveGroupMap_Click(object sender, EventArgs e)
        {
            //if (childServiceLevelGroupToDestination == null || childServiceLevelGroupToDestination.Text == "")
            //{
            //    childServiceLevelGroupToDestination = new ServiceLevelGroupToDestination();
            //    childServiceLevelGroupToDestination.MdiParent = this;
            //    childServiceLevelGroupToDestination.Show();
            //}
        }

        private void msbTrans_ServiceDept_HierarchyMap_AboveLevelMap_Click(object sender, EventArgs e)
        {
            //if (childServiceMappingFromSourceToDestination == null || childServiceMappingFromSourceToDestination.Text == "")
            //{
            //    childServiceMappingFromSourceToDestination = new ServiceMappingFromSourceToDestination();
            //    childServiceMappingFromSourceToDestination.MdiParent = this;
            //    childServiceMappingFromSourceToDestination.Show();
            //}
        }

        private void msbTrans_ServiceDept_ServiceUpdate_Click(object sender, EventArgs e)
        {
            if (childSalesService == null || childSalesService.Text == "")
            {
                childSalesService = new ActivityServiceUpdate();
                childSalesService.MdiParent = this;
                childSalesService.Show();
            }
        }

        private void msbTrans_Stationary_IndentList_Click(object sender, EventArgs e)
        {
            if (childStationaryIndentList == null || childStationaryIndentList.Text == "")
            {
                childStationaryIndentList = new StationaryIndentList("BR");
                childStationaryIndentList.MdiParent = this;
                childStationaryIndentList.Show();
            }
        }

        private void msbTrans_Stationary_IndentVerif_Click(object sender, EventArgs e)
        {
            if (childStationaryIndentList == null || childStationaryIndentList.Text == "")
            {
                childStationaryIndentList = new StationaryIndentList("MGR");
                childStationaryIndentList.MdiParent = this;
                childStationaryIndentList.Show();
            }
        }

        private void msbTrans_Stationary_IndentApprv_Click(object sender, EventArgs e)
        {
            if (childStationaryIndentList == null || childStationaryIndentList.Text == "")
            {
                childStationaryIndentList = new StationaryIndentList("HEAD");
                childStationaryIndentList.MdiParent = this;
                childStationaryIndentList.Show();
            }
        }

        private void msbTrans_Stationary_GRN_Click(object sender, EventArgs e)
        {
            if (childStationaryBrGRN == null || childStationaryBrGRN.Text == "")
            {
                childStationaryBrGRN = new StationaryBrGRN();
                childStationaryBrGRN.MdiParent = this;
                childStationaryBrGRN.Show();
            }
        }

        private void msbTrans_Stationary_Issue_Click(object sender, EventArgs e)
        {
            if (childBranchStationaryIssue == null || childBranchStationaryIssue.Text == "")
            {
                childBranchStationaryIssue = new BranchStationaryIssue();
                childBranchStationaryIssue.MdiParent = this;
                childBranchStationaryIssue.Show();
            }
        }

        private void msbTrans_ServiceDept_FM_Click(object sender, EventArgs e)
        {
            if (childFarmerMeetingForm == null || childFarmerMeetingForm.Text == "")
            {
                childFarmerMeetingForm = new FarmerMeetingForm();
                childFarmerMeetingForm.MdiParent = this;
                childFarmerMeetingForm.Show();
            }
        }

        private void msbTrans_ServiceDept_WCFF_Click(object sender, EventArgs e)
        {
            if (childfrmWrongCommitmentOrFinancialFrauds == null || childfrmWrongCommitmentOrFinancialFrauds.Text == "")
            {
                childfrmWrongCommitmentOrFinancialFrauds = new frmWrongCommitmentOrFinancialFrauds();
                childfrmWrongCommitmentOrFinancialFrauds.MdiParent = this;
                childfrmWrongCommitmentOrFinancialFrauds.Show();
            }
        }

        private void msbTrans_Foundation_EyeCampOpForm_Click(object sender, EventArgs e)
        {
            if (childEyeCampPatientDetails == null || childEyeCampPatientDetails.Text == "")
            {
                childEyeCampPatientDetails = new EyeCampPatientDetails();
                childEyeCampPatientDetails.MdiParent = this;
                childEyeCampPatientDetails.Show();
            }
        }

        private void msbReports_MIS_SBS_GCGLIndCrosTab_Click(object sender, EventArgs e)
        {
            childCompBranchMonthForReport = new CompBranchMonthForReport(3);
            childCompBranchMonthForReport.MdiParent = this;
            childCompBranchMonthForReport.Show();
        }

        private void mbReports_Foundation_EyeCampReg_Click(object sender, EventArgs e)
        {
            childReportFDateTDate = new ReportFDateTDate("FOUNDATION_EYE_CAMP_REG");
            childReportFDateTDate.MdiParent = this;
            childReportFDateTDate.Show();
        }

        private void msbTrans_HR_Misconduct_Click(object sender, EventArgs e)
        {
            //if (childMisconductForm == null || childMisconductForm.Text == "")
            //{
            //    childMisconductForm = new MisconductForm();
            //    childMisconductForm.MdiParent = this;
            //    childMisconductForm.Show();
            //}
        }

        private void msbReports_CRM_Docm_GroupSrProdRec_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(17);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbTrans_ServiceDept_DP_Click(object sender, EventArgs e)
        {
            if (childfrmDemoPlots == null || childfrmDemoPlots.Text == "")
            {
                childfrmDemoPlots = new frmDemoPlots();
                childfrmDemoPlots.MdiParent = this;
                childfrmDemoPlots.Show();
            }
        }

        private void msbTrans_Foundation_EyeSurgeryForm_Click(object sender, EventArgs e)
        {
            if (childEyeSurgeryPatientDetails == null || childEyeSurgeryPatientDetails.Text == "")
            {
                childEyeSurgeryPatientDetails = new EyeSurgeryPatientDetails();
                childEyeSurgeryPatientDetails.MdiParent = this;
                childEyeSurgeryPatientDetails.Show();
            }
        }

        private void msbTrans_Foundation_EyeSurgeryList_Click(object sender, EventArgs e)
        {
            if (childEyeSurgeryList == null || childEyeSurgeryList.Text == "")
            {
                childEyeSurgeryList = new EyeSurgeryList();
                childEyeSurgeryList.MdiParent = this;
                childEyeSurgeryList.Show();
            }
        }

        private void msbMasters_Stationary_AssetMas_Click(object sender, EventArgs e)
        {
            if (childFixedAssetMaster == null || childFixedAssetMaster.Text == "")
            {
                childFixedAssetMaster = new FixedAssetMaster();
                childFixedAssetMaster.MdiParent = this;
                childFixedAssetMaster.Show();
            }
        }

        private void msbTrans_AssetsMovement_Click(object sender, EventArgs e)
        {
            if (childFixedAssetsDetails == null || childFixedAssetsDetails.Text == "")
            {
                childFixedAssetsDetails = new FixedAssetsDetails();
                childFixedAssetsDetails.MdiParent = this;
                childFixedAssetsDetails.Show();
            }
        }

        private void msbTrans_HR_PayRoll_EmpLopEntry_Click(object sender, EventArgs e)
        {

            //if (childEmployeeLOPDetails == null || childEmployeeLOPDetails.Text == "")
            //{
            //    childEmployeeLOPDetails = new EmployeeLOPDetails();
            //    childEmployeeLOPDetails.MdiParent = this;
            //    childEmployeeLOPDetails.Show();
            //}

            if (childMonthlyAttendenceForEmployes == null || childMonthlyAttendenceForEmployes.Text == "")
            {
                childMonthlyAttendenceForEmployes = new MonthlyAttendenceForEmployes();
                childMonthlyAttendenceForEmployes.MdiParent = this;
                childMonthlyAttendenceForEmployes.Show();
            }
        }

        private void msbTrans_PartyEnroll_EnrollMember_Click(object sender, EventArgs e)
        {
            if (childMemberEnrollment == null || childMemberEnrollment.Text == "")
            {
                childMemberEnrollment = new MemberEnrollment();
                childMemberEnrollment.MdiParent = this;
                childMemberEnrollment.Show();
            }
        }

        private void custToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (childServiceReports == null || childServiceReports.Text == "")
            {
                childServiceReports = new ServiceReports("CHR");
                childServiceReports.MdiParent = this;
                childServiceReports.Show();
            }
        }

        private void msbTrans_CRM_Sale_Reciepts_Click(object sender, EventArgs e)
        {
            if (childSalesReceipt == null || childSalesReceipt.Text == "")
            {
                childSalesReceipt = new SalesReceipt();
                childSalesReceipt.MdiParent = this;
                childSalesReceipt.Show();
            }
        }

        private void msbTrans_BooksIssue_Click(object sender, EventArgs e)
        {

        }

        private void msbTrans_BooksIssue_TripSheetIssue_Click(object sender, EventArgs e)
        {
            if (childTripSheetIssue == null || childTripSheetIssue.Text == "")
            {
                childTripSheetIssue = new TripSheetIssue();
                childTripSheetIssue.MdiParent = this;
                childTripSheetIssue.Show();
            }
        }

        private void msbTrans_ServiceDept_ServiceActivities_Click(object sender, EventArgs e)
        {
            if (childServiceActivities == null || childServiceActivities.Text == "")
            {
                childServiceActivities = new ServiceActivities();
                childServiceActivities.MdiParent = this;
                childServiceActivities.Show();
            }
        }

        private void msbTrans_ServiceDept_SchoolActivities_Click(object sender, EventArgs e)
        {
            if (childfrmSchoolVisits == null || childfrmSchoolVisits.Text == "")
            {
                childfrmSchoolVisits = new frmSchoolVisits();
                childfrmSchoolVisits.MdiParent = this;
                childfrmSchoolVisits.Show();
            }
        }

        private void msbTrans_ServiceDept_ProdPromotions_Click(object sender, EventArgs e)
        {
            if (childfrmProductPromotion == null || childfrmProductPromotion.Text == "")
            {
                childfrmProductPromotion = new frmProductPromotion();
                childfrmProductPromotion.MdiParent = this;
                childfrmProductPromotion.Show();
            }
        }

        private void msbTrans_SystemInvent_IssueNew_Click(object sender, EventArgs e)
        {
            if (childNewSystemIssue == null || childNewSystemIssue.Text == "")
            {
                childNewSystemIssue = new NewSystemIssue();
                childNewSystemIssue.MdiParent = this;
                childNewSystemIssue.Show();
            }
        }

        private void msbTrans_SystemInvent_MovementReg_Click(object sender, EventArgs e)
        {
            if (childFixedAssetsDetails == null || childFixedAssetsDetails.Text == "")
            {
                childFixedAssetsDetails = new FixedAssetsDetails();
                childFixedAssetsDetails.MdiParent = this;
                childFixedAssetsDetails.Show();
            }
        }

        private void uploadPicstotableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UploadPicstoTable childUploadPicstoTable = new UploadPicstoTable();
            //childUploadPicstoTable.MdiParent = this;
            //childUploadPicstoTable.Show();
        }

        private void msbTrans_SystemInvent_RepairRepl_Click(object sender, EventArgs e)
        {
            if (childFixedAssetsServiceDetails == null || childFixedAssetsServiceDetails.Text == "")
            {
                childFixedAssetsServiceDetails = new FixedAssetsServiceDetails();
                childFixedAssetsServiceDetails.MdiParent = this;
                childFixedAssetsServiceDetails.Show();
            }
        }

        private void uploadPrintReport_Click(object sender, EventArgs e)
        {
            
            CommonData.ViewReport = "PRINT_SALES_APPT_LETTER";
            ReportViewer childReportViewer = new ReportViewer();
            childReportViewer.Show();
        }

        private void msbTrans_HR_Letters_PBLetters_Click(object sender, EventArgs e)
        {
            if (childPromotionBoardLetterPrinting == null || childPromotionBoardLetterPrinting.Text == "")
            {
                childPromotionBoardLetterPrinting = new PromotionBoardLetterPrinting();
                childPromotionBoardLetterPrinting.MdiParent = this;
                childPromotionBoardLetterPrinting.Show();
            }

        }

        private void msbMasters_HRISMasters_TrainingTypesMas_Click(object sender, EventArgs e)
        {
            if (childTrainingTopicsMaster == null || childTrainingTopicsMaster.Text == "")
            {
                childTrainingTopicsMaster = new TrainingTopicsMaster();
                childTrainingTopicsMaster.MdiParent = this;
                childTrainingTopicsMaster.Show();
            }
        }

        private void mbReports_IT_SysInventory_Detl_Click(object sender, EventArgs e)
        {
            if (CommonData.LogUserRole == "MANAGEMENT" || CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                chldReportGLSelection = new ReportGLSelection("IT_SYS_INV_CPU");
                chldReportGLSelection.MdiParent = this;
                chldReportGLSelection.Show();
            }
            else
            {
                chldfrmDoorknocks = new frmDoorknocks("IT_SYS_INV_CPU");
                chldfrmDoorknocks.MdiParent = this;
                chldfrmDoorknocks.Show();
            }
        }

        private void msbReports_CRM_Docm_GCGLSaleAccountbly_Click(object sender, EventArgs e)
        {
            frmDoorknocks chldfrmDoorknocks = new frmDoorknocks(19);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msb_Reports_HR_BioMetrics_LeaveDetails_Click(object sender, EventArgs e)
        {
            if (childAgentApprovalLetters == null || childAgentApprovalLetters.Text == "")
            {
                childAgentApprovalLetters = new AgentApprovalLetters(2);
                childAgentApprovalLetters.MdiParent = this;
                childAgentApprovalLetters.Show();
            }
        }

        private void msb_Reports_HR_BioMetrics_Attendence_Click(object sender, EventArgs e)
        {
            if (childHRAttendence == null || childHRAttendence.Text == "")
            {
                childHRAttendence = new HRAttendence("1");
                childHRAttendence.MdiParent = this;
                childHRAttendence.Show();
            }
        }

        private void msbTrans_LegalDept_LegalCaseMaster_Click(object sender, EventArgs e)
        {
            if (childLegalCaseDetails == null || childLegalCaseDetails.Text == "")
            {
                childLegalCaseDetails = new LeagalCaseDetails();
                childLegalCaseDetails.MdiParent = this;
                childLegalCaseDetails.Show();
            }
        }

        private void msbTrans_LegalDept_ComplainantMaster_Click(object sender, EventArgs e)
        {
            if (childComplainantMaster == null || childComplainantMaster.Text == "")
            {
                childComplainantMaster = new ComplainantMaster();
                childComplainantMaster.MdiParent = this;
                childComplainantMaster.Show();
            }
        }

        private void msbTrans_LegalDept_LawyerMaster_Click(object sender, EventArgs e)
        {
            if (childLawyerMaster == null || childLawyerMaster.Text == "")
            {
                childLawyerMaster = new LawyerMaster();
                childLawyerMaster.MdiParent = this;
                childLawyerMaster.Show();
            }
        }

        private void msbTrans_LegalDept_CaseHearings_Click(object sender, EventArgs e)
        {
            if (childCaseHearings == null || childCaseHearings.Text == "")
            {
                childCaseHearings = new CaseHearings();
                childCaseHearings.MdiParent = this;
                childCaseHearings.Show();
            }
        }

        private void mbReports_Legal_LegalCaseDetails_Click(object sender, EventArgs e)
        {
            if (childHRAttendence == null || childHRAttendence.Text == "")
            {
                childHRAttendence = new HRAttendence("2");
                childHRAttendence.MdiParent = this;
                childHRAttendence.Show();
            }
        }

        private void msbMasters_ServiceMas_CropMas_Click(object sender, EventArgs e)
        {
            if (childCropMaster == null || childCropMaster.Text == "")
            {
                childCropMaster = new CropMaster();
                childCropMaster.MdiParent = this;
                childCropMaster.Show();
            }
        }

        private void mbReports_Services_ScoolVisit_Summary_Click(object sender, EventArgs e)
        {
            frmDoorknocks chldfrmDoorknocks = new frmDoorknocks(20);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void mbReports_Services_ScoolVisit_Detailed_Click(object sender, EventArgs e)
        {
            if (childServiceReportByTrnNo == null || childServiceReportByTrnNo.Text == "")
            {
                childServiceReportByTrnNo = new ServiceReportByTrnNo(3);
                childServiceReportByTrnNo.MdiParent = this;
                childServiceReportByTrnNo.Show();
            }
        }

        private void mbReports_Services_FarmMeet_Summary_Click(object sender, EventArgs e)
        {
            childServiceReports = new ServiceReports("FARMER_MEET");
            childServiceReports.MdiParent = this;
            childServiceReports.Show();

        }

        private void mbReports_Services_DemoPlots_Summary_Click(object sender, EventArgs e)
        {
            childServiceReports = new ServiceReports("DEMO_PLOT");
            childServiceReports.MdiParent = this;
            childServiceReports.Show();
        }

        private void mbReports_Services_FarmMeet_Detailed_Click(object sender, EventArgs e)
        {
            if (childServiceReportByTrnNo == null || childServiceReportByTrnNo.Text == "")
            {
                childServiceReportByTrnNo = new ServiceReportByTrnNo(1);
                childServiceReportByTrnNo.MdiParent = this;
                childServiceReportByTrnNo.Show();
            }
        }

        private void mbReports_Services_DemoPlots_Detailed_Click(object sender, EventArgs e)
        {
            if (childServiceReportByTrnNo == null || childServiceReportByTrnNo.Text == "")
            {
                childServiceReportByTrnNo = new ServiceReportByTrnNo(2);
                childServiceReportByTrnNo.MdiParent = this;
                childServiceReportByTrnNo.Show();
            }
        }

        private void mbReports_Services_WCorFF_Click(object sender, EventArgs e)
        {
            frmDoorknocks chldfrmDoorknocks = new frmDoorknocks(23);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_CRM_Docm_GCGLCollectionStatement_Click(object sender, EventArgs e)
        {
            frmDoorknocks chldfrmDoorknocks = new frmDoorknocks(24);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbTrans_HR_PayRoll_EmpLoanDetl_LoanEntry_Click(object sender, EventArgs e)
        {
            if (childEmployeeLoanForm == null || childEmployeeLoanForm.Text == "")
            {
                childEmployeeLoanForm = new EmployeeLoanForm();
                childEmployeeLoanForm.MdiParent = this;
                childEmployeeLoanForm.Show();
            }
        }

        private void msbMasters_HRISMasters_LoanTypeMaster_Click(object sender, EventArgs e)
        {
            if (childLoanTypeMaster == null || childLoanTypeMaster.Text == "")
            {
                childLoanTypeMaster = new LoanTypeMaster();
                childLoanTypeMaster.MdiParent = this;
                childLoanTypeMaster.Show();
            }
        }

        private void msbReports_CRM_Docm_GCGLChecklist_Click(object sender, EventArgs e)
        {
            frmDoorknocks chldfrmDoorknocks = new frmDoorknocks(25);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void mbReports_Services_DAR_Click(object sender, EventArgs e)
        {
            if (childServicesDailyActivitiesPrint == null || childServicesDailyActivitiesPrint.Text == "")
            {
                childServicesDailyActivitiesPrint = new ServicesDailyActivitiesPrint();
                childServicesDailyActivitiesPrint.MdiParent = this;
                childServicesDailyActivitiesPrint.Show();
            }
        }

        private void msbTrans_HR_PromotionBoard_AgentApptLetApproval_Click(object sender, EventArgs e)
        {
            if (childPromotionBoardApproval == null || childPromotionBoardApproval.Text == "")
            {
                childPromotionBoardApproval = new PromotionBoardApproval(1);
                childPromotionBoardApproval.MdiParent = this;
                childPromotionBoardApproval.Show();
            }
        }

        private void msbReports_MIS_BranchBultChecklist_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(26);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void HR_CollAttendance_Click(object sender, EventArgs e)
        {
            if (childCollectedAttendence == null || childCollectedAttendence.Text == "")
            {
                childCollectedAttendence = new CollectedAttendence();
                childCollectedAttendence.MdiParent = this;
                childCollectedAttendence.Show();
            }
        }

        private void msbReports_CRM_Docm_GCGLChecklistGCOnly_Click(object sender, EventArgs e)
        {
            frmDoorknocks chldfrmDoorknocks = new frmDoorknocks(27);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void otherAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mbReports_IT_OtherAssets_Detailed_Click(object sender, EventArgs e)
        {
            if (childFixedAssetsPrint == null || childFixedAssetsPrint.Text == "")
            {
                childFixedAssetsPrint = new FixedAssetsPrint(1);
                childFixedAssetsPrint.MdiParent = this;
                childFixedAssetsPrint.Show();
            }
        }

        private void msbMasters_HRISMasters_SalStrMas_Click(object sender, EventArgs e)
        {
            if (childSalaryStructureMaster == null || childSalaryStructureMaster.Text == "")
            {
                childSalaryStructureMaster = new SalaryStructureMaster();
                childSalaryStructureMaster.MdiParent = this;
                childSalaryStructureMaster.Show();
            }
        }

        private void msbTrans_ServiceDept_HierarchyMap_BranchAboveMap_Click(object sender, EventArgs e)
        {
            if (childServiceAboveBranchLevelMapping == null || childServiceAboveBranchLevelMapping.Text == "")
            {
                childServiceAboveBranchLevelMapping = new ServiceAboveBranchLevelMapping();
                childServiceAboveBranchLevelMapping.MdiParent = this;
                childServiceAboveBranchLevelMapping.Show();
            }
        }

        private void msbTrans_SystemInvent_SysConfDetl_Click(object sender, EventArgs e)
        {
            if (childSystemConfigaration == null || childSystemConfigaration.Text == "")
            {
                childSystemConfigaration = new SystemConfigaration();
                childSystemConfigaration.MdiParent = this;
                childSystemConfigaration.Show();
            }
        }

        private void mbReports_IT_OtherAssets_Summary_Click(object sender, EventArgs e)
        {
            if (childFixedAssetsPrint == null || childFixedAssetsPrint.Text == "")
            {
                childFixedAssetsPrint = new FixedAssetsPrint(2);
                childFixedAssetsPrint.MdiParent = this;
                childFixedAssetsPrint.Show();
            }
        }

        private void msb_Reports_HR_BioMetrics_DailyAttendance_Click(object sender, EventArgs e)
        {
            if (childDailyAttendence == null || childDailyAttendence.Text == "")
            {
                childDailyAttendence = new DailyAttendence();
                childDailyAttendence.MdiParent = this;
                childDailyAttendence.Show();
            }
        }

        private void msbTrans_ServiceDept_InvDetails_Search_Click(object sender, EventArgs e)
        {
            if (childCROCustomerSearch == null || childCROCustomerSearch.Text == "")
            {
                childCROCustomerSearch = new CROCustomerSearch();
                childCROCustomerSearch.MdiParent = this;
                childCROCustomerSearch.Show();
            }
        }

        private void msbTrans_IT_MobileNos_Master_Click(object sender, EventArgs e)
        {
            if (childMobileNoMaster == null || childMobileNoMaster.Text == "")
            {
                childMobileNoMaster = new MobileNoMaster();
                childMobileNoMaster.MdiParent = this;
                childMobileNoMaster.Show();
            }
        }

        private void mbReports_IT_OtherAssets_MobileNo_Details_Click(object sender, EventArgs e)
        {
            CommonData.ViewReport = "MOBILE_NO_DETAILS";
            ReportViewer childReportViewer = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), "");
            childReportViewer.Show();
        }

        private void msb_Reports_HR_EmpInfo_ContactDetails_BirthMarrEvents_Click(object sender, EventArgs e)
        {
            if (childHRAttendence == null || childHRAttendence.Text == "")
            {
                childHRAttendence = new HRAttendence("3");
                childHRAttendence.MdiParent = this;
                childHRAttendence.Show();
            }
        }

        private void msbTrans_IT_MobileNos_MonthlyBills_Click(object sender, EventArgs e)
        {
            if (childMobileNoMonthlyBills == null || childMobileNoMonthlyBills.Text == "")
            {
                childMobileNoMonthlyBills = new MobileNoMonthlyBills();
                childMobileNoMonthlyBills.MdiParent = this;
                childMobileNoMonthlyBills.Show();
            }
        }

        private void mbReports_IT_OtherAssets_MobileNo_BillDetls_Click(object sender, EventArgs e)
        {
            if (chldfrmDoorknocks == null || chldfrmDoorknocks.Text == "")
            {
                chldfrmDoorknocks = new frmDoorknocks(28);
                chldfrmDoorknocks.MdiParent = this;
                chldfrmDoorknocks.Show();
            }
        }

        private void msbTrans_HR_Misconduct_TrainingProgramms_Planning_Click(object sender, EventArgs e)
        {            
                childTrainingPlanningDetails = new TrainingPlanningDetails("TR");
                childTrainingPlanningDetails.MdiParent = this;
                childTrainingPlanningDetails.Show();            
        }

        private void msbTrans_HR_Misconduct_TrainingProgramms_Actuals_Click(object sender, EventArgs e)
        {            
                childTrainingProgramDetails = new TrainingProgramDetails("TR");
                childTrainingProgramDetails.MdiParent = this;
                childTrainingProgramDetails.Show();            
        }

        private void msbProcess_Sales_GCGLLocking_Click(object sender, EventArgs e)
        {
            if (childSalesDataMonthClosing == null || childSalesDataMonthClosing.Text == "")
            {
                childSalesDataMonthClosing = new SalesDataMonthClosing("LOCK");
                childSalesDataMonthClosing.MdiParent = this;
                childSalesDataMonthClosing.Show();
            }
        }

        private void msbProcess_Sales_GCGLUnLocking_Click(object sender, EventArgs e)
        {
            if (childSalesDataMonthClosing == null || childSalesDataMonthClosing.Text == "")
            {
                childSalesDataMonthClosing = new SalesDataMonthClosing("UNLOCK");
                childSalesDataMonthClosing.MdiParent = this;
                childSalesDataMonthClosing.Show();
            }
        }

        private void msbTrans_IT_MobileNos_BillsByNo_Click(object sender, EventArgs e)
        {
            if (childMobileNoBillDtails == null || childMobileNoBillDtails.Text == "")
            {
                childMobileNoBillDtails = new MobileNoBillDtails();
                childMobileNoBillDtails.MdiParent = this;
                childMobileNoBillDtails.Show();
            }
        }

        private void msbTrans_HR_PayRoll_EmpLoanRecovery_Click(object sender, EventArgs e)
        {
            if (childfrmEmployeeLoanRecovery == null || childfrmEmployeeLoanRecovery.Text == "")
            {
                childfrmEmployeeLoanRecovery = new frmEmployeeLoanRecovery();
                childfrmEmployeeLoanRecovery.MdiParent = this;
                childfrmEmployeeLoanRecovery.Show();
            }
        }

        private void msb_Reports_HR_PayRoll_LoanDetls_Click(object sender, EventArgs e)
        {
            CommonData.ViewReport = "LOAN_DETAILS_ALL";
            ReportViewer childReportViewer = new ReportViewer(CommonData.CompanyCode.ToString(), CommonData.BranchCode.ToString(), "");
            childReportViewer.Show();
        }

        private void msbMasters_Products_CombiSplitting_Click(object sender, EventArgs e)
        {
            if (childfrmCombiSplitting == null || childfrmCombiSplitting.Text == "")
            {
                childfrmCombiSplitting = new frmCombiSplitting();
                childfrmCombiSplitting.MdiParent = this;
                childfrmCombiSplitting.Show();
            }
        }

        private void msbReport_CRM_SaleRegSplliting_Click(object sender, EventArgs e)
        {
            frmDoorknocks chldfrmDoorknocks = new frmDoorknocks(29);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbTrans_ServiceDept_TourExp_Click(object sender, EventArgs e)
        {
            if (childServicesTourExpenses == null || childServicesTourExpenses.Text == "")
            {
                childServicesTourExpenses = new ServicesTourExpenses();
                childServicesTourExpenses.MdiParent = this;
                childServicesTourExpenses.Show();
            }
        }

        private void msbTrans_HR_PayRoll_EmpTdsEntry_Click(object sender, EventArgs e)
        {
            if (childEmployeeTDSForm == null || childEmployeeTDSForm.Text == "")
            {
                childEmployeeTDSForm = new EmployeeTDSForm();
                childEmployeeTDSForm.MdiParent = this;
                childEmployeeTDSForm.Show();
            }
        }

        private void msbReports_MIS_EmpPerfm_AssetVsPer_Click(object sender, EventArgs e)
        {
            if (childrepCompBranchAssetEmpSelection == null || childrepCompBranchAssetEmpSelection.Text == "")
            {
                childrepCompBranchAssetEmpSelection = new repCompBranchAssetEmpSelection();
                childrepCompBranchAssetEmpSelection.MdiParent = this;
                childrepCompBranchAssetEmpSelection.Show();
            }
        }

        private void msb_Reports_HR_PayRoll_Reports_Click(object sender, EventArgs e)
        {
            if (childPayRollReports == null || childPayRollReports.Text == "")
            {
                childPayRollReports = new PayRollReports();
                childPayRollReports.MdiParent = this;
                childPayRollReports.Show();
            }
        }

        private void msbMasters_HRISMasters_PayRollMas_ESI_Click(object sender, EventArgs e)
        {
            if (childESIMaster == null || childESIMaster.Text == "")
            {
                childESIMaster = new ESIMaster();
                childESIMaster.MdiParent = this;
                childESIMaster.Show();
            }
        }

        private void msbMasters_HRISMasters_PayRollMas_PF_Click(object sender, EventArgs e)
        {
            if (childPfMaster == null || childPfMaster.Text == "")
            {
                childPfMaster = new PfMaster();
                childPfMaster.MdiParent = this;
                childPfMaster.Show();
            }
        }

        private void msbMasters_HRISMasters_PayRollMas_BankAcc_Click(object sender, EventArgs e)
        {
            if (childBankMaster == null || childBankMaster.Text == "")
            {
                childBankMaster = new BankMaster();
                childBankMaster.MdiParent = this;
                childBankMaster.Show();
            }
        }

        private void msbProcess_HR_Payroll_AttndProc_Click(object sender, EventArgs e)
        {
            if (childAttendenceProcess == null || childAttendenceProcess.Text == "")
            {
                childAttendenceProcess = new AttendenceProcess();
                childAttendenceProcess.MdiParent = this;
                childAttendenceProcess.Show();
            }
        }

        private void msbProcess_HR_Payroll_PayRollProc_Click(object sender, EventArgs e)
        {
            if (childPayRollProcess == null || childPayRollProcess.Text == "")
            {
                childPayRollProcess = new PayRollProcess();
                childPayRollProcess.MdiParent = this;
                childPayRollProcess.Show();
            }
        }

        private void msbProcess_HR_Payroll_RollBkPrcs_Click(object sender, EventArgs e)
        {
            if (childRollBackProcess == null || childRollBackProcess.Text == "")
            {
                childRollBackProcess = new RollBackProcess();
                childRollBackProcess.MdiParent = this;
                childRollBackProcess.Show();
            }
        }

        private void msbTrans_HR_PayRoll_Attnd_InOffce_Click(object sender, EventArgs e)
        {
            if (childMonthlyAttendenceForEmployes == null || childMonthlyAttendenceForEmployes.Text == "")
            {
                childMonthlyAttendenceForEmployes = new MonthlyAttendenceForEmployes();
                childMonthlyAttendenceForEmployes.MdiParent = this;
                childMonthlyAttendenceForEmployes.Show();
            }
        }

        private void msbTrans_HR_PayRoll_Attnd_OtherBR_Click(object sender, EventArgs e)
        {
            if (childMonthlyAttendanceForOtherBranches == null || childMonthlyAttendanceForOtherBranches.Text == "")
            {
                childMonthlyAttendanceForOtherBranches = new MonthlyAttendanceForOtherBranches();
                childMonthlyAttendanceForOtherBranches.MdiParent = this;
                childMonthlyAttendanceForOtherBranches.Show();
            }
        }

        private void msbTrans_HR_PayRoll_StopPay_Click(object sender, EventArgs e)
        {
            if (childStopSalaryPayment == null || childStopSalaryPayment.Text == "")
            {
                childStopSalaryPayment = new StopSalaryPayment();
                childStopSalaryPayment.MdiParent = this;
                childStopSalaryPayment.Show();
            }
        }

        private void msbTrans_HR_PayRoll_PayRollBR_Click(object sender, EventArgs e)
        {
            if (childPayRollBranch == null || childPayRollBranch.Text == "")
            {
                childPayRollBranch = new PayRollBranch();
                childPayRollBranch.MdiParent = this;
                childPayRollBranch.Show();
            }
        }

        private void msbTrans_HR_PayRoll_TckRest_Click(object sender, EventArgs e)
        {
            if (childEmpTicketRestaurantForm == null || childEmpTicketRestaurantForm.Text == "")
            {
                childEmpTicketRestaurantForm = new EmpTicketRestaurantForm();
                childEmpTicketRestaurantForm.MdiParent = this;
                childEmpTicketRestaurantForm.Show();
            }
        }

        private void msbTrans_HR_PayRoll_EmpVsLoanRecovery_Click(object sender, EventArgs e)
        {
            if (childEmployeeWiseLoanRecovery == null || childEmployeeWiseLoanRecovery.Text == "")
            {
                childEmployeeWiseLoanRecovery = new EmployeeWiseLoanRecovery();
                childEmployeeWiseLoanRecovery.MdiParent = this;
                childEmployeeWiseLoanRecovery.Show();
            }
        }

        private void msb_Reports_HR_PayRoll_LoanRecStatement_Click(object sender, EventArgs e)
        {
            if (chldfrmDoorknocks == null || chldfrmDoorknocks.Text == "")
            {
                chldfrmDoorknocks = new frmDoorknocks(30);
                chldfrmDoorknocks.MdiParent = this;
                chldfrmDoorknocks.Show();
            }
        }

        private void msbTrans_HR_PayRoll_PayMode_Click(object sender, EventArgs e)
        {
            if (chldPaymentMode == null || chldPaymentMode.Text == "")
            {
                chldPaymentMode = new PaymentMode();
                chldPaymentMode.MdiParent = this;
                chldPaymentMode.Show();
            }
        }

        private void msb_Reports_HR_PB_EligForPB_Click(object sender, EventArgs e)
        {
            if (chldfrmDoorknocks == null || chldfrmDoorknocks.Text == "")
            {
                chldfrmDoorknocks = new frmDoorknocks(31);
                chldfrmDoorknocks.MdiParent = this;
                chldfrmDoorknocks.Show();
            }
        }

        private void msb_Reports_HR_PB_NotEligForPB_Click(object sender, EventArgs e)
        {
            if (chldfrmDoorknocks == null || chldfrmDoorknocks.Text == "")
            {
                chldfrmDoorknocks = new frmDoorknocks(32);
                chldfrmDoorknocks.MdiParent = this;
                chldfrmDoorknocks.Show();
            }
        }

        private void msbTrans_ProductionUnits_PUDCR_Click(object sender, EventArgs e)
        {
            if (childPUDeliveryChallanReceipt == null || childPUDeliveryChallanReceipt.Text == "")
            {
                childPUDeliveryChallanReceipt = new PUDeliveryChallanReceipt();
                childPUDeliveryChallanReceipt.MdiParent = this;
                childPUDeliveryChallanReceipt.Show();
            }
        }

        private void msbTrans_StockPoints_SPDCR_Click(object sender, EventArgs e)
        {
            if (childStockPointDCR == null || childStockPointDCR.Text == "")
            {
                childStockPointDCR = new StockPointDCR();
                childStockPointDCR.MdiParent = this;
                childStockPointDCR.Show();
            }
        }

        private void msbReport_Production__GRN_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(18);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbMasters_HRISMasters_SalesElig_Click(object sender, EventArgs e)
        {
            if (childSalesPromotionEligibulityMaster == null || childSalesPromotionEligibulityMaster.Text == "")
            {
                childSalesPromotionEligibulityMaster = new SalesPromotionEligibulityMaster();
                childSalesPromotionEligibulityMaster.MdiParent = this;
                childSalesPromotionEligibulityMaster.Show();
            }
        }

        private void msbTrans_HR_Misconduct_TrainingProgramms_FeedBack_Click(object sender, EventArgs e)
        {
            if (childActualProgramsForFeedBack == null || childActualProgramsForFeedBack.Text == "")
            {
                childActualProgramsForFeedBack = new ActualProgramsForFeedBack();
                childActualProgramsForFeedBack.MdiParent = this;
                childActualProgramsForFeedBack.Show();
            }
        }

        private void msbReports_CRM_Docm_GCGLAcct_Click(object sender, EventArgs e)
        {
            frmDoorknocks chldfrmDoorknocks = new frmDoorknocks(33);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbTools_ImgEditor_Click(object sender, EventArgs e)
        {
            if (childImageBrowser == null || childImageBrowser.Text == "")
            {
                childImageBrowser = new ImageBrowser();
                childImageBrowser.MdiParent = this;
                childImageBrowser.Show();
            }
        }

        private void msbReport_Production_StkSummary_Click(object sender, EventArgs e)
        {
            //childEmpFromToSelection = new EmpFromToSelection(19);
            //childEmpFromToSelection.MdiParent = this;
            //childEmpFromToSelection.Show();

            //if (CommonData.LogUserRole == "MANAGEMENT" || CommonData.LogUserId.ToUpper() == "ADMIN")
            //{
            //    chldReportGLSelection = new ReportGLSelection("STOCKPOINT_RECONSILATION");
            //    chldReportGLSelection.MdiParent = this;
            //    chldReportGLSelection.Show();
            //}
            //else
            //{
                chldfrmDoorknocks = new frmDoorknocks("STOCKPOINT_RECONSILATION");
                chldfrmDoorknocks.MdiParent = this;
                chldfrmDoorknocks.Show();
            ////}
        }

        private void msbTrans_Audit_AuditQuery_Click(object sender, EventArgs e)
        {
            if (childMisconductForm == null || childMisconductForm.Text == "")
            {
                childMisconductForm = new MisconductForm();
                childMisconductForm.MdiParent = this;
                childMisconductForm.Show();
            }
        }

        private void msbTrans_HR_MasterEdit_PFDocUpload_Click(object sender, EventArgs e)
        {
            if (childPFUANMaster == null || childPFUANMaster.Text == "")
            {
                childPFUANMaster = new PFUANMaster();
                childPFUANMaster.MdiParent = this;
                childPFUANMaster.Show();
            }
        }

        private void msbTrans_Audit_AuditQueryBranch_Click(object sender, EventArgs e)
        {
            if (childAuditMisconductBranch == null || childAuditMisconductBranch.Text == "")
            {
                childAuditMisconductBranch = new AuditMisconductBranch();
                childAuditMisconductBranch.MdiParent = this;
                childAuditMisconductBranch.Show();
            }
        }

        private void mbReports_Audit_Reg_Click(object sender, EventArgs e)
        {
            //childCompBranchMonthForReport = new CompBranchMonthForReport(4);
            //childCompBranchMonthForReport.MdiParent = this;
            //childCompBranchMonthForReport.Show();
            AuditReportsFilters chldAuditReportsFilters = new AuditReportsFilters(2);
            chldAuditReportsFilters.MdiParent = this;
            chldAuditReportsFilters.Show();
        }

        private void msbTrans_AssetInv_AssetIssue_Click(object sender, EventArgs e)
        {
            if (childNewAssetIssue == null || childNewAssetIssue.Text == "")
            {
                childNewAssetIssue = new NewAssetIssue();
                childNewAssetIssue.MdiParent = this;
                childNewAssetIssue.Show();
            }
        }

        private void mbReports_Services_ConsolidationRep_Click(object sender, EventArgs e)
        {
            chldReportGLSelection = new ReportGLSelection("SERVICE_CONSOLIDATION");
            chldReportGLSelection.MdiParent = this;
            chldReportGLSelection.Show();
        }

        private void mbManager_ManageUserBranch_Click(object sender, EventArgs e)
        {
            if (childAboveBranchLevelUserBranches == null || childAboveBranchLevelUserBranches.Text == "")
            {
                childAboveBranchLevelUserBranches = new AboveBranchLevelUserBranches();
                childAboveBranchLevelUserBranches.MdiParent = this;
                childAboveBranchLevelUserBranches.Show();
            }
        }

        private void msbReport_Production_DCR_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(23);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbReports_HR_Recruitement_BranchWiseSummary_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(34);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_MIS_CompanyWise_Groups_Click(object sender, EventArgs e)
        {
            childCompBranchMonthForReport = new CompBranchMonthForReport(5);
            childCompBranchMonthForReport.MdiParent = this;
            childCompBranchMonthForReport.Show();
        }

        private void msbMaster_General_RentalAgrmnts_Click(object sender, EventArgs e)
        {
            if (childSPRentalAgriments == null || childSPRentalAgriments.Text == "")
            {
                childSPRentalAgriments = new SPRentalAgriments();
                childSPRentalAgriments.MdiParent = this;
                childSPRentalAgriments.Show();
            }
        }

        private void msb_Reports_HR_BranchInfo_Mas_Click(object sender, EventArgs e)
        {
            if (childrepCompBranchTypeSelection == null || childrepCompBranchTypeSelection.Text == "")
            {
                childrepCompBranchTypeSelection = new repCompBranchTypeSelection();
                childrepCompBranchTypeSelection.MdiParent = this;
                childrepCompBranchTypeSelection.Show();
            }

            //childReportViewer = new ReportViewer("ALL", "ALL", "ALL", "ALL");
            //CommonData.ViewReport = "SSERP_REP_BRANCH_MASTER";
            //childReportViewer.Show();
        }

        private void msbReports_CRM_PetrolAllwExpList_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(35);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbMasters_HRISMasters_ShiftMaster_Click(object sender, EventArgs e)
        {
            if (childUpdateShiftMaster == null || childUpdateShiftMaster.Text == "")
            {
                childUpdateShiftMaster = new UpdateShiftMaster();
                childUpdateShiftMaster.MdiParent = this;
                childUpdateShiftMaster.Show();
            }
        }

        private void msbTrans_Audit_DRPlanning_Click(object sender, EventArgs e)
        {
            if (childAuditDRPlanning == null || childAuditDRPlanning.Text == "")
            {
                childAuditDRPlanning = new AuditDRPlanning();
                childAuditDRPlanning.MdiParent = this;
                childAuditDRPlanning.Show();
            }
        }

        private void msbTrans_Audit_Mapping_Click(object sender, EventArgs e)
        {
            if (childAuditHierarchyMapping == null || childAuditHierarchyMapping.Text == "")
            {
                childAuditHierarchyMapping = new AuditHierarchyMapping();
                childAuditHierarchyMapping.MdiParent = this;
                childAuditHierarchyMapping.Show();
            }
        }

        private void msbReports_CRM_StockDetails_SPGRNToEmp_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(20);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbTrans_HR_ApplEntry_AgentRejoinBR_Click(object sender, EventArgs e)
        {
            if (childPrevEmployee == null || childPrevEmployee.Text == "")
            {
                childPrevEmployee = new PrevEmployee(2);
                childPrevEmployee.MdiParent = this;
                childPrevEmployee.Show();
            }
        }

        private void msbTrans_CRM_Sale_ShortWriteOff_Click(object sender, EventArgs e)
        {
            if (childShortageStockDetails == null || childShortageStockDetails.Text == "")
            {
                childShortageStockDetails = new ShortageStockDetails();
                childShortageStockDetails.MdiParent = this;
                childShortageStockDetails.Show();
            }
        }

        private void msbTrans_HR_MasterEdit_AdharMas_Click(object sender, EventArgs e)
        {
            if (childAdharMaster == null || childAdharMaster.Text == "")
            {
                childAdharMaster = new AdharMaster();
                childAdharMaster.MdiParent = this;
                childAdharMaster.Show();
            }
        }

        private void mbReports_Audit_Recovery_Click(object sender, EventArgs e)
        {
            AuditReportsFilters chldAuditReportsFilters = new AuditReportsFilters(1);
            chldAuditReportsFilters.MdiParent = this;
            chldAuditReportsFilters.Show();
        }

        private void mbReports_Audit_Summary_Click(object sender, EventArgs e)
        {
            AuditReportsFilters chldAuditReportsFilters = new AuditReportsFilters(0);
            chldAuditReportsFilters.MdiParent = this;
            chldAuditReportsFilters.Show();
        }

        private void msbTrans_StockPoints_ShortageWriteOff_Click(object sender, EventArgs e)
        {
            if (childShortageStockDetails == null || childShortageStockDetails.Text == "")
            {
                childShortageStockDetails = new ShortageStockDetails();
                childShortageStockDetails.MdiParent = this;
                childShortageStockDetails.Show();
            }
        }

        private void msbTrans_ProductionUnits_ShortageWriteOff_Click(object sender, EventArgs e)
        {
            if (childShortageStockDetails == null || childShortageStockDetails.Text == "")
            {
                childShortageStockDetails = new ShortageStockDetails();
                childShortageStockDetails.MdiParent = this;
                childShortageStockDetails.Show();
            }
        }

        private void msbTrans_HR_PayRollBr_Attnd_BiomUpDate_Click(object sender, EventArgs e)
        {
            if (childUploadBiometricsData_BR == null || childUploadBiometricsData_BR.Text == "")
            {
                childUploadBiometricsData_BR = new UploadBiometricsData_BR();
                childUploadBiometricsData_BR.MdiParent = this;
                childUploadBiometricsData_BR.Show();
            }
        }

        private void msb_Reports_HR_EmpInfo_ContactDetails_SalBankAcc_Click(object sender, EventArgs e)
        {
            childCompBranchMonthForReport = new CompBranchMonthForReport(6);
            childCompBranchMonthForReport.MdiParent = this;
            childCompBranchMonthForReport.Show();
        }

        private void msbReports_CRM_StockDetails_StockShortage_Click(object sender, EventArgs e)
        {
            childSalesRegReportForm = new SalesRegister(3);
            childSalesRegReportForm.MdiParent = this;
            childSalesRegReportForm.Show();
        }

        private void msbTrans_HR_PayRollBr_Attnd_MnthlyAttd_Click(object sender, EventArgs e)
        {
            if (childMonthlyAttdProcess == null || childMonthlyAttdProcess.Text == "")
            {
                childMonthlyAttdProcess = new MonthlyAttdProcess();
                childMonthlyAttdProcess.MdiParent = this;
                childMonthlyAttdProcess.Show();
            }
        }

        private void msb_Reports_HR_TrningPrgs_TrnerRep_Click(object sender, EventArgs e)
        {
            if (childTrainingReports == null || childTrainingReports.Text == "")
            {
                childTrainingReports = new TrainingReports();
                childTrainingReports.MdiParent = this;
                childTrainingReports.Show();
            }

        }

        private void msb_Reports_HR_TrningPrgs_Summ_Click(object sender, EventArgs e)
        {
            childCompBranchMonthForReport = new CompBranchMonthForReport(8);
            childCompBranchMonthForReport.MdiParent = this;
            childCompBranchMonthForReport.Show();
        }

        private void mbReports_Audit_Org_Chart_Click(object sender, EventArgs e)
        {
            frmDoorknocks chldfrmDoorknocks = new frmDoorknocks(37);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbTrans_ServiceDept_HierarchyMap_Click(object sender, EventArgs e)
        {
            if (childServiceHierarchyMapping == null || childServiceHierarchyMapping.Text == "")
            {
                childServiceHierarchyMapping = new ServiceHierarchyMapping();
                childServiceHierarchyMapping.MdiParent = this;
                childServiceHierarchyMapping.Show();
            }
        }

        private void msbReports_MIS_StockPoints_StockRec_Click(object sender, EventArgs e)
        {
            childCompBranchMonthForReport = new CompBranchMonthForReport(6);
            childCompBranchMonthForReport.MdiParent = this;
            childCompBranchMonthForReport.Show();
        }

        private void msbReport_StockPoint_SPReffStmt_Click(object sender, EventArgs e)
        {
            if (CommonData.LogUserId.ToUpper() == "ADMIN" || CommonData.LogUserRole == "MANAGEMENT")
            {
                childRepDateCompBranchSelection = new RepDateCompBranchSelection("RF_STATEMENT");
                childRepDateCompBranchSelection.MdiParent = this;
                childRepDateCompBranchSelection.Show();
            }
            else
            {
                childDamaged_Stock_Received_Details = new Damaged_Stock_Received_Details(2);
                childDamaged_Stock_Received_Details.MdiParent = this;
                childDamaged_Stock_Received_Details.Show();
            }
        }

        private void msbReport_StockPoint_SPDmgGRNDetl_Click(object sender, EventArgs e)
        {
            //if (childServiceHierarchyMapping == null || childServiceHierarchyMapping.Text == "")
            //{
            //    childDamaged_Stock_Received_Details = new Damaged_Stock_Received_Details(1);
            //    childDamaged_Stock_Received_Details.MdiParent = this;
            //    childDamaged_Stock_Received_Details.Show();
            //}
        }

        private void msbReport_CRM_Sale_SSB_GCGLProdSales1_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(38);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReport_CRM_Sale_SSB_GCGLProdSales2_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(39);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_CRM_Stationary_GRNReg_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(8);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbReports_MIS_StockPoints_LowDispSps_Click(object sender, EventArgs e)
        {
            childRepDateCompBranchSelection = new RepDateCompBranchSelection("LOW_DISP_SP");
            childRepDateCompBranchSelection.MdiParent = this;
            childRepDateCompBranchSelection.Show();
        }

        private void msbReport_StockPoint_ShrtgWrtffExc_Click(object sender, EventArgs e)
        {
            childRepDateCompBranchSelection = new RepDateCompBranchSelection("SHORTAGE_WRITEOFF");
            childRepDateCompBranchSelection.MdiParent = this;
            childRepDateCompBranchSelection.Show();

            //childSalesRegReportForm = new SalesRegister(3);
            //childSalesRegReportForm.MdiParent = this;
            //childSalesRegReportForm.Show();
        }

        private void msbReport_Production_ShrtgWrtfExcs_Click(object sender, EventArgs e)
        {
            childSalesRegReportForm = new SalesRegister(3);
            childSalesRegReportForm.MdiParent = this;
            childSalesRegReportForm.Show();
        }

        private void msbReport_Production_StkLdgr_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(10);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbTrans_StockPoints_TripSheet_Click(object sender, EventArgs e)
        {
            if (childTripSheet == null || childTripSheet.Text == "")
            {
                childTripSheet = new TripSheet();
                childTripSheet.MdiParent = this;
                childTripSheet.Show();
            }
        }

        private void msbTrans_ProductionUnits_TripSheet_Click(object sender, EventArgs e)
        {
            if (childTripSheet == null || childTripSheet.Text == "")
            {
                childTripSheet = new TripSheet();
                childTripSheet.MdiParent = this;
                childTripSheet.Show();
            }
        }

        private void msbReports_HR_Strengths_EmpList_Click(object sender, EventArgs e)
        {
            if (childEmployeeList == null || childEmployeeList.Text == "")
            {
                childEmployeeList = new EmployeeList();
                childEmployeeList.MdiParent = this;
                childEmployeeList.Show();
            }
        }

        private void msbReports_HR_Strengths_OtherStaff_Click(object sender, EventArgs e)
        {
            CommonData.ViewReport = "SSERP_REP_STAFF_DETAILS";
            childReportViewer = new ReportViewer(CommonData.CompanyCode, CommonData.BranchCode, CommonData.DocMonth, "");
            childReportViewer.Show();
        }

        private void msbReport_StockPoint_SP_Rental_Agriments_Details_Click(object sender, EventArgs e)
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
            

            CommonData.ViewReport = "SSERP_REP_SP_RENTAL_AGRIMENT_DETAILS";
            childReportViewer = new ReportViewer(strComp, strBranch, "", "", "", "");
            childReportViewer.Show();
        }

        private void msbReports_MIS_StockPoints_OutStandRep_Click(object sender, EventArgs e)
        {

            childRepDateCompBranchSelection = new RepDateCompBranchSelection("STOCK_SUMMARY");
            childRepDateCompBranchSelection.MdiParent = this;
            childRepDateCompBranchSelection.Show();

        }

        private void msbReport_StockPoint_PendingDcs_Click(object sender, EventArgs e)
        {
            if (CommonData.LogUserRole == "MANAGEMENT" || CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                childRepDateCompBranchSelection = new RepDateCompBranchSelection("SP_PENDING_DC");
                childRepDateCompBranchSelection.MdiParent = this;
                childRepDateCompBranchSelection.Show();
            }
            else
            {
                chldfrmDoorknocks = new frmDoorknocks("SP_PENDING_DC");
                chldfrmDoorknocks.MdiParent = this;
                chldfrmDoorknocks.Show();
            }
        }

        private void msbTrans_HR_MasterEdit_EmpInfoView_Click(object sender, EventArgs e)
        {
            if (childfrmEmployeeInfo == null || childfrmEmployeeInfo.Text == "")
            {
                childfrmEmployeeInfo = new frmEmployeeInfo();
                childfrmEmployeeInfo.MdiParent = this;
                childfrmEmployeeInfo.Show();
            }
        }

        private void msbReports_HR_Recruitement_BrWiseRecVsResgn_Click(object sender, EventArgs e)
        {
            frmDoorknocks chldfrmDoorknocks = new frmDoorknocks(41);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();

        }

        private void msbTrans_HR_PayRoll_SalDed_Click(object sender, EventArgs e)
        {
            if (childLoanDeductions == null || childLoanDeductions.Text == "")
            {
                childLoanDeductions = new LoanDeductions();
                childLoanDeductions.MdiParent = this;
                childLoanDeductions.Show();
            }
        }

        private void mbReports_IT_OtherAssets_Branch_Wise_Click(object sender, EventArgs e)
        {
            childFixedAssetsPrint = new FixedAssetsPrint(3);
            childFixedAssetsPrint.MdiParent = this;
            childFixedAssetsPrint.Show();
        }

        private void msbReports_MIS_StockPoints_RF_Summary_Click(object sender, EventArgs e)
        {
            childRepDateCompBranchSelection = new RepDateCompBranchSelection("RF_SUMMARY");
            childRepDateCompBranchSelection.MdiParent = this;
            childRepDateCompBranchSelection.Show();

        }

        private void msbMasters_FA_CC_MC_Click(object sender, EventArgs e)
        {
            if (childMajorCostCenter_Details == null || childMajorCostCenter_Details.Text == "")
            {
                childMajorCostCenter_Details = new MajorCostCenter_Details();
                childMajorCostCenter_Details.MdiParent = this;
                childMajorCostCenter_Details.Show();
            }
        }

        private void msbTrans_FA_PaymentVoucher_Click(object sender, EventArgs e)
        {
            if (childPaymentVoucher == null || childPaymentVoucher.Text == "")
            {
                childPaymentVoucher = new PaymentVouchers();
                childPaymentVoucher.MdiParent = this;
                childPaymentVoucher.Show();
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            FingerDemo childForm = new FingerDemo();
                childForm.MdiParent = this;
                childForm.Show();
            
        }

        private void mbReports_Services_Serv_Activity_Reg_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(22);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show(); 

        }

        private void msbReports_MIS_ServiceReports_BranchWise_Sum_Click(object sender, EventArgs e)
        {
            childCompBranchMonthForReport = new CompBranchMonthForReport(10);
            childCompBranchMonthForReport.MdiParent = this;
            childCompBranchMonthForReport.Show();
        }

        private void msbReports_MIS_SBS_AllIndRep_Click(object sender, EventArgs e)
        {
            if (childBranchReportFilter == null || childBranchReportFilter.Text == "")
            {
                childBranchReportFilter = new BranchReportFilter();
                childBranchReportFilter.MdiParent = this;
                childBranchReportFilter.Show();
            }
        }

        private void msbReports_MIS_ServiceReports_Company_Wise_Sum_Click(object sender, EventArgs e)
        {
            childCompAndMonthForReport = new CompAndMonthForReport(0);
            childCompAndMonthForReport.MdiParent = this;
            childCompAndMonthForReport.Show();
        }

        private void msbTrans_HR_MasterEdit_EmpFingEnrl_Click(object sender, EventArgs e)
        {
            if (childEmployeeFingerEnroll == null || childEmployeeFingerEnroll.Text == "")
            {
                childEmployeeFingerEnroll = new EmployeeFingerEnroll();
                childEmployeeFingerEnroll.MdiParent = this;
                childEmployeeFingerEnroll.Show();
            }
        }

        private void msbReports_MIS_SBS_AllIndia_Toppers_Click(object sender, EventArgs e)
        {
            childReportFilters = new ReportFilters("ALL_INDIA_TOP");
            childReportFilters.MdiParent = this;
            childReportFilters.Show();
        }

        private void msbReports_MIS_BRPer_AFCRecStmtMnthly_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(42);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_MIS_BRPer_AFCRecStmtYearly_Click(object sender, EventArgs e)
        {
            childCompBranchMonthForReport = new CompBranchMonthForReport(11);
            childCompBranchMonthForReport.MdiParent = this;
            childCompBranchMonthForReport.Show();
        }

        private void mbReports_FA_PaymentReg_Click(object sender, EventArgs e)
        {
            if (childHRAttendence == null || childHRAttendence.Text == "")
            {
                childHRAttendence = new HRAttendence("4");
                childHRAttendence.MdiParent = this;
                childHRAttendence.Show();
            }
        }

        private void msbMasters_FA_CA_Click(object sender, EventArgs e)
        {

            if (childChartOfAccounts == null || childChartOfAccounts.Text == "")
            {
                childChartOfAccounts = new ChartOfAccounts();
                childChartOfAccounts.MdiParent = this;
                childChartOfAccounts.Show();
            }
        }

        private void msbTrans_CRM_DevSpCir_SpApprs_Click(object sender, EventArgs e)
        {
            if (childfrmSpecialApprovals == null || childfrmSpecialApprovals.Text == "")
            {
                childfrmSpecialApprovals = new frmSpecialApprovals();
                childfrmSpecialApprovals.MdiParent = this;
                childfrmSpecialApprovals.Show();
            }
        }

        private void msbReport_HR_Agents_NewSrs_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(24);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbReport_HR_Agents_LeftSrs_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(25);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbTrans_CRM_DevSpCir_RepDev_Click(object sender, EventArgs e)
        {
            if (childSalesReportingDeviations == null || childSalesReportingDeviations.Text == "")
            {
                childSalesReportingDeviations = new SalesReportingDeviations();
                childSalesReportingDeviations.MdiParent = this;
                childSalesReportingDeviations.Show();
            }
        }

        private void msbReports_CRM_Deviations_or_SPApprovals_Register_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(26);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbReports_CRM_Deviations_or_SPApprovals_SpSummary_Click(object sender, EventArgs e)
        {
            childRepDateCompBranchSelection = new RepDateCompBranchSelection("SPECIAL_APPROVALS_SUMMARY");
            childRepDateCompBranchSelection.MdiParent = this;
            childRepDateCompBranchSelection.Show();
        }

        private void mbReports_Services_OrgChart_Click(object sender, EventArgs e)
        {
            SRReconciliation childSRReconciliation = new SRReconciliation("SERVICE_ORG_CHART");
            childSRReconciliation.MdiParent = this;
            childSRReconciliation.Show();
        }

        private void mbReports_Services_TourBillDetl_Click(object sender, EventArgs e)
        {
            //childEmpFromToSelection = new EmpFromToSelection(27);
            //childEmpFromToSelection.MdiParent = this;
            //childEmpFromToSelection.Show();

        }

        private void mbReports_Services_ActivityStatusRep_Click(object sender, EventArgs e)
        {
            if (childServiceActivityReportFilters == null || childServiceActivityReportFilters.Text == "")
            {
                childServiceActivityReportFilters = new ServiceActivityReportFilters();
                childServiceActivityReportFilters.MdiParent = this;
                childServiceActivityReportFilters.Show();
            }
        }

        private void mbReports_Services_ProdPromotions_Click(object sender, EventArgs e)
        {
            childServiceReports = new ServiceReports("PROD_PRM");
            childServiceReports.MdiParent = this;
            childServiceReports.Show();
        }

        private void msbReport_Stationary_DCReg_Click(object sender, EventArgs e)
        {
            childReportFDateTDate = new ReportFDateTDate("SSERP_REP_STATIONARY_DELIVERY_CHALLAN_REGISTER");
            childReportFDateTDate.MdiParent = this;
            childReportFDateTDate.Show();
        }

        private void msbReport_Stationary_KnockingDCReg_Click(object sender, EventArgs e)
        {
            childReportFDateTDate = new ReportFDateTDate("PENDING KNOCKING REGISTER");
            childReportFDateTDate.MdiParent = this;
            childReportFDateTDate.Show();

        }

        private void msbReport_Stationary_Shortage_WriteOff_Click(object sender, EventArgs e)
        {
            childReportFDateTDate = new ReportFDateTDate("SSCRM_REP_STATIONARY_SHORTAGE_REGISTER");
            childReportFDateTDate.MdiParent = this;
            childReportFDateTDate.Show();
        }

        private void msbReport_Emp_Stationary_IndentReg_Click(object sender, EventArgs e)
        {

            childEmpFromToSelection = new EmpFromToSelection(28);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbTrans_Stationary_Self_Indent_Click(object sender, EventArgs e)
        {
            if (chldStationaryIndent == null || chldStationaryIndent.Text == "")
            {
                chldStationaryIndent = new StationaryIndent("SELF");
                chldStationaryIndent.MdiParent = this;
                chldStationaryIndent.Show();
            }
        }

        private void stationarySelfGRNToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (childStationaryBrGRN == null || childStationaryBrGRN.Text == "")
            {
                childStationaryBrGRN = new StationaryBrGRN("SELF");
                childStationaryBrGRN.MdiParent = this;
                childStationaryBrGRN.Show();
            }

        }

        private void msbTrans_Stationary_Self_IndentList_Click(object sender, EventArgs e)
        {

            if (childStationaryIndentList == null || childStationaryIndentList.Text == "")
            {
                childStationaryIndentList = new StationaryIndentList("SELF");
                childStationaryIndentList.MdiParent = this;
                childStationaryIndentList.Show();
            }
        }

        private void msbTrans_Stationary_Self_IndentApprv_Click(object sender, EventArgs e)
        {

            if (childStationaryIndentList == null || childStationaryIndentList.Text == "")
            {
                childStationaryIndentList = new StationaryIndentList("SHEAD");
                childStationaryIndentList.MdiParent = this;
                childStationaryIndentList.Show();
            }
        }

        private void stationaryShortageWriteOffExcessToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (childShortageStationaryDetails == null || childShortageStationaryDetails.Text == "")
            {
                childShortageStationaryDetails = new ShortageStationaryDetails();
                childShortageStationaryDetails.MdiParent = this;
                childShortageStationaryDetails.Show();
            }
        }

        private void msbTrans_FA_ReceiptVoucher_Click(object sender, EventArgs e)
        {
            if (childReceiptVoucher == null || childReceiptVoucher.Text == "")
            {
                childReceiptVoucher = new ReceiptVoucher();
                childReceiptVoucher.MdiParent = this;
                childReceiptVoucher.Show();
            }
        }

        private void msbTrans_FA_JournalVoucher_Click(object sender, EventArgs e)
        {
            if (childJournalVoucher == null || childJournalVoucher.Text == "")
            {
                childJournalVoucher = new JournalVoucher();
                childJournalVoucher.MdiParent = this;
                childJournalVoucher.Show();
            }
        }

        private void msb_Reports_HR_TrningPrgs_Plan_Prg_Detl_Click(object sender, EventArgs e)
        {
            if (CommonData.LogUserId.ToUpper() == "ADMIN" || CommonData.LogUserRole == "MANAGEMENT")
            {
                childAfterTrainingEmpPerf = new AfterTrainingEmpPerf("PLAN_PROGRAMS");
                childAfterTrainingEmpPerf.MdiParent = this;
                childAfterTrainingEmpPerf.Show();               
            }
            else
            {
                childEmpFromToSelection = new EmpFromToSelection(29);
                childEmpFromToSelection.MdiParent = this;
                childEmpFromToSelection.Show();
            }
        }

        private void msb_Reports_HR_TrningPrgs_Actual_Prg_Detl_Click(object sender, EventArgs e)
        {
            if (CommonData.LogUserId.ToUpper() == "ADMIN" || CommonData.LogUserRole == "MANAGEMENT")
            {
                childAfterTrainingEmpPerf = new AfterTrainingEmpPerf("ACTUAL_PROGRAMS");
                childAfterTrainingEmpPerf.MdiParent = this;
                childAfterTrainingEmpPerf.Show();
            }
            else
            {
                childEmpFromToSelection = new EmpFromToSelection(30);
                childEmpFromToSelection.MdiParent = this;
                childEmpFromToSelection.Show();
            }
        }

        private void msb_Reports_HR_TrningPrgs_Aft_Tr_Emp_Perf_Click(object sender, EventArgs e)
        {
            if (childAfterTrainingEmpPerf == null || childAfterTrainingEmpPerf.Text == "")
            {
                childAfterTrainingEmpPerf = new AfterTrainingEmpPerf("EMP_PERF");
                childAfterTrainingEmpPerf.MdiParent = this;
                childAfterTrainingEmpPerf.Show();
            }
        }

        private void msbReport_StockPoint_SPDmgGRNDetl_SPvsGL_Click(object sender, EventArgs e)
        {
            if (childRepDateCompBranchSelection == null || childRepDateCompBranchSelection.Text == "")
            {
                childRepDateCompBranchSelection = new RepDateCompBranchSelection("STK_RET_GCGL");
                childRepDateCompBranchSelection.MdiParent = this;
                childRepDateCompBranchSelection.Show();
            }
        }

        private void msbReport_StockPoint_SPDmgGRNDetl_SPvsGLVsPR_Click(object sender, EventArgs e)
        {
            if (childRepDateCompBranchSelection == null || childRepDateCompBranchSelection.Text == "")
            {
                childRepDateCompBranchSelection = new RepDateCompBranchSelection("STK_RET_GCGL_PR");
                childRepDateCompBranchSelection.MdiParent = this;
                childRepDateCompBranchSelection.Show();
            }
        }

        private void msbReport_StockPoint_SPDmgGRNDetl_BrSumm_Click(object sender, EventArgs e)
        {
            if (childRepDateCompBranchSelection == null || childRepDateCompBranchSelection.Text == "")
            {
                childRepDateCompBranchSelection = new RepDateCompBranchSelection("STK_RET_GCGL_BR_SUMM");
                childRepDateCompBranchSelection.MdiParent = this;
                childRepDateCompBranchSelection.Show();
            }
        }

        private void msbReport_StockPoint_SPDmgGRNDetl_GLSUmm_Click(object sender, EventArgs e)
        {
            if (childEmpFromToSelection == null || childEmpFromToSelection.Text == "")
            {
                childEmpFromToSelection = new EmpFromToSelection(31);
                childEmpFromToSelection.MdiParent = this;
                childEmpFromToSelection.Show();
            }
        }

        private void msbReport_CRM_InvPrint_Click(object sender, EventArgs e)
        {
            if (childfrmRPFilterFinFromNoToNo == null || childfrmRPFilterFinFromNoToNo.Text == "")
            {
                childfrmRPFilterFinFromNoToNo = new frmRPFilterFinFromNoToNo();
                childfrmRPFilterFinFromNoToNo.MdiParent = this;
                childfrmRPFilterFinFromNoToNo.Show();
            }
        }

      

        private void msbTrans_ServiceDept_Emp_DAR_Entry_Click(object sender, EventArgs e)
        {
            if (childEmployeeDARWithTourBills == null || childEmployeeDARWithTourBills.Text == "")
            {
                childEmployeeDARWithTourBills = new EmployeeDARWithTourBills();
                childEmployeeDARWithTourBills.MdiParent = this;
                childEmployeeDARWithTourBills.Show();
            }
        }

        private void msbTrans_ServiceDept_Inv_Verification_Click(object sender, EventArgs e)
        {
            if (childfrmInvoiceVerification == null || childfrmInvoiceVerification.Text == "")
            {
                childfrmInvoiceVerification = new frmInvoiceVerification();
                childfrmInvoiceVerification.MdiParent = this;
                childfrmInvoiceVerification.Show();
            }
        }

        private void mbReports_Services_TourExpenses_Emp_Wise_Click(object sender, EventArgs e)
        {
            childServiceActivitiesReport = new ServiceActivitiesReport("TOUR_EXPENSES");
            childServiceActivitiesReport.MdiParent = this;
            childServiceActivitiesReport.Show();
        }

        private void mbReports_Services_TourExpenses_Branch_Wise_Click(object sender, EventArgs e)
        {
            childRepDateCompBranchSelection = new RepDateCompBranchSelection("TOUR_EXPENSES");
            childRepDateCompBranchSelection.MdiParent = this;
            childRepDateCompBranchSelection.Show();

        }

        private void msbReports_StockPoint_DCST_Reg_Click(object sender, EventArgs e)
        {
         
            if (CommonData.LogUserRole == "MANAGEMENT" || CommonData.LogUserId.ToUpper() == "ADMIN")
            {
                childRepDateCompBranchSelection = new RepDateCompBranchSelection("STOCKPOINT_DCST");
                childRepDateCompBranchSelection.MdiParent = this;
                childRepDateCompBranchSelection.Show();
            }
                
            else
            {
                chldfrmDoorknocks = new frmDoorknocks("STOCKPOINT_DCST");
                chldfrmDoorknocks.MdiParent = this;
                chldfrmDoorknocks.Show();
            }
        }

        private void msbReports_StockPoint_DCST_IntrRec_Click(object sender, EventArgs e)
        {
            if (childRepDateCompBranchSelection == null || childRepDateCompBranchSelection.Text == "")
            {
                childRepDateCompBranchSelection = new RepDateCompBranchSelection("SP_DCST_INTRSTATE_EQLCOMP");
                childRepDateCompBranchSelection.MdiParent = this;
                childRepDateCompBranchSelection.Show();
            }
        }

        private void msbReports_StockPoint_DCST_PurchReg_Click(object sender, EventArgs e)
        {
            if (childRepDateCompBranchSelection == null || childRepDateCompBranchSelection.Text == "")
            {
                childRepDateCompBranchSelection = new RepDateCompBranchSelection("SP_DCST_INTRSTATE_DIFFCOMP");
                childRepDateCompBranchSelection.MdiParent = this;
                childRepDateCompBranchSelection.Show();
            }
        }

        private void stockAdjustToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (childTransportCostSummary == null || childTransportCostSummary.Text == "")
            {
                childTransportCostSummary = new TransportCostSummary();
                childTransportCostSummary.MdiParent = this;
                childTransportCostSummary.Show();
            }

        }

        private void msbTrans_CRM_Stk_Adjust_Click(object sender, EventArgs e)
        {
            if (childSalesContraForReconsialtion == null || childSalesContraForReconsialtion.Text == "")
            {
                childSalesContraForReconsialtion = new SalesContraForReconsialtion();
                childSalesContraForReconsialtion.MdiParent = this;
                childSalesContraForReconsialtion.Show();
            }
        }

        private void msbTrans_PU_Trans_GCTransCost_Click(object sender, EventArgs e)
        {
            if (childTransportCostSummary == null || childTransportCostSummary.Text == "")
            {
                childTransportCostSummary = new TransportCostSummary();
                childTransportCostSummary.MdiParent = this;
                childTransportCostSummary.Show();
            }
        }

        private void msbTrans_SP_Trns_GCTransCost_Click(object sender, EventArgs e)
        {
            if (childTransportCostSummary == null || childTransportCostSummary.Text == "")
            {
                childTransportCostSummary = new TransportCostSummary();
                childTransportCostSummary.MdiParent = this;
                childTransportCostSummary.Show();
            }
        }

        private void msbReports_CRM_Docm_CrtsRecSum_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(44);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msb_Reports_HR_BioMetrics_DailyAtteBR_Click(object sender, EventArgs e)
        {
            if (childBranchAttendence == null || childBranchAttendence.Text == "")
            {
                childBranchAttendence = new BranchAttendence();
                childBranchAttendence.MdiParent = this;
                childBranchAttendence.Show();
            }
        }

        private void msbReports_CRM_Spirals_FieldSup_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(32);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbReports_CRM_Spirals_BPS_SBTN_SR_Bulletins_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(45);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_CRM_Spirals_OrgChart_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(46);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void transportCcostSalesReplacementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(47);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_CRM_Spirals_BPS_SBTN_GC_GL_Bulletins_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(53);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
 
        }
        

        private void msbReports_CRM_Spirals_BPS_StkRec_GC_GL_Wise_Click(object sender, EventArgs e)
        {
            
            chldfrmDoorknocks = new frmDoorknocks(48);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_CRM_Spirals_BPS_StkRec_TM_and_ABOVE_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(49);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReport_CRM_Audit_Reports_Click(object sender, EventArgs e)
        {
            if (childfrmBranchAuditReportsFilters == null || childfrmBranchAuditReportsFilters.Text == "")
            {
                childfrmBranchAuditReportsFilters = new frmBranchAuditReportsFilters();
                childfrmBranchAuditReportsFilters.MdiParent = this;
                childfrmBranchAuditReportsFilters.Show();
            }
        }

        private void mbReports_Audit_AuditReports_BR_PU_TU_Click(object sender, EventArgs e)
        {
            childfrmBranchAuditReportsFilters = new frmBranchAuditReportsFilters(0);
            childfrmBranchAuditReportsFilters.MdiParent = this;
            childfrmBranchAuditReportsFilters.Show();

        }

        private void mbReports_Audit_Emp_Wise_AuditPoints_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(34);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbTrans_SP_DCST_Click(object sender, EventArgs e)
        {
            if (childDeliveryChallanForm == null || childDeliveryChallanForm.Text == "")
            {
                childDeliveryChallanForm = new DeliveryChallan("DCST");
                childDeliveryChallanForm.MdiParent = this;
                childDeliveryChallanForm.Show();
            }
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //try
            //{
            //    string sItemText = e.ClickedItem.Text;
            //    string sItemValue = e.ClickedItem.Name;
            //    objDB = new SQLDB();
            //    SqlParameter[] param = new SqlParameter[3];
            //    string sText = "INSERT INTO USER_MENU_NAV_HIST (UMNH_USER_ID" +
            //                    ", UMNH_MENU_LABLE, UMNH_MENU_DESC, UMNH_NAV_DATE" +
            //                    ", UMNH_NAV_TIME, UMNH_NAV_TIMESTAMP) " +
            //                    "VALUES(@UserID, @MenuText, @MenuDesc" +
            //                    ", CONVERT(VARCHAR(11), GETDATE(), 113)" +
            //                    ", CONVERT(VARCHAR(8), GETDATE(), 114), getdate())";
            //    param[0] = objDB.CreateParameter("@UserID", DbType.String, CommonData.LogUserId, ParameterDirection.Input);
            //    param[1] = objDB.CreateParameter("@MenuText", DbType.String, e.ClickedItem.Text.Replace("&", ""), ParameterDirection.Input);
            //    param[2] = objDB.CreateParameter("@MenuDesc", DbType.String, e.ClickedItem.Name, ParameterDirection.Input);
            //    objDB.ExecuteSaveData(CommandType.Text, sText, param);
            //}
            //catch (Exception ex)
            //{ }


        }

        private void menuStrip_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void msbReports_CRM_Spirals_OrdFrmRec_GcGl_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(50);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_CRM_Spirals_OrdFrmRec_TMAbove_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(51);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_CRM_Spirals_BPS_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(52);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_CRM_Spirals_BPS_SBTN_TM_Above_Bulletins_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(54);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void mbReports_Services_AO_Wise_Reconciliation_stmt_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(55);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();

        }

        private void msbReports_CRM_Spirals_AdvReg_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(56);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_CRM_Spirals_DocClCert_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(58);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_CRM_Spirals_AdvRefReg_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(35);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbReports_CRM_Spirals_GPDocSheet_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(59);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_CRM_Spirals_GLWise_Stock_Sum_Click(object sender, EventArgs e)
        {
            
        }

        private void msbReport_StockPoint_Form_N_Reg_Click(object sender, EventArgs e)
        {
            childDamaged_Stock_Received_Details = new Damaged_Stock_Received_Details(3);
            childDamaged_Stock_Received_Details.MdiParent = this;
            childDamaged_Stock_Received_Details.Show();
        }

        private void msbReports_CRM_Spirals_SalesRegAll_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(60);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_CRM_Spirals_BPS_StkRec_Individual_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(57);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_CRM_Spirals_Staff_Working_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(6);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbReports_CRM_Spirals_Staff_Recruited_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(24);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbReports_CRM_Spirals_Staff_Resigned_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(25);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbReport_StockPoint_SPDmgGRNDetl_SPvsPU_Click(object sender, EventArgs e)
        {
            childRepDateCompBranchSelection = new RepDateCompBranchSelection("PU_DAMAGE_STOCK_DETL");
            childRepDateCompBranchSelection.MdiParent = this;
            childRepDateCompBranchSelection.Show();
        }

        private void msbReports_MIS_StockPoints_PU_Damage_stock_Click(object sender, EventArgs e)
        {
            childRepDateCompBranchSelection = new RepDateCompBranchSelection("PU_DAMAGE_STOCK_SUMMARY");
            childRepDateCompBranchSelection.MdiParent = this;
            childRepDateCompBranchSelection.Show();
        }

        private void msbReports_StockPoint_DCST_WithInState_Stk_Receipts_Click(object sender, EventArgs e)
        {
            childRepDateCompBranchSelection = new RepDateCompBranchSelection("SP_DCST_WITH_IN_STATE");
            childRepDateCompBranchSelection.MdiParent = this;
            childRepDateCompBranchSelection.Show();            
        }

        private void msbReports_HR_Recruitement_Rec_Analysis_Click(object sender, EventArgs e)
        {
            if (childfrmRecruitmentAnalysis == null || childfrmRecruitmentAnalysis.Text == "")
            {
                childfrmRecruitmentAnalysis = new frmRecruitmentAnalysis();
                childfrmRecruitmentAnalysis.MdiParent = this;
                childfrmRecruitmentAnalysis.Show();
            }
        }

        private void msbReports_HR_Recruitement_HRWiseRecVsResgn_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(61);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void stockDespatchesReturnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            childReportFDateTDate = new ReportFDateTDate("SSCRM_REP_GL_WISE_STOCK_RECONCILLATION");
            childReportFDateTDate.MdiParent = this;
            childReportFDateTDate.Show();
        }

        private void bRWiseStockReconciliationToolStripMenuItem_Click(object sender, EventArgs e)
        {
                childBranchReportFilter = new BranchReportFilter(1);
                childBranchReportFilter.MdiParent = this;
                childBranchReportFilter.Show();
            
        }

        private void msbReports_MIS_SBS_AllIndia_LowPerList_Click(object sender, EventArgs e)
        {
            childfrmSelectionForLowPerfs = new frmSelectionForLowPerfs();
            childfrmSelectionForLowPerfs.MdiParent = this;
            childfrmSelectionForLowPerfs.Show();
        }

        private void msbTrans_AssetInv_Calibration_Certificate_Click(object sender, EventArgs e)
        {
            if (childCalibration_Certificate == null || childCalibration_Certificate.Text == "")
            {
                childCalibration_Certificate = new Calibration_Certificate();
                childCalibration_Certificate.MdiParent = this;
                childCalibration_Certificate.Show();
            }

        }

        private void mbReports_Services_Ao_Wise_Repl_Reg_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(36);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void mbReports_IT_OtherAssets_Calibraton_Certificate_Click(object sender, EventArgs e)
        {
            if (childFDateTDateBranchSelection == null || childFDateTDateBranchSelection.Text == "")
            {
                childFDateTDateBranchSelection = new FDateTDateBranchSelection(3);
                childFDateTDateBranchSelection.MdiParent = this;
                childFDateTDateBranchSelection.Show();
            }
        }

        private void msbReport_Stationary_Branch_DC_Filter_Click(object sender, EventArgs e)
        {

            if (childFDateTDateBranchSelection == null || childFDateTDateBranchSelection.Text == "")
            {
                childFDateTDateBranchSelection = new FDateTDateBranchSelection(2);
                childFDateTDateBranchSelection.MdiParent = this;
                childFDateTDateBranchSelection.Show();
            }
        }

        private void msbReport_StockPoint_TripSheet_Trspt_Cst_Mnth_Click(object sender, EventArgs e)
        {
            childRepDateCompBranchSelection = new RepDateCompBranchSelection("SP_TRANSPORT_COST");
            childRepDateCompBranchSelection.MdiParent = this;
            childRepDateCompBranchSelection.Show(); 
        }

        private void msbReport_StockPoint_Stock_Trns_Sum_Click(object sender, EventArgs e)
        {
            childRepDateCompBranchSelection = new RepDateCompBranchSelection("STOCK_TRANSACTIONS_SUM");
            childRepDateCompBranchSelection.MdiParent = this;
            childRepDateCompBranchSelection.Show(); 
        }

        private void msbReports_MIS_Stationary_Br_Summary_Click(object sender, EventArgs e)
        {
            childStationaryCategory = new StationaryCategory(1);
            childStationaryCategory.MdiParent = this;
            childStationaryCategory.Show();

        }

        private void msbReport_StockPoint_TripSheet_Trip_Wise_Cost_Click(object sender, EventArgs e)
        {
            childRepDateCompBranchSelection = new RepDateCompBranchSelection("SP_TRNSP_COST_TRIP_WISE");
            childRepDateCompBranchSelection.MdiParent = this;
            childRepDateCompBranchSelection.Show(); 

        }

        private void msbMaster_General_BR_ZonalHead_Mas_Click(object sender, EventArgs e)
        {
            if (childZonalHeadMaster == null || childZonalHeadMaster.Text == "")
            {
                childZonalHeadMaster = new ZonalHeadMaster();
                childZonalHeadMaster.MdiParent = this;
                childZonalHeadMaster.Show();
            }
        }

        private void petrolRateMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            childPetrolRateMaster = new PetrolRateMaster();
            childPetrolRateMaster.MdiParent = this;
            childPetrolRateMaster.Show();
        }

        private void getFileNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadPicstoTable childPetrolRateMaster = new UploadPicstoTable();
            childPetrolRateMaster.MdiParent = this;
            childPetrolRateMaster.Show();
        }

        private void msbReports_MIS_SBS_Stmt_Of_Field_Support_Deveations_Click(object sender, EventArgs e)
        {
            childReportFilters = new ReportFilters("Field Support");
            childReportFilters.MdiParent = this;
            childReportFilters.Show();
        }

        private void msbTrans_HR_Trai_Meetings_Meet_Plan_Click(object sender, EventArgs e)
        {
            childTrainingPlanningDetails = new TrainingPlanningDetails("BR");
            childTrainingPlanningDetails.MdiParent = this;
            childTrainingPlanningDetails.Show();

        }

        private void msbTrans_HR_Trai_Meetings_Meet_Actuals_Click(object sender, EventArgs e)
        {
            childTrainingProgramDetails = new TrainingProgramDetails("BR");
            childTrainingProgramDetails.MdiParent = this;
            childTrainingProgramDetails.Show();   
        }

        private void msb_Reports_HR_TrningPrgs_Meeting_Detl_Click(object sender, EventArgs e)
        {
            childRepDateCompBranchSelection = new RepDateCompBranchSelection("BRANCH_MEETING");
            childRepDateCompBranchSelection.MdiParent = this;
            childRepDateCompBranchSelection.Show(); 
        }

        private void msbReports_MIS_SalesBulletinsSummery_RecVsRetSRs_Click(object sender, EventArgs e)
        {
            childReportFilters = new ReportFilters("RECRUITMENT_VS_RETAINED_SRS");
            childReportFilters.MdiParent = this;
            childReportFilters.Show();
        }

        private void msbTrans_HR_Biometrics_BrODForm_Click(object sender, EventArgs e)
        {
            if (childOnDutySlip == null || childOnDutySlip.Text == "")
            {
                childOnDutySlip = new OnDutySlip("BR");
                childOnDutySlip.MdiParent = this;
                childOnDutySlip.Show();
            }
        }

        private void msbTrans_TM_And_above_PMD_Click(object sender, EventArgs e)
        {
            childfrmTMAndAbovePMD = new frmTMAndAbovePMD();
            childfrmTMAndAbovePMD.MdiParent = this;
            childfrmTMAndAbovePMD.Show();
        }

        private void msbReports_MIS_SalesBulletinsSummery_Grps_SalesAnaly_Click(object sender, EventArgs e)
        {
            childReportFilters = new ReportFilters("SALES_ANALYSIS");
            childReportFilters.MdiParent = this;
            childReportFilters.Show();
        }

        private void msbTrans_HR_Biometrics_BR_LeaveForm_Click(object sender, EventArgs e)
        {
            if (childLeaveEntryForm == null || childLeaveEntryForm.Text == "")
            {
                childLeaveEntryForm = new LeaveEntryForm("BR");
                childLeaveEntryForm.MdiParent = this;
                childLeaveEntryForm.Show();
            }
        }

        private void msbTrans_HR_Biometrics_BR_COff_Form_Click(object sender, EventArgs e)
        {
            if (childCompensatoryOFF == null || childCompensatoryOFF.Text == "")
            {
                childCompensatoryOFF = new CompensatoryOFF("BR");
                childCompensatoryOFF.MdiParent = this;
                childCompensatoryOFF.Show();
            }
        }

        private void msbTrans_HR_Biometrics_BR_EmpLeavCrdts_Click(object sender, EventArgs e)
        {
            if (childEmployeeLeavesCredit == null || childEmployeeLeavesCredit.Text == "")
            {
                childEmployeeLeavesCredit = new EmployeeLeavesCredit("BR");
                childEmployeeLeavesCredit.MdiParent = this;
                childEmployeeLeavesCredit.Show();
            }
        }
        
        private void msbTrans_HR_MasterEdit_EmpHist_Click(object sender, EventArgs e)
        {
            if (childEmployee_History == null || childEmployee_History.Text == "")
            {
                childEmployee_History = new Employee_History();
                childEmployee_History.MdiParent = this;
                childEmployee_History.Show();
            }
        }

        private void msbTrans_HR_MasterEdit_EmpAwardsEntry_Click(object sender, EventArgs e)
        {
            if (childfrmEmpAwardsEntry == null || childfrmEmpAwardsEntry.Text == "")
            {
                childfrmEmpAwardsEntry = new frmEmpAwardsEntry();
                childfrmEmpAwardsEntry.MdiParent = this;
                childfrmEmpAwardsEntry.Show();
            }
        }

        private void msb_Reports_HR_EmpInfo_EmpAwards_Click(object sender, EventArgs e)
        {
            childRepDateCompBranchSelection = new RepDateCompBranchSelection("AWARDS_DETL");
            childRepDateCompBranchSelection.MdiParent = this;
            childRepDateCompBranchSelection.Show(); 
        }

        private void msbTrans_StockPoint_Sales_Inv_Click(object sender, EventArgs e)
        {
            if (childSPInvoice == null || childSPInvoice.Text == "")
            {
                childSPInvoice = new SPInvoice();
                childSPInvoice.MdiParent = this;
                childSPInvoice.Show();
            }
        }

        private void msbReport_StockPoint_Sales_Inv_Reg_Click(object sender, EventArgs e)
        {
            chldfrmDoorknocks = new frmDoorknocks(62);
            chldfrmDoorknocks.MdiParent = this;
            chldfrmDoorknocks.Show();
        }

        private void msbTrans_StockPoint_GCGL_Adv_Click(object sender, EventArgs e)
        {
            if (childGCGLAdvances == null || childGCGLAdvances.Text == "")
            {
                childGCGLAdvances = new GCGLAdvances();
                childGCGLAdvances.MdiParent = this;
                childGCGLAdvances.Show();
            }
        }

        private void msbReport_StockPoint_Sales_Inv_Print_Click(object sender, EventArgs e)
        {
            childfrmRPFilterFinFromNoToNo = new frmRPFilterFinFromNoToNo("SP_INVOICE_PRINT");
            childfrmRPFilterFinFromNoToNo.MdiParent = this;
            childfrmRPFilterFinFromNoToNo.Show();
        }

        private void msbReport_HR_Employee_SR_Analysis_Click(object sender, EventArgs e)
        {
            childReportFilters = new ReportFilters("SR_ANALYSIS_BY_AGE");
            childReportFilters.MdiParent = this;
            childReportFilters.Show();

        }

        private void msbReports_MIS_BranchBulletins_Prod_Sale_VatSum_Click(object sender, EventArgs e)
        {
            childRepDateCompBranchSelection = new RepDateCompBranchSelection("STATE_VAT_SUM");
            childRepDateCompBranchSelection.MdiParent = this;
            childRepDateCompBranchSelection.Show(); 
        }

        private void msbTrans_CRM_Stationary_StBulkIndent_Click(object sender, EventArgs e)
        {
            childStationaryBulkIndent = new StationaryBulkIndent();
            childStationaryBulkIndent.MdiParent = this;
            childStationaryBulkIndent.Show();
        }

        private void msbTrans_Audit_Phy_Stk_Cnt_Click(object sender, EventArgs e)
        {
            if (childPhysicalstkcount == null || childPhysicalstkcount.Text == "")
            {
                childPhysicalstkcount = new Physicalstkcount();
                childPhysicalstkcount.MdiParent = this;
                childPhysicalstkcount.Show();
            }
        }

        private void msbTrans_HR_Biometrics_State_Wise_Holidays_Click(object sender, EventArgs e)
        {
            if (childfrmHolidaysMas == null || childfrmHolidaysMas.Text == "")
            {
                childfrmHolidaysMas = new frmHolidaysMas();
                childfrmHolidaysMas.MdiParent = this;
                childfrmHolidaysMas.Show();
            }
        }

        private void msb_Reports_HR_BioMetrics_EmpDailyAttd_Click(object sender, EventArgs e)
        {
            childEmpFromToSelection = new EmpFromToSelection(37);
            childEmpFromToSelection.MdiParent = this;
            childEmpFromToSelection.Show();
        }

        private void msbTrans_HR_MasterEdit_EmpStatusRollback_Click(object sender, EventArgs e)
        {
            childEmpWorkingStatusRollback = new EmpWorkingStatusRollback();
            childEmpWorkingStatusRollback.MdiParent = this;
            childEmpWorkingStatusRollback.Show();
        }

        private void msbTrans_CRM_Vehicle_LegalNotice_Click(object sender, EventArgs e)
        {
            childVehicleLegalNotice = new VehicleLegalNotice();
            childVehicleLegalNotice.MdiParent = this;
            childVehicleLegalNotice.Show();
        }

        private void msbTrans_HR_Biometrics_Emp_OD_Branch_List_Click(object sender, EventArgs e)
        {
            frmEmpODBranchList childfrmEmpODBranchList = new frmEmpODBranchList();
            childfrmEmpODBranchList.MdiParent = this;
            childfrmEmpODBranchList.Show();
        }

        private void msbTrans_FA_Denomination_Click(object sender, EventArgs e)
        {
            Denominations childDenominations = new Denominations();
            childDenominations.MdiParent = this;
            childDenominations.Show();
        }

        private void mbReports_FA_CashBook_Click(object sender, EventArgs e)
        {
            CashBookRegister childCashBookRegister = new CashBookRegister("CASH","BOOK");
            childCashBookRegister.MdiParent = this;
            childCashBookRegister.Show();
        }

        private void mbReports_FA_DFR_Click(object sender, EventArgs e)
        {
            HRAttendence childHRAttendence = new HRAttendence("5");
            childHRAttendence.MdiParent = this;
            childHRAttendence.Show();
        }

        private void msb_Reports_HR_BioMetrics_LeaveReconsilation_Click(object sender, EventArgs e)
        {
            childPayRollReports = new PayRollReports("EMP_LEAVE_RECONC_STMT");
            childPayRollReports.MdiParent = this;
            childPayRollReports.Show();
        }

        private void msbMasters_HRISMasters_PMS_KRAMaster_Click(object sender, EventArgs e)
        {
            childKRAMaster = new KRAMaster();
            childKRAMaster.MdiParent = this;
            childKRAMaster.Show();
        }

        private void msbTrans_HR_MasterEdit_EmpResigUpdate_Click(object sender, EventArgs e)
        {
            childfrmEmpResignations = new frmEmpResignations();
            childfrmEmpResignations.MdiParent = this;
            childfrmEmpResignations.Show();
        }

        private void msbMasters_FA_OB_Click(object sender, EventArgs e)
        {
            childOpeningBranchBalances = new OpeningBranchBalances();
            childOpeningBranchBalances.MdiParent = this;
            childOpeningBranchBalances.Show();
        }

        private void mbReports_Audit_Phy_Stk_CntRep_Click(object sender, EventArgs e)
        {
            childStockRepFilter = new StockRepFilter();
            childStockRepFilter.MdiParent = this;
            childStockRepFilter.Show();
        }

        private void msbReports_HR_Strengths_Sales_manpower_Click(object sender, EventArgs e)
        {
            childReportFilters = new ReportFilters("SALES_EMP_LIST");
            childReportFilters.MdiParent = this;
            childReportFilters.Show();

        }

        private void msbTrans_CRM_Sales_Hierarch_SRAdoption_Click(object sender, EventArgs e)
        {
            childSRAdoption = new SRAdoption();
            childSRAdoption.MdiParent = this;
            childSRAdoption.Show();
        }

        private void msbTrans_SP_Stk_Dumping_Click(object sender, EventArgs e)
        {
            childStockDumping = new StockDumping();
            childStockDumping.MdiParent = this;
            childStockDumping.Show();
        }

        private void mbReports_FA_ReceiptReg_Click(object sender, EventArgs e)
        {
            if (childHRAttendence == null || childHRAttendence.Text == "")
            {
                childHRAttendence = new HRAttendence("6");
                childHRAttendence.MdiParent = this;
                childHRAttendence.Show();
            }
        }

        private void mbReports_FA_Denim_Click(object sender, EventArgs e)
        {
            if (childHRAttendence == null || childHRAttendence.Text == "")
            {
                childHRAttendence = new HRAttendence("7");
                childHRAttendence.MdiParent = this;
                childHRAttendence.Show();
            }
        }

       

      
      

     
       
    }
}
